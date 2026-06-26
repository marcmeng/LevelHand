# Level Index

路径以仓库根目录为基准。这里记录关卡、包、报告、掩码和配置入口，不复制资源内容。

## Primary Data Roots

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `Assets/ArrowMagic/SOData/Levels/` | 关卡 asset 根目录 | 查找 LevelDefinition、候选、生产关卡 |
| `Assets/ArrowMagic/SOData/Packs/` | LevelPack 根目录 | 查找 campaign/生产包引用 |
| `Assets/ArrowMagic/SOData/Reports/` | 生成/验证报告根目录 | 查找候选评分、优化轮次、验收报告 |
| `Assets/ArrowMagic/Masks/` | mask 图片和批次根目录 | 形状掩码、生产掩码、实验 mask |
| `Assets/ArrowMagic/SOData/Palettes/` | 调色板配置 | 视觉主题、颜色配置 |
| `Assets/ArrowMagic/SOData/SfxLibraries/` | 音效配置 | 音效库引用 |
| `Assets/ArrowMagic/SOData/VfxLibraries/` | VFX 配置 | 特效库引用 |

## Production and Campaign

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `Assets/ArrowMagic/SOData/Levels/Production/` | 生产关卡根 | 交付、正式包、生产候选核对 |
| `Assets/ArrowMagic/SOData/Levels/Production/Front20Polish/` | 前 20 polish 生产关卡 | 前段体验/难度 polish |
| `Assets/ArrowMagic/SOData/Levels/Production/HoleMask/` | HoleMask 生产候选 | hole mask 正式候选和 early 批次 |
| `Assets/ArrowMagic/SOData/Levels/Production/HoleProcedural/` | HoleProcedural 生产候选 | hole procedural 正式候选 |
| `Assets/ArrowMagic/SOData/Levels/Production/HoleLongOuterStrong/` | HoleLongOuterStrong 生产候选 | 长外圈强约束候选 |
| `Assets/ArrowMagic/SOData/Packs/Production/` | 生产 pack 根 | 正式包引用和交付检查 |
| `Assets/ArrowMagic/SOData/Reports/Campaign500/` | Campaign500 报告根 | campaign 优化、baseline、shape pass、外部主题报告 |
| `Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/` | 2026-06-18 Campaign500 优化轮次报告 | 排查当天优化结果和 shape refresh 批次 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/` | SGP 节奏/难度实验报告 | 查看静态节奏分类、真实过程 choice curve、伪深度风险和 process keep 候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadOrientationPreviewPack.asset` | PressureRead 结构化 demo pack | 查看远依赖/低选择/结构化直链小批 demo；Demo 场景当前挂此包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PressureReadOrientation/` | PressureRead 结构化候选关卡 | 查看通过头尾方向重组生成的候选 LevelDefinition |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockPreviewPack.asset` | PressureRead 阶段门锁实验 pack | 查看最新一次 StageLock 脚本输出；该包可能被后续试跑覆盖 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockCurated5Pack.asset` | PressureRead 阶段门锁精选 5 关稳定包 | 上一版稳定预览包；用于对比依赖合链前后的难关方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockDepMerge5Pack.asset` | PressureRead 阶段门锁依赖合链精选 5 关 | 上一版依赖合链稳定包；用于对比 BalancedDepMerge6 的产率和质量 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockBalancedDepMerge6Pack.asset` | PressureRead 平衡源排序 + 依赖合链精选 6 关 | 上一版稳定难关预览包；用于对比 LoopMerge160 的产率和质量 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockLoopMerge160Pack.asset` | PressureRead 多轮依赖合链 160 源精选 11 关 | 可覆盖目录时期的 11 关包；用于对比 Frozen 包，不建议作为当前稳定 demo 入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockLoopMerge160FrozenPack.asset` | PressureRead LoopMerge160 冻结稳定包 | 当前 Demo 挂载的稳定 11 关包；关卡已复制到独立目录，后续试跑不会覆盖 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockSourceEnhanced7Pack.asset` | PressureRead 温和源增强 7 关冻结包 | 源增强前置中长链后的实验结果包；trace 为 S=6/A=1，用于评估源语言前置方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockDepAware67Pack.asset` | PressureRead 依赖感知源增强 5 关冻结包 | `DependencyAware` 源增强后的高质量难关分支；trace 为 S=4/A=1，结构链更强但产能仍低 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockTargetedDepAware68Pack.asset` | PressureRead targeted DepAware 12 关候选包 | targeted 源 feed + DependencyAware 后的完整候选包；trace 为 S=11/A=1，用于人工看图和继续筛选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockTargetedDepAware68HardSelectedPack.asset` | PressureRead targeted DepAware 8 关精选硬关包 | trace 后按 family/base 限制精选；适合评估更接近正式入池的难关质量 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockTargetedDepAwareMergedHard10Pack.asset` | PressureRead targeted DepAware 多切片 10 关硬关池 | 合并第一切片精选 8 关和第二切片精选 2 关后的多 family 硬关池 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockLongBias5Pack.asset` | PressureRead StageLock 复杂长链 5 关硬关样本 | `HighYield/ReferenceLong + LongChainBias` 的去重小样包；用于人工评估更复杂长链难关是否成立 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction15Pack.asset` | PressureRead StageLock 15 关硬关生产候选池 | 合并 targeted hard、LongBias 和 symmetry expansion 后的当前最大去重硬关候选池 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction16V2Pack.asset` | PressureRead StageLock 16 关硬关生产候选池 v2 | 在 HardProduction15 基础上加入 HighYield 尾部 symmetry 补充后的当前最大去重硬关候选池 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction23V3Pack.asset` | PressureRead StageLock 23 关硬关生产候选池 v3 | 当前最佳硬关候选池；由 HardProduction16V2 加成功 enhanced source 二级自举扩产后合并去重得到 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction31V4BootstrapPack.asset` | PressureRead StageLock 31 关硬关自举候选池 v4 | 比 V3 更硬但 family 更集中；用于评估 hard-production 上限和同源风险 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction33V5PreviewPack.asset` | PressureRead StageLock 33 关硬关候选池 v5 preview | V4Bootstrap 加 highchain success bootstrap 严格结构去重补充后的预览包；用于评估更复杂长链补充 lane |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockStageDoorAdditions5Pack.asset` | PressureRead StageDoor 源增强 5 关对照包 | HardMidWide 与 minority success lane 生成的高质量 StageDoor 变体；用于看图评估同父本替换和少数 family 补强，不作为去重主池 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockStageDoorLane9Pack.asset` | PressureRead StageDoor 少数结构 9 关补强包 | minority success 源经 symmetry + StageDoor 后合并出的 dense/maze/section/lock 补强池；当前 Demo 已挂此包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockStageDoorPipeline10Pack.asset` | PressureRead StageDoor symmetry pipeline 10 关包 | 可复用流水线输出的 10 关 review pool；比 Lane9 多一个 highchain 长链样本，当前不作为 Demo 默认入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockStageDoorProduction24Pack.asset` | PressureRead StageDoor symmetry 24 关生产池 | V3 proven source 经 symmetry + StageDoor 产出的当前最强可重复生产池；当前 Demo 已挂此包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction45V6StageDoorStrictPack.asset` | PressureRead StageLock hard production 45 关 V6 strict 池 | V5Preview + StageDoorProduction24 合并后严格筛选的上一版主审查包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockReferenceComplexLong7Pack.asset` | PressureRead StageLock ReferenceComplex 7 关复杂长链补充包 | ReferenceComplex 小切片精选出的低产高质量补充样本；用于看复杂长链语言是否成立 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction52V7RefComplexLongPack.asset` | PressureRead StageLock 52 关 V7 复杂长链硬关池 | Strict45 + ReferenceComplexLong7 合并后的当前 V7 demo 审查包；Demo 场景已挂此包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardMidChainProbe5V1Pack.asset` | PressureRead StageLock 5 关中型链组 probe 包 | V7 中 `chains>=40` 的样本加 1 个新 47 链 HardMidWide 样本；用于验证 40-55 链、低选择、长结构链方向，不是最终量产包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_loopmerge160_notes.md` | LoopMerge160 结果说明 | 查看 160 源产率、trace 指标、当前瓶颈和下一步 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_source_profile_notes.md` | StageLock 源画像复盘 | 查看 SGP 源与外部 298 seed 的结构差距、画像排序结论和入口 root merge 实验结论 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_source_enhanced7_notes.md` | StageLock 源增强 7 关复盘 | 查看暴力预合链失败、温和预合链可行、下一步方向/依赖感知源增强结论 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_depaware67_notes.md` | StageLock 依赖感知源增强复盘 | 查看 DependencyAware 源增强的产率、trace、结构指标和下一步吞吐瓶颈 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_targeted_depaware68_notes.md` | StageLock targeted DepAware 复盘 | 查看 targeted feed 如何把 DepAware 产率从 5/67 提升到 12/68，以及 8 关精选标准 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_targeted_depaware_merged_hard10.csv` | StageLock targeted DepAware 多切片硬关池 CSV | 当前 10 关 merged hard pool 的冻结输入，含 trace 和 family 去重字段 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_longchain_bias_notes.md` | StageLock LongChainBias 复盘 | 查看 `HighYield`、`ReferenceLong`、`LongChainBias` 对产率、过程曲线和复杂长链结构的影响 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_longchain_bias_merged5.csv` | StageLock LongChainBias 5 关 CSV | `LongBias5Pack` 的冻结输入，已按 base/family 去重 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_symmetry_expansion_notes.md` | StageLock 高产源几何扩展复盘 | 查看 symmetry source expansion 对产率、trace 和最终硬关池的影响 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_pool.csv` | StageLock 15 关硬关生产候选池 CSV | `HardProduction15Pack` 的冻结输入，已按 source hash 和 family 去重 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_pool_v2.csv` | StageLock 16 关硬关生产候选池 v2 CSV | `HardProduction16V2Pack` 的冻结输入，已按 source hash 和 family 去重 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_pool_v3.csv` | StageLock 23 关硬关生产候选池 v3 CSV | `HardProduction23V3Pack` 的冻结输入，指标更硬，包含成功 enhanced source 二级自举新增候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_pool_v4_bootstrap.csv` | StageLock 31 关硬关自举候选池 v4 CSV | `HardProduction31V4BootstrapPack` 的冻结输入；更硬但 lock/section/dual family 占比高 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v3_notes.md` | StageLock HardProduction23V3 结果说明 | 查看 V3 指标、成功源自举扩产路线、unprofiled/随机救援负结果和下一步 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_pool_v5_preview.csv` | StageLock 33 关硬关候选池 v5 preview CSV | `HardProduction33V5PreviewPack` 的冻结输入，含 highchain strict 补充候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_stage_door_additions5.csv` | StageDoor 源增强 5 关对照 CSV | `StageDoorAdditions5Pack` 的冻结输入，family mix 为 section/lock/maze/dense；全部真实 trace S |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_stage_door_lane9.csv` | StageDoor 少数结构 9 关补强 CSV | `StageDoorLane9Pack` 的冻结输入；dense_weave=4、maze_long_chain=2、section_unlock=2、lock_buckle=1 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_stage_door_pipeline10.csv` | StageDoor symmetry pipeline 10 关 CSV | `StageDoorPipeline10Pack` 的冻结输入；Lane9 加一个 highchain long-chain review 样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_stage_door_production26.csv` | StageDoor symmetry 24 关生产池 CSV | `StageDoorProduction24Pack` 的冻结输入；文件名保留 26 目标但实际 source-hash 去重后为 24 关 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v5_plus_stagedoor_prod24_cap10.csv` | V5 + StageDoorProduction24 合并 47 关 CSV | `MaxPerFamily=10` 的合并池，V5=30、StageDoor=17；strict45 的基础输入 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v5_plus_stagedoor_prod24_cap10_strict45.csv` | V5 + StageDoorProduction24 strict 45 关 CSV | `HardProduction45V6StageDoorStrictPack` 的冻结输入；规则为 `traceMaxChoices<=8` 且 `traceStageLockScore>=0.60` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_reference_complex_long7.csv` | StageLock ReferenceComplex 7 关复杂长链补充 CSV | `ReferenceComplexLong7Pack` 的冻结输入；由 ReferenceComplex 小切片精选合并而来 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v7_strict45_plus_refcomplex_long.csv` | StageLock V7 52 关硬关池 CSV | `HardProduction52V7RefComplexLongPack` 的冻结输入；Strict45 加 7 个复杂长链补充样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v7_refcomplex_long_notes.md` | StageLock V7 ReferenceComplex 长链复盘 | 查看 ReferenceComplex preset、负结果、7 关补充包和 V7 52 关指标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_midchain_probe_v1.csv` | StageLock 中型链组 5 关 probe CSV | `HardMidChainProbe5V1Pack` 的冻结输入，链组范围 `41-53` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_midchain_probe_notes.md` | StageLock 中型链组 probe 复盘 | 查看 `MinOutputChains`、40/36 链底线实验、47 链候选指标和下一步 StageDoor gate 语义瓶颈 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_stage_door_minority_sym2_v1_selected_hard.csv` | StageDoor minority symmetry 精选 CSV | minority success 源经 symmetry + StageDoor 后精选出的 4 个 dense/maze S 级候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_sdsym_v3head12_r1_selected_hard.csv` | StageDoor V3 proven head12 精选 CSV | V3 proven head12 经 StageDoor symmetry 选出的 8 个 lock/section 高硬度候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_sdsym_v3tail11_r1_selected_hard.csv` | StageDoor V3 proven tail11 精选 CSV | V3 proven tail11 经 StageDoor symmetry 选出的 8 个 dual/section/maze 候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_sdsym_minority_r1_selected_hard.csv` | StageDoor symmetry pipeline minority 回归精选 CSV | 由 `Run-StageLockStageDoorSymmetrySlice.ps1` 自动复现出的 4 个 minority selected，用于验证流水线 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_sdsym_highchain_r1_selected_hard.csv` | StageDoor symmetry pipeline highchain 精选 CSV | highchain success feed 经 StageDoor symmetry 后仅精选 1 个，说明高链路线可用但低产 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_true_hard_mass_notes.md` | StageLock 真正硬关量产复盘 | 查看参考 top40 过程画像、HardXL 负结果、highchain bootstrap 正结果、V5Preview 指标和下一步 StageDoorSGP 方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PressureReadStageLock/` | PressureRead 阶段门锁候选关卡 | 查看阶段计划和合链后处理生成的候选 LevelDefinition |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PressureReadStageLockLoopMerge160Frozen/` | LoopMerge160 冻结关卡目录 | 当前 Demo 11 关的独立 LevelDefinition；避免可覆盖实验目录破坏稳定包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PressureReadStageLockSourceEnhanced/` | StageLock 源增强候选目录 | 存放轻量中长链源增强后的中间源 asset；供 StageLock 使用，不是最终关卡 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PressureReadStageLockSourceEnhanced7Frozen/` | StageLock 源增强 7 关冻结目录 | `SourceEnhanced7Pack` 对应的最终候选关卡 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PressureReadStageLockDepAware67Frozen/` | StageLock 依赖感知源增强 5 关冻结目录 | `DepAware67Pack` 对应的最终候选关卡 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PressureReadStageLockTargetedDepAware68Frozen/` | StageLock targeted DepAware 12 关冻结目录 | `TargetedDepAware68Pack` 对应的完整候选关卡 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDA68Hard/` | StageLock targeted DepAware 8 关精选冻结目录 | `TargetedDepAware68HardSelectedPack` 对应的严格精选关卡，目录名较短用于避开 Windows 路径长度 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAMerge10/` | StageLock targeted DepAware merged hard 10 关冻结目录 | `TargetedDepAwareMergedHard10Pack` 对应的多切片精选关卡 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDALongBias5/` | StageLock LongChainBias 5 关冻结目录 | `LongBias5Pack` 对应的复杂长链硬关样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction15/` | StageLock 15 关硬关生产候选冻结目录 | `HardProduction15Pack` 对应的当前最大去重硬关候选池 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction16V2/` | StageLock 16 关硬关生产候选冻结目录 | `HardProduction16V2Pack` 对应的当前最大去重硬关候选池 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction23V3/` | StageLock 23 关硬关生产候选冻结目录 | `HardProduction23V3Pack` 对应的当前最佳硬关候选池 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction31V4Bootstrap/` | StageLock 31 关硬关自举候选冻结目录 | `HardProduction31V4BootstrapPack` 对应冻结关卡；看图时重点检查同源感 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction33V5Preview/` | StageLock 33 关硬关候选冻结目录 | `HardProduction33V5PreviewPack` 对应冻结关卡；包含 highchain strict 补充样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAStageDoorAdditions5/` | StageDoor 源增强 5 关冻结目录 | `StageDoorAdditions5Pack` 对应冻结关卡；用于与 V5Preview 对照视觉和手感 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAStageDoorLane9/` | StageDoor 少数结构 9 关冻结目录 | `StageDoorLane9Pack` 对应冻结关卡；当前 Demo 验证入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAStageDoorPipeline10/` | StageDoor symmetry pipeline 10 关冻结目录 | `StageDoorPipeline10Pack` 对应冻结关卡；包含一个 highchain 长链补充样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAStageDoorProduction24/` | StageDoor symmetry 24 关生产池冻结目录 | `StageDoorProduction24Pack` 对应冻结关卡；当前 Demo 验证入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction45V6StageDoorStrict/` | V6 StageDoor strict 45 关冻结目录 | `HardProduction45V6StageDoorStrictPack` 对应冻结关卡；上一版 Demo 主审查入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAReferenceComplexLong7/` | ReferenceComplex 7 关复杂长链冻结目录 | `ReferenceComplexLong7Pack` 对应冻结关卡；用于单独审查复杂长链补充样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction52V7RefComplexLong/` | V7 52 关复杂长链硬关池冻结目录 | `HardProduction52V7RefComplexLongPack` 对应冻结关卡；当前 Demo 审查入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardMidChainProbe5V1/` | 中型链组 5 关 probe 冻结目录 | `HardMidChainProbe5V1Pack` 对应冻结关卡，用于专门审查 40+ 链低选择方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardMidGateTight3V8/` | V8 MidGate 3 关中型链组突破样本 | `HardMidGateTight3V8Pack` 对应冻结关卡；验证 `StageGateSearch + MinOutputChains>=36` 后的紧曲线/强长链小包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction55V8ProbeMidGate/` | V8Probe55 复杂长链硬关审查池 | `HardProduction55V8ProbeMidGatePack` 对应冻结关卡；`V7 52 + MidGate 3` 合并审查包，当前 Demo 审查入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDARefComplexSalvage3V9/` | V9 RefComplex salvage 3 关冻结目录 | `RefComplexSalvage3V9Pack` 对应冻结关卡；从已生成 refcomplex candidates 中严格回收的低选择补充样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction58V9ProbeRefSalvage/` | V9Probe58 复杂长链硬关审查池 | `HardProduction58V9ProbeRefSalvagePack` 对应冻结关卡；`V8Probe55 + RefComplexSalvage3` 合并审查包，当前 Demo 审查入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAAllHistorySalvage8V10/` | V10 all-history salvage 8 关冻结目录 | `AllHistorySalvage8V10Pack` 对应冻结关卡；从历史 StageLock candidates 中排除 V9 source hash 后严格回收的补充样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction66V10ProbeAllHistorySalvage/` | V10Probe66 复杂长链硬关审查池 | `HardProduction66V10ProbeAllHistorySalvagePack` 对应冻结关卡；`V9Probe58 + AllHistorySalvage8` 合并审查包，当前 Demo 审查入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction68V11ProbeHardMidWideDBStageGate/` | V11Probe68 中型长链硬关审查池 | `HardProduction68V11ProbeHardMidWideDBStageGatePack` 对应冻结关卡；`V10Probe66 + hardmid_wide DoorBalanced StageGate 2` 合并审查包，当前 Demo 审查入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction72V12ProbeHardMidWideDBMicro/` | V12Probe72 中型长链硬关审查池 | `HardProduction72V12ProbeHardMidWideDBMicroPack` 对应冻结关卡；`V11Probe68 + hardmid_wide DoorBalanced micro 4` 合并审查包，当前 Demo 审查入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction75V13HardMidWideAuto/` | V13Probe75 自动源筛中型长链硬关审查池 | `HardProduction75V13HardMidWideAutoPack` 对应冻结关卡；`V12Probe72 + hardmid_wide auto feed 3` 合并审查包，当前 Demo 审查入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_profile.csv` | SGP 源结构画像 CSV | 给 StageLock 源排序使用的 `longChainRate/structureCarrierRate/complexChainScore` 等结构指标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_gate_v2_profile.csv` | SGP 源结构 + gate potential 画像 CSV | 新增 `gatePotentialScore/gateLateRegionCount/gateCandidateCount` 等门锁潜力诊断；当前只能辅助筛源，不能单独预测出货 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_stage_lock_targeted.csv` | StageLock targeted 源 feed CSV | 由 `Build-StageLockTargetedSourceFeed.ps1` 输出，供 DepAware 源增强优先扫描 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_stage_lock_highyield.csv` | StageLock HighYield 源 feed CSV | 严控源开放度的高命中源 feed；适合主难关量产验证 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_stage_lock_reference_long.csv` | StageLock ReferenceLong 源 feed CSV | 参考 298 seed 中长链画像的低产高复杂补充 feed |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_stage_lock_reference_complex.csv` | StageLock ReferenceComplex 源 feed CSV | 参考 298 seed top complex 画像的复杂长链补充源 feed；低产精品 lane，不作为主产线 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_stage_lock_hardxl.csv` | StageLock HardXL 大源诊断 feed CSV | 直接挑 70+ 链大源测试后处理上限；当前结论为 0 产出，不作为默认量产入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_stage_lock_hardmid.csv` | StageLock HardMid 源 feed CSV | 45-90 链中等规模源，StageDoor 小批已产出 3 个 S 候选；用于验证比 HardXL 更可控的大一点硬关母体 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_stage_lock_hardmid_wide.csv` | StageLock HardMidWide 源 feed CSV | 放宽后的 40-95 链源 feed，StageDoor 小批已产出 section/lock 候选；用于补同父本替换样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hm36gate_stagegate30_tight_selected_hard.csv` | V8 MidGate 3 关精选 CSV | `StageGateSearch` 在 `GateStrong` 中链源上的 36+ 链突破样本，已 trace 全部 `S/Low` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v8_probe55_with_midgate.csv` | V8Probe55 合并 CSV | `V7 52 + V8 MidGate 3` 合并审查池，平均 `traceAvgChoices=3.428`、`traceMaxChoices=6.018`、`avgChain=12.111` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_refcomplex_salvage_selected_hard.csv` | V9 RefComplex salvage 3 关精选 CSV | 从 refcomplex candidates 中排除 V8 已有 hash 后回收的 3 个补充样本，trace 全部 `S/Low` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v9_probe58_refsalvage.csv` | V9Probe58 合并 CSV | `V8Probe55 + RefComplexSalvage3` 合并审查池，平均 `traceAvgChoices=3.418`、`traceMaxChoices=6.086`、`avgChain=12.016` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v9_refcomplex_salvage_notes.md` | V9 RefComplex salvage 复盘报告 | 查看 salvage filter、trace 结果、精选结果和 V9 汇总指标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_allhistory_salvage_v10_selected_hard.csv` | V10 all-history salvage 8 关精选 CSV | 扫描历史 StageLock candidates、排除 V9 source hash 后精选的 8 个补充样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v10_probe66_allhistorysalvage.csv` | V10Probe66 合并 CSV | `V9Probe58 + AllHistorySalvage8` 合并审查池，平均 `traceAvgChoices=3.438`、`traceMaxChoices=6.136`、`avgChain=11.956` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v10_allhistory_salvage_notes.md` | V10 all-history salvage 复盘报告 | 记录 196 行历史 candidates -> 42 trace 成功 -> 8 strict hard selected -> V10Probe66 的流程和指标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v11_probe68_hardmidwide_db_stagegate.csv` | V11Probe68 合并 CSV | `V10Probe66 + hardmid_wide DoorBalanced StageGate 2` 合并审查池，平均 `traceAvgChoices=3.434`、`traceMaxChoices=6.147`、`longChainRate=0.483` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v12_probe72_hardmidwide_db_micro.csv` | V12Probe72 合并 CSV | `V11Probe68 + hardmid_wide DoorBalanced micro 4` 合并审查池，平均 `traceAvgChoices=3.441`、`traceMaxChoices=6.181`、`chains avg=33.819` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v13_probe75_hardmidwide_auto.csv` | V13Probe75 合并 CSV | `V12Probe72 + hardmid_wide auto feed 3` 合并审查池，75 关，平均 `traceAvgChoices=3.447`、`traceMaxChoices=6.213`、`chains avg=33.987`、`longChainRate avg=0.486` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v11_hardmidwide_db_stagegate_notes.md` | V11 hardmid_wide DoorBalanced 复盘报告 | 记录 DoorBalanced 比 GateStrong 更适合 hardmid_wide 微切片，新增 37/40 链真实 S 候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v12_hardmidwide_db_micro_notes.md` | V12 hardmid_wide DoorBalanced micro 复盘报告 | 记录新增微切片脚本、existing next6 feed、4 个 36+ 链新增样本和 V12Probe72 指标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_stage_lock_hardmid_wide_micro_auto_v13.csv` | V13 hardmid_wide 自动微切片源 feed | 由 `Build-HardMidWideMicroSourceFeed.ps1` 生成，排除 V12 next6 后选出 15 个可试父源，供 DoorBalanced micro-slice 使用 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v13_hardmidwide_auto_notes.md` | V13 hardmid_wide 自动源筛复盘报告 | 记录 auto feed、auto00/02 正结果、auto04/06/08/10 负结果、V13Probe75 指标和下一步 StageDoor trace 预筛方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_door_source_minority_success_v1.csv` | StageDoor 少数 family 成功源增强 feed | 从 `sweep/maze/dense/zig` 等少数成功源做 StageDoor 变体，已产出 maze/dense S 候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_lock_source_symmetry_minority_sd_sym2.csv` | StageDoor minority symmetry 源 feed | 7 个 minority success 源经 FlipX/FlipY/Rot180 生成的 21 个 symmetry source；注意需用显式数组传 transforms |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_door_source_minority_sym2_v1.csv` | StageDoor minority symmetry 源增强 feed | `stage_lock_source_symmetry_minority_sd_sym2.csv` 再经 StageDoor 生成的 39 个合法源 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_lock_source_symmetry_sdsym_minority_r1.csv` | StageDoor symmetry pipeline minority 源 feed | 新流水线自动生成的 21 个 minority symmetry source |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_door_source_sdsym_minority_r1.csv` | StageDoor symmetry pipeline minority 增强源 feed | 新流水线自动生成的 39 个合法 StageDoor source |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_lock_source_symmetry_sdsym_highchain_r1.csv` | StageDoor symmetry pipeline highchain 源 feed | 12 个 highchain success 源经 symmetry 生成的 36 个 source |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_door_source_sdsym_highchain_r1.csv` | StageDoor symmetry pipeline highchain 增强源 feed | highchain symmetry 再经 StageDoor 生成的 72 个合法源；最终仅产 1 个精选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_lock_source_symmetry_sdsym_v3head12_r1.csv` | StageDoor V3 proven head12 symmetry feed | V3 proven head12 生成的 36 个 symmetry source |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_door_source_sdsym_v3head12_r1.csv` | StageDoor V3 proven head12 增强源 feed | V3 proven head12 symmetry 再经 StageDoor 生成的 72 个合法源 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_lock_source_symmetry_sdsym_v3tail11_r1.csv` | StageDoor V3 proven tail11 symmetry feed | V3 proven tail11 生成的 33 个 symmetry source |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_door_source_sdsym_v3tail11_r1.csv` | StageDoor V3 proven tail11 增强源 feed | V3 proven tail11 symmetry 再经 StageDoor 生成的 63 个合法源 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_lock_success_highchain_source_feed_v1.csv` | StageLock 高链成功源自举 feed CSV | 从历史高链成功候选反查 sourcePath，用于 highchain symmetry/bootstrap 补充复杂长链样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_sym_highchain12_selected_hard_sig1.csv` | StageLock highchain strict 结构去重精选 CSV | `sym_highchain12` 候选按 `MaxPerStructureSignature=1` 精选出的 2 个复杂长链补充 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_ArchitecturalLineworkSGP01_ProbePack.asset` | ArchitecturalLinework constructive probe 包 | 建筑线稿反解式构造实验当前 Demo 包；Constructive02 为 3 关，链条 19-25、coverage 0.44-0.48、真实 trace 全部 S |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/architectural_linework_constructive02_report_20260620.md` | ArchitecturalLinework Constructive02 报告 | 记录建筑迷宫方向的切入点、指标、当前缺口和下一步 density/依赖环突破计划 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SGPBuildingArchlikePresentable5Pack.asset` | SGP-only 建筑线稿可看 probe 5 关包 | 从 643 个既有 SGP 源按建筑画像 + Trace + 视觉筛选冻结；Demo 当前挂载该包，作为纠偏后的可看方向样本 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_building_archlike_presentable5_frozen_trace_metrics.csv` | SGP-only 建筑线稿可看 probe trace 指标 | 查看 5 关冻结后真实过程曲线；结果为 A=3/B=2、missing=0 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_lock_source_symmetry_highyield_head12.csv` | StageLock HighYield 头部几何扩展源 feed | 由 `Build-StageLockSymmetrySourceVariants.ps1` 生成的 36 个 Flip/Rot 源变体 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_lock_source_symmetry_symhyrestbias.csv` | StageLock HighYield 尾部几何扩展源 feed | 由 `Run-StageLockSymmetryExpansionSlice.ps1` 生成的 57 个尾部 Flip/Rot 源变体，低产但补充了 1 个入池候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_lock_source_enhanced_targeted_depaware_slice02.csv` | StageLock targeted DepAware 第二切片源增强 CSV | 第二切片 `SourceOffset=40` 的增强源输出；用于分析后续切片产率 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/ReferenceSeeds/ArrowForge298/` | 外部 298 seed 结构参考副本 | 仅用于结构画像和指标校准，不直接混入正式生产包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/ReferenceSeeds/ArrowForge298Full_20260618_100431/` | 外部 298 seed 完整参考副本 | 从 `G:\Unityproject\ArrowCopy_DependencyGeneratorV0_20260609\ArrowForgePackExports\SingleLevelSeedLevelPacks_ByLevelName_298_20260618_100431` 复制的完整 298 seed，仅用于结构画像/指标校准 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/ReferenceSeeds/ArrowForge298FullLevels_20260618_100431/` | 外部 298 seed LevelDefinition 本体副本 | 从 G 盘源关卡复制的 298 个 LevelDefinition；用于真实过程 trace，不混入正式生成包 |

