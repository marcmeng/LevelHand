# Campaign500 Stage-Relative Difficulty Review V3

Purpose: review whether labels 1/2/3/4 match current-stage player pressure, before writing back to Excel.

Label meaning: 1=current-stage normal/recovery, 2=pressure, 3=clearly hard, 4=stage peak/extreme.

Validation coverage by current levelId: 344/500. Missing rows are marked NeedRevalidate and should not be blindly trusted.

## Overall Counts

| version | 1 | 2 | 3 | 4 |
|---|---:|---:|---:|---:|
| current manifest | 338 | 103 | 35 | 24 |
| proposed review v3 | 302 | 102 | 58 | 38 |

## Per 50 Levels

| range | current 1/2/3/4 | proposed 1/2/3/4 | chains min-med-max | hole | shape | notes |
|---|---:|---:|---:|---:|---:|---|
| 1-50 | 30/12/5/3 | 30/12/5/3 | 6-62-118 | 5 | 6 |  |
| 51-100 | 38/8/4/0 | 34/10/4/2 | 43-89-110 | 5 | 10 | adds stage peaks |
| 101-150 | 36/10/4/0 | 33/10/5/2 | 47-95-114 | 5 | 10 | adds stage peaks |
| 151-200 | 35/12/3/0 | 32/10/5/3 | 51-104-135 | 5 | 10 | adds stage peaks |
| 201-250 | 32/14/3/1 | 30/10/6/4 | 54-107-171 | 5 | 10 | adds stage peaks |
| 251-300 | 34/11/1/4 | 30/10/6/4 | 59-111-318 | 5 | 10 |  |
| 301-350 | 33/8/5/4 | 29/10/6/5 | 65-120-347 | 5 | 10 | adds stage peaks |
| 351-400 | 33/8/5/4 | 28/10/7/5 | 70-144-306 | 5 | 10 | adds stage peaks |
| 401-450 | 32/9/3/6 | 28/10/7/5 | 73-158-213 | 5 | 10 | heavy recovery review |
| 451-500 | 35/11/2/2 | 28/10/7/5 | 60-149-366 | 5 | 10 | adds stage peaks, heavy recovery review |

## Proposed Difficulty 4

