param(
    [string]$ProjectRoot = "F:\Unityproject\ArrowLevel-Hand",
    [string]$UnityExe = "H:\UnityEditorFromG\unity\Editor\Unity.exe",
    [string]$TraceToolRoot = "F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab",
    [string]$OutputPrefix = "nutation_peel_v1",
    [int]$TraceMaxLevels = 8,
    [int]$UnityWaitSeconds = 600,
    [ValidateSet("TraceOrderPreferred", "TraceOrderRequired", "VisualOnly")]
    [string]$ProductionKeepMode = "TraceOrderPreferred",
    [int]$MinProductionKeepRows = 1,
    [double]$MaxDirectionalSweepRisk = 0.28,
    [int]$MaxLocalPatchSolveRun = 7,
    [int]$MaxNearOuterPatchSolveRun = 5,
    [int]$MaxSolveSameAxisRun = 7,
    [int]$MaxSolveSameDirHeadRun = 7,
    [double]$MaxDependencyLocalSameRegionRate = 0.60,
    [switch]$SkipUnity,
    [switch]$SkipTrace,
    [switch]$SkipPsgCompare,
    [string]$PsgComparePrefix = "nutation_compare_psg_trial_v1"
)

$ErrorActionPreference = "Stop"

function Ensure-Directory([string]$path) {
    if (-not (Test-Path -LiteralPath $path)) {
        New-Item -ItemType Directory -Path $path -Force | Out-Null
    }
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

function Join-SourceAndTrace(
    [string]$JoinScript,
    [string]$SourceCsv,
    [string]$TraceMetricsCsv,
    [string]$JoinedCsv,
    [string]$SummaryPath,
    [string]$BestPerSlotCsv,
    [string]$ProductionKeepCsv,
    [string]$Mode,
    [int]$MinRows
) {
    if ((Test-Path -LiteralPath $TraceMetricsCsv) -and (Test-Path -LiteralPath $JoinScript)) {
        & $JoinScript `
            -SourceCsv $SourceCsv `
            -TraceMetricsCsv $TraceMetricsCsv `
            -OutputCsv $JoinedCsv `
            -SummaryPath $SummaryPath `
            -BestPerSlotCsv $BestPerSlotCsv `
            -ProductionKeepCsv $ProductionKeepCsv `
            -ProductionKeepMode $Mode `
            -MinProductionKeepRows $MinRows `
            -MaxChoices 10 `
            -MaxDirectionalSweepRisk $MaxDirectionalSweepRisk `
            -MaxLocalPatchSolveRun $MaxLocalPatchSolveRun `
            -MaxNearOuterPatchSolveRun $MaxNearOuterPatchSolveRun `
            -MaxSolveSameAxisRun $MaxSolveSameAxisRun `
            -MaxSolveSameDirHeadRun $MaxSolveSameDirHeadRun `
            -MaxDependencyLocalSameRegionRate $MaxDependencyLocalSameRegionRate
    }
}

function Write-NutationVsPsgSummary(
    [string]$NutationJoinedCsv,
    [string]$PsgJoinedCsv,
    [string]$OutputCsv,
    [string]$SummaryPath
) {
    if (-not (Test-Path -LiteralPath $NutationJoinedCsv) -or -not (Test-Path -LiteralPath $PsgJoinedCsv)) {
        return $false
    }

    $nutationRows = @(Import-Csv -LiteralPath $NutationJoinedCsv)
    $psgRows = @(Import-Csv -LiteralPath $PsgJoinedCsv)
    if ($nutationRows.Count -eq 0 -or $psgRows.Count -eq 0) {
        return $false
    }

    $metrics = @(
        @{ Name = "sourceCoverage"; Label = "coverage"; Direction = "higher" },
        @{ Name = "chains"; Label = "chains"; Direction = "neutral" },
        @{ Name = "sourceOpeners"; Label = "source openers"; Direction = "lower" },
        @{ Name = "sourceEdgeHeads"; Label = "edge heads"; Direction = "lower" },
        @{ Name = "avgChoices"; Label = "avg choices"; Direction = "lower" },
        @{ Name = "maxChoices"; Label = "max choices"; Direction = "lower" },
        @{ Name = "localPatchRun"; Label = "local patch run"; Direction = "lower" },
        @{ Name = "nearOuterRun"; Label = "near outer run"; Direction = "lower" },
        @{ Name = "directionalRisk"; Label = "directional sweep risk"; Direction = "lower" },
        @{ Name = "stripeRisk"; Label = "stripe visual risk"; Direction = "lower" },
        @{ Name = "solveTraceQualityScore"; Label = "solve trace quality"; Direction = "higher" },
        @{ Name = "solveTraceCollapseRiskScore"; Label = "solve collapse risk"; Direction = "lower" },
        @{ Name = "solveAxisDriftRate"; Label = "axis drift rate"; Direction = "higher" },
        @{ Name = "solveSameAxisRunMax"; Label = "same axis run"; Direction = "lower" },
        @{ Name = "solveSameDirHeadRunMax"; Label = "same dir run"; Direction = "lower" },
        @{ Name = "solveRegionEntropy"; Label = "region entropy"; Direction = "higher" },
        @{ Name = "solveRegionCollapseRunMax"; Label = "region collapse run"; Direction = "lower" },
        @{ Name = "solveFrontWidthAvg"; Label = "front width avg"; Direction = "higher" },
        @{ Name = "dependencyLocalSameRegionRate"; Label = "dependency local same-region"; Direction = "lower" },
        @{ Name = "dependencyBraidScore"; Label = "dependency braid"; Direction = "higher" }
    )

    $comparisonRows = foreach ($metric in $metrics) {
        $name = $metric.Name
        $nutValues = @($nutationRows | ForEach-Object { To-Double (Get-PropertyValue $_ @($name) "") 0.0 })
        $psgValues = @($psgRows | ForEach-Object { To-Double (Get-PropertyValue $_ @($name) "") 0.0 })
        if ($nutValues.Count -eq 0 -or $psgValues.Count -eq 0) {
            continue
        }

        $nutAvg = ($nutValues | Measure-Object -Average).Average
        $psgAvg = ($psgValues | Measure-Object -Average).Average
        $delta = $nutAvg - $psgAvg
        $direction = $metric.Direction
        $better = if ($direction -eq "higher") {
            $delta -gt 0.0001
        } elseif ($direction -eq "lower") {
            $delta -lt -0.0001
        } else {
            $false
        }

        [pscustomobject]@{
            metric = $name
            label = $metric.Label
            direction = $direction
            psgAvg = [Math]::Round($psgAvg, 4)
            nutationAvg = [Math]::Round($nutAvg, 4)
            delta = [Math]::Round($delta, 4)
            nutationBetter = $better
        }
    }

    $comparisonRows | Export-Csv -LiteralPath $OutputCsv -NoTypeInformation -Encoding UTF8

    $nutKeep = @($nutationRows | Where-Object { $_.rankClass -eq "TraceOrderKeep" }).Count
    $psgKeep = @($psgRows | Where-Object { $_.rankClass -eq "TraceOrderKeep" }).Count
    $nutStyle = (@($nutationRows | Group-Object styleFamily | Sort-Object Name | ForEach-Object { "$($_.Name)=$($_.Count)" }) -join ", ")
    $psgStyle = (@($psgRows | Group-Object styleFamily | Sort-Object Name | ForEach-Object { "$($_.Name)=$($_.Count)" }) -join ", ")
    $nutFlow = (@($nutationRows | Group-Object flowLanguage | Sort-Object Name | ForEach-Object { "$($_.Name)=$($_.Count)" }) -join ", ")
    $psgFlow = (@($psgRows | Group-Object flowLanguage | Sort-Object Name | ForEach-Object { "$($_.Name)=$($_.Count)" }) -join ", ")

    $lines = @()
    $lines += "# Nutation Peel V1 vs PSG Trial"
    $lines += ""
    $lines += "Generated: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')"
    $lines += ""
    $lines += "- Nutation joined: $NutationJoinedCsv"
    $lines += "- PSG joined: $PsgJoinedCsv"
    $lines += "- Nutation rows: $($nutationRows.Count), TraceOrderKeep: $nutKeep"
    $lines += "- PSG rows: $($psgRows.Count), TraceOrderKeep: $psgKeep"
    $lines += "- Nutation style mix: $nutStyle"
    $lines += "- PSG style mix: $psgStyle"
    $lines += "- Nutation flow mix: $nutFlow"
    $lines += "- PSG flow mix: $psgFlow"
    $lines += ""
    $lines += "## Metric Deltas"
    foreach ($row in $comparisonRows) {
        $sign = if ([double]$row.delta -gt 0) { "+" } else { "" }
        $mark = if ($row.nutationBetter -eq $true) { "better" } elseif ($row.direction -eq "neutral") { "neutral" } else { "watch" }
        $lines += "- $($row.label): PSG=$($row.psgAvg), Nutation=$($row.nutationAvg), delta=$sign$($row.delta) ($mark)"
    }

    $lines | Set-Content -LiteralPath $SummaryPath -Encoding UTF8
    return $true
}

$runRoot = Join-Path $ProjectRoot ".codex-run"
Ensure-Directory $runRoot

$unityLog = Join-Path $runRoot "$OutputPrefix`_unity.log"
$reportCsv = Join-Path $ProjectRoot "Assets\ArrowMagic\SOData\Reports\DirectProcedural\nutation_peel_v1_report.csv"
$traceInputCsv = Join-Path $runRoot "$OutputPrefix`_trace_input.csv"
$traceScript = Join-Path $TraceToolRoot "Tools\SGPRhythmLab\Build-SGPRhythmTrace.ps1"
$joinScript = Join-Path $ProjectRoot "Tools\Production\Join-SGPPressureTraceMetrics.ps1"
$traceReportDir = Join-Path $TraceToolRoot "Assets\ArrowMagic\SOData\Reports\SGPRhythmLab"
$traceMetricsCsv = Join-Path $traceReportDir "$OutputPrefix`_metrics.csv"
$traceJoinedCsv = Join-Path $runRoot "$OutputPrefix`_trace_joined.csv"
$traceJoinedSummary = Join-Path $runRoot "$OutputPrefix`_trace_joined_summary.md"
$traceBestPerSlotCsv = Join-Path $runRoot "$OutputPrefix`_trace_best_per_slot.csv"
$traceProductionKeepCsv = Join-Path $runRoot "$OutputPrefix`_production_keep.csv"
$projectProductionKeepCsv = Join-Path $ProjectRoot "Assets\ArrowMagic\SOData\Reports\DirectProcedural\nutation_peel_v1_production_keep.csv"

if (-not $SkipUnity) {
    if (-not (Test-Path -LiteralPath $UnityExe)) {
        throw "Unity executable not found: $UnityExe"
    }

    & $UnityExe `
        -batchmode `
        -quit `
        -projectPath $ProjectRoot `
        -executeMethod PixelBug.ArrowMagic.EditorTools.NoMaskProceduralGenerator.BuildNutationPeelV1Pack `
        -logFile $unityLog

    $deadline = (Get-Date).AddSeconds([Math]::Max(1, $UnityWaitSeconds))
    $unitySucceeded = $false
    do {
        if (Test-Path -LiteralPath $unityLog) {
            $tail = Get-Content -LiteralPath $unityLog -Tail 100 -ErrorAction SilentlyContinue
            if (($tail -join "`n") -match "Nutation Peel V1 finished") {
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
    -SourceName "NutationPeelV1"

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

Join-SourceAndTrace `
    -JoinScript $joinScript `
    -SourceCsv $reportCsv `
    -TraceMetricsCsv $traceMetricsCsv `
    -JoinedCsv $traceJoinedCsv `
    -SummaryPath $traceJoinedSummary `
    -BestPerSlotCsv $traceBestPerSlotCsv `
    -ProductionKeepCsv $traceProductionKeepCsv `
    -Mode $ProductionKeepMode `
    -MinRows $MinProductionKeepRows

$psgJoinedCsv = ""
$compareCsv = ""
$compareSummary = ""
if (-not $SkipPsgCompare) {
    $psgReportCsv = Join-Path $ProjectRoot "Assets\ArrowMagic\SOData\Reports\DirectProcedural\sgp_pressure_hard_trial_report.csv"
    $psgTraceInputCsv = Join-Path $runRoot "$PsgComparePrefix`_trace_input.csv"
    $psgTraceMetricsCsv = Join-Path $traceReportDir "$PsgComparePrefix`_metrics.csv"
    $psgJoinedCsv = Join-Path $runRoot "$PsgComparePrefix`_trace_joined.csv"
    $psgSummary = Join-Path $runRoot "$PsgComparePrefix`_trace_joined_summary.md"
    $psgBestPerSlot = Join-Path $runRoot "$PsgComparePrefix`_trace_best_per_slot.csv"
    $psgKeep = Join-Path $runRoot "$PsgComparePrefix`_production_keep.csv"

    Export-TraceInputFromSourceReport `
        -ReportCsv $psgReportCsv `
        -TraceInputCsv $psgTraceInputCsv `
        -SourceName "PSGPressureHardTrialBaseline" | Out-Null

    if (-not $SkipTrace) {
        & $traceScript `
            -SourceRoot $ProjectRoot `
            -OutputRoot $TraceToolRoot `
            -InputCsv $psgTraceInputCsv `
            -OutputPrefix $PsgComparePrefix `
            -MaxLevels $TraceMaxLevels
    }

    Join-SourceAndTrace `
        -JoinScript $joinScript `
        -SourceCsv $psgReportCsv `
        -TraceMetricsCsv $psgTraceMetricsCsv `
        -JoinedCsv $psgJoinedCsv `
        -SummaryPath $psgSummary `
        -BestPerSlotCsv $psgBestPerSlot `
        -ProductionKeepCsv $psgKeep `
        -Mode $ProductionKeepMode `
        -MinRows 0

    $compareCsv = Join-Path $runRoot "$OutputPrefix`_vs_psg_metrics.csv"
    $compareSummary = Join-Path $runRoot "$OutputPrefix`_vs_psg_summary.md"
    Write-NutationVsPsgSummary `
        -NutationJoinedCsv $traceJoinedCsv `
        -PsgJoinedCsv $psgJoinedCsv `
        -OutputCsv $compareCsv `
        -SummaryPath $compareSummary | Out-Null
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
    psgJoinedCsv = $psgJoinedCsv
    compareCsv = $compareCsv
    compareSummary = $compareSummary
    skippedUnity = [bool]$SkipUnity
    skippedTrace = [bool]$SkipTrace
    skippedPsgCompare = [bool]$SkipPsgCompare
} | Format-List