## Generated and Experimental

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `Assets/ArrowMagic/SOData/Levels/Campaign500Optimization/` | Campaign500 优化候选 | ShapeRefresh、DirectChainFlavor 等候选 |
| `Assets/ArrowMagic/SOData/Levels/Campaign500SingleLevelPool/` | 单关卡候选池 | 单关卡 pool、rejected、筛选任务 |
| `Assets/ArrowMagic/SOData/Levels/DirectProcedural/` | 直接 procedural 候选 | direct/normal/polish/topup 候选 |
| `Assets/ArrowMagic/SOData/Levels/HoleProcedural/` | hole procedural 候选 | hole 候选池、高 chain、topup |
| `Assets/ArrowMagic/SOData/Levels/NoMaskProcedural/` | 无 mask procedural 候选 | 无 mask 风格、类型、预览 |
| `Assets/ArrowMagic/SOData/Levels/LevelImportV1/` | 导入关卡批次 | 导入教程/正式数据排查 |
| `Assets/ArrowMagic/SOData/Levels/Generated/` | 历史/批量生成关卡 | R1/R2、CompositeSeedVariants、MaskPreview 等历史生成 |
| `Assets/ArrowMagic/SOData/Levels/ShapeExperiment/` | shape 实验关卡 | shape candidates、KeepEarly、Deprecated |
| `Assets/ArrowMagic/SOData/Packs/ShapeExperiment/` | shape 实验 pack | shape preview/keep/deprecated pack |
| `Assets/ArrowMagic/SOData/Reports/ShapeExperiment/` | shape 实验报告 | mask contact sheet、catalog、review、retrospective |
| `Assets/ArrowMagic/Masks/ShapeIconMaskOnlyBatch*/` | shape icon mask 批次 | 形状 mask 批量生成和审查；收尾时区分正式/实验 |

## Legacy or Baseline Data

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `Assets/ArrowMagic/SOData/Levels/Early_20_30/` | 早期 20-30 关卡 JSON | 对照早期难度/格式 |
| `Assets/ArrowMagic/SOData/Levels/Normal_40_70/` | normal 40-70 关卡 JSON | 对照 normal 难度/格式 |
| `Assets/ArrowMagic/SOData/Levels/Seeds/` | seed 候选 | R2 final candidate pool、seed 来源排查 |

## Export and Review

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `Exports/` | 交付 zip、xlsx、readme 和导出目录 | 交付检查、打包核对；不要把临时导出误认为源数据 |
| `TempContactSheets/` | 临时 contact sheets | 图片审查、实验收尾清理 |
| `Assets/ArrowMagic/Reports/` | Unity 项目内报告资源 | 生产报告、人工检查材料 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SGPBuildingMazeStageLock2Pack.asset` | SGP-only 建筑迷宫 StageLock 2 关突破包 | 当前 Demo 挂载包；2/2 真实 Trace 为 S，建筑画像源经 StageLock/依赖合链压难度得到，用于验收“神似 + 难度”交集方向 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_building_maze_stage_lock2_frozen_trace_metrics.csv` | SGP-only 建筑迷宫 StageLock2 trace 指标 | 查看当前 2 关冻结后真实过程曲线；`S=2`、`over10Rate=0`、`stageLockScore avg=0.593` |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/building_grammar_interleave_activation_v0_20260620.md` | Building Grammar interleave activation v0 报告 | 查看 GPT dual-spine 建议落地、离线 flip probe 结果、为什么后验 flip 不足以破局，以及下一步 Cross-Potential/source-orientation 前置方向 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/building_grammar_cps_interleave_v1_20260620.md` | Building Grammar CPS Interleave v1 报告 | 查看本轮目标更新、CPS 父源筛、dominance trim、dependency anchor、realized follow-run repair 等连续实验；v167-v170 已确认 `12,2` 可破 h91 follow-run，但 stage-aware pair/triple 批量为 0 selected，当前翻链会过早打开 late gate |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/DependencyFollowRunPairRepair/` | Building Grammar pair repair 导出关卡目录 | `Build-DependencyFollowRunReport.ps1` 导出的 follow-run pair repair LevelDefinition；当前 validated h91 样本仅 `12,2` 真可解 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/depanchor_v164_h91_pairrepair_validated_trace_metrics.csv` | h91 pair repair validated trace | 查看 `12,2` pair repair 的完整 trace：2/2 `S/S`、`avgChoices=2.86`、`maxChoices=4`、`dependencyFollowRunMax=2`、`stageLockScore=0.506` |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stageaware_pairrepair_depanchor_triple_v1_joined.csv` | depanchor stage-aware pair/triple repair 汇总 | v160-v166 depanchor repair 批量 full trace 汇总；10 个 repair 全部因 `stageLock/lateRegions` 等拒绝，证明当前翻链打断会破 late stage |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_D176StrictPack.asset` | DepAnchor v176 strict review pack | Building Grammar lane 第一次 strict 过线 pack；4 关，`dependencyFollowRunMax=2`、`stageLockScore≈0.822`、`lateRegionCount=3` |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/D176Strict/` | DepAnchor v176 strict 冻结关卡目录 | `SGPRhythmLab_D176StrictPack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/depanchor_v176_pairrepair_10_8_strict_selected.csv` | DepAnchor v176 strict selected CSV | 只保留 stage-preserving `10,8` pair repair，排除 `10,2` late-region collapse |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/trace_depanchor_v176_pairrepair_metrics.csv` | DepAnchor v176 pair repair full trace | 验证 `10,8` strict repair 4/4 solved、S/tight S；`10,2` 虽 followRun=2 但 stage collapse |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/depanchor_v177_smoke_summary.md` | DepAnchor strict runner smoke summary | 自动化 runner 小样，4 source -> 2 strict，验证流水线可复现 v176 top hcd5 族 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/depanchor_v178_offset4_summary.md` | DepAnchor strict runner offset4 summary | 后续源 close-but-not-strict 诊断；候选有低选择/高 stage，但没有可解 strict pair repair |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/depanchor_v179_offset12_summary.md` | DepAnchor strict runner offset12 summary | V20 中段源诊断；7 candidates、0 pair repair、0 strict |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/depanchor_v180_offset20_summary.md` | DepAnchor strict runner offset20 summary | V20 后段源诊断；5 candidates、0 pair repair、0 strict |



| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hardmidwide_stage_door_trace_cache_v14c.csv` | V14c HardMidWide StageDoor trace-aware cache | 父源级反馈缓存，23 个 parent rows；用于判断哪些 hardmid_wide 父源已证伪或已出货 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hardmidwide_reference_gap_v14c.csv` | hardmid_wide 与 298 参考 seed 结构差距报告 CSV | 对比 reference298 top complex、SGP hardmidwide、成功/失败父源的 avgChain、longChainRate、veryLongChainRate、straightLikeRate 等指标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v15_productive_refit_notes.md` | V15 productive refit 复盘报告 | 记录 reference-complex parent 负结果、productive refit 正结果、V15 76 关 pack 和下一步方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v15_probe_refit_merge.csv` | V15 productive refit 合并 CSV | V13 75 + productive refit 去重后 76 关 hard candidate pool |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction76V15ProductiveRefitPack.asset` | V15 productive refit 76 关冻结包 | 当前 Demo activePack，新增 1 个强合链 refit hard candidate，供人工审查 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction76V15ProductiveRefit/` | V15 productive refit 冻结关卡目录 | `HardProduction76V15ProductiveRefitPack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-ProductiveRefitStageDoorSourceFeed.ps1` | Productive Refit all-history 源筛脚本 | 以现有 hard pool 为排除集，从历史 StageDoor source 中找未入池且适合二次强合链的源 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_door_source_productive_refit_allhistory_v16.csv` | V16 productive refit all-history feed | 从 44 个 StageDoor source 文件筛出的 24 个未入池源；head12 负、tail12 正 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v16_productive_refit_allhistory_notes.md` | V16 productive refit all-history 报告 | 记录全历史源筛、head/tail 产率、2 个新增 hard candidate 和 V16 pack 指标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v16_probe_refit_allhistory_merge.csv` | V16 productive refit all-history 合并 CSV | V15 76 + all-history productive refit 2，合并后 78 关 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction78V16ProductiveRefitAllHistoryPack.asset` | V16 productive refit all-history 78 关冻结包 | 当前 Demo activePack，新增 2 个 all-history refit hard candidates |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction78V16ProductiveRefitAllHistory/` | V16 productive refit all-history 冻结关卡目录 | `HardProduction78V16ProductiveRefitAllHistoryPack` 对应 LevelDefinition 副本 |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction81V17ProductiveRetryPack.asset` | V17 Productive Retry 81 关硬关审查包 | 当前 Demo activePack；V16 78 + productive retry 新增 3 个 S 级中长链 hard candidates |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v17_productive_retry_notes.md` | V17 Productive Retry 复盘报告 | 查看 orientable-history 筛源、strong-chain 负结果、3 个新增候选和下一步 orientation-risk cache 计划 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v17_probe_productive_retry_merge.csv` | V17 Productive Retry 合并 CSV | `HardProduction81V17ProductiveRetryPack` 的冻结输入，81 个去重 hard candidates |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction82V18RiskAwarePack.asset` | V18 RiskAware 82 关硬关审查包 | 当前 Demo activePack；V17 81 + risk-aware clean8 新增 1 个 S/S 中长链 hard candidate |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v18_riskaware_notes.md` | V18 RiskAware 复盘报告 | 查看 risk cache、clean8 小切片、V18 新增样本和当前 source orientability 瓶颈 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v18_probe_riskaware_merge.csv` | V18 RiskAware 合并 CSV | `HardProduction82V18RiskAwarePack` 的冻结输入，82 个去重 hard candidates |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction84V19BaseRiskAwarePack.asset` | V19 BaseRiskAware 84 关硬关审查包 | 当前 Demo activePack；V18 82 + source-hash risk-aware 低 orientation 子集新增 2 个 S/A section_unlock hard candidates |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v19_base_riskaware_notes.md` | V19 BaseRiskAware 复盘报告 | 查看 source-hash risk cache、低 orientation 子集 2 个新增样本和少数 family 0 产负结果 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v19_probe_base_riskaware_merge.csv` | V19 BaseRiskAware 合并 CSV | `HardProduction84V19BaseRiskAwarePack` 的冻结输入，84 个去重 hard candidates |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction88V20HMWTraceAwarePack.asset` | V20 HardMidWide trace-aware 88 关硬关审查包 | 当前 Demo activePack；V19 84 + HardMidWide trace-aware 长链补强新增 4 个 S 级候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v20_hmw_traceaware_notes.md` | V20 HardMidWide trace-aware 复盘报告 | 查看 GateLite minority 负结果、HardMidWide trace-aware 正结果、4 个新增长链候选和下一步 chunk prefilter 方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v20_probe_hmw_traceaware_merge.csv` | V20 HardMidWide trace-aware 合并 CSV | `HardProduction88V20HMWTraceAwarePack` 的冻结输入，88 个去重 hard candidates |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_productive_refit_v21_probe_summary.csv` | V21 ProductiveRefit full probe 汇总 CSV | 记录 4 个 trace S 但未过复杂长链 strict select 的候选；可作为后续低选择压力 hard lane 的回收池 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_v21_trace_metrics_calibration_20260620.md` | Hard Lane V2.1 trace 指标校准报告 | 查看 V2.1 窗口化/事件化指标、V20/VeryLong/ProductiveRefit 校准结果、推荐阈值和为什么 V2.1 只能作为 rhythm gate 而不能替代链条结构门槛 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v20_v21metrics_selector_probe.csv` | V20 经 V2.1 rhythm gate 重筛结果 | V20 88 关用 `MaxBoringLinearScore=0.46`、`MinStructuredHardnessV21=0.58` 重筛后保留 73 关；用于分析节奏平铺/结构硬度不足样本，不是正式冻结包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_vlgate_v21metrics_selector_probe.csv` | VeryLong pressure + V2.1 selector probe | 验证 VeryLong 长尾门锁候选在 V2.1 节奏门槛和旧结构门槛下仍能保留；用于后续 HMW/VeryLong 小切片阈值参考 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_v21_nearmiss_catalog_20260620.csv` | Hard Lane V2.1 near-miss 目录 | 收集 `structuredHardnessV21>=0.70`、低 boring、低 maxChoices，但因 `avgChain/longChainRate/carrier` 被拒的节奏硬候选；用于后续 `NearMissChainRescue` 批量验证 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hmw_v20_nearmiss_rescue_breakthrough2.csv` | NearMissRescue breakthrough 2 关 CSV | V20 trace-aware c05/c06 经 `NearMissChainRescue` 推过 strict hard select 的 2 个 section_unlock 样本，均为真实 trace S，`mergedCarrierAbsorbCount=3` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockNearMissRescueBreakthrough2Pack.asset` | NearMissRescue breakthrough 2 关 review pack | 单独冻结的 2 关 review 包；未挂 Demo，未替换 V20，用于后续人工看图或继续合并 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDANearMissRescueBreakthrough2/` | NearMissRescue breakthrough 冻结关卡目录 | `NearMissRescueBreakthrough2Pack` 对应的 LevelDefinition 副本，避免后续 preview 覆盖 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_nearmiss_rescue_v21_budget_breakthrough3.csv` | NearMissRescue budget breakthrough 3 关 CSV | c05/c06 section_unlock 2 关 + ProductiveRefit dual_zone 1 关，均 strict hard selected 且真实 trace S；用于验证预算化 rescue 已跨 family 生效 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockNearMissRescueBudgetBreakthrough3Pack.asset` | NearMissRescue budget breakthrough 3 关 review pack | 单独冻结的 3 关 review 包；未挂 Demo，未替换 V20，用于后续人工看图或作为 source-level rescue prefilter 正样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDANearMissRescueBudgetBreakthrough3/` | NearMissRescue budget breakthrough 冻结关卡目录 | `NearMissRescueBudgetBreakthrough3Pack` 对应的 LevelDefinition 副本，避免后续 preview 覆盖 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_door_source_nearmiss_rescue_prefilter_v1.csv` | NearMissRescue source prefilter v1 | 由 `Build-NearMissRescueSourceFeed.ps1` 从历史 rejected 中筛出的 22 个可救源；用于后续低预算单源 rescue 小批 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_nearmiss_rescue_v21_budget_breakthrough4.csv` | NearMissRescue budget breakthrough 4 关 CSV | breakthrough3 加 prefilter dense h4f810，覆盖 section_unlock/dual_zone/dense，均 strict hard selected 且真实 trace S |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockNearMissRescueBudgetBreakthrough4Pack.asset` | NearMissRescue budget breakthrough 4 关 review pack | 单独冻结的 4 关 review 包；未挂 Demo，未替换 V20，用于验证 source-level prefilter + budget rescue 正路线 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDANearMissRescueBudgetBreakthrough4/` | NearMissRescue budget breakthrough4 冻结关卡目录 | `NearMissRescueBudgetBreakthrough4Pack` 对应的 LevelDefinition 副本，避免后续 preview 覆盖 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_nearmiss_rescue_v22_review5.csv` | NearMissRescue V2.2 Review5 CSV | V2.2 初版精选 5 关；压掉 `choicePeak>=8`，限制外圈近距离/同边顺消，保留复杂长链结构 |

## SGP Rhythm Lab Support Closure V1 Reports

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/support_graph_closure_v1_notes_20260622.md` | Support Graph Closure V1 复盘报告 | 记录 bounded support closure 落地、静态 source gate、StageLock `SupportClosureBias`、planned-deps 假阳性、causal gate 纠偏和当前瓶颈：静态闭包必须保留为真实 solve-order closure |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_outerclean_anchor_supportclosure_v1b_probe40.csv` | SupportClosure source gate 5 源 CSV | `-SupportGraphClosureAnchor` 生成的强静态闭包源；静态分数高，但不能直接视为硬关 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_outerclean_anchor_supportclosure_v1b_probe40_trace_metrics.csv` | SupportClosure source gate trace | 5 个静态闭包源的真实 trace；仅 1/5 solved A，其余 Drop，证明静态 support graph 会在真实解链中塌掉 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_supportclosure_v1b_stagelock_closurebias_candidates.csv` | StageLock planned SupportClosureBias 候选 CSV | planned dependency closure 版本产出的 2 个候选；生成侧看似闭包成立，但 trace 后证明是假阳性 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_supportclosure_v1b_stagelock_closurebias_trace_metrics.csv` | StageLock planned SupportClosureBias trace | 2/2 solved B，但 trace-side closure 分数为 0，仍是 LocalEasy，用于对照 causal gate |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_supportclosure_v1b_stagelock_causal_lowbudget_rejected.csv` | StageLock causal SupportClosureBias 低预算拒绝 CSV | causal closure gate 小批复测；5 源 0 产，`support closure too weak=3`、`local patch burst too linear=2`，证明新 gate 能挡住 planned/static closure 假阳性 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_supportclosure_v1b_stagelock_bridge_diag_rejected.csv` | Support Feasibility Bridge V0 断点诊断 CSV | 5 源 0 产；输出 `supportClosureFailureClass`，定位失败主要为 `upstreamIsConveyor/supportIsConveyor/missingCrossRegionSupport` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_supportclosure_v1b_stagelock_bridge_penalty_rejected.csv` | Support closure failure penalty 复测 CSV | failure class 已参与 StageLock 搜索惩罚，但仍 0 产，证明不是简单选向排序问题 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_supportclosure_v1b_stagelock_preserve_target_rejected.csv` | Preserve anchor target 复测 CSV | 只保留 anchor target 方向后仍 0 产，且全部先被 `dependency follow too linear` 拒绝 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_supportclosure_v1b_stagelock_preserve_support_rejected.csv` | Preserve anchor support 复测 CSV | 同时保留 target 与 hub/support 源层方向后仍 0 产，失败集中 `upstreamIsConveyor=4/supportIsConveyor=1`；下一步应做 upstream support bridge 注入/重接 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_outerclean_anchor_supclosure_bridge_v0.csv` | Strict depth-2 support bridge source probe | 现有 source 生成器加 `HubSupportRequireInitialClearable/ValidateRealized` 后 20 源产 1 个 realized depth-2 support bridge source |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_supportclosure_bridge_v0_stagelock_rejected.csv` | Strict depth-2 support bridge StageLock probe | depth-2 source 接 StageLock 后 0 产，失败为 `upstreamIsConveyor`，说明 support 上游仍退化为 conveyor |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_outerclean_anchor_supclosure_bridge_d3_wide_v0.csv` | Strict depth-3 support bridge source probe | 80 源、3 variants、`SupportClosureMinDepth=3` 仍 0 产；说明当前池缺 realized depth-3 upstream bridge，不能靠筛选解决 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLaneV3SGPNativeLocalPatchReview3Pack.asset` | SGP-native LocalPatch Review3 对照包 | 2026-06-21 纠偏后冻结的 SGP/StageDoor/StageLock 主线对照包；Demo 已挂该包。第 1 关是当前最干净 SGP-native 样本，第 2/3 关保留为顺链/局部顺消对照，不是最终量产包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hardlane_v3_sgp_native_localpatch_review3_frozen_metrics.csv` | SGP-native LocalPatch Review3 冻结 trace | 查看 3 关冻结副本的真实过程指标；用于比较 `dependencyFollowRunMax`、`localPatchSolveRunMax`、`outerExitHeadCount` 与体感难度 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockNearMissRescueV22Review5Pack.asset` | NearMissRescue V2.2 Review5 pack | 当前 Demo activePack；5/5 trace S/S，`avgChoices=3.594`、`maxChoices max=7`、`choicePeakCount max=0` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDANearMissRescueV22Review5/` | NearMissRescue V2.2 Review5 冻结关卡目录 | `NearMissRescueV22Review5Pack` 对应 LevelDefinition 副本，后续试跑不会覆盖 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_nearmiss_rescue_v22_review5_frozen_trace_metrics.csv` | NearMissRescue V2.2 Review5 frozen trace | 冻结包实际展示关卡的真实过程指标；用于确认 Demo 包与 V2.2 gate 一致 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_v22_nearmiss_rescue_review5_notes_20260620.md` | Hard Lane V2.2 Review5 复盘报告 | 记录 V2.2 新指标、筛选阈值、补跑尝试、入选/拒绝原因和下一步非 section family 补强方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_v23_outer_exit_pressure_diagnosis_20260621.md` | Hard Lane V2.3 外出口诊断报告 | 解释 V2.2 为何控不住外出口/边缘连续消除，并记录 Review5 在 V2.3 外圈可见压力 gate 下 0/5 通过的诊断结果 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_nearmiss_rescue_v23_outerexit_retrace_metrics.csv` | Review5 V2.3 外出口 trace | 当前 V2.2 Review5 用 V2.3 新指标重放后的 trace；用于定位 `outerAvailableChoiceMax` 与 `sameSideOuterExitAvailableChoiceMax` 问题 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockV24NoSimpleShellPreview6Pack.asset` | Hard Lane V2.4 无简单外壳预览 6 关包 | 第一阶段验证包：`outerSimpleChainCount=0`、`outerLongStraightChainCount=0`，但动态外圈可见压力仍偏高；用于对比外圈简单壳链是否改善 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_v24_outer_shell_pressure_notes_20260621.md` | Hard Lane V2.4 外圈壳链复盘 | 查看为什么外圈问题不能只靠 V2.3 外出口指标，生成器新增 `OuterShellPressureGate` 做了什么，以及 V2.5 动态外圈压力待办 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v24_no_simple_shell_preview6_frozen_trace_metrics.csv` | V2.4 无简单外壳预览包 trace | 6 关冻结包真实过程指标；确认静态外壳已控住但 `outerAvailableChoiceMax/HeavyStepRate` 仍需继续压 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockV25OuterAvailReview3Pack.asset` | Hard Lane V2.5 动态外圈压力 Review3 包 | 3 关突破样本；普通外圈 heavy 明显下降，仍需继续压同侧外圈和外出口连续解 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAV25OuterAvailReview3/` | V2.5 动态外圈压力 Review3 冻结关卡目录 | `V25OuterAvailReview3Pack` 对应 LevelDefinition；由 V20 broad source 经 V2.5 gate 和去重后冻结 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v25_outer_avail_review3_trace_metrics.csv` | V2.5 Review3 trace 指标 | 3/3 `S/S`，`outerAvailableHeavyStepRate avg=0.165`，用于对比 V2.4 的普通外圈动态压力 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_v25_outer_available_pressure_notes_20260621.md` | Hard Lane V2.5 动态外圈压力复盘 | 记录 strict gate 0 产、soft gate 正结果、Review3 指标、当前不足和 V2.6 建议 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockV26OuterExitRun2Pack.asset` | Hard Lane V2.6 外出口连续解对照 2 关包 | 2 关外出口 solve-run 控制突破样本；不替代当前 Demo review 包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v26_outer_exit_run2_trace_metrics.csv` | V2.6 外出口连续解 trace 指标 | 2/2 trace `S`，`outerExitSolveRunMax=1`、`sameSideOuterExitSolveRunMax=1`，但其中一关 `tightProcessTier=A/maxChoices=8` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_v26_outer_exit_and_skeleton_v1_notes_20260621.md` | V2.6 外出口与 Skeleton V1 复盘 | 记录生成侧 outer-exit solve-run gate、V2.6 probe 指标，以及 GPT Skeleton Graph 建议如何收束为 adapter 实验 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockSkeletonGateV1Review2Pack.asset` | Skeleton Gate V1 Review2 2 关包 | 当前 Demo activePack；Skeleton -> StageDoor GateStrong -> StageLock 的突破样本，2/2 trace `S/S`，外出口动态指标已控住 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDASkeletonGateV1Review2/` | Skeleton Gate V1 Review2 冻结关卡目录 | `SkeletonGateV1Review2Pack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_skeleton_gate_v1_review2_frozen_trace_metrics.csv` | Skeleton Gate V1 Review2 frozen trace | 2/2 `S/S`，`avgChoices=2.46/2.78`、`maxChoices=5/5`、`outerExitAvailableChoiceMax=1/1` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_skeleton_gate_v1_review2_notes_20260621.md` | Skeleton Gate V1 Review2 复盘 | 查看 Skeleton-only 失败原因、StageDoor GateStrong 正结果和剩余 `outer simple shell too dominant` 卡点 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockSkeletonGateV1Final6Pack.asset` | Skeleton Gate V1 Final6 6 关包 | 当前 Demo activePack；Review2 卡点修复后的小样最终包，6/6 frozen trace `S/S`，允许少量建筑外壳但动态外圈压力受控 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDASkeletonGateV1Final6/` | Skeleton Gate V1 Final6 冻结关卡目录 | `SkeletonGateV1Final6Pack` 对应 LevelDefinition 副本；包含 15x24 与 16x25 两组尺寸 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_skeleton_gate_v1_final6_frozen_trace_metrics.csv` | Skeleton Gate V1 Final6 frozen trace | 6/6 `S/S`，`avgChoices=2.50-3.08`、`maxChoices=5-6`、`outerStraightRunMax=0`、`outerExitSolveRunMax=0-1` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_skeleton_gate_v1_final6_notes_20260621.md` | Skeleton Gate V1 Final6 复盘 | 查看 `-AllowArchitecturalOuterShell` 的作用、Final6 指标、剩余风险和下一步扩模板计划 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockSkeletonGateV1DenseDepReview2Pack.asset` | Skeleton Gate V1 DenseDep Review2 2 关包 | 当前 Demo activePack；针对 Final6 覆盖不足/依赖不足反馈的高覆盖强依赖小样，2/2 frozen trace `S/S` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDASkeletonGateV1DenseDepReview2/` | Skeleton Gate V1 DenseDep Review2 冻结关卡目录 | `SkeletonGateV1DenseDepReview2Pack` 对应 LevelDefinition 副本，15x24 与 15x25 各 1 关 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_skeleton_gate_v1_dense_dep_review2_frozen_trace_metrics.csv` | Skeleton Gate V1 DenseDep Review2 frozen trace | 2/2 `S/S`，coverage=0.822/0.805，`stageLockScore=0.767/0.927`，`mergedDependencyCount=8/4` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_skeleton_gate_v1_dense_dep_review2_notes_20260621.md` | Skeleton Gate V1 DenseDep Review2 复盘 | 查看高覆盖模式、DenseOuterGuard/StrongDoorBridge 结果、产率问题和下一步 template 扩展方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockRoomDoorSkeletonV2Review3Pack.asset` | RoomDoor Skeleton V2 Review3 3 关包 | 当前 Demo activePack；room/hub/door 语义骨架小样，3/3 process S，2/3 tight S，第三关为 S/A 视觉备选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDARoomDoorSkeletonV2Review3/` | RoomDoor Skeleton V2 Review3 冻结关卡目录 | `RoomDoorSkeletonV2Review3Pack` 对应 LevelDefinition 副本；包含 15x25 与 16x25 样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_roomdoor_skeleton_v2_review3_frozen_trace_metrics.csv` | RoomDoor Skeleton V2 Review3 frozen trace | 3/3 `processTier=S`，2/3 `tightProcessTier=S`，candidate coverage≈0.800，`stageLockScore=0.683-0.835` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_roomdoor_skeleton_v2_review3_notes_20260621.md` | RoomDoor Skeleton V2 Review3 复盘 | 查看 V2 room/door 源层、StageDoor/StageLock 结果、产率问题和下一步 template 扩展计划 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockDependencySkeletonV3Probe2Pack.asset` | Dependency Skeleton V3 Probe2 2 关包 | 当前 Demo activePack；先构造物理依赖骨架再 StageDoor/StageLock 的小样，2/2 true trace `S/S`，avg choices 2.19/2.43 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDADependencySkeletonV3Probe2/` | Dependency Skeleton V3 Probe2 冻结关卡目录 | `DependencySkeletonV3Probe2Pack` 对应 LevelDefinition 副本；用于人工对比 RoomDoor V2 的依赖强度 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_dependency_skeleton_v3_probe2_frozen_trace_metrics.csv` | Dependency Skeleton V3 Probe2 frozen trace | 2/2 `processTier=S`、2/2 `tightProcessTier=S`，`stageLockScore=0.867-0.879`，`outerExitSolveRunMax=1` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_dependency_skeleton_v3_probe2_notes_20260621.md` | Dependency Skeleton V3 Probe2 复盘 | 查看 V3 源生成、StageDoor/StageLock 指标、与 RoomDoor V2 的关键差异和下一步 realized-dependency 报告建议 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockTrueHardDependencyV7Curated5Pack.asset` | True Hard Dependency V7 Curated5 5 关包 | 当前 Demo activePack；从自然高覆盖 SGP/StageDoor/StageLock 候选中按真实依赖难度精选，5/5 trace `S`，4/5 tight `S` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDATrueHardDependencyV7Curated5/` | True Hard Dependency V7 Curated5 冻结关卡目录 | `TrueHardDependencyV7Curated5Pack` 对应 LevelDefinition 副本，覆盖 section/sweep/dense/maze_long_chain 多样性 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_true_hard_dependency_v7_curated5_selected.csv` | True Hard Dependency V7 Curated5 选择输入 | 5 关精选 CSV，包含 trueHardScore、familyKey、依赖强度、trace 曲线和外圈压力字段 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_true_hard_dependency_v7_curated5_frozen_trace_metrics.csv` | True Hard Dependency V7 Curated5 frozen trace | 5/5 solved，`avgChoices=2.59-3.63`、`maxChoices=4-6`、`stageLockScore=0.606-0.849`、`outerAvailableHeavyStepRate=0.091-0.257` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_true_hard_dependency_v7_curated5_notes_20260621.md` | True Hard Dependency V7 Curated5 复盘 | 记录目标校准、选择策略、冻结包指标、风险和下一步 dependency-anchor 注入方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockTrueHardDependencyV14OuterHeadZeroVisualPack.asset` | True Hard Dependency V14 外圈零出口头审查包 | 当前 Demo activePack；2 关 focused review，专门验证外圈没有直接朝外头是否能改善外围低难度观感 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDATrueHardDependencyV14OuterHeadZeroVisual/` | V14 外圈零出口头冻结关卡目录 | `TrueHardDependencyV14OuterHeadZeroVisualPack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_true_hard_dependency_v14_outer_head_zero_visual_frozen_trace_metrics.csv` | V14 外圈零出口头 frozen trace | 2/2 `processTier=S`，`outerExitHeadCount=0/0`，`outerExitAvailableChoiceAvg=0/0`，`outerExitSolveRatio=0/0` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_true_hard_dependency_v14_outer_head_zero_notes_20260621.md` | V14 外圈零出口头复盘 | 查看静态外出口头指标、头尾修复 probe 失败原因、V14 指标和下一步生成侧先验建议 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_difficulty_score_v1_notes_20260621.md` | Hard Lane DifficultyScoreV1 校准报告 | 查看为什么 Greedy 可解不等于真实难度、`dependencyFollowRunMax` 如何识别“顺着消”、以及 V14/V20 校准结论 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_structure_v3_causal_unlock_notes_20260621.md` | HardStructure V3 因果解锁报告 | 查看 causal unlock graph 判别器、V3 字段定义、outer-clean anchor 160 样本校准、为什么当前可解样本仍是 `LocalEasy`、以及下一步 anti-local/cross-critical 目标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_outerclean_anchor_source_v3_wide160_hardv3_trace_metrics.csv` | Outer-clean anchor HardStructure V3 trace | 160 个 outer-clean anchor source 的 V3 trace 结果；15 solved，但 `HardPotential/TrueHardCandidate=0` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_outerclean_anchor_hardv3_nearmiss_feed.csv` | Outer-clean anchor HardStructure V3 near-miss feed | V3 near-miss 3 行；用于下一步尝试从 anti-local 但不可解/关键锁不足的源切入 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_true_hard_dependency_v14_difficulty_v1_trace_metrics.csv` | V14 DifficultyScoreV1 trace | 外圈零出口头样本的难度判别重放；用于确认 V14 虽外圈干净但 `dependencyFollowRunMax=3/4` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v20_difficulty_v1_trace_metrics.csv` | V20 DifficultyScoreV1 trace | 88 关旧池重放；用于寻找同时满足外圈干净和 non-follow 的候选，并校准下一轮生成/合链 gate |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockNonFollowOuterRepairV15Review3Pack.asset` | NonFollow Outer Repair V15 Review3 包 | 当前 Demo activePack；从 V20 non-follow 样本做外圈头 subset repair 后冻结，3/3 `S/S`、`dependencyFollowRunMax=2`、外圈压力较原样本下降 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/NFOuterV15Frozen/` | V15 NonFollow 外圈修复冻结关卡目录 | `NonFollowOuterRepairV15Review3Pack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/nonfollow_outer_repair_v15_review_summary.md` | V15 NonFollow 外圈修复 runner 总结 | 查看 source 数、repair 候选数、自动精选数和 top selected 指标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_nonfollow_outer_repair_v15_review3_frozen_trace_metrics.csv` | V15 NonFollow 外圈修复 frozen trace | 3/3 `S/S`，`avgChoices=3.20-3.76`、`maxChoices=5-6`、`dependencyFollowRunMax=2`、`outerExitHeadCount=3-4` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_nonfollow_outer_repair_v15_notes_20260621.md` | V15 NonFollow 外圈修复复盘 | 记录 generation-side non-follow 0 产、inverse repair lane 正结果和下一步稳定产线建议 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_nonfollow_outer_repair_v15_review3_localburst_retrace_metrics.csv` | V15 Local Patch Burst 重放 | 用新增 local patch 指标重放 V15 Review3；证明 `dependencyFollowRunMax=2` 仍会出现 `localWindow5NeighborMax=5` 的局部扫光 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v20_localburst_trace_metrics.csv` | V20 Local Patch Burst 源池重放 | 88 关旧 hard 池的 local patch 指标重放；用于判断旧源池是否存在“局部不扫光”的可修源 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/support_bridge_ray_probe_v1_source40.csv` | Support bridge ray 40 源静态物理桥 probe | 2026-06-22 按 `target.escapeRay -> hub -> support -> upstream` 物理碰撞扫描 outer-clean anchor 源；40 源找到 200 条 bridge row，其中 `FlipBlockedRay:support` 145 条、`ExistingRay` 55 条 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/support_bridge_ray_probe_v1_emit20_emitted_sources.csv` | Flip support bridge source 导出 CSV | 2026-06-22 导出 20 个 `FlipBlockedRay:support` source asset；直接 trace 代表 3 个均 `Drop` 且 `supportClosureBestScore=0`，说明盲翻 support 会破坏可达性 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/support_bridge_ray_probe_v1_existing_emit20_trace_metrics.csv` | ExistingRay bridge source trace | 2026-06-22 不翻链，仅导出已有物理桥 source 并 trace；20 个里 1 个 solved、5 个 depth-2 closure，但 solved 样本仍为 `LocalEasy`、`dependencyFollowRunMax=3`、`localPatchSolveRunMax=4` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/support_bridge_ray_probe_v1_multihop_emit20_trace_metrics.csv` | ExistingMultiHop bridge source trace | 2026-06-22 选择已有 `B -> U -> support -> hub -> targets` 多跳物理链；19 个导出 source trace 后 1/19 solved、4/19 depth-2 closure，最好 solved 仍为 `LocalEasy`，未突破 ExistingRay 上限 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/support_bridge_ray_probe_v1_immediate_hit_split_diagnostic.csv` | Immediate blocker split 诊断 | 2026-06-22 统计 support ray 贴脸命中 immediate blocker 的位置；20 源 768 个可用 support hit 全部 `hitDistance=1`，91 endpoint、87 near-endpoint，说明 dense body 无插入空位但存在端头切链机会 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/support_bridge_ray_probe_v1_split_diag_emit20_trace_metrics.csv` | SplitEndpointSidecar trace | 2026-06-22 将 immediate blocker 端头 3 格切成 sidecar 的诊断变体；20/20 `Drop`，仅 1/20 出现 depth-2 closure 且仍 `LocalEasy`，说明 endpoint split 不是可用主修复路线 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PressureReadStageLockSourceSupportBridgeRay/` | Support bridge ray probe 源资产目录 | `Build-SupportBridgeRayProbe.ps1 -EmitSources` 输出的实验 source assets；当前仅作物理桥验证，不是正式候选池 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_TraceBridgeProofV1Pack.asset` | Trace Bridge Proof V1 包 | 2026-06-22 proof-only 包；3 关用于验证 source-level ray-collision bridge 能 surviving StageLock、solve 和 independent trace replay，不是最终高难候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/TraceBridgeProofV1/` | Trace Bridge Proof V1 冻结关卡目录 | `SGPRhythmLab_TraceBridgeProofV1Pack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/trace_bridge_proof_v1_frozen_replay_input.csv` | Trace Bridge Proof V1 冻结 replay 输入 | 冻结 pack CSV 补回 planned bridge 的 T/H/S/U 字段，用于验证最终 asset 上 bridge 是否 trace-visible |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/trace_bridge_proof_v1_frozen_replay_with_bridge_metrics.csv` | Trace Bridge Proof V1 冻结 replay 指标 | 3/3 solved，3/3 planned bridge replay `ok`/depth=3，但仍 `LocalEasy`、`localPatchSolveRunMax=5`，说明只是物理因果桥 proof |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/trace_bridge_proof_v1_notes_20260622.md` | Trace Bridge Proof V1 复盘 | 记录 GPT 协作结论、代码改动、验证命令、结果表和下一步 stage-quality-aware source-slot bridge selection 方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLaneRealclosurePeakPruneV1Pack.asset` | Realclosure Peak Prune V1 hard-lane proof 包 | 2026-06-22 历史 proof 包；4/4 frozen board trace accepted，`maxChoices=7`、`localPatchSolveRunMax=3`、planned bridge replay `ok/d3`。这是控制中盘 unlock fanout 的 proof/review 包，不是最终量产策略 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLaneRealclosurePeakPruneV1Frozen/` | Realclosure Peak Prune V1 冻结关卡目录 | `SGPRhythmLab_HardLaneRealclosurePeakPruneV1Pack` 对应 LevelDefinition 副本；冻结 manifest 带 trace bridge anchor 字段，可二次 board trace 验证 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_realclosure_peak_prune_v1_frozen_board_gate_summary.md` | Realclosure Peak Prune V1 frozen board gate 总结 | 4/4 accepted；用于查看每关 avg/max choice、choice rise、stage/anti-local、follow/local run、support closure 和 planned bridge replay |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLaneRealclosureRelaySplitV2Pack.asset` | Realclosure Relay Split V2 hard-lane review 包 | 2026-06-22 当前 Demo activePack；4/4 frozen board trace accepted，最好两关 `maxChoices=5`、soft cap 5 hit 0、planned bridge replay `ok/d3`，用于人工验证 Fanout Dynamics V2 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLaneRealclosureRelaySplitV2Frozen/` | Realclosure Relay Split V2 冻结关卡目录 | `SGPRhythmLab_HardLaneRealclosureRelaySplitV2Pack` 对应 LevelDefinition 副本；chain 11 relay split 后覆盖不删格、链数 35 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_realclosure_relay_split_v2_frozen_board_gate_summary.md` | Realclosure Relay Split V2 frozen board gate 总结 | 4/4 accepted；记录 max choice、choice rise、stage/anti-local、local/follow run、support closure 与 planned bridge replay |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLaneRealclosureRelaySplitAutoV1Pack.asset` | Realclosure Relay Split Auto V1 5 关 hard-lane review 包 | 2026-06-22 当前 Demo activePack；从 2 个 realclosure orientation 源的 auto relay split 过线样本精选 5 关，frozen board gate 5/5 accepted，4 关 `maxChoices=5`、1 关 `maxChoices=6`、planned bridge replay 全部 `ok/d3` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLaneRealclosureRelaySplitAutoV1Frozen/` | Realclosure Relay Split Auto V1 冻结关卡目录 | `SGPRhythmLab_HardLaneRealclosureRelaySplitAutoV1Pack` 对应 LevelDefinition 副本；用于人工评估“至少 5 关真正有难度”的第一批 fanout dynamics 自动化样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_realclosure_relay_split_auto_v1_frozen_board_gate_summary.md` | Realclosure Relay Split Auto V1 frozen board gate 总结 | 5/5 accepted；验收规则为 `maxChoices<=6`、`localPatchSolveRunMax<=3`、`dependencyFollowRunMax<=3`、support closure depth >= 2、planned bridge replay required |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLaneDiversePerturbProbeV1Pack.asset` | Diverse Perturb Probe V1 5 关诊断包 | 2026-06-22 当前 Demo activePack；用于验证结构扰动是否能跳出 Auto V1 同质问题。第 1 关为 `MediumStructure`，其余为多 source 对照，不是最终高难产线 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLaneDiversePerturbProbeV1Frozen/` | Diverse Perturb Probe V1 冻结关卡目录 | `SGPRhythmLab_HardLaneDiversePerturbProbeV1Pack` 对应 LevelDefinition 副本；冻结后已重放 trace |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_diverse_perturb_probe_v1_frozen_metrics.csv` | Diverse Perturb Probe V1 frozen trace | 5 关冻结指标；第 1 关 `MediumStructure/anti=0.471/maxChoices=6/local=3/follow=3/outerHead=6`，第 4/5 关外圈干净但仍 `LocalEasy` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/DesignedHardLockV0/` | Designed Hard Lock V0 proof 关卡目录 | 2026-06-22 物理依赖 breakthrough 原型；4 个镜像变体均由 `Build-DesignedHardLockV0.ps1` 生成，定位是 trace proof，不是正式高覆盖关卡 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/designed_hard_lock_v0_candidates.csv` | Designed Hard Lock V0 输入 CSV | 4 个 proof 候选的 trace 输入；路径为绝对 asset path，供 `Build-SGPRhythmTrace.ps1` 重放 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/designed_hard_lock_v0_trace_metrics.csv` | Designed Hard Lock V0 trace 指标 | 4/4 solved、4/4 `TrueHardCandidate`；`anti=0.688`、`supportClosure=0.921/d3`、`criticalLocks=3`、`maxChoices=5`、`outerExitHead=0`、`localPatch=0`，但 `dependencyFollowRunMax=4`、coverage 低 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLockDirectedBatchPressureFinal5Pack.asset` | HardLock DirectedBatch Pressure Final5 包 | 2026-06-22 pressure filler 比例控制的 5 关冻结包；5/5 board trace `solved=True` 且 `processTier=A`，3 `TrueHardCandidate` + 2 `HardPotential`，`avgChoices` 最大 3.76、`maxChoices` 最大 7、`outerExitHeadCount=0` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLockDirectedBatchPressureFinal5Frozen/` | HardLock DirectedBatch Pressure Final5 冻结关卡目录 | `SGPRhythmLab_HardLockDirectedBatchPressureFinal5Pack` 对应 LevelDefinition 副本；由 `Build-HardLockSlotDirectedBatchFillV1.ps1` 的 pressure mode 分父本输出冻结 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lock_directed_batch_pressure_final5_frozen_retrace_metrics.csv` | HardLock DirectedBatch Pressure Final5 frozen trace | 冻结后复验指标；5/5 A，平均 `avgChoices=3.28`，最低 `causalAntiLocalityScore=0.6`，`supportClosureBestDepth>=3`，`localPatchSolveRunMax<=2` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLockDirectedBatchChokeO1Final5Pack.asset` | HardLock DirectedBatch Choke O1 Final5 包 | 2026-06-22 choke filler + opener guard 的 5 关冻结包；4 关为补肉成功版，1 关为不可补父本原 hard-lock 对照；5/5 solved，`avgChoices` 平均 3.146、最大 3.76，`maxChoices<=7`，`outerExitHeadCount=0` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLockDirectedBatchChokeO1Final5Frozen/` | HardLock DirectedBatch Choke O1 冻结关卡目录 | `SGPRhythmLab_HardLockDirectedBatchChokeO1Final5Pack` 对应 LevelDefinition 副本；第 4/5 关为二轮补肉，coverage 约 0.277-0.281 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lock_directed_batch_choke_o1_final5_frozen_retrace_metrics.csv` | HardLock DirectedBatch Choke O1 frozen trace | 冻结后复验指标；5/5 solved，`antiLocal>=0.6`，`supportClosureBestDepth>=3`，`localPatchSolveRunMax<=2`，`dependencyFollowRunMax<=4` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLock030ProofReview5Pack.asset` | HardLock 0.30 proof/review 5 关包 | 2026-06-23 证明 0.30 完整链路可落成 5 个可解候选；混合了“低 avg/max 但 outer=1”和“outer=0 但 avg debt”两类，用于诊断，不作为最终 production gate |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLock030ProofReview5Frozen/` | HardLock 0.30 proof/review 冻结关卡目录 | `SGPRhythmLab_HardLock030ProofReview5Pack` 对应 LevelDefinition 副本；冻结后 5/5 board trace solved/A-tier |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hardlock_030_proof_review5_frozen_trace_metrics.csv` | HardLock 0.30 proof/review frozen trace | 5/5 solved/A-tier；用于查看 0.30 后 outer/avg 两类残余模式 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLock030DynamicOuterGate5Pack.asset` | HardLock 0.30 Dynamic Outer Gate5 包 | 2026-06-23 当前 0.30 量产线基准包；采用动态外口压力 gate，5/5 solved/A-tier，coverage 约 0.306+，`avgChoices<=4.0`，`maxChoices<=6` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLock030DynamicOuterGate5Frozen/` | HardLock 0.30 Dynamic Outer Gate5 冻结关卡目录 | `SGPRhythmLab_HardLock030DynamicOuterGate5Pack` 对应 LevelDefinition 副本；明早扩父本池/挂 Demo 时优先从这里接续 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hardlock_030_dynamic_outer_gate5_frozen_trace_metrics.csv` | HardLock 0.30 Dynamic Outer Gate5 frozen trace | 5/5 solved/A-tier，`supportClosureBestDepth=3`、`localPatchSolveRunMax=3`、`dependencyFollowRunMax=4`、`outerExitAvailableChoiceMax=1`、`outerExitSolveRunMax=1`；当前 0.30 production baseline |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pc34repair_dyn1_accepted.csv` | Parent capacity + near-miss repair 0.30 accepted pool | 2026-06-23 从 0.265 父本经 directed fill 到 0.299/0.300 near-miss，再翻新增 filler 组方向救援得到 12 个 accepted；top `pc34repair_dyn1_v003` coverage `0.3002451`、avg `3.58`、max `6`、anti `0.7`、support `0.935/d3`、local `2`、follow `4` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RP/pc34repair_dyn1/` | Parent capacity + near-miss repair 0.30 accepted levels | `pc34repair_dyn1_accepted.csv` 对应 LevelDefinition 输出目录；用于明早去重、打包和 Demo 复核 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pc34to30p2_rejected_steps.csv` | 0.30 near-miss source pool | `pc34to30p2` second-stage directed fill 的 high-structure unsolved 候选池；repair 脚本从这里筛 `coverage>=0.299` 的新增 filler 方向组合 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pcdo265p1_p05_c34_parent_feed.csv` | 0.277 intermediate parent feed | 从 `pcdo265p1_p05_s937101_b01_r1_trace_metrics.csv` 恢复出的单行中间父本 feed；coverage `0.277`、avg `2.91`、max `6`、anti `0.714`、support depth `4`、outer=0，作为 `pc34to30p2` 的输入 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pcauto1_a_parent_pool.csv` | 自动中间父本回归 A pool | 从 `pcdo265p1_p05_c34_parent_feed.csv` 自动 directed fill + repair；9 个 0.299+ near-miss，51 variants -> 12 accepted，验证探针能复现手工链路 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pcorig1_a_parent_pool.csv` | 原 cov265 第 5 父本 A pool | 从 `hard_lock_slot_trace_delta_fill_v1_cov265_final5_selected.csv` 第 5 父本直跑，14 variants -> 4 accepted；证明从原父本可自动到 0.299 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pcseed06a_a_parent_pool.csv` | 原 headfix 第 6 父本 A pool | 从 `hard_lock_slot_sgp_fill_headfix_v0_base_09` 直跑，6 variants -> 2 accepted；第二个 A 类结构底盘 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/parent_capacity_o1_review5_selected.csv` | Parent Capacity O1 review5 selected CSV | 从两个 A 父本 accepted pool 合并选出的 5 关，selected coverage 约 `0.2990196` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_ParentCapacityO1Review5Pack.asset` | Parent Capacity O1 review5 pack | 2026-06-23 冻结 5 关包；自动父本承压链路第一版 review 包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/ParentCapacityO1Review5Frozen/` | Parent Capacity O1 review5 frozen levels | `SGPRhythmLab_ParentCapacityO1Review5Pack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/parent_capacity_o1_review5_frozen_metrics.csv` | Parent Capacity O1 review5 frozen trace | 5/5 `solved=True`、5/5 `processTier=A`，`avgChoices<=3.94`、`maxChoices<=7`、`anti>=0.645`、`supportDepth>=3`、`localPatch<=1`、动态外口 pressure <=1 |

| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_D176StrictPack.asset` | Building Grammar D176 strict 4 关包 | 第一次 strict 过线；h231-like -> depanchor -> StageGateSearch+UltraLowChoice -> pair repair `10,8`，`avgChoices=2.64/maxChoices=5/followRun=2/stage=0.822/late=3` |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/depanchor_v176_pairrepair_10_8_strict_selected.csv` | D176 strict selected CSV | D176 冻结包输入；用于复查首批 strict repair 指标 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/building_maze_relaxed_source_v182_trace_feed.csv` | Building Grammar V182 relaxed source feed | V9/V23/V24/nonfollow 风格 trace 合并 165 rows；用于 near-strict depanchor 试跑 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/building_maze_allnear_source_v183_trace_feed.csv` | Building Grammar V183 all-near source feed | 156 个历史 trace metrics 去重后筛出的 36 个 low-choice/high-stage near 源 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d182b_strict_selected.csv` | Building Grammar d182b strict selected CSV | 9 near-strict 源 -> 3 strict；复现 hcd58-like homogeneous max-run 的 stage-preserving pair repair |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d184off_strict_selected.csv` | Building Grammar d184off strict selected CSV | all-near feed 后半段 -> 2 strict；仍为 hcd58-like geometry，证明后段源也可出货但多样性有限 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d182b_repairability.csv` | D182B repairability 画像 | 标记 `strict_pair_repairable` 正例：`labels=AAAA/maxLabelCharCount=1/maxHasO=False` |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d183all_repairability.csv` | D183All repairability 画像 | 标记 triple 能降 follow 但破 stage 的 `repair_breaks_stage` 负例：`AAAA|BBBB|OOOO` |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d184off_repairability.csv` | D184Off repairability 画像 | 同时包含 hcd58-like 正例和 mixed fragile 负例，用于后续 source gate 校准 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d185gate_strict_selected.csv` | Building Grammar d185 gate strict CSV | `-EnableRepairabilityGate` 正例验证；6 candidates -> 6 gated repairs -> 3 strict |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d185gate_repairability_prefilter.csv` | Building Grammar d185 gate repairability CSV | runner 内置 gate 输出；验证 homogeneous 正例不会被误杀 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d186fam1_strict_selected.csv` | Building Grammar d186 family-cap strict CSV | `SourceFamilyCap=1` 压同族复用后 8 candidates -> 2 repairs -> 1 strict，用于评估多样性瓶颈 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d186fam1_repairability_prefilter.csv` | Building Grammar d186 family-cap repairability CSV | 显示 1 个 `repair_candidate_homogeneous` 正例和 7 个 `mixed_fragile` 负例，是当前 source scarcity 的核心证据 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_D188D189PressureReview4Pack.asset` | Building Grammar D188/D189 pressure review 4 关包 | 当前 Demo activePack；D188 raw-pass 3 关 + D189 openers=4 压力 probe 1 关，用于人工评估“开局压力/依赖强度”修正后的观感 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/D188D189PressureReview4/` | D188/D189 pressure review 冻结关卡目录 | `SGPRhythmLab_D188D189PressureReview4Pack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d188_d189_pressure_review4_selected.csv` | D188/D189 pressure review selected CSV | 合并 `d188v182off9rawcross` 的 3 个 raw strict 和 `d189v182off9open4probe` 的 1 个 openers=4 pressure probe |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_d191v182off9pressureflipStrictPack.asset` | Building Grammar d191 pressure+interleave flip 2 关包 | 当前 Demo activePack；`openers=4/avg=3.32/max=5/followRun=2/stage=0.833/cross=0.043`，用于验证更强开局压力和依赖强度 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/d191v182off9pressureflipStrict/` | d191 pressure+interleave flip 冻结关卡目录 | `SGPRhythmLab_d191v182off9pressureflipStrictPack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d191v182off9pressureflip_strict_selected.csv` | d191 pressure+interleave flip selected CSV | d191 2 个 raw strict 的精选输入；同源相似，不代表稳定量产 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_D191D192PressureOpenerReview3Pack.asset` | Building Grammar D191/D192 pressure opener review 3 关包 | 当前 Demo activePack；d191 raw pressure 2 关 + d192 two-stage repair 1 关，验证 `triple 保 cross/follow` 后 `single flip 拉 openers=4` 的路线 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/D191D192PressureOpenerReview3/` | D191/D192 pressure opener review 冻结关卡目录 | `SGPRhythmLab_D191D192PressureOpenerReview3Pack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d191_d192_pressure_opener_review3_selected.csv` | D191/D192 pressure opener review selected CSV | 3 关精选输入；第三关指标 `openers=4/avg=2.68/max=5/followRun=2/stage=0.810/cross=0.043` |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_D198HardPressureRefine1Pack.asset` | Building Grammar D198 hard opening pressure 单关包 | 当前 Demo activePack；针对“外圈出口偏弱/开局压力偏低”反馈，收紧到 `openers=3`、`outerTouch=0.571`、`strictMeaningful=0.800`、`nearUnlock=0.200` 的 pair repair 样本 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/D198HardPressureRefine1/` | D198 hard pressure 冻结关卡目录 | `SGPRhythmLab_D198HardPressureRefine1Pack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d198_hardpressure_refine1_selected.csv` | D198 hard pressure selected CSV | 单关 review 输入；`openers=3/avg=2.64/max=5/followRun=2/stage=0.822/cross=0.043/spineBalance=0.571` |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_D199HardPressureOuterRun2Review1Pack.asset` | Building Grammar D199 hard outer-run2 单关包 | 当前 Demo activePack；D198 后的小幅外圈改良 review，`sameOuterSideSolveRunMax=2`、`outerTouch=0.571`、`strictMeaningful=0.840`，但 `spineAlternationRunMax=11`，不是稳定突破包 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/D199HardPressureOuterRun2Review1/` | D199 hard outer-run2 冻结关卡目录 | `SGPRhythmLab_D199HardPressureOuterRun2Review1Pack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d199_hardpressure_outerrun2_review1_selected.csv` | D199 hard outer-run2 selected CSV | 单关 review 输入；`openers=3/avg=2.68/max=5/followRun=2/stage=0.810/cross=0.043/sameOuterSideRun=2` |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_D214OuterRootNearMissReview2Pack.asset` | Building Grammar D214 outer-root near-miss 2 关包 | 当前 Demo activePack；原始 source 层 `OuterRootAnchorBias` 后的小样，`openers=3/avgChoices=2.32/max=4/stage=0.848/cross=0.043`，但 `followRun=3/sameOuterSideRun=3/earlyOuterAvailableMax=4`，不是正式过线包 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/D214OuterRootNearMissReview2/` | D214 outer-root near-miss 冻结关卡目录 | `SGPRhythmLab_D214OuterRootNearMissReview2Pack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d214_outerroot_nearmiss_review2_selected.csv` | D214 outer-root near-miss selected CSV | 2 关 review 输入；用于人工看开局压力是否比 D199 更接近方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_MixedFamilyProductionO1Pack.asset` | Mixed Family Production O1 9 关 review 包 | 2026-06-23 冻结的双 family 包；包含 hard-lock causal family、DependencySkeletonV3、RoomDoorSkeletonV2，用于人工验证多样性和体感 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/MixedFamilyProductionO1Frozen/` | Mixed Family Production O1 冻结关卡目录 | `SGPRhythmLab_MixedFamilyProductionO1Pack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_MixedFamilyProductionO1Pack.csv` | Mixed Family Production O1 pack 输入 CSV | 冻结工具输出的 9 关 pack CSV；可用于二次 trace 或 Demo 挂包 |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_CausalHardlockDiverseReviewV1Cov28Pack.asset` | Causal Hardlock Diverse Review V1 Cov28 12 关包 | 2026-06-23 当前 Demo activePack；12 个不同父本/几何，coverage 约 0.28+，12/12 frozen trace solved/A，11 TrueHardCandidate；用于人工筛哪些父本值得继续推到 0.30。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_CausalHardlockDiverseReviewV1Cov30Pack.asset` | Causal Hardlock Diverse Review V1 Cov30 4 关事实包 | 2026-06-23 现有 0.299+ 严格去重后只剩 4 个不同父本/几何；4/4 frozen trace solved/A，证明 0.30 A pool 多样性仍不足。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_hardlock_diverse_review_v1_cov28_frozen_trace_metrics.csv` | Cov28 多样性包 frozen trace | 12/12 solved/A，11 TrueHardCandidate + 1 HardPotential；用于人工 review 后挑父本继续推到 0.30。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_hardlock_diverse_review_v1_cov30_frozen_trace_metrics.csv` | Cov30 严格去重包 frozen trace | 4/4 solved/A，3 TrueHardCandidate + 1 HardPotential；用于记录当前 0.30 去重产能事实。 |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_CausalHardlockDiverseReviewV2Cov28Pack.asset` | Causal Hardlock Diverse Review V2 Cov28 7 关包 | 2026-06-23 当前 Demo activePack；加入 occupancy Jaccard near-duplicate 去重和人工 reject 后得到 7 个更干净父本，7/7 frozen trace solved/A，最大 occupancy Jaccard=0.766。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_hardlock_diverse_review_v2_cov28_frozen_trace_metrics.csv` | V2 Cov28 frozen trace | 7/7 solved/A，6 TrueHardCandidate + 1 HardPotential；用于人工确认干净父本是否可继续推到 0.30。 |

