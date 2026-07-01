# PSG Pressure Trace Join Summary

Generated: 2026-07-01 08:42:04

- Source CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h03r_order148_shortroot1_source_subset.csv
- Trace metrics CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab\Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\c5hole_lowchoice_v1_h03r_order148_shortroot1_metrics.csv
- Output CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h03r_order148_shortroot1_trace_joined.csv
- Best per slot CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h03r_order148_shortroot1_trace_best_per_slot.csv
- Production keep CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h03r_order148_shortroot1_production_keep.csv
- Joined rows: 12
- Output rows: 12
- processKeep rows: 8
- visualPass rows: 7
- STS metric present rows: 12
- stsPass rows: 5
- stsKeepCandidate rows: 3
- productionKeep mode: TraceOrderPreferred
- productionKeep source: TraceOrderKeep
- productionKeep eligible rows: 3
- productionKeep rows: 3
- productionDiversity enabled: False

## Rank Class
- ProcessKeep: 1
- Reject: 4
- TraceOrderKeep: 3
- VisualKeep: 4

## Style Family
- section_unlock: 12

## Chain Language
- curve_chain: 10
- patch_chain: 2

## Flow Language
- flow_spread: 2
- local_collapse: 4
- single_axis_sweep: 4
- staged_unlock: 2

## Risk Band
- clean: 1
- high_risk: 9
- watch: 2

## Production Keep Tag Mix
- styleFamily: section_unlock=3
- chainLanguage: curve_chain=3
- flowLanguage: flow_spread=2, staged_unlock=1
- riskBand: clean=1, watch=2

## Top Rows
- [TraceOrderKeep] score=2.744 id=c5hole_lowchoice_v1_h03r_48_nutation_strict_mixed_hard_v1_rect_s15_o148_v05_strictmixedhard_strict_hard_chain_hf1 style=section_unlock chain=curve_chain flow=flow_spread risk=watch tier=B cov=0.979 max=8 local=4 nearOuter=4 dirRisk=0.109 stripe=0.063 sts=0.86/collapse=0.219 axisRun=7 dirRun=7
- [TraceOrderKeep] score=2.715 id=c5hole_lowchoice_v1_h03r_38_nutation_strict_mixed_choiceclamp_v1_rect_s15_o148_v01_strictmixedchoiceclamp_choice_clamp_chain_hf1 style=section_unlock chain=curve_chain flow=flow_spread risk=clean tier=B cov=0.99 max=9 local=5 nearOuter=4 dirRisk=0.157 stripe=0.06 sts=0.888/collapse=0.144 axisRun=6 dirRun=5
- [TraceOrderKeep] score=2.678 id=c5hole_lowchoice_v1_h03r_41_nutation_strict_mixed_choiceclamp_v1_rect_s15_o148_v04_strictmixedchoiceclamp_choice_clamp_chain_hf1 style=section_unlock chain=curve_chain flow=staged_unlock risk=watch tier=B cov=0.982 max=11 local=6 nearOuter=2 dirRisk=0.169 stripe=0.062 sts=0.843/collapse=0.173 axisRun=6 dirRun=4
- [VisualKeep] score=2.043 id=c5hole_lowchoice_v1_h03r_46_nutation_strict_mixed_hard_v1_rect_s15_o148_v03_strictmixedhard_strict_hard_chain_hf1 style=section_unlock chain=patch_chain flow=single_axis_sweep risk=high_risk tier=B cov=0.985 max=10 local=5 nearOuter=3 dirRisk=0.127 stripe=0.12 sts=0.865/collapse=0.321 axisRun=13 dirRun=13
- [VisualKeep] score=1.993 id=c5hole_lowchoice_v1_h03r_40_nutation_strict_mixed_choiceclamp_v1_rect_s15_o148_v03_strictmixedchoiceclamp_choice_clamp_chain_hf1 style=section_unlock chain=curve_chain flow=local_collapse risk=high_risk tier=B cov=0.982 max=10 local=5 nearOuter=5 dirRisk=0.187 stripe=0.089 sts=0.806/collapse=0.378 axisRun=14 dirRun=14
- [VisualKeep] score=1.984 id=c5hole_lowchoice_v1_h03r_47_nutation_strict_mixed_hard_v1_rect_s15_o148_v04_strictmixedhard_strict_hard_chain_hf1 style=section_unlock chain=curve_chain flow=single_axis_sweep risk=high_risk tier=B cov=0.986 max=11 local=6 nearOuter=3 dirRisk=0.176 stripe=0.027 sts=0.814/collapse=0.321 axisRun=10 dirRun=10
- [VisualKeep] score=1.885 id=c5hole_lowchoice_v1_h03r_45_nutation_strict_mixed_hard_v1_rect_s15_o148_v02_strictmixedhard_strict_hard_chain_hf1 style=section_unlock chain=curve_chain flow=local_collapse risk=high_risk tier=B cov=0.989 max=11 local=7 nearOuter=5 dirRisk=0.284 stripe=0.174 sts=0.795/collapse=0.378 axisRun=18 dirRun=15
- [ProcessKeep] score=1.251 id=c5hole_lowchoice_v1_h03r_39_nutation_strict_mixed_choiceclamp_v1_rect_s15_o148_v02_strictmixedchoiceclamp_choice_clamp_chain_hf1 style=section_unlock chain=curve_chain flow=local_collapse risk=high_risk tier=B cov=0.985 max=9 local=9 nearOuter=6 dirRisk=0.435 stripe=0.148 sts=0.785/collapse=0.279 axisRun=10 dirRun=6
- [Reject] score=0.643 id=c5hole_lowchoice_v1_h03r_44_nutation_strict_mixed_hard_v1_rect_s15_o148_v01_strictmixedhard_strict_hard_chain_hf1 style=section_unlock chain=curve_chain flow=staged_unlock risk=high_risk tier=B cov=0.979 max=12 local=5 nearOuter=4 dirRisk=0.157 stripe=0.059 sts=0.857/collapse=0.173 axisRun=7 dirRun=5
- [Reject] score=0.518 id=c5hole_lowchoice_v1_h03r_42_nutation_strict_mixed_choiceclamp_v1_rect_s15_o148_v05_strictmixedchoiceclamp_choice_clamp_chain_hf1 style=section_unlock chain=curve_chain flow=local_collapse risk=high_risk tier=B cov=0.979 max=12 local=6 nearOuter=5 dirRisk=0.236 stripe=0.054 sts=0.837/collapse=0.365 axisRun=11 dirRun=11
- [Reject] score=0.497 id=c5hole_lowchoice_v1_h03r_43_nutation_strict_mixed_hard_v1_rect_s15_o148_v00_strictmixedhard_strict_hard_chain_hf1 style=section_unlock chain=patch_chain flow=single_axis_sweep risk=high_risk tier=Drop cov=0.98 max=16 local=7 nearOuter=7 dirRisk=0.391 stripe=0.032 sts=0.836/collapse=0.189 axisRun=6 dirRun=5
- [Reject] score=0.449 id=c5hole_lowchoice_v1_h03r_37_nutation_strict_mixed_choiceclamp_v1_rect_s15_o148_v00_strictmixedchoiceclamp_choice_clamp_chain_hf1 style=section_unlock chain=curve_chain flow=single_axis_sweep risk=high_risk tier=Drop cov=0.98 max=15 local=7 nearOuter=7 dirRisk=0.368 stripe=0.105 sts=0.807/collapse=0.3 axisRun=8 dirRun=8
