# Campaign500 Stage-Relative Difficulty Review V4 Validation

This version uses the fresh Unity single-level validation report as the main evidence.

Label meaning: 1=current-stage normal/recovery, 2=pressure, 3=clearly hard, 4=stage peak/extreme.

## Overall Counts

| version | 1 | 2 | 3 | 4 |
|---|---:|---:|---:|---:|
| current manifest/excel | 338 | 103 | 35 | 24 |
| proposed v4 validation | 302 | 102 | 58 | 38 |

## Validation Quality

- Green=461, Yellow=39, Red=0
- Top flags: PeakTooFlat=50, BottleneckHeavy=34, GrindyLowClear=30, StartFakeWide=7, ShapeLowFill=2, HardBottleneckHeavy=1

## Per 50 Levels

| range | current 1/2/3/4 | proposed 1/2/3/4 | validationScore min-med-max | chain min-med-max | Yellow | notes |
|---|---:|---:|---:|---:|---:|---|
| 1-50 | 30/12/5/3 | 30/12/5/3 | 0-91-305 | 6-62-118 | 5 |  |
| 51-100 | 38/8/4/0 | 34/10/4/2 | 21-121-205 | 43-89-110 | 1 | adds stage peaks, heavy recovery review, shape/novelty carries difficulty |
| 101-150 | 36/10/4/0 | 33/10/5/2 | 41-138-237 | 47-95-114 | 3 | adds stage peaks, shape/novelty carries difficulty |
| 151-200 | 35/12/3/0 | 32/10/5/3 | 51-138-210 | 51-104-135 | 4 | adds stage peaks, heavy recovery review, shape/novelty carries difficulty |
| 201-250 | 32/14/3/1 | 30/10/6/4 | 53-145-254 | 54-107-171 | 3 | adds stage peaks, heavy recovery review, shape/novelty carries difficulty |
| 251-300 | 34/11/1/4 | 30/10/6/4 | 79-180-324 | 59-111-318 | 4 | shape/novelty carries difficulty |
| 301-350 | 33/8/5/4 | 29/10/6/5 | 91-194-409 | 65-120-347 | 3 | adds stage peaks, heavy recovery review, shape/novelty carries difficulty |
| 351-400 | 33/8/5/4 | 28/10/7/5 | 106-199-368 | 70-144-306 | 9 | adds stage peaks, shape/novelty carries difficulty |
| 401-450 | 32/9/3/6 | 28/10/7/5 | 77-203-334 | 73-158-213 | 3 | heavy recovery review, shape/novelty carries difficulty |
| 451-500 | 35/11/2/2 | 28/10/7/5 | 109-184-417 | 60-149-366 | 4 | adds stage peaks, heavy recovery review |

## Proposed Difficulty 4

