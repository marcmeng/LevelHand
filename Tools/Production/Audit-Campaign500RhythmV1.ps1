param(
    [Parameter(Mandatory=$true)]
    [string]$LayoutCsv,

    [string]$DutyCsv = "Exports/Campaign500_DesignPlanning_20260702/campaign500_per_level_duty_v3_rough.csv",

    [string]$StoryCsv = "Exports/Campaign500_DesignPlanning_20260702/campaign500_full500_10level_story_v3.csv",

    [string]$OutDir = "Exports/Campaign500_DesignPlanning_20260702/Audit",

    [int]$ExpectedCount = 500
)

$ErrorActionPreference = "Stop"

$repoRoot = Split-Path -Parent (Split-Path -Parent $PSScriptRoot)
$pythonScript = Join-Path $PSScriptRoot "Campaign500RhythmAuditV1.py"

function Resolve-RepoPath([string]$PathValue) {
    if ([System.IO.Path]::IsPathRooted($PathValue)) {
        return $PathValue
    }
    return (Join-Path $repoRoot $PathValue)
}

$layoutPath = Resolve-RepoPath $LayoutCsv
$dutyPath = Resolve-RepoPath $DutyCsv
$storyPath = Resolve-RepoPath $StoryCsv
$outPath = Resolve-RepoPath $OutDir

if (-not (Test-Path -LiteralPath $layoutPath)) {
    throw "LayoutCsv not found: $layoutPath"
}
if (-not (Test-Path -LiteralPath $pythonScript)) {
    throw "Audit script not found: $pythonScript"
}

New-Item -ItemType Directory -Force -Path $outPath | Out-Null

python $pythonScript `
    --layout $layoutPath `
    --duty $dutyPath `
    --story $storyPath `
    --out-dir $outPath `
    --expected-count $ExpectedCount
