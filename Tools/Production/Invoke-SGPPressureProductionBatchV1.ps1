param(
    [string]$ProjectRoot = "F:\Unityproject\ArrowLevel-Hand",
    [string]$UnityExe = "H:\UnityEditorFromG\unity\Editor\Unity.exe",
    [string]$TraceToolRoot = "F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab",
    [string]$OutputPrefix = "sgp_pressure_hard_production_batch_v1",
    [int]$TraceMaxLevels = 64,
    [int]$TraceMaxCounterfactualMovesPerStep = 1,
    [int]$TraceCounterfactualStepStride = 4,
    [int]$UnityWaitSeconds = 900,
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
    [string[]]$SourceCsvs = @(),
    [string]$TraceMetricsCsv = "",
    [switch]$IncludeV3,
    [switch]$SkipUnity,
    [switch]$SkipTrace,
    [switch]$SkipPackBuild
)

$ErrorActionPreference = "Stop"

function Ensure-Directory([string]$path) {
    if ([string]::IsNullOrWhiteSpace($path)) { return }
    if (-not (Test-Path -LiteralPath $path)) {
        New-Item -ItemType Directory -Path $path -Force | Out-Null
    }
}

function Resolve-ProjectPath([string]$path) {
    if ([string]::IsNullOrWhiteSpace($path)) { return $path }
    if ([IO.Path]::IsPathRooted($path)) { return $path }
    return (Join-Path $ProjectRoot $path)
}

function Get-PropertyValue($row, [string[]]$names, [object]$fallback = "") {
    if ($null -eq $row) { return $fallback }
    foreach ($name in $names) {
        $prop = $row.PSObject.Properties | Where-Object { $_.Name -ieq $name } | Select-Object -First 1
        if ($null -ne $prop -and $null -ne $prop.Value -and -not [string]::IsNullOrWhiteSpace([string]$prop.Value)) {
            return $prop.Value
        }
    }
    return $fallback
}

function Wait-ForLogMarker([string]$logPath, [string]$marker, [int]$waitSeconds) {
    $deadline = (Get-Date).AddSeconds([Math]::Max(1, $waitSeconds))
    do {
        if (Test-Path -LiteralPath $logPath) {
            $match = Select-String -LiteralPath $logPath -Pattern $marker -SimpleMatch -List -ErrorAction SilentlyContinue
            if ($null -ne $match) {
                return $true
            }
        }

        if ((Get-Date) -lt $deadline) {
            Start-Sleep -Seconds 2
        }
    } while ((Get-Date) -lt $deadline)

    return $false
}

