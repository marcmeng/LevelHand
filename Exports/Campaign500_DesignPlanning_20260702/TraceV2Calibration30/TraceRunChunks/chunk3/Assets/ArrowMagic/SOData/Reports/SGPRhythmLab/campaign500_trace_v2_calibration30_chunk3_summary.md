# SGP Rhythm Trace Summary

Generated: 2026-07-02 10:17:35

- Requested rows: 5
- Traced rows: 5
- Missing/failed rows: 0
- Sort mode: Read
- Include unselected: False
- Input CSV: F:\Unityproject\ArrowLevel-Hand\Exports\Campaign500_DesignPlanning_20260702\TraceV2Calibration30\campaign500_trace_v2_calibration30_trace_input_chunk3.csv

## Trace Risk
- DependencyFollowRun: 4
- OuterExitHeadVisual: 1

## Process Tier
- B: 5

## Tight Process Tier
- B: 1
- Drop: 4

## Choice Pressure Reality
- over10Rate avg: 0.009
- maxChoices avg: 9.8
- choicePeakCount avg: 19.4
- choicePeakExcess avg: 35.2
- meaningfulUnlockRate avg: 0.832
- outerStraightRunMax avg: 0.2
- sameSideOuterStraightRunMax avg: 0.2
- outerNearFollowRunMax avg: 1.2
- sameSideOuterFollowRunMax avg: 0.8
- outerNearDependencyRate avg: 0.044
- stageLockScore avg: 0.485
- lateRegionCount avg: 1.6
- stageGateRate avg: 0.198
- structuredHardnessV21 avg: 0.518
- boringLinearScore avg: 0.34
- choiceChokeMomentScoreV1 avg: 0.762
- choiceChokeSwitchScoreV1 avg: 0.368
- choiceChokeHardBreakScoreV1 avg: 0.225
- choiceChokeCompositeBreakScoreV1 avg: 0.343
- choiceChokeCompositeBreakWindowCount avg: 0.8
- choiceChokeSweepContinuationRiskScore avg: 0.201
- choiceChokeWindowMaxLen avg: 4
- choiceChokeMidLateStepCount avg: 8.2
- choiceChokeAfterLocalSwitchCount avg: 2.8
- choiceChokeAfterLocalFrontierBreakCount avg: 0.2
- frontierDrainRemoteChokeCount avg: 0.4
- frontierDrainRemoteChokeScoreV1 avg: 0.168
- frontierDrainChokeRate avg: 0.332
- choiceChangeRate avg: 0.678
- choiceRiseRate avg: 0.265
- unlockPower avg: 0.53
- postSpikeConvergence avg: 0.313
- hardStructureV3Score avg: 0.073
- causalCudP20 avg: 6.537
- causalSolveDelayAvg avg: 4.236
- causalCrossRegionCriticalLockCount avg: 11.2
- causalAntiLocalityScore avg: 0.185

## Solve Trace Sketch
- solveTraceQualityScore avg: 0.776
- solveTraceCollapseRiskScore avg: 0.305
- solveRegionEntropy avg: 0.977
- solveRegionCollapseRunMax avg: 5.4
- solveSameAxisRunMax avg: 9.2
- solveSameDirHeadRunMax avg: 8.6
- solveFrontWidthAvg avg: 3.75
- solveFrontWidthNarrowRate avg: 0.225
- dependencyRegionEntropy avg: 0.975
- dependencyLocalSameRegionRate avg: 0.564

## HardStructure V3 Class
- LocalEasy: 5

## Process Keep Samples

## High HardStructure V3 Samples
- [B/LocalEasy] campaign500_normal_early_plus_v1_topup_fastlane1_40_nutation_flow_rail_v1_rect_s32_o311_v00_flowrail_rail_chain: hardV3=0.081, raw=0.601, cudP20=7.69, cudAvg=12.712, antiLocal=0.186, crossCrit=9, fanoutMax=6, solveDelay=4.86, dirRun=7, localPatchRun=5, outerExitRun=6, maxChoices=10, risk=OuterExitHeadVisual
- [B/LocalEasy] nutation_hub_mixed_v1_strict30_transform_wide_08_s02_rot180: hardV3=0.074, raw=0.551, cudP20=7.1, cudAvg=10.19, antiLocal=0.191, crossCrit=11, fanoutMax=4, solveDelay=3.73, dirRun=7, localPatchRun=6, outerExitRun=2, maxChoices=10, risk=DependencyFollowRun
- [B/LocalEasy] campaign500_normal_full_v1_s39_s50_tail_06_nutation_peel_v2_rect_s41_o407_v02_peelhard_curve_chain: hardV3=0.073, raw=0.54, cudP20=6.727, cudAvg=10.105, antiLocal=0.238, crossCrit=13, fanoutMax=4, solveDelay=5.667, dirRun=6, localPatchRun=6, outerExitRun=2, maxChoices=11, risk=DependencyFollowRun
- [B/LocalEasy] campaign500_shape_psg_review_top3_rescue_01_L038_suspensionbridge_section: hardV3=0.072, raw=0.531, cudP20=6, cudAvg=10.117, antiLocal=0.198, crossCrit=13, fanoutMax=4, solveDelay=4.116, dirRun=6, localPatchRun=8, outerExitRun=0, maxChoices=10, risk=DependencyFollowRun
- [B/LocalEasy] campaign500_shape_psg_next10_tail9_02_L083_ballmouse: hardV3=0.063, raw=0.469, cudP20=5.167, cudAvg=8.912, antiLocal=0.112, crossCrit=10, fanoutMax=3, solveDelay=2.809, dirRun=17, localPatchRun=9, outerExitRun=0, maxChoices=8, risk=DependencyFollowRun

