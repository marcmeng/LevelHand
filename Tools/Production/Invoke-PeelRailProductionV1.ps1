param(
    [string]$ProjectRoot = "F:\Unityproject\ArrowLevel-Hand",
    [string]$UnityExe = "H:\UnityEditorFromG\unity\Editor\Unity.exe",
    [string]$TraceToolRoot = "F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab",
    [string]$OutputPrefix = "peel_rail_v1",
    [int]$TraceMaxLevels = 8,
    [int]$UnityWaitSeconds = 900,
    [ValidateSet("TraceOrderPreferred", "TraceOrderRequired", "VisualOnly")]
    [string]$ProductionKeepMode = "TraceOrderPreferred",
    [int]$MinProductionKeepRows = 1,
    [double]$MaxDirectionalSweepRisk = 0.32,
    [int]$MaxLocalPatchSolveRun = 7,
    [int]$MaxNearOuterPatchSolveRun = 5,
    [int]$MaxSolveSameAxisRun = 7,
    [int]$MaxSolveSameDirHeadRun = 7,
    [double]$MaxDependencyLocalSameRegionRate = 0.60,
    [switch]$SkipUnity,
    [switch]$SkipTrace
)

$ErrorActionPreference = "Stop"

function Ensure-Directory([string]$path) {
    if (-not (Test-Path -LiteralPath $path)) {
        New-Item -ItemType Directory -Path $path -Force | Out-Null
    }
}

function Export-TraceInputFromSourceReport(
    [string]$ReportCsv,
    [string]$TraceInputCsv,
    [string]$SourceName
) {
    if (-not (Test-Path -LiteralPath $ReportCsv)) {
        throw "Missing source report: $ReportCsv"
    }

    $rows = Import-Csv -LiteralPath $ReportCsv
    $traceRows = foreach ($row in $rows) {
        if ([string]::IsNullOrWhiteSpace($row.LevelId) -or [string]::IsNullOrWhiteSpace($row.AssetPath)) {
            continue
        }

        [pscustomobject]@{
            selected = 1
            levelId = $row.LevelId
            path = $row.AssetPath
            source = $SourceName
            coverage = $row.Coverage
            chains = $row.Chains
            portableOpeners = $row.PortableOpeners
            status = $row.Status
        }
    }

    if (@($traceRows).Count -eq 0) {
        throw "Report has no traceable rows: $ReportCsv"
    }

    $traceRows | Export-Csv -LiteralPath $TraceInputCsv -NoTypeInformation -Encoding UTF8
    return @($traceRows).Count
}

$runRoot = Join-Path $ProjectRoot ".codex-run"
Ensure-Directory $runRoot
$unityLogRoot = Join-Path $ProjectRoot "_CodexRun"
Ensure-Directory $unityLogRoot

$unityLog = Join-Path $unityLogRoot "$OutputPrefix`_unity.log"
$reportCsv = Join-Path $ProjectRoot "Assets\ArrowMagic\SOData\Reports\DirectProcedural\peel_rail_v1_report.csv"
$traceInputCsv = Join-Path $runRoot "$OutputPrefix`_trace_input.csv"
$traceScript = Join-Path $TraceToolRoot "Tools\SGPRhythmLab\Build-SGPRhythmTrace.ps1"
$joinScript = Join-Path $ProjectRoot "Tools\Production\Join-SGPPressureTraceMetrics.ps1"
$traceReportDir = Join-Path $TraceToolRoot "Assets\ArrowMagic\SOData\Reports\SGPRhythmLab"
$traceMetricsCsv = Join-Path $traceReportDir "$OutputPrefix`_metrics.csv"
$traceJoinedCsv = Join-Path $runRoot "$OutputPrefix`_trace_joined.csv"
$traceJoinedSummary = Join-Path $runRoot "$OutputPrefix`_trace_joined_summary.md"
$traceBestPerSlotCsv = Join-Path $runRoot "$OutputPrefix`_trace_best_per_slot.csv"
$traceProductionKeepCsv = Join-Path $runRoot "$OutputPrefix`_production_keep.csv"
$projectProductionKeepCsv = Join-Path $ProjectRoot "Assets\ArrowMagic\SOData\Reports\DirectProcedural\peel_rail_v1_production_keep.csv"

