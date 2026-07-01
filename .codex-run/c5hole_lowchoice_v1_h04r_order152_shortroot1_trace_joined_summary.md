# PSG Pressure Trace Join Summary

Generated: 2026-07-01 08:28:03

- Source CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h04r_order152_shortroot1_source_subset.csv
- Trace metrics CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab\Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\c5hole_lowchoice_v1_h04r_order152_shortroot1_metrics.csv
- Output CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h04r_order152_shortroot1_trace_joined.csv
- Best per slot CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h04r_order152_shortroot1_trace_best_per_slot.csv
- Production keep CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h04r_order152_shortroot1_production_keep.csv
- Joined rows: 12
- Output rows: 12
- processKeep rows: 10
- visualPass rows: 7
- STS metric present rows: 12
- stsPass rows: 4
- stsKeepCandidate rows: 3
- productionKeep mode: TraceOrderPreferred
- productionKeep source: TraceOrderKeep
- productionKeep eligible rows: 3
- productionKeep rows: 3
- productionDiversity enabled: False

## Rank Class
- ProcessKeep: 2
- Reject: 2
- TraceOrderKeep: 3
- VisualKeep: 5

## Style Family
- mixed_unknown: 12

## Chain Language
- curve_chain: 10
- patch_chain: 2

## Flow Language
- local_collapse: 6
- single_axis_sweep: 4
- staged_unlock: 2

## Risk Band
- clean: 1
- high_risk: 10
- watch: 1

## Production Keep Tag Mix
- styleFamily: mixed_unknown=3
- chainLanguage: curve_chain=3
- flowLanguage: single_axis_sweep=1, staged_unlock=2
- riskBand: clean=1, high_risk=1, watch=1

