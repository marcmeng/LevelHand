# Campaign500 Rhythm V5 Front100 Import Notes

This package is a complete 500-level import surface for the accepted Rhythm V5 Front100 layout.

## What changed

- V5_FRONT100_TARGET_REPLACED: 52 rows. These are the front100 positions intentionally changed by the v12 rhythm plan.
- V5_DONOR_REVERT_FOR_UNIQUENESS: 52 rows. These are donor positions reverted so the front100 borrowed candidates are not duplicated elsewhere.
- KEPT_C5V4FSC_REPLACEMENT: rows already replaced by the previous strict-complete baseline and kept here.
- KEPT_ORIGINAL_TEMPLATE: rows identical to the original template source asset.

Relative to current C5V4FSC strict-complete baseline, changed rows: 104.
Relative to the original template source asset paths, changed rows: 336.

## Difficulty labels

Difficulty code is inherited from the original 500-row template:

- 1 = normal/recovery
- 2 = hard
- 3 = special hard
- 4 = extreme

The local hythmBucket field is only for pacing/audit and does not replace the template difficulty code.

## Key files

- IMPORT_README.md: copy/import instructions.
- Docs/campaign500_rhythm_v5_front100_per_level_config_index.csv: exact asset path and GUID for every order.
- Docs/campaign500_rhythm_v5_front100_template_replacement_diff.csv: original/base/final path comparison and replacement mark.
- Docs/campaign500_rhythm_v5_front100_sync_asset_inventory.csv: unique LevelDefinition assets included in the package.
- Docs/campaign500_rhythm_v5_front100_sync_file_list.csv: every copied file.

## Validation

- Pack level refs: 500
- Unique LevelDefinition assets copied: 467
- Missing assets: 0
- Missing metas: 0
