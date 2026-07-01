param(
    [string]$ProjectRoot = "F:\Unityproject\ArrowLevel-Hand",
    [string[]]$SeedCsvPaths = @(),
    [string[]]$SeedAssetRoots = @(),
    [string]$PsgKeepCsv = "Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_hard_production_keep.csv",
    [string]$OutputDir = "Assets/ArrowMagic/SOData/Reports/DirectProcedural",
    [string]$OutputPrefix = "project_seed_style_v1",
    [int]$MaxRowsPerCsv = 0,
    [int]$MaxAssetsPerRoot = 0,
    [switch]$NoDefaultSources,
    [switch]$FastStaticOnly,
    [switch]$DeepDependencyScan
)

$ErrorActionPreference = "Stop"

function Resolve-ProjectPath([string]$path) {
    if ([string]::IsNullOrWhiteSpace($path)) { return "" }
    $p = $path.Trim().Trim('"')
    if ([IO.Path]::IsPathRooted($p)) { return [IO.Path]::GetFullPath($p) }
    return [IO.Path]::GetFullPath((Join-Path $ProjectRoot ($p -replace '/', '\')))
}

function Convert-ToProjectRelative([string]$path) {
    if ([string]::IsNullOrWhiteSpace($path)) { return "" }
    $full = [IO.Path]::GetFullPath($path)
    $root = [IO.Path]::GetFullPath($ProjectRoot).TrimEnd('\')
    if ($full.StartsWith($root, [StringComparison]::OrdinalIgnoreCase)) {
        return ($full.Substring($root.Length).TrimStart('\') -replace '\\', '/')
    }
    return $full
}

function Get-RowValue($row, [string[]]$names) {
    if ($null -eq $row) { return "" }
    foreach ($name in $names) {
        foreach ($prop in $row.PSObject.Properties) {
            if ($prop.Name -ieq $name) {
                return [string]$prop.Value
            }
        }
    }
    return ""
}

function ToDouble($value, [double]$fallback = 0.0) {
    if ($null -eq $value) { return $fallback }
    $s = ([string]$value).Trim()
    if ([string]::IsNullOrWhiteSpace($s)) { return $fallback }
    $out = 0.0
    if ([double]::TryParse($s, [Globalization.NumberStyles]::Float, [Globalization.CultureInfo]::InvariantCulture, [ref]$out)) {
        return $out
    }
    return $fallback
}

function ToInt($value, [int]$fallback = 0) {
    if ($null -eq $value) { return $fallback }
    $s = ([string]$value).Trim()
    if ([string]::IsNullOrWhiteSpace($s)) { return $fallback }
    $out = 0
    if ([int]::TryParse($s, [Globalization.NumberStyles]::Integer, [Globalization.CultureInfo]::InvariantCulture, [ref]$out)) {
        return $out
    }
    return $fallback
}

function Average-Field($rows, [string]$field) {
    $vals = @($rows | ForEach-Object { ToDouble ($_.PSObject.Properties[$field].Value) 0.0 })
    if ($vals.Count -eq 0) { return 0.0 }
    return [Math]::Round((($vals | Measure-Object -Average).Average), 4)
}

function Percentile($values, [double]$p) {
    $arr = @($values | Sort-Object)
    if ($arr.Count -eq 0) { return 0 }
    $idx = [Math]::Min($arr.Count - 1, [Math]::Max(0, [int][Math]::Round(($arr.Count - 1) * $p)))
    return $arr[$idx]
}

function Entropy01($counts) {
    $arr = @($counts | ForEach-Object { [double]$_ })
    $total = ($arr | Measure-Object -Sum).Sum
    if ($total -le 0 -or $arr.Count -le 1) { return 0.0 }
    $e = 0.0
    foreach ($c in $arr) {
        if ($c -le 0) { continue }
        $p = $c / $total
        $e -= $p * [Math]::Log($p)
    }
    return [Math]::Round($e / [Math]::Log($arr.Count), 4)
}

function Histogram-String($counts) {
    $parts = New-Object System.Collections.Generic.List[string]
    for ($i = 0; $i -lt $counts.Count; $i++) {
        if ($counts[$i] -gt 0) { $parts.Add("${i}:$($counts[$i])") }
    }
    return ($parts -join ';')
}

function Decode-UnityIntHexList([string]$hex) {
    $result = New-Object System.Collections.Generic.List[int]
    if ([string]::IsNullOrWhiteSpace($hex)) { return $result }
    $clean = $hex.Trim()
    for ($i = 0; $i + 7 -lt $clean.Length; $i += 8) {
        $bytes = [byte[]]@(
            [Convert]::ToInt32($clean.Substring($i, 2), 16),
            [Convert]::ToInt32($clean.Substring($i + 2, 2), 16),
            [Convert]::ToInt32($clean.Substring($i + 4, 2), 16),
            [Convert]::ToInt32($clean.Substring($i + 6, 2), 16))
        $result.Add([BitConverter]::ToInt32($bytes, 0))
    }
    return $result
}

function Dir-FromDelta([int]$dx, [int]$dy) {
    if ($dx -eq 0 -and $dy -eq 1) { return 0 }
    if ($dx -eq 1 -and $dy -eq 0) { return 1 }
    if ($dx -eq 0 -and $dy -eq -1) { return 2 }
    if ($dx -eq -1 -and $dy -eq 0) { return 3 }
    return -1
}

function Step-Dir([int]$dir, [ref]$x, [ref]$y) {
    if ($dir -eq 0) { $y.Value++ }
    elseif ($dir -eq 1) { $x.Value++ }
    elseif ($dir -eq 2) { $y.Value-- }
    else { $x.Value-- }
}

function Get-OuterSide([int]$x, [int]$y, [int]$width, [int]$height, [int]$band) {
    if ($x -le $band) { return "L" }
    if ($x -ge $width - 1 - $band) { return "R" }
    if ($y -le $band) { return "B" }
    if ($y -ge $height - 1 - $band) { return "T" }
    return "I"
}

function Test-OutwardHead([string]$side, [int]$dir) {
    return (
        ($side -eq "L" -and $dir -eq 3) -or
        ($side -eq "R" -and $dir -eq 1) -or
        ($side -eq "B" -and $dir -eq 2) -or
        ($side -eq "T" -and $dir -eq 0)
    )
}

function Get-RegionIndex([double]$x, [double]$y, [int]$width, [int]$height) {
    $rx = [Math]::Min(2, [Math]::Max(0, [int][Math]::Floor($x * 3.0 / [Math]::Max(1, $width))))
    $ry = [Math]::Min(2, [Math]::Max(0, [int][Math]::Floor($y * 3.0 / [Math]::Max(1, $height))))
    return $ry * 3 + $rx
}

function Get-RegionDistance([int]$a, [int]$b) {
    $ax = $a % 3
    $ay = [int][Math]::Floor($a / 3)
    $bx = $b % 3
    $by = [int][Math]::Floor($b / 3)
    return [Math]::Abs($ax - $bx) + [Math]::Abs($ay - $by)
}

function Parse-LevelAsset([string]$path) {
    $lines = Get-Content -LiteralPath $path
    $levelId = ""
    $width = 0
    $height = 0
    $inAuthored = $false
    $inArrows = $false
    $arrows = New-Object System.Collections.Generic.List[object]

    foreach ($line in $lines) {
        if ($line -match '^\s*levelId:\s*(.+)$') {
            $levelId = $Matches[1].Trim()
            continue
        }
        if ($line -match '^\s*authoredLevel:\s*$') {
            $inAuthored = $true
            $inArrows = $false
            continue
        }
        if ($inAuthored -and $line -match '^\s{2}[A-Za-z_].*:$' -and $line -notmatch '^\s{2}(width|height|arrows|blockIndices):') {
            $inAuthored = $false
            $inArrows = $false
        }
        if (-not $inAuthored) { continue }
        if ($line -match '^\s{4}width:\s*(\d+)') { $width = [int]$Matches[1]; continue }
        if ($line -match '^\s{4}height:\s*(\d+)') { $height = [int]$Matches[1]; continue }
        if ($line -match '^\s{4}arrows:\s*$') { $inArrows = $true; continue }
        if ($line -match '^\s{4}blockIndices:') { $inArrows = $false; continue }
        if ($inArrows -and $line -match '^\s{4}-\s*indices:\s*([0-9A-Fa-f]+)\s*$') {
            $arrows.Add([pscustomobject]@{ indices = Decode-UnityIntHexList $Matches[1] })
        }
    }

    if ($width -le 0 -or $height -le 0 -or $arrows.Count -eq 0) { return $null }
    return [pscustomobject]@{
        path = $path
        levelId = $levelId
        width = $width
        height = $height
        arrows = $arrows
    }
}

function Build-OwnerMap($level) {
    $area = $level.width * $level.height
    $owner = [int[]](0..($area - 1) | ForEach-Object { -1 })
    for ($i = 0; $i -lt $level.arrows.Count; $i++) {
        foreach ($idx in $level.arrows[$i].indices) {
            if ($idx -ge 0 -and $idx -lt $owner.Length) { $owner[$idx] = $i }
        }
    }
    return $owner
}

function Get-OrientedIndices($arrow, [int]$orientation) {
    $arr = @($arrow.indices)
    if ($orientation -eq 1) { [array]::Reverse($arr) }
    return [int[]]$arr
}

function Get-ChainStatsForGate($level) {
    $stats = New-Object 'object[]' $level.arrows.Count
    for ($i = 0; $i -lt $level.arrows.Count; $i++) {
        $indices = @($level.arrows[$i].indices)
        if ($indices.Count -eq 0) {
            $stats[$i] = [pscustomobject]@{ region = 0; centerX = 0.0; centerY = 0.0; length = 0; headDir0 = -1; headDir1 = -1 }
            continue
        }
        $sumX = 0.0
        $sumY = 0.0
        foreach ($idx in $indices) {
            $sumX += ($idx % $level.width)
            $sumY += [int][Math]::Floor($idx / $level.width)
        }
        $cx = $sumX / [Math]::Max(1, $indices.Count)
        $cy = $sumY / [Math]::Max(1, $indices.Count)
        $headDir0 = -1
        $headDir1 = -1
        if ($indices.Count -ge 2) {
            $head = $indices[0]
            $neck = $indices[1]
            $headDir0 = Dir-FromDelta (($head % $level.width) - ($neck % $level.width)) ([int][Math]::Floor($head / $level.width) - [int][Math]::Floor($neck / $level.width))
            $rhead = $indices[$indices.Count - 1]
            $rneck = $indices[$indices.Count - 2]
            $headDir1 = Dir-FromDelta (($rhead % $level.width) - ($rneck % $level.width)) ([int][Math]::Floor($rhead / $level.width) - [int][Math]::Floor($rneck / $level.width))
        }
        $stats[$i] = [pscustomobject]@{
            region = Get-RegionIndex $cx $cy $level.width $level.height
            centerX = $cx
            centerY = $cy
            length = $indices.Count
            headDir0 = $headDir0
            headDir1 = $headDir1
        }
    }
    return $stats
}

function Get-DependencyOptionsForGate($level) {
    $owner = Build-OwnerMap $level
    $options = New-Object 'object[]' $level.arrows.Count
    for ($i = 0; $i -lt $level.arrows.Count; $i++) {
        $perChain = New-Object 'object[]' 2
        for ($o = 0; $o -lt 2; $o++) {
            $indices = Get-OrientedIndices $level.arrows[$i] $o
            $deps = New-Object System.Collections.Generic.List[int]
            $valid = $true
            if ($indices.Count -lt 2) {
                $valid = $false
            } else {
                $head = $indices[0]
                $neck = $indices[1]
                $hx = $head % $level.width
                $hy = [int][Math]::Floor($head / $level.width)
                $nx = $neck % $level.width
                $ny = [int][Math]::Floor($neck / $level.width)
                $dir = Dir-FromDelta ($hx - $nx) ($hy - $ny)
                if ($dir -lt 0) {
                    $valid = $false
                } else {
                    $seen = New-Object System.Collections.Generic.HashSet[int]
                    $x = $hx
                    $y = $hy
                    Step-Dir $dir ([ref]$x) ([ref]$y)
                    while ($x -ge 0 -and $x -lt $level.width -and $y -ge 0 -and $y -lt $level.height) {
                        $idx = $x + $y * $level.width
                        $blocker = $owner[$idx]
                        if ($blocker -eq $i) {
                            $valid = $false
                            break
                        }
                        if ($blocker -ge 0 -and $seen.Add($blocker)) {
                            $deps.Add($blocker)
                        }
                        Step-Dir $dir ([ref]$x) ([ref]$y)
                    }
                }
            }
            $perChain[$o] = [pscustomobject]@{ valid = $valid; deps = [int[]]@($deps) }
        }
        $options[$i] = $perChain
    }
    return $options
}

function Measure-GatePotential($level, $stats, $options) {
    $best = $null

    for ($entry = 0; $entry -lt 9; $entry++) {
        $entryRootOptions = 0
        $unavoidableOutside = 0
        $gateCandidates = 0
        $crossStageDeps = 0
        $farChains = 0
        $lateRegions = New-Object System.Collections.Generic.HashSet[int]

        for ($i = 0; $i -lt $stats.Length; $i++) {
            $stage = Get-RegionDistance $entry ([int]$stats[$i].region)
            if ($stage -ge 2) { $farChains++ }

            $valid = @($options[$i] | Where-Object { $_.valid })
            if ($valid.Count -eq 0) { continue }
            $blocked = @($valid | Where-Object { $_.deps.Count -gt 0 })
            $roots = @($valid | Where-Object { $_.deps.Count -eq 0 })
            if ($blocked.Count -eq 0) {
                if ($stage -gt 0) { $unavoidableOutside++ } else { $entryRootOptions++ }
            } elseif ($stage -eq 0 -and $roots.Count -gt 0) {
                $entryRootOptions++
            }

            $hasLowerStageGate = $false
            foreach ($opt in $valid) {
                if ($opt.deps.Count -le 0) { continue }
                $lowerDeps = 0
                $laterDeps = 0
                foreach ($dep in $opt.deps) {
                    if ($dep -lt 0 -or $dep -ge $stats.Length) { continue }
                    $depStage = Get-RegionDistance $entry ([int]$stats[$dep].region)
                    if ($depStage -lt $stage) { $lowerDeps++; $crossStageDeps++ }
                    elseif ($depStage -gt $stage) { $laterDeps++ }
                }
                if ($stage -gt 0 -and $lowerDeps -gt 0 -and $laterDeps -eq 0) {
                    $hasLowerStageGate = $true
                }
            }
            if ($hasLowerStageGate) {
                $gateCandidates++
                if ($stage -ge 2) { [void]$lateRegions.Add([int]$stats[$i].region) }
            }
        }

        $score =
            $lateRegions.Count * 34.0 +
            [Math]::Min(60.0, $gateCandidates * 3.0) +
            [Math]::Min(45.0, $crossStageDeps * 1.1) +
            $farChains * 0.6 +
            [Math]::Min(24.0, $entryRootOptions * 4.0) -
            $unavoidableOutside * 38.0 -
            [Math]::Abs($entryRootOptions - 4) * 6.0

        if ($null -eq $best -or $score -gt $best.gatePotentialScore) {
            $best = [pscustomobject]@{
                gatePotentialScore = [Math]::Round($score, 3)
                gateEntryRegion = $entry
                gateEntryRootOptions = $entryRootOptions
                gateUnavoidableOutsideRoots = $unavoidableOutside
                gateCandidateCount = $gateCandidates
                gateLateRegionCount = $lateRegions.Count
                gateCrossStageDepCount = $crossStageDeps
            }
        }
    }

    if ($null -eq $best) {
        return [pscustomobject]@{
            gatePotentialScore = 0
            gateEntryRegion = 0
            gateEntryRootOptions = 0
            gateUnavoidableOutsideRoots = 0
            gateCandidateCount = 0
            gateLateRegionCount = 0
            gateCrossStageDepCount = 0
        }
    }
    return $best
}

function Measure-DependencyStyle($level, $stats, $options) {
    $validOrientations = 0
    $rootOrientations = 0
    $blockedOrientations = 0
    $depRefs = 0
    $sameRegion = 0
    $far = 0
    $pairCounts = @{}

    for ($i = 0; $i -lt $options.Length; $i++) {
        foreach ($opt in $options[$i]) {
            if (-not $opt.valid) { continue }
            $validOrientations++
            if ($opt.deps.Count -eq 0) {
                $rootOrientations++
                continue
            }
            $blockedOrientations++
            foreach ($dep in $opt.deps) {
                if ($dep -lt 0 -or $dep -ge $stats.Length) { continue }
                $depRefs++
                $a = [int]$stats[$i].region
                $b = [int]$stats[$dep].region
                if ($a -eq $b) { $sameRegion++ }
                if ((Get-RegionDistance $a $b) -ge 2) { $far++ }
                $key = "$a>$b"
                if (-not $pairCounts.ContainsKey($key)) { $pairCounts[$key] = 0 }
                $pairCounts[$key]++
            }
        }
    }

    $pairEntropy = if ($pairCounts.Count -gt 0) { Entropy01 @($pairCounts.Values) } else { 0.0 }
    return [pscustomobject]@{
        dependencyValidOrientations = $validOrientations
        dependencyRootOrientationRate = [Math]::Round($rootOrientations / [double][Math]::Max(1, $validOrientations), 4)
        dependencyBlockedOrientationRate = [Math]::Round($blockedOrientations / [double][Math]::Max(1, $validOrientations), 4)
        dependencyAvgDepsPerBlocked = [Math]::Round($depRefs / [double][Math]::Max(1, $blockedOrientations), 4)
        dependencySameRegionRate = [Math]::Round($sameRegion / [double][Math]::Max(1, $depRefs), 4)
        dependencyFarRate = [Math]::Round($far / [double][Math]::Max(1, $depRefs), 4)
        dependencyRegionPairEntropy = $pairEntropy
    }
}

function Analyze-Level($level) {
    $stats = Get-ChainStatsForGate $level
    if ($FastStaticOnly -or -not $DeepDependencyScan) {
        $gate = [pscustomobject]@{
            gatePotentialScore = 0
            gateEntryRegion = 0
            gateEntryRootOptions = 0
            gateUnavoidableOutsideRoots = 0
            gateCandidateCount = 0
            gateLateRegionCount = 0
            gateCrossStageDepCount = 0
        }
        $deps = [pscustomobject]@{
            dependencyValidOrientations = 0
            dependencyRootOrientationRate = 0.0
            dependencyBlockedOrientationRate = 0.0
            dependencyAvgDepsPerBlocked = 0.0
            dependencySameRegionRate = 0.0
            dependencyFarRate = 0.0
            dependencyRegionPairEntropy = 0.0
        }
    } else {
        $options = Get-DependencyOptionsForGate $level
        $gate = Measure-GatePotential $level $stats $options
        $deps = Measure-DependencyStyle $level $stats $options
    }

    $lengths = New-Object System.Collections.Generic.List[int]
    $turns = New-Object System.Collections.Generic.List[int]
    $straightnesses = New-Object System.Collections.Generic.List[double]
    $cells = New-Object System.Collections.Generic.HashSet[int]
    $regionCounts = @(0,0,0,0,0,0,0,0,0)
    $headDirCounts = @(0,0,0,0)
    $headSideCounts = @{ L = 0; R = 0; B = 0; T = 0; I = 0 }
    $longChains = 0
    $veryLongChains = 0
    $shortChains = 0
    $midChains = 0
    $structureCarriers = 0
    $straightLike = 0
    $outerLongStraight = 0
    $outerEndpoints = 0
    $outerOutwardHeads = 0
    $endpointCount = 0
    $outerBand = 1

    for ($i = 0; $i -lt $level.arrows.Count; $i++) {
        $arrow = $level.arrows[$i]
        $indices = @($arrow.indices)
        $len = $indices.Count
        if ($len -le 0) { continue }
        $lengths.Add($len)
        if ($len -ge 10) { $longChains++ }
        if ($len -ge 16) { $veryLongChains++ }
        if ($len -le 3) { $shortChains++ }
        if ($len -ge 4 -and $len -le 8) { $midChains++ }

        $sumX = 0.0
        $sumY = 0.0
        $outerCells = 0
        foreach ($idx in $indices) {
            [void]$cells.Add([int]$idx)
            $x = $idx % $level.width
            $y = [int][Math]::Floor($idx / $level.width)
            $sumX += $x
            $sumY += $y
            if ((Get-OuterSide $x $y $level.width $level.height $outerBand) -ne "I") { $outerCells++ }
        }
        $cx = $sumX / [Math]::Max(1, $len)
        $cy = $sumY / [Math]::Max(1, $len)
        $region = Get-RegionIndex $cx $cy $level.width $level.height
        $regionCounts[$region]++

        $turnCount = 0
        $edgeCount = 0
        $prevDir = -1
        $straightRun = 0
        $maxStraightRun = 0
        for ($j = 0; $j -lt $indices.Count - 1; $j++) {
            $a = $indices[$j]
            $b = $indices[$j + 1]
            $dir = Dir-FromDelta (($b % $level.width) - ($a % $level.width)) ([int][Math]::Floor($b / $level.width) - [int][Math]::Floor($a / $level.width))
            if ($dir -lt 0) { continue }
            $edgeCount++
            if ($prevDir -ge 0 -and $dir -ne $prevDir) {
                $turnCount++
                $straightRun = 1
            } else {
                $straightRun++
            }
            $maxStraightRun = [Math]::Max($maxStraightRun, $straightRun)
            $prevDir = $dir
        }
        $turns.Add($turnCount)
        $straightness = $maxStraightRun / [double][Math]::Max(1, $edgeCount)
        $straightnesses.Add($straightness)
        $outerCellRate = $outerCells / [double][Math]::Max(1, $len)
        $isStraightLike = ($len -ge 6 -and $straightness -ge 0.78 -and $turnCount -le 1)
        if ($isStraightLike) { $straightLike++ }
        if ($len -ge 8 -and $isStraightLike -and $outerCellRate -ge 0.45) { $outerLongStraight++ }
        if ($len -ge 7 -and $turnCount -ge 2 -and $straightness -le 0.82) { $structureCarriers++ }

        if ($indices.Count -ge 2) {
            $endpointData = @(
                [pscustomobject]@{ index = $indices[0]; dir = $stats[$i].headDir0 },
                [pscustomobject]@{ index = $indices[$indices.Count - 1]; dir = $stats[$i].headDir1 }
            )
            foreach ($ep in $endpointData) {
                if ($ep.dir -ge 0 -and $ep.dir -lt 4) { $headDirCounts[$ep.dir]++ }
                $x = $ep.index % $level.width
                $y = [int][Math]::Floor($ep.index / $level.width)
                $side = Get-OuterSide $x $y $level.width $level.height $outerBand
                $headSideCounts[$side]++
                $endpointCount++
                if ($side -ne "I") {
                    $outerEndpoints++
                    if (Test-OutwardHead $side $ep.dir) { $outerOutwardHeads++ }
                }
            }
        }
    }

    $chainCount = [Math]::Max(1, $level.arrows.Count)
    $avgLen = if ($lengths.Count -gt 0) { (($lengths | Measure-Object -Average).Average) } else { 0.0 }
    $avgTurns = if ($turns.Count -gt 0) { (($turns | Measure-Object -Average).Average) } else { 0.0 }
    $avgStraight = if ($straightnesses.Count -gt 0) { (($straightnesses | Measure-Object -Average).Average) } else { 0.0 }
    $coverage = $cells.Count / [double][Math]::Max(1, $level.width * $level.height)
    $sortedLengths = @($lengths | Sort-Object -Descending)
    $top1Cells = if ($sortedLengths.Count -ge 1) { [int]$sortedLengths[0] } else { 0 }
    $top3Cells = if ($sortedLengths.Count -gt 0) { [int](($sortedLengths | Select-Object -First 3 | Measure-Object -Sum).Sum) } else { 0 }
    $top5Cells = if ($sortedLengths.Count -gt 0) { [int](($sortedLengths | Select-Object -First 5 | Measure-Object -Sum).Sum) } else { 0 }
    $veryLongCells = if ($lengths.Count -gt 0) { [int](($lengths | Where-Object { $_ -ge 16 } | Measure-Object -Sum).Sum) } else { 0 }
    $spineLongCells = if ($lengths.Count -gt 0) { [int](($lengths | Where-Object { $_ -ge 24 } | Measure-Object -Sum).Sum) } else { 0 }
    $occupiedCellCount = [Math]::Max(1, $cells.Count)
    $p80ChainValue = [int](Percentile $lengths 0.8)
    $p90ChainValue = [int](Percentile $lengths 0.9)
    $maxChain = if ($lengths.Count -gt 0) { [int](($lengths | Measure-Object -Maximum).Maximum) } else { 0 }
    $top1ChainCellShare = $top1Cells / [double]$occupiedCellCount
    $top3ChainCellShare = $top3Cells / [double]$occupiedCellCount
    $top5ChainCellShare = $top5Cells / [double]$occupiedCellCount
    $veryLongCellRate = $veryLongCells / [double]$occupiedCellCount
    $spineLongCellRate = $spineLongCells / [double]$occupiedCellCount
    $spineConcentrationScore =
        [Math]::Min(0.34, $maxChain / 90.0) +
        [Math]::Min(0.28, $top1ChainCellShare * 2.8) +
        [Math]::Min(0.24, $top3ChainCellShare * 1.2) +
        [Math]::Min(0.14, $spineLongCellRate * 0.8)
    $longChainContinuityScore =
        [Math]::Min(0.32, $p90ChainValue / 72.0) +
        [Math]::Min(0.24, $maxChain / 180.0) +
        [Math]::Min(0.24, $veryLongCellRate * 0.55) +
        [Math]::Min(0.20, $top3ChainCellShare * 0.9)
    $longRate = $longChains / [double]$chainCount
    $veryLongRate = $veryLongChains / [double]$chainCount
    $shortRate = $shortChains / [double]$chainCount
    $midRate = $midChains / [double]$chainCount
    $carrierRate = $structureCarriers / [double]$chainCount
    $straightRate = $straightLike / [double]$chainCount
    $outerLongRate = $outerLongStraight / [double]$chainCount
    $regionEntropy = Entropy01 $regionCounts
    $regionDominance = (($regionCounts | Measure-Object -Maximum).Maximum) / [double]$chainCount
    $headEntropy = Entropy01 $headDirCounts
    $dominantHeadDirCount = ($headDirCounts | Measure-Object -Maximum).Maximum
    $dominantHeadDirRate = $dominantHeadDirCount / [double][Math]::Max(1, ($headDirCounts | Measure-Object -Sum).Sum)
    $dominantHeadDir = [Array]::IndexOf($headDirCounts, $dominantHeadDirCount)
    $outerEndpointRate = $outerEndpoints / [double][Math]::Max(1, $endpointCount)
    $outerOutwardHeadRate = $outerOutwardHeads / [double][Math]::Max(1, $endpointCount)

    $complexScore =
        ([Math]::Min(1.0, $longRate) * 32.0) +
        ([Math]::Min(1.0, $carrierRate) * 34.0) +
        ([Math]::Min(1.0, $avgTurns / 3.0) * 18.0) +
        ([Math]::Min(1.0, $regionEntropy) * 10.0) -
        ([Math]::Min(1.0, $straightRate) * 12.0) -
        ([Math]::Min(1.0, $shortRate) * 10.0)

    $hardFitScore =
        [Math]::Min(22.0, $avgLen * 1.8) +
        [Math]::Min(18.0, $longRate * 42.0) +
        [Math]::Min(18.0, $carrierRate * 38.0) +
        [Math]::Min(10.0, $veryLongRate * 30.0) +
        [Math]::Min(10.0, $coverage * 10.0) +
        [Math]::Min(10.0, $regionEntropy * 10.0) -
        [Math]::Max(0.0, $shortRate - 0.28) * 24.0 -
        [Math]::Max(0.0, $straightRate - 0.46) * 18.0 -
        [Math]::Max(0.0, $outerLongRate - 0.18) * 16.0 -
        [Math]::Max(0.0, $maxChain - 90.0) * 0.55

    return [pscustomobject]@{
        width = $level.width
        height = $level.height
        aspect = [Math]::Round($level.width / [double][Math]::Max(1, $level.height), 4)
        area = $level.width * $level.height
        chains = $level.arrows.Count
        arrowCells = $cells.Count
        coverage = [Math]::Round($coverage, 4)
        avgChain = [Math]::Round($avgLen, 4)
        p50Chain = [int](Percentile $lengths 0.5)
        p80Chain = $p80ChainValue
        p90Chain = $p90ChainValue
        maxChain = $maxChain
        longChainRate = [Math]::Round($longRate, 4)
        veryLongChainRate = [Math]::Round($veryLongRate, 4)
        top1ChainCellShare = [Math]::Round($top1ChainCellShare, 4)
        top3ChainCellShare = [Math]::Round($top3ChainCellShare, 4)
        top5ChainCellShare = [Math]::Round($top5ChainCellShare, 4)
        veryLongCellRate = [Math]::Round($veryLongCellRate, 4)
        spineLongCellRate = [Math]::Round($spineLongCellRate, 4)
        spineConcentrationScore = [Math]::Round($spineConcentrationScore, 4)
        longChainContinuityScore = [Math]::Round($longChainContinuityScore, 4)
        shortChainRate = [Math]::Round($shortRate, 4)
        midChainRate = [Math]::Round($midRate, 4)
        avgTurns = [Math]::Round($avgTurns, 4)
        p80Turns = [int](Percentile $turns 0.8)
        avgStraightness = [Math]::Round($avgStraight, 4)
        turnDensity = [Math]::Round($avgTurns / [double][Math]::Max(1.0, $avgLen - 1.0), 4)
        structureCarrierRate = [Math]::Round($carrierRate, 4)
        straightLikeRate = [Math]::Round($straightRate, 4)
        outerLongStraightRate = [Math]::Round($outerLongRate, 4)
        regionEntropy = $regionEntropy
        regionDominance = [Math]::Round($regionDominance, 4)
        occupiedRegions = @($regionCounts | Where-Object { $_ -gt 0 }).Count
        regionHistogram = Histogram-String $regionCounts
        headDirectionEntropy = $headEntropy
        dominantHeadDir = $dominantHeadDir
        dominantHeadDirRate = [Math]::Round($dominantHeadDirRate, 4)
        headDirHistogram = Histogram-String $headDirCounts
        outerEndpointRate = [Math]::Round($outerEndpointRate, 4)
        outerOutwardHeadRate = [Math]::Round($outerOutwardHeadRate, 4)
        complexChainScore = [Math]::Round($complexScore, 4)
        hardSourceFitScore = [Math]::Round($hardFitScore, 4)
        gatePotentialScore = $gate.gatePotentialScore
        gateEntryRegion = $gate.gateEntryRegion
        gateEntryRootOptions = $gate.gateEntryRootOptions
        gateUnavoidableOutsideRoots = $gate.gateUnavoidableOutsideRoots
        gateCandidateCount = $gate.gateCandidateCount
        gateLateRegionCount = $gate.gateLateRegionCount
        gateCrossStageDepCount = $gate.gateCrossStageDepCount
        dependencyValidOrientations = $deps.dependencyValidOrientations
        dependencyRootOrientationRate = $deps.dependencyRootOrientationRate
        dependencyBlockedOrientationRate = $deps.dependencyBlockedOrientationRate
        dependencyAvgDepsPerBlocked = $deps.dependencyAvgDepsPerBlocked
        dependencySameRegionRate = $deps.dependencySameRegionRate
        dependencyFarRate = $deps.dependencyFarRate
        dependencyRegionPairEntropy = $deps.dependencyRegionPairEntropy
    }
}

function Get-StyleLabels($m) {
    $p90 = ToDouble $m.p90Chain
    $maxChain = ToDouble $m.maxChain
    $avgChain = ToDouble $m.avgChain
    $longRate = ToDouble $m.longChainRate
    $veryLongRate = ToDouble $m.veryLongChainRate
    $veryLongCellRate = ToDouble $m.veryLongCellRate
    $top1Share = ToDouble $m.top1ChainCellShare
    $top3Share = ToDouble $m.top3ChainCellShare
    $spineLongCellRate = ToDouble $m.spineLongCellRate
    $spineConcentration = ToDouble $m.spineConcentrationScore
    $continuity = ToDouble $m.longChainContinuityScore
    $carrier = ToDouble $m.structureCarrierRate
    $turnDensity = ToDouble $m.turnDensity
    $avgTurns = ToDouble $m.avgTurns
    $straight = ToDouble $m.straightLikeRate
    $outerStraight = ToDouble $m.outerLongStraightRate
    $regionEntropy = ToDouble $m.regionEntropy
    $regionDominance = ToDouble $m.regionDominance
    $outerHead = ToDouble $m.outerOutwardHeadRate
    $dependencyBlocked = ToDouble $m.dependencyBlockedOrientationRate
    $dependencyFar = ToDouble $m.dependencyFarRate
    $gateScore = ToDouble $m.gatePotentialScore
    $gateLate = ToInt $m.gateLateRegionCount
    $hasLongBody = ($p90 -ge 18 -or $veryLongCellRate -ge 0.34 -or $longRate -ge 0.38 -or $avgChain -ge 10.5)
    $hasSpineAnchor = ($maxChain -ge 30 -or $top1Share -ge 0.060 -or $top3Share -ge 0.160 -or $spineLongCellRate -ge 0.090 -or $spineConcentration -ge 0.620)
    $isTrueLongSpine = ($hasLongBody -and $hasSpineAnchor -and $continuity -ge 0.54)
    $hasPatchworkBody = ($p90 -ge 15 -or $veryLongCellRate -ge 0.24 -or $longRate -ge 0.30 -or $avgChain -ge 8.5)
    $isDiffuseLongBody = ($maxChain -le 24 -or $top1Share -lt 0.055 -or $top3Share -lt 0.145 -or $spineConcentration -lt 0.580)
    $isMediumLongPatchwork = (-not $isTrueLongSpine -and $hasPatchworkBody -and $isDiffuseLongBody)

    $chainLanguage = "mixed_chain"
    if ($isTrueLongSpine -and $carrier -ge 0.32) {
        $chainLanguage = "long_spine_carrier"
    } elseif ($isTrueLongSpine) {
        $chainLanguage = "long_spine"
    } elseif ($isMediumLongPatchwork -and $carrier -ge 0.38) {
        $chainLanguage = "medium_long_patchwork_carrier"
    } elseif ($isMediumLongPatchwork) {
        $chainLanguage = "medium_long_patchwork"
    } elseif ($straight -ge 0.45 -or $outerStraight -ge 0.18) {
        $chainLanguage = "peel_sweep"
    } elseif ((ToDouble $m.shortChainRate) -ge 0.45 -and $avgChain -le 5.2) {
        $chainLanguage = "short_burst"
    } elseif ($carrier -ge 0.30) {
        $chainLanguage = "weave"
    }

    $topologyLanguage = "open_flow"
    if ((ToDouble $m.coverage) -lt 0.35 -or (ToInt $m.chains) -lt 12) {
        $topologyLanguage = "sparse_seed"
    } elseif ($gateScore -ge 90 -and $gateLate -ge 4) {
        $topologyLanguage = "lock_gate"
    } elseif ($dependencyBlocked -ge 0.55 -and $dependencyFar -ge 0.32) {
        $topologyLanguage = "dependency_lock_gate"
    } elseif ($isMediumLongPatchwork -and $carrier -ge 0.44 -and $regionEntropy -ge 0.84) {
        $topologyLanguage = "fragmented_lock_like"
    } elseif ($isMediumLongPatchwork) {
        $topologyLanguage = "fragmented_fill"
    } elseif ($isTrueLongSpine -and $turnDensity -ge 0.20 -and $regionEntropy -ge 0.82) {
        $topologyLanguage = "maze_corridor"
    } elseif ($isTrueLongSpine -and $carrier -ge 0.44 -and $outerHead -le 0.20 -and $regionEntropy -ge 0.78) {
        $topologyLanguage = "lock_like_spine"
    } elseif ($carrier -ge 0.34 -and $regionEntropy -ge 0.80) {
        $topologyLanguage = "dense_weave"
    } elseif ($outerHead -ge 0.25 -and ($straight -ge 0.32 -or $outerStraight -ge 0.12)) {
        $topologyLanguage = "outer_peel"
    } elseif ($regionEntropy -ge 0.82 -and (ToDouble $m.dominantHeadDirRate) -le 0.42) {
        $topologyLanguage = "open_multi_region"
    } elseif ($regionDominance -ge 0.48) {
        $topologyLanguage = "local_mass"
    }

    $flowLanguage = "mixed_flow"
    if ($gateScore -ge 90 -and $gateLate -ge 4) {
        $flowLanguage = "stage_gate_flow"
    } elseif ($dependencyBlocked -ge 0.55 -and $dependencyFar -ge 0.32) {
        $flowLanguage = "dependency_weave_flow"
    } elseif ($outerHead -ge 0.25 -and ($straight -ge 0.32 -or $outerStraight -ge 0.12)) {
        $flowLanguage = "outer_peel_flow"
    } elseif ($topologyLanguage -in @("fragmented_fill", "fragmented_lock_like")) {
        $flowLanguage = "fragmented_fill_flow"
    } elseif ($regionEntropy -ge 0.82 -and (ToDouble $m.dominantHeadDirRate) -le 0.42) {
        $flowLanguage = "multi_region_flow"
    } elseif ($regionDominance -ge 0.48 -or ((ToDouble $m.dependencyRootOrientationRate) -ge 0.48 -and $regionEntropy -le 0.62)) {
        $flowLanguage = "local_collapse_risk"
    }

    $styleCluster = "seed_mixed_body"
    if ($topologyLanguage -eq "sparse_seed") {
        $styleCluster = "seed_sparse_tutorial"
    } elseif ($topologyLanguage -in @("lock_gate", "dependency_lock_gate", "lock_like_spine")) {
        $styleCluster = "seed_long_lock"
    } elseif ($topologyLanguage -eq "maze_corridor") {
        $styleCluster = "seed_long_maze"
    } elseif ($topologyLanguage -eq "dense_weave") {
        $styleCluster = if ($isTrueLongSpine) { "seed_long_weave" } else { "seed_dense_weave" }
    } elseif ($topologyLanguage -eq "fragmented_lock_like") {
        $styleCluster = "seed_fragmented_lock_like"
    } elseif ($topologyLanguage -eq "fragmented_fill") {
        $styleCluster = "seed_medium_long_patchwork"
    } elseif ($flowLanguage -eq "stage_gate_flow") {
        $styleCluster = "seed_stage_gate"
    } elseif ($topologyLanguage -eq "outer_peel" -or $chainLanguage -eq "peel_sweep") {
        $styleCluster = "seed_outer_peel"
    } elseif ($chainLanguage -eq "short_burst") {
        $styleCluster = "seed_core_burst"
    } elseif ($flowLanguage -eq "dependency_weave_flow") {
        $styleCluster = "seed_lock_buckle"
    } elseif ($flowLanguage -eq "multi_region_flow") {
        $styleCluster = "seed_flow_spread"
    }

    $tags = New-Object System.Collections.Generic.List[string]
    if ($isTrueLongSpine) { $tags.Add("long_spine") }
    elseif ($isMediumLongPatchwork) { $tags.Add("medium_long_patchwork") }
    if ($spineConcentration -ge 0.62) { $tags.Add("spine_concentrated") }
    elseif ($isMediumLongPatchwork) { $tags.Add("spine_diffuse") }
    if ($continuity -ge 0.36) { $tags.Add("chain_continuity") }
    if ($veryLongCellRate -ge 0.20) { $tags.Add("very_long_mass") }
    if ($carrier -ge 0.25) { $tags.Add("carrier") }
    if ($straight -ge 0.42) { $tags.Add("straight_bias") }
    if ($outerStraight -ge 0.15) { $tags.Add("outer_straight") }
    if ($regionEntropy -ge 0.82) { $tags.Add("region_spread") }
    if ($dependencyFar -ge 0.32) { $tags.Add("far_dependency") }
    if ((ToDouble $m.dependencySameRegionRate) -ge 0.55) { $tags.Add("local_dependency") }
    if ($gateScore -ge 90) { $tags.Add("gate_potential") }
    if ($outerHead -ge 0.25) { $tags.Add("outer_heads") }
    if ((ToDouble $m.shortChainRate) -ge 0.45) { $tags.Add("short_burst") }
    $tags.Add($topologyLanguage)
    if ($tags.Count -eq 0) { $tags.Add("plain") }

    $riskTags = New-Object System.Collections.Generic.List[string]
    if ((ToDouble $m.straightLikeRate) -ge 0.52) { $riskTags.Add("same_direction_heavy") }
    if ((ToDouble $m.outerLongStraightRate) -ge 0.22) { $riskTags.Add("outer_sweep_heavy") }
    if ((ToDouble $m.regionDominance) -ge 0.55) { $riskTags.Add("single_region_dominant") }
    if ((ToDouble $m.dependencySameRegionRate) -ge 0.62) { $riskTags.Add("local_dependency_heavy") }
    if ((ToDouble $m.coverage) -lt 0.35) { $riskTags.Add("sparse_seed") }

    $riskScore =
        [Math]::Max(0.0, (ToDouble $m.straightLikeRate) - 0.38) * 35.0 +
        [Math]::Max(0.0, (ToDouble $m.outerLongStraightRate) - 0.10) * 42.0 +
        [Math]::Max(0.0, (ToDouble $m.regionDominance) - 0.42) * 35.0 +
        [Math]::Max(0.0, (ToDouble $m.dependencySameRegionRate) - 0.48) * 20.0 +
        [Math]::Max(0.0, 0.36 - (ToDouble $m.coverage)) * 20.0

    $riskBand = "clean"
    if ($riskScore -ge 16.0 -or $riskTags.Count -ge 2) { $riskBand = "high_risk" }
    elseif ($riskScore -ge 7.0 -or $riskTags.Count -ge 1) { $riskBand = "watch" }
    if ($riskTags.Count -eq 0) { $riskTags.Add("none") }

    $salience =
        [Math]::Min(26.0, $continuity * 58.0) +
        [Math]::Min(18.0, $veryLongCellRate * 52.0) +
        [Math]::Min(20.0, $carrier * 48.0) +
        [Math]::Min(14.0, $regionEntropy * 14.0) +
        [Math]::Min(14.0, $dependencyBlocked * 20.0) +
        [Math]::Min(14.0, $gateLate * 3.0) -
        [Math]::Min(18.0, $riskScore * 0.55)

    return [pscustomobject]@{
        styleCluster = $styleCluster
        chainLanguage = $chainLanguage
        topologyLanguage = $topologyLanguage
        flowLanguage = $flowLanguage
        styleTags = ($tags -join ';')
        seedRiskTags = ($riskTags -join ';')
        seedRiskBand = $riskBand
        styleSalienceScore = [Math]::Round($salience, 4)
    }
}

function Find-AssetPathFromRow($row) {
    $columns = @(
        "path",
        "assetPath",
        "AssetPath",
        "copiedAssetPath",
        "sourceV11CopiedAssetPath",
        "sourceV11AssetPath",
        "OriginalAssetPath",
        "ExportAsset",
        "unityAssetPath"
    )
    $firstCandidate = $null
    foreach ($column in $columns) {
        $value = Get-RowValue $row @($column)
        if ([string]::IsNullOrWhiteSpace($value)) { continue }
        $resolved = Resolve-ProjectPath $value
        if ($null -eq $firstCandidate) {
            $firstCandidate = [pscustomobject]@{ path = $resolved; sourceColumn = $column; exists = (Test-Path -LiteralPath $resolved) }
        }
        if (Test-Path -LiteralPath $resolved) {
            return [pscustomobject]@{ path = $resolved; sourceColumn = $column; exists = $true }
        }
    }
    if ($null -ne $firstCandidate) { return $firstCandidate }
    return [pscustomobject]@{ path = ""; sourceColumn = ""; exists = $false }
}

function Add-Candidate($list, $seen, [string]$assetPath, [string]$sourceKind, [string]$sourceName, $sourceRow) {
    if ([string]::IsNullOrWhiteSpace($assetPath)) { return }
    $full = Resolve-ProjectPath $assetPath
    $key = $full.ToLowerInvariant()
    if ($seen.ContainsKey($key)) { return }
    $seen[$key] = $true
    $levelId = Get-RowValue $sourceRow @("levelId", "LevelId", "id", "sourceId", "SourceId")
    $declaredType = Get-RowValue $sourceRow @("type", "Type", "category", "levelType", "styleHint", "families")
    $score = Get-RowValue $sourceRow @("score", "Score", "rank", "Rank", "difficulty", "relativeDifficulty")
    $order = Get-RowValue $sourceRow @("order", "Order", "rank", "Rank", "slot")
    $list.Add([pscustomobject]@{
        assetPath = $full
        projectPath = Convert-ToProjectRelative $full
        sourceKind = $sourceKind
        sourceName = $sourceName
        sourceOrder = $order
        sourceLevelId = $levelId
        declaredType = $declaredType
        sourceScore = $score
    })
}

function Get-StyleDistance($a, $b) {
    $dims = @(
        @{ n = "coverage"; w = 0.8; s = 0.35 },
        @{ n = "avgChain"; w = 0.6; s = 8.0 },
        @{ n = "p90Chain"; w = 1.5; s = 18.0 },
        @{ n = "maxChain"; w = 1.8; s = 32.0 },
        @{ n = "longChainRate"; w = 0.45; s = 0.35 },
        @{ n = "veryLongChainRate"; w = 1.0; s = 0.30 },
        @{ n = "top1ChainCellShare"; w = 1.1; s = 0.10 },
        @{ n = "top3ChainCellShare"; w = 1.45; s = 0.18 },
        @{ n = "veryLongCellRate"; w = 1.25; s = 0.28 },
        @{ n = "spineLongCellRate"; w = 1.0; s = 0.18 },
        @{ n = "spineConcentrationScore"; w = 1.7; s = 0.45 },
        @{ n = "longChainContinuityScore"; w = 1.6; s = 0.34 },
        @{ n = "shortChainRate"; w = 0.9; s = 0.45 },
        @{ n = "structureCarrierRate"; w = 1.1; s = 0.40 },
        @{ n = "straightLikeRate"; w = 1.0; s = 0.45 },
        @{ n = "outerLongStraightRate"; w = 0.9; s = 0.25 },
        @{ n = "regionEntropy"; w = 1.0; s = 0.75 },
        @{ n = "regionDominance"; w = 0.8; s = 0.45 },
        @{ n = "outerOutwardHeadRate"; w = 0.8; s = 0.35 },
        @{ n = "dependencyBlockedOrientationRate"; w = 0.8; s = 0.55 },
        @{ n = "dependencyFarRate"; w = 0.9; s = 0.45 },
        @{ n = "dependencySameRegionRate"; w = 0.7; s = 0.55 }
    )
    $sum = 0.0
    foreach ($d in $dims) {
        $av = ToDouble ($a.PSObject.Properties[$d.n].Value) 0.0
        $bv = ToDouble ($b.PSObject.Properties[$d.n].Value) 0.0
        $diff = ($av - $bv) / [double]$d.s
        $sum += [double]$d.w * $diff * $diff
    }
    return [Math]::Round([Math]::Sqrt($sum), 4)
}

$projectFull = [IO.Path]::GetFullPath($ProjectRoot)
if (-not (Test-Path -LiteralPath $projectFull)) { throw "Missing project root: $projectFull" }
$outDirFull = Resolve-ProjectPath $OutputDir
New-Item -ItemType Directory -Force -Path $outDirFull | Out-Null

$effectiveCsvPaths = New-Object System.Collections.Generic.List[string]
$effectiveRoots = New-Object System.Collections.Generic.List[string]
if (-not $NoDefaultSources) {
    foreach ($p in @(
        "Assets/ArrowMagic/SOData/Levels/Seeds"
    )) {
        $full = Resolve-ProjectPath $p
        if (Test-Path -LiteralPath $full) { $effectiveRoots.Add($full) }
    }
}
foreach ($p in $SeedCsvPaths) {
    $full = Resolve-ProjectPath $p
    if (Test-Path -LiteralPath $full) { $effectiveCsvPaths.Add($full) }
}
foreach ($p in $SeedAssetRoots) {
    $full = Resolve-ProjectPath $p
    if (Test-Path -LiteralPath $full) { $effectiveRoots.Add($full) }
}

$candidates = New-Object System.Collections.Generic.List[object]
$seen = @{}
$missing = New-Object System.Collections.Generic.List[object]

foreach ($root in ($effectiveRoots | Select-Object -Unique)) {
    $assets = @(Get-ChildItem -LiteralPath $root -Recurse -Filter *.asset -File | Sort-Object FullName)
    if ($MaxAssetsPerRoot -gt 0) { $assets = @($assets | Select-Object -First $MaxAssetsPerRoot) }
    foreach ($asset in $assets) {
        Add-Candidate $candidates $seen $asset.FullName "asset_root" (Convert-ToProjectRelative $root) $null
    }
}

foreach ($csvPath in ($effectiveCsvPaths | Select-Object -Unique)) {
    $rows = @(Import-Csv -LiteralPath $csvPath)
    if ($MaxRowsPerCsv -gt 0) { $rows = @($rows | Select-Object -First $MaxRowsPerCsv) }
    foreach ($row in $rows) {
        $asset = Find-AssetPathFromRow $row
        if (-not $asset.exists) {
            $missing.Add([pscustomobject]@{
                source = Convert-ToProjectRelative $csvPath
                sourceColumn = $asset.sourceColumn
                levelId = Get-RowValue $row @("levelId", "LevelId", "id")
                path = $asset.path
                reason = "missing asset"
            })
            continue
        }
        Add-Candidate $candidates $seen $asset.path "csv" (Convert-ToProjectRelative $csvPath) $row
    }
}

$profiles = New-Object System.Collections.Generic.List[object]
$i = 0
foreach ($candidate in $candidates) {
    $i++
    if ($i % 100 -eq 0) { Write-Host "Profiled $i / $($candidates.Count) seed candidates..." }
    $level = Parse-LevelAsset $candidate.assetPath
    if ($null -eq $level) {
        $missing.Add([pscustomobject]@{
            source = $candidate.sourceName
            sourceColumn = ""
            levelId = $candidate.sourceLevelId
            path = $candidate.projectPath
            reason = "parse failed or authoredLevel empty"
        })
        continue
    }
    $m = Analyze-Level $level
    $labels = Get-StyleLabels $m
    $profiles.Add([pscustomobject]@{
        profileKind = "project_seed"
        sourceKind = $candidate.sourceKind
        sourceName = $candidate.sourceName
        sourceOrder = $candidate.sourceOrder
        sourceLevelId = $candidate.sourceLevelId
        declaredType = $candidate.declaredType
        sourceScore = $candidate.sourceScore
        levelId = $level.levelId
        path = $candidate.projectPath
        styleCluster = $labels.styleCluster
        chainLanguage = $labels.chainLanguage
        topologyLanguage = $labels.topologyLanguage
        flowLanguage = $labels.flowLanguage
        styleTags = $labels.styleTags
        seedRiskTags = $labels.seedRiskTags
        seedRiskBand = $labels.seedRiskBand
        styleSalienceScore = $labels.styleSalienceScore
        width = $m.width
        height = $m.height
        aspect = $m.aspect
        area = $m.area
        chains = $m.chains
        arrowCells = $m.arrowCells
        coverage = $m.coverage
        avgChain = $m.avgChain
        p50Chain = $m.p50Chain
        p80Chain = $m.p80Chain
        p90Chain = $m.p90Chain
        maxChain = $m.maxChain
        longChainRate = $m.longChainRate
        veryLongChainRate = $m.veryLongChainRate
        top1ChainCellShare = $m.top1ChainCellShare
        top3ChainCellShare = $m.top3ChainCellShare
        top5ChainCellShare = $m.top5ChainCellShare
        veryLongCellRate = $m.veryLongCellRate
        spineLongCellRate = $m.spineLongCellRate
        spineConcentrationScore = $m.spineConcentrationScore
        longChainContinuityScore = $m.longChainContinuityScore
        shortChainRate = $m.shortChainRate
        midChainRate = $m.midChainRate
        avgTurns = $m.avgTurns
        p80Turns = $m.p80Turns
        avgStraightness = $m.avgStraightness
        turnDensity = $m.turnDensity
        structureCarrierRate = $m.structureCarrierRate
        straightLikeRate = $m.straightLikeRate
        outerLongStraightRate = $m.outerLongStraightRate
        regionEntropy = $m.regionEntropy
        regionDominance = $m.regionDominance
        occupiedRegions = $m.occupiedRegions
        regionHistogram = $m.regionHistogram
        headDirectionEntropy = $m.headDirectionEntropy
        dominantHeadDir = $m.dominantHeadDir
        dominantHeadDirRate = $m.dominantHeadDirRate
        headDirHistogram = $m.headDirHistogram
        outerEndpointRate = $m.outerEndpointRate
        outerOutwardHeadRate = $m.outerOutwardHeadRate
        complexChainScore = $m.complexChainScore
        hardSourceFitScore = $m.hardSourceFitScore
        gatePotentialScore = $m.gatePotentialScore
        gateEntryRegion = $m.gateEntryRegion
        gateEntryRootOptions = $m.gateEntryRootOptions
        gateUnavoidableOutsideRoots = $m.gateUnavoidableOutsideRoots
        gateCandidateCount = $m.gateCandidateCount
        gateLateRegionCount = $m.gateLateRegionCount
        gateCrossStageDepCount = $m.gateCrossStageDepCount
        dependencyValidOrientations = $m.dependencyValidOrientations
        dependencyRootOrientationRate = $m.dependencyRootOrientationRate
        dependencyBlockedOrientationRate = $m.dependencyBlockedOrientationRate
        dependencyAvgDepsPerBlocked = $m.dependencyAvgDepsPerBlocked
        dependencySameRegionRate = $m.dependencySameRegionRate
        dependencyFarRate = $m.dependencyFarRate
        dependencyRegionPairEntropy = $m.dependencyRegionPairEntropy
    })
}

$clusters = New-Object System.Collections.Generic.List[object]
foreach ($group in ($profiles | Group-Object styleCluster | Sort-Object Count -Descending)) {
    $rows = @($group.Group)
    $top = @($rows | Sort-Object @{Expression = "styleSalienceScore"; Descending = $true}, @{Expression = "complexChainScore"; Descending = $true} | Select-Object -First 1)
    $chainMix = (($rows | Group-Object chainLanguage | Sort-Object Count -Descending | ForEach-Object { "$($_.Name):$($_.Count)" }) -join ';')
    $topologyMix = (($rows | Group-Object topologyLanguage | Sort-Object Count -Descending | ForEach-Object { "$($_.Name):$($_.Count)" }) -join ';')
    $flowMix = (($rows | Group-Object flowLanguage | Sort-Object Count -Descending | ForEach-Object { "$($_.Name):$($_.Count)" }) -join ';')
    $riskMix = (($rows | Group-Object seedRiskBand | Sort-Object Count -Descending | ForEach-Object { "$($_.Name):$($_.Count)" }) -join ';')
    $clusters.Add([pscustomobject]@{
        styleCluster = $group.Name
        count = $rows.Count
        share = [Math]::Round($rows.Count / [double][Math]::Max(1, $profiles.Count), 4)
        chainMix = $chainMix
        topologyMix = $topologyMix
        flowMix = $flowMix
        riskMix = $riskMix
        exemplarLevelId = $top[0].levelId
        exemplarPath = $top[0].path
        exemplarSalience = $top[0].styleSalienceScore
        width = Average-Field $rows "width"
        height = Average-Field $rows "height"
        aspect = Average-Field $rows "aspect"
        chains = Average-Field $rows "chains"
        coverage = Average-Field $rows "coverage"
        avgChain = Average-Field $rows "avgChain"
        p80Chain = Average-Field $rows "p80Chain"
        p90Chain = Average-Field $rows "p90Chain"
        maxChain = Average-Field $rows "maxChain"
        longChainRate = Average-Field $rows "longChainRate"
        veryLongChainRate = Average-Field $rows "veryLongChainRate"
        top1ChainCellShare = Average-Field $rows "top1ChainCellShare"
        top3ChainCellShare = Average-Field $rows "top3ChainCellShare"
        top5ChainCellShare = Average-Field $rows "top5ChainCellShare"
        veryLongCellRate = Average-Field $rows "veryLongCellRate"
        spineLongCellRate = Average-Field $rows "spineLongCellRate"
        spineConcentrationScore = Average-Field $rows "spineConcentrationScore"
        longChainContinuityScore = Average-Field $rows "longChainContinuityScore"
        shortChainRate = Average-Field $rows "shortChainRate"
        structureCarrierRate = Average-Field $rows "structureCarrierRate"
        straightLikeRate = Average-Field $rows "straightLikeRate"
        outerLongStraightRate = Average-Field $rows "outerLongStraightRate"
        regionEntropy = Average-Field $rows "regionEntropy"
        regionDominance = Average-Field $rows "regionDominance"
        outerOutwardHeadRate = Average-Field $rows "outerOutwardHeadRate"
        dependencyRootOrientationRate = Average-Field $rows "dependencyRootOrientationRate"
        dependencyBlockedOrientationRate = Average-Field $rows "dependencyBlockedOrientationRate"
        dependencySameRegionRate = Average-Field $rows "dependencySameRegionRate"
        dependencyFarRate = Average-Field $rows "dependencyFarRate"
        gatePotentialScore = Average-Field $rows "gatePotentialScore"
        gateLateRegionCount = Average-Field $rows "gateLateRegionCount"
    })
}

$psgMatches = New-Object System.Collections.Generic.List[object]
$psgKeepFull = Resolve-ProjectPath $PsgKeepCsv
if (Test-Path -LiteralPath $psgKeepFull) {
    $psgRows = @(Import-Csv -LiteralPath $psgKeepFull)
    foreach ($row in $psgRows) {
        $asset = Find-AssetPathFromRow $row
        if (-not $asset.exists) {
            $missing.Add([pscustomobject]@{
                source = Convert-ToProjectRelative $psgKeepFull
                sourceColumn = $asset.sourceColumn
                levelId = Get-RowValue $row @("levelId", "LevelId", "id")
                path = $asset.path
                reason = "missing PSG keep asset"
            })
            continue
        }
        $level = Parse-LevelAsset $asset.path
        if ($null -eq $level) { continue }
        $m = Analyze-Level $level
        $labels = Get-StyleLabels $m
        $nearest = @($clusters | ForEach-Object {
            [pscustomobject]@{
                cluster = $_
                distance = Get-StyleDistance $m $_
            }
        } | Sort-Object distance | Select-Object -First 3)
        $best = $nearest[0]
        $second = if ($nearest.Count -gt 1) { $nearest[1] } else { $null }
        $third = if ($nearest.Count -gt 2) { $nearest[2] } else { $null }
        $psgMatches.Add([pscustomobject]@{
            psgLevelId = $level.levelId
            psgRankClass = Get-RowValue $row @("rankClass")
            psgExistingStyleFamily = Get-RowValue $row @("styleFamily")
            psgExistingChainLanguage = Get-RowValue $row @("chainLanguage")
            psgExistingFlowLanguage = Get-RowValue $row @("flowLanguage")
            psgStaticStyleCluster = $labels.styleCluster
            psgStaticChainLanguage = $labels.chainLanguage
            psgStaticTopologyLanguage = $labels.topologyLanguage
            psgStaticFlowLanguage = $labels.flowLanguage
            nearestSeedCluster = $best.cluster.styleCluster
            nearestSeedDistance = $best.distance
            secondSeedCluster = if ($null -ne $second) { $second.cluster.styleCluster } else { "" }
            secondSeedDistance = if ($null -ne $second) { $second.distance } else { "" }
            thirdSeedCluster = if ($null -ne $third) { $third.cluster.styleCluster } else { "" }
            thirdSeedDistance = if ($null -ne $third) { $third.distance } else { "" }
            styleGapBand = if ($best.distance -le 0.85) { "close" } elseif ($best.distance -le 1.35) { "near" } elseif ($best.distance -le 2.05) { "far" } else { "outlier" }
            path = Convert-ToProjectRelative $asset.path
            coverage = $m.coverage
            chains = $m.chains
            avgChain = $m.avgChain
            p80Chain = $m.p80Chain
            p90Chain = $m.p90Chain
            maxChain = $m.maxChain
            longChainRate = $m.longChainRate
            veryLongChainRate = $m.veryLongChainRate
            top1ChainCellShare = $m.top1ChainCellShare
            top3ChainCellShare = $m.top3ChainCellShare
            top5ChainCellShare = $m.top5ChainCellShare
            veryLongCellRate = $m.veryLongCellRate
            spineLongCellRate = $m.spineLongCellRate
            spineConcentrationScore = $m.spineConcentrationScore
            longChainContinuityScore = $m.longChainContinuityScore
            shortChainRate = $m.shortChainRate
            structureCarrierRate = $m.structureCarrierRate
            straightLikeRate = $m.straightLikeRate
            outerLongStraightRate = $m.outerLongStraightRate
            regionEntropy = $m.regionEntropy
            regionDominance = $m.regionDominance
            dependencySameRegionRate = $m.dependencySameRegionRate
            dependencyFarRate = $m.dependencyFarRate
        })
    }
}

$safePrefix = if ([string]::IsNullOrWhiteSpace($OutputPrefix)) { "project_seed_style_v1" } else { $OutputPrefix -replace '[^A-Za-z0-9_]+', '_' }
$profileCsv = Join-Path $outDirFull "${safePrefix}_profile.csv"
$clusterCsv = Join-Path $outDirFull "${safePrefix}_clusters.csv"
$matchCsv = Join-Path $outDirFull "${safePrefix}_psg_match.csv"
$missingCsv = Join-Path $outDirFull "${safePrefix}_missing.csv"
$summaryPath = Join-Path $outDirFull "${safePrefix}_summary.md"

$profiles | Sort-Object @{Expression = "styleCluster"; Descending = $false}, @{Expression = "styleSalienceScore"; Descending = $true} | Export-Csv -LiteralPath $profileCsv -NoTypeInformation -Encoding UTF8
$clusters | Sort-Object count -Descending | Export-Csv -LiteralPath $clusterCsv -NoTypeInformation -Encoding UTF8
$psgMatches | Sort-Object nearestSeedDistance | Export-Csv -LiteralPath $matchCsv -NoTypeInformation -Encoding UTF8
$missing | Export-Csv -LiteralPath $missingCsv -NoTypeInformation -Encoding UTF8

$summary = New-Object System.Collections.Generic.List[string]
$summary.Add("# Project Seed Style Profile")
$summary.Add("")
$summary.Add("Generated: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')")
$summary.Add("")
$summary.Add("## Inputs")
$summary.Add("- Project root: $projectFull")
$summary.Add("- Scan mode: $(if ($DeepDependencyScan -and -not $FastStaticOnly) { 'deep dependency/ray gate' } else { 'fast static seed style' })")
$summary.Add("- Seed asset roots: $((@($effectiveRoots | Select-Object -Unique | ForEach-Object { Convert-ToProjectRelative $_ }) -join '; '))")
$summary.Add("- Seed CSVs: $((@($effectiveCsvPaths | Select-Object -Unique | ForEach-Object { Convert-ToProjectRelative $_ }) -join '; '))")
$summary.Add("- PSG keep CSV: $(Convert-ToProjectRelative $psgKeepFull)")
$summary.Add("")
$summary.Add("## Outputs")
$summary.Add("- Profile CSV: $(Convert-ToProjectRelative $profileCsv)")
$summary.Add("- Cluster CSV: $(Convert-ToProjectRelative $clusterCsv)")
$summary.Add("- PSG match CSV: $(Convert-ToProjectRelative $matchCsv)")
$summary.Add("- Missing CSV: $(Convert-ToProjectRelative $missingCsv)")
$summary.Add("")
$summary.Add("## Totals")
$summary.Add("- Candidate paths: $($candidates.Count)")
$summary.Add("- Parsed profiles: $($profiles.Count)")
$summary.Add("- Missing/failed: $($missing.Count)")
$summary.Add("- Style clusters: $($clusters.Count)")
$summary.Add("- PSG matched rows: $($psgMatches.Count)")
$summary.Add("")
$summary.Add("## Seed Cluster Mix")
foreach ($c in ($clusters | Sort-Object count -Descending)) {
    $summary.Add("- $($c.styleCluster): count=$($c.count), share=$($c.share), avgCoverage=$($c.coverage), avgChain=$($c.avgChain), p90=$($c.p90Chain), max=$($c.maxChain), spine=$($c.spineConcentrationScore), continuity=$($c.longChainContinuityScore), veryLongCells=$($c.veryLongCellRate), carrier=$($c.structureCarrierRate), topology=[$($c.topologyMix)], risk=[$($c.riskMix)]")
}
$summary.Add("")
$summary.Add("## Top Exemplars")
foreach ($c in ($clusters | Sort-Object count -Descending)) {
    $summary.Add("- $($c.styleCluster): $($c.exemplarLevelId), salience=$($c.exemplarSalience), path=$($c.exemplarPath)")
}
if ($psgMatches.Count -gt 0) {
    $summary.Add("")
    $summary.Add("## PSG Keep -> Nearest Seed Style")
    foreach ($m in ($psgMatches | Sort-Object nearestSeedDistance)) {
        $summary.Add("- $($m.psgLevelId): nearest=$($m.nearestSeedCluster), distance=$($m.nearestSeedDistance), band=$($m.styleGapBand), PSG=$($m.psgExistingStyleFamily)/$($m.psgExistingFlowLanguage), static=$($m.psgStaticStyleCluster)/$($m.psgStaticTopologyLanguage)/$($m.psgStaticFlowLanguage), p90=$($m.p90Chain), max=$($m.maxChain), spine=$($m.spineConcentrationScore), continuity=$($m.longChainContinuityScore)")
    }
}
$summary | Set-Content -LiteralPath $summaryPath -Encoding UTF8

Write-Host "Project seed style profile complete."
Write-Host "Profiles: $($profiles.Count); clusters: $($clusters.Count); PSG matches: $($psgMatches.Count); missing: $($missing.Count)"
Write-Host "Profile CSV: $profileCsv"
Write-Host "Cluster CSV: $clusterCsv"
Write-Host "PSG match CSV: $matchCsv"
Write-Host "Summary: $summaryPath"

[pscustomobject]@{
    profileCsv = $profileCsv
    clusterCsv = $clusterCsv
    psgMatchCsv = $matchCsv
    missingCsv = $missingCsv
    summaryPath = $summaryPath
    profiles = $profiles.Count
    clusters = $clusters.Count
    psgMatches = $psgMatches.Count
    missing = $missing.Count
}
