param()
$ErrorActionPreference = 'Stop'
Set-Location 'F:\Unityproject\ArrowLevel-Hand'
& '.\.codex-run\Trace-Campaign500HolefillOrderV1.ps1' `
  -SourceReportCsv 'F:\Unityproject\ArrowLevel-Hand\.worktrees\campaign500-normal-full-s03\Assets\ArrowMagic\SOData\Reports\Campaign500\NormalFullV1\c5hole_lowchoice_v1_h02r_report.csv' `
  -SourceRoot 'F:\Unityproject\ArrowLevel-Hand\.worktrees\campaign500-normal-full-s03' `
  -Order 70 `
  -OutputPrefix 'c5hole_lowchoice_v1_h02r_order70'