## High Choke Moment Samples
- [B] campaign500_shape_psg_next10_tail9_02_L083_ballmouse: choke=1, switch=0, hardBreak=0.508, composite=0.685/3, sweepRisk=0.45, drainRemote=0/0, low2=0.253, windows=7, switchSteps=15, afterLocalSwitch=9, afterLocalFrontierBreak=1, continuity=3, midLateSteps=20, best=65-72:2 2 1 1 1 2 1 2, switchBest=65-72:2 2 1 1 1 2 1 2|switch=65,68,70,71,72|afterLocal=68,70,71,72|frontierBreak=68|newRegion=68,70,72|dirBreak=65,68,70,71,72, compositeBest=65-72:2 2 1 1 1 2 1 2|switch=65,68,70,71,72|afterLocal=68,70,71,72|frontierBreak=68|newRegion=68,70,72|dirBreak=65,68,70,71,72, frontierChoke=0.429, frontierExplode=0, avg/max=3.76/8
- [B] campaign500_shape_psg_review_top3_rescue_01_L038_suspensionbridge_section: choke=0.963, switch=0.25, hardBreak=0.209, composite=0.266/0, sweepRisk=0.072, drainRemote=0/0, low2=0.114, windows=5, switchSteps=6, afterLocalSwitch=2, afterLocalFrontierBreak=0, continuity=2, midLateSteps=8, best=39-41:2 1 1, switchBest=39-41:2 1 1|switch=40|afterLocal=40|frontierBreak=|newRegion=|dirBreak=40, compositeBest=, frontierChoke=0.568, frontierExplode=0, avg/max=5.45/10
- [B] nutation_hub_mixed_v1_strict30_transform_wide_08_s02_rot180: choke=0.869, switch=0.6, hardBreak=0.195, composite=0.449/1, sweepRisk=0.458, drainRemote=2/0.839, low2=0.096, windows=3, switchSteps=8, afterLocalSwitch=2, afterLocalFrontierBreak=0, continuity=5, midLateSteps=9, best=50-54:2 2 2 2 1, switchBest=50-54:2 2 2 2 1|switch=50,51,52,53,54|afterLocal=53,54|frontierBreak=|newRegion=52|dirBreak=50,51,52,53,54, compositeBest=50-54:2 2 2 2 1|switch=50,51,52,53,54|afterLocal=53,54|frontierBreak=|newRegion=52|dirBreak=50,51,52,53,54, frontierChoke=0.202, frontierExplode=0.011, avg/max=5.99/10
- [B] campaign500_normal_early_plus_v1_topup_fastlane1_40_nutation_flow_rail_v1_rect_s32_o311_v00_flowrail_rail_chain: choke=0.494, switch=0.43, hardBreak=0.04, composite=0.04/0, sweepRisk=0, drainRemote=0/0, low2=0.043, windows=1, switchSteps=1, afterLocalSwitch=0, afterLocalFrontierBreak=0, continuity=1, midLateSteps=2, best=46-47:2 1, switchBest=46-47:2 1|switch=46|afterLocal=|frontierBreak=|newRegion=|dirBreak=46, compositeBest=, frontierChoke=0.213, frontierExplode=0.064, avg/max=6.13/10
- [B] campaign500_normal_full_v1_s39_s50_tail_06_nutation_peel_v2_rect_s41_o407_v02_peelhard_curve_chain: choke=0.484, switch=0.562, hardBreak=0.174, composite=0.275/0, sweepRisk=0.024, drainRemote=0/0, low2=0.022, windows=1, switchSteps=2, afterLocalSwitch=1, afterLocalFrontierBreak=0, continuity=2, midLateSteps=2, best=91-92:2 1, switchBest=91-92:2 1|switch=91,92|afterLocal=91|frontierBreak=|newRegion=|dirBreak=91,92, compositeBest=, frontierChoke=0.25, frontierExplode=0.391, avg/max=6.65/11

