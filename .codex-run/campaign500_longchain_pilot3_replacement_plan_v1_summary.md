# Campaign500 Long-Chain Pilot3 Replacement Plan V1

- Sections: 3, 25, 45
- Slots: 9 total, 2 regular + 1 hard-plus per section
- Aspect policy: main 0.74-0.86, one rare 0.90 extreme sample
- Coverage policy: mostly 0.975-0.995, two light negative-space rows at 0.955-0.980
- Validation gate: official trace solved, TraceOrderKeep, coverage>=0.95, sameDir<=7, STS>=0.82, collapse<=0.25

## Selected Slots

| order | section | role | old size | new size | old chains | planned chains | coverage band | visual role |
| --- | ---: | --- | --- | --- | ---: | ---: | --- | --- |
| 22 | 3 | LongNormalA | 16x24 (0.667) | 17x22 (0.773) | 55 | 25 | 0.975-0.995 | section_long_lock |
| 30 | 3 | LongNormalB | 20x32 (0.625) | 23x28 (0.821) | 92 | 46 | 0.955-0.980 | lock_light_negative_space |
| 25 | 3 | LongChallenge | 21x35 (0.6) | 25x29 (0.862) | 98 | 59 | 0.975-0.995 | lock_buckle_pressure |
| 244 | 25 | LongNormalA | 22x40 (0.55) | 26x33 (0.788) | 115 | 60 | 0.975-0.995 | section_long_lock |
| 250 | 25 | LongNormalB | 23x35 (0.657) | 26x31 (0.839) | 107 | 59 | 0.955-0.980 | dense_support_long |
| 249 | 25 | LongChallenge | 21x41 (0.512) | 28x31 (0.903) | 137 | 75 | 0.975-0.995 | lock_buckle_pressure |
| 441 | 45 | LongNormalA | 22x36 (0.611) | 25x31 (0.806) | 105 | 56 | 0.975-0.995 | sweep_to_spine |
| 450 | 45 | LongNormalB | 24x44 (0.545) | 30x35 (0.857) | 165 | 80 | 0.975-0.995 | section_long_lock |
| 449 | 45 | LongChallenge | 27x41 (0.659) | 32x35 (0.914) | 181 | 101 | 0.975-0.995 | extreme_lock_peak |

## Notes

- `targetChains` is treated as pacing budget and converted to lower long-chain density, not copied directly.
- This plan does not modify the original Campaign500 template.
- Next implementation step is a canvas-aware longify/generation pass that reads this CSV and writes a review pack.
