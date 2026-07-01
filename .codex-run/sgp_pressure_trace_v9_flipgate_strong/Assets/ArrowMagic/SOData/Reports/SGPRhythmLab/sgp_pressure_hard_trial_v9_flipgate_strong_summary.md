# SGP Rhythm Trace Summary

Generated: 2026-06-25 13:58:26

- Requested rows: 4
- Traced rows: 4
- Missing/failed rows: 0
- Sort mode: Read
- Include unselected: False
- Input CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\sgp_pressure_hard_trial_v9_trace_input.csv

## Trace Risk
- CounterfactualLocalFlow: 4

## Process Tier
- Drop: 4

## Tight Process Tier
- Drop: 4

## Choice Pressure Reality
- over10Rate avg: 0.68
- maxChoices avg: 18.25
- choicePeakCount avg: 120
- choicePeakExcess avg: 798.5
- meaningfulUnlockRate avg: 0.782
- outerStraightRunMax avg: 0.25
- sameSideOuterStraightRunMax avg: 0.25
- outerNearFollowRunMax avg: 2
- sameSideOuterFollowRunMax avg: 2
- outerNearDependencyRate avg: 0.054
- stageLockScore avg: 0.217
- lateRegionCount avg: 0.5
- stageGateRate avg: 0.026
- structuredHardnessV21 avg: 0.424
- boringLinearScore avg: 0.408
- choiceChangeRate avg: 0.595
- choiceRiseRate avg: 0.233
- unlockPower avg: 0.508
- postSpikeConvergence avg: 0.177
- hardStructureV3Score avg: 0.078
- causalCudP20 avg: 5.804
- causalSolveDelayAvg avg: 9.7
- causalCrossRegionCriticalLockCount avg: 16.25
- causalAntiLocalityScore avg: 0.206

## HardStructure V3 Class
- LocalEasy: 4

## Process Keep Samples

## High HardStructure V3 Samples
- [Drop/LocalEasy] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: hardV3=0.103, raw=0.54, cudP20=6.357, cudAvg=9.746, antiLocal=0.19, crossCrit=12, fanoutMax=5, solveDelay=11.063, dirRun=10, localPatchRun=4, outerExitRun=2, maxChoices=22, risk=CounterfactualLocalFlow
- [Drop/LocalEasy] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: hardV3=0.073, raw=0.538, cudP20=6.359, cudAvg=9.995, antiLocal=0.236, crossCrit=17, fanoutMax=5, solveDelay=10.993, dirRun=10, localPatchRun=6, outerExitRun=2, maxChoices=21, risk=CounterfactualLocalFlow
- [Drop/LocalEasy] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: hardV3=0.069, raw=0.512, cudP20=5.333, cudAvg=9.447, antiLocal=0.23, crossCrit=19, fanoutMax=4, solveDelay=8.238, dirRun=12, localPatchRun=9, outerExitRun=1, maxChoices=14, risk=CounterfactualLocalFlow
- [Drop/LocalEasy] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: hardV3=0.066, raw=0.486, cudP20=5.167, cudAvg=9.487, antiLocal=0.17, crossCrit=17, fanoutMax=5, solveDelay=8.504, dirRun=12, localPatchRun=8, outerExitRun=1, maxChoices=16, risk=CounterfactualLocalFlow

## High Structured Hardness Samples
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: hardV21=0.461, boring=0.419, avg=9.14, max=14, changeRate=0.561, riseRate=0.22, unlockPower=0.494, postSpike=0.375, crossUnlock=0.655, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: hardV21=0.419, boring=0.409, avg=13.71, max=21, changeRate=0.597, riseRate=0.215, unlockPower=0.526, postSpike=0.083, crossUnlock=0.531, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: hardV21=0.418, boring=0.392, avg=10.79, max=16, changeRate=0.633, riseRate=0.273, unlockPower=0.487, postSpike=0.167, crossUnlock=0.447, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: hardV21=0.399, boring=0.414, avg=14.97, max=22, changeRate=0.588, riseRate=0.223, unlockPower=0.523, postSpike=0.083, crossUnlock=0.364, risk=CounterfactualLocalFlow

## Boring Linear Risk Samples
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: boring=0.419, hardV21=0.461, avg=9.14, winStd=0.656, changeRate=0.561, flatRate=0.439, unlockPower=0.494, lowRun=2, sameRegionRun=5, nearRun=3, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: boring=0.414, hardV21=0.399, avg=14.97, winStd=0.809, changeRate=0.588, flatRate=0.412, unlockPower=0.523, lowRun=3, sameRegionRun=6, nearRun=2, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: boring=0.409, hardV21=0.419, avg=13.71, winStd=0.823, changeRate=0.597, flatRate=0.403, unlockPower=0.526, lowRun=2, sameRegionRun=6, nearRun=3, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: boring=0.392, hardV21=0.418, avg=10.79, winStd=0.732, changeRate=0.633, flatRate=0.367, unlockPower=0.487, lowRun=6, sameRegionRun=5, nearRun=4, risk=CounterfactualLocalFlow

## Worst Choice Curve Samples
- [Drop] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: openers=10, avg=13.71, p80=17, max=21, peakCount=138, peakExcess=1040, over10=0.827, outerRun=1, sameSideOuterRun=1, outerNearRun=3, sameSideOuterFollow=3, risk=CounterfactualLocalFlow, reason=too many openers; avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; near-outer patch solve run; local window burst
- [Drop] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: openers=7, avg=14.97, p80=20, max=22, peakCount=139, peakExcess=1217, over10=0.805, outerRun=0, sameSideOuterRun=0, outerNearRun=2, sameSideOuterFollow=2, risk=CounterfactualLocalFlow, reason=avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; near-outer patch solve run; local window burst
- [Drop] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: openers=5, avg=10.79, p80=13, max=16, peakCount=111, peakExcess=600, over10=0.707, outerRun=0, sameSideOuterRun=0, outerNearRun=1, sameSideOuterFollow=1, risk=CounterfactualLocalFlow, reason=avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; near-outer patch solve run; local window burst
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: openers=7, avg=9.14, p80=12, max=14, peakCount=92, peakExcess=337, over10=0.383, outerRun=0, sameSideOuterRun=0, outerNearRun=2, sameSideOuterFollow=2, risk=CounterfactualLocalFlow, reason=avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; near-outer patch solve run; local window burst
