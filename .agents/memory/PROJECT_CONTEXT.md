# Project Context

## Identity

- 项目名：`ArrowLevel-Hand`
- 当前标准项目路径：`F:\Unityproject\ArrowLevel-Hand`
- Unity 版本：`2022.3.62f1`
- 核心内容：`Assets/ArrowMagic` 下的 ArrowMagic 箭头消除/关卡生成相关 Unity 工程。
- 本地可移植生成包：`Packages/com.pixelbug.arrow-level-generator`

## Repository Shape

- `Assets/ArrowMagic/Scenes/`：Unity 场景入口，包括 `Demo.unity`、`Create.unity`、`Debug.unity`、`IntroCycler.unity`。
- `Assets/ArrowMagic/Scripts/`：运行时代码，按 Authoring、Board、Generation、IO、ScriptableObjects、UI 等拆分。
- `Assets/ArrowMagic/Editor/`：编辑器窗口、批处理生成器、导入器、候选池、验证器和实验工具。
- `Assets/ArrowMagic/SOData/`：ScriptableObject 数据，包括 Levels、Packs、Reports、Palettes、Sfx/Vfx libraries。
- `Assets/ArrowMagic/Masks/`：生产、实验和批量生成的掩码资源。
- `Assets/ArrowMagic/UI Toolkit/`：UI Toolkit 的 UXML/USS。
- `Packages/com.pixelbug.arrow-level-generator/`：可移植关卡 DTO、验证、评分、规划和直接矩形生成逻辑。

## Production Lanes

