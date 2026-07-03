# Campaign500 Rhythm Audit V1

- Layout: `F:\Unityproject\ArrowLevel-Hand\Exports\Campaign500_First50_V2Existing_20260702\campaign500_first50_v2_existing_layout_v1.csv`
- Score: **96 / 100**
- Grade: **A**
- Findings: 3 total, 0 errors, 2 warnings, 1 infos
- Sections below 80: 0

## Interpretation

This is a static pacing audit. It does not replace Unity playtest or solver trace, but it catches obvious rhythm issues: early pressure spikes, weak hard/peak contrast, missing post-peak release, source/style repetition, and chain-count overgrowth.

## Lowest Section Scores

| section | levels | score | avg | normal | hard | peak | top risk |
| --- | --- | ---: | ---: | ---: | ---: | ---: | --- |
| 1 | 1-10 | 88 | 29.7 | 31.2 | 37.0 | 0.0 | hard avg 37.0 is not clearly above normal avg 31.2. |
| 2 | 11-20 | 100 | 44.3 | 43.7 | 0.0 | 61.0 |  |
| 3 | 21-30 | 100 | 53.9 | 49.4 | 67.0 | 84.0 |  |
| 4 | 31-40 | 100 | 60.1 | 51.0 | 74.0 | 79.0 |  |
| 5 | 41-50 | 100 | 72.6 | 61.3 | 74.0 | 89.5 |  |

## Top Warnings

- **WARN L3 `front20_scale_intro`**: L3 镜头/画布尺度引导 has chain 37; camera/board-scale guide should stay low-mid.
- **WARN section 1 `hard_contrast`**: hard avg 37.0 is not clearly above normal avg 31.2.

## Suggested Use

Use this report before assembling a pack. A section with low score should be manually reviewed against its 10-level script before candidate replacement continues.
