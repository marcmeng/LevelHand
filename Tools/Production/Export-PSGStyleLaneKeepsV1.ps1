param(
    [string]$ProjectRoot = "",
    [string]$JoinedCsv = ".codex-run/sgp_pressure_batch4_faststs_final2_pack_20260627_trace_joined.csv",
    [string]$SourceCsv = "",
    [string]$TraceMetricsCsv = "",
    [string]$OutputDir = ".codex-run",
    [string]$OutputPrefix = "psg_style_lanes_v1",
    [ValidateSet("TraceOrderKeep", "VisualKeep", "ProcessKeep", "AllSolved", "All")]
    [string]$EligibleMode = "TraceOrderKeep",
    [string[]]$Lanes = @("patchwork_lock", "core_burst", "dense_weave", "flow_spread", "staged_unlock"),
    [int]$MaxRowsPerLane = 12,
    [int]$MinRowsPerLane = 0,
    [switch]$UniqueAcrossLanes,
    [switch]$ExcludeHighRisk,
    [switch]$StrictMinRows,
    [switch]$ForceJoin
)

$ErrorActionPreference = "Stop"

function Resolve-DefaultProjectRoot {
    $scriptRoot = if ([string]::IsNullOrWhiteSpace($PSScriptRoot)) { (Get-Location).Path } else { $PSScriptRoot }
    $candidate = [IO.Path]::GetFullPath((Join-Path $scriptRoot "..\.."))
    if (Test-Path -LiteralPath (Join-Path $candidate "Assets")) { return $candidate }
    return (Get-Location).Path
}

if ([string]::IsNullOrWhiteSpace($ProjectRoot)) {
    $ProjectRoot = Resolve-DefaultProjectRoot
}
$ProjectRoot = [IO.Path]::GetFullPath($ProjectRoot)

function Resolve-ProjectPath([string]$path) {
    if ([string]::IsNullOrWhiteSpace($path)) { return $path }
    if ([IO.Path]::IsPathRooted($path)) { return $path }
    return [IO.Path]::GetFullPath((Join-Path $ProjectRoot $path))
}

function Ensure-Directory([string]$path) {
    if ([string]::IsNullOrWhiteSpace($path)) { return }
    if (-not (Test-Path -LiteralPath $path)) {
        New-Item -ItemType Directory -Path $path -Force | Out-Null
    }
}

function To-Double([object]$value, [double]$fallback = 0.0) {
    if ($null -eq $value) { return $fallback }
    $text = ([string]$value).Trim()
    if ([string]::IsNullOrWhiteSpace($text)) { return $fallback }
    $number = 0.0
    if ([double]::TryParse($text, [Globalization.NumberStyles]::Float, [Globalization.CultureInfo]::InvariantCulture, [ref]$number)) {
        return $number
    }
    return $fallback
}

function To-Bool([object]$value) {
    $text = ([string]$value).Trim()
    return $text -ieq "true" -or $text -eq "1" -or $text -ieq "yes"
}

function Get-Value($row, [string[]]$names, [string]$fallback = "") {
    if ($null -eq $row) { return $fallback }
    foreach ($name in $names) {
        $prop = $row.PSObject.Properties | Where-Object { $_.Name -ieq $name } | Select-Object -First 1
        if ($null -ne $prop -and $null -ne $prop.Value -and -not [string]::IsNullOrWhiteSpace([string]$prop.Value)) {
            return [string]$prop.Value
        }
    }
    return $fallback
}

function Test-Eligible($row) {
    if ($ExcludeHighRisk -and (Get-Value $row @("styleRiskBand") "") -eq "high_risk") { return $false }

    switch ($EligibleMode) {
        "TraceOrderKeep" {
            return ((Get-Value $row @("rankClass") "") -eq "TraceOrderKeep") -or (To-Bool (Get-Value $row @("stsKeepCandidate") ""))
        }
        "VisualKeep" {
            return ((Get-Value $row @("rankClass") "") -in @("TraceOrderKeep", "VisualKeep")) -or
                (To-Bool (Get-Value $row @("stsKeepCandidate") "")) -or
                (To-Bool (Get-Value $row @("keepCandidate") ""))
        }
        "ProcessKeep" {
            return (To-Bool (Get-Value $row @("processKeep") "")) -or
                ((Get-Value $row @("rankClass") "") -in @("TraceOrderKeep", "VisualKeep", "ProcessKeep"))
        }
        "AllSolved" {
            return To-Bool (Get-Value $row @("solved") "")
        }
        default {
            return $true
        }
    }
}