## Recent SGP Rhythm Review Packs

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SpeciesManifoldRootPairReview2Pack.asset` | Species manifold 2-root review demo pack | 当前 Demo 挂载的两个强 root 候选；用于快速人工比较 `a02280d338` 与 `0eea76b1ba` 两个 root 的体感差异 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/species_manifold_root_pair_review2_frozen_trace_metrics.csv` | Species manifold 2-root frozen trace | 查看 Demo 挂载后冻结资源的真实 trace，2/2 solved，supportDepth=4 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SpeciesManifoldCanonicalRootReview1Pack.asset` | Species manifold canonical root 单关 demo pack | 当前 Demo 挂载；从 RootPairReview2 中去掉 rhythm/geometry near-duplicate 后只保留一个 canonical root 样本。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/species_manifold_canonical_root_review1_frozen_trace_metrics.csv` | Canonical root review1 frozen trace | 查看单关包冻结后真实 trace：1/1 solved，avg/max `3.66/6`，supportDepth `4`。 |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SpeciesNewRootReview2Pack.asset` | Species New Root Review2 2 关包 | 2026-06-23 当前 Demo activePack；从 `web_four_mirrory` 新 root source 经小步 fill 得到两个不同 rootSkeleton（`1179a2b946`、`77b588f85b`），用于人工判断真 root 多样性体感。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/species_new_root_review2_frozen_trace_metrics.csv` | Species New Root Review2 frozen trace | 2/2 solved/A；avg/max `3.52/6` 与 `3.69/7`，supportDepth `3/4`，localPatchRun `2`，dependencyFollowRun `3`，outerExitHead `0`。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/species_new_root_review2_frozen_signature_signatures.csv` | Species New Root Review2 canonical signature | 2 rows -> 2 rootSkeleton / 2 macro / 2 chainLanguage；用于确认不是 rhythm 或单链差异伪多样性。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_MixedRootTopologyReviewV1Pack.asset` | Mixed Root Topology Review V1 4 关包 | 2026-06-23 当前 Demo activePack；按 mixed-root gate + `MaxPerRootTopology=1` 从 MixedFamilyProductionO1 中冻结 4 个严格不同 rootTopology（hardlock tri、dependency_skeleton、room_door x2），4/4 frozen trace solved，3 S + 1 A。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/MixedRootTopologyReviewV1Frozen/` | Mixed Root Topology Review V1 冻结关卡目录 | `SGPRhythmLab_MixedRootTopologyReviewV1Pack` 对应 LevelDefinition 副本。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/mixed_root_topology_review_v1_frozen_trace_metrics.csv` | Mixed Root Topology Review V1 frozen trace | 4/4 solved，`avgChoices` 2.4-3.47，`maxChoices` 4-7，`causalAntiLocalityScore` 0.6-1.0；用于人工验证多 root 体感。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/mixed_root_topology_review_v1_frozen_root_topology_root_topology.csv` | Mixed Root Topology Review V1 root topology report | 4 rows -> 4 `rootTopologyHash` / 4 strictRoot / 3 sourceArchetype；当前最干净的多 root review 证据链。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_CausalMotifEmbeddingV1Smoke2Pack.asset` | Causal Motif Embedding V1 smoke proof 2 关包 | 2026-06-23 motif embedding compiler proof；2/2 frozen trace solved/A，`supportClosureBestDepth=4`、outer=0。仅证明 tri_convergent motif embedding 闭环，不代表多 root production 包。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_motif_embedding_v1_smoke2_frozen_trace_metrics.csv` | Causal Motif Embedding V1 smoke2 frozen trace | 2/2 solved/A；avg/max `2.96/5` 与 `3.76/6`，supportDepth=4，localPatch<=3，followRun<=4。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_motif_embedding_v1_strat_smoke1_summary.md` | Causal Motif Embedding V1 stratified smoke summary | 记录 source species 分层后包含 tri/web，但最终只有 tri 过线；web/dual 需要 species-aware fill。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_CausalMotifEmbeddingV1ReviewPool1Pack.asset` | Causal Motif Embedding V1 Review Pool1 5 关包 | 2026-06-23 当前 Demo activePack；同一大容器内 5 个 support-lock motif instance/rhythm variant，5/5 frozen trace solved/A，用于人工判断 motif-instance 体感差异是否足够进入短期高难池。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_motif_embedding_v1_review_pool1_frozen_trace_metrics.csv` | Causal Motif Embedding V1 Review Pool1 frozen trace | 5/5 solved/A，avg/max `2.88-3.76 / 5-7`，supportDepth `3-4`，outerExitHead=0；用于复核当前 Demo 包真实 trace。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_motif_embedding_v1_review_pool1_frozen_signature_summary.md` | Causal Motif Embedding V1 Review Pool1 signature summary | 5 core / 4 skeleton family / 4 macro / 3 rootSkeleton / 3 strictRoot / 5 rhythm variants；说明这是 motif-instance 多样性，不是严格 multi-root proof。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_hard_lite_template_v1_directed_fill_strictfanout_smoke2_final_selected.csv` | Dual-gate hard-lite strict fanout selected proof | 2026-06-23 第二 causal backbone primitive 的正式 smoke：1/1 retrace solved/A，coverage `0.2304`，branch2/fanout2，backbone `34771de5e2`。不是最终 review pack。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_hard_lite_template_v1_directed_fill_strictfanout_smoke2_final_backbone_backbones.csv` | Dual-gate hard-lite final causal backbone report | 记录 strict fanout fill 后仍保持 `serial_support_lock||dual_gate_hubfield||...fanout2|branch2|closureChain`，用于后续量产线 gate。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/DualGateHardLiteTemplateV1DirectedFillStrictFanoutSmoke2/` | Dual-gate hard-lite strict fanout proof levels | `dual_gate_hard_lite_template_v1_directed_fill_strictfanout_smoke2_final_selected.csv` 对应 LevelDefinition 输出目录。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DualGateVsTriRootReview1Pack.asset` | Dual Gate vs Tri Root 2关对照包 | 2026-06-23 当前 Demo activePack；Level1 旧 tri_convergent branch3/fanout4，Level2 新 dual_gate_hard_lite branch2/fanout2；用于用户肉眼判断是否属于不同 causal root。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_vs_tri_root_review1_frozen_trace_metrics.csv` | Dual Gate vs Tri Root frozen trace | 2/2 solved/A；Level1 avg/max `3.16/7`、branch3/fanout4；Level2 avg/max `3.04/6`、branch2/fanout2。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_vs_tri_root_review1_frozen_backbone_backbones.csv` | Dual Gate vs Tri Root causal backbone report | 2 rows -> 2 causalBackboneRoots；用于证明该对照包不是同 backbone 变体。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DualGateSpatialRootV1SkeletonReview1Pack.asset` | Dual Gate Spatial Root V1 skeleton 单关审查包 | 2026-06-23 当前 Demo activePack；低覆盖非对称双门控视觉 root 骨架，frozen trace `solved=False/supportDepth=1`，只用于肉眼判断 root 形态，不是高难生产包。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_spatial_root_v1_skeleton_review1_frozen_trace_metrics.csv` | Dual Gate Spatial Root V1 frozen trace | 记录该 skeleton 包不可解、非 hard 的 trace 事实，避免误入 production。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DualGateJointCoreRootV1Review2Pack.asset` | Dual Gate Joint-Core Root V1 单关审查包 | 2026-06-23 当前 Demo activePack；true dual_gate 定义修正版，两个分离 gate 分别打开同一中央 core 的两个入口；frozen trace solved/A，但 avgChoices 偏高，尚非 production hard。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_joint_core_root_v1_review2_frozen_trace_metrics.csv` | Dual Gate Joint-Core Root V1 frozen trace | 记录 solved/A、avg/max `4.93/6`，并证明该包可解；旧 supportClosure 指标不识别此 motif。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DualGateJointCoreRootV1Review5Pack.asset` | Dual Gate Joint-Core shared-lock 原型包 | 2026-06-23 当前 Demo activePack；B 路解 lockB、A 路解 lockA，两个锁都清后同一个 core chain 才打开。Solved/A，avg/max `4.55/6`，低覆盖原型，不是 production hard。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_joint_core_root_v1_review5_trace_metrics.csv` | Dual Gate shared-lock trace | 记录 true dual-gate shared-lock prototype 的 trace 指标；用于后续建立专门 dualGateJointCore gate。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_ValidatedRootExpansionO1ReviewPack.asset` | Validated Root Expansion O1 4 关 review 包 | 2026-06-23 当前 Demo activePack；已验证 hardlock/support-lock root 的 clean expansion，4/4 frozen trace solved/A。定位是已验证 root 产线扩张审查包，不是新 root proof。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/ValidatedRootExpansionO1Review/` | Validated Root Expansion O1 冻结关卡目录 | `SGPRhythmLab_ValidatedRootExpansionO1ReviewPack` 对应 LevelDefinition 副本。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/validated_root_expansion_o1/validated_root_expansion_o1_cov285_review_selected.csv` | Validated Root Expansion O1 clean selected CSV | 从现有 hard-gate 池 + parent capacity 新分片合并后，按 support-lock gate、动态外口 gate、parent/geometry/occupancy 去重选出的 4 个 clean review 候选。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/validated_root_expansion_o1_review_frozen_trace_metrics.csv` | Validated Root Expansion O1 frozen trace | 4/4 solved/A，avgChoices `3.23-3.69`、maxChoices `5-7`、supportDepth `3-4`、localPatch `1-2`、dependencyFollow `4`，动态外口 pressure <=1。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/validated_root_expansion_o1_review_frozen_signature_summary.md` | Validated Root Expansion O1 signature summary | 4 core / 4 skeleton family / 3 macro / 2 rootSkeleton / 2 strictRoot / 4 rhythm variants；用于说明这是同已验证 root 的 clean expansion，而不是新 root 证明。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_ValidatedRootBackgroundSGPV1T40ProofPack.asset` | Validated Root Background SGP V1 T40 proof 包 | 2026-06-23 当前 Demo activePack；单关 proof，把已验证 support-lock root 从 `coverage=0.305` 推到约 `0.398`，frozen trace `solved=True/processTier=A/supportDepth=3/avgChoices=3.58/maxChoices=7`。不是最终量产包，用于人工看接近 0.4 的填肉体感。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/ValidatedRootBackgroundSGPV1T40Proof/` | Validated Root Background SGP V1 T40 proof 冻结关卡目录 | `SGPRhythmLab_ValidatedRootBackgroundSGPV1T40ProofPack` 对应 LevelDefinition 副本。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/validatedroot_background_sgp_v1_t40_proof_frozen_trace_metrics.csv` | Validated Root Background SGP V1 T40 frozen trace | 1/1 solved/A；`supportClosureBestDepth=3`、`supportClosureBestScore=0.969`、`localPatchSolveRunMax=2`、`dependencyFollowRunMax=3`、动态外口 pressure=1。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/trace_placement_probe_v1_smoke5_summary.md` | Trace Placement Probe V1 smoke summary | 2026-06-23 对 0.398 proof 父本做 5 个 micro-chain 反向 placement probe，分类结果 `SafeNeutral=3`、`Deadlock=2`；用于诊断 0.40+ 填肉失败是中盘堵死还是可安全补位。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ray_constraint_map_v1_t40_summary.md` | Ray Constraint Map V1 T40 summary | 2026-06-23 对 0.398 proof 父本输出 ray/cell 角色图；`CriticalTimingZone=136`、`SafeFillZone=48`、`GuardSlot=2`，说明 0.40+ 剩余空间大量进入 ray-causal 时序敏感区。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ray_constraint_map_v1_t40_role_grid.txt` | Ray Constraint Map V1 T40 ASCII role grid | 用 `K/#/C/G/B/H/F/.` 标记 occupied critical、occupied、critical timing、guard、body-only、head-allowed、free-head risk、safe fill；用于肉眼查看当前父本可填空间分布。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ray_constraint_guided_fill_v1_step440_c01_exact_to442_onechain_summary.md` | Ray Constraint Guided Fill 0.441 proof summary | 2026-06-23 已验证 root 产线扩张 proof：从 0.398 proof 父本经多轮 map-guided 小步填肉推到 coverage `0.4411765`，最终样本 `ray_constraint_guided_fill_v1_step440_c01_exact_to442_onechain_b01_c15` 仍为 `solved=True/A/TrueHardCandidate`、supportDepth=3、avg/max `4.05/8`、local=2、follow=3。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RayConstraintGuidedFillV1/` | Ray Constraint Guided Fill V1 生成关卡目录 | 存放 0.398->0.441 迭代填肉 proof 的候选 LevelDefinition。当前最好 hard proof 为 `ray_constraint_guided_fill_v1_step440_c01_exact_to442_onechain_b01_c15.asset`；尚未冻结成 review/demo 包。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ray_first_blocker_fill_v1_step442_c15_to448_strict_critical_summary.md` | Ray First Blocker 0.445 proof summary | 2026-06-24 从最高 0.441 proof 父本按 ray-first blocker 单链补肉；严格 gate 1/20 accepted，样本 `ray_first_blocker_fill_v1_step442_c15_to448_strict_critical_b01_c17` coverage `0.4448529`、solved/A、supportDepth=3、avg/max `3.62/7`、local=2、follow=3。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RayFirstBlockerFillV1/` | Ray First Blocker Fill V1 生成关卡目录 | 存放从 RayConstraintMap edge/cell 反推 blocker 链的候选 LevelDefinition；当前 proof 资产为 `ray_first_blocker_fill_v1_step442_c15_to448_strict_critical_b01_c17.asset`，尚未冻结成 demo/review pack。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ray_first_blocker_fill_v2_balanced16_c09_to463_summary.md` | Ray First Blocker balanced V2 0.462 proof summary | 2026-06-24 balanced-anchor V2 连续 proof；从 `0.4411765` 经每步重算 map 的单链 ray-first blocker 迭代到 `0.4620098`，最高样本 `ray_first_blocker_fill_v2_balanced16_c09_to463_b01_c16` solved/A、supportDepth=4、avg/max `2.9/5`、local=3、follow=4。 |

## Strict Dual Gate Review Packs - 2026-06-23

- `SGPRhythmLab_DualGateJointCoreVariantV2FillReview8T018Pack.asset`: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DualGateJointCoreVariantV2FillReview8T018Pack.asset`, guid=`c1687786dc944d18bb719a04b489fb13`. 8-level strict dual T018 review pack, mounted in Demo.
- Source report: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_joint_core_variant_v2_fill_target018_review8_selected.csv`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_joint_core_variant_v2_fill_review8_t018_frozen_trace_metrics.csv`.

