# Campaign500 Rhythm V4 Final Chain Count Audit

Baseline: Campaign500FinalPack.asset
Current: Campaign500FirstAssemblyRhythmV4FinalPreviewPack.asset

## Key Findings

- Chain count does not gradually rise section by section in the current final pack.
- Sections 1-12 rise into the 70s, then sections 13-20 fluctuate around 69-86.
- Sections 21-30 are the main regression: full-pack section averages drop from 67.6 to 49.5; normal-only averages drop from 58.1 to 28.4.
- Sections 29-30 are the clearest problem: normal-only chain averages are 33.3 and 28.4, with minimum chain counts 15/15.
- After section 31, chain counts jump back to a high band: full-pack averages are mostly 96-127; normal-only averages are mostly 95-136.
- The low chain-count trough is caused mainly by SeedV5LongChain30Front300 rows placed too late. These are long-chain/small-canvas candidates, so they have low chain count despite being structurally long.

## Actionable Conclusion

For the next revision, demote SeedV5LongChain30Front300 rows earlier or replace sections 24-30 with large-canvas strict candidates. The intended 10-level ramp should not use these small-canvas long-chain rows around orders 284-299.

## Output CSVs

- campaign500_rhythm_v4_final_chain_count_section10_summary.csv
- campaign500_rhythm_v4_final_normal_chain_count_section10_summary.csv