function Get-LaneSpec([string]$laneName) {
    switch ($laneName) {
        "patchwork_lock" {
            return [pscustomobject]@{
                Name = "patchwork_lock"
                Description = "Lock-buckle / fragmented lock-like PSG candidates."
            }
        }
        "core_burst" {
            return [pscustomobject]@{
                Name = "core_burst"
                Description = "Core-burst candidates with short patch/core cluster language."
            }
        }
        "dense_weave" {
            return [pscustomobject]@{
                Name = "dense_weave"
                Description = "Dense-weave candidates with woven medium or dense grammar tags."
            }
        }
        "flow_spread" {
            return [pscustomobject]@{
                Name = "flow_spread"
                Description = "Flow-spread / region-alternating solve-order candidates."
            }
        }
        "staged_unlock" {
            return [pscustomobject]@{
                Name = "staged_unlock"
                Description = "Staged-unlock solve-order candidates."
            }
        }
        default {
            throw "Unknown lane '$laneName'. Supported lanes: patchwork_lock, core_burst, dense_weave, flow_spread, staged_unlock"
        }
    }
}

function Get-LaneMatch($row, [string]$laneName) {
    $style = (Get-Value $row @("styleFamily") "").ToLowerInvariant()
    $chain = (Get-Value $row @("chainLanguage") "").ToLowerInvariant()
    $chainTags = (Get-Value $row @("chainTags") "").ToLowerInvariant()
    $flow = (Get-Value $row @("flowLanguage") "").ToLowerInvariant()
    $flowTags = (Get-Value $row @("flowTags") "").ToLowerInvariant()
    $spec = ((Get-Value $row @("specId", "type") "") + " " + (Get-Value $row @("sourceStatus") "")).ToLowerInvariant()

    $reasons = New-Object System.Collections.Generic.List[string]
    switch ($laneName) {
        "patchwork_lock" {
            if ($style -eq "lock_buckle") { $reasons.Add("styleFamily=lock_buckle") | Out-Null }
            if ($chain -eq "lock_cluster") { $reasons.Add("chainLanguage=lock_cluster") | Out-Null }
            if ($spec -match "lock[_-]?buckle|core[_-]?lock|key[_-]?door") { $reasons.Add("specLock") | Out-Null }
        }
        "core_burst" {
            if ($style -eq "core_burst") { $reasons.Add("styleFamily=core_burst") | Out-Null }
            if ($chain -eq "core_cluster") { $reasons.Add("chainLanguage=core_cluster") | Out-Null }
            if ($spec -match "core[_-]?burst|vertical[_-]?gate") { $reasons.Add("specCoreBurst") | Out-Null }
        }
        "dense_weave" {
            if ($style -eq "dense_weave") { $reasons.Add("styleFamily=dense_weave") | Out-Null }
            if ($chainTags -match "dense_weave_chain") { $reasons.Add("chainTagsDense") | Out-Null }
            if ($spec -match "dense[_-]?weave|dense[_-]?kernel") { $reasons.Add("specDenseWeave") | Out-Null }
        }
        "flow_spread" {
            if ($flow -in @("flow_spread", "region_alternating_flow")) { $reasons.Add("flowLanguage=$flow") | Out-Null }
        }
        "staged_unlock" {
            if ($flow -eq "staged_unlock") { $reasons.Add("flowLanguage=staged_unlock") | Out-Null }
        }
    }

    return [pscustomobject]@{
        Matched = $reasons.Count -gt 0
        Reasons = ($reasons -join ";")
    }
}