## Strict Dual Gate T030 Proof Pack - 2026-06-24

- `SGPRhythmLab_DualGateJointCoreVariantV2FillProof2T030Pack.asset`: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DualGateJointCoreVariantV2FillProof2T030Pack.asset`, guid=`6c8fdbedca9d4c56a4893b67b194c1d9`. 2-level strict dual shared-core proof at selected coverage `0.3015/0.3039`; Demo mounted here after freeze.
- Source report: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_joint_core_variant_v2_fill_target030_proof2_selected.csv`.
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_DualGateJointCoreVariantV2FillProof2T030Pack.csv`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_joint_core_variant_v2_fill_proof2_t030_frozen_trace_metrics.csv`.

## Validated Root 0.512 Review Pack - 2026-06-24

- `SGPRhythmLab_PathAwareStep512C03ReviewPack.asset`: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PathAwareStep512C03ReviewPack.asset`, guid=`763823fa3f75436d9caf80a7d54b991f`. Single-level 0.512 validated-root expansion checkpoint; Demo activePack currently points here for user inspection.
- Frozen level dir: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PathAwareStep512C03Review/`.
- Source candidate: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/path_aware_probe_v1_step512_closed_debt_c03_base_map_input.csv`.
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_PathAwareStep512C03ReviewPack.csv`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/path_aware_step512_c03_review_frozen_trace_metrics.csv`; 1/1 solved/A, coverage `0.5122549`, avg/max `2.54/6`, antiLocal `0.515`, support `0.858/d3`, local/follow `3/4`.
- Ray map for next step: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/path_aware_probe_v1_step512_c03_map_*`.
- Ray-constrained cavity smoke conclusion: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ray_constrained_cavity_fill_v1_step512_smoke_conclusion.md`. Single-chain center-out candidates can be safe but only reach `0.5159-0.5172`; two-chain/three-chain blind batches reach `0.52+` coverage but 20/20 unsolved, so high-coverage fill must be staged with post-map/trace settlement.

## Web Crossover Root Proof Pack - 2026-06-24

- `SGPRhythmLab_WebCrossoverV1RootProofT030Pack.asset`: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_WebCrossoverV1RootProofT030Pack.asset`, guid=`8e92cc9a9d504068ae1c4893760dacb7`. 5-level third-root proof pack, mounted in Demo; selected coverage `0.288-0.2978`.
- Source selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/web_crossover_v1_fill_t030_merged5_selected.csv`.
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_WebCrossoverV1RootProofT030Pack.csv`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/web_crossover_v1_root_proof_t030_frozen_trace_metrics.csv`.
- Frozen web gate: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/web_crossover_v1_root_proof_t030_frozen_webgate_web_roots.csv` and summary `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/web_crossover_v1_root_proof_t030_frozen_webgate_summary.md`.

## Seeded Direct-SGP Full Coverage Baseline - 2026-06-24

- `SGPRhythmLab_SeededDirectSGPMicroFrom030To095SweepReview4Pack.asset`: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SeededDirectSGPMicroFrom030To095SweepReview4Pack.asset`, guid=`3c094f1b94234aa49f408977bd397081`. Deprecated/buggy pack: metadata says `0.94-0.95`, but authored arrows were not replaced due `paths:` vs `arrows:` writer bug; actual assets remain 37 chains / 251 cells / coverage `0.3076`.
- `SGPRhythmLab_SeededDirectSGPMicroFrom030To095SweepFix1Review4Pack.asset`: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SeededDirectSGPMicroFrom030To095SweepFix1Review4Pack.asset`, guid=`ca03633336ec4b20a42d659be39d01a5`. Current Demo visual check pack; starts from one validated `0.30` support-lock parent and lets seeded Direct-SGP + micro-fill push to real `0.9436-0.9510` coverage.
- Frozen level dir: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/SeededDirectSGPMicroFrom030To095SweepFix1Review4/`.
- Source candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/seeded_direct_sgp_micro_from030_to095_sweep_fix1_smoke4_candidates.csv`; real authored arrows `141-157` chains, occupied `770-776`.
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_SeededDirectSGPMicroFrom030To095SweepFix1Review4Pack.csv`.
- Source trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/seeded_direct_sgp_micro_from030_to095_sweep_fix1_smoke4_trace_metrics.csv`; 4/4 `solved=False/processTier=Drop`, avg/max roughly `19-21/38-41`. This pack proves geometric fill capacity, not hard/playable production quality.

## Small Canvas Seeded Direct-SGP Review Pack - 2026-06-24

- `SGPRhythmLab_SeededDirectSGPSmallCanvas18x24Max36Review6Pack.asset`: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SeededDirectSGPSmallCanvas18x24Max36Review6Pack.asset`, guid=`c2d629dd67e342c48cd0efc3a467112d`. Current Demo activePack; validates the “shrink whole canvas, preserve parent motif, then let SGP drill” route.
- Frozen level dir: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/SeededDirectSGPSmallCanvas18x24Max36Review6/`.
- Source candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/seeded_direct_sgp_smallcanvas_18x24_max36_smoke6_candidates.csv`; base compacted to 18x24 with 28 kept chains / 9 dropped chains, final totalChains=36, coverage `0.463-0.509`.
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_SeededDirectSGPSmallCanvas18x24Max36Review6Pack.csv`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/seeded_direct_sgp_smallcanvas_18x24_max36_review6_frozen_trace_metrics.csv`; 6/6 solved, 2 B-tier, supportDepth preserved on the best samples, but outer/choice still above production target.

## Hub-Spoke Root Proof Pack - 2026-06-24

- `SGPRhythmLab_HubSpokeTrueV5RootProofT0288Pack.asset`: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HubSpokeTrueV5RootProofT0288Pack.asset`, guid=`4700f5c8ee954d7da359c1185256a872`. 3-level hub-spoke proof pack at selected coverage `0.288`; Demo activePack is mounted here.
- Source selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hub_spoke_true_v5_root_proof_t0288_selected.csv`.
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_HubSpokeTrueV5RootProofT0288Pack.csv`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hub_spoke_true_v5_root_proof_t0288_frozen_trace_metrics.csv`.
- Frozen hub-spoke gate: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hub_spoke_true_v5_root_proof_t0288_frozen_gate_hub_spoke_roots.csv`.

## Cascade Relay V1 Root Proof T0207

- Pack: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_CascadeRelayV1RootProofT0207Pack.asset
- Pack GUID: c7d563624d224126937c70db12e42430
- Manifest: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_CascadeRelayV1RootProofT0207Pack.csv
- Selected source CSV: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/cascade_relay_v1_root_proof_t0207_selected.csv
- Frozen trace: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/cascade_relay_v1_root_proof_t0207_frozen_trace_metrics.csv
- Frozen gate: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/cascade_relay_v1_root_proof_t0207_frozen_gate_cascade_roots.csv
- Contents: 3 levels, coverage 0.1924-0.2071, all solved A, all cascadeRelayCandidate=True.

## Split Key V1 Root Proof T0203

- Pack: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SplitKeyV1RootProofT0203Pack.asset
- Pack GUID: fc53728dbb824b9d8e724b1586d97d0b
- Manifest: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_SplitKeyV1RootProofT0203Pack.csv
- Selected source CSV: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/split_key_v1_root_proof_t0203_selected.csv
- Frozen trace: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/split_key_v1_root_proof_t0203_frozen_trace_metrics.csv
- Frozen gate: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/split_key_v1_root_proof_t0203_frozen_gate_split_key_roots.csv
- Contents: 3 levels, coverage 0.2010-0.2034, all solved A, all splitKeyCandidate=True.


## Mixed Root Family Review V1

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_MixedRootFamilyReviewV1Pack.asset`
- Pack GUID: `1dc9459927164d23899ab69b33b9b9f6`
- Manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_MixedRootFamilyReviewV1Pack.csv`
- Selected source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/mixed_root_family_review_v1_selected.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/mixed_root_family_review_v1_frozen_trace_metrics.csv`
- Identity audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/mixed_root_family_review_v1_identity_audit.csv`
- Contents: 12 levels, 2 each from support_lock, strict_dual, web_crossover, hub_spoke, cascade_relay, split_key. Frozen trace: 12/12 solved A.

## Small Canvas Outer-Frame Seeded Direct-SGP Review4

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SC18F8Max44Review4Pack.asset`
- Pack GUID: `68c0042da95a42f688a0dc8f77f93581`
- Frozen level dir: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/SC18F8R4/`
- Source candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/seeded_direct_sgp_smallcanvas_18x24_frame8_max44_smoke8_candidates.csv`
- Selected source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/seeded_direct_sgp_smallcanvas_18x24_frame8_max44_review4_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_SC18F8Max44Review4Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/seeded_direct_sgp_smallcanvas_18x24_frame8_max44_review4_frozen_trace_metrics.csv`
- Contents: 4 levels from one compacted 18x24 support-lock parent with 8 preseeded outer-frame chains and totalChains=44. Selected coverage `0.5602-0.6134`; frozen trace 4/4 solved A with supportDepth=3. Demo scene in `.worktrees/sgp-rhythm-lab` is mounted to this pack.

## Parent030 To 0.60 Small Canvas Review4

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_Parent030To060SmallCanvasReview4Pack.asset`
- Pack GUID: `d638964e6b734b7ab45abb08dbd53630`
- Frozen level dir: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/Parent030To060SmallCanvasReview4/`
- Input parent CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/opendebt_parent030_single_input.csv`
- Source candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/parent030_to060_smallcanvas18x24_frame8_max45_try3_candidates.csv`
- Selected source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/parent030_to060_smallcanvas18x24_frame8_max45_review4_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_Parent030To060SmallCanvasReview4Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/parent030_to060_smallcanvas_review4_frozen_trace_metrics.csv`
- Contents: 4 levels from clean `near_miss_filler_orientation_v1_probe3_v006` parent compacted to 18x24 and filled to `coverage=0.6019-0.6134`; 4/4 solved, 2 A + 2 B, all supportDepth=3. Demo scene in `.worktrees/sgp-rhythm-lab` is mounted to this pack.

## Original Seed Long-Chain Review V1

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedLongChainReviewV1Pack.asset`
- Pack GUID: `9d4fe47b51bb4d11b2e5525bbbe360e2`
- Selected source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_long_chain_review_v1_selected.csv`
- Contents: 8 `ImportedOriginal` reference seeds selected from `reference_seed_structure_top_complex_298.csv` for long-chain language review. Metrics span 42-157 chains, coverage `0.851-0.982`, avgChain `8.952-15.911`, maxChain `28-63`, longChainRate `0.400-0.554`.
- Demo scene in `.worktrees/sgp-rhythm-lab` is mounted to this pack. This is not a hard/root proof pack.

## Original Seed Long-Chain Skeleton Review V2

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedLongChainSkeletonReviewV2Pack.asset`
- Pack GUID: `d313fdfd4c504b66be8826758a8bbba5`
- Selected source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_long_chain_skeleton_review_v2_selected.csv`
- Contents: 8 `ImportedOriginal` reference seeds selected for long-chain full-board skeleton language. This supersedes V1 for visual review because V1 rows 4/7/8 were too sparse/local to count under the user’s intended skeleton definition.
- Demo scene in `.worktrees/sgp-rhythm-lab` is mounted to this pack.

## Original Seed Difficulty Skeleton T050 Review8

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedDifficultySkeletonT050Review8Pack.asset`
- Pack GUID: `88aa67560fa7477ea7e6f784bcc70e4e`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_long_chain_difficulty_skeleton_trace_root_v1_t050_candidates.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedDifficultySkeletonT050Review8Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_difficulty_skeleton_t050_review8_frozen_trace_metrics.csv`
- Contents: 8 trace-root extracted skeletons from original long-chain seeds. Best true causal skeleton is Arrowz_level_074: 52 chains, solved A, avg/max `3.96/8`, support `0.838/d4`; other rows are mostly choice-pressure/d2 skeletons. Demo scene is mounted to this pack.

## Original Seed Availability Peel Min8 Review8

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedAvailabilityPeelMin8Review8Pack.asset`
- Pack GUID: `78403564a5c14a2789262cc9acfb24e2`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_long_chain_availability_peel_v1_min8_candidates.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedAvailabilityPeelMin8Review8Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_availability_peel_min8_review8_frozen_trace_metrics.csv`
- Contents: 8 original long-chain seeds after iterative availability-shell peel (`MinAvailableToPeel=8`). 8/8 solved; retained high-coverage difficulty cores, e.g. level_510 support `0.889/d4`, Arrowz_level_095 support `0.810/d4`. Demo scene is mounted to this pack.

## Availability Peel Batch2 Review12

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_AvailabilityPeelBatch2Review12Pack.asset`
- Pack GUID: `fa0b5e3d61b64548a7c99baf48dff472`
- Source input: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/availability_peel_next24_input.csv`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/availability_peel_next16_v1_min8_candidates.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/availability_peel_next16_v1_min8_review12_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_AvailabilityPeelBatch2Review12Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/availability_peel_batch2_review12_frozen_trace_metrics.csv`
- Contents: 12 selected availability-peel skeletons from the next high-complexity seed batch; all solved, support d2+, non-Drop before freeze. Demo scene is mounted to this pack.

## Availability Peel Batch2 True Skeleton Review2

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_AvailabilityPeelBatch2TrueSkeletonReview2Pack.asset`
- Pack GUID: `95abf1a1f9ce4e9eaad1043ea90878b3`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/availability_peel_batch2_true_skeleton_review2_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_AvailabilityPeelBatch2TrueSkeletonReview2Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/availability_peel_batch2_true_skeleton_review2_frozen_trace_metrics.csv`
- Contents: 2 corrected true-skeleton samples from Batch2 after availability peel + causal-core trim. 2/2 solved; 26/32 chains; supportDepth=3. Demo scene is mounted to this pack.

## Original Seed Root Batch3 Review9

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedRootBatch3Review9Pack.asset`
- Pack GUID: `f6a5e78fd7cd49bd86d36cf01d8f7f53`
- Input CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_batch3_input48.csv`
- Peel candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_batch3_peel_min8_candidates.csv`
- Merged peel trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_batch3_peel_min8_trace_merged_metrics.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_batch3_review9_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedRootBatch3Review9Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_batch3_review9_frozen_trace_metrics.csv`
- Contents: 9 original-seed root candidates extracted via availability-shell peel from high-complexity reference seeds, selected for trace-visible d3/d4 support motif and bounded choices. Demo scene is mounted to this pack.

## Original Seed RoleGraph Root Proof V1 Review1

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedRoleGraphRootProofV1Review1Pack.asset`
- Pack GUID: `7a9f31a602d64fd9ad535c8a878a45ac`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_v1_level944_minclosure_candidate.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedRoleGraphRootProofV1Review1Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_root_proof_v1_review1_frozen_trace_metrics.csv`
- Contents: 1 minimal original-seed role-graph root from level_944 closure nodes; 9 chains, solved S, supportDepth=4. Demo scene is mounted to this pack.

