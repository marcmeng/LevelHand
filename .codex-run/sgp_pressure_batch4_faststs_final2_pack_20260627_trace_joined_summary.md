# PSG Pressure Trace Join Summary

Generated: 2026-06-28 00:40:31

- Source CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\sgp_pressure_batch4_faststs_final2_pack_20260627_source_combined.csv
- Trace metrics CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab\Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\sgp_pressure_batch4_faststs_20260627_metrics.csv
- Output CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\sgp_pressure_batch4_faststs_final2_pack_20260627_trace_joined.csv
- Best per slot CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\sgp_pressure_batch4_faststs_final2_pack_20260627_trace_best_per_slot.csv
- Production keep CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\sgp_pressure_batch4_faststs_final2_pack_20260627_production_keep.csv
- Joined rows: 22
- processKeep rows: 19
- visualPass rows: 15
- STS metric present rows: 22
- stsPass rows: 16
- stsKeepCandidate rows: 15
- productionKeep mode: TraceOrderPreferred
- productionKeep source: TraceOrderKeep
- productionKeep eligible rows: 15
- productionKeep rows: 15
- productionDiversity enabled: False

## Rank Class
- ProcessKeep: 2
- Reject: 3
- TraceOrderKeep: 15
- VisualKeep: 2

## Style Family
- core_burst: 7
- dense_weave: 7
- lock_buckle: 7
- section_unlock: 1

## Chain Language
- core_cluster: 2
- lock_cluster: 5
- short_patchwork: 12
- woven_medium: 3

## Flow Language
- flow_spread: 3
- local_collapse: 5
- region_alternating_flow: 3
- single_axis_sweep: 3
- staged_unlock: 8

## Risk Band
- clean: 4
- high_risk: 11
- watch: 7

## Production Keep Tag Mix
- styleFamily: core_burst=6, dense_weave=3, lock_buckle=6
- chainLanguage: core_cluster=2, lock_cluster=4, short_patchwork=8, woven_medium=1
- flowLanguage: flow_spread=3, local_collapse=2, region_alternating_flow=3, staged_unlock=7
- riskBand: clean=4, high_risk=4, watch=7

## Top Rows
- [TraceOrderKeep] score=2.745 id=sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle style=lock_buckle chain=lock_cluster flow=staged_unlock risk=high_risk tier=A cov=0.991 max=8 local=3 nearOuter=3 dirRisk=0.03 stripe=0.233 sts=0.904/collapse=0.094 axisRun=4 dirRun=4
- [TraceOrderKeep] score=2.745 id=sgp_pressure_hard_review6_01_sgp_pressure_hard_rect_lock_buckle_a style=lock_buckle chain=lock_cluster flow=staged_unlock risk=high_risk tier=A cov=0.991 max=8 local=3 nearOuter=3 dirRisk=0.03 stripe=0.233 sts=0.904/collapse=0.094 axisRun=4 dirRun=4
- [TraceOrderKeep] score=2.732 id=sgp_pressure_hard_interference6_04_sgp_pressure_hard_rect_interfere_lock_buckle_b style=lock_buckle chain=short_patchwork flow=staged_unlock risk=clean tier=A cov=0.985 max=7 local=4 nearOuter=3 dirRisk=0.079 stripe=0.104 sts=0.848/collapse=0.072 axisRun=4 dirRun=4
- [TraceOrderKeep] score=2.718 id=sgp_pressure_hard_review6_04_sgp_pressure_hard_rect_lock_buckle_b style=lock_buckle chain=lock_cluster flow=region_alternating_flow risk=clean tier=A cov=0.985 max=8 local=4 nearOuter=3 dirRisk=0.079 stripe=0.104 sts=0.909/collapse=0.05 axisRun=4 dirRun=4
- [TraceOrderKeep] score=2.693 id=sgp_pressure_hard_interference6_03_sgp_pressure_hard_rect_interfere_dense_weave_a style=dense_weave chain=short_patchwork flow=region_alternating_flow risk=clean tier=B cov=0.986 max=8 local=5 nearOuter=4 dirRisk=0.157 stripe=0 sts=0.888/collapse=0.113 axisRun=6 dirRun=4
- [TraceOrderKeep] score=2.668 id=sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst style=core_burst chain=short_patchwork flow=local_collapse risk=high_risk tier=B cov=0.99 max=8 local=4 nearOuter=4 dirRisk=0.155 stripe=0.086 sts=0.805/collapse=0.253 axisRun=6 dirRun=6
- [TraceOrderKeep] score=2.655 id=sgp_pressure_hard_review6_02_sgp_pressure_hard_rect_core_burst_a style=core_burst chain=short_patchwork flow=staged_unlock risk=clean tier=B cov=0.982 max=8 local=6 nearOuter=3 dirRisk=0.176 stripe=0.075 sts=0.892/collapse=0.145 axisRun=5 dirRun=5
- [TraceOrderKeep] score=2.651 id=sgp_pressure_hard_interference6_06_sgp_pressure_hard_rect_interfere_dense_weave_b style=dense_weave chain=short_patchwork flow=staged_unlock risk=watch tier=B cov=0.979 max=8 local=4 nearOuter=4 dirRisk=0.109 stripe=0.028 sts=0.896/collapse=0.124 axisRun=7 dirRun=5
- [TraceOrderKeep] score=2.633 id=sgp_pressure_hard_interference_v2_six_06_sgp_pressure_hard_rect_interfere_v2_dense_weave_b style=dense_weave chain=woven_medium flow=staged_unlock risk=watch tier=B cov=0.975 max=8 local=4 nearOuter=4 dirRisk=0.109 stripe=0.015 sts=0.883/collapse=0.17 axisRun=7 dirRun=5
- [TraceOrderKeep] score=2.627 id=sgp_pressure_hard_interference6_02_sgp_pressure_hard_rect_interfere_core_burst_a style=core_burst chain=core_cluster flow=flow_spread risk=watch tier=B cov=0.98 max=9 local=5 nearOuter=4 dirRisk=0.157 stripe=0.061 sts=0.947/collapse=0.037 axisRun=4 dirRun=4
- [TraceOrderKeep] score=2.61 id=sgp_pressure_hard_review6_05_sgp_pressure_hard_rect_core_burst_b style=core_burst chain=short_patchwork flow=staged_unlock risk=watch tier=B cov=0.989 max=10 local=7 nearOuter=3 dirRisk=0.224 stripe=0.044 sts=0.904/collapse=0.114 axisRun=6 dirRun=5
- [TraceOrderKeep] score=2.609 id=sgp_pressure_hard_interference_v2_six_01_sgp_pressure_hard_rect_interfere_v2_lock_buckle_a style=lock_buckle chain=lock_cluster flow=local_collapse risk=high_risk tier=B cov=0.984 max=8 local=5 nearOuter=5 dirRisk=0.234 stripe=0.055 sts=0.762/collapse=0.196 axisRun=5 dirRun=5
