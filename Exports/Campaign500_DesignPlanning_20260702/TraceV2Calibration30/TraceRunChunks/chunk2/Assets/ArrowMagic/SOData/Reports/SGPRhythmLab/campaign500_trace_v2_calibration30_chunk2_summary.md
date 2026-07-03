# SGP Rhythm Trace Summary

Generated: 2026-07-02 10:14:00

- Requested rows: 5
- Traced rows: 5
- Missing/failed rows: 0
- Sort mode: Read
- Include unselected: False
- Input CSV: F:\Unityproject\ArrowLevel-Hand\Exports\Campaign500_DesignPlanning_20260702\TraceV2Calibration30\campaign500_trace_v2_calibration30_trace_input_chunk2.csv

## Trace Risk
- OuterExitHeadVisual: 3
- DependencyFollowRun: 2

## Process Tier
- A: 1
- B: 4

## Tight Process Tier
- B: 2
- Drop: 3

## Choice Pressure Reality
- over10Rate avg: 0
- maxChoices avg: 9
- choicePeakCount avg: 9.4
- choicePeakExcess avg: 14.6
- meaningfulUnlockRate avg: 0.873
- outerStraightRunMax avg: 0.6
- sameSideOuterStraightRunMax avg: 0.6
- outerNearFollowRunMax avg: 1.2
- sameSideOuterFollowRunMax avg: 1
- outerNearDependencyRate avg: 0.042
- stageLockScore avg: 0.438
- lateRegionCount avg: 1.6
- stageGateRate avg: 0.143
- structuredHardnessV21 avg: 0.523
- boringLinearScore avg: 0.309
- choiceChokeMomentScoreV1 avg: 0.728
- choiceChokeSwitchScoreV1 avg: 0.676
- choiceChokeHardBreakScoreV1 avg: 0.146
- choiceChokeCompositeBreakScoreV1 avg: 0.283
- choiceChokeCompositeBreakWindowCount avg: 0.2
- choiceChokeSweepContinuationRiskScore avg: 0.01
- choiceChokeWindowMaxLen avg: 2.6
- choiceChokeMidLateStepCount avg: 4.2
- choiceChokeAfterLocalSwitchCount avg: 0.8
- choiceChokeAfterLocalFrontierBreakCount avg: 0
- frontierDrainRemoteChokeCount avg: 0.8
- frontierDrainRemoteChokeScoreV1 avg: 0.344
- frontierDrainChokeRate avg: 0.307
- choiceChangeRate avg: 0.649
- choiceRiseRate avg: 0.244
- unlockPower avg: 0.513
- postSpikeConvergence avg: 0.253
- hardStructureV3Score avg: 0.109
- causalCudP20 avg: 7.376
- causalSolveDelayAvg avg: 4.285
- causalCrossRegionCriticalLockCount avg: 10.6
- causalAntiLocalityScore avg: 0.256

## Solve Trace Sketch
- solveTraceQualityScore avg: 0.851
- solveTraceCollapseRiskScore avg: 0.197
- solveRegionEntropy avg: 0.988
- solveRegionCollapseRunMax avg: 4.2
- solveSameAxisRunMax avg: 6.2
- solveSameDirHeadRunMax avg: 6.2
- solveFrontWidthAvg avg: 3.99
- solveFrontWidthNarrowRate avg: 0.146
- dependencyRegionEntropy avg: 0.983
- dependencyLocalSameRegionRate avg: 0.462

## HardStructure V3 Class
- LocalEasy: 5

## Process Keep Samples
- [A] nutation_longchain_candidate80_holefix_v1_16_nutation_longchain_spine_v1_rect_c80hf_04_d1_core_spine: openers=6, avg=6.07, p80=7, max=8, peakCount=6, peakExcess=6, over10=0, meaningful=0.963, stageScore=0.3, hardV21=0.433, boring=0.368, unlockPower=0.449, postSpike=0, outerNearRun=1, sameSideOuterRun=1

