# Campaign500 Shape Second Pass Stats - Post20

- Source: `Exports/Campaign500SingleLevels_Hole10V4_20260613_095022/ShapeUsage/campaign500_shape_usage.csv`
- Scope: shape levels with order > 20
- Count: `96`
- Order range: `23-498`
- Chains min/avg/max: `80/113.1/207`
- MaskCoverage min/avg/max: `0.480/0.603/0.889`
- MaskFill min/avg/max: `0.964/0.980/0.992`
- MaskArea min/avg/max: `615/867.8/1599`

## Risk Counts
- LikelyReadable: 77
- NeedsVisualCheck: 19

## Theme Counts
- Art: 3
- Character: 6
- Landmark: 14
- Magic: 16
- Music: 5
- Nature: 7
- Object: 7
- Ocean: 15
- Space: 6
- Symbol: 8
- ToolUI: 4
- Vehicle: 5

## Role Counts
- character: 10
- creature: 11
- equipment: 3
- landmark: 15
- natural: 6
- object: 34
- symbol: 8
- vehicle: 9

## Difficulty Band Counts
- big: 6
- main: 68
- transition: 22

## Per 100 Levels
| Segment | ShapeCount | LikelyReadable | NeedsVisualCheck | HighRisk |
|---|---:|---:|---:|---:|
| 001-100 | 16 | 15 | 1 | 0 |
| 101-200 | 20 | 17 | 3 | 0 |
| 201-300 | 20 | 16 | 4 | 0 |
| 301-400 | 20 | 15 | 5 | 0 |
| 401-500 | 20 | 14 | 6 | 0 |

## High Priority Visual Check Rows
| Level | Expected | MaskName | Theme | Chains | Size | Coverage | Fill | Risk | Reasons |
|---:|---|---|---|---:|---|---:|---:|---|---|
| 87 | paint palette | `MainPaintPaletteTall` | Art | 95 | 30x40 | 0.733 | 0.980 | NeedsVisualCheck | complex_name |
| 113 | magic rune stone | `MainMagicRuneStoneTall` | Magic | 98 | 28x42 | 0.703 | 0.987 | NeedsVisualCheck | abstract_name;complex_name |
| 133 | knight mascot | `MainKnightMascotTall` | Magic | 95 | 30x42 | 0.555 | 0.964 | NeedsVisualCheck | low_fill;complex_name |
| 138 | space comet icon | `MainSpaceCometIconTall` | Space | 96 | 30x42 | 0.537 | 0.982 | NeedsVisualCheck | abstract_name |
| 233 | transition scissors | `TransitionDistinctScissorsTall` | Object | 108 | 34x46 | 0.501 | 0.977 | NeedsVisualCheck | medium_mask_coverage;abstract_name;distinct_shape_hint |
| 243 | astronaut mascot | `MainAstronautMascotTall` | Space | 111 | 30x42 | 0.652 | 0.985 | NeedsVisualCheck | complex_name |
| 253 | space saturn icon | `MainSpaceSaturnIconTall` | Space | 111 | 30x42 | 0.600 | 0.979 | NeedsVisualCheck | abstract_name |
| 293 | transition wide landmark01 | `TransitionWideLandmark01Tall` | Landmark | 119 | 34x50 | 0.532 | 0.976 | NeedsVisualCheck | complex_name |
| 307 | transition ocean sea turtle | `TransitionOceanSeaTurtleTall` | Ocean | 122 | 36x48 | 0.516 | 0.976 | NeedsVisualCheck | medium_mask_coverage |
| 327 | transition wide landmark06 | `TransitionWideLandmark06Tall` | Landmark | 131 | 38x52 | 0.561 | 0.976 | NeedsVisualCheck | complex_name |
| 338 | transition wide landmark05 | `TransitionWideLandmark05Tall` | Landmark | 134 | 36x50 | 0.653 | 0.980 | NeedsVisualCheck | complex_name |
| 353 | transition magic knight helmet | `TransitionMagicKnightHelmetTall` | Magic | 141 | 36x50 | 0.621 | 0.988 | NeedsVisualCheck | complex_name |
| 358 | transition ocean lobster | `TransitionOceanLobsterTall` | Ocean | 137 | 38x50 | 0.514 | 0.972 | NeedsVisualCheck | medium_mask_coverage |
| 453 | bean mascot | `MainBeanMascotTall` | Character | 88 | 30x42 | 0.624 | 0.973 | NeedsVisualCheck | complex_name |
| 458 | pine tree | `MainPineTreeTall` | Nature | 86 | 30x42 | 0.504 | 0.975 | NeedsVisualCheck | medium_mask_coverage |
| 463 | iron tower | `MainIronTowerTall` | Landmark | 86 | 30x44 | 0.480 | 0.984 | NeedsVisualCheck | medium_mask_coverage;complex_name;simple_icon_name |
| 468 | tool wrench | `MainToolWrenchTall` | ToolUI | 84 | 30x42 | 0.494 | 0.987 | NeedsVisualCheck | medium_mask_coverage |
| 473 | pyramid | `MainPyramidTall` | Landmark | 82 | 30x42 | 0.494 | 0.974 | NeedsVisualCheck | medium_mask_coverage |
| 478 | space ufo icon | `MainSpaceUfoIconTall` | Space | 86 | 32x42 | 0.567 | 0.975 | NeedsVisualCheck | abstract_name |