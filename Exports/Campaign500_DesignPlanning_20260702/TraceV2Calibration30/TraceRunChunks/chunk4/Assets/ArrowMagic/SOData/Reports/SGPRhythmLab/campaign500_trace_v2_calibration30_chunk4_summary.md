# SGP Rhythm Trace Summary

Generated: 2026-07-02 10:23:52

- Requested rows: 5
- Traced rows: 5
- Missing/failed rows: 0
- Sort mode: Read
- Include unselected: False
- Input CSV: F:\Unityproject\ArrowLevel-Hand\Exports\Campaign500_DesignPlanning_20260702\TraceV2Calibration30\campaign500_trace_v2_calibration30_trace_input_chunk4.csv

## Trace Risk
- DependencyFollowRun: 5

## Process Tier
- B: 5

## Tight Process Tier
- B: 2
- Drop: 3

## Choice Pressure Reality
- over10Rate avg: 0.002
- maxChoices avg: 9.6
- choicePeakCount avg: 24.6
- choicePeakExcess avg: 37.4
- meaningfulUnlockRate avg: 0.805
- outerStraightRunMax avg: 0.4
- sameSideOuterStraightRunMax avg: 0.4
- outerNearFollowRunMax avg: 0.8
- sameSideOuterFollowRunMax avg: 0.8
- outerNearDependencyRate avg: 0.025
- stageLockScore avg: 0.506
- lateRegionCount avg: 1.8
- stageGateRate avg: 0.178
- structuredHardnessV21 avg: 0.48
- boringLinearScore avg: 0.346
- choiceChokeMomentScoreV1 avg: 0.77
- choiceChokeSwitchScoreV1 avg: 0.341
- choiceChokeHardBreakScoreV1 avg: 0.15
- choiceChokeCompositeBreakScoreV1 avg: 0.347
- choiceChokeCompositeBreakWindowCount avg: 1.2
- choiceChokeSweepContinuationRiskScore avg: 0.265
- choiceChokeWindowMaxLen avg: 4.8
- choiceChokeMidLateStepCount avg: 9.6
- choiceChokeAfterLocalSwitchCount avg: 2.6
- choiceChokeAfterLocalFrontierBreakCount avg: 0
- frontierDrainRemoteChokeCount avg: 1.2
- frontierDrainRemoteChokeScoreV1 avg: 0.495
- frontierDrainChokeRate avg: 0.21
- choiceChangeRate avg: 0.615
- choiceRiseRate avg: 0.244
- unlockPower avg: 0.516
- postSpikeConvergence avg: 0.08
- hardStructureV3Score avg: 0.076
- causalCudP20 avg: 6.621
- causalSolveDelayAvg avg: 4.49
- causalCrossRegionCriticalLockCount avg: 15.4
- causalAntiLocalityScore avg: 0.197

## Solve Trace Sketch
- solveTraceQualityScore avg: 0.825
- solveTraceCollapseRiskScore avg: 0.237
- solveRegionEntropy avg: 0.989
- solveRegionCollapseRunMax avg: 4.6
- solveSameAxisRunMax avg: 10.6
- solveSameDirHeadRunMax avg: 9.4
- solveFrontWidthAvg avg: 3.62
- solveFrontWidthNarrowRate avg: 0.192
- dependencyRegionEntropy avg: 0.987
- dependencyLocalSameRegionRate avg: 0.534

## HardStructure V3 Class
- LocalEasy: 5

## Process Keep Samples

