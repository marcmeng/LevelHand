# Campaign500 Trace V2 Calibration 30 Selection

Generated: 2026-07-02 10:03:08  
Updated: 2026-07-02 10:28:56

- Source layout: Exports/C5V5F100/Docs/campaign500_rhythm_v5_front100_per_level_config_index.csv
- Purpose: human + Trace V2 calibration sample, not a final production pack.
- Selection size: 30 levels.
- Missing exported trace assets: 0
- Mapping rule: trace metrics are joined back by normalized asset path, not by levelId text.

## Sample Mix

By category:
- hole: 2
- normal: 21
- shape: 7

By difficulty code:
- difficultyCode 1: 15
- difficultyCode 2: 7
- difficultyCode 3: 4
- difficultyCode 4: 4

## Selected Levels

| level | code | category | chain | arrows | sampleRole | slotDuty | review question |
| ---: | ---: | --- | ---: | ---: | --- | --- | --- |
| 1 | 1 | normal | 6 | 21 | front20_control | 新手消除规则引导 | 验证 evaluator 不应把教程误判为困难 |
| 2 | 1 | hole | 22 | 130 | hole_theme_control | 营救主题与洞洞空间初见 | 验证 hole anchor 与困难卡点的区别 |
| 3 | 1 | normal | 15 | 181 | scale_intro_control | 镜头/画布尺度引导 | 验证普通正式棋盘不被误判为读题难 |
| 4 | 1 | shape | 55 | 459 | early_shape_jump | 特殊图形解锁：水晶球 | 判断视觉/数量跳变是否只是重，不是真难 |
| 10 | 2 | normal | 38 | 430 | first_hard_quiz | 前10关小考：轻困难lock | 应出现轻读题或可解释小卡点 |
| 19 | 3 | normal | 60 | 772 | front20_peak | 前20第一标杆/小Boss | 检查是否存在可读 choke，而不是单纯高量级 |
| 20 | 1 | normal | 40 | 427 | post_front20_release | 前20收束/新主题预告 | 验证峰值后是否真正释放 |
| 23 | 1 | shape | 87 | 735 | early_shape_heavy | ShapeAnchor | 检查视觉重与难度的分离 |
| 25 | 2 | normal | 64 | 626 | early_hard_read | LocalRunBreaker | 应观察到 region/axis/choke 之一 |
| 29 | 4 | normal | 75 | 772 | early_extreme_peak | ExtremeMemory | 验证 peak 是否有主题和卡点证据 |
| 30 | 1 | normal | 47 | 538 | post_extreme_release | NormalPractice | 检查 post-peak release |
| 38 | 2 | shape | 88 | 718 | shape_hard_jump | ShapeReadAnchor | 判断 shape 压力是否可读还是视觉重 |
| 47 | 4 | normal | 94 | 667 | early_extreme_hub | ExtremeMemory | 检查 hub 是否产生可解释卡点 |
| 72 | 3 | normal | 92 | 862 | mid_peak_pressure | DependencyPeak | 检查 peak 是否比普通多真实读题 |
| 83 | 2 | shape | 91 | 709 | shape_hard_mid | ShapeReadAnchor | 检查 shape hard 的空间约束是否真实 |
| 96 | 4 | normal | 121 | 1155 | longchain_extreme | ExtremeMemory | 检查 longchain 是否体现主干卡点 |
| 100 | 1 | normal | 83 | 763 | section_landing_heavy | NormalPractice | 判断普通重关是否是假难 |
| 112 | 3 | normal | 92 | 1343 | high_arrow_peak | DependencyPeak | 区分高箭头疲劳和真实结构难 |
| 150 | 1 | normal | 84 | 902 | high_arrow_normal | NormalPractice | 检查 visual overload 是否被误判为难 |
| 153 | 2 | shape | 131 | 1056 | shape_high_arrow_hard | ShapeReadAnchor | 检查 shape hard 的可读卡点与视觉负担 |
| 168 | 2 | shape | 118 | 1004 | shape_high_arrow_hard_2 | ShapeReadAnchor | 作为 shape hard 对照 |
| 181 | 1 | normal | 77 | 700 | section19_recovery | RecoveryFlow | 检查 recovery 是否真的释放 |
| 185 | 2 | normal | 75 | 705 | section19_hard_contrast | ReadCheck | 检查 hard 是否反而不如 normal 难 |
| 188 | 1 | normal | 106 | 784 | section19_heavy_normal | NormalPractice | 检查 normal 是否压过困难 |
| 217 | 1 | hole | 56 | 540 | post_extreme_release_late | HoleSpatialAnchor | 检查 release failure 是否被事件化 |
| 305 | 3 | normal | 82 | 818 | section31_peak_contrast | StylePeak | 检查 peak contrast 是否不足 |
| 310 | 1 | normal | 111 | 838 | section31_heavy_normal | NormalPractice | 与305对照，判断普通倒挂 |
| 378 | 1 | shape | 138 | 1134 | late_shape_heavy | ShapeAnchor | 检查空间/视觉重与卡点难度区别 |
| 456 | 4 | normal | 92 | 1045 | late_extreme_longchain | ExtremeMemory | 检查终局峰值是否有强 readable choke |
| 466 | 1 | normal | 166 | 1015 | late_heavy_normal | NormalPractice | 检查普通高量级是否是假难 |

