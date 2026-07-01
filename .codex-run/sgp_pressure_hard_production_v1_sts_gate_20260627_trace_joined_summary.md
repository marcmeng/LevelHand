# PSG Pressure Trace Join Summary

Generated: 2026-06-27 23:04:08

- Source CSV: F:\Unityproject\ArrowLevel-Hand\Assets\ArrowMagic\SOData\Reports\DirectProcedural\sgp_pressure_hard_trial_report.csv
- Trace metrics CSV: F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab\Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\sgp_pressure_hard_production_v1_sts_gate_20260627_metrics.csv
- Output CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\sgp_pressure_hard_production_v1_sts_gate_20260627_trace_joined.csv
- Best per slot CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\sgp_pressure_hard_production_v1_sts_gate_20260627_trace_best_per_slot.csv
- Production keep CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\sgp_pressure_hard_production_v1_sts_gate_20260627_production_keep.csv
- Joined rows: 4
- processKeep rows: 3
- visualPass rows: 1
- STS metric present rows: 4
- stsPass rows: 2
- stsKeepCandidate rows: 2
- productionKeep mode: TraceOrderPreferred
- productionKeep source: TraceOrderKeep
- productionKeep rows: 2

## Rank Class
- ProcessKeep: 1
- Reject: 1
- TraceOrderKeep: 2

## Top Rows
- [TraceOrderKeep] score=2.745 id=sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle tier=A cov=0.991 max=8 local=3 nearOuter=3 dirRisk=0.03 stripe=0.233 sts=0.904/collapse=0.094 axisRun=4 dirRun=4
- [TraceOrderKeep] score=2.668 id=sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst tier=B cov=0.99 max=8 local=4 nearOuter=4 dirRisk=0.155 stripe=0.086 sts=0.805/collapse=0.253 axisRun=6 dirRun=6
- [ProcessKeep] score=1.223 id=sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave tier=B cov=0.978 max=6 local=13 nearOuter=9 dirRisk=0.52 stripe=0.131 sts=0.861/collapse=0.248 axisRun=8 dirRun=8
- [Reject] score=0.44 id=sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock tier=Drop cov=0.994 max=16 local=7 nearOuter=4 dirRisk=0.254 stripe=0.096 sts=0.786/collapse=0.326 axisRun=13 dirRun=12
