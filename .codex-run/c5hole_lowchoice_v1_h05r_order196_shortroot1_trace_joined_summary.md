# PSG Pressure Trace Join Summary

Generated: 2026-07-01 09:05:23

- Source CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h05r_order196_shortroot1_source_subset.csv
- Trace metrics CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab\Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\c5hole_lowchoice_v1_h05r_order196_shortroot1_metrics.csv
- Output CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h05r_order196_shortroot1_trace_joined.csv
- Best per slot CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h05r_order196_shortroot1_trace_best_per_slot.csv
- Production keep CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h05r_order196_shortroot1_production_keep.csv
- Joined rows: 12
- Output rows: 12
- processKeep rows: 7
- visualPass rows: 6
- STS metric present rows: 12
- stsPass rows: 1
- stsKeepCandidate rows: 1
- productionKeep mode: TraceOrderPreferred
- productionKeep source: TraceOrderKeep
- productionKeep eligible rows: 1
- productionKeep rows: 1
- productionDiversity enabled: False

## Rank Class
- ProcessKeep: 1
- Reject: 5
- TraceOrderKeep: 1
- VisualKeep: 5

## Style Family
- mixed_unknown: 12

## Chain Language
- curve_chain: 10
- patch_chain: 2

## Flow Language
- local_collapse: 4
- single_axis_sweep: 5
- staged_unlock: 3

## Risk Band
- high_risk: 9
- watch: 3

## Production Keep Tag Mix
- styleFamily: mixed_unknown=1
- chainLanguage: curve_chain=1
- flowLanguage: staged_unlock=1
- riskBand: watch=1

## Top Rows
- [TraceOrderKeep] score=2.563 id=c5hole_lowchoice_v1_h05r_05_nutation_strict_mixed_choiceclamp_v1_rect_s20_o196_v04_strictmixedchoiceclamp_choice_clamp_chain_hf1 style=mixed_unknown chain=curve_chain flow=staged_unlock risk=watch tier=B cov=0.982 max=11 local=7 nearOuter=3 dirRisk=0.224 stripe=0.075 sts=0.842/collapse=0.22 axisRun=7 dirRun=7
- [VisualKeep] score=2.078 id=c5hole_lowchoice_v1_h05r_09_nutation_strict_mixed_peak_v1_rect_s20_o196_v02_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=curve_chain flow=single_axis_sweep risk=high_risk tier=A cov=0.99 max=10 local=3 nearOuter=3 dirRisk=0.03 stripe=0.099 sts=0.849/collapse=0.318 axisRun=10 dirRun=10
- [VisualKeep] score=2.043 id=c5hole_lowchoice_v1_h05r_07_nutation_strict_mixed_peak_v1_rect_s20_o196_v00_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=curve_chain flow=staged_unlock risk=watch tier=B cov=0.988 max=11 local=4 nearOuter=4 dirRisk=0.132 stripe=0.082 sts=0.808/collapse=0.233 axisRun=7 dirRun=6
- [VisualKeep] score=2.028 id=c5hole_lowchoice_v1_h05r_10_nutation_strict_mixed_peak_v1_rect_s20_o196_v03_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=curve_chain flow=single_axis_sweep risk=high_risk tier=B cov=0.986 max=10 local=6 nearOuter=3 dirRisk=0.199 stripe=0.079 sts=0.813/collapse=0.318 axisRun=8 dirRun=8
- [VisualKeep] score=2.016 id=c5hole_lowchoice_v1_h05r_03_nutation_strict_mixed_choiceclamp_v1_rect_s20_o196_v02_strictmixedchoiceclamp_choice_clamp_chain_hf1 style=mixed_unknown chain=curve_chain flow=single_axis_sweep risk=high_risk tier=B cov=0.98 max=10 local=5 nearOuter=4 dirRisk=0.157 stripe=0.098 sts=0.854/collapse=0.312 axisRun=13 dirRun=8
- [VisualKeep] score=1.993 id=c5hole_lowchoice_v1_h05r_01_nutation_strict_mixed_choiceclamp_v1_rect_s20_o196_v00_strictmixedchoiceclamp_choice_clamp_chain_hf1 style=mixed_unknown chain=curve_chain flow=staged_unlock risk=watch tier=B cov=0.991 max=10 local=6 nearOuter=6 dirRisk=0.289 stripe=0.09 sts=0.807/collapse=0.226 axisRun=6 dirRun=6
- [ProcessKeep] score=1.268 id=c5hole_lowchoice_v1_h05r_06_nutation_strict_mixed_choiceclamp_v1_rect_s20_o196_v05_strictmixedchoiceclamp_choice_clamp_chain_hf1 style=mixed_unknown chain=curve_chain flow=local_collapse risk=high_risk tier=B cov=0.983 max=9 local=13 nearOuter=7 dirRisk=0.513 stripe=0.071 sts=0.814/collapse=0.296 axisRun=11 dirRun=6
- [Reject] score=0.59 id=c5hole_lowchoice_v1_h05r_11_nutation_strict_mixed_peak_v1_rect_s20_o196_v04_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=curve_chain flow=local_collapse risk=high_risk tier=B cov=0.982 max=12 local=4 nearOuter=3 dirRisk=0.079 stripe=0.127 sts=0.81/collapse=0.351 axisRun=10 dirRun=10
- [Reject] score=0.508 id=c5hole_lowchoice_v1_h05r_08_nutation_strict_mixed_peak_v1_rect_s20_o196_v01_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=patch_chain flow=local_collapse risk=high_risk tier=Drop cov=0.991 max=15 local=5 nearOuter=4 dirRisk=0.157 stripe=0.075 sts=0.837/collapse=0.351 axisRun=11 dirRun=11
- [Reject] score=0.473 id=c5hole_lowchoice_v1_h05r_02_nutation_strict_mixed_choiceclamp_v1_rect_s20_o196_v01_strictmixedchoiceclamp_choice_clamp_chain_hf1 style=mixed_unknown chain=curve_chain flow=single_axis_sweep risk=high_risk tier=Drop cov=0.982 max=14 local=6 nearOuter=6 dirRisk=0.314 stripe=0.041 sts=0.8/collapse=0.304 axisRun=8 dirRun=8
- [Reject] score=0.466 id=c5hole_lowchoice_v1_h05r_04_nutation_strict_mixed_choiceclamp_v1_rect_s20_o196_v03_strictmixedchoiceclamp_choice_clamp_chain_hf1 style=mixed_unknown chain=curve_chain flow=single_axis_sweep risk=high_risk tier=Drop cov=0.984 max=13 local=6 nearOuter=6 dirRisk=0.266 stripe=0.071 sts=0.818/collapse=0.326 axisRun=11 dirRun=11
- [Reject] score=0.405 id=c5hole_lowchoice_v1_h05r_12_nutation_strict_mixed_peak_v1_rect_s20_o196_v05_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=patch_chain flow=local_collapse risk=high_risk tier=Drop cov=0.992 max=12 local=12 nearOuter=6 dirRisk=0.483 stripe=0.098 sts=0.8/collapse=0.375 axisRun=12 dirRun=9
