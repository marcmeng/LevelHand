# PSG Pressure Trace Join Summary

Generated: 2026-07-01 07:42:27

- Source CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h03r_order112_source_subset.csv
- Trace metrics CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab\Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\c5hole_lowchoice_v1_h03r_order112_metrics.csv
- Output CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h03r_order112_trace_joined.csv
- Best per slot CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h03r_order112_trace_best_per_slot.csv
- Production keep CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h03r_order112_production_keep.csv
- Joined rows: 6
- Output rows: 6
- processKeep rows: 5
- visualPass rows: 2
- STS metric present rows: 6
- stsPass rows: 3
- stsKeepCandidate rows: 2
- productionKeep mode: TraceOrderPreferred
- productionKeep source: TraceOrderKeep
- productionKeep eligible rows: 2
- productionKeep rows: 2
- productionDiversity enabled: False

## Rank Class
- ProcessKeep: 2
- Reject: 1
- TraceOrderKeep: 2
- VisualKeep: 1

## Style Family
- mixed_unknown: 6

## Chain Language
- curve_chain: 4
- patch_chain: 2

## Flow Language
- flow_spread: 1
- local_collapse: 1
- single_axis_sweep: 1
- staged_unlock: 3

## Risk Band
- clean: 1
- high_risk: 4
- watch: 1

## Production Keep Tag Mix
- styleFamily: mixed_unknown=2
- chainLanguage: curve_chain=1, patch_chain=1
- flowLanguage: staged_unlock=2
- riskBand: clean=1, high_risk=1

## Top Rows
- [TraceOrderKeep] score=2.754 id=c5hole_lowchoice_v1_h03r_11_nutation_strict_mixed_peak_v1_rect_s12_o112_v04_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=curve_chain flow=staged_unlock risk=clean tier=B cov=0.982 max=8 local=5 nearOuter=4 dirRisk=0.157 stripe=0.108 sts=0.919/collapse=0.117 axisRun=5 dirRun=5
- [TraceOrderKeep] score=2.654 id=c5hole_lowchoice_v1_h03r_08_nutation_strict_mixed_peak_v1_rect_s12_o112_v01_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=patch_chain flow=staged_unlock risk=high_risk tier=B cov=0.989 max=8 local=8 nearOuter=4 dirRisk=0.303 stripe=0.129 sts=0.898/collapse=0.192 axisRun=7 dirRun=7
- [VisualKeep] score=2.096 id=c5hole_lowchoice_v1_h03r_10_nutation_strict_mixed_peak_v1_rect_s12_o112_v03_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=curve_chain flow=flow_spread risk=watch tier=B cov=0.989 max=9 local=5 nearOuter=4 dirRisk=0.204 stripe=0.056 sts=0.798/collapse=0.213 axisRun=6 dirRun=6
- [ProcessKeep] score=1.407 id=c5hole_lowchoice_v1_h03r_09_nutation_strict_mixed_peak_v1_rect_s12_o112_v02_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=patch_chain flow=single_axis_sweep risk=high_risk tier=B cov=0.986 max=7 local=8 nearOuter=6 dirRisk=0.386 stripe=0.01 sts=0.819/collapse=0.177 axisRun=5 dirRun=5
- [ProcessKeep] score=1.265 id=c5hole_lowchoice_v1_h03r_12_nutation_strict_mixed_peak_v1_rect_s12_o112_v05_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=curve_chain flow=local_collapse risk=high_risk tier=B cov=0.976 max=9 local=11 nearOuter=4 dirRisk=0.447 stripe=0.097 sts=0.753/collapse=0.432 axisRun=17 dirRun=9
- [Reject] score=0.557 id=c5hole_lowchoice_v1_h03r_07_nutation_strict_mixed_peak_v1_rect_s12_o112_v00_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=curve_chain flow=staged_unlock risk=high_risk tier=Drop cov=0.974 max=13 local=6 nearOuter=6 dirRisk=0.289 stripe=0.049 sts=0.831/collapse=0.205 axisRun=6 dirRun=5
