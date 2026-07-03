# SGP Rhythm Trace Summary

Generated: 2026-07-02 10:11:16

- Requested rows: 5
- Traced rows: 5
- Missing/failed rows: 0
- Sort mode: Read
- Include unselected: False
- Input CSV: F:\Unityproject\ArrowLevel-Hand\Exports\Campaign500_DesignPlanning_20260702\TraceV2Calibration30\campaign500_trace_v2_calibration30_trace_input_chunk1.csv

## Trace Risk
- OuterExitHeadVisual: 2
- DependencyFollowRun: 2
- VisibleOuterExitPressure: 1

## Process Tier
- A: 2
- B: 3

## Tight Process Tier
- A: 1
- B: 2
- Drop: 2

## Choice Pressure Reality
- over10Rate avg: 0
- maxChoices avg: 6.8
- choicePeakCount avg: 0.8
- choicePeakExcess avg: 0.8
- meaningfulUnlockRate avg: 0.906
- outerStraightRunMax avg: 0.4
- sameSideOuterStraightRunMax avg: 0.4
- outerNearFollowRunMax avg: 1.6
- sameSideOuterFollowRunMax avg: 1
- outerNearDependencyRate avg: 0.067
- stageLockScore avg: 0.569
- lateRegionCount avg: 1.8
- stageGateRate avg: 0.349
- structuredHardnessV21 avg: 0.557
- boringLinearScore avg: 0.328
- choiceChokeMomentScoreV1 avg: 0.626
- choiceChokeSwitchScoreV1 avg: 0.582
- choiceChokeHardBreakScoreV1 avg: 0.218
- choiceChokeCompositeBreakScoreV1 avg: 0.428
- choiceChokeCompositeBreakWindowCount avg: 0.6
- choiceChokeSweepContinuationRiskScore avg: 0.143
- choiceChokeWindowMaxLen avg: 3.6
- choiceChokeMidLateStepCount avg: 3.6
- choiceChokeAfterLocalSwitchCount avg: 1.6
- choiceChokeAfterLocalFrontierBreakCount avg: 0
- frontierDrainRemoteChokeCount avg: 0.6
- frontierDrainRemoteChokeScoreV1 avg: 0.303
- frontierDrainChokeRate avg: 0.615
- choiceChangeRate avg: 0.676
- choiceRiseRate avg: 0.182
- unlockPower avg: 0.342
- postSpikeConvergence avg: 0.267
- hardStructureV3Score avg: 0.115
- causalCudP20 avg: 6.667
- causalSolveDelayAvg avg: 2.262
- causalCrossRegionCriticalLockCount avg: 4
- causalAntiLocalityScore avg: 0.226

## Solve Trace Sketch
- solveTraceQualityScore avg: 0.846
- solveTraceCollapseRiskScore avg: 0.168
- solveRegionEntropy avg: 0.967
- solveRegionCollapseRunMax avg: 2.6
- solveSameAxisRunMax avg: 7.2
- solveSameDirHeadRunMax avg: 5
- solveFrontWidthAvg avg: 3.1
- solveFrontWidthNarrowRate avg: 0.349
- dependencyRegionEntropy avg: 0.978
- dependencyLocalSameRegionRate avg: 0.299

## HardStructure V3 Class
- LocalEasy: 4
- MediumStructure: 1

## Process Keep Samples
- [A] tutorial_simple_01_hi: openers=4, avg=2.33, p80=3, max=4, peakCount=0, peakExcess=0, over10=0, meaningful=1, stageScore=0.632, hardV21=0.447, boring=0.469, unlockPower=0.18, postSpike=0, outerNearRun=2, sameSideOuterRun=0
- [A] campaign500_longchain_prod200_pool_v1_02_l003_long_normal_a_normal_s01_slot_seedweave_braid_carrier: openers=7, avg=4.07, p80=7, max=8, peakCount=1, peakExcess=1, over10=0, meaningful=0.875, stageScore=0.633, hardV21=0.509, boring=0.269, unlockPower=0.267, postSpike=0, outerNearRun=1, sameSideOuterRun=1