if (-not $SkipUnity) {
    if (-not (Test-Path -LiteralPath $UnityExe)) {
        throw "Unity executable not found: $UnityExe"
    }

    & $UnityExe `
        -batchmode `
        -quit `
        -projectPath $ProjectRoot `
        -executeMethod PixelBug.ArrowMagic.EditorTools.NoMaskProceduralGenerator.BuildPeelRailV1Pack `
        -logFile $unityLog

    $deadline = (Get-Date).AddSeconds([Math]::Max(1, $UnityWaitSeconds))
    $unitySucceeded = $false
    do {
        if (Test-Path -LiteralPath $unityLog) {
            $tail = Get-Content -LiteralPath $unityLog -Tail 120 -ErrorAction SilentlyContinue
            if (($tail -join "`n") -match "Peel Rail V1 finished") {
                $unitySucceeded = $true
                break
            }
        }

        if ((Get-Date) -lt $deadline) {
            Start-Sleep -Seconds 2
        }
    } while ((Get-Date) -lt $deadline)

    if (-not $unitySucceeded) {
        throw "Unity generation did not reach success marker. ExitCode=$LASTEXITCODE Log: $unityLog"
    }
}

$generatedRows = Export-TraceInputFromSourceReport `
    -ReportCsv $reportCsv `
    -TraceInputCsv $traceInputCsv `
    -SourceName "PeelRailV1"

if (-not $SkipTrace) {
    if (-not (Test-Path -LiteralPath $traceScript)) {
        throw "Missing trace script: $traceScript"
    }

    & $traceScript `
        -SourceRoot $ProjectRoot `
        -OutputRoot $TraceToolRoot `
        -InputCsv $traceInputCsv `
        -OutputPrefix $OutputPrefix `
        -MaxLevels $TraceMaxLevels

    if (-not (Test-Path -LiteralPath $traceMetricsCsv)) {
        throw "Trace did not produce expected metrics: $traceMetricsCsv"
    }
}

if ((Test-Path -LiteralPath $traceMetricsCsv) -and (Test-Path -LiteralPath $joinScript)) {
    & $joinScript `
        -SourceCsv $reportCsv `
        -TraceMetricsCsv $traceMetricsCsv `
        -OutputCsv $traceJoinedCsv `
        -SummaryPath $traceJoinedSummary `
        -BestPerSlotCsv $traceBestPerSlotCsv `
        -ProductionKeepCsv $traceProductionKeepCsv `
        -ProductionKeepMode $ProductionKeepMode `
        -MinProductionKeepRows $MinProductionKeepRows `
        -MaxChoices 10 `
        -MaxDirectionalSweepRisk $MaxDirectionalSweepRisk `
        -MaxLocalPatchSolveRun $MaxLocalPatchSolveRun `
        -MaxNearOuterPatchSolveRun $MaxNearOuterPatchSolveRun `
        -MaxSolveSameAxisRun $MaxSolveSameAxisRun `
        -MaxSolveSameDirHeadRun $MaxSolveSameDirHeadRun `
        -MaxDependencyLocalSameRegionRate $MaxDependencyLocalSameRegionRate
}

$productionKeepRows = 0
if (Test-Path -LiteralPath $traceProductionKeepCsv) {
    $productionKeepRows = @((Import-Csv -LiteralPath $traceProductionKeepCsv)).Count
    Copy-Item -LiteralPath $traceProductionKeepCsv -Destination $projectProductionKeepCsv -Force
}
if ($MinProductionKeepRows -gt 0 -and $productionKeepRows -lt $MinProductionKeepRows) {
    throw "Production keep rows $productionKeepRows below MinProductionKeepRows=$MinProductionKeepRows. Expected keep CSV: $traceProductionKeepCsv"
}

[pscustomobject]@{
    projectRoot = $ProjectRoot
    unityLog = $unityLog
    reportCsv = $reportCsv
    traceInputCsv = $traceInputCsv
    traceMetricsCsv = $traceMetricsCsv
    traceJoinedCsv = $traceJoinedCsv
    traceJoinedSummary = $traceJoinedSummary
    traceBestPerSlotCsv = $traceBestPerSlotCsv
    traceProductionKeepCsv = $traceProductionKeepCsv
    projectProductionKeepCsv = $projectProductionKeepCsv
    productionKeepMode = $ProductionKeepMode
    productionKeepRows = $productionKeepRows
    generatedRows = $generatedRows
    skippedUnity = [bool]$SkipUnity
    skippedTrace = [bool]$SkipTrace
}