## Top Rows
- [TraceOrderKeep] score=2.824 id=c5hole_lowchoice_v1_h04r_09_nutation_strict_mixed_hard_v1_rect_s16_o152_v02_strictmixedhard_strict_hard_chain_hf1 style=mixed_unknown chain=curve_chain flow=staged_unlock risk=clean tier=A cov=0.98 max=8 local=3 nearOuter=3 dirRisk=0.03 stripe=0.1 sts=0.907/collapse=0.118 axisRun=6 dirRun=5
- [TraceOrderKeep] score=2.739 id=c5hole_lowchoice_v1_h04r_03_nutation_strict_mixed_choiceclamp_v1_rect_s16_o152_v02_strictmixedchoiceclamp_choice_clamp_chain_hf1 style=mixed_unknown chain=curve_chain flow=staged_unlock risk=watch tier=A cov=0.99 max=10 local=3 nearOuter=3 dirRisk=0.03 stripe=0.129 sts=0.903/collapse=0.188 axisRun=7 dirRun=7
- [TraceOrderKeep] score=2.642 id=c5hole_lowchoice_v1_h04r_12_nutation_strict_mixed_hard_v1_rect_s16_o152_v05_strictmixedhard_strict_hard_chain_hf1 style=mixed_unknown chain=curve_chain flow=single_axis_sweep risk=high_risk tier=B cov=0.985 max=9 local=8 nearOuter=4 dirRisk=0.326 stripe=0.051 sts=0.841/collapse=0.24 axisRun=7 dirRun=6
- [VisualKeep] score=2.083 id=c5hole_lowchoice_v1_h04r_04_nutation_strict_mixed_choiceclamp_v1_rect_s16_o152_v03_strictmixedchoiceclamp_choice_clamp_chain_hf1 style=mixed_unknown chain=curve_chain flow=local_collapse risk=high_risk tier=B cov=0.993 max=8 local=5 nearOuter=3 dirRisk=0.15 stripe=0.131 sts=0.772/collapse=0.406 axisRun=14 dirRun=14
- [VisualKeep] score=2.072 id=c5hole_lowchoice_v1_h04r_05_nutation_strict_mixed_choiceclamp_v1_rect_s16_o152_v04_strictmixedchoiceclamp_choice_clamp_chain_hf1 style=mixed_unknown chain=curve_chain flow=single_axis_sweep risk=high_risk tier=B cov=0.99 max=8 local=5 nearOuter=5 dirRisk=0.187 stripe=0.045 sts=0.862/collapse=0.331 axisRun=11 dirRun=11
- [VisualKeep] score=2.004 id=c5hole_lowchoice_v1_h04r_10_nutation_strict_mixed_hard_v1_rect_s16_o152_v03_strictmixedhard_strict_hard_chain_hf1 style=mixed_unknown chain=patch_chain flow=single_axis_sweep risk=high_risk tier=B cov=0.98 max=10 local=6 nearOuter=4 dirRisk=0.229 stripe=0.105 sts=0.79/collapse=0.299 axisRun=9 dirRun=7
- [VisualKeep] score=1.985 id=c5hole_lowchoice_v1_h04r_02_nutation_strict_mixed_choiceclamp_v1_rect_s16_o152_v01_strictmixedchoiceclamp_choice_clamp_chain_hf1 style=mixed_unknown chain=curve_chain flow=local_collapse risk=high_risk tier=B cov=0.989 max=10 local=7 nearOuter=4 dirRisk=0.301 stripe=0.104 sts=0.81/collapse=0.252 axisRun=6 dirRun=6
- [VisualKeep] score=1.926 id=c5hole_lowchoice_v1_h04r_11_nutation_strict_mixed_hard_v1_rect_s16_o152_v04_strictmixedhard_strict_hard_chain_hf1 style=mixed_unknown chain=curve_chain flow=local_collapse risk=high_risk tier=B cov=0.99 max=10 local=6 nearOuter=5 dirRisk=0.306 stripe=0.091 sts=0.726/collapse=0.45 axisRun=12 dirRun=9
- [ProcessKeep] score=1.256 id=c5hole_lowchoice_v1_h04r_06_nutation_strict_mixed_choiceclamp_v1_rect_s16_o152_v05_strictmixedchoiceclamp_choice_clamp_chain_hf1 style=mixed_unknown chain=curve_chain flow=local_collapse risk=high_risk tier=B cov=0.983 max=11 local=10 nearOuter=5 dirRisk=0.453 stripe=0.117 sts=0.845/collapse=0.217 axisRun=8 dirRun=5
- [ProcessKeep] score=1.207 id=c5hole_lowchoice_v1_h04r_01_nutation_strict_mixed_choiceclamp_v1_rect_s16_o152_v00_strictmixedchoiceclamp_choice_clamp_chain_hf1 style=mixed_unknown chain=curve_chain flow=local_collapse risk=high_risk tier=B cov=0.986 max=10 local=9 nearOuter=5 dirRisk=0.405 stripe=0.063 sts=0.768/collapse=0.388 axisRun=14 dirRun=11
- [Reject] score=0.52 id=c5hole_lowchoice_v1_h04r_08_nutation_strict_mixed_hard_v1_rect_s16_o152_v01_strictmixedhard_strict_hard_chain_hf1 style=mixed_unknown chain=patch_chain flow=local_collapse risk=high_risk tier=Drop cov=0.986 max=14 local=5 nearOuter=3 dirRisk=0.15 stripe=0.121 sts=0.76/collapse=0.382 axisRun=14 dirRun=13
- [Reject] score=0.486 id=c5hole_lowchoice_v1_h04r_07_nutation_strict_mixed_hard_v1_rect_s16_o152_v00_strictmixedhard_strict_hard_chain_hf1 style=mixed_unknown chain=curve_chain flow=single_axis_sweep risk=high_risk tier=Drop cov=0.99 max=13 local=8 nearOuter=4 dirRisk=0.303 stripe=0.051 sts=0.816/collapse=0.339 axisRun=9 dirRun=9