- `PSG` / `Pressure-SGP`：正式普通量产线。含义是先用 SGP 做高覆盖填充，再用 pressure/trace 筛选普通难度可玩关卡；不要把它混同为 high/root-hard 产线。
- PSG V1 核心提交：`aa1564bd Add PSG pressure hard production lane`。
- PSG V1 优先入口：`Tools/Production/Invoke-SGPPressureHardProductionV1.ps1`，Unity 方法为 `NoMaskProceduralGenerator.BuildSgpPressureHardTrialPack`。
- PSG V1 扩池生产入口：`Tools/Production/Invoke-SGPPressureProductionBatchV1.ps1`，默认聚合 Trial/Review6/Interference6/InterferenceV2Six，经 STS trace-order keep 后自动写 `SGPPressureHardProductionKeepPack.asset`。
- PSG 当前风格语言先落在 `Join-SGPPressureTraceMetrics.ps1` 的只读 tagging 层：输出 `styleFamily`、`chainLanguage`、`flowLanguage` 和 `riskTags`，用于人工审查和后续 pack diversity；尚未改 PSG 生成 core。
- PSG diversity cap 也在 selection 层：默认关闭，开启后按 style/flow/chain/signature/high-risk 上限选 production keep；它是组包控制，不是生成器约束。
- PSG 5-lane 稳定输出口为 `Tools/Production/Export-PSGStyleLaneKeepsV1.ps1`；从 joined CSV 或 source+trace 导出 `patchwork_lock/core_burst/dense_weave/flow_spread/staged_unlock` 五个 lane keep CSV，不改 canonical keep、不重打 pack。它是只读后筛/风格对比导出脚本，不是 PSG 生成入口，也不是按风格生产候选。
- PSG seed-derived style 学习入口为 `Tools/Production/Build-ProjectSeedStyleProfileV1.ps1`；默认只看初始 seed 池 `Assets/ArrowMagic/SOData/Levels/Seeds`，用 fast static 风格画像，避免误扫 generated/composite 候选卡住。当前 V3 画像把链形态、拓扑和主链集中度分开看；PSG 15 keep 更接近 `seed_fragmented_lock_like`，不是原始 seed 的 `long_maze/long_lock` 主链型。该入口只输出 profile/cluster/PSG match 报告，不改生成 core。
- `Nutation`：对标 PSG 的新生成 family，不再只表示“弯链”。Nutation 下面按 solve topology 分 `Flow / Peel / LongChain / Hub / Maze` 等 style，再按 chain language 分 `curve_chain / rail_chain / hook_chain / spine_chain / patch_chain / mixed_chain`。
- 当前 Nutation 工作区：`.worktrees/nutation-peel`，分支 `codex/nutation-peel`。所有 Nutation family 的 peel/flow/chain-language 试验优先放这里，主项目只接收稳定 review pack 或最终候选。
- Nutation Hub/Maze mixed-chain 隔离工作区：`.worktrees/nutation-hub-maze-mixed`，分支 `codex/nutation-hub-maze-mixed`。用于在 `.worktrees/nutation-peel` 被占用时继续 Hub/Maze mixed 试验；当前 Hub mixed 有 2 个 TraceOrderKeep prototype，Maze mixed 仍非 production-ready。
- 当前 Nutation lanes：`NutationFlowCurveV1` / `NutationFlowRailV1` / `NutationFlowPatchV1` 分别是 `Nutation / Flow / curve_chain|rail_chain|patch_chain` baseline/review lanes；`NutationPeelV2` 在分类上作为 `Nutation / Peel / curve_chain` 基线，也可称 `NutationPeelCurveV2`；`NutationPeelRailV1` 是 `Nutation / Peel / rail_chain`；`NutationPeelPatchV1` 是 `Nutation / Peel / patch_chain` strict near-miss/prototype；`NutationLongChainSpineV1` 是 `Nutation / LongChain / spine_chain`；`NutationLongChainCurveV1` 是 `Nutation / LongChain / curve_chain`；`NutationLongChainRailV1` 是 `Nutation / LongChain / rail_chain`；`NutationLongChainPatchV1` 是 `Nutation / LongChain / patch_chain` strict-review lane；`NutationHubSpokeV1/V2/V3/V4/V5Pool/V5LocalBreak` 都是 `Nutation / Hub / patch_chain` proof/probe lane；`NutationHubCurveV1` 是 `Nutation / Hub / curve_chain` style-proof/prototype；`NutationHubRailV1` 是 `Nutation / Hub / rail_chain` style-proof/prototype；`NutationMazePatchV1/V2` 是 `Nutation / Maze / patch_chain` proof/probe lane；`NutationMazeCurveV1` 是 `Nutation / Maze / curve_chain` low-yield style-proof/prototype；`NutationMazeRailV1` 是 `Nutation / Maze / rail_chain` folded-rail style-proof/prototype。Hub/Maze 目前都不是量产绿灯。统一审查出口为 `.worktrees/nutation-peel/Tools/Production/Export-NutationStyleMatrixV1.ps1`；代表样本包出口为 `.worktrees/nutation-peel/Tools/Production/Invoke-NutationStyleMatrixRepresentativeReviewV1.ps1`。
- 最新 Nutation 状态（2026-06-29）：`NutationMazeCurveV1` smoke1 补齐 `Maze / curve_chain` proof，但低产，4 specs 仅 1 traceable row；该 row 1/1 official solved，`styleFamily=constraint_maze`、`chainLanguage=curve_chain`、`chainLanguageDetail=maze_curve_chain`，rank 为 VisualKeep，0 production keep，source coverage `0.892`、avg/max choices `4.95/10`、STS `0.690` / collapse `0.356`，anti-collapse score `77.836`，主要 gap 为 same-axis、STS/collapse 边界和 local-collapse。当前 style matrix 为 14 lanes / 53 rows，strict keep 仍只来自 PeelCurve、PeelRail、LongChainSpine；Flow review-only，PeelPatch strict-near-miss，Hub/Maze 系列只作 style proof 或 anti-collapse review。
- 最新 Nutation 状态追加（2026-06-29）：`NutationLongChainCurveV1` 补齐 `LongChain / curve_chain`，入口 `.worktrees/nutation-peel/Tools/Production/Invoke-NutationLongChainCurveProductionV1.ps1`，Unity method `NoMaskProceduralGenerator.BuildNutationLongChainCurveV1Pack`。smoke1 第二版 4/4 solved、4/4 STS pass、3 TraceOrderKeep + 1 Reject，source straightness `0.343-0.389`、avgChain `11.55-12.56`、maxChain `21-24`，成功避开过度蛇形和 rail 化。当前 style matrix 为 15 lanes / 57 rows，strict keep 9 行：LongChainCurve 3、LongChainSpine 2、PeelCurve 2、PeelRail 2。
- 最新 Nutation 状态追加（2026-06-29）：`NutationLongChainRailV1` 补齐 `LongChain / rail_chain`，入口 `.worktrees/nutation-peel/Tools/Production/Invoke-NutationLongChainRailProductionV1.ps1`，Unity method `NoMaskProceduralGenerator.BuildNutationLongChainRailV1Pack`。smoke2 为 4/4 official solved、4/4 visualPass、3 TraceOrderKeep + 1 VisualKeep，production keep 3；source straightness `0.408-0.490`、avgChain `12.45-13.43`、maxChain `22-23`，比 LongChainCurve 更直但已压住 stripe 风险。当时 style matrix 为 16 lanes / 61 rows，后续已由 FlowRail/FlowPatch checkpoint 更新到 18 lanes / 69 rows，并由 LongChainPatch checkpoint 更新到 19 lanes / 73 rows。
- 最新 Nutation 状态追加（2026-06-29）：`NutationFlowRailV1` 和 `NutationFlowPatchV1` 补齐 Flow style 的 rail/patch chain-language review lanes。入口分别为 `.worktrees/nutation-peel/Tools/Production/Invoke-NutationFlowRailProductionV1.ps1` 和 `.worktrees/nutation-peel/Tools/Production/Invoke-NutationFlowPatchProductionV1.ps1`，二者复用 `Invoke-NutationFlowCurveProductionV1.ps1 -Lane Rail|Patch`。FlowRail smoke1 为 4/4 solved、4/4 `flow_continuous` + `rail_chain`/`flow_rail_chain`、4 TraceOrderKeep；FlowPatch smoke1 轻调后为 4/4 solved、4/4 `patch_chain`/`flow_patch_chain`、2 TraceOrderKeep + 1 VisualKeep + 1 Reject。该 checkpoint 的 style matrix 为 18 lanes / 69 rows，Flow 三语言均为 `flow_review_ready`；后续已由 LongChainPatch 更新到 19 lanes / 73 rows，strict keep 仍只来自 LongChain/Peel，不包含 Flow。
- 最新 Nutation 状态追加（2026-06-29）：`NutationLongChainPatchV1` 补齐 `LongChain / patch_chain`，入口 `.worktrees/nutation-peel/Tools/Production/Invoke-NutationLongChainPatchProductionV1.ps1`，Unity method `NoMaskProceduralGenerator.BuildNutationLongChainPatchV1Pack`。smoke1 为 4/4 official solved、3 TraceOrderKeep + 1 Reject、production keep 3；source straightness `0.288-0.316`、avgChain `9.66-11.48`、maxChain `21-22`，是“长链主干 + patch support”，不同于 FlowPatch 的连续流。当前 style matrix 为 19 lanes / 73 rows，strict keep 15 行：LongChainCurve 3、LongChainPatch 3、LongChainRail 3、LongChainSpine 2、PeelCurve 2、PeelRail 2。
- `Flow` 在当前体系中定位为连续传播、低 strict 的 baseline/noise primitive，天然容易连消；FlowCurve/FlowRail/FlowPatch 都只作为 review/noise/language 对照，不作为普通 strict 量产入口。
- 最新 Nutation 状态追加（2026-06-29）：`NutationHubSpokeV5LocalBreak` 是 Hub V5.1 的 `Hub / patch_chain` review/prototype lane，入口 `.worktrees/nutation-peel/Tools/Production/Invoke-NutationHubSpokeProductionV5LocalBreak.ps1`，Unity method `NoMaskProceduralGenerator.BuildNutationHubSpokeV5LocalBreakPack`。smoke4 为 `16/16` solved、`6` processKeep、`1` VisualKeep、`1` TraceOrderKeep；VisualKeep 和 TraceOrderKeep 尚未重合，因此 Hub V5 仍不是 strict production。full pack `NutationHubSpokeV5LocalBreakPack.asset` 已生成（GUID `fc62c0876adad0a42b083d855ca6226a`），但未强制覆盖当前已打开/dirty 的 Demo activePack。
- 最新 Nutation 状态追加（2026-06-29）：`Export-NutationHubV5HybridReviewV1.ps1` 合并 Hub V5 Pool + V5 LocalBreak 做只读 hybrid rerank；默认输出 `nutation_hub_v5_hybrid_review_v1_*`。当前 `40` rows / `0` HybridStrict / `8` near rows，证明 Hub V5 的 local-visual 和 trace-order 优点仍未在同一候选重合，下一步需要 generation-side multi-objective scheduling，而不是把 near-miss 当 production keep。
- 最新 Nutation 生产分档（2026-06-29）：当前 matrix 已纳入 Hub V5 Pool，为 `20` joined CSV / `97` rows / `16` style-chain representatives。生产-ready review lanes 是 `LongChain curve/rail/patch/spine` 和 `Peel curve/rail`；`Flow curve/rail/patch` 可作为 `10-20%` controlled mix；`Hub/Maze/PeelPatch` 继续优化。代表包为 `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationStyleMatrixRepresentativeReviewPack.asset`，GUID `2d8a4da775c84f85bc9575d170512392`。
- Nutation 生产准备调整（2026-06-29）：用户人工看过 `NutationProductionPreviewV1Pack` 后认为 LongChain 体感暂不适合量产；下一条生产准备分支改为 `.worktrees/nutation-flow-peel-production` / `codex/nutation-flow-peel-production`。该分支有两个出口：Flow/Peel dedicated wrapper 和 Mixed quality-only global wrapper。Mixed 不是 Flow/Peel 比例组包，也不限制 style/chain language；默认可聚合 Flow/Peel/LongChain/Hub/Maze 等 Nutation sources，只由 solved、coverage、choices、local run、directional risk、重复度等质量门槛决定。
- Campaign500 Normal Full V1（2026-06-30）：在 `.worktrees/nutation-flow-peel-production` 完成 normal-only 全量生产。source 为 200 normal slots x 3 variants = 600 rows，official trace 600/600 solved，ReviewPack 200 levels，ProductionKeepPack 86 levels，ProductionStrictKeepPack 70 levels。LongChainProbe 已排除并由 PeelHard replacement 覆盖原 slot；后续生产/审查优先从 `Assets/ArrowMagic/SOData/Reports/Campaign500/NormalFullV1/` 和 `Assets/ArrowMagic/SOData/Packs/Campaign500/` 查 full_v1 产物。
- Campaign500 Normal Strict152（2026-06-30）：在 `.worktrees/nutation-flow-peel-production` 用严格 `TraceOrderKeep` 补齐 normal replacement target，最终为 `152/152` unique normal slots。`campaign500_normal_full_v1_review.csv`、`campaign500_normal_full_v1_production_keep.csv`、`campaign500_normal_full_v1_production_strict_keep.csv` 均为 152 行且全是 TraceOrderKeep；对应 `Campaign500NormalFullV1ReviewPack.asset`、`Campaign500NormalFullV1ProductionKeepPack.asset`、`Campaign500NormalFullV1ProductionStrictKeepPack.asset` 均为 152 levels。主项目 Demo 已挂 ReviewPack。

