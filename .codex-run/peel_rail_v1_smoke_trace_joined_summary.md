# PSG Pressure Trace Join Summary

Generated: 2026-06-28 19:45:30

- Source CSV: F:\Unityproject\ArrowLevel-Hand\Assets\ArrowMagic\SOData\Reports\DirectProcedural\peel_rail_v1_report.csv
- Trace metrics CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab\Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\peel_rail_v1_smoke_metrics.csv
- Output CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\peel_rail_v1_smoke_trace_joined.csv
- Best per slot CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\peel_rail_v1_smoke_trace_best_per_slot.csv
- Production keep CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\peel_rail_v1_smoke_production_keep.csv
- Joined rows: 4
- processKeep rows: 0
- visualPass rows: 0
- STS metric present rows: 4
- stsPass rows: 2
- stsKeepCandidate rows: 0
- productionKeep mode: TraceOrderPreferred
- productionKeep source: TraceOrderKeep
- productionKeep eligible rows: 0
- productionKeep rows: 0
- productionDiversity enabled: False

## Rank Class
- Reject: 4

## Style Family
- peel_layered: 4

## Chain Language
- curve_chain: 4

## Flow Language
- flow_spread: 1
- local_collapse: 1
- region_alternating_flow: 2

## Risk Band
- high_risk: 4

## Top Rows
- [Reject] score=0.521 id=peel_rail_v1_04_peel_rail_v1_rect_core_burst style=peel_layered chain=curve_chain flow=flow_spread risk=high_risk tier=Drop cov=0.952 max=13 local=3 nearOuter=3 dirRisk=0.03 stripe=0 sts=0.952/collapse=0.041 axisRun=4 dirRun=4
- [Reject] score=0.506 id=peel_rail_v1_01_peel_rail_v1_rect_lock_buckle style=peel_layered chain=curve_chain flow=region_alternating_flow risk=high_risk tier=Drop cov=0.966 max=14 local=3 nearOuter=3 dirRisk=0.03 stripe=0 sts=0.894/collapse=0.075 axisRun=6 dirRun=3
- [Reject] score=0.412 id=peel_rail_v1_03_peel_rail_v1_rect_dense_weave style=peel_layered chain=curve_chain flow=region_alternating_flow risk=high_risk tier=Drop cov=0.963 max=16 local=6 nearOuter=3 dirRisk=0.199 stripe=0 sts=0.841/collapse=0.121 axisRun=5 dirRun=4
- [Reject] score=0.399 id=peel_rail_v1_02_peel_rail_v1_rect_section_unlock style=peel_layered chain=curve_chain flow=local_collapse risk=high_risk tier=Drop cov=0.964 max=13 local=5 nearOuter=4 dirRisk=0.204 stripe=0 sts=0.8/collapse=0.155 axisRun=5 dirRun=4
