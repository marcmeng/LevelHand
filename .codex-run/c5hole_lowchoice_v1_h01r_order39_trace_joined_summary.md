# PSG Pressure Trace Join Summary

Generated: 2026-07-01 07:33:03

- Source CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h01r_order39_source_subset.csv
- Trace metrics CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab\Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\c5hole_lowchoice_v1_h01r_order39_metrics.csv
- Output CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h01r_order39_trace_joined.csv
- Best per slot CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h01r_order39_trace_best_per_slot.csv
- Production keep CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h01r_order39_production_keep.csv
- Joined rows: 6
- Output rows: 6
- processKeep rows: 5
- visualPass rows: 4
- STS metric present rows: 6
- stsPass rows: 3
- stsKeepCandidate rows: 1
- productionKeep mode: TraceOrderPreferred
- productionKeep source: TraceOrderKeep
- productionKeep eligible rows: 1
- productionKeep rows: 1
- productionDiversity enabled: False

## Rank Class
- ProcessKeep: 1
- Reject: 1
- TraceOrderKeep: 1
- VisualKeep: 3

## Style Family
- mixed_unknown: 6

## Chain Language
- curve_chain: 4
- patch_chain: 2

## Flow Language
- flow_spread: 1
- local_collapse: 2
- single_axis_sweep: 3

## Risk Band
- high_risk: 5
- watch: 1

## Production Keep Tag Mix
- styleFamily: mixed_unknown=1
- chainLanguage: patch_chain=1
- flowLanguage: single_axis_sweep=1
- riskBand: watch=1

## Top Rows
- [TraceOrderKeep] score=2.61 id=c5hole_lowchoice_v1_h01r_19_nutation_strict_mixed_peak_v1_rect_s04_o039_v00_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=patch_chain flow=single_axis_sweep risk=watch tier=B cov=0.98 max=10 local=7 nearOuter=3 dirRisk=0.249 stripe=0.027 sts=0.868/collapse=0.214 axisRun=9 dirRun=7
- [VisualKeep] score=2.137 id=c5hole_lowchoice_v1_h01r_21_nutation_strict_mixed_peak_v1_rect_s04_o039_v02_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=curve_chain flow=local_collapse risk=high_risk tier=B cov=0.98 max=9 local=4 nearOuter=4 dirRisk=0.155 stripe=0.076 sts=0.814/collapse=0.207 axisRun=5 dirRun=5
- [VisualKeep] score=2.1 id=c5hole_lowchoice_v1_h01r_22_nutation_strict_mixed_peak_v1_rect_s04_o039_v03_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=curve_chain flow=single_axis_sweep risk=high_risk tier=B cov=0.986 max=8 local=5 nearOuter=4 dirRisk=0.157 stripe=0.138 sts=0.81/collapse=0.289 axisRun=8 dirRun=8
- [VisualKeep] score=1.995 id=c5hole_lowchoice_v1_h01r_24_nutation_strict_mixed_peak_v1_rect_s04_o039_v05_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=patch_chain flow=single_axis_sweep risk=high_risk tier=B cov=0.992 max=9 local=6 nearOuter=6 dirRisk=0.289 stripe=0.137 sts=0.789/collapse=0.346 axisRun=8 dirRun=8
- [ProcessKeep] score=1.295 id=c5hole_lowchoice_v1_h01r_23_nutation_strict_mixed_peak_v1_rect_s04_o039_v04_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=curve_chain flow=local_collapse risk=high_risk tier=B cov=0.986 max=8 local=10 nearOuter=5 dirRisk=0.43 stripe=0.141 sts=0.844/collapse=0.278 axisRun=8 dirRun=8
- [Reject] score=0.692 id=c5hole_lowchoice_v1_h01r_20_nutation_strict_mixed_peak_v1_rect_s04_o039_v01_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=curve_chain flow=flow_spread risk=high_risk tier=B cov=0.989 max=12 local=3 nearOuter=3 dirRisk=0.03 stripe=0 sts=0.9/collapse=0.199 axisRun=8 dirRun=7