| level | type | role | rel | chains | validationScore | evidence | warnings | id |
|---:|---|---|---|---:|---:|---:|---|---|
| 19 | lock | FixedFront40 | VeryHard | 94 | 141 | 0.654 | PeakLabelButFlatFlag | direct_pure_topup_06_19_direct_normal_pure_rect_lock_buckle_g8_v09 |
| 23 | shape_magic | FixedFront40 | Normal | 80 | 257 | 0.821 | ValidationYellow:StartFakeWide|BottleneckHeavy | r2_outer_strong_005_r2_006_a401_procmask_79_long_maskproceduralvertical_30x44-mainmagicquilltall |
| 44 | section | Pressure | Hard | 86 | 238 | 0.88 | PeakLabelButFlatFlag | direct_pure_normal_extra_01_07_direct_normal_pure_extra_rect_quasi_symmetry_g15_v01 |
| 78 | shape_music | Shape | Novelty | 92 | 205 | 0.853 | ShapeOrNoveltyAsHard | r2_outer_strong_005_r2_006_a401_procmask_95_onion_maskproceduralvertical_28x42-maindistinctmusicnotetall |
| 87 | shape_object | Shape | Novelty | 95 | 192 | 0.86 | ShapeOrNoveltyAsHard | r2_outer_strong_005_r2_006_a401_procmask_46_long_maskproceduralvertical_30x40-mainpaintpalettetall |
| 113 | shape_magic | Shape | Novelty | 98 | 199 | 0.813 | ShapeOrNoveltyAsHard | r2_outer_strong_005_r2_006_a401_procmask_80_long_maskproceduralvertical_28x42-mainmagicrunestonetall |
| 148 | section | Pressure | Hard | 105 | 237 | 0.833 | ValidationYellow:StartFakeWide|PeakTooFlat | direct_normal_08_direct_normal_rect_stair_push_g0_v01 |
| 153 | shape_character | Shape | Novelty | 101 | 204 | 0.764 | ShapeOrNoveltyAsHard | r2_outer_strong_005_r2_006_a401_procmask_21_long_maskproceduralvertical_30x42-mainraisedhandmascottall |
| 169 | lock | Pressure | Hard | 115 | 208 | 0.921 | ValidationYellow:BottleneckHeavy|PeakTooFlat | direct_normal_31_direct_normal_rect_outer_shell_g3_v04 |
| 183 | shape_ocean | Shape | Novelty | 104 | 210 | 0.803 | ShapeOrNoveltyAsHard | r2_outer_strong_005_r2_006_a401_procmask_60_onion_maskproceduralvertical_28x42-mainoceanshelltall |
| 209 | lock | Peak | VeryHard | 157 | 250 | 0.832 | ValidationYellow:GrindyLowClear | direct_pure_topup_08_08_direct_polish_hard_pure_rect_lock_buckle_g12_v04 |
| 239 | section | Peak | RelativeExtreme | 171 | 190 | 0.777 |  | direct_polish_hard_19_direct_polish_hard_rect_section_unlock_g6_v03 |
| 241 | sweep | Recovery | Recovery | 120 | 254 | 0.852 | RecoveryButHeavy|ValidationYellow:BottleneckHeavy|GrindyLowClear | direct_normal_115_direct_normal_rect_sweep_g5_v12 |
| 245 | lock | Pressure | Hard | 150 | 216 | 0.806 |  | direct_polish_clean_22_direct_polish_normal_rect_core_burst_g2_v03 |
| 269 | lock | Peak | RelativeExtreme | 203 | 324 | 0.896 | ValidationYellow:GrindyLowClear | direct_advanced_17_direct_advanced_veryhard_rect_lock_buckle_g2_v03 |
| 276 | section | Peak | RelativeExtreme | 201 | 319 | 0.885 |  | direct_advanced_20_direct_advanced_veryhard_rect_dual_zone_g2_v03 |
| 287 | lock | Peak | RelativeExtreme | 280 | 301 | 0.863 |  | direct_advanced_57_direct_advanced_extreme_rect_lock_buckle_g5_v02 |
| 299 | lock | Peak | RelativeExtreme | 318 | 321 | 0.892 | ValidationYellow:GrindyLowClear | direct_advanced_51_direct_advanced_extreme_rect_core_burst_g4_v01 |
| 316 | section | Peak | RelativeExtreme | 318 | 303 | 0.88 |  | direct_advanced_60_direct_advanced_extreme_rect_dual_zone_g5_v02 |
| 324 | section | Pressure | Hard | 186 | 329 | 0.832 | ValidationYellow:BottleneckHeavy|GrindyLowClear | direct_advanced_32_direct_advanced_veryhard_rect_section_unlock_g3_v04 |
| 329 | lock | Peak | RelativeExtreme | 263 | 308 | 0.787 |  | direct_advanced_49_direct_advanced_extreme_rect_lock_buckle_g4_v01 |
| 336 | section | Peak | RelativeExtreme | 216 | 336 | 0.831 |  | direct_advanced_28_direct_advanced_veryhard_rect_dual_zone_g3_v04 |
| 349 | maze | Pressure | Hard | 347 | 409 | 0.903 |  | direct_polish_extreme_long_02_direct_polish_extreme_rect_long_corridor_g9_v01 |
| 359 | section | Peak | RelativeExtreme | 270 | 321 | 0.888 | ValidationYellow:GrindyLowClear | direct_advanced_61_direct_advanced_extreme_rect_stair_push_g5_v02 |
| 369 | lock | Pressure | Hard | 198 | 309 | 0.814 |  | direct_advanced_09_direct_advanced_veryhard_rect_lock_buckle_g1_v02 |
| 376 | lock | Peak | RelativeExtreme | 306 | 331 | 0.902 |  | direct_advanced_67_direct_advanced_extreme_rect_core_burst_g6_v03 |
| 388 | section | Pressure | Hard | 305 | 368 | 0.858 | ValidationYellow:BottleneckHeavy|GrindyLowClear | direct_advanced_70_direct_advanced_extreme_rect_quasi_symmetry_g6_v03 |
| 396 | section | Peak | RelativeExtreme | 181 | 314 | 0.86 | ValidationYellow:GrindyLowClear | direct_advanced_16_direct_advanced_veryhard_rect_section_unlock_g1_v02 |
| 403 | shape_vehicle | Shape | Novelty | 170 | 332 | 0.87 | ShapeOrNoveltyAsHard | r2_outer_strong_005_r2_006_a401_procmask_137_long_maskproceduralvertical_40x50-transitioncarrier03tall |
| 419 | section | Peak | RelativeExtreme | 203 | 285 | 0.878 |  | direct_advanced_40_direct_advanced_veryhard_rect_section_unlock_g0_v05 |
| 427 | shape_nature | Shape | Novelty | 191 | 307 | 0.789 | ShapeOrNoveltyAsHard | r2_outer_strong_005_r2_006_a401_procmask_147_onion_maskproceduralvertical_42x60-biggrove02tall |
| 429 | dense | Pressure | Hard | 205 | 334 | 0.923 |  | direct_advanced_10_direct_advanced_veryhard_rect_dense_weave_g1_v02 |
| 436 | lock | Peak | RelativeExtreme | 194 | 273 | 0.854 |  | direct_advanced_25_direct_advanced_veryhard_rect_lock_buckle_g3_v04 |
| 469 | maze | Pressure | Hard | 366 | 417 | 0.93 | ValidationYellow:BottleneckHeavy|GrindyLowClear | direct_polish_extreme_long_03_direct_polish_extreme_rect_hui_spiral_g9_v01 |
| 472 | section | Pressure | Hard | 231 | 302 | 0.86 |  | direct_advanced_13_direct_advanced_veryhard_rect_stair_push_g1_v02 |
| 490 | section | Normal | Normal | 199 | 291 | 0.783 |  | direct_advanced_24_direct_advanced_veryhard_rect_section_unlock_g2_v03 |
| 491 | sweep | Normal | Normal | 337 | 365 | 0.827 |  | direct_polish_extreme_long_04_direct_polish_extreme_rect_snake_spine_g9_v01 |
| 494 | dense | Normal | Normal | 192 | 288 | 0.785 |  | direct_advanced_02_direct_advanced_veryhard_rect_dense_weave_g0_v01 |

## Main Changes

### Promotions (128)