## High HardStructure V3 Samples
- [B/LocalEasy] c5nep_w10smp_35_nutation_strict_mixed_peak_v1_rect_s22_o216_v02_strictmixedpeak_mixed_chain: hardV3=0.089, raw=0.554, cudP20=6.5, cudAvg=11.143, antiLocal=0.288, crossCrit=14, fanoutMax=4, solveDelay=3.975, dirRun=7, localPatchRun=5, outerExitRun=2, maxChoices=9, risk=DependencyFollowRun
- [B/LocalEasy] campaign500_normal_full_v1_s27_s38_129_nutation_peel_rail_v1_rect_s37_o369_v02_peelmid_rail_chain: hardV3=0.075, raw=0.559, cudP20=7.2, cudAvg=12.152, antiLocal=0.141, crossCrit=11, fanoutMax=5, solveDelay=4.962, dirRun=5, localPatchRun=6, outerExitRun=3, maxChoices=10, risk=DependencyFollowRun
- [B/LocalEasy] nutation_longchain_candidate80_holefix_s2_v1_47_nutation_longchain_patch_v1_rect_c80hf2_12_d3_maze_patch: hardV3=0.075, raw=0.553, cudP20=6.462, cudAvg=11.209, antiLocal=0.214, crossCrit=17, fanoutMax=4, solveDelay=5.863, dirRun=5, localPatchRun=6, outerExitRun=1, maxChoices=11, risk=DependencyFollowRun
- [B/LocalEasy] nutation_longchain_candidate80_v1_72_nutation_longchain_spine_v1_rect_c80_18_d4_weave_spine: hardV3=0.074, raw=0.547, cudP20=7.643, cudAvg=12.406, antiLocal=0.17, crossCrit=15, fanoutMax=4, solveDelay=2.557, dirRun=6, localPatchRun=7, outerExitRun=4, maxChoices=8, risk=DependencyFollowRun
- [B/LocalEasy] campaign500_shape_psg_next10b_07_L153_pumpkin: hardV3=0.069, raw=0.514, cudP20=5.299, cudAvg=9.062, antiLocal=0.171, crossCrit=20, fanoutMax=4, solveDelay=5.093, dirRun=24, localPatchRun=13, outerExitRun=0, maxChoices=10, risk=DependencyFollowRun

## High Choke Moment Samples
- [B] nutation_longchain_candidate80_v1_72_nutation_longchain_spine_v1_rect_c80_18_d4_weave_spine: choke=1, switch=0.3, hardBreak=0.413, composite=0.532/3, sweepRisk=0.479, drainRemote=2/0.838, low2=0.228, windows=6, switchSteps=18, afterLocalSwitch=8, afterLocalFrontierBreak=0, continuity=4, midLateSteps=21, best=80-85:2 1 1 2 2 2, switchBest=42-47:2 2 2 2 2 1|switch=42,44,45,46,47|afterLocal=42,44,45,46,47|frontierBreak=|newRegion=44,45,46|dirBreak=42,45,46,47, compositeBest=42-47:2 2 2 2 2 1|switch=42,44,45,46,47|afterLocal=42,44,45,46,47|frontierBreak=|newRegion=44,45,46|dirBreak=42,45,46,47, frontierChoke=0.37, frontierExplode=0, avg/max=3.71/8
- [B] c5nep_w10smp_35_nutation_strict_mixed_peak_v1_rect_s22_o216_v02_strictmixedpeak_mixed_chain: choke=0.929, switch=0.8, hardBreak=0.195, composite=0.46/2, sweepRisk=0.434, drainRemote=2/0.81, low2=0.145, windows=5, switchSteps=12, afterLocalSwitch=4, afterLocalFrontierBreak=0, continuity=5, midLateSteps=10, best=79-83:2 2 2 2 1, switchBest=79-83:2 2 2 2 1|switch=79,80,81,82,83|afterLocal=83|frontierBreak=|newRegion=|dirBreak=79,80,81,82,83, compositeBest=29-31:2 1 2|switch=29,30,31|afterLocal=29|frontierBreak=|newRegion=31|dirBreak=29,30,31, frontierChoke=0.193, frontierExplode=0.072, avg/max=4.87/9
- [B] campaign500_normal_full_v1_s27_s38_129_nutation_peel_rail_v1_rect_s37_o369_v02_peelmid_rail_chain: choke=0.899, switch=0.495, hardBreak=0.054, composite=0.157/0, sweepRisk=0.024, drainRemote=0/0, low2=0.083, windows=3, switchSteps=4, afterLocalSwitch=0, afterLocalFrontierBreak=0, continuity=2, midLateSteps=7, best=82-84:2 1 1, switchBest=82-84:2 1 1|switch=82,83|afterLocal=|frontierBreak=|newRegion=|dirBreak=82, compositeBest=, frontierChoke=0.31, frontierExplode=0.083, avg/max=6/10
- [B] nutation_longchain_candidate80_holefix_s2_v1_47_nutation_longchain_patch_v1_rect_c80hf2_12_d3_maze_patch: choke=0.618, switch=0.112, hardBreak=0.086, composite=0.44/1, sweepRisk=0.266, drainRemote=2/0.825, low2=0.066, windows=1, switchSteps=7, afterLocalSwitch=1, afterLocalFrontierBreak=0, continuity=3, midLateSteps=8, best=114-121:2 2 1 2 1 1 2 1, switchBest=114-121:2 2 1 2 1 1 2 1|switch=114,115,116,117,118,119,120|afterLocal=114|frontierBreak=117,119,120|newRegion=115,117,119,120|dirBreak=115,116,117,118,119,120, compositeBest=114-121:2 2 1 2 1 1 2 1|switch=114,115,116,117,118,119,120|afterLocal=114|frontierBreak=117,119,120|newRegion=115,117,119,120|dirBreak=115,116,117,118,119,120, frontierChoke=0.083, frontierExplode=0.256, avg/max=6.77/11
- [B] campaign500_shape_psg_next10b_07_L153_pumpkin: choke=0.404, switch=0, hardBreak=0, composite=0.147/0, sweepRisk=0.12, drainRemote=0/0, low2=0.023, windows=2, switchSteps=2, afterLocalSwitch=0, afterLocalFrontierBreak=0, continuity=2, midLateSteps=2, best=130-131:2 1, switchBest=130-131:2 1|switch=130,131|afterLocal=|frontierBreak=|newRegion=|dirBreak=130,131, compositeBest=, frontierChoke=0.092, frontierExplode=0.046, avg/max=6.08/10

