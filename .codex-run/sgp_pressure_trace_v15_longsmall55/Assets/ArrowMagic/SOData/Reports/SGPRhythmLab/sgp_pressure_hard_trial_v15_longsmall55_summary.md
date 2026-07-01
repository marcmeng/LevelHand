# SGP Rhythm Trace Summary

Generated: 2026-06-25 14:24:09

- Requested rows: 4
- Traced rows: 4
- Missing/failed rows: 0
- Sort mode: Read
- Include unselected: False
- Input CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\sgp_pressure_hard_trial_v15_trace_input.csv

## Trace Risk
- CounterfactualLocalFlow: 4

## Process Tier
- B: 1
- Drop: 3

## Tight Process Tier
- Drop: 4

## Choice Pressure Reality
- over10Rate avg: 0.229
- maxChoices avg: 13
- choicePeakCount avg: 31
- choicePeakExcess avg: 107.5
- meaningfulUnlockRate avg: 0.864
- outerStraightRunMax avg: 0.25
- sameSideOuterStraightRunMax avg: 0.25
- outerNearFollowRunMax avg: 1.5
- sameSideOuterFollowRunMax avg: 1.25
- outerNearDependencyRate avg: 0.059
- stageLockScore avg: 0.29
- lateRegionCount avg: 0.75
- stageGateRate avg: 0.052
- structuredHardnessV21 avg: 0.498
- boringLinearScore avg: 0.265
- choiceChangeRate avg: 0.693
- choiceRiseRate avg: 0.253
- unlockPower avg: 0.559
- postSpikeConvergence avg: 0.234
- hardStructureV3Score avg: 0.097
- causalCudP20 avg: 6.841
- causalSolveDelayAvg avg: 6.957
- causalCrossRegionCriticalLockCount avg: 8.25
- causalAntiLocalityScore avg: 0.202

## HardStructure V3 Class
- LocalEasy: 4

## Process Keep Samples

## High HardStructure V3 Samples
- [Drop/LocalEasy] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: hardV3=0.113, raw=0.593, cudP20=7.183, cudAvg=11.124, antiLocal=0.164, crossCrit=8, fanoutMax=3, solveDelay=7.655, dirRun=3, localPatchRun=4, outerExitRun=1, maxChoices=15, risk=CounterfactualLocalFlow
- [Drop/LocalEasy] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: hardV3=0.106, raw=0.56, cudP20=6.976, cudAvg=10.566, antiLocal=0.203, crossCrit=9, fanoutMax=5, solveDelay=7.288, dirRun=5, localPatchRun=4, outerExitRun=1, maxChoices=14, risk=CounterfactualLocalFlow
- [Drop/LocalEasy] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: hardV3=0.098, raw=0.604, cudP20=7.605, cudAvg=11.563, antiLocal=0.288, crossCrit=6, fanoutMax=6, solveDelay=8.827, dirRun=7, localPatchRun=7, outerExitRun=2, maxChoices=15, risk=CounterfactualLocalFlow
- [B/LocalEasy] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: hardV3=0.071, raw=0.524, cudP20=5.6, cudAvg=10.103, antiLocal=0.154, crossCrit=10, fanoutMax=4, solveDelay=4.058, dirRun=10, localPatchRun=8, outerExitRun=2, maxChoices=8, risk=CounterfactualLocalFlow

## High Structured Hardness Samples
- [B] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: hardV21=0.529, boring=0.294, avg=5.26, max=8, changeRate=0.625, riseRate=0.232, unlockPower=0.496, postSpike=0.25, crossUnlock=0.769, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: hardV21=0.519, boring=0.218, avg=9.46, max=15, changeRate=0.745, riseRate=0.273, unlockPower=0.59, postSpike=0.4, crossUnlock=0.4, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: hardV21=0.491, boring=0.223, avg=8.39, max=14, changeRate=0.754, riseRate=0.262, unlockPower=0.627, postSpike=0.143, crossUnlock=0.562, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: hardV21=0.452, boring=0.325, avg=8.36, max=15, changeRate=0.649, riseRate=0.246, unlockPower=0.523, postSpike=0.143, crossUnlock=0.571, risk=CounterfactualLocalFlow

## Boring Linear Risk Samples
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: boring=0.325, hardV21=0.452, avg=8.36, winStd=0.864, changeRate=0.649, flatRate=0.351, unlockPower=0.523, lowRun=3, sameRegionRun=4, nearRun=1, risk=CounterfactualLocalFlow
- [B] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: boring=0.294, hardV21=0.529, avg=5.26, winStd=0.718, changeRate=0.625, flatRate=0.375, unlockPower=0.496, lowRun=2, sameRegionRun=2, nearRun=2, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: boring=0.223, hardV21=0.491, avg=8.39, winStd=1.042, changeRate=0.754, flatRate=0.246, unlockPower=0.627, lowRun=2, sameRegionRun=3, nearRun=1, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: boring=0.218, hardV21=0.519, avg=9.46, winStd=0.918, changeRate=0.745, flatRate=0.255, unlockPower=0.59, lowRun=4, sameRegionRun=2, nearRun=2, risk=CounterfactualLocalFlow

## Worst Choice Curve Samples
- [Drop] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: openers=4, avg=9.46, p80=12, max=15, peakCount=42, peakExcess=176, over10=0.464, outerRun=0, sameSideOuterRun=0, outerNearRun=1, sameSideOuterFollow=1, risk=CounterfactualLocalFlow, reason=avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; near-outer patch solve run; local window burst
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: openers=3, avg=8.36, p80=11, max=15, peakCount=34, peakExcess=134, over10=0.293, outerRun=0, sameSideOuterRun=0, outerNearRun=2, sameSideOuterFollow=1, risk=CounterfactualLocalFlow, reason=avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; near-outer patch solve run; local window burst
- [Drop] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: openers=3, avg=8.39, p80=10, max=14, peakCount=46, peakExcess=118, over10=0.161, outerRun=0, sameSideOuterRun=0, outerNearRun=1, sameSideOuterFollow=1, risk=CounterfactualLocalFlow, reason=avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; local window burst
- [B] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: openers=5, avg=5.26, p80=6, max=8, peakCount=2, peakExcess=2, over10=0, outerRun=1, sameSideOuterRun=1, outerNearRun=2, sameSideOuterFollow=2, risk=CounterfactualLocalFlow, reason=flow or late-campaign only
