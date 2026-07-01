# SGP Rhythm Trace Summary

Generated: 2026-06-25 14:44:17

- Requested rows: 3
- Traced rows: 3
- Missing/failed rows: 0
- Sort mode: Read
- Include unselected: False
- Input CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\sgp_pressure_hard_trial_v17_current_trace_input.csv

## Trace Risk
- CounterfactualLocalFlow: 2
- OuterExitHeadVisual: 1

## Process Tier
- B: 1
- Drop: 2

## Tight Process Tier
- Drop: 3

## Choice Pressure Reality
- over10Rate avg: 0.088
- maxChoices avg: 12.33
- choicePeakCount avg: 25
- choicePeakExcess avg: 66
- meaningfulUnlockRate avg: 0.922
- outerStraightRunMax avg: 0
- sameSideOuterStraightRunMax avg: 0
- outerNearFollowRunMax avg: 1.67
- sameSideOuterFollowRunMax avg: 1
- outerNearDependencyRate avg: 0.084
- stageLockScore avg: 0.304
- lateRegionCount avg: 1
- stageGateRate avg: 0.08
- structuredHardnessV21 avg: 0.485
- boringLinearScore avg: 0.277
- choiceChangeRate avg: 0.629
- choiceRiseRate avg: 0.228
- unlockPower avg: 0.551
- postSpikeConvergence avg: 0.27
- hardStructureV3Score avg: 0.087
- causalCudP20 avg: 6.987
- causalSolveDelayAvg avg: 5.453
- causalCrossRegionCriticalLockCount avg: 8.33
- causalAntiLocalityScore avg: 0.219

## HardStructure V3 Class
- LocalEasy: 3

## Process Keep Samples

## High HardStructure V3 Samples
- [Drop/LocalEasy] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: hardV3=0.102, raw=0.539, cudP20=6.495, cudAvg=10.406, antiLocal=0.161, crossCrit=8, fanoutMax=5, solveDelay=6.742, dirRun=9, localPatchRun=4, outerExitRun=4, maxChoices=12, risk=CounterfactualLocalFlow
- [B/LocalEasy] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: hardV3=0.082, raw=0.61, cudP20=7.5, cudAvg=11.061, antiLocal=0.25, crossCrit=8, fanoutMax=5, solveDelay=3.846, dirRun=4, localPatchRun=8, outerExitRun=3, maxChoices=11, risk=OuterExitHeadVisual
- [Drop/LocalEasy] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: hardV3=0.077, raw=0.573, cudP20=6.967, cudAvg=11.07, antiLocal=0.246, crossCrit=9, fanoutMax=5, solveDelay=5.77, dirRun=14, localPatchRun=6, outerExitRun=2, maxChoices=14, risk=CounterfactualLocalFlow

## High Structured Hardness Samples
- [B] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: hardV21=0.545, boring=0.287, avg=6.35, max=11, changeRate=0.589, riseRate=0.214, unlockPower=0.515, postSpike=0.667, crossUnlock=0.667, risk=OuterExitHeadVisual
- [Drop] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: hardV21=0.482, boring=0.282, avg=6.82, max=14, changeRate=0.703, riseRate=0.25, unlockPower=0.578, postSpike=0.143, crossUnlock=0.562, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: hardV21=0.428, boring=0.263, avg=7.48, max=12, changeRate=0.594, riseRate=0.219, unlockPower=0.559, postSpike=0, crossUnlock=0.571, risk=CounterfactualLocalFlow

## Boring Linear Risk Samples
- [B] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: boring=0.287, hardV21=0.545, avg=6.35, winStd=0.783, changeRate=0.589, flatRate=0.411, unlockPower=0.515, lowRun=4, sameRegionRun=2, nearRun=1, risk=OuterExitHeadVisual
- [Drop] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: boring=0.282, hardV21=0.482, avg=6.82, winStd=0.974, changeRate=0.703, flatRate=0.297, unlockPower=0.578, lowRun=2, sameRegionRun=4, nearRun=2, risk=CounterfactualLocalFlow
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: boring=0.263, hardV21=0.428, avg=7.48, winStd=0.86, changeRate=0.594, flatRate=0.406, unlockPower=0.559, lowRun=6, sameRegionRun=2, nearRun=1, risk=CounterfactualLocalFlow

## Worst Choice Curve Samples
- [Drop] sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst: openers=4, avg=6.82, p80=9, max=14, peakCount=22, peakExcess=68, over10=0.138, outerRun=0, sameSideOuterRun=0, outerNearRun=2, sameSideOuterFollow=1, risk=CounterfactualLocalFlow, reason=avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; near-outer patch solve run; local window burst
- [Drop] sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock: openers=3, avg=7.48, p80=10, max=12, peakCount=38, peakExcess=100, over10=0.108, outerRun=0, sameSideOuterRun=0, outerNearRun=1, sameSideOuterFollow=1, risk=CounterfactualLocalFlow, reason=avg choices high; p80 choices high; choice peak high; too many over-10 steps; local patch solve run; near-outer patch solve run; local window burst
- [B] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: openers=5, avg=6.35, p80=8, max=11, peakCount=15, peakExcess=30, over10=0.018, outerRun=0, sameSideOuterRun=0, outerNearRun=2, sameSideOuterFollow=1, risk=OuterExitHeadVisual, reason=flow or late-campaign only