## High Choke Switch Samples
- [B] c5nep_w10smp_35_nutation_strict_mixed_peak_v1_rect_s22_o216_v02_strictmixedpeak_mixed_chain: switch=0.8, hardBreak=0.195, composite=0.46/2, sweepRisk=0.434, drainRemote=2/0.81, steps=12, afterLocal=4, frontierBreak=0, afterLocalFrontierBreak=0, newRegion=2, dirBreak=12, continuity=5, low2SwitchRate=1, avgJump=11.945, regionSwitch=0.833, best=79-83:2 2 2 2 1|switch=79,80,81,82,83|afterLocal=83|frontierBreak=|newRegion=|dirBreak=79,80,81,82,83, compositeBest=29-31:2 1 2|switch=29,30,31|afterLocal=29|frontierBreak=|newRegion=31|dirBreak=29,30,31, localPatchRun=5, avg/max=4.87/9
- [B] campaign500_normal_full_v1_s27_s38_129_nutation_peel_rail_v1_rect_s37_o369_v02_peelmid_rail_chain: switch=0.495, hardBreak=0.054, composite=0.157/0, sweepRisk=0.024, drainRemote=0/0, steps=4, afterLocal=0, frontierBreak=0, afterLocalFrontierBreak=0, newRegion=0, dirBreak=3, continuity=2, low2SwitchRate=0.571, avgJump=19.75, regionSwitch=1, best=82-84:2 1 1|switch=82,83|afterLocal=|frontierBreak=|newRegion=|dirBreak=82, compositeBest=, localPatchRun=6, avg/max=6/10
- [B] nutation_longchain_candidate80_v1_72_nutation_longchain_spine_v1_rect_c80_18_d4_weave_spine: switch=0.3, hardBreak=0.413, composite=0.532/3, sweepRisk=0.479, drainRemote=2/0.838, steps=18, afterLocal=8, frontierBreak=2, afterLocalFrontierBreak=0, newRegion=7, dirBreak=17, continuity=4, low2SwitchRate=0.857, avgJump=18.721, regionSwitch=1, best=42-47:2 2 2 2 2 1|switch=42,44,45,46,47|afterLocal=42,44,45,46,47|frontierBreak=|newRegion=44,45,46|dirBreak=42,45,46,47, compositeBest=42-47:2 2 2 2 2 1|switch=42,44,45,46,47|afterLocal=42,44,45,46,47|frontierBreak=|newRegion=44,45,46|dirBreak=42,45,46,47, localPatchRun=7, avg/max=3.71/8
- [B] nutation_longchain_candidate80_holefix_s2_v1_47_nutation_longchain_patch_v1_rect_c80hf2_12_d3_maze_patch: switch=0.112, hardBreak=0.086, composite=0.44/1, sweepRisk=0.266, drainRemote=2/0.825, steps=7, afterLocal=1, frontierBreak=3, afterLocalFrontierBreak=0, newRegion=4, dirBreak=6, continuity=3, low2SwitchRate=0.875, avgJump=21.148, regionSwitch=1, best=114-121:2 2 1 2 1 1 2 1|switch=114,115,116,117,118,119,120|afterLocal=114|frontierBreak=117,119,120|newRegion=115,117,119,120|dirBreak=115,116,117,118,119,120, compositeBest=114-121:2 2 1 2 1 1 2 1|switch=114,115,116,117,118,119,120|afterLocal=114|frontierBreak=117,119,120|newRegion=115,117,119,120|dirBreak=115,116,117,118,119,120, localPatchRun=6, avg/max=6.77/11
- [B] campaign500_shape_psg_next10b_07_L153_pumpkin: switch=0, hardBreak=0, composite=0.147/0, sweepRisk=0.12, drainRemote=0/0, steps=2, afterLocal=0, frontierBreak=0, afterLocalFrontierBreak=0, newRegion=0, dirBreak=2, continuity=2, low2SwitchRate=0.667, avgJump=26.5, regionSwitch=1, best=130-131:2 1|switch=130,131|afterLocal=|frontierBreak=|newRegion=|dirBreak=130,131, compositeBest=, localPatchRun=13, avg/max=6.08/10

