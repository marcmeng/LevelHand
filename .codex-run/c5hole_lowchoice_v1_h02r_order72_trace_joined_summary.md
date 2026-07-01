# PSG Pressure Trace Join Summary

Generated: 2026-07-01 07:51:23

- Source CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h02r_order72_source_subset.csv
- Trace metrics CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab\Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\c5hole_lowchoice_v1_h02r_order72_metrics.csv
- Output CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h02r_order72_trace_joined.csv
- Best per slot CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h02r_order72_trace_best_per_slot.csv
- Production keep CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\c5hole_lowchoice_v1_h02r_order72_production_keep.csv
- Joined rows: 6
- Output rows: 6
- processKeep rows: 3
- visualPass rows: 2
- STS metric present rows: 6
- stsPass rows: 2
- stsKeepCandidate rows: 0
- productionKeep mode: TraceOrderPreferred
- productionKeep source: TraceOrderKeep
- productionKeep eligible rows: 0
- productionKeep rows: 0
- productionDiversity enabled: False

## Rank Class
- ProcessKeep: 1
- Reject: 3
- VisualKeep: 2

## Style Family
- mixed_unknown: 6

## Chain Language
- curve_chain: 5
- patch_chain: 1

## Flow Language
- flow_spread: 1
- local_collapse: 3
- single_axis_sweep: 1
- staged_unlock: 1

## Risk Band
- high_risk: 6

## Top Rows
- [VisualKeep] score=2.054 id=c5hole_lowchoice_v1_h02r_20_nutation_strict_mixed_peak_v1_rect_s08_o072_v01_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=curve_chain flow=single_axis_sweep risk=high_risk tier=B cov=0.985 max=10 local=5 nearOuter=3 dirRisk=0.174 stripe=0.11 sts=0.796/collapse=0.303 axisRun=9 dirRun=6
- [VisualKeep] score=1.919 id=c5hole_lowchoice_v1_h02r_23_nutation_strict_mixed_peak_v1_rect_s08_o072_v04_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=curve_chain flow=local_collapse risk=high_risk tier=B cov=0.986 max=10 local=6 nearOuter=6 dirRisk=0.289 stripe=0.129 sts=0.772/collapse=0.41 axisRun=11 dirRun=11
- [ProcessKeep] score=1.189 id=c5hole_lowchoice_v1_h02r_19_nutation_strict_mixed_peak_v1_rect_s08_o072_v00_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=curve_chain flow=local_collapse risk=high_risk tier=B cov=0.983 max=11 local=9 nearOuter=6 dirRisk=0.458 stripe=0.073 sts=0.755/collapse=0.319 axisRun=10 dirRun=6
- [Reject] score=0.626 id=c5hole_lowchoice_v1_h02r_24_nutation_strict_mixed_peak_v1_rect_s08_o072_v05_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=curve_chain flow=staged_unlock risk=high_risk tier=B cov=0.986 max=12 local=5 nearOuter=5 dirRisk=0.187 stripe=0.052 sts=0.888/collapse=0.115 axisRun=5 dirRun=5
- [Reject] score=0.575 id=c5hole_lowchoice_v1_h02r_21_nutation_strict_mixed_peak_v1_rect_s08_o072_v02_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=curve_chain flow=local_collapse risk=high_risk tier=B cov=0.977 max=12 local=5 nearOuter=4 dirRisk=0.157 stripe=0.017 sts=0.829/collapse=0.36 axisRun=10 dirRun=10
- [Reject] score=0.524 id=c5hole_lowchoice_v1_h02r_22_nutation_strict_mixed_peak_v1_rect_s08_o072_v03_strictmixedpeak_strict_peak_chain_hf1 style=mixed_unknown chain=patch_chain flow=flow_spread risk=high_risk tier=Drop cov=0.98 max=13 local=6 nearOuter=5 dirRisk=0.236 stripe=0.073 sts=0.87/collapse=0.173 axisRun=8 dirRun=5