## High HardStructure V3 Samples
- [B/LocalEasy] nutation_longchain_candidate80_holefix_v1_15_nutation_longchain_patch_v1_rect_c80hf_04_d1_core_patch: hardV3=0.157, raw=0.619, cudP20=7.619, cudAvg=12.544, antiLocal=0.314, crossCrit=13, fanoutMax=4, solveDelay=5.086, dirRun=6, localPatchRun=4, outerExitRun=2, maxChoices=10, risk=OuterExitHeadVisual
- [A/LocalEasy] nutation_longchain_candidate80_holefix_v1_16_nutation_longchain_spine_v1_rect_c80hf_04_d1_core_spine: hardV3=0.135, raw=0.62, cudP20=7.611, cudAvg=12.174, antiLocal=0.278, crossCrit=10, fanoutMax=3, solveDelay=4.815, dirRun=8, localPatchRun=4, outerExitRun=4, maxChoices=8, risk=DependencyFollowRun
- [B/LocalEasy] campaign500_normal_full_v1_s01_s13_13_nutation_flow_rail_v1_rect_s02_o011_v00_flowrail_rail_chain: hardV3=0.107, raw=0.667, cudP20=8.955, cudAvg=12.529, antiLocal=0.286, crossCrit=6, fanoutMax=5, solveDelay=4.114, dirRun=5, localPatchRun=5, outerExitRun=2, maxChoices=9, risk=OuterExitHeadVisual
- [B/LocalEasy] campaign500_shape_psg_direction3_01_L023_train: hardV3=0.075, raw=0.553, cudP20=6.424, cudAvg=10.832, antiLocal=0.235, crossCrit=13, fanoutMax=3, solveDelay=3.894, dirRun=6, localPatchRun=7, outerExitRun=1, maxChoices=10, risk=DependencyFollowRun
- [B/LocalEasy] nutation_longchain_candidate80_holefix_v1_03_nutation_longchain_patch_v1_rect_c80hf_01_d1_lock_patch: hardV3=0.071, raw=0.529, cudP20=6.273, cudAvg=10.572, antiLocal=0.167, crossCrit=11, fanoutMax=4, solveDelay=3.517, dirRun=6, localPatchRun=5, outerExitRun=1, maxChoices=8, risk=OuterExitHeadVisual

## High Choke Moment Samples
- [B] nutation_longchain_candidate80_holefix_v1_03_nutation_longchain_patch_v1_rect_c80hf_01_d1_lock_patch: choke=1, switch=0.83, hardBreak=0.233, composite=0.66/1, sweepRisk=0, drainRemote=2/0.866, low2=0.125, windows=3, switchSteps=7, afterLocalSwitch=1, afterLocalFrontierBreak=0, continuity=2, midLateSteps=8, best=50-53:2 1 1 1, switchBest=50-53:2 1 1 1|switch=51,52,53|afterLocal=51|frontierBreak=52,53|newRegion=52,53|dirBreak=51,52,53, compositeBest=50-53:2 1 1 1|switch=51,52,53|afterLocal=51|frontierBreak=52,53|newRegion=52,53|dirBreak=51,52,53, frontierChoke=0.375, frontierExplode=0.016, avg/max=4.62/8
- [B] campaign500_normal_full_v1_s01_s13_13_nutation_flow_rail_v1_rect_s02_o011_v00_flowrail_rail_chain: choke=0.946, switch=0.83, hardBreak=0.174, composite=0.28/0, sweepRisk=0, drainRemote=0/0, low2=0.15, windows=3, switchSteps=4, afterLocalSwitch=1, afterLocalFrontierBreak=0, continuity=2, midLateSteps=5, best=8-10:2 2 2, switchBest=39-40:2 1|switch=39,40|afterLocal=|frontierBreak=|newRegion=|dirBreak=39,40, compositeBest=, frontierChoke=0.35, frontierExplode=0.125, avg/max=5.08/9
- [A] nutation_longchain_candidate80_holefix_v1_16_nutation_longchain_spine_v1_rect_c80hf_04_d1_core_spine: choke=0.585, switch=0.43, hardBreak=0.04, composite=0.04/0, sweepRisk=0, drainRemote=0/0, low2=0.033, windows=1, switchSteps=1, afterLocalSwitch=0, afterLocalFrontierBreak=0, continuity=1, midLateSteps=2, best=59-60:2 1, switchBest=59-60:2 1|switch=59|afterLocal=|frontierBreak=|newRegion=|dirBreak=59, compositeBest=, frontierChoke=0.333, frontierExplode=0, avg/max=6.07/8
- [B] nutation_longchain_candidate80_holefix_v1_15_nutation_longchain_patch_v1_rect_c80hf_04_d1_core_patch: choke=0.566, switch=1, hardBreak=0.24, composite=0.28/0, sweepRisk=0, drainRemote=2/0.853, low2=0.053, windows=2, switchSteps=3, afterLocalSwitch=2, afterLocalFrontierBreak=0, continuity=2, midLateSteps=4, best=74-75:2 1, switchBest=74-75:2 1|switch=74,75|afterLocal=74|frontierBreak=|newRegion=|dirBreak=74,75, compositeBest=, frontierChoke=0.133, frontierExplode=0.12, avg/max=6.17/10
- [B] campaign500_shape_psg_direction3_01_L023_train: choke=0.541, switch=0.29, hardBreak=0.045, composite=0.155/0, sweepRisk=0.048, drainRemote=0/0, low2=0.034, windows=2, switchSteps=2, afterLocalSwitch=0, afterLocalFrontierBreak=0, continuity=2, midLateSteps=2, best=86-87:2 1, switchBest=86-87:2 1|switch=86,87|afterLocal=|frontierBreak=|newRegion=|dirBreak=86,87, compositeBest=, frontierChoke=0.345, frontierExplode=0.046, avg/max=5.43/10

