# SGP Rhythm Trace Summary

Generated: 2026-06-25 12:50:17

- Requested rows: 3
- Traced rows: 3
- Missing/failed rows: 0
- Sort mode: LowChoice
- Include unselected: False
- Input CSV: F:\Unityproject\ArrowLevel-Hand\.codex-run\sgp_pressure_hard_benchmark_trace_input.csv

## Trace Risk
- LocalPatchBurst: 1
- DependencyFollowRun: 1
- CounterfactualLocalFlow: 1

## Process Tier
- A: 3

## Tight Process Tier
- A: 3

## Choice Pressure Reality
- over10Rate avg: 0
- maxChoices avg: 5.33
- choicePeakCount avg: 0
- choicePeakExcess avg: 0
- meaningfulUnlockRate avg: 1
- outerStraightRunMax avg: 0
- sameSideOuterStraightRunMax avg: 0
- outerNearFollowRunMax avg: 0
- sameSideOuterFollowRunMax avg: 0
- outerNearDependencyRate avg: 0
- stageLockScore avg: 0.558
- lateRegionCount avg: 2
- stageGateRate avg: 0.444
- structuredHardnessV21 avg: 0.63
- boringLinearScore avg: 0.421
- choiceChangeRate avg: 0.547
- choiceRiseRate avg: 0.196
- unlockPower avg: 0.418
- postSpikeConvergence avg: 0.111
- hardStructureV3Score avg: 0.671
- causalCudP20 avg: 8.933
- causalSolveDelayAvg avg: 1.033
- causalCrossRegionCriticalLockCount avg: 4.67
- causalAntiLocalityScore avg: 0.718

## HardStructure V3 Class
- TrueHardCandidate: 2
- HardPotential: 1

## Process Keep Samples
- [A] rootvarv128prod_45_rootvarv122_15_root_variant_library_v1_22_size_nonrecursive_32_s_6775bdd7: openers=5, avg=2.96, p80=4, max=5, peakCount=0, peakExcess=0, over10=0, meaningful=1, stageScore=0.761, hardV21=0.603, boring=0.439, unlockPower=0.364, postSpike=0, outerNearRun=0, sameSideOuterRun=0
- [A] rootvarv128prod_01_rootvarv121_03_rootvarv119_05_rv115c_08_rootvarlibv18bal_03_casc_2f740046: openers=5, avg=2.57, p80=4, max=5, peakCount=0, peakExcess=0, over10=0, meaningful=1, stageScore=0.489, hardV21=0.576, boring=0.465, unlockPower=0.379, postSpike=0, outerNearRun=0, sameSideOuterRun=0
- [A] rootvarv128prod_57_rootvarv122_27_root_variant_library_v1_22_size_nonrecursive_43_s_bd4f6820: openers=3, avg=2.98, p80=4, max=6, peakCount=0, peakExcess=0, over10=0, meaningful=1, stageScore=0.424, hardV21=0.71, boring=0.36, unlockPower=0.512, postSpike=0.333, outerNearRun=0, sameSideOuterRun=0

## High HardStructure V3 Samples
- [A/TrueHardCandidate] rootvarv128prod_57_rootvarv122_27_root_variant_library_v1_22_size_nonrecursive_43_s_bd4f6820: hardV3=0.695, raw=0.713, cudP20=8.8, cudAvg=14.422, antiLocal=0.684, crossCrit=7, fanoutMax=4, solveDelay=1.474, dirRun=3, localPatchRun=2, outerExitRun=0, maxChoices=6, risk=LocalPatchBurst
- [A/TrueHardCandidate] rootvarv128prod_45_rootvarv122_15_root_variant_library_v1_22_size_nonrecursive_32_s_6775bdd7: hardV3=0.684, raw=0.684, cudP20=9, cudAvg=13.639, antiLocal=0.789, crossCrit=3, fanoutMax=2, solveDelay=1.105, dirRun=4, localPatchRun=2, outerExitRun=1, maxChoices=5, risk=DependencyFollowRun
- [A/HardPotential] rootvarv128prod_01_rootvarv121_03_rootvarv119_05_rv115c_08_rootvarlibv18bal_03_casc_2f740046: hardV3=0.635, raw=0.655, cudP20=9, cudAvg=14.49, antiLocal=0.68, crossCrit=4, fanoutMax=2, solveDelay=0.52, dirRun=6, localPatchRun=2, outerExitRun=0, maxChoices=5, risk=CounterfactualLocalFlow

