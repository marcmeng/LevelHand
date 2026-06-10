# Shape Experiment Notes

## Goal

Shape levels are special silhouette levels for the 500-level progression. This batch is experimental: generate solid, readable masks first, then run seed matching and keep the best candidate per shape.

## Rules

- Use solid silhouette masks only.
- Do not include hole/blocker-style masks in this line.
- Avoid thin or highly detailed outlines.
- Prefer shapes that remain recognizable after arrow refill and repair.
- For each shape, generate up to 3 candidates and keep 1 final level.
- Early shapes should be small and simple. Later shapes can use larger canvases, but should still stay readable on a vertical phone layout.

## Current Mask Batch

- Output folder: `Assets/ArrowMagic/Masks/ShapeExperiment`
- Catalog: `Assets/ArrowMagic/SOData/Reports/ShapeExperiment/shape_experiment_mask_catalog.csv`
- Contact sheet: `Assets/ArrowMagic/SOData/Reports/ShapeExperiment/shape_experiment_mask_contact_sheet.png`

Stage distribution:

- Early: 8 masks
- Mid: 10 masks
- MidLate: 8 masks
- Late: 4 masks

## Candidate Generation Plan

1. For each mask, search matching seeds from the final source packs.
2. Generate up to 3 candidates using mask-crop plus repair/refill.
3. Reject candidates with large visible holes, broken silhouettes, excessive tiny edge chains, or unclear shape identity.
4. Prefer candidates with clean outer contour, good opening moves, visible dependency flow, and smooth ending.
5. Pick one final candidate per accepted shape.

## Notes For Later

- Hole masks belong to a separate future type where empty areas become blockers.
- Shape masks should not use that rule.
- If a shape needs internal detail to be recognizable, skip it for this line instead of adding hole blockers.

## Deprecated Experiments

- 2026-06-10: `ShapeExperimentPreviewPack` was archived to `Deprecated`. The first 10 small masks were technically solvable, but the in-game silhouettes were not readable enough. Do not use them as production candidates.
- 2026-06-10: `OriginalStarMaskPreviewPack` remains the current readable baseline for shape-mask experiments.
- 2026-06-10: `ReadableShapePreviewPack` was reduced to the accepted 3-level view: original `19x19` star, `28x28` large star, and `28x30` house. The `20x20` heart and `21x21` gem were rejected because ordinary silhouettes are still hard to read at that size.
- 2026-06-10: `EarlySymbolPreviewPack` adds three front-stage super symbols: `19x19` plus, `21x21` x, and `20x22` lightning, with original `19x19` star as baseline. These are meant to test whether abstract high-contrast silhouettes can work before the safer `28x28+` size tier.
- 2026-06-10: Straight-dominated generated levels should be rejected before entering preview/final packs. Batch generators now compute straight-chain metrics after generation and skip candidates with too many straight or long-straight chains. Reports include `QualityRejected=True` and `straightness=...` when this happens.
- 2026-06-10: `AnimalShapePreviewPack` adds a 5-level animal/pet preview from `CatHeadLarge`, `DogHeadLarge`, `BunnyHeadLarge`, `FishLarge`, and `PawBold`. `TurtleLarge` was generated but rejected by the straight-dominated filter, so wide shell-like masks need either better source matching or a different silhouette strategy.
- 2026-06-10: Manual animal review rejected `CatHeadLarge` and `PawBold`; the preview pack was trimmed to `DogHeadLarge`, `BunnyHeadLarge`, and `FishLarge`. `BunnyHeadLarge` was visually upside down in game, so the next animal mask set was rebuilt as larger portrait silhouettes: `DogSittingTall`, `BunnyTall`, `PenguinTall`, `GiraffeTall`, `HorseHeadTall`, and `FoxSittingTall`.
- 2026-06-10: Large animal generation now tries the next matched seed candidate when a generated candidate fails quality checks. The checks currently reject straight-dominated output and mask fill below 0.90. `DogSittingTall`, `BunnyTall`, and `HorseHeadTall` are in the preview pack; `PenguinTall`, `GiraffeTall`, and `FoxSittingTall` were slow/high-risk and should be retried separately with smaller masks or a stricter seed strategy.
- 2026-06-10: Animal mask readability should be approved before expensive level generation. The front-facing dog/horse-style silhouettes were too generic after generation, so the next mask set switched to side-view cartoon silhouettes: `DogSitSide`, `CatSitSide`, `BunnyTall`, `DuckSide`, `FishSideLarge`, and `ElephantSide`. Dog uses a side sitting profile with snout, tail, legs, and floppy ear.
- 2026-06-10: Side-view animal generation produced a 4-level `AnimalShapePreviewPack`: `DogSitSide`, `CatSitSide`, `DuckSide`, and `FishSideLarge`. `ElephantSide` reached a feasible Top1 but stalled in final repair/generation, and `BunnyTall` was not reached in that run; both should be retried separately with reduced size or a more constrained seed shortlist.
- 2026-06-10: Fit-to-canvas animal masks produced a new 4-level `AnimalShapePreviewPack` and attached it to Demo. Results: `DogSitSide` 882/910 fill, 112 chains; `CatSitSide` 840/875 fill, 116 chains; `DuckSide` 725/765 fill, 114 chains after Top1 failed and Top2 succeeded; `FishSideLarge` 677/719 fill, 100 chains. Visual occupancy should be better, but Dog/Cat generation became expensive because the masks were stretched to large 42x42/40x44 canvases. Next animal iteration should preserve natural animal proportions and reduce/crop the mask canvas around the silhouette instead of stretching the animal to fill a large canvas.
- 2026-06-10: `AnimalBestPreview` mask-only pass adds three strong-feature, low-detail silhouettes for review before generation. The compact vertical-game-friendly version uses `WhaleBoldSide` 32x28 area 449, `TurtleBoldSide` 32x28 area 455, and `SnailBoldSide` 30x28 area 450. These keep the mask area around 450 cells and avoid the overly wide Dog/Cat style while preserving broad, readable silhouettes.
- 2026-06-10: `AnimalBestPreviewPack` was generated from the compact masks and attached to Demo. All three accepted on MatchTop1: whale 428/449 fill, 72 chains; turtle 418/455 fill, 68 chains; snail 433/450 fill, 61 chains. This pass is much faster and better matched to vertical gameplay than the large Dog/Cat masks.
- 2026-06-10: `AnimalBestPreviewPack` was tight-cropped by mask/content bounds and reattached to Demo using the same pack path. Final preview levels are whale 32x26, turtle 32x24, and snail 29x22; no arrows were dropped, all three pass Greedy, and coverage improved from 0.478/0.467/0.515 to 0.514/0.544/0.679. This fixes the visible empty top/bottom and side canvas from the earlier animal pass.