## Original Seed RoleGraph Level944 Root030 Review1

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedRoleGraphLevel944Root030Review1Pack.asset`
- Pack GUID: `a3cd8a75d6cb4fd591cf8d9828e0cb5d`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_level944_root030_review1_selected.csv`
- Carrier candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_level944_carrier_search_v1_candidates.csv`
- Carrier trace merged: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_level944_carrier_search_v1_trace_merged_metrics.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedRoleGraphLevel944Root030Review1Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_level944_root030_review1_frozen_trace_metrics.csv`
- Contents: 1 original-seed root from level_944, 35 chains, coverage `0.3024691`, solved A, support `0.976/d4`, TrueHardCandidate. Demo scene is mounted to this pack.

## Original Seed RoleGraph Next5 Review

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedRoleGraphNext5ReviewPack.asset`
- Pack GUID: `9e2e67f9fb8649cf9d2d481ab8fda5c7`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_next5_review_selected.csv`
- Carrier candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_next5_carrier_v1_candidates.csv`
- Carrier trace merged: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_next5_carrier_v1_trace_merged_metrics.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedRoleGraphNext5ReviewPack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_next5_review_frozen_trace_metrics.csv`
- Contents: 5 original-seed rolegraph roots extracted via fixed trace nucleus plus carrier search. 5/5 solved; source families: level_792, Arrowz_level_182, Arrowz_level_232, Arrowz_level_154, Arrowz_level_264. Demo scene is mounted to this pack.

## Root Library Initial Families V1

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootLibraryInitialFamiliesV1Pack.asset`
- Pack GUID: `7c4cf2078e27463ab3c640d82bdd67fa`
- Source selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/mixed_root_family_review_v1_selected.csv`
- Catalog: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_library_initial_families_v1_catalog.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootLibraryInitialFamiliesV1Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_library_initial_families_v1_frozen_trace_metrics.csv`
- Contents: 12 initial generated/proof roots, 2 each from support_lock, strict_dual, web_crossover, hub_spoke, cascade_relay, split_key. This is the baseline generated-root library, not a production campaign pack.

## Root Library Original Seed RoleGraph V1

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootLibraryOriginalSeedRoleGraphV1Pack.asset`
- Pack GUID: `f142bc1c67394ea5a9fc166f513a0a68`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_library_original_seed_rolegraph_v1_selected.csv`
- Catalog: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_library_original_seed_rolegraph_v1_catalog.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootLibraryOriginalSeedRoleGraphV1Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_library_original_seed_rolegraph_v1_frozen_trace_metrics.csv`
- Contents: 4 original-seed rolegraph RootReviewCandidate rows after excluding NucleusOnly/ThinRoot rows; source families level_944, level_792, Arrowz_level_182, Arrowz_level_232.

## Original Seed RoleGraph Batch4 Review5

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedRoleGraphBatch4Review5Pack.asset`
- Pack GUID: `4a70f5e8e2b04ad2b4e43c4ff0786a91`
- Source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_batch4_sources.csv`
- Carrier candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_batch4_carrier_v1_candidates.csv`
- Prefiltered candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_batch4_carrier_v1_prefilter_top.csv`
- Prefilter trace merged: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_batch4_prefilter_trace_merged_metrics.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_batch4_review5_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedRoleGraphBatch4Review5Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_batch4_review5_frozen_trace_metrics.csv`
- Contents: 5 original-seed rolegraph RootReviewCandidate rows from source families Arrowz_level_055, Arrowz_level_120, level_699, level_730, level_810. Demo scene is mounted to this pack for review.

## Original Seed Strict Role Root V1 Review8

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedStrictRoleRootV1Review8Pack.asset`
- Pack GUID: `2256b922b4464fe7b27c9e3a572b2796`
- Source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_v1_sources.csv`
- Candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_v1_candidates.csv`
- Candidate trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_v1_trace_metrics.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_v1_review8_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedStrictRoleRootV1Review8Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_v1_review8_frozen_trace_metrics.csv`
- Contents: 8 strict role roots extracted from original seed residuals using role-labeled chains only, no coverage target. Demo scene is mounted to this pack.

## Original Seed Strict Role Root Batch5 V1 Review8

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedStrictRoleRootBatch5V1Review8Pack.asset`
- Pack GUID: `e8b42fb7fb8946c4a21d4e8372d05fbb`
- Source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_batch5_sources.csv`
- Candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_batch5_v1_candidates.csv`
- Candidate trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_batch5_v1_trace_metrics.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_batch5_v1_review8_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedStrictRoleRootBatch5V1Review8Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_batch5_v1_review8_frozen_trace_metrics.csv`
- Contents: 8 strict role roots from additional original-seed sources. Demo scene is mounted to this pack.

## Original Seed Strict Role Root Full V1 Review21

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedStrictRoleRootFullV1Review21Pack.asset`
- Pack GUID: `3f5cf33e5b6e4b2d8e44b5f2a38aa729`
- Source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_full_sources.csv`
- Candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_full_v1_candidates.csv`
- Catalog: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_full_v1_catalog.csv`
- Selected review CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_full_v1_review21_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedStrictRoleRootFullV1Review21Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_full_v1_review21_frozen_trace_metrics.csv`
- Contents: 21 full-run StrictRootReview candidates from original seed strict role extraction, no coverage filler target. Frozen trace 21/21 solved; Demo scene in `.worktrees/sgp-rhythm-lab` is mounted to this pack for broad human filtering.

## Original Seed Root Extractable V1 Review9

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedRootExtractableV1Review9Pack.asset`
- Pack GUID: `c1e9a01e09d34a78b8bf0670e958c7f1`
- Extractability screen: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_extractability_gate_v1_screen.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_extractable_v1_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedRootExtractableV1Review9Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_extractable_v1_review9_frozen_trace_metrics.csv`
- Contents: 9 source-gated original-seed strict roots, selected from RootExtractableA/B only; 9/9 solved, S=1/A=8, no LocalEasy rows. Demo scene is mounted to this pack.

## Dynamic RoleMap SmallCanvas Review1

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DynamicRoleMapSmallCanvasReview1Pack.asset`
- Pack GUID: `91bbd2d947f34878b6301b6b49392e1b`
- Selected input: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dynamic_rolemap_smallcanvas_review_pack_input.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_DynamicRoleMapSmallCanvasReview1Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dynamic_rolemap_smallcanvas_review1_frozen_trace_abs_metrics.csv`
- Contents: 6-level visual progression pack for dynamic RoleMap fill on the 18x24 small canvas. Level 1 is the 0.613 solved parent; Levels 2-6 are solved one-chain dynamic-fill candidates through step3, with supportDepth preserved or improved to d4.

## All Seed Root Extractable V2 Review13

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_AllSeedRootExtractableV2Review13Pack.asset`
- Pack GUID: `8db16fca66f9417f990df524607548b3`
- Prefilter screen: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_root_source_prefilter_v1_screen.csv`
- Trace eligibility screen: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_root_source_trace_eligibility_v1_screen.csv`
- Eligible source trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_root_source_trace_eligible_v1_trace_metrics.csv`
- Strict candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_strict_role_root_v1_shortid_candidates.csv`
- Strict candidate trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_strict_role_root_v1_shortid_trace_metrics.csv`
- Catalog: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_strict_role_root_v1_catalog.csv`
- Extractability screen: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_root_extractability_gate_v2_screen.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_root_extractable_v2_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_AllSeedRootExtractableV2Review13Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_root_extractable_v2_review13_frozen_trace_metrics.csv`
- Contents: 13 one-per-source original-seed strict role roots from the all-951 seed scan. Frozen trace is 13/13 solved with S=3/A=9/B=1; Demo scene in `.worktrees/sgp-rhythm-lab` is mounted to this pack.

## Root Canvas Variant V1B Review16

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootCanvasVariantV1BReview16Pack.asset`
- Pack GUID: `dc94d42f0ca34ecfb9d265855f14f4b7`
- Source roots: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_canvas_variant_v1_source_roots.csv`
- Candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_canvas_variant_v1b_candidates.csv`
- Candidate trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_canvas_variant_v1b_trace_metrics.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_canvas_variant_v1b_review16_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootCanvasVariantV1BReview16Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_canvas_variant_v1b_review16_frozen_trace_metrics.csv`
- Signature summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_canvas_variant_v1b_review16_signature_summary.md`
- Backbone summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_canvas_variant_v1b_review16_backbone_summary.md`
- Contents: 16 self-produced root/canvas variants from support_lock, strict_dual, web_crossover and hub_spoke roots; 16/16 solved, S=1/A=15. Demo scene in `.worktrees/sgp-rhythm-lab` is mounted to this pack.

## Root Variant Mixed V1 Review16

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantMixedV1Review16Pack.asset`
- Pack GUID: `e09071087b31411381ecea8cb88168d9`
- Mixed selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_mixed_v1_review16_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantMixedV1Review16Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_mixed_v1_review16_frozen_trace_metrics.csv`
- Signature summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_mixed_v1_review16_signature_summary.md`
- Backbone summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_mixed_v1_review16_backbone_summary.md`
- Contents: 16 self-produced root variants: 4 root families x (2 canvas_embedding + 2 peripheral_jitter_soft). Frozen trace 16/16 solved A; Demo scene is mounted to this pack.

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_experience_variant_v1_review15_selected.csv` | Root Experience Variant V1 review input | 15 peripheral-jitter-only variants; no mirror/rotation/pure canvas embedding; duplicate moved signatures removed. |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootExperienceVariantV1Review15Pack.asset` | Root Experience Variant V1 review pack | Current Demo pack for experiential root variants; GUID `a41c7df3082c4e87bdd15e2aee3370d4`. |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_experience_variant_v1_review15_frozen_trace_metrics.csv` | Root Experience Variant V1 frozen trace | 15/15 solved, all A-tier; TrueHardCandidate=6, HardPotential=6, MediumStructure=3. |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_experience_variant_v1_review15_backbone_summary.md` | Root Experience Variant V1 backbone summary | Reports 5 causal backbone roots and 10 backbone variants for the mounted experiential variant pack. |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVisiblePeripheralJitterV1Review3Pack.asset` | Root Visible Peripheral Jitter V1 probe pack | Current Demo probe after soft jitter was judged too subtle; GUID `b8c910e29ce84f72b83905d577836553`. |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_visible_peripheral_jitter_v1_review3_frozen_trace_metrics.csv` | Visible jitter V1 frozen trace | 3/3 solved A; use only to judge whether aggressive single-chain jitter has enough visible difference. |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootClusterRemapV1BReview6Pack.asset` | Root Cluster Remap V1B probe pack | Current Demo pack for cluster-translation variant review; GUID `c2ce4a8cd84e4e6db1fd34d0b6e97ff0`. |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_cluster_remap_v1b_review6_frozen_trace_metrics.csv` | Root Cluster Remap V1B frozen trace | 6/6 solved A; use to judge whether non-core cluster translation creates enough visible/experiential difference. |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootRoleZoneSwapV1Review2Pack.asset` | Root Role-Zone Swap V1 probe pack | Current Demo pack for non-zero role-zone swap review; GUID `d44b9b5752e44d71b37b8d79cfa2a619`. |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_role_zone_swap_v1_review2_frozen_trace_metrics.csv` | Root Role-Zone Swap V1 frozen trace | 2/2 solved, tiers S/A, both HardPotential; use to judge whether role-zone swap creates enough visible/experiential difference. |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootRoleZoneSwapV1Roots12Review1Pack.asset` | Root Role-Zone Swap V1 single proof | Current Demo pack after duplicated Review2 was rejected; one non-zero hub_spoke proof only, GUID `edb2b3dbca7f426089050a3d829fb356`. |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_role_zone_swap_v1_roots12_review1_frozen_trace_metrics.csv` | Single proof frozen trace | 1/1 solved, S-tier, TrueHardCandidate; demonstrates old-root role-zone mutation can work technically but not as a diverse production source. |

## Causal Root Family One Each V1 Review6

- Pack: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_CausalRootFamilyOneEachV1Pack.asset
- Pack GUID: 4d9c124509544bebb0d177676f89a8fa
- Selected CSV: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_root_family_one_each_v1_selected.csv
- Source/audit: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/mixed_root_family_review_v1_identity_audit.csv
- Frozen trace/audits: mixed_root_family_review_v1_frozen_trace_metrics.csv, mixed_root_family_review_v1_frozen_*_gate_*.csv
- Contents: one representative each for support_lock, strict_dual, web_crossover, hub_spoke, cascade_relay, split_key. Demo scene is mounted to this pack for root-family visual review.

## Campaign500 Hardening Analyzer V1

- Report folder: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/`.
- Latest calibrated run: `campaign_hardening_final_and_candidate_pool_20260624_224354_summary.csv`, `campaign_hardening_final_and_candidate_pool_20260624_224354_leak_rank.csv`, `campaign_hardening_final_and_candidate_pool_20260624_224354_top20_plan.csv`, `campaign_hardening_final_and_candidate_pool_20260624_224354_notes.md`.
- Review pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningLeakReviewPack.asset`; contains top 20 leak candidates from the latest analyzer run and is not auto-mounted to Demo.
- Sandbox V1 pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV1Pack.asset`; contains original/light/heavy copies of top 10 leak-priority levels.
- Sandbox V1 levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V1/`.
- Sandbox V1 report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v1_20260624_224903.csv`.
- Sandbox V2 pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV2Pack.asset`; contains 10 original/strong pairs, currently mounted to Demo for review.
- Sandbox V2 levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V2/`.
- Sandbox V2 report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v2_20260624_234908.csv`.
- Use this when selecting levels for difficulty hardening sandbox, especially for early free outer exits, high opening choices, weak dependency proxy, and boundary straight/short outer leak cleanup.

## Gate Vocabulary V1 Light Review Pack

- Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GateVocabularyV1LightReviewPack.asset`
- Pack guid: `0a8b5ee614ec49deb72f2bb146324904`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_GateVocabularyV1LightReviewPack.csv`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocabulary_v1_light_review_candidates.csv`
- Trace CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocabulary_v1_light_review_trace_metrics.csv`
- Purpose: inspect distinct gate/door designs inside shared-core strict-dual family; levels 1,2,4 are strict-dual trace candidates, level 3 is a rejected right-facing visual probe.

## Gate Vocabulary V1 Solved Skeleton Growth Review Pack

- Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GateVocabularyV1SolvedSkeletonGrowthReviewPack.asset`
- Pack guid: `584221c803cf4d5fa98ac9ac479176ae`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_GateVocabularyV1SolvedSkeletonGrowthReviewPack.csv`
- Selected source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocabulary_v1_solved_skeleton_growth_review_selected.csv`
- Skeleton trace CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocabulary_v1_skeleton_only_trace_metrics.csv`
- Conservative growth trace CSVs: `gate_vocabulary_v1_strict_dual_from_solved_skeleton_t020_b01_r*_trace_metrics.csv`
- Purpose: review solved strict-dual gate skeleton vocabulary and first safe-fill growth steps before pushing coverage higher.

## Gate Vocabulary V1 One-Per-Door Production Probe Pack

- Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GateVocabularyV1OnePerDoorProdReviewPack.asset`
- Pack GUID: `724ca47d6f744c088f20d60fe3412dfa`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocab_prod_one_per_door_v1_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_GateVocabularyV1OnePerDoorProdReviewPack.csv`
- Frozen trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_GateVocabularyV1OnePerDoorProdReviewPack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocab_prod_one_per_door_v1_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GateVocabularyV1OnePerDoorProdReviewFrozen`
- Contents: 4 solved strict-dual candidates, one per gate/door design. This is a low-coverage vocabulary review pack (`coverage 0.1091-0.1348`), not a final 0.30 production pack.

## Gate Vocabulary Door Push020 Mid Review Pack

- Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GateVocabularyDoorPush020MidReviewPack.asset`
- Pack GUID: `9a4ff7b38d774e3a83b0d13b5294a2a7`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocab_door_push020_mid_review4_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_GateVocabularyDoorPush020MidReviewPack.csv`
- Frozen trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_GateVocabularyDoorPush020MidReviewPack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocab_door_push020_mid_review_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GateVocabularyDoorPush020MidReviewFrozen`
- Contents: 4 one-per-door strict-dual candidates at coverage `0.1679-0.1814`; all solved, strictDualGateCandidate, TrueHardCandidate, A-tier.

## Gate Vocabulary Door MultiVar V1 Review8 Pack

- Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GateVocabularyDoorMultiVarV1Review8Pack.asset`
- Pack GUID: `85a0f9a3cf9b4a80a712c7db013d4c4f`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocab_door_multivar_v1_review8_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_GateVocabularyDoorMultiVarV1Review8Pack.csv`
- Frozen trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_GateVocabularyDoorMultiVarV1Review8Pack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocab_door_multivar_v1_review8_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GateVocabularyDoorMultiVarV1ReviewFrozen`
- Contents: 8 strict-dual door variants (4 door designs x 2 seed/fill variants). Frozen trace: 8/8 solved, strictDualGateCandidate, A-tier; 7 TrueHardCandidate and 1 HardPotential.

## Non-Door Root MultiVar V1 Review11 Pack

- Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_NonDoorRootMultiVarV1Review11Pack.asset`
- Pack GUID: `d2b359c580fc48a7ab06e61af81a4499`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/non_door_root_multivar_v1_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_NonDoorRootMultiVarV1Review11Pack.csv`
- Frozen trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_NonDoorRootMultiVarV1Review11Pack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/non_door_root_multivar_v1_review11_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/NonDoorRootMultiVarV1ReviewFrozen`
- Contents: 11 non-door root review levels: support_lock, web_crossover, hub_spoke, cascade_relay, split_key existing family pairs plus one newly recovered support_lock fill variant. Current Demo activePack points here.

## Gate Vocabulary Door Size Smoke V1 Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GateVocabularyDoorSizeSmokeV1Pack.asset`
- Pack GUID: `5d0ff4be64c94ad48788fa3766f67593`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocab_door_size_smoke_v1_candidates.csv`
- Absolute trace input: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocab_door_size_smoke_v1_candidates_abs.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocab_door_size_smoke_v1_selected.csv`
- Trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocab_door_size_smoke_v1_trace_abs_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GateVocabularyDoorSizeSmokeV1Frozen`
- Contents: 8 non-mirror size variants: first 4 strict-dual door variants x `wide30_shift`/`tall40_shift`. Solved/strictDual 8/8; tall shift looked safer than wide shift in class retention.

## Non-Door Root Size Smoke V1 Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_NonDoorRootSizeSmokeV1Pack.asset`
- Pack GUID: `6e4a8fbaf2dd4fe9ab2ff1f718737b62`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/non_door_root_size_smoke_v1_candidates.csv`
- Absolute trace input: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/non_door_root_size_smoke_v1_candidates_abs.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/non_door_root_size_smoke_v1_selected.csv`
- Candidate trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/non_door_root_size_smoke_v1_trace_abs_metrics.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_NonDoorRootSizeSmokeV1Pack.csv`
- Frozen trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_NonDoorRootSizeSmokeV1Pack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/non_door_root_size_smoke_v1_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/NonDoorRootSizeSmokeV1Frozen`
- Contents: 6 non-door non-mirror size variants: hub_spoke, split_key, web_crossover x wide30/tall40. All solved/A; 3 TrueHardCandidate and 3 HardPotential. Support_lock size variants from the old Medium baseline were excluded.

## Non-Door Root Size Smoke V1 Plus Support Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_NonDoorRootSizeSmokeV1PlusSupportPack.asset`
- Pack GUID: `be8f304517d54a58b8fcdb9295504ce0`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/non_door_root_size_smoke_v1_plus_support_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_NonDoorRootSizeSmokeV1PlusSupportPack.csv`
- Frozen trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_NonDoorRootSizeSmokeV1PlusSupportPack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/non_door_root_size_smoke_v1_plus_support_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/NonDoorRootSizeSmokeV1PlusSupportFrozen`
- Contents: 8 non-door non-mirror size variants: support_lock, web_crossover, hub_spoke, split_key x wide30/tall40. Frozen trace 8/8 solved/A; 4 TrueHardCandidate + 4 HardPotential. Not currently mounted to Demo.

## Tonight Fullish Max44 V1 Review Pack

- Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_TonightFullishMax44V1ReviewPack.asset`
- Pack GUID: `2f4a0a26d8b44ac99d8443914bd1a2de`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/tonight_smallcanvas_max44_v1_selected6.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_TonightFullishMax44V1ReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/tonight_fullish_max44_v1_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/TonightFullishMax44V1Review`
- Contents: 6 small-canvas full-ish candidates from the verified 0.338 parent route. Frozen trace: 6/6 solved, all supportClosureBestDepth=4, avgChoices `3.61-5.20`, maxChoices `7-9`, outerExitHeadCount `8-9`; 5/6 MediumStructure and 1/6 LocalEasy. Current Demo activePack points here.

## Root Variant Library V1 Core Pack

- Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV1CorePack.asset`
- Pack GUID: `a9802eed58384d9eb06618041ff1b457`
- Core selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_core_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV1CorePack.csv`
- Frozen trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV1CorePack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_core_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVariantLibraryV1CoreFrozen`
- Contents: 29 core root/variant candidates, filtered to solved/A and TrueHardCandidate/HardPotential only. Includes strict_dual_gate, support_lock, web_crossover, hub_spoke, split_key plus non-mirror size variants. Current Demo activePack points here.

## Tonight Fullchain Growth Review5 Pack

- Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_TonightFullchainGrowthReview5Pack.asset`
- Pack GUID: `17ae8e3c914a41b2be6399f7258be1bf`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/tonight_fullchain_growth_review5_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_TonightFullchainGrowthReview5Pack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/tonight_fullchain_growth_review5_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/TonightFullchainGrowthReview5`
- Contents: 6 growth/boundary nodes from one verified ~0.30 parent. First 4 are A-tier strict-ish growth nodes up to coverage `0.5686275`; last 2 are B-tier MediumStructure density-boundary nodes up to `0.5955882`. Current `.worktrees/sgp-rhythm-lab` Demo activePack points here.

## Tonight Fullchain Growth Review6 Pack

- Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_TonightFullchainGrowthReview6Pack.asset`
- Pack GUID: `a2f7314b49f84817970ec0a4a6c65b44`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/tonight_fullchain_growth_review6_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_TonightFullchainGrowthReview6Pack.csv`
- Absolute trace input: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/tonight_fullchain_growth_review6_trace_input.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/tonight_fullchain_growth_review6_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/TonightFullchainGrowthReview6`
- Contents: 10 high-density growth nodes from the same verified ~0.30 parent, spanning coverage `0.5992647 -> 0.6384804`. Frozen trace is 10/10 solved; B-tier holds through `0.629902`, while `0.6335784` and `0.6384804` are solved/depth4 but process Drop due high openers/choice peak/near-outer patch bursts. Current `.worktrees/sgp-rhythm-lab` Demo activePack points here.

## Campaign500 Hardening Sandbox V3

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV3Pack.asset`
- Pack GUID: `28ab2ac1c2c809d47a3d82be185cb2d9`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V3`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v3_20260625_011430.csv`
- Contents: 10 original/pressure pairs from Campaign500 leak-ranked levels. Odd entries are original copies, even entries are V3 pressure-hardening outputs.
- V3 pressure averages over 10 outputs: opening choices `27.3 -> 22.2`, direct clearable outer exits `27.3 -> 22.2`, early average choices `26.42 -> 21.72`, full average choices `14.32 -> 11.65`, leak score `602.5 -> 507.6`, chains `145.0 -> 144.2`. Greedy solved 10/10.
- Current `Assets/ArrowMagic/Scenes/Demo.unity` activePack points to this V3 pack.

## Campaign500 Hardening Sandbox V4

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV4Pack.asset`
- Pack GUID: `be23e479c2ed2c74987c99ebef164ab1`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V4`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v4_20260625_064105.csv`
- Contents: 10 original/gate-v4 pairs from Campaign500 leak-ranked levels. Odd entries are original copies, even entries are V4 gate-fold outputs.
- V4 gate averages over 10 outputs: opening choices `27.8 -> 18.1`, direct clearable outer exits `27.8 -> 18.1`, early average choices `26.78 -> 17.59`, full average choices `14.71 -> 10.87`, leak score `610.8 -> 413.2`, chains `156.1 -> 151.1`. Greedy solved 10/10.
- Superseded for current review by V4.1; keep this pack/report for V4 baseline comparison.

