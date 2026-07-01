# SGP Rhythm Trace Summary

Generated: 2026-06-25 13:50:22

- Requested rows: 4
- Traced rows: 4
- Missing/failed rows: 0
- Sort mode: Read
- Include unselected: False
- Input CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\sgp_pressure_hard_trial_v8_trace_input.csv

## Trace Risk
- CounterfactualLocalFlow: 4

## Process Tier
- Drop: 4

## Tight Process Tier
- Drop: 4

## Choice Pressure Reality
- over10Rate avg: 0.7
- maxChoices avg: 19
- choicePeakCount avg: 123
- choicePeakExcess avg: 870.25
- meaningfulUnlockRate avg: 0.783
- outerStraightRunMax avg: 0.25
- sameSideOuterStraightRunMax avg: 0.25
- outerNearFollowRunMax avg: 1.75
- sameSideOuterFollowRunMax avg: 1.75
- outerNearDependencyRate avg: 0.053
- stageLockScore avg: 0.172
- lateRegionCount avg: 0.25
- stageGateRate avg: 0.026
- structuredHardnessV21 avg: 0.424
- boringLinearScore avg: 0.413
- choiceChangeRate avg: 0.59
- choiceRiseRate avg: 0.226
- unlockPower avg: 0.5
- postSpikeConvergence avg: 0.184
- hardStructureV3Score avg: 0.078
- causalCudP20 avg: 5.831
- causalSolveDelayAvg avg: 9.756
- causalCrossRegionCriticalLockCount avg: 16.25
- causalAntiLocalityScore avg: 0.214

## HardStructure V3 Class
- LocalEasy: 4

## Process Keep Samples

## High HardStructure V3 Samples
- [Drop/LocalEasy] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: hardV3=0.101, raw=0.533, cudP20=6.4, cudAvg=9.803, antiLocal=0.193, crossCrit=12, fanoutMax=5, solveDelay=11.257, dirRun=10, localPatchRun=4, outerExitRun=2, maxChoices=24, risk=CounterfactualLocalFlow
- [Drop/LocalEasy] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: hardV3=0.073, raw=0.538, cudP20=6.359, cudAvg=9.995, antiLocal=0.236, crossCrit=17, fanoutMax=5, solveDelay=10.993, dirRun=10, localPatchRun=6, outerExitRun=2, maxChoices=21, risk=CounterfactualLocalFlow
- [Drop/LocalEasy] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: hardV3=0.069, raw=0.512, cudP20=5.333, cudAvg=9.47, antiLocal=0.232, crossCrit=19, fanoutMax=4, solveDelay=8.288, dirRun=12, localPatchRun=9, outerExitRun=2, maxChoices=14, risk=CounterfactualLocalFlow
- [Drop/LocalEasy] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: hardV3=0.067, raw=0.496, cudP20=5.233, cudAvg=9.857, antiLocal=0.197, crossCrit=17, fanoutMax=4, solveDelay=8.485, dirRun=12, localPatchRun=8, outerExitRun=1, maxChoices=17, risk=CounterfactualLocalFlow

## High Structured Hardness Samples
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: hardV21=0.444, boring=0.421, avg=9.14, max=14, changeRate=0.561, riseRate=0.22, unlockPower=0.485, postSpike=0.286, crossUnlock=0.655, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: hardV21=0.435, boring=0.406, avg=11.45, max=17, changeRate=0.612, riseRate=0.252, unlockPower=0.472, postSpike=0.286, crossUnlock=0.486, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: hardV21=0.419, boring=0.409, avg=13.71, max=21, changeRate=0.597, riseRate=0.215, unlockPower=0.526, postSpike=0.083, crossUnlock=0.531, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: hardV21=0.399, boring=0.414, avg=16.35, max=24, changeRate=0.588, riseRate=0.216, unlockPower=0.519, postSpike=0.083, crossUnlock=0.375, risk=CounterfactualLocalFlow

## Boring Linear Risk Samples
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: boring=0.421, hardV21=0.444, avg=9.14, winStd=0.653, changeRate=0.561, flatRate=0.439, unlockPower=0.485, lowRun=2, sameRegionRun=5, nearRun=3, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: boring=0.414, hardV21=0.399, avg=16.35, winStd=0.819, changeRate=0.588, flatRate=0.412, unlockPower=0.519, lowRun=3, sameRegionRun=6, nearRun=2, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: boring=0.409, hardV21=0.419, avg=13.71, winStd=0.823, changeRate=0.597, flatRate=0.403, unlockPower=0.526, lowRun=2, sameRegionRun=6, nearRun=3, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: boring=0.406, hardV21=0.435, avg=11.45, winStd=0.697, changeRate=0.612, flatRate=0.388, unlockPower=0.472, lowRun=6, sameRegionRun=5, nearRun=4, risk=CounterfactualLocalFlow

## Worst Choice Curve Samples
- [Drop] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: openers=10, avg=13.71, p80=17, max=21, peakCount=138, peakExcess=1040, over10=0.827, outerRun=1, sameSideOuterRun=1, outerNearRun=3, sameSideOuterFollow=3, risk=CounterfactualLocalFlow, reason=too many openers; avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; near-outer patch solve run; local window burst
- [Drop] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: openers=9, avg=16.35, p80=22, max=24, peakCount=140, peakExcess=1423, over10=0.819, outerRun=0, sameSideOuterRun=0, outerNearRun=1, sameSideOuterFollow=1, risk=CounterfactualLocalFlow, reason=avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; near-outer patch solve run; local window burst
- [Drop] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: openers=8, avg=11.45, p80=14, max=17, peakCount=122, peakExcess=681, over10=0.771, outerRun=0, sameSideOuterRun=0, outerNearRun=1, sameSideOuterFollow=1, risk=CounterfactualLocalFlow, reason=avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; near-outer patch solve run; local window burst
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: openers=8, avg=9.14, p80=12, max=14, peakCount=92, peakExcess=337, over10=0.383, outerRun=0, sameSideOuterRun=0, outerNearRun=2, sameSideOuterFollow=2, risk=CounterfactualLocalFlow, reason=avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; near-outer patch solve run; local window burst
