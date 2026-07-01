# Campaign Hardening Analyzer

Run: `final_and_candidate_pool`
Rows: 504

## Sources
- final: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500FinalPack.asset`
- candidate_pool: `Assets/ArrowMagic/SOData/Packs/Campaign500/SingleLevelCandidatePoolPack.asset`

## Leak Bands
- MediumLeak: 220
- LowLeak: 113
- HighLeak: 103
- Ok: 42
- CriticalLeak: 26

## Top Flags
- DirectEscapeLeak: 492
- ChoiceExplosion: 484
- OuterExitHeavy: 448
- WeakDependency: 414
- TooManyGoodOpeners: 142
- BoundaryStraightLeak: 101

## Top 20 Hardening Sandbox Candidates
- L387: score=1047, H=6, chains=149, opening=34, earlyAvg=30.9, directClearOuter=34, avgChoices=15.799, deps=1.896, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=r2_outer_strong_005_r2_006_a401_maskonlyb07_04_main_d20die
- L425: score=997, H=6, chains=213, opening=27, earlyAvg=27.9, directClearOuter=27, avgChoices=18.343, deps=1.624, ops=redirect_outer_heads|wrap_boundary_straights|gate_opening_choices|inject_region_gate, id=direct_advanced_33_direct_advanced_veryhard_rect_lock_buckle_g0_v05
- L183: score=969, H=6, chains=103, opening=31, earlyAvg=29.2, directClearOuter=31, avgChoices=13.825, deps=1.833, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=r2_outer_strong_005_r2_006_a401_maskonlyb04_04_main_airplane
- L490: score=942, H=6, chains=199, opening=28, earlyAvg=27, directClearOuter=28, avgChoices=14.643, deps=1.696, ops=redirect_outer_heads|wrap_boundary_straights|gate_opening_choices|inject_region_gate, id=direct_advanced_24_direct_advanced_veryhard_rect_section_unlock_g2_v03
- L381: score=938, H=6, chains=145, opening=27, earlyAvg=24.8, directClearOuter=27, avgChoices=14.779, deps=1.814, ops=redirect_outer_heads|wrap_boundary_straights|gate_opening_choices|inject_region_gate, id=direct_pure_topup_10_11_direct_polish_hard_pure_rect_dense_weave_g11_v09
- L343: score=928, H=6, chains=135, opening=31, earlyAvg=28.2, directClearOuter=31, avgChoices=12.385, deps=1.923, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=r2_outer_strong_005_r2_006_a401_maskonlyb07_07_main_skeletonskull
- L284: score=919, H=6, chains=172, opening=31, earlyAvg=27.1, directClearOuter=31, avgChoices=10.18, deps=1.993, ops=redirect_outer_heads|wrap_boundary_straights|gate_opening_choices|inject_region_gate, id=direct_pure_topup_07_22_direct_polish_hard_pure_rect_lock_buckle_g11_v03
- L382: score=878, H=6, chains=150, opening=24, earlyAvg=22.1, directClearOuter=24, avgChoices=15.74, deps=1.659, ops=redirect_outer_heads|wrap_boundary_straights|gate_opening_choices|inject_region_gate, id=direct_pure_topup_10_14_direct_polish_hard_pure_rect_stair_push_g11_v09
- L494: score=873, H=6, chains=192, opening=27, earlyAvg=26.3, directClearOuter=27, avgChoices=15.766, deps=1.842, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=direct_advanced_02_direct_advanced_veryhard_rect_dense_weave_g0_v01
- L436: score=873, H=6, chains=194, opening=26, earlyAvg=25.4, directClearOuter=26, avgChoices=13.876, deps=1.554, ops=redirect_outer_heads|wrap_boundary_straights|gate_opening_choices|inject_region_gate, id=direct_advanced_25_direct_advanced_veryhard_rect_lock_buckle_g3_v04
- L474: score=870, H=6, chains=178, opening=25, earlyAvg=25.6, directClearOuter=25, avgChoices=15.899, deps=1.673, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=direct_advanced_42_direct_advanced_veryhard_rect_dense_weave_g1_v06
- L449: score=869, H=6, chains=181, opening=28, earlyAvg=26.2, directClearOuter=28, avgChoices=11.547, deps=1.706, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=direct_pure_topup_07_14_direct_polish_hard_pure_rect_core_burst_g10_v02
- L465: score=866, H=6, chains=213, opening=27, earlyAvg=24.7, directClearOuter=27, avgChoices=11.718, deps=1.731, ops=redirect_outer_heads|wrap_boundary_straights|gate_opening_choices|inject_region_gate, id=direct_advanced_23_direct_advanced_veryhard_rect_sweep_g2_v03
- L488: score=859, H=6, chains=195, opening=27, earlyAvg=23.9, directClearOuter=27, avgChoices=10.656, deps=1.768, ops=redirect_outer_heads|wrap_boundary_straights|gate_opening_choices|inject_region_gate, id=direct_advanced_29_direct_advanced_veryhard_rect_stair_push_g3_v04
- L173: score=857, H=6, chains=104, opening=25, earlyAvg=26.1, directClearOuter=25, avgChoices=15.625, deps=1.684, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=r2_outer_strong_005_r2_006_a401_maskonlyb05_18_large_teapot
- L366: score=856, H=6, chains=139, opening=27, earlyAvg=27.7, directClearOuter=27, avgChoices=12.777, deps=2.143, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=direct_polish_clean_24_direct_polish_normal_rect_stair_push_g2_v03
- L475: score=854, H=6, chains=195, opening=26, earlyAvg=24.7, directClearOuter=26, avgChoices=12.241, deps=1.751, ops=redirect_outer_heads|wrap_boundary_straights|gate_opening_choices|inject_region_gate, id=direct_advanced_36_direct_advanced_veryhard_rect_dual_zone_g0_v05
- L405: score=850, H=6, chains=170, opening=25, earlyAvg=26.2, directClearOuter=25, avgChoices=15.906, deps=1.6, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=direct_pure_topup_10_17_direct_polish_hard_pure_rect_maze_long_chain_g11_v09
- L380: score=846, H=6, chains=144, opening=26, earlyAvg=23.6, directClearOuter=26, avgChoices=11.215, deps=1.788, ops=redirect_outer_heads|wrap_boundary_straights|gate_opening_choices|inject_region_gate, id=direct_pure_topup_08_07_direct_polish_hard_pure_rect_section_unlock_g12_v04
- L500: score=844, H=6, chains=200, opening=25, earlyAvg=24.3, directClearOuter=25, avgChoices=11.265, deps=1.726, ops=redirect_outer_heads|wrap_boundary_straights|gate_opening_choices|inject_region_gate, id=direct_advanced_45_direct_advanced_veryhard_rect_stair_push_g1_v06

## Interpretation

- HighLeak: best sandbox targets for light/heavy hardening.
- DirectEscapeLeak means early no-cost outer exits are likely making the level too sweepable.
- ChoiceExplosion means the player probably has too many simultaneous valid removals.
- WeakDependency means the level stays wide while average unlock delay is low.
- This pass is analysis only; it does not mutate level assets.