## Campaign500 Hardening Sandbox V4.1

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV41Pack.asset`
- Pack GUID: `23f5588cd74a74c488e7752bbd4f825e`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V41`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v41_20260625_071610.csv`
- Contents: 8 V4/V4.1 pairs from the V4 accepted outputs. Odd entries are V4 copies, even entries are V4.1 second-pass gate-fold outputs.
- V4.1 averages over 8 outputs: chains `141.875 -> 140.5`, opening choices `18.75 -> 15.5`, direct clearable outer exits `18.75 -> 15.5`, early average choices `18.50 -> 16.26`, full average choices `10.84 -> 10.05`, leak score `422 -> 358.5`. Greedy solved 8/8.
- Current `Assets/ArrowMagic/Scenes/Demo.unity` activePack points to this V4.1 pack for review.

## Campaign500 Hardening Sandbox V4.2 Boundary Probe

- Pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV42Pack.asset`
- Pack GUID: `a8bd54c2ac26d1141862d736a4c745a1`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V42`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v42_20260625_072958.csv`
- Contents: 1 accepted V4.1/V4.2 pair plus skipped rows in the report; this is a saturation/boundary artifact, not the main demo review pack.
- Result: only campaign order `449` improved (`opening/direct outer 14 -> 13`, avg choices `8.069 -> 7.908`, leak `301 -> 281`); 7/8 scanned rows had `ops=0`.

## Campaign500 Hardening Sandbox V5 Visible Gate

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV5Pack.asset`
- Pack GUID: `045e629960371db42b4407cfdf5e8752`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V5`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v5_20260625_075227.csv`
- Contents: 6 original/visible-gate pairs. Odd entries are original copies, even entries are V5 outputs with short inserted hook/bend gate chains on direct outer-exit rays.
- V5 averages over 6 outputs: chains `125 -> 128`, arrow tiles `917.17 -> 926.17`, opening choices `28 -> 21.83`, direct clearable outer exits `28 -> 21.83`, early average choices `26.90 -> 19.98`, full average choices `14.81 -> 9.70`, leak score `602.33 -> 455.83`. Greedy solved 6/6.
- Superseded for current review by V6; keep this pack/report for visible-gate baseline comparison.

## Campaign500 Hardening Sandbox V6 Early Peel Gate

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV6Pack.asset`
- Pack GUID: `6e5820f0c0e73ff4bae751486923559b`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V6`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v6_20260625_082646.csv`
- Contents: 6 original/early-peel-gate pairs. Odd entries are original copies, even entries are V6 outputs with short hook/bend gates selected from simulated early peel waves.
- V6 averages over 6 outputs: chains `125 -> 129`, arrow tiles `917.17 -> 929.17`, opening choices `28 -> 23.33`, direct clearable outer exits `28 -> 23.33`, early average choices `26.90 -> 21.75`, full average choices `14.81 -> 10.95`, leak score `602.33 -> 486.67`. Greedy solved 6/6.
- Superseded for current Demo review by V7; keep this pack as the deeper-peel comparison baseline.

## Campaign500 Hardening Sandbox V7 Opening Peel Gate

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV7Pack.asset`
- Pack GUID: `056a052d84fe8794c9d59e7b18ece827`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V7`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v7_20260625_091100.csv`
- Contents: 4 original/opening-peel-gate pairs. Odd entries are original copies, even entries are V7 outputs with short opening-gate chains inserted onto current opening clearable direct-exit rays.
- V7 averages over 4 outputs: chains `122.75 -> 127.75`, arrow tiles `912.75 -> 927.75`, opening choices `30.25 -> 22.5`, direct clearable outer exits `30.25 -> 22.5`, full average choices `14.41 -> 9.95`, leak score `620 -> 464.75`. Greedy solved 4/4.
- Superseded for current Demo review by V8. V7 is stricter than V6 on opening reduction but is an add-chain tactical sandbox, not a production pass.

## Campaign500 Hardening Sandbox V8 Opening Rewire Gate

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV8Pack.asset`
- Pack GUID: `ec493029142ef3d4c8b34f2c1f28afcf`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V8`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v8_20260625_092655.csv`
- Contents: 4 original/opening-rewire-gate pairs. Odd entries are original copies, even entries are V8 outputs with current opening free chains merged/rewired; one accepted sample uses a short bridge, most accepted operations are adjacent chain rewires.
- V8 averages over 4 outputs: chains `150 -> 147.25`, arrow tiles `963.25 -> 965`, opening choices `26.75 -> 20`, direct clearable outer exits `26.75 -> 20`, full average choices `14.70 -> 10.89`, leak score `596.75 -> 466`. Greedy solved 4/4.
- Superseded for current Demo review by V9. V8 is the first hardening sandbox in this series that primarily reduces opening pressure by changing existing chain structure instead of adding standalone gate chains.

## Campaign500 Hardening Sandbox V9 Opening Outer Rewire Gate

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV9Pack.asset`
- Pack GUID: `9b28502a2d25e23459733bd8fcd480d9`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V9`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v9_20260625_095945.csv`
- Contents: 4 original/opening-outer-rewire-gate pairs. Odd entries are original copies, even entries are V9 outputs.
- V9 operations: bulk-flip direct outer heads inward, then run a post opening-rewire pass without fallback add-gates. Report includes both clearable outer exits and all direct outer exits.
- V9 averages over 4 outputs: chains `151 -> 150`, arrow tiles `1019 -> 1020.25`, opening choices `28 -> 18.5`, direct clearable outer exits `28 -> 18.5`, all direct outer exits `28 -> 18.5`, full average choices `14.72 -> 9.87`, leak score `614.5 -> 409`. Greedy solved 4/4.
- Superseded for current Demo review by V10.
- Limitation: V9 reduces one-ended direct outer heads well, but remaining two-ended boundary chains still show many outer exits. Next breakthrough should add a dedicated endpoint-inset / boundary double-ended chain merge operator rather than more flipping.

## Campaign500 Hardening Sandbox V10 Outer Exit Endpoint

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV10Pack.asset`
- Pack GUID: `2c430fadb59a22249a45f1bf01814753`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V10`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v10_20260625_104309.csv`
- Source report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v9_20260625_095945.csv`
- Contents: 3 V9-before/outer-exit-endpoint pairs. Odd entries are V9 outputs before endpoint hardening, even entries are V10 endpoint outputs.
- V10 operations: endpoint reroute moves a direct outer chain head to a nearby empty cell around its neck; endpoint trim removes a short boundary-facing head segment. Every accepted step must keep Greedy solved and reduce all direct outer exits.
- V10 averages over 3 accepted outputs, measured from V9 source to V10 output: chains `139.67 -> 139.67`, arrow tiles `997 -> 988.33`, opening choices `18 -> 12.33`, direct clearable outer exits `18 -> 12.33`, all direct outer exits `18 -> 12.33`, full average choices `10.28 -> 7.31`, leak score `395 -> 275.33`. Greedy solved 3/3.
- Skipped sample: L449 only improved direct outer exits `20 -> 19`, so it was rejected under the outer-exit-focused acceptance rule.
- Superseded for current Demo review by V11.

## Campaign500 Hardening Sandbox V11 Multi-Layer Outer Exit

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV11Pack.asset`
- Pack GUID: `29a79c0bec2fdd740a603c8e8bf06340`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V11`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v11_20260625_111032.csv`
- Source report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v10_20260625_104309.csv`
- Contents: 3 V10-before/multi-layer-V11 pairs. Odd entries are V10 endpoint outputs before V11, even entries are V11 outputs.
- V11 operations: simulate the first 4 Greedy peel waves, collect chains that are clearable in those early waves and whose head ray reaches the outside after earlier layers clear, then apply future-outer orientation flip, endpoint reroute, and trim. Every accepted step keeps Greedy solved and reduces multi-layer peel outer risk.
- V11 averages over 3 outputs, measured from V10 source to V11 output: chains `139.67 -> 139.67`, arrow tiles `988.33 -> 981`, opening choices `12.33 -> 12.33`, all direct outer exits `12.33 -> 12.33`, peel-layer outer exits `51 -> 37`, future peel outer exits `38.67 -> 24.67`, full average choices `7.31 -> 5.72`. Greedy solved 3/3.
- Important interpretation: V11 does not yet reduce current-frame direct outer exits beyond V10; it reduces the second/third/fourth-layer outer exits that appear after the initial clearable layer is removed. Current `Assets/ArrowMagic/Scenes/Demo.unity` activePack points to this V11 pack for review.

## Campaign500 Hardening V12 PBE/NEE Classification

- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_v12_pbe_nee_20260625_112948.csv`
- Source report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v11_20260625_111032.csv`
- Contents: analysis-only CSV over the 3 V10-before/V11-after pairs; no new level pack and no Demo change.
- Definitions: `PBE` = persistent boundary exit whose head ray already reaches outside on the initial board; `NEE` = newly exposed exit whose head ray only reaches outside after earlier peel waves clear blockers.
- Result: V10-before averages direct outer `12.33`, peel outer `51`, future outer `38.67`, PBE `12.33`, future PBE `0`, NEE `38.67`. V11-after averages direct outer `12.33`, peel outer `37`, future outer `24.67`, PBE `12.33`, future PBE `0`, NEE `24.67`.
- Interpretation: in this sample, V11's future-layer improvement is entirely NEE reduction. The remaining current-frame direct outer problem is PBE/wave0 and needs boundary structure repair; the early continuous sweep problem is NEE and should be treated by peel-aware propagation gates.

## Campaign500 Hardening Sandbox V12 BDR-lite Boundary Rewire

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV12BDRPack.asset`
- Pack GUID: `22d9ef7c9eba2844d8a2af3166daad93`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V12BDR`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v12_bdr_20260625_114050.csv`
- Source report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v11_20260625_111032.csv`
- Contents: 3 V11-before/V12BDR pairs. Odd entries are V11 outputs before boundary rewiring, even entries are V12BDR outputs.
- Operation: BDR-lite prepends a small one-cell hook at selected current direct-outer/PBE endpoints. It does not add chains; it adds 1-2 tiles per accepted sample and requires Greedy solvability, current direct outer/PBE drop, and NEE non-regression.
- Result over 3 outputs from V11 to V12BDR: chains `139.67 -> 139.67`, arrow tiles `981 -> 983`, opening choices `12.33 -> 10.67`, all direct outer exits `12.33 -> 10.67`, peel outer exits `37 -> 32`, future peel outer exits `24.67 -> 21.33`, full average choices `5.72 -> 5.36`. Greedy solved 3/3.
- Interpretation: BDR-lite is directionally correct but still mild. It proves current PBE can be reduced without NEE rebound, but production-level impact likely needs stronger boundary double-end restructuring or two-cell/merge-based stitching.

## Root Variant Library V1.2 Core Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV12CorePack.asset`
- Pack GUID: `1683b2f5c1aa4a129d36f4bd00e2efff`
- Core selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_2_core_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV12CorePack.csv`
- Absolute trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV12CorePack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_2_core_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVariantLibraryV12CoreFrozen`
- Contents: 35 root/variant candidates across strict_dual_gate, support_lock, web_crossover, hub_spoke, split_key, and cascade_relay. Current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.3 Core Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV13CorePack.asset`
- Pack GUID: `ba7c9cf303914fc695b55e69be80763e`
- Core selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_3_core_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV13CorePack.csv`
- Absolute trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV13CorePack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_3_core_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVariantLibraryV13CoreFrozen`
- Contents: 36 root/variant candidates across strict_dual_gate, support_lock, web_crossover, hub_spoke, split_key, and cascade_relay. V1.3 adds one trace/classifier-approved non-size `hub_spoke` variant to V1.2; current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.4 Core Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV14CorePack.asset`
- Pack GUID: `3c1bfa73bb1c4c2c885dfbfca5849f7b`
- Core selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_4_core_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV14CorePack.csv`
- Absolute trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV14CorePack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_4_core_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVariantLibraryV14CoreFrozen`
- Contents: 37 root/variant candidates. V1.4 adds the `wide30_shift` size variant of the newly admitted `hub_spoke` V1.3 root; current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.5 Core Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV15CorePack.asset`
- Pack GUID: `f1f40d935d264f729fa7390bde978ac9`
- Core selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_5_core_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV15CorePack.csv`
- Absolute trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV15CorePack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_5_core_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVariantLibraryV15CoreFrozen`
- Contents: 39 root/variant candidates. V1.5 adds two non-size `support_lock` variants to V1.4; current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.6 Core Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV16CorePack.asset`
- Pack GUID: `d6dbde4de6d94b07be26fcb17779ab7e`
- Core selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_6_core_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV16CorePack.csv`
- Absolute trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV16CorePack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_6_core_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVariantLibraryV16CoreFrozen`
- Contents: 43 root/variant candidates. V1.6 balances web_crossover and split_key to 6 each by adding unique wide/tall size variants that pass family classifiers; current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.7 Core Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV17CorePack.asset`
- Pack GUID: `80de53d213124a79b9e87fe1dd4cfe05`
- Core selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_7_core_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV17CorePack.csv`
- Absolute trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV17CorePack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_7_core_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVariantLibraryV17CoreFrozen`
- Contents: 44 root/variant candidates. Non-door families are balanced at 6 each; strict_dual_gate remains 14. Current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.8 Balanced Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV18BalancedReviewPack.asset`
- Pack GUID: `56cc167e1e3d4c7e9824595d39f67098`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_8_balanced_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV18BalancedReviewPack.csv`
- Absolute trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV18BalancedReviewPack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_8_balanced_review_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVariantLibraryV18BalancedReviewFrozen`
- Contents: 36 candidates, exactly 6 per family across support_lock, strict_dual_gate, hub_spoke, web_crossover, split_key, and cascade_relay. Current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here for manual review.

## Root Variant Library V1.9 New Variants Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV19NewVariantsReviewPack.asset`
- Pack GUID: `dd827c0c9cf148b3840c571946af7fc2`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_9_new_variants_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV19NewVariantsReviewPack.csv`
- Absolute trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV19NewVariantsReviewPack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_9_new_variants_review_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVariantLibraryV19NewVariantsReviewFrozen`
- Audit inputs: `root_variant_library_v1_8_balanced_review_geometry_audit.csv`, `root_variant_library_v1_8_balanced_review_similarity.csv`, `root_variant_library_v1_8_balanced_review_guide.md`.
- Contents: 25 levels grouped as source controls plus new variants: cascade_relay 1+3, hub_spoke 1+1, split_key 1+4, strict_dual_gate 3+3, support_lock 1+1, web_crossover 1+5. Frozen trace: 25/25 solved, all A-tier, 21 TrueHardCandidate + 4 HardPotential. Current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.10 Hub/Support Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV110HubSupportReviewPack.asset`
- Pack GUID: `fd02a36230034d6ea401bed00ded456b`
- Source roots CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_10_hub_support_source_roots.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_10_hub_support_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV110HubSupportReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_10_hub_support_review_frozen_trace_metrics.csv`
- Joined frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_10_hub_support_review_frozen_trace_joined.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVariantLibraryV110HubSupportReviewFrozen`
- Intermediate candidate reports: `root_variant_library_v1_10_hub_support_jitter_mild_*` and `root_variant_library_v1_10_hub_support_cluster_mild_*` under `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- Contents: 20 levels = 7 source controls plus 13 non-size hub/support variants. Frozen trace: 20/20 solved, all A-tier; variant mix is hub_spoke 7 new and support_lock 6 new. Current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.11 Hub/Support Size Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV111HubSupportSizeReviewPack.asset`
- Pack GUID: `17f503e6c015427b8dc3e3812422ea66`
- Size source variants CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_10_hub_support_size_source_variants.csv`
- Size smoke candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_10_hub_support_size_smoke_candidates.csv`
- Size smoke trace join: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_10_hub_support_size_smoke_joined_trace.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_11_hub_support_size_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV111HubSupportSizeReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_11_hub_support_size_review_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV111SizeFrozen`
- Contents: 26 non-mirror size variants from V1.10 hub/support variants (`wide30_shift` + `tall40_shift`). Frozen trace: 26/26 solved, all A-tier, 8 TrueHardCandidate + 18 HardPotential. Current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.12 Other Roots Size Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV112OtherRootsSizeReviewPack.asset`
- Pack GUID: `a096dfe33bbc455b96ea4c2c284cdbc9`
- Size source variants CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_12_other_roots_size_source_variants.csv`
- Size smoke candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_12_other_roots_size_smoke_candidates.csv`
- Size smoke trace join: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_12_other_roots_size_smoke_joined_trace.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_12_other_roots_size_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV112OtherRootsSizeReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_12_other_roots_size_review_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV112SizeFrozen`
- Contents: 23 non-mirror size variants from V1.9 `cascade_relay`, `split_key`, `strict_dual_gate`, and `web_crossover` new variants. Frozen trace: 23/23 solved, all A-tier, 13 TrueHardCandidate + 10 HardPotential; strict-dual retained rows keep `strictDualGateCandidate=True`. Current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.13 Cascade/Strict Cluster Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV113CascadeStrictClusterReviewPack.asset`
- Pack GUID: `3d575d3a2da44401807ba42005285ff1`
- Frozen source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_13_cascade_strict_cluster_source_frozen.csv`
- Cluster candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_13_cascade_strict_cluster_mild_candidates.csv`
- Cluster joined trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_13_cascade_strict_cluster_mild_joined_trace.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_13_cascade_strict_cluster_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV113CascadeStrictClusterReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_13_cascade_strict_cluster_review_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV113ClusterFrozen`
- Negative spatial recomposition diagnostics: `root_variant_library_v1_13_thin_roots_spatial_*` under `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- Contents: 7 levels = 3 source controls plus 4 conservative cluster variants for `cascade_relay` and `strict_dual_gate`. Frozen trace: 7/7 solved, all A-tier; strict-dual variants keep `strictDualGateCandidate=True`. Current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.14b Consolidated Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV114bConsolidatedReviewPack.asset`
- Pack GUID: `375e9f8eb8e6443d938e245fb558e400`
- Input pool CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_14_consolidated_pool_all.csv`
- First-pass selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_14_consolidated_review_selected.csv`
- Final selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_14b_consolidated_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV114bConsolidatedReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_14b_consolidated_review_frozen_trace_metrics.csv`
- Joined frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_14b_consolidated_review_frozen_trace_joined.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV114bConsolidatedFrozen`
- Contents: 45 deduped review candidates across six root families. Distribution: cascade_relay 8, hub_spoke 8, strict_dual_gate 8, support_lock 8, web_crossover 8, split_key 5. Frozen trace: 45/45 solved, all A-tier, 36 TrueHardCandidate + 9 HardPotential. All strict_dual rows keep `strictDualGateCandidate=True`. Current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.15c Consolidated Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV115cConsolidatedReviewPack.asset`
- Pack GUID: `7f7307ed6ac9454186e7115eaaba24c4`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_15c_consolidated_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV115cConsolidatedReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_15c_consolidated_review_frozen_trace_metrics.csv`
- Joined frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_15c_consolidated_review_frozen_trace_joined.csv`
- Split gate audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_15c_consolidated_review_frozen_split_gate_split_key_roots.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV115cConsolidatedFrozen`
- Split supplementary reports: `root_variant_library_v1_15_split_canvas_more_*`, `root_variant_library_v1_15_split_canvas_spatial_joined_trace_gate.csv`; destructive diagnostics retained as `root_variant_library_v1_15_split_rolezone_*`, `root_variant_library_v1_15_split_cluster_*`, and `root_variant_library_v1_15_split_spatial_recompose_*`.
- Contents: 48 balanced review candidates, 8 each across cascade_relay, hub_spoke, split_key, strict_dual_gate, support_lock, and web_crossover. Frozen trace: 48/48 solved, all A-tier, 40 TrueHardCandidate + 8 HardPotential. All 8 `split_key` rows keep `splitKeyCandidate=True`. Current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Cascade Relay Recovery V1

- Base CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/cascade_relay_recovery_v1_base.csv`
- Recovery trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/cascade_relay_recovery_v1_t026_b02_r2_trace_metrics.csv`
- Cascade classifier: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/cascade_relay_recovery_v1_t026_b02_r2_gate_cascade_roots.csv`
- Size smoke candidates/trace: `cascade_relay_size_smoke_v1_candidates.csv`, `cascade_relay_size_smoke_v1_trace_metrics.csv`, `cascade_relay_size_smoke_v1_gate_cascade_roots.csv`
- Core-admitted rows: recovery c16/c19/c07 plus tall40_shift size variants c02/c04; wide30_shift size variants are solved/cascade but MediumStructure and excluded.

## Root Variant Library V1.16 Size Nonrecursive Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV116SizeNonrecursiveReviewPack.asset`
- Pack GUID: `fe5c4868615449c094b867da86e8417c`
- Source rows CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_16_size_source_nonrecursive.csv`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_16_size_nonrecursive_candidates.csv`
- Candidate trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_16_size_nonrecursive_metrics.csv`
- Joined candidate/gate audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_16_size_nonrecursive_joined_trace_gate.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_16_size_nonrecursive_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV116SizeNonrecursiveReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_16_size_nonrecursive_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_16_size_nonrecursive_review_frozen_trace_joined.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV116SizeNonrecursiveFrozen`
- Contents: 31 non-mirror wide/tall canvas variants from V1.15c source/non-size rows. Frozen trace: 31/31 solved, all A-tier, 19 TrueHardCandidate + 12 HardPotential. Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack for visual review.

## Root Variant Library V1.17 Cascade/Hub Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV117CascadeHubReviewPack.asset`
- Pack GUID: `e15f131e6a104b1aa3b3fe2093b0206c`
- Source rows CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_17_cascade_hub_source_frozen.csv`
- All first-pass candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_17_cascade_hub_candidates_all.csv`
- All first-pass trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_17_cascade_hub_metrics.csv`
- First-pass joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_17_cascade_hub_joined_trace_gate.csv`
- Hub second-pass candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_17_hub_candidates_all.csv`
- Hub second-pass joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_17_hub_joined_trace_gate.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_17_cascade_hub_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV117CascadeHubReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_17_cascade_hub_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_17_cascade_hub_review_frozen_trace_joined.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV117CascadeHubFrozen`
- Contents: 10 non-size variants, 5 cascade_relay and 5 hub_spoke. Frozen trace: 10/10 solved, all A-tier, 4 TrueHardCandidate + 6 HardPotential. Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack for visual review.

## Root Variant Library V1.18 Cascade/Hub Size Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV118CascadeHubSizeReviewPack.asset`
- Pack GUID: `a72095f2ae7d4b598da21dc7e0856ce8`
- Source rows CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_18_cascade_hub_size_source.csv`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_18_cascade_hub_size_candidates.csv`
- Candidate trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_18_cascade_hub_size_metrics.csv`
- Candidate joined gate audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_18_cascade_hub_size_joined_trace_gate.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_18_cascade_hub_size_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV118CascadeHubSizeReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_18_cascade_hub_size_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_18_cascade_hub_size_review_frozen_trace_joined.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV118CascadeHubSizeFrozen`
- Contents: 9 non-mirror size variants from V1.17 cascade/hub non-size variants. Frozen trace: 9/9 solved, all A-tier, 3 TrueHardCandidate + 6 HardPotential. Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack for visual review.