## High Choke Switch Samples
- [B] nutation_hub_mixed_v1_strict30_transform_wide_08_s02_rot180: switch=0.6, hardBreak=0.195, composite=0.449/1, sweepRisk=0.458, drainRemote=2/0.839, steps=8, afterLocal=2, frontierBreak=0, afterLocalFrontierBreak=0, newRegion=2, dirBreak=7, continuity=5, low2SwitchRate=0.889, avgJump=12.465, regionSwitch=0.875, best=50-54:2 2 2 2 1|switch=50,51,52,53,54|afterLocal=53,54|frontierBreak=|newRegion=52|dirBreak=50,51,52,53,54, compositeBest=50-54:2 2 2 2 1|switch=50,51,52,53,54|afterLocal=53,54|frontierBreak=|newRegion=52|dirBreak=50,51,52,53,54, localPatchRun=6, avg/max=5.99/10
- [B] campaign500_normal_full_v1_s39_s50_tail_06_nutation_peel_v2_rect_s41_o407_v02_peelhard_curve_chain: switch=0.562, hardBreak=0.174, composite=0.275/0, sweepRisk=0.024, drainRemote=0/0, steps=2, afterLocal=1, frontierBreak=0, afterLocalFrontierBreak=0, newRegion=0, dirBreak=2, continuity=2, low2SwitchRate=1, avgJump=17.5, regionSwitch=1, best=91-92:2 1|switch=91,92|afterLocal=91|frontierBreak=|newRegion=|dirBreak=91,92, compositeBest=, localPatchRun=6, avg/max=6.65/11
- [B] campaign500_normal_early_plus_v1_topup_fastlane1_40_nutation_flow_rail_v1_rect_s32_o311_v00_flowrail_rail_chain: switch=0.43, hardBreak=0.04, composite=0.04/0, sweepRisk=0, drainRemote=0/0, steps=1, afterLocal=0, frontierBreak=0, afterLocalFrontierBreak=0, newRegion=0, dirBreak=1, continuity=1, low2SwitchRate=0.5, avgJump=16.5, regionSwitch=1, best=46-47:2 1|switch=46|afterLocal=|frontierBreak=|newRegion=|dirBreak=46, compositeBest=, localPatchRun=5, avg/max=6.13/10
- [B] campaign500_shape_psg_review_top3_rescue_01_L038_suspensionbridge_section: switch=0.25, hardBreak=0.209, composite=0.266/0, sweepRisk=0.072, drainRemote=0/0, steps=6, afterLocal=2, frontierBreak=0, afterLocalFrontierBreak=0, newRegion=0, dirBreak=5, continuity=2, low2SwitchRate=0.6, avgJump=15.811, regionSwitch=0.833, best=39-41:2 1 1|switch=40|afterLocal=40|frontierBreak=|newRegion=|dirBreak=40, compositeBest=, localPatchRun=8, avg/max=5.45/10
- [B] campaign500_shape_psg_next10_tail9_02_L083_ballmouse: switch=0, hardBreak=0.508, composite=0.685/3, sweepRisk=0.45, drainRemote=0/0, steps=15, afterLocal=9, frontierBreak=2, afterLocalFrontierBreak=1, newRegion=5, dirBreak=15, continuity=3, low2SwitchRate=0.652, avgJump=16.249, regionSwitch=0.933, best=65-72:2 2 1 1 1 2 1 2|switch=65,68,70,71,72|afterLocal=68,70,71,72|frontierBreak=68|newRegion=68,70,72|dirBreak=65,68,70,71,72, compositeBest=65-72:2 2 1 1 1 2 1 2|switch=65,68,70,71,72|afterLocal=68,70,71,72|frontierBreak=68|newRegion=68,70,72|dirBreak=65,68,70,71,72, localPatchRun=9, avg/max=3.76/8

