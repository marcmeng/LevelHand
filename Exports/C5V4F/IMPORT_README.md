# Campaign500 Rhythm V4 Final Sync Package

## What this contains

- Final playable pack: Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500FirstAssemblyRhythmV4FinalPreviewPack.asset
- Pack GUID: 6314e181422e089488110d180f31b44f
- 500 per-order level config index: Docs/campaign500_rhythm_v4_final_per_level_config_index.csv
- Template replacement diff: Docs/campaign500_rhythm_v4_final_template_replacement_diff.csv
- Template replacement summary: Docs/campaign500_rhythm_v4_final_template_replacement_summary.csv
- Arrow count audit: Docs/campaign500_rhythm_v4_final_arrow_count_compare_vs_initial.csv
- Arrow count audit summary: Docs/campaign500_rhythm_v4_final_arrow_count_audit_summary.md
- Chain count section summary: Docs/campaign500_rhythm_v4_final_chain_count_section10_summary.csv
- Normal-only chain count section summary: Docs/campaign500_rhythm_v4_final_normal_chain_count_section10_summary.csv
- Chain count audit summary: Docs/campaign500_rhythm_v4_final_chain_count_audit_summary.md
- Front100 chain rhythm review: Docs/campaign500_rhythm_v4_final_front100_chain_rhythm_review.md
- Chain count difficulty-bucket review: Docs/campaign500_rhythm_v4_final_chain_count_difficulty_bucket_review.md
- Chain count by difficulty bucket: Docs/campaign500_rhythm_v4_final_chain_count_by_section_difficulty_bucket.csv
- Unique Unity assets copied under U/Assets/...
- Audit reports and final placement CSVs under Docs/Exports/...

## Import method

1. In the target Unity project, make sure the ArrowMagic scripts/schema are already present and compatible.
2. Copy the contents of U/Assets into the target project Assets folder, preserving the relative paths.
3. Keep every .asset.meta next to its .asset; the pack references level configs by GUID.
4. Use this pack in the target project: Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500FirstAssemblyRhythmV4FinalPreviewPack.asset.

## Counts

- Per-level rows: 500
- Unique LevelDefinition assets: 453
- Unity .asset files including pack: 454
- Unity .meta files: 454
- Load modes: BasePreviewFallback=167; ManifestAsset=333
- Missing assets: 0
- Missing metas: 0

## Notes

campaign500_rhythm_v4_final_per_level_config_index.csv is the main per-level map. loadedAssetPath is the actual LevelDefinition used by the final pack for that order. Some rows are base preview fallbacks by design, but their actual LevelDefinition assets are included in this package.

Folder .meta files are intentionally not required for the pack references; preserve the file .meta files for the pack and every LevelDefinition.