## High Structured Hardness Samples
- [A] rootvarv128prod_57_rootvarv122_27_root_variant_library_v1_22_size_nonrecursive_43_s_bd4f6820: hardV21=0.71, boring=0.36, avg=2.98, max=6, changeRate=0.55, riseRate=0.2, unlockPower=0.512, postSpike=0.333, crossUnlock=0.875, risk=LocalPatchBurst
- [A] rootvarv128prod_45_rootvarv122_15_root_variant_library_v1_22_size_nonrecursive_32_s_6775bdd7: hardV21=0.603, boring=0.439, avg=2.96, max=5, changeRate=0.609, riseRate=0.217, unlockPower=0.364, postSpike=0, crossUnlock=0.6, risk=DependencyFollowRun
- [A] rootvarv128prod_01_rootvarv121_03_rootvarv119_05_rv115c_08_rootvarlibv18bal_03_casc_2f740046: hardV21=0.576, boring=0.465, avg=2.57, max=5, changeRate=0.483, riseRate=0.172, unlockPower=0.379, postSpike=0, crossUnlock=0.8, risk=CounterfactualLocalFlow

## Boring Linear Risk Samples
- [A] rootvarv128prod_01_rootvarv121_03_rootvarv119_05_rv115c_08_rootvarlibv18bal_03_casc_2f740046: boring=0.465, hardV21=0.576, avg=2.57, winStd=0.489, changeRate=0.483, flatRate=0.517, unlockPower=0.379, lowRun=17, sameRegionRun=2, nearRun=1, risk=CounterfactualLocalFlow
- [A] rootvarv128prod_45_rootvarv122_15_root_variant_library_v1_22_size_nonrecursive_32_s_6775bdd7: boring=0.439, hardV21=0.603, avg=2.96, winStd=0.511, changeRate=0.609, flatRate=0.391, unlockPower=0.364, lowRun=6, sameRegionRun=3, nearRun=1, risk=DependencyFollowRun
- [A] rootvarv128prod_57_rootvarv122_27_root_variant_library_v1_22_size_nonrecursive_43_s_bd4f6820: boring=0.36, hardV21=0.71, avg=2.98, winStd=0.648, changeRate=0.55, flatRate=0.45, unlockPower=0.512, lowRun=6, sameRegionRun=2, nearRun=2, risk=LocalPatchBurst

## Worst Choice Curve Samples
- [A] rootvarv128prod_57_rootvarv122_27_root_variant_library_v1_22_size_nonrecursive_43_s_bd4f6820: openers=3, avg=2.98, p80=4, max=6, peakCount=0, peakExcess=0, over10=0, outerRun=0, sameSideOuterRun=0, outerNearRun=0, sameSideOuterFollow=0, risk=LocalPatchBurst, reason=usable process curve
- [A] rootvarv128prod_01_rootvarv121_03_rootvarv119_05_rv115c_08_rootvarlibv18bal_03_casc_2f740046: openers=5, avg=2.57, p80=4, max=5, peakCount=0, peakExcess=0, over10=0, outerRun=0, sameSideOuterRun=0, outerNearRun=0, sameSideOuterFollow=0, risk=CounterfactualLocalFlow, reason=usable process curve
- [A] rootvarv128prod_45_rootvarv122_15_root_variant_library_v1_22_size_nonrecursive_32_s_6775bdd7: openers=5, avg=2.96, p80=4, max=5, peakCount=0, peakExcess=0, over10=0, outerRun=0, sameSideOuterRun=0, outerNearRun=0, sameSideOuterFollow=0, risk=DependencyFollowRun, reason=usable process curve