| level | cur->prop | type | role | rel | chains | valScore | flags | id |
|---:|---:|---|---|---|---:|---:|---|---|
| 23 | 2->4 | shape_magic | FixedFront40 | Normal | 80 | 257 | StartFakeWide|BottleneckHeavy | r2_outer_strong_005_r2_006_a401_procmask_79_long_maskproceduralvertical_30x44-mainmagicquilltall |
| 32 | 2->3 | shape_magic | FixedFront40 | Normal | 83 | 174 | BottleneckHeavy | r2_outer_strong_005_r2_006_a401_procmask_02_long_maskproceduralvertical_28x40-mainshieldtall |
| 38 | 2->3 | shape_magic | FixedFront40 | Normal | 88 | 209 | GrindyLowClear | r2_outer_strong_005_r2_006_a401_procmask_06_long_maskproceduralvertical_30x42-mainkeytall |
| 42 | 1->2 | dense | Normal | Normal | 80 | 117 | None | direct_pure_topup_04_12_direct_normal_pure_rect_dense_weave_g7_v02 |
| 43 | 1->2 | shape_magic | Shape | Novelty | 87 | 138 | None | r2_outer_strong_005_r2_006_a401_procmask_81_long_maskproceduralvertical_32x42-mainmagicbattall |
| 44 | 2->4 | section | Pressure | Hard | 86 | 238 | PeakTooFlat | direct_pure_normal_extra_01_07_direct_normal_pure_extra_rect_quasi_symmetry_g15_v01 |
| 48 | 1->2 | shape_magic | Shape | Novelty | 88 | 110 | None | r2_outer_strong_005_r2_006_a401_procmask_82_long_maskproceduralvertical_32x44-mainmagicblackcattall |
| 57 | 1->2 | section | Normal | Normal | 84 | 169 | None | direct_pure_topup_06_18_direct_normal_pure_rect_section_unlock_g8_v09 |
| 63 | 1->2 | shape_nature | Shape | Novelty | 90 | 168 | None | r2_outer_strong_005_r2_006_a401_procmask_123_long_maskproceduralvertical_32x42-mainleaf09tall |
| 67 | 1->2 | shape_symbol | Shape | Novelty | 86 | 158 | None | r2_outer_strong_005_r2_006_a401_procmask_19_onion_maskproceduralvertical_30x40-mainstartall |
| 73 | 1->2 | shape_symbol | Shape | Novelty | 90 | 173 | None | r2_outer_strong_005_r2_006_a401_procmask_92_onion_maskproceduralvertical_28x42-maindistinctcrescenttall |
| 74 | 1->2 | section | Normal | Normal | 87 | 181 | None | direct_pure_normal_extra_01_21_direct_normal_pure_extra_rect_dual_zone_g17_v03 |
| 75 | 1->3 | sweep | Recovery | Recovery | 103 | 187 | None | direct_normal_121_direct_normal_rect_outer_shell_g0_v13 |
| 78 | 1->4 | shape_music | Shape | Novelty | 92 | 205 | None | r2_outer_strong_005_r2_006_a401_procmask_95_onion_maskproceduralvertical_28x42-maindistinctmusicnotetall |
| 87 | 1->4 | shape_object | Shape | Novelty | 95 | 192 | None | r2_outer_strong_005_r2_006_a401_procmask_46_long_maskproceduralvertical_30x40-mainpaintpalettetall |
| 93 | 1->2 | shape_symbol | Shape | Novelty | 92 | 148 | None | r2_outer_strong_005_r2_006_a401_procmask_13_onion_maskproceduralvertical_28x42-mainlightningtall |
| 94 | 2->3 | section | Pressure | Hard | 107 | 174 | PeakTooFlat | direct_normal_112_direct_normal_rect_section_unlock_g5_v12 |
| 98 | 1->2 | shape_music | Shape | Novelty | 94 | 160 | None | r2_outer_strong_005_r2_006_a401_procmask_45_long_maskproceduralvertical_28x42-mainmicrophonetall |
| 99 | 1->3 | section | Normal | Normal | 101 | 180 | None | direct_normal_topup_07_direct_normal_topup_rect_quasi_symmetry_g6_v01 |
| 103 | 1->2 | shape_ocean | Shape | Novelty | 93 | 197 | None | r2_outer_strong_005_r2_006_a401_procmask_68_onion_maskproceduralvertical_30x42-mainoceandriftbottletall |
| 105 | 1->2 | maze | Recovery | Recovery | 108 | 173 | GrindyLowClear | direct_normal_60_direct_normal_rect_maze_long_chain_g5_v06 |
| 108 | 1->2 | shape_symbol | Shape | Novelty | 93 | 167 | None | r2_outer_strong_005_r2_006_a401_procmask_qd_108_onion_maskproceduralvertical_28x42-maindistinctdigit2tall |
| 113 | 1->4 | shape_magic | Shape | Novelty | 98 | 199 | None | r2_outer_strong_005_r2_006_a401_procmask_80_long_maskproceduralvertical_28x42-mainmagicrunestonetall |
| 114 | 1->2 | maze | Recovery | Recovery | 94 | 158 | None | direct_pure_topup_04_16_direct_normal_pure_rect_maze_long_chain_g7_v02 |
| 123 | 1->3 | shape_magic | Shape | Novelty | 92 | 182 | None | r2_outer_strong_005_r2_006_a401_procmask_83_long_maskproceduralvertical_30x42-mainmagicdragoneggtall |
| 124 | 1->3 | section | Normal | Normal | 105 | 234 | GrindyLowClear | direct_normal_topup_79_direct_normal_topup_rect_quasi_symmetry_g9_v10 |
| 127 | 1->2 | shape_character | Shape | Novelty | 99 | 151 | None | r2_outer_strong_005_r2_006_a401_procmask_36_long_maskproceduralvertical_30x42-mainowltall |
| 140 | 1->2 | section | Normal | Normal | 102 | 150 | None | direct_normal_92_direct_normal_rect_section_unlock_g3_v10 |
| 147 | 1->2 | shape_landmark | Shape | Novelty | 101 | 144 | None | r2_outer_strong_005_r2_006_a401_procmask_29_long_maskproceduralvertical_30x44-mainlibertystatuetall |
| 148 | 2->4 | section | Pressure | Hard | 105 | 237 | StartFakeWide|PeakTooFlat | direct_normal_08_direct_normal_rect_stair_push_g0_v01 |
| 150 | 1->3 | section | Normal | Normal | 104 | 185 | None | direct_normal_topup_65_direct_normal_topup_rect_section_unlock_g8_v09 |
| 153 | 1->4 | shape_character | Shape | Novelty | 101 | 204 | None | r2_outer_strong_005_r2_006_a401_procmask_21_long_maskproceduralvertical_30x42-mainraisedhandmascottall |
| 154 | 2->3 | section | Pressure | Hard | 113 | 172 | PeakTooFlat | direct_normal_98_direct_normal_rect_stair_push_g3_v10 |
| 166 | 1->2 | section | Normal | Normal | 119 | 150 | None | direct_polish_clean_19_direct_polish_normal_rect_section_unlock_g2_v03 |
| 168 | 1->2 | shape_object | Shape | Novelty | 100 | 162 | None | r2_outer_strong_005_r2_006_a401_procmask_102_onion_maskproceduralvertical_28x42-maindistinctboottall |
| 169 | 2->4 | lock | Pressure | Hard | 115 | 208 | BottleneckHeavy|PeakTooFlat | direct_normal_31_direct_normal_rect_outer_shell_g3_v04 |
| 173 | 1->2 | shape_magic | Shape | Novelty | 104 | 173 | None | r2_outer_strong_005_r2_006_a401_procmask_08_onion_maskproceduralvertical_28x40-maincandletall |
| 175 | 1->3 | section | Normal | Normal | 92 | 198 | BottleneckHeavy | direct_pure_normal_extra_01_09_direct_normal_pure_extra_rect_section_unlock_g16_v02 |
| 178 | 1->3 | shape_art | Shape | Novelty | 104 | 189 | BottleneckHeavy | r2_outer_strong_005_r2_006_a401_procmask_27_onion_maskproceduralvertical_30x42-mainpainterbusttall |
| 180 | 1->3 | sweep | Recovery | Recovery | 108 | 182 | None | direct_normal_topup_56_direct_normal_topup_rect_sweep_g6_v07 |
| 183 | 1->4 | shape_ocean | Shape | Novelty | 104 | 210 | None | r2_outer_strong_005_r2_006_a401_procmask_60_onion_maskproceduralvertical_28x42-mainoceanshelltall |
| 187 | 1->2 | shape_landmark | Shape | Novelty | 105 | 157 | None | r2_outer_strong_005_r2_006_a401_procmask_33_onion_maskproceduralvertical_30x44-mainclocktowertall |
| 191 | 1->2 | lock | Normal | Normal | 95 | 142 | None | direct_pure_normal_extra_02_18_direct_normal_pure_extra_rect_lock_buckle_g20_v06 |
| 193 | 1->2 | shape_ocean | Shape | Novelty | 102 | 175 | None | r2_outer_strong_005_r2_006_a401_procmask_61_onion_maskproceduralvertical_30x40-mainoceanstarfishtall |
| 209 | 3->4 | lock | Peak | VeryHard | 157 | 250 | GrindyLowClear | direct_pure_topup_08_08_direct_polish_hard_pure_rect_lock_buckle_g12_v04 |
| 210 | 1->3 | section | Normal | Normal | 113 | 185 | None | direct_normal_149_direct_normal_rect_quasi_symmetry_g2_v15 |
| 218 | 1->3 | shape_vehicle | Shape | Novelty | 106 | 206 | None | r2_outer_strong_005_r2_006_a401_procmask_16_onion_maskproceduralvertical_30x42-maintrainfronttall |
| 220 | 1->2 | lock | Normal | Normal | 80 | 200 | None | direct_pure_normal_extra_02_10_direct_normal_pure_extra_rect_lock_buckle_g19_v05 |
| 221 | 1->3 | dense | Recovery | Recovery | 109 | 200 | GrindyLowClear | direct_normal_topup_11_direct_normal_topup_rect_dense_weave_g7_v02 |
| 223 | 1->3 | shape_music | Shape | Novelty | 103 | 210 | None | r2_outer_strong_005_r2_006_a401_procmask_supp_08_onion_maskfinalsupplement_30x42-mainmusicdrumtall |
| 232 | 2->3 | lock | Pressure | Hard | 143 | 210 | None | direct_polish_clean_04_direct_polish_normal_rect_core_burst_g0_v01 |
| 238 | 1->2 | shape_character | Shape | Novelty | 108 | 145 | None | r2_outer_strong_005_r2_006_a401_procmask_15_onion_maskproceduralvertical_30x42-mainsnowmantall |
| 241 | 1->4 | sweep | Recovery | Recovery | 120 | 254 | BottleneckHeavy|GrindyLowClear | direct_normal_115_direct_normal_rect_sweep_g5_v12 |
| 243 | 1->2 | shape_space | Shape | Novelty | 111 | 194 | None | r2_outer_strong_005_r2_006_a401_procmask_23_onion_maskproceduralvertical_30x42-mainastronautmascottall |
| 244 | 1->3 | section | Normal | Normal | 115 | 188 | None | direct_normal_topup_62_direct_normal_topup_rect_stair_push_g7_v08 |
| 245 | 2->4 | lock | Pressure | Hard | 150 | 216 | None | direct_polish_clean_22_direct_polish_normal_rect_core_burst_g2_v03 |
| 247 | 1->2 | shape_object | Shape | Novelty | 109 | 168 | None | r2_outer_strong_005_r2_006_a401_procmask_103_onion_maskproceduralvertical_28x42-maindistinctmittentall |
| 248 | 1->2 | section | Normal | Normal | 102 | 179 | BottleneckHeavy | direct_normal_07_direct_normal_rect_dual_zone_g0_v01 |
| 250 | 1->2 | dense | Normal | Normal | 107 | 181 | None | direct_normal_134_direct_normal_rect_dense_weave_g1_v14 |
| 252 | 2->3 | lock | Pressure | Hard | 147 | 235 | None | direct_pure_topup_09_06_direct_polish_hard_pure_rect_core_burst_g14_v06 |
... 68 more in CSV

