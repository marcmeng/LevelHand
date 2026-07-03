# Campaign500 First50 V2 Existing Resource Layout V1

- Scope: first 50 levels only.
- Source: existing C5V4FSC resources; no new generation.
- Layout CSV: `Exports/Campaign500_First50_V2Existing_20260702/campaign500_first50_v2_existing_layout_v1.csv`
- Match report: `Exports/Campaign500_First50_V2Existing_20260702/campaign500_first50_v2_existing_match_report_v1.csv`
- Resource check: `Exports/Campaign500_First50_V2Existing_20260702/campaign500_first50_v2_existing_resource_check_v1.csv`
- Missing assets/metas: `0`
- Rows changed from same V4 order: `32`
- Effective load avg/min/max: `52.1/6.0/92.0`

## Difficulty Counts

- `1`: `36`
- `2`: `9`
- `3`: `3`
- `4`: `2`

## Category Counts

- `hole`: `5`
- `normal`: `35`
- `shape`: `10`

## Slot Duty Counts

- `BridgeRamp`: `4`
- `CanvasStep`: `6`
- `DependencyPeak`: `3`
- `ExtremeMemory`: `2`
- `HoleSpatialAnchor`: `5`
- `LanguageVariation`: `3`
- `LocalRunBreaker`: `1`
- `NormalPractice`: `5`
- `ReadCheck`: `6`
- `RecoveryFlow`: `5`
- `SetupOrBreath`: `2`
- `ShapeAnchor`: `7`
- `TutorialRule`: `1`

## Band Fit

- `in_band`: `30`
- `out_of_band`: `4`
- `theme_anchor_unscored_load`: `16`

## Notes

- Shape slots preserve theme/perspective first and use raw chains only as an anchor; hole slots preserve spatial contrast.
- Normal slots are matched by V2 duty, effective-load band, style/language hints, and existing quality status.
- This layout is a rehearsal for the full 500 flow, not a final front50 decision.
