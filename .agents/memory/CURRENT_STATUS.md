# Current Status

## Snapshot - 2026-06-18

- 已建立项目记忆骨架：`AGENTS.md`、`.agents/memory/`、`.agents/index/`、`.agents/skills/project-memory/SKILL.md`。
- 当前标准项目路径：`F:\Unityproject\ArrowLevel-Hand`
- 当前 Unity 版本：`2022.3.62f1`
- 工作树在建立记忆前已经存在大量修改和未跟踪资源，集中在 `Assets/ArrowMagic`、`Packages`、`Exports`、`.codex-run`、`TempContactSheets` 等位置。
- 状态：既有工作树改动来源未逐项确认；不要因为收尾清理规则擅自删除或回退这些既有改动。

## Active Continuation Points

- 新对话如果用户说“按项目记忆继续”，先读 `AGENTS.md` 和 `.agents/skills/project-memory/SKILL.md`。
- 关卡、掩码、包、报告相关任务先读 `.agents/index/LEVEL_INDEX.md`。
- 编辑器工具、批处理、导入/验证相关任务先读 `.agents/index/SCRIPT_INDEX.md`。
- 本次仅新增项目记忆文档；未整理、清理或验证既有 Unity 资源产物。

## Context Compression Checkpoint Rule - 2026-06-22

- 已采用：对话压缩摘要不能作为可靠长期项目记忆；后续还要依赖的信息必须写回项目 md。
- 阶段完成、长时间实验、上下文可能压缩、切换 worktree/分支、合并/清理/交接前，先写 5-10 行 checkpoint。
- checkpoint 优先写入 `CURRENT_STATUS.md`；原则/取舍写入 `DECISIONS.md`；未归类的重要信息写入 `CONVERSATION_MEMORY.md`；新资源入口写入 `.agents/index/`。
- checkpoint 只记录结论、路径、commit/branch/worktree、验证结果、风险和下一步，不复制大段日志。

## Rosetta GPT Advisor Protocol - 2026-06-22

- 已采用：需要方案顾问、架构审稿或实验路线分歧时，可通过 Rosetta 咨询 GPT。
- GPT 只作为 second opinion；项目侧最终判断仍以用户目标、仓库事实、项目记忆和实测数据为准。
- 若 GPT 方案不被认可，不直接实现；补充目标、证据、失败分布、约束和验收指标继续追问，直到形成可验证共识或记录未达成一致。
- 达成共识后才落项目规范：原则/取舍写入 `DECISIONS.md`，流程写入 `WORKFLOW.md`，新入口写入 index，当前执行状态写入 `CURRENT_STATUS.md`。

## SGP Native Hard Lane Correction - 2026-06-21

- 人工明确纠偏：难关突破必须继续基于 SGP/StageDoor/StageLock 主线，不再把 Skeleton-first/手摆式骨架作为最终生产路线；Skeleton/DependencySkeleton 只保留为诊断参照。
- 已确认 `V22` SGP-native review5 在新指标下仍有局部顺消问题：原 5 关 retrace 均可解，但 `localWindow5NeighborMax=5`；pair/triple/interruptor repair 后 14/14 可解，但全部 `ProcessTier=B`，`localWindow5NeighborMax=5`、`nearOuterWindow5NeighborMax=5`，不能作为最终突破包。
- 已冻结 SGP-native 对照包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLaneV3SGPNativeLocalPatchReview3Pack.asset`，Demo 已指向该包。该包用于人工确认 SGP-native 体感，不是最终量产包。
- 冻结 trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hardlane_v3_sgp_native_localpatch_review3_frozen_metrics.csv`。3/3 可解；第 1 关为当前最干净 SGP-native 样本，`processTier=A`、`openers=4`、`avgChoices=3.92`、`maxChoices=7`、`difficultyScoreV1=0.733`、`dependencyFollowRunMax=2`、`localPatchSolveRunMax=2`、`nearOuterPatchSolveRunMax=2`、`outerExitHeadCount=1`、`outerExitSolveRunMax=1`。第 2/3 关仍为对照，`dependencyFollowRunMax=4`、`localPatchSolveRunMax=4`。
- 关键负结果：用 `NonFollowHardGate + OuterShellPressureGate` 直接重跑 `v22_target`、`v22_diversity` StageDoor 源，0 个新候选。拒绝集中在 `dependency follow too linear` 和 `local patch burst too linear`。历史 `pressure_read_stage_lock_source_attempt_cache.csv` 中同时低 `depRun/localPatch` 的 generated 记录只有 skeleton 诊断样本，说明现有 SGP-native 历史池天然过线样本极少。
- 当前技术判断：`localPatchSolveRunMax` 更贴近“玩家在一个小区域连续扫”的体感，应作为硬门槛；`localWindow5NeighborMax` 对 SGP-native 过敏，适合作为降权/对照，不宜单独硬杀。下一步应在 SGP/StageDoor 源阶段主动制造跨区交错打开或 dependency anchor，而不是继续盲扫历史池或回到手摆骨架。

## SGP Native DependencyBraid Trace Validation - 2026-06-21

- 已在 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` 增加只读诊断指标，不改变现有 tier 规则和生成器：
  - `dependencyBraidScore`
  - `dependencyRegionTransitionRate`
  - `dependencyDirectionChangeRate`
  - `dependencyFarRate`
  - `dependencyDistanceAvg/P20/Min`
  - `dependencySameSideOuterRate`
  - `dependencySameSideSameDirOuterRate`
  - `sameSideSameDirOuterDependencyRunMax`
  - `traceRegionTransitionRate`
  - `traceDirectionChangeRate`
  - `traceOuterSameSideSameDirRunMax`
  - `dependencyBraidBadLocalRate`
- 验证结果：`HardLaneV3SGPNativeLocalPatchReview3` 第 1 关 `dependencyBraidScore=0.770`、`dependencyRegionTransitionRate=0.857`、`dependencyFarRate=0.929`、`dependencyDistanceP20=6`、`localPatchSolveRunMax=2`、`outerExitSolveRunMax=1`；第 2/3 关 `dependencyBraidScore=0.545/0.518`、`dependencyFollowRunMax=4`、`localPatchSolveRunMax=4`、`outerExitSolveRunMax=2`。
- 对 `V22 review5` 全量重跑后，新指标稳定把第 1 关排最高，其余 B 级样本因 `localPatchRun=4-7`、`nearOuterRun=4-5`、`dependencyDistanceP20≈4.46`、`dependencyFarRate≈0.5` 被压低。说明 `DependencyBraid` 指标能解释人工反馈中的“依赖方向/位置太近、外圈仍容易连续消”的问题。
- 仍需注意：第 1 关虽然 braid 指标最高，但 `dependencySameSideOuterRate=0.286`，说明外圈同侧依赖仍未完全清零。下一步应把 DependencyBraid 接入 StageLock edge selection：奖励跨区/远距离/换方向依赖，惩罚同侧外圈、同方向、local patch 依赖，而不是继续只靠后验筛选。

## SGP Native DependencyBraid StageLock V1 - 2026-06-21

- 已在 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` 增加 opt-in `-DependencyBraidBias`，将 DependencyBraid 接入 StageLock 生成侧：
  - `Evaluate-Orientation` 输出 `dependencyBraidScore`、跨区率、远依赖率、换方向率、依赖距离 P20、同侧外圈依赖率、同侧同向外圈 run、`dependencyBraidBadLocalRate`。
  - `Get-DependencyOptions` 新增每条候选依赖边的 `braidEdgeScore`，奖励跨区/远距离/换方向，惩罚 local patch、同侧外圈、同侧同向外圈。
  - `Find-BestStageLockOrientation` 在 `-DependencyBraidBias` 下用 braid 指标做选向 tie-breaker/软惩罚；默认不开启，不影响旧产线。
- 已修正 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` 的 `dependencyBraidScore`：现在为正向结构分减去 local/same-side outer 污染分，避免“表面跨区但体感顺消”的样本拿高分。
- 验证结果：
  - `hardlane_v3_sgp_native_localpatch_review3_braidtrace_v2_metrics.csv` 中人工较优第 1 关 `dependencyBraidScore=0.671`，两个 B 样本降到 `0.452/0.425`，与人工反馈一致。
  - `dependency_braid_v20_sample10_v2_metrics.csv` 显示 V20 小样平均 `dependencyBraidScore=0.499`、`dependencyBraidBadLocalRate=0.854`、`dependencySameSideOuterRate=0.474`，说明旧硬关池仍有大量局部/同侧外圈依赖污染。
  - 对 `stage_door_source_nearmiss_rescue_v22_target_feed.csv` 小批 generation smoke 后 `0` 产；拒绝集中在 `outer simple shell too dominant` 和 `weak stage lock`。两个 section 源在新分数下仅 `dependencyBraidScore=0.39` 且 `dependencyBraidBadLocalRate=0.773`，说明新评分能识别坏源，但不能在 StageLock 后验阶段凭空制造复杂依赖。
- 当前结论：GPT 的 edge-level dependency selection 思路可行，但必须继续前移到 source feed/source generation；下一步不要继续同一批 v22 源盲跑，应优先找/造低 `dependencyBraidBadLocalRate`、低同侧外圈依赖、高 `dependencyDistanceP20` 的 SGP/StageDoor 源。
- 详细报告：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_stage_lock_v1_notes.md`。

## SGP Native DependencyBraid Source/Anchor Probe - 2026-06-21

- 已修复 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` 的绝对路径输入问题：trace CSV 的 `path` 若为 rooted path，不再错误拼接 `SourceRoot`。
- 已新增 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-DependencyBraidSourceFeed.ps1`：
  - 从 trace metrics 生成 StageLock 可读 source feed。
  - 评分/筛选字段包括 `dependencyBraidScore`、`dependencyBraidBadLocalRate`、`dependencySameSideOuterRate`、`dependencyDistanceP20`、local/near-outer patch run、family/source cap。
  - 产物：`dependency_braid_source_feed_v20_strict_v1.csv`（4 行）与 `dependency_braid_source_feed_v20_nearmiss_v1.csv`（16 行）。
- 已在 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` 增加：
  - `-MaxGeneratedDependencyBraidBadLocalRate`
  - `-PreserveAnchorTargetOrientation`
  - 保锚模式会读取 `anchorPlan` 的 `Tn->Bm@oX`，锁定 target 链方向，并跳过 post-merge，避免链序改变后 anchor 失效。
- 全量 V20 braid trace：
  - 输出 `pressure_read_stage_lock_hard_production_v20_braid_full_v1_metrics.csv`。
  - 88/88 traced。
  - `dependencyBraidBadLocalRate` 平均 `0.794`，p50 `0.818`；严格干净源只有 4 个，证明旧 hard pool 天然 local-patch 污染很重。
- StageLock rerun 结果：
  - 严格/near-miss braid feed 均未稳定产出。
  - 放宽静态外壳时曾产 1 个外出口干净候选（`outerExitHeadCount=0`、`outerExitSolveRunMax=0`），但 `dependencyBraidBadLocalRate=0.818`、`localPatchSolveRunMax=5`，被新 hard gate 正确挡掉。
- Anchor probe 结果：
  - 使用 building-grammar 分支现有 `Build-DependencyAnchorSourceVariants.ps1` 做外部 probe，输出到 rhythm-lab：`dependency_braid_anchor_source_v1.csv`。
  - Anchor 能造出干净依赖身体（如 `dependencyBraidBadLocalRate=0.333-0.5`、`localPatchSolveRunMax=1`、`dependencyFollowRunMax=2`），但常伴随不可解和大量 `outerExitHeadCount`。
  - Anchor-preserving StageLock 0 产；说明保住远依赖后，现有外圈/可解性约束无法自动修好。
  - 对 anchor 源做 outer-exit-head subset/all-flip 修复：122/320 组合里有少量可解样本，但可解样本仍保留 `outerExitHeadCount=5-6`；all-flip 0/8 solved。
- 当前关键判断：
  - 真正缺口不是 Greedy，也不是单纯外圈头修复，而是 source-generation 阶段需要三约束同时成立：远/跨区 physical dependency、低 outer-exit head、可 stage-lock。
  - 下一步不要继续旧 V20/V22 池盲跑；应把 dependency-anchor 生成器升级为“outer-clean anchor source generator”，先筛 anchor 候选是否制造外出口头，再交给保锚 StageLock。

## SGP Rhythm Lab Mid-Chain Hard Probe - 2026-06-20

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已在 StageLock 生成链路加入中型链组实验旋钮：
  - `Build-PressureReadStageLockVariants.ps1 -MinOutputChains`
  - `Select-StageLockHardCandidates.ps1 -MinChains`
  - `Run-StageLockStageDoorSymmetrySlice.ps1` 透传上述参数
- 目的：避免 StageLock 为了压低选择数把大一点的源合并到 25-35 链，验证“40-55 链 + 低选择 + 长结构链”的难关量产可能性。
- 关键实验结果：
  - `HardMidWide + MinOutputChains=40`：70 个合法 StageDoor 源，0 个 StageLock 候选；主要失败为 `weak stage lock` 和 `no stage-lock orientation`。
  - `HardMidWide + MinOutputChains=36`：跑出 1 个 47 链候选，真实 trace 为 `S`，`traceAvgChoices=4.32`、`traceMaxChoices=8`、`avgChain=13.681`、`longChainRate=0.596`、`structureCarrierRate=0.723`。
  - `DoorBalanced + MinOutputChains=36` 可复现同粗结构候选，但没有提高去重产率。
  - `DoorBalanced + StageDoor v1-only + MinOutputChains=36` 仍只产出同粗结构候选；优点是更快、更少无效变体，不能解决去重产能。
- 当前中型链组 probe 包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardMidChainProbe5V1Pack.asset`。
- 对应报告：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_midchain_probe_notes.md`。
- 当前结论：中型链组难关方向成立但极低产；瓶颈不是链长，而是大源缺少清晰 stage/door gate。下一步应强化 StageDoor 源生成的显式门锁语义，而不是继续盲扫 HardMid/HardXL。

## Open Items

- 状态：待验证 - 旧项目路径尚未在仓库记忆中确认；如后续确认迁移来源，需要更新路径迁移记录。
- 状态：待验证 - 现有未跟踪关卡、掩码、报告、导出包哪些是正式交付、哪些是实验产物，需要按具体任务确认。
- 状态：待验证 - 若要提交项目记忆，需要先确认是否同时提交既有 dirty worktree 中的其他内容。

## SGP Rhythm Lab Trace-Delta Filler Compiler V1 - 2026-06-22

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 核心结论：补链条不能继续按“哪里空就放哪里”或 `firstHit` 硬保守来做；有效路线是 `hard-lock base -> slot-based filler candidates -> board-level trace delta gate`。
- 已新增脚本：`.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-HardLockSlotTraceDeltaFillV1.ps1`。它从已验证 `HardLockSlotHeadFixV0` 真难样本出发，每次只插入一条 filler，并用 trace 前后 delta 控制 `maxChoices/avgChoices/antiLocal/supportClosure/localPatch/followRun/outerExit`。
- GPT 顾问的 placement field 思路被收敛为三类 slot 候选空间：`bridge`（贴近 hard-lock/ray corridor）、`noise`（贴近核心/控制 fanout）、`edge`（边缘补 coverage）。slot 只作为候选空间收缩，最终验收仍只信 board-level trace。
- 验证结果：`hard_lock_slot_trace_delta_fill_v1_slot_run1` 中间跑法耗时偏大但有效；从部分完成的 5 个 base 中筛出并冻结 5 关：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLockTraceDeltaFillV1SlotRun1Pack.asset`
  - Levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLockTraceDeltaFillV1SlotRun1Frozen/`
  - Frozen trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lock_trace_delta_fill_v1_slot_run1_frozen_metrics.csv`
- Frozen trace 5/5 `solved=True`、5/5 `processTier=A`、5/5 `TrueHardCandidate`；平均 `maxChoices=5.8`、`causalAntiLocalityScore=0.678`、`hardStructureV3Score=0.773`、`causalCudP20=10.667`、`causalCrossRegionCriticalLockCount=4.6`、`choicePeakCount=0`、`outerExitHeadCount=0`。
- 当前不足：覆盖只提升到约 `0.223-0.241`，还没到量产视觉标准；原多 base 多 round 跑法 15 分钟仍未自然结束，说明下一步要改成更快的 one-shot top-K 或 per-base checkpoint runner，而不是长串交互式迭代。

## SGP Rhythm Lab Designed Hard Lock V0 Breakthrough - 2026-06-22

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 新增 proof 脚本：`.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-DesignedHardLockV0.ps1`；定位是“物理依赖 hard-lock 原型”，不是最终 SGP 量产工具。
- 关键纠偏：运行时可消除不是只看 head ray 的首个 blocker，而是 `Build-SGPRhythmTrace.ps1` 内 `Propagate-Exits` 的真实 board-level 逃逸传播；设计依赖时必须按该规则预留整条逃逸路径。
- 验证结果：`designed_hard_lock_v0_trace_metrics.csv` 中 4/4 solved，4/4 `processTier=A`，4/4 `HardStructureV3Class=TrueHardCandidate`。
- 核心指标：`hardStructureV3Score=0.747`、`causalAntiLocalityScore=0.688`、`causalCudP20=9.667`、`causalCrossRegionCriticalLockCount=3`、`supportClosureBestScore=0.921`、`supportClosureBestDepth=3`、`maxChoices=5`、`avgChoices=2.7`、`outerExitHeadCount=0`、`localPatchSolveRunMax=0`。
- 当前不足：`dependencyFollowRunMax=4`，并且 coverage 很低，属于证明“真实难度结构可成立”的 breakthrough，不是可直接投产关卡。
- 下一步：把 DesignedHardLock 的 4 段 physical lock 模块前移为 SGP source/body 约束或 slot，再由 SGP 做高覆盖填充和变体化；最终 gate 仍使用 board-level trace。

## SGP Support Bridge Ray Semantics Probe - 2026-06-22

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已把 GPT 讨论中的关键语义纠偏落成只读/可导出 probe：`.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SupportBridgeRayProbe.ps1`。
- 语义锁定：`A -> B` 只表示 `B.escapeRay` 物理撞到 `A.geometry`；合法 support bridge 必须由真实 ray collision 形成 `upstream -> support -> hub -> target`。
- 验证结果：
  - `support_bridge_ray_probe_v1_source40.csv`：40 个 outer-clean anchor source 中找到 200 条静态物理 bridge row；`FlipBlockedRay:support=145`、`ExistingRay=55`。
  - `support_bridge_ray_probe_v1_emit20_emitted_sources.csv`：导出 20 个翻 support source；抽 3 个直接 trace，全部 `Drop`，`supportClosureBestScore=0`、depth `0`，说明盲翻 support 会把静态桥做出来但破坏可达性。
  - 单源 StageLock smoke：翻 support source 被 `dependency too local` 早期挡住；放松到只保留 `SupportClosureBias` 也 0 产。
  - `support_bridge_ray_probe_v1_existing_emit20_trace_metrics.csv`：不翻链的 existing-ray 20 源里 1/20 solved、5/20 出现 depth-2 causal closure；最好 solved 样本 `supportClosureBestScore=0.374`、depth `2`，但仍是 `HardStructureV3Class=LocalEasy`、`causalAntiLocalityScore=0.29`、`dependencyFollowRunMax=3`、`localPatchSolveRunMax=4`。
- 当前判断：
  - GPT 的 ray 语义修正是有效的，已经变成可复用 probe。
  - `FlipBlockedRay` 只能作为诊断/局部候选，不能作为主修复 operator。
  - `ExistingRay` 证明真实 causal closure 能出现，但仍太弱、太 local，无法直接作为量产突破。
  - 下一步应做 feasibility-aware `SidecarAsUpstreamBlocker`：在 support 逃逸线上增加或选择不会破可解性的 upstream blocker，并要求它在 trace-causal closure 中真实出现；不要继续扩大盲翻 support 或只筛 existing-ray。
- 2026-06-22 追加 Sidecar/MultiHop 验证：
  - `InsertSidecarAsUpstreamBlocker`（在 support ray 空位插 L 形小链）在 20 源严格/宽松阈值下均 0 产；诊断显示当前 dense SGP body 的 support ray 前方 `rayCells=0`，没有可插空位。
  - `ExistingMultiHopRay`（只选择已有 `B -> U -> support -> hub -> targets` 多跳物理链）20 源产 85 row、导出 19 source；trace 后 1/19 solved、4/19 depth-2 closure，最好 solved 仍为 `supportClosureBestScore=0.374`、`HardStructureV3Class=LocalEasy`、`causalAntiLocalityScore=0.29`、`dependencyFollowRunMax=3`、`localPatchSolveRunMax=4`，没有突破 `ExistingRay` 上限。
  - `SplitEndpointSidecar`（把 immediate blocker 端头 3 格切成 sidecar）诊断显示 20 源有 91 个 endpoint 机会，但严格非直线 sidecar 为 0；放宽直段后导出 20 source，trace 0/20 solved，仅 1/20 出现 depth-2 closure 且仍 `LocalEasy`。
  - 结论：当前 Sidecar 不能作为后验微修复主线；真正下一步应前移到 source-level bridge-slot generation，在密度填充前预留/塑造非局部 support lane，再用 trace-causal closure 做验收。

## SGP Support Graph Closure V1 - 2026-06-22

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已将 GPT 讨论中的 bounded support closure 落地到 SGP hard-lane：closure 不是 full DFS，也不是单纯静态 dependency graph 分数，而是“静态结构先验 + 真实执行因果验证”。
- 已在 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` 增加 trace-side Support Graph Closure V1 指标，并用 depth gate 避免一跳 conveyor 被误判为多跳 support closure。
- 已在 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-OuterCleanDependencyAnchorSourceVariants.ps1` 增加 `-SupportGraphClosureAnchor`，source anchor 阶段可要求 support->hub->target 静态闭包。
- 已在 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` 增加 `-SupportClosureBias`，并完成关键纠偏：StageLock gate 不再直接信任 planned deps，而是在模拟 solve 中记录 `parentOf[child] = first chain that made child available`，用实际 availability transition 构造 causal closure。
- 验证结果：
  - `dependency_braid_outerclean_anchor_supportclosure_v1b_probe40.csv` 可生成 5 个强静态闭包源，静态分数约 `0.892-0.995`、depth `2-3`。
  - `dependency_braid_outerclean_anchor_supportclosure_v1b_probe40_trace_metrics.csv` 证明静态闭包不等于真实闭包：只有 1/5 solved A，且真实 closure 仅 `0.374/d2`；4 个 Drop 并非简单 greedy 假阴性。
  - planned-deps 版 StageLock `SupportClosureBias` 曾产 2 个候选，生成侧看似 `0.431/d2` 和 `0.816/d3`，但 trace 均塌回 `supportClosureBestScore=0`、`HardStructure V3 Class=LocalEasy`、`localPatchSolveRunMax=5`。
  - causal gate 低预算复测输出 `dependency_braid_supportclosure_v1b_stagelock_causal_lowbudget_rejected.csv`：5 源 0 产，拒绝为 `support closure too weak=3`、`local patch burst too linear=2`。其中原假阳性源被压成 `supportClosureBestScore=0`、depth `0-1`，说明新 gate 已能挡住 static/planned closure 假阳性。
- 历史 `*metrics.csv` 扫描显示真实 `supportClosureBestDepth>=2` 且 `score>0` 的样本只有 4 条，最高约 `0.385/d2`，且仍是 `Drop/B/A` 混杂、`localPatchSolveRunMax=3-5`；说明当前缺的是 support bridge 结构本体，不是单纯阈值或扫源量。
- 2026-06-22 追加 Support Feasibility Bridge V0 诊断：
  - `Build-PressureReadStageLockVariants.ps1` 已输出 `supportClosureFailureClass`、`supportClosureFailurePenalty`、best hub/root/target/support/upstream raw 与 valid counts。
  - `dependency_braid_supportclosure_v1b_stagelock_bridge_diag_rejected.csv`：5 源 0 产，失败类型为 `upstreamIsConveyor=3`、`supportIsConveyor=1`、`missingCrossRegionSupport=1`。
  - 将 failure class 加入 StageLock 搜索惩罚后，`dependency_braid_supportclosure_v1b_stagelock_bridge_penalty_rejected.csv` 仍 0 产，仍由 `upstreamIsConveyor` 主导，说明不是简单选向排序不足。
  - 仅开 `-PreserveAnchorTargetOrientation` 后，`dependency_braid_supportclosure_v1b_stagelock_preserve_target_rejected.csv` 仍 0 产且 5/5 先因 `dependency follow too linear` 被拒。
  - 新增 `-PreserveAnchorSupportOrientation`，把 anchor blocker/hub/support 固定到源层方向；`dependency_braid_supportclosure_v1b_stagelock_preserve_support_rejected.csv` 仍 0 产，失败为 `upstreamIsConveyor=4`、`supportIsConveyor=1`。
  - 使用现有 source 生成器的严格开关验证：`dependency_braid_outerclean_anchor_supclosure_bridge_v0.csv` 在 20 源里可产 1 个 realized depth-2 support bridge source，但进 StageLock 后仍被 `dependency follow too linear` 拒绝，causal 失败为 `upstreamIsConveyor`。
  - 将 source 层提升到 `SupportClosureMinDepth=3` 后，`dependency_braid_outerclean_anchor_supclosure_bridge_d3_v0.csv`（20 源）和 `dependency_braid_outerclean_anchor_supclosure_bridge_d3_wide_v0.csv`（80 源、3 variants）均 0 产。
- 当前判断：Support Graph Closure V1 已成为有效 guardrail，但还不是量产突破；下一步不应再盲目扩大候选、继续调方向惩罚或只靠现有 source strict filter，而应做真正的 Support Feasibility Bridge：在 source/repair 层显式给 support 的上游增加或重接跨区、非贴脸、能先于 hub 触发的 support unlocker，并要求该 upstream->support->hub->target 关系出现在 causal closure graph。
- 详细报告：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/support_graph_closure_v1_notes_20260622.md`。

## SGP Building Grammar Hard Outer Pressure v199-v208 - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-building-grammar`，branch `codex/sgp-building-grammar`。
- 人工反馈：D198 仍表现为外圈出口和依赖关系偏弱、开局消除压力偏低；新的重点不是简单调高/调低 `openers`，而是减少外圈可见连扫，并强化远距离/meaningful dependency。
- 已在 `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` 前置新增普通外圈 solve 统计：
  - `outerTouchSolveCount`
  - `outerTouchSolveRatio`
  - `outerTouchRunMax`
  - `sameOuterSideSolveRunMax`
  并新增 opening-pressure 评分旋钮 `OpeningPressureMaxOuterTouchSolveRatio`、`OpeningPressureMaxSameOuterSideSolveRunMax`。
- 已在 `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Run-DepAnchorLowChoiceStrictSlice.ps1` 透传上述两个 opening-pressure 旋钮。
- d205 结论：alt-good/openers=6 源 triple repair 0 产，不能作为“压开局/压外圈”的可修路线。
- d206/d207/d208 结论：
  - `targetOpeners=2 + outergate` 在 180s 内未进入 StageLock candidate 输出，说明当前源上压到 2 入口过重。
  - target3 frontgate 可产 raw：`crossUnlockRatio≈0.048`、`spineBalance=0.75`、`spineAlternationRunMax=9`、`avgChoices=2.724`，但 `dependencyFollowRunMax=3`、`outerTouchSolveRatio=0.621`、`sameOuterSideSolveRunMax=5`。
  - d207/d208 triple repair 可把 `followRun` 压到 2，但会把 `stageLockScore` 打到 `0.469/0.493`、`lateRegionCount=1`，判定为坏修复。
- 历史 trace 扫描结果：
  - 严格条件 `openers=2..4`、`avg<=3.25`、`max<=5`、`followRun<=3`、`stage>=0.74`、`late>=3`、`cross>=0.038`、`outerTouch<=0.55`、`sameOuterSideRun<=2`、`nearUnlock<=0.28`、`avgUnlockDistance>=8` 为 0 产。
  - 放宽后最好的临界样本仍集中在同一 `47e565/hcd58` family，最佳为 `pressure_read_stage_lock_47e565_04_pairrepair_10_8_13_f6c370a142`：`openers=3`、`avgChoices=2.68`、`maxChoices=5`、`followRun=2`、`stageLockScore=0.810`、`lateRegionCount=3`、`crossUnlockRatio=0.043`、`outerTouchSolveRatio=0.571`、`sameOuterSideSolveRunMax=2`、`strictMeaningfulUnlockRate=0.840`、`nearUnlockRate=0.200`、`avgUnlockDistance=8.927`。
- 已冻结并挂 Demo 小幅改良 review 包：
  - Pack：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_D199HardPressureOuterRun2Review1Pack.asset`
  - Levels：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/D199HardPressureOuterRun2Review1/`
  - Selected CSV：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d199_hardpressure_outerrun2_review1_selected.csv`
  - Demo activePack GUID：`c9f5ed1ec7a646eeb0491ad82d2c8371`
- 当前判断：D199 是比 D198 外圈同侧连扫略好的 review，不是稳定突破。下一步不应继续后验翻链修 d207/d208，而应在 source/StageLock 前置阶段找或制造“外圈天然干净 + cross/stage 保留”的源；否则强修会打碎 stage lock。

## SGP Building Grammar Dual-Spine Production Probe - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-building-grammar`，branch `codex/sgp-building-grammar`。
- 目标仍未完成：需要基于 SGP/StageLock 主流程稳定产出“真实 dual-spine / cross unlock”难关，而不是只产强单线门锁。
- 已确认主线未损坏：`v139_dense_restore_regression` 复现已知 strict control，`chains=30`、`coverage=0.933`、`avgChoices=2.70`、`maxChoices=4`、`dependencyFollowRunMax=2`、`crossUnlockRatio=0.042`、`singleSpineDominanceRate=0.500`、`spineBalance=0.556`。
- 正向扩展信号：`v144_dense_symmetry_probe` 中，已知 strict source 的 `FlipX` symmetry 也 strict 通过，指标与原 control 基本一致；`FlipY/Rot180` 在 150s 单源上限下 timeout。
- 负向结论：
  - 强修复参数组合（stage rescue + trim + straight-cut 等）不适合作为默认量产；dense control 会变成 empty/weak stage lock，虽已加内部预算避免无限卡住。
  - `StageDoor` source enhancement 能做出很强低选择门锁候选（如 `avg=2.529`、`max=5`、`followRun=2`、`stageLockScore=0.856`），但真实 `crossUnlockRatio=0`，属于强单线门锁，不算 dual-spine strict。
  - flip 可把 `cross` 拉到约 `0.04`，但会导致 `dominance=0.742`、`balance=0.300` 这类伪 cross；strict selector 应继续拒绝。
  - `DualBalanced` StageDoor profile 作为实验已加入，但 4-merge 会失去 StageLock orientability，3-merge 回到单线强门锁；当前不是突破。
  - `v149` risk cache 排雷后，中型 CPS next feed 只剩少量已 spent/低价值源，现有 CPS pool 基本到瓶颈。