## High Structured Hardness Samples
- [B] nutation_longchain_candidate80_v1_72_nutation_longchain_spine_v1_rect_c80_18_d4_weave_spine: hardV21=0.576, boring=0.385, avg=3.71, max=8, changeRate=0.527, riseRate=0.209, unlockPower=0.495, postSpike=0, crossUnlock=0.789, risk=DependencyFollowRun
- [B] c5nep_w10smp_35_nutation_strict_mixed_peak_v1_rect_s22_o216_v02_strictmixedpeak_mixed_chain: hardV21=0.532, boring=0.309, avg=4.87, max=9, changeRate=0.61, riseRate=0.244, unlockPower=0.521, postSpike=0.167, crossUnlock=0.7, risk=DependencyFollowRun
- [B] campaign500_normal_full_v1_s27_s38_129_nutation_peel_rail_v1_rect_s37_o369_v02_peelmid_rail_chain: hardV21=0.478, boring=0.232, avg=6, max=10, changeRate=0.759, riseRate=0.265, unlockPower=0.579, postSpike=0.091, crossUnlock=0.5, risk=DependencyFollowRun
- [B] campaign500_shape_psg_next10b_07_L153_pumpkin: hardV21=0.407, boring=0.446, avg=6.08, max=10, changeRate=0.577, riseRate=0.254, unlockPower=0.488, postSpike=0.143, crossUnlock=0.606, risk=DependencyFollowRun
- [B] nutation_longchain_candidate80_holefix_s2_v1_47_nutation_longchain_patch_v1_rect_c80hf2_12_d3_maze_patch: hardV21=0.405, boring=0.36, avg=6.77, max=11, changeRate=0.6, riseRate=0.25, unlockPower=0.499, postSpike=0, crossUnlock=0.567, risk=DependencyFollowRun

