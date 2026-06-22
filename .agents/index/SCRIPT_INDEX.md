# Script Index

路径以仓库根目录为基准。先按用途找入口，再读取具体脚本和相邻实现。

## Runtime

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `Assets/ArrowMagic/Scripts/Board/` | 棋盘、tile、链条和玩法核心 | 修改棋盘行为、消除逻辑、坐标/网格规则 |
| `Assets/ArrowMagic/Scripts/Generation/ArrowLevelGeneratorAdapter.cs` | package DTO 与项目资产之间的生成适配 | 接入 `com.pixelbug.arrow-level-generator` 或转换关卡数据 |
| `Assets/ArrowMagic/Scripts/IO/LevelIO.cs` | 关卡 IO 基础入口 | 读写关卡数据、排查序列化 |
| `Assets/ArrowMagic/Scripts/IO/LevelLoader.cs` | 关卡加载 | 场景加载关卡、包引用、启动关卡 |
| `Assets/ArrowMagic/Scripts/IO/LevelProgression.cs` | 关卡进度 | 关卡顺序、解锁、进度保存相关任务 |
| `Assets/ArrowMagic/Scripts/IO/LevelSaveData.cs` | 存档数据结构 | 修改存档字段、迁移进度数据 |
| `Assets/ArrowMagic/Scripts/UI/ArrowMagicHudUIITK.cs` | UI Toolkit HUD 控制 | 修改游戏 HUD、按钮、状态显示 |
| `Assets/ArrowMagic/Scripts/UI/UIToolkitTileRenderer.cs` | UI Toolkit tile 渲染 | 修改 tile 外观、棋盘视觉映射 |
| `Assets/ArrowMagic/Scripts/ScriptableObjects/` | LevelDefinition、LevelPack 等 SO 类型 | 改关卡/包数据结构时先看 |

## Portable Package

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `Packages/com.pixelbug.arrow-level-generator/Runtime/Model/` | 中立关卡 DTO | 跨项目生成格式、导入导出适配 |
| `Packages/com.pixelbug.arrow-level-generator/Runtime/Validation/` | 几何验证和贪心逃逸 solver | 判定关卡是否合法、可解 |
| `Packages/com.pixelbug.arrow-level-generator/Runtime/Scoring/` | 覆盖、外圈、开局、短边、直线等质量评分 | 调整质量门槛和候选排序 |
| `Packages/com.pixelbug.arrow-level-generator/Runtime/Planning/` | campaign 规划 | 组包、难度分布、family 限制 |
| `Packages/com.pixelbug.arrow-level-generator/Runtime/Generation/` | 可移植直接矩形生成器 | 生成非 Unity 资产的候选数据 |