## Long-Term Working Agreements

- 资源查找顺序：先看 `.agents/index/`，再做针对性搜索。
- 索引只做导航：路径 + 用途 + 什么时候该看它，不复制大段文件内容。
- Unity 资源改动要注意 `.meta` 配套文件；不要无意触发大面积资源重导入。
- 不擅自回退或清理已有工作树改动，尤其是未确认来源的关卡、掩码、报告和导出产物。
- 生成、导入、验证相关工作完成后，要把正式结果和实验产物分离。

## Arrow Chain Rules

- 棋盘格类型只有 `Empty / Arrow / Block`；可导入 seed 至少要有 1 条箭头链，每条链至少 2 格，所有 index 必须在 `0 <= index < width * height`，链内/链间不能重复占格，链内相邻格必须上下左右相邻。
- 链条连接只认相邻箭头。A 指向 B 时，B 必须在 A 的 `outDir` 相邻一格，且 B 的 `inDir` 为 `Opposite(A.outDir)`；空格不会连接链体。
- Authored seed 的 `indices` 是 `head -> ... -> tail`；构建运行时棋盘时会反写为 tail->head 方向。闭环 authored 链通常不接受。
- authored 构建会在写完链后断开跨链外部 predecessor：如果两条不同 authored 链因相邻方向被误连，`AuthoredLevelBuilder` 会改被指向格子的 `inDir` 以保持链分离。因此生成器必须做 round-trip/实际链审计，不能只看写出的 `indices`。
- portable validator 还会拒绝 head escape ray 打回本链的 authored 形状；WBP 的 loop-risk/self-ray 风险应作为正式候选的硬门槛或强 warning。
- 点击传播与链体连接是两个概念。点击某箭头后信号从该格 `inDir` 进入，按 `outDir` 传播；默认 `SignalTravelMode.ThroughEmpty` 下信号可以穿过空格向外逃逸，但遇到另一个箭头会停止，不会跨空格接链。
- 有效点击要求传播最终出棋盘；有效后清除该箭头所在的整条相邻链。撞 Block、方向不匹配、循环或不能出棋盘则不清除。胜利条件是棋盘无 Arrow。
- 运行时清除的是点击格所在的实际相邻链，不是 authored JSON 中某个链 id 的抽象集合；因此 authored `indices`、构建后的 tile 方向、实际 `ArrowChainUtility` 链集合需要能互相解释。
- `blocker` 在 WBP 中通常是语义角色，表示某条箭头链阻挡/延迟另一条链的逃逸射线；不要默认把它等同于 `TileType.Block`，除非明确计划写入 `blockIndices`。
- 正式 pack 入口除 greedy 可清空外，还要求至少有箭头格、至少有初始可消链、难度分不为 0。
- 用户提供的 `SeedLevelImporter.cs` / `SeedJsonFormat.md` 路径来自相邻 `F:\Unityproject\ArrowCopy` 工程引用；在当前 `ArrowLevel-Hand` 工作树中未作为同路径源码验证，后续若要改 importer/seed pack 入口需重新定位本仓库对应文件。
- `ClearAllArrowsGenerator` 是基础造形器：按 allowed mask 和 coverage 随机造能朝边界逃逸的链，再由 greedy/score 过滤；它不是 Generated-Root WBP 的默认继承路线，只能作为规则、validator 和 coverage 机械能力参考。
- Generated-Root WBP 必须同时维护两张图：链体连接图只认相邻箭头，信号/ray 图可穿空格但遇箭头停止。cell plan 中的 intentional empty / corridor / guard / choke 只能影响信号与 first-hit，不得被当成 chain body 连接。
- WBP candidate 在进入正式验收前需要同时通过 authored legality、round-trip/实际链审计、head self-ray/loop-risk 检查、Greedy solved、official trace 与 relation/difficulty audit；coverage 或 root hard score 单独不能作为成功证据。

