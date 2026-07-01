# PSG Pressure Trace Join Summary

Generated: 2026-06-28 11:25:03

- Source CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\nutation-flow-peel\Assets\ArrowMagic\SOData\Reports\DirectProcedural\sgp_pressure_hard_trial_report.csv
- Trace metrics CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab\Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\nutation_compare_psg_trial_v1_wt_peel_metrics.csv
- Output CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\nutation-flow-peel\.codex-run\nutation_compare_psg_trial_v1_wt_peel_trace_joined.csv
- Best per slot CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\nutation-flow-peel\.codex-run\nutation_compare_psg_trial_v1_wt_peel_trace_best_per_slot.csv
- Production keep CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\nutation-flow-peel\.codex-run\nutation_compare_psg_trial_v1_wt_peel_production_keep.csv
- Joined rows: 4
- processKeep rows: 3
- visualPass rows: 1
- STS metric present rows: 4
- stsPass rows: 2
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

## Style Family
- core_burst: 1
- dense_weave: 1
- lock_buckle: 1
- section_unlock: 1

## Chain Language
- lock_cluster: 1
- short_patchwork: 2
- woven_medium: 1

## Flow Language
- local_collapse: 2
- single_axis_sweep: 1
- staged_unlock: 1

## Risk Band
- high_risk: 4

## Production Keep Tag Mix
- styleFamily: core_burst=1, lock_buckle=1
- chainLanguage: lock_cluster=1, short_patchwork=1
- flowLanguage: local_collapse=1, staged_unlock=1
- riskBand: high_risk=2

## Top Rows
- [TraceOrderKeep] score=2.745 id=sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle style=lock_buckle chain=lock_cluster flow=staged_unlock risk=high_risk tier=A cov=0.991 max=8 local=3 nearOuter=3 dirRisk=0.03 stripe=0.233 sts=0.904/collapse=0.094 axisRun=4 dirRun=4
- [TraceOrderKeep] score=2.668 id=sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst style=core_burst chain=short_patchwork flow=local_collapse risk=high_risk tier=B cov=0.99 max=8 local=4 nearOuter=4 dirRisk=0.155 stripe=0.086 sts=0.805/collapse=0.253 axisRun=6 dirRun=6
- [ProcessKeep] score=1.223 id=sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave style=dense_weave chain=woven_medium flow=local_collapse risk=high_risk tier=B cov=0.978 max=6 local=13 nearOuter=9 dirRisk=0.52 stripe=0.131 sts=0.861/collapse=0.248 axisRun=8 dirRun=8
- [Reject] score=0.44 id=sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock style=section_unlock chain=short_patchwork flow=single_axis_sweep risk=high_risk tier=Drop cov=0.994 max=16 local=7 nearOuter=4 dirRisk=0.254 stripe=0.096 sts=0.786/collapse=0.326 axisRun=13 dirRun=12