### Demotions (86)

| level | cur->prop | type | role | rel | chains | valScore | flags | id |
|---:|---:|---|---|---|---:|---:|---|---|
| 24 | 2->1 | section | FixedFront40 | Normal | 82 | 74 | None | direct_pure_topup_05_02_direct_normal_pure_rect_section_unlock_g9_v04 |
| 25 | 3->2 | lock | FixedFront40 | Normal | 98 | 159 | None | direct_pure_normal_extra_01_10_direct_normal_pure_extra_rect_lock_buckle_g16_v02 |
| 30 | 4->2 | lock | FixedFront40 | Normal | 106 | 86 | BottleneckHeavy | direct_normal_topup_68_direct_normal_topup_rect_core_burst_g8_v09 |
| 33 | 2->1 | dense | FixedFront40 | Normal | 78 | 40 | None | direct_pure_topup_04_17_direct_normal_pure_rect_outer_shell_g8_v03 |
| 34 | 3->2 | lock | FixedFront40 | Normal | 93 | 105 | None | direct_pure_topup_04_03_direct_normal_pure_rect_lock_buckle_g6_v01 |
| 39 | 3->1 | lock | FixedFront40 | Normal | 101 | 90 | None | direct_normal_06_direct_normal_rect_core_burst_g0_v01 |
| 40 | 4->3 | lock | FixedFront40 | Normal | 103 | 202 | None | direct_pure_topup_05_11_direct_normal_pure_rect_lock_buckle_g10_v05 |
| 52 | 2->1 | lock | Pressure | Hard | 87 | 88 | PeakTooFlat | direct_pure_topup_06_17_direct_normal_pure_rect_outer_shell_g8_v09 |
| 56 | 2->1 | dense | Pressure | Hard | 96 | 114 | PeakTooFlat | direct_pure_topup_05_01_direct_normal_pure_rect_outer_shell_g9_v04 |
| 59 | 3->2 | lock | Peak | VeryHard | 89 | 156 | PeakTooFlat | direct_pure_topup_04_11_direct_normal_pure_rect_lock_buckle_g7_v02 |
| 65 | 2->1 | lock | Pressure | Hard | 104 | 114 | PeakTooFlat | direct_normal_23_direct_normal_rect_lock_buckle_g2_v03 |
| 69 | 2->1 | section | Pressure | Hard | 100 | 88 | PeakTooFlat | direct_normal_48_direct_normal_rect_stair_push_g4_v05 |
| 72 | 2->1 | lock | Pressure | Hard | 104 | 104 | PeakTooFlat | direct_normal_topup_66_direct_normal_topup_rect_lock_buckle_g8_v09 |
| 76 | 3->2 | section | Peak | VeryHard | 94 | 124 | PeakTooFlat | direct_pure_normal_extra_02_06_direct_normal_pure_extra_rect_stair_push_g18_v04 |
| 84 | 2->1 | lock | Pressure | Hard | 88 | 85 | PeakTooFlat | direct_pure_normal_extra_01_04_direct_normal_pure_extra_rect_core_burst_g15_v01 |
| 88 | 2->1 | section | Pressure | Hard | 101 | 108 | PeakTooFlat | direct_normal_28_direct_normal_rect_stair_push_g2_v03 |
| 96 | 3->2 | section | Peak | VeryHard | 110 | 151 | PeakTooFlat | direct_normal_topup_13_direct_normal_topup_rect_dual_zone_g7_v02 |
| 104 | 2->1 | lock | Pressure | Hard | 102 | 104 | PeakTooFlat | direct_normal_topup_20_direct_normal_topup_rect_core_burst_g8_v03 |
| 109 | 2->1 | section | Pressure | Hard | 108 | 114 | PeakTooFlat | direct_normal_topup_31_direct_normal_topup_rect_quasi_symmetry_g9_v04 |
| 112 | 2->1 | lock | Pressure | Hard | 108 | 86 | PeakTooFlat | direct_pure_topup_06_22_direct_normal_pure_rect_core_burst_g8_v09 |
| 116 | 2->1 | lock | Pressure | Hard | 89 | 121 | PeakTooFlat | direct_pure_topup_05_19_direct_normal_pure_rect_lock_buckle_g11_v06 |
| 119 | 3->2 | section | Peak | VeryHard | 102 | 155 | PeakTooFlat | direct_normal_32_direct_normal_rect_section_unlock_g3_v04 |
| 125 | 2->1 | lock | Pressure | Hard | 93 | 93 | PeakTooFlat | direct_pure_normal_extra_01_20_direct_normal_pure_extra_rect_core_burst_g17_v03 |
| 129 | 2->1 | section | Pressure | Hard | 102 | 106 | PeakTooFlat | direct_normal_topup_30_direct_normal_topup_rect_stair_push_g9_v04 |
| 132 | 2->1 | lock | Pressure | Hard | 86 | 167 | PeakTooFlat | direct_pure_topup_04_22_direct_normal_pure_rect_core_burst_g8_v03 |
| 144 | 2->1 | lock | Pressure | Hard | 113 | 134 | PeakTooFlat | direct_normal_103_direct_normal_rect_lock_buckle_g4_v11 |
| 149 | 3->2 | lock | Peak | VeryHard | 114 | 146 | PeakTooFlat | direct_normal_126_direct_normal_rect_core_burst_g0_v13 |
| 151 | 2->1 | hole_rescue | HoleRescue | Mechanic | 51 | 153 | GrindyLowClear | hole_candidate_060_tall_20x28_lock |
| 164 | 2->1 | lock | Pressure | Hard | 114 | 134 | PeakTooFlat | direct_normal_topup_18_direct_normal_topup_rect_lock_buckle_g8_v03 |
| 167 | 2->1 | hole_rescue | HoleRescue | Mechanic | 53 | 79 | None | hole_topup_043_topup_wide_26x22_maze |
| 177 | 2->1 | hole_rescue | HoleRescue | Mechanic | 52 | 74 | None | hole_candidate_074_wide_28x22_dense |
| 179 | 3->1 | lock | Peak | VeryHard | 115 | 112 | PeakTooFlat | direct_normal_topup_26_direct_normal_topup_rect_lock_buckle_g9_v04 |
| 184 | 2->1 | hole_rescue | HoleRescue | Mechanic | 55 | 51 | None | hole_topup_024_topup_std_24x28_dual |
| 189 | 2->1 | lock | Pressure | Hard | 112 | 124 | PeakTooFlat | direct_normal_56_direct_normal_rect_core_burst_g5_v06 |
| 192 | 2->1 | hole_rescue | HoleRescue | Mechanic | 54 | 118 | GrindyLowClear | hole_candidate_063_tall_20x30_lock |
| 196 | 3->2 | lock | Peak | VeryHard | 135 | 148 | None | direct_polish_clean_13_direct_polish_normal_rect_core_burst_g1_v02 |
| 201 | 2->1 | hole_rescue | HoleRescue | Mechanic | 56 | 75 | None | hole_candidate_037_std_26x24_section |
| 204 | 2->1 | section | Pressure | Hard | 107 | 140 | PeakTooFlat | direct_normal_67_direct_normal_rect_dual_zone_g0_v07 |
| 208 | 2->1 | section | Pressure | Hard | 111 | 147 | PeakTooFlat | direct_normal_12_direct_normal_rect_section_unlock_g1_v02 |
| 214 | 2->1 | lock | Pressure | Hard | 105 | 145 | PeakTooFlat | direct_normal_66_direct_normal_rect_core_burst_g0_v07 |
| 216 | 3->2 | section | Peak | VeryHard | 144 | 148 | None | direct_pure_topup_09_08_direct_polish_hard_pure_rect_stair_push_g14_v06 |
| 217 | 2->1 | hole_rescue | HoleRescue | Mechanic | 56 | 99 | None | hole_candidate_072_wide_28x22_lock |
| 224 | 2->1 | section | Pressure | Hard | 108 | 111 | PeakTooFlat | direct_normal_27_direct_normal_rect_dual_zone_g2_v03 |
| 225 | 2->1 | hole_rescue | HoleRescue | Mechanic | 57 | 126 | None | hole_topup_022_topup_std_22x28_maze |
| 227 | 3->2 | lock | Peak | VeryHard | 163 | 170 | None | direct_polish_hard_02_direct_polish_hard_rect_lock_buckle_g4_v01 |
| 229 | 2->1 | section | Pressure | Hard | 107 | 112 | PeakTooFlat | direct_normal_152_direct_normal_rect_section_unlock_g3_v16 |
| 236 | 2->1 | section | Pressure | Hard | 107 | 130 | PeakTooFlat | direct_normal_topup_47_direct_normal_topup_rect_quasi_symmetry_g11_v06 |
| 242 | 2->1 | hole_rescue | HoleRescue | Mechanic | 58 | 74 | BottleneckHeavy | hole_candidate_073_wide_28x22_section |
| 251 | 2->1 | hole_rescue | HoleRescue | Mechanic | 59 | 134 | None | hole_candidate_047_std_28x24_shell |
| 267 | 2->1 | hole_rescue | HoleRescue | Mechanic | 61 | 169 | None | hole_candidate_025_std_22x28_section |
| 274 | 2->1 | section | Pressure | Hard | 110 | 102 | PeakTooFlat | direct_normal_19_direct_normal_rect_quasi_symmetry_g1_v02 |
| 277 | 2->1 | hole_rescue | HoleRescue | Mechanic | 61 | 186 | None | hole_topup_030_topup_std_26x26_dual |
| 284 | 2->1 | lock | Pressure | Hard | 167 | 149 | None | direct_pure_topup_07_24_direct_polish_hard_pure_rect_core_burst_g11_v03 |
| 285 | 2->1 | hole_rescue | HoleRescue | Mechanic | 62 | 101 | None | hole_topup_025_topup_std_24x28_maze |
| 289 | 2->1 | section | Pressure | Hard | 115 | 148 | PeakTooFlat | direct_normal_38_direct_normal_rect_stair_push_g3_v04 |
| 301 | 3->1 | hole_rescue | HoleRescue | Mechanic | 65 | 172 | BottleneckHeavy | hole_topup_033_topup_std_26x28_dual |
| 312 | 2->1 | lock | Pressure | Hard | 134 | 104 | None | direct_pure_topup_09_22_direct_polish_hard_pure_rect_outer_shell_g9_v07 |
| 317 | 3->1 | hole_rescue | HoleRescue | Mechanic | 65 | 119 | None | hole_candidate_053_std_28x28_maze |
| 325 | 3->1 | hole_rescue | HoleRescue | Mechanic | 66 | 91 | None | hole_candidate_044_std_26x28_dense |
| 335 | 3->1 | hole_rescue | HoleRescue | Mechanic | 67 | 115 | None | hole_topup_049_topup_wide_30x24_maze |
... 26 more in CSV