## Path Migration Notes

- 当前标准项目路径：`F:\Unityproject\ArrowLevel-Hand`
- 旧路径：未确认。
- 状态：已确认当前工作目录；旧路径信息待后续明确后再记录。

## Nutation Hub V5 Hybrid Status

- `NutationHubSpokeV5Hybrid` 是 Hub V5 的 V5.2 generation-side convergence lane，目标是把 Pool 的 trace-order 优点和 LocalBreak 的 local-visual 优点合到同一候选，不替换原始 V5/Pool/LocalBreak。
- 当前保留版本是 smoke4 权重：在 smoke2 基础上增加成熟阶段外圈 head 分散惩罚和轻量 trace-near 排序惩罚；smoke3/smoke5/smoke6 都是负例。
- 当前最佳 review pack 为 `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubSpokeV5HybridProductionKeepPack.asset`，GUID `a2be47b7e6cfd6b488ae343b81e997b0`，1 行 TraceOrderKeep，仍非 strict production；当前 strict gap 为 `localPatchRun+1`。
- `NutationHubSpokeV5HybridSearch` 是 2026-06-30 的 seed-band 扩池诊断负例：`24/24` solved 但 `0` production keep，四路 rerank 后 smoke4 仍最佳。不要把 Search 当当前 baseline；下一步应做 final-candidate solve-order scheduler/proxy calibration。
