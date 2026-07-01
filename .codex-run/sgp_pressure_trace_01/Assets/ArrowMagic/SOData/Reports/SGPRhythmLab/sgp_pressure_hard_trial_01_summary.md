# SGP Rhythm Trace Summary

Generated: 2026-06-25 12:40:38

- Requested rows: 1
- Traced rows: 1
- Missing/failed rows: 0
- Sort mode: LowChoice
- Include unselected: False
- Input CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\sgp_pressure_hard_trial_trace_input_01.csv

## Trace Risk
- CounterfactualLocalFlow: 1

## Process Tier
- Drop: 1

## Tight Process Tier
- Drop: 1

## Choice Pressure Reality
- over10Rate avg: 0.595
- maxChoices avg: 18
- choicePeakCount avg: 213
- choicePeakExcess avg: 944
- meaningfulUnlockRate avg: 0.78
- outerStraightRunMax avg: 2
- sameSideOuterStraightRunMax avg: 2
- outerNearFollowRunMax avg: 2
- sameSideOuterFollowRunMax avg: 2
- outerNearDependencyRate avg: 0.062
- stageLockScore avg: 0.305
- lateRegionCount avg: 1
- stageGateRate avg: 0.009
- structuredHardnessV21 avg: 0.393
- boringLinearScore avg: 0.447
- choiceChangeRate avg: 0.566
- choiceRiseRate avg: 0.226
- unlockPower avg: 0.477
- postSpikeConvergence avg: 0.2
- hardStructureV3Score avg: 0.06
- causalCudP20 avg: 5
- causalSolveDelayAvg avg: 9.612
- causalCrossRegionCriticalLockCount avg: 16
- causalAntiLocalityScore avg: 0.12

## HardStructure V3 Class
- LocalEasy: 1

## Process Keep Samples

## High HardStructure V3 Samples
- [Drop/LocalEasy] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: hardV3=0.06, raw=0.446, cudP20=5, cudAvg=8.085, antiLocal=0.12, crossCrit=16, fanoutMax=4, solveDelay=9.612, dirRun=15, localPatchRun=11, outerExitRun=4, maxChoices=18, risk=CounterfactualLocalFlow

## High Structured Hardness Samples
- [Drop] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: hardV21=0.393, boring=0.447, avg=11.16, max=18, changeRate=0.566, riseRate=0.226, unlockPower=0.477, postSpike=0.2, crossUnlock=0.32, risk=CounterfactualLocalFlow

## Boring Linear Risk Samples
- [Drop] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: boring=0.447, hardV21=0.393, avg=11.16, winStd=0.703, changeRate=0.566, flatRate=0.434, unlockPower=0.477, lowRun=2, sameRegionRun=6, nearRun=5, risk=CounterfactualLocalFlow

## Worst Choice Curve Samples
- [Drop] sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle: openers=13, avg=11.16, p80=13, max=18, peakCount=213, peakExcess=944, over10=0.595, outerRun=2, sameSideOuterRun=2, outerNearRun=2, sameSideOuterFollow=2, risk=CounterfactualLocalFlow, reason=too many openers; avg choices high; p80 choices high; choice peak high; too many over-10 steps; nearby solve run; local patch solve run; near-outer patch solve run; local window burst