- 已改脚本：
  - `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Run-CPSInterleaveSourceBatch.ps1` 透传 `-EnableWeakInterleaveSidecar`。
  - `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` 增加 scheduled trace/weak sidecar diagnostics、高成本 prune 内部预算、失衡 cross flip 拒绝。
  - `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-StageLockSourceEnhancedVariants.ps1` 增加实验 `StageDoorProfile=DualBalanced`。
  - `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-CPSInterleaveSourceRiskCache.ps1` next feed 排除 repeated empty/timeout/spent source。
- 当前报告：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/building_grammar_cps_interleave_v1_20260620.md` 已追加 `v116-v150 Weak-Activation / Source-Expansion Follow-up`。
- 下一步建议：停止 broad source scan 和强后处理救援；转向源生成/父源 refit 前置 cross topology：在 SGP 父源阶段制造两个空间分离 carrier groups 与真实 latent A->B/B->A edge potential，再进 StageLock strict gate。Yellow 单线 StageDoor 难关可作为 hard-door-lock 候选单独池，不计入 dual-spine 产能。

## SGP Hard Lane V2.1 Trace Metrics - 2026-06-20

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已把 GPT 讨论中的 Hard Lane V2.1 思路落成可计算 trace 指标：`choiceChangeRate`、`collapseEventRate`、`choiceRiseRate`、`choiceFlatRate`、`lowChoiceRunMax`、`unlockPower`、`postSpikeConvergence`、`crossRegionUnlockRate`、`boringLinearScore`、`structuredHardnessV21`。
- 关键修正：raw `collapsePressureIndex` 区分度不足，V20/VeryLong/ProductiveRefit 上几乎都接近高值；当前采用窗口化/事件化信号，重点看选择空间是否变化、解锁事件是否有结构影响、解锁后是否重新收紧。
- `Select-StageLockHardCandidates.ps1` 已支持 `-MaxBoringLinearScore` 和 `-MinStructuredHardnessV21`；`Run-HardMidWideDoorBalancedStageGateMicroSlice.ps1` 已透传这两个参数。
- V20 校准：用 `MaxBoringLinearScore=0.46`、`MinStructuredHardnessV21=0.58` 且保留旧链条结构门槛后，`73/88` 保留，拒绝原因为 `weak structured hardness v21=12`、`max choices=1`、`boring linear rhythm=1`。
- VeryLong pressure 候选 `1/1` 保留；ProductiveRefit V21 probe `0/4` 保留，说明 V2.1 不能替代 `longChainRate/structureCarrierRate/avgChain` 等链条结构门槛，只能作为 rhythm gate 和 score boost。
- 补跑被 prefilter 跳过的 `hmw_broad_v21_agglong_vlgate_pro_c03`：生成 3 个真实 `S` 候选，`structuredHardnessV21=0.702-0.763`，但全部因 `avgChain/longChainRate` 不足被 strict select 拒绝。结论：V2.1 已能发现节奏 hard near-miss，下一步应做二次合链/重组把 near-miss 强化成真 hard candidate，而不是放宽链条结构门槛。
- 已新增实验开关 `Build-PressureReadStageLockVariants.ps1 -NearMissChainRescue`，并透传到 `Run-HardMidWideDoorBalancedStageGateMicroSlice.ps1`。该开关默认关闭，只在 `AggressiveLongChainMerge` 下放松 near-miss 合链接受条件。c03 复测显示：单独开关无变化；叠加 `AllowEntryRootMerge` 可把一个 maze 样本推到 `avgChain=10.044`、`structuredHardnessV21=0.810`，但仍因 `longChainRate=0.333`、`structureCarrierRate=0.467` 被 strict select 拒绝。下一步应优先补“中短链并入结构承载链”的策略。
- 已继续实现 `NearMissChainRescue` 下的结构承载链吸收合链：支持端点吸收、插入式吸收、预算回退和搜索上限。c03 最好样本可达 `mergedCarrierAbsorbCount=5`、`avgChain=11.3`、`veryLongChainRate=0.225`、真实 trace 仍为 `S`，但 `longChainRate=0.375`、`structureCarrierRate=0.450` 仍未过正式线；c04 未加搜索上限时超时，已终止本次实验 PowerShell 并加组合搜索上限。结论：插入式吸收方向有效，但 c03/c04 不是继续硬拧的好源，下一步应批量找几何更适合吸收的 highV21 near-miss。
- 已生成 near-miss catalog：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_v21_nearmiss_catalog_20260620.csv`。当前定义下有 21 行记录，但按 `sourceLevelId` 粗去重只有 3 个源，全部来自 c03；说明下一步优先级应是扩大 highV21 near-miss 源池，而不是继续单点救 c03。
- 突破验证：将 `NearMissChainRescue + AllowEntryRootMerge + AggressiveLongChainMerge` 用在 V20 trace-aware 的 c05/c06 section_unlock chunk 后，c05 产出 1 个正式 hard selected，c06 产出 1 个正式 hard selected。两个样本均真实 trace `S`，`avgChain=11.079`、`longChainRate=0.474`、`veryLongChainRate=0.263`、`structureCarrierRate=0.605`、`mergedCarrierAbsorbCount=3`，证明插入式/吸收式合链能把原本 `long chain rate too low` 的 near-miss 推成正式 hard。
- 已冻结 review 包但未挂 demo、未替换 V20：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockNearMissRescueBreakthrough2Pack.asset`；冻结关卡目录：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDANearMissRescueBreakthrough2/`。
- 当前报告：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_v21_trace_metrics_calibration_20260620.md`。
- 下一步建议：继续 HMW/VeryLong 小切片，使用 `-AggressiveLongChainMerge -AllowVeryLongPressureGate -MaxBoringLinearScore 0.46 -MinStructuredHardnessV21 0.58`，目标扩充同时满足节奏硬度与复杂长链结构的候选池；V20 仍是当前正式 demo/review 包，V21 不冻结正式包。
- 已把 `NearMissChainRescue` 的 carrier absorb 搜索预算参数化，并透传到 `Run-HardMidWideDoorBalancedStageGateMicroSlice.ps1`：`CarrierAbsorbComboLimit`、`CarrierAbsorbCarrierSeenLimit`、`CarrierAbsorbAbsorbSeenLimit`、`NearMissCarrierPassLimit`、`NearMissCarrierMaxMergeLimit`。默认值保持旧逻辑，显式低预算用于单源快筛，避免 maze/dual 源爆搜。
- 预算化 rescue 新增正结果：ProductiveRefit V21 的 `dual_zone h0bd649` 单源低预算 21 秒产出 1 个 strict hard selected，真实 trace `S`，`avgChoices=3.92`、`maxChoices=7`、`stageLockScore=0.562`、`structuredHardnessV21=0.580`、`chains=36`、`avgChain=10.556`、`longChainRate=0.444`、`structureCarrierRate=0.583`、`straightLikeRate=0`。说明 rescue 不只适用于 section_unlock。
- 预算化 rescue 负结果：ProductiveRefit maze `h62e1b` 低/中预算均可 trace `S`，但最高只到 `avgChain=11.3`、`longChainRate=0.375`、`structureCarrierRate=0.450`；节奏很硬但链条分布不够，暂归为低选择压力 near-miss，不进入当前复杂长链正式池。单源 `section_h69a30` 与 `dual_hc891a` 快筛 0 产，失败集中在 `weak stage lock`。
- 已合并 c05/c06 section_unlock 2 关 + ProductiveRefit dual_zone 1 关，冻结新的 3 关 review pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockNearMissRescueBudgetBreakthrough3Pack.asset`；CSV：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_nearmiss_rescue_v21_budget_breakthrough3.csv`；关卡目录：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDANearMissRescueBudgetBreakthrough3/`。该包未挂 demo，未替换 V20。
- 已新增 `Build-NearMissRescueSourceFeed.ps1`，扫描历史 `*_selected_hard_rejected.csv` 并反查 `sourceLevelId/sourcePath`，输出可直接重跑的 source feed：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_door_source_nearmiss_rescue_prefilter_v1.csv`。当前输出 22 个 near-miss source rows，覆盖 `maze_long_chain/maze/sweep/section_unlock/dense`。
- prefilter 正结果：dense `h4f810` 低预算 43 秒产出 1 个 strict hard selected，真实 trace `S`，`avgChoices=4.11`、`maxChoices=8`、`stageLockScore=0.571`、`structuredHardnessV21=0.749`、`chains=36`、`avgChain=12.111`、`longChainRate=0.444`、`structureCarrierRate=0.722`。这证明预算化 rescue + source-level prefilter 已经跨到第三个 family：`section_unlock + dual_zone + dense`。
- prefilter 负结果：sweep `h058d` 低/中预算均停在 `avgChain=9.833`、`longChainRate=0.417`、`structureCarrierRate=0.639`，拒绝原因仍为 `avg chain too low`，不是预算问题；后续可作为 near-miss 观察，不优先继续烧。
- 已合并 c05/c06 section_unlock 2 关 + ProductiveRefit dual_zone 1 关 + prefilter dense 1 关，冻结新的 4 关 review pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockNearMissRescueBudgetBreakthrough4Pack.asset`；CSV：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_nearmiss_rescue_v21_budget_breakthrough4.csv`；关卡目录：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDANearMissRescueBudgetBreakthrough4/`。该包未挂 demo，未替换 V20。
- 新接续点：用 `Build-NearMissRescueSourceFeed.ps1` 继续跑 quick profile 小批，优先 `dense/section/dual/sweep` 中 `processTier=S/A`、`stageLockScore>=0.55`、`longChainRate 0.36-0.42`、`structureCarrierRate>=0.50`、`mergePotentialStructure/Long` 高的源；先跳过 `maze/maze_long_chain` mid profile，除非专门做低选择压力 lane。

## Worktree Cleanup - 2026-06-19

- 已保留主工作树：`F:\Unityproject\ArrowLevel-Hand`，branch `main`。
- 已保留新开的 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已将旧 worktree 的工作区改动归档为 WIP commit，并移除对应 worktree 目录：
  - `nomask-procedural-generator` -> commit `7fd1a99e`
  - `codex/sgp-guided-map` -> commit `d8a428cd`
  - `codex/architectural-linework` -> commit `7221e381`
  - `shape-procedural-mask-fill` -> commit `55e93871`
- 已执行 `git worktree prune`。
- 未将这些 WIP 归档分支合并到 `main`；后续如需采收内容，应从对应分支 cherry-pick、merge 或人工挑选。
- 当前主工作树仍有大量既有 dirty 状态；不要把 worktree 归档分支和主目录现有改动混为一谈。

## SGP Rhythm Lab - 2026-06-19

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已建立静态筛选脚本：`.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmLab.ps1`。
- 已建立真实过程 trace 脚本：`.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1`。
- 报告目录：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`。
- 当前 20 关 trace 初跑结果：`S=4`、`A=5`、`B=4`、`Drop=7`；说明真实过程筛选比静态筛选更严格。
- 待办：用 `processTier=S/A` 和 `choice curve` 指标回灌 SGP 生成/筛选，减少普通关中 `maxChoices >= 12`、中盘平均选择过高和贴脸连续解锁。

## PressureRead Far Dependency Prototype - 2026-06-19

- 已新增实验脚本：`.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PressureReadOrientationVariants.ps1`。
- 当前策略：保留现有关卡形状，通过链条头尾方向重组构造无环依赖图；优先远距离、跨区域、换方向的 blocker，限制近距离依赖、附近连续解，以及无意义外圈长直链连续消除。
- 已加入直链结构指标：生成器和 trace 均记录 `outerStraightRunMax`、`sameSideOuterStraightRunMax`、`outerStraightSolveRatio`。直链不被禁止，但同边外圈长直链互挡、平行直链互挡和连续扫边会被降权或筛掉。
- 最新小批结果：生成 10 个候选，真实 trace `ProcessTier` 为 `S=6`、`A=4`；`Trace Risk=Low 10`；`over10Rate avg=0`，`maxChoices avg=8.8`，`meaningfulUnlockRate avg=0.906`，`outerStraightRunMax avg=1.2`，`sameSideOuterStraightRunMax avg=0.8`。
- 当前 Demo 已挂：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadOrientationPreviewPack.asset`。
- 当前报告：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_structured_trace_summary.md`。
- 下步重点：人工看 Demo 感受“结构化直链 + 远依赖”是否成立；如果仍觉得直链感强，下一步再做轻量几何改写，把外圈长直链拆成“边界段 + 门锁段”，而不是把所有直链弯掉。

## PressureRead StageLock Breakthrough Prototype - 2026-06-19

- 已新增实验脚本：`.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1`。
- 已新增参考画像脚本：`.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-ReferenceSeedStructureProfile.ps1`，用于分析外部 298 seed 的链长/转弯/结构承载指标；外部参考只用于指标校准。
- 当前策略：在 PressureRead orientation 基础上加入阶段计划、区域延迟打开指标，并尝试把非入口区独立根链合并到相邻低阶段关键链尾部，降低平行入口，形成更像门锁的长链结构；同时用参考画像加入 `avgChain`、`longChainRate`、`structureCarrierRate` 等结构指标。
- 当前 Demo 已挂稳定 curated 包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockCurated5Pack.asset`；`Demo.unity` 已指向该包。
- 可覆盖写入的实验包仍是：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockPreviewPack.asset`。
- 当前候选目录：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PressureReadStageLock/`。
- 当前报告：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_trace_summary.md`。
- 最新 curated 小批结果：5 关，真实 trace `ProcessTier=S=3/A=2`，`Trace Risk=Low 5`；`over10Rate avg=0`、`maxChoices avg=8.4`、`meaningfulUnlockRate avg=0.934`、`stageLockScore avg=0.642`、`lateRegionCount avg=2.4`、`stageGateRate avg=0.321`。
- 结构观察：较优样本包括 `zig_river`、`section_unlock`、`sweep`、`outer_shell`，其中 `longChainRate≈0.25-0.35`、`structureCarrierRate≈0.4-0.56` 的关卡更接近“难但可读”；`maxChain=120` 的样本保留为风险对照，不放在 demo 第一关。
- 当前瓶颈：240 源 deterministic 后处理约产出 5 个可用候选；失败主要集中在 `static curve too open`、`weak stage lock` 和后处理方向构造成本高。继续量产应把 stage/door 语义前置进 SGP 生成，而不是主要依赖 StageLock 后处理硬捞。

## PressureRead StageLock Dependency Merge - 2026-06-19

- 已在 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` 增加依赖子链合并：当 B 被 A 解锁，且 B 可以通过端点拼到 A 尾部时，把 B 并入 A，重新计算依赖并重新评估，只接受可解且过程曲线不变差/变好的结果。
- 当前 Demo 已改挂：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockBalancedDepMerge6Pack.asset`。
- 最新 120 源结果：生成 5 关，真实 trace `ProcessTier=S=4/A=1`、`Trace Risk=Low 5`；`over10Rate avg=0`、`maxChoices avg=7.6`、`meaningfulUnlockRate avg=0.954`、`stageLockScore avg=0.663`、`lateRegionCount avg=2.4`、`stageGateRate avg=0.385`。
- 代表样本：
  - `pressure_read_stage_lock_04_direct_pure_topup_03_05_direct_refresh_topup_rect_sweep_g0_v07`: `openers=3`、`avg=3.318`、`max=7`、`stageLockScore=0.785`、`avgChain=9.955`、`maxChain=36`、`longChainRate=0.364`、`structureCarrierRate=0.614`、`mergedDependencyCount=12`。
  - `pressure_read_stage_lock_03_direct_pure_topup_03_12_direct_refresh_topup_rect_dense_weave_g1_v08`: `openers=2`、`avg=3.462`、`max=6`、`avgChain=11.077`、`maxChain=29`、`longChainRate=0.513`、`structureCarrierRate=0.641`、`mergedDependencyCount=12`。
- 重要结论：依赖子链合并是当前最明确的难度和结构提升点；它把“解锁后马上多一根可选链”的伪深度转成“关键长链”。但 120 源产 5 个仍不是最终量产，下一步要做快速预筛或把 door blocker 直接前置到 SGP 源生成。

## PressureRead Balanced DepMerge Production Signal - 2026-06-19

## SGP Building Grammar Extreme Probe - 2026-06-20

- 当前实验 worktree：`.worktrees/sgp-building-grammar`，branch `codex/sgp-building-grammar`，从 `codex/sgp-rhythm-lab` 派生；该 worktree 内 `Tools/SGPRhythmLab` 与 `Assets/ArrowMagic/SOData/.../SGPRhythmLab` 多数为未跟踪实验产物，属于本分支实验上下文。
- 已建立巨难关卡验收方向：核心不再是“看起来建筑”，而是低选择曲线、强 stage/door lock、区域延迟释放、中长结构链承载、Greedy 可解。远/近依赖质量是辅助信号，不作为唯一硬门槛。
- 关键脚本变更：
  - `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1`：新增 `extremeProcessTier`、`strictMeaningfulUnlockRate`、`nearUnlockRate`、`avgUnlockDistance`、`avgSolveJumpDistance`；S+/S 硬门槛回到低选择曲线 + stage lock。
  - `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Select-StageLockExtremeCandidates.ps1`：新增极难候选选择器，默认只选 `S+/S`，`-IncludeExtremeA` 才允许 A 作为人工备选。
  - `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1`：新增 `-ExtremeBias`，生成阶段主动偏向 `openers<=3`、低 `choiceP80/maxChoices`、高 `stageLockScore/lateRegionCount/stageGateRate`；候选 CSV 保留 `type/families/stageLockFamilyKey`。
  - `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Run-StageLockTargetedDepAwareSlice.ps1`：新增 `-ExtremeMode`、`-IncludeExtremeA`、`-RandomAttemptsPerLevel`、`-AllowEntryRootMerge`，修复 trace 输出 root，空选择时跳过 freeze。
- 已验证结果：
  - hard10 重新 trace 后得到 `S+=1/A=2/Drop=7`；最佳 S+ 为 `openers=3`、`avgChoices=2.59`、`choiceP80=3`、`maxChoices=4`、`stageLockScore=0.821`、`lateRegionCount=3`、`stageGateRate=0.545`。
  - `bg_extreme01`：120 增强源 -> 14 StageLock 候选 -> `S+=1/A=4`，冻结包 `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLock_bg_extreme01_ExtremeProbePack.asset`。
  - `bg_extreme03`（启用 ExtremeBias）：80 增强源 -> 3 StageLock 候选 -> `S+=1/A=2`，3 个全入池；冻结包 `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLock_bg_extreme03_ExtremePack.asset`。
  - `bg_extreme04`：offset=20 后仍为 `S+=1/A=2`，但收敛到同类 `sweep/lock_buckle`。
  - `bg_extreme05/bg_extreme06`：过滤掉 `sweep/lock_buckle` 后，100 增强源均产出 0 个极难 StageLock 候选；开启 `AllowEntryRootMerge` 仍为 0。
  - `bg_extreme07` 微型随机方向搜索：非核心家族 10 源、20 次随机尝试仍产出 0；说明瓶颈不是简单方向搜索不足。
- 当前核心结论：`ExtremeBias` 能显著提高已可用父本的候选纯度，但不能扩父本。真正巨难目前主要依赖 `sweep/lock_buckle`，其他家族失败集中在 `static curve too open`、`no stage-lock orientation`、`weak stage lock`。下一步要做源级 stage-lock 结构预改造/预筛，而不是继续盲目大跑。
- 当前详细报告：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/building_grammar_extreme_probe_20260620.md`。

## SGP Building Grammar Extreme Harden - 2026-06-20

- 已在 `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` 给 `-ExtremeBias` 增加高 `stageGateRate`、低 `activeRegionAvg` 的 stage-pass 分支，避免 `firstThirdRegionCount<=5` 误杀前段区域多但仍强门锁的候选。
- 已增加 `-ExtremeBias` 专用 tight-choice structure waiver：当真实过程选择曲线足够紧时，允许略低于普通长链结构门槛的候选进入极难人工审查池。
- 结果：`bg_extreme03` 的两个 `lock_buckle` A 级候选被压到 S；与原 S+ `sweep` 合并形成 3 关 HardenV2 包。
- 当前 harden 包：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLock_bg_extreme_HardenV2Pack.asset`，Demo 已挂该包 guid `ca6c05ed86b94e609906ebaccc67eb8d`。
- 冻结 trace：`Extreme Process Tier S+=1/S=2`、`Trace Risk Low=3`、`maxChoices avg=4.67`、`stageLockScore avg=0.793`、`stageGateRate avg=0.655`、`over10Rate avg=0`。
- 风险：两个 `lock_buckle` 的过程难度达标，但 `longChainRate` 低于 sweep，需要人工看图确认是否显得过于干净或紧凑。
- 详细报告：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/building_grammar_extreme_harden_20260620.md`。

## SGP Building Grammar Visual Strict Feedback - 2026-06-20

- 人工反馈：`HardenV2` 第 1 关可接受；第 2/3 关外出口集中，依赖集中在一条链路，容易顺着点，实际体感不够硬。
- 已在 `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` 增加视觉/过程集中度指标：`dependencyFollowRunMax`、`dependencyFollowRate`、`sameRegionSolveRunMax`、`dominantRegionSolveRate`、`sameOuterSideSolveRunMax`、`dominantOuterSideSolveRate`、`outerTouchSolveRatio`。
- 已在 `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Select-StageLockExtremeCandidates.ps1` 增加同侧外出口集中和依赖顺链集中惩罚/剔除；单纯依赖跟随不硬杀，必须结合外侧集中，避免误伤第 1 关。
- 验证：`HardenV2` Level 1 为 `risk=Low`、`sameOuterSideSolveRunMax=3`；Level 2/3 被标为 `OuterSideConcentration/DependencyFollowRun`，`sameOuterSideSolveRunMax=6`。
- 已生成视觉严格对照包：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLock_bg_visualstrict01_ProbePack.asset`，Demo 已挂 guid `3b1df34a74954caeab335d11c347179f`。
- VisualStrict01 冻结 trace：`Trace Risk Low=4`、`Process Tier S=4`、`Extreme Process Tier S+=1/A=3`、`maxChoices avg=5.25`、`stageLockScore avg=0.797`；这是体感修正版，不是纯 S/S+ 极限版。
- 详细报告：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/building_grammar_visualstrict_feedback_20260620.md`。

## Building Maze Gap Analysis - 2026-06-20

- 人工反馈继续确认：`visualstrict01` 只是修了外出口集中问题，仍没有向竞品建筑迷宫方向突破。
- 已新增画像脚本 `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Measure-ArchitectureLineworkProfile.ps1`，用于把外部 reference `seed.json` 和本地 LevelDefinition 放到同一口径比较。
- 已分析 `Above300` reference 前 80 个 seed 与本地 SGP hard/visualstrict 候选：reference 平均 `lowTurnChainRate=0.673`、`highTurnChainRate=0.246`、`avgSegmentLength=3.304`、`longSegmentRate=0.168`、`thinWallChainRate=0.649`；本地 SGP hard 候选为 `lowTurnChainRate=0.385`、`highTurnChainRate=0.502`、`avgSegmentLength=2.560`、`longSegmentRate=0.055`、`thinWallChainRate=0.473`；visualstrict 更偏离。
- 核心结论：当前 SGP/StageLock pipeline 优化的是“高弯折依赖链”，而竞品建筑迷宫是“低弯折建筑墙段”。`structureCarrier = length>=7 && turnCount>=2` 这类指标天然把生成器推向错误链条语言。
- 下一步应新建 `ArchitecturalLineworkSGP` 源生成 lane：先生成墙段/门洞/半包围建筑线稿，再做方向分配和 StageLock/Greedy 验证；不要继续只靠 StageLock selector 调参。
- 详细报告：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/building_maze_gap_analysis_20260620.md`。

## SGP Building Grammar Interleave Activation v0 - 2026-06-20

- 已吸收 GPT 关于 `activation scheduling collapse` 的建议：当前 near-miss 不是没有双 spine，而是解锁时间轴单线程坍塌，`crossUnlockRatio=0`、`spineAlternationRunMax=10-22`。
- 已在 `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` 增加按需 dual-spine activation metrics，并在 `-InterleavedLockBias` 下加入局部 orientation flip repair；完整 dual-spine 计算改成 on-demand，避免每次普通评价都算。
- 已在 `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` 增加 `-InterleaveFlipProbe`，用于对已有 LevelDefinition 做离线翻链 probe，不写关卡 asset。
- 验证结果：4 个旧 near-miss 离线 flip probe 成功跑完，输出 `sgp_building_dualspine_v0_flip_probe_metrics.csv`；局部 flip 能降低单 spine dominance，少量提升 `crossUnlockRatio`，但无法把 `spineAlternationRunMax` 压到目标，且常把 `avgChoices` 推高到 2.79/2.83。
- 关键结论：单纯后验 flip/repair 不足以破局。下一步应把“交叉激活”前置到 source/orientation selection：做 `Cross-Potential Score` 父源筛选、orientation 构造阶段主动制造 A->B/B->A 边，再用 dual-spine gate 验收。
- 性能结论：主 StageLock 生成路径即使 1 source/0 random 也可到约 102 秒；Top24 首源诊断超过 120 秒。后续长跑前需要加 per-source timeout/chunk progress，避免无监控浪费。
- 详细报告：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/building_grammar_interleave_activation_v0_20260620.md`。

- 已将 StageLock 源排序从“纯硬结构优先”改为平衡排序：仍奖励中长链、结构承载链、`Mixed/Bridge`、`dense_weave/sweep/section_unlock` 等源，但强惩罚原始 `openers/avgChoices/maxChoices` 太高的源。
- 已加入 `mergePotential` 轻量预检与动态依赖合链上限：低潜力源跳过昂贵合链，高潜力且仍偏开的源允许更多依赖子链合并。
- 最新 60 源结果：生成 6 关，耗时约 121 秒，真实 trace `ProcessTier=S=5/A=1`、`Trace Risk=Low 6`；`over10Rate avg=0`、`maxChoices avg=7.33`、`meaningfulUnlockRate avg=0.954`、`stageLockScore avg=0.651`、`lateRegionCount avg=2.33`、`stageGateRate avg=0.373`。
- 当前稳定包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockBalancedDepMerge6Pack.asset`。
- 结论：这是比 120 源 5 关更强的量产信号，说明源排序和依赖合链预筛能显著提高单位扫描产出；但还未达到最终量产，应继续做源 profile/cache 或把 door blocker 前置到 SGP 源生成。

## PressureRead LoopMerge160 Difficulty Production Signal - 2026-06-19

- 已将 StageLock 依赖子链合并从单次尝试升级为最多 3 轮的小循环：每轮合并后重新计算依赖、重新评估可解与过程曲线，只接受不变差或更好的合并结果。
- 合并上限从 56 提到 72，且高潜力源允许更多依赖子链合并；同时新增 `CachedPotential` 源排序模式和 `pressure_read_stage_lock_source_attempt_cache.csv`，记录成功、近似成功、合链潜力和失败原因。
- 最新 160 源结果：生成 11 关，耗时约 780 秒，真实 trace `ProcessTier=S=9/A=2`、`Trace Risk=Low 11`；`over10Rate avg=0`、`maxChoices avg=7.09`、`meaningfulUnlockRate avg=0.95`、`stageLockScore avg=0.667`、`lateRegionCount avg=2.64`、`stageGateRate avg=0.354`。
- 当前 Demo 已改挂冻结稳定包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockLoopMerge160FrozenPack.asset`；对应关卡复制在独立目录 `PressureReadStageLockLoopMerge160Frozen/`，后续试跑不会覆盖。
- 相关报告：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_loopmerge160_notes.md`、`pressure_read_stage_lock_trace_loopmerge160_summary.md`。
- 当前瓶颈：160 源产 11 关，产率约 6.9%；少数样本依赖 `maxChain>90` 的巨链，量产时应作为风险样本过滤或排后。下一步应补 SGP 源结构画像缓存，让源排序在扫描前就能看到 `longChainRate/structureCarrierRate`，并继续把 door/stage 语义前置到源生成。

## StageLock Source Profiling Follow-up - 2026-06-19

- 已新增 SGP 源结构画像脚本：`.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPSourceStructureProfile.ps1`，输出 `sgp_source_structure_profile.csv` 和 `sgp_source_structure_top_hardfit.csv`。
- 当前 SGP 源画像：643 个源，`avgChain=7.122`、`longChainRate=0.199`、`structureCarrierRate=0.435`、`shortChainRate=0.089`；外部 298 seed 参考为 `avgChain=8.829`、`longChainRate=0.314`、`structureCarrierRate=0.378`、`shortChainRate=0.280`。
- 结论：SGP 源并不是结构承载链太少，而是中长链比例明显不足；当前难度主要靠 LoopMerge 后处理把短中链串成结构长链。
- 已将源画像接入 `Build-PressureReadStageLockVariants.ps1` 的源排序和 `CachedPotential` 模式；同口径 160 源仍生成 11 关，说明画像提升了可解释性/稳定性，但没有单独扩大产能。
- 重要负结果：`CachedPotential -ExcludeCachedGenerated` 扫描 160 个高潜力未出货源，生成 0 关；失败集中为 `static curve too open=105`、`weak stage lock=42`。这说明当前瓶颈是源可改造性，不是排序。
- 入口 root merge 已保留为 `-AllowEntryRootMerge` 实验开关，默认关闭；小批验证显示默认开启会让 60 源产量从 6 降到 5。
- 下一步：把 stage/door 语义和中长链比例前置到 SGP 源生成，而不是继续主要靠后处理捞源。

## StageLock Source Enhanced Experiment - 2026-06-19

- 已新增源增强脚本：`.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-StageLockSourceEnhancedVariants.ps1`，在 StageLock 之前轻量合并相邻端点链条，生成中长链比例更高的源候选。
- 强负结果：重度预合链（每源合并 10/18 次）能显著提高中长链比例，但 20 个增强源生成 0 个 StageLock 候选，主要失败为 `no stage-lock orientation`。结论：不感知方向/依赖的暴力拉长会破坏可门锁化空间。
- 温和结果：温和预合链（每源合并 3/6 次）从 60 个高 hard-fit 源生成 80 个增强源，耗时约 62 秒；源平均 `avgChain=7.929 -> 8.635`。
- StageLock over 80 温和增强源：生成 7 关，耗时约 170 秒，真实 trace `ProcessTier=S=6/A=1`、`Trace Risk=Low 7`、`over10Rate avg=0`、`maxChoices avg=8`、`stageLockScore avg=0.675`、`lateRegionCount avg=2.71`、`stageGateRate avg=0.375`。
- 已冻结包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockSourceEnhanced7Pack.asset`；报告：`pressure_read_stage_lock_source_enhanced7_notes.md`。
- 结论：温和源增强是可行信号，但不是最终产能突破。下一步应做方向/依赖感知源增强：先确定潜在 blocker/door，再合并成中长链，而不是只按端点相邻盲合。

