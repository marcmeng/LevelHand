# Campaign500 First Assembly V1 - 2026-07-01

This is a planning/assembly manifest only. It does not copy external worktree assets into main and does not rebuild a Unity pack.

## Policy

- Normal automatic replacements use the current Campaign hard gate.
- Shape inventory candidates are allowed, including manual-review rows, to keep one playable level per shape.
- HubMixed local9 is allowed only as a small late-campaign style injection.
- Missing normal target slots without a confirmed candidate stay on the original V11 level.
- Hole slots are unchanged.

## Counts

- Active planned replacements: 286
- Normal active replacements: 187
- Shape active replacements: 99
- Hole active replacements: 0
- Remaining gaps: 14 total; front300=6; after300=8

## Replacement Sources

- Campaign500_HardGateUntil0910: 145
- Campaign500NormalFullV1ExactVariant: 1
- HubMixedStrict30Refill: 4
- NutationLongChainStrict60: 36
- SeedV5LongChainExact30: 1
- ShapeMaskInventory: 99

## Remaining Gaps By Category

- normal: 13
- shape: 1

## Remaining Normal Gaps By Role

- NormalA_FlowOrNeutral: 7
- NormalB_NeutralOrPeelLight: 6

## Key Files

- campaign500_first_assembly_v1_500_manifest.csv
- campaign500_first_assembly_v1_replacements.csv
- campaign500_first_assembly_v1_remaining_gaps.csv
- campaign500_first_assembly_v1_gap_demand_by_lane.csv
- campaign500_first_assembly_v1_external_asset_copy_queue.csv
- campaign500_first_assembly_v1_optional_normal_shape_overrides.csv
- campaign500_first_assembly_v1_section_summary.csv
- campaign500_first_assembly_v1_hard_peak_ramp.csv

## Notes

Rows with assemblyCopyRequired=True point to assets that currently live in worktrees. They must be copied/imported into main before building a playable first-version pack.

Order 62 is filled by campaign500_normal_full_v1_s01_s13_77_nutation_mixed_neutral_v1_rect_s07_o062_v01_neutralmixed_mixed_chain, an exact same-order NormalFullV1 variant that passes the current Campaign hard gate.

Shape slot 073/windmill has no formal candidate asset in the inventory, so V1 keeps the original V11 level and marks it as a remaining shape gap.
