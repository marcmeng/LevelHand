param(
    [string]$MetricsCsv = ".codex-run/c5v4f_hardpeak_all_observation_pressure_v1.csv",
    [string]$OutputCsv = ".codex-run/c5v4f_hardpeak_read_demand_gate_v1.csv",
    [string]$SummaryPath = ".codex-run/c5v4f_hardpeak_read_demand_gate_v1_summary.md",
    [double]$KeepReadDemand = 28.0,
    [double]$NearReadDemand = 25.0
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

function To-Double {
    param([object]$Value, [double]$Default = 0.0)
    if ($null -eq $Value) { return $Default }
    $text = [string]$Value
    if ([string]::IsNullOrWhiteSpace($text)) { return $Default }
    $parsed = 0.0
    if ([double]::TryParse($text, [Globalization.NumberStyles]::Float, [Globalization.CultureInfo]::InvariantCulture, [ref]$parsed)) {
        return $parsed
    }
    return $Default
}

function To-Int {
    param([object]$Value, [int]$Default = 0)
    if ($null -eq $Value) { return $Default }
    $text = [string]$Value
    if ([string]::IsNullOrWhiteSpace($text)) { return $Default }
    $parsed = 0
    if ([int]::TryParse($text, [ref]$parsed)) { return $parsed }
    return $Default
}

if (-not (Test-Path -LiteralPath $MetricsCsv)) {
    throw "Missing metrics CSV: $MetricsCsv"
}

$outDir = Split-Path -Parent $OutputCsv
if (-not [string]::IsNullOrWhiteSpace($outDir)) {
    New-Item -ItemType Directory -Force -Path $outDir | Out-Null
}

$summaryDir = Split-Path -Parent $SummaryPath
if (-not [string]::IsNullOrWhiteSpace($summaryDir)) {
    New-Item -ItemType Directory -Force -Path $summaryDir | Out-Null
}

$rows = Import-Csv -LiteralPath $MetricsCsv
$classified = New-Object System.Collections.Generic.List[object]

foreach ($row in $rows) {
    $read = To-Double $row.readDemandScore
    $obs = To-Double $row.observationPressureScore
    $opener = To-Double $row.openerRatio
    $avgRay = To-Double $row.avgEscapeRayLength
    $shortRay = To-Double $row.shortRayRate
    $cross = To-Double $row.crossRegionEdgeRate
    $depth = To-Int $row.dependencyDepth
    $edges = To-Int $row.dependencyEdges
    $chains = To-Int $row.chainCount

    $reasons = New-Object System.Collections.Generic.List[string]
    if ($read -lt $NearReadDemand) { [void]$reasons.Add("read_below_near") }
    if ($read -lt 15.0) { [void]$reasons.Add("read_very_low") }
    if ($obs -lt 65.0) { [void]$reasons.Add("static_dependency_low") }
    if ($opener -gt 0.18) { [void]$reasons.Add("too_many_openers") }
    if ($avgRay -lt 0.90) { [void]$reasons.Add("ray_too_local") }
    if ($shortRay -gt 0.985) { [void]$reasons.Add("adjacent_blockers_saturated") }
    if ($cross -lt 0.22) { [void]$reasons.Add("cross_region_low") }
    if ($depth -lt 6) { [void]$reasons.Add("dependency_depth_low") }
    if ($chains -gt 0 -and ($edges / [double]$chains) -lt 0.65) { [void]$reasons.Add("dependency_density_low") }

    $gate = "RegenPriority"
    if ($read -ge $KeepReadDemand -and $obs -ge 70.0 -and $opener -le 0.12 -and $avgRay -ge 0.90 -and $cross -ge 0.21 -and $depth -ge 6) {
        $gate = "ReadKeep"
    } elseif ($read -ge $NearReadDemand -and $obs -ge 70.0 -and $opener -le 0.16 -and $depth -ge 6) {
        $gate = "ReadNear"
    }

    if ($read -lt 15.0 -or $opener -gt 0.24 -or $obs -lt 60.0) {
        $gate = "RegenPriorityHigh"
    }

    [void]$classified.Add([pscustomobject]@{
        order = To-Int $row.order
        sectionWaveRole = $row.sectionWaveRole
        readDemandGate = $gate
        readDemandScore = [math]::Round($read, 2)
        observationPressureScore = [math]::Round($obs, 2)
        chainCount = $chains
        openerRatio = [math]::Round($opener, 4)
        avgEscapeRayLength = [math]::Round($avgRay, 3)
        shortRayRate = [math]::Round($shortRay, 4)
        crossRegionEdgeRate = [math]::Round($cross, 4)
        dependencyDepth = $depth
        dependencyEdges = $edges
        productionLane = $row.productionLane
        productionStyle = $row.productionStyle
        productionChainLanguage = $row.productionChainLanguage
        loadedAssetPath = $row.loadedAssetPath
        gateReasons = ($reasons -join ";")
    })
}

$classified |
    Sort-Object @{ Expression = "readDemandGate"; Descending = $false }, @{ Expression = "readDemandScore"; Descending = $true }, order |
    Export-Csv -LiteralPath $OutputCsv -NoTypeInformation -Encoding UTF8

$gateLines = $classified |
    Group-Object readDemandGate |
    Sort-Object Name |
    ForEach-Object { "- $($_.Name): $($_.Count)" }

$keepLines = $classified |
    Where-Object { $_.readDemandGate -eq "ReadKeep" } |
    Sort-Object @{ Expression = "readDemandScore"; Descending = $true }, order |
    Select-Object -First 20 |
    ForEach-Object {
        "- order $($_.order) [$($_.sectionWaveRole)] read=$($_.readDemandScore), obs=$($_.observationPressureScore), cross=$($_.crossRegionEdgeRate), depth=$($_.dependencyDepth), lane=$($_.productionLane)"
    }

$nearLines = $classified |
    Where-Object { $_.readDemandGate -eq "ReadNear" } |
    Sort-Object @{ Expression = "readDemandScore"; Descending = $true }, order |
    Select-Object -First 20 |
    ForEach-Object {
        "- order $($_.order) [$($_.sectionWaveRole)] read=$($_.readDemandScore), obs=$($_.observationPressureScore), cross=$($_.crossRegionEdgeRate), depth=$($_.dependencyDepth), lane=$($_.productionLane)"
    }

$regenHighLines = $classified |
    Where-Object { $_.readDemandGate -eq "RegenPriorityHigh" } |
    Sort-Object order |
    Select-Object -First 30 |
    ForEach-Object {
        "- order $($_.order) [$($_.sectionWaveRole)] read=$($_.readDemandScore), obs=$($_.observationPressureScore), reasons=$($_.gateReasons)"
    }

$summary = @(
    "# Campaign Read-Demand Gate V1",
    "",
    "- Metrics CSV: $MetricsCsv",
    "- Output CSV: $OutputCsv",
    "- Keep threshold: readDemand >= $KeepReadDemand plus static dependency, opener, cross-region, and depth guards",
    "- Near threshold: readDemand >= $NearReadDemand with relaxed cross-region/ray guards",
    "",
    "## Gate Counts",
    $gateLines,
    "",
    "## ReadKeep",
    $(if ($keepLines.Count -gt 0) { $keepLines } else { "- none" }),
    "",
    "## ReadNear",
    $(if ($nearLines.Count -gt 0) { $nearLines } else { "- none" }),
    "",
    "## RegenPriorityHigh First 30",
    $(if ($regenHighLines.Count -gt 0) { $regenHighLines } else { "- none" }),
    "",
    "## Interpretation",
    "- ReadKeep rows are the best current hard/peak survivors for visual read demand.",
    "- RegenPriority rows need replacement if the goal is harder observation-first play.",
    "- Adjacent blocker saturation is still reported as a reason even for some ReadKeep rows, because current pool has almost no long lookahead rays."
)

$summary | Set-Content -LiteralPath $SummaryPath -Encoding UTF8

Write-Host "Wrote $OutputCsv"
Write-Host "Wrote $SummaryPath"
($classified | Group-Object readDemandGate | Sort-Object Name | ForEach-Object { "$($_.Name)=$($_.Count)" }) -join "; " | Write-Host