### RecoveryButHeavy (9)

| level | cur->prop | type | role | rel | chains | valScore | flags | id |
|---:|---:|---|---|---|---:|---:|---|---|
| 75 | 1->3 | sweep | Recovery | Recovery | 103 | 187 | None | direct_normal_121_direct_normal_rect_outer_shell_g0_v13 |
| 180 | 1->3 | sweep | Recovery | Recovery | 108 | 182 | None | direct_normal_topup_56_direct_normal_topup_rect_sweep_g6_v07 |
| 221 | 1->3 | dense | Recovery | Recovery | 109 | 200 | GrindyLowClear | direct_normal_topup_11_direct_normal_topup_rect_dense_weave_g7_v02 |
| 241 | 1->4 | sweep | Recovery | Recovery | 120 | 254 | BottleneckHeavy|GrindyLowClear | direct_normal_115_direct_normal_rect_sweep_g5_v12 |
| 341 | 1->3 | sweep | Recovery | Recovery | 154 | 263 | None | direct_pure_topup_08_14_direct_polish_hard_pure_rect_sweep_g12_v04 |
| 445 | 1->3 | sweep | Recovery | Recovery | 197 | 256 | None | direct_advanced_31_direct_advanced_veryhard_rect_sweep_g3_v04 |
| 465 | 1->3 | sweep | Recovery | Recovery | 213 | 279 | None | direct_advanced_23_direct_advanced_veryhard_rect_sweep_g2_v03 |
| 486 | 1->3 | sweep | Recovery | Recovery | 213 | 294 | None | direct_advanced_07_direct_advanced_veryhard_rect_sweep_g0_v01 |
| 499 | 1->3 | section | Recovery | Recovery | 158 | 286 | GrindyLowClear | direct_pure_topup_09_09_direct_polish_hard_pure_rect_quasi_symmetry_g14_v06 |