## StageLock Dependency-Aware Source Enhancement - 2026-06-20

- 已在 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-StageLockSourceEnhancedVariants.ps1` 增加 `-Mode DependencyAware`，只沿真实逃逸射线 blocker 关系合并相邻端点链条，偏好跨区、带转折、结构承载的中长链，惩罚同侧外圈纯直线互接。
- `Freeze-StageLockCandidatePack.ps1` 新增 `-FrozenLevelIdPrefix`，避免不同冻结包关卡都硬编码成 `loopmerge160_*`。
- 40 个高 hard-fit 源用 DependencyAware 增强，生成 67 个增强源，耗时约 227 秒；源平均 `avgChain=8.005 -> 8.713`，`longChainRate=0.329 -> 0.367`。
- StageLock over 67 DependencyAware 源：生成 5 关，耗时约 172 秒；真实 trace `ProcessTier=S=4/A=1`、`Trace Risk=Low 5`、`over10Rate avg=0`、`maxChoices avg=8.2`、`meaningfulUnlockRate avg=0.955`、`stageLockScore avg=0.683`、`lateRegionCount avg=2.6`、`stageGateRate avg=0.375`。
- 结构指标：5 关平均 `avgChain=9.686`、`longChainRate=0.394`、`structureCarrierRate=0.546`、`straightLikeRate=0.038`，已经接近 298 参考 seed 中高复杂样本的中长结构链区间。
- 已冻结包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockDepAware67Pack.asset`；报告：`pressure_read_stage_lock_depaware67_notes.md`。
- 回连分析：成功源主要来自 `section_unlock`、`dual_zone`、`sweep`，并不是预合链最长的那批；源阶段应保留足够头尾方向空间，StageLock 后验依赖合链再把关键链做长。
- 当前结论：DependencyAware 是高质量难关分支，但还不是满产方案；瓶颈是源增强较慢，且 StageLock 失败仍集中在 `weak stage lock` 和 `no stage-lock orientation`。下一步应缓存/限缩依赖搜索，并把 door/stage 语义前置进 SGP 源生成。

## StageLock Targeted DepAware Production Signal - 2026-06-20

- 已新增 targeted 源筛脚本：`.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-StageLockTargetedSourceFeed.ps1`。它综合源画像、过程压力、family 先验和尝试缓存，优先选择更容易被 StageLock 压成门锁的源，而不是只按 raw hard structure 排序。
- 已新增 trace 后精筛脚本：`.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Select-StageLockHardCandidates.ps1`。它合并候选 CSV 与真实 trace，按 `S/A`、选择曲线、`stageLockScore`、结构链指标和 `MaxPerBase/MaxPerFamily` 选出可组包硬关。
- Targeted feed 从 643 个 SGP 源中选出 109 个源；前排 family 包括 `dense_weave`、`sweep`、`dual_zone`、`outer_shell`、`section_unlock`、`lock_buckle`。
- 用 targeted 前 40 源跑 DependencyAware 增强，生成 68 个增强源，耗时约 229 秒；StageLock over 68 增强源直接生成 12 关，耗时约 130 秒。
- 真实 trace：`ProcessTier=S=11/A=1`、`Trace Risk=Low 12`、`over10Rate avg=0`、`maxChoices avg=7`、`meaningfulUnlockRate avg=0.956`、`stageLockScore avg=0.721`、`lateRegionCount avg=2.92`、`stageGateRate avg=0.399`。
- 结构指标：12 关平均 `avgChain=10.101`、`longChainRate=0.383`、`structureCarrierRate=0.56`、`straightLikeRate=0.038`。
- 已冻结完整候选包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockTargetedDepAware68Pack.asset`；关卡目录 `PressureReadStageLockTargetedDepAware68Frozen/`。
- 已冻结严格精选包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockTargetedDepAware68HardSelectedPack.asset`；关卡目录 `PTDA68Hard/`。精选 8 关，family mix 为 `section_unlock=2`、`dual_zone=2`、`sweep=2`、`lock_buckle=1`、`zig_river=1`，平均 trace choices `3.816`、平均 max choices `7.125`。
- 关键结论：targeted 源选择把 DepAware 产率从 5/67 提升到 12/68，是当前最明确的量产突破。下一步不是盲扫，而是按 targeted feed 多切片扩池，并优化 DepAware 搜索速度。

## StageLock Targeted DepAware Multi-Slice Pool - 2026-06-20

- 已给 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-StageLockSourceEnhancedVariants.ps1` 增加 `-SourceOffset`、`-OutputCsvName`、`-RunLabel`，支持按 targeted feed 切片扩池且不覆盖源增强 CSV。
- 已新增 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Merge-StageLockSelectedCandidates.ps1`，用于合并多个 selected hard CSV，按 `MaxPerBase/MaxPerFamily` 去重并重新 rank。
- 已新增 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Run-StageLockTargetedDepAwareSlice.ps1`，封装单切片流程：DepAware 源增强 -> StageLock -> trace -> hard select -> freeze。
- 第二切片验证：targeted feed `SourceOffset=40`、`SourceLimit=40` 生成 65 个增强源，StageLock 产出 2 关，trace `S=2`、`Low risk=2`、`maxChoices avg=7`、`stageLockScore avg=0.752`。产率低于第一切片，但质量可作为补充。
- 已合并第一切片 8 关精选与第二切片 2 关精选，形成 10 关多切片硬关池：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockTargetedDepAwareMergedHard10Pack.asset`；冻结目录 `PTDAMerge10/`。
- Merged Hard10 指标：family mix 为 `dual_zone=2`、`section_unlock=2`、`sweep=2`、`dense_weave=1`、`zig_river=1`、`lock_buckle=1`、`maze_long_chain=1`；`traceAvgChoices avg=3.835`、`traceMaxChoices avg=7.1`、`stageLockScore avg=0.741`、`avgChain avg=9.795`、`structureCarrierRate avg=0.574`。
- 关键结论：前 40 targeted 源是高产黄金切片；后续切片能补多样性但需要更强前筛或性能优化，不宜简单线性扫完整 109 源。

## StageLock LongChainBias Hard-Lane Signal - 2026-06-20

- 已复制完整外部 298 seed 参考到 `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/ReferenceSeeds/ArrowForge298Full_20260618_100431/`，仅用于结构画像/指标校准，不混入正式生成包。
- 已重跑 `Build-ReferenceSeedStructureProfile.ps1` 完整 298 画像：参考平均 `avgChain=8.829`、`longChainRate=0.314`、`veryLongChainRate=0.153`、`maxChain=39.973`；top complex 样本约 `avgChain=12-15`、`longChainRate=0.5+`、`veryLongChainRate=0.25+`。
- `Build-StageLockTargetedSourceFeed.ps1` 新增 `Balanced/HighYield/ReferenceLong` preset。`HighYield` 严控源开放度；`ReferenceLong` 偏向 298 seed 的中长链画像。
- `Build-PressureReadStageLockVariants.ps1` 新增 `-LongChainBias`：在过程曲线不变差时允许更强依赖合链，目标是增加复杂长链而不是放开无脑外出口。
- `Build-StageLockSourceEnhancedVariants.ps1` 输出文件名已改为短 hash，修复长路径失败；`Merge-StageLockSelectedCandidates.ps1` 会重新归一化已有 `selectionBaseKey`，避免不同 run label 的同源重复。
- HighYield mini 不开 bias：12 源 -> 22 增强源 -> 4 关，`S=4`，`avgChoices=3.988`，`maxChoices=7.75`，`avgChain=9.883`，`longChainRate=0.396`。
- HighYield mini 开 `LongChainBias`：产率仍 4 关，`S=4`，`avgChoices=3.96`，`maxChoices=7.75`，`avgChain=11.408`，`longChainRate=0.45`，说明 bias 对结构质量正向且不伤头部产率。
- ReferenceLong + `LongChainBias`：12 源 -> 2 关，`S=2`，`avgChain=13.21`，`longChainRate=0.477`，更接近参考 top complex，但产率低，适合作为补充 lane。
- HighYield 后 19 源 + `LongChainBias` 仍只出 1 关；结论：bias 提升结构复杂度，但不解决 targeted feed 后段源太开放导致的产率问题。
- 当前冻结复杂长链小样包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockLongBias5Pack.asset`，冻结目录 `PTDALongBias5/`。
- LongBias5 指标：5 关，family mix `maze_long_chain=1`、`section_unlock=1`、`dual_zone=2`、`sweep=1`；`traceAvgChoices avg=3.98`、`traceMaxChoices avg=7.6`、`avgChain avg=11.173`、`longChainRate avg=0.446`、`veryLongChainRate avg=0.184`、`structureCarrierRate avg=0.626`。

## StageLock Symmetry Expansion Hard Pool - 2026-06-20

- 已新增 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-StageLockSymmetrySourceVariants.ps1`，对已验证高产源生成 `FlipX/FlipY/Rot180` 几何变体 feed，不改核心 SGP/StageLock 逻辑。
- 已新增 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Run-StageLockSymmetryExpansionSlice.ps1`，封装 symmetry feed 生成 + `DependencyAware + StageLock + trace + hard select + freeze` 的单切片流程。
- HighYield 头部 12 源 -> 36 symmetry source variants；继续走 `DependencyAware + LongChainBias + trace` 后产出 7 个 hard selected，全部 `ProcessTier=S`。
- Symmetry 7 关指标：`traceAvgChoices avg=4.191`、`traceMaxChoices avg=7.857`、`meaningfulUnlockRate avg=0.925`、`avgChain avg=10.955`、`longChainRate avg=0.472`、`structureCarrierRate avg=0.593`。
- HighYield 尾部 19 源 -> 57 symmetry source variants -> 96 enhanced sources -> 2 个 hard selected，全部 `ProcessTier=S`；但产率远低于头部源，说明 symmetry 能补充供给但不能救开放度过高的尾部源。
- 尾部 symmetry 指标：`traceAvgChoices avg=4.53`、`traceMaxChoices avg=7.5`、`meaningfulUnlockRate avg=0.896`、`avgChain avg=9.829`、`longChainRate avg=0.405`、`structureCarrierRate avg=0.629`。
- 已修复 `Merge-StageLockSelectedCandidates.ps1` 的跨命名去重：同一 source hash 不管来自 `_a4619b94` 还是 `_ha4619b94` 都统一为 `src_hash_a4619b94`。
- 已将 `TargetedDepAwareMergedHard10` 与 `LongBias/Symmetry` 结果统一 merge，形成当前最大去重硬关候选池：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction15Pack.asset`，冻结目录 `PTDAHardProduction15/`。
- HardProduction15 指标：15 关，family mix `lock_buckle=1`、`sweep=3`、`dual_zone=4`、`maze_long_chain=1`、`zig_river=1`、`section_unlock=4`、`dense_weave=1`；`traceAvgChoices avg=3.918`、`traceMaxChoices avg=7.267`、`stageLockScore avg=0.705`、`avgChain avg=10.366`、`longChainRate avg=0.409`、`structureCarrierRate avg=0.59`。
- 当前最大去重硬关候选池已更新为 `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction16V2Pack.asset`，冻结目录 `PTDAHardProduction16V2/`。
- HardProduction16V2 指标：16 关，family mix `lock_buckle=1`、`sweep=3`、`dual_zone=4`、`maze_long_chain=2`、`zig_river=1`、`section_unlock=4`、`dense_weave=1`；`traceAvgChoices avg=3.938`、`traceMaxChoices avg=7.188`、`stageLockScore avg=0.702`、`avgChain avg=10.125`、`longChainRate avg=0.401`、`structureCarrierRate avg=0.589`。
- 当前结论：symmetry expansion 是有效供给放大器，但会带来一定结构同源性；适合作为 top HighYield 源的补充扩池，最终仍需要 source-hash 去重、family cap 和人工视觉筛选。

## StageLock HardProduction23V3 - 2026-06-20

- 当前最佳硬关候选池已更新为 `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction23V3Pack.asset`，冻结目录 `PTDAHardProduction23V3/`。
- 输入 CSV：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_pool_v3.csv`。
- V3 共 23 关，family mix：`section_unlock=6`、`dual_zone=6`、`lock_buckle=4`、`sweep=3`、`maze_long_chain=2`、`dense_weave=1`、`zig_river=1`。
- V3 指标：`traceAvgChoices avg=3.635`、`traceMaxChoices avg=6.652`、`stageLockScore avg=0.727`、`avgChain avg=11.168`、`longChainRate avg=0.440`、`structureCarrierRate avg=0.600`。
- 新增有效扩产路线：从 HardProduction16V2 的 16 个成功 enhanced source 构建 `stage_lock_success_enhanced_source_feed_v1.csv`，做二级 symmetry/DependencyAware/LongChainBias 后产出 18 个 StageLock 候选并精选 12 个；与 V2 合并去重后新增 7 关。
- 重要负结果：HighYield 头部 12 源加 `RandomAttemptsPerLevel=60` 没有增加产量，只复现原 7 关；unprofiled Direct/NoMask head80 trace 很开放，StageLock HighYield/ReferenceLong 小样均为 0 关。
- 新增诊断：`Build-SGPSourceStructureProfile.ps1` 已加入 `gatePotentialScore/gateLateRegionCount` 等前置指标，但当前 gate 分布偏宽，只能辅助排除明显差源，不能单独预测出货。
- 第二轮自举已跑通：从 V3 的 23 个成功 source 生成 69 个 symmetry source、120 个 enhanced source、24 个 StageLock candidates，严格精选 15 个；与 V3 合并后得到 `HardProduction31V4BootstrapPack`。
- V4Bootstrap 路径：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction31V4BootstrapPack.asset`，冻结目录 `PTDAHardProduction31V4Bootstrap/`。
- V4Bootstrap 指标：31 关，`traceAvgChoices avg=3.337`、`traceMaxChoices avg=5.968`、`stageLockScore avg=0.776`、`avgChain avg=12.120`、`longChainRate avg=0.470`、`structureCarrierRate avg=0.607`。
- V4Bootstrap 风险：family 明显集中在 `lock_buckle=8`、`section_unlock=8`、`dual_zone=8`，比 V3 更硬但同源感风险更高；它适合作为 hard-production preview，需要人工看图后再决定是否作为正式池。
- 少数 family 自举负结果：从 V3 中筛 `sweep/maze_long_chain/dense_weave/zig_river` 7 个成功源，生成 21 个 symmetry source、39 个 enhanced source，但产出 0 个 StageLock candidates；主要失败为 `no stage-lock orientation` 和 `weak stage lock`。
- 下一步建议：如果追求最终多样性，不能继续靠 symmetry 自举少数 family，应该在源生成层给 `sweep/maze/dense/zig` 补 family-specific door/stage 语义；如果追求特难/极难池，优先人工评估 V4Bootstrap 的同源感和视觉质量。

## StageLock True Hard Mass Production - 2026-06-20

- 已把外部 298 seed 的 LevelDefinition 本体复制到 `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/ReferenceSeeds/ArrowForge298FullLevels_20260618_100431/`，仅用于结构/过程画像参考，不混入正式生成包。
- 已对参考复杂 top40 跑真实过程 trace：`reference_seed_top_complex40_trace_metrics.csv`，并输出对比 `reference_vs_stage_lock_hard_pool_compare.csv`。
- 关键对比：参考 top40 平均 `chains=71.3`、`avgChoices=5.26`、`maxChoices=11.98`、`stageLockScore=0.389`；其中 `S/A` 子集平均 `chains=50.2`、`avgChoices=4.05`、`maxChoices=8.15`、`stageLockScore=0.517`。我们的 V4Bootstrap 平均 `chains=34.0`、`avgChoices=3.34`、`maxChoices=5.97`、`stageLockScore=0.776`。
- 结论：我们的过程难度已经比参考复杂样本更紧，但最终链组规模偏小；参考可学的是“50 链左右 + avgChain≈12 + 可控入口”，不是简单放开更多开口。
- 已新增 `Build-StageLockTargetedSourceFeed.ps1 -Preset HardXL`。HardXL feed 22 源平均 `chains=95`、`sourceOpeners=11.86`、`avgChain=7.60`、`longChainRate=0.282`，但 `DependencyAware + StageLock` 产出 0；放大入口 root merge 后仍 0，失败集中在 `no stage-lock orientation/weak stage lock/static curve too open`。
- 决策：90 链级别大源不能靠当前 StageLock 后处理硬压；大尺寸硬关需要在 SGP 源生成阶段前置 stage/door 语义。
- 已新增 `Select-StageLockHardCandidates.ps1 -MaxPerStructureSignature`，用粗结构签名限制镜像/旋转同构候选。
- 已从历史高链成功候选抽取 `stage_lock_success_highchain_source_feed_v1.csv`，跑 `sym_highchain12`：12 源 -> 36 symmetry -> 72 enhanced -> 16 StageLock candidates，trace 全部 `S/Low`；严格 structure signature cap 后选出 2 个长链补充候选。
- 已形成 V5Preview：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction33V5PreviewPack.asset`，冻结目录 `PTDAHardProduction33V5Preview/`。
- V5Preview 指标：33 关，`traceAvgChoices avg=3.335`、`traceMaxChoices avg=6.03`、`stageLockScore avg=0.766`、`chains avg=34.06`、`avgChain avg=12.03`、`longChainRate avg=0.472`、`structureCarrierRate avg=0.606`。
- 当前报告：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_true_hard_mass_notes.md`。
- 下一步：继续可量产时，主线用 V3/V4 成功源自举；少量引入 highchain bootstrap 作为复杂长链风格；真正提升到 50+ 链的硬关，需要做 `StageDoorSGP`，在源生成阶段生成入口区、延迟区、门锁链和跨区 blocker。

## StageDoor HardMid / Minority Follow-up - 2026-06-20

- 已在 `Build-StageLockSourceEnhancedVariants.ps1` 的 `StageDoor` 模式上验证合法源增强链路：增强后先跑真实 trace，要求 missing/failed=0，再进入 StageLock。
- HardMid 小批：23 源 -> 43 合法 StageDoor 源 -> 3 个 StageLock 候选，全部真实 trace `S`。指标约 `avgChoices=3.16-3.97`、`maxChoices=6-7`、`avgChain=10.06-12.42`、`longChainRate=0.472-0.528`。
- HardMidWide 小批：43 源 -> 77 合法 StageDoor 源 -> 4 个 StageLock 候选，全部 `S`；严格精选后留下 `section_unlock=2`、`lock_buckle=1`。
- Minority success lane：7 个少数 family 成功源 -> 13 合法 StageDoor 源 -> 4 个候选，全部 `S`；严格精选后留下 `maze_long_chain=1`、`dense_weave=1`，用于补 V5 中较少的结构方向。
- 已冻结独立对照包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockStageDoorAdditions5Pack.asset`，冻结目录 `PTDAStageDoorAdditions5/`，CSV 为 `pressure_read_stage_lock_stage_door_additions5.csv`。
- StageDoorAdditions5 family mix：`section_unlock=2`、`lock_buckle=1`、`maze_long_chain=1`、`dense_weave=1`；过程指标约 `avgChoices=3.16-3.72`、`maxChoices=6-7`、`nearSolveRunMax=1-2`、`avgSolveJumpDistance≈8-10`。
- 合并到 V5Preview 时，严格 source hash 去重后没有扩大总池，说明这批更适合作为“同父本高质量替换/看图候选”，不是原始产能突破。
- 已修复 `Select-StageLockHardCandidates.ps1`：`Get-BaseKey` 识别 StageDoor 短 hash（`hXXXXXXXX`），`Get-FamilyKey` 识别 StageDoor 短名 `_section_hXXXXXXXX`、`_lock_hXXXXXXXX`，避免精选阶段重复同父本或误分 family。
- 当前判断：StageDoor 源增强是有效的 quality/variant lane；真正把硬关规模推到 50+ 链，仍需要在 SGP 源生成阶段前置 door/stage 语义，而不是依赖后处理硬压大源。

## StageDoor Minority Symmetry Lane9 - 2026-06-20

- 已验证少数 family 的可复用扩池链路：`minority success feed -> symmetry source -> StageDoor source -> StageLock -> trace -> selected merge`。
- 注意：`Build-StageLockSymmetrySourceVariants.ps1` 的 `-Transforms` 用 `powershell -File` 直接传多个值时只吃第一个，需用 `& .\script.ps1 -Transforms @('FlipX','FlipY','Rot180')` 显式数组调用。
- 本轮结果：7 个 minority success 源 -> 21 个 symmetry 源 -> 39 个合法 StageDoor 源 -> 6 个 StageLock 候选，真实 trace 全部 `S`。
- 严格精选后从 symmetry lane 保留 4 个候选；与 `StageDoorAdditions5` 合并形成 `StageDoorLane9`。
- 已冻结包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockStageDoorLane9Pack.asset`，冻结目录 `PTDAStageDoorLane9/`，CSV 为 `pressure_read_stage_lock_stage_door_lane9.csv`。
- StageDoorLane9 family mix：`dense_weave=4`、`maze_long_chain=2`、`section_unlock=2`、`lock_buckle=1`。
- StageDoorLane9 指标：`traceAvgChoices avg=3.636`、`traceMaxChoices avg=6.889`、`traceStageLockScore avg=0.661`、`avgChain avg=10.640`、`longChainRate avg=0.437`、`structureCarrierRate avg=0.583`。
- Demo 场景已挂到 `StageDoorLane9Pack`（guid `584b811e36984ef0a2dfe01d75655b12`），用于人工看图验证 dense/maze/section/lock 的结构质量。
- 当前判断：`minority symmetry + StageDoor` 是正向的少数结构补强路线，比单纯 StageDoorAdditions5 更接近可复用扩池；但它仍主要依赖已证明成功父本，属于 variety/replace lane，不是 50+ 链原始产能突破。

## Reusable StageDoor Symmetry Pipeline - 2026-06-20

- 已新增可复用脚本：`.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Run-StageLockStageDoorSymmetrySlice.ps1`。
- 脚本流程：symmetry source -> StageDoor source -> 中间源 trace 合法性校验 -> StageLock -> final trace -> hard select -> freeze pack。
- 关键安全点：中间 StageDoor 源 trace 如出现 missing/failed 会直接 throw，避免 HardXL 早期那种非连续链条假阳性流入候选。
- 回归验证 `sdsym_minority_r1`：7 minority success 源 -> 21 symmetry 源 -> 39 合法 StageDoor 源 -> 6 candidates -> 4 selected，复现手工 Lane9 路线。
- 高链验证 `sdsym_highchain_r1`：12 highchain success 源 -> 36 symmetry 源 -> 72 合法 StageDoor 源 -> 1 candidate -> 1 selected。结论是 highchain 在 StageDoor symmetry 下可用但低产，适合作为少量长链补充，不适合默认产线。
- 已合并形成 `StageDoorPipeline10`：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockStageDoorPipeline10Pack.asset`，冻结目录 `PTDAStageDoorPipeline10/`，CSV 为 `pressure_read_stage_lock_stage_door_pipeline10.csv`。
- StageDoorPipeline10 family mix：`dense_weave=4`、`maze_long_chain=2`、`section_unlock=2`、`lock_buckle=1`、`other=1`。
- StageDoorPipeline10 指标：`traceAvgChoices avg=3.697`、`traceMaxChoices avg=6.900`、`traceStageLockScore avg=0.665`、`avgChain avg=10.631`、`longChainRate avg=0.440`、`structureCarrierRate avg=0.583`。
- Demo 仍挂 `StageDoorLane9Pack`，因为 Pipeline10 多出的 highchain 样本偏松，先作为 review addition，不默认替换当前看图入口。

## StageDoor Symmetry V3 Proven Production24 - 2026-06-20

- 已用可复用脚本跑更宽的 V3 proven success feed：
  - `sdsym_v3head12_r1`：12 个 V3 proven 源 -> 36 symmetry 源 -> 72 合法 StageDoor 源 -> 18 candidates -> 8 selected。
  - `sdsym_v3tail11_r1`：11 个 V3 proven 源 -> 33 symmetry 源 -> 63 合法 StageDoor 源 -> 18 candidates -> 8 selected。
- 两个切片均达到 `StageLockTargetCount=18`，说明 `V3 proven source + symmetry + StageDoor` 是目前最明确的可重复产线信号。
- 已合并 `StageDoorPipeline10 + v3head12 + v3tail11`，按 source hash 去重后得到 24 关，而不是目标 26 关。
- 当前 StageDoor 生产池：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockStageDoorProduction24Pack.asset`，冻结目录 `PTDAStageDoorProduction24/`，CSV 为 `pressure_read_stage_lock_stage_door_production26.csv`。
- Production24 family mix：`section_unlock=7`、`lock_buckle=5`、`dense_weave=4`、`dual_zone=4`、`maze_long_chain=3`、`other=1`。
- Production24 指标：`traceAvgChoices avg=3.516`、`traceMaxChoices avg=6.250`、`traceStageLockScore avg=0.717`、`avgChain avg=11.980`、`longChainRate avg=0.488`、`structureCarrierRate avg=0.620`。
- Demo 场景已切到 `StageDoorProduction24Pack`（guid `eb08fa43e3fd419788bff811b869b74c`）作为当前完整生产池看图入口。
- 当前判断：Production24 是目前最强的 StageDoor-symmetry 量产信号，已经能批量生成低选择、长链、阶段锁硬关；但它仍依赖 proven V3 parents，不能视为 raw 50+ chain source generation 已解决。

## HardProduction45V6StageDoorStrict - 2026-06-20

- 已将 `V5Preview` 与 `StageDoorProduction24` 做 source-hash/family 去重合并，验证 StageDoor 是否带来真实新增容量。
- 合并诊断：
  - `MaxPerFamily=8`：41 关，V5=27，StageDoor=14。
  - `MaxPerFamily=10`：47 关，V5=30，StageDoor=17。
  - `MaxPerFamily=12`：53 关，V5=31，StageDoor=22。
- 当前选择 `MaxPerFamily=10` 的 47 关池作为基础，再应用 strict filter：`traceMaxChoices <= 8` 且 `traceStageLockScore >= 0.60`，去掉两个风险 sweep，得到 45 关。
- 当前最佳审查包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction45V6StageDoorStrictPack.asset`，冻结目录 `PTDAHardProduction45V6StageDoorStrict/`，CSV 为 `pressure_read_stage_lock_v5_plus_stagedoor_prod24_cap10_strict45.csv`。
- Strict45 输入贡献：V5=28，StageDoor=17，说明 Production24 带来真实新增容量，不只是替换旧父本。
- Strict45 family mix：`section_unlock=10`、`dual_zone=10`、`lock_buckle=10`、`dense_weave=5`、`maze_long_chain=5`、`other=3`、`sweep=1`、`zig_river=1`。
- Strict45 指标：`traceAvgChoices avg=3.356`、`traceMaxChoices avg=5.956`、`traceStageLockScore avg=0.755`、`avgChain avg=12.047`、`longChainRate avg=0.475`、`structureCarrierRate avg=0.610`。
- Demo 场景已切到 `HardProduction45V6StageDoorStrictPack`（guid `58361881dd3a493299a1f3f022c45f4a`）作为当前主审查入口。
- 当前判断：Strict45 是目前最适合人工审查的 hard candidate pool；它已经满足“低选择 + 长链 + 阶段锁 + 可重复流水线”的候选池目标，但 raw 50+ chain 新源生成仍未完成。

## HardProduction52V7RefComplexLong - 2026-06-20

- 已新增 `Build-StageLockTargetedSourceFeed.ps1 -Preset ReferenceComplex`，参考 298 seed 的复杂长链画像筛源，偏向 `avgChain/longChainRate/structureCarrierRate` 更高且 straight-like 风险较低的源。
- 已给 `Select-StageLockHardCandidates.ps1` 增加可配置结构门槛：`MaxTraceMaxChoices`、`MinTraceStageLockScore`、`MinAvgChain`、`MinLongChainRate`、`MinStructureCarrierRate`、`MaxCandidateMaxChain`、`MaxStraightLikeRate`。
- 已给 `Run-StageLockStageDoorSymmetrySlice.ps1` 增加 `StageDoorVariantsPerSource` 和上述精选阈值透传。
- 24 个 `ReferenceComplex` 源在主项目存在但 worktree 仅有 2 个，已把这 24 个源 asset 和 `.meta` 从主项目复制进 `.worktrees/sgp-rhythm-lab` 同路径，供当前实验可复现使用。
- 负结果：`ReferenceComplex` 全量 12 源 + 4 变体 + 120 StageDoor 源在 StageLock 阶段超时；只取最长源切片产出 0，失败集中为 `no stage-lock orientation`。结论是源不能先被硬拉太长，长链必须保留 door/stage 可定向空间。
- 正结果：按 4 源小切片、`StageDoorVariantsPerSource=3` 跑完整 24 源，得到 `ReferenceComplexLong7`：7 关，family mix 为 `sweep=2`、`section_unlock=2`、`outer_shell=1`、`maze_long_chain=1`、`dense_weave=1`；代表样本达到 `avgChain=15.036`、`longChainRate=0.536`、`structureCarrierRate=0.750`、`traceMaxChoices=5`。
- 已将 `Strict45 + ReferenceComplexLong7` 合并成当前 V7 审查包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction52V7RefComplexLongPack.asset`，冻结目录 `PTDAHardProduction52V7RefComplexLong/`。
- V7 指标：52 关，贡献 `Strict45Base=45`、`ReferenceComplexLong=7`；`traceAvgChoices avg=3.379`、`traceMaxChoices avg=5.962`、`traceStageLockScore avg=0.747`、`avgChain avg=12.100`、`longChainRate avg=0.479`、`veryLongChainRate avg=0.232`、`structureCarrierRate avg=0.623`。
- V7 family mix：`section_unlock=12`、`dual_zone=10`、`lock_buckle=10`、`dense_weave=6`、`maze_long_chain=6`、`sweep=3`、`other=3`、`outer_shell=1`、`zig_river=1`。
- Demo 场景已切到 `HardProduction52V7RefComplexLongPack`（guid `e7900fa8cf964e80a5416e726b7d841e`）。
- 当前判断：`ReferenceComplex` 是低产精品补充线，不适合作为主产线；主产线仍是 Strict/StageDoor proven route。下一步真正突破应在源生成阶段直接生成 StageDoor/StageLock-ready 的门区、延迟区、跨区 blocker 和依赖感知中长链。

