param(
    [Parameter(Mandatory = $true)]
    [string[]]$JoinedCsv,
    [Parameter(Mandatory = $true)]
    [string]$MissingDiagCsv,
    [Parameter(Mandatory = $true)]
    [string]$OutputPrefix
)

$ErrorActionPreference = "Stop"

function To-DoubleValue($value, [double]$fallback = [double]::NaN) {
    if ($null -eq $value) { return $fallback }
    $text = ([string]$value).Trim()
    if ([string]::IsNullOrWhiteSpace($text)) { return $fallback }
    $parsed = 0.0
    if ([double]::TryParse($text, [Globalization.NumberStyles]::Float, [Globalization.CultureInfo]::InvariantCulture, [ref]$parsed)) {
        return $parsed
    }
    return $fallback
}

function To-IntValue($value, [int]$fallback = -999999) {
    $number = To-DoubleValue $value ([double]$fallback)
    if ([double]::IsNaN($number)) { return $fallback }
    return [int][Math]::Round($number)
}

function Get-Value($row, [string[]]$names, $fallback = "") {
    foreach ($name in $names) {
        $prop = $row.PSObject.Properties[$name]
        if ($null -ne $prop -and -not [string]::IsNullOrWhiteSpace([string]$prop.Value)) {
            return $prop.Value
        }
    }
    return $fallback
}

function Get-StrictScore($row) {
    $score = 0.0
    $score += (To-DoubleValue (Get-Value $row @("solveTraceQualityScore")) 0.0) * 10000.0
    $score += (To-DoubleValue (Get-Value $row @("sourceCoverage", "coverage")) 0.0) * 1000.0
    $score -= (To-IntValue (Get-Value $row @("maxChoices")) 99) * 140.0
    $score -= (To-IntValue (Get-Value $row @("localPatchSolveRunMax", "localPatchRun")) 99) * 90.0
    $score -= (To-IntValue (Get-Value $row @("nearOuterPatchSolveRunMax", "nearOuterRun")) 99) * 70.0
    $score -= (To-DoubleValue (Get-Value $row @("solveTraceCollapseRiskScore")) 1.0) * 900.0
    $score -= (To-IntValue (Get-Value $row @("solveSameAxisRunMax")) 99) * 50.0
    $score -= (To-IntValue (Get-Value $row @("solveSameDirHeadRunMax")) 99) * 45.0
    return $score
}

$missingOrders = @{}
Import-Csv -LiteralPath $MissingDiagCsv | ForEach-Object {
    $missingOrders[[string]$_.order] = $_
}

$allRows = New-Object System.Collections.Generic.List[object]
foreach ($csv in $JoinedCsv) {
    if (-not (Test-Path -LiteralPath $csv)) { continue }
    foreach ($row in (Import-Csv -LiteralPath $csv)) {
        $order = [string](Get-Value $row @("order"))
        if (-not $missingOrders.ContainsKey($order)) { continue }
        $coverage = To-DoubleValue (Get-Value $row @("sourceCoverage", "coverage")) 0.0
        $strict =
            ([string](Get-Value $row @("solved")) -ieq "True") -and
            $coverage -ge 0.93 -and
            (To-IntValue (Get-Value $row @("maxChoices")) 99) -le 11 -and
            (To-IntValue (Get-Value $row @("localPatchSolveRunMax", "localPatchRun")) 99) -le 7 -and
            (To-IntValue (Get-Value $row @("nearOuterPatchSolveRunMax", "nearOuterRun")) 99) -le 6 -and
            (To-DoubleValue (Get-Value $row @("solveTraceQualityScore")) 0.0) -ge 0.82 -and
            (To-DoubleValue (Get-Value $row @("solveTraceCollapseRiskScore")) 1.0) -le 0.28 -and
            (To-IntValue (Get-Value $row @("solveSameAxisRunMax")) 99) -le 9 -and
            (To-IntValue (Get-Value $row @("solveSameDirHeadRunMax")) 99) -le 8

        $allRows.Add([pscustomobject][ordered]@{
            strict = $strict
            score = [Math]::Round((Get-StrictScore $row), 4)
            order = $order
            section10 = Get-Value $row @("section10")
            levelId = Get-Value $row @("levelId")
            path = Get-Value $row @("path", "assetPath")
            source = Split-Path -Leaf $csv
            solved = Get-Value $row @("solved")
            sourceCoverage = [Math]::Round($coverage, 4)
            maxChoices = To-IntValue (Get-Value $row @("maxChoices")) 99
            localPatchSolveRunMax = To-IntValue (Get-Value $row @("localPatchSolveRunMax", "localPatchRun")) 99
            nearOuterPatchSolveRunMax = To-IntValue (Get-Value $row @("nearOuterPatchSolveRunMax", "nearOuterRun")) 99
            solveTraceQualityScore = [Math]::Round((To-DoubleValue (Get-Value $row @("solveTraceQualityScore")) 0.0), 4)
            solveTraceCollapseRiskScore = [Math]::Round((To-DoubleValue (Get-Value $row @("solveTraceCollapseRiskScore")) 1.0), 4)
            solveSameAxisRunMax = To-IntValue (Get-Value $row @("solveSameAxisRunMax")) 99
            solveSameDirHeadRunMax = To-IntValue (Get-Value $row @("solveSameDirHeadRunMax")) 99
        }) | Out-Null
    }
}

$allCsv = "$OutputPrefix`_all_audited.csv"
$strictCsv = "$OutputPrefix`_strict_rows.csv"
$bestCsv = "$OutputPrefix`_strict_best_per_order.csv"
$summaryPath = "$OutputPrefix`_summary.md"

$allRows | Sort-Object @{ Expression = { [int]$_.order } }, @{ Expression = { -[double]$_.score } } |
    Export-Csv -LiteralPath $allCsv -NoTypeInformation -Encoding UTF8

$strictRows = @($allRows | Where-Object { $_.strict })
$strictRows | Sort-Object @{ Expression = { [int]$_.order } }, @{ Expression = { -[double]$_.score } } |
    Export-Csv -LiteralPath $strictCsv -NoTypeInformation -Encoding UTF8

$best = foreach ($group in ($strictRows | Group-Object order)) {
    $group.Group | Sort-Object @{ Expression = { -[double]$_.score } }, levelId | Select-Object -First 1
}
$best | Sort-Object @{ Expression = { [int]$_.order } } |
    Export-Csv -LiteralPath $bestCsv -NoTypeInformation -Encoding UTF8

$missingCount = $missingOrders.Count
$strictUnique = @($best).Count
$lines = @(
    "# Campaign500 Strict Holefill Audit",
    "",
    "- Inputs: $($JoinedCsv.Count)",
    "- Audited rows in missing set: $($allRows.Count)",
    "- Strict rows: $($strictRows.Count)",
    "- Strict unique missing orders: $strictUnique/$missingCount",
    "- Output all: $allCsv",
    "- Output strict: $strictCsv",
    "- Output best: $bestCsv"
)
$lines | Set-Content -LiteralPath $summaryPath -Encoding UTF8

[pscustomobject]@{
    allCsv = $allCsv
    strictCsv = $strictCsv
    bestCsv = $bestCsv
    summaryPath = $summaryPath
    auditedRows = $allRows.Count
    strictRows = $strictRows.Count
    strictUniqueOrders = $strictUnique
    missingOrders = $missingCount
} | Format-List
