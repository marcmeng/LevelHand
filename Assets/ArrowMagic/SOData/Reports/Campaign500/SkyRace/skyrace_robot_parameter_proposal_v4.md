# SkyRace robot parameter proposal - V4 campaign

- Source campaign difficulty: campaign500_stage_relative_difficulty_v4_after_hole_highchain.csv
- Target: 15 consecutive levels
- Time unit: minutes, matching J_SkyRace finishMinute naming
- Plan bands are continuous for finished players: fast 1-17, standard 18-29, slow 30-999.

## 15-level standard-player estimate
- standard_min: 9.7
- standard_p25: 16.4
- standard_median: 18.6
- standard_p75: 22.6
- standard_max: 24.6
- fast_p25: 12.8
- fast_median: 14.5
- fast_p75: 17.6
- slow_p25: 22.1
- slow_median: 25.2
- slow_p75: 30.5

## Proposed table values
| Sheet | Id | Field | Current | Proposal | Note |
|---|---:|---|---|---|---|
| SkyRace | 1 | targetCount | `15` | `15` | keep 15-level race target |
| SkyRaceRobot | 1 | finishMinute | `45,90` | `10,16` | fast robot; competitive for strong players but still beatable |
| SkyRaceRobot | 2 | finishMinute | `45,90` | `16,24` | normal robot; around standard-player p25 to p75 |
| SkyRaceRobot | 3 | finishMinute | `45,90` | `24,36` | slow robot; covers late-campaign slower clears |
| SkyRaceRobot | 4 | finishMinute | `1000,2000` | `1000,2000` | non-finisher placeholder kept |
| SkyRaceRobotPlan | 1 | robots | `2,3,3,4` | `2,3,3,4` | new player gets easier race |
| SkyRaceRobotPlan | 2 | robots | `2,3,3,4` | `2,3,3,4` | previous non-finisher gets easier race |
| SkyRaceRobotPlan | 3 | finishMinute | `31,40` | `30,999` | slow finisher band; avoids gaps above 40 minutes |
| SkyRaceRobotPlan | 3 | robots | `2,3,3,4` | `2,3,3,4` | one normal, two slow, one non-finisher |
| SkyRaceRobotPlan | 4 | finishMinute | `21,30` | `18,29` | standard player band based on 15-level estimates |
| SkyRaceRobotPlan | 4 | robots | `1,2,3,4` | `1,2,3,4` | standard race mix |
| SkyRaceRobotPlan | 5 | finishMinute | `4,20` | `1,17` | fast player band; covers very early/easy clears too |
| SkyRaceRobotPlan | 5 | robots | `1,2,3,4` | `1,1,2,3` | fast players need two competitive robots; still one slower robot |

