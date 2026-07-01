# SGP Rhythm Trace Summary

Generated: 2026-06-25 13:27:45

- Requested rows: 4
- Traced rows: 4
- Missing/failed rows: 0
- Sort mode: Read
- Include unselected: False
- Input CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\sgp_pressure_hard_trial_v7_trace_input.csv

## Trace Risk
- CounterfactualLocalFlow: 4

## Process Tier
- Drop: 4

## Tight Process Tier
- Drop: 4

## Choice Pressure Reality
- over10Rate avg: 0.771
- maxChoices avg: 19.75
- choicePeakCount avg: 130
- choicePeakExcess avg: 1000
- meaningfulUnlockRate avg: 0.778
- outerStraightRunMax avg: 0.25
- sameSideOuterStraightRunMax avg: 0.25
- outerNearFollowRunMax avg: 1.75
- sameSideOuterFollowRunMax avg: 1.75
- outerNearDependencyRate avg: 0.05
- stageLockScore avg: 0.142
- lateRegionCount avg: 0.25
- stageGateRate avg: 0.014
- structuredHardnessV21 avg: 0.424
- boringLinearScore avg: 0.411
- choiceChangeRate avg: 0.597
- choiceRiseRate avg: 0.222
- unlockPower avg: 0.492
- postSpikeConvergence avg: 0.198
- hardStructureV3Score avg: 0.076
- causalCudP20 avg: 5.826
- causalSolveDelayAvg avg: 10.624
- causalCrossRegionCriticalLockCount avg: 15.5
- causalAntiLocalityScore avg: 0.209

## HardStructure V3 Class
- LocalEasy: 4

## Process Keep Samples

## High HardStructure V3 Samples
- [Drop/LocalEasy] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: hardV3=0.101, raw=0.532, cudP20=6.4, cudAvg=9.751, antiLocal=0.194, crossCrit=12, fanoutMax=5, solveDelay=11.388, dirRun=10, localPatchRun=4, outerExitRun=2, maxChoices=24, risk=CounterfactualLocalFlow
- [Drop/LocalEasy] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: hardV3=0.073, raw=0.538, cudP20=6.359, cudAvg=9.936, antiLocal=0.239, crossCrit=15, fanoutMax=5, solveDelay=11.768, dirRun=10, localPatchRun=6, outerExitRun=2, maxChoices=22, risk=CounterfactualLocalFlow
- [Drop/LocalEasy] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: hardV3=0.066, raw=0.492, cudP20=5.333, cudAvg=9.601, antiLocal=0.23, crossCrit=19, fanoutMax=4, solveDelay=9.475, dirRun=13, localPatchRun=9, outerExitRun=4, maxChoices=15, risk=CounterfactualLocalFlow
- [Drop/LocalEasy] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: hardV3=0.066, raw=0.489, cudP20=5.214, cudAvg=9.302, antiLocal=0.172, crossCrit=16, fanoutMax=4, solveDelay=9.867, dirRun=12, localPatchRun=8, outerExitRun=3, maxChoices=18, risk=CounterfactualLocalFlow

## High Structured Hardness Samples
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: hardV21=0.452, boring=0.419, avg=10.38, max=15, changeRate=0.576, riseRate=0.22, unlockPower=0.472, postSpike=0.333, crossUnlock=0.655, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: hardV21=0.433, boring=0.4, avg=13.09, max=18, changeRate=0.626, riseRate=0.245, unlockPower=0.464, postSpike=0.286, crossUnlock=0.471, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: hardV21=0.412, boring=0.414, avg=14.65, max=22, changeRate=0.591, riseRate=0.208, unlockPower=0.517, postSpike=0.091, crossUnlock=0.484, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: hardV21=0.4, boring=0.412, avg=16.61, max=24, changeRate=0.595, riseRate=0.216, unlockPower=0.517, postSpike=0.083, crossUnlock=0.375, risk=CounterfactualLocalFlow

## Boring Linear Risk Samples
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: boring=0.419, hardV21=0.452, avg=10.38, winStd=0.665, changeRate=0.576, flatRate=0.424, unlockPower=0.472, lowRun=2, sameRegionRun=5, nearRun=3, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: boring=0.414, hardV21=0.412, avg=14.65, winStd=0.817, changeRate=0.591, flatRate=0.409, unlockPower=0.517, lowRun=2, sameRegionRun=6, nearRun=3, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: boring=0.412, hardV21=0.4, avg=16.61, winStd=0.825, changeRate=0.595, flatRate=0.405, unlockPower=0.517, lowRun=2, sameRegionRun=6, nearRun=2, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: boring=0.4, hardV21=0.433, avg=13.09, winStd=0.727, changeRate=0.626, flatRate=0.374, unlockPower=0.464, lowRun=2, sameRegionRun=5, nearRun=4, risk=CounterfactualLocalFlow

## Worst Choice Curve Samples
- [Drop] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: openers=12, avg=13.09, p80=15, max=18, peakCount=129, peakExcess=876, over10=0.871, outerRun=0, sameSideOuterRun=0, outerNearRun=1, sameSideOuterFollow=1, risk=CounterfactualLocalFlow, reason=too many openers; avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; near-outer patch solve run; local window burst
- [Drop] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: openers=12, avg=14.65, p80=18, max=22, peakCount=138, peakExcess=1180, over10=0.867, outerRun=1, sameSideOuterRun=1, outerNearRun=3, sameSideOuterFollow=3, risk=CounterfactualLocalFlow, reason=too many openers; avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; near-outer patch solve run; local window burst
- [Drop] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: openers=10, avg=16.61, p80=22, max=24, peakCount=141, peakExcess=1457, over10=0.826, outerRun=0, sameSideOuterRun=0, outerNearRun=1, sameSideOuterFollow=1, risk=CounterfactualLocalFlow, reason=too many openers; avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; near-outer patch solve run; local window burst
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: openers=11, avg=10.38, p80=13, max=15, peakCount=112, peakExcess=487, over10=0.519, outerRun=0, sameSideOuterRun=0, outerNearRun=2, sameSideOuterFollow=2, risk=CounterfactualLocalFlow, reason=too many openers; avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; local window burst