## Root Variant Library V1.19 Consolidated Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV119ConsolidatedReviewPack.asset`
- Pack GUID: `7bb6312a6fa34784be320bebdaad03ba`
- Input pool CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_19_consolidated_pool_all.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_19_consolidated_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV119ConsolidatedReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_19_consolidated_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_19_consolidated_review_frozen_trace_joined.csv`
- Family gate audits: `root_variant_library_v1_19_consolidated_review_frozen_gate_cascade_cascade_roots.csv`, `root_variant_library_v1_19_consolidated_review_frozen_gate_hub_hub_spoke_roots.csv`, `root_variant_library_v1_19_consolidated_review_frozen_gate_split_split_key_roots.csv`, `root_variant_library_v1_19_consolidated_review_frozen_gate_web_web_roots.csv` under `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV119ConsolidatedFrozen`
- Contents: 52 review candidates: cascade_relay 10, hub_spoke 10, split_key 8, strict_dual_gate 8, support_lock 8, web_crossover 8. Frozen trace: 52/52 solved, all A-tier, 42 TrueHardCandidate + 10 HardPotential. Intended-family gate pass after frozen trace: 47/52; cascade 8/10, hub 8/10, split 8/8, strict dual 8/8, support 8/8, web 7/8. Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack for visual review.

## Root Variant Library V1.20 Identity-Clean Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV120IdentityCleanReviewPack.asset`
- Pack GUID: `4adcf0892b1b44c4930e971011f750a6`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_20_identity_clean_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV120IdentityCleanReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_20_identity_clean_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_20_identity_clean_review_frozen_trace_joined.csv`
- Family gate audits: `root_variant_library_v1_20_identity_clean_review_frozen_gate_cascade_cascade_roots.csv`, `root_variant_library_v1_20_identity_clean_review_frozen_gate_hub_hub_spoke_roots.csv`, `root_variant_library_v1_20_identity_clean_review_frozen_gate_split_split_key_roots.csv`, `root_variant_library_v1_20_identity_clean_review_frozen_gate_web_web_roots.csv` under `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV120IdentityCleanFrozen`
- Contents: 47 identity-clean review candidates: cascade_relay 8, hub_spoke 8, split_key 8, strict_dual_gate 8, support_lock 8, web_crossover 7. Frozen trace: 47/47 solved, all A-tier, 37 TrueHardCandidate + 10 HardPotential. All intended family gates pass after frozen trace. Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack for visual review and future production-stat baselining.

## Root Variant Library V1.21 Balanced-Clean Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV121BalancedCleanReviewPack.asset`
- Pack GUID: `c6e32e5f87e94afba369d42eba62708e`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_21_balanced_clean_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV121BalancedCleanReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_21_balanced_clean_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_21_balanced_clean_review_frozen_trace_joined.csv`
- Family gate audits: `root_variant_library_v1_21_balanced_clean_review_frozen_gate_cascade_cascade_roots.csv`, `root_variant_library_v1_21_balanced_clean_review_frozen_gate_hub_hub_spoke_roots.csv`, `root_variant_library_v1_21_balanced_clean_review_frozen_gate_split_split_key_roots.csv`, `root_variant_library_v1_21_balanced_clean_review_frozen_gate_web_web_roots.csv` under `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV121BalancedCleanFrozen`
- Contents: 48 balanced identity-clean review candidates, 8 each across cascade_relay, hub_spoke, split_key, strict_dual_gate, support_lock, and web_crossover. Frozen trace: 48/48 solved, all A-tier, 38 TrueHardCandidate + 10 HardPotential. All intended family gates pass after frozen trace. Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack and this is the current best root-variant-library baseline.

## Root Variant Library V1.22 Size-Nonrecursive Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV122SizeNonrecursiveReviewPack.asset`
- Pack GUID: `ea936cfad65d418aa03d3b6c24855c0e`
- Source rows CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_22_size_source_nonrecursive.csv`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_22_size_nonrecursive_candidates.csv`
- Candidate trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_22_size_nonrecursive_metrics.csv`
- Joined candidate/gate audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_22_size_nonrecursive_joined_trace_gate.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_22_size_nonrecursive_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV122SizeNonrecursiveReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_22_size_nonrecursive_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_22_size_nonrecursive_review_frozen_trace_joined.csv`
- Family gate audits: `root_variant_library_v1_22_size_nonrecursive_review_frozen_gate_cascade_cascade_roots.csv`, `root_variant_library_v1_22_size_nonrecursive_review_frozen_gate_hub_hub_spoke_roots.csv`, `root_variant_library_v1_22_size_nonrecursive_review_frozen_gate_split_split_key_roots.csv`, `root_variant_library_v1_22_size_nonrecursive_review_frozen_gate_web_web_roots.csv` under `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV122SizeNonrecursiveFrozen`
- Contents: 38 non-mirror size variants from V1.21 source/non-size rows. Distribution: cascade_relay 7, hub_spoke 3, split_key 4, strict_dual_gate 8, support_lock 8, web_crossover 8. Frozen trace: 38/38 solved, 37 A + 1 S, 22 TrueHardCandidate + 16 HardPotential. All retained rows pass intended family gate after frozen trace. Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack for size-expansion visual review.

## Root Variant Library V1.23 Hub Conservative Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV123HubConservativeReviewPack.asset`
- Pack GUID: `a4f8f06e9b88473abf3ed4e58e416723`
- Source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_23_hub_conservative_source.csv`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_23_hub_conservative_candidates_all.csv`
- Joined candidate/gate audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_23_hub_conservative_joined_trace_gate.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_23_hub_conservative_review_selected.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_23_hub_conservative_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_23_hub_conservative_review_frozen_trace_joined.csv`
- Contents: 6 conservative hub-spoke non-size variants. Frozen trace: 6/6 solved, all A-tier, 6/6 hub-spoke identity pass.

## Root Variant Library V1.24 Hub Conservative Size Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV124HubConservativeSizeReviewPack.asset`
- Pack GUID: `d375fa699a2846f6a20c7a76c12dcc7e`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_24_hub_conservative_size_candidates.csv`
- Joined candidate/gate audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_24_hub_conservative_size_joined_trace_gate.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_24_hub_conservative_size_review_selected.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_24_hub_conservative_size_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_24_hub_conservative_size_review_frozen_trace_joined.csv`
- Contents: 4 non-mirror hub-spoke size variants. Frozen trace: 4/4 solved, all A-tier, 4/4 hub-spoke identity pass.

## Root Variant Library V1.25 Split Conservative Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV125SplitConservativeReviewPack.asset`
- Pack GUID: `bc9bff8f450149488a34f9a1347a7c60`
- Source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_25_split_conservative_source.csv`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_25_split_conservative_candidates_all.csv`
- Joined candidate/gate audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_25_split_conservative_joined_trace_gate.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_25_split_conservative_review_selected.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_25_split_conservative_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_25_split_conservative_review_frozen_trace_joined.csv`
- Contents: 4 source-capped split-key non-size variants. Frozen trace: 4/4 solved, all A-tier, 4/4 split-key identity pass.

## Root Variant Library V1.26 Split Conservative Size Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV126SplitConservativeSizeReviewPack.asset`
- Pack GUID: `e6ec4727c8524d9ca53d4aacb3295f98`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_26_split_conservative_size_candidates.csv`
- Joined candidate/gate audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_26_split_conservative_size_joined_trace_gate.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_26_split_conservative_size_review_selected.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_26_split_conservative_size_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_26_split_conservative_size_review_frozen_trace_joined.csv`
- Contents: 7 dynamic-size split-key variants. Frozen trace: 7/7 solved, all A-tier, 7/7 split-key identity pass.

## Root Variant Library V1.27 Hub/Split Tonight Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV127HubSplitTonightReviewPack.asset`
- Pack GUID: `f73fdf8cefb84ff1aa5571accdd4ccf5`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_27_hub_split_tonight_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV127HubSplitTonightReviewPack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_27_hub_split_tonight_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_27_hub_split_tonight_review_frozen_trace_joined.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV127HubSplitTonightFrozen`
- Contents: 21 levels = hub root 6, hub size 4, split root 4, split size 7. Frozen trace: 21/21 solved, 21/21 intended family gate pass. Superseded by V1.28 as the current balanced review pack.

## Root Variant Library V1.28 Balanced Production Library Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV128BalancedProductionLibraryPack.asset`
- Pack GUID: `c81950ad0c0d43ac9dd91715c9d78ef7`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_28_balanced_production_library_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV128BalancedProductionLibraryPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_28_balanced_production_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_28_balanced_production_frozen_trace_joined.csv`
- Frozen joined summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_28_balanced_production_frozen_trace_joined_summary.md`
- Family gate audits: `root_variant_library_v1_28_balanced_production_frozen_gate_cascade_cascade_roots.csv`, `root_variant_library_v1_28_balanced_production_frozen_gate_hub_hub_spoke_roots.csv`, `root_variant_library_v1_28_balanced_production_frozen_gate_split_split_key_roots.csv`, `root_variant_library_v1_28_balanced_production_frozen_gate_web_web_roots.csv` under `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV128BalancedProductionFrozen`
- Contents: 72 levels, 12 each for `cascade_relay`, `hub_spoke`, `split_key`, `strict_dual_gate`, `support_lock`, and `web_crossover`. Frozen trace: 72/72 solved, 72/72 intended family gate pass. Superseded as Demo target by V1.30, but remains the balanced production-library baseline.

## Root Variant Library V1.29 Four-Family Non-Size Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV129FourFamReviewPack.asset`
- Pack GUID: `9f604bc426124186bab65ebaf9ccd478`
- Source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_29_expansion_source_four_families.csv`
- Candidate CSVs: `root_variant_library_v1_29_fourfam_candidates_all.csv`, `root_variant_library_v1_29_web_mild_jitter_candidates.csv`
- Candidate joined audits: `root_variant_library_v1_29_fourfam_joined_trace_gate.csv`, `root_variant_library_v1_29_fourfam_all_joined_trace_gate.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_29_fourfam_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV129FourFamReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_29_fourfam_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_29_fourfam_review_frozen_trace_joined.csv`
- Frozen joined summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_29_fourfam_review_frozen_trace_joined_summary.md`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV129FourFamFrozen`
- Contents: 16 non-size variants, 4 each for `cascade_relay`, `strict_dual_gate`, `support_lock`, and `web_crossover`. Frozen trace: 16/16 solved, 16/16 intended family gate pass.

## Root Variant Library V1.30 Four-Family Size Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV130FourFamSizeReviewPack.asset`
- Pack GUID: `0e14f4e499e54d089e0a92a1a81e7b27`
- Source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_30_fourfam_size_source.csv`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_30_fourfam_size_candidates.csv`
- Candidate joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_30_fourfam_size_joined_trace_gate.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_30_fourfam_size_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV130FourFamSizeReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_30_fourfam_size_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_30_fourfam_size_review_frozen_trace_joined.csv`
- Frozen joined summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_30_fourfam_size_review_frozen_trace_joined_summary.md`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV130FourFamSizeFrozen`
- Contents: 16 non-mirror size variants from V1.29 rows, 4 each for `cascade_relay`, `strict_dual_gate`, `support_lock`, and `web_crossover`. Frozen trace: 16/16 solved, 16/16 intended family gate pass. Superseded as Demo target by V1.31, but remains the focused four-family size-expansion review pack.

## Root Variant Library V1.31 Extended Balanced Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV131ExtendedBalancedReviewPack.asset`
- Pack GUID: `91a29088725441d3b604fa2e66f8d71e`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_31_extended_balanced_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV131ExtendedBalancedReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_31_extended_balanced_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_31_extended_balanced_frozen_trace_joined.csv`
- Frozen joined summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_31_extended_balanced_frozen_trace_joined_summary.md`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV131ExtendedBalancedFrozen`
- Contents: 108 review levels, 18 each for `cascade_relay`, `hub_spoke`, `split_key`, `strict_dual_gate`, `support_lock`, and `web_crossover`. Built from V1.28 baseline plus V1.29 four-family non-size variants, V1.30 four-family size variants, and V1.27 hub/split extras. Frozen trace: 108/108 solved, 108/108 intended family gate pass. Superseded as current Demo target by Original Seed Merged Usable Root V1 review.

## Original Seed Merged Usable Root V1 Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedMergedUsableRootV1Pack.asset`
- Pack GUID: `1096eb72369f4630ba4b9a09bdac9c27`
- Merged all-candidates CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_merged_usability_v1_all_candidates.csv`
- Selected source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_merged_usable_v1_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedMergedUsableRootV1Pack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_merged_usable_root_v1_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_merged_usable_root_v1_frozen_trace_joined.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/OriginalSeedMergedUsableRootV1Frozen`
- Contents: 16 original-seed root candidates, source-deduped from rolegraph/extractable scans and copied into short active paths from cold archive. Frozen trace: 16/16 solved, 7 S + 9 A, supportClosureBestDepth all 3-4. `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack currently points here for human filtering of truly usable original-seed roots.
## Campaign500 Hardening Sandbox V13BDR2 Boundary Inset

- Pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV13BDR2Pack.asset`
- Pack GUID: `900364ecf4764fe49beacb4d41643f3b`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V13BDR2`
- Latest report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v13_bdr2_20260625_115819.csv`
- Source report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v11_20260625_111032.csv`
- Contents: 2 accepted before/after pairs, with V11 before and V13 after. L387: direct/PBE `18->12`, opening `18->12`, NEE `33->30`. L405: direct/PBE `10->8`, opening `10->8`, NEE `30->21`. L173 was skipped in the report because it only improved PBE `9->8`.
- Demo scene `Assets/ArrowMagic/Scenes/Demo.unity` activePack currently points to this V13BDR2 pack for review.

## Campaign500 Hardening Sandbox V14CMP Boundary Compression

- Pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV14CMPPack.asset`
- Pack GUID: `9d95355a84e2c6643a7adc5765763940`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V14CMP`
- Latest report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v14_cmp_20260625_121057.csv`
- Source report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v13_bdr2_20260625_115819.csv`
- Contents: 1 accepted before/after pair, with V13 before and V14 after. L405: chains `167->165`, direct/PBE `8->6`, opening `8->6`, NEE `21->21`, arrow tiles unchanged. L387 was skipped because there was no safe adjacent boundary-compression candidate.
- Demo scene `Assets/ArrowMagic/Scenes/Demo.unity` activePack currently points to this V14CMP pack for review.

## SGP Pressure Hard Trial / Benchmark

- Trial pack: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardTrialPack.asset`
- Trial pack GUID: `acd1590a350614a4e86c901d33b5c5dd`
- Trial report: `Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_hard_trial_report.csv`
- Trial trace report: `.codex-run/sgp_pressure_trace_v16_scorecurve/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_hard_trial_v16_scorecurve_metrics.csv`
- Benchmark pack: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardBenchmarkPack.asset`
- Benchmark pack GUID: `c8e516eece57cc94ca87c60d18b5b0d3`
- Benchmark levels: `Assets/ArrowMagic/SOData/Levels/DirectProcedural/SGPPressureHardBenchmark/`
- Trace report: `.codex-run/sgp_pressure_benchmark_trace/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_hard_benchmark_metrics.csv`
- Contents: 3 copied validated hard-library benchmark levels; trace result 3/3 solved A-tier, openers `3-5`, avg choices `2.57-2.98`, max choices `5-6`, outer exits `0/1`. Demo currently points to the benchmark pack for review.
- Current trial contents: v16 direct SGP small long-chain pressure trial, 4/4 solved B-tier; chains `57-63`, openers `3-5`, avg choices `5.26-6.64`, max choices `8-12`. Demo currently points to the trial pack after the latest run.

## SGP Pressure Hard Production V1 - 2026-06-26

- Production wrapper: `Tools/Production/Invoke-SGPPressureHardProductionV1.ps1`
- Current review pack: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardTrialPack.asset`
- Current review pack GUID: `acd1590a350614a4e86c901d33b5c5dd`
- Current levels: `Assets/ArrowMagic/SOData/Levels/DirectProcedural/SGPPressureHardTrial/`
- Source report: `Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_hard_trial_report.csv`
- Speedcheck Unity log: `.codex-run/sgp_pressure_hard_production_v1_speedcheck_unity.log`
- Speedcheck trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_hard_production_v1_speedcheck_trace_metrics.csv`
- Current contents: 4 high-coverage normal-production review candidates: `lock_buckle` coverage `0.991`, `section_unlock` `0.994`, `dense_weave` `0.978`, `core_burst` `0.990`; all portable solved.
- Verification result: official trace 4/4 solved. Practical normal-production filter `coverage>=0.97 + solved + processTier A/B` keeps 3/4 (`lock_buckle`, `dense_weave`, `core_burst`). Stricter high-support filter `supportDepth>=3 + A/B + maxChoices<=8` keeps only `dense_weave`.
- Filtered clean-hit pack retained for single-level demo/reference: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardProductionV1Pack.asset`, GUID `afdb809ddc1a4502910d678912899a75`, containing `sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave.asset`.
- Demo scene `Assets/ArrowMagic/Scenes/Demo.unity` and `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` currently point to the 4-level `SGPPressureHardTrialPack` for review.

## SGP Sandwich Tail Safe 0859 Review Pack - 2026-06-26

- Lab Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SandwichTailSafe0859ReviewPack.asset`
- Pack GUID: `73eb729f7ca4413cb0a1a3b1b8d20c7d`
- Level: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/SandwichTailSafe0859Review/tail0857B_alt_ray1_single_c42.asset`
- Level GUID: `415ba263e0054f3fe88215e819ea4b6e`
- Source trace: `.worktrees/sgp-sandwich-refill/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/tail0857B_alt_ray1_single_unique_trace_metrics.csv`
- Contents: single sandwich/tail-safe boundary candidate, coverage `0.8596154`, `solved=True`, `processTier=B`, `supportClosureBestDepth=4`, `hardStructureV3Class=LocalEasy`. Manual review rejected it as worse than the normal production samples; keep this pack only as a negative boundary/reference for why supportDepth alone does not prove high difficulty.

## SGP Sandwich Owner-Hit Grammar Final 0900 - 2026-06-26

- Final selected CSV: `.worktrees/sgp-sandwich-refill/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ownerhit0900_sandwich_final_selected.csv`
- Final level asset: `.worktrees/sgp-sandwich-refill/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/OwnerHitGrammarFrom0898B/tail_single/ownerhit0898_tail_single_c63.asset`
- Final candidate CSV: `.worktrees/sgp-sandwich-refill/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ownerhit0898_tail_single_unique_candidates.csv`
- Final official trace metrics: `.worktrees/sgp-sandwich-refill/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ownerhit0898_tail_single_unique_trace_metrics.csv`
- Verification result: coverage `0.9000000`, `solved=True`, `processTier=B`, `supportClosureBestDepth=4`, `avgChoices=4.39`, `choiceP80=7`, `maxChoices=10`, `outerExitHeadCount=10`, but `hardStructureV3Class=LocalEasy` / `hardStructureV3Score=0.071`; keep as high-coverage support-preserved boundary, not as a proven high-difficulty candidate.
- Route summary: strict sandwich/refill parent -> no-new-head tail-safe fill to ~0.86 -> support-safe owner-hit grammar fill to ~0.898 -> tail single-cell finish to exactly `0.9`.
- Review pack mounted in `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity`: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SandwichOwnerHit0900ReviewPack.asset`, GUID `3c8ae3e32cd54fa6b83ff2d7f8f09000`.

## Geometry Supply Owner-Hit Probe root154 + lock_buckle - 2026-06-26

- Summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_probe_summary.md`
- Summary CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_probe_summary.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_probe_selected.csv`
- Candidate/trace batches:
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_v1_candidates.csv`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_v1_trace_metrics.csv`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_r2_candidates.csv`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_r2_trace_metrics.csv`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_r3_candidates.csv`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_r3_trace_metrics.csv`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_r4_len10_candidates.csv`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_r4_len10_trace_metrics.csv`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_r5_len10_candidates.csv`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_r5_len10_trace_metrics.csv`
- Candidate levels root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeometrySupplyOwnerHitV1/`
- Base root: `orig_seed_usable_v1_01_rolegraph_next5_arrowz_level_154`, coverage `0.291498`, official trace `S/TrueHardCandidate/supportDepth4`.
- Supply level: `Assets/ArrowMagic/SOData/Levels/DirectProcedural/SGPPressureHardTrial/sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle.asset`; used center-crop `20x28 -> 19x26` and same-chain edge constraint.
- Current result: five traced rounds; best strict coverage reached `0.3623482` with `solved=True`, `processTier=S`, `hardStructureV3Class=TrueHardCandidate`, `supportClosureBestDepth=4`. Not mounted in Demo; this is a low-coverage high-difficulty growth proof.

## Geometry Supply Owner-Hit Multi-Root Probe - 2026-06-26

- Summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_multi_root_probe_summary.md`
- Summary CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_multi_root_probe_summary.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_multi_root_probe_selected.csv`
- Candidate levels root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeometrySupplyOwnerHitV1/`
- Tested roots/supplies:
  - `root05 + core_burst`: reached `coverage=0.6419355`, `solved=True`, `processTier=S`, `hardStructureV3Class=MediumStructure`, `supportClosureBestDepth=4`; high-coverage nonLocal control, not hard enough.
  - `root10 + dense_weave + bundle3`: current strongest hard lane; strict TrueHard/support4 candidates reached `0.4072464`, `0.4434783`, `0.4753623`, and `0.5000000`. Highest row `0.5028986` downgraded to `HardPotential/support4`.
  - `root98 + dense_weave`: `0.3002451`, 24/24 `TrueHardCandidate/support4`, but low throughput.
  - `root76 + dense_weave`: strict `TrueHard/support4` candidate at `0.3345588`, lower pass density than root10.
- Demo status: not mounted. Use this as high-difficulty research evidence and scheduler input only.

## Mask Line Inventory Baseline - 2026-06-26

- Current HoleMask assets: `Assets/ArrowMagic/SOData/Levels/Production/HoleMask/Candidates` (68), `Assets/ArrowMagic/SOData/Levels/Production/HoleMask/Early` (2), `Assets/ArrowMagic/SOData/Levels/Production/HoleMask/Early30To40` (1).
- Historical manifest: `Assets/ArrowMagic/SOData/Reports/LevelImportV1/hole_mask_early_front_manifest.csv` has 70 rows, playableFill `0.602-0.779`, boardFill `0.451-0.721`, chains `22-97`, and references missing pack `Assets/ArrowMagic/SOData/Packs/Production/HoleMask/HoleMask_FinalScreening_EarlyFront.asset`.
- Runnable/reference packs currently present: `Assets/ArrowMagic/SOData/Packs/HoleV13Top5DemoPack.asset`, `Assets/ArrowMagic/SOData/Packs/Production/HoleProcedural/HoleProceduralCandidatePack.asset`, `Assets/ArrowMagic/SOData/Packs/Production/HoleProcedural/HoleProceduralPreviewTop50Pack.asset`, `Assets/ArrowMagic/SOData/Packs/ShapeExperiment/ShapeIconMaskOnlyBatch11CandidatePack.asset`, `Assets/ArrowMagic/SOData/Packs/ShapeExperiment/ShapeIconMaskOnlyBatch12CandidatePack.asset`.
- Partial SeedMask production output: `Assets/ArrowMagic/Reports/Production/HoleLongOuterStrong/HoleLongOuterStrong_Production_Report.txt` records one accepted `22x34_long` candidate in `Assets/ArrowMagic/SOData/Levels/Production/HoleLongOuterStrong/Candidates`; no synced pack was found under `Assets/ArrowMagic/SOData/Packs/Production/HoleLongOuterStrong/`.
- Baseline pack should be rebuilt non-destructively from existing HoleMask assets, then checked with `CampaignSingleLevelValidator` and official trace before any Demo attachment or larger SeedMask generation.