function Invoke-UnityMethod([string]$method, [string]$logPath, [string]$successMarker) {
    if (-not (Test-Path -LiteralPath $UnityExe)) {
        throw "Unity executable not found: $UnityExe"
    }

    Ensure-Directory (Split-Path -Parent $logPath)
    & $UnityExe `
        -batchmode `
        -quit `
        -projectPath $ProjectRoot `
        -executeMethod $method `
        -logFile $logPath

    $exitCode = $LASTEXITCODE
    if (Wait-ForLogMarker $logPath $successMarker $UnityWaitSeconds) {
        return
    }

    throw "Unity method did not reach success marker '$successMarker'. Method=$method ExitCode=$exitCode Log=$logPath"
}

if (-not (Test-Path -LiteralPath $ProjectRoot)) { throw "Project root not found: $ProjectRoot" }
if (-not (Test-Path -LiteralPath $TraceToolRoot)) { throw "Trace tool root not found: $TraceToolRoot" }

$runRoot = Join-Path $ProjectRoot ".codex-run"
Ensure-Directory $runRoot

$candidateBatches = New-Object System.Collections.Generic.List[object]
$candidateBatches.Add([pscustomobject]@{
    Label = "trial"
    Method = "PixelBug.ArrowMagic.EditorTools.NoMaskProceduralGenerator.BuildSgpPressureHardTrialPack"
    Report = "Assets\ArrowMagic\SOData\Reports\DirectProcedural\sgp_pressure_hard_trial_report.csv"
    Success = "SGP Pressure Hard Trial finished"
}) | Out-Null
$candidateBatches.Add([pscustomobject]@{
    Label = "review6"
    Method = "PixelBug.ArrowMagic.EditorTools.NoMaskProceduralGenerator.BuildSgpPressureHardReview6Pack"
    Report = "Assets\ArrowMagic\SOData\Reports\DirectProcedural\sgp_pressure_hard_review6_report.csv"
    Success = "SGP Pressure Hard Review 6 finished"
}) | Out-Null
$candidateBatches.Add([pscustomobject]@{
    Label = "interference6"
    Method = "PixelBug.ArrowMagic.EditorTools.NoMaskProceduralGenerator.BuildSgpPressureHardInterference6Pack"
    Report = "Assets\ArrowMagic\SOData\Reports\DirectProcedural\sgp_pressure_hard_interference6_report.csv"
    Success = "SGP Pressure Hard Interference 6 finished"
}) | Out-Null
$candidateBatches.Add([pscustomobject]@{
    Label = "interference_v2_six"
    Method = "PixelBug.ArrowMagic.EditorTools.NoMaskProceduralGenerator.BuildSgpPressureHardInterferenceV2SixPack"
    Report = "Assets\ArrowMagic\SOData\Reports\DirectProcedural\sgp_pressure_hard_interference_v2_six_report.csv"
    Success = "SGP Pressure Hard Interference V2 Six finished"
}) | Out-Null

if ($IncludeV3) {
    $candidateBatches.Add([pscustomobject]@{
        Label = "interference_v3_six"
        Method = "PixelBug.ArrowMagic.EditorTools.NoMaskProceduralGenerator.BuildSgpPressureHardInterferenceV3SixPack"
        Report = "Assets\ArrowMagic\SOData\Reports\DirectProcedural\sgp_pressure_hard_interference_v3_six_report.csv"
        Success = "SGP Pressure Hard Interference V3 Six finished"
    }) | Out-Null
}

$runUnityGeneration = (-not [bool]$SkipUnity) -and @($SourceCsvs).Count -eq 0
if ($runUnityGeneration) {
    foreach ($batch in $candidateBatches) {
        $unityLog = Join-Path $runRoot ("{0}_{1}_unity.log" -f $OutputPrefix, $batch.Label)
        Invoke-UnityMethod $batch.Method $unityLog $batch.Success
    }
} elseif ((-not [bool]$SkipUnity) -and @($SourceCsvs).Count -gt 0) {
    Write-Warning "SourceCsvs was supplied, so Unity candidate generation is skipped for custom source reports."
}

$sourceInputs = New-Object System.Collections.Generic.List[object]
if (@($SourceCsvs).Count -gt 0) {
    foreach ($sourceCsv in $SourceCsvs) {
        $resolved = Resolve-ProjectPath $sourceCsv
        $sourceInputs.Add([pscustomobject]@{
            Label = [IO.Path]::GetFileNameWithoutExtension($resolved)
            Path = $resolved
        }) | Out-Null
    }
} else {
    foreach ($batch in $candidateBatches) {
        $sourceInputs.Add([pscustomobject]@{
            Label = $batch.Label
            Path = Resolve-ProjectPath $batch.Report
        }) | Out-Null
    }
}

$combinedSourceCsv = Join-Path $runRoot "$OutputPrefix`_source_combined.csv"
$traceInputCsv = Join-Path $runRoot "$OutputPrefix`_trace_input.csv"
$traceScript = Join-Path $TraceToolRoot "Tools\SGPRhythmLab\Build-SGPRhythmTrace.ps1"
$joinScript = Join-Path $ProjectRoot "Tools\Production\Join-SGPPressureTraceMetrics.ps1"
$traceReportDir = Join-Path $TraceToolRoot "Assets\ArrowMagic\SOData\Reports\SGPRhythmLab"
$traceMetricsPath = if (-not [string]::IsNullOrWhiteSpace($TraceMetricsCsv)) {
    Resolve-ProjectPath $TraceMetricsCsv
} else {
    Join-Path $traceReportDir "$OutputPrefix`_metrics.csv"
}
$traceJoinedCsv = Join-Path $runRoot "$OutputPrefix`_trace_joined.csv"
$traceJoinedSummary = Join-Path $runRoot "$OutputPrefix`_trace_joined_summary.md"
$traceBestPerSlotCsv = Join-Path $runRoot "$OutputPrefix`_trace_best_per_slot.csv"
$traceProductionKeepCsv = Join-Path $runRoot "$OutputPrefix`_production_keep.csv"
$canonicalProductionKeepCsv = Join-Path $ProjectRoot "Assets\ArrowMagic\SOData\Reports\DirectProcedural\sgp_pressure_hard_production_keep.csv"
$productionKeepPackPath = Join-Path $ProjectRoot "Assets\ArrowMagic\SOData\Packs\DirectProcedural\SGPPressureHardProductionKeepPack.asset"

foreach ($input in $sourceInputs) {
    if (-not (Test-Path -LiteralPath $input.Path)) {
        throw "Missing source CSV for $($input.Label): $($input.Path)"
    }
}

$combinedRows = New-Object System.Collections.Generic.List[object]
$globalIndex = 0
foreach ($input in $sourceInputs) {
    $rows = @(Import-Csv -LiteralPath $input.Path)
    foreach ($row in $rows) {
        $levelId = [string](Get-PropertyValue $row @("LevelId", "levelId") "")
        $assetPath = [string](Get-PropertyValue $row @("AssetPath", "assetPath", "path", "Path") "")
        if ([string]::IsNullOrWhiteSpace($levelId) -or [string]::IsNullOrWhiteSpace($assetPath)) {
            continue
        }

        $globalIndex++
        $combinedRows.Add([pscustomobject]@{
            Index = $globalIndex
            SourceBatch = $input.Label
            SourceReport = $input.Path
            SourceIndex = Get-PropertyValue $row @("Index", "index", "Row", "row") ""
            Type = Get-PropertyValue $row @("Type", "type", "SpecId", "specId") ""
            LevelId = $levelId
            AssetPath = $assetPath
            Width = Get-PropertyValue $row @("Width", "width") ""
            Height = Get-PropertyValue $row @("Height", "height") ""
            Chains = Get-PropertyValue $row @("Chains", "chains") ""
            Arrows = Get-PropertyValue $row @("Arrows", "arrows") ""
            Coverage = Get-PropertyValue $row @("Coverage", "coverage", "sourceCoverage") ""
            TargetCoverage = Get-PropertyValue $row @("TargetCoverage", "targetCoverage") ""
            OuterBandCoverage = Get-PropertyValue $row @("OuterBandCoverage", "outerBandCoverage") ""
            InitialMovableChains = Get-PropertyValue $row @("InitialMovableChains", "initialMovableChains") ""
            EdgeHeadChains = Get-PropertyValue $row @("EdgeHeadChains", "edgeHeadChains") ""
            ShortChains = Get-PropertyValue $row @("ShortChains", "shortChains") ""
            GreedyMoves = Get-PropertyValue $row @("GreedyMoves", "greedyMoves") ""
            AvgChain = Get-PropertyValue $row @("AvgChain", "avgChain") ""
            MaxChain = Get-PropertyValue $row @("MaxChain", "maxChain") ""
            Straightness = Get-PropertyValue $row @("Straightness", "straightness") ""
            BlockLinks = Get-PropertyValue $row @("BlockLinks", "blockLinks") ""
            PortableSolved = Get-PropertyValue $row @("PortableSolved", "portableSolved") ""
            PortableOpeners = Get-PropertyValue $row @("PortableOpeners", "portableOpeners") ""
            PortableScore = Get-PropertyValue $row @("PortableScore", "portableScore") ""
            PortableQuality = Get-PropertyValue $row @("PortableQuality", "portableQuality") ""
            Attempts = Get-PropertyValue $row @("Attempts", "attempts") ""
            Status = Get-PropertyValue $row @("Status", "status") ""
        }) | Out-Null
    }
}

if ($combinedRows.Count -eq 0) {
    throw "No traceable rows found in source reports."
}

$combinedRows | Export-Csv -LiteralPath $combinedSourceCsv -NoTypeInformation -Encoding UTF8

$traceRows = foreach ($row in $combinedRows) {
    [pscustomobject]@{
        selected = 1
        levelId = $row.LevelId
        path = $row.AssetPath
        source = $row.SourceBatch
        coverage = $row.Coverage
        chains = $row.Chains
        portableOpeners = $row.PortableOpeners
        status = $row.Status
    }
}
$traceRows | Export-Csv -LiteralPath $traceInputCsv -NoTypeInformation -Encoding UTF8

if ($TraceMaxLevels -gt 0 -and $TraceMaxLevels -lt $combinedRows.Count) {
    Write-Warning "TraceMaxLevels=$TraceMaxLevels is below combined source rows=$($combinedRows.Count); only the first trace rows may be replayed."
}

$runTrace = (-not [bool]$SkipTrace) -and [string]::IsNullOrWhiteSpace($TraceMetricsCsv)
if ($runTrace) {
    if (-not (Test-Path -LiteralPath $traceScript)) {
        throw "Missing trace script: $traceScript"
    }

    & $traceScript `
        -SourceRoot $ProjectRoot `
        -OutputRoot $TraceToolRoot `
        -InputCsv $traceInputCsv `
        -OutputPrefix $OutputPrefix `
        -MaxLevels $TraceMaxLevels `
        -MaxCounterfactualMovesPerStep $TraceMaxCounterfactualMovesPerStep `
        -CounterfactualStepStride $TraceCounterfactualStepStride
}

if (-not (Test-Path -LiteralPath $traceMetricsPath)) {
    throw "Missing trace metrics CSV: $traceMetricsPath"
}
if (-not (Test-Path -LiteralPath $joinScript)) {
    throw "Missing PSG join script: $joinScript"
}

& $joinScript `
    -SourceCsv $combinedSourceCsv `
    -TraceMetricsCsv $traceMetricsPath `
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

$productionKeepRows = @()
if (Test-Path -LiteralPath $traceProductionKeepCsv) {
    $productionKeepRows = @(Import-Csv -LiteralPath $traceProductionKeepCsv)
}
if ($MinProductionKeepRows -gt 0 -and $productionKeepRows.Count -lt $MinProductionKeepRows) {
    throw "Production keep rows $($productionKeepRows.Count) below MinProductionKeepRows=$MinProductionKeepRows. Expected keep CSV: $traceProductionKeepCsv"
}

Ensure-Directory (Split-Path -Parent $canonicalProductionKeepCsv)
Copy-Item -LiteralPath $traceProductionKeepCsv -Destination $canonicalProductionKeepCsv -Force

$packUnityLog = Join-Path $runRoot "$OutputPrefix`_pack_unity.log"
if (-not $SkipPackBuild) {
    Invoke-UnityMethod `
        "PixelBug.ArrowMagic.EditorTools.NoMaskProceduralGenerator.BuildSgpPressureHardProductionKeepPack" `
        $packUnityLog `
        "SGP Pressure Hard Production Keep finished"

    if (-not (Test-Path -LiteralPath $productionKeepPackPath)) {
        throw "Production keep pack was not found after Unity build: $productionKeepPackPath"
    }
}

[pscustomobject]@{
    projectRoot = $ProjectRoot
    outputPrefix = $OutputPrefix
    sourceRows = $combinedRows.Count
    sourceCombinedCsv = $combinedSourceCsv
    traceInputCsv = $traceInputCsv
    traceMetricsCsv = $traceMetricsPath
    traceJoinedCsv = $traceJoinedCsv
    traceJoinedSummary = $traceJoinedSummary
    traceBestPerSlotCsv = $traceBestPerSlotCsv
    traceProductionKeepCsv = $traceProductionKeepCsv
    canonicalProductionKeepCsv = $canonicalProductionKeepCsv
    productionKeepRows = $productionKeepRows.Count
    productionKeepMode = $ProductionKeepMode
    productionDiversityEnabled = [bool]$EnableProductionDiversity
    maxProductionKeepRows = $MaxProductionKeepRows
    maxProductionKeepPerStyleFamily = $MaxProductionKeepPerStyleFamily
    maxProductionKeepPerFlowLanguage = $MaxProductionKeepPerFlowLanguage
    maxProductionKeepPerChainLanguage = $MaxProductionKeepPerChainLanguage
    maxProductionKeepPerStyleFlow = $MaxProductionKeepPerStyleFlow
    maxProductionKeepPerStyleSignature = $MaxProductionKeepPerStyleSignature
    maxProductionKeepHighRiskRows = $MaxProductionKeepHighRiskRows
    strictProductionDiversity = [bool]$StrictProductionDiversity
    traceMaxCounterfactualMovesPerStep = $TraceMaxCounterfactualMovesPerStep
    traceCounterfactualStepStride = $TraceCounterfactualStepStride
    productionKeepPackPath = $productionKeepPackPath
    packUnityLog = $(if ($SkipPackBuild) { "" } else { $packUnityLog })
    skippedUnity = [bool]$SkipUnity
    skippedTrace = [bool]$SkipTrace
    skippedPackBuild = [bool]$SkipPackBuild
    includedV3 = [bool]$IncludeV3
} | Format-List
