# Campaign500 PSG Regeneration Template

Generated: 20260626_095625

Source baseline: `Exports/C500V11_Formal_20260618_172819/Docs/campaign500_final_v11_manifest.csv` plus `G:/bm/Excels_ArrawGame/G_关卡.xlsx`.

## Files

- `campaign500_psg_regeneration_template.csv`: main 500-slot template for PSG regeneration.
- `campaign500_psg_template_normal.csv`: normal slots only.
- `campaign500_psg_template_shape.csv`: shape slots only.
- `campaign500_psg_template_hole.csv`: hole/rescue slots only.
- `campaign500_psg_template_section10_summary.csv`: every-10-level pacing summary.
- `source_campaign500_final_v11_manifest.csv`: untouched source manifest snapshot.
- `source_campaign500_final_v11_shape_usage.csv`: untouched shape usage snapshot.

## Slot Counts

- normal: 350
- shape: 100
- hole: 50

## Difficulty Counts From Current Config

- 1 普通/恢复: 314
- 2 困难: 94
- 3 特别困难: 59
- 4 极难: 33

## PSG Use Notes

- Keep `order`, `category`, `difficultyCode`, `experienceRole`, `targetWidth/targetHeight`, and `targetChains` as the regeneration contract.
- `targetChainsMin/Max` is a soft band around the old chain count; use exact `targetChains` when a slot needs strict pacing.
- `shapeId/shapeNameZh/shapeUsageDisplayId` identify the intended visible shape. PSG can regenerate the board, but shape slots should preserve this identity.
- `missionAni*` fields mark hole rescue slots that need the locked animal/image presentation.
- `sourceV11*` columns are references only; PSG regeneration should not depend on old assets unless doing visual comparison.
