# PSG Pressure Trace Join Summary

Generated: 2026-06-28 11:35:39

- Source CSV: F:\Unityproject\ArrowLevel-Hand\_CodexRun\campaign500_psg_normal_prod200_unified_qtrace_candidate_pool.csv
- Trace metrics CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab\Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\campaign500_psg_normal_prod200_unified_qtrace_metrics.csv
- Output CSV: F:\Unityproject\ArrowLevel-Hand\_CodexRun\campaign500_psg_normal_prod200_unified_qtrace_trace_joined.csv
- Best per slot CSV: F:\Unityproject\ArrowLevel-Hand\_CodexRun\campaign500_psg_normal_prod200_unified_qtrace_best_by_slot_raw.csv
- Production keep CSV: F:\Unityproject\ArrowLevel-Hand\_CodexRun\campaign500_psg_normal_prod200_unified_qtrace_production_keep_raw.csv
- Joined rows: 200
- processKeep rows: 72
- visualPass rows: 29
- STS metric present rows: 200
- stsPass rows: 50
- stsKeepCandidate rows: 14
- productionKeep mode: TraceOrderPreferred
- productionKeep source: TraceOrderKeep
- productionKeep eligible rows: 14
- productionKeep rows: 14
- productionDiversity enabled: True
- productionDiversity strict: False
- productionDiversity caps: maxRows=0, maxPerStyleFamily=0, maxPerFlowLanguage=0, maxPerChainLanguage=0, maxPerStyleFlow=0, maxPerStyleSignature=0, maxHighRiskRows=0
- productionDiversity dropped rows: 0
- productionDiversity backfilled rows: 0

## Rank Class
- ProcessKeep: 42
- Reject: 128
- TraceOrderKeep: 14
- VisualKeep: 16

## Style Family
- dense_weave: 25
- maze_long_chain: 16
- mixed_unknown: 68
- section_unlock: 65
- sweep: 26

## Chain Language
- long_chain: 16
- outer_edge_chain: 15
- short_patchwork: 168
- woven_medium: 1

## Flow Language
- flow_spread: 14
- local_collapse: 107
- peel: 1
- region_alternating_flow: 3
- single_axis_sweep: 58
- staged_unlock: 17

## Risk Band
- clean: 1
- high_risk: 190
- watch: 9

## Production Keep Tag Mix
- styleFamily: dense_weave=2, maze_long_chain=1, mixed_unknown=5, section_unlock=5, sweep=1
- chainLanguage: long_chain=1, short_patchwork=13
- flowLanguage: flow_spread=2, local_collapse=3, peel=1, region_alternating_flow=1, single_axis_sweep=1, staged_unlock=6
- riskBand: clean=1, high_risk=4, watch=9

