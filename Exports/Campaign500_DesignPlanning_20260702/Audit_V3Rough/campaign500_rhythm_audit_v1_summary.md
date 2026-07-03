# Campaign500 Rhythm Audit V1

- Layout: `F:\Unityproject\ArrowLevel-Hand\Exports\Campaign500_DesignPlanning_20260702\campaign500_per_level_duty_v3_rough.csv`
- Score: **72 / 100**
- Grade: **B**
- Findings: 410 total, 0 errors, 151 warnings, 259 infos
- Sections below 80: 2

## Interpretation

This is a static pacing audit. It does not replace Unity playtest or solver trace, but it catches obvious rhythm issues: early pressure spikes, weak hard/peak contrast, missing post-peak release, source/style repetition, and chain-count overgrowth.

## Lowest Section Scores

| section | levels | score | avg | normal | hard | peak | top risk |
| --- | --- | ---: | ---: | ---: | ---: | ---: | --- |
| 18 | 171-180 | 76 | 99.6 | 100.8 | 106.0 | 113.5 | hard avg 106.0 is not clearly above normal avg 100.8.; peak avg 113.5 is not clearly above normal avg 100.8. |
| 19 | 181-190 | 76 | 99.2 | 103.4 | 103.0 | 112.0 | hard avg 103.0 is not clearly above normal avg 103.4.; peak avg 112.0 is not clearly above normal avg 103.4. |
| 2 | 11-20 | 88 | 49.2 | 46.3 | 0.0 | 94.0 | front20 section max chain 94 may be too high. |
| 3 | 21-30 | 88 | 72.6 | 70.2 | 98.0 | 98.0 | section avg jumps +23.4 from previous section. |
| 4 | 31-40 | 88 | 79.2 | 71.8 | 98.7 | 101.0 | early section avg chain 79.2 may rise too fast. |
| 5 | 41-50 | 88 | 77.6 | 67.7 | 81.0 | 103.0 | early section avg chain 77.6 may rise too fast. |
| 6 | 51-60 | 88 | 82.0 | 80.5 | 97.5 | 92.5 | peak avg 92.5 is not clearly above normal avg 80.5. |
| 12 | 111-120 | 88 | 90.3 | 90.8 | 93.7 | 105.0 | hard avg 93.7 is not clearly above normal avg 90.8. |
| 15 | 141-150 | 88 | 98.3 | 102.0 | 111.0 | 113.5 | peak avg 113.5 is not clearly above normal avg 102.0. |
| 21 | 201-210 | 88 | 106.2 | 104.8 | 110.7 | 153.0 | hard avg 110.7 is not clearly above normal avg 104.8. |
| 37 | 361-370 | 88 | 150.8 | 148.6 | 0.0 | 201.0 | section avg jumps +24.4 from previous section. |
| 1 | 1-10 | 100 | 31.2 | 27.2 | 60.0 | 0.0 |  |

## Top Warnings

- **WARN L19 `front20_boss`**: L19 first mini-boss chain 94 is above the planned effective-load band; verify it is not early hard wall.
- **WARN L19 `early_jump`**: chain jumps +41 from L18; early sections prefer smoother rise.
- **WARN L30 `post_peak_release`**: previous level was extreme peak code 4, but chain only changes -6; expected visible release.
- **WARN L38 `early_jump`**: chain jumps +59 from L37; early sections prefer smoother rise.
- **WARN L47 `early_jump`**: chain jumps +74 from L46; early sections prefer smoother rise.
- **WARN L55 `early_jump`**: chain jumps +51 from L54; early sections prefer smoother rise.
- **WARN L65 `early_jump`**: chain jumps +38 from L64; early sections prefer smoother rise.
- **WARN L69 `early_jump`**: chain jumps +41 from L68; early sections prefer smoother rise.
- **WARN L72 `early_jump`**: chain jumps +59 from L71; early sections prefer smoother rise.
- **WARN L78 `early_jump`**: chain jumps +39 from L77; early sections prefer smoother rise.
- **WARN L83 `early_jump`**: chain jumps +58 from L82; early sections prefer smoother rise.
- **WARN L87 `early_jump`**: chain jumps +52 from L86; early sections prefer smoother rise.
- **WARN L90 `post_peak_release`**: previous level was extreme peak code 4, but chain only changes -2; expected visible release.
- **WARN L93 `early_jump`**: chain jumps +38 from L92; early sections prefer smoother rise.
- **WARN L96 `early_jump`**: chain jumps +49 from L95; early sections prefer smoother rise.

## Suggested Use

Use this report before assembling a pack. A section with low score should be manually reviewed against its 10-level script before candidate replacement continues.