## High HardStructure V3 Samples
- [A/MediumStructure] tutorial_simple_01_hi: hardV3=0.229, raw=0.407, cudP20=7, cudAvg=8.75, antiLocal=0.5, crossCrit=0, fanoutMax=1, solveDelay=0.5, dirRun=2, localPatchRun=3, outerExitRun=2, maxChoices=4, risk=VisibleOuterExitPressure
- [A/LocalEasy] campaign500_longchain_prod200_pool_v1_02_l003_long_normal_a_normal_s01_slot_seedweave_braid_carrier: hardV3=0.115, raw=0.468, cudP20=7.536, cudAvg=10.659, antiLocal=0.125, crossCrit=1, fanoutMax=2, solveDelay=2, dirRun=4, localPatchRun=3, outerExitRun=3, maxChoices=8, risk=OuterExitHeadVisual
- [B/LocalEasy] seed_Arrowz_level_035_holeblock_multiseed_final: hardV3=0.083, raw=0.435, cudP20=5.833, cudAvg=10.15, antiLocal=0.125, crossCrit=2, fanoutMax=3, solveDelay=1.562, dirRun=2, localPatchRun=4, outerExitRun=3, maxChoices=6, risk=OuterExitHeadVisual
- [B/LocalEasy] campaign500_normal_full_v1_s01_s13_123_nutation_flow_rail_v1_rect_s11_o102_v02_flowrail_rail_chain: hardV3=0.079, raw=0.587, cudP20=7.867, cudAvg=11.468, antiLocal=0.194, crossCrit=8, fanoutMax=3, solveDelay=3.839, dirRun=6, localPatchRun=6, outerExitRun=4, maxChoices=8, risk=DependencyFollowRun
- [B/LocalEasy] campaign500_shape_psg_newbie4_01_L004_crystalball: hardV3=0.068, raw=0.506, cudP20=5.1, cudAvg=8.906, antiLocal=0.185, crossCrit=9, fanoutMax=3, solveDelay=3.407, dirRun=11, localPatchRun=8, outerExitRun=0, maxChoices=8, risk=DependencyFollowRun

## High Choke Moment Samples
- [B] seed_Arrowz_level_035_holeblock_multiseed_final: choke=0.905, switch=0.75, hardBreak=0.174, composite=0.2/0, sweepRisk=0, drainRemote=1/0.46, low2=0.182, windows=2, switchSteps=2, afterLocalSwitch=1, afterLocalFrontierBreak=0, continuity=1, midLateSteps=4, best=21-22:2 1, switchBest=21-22:2 1|switch=21|afterLocal=21|frontierBreak=|newRegion=|dirBreak=21, compositeBest=, frontierChoke=0.773, frontierExplode=0, avg/max=3.73/6
- [B] campaign500_shape_psg_newbie4_01_L004_crystalball: choke=0.895, switch=0.25, hardBreak=0.296, composite=0.665/1, sweepRisk=0.128, drainRemote=0/0, low2=0.127, windows=3, switchSteps=4, afterLocalSwitch=2, afterLocalFrontierBreak=0, continuity=2, midLateSteps=2, best=7-10:2 2 2 2, switchBest=7-10:2 2 2 2|switch=7,9|afterLocal=7,9|frontierBreak=|newRegion=9|dirBreak=9, compositeBest=7-10:2 2 2 2|switch=7,9|afterLocal=7,9|frontierBreak=|newRegion=9|dirBreak=9, frontierChoke=0.455, frontierExplode=0, avg/max=4.35/8
- [B] campaign500_normal_full_v1_s01_s13_123_nutation_flow_rail_v1_rect_s11_o102_v02_flowrail_rail_chain: choke=0.718, switch=0.495, hardBreak=0.054, composite=0.157/0, sweepRisk=0.024, drainRemote=1/0.475, low2=0.079, windows=2, switchSteps=3, afterLocalSwitch=0, afterLocalFrontierBreak=0, continuity=2, midLateSteps=3, best=37-38:2 1, switchBest=37-38:2 1|switch=37,38|afterLocal=|frontierBreak=|newRegion=|dirBreak=37, compositeBest=, frontierChoke=0.316, frontierExplode=0, avg/max=4.79/8
- [A] campaign500_longchain_prod200_pool_v1_02_l003_long_normal_a_normal_s01_slot_seedweave_braid_carrier: choke=0.614, switch=0.547, hardBreak=0.258, composite=0.512/1, sweepRisk=0.319, drainRemote=1/0.578, low2=0.4, windows=1, switchSteps=6, afterLocalSwitch=2, afterLocalFrontierBreak=0, continuity=4, midLateSteps=6, best=10-15:2 1 2 2 2 1, switchBest=10-15:2 1 2 2 2 1|switch=10,11,12,13,14,15|afterLocal=10,15|frontierBreak=12|newRegion=12,14|dirBreak=10,11,12,13,14, compositeBest=10-15:2 1 2 2 2 1|switch=10,11,12,13,14,15|afterLocal=10,15|frontierBreak=12|newRegion=12,14|dirBreak=10,11,12,13,14, frontierChoke=0.533, frontierExplode=0, avg/max=4.07/8
- [A] tutorial_simple_01_hi: choke=0, switch=0.869, hardBreak=0.31, composite=0.604/1, sweepRisk=0.246, drainRemote=0/0, low2=0.667, windows=1, switchSteps=4, afterLocalSwitch=3, afterLocalFrontierBreak=0, continuity=4, midLateSteps=3, best=3-6:2 2 2 1, switchBest=3-6:2 2 2 1|switch=3,4,5,6|afterLocal=4,5,6|frontierBreak=|newRegion=4|dirBreak=4,6, compositeBest=3-6:2 2 2 1|switch=3,4,5,6|afterLocal=4,5,6|frontierBreak=|newRegion=4|dirBreak=4,6, frontierChoke=1, frontierExplode=0, avg/max=2.33/4

