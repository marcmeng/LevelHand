# Campaign500 Rhythm Audit V1

- Layout: `F:\Unityproject\ArrowLevel-Hand\Exports\C5V5F100\Docs\campaign500_rhythm_v5_front100_per_level_config_index.csv`
- Score: **68 / 100**
- Grade: **C**
- Findings: 389 total, 0 errors, 150 warnings, 239 infos
- Sections below 80: 5

## Interpretation

This is a static pacing audit. It does not replace Unity playtest or solver trace, but it catches obvious rhythm issues: early pressure spikes, weak hard/peak contrast, missing post-peak release, source/style repetition, and chain-count overgrowth.

## Lowest Section Scores

| section | levels | score | avg | normal | hard | peak | top risk |
| --- | --- | ---: | ---: | ---: | ---: | ---: | --- |
| 19 | 181-190 | 76 | 72.7 | 78.8 | 75.0 | 22.0 | hard avg 75.0 is not clearly above normal avg 78.8.; peak avg 22.0 is not clearly above normal avg 78.8. |
| 31 | 301-310 | 76 | 100.1 | 104.6 | 118.0 | 87.5 | peak avg 87.5 is not clearly above normal avg 104.6.; section avg jumps +29.7 from previous section. |
| 38 | 371-380 | 76 | 108.7 | 119.0 | 79.0 | 89.0 | hard avg 79.0 is not clearly above normal avg 119.0.; peak avg 89.0 is not clearly above normal avg 119.0. |
| 46 | 451-460 | 76 | 109.1 | 116.5 | 81.0 | 124.0 | hard avg 81.0 is not clearly above normal avg 116.5.; peak avg 124.0 is not clearly above normal avg 116.5. |
| 47 | 461-470 | 76 | 105.7 | 126.2 | 89.0 | 104.0 | hard avg 89.0 is not clearly above normal avg 126.2.; peak avg 104.0 is not clearly above normal avg 126.2. |
| 10 | 91-100 | 88 | 86.4 | 82.0 | 87.5 | 110.5 | hard avg 87.5 is not clearly above normal avg 82.0. |
| 12 | 111-120 | 88 | 81.0 | 86.0 | 94.0 | 67.5 | peak avg 67.5 is not clearly above normal avg 86.0. |
| 13 | 121-130 | 88 | 78.5 | 76.6 | 88.0 | 89.0 | peak avg 89.0 is not clearly above normal avg 76.6. |
| 15 | 141-150 | 88 | 81.8 | 81.0 | 111.0 | 69.0 | peak avg 69.0 is not clearly above normal avg 81.0. |
| 16 | 151-160 | 88 | 88.1 | 81.3 | 92.8 | 86.5 | peak avg 86.5 is not clearly above normal avg 81.3. |
| 17 | 161-170 | 88 | 85.0 | 81.0 | 118.5 | 73.0 | peak avg 73.0 is not clearly above normal avg 81.0. |
| 18 | 171-180 | 88 | 78.4 | 72.8 | 106.0 | 69.0 | peak avg 69.0 is not clearly above normal avg 72.8. |

## Top Warnings

- **WARN L4 `early_jump`**: chain jumps +40 from L3; early sections prefer smoother rise.
- **WARN L23 `early_jump`**: chain jumps +44 from L22; early sections prefer smoother rise.
- **WARN L38 `early_jump`**: chain jumps +47 from L37; early sections prefer smoother rise.
- **WARN L47 `early_jump`**: chain jumps +51 from L46; early sections prefer smoother rise.
- **WARN L72 `early_jump`**: chain jumps +47 from L71; early sections prefer smoother rise.
- **WARN L83 `early_jump`**: chain jumps +46 from L82; early sections prefer smoother rise.
- **WARN L96 `early_jump`**: chain jumps +39 from L95; early sections prefer smoother rise.
- **WARN L150 `arrow_cap`**: non-peak arrow count 902 may feel visually heavy.
- **WARN L153 `arrow_cap`**: non-peak arrow count 1056 may feel visually heavy.
- **WARN L168 `arrow_cap`**: non-peak arrow count 1004 may feel visually heavy.
- **WARN L213 `arrow_cap`**: non-peak arrow count 935 may feel visually heavy.
- **WARN L217 `post_peak_release`**: previous level was extreme peak code 4, but chain only changes -3; expected visible release.
- **WARN L228 `arrow_cap`**: non-peak arrow count 953 may feel visually heavy.
- **WARN L233 `arrow_cap`**: non-peak arrow count 961 may feel visually heavy.
- **WARN L240 `post_peak_release`**: previous level was extreme peak code 4, but chain only changes 43; expected visible release.

## Suggested Use

Use this report before assembling a pack. A section with low score should be manually reviewed against its 10-level script before candidate replacement continues.
