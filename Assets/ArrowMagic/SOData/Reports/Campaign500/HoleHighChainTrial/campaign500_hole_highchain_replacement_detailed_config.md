# Hole High-Chain Replacement Detailed Config

Excel writeback: `diffculty` column uses V4 after high-chain proposed difficulty.

## Summary

- Replaced hole levels: 10
- Official validation after replacement: Green=458, Yellow=42, Red=0
- Excel difficulty distribution after writeback: 1=302, 2=102, 3=58, 4=38
- Excel backup before writeback: `E:\bm\Excels_ArrawGame\G_关卡.backup_v4_hole_highchain_20260613_220813.xlsx`

## Replacements

| level | excel row | mission id | levelName | Excel diff before->after | V4 diff before->after | score before->after | chains before->after | new status | old level -> new level |
|---:|---:|---:|---|---:|---:|---:|---:|---|---|
| 151 | 156 | 251 | Main/Level_Main_151_hole | 2->3 | 1->3 | 153->201 | 51->80 | Yellow StartFakeWide|BottleneckHeavy | hole_candidate_060_tall_20x28_lock -> hole_highchain_016_hc_30x28_dual_a |
| 192 | 197 | 292 | Main/Level_Main_192_hole | 2->3 | 1->3 | 118->235 | 54->82 | Yellow StartFakeWide | hole_candidate_063_tall_20x30_lock -> hole_highchain_017_hc_28x30_dense_a |
| 242 | 247 | 342 | Main/Level_Main_242_hole | 2->1 | 1->1 | 74->109 | 58->83 | Green None | hole_candidate_073_wide_28x22_section -> hole_highchain_004_hc_32x28_dual_a |
| 267 | 272 | 367 | Main/Level_Main_267_hole | 2->1 | 1->1 | 169->179 | 61->82 | Green BottleneckHeavy | hole_candidate_025_std_22x28_section -> hole_highchain_extra_001_hc2_32x28_dual_b |
| 301 | 306 | 401 | Main/Level_Main_301_hole | 3->2 | 1->2 | 172->250 | 65->87 | Yellow BottleneckHeavy|GrindyLowClear | hole_topup_033_topup_std_26x28_dual -> hole_highchain_014_hc_34x28_shell_a |
| 335 | 340 | 435 | Main/Level_Main_335_hole | 3->2 | 1->2 | 115->230 | 67->89 | Yellow BottleneckHeavy|GrindyLowClear | hole_topup_049_topup_wide_30x24_maze -> hole_highchain_extra_003_hc2_32x28_lock_a |
| 367 | 372 | 467 | Main/Level_Main_367_hole | 3->1 | 1->1 | 106->161 | 72->85 | Green BottleneckHeavy | hole_candidate_078_wide_30x26_lock -> hole_highchain_010_hc_34x26_dual_a |
| 392 | 397 | 492 | Main/Level_Main_392_hole | 3->2 | 1->2 | 141->229 | 75->87 | Green None | hole_candidate_042_std_26x28_lock -> hole_highchain_extra_019_hc2_34x28_dual_b |
| 451 | 456 | 551 | Main/Level_Main_451_hole | 3->1 | 1->1 | 145->141 | 64->91 | Yellow GrindyLowClear | hole_candidate_030_std_24x28_lock -> hole_highchain_extra_016_hc2_32x30_dual_b |
| 492 | 497 | 592 | Main/Level_Main_492_hole | 2->2 | 1->2 | 179->230 | 63->90 | Yellow StartFakeWide|BottleneckHeavy | hole_candidate_041_std_26x26_maze -> hole_highchain_extra_018_hc2_32x30_lock_a |

## Full Detail

CSV: `F:/Unityproject/ArrowLevel-Hand/Assets/ArrowMagic/SOData/Reports/Campaign500/HoleHighChainTrial/campaign500_hole_highchain_replacement_detailed_config.csv`