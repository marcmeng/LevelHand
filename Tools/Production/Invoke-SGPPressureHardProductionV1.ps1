param(
    [string]$ProjectRoot = "F:\Unityproject\ArrowLevel-Hand",
    [string]$UnityExe = "H:\UnityEditorFromG\unity\Editor\Unity.exe",
    [string]$TraceToolRoot = "F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab",
    [string]$OutputPrefix = "sgp_pressure_hard_production_v1",
    [int]$TraceMaxLevels = 8,
    [int]$UnityWaitSeconds = 600,
    [ValidateSet("TraceOrderPreferred", "TraceOrderRequired", "VisualOnly")]
    [string]$ProductionKeepMode = "TraceOrderPreferred",
    [int]$MinProductionKeepRows = 1,
    [switch]$EnableProductionDiversity,
    [int]$MaxProductionKeepRows = 0,
    [int]$MaxProductionKeepPerStyleFamily = 0,
    [int]$MaxProductionKeepPerFlowLanguage = 0,
    [int]$MaxProductionKeepPerChainLanguage = 0,
    [int]$MaxProductionKeepPerStyleFlow = 0,
    [int]$MaxProductionKeepPerStyleSignature = 0,
    [int]$MaxProductionKeepHighRiskRows = 0,
    [switch]$StrictProductionDiversity,
    [switch]$SkipUnity,
    [switch]$SkipTrace
)

$ErrorActionPreference = "Stop"

function Ensure-Directory([string]$path) {
    if (-not (Test-Path -LiteralPath $path)) {
        New-Item -ItemType Directory -Path $path -Force | Out-Null
    }
}

$runRoot = Join-Path $ProjectRoot ".codex-run"
Ensure-Directory $runRoot

$unityLog = Join-Path $runRoot "$OutputPrefix`_unity.log"
$reportCsv = Join-Path $ProjectRoot "Assets\ArrowMagic\SOData\Reports\DirectProcedural\sgp_pressure_hard_trial_report.csv"
$traceInputCsv = Join-Path $runRoot "$OutputPrefix`_trace_input.csv"
$traceScript = Join-Path $TraceToolRoot "Tools\SGPRhythmLab\Build-SGPRhythmTrace.ps1"
$joinScript = Join-Path $ProjectRoot "Tools\Production\Join-SGPPressureTraceMetrics.ps1"
$traceReportDir = Join-Path $TraceToolRoot "Assets\ArrowMagic\SOData\Reports\SGPRhythmLab"
$traceMetricsCsv = Join-Path $traceReportDir "$OutputPrefix`_metrics.csv"
$traceJoinedCsv = Join-Path $runRoot "$OutputPrefix`_trace_joined.csv"
$traceJoinedSummary = Join-Path $runRoot "$OutputPrefix`_trace_joined_summary.md"
$traceBestPerSlotCsv = Join-Path $runRoot "$OutputPrefix`_trace_best_per_slot.csv"
$traceProductionKeepCsv = Join-Path $runRoot "$OutputPrefix`_production_keep.csv"

if (-not $SkipUnity) {
    if (-not (Test-Path -LiteralPath $UnityExe)) {
        throw "Unity executable not found: $UnityExe"
    }

    & $UnityExe `
        -batchmode `
        -quit `
        -projectPath $ProjectRoot `
        -executeMethod PixelBug.ArrowMagic.EditorTools.NoMaskProceduralGenerator.BuildSgpPressureHardTrialPack `
        -logFile $unityLog

    $deadline = (Get-Date).AddSeconds([Math]::Max(1, $UnityWaitSeconds))
    $unitySucceeded = $false
    do {
        if (Test-Path -LiteralPath $unityLog) {
            $tail = Get-Content -LiteralPath $unityLog -Tail 80 -ErrorAction SilentlyContinue
            if (($tail -join "`n") -match "SGP Pressure Hard Trial finished") {
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

if (-not (Test-Path -LiteralPath $reportCsv)) {
    throw "Missing pressure-hard report: $reportCsv"
}

$rows = Import-Csv -LiteralPath $reportCsv
$traceRows = foreach ($row in $rows) {
    if ([string]::IsNullOrWhiteSpace($row.LevelId) -or [string]::IsNullOrWhiteSpace($row.AssetPath)) {
        continue
    }

    [pscustomobject]@{
        selected = 1
        levelId = $row.LevelId
        path = $row.AssetPath
        source = "SGPPressureHardProductionV1"
        coverage = $row.Coverage
        chains = $row.Chains
        portableOpeners = $row.PortableOpeners
        status = $row.Status
    }
}

if (@($traceRows).Count -eq 0) {
    throw "Report has no traceable rows: $reportCsv"
}

$traceRows | Export-Csv -LiteralPath $traceInputCsv -NoTypeInformation -Encoding UTF8

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
        -EnableProductionDiversity:$EnableProductionDiversity `
        -MaxProductionKeepRows $MaxProductionKeepRows `
        -MaxProductionKeepPerStyleFamily $MaxProductionKeepPerStyleFamily `
        -MaxProductionKeepPerFlowLanguage $MaxProductionKeepPerFlowLanguage `
        -MaxProductionKeepPerChainLanguage $MaxProductionKeepPerChainLanguage `
        -MaxProductionKeepPerStyleFlow $MaxProductionKeepPerStyleFlow `
        -MaxProductionKeepPerStyleSignature $MaxProductionKeepPerStyleSignature `
        -MaxProductionKeepHighRiskRows $MaxProductionKeepHighRiskRows `
        -StrictProductionDiversity:$StrictProductionDiversity
}

$productionKeepRows = 0
if (Test-Path -LiteralPath $traceProductionKeepCsv) {
    $productionKeepRows = @((Import-Csv -LiteralPath $traceProductionKeepCsv)).Count
}
if ($MinProductionKeepRows -gt 0 -and $productionKeepRows -lt $MinProductionKeepRows) {
    throw "Production keep rows $productionKeepRows below MinProductionKeepRows=$MinProductionKeepRows. Expected keep CSV: $traceProductionKeepCsv"
}

$summary = [pscustomobject]@{
    projectRoot = $ProjectRoot
    unityLog = $unityLog
    reportCsv = $reportCsv
    traceInputCsv = $traceInputCsv
    traceMetricsCsv = $traceMetricsCsv
    traceJoinedCsv = $traceJoinedCsv
    traceJoinedSummary = $traceJoinedSummary
    traceBestPerSlotCsv = $traceBestPerSlotCsv
    traceProductionKeepCsv = $traceProductionKeepCsv
    productionKeepMode = $ProductionKeepMode
    productionKeepRows = $productionKeepRows
    productionDiversityEnabled = [bool]$EnableProductionDiversity
    maxProductionKeepRows = $MaxProductionKeepRows
    maxProductionKeepPerStyleFamily = $MaxProductionKeepPerStyleFamily
    maxProductionKeepPerFlowLanguage = $MaxProductionKeepPerFlowLanguage
    maxProductionKeepPerChainLanguage = $MaxProductionKeepPerChainLanguage
    maxProductionKeepPerStyleFlow = $MaxProductionKeepPerStyleFlow
    maxProductionKeepPerStyleSignature = $MaxProductionKeepPerStyleSignature
    maxProductionKeepHighRiskRows = $MaxProductionKeepHighRiskRows
    strictProductionDiversity = [bool]$StrictProductionDiversity
    generatedRows = @($traceRows).Count
    skippedUnity = [bool]$SkipUnity
    skippedTrace = [bool]$SkipTrace
}

$summary | Format-List