## StageGateSearch + MidGate V8 Probe - 2026-06-20

- 已给 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` 增加 `-StageGateSearch`：生成阶段枚举入口候选和根链组合，并用真实 `Evaluate-Orientation` 选择更低选择、更晚开区域、更像阶段门锁的方向。
- 已给 `Run-StageLockStageDoorSymmetrySlice.ps1` 透传 `-StageGateSearch`，报告会固定输出 `Stage gate search: True/False`。
- 负结果：`GateStrong` 静态门很多，但不等于真实解题门锁；30 个 `GateStrong` 源在原验收下仍 0 产出，失败集中为 `weak stage lock`。
- 正结果：加入 `StageGateSearch` 和“紧曲线型难关”验收后，`stage_door_source_sdsym_hm36gate_v1.csv` 的 30 个源产出 3 个 36+ 链候选，真实 trace 全部 `S/Low`。
- V8 midgate 3 关指标：
  - `chains=41/48/42`
  - `traceAvgChoices=4.02/5.19/3.62`
  - `traceMaxChoices=6/9/6`
  - `avgChain=11.878/13.396/11.595`
  - `longChainRate=0.488/0.500/0.524`
  - `structureCarrierRate=0.561/0.667/0.548`
- 已冻结独立小包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardMidGateTight3V8Pack.asset`，冻结目录 `PTDAHardMidGateTight3V8/`。
- 已合并 `V7 52 + V8 midgate 3` 得到审查 probe 包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction55V8ProbeMidGatePack.asset`，冻结目录 `PTDAHardProduction55V8ProbeMidGate/`，CSV 为 `pressure_read_stage_lock_hard_production_v8_probe55_with_midgate.csv`。
- V8Probe55 指标：55 关，`traceAvgChoices avg=3.428`、`traceMaxChoices avg=6.018`、`traceStageLockScore avg=0.735`、`avgChain avg=12.111`、`longChainRate avg=0.480`、`structureCarrierRate avg=0.621`。
- Demo 场景已切到 `HardProduction55V8ProbeMidGatePack`（guid `780312a1e18649d5aa72f2ebc07420ea`）。
- 当前判断：`StageGateSearch` 是有效的中链保留突破，但产率仍低；它适合作为 `MinOutputChains>=36` 的补充搜索档。真正大规模扩到 100+ 精品硬关，仍要继续扩大 proven/ReferenceComplex 源切片，或在源生成阶段前置 gate/door 语义。

## RefComplexSalvage3 / V9Probe58 - 2026-06-20

- 已系统扫描所有 `pressure_read_stage_lock_sdsym_refcomplex*_selected_hard*.csv`，确认已精选 refcomplex 行均已被 V8 source-hash 覆盖，没有漏合并。
- 进一步扫描所有 `pressure_read_stage_lock_sdsym_refcomplex*_candidates.csv`，排除 V8 已有 source hash 后，用窄门槛回收候选：`avgChoices<=4.3`、`maxChoices<=8`、`stageLockScore>=0.58`、`avgChain>=9.3`、`longChainRate>=0.42`、`structureCarrierRate>=0.48`、`straightLikeRate<=0.18`。
- Salvage 找到 5 个去重候选，真实 trace 全部 `S/Low`；tight process 为 `S=1`、`A=4`，平均 `stageGateRate=0.491`，外圈直链连续风险为 0。
- 严格精选 `MaxPerFamily=2`、`MaxPerStructureSignature=1` 后保留 3 个：`outer_shell=2`、`dual_zone=1`。
- 已冻结小包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockRefComplexSalvage3V9Pack.asset`，冻结目录 `PTDARefComplexSalvage3V9/`。
- 已合并 `V8Probe55 + RefComplexSalvage3` 得到当前审查包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction58V9ProbeRefSalvagePack.asset`，冻结目录 `PTDAHardProduction58V9ProbeRefSalvage/`。
- V9Probe58 指标：58 关，`traceAvgChoices avg=3.418`、`traceMaxChoices avg=6.086`、`traceStageLockScore avg=0.735`、`chains avg=33.121`、`avgChain avg=12.016`、`longChainRate avg=0.479`、`structureCarrierRate avg=0.616`。
- Demo 场景已切到 `HardProduction58V9ProbeRefSalvagePack`（guid `9cced90813894d7581ab21625c4921d3`）。
- 报告：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v9_refcomplex_salvage_notes.md`。
- 当前判断：Salvage 是有效的“已生成候选再精筛”补充 lane，可少量回收低选择、高阶段感但结构指标刚好低于旧门槛的样本；它不解决原始产能，下一步仍应继续 proven/ReferenceComplex 小切片和 source-generation gate/door 语义。

## StageLock Salvage Tooling - 2026-06-20

- 已新增 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Select-StageLockSalvageCandidates.ps1`，把 RefComplex salvage 的临时扫描沉淀为可复用工具。
- 用法要点：必须传入 `ExistingPoolCsv`，脚本会从已有池抽取 source hash 并排除同源候选；候选来源可用 `CandidateGlob` 或 `CandidateCsvs` 指定；输出包括 selected CSV、rejected CSV 和 summary。
- 回归验证：以 `pressure_read_stage_lock_hard_production_v8_probe55_with_midgate.csv` 为已有池、扫描 `pressure_read_stage_lock_sdsym_refcomplex*_candidates.csv`，从 28 行中稳定回收 5 个去重候选 source hash。
- 继续验证：这 5 个候选重新跑 `Build-SGPRhythmTrace.ps1` 后全部成功 trace，`Select-StageLockHardCandidates.ps1` 在同等硬关阈值下精选 3 个，可接入 freeze/merge 流水线。
- 注意：trace 脚本在 worktree 中要同时传 `-SourceRoot` 和 `-OutputRoot` 为当前 worktree 路径，否则会去主项目读 asset 导致全部 missing/failed。
- 当前工具输出：
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_refcomplex_salvage_tool_candidates.csv`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_refcomplex_salvage_tool_trace_metrics.csv`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_refcomplex_salvage_tool_selected_hard.csv`

## AllHistorySalvage8 / V10Probe66 - 2026-06-20

- 用 `Select-StageLockSalvageCandidates.ps1` 扫描全部历史 `pressure_read_stage_lock*_candidates.csv`，以 V9Probe58 为已有池排除 source hash。
- 筛选门槛：`avgChoices<=4.4`、`maxChoices<=8`、`stageLockScore>=0.58`、`avgChain>=9.3`、`longChainRate>=0.40`、`structureCarrierRate>=0.48`、`straightLikeRate<=0.18`。
- 结果：196 行历史候选中回收 42 个未入 V9 的去重 source；真实 trace 42/42 成功，全部 process `S`，tight process 为 `S=20`、`A=14`、`B=3`、`Drop=5`。
- 严格精选后得到 8 个补充样本：`section_unlock=4`、`dual_zone=2`、`maze_long_chain=1`、`other=1`。
- 已冻结补充小包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockAllHistorySalvage8V10Pack.asset`，冻结目录 `PTDAAllHistorySalvage8V10/`。
- 已合并 `V9Probe58 + AllHistorySalvage8` 得到当前审查包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction66V10ProbeAllHistorySalvagePack.asset`，冻结目录 `PTDAHardProduction66V10ProbeAllHistorySalvage/`。
- V10Probe66 指标：66 关，`traceAvgChoices avg=3.438`、`traceMaxChoices avg=6.136`、`traceStageLockScore avg=0.732`、`chains avg=33.273`、`avgChain avg=11.956`、`longChainRate avg=0.481`、`veryLongChainRate avg=0.230`、`structureCarrierRate avg=0.617`、`straightLikeRate avg=0.047`。
- Demo 场景已切到 `HardProduction66V10ProbeAllHistorySalvagePack`（guid `1c2b0c613918484a9ceb89b5e4271b12`）。
- 当前判断：all-history salvage 能低成本扩池并补少量 `maze_long_chain/section/dual`，但仍没有突破 40+ 链产能上限；真正下一步要在 source generation 前置“少量长结构承载链 + gate/door-ready 区域语义”，避免 StageLock 后处理把大源压回 30 多链。

## HardMidWide DoorBalanced StageGate / V11Probe68 - 2026-06-20

- 继续验证 36+ 链难关扩产：`hardmid_wide` 源 + symmetry + StageDoor source enhancement + `StageGateSearch + MinOutputChains=36 + LongChainBias`。
- 负结果：`GateStrong` 在 first micro-slice 上过窄/不稳定；16 源切片超时，4 源切片只到 symmetry 阶段，直接跑 GateStrong StageDoor 未产出 summary/rejected。当前不建议把 GateStrong 作为 hardmid_wide 默认。
- 正结果：改用 `DoorBalanced` 后，两个 4 源微切片各产出 1 个 hard selected：
  - 40 链 maze：`traceAvgChoices=3.6`、`traceMaxChoices=7`、`traceStageLockScore=0.630`、`longChainRate=0.525`、`structureCarrierRate=0.575`。
  - 37 链 dense：`traceAvgChoices=3.0`、`traceMaxChoices=6`、`traceStageLockScore=0.606`、`longChainRate=0.568`、`structureCarrierRate=0.649`。
- 已合并 `V10Probe66 + hardmid_wide DoorBalanced StageGate 2` 得到当前审查包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction68V11ProbeHardMidWideDBStageGatePack.asset`，冻结目录 `PTDAHardProduction68V11ProbeHardMidWideDBStageGate/`。
- V11Probe68 指标：68 关，`traceAvgChoices avg=3.434`、`traceMaxChoices avg=6.147`、`traceStageLockScore avg=0.728`、`chains avg=33.426`、`avgChain avg=11.955`、`longChainRate avg=0.483`、`structureCarrierRate avg=0.617`。
- Demo 场景已切到 `HardProduction68V11ProbeHardMidWideDBStageGatePack`（guid `d6cd7cbd53b445ef9a2b51f9b3e53148`）。
- 当前判断：这是目前最接近“中型长链 hard production”突破的 lane；产率慢，应按 4-8 源小切片跑，不适合一次性大 wrapper。下一步可以继续 hardmid_wide offset 小切片，或把 DoorBalanced 作为默认 hardmid_wide profile 后再优化性能。

## ArchitecturalLinework Constructive02 Probe - 2026-06-20

- 新分支/worktree：`.worktrees/sgp-building-grammar`，用于继续攻克竞品建筑迷宫/建筑线稿方向，不影响 `sgp-rhythm-lab`。
- 新脚本：`.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-ArchitecturalLineworkSGP.ps1`。
- 当前有效切入点从“先摆结构再筛”调整为“反解式线稿构造”：逐根放低弯折墙段，同时确定头尾方向与逃逸射线；后续链避开关键逃逸区，最后用少量 trim 打断依赖环并走真实 Greedy trace。
- 当前 probe 包：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_ArchitecturalLineworkSGP01_ProbePack.asset`，guid `8c13b1e36ad943d4b9a1a1f8a18b2b65`。
- Demo 场景已切到该 probe 包。
- 当前报告：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/architectural_linework_constructive02_report_20260620.md`。
- Constructive02 三关指标：
  - 18x29：19 链，coverage `0.448`，openers `4`，trace avg `4.32`，max `7`，process `S`，tight `A`，extreme `A`，stage `0.638`，lateRegions `3`。
  - 16x27：19 链，coverage `0.477`，openers `4`，trace avg `3.74`，max `6`，process `S`，tight `S`。
  - 20x32：25 链，coverage `0.444`，openers `5`，trace avg `4.96`，max `9`，process `S`，tight `B`。
- 当前判断：这是实质方向变化，已经证明“建筑线稿 + 难度 trace”可以闭环；但仍不是最终生产质量，主要缺口是 coverage/链条数偏低、构造速度慢。
- 下一步建议：在 constructive pass 后增加安全子区域 top-up；用真实依赖环检测替代暴力 trim；目标从 20-30 链/0.45 覆盖逐步升到 45-70 链/0.62+ 覆盖，同时保持 trace S/A。
- 复盘修正：用户人工看图后判定该 constructive 手摆线稿路线完全偏离要求。问题不是单项指标，而是路线本身没有基于已验证 SGP 填充能力，产物稀疏、低级、没有 SGP 关卡质感。该 probe 已从 Demo 撤下，不再作为展示或继续优化方向。
- 新约束：后续建筑迷宫方向必须回到 SGP 基础能力上做结构干预，例如 mask/guide/door/gate/局部约束/筛选/后处理，不再展示纯手摆线段式原型。

## SGP Building Archlike Presentable5 - 2026-06-20

- 当前实验 worktree：`.worktrees/sgp-building-grammar`，branch `codex/sgp-building-grammar`。
- 纠偏后采用 SGP-only 路线：从 643 个既有 SGP 源读取数据，按建筑线稿画像筛选，不再使用纯手摆 constructive 产物。
- 建筑画像筛选指标：高 `lowTurnChainRate`、低 `highTurnChainRate`、较高 `avgSegmentLength/longSegmentRate/thinWallChainRate`，同时保持高覆盖和真实 Trace 可解。
- 全量画像输出：
  - `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_building_root_all_candidates_arch_profile.csv`
  - `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_building_archlike_source_top80.csv`
- 小跑 StageLock 后处理验证：对 top2 建筑画像源低随机尝试 0 产出；结论是本轮不强行后处理，先采用 SGP 源级画像 + Trace + 视觉精选。
- 已冻结可看 probe 包：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SGPBuildingArchlikePresentable5Pack.asset`，guid `2ca1e27c9fd84a56947bd98812e2b5f3`。
- 冻结关卡目录：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/SGPBuildingArchlikePresentable5/`。
- Demo 场景已挂该包。
- 冻结 trace：5/5 成功，`ProcessTier A=3/B=2`，`Trace Risk Low=3/OuterStraightRun=1/TooManyChoices=1`，平均 `avgChoices=5.67`、平均 `maxChoices=10.2`、`over10Rate avg=0.018`。
- 当前判断：这批是“能拿出来看的 SGP 建筑线稿方向 probe”，不是最终突破。优势是密度、覆盖、线稿画像和 SGP 质感回来了；短板是 stage/door lock 不强，仍有两个 B 级样本和少量外圈直线/开放度风险。下一步若继续攻克，应把建筑画像约束前置到 SGP 源生成或 mask/guide 生成阶段，而不是在普通 SGP 源后面硬压 StageLock。

## HardMidWide DoorBalanced Micro / V12Probe72 - 2026-06-20

- 已新增 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Run-HardMidWideDoorBalancedStageGateMicroSlice.ps1`，把 hardmid_wide 中型长链 lane 工具化为可重复微切片流程：`symmetry -> DoorBalanced StageDoor source -> source trace -> chunked StageLock -> candidate trace -> hard select`。
- 已生成可复用小 feed：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_stage_lock_hardmid_wide_existing_next6.csv`，只保留当前 worktree 中存在且长链信号更强的 hardmid_wide 源，避免缺源和低价值 offset。
- 负结果：`hmw_db_sg_o08_l2_v12a` 的 offset 8 源增强后链条语言偏短，两个 chunk 都 0 产；`nomask_style_12_long_corridor` 虽画像强但当前 worktree 缺源，未纳入本轮。
- 正结果：`existing_next6` 三个 2 源微切片共新增 4 个 hard selected：
  - 37 链 dense：`traceAvgChoices=4.14`、`traceMaxChoices=8`、`traceStageLockScore=0.565`、`maxChain=45`、`structureCarrierRate=0.730`。
  - 53 链 maze：`traceAvgChoices=3.72`、`traceMaxChoices=7`、`traceStageLockScore=0.530`、`maxChain=57`、`longChainRate=0.547`。
  - 36 链 section：`traceAvgChoices=2.64`、`traceMaxChoices=5`、`traceStageLockScore=0.640`。
  - 36 链 section：`traceAvgChoices=3.72`、`traceMaxChoices=7`、`traceStageLockScore=0.717`。
- 已合并 `V11Probe68 + hardmid_wide DoorBalanced micro 4` 得到当前审查包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction72V12ProbeHardMidWideDBMicroPack.asset`，冻结目录 `PTDAHardProduction72V12ProbeHardMidWideDBMicro/`。
- V12Probe72 指标：72 关，`traceAvgChoices avg=3.441`、`traceMaxChoices avg=6.181`、`traceStageLockScore avg=0.722`、`chains avg=33.819`、`avgChain avg=11.972`、`longChainRate avg=0.484`、`structureCarrierRate avg=0.620`。
- Demo 场景已切到 `HardProduction72V12ProbeHardMidWideDBMicroPack`（guid `eb1515f4fae24be8a267d18b3a337460`）。
- 当前判断：这是目前第一个可重复的 36+ 链中型长链扩产流程，已经从 2 个样本扩到 6 个新增 hardmid_wide DB StageGate 样本；产率仍慢，但 chunked workflow 让失败不再整批浪费。下一步可以继续找存在的 hardmid_wide/highchain 源做 small feed，或优化 StageLock chunk 性能。

## SGP Building Maze StageLock2 - 2026-06-20

- 当前实验 worktree：`.worktrees/sgp-building-grammar`，branch `codex/sgp-building-grammar`。
- 目标纠偏后采用更高标准：必须 SGP-based，同时过建筑线稿画像与真实 Trace 难度；不再用 Presentable5 的低标准验收。
- 从 643 个既有 SGP 源生成建筑画像，并放宽为 broad top24；再用 StageLock/依赖合链对建筑画像源做方向搜索。
- 有效产出来自建筑画像 top9：2 个 StageLock 变体，其中 `ExtremeBias` 为 0 产出，说明当前可行区间是中高难 S 级，不是 extreme。
- 当前冻结包：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SGPBuildingMazeStageLock2Pack.asset`，guid `67fbd4ef53414f7ba28ea8f2e11b8914`。
- 冻结目录：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/SGPBuildingMazeStageLock2/`。
- Demo 场景已切到该包。
- 冻结 trace：2/2 成功，`ProcessTier S=2`，`Trace Risk Low=1/DependencyFollowRun=1`，`over10Rate avg=0`、`maxChoices avg=8.5`、`stageLockScore avg=0.593`、`lateRegionCount avg=2`、`outerStraightRunMax avg=1`。
- 两个样本：
  - `double_room_lock_tall` 变体：视觉画像更贴近建筑线稿，`openers=7`、`avgChoices=4.31`、`maxChoices=8`、`stageLockScore=0.597`、`lowTurn=0.625`、`avgSegment=3.819`、`thinWall=0.729`。
  - `outer_shell` 变体：过程更硬且长链承载强，`openers=8`、`avgChoices=3.73`、`maxChoices=9`、`stageLockScore=0.590`、但 `highTurn=0.467`，视觉神似度弱于第一关。
- 当前判断：这是比 Presentable5 更接近目标的实质进展，难度达标为 S 级，但数量只有 2，且第一关神似更好、第二关更像密集难关。后续扩产应继续以 `double_room_lock / section / shell` 建筑画像源为主，围绕 StageLock 可定向性做源级 guide，而不是只扩大普通 hard 池。

## HardMidWide Auto Feed / V13Probe75 - 2026-06-20

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已新增 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-HardMidWideMicroSourceFeed.ps1`，用于从 `sgp_source_structure_stage_lock_hardmid_wide.csv` 自动筛选 hardmid_wide 微切片父源。
- 自动 feed 输出：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_stage_lock_hardmid_wide_micro_auto_v13.csv`；排除 V12 next6 后选出 15 个源。
- 正结果：
  - `hmw_db_sg_auto00_v13a` 选出 2 个 37 链 dense_weave hard candidates，真实 trace 分别为 `S/A` 与 `S/S`。
  - `hmw_db_sg_auto02_v13b` 选出 1 个 40 链 maze_long_chain hard candidate，真实 trace 为 `S/A`。
- 负结果：`hmw_db_sg_auto04_v13c` 0 产且主要为 `weak stage lock`；`hmw_db_sg_auto06_v13d` 生成 1 个结构强候选但因 `traceMaxChoices=9` 被 hard select 拒绝；`hmw_db_sg_auto08_v13e` 失败集中为 `dependency too local/static curve too open/weak final chain structure/no stage-lock orientation`；`hmw_db_sg_auto10_v13f` 0 symmetry rows。
- 已合并 `V12Probe72 + hardmid_wide auto feed 3` 得到当前审查包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction75V13HardMidWideAutoPack.asset`，冻结目录 `PTDAHardProduction75V13HardMidWideAuto/`。
- V13Probe75 指标：75 关，`traceAvgChoices avg=3.447`、`traceMaxChoices avg=6.213`、`traceStageLockScore avg=0.717`、`chains avg=33.987`、`avgChain avg=11.967`、`longChainRate avg=0.486`、`veryLongChainRate avg=0.233`、`structureCarrierRate avg=0.620`、`straightLikeRate avg=0.050`。
- Demo 场景已切到 `HardProduction75V13HardMidWideAutoPack`（guid `b13168c7a9744d359dd5c681a44660de`）。
- 复盘报告：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v13_hardmidwide_auto_notes.md`。
- 当前判断：自动源筛路线成立，但静态源评分不够；下一步应建立 StageDoor trace-aware prefilter/cache，把 DoorBalanced 中间源的 `processTier/tightProcessTier/stageLockScore/maxChoices` 反馈到父源排序，减少 auto feed 后半段的无效 StageLock。

## HardProduction76V15 Productive Refit - 2026-06-20

- 当前 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已新增/验证 trace-aware 父源缓存：`hardmidwide_stage_door_trace_cache_v14c.csv`，覆盖 23 个 parent rows；失败总计为 `weakStageLock=49`、`noStageLockOrientation=16`、`weakFinalChainStructure=18`。
- 已对比参考 298 seed 与 SGP hardmidwide 父源：参考 top complex 的 `avgChain/veryLongChainRate` 明显更高，SGP 父源更密但父链偏短。
- 负结果：严格 reference-complex parent feed 选出 6 个父源并复制到 worktree；两个小切片共 27 个 StageDoor 源，0 个 StageLock 候选，失败集中于 `no stage-lock orientation`。
- 正结果：productive refit 从已出货的 StageDoor 源中取 24 个做 `AllowEntryRootMerge + LongChainBias + StageGateSearch`，产出 6 candidates，真实 trace 全部成功，strict selected 4；与 V13 合并去重后得到 76 关，新增 1 个 source hash。
- 当前 Demo activePack 已切到 `SGPRhythmLab_PressureReadStageLockHardProduction76V15ProductiveRefitPack.asset`，guid `f16dd0c7c70147a5b5a47f6514ddda21`。
- 报告：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v15_productive_refit_notes.md`。
- 下一步：把 productive refit 工具化成可复用 pipeline，扩展到全部成功 StageDoor-source history，并修复 StageDoor hash 的 family/base 识别；继续避免直接把过长父源作为默认筛选目标。

## HardProduction78V16 Productive Refit AllHistory - 2026-06-20

- 当前 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已新增脚本：`Build-ProductiveRefitStageDoorSourceFeed.ps1`，用于从历史 StageDoor source CSV 生成未入池 productive refit feed。
- 脚本扫描 44 个 StageDoor source 文件，筛出 739 个 eligible，去重和 family cap 后 selected 24。
- head12 负结果：0 candidates，失败 `no stage-lock orientation=10`、`weak stage lock=2`。
- tail12 正结果：2 candidates -> trace 2/2 成功 -> strict selected 2，均为 `S/A`。
- 已合并 `V15 76 + V16 all-history refit 2` 得到 78 关：`pressure_read_stage_lock_hard_production_v16_probe_refit_allhistory_merge.csv`。
- 当前 Demo activePack 已切到 `SGPRhythmLab_PressureReadStageLockHardProduction78V16ProductiveRefitAllHistoryPack.asset`，guid `cb12779a948d4f95a2381403a6b59013`。
- 报告：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v16_productive_refit_allhistory_notes.md`。
- 下一步：给 feed scorer 加 orientation-risk/cache 惩罚，优先 hardmid36/v3head/v3tail-like medium-long sources；继续以 12-row slice 扩到更多 unique hard candidates。

## SGP Building Grammar UltraLow AntiFollow Probe - 2026-06-20

- 当前实验 worktree：`.worktrees/sgp-building-grammar`，branch `codex/sgp-building-grammar`。
- 本轮目标：基于 SGP/StageLock 主流程尝试 `UltraLowChoice + AntiFollow + 建筑线稿画像` 三重验收，目标把平均可选链压到约 1-2，同时避免 ABC 顺消。
- 已在 `Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` 增加/恢复 `-UltraLowChoiceBias`、静态 `dependencyFollowRunMax/dependencyFollowRate`、`availableTrace/solveOrder`、Ultra pruning、局部 split/rechain 和 rejected/candidate 字段输出。
- 修复关键 bug：`choicePrunedCount > 0` 时必须用 `Write-LevelAssetFromArrows` 写 `$outputLevel`；否则候选 CSV 会显示 pruned 后低 choices，但真实 LevelDefinition 仍是未 pruned 资产，导致真实 Trace 与静态指标不一致。
- 小批验证：当前 worktree 实际存在的 6 个建筑画像源经 `StageGateSearch + UltraLowChoiceBias` 最终 `eligible=0`，未冻结 demo。
- 关键发现：单纯删链可以把 `double_room_lock_a` 压到真实 `openers=2, avgChoices=1.67, maxChoices=3, stageLockScore=0.961`，但 `dependencyFollowRunMax=5, dependencyFollowRate=0.577, nearUnlockRate=0.52`，属于低选择但强顺消，不符合最终目标。
- 当前结论：只靠后处理删链压选择会制造顺消传送带；下一步要前置“源级交错门锁”，保留 2 条左右并行待解队列，让 A 开 B 后 C/D 仍可作为更合理选择。
- 本轮报告：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/building_grammar_ultralow_antifollow_probe_20260620.md`。

## SGP Building Grammar UltraLow Interleaved Probe - 2026-06-20

- 继续在 `.worktrees/sgp-building-grammar` 攻克 `UltraLowChoice + 建筑线稿 + 反 ABC 顺消`。
- 已从主工作树复制 `sgp_building_archlike_broad_source_top24.csv` 缺失的 18 个 SGP 父源 asset/.meta 到当前 worktree；当前 top24 父源 24/24 可读，不改主工作树。
- `Build-PressureReadStageLockVariants.ps1` 已加入顺消段靶向删除/头尾互换、低弯折中长链保护、`nearUnlockRate` 静态评分和更严格 curve pruning。
- `Build-SGPRhythmTrace.ps1` 已加入真实 `avoidableFollowRunMax/Rate`、`forcedFollowRunMax/Rate`、`independentAlternativeRate`，用于区分“被迫门锁跟随”和“无脑 ABC 顺消”。
- `Select-BuildingMazeUltraLowCandidates.ps1` 已使用 forced/avoidable follow 辅助 gate；仍保持默认 `avgChoices<=2.75`，未放水冻结。
- 当前最接近样本：`pressure_read_stage_lock_02_nomask_style_02_snake_spine_6bad3d56`，真实 `S/extreme S`，`avgChoices=2.76`、`maxChoices=4`、`followRunMax=2`、`nearUnlock=0.294`、建筑画像 `lowTurn=0.667/thinWall=0.762`；因 `avgChoices>2.75` 和总 `followRate>0.35` 被 gate 拒绝，未冻结 demo。
- 结论：局部后处理已经能做出近似目标样本，但最终交集仍为空；下一步应做 source-level interleaved parent prefilter/cache，优先找天然保留独立替代选择的父源，而不是继续大批量盲扫。
- 本轮报告：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/building_grammar_ultralow_interleaved_probe_20260620.md`。

## SGP Building Grammar Dual-Spine Trace v0 - 2026-06-20

- 已实现真实 Trace 层 Dual-Spine v0 指标，不改 SGP 生成器：
  - `dualSpineCount`
  - `spineALength/spineBLength`
  - `spineBalance`
  - `spineAlternationRunMax`
  - `crossUnlockRatio`
  - `singleSpineDominanceRate`
  - `spineCoverageRate`
  - `spatialDivergence/Norm`
  - `spinePatternHead`