## Editor Tools

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `Assets/ArrowMagic/Editor/AuthoredLevelEditorWindow.cs` | 手工关卡编辑窗口 | 调整手工编辑流程 |
| `Assets/ArrowMagic/Editor/GeneratedLevelPreviewWindow.cs` | 生成关卡预览窗口 | 调整预览、检查候选 |
| `Assets/ArrowMagic/Editor/LevelDefinitionEditor.cs` | LevelDefinition inspector/editor | 修改关卡 asset 编辑体验 |
| `Assets/ArrowMagic/Editor/LevelExporter.cs` | 关卡导出 | 生成交付文件、导出格式排查 |
| `Assets/ArrowMagic/Editor/LevelImportDifficultyAnalyzer.cs` | 导入难度分析 | 导入关卡、检查难度和报告 |
| `Assets/ArrowMagic/Editor/LevelImportCampaignOrderBuilder.cs` | 导入 campaign 顺序构建 | 组装导入关卡顺序和包 |
| `Assets/ArrowMagic/Editor/CampaignSingleLevelValidator.cs` | 单关卡验证 | 候选池验收、批量验证 |
| `Assets/ArrowMagic/Editor/CampaignSingleLevelCandidatePoolTools.cs` | 单关卡候选池工具 | 候选池整理、筛选、导出 |
| `Assets/ArrowMagic/Editor/Campaign500OptimizationRoundRunner.cs` | Campaign500 优化轮次 runner | Campaign500 批次优化和报告 |
| `Assets/ArrowMagic/Editor/CampaignHoleProceduralGenerator.cs` | Hole procedural 生成 | Hole 类关卡候选生成 |
| `Assets/ArrowMagic/Editor/CampaignHoleHighChainTrialRunner.cs` | 高 chain hole 试跑 | Hole 高链数实验/验证 |
| `Assets/ArrowMagic/Editor/NoMaskProceduralGenerator.cs` | 无 mask procedural 生成 | 无掩码候选、风格预览 |
| `Assets/ArrowMagic/Editor/ArchitecturalLineworkPreviewBuilder.cs` | Architectural linework 预览 | 建筑线稿形态预览/候选 |
| `Assets/ArrowMagic/Editor/DirectArchitecturePreviewAutoRunner.cs` | 直接建筑预览自动 runner | 自动生成 architecture preview |
| `Assets/ArrowMagic/Editor/CompositeSeedVariantBatchBuilder.cs` | 复合 seed 变体批处理 | R1/R2 seed 变体、候选批量构建 |
| `Assets/ArrowMagic/Editor/MaskPreviewPackBuilder_Experiment.cs` | mask preview 实验包构建 | 掩码预览、实验产物；收尾时重点区分正式/实验 |
| `Assets/ArrowMagic/Editor/SeedMaskPatchWindow.cs` | seed/mask patch 大型编辑器工具 | 修补 seed/mask、批量 patch、历史实验排查 |
| `Assets/ArrowMagic/Editor/ShapeExperimentMaskBuilder.cs` | shape 实验 mask 构建 | shape icon/mask 批次和报告 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmLab.ps1` | SGP 节奏静态筛选实验 | 审核候选开局数量、平均选择数、静态依赖指标和替换建议 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` | SGP 真实过程曲线 trace | 需要判断整局每步可消数量、选择峰值、伪深度/贴脸连续解锁、外圈直链连续消除和 S/A/B/Drop 过程分层 |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` | Building Grammar trace + interleave probe | 当前分支新增 `-InterleaveFlipProbe`，用于对已有候选做离线翻链 probe，验证 `crossUnlockRatio/spineAlternationRunMax/singleSpineDominanceRate` 是否能改善；不写新关卡 asset |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` | Building Grammar CPS/interleave StageLock 实验 | 当前分支支持 `-OutputPrefix`、`-SourceSortMode CrossPotential`、`-InterleavedLockBias`、`-MinCrossPotentialScore`、`-CrossPotentialOnly`、`interleaveSolveHead/availableTraceHead` 调试字段和 interleave-aware dependency merge guard；`InterleaveFlipPasses` 默认 0，pair-flip/`-EnableInterleaveDominanceTrim` 仅显式实验；新增 `-UltraChoiceEvalLimit` 防止坏源在 ultra prune 中拖到外部超时，`-AllowTightCurveStageException` 仅用于紧选择曲线但 stage 分层略弱的显式 probe；`-EnableCurveRootStraightPrune`、`-EnableTightRootPairSearch`、`-EnableFollowRunStraightCut`、`-EnableSoftInterleaveDominanceTrim` 都是显式 probe 开关，目前不能作为默认量产参数；soft trim 已加 stage/curve/interleave floors，只能视为安全修复通道，不代表已能 strict 量产；2026-06-21 新增 scheduled trace/weak sidecar diagnostics 和失衡 cross flip 拒绝，结论是后验 flip 可造伪 cross 但不能稳定突破 dual-spine；v175 证明 `-StageGateSearch -UltraLowChoiceBias` 能把 h231 类高结构源压成低选择 near-pass；v176 进一步证明 top8 h231-like 源经该组合后可通过 stage-preserving pair repair 达到 `followRun=2/stage>=0.82`，下一步应自动化该 lane 而非继续 sidecar/trim |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Run-CPSInterleaveSourceBatch.ps1` | Building Grammar CPS/interleave 分源 runner | 每个父源独立隐藏 PowerShell 子进程，支持 per-source timeout、连续 timeout 早停、`BatchSourceSortMode`、`UltraChoiceEvalLimit`、`InterleaveFlipPasses/Candidates`、`AllowTightCurveStageException`、`EnableCurveRootStraightPrune`、`EnableTightRootPairSearch`、`EnableFollowRunStraightCut`、`EnableSoftInterleaveDominanceTrim` 和 `EnableWeakInterleaveSidecar`；用于稳定扫源，不直接代表质量达标；CPS feed 读取源资产时建议 `SourceRoot=F:\Unityproject\ArrowLevel-Hand`、`OutputRoot=.worktrees/sgp-building-grammar` |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-CPSInterleaveProductionFeed.ps1` | Building Grammar CPS 生产 feed | 从 CPS probe CSV 生成下一轮源 feed，可读取 batch summary glob，排除 timeout/empty 失败源，按 productionScore/risk 筛中型源；注意 `knownGenerated` 只表示能产，不表示质量过关 |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-CPSInterleaveRetryFeed.ps1` | Building Grammar CPS near-pass retry feed | 从 strict rejected CSV 中抽取只因 `avgChoices/followRun` 轻微失败且 coverage/cross/dominance/balance 接近过线的 source，用于小范围 `UltraChoiceEvalLimit=0` 深挖；当前全量历史只稳定找到 dense_weave 一个 strict retry 源 |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-CPSInterleaveSourceRiskCache.ps1` | Building Grammar CPS source 风险缓存 | 聚合 CPS top100、分源 summary、merged candidates 和 pressure-read rejected，按 source 输出 `strict/candidate_near/preflight_near/generated_far/empty_far/timeout_risk/untried` 分类，并生成 chain-range-limited next feed；2026-06-21 起 next feed 会排除 repeated empty/timeout/spent source，用于避免按 CPS 分数盲跑或反复烧已证伪 near 源 |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-StageLockSourceEnhancedVariants.ps1` | Building Grammar StageDoor 源增强实验 | 当前分支从 rhythm-lab 源增强脚本派生，支持 `StageDoorProfile=DualBalanced` 与 `DualCarrier` 实验；`DualCarrier` 会输出 `dualCarrierBridgeCount/dualCarrierMergeCounts` 并禁用普通 fallback，用于诊断双 carrier/跨侧 bridge 是否真实形成。当前结论：能造出 `cross≈0.048` 的早期信号，但仍会在 choice curve 与真实双线之间拉扯，不能作为 dual-spine 量产突破 |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-DependencyAnchorSourceVariants.ps1` | Building Grammar dependency anchor 源注入 | 从自然高覆盖 SGP/StageLock 成品出发，解析 `LevelDefinition` 并选择少量 target/blocker 逃逸射线依赖候选，只翻转 target 链来注入 dependency anchor；支持 `-UseFinalPath` 直接锚定最终 curated body，也支持 `-EnableInterruptorSidecar` 做 source-side sidecar 实验。v163-v166 结论：final-body h91 可到 relaxed pass 且 strict 仅差 `followRun=3`，但单纯提高 `anchorCrossCount` 会打开选择曲线；v172-v173 结论：source-side free sidecar 会提高选择数/破坏 realized cross，不作为主线；下一步应找 h231-like 可低选择门锁化源，并把 anti-follow 约束前移 |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-DependencyFollowRunReport.ps1` | Building Grammar realized follow-run 诊断与 pair/triple/interruptor repair probe | 从最终 `LevelDefinition` 重算 greedy dependency follow-run，输出 max-run segment、逐步替代链、single/pair/triple flip probe；支持 `-ExportBestPairFlipRepair` / `-ExportAllPairFlipRepairs` / `-ExportAllTripleFlipRepairs` 导出 repair 资产；v171 新增 `-EnableInterruptorMergeProbe` / `-ExportAllInterruptorMergeRepairs` 和 `interruptorAltCount/CloseAlt/BestDeficit` 诊断；v197 修复 repair 导出长路径问题，并新增 `-ExportAllSingleFlipRepairs`、`-RepairAllowNoFollowGain`、`-RepairMinOpeners`、`-IncludeAllFlipCandidates`，用于 two-stage repair 中把 cross-preserving 冷候选拉回 `openers>=4` |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Measure-FollowRunRepairability.ps1` | Building Grammar follow-run repairability 画像 | 合并 candidate、follow-run segments、repair candidates 和 repair trace，输出 `repairabilityClass`、max-run label pattern、是否含 `O`、repair 是否保 stage；v182-v184 用于区分 `strict_pair_repairable` 的 hcd58-like homogeneous max-run 与 `mixed_fragile/repair_breaks_stage` 负例 |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-StageAwareFollowRunPairRepair.ps1` | Building Grammar stage-aware follow repair 流水线 | 串联 follow-run repair export 与 full trace，再按 `tightProcessTier/stageLockScore/lateRegionCount/choice/cross/dominance/balance` 选修复结果；支持 `-EnableInterruptorMergeRepairs`。v169-v171 批量验证 depanchor v160-v166 仍为 `0 selected`，说明当前翻链修复会过早打开 late gate，而后验 interruptor merge 在 dense final body 中缺少几何空间 |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Run-DepAnchorLowChoiceStrictSlice.ps1` | Building Grammar depanchor strict 自动化切片 | 自动串联 h231-like 源筛、final-body dependency anchor、cross-potential probe、`StageGateSearch + UltraLowChoiceBias`、follow-run pair repair、full trace、strict select 和可选 freeze pack；v177-v180 验证 runner 可复现 v176 strict；v185 起支持 `-EnableRepairabilityGate`，按 homogeneous max-run 画像过滤 repair trace；v188 起支持 `-IncludeRawPassingCandidates` 与 `StrictMinOpeners/StrictMinCrossUnlockRatio/StrictMinSpineBalance/StrictMaxSingleSpineDominanceRate/StrictMaxDependencyFollowRate`，避免漏掉原始已过线候选并显式控制开局压力/依赖强度；v191 起透传 `InterleaveFlipPasses/InterleaveFlipCandidates`，用于显式打开 StageLock 内部跨线翻向 probe；v197 起 `InputTraceCsv` 支持 alias `TraceFeedPath`；v198 起支持 `-EnableTwoStageRepairProbe`、`StrictMaxOpeners`、外圈 solve/straight 限制、远依赖质量 gate，用于压低开局选择、减少外圈可见扫边和筛出更硬依赖样本 |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Select-CPSInterleaveCandidates.ps1` | Building Grammar CPS/interleave 硬筛 | 合并候选 CSV，按低选择、follow run、near unlock、cross unlock、dominance、balance 和 coverage 硬筛；会按 source + 指标签名去重，避免同父源重复产物被误算为产能 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PressureReadOrientationVariants.ps1` | PressureRead 头尾方向重组实验 | 保留 SGP 形状，通过可解依赖图、远依赖和结构化直链指标生成 demo 候选 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` | PressureRead 阶段门锁实验 | 在 orientation 基础上加入阶段计划、区域延迟打开指标、root 合链、多轮依赖子链合并和 `CachedPotential` 源缓存排序；支持 `-LongChainBias` 用于在过程曲线不变差时强化复杂长链结构；支持 `-MinOutputChains` 阻止中型链组实验被合链压回小关；支持 `-StageGateSearch` 枚举入口候选/根链组合，用于中链复杂长链实验中搜索强门锁型或紧曲线型难关；`-AllowEntryRootMerge + -LongChainBias` 会放大 root merge 上限，仅用于显式实验 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` / `Build-SGPRhythmTrace.ps1` | Trace Bridge Proof V1 | 2026-06-22 新增 planned upstream order bias、fixed orientation 重套、`-AllowWeakStageForTraceBridgeProbe` proof-only 开关和 planned trace bridge replay 字段；用于验证 ray-collision bridge 是否在冻结 asset 的 trace 中成立 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-ReferenceSeedStructureProfile.ps1` | 外部 seed 结构画像 | 分析 298 seed 池的链长、转弯、结构承载链和巨链风险，用于校准 SGP 难关结构指标 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPSourceStructureProfile.ps1` | SGP 源结构画像 | 扫描 `sgp_rhythm_all_candidates.csv` 中的源 asset，输出 `longChainRate/structureCarrierRate/complexChainScore` 和 `gatePotentialScore/gateLateRegionCount` 等排序前结构/门锁潜力指标 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-StageLockTargetedSourceFeed.ps1` | StageLock targeted 源筛 | 综合源画像、过程压力、gate potential、family 先验和尝试缓存，输出更适合 DepAware/StageLock 的高命中源 CSV；支持 `Balanced`、`HighYield`、`ReferenceLong`、`ReferenceComplex`、`HardXL` preset，其中 `ReferenceComplex` 用于复杂长链补充 lane，`HardXL` 只作为大源诊断入口 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-HardMidWideMicroSourceFeed.ps1` | HardMidWide 微切片源筛 | 从 `sgp_source_structure_stage_lock_hardmid_wide.csv` 按链条规模、长链率、结构承载率、gate potential、直链风险和已试 feed 排除生成可复跑 small feed；用于 `Run-HardMidWideDoorBalancedStageGateMicroSlice.ps1` 前置选源 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-StageLockSymmetrySourceVariants.ps1` | StageLock 高产源几何扩展 | 对已验证高产源生成 `FlipX`、`FlipY`、`Rot180` 几何变体 feed，用于在不改核心生成器的情况下扩充可门锁化源 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-StageLockSourceEnhancedVariants.ps1` | StageLock 源增强实验 | 在 StageLock 前生成中长链比例更高的源候选；支持 `AdjacentMild`、`DependencyAware` 和 `StageDoor`，其中 `StageDoor` 会先选入口区/延迟区并做合法连续性检查；增强源文件名使用短 hash 避免 Windows 长路径失败 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Select-StageLockHardCandidates.ps1` | StageLock trace 后精选 | 合并候选 CSV 与真实 trace，按过程曲线、stage lock、结构链、base/family 上限和 `MaxPerStructureSignature` 粗结构签名限制输出可冻结硬关候选；支持 `MaxTraceMaxChoices`、`MinTraceStageLockScore`、`MinChains`、`MinAvgChain`、`MinLongChainRate`、`MinStructureCarrierRate` 等复杂长链筛选阈值；已识别 StageDoor 短 hash 与 `_section_h`、`_lock_h` family 短名 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Select-StageLockSalvageCandidates.ps1` | StageLock 历史候选补漏 | 从历史 candidates CSV 中排除当前池已覆盖的 source hash，再按低选择、stage lock、长链、结构承载和直链风险阈值捞出可重新 trace/精选的漏网候选；适合 ReferenceComplex/长链小切片回收，不直接替代真实 trace 和 hard select |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Merge-StageLockSelectedCandidates.ps1` | StageLock 多批精选合并 | 合并多个 selected hard CSV，按 base/family 去重并重新 rank，输出可冻结的多切片候选池；会重新归一化已有 `selectionBaseKey` 以避免不同 run label 的同源重复 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Run-StageLockTargetedDepAwareSlice.ps1` | StageLock targeted DepAware 单切片流水线 | 封装源增强、StageLock、trace、hard select、freeze 的单切片流程，用于重复扩池；支持透传 `-LongChainBias`、`-AllowEntryRootMerge`、`-RandomAttemptsPerLevel`、`-BootstrapRandomAttempts` |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Run-StageLockSymmetryExpansionSlice.ps1` | StageLock symmetry 扩产流水线 | 封装高产源几何扩展 + targeted DepAware 单切片验证，用于批量测试 HighYield/ReferenceLong 或成功 enhanced source feed 的 symmetry 扩池效果；支持随机救援参数透传 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Run-StageLockStageDoorSymmetrySlice.ps1` | StageLock StageDoor symmetry 扩池流水线 | 封装 symmetry source -> StageDoor source -> 中间源 trace -> StageLock -> final trace -> hard select -> freeze；适合 minority/highchain success feed、ReferenceComplex 小切片和 HardMid 中型链组 probe；支持 `StageDoorVariantsPerSource`、`StageDoorProfile`、`MinOutputChains`、`MinChains`、`StageGateSearch` 与复杂长链精选阈值透传 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Run-HardMidWideDoorBalancedStageGateMicroSlice.ps1` | HardMidWide DoorBalanced 微切片 | 专门跑 `hardmid_wide` 中型长链小切片：symmetry -> DoorBalanced StageDoor source -> source trace -> chunked StageLock -> trace/select；默认不用 `GateStrong`，用于稳定扩 36+ 链 hard candidates；支持可选 `-UseChunkPrefilter`，输出 `stage_door_source_<slice>_chunk_scores.csv` 并按 source trace、StageDoor 源信号和父源 history cache 跳过低价值 chunk |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Freeze-StageLockCandidatePack.ps1` | StageLock 候选冻结 | 把当前可覆盖实验候选复制到独立目录并生成稳定 pack，支持自定义冻结关卡前缀，避免后续试跑覆盖 `.meta` 导致 demo pack 漂移 |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-ArchitecturalLineworkSGP.ps1` | ArchitecturalLinework 反解式线稿构造实验 | 已被人工反馈判定偏离目标：纯手摆/constructive 产物不是后续主线；仅作为失败实验参考，不再用于展示或默认继续 |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Render-LevelContactSheet.ps1` | LevelDefinition contact sheet 渲染 | 内部视觉筛选用；把候选 CSV 渲染成 PNG，辅助检查建筑线稿、外出口直链、空洞和机械感，不作为最终生产逻辑 |

