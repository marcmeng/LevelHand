# PSG Pressure Trace Join Summary

Generated: 2026-06-28 13:21:24

- Source CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\nutation-peel\Assets\ArrowMagic\SOData\Reports\DirectProcedural\nutation_peel_v1_report.csv
- Trace metrics CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab\Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\nutation_peel_v1c_wt_smoke_metrics.csv
- Output CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\nutation-peel\.codex-run\nutation_peel_v1c_wt_smoke_trace_joined.csv
- Best per slot CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\nutation-peel\.codex-run\nutation_peel_v1c_wt_smoke_trace_best_per_slot.csv
- Production keep CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\nutation-peel\.codex-run\nutation_peel_v1c_wt_smoke_production_keep.csv
- Joined rows: 4
- processKeep rows: 2
- visualPass rows: 2
- STS metric present rows: 4
- stsPass rows: 2
- stsKeepCandidate rows: 2
- productionKeep mode: TraceOrderPreferred
- productionKeep source: TraceOrderKeep
- productionKeep eligible rows: 2
- productionKeep rows: 2
- productionDiversity enabled: False

## Rank Class
- Reject: 2
- TraceOrderKeep: 2

## Style Family
- peel_layered: 4

## Chain Language
- peel_layer_chain: 4

## Flow Language
- local_collapse: 1
- single_axis_sweep: 1
- staged_unlock: 2

## Risk Band
- high_risk: 2
- watch: 2

## Production Keep Tag Mix
- styleFamily: peel_layered=2
- chainLanguage: peel_layer_chain=2
- flowLanguage: staged_unlock=2
- riskBand: watch=2

## Top Rows
- [TraceOrderKeep] score=2.57 id=nutation_peel_v1_01_nutation_peel_rect_lock_buckle style=peel_layered chain=peel_layer_chain flow=staged_unlock risk=watch tier=B cov=0.977 max=9 local=6 nearOuter=3 dirRisk=0.176 stripe=0 sts=0.885/collapse=0.143 axisRun=6 dirRun=5
- [TraceOrderKeep] score=2.55 id=nutation_peel_v1_04_nutation_peel_rect_core_burst style=peel_layered chain=peel_layer_chain flow=staged_unlock risk=watch tier=B cov=0.972 max=7 local=6 nearOuter=4 dirRisk=0.206 stripe=0.017 sts=0.867/collapse=0.211 axisRun=7 dirRun=7
- [Reject] score=0.307 id=nutation_peel_v1_02_nutation_peel_rect_section_unlock style=peel_layered chain=peel_layer_chain flow=local_collapse risk=high_risk tier=B cov=0.966 max=11 local=8 nearOuter=6 dirRisk=0.41 stripe=0 sts=0.773/collapse=0.288 axisRun=9 dirRun=6
- [Reject] score=0.294 id=nutation_peel_v1_03_nutation_peel_rect_dense_weave style=peel_layered chain=peel_layer_chain flow=single_axis_sweep risk=high_risk tier=Drop cov=0.976 max=12 local=8 nearOuter=5 dirRisk=0.333 stripe=0.023 sts=0.822/collapse=0.282 axisRun=16 dirRun=7