| level | type | role | rel | chains | evidence | warnings | id |
|---:|---|---|---|---:|---:|---|---|
| 19 | lock | FixedFront40 | VeryHard | 94 | 0.836 |  | direct_pure_topup_06_19_direct_normal_pure_rect_lock_buckle_g8_v09 |
| 30 | lock | FixedFront40 | Normal | 106 | 0.85 | NeedRevalidate | direct_normal_topup_68_direct_normal_topup_rect_core_burst_g8_v09 |
| 47 | lock | Peak | VeryHard | 118 | 1.004 | NeedRevalidate | direct_normal_73_direct_normal_rect_lock_buckle_g1_v08 |
| 89 | lock | Peak | VeryHard | 100 | 0.893 |  | direct_normal_topup_34_direct_normal_topup_rect_lock_buckle_g10_v05 |
| 96 | section | Peak | VeryHard | 110 | 1.02 |  | direct_normal_topup_13_direct_normal_topup_rect_dual_zone_g7_v02 |
| 136 | section | Peak | VeryHard | 113 | 0.954 | NeedRevalidate | direct_normal_39_direct_normal_rect_quasi_symmetry_g3_v04 |
| 149 | lock | Peak | VeryHard | 114 | 1.028 |  | direct_normal_126_direct_normal_rect_core_burst_g0_v13 |
| 156 | section | Peak | VeryHard | 108 | 0.925 | NeedRevalidate | direct_normal_29_direct_normal_rect_quasi_symmetry_g2_v03 |
| 179 | lock | Peak | VeryHard | 115 | 0.921 |  | direct_normal_topup_26_direct_normal_topup_rect_lock_buckle_g9_v04 |
| 196 | lock | Peak | VeryHard | 135 | 0.993 |  | direct_polish_clean_13_direct_polish_normal_rect_core_burst_g1_v02 |
| 209 | lock | Peak | VeryHard | 157 | 0.964 | NeedRevalidate | direct_pure_topup_08_08_direct_polish_hard_pure_rect_lock_buckle_g12_v04 |
| 216 | section | Peak | VeryHard | 144 | 0.982 |  | direct_pure_topup_09_08_direct_polish_hard_pure_rect_stair_push_g14_v06 |
| 227 | lock | Peak | VeryHard | 163 | 0.996 |  | direct_polish_hard_02_direct_polish_hard_rect_lock_buckle_g4_v01 |
| 239 | section | Peak | RelativeExtreme | 171 | 1.072 |  | direct_polish_hard_19_direct_polish_hard_rect_section_unlock_g6_v03 |
| 269 | lock | Peak | RelativeExtreme | 203 | 1.014 | NeedRevalidate | direct_advanced_17_direct_advanced_veryhard_rect_lock_buckle_g2_v03 |
| 276 | section | Peak | RelativeExtreme | 201 | 1.028 |  | direct_advanced_20_direct_advanced_veryhard_rect_dual_zone_g2_v03 |
| 287 | lock | Peak | RelativeExtreme | 280 | 1.041 | NeedRevalidate | direct_advanced_57_direct_advanced_extreme_rect_lock_buckle_g5_v02 |
| 299 | lock | Peak | RelativeExtreme | 318 | 1.056 | NeedRevalidate | direct_advanced_51_direct_advanced_extreme_rect_core_burst_g4_v01 |
| 316 | section | Peak | RelativeExtreme | 318 | 1.053 |  | direct_advanced_60_direct_advanced_extreme_rect_dual_zone_g5_v02 |
| 329 | lock | Peak | RelativeExtreme | 263 | 1.002 |  | direct_advanced_49_direct_advanced_extreme_rect_lock_buckle_g4_v01 |
| 336 | section | Peak | RelativeExtreme | 216 | 1.023 |  | direct_advanced_28_direct_advanced_veryhard_rect_dual_zone_g3_v04 |
| 347 | lock | Peak | RelativeExtreme | 189 | 0.951 |  | direct_polish_hard_22_direct_polish_hard_rect_core_burst_g6_v03 |
| 349 | maze | Pressure | Hard | 347 | 0.895 |  | direct_polish_extreme_long_02_direct_polish_extreme_rect_long_corridor_g9_v01 |
| 359 | section | Peak | RelativeExtreme | 270 | 1.035 | NeedRevalidate | direct_advanced_61_direct_advanced_extreme_rect_stair_push_g5_v02 |
| 376 | lock | Peak | RelativeExtreme | 306 | 1.043 | NeedRevalidate | direct_advanced_67_direct_advanced_extreme_rect_core_burst_g6_v03 |
| 388 | section | Pressure | Hard | 305 | 0.891 | NeedRevalidate | direct_advanced_70_direct_advanced_extreme_rect_quasi_symmetry_g6_v03 |
| 389 | lock | Peak | RelativeExtreme | 181 | 0.971 | NeedRevalidate | direct_pure_topup_07_12_direct_polish_hard_pure_rect_lock_buckle_g10_v02 |
| 396 | section | Peak | RelativeExtreme | 181 | 0.983 | NeedRevalidate | direct_advanced_16_direct_advanced_veryhard_rect_section_unlock_g1_v02 |
| 407 | lock | Peak | RelativeExtreme | 196 | 0.999 | NeedRevalidate | direct_advanced_11_direct_advanced_veryhard_rect_core_burst_g1_v02 |
| 419 | section | Peak | RelativeExtreme | 203 | 0.996 | NeedRevalidate | direct_advanced_40_direct_advanced_veryhard_rect_section_unlock_g0_v05 |
| 429 | dense | Pressure | Hard | 205 | 0.892 |  | direct_advanced_10_direct_advanced_veryhard_rect_dense_weave_g1_v02 |
| 436 | lock | Peak | RelativeExtreme | 194 | 0.892 |  | direct_advanced_25_direct_advanced_veryhard_rect_lock_buckle_g3_v04 |
| 449 | lock | Peak | RelativeExtreme | 181 | 0.87 | NeedRevalidate | direct_pure_topup_07_14_direct_polish_hard_pure_rect_core_burst_g10_v02 |
| 456 | section | Peak | RelativeExtreme | 224 | 1.026 |  | direct_advanced_48_direct_advanced_veryhard_rect_section_unlock_g1_v06 |
| 469 | maze | Pressure | Hard | 366 | 0.881 | NeedRevalidate | direct_polish_extreme_long_03_direct_polish_extreme_rect_hui_spiral_g9_v01 |
| 472 | section | Pressure | Hard | 231 | 0.86 |  | direct_advanced_13_direct_advanced_veryhard_rect_stair_push_g1_v02 |
| 479 | section | Peak | RelativeExtreme | 185 | 0.927 |  | direct_advanced_14_direct_advanced_veryhard_rect_quasi_symmetry_g1_v02 |
| 491 | sweep | Normal | Normal | 337 | 0.804 | NeedRevalidate | direct_polish_extreme_long_04_direct_polish_extreme_rect_snake_spine_g9_v01 |