## Top Rows
- [TraceOrderKeep] score=2.725 id=campaign500_psg_prod200_c02_o011_s050_n050_65_sgp_pressure_hard_campaign500_pure_relaxed_l102_dense style=dense_weave chain=short_patchwork flow=region_alternating_flow risk=clean tier=A cov=0.982 max=8 local=3 nearOuter=3 dirRisk=0.03 stripe=0.058 sts=0.882/collapse=0.072 axisRun=4 dirRun=4
- [TraceOrderKeep] score=2.661 id=campaign500_psg_prod200_c01_o011_s000_n050_88_sgp_pressure_hard_campaign500_pure_relaxed_l041_dense style=dense_weave chain=short_patchwork flow=staged_unlock risk=watch tier=B cov=0.985 max=7 local=5 nearOuter=5 dirRisk=0.21 stripe=0.073 sts=0.901/collapse=0.168 axisRun=6 dirRun=6
- [TraceOrderKeep] score=2.655 id=campaign500_psg_prod200_c02_o011_s050_n050_61_sgp_pressure_hard_campaign500_layout_soft_l100_lock style=mixed_unknown chain=short_patchwork flow=staged_unlock risk=watch tier=B cov=0.989 max=7 local=5 nearOuter=5 dirRisk=0.21 stripe=0.134 sts=0.831/collapse=0.235 axisRun=6 dirRun=6
- [TraceOrderKeep] score=2.64 id=campaign500_psg_prod200_c01_o011_s000_n050_14_sgp_pressure_hard_campaign500_layout_soft_l016_lock style=mixed_unknown chain=short_patchwork flow=staged_unlock risk=watch tier=A cov=0.982 max=10 local=3 nearOuter=3 dirRisk=0.03 stripe=0.084 sts=0.918/collapse=0.142 axisRun=6 dirRun=6
- [TraceOrderKeep] score=2.639 id=campaign500_psg_prod200_c02_o011_s050_n050_111_sgp_pressure_hard_campaign500_language_b_l116_lock style=mixed_unknown chain=short_patchwork flow=peel risk=watch tier=B cov=0.994 max=8 local=5 nearOuter=5 dirRisk=0.187 stripe=0.155 sts=0.844/collapse=0.244 axisRun=7 dirRun=7
- [TraceOrderKeep] score=2.614 id=campaign500_psg_prod200_c01_o011_s000_n050_08_sgp_pressure_hard_campaign500_language_b_l015_section style=section_unlock chain=short_patchwork flow=single_axis_sweep risk=high_risk tier=B cov=0.992 max=7 local=7 nearOuter=7 dirRisk=0.368 stripe=0.23 sts=0.873/collapse=0.169 axisRun=6 dirRun=6
- [TraceOrderKeep] score=2.592 id=campaign500_psg_prod200_c01_o011_s000_n050_20_sgp_pressure_hard_campaign500_layout_soft_l019_lock style=mixed_unknown chain=short_patchwork flow=local_collapse risk=high_risk tier=B cov=0.985 max=10 local=4 nearOuter=4 dirRisk=0.155 stripe=0.015 sts=0.833/collapse=0.184 axisRun=5 dirRun=5
- [TraceOrderKeep] score=2.589 id=campaign500_psg_prod200_c02_o011_s050_n050_77_sgp_pressure_hard_campaign500_language_b_l106_section style=section_unlock chain=short_patchwork flow=staged_unlock risk=watch tier=B cov=0.991 max=8 local=6 nearOuter=5 dirRisk=0.282 stripe=0.124 sts=0.825/collapse=0.197 axisRun=6 dirRun=6
- [TraceOrderKeep] score=2.576 id=campaign500_psg_prod200_c02_o011_s050_n050_106_sgp_pressure_hard_campaign500_language_a_l115_section style=section_unlock chain=short_patchwork flow=local_collapse risk=high_risk tier=B cov=0.981 max=8 local=5 nearOuter=4 dirRisk=0.204 stripe=0.104 sts=0.763/collapse=0.335 axisRun=7 dirRun=7
- [TraceOrderKeep] score=2.576 id=campaign500_psg_prod200_c01_o011_s000_n050_165_sgp_pressure_hard_campaign500_layout_soft_l068_lock style=mixed_unknown chain=short_patchwork flow=staged_unlock risk=watch tier=B cov=0.988 max=9 local=6 nearOuter=4 dirRisk=0.206 stripe=0.108 sts=0.863/collapse=0.228 axisRun=7 dirRun=7
- [TraceOrderKeep] score=2.532 id=campaign500_psg_prod200_c01_o011_s000_n050_161_sgp_pressure_hard_campaign500_layout_soft_l066_sweep style=sweep chain=short_patchwork flow=staged_unlock risk=watch tier=B cov=0.978 max=9 local=5 nearOuter=4 dirRisk=0.157 stripe=0.13 sts=0.861/collapse=0.176 axisRun=8 dirRun=5
- [TraceOrderKeep] score=2.506 id=campaign500_psg_prod200_c04_o011_s150_n050_89_sgp_pressure_hard_campaign500_layout_soft_l254_section style=section_unlock chain=short_patchwork flow=flow_spread risk=watch tier=B cov=0.985 max=10 local=5 nearOuter=4 dirRisk=0.205 stripe=0.131 sts=0.834/collapse=0.249 axisRun=7 dirRun=7
