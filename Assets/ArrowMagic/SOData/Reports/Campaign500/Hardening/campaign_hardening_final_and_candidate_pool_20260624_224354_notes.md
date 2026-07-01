# Campaign Hardening Analyzer

Run: `final_and_candidate_pool`
Rows: 504

## Sources
- final: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500FinalPack.asset`
- candidate_pool: `Assets/ArrowMagic/SOData/Packs/Campaign500/SingleLevelCandidatePoolPack.asset`

## Leak Bands
- LowLeak: 284
- MediumLeak: 141
- Ok: 77
- HighLeak: 2

## Top Flags
- BoundaryLeak: 492
- DirectEscapeLeak: 492
- ChoiceExplosion: 484
- OuterExitHeavy: 448
- WeakDependency: 358
- TooManyGoodOpeners: 142
- BoundaryStraightLeak: 101

## Top 20 Hardening Sandbox Candidates
- L425: score=676, H=5, chains=213, opening=27, earlyAvg=27.9, directClearOuter=27, avgChoices=18.343, deps=1.624, ops=redirect_outer_heads|wrap_boundary_straights|gate_opening_choices|inject_region_gate, id=direct_advanced_33_direct_advanced_veryhard_rect_lock_buckle_g0_v05
- L387: score=662, H=5, chains=149, opening=34, earlyAvg=30.9, directClearOuter=34, avgChoices=15.799, deps=1.896, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=r2_outer_strong_005_r2_006_a401_maskonlyb07_04_main_d20die
- L474: score=625, H=4, chains=178, opening=25, earlyAvg=25.6, directClearOuter=25, avgChoices=15.899, deps=1.673, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=direct_advanced_42_direct_advanced_veryhard_rect_dense_weave_g1_v06
- L183: score=622, H=4, chains=103, opening=31, earlyAvg=29.2, directClearOuter=31, avgChoices=13.825, deps=1.833, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=r2_outer_strong_005_r2_006_a401_maskonlyb04_04_main_airplane
- L494: score=604, H=4, chains=192, opening=27, earlyAvg=26.3, directClearOuter=27, avgChoices=15.766, deps=1.842, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=direct_advanced_02_direct_advanced_veryhard_rect_dense_weave_g0_v01
- L405: score=602, H=4, chains=170, opening=25, earlyAvg=26.2, directClearOuter=25, avgChoices=15.906, deps=1.6, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=direct_pure_topup_10_17_direct_polish_hard_pure_rect_maze_long_chain_g11_v09
- L490: score=601, H=4, chains=199, opening=28, earlyAvg=27, directClearOuter=28, avgChoices=14.643, deps=1.696, ops=redirect_outer_heads|wrap_boundary_straights|gate_opening_choices|inject_region_gate, id=direct_advanced_24_direct_advanced_veryhard_rect_section_unlock_g2_v03
- L343: score=600, H=4, chains=135, opening=31, earlyAvg=28.2, directClearOuter=31, avgChoices=12.385, deps=1.923, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=r2_outer_strong_005_r2_006_a401_maskonlyb07_07_main_skeletonskull
- L449: score=598, H=4, chains=181, opening=28, earlyAvg=26.2, directClearOuter=28, avgChoices=11.547, deps=1.706, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=direct_pure_topup_07_14_direct_polish_hard_pure_rect_core_burst_g10_v02
- L382: score=598, H=4, chains=150, opening=24, earlyAvg=22.1, directClearOuter=24, avgChoices=15.74, deps=1.659, ops=redirect_outer_heads|wrap_boundary_straights|gate_opening_choices|inject_region_gate, id=direct_pure_topup_10_14_direct_polish_hard_pure_rect_stair_push_g11_v09
- L173: score=596, H=4, chains=104, opening=25, earlyAvg=26.1, directClearOuter=25, avgChoices=15.625, deps=1.684, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=r2_outer_strong_005_r2_006_a401_maskonlyb05_18_large_teapot
- L287: score=594, H=4, chains=214, opening=25, earlyAvg=24.8, directClearOuter=25, avgChoices=16.136, deps=1.751, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=direct_advanced_35_direct_advanced_veryhard_rect_core_burst_g0_v05
- L452: score=594, H=4, chains=154, opening=25, earlyAvg=22, directClearOuter=25, avgChoices=15.396, deps=1.829, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=direct_pure_topup_07_02_direct_polish_hard_pure_rect_lock_buckle_g9_v01
- L381: score=587, H=4, chains=145, opening=27, earlyAvg=24.8, directClearOuter=27, avgChoices=14.779, deps=1.814, ops=redirect_outer_heads|wrap_boundary_straights|gate_opening_choices|inject_region_gate, id=direct_pure_topup_10_11_direct_polish_hard_pure_rect_dense_weave_g11_v09
- L166: score=583, H=4, chains=119, opening=22, earlyAvg=25.1, directClearOuter=22, avgChoices=13.714, deps=1.66, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=direct_polish_clean_19_direct_polish_normal_rect_section_unlock_g2_v03
- L436: score=577, H=4, chains=194, opening=26, earlyAvg=25.4, directClearOuter=26, avgChoices=13.876, deps=1.554, ops=redirect_outer_heads|wrap_boundary_straights|gate_opening_choices|inject_region_gate, id=direct_advanced_25_direct_advanced_veryhard_rect_lock_buckle_g3_v04
- L238: score=577, H=4, chains=106, opening=24, earlyAvg=23.3, directClearOuter=24, avgChoices=17.075, deps=1.829, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=r2_outer_strong_005_r2_006_a401_maskonlyb04_07_main_cactus
- L439: score=575, H=4, chains=158, opening=24, earlyAvg=24.8, directClearOuter=24, avgChoices=13.696, deps=1.694, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=direct_pure_topup_07_09_direct_polish_hard_pure_rect_maze_long_chain_g9_v01
- L410: score=573, H=4, chains=139, opening=23, earlyAvg=21.2, directClearOuter=23, avgChoices=15.388, deps=1.664, ops=redirect_outer_heads|gate_opening_choices|inject_region_gate, id=direct_pure_topup_10_13_direct_polish_hard_pure_rect_dual_zone_g11_v09
- L284: score=572, H=4, chains=172, opening=31, earlyAvg=27.1, directClearOuter=31, avgChoices=10.18, deps=1.993, ops=redirect_outer_heads|wrap_boundary_straights|gate_opening_choices|inject_region_gate, id=direct_pure_topup_07_22_direct_polish_hard_pure_rect_lock_buckle_g11_v03

## Interpretation

- HighLeak: best sandbox targets for light/heavy hardening.
- BoundaryLeak is scored once with a capped boundary channel, so edge/direct/straight leaks do not double-count the same freedom.
- DirectEscapeLeak means early no-cost outer exits are likely making the level too sweepable.
- ChoiceExplosion means the player probably has too many simultaneous valid removals.
- WeakDependency means the level stays wide while average unlock delay is low.
- This pass is analysis only; it does not mutate level assets.
