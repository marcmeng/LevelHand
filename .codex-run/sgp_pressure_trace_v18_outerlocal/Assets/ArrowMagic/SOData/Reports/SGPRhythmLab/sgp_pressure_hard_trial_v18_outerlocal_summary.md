# SGP Rhythm Trace Summary

Generated: 2026-06-25 14:52:15

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
- A: 1
- B: 2
- Drop: 1

## Tight Process Tier
- B: 2
- Drop: 2

## Choice Pressure Reality
- over10Rate avg: 0.049
- maxChoices avg: 9.5
- choicePeakCount avg: 6.25
- choicePeakExcess avg: 23.25
- meaningfulUnlockRate avg: 0.896
- outerStraightRunMax avg: 0.25
- sameSideOuterStraightRunMax avg: 0.25
- outerNearFollowRunMax avg: 2
- sameSideOuterFollowRunMax avg: 1.75
- outerNearDependencyRate avg: 0.048
- stageLockScore avg: 0.572
- lateRegionCount avg: 2.25
- stageGateRate avg: 0.186
- structuredHardnessV21 avg: 0.554
- boringLinearScore avg: 0.305
- choiceChangeRate avg: 0.666
- choiceRiseRate avg: 0.282
- unlockPower avg: 0.499
- postSpikeConvergence avg: 0.24
- hardStructureV3Score avg: 0.119
- causalCudP20 avg: 6.868
- causalSolveDelayAvg avg: 4.022
- causalCrossRegionCriticalLockCount avg: 11.75
- causalAntiLocalityScore avg: 0.247

## HardStructure V3 Class
- LocalEasy: 4

## Process Keep Samples
- [A] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: openers=1, avg=5.31, p80=7, max=8, peakCount=4, peakExcess=4, over10=0, meaningful=0.842, stageScore=0.682, hardV21=0.493, boring=0.304, unlockPower=0.49, postSpike=0, outerNearRun=3, sameSideOuterRun=3

## High HardStructure V3 Samples
- [A/LocalEasy] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: hardV3=0.192, raw=0.628, cudP20=6.731, cudAvg=12.065, antiLocal=0.298, crossCrit=13, fanoutMax=3, solveDelay=4.386, dirRun=4, localPatchRun=3, outerExitRun=2, maxChoices=8, risk=CounterfactualLocalFlow
- [B/LocalEasy] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: hardV3=0.131, raw=0.609, cudP20=7.294, cudAvg=11.803, antiLocal=0.276, crossCrit=11, fanoutMax=4, solveDelay=3.172, dirRun=6, localPatchRun=4, outerExitRun=2, maxChoices=8, risk=CounterfactualLocalFlow
- [Drop/LocalEasy] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: hardV3=0.082, raw=0.607, cudP20=7.588, cudAvg=12.122, antiLocal=0.25, crossCrit=13, fanoutMax=3, solveDelay=5.783, dirRun=12, localPatchRun=7, outerExitRun=1, maxChoices=16, risk=CounterfactualLocalFlow
- [B/LocalEasy] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: hardV3=0.07, raw=0.522, cudP20=5.857, cudAvg=10.252, antiLocal=0.164, crossCrit=10, fanoutMax=3, solveDelay=2.745, dirRun=8, localPatchRun=13, outerExitRun=1, maxChoices=6, risk=DependencyFollowRun

## High Structured Hardness Samples
- [B] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: hardV21=0.661, boring=0.323, avg=3.78, max=6, changeRate=0.561, riseRate=0.228, unlockPower=0.48, postSpike=0.5, crossUnlock=0.769, risk=DependencyFollowRun
- [B] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: hardV21=0.563, boring=0.374, avg=4.93, max=8, changeRate=0.667, riseRate=0.283, unlockPower=0.487, postSpike=0.333, crossUnlock=0.647, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: hardV21=0.499, boring=0.218, avg=6.69, max=16, changeRate=0.767, riseRate=0.317, unlockPower=0.541, postSpike=0.125, crossUnlock=0.684, risk=CounterfactualLocalFlow
- [A] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: hardV21=0.493, boring=0.304, avg=5.31, max=8, changeRate=0.667, riseRate=0.298, unlockPower=0.49, postSpike=0, crossUnlock=0.765, risk=CounterfactualLocalFlow

## Boring Linear Risk Samples
- [B] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: boring=0.374, hardV21=0.563, avg=4.93, winStd=0.8, changeRate=0.667, flatRate=0.333, unlockPower=0.487, lowRun=3, sameRegionRun=5, nearRun=2, risk=CounterfactualLocalFlow
- [B] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: boring=0.323, hardV21=0.661, avg=3.78, winStd=0.648, changeRate=0.561, flatRate=0.439, unlockPower=0.48, lowRun=3, sameRegionRun=2, nearRun=1, risk=DependencyFollowRun
- [A] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: boring=0.304, hardV21=0.493, avg=5.31, winStd=0.816, changeRate=0.667, flatRate=0.333, unlockPower=0.49, lowRun=4, sameRegionRun=3, nearRun=3, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: boring=0.218, hardV21=0.499, avg=6.69, winStd=0.991, changeRate=0.767, flatRate=0.233, unlockPower=0.541, lowRun=3, sameRegionRun=2, nearRun=1, risk=CounterfactualLocalFlow

## Worst Choice Curve Samples
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: openers=1, avg=6.69, p80=10, max=16, peakCount=19, peakExcess=87, over10=0.197, outerRun=0, sameSideOuterRun=0, outerNearRun=1, sameSideOuterFollow=1, risk=CounterfactualLocalFlow, reason=avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; near-outer patch solve run; local window burst
- [A] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: openers=1, avg=5.31, p80=7, max=8, peakCount=4, peakExcess=4, over10=0, outerRun=1, sameSideOuterRun=1, outerNearRun=3, sameSideOuterFollow=3, risk=CounterfactualLocalFlow, reason=usable process curve
- [B] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: openers=3, avg=4.93, p80=6, max=8, peakCount=2, peakExcess=2, over10=0, outerRun=0, sameSideOuterRun=0, outerNearRun=3, sameSideOuterFollow=2, risk=CounterfactualLocalFlow, reason=flow or late-campaign only
- [B] sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave: openers=3, avg=3.78, p80=5, max=6, peakCount=0, peakExcess=0, over10=0, outerRun=0, sameSideOuterRun=0, outerNearRun=1, sameSideOuterFollow=1, risk=DependencyFollowRun, reason=flow or late-campaign only
