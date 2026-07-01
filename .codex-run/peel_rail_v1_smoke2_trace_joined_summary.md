# PSG Pressure Trace Join Summary

Generated: 2026-06-28 20:10:45

- Source CSV: F:\Unityproject\ArrowLevel-Hand\Assets\ArrowMagic\SOData\Reports\DirectProcedural\peel_rail_v1_report.csv
- Trace metrics CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab\Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\peel_rail_v1_smoke2_metrics.csv
- Output CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\peel_rail_v1_smoke2_trace_joined.csv
- Best per slot CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\peel_rail_v1_smoke2_trace_best_per_slot.csv
- Production keep CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\peel_rail_v1_smoke2_production_keep.csv
- Joined rows: 4
- processKeep rows: 4
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
- ProcessKeep: 3
- TraceOrderKeep: 1

## Style Family
- peel_layered: 4

## Chain Language
- rail_chain: 4

## Flow Language
- flow_spread: 1
- peel: 2
- staged_unlock: 1

## Risk Band
- high_risk: 4

## Production Keep Tag Mix
- styleFamily: peel_layered=1
- chainLanguage: rail_chain=1
- flowLanguage: staged_unlock=1
- riskBand: high_risk=1

## Top Rows
- [TraceOrderKeep] score=2.768 id=peel_rail_v1_04_peel_rail_v1_rect_core_burst style=peel_layered chain=rail_chain flow=staged_unlock risk=high_risk tier=B cov=0.998 max=7 local=6 nearOuter=3 dirRisk=0.176 stripe=0.281 sts=0.94/collapse=0.032 axisRun=5 dirRun=3
- [ProcessKeep] score=1.39 id=peel_rail_v1_02_peel_rail_v1_rect_section_unlock style=peel_layered chain=rail_chain flow=flow_spread risk=high_risk tier=B cov=0.997 max=8 local=5 nearOuter=5 dirRisk=0.21 stripe=0.554 sts=0.796/collapse=0.106 axisRun=3 dirRun=3
- [ProcessKeep] score=1.389 id=peel_rail_v1_01_peel_rail_v1_rect_lock_buckle style=peel_layered chain=rail_chain flow=peel risk=high_risk tier=A cov=0.995 max=10 local=4 nearOuter=3 dirRisk=0.079 stripe=0.607 sts=0.888/collapse=0.132 axisRun=5 dirRun=4
- [ProcessKeep] score=1.321 id=peel_rail_v1_03_peel_rail_v1_rect_dense_weave style=peel_layered chain=rail_chain flow=peel risk=high_risk tier=B cov=0.998 max=9 local=6 nearOuter=6 dirRisk=0.266 stripe=0.433 sts=0.895/collapse=0.028 axisRun=5 dirRun=2