## Main Review Warnings

### NeedRevalidate (156)

| level | cur->prop | type | role | rel | chains | evidence | flags/id |
|---:|---:|---|---|---|---:|---:|---|
| 2 | 1->1 | hole_rescue | FixedFront40 | Mechanic | 22 | 0.188 | missing validation; seed_Arrowz_level_035_holeblock_multiseed_final |
| 3 | 1->1 | normal | FixedFront40 | Recovery | 19 | 0.116 | missing validation; level3_tiny_03_level3_tiny_mini_maze_17 |
| 11 | 1->1 | normal | FixedFront40 | Recovery | 31 | 0.243 | missing validation; level3_thirty_03_level3_thirty_mini_maze_30 |
| 17 | 1->1 | hole_rescue | FixedFront40 | Mechanic | 37 | 0.247 | missing validation; r1_ab_091_above300_level_571_final_holeblock_multiseed_final |
| 21 | 1->1 | sweep | FixedFront40 | Normal | 44 | 0.31 | missing validation; nomask_style_02_snake_spine |
| 22 | 1->1 | section | FixedFront40 | Normal | 55 | 0.389 | missing validation; nomask_style_expansion_26_stair_ladder_a |
| 23 | 2->1 | shape_magic | FixedFront40 | Normal | 80 | 0.411 | missing validation; r2_outer_strong_005_r2_006_a401_procmask_79_long_maskproceduralvertical_30x44-mainmagicquilltall |
| 26 | 1->1 | hole_rescue | HoleRescue | Mechanic | 42 | 0.199 | missing validation; hole_candidate_058_tall_18x26_section |
| 27 | 2->1 | shape_object | Shape | Novelty | 80 | 0.547 | missing validation; r2_outer_strong_005_r2_006_a401_procmask_48_long_maskproceduralvertical_28x42-mainhourglasstall |
| 28 | 1->2 | maze | FixedFront40 | Normal | 63 | 0.562 | missing validation; direct_pure_topup_02_24_direct_refresh_topup_rect_maze_long_chain_g5_v06 |
| 29 | 3->2 | dense | FixedFront40 | Normal | 99 | 0.689 | missing validation; direct_pure_normal_extra_02_11_direct_normal_pure_extra_rect_dense_weave_g19_v05 |
| 30 | 4->4 | lock | FixedFront40 | Normal | 106 | 0.85 | missing validation; direct_normal_topup_68_direct_normal_topup_rect_core_burst_g8_v09 |
| 31 | 1->1 | sweep | FixedFront40 | Normal | 61 | 0.379 | missing validation; direct_pure_topup_01_05_direct_refresh_topup_rect_sweep_g0_v01 |
| 34 | 3->3 | lock | FixedFront40 | Normal | 93 | 0.717 | missing validation; direct_pure_topup_04_03_direct_normal_pure_rect_lock_buckle_g6_v01 |
| 36 | 2->2 | lock | FixedFront40 | Normal | 83 | 0.668 | missing validation; direct_pure_normal_extra_01_12_direct_normal_pure_extra_rect_core_burst_g16_v02 |
| 37 | 1->1 | hole_rescue | HoleRescue | Mechanic | 41 | 0.173 | missing validation; hole_candidate_009_std_20x22_lock |
| 38 | 2->1 | shape_magic | FixedFront40 | Normal | 88 | 0.476 | missing validation; r2_outer_strong_005_r2_006_a401_procmask_06_long_maskproceduralvertical_30x42-mainkeytall |
| 39 | 3->3 | lock | FixedFront40 | Normal | 101 | 0.771 | missing validation; direct_normal_06_direct_normal_rect_core_burst_g0_v01 |
| 41 | 1->1 | dense | Recovery | Recovery | 62 | 0.339 | missing validation; direct_pure_topup_03_17_direct_refresh_topup_rect_outer_shell_g2_v09 |
| 42 | 1->1 | dense | Normal | Normal | 80 | 0.517 | missing validation; direct_pure_topup_04_12_direct_normal_pure_rect_dense_weave_g7_v02 |
| 44 | 2->2 | section | Pressure | Hard | 86 | 0.717 | missing validation; direct_pure_normal_extra_01_07_direct_normal_pure_extra_rect_quasi_symmetry_g15_v01 |
| 45 | 1->1 | lock | Recovery | Recovery | 74 | 0.416 | missing validation; direct_pure_topup_05_03_direct_normal_pure_rect_lock_buckle_g9_v04 |
| 46 | 1->1 | hole_rescue | HoleRescue | Mechanic | 43 | 0.214 | missing validation; hole_candidate_017_std_20x26_maze |
| 47 | 3->4 | lock | Peak | VeryHard | 118 | 1.004 | missing validation; direct_normal_73_direct_normal_rect_lock_buckle_g1_v08 |
| 49 | 2->3 | section | Pressure | Hard | 89 | 0.764 | missing validation; direct_pure_normal_extra_01_23_direct_normal_pure_extra_rect_quasi_symmetry_g17_v03 |
| 54 | 1->1 | hole_rescue | HoleRescue | Mechanic | 43 | 0.12 | missing validation; hole_topup_001_topup_tall_18x26_dense |
| 57 | 1->1 | section | Normal | Normal | 84 | 0.409 | missing validation; direct_pure_topup_06_18_direct_normal_pure_rect_section_unlock_g8_v09 |
| 61 | 1->1 | hole_rescue | HoleRescue | Mechanic | 43 | 0.064 | missing validation; hole_candidate_057_tall_18x26_lock |
| 68 | 1->1 | lock | Normal | Normal | 59 | 0.325 | missing validation; direct_pure_topup_03_14_direct_refresh_topup_rect_core_burst_g1_v08 |
| 69 | 2->2 | section | Pressure | Hard | 100 | 0.793 | missing validation; direct_normal_48_direct_normal_rect_stair_push_g4_v05 |
| 70 | 1->2 | section | Normal | Normal | 103 | 0.655 | missing validation; direct_normal_139_direct_normal_rect_quasi_symmetry_g1_v14 |
| 71 | 1->1 | hole_rescue | HoleRescue | Mechanic | 45 | 0.127 | missing validation; hole_candidate_019_std_22x24_section |
| 77 | 1->1 | lock | Normal | Normal | 48 | 0.28 | missing validation; nomask_style_expansion_33_double_room_lock_tall |
| 81 | 1->1 | sweep | Recovery | Recovery | 46 | 0.112 | missing validation; nomask_style_expansion_04_snake_spine_a |
| 82 | 1->1 | hole_rescue | HoleRescue | Mechanic | 45 | 0.104 | missing validation; hole_candidate_018_std_22x24_lock |
| 86 | 1->1 | dense | Normal | Normal | 55 | 0.347 | missing validation; direct_pure_topup_01_17_direct_refresh_topup_rect_outer_shell_g2_v03 |
| 91 | 1->1 | hole_rescue | HoleRescue | Mechanic | 47 | 0.099 | missing validation; hole_candidate_071_wide_26x22_maze |
| 92 | 1->1 | lock | Normal | Normal | 48 | 0.327 | missing validation; nomask_style_expansion_30_vertical_gate_a |
| 94 | 2->3 | section | Pressure | Hard | 107 | 0.857 | missing validation; direct_normal_112_direct_normal_rect_section_unlock_g5_v12 |
| 95 | 1->1 | dense | Normal | Normal | 61 | 0.382 | missing validation; direct_pure_topup_02_04_direct_refresh_topup_rect_dense_weave_g3_v04 |
... 116 more in CSV

### MaybeTooLow (0)

None

### WeakPeakEvidence (0)

None

### RecoveryButHeavy (4)

| level | cur->prop | type | role | rel | chains | evidence | flags/id |
|---:|---:|---|---|---|---:|---:|---|
| 421 | 1->3 | sweep | Recovery | Recovery | 180 | 0.587 | missing validation; direct_polish_hard_08_direct_polish_hard_rect_sweep_g4_v01 |
| 445 | 1->3 | sweep | Recovery | Recovery | 197 | 0.723 | missing validation; direct_advanced_31_direct_advanced_veryhard_rect_sweep_g3_v04 |
| 465 | 1->3 | sweep | Recovery | Recovery | 213 | 0.7 | None; direct_advanced_23_direct_advanced_veryhard_rect_sweep_g2_v03 |
| 486 | 1->3 | sweep | Recovery | Recovery | 213 | 0.724 | None; direct_advanced_07_direct_advanced_veryhard_rect_sweep_g0_v01 |

### HardHoleCheck (0)

None

### ShapeOrNoveltyAsHard (0)

None
