param(
    [string]$TemplateCsv = "Exports\Campaign500_PSG_Template_20260626_095625\campaign500_normal_4slot_plan_v1.csv",
    [string]$MissingDiagCsv = ".codex-run\campaign500_missing_after_raw_trace_v1_failure_diag.csv",
    [string]$OutputRoot = ".codex-run\campaign500_holefill_lowchoice_v1_plans",
    [string]$Prefix = "c5hole_lowchoice_v1"
)

$ErrorActionPreference = "Stop"

function Copy-RowWithLane($row, [string]$lane, [string]$styleFamily, [string]$chain, [string]$bandOverride, [string]$reasonSuffix) {
    $props = [ordered]@{}
    foreach ($prop in $row.PSObject.Properties) {
        $props[$prop.Name] = $prop.Value
    }

    $props["productionLane"] = $lane
    $props["productionStyleFamily"] = $styleFamily
    $props["productionChainLanguage"] = $chain
    if (-not [string]::IsNullOrWhiteSpace($bandOverride)) {
        $props["productionDifficultyBand"] = $bandOverride
    }

    $props["productionWave"] = "StrictHolefillLowChoiceV1"
    $props["selectionReason"] = "strict holefill low-choice: $reasonSuffix; baseLane=$($row.productionLane); baseChain=$($row.productionChainLanguage)"
    return [pscustomobject]$props
}

function Get-StrategyRows($baseRow, $diagRow) {
    $fail = ([string]$diagRow.fail).ToLowerInvariant()
    $band = [string]$diagRow.band
    $role = [string]$diagRow.roleInSection
    $rows = New-Object System.Collections.Generic.List[object]

    if ($fail.Contains("choices")) {
        $rows.Add((Copy-RowWithLane $baseRow "StrictMixedChoiceClamp" "strict_mixed_choice" "choice_clamp_chain_hf1" $band "choice clamp primary")) | Out-Null
        if ($band -match "Peak") {
            $rows.Add((Copy-RowWithLane $baseRow "StrictMixedPeak" "strict_mixed_dependency" "strict_peak_chain_hf1" "Peak" "peak strict mixed backup")) | Out-Null
        } elseif ($role -match "Normal") {
            $rows.Add((Copy-RowWithLane $baseRow "StrictMixedLocalBreak" "strict_mixed_local" "local_break_chain_hf1" $band "normal local-break backup")) | Out-Null
        } else {
            $rows.Add((Copy-RowWithLane $baseRow "StrictMixedHard" "strict_mixed_dependency" "strict_hard_chain_hf1" "Hard" "hard strict mixed backup")) | Out-Null
        }
        return $rows
    }

    if ($fail.Contains("local") -or $fail.Contains("near")) {
        $rows.Add((Copy-RowWithLane $baseRow "StrictMixedLocalBreak" "strict_mixed_local" "local_break_chain_hf1" $band "local/near break primary")) | Out-Null
        $rows.Add((Copy-RowWithLane $baseRow "StrictMixedChoiceClamp" "strict_mixed_choice" "choice_clamp_chain_hf1" $band "choice clamp backup")) | Out-Null
        return $rows
    }

    if ($fail.Contains("axis") -or $fail.Contains("dir") -or $fail.Contains("collapse") -or $fail.Contains("sts")) {
        $rows.Add((Copy-RowWithLane $baseRow "StrictMixedAxisBreak" "strict_mixed_dependency" "axis_break_chain_hf1" $band "axis/sts break primary")) | Out-Null
        $rows.Add((Copy-RowWithLane $baseRow "StrictMixedLocalBreak" "strict_mixed_local" "local_break_chain_hf1" $band "local-break backup")) | Out-Null
        return $rows
    }

    $rows.Add((Copy-RowWithLane $baseRow "StrictMixedChoiceClamp" "strict_mixed_choice" "choice_clamp_chain_hf1" $band "fallback choice clamp")) | Out-Null
    $rows.Add((Copy-RowWithLane $baseRow "StrictMixedAxisBreak" "strict_mixed_dependency" "axis_break_chain_hf1" $band "fallback axis break")) | Out-Null
    return $rows
}