- `Select-BuildingMazeUltraLowCandidates.ps1` 新增可选 `-EnableDualSpineGate`，默认不影响旧筛选；严格目标为 `balance>=0.65`、`altRun<=2`、`crossUnlockRatio 0.15-0.35`、`singleSpine<=0.55`。
- 用当前 4 个 near-miss 候选验证：`Eligible=0`、`Rejected=4`。全部失败在 `crossUnlock<0.15`、`spineAltRun>2`、`singleSpine>0.55`，说明它们不是交错双门锁，而是顺序双块/单主干传播。
- 代表样本 `snake_spine`：`avgChoices=2.76`、`followRunMax=2`、`nearUnlock=0.294`、建筑画像强，但 `spineAlternationRunMax=10`、`crossUnlockRatio=0`、`singleSpineDominanceRate=0.621`，被正确识别为 near-miss，不冻结 demo。
- 当前破局结论更精确：瓶颈不是 choice、Greedy 或建筑画像单项，而是 current SGP/StageLock 候选缺少跨 spine 互相解锁；下一步应做 `DualSpineSourcePrefilter v0`，优先找天然 `crossUnlockRatio>0` 且 `altRun` 低的父源，再投入昂贵 UltraLow 搜索。
- 本轮报告：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/building_grammar_dual_spine_v0_20260620.md`。

## HardProduction81V17 Productive Retry - 2026-06-20

- 当前 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已将 `Build-ProductiveRefitStageDoorSourceFeed.ps1` 增加 `-PreferOrientableHistory`：复杂长链奖励仍保留，但源本身过开放、refcomplex 头部历史低产会扣分，hardmid/v3head/v3tail/next6 等已证明可出货源会加权。
- V17 productive retry：从 trace-aware HardMidWide feed 中选 2 个高价值父本 retry，生成 4 candidates，真实 trace 4/4 成功，strict selected 3。
- 已合并 V16 78 + 新增 3 得到 81 关：`pressure_read_stage_lock_hard_production_v17_probe_productive_retry_merge.csv`。
- 当前 Demo activePack 已切到 `SGPRhythmLab_PressureReadStageLockHardProduction81V17ProductiveRetryPack.asset`，guid `f4d7ac9e53b44a16b3510dc404a064e9`。
- V17 指标：`traceAvgChoices avg=3.479`、`traceMaxChoices avg=6.284`、`traceStageLockScore avg=0.710`、`avgChain avg=11.921`、`longChainRate avg=0.486`、`veryLongChainRate avg=0.233`、`structureCarrierRate avg=0.622`。
- 负结果：strong-chain broad/mid slices 主要失败为 `no stage-lock orientation`；GateStrong productive retry 超时且无输出，当前不作为默认量产 profile。
- 下一步：增加 orientation-risk cache 到 productive refit feed，继续 2-4 父本小切片扩产，避免大批量盲扫。

## HardProduction82V18 RiskAware - 2026-06-20

- 当前 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已在 `Build-ProductiveRefitStageDoorSourceFeed.ps1` 增加 `-UseStageLockRiskCache`，从历史 StageLock rejected/selected 反馈中学习 exact StageDoor source 的 orientation 风险和成功信号。
- V18 risk-aware feed：以 V17 81 关为排除池，输出 33 个 source；其中 clean no-risk subset 8 个。
- clean8 小切片产出 1 candidate，trace 1/1 成功，strict selected 1：section_unlock `src_hash_daf3723e`，S/S，openers=3，avgChoices=3.41，maxChoices=6，avgChain=10.268，longChainRate=0.415，structureCarrierRate=0.634。
- 已合并 V17 81 + V18 1 得到 82 关：`pressure_read_stage_lock_hard_production_v18_probe_riskaware_merge.csv`。
- 当前 Demo activePack 已切到 `SGPRhythmLab_PressureReadStageLockHardProduction82V18RiskAwarePack.asset`，guid `eae25cb670154be99008e06fba07e26f`。
- V18 指标：`traceAvgChoices avg=3.478`、`traceMaxChoices avg=6.280`、`traceStageLockScore avg=0.708`、`avgChain avg=11.901`、`longChainRate avg=0.486`、`structureCarrierRate avg=0.622`。
- 继续瓶颈：clean risk-free source 仍有 5/8 失败为 `no stage-lock orientation`，后续应继续增强 source orientability 而不是盲目扩 broad scan。

## HardProduction84V19 Base RiskAware - 2026-06-20

- 当前 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已将 Productive Refit 风险缓存从 exact LevelId 升级为 source-hash 级别：`Get-BaseKey(...) -> src_hash_xxxxxxxx`。
- V19 base-risk feed：以 V18 82 关为排除池，输出 33 个 source；source-hash 风险缓存 entries=495，clean no-risk rows=0。
- 低 orientation 风险子集 6 个 source 产出 2 candidates，trace 2/2 成功，strict selected 2，均为 section_unlock S/A。
- 新增样本：`src_hash_d85bd7b9` 与 `src_hash_832593b9`，均 openers=4，avgChoices=4.02，maxChoices=7，avgChain=10.643，longChainRate=0.405，structureCarrierRate=0.571。
- 已合并 V18 82 + V19 2 得到 84 关：`pressure_read_stage_lock_hard_production_v19_probe_base_riskaware_merge.csv`。
- 当前 Demo activePack 已切到 `SGPRhythmLab_PressureReadStageLockHardProduction84V19BaseRiskAwarePack.asset`，guid `d986d60965134488a36ad660426c54c1`。
- V19 指标：`traceAvgChoices avg=3.491`、`traceMaxChoices avg=6.298`、`traceStageLockScore avg=0.708`、`avgChain avg=11.871`、`longChainRate avg=0.484`、`structureCarrierRate avg=0.621`。
- 负结果：少数 family low-risk retry 6 源 0 产，失败为 `weak stage lock=4`、`static curve too open=2`；dense/outer/lock 后续需要源级 stage/door 语义增强，不宜继续盲 retry。

## SGP StageLock V20 HMW TraceAware - 2026-06-20

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 当前 Demo activePack 已切到：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction88V20HMWTraceAwarePack.asset`，guid `f639434991654d708a62b8694514f919`。
- V20 冻结目录：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction88V20HMWTraceAware/`。
- V20 合并 CSV：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v20_probe_hmw_traceaware_merge.csv`。
- V20 指标：88 关，`traceAvgChoices avg=3.502`、`traceMaxChoices avg=6.352`、`traceStageLockScore avg=0.705`、`chains avg=34.795`、`avgChain avg=11.866`、`longChainRate avg=0.485`、`veryLongChainRate avg=0.233`、`structureCarrierRate avg=0.622`、`straightLikeRate avg=0.055`。
- V20 相比 V19 新增 4 个全 trace `S` 的 HardMidWide trace-aware 长链候选，覆盖 `dense_weave`、`maze_long_chain` 和 `sweep/lock`，新增样本平均 `chains=38`、`avgChain=11.764`、`longChainRate=0.520`、`structureCarrierRate=0.640`。
- GateLite minority v21 是负结果：源级能提高长链率但最终 StageLock 0 候选，拒绝集中在 `weak stage lock`、`static curve too open`、`no stage-lock orientation`；该 profile 暂不适合继续盲跑。
- HardMidWide trace-aware 路线是当前正结果：51 个 StageDoor source 中 source trace 为 `S=4/B=1/Drop=46`，最终 c01/c04 产出 4 个严格 hard 候选，c05/c06 有候选但未过 strict select，c02/c03/c07 为 0。注意 source `processTier=S/B` 不是可靠预测器，新增 dense/maze/sweep 多来自 source-trace `Drop` 但具备较强 `stageLockScore/stageGateRate/sourceDoorScore`。下一步应做 chunk-level scorer/prefilter，结合源级门锁潜力、family/source risk cache 和 chunk 历史结果，而不是按 source processTier 盲跳。
- 详细报告：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v20_hmw_traceaware_notes.md`。

- 已给 `Run-HardMidWideDoorBalancedStageGateMicroSlice.ps1` 增加可选 `-UseChunkPrefilter` chunk scorer：结合 source trace、StageDoor 源信号和 `hardmidwide_stage_door_trace_cache_v14c.csv` 的父源历史，输出 `stage_door_source_<slice>_chunk_scores.csv` 并可按 top chunk/score threshold 跳过低价值块。默认不开，旧流程不变。`hmw_prefilter_smoke_v20` smoke 已验证：2 chunk 中跑 top1、跳过低分 chunk，产出 1 个已知强 dense-weave S 候选；该 smoke 不并入正式 V20。
- V21 ProductiveRefit-from-V20 已重建 feed：`stage_door_source_productive_refit_v21_from_v20_feed.csv`，16 个未入 V20 source，family mix 为 `outer_shell=3/maze_long_chain=3/section_unlock=3/dual_zone=3/lock_buckle=2/dense=2`；head4 小切片产 1 个 trace S 候选，但 `longChainRate=0.333`、`structureCarrierRate=0.467`，因 `long chain rate too low` 未过 strict hard select，不并入 V20。`Build-ProductiveRefitStageDoorSourceFeed.ps1` 已默认排除 `smoke` 文件，避免小测污染正式 feed/risk cache。
- V21 ProductiveRefit full probe 已跑完：除 head4 外，c02=0 candidates、c03=2 candidates/0 selected、c04=1 candidate/0 selected；4 个候选真实 trace 全部 `S`，`traceAvgChoices=2.42-3.85`、`traceMaxChoices=6-7`，但 strict reject 为 `long chain rate too low=2`、`avg chain too low=2`。结论：ProductiveRefit 可产“低选择压力硬关”，但当前不满足复杂长链补强 lane，不冻结 V21，V20 仍为当前 demo/review 包。汇总 CSV：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_productive_refit_v21_probe_summary.csv`。

## SGP Building Grammar CPS Interleave v1 - 2026-06-20

- 当前实验 worktree：`.worktrees/sgp-building-grammar`，branch `codex/sgp-building-grammar`。
- 目标已更新为：继续基于 SGP/StageLock 主流程攻克 `UltraLowChoice + 建筑线稿 + 反 ABC 顺消 + 双解锁线互相激活`，不回到手摆/constructive 逻辑。
- 已在 `Build-PressureReadStageLockVariants.ps1` 增加/调整：
  - `-OutputPrefix`：隔离实验 CSV/pack/level id，避免覆盖稳定 preview。
  - `-SourceSortMode CrossPotential` 与 `Measure-CrossPotential`：按潜在跨区互锁边筛父源。
  - `-InterleavedLockBias` 下让 StageGate、随机尝试、合链评估使用 dual-spine trace score。
  - 候选 CSV 增加 `interleaveSolveHead`、`availableTraceHead`，用于定位真实 solve timeline collapse。
  - 依赖合链接受条件增加 interleave 不恶化守门。
  - `InterleaveFlipPasses` 默认改为 0；pair-flip 当前性能过重，只作为显式离线实验。
  - `-EnableInterleaveDominanceTrim` 保留但默认关闭；实测删 dominant run 会把局面推向更低选择、更强顺消。
- 关键结果：
  - v2 full interleave scoring：`altRun=10`、`cross=0.038`、`dominance=0.677`、`balance=0.364`。
  - v5 dominance trim：失败，`avgChoices=2.14`、`followRun=3`、`altRun=11`，更像单线传送带。
  - v8fast merge guard：正向，`avgChoices=2.70`、`maxChoices=4`、`followRun=2`、`nearUnlock=0.154`、`altRun=8`、`cross=0.040`、`dominance=0.533`、`balance=0.556`。
- 后续 v11 dominant spine cap：在 interleave 模式下禁止 dominant spine 继续合出 30+ 超长链，`A:L36` 被压到 `A:L23`，`dominance=0.500`、`followRate=0.345`、`cross=0.042`，但 `altRun` 仍为 8；当前卡点变为早期开出的弱 B 链太短（如 `B:L7`），会被 A 侧中长链持续压过。
- 多父源 v12 直接跑过慢，4 分钟无输出后已停止；后续应做单父源分批或 per-source timeout，不再盲放多父源。
- 已新增 `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Run-CPSInterleaveSourceBatch.ps1`，用每父源独立子进程、per-source timeout、连续 timeout 早停来稳定跑 CPS/interleave 源。v18/v19/v21/v22/v23 小批验证：runner 稳定，没有整批卡死；top12 中可产父源目前只有 `direct_pure_topup_03_12...dense_weave_g1_v08` 和 `nomask_05_dense_weave`，其中后者 `cross=0/dominance=0.607/follow=0.481`，只能算低选择候选，不达互锁目标。
- 已增加 `Build-PressureReadStageLockVariants.ps1 -RandomSeed`，但同一可产父源换 seed 后仍产出同一最优候选，说明短期不能靠 seed variation 扩产。
- 已试 `EnableWeakInterleaveSidecar`：能把弱目标 candidate 从 1 增到 10、chosen=5，但会让 `altRun 8->9`、`nearUnlock 0.154->0.25`，因此默认关闭，仅保留为实验开关。
- 当前新结论：CPS 只能证明父源存在潜在互锁边；真正 collapse 来自依赖合链阶段为了低选择/长结构链生成单侧超长链（例：`A:L36`）。下一步应继续优化 dependency merge 的“合到哪一侧”和未来可选最长链排程，而不是删链或盲目 flip。
- 2026-06-20 继续推进到 v46：
  - 修复 `CrossPotentialOnly` CSV 表头 bug：第一行 missing source 时不会再吞掉后续 ok 行的 CPS 指标。
  - 发现父源池误判原因：worktree 缺少大量未跟踪源资产；CPS 扫描应 `SourceRoot=F:\Unityproject\ArrowLevel-Hand` 读取完整主项目源，`OutputRoot` 仍写当前 worktree，避免污染其它实验。
  - 新增 `Build-CPSInterleaveProductionFeed.ps1`、`Select-CPSInterleaveCandidates.ps1`；`Run-CPSInterleaveSourceBatch.ps1` 支持 `BatchSourceSortMode` 和 `UltraChoiceEvalLimit`。
  - 关键性能突破：`UltraChoiceEvalLimit=120/180` 后，原本外部 120s/150s 超时的坏源能在约 35-70s 内内部 empty/reject；v33/v35/v40/v46 可稳定扫源，基本不再整批卡死。
  - 质量结论仍未达“稳定产出”：严格硬筛（低选择 + followRun<=2 + nearUnlock<=0.20 + cross>=0.03 + dominance<=0.56 + balance>=0.50）去重后仍只有 `direct_pure_topup_03_12...dense_weave_g1_v08` 这一类正式候选。
  - 新增生成但未过硬筛的代表：`nomask_style_12_long_corridor`（avg=3.33/max=6/followRun=3/cross=0.026/dominance=0.622）、`direct_pure_topup_01_14...core_burst`（avg=3.5/max=8/followRun=4/cross=0.024）。它们证明能稳定产出 probe，但不是巨难互锁质量。
  - 尝试 `AllowTightCurveStageException`，旧 near-pass 源未复现紧曲线，当前代码下仍为 `avg=3.255/max=6/stageLock=0.441`，暂不作为正式放宽口。
  - 当前主失败分布：`static curve too open` 和 `weak stage lock` 为主；继续盲扫收益低。下一步应改生成器本体的依赖锁制造能力：在 root/dependency merge 前就压低开局可选和阶段开放，而不是靠 ultra prune 后验删/翻。
- 本轮报告：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/building_grammar_cps_interleave_v1_20260620.md`。

## SGP Hard Lane V2.2 NearMissRescue Review5 - 2026-06-20

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已把用户反馈“最高可选偏高、外围近距离/同边顺消”落成 V2.2 trace/selector 指标：
  - `choicePeakCount/Rate/Excess`：专门抓 `choices>=8` 的峰值。
  - `outerNearFollowRate/RunMax`、`sameSideOuterFollowRate/RunMax`：抓连续解题中外圈近距离、同边近距离顺消。
  - `outerNearDependencyRate/RunMax`、`sameSideOuterDependencyRate/RunMax`：抓上一根直接解锁下一根的外圈近距离依赖。
- `Select-StageLockHardCandidates.ps1` 已支持 V2.2 硬门槛和评分惩罚；`Run-HardMidWideDoorBalancedStageGateMicroSlice.ps1` 已透传这些参数。
- Breakthrough4 用严格 V2.2 重筛后去掉 dense `maxChoices=8` 样本，保留 3 个核心样本。
- 新跑 section/dense/dual targeted rescue 6 源，产出 5 candidates；按 V2.2 初版 `outerNearFollowRunMax<=2`、`sameSideOuterFollowRunMax<=1`、`choicePeak=0` 选出 2 个 section 样本。
- diversity dual/sweep 6 源产出 3 trace-success candidates，但因外圈近依赖偏高且链条结构不足未纳入。
- 已冻结 5 关 review pack 并挂 Demo：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockNearMissRescueV22Review5Pack.asset`
  - Frozen levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDANearMissRescueV22Review5/`
  - Selection CSV：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_nearmiss_rescue_v22_review5.csv`
  - Frozen trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_nearmiss_rescue_v22_review5_frozen_trace_metrics.csv`
  - Report：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_v22_nearmiss_rescue_review5_notes_20260620.md`
- Review5 冻结 trace 验证：5/5 `processTier=S`、5/5 `tightProcessTier=S`；`avgChoices=3.594`、`maxChoices max=7`、`choicePeakCount max=0`、`outerNearFollowRunMax max=2`、`sameSideOuterFollowRunMax max=1`、`outerNearDependencyRate avg=0.044/max=0.057`、`stageLockScore avg=0.661`、`structuredHardnessV21 avg=0.679`。
- 当前判断：V2.2 比 V2.1 更接近最终难关节奏，能压掉 8+ 峰值并限制外圈同边顺消；短板是包小且 section-heavy。下一步应继续找非 section family 且能通过相同 V2.2 gates 的样本，不应靠放宽链条结构或外圈指标硬凑。

## SGP Hard Lane V2.3 Outer Exit Pressure Diagnosis - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 用户反馈 V2.2 Review5 仍有明显外出口和边缘连续消除感。诊断结论：V2.2 主要看 Greedy 实际 solve order，没充分看“玩家每一步同时看见多少外圈/外出口可选项”。
- 已新增 V2.3 trace 指标：
  - `headOuterExit` 静态链条标记。
  - `outerAvailableChoiceAvg/Max/Rate`、`outerAvailableHeavyStepRate`、`sameSideOuterAvailableChoiceMax`。
  - `outerExitAvailableChoiceAvg/Max/Rate`、`outerExitAvailableHeavyStepRate`、`sameSideOuterExitAvailableChoiceMax`。
  - `outerExitSolveRatio`、`outerExitSolveRunMax`、`sameSideOuterExitSolveRunMax`。
- `Select-StageLockHardCandidates.ps1` 已支持上述 V2.3 gate 和 score penalty；`Run-HardMidWideDoorBalancedStageGateMicroSlice.ps1` 已透传这些参数。
- 对当前 Review5 做 V2.3 严格诊断：`Selected=0/5`，拒绝原因为 `outer available choice max=1`、`same-side outer exit available choice max=4`。说明用户截图问题是真实指标盲点，不是个别视觉误判。
- 诊断报告：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_v23_outer_exit_pressure_diagnosis_20260621.md`。
- 下一步建议：后续生产不应继续把 V2.2 Review5 当作外出口已合格包；应按 V2.3 gate 重跑/重筛候选，即使产率明显降低。推荐初始硬门槛：`MaxOuterAvailableChoiceMax=3`、`MaxSameSideOuterAvailableChoiceMax=2`、`MaxOuterExitAvailableChoiceMax=2`、`MaxSameSideOuterExitAvailableChoiceMax=1`、`MaxOuterExitSolveRunMax=1`、`MaxSameSideOuterExitSolveRunMax=1`。

## SGP Hard Lane V2.4 Outer Shell Pressure - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 用户继续反馈：候选外圈总是很简单，尤其是左右边界长包边链/简单外壳链会让难度上不去。
- 新结论：问题分两层：
  - 静态外壳形态：外圈简单链、外圈长直链、同侧简单外壳集中。
  - 动态外圈压力：每一步玩家同时能看到多少外圈可选链，即使它们不是直接外出口。
- 已在 `Build-SGPRhythmTrace.ps1`/`Select-StageLockHardCandidates.ps1` 使用 V2.4 静态外壳指标筛出问题；严格静态+动态 gate 在 V20 88 关池上为 `0/88`，说明仅靠重筛无法量产，需要生成器前置控制。
- 已在 `Build-PressureReadStageLockVariants.ps1` 增加 `-OuterShellPressureGate`：
  - 识别 `outerSimple`。
  - 简单外壳链不再容易成为开局根链。
  - 合链时优先把简单外壳链吃进内部/非简单结构链。
  - 最终按 `MaxOuterSimpleChains`、`MaxOuterSimpleSideMax`、`MaxOuterSimpleCellRate`、`MaxOuterLongStraightRate` 拒绝。
  - 同时缩短生成资源名，降低 Windows 路径过长风险。
- `Run-HardMidWideDoorBalancedStageGateMicroSlice.ps1` 已透传 `OuterShellPressureGate` 和 generator 侧外壳上限参数。
- 冻结第一阶段预览包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockV24NoSimpleShellPreview6Pack.asset`。
- 冻结 trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v24_no_simple_shell_preview6_frozen_trace_metrics.csv`。
- 预览包结果：6/6 `S/S`，`outerSimpleChainCount=0`，`outerLongStraightChainCount=0`，`outerExitAvailableChoiceMax=2`，`sameSideOuterExitAvailableChoiceMax=1`，`outerExitSolveRunMax=1`。
- 仍未解决：`outerAvailableChoiceMax=5`、`outerAvailableHeavyStepRate≈0.47-0.51`，外圈普通可见项仍偏多。下一步 V2.5 应把动态 outer available pressure 前置进 orientation search/merge acceptance，而不是只靠 selector。

## SGP Building Grammar CPS Interleave v47-v73 - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-building-grammar`，branch `codex/sgp-building-grammar`。
- 当前目标仍未完成：尚未达到“多父源稳定产出 formal-quality dual-spine hard levels”。
- 本轮关键代码变化：
  - `Build-PressureReadStageLockVariants.ps1` 增加 `EnableStageLockMergeRescue` 诊断、stage-rescue 真实评估 picker、stage floor guard、root merge stage guard。
  - `Run-CPSInterleaveSourceBatch.ps1` 增加 `EnableInterleaveDominanceTrim` 透传。
  - interleave dominance trim 参数化，UltraLow/Interleave 下使用更强 dominance/follow 目标和 `coverage>=0.90` 底线。
  - UltraChoice prune 在 interleave 模式下增加 `coverage>=0.90` 守门。
  - targeted follow flip 试验后默认关闭；它会拖慢并破坏 stage/coverage。
- 关键实验证据：
  - Stage rescue 可把 `nomask_style_11_dense_kernel` 救成候选，但会单主干化，典型为 `altRun=15/dominance=0.708/followRun=3`，不能进 formal。
  - Strong trim 能把 dense near-pass 的 dominance/balance 拉好，但仍停在 `followRun=3`。
  - 真正复现 strict dense 候选的关键是 `UltraChoiceEvalLimit=0`：`direct_pure_topup_03_12...dense_weave_g1_v08` 可稳定得到 `chains=30/coverage=0.933/avg=2.70/max=4/followRun=2/cross=0.042/dominance=0.500/balance=0.556`。
  - `UltraChoiceEvalLimit=120/180` 适合 broad scan 防超时，但会漏掉上述 strict 候选。
  - unlimited broad scan 不可行：v72/v73 中多个 weak-stage/low-orientability 源 180 秒超时。
- 当前结论：
  - 现阶段只有 `direct_pure_topup_03_12...dense_weave_g1_v08` 这一类 strict family 稳定可用。
  - 产能瓶颈不是单纯 timeout，而是可定向、可交错父源太少；同时 follow-run breaker 只能通过昂贵 unlimited prune 找到。
- 下一步建议：
  1. 预算扫描：用 `UltraChoiceEvalLimit=120/180` 找 generated/near-pass，不要 broad unlimited。
  2. 构建 near-pass retry feed：只收集因 `avgChoices` 或 `followRun` 轻微失败、且 coverage/cross/dominance/balance 已接近过线的候选。
  3. 对 retry feed 才跑 `UltraChoiceEvalLimit=0`，并使用 per-source timeout history。
  4. `EnableStageLockMergeRescue` 和 `EnableInterleaveDominanceTrim` 只作为显式 probe 开关，不作为默认量产开关。
- 当前报告：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/building_grammar_cps_interleave_v1_20260620.md`。

## SGP Building Grammar CPS Interleave v74-v93 - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-building-grammar`，branch `codex/sgp-building-grammar`。
- 目标仍未完成：尚未达到多父源稳定产出 formal-quality dual-spine hard levels。
- 本轮新增工具/开关：
  - `Build-CPSInterleaveRetryFeed.ps1`：从 strict rejected 中抽取只因 `avgChoices/followRun` 轻微失败的源，生成 targeted unlimited retry feed。
  - `Build-PressureReadStageLockVariants.ps1` 新增 `-EnableCurveRootStraightPrune` 和 `-EnableTightRootPairSearch`，均为显式 probe 开关，不作为默认量产开关。
  - `Run-CPSInterleaveSourceBatch.ps1` 已透传以上两个开关。
- 关键修正：CPS/interleave batch 后续必须 `SourceRoot=F:\Unityproject\ArrowLevel-Hand`、`OutputRoot=.worktrees/sgp-building-grammar`。v6/v80 feed 里的很多 source asset 存在于主项目但不在 worktree；之前部分 `empty/missing source` 不能当真实生成失败。
- 关键实验结果：
  - `v74_nearpass_retry_unlimited` 复现 strict dense 候选，selector 1/1 通过：`chains=30/coverage=0.933/avg=2.70/max=4/followRun=2/cross=0.042/dominance=0.500/balance=0.556`。
  - `v75_all_nearpass_retry_feed` 扫全量 strict rejected 后仍只有 1 个 near-pass retry 源，说明当前 retry 窗口很窄。
  - `v80` 重新扩 broad source 后，CPS 高分前排多数真实失败在 `static curve too open` 或 `weak stage lock`，CPS 分数不能直接代表可量产性。
  - dense/dense-kernel 邻域（v81/v82）没有复现 `03_12_dense_weave` 的 strict 窗口，成功源是窄拓扑窗口，不是 broad family 规则。
  - `EnableCurveRootStraightPrune` 对 core_burst 有触发但没有实质改善；`EnableTightRootPairSearch` 对 maze/sweep probe 无明显收益。
  - `v91` 产出 yellow near-pass：`nomask_style_12_long_corridor`，但仍失败 `avgChoices/followRun/crossUnlock/dominance/balance`；`v92 unlimited` 反而让 curve 变差。
  - `v93 dense multi-target` 证明单个强父源即使 `TargetPerSource=3` 也只稳定产 1 个 strict 候选。
- 当前结论：
  - 目前 formal strict 产能瓶颈是“可 orientable + 可 interleave 的父源太少”，不是单纯重试次数不够。
  - 继续 broad unlimited 没意义；下一步应先做 parent source preflight/risk cache，用实际 StageGate preflight 指标（`openers/avg/max/stageLockScore`）过滤，再进昂贵生成。
  - `direct_pure_topup_03_12...dense_weave_g1_v08` 应作为所有后续改动的 known-good control。

## SGP Building Grammar CPS Interleave v94-v97 - 2026-06-21

- 新增 `Build-CPSInterleaveSourceRiskCache.ps1`：
  - 聚合 CPS top100、batch summary、merged candidates、pressure-read rejected CSV。
  - source-level 分类：`strict`、`candidate_near`、`preflight_near`、`generated_far`、`empty_far`、`timeout_risk`、`untried`。
  - 输出 `cps_interleave_v95_source_risk_cache.csv` 和 chain-range-limited next feed。
- v95 风险缓存分布：`untried=51`、`empty_far=23`、`timeout_risk=16`、`generated_far=5`、`preflight_near=3`、`candidate_near=1`、`strict=1`。
- v96 risk-cache untried slice 产出一个新的 yellow 源：
  - `nomask_style_10_zig_river`
  - `chains=49/coverage=0.947/avg=2.755/max=6/followRun=6/nearUnlock=0.163/cross=0.026/dominance=0.592/balance=0.462`
  - strict rejected by `chains/followRun/crossUnlock/dominance/balance`。
- v97 对 `zig_river` 做 targeted unlimited retry，180 秒 timeout，没有形成可量产突破。
- 当前最新结论：
  - 风险缓存能找出更多 yellow 源，但 strict 仍只有 dense control。
  - 目前真正需要攻克的是“有界 follow-run breaker”，而不是继续 broad unlimited 或单纯扩源。
  - 下一轮建议：针对 `zig_river` / `long_corridor` 这种 curve 接近但 `dependencyFollowRunMax` 高的源，做 bounded follow-run repair；仍以 `dense_weave` 作为回归控制。

## SGP Building Grammar CPS Interleave v98-v102 - 2026-06-21

- 新增/调整：
  - `Run-CPSInterleaveSourceBatch.ps1` 暴露 `InterleaveFlipPasses` / `InterleaveFlipCandidates`，默认仍为 0。
  - `Build-PressureReadStageLockVariants.ps1` 新增 `-EnableFollowRunStraightCut`：只在 dependency-follow run 压力下允许删除短/中低弯折目标链。
  - 修正 `Get-DependencyFollowTargetChains`：按当前 orientation 判断依赖，不再固定 orientation 0。
  - 新增 `-EnableSoftInterleaveDominanceTrim`：降低 interleave trim 强度，避免 UltraLow 强 trim 直接破坏 curve。
- `zig_river` probe 结果：
  - `v98` interleave flip 有执行，但 `followRun` 仍为 6，balance/dominance 更差。
  - `v99/v100` follow-run straight cut 删除了链（`remove:41`），但 `followRun` 仍为 6。
  - trace 显示真实问题是长 single-spine run：`spinePatternHead=OOOOOOOOOOAAAAAAAAAAAAAAAAABBBBBBBBBBAAAAAAAAAAAAA`，`spineAlternationRunMax=17`。
  - `v101` 强 trim 会把 curve 打开到 `openers=9/max=9`。
  - `v102` soft trim 保住 curve（`openers=6/avg=2.915/max=6`），但 stage lock 掉到 `lateRegionCount=0/stageLockScore=0.345`，不能作为候选。
