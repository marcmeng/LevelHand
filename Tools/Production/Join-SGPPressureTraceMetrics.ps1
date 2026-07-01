param(
    [Parameter(Mandatory = $true)]
    [string]$SourceCsv,
    [Parameter(Mandatory = $true)]
    [string]$TraceMetricsCsv,
    [Parameter(Mandatory = $true)]
    [string]$OutputCsv,
    [string]$SummaryPath = "",
    [string]$BestPerSlotCsv = "",
    [string]$ProductionKeepCsv = "",
    [ValidateSet("TraceOrderPreferred", "TraceOrderRequired", "VisualOnly")]
    [string]$ProductionKeepMode = "TraceOrderPreferred",
    [int]$MinProductionKeepRows = 0,
    [string]$SlotField = "",
    [double]$MinCoverage = 0.97,
    [string[]]$AllowedProcessTiers = @("S", "A", "B"),
    [double]$MaxChoices = 10,
    [double]$MaxDirectionalSweepRisk = 0.32,
    [double]$MaxStripeVisualRisk = 0.18,
    [int]$MaxLocalPatchSolveRun = 8,
    [int]$MaxNearOuterPatchSolveRun = 5,
    [double]$MinSolveTraceQuality = 0.72,
    [double]$MaxSolveTraceCollapseRisk = 0.35,
    [int]$MaxSolveSameAxisRun = 8,
    [int]$MaxSolveSameDirHeadRun = 7,
    [double]$MaxDependencyLocalSameRegionRate = 0.65,
    [switch]$EnableProductionDiversity,
    [int]$MaxProductionKeepRows = 0,
    [int]$MaxProductionKeepPerStyleFamily = 0,
    [int]$MaxProductionKeepPerFlowLanguage = 0,
    [int]$MaxProductionKeepPerChainLanguage = 0,
    [int]$MaxProductionKeepPerStyleFlow = 0,
    [int]$MaxProductionKeepPerStyleSignature = 0,
    [int]$MaxProductionKeepHighRiskRows = 0,
    [switch]$StrictProductionDiversity
)

$ErrorActionPreference = "Stop"

function Ensure-Directory([string]$path) {
    if ([string]::IsNullOrWhiteSpace($path)) { return }
    if (-not (Test-Path -LiteralPath $path)) {
        New-Item -ItemType Directory -Path $path -Force | Out-Null
    }
}

function Write-TextWithFallback([string]$path, [string[]]$lines) {
    $lastError = $null
    for ($attempt = 1; $attempt -le 5; $attempt++) {
        try {
            $lines | Set-Content -LiteralPath $path -Encoding UTF8
            return $path
        } catch {
            $lastError = $_
            Start-Sleep -Milliseconds (150 * $attempt)
        }
    }

    $dir = Split-Path -Parent $path
    $name = [IO.Path]::GetFileNameWithoutExtension($path)
    $ext = [IO.Path]::GetExtension($path)
    if ([string]::IsNullOrWhiteSpace($ext)) { $ext = ".txt" }
    $fallback = Join-Path $dir ("{0}_{1}{2}" -f $name, (Get-Date -Format "yyyyMMddHHmmss"), $ext)
    $lines | Set-Content -LiteralPath $fallback -Encoding UTF8
    Write-Warning "Could not write summary '$path' after retries. Wrote '$fallback' instead. Last error: $($lastError.Exception.Message)"
    return $fallback
}