## High Choke Switch Samples
- [A] tutorial_simple_01_hi: switch=0.869, hardBreak=0.31, composite=0.604/1, sweepRisk=0.246, drainRemote=0/0, steps=4, afterLocal=3, frontierBreak=0, afterLocalFrontierBreak=0, newRegion=1, dirBreak=2, continuity=4, low2SwitchRate=1, avgJump=3.875, regionSwitch=1, best=3-6:2 2 2 1|switch=3,4,5,6|afterLocal=4,5,6|frontierBreak=|newRegion=4|dirBreak=4,6, compositeBest=3-6:2 2 2 1|switch=3,4,5,6|afterLocal=4,5,6|frontierBreak=|newRegion=4|dirBreak=4,6, localPatchRun=3, avg/max=2.33/4
- [B] seed_Arrowz_level_035_holeblock_multiseed_final: switch=0.75, hardBreak=0.174, composite=0.2/0, sweepRisk=0, drainRemote=1/0.46, steps=2, afterLocal=1, frontierBreak=0, afterLocalFrontierBreak=0, newRegion=0, dirBreak=2, continuity=1, low2SwitchRate=0.5, avgJump=15.917, regionSwitch=1, best=21-22:2 1|switch=21|afterLocal=21|frontierBreak=|newRegion=|dirBreak=21, compositeBest=, localPatchRun=4, avg/max=3.73/6
- [A] campaign500_longchain_prod200_pool_v1_02_l003_long_normal_a_normal_s01_slot_seedweave_braid_carrier: switch=0.547, hardBreak=0.258, composite=0.512/1, sweepRisk=0.319, drainRemote=1/0.578, steps=6, afterLocal=2, frontierBreak=1, afterLocalFrontierBreak=0, newRegion=2, dirBreak=5, continuity=4, low2SwitchRate=1, avgJump=8.129, regionSwitch=1, best=10-15:2 1 2 2 2 1|switch=10,11,12,13,14,15|afterLocal=10,15|frontierBreak=12|newRegion=12,14|dirBreak=10,11,12,13,14, compositeBest=10-15:2 1 2 2 2 1|switch=10,11,12,13,14,15|afterLocal=10,15|frontierBreak=12|newRegion=12,14|dirBreak=10,11,12,13,14, localPatchRun=3, avg/max=4.07/8
- [B] campaign500_normal_full_v1_s01_s13_123_nutation_flow_rail_v1_rect_s11_o102_v02_flowrail_rail_chain: switch=0.495, hardBreak=0.054, composite=0.157/0, sweepRisk=0.024, drainRemote=1/0.475, steps=3, afterLocal=0, frontierBreak=0, afterLocalFrontierBreak=0, newRegion=0, dirBreak=1, continuity=2, low2SwitchRate=1, avgJump=11.5, regionSwitch=0.667, best=37-38:2 1|switch=37,38|afterLocal=|frontierBreak=|newRegion=|dirBreak=37, compositeBest=, localPatchRun=6, avg/max=4.79/8
- [B] campaign500_shape_psg_newbie4_01_L004_crystalball: switch=0.25, hardBreak=0.296, composite=0.665/1, sweepRisk=0.128, drainRemote=0/0, steps=4, afterLocal=2, frontierBreak=0, afterLocalFrontierBreak=0, newRegion=1, dirBreak=3, continuity=2, low2SwitchRate=0.571, avgJump=13.2, regionSwitch=1, best=7-10:2 2 2 2|switch=7,9|afterLocal=7,9|frontierBreak=|newRegion=9|dirBreak=9, compositeBest=7-10:2 2 2 2|switch=7,9|afterLocal=7,9|frontierBreak=|newRegion=9|dirBreak=9, localPatchRun=8, avg/max=4.35/8

