param(
    [Parameter(Mandatory = $true)]
    [string]$SourceReportCsv,
    [Parameter(Mandatory = $true)]
    [string]$SourceRoot,
    [Parameter(Mandatory = $true)]
    [int]$Order,
    [Parameter(Mandatory = $true)]
    [string]$OutputPrefix,
    [string]$TraceToolRoot = "F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab",
    [string]$JoinScript = "F:\Unityproject\ArrowLevel-Hand\.worktrees\nutation-flow-peel-production\Tools\Production\Join-SGPPressureTraceMetrics.ps1",
    [string]$MissingDiagCsv = "F:\Unityproject\ArrowLevel-Hand\.codex-run\campaign500_missing_after_raw_trace_v1_failure_diag.csv",
    [string]$RunRoot = "F:\Unityproject\ArrowLevel-Hand\.codex-run",
    [string]$ShortSourceDrive = ""
)

$ErrorActionPreference = "Stop"

if (-not (Test-Path -LiteralPath $SourceReportCsv)) { throw "Missing source report: $SourceReportCsv" }
if (-not (Test-Path -LiteralPath $SourceRoot)) { throw "Missing source root: $SourceRoot" }

$traceScript = Join-Path $TraceToolRoot "Tools\SGPRhythmLab\Build-SGPRhythmTrace.ps1"
if (-not (Test-Path -LiteralPath $traceScript)) { throw "Missing trace script: $traceScript" }
if (-not (Test-Path -LiteralPath $JoinScript)) { throw "Missing join script: $JoinScript" }

New-Item -ItemType Directory -Path $RunRoot -Force | Out-Null

$sourceSubset = Join-Path $RunRoot "$OutputPrefix`_source_subset.csv"
$traceInput = Join-Path $RunRoot "$OutputPrefix`_trace_input.csv"
$joined = Join-Path $RunRoot "$OutputPrefix`_trace_joined.csv"
$summary = Join-Path $RunRoot "$OutputPrefix`_trace_joined_summary.md"
$bestPerSlot = Join-Path $RunRoot "$OutputPrefix`_trace_best_per_slot.csv"
$productionKeep = Join-Path $RunRoot "$OutputPrefix`_production_keep.csv"
$auditPrefix = Join-Path $RunRoot "$OutputPrefix`_strict_audit"

$rows = @(Import-Csv -LiteralPath $SourceReportCsv | Where-Object {
    [int]$_.Order -eq $Order -and -not [string]::IsNullOrWhiteSpace([string]$_.LevelId)
})
if ($rows.Count -eq 0) { throw "No source rows found for order $Order in $SourceReportCsv" }

$rows | Export-Csv -LiteralPath $sourceSubset -NoTypeInformation -Encoding UTF8
$traceRows = foreach ($row in $rows) {
    [pscustomobject]@{
        selected = 1
        levelId = $row.LevelId
        path = $row.AssetPath
        source = $OutputPrefix
        order = $row.Order
        section10 = $row.Section10
        variantIndex = $row.VariantIndex
        variantLabel = $row.VariantLabel
        productionLane = $row.ProductionLane
        productionChainLanguage = $row.ProductionChainLanguage
        productionDifficultyBand = $row.ProductionDifficultyBand
        coverage = $row.Coverage
        chains = $row.Chains
        portableOpeners = $row.PortableOpeners
        status = $row.Status
    }
}
$traceRows | Export-Csv -LiteralPath $traceInput -NoTypeInformation -Encoding UTF8

$traceSourceRoot = $SourceRoot
$mappedDrive = ""
try {
    if (-not [string]::IsNullOrWhiteSpace($ShortSourceDrive)) {
        $mappedDrive = $ShortSourceDrive.TrimEnd("\").TrimEnd(":") + ":"
        & subst $mappedDrive /D 2>$null
        & subst $mappedDrive (Resolve-Path -LiteralPath $SourceRoot).Path
        $traceSourceRoot = "$mappedDrive\"
    }

    & $traceScript `
        -SourceRoot $traceSourceRoot `
        -OutputRoot $TraceToolRoot `
        -InputCsv $traceInput `
        -OutputPrefix $OutputPrefix `
        -MaxLevels $rows.Count
}
finally {
    if (-not [string]::IsNullOrWhiteSpace($mappedDrive)) {
        & subst $mappedDrive /D 2>$null
    }
}

$metrics = Join-Path $TraceToolRoot "Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\$OutputPrefix`_metrics.csv"
if (-not (Test-Path -LiteralPath $metrics)) { throw "Trace did not produce metrics: $metrics" }

& $JoinScript `
    -SourceCsv $sourceSubset `
    -TraceMetricsCsv $metrics `
    -OutputCsv $joined `
    -SummaryPath $summary `
    -BestPerSlotCsv $bestPerSlot `
    -ProductionKeepCsv $productionKeep `
    -ProductionKeepMode TraceOrderPreferred `
    -SlotField order `
    -AllowedProcessTiers S,A,B `
    -MinCoverage 0.93 `
    -MaxChoices 11 `
    -MaxDirectionalSweepRisk 0.34 `
    -MaxStripeVisualRisk 0.24 `
    -MaxLocalPatchSolveRun 7 `
    -MaxNearOuterPatchSolveRun 6 `
    -MinSolveTraceQuality 0.82 `
    -MaxSolveTraceCollapseRisk 0.28 `
    -MaxSolveSameAxisRun 9 `
    -MaxSolveSameDirHeadRun 8

& "F:\Unityproject\ArrowLevel-Hand\.codex-run\Audit-Campaign500StrictHolefillV1.ps1" `
    -JoinedCsv $joined `
    -MissingDiagCsv $MissingDiagCsv `
    -OutputPrefix $auditPrefix
