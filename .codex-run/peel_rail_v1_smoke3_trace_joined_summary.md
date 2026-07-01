# PSG Pressure Trace Join Summary

Generated: 2026-06-28 20:19:55

- Source CSV: F:\Unityproject\ArrowLevel-Hand\Assets\ArrowMagic\SOData\Reports\DirectProcedural\peel_rail_v1_report.csv
- Trace metrics CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab\Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\peel_rail_v1_smoke3_metrics.csv
- Output CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\peel_rail_v1_smoke3_trace_joined.csv
- Best per slot CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\peel_rail_v1_smoke3_trace_best_per_slot.csv
- Production keep CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\peel_rail_v1_smoke3_production_keep.csv
- Joined rows: 4
- processKeep rows: 2
- visualPass rows: 0
- STS metric present rows: 4
- stsPass rows: 4
- stsKeepCandidate rows: 1
- productionKeep mode: TraceOrderPreferred
- productionKeep source: TraceOrderKeep
- productionKeep eligible rows: 1
- productionKeep rows: 1
- productionDiversity enabled: False

## Rank Class
- ProcessKeep: 1
- Reject: 2
- TraceOrderKeep: 1

## Style Family
- peel_layered: 4

## Chain Language
- rail_chain: 4

## Flow Language
- flow_spread: 2
- region_alternating_flow: 1
- staged_unlock: 1

## Risk Band
- high_risk: 4

## Production Keep Tag Mix
- styleFamily: peel_layered=1
- chainLanguage: rail_chain=1
- flowLanguage: region_alternating_flow=1
- riskBand: high_risk=1

## Top Rows
- [TraceOrderKeep] score=2.649 id=peel_rail_v1_04_peel_rail_v1_rect_core_burst style=peel_layered chain=rail_chain flow=region_alternating_flow risk=high_risk tier=B cov=0.997 max=10 local=6 nearOuter=3 dirRisk=0.176 stripe=0.286 sts=0.914/collapse=0.007 axisRun=4 dirRun=3
- [ProcessKeep] score=1.454 id=peel_rail_v1_02_peel_rail_v1_rect_section_unlock style=peel_layered chain=rail_chain flow=staged_unlock risk=high_risk tier=A cov=1 max=9 local=3 nearOuter=3 dirRisk=0.03 stripe=0.352 sts=0.938/collapse=0.059 axisRun=5 dirRun=4
- [Reject] score=0.636 id=peel_rail_v1_01_peel_rail_v1_rect_lock_buckle style=peel_layered chain=rail_chain flow=flow_spread risk=high_risk tier=B cov=0.996 max=12 local=4 nearOuter=3 dirRisk=0.102 stripe=0.242 sts=0.911/collapse=0.077 axisRun=5 dirRun=4
- [Reject] score=0.611 id=peel_rail_v1_03_peel_rail_v1_rect_dense_weave style=peel_layered chain=rail_chain flow=flow_spread risk=high_risk tier=B cov=0.992 max=11 local=4 nearOuter=4 dirRisk=0.109 stripe=0.318 sts=0.945/collapse=0.077 axisRun=7 dirRun=3