## High Choke Switch Samples
- [B] nutation_longchain_candidate80_holefix_v1_15_nutation_longchain_patch_v1_rect_c80hf_04_d1_core_patch: switch=1, hardBreak=0.24, composite=0.28/0, sweepRisk=0, drainRemote=2/0.853, steps=3, afterLocal=2, frontierBreak=0, afterLocalFrontierBreak=0, newRegion=0, dirBreak=3, continuity=2, low2SwitchRate=0.75, avgJump=17.311, regionSwitch=0.667, best=74-75:2 1|switch=74,75|afterLocal=74|frontierBreak=|newRegion=|dirBreak=74,75, compositeBest=, localPatchRun=4, avg/max=6.17/10
- [B] nutation_longchain_candidate80_holefix_v1_03_nutation_longchain_patch_v1_rect_c80hf_01_d1_lock_patch: switch=0.83, hardBreak=0.233, composite=0.66/1, sweepRisk=0, drainRemote=2/0.866, steps=7, afterLocal=1, frontierBreak=2, afterLocalFrontierBreak=0, newRegion=3, dirBreak=7, continuity=2, low2SwitchRate=0.875, avgJump=21.4, regionSwitch=1, best=50-53:2 1 1 1|switch=51,52,53|afterLocal=51|frontierBreak=52,53|newRegion=52,53|dirBreak=51,52,53, compositeBest=50-53:2 1 1 1|switch=51,52,53|afterLocal=51|frontierBreak=52,53|newRegion=52,53|dirBreak=51,52,53, localPatchRun=5, avg/max=4.62/8
- [B] campaign500_normal_full_v1_s01_s13_13_nutation_flow_rail_v1_rect_s02_o011_v00_flowrail_rail_chain: switch=0.83, hardBreak=0.174, composite=0.28/0, sweepRisk=0, drainRemote=0/0, steps=4, afterLocal=1, frontierBreak=0, afterLocalFrontierBreak=0, newRegion=0, dirBreak=4, continuity=2, low2SwitchRate=0.667, avgJump=15.844, regionSwitch=1, best=39-40:2 1|switch=39,40|afterLocal=|frontierBreak=|newRegion=|dirBreak=39,40, compositeBest=, localPatchRun=5, avg/max=5.08/9
- [A] nutation_longchain_candidate80_holefix_v1_16_nutation_longchain_spine_v1_rect_c80hf_04_d1_core_spine: switch=0.43, hardBreak=0.04, composite=0.04/0, sweepRisk=0, drainRemote=0/0, steps=1, afterLocal=0, frontierBreak=0, afterLocalFrontierBreak=0, newRegion=0, dirBreak=1, continuity=1, low2SwitchRate=0.5, avgJump=43.333, regionSwitch=1, best=59-60:2 1|switch=59|afterLocal=|frontierBreak=|newRegion=|dirBreak=59, compositeBest=, localPatchRun=4, avg/max=6.07/8
- [B] campaign500_shape_psg_direction3_01_L023_train: switch=0.29, hardBreak=0.045, composite=0.155/0, sweepRisk=0.048, drainRemote=0/0, steps=2, afterLocal=0, frontierBreak=0, afterLocalFrontierBreak=0, newRegion=0, dirBreak=2, continuity=2, low2SwitchRate=0.667, avgJump=30.5, regionSwitch=1, best=86-87:2 1|switch=86,87|afterLocal=|frontierBreak=|newRegion=|dirBreak=86,87, compositeBest=, localPatchRun=7, avg/max=5.43/10

## High Structured Hardness Samples
- [B] nutation_longchain_candidate80_holefix_v1_03_nutation_longchain_patch_v1_rect_c80hf_01_d1_lock_patch: hardV21=0.6, boring=0.271, avg=4.62, max=8, changeRate=0.746, riseRate=0.302, unlockPower=0.507, postSpike=0.4, crossUnlock=0.579, risk=OuterExitHeadVisual
- [B] campaign500_normal_full_v1_s01_s13_13_nutation_flow_rail_v1_rect_s02_o011_v00_flowrail_rail_chain: hardV21=0.558, boring=0.217, avg=5.08, max=9, changeRate=0.667, riseRate=0.179, unlockPower=0.627, postSpike=0, crossUnlock=0.857, risk=OuterExitHeadVisual
- [B] nutation_longchain_candidate80_holefix_v1_15_nutation_longchain_patch_v1_rect_c80hf_04_d1_core_patch: hardV21=0.548, boring=0.269, avg=6.17, max=10, changeRate=0.662, riseRate=0.257, unlockPower=0.504, postSpike=0.667, crossUnlock=0.684, risk=OuterExitHeadVisual
- [B] campaign500_shape_psg_direction3_01_L023_train: hardV21=0.474, boring=0.421, avg=5.43, max=10, changeRate=0.628, riseRate=0.279, unlockPower=0.478, postSpike=0.2, crossUnlock=0.542, risk=DependencyFollowRun
- [A] nutation_longchain_candidate80_holefix_v1_16_nutation_longchain_spine_v1_rect_c80hf_04_d1_core_spine: hardV21=0.433, boring=0.368, avg=6.07, max=8, changeRate=0.542, riseRate=0.203, unlockPower=0.449, postSpike=0, crossUnlock=0.833, risk=DependencyFollowRun

