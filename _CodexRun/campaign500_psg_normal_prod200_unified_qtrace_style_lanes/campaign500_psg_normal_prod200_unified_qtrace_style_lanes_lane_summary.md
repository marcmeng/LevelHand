# PSG Style Lane Keeps V1

Generated: 2026-06-28 11:35:54

- Project root: F:\Unityproject\ArrowLevel-Hand
- Joined CSV: F:\Unityproject\ArrowLevel-Hand\_CodexRun\campaign500_psg_normal_prod200_unified_qtrace_trace_joined.csv
- Output dir: F:\Unityproject\ArrowLevel-Hand\_CodexRun\campaign500_psg_normal_prod200_unified_qtrace_style_lanes
- Eligible mode: TraceOrderKeep
- Unique across lanes: False
- Exclude high risk: True
- Joined rows: 200
- Eligible rows: 10

## Lane Outputs
- patchwork_lock: selected=0, matched=0, status=ok, csv=F:\Unityproject\ArrowLevel-Hand\_CodexRun\campaign500_psg_normal_prod200_unified_qtrace_style_lanes\campaign500_psg_normal_prod200_unified_qtrace_style_lanes_lane_patchwork_lock_keep.csv
- core_burst: selected=0, matched=0, status=ok, csv=F:\Unityproject\ArrowLevel-Hand\_CodexRun\campaign500_psg_normal_prod200_unified_qtrace_style_lanes\campaign500_psg_normal_prod200_unified_qtrace_style_lanes_lane_core_burst_keep.csv
- dense_weave: selected=2, matched=2, status=ok, csv=F:\Unityproject\ArrowLevel-Hand\_CodexRun\campaign500_psg_normal_prod200_unified_qtrace_style_lanes\campaign500_psg_normal_prod200_unified_qtrace_style_lanes_lane_dense_weave_keep.csv
  - style=dense_weave:2
  - chain=short_patchwork:2
  - flow=staged_unlock:1;region_alternating_flow:1
  - risk=watch:1;clean:1
- flow_spread: selected=3, matched=3, status=ok, csv=F:\Unityproject\ArrowLevel-Hand\_CodexRun\campaign500_psg_normal_prod200_unified_qtrace_style_lanes\campaign500_psg_normal_prod200_unified_qtrace_style_lanes_lane_flow_spread_keep.csv
  - style=section_unlock:2;dense_weave:1
  - chain=short_patchwork:3
  - flow=flow_spread:2;region_alternating_flow:1
  - risk=watch:2;clean:1
- staged_unlock: selected=6, matched=6, status=ok, csv=F:\Unityproject\ArrowLevel-Hand\_CodexRun\campaign500_psg_normal_prod200_unified_qtrace_style_lanes\campaign500_psg_normal_prod200_unified_qtrace_style_lanes_lane_staged_unlock_keep.csv
  - style=mixed_unknown:3;section_unlock:1;sweep:1;dense_weave:1
  - chain=short_patchwork:6
  - flow=staged_unlock:6
  - risk=watch:6

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