function Get-LaneScore($row, [string]$laneName, [string]$laneReasons) {
    $score = To-Double (Get-Value $row @("psgRankScore") "0") 0.0
    $risk = Get-Value $row @("styleRiskBand") ""
    $flow = (Get-Value $row @("flowLanguage") "").ToLowerInvariant()
    $style = (Get-Value $row @("styleFamily") "").ToLowerInvariant()
    $chain = (Get-Value $row @("chainLanguage") "").ToLowerInvariant()
    $quality = To-Double (Get-Value $row @("solveTraceQualityScore") "0") 0.0
    $collapse = To-Double (Get-Value $row @("solveTraceCollapseRiskScore") "0") 0.0
    $regionEntropy = To-Double (Get-Value $row @("solveRegionEntropy") "0") 0.0
    $axisDrift = To-Double (Get-Value $row @("solveAxisDriftRate") "0") 0.0

    switch ($laneName) {
        "patchwork_lock" {
            if ($style -eq "lock_buckle") { $score += 0.45 }
            if ($chain -eq "lock_cluster") { $score += 0.25 }
            if ($flow -eq "staged_unlock") { $score += 0.12 }
        }
        "core_burst" {
            if ($style -eq "core_burst") { $score += 0.45 }
            if ($chain -eq "core_cluster") { $score += 0.24 }
            if ($flow -eq "local_collapse") { $score -= 0.10 }
        }
        "dense_weave" {
            if ($style -eq "dense_weave") { $score += 0.45 }
            if ($chain -eq "woven_medium") { $score += 0.24 }
            if ($regionEntropy -ge 0.98) { $score += 0.08 }
        }
        "flow_spread" {
            if ($flow -eq "flow_spread") { $score += 0.35 }
            if ($flow -eq "region_alternating_flow") { $score += 0.42 }
            $score += [Math]::Min(0.18, $axisDrift * 0.20)
            $score += [Math]::Min(0.16, $regionEntropy * 0.14)
        }
        "staged_unlock" {
            if ($flow -eq "staged_unlock") { $score += 0.45 }
            if ($style -eq "lock_buckle") { $score += 0.18 }
            if ($quality -ge 0.85 -and $collapse -le 0.18) { $score += 0.12 }
        }
    }

    if ($risk -eq "watch") { $score -= 0.08 }
    if ($risk -eq "high_risk") { $score -= 0.18 }
    if ([string]::IsNullOrWhiteSpace($laneReasons)) { $score -= 1.0 }
    return [Math]::Round($score, 4)
}

function Copy-RowWithLaneFields($row, [string]$laneName, [double]$laneScore, [string]$laneReasons, [int]$laneRank) {
    $ordered = [ordered]@{
        laneName = $laneName
        laneRank = $laneRank
        laneScore = $laneScore
        laneReasons = $laneReasons
    }
    foreach ($prop in $row.PSObject.Properties) {
        if (-not $ordered.Contains($prop.Name)) {
            $ordered[$prop.Name] = $prop.Value
        }
    }
    return [pscustomobject]$ordered
}

function Summarize-Mix([object[]]$InputRows, [string]$fieldName) {
    $items = @($InputRows)
    if ($items.Count -eq 0) { return "" }
    return (($items | Group-Object $fieldName | Sort-Object Count -Descending | ForEach-Object { "$($_.Name):$($_.Count)" }) -join ";")
}

if (-not (Test-Path -LiteralPath $ProjectRoot)) { throw "Missing project root: $ProjectRoot" }
$outputDirFull = Resolve-ProjectPath $OutputDir
Ensure-Directory $outputDirFull

$safePrefix = if ([string]::IsNullOrWhiteSpace($OutputPrefix)) { "psg_style_lanes_v1" } else { $OutputPrefix -replace '[^A-Za-z0-9_]+', '_' }
$joinedFull = Resolve-ProjectPath $JoinedCsv

