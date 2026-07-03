# Campaign500 Slot Spec V2 Gap Summary

This summarizes demand implied by the locked V2 spec. It is not a candidate inventory.

| phase | duty | production need | count | avg load | range |
| --- | --- | --- | ---: | ---: | --- |
| after300 | BridgeRamp | normal_pool_available_match_by_band | 11 | 109.5 | 105-112 |
| after300 | CanvasStep | partial_supply_need_normalplus_readcheck | 7 | 121.6 | 113-128 |
| after300 | DependencyPeak | needs_hard_or_special_peak_supply | 25 | 137.8 | 129-146 |
| after300 | ExtremeMemory | needs_extreme_peak_supply | 16 | 153.7 | 144-160 |
| after300 | HoleSpatialAnchor | use_existing_hole_anchor_pool | 20 | 103.9 | 94-112 |
| after300 | LanguageVariation | normal_pool_available_match_by_band | 11 | 105.9 | 102-110 |
| after300 | LocalRunBreaker | partial_supply_need_normalplus_readcheck | 3 | 128.7 | 127-130 |
| after300 | NormalPractice | normal_pool_available_match_by_band | 21 | 107.3 | 98-112 |
| after300 | ReadCheck | partial_supply_need_shape_readcheck | 19 | 125.8 | 118-130 |
| after300 | RecoveryFlow | normal_pool_available_match_by_band | 20 | 99.2 | 94-104 |
| after300 | SetupOrBreath | normal_pool_available_match_by_band | 26 | 110.9 | 104-112 |
| after300 | ShapeAnchor | use_existing_shape_anchor_pool | 20 | 109.3 | 104-112 |
| after300 | StylePeak | needs_underlying_hard_supply_shape_cameo | 1 | 146 | 146-146 |
| front100 | BridgeRamp | normal_pool_available_match_by_band | 7 | 66.1 | 53-81 |
| front100 | CanvasStep | partial_supply_need_normalplus_readcheck | 9 | 82.6 | 65-103 |
| front100 | DependencyPeak | needs_hard_or_special_peak_supply | 4 | 87 | 78-94 |
| front100 | ExtremeMemory | needs_extreme_peak_supply | 5 | 99.4 | 84-114 |
| front100 | HoleSpatialAnchor | use_existing_hole_anchor_pool | 8 | 61.8 | 46-72 |
| front100 | LanguageVariation | normal_pool_available_match_by_band | 4 | 57.2 | 44-70 |
| front100 | LocalRunBreaker | partial_supply_need_normalplus_readcheck | 3 | 80.7 | 62-92 |
| front100 | NormalPractice | normal_pool_available_match_by_band | 5 | 63.8 | 41-82 |
| front100 | ReadCheck | partial_supply_need_normalplus_readcheck | 1 | 80 | 80-80 |
| front100 | ReadCheck | partial_supply_need_shape_readcheck | 9 | 86.3 | 75-106 |
| front100 | RecoveryFlow | normal_pool_available_match_by_band | 7 | 52.9 | 36-66 |
| front100 | SetupOrBreath | normal_pool_available_match_by_band | 6 | 75.7 | 58-92 |
| front100 | ShapeAnchor | use_existing_shape_anchor_pool | 7 | 60.7 | 42-83 |
| front100 | StylePeak | needs_hard_or_special_peak_supply | 5 | 101.4 | 93-110 |
| front20 | BridgeRamp | normal_pool_available_match_by_band | 1 | 50 | 50-50 |
| front20 | CanvasStep | normal_pool_available_match_by_band | 2 | 33.5 | 23-44 |
| front20 | DependencyPeak | needs_hard_or_special_peak_supply | 1 | 61 | 61-61 |
| front20 | HoleSpatialAnchor | use_existing_hole_anchor_pool | 2 | 25.5 | 13-38 |
| front20 | LanguageVariation | normal_pool_available_match_by_band | 1 | 40 | 40-40 |
| front20 | NormalPractice | normal_pool_available_match_by_band | 3 | 34 | 29-37 |
| front20 | ReadCheck | partial_supply_need_normalplus_readcheck | 3 | 39.7 | 35-46 |
| front20 | RecoveryFlow | normal_pool_available_match_by_band | 2 | 37.5 | 33-42 |
| front20 | ShapeAnchor | use_existing_shape_anchor_pool | 4 | 32.2 | 25-38 |
| front20 | TutorialRule | keep_original | 1 | 6 | 6-6 |
| front300 | BridgeRamp | normal_pool_available_match_by_band | 15 | 95.9 | 84-106 |
| front300 | CanvasStep | partial_supply_need_normalplus_readcheck | 13 | 107.8 | 97-120 |
| front300 | DependencyPeak | needs_hard_or_special_peak_supply | 15 | 123.8 | 111-139 |
| front300 | ExtremeMemory | needs_extreme_peak_supply | 12 | 142.8 | 132-152 |
| front300 | HoleSpatialAnchor | use_existing_hole_anchor_pool | 19 | 90.2 | 72-104 |
| front300 | LanguageVariation | normal_pool_available_match_by_band | 9 | 90.9 | 81-101 |
| front300 | LocalRunBreaker | partial_supply_need_normalplus_readcheck | 2 | 106 | 100-112 |
| front300 | NormalPractice | normal_pool_available_match_by_band | 18 | 93.9 | 76-108 |
| front300 | ReadCheck | partial_supply_need_normalplus_readcheck | 5 | 115.4 | 107-122 |
| front300 | ReadCheck | partial_supply_need_shape_readcheck | 22 | 116.1 | 98-130 |
| front300 | RecoveryFlow | normal_pool_available_match_by_band | 21 | 84.9 | 73-96 |
| front300 | SetupOrBreath | normal_pool_available_match_by_band | 23 | 102.5 | 86-108 |
| front300 | ShapeAnchor | use_existing_shape_anchor_pool | 18 | 95.2 | 83-108 |
| front300 | StylePeak | needs_hard_or_special_peak_supply | 8 | 115.9 | 104-127 |

## Main Reading

- Normal and bridge slots can mostly use existing Flow/Peel/Mixed pools if they match effective load and repetition rules.
- ReadCheck and LocalRunBreaker have only partial supply. Current choke candidates are useful as NormalPlus examples, not hard peaks.
- DependencyPeak, StylePeak, and ExtremeMemory are the real production gaps. They should be generated from explicit hard-read/choke-rich lanes.
- Shape-hosted peak slots should be supplied by the same hard-read/choke-rich lanes with shape as a theme lens, not by shape geometry alone.