### ShapeOrNoveltyAsHard (24)

| level | cur->prop | type | role | rel | chains | valScore | flags | id |
|---:|---:|---|---|---|---:|---:|---|---|
| 78 | 1->4 | shape_music | Shape | Novelty | 92 | 205 | None | r2_outer_strong_005_r2_006_a401_procmask_95_onion_maskproceduralvertical_28x42-maindistinctmusicnotetall |
| 87 | 1->4 | shape_object | Shape | Novelty | 95 | 192 | None | r2_outer_strong_005_r2_006_a401_procmask_46_long_maskproceduralvertical_30x40-mainpaintpalettetall |
| 113 | 1->4 | shape_magic | Shape | Novelty | 98 | 199 | None | r2_outer_strong_005_r2_006_a401_procmask_80_long_maskproceduralvertical_28x42-mainmagicrunestonetall |
| 123 | 1->3 | shape_magic | Shape | Novelty | 92 | 182 | None | r2_outer_strong_005_r2_006_a401_procmask_83_long_maskproceduralvertical_30x42-mainmagicdragoneggtall |
| 153 | 1->4 | shape_character | Shape | Novelty | 101 | 204 | None | r2_outer_strong_005_r2_006_a401_procmask_21_long_maskproceduralvertical_30x42-mainraisedhandmascottall |
| 178 | 1->3 | shape_art | Shape | Novelty | 104 | 189 | BottleneckHeavy | r2_outer_strong_005_r2_006_a401_procmask_27_onion_maskproceduralvertical_30x42-mainpainterbusttall |
| 183 | 1->4 | shape_ocean | Shape | Novelty | 104 | 210 | None | r2_outer_strong_005_r2_006_a401_procmask_60_onion_maskproceduralvertical_28x42-mainoceanshelltall |
| 218 | 1->3 | shape_vehicle | Shape | Novelty | 106 | 206 | None | r2_outer_strong_005_r2_006_a401_procmask_16_onion_maskproceduralvertical_30x42-maintrainfronttall |
| 223 | 1->3 | shape_music | Shape | Novelty | 103 | 210 | None | r2_outer_strong_005_r2_006_a401_procmask_supp_08_onion_maskfinalsupplement_30x42-mainmusicdrumtall |
| 268 | 1->3 | shape_magic | Shape | Novelty | 118 | 238 | None | r2_outer_strong_005_r2_006_a401_procmask_87_onion_maskproceduralvertical_34x50-transitionmagicbroomtall |
| 293 | 1->3 | shape_landmark | Shape | Novelty | 119 | 255 | None | r2_outer_strong_005_r2_006_a401_procmask_125_onion_maskproceduralvertical_34x50-transitionwidelandmark01tall |
| 298 | 1->3 | shape_magic | Shape | Novelty | 122 | 279 | BottleneckHeavy | r2_outer_strong_005_r2_006_a401_procmask_43_long_maskproceduralvertical_30x42-maintreasurechesttall |
| 313 | 1->3 | shape_music | Shape | Novelty | 121 | 258 | BottleneckHeavy | r2_outer_strong_005_r2_006_a401_procmask_supp_06_onion_maskfinalsupplement_30x42-mainmusicvinylrecordtall |
| 343 | 1->3 | shape_art | Shape | Novelty | 135 | 240 | None | r2_outer_strong_005_r2_006_a401_procmask_26_long_maskproceduralvertical_30x42-mainclassicportraittall |
| 353 | 1->3 | shape_magic | Shape | Novelty | 141 | 255 | BottleneckHeavy | r2_outer_strong_005_r2_006_a401_procmask_89_long_maskproceduralvertical_36x50-transitionmagicknighthelmettall |
| 363 | 1->3 | shape_landmark | Shape | Novelty | 138 | 262 | None | r2_outer_strong_005_r2_006_a401_procmask_56_onion_maskproceduralvertical_34x50-transitionobelisktall |
| 378 | 1->3 | shape_landmark | Shape | Novelty | 144 | 260 | None | r2_outer_strong_005_r2_006_a401_procmask_55_onion_maskproceduralvertical_36x50-transitionbridgetowertall |
| 383 | 1->3 | shape_ocean | Shape | Novelty | 142 | 239 | BottleneckHeavy | r2_outer_strong_005_r2_006_a401_procmask_57_onion_maskproceduralvertical_36x50-transitionsailboattall |
| 398 | 1->3 | shape_landmark | Shape | Novelty | 163 | 288 | BottleneckHeavy | r2_outer_strong_005_r2_006_a401_procmask_53_long_maskproceduralvertical_36x50-transitiondomepalacetall |
| 403 | 1->4 | shape_vehicle | Shape | Novelty | 170 | 332 | BottleneckHeavy | r2_outer_strong_005_r2_006_a401_procmask_137_long_maskproceduralvertical_40x50-transitioncarrier03tall |
| 408 | 1->3 | shape_nature | Shape | Novelty | 167 | 264 | None | r2_outer_strong_005_r2_006_a401_procmask_114_onion_maskproceduralvertical_36x44-transitiondistinctbutterflytall |
| 418 | 1->3 | shape_space | Shape | Novelty | 186 | 265 | None | r2_outer_strong_005_r2_006_a401_procmask_58_onion_maskproceduralvertical_40x56-biggiantrobottall |
| 423 | 1->3 | shape_vehicle | Shape | Novelty | 186 | 288 | None | r2_outer_strong_005_r2_006_a401_procmask_144_onion_maskproceduralvertical_42x60-bigcarrier04tall |
| 427 | 1->4 | shape_nature | Shape | Novelty | 191 | 307 | None | r2_outer_strong_005_r2_006_a401_procmask_147_onion_maskproceduralvertical_42x60-biggrove02tall |