function Clamp01([double]$value) {
    return [Math]::Max(0.0, [Math]::Min(1.0, $value))
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

function Has-PropertyValue($row, [string[]]$names) {
    $value = Get-PropertyValue $row $names ""
    return -not [string]::IsNullOrWhiteSpace([string]$value)
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

function To-Int([object]$value, [int]$fallback = 0) {
    return [int][Math]::Round((To-Double $value $fallback))
}

function To-Bool([object]$value) {
    $text = ([string]$value).Trim()
    return $text -ieq "true" -or $text -eq "1" -or $text -ieq "yes"
}

function Get-SlotValue($row, [int]$fallback) {
    if (-not [string]::IsNullOrWhiteSpace($SlotField)) {
        $slotFromField = Get-PropertyValue $row @($SlotField) ""
        if (-not [string]::IsNullOrWhiteSpace([string]$slotFromField)) { return [string]$slotFromField }
    }

    $order = Get-PropertyValue $row @("Order", "order") ""
    if (-not [string]::IsNullOrWhiteSpace([string]$order)) { return [string]$order }

    $index = Get-PropertyValue $row @("Index", "index", "Row", "row") ""
    if (-not [string]::IsNullOrWhiteSpace([string]$index)) { return [string]$index }

    $spec = [string](Get-PropertyValue $row @("SpecId", "Type", "source") "")
    if ($spec -match "selected_slot(?<slot>\d+)") { return $Matches.slot }

    return [string]$fallback
}

function Get-StartSideHint([string]$side, [int]$run) {
    if ($run -le 0 -or [string]::IsNullOrWhiteSpace($side) -or $side -eq "I") { return "" }
    switch ($side) {
        "L" { return "left-edge inward / starts around left side" }
        "R" { return "right-edge inward / starts around right side" }
        "T" { return "top-edge inward / starts around top side" }
        "B" { return "bottom-edge inward / starts around bottom side" }
        default { return "" }
    }
}

function Get-RankClass([bool]$processKeep, [bool]$visualPass, [bool]$stsKeep) {
    if ($stsKeep) { return "TraceOrderKeep" }
    if ($visualPass) { return "VisualKeep" }
    if ($processKeep) { return "ProcessKeep" }
    return "Reject"
}

function Add-UniqueTag($tags, [string]$tag) {
    if ([string]::IsNullOrWhiteSpace($tag)) { return }
    if (-not ($tags -contains $tag)) {
        $tags.Add($tag) | Out-Null
    }
}

function Join-Tags($tags) {
    return (@($tags.ToArray()) | Where-Object { -not [string]::IsNullOrWhiteSpace([string]$_) } | Select-Object -Unique) -join ";"
}

function Get-StyleFamily([string]$specId, [string]$type, [string]$styleHint, [string]$profileHint) {
    $haystack = ("$specId $type $styleHint $profileHint").ToLowerInvariant()
    if ($haystack -match "peel[_-]?rail|rail[_-]?peel") { return "peel_layered" }
    if ($haystack -match "nutation[_-]?peel|peel[_-]?layer|layered[_-]?peel") { return "peel_layered" }
    if ($haystack -match "lock[_-]?buckle|key[_-]?door|center[_-]?cross|core[_-]?lock") { return "lock_buckle" }
    if ($haystack -match "section[_-]?unlock|four[_-]?pockets|section") { return "section_unlock" }
    if ($haystack -match "maze[_-]?long[_-]?chain|long[_-]?corridor|maze") { return "maze_long_chain" }
    if ($haystack -match "dense[_-]?weave|dense[_-]?kernel|dense") { return "dense_weave" }
    if ($haystack -match "core[_-]?burst|vertical[_-]?gate") { return "core_burst" }
    if ($haystack -match "dual[_-]?zone|double[_-]?room|dual") { return "dual_zone" }
    if ($haystack -match "stair[_-]?push|stair[_-]?ladder|stair") { return "stair_push" }
    if ($haystack -match "quasi[_-]?symmetry|symmetry") { return "quasi_symmetry" }
    if ($haystack -match "outer[_-]?shell|double[_-]?shell|shell[_-]?peel|shell") { return "outer_shell" }
    if ($haystack -match "sweep|zig[_-]?river|snake[_-]?spine") { return "sweep" }
    return "mixed_unknown"
}

function Get-GeneratorVariant([string]$sourceBatch, [string]$specId, [string]$sourceStatus) {
    $haystack = ("$sourceBatch $specId $sourceStatus").ToLowerInvariant()
    if ($haystack -match "peel[_-]?rail|rail[_-]?peel") { return "peel_rail_v1" }
    if ($haystack -match "nutation[_-]?peel|peel[_-]?layer|layered[_-]?peel") { return "nutation_peel_v1" }
    if ($haystack -match "interfere[_-]?v3|region[_-]?flow|headflow") { return "region_flow_v3" }
    if ($haystack -match "interfere[_-]?v2|flow[_-]?spread|flowrun|flowjump") { return "flow_spread_v2" }
    if ($haystack -match "interfere|neardir") { return "near_dir_interference" }
    if ($haystack -match "review") { return "review_baseline" }
    if ($haystack -match "trial") { return "trial_baseline" }
    return "unknown_variant"
}

function Get-GeneratorGrammar([string]$specId, [string]$sourceStatus) {
    $haystack = ("$specId $sourceStatus").ToLowerInvariant()
    if ($haystack -match "peel style|rectangle[_-]?peel|_rect_|rect_") { return "rectangle_peel" }
    if ($haystack -match "sgp") { return "sgp_fill" }
    return "unknown_grammar"
}

function Get-ChainLanguage(
    [string]$styleFamily,
    [double]$avgChain,
    [double]$maxChain,
    [double]$straightness,
    [int]$chains,
    [int]$shortChains,
    [int]$sourceEdgeHeads,
    [double]$straightLikeChainRate,
    [double]$longLowTurnChainRate,
    [double]$outerLongStraightChainRate
) {
    $tags = New-Object System.Collections.Generic.List[string]
    $shortRate = if ($chains -gt 0) { $shortChains / [double]$chains } else { 0.0 }
    $edgeHeadRate = if ($chains -gt 0) { $sourceEdgeHeads / [double]$chains } else { 0.0 }

    if ($styleFamily -eq "maze_long_chain" -or $avgChain -ge 10.8 -or $maxChain -ge 22) { Add-UniqueTag $tags "long_chain" }
    if ($maxChain -ge 24) { Add-UniqueTag $tags "long_spine" }
    if ($straightness -ge 0.58 -or $straightLikeChainRate -ge 0.12 -or $longLowTurnChainRate -ge 0.08 -or $outerLongStraightChainRate -ge 0.05) { Add-UniqueTag $tags "straight_lane" }
    if ($shortRate -ge 0.28 -or ($avgChain -gt 0 -and $avgChain -le 9.2)) { Add-UniqueTag $tags "short_patchwork" }
    if ($chains -ge 56 -and $avgChain -ge 9.0 -and $avgChain -le 11.2 -and $straightness -lt 0.60) { Add-UniqueTag $tags "woven_medium" }
    if ($sourceEdgeHeads -ge 6 -or $edgeHeadRate -ge 0.08) { Add-UniqueTag $tags "edge_headed" }
    if ($styleFamily -eq "lock_buckle") { Add-UniqueTag $tags "lock_cluster" }
    if ($styleFamily -eq "core_burst") { Add-UniqueTag $tags "core_cluster" }
    if ($styleFamily -eq "dense_weave") { Add-UniqueTag $tags "dense_weave_chain" }
    if ($styleFamily -eq "peel_layered") { Add-UniqueTag $tags "peel_layer_chain" }
    if ($styleFamily -eq "outer_shell") { Add-UniqueTag $tags "outer_shell_chain" }

    $detail = "mixed_chain"
    if (($tags -contains "long_chain") -and ($tags -contains "straight_lane")) { $detail = "long_straight_chain" }
    elseif ($tags -contains "long_chain") { $detail = "long_chain" }
    elseif ($tags -contains "peel_layer_chain") { $detail = "peel_layer_chain" }
    elseif ($tags -contains "dense_weave_chain") { $detail = "woven_medium" }
    elseif ($tags -contains "lock_cluster") { $detail = "lock_cluster" }
    elseif ($tags -contains "core_cluster") { $detail = "core_cluster" }
    elseif ($tags -contains "short_patchwork") { $detail = "short_patchwork" }
    elseif ($tags -contains "edge_headed") { $detail = "outer_edge_chain" }
    elseif ($tags -contains "woven_medium") { $detail = "woven_medium" }
    elseif ($tags -contains "straight_lane") { $detail = "straight_lane" }

    # Keep the main language coarse. Fine labels remain in chainLanguageDetail/tags
    # so style topology and chain geometry do not explode into one-off lane names.
    $primary = "mixed_chain"
    if ($tags -contains "long_spine") { $primary = "spine_chain" }
    elseif ($tags -contains "straight_lane") { $primary = "rail_chain" }
    elseif ($tags -contains "woven_medium" -or $tags -contains "peel_layer_chain" -or $tags -contains "dense_weave_chain") { $primary = "curve_chain" }
    elseif ($tags -contains "lock_cluster" -or $tags -contains "core_cluster" -or $tags -contains "outer_shell_chain") { $primary = "hook_chain" }
    elseif ($tags -contains "short_patchwork") { $primary = "patch_chain" }

    [pscustomobject]@{
        Primary = $primary
        Detail = $detail
        Tags = Join-Tags $tags
        ShortChainRate = [Math]::Round($shortRate, 3)
        EdgeHeadRate = [Math]::Round($edgeHeadRate, 3)
    }
}

function Get-FlowLanguage(
    [bool]$stsPresent,
    [string]$generatorGrammar,
    [double]$solveTraceCollapseRisk,
    [double]$solveAxisDrift,
    [int]$solveSameAxisRun,
    [int]$solveSameDirRun,
    [double]$solveRegionEntropy,
    [int]$solveRegionCollapseRun,
    [double]$solveFrontWidthAvg,
    [double]$solveFrontWidthNarrowRate,
    [int]$edgeInwardRun,
    [double]$directionalRisk,
    [double]$stripeRisk,
    [int]$localRun,
    [double]$stageLockScore,
    [int]$lateRegionCount
) {
    $tags = New-Object System.Collections.Generic.List[string]
    if ($generatorGrammar -eq "rectangle_peel") { Add-UniqueTag $tags "peel_grammar" }
    if (-not $stsPresent) {
        return [pscustomobject]@{
            Primary = "unknown_flow"
            Tags = Join-Tags $tags
        }
    }

    if ($solveRegionEntropy -ge 0.98) { Add-UniqueTag $tags "multi_region" }
    if ($solveAxisDrift -ge 0.48) { Add-UniqueTag $tags "axis_drift" }
    if ($solveFrontWidthAvg -ge 3.5) { Add-UniqueTag $tags "wide_front" }
    if ($solveFrontWidthNarrowRate -le 0.10) { Add-UniqueTag $tags "stable_front" }
    if ($stageLockScore -ge 0.42 -or $lateRegionCount -ge 2) { Add-UniqueTag $tags "staged_unlock" }
    if ($edgeInwardRun -ge 2 -or $stripeRisk -ge 0.12) { Add-UniqueTag $tags "peel_visible" }
    if ($directionalRisk -ge 0.24 -or $solveSameAxisRun -ge 7 -or $solveSameDirRun -ge 6) { Add-UniqueTag $tags "sweep_pressure" }
    if ($solveTraceCollapseRisk -ge 0.35 -or $solveRegionCollapseRun -ge 6 -or $localRun -gt 8) { Add-UniqueTag $tags "collapse_risk" }

    $primary = "mixed_flow"
    if ($tags -contains "collapse_risk") { $primary = "local_collapse" }
    elseif ($directionalRisk -ge 0.32 -or $solveSameAxisRun -ge 9 -or $solveSameDirRun -ge 8) { $primary = "single_axis_sweep" }
    elseif ($tags -contains "staged_unlock") { $primary = "staged_unlock" }
    elseif ($solveRegionEntropy -ge 0.985 -and $solveAxisDrift -ge 0.50 -and $solveFrontWidthAvg -ge 3.4) { $primary = "region_alternating_flow" }
    elseif ($solveRegionEntropy -ge 0.97 -and $solveFrontWidthAvg -ge 3.2) { $primary = "flow_spread" }
    elseif ($tags -contains "peel_visible") { $primary = "peel" }

    [pscustomobject]@{
        Primary = $primary
        Tags = Join-Tags $tags
    }
}

function Get-RiskTags(
    [bool]$processKeep,
    [double]$traceAvgChoices,
    [double]$traceMaxChoices,
    [double]$maxChoicesLimit,
    [bool]$directionalRiskPresent,
    [double]$directionalRisk,
    [double]$maxDirectionalSweepRisk,
    [bool]$stripeRiskPresent,
    [double]$stripeRisk,
    [double]$maxStripeVisualRisk,
    [bool]$localRunPresent,
    [int]$localRun,
    [int]$maxLocalPatchSolveRun,
    [bool]$nearOuterRunPresent,
    [int]$nearOuterRun,
    [int]$maxNearOuterPatchSolveRun,
    [bool]$stsPresent,
    [double]$solveTraceCollapseRisk,
    [double]$maxSolveTraceCollapseRisk,
    [int]$solveSameAxisRun,
    [int]$maxSolveSameAxisRun,
    [int]$solveSameDirRun,
    [int]$maxSolveSameDirHeadRun,
    [int]$solveRegionCollapseRun,
    [double]$dependencyLocalSameRegionRate,
    [double]$maxDependencyLocalSameRegionRate,
    [int]$edgeInwardRun,
    [double]$straightLikeChainRate,
    [double]$longLowTurnChainRate
) {
    $tags = New-Object System.Collections.Generic.List[string]
    if (-not $processKeep) { Add-UniqueTag $tags "process_reject" }
    if ($traceMaxChoices -gt $maxChoicesLimit) { Add-UniqueTag $tags "high_choices" }
    elseif ($traceMaxChoices -ge ($maxChoicesLimit - 1)) { Add-UniqueTag $tags "choice_peak_watch" }
    if ($traceAvgChoices -gt 6.0) { Add-UniqueTag $tags "broad_choice_curve" }

    if ($directionalRiskPresent) {
        if ($directionalRisk -gt $maxDirectionalSweepRisk) { Add-UniqueTag $tags "directional_sweep" }
        elseif ($directionalRisk -ge 0.22) { Add-UniqueTag $tags "directional_sweep_watch" }
    }
    if ($stripeRiskPresent) {
        if ($stripeRisk -gt $maxStripeVisualRisk) { Add-UniqueTag $tags "stripe_visual" }
        elseif ($stripeRisk -ge 0.12) { Add-UniqueTag $tags "stripe_watch" }
    }
    if ($localRunPresent) {
        if ($localRun -gt $maxLocalPatchSolveRun) { Add-UniqueTag $tags "local_patch_run" }
        elseif ($localRun -ge 7) { Add-UniqueTag $tags "local_patch_watch" }
    }
    if ($nearOuterRunPresent) {
        if ($nearOuterRun -gt $maxNearOuterPatchSolveRun) { Add-UniqueTag $tags "near_outer_patch" }
        elseif ($nearOuterRun -ge 5) { Add-UniqueTag $tags "near_outer_watch" }
    }

    if ($stsPresent) {
        if ($solveTraceCollapseRisk -gt $maxSolveTraceCollapseRisk) { Add-UniqueTag $tags "solve_collapse" }
        elseif ($solveTraceCollapseRisk -ge 0.25) { Add-UniqueTag $tags "collapse_watch" }
        if ($solveSameAxisRun -gt $maxSolveSameAxisRun) { Add-UniqueTag $tags "same_axis_run" }
        elseif ($solveSameAxisRun -ge 7) { Add-UniqueTag $tags "same_axis_watch" }
        if ($solveSameDirRun -gt $maxSolveSameDirHeadRun) { Add-UniqueTag $tags "same_dir_run" }
        elseif ($solveSameDirRun -ge 6) { Add-UniqueTag $tags "same_dir_watch" }
        if ($solveRegionCollapseRun -ge 6) { Add-UniqueTag $tags "region_collapse_run" }
        if ($dependencyLocalSameRegionRate -gt $maxDependencyLocalSameRegionRate) { Add-UniqueTag $tags "dependency_local" }
        elseif ($dependencyLocalSameRegionRate -ge 0.55) { Add-UniqueTag $tags "dependency_local_watch" }
    }

    if ($edgeInwardRun -ge 3) { Add-UniqueTag $tags "edge_inward_sweep" }
    if ($straightLikeChainRate -ge 0.16) { Add-UniqueTag $tags "straight_visual" }
    if ($longLowTurnChainRate -ge 0.08) { Add-UniqueTag $tags "long_low_turn_visual" }

    $joined = Join-Tags $tags
    $band = "clean"
    if (-not $processKeep -or $joined -match "high_choices|directional_sweep(;|$)|stripe_visual|local_patch_run|near_outer_patch|solve_collapse|same_axis_run|same_dir_run|dependency_local(;|$)|region_collapse_run") {
        $band = "high_risk"
    } elseif (-not [string]::IsNullOrWhiteSpace($joined)) {
        $band = "watch"
    }

    [pscustomobject]@{
        Tags = $joined
        Band = $band
    }
}

function Get-DiversityValue($row, [string]$fieldName) {
    $value = [string](Get-PropertyValue $row @($fieldName) "")
    if ([string]::IsNullOrWhiteSpace($value)) { return "unknown" }
    return $value
}

function Get-StyleSignature($row) {
    $parts = @(
        (Get-DiversityValue $row "styleFamily"),
        (Get-DiversityValue $row "chainLanguage"),
        (Get-DiversityValue $row "flowLanguage"),
        ("{0}x{1}" -f (Get-DiversityValue $row "width"), (Get-DiversityValue $row "height")),
        ("chains={0}" -f (Get-DiversityValue $row "chains")),
        ("cov={0}" -f (Get-DiversityValue $row "sourceCoverage")),
        ("open={0}" -f (Get-DiversityValue $row "sourceOpeners")),
        ("edge={0}" -f (Get-DiversityValue $row "sourceEdgeHeads")),
        ("avg={0}" -f (Get-DiversityValue $row "avgChoices")),
        ("max={0}" -f (Get-DiversityValue $row "maxChoices")),
        ("local={0}" -f (Get-DiversityValue $row "localPatchRun")),
        ("near={0}" -f (Get-DiversityValue $row "nearOuterRun")),
        ("axis={0}" -f (Get-DiversityValue $row "solveSameAxisRunMax")),
        ("dir={0}" -f (Get-DiversityValue $row "solveSameDirHeadRunMax"))
    )
    return ($parts -join "|")
}

function Get-CountValue($counts, [string]$key) {
    if ($counts.ContainsKey($key)) { return [int]$counts[$key] }
    return 0
}

function Add-CountValue($counts, [string]$key) {
    $counts[$key] = (Get-CountValue $counts $key) + 1
}

function Get-DiversityRejectReasons(
    $row,
    $counts,
    [int]$maxRows,
    [int]$selectedCount,
    [int]$maxPerStyleFamily,
    [int]$maxPerFlowLanguage,
    [int]$maxPerChainLanguage,
    [int]$maxPerStyleFlow,
    [int]$maxPerStyleSignature,
    [int]$maxHighRiskRows
) {
    $reasons = New-Object System.Collections.Generic.List[string]
    if ($maxRows -gt 0 -and $selectedCount -ge $maxRows) { $reasons.Add("maxRows=$maxRows") | Out-Null }

    $style = Get-DiversityValue $row "styleFamily"
    $flow = Get-DiversityValue $row "flowLanguage"
    $chain = Get-DiversityValue $row "chainLanguage"
    $styleFlow = "$style|$flow"
    $signature = Get-StyleSignature $row
    $riskBand = Get-DiversityValue $row "styleRiskBand"

    if ($maxPerStyleFamily -gt 0 -and (Get-CountValue $counts "style:$style") -ge $maxPerStyleFamily) {
        $reasons.Add("styleFamily:$style>=$maxPerStyleFamily") | Out-Null
    }
    if ($maxPerFlowLanguage -gt 0 -and (Get-CountValue $counts "flow:$flow") -ge $maxPerFlowLanguage) {
        $reasons.Add("flowLanguage:$flow>=$maxPerFlowLanguage") | Out-Null
    }
    if ($maxPerChainLanguage -gt 0 -and (Get-CountValue $counts "chain:$chain") -ge $maxPerChainLanguage) {
        $reasons.Add("chainLanguage:$chain>=$maxPerChainLanguage") | Out-Null
    }
    if ($maxPerStyleFlow -gt 0 -and (Get-CountValue $counts "styleFlow:$styleFlow") -ge $maxPerStyleFlow) {
        $reasons.Add("styleFlow:$styleFlow>=$maxPerStyleFlow") | Out-Null
    }
    if ($maxPerStyleSignature -gt 0 -and (Get-CountValue $counts "signature:$signature") -ge $maxPerStyleSignature) {
        $reasons.Add("styleSignature>=$maxPerStyleSignature") | Out-Null
    }
    if ($maxHighRiskRows -gt 0 -and $riskBand -eq "high_risk" -and (Get-CountValue $counts "risk:high_risk") -ge $maxHighRiskRows) {
        $reasons.Add("highRisk>=$maxHighRiskRows") | Out-Null
    }

    return $reasons
}

function Add-DiversitySelection($row, $counts) {
    $style = Get-DiversityValue $row "styleFamily"
    $flow = Get-DiversityValue $row "flowLanguage"
    $chain = Get-DiversityValue $row "chainLanguage"
    $styleFlow = "$style|$flow"
    $signature = Get-StyleSignature $row
    $riskBand = Get-DiversityValue $row "styleRiskBand"

    Add-CountValue $counts "style:$style"
    Add-CountValue $counts "flow:$flow"
    Add-CountValue $counts "chain:$chain"
    Add-CountValue $counts "styleFlow:$styleFlow"
    Add-CountValue $counts "signature:$signature"
    if ($riskBand -eq "high_risk") { Add-CountValue $counts "risk:high_risk" }
}

function Select-DiverseProductionRows(
    [object[]]$rows,
    [int]$minRows,
    [bool]$enableDiversity,
    [bool]$strictDiversity,
    [int]$maxRows,
    [int]$maxPerStyleFamily,
    [int]$maxPerFlowLanguage,
    [int]$maxPerChainLanguage,
    [int]$maxPerStyleFlow,
    [int]$maxPerStyleSignature,
    [int]$maxHighRiskRows
) {
    $diversityRequested = $enableDiversity -or
        $maxRows -gt 0 -or
        $maxPerStyleFamily -gt 0 -or
        $maxPerFlowLanguage -gt 0 -or
        $maxPerChainLanguage -gt 0 -or
        $maxPerStyleFlow -gt 0 -or
        $maxPerStyleSignature -gt 0 -or
        $maxHighRiskRows -gt 0

    if (-not $diversityRequested) {
        return [pscustomobject]@{
            Rows = @($rows)
            Dropped = @()
            Backfilled = @()
            Enabled = $false
            Strict = $false
        }
    }

    $selected = New-Object System.Collections.Generic.List[object]
    $dropped = New-Object System.Collections.Generic.List[object]
    $counts = @{}

    foreach ($row in $rows) {
        $reasons = Get-DiversityRejectReasons `
            $row `
            $counts `
            $maxRows `
            $selected.Count `
            $maxPerStyleFamily `
            $maxPerFlowLanguage `
            $maxPerChainLanguage `
            $maxPerStyleFlow `
            $maxPerStyleSignature `
            $maxHighRiskRows

        if ($reasons.Count -eq 0) {
            $selected.Add($row) | Out-Null
            Add-DiversitySelection $row $counts
        } else {
            $dropped.Add([pscustomobject]@{
                row = $row
                reasons = ($reasons -join ";")
            }) | Out-Null
        }
    }

    $backfilled = New-Object System.Collections.Generic.List[object]
    if (-not $strictDiversity -and $minRows -gt 0 -and $selected.Count -lt $minRows) {
        foreach ($drop in $dropped.ToArray()) {
            if ($selected.Count -ge $minRows) { break }
            if ($maxRows -gt 0 -and $selected.Count -ge $maxRows) { break }
            $selected.Add($drop.row) | Out-Null
            $backfilled.Add($drop) | Out-Null
        }
    }

    return [pscustomobject]@{
        Rows = @($selected.ToArray())
        Dropped = @($dropped.ToArray())
        Backfilled = @($backfilled.ToArray())
        Enabled = $true
        Strict = $strictDiversity
    }
}

if (-not (Test-Path -LiteralPath $SourceCsv)) { throw "Missing source CSV: $SourceCsv" }
if (-not (Test-Path -LiteralPath $TraceMetricsCsv)) { throw "Missing trace metrics CSV: $TraceMetricsCsv" }

Ensure-Directory (Split-Path -Parent $OutputCsv)
if (-not [string]::IsNullOrWhiteSpace($SummaryPath)) { Ensure-Directory (Split-Path -Parent $SummaryPath) }
if (-not [string]::IsNullOrWhiteSpace($BestPerSlotCsv)) { Ensure-Directory (Split-Path -Parent $BestPerSlotCsv) }
if (-not [string]::IsNullOrWhiteSpace($ProductionKeepCsv)) { Ensure-Directory (Split-Path -Parent $ProductionKeepCsv) }

$sourceRows = @(Import-Csv -LiteralPath $SourceCsv | Where-Object {
    -not [string]::IsNullOrWhiteSpace([string](Get-PropertyValue $_ @("LevelId", "levelId") "")) -and
    -not [string]::IsNullOrWhiteSpace([string](Get-PropertyValue $_ @("AssetPath", "path", "assetPath") ""))
})
$traceRows = @(Import-Csv -LiteralPath $TraceMetricsCsv)

if ($sourceRows.Count -eq 0) { throw "No traceable source rows in $SourceCsv" }
if ($traceRows.Count -eq 0) { throw "No trace rows in $TraceMetricsCsv" }

$sourceByLevelId = @{}
foreach ($row in $sourceRows) {
    $levelId = [string](Get-PropertyValue $row @("LevelId", "levelId") "")
    if (-not [string]::IsNullOrWhiteSpace($levelId)) {
        $sourceByLevelId[$levelId] = $row
    }
}

$joined = New-Object System.Collections.Generic.List[object]
$ordinal = 0
foreach ($trace in $traceRows) {
    $levelId = [string](Get-PropertyValue $trace @("levelId", "LevelId") "")
    if ([string]::IsNullOrWhiteSpace($levelId) -or -not $sourceByLevelId.ContainsKey($levelId)) { continue }

    $ordinal++
    $source = $sourceByLevelId[$levelId]
    $slot = Get-SlotValue $source $ordinal
    $specId = [string](Get-PropertyValue $source @("SpecId", "Type", "source") "")
    $sourceBatch = [string](Get-PropertyValue $source @("SourceBatch", "sourceBatch", "source") "")
    $sourceType = [string](Get-PropertyValue $source @("Type") "")
    $styleHint = [string](Get-PropertyValue $source @("StyleHint", "styleHint") "")
    $profileHint = [string](Get-PropertyValue $source @("ProfileHint", "profileHint") "")
    $sourceStatus = [string](Get-PropertyValue $source @("Status", "status") "")
    $assetPath = [string](Get-PropertyValue $source @("AssetPath", "path", "assetPath") "")
    $coverage = To-Double (Get-PropertyValue $source @("Coverage", "coverage", "sourceCoverage") "") 0.0
    $sourceChains = To-Int (Get-PropertyValue $source @("Chains", "chains") "") 0
    $sourceOpeners = To-Int (Get-PropertyValue $source @("PortableOpeners", "portableOpeners", "InitialMovableChains") "") 0
    $sourceEdgeHeads = To-Int (Get-PropertyValue $source @("EdgeHeadChains", "edgeHeadChains") "") 0
    $sourceShortChains = To-Int (Get-PropertyValue $source @("ShortChains", "shortChains") "") 0
    $sourceAvgChain = To-Double (Get-PropertyValue $source @("AvgChain", "avgChain") "") 0.0
    $sourceMaxChain = To-Double (Get-PropertyValue $source @("MaxChain", "maxChain") "") 0.0
    $sourceStraightness = To-Double (Get-PropertyValue $source @("Straightness", "straightness") "") 0.0

    $solved = To-Bool (Get-PropertyValue $trace @("solved") "")
    $tier = [string](Get-PropertyValue $trace @("processTier") "")
    $traceMaxChoices = To-Double (Get-PropertyValue $trace @("maxChoices") "") 9999.0
    $traceAvgChoices = To-Double (Get-PropertyValue $trace @("avgChoices") "") 9999.0
    $localRunPresent = Has-PropertyValue $trace @("localPatchSolveRunMax")
    $nearOuterRunPresent = Has-PropertyValue $trace @("nearOuterPatchSolveRunMax")
    $directionalRiskPresent = Has-PropertyValue $trace @("directionalSweepRiskScore")
    $stripeRiskPresent = Has-PropertyValue $trace @("stripeVisualRiskScore")
    $localRun = To-Int (Get-PropertyValue $trace @("localPatchSolveRunMax") "") 0
    $nearOuterRun = To-Int (Get-PropertyValue $trace @("nearOuterPatchSolveRunMax") "") 0
    $directionalRisk = To-Double (Get-PropertyValue $trace @("directionalSweepRiskScore") "") 0.0
    $stripeRisk = To-Double (Get-PropertyValue $trace @("stripeVisualRiskScore") "") 0.0
    $dependencyFollowRun = To-Int (Get-PropertyValue $trace @("dependencyFollowRunMax") "") 9999
    $edgeInwardRun = To-Int (Get-PropertyValue $trace @("edgeInwardSweepRunMax") "") 0
    $edgeInwardSide = [string](Get-PropertyValue $trace @("edgeInwardSweepSide") "")
    $straightLikeChainRate = To-Double (Get-PropertyValue $trace @("straightLikeChainRate") "") 0.0
    $longLowTurnChainRate = To-Double (Get-PropertyValue $trace @("longLowTurnChainRate") "") 0.0
    $outerLongStraightChainRate = To-Double (Get-PropertyValue $trace @("outerLongStraightChainRate") "") 0.0

    $stsPresent = Has-PropertyValue $trace @("solveTraceQualityScore")
    $solveTraceQuality = To-Double (Get-PropertyValue $trace @("solveTraceQualityScore") "") 0.0
    $solveTraceCollapseRisk = To-Double (Get-PropertyValue $trace @("solveTraceCollapseRiskScore") "") 0.0
    $solveAxisDrift = To-Double (Get-PropertyValue $trace @("solveAxisDriftRate") "") 0.0
    $solveSameAxisRun = To-Int (Get-PropertyValue $trace @("solveSameAxisRunMax") "") 0
    $solveSameDirRun = To-Int (Get-PropertyValue $trace @("solveSameDirHeadRunMax") "") 0
    $solveRegionEntropy = To-Double (Get-PropertyValue $trace @("solveRegionEntropy") "") 0.0
    $solveRegionCollapseRun = To-Int (Get-PropertyValue $trace @("solveRegionCollapseRunMax") "") 0
    $solveFrontWidthAvg = To-Double (Get-PropertyValue $trace @("solveFrontWidthAvg") "") 0.0
    $solveFrontWidthNarrowRate = To-Double (Get-PropertyValue $trace @("solveFrontWidthNarrowRate") "") 0.0
    $dependencyRegionEntropy = To-Double (Get-PropertyValue $trace @("dependencyRegionEntropy") "") 0.0
    $dependencyLocalSameRegionRate = To-Double (Get-PropertyValue $trace @("dependencyLocalSameRegionRate") "") 0.0
    $dependencySameAxisHeadRate = To-Double (Get-PropertyValue $trace @("dependencySameAxisHeadRate") "") 0.0
    $dependencySameDirHeadRate = To-Double (Get-PropertyValue $trace @("dependencySameDirHeadRate") "") 0.0
    $dependencyBraidScore = To-Double (Get-PropertyValue $trace @("dependencyBraidScore") "") 0.0
    $stageLockScore = To-Double (Get-PropertyValue $trace @("stageLockScore") "") 0.0
    $lateRegionCount = To-Int (Get-PropertyValue $trace @("lateRegionCount") "") 0

    $processReasons = New-Object System.Collections.Generic.List[string]
    if (-not $solved) { $processReasons.Add("unsolved") | Out-Null }
    if ($coverage -lt $MinCoverage) { $processReasons.Add("coverage<$MinCoverage") | Out-Null }
    if (-not ($AllowedProcessTiers -contains $tier)) { $processReasons.Add("tier=$tier") | Out-Null }
    if ($traceMaxChoices -gt $MaxChoices) { $processReasons.Add("maxChoices>$MaxChoices") | Out-Null }
    $processKeep = $processReasons.Count -eq 0

    $visualReasons = New-Object System.Collections.Generic.List[string]
    if ($directionalRiskPresent -and $directionalRisk -gt $MaxDirectionalSweepRisk) { $visualReasons.Add("directionalRisk>$MaxDirectionalSweepRisk") | Out-Null }
    if ($stripeRiskPresent -and $stripeRisk -gt $MaxStripeVisualRisk) { $visualReasons.Add("stripeRisk>$MaxStripeVisualRisk") | Out-Null }
    if ($localRunPresent -and $localRun -gt $MaxLocalPatchSolveRun) { $visualReasons.Add("localPatchRun>$MaxLocalPatchSolveRun") | Out-Null }
    if ($nearOuterRunPresent -and $nearOuterRun -gt $MaxNearOuterPatchSolveRun) { $visualReasons.Add("nearOuterRun>$MaxNearOuterPatchSolveRun") | Out-Null }
    $visualPass = $processKeep -and $visualReasons.Count -eq 0

    $visualHardReasons = New-Object System.Collections.Generic.List[string]
    $hardDirectionalLimit = [Math]::Max($MaxDirectionalSweepRisk * 1.35, $MaxDirectionalSweepRisk + 0.10)
    $hardStripeLimit = [Math]::Max($MaxStripeVisualRisk * 1.60, $MaxStripeVisualRisk + 0.08)
    $hardLocalRunLimit = $MaxLocalPatchSolveRun + 2
    $hardNearOuterRunLimit = $MaxNearOuterPatchSolveRun + 2
    if ($directionalRiskPresent -and $directionalRisk -gt $hardDirectionalLimit) { $visualHardReasons.Add("directionalRiskHard>$([Math]::Round($hardDirectionalLimit, 3))") | Out-Null }
    if ($stripeRiskPresent -and $stripeRisk -gt $hardStripeLimit) { $visualHardReasons.Add("stripeRiskHard>$([Math]::Round($hardStripeLimit, 3))") | Out-Null }
    if ($localRunPresent -and $localRun -gt $hardLocalRunLimit) { $visualHardReasons.Add("localPatchRunHard>$hardLocalRunLimit") | Out-Null }
    if ($nearOuterRunPresent -and $nearOuterRun -gt $hardNearOuterRunLimit) { $visualHardReasons.Add("nearOuterRunHard>$hardNearOuterRunLimit") | Out-Null }

    $stsReasons = New-Object System.Collections.Generic.List[string]
    if (-not $stsPresent) {
        $stsReasons.Add("stsMissing") | Out-Null
    } else {
        if ($solveTraceQuality -lt $MinSolveTraceQuality) { $stsReasons.Add("solveTraceQuality<$MinSolveTraceQuality") | Out-Null }
        if ($solveTraceCollapseRisk -gt $MaxSolveTraceCollapseRisk) { $stsReasons.Add("solveTraceCollapseRisk>$MaxSolveTraceCollapseRisk") | Out-Null }
        if ($solveSameAxisRun -gt $MaxSolveSameAxisRun) { $stsReasons.Add("sameAxisRun>$MaxSolveSameAxisRun") | Out-Null }
        if ($solveSameDirRun -gt $MaxSolveSameDirHeadRun) { $stsReasons.Add("sameDirRun>$MaxSolveSameDirHeadRun") | Out-Null }
        if ($dependencyLocalSameRegionRate -gt $MaxDependencyLocalSameRegionRate) { $stsReasons.Add("dependencyLocalSameRegion>$MaxDependencyLocalSameRegionRate") | Out-Null }
    }
    $stsPass = $stsPresent -and $stsReasons.Count -eq 0
    $keepCandidate = $visualPass
    $stsKeepCandidate = $processKeep -and $stsPass -and $visualHardReasons.Count -eq 0

    $coverageScore = Clamp01 (($coverage - $MinCoverage) / 0.03)
    $choiceScore = Clamp01 (
        (Clamp01 (1.0 - ([Math]::Max(0.0, $traceMaxChoices - 6.0) / 6.0))) * 0.58 +
        (Clamp01 (1.0 - ([Math]::Max(0.0, $traceAvgChoices - 4.0) / 4.0))) * 0.42)
    $directionalClean = if ($directionalRiskPresent) { Clamp01 (1.0 - ($directionalRisk / [Math]::Max(0.001, $MaxDirectionalSweepRisk))) } else { 0.5 }
    $stripeClean = if ($stripeRiskPresent) { Clamp01 (1.0 - ($stripeRisk / [Math]::Max(0.001, $MaxStripeVisualRisk))) } else { 0.5 }
    $localClean = if ($localRunPresent) { Clamp01 (1.0 - ([Math]::Max(0, $localRun - 3) / [double][Math]::Max(1, $MaxLocalPatchSolveRun - 3))) } else { 0.5 }
    $nearOuterClean = if ($nearOuterRunPresent) { Clamp01 (1.0 - ([Math]::Max(0, $nearOuterRun - 2) / [double][Math]::Max(1, $MaxNearOuterPatchSolveRun - 2))) } else { 0.5 }
    $visualScore = Clamp01 (
        $directionalClean * 0.32 +
        $stripeClean * 0.24 +
        $localClean * 0.24 +
        $nearOuterClean * 0.20)
    $stsRankScore = 0.5
    if ($stsPresent) {
        $axisClean = Clamp01 (1.0 - ([Math]::Max(0, $solveSameAxisRun - 4) / 6.0))
        $dirClean = Clamp01 (1.0 - ([Math]::Max(0, $solveSameDirRun - 3) / 6.0))
        $depClean = Clamp01 (1.0 - ($dependencyLocalSameRegionRate / [Math]::Max(0.001, $MaxDependencyLocalSameRegionRate)))
        $stsRankScore = Clamp01 (
            $solveTraceQuality * 0.34 +
            (Clamp01 (1.0 - $solveTraceCollapseRisk)) * 0.22 +
            $axisClean * 0.12 +
            $dirClean * 0.12 +
            $solveRegionEntropy * 0.08 +
            (Clamp01 ($solveFrontWidthAvg / 4.0)) * 0.06 +
            $depClean * 0.06)
    }
    $keepBonus = if ($stsKeepCandidate) { 2.0 } elseif ($keepCandidate) { 1.4 } elseif ($processKeep) { 0.7 } else { 0.0 }
    $rankScore = $keepBonus + $coverageScore * 0.18 + $choiceScore * 0.24 + $visualScore * 0.25 + $stsRankScore * 0.33

    $allRejectReasons = @()
    if (-not $processKeep) { $allRejectReasons += @($processReasons) }
    elseif ($stsPresent -and -not $stsKeepCandidate) {
        if (-not $stsPass) { $allRejectReasons += @($stsReasons) }
        if ($visualHardReasons.Count -gt 0) { $allRejectReasons += @($visualHardReasons) }
    }
    elseif (-not $visualPass) { $allRejectReasons += @($visualReasons) }

    $styleFamily = Get-StyleFamily $specId $sourceType $styleHint $profileHint
    $generatorVariant = Get-GeneratorVariant $sourceBatch $specId $sourceStatus
    $generatorGrammar = Get-GeneratorGrammar $specId $sourceStatus
    $chainLanguage = Get-ChainLanguage `
        $styleFamily `
        $sourceAvgChain `
        $sourceMaxChain `
        $sourceStraightness `
        $sourceChains `
        $sourceShortChains `
        $sourceEdgeHeads `
        $straightLikeChainRate `
        $longLowTurnChainRate `
        $outerLongStraightChainRate
    $flowLanguage = Get-FlowLanguage `
        $stsPresent `
        $generatorGrammar `
        $solveTraceCollapseRisk `
        $solveAxisDrift `
        $solveSameAxisRun `
        $solveSameDirRun `
        $solveRegionEntropy `
        $solveRegionCollapseRun `
        $solveFrontWidthAvg `
        $solveFrontWidthNarrowRate `
        $edgeInwardRun `
        $directionalRisk `
        $stripeRisk `
        $localRun `
        $stageLockScore `
        $lateRegionCount
    $riskTags = Get-RiskTags `
        $processKeep `
        $traceAvgChoices `
        $traceMaxChoices `
        $MaxChoices `
        $directionalRiskPresent `
        $directionalRisk `
        $MaxDirectionalSweepRisk `
        $stripeRiskPresent `
        $stripeRisk `
        $MaxStripeVisualRisk `
        $localRunPresent `
        $localRun `
        $MaxLocalPatchSolveRun `
        $nearOuterRunPresent `
        $nearOuterRun `
        $MaxNearOuterPatchSolveRun `
        $stsPresent `
        $solveTraceCollapseRisk `
        $MaxSolveTraceCollapseRisk `
        $solveSameAxisRun `
        $MaxSolveSameAxisRun `
        $solveSameDirRun `
        $MaxSolveSameDirHeadRun `
        $solveRegionCollapseRun `
        $dependencyLocalSameRegionRate `
        $MaxDependencyLocalSameRegionRate `
        $edgeInwardRun `
        $straightLikeChainRate `
        $longLowTurnChainRate

    $joined.Add([pscustomobject]@{
        rankClass = Get-RankClass $processKeep $visualPass $stsKeepCandidate
        psgRankScore = [Math]::Round($rankScore, 3)
        processKeep = $processKeep
        visualPass = $visualPass
        keepCandidate = $keepCandidate
        stsMetricPresent = $stsPresent
        stsPass = $stsPass
        stsKeepCandidate = $stsKeepCandidate
        rejectReasons = ($allRejectReasons -join ";")
        slot = $slot
        sourceIndex = Get-PropertyValue $source @("Index", "Row", "index", "row") ""
        order = Get-PropertyValue $source @("Order", "order") ""
        sourceBatch = $sourceBatch
        sourceReport = Get-PropertyValue $source @("SourceReport", "sourceReport") ""
        section10 = Get-PropertyValue $source @("Section10", "section10") ""
        mode = Get-PropertyValue $source @("Mode", "mode") ""
        styleHint = $styleHint
        profileHint = $profileHint
        type = $sourceType
        styleFamily = $styleFamily
        generatorVariant = $generatorVariant
        generatorGrammar = $generatorGrammar
        chainLanguage = $chainLanguage.Primary
        chainLanguageDetail = $chainLanguage.Detail
        chainTags = $chainLanguage.Tags
        flowLanguage = $flowLanguage.Primary
        flowTags = $flowLanguage.Tags
        riskTags = $riskTags.Tags
        styleRiskBand = $riskTags.Band
        specId = $specId
        levelId = $levelId
        assetPath = $assetPath
        path = $assetPath
        selected = 1
        width = Get-PropertyValue $source @("Width", "width") (Get-PropertyValue $trace @("width") "")
        height = Get-PropertyValue $source @("Height", "height") (Get-PropertyValue $trace @("height") "")
        sourceCoverage = [Math]::Round($coverage, 3)
        chains = $sourceChains
        sourceAvgChain = [Math]::Round($sourceAvgChain, 3)
        sourceMaxChain = [Math]::Round($sourceMaxChain, 3)
        sourceStraightness = [Math]::Round($sourceStraightness, 3)
        sourceShortChainRate = $chainLanguage.ShortChainRate
        sourceEdgeHeadRate = $chainLanguage.EdgeHeadRate
        sourceOpeners = $sourceOpeners
        sourceEdgeHeads = $sourceEdgeHeads
        solved = $solved
        processTier = $tier
        processTierReason = Get-PropertyValue $trace @("processTierReason") ""
        avgChoices = [Math]::Round($traceAvgChoices, 3)
        maxChoices = [Math]::Round($traceMaxChoices, 3)
        localPatchRun = if ($localRunPresent) { $localRun } else { "" }
        nearOuterRun = if ($nearOuterRunPresent) { $nearOuterRun } else { "" }
        dependencyFollowRun = $dependencyFollowRun
        directionalRisk = if ($directionalRiskPresent) { [Math]::Round($directionalRisk, 3) } else { "" }
        stripeRisk = if ($stripeRiskPresent) { [Math]::Round($stripeRisk, 3) } else { "" }
        edgeInwardSweepRun = $edgeInwardRun
        edgeInwardSweepSide = $edgeInwardSide
        startSideHint = Get-StartSideHint $edgeInwardSide $edgeInwardRun
        solveVectorRun = To-Int (Get-PropertyValue $trace @("solveVectorRunMax") "") 0
        solveTraceQualityScore = if ($stsPresent) { [Math]::Round($solveTraceQuality, 3) } else { "" }
        solveTraceCollapseRiskScore = if ($stsPresent) { [Math]::Round($solveTraceCollapseRisk, 3) } else { "" }
        solveAxisDriftRate = if ($stsPresent) { [Math]::Round($solveAxisDrift, 3) } else { "" }
        solveSameAxisRunMax = if ($stsPresent) { $solveSameAxisRun } else { "" }
        solveSameDirHeadRunMax = if ($stsPresent) { $solveSameDirRun } else { "" }
        solveRegionEntropy = if ($stsPresent) { [Math]::Round($solveRegionEntropy, 3) } else { "" }
        solveRegionCollapseRunMax = if ($stsPresent) { $solveRegionCollapseRun } else { "" }
        solveFrontWidthAvg = if ($stsPresent) { [Math]::Round($solveFrontWidthAvg, 3) } else { "" }
        solveFrontWidthNarrowRate = if ($stsPresent) { [Math]::Round($solveFrontWidthNarrowRate, 3) } else { "" }
        dependencyRegionEntropy = if ($stsPresent) { [Math]::Round($dependencyRegionEntropy, 3) } else { "" }
        dependencyLocalSameRegionRate = if ($stsPresent) { [Math]::Round($dependencyLocalSameRegionRate, 3) } else { "" }
        dependencySameAxisHeadRate = if ($stsPresent) { [Math]::Round($dependencySameAxisHeadRate, 3) } else { "" }
        dependencySameDirHeadRate = if ($stsPresent) { [Math]::Round($dependencySameDirHeadRate, 3) } else { "" }
        dependencyBraidScore = [Math]::Round($dependencyBraidScore, 3)
        hardStructureV3Class = Get-PropertyValue $trace @("hardStructureV3Class") ""
        sourceStatus = $sourceStatus
    }) | Out-Null
}

if ($joined.Count -eq 0) {
    throw "No joined rows. Check LevelId values in source and trace CSVs."
}

$ranked = @($joined.ToArray() | Sort-Object `
    @{ Expression = { if ($_.stsKeepCandidate) { 0 } elseif ($_.keepCandidate) { 1 } elseif ($_.processKeep) { 2 } else { 3 } } },
    @{ Expression = { -1.0 * [double]$_.psgRankScore } },
    slot,
    maxChoices,
    avgChoices,
    @{ Expression = { -1.0 * [double]$_.sourceCoverage } })

$ranked | Export-Csv -LiteralPath $OutputCsv -NoTypeInformation -Encoding UTF8

$bestRows = @()
if (-not [string]::IsNullOrWhiteSpace($BestPerSlotCsv)) {
    $bestRows = @($ranked | Group-Object slot | ForEach-Object { $_.Group | Select-Object -First 1 })
    $bestRows | Export-Csv -LiteralPath $BestPerSlotCsv -NoTypeInformation -Encoding UTF8
}

$stsMetricPresentRows = @($ranked | Where-Object { $_.stsMetricPresent }).Count
$productionKeepSource = switch ($ProductionKeepMode) {
    "TraceOrderRequired" { "TraceOrderKeep" }
    "VisualOnly" { "VisualKeep" }
    default {
        if ($stsMetricPresentRows -gt 0) { "TraceOrderKeep" } else { "VisualKeepFallbackNoSTS" }
    }
}

$productionKeepEligibleRows = @()
if ($productionKeepSource -eq "TraceOrderKeep") {
    $productionKeepEligibleRows = @($ranked | Where-Object { $_.stsKeepCandidate })
} else {
    $productionKeepEligibleRows = @($ranked | Where-Object { $_.keepCandidate })
}

$diversitySelection = Select-DiverseProductionRows `
    $productionKeepEligibleRows `
    $MinProductionKeepRows `
    ([bool]$EnableProductionDiversity) `
    ([bool]$StrictProductionDiversity) `
    $MaxProductionKeepRows `
    $MaxProductionKeepPerStyleFamily `
    $MaxProductionKeepPerFlowLanguage `
    $MaxProductionKeepPerChainLanguage `
    $MaxProductionKeepPerStyleFlow `
    $MaxProductionKeepPerStyleSignature `
    $MaxProductionKeepHighRiskRows
$productionKeepRows = @($diversitySelection.Rows)

if (-not [string]::IsNullOrWhiteSpace($ProductionKeepCsv)) {
    $productionKeepRows | Export-Csv -LiteralPath $ProductionKeepCsv -NoTypeInformation -Encoding UTF8
}

if ($MinProductionKeepRows -gt 0 -and $productionKeepRows.Count -lt $MinProductionKeepRows) {
    throw "Production keep rows $($productionKeepRows.Count) below MinProductionKeepRows=$MinProductionKeepRows using $productionKeepSource."
}

$actualSummaryPath = ""
if (-not [string]::IsNullOrWhiteSpace($SummaryPath)) {
    $summary = @()
    $summary += "# PSG Pressure Trace Join Summary"
    $summary += ""
    $summary += "Generated: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')"
    $summary += ""
    $summary += "- Source CSV: $SourceCsv"
    $summary += "- Trace metrics CSV: $TraceMetricsCsv"
    $summary += "- Output CSV: $OutputCsv"
    if (-not [string]::IsNullOrWhiteSpace($BestPerSlotCsv)) { $summary += "- Best per slot CSV: $BestPerSlotCsv" }
    if (-not [string]::IsNullOrWhiteSpace($ProductionKeepCsv)) { $summary += "- Production keep CSV: $ProductionKeepCsv" }
    $summary += "- Joined rows: $($ranked.Count)"
    $summary += "- processKeep rows: $(@($ranked | Where-Object { $_.processKeep }).Count)"
    $summary += "- visualPass rows: $(@($ranked | Where-Object { $_.visualPass }).Count)"
    $summary += "- STS metric present rows: $stsMetricPresentRows"
    $summary += "- stsPass rows: $(@($ranked | Where-Object { $_.stsPass }).Count)"
    $summary += "- stsKeepCandidate rows: $(@($ranked | Where-Object { $_.stsKeepCandidate }).Count)"
    $summary += "- productionKeep mode: $ProductionKeepMode"
    $summary += "- productionKeep source: $productionKeepSource"
    $summary += "- productionKeep eligible rows: $($productionKeepEligibleRows.Count)"
    $summary += "- productionKeep rows: $($productionKeepRows.Count)"
    if ($diversitySelection.Enabled) {
        $summary += "- productionDiversity enabled: True"
        $summary += "- productionDiversity strict: $($diversitySelection.Strict)"
        $summary += "- productionDiversity caps: maxRows=$MaxProductionKeepRows, maxPerStyleFamily=$MaxProductionKeepPerStyleFamily, maxPerFlowLanguage=$MaxProductionKeepPerFlowLanguage, maxPerChainLanguage=$MaxProductionKeepPerChainLanguage, maxPerStyleFlow=$MaxProductionKeepPerStyleFlow, maxPerStyleSignature=$MaxProductionKeepPerStyleSignature, maxHighRiskRows=$MaxProductionKeepHighRiskRows"
        $summary += "- productionDiversity dropped rows: $($diversitySelection.Dropped.Count)"
        $summary += "- productionDiversity backfilled rows: $($diversitySelection.Backfilled.Count)"
    } else {
        $summary += "- productionDiversity enabled: False"
    }
    $summary += ""
    $summary += "## Rank Class"
    foreach ($g in ($ranked | Group-Object rankClass | Sort-Object Name)) {
        $summary += "- $($g.Name): $($g.Count)"
    }
    $summary += ""
    $summary += "## Style Family"
    foreach ($g in ($ranked | Group-Object styleFamily | Sort-Object Name)) {
        $summary += "- $($g.Name): $($g.Count)"
    }
    $summary += ""
    $summary += "## Chain Language"
    foreach ($g in ($ranked | Group-Object chainLanguage | Sort-Object Name)) {
        $summary += "- $($g.Name): $($g.Count)"
    }
    $summary += ""
    $summary += "## Flow Language"
    foreach ($g in ($ranked | Group-Object flowLanguage | Sort-Object Name)) {
        $summary += "- $($g.Name): $($g.Count)"
    }
    $summary += ""
    $summary += "## Risk Band"
    foreach ($g in ($ranked | Group-Object styleRiskBand | Sort-Object Name)) {
        $summary += "- $($g.Name): $($g.Count)"
    }
    if ($productionKeepRows.Count -gt 0) {
        $summary += ""
        $summary += "## Production Keep Tag Mix"
        $summary += "- styleFamily: " + ((@($productionKeepRows | Group-Object styleFamily | Sort-Object Name | ForEach-Object { "$($_.Name)=$($_.Count)" }) -join ", "))
        $summary += "- chainLanguage: " + ((@($productionKeepRows | Group-Object chainLanguage | Sort-Object Name | ForEach-Object { "$($_.Name)=$($_.Count)" }) -join ", "))
        $summary += "- flowLanguage: " + ((@($productionKeepRows | Group-Object flowLanguage | Sort-Object Name | ForEach-Object { "$($_.Name)=$($_.Count)" }) -join ", "))
        $summary += "- riskBand: " + ((@($productionKeepRows | Group-Object styleRiskBand | Sort-Object Name | ForEach-Object { "$($_.Name)=$($_.Count)" }) -join ", "))
    }
    if ($diversitySelection.Enabled -and $diversitySelection.Dropped.Count -gt 0) {
        $summary += ""
        $summary += "## Diversity Dropped Top Rows"
        foreach ($d in ($diversitySelection.Dropped | Select-Object -First 12)) {
            $r = $d.row
            $summary += "- id=$($r.levelId) style=$($r.styleFamily) chain=$($r.chainLanguage) flow=$($r.flowLanguage) risk=$($r.styleRiskBand) score=$($r.psgRankScore) reason=$($d.reasons)"
        }
    }
    if ($diversitySelection.Enabled -and $diversitySelection.Backfilled.Count -gt 0) {
        $summary += ""
        $summary += "## Diversity Backfilled Rows"
        foreach ($d in ($diversitySelection.Backfilled | Select-Object -First 12)) {
            $r = $d.row
            $summary += "- id=$($r.levelId) style=$($r.styleFamily) chain=$($r.chainLanguage) flow=$($r.flowLanguage) risk=$($r.styleRiskBand) score=$($r.psgRankScore) originalReason=$($d.reasons)"
        }
    }
    $summary += ""
    $summary += "## Top Rows"
    foreach ($r in ($ranked | Select-Object -First 12)) {
        $summary += "- [$($r.rankClass)] score=$($r.psgRankScore) id=$($r.levelId) style=$($r.styleFamily) chain=$($r.chainLanguage) flow=$($r.flowLanguage) risk=$($r.styleRiskBand) tier=$($r.processTier) cov=$($r.sourceCoverage) max=$($r.maxChoices) local=$($r.localPatchRun) nearOuter=$($r.nearOuterRun) dirRisk=$($r.directionalRisk) stripe=$($r.stripeRisk) sts=$($r.solveTraceQualityScore)/collapse=$($r.solveTraceCollapseRiskScore) axisRun=$($r.solveSameAxisRunMax) dirRun=$($r.solveSameDirHeadRunMax)"
    }
    $actualSummaryPath = Write-TextWithFallback $SummaryPath $summary
}

[pscustomobject]@{
    sourceCsv = $SourceCsv
    traceMetricsCsv = $TraceMetricsCsv
    outputCsv = $OutputCsv
    summaryPath = $(if (-not [string]::IsNullOrWhiteSpace($actualSummaryPath)) { $actualSummaryPath } else { $SummaryPath })
    bestPerSlotCsv = $BestPerSlotCsv
    productionKeepCsv = $ProductionKeepCsv
    productionKeepMode = $ProductionKeepMode
    productionKeepSource = $productionKeepSource
    joinedRows = $ranked.Count
    processKeepRows = @($ranked | Where-Object { $_.processKeep }).Count
    visualPassRows = @($ranked | Where-Object { $_.visualPass }).Count
    stsMetricPresentRows = $stsMetricPresentRows
    stsPassRows = @($ranked | Where-Object { $_.stsPass }).Count
    stsKeepCandidateRows = @($ranked | Where-Object { $_.stsKeepCandidate }).Count
    productionKeepEligibleRows = $productionKeepEligibleRows.Count
    productionKeepRows = $productionKeepRows.Count
    productionDiversityEnabled = $diversitySelection.Enabled
    productionDiversityDroppedRows = $diversitySelection.Dropped.Count
    productionDiversityBackfilledRows = $diversitySelection.Backfilled.Count
} | Format-List
