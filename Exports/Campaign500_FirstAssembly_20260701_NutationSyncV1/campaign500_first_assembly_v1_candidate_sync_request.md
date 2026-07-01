# Campaign500 First Assembly V1 - Candidate Sync Request

Generated from campaign500_first_assembly_v1_remaining_gaps.csv.

## Current Gap State

- Normal gaps: 13
- Front300 normal gaps: 5
- After300 normal gaps: 8
- Hard/Peak gaps: 0
- Shape gap: order 73 / windmill has no formal candidate asset; V1 keeps original V11 fallback.
- Order 62 is now filled by exact NormalFullV1 v01 and removed from this request.

## Priority

1. Fill front300 ordinary NormalA/NormalB first, especially sections 21-26.
2. Then fill late ordinary/PeelLight relief slots.
3. Do not produce more Hard/Peak for this first assembly unless replacing a manual-review concern.
4. Keep Campaign hard gate for automatic normal insertion: coverage >= 0.93, maxChoices <= 11, local <= 7, nearOuter <= 6, STS >= 0.82, collapse <= 0.28, sameAxis <= 9, sameDir <= 8.

## Demand By Lane

- NormalA_FlowOrNeutral / FlowPatch / patch_chain / NormalFlow: 2 orders [202, 441]
- NormalB_NeutralOrPeelLight / NeutralMixed / mixed_chain / NormalNeutral: 4 orders [206, 231, 326, 390]
- NormalA_FlowOrNeutral / FlowCurve / curve_chain / NormalFlow: 2 orders [248, 302]
- NormalA_FlowOrNeutral / FlowRail / rail_chain / NormalFlow: 1 orders [254]
- NormalA_FlowOrNeutral / NeutralMixed / mixed_chain / NormalNeutral: 2 orders [397, 431]
- NormalB_NeutralOrPeelLight / PeelLight / curve_chain / NormalPeelLight: 2 orders [446, 462]

## Front300 Gaps

- order 202, section 21, NormalA_FlowOrNeutral, lane=FlowPatch, chain=patch_chain, band=NormalFlow, currentFallback=direct_pure_topup_06_09_direct_normal_pure_rect_outer_shell_g7_v08
- order 206, section 21, NormalB_NeutralOrPeelLight, lane=NeutralMixed, chain=mixed_chain, band=NormalNeutral, currentFallback=direct_normal_topup_04_direct_normal_topup_rect_core_burst_g6_v01
- order 231, section 24, NormalB_NeutralOrPeelLight, lane=NeutralMixed, chain=mixed_chain, band=NormalNeutral, currentFallback=direct_pure_normal_extra_03_08_direct_normal_pure_extra_rect_maze_long_chain_g15_v07
- order 248, section 25, NormalA_FlowOrNeutral, lane=FlowCurve, chain=curve_chain, band=NormalFlow, currentFallback=direct_normal_07_direct_normal_rect_dual_zone_g0_v01
- order 254, section 26, NormalA_FlowOrNeutral, lane=FlowRail, chain=rail_chain, band=NormalFlow, currentFallback=direct_normal_topup_38_direct_normal_topup_rect_stair_push_g10_v05

## After300 Gaps

- order 302, section 31, NormalA_FlowOrNeutral, lane=FlowCurve, chain=curve_chain, band=NormalFlow, currentFallback=direct_normal_topup_51_direct_normal_topup_rect_dense_weave_g6_v07
- order 326, section 33, NormalB_NeutralOrPeelLight, lane=NeutralMixed, chain=mixed_chain, band=NormalNeutral, currentFallback=direct_normal_topup_12_direct_normal_topup_rect_core_burst_g7_v02
- order 390, section 39, NormalB_NeutralOrPeelLight, lane=NeutralMixed, chain=mixed_chain, band=NormalNeutral, currentFallback=direct_pure_topup_08_11_direct_polish_hard_pure_rect_dual_zone_g12_v04
- order 397, section 40, NormalA_FlowOrNeutral, lane=NeutralMixed, chain=mixed_chain, band=NormalNeutral, currentFallback=direct_normal_topup_40_direct_normal_topup_rect_sweep_g10_v05
- order 431, section 44, NormalA_FlowOrNeutral, lane=NeutralMixed, chain=mixed_chain, band=NormalNeutral, currentFallback=direct_normal_54_direct_normal_rect_dense_weave_g5_v06
- order 441, section 45, NormalA_FlowOrNeutral, lane=FlowPatch, chain=patch_chain, band=NormalFlow, currentFallback=direct_normal_topup_48_direct_normal_topup_rect_sweep_g11_v06
- order 446, section 45, NormalB_NeutralOrPeelLight, lane=PeelLight, chain=curve_chain, band=NormalPeelLight, currentFallback=direct_polish_clean_21_direct_polish_normal_rect_dense_weave_g2_v03
- order 462, section 47, NormalB_NeutralOrPeelLight, lane=PeelLight, chain=curve_chain, band=NormalPeelLight, currentFallback=direct_pure_topup_08_19_direct_polish_hard_pure_rect_dense_weave_g13_v05