## Tests

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `Assets/ArrowMagic/Editor/Tests/AuthoredLevelBuilderTests.cs` | 手工关卡构建测试 | 修改 Authoring/关卡构建 |
| `Assets/ArrowMagic/Editor/Tests/BoardGenerationTuningTests.cs` | Board/generation 调参测试 | 修改生成、棋盘约束 |
| `Assets/ArrowMagic/Editor/Tests/TileView2DTests.cs` | Tile 视图测试 | 修改 tile 渲染/坐标映射 |

| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-HardMidWideStageDoorTraceCache.ps1` | HardMidWide StageDoor trace-aware 父源缓存 | 聚合 hardmid_wide DoorBalanced 微切片历史到原始父源层，记录 hardSelected、candidate、weak stage lock、no orientation 等反馈，用于避免重复跑低价值父源 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-HardMidWideTraceAwareMicroSourceFeed.ps1` | HardMidWide trace-aware 源筛 | 在 micro feed 基础上结合 StageDoor trace cache 排除已证伪/已出货父源，生成下一轮小切片 feed |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-ProductiveRefitStageDoorSourceFeed.ps1` | Productive Refit StageDoor 源筛 | 扫描历史 StageDoor source CSV，排除已入池 source hash，按中长链、结构承载、直线风险和 family cap 生成可二次强合链的 productive refit feed |

| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-ProductiveRefitStageDoorSourceFeed.ps1` | Productive Refit StageDoor 源筛 | 已支持 `-PreferOrientableHistory`，用于按可定向历史、源选择曲线和中长链结构筛出更适合二次强合链的 StageDoor 源 |

| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-ProductiveRefitStageDoorSourceFeed.ps1` | Productive Refit StageDoor 源筛 | 支持 `-PreferOrientableHistory` 与 `-UseStageLockRiskCache`，后者会把历史 StageLock rejected/selected 精确反馈到 StageDoor 源排序，减少重复尝试不可定向源 |

| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-ProductiveRefitStageDoorSourceFeed.ps1` | Productive Refit StageDoor 源筛 | `-UseStageLockRiskCache` 已升级为 source-hash 级别风险缓存，跨 v2/v3/v4 和 run label 共享同源 `no stage-lock orientation`/`weak stage lock` 历史；默认排除 `smoke` 文件，避免小测污染正式 feed 与 risk/selected history cache |

| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` | Hard Lane V2.1 trace 指标 | 已输出窗口化/事件化节奏指标：`choiceChangeRate`、`unlockPower`、`postSpikeConvergence`、`boringLinearScore`、`structuredHardnessV21`；用于区分顺消平铺、结构解锁和低选择硬关 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Select-StageLockHardCandidates.ps1` | Hard Lane V2.1 精选 | 已支持 `-MaxBoringLinearScore`、`-MinStructuredHardnessV21`，作为 rhythm gate/score boost；不能替代 `longChainRate`、`structureCarrierRate` 等链条结构门槛 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-NearMissRescueSourceFeed.ps1` | Near-miss rescue 源筛 | 扫描历史 selected-hard rejected 记录，反查候选的 `sourceLevelId/sourcePath`，输出可直接重跑的 StageDoor source feed；用于预算化单源救援前的 source-level prefilter |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Run-HardMidWideDoorBalancedStageGateMicroSlice.ps1` | HMW + V2.1 量产切片 | 已透传 `-MaxBoringLinearScore`、`-MinStructuredHardnessV21`，以及 NearMiss carrier absorb 预算参数；推荐与 `-AggressiveLongChainMerge`、`-AllowVeryLongPressureGate` 组合试跑 HMW/VeryLong 小切片 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` | Near-miss 二次合链实验 | 支持 `-NearMissChainRescue`，默认关闭；仅在 `-AggressiveLongChainMerge` 下启用 high rhythm near-miss 的二次合链，包含端点吸收、插入式吸收、预算回退、搜索上限和显式预算参数：`CarrierAbsorbComboLimit`、`CarrierAbsorbCarrierSeenLimit`、`CarrierAbsorbAbsorbSeenLimit`、`NearMissCarrierPassLimit`、`NearMissCarrierMaxMergeLimit`；V2.4 增加 `-OuterShellPressureGate`，用于识别/惩罚/合链吸收简单外圈壳链；V2.5 增加生成侧动态外圈可见压力 gate、post-merge 重新 orientation、早期外圈头惩罚，用于降低普通外圈同时可消和扫边风险 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SkeletonConstraintAdapterV1.ps1` | Skeleton Gate V1 源生成 | Skeleton Graph 思路的最小 adapter：生成内缩结构骨架长链 + 短折链填缝源，作为 StageDoor/StageLock 前置结构先验；直接进 StageLock 低产，当前有效路径是 Skeleton -> StageDoor GateStrong -> StageLock |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SkeletonConstraintAdapterV1.ps1` | Skeleton Gate V1 高覆盖模式 | 支持 `-TargetCoverage`、`-DenseOuterGuardFill`、`-StrongDoorBridges`；用于 DenseDep 小批，把边缘空白转成会拐入内部的 guard 链，并增加 door/bridge 链提升覆盖率和依赖潜力 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SkeletonConstraintAdapterV1.ps1` | RoomDoor Skeleton V2 源生成 | 支持 `-RoomDoorSkeletonV2`；先生成 room/hub/main spine/door bridge/room-local lane 的语义骨架，再接 StageDoor GateStrong 和 StageLock；用于验证建筑语法方向，当前只适合小批 review，不是量产入口 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SkeletonConstraintAdapterV1.ps1` | Dependency Skeleton V3 源生成 | 支持 `-DependencySkeletonV3`；先摆跨区 dependency blocker/target 链，让目标链头部逃逸射线有机会撞到 blocker 链，再接 StageDoor GateStrong + StageLock。当前小样证明依赖强度优于单纯 RoomDoor 视觉骨架；后续应扩 layout 并补 realized-dependency 报告 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` | Skeleton Gate V1 architectural outer shell | 支持 `-AllowArchitecturalOuterShell`，仅在动态外圈/外出口压力干净时允许少量建筑式外壳链通过，避免 Skeleton Gate 样本被静态 `outer simple shell too dominant` 误杀；应与 `-OuterShellPressureGate` 一起用于 Skeleton -> StageDoor -> StageLock 小批验证 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Select-TrueHardDependencyCandidates.ps1` | True Hard Dependency 精选 | 合并候选 CSV 与 trace CSV，按真实依赖强度、低选择曲线、stage lock、外圈压力、静态外出口头、链条结构和 metric signature 去重筛选；用于从自然高覆盖 SGP/StageDoor/StageLock 主体里挑真正难关，避免回到手摆骨架图 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Repair-OuterExitHeadChains.ps1` | 外圈出口头修复 probe | 对外圈头部直接朝外的链做头尾互换组合实验，输出候选后必须重新跑 trace/Greedy；V14 结论是盲翻会破可解，只能作为诊断或后续生成侧先验参考 |

| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` | Hard Lane V2.2 trace 指标 | 在 V2.1 基础上新增 `choicePeakCount/Rate/Excess`、`outerNearFollow*`、`sameSideOuterFollow*`、`outerNearDependency*`、`sameSideOuterDependency*`，用于压制 8+ 选择峰值和外圈近距离/同边顺消 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Select-StageLockHardCandidates.ps1` | Hard Lane V2.2 精选 | 已支持 `-MaxTraceChoiceP80`、`-MaxTraceChoicePeakCount/Rate/Excess`、`-MaxOuterNearFollowRate/RunMax`、`-MaxSameSideOuterFollowRunMax`、`-MaxOuterNearDependencyRate`、`-MaxSameSideOuterDependencyRunMax`，并将这些指标纳入 selection score 惩罚 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Run-HardMidWideDoorBalancedStageGateMicroSlice.ps1` | HMW + V2.2 量产切片 | 已透传 V2.2 choice peak 与 outer follow/dependency 参数；后续小批可直接用相同 gate 跑 NearMissRescue review 候选 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` | Hard Lane V2.3/V14 外出口压力指标 | 新增 `headOuterExit`、`outerAvailableChoice*`、`outerExitAvailableChoice*`、`sameSideOuterAvailableChoiceMax`、`sameSideOuterExitAvailableChoiceMax`、`outerExitSolveRunMax` 等动态指标；V14 追加 `outerExitHeadCount/Rate/SideMax` 静态外出口头指标，解决肉眼看到外圈朝外头太多但动态 gate 漏判的问题 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Select-StageLockHardCandidates.ps1` | Hard Lane V2.3 外出口精选 | 已支持 V2.3 外圈/外出口可见压力 gate 和 score penalty，如 `-MaxOuterAvailableChoiceMax`、`-MaxSameSideOuterExitAvailableChoiceMax`、`-MaxOuterExitSolveRunMax` |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` | Hard Lane DifficultyScoreV1 难度判别 | 在 Greedy solve trace 上追加浅层反事实和依赖顺消指标：`counterfactualBranchDivergenceAvg`、`counterfactualGreedyRegretAvg`、`counterfactualLocalOnlyStepRate`、`dependencyFollowRate`、`dependencyFollowRunMax`、`difficultyScoreV1`；用于判断“外圈干净但顺着连消”的真实难度问题，并指导后续生成/合链验收 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Select-TrueHardDependencyCandidates.ps1` | True Hard + DifficultyScoreV1 精选 | 已支持 `-MinDifficultyScoreV1`、`-MaxDependencyFollowRunMax`、`-MaxDependencyFollowRate`、`-MaxCounterfactualLocalOnlyStepRate`；后续 hard lane 合链应把 `dependencyFollowRunMax<=2` 作为 acceptance floor，避免合出 3+ parent-child 顺消段 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Run-NonFollowOuterRepairSlice.ps1` | Non-follow hard 外圈修复流水线 | 从 DifficultyScoreV1 trace 中筛 `dependencyFollowRunMax<=2` 但外圈脏的 hard 候选，做 bounded outer-exit-head subset repair，再重新 trace 和精选；V15 证明能稳定产出 S/S、`depRun=2`、外圈压力下降的小批 review 候选 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` | Local Patch Burst 难度校准 | 2026-06-21 新增 `localPatchSolveRunMax`、`nearOuterPatchSolveRunMax`、`localWindow5NeighborMax`、`nearOuterWindow5NeighborMax`、`localUnlockBurstMax`，用于识别 `dependencyFollowRunMax` 已压住但同一区域仍一串扫光的体感低难问题 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` | DependencyBraid 依赖交错诊断 | 2026-06-21 新增 `dependencyBraidScore`、跨区率、换方向率、远依赖率、依赖距离 P20、同侧外圈依赖率、同侧同向外圈 run 等只读指标；用于验证 SGP-native 关卡是否仍是外圈/局部传送带，并作为后续 StageLock edge selection 的评分输入 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` | HardStructure V3 因果解锁判别器 | 2026-06-21 新增 `hardStructureV3Score/rawScore/class`、`causalCudP20`、`causalAntiLocalityScore`、`causalCrossRegionCriticalLockCount`、`causalSolveDelayAvg`、`directionalSolveRunMax` 等字段；用于区分“raw 远依赖潜力”与“实际仍本地顺消”的假难关 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` | StageLock 生成侧 local burst gate | 2026-06-21 将 local patch 指标前移到 `Evaluate-Orientation`，并在 `NonFollowHardGate` 下支持 `MaxGeneratedLocalPatchSolveRunMax`、`MaxGeneratedNearOuterPatchSolveRunMax`、`MaxGeneratedLocalWindow5NeighborMax`、`MaxGeneratedNearOuterWindow5NeighborMax`；用于避免方向选择/合链制造局部传送带 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` | StageLock DependencyBraid edge bias | 2026-06-21 新增 opt-in `-DependencyBraidBias`：生成侧输出污染修正后的 `dependencyBraidScore`、`dependencyBraidBadLocalRate`、同侧外圈依赖等字段，并在候选依赖边上增加 `braidEdgeScore`，用于奖励跨区/远距离/换方向依赖、惩罚 local patch 与同侧外圈依赖；验证结论是后验 StageLock 能识别/偏置坏源，但真正突破还需把 low-pollution dependency potential 前移到 source feed/source generation |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-DependencyBraidSourceFeed.ps1` | DependencyBraid 源 feed 筛选 | 2026-06-21 新增：从 trace metrics 中按 `dependencyBraidScore`、`dependencyBraidBadLocalRate`、同侧外圈依赖、依赖距离 P20、local/near-outer patch run 和 family cap 生成 StageLock source feed；用于避免继续盲跑旧 hard pool |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-DependencyBraidSourceFeed.ps1` | Outer-clean DependencyBraid 源 feed | 2026-06-21 追加 `MaxOuterExitHeadCount/MaxOuterExitHeadSideMax/MaxOuterAvailableChoiceMax`，并把 outer-head/outer-available 写入 feed 和 summary；用于避免把外圈天然脏的 body 送进 dependency-anchor 或 StageLock |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-DependencyBraidSourceFeed.ps1` | HardStructure V3 源 feed gate | 2026-06-21 追加可选 `MinHardStructureV3Score`、`MinCausalAntiLocalityScore`、`MinCausalCudP20`、`MinCausalCrossRegionCriticalLockCount`；默认不影响旧流程，用于后续只送入 anti-local 和跨区关键锁更强的源 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-OuterCleanDependencyAnchorSourceVariants.ps1` | Outer-clean dependency anchor 源生成 | 2026-06-21 新增：从 SGP/StageLock 既有 body 中抽远距离 blocker-target 锚点，但在 source 层拒绝 target 头朝外、base/emitted 外出口头超阈值、外出口头回归；验证 `v3_wide160` 产出 15/160 可解、2/160 strict，是当前 SGP-native 真难关突破方向 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` | Anchor-preserving StageLock probe | 2026-06-21 新增 `-MaxGeneratedDependencyBraidBadLocalRate` 与 `-PreserveAnchorTargetOrientation`；后者读取 `anchorPlan` 的 target orientation 并跳过 post-merge，验证结果是 naive anchor 源仍会在外圈/可解性/局部污染之间冲突，下一步应改 source generation |

