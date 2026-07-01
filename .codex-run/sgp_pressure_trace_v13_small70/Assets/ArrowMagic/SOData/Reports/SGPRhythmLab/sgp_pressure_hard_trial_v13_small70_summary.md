# SGP Rhythm Trace Summary

Generated: 2026-06-25 14:18:50

- Requested rows: 4
- Traced rows: 4
- Missing/failed rows: 0
- Sort mode: Read
- Include unselected: False
- Input CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\sgp_pressure_hard_trial_v13_trace_input.csv

## Trace Risk
- CounterfactualLocalFlow: 4

## Process Tier
- B: 1
- Drop: 3

## Tight Process Tier
- Drop: 4

## Choice Pressure Reality
- over10Rate avg: 0.284
- maxChoices avg: 14.5
- choicePeakCount avg: 46.5
- choicePeakExcess avg: 175.75
- meaningfulUnlockRate avg: 0.851
- outerStraightRunMax avg: 0
- sameSideOuterStraightRunMax avg: 0
- outerNearFollowRunMax avg: 1.5
- sameSideOuterFollowRunMax avg: 1.25
- outerNearDependencyRate avg: 0.045
- stageLockScore avg: 0.403
- lateRegionCount avg: 1.5
- stageGateRate avg: 0.054
- structuredHardnessV21 avg: 0.461
- boringLinearScore avg: 0.321
- choiceChangeRate avg: 0.668
- choiceRiseRate avg: 0.252
- unlockPower avg: 0.516
- postSpikeConvergence avg: 0.267
- hardStructureV3Score avg: 0.072
- causalCudP20 avg: 6.24
- causalSolveDelayAvg avg: 6.996
- causalCrossRegionCriticalLockCount avg: 8.5
- causalAntiLocalityScore avg: 0.179

## HardStructure V3 Class
- LocalEasy: 4

## Process Keep Samples

## High HardStructure V3 Samples
- [Drop/LocalEasy] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: hardV3=0.075, raw=0.553, cudP20=6.455, cudAvg=10.549, antiLocal=0.246, crossCrit=13, fanoutMax=5, solveDelay=7.986, dirRun=6, localPatchRun=5, outerExitRun=1, maxChoices=19, risk=CounterfactualLocalFlow
- [Drop/LocalEasy] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: hardV3=0.072, raw=0.537, cudP20=6.571, cudAvg=9.203, antiLocal=0.127, crossCrit=5, fanoutMax=4, solveDelay=7.887, dirRun=6, localPatchRun=10, outerExitRun=1, maxChoices=15, risk=CounterfactualLocalFlow
- [B/LocalEasy] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: hardV3=0.07, raw=0.522, cudP20=5.867, cudAvg=9.038, antiLocal=0.197, crossCrit=10, fanoutMax=3, solveDelay=5.197, dirRun=6, localPatchRun=5, outerExitRun=1, maxChoices=11, risk=CounterfactualLocalFlow
- [Drop/LocalEasy] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: hardV3=0.069, raw=0.514, cudP20=6.068, cudAvg=9.499, antiLocal=0.147, crossCrit=6, fanoutMax=4, solveDelay=6.912, dirRun=11, localPatchRun=5, outerExitRun=1, maxChoices=13, risk=CounterfactualLocalFlow

## High Structured Hardness Samples
- [B] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: hardV21=0.529, boring=0.32, avg=7.32, max=11, changeRate=0.761, riseRate=0.313, unlockPower=0.442, postSpike=0.667, crossUnlock=0.476, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: hardV21=0.498, boring=0.3, avg=10.21, max=19, changeRate=0.667, riseRate=0.25, unlockPower=0.561, postSpike=0.2, crossUnlock=0.722, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: hardV21=0.439, boring=0.324, avg=7.91, max=13, changeRate=0.63, riseRate=0.219, unlockPower=0.545, postSpike=0.2, crossUnlock=0.375, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: hardV21=0.378, boring=0.341, avg=9.72, max=15, changeRate=0.613, riseRate=0.227, unlockPower=0.514, postSpike=0, crossUnlock=0.294, risk=CounterfactualLocalFlow

## Boring Linear Risk Samples
- [Drop] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: boring=0.341, hardV21=0.378, avg=9.72, winStd=0.826, changeRate=0.613, flatRate=0.387, unlockPower=0.514, lowRun=2, sameRegionRun=4, nearRun=2, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: boring=0.324, hardV21=0.439, avg=7.91, winStd=0.868, changeRate=0.63, flatRate=0.37, unlockPower=0.545, lowRun=2, sameRegionRun=4, nearRun=2, risk=CounterfactualLocalFlow
- [B] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: boring=0.32, hardV21=0.529, avg=7.32, winStd=0.709, changeRate=0.761, flatRate=0.239, unlockPower=0.442, lowRun=2, sameRegionRun=3, nearRun=2, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: boring=0.3, hardV21=0.498, avg=10.21, winStd=0.944, changeRate=0.667, flatRate=0.333, unlockPower=0.561, lowRun=2, sameRegionRun=4, nearRun=2, risk=CounterfactualLocalFlow

## Worst Choice Curve Samples
- [Drop] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: openers=4, avg=10.21, p80=14, max=19, peakCount=49, peakExcess=286, over10=0.562, outerRun=0, sameSideOuterRun=0, outerNearRun=2, sameSideOuterFollow=1, risk=CounterfactualLocalFlow, reason=avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; near-outer patch solve run; local window burst
- [Drop] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: openers=5, avg=9.72, p80=13, max=15, peakCount=57, peakExcess=236, over10=0.434, outerRun=0, sameSideOuterRun=0, outerNearRun=1, sameSideOuterFollow=1, risk=CounterfactualLocalFlow, reason=avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; near-outer patch solve run; local window burst
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: openers=6, avg=7.91, p80=10, max=13, peakCount=43, peakExcess=108, over10=0.095, outerRun=0, sameSideOuterRun=0, outerNearRun=2, sameSideOuterFollow=2, risk=CounterfactualLocalFlow, reason=avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; near-outer patch solve run; local window burst
- [B] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: openers=7, avg=7.32, p80=9, max=11, peakCount=37, peakExcess=73, over10=0.044, outerRun=0, sameSideOuterRun=0, outerNearRun=1, sameSideOuterFollow=1, risk=CounterfactualLocalFlow, reason=flow or late-campaign only