## High Structured Hardness Samples
- [B] seed_Arrowz_level_035_holeblock_multiseed_final: hardV21=0.725, boring=0.243, avg=3.73, max=6, changeRate=0.762, riseRate=0.238, unlockPower=0.389, postSpike=1, crossUnlock=0.4, risk=OuterExitHeadVisual
- [B] campaign500_shape_psg_newbie4_01_L004_crystalball: hardV21=0.57, boring=0.375, avg=4.35, max=8, changeRate=0.574, riseRate=0.259, unlockPower=0.477, postSpike=0.333, crossUnlock=0.643, risk=DependencyFollowRun
- [B] campaign500_normal_full_v1_s01_s13_123_nutation_flow_rail_v1_rect_s11_o102_v02_flowrail_rail_chain: hardV21=0.533, boring=0.283, avg=4.79, max=8, changeRate=0.73, riseRate=0.27, unlockPower=0.399, postSpike=0, crossUnlock=0.8, risk=DependencyFollowRun
- [A] campaign500_longchain_prod200_pool_v1_02_l003_long_normal_a_normal_s01_slot_seedweave_braid_carrier: hardV21=0.509, boring=0.269, avg=4.07, max=8, changeRate=0.714, riseRate=0.143, unlockPower=0.267, postSpike=0, crossUnlock=0.5, risk=OuterExitHeadVisual
- [A] tutorial_simple_01_hi: hardV21=0.447, boring=0.469, avg=2.33, max=4, changeRate=0.6, riseRate=0, unlockPower=0.18, postSpike=0, crossUnlock=0, risk=VisibleOuterExitPressure

## Boring Linear Risk Samples
- [A] tutorial_simple_01_hi: boring=0.469, hardV21=0.447, avg=2.33, winStd=0.716, changeRate=0.6, flatRate=0.4, unlockPower=0.18, lowRun=4, sameRegionRun=0, nearRun=2, risk=VisibleOuterExitPressure
- [B] campaign500_shape_psg_newbie4_01_L004_crystalball: boring=0.375, hardV21=0.57, avg=4.35, winStd=0.728, changeRate=0.574, flatRate=0.426, unlockPower=0.477, lowRun=4, sameRegionRun=4, nearRun=3, risk=DependencyFollowRun
- [B] campaign500_normal_full_v1_s01_s13_123_nutation_flow_rail_v1_rect_s11_o102_v02_flowrail_rail_chain: boring=0.283, hardV21=0.533, avg=4.79, winStd=0.8, changeRate=0.73, flatRate=0.27, unlockPower=0.399, lowRun=2, sameRegionRun=2, nearRun=1, risk=DependencyFollowRun
- [A] campaign500_longchain_prod200_pool_v1_02_l003_long_normal_a_normal_s01_slot_seedweave_braid_carrier: boring=0.269, hardV21=0.509, avg=4.07, winStd=0.885, changeRate=0.714, flatRate=0.286, unlockPower=0.267, lowRun=6, sameRegionRun=1, nearRun=1, risk=OuterExitHeadVisual
- [B] seed_Arrowz_level_035_holeblock_multiseed_final: boring=0.243, hardV21=0.725, avg=3.73, winStd=0.859, changeRate=0.762, flatRate=0.238, unlockPower=0.389, lowRun=2, sameRegionRun=1, nearRun=1, risk=OuterExitHeadVisual

## Worst Choice Curve Samples
- [A] campaign500_longchain_prod200_pool_v1_02_l003_long_normal_a_normal_s01_slot_seedweave_braid_carrier: openers=7, avg=4.07, p80=7, max=8, peakCount=1, peakExcess=1, over10=0, outerRun=0, sameSideOuterRun=0, outerNearRun=1, sameSideOuterFollow=1, risk=OuterExitHeadVisual, reason=usable process curve
- [B] campaign500_normal_full_v1_s01_s13_123_nutation_flow_rail_v1_rect_s11_o102_v02_flowrail_rail_chain: openers=7, avg=4.79, p80=6, max=8, peakCount=2, peakExcess=2, over10=0, outerRun=1, sameSideOuterRun=1, outerNearRun=2, sameSideOuterFollow=1, risk=DependencyFollowRun, reason=flow or late-campaign only
- [B] campaign500_shape_psg_newbie4_01_L004_crystalball: openers=1, avg=4.35, p80=5, max=8, peakCount=1, peakExcess=1, over10=0, outerRun=0, sameSideOuterRun=0, outerNearRun=2, sameSideOuterFollow=2, risk=DependencyFollowRun, reason=flow or late-campaign only
- [B] seed_Arrowz_level_035_holeblock_multiseed_final: openers=6, avg=3.73, p80=5, max=6, peakCount=0, peakExcess=0, over10=0, outerRun=1, sameSideOuterRun=1, outerNearRun=1, sameSideOuterFollow=1, risk=OuterExitHeadVisual, reason=flow or late-campaign only
- [A] tutorial_simple_01_hi: openers=4, avg=2.33, p80=3, max=4, peakCount=0, peakExcess=0, over10=0, outerRun=0, sameSideOuterRun=0, outerNearRun=2, sameSideOuterFollow=0, risk=VisibleOuterExitPressure, reason=usable process curve
