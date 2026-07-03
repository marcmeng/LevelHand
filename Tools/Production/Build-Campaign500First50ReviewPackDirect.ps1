param(
    [string]$ProjectRoot = ""
)

$ErrorActionPreference = "Stop"

if ([string]::IsNullOrWhiteSpace($ProjectRoot)) {
    $ProjectRoot = Split-Path -Parent (Split-Path -Parent $PSScriptRoot)
}

$scriptPath = Join-Path $ProjectRoot "Tools\Production\Campaign500First50ReviewPackDirectBuilder.py"
if (-not (Test-Path -LiteralPath $scriptPath)) {
    throw "Builder script not found: $scriptPath"
}

python $scriptPath
