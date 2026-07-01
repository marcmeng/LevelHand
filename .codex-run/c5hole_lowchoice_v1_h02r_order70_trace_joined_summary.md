# PSG Pressure Trace Join Summary

Generated: 2026-07-01 07:37:41

- Source CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h02r_order70_source_subset.csv
- Trace metrics CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab\Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\c5hole_lowchoice_v1_h02r_order70_metrics.csv
- Output CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h02r_order70_trace_joined.csv
- Best per slot CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h02r_order70_trace_best_per_slot.csv
- Production keep CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h02r_order70_production_keep.csv
- Joined rows: 6
- Output rows: 6
- processKeep rows: 5
- visualPass rows: 4
- STS metric present rows: 6
- stsPass rows: 3
- stsKeepCandidate rows: 2
- productionKeep mode: TraceOrderPreferred
- productionKeep source: TraceOrderKeep
- productionKeep eligible rows: 2
- productionKeep rows: 2
- productionDiversity enabled: False

## Rank Class
- ProcessKeep: 1
- Reject: 1
- TraceOrderKeep: 2
- VisualKeep: 2

## Style Family
- section_unlock: 6

## Chain Language
- curve_chain: 5
- patch_chain: 1

## Flow Language
- local_collapse: 2
- single_axis_sweep: 2
- staged_unlock: 2

## Risk Band
- high_risk: 4
- watch: 2

## Production Keep Tag Mix
- styleFamily: section_unlock=2
- chainLanguage: curve_chain=2
- flowLanguage: single_axis_sweep=1, staged_unlock=1
- riskBand: watch=2

## Top Rows
- [TraceOrderKeep] score=2.747 id=c5hole_lowchoice_v1_h02r_10_nutation_strict_mixed_hard_v1_rect_s07_o070_v03_strictmixedhard_strict_hard_chain_hf1 style=section_unlock chain=curve_chain flow=staged_unlock risk=watch tier=B cov=0.98 max=7 local=5 nearOuter=5 dirRisk=0.187 stripe=0.069 sts=0.885/collapse=0.205 axisRun=7 dirRun=7
- [TraceOrderKeep] score=2.599 id=c5hole_lowchoice_v1_h02r_08_nutation_strict_mixed_hard_v1_rect_s07_o070_v01_strictmixedhard_strict_hard_chain_hf1 style=section_unlock chain=curve_chain flow=single_axis_sweep risk=watch tier=B cov=0.978 max=10 local=6 nearOuter=4 dirRisk=0.206 stripe=0.059 sts=0.833/collapse=0.269 axisRun=8 dirRun=8
- [VisualKeep] score=1.977 id=c5hole_lowchoice_v1_h02r_12_nutation_strict_mixed_hard_v1_rect_s07_o070_v05_strictmixedhard_strict_hard_chain_hf1 style=section_unlock chain=curve_chain flow=local_collapse risk=high_risk tier=B cov=0.985 max=11 local=6 nearOuter=3 dirRisk=0.199 stripe=0.113 sts=0.801/collapse=0.357 axisRun=11 dirRun=8
- [VisualKeep] score=1.942 id=c5hole_lowchoice_v1_h02r_11_nutation_strict_mixed_hard_v1_rect_s07_o070_v04_strictmixedhard_strict_hard_chain_hf1 style=section_unlock chain=patch_chain flow=local_collapse risk=high_risk tier=B cov=0.986 max=10 local=5 nearOuter=5 dirRisk=0.21 stripe=0.052 sts=0.751/collapse=0.374 axisRun=16 dirRun=12
- [ProcessKeep] score=1.276 id=c5hole_lowchoice_v1_h02r_09_nutation_strict_mixed_hard_v1_rect_s07_o070_v02_strictmixedhard_strict_hard_chain_hf1 style=section_unlock chain=curve_chain flow=single_axis_sweep risk=high_risk tier=B cov=0.989 max=8 local=7 nearOuter=7 dirRisk=0.344 stripe=0.091 sts=0.846/collapse=0.302 axisRun=10 dirRun=10
- [Reject] score=0.61 id=c5hole_lowchoice_v1_h02r_07_nutation_strict_mixed_hard_v1_rect_s07_o070_v00_strictmixedhard_strict_hard_chain_hf1 style=section_unlock chain=curve_chain flow=staged_unlock risk=high_risk tier=Drop cov=0.98 max=14 local=4 nearOuter=4 dirRisk=0.109 stripe=0.035 sts=0.872/collapse=0.203 axisRun=8 dirRun=7