- 当前最新结论：
  - 黄色源已经能找到；真正 blocker 是“打断长 A spine run”时保住 stage semantics。
  - 下一步应做 stage-aware interleave trim：不允许 `lateRegionCount/stageLockScore/stageGateRate` 退化过多，并优先让 B-spine 在 A-run 中段可用，而不是粗暴删 A-run 链。

## SGP Building Grammar CPS Interleave v103-v115 - 2026-06-21

- 已实现 stage-aware soft trim guard：
  - `New-InterleaveDominanceTrimmedLevel` 在 soft trim 路径下保护 `stageLockScore/stageGateRate/activeRegionAvg/lateRegionCount/firstThirdRegionCount`。
  - 同时保护 base curve（`avgChoices/maxChoices/openers`）和 base interleave floor（`spineAlternationRunMax/singleSpineDominanceRate/spineBalance/crossUnlockRatio`）。
  - trim 批次最终验收必须证明相对 pre-trim 有 meaningful interleave gain，否则整组 edits 丢弃。
  - UltraChoice prune 在 soft trim 路径下也加了 interleave/stage floor，避免 prune 把 trim 结果拉坏。
- 验证结果：
  - `v103` 证明 stage-aware trim 能安全改善 `zig_river`：`chains=47/coverage=0.931/avg=2.83/max=6/followRun=5/cross=0.028/dominance=0.553/balance=0.500/stageLockScore=0.645`。
  - `v104-v112` 证明继续加大 trim 会守住 stage 但让 curve/interleave 互相拉扯，仍不能 strict。
  - `v113/v114` 的 `long_corridor` stage/interleave 很好（`stageLockScore=0.802/cross=0.034/dominance=0.500/balance=0.556`），但 curve 太开（`avg=3.50/max=8`），unlimited 未改善。
  - `v115` dense control 仍 strict 1/1，说明新增 guard 没破坏已知可行路线。
- 当前最新结论：
  - stage-aware trim 是更安全的修复通道，但不是最终突破。
  - `zig_river` 需要的是 trace-aware B-spine activation，不是继续删 A-run。
  - `long_corridor` 需要的是 curve-specific prune，不能破坏它已经很好的 stage/interleave。
  - strict selector 继续保持不降级；黄色候选不计入正式产能。

## SGP Hard Lane V2.5 Outer Available Pressure - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已在 `Build-PressureReadStageLockVariants.ps1` 继续推进 V2.5：
  - post-merge acceptance 加入动态外圈压力不变差/可改善判断。
  - root/dependency/carrier merge 后不再把新链 orientation 重置为全 0，而是重新跑 stage-lock orientation。
  - early root / dependent chain orientation 增加外圈头惩罚，优先避免链头落在最外圈，尤其避免朝外。
- 严格 V2.5 gate 在 V20 broad 30-source probe 上仍为 `0` 候选，说明 `sameSideOuterAvailableChoiceMax<=2`、`sameSideOuterExitAvailableChoiceMax<=1`、`outerAvailableHeavyStepRate<=0.38` 同时成立仍过窄。
- 但 broad probe 证明路线可行：部分 rejected 已达到 `outerAvailableHeavyStepRate<=0.343`，剩余主要卡在同侧外圈集中和外出口可见压力。
- 已冻结 V2.5 Review3 小包：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockV25OuterAvailReview3Pack.asset`
  - Levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAV25OuterAvailReview3/`
  - Trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v25_outer_avail_review3_trace_metrics.csv`
  - Report：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_v25_outer_available_pressure_notes_20260621.md`
- V2.5 Review3 指标：3/3 `S/S`，`avgChoices avg=3.023`，`maxChoices max=6`，`stageLockScore avg=0.680`，`outerAvailableChoiceMax max=4`，`outerAvailableHeavyStepRate avg=0.165`。相比 V2.4 Preview6 的 `outerAvailableHeavyStepRate avg=0.495` 有明显下降。
- 当前不足：`sameSideOuterAvailableChoiceMax` 仍可到 3，`sameSideOuterExitAvailableChoiceMax` 可到 2，`outerExitSolveRunMax` 可到 2；Review3 只是突破样本，不是最终封板。
- 下一步建议 V2.6：保持普通外圈 heavy gain，同时加入 side-balanced outer-root scheduling、outer-exit solve-run 惩罚、冻结前候选去重，并用 `outerAvailableChoiceMax<=4`、`outerAvailableHeavyStepRate<=0.30`、`outerExitAvailableChoiceMax<=2` 作为更现实的第一批生产门槛。

## SGP Hard Lane V2.6 Outer Exit Solve Run - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已在 `Build-PressureReadStageLockVariants.ps1` 增加生成侧外出口连续解指标和 gate：
  - `MaxGeneratedOuterExitSolveRunMax`
  - `MaxGeneratedSameSideOuterExitSolveRunMax`
  - `outerExitSolveCount`
  - `outerExitSolveRatio`
  - `outerExitSolveRunMax`
  - `sameSideOuterExitSolveRunMax`
- V2.6 probe 产出 2 个候选并冻结对照包：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockV26OuterExitRun2Pack.asset`
  - Trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v26_outer_exit_run2_trace_metrics.csv`
  - Report：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_v26_outer_exit_and_skeleton_v1_notes_20260621.md`
- V2.6 指标：2/2 trace `S`，`outerAvailableHeavyStepRate avg=0.138`，`sameSideOuterAvailableChoiceMax max=2`，`outerExitAvailableChoiceMax max=2`，`sameSideOuterExitAvailableChoiceMax max=1`，`outerExitSolveRunMax max=1`，`sameSideOuterExitSolveRunMax max=1`。
- 当前不足：一个候选 `tightProcessTier=A` 且 `maxChoices=8`；V2.6 是外出口控制突破，不是最终 review pack。
- GPT Skeleton Graph 建议已复盘：有用点是“折点/转向由结构节点驱动，而不是 cell-by-cell 自由蛇形生长”。下一步不应重写 SGP，而应做 `SkeletonConstraintAdapterV1` 小实验：先生成 macro skeleton/cell labels，再让现有 SGP 在结构带内生成源关卡，最后继续走 StageLock + V2.6 外圈压力控制。

## SGP Hard Lane Skeleton Gate V1 Review2 - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已新增 `Tools/SGPRhythmLab/Build-SkeletonConstraintAdapterV1.ps1`，作为 Skeleton Graph 思路的最小落地：先生成结构骨架长链，再用短折链填缝，不替代现有 StageLock/V2.6 主流程。
- 关键验证：
  - Skeleton-only 直接进 StageLock 基本 0 产；原因不是覆盖/链长单点问题，而是缺少可定向的 stage/door 依赖。
  - Skeleton -> StageDoor -> StageLock 可以打通；`GateStrong` profile 产出 2 个真实 trace `S/S` 样本。
- 已冻结 review 包并挂 Demo：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockSkeletonGateV1Review2Pack.asset`
  - Levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDASkeletonGateV1Review2/`
  - Frozen trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_skeleton_gate_v1_review2_frozen_trace_metrics.csv`
  - Report：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_skeleton_gate_v1_review2_notes_20260621.md`
- Review2 指标：2/2 `S/S`，`avgChoices=2.46/2.78`，`maxChoices=5/5`，`stageLockScore=0.843/0.694`，`outerExitAvailableChoiceMax=1/1`，`outerExitSolveRunMax=1/1`。
- 当前卡点：Review2 是 loose GateStrong 产物，不是严格 V2.6 静态外壳封板；strict/dyn-strict gate 主要卡在 `outer simple shell too dominant`。即使放宽到 `MaxOuterSimpleChains=3`、`MaxOuterSimpleSideMax=2`、`MaxOuterSimpleCellRate=0.20`，near-pass 仍被该原因拒绝。下一步应专门减少 StageDoor 合链后的 simple outer shell，或检查 generator-side simple-shell rejection 是否还有额外条件，而不是继续堆通用限制。

## SGP Hard Lane Skeleton Gate V1 Final6 - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 当前方向已收束为：`SkeletonConstraintAdapterV1 -> StageDoor GateStrong -> StageLock`。Skeleton 只作为宏观结构先验，不替代已验证的 SGP/StageLock 可解验证链路。
- 已在 `Build-PressureReadStageLockVariants.ps1` 新增 `-AllowArchitecturalOuterShell`。该开关不是普通放宽，而是在动态外圈压力干净时，允许少量建筑式外壳链存在，解决 Review2 中“动态可玩但被静态 simple outer shell gate 误杀”的问题。
- Final6 已冻结并挂 Demo：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockSkeletonGateV1Final6Pack.asset`
  - Levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDASkeletonGateV1Final6/`
  - Frozen trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_skeleton_gate_v1_final6_frozen_trace_metrics.csv`
  - Report：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_skeleton_gate_v1_final6_notes_20260621.md`
- Final6 冻结 trace：6/6 solved，6/6 `processTier=S`，6/6 `tightProcessTier=S`；尺寸覆盖 15x24 与 16x25；chains 22-25；coverage 0.75-0.775；openers 1-2；`avgChoices=2.50-3.08`；`maxChoices=5-6`；`stageLockScore=0.668-0.821`；`outerStraightRunMax=0`；`outerExitSolveRunMax=0-1`。
- 当前结论：Skeleton Gate V1 已有小样突破，能做出区别于旧蛇形 StageLock 的结构化 hard lane；但样本只有 6 关，还不是完整量产 lane。下一步若人工看图通过，应扩 skeleton layout 模板和尺寸，而不是回退到纯 SGP 或纯手摆。

## SGP Hard Lane Skeleton Gate V1 DenseDep Review2 - 2026-06-21

- 人工反馈 Final6：形态神似但仍差一点，主要是依赖关系不够、覆盖率不足。
- 已在 `Build-SkeletonConstraintAdapterV1.ps1` 增加高覆盖模式：`-TargetCoverage`、`-DenseOuterGuardFill`、`-StrongDoorBridges`。该模式用会拐入内部的边缘护栏链和更强 door/bridge 链提高覆盖与依赖潜力，不改默认旧流程。
- 已冻结并挂 Demo：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockSkeletonGateV1DenseDepReview2Pack.asset`
  - Levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDASkeletonGateV1DenseDepReview2/`
  - Frozen trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_skeleton_gate_v1_dense_dep_review2_frozen_trace_metrics.csv`
  - Report：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_skeleton_gate_v1_dense_dep_review2_notes_20260621.md`
- DenseDep Review2 指标：2/2 `S/S`；尺寸 15x24、15x25；chains=24/24；coverage=0.822/0.805；openers=4/2；`avgChoices=3.00/2.12`；`maxChoices=4/5`；`stageLockScore=0.767/0.927`；`stageGateRate=0.458/0.792`；`mergedDependencyCount=8/4`；`outerExitSolveRunMax=1/1`。
- 结论：DenseDep 是对“覆盖不足/依赖不足”的正向改进，但产率很低，16x25 高覆盖源主要失败于 `outer available pressure too high`、`outer simple shell too dominant`、`weak stage lock`。下一步如果视觉过关，应新增更多自带 door/bridge 语义的 skeleton template，而不是继续只调筛选阈值。

## SGP Hard Lane RoomDoor Skeleton V2 Review3 - 2026-06-21

- 根据 GPT 反馈和人工判断，DenseDep 仍偏“密集路径填充”，缺少主干、房间、门、hub 的语义层。已在 `Build-SkeletonConstraintAdapterV1.ps1` 新增显式 `-RoomDoorSkeletonV2` 模式。
- V2 源生成逻辑：先放 4 个外房间和可选 core room，再放 main spine、door bridge、room-local U/C lane，最后才做有限 outer guard/top-up。默认旧 V1/DenseDep 流程不受影响。
- V2 source 小批覆盖 15x24、15x25、16x25，源指标：coverage 0.790-0.805，chains 31-38，`straightLikeRate=0`，`structureCarrierRate=0.571-0.710`，`localSnakeTurnRunMax=1`。
- 已冻结并挂 Demo：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockRoomDoorSkeletonV2Review3Pack.asset`
  - Levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDARoomDoorSkeletonV2Review3/`
  - Frozen trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_roomdoor_skeleton_v2_review3_frozen_trace_metrics.csv`
  - Report：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_roomdoor_skeleton_v2_review3_notes_20260621.md`
- Review3 指标：3/3 `processTier=S`，`tightProcessTier=2S/1A`；sizes 15x25/16x25/15x25；chains 28/24/29；candidate coverage≈0.800；openers 3/4/5；`avgChoices=2.57/3.25/3.52`；`maxChoices=5/7/7`；`stageLockScore=0.835/0.683/0.738`；`meaningfulUnlockRate=0.920/1.000/0.958`；`outerExitSolveRunMax=1`。
- 当前结论：RoomDoor V2 是比 DenseDep 更合理的结构语义方向，StageDoor 可可靠转化，StageLock 可通过；但仍不是量产线。问题是 15x24 0 产、16x25 两个候选高度相似、第三关只到 S/A。下一步若视觉认可，应扩 6-10 个 room graph template，而不是放宽筛选阈值。

## SGP Hard Lane Dependency Skeleton V3 Probe2 - 2026-06-21

- 人工反馈 RoomDoor Skeleton V2：视觉上像 room/door/skeleton，但实际阻挡关系弱，不如早期强依赖版本。关键决策：Greedy/StageLock 只能做可解验收，不能作为“怎么摆”的参考；下一步必须先做物理依赖骨架。
- 已在 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SkeletonConstraintAdapterV1.ps1` 新增 `-DependencySkeletonV3`，用于先摆跨区 blocker/target 链，让目标链头部逃逸射线能撞到 blocker 链；room/door/spine 只作为投影标签。
- V3 source 小批：`stage_lock_source_dependency_skeleton_v3_probe3.csv`，3 个源，coverage `0.832-0.837`，chains `36-39`，`straightLikeRate=0`，`structureCarrierRate=0.611-0.641`，`intendedDependencyCount=4-5`。
- 经 StageDoor GateStrong + StageLock 后产出 2 个候选，并冻结/挂 Demo：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockDependencySkeletonV3Probe2Pack.asset`
  - Levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDADependencySkeletonV3Probe2/`
  - Frozen trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_dependency_skeleton_v3_probe2_frozen_trace_metrics.csv`
  - Report：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_dependency_skeleton_v3_probe2_notes_20260621.md`
- Probe2 指标：2/2 `processTier=S`，2/2 `tightProcessTier=S`；coverage `0.832`；`avgChoices=2.19/2.43`；`maxChoices=5/4`；`stageLockScore=0.879/0.867`；`avgDepDistance=8.588/9.361`；`nearDepRate=0.114/0.028`；`depMeaningfulSignal=1.0`；`outerExitSolveRunMax=1`。
- 人工看图反馈：V3 Probe2 视觉“不太行”。问题是它像几根依赖示意线拼出来的 proof-of-concept，不像完整关卡；指标硬，但画面退化为过稀、过手摆、缺少既有 SGP 的密度和自然填充。
- 追加验证：`DependencySkeletonV3` direct StageLock 0 产，拒绝原因为 `no stage-lock orientation`；提高 `TargetCoverage=0.90` 的 dense source 只到 coverage `0.837/0.856`，说明当前 V3 填充器不是简单调覆盖率能救。
- 当前结论：GPT 的“Dependency Graph 先成立，再投影到几何”方向仍有效，但当前 V3 实现只能算依赖指标正样本，不能算视觉/正式路线成功。下一步不要继续把 dependency skeleton 当关卡主体；应改成“现有高质量 SGP/StageDoor 源为主体 + dependency anchors/constraints 注入”，并新增 realized-dependency 报告，统计 intended blocker edges 有多少在 StageDoor/StageLock 后真实保留。

## SGP True Hard Dependency V7 Curated5 - 2026-06-21

- 核心目标已重新校准：不是追求“建筑链条语言”本身，而是做出真正有难度、可读、非无脑扫的关卡。建筑/线稿/房间感只作为结构可读性的辅助约束，主线是“自然高覆盖 SGP 主体 + 真实跨区依赖 + 低选择过程曲线”。
- 新增筛选脚本：`.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Select-TrueHardDependencyCandidates.ps1`。
  - 该脚本合并候选 CSV 与 trace CSV，按 `depMeaningfulSignal`、`avgDepDistance`、`nearDepRate`、coverage、choice curve、stageLock、outer pressure、chain structure 和 metric signature 去重筛选。
  - 目的：从已有高质量 SGP/StageDoor/StageLock 自然填充关卡中筛出“真实依赖难度”样本，避免再次走向手摆骨架图。
- 已冻结并挂 Demo：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockTrueHardDependencyV7Curated5Pack.asset`
  - Levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDATrueHardDependencyV7Curated5/`
  - Selected CSV：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_true_hard_dependency_v7_curated5_selected.csv`
  - Frozen trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_true_hard_dependency_v7_curated5_frozen_trace_metrics.csv`
  - Report：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_true_hard_dependency_v7_curated5_notes_20260621.md`
- Curated5 指标：5/5 solved；5/5 `processTier=S`；4/5 `tightProcessTier=S`、1/5 `tightProcessTier=A`；families 覆盖 `section_unlock/sweep/dense_weave/maze_long_chain`；coverage `0.955-0.993`；chains `32-44`；`avgChoices=2.59-3.63`；`maxChoices=4-6`；`stageLockScore=0.606-0.849`；`meaningfulUnlockRate=0.879-1.000`；`avgSolveJumpDistance=7.716-10.143`；`outerAvailableHeavyStepRate=0.091-0.257`。
- 当前风险：这是 selection-first 方向，还不是生成时依赖注入；严格强依赖样本仍集中在 `section_unlock`，部分多样性补充样本 `outerExitSolveRunMax=2`，需要人工看 Demo 判断是否可接受。
- 下一步：以该 selector 作为目标质量模型，做“高质量 SGP 主体 + 少量 dependency-anchor 注入”的生成侧实验，并补 realized-dependency 报告。

## SGP Building Grammar DualCarrier Diagnostic - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-building-grammar`，branch `codex/sgp-building-grammar`。
- 目标仍未完成：需要稳定产出真实 dual-spine / cross-unlock 难关；本轮只证明了源增强层的瓶颈和下一步切入点。
- 已新增实验 `StageDoorProfile=DualCarrier` 到 `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-StageLockSourceEnhancedVariants.ps1`：
  - 奖励两侧 carrier 平衡增长；
  - 只在两侧已有本地 carrier 后允许少量跨侧 bridge；
  - 输出 `dualCarrierBridgeCount`、`dualCarrierMergeCounts` 诊断字段；
  - 对 `DualCarrier` 禁用普通 `DependencyAware` fallback，避免绕开 dual-carrier 约束后退化成单线。
- 小批验证：
  - `DependencySkeletonV3 -> GateStrong -> StageLock` 能出候选，但一个是 `cross=0.034` 且 `dominance=0.697/balance=0.286`，另一个 `balance=0.875` 但 `cross=0`。
  - `DependencySkeletonV3 -> DualCarrier -> StageLock` 能产生更早的 cross 信号，最好样本 `cross=0.048/dominance=0.515/balance=0.444`，但 `avgChoices=3.939/followRun=4`，未过 strict。
  - 更深 carrier 合并可把某样本压到 `avgChoices=3.133`，但 cross 归零并出现单线支配；16x26 尺寸扩展 0 产，主要失败为 `no stage-lock orientation`。
- 当前结论：`DualCarrier` 是有用诊断 profile，不是稳定生产解。StageDoor merge 无法从当前依赖骨架里稳定“发现”真实双线互激活；下一步应做“自然高覆盖 SGP/StageDoor 主体 + 少量 dependency-anchor 注入”的源生成实验，并增加 realized-dependency 报告，直接统计 intended blocker/target pair 在 StageDoor/StageLock 后是否保留。

## SGP Building Grammar Dependency Anchor v160-v166 - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-building-grammar`，branch `codex/sgp-building-grammar`。
- 新增脚本：`.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-DependencyAnchorSourceVariants.ps1`。
  - 从 TrueHard V7 等自然高覆盖 SGP/StageLock 成品出发，解析 `LevelDefinition`，寻找现有 target/blocker 逃逸射线依赖候选，只翻转少量 target 链来注入 dependency anchor。
  - 支持 `-UseFinalPath`，用于直接锚定最终 curated 成品主体，而不是回到 pre-final source body。
- 关键结果：
  - `v163_finalbody_anchor` 是当前最强正信号：h91 section source 产出 2 个 relaxed pass 候选，`chains=29`、`coverage=0.943`、`avgChoices=2.724`、`maxChoices=4`、`nearUnlock=0.192`、`cross=0.048`、`dominance=0.483`、`balance=0.75`、`stageLockScore=0.835`，严格 gate 只差 `dependencyFollowRunMax=3`，目标为 `<=2`。
  - `v164_h91_followcut` 改善了次要指标（`nearUnlock=0.154`、`cross=0.050`、`dominance=0.448`），但 `followRun` 仍为 3。
  - `v165/v166` 证明单纯提高 `anchorCrossCount` 不够：双 cross anchor 组合会把选择曲线打开到 `avgChoices=3.812/maxChoices=6`，且 `followRun` 反而到 4。
- 当前最新结论：
  - 正确主线是“自然高质量 SGP final body + 少量 dependency anchor”，不是手摆骨架，也不是单纯多加 cross。
  - 现在最接近 strict 的卡点是 realized solve trace 中那段具体 3 连 follow run；下一步应做 trace-positioned anchor：先定位 max follow-run 的链 id/空间组，再选择能在该 run 中段激活 opposite spine 的 anchor。
  - strict selector 暂不降级；`followRun=3` 的 h91 输出只能算 yellow near-pass，不计入稳定产能。
  - 当前报告已追加：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/building_grammar_cps_interleave_v1_20260620.md` 的 `v160-v166 Dependency Anchor Source Prior`。

## SGP Building Grammar Follow-Run Pair Repair v167-v168 - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-building-grammar`，branch `codex/sgp-building-grammar`。
- 已新增诊断脚本：`.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-DependencyFollowRunReport.ps1`。
  - 从最终 `LevelDefinition` 重算 greedy dependency follow-run。
  - 输出 max follow-run segment、逐步可选替代链、single/pair/triple flip probe。
  - 支持导出 pair repair 资产：`-ExportBestPairFlipRepair`、`-ExportAllPairFlipRepairs`。
  - 已修正一个关键误判：自撞/非法逃逸射线链不再被当作“无依赖”忽略，而是会导致该候选在简化模型中也不可解。
- h91 near-pass 的真实 max follow-run 卡段已经定位：
  - `15,20,12,21`，labels=`BBBB`，groups=`RRRR`。
  - `9,13,2,19`，labels=`AAAA`，groups=`LLRR`。
  - 这两段不是“没有选择”的强制顺序，而是 Greedy 偏向更长的依赖子链，导致 ABC 顺消感。
- 单翻无效；三翻没有比有效 pair 更好，且很多组合会造成自撞/非法方向。
- 当前唯一 validated pair repair 是翻转 `12,2`：
  - full trace：`2/2` solved，`processTier=S`，`tightProcessTier=S`。
  - 指标：`openers=4`、`avgChoices=2.86`、`maxChoices=4`、`dependencyFollowRunMax=2`、`nearUnlockRate=0.16`、`crossUnlockRatio=0.059`、`singleSpineDominanceRate=0.414`、`spineBalance=0.571`。
  - strict `MaxAvgChoices=2.85` 下只因 `avgChoices=2.862` 被拒；relaxed `MaxAvgChoices=2.90` 下 `2/2` selected。
  - 风险：`stageLockScore` 会从 h91 anchor body 的约 `0.835` 降到 `0.506`，阶段门锁感变弱，不能直接算稳定量产。
- 当前结论：trace-positioned pair flip 是真实破局机制，但下一步必须改成 stage-aware repair：只接受 `invalidChainCount=0`、可解、降低 `followRun`、并在完整 trace 后保留 stage floor 的组合。

## SGP Building Grammar Stage-Aware Follow Repair v169-v170 - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-building-grammar`，branch `codex/sgp-building-grammar`。
- 新增流水线脚本：`.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-StageAwareFollowRunPairRepair.ps1`。
  - 串联 `Build-DependencyFollowRunReport.ps1` repair export 与 `Build-SGPRhythmTrace.ps1` full trace。
  - 输出 joined / selected / rejected / runs CSV。
  - 使用完整 trace gate：`tightProcessTier=S`、`avgChoices<=2.90`、`maxChoices<=5`、`followRun<=2`、`nearUnlock<=0.20`、`cross>=0.03`、`dominance<=0.56`、`balance>=0.50`、`stageLock>=0.60`、`lateRegionCount>=2`。
- `Build-DependencyFollowRunReport.ps1` 已扩展 `-ExportAllTripleFlipRepairs`，用于验证小组合翻转是否能在不破 stage 的情况下打断 follow-run。
- `Build-StageAwareFollowRunPairRepair.ps1` 已输出 `repairStageLockDrop`、`repairLateRegionDrop`；h91 `12,2` repair 从 `lateRegionCount=4` 掉到 `1`，`repairLateRegionDrop=3`。
- 批量验证结果：
  - pair-only h91：2 repair / 2 trace / 0 selected。
  - pair-only depanchor v160-v166：2 repair / 2 trace / 0 selected。
  - pair+triple h91：6 repair / 6 trace / 0 selected。
  - pair+triple depanchor v160-v166：10 repair / 10 trace / 0 selected。
- 关键诊断：
  - 所有能把 `followRun=3` 压到 `2` 的合法组合，都会把 `stageLockScore` 从约 `0.835` 降到 `0.493-0.506`，且 `lateRegionCount=1`。
  - 说明当前 repair 是通过翻 late-stage gate chain 来打断 ABC 顺消，会过早打开局面，不符合“巨难/阶段感”目标。
- 当前下一步：
  - repair 前先给 max-run chains 标注 stage/region role。
  - 禁止或重罚 late-gate carrier 被翻。
  - 新增 non-gate interruptor 搜索：找能在 max run 中段可见、能竞争 Greedy child、但不破 lateRegion 的邻近非门锁链。

## SGP Building Grammar Non-Gate Interruptor Probe v171 - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-building-grammar`，branch `codex/sgp-building-grammar`。
- 已在 `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-DependencyFollowRunReport.ps1` 增加 non-gate interruptor 诊断/repair probe：
  - 新开关：`-EnableInterruptorMergeProbe`、`-ExportAllInterruptorMergeRepairs`。
  - 在 max follow-run 中寻找已经可见但 Greedy 未选的 independent alternative，再尝试让该替代链吸收非 max-run 邻链，通过长度竞争打断顺消。
  - 支持尾部 append 和中段 splice；同时输出 `interruptorAltCount`、`interruptorCloseAltCount`、`interruptorBestDeficit`、`interruptorBest`。
- 已在 `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-StageAwareFollowRunPairRepair.ps1` 增加 `-EnableInterruptorMergeRepairs`，把 interruptor repair 接入完整 trace gate。
- 验证结果：
  - h91 v164：`interruptorAltCount=10`、`interruptorCloseAltCount=8`、最佳替代为 `step=13 alt=23 cur=21 deficit=2`，但有效 append/splice repair 为 `0`。
  - depanchor v160-v166：只有 v160 出现 2 条几何 probe，但本地结果变差到 `avgChoices=4.658/maxChoices=8/followRun=3`，没有导出 repair。
  - stage-aware 批量仍为 `0 selected`；唯一能降 follow-run 的仍是翻 `12,2` 这类 late-gate chain，且会把 `stageLockScore` 降到约 `0.506`、`lateRegionCount=1`。
- 当前结论：non-gate interruptor 目标正确，但不能从当前 dense final body 后验稳定恢复；h91 是“有可见替代，但无局部几何插队空间”的典型。下一步应把 interruptor requirement 前移到 dependency-anchor source 生成/筛选：优先选择或制造 max-run alternative 附近有可竞争 sidecar/merge space 的 body。

## SGP True Hard Dependency V14 Outer Head Zero - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 人工反馈 V7/V9 仍有核心问题：外圈直接朝外出口偏多，导致外围难度偏低、可扫边感太强。
- 已把“外圈问题”拆成两类：
  - 动态外出口：当前或 solve 过程中可直接消的外圈出口，已有 `outerExitAvailable*`、`outerExitSolve*` 指标。
  - 静态外出口头：外圈头部直接朝外，即使当前不一定可消，也会造成视觉低难度感；本轮新增 `outerExitHeadCount/Rate/SideMax`。
- 已修改 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` 输出静态外出口头指标，并修改 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Select-TrueHardDependencyCandidates.ps1` 支持 `MaxOuterExitHeadCount/Rate/SideMax` gate。
- 新增 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Repair-OuterExitHeadChains.ps1` 做外圈出口头链头尾互换 probe。结论：对 V9 sweep 样本全翻或 1-2 个组合翻转均会破可解，说明部分外出口头是依赖入口，不能后验盲删。
- 已冻结并挂 Demo 外圈零出口头审查包：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockTrueHardDependencyV14OuterHeadZeroVisualPack.asset`
  - Levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDATrueHardDependencyV14OuterHeadZeroVisual/`
  - Frozen trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_true_hard_dependency_v14_outer_head_zero_visual_frozen_trace_metrics.csv`
  - Report：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_true_hard_dependency_v14_outer_head_zero_notes_20260621.md`
- V14 指标：2/2 solved，2/2 `processTier=S`，`tightProcessTier=1S/1A`，`outerExitHeadCount=0/0`，`outerExitAvailableChoiceAvg=0/0`，`outerExitSolveRatio=0/0`，`avgChoices=3.00/3.87`，`maxChoices=6/8`，selection coverage `0.993/0.993`。
- 当前结论：V14 是 focused review pack，不是量产答案；若人工认可外圈观感，下一步要把 outer-head-zero 做成生成侧先验，而不是后验修复或单纯筛选。

## SGP Hard Lane DifficultyScoreV1 - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 目标：让 SGP 真正能判断难度，用于指导生成、筛选和合链；不能只依赖 Greedy 可解路径或单个 `maxChoices`。
- 已在 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` 增加浅层反事实指标：
  - `counterfactualBranchDivergenceAvg/Max`
  - `counterfactualBestFutureValueAvg`
  - `counterfactualGreedyRegretAvg`
  - `counterfactualMeaningfulOptionRate`
  - `counterfactualLocalOnlyStepRate`
  - `counterfactualFakeChoiceRate`
  - `choiceControlScoreV1/counterfactualScoreV1/antiFlowScoreV1/outerCleanScoreV1/difficultyScoreV1`
  - 批量性能参数：`MaxCounterfactualMovesPerStep`、`CounterfactualStepStride`