## High Structured Hardness Samples
- [B] campaign500_normal_early_plus_v1_topup_fastlane1_40_nutation_flow_rail_v1_rect_s32_o311_v00_flowrail_rail_chain: hardV21=0.598, boring=0.216, avg=6.13, max=10, changeRate=0.804, riseRate=0.304, unlockPower=0.586, postSpike=0.667, crossUnlock=0.643, risk=OuterExitHeadVisual
- [B] campaign500_shape_psg_next10_tail9_02_L083_ballmouse: hardV21=0.547, boring=0.427, avg=3.76, max=8, changeRate=0.611, riseRate=0.267, unlockPower=0.486, postSpike=0.167, crossUnlock=0.417, risk=DependencyFollowRun
- [B] campaign500_shape_psg_review_top3_rescue_01_L038_suspensionbridge_section: hardV21=0.515, boring=0.376, avg=5.45, max=10, changeRate=0.667, riseRate=0.264, unlockPower=0.541, postSpike=0.3, crossUnlock=0.565, risk=DependencyFollowRun
- [B] campaign500_normal_full_v1_s39_s50_tail_06_nutation_peel_v2_rect_s41_o407_v02_peelhard_curve_chain: hardV21=0.482, boring=0.313, avg=6.65, max=11, changeRate=0.769, riseRate=0.297, unlockPower=0.507, postSpike=0.286, crossUnlock=0.481, risk=DependencyFollowRun
- [B] nutation_hub_mixed_v1_strict30_transform_wide_08_s02_rot180: hardV21=0.446, boring=0.369, avg=5.99, max=10, changeRate=0.538, riseRate=0.194, unlockPower=0.529, postSpike=0.143, crossUnlock=0.611, risk=DependencyFollowRun

## Boring Linear Risk Samples
- [B] campaign500_shape_psg_next10_tail9_02_L083_ballmouse: boring=0.427, hardV21=0.547, avg=3.76, winStd=0.753, changeRate=0.611, flatRate=0.389, unlockPower=0.486, lowRun=8, sameRegionRun=6, nearRun=5, risk=DependencyFollowRun
- [B] campaign500_shape_psg_review_top3_rescue_01_L038_suspensionbridge_section: boring=0.376, hardV21=0.515, avg=5.45, winStd=0.913, changeRate=0.667, flatRate=0.333, unlockPower=0.541, lowRun=3, sameRegionRun=6, nearRun=5, risk=DependencyFollowRun
- [B] nutation_hub_mixed_v1_strict30_transform_wide_08_s02_rot180: boring=0.369, hardV21=0.446, avg=5.99, winStd=0.738, changeRate=0.538, flatRate=0.462, unlockPower=0.529, lowRun=5, sameRegionRun=4, nearRun=2, risk=DependencyFollowRun
- [B] campaign500_normal_full_v1_s39_s50_tail_06_nutation_peel_v2_rect_s41_o407_v02_peelhard_curve_chain: boring=0.313, hardV21=0.482, avg=6.65, winStd=0.867, changeRate=0.769, flatRate=0.231, unlockPower=0.507, lowRun=2, sameRegionRun=4, nearRun=1, risk=DependencyFollowRun
- [B] campaign500_normal_early_plus_v1_topup_fastlane1_40_nutation_flow_rail_v1_rect_s32_o311_v00_flowrail_rail_chain: boring=0.216, hardV21=0.598, avg=6.13, winStd=0.941, changeRate=0.804, flatRate=0.196, unlockPower=0.586, lowRun=2, sameRegionRun=2, nearRun=2, risk=OuterExitHeadVisual

## Worst Choice Curve Samples
- [B] campaign500_normal_full_v1_s39_s50_tail_06_nutation_peel_v2_rect_s41_o407_v02_peelhard_curve_chain: openers=8, avg=6.65, p80=9, max=11, peakCount=39, peakExcess=70, over10=0.043, outerRun=0, sameSideOuterRun=0, outerNearRun=1, sameSideOuterFollow=1, risk=DependencyFollowRun, reason=flow or late-campaign only
- [B] campaign500_normal_early_plus_v1_topup_fastlane1_40_nutation_flow_rail_v1_rect_s32_o311_v00_flowrail_rail_chain: openers=4, avg=6.13, p80=8, max=10, peakCount=15, peakExcess=25, over10=0, outerRun=1, sameSideOuterRun=1, outerNearRun=2, sameSideOuterFollow=0, risk=OuterExitHeadVisual, reason=flow or late-campaign only
- [B] campaign500_shape_psg_review_top3_rescue_01_L038_suspensionbridge_section: openers=2, avg=5.45, p80=7, max=10, peakCount=15, peakExcess=26, over10=0, outerRun=0, sameSideOuterRun=0, outerNearRun=0, sameSideOuterFollow=0, risk=DependencyFollowRun, reason=flow or late-campaign only
- [B] nutation_hub_mixed_v1_strict30_transform_wide_08_s02_rot180: openers=5, avg=5.99, p80=8, max=10, peakCount=27, peakExcess=54, over10=0, outerRun=0, sameSideOuterRun=0, outerNearRun=2, sameSideOuterFollow=2, risk=DependencyFollowRun, reason=flow or late-campaign only
- [B] campaign500_shape_psg_next10_tail9_02_L083_ballmouse: openers=2, avg=3.76, p80=5, max=8, peakCount=1, peakExcess=1, over10=0, outerRun=0, sameSideOuterRun=0, outerNearRun=1, sameSideOuterFollow=1, risk=DependencyFollowRun, reason=flow or late-campaign only
