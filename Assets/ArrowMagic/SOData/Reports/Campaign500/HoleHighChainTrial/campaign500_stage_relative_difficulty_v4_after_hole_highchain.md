# Campaign500 Stage-Relative Difficulty V4 After Hole High-Chain

Uses the latest official Campaign500 validation summary after applying 10 high-chain hole replacements.

## Overall Counts

| version | 1 | 2 | 3 | 4 |
|---|---:|---:|---:|---:|
| current manifest/excel | 338 | 103 | 35 | 24 |
| proposed v4 after high-chain | 302 | 102 | 58 | 38 |

- Validation: Green=458, Yellow=42, Red=0
- Top flags: PeakTooFlat=50, BottleneckHeavy=37, GrindyLowClear=30, StartFakeWide=10, ShapeLowFill=2, HardBottleneckHeavy=1

## Per 50 Levels

| range | current 1/2/3/4 | proposed 1/2/3/4 | validationScore min-med-max | chain min-med-max | Yellow | notes |
|---|---:|---:|---:|---:|---:|---|
| 1-50 | 30/12/5/3 | 30/12/5/3 | 0-91-305 | 6-62-118 | 5 |  |
| 51-100 | 38/8/4/0 | 34/10/4/2 | 21-121-205 | 43-89-110 | 1 | adds stage peaks, shape/novelty carries difficulty |
| 101-150 | 36/10/4/0 | 33/10/5/2 | 41-138-237 | 47-95-114 | 3 | adds stage peaks, hole now carries pressure, shape/novelty carries difficulty |
| 151-200 | 35/12/3/0 | 32/10/5/3 | 51-139-235 | 52-104-135 | 4 | adds stage peaks, hole now carries pressure, shape/novelty carries difficulty |
| 201-250 | 32/14/3/1 | 30/10/6/4 | 53-145-254 | 54-107-171 | 3 | adds stage peaks, hole now carries pressure, shape/novelty carries difficulty |
| 251-300 | 34/11/1/4 | 30/10/6/4 | 79-180-324 | 59-111-318 | 4 | hole now carries pressure, shape/novelty carries difficulty |
| 301-350 | 33/8/5/4 | 29/10/6/5 | 91-202-409 | 65-120-347 | 5 | adds stage peaks, hole now carries pressure, shape/novelty carries difficulty |
| 351-400 | 33/8/5/4 | 28/10/7/5 | 116-201-368 | 70-144-306 | 9 | adds stage peaks, hole now carries pressure, shape/novelty carries difficulty |
| 401-450 | 32/9/3/6 | 28/10/7/5 | 77-203-334 | 73-158-213 | 3 | shape/novelty carries difficulty |
| 451-500 | 35/11/2/2 | 28/10/7/5 | 109-194-417 | 60-149-366 | 5 | adds stage peaks, hole now carries pressure |

## Hole Proposed Distribution

- hole: 1=41, 2=7, 3=2, 4=0

| level | prop | chains | score | status | flags | id |
|---:|---:|---:|---:|---|---|---|
| 135 | 2 | 52 | 183 | Green | None | hole_topup_012_topup_tall_20x30_shell |
| 151 | 3 | 80 | 201 | Yellow | StartFakeWide|BottleneckHeavy | hole_highchain_016_hc_30x28_dual_a |
| 192 | 3 | 82 | 235 | Yellow | StartFakeWide | hole_highchain_017_hc_28x30_dense_a |
| 235 | 2 | 58 | 199 | Green | None | hole_topup_045_topup_wide_28x22_dual |
| 292 | 2 | 62 | 230 | Yellow | StartFakeWide|BottleneckHeavy | hole_candidate_040_std_26x26_section |
| 301 | 2 | 87 | 250 | Yellow | BottleneckHeavy|GrindyLowClear | hole_highchain_014_hc_34x28_shell_a |
| 335 | 2 | 89 | 230 | Yellow | BottleneckHeavy|GrindyLowClear | hole_highchain_extra_003_hc2_32x28_lock_a |
| 392 | 2 | 87 | 229 | Green | None | hole_highchain_extra_019_hc2_34x28_dual_b |
| 492 | 2 | 90 | 230 | Yellow | StartFakeWide|BottleneckHeavy | hole_highchain_extra_018_hc2_32x30_lock_a |