## Recent Building Grammar Notes

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` | Building Grammar hard outer pressure frontgate | 2026-06-21 已在 `Evaluate-Orientation` 里新增普通外圈 solve 指标 `outerTouchSolveRatio/outerTouchRunMax/sameOuterSideSolveRunMax`，并新增 `OpeningPressureMaxOuterTouchSolveRatio`、`OpeningPressureMaxSameOuterSideSolveRunMax` 生成侧评分旋钮；用于避免只压外圈长直链而漏掉普通外圈连扫 |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Run-DepAnchorLowChoiceStrictSlice.ps1` | Building Grammar hard pressure runner | 2026-06-21 已透传 `OpeningPressureMaxOuterTouchSolveRatio`、`OpeningPressureMaxSameOuterSideSolveRunMax`；后续 hard-pressure 小切片可直接把普通外圈触边解决比例和同侧外圈连消作为 StageLock 搜索目标，而不只作为最终筛选 |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` | Building Grammar d215 topology admission | 2026-06-21 新增 opt-in `-TopologyAdmissionGate`、`TopologyMaxEarlyOuterAvailableChoiceMax`、`TopologyMaxEarlySameOuterSideSolveRunMax`、`TopologyMaxSameOuterSideSolveRunMax`；用于把 early outer 可见选择和同侧外圈连消从软评分提升为 StageLock 输出前硬准入，并在 UltraChoice prune 中尝试温和 topology relief |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Run-DepAnchorLowChoiceStrictSlice.ps1` | Building Grammar d215 strict/rebound runner | 2026-06-21 新增 strict early-outer gate、透传 `TopologyAdmissionGate`，并支持 `-EnableRepairReboundGuard`；repair 只有在不降低 stage/late 且不反弹 early outer/same-side outer 指标时才可入选 strict |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-DependencyAnchorSourceVariants.ps1` | Building Grammar outer-root dependency anchor | 2026-06-21 新增显式 `-OuterRootAnchorBias`，用于优先选择外圈 target chain 并让其被内侧/跨侧 blocker 锁住，导出 `anchorOuterRootTargetCount/anchorInnerBlockerCount/anchorSameSideOuterCount`；d214 证明能强化开局压力 near-miss，但仍需 StageLock 内避免同侧外圈连消与 repair 后 early outer 反弹 |
| `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` | Building Grammar early outer pressure trace | 2026-06-21 新增前 25% solve window 指标：`earlyOuterTouchSolveRatio`、`earlySameOuterSideSolveRunMax`、`earlyOuterAvailableChoiceAvg/Max`、`earlySameOuterAvailableChoiceMax`，用于识别“全局外圈指标合格但开局外圈仍白给”的问题 |

## Recent SGP Rhythm Lab Support Closure Notes

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` | Support Graph Closure V1 trace 诊断 | 2026-06-22 新增 trace-side bounded support closure 指标：`supportClosureBestScore/RawScore/Depth/NodeCount/ConveyorRate/RegionEntropy/BranchMax/AvgDistance/AvgDelay` 等；分数带 depth gate，一跳 support 只作为 raw diagnostic，避免把 conveyor 误判为真实多跳支撑结构 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-OuterCleanDependencyAnchorSourceVariants.ps1` | Support Graph Closure source anchor gate | 2026-06-22 新增 opt-in `-SupportGraphClosureAnchor` 及 `SupportClosureMinDepth/Score/RegionEntropy/MaxConveyorRate/MinBranchMax`；用于在 source anchor 选择阶段要求 support->hub->target 静态闭包。验证结论：可造强静态闭包源，但必须经 trace/causal closure gate 再验收 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` | StageLock SupportClosureBias | 2026-06-22 新增 opt-in `-SupportClosureBias` 与 `MinGeneratedSupportClosure*` gate；已从 planned dependency closure 纠偏为 causal closure gate：在模拟 solve 中记录 `parentOf` availability transition，只让真实执行因果图里的 support closure 过关。追加 `supportClosureFailureClass/supportClosureFailurePenalty` 与 `-PreserveAnchorSupportOrientation`，用于诊断 upstream/support bridge 是否在 trace 中退化成 conveyor |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SupportBridgeRayProbe.ps1` | Support bridge ray 物理语义 probe | 2026-06-22 新增；按 `A -> B` = `B.escapeRay hits A.geometry` 的唯一语义扫描/导出 `upstream -> support -> hub -> target` 物理桥。可输出诊断 CSV，也可用 `-EmitSources` 导出翻 support、existing-ray、多跳 existing 或 endpoint-split source 资产。验证结论：`FlipBlockedRay:support` 静态桥多但易打断可解；`ExistingRay/ExistingMultiHopRay` 可出现 trace-visible depth-2 closure，但仍偏 LocalEasy；`InsertSidecarAsUpstreamBlocker` 在 dense body 中无空位，`SplitEndpointSidecar` 会破可解。下一步应前移到 source-level bridge-slot generation，不能把后验 sidecar 当主线 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Invoke-StageLockBoardTraceGate.ps1` | Board-level trace final gate | 2026-06-22 新增；批量调用 `Build-SGPRhythmTrace.ps1` 后按真实 replay 结果筛选 hard-lane 候选。`Evaluate-Orientation` 只作为 prefilter；最终验收在此脚本中检查可解、process tier、choice peak/wave、local patch、dependency follow、outer exit、support closure 和 planned bridge replay |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-ChoicePeakPruneVariants.ps1` | Realclosure choice peak prune 诊断 | 2026-06-22 新增；从单个输入候选复制 asset，移除指定 burst chain 并 remap bridge ids，用于验证中盘 unlock fanout 是否为 choice peak 根因。当前只能作为 proof/diagnostic，不能当最终量产生成器 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-ChoicePeakStaggerVariants.ps1` | Realclosure target flip stagger 诊断 | 2026-06-22 新增；对 burst target 链做子集头尾反转，验证 target-side 物理错峰。当前实验 `14/23/24/26/28` 的 25 个组合全部不可解，说明直接翻 target 会破坏 trace 因果顺序，暂不作为主线 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-ChoicePeakRelaySplitVariants.ps1` | Realclosure relay split fanout 修复 | 2026-06-22 新增并扩展；将高 fanout parent chain 切成两段并尝试段方向组合，在不删除覆盖的情况下物理错峰释放。支持 `-ProcessAllRows/-InputRowLimit/-InputRowOffset` 多源扫。当前 chain `11` 单源 40 变体中 4 个通过严格 board gate；8 个 orientation 源已小批验证，s01/s02 各 4/40 过线，并冻结出 Auto V1 5 关包 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Freeze-StageLockCandidatePack.ps1` | StageLock 候选冻结 | 2026-06-22 已补 bridge manifest 保留：冻结 CSV 会保留并回填 `traceAnchorBridgeTarget/Hub/Support/Upstream` 与 planned replay 字段，确保冻结包二次 board trace 时仍可验证 planned bridge replay |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-DesignedHardLockV0.ps1` | Designed hard-lock 物理依赖 proof | 2026-06-22 新增；按 `Propagate-Exits` 真实逃逸传播手工构造 4 个镜像 physical lock 原型，用于证明 `TrueHardCandidate` 可以由 trace-visible `upstream/support/hub/target` 结构产生。不是正式 SGP 量产工具，下一步应抽象为 source slot/constraint |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-HardLockSlotSGPFillV0.ps1` | Hard-lock slot + SGP-style filler 实验 | 2026-06-22 新增；先放 trace-proven physical hard-lock slot，再在非 reserved corridor 区域做程序化 bent-chain top-up。已修正 head 语义为 `indices[0]`，`TargetCoverage=0.20` 小批 12 个中 7 个 `solved=True + TrueHardCandidate`，是当前 hard-lock 接回 SGP 的最小有效 proof |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-HardLockSlotSourceOverlayV0.ps1` | Hard-lock slot source overlay 实验 | 2026-06-22 新增；把 hard-lock slot overlay 到既有高覆盖 SGP source body，并删除冲突链。当前判断 post overlay 不是主线；保留作对照/诊断。已修正 corridor reserve 的 head 方向为 `indices[0]` |