## 15-level groups
| Levels | StdMin | FastMin | SlowMin | Chains | AvgChains | Diff | TopTypes |
|---|---:|---:|---:|---:|---:|---|---|
| 1-15 | 9.7 | 7.5 | 13.0 | 525 | 35 | 1:13;2:2 | normal:6;shape:4;lock:2;tutorial:1;hole_rescue:1 |
| 16-30 | 14.5 | 11.3 | 19.6 | 1028 | 68.5 | 1:8;2:4;3:1;4:2 | lock:4;hole_rescue:2;section:2;normal:1;shell:1 |
| 31-45 | 15.3 | 11.9 | 20.6 | 1185 | 79 | 1:7;2:4;3:3;4:1 | lock:5;dense:4;shape_magic:3;sweep:1;hole_rescue:1 |
| 46-60 | 15.0 | 11.7 | 20.2 | 1215 | 81 | 1:10;2:4;3:1 | section:4;lock:3;hole_rescue:2;shape_magic:1;sweep:1 |
| 61-75 | 14.9 | 11.6 | 20.1 | 1232 | 82.1 | 1:10;2:4;3:1 | lock:3;section:3;hole_rescue:2;sweep:2;shape_symbol:2 |
| 76-90 | 14.9 | 11.6 | 20.2 | 1192 | 79.5 | 1:11;2:1;3:1;4:2 | section:4;lock:3;maze:2;shape_music:1;sweep:1 |
| 91-105 | 15.8 | 12.4 | 21.4 | 1220 | 81.3 | 1:8;2:5;3:2 | lock:3;section:3;hole_rescue:2;dense:2;shape_symbol:1 |
| 106-120 | 16.4 | 12.8 | 22.1 | 1401 | 93.4 | 1:10;2:3;3:1;4:1 | section:5;lock:3;shape_symbol:1;dense:1;shape_magic:1 |
| 121-135 | 15.5 | 12.1 | 20.9 | 1226 | 81.7 | 1:11;2:2;3:2 | section:3;lock:3;maze:2;dense:2;shape_magic:2 |
| 136-150 | 17.4 | 13.6 | 23.5 | 1474 | 98.3 | 1:9;2:3;3:2;4:1 | section:5;lock:3;shape_space:1;sweep:1;hole_rescue:1 |
| 151-165 | 16.4 | 12.8 | 22.2 | 1392 | 92.8 | 1:11;2:1;3:2;4:1 | lock:3;section:3;dense:2;sweep:2;hole_rescue:1 |
| 166-180 | 17.9 | 14.0 | 24.1 | 1487 | 99.1 | 1:7;2:5;3:2;4:1 | section:4;lock:3;hole_rescue:2;shape_object:1;dense:1 |
| 181-195 | 17.1 | 13.3 | 23.0 | 1467 | 97.8 | 1:10;2:3;3:1;4:1 | lock:3;section:3;shape_ocean:2;hole_rescue:2;sweep:2 |
| 196-210 | 17.5 | 13.6 | 23.6 | 1595 | 106.3 | 1:12;2:1;3:1;4:1 | section:5;lock:3;maze:1;shape_object:1;hole_rescue:1 |
| 211-225 | 17.1 | 13.4 | 23.1 | 1461 | 97.4 | 1:10;2:2;3:3 | lock:3;section:3;dense:2;hole_rescue:2;sweep:1 |
| 226-240 | 18.1 | 14.1 | 24.4 | 1658 | 110.5 | 1:10;2:3;3:1;4:1 | section:5;lock:3;sweep:2;shape_magic:1;maze:1 |
| 241-255 | 20.1 | 15.7 | 27.1 | 1671 | 111.4 | 1:4;2:7;3:2;4:2 | section:3;lock:3;hole_rescue:2;shape_space:2;dense:2 |
| 256-270 | 20.0 | 15.6 | 27.0 | 1814 | 120.9 | 1:9;2:2;3:3;4:1 | section:4;lock:3;sweep:2;shape_ocean:1;dense:1 |
| 271-285 | 18.6 | 14.5 | 25.2 | 1711 | 114.1 | 1:12;2:2;4:1 | lock:3;section:3;shape_toolui:2;dense:2;hole_rescue:2 |
| 286-300 | 22.5 | 17.5 | 30.3 | 2063 | 137.5 | 1:7;2:4;3:2;4:2 | section:5;lock:3;shape_ocean:1;dense:1;hole_rescue:1 |
| 301-315 | 19.8 | 15.5 | 26.8 | 1845 | 123 | 1:10;2:3;3:2 | section:3;lock:3;dense:2;maze:2;hole_rescue:1 |
| 316-330 | 22.2 | 17.3 | 30.0 | 2099 | 139.9 | 1:10;2:2;4:3 | section:5;lock:3;hole_rescue:2;shape_character:1;maze:1 |
| 331-345 | 22.9 | 17.9 | 30.9 | 2122 | 141.5 | 1:6;2:5;3:3;4:1 | sweep:3;section:3;lock:2;hole_rescue:2;dense:1 |
| 346-360 | 23.7 | 18.5 | 32.0 | 2275 | 151.7 | 1:8;2:3;3:2;4:2 | section:4;dense:3;lock:2;maze:2;shape_landmark:1 |
| 361-375 | 22.3 | 17.4 | 30.2 | 2169 | 144.6 | 1:8;2:5;3:1;4:1 | sweep:3;section:3;dense:2;lock:2;shape_landmark:1 |
| 376-390 | 24.6 | 19.2 | 33.2 | 2394 | 159.6 | 1:9;2:1;3:3;4:2 | section:4;lock:3;hole_rescue:2;dense:2;shape_landmark:1 |
| 391-405 | 22.6 | 17.6 | 30.5 | 2144 | 142.9 | 1:10;2:1;3:2;4:2 | section:3;sweep:2;hole_rescue:2;lock:2;dense:2 |
| 406-420 | 22.6 | 17.7 | 30.6 | 2185 | 145.7 | 1:9;2:2;3:3;4:1 | section:5;dense:2;lock:2;sweep:2;shape_nature:1 |
| 421-435 | 24.3 | 19.0 | 32.8 | 2299 | 153.3 | 1:7;2:3;3:3;4:2 | dense:3;section:3;shape_vehicle:2;lock:2;hole_rescue:2 |
| 436-450 | 23.0 | 17.9 | 31.0 | 2285 | 152.3 | 1:8;2:5;3:1;4:1 | section:4;lock:2;sweep:2;dense:2;shape_nature:1 |
| 451-465 | 21.6 | 16.8 | 29.1 | 2117 | 141.1 | 1:9;2:4;3:2 | lock:3;section:3;sweep:3;dense:2;hole_rescue:1 |
| 466-480 | 23.1 | 18.0 | 31.2 | 2218 | 147.9 | 1:10;3:3;4:2 | section:5;hole_rescue:2;sweep:2;shape_toolui:1;maze:1 |
| 481-495 | 23.4 | 18.3 | 31.6 | 2187 | 145.8 | 1:7;2:4;3:1;4:3 | section:3;sweep:3;lock:2;hole_rescue:2;shape_object:2 |
| 496-500 | 7.4 | 5.8 | 10.0 | 701 | 140.2 | 1:2;2:2;3:1 | section:2;lock:1;dense:1;shape_nature:1 |