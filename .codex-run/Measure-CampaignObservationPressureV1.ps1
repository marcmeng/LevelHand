param(
    [string]$IndexCsv = "Exports/C5V4F/Docs/campaign500_rhythm_v4_final_per_level_config_index.csv",
    [string]$PackageRoot = "Exports/C5V4F/U",
    [string]$OutputCsv = ".codex-run/c5v4f_hard50_observation_pressure_v1.csv",
    [string]$SummaryPath = ".codex-run/c5v4f_hard50_observation_pressure_v1_summary.md",
    [int]$MaxRows = 50,
    [string[]]$SectionRoles = @("Hard", "PeakExtreme"),
    [switch]$IncludeAllNormal
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

function Clamp01 {
    param([double]$Value)
    if ($Value -lt 0.0) { return 0.0 }
    if ($Value -gt 1.0) { return 1.0 }
    return $Value
}

function SafeDiv {
    param([double]$Numerator, [double]$Denominator)
    if ([math]::Abs($Denominator) -lt 0.000001) { return 0.0 }
    return $Numerator / $Denominator
}

function Decode-IndexHex {
    param([string]$Hex)

    if ([string]::IsNullOrWhiteSpace($Hex)) { return @() }

    $clean = $Hex -replace '[^0-9A-Fa-f]', ''
    if ($clean.Length -lt 8) { return @() }

    $values = New-Object System.Collections.Generic.List[int]
    for ($i = 0; $i + 7 -lt $clean.Length; $i += 8) {
        $bytes = [byte[]]@(
            [Convert]::ToByte($clean.Substring($i, 2), 16),
            [Convert]::ToByte($clean.Substring($i + 2, 2), 16),
            [Convert]::ToByte($clean.Substring($i + 4, 2), 16),
            [Convert]::ToByte($clean.Substring($i + 6, 2), 16)
        )
        [void]$values.Add([BitConverter]::ToInt32($bytes, 0))
    }

    return @($values.ToArray())
}

function Resolve-AssetPath {
    param([string]$LoadedAssetPath, [string]$Root)

    $relative = $LoadedAssetPath -replace '/', '\'
    $candidate = Join-Path $Root $relative
    if (Test-Path -LiteralPath $candidate) {
        return (Resolve-Path -LiteralPath $candidate).Path
    }

    if (Test-Path -LiteralPath $relative) {
        return (Resolve-Path -LiteralPath $relative).Path
    }

    return $candidate
}

function Read-LevelAsset {
    param([string]$Path)

    $lines = Get-Content -LiteralPath $Path
    $width = $null
    $height = $null
    $chains = New-Object System.Collections.Generic.List[object]
    $blockIndices = @()
    $inArrows = $false

    foreach ($line in $lines) {
        if ($null -eq $width -and $line -match '^\s*width:\s*(\d+)\s*$') {
            $width = [int]$matches[1]
            continue
        }

        if ($null -eq $height -and $line -match '^\s*height:\s*(\d+)\s*$') {
            $height = [int]$matches[1]
            continue
        }

        if ($line -match '^\s*arrows:\s*$') {
            $inArrows = $true
            continue
        }

        if ($line -match '^\s*blockIndices:\s*(.*)$') {
            $inArrows = $false
            $blockIndices = @(Decode-IndexHex $matches[1])
            continue
        }

        if ($inArrows -and $line -match '^\s*-\s*indices:\s*([0-9A-Fa-f]+)\s*$') {
            $indices = @(Decode-IndexHex $matches[1])
            if ($indices.Count -gt 0) {
                [void]$chains.Add([int[]]$indices)
            }
            continue
        }
    }

    if ($null -eq $width -or $null -eq $height) {
        throw "Missing width/height in asset: $Path"
    }

    return [pscustomobject]@{
        Width = [int]$width
        Height = [int]$height
        Chains = @($chains.ToArray())
        BlockIndices = @($blockIndices)
    }
}

function Get-HeadDirection {
    param([int[]]$Chain, [int]$Width)

    if ($Chain.Count -lt 2) { return $null }

    $delta = [int]$Chain[0] - [int]$Chain[1]
    if ($delta -eq 1) {
        return [pscustomobject]@{ Name = "R"; Dx = 1; Dy = 0 }
    }
    if ($delta -eq -1) {
        return [pscustomobject]@{ Name = "L"; Dx = -1; Dy = 0 }
    }
    if ($delta -eq $Width) {
        return [pscustomobject]@{ Name = "D"; Dx = 0; Dy = 1 }
    }
    if ($delta -eq -$Width) {
        return [pscustomobject]@{ Name = "U"; Dx = 0; Dy = -1 }
    }

    return $null
}

function Get-Region {
    param([int]$Index, [int]$Width, [int]$Height)

    $x = $Index % $Width
    $y = [math]::Floor($Index / $Width)
    $rx = if ($x -lt ($Width / 2.0)) { 0 } else { 1 }
    $ry = if ($y -lt ($Height / 2.0)) { 0 } else { 2 }
    return $rx + $ry
}

function Trace-HeadRay {
    param(
        [int]$HeadIndex,
        [object]$Direction,
        [int]$Width,
        [int]$Height,
        [hashtable]$CellToChain,
        [System.Collections.Generic.HashSet[int]]$BlockSet
    )

    $x = $HeadIndex % $Width
    $y = [math]::Floor($HeadIndex / $Width)
    $distance = 0

    while ($true) {
        $x += [int]$Direction.Dx
        $y += [int]$Direction.Dy

        if ($x -lt 0 -or $x -ge $Width -or $y -lt 0 -or $y -ge $Height) {
            return [pscustomobject]@{
                Escapes = $true
                Distance = $distance
                BlockerChain = -1
                BlockedByBlock = $false
            }
        }

        $distance++
        $idx = ($y * $Width) + $x

        if ($BlockSet.Contains([int]$idx)) {
            return [pscustomobject]@{
                Escapes = $false
                Distance = $distance
                BlockerChain = -2
                BlockedByBlock = $true
            }
        }

        if ($CellToChain.ContainsKey([int]$idx)) {
            return [pscustomobject]@{
                Escapes = $false
                Distance = $distance
                BlockerChain = [int]$CellToChain[[int]$idx]
                BlockedByBlock = $false
            }
        }
    }
}

function Get-LongestDependencyDepth {
    param([int]$ChainCount, [hashtable]$Adjacency)

    $memo = @{}
    $script:CycleHits = 0

    function Visit-DependencyNode {
        param([int]$Node, [hashtable]$Adj, [hashtable]$Memo, [System.Collections.Generic.HashSet[int]]$Visiting)

        if ($Memo.ContainsKey($Node)) { return [int]$Memo[$Node] }
        if ($Visiting.Contains($Node)) {
            $script:CycleHits++
            return 0
        }

        [void]$Visiting.Add($Node)
        $best = 0

        if ($Adj.ContainsKey($Node)) {
            foreach ($next in $Adj[$Node]) {
                $candidate = 1 + (Visit-DependencyNode -Node ([int]$next) -Adj $Adj -Memo $Memo -Visiting $Visiting)
                if ($candidate -gt $best) { $best = $candidate }
            }
        }

        [void]$Visiting.Remove($Node)
        $Memo[$Node] = $best
        return $best
    }

    $maxDepth = 0
    for ($i = 0; $i -lt $ChainCount; $i++) {
        $depth = Visit-DependencyNode -Node $i -Adj $Adjacency -Memo $memo -Visiting ([System.Collections.Generic.HashSet[int]]::new())
        if ($depth -gt $maxDepth) { $maxDepth = $depth }
    }

    return [pscustomobject]@{
        Depth = [int]$maxDepth
        CycleHits = [int]$script:CycleHits
    }
}

function Measure-LevelObservation {
    param([object]$Row, [string]$AssetPath)

    $level = Read-LevelAsset -Path $AssetPath
    $width = [int]$level.Width
    $height = [int]$level.Height
    $chains = @($level.Chains)
    $chainCount = $chains.Count

    $cellToChain = @{}
    for ($chainIndex = 0; $chainIndex -lt $chainCount; $chainIndex++) {
        foreach ($idx in @($chains[$chainIndex])) {
            if (-not $cellToChain.ContainsKey([int]$idx)) {
                $cellToChain[[int]$idx] = [int]$chainIndex
            }
        }
    }

    $blockSet = [System.Collections.Generic.HashSet[int]]::new()
    foreach ($idx in @($level.BlockIndices)) {
        [void]$blockSet.Add([int]$idx)
    }

    $headRegions = @{}
    $validEscapeChains = 0
    $blockedChains = 0
    $blockCellBlockedChains = 0
    $longRayChains = 0
    $immediateRayChains = 0
    $shortRayChains = 0
    $rayDistanceTotal = 0
    $rayDistanceCount = 0
    $invalidHeadDirections = 0
    $chainLengths = New-Object System.Collections.Generic.List[int]
    $edgeKeys = @{}
    $adjacency = @{}
    $outDegree = @{}
    $crossRegionEdges = 0
    $sameRegionEdges = 0

    $longRayThreshold = [math]::Max(4, [math]::Floor([math]::Min($width, $height) * 0.25))

    for ($chainIndex = 0; $chainIndex -lt $chainCount; $chainIndex++) {
        $chain = [int[]]@($chains[$chainIndex])
        [void]$chainLengths.Add($chain.Count)
        if ($chain.Count -eq 0) { continue }

        $head = [int]$chain[0]
        $region = Get-Region -Index $head -Width $width -Height $height
        if (-not $headRegions.ContainsKey($region)) { $headRegions[$region] = 0 }
        $headRegions[$region] = [int]$headRegions[$region] + 1

        $direction = Get-HeadDirection -Chain $chain -Width $width
        if ($null -eq $direction) {
            $invalidHeadDirections++
            continue
        }

        $ray = Trace-HeadRay -HeadIndex $head -Direction $direction -Width $width -Height $height -CellToChain $cellToChain -BlockSet $blockSet
        $rayDistanceTotal += [double]$ray.Distance
        $rayDistanceCount++
        if ([int]$ray.Distance -le 1) { $immediateRayChains++ }
        if ([int]$ray.Distance -le 2) { $shortRayChains++ }
        if ([int]$ray.Distance -ge $longRayThreshold) { $longRayChains++ }

        if ([bool]$ray.Escapes) {
            $validEscapeChains++
            continue
        }

        $blockedChains++
        if ([bool]$ray.BlockedByBlock) {
            $blockCellBlockedChains++
            continue
        }

        $blocker = [int]$ray.BlockerChain
        if ($blocker -lt 0 -or $blocker -eq $chainIndex) { continue }

        $edgeKey = "$blocker->$chainIndex"
        if (-not $edgeKeys.ContainsKey($edgeKey)) {
            $edgeKeys[$edgeKey] = $true

            if (-not $adjacency.ContainsKey($blocker)) {
                $adjacency[$blocker] = New-Object System.Collections.Generic.List[int]
            }
            [void]$adjacency[$blocker].Add([int]$chainIndex)

            if (-not $outDegree.ContainsKey($blocker)) { $outDegree[$blocker] = 0 }
            $outDegree[$blocker] = [int]$outDegree[$blocker] + 1

            $blockerHead = [int]@($chains[$blocker])[0]
            $blockerRegion = Get-Region -Index $blockerHead -Width $width -Height $height
            if ($blockerRegion -ne $region) { $crossRegionEdges++ } else { $sameRegionEdges++ }
        }
    }

    $dependencyEdges = $edgeKeys.Count
    $depthResult = Get-LongestDependencyDepth -ChainCount $chainCount -Adjacency $adjacency
    $criticalBlockers = 0
    foreach ($key in $outDegree.Keys) {
        if ([int]$outDegree[$key] -ge 2) { $criticalBlockers++ }
    }

    $entropy = 0.0
    foreach ($key in $headRegions.Keys) {
        $p = SafeDiv ([double]$headRegions[$key]) ([double]$chainCount)
        if ($p -gt 0.0) {
            $entropy -= $p * ([math]::Log($p) / [math]::Log(2.0))
        }
    }
    $regionEntropyNorm = Clamp01 (SafeDiv $entropy 2.0)

    $avgChainLen = if ($chainLengths.Count -gt 0) { ($chainLengths | Measure-Object -Average).Average } else { 0.0 }
    $maxChainLen = if ($chainLengths.Count -gt 0) { ($chainLengths | Measure-Object -Maximum).Maximum } else { 0 }
    $avgRayLength = SafeDiv $rayDistanceTotal ([double]$rayDistanceCount)
    $openerRatio = SafeDiv ([double]$validEscapeChains) ([double]$chainCount)
    $decoyToOpenerRatio = SafeDiv ([double]$blockedChains) ([double][math]::Max(1, $validEscapeChains))
    $dependencyDensity = SafeDiv ([double]$dependencyEdges) ([double]$chainCount)
    $crossRegionEdgeRate = SafeDiv ([double]$crossRegionEdges) ([double][math]::Max(1, $dependencyEdges))
    $sameRegionEdgeRate = SafeDiv ([double]$sameRegionEdges) ([double][math]::Max(1, $dependencyEdges))
    $longRayRate = SafeDiv ([double]$longRayChains) ([double]$chainCount)
    $immediateRayRate = SafeDiv ([double]$immediateRayChains) ([double]$chainCount)
    $shortRayRate = SafeDiv ([double]$shortRayChains) ([double]$chainCount)

    $score =
        22.0 * (Clamp01 (SafeDiv ([double]$depthResult.Depth) 5.0)) +
        18.0 * (Clamp01 (SafeDiv $dependencyDensity 0.65)) +
        14.0 * (Clamp01 (SafeDiv $crossRegionEdgeRate 0.45)) +
        12.0 * (Clamp01 (SafeDiv $decoyToOpenerRatio 4.0)) +
        10.0 * (Clamp01 (SafeDiv $longRayRate 0.35)) +
        10.0 * (Clamp01 (SafeDiv ([double]$criticalBlockers) 3.0)) +
        8.0 * $regionEntropyNorm

    if ($openerRatio -gt 0.18) {
        $score -= 12.0 * (Clamp01 (SafeDiv ($openerRatio - 0.18) 0.20))
    }
    if ([int]$depthResult.Depth -lt 2) { $score -= 8.0 }
    if ($dependencyDensity -lt 0.15) { $score -= 5.0 }

    $score = [math]::Max(0.0, [math]::Min(100.0, $score))
    $class = if ($score -ge 70.0) {
        "HighObservation"
    } elseif ($score -ge 50.0) {
        "ObservationHard"
    } elseif ($score -ge 30.0) {
        "WatchLight"
    } else {
        "LowObservation"
    }

    $rayLookaheadScore = Clamp01 (SafeDiv ($avgRayLength - 1.0) 3.0)
    $shortRayPenalty = Clamp01 (SafeDiv ($shortRayRate - 0.70) 0.30)
    $immediateRayPenalty = Clamp01 (SafeDiv ($immediateRayRate - 0.55) 0.45)
    $crossReadScore = Clamp01 (SafeDiv $crossRegionEdgeRate 0.35)
    $deepReadScore = Clamp01 (SafeDiv ([double]$depthResult.Depth) 10.0)
    $fanoutReadScore = Clamp01 (SafeDiv ([double]$criticalBlockers) 18.0)
    $openerBandScore = if ($openerRatio -lt 0.03) {
        Clamp01 (SafeDiv $openerRatio 0.03)
    } elseif ($openerRatio -le 0.12) {
        1.0
    } elseif ($openerRatio -le 0.24) {
        Clamp01 (1.0 - (SafeDiv ($openerRatio - 0.12) 0.12))
    } else {
        0.0
    }

    $readDemandScore =
        26.0 * $rayLookaheadScore +
        18.0 * $crossReadScore +
        14.0 * $deepReadScore +
        12.0 * $fanoutReadScore +
        10.0 * $regionEntropyNorm +
        10.0 * $openerBandScore +
        10.0 * (Clamp01 (SafeDiv $longRayRate 0.10)) -
        20.0 * $shortRayPenalty -
        10.0 * $immediateRayPenalty

    $readDemandScore = [math]::Max(0.0, [math]::Min(100.0, $readDemandScore))
    $readDemandClass = if ($readDemandScore -ge 70.0) {
        "ReadVeryHard"
    } elseif ($readDemandScore -ge 55.0) {
        "ReadHard"
    } elseif ($readDemandScore -ge 35.0) {
        "ReadMedium"
    } else {
        "ReadLight"
    }

    $notes = New-Object System.Collections.Generic.List[string]
    if (($Row.sectionWaveRole -eq "Hard" -or $Row.sectionWaveRole -eq "PeakExtreme") -and $score -lt 50.0) {
        [void]$notes.Add("hard_role_low_observation")
    }
    if ($openerRatio -gt 0.24) { [void]$notes.Add("many_openers") }
    if ([int]$depthResult.Depth -lt 2) { [void]$notes.Add("shallow_dependency") }
    if ($crossRegionEdgeRate -lt 0.18) { [void]$notes.Add("mostly_same_region_dependencies") }
    if ($longRayRate -lt 0.12) { [void]$notes.Add("short_head_rays") }
    if ($shortRayRate -gt 0.90) { [void]$notes.Add("adjacent_blockers_dominate") }
    if ($readDemandScore -lt 35.0 -and ($Row.sectionWaveRole -eq "Hard" -or $Row.sectionWaveRole -eq "PeakExtreme")) {
        [void]$notes.Add("hard_role_low_read_demand")
    }
    if ($invalidHeadDirections -gt 0) { [void]$notes.Add("invalid_head_direction=$invalidHeadDirections") }
    if ([int]$depthResult.CycleHits -gt 0) { [void]$notes.Add("dependency_cycles=$($depthResult.CycleHits)") }

    return [pscustomobject]@{
        order = [int]$Row.order
        sectionWaveRole = $Row.sectionWaveRole
        finalStatus = $Row.finalStatus
        productionLane = $Row.productionLane
        productionStyle = $Row.productionStyle
        productionChainLanguage = $Row.productionChainLanguage
        loadedAssetPath = $Row.loadedAssetPath
        width = $width
        height = $height
        chainCount = $chainCount
        maxChainLen = [int]$maxChainLen
        avgChainLen = [math]::Round([double]$avgChainLen, 3)
        validEscapeChains = $validEscapeChains
        blockedChains = $blockedChains
        blockCellBlockedChains = $blockCellBlockedChains
        openerRatio = [math]::Round($openerRatio, 4)
        decoyToOpenerRatio = [math]::Round($decoyToOpenerRatio, 3)
        dependencyEdges = $dependencyEdges
        dependencyDensity = [math]::Round($dependencyDensity, 4)
        dependencyDepth = [int]$depthResult.Depth
        dependencyCycleHits = [int]$depthResult.CycleHits
        crossRegionEdges = $crossRegionEdges
        crossRegionEdgeRate = [math]::Round($crossRegionEdgeRate, 4)
        sameRegionEdgeRate = [math]::Round($sameRegionEdgeRate, 4)
        criticalBlockerChains = $criticalBlockers
        avgEscapeRayLength = [math]::Round($avgRayLength, 3)
        longRayRate = [math]::Round($longRayRate, 4)
        immediateRayRate = [math]::Round($immediateRayRate, 4)
        shortRayRate = [math]::Round($shortRayRate, 4)
        regionEntropy = [math]::Round($regionEntropyNorm, 4)
        observationPressureScore = [math]::Round($score, 2)
        observationClass = $class
        readDemandScore = [math]::Round($readDemandScore, 2)
        readDemandClass = $readDemandClass
        notes = ($notes -join ";")
    }
}

if (-not (Test-Path -LiteralPath $IndexCsv)) {
    throw "Missing index CSV: $IndexCsv"
}

$outDir = Split-Path -Parent $OutputCsv
if (-not [string]::IsNullOrWhiteSpace($outDir)) {
    New-Item -ItemType Directory -Force -Path $outDir | Out-Null
}

$summaryDir = Split-Path -Parent $SummaryPath
if (-not [string]::IsNullOrWhiteSpace($summaryDir)) {
    New-Item -ItemType Directory -Force -Path $summaryDir | Out-Null
}

$selectedRows = Import-Csv -LiteralPath $IndexCsv |
    Where-Object {
        if ($_.category -ne "normal") { return $false }
        if ($IncludeAllNormal) { return $true }
        return $SectionRoles -contains $_.sectionWaveRole
    } |
    Sort-Object { [int]$_.order }

if ($MaxRows -gt 0) {
    $selectedRows = $selectedRows | Select-Object -First $MaxRows
}

$results = New-Object System.Collections.Generic.List[object]
foreach ($row in $selectedRows) {
    $assetPath = Resolve-AssetPath -LoadedAssetPath $row.loadedAssetPath -Root $PackageRoot
    if (-not (Test-Path -LiteralPath $assetPath)) {
        [void]$results.Add([pscustomobject]@{
            order = [int]$row.order
            sectionWaveRole = $row.sectionWaveRole
            finalStatus = $row.finalStatus
            productionLane = $row.productionLane
            productionStyle = $row.productionStyle
            productionChainLanguage = $row.productionChainLanguage
            loadedAssetPath = $row.loadedAssetPath
            width = ""
            height = ""
            chainCount = ""
            maxChainLen = ""
            avgChainLen = ""
            validEscapeChains = ""
            blockedChains = ""
            blockCellBlockedChains = ""
            openerRatio = ""
            decoyToOpenerRatio = ""
            dependencyEdges = ""
            dependencyDensity = ""
            dependencyDepth = ""
            dependencyCycleHits = ""
            crossRegionEdges = ""
            crossRegionEdgeRate = ""
            sameRegionEdgeRate = ""
            criticalBlockerChains = ""
            avgEscapeRayLength = ""
            longRayRate = ""
            immediateRayRate = ""
            shortRayRate = ""
            regionEntropy = ""
            observationPressureScore = 0
            observationClass = "MissingAsset"
            readDemandScore = 0
            readDemandClass = "MissingAsset"
            notes = "missing_asset"
        })
        continue
    }

    [void]$results.Add((Measure-LevelObservation -Row $row -AssetPath $assetPath))
}

$results | Export-Csv -LiteralPath $OutputCsv -NoTypeInformation -Encoding UTF8

$count = $results.Count
$avgScore = if ($count -gt 0) { ($results | Measure-Object -Property observationPressureScore -Average).Average } else { 0.0 }
$avgReadDemandScore = if ($count -gt 0) { ($results | Measure-Object -Property readDemandScore -Average).Average } else { 0.0 }
$classLines = $results |
    Group-Object observationClass |
    Sort-Object Name |
    ForEach-Object { "- $($_.Name): $($_.Count)" }
$readClassLines = $results |
    Group-Object readDemandClass |
    Sort-Object Name |
    ForEach-Object { "- $($_.Name): $($_.Count)" }
$roleLines = $results |
    Group-Object sectionWaveRole |
    Sort-Object Name |
    ForEach-Object {
        $avg = ($_.Group | Measure-Object -Property observationPressureScore -Average).Average
        $avgRead = ($_.Group | Measure-Object -Property readDemandScore -Average).Average
        "- $($_.Name): count=$($_.Count), avgScore=$([math]::Round($avg, 2)), avgReadDemand=$([math]::Round($avgRead, 2))"
    }
$laneLines = $results |
    Group-Object productionLane |
    Sort-Object Name |
    ForEach-Object {
        $name = if ([string]::IsNullOrWhiteSpace($_.Name)) { "(blank)" } else { $_.Name }
        $avg = ($_.Group | Measure-Object -Property observationPressureScore -Average).Average
        $avgRead = ($_.Group | Measure-Object -Property readDemandScore -Average).Average
        "- ${name}: count=$($_.Count), avgScore=$([math]::Round($avg, 2)), avgReadDemand=$([math]::Round($avgRead, 2))"
    }

$bottomRows = $results |
    Sort-Object observationPressureScore, order |
    Select-Object -First 10 |
    ForEach-Object {
        "- order $($_.order) [$($_.sectionWaveRole)] score=$($_.observationPressureScore), read=$($_.readDemandScore), avgRay=$($_.avgEscapeRayLength), shortRay=$($_.shortRayRate), depth=$($_.dependencyDepth), openerRatio=$($_.openerRatio), edges=$($_.dependencyEdges), notes=$($_.notes)"
    }

$topRows = $results |
    Sort-Object @{ Expression = "observationPressureScore"; Descending = $true }, order |
    Select-Object -First 10 |
    ForEach-Object {
        "- order $($_.order) [$($_.sectionWaveRole)] score=$($_.observationPressureScore), read=$($_.readDemandScore), avgRay=$($_.avgEscapeRayLength), shortRay=$($_.shortRayRate), depth=$($_.dependencyDepth), openerRatio=$($_.openerRatio), edges=$($_.dependencyEdges), crossRate=$($_.crossRegionEdgeRate)"
    }

$lowestReadRows = $results |
    Sort-Object readDemandScore, order |
    Select-Object -First 10 |
    ForEach-Object {
        "- order $($_.order) [$($_.sectionWaveRole)] read=$($_.readDemandScore), obs=$($_.observationPressureScore), avgRay=$($_.avgEscapeRayLength), shortRay=$($_.shortRayRate), crossRate=$($_.crossRegionEdgeRate), notes=$($_.notes)"
    }

$highestReadRows = $results |
    Sort-Object @{ Expression = "readDemandScore"; Descending = $true }, order |
    Select-Object -First 10 |
    ForEach-Object {
        "- order $($_.order) [$($_.sectionWaveRole)] read=$($_.readDemandScore), obs=$($_.observationPressureScore), avgRay=$($_.avgEscapeRayLength), shortRay=$($_.shortRayRate), crossRate=$($_.crossRegionEdgeRate), depth=$($_.dependencyDepth)"
    }

$summary = @(
    "# Campaign Observation Pressure V1",
    "",
    "- Input index: $IndexCsv",
    "- Package root: $PackageRoot",
    "- Selected rows: $(if ($IncludeAllNormal) { 'all normal rows' } else { 'normal rows with sectionWaveRole ' + ($SectionRoles -join '/') })",
    "- Max rows: $(if ($MaxRows -gt 0) { $MaxRows } else { 'unlimited' })",
    "- Result CSV: $OutputCsv",
    "- Average observation score: $([math]::Round($avgScore, 2))",
    "- Average read-demand score: $([math]::Round($avgReadDemandScore, 2))",
    "",
    "## Class Distribution",
    $classLines,
    "",
    "## Read Demand Distribution",
    $readClassLines,
    "",
    "## By Section Role",
    $roleLines,
    "",
    "## By Production Lane",
    $laneLines,
    "",
    "## Lowest 10",
    $bottomRows,
    "",
    "## Highest 10",
    $topRows,
    "",
    "## Lowest Read-Demand 10",
    $lowestReadRows,
    "",
    "## Highest Read-Demand 10",
    $highestReadRows,
    "",
    "## Notes",
    "- This is a static authored-geometry proxy, not a replacement for official Greedy/trace validation.",
    "- observationPressureScore measures static dependency density.",
    "- readDemandScore is stricter: adjacent/short blockers are penalized, while long lookahead rays and cross-region dependencies are rewarded."
)

$summary | Set-Content -LiteralPath $SummaryPath -Encoding UTF8

Write-Host "Wrote $OutputCsv"
Write-Host "Wrote $SummaryPath"
Write-Host "Rows: $count; avg observation score: $([math]::Round($avgScore, 2))"
Write-Host "Rows: $count; avg read-demand score: $([math]::Round($avgReadDemandScore, 2))"