if (-not [string]::IsNullOrWhiteSpace($SourceCsv) -and -not [string]::IsNullOrWhiteSpace($TraceMetricsCsv) -and ($ForceJoin -or -not (Test-Path -LiteralPath $joinedFull))) {
    $sourceFull = Resolve-ProjectPath $SourceCsv
    $traceFull = Resolve-ProjectPath $TraceMetricsCsv
    if (-not (Test-Path -LiteralPath $sourceFull)) { throw "Missing SourceCsv: $sourceFull" }
    if (-not (Test-Path -LiteralPath $traceFull)) { throw "Missing TraceMetricsCsv: $traceFull" }
    $joinedFull = Join-Path $outputDirFull "$safePrefix`_trace_joined.csv"
    $joinSummary = Join-Path $outputDirFull "$safePrefix`_trace_joined_summary.md"
    $joinScript = Join-Path $ProjectRoot "Tools\Production\Join-SGPPressureTraceMetrics.ps1"
    if (-not (Test-Path -LiteralPath $joinScript)) { throw "Missing join script: $joinScript" }
    & $joinScript -SourceCsv $sourceFull -TraceMetricsCsv $traceFull -OutputCsv $joinedFull -SummaryPath $joinSummary
}

if (-not (Test-Path -LiteralPath $joinedFull)) {
    throw "Missing JoinedCsv: $joinedFull. Provide -JoinedCsv or -SourceCsv/-TraceMetricsCsv with -ForceJoin."
}

$rows = @(Import-Csv -LiteralPath $joinedFull)
if ($rows.Count -eq 0) { throw "JoinedCsv has no rows: $joinedFull" }

$eligible = @($rows | Where-Object { Test-Eligible $_ })
$usedLevelIds = @{}
$laneOutputs = New-Object System.Collections.Generic.List[object]
$summaryLines = New-Object System.Collections.Generic.List[string]
$summaryLines.Add("# PSG Style Lane Keeps V1")
$summaryLines.Add("")
$summaryLines.Add("Generated: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')")
$summaryLines.Add("")
$summaryLines.Add("- Project root: $ProjectRoot")
$summaryLines.Add("- Joined CSV: $joinedFull")
$summaryLines.Add("- Output dir: $outputDirFull")
$summaryLines.Add("- Eligible mode: $EligibleMode")
$summaryLines.Add("- Unique across lanes: $([bool]$UniqueAcrossLanes)")
$summaryLines.Add("- Exclude high risk: $([bool]$ExcludeHighRisk)")
$summaryLines.Add("- Joined rows: $($rows.Count)")
$summaryLines.Add("- Eligible rows: $($eligible.Count)")
$summaryLines.Add("")
$summaryLines.Add("## Lane Outputs")

