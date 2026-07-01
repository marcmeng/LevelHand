# Campaign500 Rhythm V5 Front100 - v12 Final Review Notes
Status: review-only placement locked from v11. No Unity assets or packs were changed by this step.
Decision:
- Use the v11 per-level rhythm placement as the accepted version.
- Order 17 is a hole slot, so it is kept unchanged at projectedChain=37 / canvasAdjustedChain=37.
- Difficulty labels are inherited from the original 500-row template: 1=??, 2=??, 3=????, 4=??. This preserves the original template ratio exactly.
- hythmBucket remains the local production/rhythm role used by the rebalance model and should not replace the template difficultyCode.
500 difficulty-code summary:

templateDifficultyCode difficultyLabel difficultyTag count ratio policy                          
---------------------- --------------- ------------- ----- ----- ------                          
1                      ??1             Normal1       314   0.628 inherited_from_original_template
2                      ??2             Hard2         94    0.188 inherited_from_original_template
3                      ????3           SpecialHard3  59    0.118 inherited_from_original_template
4                      ??4             Extreme4      33    0.066 inherited_from_original_template



Front20 locked layout:

order category difficultyLabel rhythmBucket projectedChain canvasAdjustedChain projectionReason  linkedOrder sourcePool                          finalReviewNote                                              
----- -------- --------------- ------------ -------------- ------------------- ----------------  ----------- ----------                          ---------------                                              
    1 normal   ??1             Ordinary     6              6                   keep_current                                                                                                                   
    2 hole     ??1             HoleRescue   22             22                  keep_current                                                                                                                   
    3 normal   ??1             Ordinary     15             15                  v11_review_target 290         SeedV5LongChain30Front300           s1 ordinary 36->15                                           
    4 shape    ??1             Shape        55             55                  keep_current                  ShapeMaskInventory                                                                               
    5 normal   ??1             Ordinary     21             21                  v11_review_target 272         SeedV5LongChain30Front300           s1 ordinary 36->21                                           
    6 shape    ??1             Shape        53             53                  keep_current                  ShapeMaskInventory                                                                               
    7 normal   ??1             Ordinary     24             24                  v11_review_target 286         SeedV5LongChain30Front300           s1 ordinary 50->24                                           
    8 normal   ??1             Hard         35             35                  v11_review_target 204         SeedV5LongChain30Front300           s1 hard 46->35                                               
    9 shape    ??1             Shape        34             34                  keep_current                  ShapeMaskInventory                                                                               
   10 normal   ??2             Peak         38             38                  v11_review_target 295         Campaign500_HardGateUntil0910       s1 no real peak pressure 52->38                              
   11 normal   ??1             Ordinary     29             29                  v11_review_target 284         SeedV5LongChain30Front300           s2 ordinary 34->29                                           
   12 normal   ??1             Ordinary     34             35                  v11_review_target 148         SeedV5LongChain30Front300           s2 ordinary 48->34                                           
   13 shape    ??1             Shape        60             60                  keep_current                  ShapeMaskInventory                                                                               
   14 normal   ??1             Ordinary     36             36                  v11_review_target 104         SeedV5LongChain30Front300           s2 ordinary 54->36                                           
   15 normal   ??1             Ordinary     38             38                  v11_review_target 297         Campaign500_HardGateUntil0910       s2 ordinary 55->38                                           
   16 normal   ??1             Hard         50             50                  v11_review_target 134         Campaign500NormalFullV1UnusedStrict s2 hard 56->50                                               
   17 hole     ??1             HoleRescue   37             37                  keep_current                                                      final: kept as original hole slot; no chain downshift applied
   18 normal   ??1             Ordinary     39             39                  v11_review_target 289         Campaign500_HardGateUntil0910       s2 ordinary 47->39                                           
   19 normal   ????3           Peak         60             66                  v11_review_target 227         NutationLongChainStrict60Front300   s2 order19 benchmark 79->60                                  
   20 normal   ??1             Ordinary     40             40                  v11_review_target 291         Campaign500_HardGateUntil0910       s2 ordinary 38->40                                           



Key files:
- campaign500_rhythm_v5_front100_rebalance_plan_v12_final_review_only.csv
- campaign500_rhythm_v5_front100_v12_500_per_level_layout_annotated.csv
- campaign500_rhythm_v5_front100_v12_front100_per_level_layout_annotated.csv
- campaign500_rhythm_v5_front100_v12_front20_per_level_layout_annotated.csv
- campaign500_rhythm_v5_front100_v12_difficulty_code_summary_500.csv
- campaign500_rhythm_v5_front100_v12_section10_difficulty_mix.csv