- 已新增更贴合“顺着消”的关键指标：
  - `dependencyFollowRate`
  - `dependencyFollowRunMax`
  - `dependencyFollowMeaningfulRate`
  - 该指标统计 solve order 是否连续沿着 parent-child dependency 走，能直接解释“外圈干净但内部顺消”的体感问题。
- 已在 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Select-TrueHardDependencyCandidates.ps1` 接入：
  - `MinDifficultyScoreV1`
  - `MaxDependencyFollowRunMax`
  - `MaxDependencyFollowRate`
  - `MaxCounterfactualLocalOnlyStepRate`
- 校准结果：
  - V14 外圈干净，但 `dependencyFollowRunMax=3/4`、`dependencyFollowRate≈0.42/0.43`，验证用户反馈的“依赖关系简单、顺着消”。
  - V20 88 关中只有 5 关 `dependencyFollowRunMax<=2`；这些 near-miss 外圈普遍脏，`outerExitHeadCount=5-8`、`outerExitSolveRatio≈0.13-0.22`。
  - `outer clean + dependencyFollowRunMax<=2 + 稳定选择曲线` 当前旧池 0 产，说明下一步必须生成侧同时前置 outer-head-zero 和 non-follow interruptor，不能继续靠后验筛选。
- 当前报告：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_difficulty_score_v1_notes_20260621.md`。
- 下一步建议：生成/合链时硬控 `dependencyFollowRunMax<=2`，并将其作为 merge acceptance floor；如果合链导致 3+ parent-child follow run，必须拒绝或插入 non-gate interruptor。

## SGP NonFollow Outer Repair V15 - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已把 `dependencyFollowRunMax/Rate` 前移到 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1`：
  - `Evaluate-Orientation` 输出 `dependencyFollowCount/Rate/RunMax/MeaningfulRate`。
  - 新增 `-NonFollowHardGate`、`-MaxGeneratedDependencyFollowRunMax`、`-MaxGeneratedDependencyFollowRate`。
  - orientation score 和合链接受条件会惩罚或拒绝制造 3+ parent-child 顺消的结果。
- 生成侧 strict probe 结果：对 top near-miss 4 源打开 `NonFollowHardGate` 后 `0/4` 产出，全部拒绝为 `dependency follow too linear`；说明当前高质量源的顺消是结构性问题，不是简单调 root 数能解决。
- 新增稳定 repair lane：`.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Run-NonFollowOuterRepairSlice.ps1`。
  - 从 DifficultyScoreV1 trace 里先找 `dependencyFollowRunMax<=2` 的真实 non-follow hard 候选。
  - 对这些候选做 bounded outer-exit-head subset repair。
  - 重新跑 trace 后精选 S/S、低外圈出口压力候选。
- 已冻结并挂 Demo：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockNonFollowOuterRepairV15Review3Pack.asset`
  - Levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/NFOuterV15Frozen/`
  - Frozen trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_nonfollow_outer_repair_v15_review3_frozen_trace_metrics.csv`
  - Report：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_nonfollow_outer_repair_v15_notes_20260621.md`
- V15 指标：3/3 `processTier=S`、3/3 `tightProcessTier=S`；`avgChoices=3.20-3.76`；`maxChoices=5-6`；`dependencyFollowRunMax=2`；`dependencyFollowRate=0.389-0.448`；`difficultyScoreV1=0.763-0.820`；`outerExitHeadCount=3-4`；`outerExitAvailableChoiceMax=3`；`outerExitSolveRatio=0.081-0.133`。
- 当前结论：这是“真正难度”方向的正突破：先锁定 non-follow hard，再修外圈，比 V14 的“外圈干净但顺消”更接近最终目标。风险是外圈出口头仍非 0，且 generation-side non-follow 仍低产；下一步应扩大 V20/V21/V15 source trace 池，用该 runner 做小批生产，同时继续前置 non-gate interruptor 到源生成阶段。

## SGP Local Patch Burst Diagnosis - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 人工反馈 V15 Review3 虽外出口改善，但三关外圈结构相似，且局部红框区域会“一下全都消”，体感难度仍偏低。
- 已在 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` 新增局部连消指标：
  - `localPatchSolveRunMax`
  - `nearOuterPatchSolveRunMax`
  - `localWindow5NeighborMax`
  - `nearOuterWindow5NeighborMax`
  - `localUnlockBurstMax`
- 校准结果：
  - V15 Review3 重放后，三关虽 `dependencyFollowRunMax=2`，但 `localWindow5NeighborMax=5`，第一关 `localPatchSolveRunMax=5`，能解释“同一小块一串扫完”的肉眼反馈。
  - V15 repair 77 候选用 `localPatch<=3 / nearOuterPatch<=2 / localWindow5<=4` 严格重筛后 `0/77` 通过。
  - V20 88 关重放后 `localPatchSolveRunMax` 分布 `min=2,p50=5,max=13`，`localWindow5NeighborMax` 分布 `min=4,p50=5,max=5`；旧池几乎都有局部扫光倾向。
  - 唯一低 burst 近源（dense_weave）经外圈修复可达 `outerExitHead=0`，但仍为 `dependencyFollowRunMax=3/localPatchSolveRunMax=4`。
- 已把同一套 local patch 指标前移到 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` 的 `Evaluate-Orientation`，并在 `NonFollowHardGate` 下加入生成侧拒绝：
  - `MaxGeneratedLocalPatchSolveRunMax`
  - `MaxGeneratedNearOuterPatchSolveRunMax`
  - `MaxGeneratedLocalWindow5NeighborMax`
  - `MaxGeneratedNearOuterWindow5NeighborMax`
- 单源验证：低 burst dense_weave 源开启生成侧 local gate 后 `0` 产，拒绝原因为 `local patch burst too linear`，候选指标 `dependencyFollowRunMax=2` 但 `localWindow5NeighborMax=5/nearOuterWindow5NeighborMax=5`。
- 当前结论：问题不是 `maxChoices` 或 Greedy 可解本身，而是旧源/旧合链语言天然会形成“局部传送带”。后续要前置 non-gate interruptor / 跨区替代选择，或扩新源池；不应继续在 V15/V20 旧池上只靠后验筛选和外圈修复硬捞。

## SGP Building Grammar DepAnchor LowChoice v175 - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-building-grammar`，branch `codex/sgp-building-grammar`。
- 目标仍是基于 SGP/StageLock 主流程攻克建筑迷宫巨难关卡：让双解锁线在真实 trace 中互相激活，避免单侧超长链/ABC 顺消，先从 near-pass 推到稳定候选。
- 已证伪的路线：
  - v172/v173 source-side `-EnableInterruptorSidecar`：能保持覆盖但会打开选择数或杀掉 realized cross，典型结果 `avgChoices≈3.3-3.9`、`maxChoices=7`、`followRun=3-4`。
  - v174 broad final-body anchor 单纯扩父源：高 cross potential 源若不加低选择门锁，会变成开放房间；低 cross 源会回到单主干。
  - 对当前 best h231 near-pass 的后验 flip/soft trim/hard trim/interleave flip：能改善 balance 的同时会把 `avg/max/followRun/nearUnlock` 打坏，不能作为主线。
- 当前正突破：
  - h231 final-body dependency anchor + `Build-PressureReadStageLockVariants.ps1 -StageGateSearch -UltraLowChoiceBias -InterleavedLockBias -SourceSortMode CrossPotential -AllowTightCurveStageException`。
  - 输出：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_depanchor_v175_h231_lowchoice_light_full_candidates.csv`
  - Pack：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLock_depanchor_v175_h231_lowchoice_light_full_Pack.asset`
  - 8/8 指标一致：`avgChoices=2.613`、`maxChoices=4`、`dependencyFollowRunMax=3`、`crossUnlockRatio=0.040`、`singleSpineDominanceRate=0.645`、`spineBalance=0.300`、`stageLockScore=0.825`、`lateRegionCount=3`、`nearUnlockRate=0.111`。
- follow-run 诊断：最长段只有 3，主要是 `5,11,4,6`、`22,12,0,17`、`15,23,24,30`；翻 chain `4` 可把 run 降到 2 但会不可解，pair/trim/interruptor repair 也未找到可用解。
- 当前结论：v175 是真正进展但仍是 yellow near-pass，不是 strict 稳产。下一步不要再走 sidecar/flip/trim 后验修，应该寻找更多 h231-like 的“高结构 + 可低选择门锁化”父源，并把 anti-follow/interruptor 约束前移到生成/StageLock finalization，而不是 final body 后处理。

## SGP Building Grammar DepAnchor v176 Strict Breakthrough - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-building-grammar`，branch `codex/sgp-building-grammar`。
- 从 `.worktrees/sgp-rhythm-lab` 的 V20 difficulty trace 池 `pressure_read_stage_lock_hard_production_v20_difficulty_v1_trace_metrics.csv` 里筛 10 个 h231-like 源，条件为 solved、`chains=30-38`、`avgChoices=2.7-4.2`、`maxChoices<=7`、`stageLockScore>=0.65`、`dependencyFollowRunMax>=3`。
- 生成 30 个 final-body dependency anchor source：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_lock_source_depanchor_v176_h231like_v20.csv`。
- 对 top8 cross-potential source 跑 `Build-PressureReadStageLockVariants.ps1 -SourceSortMode CrossPotential -MinCrossPotentialScore 0.80 -InterleavedLockBias -StageGateSearch -UltraLowChoiceBias -AllowTightCurveStageException`。
- near-pass 输出：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_depanchor_v176_top8_lowchoice_light_candidates.csv`。
  - hcd5 组 4 关：`avgChoices=2.321`、`maxChoices=4`、`followRun=3`、`stageLockScore=0.847`、`lateRegionCount=3`。
  - h91-like/hfc8-hd08 组 4 关：`avgChoices=2.724`、`maxChoices=4`、`followRun=3`、`dominance=0.483`、`balance=0.75`、`stageLockScore=0.835`、`lateRegionCount=4`。
- 对 v176 top8 跑 follow-run pair repair 后，hcd5 组出现第一批 strict repair：
  - `10,8` pair repair 4/4 full trace solved。
  - 指标：`openers=3`、`avgChoices=2.64`、`maxChoices=5`、`dependencyFollowRunMax=2`、`stageLockScore=0.822`、`lateRegionCount=3`、`nearSolveRunMax=1`、`processTier=S/tightProcessTier=S`。
  - `10,2` repair 虽也 `followRun=2`，但 `stageLockScore=0.562`、`lateRegionCount=1`，已判定为坏修复。
- 已冻结 4 关 strict review pack：
  - Pack：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_D176StrictPack.asset`
  - Levels：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/D176Strict/`
  - Selected CSV：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/depanchor_v176_pairrepair_10_8_strict_selected.csv`
  - Trace：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/trace_depanchor_v176_pairrepair_metrics.csv`
- 当前结论：这是 building-grammar lane 第一次真正 strict 过线，证明“h231-like 源 -> final-body depanchor -> StageGateSearch+UltraLowChoice -> trace-positioned stage-preserving pair repair”可行。仍不是稳定量产：当前只有 4 个、源族相关性高。下一步应把该 lane 自动化到更大 source feed，并只收 `followRun<=2/stage>=0.80/late>=3/avg<=2.90/max<=5` 的 repair。

## SGP Building Grammar DepAnchor Automated Strict Slice v177-v180 - 2026-06-21

- 新增自动化 runner：`.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Run-DepAnchorLowChoiceStrictSlice.ps1`。
  - 流程：从 trace CSV 筛 h231-like 源 -> final-body dependency anchor -> cross-potential probe -> top source family cap -> `StageGateSearch + UltraLowChoiceBias` -> follow-run pair repair export -> full trace -> strict select -> 可选 freeze pack。
  - strict gate：`followRun<=2`、`stageLockScore>=0.80`、`lateRegionCount>=3`、`avgChoices<=2.90`、`maxChoices<=5`；可选 `RequireTightS`。
  - runner 已修复：即使 0 candidate/0 repair/0 strict，也会写 summary，避免后续批量跑丢失败原因。
- 用 V20 difficulty trace 池扫 h231-like 27 源：
  - `depanchor_v177_smoke`：offset 0，4 源 -> 4 candidate -> 4 repair -> 2 strict；复现 v176 top hcd5 族。
  - `depanchor_v178_offset4`：8 源 -> 8 candidate -> 0 repair -> 0 strict；候选 `avgChoices=2.839-3.065`、`followRun=2-3`、`stage>=0.821`，属于 close-but-not-strict。
  - `depanchor_v179_offset12`：8 源 -> 7 candidate -> 0 repair -> 0 strict；候选 `avgChoices=2.613-3.242`、`followRun=2-3`、`stage>=0.820`。
  - `depanchor_v180_offset20`：7 源 -> 5 candidate -> 0 repair -> 0 strict；候选 `avgChoices=2.583-2.829`，但全部 `followRun=3`。
- 当前结论：流水线已可复跑，且 smoke 能复现 strict；但 V20 当前 h231-like 池的 strict 产能集中在 top hcd5 族，后续源只有 near-pass。瓶颈从“管线是否成立”转移为“source scarcity / 可 stage-preserving repair 的 max-run 几何太少”。
- 下一步：扩大输入源到 V21/V15/nonfollow repair/high-stage h231-like bodies；新增 source-level “repairable max-run geometry”预测，不只看 cross potential。

## SGP Building Grammar Repairability Gate v182-v184 - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-building-grammar`，branch `codex/sgp-building-grammar`。
- 已扩展源池：
  - `building_maze_relaxed_source_v182_trace_feed.csv`：从 V9/V23/V24/nonfollow 风格 trace 合并 165 rows。
  - `building_maze_allnear_source_v183_trace_feed.csv`：从 156 个历史 trace metrics 去重后筛 36 个 low-choice/high-stage near 源。
- 新结果：
  - `d182b`：9 源 -> 6 StageLock candidates -> 6 pair repairs -> 3 strict。指标：`avgChoices=2.64`、`maxChoices=5`、`openers=3`、`stageLockScore=0.822`、`lateRegionCount=3`、`dependencyFollowRunMax=2`、`tightProcessTier=S`。
  - `d183all`：16 源 -> 8 candidates -> pair repair 0；candidate 很接近（`avgChoices=2.724`、`maxChoices=4`、`stage=0.835`、`late=4`、`followRun=3`），但 triple repair 虽可降 `followRun=2`，会把 `stageLockScore` 打到 `0.506`、`lateRegionCount=1`，不可收。
  - `d184off`：20 源 -> 10 candidates -> 4 pair repairs -> 2 strict；仍集中在 hcd58-like geometry。
- 新增分析脚本：`.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Measure-FollowRunRepairability.ps1`。
  - 输入 candidate/segments/repair/repair-trace CSV，输出 `repairabilityClass`、max-run label patterns、是否含 `O`、repair 是否保 stage。
  - 输出文件：`d182b_repairability.csv`、`d183all_repairability.csv`、`d184off_repairability.csv`。
- 关键新判断：
  - 可修正例：`labels=AAAA`、`maxLabelCharCount=1`、`maxHasO=False`，pair repair `10,8` 后仍保 `stage>=0.80/late>=3`，分类为 `strict_pair_repairable`。
  - 负例：`AAAA|ABBB`、`AAAA|OOOO`、`AAAA|BBBB|OOOO` 或 max-run 含 `O`，分类为 `mixed_fragile` / `repair_breaks_stage`；即使 triple 能压 follow-run，也大概率打碎 stage。
- 当前结论：关键突破是“repairability gate”而不是 triple repair 本身。SGP/StageLock 能稳定产出 near-pass；真正瓶颈是能否找到 stage-preserving repairable max-run geometry。下一步应把 repairability gate 接入 strict slice runner，优先搜索 hcd58-like homogeneous max-run 源，减少全池盲跑。

## SGP Building Grammar Runner Repairability Gate v185-v186 - 2026-06-21

- 已在 `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Run-DepAnchorLowChoiceStrictSlice.ps1` 增加可选 `-EnableRepairabilityGate`。
  - gate 条件默认：`maxFollowLength<=3`、`maxLabelPatternCount<=1`、`maxLabelCharCount<=1`、不允许 max-run 含 `O`。
  - runner 会先导出 pair repair，再生成 `<run>_repairability_prefilter.csv`，然后只 trace `<run>_repairability_prefiltered_repairs.csv`。
  - 默认关闭，避免影响 v176/v177 复现路径。
- 验证结果：
  - `d185gate`：使用 V182 relaxed feed、family cap 3，6 candidates -> 6 raw repairs -> 6 gated repairs -> 3 strict。证明 gate 不误杀 hcd58 homogeneous 正例。
  - `d186fam1`：同 feed、family cap 1，8 candidates -> 2 raw repairs -> 2 gated repairs -> 1 strict。证明限制同族复用后仍能出货，但产率降到每批约 1 个。
- `d186fam1_repairability_prefilter.csv` 关键诊断：
  - 唯一正例为 `repair_candidate_homogeneous`，`labels=AAAA`、`maxLabelCharCount=1`、`maxHasO=False`，最终 strict 为 `pairrepair_10_8`。
  - 其余 7 个为 `mixed_fragile`，常见 `AAAA|BBBB|OOOO`、`AAAA|ABBB`，无可用 pair repair。
- 当前结论：runner 已具备“保正例/识别负例/减少无效 trace”的 gate 能力；但 stable production 仍未完成，核心瓶颈是 homogeneous max-run 可修源太少。下一步要在 StageLock 前找更多 hcd58-like 源，而不是继续单纯扩大 cross-potential 池。

## SGP Building Grammar Raw-Pass Pressure Gate v187-v189 - 2026-06-21

- 人工看 D185/D186 review 后反馈：外圈出口和依赖关系偏弱，开局消除压力偏低；说明 `avgChoices≈2.64/openers=3` 的极低选择 strict 虽过指标，但体感偏冷。
- 已修正 `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Run-DepAnchorLowChoiceStrictSlice.ps1` 的一个生产盲点：
  - 新增 `-IncludeRawPassingCandidates`，对 StageLock 原始候选先跑 raw trace；如果原始 candidate 已满足 strict gate，会和 pair-repair 结果合并入选。
  - 新增可调体验 gate：`StrictMinOpeners`、`StrictMinCrossUnlockRatio`、`StrictMinSpineBalance`、`StrictMaxSingleSpineDominanceRate`、`StrictMaxDependencyFollowRate`。
  - 旧默认行为保持不变；只有显式开启 raw-pass/新 gate 时生效。
- `d187v182off9fam1` 复查 V182 offset9/family cap1：10 StageLock candidates，pair repair 0，strict 0；证明 offset 后段不是没有候选，而是旧 runner 只看 pair repair 会漏掉 raw-pass。
- `d188v182off9rawcross`：启用 raw-pass，gate 为 `avg<=3.10/max<=5/followRun<=2/followRate<=0.55/stage>=0.80/late>=3/openers>=3/cross>=0.035/balance>=0.40/dominance<=0.70`，产出 3 个 raw strict。
  - 指标：`openers=3`、`avgChoices=3.06`、`maxChoices=4`、`dependencyFollowRunMax=2`、`dependencyFollowRate=0.40`、`stageLockScore=0.821`、`lateRegionCount=3`、`crossUnlockRatio≈0.04`。
- `d189v182off9open4probe`：开局压力优先 gate，`openers>=4`、`avg<=3.50`、`stage>=0.79`，产出 1 个 raw probe。
  - 指标：`openers=4`、`avgChoices=3.42`、`maxChoices=5`、`dependencyFollowRunMax=2`、`dependencyFollowRate=0.333`、`stageLockScore=0.799`、`lateRegionCount=3`、`crossUnlockRatio=0.042`、`spineBalance=0.556`。
- 已冻结并挂 Demo 对照包：
  - Pack：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_D188D189PressureReview4Pack.asset`
  - Levels：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/D188D189PressureReview4/`
  - Selected CSV：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d188_d189_pressure_review4_selected.csv`
  - Demo activePack GUID：`51bf7f997b44461189aad34ec80fe392`
- 当前结论：raw-pass 是真实产能补点；但 `openers>=4 + followRun<=2 + cross>0` 仍很窄，目前只有 1 个压力样本。下一步应把开局压力/外圈出口作为 StageLock source/finalization 的前置目标，而不是只靠后验筛选。

## SGP Building Grammar Opening Pressure Interleave Flip v190-v191 - 2026-06-21

- 人工继续反馈：外圈出口和依赖关系偏弱，开局消除压力偏低；因此目标从极低选择的 `openers=3` 调整为更强开局压力的 `openers=4`，同时保留 `dependencyFollowRunMax<=2`、真实 `crossUnlockRatio>0` 和 stage lock。
- 已在 `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1` 增加 `-OpeningPressureBias` 系列参数，用于 StageLock 搜索阶段把入口数、cross、spine balance、dominance、follow rate 纳入评分；避免 `UltraLowChoiceBias` 一味压低开局入口。
- 已在 `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Run-DepAnchorLowChoiceStrictSlice.ps1` 透传 `InterleaveFlipPasses/InterleaveFlipCandidates`，允许显式使用 StageLock 内部 interleave flip probe；默认仍为 0，不影响旧复现。
- `d190v182off9pressurebias` 复测结果：10 个 raw candidate 中 6 个 `openers=4`，但只有 1 个 strict；失败主要分两类：`followRun=3` 的强依赖近似样本，以及 `followRun=2` 但 `cross=0` 的弱依赖样本。
- `d191v182off9pressureflip`：同 offset9 加 `InterleaveFlipPasses=2/Candidates=18`，产出 2 个 raw strict 并自动冻结 pack。
  - 指标：`openers=4`、`avgChoices=3.32`、`maxChoices=5`、`dependencyFollowRunMax=2`、`dependencyFollowRate=0.333`、`stageLockScore=0.833`、`lateRegionCount=3`、`crossUnlockRatio=0.043`、`spineBalance=0.556`、`singleSpineDominanceRate=0.613`。
  - Pack：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_d191v182off9pressureflipStrictPack.asset`
  - Levels：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/d191v182off9pressureflipStrict/`
  - Selected CSV：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d191v182off9pressureflip_strict_selected.csv`
  - Demo activePack GUID：`d557d7b332f54fdb89cd253b3b1b30ff`
- 当前判断：d191 是“更热的开局压力档”的正结果，比 d188/d189 的 `stage≈0.80` 更强；但 2 关仍同源相似，不代表稳定量产。下一步应扩 offset/source feed 验证该参数档是否能跨 source family 出货，并补上更直接的外圈出口动态指标。

## SGP Building Grammar Two-Stage Repair Probe v192-v197 - 2026-06-21

- 重要纠偏：`Run-DepAnchorLowChoiceStrictSlice.ps1` 的输入参数实际名为 `InputTraceCsv`；此前命令里使用的 `-TraceFeedPath` 没有真正切换到 V182 feed，d191/d192/d194 label 虽含 v182，但实际主要跑的是默认 V20 difficulty trace 源。已为 `InputTraceCsv` 增加 alias `TraceFeedPath`，避免后续误传。
- 扩源结果：
  - 默认 V20 h231-like 27 源已基本用 pressure+flip 档扫完：`d191` 出 2 strict，`d192`/`d194` 为 0。
  - 真实 V183 all-near feed 用 `InputTraceCsv + MinSourceFollowRun=0` 跑 `d196/d197`，0 strict。
  - 主要失败模式稳定复现为：raw candidate 有 cross/stage 但 `dependencyFollowRunMax=3-5`；pair repair 可把 `followRun=2` 但常把 `crossUnlockRatio` 打成 0。
- 已修复 `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-DependencyFollowRunReport.ps1` 的 repair 导出长路径问题：repair level id 现在会用短 base + hash，避免三翻/多翻 asset 写 `.meta` 时超过 Windows 路径限制。
- 已新增显式 probe 能力：
  - `ExportAllSingleFlipRepairs`
  - `RepairAllowNoFollowGain`
  - `RepairMinOpeners`
  - `IncludeAllFlipCandidates`
- 正突破：two-stage repair route 成立。
  1. `d192`/`d197` 的 triple repair 可得到 `openers=3`、`followRun=2`、`stageLockScore=0.822`、`crossUnlockRatio=0.043` 的 cross-preserving 候选。
  2. 对该候选做 single opener repair，可得到完整过线样本：`openers=4`、`avgChoices=2.68`、`maxChoices=5`、`dependencyFollowRunMax=2`、`dependencyFollowRate=0.444`、`stageLockScore=0.810`、`lateRegionCount=3`、`crossUnlockRatio=0.043`、`spineBalance=0.571`、`singleSpineDominanceRate=0.571`。
- 已冻结并挂 Demo 新 review pack：
  - Pack：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_D191D192PressureOpenerReview3Pack.asset`
  - Levels：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/D191D192PressureOpenerReview3/`
  - Selected CSV：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d191_d192_pressure_opener_review3_selected.csv`
  - Demo activePack GUID：`a9f92033148947d5ad82f58f1e5d6393`
- 当前判断：这是比 d191 更关键的生产路线突破，因为它第一次把 `保 cross` 和 `拉开局入口` 拆成可验证的两段修复链。但仍非稳定量产：当前 only 1 个 two-stage unique 样本，且与 d197 复现样本高度同构。下一步应把 two-stage repair 自动接入 runner，再跨源验证产率。

## SGP Building Grammar Hard Opening Pressure v198 - 2026-06-21

- 人工复查 D191/D192 后再次反馈：外圈出口和依赖关系偏弱，开局消除压力偏低。新的解释是：`openers=4` 并不等于“有压力”，反而容易形成外圈可见入口过多、顺着点的低压开局。
- 已在 `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Run-DepAnchorLowChoiceStrictSlice.ps1` 增加更硬的可选 strict gate：
  - `StrictMaxOpeners`
  - `StrictMaxSpineAlternationRunMax`
  - `StrictMaxOuterTouchSolveRatio`
  - `StrictMaxSameOuterSideSolveRunMax`
  - `StrictMaxOuterStraightSolveRatio`
  - `StrictMaxSameSideOuterStraightRunMax`
  - `StrictMinStrictMeaningfulUnlockRate`
  - `StrictMaxNearUnlockRate`
  - `StrictMinAvgUnlockDistance`
- 同时把 two-stage repair route 自动接入 runner：`-EnableTwoStageRepairProbe` 会在 pair repair 之外尝试 triple repair，并在需要时继续 single opener repair；默认关闭，避免影响旧复现。
- `d198hardpressure_off0` 验证中，大批 run 在 StageLock 后段耗时偏高，已手动停止主 runner，但已落 raw candidate/trace；随后对 5 个候选手动跑 pair repair，得到 1 个更符合“硬开局/外圈较干净/远依赖”的样本：
  - `levelId=pressure_read_stage_lock_47e565_04_pairrepair_10_8_45f501b6d8`
  - `openers=3`
  - `avgChoices=2.64`
  - `maxChoices=5`
  - `dependencyFollowRunMax=2`
  - `dependencyFollowRate=0.481`
  - `stageLockScore=0.822`
  - `lateRegionCount=3`
  - `crossUnlockRatio=0.043`
  - `spineBalance=0.571`
  - `singleSpineDominanceRate=0.571`
  - `outerTouchSolveRatio=0.571`
  - `sameOuterSideSolveRunMax=3`
  - `strictMeaningfulUnlockRate=0.800`
  - `nearUnlockRate=0.200`
  - `avgUnlockDistance=8.872`
- 已冻结并挂 Demo 单关 review：
  - Pack：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_D198HardPressureRefine1Pack.asset`
  - Levels：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/D198HardPressureRefine1/`
  - Selected CSV：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d198_hardpressure_refine1_selected.csv`
  - Demo activePack GUID：`b8d5a783a3a14f50a42fd32d4e63e3bf`
- 当前判断：v198 比 D191/D192 更符合“压开局、压外圈、提高远依赖质量”的人工反馈；但仍只有 1 个样本，且 `spineAlternationRunMax=10` 表示双线交替还不稳定。下一步应将 source/StageLock 搜索缩小到能快速产生 `openers=3 + followRun=3 near-miss` 的源，再批量跑 pair/two-stage repair，而不是一次性大范围长跑。

## SGP Building Grammar Outer-Root Anchor v209-v214 - 2026-06-21

- 人工继续反馈：外圈出口和依赖关系偏弱，开局消除压力偏低。已确认此前 `outerTouchSolveRatio` 全局指标不足以代表开局压力，新增并使用前 25% solve window 指标：
  - `earlyOuterTouchSolveRatio`
  - `earlySameOuterSideSolveRunMax`
  - `earlyOuterAvailableChoiceAvg`
  - `earlyOuterAvailableChoiceMax`
  - `earlySameOuterAvailableChoiceMax`
- 已更新 `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` 与 `Build-PressureReadStageLockVariants.ps1`，让 trace 和 StageLock 生成侧都能输出/惩罚这些 early outer 指标；`Run-DepAnchorLowChoiceStrictSlice.ps1` 已透传对应 OpeningPressure 参数。
- 已在 `.worktrees/sgp-building-grammar/Tools/SGPRhythmLab/Build-DependencyAnchorSourceVariants.ps1` 增加显式开关 `-OuterRootAnchorBias`：
  - 识别外圈 target chain：`outerCellRate >= OuterRootMinTargetOuterCellRate`
  - 奖励“外圈 target 被内侧/跨侧 blocker 锁住”
  - 惩罚同侧外圈 blocker
  - 导出 `anchorOuterRootTargetCount`、`anchorInnerBlockerCount`、`anchorSameSideOuterCount`
  - runner 可用 `-OuterRootAnchorBias` 复现。