## Official Trace Baseline

- Chunked official trace completed: 20 / 30 levels.
- Solved in completed trace: 20 / 20.
- Completed orders: 1, 2, 3, 4, 10, 19, 20, 23, 25, 29, 30, 38, 47, 72, 83, 96, 100, 112, 150, 153
- Pending orders: 168, 181, 185, 188, 217, 305, 310, 378, 456, 466
- Chunk 5 timed out on heavier late-section samples; pending rows should be run one-by-one or with a larger timeout.

Completed process tier mix:
- processTier A: 3
- processTier B: 17

## Partial Trace Metrics

| level | solved | tier | avgChoices | maxChoices | choiceChokeWindows | remoteChokes | localRun | nearOuterRun | STS |
| ---: | --- | --- | ---: | ---: | ---: | ---: | ---: | ---: | ---: |
| 1 | True | A | 2.33 | 4 | 1 | 0 | 3 | 3 | 0.866 |
| 2 | True | B | 3.73 | 6 | 2 | 1 | 4 | 4 | 0.855 |
| 3 | True | A | 4.07 | 8 | 1 | 1 | 3 | 3 | 0.924 |
| 4 | True | B | 4.35 | 8 | 3 | 0 | 8 | 6 | 0.707 |
| 10 | True | B | 4.79 | 8 | 2 | 1 | 6 | 3 | 0.878 |
| 19 | True | A | 6.07 | 8 | 1 | 0 | 4 | 3 | 0.848 |
| 20 | True | B | 5.08 | 9 | 3 | 0 | 5 | 5 | 0.919 |
| 23 | True | B | 5.43 | 10 | 2 | 0 | 7 | 2 | 0.749 |
| 25 | True | B | 4.62 | 8 | 3 | 2 | 5 | 5 | 0.833 |
| 29 | True | B | 6.17 | 10 | 2 | 2 | 4 | 4 | 0.907 |
| 30 | True | B | 6.13 | 10 | 1 | 0 | 5 | 3 | 0.865 |
| 38 | True | B | 5.45 | 10 | 5 | 0 | 8 | 4 | 0.721 |
| 47 | True | B | 5.99 | 10 | 3 | 2 | 6 | 5 | 0.82 |
| 72 | True | B | 6.65 | 11 | 1 | 0 | 6 | 2 | 0.831 |
| 83 | True | B | 3.76 | 8 | 7 | 0 | 9 | 2 | 0.643 |
| 96 | True | B | 6.77 | 11 | 1 | 2 | 6 | 4 | 0.859 |
| 100 | True | B | 4.87 | 9 | 5 | 2 | 5 | 5 | 0.85 |
| 112 | True | B | 3.71 | 8 | 6 | 2 | 7 | 4 | 0.825 |
| 150 | True | B | 6 | 10 | 3 | 0 | 6 | 3 | 0.891 |
| 153 | True | B | 6.08 | 10 | 2 | 0 | 13 | 3 | 0.701 |

## Output Files

- campaign500_trace_v2_calibration30_selection.csv: full selection metadata.
- campaign500_trace_v2_calibration30_trace_input.csv: all-30 trace input for Build-SGPRhythmTrace.ps1.
- campaign500_trace_v2_calibration30_metrics_partial20.csv: raw official trace metrics for completed chunks.
- campaign500_trace_v2_calibration30_metrics_partial20_joined_selection.csv: completed metrics joined with campaign slot metadata by asset path.
- campaign500_trace_v2_calibration30_trace_pending10.csv: pending selection rows.
- campaign500_trace_v2_calibration30_trace_input_pending10.csv: pending trace input rows.
- campaign500_trace_v2_calibration30_human_review_sheet.csv: human review sheet with partial trace metrics and blank human labels.

## Intended Calibration Use

Use this set to calibrate whether Trace V2 can distinguish:

- true readable choke vs. only high chain/arrow count;
- hole/shape spatial pressure vs. real difficulty;
- peak slots that actually feel harder than nearby normal slots;
- recovery slots that still feel overloaded;
- longchain-heavy levels that may have lower chain count but higher reading burden.
