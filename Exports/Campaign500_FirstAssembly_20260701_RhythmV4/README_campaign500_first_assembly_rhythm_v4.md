# Campaign500 First Assembly Rhythm V4

BuiltAt: 2026-07-01 11:30:01

V4 changes placement, not generation logic. It reuses the V3 strict candidate set as the quality floor, then reassigns front300 normal rows by original-template chain rhythm, 10-level wave role, long-chain effective load, and style/chain-language diversity.

- Manifest: `Exports/Campaign500_FirstAssembly_20260701_RhythmV4/campaign500_first_assembly_rhythm_v4_500_manifest.csv`
- Front300 plan: `Exports/Campaign500_FirstAssembly_20260701_RhythmV4/campaign500_first_assembly_rhythm_v4_front300_normal_plan.csv`
- Section profile: `Exports/Campaign500_FirstAssembly_20260701_RhythmV4/campaign500_first_assembly_rhythm_v4_section10_profile.csv`
- Front20 comparison: `Exports/Campaign500_FirstAssembly_20260701_RhythmV4/campaign500_first_assembly_rhythm_v4_front20_compare.csv`
- Unused candidate audit: `Exports/Campaign500_FirstAssembly_20260701_RhythmV4/campaign500_first_assembly_rhythm_v4_unused_candidates.csv`

## Key Counts

- Front300 normal rows: 210
- Front300 active normal replacements: 209
- Front300 kept current: 1 (order 1 is intentionally kept current)
- Candidate pool rows: 210
- Unused candidates: 1
- Remaining gaps inherited after300/non-normal: 9

## Front20 Softening

- Front20 normal rows: 14
- Front20 active replacements: 13
- Front20 actual chains avg/min/max: 47.3/34/63
- Front20 effective load avg/min/max: 49.4/36.5/65.3
- Order 1: KeepCurrent/base preview fallback; no generated candidate is assigned.

## Front300 Mix

- Style mix: flow=36; hardgate=61; hub=4; longchain=47; neutral=45; peel=16
- Chain-language mix: choice_clamp_chain=5; curve_chain=22; family_profile_chain=22; long_chain=1; mixed_chain=63; patch_chain=17; rail_chain=50; spine_chain=17; strict_hard_chain=3; strict_peak_chain=5; unknown_chain=4
- Source pool mix: Campaign500NormalFullV1UnusedStrict=14; Campaign500_HardGateUntil0910=144; ExternalHardGateDedupFront300=2; HubMixedRefill30Front300=3; NutationLongChainStrict60Front300=24; SeedV5LongChain30Front300=22

## Notes

The strict candidate pool currently has a minimum generated chain count around the mid-30s, so very early template targets like 6/19 chains cannot be matched exactly without either keeping originals or producing a dedicated early-game low-chain strict lane. V4 keeps the first level and places the lowest effective-load Flow/Neutral candidates in the remaining early normal slots.