- `d212_outerroot_probe`：用 d210 near-miss 成品再锚，2/4 指标方向改善但失败：
  - `outerTouchSolveRatio=0.517`、`sameOuterSideSolveRunMax=2`
  - 但 `stageLockScore=0.654`、`lateRegionCount=2`、`crossUnlockRatio=0`
  - 结论：成品二次锚会破 stage/cross，不是主线。
- `d213_d211_earlyouter_repair`：对 d211 near-miss 做 pair/triple 修复，能把 `followRun=2`，但 stage 破坏严重：
  - best trace `stageLockScore≈0.506`、`lateRegionCount=1`
  - 结论：后验翻链无法保住后段门锁。
- `d214_d206_outerroot_source`：回到 d206 原始 source feed 重做 outer-root anchor，得到 2 个 near-miss raw 候选：
  - `openers=3`
  - `avgChoices=2.32`
  - `maxChoices=4`
  - `dependencyFollowRunMax=3`
  - `stageLockScore=0.848`
  - `lateRegionCount=3`
  - `crossUnlockRatio=0.043`
  - `outerTouchSolveRatio=0.571`
  - `sameOuterSideSolveRunMax=3`
  - `earlyOuterAvailableChoiceAvg=2.429`
  - `earlyOuterAvailableChoiceMax=4`
  - pair repair 会回到 D198/D199 类 `followRun=2`，但 `earlyOuterAvailableChoiceMax` 反弹到 5。
- 当前 Demo 已挂 near-miss review 包，不是正式成果：
  - Pack：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_D214OuterRootNearMissReview2Pack.asset`
  - Levels：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/D214OuterRootNearMissReview2/`
  - Selected CSV：`.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d214_outerroot_nearmiss_review2_selected.csv`
  - Demo activePack GUID：`d2140000000000000000000000000002`
- 当前结论：最接近突破的是“原始 source 层 outer-root anchor + StageLock”，但仍差最后一层：需要在 StageLock 方向选择阶段显式避免 `sameOuterSideSolveRunMax=3+` 和 `earlyOuterAvailableChoiceMax=4+`，同时不能靠 pair repair 打断，因为 repair 会让 early outer 可选数反弹或破坏 stage。

## SGP Building Grammar d215 Topology Admission Controller - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-building-grammar`，branch `codex/sgp-building-grammar`。
- 已将 GPT 讨论的 d215 思路落成最小工程改动：
  - `Build-PressureReadStageLockVariants.ps1` 新增 opt-in `-TopologyAdmissionGate`，把 `earlyOuterAvailableChoiceMax`、`earlySameOuterSideSolveRunMax`、`sameOuterSideSolveRunMax` 从软评分提升为 StageLock 输出前 hard gate。
  - StageLock 选向排序会对 topology gate 失败方向加大惩罚；最终输出前仍硬拒绝，避免 bad near-miss 进入 candidate CSV。
  - `Run-DepAnchorLowChoiceStrictSlice.ps1` 新增 strict early-outer gate 参数，并透传 topology admission 参数。
  - runner 新增 `-EnableRepairReboundGuard`：pair/triple/single repair 只有在不降低 `stageLockScore/lateRegionCount`、不增加 early outer 与 same-side outer 指标时才允许入选 strict。
- 验证结果：
  - `d215_topology_gate_d206_smoke`：d206 8 源 -> 24 anchor source -> 10 top StageLock near-miss 全部被 topology gate 拒绝；主因 `earlyOuterAvailableChoiceMax=4/5`，证明 early outer leak 是当前 root cause。
  - `d215_topology_prune_d206_smoke`：加入温和 topology prune 后仍 0 candidate；说明现有 d206 outer-root/source 几何需要以 4-5 个 early outer 可见选择为代价才能保 stage/cross，不能靠删/翻少量链修成 d215 strict。
  - 历史 trace 扫描发现 2 个 early-clean near-miss：`earlyOuterAvailableChoiceMax=3`、`earlySameOuterSideSolveRunMax=2`、`stageLockScore=0.791/0.835`、`crossUnlockRatio=0.048`，但 `sameOuterSideSolveRunMax=5`。
  - 对这 2 个 near-miss 做 pair repair：0 个可导出候选；做 triple repair：6 个候选全部可解且 `followRun=2`，但 `stageLockScore` 掉到 `0.469-0.506`、`lateRegionCount=1`、`earlyOuterAvailableChoiceMax` 反弹到 `4/5`，再次证明后验修复会破坏拓扑。
- 当前结论：d215 hard gate 方向正确，但还未稳定产出。真正切入点应前移到 source/StageLock candidate generation：主动生成“天然 early-clean + same-side-clean + stage/cross 保留”的父源/选向，而不是继续对已成型 candidate 做 follow repair。
- 下一步建议：做 source-side topology prefilter/bias，而非放宽 gate。候选源必须在进入 StageLock 前就具备：`earlyOuterAvailableChoiceMax<=3` 潜力、跨侧/内侧 blocker 足够、同侧外圈 blocker 少；否则 StageLock 后验 hard gate 会稳定 0 产。

## SGP Rhythm Lab Outer-Clean Dependency Anchor Probe - 2026-06-21

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已新增/修改：
  - `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-OuterCleanDependencyAnchorSourceVariants.ps1`
  - `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-DependencyBraidSourceFeed.ps1`
  - `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1`
- 核心纠偏：不要再用 StageLock 后验硬洗旧体。StageLock 会把 outer-clean anchor 的远依赖洗回 `dependencyFollowRunMax=4`、`dependencyBraidBadLocalRate≈0.824` 的局部顺消。有效方向是先在 source/anchor 层生成外圈干净、远距离 blocker-target 关系。
- `Build-OuterCleanDependencyAnchorSourceVariants.ps1` 会在锚点生成前置过滤：
  - base 外出口头过多直接拒；
  - target 翻向后头部朝外直接拒；
  - emitted 外出口头数量增加或超阈值直接拒；
  - 输出 `baseOuterExitHeadCount/emittedOuterExitHeadCount/anchorTargetOuterExitCount` 等诊断字段。
- 验证结果：
  - `dependency_braid_outerclean_anchor_source_v3_wide160.csv` 生成 160 个 outer-clean anchor source。
  - trace 后 15 个直接可解，2 个 strict 过线。
  - 最好两关：
    - `stage_lock_source_outerclean_anchor__12_h7b6d1627_v12`：`openers=3`、`avgChoices=3.65`、`maxChoices=8`、`outerExitSolveRunMax=1`、`outerAvailableChoiceMax=4`、`dependencyBraidScore=0.730`、`dependencyFollowRunMax=3`、`localPatchSolveRunMax=4`。
    - `stage_lock_source_outerclean_anchor__27_h7b6d1627_v27`：`openers=3`、`avgChoices=3.68`、`maxChoices=8`、`outerExitSolveRunMax=1`、`outerAvailableChoiceMax=4`、`dependencyBraidScore=0.699`、`dependencyFollowRunMax=3`、`localPatchSolveRunMax=4`。
- 已挂 Demo review pack：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DependencyBraidOuterCleanPreviewPack.asset`
  - Selected CSV：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_outerclean_preview4_selected.csv`
  - Demo activePack GUID：`b14cfd7a384947d5bb5a9d4304d0bc91`
- 当前判断：这是 SGP-native 方向的真实突破，但不是最终量产。产率仍低（`15/160` 可解，`2/160` strict），且 strict 样本 `maxChoices=8` 仍偏高。下一步应做 anchor-set topology-aware 选择：生成前预测/拒绝会成环或方向单一的锚点组合，提高 strict 命中率，并把 max choice peak 压到 `5-6`。

## SGP Rhythm Lab HardStructure V3 Causal Unlock - 2026-06-21

- 已在 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` 落地 HardStructure V3 只读判别器：
  - 记录每条链 `firstAvailableStep` 与 `clearStepByChain`。
  - 基于现有 `parentOf` unlock edge 近似重建 causal unlock graph。
  - 输出 `hardStructureV3Score/rawScore/class`、`causalCudP20`、`causalAntiLocalityScore`、`causalCrossRegionCriticalLockCount`、`causalSolveDelayAvg`、`causalFanoutMax`、`directionalSolveRunMax` 等字段。
  - 正式 `hardStructureV3Score` 会被 `causalAntiLocalityScore` 和 `localPatchSolveRunMax` gate，避免“raw 远依赖高但仍是本地顺消”的假高分。
- 已在 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-DependencyBraidSourceFeed.ps1` 增加 V3 可选筛选和评分字段：`MinHardStructureV3Score`、`MinCausalAntiLocalityScore`、`MinCausalCudP20`、`MinCausalCrossRegionCriticalLockCount`；默认不影响旧流程。
- 校准 run：`dependency_braid_outerclean_anchor_source_v3_wide160_hardv3_trace_metrics.csv`。
  - 160/160 traced，15 solved。
  - V3 class：`LocalEasy=141`、`WeakCausality=13`、`MediumStructure=6`、`HardPotential=0`、`TrueHardCandidate=0`。
  - 当前 demo review 的 12/27 号样本虽外圈基本干净且 raw 远依赖高，但 `causalAntiLocalityScore≈0.29`、`localPatchSolveRunMax=4`、`dependencyBraidBadLocalRate≈0.64-0.67`，被 V3 正确判为 `LocalEasy`，解释了人工反馈“还是比较顺着消、依赖太近”。
- V3 strict feed：`dependency_braid_outerclean_anchor_hardv3_strict_feed.csv` 为 0 行；near-miss feed：`dependency_braid_outerclean_anchor_hardv3_nearmiss_feed.csv` 为 3 行。
  - 最好的结构 near-miss `stage_lock_source_outerclean_anchor__82_hcd58a195_v2` 有 `causalAntiLocalityScore=0.667`、`localPatchSolveRunMax=3`，但 `processTier=Drop` 且 `causalCrossRegionCriticalLockCount=1`。
- 当前结论：外圈出口问题已经基本可控；真正瓶颈是“可解 + 多个跨区关键锁 + anti-local 高”三者不能同时成立。下一步应改 anchor-set topology-aware 选择/生成，而不是继续只做后验外圈修复或 Greedy 指标筛选。
- 详细报告：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_structure_v3_causal_unlock_notes_20260621.md`。

## SGP Rhythm Lab Trace Bridge Proof V1 - 2026-06-22

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已通过 rosetta/GPT 协作确认并落地核心纠偏：support bridge 必须是 ray-collision 物理因果链，并且必须在 execution trace 中可见；static graph/closure 不能单独作为成功依据。
- 已更新 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PressureReadStageLockVariants.ps1`：trace bridge 诊断字段进入 weak-stage rejected；planned upstream order bias；`-AllowWeakStageForTraceBridgeProbe` proof-only 开关；compatibility search 后重套 fixed target/support orientation，避免 planned bridge 被搜索覆盖。
- 已更新 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1`：新增 planned trace bridge replay 字段，验证 target/support/upstream 的静态 ray 存在和 trace 顺序；replay 采用与 source probe 一致的 head-ray containment 语义。
- Proof 包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_TraceBridgeProofV1Pack.asset`；冻结关卡：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/TraceBridgeProofV1/`。
- 冻结 replay：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/trace_bridge_proof_v1_frozen_replay_with_bridge_metrics.csv`。3/3 solved，3/3 planned bridge replay `ok`，depth=3，static/order target-support-upstream 均通过。
- 当前判断：这是“source-level ray-collision bridge 能 surviving StageLock + solve + independent replay”的工程 proof，但不是最终高难关。3/3 仍为 `HardStructureV3Class=LocalEasy`，`localPatchSolveRunMax=5`，`causalAntiLocalityScore=0.188-0.257`，`stageLockScore=0.381-0.547`。
- 下一步：不要继续扩大 proof-only weak-stage bypass；应把 source-slot bridge selection 做成 stage-quality-aware，候选必须同时有 late-region stage 潜力和 local patch 抑制，再进入 StageLock。详细报告：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/trace_bridge_proof_v1_notes_20260622.md`。

## SGP Rhythm Lab Board Trace Gate Low Peak V1 - 2026-06-22

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已新增/使用独立最终门：`.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Invoke-StageLockBoardTraceGate.ps1`，它批量调用 `Build-SGPRhythmTrace.ps1` 后再筛选；`Evaluate-Orientation` 只作生成侧 prefilter。
- realclosure 8 个 unique candidates 证明了 `U1 -> S2 -> H9 -> T25/T29/T32` 的 board-trace bridge 可真实 replay：`solved=True`、`processTier=A`、planned bridge `ok`、support closure depth=3，但 `choiceWave=... 4 8 7 7 ...`，因 `maxChoices=8` 被 low-peak gate 拒绝。
- `MaxGeneratedMaxChoices=6/7` 单源实验均 0 产出，失败原因仍是 `choice peak too high max=8`；说明当前卡点是中盘 unlock fanout spike，不是 bridge/外圈/局部 run。
- 已把 board gate 增加 choice wave shaping 字段：`boardTraceChoiceSoftCapHits`、`boardTraceChoiceSoftCapExcess`、`boardTraceChoiceMaxRise`、`boardTraceChoiceMaxRiseStep`。
- 从历史 trace 中捞出并复验 4 个低峰值候选，冻结为 review pack：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLaneTraceGateLowPeakV1Pack.asset`
  - Levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLaneTraceGateLowPeakV1Frozen/`
  - Gate summary：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_trace_gate_low_peak_v1_frozen_board_gate_summary.md`
  - Demo activePack 已指向 GUID `3bdbe33d46084384b98d6c0718f5dc9b`。
- Frozen replay 4/4 accepted：`maxChoices<=7`、soft cap 6 只 hit 1 次、`localPatchSolveRunMax=3`、`dependencyFollowRunMax=3`、`supportClosureBestScore=0.374/depth=2`。这批是可看过渡包；结构证明弱于 realclosure depth=3。
- 下一步核心：不要只在 graph/evaluator 做抽象 `releasePhase`，因为资产没有 activation time。要削 realclosure 的 `8/7/7` 峰值，必须通过 SGP-native 物理操作进入 board trace：target orientation flip、relay split、或 sidecar blocker，并保持 `U->S->H` bridge 静态 ray 与 trace 顺序。

## SGP Rhythm Lab Realclosure Peak Prune V1 - 2026-06-22

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已通过 step diagnostics 定位 realclosure 样本的 choice peak 根因：峰值不是 planned bridge hub `H9`，而是 step 7 清掉 chain `11` 后一次解锁 `14/23/24/26/28`，导致 step 8 choices 从 4 跳到 8。
- 已新增 `Build-ChoicePeakPruneVariants.ps1` 做诊断性物理削峰：从 burst target group 中移除单条链并 remap bridge ids。该脚本当前定位为 proof/diagnostic，不是最终量产生成器。
- 验证结果：`realclosure_peak_prune_v1_board_gate_accepted.csv` 中 4/5 变体通过 board trace gate；冻结正式包后再次复验 `hard_lane_realclosure_peak_prune_v1_frozen_board_gate_summary.md`，4/4 accepted。
- 冻结包：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLaneRealclosurePeakPruneV1Pack.asset`
  - Levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLaneRealclosurePeakPruneV1Frozen/`
  - Manifest：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_HardLaneRealclosurePeakPruneV1Pack.csv`
  - Gate summary：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_realclosure_peak_prune_v1_frozen_board_gate_summary.md`
  - Demo activePack GUID：`6d71ad8444744e75847e3debf2a96fc5`
- 指标范围：4/4 `solved=True`、`processTier=A`、`maxChoices=7`、`boardTraceChoiceMaxRise=3@s8`、`dependencyFollowRunMax=3`、`localPatchSolveRunMax=3`、`supportClosureBestDepth=2`、planned bridge replay `ok/d3`。
- 已修复 `Freeze-StageLockCandidatePack.ps1`：冻结 manifest 现在保留并回填 `traceAnchorBridgeTarget/Hub/Support/Upstream` 与 planned replay 字段，避免冻结包二次 trace 时丢失 bridge replay 输入。
- 当前判断：这是 realclosure 主线的实质性突破，证明“保留真实物理桥 + 控制中盘 unlock fanout”可以同时成立；但直接删除 burst 链会损 coverage/形状，只能作为 V1 proof。下一步应把 prune 升级成 `relay split` / `target stagger` / `bridge-safe relink`，在不降低覆盖的情况下把 `8/7/7` 波峰拆成两批释放。

## SGP Rhythm Lab Realclosure Relay Split V2 - 2026-06-22

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已按 GPT/Codex 共识验证 Fanout Dynamics V2：优先不删链、不抽象加 phase，而是把 fanout parent chain `11` 物理切成两段，让中盘 target release 真实错峰。
- 负结果：`Build-ChoicePeakStaggerVariants.ps1` 对 burst targets `14/23/24/26/28` 做直接翻向，25/25 均不可解，planned bridge `targetOrderFailed`。结论：target flip 太粗，会破坏已验证因果顺序，暂不作为主线。
- 正结果：`Build-ChoicePeakRelaySplitVariants.ps1` 对 chain `11` 做 relay split，40 个变体中 18 个可解，其中 4 个通过严格 board gate。
- 冻结包：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLaneRealclosureRelaySplitV2Pack.asset`
  - Levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLaneRealclosureRelaySplitV2Frozen/`
  - Gate summary：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_realclosure_relay_split_v2_frozen_board_gate_summary.md`
  - Demo activePack GUID：`6fbd38441fa644ad8d23d2fc98982a6b`
- Frozen board gate：4/4 accepted，全部 `solved=True`、`processTier=A`、planned bridge replay `ok/d3`、`dependencyFollowRunMax=3`、`localPatchSolveRunMax=3`、`supportClosureBestDepth=2`。
- 最好两关：split index `8`，orientation `a0b0`/`a1b1`，`maxChoices=5`、soft cap 5 hit `0`、`choice rise=2@s10`，choice wave 从原 `...4 8 7 7...` 变为 `...4 3 5 4...`，且覆盖不删格、链数从 34 增到 35。
- 当前判断：这是比 Prune V1 更接近正式生产能力的突破，证明“保 bridge + 不删覆盖 + 物理错峰释放”可行。下一步应将 relay split 从单点脚本升级为通用 fanout parent detector/repair：自动定位 high fanout parent，尝试切点/方向组合，用 board trace gate 选出 `maxChoices<=5/6` 且 coverage 不降的候选。

## SGP Rhythm Lab Realclosure Relay Split Auto V1 - 2026-06-22

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已将 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-ChoicePeakRelaySplitVariants.ps1` 从单行输入扩展为多行输入，新增 `-ProcessAllRows/-InputRowLimit/-InputRowOffset`，用于批量扫多个 realclosure orientation 源。
- 运行 `bridge_pressure_v1f10_realclosure_unique_graph_candidates_aggregate.csv` 的 8 个 orientation 源，先生成 320 个 parent `11` relay split 变体；整批 trace 超时后改为按 `sourceRowIndex` 分片跑。
- 分片结果：`s01` 和 `s02` 各 40 个变体，各自 4/40 通过严格 board trace gate。精选 5 关冻结为：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLaneRealclosureRelaySplitAutoV1Pack.asset`
  - Levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLaneRealclosureRelaySplitAutoV1Frozen/`
  - Gate summary：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_realclosure_relay_split_auto_v1_frozen_board_gate_summary.md`
  - Demo activePack GUID：`c8ef38d8d5b94e1abd69a23ebd54a6a0`
- Frozen board gate：5/5 accepted。4 关 `maxChoices=5`、soft cap 5 hit `0`、choice rise `2@s10`；1 关 `maxChoices=6`、soft hit `1`、choice rise `3@s12`。全部 `processTier=A`、`stageLockScore=0.601`、`causalAntiLocalityScore=0.281`、`dependencyFollowRunMax=3`、`localPatchSolveRunMax=3`、`supportClosureBestScore=0.374/d2`、planned bridge replay `ok/d3`。
- 当前判断：用户目标“至少先能 5 关真正有难度的关卡”已有第一版可看包；但仍属于同一 realclosure family/orientation 扩展，不能视为量产多样性完成。下一步应继续做 fanout parent detector：不再手写 parent `11`，而是从 trace/choice wave 自动定位 burst parent，并扩到更多 source/family。

## SGP Rhythm Lab Diverse Perturb Probe V1 - 2026-06-22

- 用户反馈 `HardLaneRealclosureRelaySplitAutoV1`：5 关几乎一模一样，且一眼看仍偏简单；该包不应视为“真正 5 关高难突破”。
- 复验历史候选：`hard_lane_diverse_retrace_v1_metrics.csv` 共 35 行全部可解，但只有 1 个 `MediumStructure`，且外圈头/外圈连消偏脏；外圈干净样本大多仍回到 `LocalEasy`。
- 全历史 high anti-local 扫描确认：非 `LocalEasy` 的高结构样本基本是 unsolved near-miss；既有 feasibility bridge 单翻/双翻 repair 失败（单翻 0 解，双翻 180 个仅 1 解且退回 `LocalEasy`），说明后验修 near-miss 不是当前高性价比路线。
- 通过 Rosetta/GPT 安全抽象咨询确认：下一步应做生成侧结构扰动/结构分布控制，而不是继续凑过渡包；最终 gate 仍以 board-level trace replay 为准。
- 最小结构扰动实验：对 12 个不同 SGP body 做单链/双链方向扰动，260 个候选中 70 个可解，6 个 `MediumStructure`；但 6 个全来自同一 source hash，且 `outerExitHeadCount=6-7`。对这些 MediumStructure 做外圈头 subset repair 120 个变体，0 个可解，证明该难结构依赖边缘阻挡关系。
- 当前 Demo 已挂 probe 包：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLaneDiversePerturbProbeV1Pack.asset`
  - Levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLaneDiversePerturbProbeV1Frozen/`
  - Frozen trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_diverse_perturb_probe_v1_frozen_metrics.csv`
  - Demo activePack GUID：`00da5863bfa34a7fa90dfd97d9026349`
- Probe 包指标：第 1 关 `MediumStructure/anti=0.471/maxChoices=6/local=3/follow=3/outerHead=6`；第 2/3 关 anti 较高但仍 `LocalEasy` 且外圈偏脏；第 4/5 关外圈干净但仍 `LocalEasy`。结论：这是诊断包，不是最终高难产线。
- 下一步真正方向：把“可解跨区支持链 + 外圈不白给 + 多 source structure signature”前移到 source/StageLock generation；单纯筛选、realclosure relay split 或后验外圈翻链都不足以稳定产出 5 个多样高难关。

## SGP Rhythm Lab HardLock Slot HeadFix V0 - 2026-06-22

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 关键纠偏：`Build-SGPRhythmTrace.ps1` 的运行时语义里链条头部是 `indices[0]`，head forward 由 `indices[0] - indices[1]` 得出；此前 `Build-HardLockSlotSGPFillV0.ps1` 和 overlay reserve 误把 `path[-1]` 当头部，导致保留的是尾部走廊，coverage 一高就破坏真实逃逸传播。
- 已修正 `Build-HardLockSlotSGPFillV0.ps1` 和 `Build-HardLockSlotSourceOverlayV0.ps1` 的 `Add-CriticalEscapeLaneReserve`；同时修正 filler 外出口头判断为 `path[0]/path[1]`。
- 验证结果：`hard_lock_slot_sgp_fill_headfix_v0` 在 `TargetCoverage=0.20` 小批 12 个候选中，7 个 `solved=True + processTier=A + TrueHardCandidate`；最好样本约 `hardStructureV3Score=0.763`、`causalAntiLocalityScore=0.667`、`supportClosureBestDepth=4`、`maxChoices=6`、`dependencyFollowRunMax=3`、`localPatchSolveRunMax=1`、`outerExitHeadCount=0`。
- 当前判断：今天的真正突破是“trace-visible hard-lock + SGP-style filler”已能稳定产出超过 5 个真高难候选；但 coverage 仍低（约 0.20），还不是量产视觉标准。下一步应在正确 head/corridor 语义上扩展 ray-aware SGP slot/fill，而不是回到 post-overlay 或盲目拉覆盖。

## SGP Rhythm Lab Trace-Delta Filler Compiler V1 - 2026-06-22

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已新增并验证 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-HardLockSlotTraceDeltaFillV1.ps1`：以 `HardLockSlotHeadFixV0` 真 hard-lock 低覆盖样本为骨架，只生成 slot-aware filler 候选，再用 board-level trace delta gate 接收/拒绝。
- 核心原则：`firstHit` 只作为诊断，不作为硬保持条件；filler 是否可接收由最终 trace 变化决定，包括 `solved/processTier`、`maxChoices/avgChoices`、`causalAntiLocalityScore`、`supportClosureBestDepth/Score`、`localPatchSolveRunMax`、`dependencyFollowRunMax` 和 `outerExitHeadCount`。
- Slot 模型采用三类候选：`bridge` 贴近 hard-lock/ray corridor，`noise` 位于 hub/core fanout 区，`edge` 只做低预算外圈/覆盖补肉；slot 只是候选空间收缩，最终仍以 board trace 为真值。
- 小步覆盖实验成功：从 0.20 左右逐步补到约 `0.261~0.2635`，冻结为：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLockTraceDeltaFillV1Cov265Final5Pack.asset`
  - Levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLockTraceDeltaFillV1Cov265Final5Frozen/`
  - Frozen trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lock_trace_delta_fill_v1_cov265_final5_frozen_metrics.csv`
- 冻结后 board trace：5/5 `solved=True`、5/5 `processTier=A`、4/5 `TrueHardCandidate`、1/5 `HardPotential`；`maxChoices` 最大 6，平均选择约 2.85，平均 `causalAntiLocalityScore` 约 0.67，平均 `hardStructureV3Score` 约 0.737，`supportClosureBestDepth` 最低 3，`localPatchSolveRunMax` 最大 2，`dependencyFollowRunMax` 最大 4，`outerExitHeadCount=0`。
- 当前判断：这是从“只有 hard-lock 骨架”迈向“可补肉且难度不坍塌”的第一组冻结成果，但覆盖仍偏实验级。下一步应按同一 trace-delta 预算继续小步推 `0.28/0.30`，并观察视觉是否仍有骨架感；若某父本补肉空间耗尽，优先换父本，不硬放宽难度门槛。

## SGP Rhythm Lab Directed Batch Fill V1 - 2026-06-22

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 和用户/GPT 对齐后的新主线：不要把 filler 设计成手摆闭环 module，也不要对每根链逐一硬判难度；改为 `SGP candidate geometry -> per-chain lightweight direction resolver -> batch-level board trace gate`。
- 已新增实验脚本：`.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-HardLockSlotDirectedBatchFillV1.ps1`。该脚本从 V2 group fill 派生，加入 directed target 生成：候选链的 head ray 通过已有 blocker target 点决定方向，再由 2-4 条链组成 batch，最终整批写入后用 board trace 验收。
- 关键实现判断：单链阶段只做方向/碰撞/外出口轻量筛选；`maxChoices/avgChoices/antiLocal/support/localRun/followRun` 作为 batch/final trace 的控制信号，不再作为每根链的即时审判。
- 正向证据：`hard_lock_slot_directed_batch_v1_b01_probe1` 在 b01 父本上成功突破 V1 单链 gate 卡点，从 `coverage=0.261` 推到 `0.277`，且 `solved=True`、`processTier=A`、`supportClosureBestScore=0.975/d4`、`outerExitHeadCount=0`、`localPatchSolveRunMax=1`。这证明“batch-level + 点控方向”能补进 V1 拒绝的组合。
- 风险证据：b02 初始 directed batch 可推到 `coverage=0.2782` 且 support/anti 不差，但 `avgChoices=4.41` 偏爽；加入 `MaxAvgChoicesCeiling=4.0` 后同样小批不再接受。说明 Directed Batch 需要继续加强 effective choice space 控制，而不是放宽 avg/max。
- 已修一个脚本健壮性 bug：组合候选中可能混入空 filler，导致 accepted 后复制 null path 崩溃；已过滤 null candidate/path。
- 当前判断：Directed Batch 是比“单根补链”和“手摆 module”更接近量产的主线，但还不是最终生产器。下一步应改进方向评分/候选池：优先选择能降低 independent path 的 blocker target，增加 batch diversity，强化 avgChoices 区间惩罚，同时保留 board-level trace 为最终真值。

## SGP Rhythm Lab Choice-Aware Directed Batch - 2026-06-22

- 用户提出关键问题：`avgChoices` 被补链拉高后，后续补链是否可以压回。当前结论：可以，但只有 `relay/guard` 型补链能压回；普通 coverage filler 往往继续增加独立选择空间。
- 已更新 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-HardLockSlotDirectedBatchFillV1.ps1`：
  - `avgChoices` 在过程门中改成“恢复债务”：允许小幅超过 `MaxAvgChoicesCeiling`，但最终 `final_selected` 必须无债务。
  - 默认 `MinGroupSize` 改为 1，允许小步 directed fill；大 batch 仍可由 trace 选择，但不强制每次 2 条起步。
  - 输出 `avgChoiceDebt`、`candidateTargetOwners`、`candidateParallelRisk`，用于后续分析选择空间污染。
- b02 验证结果：大组一步推到 `coverage=0.2782` 时 `avgChoices=4.53/max=8`，最终因债务未还被拒；小步 relay 单链推到 `coverage=0.2708` 时 `avgChoices=3.76/max=7`、`solved=True`、`processTier=A`、`supportClosureBestDepth=4`、`outerExitHeadCount=0`。说明“选择债务”模型成立，但恢复不能指望普通补肉自动发生，必须偏向 relay/guard 候选。
- 多父本 run2 在 b03 前耗时过长被停止；已完成片段显示 b01/b02 可通过小步 relay 保持 avg/max，但需要改成分父本 runner 或短 timeout，避免单个父本拖慢整批。