## Boring Linear Risk Samples
- [B] campaign500_shape_psg_direction3_01_L023_train: boring=0.421, hardV21=0.474, avg=5.43, winStd=0.777, changeRate=0.628, flatRate=0.372, unlockPower=0.478, lowRun=2, sameRegionRun=6, nearRun=5, risk=DependencyFollowRun
- [A] nutation_longchain_candidate80_holefix_v1_16_nutation_longchain_spine_v1_rect_c80hf_04_d1_core_spine: boring=0.368, hardV21=0.433, avg=6.07, winStd=0.649, changeRate=0.542, flatRate=0.458, unlockPower=0.449, lowRun=2, sameRegionRun=3, nearRun=1, risk=DependencyFollowRun
- [B] nutation_longchain_candidate80_holefix_v1_03_nutation_longchain_patch_v1_rect_c80hf_01_d1_lock_patch: boring=0.271, hardV21=0.6, avg=4.62, winStd=0.924, changeRate=0.746, flatRate=0.254, unlockPower=0.507, lowRun=4, sameRegionRun=3, nearRun=2, risk=OuterExitHeadVisual
- [B] nutation_longchain_candidate80_holefix_v1_15_nutation_longchain_patch_v1_rect_c80hf_04_d1_core_patch: boring=0.269, hardV21=0.548, avg=6.17, winStd=0.809, changeRate=0.662, flatRate=0.338, unlockPower=0.504, lowRun=2, sameRegionRun=2, nearRun=1, risk=OuterExitHeadVisual
- [B] campaign500_normal_full_v1_s01_s13_13_nutation_flow_rail_v1_rect_s02_o011_v00_flowrail_rail_chain: boring=0.217, hardV21=0.558, avg=5.08, winStd=0.946, changeRate=0.667, flatRate=0.333, unlockPower=0.627, lowRun=3, sameRegionRun=2, nearRun=2, risk=OuterExitHeadVisual

## Worst Choice Curve Samples
- [B] nutation_longchain_candidate80_holefix_v1_15_nutation_longchain_patch_v1_rect_c80hf_04_d1_core_patch: openers=5, avg=6.17, p80=8, max=10, peakCount=19, peakExcess=29, over10=0, outerRun=0, sameSideOuterRun=0, outerNearRun=1, sameSideOuterFollow=1, risk=OuterExitHeadVisual, reason=flow or late-campaign only
- [B] campaign500_shape_psg_direction3_01_L023_train: openers=2, avg=5.43, p80=7, max=10, peakCount=16, peakExcess=30, over10=0, outerRun=0, sameSideOuterRun=0, outerNearRun=0, sameSideOuterFollow=0, risk=DependencyFollowRun, reason=flow or late-campaign only
- [B] campaign500_normal_full_v1_s01_s13_13_nutation_flow_rail_v1_rect_s02_o011_v00_flowrail_rail_chain: openers=5, avg=5.08, p80=7, max=9, peakCount=4, peakExcess=6, over10=0, outerRun=1, sameSideOuterRun=1, outerNearRun=2, sameSideOuterFollow=2, risk=OuterExitHeadVisual, reason=flow or late-campaign only
- [B] nutation_longchain_candidate80_holefix_v1_03_nutation_longchain_patch_v1_rect_c80hf_01_d1_lock_patch: openers=4, avg=4.62, p80=6, max=8, peakCount=2, peakExcess=2, over10=0, outerRun=1, sameSideOuterRun=1, outerNearRun=2, sameSideOuterFollow=1, risk=OuterExitHeadVisual, reason=flow or late-campaign only
- [A] nutation_longchain_candidate80_holefix_v1_16_nutation_longchain_spine_v1_rect_c80hf_04_d1_core_spine: openers=6, avg=6.07, p80=7, max=8, peakCount=6, peakExcess=6, over10=0, outerRun=1, sameSideOuterRun=1, outerNearRun=1, sameSideOuterFollow=1, risk=DependencyFollowRun, reason=usable process curve
