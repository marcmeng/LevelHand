param(
    [string[]]$CandidateReportCsvs = @(),
    [string]$OutputDir = "F:\Unityproject\ArrowLevel-Hand\_CodexRun",
    [string]$OutputPrefix = "campaign500_psg_normal_unified_prod200",
    [ValidateSet("All", "SectionQuota")]
    [string]$TraceCandidateMode = "SectionQuota",
    [int]$TraceNormalRowsPerSection = 4,
    [int]$TraceChallengeRowsPerSection = 3,
    [int]$MinOrder = 11,
    [int]$MaxOrder = 0
)

$ErrorActionPreference = "Stop"

function Ensure-Directory([string]$path) {
    if (-not (Test-Path -LiteralPath $path)) {
        New-Item -ItemType Directory -Path $path -Force | Out-Null
    }
}

function To-Int($value, [int]$default = 0) {
    if ([string]::IsNullOrWhiteSpace([string]$value)) { return $default }
    $parsed = 0
    if ([int]::TryParse([string]$value, [ref]$parsed)) { return $parsed }
    return $default
}

function To-Double($value, [double]$default = 0.0) {
    if ([string]::IsNullOrWhiteSpace([string]$value)) { return $default }
    $parsed = 0.0
    if ([double]::TryParse(
        [string]$value,
        [Globalization.NumberStyles]::Float,
        [Globalization.CultureInfo]::InvariantCulture,
        [ref]$parsed)) {
        return $parsed
    }
    return $default
}

function Get-Value($row, [string[]]$names, $default = "") {
    foreach ($name in $names) {
        $prop = $row.PSObject.Properties[$name]
        if ($null -ne $prop -and -not [string]::IsNullOrWhiteSpace([string]$prop.Value)) {
            return $prop.Value
        }
    }
    return $default
}

function Get-ProjectRootFromReport([string]$reportCsv) {
    $full = [IO.Path]::GetFullPath($reportCsv)
    $marker = "\Assets\"
    $idx = $full.IndexOf($marker, [StringComparison]::OrdinalIgnoreCase)
    if ($idx -gt 0) {
        return $full.Substring(0, $idx)
    }
    return (Split-Path -Parent $full)
}

function Get-CanvasBand([double]$aspect) {
    if ($aspect -ge 0.72) { return "wide_portrait" }
    if ($aspect -ge 0.64) { return "balanced_portrait" }
    return "tall_portrait"
}

function Get-ModeGroup([string]$mode) {
    if ([string]::IsNullOrWhiteSpace($mode)) { return "unknown" }
    $m = $mode.ToLowerInvariant()
    if ($m -eq "layout_soft") { return "layout_soft" }
    if ($m.StartsWith("language")) { return "language" }
    if ($m.StartsWith("pure")) { return "pure" }
    return $m
}

