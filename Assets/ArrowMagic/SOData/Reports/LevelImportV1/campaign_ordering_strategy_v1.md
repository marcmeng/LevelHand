# Campaign Ordering Strategy V1

Updated: 2026-06-11

This is the working import strategy for the first 500-level campaign pass. It uses
`level_import_v1_difficulty_manifest.csv` as the single source of truth for candidate
difficulty and structural labels.

## Candidate Pools

| Pool | Count | Intended Use |
|---|---:|---|
| `normal_campaign500` | 500 | Main campaign backbone. These are already balanced procedural levels. |
| `shape_final_with_supplement` | 147 | Visual novelty levels with readable silhouettes. Insert as special-shape spice. |
| `shape_early_prop` | 4 | Very early satisfying icon/prop levels. |
| `hole_mask_early_front` | 70 | Hole/blocker mechanic levels. Introduce after players understand normal clearing. |

## Difficulty V1 Distribution

| Difficulty | Count | Meaning |
|---|---:|---|
| `Flow` | 166 | Satisfying, open, low-friction clearing. Good for onboarding and relief. |
| `Normal` | 347 | Mainline readable puzzle pressure. |
| `Hard` | 157 | Clear dependencies or blocker pressure, but still fair. |
| `VeryHard` | 41 | Dense or constrained levels for late-stage spikes. |
| `Extreme` | 10 | Highest-pressure levels. Use sparingly near late chapter peaks. |

`Bottleneck` is not a difficulty tier in this version. Bottleneck-like behavior is
tracked through `pressureTag`, `paceTag`, `reasonTags`, and `riskTags`.

## Recommended 500-Level Rhythm

Use `normal_campaign500` as the backbone, then replace selected slots with Shape or
Hole levels of a matching or slightly lower difficulty.

| Range | Flow | Normal | Hard | VeryHard | Extreme | Notes |
|---|---:|---:|---:|---:|---:|---|
| 1-50 | 40% | 55% | 5% | 0% | 0% | Teach clearing rhythm. Shape props can appear here. |
| 51-150 | 25% | 60% | 15% | 0% | 0% | Start adding readable lock/section structure. |
| 151-300 | 15% | 55% | 25% | 5% | 0% | First real difficulty ramp. Introduce Hole gradually. |
| 301-420 | 10% | 45% | 30% | 13% | 2% | Regular challenge with relief levels after spikes. |
| 421-500 | 8% | 35% | 32% | 20% | 5% | Late-game pressure, but still avoid long streaks of high-pressure levels. |

## Insertion Rules

- Shape levels should be treated as novelty, not automatic difficulty spikes.
- Hole levels should not appear before the player has seen enough normal dependency play.
- Avoid placing two visually similar Shape themes too close together.
- Avoid placing two `HighPressure` levels back to back unless the previous level is `Flow`.
- Prefer `Flow` or easy `Normal` after `VeryHard` and `Extreme`.
- Keep `riskTags != NoRisk` out of the first import unless manually approved.

## Tags To Use For Filtering

- `difficultyTagV1`: primary campaign difficulty tier.
- `pressureTag`: opening pressure, bottleneck pressure, low-clear pressure, fake-choice pressure.
- `paceTag`: flow, steady, grind, bottleneck, spike.
- `startTag`: whether the opening is wide, medium, narrow, or fake-wide.
- `clearTag`: whether clearing feels bursty, rhythmic, tight, or grindy.
- `choiceTag`: whether decisions are readable, tactical, simple, many-good, or fake-many.
- `shapeTag`: square, tall, wide, special shape, or hole blocker.
- `visualTag`: sparse, medium, dense, or chaotic.
- `noveltyTag` / `mechanicTag`: standard, shape, or hole/blocker identity.
- `riskTags`: quality or placement risk. Treat as exclusion by default for V1 import.

## Next Step

Generate a campaign import candidate list that:

1. Starts from `normal_campaign500`.
2. Removes or parks `riskTags != NoRisk` unless a level is intentionally kept.
3. Inserts Shape and Hole levels into matching difficulty slots.
4. Enforces anti-repeat rules for difficulty, visual density, theme, mechanic, and pressure.
5. Outputs an ordered CSV first, then builds the final LevelPack only after review.
