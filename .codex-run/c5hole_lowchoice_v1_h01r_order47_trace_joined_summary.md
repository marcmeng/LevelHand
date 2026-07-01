# PSG Pressure Trace Join Summary

Generated: 2026-07-01 07:47:32

- Source CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h01r_order47_source_subset.csv
- Trace metrics CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab\Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\c5hole_lowchoice_v1_h01r_order47_metrics.csv
- Output CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h01r_order47_trace_joined.csv
- Best per slot CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h01r_order47_trace_best_per_slot.csv
- Production keep CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h01r_order47_production_keep.csv
- Joined rows: 6
- Output rows: 6
- processKeep rows: 3
- visualPass rows: 3
- STS metric present rows: 6
- stsPass rows: 2
- stsKeepCandidate rows: 1
- productionKeep mode: TraceOrderPreferred
- productionKeep source: TraceOrderKeep
- productionKeep eligible rows: 1
- productionKeep rows: 1
- productionDiversity enabled: False

## Rank Class
- Reject: 3
- TraceOrderKeep: 1
- VisualKeep: 2

## Style Family
- mixed_unknown: 6

## Chain Language
- curve_chain: 3
- patch_chain: 3

## Flow Language
- flow_spread: 1
- single_axis_sweep: 5

## Risk Band
- high_risk: 4
- watch: 2

## Production Keep Tag Mix
- styleFamily: mixed_unknown=1
- chainLanguage: curve_chain=1
- flowLanguage: single_axis_sweep=1
- riskBand: watch=1

## Top Rows
- [TraceOrderKeep] score=2.606 id=c5hole_lowchoice_v1_h01r_36_nutation_strict_mixed_peak_v1_rect_s05_o047_v05_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=curve_chain flow=single_axis_sweep risk=watch tier=B cov=0.983 max=11 local=6 nearOuter=3 dirRisk=0.176 stripe=0.061 sts=0.846/collapse=0.222 axisRun=8 dirRun=8
- [VisualKeep] score=2.026 id=c5hole_lowchoice_v1_h01r_33_nutation_strict_mixed_peak_v1_rect_s05_o047_v02_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=patch_chain flow=single_axis_sweep risk=watch tier=B cov=0.982 max=11 local=4 nearOuter=4 dirRisk=0.109 stripe=0.059 sts=0.812/collapse=0.263 axisRun=8 dirRun=8
- [VisualKeep] score=1.987 id=c5hole_lowchoice_v1_h01r_31_nutation_strict_mixed_peak_v1_rect_s05_o047_v00_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=patch_chain flow=single_axis_sweep risk=high_risk tier=B cov=0.983 max=9 local=7 nearOuter=4 dirRisk=0.279 stripe=0.073 sts=0.809/collapse=0.328 axisRun=9 dirRun=9
- [Reject] score=0.602 id=c5hole_lowchoice_v1_h01r_35_nutation_strict_mixed_peak_v1_rect_s05_o047_v04_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=curve_chain flow=flow_spread risk=high_risk tier=Drop cov=0.986 max=13 local=5 nearOuter=5 dirRisk=0.187 stripe=0.02 sts=0.874/collapse=0.162 axisRun=7 dirRun=5
- [Reject] score=0.5 id=c5hole_lowchoice_v1_h01r_32_nutation_strict_mixed_peak_v1_rect_s05_o047_v01_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=patch_chain flow=single_axis_sweep risk=high_risk tier=Drop cov=0.987 max=17 local=5 nearOuter=5 dirRisk=0.21 stripe=0.064 sts=0.758/collapse=0.317 axisRun=8 dirRun=8
- [Reject] score=0.491 id=c5hole_lowchoice_v1_h01r_34_nutation_strict_mixed_peak_v1_rect_s05_o047_v03_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=curve_chain flow=single_axis_sweep risk=high_risk tier=Drop cov=0.984 max=14 local=6 nearOuter=4 dirRisk=0.206 stripe=0.109 sts=0.817/collapse=0.346 axisRun=10 dirRun=10
