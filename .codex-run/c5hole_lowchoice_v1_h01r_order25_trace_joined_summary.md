# PSG Pressure Trace Join Summary

Generated: 2026-07-01 07:23:47

- Source CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h01r_order25_source_subset.csv
- Trace metrics CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab\Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\c5hole_lowchoice_v1_h01r_order25_metrics.csv
- Output CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h01r_order25_trace_joined.csv
- Best per slot CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h01r_order25_trace_best_per_slot.csv
- Production keep CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h01r_order25_production_keep.csv
- Joined rows: 6
- Output rows: 6
- processKeep rows: 4
- visualPass rows: 4
- STS metric present rows: 6
- stsPass rows: 4
- stsKeepCandidate rows: 3
- productionKeep mode: TraceOrderPreferred
- productionKeep source: TraceOrderKeep
- productionKeep eligible rows: 3
- productionKeep rows: 3
- productionDiversity enabled: False

## Rank Class
- Reject: 2
- TraceOrderKeep: 3
- VisualKeep: 1

## Style Family
- mixed_unknown: 6

## Chain Language
- curve_chain: 4
- patch_chain: 2

## Flow Language
- local_collapse: 1
- single_axis_sweep: 2
- staged_unlock: 3

## Risk Band
- high_risk: 3
- watch: 3

## Production Keep Tag Mix
- styleFamily: mixed_unknown=3
- chainLanguage: curve_chain=3
- flowLanguage: single_axis_sweep=1, staged_unlock=2
- riskBand: watch=3

## Top Rows
- [TraceOrderKeep] score=2.69 id=c5hole_lowchoice_v1_h01r_10_nutation_strict_mixed_hard_v1_rect_s03_o025_v03_strictmixedhard_strict_hard_chain_hf1 style=mixed_unknown chain=curve_chain flow=staged_unlock risk=watch tier=A cov=0.989 max=11 local=4 nearOuter=3 dirRisk=0.102 stripe=0.091 sts=0.851/collapse=0.224 axisRun=6 dirRun=6
- [TraceOrderKeep] score=2.684 id=c5hole_lowchoice_v1_h01r_08_nutation_strict_mixed_hard_v1_rect_s03_o025_v01_strictmixedhard_strict_hard_chain_hf1 style=mixed_unknown chain=curve_chain flow=staged_unlock risk=watch tier=B cov=0.982 max=10 local=5 nearOuter=5 dirRisk=0.21 stripe=0.068 sts=0.931/collapse=0.117 axisRun=6 dirRun=5
- [TraceOrderKeep] score=2.528 id=c5hole_lowchoice_v1_h01r_09_nutation_strict_mixed_hard_v1_rect_s03_o025_v02_strictmixedhard_strict_hard_chain_hf1 style=mixed_unknown chain=curve_chain flow=single_axis_sweep risk=watch tier=B cov=0.992 max=11 local=7 nearOuter=6 dirRisk=0.314 stripe=0.091 sts=0.865/collapse=0.204 axisRun=9 dirRun=5
- [VisualKeep] score=1.946 id=c5hole_lowchoice_v1_h01r_12_nutation_strict_mixed_hard_v1_rect_s03_o025_v05_strictmixedhard_strict_hard_chain_hf1 style=mixed_unknown chain=patch_chain flow=local_collapse risk=high_risk tier=B cov=0.99 max=10 local=5 nearOuter=4 dirRisk=0.204 stripe=0.162 sts=0.73/collapse=0.414 axisRun=13 dirRun=13
- [Reject] score=0.606 id=c5hole_lowchoice_v1_h01r_07_nutation_strict_mixed_hard_v1_rect_s03_o025_v00_strictmixedhard_strict_hard_chain_hf1 style=mixed_unknown chain=curve_chain flow=staged_unlock risk=high_risk tier=B cov=0.985 max=12 local=5 nearOuter=5 dirRisk=0.187 stripe=0.042 sts=0.884/collapse=0.181 axisRun=6 dirRun=6
- [Reject] score=0.548 id=c5hole_lowchoice_v1_h01r_11_nutation_strict_mixed_hard_v1_rect_s03_o025_v04_strictmixedhard_strict_hard_chain_hf1 style=mixed_unknown chain=patch_chain flow=single_axis_sweep risk=high_risk tier=Drop cov=0.98 max=14 local=5 nearOuter=3 dirRisk=0.127 stripe=0.066 sts=0.837/collapse=0.344 axisRun=11 dirRun=11