## Boring Linear Risk Samples
- [B] campaign500_shape_psg_next10b_07_L153_pumpkin: boring=0.446, hardV21=0.407, avg=6.08, winStd=0.68, changeRate=0.577, flatRate=0.423, unlockPower=0.488, lowRun=2, sameRegionRun=4, nearRun=9, risk=DependencyFollowRun
- [B] nutation_longchain_candidate80_v1_72_nutation_longchain_spine_v1_rect_c80_18_d4_weave_spine: boring=0.385, hardV21=0.576, avg=3.71, winStd=0.702, changeRate=0.527, flatRate=0.473, unlockPower=0.495, lowRun=6, sameRegionRun=4, nearRun=3, risk=DependencyFollowRun
- [B] nutation_longchain_candidate80_holefix_s2_v1_47_nutation_longchain_patch_v1_rect_c80hf2_12_d3_maze_patch: boring=0.36, hardV21=0.405, avg=6.77, winStd=0.752, changeRate=0.6, flatRate=0.4, unlockPower=0.499, lowRun=8, sameRegionRun=4, nearRun=3, risk=DependencyFollowRun
- [B] c5nep_w10smp_35_nutation_strict_mixed_peak_v1_rect_s22_o216_v02_strictmixedpeak_mixed_chain: boring=0.309, hardV21=0.532, avg=4.87, winStd=0.814, changeRate=0.61, flatRate=0.39, unlockPower=0.521, lowRun=5, sameRegionRun=3, nearRun=1, risk=DependencyFollowRun
- [B] campaign500_normal_full_v1_s27_s38_129_nutation_peel_rail_v1_rect_s37_o369_v02_peelmid_rail_chain: boring=0.232, hardV21=0.478, avg=6, winStd=1.054, changeRate=0.759, flatRate=0.241, unlockPower=0.579, lowRun=3, sameRegionRun=3, nearRun=2, risk=DependencyFollowRun

## Worst Choice Curve Samples
- [B] nutation_longchain_candidate80_holefix_s2_v1_47_nutation_longchain_patch_v1_rect_c80hf2_12_d3_maze_patch: openers=4, avg=6.77, p80=8, max=11, peakCount=52, peakExcess=81, over10=0.008, outerRun=0, sameSideOuterRun=0, outerNearRun=2, sameSideOuterFollow=2, risk=DependencyFollowRun, reason=flow or late-campaign only
- [B] campaign500_normal_full_v1_s27_s38_129_nutation_peel_rail_v1_rect_s37_o369_v02_peelmid_rail_chain: openers=6, avg=6, p80=8, max=10, peakCount=25, peakExcess=39, over10=0, outerRun=1, sameSideOuterRun=1, outerNearRun=0, sameSideOuterFollow=0, risk=DependencyFollowRun, reason=flow or late-campaign only
- [B] campaign500_shape_psg_next10b_07_L153_pumpkin: openers=2, avg=6.08, p80=8, max=10, peakCount=31, peakExcess=42, over10=0, outerRun=0, sameSideOuterRun=0, outerNearRun=0, sameSideOuterFollow=0, risk=DependencyFollowRun, reason=flow or late-campaign only
- [B] c5nep_w10smp_35_nutation_strict_mixed_peak_v1_rect_s22_o216_v02_strictmixedpeak_mixed_chain: openers=3, avg=4.87, p80=7, max=9, peakCount=14, peakExcess=24, over10=0, outerRun=0, sameSideOuterRun=0, outerNearRun=1, sameSideOuterFollow=1, risk=DependencyFollowRun, reason=flow or late-campaign only
- [B] nutation_longchain_candidate80_v1_72_nutation_longchain_spine_v1_rect_c80_18_d4_weave_spine: openers=4, avg=3.71, p80=5, max=8, peakCount=1, peakExcess=1, over10=0, outerRun=1, sameSideOuterRun=1, outerNearRun=1, sameSideOuterFollow=1, risk=DependencyFollowRun, reason=flow or late-campaign only