function Add-SelectionMetadata($row) {
    $width = To-Int (Get-Value $row @("Width", "width") "") 0
    $height = To-Int (Get-Value $row @("Height", "height") "") 0
    $coverage = To-Double (Get-Value $row @("Coverage", "coverage") "") 0.0
    $outerCoverage = To-Double (Get-Value $row @("OuterBandCoverage", "outerBandCoverage") "") 0.0
    $openers = To-Int (Get-Value $row @("PortableOpeners", "portableOpeners", "InitialMovableChains") "") 0
    $edgeHeads = To-Int (Get-Value $row @("EdgeHeadChains", "edgeHeadChains") "") 0
    $attempts = To-Int (Get-Value $row @("Attempts", "attempts") "") 0
    $difficulty = To-Int (Get-Value $row @("DifficultyCode", "difficultyCode") "") 1
    $modeGroup = Get-ModeGroup ([string](Get-Value $row @("Mode", "mode") ""))
    $aspect = if ($height -gt 0) { $width / [double]$height } else { 0.0 }

    $score = $coverage * 100.0 + $outerCoverage * 16.0
    if ($coverage -ge 0.97) { $score += 10.0 } elseif ($coverage -lt 0.965) { $score -= 16.0 }
    if ($openers -ge 2 -and $openers -le 6) { $score += 7.0 } elseif ($openers -eq 1) { $score += 2.0 } else { $score -= 4.0 }
    if ($edgeHeads -gt 0 -and $edgeHeads -le 12) { $score += 2.0 } elseif ($edgeHeads -gt 16) { $score -= 3.0 }
    switch ($modeGroup) {
        "layout_soft" { $score += 6.0 }
        "language" { $score += 4.0 }
        "pure" { $score += 2.0 }
    }
    $score -= [Math]::Min(5.0, $attempts / 220.0)

    $row | Add-Member -NotePropertyName traceSelected -NotePropertyValue $false -Force
    $row | Add-Member -NotePropertyName traceSelectReason -NotePropertyValue "" -Force
    $row | Add-Member -NotePropertyName traceSection -NotePropertyValue (To-Int (Get-Value $row @("Section10", "section10") "") 0) -Force
    $row | Add-Member -NotePropertyName traceSectionBucket -NotePropertyValue $(if ($difficulty -le 1) { "normal" } else { "challenge" }) -Force
    $row | Add-Member -NotePropertyName traceCanvasBand -NotePropertyValue (Get-CanvasBand $aspect) -Force
    $row | Add-Member -NotePropertyName traceModeGroup -NotePropertyValue $modeGroup -Force
    $row | Add-Member -NotePropertyName traceStaticScore -NotePropertyValue ([Math]::Round($score, 3)) -Force
    return $row
}

function Get-DiversityKey($row, [string]$passName) {
    $style = [string](Get-Value $row @("StyleHint", "styleHint") "")
    $canvas = [string](Get-Value $row @("traceCanvasBand") "")
    $modeGroup = [string](Get-Value $row @("traceModeGroup") "")
    $difficulty = [string](Get-Value $row @("DifficultyCode", "difficultyCode") "")
    switch ($passName) {
        "style_canvas" { return "$style|$canvas" }
        "style_mode" { return "$style|$modeGroup" }
        "difficulty_style" { return "$difficulty|$style" }
        "style" { return $style }
        default { return [string](Get-Value $row @("LevelId", "levelId") "") }
    }
}

function Add-QuotaPass($ranked, [hashtable]$selectedById, [System.Collections.Generic.List[object]]$picked, [int]$quota, [string]$passName) {
    if ($quota -le 0 -or $picked.Count -ge $quota) { return }

    $used = @{}
    foreach ($row in $picked) {
        $key = Get-DiversityKey $row $passName
        if (-not [string]::IsNullOrWhiteSpace($key)) { $used[$key] = $true }
    }

    foreach ($row in $ranked) {
        if ($picked.Count -ge $quota) { break }
        $id = [string](Get-Value $row @("LevelId", "levelId") "")
        if ([string]::IsNullOrWhiteSpace($id) -or $selectedById.ContainsKey($id)) { continue }

        $key = Get-DiversityKey $row $passName
        if ($passName -ne "any" -and $used.ContainsKey($key)) { continue }

        $selectedById[$id] = $true
        $row.traceSelected = $true
        $row.traceSelectReason = "section quota $passName"
        $picked.Add($row) | Out-Null
        if (-not [string]::IsNullOrWhiteSpace($key)) { $used[$key] = $true }
    }
}