if (-not (Test-Path -LiteralPath $TemplateCsv)) { throw "Missing template CSV: $TemplateCsv" }
if (-not (Test-Path -LiteralPath $MissingDiagCsv)) { throw "Missing missing diag CSV: $MissingDiagCsv" }

$template = Import-Csv -LiteralPath $TemplateCsv
$diag = Import-Csv -LiteralPath $MissingDiagCsv
$diagByOrder = @{}
foreach ($row in $diag) {
    $diagByOrder[[int]$row.order] = $row
}

$chunks = @(
    [pscustomobject]@{ id = "h01"; orders = @(25, 39, 47, 56); runner = ".worktrees\campaign500-normal-full-s01" },
    [pscustomobject]@{ id = "h02"; orders = @(70, 72, 89, 105); runner = ".worktrees\campaign500-normal-full-s03" },
    [pscustomobject]@{ id = "h03"; orders = @(112, 129, 136, 148); runner = ".worktrees\campaign500-normal-full-s04" },
    [pscustomobject]@{ id = "h04"; orders = @(152, 166, 172, 182); runner = ".worktrees\campaign500-normal-full-s05" },
    [pscustomobject]@{ id = "h05"; orders = @(196, 206, 219, 221); runner = ".worktrees\nutation-flow-peel-production" },
    [pscustomobject]@{ id = "h06"; orders = @(231, 248, 252, 274); runner = ".worktrees\campaign500-normal-full-s02" },
    [pscustomobject]@{ id = "h07"; orders = @(299, 302, 312, 326); runner = ".worktrees\campaign500-normal-full-s06" },
    [pscustomobject]@{ id = "h08"; orders = @(331, 341, 359, 372); runner = ".worktrees\campaign500-normal-full-s05" },
    [pscustomobject]@{ id = "h09"; orders = @(389, 397, 419, 425); runner = ".worktrees\campaign500-normal-full-s02" },
    [pscustomobject]@{ id = "h10"; orders = @(436, 449, 456, 465); runner = ".worktrees\campaign500-normal-full-s06" },
    [pscustomobject]@{ id = "h11"; orders = @(479, 485, 500); runner = ".worktrees\nutation-flow-peel-production" }
)

New-Item -ItemType Directory -Path $OutputRoot -Force | Out-Null
$manifest = New-Object System.Collections.Generic.List[object]

foreach ($chunk in $chunks) {
    $planRows = New-Object System.Collections.Generic.List[object]
    foreach ($order in $chunk.orders) {
        $base = $template | Where-Object { [int]$_.order -eq [int]$order } | Select-Object -First 1
        if ($null -eq $base) { throw "Missing base plan row for order $order" }
        $diagRow = $diagByOrder[[int]$order]
        if ($null -eq $diagRow) { throw "Missing diag row for order $order" }
        foreach ($strategyRow in (Get-StrategyRows $base $diagRow)) {
            $planRows.Add($strategyRow) | Out-Null
        }
    }

    $planPath = Join-Path $OutputRoot "$Prefix`_$($chunk.id)_plan.csv"
    $planRows | Export-Csv -LiteralPath $planPath -NoTypeInformation -Encoding UTF8
    $manifest.Add([pscustomobject]@{
        shardId = $chunk.id
        runner = $chunk.runner
        plan = (Resolve-Path -LiteralPath $planPath).Path
        orders = ($chunk.orders -join ",")
        planRows = $planRows.Count
        expandedRows = $planRows.Count * 6
    }) | Out-Null
}

$manifestPath = Join-Path $OutputRoot "$Prefix`_manifest.csv"
$manifest | Export-Csv -LiteralPath $manifestPath -NoTypeInformation -Encoding UTF8
$manifest | Format-Table -AutoSize
