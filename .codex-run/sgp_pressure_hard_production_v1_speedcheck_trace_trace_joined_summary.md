# PSG Pressure Trace Join Summary

Generated: 2026-06-27 22:59:01

- Source CSV: F:\Unityproject\ArrowLevel-Hand\Assets\ArrowMagic\SOData\Reports\DirectProcedural\sgp_pressure_hard_trial_report.csv
- Trace metrics CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab\Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\sgp_pressure_hard_production_v1_speedcheck_trace_metrics.csv
- Output CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\sgp_pressure_hard_production_v1_speedcheck_trace_trace_joined.csv
- Best per slot CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\sgp_pressure_hard_production_v1_speedcheck_trace_trace_best_per_slot.csv
- Production keep CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\sgp_pressure_hard_production_v1_speedcheck_trace_production_keep.csv
- Joined rows: 4
- processKeep rows: 3
- visualPass rows: 2
- STS metric present rows: 0
- stsPass rows: 0
- stsKeepCandidate rows: 0
- productionKeep mode: TraceOrderPreferred
- productionKeep source: VisualKeepFallbackNoSTS
- productionKeep rows: 2

## Rank Class
- ProcessKeep: 1
- Reject: 1
- VisualKeep: 2

## Top Rows
- [VisualKeep] score=2.015 id=sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle tier=A cov=0.991 max=8 local=3 nearOuter=3 dirRisk= stripe= sts=/collapse= axisRun= dirRun=
- [VisualKeep] score=1.99 id=sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst tier=B cov=0.99 max=8 local=4 nearOuter=4 dirRisk= stripe= sts=/collapse= axisRun= dirRun=
- [ProcessKeep] score=1.223 id=sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave tier=B cov=0.978 max=6 local=13 nearOuter=9 dirRisk= stripe= sts=/collapse= axisRun= dirRun=
- [Reject] score=0.441 id=sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock tier=Drop cov=0.994 max=16 local=7 nearOuter=4 dirRisk= stripe= sts=/collapse= axisRun= dirRun=