function Select-TraceRows($rows) {
    $annotated = @($rows | ForEach-Object { Add-SelectionMetadata $_ })
    if ($TraceCandidateMode -eq "All") {
        foreach ($row in $annotated) {
            $row.traceSelected = $true
            $row.traceSelectReason = "all"
        }
        return $annotated
    }

    $selectedById = @{}
    $selected = New-Object System.Collections.Generic.List[object]
    foreach ($sectionGroup in @($annotated | Group-Object traceSection | Sort-Object @{ Expression = { To-Int $_.Name } })) {
        foreach ($bucket in @("normal", "challenge")) {
            $quota = if ($bucket -eq "normal") { $TraceNormalRowsPerSection } else { $TraceChallengeRowsPerSection }
            if ($quota -le 0) { continue }
            $bucketRows = @($sectionGroup.Group | Where-Object { $_.traceSectionBucket -eq $bucket })
            if ($bucketRows.Count -eq 0) { continue }

            $ranked = @($bucketRows | Sort-Object `
                @{ Expression = { -1.0 * (To-Double $_.traceStaticScore) } }, `
                @{ Expression = { To-Int $_.Order } }, `
                Row)
            $picked = New-Object System.Collections.Generic.List[object]
            Add-QuotaPass $ranked $selectedById $picked $quota "style_canvas"
            Add-QuotaPass $ranked $selectedById $picked $quota "style_mode"
            if ($bucket -eq "challenge") { Add-QuotaPass $ranked $selectedById $picked $quota "difficulty_style" }
            Add-QuotaPass $ranked $selectedById $picked $quota "style"
            Add-QuotaPass $ranked $selectedById $picked $quota "any"
            foreach ($row in $picked) { $selected.Add($row) | Out-Null }
        }
    }

    return @($selected.ToArray() | Sort-Object traceSection, @{ Expression = { if ($_.traceSectionBucket -eq "normal") { 0 } else { 1 } } }, @{ Expression = { To-Int $_.Order } })
}

if ($CandidateReportCsvs.Count -eq 0) {
    $CandidateReportCsvs = @(
        Get-ChildItem -LiteralPath "D:\Unityproject" -Directory -Filter "ArrowLevel-Hand-campaign500-psg-normal*" -ErrorAction SilentlyContinue |
            ForEach-Object {
                Get-ChildItem -LiteralPath (Join-Path $_.FullName "Assets\ArrowMagic\SOData\Reports\Campaign500\PSGNormal") -Filter "campaign500_psg_normal_prod200_*_report.csv" -ErrorAction SilentlyContinue
            } |
            Where-Object { $_.Name -match '^campaign500_psg_normal_prod200_c\d+_' } |
            Select-Object -ExpandProperty FullName
    )
}

if ($CandidateReportCsvs.Count -eq 0) {
    throw "No candidate report CSVs provided or discovered."
}

Ensure-Directory $OutputDir

$safePrefix = $OutputPrefix -replace '[^\w\-.]+', '_'
$candidatePoolCsv = Join-Path $OutputDir "$safePrefix`_candidate_pool.csv"
$traceSelectionCsv = Join-Path $OutputDir "$safePrefix`_trace_selection.csv"
$traceInputCsv = Join-Path $OutputDir "$safePrefix`_trace_input.csv"
$summaryPath = Join-Path $OutputDir "$safePrefix`_summary.md"

$allRows = New-Object System.Collections.Generic.List[object]
foreach ($report in $CandidateReportCsvs) {
    if (-not (Test-Path -LiteralPath $report)) {
        throw "Missing candidate report: $report"
    }

    $projectRoot = Get-ProjectRootFromReport $report
    $reportRows = @(Import-Csv -LiteralPath $report)
    foreach ($row in $reportRows) {
        $order = To-Int (Get-Value $row @("Order", "order") "") 0
        if ($order -lt $MinOrder) { continue }
        if ($MaxOrder -gt 0 -and $order -gt $MaxOrder) { continue }

        $assetPath = [string](Get-Value $row @("AssetPath", "path", "assetPath") "")
        $absoluteAssetPath = if ([IO.Path]::IsPathRooted($assetPath)) {
            $assetPath
        } elseif (-not [string]::IsNullOrWhiteSpace($assetPath)) {
            Join-Path $projectRoot $assetPath
        } else {
            ""
        }
        $traceEligible = -not [string]::IsNullOrWhiteSpace([string](Get-Value $row @("LevelId", "levelId") "")) -and
            -not [string]::IsNullOrWhiteSpace($absoluteAssetPath) -and
            (Test-Path -LiteralPath $absoluteAssetPath)

        $row | Add-Member -NotePropertyName sourceReportCsv -NotePropertyValue $report -Force
        $row | Add-Member -NotePropertyName sourceProjectRoot -NotePropertyValue $projectRoot -Force
        $row | Add-Member -NotePropertyName originalAssetPath -NotePropertyValue $assetPath -Force
        $row | Add-Member -NotePropertyName absoluteAssetPath -NotePropertyValue $absoluteAssetPath -Force
        $row | Add-Member -NotePropertyName traceEligible -NotePropertyValue $traceEligible -Force
        $allRows.Add($row) | Out-Null
    }
}

$poolRows = @($allRows.ToArray() | Sort-Object @{ Expression = { To-Int $_.Order } }, @{ Expression = { To-Int $_.Row } })
$poolRows | Export-Csv -LiteralPath $candidatePoolCsv -NoTypeInformation -Encoding UTF8

$eligibleRows = @($poolRows | Where-Object { $_.traceEligible })
$selectedRows = @(Select-TraceRows $eligibleRows)

$poolRows |
    Sort-Object @{ Expression = { To-Int $_.Section10 } }, traceSectionBucket, @{ Expression = { To-Int $_.Order } }, @{ Expression = { To-Int $_.Row } } |
    Export-Csv -LiteralPath $traceSelectionCsv -NoTypeInformation -Encoding UTF8

$traceInputRows = @($selectedRows | ForEach-Object {
    [pscustomobject]@{
        selected = 1
        levelId = $_.LevelId
        path = $_.absoluteAssetPath
        source = "Campaign500PSGNormalUnified"
        sourceProjectRoot = $_.sourceProjectRoot
        sourceReportCsv = $_.sourceReportCsv
        originalAssetPath = $_.originalAssetPath
        order = $_.Order
        section10 = $_.Section10
        styleHint = $_.StyleHint
        difficultyCode = $_.DifficultyCode
        experienceRole = $_.ExperienceRole
        mode = $_.Mode
        coverage = $_.Coverage
        chains = $_.Chains
        portableOpeners = $_.PortableOpeners
        status = $_.Status
    }
})
$traceInputRows | Export-Csv -LiteralPath $traceInputCsv -NoTypeInformation -Encoding UTF8

$summary = @()
$summary += "# Campaign500 PSG Normal Unified Trace Input V1"
$summary += ""
$summary += "- Candidate reports: $($CandidateReportCsvs.Count)"
$summary += "- Candidate pool rows: $($poolRows.Count)"
$summary += "- Trace eligible rows: $($eligibleRows.Count)"
$summary += "- Trace candidate mode: $TraceCandidateMode"
$summary += "- Normal rows per section: $TraceNormalRowsPerSection"
$summary += "- Challenge rows per section: $TraceChallengeRowsPerSection"
$summary += "- Trace input rows: $($traceInputRows.Count)"
$summary += "- Candidate pool CSV: $candidatePoolCsv"
$summary += "- Trace selection CSV: $traceSelectionCsv"
$summary += "- Trace input CSV: $traceInputCsv"
$summary += ""
$summary += "## Selected By Section"
foreach ($g in ($selectedRows | Group-Object Section10 | Sort-Object @{ Expression = { To-Int $_.Name } })) {
    $mix = (@($g.Group | Group-Object traceSectionBucket | Sort-Object Name | ForEach-Object { "$($_.Name)=$($_.Count)" }) -join ", ")
    $summary += "- section $($g.Name): $($g.Count) ($mix)"
}
$summary += ""
$summary += "## Selected Canvas"
foreach ($g in ($selectedRows | Group-Object traceCanvasBand | Sort-Object Name)) {
    $summary += "- $($g.Name): $($g.Count)"
}
$summary += ""
$summary += "## Selected Style"
foreach ($g in ($selectedRows | Group-Object StyleHint | Sort-Object Name)) {
    $summary += "- $($g.Name): $($g.Count)"
}
$summary | Set-Content -LiteralPath $summaryPath -Encoding UTF8

[pscustomobject]@{
    candidateReports = $CandidateReportCsvs.Count
    candidatePoolCsv = $candidatePoolCsv
    traceSelectionCsv = $traceSelectionCsv
    traceInputCsv = $traceInputCsv
    summaryPath = $summaryPath
    poolRows = $poolRows.Count
    traceEligibleRows = $eligibleRows.Count
    traceInputRows = $traceInputRows.Count
} | Format-List
