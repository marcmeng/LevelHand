# Campaign500 V4 singlelevels replacements for Unity

- Baseline singlelevels: `F:/Unityproject/ArrowLevel-Hand/Exports/Campaign500SingleLevels_Hole10V4_20260613_095022`
- Current official pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PlanPreviewPack.asset`
- Replacement count: `10`
- Scope: current official pack vs Campaign500SingleLevels_Hole10V4_20260613_095022 manifest.
- Result: only these 10 levels changed after the V4 singlelevels export; all are high-chain hole replacements.

| Level | MainMissionId | ExcelRow | Old LevelId | Old Chains | New LevelId | New Chains | Difficulty | Status |
|---:|---:|---:|---|---:|---|---:|---|---|
| 151 | 251 | 156 | `hole_candidate_060_tall_20x28_lock` | 51 | `hole_highchain_016_hc_30x28_dual_a` | 80 | 2->3 | Yellow->Yellow |
| 192 | 292 | 197 | `hole_candidate_063_tall_20x30_lock` | 54 | `hole_highchain_017_hc_28x30_dense_a` | 82 | 2->3 | Yellow->Yellow |
| 242 | 342 | 247 | `hole_candidate_073_wide_28x22_section` | 58 | `hole_highchain_004_hc_32x28_dual_a` | 83 | 2->1 | Green->Green |
| 267 | 367 | 272 | `hole_candidate_025_std_22x28_section` | 61 | `hole_highchain_extra_001_hc2_32x28_dual_b` | 82 | 2->1 | Green->Green |
| 301 | 401 | 306 | `hole_topup_033_topup_std_26x28_dual` | 65 | `hole_highchain_014_hc_34x28_shell_a` | 87 | 3->2 | Green->Yellow |
| 335 | 435 | 340 | `hole_topup_049_topup_wide_30x24_maze` | 67 | `hole_highchain_extra_003_hc2_32x28_lock_a` | 89 | 3->2 | Green->Yellow |
| 367 | 467 | 372 | `hole_candidate_078_wide_30x26_lock` | 72 | `hole_highchain_010_hc_34x26_dual_a` | 85 | 3->1 | Green->Green |
| 392 | 492 | 397 | `hole_candidate_042_std_26x28_lock` | 75 | `hole_highchain_extra_019_hc2_34x28_dual_b` | 87 | 3->2 | Green->Green |
| 451 | 551 | 456 | `hole_candidate_030_std_24x28_lock` | 64 | `hole_highchain_extra_016_hc2_32x30_dual_b` | 91 | 3->1 | Green->Yellow |
| 492 | 592 | 497 | `hole_candidate_041_std_26x26_maze` | 63 | `hole_highchain_extra_018_hc2_32x30_lock_a` | 90 | 2->2 | Yellow->Yellow |

## Paths to update

### Level 151
- Old singlelevel asset: `F:/Unityproject/ArrowLevel-Hand/Exports/Campaign500SingleLevels_Hole10V4_20260613_095022/Levels/151_hole_rescue_hole_candidate_060_tall_20x28_lock.asset`
- Old source asset: `Assets/ArrowMagic/SOData/Levels/HoleProcedural/CandidatePool/hole_candidate_060_tall_20x28_lock.asset`
- New source asset: `Assets/ArrowMagic/SOData/Levels/HoleProcedural/HighChainCandidates/hole_highchain_016_hc_30x28_dual_a.asset`
- Note: v2 mid-stage stronger hole; yellow pressure acceptable if no red

### Level 192
- Old singlelevel asset: `F:/Unityproject/ArrowLevel-Hand/Exports/Campaign500SingleLevels_Hole10V4_20260613_095022/Levels/192_hole_rescue_hole_candidate_063_tall_20x30_lock.asset`
- Old source asset: `Assets/ArrowMagic/SOData/Levels/HoleProcedural/CandidatePool/hole_candidate_063_tall_20x30_lock.asset`
- New source asset: `Assets/ArrowMagic/SOData/Levels/HoleProcedural/HighChainCandidates/hole_highchain_017_hc_28x30_dense_a.asset`
- Note: v2 second 150-200 pressure sample

### Level 242
- Old singlelevel asset: `F:/Unityproject/ArrowLevel-Hand/Exports/Campaign500SingleLevels_Hole10V4_20260613_095022/Levels/242_hole_rescue_hole_candidate_073_wide_28x22_section.asset`
- Old source asset: `Assets/ArrowMagic/SOData/Levels/HoleProcedural/CandidatePool/hole_candidate_073_wide_28x22_section.asset`
- New source asset: `Assets/ArrowMagic/SOData/Levels/HoleProcedural/HighChainCandidates/hole_highchain_004_hc_32x28_dual_a.asset`
- Note: v2 early 200s hole pressure

### Level 267
- Old singlelevel asset: `F:/Unityproject/ArrowLevel-Hand/Exports/Campaign500SingleLevels_Hole10V4_20260613_095022/Levels/267_hole_rescue_hole_candidate_025_std_22x28_section.asset`
- Old source asset: `Assets/ArrowMagic/SOData/Levels/HoleProcedural/CandidatePool/hole_candidate_025_std_22x28_section.asset`
- New source asset: `Assets/ArrowMagic/SOData/Levels/HoleProcedural/HighChainCandidates/hole_highchain_extra_001_hc2_32x28_dual_b.asset`
- Note: v2 late 200s hole pressure

### Level 301
- Old singlelevel asset: `F:/Unityproject/ArrowLevel-Hand/Exports/Campaign500SingleLevels_Hole10V4_20260613_095022/Levels/301_hole_rescue_hole_topup_033_topup_std_26x28_dual.asset`
- Old source asset: `Assets/ArrowMagic/SOData/Levels/HoleProcedural/TopupCandidates/hole_topup_033_topup_std_26x28_dual.asset`
- New source asset: `Assets/ArrowMagic/SOData/Levels/HoleProcedural/HighChainCandidates/hole_highchain_014_hc_34x28_shell_a.asset`
- Note: v2 first 300s higher-hole check

### Level 335
- Old singlelevel asset: `F:/Unityproject/ArrowLevel-Hand/Exports/Campaign500SingleLevels_Hole10V4_20260613_095022/Levels/335_hole_rescue_hole_topup_049_topup_wide_30x24_maze.asset`
- Old source asset: `Assets/ArrowMagic/SOData/Levels/HoleProcedural/TopupCandidates/hole_topup_049_topup_wide_30x24_maze.asset`
- New source asset: `Assets/ArrowMagic/SOData/Levels/HoleProcedural/HighChainCandidates/hole_highchain_extra_003_hc2_32x28_lock_a.asset`
- Note: v2 mid 300s higher-hole check

### Level 367
- Old singlelevel asset: `F:/Unityproject/ArrowLevel-Hand/Exports/Campaign500SingleLevels_Hole10V4_20260613_095022/Levels/367_hole_rescue_hole_candidate_078_wide_30x26_lock.asset`
- Old source asset: `Assets/ArrowMagic/SOData/Levels/HoleProcedural/CandidatePool/hole_candidate_078_wide_30x26_lock.asset`
- New source asset: `Assets/ArrowMagic/SOData/Levels/HoleProcedural/HighChainCandidates/hole_highchain_010_hc_34x26_dual_a.asset`
- Note: v2 replaces red section candidate with steadier dual candidate

### Level 392
- Old singlelevel asset: `F:/Unityproject/ArrowLevel-Hand/Exports/Campaign500SingleLevels_Hole10V4_20260613_095022/Levels/392_hole_rescue_hole_candidate_042_std_26x28_lock.asset`
- Old source asset: `Assets/ArrowMagic/SOData/Levels/HoleProcedural/CandidatePool/hole_candidate_042_std_26x28_lock.asset`
- New source asset: `Assets/ArrowMagic/SOData/Levels/HoleProcedural/HighChainCandidates/hole_highchain_extra_019_hc2_34x28_dual_b.asset`
- Note: v2 late 300s second sample

### Level 451
- Old singlelevel asset: `F:/Unityproject/ArrowLevel-Hand/Exports/Campaign500SingleLevels_Hole10V4_20260613_095022/Levels/451_hole_rescue_hole_candidate_030_std_24x28_lock.asset`
- Old source asset: `Assets/ArrowMagic/SOData/Levels/HoleProcedural/CandidatePool/hole_candidate_030_std_24x28_lock.asset`
- New source asset: `Assets/ArrowMagic/SOData/Levels/HoleProcedural/HighChainCandidates/hole_highchain_extra_016_hc2_32x30_dual_b.asset`
- Note: v2 first 400+ stronger hole

### Level 492
- Old singlelevel asset: `F:/Unityproject/ArrowLevel-Hand/Exports/Campaign500SingleLevels_Hole10V4_20260613_095022/Levels/492_hole_rescue_hole_candidate_041_std_26x26_maze.asset`
- Old source asset: `Assets/ArrowMagic/SOData/Levels/HoleProcedural/CandidatePool/hole_candidate_041_std_26x26_maze.asset`
- New source asset: `Assets/ArrowMagic/SOData/Levels/HoleProcedural/HighChainCandidates/hole_highchain_extra_018_hc2_32x30_lock_a.asset`
- Note: v2 late campaign stronger hole
