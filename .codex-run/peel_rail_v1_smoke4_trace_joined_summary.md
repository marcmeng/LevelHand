# PSG Pressure Trace Join Summary

Generated: 2026-06-28 20:31:52

- Source CSV: F:\Unityproject\ArrowLevel-Hand\Assets\ArrowMagic\SOData\Reports\DirectProcedural\peel_rail_v1_report.csv
- Trace metrics CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab\Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\peel_rail_v1_smoke4_metrics.csv
- Output CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\peel_rail_v1_smoke4_trace_joined.csv
- Best per slot CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\peel_rail_v1_smoke4_trace_best_per_slot.csv
- Production keep CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\peel_rail_v1_smoke4_production_keep.csv
- Joined rows: 4
- processKeep rows: 3
- visualPass rows: 2
- STS metric present rows: 4
- stsPass rows: 3
- stsKeepCandidate rows: 2
- productionKeep mode: TraceOrderPreferred
- productionKeep source: TraceOrderKeep
- productionKeep eligible rows: 2
- productionKeep rows: 2
- productionDiversity enabled: False

## Rank Class
- Reject: 1
- TraceOrderKeep: 2
- VisualKeep: 1

## Style Family
- peel_layered: 4

## Chain Language
- rail_chain: 4

## Flow Language
- flow_spread: 2
- region_alternating_flow: 1
- staged_unlock: 1

## Risk Band
- high_risk: 3
- watch: 1

## Production Keep Tag Mix
- styleFamily: peel_layered=2
- chainLanguage: rail_chain=2
- flowLanguage: flow_spread=2
- riskBand: high_risk=1, watch=1

## Top Rows
- [TraceOrderKeep] score=2.735 id=peel_rail_v1_02_peel_rail_v1_rect_section_unlock style=peel_layered chain=rail_chain flow=flow_spread risk=watch tier=A cov=0.995 max=9 local=3 nearOuter=3 dirRisk=0.03 stripe=0.132 sts=0.927/collapse=0.104 axisRun=6 dirRun=5
- [TraceOrderKeep] score=2.614 id=peel_rail_v1_01_peel_rail_v1_rect_lock_buckle style=peel_layered chain=rail_chain flow=flow_spread risk=high_risk tier=B cov=0.988 max=9 local=6 nearOuter=6 dirRisk=0.266 stripe=0.075 sts=0.89/collapse=0.046 axisRun=4 dirRun=4
- [VisualKeep] score=1.976 id=peel_rail_v1_04_peel_rail_v1_rect_core_burst style=peel_layered chain=rail_chain flow=staged_unlock risk=high_risk tier=B cov=0.992 max=10 local=4 nearOuter=4 dirRisk=0.132 stripe=0.141 sts=0.835/collapse=0.21 axisRun=8 dirRun=5
- [Reject] score=0.528 id=peel_rail_v1_03_peel_rail_v1_rect_dense_weave style=peel_layered chain=rail_chain flow=region_alternating_flow risk=high_risk tier=Drop cov=0.994 max=13 local=5 nearOuter=5 dirRisk=0.187 stripe=0.06 sts=0.919/collapse=0.092 axisRun=5 dirRun=5