foreach ($laneName in $Lanes) {
    $spec = Get-LaneSpec $laneName
    $matched = New-Object System.Collections.Generic.List[object]
    foreach ($row in $eligible) {
        $levelId = Get-Value $row @("levelId", "LevelId") ""
        if ($UniqueAcrossLanes -and -not [string]::IsNullOrWhiteSpace($levelId) -and $usedLevelIds.ContainsKey($levelId)) { continue }
        $match = Get-LaneMatch $row $spec.Name
        if (-not $match.Matched) { continue }
        $matched.Add([pscustomobject]@{
            row = $row
            laneReasons = $match.Reasons
            laneScore = Get-LaneScore $row $spec.Name $match.Reasons
        }) | Out-Null
    }

    $selected = @($matched | Sort-Object @{ Expression = { -1.0 * [double]$_.laneScore } }, @{ Expression = { -1.0 * (To-Double (Get-Value $_.row @("psgRankScore") "0")) } }, @{ Expression = { To-Double (Get-Value $_.row @("maxChoices") "9999") } })
    if ($MaxRowsPerLane -gt 0) { $selected = @($selected | Select-Object -First $MaxRowsPerLane) }

    $outRows = New-Object System.Collections.Generic.List[object]
    $rank = 0
    foreach ($entry in $selected) {
        $rank++
        $levelId = Get-Value $entry.row @("levelId", "LevelId") ""
        if ($UniqueAcrossLanes -and -not [string]::IsNullOrWhiteSpace($levelId)) { $usedLevelIds[$levelId] = $true }
        $outRows.Add((Copy-RowWithLaneFields $entry.row $spec.Name $entry.laneScore $entry.laneReasons $rank)) | Out-Null
    }

    $laneCsv = Join-Path $outputDirFull ("{0}_lane_{1}_keep.csv" -f $safePrefix, $spec.Name)
    $outRows | Export-Csv -LiteralPath $laneCsv -NoTypeInformation -Encoding UTF8

    $status = if ($MinRowsPerLane -gt 0 -and $outRows.Count -lt $MinRowsPerLane) { "below_min" } else { "ok" }
    $laneOutputs.Add([pscustomobject]@{
        laneName = $spec.Name
        description = $spec.Description
        outputCsv = $laneCsv
        matchedRows = $matched.Count
        selectedRows = $outRows.Count
        minRows = $MinRowsPerLane
        status = $status
        styleMix = Summarize-Mix -InputRows @($outRows.ToArray()) -fieldName "styleFamily"
        chainMix = Summarize-Mix -InputRows @($outRows.ToArray()) -fieldName "chainLanguage"
        flowMix = Summarize-Mix -InputRows @($outRows.ToArray()) -fieldName "flowLanguage"
        riskMix = Summarize-Mix -InputRows @($outRows.ToArray()) -fieldName "styleRiskBand"
    }) | Out-Null

    $summaryLines.Add("- $($spec.Name): selected=$($outRows.Count), matched=$($matched.Count), status=$status, csv=$laneCsv")
    if ($outRows.Count -gt 0) {
        $summaryLines.Add("  - style=$((Summarize-Mix -InputRows @($outRows.ToArray()) -fieldName 'styleFamily'))")
        $summaryLines.Add("  - chain=$((Summarize-Mix -InputRows @($outRows.ToArray()) -fieldName 'chainLanguage'))")
        $summaryLines.Add("  - flow=$((Summarize-Mix -InputRows @($outRows.ToArray()) -fieldName 'flowLanguage'))")
        $summaryLines.Add("  - risk=$((Summarize-Mix -InputRows @($outRows.ToArray()) -fieldName 'styleRiskBand'))")
    }
}

$indexCsv = Join-Path $outputDirFull "$safePrefix`_lane_index.csv"
$summaryPath = Join-Path $outputDirFull "$safePrefix`_lane_summary.md"
$laneOutputs | Export-Csv -LiteralPath $indexCsv -NoTypeInformation -Encoding UTF8

$summaryLines.Add("")
$summaryLines.Add("## Stable Lane Contract")
$summaryLines.Add("- patchwork_lock: lock_buckle / lock_cluster candidates.")
$summaryLines.Add("- core_burst: core_burst / core_cluster candidates.")
$summaryLines.Add("- dense_weave: dense_weave family / dense_weave_chain candidates.")
$summaryLines.Add("- flow_spread: flow_spread and region_alternating_flow candidates.")
$summaryLines.Add("- staged_unlock: staged_unlock candidates.")
$summaryLines.Add("")
$summaryLines.Add("## Notes")
$summaryLines.Add("- Lane CSVs are independent by default; a level can appear in more than one lane when style and flow overlap.")
$summaryLines.Add("- Use -UniqueAcrossLanes when building a mixed review pack that must avoid duplicates across lanes.")
$summaryLines.Add("- This script does not modify canonical PSG keep CSV or Unity packs.")
$summaryLines | Set-Content -LiteralPath $summaryPath -Encoding UTF8

$belowMin = @($laneOutputs | Where-Object { $_.status -eq "below_min" })
if ($StrictMinRows -and $belowMin.Count -gt 0) {
    throw "Some lanes are below MinRowsPerLane=${MinRowsPerLane}: $((@($belowMin | ForEach-Object { $_.laneName }) -join ', '))"
}

[pscustomobject]@{
    projectRoot = $ProjectRoot
    joinedCsv = $joinedFull
    outputDir = $outputDirFull
    outputPrefix = $safePrefix
    eligibleMode = $EligibleMode
    joinedRows = $rows.Count
    eligibleRows = $eligible.Count
    laneIndexCsv = $indexCsv
    laneSummary = $summaryPath
    laneCount = $laneOutputs.Count
    belowMinCount = $belowMin.Count
} | Format-List