## Proposed Difficulty 4

| level | type | role | rel | chains | score | warnings | id |
|---:|---|---|---|---:|---:|---|---|
| 19 | lock | FixedFront40 | VeryHard | 94 | 141 | PeakLabelButFlatFlag | direct_pure_topup_06_19_direct_normal_pure_rect_lock_buckle_g8_v09 |
| 23 | shape_magic | FixedFront40 | Normal | 80 | 257 | ValidationYellow:StartFakeWide|BottleneckHeavy | r2_outer_strong_005_r2_006_a401_procmask_79_long_maskproceduralvertical_30x44-mainmagicquilltall |
| 44 | section | Pressure | Hard | 86 | 238 | PeakLabelButFlatFlag | direct_pure_normal_extra_01_07_direct_normal_pure_extra_rect_quasi_symmetry_g15_v01 |
| 78 | shape_music | Shape | Novelty | 92 | 205 | ShapeOrNoveltyAsHard | r2_outer_strong_005_r2_006_a401_procmask_95_onion_maskproceduralvertical_28x42-maindistinctmusicnotetall |
| 87 | shape_object | Shape | Novelty | 95 | 192 | ShapeOrNoveltyAsHard | r2_outer_strong_005_r2_006_a401_procmask_46_long_maskproceduralvertical_30x40-mainpaintpalettetall |
| 113 | shape_magic | Shape | Novelty | 98 | 199 | ShapeOrNoveltyAsHard | r2_outer_strong_005_r2_006_a401_procmask_80_long_maskproceduralvertical_28x42-mainmagicrunestonetall |
| 148 | section | Pressure | Hard | 105 | 237 | ValidationYellow:StartFakeWide|PeakTooFlat | direct_normal_08_direct_normal_rect_stair_push_g0_v01 |
| 153 | shape_character | Shape | Novelty | 101 | 204 | ShapeOrNoveltyAsHard | r2_outer_strong_005_r2_006_a401_procmask_21_long_maskproceduralvertical_30x42-mainraisedhandmascottall |
| 169 | lock | Pressure | Hard | 115 | 208 | ValidationYellow:BottleneckHeavy|PeakTooFlat | direct_normal_31_direct_normal_rect_outer_shell_g3_v04 |
| 183 | shape_ocean | Shape | Novelty | 104 | 210 | ShapeOrNoveltyAsHard | r2_outer_strong_005_r2_006_a401_procmask_60_onion_maskproceduralvertical_28x42-mainoceanshelltall |
| 209 | lock | Peak | VeryHard | 157 | 250 | ValidationYellow:GrindyLowClear | direct_pure_topup_08_08_direct_polish_hard_pure_rect_lock_buckle_g12_v04 |
| 239 | section | Peak | RelativeExtreme | 171 | 190 |  | direct_polish_hard_19_direct_polish_hard_rect_section_unlock_g6_v03 |
| 241 | sweep | Recovery | Recovery | 120 | 254 | RecoveryButHeavy|ValidationYellow:BottleneckHeavy|GrindyLowClear | direct_normal_115_direct_normal_rect_sweep_g5_v12 |
| 245 | lock | Pressure | Hard | 150 | 216 |  | direct_polish_clean_22_direct_polish_normal_rect_core_burst_g2_v03 |
| 269 | lock | Peak | RelativeExtreme | 203 | 324 | ValidationYellow:GrindyLowClear | direct_advanced_17_direct_advanced_veryhard_rect_lock_buckle_g2_v03 |
| 276 | section | Peak | RelativeExtreme | 201 | 319 |  | direct_advanced_20_direct_advanced_veryhard_rect_dual_zone_g2_v03 |
| 287 | lock | Peak | RelativeExtreme | 280 | 301 |  | direct_advanced_57_direct_advanced_extreme_rect_lock_buckle_g5_v02 |
| 299 | lock | Peak | RelativeExtreme | 318 | 321 | ValidationYellow:GrindyLowClear | direct_advanced_51_direct_advanced_extreme_rect_core_burst_g4_v01 |
| 316 | section | Peak | RelativeExtreme | 318 | 303 |  | direct_advanced_60_direct_advanced_extreme_rect_dual_zone_g5_v02 |
| 324 | section | Pressure | Hard | 186 | 329 | ValidationYellow:BottleneckHeavy|GrindyLowClear | direct_advanced_32_direct_advanced_veryhard_rect_section_unlock_g3_v04 |
| 329 | lock | Peak | RelativeExtreme | 263 | 308 |  | direct_advanced_49_direct_advanced_extreme_rect_lock_buckle_g4_v01 |
| 336 | section | Peak | RelativeExtreme | 216 | 336 |  | direct_advanced_28_direct_advanced_veryhard_rect_dual_zone_g3_v04 |
| 349 | maze | Pressure | Hard | 347 | 409 |  | direct_polish_extreme_long_02_direct_polish_extreme_rect_long_corridor_g9_v01 |
| 359 | section | Peak | RelativeExtreme | 270 | 321 | ValidationYellow:GrindyLowClear | direct_advanced_61_direct_advanced_extreme_rect_stair_push_g5_v02 |
| 369 | lock | Pressure | Hard | 198 | 309 |  | direct_advanced_09_direct_advanced_veryhard_rect_lock_buckle_g1_v02 |
| 376 | lock | Peak | RelativeExtreme | 306 | 331 |  | direct_advanced_67_direct_advanced_extreme_rect_core_burst_g6_v03 |
| 388 | section | Pressure | Hard | 305 | 368 | ValidationYellow:BottleneckHeavy|GrindyLowClear | direct_advanced_70_direct_advanced_extreme_rect_quasi_symmetry_g6_v03 |
| 396 | section | Peak | RelativeExtreme | 181 | 314 | ValidationYellow:GrindyLowClear | direct_advanced_16_direct_advanced_veryhard_rect_section_unlock_g1_v02 |
| 403 | shape_vehicle | Shape | Novelty | 170 | 332 | ShapeOrNoveltyAsHard | r2_outer_strong_005_r2_006_a401_procmask_137_long_maskproceduralvertical_40x50-transitioncarrier03tall |
| 419 | section | Peak | RelativeExtreme | 203 | 285 |  | direct_advanced_40_direct_advanced_veryhard_rect_section_unlock_g0_v05 |
| 427 | shape_nature | Shape | Novelty | 191 | 307 | ShapeOrNoveltyAsHard | r2_outer_strong_005_r2_006_a401_procmask_147_onion_maskproceduralvertical_42x60-biggrove02tall |
| 429 | dense | Pressure | Hard | 205 | 334 |  | direct_advanced_10_direct_advanced_veryhard_rect_dense_weave_g1_v02 |
| 436 | lock | Peak | RelativeExtreme | 194 | 273 |  | direct_advanced_25_direct_advanced_veryhard_rect_lock_buckle_g3_v04 |
| 469 | maze | Pressure | Hard | 366 | 417 | ValidationYellow:BottleneckHeavy|GrindyLowClear | direct_polish_extreme_long_03_direct_polish_extreme_rect_hui_spiral_g9_v01 |
| 472 | section | Pressure | Hard | 231 | 302 |  | direct_advanced_13_direct_advanced_veryhard_rect_stair_push_g1_v02 |
| 490 | section | Normal | Normal | 199 | 291 |  | direct_advanced_24_direct_advanced_veryhard_rect_section_unlock_g2_v03 |
| 491 | sweep | Normal | Normal | 337 | 365 |  | direct_polish_extreme_long_04_direct_polish_extreme_rect_snake_spine_g9_v01 |
| 494 | dense | Normal | Normal | 192 | 288 |  | direct_advanced_02_direct_advanced_veryhard_rect_dense_weave_g0_v01 |