### ValidationYellow (39)

| level | cur->prop | type | role | rel | chains | valScore | flags | id |
|---:|---:|---|---|---|---:|---:|---|---|
| 1 | 1->1 | tutorial | FixedFront40 | Tutorial | 6 | 305 | HardBottleneckHeavy | tutorial_simple_01_hi |
| 23 | 2->4 | shape_magic | FixedFront40 | Normal | 80 | 257 | StartFakeWide|BottleneckHeavy | r2_outer_strong_005_r2_006_a401_procmask_79_long_maskproceduralvertical_30x44-mainmagicquilltall |
| 29 | 3->3 | dense | FixedFront40 | Normal | 99 | 230 | StartFakeWide | direct_pure_normal_extra_02_11_direct_normal_pure_extra_rect_dense_weave_g19_v05 |
| 38 | 2->3 | shape_magic | FixedFront40 | Normal | 88 | 209 | GrindyLowClear | r2_outer_strong_005_r2_006_a401_procmask_06_long_maskproceduralvertical_30x42-mainkeytall |
| 47 | 3->3 | lock | Peak | VeryHard | 118 | 213 | GrindyLowClear|PeakTooFlat | direct_normal_73_direct_normal_rect_lock_buckle_g1_v08 |
| 100 | 1->1 | lock | Normal | Normal | 51 | 119 | StartFakeWide | direct_pure_topup_01_11_direct_refresh_topup_rect_lock_buckle_g1_v02 |
| 105 | 1->2 | maze | Recovery | Recovery | 108 | 173 | GrindyLowClear | direct_normal_60_direct_normal_rect_maze_long_chain_g5_v06 |
| 124 | 1->3 | section | Normal | Normal | 105 | 234 | GrindyLowClear | direct_normal_topup_79_direct_normal_topup_rect_quasi_symmetry_g9_v10 |
| 148 | 2->4 | section | Pressure | Hard | 105 | 237 | StartFakeWide|PeakTooFlat | direct_normal_08_direct_normal_rect_stair_push_g0_v01 |
| 151 | 2->1 | hole_rescue | HoleRescue | Mechanic | 51 | 153 | GrindyLowClear | hole_candidate_060_tall_20x28_lock |
| 156 | 3->3 | section | Peak | VeryHard | 108 | 155 | GrindyLowClear|PeakTooFlat | direct_normal_29_direct_normal_rect_quasi_symmetry_g2_v03 |
| 169 | 2->4 | lock | Pressure | Hard | 115 | 208 | BottleneckHeavy|PeakTooFlat | direct_normal_31_direct_normal_rect_outer_shell_g3_v04 |
| 192 | 2->1 | hole_rescue | HoleRescue | Mechanic | 54 | 118 | GrindyLowClear | hole_candidate_063_tall_20x30_lock |
| 209 | 3->4 | lock | Peak | VeryHard | 157 | 250 | GrindyLowClear | direct_pure_topup_08_08_direct_polish_hard_pure_rect_lock_buckle_g12_v04 |
| 221 | 1->3 | dense | Recovery | Recovery | 109 | 200 | GrindyLowClear | direct_normal_topup_11_direct_normal_topup_rect_dense_weave_g7_v02 |
| 241 | 1->4 | sweep | Recovery | Recovery | 120 | 254 | BottleneckHeavy|GrindyLowClear | direct_normal_115_direct_normal_rect_sweep_g5_v12 |
| 261 | 1->2 | sweep | Recovery | Recovery | 120 | 227 | StartFakeWide | direct_normal_75_direct_normal_rect_sweep_g1_v08 |
| 269 | 4->4 | lock | Peak | RelativeExtreme | 203 | 324 | GrindyLowClear | direct_advanced_17_direct_advanced_veryhard_rect_lock_buckle_g2_v03 |
| 292 | 2->2 | hole_rescue | HoleRescue | Mechanic | 62 | 230 | StartFakeWide|BottleneckHeavy | hole_candidate_040_std_26x26_section |
| 299 | 4->4 | lock | Peak | RelativeExtreme | 318 | 321 | GrindyLowClear | direct_advanced_51_direct_advanced_extreme_rect_core_burst_g4_v01 |
| 306 | 1->2 | maze | Recovery | Recovery | 108 | 232 | StartFakeWide | direct_pure_normal_extra_01_24_direct_normal_pure_extra_rect_maze_long_chain_g17_v03 |
| 324 | 2->4 | section | Pressure | Hard | 186 | 329 | BottleneckHeavy|GrindyLowClear | direct_advanced_32_direct_advanced_veryhard_rect_section_unlock_g3_v04 |
| 344 | 2->3 | section | Pressure | Hard | 286 | 313 | GrindyLowClear | direct_advanced_69_direct_advanced_extreme_rect_stair_push_g6_v03 |
| 351 | 3->1 | hole_rescue | HoleRescue | Mechanic | 70 | 116 | GrindyLowClear | hole_candidate_080_wide_30x26_dual |
| 359 | 4->4 | section | Peak | RelativeExtreme | 270 | 321 | GrindyLowClear | direct_advanced_61_direct_advanced_extreme_rect_stair_push_g5_v02 |
| 370 | 1->2 | section | Normal | Normal | 149 | 218 | GrindyLowClear | direct_pure_topup_09_19_direct_polish_hard_pure_rect_quasi_symmetry_g9_v07 |
| 371 | 1->1 | maze | Normal | Normal | 100 | 201 | GrindyLowClear | direct_pure_normal_extra_01_08_direct_normal_pure_extra_rect_maze_long_chain_g15_v01 |
| 385 | 3->1 | hole_rescue | HoleRescue | Mechanic | 72 | 126 | GrindyLowClear | hole_candidate_068_tall_28x30_dual |
| 388 | 2->4 | section | Pressure | Hard | 305 | 368 | BottleneckHeavy|GrindyLowClear | direct_advanced_70_direct_advanced_extreme_rect_quasi_symmetry_g6_v03 |
| 394 | 2->3 | lock | Pressure | Hard | 214 | 277 | GrindyLowClear | direct_advanced_35_direct_advanced_veryhard_rect_core_burst_g0_v05 |
| 395 | 1->1 | dense | Normal | Normal | 118 | 182 | GrindyLowClear | direct_normal_94_direct_normal_rect_dense_weave_g3_v10 |
| 396 | 4->4 | section | Peak | RelativeExtreme | 181 | 314 | GrindyLowClear | direct_advanced_16_direct_advanced_veryhard_rect_section_unlock_g1_v02 |
| 412 | 2->1 | section | Pressure | Hard | 157 | 216 | GrindyLowClear | direct_pure_topup_10_24_direct_polish_hard_pure_rect_stair_push_g12_v10 |
| 426 | 3->1 | hole_rescue | HoleRescue | Mechanic | 77 | 203 | GrindyLowClear | hole_topup_050_topup_wide_30x26_dense |
| 435 | 4->1 | hole_rescue | HoleRescue | Mechanic | 79 | 193 | GrindyLowClear | hole_candidate_096_wide_34x26_lock |
| 454 | 2->2 | section | Pressure | Hard | 159 | 221 | GrindyLowClear | direct_pure_topup_09_07_direct_polish_hard_pure_rect_dual_zone_g14_v06 |
| 469 | 2->4 | maze | Pressure | Hard | 366 | 417 | BottleneckHeavy|GrindyLowClear | direct_polish_extreme_long_03_direct_polish_extreme_rect_hui_spiral_g9_v01 |
| 492 | 2->1 | hole_rescue | HoleRescue | Mechanic | 63 | 179 | GrindyLowClear | hole_candidate_041_std_26x26_maze |
| 499 | 1->3 | section | Recovery | Recovery | 158 | 286 | GrindyLowClear | direct_pure_topup_09_09_direct_polish_hard_pure_rect_quasi_symmetry_g14_v06 |
