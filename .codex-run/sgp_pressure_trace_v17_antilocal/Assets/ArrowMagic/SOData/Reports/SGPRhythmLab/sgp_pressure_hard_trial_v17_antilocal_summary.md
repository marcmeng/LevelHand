# SGP Rhythm Trace Summary

Generated: 2026-06-25 14:41:46

- Requested rows: 4
- Traced rows: 4
- Missing/failed rows: 0
- Sort mode: Read
- Include unselected: False
- Input CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\sgp_pressure_hard_trial_v16_trace_input.csv

## Trace Risk
- CounterfactualLocalFlow: 3
- DependencyFollowRun: 1

## Process Tier
- B: 4

## Tight Process Tier
- Drop: 4

## Choice Pressure Reality
- over10Rate avg: 0.016
- maxChoices avg: 10.25
- choicePeakCount avg: 18.25
- choicePeakExcess avg: 30.25
- meaningfulUnlockRate avg: 0.872
- outerStraightRunMax avg: 0.25
- sameSideOuterStraightRunMax avg: 0.25
- outerNearFollowRunMax avg: 2
- sameSideOuterFollowRunMax avg: 1.5
- outerNearDependencyRate avg: 0.045
- stageLockScore avg: 0.48
- lateRegionCount avg: 2
- stageGateRate avg: 0.122
- structuredHardnessV21 avg: 0.514
- boringLinearScore avg: 0.33
- choiceChangeRate avg: 0.687
- choiceRiseRate avg: 0.264
- unlockPower avg: 0.529
- postSpikeConvergence avg: 0.25
- hardStructureV3Score avg: 0.098
- causalCudP20 avg: 6.608
- causalSolveDelayAvg avg: 4.608
- causalCrossRegionCriticalLockCount avg: 11
- causalAntiLocalityScore avg: 0.228

## HardStructure V3 Class
- LocalEasy: 4

## Process Keep Samples

## High HardStructure V3 Samples
- [B/LocalEasy] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: hardV3=0.17, raw=0.611, cudP20=7.381, cudAvg=10.305, antiLocal=0.339, crossCrit=11, fanoutMax=4, solveDelay=4.982, dirRun=7, localPatchRun=4, outerExitRun=1, maxChoices=11, risk=DependencyFollowRun
- [B/LocalEasy] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: hardV3=0.078, raw=0.58, cudP20=7.267, cudAvg=10.606, antiLocal=0.233, crossCrit=14, fanoutMax=5, solveDelay=4.883, dirRun=8, localPatchRun=9, outerExitRun=2, maxChoices=12, risk=CounterfactualLocalFlow
- [B/LocalEasy] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: hardV3=0.072, raw=0.535, cudP20=6.182, cudAvg=10.002, antiLocal=0.186, crossCrit=9, fanoutMax=3, solveDelay=4.508, dirRun=8, localPatchRun=5, outerExitRun=1, maxChoices=10, risk=CounterfactualLocalFlow
- [B/LocalEasy] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: hardV3=0.071, raw=0.524, cudP20=5.6, cudAvg=10.103, antiLocal=0.154, crossCrit=10, fanoutMax=4, solveDelay=4.058, dirRun=10, localPatchRun=8, outerExitRun=2, maxChoices=8, risk=CounterfactualLocalFlow

## High Structured Hardness Samples
- [B] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: hardV21=0.542, boring=0.333, avg=5.71, max=12, changeRate=0.742, riseRate=0.274, unlockPower=0.594, postSpike=0.167, crossUnlock=0.824, risk=CounterfactualLocalFlow
- [B] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: hardV21=0.529, boring=0.294, avg=5.26, max=8, changeRate=0.625, riseRate=0.232, unlockPower=0.496, postSpike=0.25, crossUnlock=0.769, risk=CounterfactualLocalFlow
- [B] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: hardV21=0.501, boring=0.342, avg=6.64, max=11, changeRate=0.655, riseRate=0.259, unlockPower=0.527, postSpike=0.25, crossUnlock=0.733, risk=DependencyFollowRun
- [B] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: hardV21=0.482, boring=0.351, avg=6.62, max=10, changeRate=0.726, riseRate=0.29, unlockPower=0.499, postSpike=0.333, crossUnlock=0.5, risk=CounterfactualLocalFlow

## Boring Linear Risk Samples
- [B] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: boring=0.351, hardV21=0.482, avg=6.62, winStd=0.85, changeRate=0.726, flatRate=0.274, unlockPower=0.499, lowRun=2, sameRegionRun=5, nearRun=1, risk=CounterfactualLocalFlow
- [B] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: boring=0.342, hardV21=0.501, avg=6.64, winStd=0.743, changeRate=0.655, flatRate=0.345, unlockPower=0.527, lowRun=7, sameRegionRun=4, nearRun=2, risk=DependencyFollowRun
- [B] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: boring=0.333, hardV21=0.542, avg=5.71, winStd=1.028, changeRate=0.742, flatRate=0.258, unlockPower=0.594, lowRun=3, sameRegionRun=6, nearRun=2, risk=CounterfactualLocalFlow
- [B] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: boring=0.294, hardV21=0.529, avg=5.26, winStd=0.718, changeRate=0.625, flatRate=0.375, unlockPower=0.496, lowRun=2, sameRegionRun=2, nearRun=2, risk=CounterfactualLocalFlow

## Worst Choice Curve Samples
- [B] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: openers=3, avg=5.71, p80=8, max=12, peakCount=21, peakExcess=40, over10=0.048, outerRun=0, sameSideOuterRun=0, outerNearRun=2, sameSideOuterFollow=2, risk=CounterfactualLocalFlow, reason=flow or late-campaign only
- [B] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: openers=3, avg=6.64, p80=9, max=11, peakCount=28, peakExcess=51, over10=0.017, outerRun=0, sameSideOuterRun=0, outerNearRun=2, sameSideOuterFollow=1, risk=DependencyFollowRun, reason=flow or late-campaign only
- [B] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: openers=4, avg=6.62, p80=8, max=10, peakCount=22, peakExcess=28, over10=0, outerRun=0, sameSideOuterRun=0, outerNearRun=2, sameSideOuterFollow=1, risk=CounterfactualLocalFlow, reason=flow or late-campaign only
- [B] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: openers=5, avg=5.26, p80=6, max=8, peakCount=2, peakExcess=2, over10=0, outerRun=1, sameSideOuterRun=1, outerNearRun=2, sameSideOuterFollow=2, risk=CounterfactualLocalFlow, reason=flow or late-campaign only
