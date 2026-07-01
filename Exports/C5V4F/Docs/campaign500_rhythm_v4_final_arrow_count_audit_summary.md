# Campaign500 Rhythm V4 Final Arrow Count Audit

Baseline: Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500FinalPack.asset
Current: Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500FirstAssemblyRhythmV4FinalPreviewPack.asset

## Key Findings

- Current final 500 average arrow cells: 751.3 vs initial 805.4.
- After order 300 average arrow cells: 883.0 vs initial 965.1; min after300 is 535, so there are no after300 levels below 80 arrow cells.
- Replaced after300 levels: 65/65 are large canvas by area >= 700; average arrow cells 954.0, min 578, average chains 109.0.
- Large canvas exists in new candidates: REPLACED rows have 201 large-canvas levels out of 333.
- The visible small-late issue is concentrated before 300, mainly SeedV5LongChain30Front300: 22 rows, 19 small canvas under area 600, 18 rows under 500 arrow cells.
- The most visible small late-front300 rows are 290, 294, 296, 299 with areas 192/192/221/192 and arrow counts 181/181/209/180.

## Output CSVs

- campaign500_rhythm_v4_final_arrow_count_compare_vs_initial.csv
- campaign500_rhythm_v4_final_arrow_count_section10_summary.csv
- campaign500_rhythm_v4_final_arrow_count_by_replacement_mark.csv

## Interpretation

If a late-front300 level looks like only dozens of arrows, the likely observed number is chain count, not arrow-cell count. However, the SeedV5LongChain30Front300 source pool is genuinely too small for its placement stage and should be demoted earlier or replaced with large-canvas strict candidates.
