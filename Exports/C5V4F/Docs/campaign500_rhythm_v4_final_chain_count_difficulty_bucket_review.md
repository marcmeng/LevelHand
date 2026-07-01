# Campaign500 Rhythm V4 Final Chain Count Difficulty-Bucket Review

## Method

- Uses actual LevelDefinition chain counts from current and initial packs.
- Groups normal slots by sectionWaveRole bucket instead of averaging all 10 levels together.
- Bucket mapping: Normal + Recovery = Ordinary, Hard = Hard, PeakExtreme = Peak.

## Front100 Verdict

- Front100 is acceptable to freeze for now, but it is not strictly monotonic per bucket.
- Ordinary rises from section1 to section3, then stays in a softer 57-71 band through section10.
- Hard rises into the 80-90 band after section3, with some fluctuation but no collapse.
- Peak rises into the 80-100 band after section2, with section9 softer at 73 but still above ordinary.
- Front100 does not have the pathological mixed-average issue seen later; hard/peak generally remain above ordinary.

## Front100 Bucket Values

| Section | Ordinary | Hard | Peak |
| --- | ---: | ---: | ---: |
| 1 | 32.0 | 46.0 | 52.0 |
| 2 | 46.0 | 56.0 | 79.0 |
| 3 | 66.8 | 89.0 | 90.0 |
| 4 | 70.2 | 72.0 | 86.0 |
| 5 | 67.0 | 81.5 | 84.0 |
| 6 | 71.0 | 75.0 | 82.0 |
| 7 | 63.8 | 87.5 | 82.0 |
| 8 | 63.0 | 83.0 | 79.0 |
| 9 | 57.7 | 86.7 | 73.0 |
| 10 | 57.8 | 91.0 | 100.0 |

## Full-Campaign Problem

- Sections21-30 fail per-bucket rhythm. This is not just averaging 50 and 80 together; every bucket drops.
- Section29 values: Ordinary 34.0, Hard 34.0, Peak 29.0.
- Section30 values: Ordinary 33.4, Hard 17.0, Peak 15.0.
- Initial section29/30 expected much higher: section29 Ordinary 108.8, Hard 143.5, Peak 214; section30 Ordinary 109, Hard 163, Peak 178.
- The main cause remains SeedV5LongChain30Front300 rows placed too late.

## Output CSVs

- campaign500_rhythm_v4_final_chain_count_by_section_difficulty_bucket.csv
- campaign500_rhythm_v4_final_chain_count_section_bucket_wide.csv
- campaign500_rhythm_v4_final_chain_count_by_section_wave_role.csv
- campaign500_rhythm_v4_final_chain_count_wave_role_band_trend.csv
