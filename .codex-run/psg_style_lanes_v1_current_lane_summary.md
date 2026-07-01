# PSG Style Lane Keeps V1

Generated: 2026-06-28 01:55:44

- Project root: F:\Unityproject\ArrowLevel-Hand
- Joined CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\sgp_pressure_batch4_faststs_final2_pack_20260627_trace_joined.csv
- Output dir: F:\Unityproject\ArrowLevel-Hand\.codex-run
- Eligible mode: TraceOrderKeep
- Unique across lanes: False
- Exclude high risk: False
- Joined rows: 22
- Eligible rows: 15

## Lane Outputs
- patchwork_lock: selected=6, matched=6, status=ok, csv=F:\Unityproject\ArrowLevel-Hand\.codex-run\psg_style_lanes_v1_current_lane_patchwork_lock_keep.csv
  - style=lock_buckle:6
  - chain=lock_cluster:4;short_patchwork:2
  - flow=staged_unlock:3;region_alternating_flow:2;local_collapse:1
  - risk=high_risk:3;clean:2;watch:1
- core_burst: selected=6, matched=6, status=ok, csv=F:\Unityproject\ArrowLevel-Hand\.codex-run\psg_style_lanes_v1_current_lane_core_burst_keep.csv
  - style=core_burst:6
  - chain=short_patchwork:4;core_cluster:2
  - flow=flow_spread:3;staged_unlock:2;local_collapse:1
  - risk=watch:4;high_risk:1;clean:1
- dense_weave: selected=3, matched=3, status=ok, csv=F:\Unityproject\ArrowLevel-Hand\.codex-run\psg_style_lanes_v1_current_lane_dense_weave_keep.csv
  - style=dense_weave:3
  - chain=short_patchwork:2;woven_medium:1
  - flow=staged_unlock:2;region_alternating_flow:1
  - risk=watch:2;clean:1
- flow_spread: selected=6, matched=6, status=ok, csv=F:\Unityproject\ArrowLevel-Hand\.codex-run\psg_style_lanes_v1_current_lane_flow_spread_keep.csv
  - style=core_burst:3;lock_buckle:2;dense_weave:1
  - chain=short_patchwork:3;core_cluster:2;lock_cluster:1
  - flow=flow_spread:3;region_alternating_flow:3
  - risk=watch:4;clean:2
- staged_unlock: selected=7, matched=7, status=ok, csv=F:\Unityproject\ArrowLevel-Hand\.codex-run\psg_style_lanes_v1_current_lane_staged_unlock_keep.csv
  - style=lock_buckle:3;dense_weave:2;core_burst:2
  - chain=short_patchwork:4;lock_cluster:2;woven_medium:1
  - flow=staged_unlock:7
  - risk=watch:3;clean:2;high_risk:2

## Stable Lane Contract
- patchwork_lock: lock_buckle / lock_cluster candidates.
- core_burst: core_burst / core_cluster candidates.
- dense_weave: dense_weave family / dense_weave_chain candidates.
- flow_spread: flow_spread and region_alternating_flow candidates.
- staged_unlock: staged_unlock candidates.

## Notes
- Lane CSVs are independent by default; a level can appear in more than one lane when style and flow overlap.
- Use -UniqueAcrossLanes when building a mixed review pack that must avoid duplicates across lanes.
- This script does not modify canonical PSG keep CSV or Unity packs.
