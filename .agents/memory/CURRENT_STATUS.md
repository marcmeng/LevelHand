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

## AssetDatabase Trim - 2026-06-24

- 用户反馈项目实验产物太多、影响运行；先执行低风险 report-only 冷归档，不删除正式关卡/pack/脚本。
- 目标 worktree：`.worktrees/sgp-building-grammar`。
- 已将 `Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/` 及 `SGPRhythmLab.meta` 移出 `Assets`，归档到 `_AssetArchive/20260624_assetdatabase_trim/Reports/SGPRhythmLab/`。
- manifest：`_AssetArchive/20260624_assetdatabase_trim/manifests/sgp_building_grammar_reports_sgprhythmlab_manifest.csv`。
- summary：`_AssetArchive/20260624_assetdatabase_trim/manifests/sgp_building_grammar_reports_sgprhythmlab_summary.md`。
- 效果：`sgp-building-grammar` 的 `Assets/ArrowMagic/SOData/Reports` 从 8238 个文件降到 102 个；`Assets/ArrowMagic/SOData` 从 21854 个文件降到 13718 个。
- 下一步建议：先做 pack/scene 引用审计，再移动未被当前 Demo/review pack 引用的旧 `Levels/SGPRhythmLab` 候选目录；不要直接删除。

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

## SGP Sandwich Failure-Pocket Scheduler Probe - 2026-06-25

- 新 clean worktree：`.worktrees/sgp-sandwich-refill`，branch `codex/sgp-sandwich-refill`；用于隔离 sandwich/refill 产线实验。
- 已新增 `Build-FailurePocketExtractorV1.ps1`：从 SGP growth report 自动抽取 failure pocket anchor owners、added owners、direct-exit owners，并输出下一轮 `FailurePocketAnchorMode/Owners` 命令片段。
- Extractor 回放验证成功：r7 自动复现 `30;42;44;45;46;47`，r8 自动复现 `30;42;44;45;46;47;48;49`；r9 hard-anchor 0 新增链，正确标记同 pocket capacity boundary 并建议 Reward/换 pocket。
- Reward 过渡验证：从 r8c02 hard-anchor 边界切 Reward 可继续长到 coverage `0.5846`，`solved=True/supportDepth=4`，但新增链为 direct-exit opener，processTier 掉到 B；说明 Reward 可暴露下一 failure pocket，不能裸接受。
- 旧 `Build-WavePeelReleaseScaffoldGroupV0.ps1` 用于 Reward 口 source-slot refill 在该场景下 2/4 链版本均超时，确认随机 refill 是当前效率瓶颈。
- 已新增 `Build-FailurePocketFastRewriteV1.ps1`：确定性枚举 Reward direct-exit source slot 内的 head/neck，将单个 direct-exit opener 改写为 first-hit base owner 的 scaffold，避免随机搜索。
- Fast rewrite 验证：两个 Reward direct-exit 口同时改写会 `unsolved`；单口改写可行。owner50 单口 best `coverage=0.5577/solved=A/supportDepth=4`；后续 Reward 再暴露 owner51/52，单口改写 owner52 可 4/4 solved/A/supportDepth=4。
- 当前判断：sandwich line 具备继续挖的价值，但不是“一次 refill 堵所有口”；应采用 `Reward暴露口 -> 单口 fast rewrite commit -> 再 Reward` 的 failure-pocket scheduler。核心未解问题是吞吐：单口 commit 每轮 coverage 增长较小，需做 pocket selection / 多口兼容性预测，避免两个口一起改写导致 release 死锁。
- 已新增 `Invoke-FailurePocketSchedulerV1.ps1`：自动执行 `Reward SGP -> growth report -> 选单个 direct-exit pocket -> fast rewrite -> trace accept -> commit` 的单口原子调度闭环。
- Scheduler V1 smoke：从 r8c02/anchors `30;42;44;45;46;47;48;49` 自动 commit 3 个 pocket（50、51、52），全部回到 `solved=True/processTier=A/supportDepth=4`；coverage 从 `0.5538 -> 0.5577 -> 0.5615 -> 0.5654`。Reward 中间态可到 `0.58-0.59` 但为 B/outer 变高，rewrite commit 后质量恢复但 coverage 回落。
- 当前关键结论：V1 证明“单口 scheduler 可稳定”，但吞吐偏低；下一步应研究 pocket selection / rewrite 保留率 / 局部多口兼容性，而不是继续扩大 Reward 或一次性多口 rewrite。
- 2026-06-26 继续验证目标“接近 0.9 覆盖”：将 scheduler 加上多 Reward/多单口试探、B-tier debt 接受条件、可配置 rewrite 长度和真实 committed owner anchor。len6 单口 rewrite 可从 `0.5654` 提到 `0.5769` 且 `8/8 solved/A/supportDepth4`；继续多轮后可到 `0.6038 A/supportDepth4`、`0.6135 B/supportDepth4`、`0.6192 B/supportDepth4`，outer 约 `9`，max 约 `10-12`。
- 关键负结果：从 `0.6135 B/supportDepth4` 直接 SGP 冲 `0.70/0.84` 会 `supportDepth=0/unsolved`；`0.66` 附近已掉到 `supportDepth=2/0`。因此“先补满到高覆盖再修”在当前 SGP/Reward 下不可行。
- 关键负结果：混合保留 raw SGP chains 能到 `0.63-0.65` 且有时 `solved/supportDepth4`，但多为 `Drop`；keep-one/keep-two raw chain 后续会直接 `supportDepth=0`。raw SGP debt 不能作为可长期保留结构。
- 关键正结果：pair rewrite 不是原则失败，失败主要来自组合搜索不足。新增 `EnumerateOptionCombos` 后，`o56+o59` 的前 40 个组合找到 `0.6154/B/supportDepth4/outer9/max10` 的双口兼容候选；但继续增长仍慢，当前最高稳定约 `0.6192 B/supportDepth4`。
- 当前判断：sandwich scheduler 仍有价值，但当前 production V1 不是接近满覆盖路线；硬瓶颈是 `0.62` 后 SGP 新增链会破 support closure，且 fast rewrite 的可保留覆盖太低。下一步若继续本线，应做“组合式 rewrite scheduler + support-aware pair/top-k search”，或重新定义让 SGP 生成阶段避免 0.65 后的 support-breaking raw chains。

## SGP Small Sandwich Probe - 2026-06-25

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，只动试验分支资源，未切主项目正式包。
- 小关卡验证从 `swv2_r4_sgp_step2_from_r3c09_b01_c01` 出发，使用 `Build-SeededDirectSGPFillBaselineV1.ps1` 做 `MaxNewChains=2` 的小步 SGP burst。
- 产物 `small_sandwich40_step2_probe` 共 4 个候选，4/4 Greedy solved 且 `supportClosureBestDepth=4`；最佳过程样本 `b01_c04` 为 20x26、42 链、coverage `0.5019231`、`avgChoices=3.98`、`maxChoices=9`、`processTier=A`、`localPatchSolveRunMax=2`、`nearOuterPatchSolveRunMax=1`、`outerExitHeadCount=9`。
- 继续补满 probe：从 42 链样本直接冲 coverage `0.70` 全部 Greedy 失败；LDF `0.60` 可解上限约 49-53 链、coverage `0.604-0.619`，但外出口和可选数明显上升；从 53 链继续冲 `0.66/0.68` 全部不可解。
- 已新增小步语义接受层脚本：`.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SmallSemanticSlotFillV0.ps1`。它不重写 SGP，只封装 `Build-SeededDirectSGPFillBaselineV1.ps1`：每轮小步补肉 -> 跑 trace -> 只接受 solved 且外出口/选择数/local/dependency 未爆的候选。
- V0 完整 run：`small_sandwich_semantic_slot_v0_light_t063`，3 轮接受，从 coverage `0.5019231` 逐步到 `0.5538462`、`0.5884615`、`0.6269231`；最终 50 链、`avgChoices=6.28`、`maxChoices=16`、`outerExitHeadCount=16`、`localPatchSolveRunMax=2`、`dependencyFollowRunMax=10`。报告：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/small_sandwich_semantic_slot_v0_light_t063_summary.md`。
- V0 第 4 轮继续冲 `0.66` 有可解候选，但全部 `outerExitHeadCount=19` 且 `avgChoices≈8+`，因此按外出口/节奏目标不接受。说明轻量语义接受层能小幅突破直接补肉上限，但还不能质变。
- 当前 Demo 对比 pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SmallSandwich40ReviewPack.asset`，worktree Demo 已指向该 pack；Level 1/2/3/4 分别为 42 链 base、49 链较稳直接补肉、53 链最高直接补肉、50 链 semantic-slot V0。
- 结论：小步补链不会立刻破可解/局部顺消，但“直接补满”会快速制造外出口和选择爆炸；当前小内核的可解补肉上限约 coverage `0.62`。下一步应转向 trace-guided slot/edge grammar/repair，而不是继续盲目提高 target coverage。

## SGP Rhythm Lab HardLock 0.30 Production Chain Proof - 2026-06-23

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 目标已更新为“跑通 0.30 左右覆盖率的完整高难生产链路”，不只做父本分类；今晚必须证明 `parent -> directed/pressure fill -> board trace -> frozen pack` 可以闭环。
- 已从 `HardLockDirectedBatchPressure/Choke` 父本继续推到约 `0.30`：直接从低覆盖父本冲 0.30 过慢且会卡承压上限；从 `0.2929` near-miss 父本继续补肉会得到 0.30+ 未解候选，说明 0.30 附近是真实边界。
- 新增/使用 near-miss orientation rescue 后，得到 15 个 `solved=True` 的 0.299-0.309 候选；残余模式分两类：低 avg/max 但静态 `outerExitHeadCount=1`，或 `outerExitHeadCount=0` 但 `avgChoices≈4.17`。
- 后验单外口翻转实验：对低 avg 的两个 0.30 候选翻掉唯一 outer-exit head 后，`outerExitHeadCount=0`、avg/max 更低，但 `solved=False`；结论是该 outer head 在当前几何中承担可达性/支撑作用，不能作为纯脏链后验翻掉。
- 后验 pressure/choke rescue 实验：对 `outerExitHeadCount=0` 但 avg 偏高的两个 0.30 候选短预算补肉，0 个可用候选；结论是当前几何空间已接近承压上限，不能靠后验补链强行压回。
- 冻结 0.30 proof/review 包：`SGPRhythmLab_HardLock030ProofReview5Pack.asset`；5/5 frozen board trace solved/A-tier，但混合了“outer=1 低选择”和“outer=0 avg debt”两类，不作为最终 production gate。
- 进一步采用动态外口压力 gate，而不是静态 `outerExitHeadCount==0`：允许 `outerExitHeadCount=1`，但要求 `outerExitAvailableChoiceMax<=1` 且 `outerExitSolveRunMax<=1`。冻结包：`SGPRhythmLab_HardLock030DynamicOuterGate5Pack.asset`。
- `HardLock030DynamicOuterGate5` frozen trace：5/5 `solved=True`、5/5 `processTier=A`、coverage 均约 `0.306+`、`avgChoices=3.86-4.0`、`maxChoices=6`、`supportClosureBestDepth=3`、`localPatchSolveRunMax=3`、`dependencyFollowRunMax=4`、`outerExitAvailableChoiceMax=1`、`outerExitSolveRunMax=1`。
- 当前判断：0.30 完整链路已跑通，真正下一步不是继续后验救单关，而是把“动态外口压力 + avg choice pressure + parent capacity”前置到父本/填充选择，扩父本池解决多样性和稳定产能。

## SGP Rhythm Lab Dynamic Outer Gate Implementation - 2026-06-23

- 已按 GPT/用户审稿结论把动态外口压力 gate 落到 directed batch fill 主脚本：`.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-HardLockSlotDirectedBatchFillV1.ps1`。
- 新增显式参数：`MaxOuterExitHeadCount`、`MaxOuterExitAvailableChoiceMax`、`MaxOuterExitSolveRunMax`、`MaxSameSideOuterExitSolveRunMax`。默认仍为旧严格模式（outer head 必须 0），只有显式传参时才允许动态受控 outer head。
- batch accept 与 final accept 均改为 `Test-DynamicOuterPressure`：静态 outer head 只负责触发风险检查，真正验收看动态外口可见选择和外口连续消是否低。
- 输出 CSV 现在保留 `outerExitAvailableChoiceMax`、`outerExitSolveRunMax`、`sameSideOuterExitSolveRunMax`，summary 会记录动态外口 gate 参数，便于明早产线筛选。
- smoke：对 `HardLock030DynamicOuterGate5` 第一父本用 `outerHead<=1 / outerExitAvailableChoiceMax<=1 / outerExitSolveRunMax<=1` 小预算运行成功，脚本和参数链路正常；该父本 0.306 后无可用继续补肉候选，说明下一步应扩父本池而不是硬推同一父本。
- 下一步量产命令应从可承压父本池分片跑 directed batch fill，并显式启用动态外口 gate，同时保留 `avgChoices<=4.0`、`maxChoices<=6/7`、`supportClosureDepth>=3`、`localPatchSolveRunMax<=3`、`dependencyFollowRunMax<=4`。

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
- 已进一步加入 `Choice Pressure Classifier`：
  - `relay/guard` 视为 pressure filler，`locked` 更接近 expansion/neutral。
  - 当当前 `avgChoices >= 3.4` 时进入 pressure mode，候选组必须 100% pressure；正常状态下也要求 pressure 比例下限。
  - 新增 `BaseRowOffset`，支持按父本分片跑，避免一个慢父本拖住整批。
- b02 验证结果：大组一步推到 `coverage=0.2782` 时 `avgChoices=4.53/max=8`，最终因债务未还被拒；小步 relay 单链推到 `coverage=0.2708` 时 `avgChoices=3.76/max=7`、`solved=True`、`processTier=A`、`supportClosureBestDepth=4`、`outerExitHeadCount=0`。说明“选择债务”模型成立，但恢复不能指望普通补肉自动发生，必须偏向 relay/guard 候选。
- 多父本 run2 在 b03 前耗时过长被停止；已完成片段显示 b01/b02 可通过小步 relay 保持 avg/max，但需要改成分父本 runner 或短 timeout，避免单个父本拖慢整批。
- Pressure Final5 冻结成果：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLockDirectedBatchPressureFinal5Pack.asset`
  - Levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLockDirectedBatchPressureFinal5Frozen/`
  - Selected CSV：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lock_slot_directed_batch_v1_pressure_final5_selected.csv`
  - Frozen trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lock_directed_batch_pressure_final5_frozen_retrace_metrics.csv`
  - 指标：5/5 `solved=True`、5/5 `processTier=A`、3 `TrueHardCandidate` + 2 `HardPotential`、`avgChoices` 平均 `3.28` 最大 `3.76`、`maxChoices` 最大 `7`、`causalAntiLocalityScore` 最低 `0.6` 平均 `0.646`、`supportClosureBestDepth` 最低 `3`、`localPatchSolveRunMax` 最大 `2`、`dependencyFollowRunMax` 最大 `4`、`outerExitHeadCount=0`。
- 当前判断：这是比 cov265 更接近“补满且不变简单”的 5 关小包，证明 pressure filler 比例控制有效；仍不是最终量产完成，因为父本仍来自同一 hard-lock/cov265 lane，多样性和更高 coverage 需要继续扩 source 与 guard 库。

## SGP Rhythm Lab Choke O1 Directed Batch - 2026-06-22

- 在 `Build-HardLockSlotDirectedBatchFillV1.ps1` 上新增 choke filler 初版：候选链若被已有逃逸 ray 命中次数 `incomingRayHits >= 2`，标记为 `choke`，并纳入 pressure filler；高 avgChoices pressure mode 下要求一定 choke 比例。
- 新增动态保险：`MaxDeltaOpeners=0`，即补链后的 board trace `openers` 不允许比补链前增加。当前理解：`incomingRayHits` 是静态压缩信号，`openers` 不增长是最低动态验证，避免“假 choke”变成新独立入口。
- 5 父本试跑 `hard_lock_slot_directed_batch_v1_choke_o1_final5_try1`：4/5 父本成功补肉；b03 换 seed、扩大候选池仍 0 通过，判定为“不适合继续补肉”的父本，不硬塞。b04/b05 能二轮补肉到 `coverage=0.2806/0.277` 且 avg choices 仍低。
- 冻结混合 5 关包：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLockDirectedBatchChokeO1Final5Pack.asset`
  - Levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLockDirectedBatchChokeO1Final5Frozen/`
  - Mixed selected CSV：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lock_slot_directed_batch_v1_choke_o1_final5_mixed_selected.csv`
  - Frozen trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lock_directed_batch_choke_o1_final5_frozen_retrace_metrics.csv`
- Frozen trace 指标：5/5 `solved=True`，`avgChoices` 平均 `3.146`、最大 `3.76`，`maxChoices` 最大 `7`，`causalAntiLocalityScore` 最低 `0.6`，`supportClosureBestDepth` 最低 `3`，`localPatchSolveRunMax` 最大 `2`，`dependencyFollowRunMax` 最大 `4`，`outerExitHeadCount=0`。
- 当前判断：Choke O1 证明“补链可以提高 coverage 而不稀释难度”的方向成立；下一步不是继续单纯加链，而是做 `effective escape path / choice reduction` 更强的动态验证，并扩大父本池，跳过 b03 这类可补性耗尽父本。

## Seed Parent / Excellent Skeleton Analysis O1 - 2026-06-22

- 用户纠偏：骨架应该从优秀 seed 中抽，不应先看 coverage。已采纳为 Choke O2 父本分析原则。
- 已跑内部 `Assets/ArrowMagic/SOData/Levels/Seeds` 全量 951 seed 结构画像：`StandardSeedLike=604`、`R1ABFinal=176`、`R2RefillOrVariant=171`。R1 修复平均 coverage 约 `0.850` 且长链/carrier 基本保持；R2 二次补肉 coverage 仅小幅升至约 `0.860`，但 `avgChain/longRate/veryLong/carrier` 均下降。
- 仅按静态骨架/coverage 选出的 top80 父本已跑 board trace：80/80 solved，但平均 `openers=6.75`、`avgChoices=4.15`、`maxChoices=8.39`、`causalAntiLocalityScore=0.261`、`localPatchSolveRunMax=4.64`，说明“骨架漂亮/覆盖高”不等于动态难。
- 已生成动态优秀 seed 骨架候选：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/excellent_seed_skeleton_candidates_o1_20260622.csv`。当前优先拆解对象包括 `seed_Above300_level_664`、`seed_Arrowz_level_036`、`seed_Above300_level_891`、`seed_Arrowz_level_023`、`seed_Above300_level_467` 等。
- 下一步：先对这些动态优秀 seed 抽骨架共性（选择压力、跨区因果、非局部依赖、低 local run），再用这些共性反哺 SGP Directed Fill / Choke O2；coverage 只作为“可扩张容量”指标，不再作为骨架优先级主指标。

## SGP Rhythm Lab Parent Capacity + Dynamic Repair 0.30 Proof - 2026-06-23

- 当前实验 worktree：`.worktrees/sgp-rhythm-lab`，branch `codex/sgp-rhythm-lab`。
- 已把动态外口压力 gate 前移到 `Invoke-ParentCapacityProbeV1.ps1`，并同步到 `Repair-NearMissFillerOrientationV1.ps1`；静态 `outerExitHeadCount` 现在是风险信号，正式 gate 看 `outerExitAvailableChoiceMax/outerExitSolveRunMax/sameSideOuterExitSolveRunMax` 是否都 <=1。
- 父本探针结论：`hard_lock_slot_directed_batch_v1_pressure_final5_selected.csv` 中多数 0.27-0.28 父本已接近补肉上限；`hard_lock_slot_trace_delta_fill_v1_cov265_final5_selected.csv` 里的 0.265 父本可先推到 0.277/0.284，但直接冲 0.30 会出现 `unsolved` near-miss。
- 关键闭环：从 `pcdo265p1_p05_s937101_b01_r1_c34` 中间父本（coverage `0.277`、avg `2.91`、max `6`、anti `0.714`、support depth `4`、outer=0）继续 directed fill，得到一批 `0.299~0.300` near-miss；再用新增 filler 组方向翻转救援，动态外口 gate 下 51 variants -> 12 accepted。
- 当前最佳 accepted：`pc34repair_dyn1_v003`，coverage `0.3002451`、avgChoices `3.58`、maxChoices `6`、causalAntiLocality `0.7`、supportClosure `0.935/d3`、localPatchRun `2`、dependencyFollowRun `4`、outerExitHead `1` 且动态外口压力均为 `1`。
- 关键产物：
  - Accepted CSV：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pc34repair_dyn1_accepted.csv`
  - Source near-miss CSV：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pc34to30p2_rejected_steps.csv`
  - Intermediate parent feed：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pcdo265p1_p05_c34_parent_feed.csv`
  - Repair output levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RP/pc34repair_dyn1/`
- 当前判断：今晚已经跑通“父本池 -> 小步 directed fill -> 0.30 near-miss -> orientation repair -> board trace accepted”的生产链路；下一步不是继续单父本硬推，而是把该链路批量化并做 family/source 去重，目标是生成 5 个视觉不重复的 0.30 accepted 关卡包。

## SGP Rhythm Lab Parent Capacity O1 Review5 - 2026-06-23

- 已把 `Invoke-ParentCapacityProbeV1.ps1` 从“direct fill 预筛”升级为完整自动链路：`parent -> directed batch fill -> high-coverage near-miss -> Repair-NearMissFillerOrientationV1 -> A parent pool`。新增参数包括 `RunNearMissOrientationRepair`、`RepairMinCoverage`、`RepairMaxCandidates`、`MinCoverageGainForA/B`，并支持从 LevelDefinition asset 回读空缺 `coverage`。
- 回归 1：用 `pcdo265p1_p05_c34_parent_feed.csv` 的 0.277 中间父本，自动得到 9 个 `coverage>=0.299` near-miss，51 个 repair variants -> 12 accepted，A pool=1；top `pcauto1_p01_s937257_rp_v003`，coverage `0.3002451`、avg `3.58`、max `6`、anti `0.7`、support `0.935/d3`、local `2`、follow `4`。
- 回归 2：从原 `hard_lock_slot_trace_delta_fill_v1_cov265_final5_selected.csv` 第 5 父本直跑，自动先补到 `0.277/0.283`，再修 0.299 near-miss；14 variants -> 4 accepted，`pcorig1` A pool=1，best coverage `0.2990196`、avg `3.47`、max `7`、anti `0.645`、support depth `4`。
- 父本池验证：对 cov265 另外 4 个父本并行探针，p01 为 `B_hard_but_limited`（最高 `0.2782`），p02/p03/p04 为 E；说明同一批 hard-lock 父本并非都能承压到 0.30，先扩 A 父本池是必要路线。
- 新增原始 headfix 骨架探针：`hard_lock_slot_sgp_fill_headfix_v0_base_09` 从 `0.2059` 补到 `0.2843`，再修 0.299 near-miss；6 variants -> 2 accepted，`pcseed06a` A pool=1。`mirrorxy_04` 补到 `0.2806` 但无 0.30 repair accepted，暂归 B。
- 已冻结 5 关 review 包：
  - Pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_ParentCapacityO1Review5Pack.asset`
  - Levels：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/ParentCapacityO1Review5Frozen/`
  - Selected CSV：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/parent_capacity_o1_review5_selected.csv`
  - Frozen trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/parent_capacity_o1_review5_frozen_metrics.csv`
- Frozen trace：5/5 `solved=True`、5/5 `processTier=A`，selected coverage 均 `0.2990196`；`avgChoices` 最大 `3.94`、`maxChoices` 最大 `7`、`causalAntiLocalityScore` 最低 `0.645`、`supportClosureBestDepth>=3`、`localPatchSolveRunMax<=1`、`dependencyFollowRunMax<=4`、动态外口压力 `outerExitAvailableChoiceMax/outerExitSolveRunMax<=1`。
- 当前判断：0.30 量产链路已能自动跑通并冻结 5 关，但多样性仍不足（当前 5 关来自 2 个 A 父本）。明早优先继续 parent mining：从更多 hard-lock true-hard / 优秀 seed-derived 骨架中扩 A pool，再做 family/source 去重和 Demo 人工复核。

## SGP Rhythm Lab Mixed Family Production O1 - 2026-06-23

- 已验证 GPT/本地一致结论：父本筛选应先扩结构 family，再推单一父本覆盖率；seed-derived 几何骨架不是今晚主线。
- 新增 `Build-CausalSeedSkeletonParentsV1.ps1` 作为 seed-derived causal skeleton 诊断脚本；V1 几何抽骨架 14 个仅 1 个 non-LocalEasy，V2 causal-first antiLocal/openers 有改善但仍仅 1 个 non-LocalEasy，direct capacity probe 0 finals。结论：直接从优秀 seed 抽低覆盖骨架会丢因果锁，后续需真正 parentOf/causal subgraph extractor 才继续。
- 修正 `Build-SkeletonConstraintAdapterV1.ps1`：layout 不再固定 `% 3`，可生成更多 DependencySkeletonV3 / RoomDoorSkeletonV2 family 变体。
- 语义骨架扩展结果：`skeleton_family_o2_dependency_v3_cov30` 12 个中 1 HardPotential + 4 MediumStructure；`skeleton_family_o2_roomdoor_v2_cov30` 12 个中 3 HardPotential + 7 MediumStructure。说明 semantic skeleton 是有效第二 family，但不是 supportClosureDepth>=3 的同一难度范式。
- 已采用双 gate：causal-depth hard-lock family 继续要求 `supportClosureDepth>=3`；semantic choice-compression family 不以 support closure 作硬门槛，而要求 `hardStructure!=LocalEasy`、`avg<=3.5`、`max<=7`、`anti>=0.6`、`localPatch<=2`、动态外口压力<=1。
- 冻结混合 review 包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_MixedFamilyProductionO1Pack.asset`。
- 混合包 frozen trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/mixed_family_production_o1_frozen_trace_metrics.csv`；9/9 solved，3 TrueHardCandidate、5 HardPotential、1 MediumStructure，来源包括 hard-lock causal、DependencySkeletonV3、RoomDoorSkeletonV2。
- 下一步：把 mixed family pack 给用户人工看；若体感通过，做 production scheduler/family cap，把 causal-depth 和 semantic-compression 两条线按比例出货。

## User Review - Mixed Family Production O1 - 2026-06-23

- 用户人工反馈：前 5 个 causal-depth hard-lock 候选确实有难度，方向成立。
- 主要问题：重复度高且不是“同构相似”，而是第 1/2 关一模一样、第 3/4/5 关一模一样；这是选包/冻结阶段近重复资产没有被 dedupe 挡住，不能作为量产多样性证明。
- 用户人工反馈：第 6 关及之后的 semantic-choice-compression 候选体感完全不行，视觉上非常稀疏/简单；说明当前 semantic gate 误收，不能按现有指标进入 production。
- 待合并 GPT 反馈后再定下一步；当前本地倾向是保留 causal-depth hard-lock family，收紧去重/父本 cap，暂停或重做 semantic-compression gate。


## GPT Review + Project Decision - Mixed Family Production O1 - 2026-06-23

- GPT 审稿同意 causal-depth hardlock 应作为 production backbone，并指出当前 A-class 正在单 family 收敛，需要 family/skeleton dedup；这一点与项目侧判断一致。
- GPT 建议 semantic-choice-compression 可作为辅助 family，但用户试玩明确反馈第 6 关及以后完全不行；项目侧以人工体感为准，判定 semantic gate 误收，暂不进入 production。
- 关键差异：GPT 按低 avg/max、antiLocal 判断 semantic 可用；项目侧认为这些样本是“低选择但低结构密度/低链数/大空白”的假难，说明现有 semantic gate 缺少视觉密度、链数/结构块完整度和 exact/near duplicate 控制。
- 下一步决策：短期量产只走 causal-depth hardlock family；新增 exact geometry signature dedupe + parent/source cap，避免第 1/2 和第 3/4/5 这种完全重复进入 review/production 包。
- semantic-choice-compression 保留为诊断/后续重做 gate，不作为明天 20-30 关生产池来源。

## Causal Hardlock Diverse Review V1 - 2026-06-23

- 用户确认 MixedFamily O1 前 5 个 causal-depth hardlock 有难度，但 1/2 完全一样、3/4/5 完全一样；semantic 6+ 体感失败，暂停 production。
- 新增 `Select-CausalHardlockDiverseCandidatesV1.ps1`：从 accepted CSV 合并 causal hardlock 候选，按 `coverage/avg/max/anti/support/local/follow/dynamic outer` gate 筛选，并计算 exact geometry hash；每 parentGroup/geometryHash 默认最多 1 个。
- 严格 0.299+ 去重结果：30 个 raw candidates 只能选出 4 个不同父本/几何，冻结为 `SGPRhythmLab_CausalHardlockDiverseReviewV1Cov30Pack.asset`；4/4 frozen trace solved/A，3 TrueHardCandidate + 1 HardPotential。结论：现有 0.30 A pool 不够 8-12 个，多样性不足，不能直接量产 20-30。
- 0.28+ 多样性诊断结果：70 个 raw candidates 选出 12 个不同父本/几何，冻结为 `SGPRhythmLab_CausalHardlockDiverseReviewV1Cov28Pack.asset`；12/12 frozen trace solved/A，11 TrueHardCandidate + 1 HardPotential。Demo 已挂该包用于人工判断不同父本是否值得继续推到 0.30。
- 下一步：人工看 Cov28 多样性包；通过的父本再跑 parent capacity/near-miss repair 推到 0.30，目标扩出至少 8-12 个不重复 0.30 A 类父本后再做 20-30 小批量。

## User Review - CausalHardlockDiverseReviewV1Cov28 - 2026-06-23

- 用户试玩反馈：整体难度在线，说明 causal-depth hardlock 多父本路线成立。
- 问题 1：第 1/2 关仍重复，留 1 个即可；当前 exact geometry hash + parentGroup 仍不足以挡住“视觉重复/近重复”。
- 问题 2：第 10/11 关右上角存在 3 个连续消除，需在后续选择/trace gate 中加强局部连续消除或角落连续消除惩罚。
- 问题 3：部分父本只是个别链条不一样，属于 near-duplicate family；下一版 review 包需要增加更粗的 visual/occupancy/skeleton signature，而不只 exact geometry hash。
- 下一步：从 Cov28 包中保留难度成立样本，但新增 visual-near-dedup、每 base/root skeleton 限制和 corner/local run reject，再挑父本继续推 0.30。

## CausalHardlockDiverseReviewV2Cov28 - 2026-06-23

- 针对用户反馈“1/2 重复、10/11 右上角三连消、部分父本只是个别链不同”，已在 `Select-CausalHardlockDiverseCandidatesV1.ps1` 增加 `MaxOccupancyJaccard` 和 `ManualRejectLevelIds`。
- 旧 Cov28 12 关包的占用 Jaccard 证实重复：1/2=0.971、10/11=0.976、6/8=1.0；说明 parentGroup/exact geometry hash 不足以防 near-duplicate。
- 新 V2 选择：`MaxOccupancyJaccard=0.90` 并手动排除用户点名的 `pcauto1_p01_s937257_rp_v044`、`pc34repair_dyn1_v009`；最终 68 raw -> 7 selected，7 个不同父本/几何。
- 冻结包：`SGPRhythmLab_CausalHardlockDiverseReviewV2Cov28Pack.asset`；Demo 已挂该包。Frozen trace：7/7 solved、7/7 A，6 TrueHardCandidate + 1 HardPotential；最大 pair occupancy Jaccard=0.766。
- 当前判断：V2 是更可信的多父本基座包；若人工体感通过，下一步把这 7 个父本分别推到 0.30，并继续扩 A pool，而不是从单父本重复抽样。

## User Review - Skeleton Family Definition Gap - 2026-06-23

- 用户反馈：当前 V2 关卡难度已经足够，但多个父本的底层逻辑仍然很像，真实骨架可能只有 1-2 个；其余是同一基础结构上改个别链条、翻转或 repair 后的变体，例如 1 和 6。
- 当前 dedup 只覆盖 exact geometry、occupancy near-duplicate 和 parentGroup，不足以识别“同一 causal skeleton family”。
- 下一步需要定义并实现 skeleton family signature：不能只看链条资产/占用相似度，必须结合 causal dependency topology、macro region flow、solve rhythm 和 chain language。

## Causal Skeleton Signature Gap - 2026-06-23

- GPT 顾问补充并被项目侧采纳：除因果骨架、空间骨架、节奏骨架、链条语言外，还需要 `dependency interaction density`，判断多个骨架/锁点之间是否互相耦合。
- 当前 causal hardlock 家族难度成立，但真实 skeleton species 仍可能只有 1-2 个；下一步不能继续只做 fill/trace，而要做 causal skeleton signature extractor 和 diversity generator/sampler。

## Causal Skeleton Signature V1 - 2026-06-23

- 用户确认当前 causal-depth hardlock 难度已经足够，但多个父本底层逻辑仍高度相似，真实骨架可能只有 1-2 个。
- 新增脚本：.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-CausalSkeletonSignatureV1.ps1，把关卡分成三层：coreSignature（细差异）、speciesSignature/skeletonFamily（子家族）、macroSkeletonSignature（解题流程宏型）、ootSkeletonSignature（最高层结构盆地）。
- 已跑当前 V2 Cov28 7 关包：7 core / 5 skeleton family / 4 macro / 1 root skeleton。旧 V1 Cov28 12 关包：12 rows / 9 core / 6 family / 4 macro / 1 root。Cov30 4 关包：4 rows / 2 core / 2 family / 2 macro / 1 root。
- 结论：当前产线能稳定产“有难度的关”，但仍在同一个 root skeleton 盆地里采样；继续只做 occupancy/parent/geometry 去重不能解决结构物种多样性。
- 已扩展 Select-CausalHardlockDiverseCandidatesV1.ps1：可选读取 -SignatureCsv，并支持 -MaxPerSkeletonFamily、-MaxPerMacroSkeleton、-MaxPerRootSkeleton，默认不影响旧流程。
- 下一步：不要继续在同 root 里补变体；应做 causal skeleton diversity generator/sampler，主动生成不同 root skeleton（例如单 hub/双 hub、ring-to-core/core-to-ring、串联 hub、交叉 hub、不同节奏 wave）。

## Causal Skeleton Species Sampler V1 - 2026-06-23

- 新增 `Build-CausalSkeletonSpeciesSamplerV1.ps1`，用于生成不同 causal skeleton species 的低覆盖 source/probe，而不是继续在旧 hardlock root 内做变体。
- `causal_skeleton_species_sampler_v1_cov235` 生成 12 个 source species；board trace 12/12 有结果，signature 显示 12 core / 12 skeleton family / 12 macro / 12 root，证明“不同 root skeleton 可生成”这个方向成立。
- 但这些 source 多数还只是 Medium 或未解：可解且 supportDepth>=3 的主要有 `tri_branch_base` 与 `web_four_mirrory`，尚不能直接作为 production hard pack。
- 将可解 depth>=3 source 送入现有 directed fill 后，`web_four_mirrory` 可被补成 hard-like 候选，但 final signature 变成旧 root `db4cc18089`；`tri_branch_base` 单独 directed fill 0 accepted。
- 当前关键结论：新 species 生成器已证明结构物种空间可展开，但现有 directed fill/pressure gate 会把可用样本吸回旧 causal-depth hardlock root。下一步必须加 species-preserving / root-collapse gate，再做 species-specific fill，而不是继续把新 source 直接丢进旧 fill 产线。

## Species-Preserving Evaluation Gate V1 - 2026-06-23

- 新增 `Select-CausalSkeletonSpeciesPreservingCandidatesV1.ps1`：读取 candidate CSV + final signature CSV + 可选 source signature CSV，输出 selected/rejected/summary；支持 denied root、source-root preservation、weak root reject 和 hard metrics gate。
- 验证 1：`causal_species_directed_fill_v1_try1_salvaged` 的 hard-like 候选 final root=`db4cc18089`，被 `DenyRootSkeletonHashes db4cc18089` 正确拒绝，确认“回旧 root 的 hard 候选不能算新 family”。
- 验证 2：`causal_species_directed_fill_v1_cov28_try1_b01_r1` 整批 18 个候选经签名后，species-preserving gate 选出 3 个非旧 root 候选（roots `d6c7706b07/a02280d338/fdbfe36461`），证明 directed fill 中存在可保种中间样本。
- 验证 3：把这 3 个中间样本继续小步补到约 0.254-0.262 后，2 个能保持非旧 root，但 final root 退化为 `247c477ed8 = depLocal / edgeSparse / braidWeak`；开启 `-RejectWeakFinalRoot` 后 0/2 通过。
- 当前结论：identity preservation gate 已成立；下一步 production 方向不是继续简单补肉，而是 species-quality-preserving fill：既禁止旧 root 坍塌，也禁止新 root 退化成 weak/local/sparse root，fill 时必须保持 `depCross`、`edgeCoupled` 或至少 `braidMed+`。

## Species-Preserving Fill Compiler V2 - 2026-06-23

- 新增 `Invoke-SpeciesPreservingFillCompilerV2.ps1`：包装 directed batch fill，每轮执行 `fill -> causal skeleton signature -> species-preserving selector -> next feed`，不直接修改旧 hardlock 量产线。
- smoke 验证：child fill、signature、SCL gate 均可串联执行；脚本使用 hashtable splat 避免 PowerShell array splat 参数错位。
- 严格 SCL 验证 `species_preserving_compiler_v2_strict_try2`：输入 3 个非旧 root 中间候选，普通 fill 可产 2 个可解候选（coverage 0.254/0.262，supportDepth=4），但 SCL 0/2 通过。
- 拒绝原因全部为 `sourceRootNotPreserved;weakFinalRootSkeleton`，final root 统一退化到 `247c477ed8 = depLocal / edgeSparse / braidWeak`。
- 当前结论：新物种线的下一步不是继续提高 coverage，而是改 fill candidate/ranking，使其保持 source root 或至少保持 `depCross/edgeCoupled/braidMed+`；否则 fill 会把新 species 投影成弱局部 root。

## Species Manifold Gate + 0.29 Proof - 2026-06-23

- GPT 顾问与本地实测已达成一致：`exact source-root preservation` 太硬，会把 fill evolution 锁死；正式生产 gate 改为 `strong-root manifold preserve`：允许强 root A->B 迁移，但禁止回旧 canonical root、禁止 weak/local/sparse root，并要求 final root 仍具备 cross dependency、wide region trace、edge coupling 和 medium/strong braid。
- 已在 `Select-CausalSkeletonSpeciesPreservingCandidatesV1.ps1` 新增 `-RequireStrongRootManifold`，并修复 `MaxPerFinalRoot/MaxPerSourceRoot` 的排序前占坑问题；现在先按 `selectionScore/coverage` 排序，再执行 root/source cap，避免低分候选抢占 root 名额。
- 已在 `Select-SpeciesManifoldFromFillRunV1.ps1` 透传 `-RequireStrongRootManifold`，用于批量 fill-run 后按强 root 流形筛选。
- 关键实测：从保种候选 `fdbfe36461` 继续 fill 到约 `0.2757` 时，strict source-root gate 0/32，通过 strong-manifold gate 可选出 root `a02280d338` 候选（cov `0.2757`、avg `2.82`、max `5`、anti `0.536`、support depth `4`）。
- 再从该 best 候选冲 `0.29`：directed fill 产生 14 个强 root near-miss 但均 unsolved；经 `Repair-NearMissFillerOrientationV1` 38 variants -> 24 accepted，再经 strong-manifold gate 选出 2 个不同 final root（`a02280d338`、`0eea76b1ba`），coverage 均 `0.290441`，solved/A，support depth `4`，localPatch `2`，followRun `4`。
- 继续从 `0.290441` best 推 `0.30` 时当前 fill operator 报 `No filler group candidates`，说明单个体几何已接近承压上限；下一步应并行扩更多 source/root basin，而不是硬钻单个体。
- 待修小问题：部分新脚本/repair 输入 CSV 的 path 以 `./.worktrees` 开头会被 OutputRoot 二次拼接，当前用临时 abspath feed 规避；后续应在 fill/repair 脚本内统一 normalize rooted/relative path。

## Species Manifold Root Pair Demo Pack - 2026-06-23

- 已将 strong-manifold gate 选出的两个 `0.290441` coverage 强 root 候选冻结成 `SGPRhythmLab_SpeciesManifoldRootPairReview2Pack.asset`，关卡副本在 `SpeciesManifoldRootPairReview2Frozen/`。
- Demo 场景 `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` 的 `activePack` 已切到 pack guid `62220026a51e4a7cb8dce3f60b427e8d`。
- 冻结后 board trace：2/2 solved；v001 avg/max `3.66/6`，v009 avg/max `4.14/7`，两者 `causalAntiLocalityScore=0.552`、`supportClosureBestDepth=4`、`localPatchSolveRunMax=2`、`dependencyFollowRunMax=4`。
- 该包用于人工看两个 root 的体感差异，不代表最终量产包；下一步仍是扩更多 source/root basin，避免单 root 变体化。

## Root Identity Canonicalization - 2026-06-23

- 用户指出 `SpeciesManifoldRootPairReview2` 两关几乎一样；本地 diff 证实 35 根链只差 1 根，occupancy Jaccard=`0.9506`。
- 已修正 `Build-CausalSkeletonSignatureV1.ps1`：root signature 移除 rhythm bucket，新增 `rhythmVariantSignature/rhythmVariantHash`。
- 回归：同一冻结包重算后 `RootSkeletonSignatures=1`、`RhythmVariants=2`；selector 在 `MaxPerFinalRoot=1` 下只保留 v001，v009 因 `maxPerFinalRoot` 拒绝。
- 已新增 canonical 单关 demo 包 `SGPRhythmLab_SpeciesManifoldCanonicalRootReview1Pack.asset` 并把 Demo activePack 切到 guid `0830f927c72c4ecba5af5f72f3ee9c05`。Frozen trace 1/1 solved，avg/max `3.66/6`，supportDepth `4`。
- 下一步多样性目标：继续扩不同 rootSkeleton，而不是收集同 root 的 rhythm/geometry variant。

## Species New Root Review2 - 2026-06-23

- 已按 rootSkeleton/rhythm/chainLanguage 分层继续扩真实结构物种：从 `causal_skeleton_species_sampler_v2_cov255_seed947901` 的 `web_four_mirrory` 出发，小步 directed fill 后保住两个不同强 root：`1179a2b946` 与 `77b588f85b`。
- 冻结 Demo 包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SpeciesNewRootReview2Pack.asset`，Demo activePack 已切到 guid `e7900fa8cf964e80a5416e726b7d841e`。
- 冻结 trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/species_new_root_review2_frozen_trace_metrics.csv`；2/2 solved/A，avg/max 分别 `3.52/6` 与 `3.69/7`，supportDepth `3/4`，localPatchRun `2/2`，dependencyFollowRun `3/3`，outerExitHead `0/0`。
- 冻结 signature：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/species_new_root_review2_frozen_signature_signatures.csv`；2 rows -> 2 rootSkeleton / 2 macro / 2 chainLanguage（`snake_midLen_shortDominant` 与 `bent_midLen_shortDominant`）。
- 负结果：新 sampler `seed948777` 有 10 root source，其中 `web_four_mirrory` source root=`41f738e4f0` 很强，但第一轮 fill 14 个候选中 9 个投影到已知 `77b588f85b`、3 个回旧 canonical `7f70a964d4`、仅 1 个保持 `41f738e4f0`，暂不放入 Demo。
- 下一步：继续多 seed 挖 `solved + supportDepth>=3 + strong root` source；每个 source 只允许小步 fill，并用 final root cap + chainLanguage cap + occupancy cap 进入 review，避免 root 投影和同类变体。

## User Review - SpeciesNewRootReview2 - 2026-06-23

- 用户试玩反馈：`SpeciesNewRootReview2` 两关体感仍然几乎一样，大约只有 2 根链条不同；不能算真正结构物种差异。
- 本地 diff 证实：第 1 关 29 chains，第 2 关 32 chains，其中 27 条完全相同；occupancy Jaccard=`0.8673`，共同占用 196/221 cells。两关都来自 `web_four` archetype，第二关主要是后续 filler/少量替换。
- 结论：`rootSkeletonHash` 仍然过度依赖聚合桶（如 `depMixed/depCross`、`antiWeak/antiHard`），会把同一 source archetype 的 fill/rhythm/局部链条变化误判成不同 root。
- 下一步：废弃该包作为“新 root proof”；选择层必须新增 `sourceArchetype/templateFamily` cap，且真正多样性应来自不同 archetype（如 tri_branch、dual_diagonal、single_spoke/web_four 之间），不是同一 web_four 的后续变体。

## Tri-Branch Solvable Source Prior - 2026-06-23

- GPT 顾问与项目侧一致：`tri_branch` 不能再当随机 layout/fill 变体生成；它必须是 source-level dependency topology template，先约束 `Root -> A/B/C -> Hub`、跨区 branch、branch 间互锁/汇聚，再交给 SGP/fill。
- 已在 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-CausalSkeletonSpeciesSamplerV1.ps1` 增加 `-TriBranchSolvableBridge` 最小 source prior：tri_branch 额外带入 b3 bridge/support 段，用于验证“source 前置可解性”方向。
- smoke 结果 `causal_skeleton_species_sampler_v2_tri_solvable_seed949901`：3 个 tri_branch source 中 2 个从旧版全无解提升为 solved；`mirrorx/mirrory` 均 supportDepth=4，maxChoices=5/6，antiLocal=0.636/0.600，outerExit=0。
- tri_branch solved source 小步 directed fill 后可保持 sourceArchetype=tri_branch：`species_tri_branch_solved_fill_v1_cov27_b02` 中最高可到 coverage `0.2843`，较稳候选 `c03` 为 coverage `0.2745`、avg/max `3.19/6`、anti `0.593`、supportDepth `4`、localPatchRun `1`。
- 结论：轻量 source prior 已证明 tri_branch 不是死路，但要量产仍需真正 graph-first tri_branch V1；post-hoc repair 对旧 tri_branch near-miss 无效，source generator 必须前置可达性。

## Species Cross-Archetype Review1 - 2026-06-23

- 已冻结 2 关跨 archetype demo pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SpeciesCrossArchetypeReview1Pack.asset`，Demo activePack guid=`462f2102cab84e7d8fda798df9ae00ab`。
- 关卡 1：canonical/旧高难对照，avg/max `3.66/6`、anti `0.552`、supportDepth `4`、localPatch `2`、followRun `4`；冻结 trace 显示 outerExitHeadCount=`1`，需人工确认是否影响体感。
- 关卡 2：tri_branch 新 source archetype，avg/max `3.19/6`、anti `0.593`、supportDepth `4`、localPatch `1`、followRun `4`、outerExitHeadCount=`0`。
- 冻结包 trace 需使用绝对路径 manifest `SGPRhythmLab_SpeciesCrossArchetypeReview1Pack_abspath.csv`，相对路径 manifest 当前会被 `Build-SGPRhythmTrace.ps1` 误判 missing；这是已知 path normalize 待修问题。
- 冻结 signature 显示 `SourceArchetypeFamilies=2`、`RootArchetypeSignatures=2`，但 `RootSkeletonSignatures=1`；说明 rootSkeleton 聚合桶不能单独作为物种差异充分条件，production diversity 应优先看 sourceArchetype/rootArchetype/occupancy/trace 组合。

## User Review - SpeciesCrossArchetypeReview1 - 2026-06-23

- 用户试玩反馈：`SpeciesCrossArchetypeReview1` 的 Level1/Level2 仍不算严格不同 root；体感是中间两块上下对调，属于局部结构差异。
- 本地 diff：Level1 35 chains，Level2 32 chains，19 条链完全相同；occupancy Jaccard=`0.6119`，说明单纯 occupancy/Jaccard 不足以识别“同全局骨架、局部模块换位”。
- 结论：`sourceArchetypeFamily=tri_branch` 与 `rootArchetypeSignature` 只能证明生成来源/局部结构不同，不能单独证明玩家认知上的 strict root 不同。
- 下一步：新增 strict root / role-layout gate；严格 root 必须体现全局角色布局、关键 hub/support/upstream 的空间分布和解题宏路径变化，而不是中部模块局部置换。

## Strict Root Gate V1 - 2026-06-23

- 根据用户反馈，`SpeciesCrossArchetypeReview1` 被判定为局部差异：中间两块上下对调，不是严格不同 root。
- 已在 `Build-CausalSkeletonSignatureV1.ps1` 新增 `globalLayoutSignature/Hash` 与 `strictRootSignature/Hash`；strict root 使用 rootSkeleton + 2x2 全局 presence 布局，刻意忽略局部 centroid 密度差异，避免把模块上下换位当成新 root。
- 回归 `species_cross_archetype_review1_strict_regression_v2`：该包 `RootSkeletonSignatures=1`、`StrictRootSignatures=1`、但 `SourceArchetypeFamilies=2`，符合人工判退：source 名不同不足以证明 strict root 不同。
- 已在 `Select-CausalSkeletonSpeciesPreservingCandidatesV1.ps1` 和 `Select-CausalHardlockDiverseCandidatesV1.ps1` 增加 `MaxPerStrictRoot`。
- selector 回归：对 `SpeciesCrossArchetypeReview1` 使用 `MaxPerStrictRoot=1` 时只选 1/2，另一关因 `maxPerStrictRoot` 拒绝；该包不再能作为跨 root proof。
- 下一步：真正新 root 必须来自 graph-first template 的全局角色布局变化，而不是 sourceArchetype 名称、局部模块换位、链条语言或 rhythm 变化。

## Root Topology Model Direction - 2026-06-23

- GPT 顾问与项目侧一致：当前问题已从“root 去重规则”升级为“root 定义维度不完整”。`strictRootHash` 只能阻止假多样性，不能创造真实 root space。
- 真正 root family 需要三层联合定义：role-layout（结构角色，如 tri_branch/hub_spoke/diagonal_coupled/web_crossover）、dependency-topology（谁挡谁、fanout、depth、coupling）、causal-trace-pattern（unlock order、delay、跨区节奏）。
- 下一步不再继续强化 hash-based gate，而是实现 `RootTopologyExtractor V1`：从 trace/signature/geometry 中输出可读 root family 标签和 topology features，用它指导 graph-first tri_branch/dual/web 生成。

## Mixed Root Topology Review V1 - 2026-06-23

- 用户持续反馈证明：`rootSkeletonHash/strictRootHash/sourceArchetype` 仍不足以判断“严格不同 root”；局部模块交换、depth 3/4 或少量链条差异都不能算新 root。
- 已新增 `Build-RootTopologyExtractorV1.ps1`，输出 `roleLayoutFamily/dependencyTopologyFamily/causalTracePatternFamily/rootTopologyHash`；并修正 depth identity：`supportDepth>=3` 统一为 `depth3p`，避免把“差一根链条/多一层延迟”当新 topology。
- 回归结果：`SpeciesCrossArchetypeReview1` 两关 -> `RootTopologyFamilies=1`；`SpeciesNewRootReview2` 两关 -> `RootTopologyFamilies=1`，符合人工判退。
- 已扩展 `Select-CausalSkeletonSpeciesPreservingCandidatesV1.ps1`：支持 `-RootTopologyCsv` 与 `-MaxPerRootTopology`；负样本回归 2 选 1，reject=`maxPerRootTopology`。
- GPT 顾问与本地实测一致：若要创造新 root，主线应是 topology/motif-first generator；但 `RootTopologyTemplateGeneratorV1` 纯几何模板 smoke 证明 non-tri 模板缺少 trace-visible causal backbone，12 source 仅 tri 可用，现有 directed fill 救不动。
- 新实用突破：`MixedFamilyProductionO1` 里已有可用 motif 种子。用 mixed-root gate（不强制 hardlock `supportDepth>=3`，但要求 solved、低 choice、anti/local/follow）选出 4 个严格不同 rootTopology：hardlock tri、dependency_skeleton、room_door x2。
- 已冻结 review pack：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_MixedRootTopologyReviewV1Pack.asset`，Demo activePack guid=`d3e2b07d03634ef28d9e0e0e011eeb97`。
- Frozen verification：`mixed_root_topology_review_v1_frozen_trace_metrics.csv` 4/4 solved（3 S + 1 A）；`mixed_root_topology_review_v1_frozen_signature_signatures.csv` -> 4 rootSkeleton / 4 strictRoot；`mixed_root_topology_review_v1_frozen_root_topology_root_topology.csv` -> 4 rootTopology / 3 sourceArchetype。
- 下一步：把 `MixedFamily` 中的 dependency_skeleton/room_door 作为 motif library seed，反推 `motif-embedded topology template`；fill 只做外围补全，不能再用纯 geometry template 期望 trace 自动形成因果骨架。

## User Review - MixedRootTopologyReviewV1 - 2026-06-23

- 用户试玩反馈：`MixedRootTopologyReviewV1` 中 Level2/3/4 都非常简单；只有 Level1 接近当前 hardlock 难度。
- 本地指标确认用户判断：Level2/3/4 虽然 `avgChoices/maxChoices` 低且 `causalAntiLocalityScore` 高，但 `supportClosureBestDepth=0/1`、`supportClosureBestScore=0`、trace pattern 为 `immediate` 或 `singleRegion`，缺少 trace-visible multi-hop causal support。
- 结论：`rootTopologyHash` 不同只能证明结构身份不同，不能证明高难；`dependency_skeleton/room_door` 当前属于 false-hard/simple-root 负样本，不能进入高难 production/review，除非后续嵌入真实 causal motif 并通过 support/closure 或等价难度 gate。
- gate 修正：多 root 产线不能放松到只看 solved + low choice + antiLocal；高难 root 必须有至少一种已验证的难度机制。当前唯一 validated 机制仍是 `supportClosureDepth>=3/supportScore>=0.70` 的 trace-visible hardlock motif。
- 下一步：撤销 mixed-root gate 作为生产 gate；保留它只作结构诊断。真正多 root 需要 `motif-embedded template`，即每个 root template 内置可见 causal motif，而不是把 simple room/door root 当高难。

## Gate Hotfix After MixedRoot False-Hard Feedback - 2026-06-23

- 已在 `Select-CausalSkeletonSpeciesPreservingCandidatesV1.ps1` 新增 `-RequireRootTopologyProductionQuality`：当提供 `RootTopologyCsv` 时，只允许 `Build-RootTopologyExtractorV1.ps1` 标为 `productionCandidate` 的 root 进入高难 review。
- 回归 `MixedFamilyProductionO1`：开启该开关后 9 关只选 1 关，Level2/3/4 被 `rootTopologyNotProductionQuality` 拒绝，符合用户“234都非常简单”的反馈。
- Demo activePack 已从错误的 `MixedRootTopologyReviewV1Pack` 切回已知高难 0.30 基准包 `SGPRhythmLab_HardLock030DynamicOuterGate5Pack.asset`（guid=`65153ebe983f4a1786c8c810e59ec2f5`）。
- 当前结论：短期高难产线继续用 hardlock causal motif；多 root 方向必须先让 dependency_skeleton/room_door 等模板嵌入 multi-hop causal motif，再谈 production。

## Causal Motif Embedding Compiler V1 - 2026-06-23

- 用户与 GPT 顾问共识已收敛：`rootTopology != difficulty`；root topology 只作为结构容器/身份，进入高难 production 必须先具备 trace-visible causal motif。当前 validated motif 仍是 `supportClosureBestDepth>=3` 且 `supportClosureBestScore>=0.70` 的 hard-lock/support-lock。
- 新增 `Invoke-CausalMotifEmbeddingCompilerV1.ps1`：串联 `species sampler -> board trace source gate -> species-stratified source sampling -> directed batch fill -> final trace/signature/rootTopology`。它明确禁止 supportDepth 0/1 的 false-hard/simple root 进入 fill。
- smoke `causal_motif_embedding_v1_smoke1`：3 个 source 小样中 2 个 tri_convergent final 过线；冻结包 `SGPRhythmLab_CausalMotifEmbeddingV1Smoke2Pack.asset`，2/2 frozen trace solved/A，`supportClosureBestDepth=4`，`avg/max=2.96/5` 与 `3.76/6`，`outerExitHeadCount=0`。
- 已修正 wrapper source selection：新增 `MaxSourceRowsPerSpecies`，先按 rootSpecies 分层取 source，避免 tri source 挤掉 web source。
- stratified smoke `causal_motif_embedding_v1_strat_smoke1`：source 选择包含 2 个 tri + 2 个 web；最终只有 1 个 tri 过线，web source 能过 support-lock source gate，但旧 directed fill 无法稳定承压。
- 当前结论：motif embedding chain 已跑通，但仍只在 tri_convergent 容器中稳定；web/dual 下一步需要 species-aware fill，而不是放宽 difficulty gate 或把 simple room/door root 当 hard。

## User Review - Causal Motif Embedding V1 Smoke2 - 2026-06-23

- 用户试玩 `SGPRhythmLab_CausalMotifEmbeddingV1Smoke2Pack` 后反馈：两个 proof 关“可以，差别挺大的”。
- 这说明即使当前 rootTopology/roleLayout 都仍是 `tri_convergent`，causal motif 的空间落点、fanout/branching 和 trace rhythm 变化已经能产生可感知差异。
- 结论修正：严格 root 多样性仍是长期目标，但短期 review/production 可以接受“同大容器内的不同 motif instance / rhythm variant”作为体感多样性补充，前提是每关都通过 support-lock motif gate 和 board trace hard gate。
- 下一步：继续推进 web/dual 的 species-aware fill，同时保留 tri_convergent motif instance 作为当前可用高难差异来源。

## Causal Motif Embedding Review Pool1 - 2026-06-23

- 基于用户对 `CausalMotifEmbeddingV1Smoke2` 的反馈（“差别挺大”），短期策略修正：同一 `tri_convergent` 大容器内的不同 causal motif instance / rhythm variant 可以进入 review，只要通过 trace-visible support-lock hard gate；严格新 root 仍是长期目标。
- 新跑 seed `964101` 得到 4 个过线 motif final；合并 Smoke2 的 2 个 proof 后，用 `MaxPerStrictRoot=2`、`MaxPerRootTopology=3`、`MaxOccupancyJaccard=0.92`、`RequireRootTopologyProductionQuality` 精选 5/6。
- 冻结 Demo 包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_CausalMotifEmbeddingV1ReviewPool1Pack.asset`，Demo activePack guid=`0916e69060664a15823243a67172666b`。
- 冻结 trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_motif_embedding_v1_review_pool1_frozen_trace_metrics.csv`；5/5 solved/A，avg/max 范围 `2.88-3.76 / 5-7`，supportDepth `3-4`，supportScore `0.908-0.968`，anti `0.571-0.737`，outerExitHead=0。
- 冻结 signature：5 core / 4 skeleton family / 4 macro / 3 rootSkeleton / 3 strictRoot / 5 rhythm variants；该包应标注为“可用高难 motif-instance 体感 review”，不是严格 multi-root proof。
- 下一步：用户试玩后若体感认可，短期可沿该路线扩 8-12 关 motif-instance pool；若仍觉得重复，则继续做 web/dual species-aware fill，不能放宽 support-lock gate。

## Root Diversity Reclassification - 2026-06-23

- 对 `CausalMotifEmbeddingV1ReviewPool1` 的最新人工/GPT 审稿结论：5 关难度有一些，但严格 root 基本只有 1 个；其余是同 causal backbone 下的 motif placement / rhythm / fill realization。
- 当前 root 生成链路问题：species/template source + directed fill 可以产 hard realization，但没有先定义不同 causal backbone；因此 selector 只能把同一 backbone 的变体拆桶，无法生成真正多 root。
- 下一步最小可执行方向：新增/实现 `causal backbone signature` 诊断，先把现有 review 包重分为 true backbone root；随后做 graph-first causal backbone generator V1，而不是继续强化 hash/gate。

## Causal Backbone Signature V1 - 2026-06-23

- 已新增 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-CausalBackboneSignatureV1.ps1`，作为 root 级因果骨架诊断层；输入 signature CSV + board trace CSV，输出 `causalBackboneHash` 与 `backboneVariantHash`。
- 该脚本刻意合并 depth3/4、fanout3/4、depMixed/depCross、rhythm、chainLanguage 和 fill 变体，只保留 root 级 support-lock causal degrees of freedom：motif class、hubfield role、distributed flow、lock shape。
- 在 `CausalMotifEmbeddingV1ReviewPool1` 上验证：旧 signature 显示 3 rootSkeleton / 3 strictRoot / 3 rootTopology；新 causal backbone 诊断输出 `CausalBackboneRoots=1`、`BackboneVariants=5`，与用户/GPT 的体感判断一致。
- 输出：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_motif_embedding_v1_review_pool1_backbone_v1_backbones.csv` 与 `_summary.md`。
- 下一步：selector 中 strict root 多样性应优先使用 `causalBackboneHash`；真正新 root 必须由 graph-first causal backbone generator 产生不同 backbone，而不是继续依赖 hash/gate 分桶。

## Causal Backbone Signature Calibration - 2026-06-23
- `Build-CausalBackboneSignatureV1.ps1` calibrated with synthetic controls: 5 conceptual causal backbones (serial, dual-gate, tri-convergent, multi-hub web, no-backbone) produced 5 `causalBackboneRoots`, while old synthetic strict/rootSkeleton hashes were 1.
- Current hard motif review pack still collapses to 1 causal backbone with 5 rhythm/geometry variants, matching user visual review.
- Real `Build-RootTopologyTemplateGeneratorV1` calibration (dual_gate, tri_convergent, web_crossover, hub_spoke) produced 4 assets; board trace shows only `tri_convergent` has depth3 branch3 causal backbone and it maps to the existing hard root `6fc63698fd`; dual/web/hub are `no_causal_backbone` or shallow/unsolved.
- Conclusion: current root metric is directionally correct for separating causal hard roots from layout roots, but generator still lacks multiple causal hard backbone templates.
- Next: create explicit graph-first causal backbone definitions for at least dual-gate-hard and multi-hub-web-hard, then realize through SGP/fill and trace-gate them.

## Dual-Gate Hard-Lite Causal Backbone Primitive - 2026-06-23

- GPT 顾问与本地实测一致：`dual_gate_hard_lite` 的 ablation proof 可以算第二 causal backbone 机制证明，但必须固化为 graph-first primitive，不能继续靠从 tri root 剪枝。
- 已在 `Build-SGPRhythmTrace.ps1` 增加 support closure 独立性诊断字段：`supportClosureQualifiedIndependentCount`、`supportClosureQualifiedMinOverlap`、`supportClosureQualifiedRoots`；用于确认多个 closure 是否只是同 basin 变体。
- 现有 DesignedHardLockV0 多桥样本 retrace 后仍为单 basin：`supportClosureQualifiedIndependentCount=1`，证明“复制桥”不等于新 root。
- ablation proof `dual_gate_hard_lite_probe_v1` 3/3 solved/A；其中 branch2/fanout2 样本形成新 causalBackbone `34771de5e2 = serial_support_lock||dual_gate_hubfield||full_board|distributed_cross_flow||depth3p|fanout2|branch2|closureChain`，区别于当前主 tri root `6fc63698fd = cross_region_convergent_support_lock||single_backbone_hubfield||...fanout3p|branch3p|closureGraph`。
- 已把 `dual_gate_hard_lite` 加入 `Build-RootTopologyTemplateGeneratorV1.ps1` 作为 graph-first source primitive；0.19 source smoke 中 base/mirrory 保持 solved + depth3 + branch2 + fanout2，mirrorx 退回 branch3，应由 backbone gate 拒绝。
- 已在 `Build-HardLockSlotDirectedBatchFillV1.ps1` 新增 opt-in `MaxCausalFanoutMax` 和 `MaxSupportClosureBranchMax`，用于 species/backbone preserving fill。默认 0 不影响旧 hardlock 产线。
- 严格 fanout/branch 小步 fill smoke2：`dual_gate_hard_lite_template_v1_directed_fill_strictfanout_smoke2_b01_r2_c17` retrace solved/A、coverage `0.2304`、avg/max `3.04/6`、anti `0.75`、supportDepth `3`、branch `2`、fanout `2`、localPatch `1`、follow `4`、outerExitHead `0`、TrueHardCandidate；backbone 仍为 `34771de5e2`。
- 当前限制：这不是 0.30 量产包；它证明第二 causal backbone primitive 可独立生成并小步承压。下一步要做 dual-lite species-aware fill/capacity 扩张，目标先推到 `0.25-0.27` 且保持 `branch2/fanout2`，再考虑进 review pack。

## Dual Gate vs Tri Root Review Pack - 2026-06-23

- 已按用户要求冻结 2 关肉眼对照包，并把 Demo activePack 切到该包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DualGateVsTriRootReview1Pack.asset`，guid=`1b2f257a8d734053902b1ec4df8539a7`。
- Level1：旧 `tri_convergent` baseline，frozen trace solved/A，avg/max `3.16/7`，supportDepth `3`，branchMax `3`，fanoutMax `4`，outerExitHead `0`。
- Level2：新 `dual_gate_hard_lite` proof，frozen trace solved/A，avg/max `3.04/6`，supportDepth `3`，branchMax `2`，fanoutMax `2`，outerExitHead `0`。
- Frozen backbone report：`dual_gate_vs_tri_root_review1_frozen_backbone_backbones.csv`，2 rows -> 2 causalBackboneRoots。该包只用于人工判断 dual-lite 是否体感为新骨架，不是 production 包。

## Dual Gate Spatial Root Skeleton Review - 2026-06-23

- 用户反馈此前 `dual_gate_hard_lite` 虽有 branch2/fanout2 causal backbone 指标证明，但肉眼仍像旧 root 的左右/局部变体；当前重点改为先验证空间角色布局，而不是继续证明指标。
- 已在 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-RootTopologyTemplateGeneratorV1.ps1` 新增 `dual_gate_spatial` review template：两个空间分离、非对称 gate island + 一个共享中央 core，低覆盖生成，刻意避免镜像变体。
- 已冻结单关视觉审查包并挂到 Demo：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DualGateSpatialRootV1SkeletonReview1Pack.asset`，guid=`26db0c7e17d747e4b32e903d7a774d64`。
- Frozen trace：`dual_gate_spatial_root_v1_skeleton_review1_frozen_trace_metrics.csv`；结果 `solved=False`、`supportClosureBestDepth=1`、`maxChoices=12`，因此它不是 hard/production 候选，只用于人工判断 root 形态是否明显不同。
- 下一步：若用户认可视觉 root 方向，再做 `dual_gate_spatial` 的 causal motif embedding；若仍像旧 root，则继续调整 role-layout，不进入补肉/量产。

## Dual Gate Joint-Core Root Correction - 2026-06-23

- 用户纠正：`dual_gate` 必须是“两个空间分离的控制系统，共同控制一个核心”；此前 `dual_gate_spatial` 只算 dual-island visual skeleton，不符合 true dual_gate 定义。
- 已在 `Build-RootTopologyTemplateGeneratorV1.ps1` 标记 `dual_gate_spatial` 为非 true dual_gate，并新增 `dual_gate_joint_core`：Gate A 与 Gate B 分处不同区域，各自形成 gate chain，分别打开中央 core 的两个入口。
- `dual_gate_joint_core_root_v1_review2` frozen trace：`solved=True`、`processTier=A`、`openers=6`、`avgChoices=4.93`、`maxChoices=6`；trace step 显示 A 路 `0->1->2->3->8`、B 路 `4->5->6->7->9`，两个 gate 分别解锁同一中央 core 的上下入口。
- 当前限制：旧 `supportClosureBestDepth` 指标仍为 0，说明该指标只识别 support-lock closure，不识别 dual serial joint-core motif；下一步需要新增 `dualGateJointCore` 诊断/gate，再做 choice-pressure 压缩和 production fill。
- Demo activePack 已切到 `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DualGateJointCoreRootV1Review2Pack.asset`，guid=`7b77e11c291e403884c67c4c74910028`。

## Dual Gate Shared-Lock Prototype - 2026-06-23

- 用户进一步纠正：`dual_gate` 不是“上面解中间一根、下面解核心下一根”，而是两个空间分离控制系统共同控制同一个核心。
- 已将 `dual_gate_joint_core` 改为 shared-lock 模型：同一中央 core chain 的 escape ray 依次被 lockB 与 lockA 阻挡；Gate A 解 lockA，Gate B 解 lockB，两个 gate 都完成后 core 才打开。
- `dual_gate_joint_core_root_v1_review5` trace：`solved=True`、`processTier=A`、`openers=6`、`avgChoices=4.55`、`maxChoices=6`、`causalAntiLocalityScore=0.8`、`localPatchSolveRunMax=2`、`dependencyFollowRunMax=2`。
- Trace order：B 路 `3->4->5` 解 lockB，A 路 `0->1->2` 解 lockA，之后 core `6` 才可用；这版符合 true dual-gate shared-lock 原型，但仍是低覆盖/偏松原型，不是 production hard。
- Demo activePack 已切到 `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DualGateJointCoreRootV1Review5Pack.asset`，guid=`387a8b56c7db45d8ac941bba0a8693b8`。

## Validated Root Expansion O1 - 2026-06-23

- 当前对话目标切换为“已验证 root 的产线扩张”，不继续追新 root；主线只使用 trace-visible hardlock/support-lock motif，保留 `supportClosureBestDepth>=3`、`supportClosureBestScore>=0.70`、`avgChoices<=4.0`、`maxChoices<=7`、`localPatchSolveRunMax<=3`、`dependencyFollowRunMax<=4` 和动态外口 pressure gate。
- 从现有报告清洗出 55 个 hard-gate 候选，其中 `coverage>=0.299` 有 39 个；严格 occupancy/parent 去重后只有 2 个 0.30 独立样本，说明当前瓶颈是可承压父本不足，不是 accepted 行数不足。
- 已并行跑 `Invoke-ParentCapacityProbeV1` 分片：先从 `causal_hardlock_diverse_review_v2_cov28_selected.csv` 的 7 个父本推 0.30，再从 `causal_hardlock_diverse_review_v1_cov28_selected.csv` 补跑 4 个代表父本；只有少数父本能经 near-miss orientation repair 产出 0.299+ accepted，多个父本直接 `No filler group candidates`。
- 合并后 hard-gate 候选增加到 80 行，其中 `coverage>=0.299` 有 64 行；但严格去重后仍只有 2 个 0.30 独立样本，放宽到 `coverage>=0.285` 得到 4 个 clean review 候选。
- 已冻结并挂 Demo：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_ValidatedRootExpansionO1ReviewPack.asset`，guid=`4a794e0272e747d8ae902a7cc47ca568`；Demo activePack 已指向该包。
- Frozen trace：`validated_root_expansion_o1_review_frozen_trace_metrics.csv`，4/4 `solved=True`、4/4 `processTier=A`；avgChoices `3.23-3.69`、maxChoices `5-7`、supportDepth `3-4`、supportScore `0.921-0.975`、localPatch `1-2`、dependencyFollow `4`、动态外口 pressure 均不超过 1。
- Frozen signature：`validated_root_expansion_o1_review_frozen_signature_summary.md`；4 core / 4 skeleton family / 3 macro / 2 rootSkeleton / 2 strictRoot / 4 rhythm variants。该包定位是“已验证 root 产线 clean expansion review”，不是新 root proof，也不是 12 关量产完成。
- 下一步：用户肉眼试玩该 4 关；若重复度可接受，可把当前父本池标为短期 hard production lane。若仍嫌重复，必须扩可承压父本来源或把 validated motif 嵌入更多容器，不能仅放宽 selector 拼数量。

## Validated Root Background SGP Fill V1 - 2026-06-23

- 用户纠正：`0.30` 只是父本/骨架分水岭，不是终极目标；完整关卡仍需继续提高 coverage，当前主线转为“已验证 support-lock root 的填肉扩张”。
- 已新增 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-ValidatedRootBackgroundSGPFillV1.ps1`：从已验证父本读取 LevelDefinition，保护前 N 条核心链的逃逸通道，在剩余空间做全局 bent/random-walk 背景 SGP fill，并每轮用 `Build-SGPRhythmTrace.ps1` 做 board-level gate。
- 关键实测：旧 `Build-HardLockSlotDirectedBatchFillV1.ps1` 在 `0.305` 左右出现 `No filler group candidates`；新背景 fill 用 `ProtectedChainCount=14` 已将同一 validated root 从 `coverage=0.3051471` 推到 `0.3664216`，再推到 `0.3982843`，且 frozen trace 仍 `solved=True/processTier=A/supportClosureBestDepth=3/supportClosureBestScore=0.969/avgChoices=3.58/maxChoices=7/localPatch=2/dependencyFollow=3`。
- 已冻结并挂 Demo 的 proof 包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_ValidatedRootBackgroundSGPV1T40ProofPack.asset`，guid=`b79c62c3b91f4b5cae1d7cc31dbf6c41`；frozen trace：`validatedroot_background_sgp_v1_t40_proof_frozen_trace_metrics.csv`。
- 负结果：不保护核心 ray 时能生成 `0.33+` 候选但全部 `unsolved`；保护前 20 条链过宽导致没有候选；`0.42+` near-miss 经 `Repair-NearMissFillerOrientationV1.ps1` 仍 `accepted=0`。当前单父本稳定上限约 `0.40`。
- 小批量复测：4 个 step2 父本中只有 1 个能过一跳到 `coverage=0.337`，说明 0.40 已证明可达，但还不是多父本稳定量产。下一步应做自动保护链数/seed 调度与多父本 capacity search，而不是继续手调单父本。
- 同步修复 `Build-SGPRhythmTrace.ps1` 的 `.Contains()` 单元素集合健壮性：新增 `Test-ContainsInt`，避免新填肉候选在单可选状态下触发 `System.Int32 does not contain Contains` 的 trace failure。

## Strict Dual Gate Trace Gate V1 - 2026-06-23

- 已在 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` 增加 strict dual-gate 诊断字段：`strictDualGateCandidate`、`strictDualGateFailReasons`、raw candidate、core/gate ids、gate ancestry、A-only/B-only/A+B counterfactual。
- 定义收敛：strict dual_gate 必须是两个空间分离、因果独立的 gate 系统共同控制同一个 core；只清 A 或只清 B 不能打开 core，A+B 必须能打开 core，且 A/B 在 core 前不能互相解锁或共享 ancestry。
- 额外 root-level 保护：若 raw 双 blocker 出现在 `supportClosureBestBranchMax>=3` 或 `causalFanoutMax>=3` 的 tri-convergent backbone 内，判为 `triConvergentBackboneDominates`，不算 strict dual root。
- 回归结果：`dual_gate_joint_core_root_v1_review5` 通过 strict dual gate（core=6，gateA=5，gateB=2；A-only/B-only false，A+B true）。
- 负样本回归：旧 `dual_gate_vs_tri_root_review1` 中 tri baseline raw=true 但 root=false，原因 `triConvergentBackboneDominates`；旧 dual-lite proof root=false，原因 `gateAHasNoUpstreamControl`；`CausalMotifEmbeddingV1ReviewPool1` 5/5 root=false。
- 当前判断：review5 是 strict dual-gate low-coverage/root proof；下一步可以基于该 gate 做 dual_gate 生成/补肉，不能再只靠视觉或 raw blocker 判新 root。

## Strict Dual Gate Temporal Independence Review - 2026-06-23

- 已通过 Rosetta/GPT 在指定“高难关卡设计建议”对话复审 strict dual-gate gate。GPT 认可 RAW/STRICT 双层定义，但指出 `branchMax/fanoutMax` 是结构 proxy，长期可能误杀高 fanout 的真 dual_gate；建议补 `temporal non-interference`。
- 已在 `Build-SGPRhythmTrace.ps1` 增加 temporal independence 字段：`strictDualGateAInfluencesB`、`strictDualGateBInfluencesA`；如果清 A-side ancestry 会让 B gate 可用，或清 B-side ancestry 会让 A gate 可用，则 strict gate 失败。
- 回归结果：`dual_gate_joint_core_root_v1_review5` 仍为 `strictDualGateCandidate=True`，且 `AInfluencesB=False`、`BInfluencesA=False`、A-only/B-only false、A+B true。
- 负样本保持稳定：旧 tri baseline raw=true 但 strict=false，原因 `triConvergentBackboneDominates`；旧 dual-lite / review pool 多数 raw=false，原因 `aPlusBDoesNotOpenCore`。
- 当前工程取舍：V1 继续保留 `triConvergentBackboneDominates` 作为 conservative hard reject，因为本地人工负样本需要它挡旧 root；同时记录风险，后续若出现高 fanout 真 dual_gate，再把 branch/fanout hard reject 降级为 soft risk，并改用更强 temporal/causal independence gate。

## Strict Dual Gate Backbone Integration - 2026-06-23

- 已把 `strictDualGateCandidate=True` 接入 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-CausalBackboneSignatureV1.ps1`。
- 新 backbone 分类：`strict_dual_gate_shared_core||dual_gate_joint_core||...||dualGate|twoNecessaryGates|temporalIndependent|sharedCore`。
- 验证：`dual_gate_joint_core_root_v1_review5` 现在输出 causalBackboneHash=`57ae9ef455`，class=`strict_dual_gate_shared_core`，不再被误标为 `no_causal_backbone`。
- 对照：旧 tri baseline 仍为 `cross_region_convergent_support_lock`，raw dual=true 但 strict=false；旧 dual-lite proof 仍为 `serial_support_lock`，strict=false。
- 当前可用结论：strict dual-gate 已进入 trace gate + backbone identity 两层；下一步可以基于 `strictDualGateCandidate=True` / backbone class 做 dual_gate 专用补肉或 review pack，不再靠视觉/局部差异判断 root。

## Strict Dual Gate Identity-Preserving Fill Smoke3 - 2026-06-23

- 已将 `RequireStrictDualGate` 接入 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-HardLockSlotDirectedBatchFillV1.ps1`：batch/final gate 要求 `strictDualGateCandidate=True`，并跳过 supportClosure 作为 hard requirement，因为 strict dual 是 shared-core 双 gate motif，不是 support-lock closure motif。
- Strict dual fill 专用调整：`RequireStrictDualGate` 时放宽 pressure-only group ratio；难度由 strict dual identity gate、choice/max/outer/local/follow trace gate 共同把关。普通 hard-lock/support-lock 产线不受影响。
- 实测 `dual_gate_strict_fill_smoke3_target012` 从 `dual_gate_joint_core_root_v1_review5` 父本连续接受 6 轮，coverage `0.060 -> 0.1373`、chains `11 -> 19`，最终 trace `solved=True/processTier=A/strictDualGateCandidate=True/raw=True`，gates 仍为 `5+2->6`，`AInfluencesB=False/BInfluencesA=False`，avg/max `4.95/7`，anti `0.846`，outerExitHead `0`，localPatch `1`，dependencyFollow `3`。
- 已冻结并挂 Demo：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DualGateStrictFillSmoke3Pack.asset`，guid=`b33f27370e7045b8b584c9b00c3249a6`；Demo activePack 已指向该包。
- Frozen retrace：`dual_gate_strict_fill_smoke3_frozen_retrace2_metrics.csv`，1/1 passed strict dual gate。该包是 single-root proof/review，不是多关量产包。
- 下一步：用户肉眼判断 strict dual fill 是否体感区别明显；若认可，扩 seed/父本与 fill target；若仍像旧 root，应继续改 dual shared-core spatial realization，而不是放宽 strict gate。

## Trace Placement Probe V1 - 2026-06-23

- 已按 GPT/用户审稿结论新增独立诊断工具 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-TracePlacementProbeV1.ps1`；定位是“反向调用 board trace 的可放箭头/填肉分类器”，不改 `Build-SGPRhythmTrace.ps1` 真相层，也不直接进入 production fill。
- 工具从已验证父本出发，只枚举结构敏感区附近的短 micro-chain：核心 escapeRay corridor、ray hit junction、dependency node boundary、occupied boundary；每个候选临时插入后统一跑 board-level trace，并分类为 `SafePressure/SafeNeutral/TooEasy/MotifBroken/Deadlock`。
- 分类原则：`Deadlock` = trace 不可解但 support-lock/strict-dual motif 指标仍保留；`MotifBroken` = support-lock/strict-dual motif 掉线或分数明显下降；`TooEasy` = solved 但 openers/avg/max/outer pressure 增加或形成独立 filler；`Safe*` = solved 且 motif/choice 稳定。
- smoke：`trace_placement_probe_v1_smoke5` 对当前 0.398 proof 父本跑 5 个 probe，结果 `SafeNeutral=3`、`Deadlock=2`；说明 0.40 后不是“放不上”，而是同一敏感区域中既有安全中性补位，也有会中盘卡死的插法。
- 追加小批诊断：`trace_placement_probe_v1_t40_top12` 跑 12 个 probe，结果 `SafeNeutral=4`、`Deadlock=5`、`TooEasy=3`，未发现 `SafePressure`；Safe 候选只把 coverage 小步推到约 `0.402-0.404`，Deadlock 多为“不可解但 motif 指标保留”，TooEasy 多为新增 opener。当前判断：该 0.398 父本还有少量安全视觉补位，但缺少能继续压选择/承压的安全压力区，直接冲 0.45 需要更强父本或改候选策略。
- 性能注意：20 probe full trace 超过 3 分钟；V1 后续要用于 0.40->0.45 时应先做 top-K 候选收缩和小批 trace，不要全图全量扫。

## Ray Constraint Map V1 - 2026-06-23

- 已按用户/GPT 复核新增独立诊断工具 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-RayConstraintMapV1.ps1`；定位是“trace 后的空间语义解释层”，不替代 fill/probe/trace。
- 工具输入已验证父本，重跑 board trace 后输出：每个 cell 的 `primaryRole`、`canBeHead`、`canBeBodyBlocker`、`activeRayPassCount`、`criticalRayPassCount`、`wouldBlockChains`、`wouldReplaceBlockers`、head anchored/direct-outer 方向、timing/too-easy risk 和 fillPriority；同时输出 active dependency ray edges 与 ASCII role grid。
- 默认 0.398 proof 父本输出：`ray_constraint_map_v1_t40_cells.csv`、`ray_constraint_map_v1_t40_edges.csv`、`ray_constraint_map_v1_t40_summary.md`、`ray_constraint_map_v1_t40_role_grid.txt`。
- 关键诊断：coverage `0.3982843`、chains `48`、critical chains `23`、active dependency edges `43`、direct-exit rays `5`；空格角色中 `CriticalTimingZone=136`、`SafeFillZone=48`、`GuardSlot=2`、`BodyOnlyRayBlocker=6`、`HeadAllowed=231`、`HighRiskFreeHead=68`。
- 当前解释：0.30 不是上限，而是从稳定 skeleton 进入 ray-causal sensitive fill 的临界点；0.30->0.40 主要吃掉 safe region，0.40+ 剩余空间大量落在 critical timing / free-head risk 区。当前父本本身不坏，但若要继续到 0.45，需要按 RayConstraintMap 做 timed blocker/guard/body/head 角色分配，而不是继续无语义补空格。

## Ray Constraint Guided Fill V1 - 2026-06-23

- 已新增 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-RayConstraintGuidedFillV1.ps1`，读取 RayConstraintMap 的 cell role，只在 `SafeFillZone/HeadAllowed/GuardSlot/BodyOnlyRayBlocker` 等语义区域生成短链，并用 board-level trace 作最终 gate；同时修复 `Build-RayConstraintMapV1.ps1` 和 GuidedFill 输入在无 `rank` 字段时排序不稳定的问题，改为保持输入顺序。
- 实验链路：从 0.398 proof 父本开始，按“重算 ray map -> 小步补 1-2 条链 -> trace 回收 -> 选稳定样本继续”的方式连续推进：`0.398 -> 0.409 -> 0.419 -> 0.429 -> 0.436 -> 0.441`。
- 关键通过样本：`ray_constraint_guided_fill_v1_step440_c01_exact_to442_onechain_b01_c15`，coverage `0.4411765`、chains `57`、`solved=True/processTier=A/TrueHardCandidate`、hardScore `0.724`、antiLocal `0.654`、supportClosure `0.969/d3`、avg/max choices `4.05/8`、localPatch `2`、dependencyFollow `3`。
- 失败边界：0.436 父本直接 2-chain 冲 0.45 时 `0/20`，全部 `unsolved`；改为 1-chain 冲 0.442 时 `1/24` 通过。结论是 0.436+ 已进入极窄安全窗口，不能再靠普通随机 batch；下一刀应做“paired compensation / unlocker-aware fill”，即新增链 A 若打断或放松路径，必须同时生成 guard/unlocker B 做因果补偿。
- 当前生产判断：已验证 root 产线可以突破 0.40 并保持难度，短期最好 proof 到 0.441；但要继续往完整高覆盖推进，需要把 RayConstraintMap 从单链放置器升级为批量因果补偿编译器，而不是继续扩大候选数。

## Ray Saturation Fill Hypothesis - 2026-06-24

- 用户提出并需要优先复核的核心假设：当前 0.40+ 骨架已经由既有箭头的 escape rays 形成一张 ray dependency 网；剩余空格不再是“自由空地”，而大多会落在某条已有逃逸射线、critical timing zone 或潜在 blocker 位置上。若该假设成立，后续补链本质不是几何填空，而是对现有 ray dependency graph 做增量改写。
- 必须先区分两个命题：`A. 剩余空格大多被已有 escape rays 覆盖` 与 `B. 任意新链都会成为有意义 blocker 且不会降难度`。A 可能成立；B 不自动成立，因为新链可能成为 early free head、替换掉更强/更晚的 first-hit、短路解锁顺序、制造外口入口，或直接造成不可解。
- 新链的正确判定应基于 dependency delta，而不是单链数量：若新链 C 被某些现有链的 escape ray 命中，则新增 blocker 边 `C -> target`；同时 C 自身的 escape ray 也必须被已有结构或配套 unlocker/guard 控制，否则 C 会变成自由入口。`firstHit` 改变不是天然错误；只有当新 firstHit 的可达时间、依赖深度和 trace 顺序破坏主 motif 时才是错误。
- 下一步应先做 `Ray Saturation Audit`：统计剩余空格中 active/critical ray 覆盖率、direct outer head 风险、body-only/guard/head-allowed 比例、以及插入单格/短链后新增 blocker 边、替换 first-hit 边和新链自身被 blocker 控制的比例。该审计用于验证“骨架是否已覆盖所有逃逸路线”这个前提。
- 若审计确认剩余空间已高度 ray-saturated，生产填肉应升级为 `RayFieldFillPlannerV2`：先选择要新增/替换的 blocker 关系，再反推链头方向、链身路径和必要 unlocker/guard，最后由 board trace gate 验证 coverage、solved 和 difficulty；不能再把 `GuidedFillV1` 的随机短链当主线。

## Ray First Blocker Fill V1 - 2026-06-24

- 已通过 Rosetta/GPT 反问确认：`direct/free head` 不是硬拒绝条件，只是风险信号；关键是新链是否阻挡现有 active/critical ray、是否进入 trace-visible dependency、以及 trace delta 是否保持 solved/choice/local/follow/motif 稳定。
- 新增实验脚本 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-RayFirstBlockerFillV1.ps1`：读取 RayConstraintMap 的 cell role 和 edge csv 的 `rayCellsBeforeHit`，先选择要阻挡的 target ray，再选 blocker cell，并从 blocker cell 反推新增短链的 head/body；最终仍只信 `Build-SGPRhythmTrace.ps1`。
- 首轮发现脚本 bug：未显式允许 `AllowCriticalTiming` 时仍会选 `CriticalTimingZone` 作为 anchor；已修正 anchor gate，使 Guard/BodyOnly/Critical 均遵守显式开关。
- 干净无 critical 对照：`ray_first_blocker_fill_v1_step442_c15_to448_nocritical`，20/20 trace 完成但 0 accepted，20 个全部 unsolved，且候选高度集中在同一 body-only target ray，说明 0.44+ 单纯安全格 blocker 过窄且容易被单 anchor 吸附。
- 显式允许 critical timing 的严格 proof：`ray_first_blocker_fill_v1_step442_c15_to448_strict_critical` 从最高 proof 父本 `coverage=0.4411765` 接受 1/20；accepted 为 `ray_first_blocker_fill_v1_step442_c15_to448_strict_critical_b01_c17`，coverage `0.4448529`、chains `58`、solved/A、HardPotential、antiLocal `0.611`、supportClosure `0.969/d3`、avg/max `3.62/7`、localPatch `2`、dependencyFollow `3`、outer dynamic pressure <=1。
- 该 accepted 新链为 `430;406;405`，meta=`target=22;oldHit=16;blocker=430;role=CriticalTimingZone;dir=sameRay`。结论：critical/free-like 插入不是天然降难；只要作为 ray-first blocker 且 trace delta 稳定，可以继续小步提高 coverage。
- 当前局限：20 个候选只有 1 个严格通过，18 个 unsolved、1 个 local patch，说明 0.44+ 已是极窄窗口；下一步应做 anchor 多样性、trace-parent participation 检查，以及必要时的 paired compensation，而不是扩大随机预算。

## Ray First Blocker Balanced Anchor V2 - 2026-06-24

- 已按 GPT 二审结论确认当前瓶颈是 `single-ray selection collapse`，不是 ray-first 思路错误；V2 最小修正是在 `Build-RayFirstBlockerFillV1.ps1` 加 `MaxAnchorsPerTargetRay` 和 `MaxAnchorPool`，每条 target ray 最多取少量 high-priority anchors，再做候选采样。
- V2 对同一 0.441 父本的候选分布从“几乎单 target ray”变为 `25` 个 target ray；严格 gate 小批 `ray_first_blocker_fill_v2_balanced16_step442_c15_to448` 16 候选 4 accepted，accepted targets 包括 `37/51/18` 等，证明均衡 anchor 显著提高 accepted rate。
- 已连续迭代单链 V2，并每步重算 RayConstraintMap：`0.4411765 -> 0.4460784 -> 0.4522059 -> 0.4571078 -> 0.4620098`。各步均保持 `solved=True/processTier=A`、support-lock `d3/d4`、localPatch `<=3`、dependencyFollow `<=4`、动态外口 pressure <=1。
- 当前最高样本：`ray_first_blocker_fill_v2_balanced16_c09_to463_b01_c16`，coverage `0.4620098`、avg/max `2.9/5`、antiLocal `0.571`、supportClosure `0.969/d4`、localPatch `3`、dependencyFollow `4`，新增链 `509;508;507;531`，meta=`target=50;oldHit=59;blocker=509;role=CriticalTimingZone;dir=sameRay`。
- 关键结论：`CriticalTimingZone` 应作为 timed blocker actuator 精确使用，而非禁区；balanced ray-first blocker 可以稳定越过 0.45 并保持可解/难度。但最高样本 avgChoices 已降到约 `2.9`，下一阶段需防止过度压缩成单一路径，同时解决大量 rejected 仍为 `unsolved` 的问题。
- 下一步建议：继续 single-chain V2 到约 `0.48-0.50` 做短程稳定性验证，同时加入 `choice lower band` / `trace-parent participation` / `ray target cooldown`；若 accepted rate 再次塌陷或 avgChoices 低于目标带，应进入 `limited paired compensation`，而不是直接全局 region fill。

## Ray First Iterative Runner and New Boundary - 2026-06-24

- 新增自动 wrapper `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Invoke-RayFirstBlockerIterativeFillV1.ps1`：每轮 `selected parent -> Build-RayConstraintMapV1 -> Build-RayFirstBlockerFillV1 balanced-anchor -> trace gate -> 按 avg 下限/coverage/traceScore 选下一父本`。
- smoke 从 avg 较稳的 `ray_first_blocker_fill_v2_balanced16_c09_to463_b01_c09`（coverage `0.4607843`、avg/max `3.03/6`）出发，自动推进到 `ray_first_blocker_iterative_v1_from0460_to0478_smoke_r01_fill_to467_b01_c05`，coverage `0.4669118`、avg/max `3.05/6`、support `0.97/d4`、localPatch `3`、dependencyFollow `3`。
- 下一轮目标 `0.4729` 时 `12/12` 候选均 trace 完成但 `0 accepted`，失败全部为 `unsolved`；失败候选的 avg/max 不一定坏（例如 `3.39/6`），说明瓶颈不是变简单，而是 causal reachability 被新增 C 链切断。
- `0.4669` 父本 map 显示：GuardSlot=`0`、BodyOnlyRayBlocker=`12`、CriticalTimingZone=`140`、HeadAllowed=`163`、SafeFill=`56`、HighRiskFreeHead=`64`。系统已进入“无 guard buffer 的 critical timing 层”，继续 blind single-chain 会高概率堵死。
- 已再次咨询 GPT/Pro：建议不要立刻写 full paired generator，而先做 `dependency-loss diagnostic`。下一刀应比较 C 插入前后 trace/dependency，定位 `lost_dependency_edges`、`first_unreachable_target`、`motif_break_point`，再从这些断点抽取 D repair anchor；D 的职责是恢复原 causal graph continuity，而不是随机控制 C escape 或做 cosmetic fill。
- 当前判断：目标 0.95+ 仍未达成，但 single-chain balanced ray-first 已证明可把一个父本类从 `0.441` 自动/半自动推到 `0.467` 并保持难度；要继续 0.48+，必须进入“C-break 诊断 -> D repair anchor extraction -> C+D minimal causal repair loop”。

## Ray First Loss Diagnostic and Old-Path Equivalence Probe - 2026-06-24

- 新增/修复 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-RayFirstBlockerLossDiagnosticV1.ps1`，用于比较 C 插入前后的 active dependency edges，并输出 `targetRetargetedToNew/newBlockOwners/changedCriticalOwners/directCutOwners/repairDescriptors/likelyFailureClass`。
- 对 `ray_first_blocker_iterative_v1_from0460_to0478_smoke_r02_fill_to473` 的 12 个失败 C 候选做诊断：`11/12 direct_dependency_cut`、`1/12 unclassified_unsolved`；`12/12 targetRetargetedToNew=True`，说明 C 都成功成为目标链新 blocker，但依赖时序被改写后不可解。
- 重要反例：这些 C 自己并非 direct/free exit；post map 显示新增 owner `62` 都有 firstHit（被现有链挡住）。因此失败不是“C 没被堵住”，而是 C 把 target 改接到错误 causal basin 或压扁 oldHit 的等价时序。
- 进一步检查：12 个失败 C 的 `C.firstHitOwner` 全部不在对应 target 的 `oldHit -> ancestors` 旧依赖路径中。例如 target `2` 的旧路径为 `60->58->16->3`，失败 C 却接到 `28` basin。结论：0.46+ 的 C 生成必须加入 `old-path equivalence`，不能只看“C 能否被某链挡住”。
- 已在 `Build-RayFirstBlockerFillV1.ps1` 加 opt-in `-RequireCandidateHitOldPath`，并输出 `selfHit/oldPath/selfHitOldPath` 到 `addedMeta`；默认不开启，不影响旧产线。另加 `TargetOwnerFilter` 与单格 ray 方向推断，支持 C timing repair/诊断。
- 负结果：只对失败 C 的 owner `62` 做 D timing repair（D 阻挡 `C.escapeRay -> C.firstHit` transition）时，`24/24` 仍 `solved=False/Drop`；说明“只延迟 C”不能恢复旧因果等价。
- 正结果：回到 `0.4669` 父本，在 C 生成阶段启用 `-RequireCandidateHitOldPath` 后，24 个候选中出现 1 个 solved/A 样本：`ray_first_blocker_oldpath_v1_r02_to473_relaxedlocal_smoke24_b01_c06`，coverage `0.4705882`、avg/max `2.57/5`、supportDepth `3`、dependencyFollow `3`、outer dynamic pressure `1`；但 `localPatchSolveRunMax=4`、hardStructureV3Score `0.29`，只能算方向 proof，不是 production hard。
- 当前下一步：把 `old-path equivalence` 从 hard bool 升级为排序/生成目标，优先选择 `selfHit` 落在 oldPath 中段而不是过早 root，并加入 localPatch 防退化；不要把 0.4706 relaxed-local 样本当最终关卡。

## Ray-Field Basin-Aware Fill Proof - 2026-06-24

- 已按用户反驳和 GPT 二审在 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-RayFirstBlockerFillV1.ps1` 增加 `-EmitDebtCounterfactuals`、`-AllowDebtFreeHeadCandidate`、`-RejectWrongBasinCandidate`。
- Counterfactual 证明：`FreeHeadDebt` 不是硬坏信号。`ray_debt_repair_probe_v1_step488_freec_smoke12_b01_c09_cOnly` 为 free-head C-only，却 `solved=True/A`、coverage `0.4901961`、avg/max `2.5/5`、anti `0.508`、support `0.845/d3`。同 C 加 D 后反而 `solved=False/support=0`。
- Clean blocker pair 仍是当前稳定增长源：`ray_debt_repair_probe_v1_step488_freec_smoke12_b01_c11` 从 `0.4877451 -> 0.4926471`，avg/max `2.59/5`、anti `0.523`、support `0.848/d3`、local/follow `3/3`。
- 第二轮 clean pair 推到 `ray_debt_repair_probe_v1_step493_to500_freec_smoke16_b01_c05`，coverage `0.4987745`、avg/max `2.62/5`、anti `0.507`、support `0.858/d3`、local/follow `3/3`。
- 直接两链冲 `0.505` 失败：solved rows 多为 anti/support/local/follow 退化，WrongBasin C+D 仍多为 unsolved。GPT 复审后结论是 0.50+ 瓶颈为 bad causal basin，而不是通用 D repair。
- Basin-aware 单链批 `ray_basin_aware_fill_v1_step499_to502_single_smoke24` 经手动 trace gate 选出 4 个合格样本，当前最佳 `..._b01_c06`：coverage `0.502451`、solved/A、avg/max `2.44/5`、anti `0.500`、support `0.858/d3`、local/follow `3/3`、动态外口 pressure `1`。
- 当前最高已验证父本 CSV：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ray_basin_aware_fill_v1_step502_base_abs.csv`。下一步应从该父本重算 RayConstraintMap，使用 basin history/cooldown + single micro-slot 继续推进，不要再强制 D repair。

## Ray-Field Boundary Cascade and 0.508 Internal Growth - 2026-06-24

- 已新增并运行 `Build-OuterPressureGuardProbeV1.ps1`，专门验证 0.502 父本的 target64/owner72 外口债：D 必须占住 owner72 的 direct ray cell 408，并枚举包含 408 的短链。
- 结果：3 个合法 D 中 2 个无解（D 自己被 owner12 挡住后 support closure 掉到 0/d1），1 个可解但不达标：`ray_outer_pressure_guard_v1_target64_owner72_body408_probe_c03` coverage `0.5085784`、solved/A、avg/max `2.36/5`、support `0.858/d3`，但 `dependencyFollowRunMax=7`、`outerExitHeadCount=2`。post map 证明 `72 -> 73` 成立，但新 D(owner73) 变成沿边界长 direct-exit ray。
- 结论：target64 是 valid basin 但会生成 boundary cascade，不应进入正式父本池；不要继续 perimeter shell guard，除非先有 boundary-cascade depth/path gate。
- 回到 0.502 父本，排除 target64 与旧退化 target8 后，非边界内部 C 小片前 16 个中 2 个通过正式 gate。当前最高新父本：`ray_basin_aware_fill_v1_step502_to506_nonboundary_smoke64_b01_c01`，coverage `0.5085784`、solved/A、avg/max `2.53/5`、anti `0.500`、support `0.858/d3`、local/follow `3/3`、动态外口 pressure `1`。已固化为 `ray_basin_aware_fill_v1_step509_base_abs.csv` 并生成 `ray_basin_aware_fill_v1_step509_map_*`。
- 从 0.508 再冲 0.513 的 24 个非边界候选 0 通过：22 unsolved、2 anti-local/follow 退化。说明 0.508 后又进入 internal critical timing 边界；下一步应降低步长、加入 target cooldown/shorter path bias，并对失败 C 做 loss diagnostic，而不是扩大随机候选或追 boundary cascade。

## Strict Dual Gate Joint-Core Variant V2 Fill Review - 2026-06-23

- 已在 `Build-RootTopologyTemplateGeneratorV1.ps1` 增加 strict dual shared-core 的 4 个空间实现变体：base / vertical / right / down；它们仍属于同一 root family（`strict_dual_gate_shared_core`），用途是验证同一 strict dual motif 的空间变体体感，不是证明新 root 物种。
- source `dual_gate_joint_core_variant_v2` 4/4 通过 strict dual source trace；随后用 `Build-HardLockSlotDirectedBatchFillV1.ps1 -RequireStrictDualGate` 生成 4 个 filled finals，coverage `0.1115-0.1275`，avgChoices `3.71-4.94`，maxChoices `6-7`，antiLocal `0.769-0.909`。
- 已冻结并挂 Demo：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DualGateJointCoreVariantV2FillReviewPack.asset`，guid=`a033c607578648f9a3330af450abde68`；Demo activePack 已指向该包。
- Frozen retrace：`dual_gate_joint_core_variant_v2_fill_review_frozen_trace_metrics.csv`，4/4 `solved=True/processTier=A/strictDualGateCandidate=True/raw=True`；gate core 固定为 `6`，gateA=`5`，gateB=`2`，gate distance `5/6/9/9`，localPatch `1-2`，dependencyFollow `1-3`。
- 用户肉眼反馈：这组和旧 root 有明显不同，且 4 个 strict dual 变体“不算太同质化”。结论：`strict_dual_gate_shared_core` 可升级为已认可的第二 root family。
- 下一步：把 strict dual shared-core 纳入生产路径，优先做 coverage/肉感扩张与 8-12 关 review pack；继续保持 `strictDualGateCandidate=True` 作为身份 gate，不用再证明它是不是新 root。


## Strict Dual Gate Production-Style Expansion T018 - 2026-06-23

- GPT/Pro reviewed next step after user accepted `strict_dual_gate_shared_core` as second root family. Consensus: do not jump to `0.25+`; first expand strict dual density into `0.16-0.21` identity-preserving zone and produce 8-12 review levels.
- Ran two strict-dual fill batches from `dual_gate_joint_core_variant_v2_fill_target012_final_selected.csv` using `Build-HardLockSlotDirectedBatchFillV1.ps1 -RequireStrictDualGate`, target coverage `0.18`, seeds `966410` and `966411`.
- Merged 8 exact-hash-unique finals into `dual_gate_joint_core_variant_v2_fill_target018_review8_selected.csv`; coverage `0.1801-0.1961`, avgChoices `3.50-4.42`, maxChoices `6-7`, antiLocal `0.75-0.95`.
- Frozen and mounted Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DualGateJointCoreVariantV2FillReview8T018Pack.asset`, guid=`c1687786dc944d18bb719a04b489fb13`.
- Frozen retrace: `dual_gate_joint_core_variant_v2_fill_review8_t018_frozen_trace_metrics.csv`, 8/8 `solved=True/processTier=A/strictDualGateCandidate=True/raw=True`; `AOnly=False/BOnly=False/APlusB=True`, `AInfluencesB=False/BInfluencesA=False`, localPatch `1-2`, dependencyFollow `2-4`, outerExitHead `0`, TrueHardCandidate `8/8`.
- Next user review: judge whether T018 density feels like fuller dual-gate levels and whether 8 levels still feel diverse enough. If accepted, next production step is either broaden seeds/variants at T018 or cautiously probe T021, not 0.25+.

## Strict Dual Gate T030 Proof Pack - 2026-06-24

- Root expansion line checkpoint: `strict_dual_gate_shared_core` has a frozen 0.30 proof pack and is no longer only a low-density root prototype.
- Frozen and mounted Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DualGateJointCoreVariantV2FillProof2T030Pack.asset`, guid=`6c8fdbedca9d4c56a4893b67b194c1d9`.
- Source selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_joint_core_variant_v2_fill_target030_proof2_selected.csv`; selected coverage values `0.3015` and `0.3039`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_joint_core_variant_v2_fill_proof2_t030_frozen_trace_metrics.csv`; 2/2 `solved=True/processTier=A/strictDualGateCandidate=True/raw=True`, A-only/B-only false, A+B true, A/B temporal interference false/false, outerExitHead=0.
- Next root-family task: pursue `web_crossover` as third causal root candidate. Do not spend this lane on full high coverage; another thread owns that work.

## Path-Aware 0.512 Review Pack - 2026-06-24

- GPT challenged the ray-saturation hypothesis: ray coverage is only geometry saturation; 0.50+ fill must track path/motif impact, not just whether a new chain has free/direct head risk.
- Added diagnostic script `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PathAwarePlacementProbeV1.ps1`. It merges candidate trace metrics with pre/post ray-edge rewrites and classifies placements as `ClosedHardFill`, `ClosedDebtFill`, `MotifBroken`, `PathDisconnected`, `Linearized`, `BoundaryCascade`, etc.
- Applied it to the 0.508 parent cooldown-short batch. First 12 candidates split into `ClosedDebtFill=5`, `ClosedHardFill=3`, `MotifBroken=4`. The failure cases are explained by direct cuts of critical owners such as `17/40/44`, not by generic free-head status.
- Selected the strongest inspectable 0.512 sample `ray_basin_aware_fill_v1_step509_to511_cooldown_short_smoke24_b01_c03`: coverage `0.5122549`, `solved=True/A`, avg/max `2.54/6`, antiLocal `0.515`, support `0.858/d3`, local/follow `3/4`, dynamic outer pressure `1`.
- Frozen and mounted Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PathAwareStep512C03ReviewPack.asset`, guid=`763823fa3f75436d9caf80a7d54b991f`; Demo activePack now points to this pack.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/path_aware_step512_c03_review_frozen_trace_metrics.csv`; 1/1 `solved=True/processTier=A`.
- Next validated-root expansion task after user review: use path-aware placement class as the next gate, not raw single-chain `ray-first` acceptance. Continue from the 0.512 map `path_aware_probe_v1_step512_c03_map_*` and avoid candidates that directly cut critical owners unless the batch closes the debt in trace.

## Web Crossover Root Proof V1 - 2026-06-24

- Root expansion line checkpoint: third causal root candidate `web_crossover` now has a frozen proof pack near `0.30` coverage.
- Added classifier script `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-WebCrossoverRootClassifierV1.ps1`; it treats web as trace-visible multiple independent causal closures, not visual/layout web.
- GPT-reviewed web V1 gate: `>=2` independent causal hubs/closures, no strict dual masquerade, no single-hub tri collapse, cross-region trace flow, solved/A-or-S, localPatch<=3, dependencyFollow<=4.
- Source pool: current trace of `species_web_four_fill_v2_cov28_b01_r1_candidates.csv` yielded 6 web-pass parents. Directed fill pushed 5 finals to coverage `0.288-0.2978`; last push toward `0.303` found no acceptable filler.
- Frozen and mounted Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_WebCrossoverV1RootProofT030Pack.asset`, guid=`8e92cc9a9d504068ae1c4893760dacb7`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/web_crossover_v1_root_proof_t030_frozen_trace_metrics.csv`; 5/5 traced with missing/failed=0.
- Frozen web gate: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/web_crossover_v1_root_proof_t030_frozen_webgate_web_roots.csv`; 5/5 `webCrossoverCandidate=True`, independent causal hubs `2-3`, qualified hubs `3-4`, supportDepth=4, fanout=3-4.
- Next: ask GPT whether to attempt `hub_spoke` next or define another root primitive; do not dilute this lane into high-coverage work.

## Ray-Constrained Cavity Fill Smoke - 2026-06-24

- GPT/Pro 审稿确认：0.51+ validated-root 扩张应从 `ray-first patch` 切到 `ray-constrained cavity generation`；ray-first 只保留为物理约束/解释层，不再作为主生成器。
- 新增实验脚本 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-RayConstrainedCavityFillV1.ps1`：从 0.512 父本的中心空洞出发，在局部 ray-interference field 内生成 SGP-style 弯链候选，再交给 board trace 与 path-aware probe。
- 单链 smoke：20 个候选中 9 个 solved/A，5 个满足 strict-safe（supportDepth>=3、local<=3、follow<=4），path-aware good5 全部为 `SoftDebtNoRetarget`；安全候选覆盖仅到 `0.5159-0.5172`。
- 两链/三链 batch smoke：分别 20/20 和 20/20 全部无解；三链可到 `0.5257-0.5331` coverage 但大面积 support/motif collapse。
- 当前结论：方向可行，但必须“单链或逐步 trace-settled micro-fill”；不能把多个中心空洞链同时写入。下一步应选择 strict-safe 单链作为下一父本、重算 RayConstraintMap 后迭代，或做基于 post-map debt 的真正 pair fill。
- 用户纠偏后已调整 path-aware 分类：`dependencyFollowRun/localPatch` 不再是一超线就硬拒绝。部分顺消可作为 `SoftLinearizedDebt` 次选继续参与排序；只有极端传送带（如 followRun≈10 或低选择长跑）才标 `ConveyorCollapse` 硬拒绝。
- 重新分类 0.512 单链 solved9：`SoftDebtNoRetarget=5`、`SoftLinearizedDebt=2`、`MotifWeakened=1`、`ConveyorCollapse=1`。其中 c18（follow=5/supportDepth=4）和 c10（follow=6/supportDepth=3）可作为次选，不应被早期 hard gate 卡掉。

## Bare SGP Fill Probe From 0.512 - 2026-06-24

- 用户纠偏：先用当前 SGP 裸跑，看补肉生成能力本身，不要一开始用 hard/path-aware/顺消 gate 卡住；再逐层加约束。
- 已给 `Build-ValidatedRootBackgroundSGPFillV1.ps1` 增加 opt-in `-AcceptAllTraceRows`，仅用于裸跑实验；默认不影响正式 gate。
- 0.512 父本上三档结果：
  - `ProtectedChainCount=14`，即原核心保护强：只能新增 1 条链，coverage 到 `0.517-0.520`，说明保护太强时 SGP 候选空间极窄。
  - `ProtectedChainCount=0`，完全裸跑：能直接补到 `0.593-0.600`、新增 9-11 链，但 8/8 无解且 supportDepth 多为 0，说明纯几何 SGP 有填满能力但会破坏因果结构。
  - `ProtectedChainCount=12`，中等保护：能补到 `0.546-0.549`、新增 5 链，choice/anti 不爆且部分 supportDepth=4，但 4/4 无解；path-aware 分类为 `PathDisconnected=2`、`MotifBroken=1`、`BoundaryCascade=1`，关键改写集中在 critical owners `15/16/17/51`。
- 当前思路 checkpoint：0.51 后真正矛盾不是“SGP能不能填”，而是“哪些核心 ray/owner 需要保护，哪些区域可裸填”。下一步应先整理分层约束顺序，再决定是否继续实验。
- 结论报告：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ray_constrained_cavity_fill_v1_step512_smoke_conclusion.md`。

## Naked Background SGP Baseline From 0.30 - 2026-06-24

- 用户提出：从 0.30 初始父本出发，在“不约束”的情况下先让 SGP 裸跑，确认几何补满基线是否存在，再逐层加约束。
- 使用 `hardlock_030_dynamic_outer_gate5_selected.csv` 第 1 个父本，`ProtectedChainCount=0`、`AvoidCoreRay=false`、`RequireExistingBlocker=false`、`AcceptAllTraceRows`。
- 分轮裸跑：第一轮从 coverage `0.3076` 冲到约 `0.5135`，第二轮冲到约 `0.7194`；trace 全部 `solved=False/Drop`，support closure 直接掉到 `0/d0`，说明裸 SGP 几何能快速补肉，但会立刻破坏可解因果骨架。
- 从 `0.7194` 候选继续裸跑只到约 `0.7365` 后无候选；一轮直冲 `0.95` 最高也只到 `0.7488`。当前 `Build-ValidatedRootBackgroundSGPFillV1` 是短折线/random-walk 背景填肉器，不是完整铺满式 SGP；碎空洞后会自然停在约 `0.74-0.75`。
- 关键产物：`validatedroot_sgp_naked_from030_to095_smoke1_*`、`validatedroot_sgp_naked_from072_to095_smoke1_*`、`validatedroot_sgp_naked_from030_to095_oneshot_smoke1_*`。
- 当前判断：0.30 后的基线不是“SGP 能直接补满且保持难度”，而是“SGP 有强几何补肉能力，但需要 critical owner/ray 级保护和更完整的空洞填充策略”。下一步不应每步硬卡 avgChoices；应把选择曲线作为包络/软债，同时先保护会导致 support/motif 断裂的少量 critical owner，再让 SGP 在剩余空间填肉。

## Seeded Direct-SGP + Micro-Fill Baseline From 0.30 - 2026-06-24

- 用户纠正：`Build-ValidatedRootBackgroundSGPFillV1` 不是原始 SGP，不能用它的 `0.74-0.75` 上限否定 SGP 满铺能力；必须先验证真正 SGP 类规则在 0.30 父本上能否无约束接近满覆盖。
- 新增实验脚本 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SeededDirectSGPFillBaselineV1.ps1`：把 package `DirectRectangleArrowLevelGenerator` 的 layer-head/inward-growth/family scoring/remaining heads 思路改造成 seeded fill baseline；可选 `-EnableMicroFill` 对应项目原有 seed/mask patch 里的 shape-heal/micro-fill 补碎空洞层。
- 修正重要 bug：最初 `Write-LevelAsset` 查找 `paths:` 字段，实际 LevelDefinition 使用 `authoredLevel.arrows:`，导致旧 `seeded_direct_sgp_micro_from030_to095_sweep_smoke4` 只改了 `arrowCoverage` 元数据，资产仍只有 37 链/251 格/真实 coverage `0.3076`。旧 review pack `SGPRhythmLab_SeededDirectSGPMicroFrom030To095SweepReview4Pack.asset` 不可再作为 0.95 证据。
- 修复脚本后重新生成 `seeded_direct_sgp_micro_from030_to095_sweep_fix1_smoke4`：4 个候选真实 coverage `0.9436-0.9510`、链条数 `141-157`、占用 `770-776` 格，证明 seeded Direct-SGP + micro-fill 的几何满铺能力成立。
- 修正版 0.95 trace 结果全部失败：4/4 `solved=False/processTier=Drop`，`avgChoices=19.16-21.28`、`maxChoices=38-41`、antiLocal 基本坍塌。结论从“无约束可满铺且保难度”修正为“无约束可满铺几何，但会破坏可解性与难度压力”。
- 已冻结并挂到 sgp-rhythm-lab Demo 作为视觉检查包：`SGPRhythmLab_SeededDirectSGPMicroFrom030To095SweepFix1Review4Pack.asset`，guid=`ca03633336ec4b20a42d659be39d01a5`。它不是 production hard 包，只用于看真正 0.95 满铺形态。
- 当前核心结论更新：路线 1 仍更靠谱，但父本限制必须保护 motif/critical owner；再交给 seeded Direct-SGP/micro-fill 后用 trace 筛选。不能把 coverage 元数据当真，后续必须用实际 authored arrows 重新数链条数/占用格验证。

## Small Canvas Seeded Direct-SGP Smoke - 2026-06-24

- 用户明确修正方向：不是在 24x34 大画布里局部框几个 cage，而是保证父本核心思路后把整个生成画布缩小，让 SGP 在小世界内补肉；小画布父本链条可以相应减少，不必保留全部 37 链。
- `Build-SeededDirectSGPFillBaselineV1.ps1` 已扩展 `CanvasWidth/CanvasHeight/MaxTotalChains`，并修复 LevelDefinition `authoredLevel.arrows` 写入。当前压缩方式是把 0.30 父本重映射到小画布，允许丢弃无法无冲突放入的小链。
- base-only 尺寸诊断：14x20 保留 25 链但不可解/support=0；16x22 保留 26 链但不可解/supportDepth=1；18x24 保留 28 链、coverage `0.3449`，`solved=True/A`、avg/max `2.54/6`、supportScore `0.888`、supportDepth `3`、outer=1；20x26 保留 32 链但不可解。当前最小不坏画布是 18x24。
- 在 18x24 上做链数预算：max36（总 36 链）全部可解，coverage `0.463-0.509`，其中 2 个 `processTier=B`，best B `coverage=0.4884`、avg/max `3.97/10`、supportDepth=3、outer=8；max40/44 全部仍可解但 choices/outer 明显上升并 Drop。当前阈值约为 18x24 + 36 总链。
- 已冻结并挂到 Demo：`SGPRhythmLab_SeededDirectSGPSmallCanvas18x24Max36Review6Pack.asset`，guid=`c2d629dd67e342c48cd0efc3a467112d`。这是小画布路线验证包，不是最终 production 包。

## Root Family GPT Advisor Conversation - 2026-06-24

- User updated the Rosetta/ChatGPT advisor target for this root-family expansion line to conversation id `WEB:bf5c018e-8461-4f2e-a336-4f43782a52f2`.
- Future GPT consultations for root definitions, root-family strategy, and uncertainty in this lane should use that conversation id instead of the previous `6a36691e-0ebc-83e8-9dae-6d28174a2491`.

## Hub-Spoke Root Proof T0288 - 2026-06-24

- Root expansion line checkpoint: fourth causal root candidate `hub_spoke` now has a frozen proof/review pack near `0.288` coverage.
- Definition used: one initially clear central hub unlocks multiple spatially separated spoke branches; reject hidden convergent-core/web/tri collapse. Final truth is board trace plus `Build-HubSpokeRootClassifierV1.ps1`.
- Empirical growth path: true source -> `0.2255` -> `0.2414` -> `0.2659` -> `0.2806`; all solved/A and hub-spoke gate true. A further generic fill reached `0.3027` solved/A but failed hub-spoke identity, while same batch highest true hub-spoke was `0.288`.
- GPT/Pro reviewed and agreed: freeze `~0.288` as canonical hub-spoke root proof; do not force 0.30 by postfiltering because it crosses root identity boundary.
- Frozen and mounted Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HubSpokeTrueV5RootProofT0288Pack.asset`, guid=`4700f5c8ee954d7da359c1185256a872`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hub_spoke_true_v5_root_proof_t0288_frozen_trace_metrics.csv`; 3/3 `solved=True/processTier=A`, avgChoices `3.30-3.38`, maxChoices `7`, supportDepth `4`.
- Frozen hub-spoke gate: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hub_spoke_true_v5_root_proof_t0288_frozen_gate_hub_spoke_roots.csv`; 3/3 `hubSpokeCandidate=True`.
- Next root-family work: move to next root primitive; revisit hub-spoke density only with a dedicated identity-preserving/spoke-safe fill objective, not generic density fill.

## Checkpoint - 2026-06-24 09:00 - Cascade Relay V1 root proof frozen

- Goal line: root family expansion; cascade_relay is now proven as a bounded linear-propagation root, not a 0.30-growth root.
- GPT advisor conversation in use: https://chatgpt.com/c/6a3b0ea9-c568-83e8-ba82-d42201a1d66e . GPT agreed with measured boundary: freeze ~0.20-0.21 instead of forcing 0.30.
- Reliable highest cascade pass before freeze: cascade_relay_v1_fill_from_c24_t023_smoke1_b01_r1_c13, coverage 0.2071, solved A, avgChoices 2.89, maxChoices 5, fanout 2, dependencyFollowRunMax 4, localPatchSolveRunMax 3.
- Frozen pack: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_CascadeRelayV1RootProofT0207Pack.asset, GUID c7d563624d224126937c70db12e42430.
- Frozen retrace: cascade_relay_v1_root_proof_t0207_frozen_trace_metrics.csv; 3/3 solved A and 3/3 cascadeRelayCandidate=True in cascade_relay_v1_root_proof_t0207_frozen_gate_cascade_roots.csv.
- Demo scene activePack now points to SGPRhythmLab_CascadeRelayV1RootProofT0207Pack for visual review.
- Next root primitive: split_key_dependency; implement source + gate, then grow only within identity boundary.

## Checkpoint - 2026-06-24 09:24 - Split Key V1 root proof frozen

- Implemented split_key_dependency topology source in Build-RootTopologyTemplateGeneratorV1.ps1: one core escape ray with three independent lock states (ayBlockers=3;4;5).
- Implemented Build-SplitKeyRootClassifierV1.ps1; current gate checks 3-lock core ray, non-dual, low fanout, non-cascade, local stability, and region split.
- Source proof: split_key_v1_source_cov08_* passed with ayBlockerCount=3, strictDualGateCandidate=False, fanout 1, followRun 1, localPatchRun 2.
- Growth chain reached stable split-key boundary at ~0.20: reliable highest pass split_key_v1_fill_from_c07_t022_smoke1_b01_r1_c11, coverage 0.2034, solved A, avgChoices 3.77, maxChoices 7, fanout 2, followRun 3, localPatchRun 2.
- Frozen pack: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SplitKeyV1RootProofT0203Pack.asset, GUID fc53728dbb824b9d8e724b1586d97d0b.
- Frozen retrace/gate: split_key_v1_root_proof_t0203_frozen_trace_metrics.csv and split_key_v1_root_proof_t0203_frozen_gate_split_key_roots.csv; 3/3 solved A and 3/3 splitKeyCandidate=True.
- Demo scene activePack now points to split-key proof pack for review.
- Next root primitive in queue: orbit_delay.


## Checkpoint - 2026-06-24 09:33 - Root discovery saturated; mixed root-family review pack mounted

- GPT advisor conversation in use: https://chatgpt.com/c/6a3b0ea9-c568-83e8-ba82-d42201a1d66e . Latest GPT review agreed blocker-mask/conflict/reverse cannot be independent roots under static straight-ray + clear-trace physics; they collapse into existing families.
- Root discovery phase is now considered saturated for current rules. Independent proof families available: support_lock, strict_dual, web_crossover, hub_spoke, cascade_relay, split_key.
- Created mixed review source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/mixed_root_family_review_v1_selected.csv` (12 rows, 2 per family).
- Frozen mixed review pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_MixedRootFamilyReviewV1Pack.asset`, GUID `1dc9459927164d23899ab69b33b9b9f6`.
- Frozen trace: `mixed_root_family_review_v1_frozen_trace_metrics.csv`; 12/12 solved, 12/12 A tier.
- Identity audit: `mixed_root_family_review_v1_identity_audit.csv`; designated family gates pass for strict_dual, web_crossover, hub_spoke, cascade_relay, split_key, and support_lock rows keep support closure hard signal.
- Demo scene activePack now points to `SGPRhythmLab_MixedRootFamilyReviewV1Pack` for user visual/feel review.
- Next production phase: root-family mixer + collapse detector; do not invent new primitive unless game physics changes.

## User Feedback - 2026-06-24 09:41 - MixedRootFamilyReviewV1 visual root overlap

- User reviewed SGPRhythmLab_MixedRootFamilyReviewV1Pack and observed Level 1 (support_lock) and Level 5 (web_crossover) feel basically the same root.
- Project-side interpretation: current family identity audit can mark secondary motifs (webCrossoverCandidate=True) while the player-facing primary root remains support-lock. Mixed review should distinguish primary root from modifier/secondary motif.
- Required correction: future production/root review should assign one primaryRootFamily by dominant causal driver; web-crossover must pass a stronger standalone gate or be downgraded to support-lock variant/modifier.

## Small Canvas Outer-Frame Seeded Direct-SGP Breakthrough - 2026-06-24

- User correction accepted and validated: for fuller validated-root production, first shrink the whole board and predefine/seal the outer frame, then let seeded Direct-SGP fill inward. This is better than local cages in the 24x34 parent and better than a few controlled exit teeth.
- Script path: `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SeededDirectSGPFillBaselineV1.ps1`; active useful knobs are `CanvasWidth=18`, `CanvasHeight=24`, `PreseedOuterFrame`, `OuterFrameSegmentLength=7`, `OuterFrameMaxChains=8`, `MinHeadLayer=1`, `MaxTotalChains=44`.
- Smoke result `seeded_direct_sgp_smallcanvas_18x24_frame8_max44_smoke8`: 8/8 solved; selected review rows are the 4 A-tier rows with `supportClosureBestDepth>=3`. Selected coverage `0.5602-0.6134`, total chains `44`, compacted parent kept/dropped `28/9`, outer frame added `8`.
- Frozen and mounted Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SC18F8Max44Review4Pack.asset`, guid=`68c0042da95a42f688a0dc8f77f93581`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/seeded_direct_sgp_smallcanvas_18x24_frame8_max44_review4_frozen_trace_metrics.csv`; 4/4 `solved=True/processTier=A`, avg/max roughly `4.27-4.68 / 10-11`, supportDepth `3`, localPatch `2-3`, dependencyFollow `3-4`.
- Important caveat: static `outerExitHeadCount` remains `10-11` because the preseeded frame segments count as heads; dynamic outer pressure is bounded (`outerExitAvailableChoiceMax=5-6`, `outerExitSolveRunMax=2-3`). Next step is user visual/feel review, then push the same outer-frame route with stricter dynamic outer and diversity gates.

## Local Room Role-Constrained SGP Probe - 2026-06-24

- User correction accepted: the local-room proof must feed SGP a cell-role constraint map, not just crop a room and run naked fill.
- Added experimental script `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-LocalRoomSGPFillProbeV1.ps1`. It clips a small rectangular room from an existing level, derives roles from local escape rays (`K` occupied/fixed, `E` reserved exit corridor, `M` must-block, `B` body-only ray corridor, `H` head-allowed, `S` safe fill), then constrains SGP-style fill so heads cannot spawn on body/must-block/exit cells and bodies are rewarded for occupying `M`.
- 8x10 mid-room smoke with reserved exits (`local_room_role_sgp_probe_v1_smoke2_midroom`) showed role-constrained fill can push local coverage from `0.4375-0.4625` to `0.5625-0.6875`, with many solved A/S/B, but no `M` cells because exits were reserved.
- 8x10 must-block smoke (`local_room_role_sgp_probe_v1_smoke3_mustblock`) forced `ReservedExitCorridors=0` and required occupying `M`: 12 candidates, 9 solved, 5 A, coverage up to `0.7125`; must-block occupancy `1-4` cells.
- 10x12 must-block smoke (`local_room_role_sgp_probe_v1_smoke4_10x12_mustblock`) produced 8 candidates, 4 solved (1 A, 3 B), coverage up to `0.675`; must-block occupancy `1-2` cells.
- Current interpretation: the role-constrained approach is real but currently behaves as a safe/pressure fill controller, not a 0.9+ local fill solver. To reach fuller coverage, the next step needs multi-pass role recomputation and/or staged room expansion rather than forcing one-pass SGP to fill all constrained cells.
- E-role A/B validation (`local_room_role_sgp_probe_v1_abtest_e_empty` vs `local_room_role_sgp_probe_v1_abtest_e_terminal`) compared the same 24 room/variant pairs. Treating `E` as terminal-capable instead of reserved-empty improved coverage in 16/24 pairs, average coverage delta `+0.0365`, max solved local coverage `0.7375` vs `0.6875`, and did not change solved count (`15/24` both). It occupied exit cells on average `1.92` cells / `0.79` heads.
- Caveat: terminal-capable `E` did not automatically strengthen the hard motif; one paired sample lost `supportClosureBestDepth>=3`, and high-coverage terminal rows mostly had `supportClosureBestDepth=0`. Next local-fill step should treat `E` as endpoint/head candidate selected by a path-cover/flow layer, not as ordinary free S/H filler.
- Added local-fill ordering/candidate experiments to the same script: `FillOrder=InnerFirst` prioritizes deeper remaining cavities, and `HeadCandidateMode` now supports `Ring`, `RoleAny`, and `Frontier`.
- Ring + InnerFirst alone was not enough: `local_room_role_sgp_probe_v1_innerfirst_e_terminal` only nudged max solved local coverage to `0.75` and had solved `14/24`.
- RoleAny proved the cell-role space can be nearly filled: `local_room_role_sgp_probe_v1_innerfirst_roleany_e_terminal` reached average coverage `0.9047` and max `0.9125`, but solved collapsed to `1/24`, showing arbitrary internal heads create dead/cyclic flow.
- Frontier is the current best local proof: heads can be anywhere role-valid, but must face current boundary/occupied frontier while chain bodies eat inward. `local_room_role_sgp_probe_v1_innerfirst_frontier_e_terminal` reached average coverage `0.9089`, max `0.9375`, and solved `7/24`; best solved local rooms reached `0.925`. Remaining blocker is not geometric coverage, but preserving solvable/global causal order and hard motif at high density.

## Full 0.30 Parent Frontier Fill Test - 2026-06-24

- Tested the original validated 0.30 parent `near_miss_filler_orientation_v1_probe3_v006` (24x34, base coverage `0.307598`, trace A, avg/max `4/6`, openers `5`, supportDepth `3`, supportScore `0.966`, antiLocal `0.625`, dynamic outer max `1`) as one whole role-constrained room.
- Tool/mode: `Build-LocalRoomSGPFillProbeV1.ps1`, `FillOrder=InnerFirst`, `HeadCandidateMode=Frontier`, `ExitRoleMode=Terminal`, full board `RoomWidth=24`, `RoomHeight=34`.
- Geometry fills succeed: target `0.90` produced coverage `0.8995-0.9044` with 147-156 total chains.
- Solvability/difficulty collapse immediately: all full-board targets `0.65`, `0.75`, `0.80`, `0.85`, `0.90` were `solved=False/Drop`, supportDepth `0`, supportScore `0`, antiLocal `0`. At `0.65`, openers already rose to `13-14` and dynamic outer max to `13-14`; at `0.90`, openers `25-26` and outer max `23-24`.
- Conclusion: Frontier fill works as a local cavity strategy, but whole-board application destroys the 0.30 parent by creating many independent/frontier exits. Next approach must apply Frontier room-by-room or shell-by-shell with trace settlement and a cap on newly introduced openers/outer frontier, not as one global fill pass.
- Small-canvas roomwise test on the 18x24 support-lock review parent (`seeded_direct_sgp_smallcanvas_18x24_frame8_max44_smoke8_b01_c07`, base coverage `0.6134`) showed the same distinction. Roomwise `Frontier` can push coverage to `0.80+`, but all variants became `solved=False`, supportDepth `0-1`, and LocalEasy.
- Added `HeadCandidateMode=Clearable`: new head must face cells of currently statically available/clearable chains, not any occupied/boundary frontier. On 18x24 small canvas, `Clearable` + 1 room added only 1 chain, reached coverage `0.6227`, and preserved solved A + supportDepth `3`; adding a second room/second chain reached `0.6296` but became unsolved with supportDepth `0`.
- Interpretation: clearable-frontier is the correct safety direction but too coarse as a batch room fill. It must become an iterative micro-fill loop: propose one chain facing current clearable frontier, board trace accept/reject, update clearable frontier, then continue. Room count alone is not a safe batch boundary.

## Original Seed Long-Chain Review V1 - 2026-06-24

- User requested several structures from original seeds with many long chains, to inspect historical long-chain language separately from the current root/fill production lane.
- Created review pack from top `ImportedOriginal` rows in `reference_seed_structure_top_complex_298.csv`, selecting 8 original reference seeds by `complexChainScore` / long-chain metrics.
- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedLongChainReviewV1Pack.asset`, GUID `9d4fe47b51bb4d11b2e5525bbbe360e2`.
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_long_chain_review_v1_selected.csv`.
- Top selected metrics include `level_540` with 112 chains, coverage `0.982`, avgChain `15.911`, maxChain `63`, longChainRate `0.554`; selected set spans 42-157 chains and coverage `0.851-0.982`.
- Demo scene in `.worktrees/sgp-rhythm-lab` now points to this review pack. This is a visual/structure-language inspection pack, not a hard/root proof pack.

## Original Seed Long-Chain Skeleton Review V2 - 2026-06-24

- User clarified the target is not merely high `longChainRate`; it is the visual/structural skeleton language of many medium/long chains spread across a near-full board. In V1, rows 4/7/8 were judged not to count because they were too sparse/local despite passing some chain metrics.
- Created V2 review pack excluding V1 rows 4/7/8 and adding stronger full-board long-chain originals: `level_463`, `Arrowz_level_074`, `Arrowz_level_070`.
- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedLongChainSkeletonReviewV2Pack.asset`, GUID `d313fdfd4c504b66be8826758a8bbba5`.
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_long_chain_skeleton_review_v2_selected.csv`.
- Selected set: 8 `ImportedOriginal` seeds, 64-157 chains, coverage `0.865-0.982`, avgChain `10.410-15.911`, maxChain `48-66` except no sparse/local-long rows. Demo scene is now mounted to V2.

## Original Seed Difficulty Skeleton Extraction - 2026-06-24

- User asked to extract the difficulty skeleton from original long-chain seeds, not just display full long-chain originals.
- First static extraction via `Build-CausalSeedSkeletonParentsV1.ps1` at T020/T030 kept solved long-chain pressure but usually lost trace-visible support closure (`supportClosureBestDepth` mostly 0-2), proving static fanout/long-chain ranking is not enough.
- Ran lightweight full trace on V2 originals with `MaxCounterfactualMovesPerStep=0`; original full seeds were all solvable but generally too loose (`avgChoices=5.04-7.84`, max `11-16`). Several had strong full-level support closure (`d3-d4`), especially level_510, Arrowz_070, Arrowz_123, Arrowz_074, level_540, Arrowz_095.
- Added script `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-OriginalSeedDifficultySkeletonFromTraceV1.ps1`, which seeds extraction from each original full trace `supportClosureBestRoot`, preserves descendants/ancestors, then adds high-score structural chains.
- Best frozen review pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedDifficultySkeletonT050Review8Pack.asset`, GUID `88aa67560fa7477ea7e6f784bcc70e4e`; Demo is mounted here.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_difficulty_skeleton_t050_review8_frozen_trace_metrics.csv`; 8/8 solved. Level 4 / Arrowz_level_074 is the clearest true extracted difficulty skeleton: 52 chains, A tier, avg/max `3.96/8`, support `0.838/d4`, antiLocal `0.375`. Others mostly preserve choice-pressure or d2 shells, showing many full originals need a broader global density shell to retain their difficulty.

## Availability Peel Skeleton V1 - 2026-06-24

- User proposed a stronger skeleton extraction idea: if a full seed has many currently available easy moves, remove all currently available/free chains and inspect the remaining board as the difficulty skeleton.
- Implemented `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-AvailabilityPeelSkeletonV1.ps1`: iterative wave peel removes all chains whose escape ray is currently unobstructed while available count >= threshold, stopping at remaining chain/coverage limits. This targets continuous easy solves and isolated chains without blocker relations.
- Ran on `OriginalSeedLongChainSkeletonReviewV2` with `MinAvailableToPeel=8`, `MaxPeelWaves=12`, `MinRemainingChains=24`, `MinRemainingCoverage=0.18`.
- Result: 8/8 availability-peel skeletons generated and frozen; Demo mounted pack `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedAvailabilityPeelMin8Review8Pack.asset`, GUID `78403564a5c14a2789262cc9acfb24e2`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_availability_peel_min8_review8_frozen_trace_metrics.csv`; 8/8 solved. Strong rows include level_510 (123 chains, B, avg/max `4.22/9`, support `0.889/d4`) and Arrowz_level_095 (73 chains, A, avg/max `3.85/8`, support `0.810/d4`).
- Interpretation: availability peel is better than low-coverage static skeleton extraction for original long-chain seeds. It removes free/easy shell but preserves the high-coverage dependency core that actually carries difficulty.

## Availability Peel Batch2 Next16 - 2026-06-24

- Continued availability-peel extraction on the next high-complexity reference seed batch after the first 8 original long-chain seeds. Input CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/availability_peel_next24_input.csv`; 16 assets were present in the current worktree.
- Generated 16 peel cores via `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-AvailabilityPeelSkeletonV1.ps1`; candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/availability_peel_next16_v1_min8_candidates.csv`.
- Trace result: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/availability_peel_next16_v1_min8_trace_metrics.csv`; 16/16 solved, many retained support d3-d4. Strong rows: level_414 support `0.937/d4`, level_585 `0.916/d3`, level_943 `0.901/d3`, level_759 `0.892/d4`, Arrowz_level_144 `0.802/d4`.
- Created and mounted review pack from 12 non-Drop/support d2+ rows: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_AvailabilityPeelBatch2Review12Pack.asset`, GUID `fa0b5e3d61b64548a7c99baf48dff472`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/availability_peel_batch2_review12_frozen_trace_metrics.csv`; 12/12 solved, top row level_414 remains support `0.937/d4`, avg/max `4.05/9`.
- Demo scene in `.worktrees/sgp-rhythm-lab` now points to the Batch2 review pack.

## Availability Peel Correction - True Skeleton Review2 - 2026-06-24

- User correctly rejected `AvailabilityPeelBatch2Review12` as not a true skeleton: many rows retained most/full boards (e.g. 70-130+ chains or very high coverage), so it should be called an availability-shell/core-residual pack, not a difficulty skeleton pack.
- Corrected extraction to two stages: (1) availability-shell peel removes currently free/easy chains; (2) causal-core trim starts from the trace-visible `supportClosureBestRoot` and keeps only a small causal neighborhood / high-score dependency carriers.
- Tested trim at target coverage 0.35/0.45/0.55 on Batch2. 0.35 was too skinny and mostly lost d3 support; 0.45 produced 2 true skeletons with 26/32 chains and retained supportDepth=3; 0.55 only made the same two sources fatter and did not add more d3 roots.
- Mounted corrected Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_AvailabilityPeelBatch2TrueSkeletonReview2Pack.asset`, GUID `95abf1a1f9ce4e9eaad1043ea90878b3`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/availability_peel_batch2_true_skeleton_review2_frozen_trace_metrics.csv`; 2/2 solved, chains 26/32, supportDepth=3, supportScore 0.789/0.838.
- Current rule: do not present availability peel residuals as skeletons. A true extracted skeleton must be visibly small and pass board trace with supportDepth>=3 or an explicitly accepted alternate hard motif.

## Naming Correction - Original Seed Root - 2026-06-24

- User clarified that the current extraction target should be called `original_seed_root`, not `skeleton`.
- Working definition: remove the free/easy shell from a high-coverage original seed, then trim/preserve the trace-visible causal core that carries the original seed's difficulty motif.
- Review target is not “thin visual skeleton” and not “whole original seed”; it is the root-level causal pattern latent in original seed designs.
- Future packs/reports should prefer `OriginalSeedRoot` naming; old `Skeleton` labels in existing paths are legacy/experimental wording.

## Original Seed Root Batch3 Review9 - 2026-06-24

- Continued original-seed root mining using all-reference profile, not only the previous top-complex 24. Excluded the first 24 already processed source ids and selected the next 48 available high `complexChainScore` seeds from `reference_seed_structure_profile_298.csv`.
- Ran availability-shell peel on 48 inputs; 47 residuals generated. Trace was run in chunks; 46/47 residuals were traced successfully. One very thick residual (`original_seed_root_batch3_peel_min8_37_Arrowz_level_080`, 167 chains) timed out under the 90s single-row budget and was skipped for this review.
- Strong residual root candidates were selected by: solved, non-Drop, supportClosureBestDepth>=3, supportClosureBestScore>=0.72, chains<=60, maxChoices<=10. This intentionally targets `original_seed_root`, not a super-thin visual skeleton.
- Frozen and mounted Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedRootBatch3Review9Pack.asset`, GUID `f6a5e78fd7cd49bd86d36cf01d8f7f53`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_batch3_review9_frozen_trace_metrics.csv`; 9/9 solved, all non-Drop, supportDepth d3/d4, chains 22-57.
- Best examples: level_944 root (57 chains, A, avg/max 2.79/5, support 0.903/d4), Arrowz_level_182 (49 chains, A, 4.55/9, 0.877/d3), Arrowz_level_154 (39 chains, A, 4.13/7, 0.858/d3), Arrowz_level_179 (22 chains, S, 4.0/7, antiLocal 0.588, support 0.725/d3).

## Small Canvas OpenDebt HeadSlot Probe - 2026-06-24

- User requested the HeadSlot/OpenDebt proof stay on the small canvas route. Base used for the first probe: `seeded_direct_sgp_smallcanvas_18x24_frame8_max44_smoke8_b01_c04` from `seeded_direct_sgp_smallcanvas_18x24_frame8_max44_review4_selected.csv` (18x24, coverage `0.5601852`, solved A, supportDepth 3).
- Generated ray map `smallcanvas_opendebt_b04_map_*`: 5 direct-exit rays, 2 guard slots, 15 body-only ray blocker cells, 93 head-allowed cells, 20 safe fill cells.
- Existing `Build-RayFirstBlockerFillV1.ps1` already had debt-aware batch hooks. Added opt-in `AllowDirectExitAnchors` and corrected debt semantics: direct-exit anchors with `oldHit=-1` are closed if the new C head hits an existing chain; they are `FreeHeadDebt` only when C has no first hit.
- Smoke results:
  - `smallcanvas_opendebt_cplusd_b04_direct_try2`: 23 traced candidates, 9 accepted; exposed that direct-exit closed candidates were incorrectly labeled `WrongBasinDebt`.
  - `smallcanvas_opendebt_cplusd_b04_direct_try3`: after debt fix, 22 traced candidates, 8 accepted; top accepted coverage `0.5787037`, solved A, supportDepth 3, avg/max `4.22/11`.
- Important interpretation: on this small-canvas parent, the useful first step is mostly `CLOSED_SLOT` direct-exit blocking, not true `FreeHeadDebt + D`. Forced D can break motif when C-only is already safe. Next implementation should prefer closed direct-exit blockers, and only attach D for actual `FreeHeadDebt`.
- Interrupted run `smallcanvas_opendebt_cplusd_b04_direct_try4` produced only a partial candidates CSV and no trace; do not treat it as evidence.

## Parent030 To 0.60 Small Canvas Review4 - 2026-06-24

- User requested restarting from the clean `0.30` parent instead of continuing from already filled `0.50+` samples, because overfilled parents may have lost reserved/flexible space.
- Input parent: `near_miss_filler_orientation_v1_probe3_v006` (`coverage≈0.3076`, validated support-lock parent). A one-row input CSV was written to `opendebt_parent030_single_input.csv`.
- Baseline small-canvas fill with `CanvasWidth=18`, `CanvasHeight=24`, `PreseedOuterFrame`, `OuterFrameMaxChains=8`, `MinHeadLayer=1`:
  - `MaxTotalChains=46` reached coverage `0.606-0.618` with supportDepth 3 but most high-support rows were Drop due choice pressure.
  - `MaxTotalChains=44` preserved quality better, but the best supportDepth 3 row stopped at `0.599537` (A).
  - `MaxTotalChains=45` produced 4 reviewable rows at `coverage=0.6018519-0.6134259`, all solved with supportDepth 3; 2 A-tier and 2 B-tier.
- Frozen review pack mounted in Demo: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_Parent030To060SmallCanvasReview4Pack.asset`, GUID `d638964e6b734b7ab45abb08dbd53630`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/parent030_to060_smallcanvas_review4_frozen_trace_metrics.csv`; 4/4 solved, supportDepth=3, avg/max ranges `4.84-5.22 / 10-12`.
- Current interpretation: clean 0.30 parent can be pushed to 0.60+ on an 18x24 small canvas while preserving the support-lock motif, but choice pressure is higher than earlier 0.30/0.56 samples. Next improvement should not just add chains; it should lower effective choices by replacing crude outer frame/SGP fill with constraint-field HeadSlot scoring.

## Near-Full Coverage Reframing - 2026-06-24

- User corrected the success target: `0.60` coverage is only a diagnostic waypoint, not meaningful proof for a finished level. Future production validation must target near-full boards, roughly `0.85+` as feasibility and `0.95+` as near-product coverage.
- GPT advisor agreed that near-full difficulty is an escape-flow control problem, not a space-fill problem. Codex adjustment: outlet/sink planning may be used as a boundary budget, but it must not become the primary structure; the main generator needs ray-pressure / escape-flow constrained fill.
- Smoke `nearfull_flow_baseline_from060_t090_acceptall_smoke1`: starting from `Parent030To060SmallCanvasReview4` best row, existing background fill with blocker/core-ray constraints generated no continuation candidates. This means the crude outer-frame `0.60` state is a constrained sandbox, not a good expandable parent.
- Loose smoke `nearfull_flow_baseline_from060_t090_loose_smoke2`: relaxing blocker/core-ray constraints grew only from `0.613` to `0.657`, then no continuation candidates. This supports the same conclusion: outer-frame `0.60` is not the right midpoint for full-board growth.
- Pure seeded Direct-SGP smoke `nearfull_seeded_direct_from060_t095_smoke1`: geometry can fill the same `0.60` state to coverage `0.9329-0.9444`, but 3/3 trace `solved=False/Drop`, supportDepth `0`, openers `12-13`, avg choices `10.35-12.24`, max choices `20-22`. Full coverage geometry exists; hard motif and flow stability collapse.
- Updated next target: skip treating `0.60` as success and build a near-full `flow-pressure constrained SGP` experiment from the start. Hard gates should be `solved`, motif/root persistence, and no irreversible dependency collapse; avg/max choices become curve/distribution signals, not per-step hard locks.

## Near-Full Flow-Pressure Smoke1 - 2026-06-24

- Added opt-in `RequireHeadHitsExisting` to `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SeededDirectSGPFillBaselineV1.ps1`. It requires a newly generated SGP chain head to ray-hit existing occupied structure instead of immediately drilling to the outside. This is a minimal flow-pressure constraint, not a full allocator.
- Baseline compact check: `nearfull_compact_base18x24_tracecheck` maps clean `0.30` parent to 18x24 without extra fill. Trace remains solved/A with supportDepth `3`, supportScore `0.888`, avg/max `2.54/6`, dynamic outer max `1`. The small-canvas root itself is valid.
- One-shot `RequireHeadHitsExisting` without microfill (`nearfull_flowpressure_headhit_18x24_t090_smoke1`) only reached coverage `0.50-0.519` and all rows were unsolved/supportDepth `0`. The constraint is too locking if applied as a batch generator.
- One-shot `RequireHeadHitsExisting + microfill` (`nearfull_flowpressure_headhit_micro_18x24_t090_smoke2`) reached coverage `0.900463` with low choices (`avg 2.75-3.73`, `max 5-8`) but 4/4 unsolved/supportDepth `0`. This is not a too-easy failure; it is flow/motif disconnection.
- Trace-settled tiny-gain background fill from the compact 0.30 root (`nearfull_flow_settled_bg18x24_t090_tinygain_smoke4`) preserved solved support-lock for three small steps up to coverage `0.3935`, but was too slow and timed out.
- Continuation from that accepted point with larger gain (`nearfull_flow_settled_bg18x24_t090_from0393_gain02_smoke5`) accepted one step to coverage `0.4259259`, solved/A, supportDepth `3`, avg/max `2.48/5`, dynamic outer max `1`; the next step to `0.4444444` failed 8/8 as unsolved with supportDepth `1`.
- Current hard boundary: current SGP-style fill can preserve motif from `0.30` to about `0.42` under trace-settled micro-growth, but near-full `0.85+` is not reachable by simply shrinking step size or requiring heads to hit existing chains. The next missing component is a flow allocator that decides which causal lanes can be filled/closed after `~0.43` without disconnecting the support-lock motif.

## Temporal Core Lane Diagnostic - 2026-06-24

- Added `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Compare-TemporalCoreLaneV1.ps1`, a diagnostic-only script that runs both greedy longest-chain trace and wave trace (clear all currently available chains) for selected core owners.
- Compared accepted `0.4259259` sample with failed `0.4444444` sample using core owners `4;5;19;23;25;26`.
- Result: accepted sample solves in both greedy and wave modes and all core owners become available/cleared. Failed sample is unsolved in both modes; all six core owners are never available, even when clearing all currently available chains each wave.
- Static ray graph did not explain the failure: core static edges remain effectively identical. The failure is temporal flow/release order collapse.
- Concrete break: accepted clears relay owner `24` before releasing the core chain (`...24 -> 12 -> 14 -> 23 -> 13 -> 4 -> 5...`). Failed candidates add two chains and rewrite/replace that relay flow (`24` becomes blocked by new `33/34` or equivalent), leaving the support-lock core permanently unreleased.
- Control run `nearfull_temporal_lane_noprotect_from0425_repro_r2_control` generated 12 candidates at coverage `0.4444444`; all 12 were unsolved/supportDepth=1 and all 12 had the same core-never-available failure in wave mode. This confirms the current background SGP growth after ~0.426 systematically disconnects temporal core lanes rather than merely increasing choices or outer exits.
- Current next step: flow allocator should be defined as a temporal core reachability / relay conservation gate, not a static first-hit or average-choice gate. Candidate fill must prove the core lane remains releasable before full production trace.

## Temporal Core Wave Gate Smoke - 2026-06-24

- Extended `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-ValidatedRootBackgroundSGPFillV1.ps1` with opt-in `-RequireTemporalCoreWaveReachable -TemporalCoreOwners`.
- The gate runs inside candidate generation before adding each new chain: it temporarily inserts the candidate chain and runs lightweight wave simulation; if specified core owners cannot all become clearable/cleared, the candidate path is skipped before asset write/full trace.
- Smoke from accepted `0.4259259` parent with core owners `4;5;19;23;25;26`: `nearfull_temporal_core_wavegate_from0425_smoke1` generated 24/24 full-trace solved A candidates at coverage `0.4351852`, supportDepth=3, supportScore=0.888, avg/max `2.38/5`.
- Continuing from the selected `0.4351852` candidate with the same temporal gate produced no further background fill candidates. A no-gate control from the same parent generated 12 candidates at `0.4444444`, but 12/12 were unsolved/supportDepth=1 and all six core owners were never available in both greedy and wave diagnostics.
- Current conclusion: temporal core wave gate is the right invariant and is not merely over-conservative at 0.4259, but existing background SGP candidate language runs out of core-preserving moves at about `0.435`. The next generator work must create candidate chains from temporal lane pressure fields, not just filter ordinary SGP random/bent chains.

## Temporal Feasible Generator V1 Breakthrough - 2026-06-24

- Added opt-in `-UseTemporalFeasibleGenerator` to `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-ValidatedRootBackgroundSGPFillV1.ps1`. Instead of ordinary boundary/free-zone template fill, it enumerates short candidate chains, scores them cheaply, then wave-tests only top candidates before asset write/full board trace.
- This is the first proof that the `0.435` wall is a generator-language mismatch, not a true impossibility: `nearfull_temporal_feasible_gen_v1_from0435_smoke3_tiny` advanced from `0.4351852` to `0.4467593`, solved/A, supportDepth=3.
- Continued run via `nearfull_temporal_feasible_gen_v1_from0446_to050_smoke4` and `nearfull_temporal_feasible_gen_v1_from0486_to050_smoke5` advanced to coverage `0.5115741`, 40 chains, solved/A, supportScore `0.888/d3`, avg/max `2.35/5`.
- Core-lane diagnostic `nearfull_temporal_feasible_gen_v1_from0511_corelane_check` confirms the 0.5116 result is not a fake pass: all core owners `4;5;19;23;25;26` become available/cleared in both greedy and wave modes.
- New limitation: the 0.5116 sample is classified `LocalEasy`, with antiLocal `0.342` and dependencyFollowRunMax `6`. V1 solves temporal reachability past 0.5, but its scoring slides toward low-choice/local-easy compression.
- Next step: keep the temporal-feasible generator, but add hard-pressure ranking before top-K wave test: penalize local same-region/follow-run-like paths, reward cross-region/anti-local retargeting, and select for support-lock preservation plus hardness, not only core releasability.
- Pressure-debt smoke `nearfull_temporal_pressure_debt_v1_from0486_smoke1` tested a soft model where local debt chains are allowed and later candidates are biased toward pressure evidence. It still reached `0.5115741` solved with supportDepth=3, but stayed `LocalEasy` and dropped to antiLocal `0.289`, supportScore `0.708`.
- Interpretation update: the debt model is conceptually correct, but the current pressure evidence (`head hit`, `incoming ray hits`, `touched owners`, region spread, turns) is too local. "Debt repayment" must be tied to trace/delta pressure signals such as reduced follow-run, restored cross-region release, or improved anti-local ordering, not merely local geometry.

## Solution-Space Sculpting Direction - 2026-06-24

- User/GPT/Codex aligned on the next conceptual shift: near-full fill should not be modeled as judging whether each chain is good or bad. It should control how the whole solution space changes after each small fill step.
- The accepted target model is iterative trace-delta control: generate a wave-safe candidate batch, evaluate each candidate by how it changes core stability, path diversity, linearization risk, choice wave, follow-run, and anti-locality, then apply only 1-3 chains before recomputing the field.
- Chain labels such as `pressure / neutral / debt` remain useful as decision shorthand, but they are not the objective. The objective is to prevent the evolving board from collapsing into a single local/linear solve path while still growing coverage.
- A debt chain can be accepted when geometry leaves no harder local option, but debt must be repaid by later candidates that improve trace-level pressure. Local geometry evidence alone is not sufficient to prove repayment.
- Proposed V1 controller fields: `coreStabilityDelta`, `linearizationRisk`, `pathDiversityDelta`. Hard reject if core release breaks; conditional accept if linearization worsens; prioritize candidates that preserve/recover path diversity and support-lock pressure.

## Correction - OriginalSeedRootBatch3Review9 Demoted - 2026-06-24

- User reviewed the mounted `OriginalSeedRootBatch3Review9` sample and correctly rejected it as a root: the sample is a dense residual board with many unrelated chains, not a reusable root-level causal structure.
- Correction: `supportClosureBestDepth>=3` is only a root signal / hard motif indicator, not sufficient to define a root. A dense residual can contain a d3 support closure while still being a partial/full level body.
- `SGPRhythmLab_OriginalSeedRootBatch3Review9Pack` should be treated as `original_seed_root_signal_carrier` / residual candidate pack, not a true original-seed root pack.
- Next extraction must use role-graph minimality: identify explicit role chains and blocker edges, remove non-role filler, then validate the minimized role template. Root must be a reusable causal topology, not just a metric-qualified residual board.

## Original Seed RoleGraph Root Proof V1 - 2026-06-24

- User rejected dense residual boards as roots; implemented a first true role-graph extraction sample from `level_944`.
- Source: `OriginalSeedRootBatch3Review9` row `orig_seed_root_b3_01...level_944`, whose trace reported `supportClosureQualifiedRoots = 49:d4:b2:n8:s0.903:nodes[0;6;21;23;27;40;50;53]`.
- Extracted minimal role graph by keeping closure root `49` plus nodes `[0,6,21,23,27,40,50,53]`, 9 chains total, coverage `0.0661376`.
- This minimal graph remains trace-valid: frozen trace solved=True, processTier=S, avg/max choices `1.67/3`, openers=1, supportClosureBestScore `0.872`, supportDepth=4, antiLocal=1, localPatchRun=1, dependencyFollowRun=2.
- Frozen and mounted Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedRoleGraphRootProofV1Review1Pack.asset`, GUID `7a9f31a602d64fd9ad535c8a878a45ac`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_root_proof_v1_review1_frozen_trace_metrics.csv`.
- New extraction rule validated: original-seed root should be extracted from trace closure role nodes, not selected by dense residual metrics alone.

## Original Seed Root Level944 0.30 Carrier Expansion - 2026-06-24

- User noted the 9-chain role graph nucleus was too small compared with prior 0.25-0.30 root proofs. Corrected naming: 9-chain graph is `root nucleus`; reviewable root should add necessary carrier/guard/support shell while preserving the causal motif.
- Naive causal-score expansion to 0.22/0.26/0.30 failed: coverage reached `0.248-0.304`, but support closure collapsed to d2/LocalEasy. Conclusion: ordinary expansion changes root identity and is not acceptable.
- Implemented a carrier-search proof around fixed level_944 nucleus `[0,6,21,23,27,40,49,50,53]`. Generated 101 variants by keeping the nucleus fixed and adding carrier chains from the original residual; traced all variants in chunks.
- Selected best 0.30 root: `orig_seed_rolegraph_level944_carrier_v1_076_t30`, coverage `0.3024691`, 35 chains. Frozen trace: solved=True, processTier=A, avg/max choices `4.03/7`, support `0.976/d4`, antiLocal `0.667`, localPatchRun=2, dependencyFollowRun=2, hardStructureV3Class=TrueHardCandidate.
- Frozen and mounted Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedRoleGraphLevel944Root030Review1Pack.asset`, GUID `a3cd8a75d6cb4fd591cf8d9828e0cb5d`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_level944_root030_review1_frozen_trace_metrics.csv`.
- Validated extraction pattern: original seed root = minimal nucleus + trace-tested carrier shell; do not expand by static score alone.

## Original Seed RoleGraph Next5 Review - 2026-06-24

- Continued original-seed role-graph root extraction after the accepted level_944 root030 proof. Generalized carrier-search script generated 242 variants from six source residuals; five source families produced trace-qualified d3/d4 motifs, while level_426 produced no d3+ qualified candidate in this run.
- Mounted Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedRoleGraphNext5ReviewPack.asset`, GUID `9e2e67f9fb8649cf9d2d481ab8fda5c7`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_next5_review_frozen_trace_metrics.csv`; 5/5 solved, all A/S. Rows: level_792 30 chains cov 0.300 support d3 true-hard; Arrowz_level_182 23 chains cov 0.321 d3 medium; Arrowz_level_232 25 chains cov 0.315 d4 medium; Arrowz_level_154 11 chains cov 0.291 d4 true-hard; Arrowz_level_264 8 chains cov 0.339 d3 true-hard.
- Interpretation: this is a visual/root-boundary review pack, not final production. The last two are long-chain sparse nuclei with carrier shell and may be too thin by user visual standard; the first three are closer to 0.30 reviewable roots.

## Root Library Baseline Packs - 2026-06-24

- User requested building a reusable root library and preserving the initial generated roots separately.
- Frozen initial generated root family baseline pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootLibraryInitialFamiliesV1Pack.asset`, GUID `7c4cf2078e27463ab3c640d82bdd67fa`. It contains 12 roots: 2 each from support_lock, strict_dual, web_crossover, hub_spoke, cascade_relay, split_key. Frozen trace: `root_library_initial_families_v1_frozen_trace_metrics.csv`; 12/12 solved.
- Added original-seed rolegraph catalog with new review classes: `RootReviewCandidate`, `ThinRoot`, `NucleusOnly`. Rule currently: chains < 13 => NucleusOnly; 13-19 or low coverage/short wave => ThinRoot; 20+ chains and coverage >= 0.25 with adequate wave/motif => RootReviewCandidate.
- Frozen clean original-seed root library pack excluding nucleus-only rows: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootLibraryOriginalSeedRoleGraphV1Pack.asset`, GUID `f142bc1c67394ea5a9fc166f513a0a68`. It contains 4 RootReviewCandidate rows: level_944, level_792, Arrowz_level_182, Arrowz_level_232. Frozen trace: `root_library_original_seed_rolegraph_v1_frozen_trace_metrics.csv`; 4/4 solved A.
- Current interpretation: original seeds can still yield roots, but only through trace nucleus + carrier search + rootReviewClass gate. 8/11 chain hard motifs are nuclei, not roots, and must not enter root review/production root library.

## Original Seed RoleGraph Batch4 Review5 - 2026-06-24

- Continued original-seed rolegraph root extraction after establishing `NucleusOnly/ThinRoot/RootReviewCandidate` gate. Source batch came from `original_seed_root_batch3_peel_min8_trace_merged_metrics.csv`, excluding already-used source ids.
- Batch4 carrier search generated 628 variants from 6 usable source families. Full trace was too heavy, so candidates were prefiltered to 108 by coverage `0.25-0.34`, chains `>=20`, and closeness to `0.30`; then traced by source. Heavy sources level_980 and level_730 needed top-4 sampling.
- Mounted Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedRoleGraphBatch4Review5Pack.asset`, GUID `4a70f5e8e2b04ad2b4e43c4ff0786a91`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_batch4_review5_frozen_trace_metrics.csv`; 5/5 solved. Rows: Arrowz_level_055 20 chains S true-hard avg/max 3.5/8; Arrowz_level_120 34 chains S hard-potential 3.91/8; level_699 29 chains S true-hard 4.31/7; level_730 56 chains B medium 5.75/11; level_810 35 chains A true-hard 5.66/10.
- Catalog updated: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_library_original_seed_rolegraph_v1_catalog.csv` now has 11 rows total, 9 `RootReviewCandidate`. Batch4 is a review pack: level_730/level_810 may be visually useful but have high choice pressure, so user review should decide final library admission.

## Original Seed Strict Role Root V1 - 2026-06-24

- User corrected the root definition again: carrier-search roots must not include arbitrary chains just to reach coverage 0.30. A root chain must have a role: nucleus, closure blocker/target, support carrier, hub guard/choke, shared guard, or other trace-visible dependency role. Cosmetic/filler chains are not root chains.
- Added strict extractor: `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-OriginalSeedStrictRoleRootV1.ps1`. It outputs nucleus/direct/strict variants from original seed residuals without coverage target; strict variants are based on static ray dependency role labels around the trace nucleus.
- Source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_v1_sources.csv`, 15 d3+ source residuals from Batch3 merged trace.
- Generated 39 strict candidates; trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_v1_trace_metrics.csv`, 39/39 traced.
- Mounted Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedStrictRoleRootV1Review8Pack.asset`, GUID `2256b922b4464fe7b27c9e3a572b2796`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_v1_review8_frozen_trace_metrics.csv`; 8/8 solved. Rows include level_792 (24 chains, A, HardPotential, d3), level_944 (31 chains, B, Medium, d4), level_699 (36 chains, A, Medium, d4), level_980 (26 chains, A, Medium, d4), plus four LocalEasy strict roots from Arrowz_level_182, level_730, level_810, level_426.
- Key interpretation: strict root extraction proves previous 0.30 carrier roots included non-role filler. Strict roots may have coverage below 0.30 and must be reviewed as role structures; next step is user visual review to decide which strict roots become variant parents.

## Original Seed Strict Role Root Batch5 V1 - 2026-06-24

- Continued strict role-root extraction on additional unprocessed original-seed sources from `availability_peel_next16_v1_min8_trace_metrics.csv`, `original_seed_long_chain_availability_peel_v1_min8_trace_metrics.csv`, and related full/difficulty-skeleton traces. Selected 14 new d3+ source residuals not present in the first strict source batch.
- Ran `Build-OriginalSeedStrictRoleRootV1.ps1` with no coverage target. Generated 33 candidates (nucleus/direct/strict); trace file: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_batch5_v1_trace_metrics.csv`.
- Mounted Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedStrictRoleRootBatch5V1Review8Pack.asset`, GUID `e8b42fb7fb8946c4a21d4e8372d05fbb`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_batch5_v1_review8_frozen_trace_metrics.csv`; 8/8 solved. Source families: Arrowz_level_074, level_585, Arrowz_level_123, Arrowz_level_070, Arrowz_level_095, Arrowz_level_144, level_414, level_510.
- Strict role catalog: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_library_original_seed_strict_role_v1_catalog.csv` now has 16 rows total across first strict review8 and Batch5 review8.
- Interpretation: strict role extraction continues to produce visually plausible roots without fixed coverage. Batch5 roots are mostly MediumStructure, with some LocalEasy labels caused by local/follow runs; user visual review should decide which enter root variant parent set.

## Original Seed Strict Role Root Full V1 Review21 - 2026-06-24

- Ran full strict role-root extraction across 25 currently available original-seed d3+ source residual families, excluding prior frozen review traces to avoid duplicated/truncated IDs.
- Source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_full_sources.csv`.
- Candidate/trace outputs: `original_seed_strict_role_root_full_v1_candidates.csv` (72 candidates = 24 nucleus, 24 direct, 24 strict) and `original_seed_strict_role_root_full_v1_trace_metrics.csv` (72/72 traced, missing/failed=0).
- Catalog: `original_seed_strict_role_root_full_v1_catalog.csv`; classes are 21 `StrictRootReview`, 21 `StrictThinRoot`, 30 `NucleusOnly`. `admitted=True` for 18 of the StrictRootReview rows; 3 edge rows are intentionally kept for human review.
- Frozen and mounted Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedStrictRoleRootFullV1Review21Pack.asset`, GUID `3f5cf33e5b6e4b2d8e44b5f2a38aa729`.
- Frozen manifest: `SGPRhythmLab_OriginalSeedStrictRoleRootFullV1Review21Pack.csv`; frozen trace: `original_seed_strict_role_root_full_v1_review21_frozen_trace_metrics.csv`; 21/21 traced, 21/21 solved, process tiers S=3/A=14/B=4.
- Interpretation: this is a broad review pool, not final root library admission. User should filter duplicates, thin/easy roots, and LocalEasy/edge cases after visual review; detailed source/coverage/admitted fields live in `original_seed_strict_role_root_full_v1_review21_selected.csv`.

## Original Seed Root Extractability Correction - 2026-06-24

- User reviewed `OriginalSeedStrictRoleRootFullV1Review21` and judged many samples too forced compared with the internally designed/generated root standard.
- Decision: keep this full review pack as evidence/reference, but do not treat it as a final root library or production source.
- Next workflow should first classify original seeds by `root extractability` (whether a source can genuinely yield a reusable root), then run strict root extraction only on sources that pass this source-level gate.
- Root extraction target shifts from “extract something role-labeled from every d3+ source” to “extract roots only from sources whose role structure visually and causally resembles our accepted root standard.”
- Source-level gate should reject dense residual leftovers, overly thin nuclei, LocalEasy-only motifs, and samples where strict role chains are just scattered fragments rather than a coherent causal root.

## Original Seed Root Extractability Gate V1 - 2026-06-24

- Implemented the first source-level root extractability screen over the existing 21 full-run `StrictRootReview` rows, instead of treating all d3+ strict extracts as valid roots.
- Screen output: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_extractability_gate_v1_screen.csv`. Tiers: 6 `RootExtractableA`, 5 `RootExtractableB` rows, 6 `ReferenceOnly`, 4 `Reject`; after one-per-source selection, A/B review set has 9 roots.
- Selected A/B review input: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_extractable_v1_review_selected.csv`.
- Frozen and mounted Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedRootExtractableV1Review9Pack.asset`, GUID `c1e9a01e09d34a78b8bf0670e958c7f1`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_extractable_v1_review9_frozen_trace_metrics.csv`; 9/9 solved, S=1/A=8, HardPotential=1/MediumStructure=8, no LocalEasy rows.
- Interpretation: this replaces the broad Review21 pack as the current default visual review target. Review21 is retained as evidence; ExtractableV1Review9 is the first candidate root-library admission pool.

## Dynamic RoleMap Small Canvas Fill Probe - 2026-06-24

- Validated on the 18x24 small-canvas parent pack `parent030_to060_smallcanvas18x24_frame8_max45_review4_selected.csv` (starting coverage 0.613, solved=True, supportDepth=3).
- Static roomwise RoleMap fill is insufficient: using local or global RoleMap to fill many chains at once reached ~0.83-0.84 coverage but became solved=False and supportDepth=0. A single center-room batch was already enough to break the core.
- Root cause found: `CriticalTimingZone` must not be treated as “must-fill blocker” by default. A one-chain candidate whose body crossed three CriticalTimingZone cells immediately broke solved/core release.
- Added experimental controls to `Build-LocalRoomSGPFillProbeV1.ps1`: `-GlobalRoleMapCellsCsv`, `-UseGlobalRoleMap`, `-GlobalCriticalTimingRole {MustBlock|BodyOnly|Forbidden}`, and `-RoomwiseMaxAcceptedChains`.
- With `-GlobalCriticalTimingRole Forbidden` and dynamic one-chain steps, progression worked: step1 had 4/8 solved candidates; step2 had 3/8 solved candidates and some improved supportDepth from 3 to 4; step3 had 5/8 solved candidates. Coverage advanced from 0.613 -> 0.627 -> 0.637 -> up to 0.653 while preserving solved branches.
- Current conclusion: RoleMap is dynamic, not a one-shot map. Production fill should loop: recompute global ray/RoleMap after accepted insertions, lock CriticalTimingZone unless a trace-backed candidate proves it safe, generate small candidate batches, accept only trace-preserving candidates, then recompute.

## Dynamic RoleMap From 0.30 Parent Probe - 2026-06-24

- Re-ran the dynamic RoleMap chain from the original 0.30 parent `near_miss_filler_orientation_v1_probe3_v006` via `opendebt_parent030_single_input.csv`.
- Baseline: coverage `0.307598`, solved=True, supportDepth=3, avg/max `4/6`, outerExit=1. Initial RoleMap had 116 CriticalTimingZone cells and 328 HeadAllowed cells.
- Step1 with `CriticalTimingZone=Forbidden` and `RoomwiseMaxAcceptedChains=1`: 12/12 candidates solved, supportDepth=3. Future-capacity ranking favored shorter candidates v12/v08/v06 (future score 376) over higher-coverage v10/v11 (future score 364).
- Selected v12 for preserving future capacity, then step2: 6/12 candidates solved; unsolved branches prove supportDepth alone is insufficient and board trace must remain the hard gate. Solved branches had similar future capacity (367).
- Selected v08 from step2, then step3: 6/12 candidates solved; best branch v08 had coverage `0.314951`, supportDepth=4, avg/max `3.32/7`, localRun=3, followRun=4. Higher-coverage solved branches reached `0.317402` but avg/max rose to about `5.05/8`.
- Current interpretation: the dynamic chain works from the 0.30 parent but current room ordering always fills the same center window `(8,12)` and advances coverage slowly. Next automation should choose rooms by constrained-region urgency/future-capacity, not a fixed center-first room queue.

## Incremental RoleMap Fill Compiler V1 - 2026-06-24

- Added/validated `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-IncrementalRoleMapFillCompilerV1.ps1` as the first automated loop for dynamic RoleMap fill: generate SGP-style room candidates, cheap risk filter, trace a bounded TopK, commit one chain, then rebuild RoleMap.
- Important fix: added `-MaxTraceBatches`; the compiler now traces multiple sorted TopK batches before declaring `no_trace_accept`. This avoids false stops when the first low-risk batch contains only unsolved or too-linear candidates.
- Validation chain from the original 0.30 parent: baseline `0.307598`, earlier manual/automated steps reached `0.317402`; with the compiler and one-commit checkpoints it advanced to `0.3504902`.
- Final retrace for `incremental_rolemap_parent030_to035_onecommit_f_final_selected.csv`: solved=True, processTier=A, supportDepth=4, avg/max `3.63/7`, localRun=3, followRun=5, antiLocal=0.689, outerExitHeadCount=1. Commit table records coverage `0.3504902`.
- Key observation: difficulty is not monotonic per inserted chain. One step drifted looser around avg/max `4.30/8`, later commits recovered pressure to `3.08/6` and then stabilized near `3.6/7`. Do not hard-reject every local simplification if full trace and later pressure recovery remain possible.
- Current status: direction validated to 0.35, not final production. It still needs speed improvements, better room scheduling, larger-step packing, and proof beyond 0.35 toward 0.6/0.9.
- V2 scheduling smoke: added opt-in `-UseRegionAwareSchedule`, `-RegionScheduleMode`, `-TraceAllBatchesBeforeCommit`, and `-CommitSelectionMode PressureFirst/Balanced`. Region-aware scheduling ranks rooms by Core/Mid/Outer role, available capacity, pressure slots, critical timing risk, coverage, and recent-room penalty.
- Region-aware comparison from the 0.35049 state: simple first-accept scheduling committed to `0.3529412` but loosened to avg/max `4.21/8`; `TraceAllBatchesBeforeCommit + PressureFirst` traced 3 TopK batches and selected `0.3553922`, solved/A, supportDepth=4, avg/max `3.63/7`, localRun=3, followRun=5, antiLocal=0.696. This confirms region scheduling plus pressure-aware commit selection is a better V2 direction.
- High-coverage push from the original 0.30 parent now reaches `0.4203431` while preserving solved=True and supportDepth=4. Important chain of outputs: `incremental_rolemap_parent030_to038_pressure_recover1_final_selected.csv` reached `0.3811275`; `incremental_rolemap_parent030_to039_wide1_final_selected.csv` reached `0.3921569`; `incremental_rolemap_parent030_to405_wide1_final_selected.csv` reached `0.4080882`; `incremental_rolemap_parent030_to425_wide1_final_selected.csv` reached `0.4203431`.
- Latest high-coverage metrics at `0.4203431`: solved=True, avg/max `4.10/9`, supportDepth=4, localRun=2, followRun=4, outerExitHeadCount=2. This proves the V1 line can climb past 0.4 without immediate difficulty collapse.
- Current bottleneck: after ~0.414 coverage, valid candidate space becomes sparse. A 12-window pass found no accept after `0.4142157`; a 24-window pass found one accept to `0.4178922`; another 24-window single-commit pass found one accept to `0.4203431`. The next problem is candidate scarcity/search cost and macro region scheduling, not first-order feasibility.

## All Seed Strict Role Root Scan V2 Review13 - 2026-06-24

- User requested a full pass over all seeds after the first 25-source strict review proved too narrow and partly forced. Actual seed pool count is 951 `.asset` files under `Assets/ArrowMagic/SOData/Levels/Seeds`: 780 root seeds and 171 under `Seeds/R2FinalCandidatePool`.
- Full scan funnel: 951 profile rows -> 798 deep-scan source candidates after cheap prefilter -> 404 trace-eligible availability-peel sources -> 106 sources with supportClosureBestDepth >= 3 -> 267 strict role-root candidates -> 57 `StrictRootReview` rows -> 15 `RootExtractableA/B` rows -> 13 one-per-source selected review roots.
- Important boundary: 392 availability-peel sources were classified as `TooDenseFullBody` and not burned through full trace in this pass. They are not lost; they need a different dense-seed extractor if we want to mine them later. This pass is a full seed screening pass, not a promise that every dense full-body seed has been fully extracted.
- Key outputs: prefilter `all_seed_root_source_prefilter_v1_screen.csv`, trace eligibility `all_seed_root_source_trace_eligibility_v1_screen.csv`, eligible trace `all_seed_root_source_trace_eligible_v1_trace_metrics.csv`, strict candidate trace `all_seed_strict_role_root_v1_shortid_trace_metrics.csv`, catalog `all_seed_strict_role_root_v1_catalog.csv`, extractability screen `all_seed_root_extractability_gate_v2_screen.csv`, selected review input `all_seed_root_extractable_v2_review_selected.csv`.
- Frozen and mounted Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_AllSeedRootExtractableV2Review13Pack.asset`, GUID `8db16fca66f9417f990df524607548b3`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_root_extractable_v2_review13_frozen_trace_metrics.csv`; 13/13 solved, tiers S=3/A=9/B=1, classes HardPotential=1 and MediumStructure=12.
- Interpretation: `AllSeedRootExtractableV2Review13` is now the broadest current original-seed root review pack. It should still be visually filtered for duplicate variants, especially source-family pairs such as r1_ab_089 and r1_ab_156 variants.

## Root Canvas Variant V1B Review16 - 2026-06-24

- User asked to start producing variants from our own generated roots and canvas variants. Implemented `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-RootCanvasVariantsV1.ps1` as a first safe variant line: it reads frozen root assets, applies continuity-preserving geometry/canvas transforms, writes new LevelDefinition assets, and emits a candidate CSV.
- Source roots for V1B: first confirmed self-produced roots from `support_lock`, `strict_dual`, `web_crossover`, and `hub_spoke` in `RootLibraryInitialFamiliesV1`.
- Variant presets used: `mirrorx`, `rot180`, `wide28_mirrorx`, `tall38_mirrory`. This intentionally preserves causal chain identity while changing orientation/canvas, rather than inventing new dependencies.
- Candidate output: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_canvas_variant_v1b_candidates.csv`; selected review input: `root_canvas_variant_v1b_review16_selected.csv`.
- Frozen and mounted Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootCanvasVariantV1BReview16Pack.asset`, GUID `dc94d42f0ca34ecfb9d265855f14f4b7`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_canvas_variant_v1b_review16_frozen_trace_metrics.csv`; 16/16 solved, tiers S=1/A=15, classes TrueHardCandidate=6, HardPotential=6, MediumStructure=4.
- Signature diagnostics: `root_canvas_variant_v1b_review16_signature_summary.md` reports 5 rootSkeleton signatures; `root_canvas_variant_v1b_review16_backbone_summary.md` reports 5 causal backbone roots and 7 backbone variants. Interpretation: the variant line preserves several distinct root identities instead of collapsing everything to one root, but support_lock/hub_spoke still partially merge with the common convergent support-lock backbone.
- Next step: user visual review of Review16, then expand from safe transforms into role-anchor remap variants and eventually canvas-specific SGP refill around preserved root specs.

## Root Variant Mixed V1 Review16 - 2026-06-24

- User clarified that canvas shrink/embedding variants are still worth keeping, but they also need real variants because pure scale/canvas moves preserve chain distribution and feel too similar.
- Added `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-RootSpatialRecomposeVariantsV1.ps1` as an attempted chain-level spatial recompose. Result: 14/14 trace Drop, proving independent per-chain relocation destroys ray-causal dependency and should not be the default variant method.
- Added `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-RootPeripheralJitterVariantsV1.ps1`. It locks trace core chains from supportClosure/strictDual fields and only jitters non-core peripheral chains. Hard jitter (move 6 chains) produced 4/24 solved; soft jitter (move 3 chains) produced 17/32 solved A.
- Frozen and mounted Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantMixedV1Review16Pack.asset`, GUID `e09071087b31411381ecea8cb88168d9`.
- Pack composition: 4 self-produced root families (`support_lock`, `strict_dual`, `web_crossover`, `hub_spoke`) x 4 rows each. Each family has 2 canvas_embedding variants and 2 peripheral_jitter_soft variants.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_mixed_v1_review16_frozen_trace_metrics.csv`; 16/16 solved, all A, classes TrueHardCandidate=6, HardPotential=8, MediumStructure=2.
- Signature diagnostics: `root_variant_mixed_v1_review16_signature_summary.md` reports 15 core signatures, 13 skeleton family signatures, 5 rootSkeleton signatures; `root_variant_mixed_v1_review16_backbone_summary.md` reports 6 causal backbone roots and 12 backbone variants.
- Interpretation: V1 production line now has two viable variant modes: low-risk canvas embedding and trace-core-preserving peripheral jitter. The next variant step should not move chains independently; it should either jitter only non-core chains, or move causal clusters as units with trace gate.

## SGP Constraint Adapter V1 Smoke - 2026-06-24

- User/GPT convergence: keep Direct SGP as the coverage/packing engine, but inject a stateful constraint adapter rather than rewriting SGP or building a second planner.
- Implemented experimental adapter hooks in `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SeededDirectSGPFillBaselineV1.ps1`: optional RoleMap-backed head filter, body walk role bias, critical-timing body rejection/penalty, outer-exit budget, and mop-up short-chain budget. Defaults preserve old behavior.
- From an existing 0.30 rootlib parent, `TargetCoverage=0.60 / MaxNewChains=90` with adapter still filled to `0.536-0.580` but 8/8 were unsolved with `supportDepth=0`; conclusion: one-shot large SGP fill still rewrites the temporal/core release graph.
- Small batch smoke `MaxNewChains=5 / AdapterOuterExitBudget=2` produced 16 candidates, 1 solved A candidate at coverage `0.3676471`, `avg/max=3.64/7`, `supportDepth=3`, `localRun=2`, `followRun=5`, `outerExitHeadCount=3`. This proves adapter-guided SGP can add multiple chains while preserving the motif, but hit rate is low.
- Strict outer budget smoke `AdapterOuterExitBudget=0` produced no solved candidates; this confirms dynamic outer pressure cannot be hard-zeroed and should remain a controlled budget/gate.
- Current conclusion: the next viable route is not full-board one-shot SGP and not single-chain only; it is small-batch SGP + adapter + trace commit + RoleMap recompute, with dynamic outer budget and better selection/ranking.

## SGP Constraint Frame Layer V1 Smoke - 2026-06-24

- Added experimental `-UseConstraintFrameLayer` to the seeded SGP adapter: SafeFill/BodyOnly cells are no longer allowed to become heads, high-risk/direct outer heads consume both global and per-side outer budget, and RoleMap still guides body scoring. This is a coarse frame layer, not a full global solver.
- Step1 from the existing 0.30 rootlib parent with `MaxNewChains=5`, `AdapterOuterExitBudget=2`, `AdapterOuterExitBudgetPerSide=1`: 24 traced candidates, 1 solved A candidate at coverage `0.3676471`, `avg/max=3.64/7`, `supportDepth=3`, `localRun=2`, `followRun=5`, `outerExitHeadCount=3`.
- Step2 from that candidate: 24 traced candidates, 3 solved A candidates. Best pressure-preserving pick `sgp_frame_layer_v1_step2_batch5_smoke_b01_c19` reached coverage `0.3909314`, `avg/max=3.96/9`, `supportDepth=4`, `localRun=2`, `followRun=4`, `outerExitHeadCount=5`.
- Step3 candidates geometrically reached `0.411-0.431` coverage, but the traced top candidate was only solved Drop with `avg/max=4.87/11`, `supportDepth=3`, `outerExitHeadCount=7`. This shows the current frame layer can continue the chain but does not yet control outer-exit proliferation or choice curve near 0.42+.
- Current conclusion: coarse frame is useful but too weak. The next frame layer needs explicit planned outer-exit shell/pressure corridors rather than deriving all frame constraints from the current RoleMap head roles.

## SGP Pressure Gradient Layer V2 Smoke - 2026-06-24

- GPT review corrected the previous “outer shell first” interpretation: next cut should be a dynamic pressure-gradient layer between core and shell, not a fixed prebuilt outer shell.
- Added experimental `-UsePressureGradientLayer` to the seeded SGP adapter: per-region head budget and per-region outer-exit budget are tracked in addition to global/per-side outer budgets. This tries to prevent SGP from concentrating new exits in one or two regions.
- Step1 from the existing 0.30 rootlib parent with `MaxNewChains=5`, `AdapterOuterExitBudget=2`, `AdapterOuterExitBudgetPerSide=1`, `AdapterOuterExitBudgetPerRegion=1`, `AdapterHeadBudgetPerRegion=2`: 24 traced candidates, 1 solved A candidate at coverage `0.3504902`, `avg/max=3.19/7`, `supportDepth=4`, `localRun=3`, `followRun=5`, `outerExitHeadCount=3`.
- Step2 from that candidate: 16 traced candidates, 1 solved A candidate at coverage `0.3762255`, `avg/max=4.43/9`, `supportDepth=4`, `localRun=2`, `followRun=5`, `outerExitHeadCount=4`.
- Current interpretation: region pressure gradient improves stability/keeps support depth but is conservative and does not yet solve high-coverage speed. It should be integrated into the incremental compiler as a selection/ranking signal, but the route still needs larger macro regions or planned pressure corridors to climb efficiently past ~0.42.

## SGP Incremental Compiler Cavity Recovery Proof - 2026-06-24

- Added experimental knobs to `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-IncrementalRoleMapFillCompilerV1.ps1`: `RoomwiseMaxAcceptedChains`, `TraceCavitySplitRecovery`, and `CavitySplitRecoveryLimit`. Defaults preserve old single-chain / no-recovery behavior.
- GPT review and local data agreed that `cavitySplit` is not a hard reject after about 0.35 coverage; it should be treated as a recoverable soft-risk candidate class and traced in a small recovery beam.
- One-chain pressure-gradient compiler was safe but too slow: `0.307598 -> 0.3210784` in 4 commits, all solved/A-like with supportDepth 3, but each commit only added a tiny chain.
- Allowing 2 accepted room chains sped progress: `0.307598 -> 0.3296569` in 2 commits while keeping solved/A, supportDepth 4, maxChoices 7, outerExitHeadCount 1.
- Cavity recovery + deeper trace beam then produced a stable chain of accepted states:
  - `0.3296569 -> 0.3566176`, supportDepth 4, maxChoices 7, outerExitHeadCount 1.
  - `0.3566176 -> 0.3664216`, supportDepth 4, maxChoices 7, outerExitHeadCount 1.
  - A too-narrow topK path reached `0.3872549` but with maxChoices 9 and outerExitHeadCount 4, so it was not the preferred route.
  - Deep tracing all 15 candidates at the same transition found a healthier route: `0.375 -> 0.3823529`, supportDepth 4, avg/max `3.81/7`, outerExitHeadCount 3.
  - Continued deep beam reached `0.3909314`, then `0.3982843`, both solved/A with supportDepth 4, maxChoices 7, localRun 3, followRun 4, outerExitHeadCount 3.
- Current conclusion: the promising route is no longer tightening cheap filters. It is `RoleMap + pressure gradient + cavitySplit soft recovery + small/deeper beam + commit/recompute`. The next bottleneck is cost and beam scheduling, not first-order feasibility.
- Continued from the `0.3982843` parent:
  - Two-chain candidate from `0.3982843` can jump to `0.4080882`, solved/A, supportDepth 4, avg/max `4.76/8`, outerExitHeadCount 4; this is valid but yellow because choices and outer pressure rise.
  - Single-chain deep beam found a healthier path over the 0.40 line: `0.3982843 -> 0.4007353`, supportDepth 4, avg/max `4.19/7`, outerExitHeadCount 3.
  - From that healthier state, two-chain candidates remained viable to `0.4105392`, supportDepth 4, avg/max `4.15/7`, outerExitHeadCount 3, then `0.4203431`, supportDepth 4, avg/max `4.27/8`, outerExitHeadCount 3.
  - After `0.4203431`, two-chain candidates all failed; single-chain deep beam still advanced to `0.4227941` and `0.4264706`, both solved/A with supportDepth 4, maxChoices 8, outerExitHeadCount 3.
  - From `0.4264706`, a two-chain deep attempt traced 15 candidates and all dropped. Current boundary for this parent/compiler is therefore not an absolute 0.42 wall, but a mode switch: above ~0.42 only small single-chain recovery steps are currently safe, and large local batches break the core release graph.
- Current next engineering cut: add a high-density scheduler that automatically switches `RoomwiseMaxAcceptedChains` from 2 to 1 when solved-rate drops / maxChoices or outer pressure rises, and reduce cost by caching RoleMap/trace candidates or tracing region-diverse beams instead of all deep candidates every round.
- Implemented adaptive high-density mode in `Build-IncrementalRoleMapFillCompilerV1.ps1`: `-UseAdaptiveHighDensityMode`, `-HighDensityCoverageThreshold`, `-HighDensityRoomwiseMaxAcceptedChains`. Smoke from `0.4007353` with threshold `0.40` confirmed the round used `roomwiseMaxAcceptedChains=1` and committed to `0.4056373`, solved/A, supportDepth 4, avg/max `4.00/8`, outerExitHeadCount 3.

## Constraint Slot Planner Probe - 2026-06-24

- User asked whether GitHub/skills could provide reusable capability; local check found no installed `ortools` or `z3` Python packages. A temporary standalone prototype was added at `.codex-run/constraint_slot_planner_probe.py` to test the idea without changing the Unity pipeline.
- Probe reads existing RoleMap cell CSV and performs static candidate-chain set packing / beam planning. It outputs `slot_plan_summary.csv`, `slot_plan_chains.csv`, and ASCII role-plan previews under `.codex-run/constraint_slot_planner_probe_*`.
- On the latest ~0.40 RoleMap (`pressure_gradient_compiler_v1_adaptive_smoke1_round01_rolemap_cells.csv`), strict RoleMap hard constraints capped the plan at `0.6838` coverage; allowing SafeFill heads reached `0.7341`; allowing CriticalTimingZone as body-only reached `0.8578` with only 1 outer head.
- The same ~0.40 RoleMap in an unconstrained control mode reached `0.9240` coverage, proving the simple chain language can near-fill, but hard RoleMap constraints are too restrictive. On the earlier ~0.30 RoleMap, the unconstrained control reached `0.8885`, while RoleMap-constrained modes stayed around `0.78-0.82`.
- Current conclusion: a constraint-solver/slot-planner layer is useful as a diagnostic/planning adapter, but it cannot replace SGP directly. The important correction is that RoleMap must become a soft risk/budget field: critical zones should usually be allowed as body cells, heads/outer exits need budgets, and final difficulty/solvability must still go through trace/SGP commit gates.

## AssetDatabase Trim for SGP Rhythm Lab - 2026-06-24

- User reported Unity stuck for a long time at database after restarting `.worktrees/sgp-rhythm-lab`. Investigation found `Assets/ArrowMagic/SOData` had roughly 84k imported files, including 28k `.asset`, 42k `.meta`, 9.6k `.csv`, and 3.2k `.md`, mostly historical SGPRhythmLab experiment outputs.
- Performed a reversible hot/cold asset split: kept current review assets in `Assets`, moved historical experiment assets/reports/packs outside `Assets` to `.worktrees/sgp-rhythm-lab/_AssetArchive/20260624_assetdatabase_trim` so Unity no longer imports them.
- Current hot assets kept in `Assets`: active `RootVariantMixedV1Review16`, `RootCanvasVariantV1BReview16`, `RootPeripheralJitterV1Soft`, `RootLibraryInitialFamiliesV1`, plus pack assets for current mixed variant, canvas variant, initial families, and all-seed extractable review13.
- After trim, `SOData` dropped to about 7.9k imported files: 3.7k `.asset`, 4.0k `.meta`, 131 `.csv`, 24 `.md`, etc. Reports/SGPRhythmLab dropped to about 99 files. Archive currently contains about 76.9k files.
- Active Demo pack remains `SGPRhythmLab_RootVariantMixedV1Review16Pack.asset` (GUID `e09071087b31411381ecea8cb88168d9`), and the pack/level directory still exists. Unity project process `82904` restarted and no sgp-rhythm-lab AssetImportWorker was observed after startup.
- Restore note: archived files were moved, not deleted. Normal path mirror is under `_AssetArchive/20260624_assetdatabase_trim/Assets/...`; reports that hit Windows path-length limits were short-name archived under `_AssetArchive/20260624_assetdatabase_trim/_short_reports/SGPRhythmLab` with `reports_short_manifest.csv`.

## Root Experience Variant V1 Review15 - 2026-06-24

- User clarified that mirror/rotation/full-pack transform variants are not meaningful production variants because the puzzle experience remains the same. Canvas embedding can be retained as a weak/debug variant, but official review needs experiential differences.
- Built `RootExperienceVariantV1Review15` from `root_peripheral_jitter_v1_soft_review_selected.csv` only. It excludes mirror/rotation/pure canvas embedding and removes duplicate perturbation signatures from the soft jitter pool.
- Frozen and mounted Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootExperienceVariantV1Review15Pack.asset`, GUID `a41c7df3082c4e87bdd15e2aee3370d4`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_experience_variant_v1_review15_frozen_trace_metrics.csv`; 15/15 solved, all A-tier, classes TrueHardCandidate=6 / HardPotential=6 / MediumStructure=3, avgChoices range `3.02-4.14`, maxChoices `6-7`, supportDepth `3-4`.
- Signature diagnostics: `root_experience_variant_v1_review15_signature_summary.md` reports 15 core signatures, 13 skeleton family signatures, 5 rootSkeleton signatures; `root_experience_variant_v1_review15_backbone_summary.md` reports 5 causal backbone roots and 10 backbone variants.
- Interpretation: this is the current best "experience-difference variant" review pack for existing roots. It is not a new-root generator; next root-diversity work should continue on causal-backbone/root expansion separately.

## Root Visible Peripheral Jitter V1 Probe - 2026-06-24

- User reviewed `RootExperienceVariantV1Review15` and said visual/experiential difference was still not obvious. Conclusion: soft peripheral jitter is too conservative and should not be counted as a production diversity line by itself.
- Ran a stronger non-mirror/non-rotation probe using `Build-RootPeripheralJitterVariantsV1.ps1` with `MaxMovedChains=8`, `SearchRadius=6`, `VariantsPerRoot=12`. It generated 48 candidates across support_lock, strict_dual, web_crossover, and hub_spoke.
- Trace result: only 3/48 survived as solved A-tier. This confirms aggressive single-chain jitter has low survival rate because it tends to break causal dependencies.
- Mounted Demo probe pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVisiblePeripheralJitterV1Review3Pack.asset`, GUID `b8c910e29ce84f72b83905d577836553`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_visible_peripheral_jitter_v1_review3_frozen_trace_metrics.csv`; 3/3 solved A, one TrueHardCandidate and two MediumStructure.
- Interpretation: this is a probe pack for human review only. If the user still sees insufficient difference, stop single-chain jitter and move to causal cluster / role-anchor remap variants.

## AssetDatabase Trim for SGP Rhythm Lab Round2 - 2026-06-24

- User asked whether `.worktrees/sgp-rhythm-lab` still had optimization room after the first AssetDatabase trim. It did: `Assets/ArrowMagic/SOData` still had 8365 files, including untracked `ReferenceSeeds`, old SGPRhythmLab review/source levels, old packs, and old reports.
- Performed a second reversible hot/cold split to `.worktrees/sgp-rhythm-lab/_AssetArchive/20260624_assetdatabase_trim_round2/`. Manifest: `manifests/sgp_rhythm_lab_round2_hotset_trim_manifest.csv`; summary: `manifests/sgp_rhythm_lab_round2_hotset_trim_summary.md`.
- Kept hot in `Assets`: active Demo pack `SGPRhythmLab_RootVisiblePeripheralJitterV1Review3Pack.asset`, active level dir `Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVisiblePeripheralJitterV1Review3/`, and reports with prefix `root_visible_peripheral_jitter_v1_review3*`.
- Moved 268 filesystem items / 1984 files / 8.41 MB out of `Assets`. `Assets/ArrowMagic/SOData` dropped to 6446 files; `Reports/SGPRhythmLab` dropped to 179 files; `Levels/SGPRhythmLab` now contains only 7 files for the active review3 hot set.
- Verified active pack still exists, its three referenced level GUIDs remain in the active level dir, active report prefix files remain, and `ReferenceSeeds` is no longer under `Assets` but exists in the archive.
- Intentionally did not move tracked `Assets/ArrowMagic/SOData/Levels/Generated` or `Assets/ArrowMagic/SOData/Levels/Seeds`; those are production/source data and need a separate decision before any cleanup.

## AssetDatabase Trim for SGP Rhythm Lab Round3 - 2026-06-24

- Follow-up check found two more untracked Unity-imported legacy procedural level folders: `Assets/ArrowMagic/SOData/Levels/DirectProcedural` and `Assets/ArrowMagic/SOData/Levels/NoMaskProcedural`.
- Confirmed current `Demo.unity`, active `SGPRhythmLab_RootVisiblePeripheralJitterV1Review3Pack.asset`, and active review3 level dir do not reference `DirectProcedural` or `NoMaskProcedural`; `git ls-files` also reported no tracked files under those folders.
- Moved both folders and their meta files to `.worktrees/sgp-rhythm-lab/_AssetArchive/20260624_assetdatabase_trim_round3/`. Manifest: `manifests/sgp_rhythm_lab_round3_legacy_procedural_levels_manifest.csv`; summary: `manifests/sgp_rhythm_lab_round3_legacy_procedural_levels_summary.md`.
- Moved 4 filesystem items / 90 files / 0.28 MB out of `Assets`; this is small on disk but removes another pair of old experiment folders from Unity AssetDatabase import.

## AssetDatabase Trim for SGP Rhythm Lab Round4 - 2026-06-24

- Follow-up recursive count found remaining non-active report files and child directories under `Assets/ArrowMagic/SOData/Reports/SGPRhythmLab` even after round2.
- Moved all remaining report children that do not start with `root_visible_peripheral_jitter_v1_review3` to `.worktrees/sgp-rhythm-lab/_AssetArchive/20260624_assetdatabase_trim_round4/`. Manifest: `manifests/sgp_rhythm_lab_round4_reports_tail_manifest.csv`; summary: `manifests/sgp_rhythm_lab_round4_reports_tail_summary.md`.
- Moved 20 filesystem items / 175 files / 2.13 MB. `Reports/SGPRhythmLab` now has 10 files total, all belonging to the active review3 report prefix.

## Root Cluster Remap V1B Probe - 2026-06-24

- User confirmed single-chain jitter does not create meaningful experiential difference. Decision: single-chain jitter is not an official variant direction.
- Added `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-RootClusterRemapVariantsV1.ps1`. It selects a spatial cluster of non-core chains and translates the cluster as a unit while preserving trace core chains.
- First strong cluster run generated 27 candidates but only 1 solved; V1B softer run generated 46 candidates and 10 solved A-tier candidates. Survivors concentrated in hub_spoke/support_lock; strict_dual/web_crossover did not survive this cluster strategy.
- Frozen and mounted Demo probe pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootClusterRemapV1BReview6Pack.asset`, GUID `c2ce4a8cd84e4e6db1fd34d0b6e97ff0`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_cluster_remap_v1b_review6_frozen_trace_metrics.csv`; 6/6 solved A, 2 HardPotential and 4 MediumStructure, supportDepth 3-4.
- Interpretation: this is a cluster-translation visibility probe. If user still sees insufficient difference, stop translation-based variants and move to role-anchor remap / causal cluster swap instead.

## Root Role-Zone Swap V1 Probe - 2026-06-24

- User rejected both single-chain jitter and non-core cluster translation as visually/experientially indistinguishable.
- Added `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-RootRoleZoneSwapVariantsV1.ps1`. It forms two non-core role zones and swaps their positions, preserving trace core chains and using board trace as the final gate.
- First role-zone run generated 34 candidates and all solved, but most were no-op (`offsetA=0,0`, `offsetB=0,0`). This exposed an important bug: role-zone remap scripts must reject zero-distance/no-op variants before counting them.
- Two non-zero role-zone swap survivors were selected from hub_spoke, with large offsets (`16,0` and `-16,1` / `-15,1`) and swapDistance 32-33.
- Frozen and mounted Demo probe pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootRoleZoneSwapV1Review2Pack.asset`, GUID `d44b9b5752e44d71b37b8d79cfa2a619`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_role_zone_swap_v1_review2_frozen_trace_metrics.csv`; 2/2 solved, tiers S/A, both HardPotential, supportDepth 3-4, avgChoices 3.6-3.9, maxChoices 9.
- Interpretation: this is the first genuine role-zone swap probe. If user still sees insufficient difference, the conclusion is that old-root variants are not enough and root diversity must come from generating/admitting new causal roots.

## Role-Zone Swap Dedup Correction - 2026-06-24

- User reviewed `RootRoleZoneSwapV1Review2` and confirmed the two levels were effectively identical. Geometry comparison: they were not byte-identical, but came from the same hub_spoke source and same operation family; chain Jaccard was `34/46 = 0.739` with only 6 chains different each way, so the review pack was correctly rejected as duplicated.
- Corrected selection rule: do not present multiple variants from the same source/root with the same operation class; role-zone swap candidates also need geometry/operation dedup, not just non-zero offsets.
- Ran non-zero role-zone swap over all 12 initial roots. Only 8 non-zero candidates were generated; 2 solved, both hub_spoke. Cascade/split non-zero candidates dropped; support/strict/web produced no non-zero landed candidates under this search.
- Mounted a single technical proof instead of the duplicated pair: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootRoleZoneSwapV1Roots12Review1Pack.asset`, GUID `edb2b3dbca7f426089050a3d829fb356`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_role_zone_swap_v1_roots12_review1_frozen_trace_metrics.csv`; 1/1 solved, S-tier, TrueHardCandidate, supportDepth=4, avg/max choices 3.8/9.
- Conclusion: old-root mutation/variant route is not a sufficient diversity source. It can produce isolated technical proofs, but production diversity should return to generating/admitting genuinely new causal roots.

## Causal Root Family One-Each Review Pack - 2026-06-24

- User decision: stop old-root mutation as the main diversity route and move to new causal roots.
- Restored archived MixedRootFamilyReviewV1 pack assets/levels/reports from _AssetArchive/20260624_assetdatabase_trim back into .worktrees/sgp-rhythm-lab/Assets for active review.
- Created one-per-family review pack: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_CausalRootFamilyOneEachV1Pack.asset.
- Pack GUID: 4d9c124509544bebb0d177676f89a8fa; Demo scene activePack now points to this pack.
- Contents: 6 representatives: support_lock, strict_dual, web_crossover, hub_spoke, cascade_relay, split_key. These reuse frozen MixedRootFamilyReviewV1 assets and existing board-trace/classifier audits.
- Verification: all 6 level GUIDs referenced by the pack were found in Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/MixedRootFamilyReviewV1.
- Next review task: user should judge whether these six are player-facing distinct causal roots; do not count within-family pairs/variants as new root diversity.

## Campaign500 Hardening Analyzer V0 - 2026-06-24

- User requested a way to raise candidate/final pack difficulty by reducing early free outer exits and too many simultaneous choices.
- Added analysis-only editor tool `Assets/ArrowMagic/Editor/CampaignHardeningAnalyzer.cs`; it reads `Campaign500FinalPack` and `SingleLevelCandidatePoolPack`, runs Greedy/choice trace, counts direct/clearable outer exits, edge short leaks, early choice explosion, and weak dependency signals.
- Latest run output: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_final_and_candidate_pool_20260624_221821_summary.csv`, `..._leak_rank.csv`, `..._top20_plan.csv`, and `..._notes.md`.
- Generated review pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningLeakReviewPack.asset` with the top 20 leak candidates; it is not mounted to Demo yet.
- Calibrated final-pack priority bands after first too-broad run: final 500 distribution is CriticalLeak=26, HighLeak=103, MediumLeak=220, LowLeak=112, Ok=39.
- Critical candidate campaign orders: 387,425,183,490,381,343,284,382,436,494,474,449,465,488,173,366,475,405,380,500,287,459,445,452,238,456.
- Validation: `dotnet build .\ArrowLevel-Hand.sln` passed with existing warnings; Unity batch executed analyzer and wrote reports/review pack.
- Next step: build a light/heavy hardening sandbox from the top leak rows, starting with safe operators only: redirect outer heads, wrap/merge boundary straight or short outer chains, then re-run Greedy and compare choice/outer-exit deltas.

## Campaign500 Hardening GPT Review - 2026-06-24

- Consulted GPT via the user-provided ChatGPT conversation about Hardening Analyzer bands and next steps.
- GPT agreed leak bands should stay separate from game difficulty labels: they measure early entropy leak / sweepability, not player-facing difficulty.
- GPT recommended one minimal calibration before mutation sandbox: merge correlated boundary leak signals into one bounded boundary channel, reduce choice-explosion weights, and slightly tighten weak-dependency trigger.
- Do not change score bands yet; keep `Ok/Low/Medium/High/Critical` thresholds stable so the next report is comparable.
- Calibration implemented in `CampaignHardeningAnalyzer.cs`: boundary leaks now score once through a capped `BoundaryLeak` channel, choice weights are lower, and weak-dependency trigger is tighter. Batch mode now logs progress every 25 levels and per-level slow warnings.
- Latest calibrated run: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_final_and_candidate_pool_20260624_224354_summary.csv`, `..._leak_rank.csv`, `..._top20_plan.csv`, `..._notes.md`.
- Final-pack distribution changed from `Critical=26/High=103/Medium=220/Low=112/Ok=39` to `High=2/Medium=141/Low=284/Ok=73` with no Critical. This is now a sharper repair-priority queue rather than a broad difficulty label.
- Built copied light/heavy hardening sandbox V1 for selected HighLeak/top MediumLeak levels; do not mutate `Campaign500FinalPack.asset`.
- Sandbox V1 output: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV1Pack.asset`, levels under `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V1`, report `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v1_20260624_224903.csv`.
- Sandbox V1 result: 10 source levels x original/light/heavy = 30 assets; all Greedy solved. Light accepted avg `1.9` ops, score `618.8 -> 581.0`, opening choices/direct outer `28.0 -> 26.1`. Heavy accepted avg `3.4` ops, score `618.8 -> 543.5`, opening choices/direct outer `28.0 -> 24.6`. This validates safe outer-head redirect as a first operator, but not yet full gate/region hardening.
- Demo scene `Assets/ArrowMagic/Scenes/Demo.unity` was mounted to `Campaign500HardeningSandboxV1Pack.asset` for review. Levels were grouped as original/light/heavy triplets per source level.
- Next local step: add a second sandbox operator that turns remaining free exits into local dependency gates instead of only reversing exposed outer-head chains; keep per-step Greedy + entropy gates and compare against V1.

## Campaign500 Hardening Qualitative Sandbox V2 - 2026-06-24

- User reviewed V1 and correctly found the difference visually indistinguishable. V1 was only chain reversal, so it changed direction metrics but not the visible body language.
- Added V2 to `Assets/ArrowMagic/Editor/CampaignHardeningAnalyzer.cs`: `Tools/Arrow Magic/Campaign 500/Hardening/Build Qualitative Sandbox V2`.
- V2 main operator is accepted edge/direct-exit chain merge: two adjacent boundary/free-exit chains are merged into one longer chain, reducing chain count and visible arrowheads/colors. Every merge is gated by Greedy solvability, unchanged arrow tile count, reduced opening/outer/early choices or leak score, and no excessive forced-move/dependency regression.
- V2 output pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV2Pack.asset`; level folder `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V2`; report `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v2_20260624_234908.csv`.
- V2 selected 10 original/strong pairs from orders `425,387,474,183,494,405,490,343,449,382`; all strong variants are Greedy solved.
- V2 average strong result: accepted ops `9.0`, chains `167.0 -> 158.0`, opening/direct clearable outer `28.0 -> 16.6`, leak score `618.8 -> 399.5`. This is the first visually meaningful hardening sandbox, unlike V1.
- Demo scene is currently mounted to V2 pack. Review rule: odd levels are originals, even levels are the corresponding strong V2 variant.

## Gate Vocabulary V1 Light Review - 2026-06-24

- Mounted Demo activePack to `SGPRhythmLab_GateVocabularyV1LightReviewPack` (guid `0a8b5ee614ec49deb72f2bb146324904`).
- Pack path: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GateVocabularyV1LightReviewPack.asset`.
- Contains 4 shared-core dual-gate door-shape candidates: base, vertical, right-facing, downward/crossed.
- Board trace: all 4 assets import/reference correctly; levels 1,2,4 pass `strictDualGateCandidate`; level 3/right-facing is visual-only for now because strict dual candidate is rejected in trace.
- This is a gate vocabulary review pack, not a final production-hard pack; next step is to keep visually accepted door designs and re-run solvable/coverage fill.

## Gate Vocabulary V1 Solved Skeleton Growth Review - 2026-06-24

- User confirmed Gate Vocabulary V1 has visible body-feel differences and is theoretically viable.
- Local trace corrected GPT's broad diagnosis: pure strict-dual gate skeletons are already `solved=True + strictDualGateCandidate=True`; the earlier failure came from light filler breaking solvability.
- Mounted Demo activePack to `SGPRhythmLab_GateVocabularyV1SolvedSkeletonGrowthReviewPack` (guid `584221c803cf4d5fa98ac9ac479176ae`).
- Pack contains 4 solved strict-dual skeleton-only gate designs plus 3 accepted conservative growth steps from the first design.
- Current conservative growth reached 17 chains / maxChoices 6 / avgChoices about 4.2 / antiLocal about 0.91 while preserving strict-dual identity; not yet coverage 0.30.
- Next step: implement faster single-base/early-stop fill for solved gate skeletons, then push each accepted gate design toward 0.20 -> 0.25 -> 0.30.

## SGP Full-Density Control Experiments - 2026-06-24

- Goal: start from a verified ~0.30 parent/core skeleton and push toward near-full coverage while preserving solvability and hard motif.
- Verified baseline: raw seeded SGP can geometrically reach coverage ~0.88, but trace collapses (`solved=False`, avgChoices ~21-24, maxChoices ~36-43, outerExitHeadCount 32-39, supportClosureDepth 0). SGP is a packing engine that reaches density by many outward exits.
- Hard RoleMap filtering protects the structure but starves fill: coverage caps around 0.53-0.54 with adapter outer exit budget held near 2.
- Soft-budget Constraint Adapter was implemented in `Build-SeededDirectSGPFillBaselineV1.ps1` and comparison wrapper `Run-SGPBudgetAdapterComparisonV1.ps1`; it restores coverage to ~0.88 but still has 50+ adapter outer exits and traces all Drop/unsolved. Increasing over-budget penalties did not materially reduce outer exits. Conclusion: scoring/budget adapter is too late to change SGP’s high-density generation language.
- `UsePreActionMask` was added to move constraints earlier: only `GuardSlot/HeadAllowed` conditional zones may be heads; Safe/HighRisk/CriticalTiming/BodyOnly zones can still be consumed as body. This changes SGP behavior: coverage ~0.75-0.79 and adapter outer exits drop to 5-14, but trace deadlocks quickly (`avgChoices 0-1`, unsolved).
- Crude `PreseedOuterExits + UsePreActionMask` restores some choices (avg ~6-7) but lowers coverage to ~0.58-0.62 and remains unsolved, so random/short boundary exits are not a valid production scaffold.
- GPT review agreed: stop tuning adapter/preseed exits. Current failure is a missing temporal release scaffold / unlock flow backbone. Next cut should be a minimal release-path scaffold extracted from the 0.30 parent: preserve 1-2 support-lock paths, define one controlled escape/release corridor and one mid-lock bottleneck, then let SGP fill bodies inside that scaffold. Pass criteria: coverage >=0.75 with at least some solved candidates, avgChoices in single digits, maxChoices <=10-12, supportDepth >=3, and outerExitHeadCount not exploding (>20).
- Follow-up release-head probes extended `Build-SeededDirectSGPFillBaselineV1.ps1` with `PreActionMaskAllowReleaseHeads` and `PreActionMaskReleaseRegions`. Allowing CriticalTimingZone release heads reached only ~0.59-0.63 coverage, all unsolved, outerExitHeadCount=1, avgChoices ~2-3. Allowing HighRiskFreeHead only in parent direct-exit owner regions (`0;3;5;6;8`) still capped around ~0.58-0.63. Conclusion: region/head-slot whitelist is still just a mask; it does not create a valid temporal release scaffold. Next experiment must explicitly construct/freeze release paths, not only permit more head categories.
- Corrected release-scaffold diagnosis: reserving only `rayCellsBeforeHit` is insufficient because after the original blocker clears, later cells on the same escape ray can still be occupied by new SGP chains and break temporal release. `Build-SeededDirectSGPFillBaselineV1.ps1` now supports `PreActionMaskReserveFullRayOwners` and also applies adapter step rejection to the second cell of a generated chain.
- Full-ray release-lane experiments on the 0.338 parent:
  - Selected-edge reserve (`4;5;6;8;9;10;32;40`) and release-head whitelist still produced coverage ~0.57-0.60 but all Drop/LocalEasy, supportDepth=0.
  - Full-ray reserve for critical owners restored supportDepth from 0 to 2 but stayed unsolved; temporal comparison showed carrier owner `31`/`15` closure was missing.
  - Full edge-closure reserve (direct roots + all `criticalDependencyEdge` owners) produced 4/4 solved/supportDepth=4 but only coverage ~0.41-0.42 and high choices.
  - Minimal closure reserve (`0;1;2;3;4;5;6;7;8;9;10;11;12;13;14;15;16;17;19;31;32;40`) produced solved/supportDepth=4 candidates at coverage ~0.45-0.47, still not production due high avg/max choices and low density.
- Current conclusion: full escape-ray release skeleton is the correct solvability scaffold, but treating it as a hard forbidden reserve is too conservative. Next cut should turn release lanes into controlled lanes that can contain fill only when the inserted chain participates in the temporal release order; do not return to static head/region masks as the main route.

## Tonight Fullish Max44 Review Pack - 2026-06-24

- Goal alignment with GPT/user: tonight pass target is a playable full-ish candidate, not final 0.9 production. Practical threshold: solved, supportDepth >=3/4, avg/max choices controlled, no motif collapse, coverage materially above the 0.338 parent.
- Main full-canvas release-lane experiments proved the scaffold boundary but did not yet produce high coverage: full-ray release closure can preserve solved/supportDepth but caps coverage near 0.42-0.47.
- Fallback route produced a playable review pack by shrinking canvas to 18x24, keeping 44 total chains, pre-seeding an outer frame, and running seeded SGP fill.
- Mounted Demo activePack to `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_TonightFullishMax44V1ReviewPack.asset` (guid `2f4a0a26d8b44ac99d8443914bd1a2de`).
- Frozen pack contains 6 candidates under `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/TonightFullishMax44V1Review`.
- Frozen trace verification: 6/6 solved, all supportClosureBestDepth=4, supportScore `0.918-0.946`, avgChoices `3.61-5.20`, maxChoices `7-9`, outerExitHeadCount `8-9`; 5/6 are `MediumStructure`, 1/6 `LocalEasy`; processTier includes S/A. Best first-review candidate is rank 1 / `tonight_fullish_max44_v1_01...` with solved=True, MediumStructure, depth=4, avg/max `4.18/7`.
- Density up-probe `tonight_smallcanvas_max50_v1` reached coverage up to `0.6227` and remained solvable, but mostly collapsed to `LocalEasy` with outerExitHeadCount `12-15`; not mounted as the main demo pack. This confirms max44 is the better tonight review balance while max50 shows the next density frontier.
- Interpretation: this is the first end-to-end playable “full-ish” package for tonight, not final production. Remaining gap is outerExitHeadCount still high and coverage is ~0.50-0.58 rather than near-full; next production direction is controlled release-lane fill on full canvas, plus this small-canvas route as fallback/demo baseline.

## Gate Vocabulary V1 One-Per-Door Production Probe - 2026-06-24

- User rejected the previous solved-skeleton growth pack because levels 5/6/7 were the same root/gate grown step-by-step rather than distinct roots.
- Rebuilt the review unit as one filled candidate per strict-dual gate design: offsets 0-3 from `gate_vocabulary_v1_skeleton_only_trace_metrics.csv`, each filled independently with `Build-HardLockSlotDirectedBatchFillV1.ps1`.
- Mounted Demo activePack to `SGPRhythmLab_GateVocabularyV1OnePerDoorProdReviewPack`, GUID `724ca47d6f744c088f20d60fe3412dfa`.
- Pack contents are 4 solved strict-dual candidates, one per door/gate design. Selected candidate coverage is still low (`0.1091-0.1348`), chains 17-19, avgChoices `3.58-3.82`, maxChoices 6, antiLocal `0.833-0.846`, supportDepth 2-3.
- Frozen trace verified 4/4 `solved=True` and 4/4 `strictDualGateCandidate=True`.
- Interpretation: this is a player-facing vocabulary review pack, not the final 0.30 production pack. If the user confirms the 4 door designs are truly distinct enough, next step is to push each accepted door independently toward 0.20 -> 0.25 -> 0.30 while enforcing one-output-per-door and no growth-sequence duplicates.

## Gate Vocabulary Door Push020 Mid Review - 2026-06-24

- User confirmed the 4 one-per-door strict-dual designs have visible/experiential difference, so gate/door vocabulary is accepted as a valid diversity axis.
- Attempted parallel push toward coverage 0.20 from the 4 accepted door candidates. Long run timed out before writing final CSV, but accepted trace steps were recovered from per-round candidate/trace files.
- Mounted Demo activePack to `SGPRhythmLab_GateVocabularyDoorPush020MidReviewPack`, GUID `9a4ff7b38d774e3a83b0d13b5294a2a7`.
- Review pack contains 4 candidates, one per door design, at coverage `0.1679-0.1814`, chains 23-25, avgChoices `3.48-3.58`, maxChoices 6, antiLocal `0.833-0.895`, supportDepth 2-4.
- Frozen trace verified 4/4 `solved=True`, 4/4 `strictDualGateCandidate=True`, 4/4 `TrueHardCandidate`, processTier A.
- Interpretation: this is the first filled strict-dual door vocabulary review pack with more body than the skeleton pack. It still has not reached 0.20/0.25/0.30; next step after user review is per-door push with shorter/faster staged targets, not one long 0.20 run.

## Gate Vocabulary Door MultiVar V1 Review8 - 2026-06-24

- User updated tonight's goal: not only door/gate roots; build variants for each validated root direction, then size/canvas expansions after root variants are reviewed.
- Completed strict-dual door multi-variant review pack: 4 accepted door designs x 2 seed/fill variants.
- Demo activePack mounted to `SGPRhythmLab_GateVocabularyDoorMultiVarV1Review8Pack`, GUID `85a0f9a3cf9b4a80a712c7db013d4c4f`.
- Selected CSV: `gate_vocab_door_multivar_v1_review8_selected.csv`; frozen trace: `gate_vocab_door_multivar_v1_review8_frozen_trace_metrics.csv`.
- Frozen trace result: 8/8 solved, 8/8 strictDualGateCandidate, 8/8 A-tier, 7 TrueHardCandidate + 1 HardPotential. Coverage from selected source rows is `0.1618-0.190`, chains 23-25, avgChoices `3.20-3.58`, maxChoices 6, antiLocal `0.789-0.895`, supportDepth 2-4.
- This pack is for human review of per-door variants. It should not be counted as non-door root diversity; next work should switch to non-door root families/directions before size/canvas expansion.

## Non-Door Root MultiVar V1 Review11 - 2026-06-24

- After completing strict-dual door variants, switched to non-door root directions per updated goal.
- Built `non_door_root_variant_base_v1.csv` from one representative each of support_lock, web_crossover, hub_spoke, cascade_relay, split_key and measured current coverage/metrics.
- Direct directed-fill worked for support_lock only: recovered `non_door_root_var_s1_support_b01_r1_c03` from round trace, 41 chains, avg/max `3.2/7`, antiLocal `0.703`, supportDepth 4, TrueHardCandidate.
- Direct fill produced no final candidates for web_crossover/hub_spoke/split_key under the quick shared parameter set; these families need their own variant/size route rather than strict-dual-style top-up.
- Mounted Demo activePack to `SGPRhythmLab_NonDoorRootMultiVarV1Review11Pack`, GUID `d2b359c580fc48a7ab06e61af81a4499`.
- Pack contains support_lock/web_crossover/hub_spoke/cascade_relay/split_key existing family pairs plus the new support_lock fill variant. Frozen trace: 11/11 solved, all A-tier; 7 TrueHardCandidate, 4 MediumStructure.
- Interpretation: this pack is for human review of non-door root family/direction difference. It is not a final production-hard filtered pack because cascade and two support_lock baselines remain MediumStructure.

## Gate Vocabulary Door Size Smoke V1 - 2026-06-24

- Ran first non-mirror size expansion smoke from the strict-dual door multivar pack: first 4 roots x `wide30_shift` and `tall40_shift`.
- Generated 8 size variants with `Build-RootCanvasVariantsV1.ps1`, then traced with absolute-path manifest after discovering relative paths were joined against SourceRoot instead of OutputRoot.
- Trace result: 8/8 solved, 8/8 strictDualGateCandidate, A-tier; 5 TrueHardCandidate, 1 HardPotential, 2 MediumStructure. Wide shifts are more likely to lower hard class; tall shifts were all TrueHard in this smoke.
- Frozen size smoke pack: `SGPRhythmLab_GateVocabularyDoorSizeSmokeV1Pack`, GUID `5d0ff4be64c94ad48788fa3766f67593`.
- Demo was not switched to this pack; current active Demo remains `NonDoorRootMultiVarV1Review11Pack` for root-family review.

## Non-Door Root Size Smoke V1 - 2026-06-24

- Continued active goal by testing non-door root size expansion after building non-door root review pack.
- Generated non-mirror `wide30_shift` and `tall40_shift` variants for `support_lock`, `web_crossover`, `hub_spoke`, and `split_key` from `non_door_root_variant_base_v1.csv`.
- Initial trace: 8/8 solved/A. `hub_spoke`, `web_crossover`, and `split_key` kept at least HardPotential/TrueHard in both wide/tall variants; `support_lock` stayed MediumStructure in both size variants and was excluded from the frozen size smoke pack.
- Frozen pack: `SGPRhythmLab_NonDoorRootSizeSmokeV1Pack`, GUID `6e4a8fbaf2dd4fe9ab2ff1f718737b62`.
- Frozen trace: 6/6 solved/A; hub_spoke wide=TrueHard/tall=HardPotential, split_key wide=HardPotential/tall=TrueHard, web_crossover wide=HardPotential/tall=TrueHard.
- Demo activePack was not switched; current Demo remains `SGPRhythmLab_NonDoorRootMultiVarV1Review11Pack` for root-family visual review.
- Next: support_lock size expansion should use the newly recovered TrueHard support variant (`non_door_root_var_s1_support_b01_r1_c03`) rather than the original Medium support baseline. For web/hub/split, size expansion is viable and can be scaled after human review.

## Non-Door Root Size Smoke V1 Plus Support - 2026-06-24

- Extended the non-door size smoke to include support_lock by using the recovered TrueHard support source `non_door_root_var_s1_support_b01_r1_c03` instead of the original Medium support baseline.
- Support recovered size trace: wide30_shift kept TrueHardCandidate/supportDepth4; tall40_shift kept HardPotential/supportDepth2. This proves support_lock size expansion is viable when the source parent is strong enough.
- Frozen pack: `SGPRhythmLab_NonDoorRootSizeSmokeV1PlusSupportPack`, GUID `be8f304517d54a58b8fcdb9295504ce0`.
- Frozen trace: 8/8 solved/A, covering support_lock, web_crossover, hub_spoke, split_key x wide30/tall40. Result distribution: 4 TrueHardCandidate + 4 HardPotential.
- Demo activePack was intentionally left on `SGPRhythmLab_NonDoorRootMultiVarV1Review11Pack` so the user can first judge root family differences; the size pack is ready but not mounted.
- Decision implication: size/canvas expansion should run after a strong per-root parent is selected. For strict-dual and recovered support, tall/wide shifts are viable; for web/hub/split both wide/tall survived, with tall often preserving class better for web/split and wide preserving depth better for hub/support.

## Root Variant Library V1 Core Pack - 2026-06-25

- Consolidated the scattered strict-dual, non-door, and size-smoke outputs into the first core root/variant library pack.
- Input catalog: `root_variant_library_v1_core_selected.csv`; filtered out MediumStructure diagnostic rows and kept only solved/A candidates with `TrueHardCandidate` or `HardPotential`.
- Frozen pack: `SGPRhythmLab_RootVariantLibraryV1CorePack`, GUID `a9802eed58384d9eb06618041ff1b457`.
- Demo activePack is now mounted to this core library pack.
- Frozen trace: 29/29 solved, 29/29 A-tier, 23 TrueHardCandidate + 6 HardPotential, no MediumStructure. AvgChoices average `3.51`, maxChoices range `6-7`, antiLocal range `0.607-0.895`.
- Root/variant coverage in the pack: strict_dual_gate door/fill variants and size variants; support_lock recovered fill + size variants; web_crossover existing and size variants; hub_spoke existing and size variants; split_key existing and size variants.
- This is now the main review artifact for the active goal. Remaining gaps: cascade_relay has only Medium diagnostic samples and is not admitted into the core library; each admitted root still needs human review for perceptual duplication before production admission.

## Campaign500 Hardening Sandbox V3 - 2026-06-25

- User asked to push hardening further than V2 by reducing opening/direct outer exits and average available choices.
- Implemented V3 in `Assets/ArrowMagic/Editor/CampaignHardeningAnalyzer.cs`: redirect pass for direct-clear outer heads, pressure-scored merge pass, Greedy validation, and rollback fix for rejected merge candidates.
- Important bug found/fixed: rejected full Greedy merge candidates were leaving their mutated authored data behind when no candidate was accepted, causing false unsolved outputs with `ops=0`.
- Final mounted demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV3Pack.asset` (GUID `28ab2ac1c2c809d47a3d82be185cb2d9`), 10 original/pressure pairs.
- Final report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v3_20260625_011430.csv`.
- Final pressure averages: opening choices `27.3 -> 22.2`, direct clearable outer exits `27.3 -> 22.2`, early avg choices `26.42 -> 21.72`, full avg choices `14.32 -> 11.65`, leak score `602.5 -> 507.6`, Greedy solved 10/10.
- Demo scene now points to V3. Current limitation: V3 mostly redirects/merges existing chains; bigger “质变” beyond this likely needs a true gate-injection operator, not just local post-processing.

## Campaign500 Hardening Sandbox V4 - 2026-06-25

- User approved trying V4 after V3. Implemented V4 as a gate-fold sandbox, not replacing V3.
- V4 route: skip oversized sources (`chains > 200`) for this probe, run selected outer-head redirect, then fold opening/free chains into adjacent chains using `FindGateFoldCandidates`; every accepted fold must keep Greedy solved and roll back rejected candidates.
- Final mounted demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV4Pack.asset` (GUID `be23e479c2ed2c74987c99ebef164ab1`), 10 original/gate-v4 pairs.
- Final report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v4_20260625_064105.csv`.
- Final V4 averages: opening choices `27.8 -> 18.1`, direct clearable outer exits `27.8 -> 18.1`, early avg choices `26.78 -> 17.59`, full avg choices `14.71 -> 10.87`, leak score `610.8 -> 413.2`, chains `156.1 -> 151.1`, Greedy solved 10/10.
- V4 is a clear step beyond V3 in pressure, but it is slower; future productionization should add a cheap gate-fold prefilter or run it only on medium-size/high-leak sources first.

## Campaign500 Hardening Sandbox V4.1/V4.2 Boundary - 2026-06-25

- Continued V4 with a second-pass gate-fold sandbox from V4 outputs. V4.1 pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV41Pack.asset` (GUID `23f5588cd74a74c488e7752bbd4f825e`), 8 V4/V4.1 pairs; current `Assets/ArrowMagic/Scenes/Demo.unity` activePack is intentionally mounted to V4.1 for review.
- V4.1 report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v41_20260625_071610.csv`.
- V4.1 averages over the 8 accepted outputs: chains `141.875 -> 140.5`, opening choices `18.75 -> 15.5`, direct clearable outer exits `18.75 -> 15.5`, early avg choices `18.50 -> 16.26`, full avg choices `10.84 -> 10.05`, leak score `422 -> 358.5`, Greedy solved 8/8.
- Added V4.2 third-pass probe from V4.1 outputs to test remaining headroom. V4.2 produced only 1 meaningful pair out of 8 scanned: opening/direct outer `14 -> 13`, avg choices `8.069 -> 7.908`, leak `301 -> 281`; the other 7 had `ops=0`.
- V4.2 pack/report are retained as boundary evidence, not the main review pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV42Pack.asset` and `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v42_20260625_072958.csv`.
- Current conclusion: the local postprocess route (`outer-head redirect + gate-fold chain merge`) is basically saturated at V4.1. Further real difficulty uplift needs a new operator that injects/rebuilds dependency gates or regenerates portions under a trace gate, not another simple repeated fold pass.

## Campaign500 Hardening Sandbox V5 Visible Gate - 2026-06-25

- User reported V4.1 had little visible/feel difference. Implemented V5 as a visible gate-injection sandbox in `Assets/ArrowMagic/Editor/CampaignHardeningAnalyzer.cs`.
- V5 route: from latest leak-ranked final pack rows, skip very large/low-opening sources, then insert short 2-3 cell hook/bend gate chains into direct outer-exit rays. Every insertion is validated with `AnalyzeLevel`/Greedy and rolled back if it breaks solvability or makes choice pressure explode.
- Final mounted demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV5Pack.asset` (GUID `045e629960371db42b4407cfdf5e8752`), 6 original/visible-gate pairs; current `Assets/ArrowMagic/Scenes/Demo.unity` activePack points to V5.
- Final report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v5_20260625_075227.csv`.
- V5 averages over 6 accepted outputs: chains `125 -> 128`, arrow tiles `917.17 -> 926.17`, opening choices `28 -> 21.83`, direct clearable outer exits `28 -> 21.83`, early avg choices `26.90 -> 19.98`, full avg choices `14.81 -> 9.70`, leak score `602.33 -> 455.83`, Greedy solved 6/6.
- Interpretation: V5 is the first hardening pass that should be visible to the eye because it adds actual small gate chains. It is slower and only accepted 6/29 scanned rows; productionizing it requires a faster prefilter and probably a more motif-aware gate placement scorer to avoid adding arbitrary-looking small hooks.

## Campaign500 Hardening Sandbox V6 Early Peel Gate - 2026-06-25

- User proposed a better gate direction: first simulate removing the early free layer, continue peeling if many moves remain, then build gates around the leaked early/deep layers. Implemented V6 in `CampaignHardeningAnalyzer.cs`.
- V6 route: `BuildEarlyPeelResultV6` repeatedly removes current available chains in waves until choice count falls near target or the peel cap is reached, records removed chain wave/depth, then `FindEarlyPeelGateCandidatesV6` places short hook/bend gate chains on rays of wave 1-3 leaked chains. Every candidate is still validated by full `AnalyzeLevel`/Greedy.
- Final demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV6Pack.asset` (GUID `6e5820f0c0e73ff4bae751486923559b`), 6 original/early-peel-gate pairs; superseded for current Demo review by V7.
- Final report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v6_20260625_082646.csv`.
- V6 averages over 6 accepted outputs: chains `125 -> 129`, arrow tiles `917.17 -> 929.17`, opening choices `28 -> 23.33`, direct clearable outer exits `28 -> 23.33`, early avg choices `26.90 -> 21.75`, full avg choices `14.81 -> 10.95`, leak score `602.33 -> 486.67`, Greedy solved 6/6.
- Operations confirm the intended behavior: accepted gates mostly target `w2/w3` peel waves, not only the initial opening layer. Compared with V5, V6 adds one more gate per level on average but is less aggressive on immediate opener reduction; it should be reviewed for whether deeper staged feel is better than V5's stronger first-screen pressure.

## Campaign500 Hardening Sandbox V7 Opening Peel Gate - 2026-06-25

- User asked why V6 could not push opening choices down enough. Diagnosis: V6 rewarded deeper peel waves and allowed `ops>=3` to pass, so it often changed midgame leakage more than the first-screen opener count.
- Implemented V7 in `Assets/ArrowMagic/Editor/CampaignHardeningAnalyzer.cs`: current-opening clearable chains are targeted first, a quick pressure prefilter selects gate candidates, and only the top candidates get full Greedy validation. Each accepted opening gate must reduce `OpeningChoices` by at least 1.
- Initial over-wide V7 candidate search stalled on L387; fixed by adding quick prefilter and reducing the review sample to 4 pairs / max 5 opening gates / no deep-peel add-on for the interactive sandbox.
- Final mounted demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV7Pack.asset` (GUID `056a052d84fe8794c9d59e7b18ece827`), 4 original/opening-peel-gate pairs; current `Assets/ArrowMagic/Scenes/Demo.unity` activePack points to V7.
- Final report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v7_20260625_091100.csv`.
- V7 averages over 4 accepted outputs: chains `122.75 -> 127.75`, arrow tiles `912.75 -> 927.75`, opening choices `30.25 -> 22.5`, direct clearable outer exits `30.25 -> 22.5`, full avg choices `14.41 -> 9.95`, leak score `620 -> 464.75`, Greedy solved 4/4.
- Interpretation: V7 is the strongest opening-pressure sandbox so far, but it is still a tactical add-chain operator. For production, the next step should test replacing/rewiring some early free chains as lock/key gates instead of only inserting short blockers.

## Campaign500 Hardening Sandbox V8 Opening Rewire Gate - 2026-06-25

- User clarified the goal is qualitative change, not just stronger numbers. Implemented V8 in `Assets/ArrowMagic/Editor/CampaignHardeningAnalyzer.cs`.
- V8 route: find current opening free/direct chains, find nearby partner chains, optionally bridge through 0-5 empty cells, then merge/rewire the two existing chains into one longer chain. A limited V7 fallback gate is allowed only after at least one rewire path exists, and accepted outputs must include `openingRewire`.
- Frozen demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV8Pack.asset` (GUID `ec493029142ef3d4c8b34f2c1f28afcf`), 4 original/opening-rewire-gate pairs; current `Assets/ArrowMagic/Scenes/Demo.unity` activePack points to V8.
- Final report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v8_20260625_092655.csv`.
- V8 averages over 4 accepted outputs: chains `150 -> 147.25`, arrow tiles `963.25 -> 965`, opening choices `26.75 -> 20`, direct clearable outer exits `26.75 -> 20`, full avg choices `14.70 -> 10.89`, leak score `596.75 -> 466`, Greedy solved 4/4.
- Compared with V7, V8's visible difference should be stronger because the chain count goes down and original free chains become longer rewired chains; however, most accepted operations are adjacent merges, with only one bridge rewire in the first pack. Next improvement should increase non-adjacent bridge rewires and true key/lock pair rewrites if the visual still feels too close.

## Campaign500 Hardening Sandbox V9 Opening Outer Rewire Gate - 2026-06-25

- User feedback on V8: visible change exists, but there are still too many outer exits. Implemented V9 in `Assets/ArrowMagic/Editor/CampaignHardeningAnalyzer.cs`.
- V9 route: bulk-flip direct outer heads inward, then run a post opening-rewire pass without fallback add-gates. Sandbox reports now include both clearable outer exits and all direct outer exits.
- Frozen demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV9Pack.asset` (GUID `9b28502a2d25e23459733bd8fcd480d9`), 4 original/opening-outer-rewire-gate pairs; current `Assets/ArrowMagic/Scenes/Demo.unity` activePack points to V9.
- Final report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v9_20260625_095945.csv`.
- V9 averages over 4 accepted outputs: chains `151 -> 150`, arrow tiles `1019 -> 1020.25`, opening choices `28 -> 18.5`, direct clearable outer exits `28 -> 18.5`, all direct outer exits `28 -> 18.5`, full avg choices `14.72 -> 9.87`, leak score `614.5 -> 409`, Greedy solved 4/4.
- Interpretation: V9 is materially stronger than V8 on outer pressure, but still leaves about 14-25 direct outer exits per sample. The remaining exits are often double-ended boundary chains where flipping the head just moves the outer exit to the other end. Next breakthrough should add endpoint-inset / boundary double-ended chain merge or trim-rebuild operators; more head flipping has diminishing returns.

## Campaign500 Hardening Sandbox V10 Outer Exit Endpoint - 2026-06-25

- User emphasized outer exits must be treated as the priority. Implemented V10 in `Assets/ArrowMagic/Editor/CampaignHardeningAnalyzer.cs`.
- V10 now starts from the latest V9 accepted outputs instead of rerunning final source hardening. Demo pairs are `V9 before endpoint` then `V10 endpoint output`, so the review focuses only on the additional outer-exit treatment.
- Operators: `endpointReroute` moves a direct-outer chain head to an adjacent empty cell around the neck; `endpointTrim` removes a short boundary-facing head segment. Every accepted step must keep Greedy solved and reduce all direct outer exits.
- Frozen demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV10Pack.asset` (GUID `2c430fadb59a22249a45f1bf01814753`), 3 V9-before/V10-endpoint pairs; current `Assets/ArrowMagic/Scenes/Demo.unity` activePack points to V10.
- Final report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v10_20260625_104309.csv`; source V9 report: `campaign_hardening_sandbox_v9_20260625_095945.csv`.
- V10 accepted outputs average from V9 source to V10: chains `139.67 -> 139.67`, arrow tiles `997 -> 988.33`, opening choices `18 -> 12.33`, direct clearable outer exits `18 -> 12.33`, all direct outer exits `18 -> 12.33`, full avg choices `10.28 -> 7.31`, leak score `395 -> 275.33`, Greedy solved 3/3.
- L449 was skipped because endpoint treatment only changed direct outer exits `20 -> 19`; this is intentionally rejected under the new outer-exit-focused gate.
- Risk/next step: endpoint trim removes some arrow tiles, so visual review must check whether boundary holes look acceptable. If quality is good, production version should add a refill pass after endpoint trim; if it looks too cut-up, prefer reroute/merge over trim.

## Campaign500 Hardening Sandbox V11 Multi-Layer Outer Exit - 2026-06-25

- User asked to also treat chains that become continuously clearable after the initial arrows are removed, not just current-frame outer exits.
- Implemented V11 in `Assets/ArrowMagic/Editor/CampaignHardeningAnalyzer.cs`: it starts from latest V10 outputs, simulates the first 4 Greedy peel waves, detects future-layer clearable chains whose head ray reaches outside, then applies future-outer orientation flip, endpoint reroute, and trim.
- Frozen demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV11Pack.asset` (GUID `29a79c0bec2fdd740a603c8e8bf06340`), 3 V10-before/V11 pairs; current `Assets/ArrowMagic/Scenes/Demo.unity` activePack points to V11.
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v11_20260625_111032.csv`.
- V11 accepted outputs average from V10 source to V11: chains `139.67 -> 139.67`, arrow tiles `988.33 -> 981`, opening choices `12.33 -> 12.33`, all direct outer exits `12.33 -> 12.33`, peel-layer outer exits `51 -> 37`, future peel outer exits `38.67 -> 24.67`, full avg choices `7.31 -> 5.72`, Greedy solved 3/3.
- Interpretation: V11 is a qualitative layer over V10. It leaves current direct outer exits roughly unchanged, but reduces the continuous second/third/fourth-layer sweep that appears after the first openings clear.

## Campaign500 Hardening V12 PBE/NEE Classification - 2026-06-25

- Implemented analysis-only V12 in `Assets/ArrowMagic/Editor/CampaignHardeningAnalyzer.cs`: menu entry `Tools/Arrow Magic/Campaign 500/Hardening/Build PBE NEE Classification Report V12`.
- V12 reads the latest V11 before/after assets and classifies each early peel outer exit into `PBE` (initial board already direct outer) or `NEE` (newly exposed only after earlier peel waves clear).
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_v12_pbe_nee_20260625_112948.csv`; no new pack and no Demo change.
- Result over 3 V10-before/V11-after pairs: V10-before averages direct outer `12.33`, peel outer `51`, future outer `38.67`, PBE `12.33`, future PBE `0`, NEE `38.67`; V11-after averages direct outer `12.33`, peel outer `37`, future outer `24.67`, PBE `12.33`, future PBE `0`, NEE `24.67`.
- Current conclusion: this sample's future-layer leaks are all NEE, not persistent future PBE. Next operator work should split routes: current/wave0 PBE requires boundary structure repair; NEE requires peel-aware propagation gates.

## Campaign500 Hardening Sandbox V12 BDR-lite - 2026-06-25

- Synced V12 classification result back to the GPT advisor conversation; GPT agreed with PBE/NEE routing and recommended prioritizing current-frame PBE because it is the visible first-impression outer-exit problem.
- Implemented V12 BDR-lite in `Assets/ArrowMagic/Editor/CampaignHardeningAnalyzer.cs`: menu entry `Tools/Arrow Magic/Campaign 500/Hardening/Build Boundary Rewire Sandbox V12`.
- Operation: start from latest V11 outputs and prepend small one-cell hooks to selected current direct-outer/PBE endpoints. Each accepted step must keep Greedy solved, reduce current direct outer/PBE, and avoid NEE rebound.
- Frozen demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV12BDRPack.asset` (GUID `22d9ef7c9eba2844d8a2af3166daad93`), 3 V11-before/V12BDR pairs; current `Assets/ArrowMagic/Scenes/Demo.unity` activePack points to V12BDR.
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v12_bdr_20260625_114050.csv`.
- Result over 3 outputs from V11 to V12BDR: chains `139.67 -> 139.67`, arrow tiles `981 -> 983`, opening choices `12.33 -> 10.67`, all direct outer exits `12.33 -> 10.67`, peel outer exits `37 -> 32`, future peel outer exits `24.67 -> 21.33`, full avg choices `5.72 -> 5.36`, Greedy solved 3/3.
- Current conclusion: BDR-lite is directionally correct but mild. It reduces current PBE without NEE rebound, but a production-worthy visible shift likely needs stronger boundary double-end restructuring, two-cell hook/stitching, or endpoint merge/rebuild.

## SGP Fullchain Growth Review4 - 2026-06-25

- Active goal: from a verified ~0.30 hard parent, push toward a fuller playable board while preserving solved trace and support-lock motif; GPT alignment favored keeping SGP as packing engine plus minimal decision-time steering, with trace as final gate.
- Negative result: one-shot high-density adapter (`HeadOnly` / `PenalizeBody`) reached `~0.65-0.68` coverage but all dropped unsolved with `supportClosureBestDepth=0`; plain SGP can fill but destroys temporal motif.
- Positive route: `Build-IncrementalRoleMapFillCompilerV1.ps1` with commit-based trace validation is viable. It advanced the same parent through `0.426 -> 0.507 -> 0.529 -> 0.537` while keeping `solved=True`, A-tier, `maxChoices=9`, `outerExitSolveRunMax=1`.
- Best current frozen review pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_TonightFullchainGrowthReview4Pack.asset` (GUID `c40d5f6f69b94b4f8f6dd150ca34f728`), mounted in `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/tonight_fullchain_growth_review4_frozen_trace_metrics.csv`; 4/4 solved/A, first two supportDepth=4 at `~0.507/~0.528`, last two supportDepth=3 at `~0.534/~0.537`.
- Current boundary: above `~0.53` the same parent still yields occasional valid commits, but candidate space narrows sharply and most topK candidates are MediumStructure/Drop or support-depth preserving but unsolved.
- Implementation note: `Build-SeededDirectSGPFillBaselineV1.ps1` was extended with `PreActionMaskReservedCellMode` (`Hard/HeadOnly/PenalizeBody`) and `AdapterReservedBodyPenalty`; this proved static mask relaxation is not enough.
- Next step: do not chase 0.9 with one parent tonight. Either improve incremental compiler selection to prefer motif depth over risk-only, or run the same commit loop across multiple strong parents and keep the highest stable fullchain candidates.

## SGP Fullchain Growth Review5 Boundary - 2026-06-25

- Continued the same verified ~0.30 parent with `Build-IncrementalRoleMapFillCompilerV1.ps1`.
- Strict-ish A line now reaches coverage `0.5686275` with `solved=True`, A-tier, `supportClosureBestDepth=3`, `avgChoices=5.23`, `maxChoices=10`, `localPatchSolveRunMax=3`, `dependencyFollowRunMax=5`, `outerExitSolveRunMax=1`.
- Full-batch search at the `0.568` boundary found a depth-4 candidate at coverage `0.5759804`, but it was `processTier=Drop` due local patch/nearby solve bursts (`localPatchSolveRunMax=6`); this is a boundary diagnostic, not production hard.
- Relaxed density line can continue to coverage `0.5955882` while staying `solved=True`, B-tier/MediumStructure, `supportDepth=3`, `localRun=3`, `outerExitSolveRunMax=1`; quality debt is high (`maxChoices=12`, `followRun=6`, `outerExitHeadCount=5`).
- From `0.5955882`, both ordinary relaxed and `TraceCavitySplitRecovery` runs failed to find an acceptable next commit; traced candidates became `solved=False` and/or `supportDepth=2`. Current single-parent practical boundary is therefore ~`0.596` for solved Medium and ~`0.568` for A-quality.
- Frozen review pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_TonightFullchainGrowthReview5Pack.asset`, GUID `17ae8e3c914a41b2be6399f7258be1bf`; Demo in `.worktrees/sgp-rhythm-lab` now points to this pack.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/tonight_fullchain_growth_review5_frozen_trace_metrics.csv`; 6/6 solved, first 4 A-tier growth nodes, last 2 B-tier density-boundary nodes.
- GPT review after these results: likely issue is high-density candidate-language drift/high-branch contamination, not raw SGP packing or trace gate; next implementation cut should stabilize candidate generation around branch/local complexity and dependency reuse instead of only relaxing gates.
- Implemented opt-in candidate language stabilizer in `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-IncrementalRoleMapFillCompilerV1.ps1`: `UseCandidateManifoldStabilizer`, `MaxCandidatePathLength`, `MaxCandidateHeadPotential`, `CandidateHeadPotentialPenalty`, `CandidateDependencyTouchReward`, `CandidateSafeOnlyPenalty`.
- Stabilizer smoke from the `0.5686275` parent selected the cleaner B/Medium node at `0.5747549` (`localRun=3`) and avoided the depth-4/local-burst Drop candidate; it did not raise the density ceiling by itself. Current interpretation: this is a useful guardrail, not the missing high-density generator language.

## Root Variant Library V1.2 Core Pack - 2026-06-25

- Active goal continues: build multiple validated variants for each distinct root direction, then size/canvas extensions after root variants pass review.
- Recovered cascade_relay into the core-quality pool. Existing cascade diagnostic samples were true cascade shape but Medium; a short recovery fill produced HardPotential cascade candidates while preserving cascade classifier (`fanout<=2`, `branch<=1/2`, no strict dual, no web collapse).
- Added 3 cascade fill-recovery candidates and 2 cascade tall40 size variants; wide30 cascade size variants stayed solved/cascade but dropped to MediumStructure, so they were excluded from core.
- Added a second non-size support_lock fill variant from the recovered TrueHard support parent: coverage `0.3174`, solved/A, HardPotential, maxChoices 7, supportDepth 4.
- Frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV12CorePack.asset`, GUID `1683b2f5c1aa4a129d36f4bd00e2efff`; Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack.
- Frozen trace: `root_variant_library_v1_2_core_frozen_trace_metrics.csv`; 35/35 solved, all A-tier, 23 TrueHardCandidate + 12 HardPotential, avgChoices average `3.386`, maxChoices range `5-7`, antiLocal range `0.607-0.895`.
- Current library distribution: strict_dual_gate 14, support_lock 4, web_crossover 4, hub_spoke 4, split_key 4, cascade_relay 5. Next priority is not more door/cascade; review V1.2 visually, then generate stronger non-size variants for web_crossover / hub_spoke / split_key if they feel too close.

## Root Variant Library V1.3 Core Pack - 2026-06-25

- Continued active goal by targeting weaker non-door variant coverage for `web_crossover`, `hub_spoke`, and `split_key`.
- Result: `hub_spoke` produced one new non-size fill variant that passed the family classifier and frozen trace as `TrueHardCandidate` (`avg/max=3.37/7`, antiLocal `0.703`, supportDepth `4`, localRun `2`, followRun `3`, outerExitHeadCount `0`).
- `web_crossover` produced no legal filler groups under the shared directed-fill operator; this is treated as operator mismatch, not as a reason to mark web invalid.
- `split_key` produced split-classified candidates, but they dropped to `MediumStructure`; these are retained as diagnostics and not admitted into the core hard library.
- Frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV13CorePack.asset`, GUID `ba7c9cf303914fc695b55e69be80763e`; Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack.
- Frozen trace: `root_variant_library_v1_3_core_frozen_trace_metrics.csv`; 36/36 solved, all A-tier, 24 TrueHardCandidate + 12 HardPotential, avgChoices average `3.385`, maxChoices range `5-7`, antiLocal range `0.607-0.895`.
- GPT review agreed with the local decision: admit the hub variant; do not keep burning time on web/split with the same shared directed-fill operator; web/split need family-specific fill policy or should rely on existing strong/size variants until that policy exists.

## Root Variant Library V1.4 Core Pack - 2026-06-25

- After V1.3, ran size smoke for the newly admitted `hub_spoke` non-size variant.
- `wide30_shift` preserved hub-spoke identity (`hubSpokeCandidate=True`) and remained `TrueHardCandidate`; `tall40_shift` stayed solved/A/HardPotential but failed hub-spoke identity (`not_enough_spokes`) and was kept diagnostic only.
- Frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV14CorePack.asset`, GUID `3c1bfa73bb1c4c2c885dfbfca5849f7b`; Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack.
- Frozen trace: `root_variant_library_v1_4_core_frozen_trace_metrics.csv`; 37/37 solved, all A-tier, 25 TrueHardCandidate + 12 HardPotential, avgChoices average `3.385`, maxChoices range `5-7`, antiLocal range `0.607-0.895`.
- Current library distribution: strict_dual_gate 14, hub_spoke 6, cascade_relay 5, support_lock 4, web_crossover 4, split_key 4. Next work should either create a family-specific web/split fill policy or move to the next causal root direction; do not continue shared-fill brute force on web/split.

## Root Variant Library V1.5 Core Pack - 2026-06-25

- Expanded `support_lock` using two strong non-size support parents. Shared directed-fill produced two new support variants at coverage `0.3235` and `0.3248`, both supportDepth `4`, maxChoices `7`, localRun `2`, followRun `3/4`.
- Frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV15CorePack.asset`, GUID `f1f40d935d264f729fa7390bde978ac9`; Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack.
- Frozen trace: `root_variant_library_v1_5_core_frozen_trace_metrics.csv`; 39/39 solved, all A-tier, 26 TrueHardCandidate + 13 HardPotential.
- Current library distribution: strict_dual_gate 14, hub_spoke 6, support_lock 6, cascade_relay 5, web_crossover 4, split_key 4.
- Next priority: do not add more strict_dual/support/hub until web/split/cascade catch up, unless the user wants a larger same-family review pack. Web/split need family-specific fill policy or new root sources; cascade can still accept tall/relay-style variants but should avoid long same-structure duplication.

## Root Variant Library V1.6 Core Pack - 2026-06-25

- Filled the web/split variant gap without using shared directed-fill: generated wide/tall size variants for both existing `web_crossover` and both existing `split_key` sources, traced them, ran family classifiers, then deduped by arrow geometry hash against the existing library.
- Of 8 size candidates, 4 were exact duplicates of existing V1.5 web/split size rows and 4 were unique. All 4 unique rows passed board trace, hard class, and family classifier.
- Frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV16CorePack.asset`, GUID `d6dbde4de6d94b07be26fcb17779ab7e`; Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack.
- Frozen trace: `root_variant_library_v1_6_core_frozen_trace_metrics.csv`; 43/43 solved, all A-tier, 27 TrueHardCandidate + 16 HardPotential, avgChoices average `3.402`, maxChoices range `5-7`, antiLocal range `0.571-0.895`.
- Current library distribution: strict_dual_gate 14, support_lock 6, hub_spoke 6, web_crossover 6, split_key 6, cascade_relay 5.
- Next best increment: either add one more cascade_relay core-quality variant to balance non-door families at 6 each, or start a separate family-specific fill-policy experiment for web/split if visual review says their size variants are too similar.

## Root Variant Library V1.7 Core Pack - 2026-06-25

- Balanced the final non-door family count by adding one extra cascade_relay recovery candidate (`cascade_relay_recovery_v1_t026_b02_r2_c13`): non-duplicate geometry hash, cascade classifier pass, HardPotential, avg/max `2.71/5`, antiLocal `0.692`, supportDepth `3`, branch/fanout `2/2`, localRun `2`, followRun `4`.
- Frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV17CorePack.asset`, GUID `80de53d213124a79b9e87fe1dd4cfe05`; Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack.
- Frozen trace: `root_variant_library_v1_7_core_frozen_trace_metrics.csv`; 44/44 solved, all A-tier, 27 TrueHardCandidate + 17 HardPotential, avgChoices average `3.387`, maxChoices range `5-7`, antiLocal range `0.571-0.895`.
- Current library distribution: strict_dual_gate 14; support_lock, hub_spoke, web_crossover, split_key, cascade_relay each 6.
- This is the current review artifact for the active root/variant-library goal. Next work should be human review for perceptual duplicates, then either (1) trim strict_dual down for balanced review, (2) build family-specific web/split fill policies, or (3) start larger-canvas/coverage growth per accepted root family.

## Root Variant Library V1.8 Balanced Review Pack - 2026-06-25

- Built a smaller human-review pack from V1.7 by keeping exactly 6 candidates per family: support_lock, strict_dual_gate, hub_spoke, web_crossover, split_key, and cascade_relay.
- Strict_dual was trimmed from 14 to 6 representative rows: four base door designs plus one wide and one tall size example. Non-door families remain at their 6 V1.7 rows each.
- Frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV18BalancedReviewPack.asset`, GUID `56cc167e1e3d4c7e9824595d39f67098`; Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack.
- Frozen trace: `root_variant_library_v1_8_balanced_review_frozen_trace_metrics.csv`; 36/36 solved, all A-tier, 21 TrueHardCandidate + 15 HardPotential, avgChoices average `3.371`, maxChoices range `5-7`, antiLocal range `0.571-0.895`.
- Use V1.8 for manual duplicate/experience review. Keep V1.7 as the broader full core library with all strict_dual variants.

## Root Variant Library V1.9 New Variants Review Pack - 2026-06-25

- Active goal: continue producing non-mirror, non-size experiential variants for each accepted root direction before doing larger canvas/size expansion.
- Generated V1.8 duplicate audit reports: `root_variant_library_v1_8_balanced_review_geometry_audit.csv`, `root_variant_library_v1_8_balanced_review_similarity.csv`, and `root_variant_library_v1_8_balanced_review_guide.md`. Audit showed `strict_dual_gate` is relatively healthy, while `split_key`/`web_crossover` have only 2 chain-language signatures and several families still contain high-Jaccard variants.
- Tried `role-zone swap` as a stronger experiential operator; it was too destructive for trace (only 1/6 fast candidates solved and that candidate was MediumStructure), so it is retained as a negative/diagnostic route rather than admitted.
- `cluster_remap` produced admitted variants for `hub_spoke`, `split_key`, and `strict_dual_gate`; `peripheral_jitter_strong` produced 3 admitted `cascade_relay` variants; `peripheral_jitter_mild` produced admitted variants for `split_key`, `support_lock`, and `web_crossover`.
- Frozen review pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV19NewVariantsReviewPack.asset`, GUID `dd827c0c9cf148b3840c571946af7fc2`; `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` is now mounted to this pack.
- Selected CSV: `root_variant_library_v1_9_new_variants_review_selected.csv`; frozen trace: `root_variant_library_v1_9_new_variants_review_frozen_trace_metrics.csv`.
- Pack contents: 25 levels grouped as source control + new variants: cascade_relay 1+3, hub_spoke 1+1, split_key 1+4, strict_dual_gate 3+3, support_lock 1+1, web_crossover 1+5.
- Frozen trace result: 25/25 solved, all A-tier, 21 TrueHardCandidate + 4 HardPotential, avgChoices average `3.432`; maxChoices remain in reviewable range. Use this pack for human perceptual review of non-size variants.

## SGP Fullchain Growth Review6 Short-Chain Boundary - 2026-06-25

- Continued the tonight fullchain goal from the same verified ~0.30 parent after Review5 stalled at `~0.596`.
- Implemented candidate-language fixes: `Build-LocalRoomSGPFillProbeV1.ps1` now supports `-MaxGeneratedChainLength`; `Build-IncrementalRoleMapFillCompilerV1.ps1` passes it through and commit sorting now penalizes lower `processTier` so B candidates beat Drop candidates when both satisfy hard thresholds.
- Key finding: the previous `0.599` boundary was not a hard parent limit. Short-chain candidates pushed the line through `0.6029`, `0.6066`, `0.6115`, `0.6225`, then strict search reached `0.6384804` while staying solved and preserving `supportClosureBestDepth=4` at the high end.
- Frozen Review6 pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_TonightFullchainGrowthReview6Pack.asset`, GUID `a2f7314b49f84817970ec0a4a6c65b44`; `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` now points to this pack.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/tonight_fullchain_growth_review6_frozen_trace_metrics.csv`; 10/10 solved. B-tier clean growth holds through coverage `0.629902`; `0.6335784` and `0.6384804` remain solved/depth4 but Drop due high openers/choice peak/near-outer patch bursts.
- Current strict practical boundary for this single parent is `~0.6385`. Attempts from `0.6385` with strict and cavity-split recovery candidates traced only unsolved candidates with supportDepth `1-2`; next cut should change candidate language/region scheduling, not simply relax thresholds.
- GPT second opinion agreed the `0.6385` stall is likely region sampling collapse rather than parent capacity ceiling. A small `-AvoidWindowOffsets` experiment validated this: avoiding repeated failing windows `30,34,23,31` pushed the same parent to `0.6433824` solved/depth4, then to `0.6458333` solved/depth4.
- A relaxed head-potential run from `0.6458333` reached `0.6507353` solved/depth4 (`avgChoices=6.05`, `maxChoices=12`, `outerExitHeadCount=7`), but it used a high-risk HeadAllowed commit (`headPotentialSum=16`, `riskScore=801`). Treat `0.6507` as a boundary proof, not a clean production candidate.
- Current artifact mounted in Demo remains Review6 pack (up to frozen `0.6384804`). Latest non-frozen boundary CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/tonight_incremental_from0646_short4_relaxhp_t066_v1_final_selected.csv`.

## Root Variant Library V1.10 Hub/Support Review Pack - 2026-06-25

- Continued active root/variant-library goal by targeting underrepresented non-size variants for `hub_spoke` and `support_lock` after V1.9.
- Source controls: V1.8 ranks `7,8,11` (`hub_spoke`) and `25,28,29,30` (`support_lock`), saved at `root_variant_library_v1_10_hub_support_source_roots.csv`.
- `peripheral_jitter_mild` generated 70 variants; frozen trace admitted 6 new A-tier variants: 1 `hub_spoke` and 5 `support_lock`, all depth `4`.
- `cluster_mild` generated 25 variants; frozen trace admitted 7 new A-tier variants: 6 `hub_spoke` and 1 `support_lock`, all depth `4`.
- Frozen review pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV110HubSupportReviewPack.asset`, GUID `fd02a36230034d6ea401bed00ded456b`; `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` is mounted to this pack.
- Selected CSV: `root_variant_library_v1_10_hub_support_review_selected.csv`; frozen trace: `root_variant_library_v1_10_hub_support_review_frozen_trace_metrics.csv`; joined frozen trace: `root_variant_library_v1_10_hub_support_review_frozen_trace_joined.csv`.
- Pack contents: 20 levels = 7 source controls + 13 variants. Frozen trace result: 20/20 solved, all A-tier; variants are 8 TrueHardCandidate + 5 HardPotential. Use this pack for manual perceptual review of hub/support non-size variants before size expansion.

## Root Variant Library V1.11 Hub/Support Size Review Pack - 2026-06-25

- Took the 13 V1.10 hub/support non-size variants and generated only non-mirror canvas variants: `wide30_shift` and `tall40_shift`.
- Size smoke result: 26/26 traced candidates solved/A and preserved hard class; no candidate was admitted by mirror-only transformation.
- Frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV111HubSupportSizeReviewPack.asset`, GUID `17f503e6c015427b8dc3e3812422ea66`; `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` is mounted to this pack.
- Selected CSV: `root_variant_library_v1_11_hub_support_size_review_selected.csv`; frozen trace: `root_variant_library_v1_11_hub_support_size_review_frozen_trace_metrics.csv`; smoke trace join: `root_variant_library_v1_10_hub_support_size_smoke_joined_trace.csv`.
- Frozen trace result: 26/26 solved, all A-tier, 8 TrueHardCandidate + 18 HardPotential. This proves size expansion is technically stable for hub/support variants; manual review still needs to decide whether wide/tall changes produce enough experiential difference.

## Root Variant Library V1.12 Other Roots Size Review Pack - 2026-06-25

- Continued active root/variant-library goal by applying non-mirror size expansion to the remaining V1.9 new-variant families: `cascade_relay`, `split_key`, `strict_dual_gate`, and `web_crossover`.
- Source variants: 15 V1.9 new variants excluding `hub_spoke`/`support_lock`; generated `wide30_shift` and `tall40_shift` only.
- Smoke trace: 30/30 candidates solved/A, but 7 were excluded from review because they dropped to `MediumStructure` or failed strict-dual identity after size transformation.
- Frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV112OtherRootsSizeReviewPack.asset`, GUID `a096dfe33bbc455b96ea4c2c284cdbc9`; `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` is mounted to this pack.
- Selected CSV: `root_variant_library_v1_12_other_roots_size_review_selected.csv`; frozen trace: `root_variant_library_v1_12_other_roots_size_review_frozen_trace_metrics.csv`; smoke trace join: `root_variant_library_v1_12_other_roots_size_smoke_joined_trace.csv`.
- Frozen trace result: 23/23 solved, all A-tier, 13 TrueHardCandidate + 10 HardPotential; avgChoices average `3.608`, maxChoices max `8`, antiLocal range `0.571-0.789`, supportDepth min `3`. Strict-dual size rows retained in this pack all have `strictDualGateCandidate=True`.

## Root Variant Library V1.13 Cascade/Strict Cluster Review Pack - 2026-06-25

- Continued active root/variant-library goal by trying stronger non-size transformations for thinner families after V1.12.
- Negative route: `Build-RootSpatialRecomposeVariantsV1.ps1` on `cascade_relay`, `split_key`, and `strict_dual_gate` generated 30 candidates, but no candidate survived family-safe review. Cascade/split all dropped or became unsolved; the two solved strict-dual-looking rows failed `strictDualGateCandidate=True`. Keep `root_variant_library_v1_13_thin_roots_spatial_*` as diagnostics only.
- Positive route: a conservative `cluster_mild` run on frozen V1.8 `cascade_relay` + `strict_dual_gate` sources generated 37 candidates and admitted 4 new variants: 2 `cascade_relay` HardPotential and 2 `strict_dual_gate` variants that keep `strictDualGateCandidate=True`.
- Frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV113CascadeStrictClusterReviewPack.asset`, GUID `3d575d3a2da44401807ba42005285ff1`; `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` is mounted to this pack.
- Selected CSV: `root_variant_library_v1_13_cascade_strict_cluster_review_selected.csv`; frozen trace: `root_variant_library_v1_13_cascade_strict_cluster_review_frozen_trace_metrics.csv`; joined trace: `root_variant_library_v1_13_cascade_strict_cluster_mild_joined_trace.csv`.
- Frozen trace result: 7/7 solved, all A-tier: 3 source controls + 4 variants. Use this compact pack for quick perceptual review of whether cluster-remap produces real non-size differences for cascade/strict.

## Root Variant Library V1.14b Consolidated Review Pack - 2026-06-25

- Consolidated current root/variant work from V1.9-V1.13 into a single balanced review library, with geometry-hash dedupe and per-family caps.
- Input pool: 101 rows from `V1.9_non_size_mixed`, `V1.10_hub_support_non_size`, `V1.11_hub_support_size`, `V1.12_other_roots_size`, and `V1.13_cascade_strict_cluster`.
- V1.14 first pass exposed one strict-dual identity drift row (`strictDualGateCandidate=False`) after frozen trace; V1.14b replaced it with the V1.13 strict cluster HardPotential row that preserves strict-dual identity.
- Frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV114bConsolidatedReviewPack.asset`, GUID `375e9f8eb8e6443d938e245fb558e400`; `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` is mounted to this pack.
- Selected CSV: `root_variant_library_v1_14b_consolidated_review_selected.csv`; full input pool: `root_variant_library_v1_14_consolidated_pool_all.csv`; frozen trace: `root_variant_library_v1_14b_consolidated_review_frozen_trace_metrics.csv`; joined trace: `root_variant_library_v1_14b_consolidated_review_frozen_trace_joined.csv`.
- Frozen trace result: 45/45 solved, all A-tier, 36 TrueHardCandidate + 9 HardPotential; avgChoices average `3.458`, avgChoices max `4.03`, maxChoices max `8`, antiLocal range `0.632-0.895`.
- Family distribution: cascade_relay 8, hub_spoke 8, strict_dual_gate 8, support_lock 8, web_crossover 8, split_key 5. Split_key remains the only thin family and should be the next targeted non-size/root-variant workstream.
- Strict-dual audit: all 8 strict_dual_gate rows in V1.14b keep `strictDualGateCandidate=True` after frozen trace.

## Root Variant Library V1.15c Consolidated Review Pack - 2026-06-25

- Continued the active root/variant-library goal by targeting the only thin family in V1.14b: `split_key`.
- Important correction: V1.14b contained one old `split_key` cluster row that current `Build-SplitKeyRootClassifierV1.ps1` marks `splitKeyCandidate=False`; V1.15c removes that false split row before consolidation.
- Negative/diagnostic split routes: `role_zone_swap`, `cluster_strong`, and `spatial_recompose` are too destructive or fail split identity under trace; keep their V1.15 reports as diagnostics, not review content. A wrong `SourceRoot` initially caused false parse failures; future trace for `.worktrees/sgp-rhythm-lab` assets must pass `-SourceRoot F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab`.
- Positive split route: `Build-RootCanvasVariantsV1.ps1` non-mirror `wide30_shift/tall40_shift` from split sources produced 10 trace-valid split candidates. V1.15c admits 4 of them to replace the false split and bring `split_key` to 8 true rows.
- Frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV115cConsolidatedReviewPack.asset`, GUID `7f7307ed6ac9454186e7115eaaba24c4`; `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` is mounted to this pack.
- Selected CSV: `root_variant_library_v1_15c_consolidated_review_selected.csv`; frozen trace: `root_variant_library_v1_15c_consolidated_review_frozen_trace_metrics.csv`; joined trace: `root_variant_library_v1_15c_consolidated_review_frozen_trace_joined.csv`; split gate: `root_variant_library_v1_15c_consolidated_review_frozen_split_gate_split_key_roots.csv`.
- Frozen trace result: 48/48 solved, all A-tier, 40 TrueHardCandidate + 8 HardPotential; avgChoices average `3.485`, avgChoices max `4.03`, maxChoices max `8`, antiLocal range `0.632-0.895`.
- Family distribution is now balanced at 8 each: cascade_relay, hub_spoke, split_key, strict_dual_gate, support_lock, web_crossover. All 8 rows labeled `split_key` have `splitKeyCandidate=True` after frozen trace.

## SGP Fullchain Growth Wave/Sink Boundary - 2026-06-25

- Added opt-in Wave envelope fields to `Build-IncrementalRoleMapFillCompilerV1.ps1`: `-UseWaveConstraintEnvelope` scores candidate chains by dependency touch, guard/body touch, head potential, direct outer risk, and dependency-free head risk; report rows now include `waveClass/waveScore`.
- Small-beam test from Review6 boundary `0.6384804` showed the top Wave candidates are not pure outer exits: anchored `HeadAllowed`, non-direct-outer, `dependencyTouch=3-9`. Manual trace of unique top candidates found 3/7 were `solved=True` and `supportClosureBestDepth=4`, but all were Drop (`maxChoices=12/13`, `followRun=6`).
- Added experimental `-AllowWaveRecoveryWindow`: from `0.6384804` it committed one `WaveRecovery` step to `0.6421569`, solved/depth4, `avgChoices=6.01`, `maxChoices=12`, `followRun=6`, `outerExitHeadCount=6`; this is a bridge proof, not production quality.
- Immediate strict payback from the `0.6421569` recovery sample failed: small beam traced 6 candidates and all were unsolved/depth2. GPT review agreed this means recovery commit alone is not a stable production path.
- Added opt-in Sink-first fields to the compiler: `-UseSinkFirstSelection`, `sinkScore/sinkClass`, and non-increasing choice checks. Sink-first test from the `0.6421569` recovery sample found high sinkScore candidates (`52-116`), but traced 8/8 failed strict acceptance; all were unsolved (some depth4 but no solved). Current conclusion: choice-sink candidates exist as local signals, but the current candidate language cannot turn them into a valid payback step.
- Raw unconstrained SGP baseline from the same `0.6421569` sample (`tonight_raw_sgp_from0642_unconstrained_t095_v1`) confirms geometry capacity is not the bottleneck: 4/4 candidates reached coverage `0.9498-0.9510` with `187-201` total chains, but 4/4 traced as `solved=False`, `Drop`, `LocalEasy`, `supportClosureBestDepth=0`, `avgChoices=10.3-13.75`, `maxChoices=24-27`, `outerExitHeadCount=24-27`.
- Tested existing SGP soft adapter as a candidate preference layer (`tonight_softbias_sgp_from0642_t095_v1`): role-map soft scoring kept coverage high (`0.9498-0.9510`) but did not preserve difficulty (`solved=False`, `LocalEasy`, `supportDepth=0`, `avgChoices=11.52-13.6`, `maxChoices=20-25`, `outerExitHeadCount=22-27`). Conclusion: pure soft scoring/ordering is too weak once SGP enters full-density mop-up; the next middle layer needs a small hard release/motif boundary plus soft scoring, not soft scoring alone.
- Tested simplified Unlock/Release Scaffold SGP from `0.6421569` using `PreActionMaskReserveFullRayOwners=6,26,32,54,87,118` (support closure nodes) plus soft adapter. At target `0.80`, coverage reached `0.800-0.801` and choices were controlled (`avgChoices=4.0-5.24`, `maxChoices=7-8`), but 4/4 were unsolved/LocalEasy with supportDepth `0-1`. At target `0.70`, coverage reached `0.701-0.703`, choices stayed moderate (`avg=5.33-6.11`, `max=9-11`, outerExit=7), but 4/4 were unsolved/supportDepth=0. At target `0.66`, coverage reached `0.664-0.670`, but 4/4 remained unsolved, supportDepth `0-2`. Conclusion: full-ray release scaffold can suppress outer/choice explosion, but the current static reserve form still breaks/rewrites temporal support closure; it is not enough as Unlock Sequence V1.
- Next cut should avoid treating WaveRecovery as production. Either derive sink/choice-compressor candidates before allowing pressure bridge commits, or change candidate generation to produce explicit sink/pressure pairs. Do not simply expand beam size on the same candidate language.

## Root Variant Library V1.16 Size Nonrecursive Review Pack - 2026-06-25

- Continued active root/variant-library goal by taking V1.15c `source_control` plus `non_size_variant` rows only, avoiding recursive size-on-size transforms.
- Generated non-mirror canvas variants with `wide30_shift` and `tall40_shift`: 27 source rows -> 54 candidates.
- Board trace was run in chunks because one-shot 54-level trace exceeded 5 minutes; merged result is 54/54 solved and all A-tier.
- Family identity gates selected 31 reviewable size variants: cascade_relay 2, hub_spoke 1, split_key 4, strict_dual_gate 8, support_lock 8, web_crossover 8.
- Frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV116SizeNonrecursiveReviewPack.asset`, GUID `fe5c4868615449c094b867da86e8417c`; `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` is mounted to this pack.
- Selected CSV: `root_variant_library_v1_16_size_nonrecursive_review_selected.csv`; frozen trace: `root_variant_library_v1_16_size_nonrecursive_review_frozen_trace_metrics.csv`; frozen joined audit: `root_variant_library_v1_16_size_nonrecursive_review_frozen_trace_joined.csv`.
- Frozen trace result: 31/31 solved, all A-tier, 19 TrueHardCandidate + 12 HardPotential; avgChoices average `3.520`, avgChoices max `4.03`, maxChoices max `8`, antiLocal range `0.607-0.895`.
- Current conclusion: size expansion is stable for strict/support/web/split, but cascade and hub preserve identity poorly under simple wide/tall shifts; those families need dedicated non-size/root-preserving variant work rather than more generic canvas shifting.

## Root Variant Library V1.17 Cascade/Hub Review Pack - 2026-06-25

- Followed V1.16 finding that `cascade_relay` and `hub_spoke` preserve identity poorly under generic canvas shifts; targeted those two families with non-size, core-preserving variants.
- Source rows: V1.15c frozen `source_control` plus `non_size_variant` rows for cascade/hub only, saved as `root_variant_library_v1_17_cascade_hub_source_frozen.csv`.
- Route 1 (`peripheral_jitter_v117`): 100 cascade/hub candidates; route 2 (`cluster_remap_v117`): 78 cascade/hub candidates. Trace showed most aggressive moves break solvability or identity; accepted 5 cascade variants.
- Hub-specific second pass used milder jitter/default cluster. Trace admitted 8 hub candidates, but source cap selected 5 to avoid same-parent repetition.
- Frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV117CascadeHubReviewPack.asset`, GUID `e15f131e6a104b1aa3b3fe2093b0206c`; `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` is mounted to this pack.
- Selected CSV: `root_variant_library_v1_17_cascade_hub_review_selected.csv`; frozen trace: `root_variant_library_v1_17_cascade_hub_review_frozen_trace_metrics.csv`; frozen joined audit: `root_variant_library_v1_17_cascade_hub_review_frozen_trace_joined.csv`.
- Frozen trace result: 10/10 solved, all A-tier, 4 TrueHardCandidate + 6 HardPotential; avgChoices average `3.422`, avgChoices max `4.03`, maxChoices max `8`, antiLocal range `0.640-0.750`.
- Current conclusion: cascade can accept a small mix of cluster remap and jitter, while hub-spoke requires very mild peripheral jitter; cluster/default spatial remap tends to destroy hub identity.

## Root Variant Library V1.18 Cascade/Hub Size Review Pack - 2026-06-25

- Continued the active goal by taking V1.17 frozen cascade/hub review rows and applying non-mirror size expansion (`wide30_shift`, `tall40_shift`) to those newly accepted variants.
- Generated 20 candidates from 10 V1.17 frozen sources; board trace result was 20/20 solved (`19 A`, `1 S`), showing the geometry survives size expansion.
- Family identity gates retained 11 candidates: cascade 7 and hub 4. To avoid repetitive same-source output while still preserving the only hub tall pass, final selected pack keeps 9 rows.
- Frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV118CascadeHubSizeReviewPack.asset`, GUID `a72095f2ae7d4b598da21dc7e0856ce8`; `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` is mounted to this pack.
- Selected CSV: `root_variant_library_v1_18_cascade_hub_size_review_selected.csv`; frozen trace: `root_variant_library_v1_18_cascade_hub_size_review_frozen_trace_metrics.csv`; frozen joined audit: `root_variant_library_v1_18_cascade_hub_size_review_frozen_trace_joined.csv`.
- Frozen trace result: 9/9 solved, all A-tier, 3 TrueHardCandidate + 6 HardPotential; avgChoices average `3.363`, avgChoices max `4.03`, maxChoices max `8`, antiLocal range `0.640-0.773`.
- Current conclusion: cascade V1.17 variants size-expand cleanly, especially tall40; hub-spoke size expansion is much narrower, mostly wide30 with only one tall40 pass. Future hub work should prefer horizontal/spacing-preserving changes or new hub roots rather than generic vertical expansion.

## Root Variant Library V1.19 Consolidated Review Pack - 2026-06-25

- Consolidated V1.15c, V1.17, and V1.18 into a broad root/variant review library for the active goal: multiple candidates per confirmed root direction, plus non-mirror size expansions where identity survives.
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_19_consolidated_review_selected.csv`; pool CSV: `root_variant_library_v1_19_consolidated_pool_all.csv`.
- Frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV119ConsolidatedReviewPack.asset`, GUID `7bb6312a6fa34784be320bebdaad03ba`; `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` is mounted to this pack.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_19_consolidated_review_frozen_trace_metrics.csv`; joined audit: `root_variant_library_v1_19_consolidated_review_frozen_trace_joined.csv`.
- Frozen trace result: 52/52 solved, all A-tier, 42 TrueHardCandidate + 10 HardPotential; avgChoices average `3.504`, range `2.57-4.03`; maxChoices max `8`; antiLocal average `0.718`, range `0.632-0.895`.
- Intended family gate after frozen trace: cascade_relay 8/10, hub_spoke 8/10, split_key 8/8, strict_dual_gate 8/8, support_lock 8/8, web_crossover 7/8. The 5 failed identity rows remain useful for visual review but must not be counted as target-family production capacity.
- Current conclusion: V1.19 is a good review/dashboard pack for the root library, not yet a final deduped production pack. The next production cut should either drop or relabel identity-drift rows, then continue creating experiential variants for under-diverse families rather than simple mirror/near-identical shifts.

## Root Variant Library V1.20 Identity-Clean Review Pack - 2026-06-25

- Cut directly from V1.19 by removing the 5 rows whose intended family gate failed after frozen trace. This is the current clean review entry for the active root/variant-library goal.
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_20_identity_clean_review_selected.csv`.
- Frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV120IdentityCleanReviewPack.asset`, GUID `4adcf0892b1b44c4930e971011f750a6`; `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` is mounted to this clean pack.
- Frozen trace was run in 6 chunks due 5-minute one-shot timeout, then merged to `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_20_identity_clean_review_frozen_trace_metrics.csv`.
- Joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_20_identity_clean_review_frozen_trace_joined.csv`.
- Result: 47/47 solved, all A-tier, 37 TrueHardCandidate + 10 HardPotential; avgChoices average `3.536`, range `2.57-4.03`; maxChoices max `8`; antiLocal average `0.720`, range `0.632-0.895`.
- Intended-family gate pass: cascade_relay 8/8, hub_spoke 8/8, split_key 8/8, strict_dual_gate 8/8, support_lock 8/8, web_crossover 7/7. This pack is cleaner for manual review and future production statistics than V1.19.

## Root Variant Library V1.21 Balanced-Clean Review Pack - 2026-06-25

- Balanced V1.20 by adding one trace/family-safe V1.16 web_crossover tall40 non-mirror size variant (`rootvarv116size_24...`), bringing web_crossover back to 8 without using mirror-only variation.
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_21_balanced_clean_review_selected.csv`.
- Frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV121BalancedCleanReviewPack.asset`, GUID `c6e32e5f87e94afba369d42eba62708e`; `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` is mounted to this pack.
- Frozen trace was run in 6 chunks and merged to `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_21_balanced_clean_review_frozen_trace_metrics.csv`.
- Joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_21_balanced_clean_review_frozen_trace_joined.csv`.
- Result: 48/48 solved, all A-tier, 38 TrueHardCandidate + 10 HardPotential; avgChoices average `3.539`, range `2.57-4.03`; maxChoices max `8`; antiLocal average `0.719`, range `0.632-0.895`.
- Intended-family gate pass is balanced and clean: cascade_relay 8/8, hub_spoke 8/8, split_key 8/8, strict_dual_gate 8/8, support_lock 8/8, web_crossover 8/8. Treat V1.21 as the current best review pack/root-variant-library baseline.

## Root Variant Library V1.22 Size-Nonrecursive Review Pack - 2026-06-25

- Continued the active root/variant-library goal by applying non-mirror size expansion to V1.21 `source_control` + `non_size_variant` rows only; size-on-size recursion was deliberately excluded.
- Source rows: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_22_size_source_nonrecursive.csv` (27 rows: cascade 5, hub 6, split 2, strict 5, support 5, web 4).
- Generated candidates: `root_variant_library_v1_22_size_nonrecursive_candidates.csv` (54 candidates = `wide30_shift` + `tall40_shift`).
- Candidate trace/gate result: 54/54 traced, all solved; 38 survived quality + intended-family gate. Hub is the narrowest family under generic wide/tall shifts (3/12 pass), while strict/support/web are stable and cascade is mostly stable.
- Frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV122SizeNonrecursiveReviewPack.asset`, GUID `ea936cfad65d418aa03d3b6c24855c0e`; `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` is mounted to this pack.
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_22_size_nonrecursive_review_selected.csv`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_22_size_nonrecursive_review_frozen_trace_metrics.csv`; joined audit: `root_variant_library_v1_22_size_nonrecursive_review_frozen_trace_joined.csv`.
- Frozen trace result: 38/38 solved, 37 A + 1 S, 22 TrueHardCandidate + 16 HardPotential; avgChoices average `3.515`, range `2.57-4.03`; maxChoices max `8`; antiLocal average `0.696`, range `0.591-0.895`.
- Intended-family gate pass after frozen trace: cascade_relay 7/7, hub_spoke 3/3, split_key 4/4, strict_dual_gate 8/8, support_lock 8/8, web_crossover 8/8. Treat this as the current size-expansion review pack; do not infer that generic size expansion is sufficient for hub-spoke.

## Small-Canvas Physical Pin / SGP Fill Probe - 2026-06-25

- Goal: validate the user-proposed route “先打骨架钉子/门锁，再让 SGP 填”，before attempting full near-1 coverage.
- Negative setup: using the high-density `0.6421569` fullchain boundary sample as a 18x24 compact parent is invalid for this test. Compacting it kept 71 chains at coverage `0.7222` and produced 6/6 `solved=False`, `supportClosureBestDepth=0`; this tests compression damage, not pin strategy.
- Base scan: compacted V1.15c root library candidates to 18x24 and 20x26 with no fill. 18x24 was too destructive (only 2/48 solved and max support depth 2). 20x26 produced usable thin parents; best test parent was `tonight_compact_rootlib_20x26_baseonly_scan48_b04_c01`, coverage `0.2558`, `solved=True`, `processTier=S`, `supportClosureBestDepth=4`.
- Physical outer-frame pins + SGP on that 20x26 parent:
  - No frame, max44: 8/8 solved but all Drop; avg coverage `0.482`, avgChoices `7.07`, avg maxChoices `17.5`, avg outerExitHeadCount `15.62`.
  - Outer frame 4, max44: 8/8 solved, all Drop; avg coverage `0.504`, avgChoices `6.00`, avg maxChoices `13.12`, avg outerExitHeadCount `13.88`.
  - Outer frame 8, max44: 8/8 solved, 2 A + 2 B + 4 Drop; avg coverage `0.498`, avgChoices `5.57`, avg maxChoices `11.88`, avg openers `8.5`, avg outerExitHeadCount `10.62`, depth4 in 7/8.
  - Outer frame 8, max50: 8/8 solved but all Drop/LocalEasy; avg coverage `0.565`, avgChoices `6.66`, avg maxChoices `13.88`, avg outerExitHeadCount `16.12`.
- Control result: adding `RequireHeadHitsExisting` with the same 20x26 parent and outer frame was worse (1/8 solved, all Drop/LocalEasy, support depth mostly 0-2). A generic head-hit restriction is not a usable pin policy.
- Current conclusion: pre-generating physical structures can keep solvability and support-lock alive better than pure SGP, but generic outer-frame pins are still exit buffers, not true gate pins. The next cut should generate trace-derived gate pins from the parent’s unlock/escape structure, then hand the remaining constrained space to SGP. Do not continue tuning universal outer frames or head-hit filters as the main route.
- Trace-derived RayBreaker pin probe:
  - Generated RayConstraintMap for `tonight_compact_rootlib_20x26_baseonly_scan48_b04_c01`: 7 direct-exit rays, 55 CriticalTimingZone cells, 248 HeadAllowed cells, 0 GuardSlot/BodyOnly cells. Direct ray cells were all CriticalTimingZone, so RayFirst required `AllowCriticalTiming`.
  - `tonight_tracepin_raybreaker_b04_20x26_critical_smoke24`: allowing CriticalTiming anchors generated 24 one-chain pin candidates; 16/24 solved, 14 S + 2 A, 11/24 preserved supportDepth>=4, 21/24 had `outerExitHeadCount=0`, all had `outerExitHeadCount<=1`. Best examples include c09/c16 with coverage `~0.264`, solved A, supportDepth=4, outer=0.
  - Follow-up `pin -> SGP max44` (`tonight_pinthen_sgp_b04_20x26_max44_smoke16`) shows one physical pin alone is not enough: 15/16 solved and 12/16 depth4, but all Drop; avg coverage `0.479`, avgChoices `6.62`, avg maxChoices `16.5`, avg openers `16.31`, outer exits returned to `~15-16`.
- Updated conclusion: trace-derived RayBreaker pins are valid local structures, but they must become a constraint field for SGP, not just one preseeded chain. Otherwise SGP overwrites the pin’s pressure by creating new outer exits elsewhere. Next probe should pass pin/ray ownership into SGP candidate selection: reserve or heavily penalize heads that create new direct-exit rays outside the selected pin release lanes, while allowing SGP body fill around those lanes.

## Pin-Debt Pair Probe - 2026-06-25

- Added independent experimental script `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PinDebtSGPFillProbeV1.ps1`; it emits paired candidates from the same parent and same generated chain `C`: `cOnly` = parent + C, `cPin` = parent + C + a generated ray-blocking Pin.
- Strict pair probe on `tonight_compact_rootlib_20x26_baseonly_scan48_b04_c01`: `tonight_pindebt_pairprobe_b04_20x26_strict_smoke16_v2`, 16 pairs / 32 assets, trace missing=0.
- Result: `cOnly` was 16/16 solved and 16/16 S/A, supportDepth=4 for all, avgChoices average `3.202`, maxChoices average `7.875`. `cPin` reduced choices/openers (avgChoices `2.978`, maxChoices `6.562`) but only 5/16 solved S/A; 11/16 became Drop and supportDepth often collapsed to 0.
- Important nuance: script-level “direct head ray” did not correspond to trace `outerExitHeadCount` in this sample (`outerExitHeadCount=0` for both variants). The pin often solved a geometric ray symptom that was not trace-visible outer pressure, then cut the temporal support/release structure.
- Current conclusion: “new direct ray creates immediate pin debt” is not safe as an automatic rule. It is promising only as `generate pin alternatives -> core/release gate -> commit one composite candidate`; otherwise use soft penalty or leave the chain unpinned. Next iteration should generate multiple Pin alternatives per C and require support/release preservation before accepting the composite.

## PinField V2 Batch Constraint Probe - 2026-06-25

- Added experimental compiler `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-PinFieldRoleMapV2.ps1`; it transforms RayConstraintMap cells into an adapter role-map so SGP can run in a precompiled field instead of per-chain Pin validation.
- Test parent remained `tonight_compact_rootlib_20x26_baseonly_scan48_b04_c01` on 20x26. Generated `tonight_pinfield_v2_b04_halo1_directcritical_body_cells.csv`: 93 body-only cells, 224 HeadAllowed, 46 HighRiskFreeHead, 133 occupied.
- Hard PinField runs (`direct+critical+halo body-only`, `direct-only body-only`, `reserved ray cells with PenalizeBody`) all controlled choices/outer exits but failed solvability:
  - `tonight_pinfield_v2_b04_t070_max60_smoke8`: 0/8 solved, avg coverage `0.495`, avgChoices `2.83`, outerExitHeadCount `0`, supportAvg `0.25`.
  - `tonight_pinfield_v2_b04_directonly_t070_max60_smoke8`: 0/8 solved, avg coverage `0.483`, avgChoices `2.67`, outer `0`, supportAvg `0.5`.
  - `tonight_pinfield_v2_b04_reservedpenalty_t070_max60_smoke8`: 0/8 solved, avg coverage `0.499`, avgChoices `2.73`, outer `0`, supportAvg `0`.
  - Low-pressure reserved penalty at target `0.50/max44` was also 0/8 solved at avg coverage `0.469`, while prior no-frame max44 was 8/8 solved and outer-frame max44 was 8/8 solved with 2 A.
- Soft PinField (`no pre-action mask`) restored coverage but collapsed difficulty/outer pressure: `tonight_pinfield_v2_b04_softfield_t070_max60_smoke8` reached avg coverage `0.634` and 2/8 solved, but all Drop, avgChoices `14.33`, avg maxChoices `28.63`, avg outerExitHeadCount `28.25`.
- Current conclusion: current adapter role-map can express “forbid head / bias body”, but it cannot yet express “preserve temporal release lane”. Hard field kills release; soft field lets SGP reopen outer exits. Next viable route must encode release-lane ownership/order, not just cell role. Do not treat `outer=0 + low choice` as success unless solved/supportDepth survive.

## Lane Compiler V0 / Shuffle Test - 2026-06-25

- Added `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-LaneConstraintFieldV0.ps1`; it projects top direct critical rays into lane rows and emits soft role-map, lane-edge reserve CSV, and shuffled reserve CSV for the GPT-recommended lane-shuffle test.
- Test parent: same 20x26 `b04` support-lock parent. V0 selected top 4 direct critical lanes: owners `0,9,10,11`, each with 3 protected cells.
- Batch SGP comparison at target `0.70/max60`, 6 candidates each:
  - Raw SGP: avg coverage `0.670`, 2/6 solved but 0/6 S/A, support4=0, avgChoices `12.68`, avg maxChoices `27`, avg outer `24.5`.
  - Soft lane: avg coverage `0.662`, 1/6 solved, 0/6 S/A, support4=0, avgChoices `13.44`, avg maxChoices `28`, avg outer `27.67`.
  - Hard lane head-only: avg coverage `0.495`, 0/6 solved, support4=0, avgChoices `2.60`, avg maxChoices `5.5`, outer `0`.
  - Shuffled hard head-only: avg coverage `0.491`, 0/6 solved, support4=0, avgChoices `2.54`, avg maxChoices `5.67`, outer `0`.
- Shuffle test result: true lane hard-head-only and shuffled hard-head-only were effectively equivalent. This means current Lane V0 did not encode meaningful release-lane semantics; it only reduced head freedom globally/locally. Soft lane also did not improve over raw. Next step should not keep tuning owner lists or head-only masks; it needs a different interface where SGP candidates are generated to attach to/continue a lane, not merely avoid lane cells as heads.

## Trace-Bound SGP Probe V0 - 2026-06-25

- Added `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-TraceBoundSGPProbeV0.ps1`; it keeps SGP-style random-walk body generation, but constrains each new chain head so its first-hit ray targets an existing trace edge `firstHitOwner`.
- Test parent remained `tonight_compact_rootlib_20x26_baseonly_scan48_b04_c01` (20x26, base coverage `0.2558`, solved/S, supportDepth=4).
- Smoke results:
  - `tonight_tracebound_v0_b04_add02_smoke8`: avg coverage `0.2726`; 1/8 solved/S/supportDepth=4; choices/outer stayed low (`avgChoices≈3.0`, `outer=0`) but most samples broke support.
  - `tonight_tracebound_v0_b04_add04_smoke8`: avg coverage `0.2916`; 0/8 solved, support4=0; choices/outer stayed low.
  - `tonight_tracebound_v0_b04_add12_smoke8`: avg coverage `0.3584`; 0/8 solved, support4=0; choices/outer stayed low.
  - V0b `MaxHitsPerTargetOwner=1` owner de-dup (`add04/add08`) still produced 0/8 solved/support4, so failure is not only repeated pressure on one owner.
- Current conclusion: binding new chains to trace `firstHitOwner` is stronger than geometry masks for suppressing outer exits, but still lacks release-wave/order semantics. It can produce a low-dose valid sample, yet batch insertion quickly collapses temporal support. Next probe must bind candidate generation to owner ancestry/release wave or a trace-edge continuation mode, not just first-hit owner.

## Trace-Edge Contract V1 Smoke - 2026-06-25

- GPT/Rosetta review agreed with the local conclusion: stop tuning Pin/Lane masks; the next correct test is a minimal trace-edge contract. GPT recommended keeping it small: edge id/parent anchor/release wave/dependency lock mask, then smoke `+2/+4/+8`.
- Extended `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-TraceBoundSGPProbeV0.ps1` with opt-in `-UseTraceEdgeContract`: new chain heads must be inserted on the original dependency edge ray corridor, so the chain becomes an intermediate node in `edgeOwner -> firstHitOwner` rather than randomly hitting the same owner elsewhere.
- V1 result on the same 20x26 b04 parent:
  - plain trace-edge contract `add04`: 1/8 solved, 0/8 supportDepth>=4; better attachment semantics than V0 but still cliff.
  - wave-filtered V1b (`MinHitWave=1`, `MaxReleaseGap=4`) `add04`: 3/8 solved, 2/8 supportDepth>=4, outer=0, avgChoices about `2.62`. This is a real improvement and confirms release-wave filtering matters.
  - narrower V1c (`MinHitWave=2`, `MaxReleaseGap=2`) kept 3/8 solved but 0 supportDepth>=4, so over-narrowing wave windows loses the hard motif.
  - body lock-mask V1d (`ProtectCriticalRayBodyCells`) timed out even at `add03`; the mask is too restrictive for current SGP search language and should not be treated as production-ready.
- Current conclusion: trace-edge corridor continuation + moderate release-wave filtering is the first semantic interface that improves over firstHitOwner-only. It is not yet production because `add04` still only preserves support in 2/8 cases. Next cut should keep V1b as the baseline and improve body generation/edge scheduling, not hard forbid all critical ray body cells.

## Trace-Edge Contract V1e Edge Scheduling - 2026-06-25

- Extended `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-TraceBoundSGPProbeV0.ps1` with:
  - `-EdgeSchedulePreset WaveBridgeV1`: choose a small wave-distributed edge plan instead of random eligible edges.
  - `-SkipFailedScheduledEdges`: skip an edge that cannot generate geometry rather than stalling the whole candidate.
  - `-AvoidCriticalRayBodyCells`: soft-prefer bodies outside critical ray cells, without hard-forbidding them.
- V1e smoke on the same b04 parent:
  - `tbv1e_schedavoidskip4_smoke12`: 12 candidates, avg added chains `2.58`, avg coverage `0.2769`; 10/12 solved S/A and 10/12 supportDepth=4, outer=0, avgChoices `2.755`.
  - This is a large support-preservation improvement over V1b add04 (2/8 supportDepth=4), but it adds fewer chains than requested because some scheduled edges cannot be geometrically realized by the current SGP body sampler.
  - Longer-body probe `tbv1e_schedavoid3_len8_smoke12` reached max coverage `0.2942`, but the highest-coverage samples collapsed; stable samples stayed around 2-3 chains. Longer bodies are not a free coverage win.
- Current conclusion: edge scheduling + soft body avoidance is the most stable trace-semantic interface so far, but current candidate language is still low-throughput. Next work should expand the set of geometrically realizable trace-edge insertions or generate small edge groups/pairs, rather than increasing body length or hardening masks.

## Migration-Aware Acceptance Probe - 2026-06-25

- Added `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Select-MigrationAwareTraceCandidatesV1.ps1`; it joins candidate CSV + board trace and classifies rows as `SupportPreserved`, `HardMotifMigrated`, `SupportRootMigrated`, or `Reject`.
- Acceptance rule used for this probe: solved, choices/outer within limit, and either old supportDepth preserved (`>=4`) or a new hard motif appears via `hardStructureV3Class in {TrueHardCandidate, HardPotential}` or migrated support root (`supportDepth>=3`, root changed).
- Applied to current trace-edge batches:
  - V1b add04: 2 selected, both `SupportPreserved`.
  - V1e scheduled/soft-avoid 12: 10 selected, all `SupportPreserved`.
  - V1e longer-body 12: 9 selected, all `SupportPreserved`.
- Scan of recent trace-bound/trace-edge metrics found no `TrueHardCandidate` or `HardPotential` rows. Current candidate language has not yet produced a true migration case; allowing migration does not rescue the current rejects.
- Current conclusion: migration-aware acceptance is the right gate shape, but the current trace-edge scheduled generator is still a support-preserving micro-fill language, not a motif-migration generator. Next work should explicitly generate candidates that can create a new trace-visible motif or broaden edge groups, rather than merely relaxing supportDepth.

## Root Variant Library V1.23-V1.27 Hub/Split Tonight Pass - 2026-06-25

- Continued active root/variant-library goal by targeting families that generic size expansion under-served: `hub_spoke` and `split_key`.
- V1.23 hub conservative variants: from V1.21 frozen hub rows, generated 95 candidates (`jitter=64`, `cluster=23`, `zone=8`). Frozen review pack keeps 6 candidates; result `6/6 solved`, `6/6 hubSpokeCandidate`, all A-tier. Pack GUID `a4f8f06e9b88473abf3ed4e58e416723`.
- V1.24 hub conservative size: size-expanded V1.23 with non-mirror wide/tall shifts. Frozen review pack keeps 4 candidates; result `4/4 solved`, `4/4 hubSpokeCandidate`, all A-tier. Pack GUID `d375fa699a2846f6a20c7a76c12dcc7e`.
- V1.25 split conservative variants: from V1.21 frozen split rows, generated 105 candidates; only peripheral jitter preserved split-key identity reliably. Frozen review pack keeps 4 source-capped candidates; result `4/4 solved`, `4/4 splitKeyCandidate`, all A-tier. Pack GUID `bc9bff8f450149488a34f9a1347a7c60`.
- V1.26 split conservative size: added dynamic canvas presets `wideplus6_shift`/`tallplus6_shift` because fixed `wide30_shift` can overflow already-wide sources. Frozen review pack keeps 7 candidates; result `7/7 solved`, `7/7 splitKeyCandidate`, all A-tier. Pack GUID `e6ec4727c8524d9ca53d4aacb3295f98`.
- V1.27 consolidated tonight review combines V1.23-V1.26 into 21 levels: hub root 6, hub size 4, split root 4, split size 7. Frozen trace: `21/21 solved`, `21/21 intendedFamilyGatePass`; Demo in `.worktrees/sgp-rhythm-lab` is mounted to V1.27. Pack GUID `f73fdf8cefb84ff1aa5571accdd4ccf5`.
- Engineering conclusion: hub needs conservative identity-preserving jitter/cluster before size expansion; split-key is highly sensitive and should use non-core jitter only, then dynamic size expansion. Role-zone swap is too destructive for these two families in the current form.
## Campaign500 Hardening V13BDR2 Hybrid - 2026-06-25

- Synced the V12BDR result/next-step question to GPT conversation `6a3be215-05c8-83e8-b5c9-307a492fea69`; GPT advised `two-cell inward hook / endpoint inset` as the next mainline, with double-end stitch deferred and endpoint merge kept as low-frequency auxiliary.
- Implemented V13BDR2 in `Assets/ArrowMagic/Editor/CampaignHardeningAnalyzer.cs`: menu `Tools/Arrow Magic/Campaign 500/Hardening/Build Boundary Inset Sandbox V13`, pack `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV13BDR2Pack.asset`, GUID `900364ecf4764fe49beacb4d41643f3b`.
- V13 now starts from latest V11 outputs, applies two-cell boundary inset first, then one-cell V12 hook as fallback. This is a replacement/stronger branch from V11, not an incremental edit after V12BDR.
- Latest report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v13_bdr2_20260625_115819.csv`.
- Accepted demo pairs: L387 and L405. L387: direct/PBE `18->12`, opening `18->12`, NEE `33->30`; L405: direct/PBE `10->8`, opening `10->8`, NEE `30->21`. L173 was skipped because PBE only dropped `9->8`, below the 15% gate.
- Validation: `dotnet build ArrowLevel-Hand.sln` passed with existing warnings only; Unity batch generation passed and `Assets/ArrowMagic/Scenes/Demo.unity` activePack points to V13BDR2.

## Campaign500 Hardening V14CMP Edge Compression - 2026-06-25

- Synced V13BDR2 results and next plan to GPT conversation `6a3be215-05c8-83e8-b5c9-307a492fea69`; GPT agreed that V14 should be a low-frequency endpoint merge/compression pass, strictly limited to boundary redundancy cleanup and not dependency-topology rewriting.
- Implemented V14CMP in `Assets/ArrowMagic/Editor/CampaignHardeningAnalyzer.cs`: menu `Tools/Arrow Magic/Campaign 500/Hardening/Build Boundary Compression Sandbox V14`, pack `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV14CMPPack.asset`, GUID `9d95355a84e2c6643a7adc5765763940`.
- Latest report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v14_cmp_20260625_121057.csv`.
- Result: L405 accepted with 2 edge-merge ops: chains `167->165`, direct/PBE `8->6`, opening `8->6`, NEE `21->21`, arrow tiles unchanged `1044`, edge-short outer `0->0`, boundary-straight `1->1`. L387 had no safe compression candidate and was skipped.
- Validation: `dotnet build ArrowLevel-Hand.sln` passed with existing warnings only; Unity batch generation passed and `Assets/ArrowMagic/Scenes/Demo.unity` activePack now points to V14CMP for review.

## Root Variant Library V1.28 Balanced Production Library - 2026-06-25

- Continued active root/variant-library goal by consolidating stable rows from V1.21 baseline, V1.22 size expansion, and V1.27 hub/split conservative passes into a balanced six-family library.
- Frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV128BalancedProductionLibraryPack.asset`, GUID `c81950ad0c0d43ac9dd91715c9d78ef7`; frozen levels in `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV128BalancedProductionFrozen`.
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_28_balanced_production_library_selected.csv`; frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV128BalancedProductionLibraryPack.csv`.
- Frozen trace was run in six 12-row chunks and merged to `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_28_balanced_production_frozen_trace_metrics.csv`; joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_28_balanced_production_frozen_trace_joined.csv`.
- Result: 72/72 solved and 72/72 intended family gate pass. Distribution is 12 each for `cascade_relay`, `hub_spoke`, `split_key`, `strict_dual_gate`, `support_lock`, and `web_crossover`. Family maxChoices caps stayed <=8; family avgChoices averages stayed roughly `3.25-3.82`; min support depth is >=3 except `split_key` where family classifier permits depth 2 with split-key gate pass.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack now points to V1.28. Treat this as the current balanced root/variant review library and the baseline for next root-direction and size-expansion work.

## Root Variant Library V1.29-V1.30 Four-Family Expansion - 2026-06-25

- Continued the active root/variant-library goal after V1.28 by targeting the four families that still mostly depended on V1.21 baseline + V1.22 size rows: `cascade_relay`, `strict_dual_gate`, `support_lock`, and `web_crossover`.
- V1.29 source feed: 16 V1.28 frozen parents, 4 per target family, from `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_29_expansion_source_four_families.csv`.
- V1.29 candidate generation: `peripheral_jitter` generated 96 candidates; `cluster_remap` generated 48 candidates with 80 geometry failures; web-specific mild jitter generated 48 more candidates after strong web perturbation proved fragile. Combined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_29_fourfam_all_joined_trace_gate.csv`.
- V1.29 frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV129FourFamReviewPack.asset`, GUID `9f604bc426124186bab65ebaf9ccd478`. Frozen result: 16/16 solved and 16/16 intended family gate pass. Distribution: 4 each for cascade/strict/support/web; operators are cascade `cluster_remap:4`, strict `cluster_remap:2 + peripheral_jitter:2`, support `cluster_remap:2 + peripheral_jitter:2`, web `web_mild_jitter:4`.
- V1.30 size expansion: expanded V1.29 frozen rows with non-mirror `wideplus6_shift` and `tallplus6_shift`; 32 candidates traced, 25 intended-family gate pass, 16 selected.
- V1.30 frozen pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV130FourFamSizeReviewPack.asset`, GUID `0e14f4e499e54d089e0a92a1a81e7b27`. Frozen result: 16/16 solved and 16/16 intended family gate pass; 4 per target family. `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack now points to V1.30 for review.
- Engineering conclusion: cluster remap is useful for cascade and partially useful for strict/support, but too destructive for web; web_crossover currently needs mild non-core jitter or size-level variation. Cascade size expansion is also more fragile in wide direction than tall direction.

## Root Variant Library V1.31 Extended Balanced Review - 2026-06-25

- Consolidated the validated library into a larger balanced review pack for the active goal: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV131ExtendedBalancedReviewPack.asset`, GUID `91a29088725441d3b604fa2e66f8d71e`.
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_31_extended_balanced_review_selected.csv`; frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV131ExtendedBalancedReviewPack.csv`; frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV131ExtendedBalancedFrozen`.
- Composition: 108 levels, exactly 18 each for `cascade_relay`, `hub_spoke`, `split_key`, `strict_dual_gate`, `support_lock`, and `web_crossover`.
- Sources: V1.28 contributes 12 baseline rows per family; V1.29 contributes 3 non-size rows for cascade/strict/support/web; V1.30 contributes 3 size rows for cascade/strict/support/web; V1.27 contributes 6 extra hub and 6 extra split rows.
- Frozen trace was run in six 18-row chunks and merged to `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_31_extended_balanced_frozen_trace_metrics.csv`; joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_31_extended_balanced_frozen_trace_joined.csv`.
- Verification: 108/108 solved and 108/108 intended family gate pass. Family maxChoices max stayed <=8; family avgChoices averages range `3.16-3.73`; min antiLocal ranges `0.579-0.667` by family; split_key min support depth remains 2 because the split-key classifier permits that family shape.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack now points to V1.31. Treat V1.31 as the current stable root/variant review library for manual visual curation and future量产 sampling.

## SGP Pressure Hard Trial - 2026-06-25

- In main project `F:\Unityproject\ArrowLevel-Hand`, added experimental direct SGP pressure-hard entry in `Assets/ArrowMagic/Editor/NoMaskProceduralGenerator.cs`.
- Trial pack path: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardTrialPack.asset`; report: `Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_hard_trial_report.csv`.
- Result: old fallback produced solvable but easy-looking 198-228 chain samples; first traced sample was `Drop/LocalEasy` with `avgChoices=11.16`, `maxChoices=18`, `outerExitHeadCount=16`.
- Pure pressure profile reduced chain count to 131-158 at high coverage, but 4/4 failed Greedy. Conclusion: hardening SGP by simply favoring blocked heads creates deadlocks; generation needs temporal release/order constraints, not just fewer clear rays.
- To avoid leaving Demo empty, copied 3 validated SGP hard-library benchmark levels into `Assets/ArrowMagic/SOData/Levels/DirectProcedural/SGPPressureHardBenchmark/` and built `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardBenchmarkPack.asset` (GUID `c8e516eece57cc94ca87c60d18b5b0d3`).
- Demo activePack now points to `SGPPressureHardBenchmarkPack`. Benchmark trace in `.codex-run/sgp_pressure_benchmark_trace/.../sgp_pressure_hard_benchmark_metrics.csv`: 3/3 solved A-tier; openers `3-5`, avg choices `2.57-2.98`, max `5-6`, outer exits `0/1`.
- Follow-up v16 trial changed the direct SGP experiment to small near-full long-chain boards (`20x28` to `22x28`/`21x30`, target `55-60` chains), added a release-only pressure profile, a best-candidate flip-gate pass, and cheap early-choice scoring in `NoMaskProceduralGenerator.cs`.
- Current trial output: `SGPPressureHardTrialPack.asset` (GUID `acd1590a350614a4e86c901d33b5c5dd`) with 4/4 solved B-tier trace rows in `.codex-run/sgp_pressure_trace_v16_scorecurve/.../sgp_pressure_hard_trial_v16_scorecurve_metrics.csv`.
- v16 metrics: chains `57-63`, openers `3-5`, avg choices `5.26-6.64`, max choices `8-12`, outerExitHeadCount `1-5`, stageLockScore up to `0.641`; Demo activePack now points to the trial pack.
- Current conclusion: direct SGP can improve when difficulty is scored during candidate selection and the board is sized to 50-60 long chains, but it still lacks benchmark-level support closure/root structure (`HardStructureV3Class` remains `LocalEasy`). Next step should be root/trace-edge semantic generation, not more random blocked-head pressure.

## SGP -> Re-anchor -> SGP Sandwich Probe - 2026-06-25

- Tested the proposed sandwich line: let SGP raise density first, then use existing skeleton/trace tools to re-anchor difficulty before giving control back to SGP.
- Middle-state sample: `tonight_pinthen_sgp_b04_20x26_max44_smoke16_b01_c05`, coverage `0.4807692`, solved, supportDepth `4`, but Drop due choices (`avgChoices=7.11`, `maxChoices=18`, `outerExitHeadCount=15`).
- Ray map output: `sandwich_sgp048_c05_raymap_v1_*`; it found 25 `criticalDependencyEdge`, 17 `directExitRay`, and confirmed the failure is choice/outer explosion rather than lack of hard motif.
- Trace-edge re-anchor test `sandwich_sgp048_c05_reanchor4_v1`: only added `0-1` chains per candidate and did not materially improve choices (`avgChoices≈7`, `maxChoices=18`, all Drop). Critical-edge micro anchors are too weak at this dense middle state.
- Outer-head subset repair `sandwich_outerhead_c05_subset2_v1`: flipping 2 outer heads reduced best avg choice to `6.27` and outerExitHead to `13` while preserving supportDepth `4`, but all rows still Drop and some became unsolved.
- Extreme all-outer-head flip `sandwich_outerhead_c05_all_v1`: `avgChoices=2`, `outerExitHead=1`, but unsolved and supportDepth `0`. Outer exits are the correct pressure surface, but geometry-level flip/repair is too destructive.
- Current conclusion: sandwich route remains plausible, but the re-anchor pass must operate as SGP generation-time outer-exit budget + release-owner/trace contract, not post-hoc geometry flips or small critical-edge additions.

## Wave Peel Re-root Probe V1 - 2026-06-25

- Added `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-WavePeelRerootProbeV1.ps1`.
- User correction captured: do not touch/clear the difficulty skeleton. Script now protects `baseChains` parent owners and trace `supportClosureQualifiedRoots/supportClosureBestRoot` owners before rewriting any opener wave.
- Negative control: `ReverseEligibleExceptKeep` on c05 compressed choices but made 24/24 unsolved when keep count was 3-6; wider keep 8/10/12/14 only solved a few rows and still stayed Drop. Reversing opener chains is not root refill.
- Positive control: `DropEligibleExceptKeep` on c05 removed only non-skeleton opener chains. Result: 16/16 solved, mostly A; k0 rows returned to coverage `0.2635`, `avgChoices=2.93`, `maxChoices=7`, supportDepth=4. Useful k2/k4 rows stayed coverage `0.28-0.3577`, A, supportDepth=4.
- Cycle test: selected `wavepeel_drop_c05_protect_v1_b01_c11_k4` (`coverage=0.3577`, A) and gave it back to SGP. `sandwich_cycle1_sgp_from_k4_v1` filled to coverage `0.5096-0.5596`, all solved/supportDepth=4, but all Drop again with openers `14-17`, avgChoices `6.21-8.30`.
- Second peel of best cycle row `sandwich_cycle1_sgp_from_k4_v1_b01_c04` again recovered A rows: k0 coverage `0.3577`, `avgChoices=3.44`, supportDepth=4; k2/k4 rows coverage up to `0.45`, A; k6 rows returned to Drop.
- Current conclusion: SGP repeatedly creates removable non-skeleton opener waves; peeling those waves is a stable way to restore difficulty without killing support. It is not yet a full production solution because coverage drops. The next required primitive is root/gate refill of the peeled cells, not reversal and not leaving holes.

## Wave Peel K4 Refill Probes - 2026-06-25

- Tested refill routes from `wavepeel_drop_c05_protect_v1_b01_c11_k4` (`coverage=0.3577`, solved/A, supportDepth=4) using protected-chain ray map `wavepeel_k4_raymap_p28_v1_*` (`ProtectedChainCount=28`).
- `Build-TraceBoundSGPProbeV0` with `UseTraceEdgeContract + WaveBridgeV1 + AvoidCriticalRayBodyCells` preserved difficulty perfectly but throughput was too low: 12/12 A/supportDepth=4, yet only `0-1` chain added, best coverage `0.3692`.
- `Build-HardLockSlotDirectedBatchFillV1` could add a 3-chain group to coverage `0.4000` while keeping solved/A/supportDepth=4, but the best row was still weak (`LocalEasy`, antiLocal `0.393`, `outerExitAvailableChoiceMax=4`) and rejected by outer pressure.
- `Build-SeededDirectSGPFillBaselineV1` with `PreseedReleaseScaffold` (6 chains) and then with `UseConstraintAdapter + soft budget` both filled to about `0.49-0.56`, but 8/8 Drop/unsolved or LocalEasy with avg choices around `8-10`. Preseed/soft preferences do not stop SGP from recreating open outer/opener waves.
- `Build-IncrementalRoleMapFillCompilerV1` from the same k4 parent produced three strict commits and stopped at coverage `0.3692`; all accepted commits were 2-cell safe additions (`supportDepth=4`, `avgChoices=3.39/4.06/3.94`, `maxChoices=8`).
- Current conclusion: safe refill candidates exist, but current trace-edge/local-room candidate language is too narrow. The missing primitive is not reversal, not scaffold-only SGP, and not soft adapter; it is a higher-throughput release-compatible root/gate scaffold generator that can fill the peeled opener cells as groups while preserving the protected skeleton.

## Wave Peel Release Scaffold Group V0 - 2026-06-25

- Added `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-WavePeelReleaseScaffoldGroupV0.ps1`. It computes `peeledCells = used(full SGP middle state) - used(peel base)` and only generates new short chains inside that peeled non-skeleton space; each new head must first-hit a protected skeleton owner (`ProtectedChainCount=28` in the k4 probe).
- Initial k4 group smoke from `wavepeel_drop_c05_protect_v1_b01_c11_k4`: `TargetAddedChains=6` generated 12/12 candidates at coverage `0.398-0.413`, but 12/12 were unsolved/Drop. Throughput exists, but blind 6-chain owner-hit groups break temporal release.
- Group-size sweep: `add2` produced 2/12 solved A/supportDepth=4 up to coverage `0.3769`; `add3` produced 1/12; `add4/add6` produced no acceptable rows. This proves the peeled-space owner-hit language is not useless, but group size must be release-aware.
- Iteration test from an accepted `add2` row reached a second solved A/supportDepth=4 step at coverage `0.3923` (4/16 acceptable rows). A third random `add2` step failed 0/20; a `TargetAddedChains=1` probe found one A/supportDepth=4 row at `0.3981`, then another single step reached solved/supportDepth=4 at `0.4038` but only tier B (`localPatchSolveRunMax=5`, `outerExitAvailableChoiceMax=4`).
- Script now supports `-CountExistingProtectedOwnerHits` to count existing non-skeleton protected-owner hits before adding more; this ruled out simple repeated-owner overuse as the only failure cause.
- Current conclusion: V0 is a meaningful positive-control over trace-edge micro-fill because it can refill peeled cells in small groups and iterate to ~0.39 A-tier, but it is not yet production. The next primitive must schedule refill by `release wave + owner role`, not just "head hits any protected owner".

## Active Loop To Validate Next - 2026-06-25

- Preserve this loop across context compression: `validated skeleton/root -> SGP density fill -> trace detects explosion -> protect skeleton owners -> peel only SGP-created non-skeleton opener waves -> refill peeled cells with root/gate/blocker chains -> return to SGP`.
- The immediate next run should execute the loop once end-to-end, even if the current blocker refill is still a V0 placeholder. The value is to measure where the loop breaks: SGP growth, peel recovery, blocker refill, or second SGP pass.
- Do not use reversal as the refill. Do not clear skeleton/support owners during peel. Do not present owner-hit V0 as final production; use it only to approximate the blocker/root refill step until owner/wave scheduling is implemented.

## SGP Pressure Hard Anti-Local Follow-up - 2026-06-25

- In main project `F:\Unityproject\ArrowLevel-Hand`, pressure-hard direct SGP was extended with early choice-curve diagnostics: avg/max choices, local run, jump distance, near-step rate, plus trace-style local patch stats based on chain bbox/center/micro-region.
- Pressure profile was changed from high-turn snake to straighter mixed chains, and `EdgeHeadChains` for pressure now uses the trace-like near-outer outward-head口径 rather than only exact boundary heads.
- Best restored review pack remains `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardTrialPack.asset` (GUID `acd1590a350614a4e86c901d33b5c5dd`) with 4 levels mounted in `Assets/ArrowMagic/Scenes/Demo.unity`.
- v18/v23 result: outer exits improved versus the broken light-gate attempts and chain shape is less snake, but internal Status still shows early opener waves (`EarlyChoices≈22-26`, `LocalRun≈6-7`), and trace still has LocalEasy rows.
- Strong middle blocked-ray gating was tested and rejected: it made 4/4 Greedy fail; light gating produced only 2 valid levels with worse opener explosion. Conclusion: direct SGP scoring alone cannot create the desired hard structure; next step should use release-owner/root-gate scaffold or the protected peel/refill loop.

## Sandwich Loop Full Pass V1 - 2026-06-25

- Executed the approved loop once from `wavepeel_drop_c05_protect_v1_b01_c11_k4` (`coverage=0.3577`, A, supportDepth=4).
- Step 1 SGP density fill: `loop1_sgp_from_k4_v1` reached coverage `0.521-0.565`, all solved/supportDepth=4 but all Drop (`openers=14-22`, `avgChoices=6.87-9.71`, `maxChoices=15-22`). This confirms SGP can fill but recreates opener/choice explosion rather than killing the skeleton.
- Step 2 protected peel on representative `loop1_sgp_from_k4_v1_b01_c04`: `loop1_peel_from_sgp_c04_v1` restored A/supportDepth=4. Useful rows: `k2` coverage `0.3904`, avg `3.74`, max `8`; `k4/k6` keep more coverage but outer/avg remained loose.
- Step 3 blocker/root placeholder refill using `Build-WavePeelReleaseScaffoldGroupV0` on the peeled row `k2`: `loop1_refill_after_peel_c02_v1` added 2 chains; 6/16 solved, 5/16 A/supportDepth=4. Best production-like row: `loop1_refill_after_peel_c02_v1_b01_c15`, coverage `0.4058`, A, MediumStructure, avg `3.36`, max `7`.
- Step 4 SGP density fill after refill: `loop1_sgp_after_refill_c15_v1` again reached coverage `0.5327-0.5769`, all solved/supportDepth=4 but all Drop. Best choices improved versus the first SGP pass (`avg≈5.96-6.11`, `max=15-16`) but not enough for hard acceptance.
- Step 5 second protected peel on the second SGP pass: `loop1_peel_after_second_sgp_c05_v1` restored A/supportDepth=4 again at higher coverage. Strong rows: `k2` coverage `0.4346-0.4404`, A, MediumStructure, avg `3.58-3.82`, max not above target; `k0` returns to coverage `0.4058`, A, avg `3.36`.
- Current conclusion: the loop is operational and the control handle is real. SGP repeatedly adds removable non-skeleton opener waves; protected peel can recover hard structure even after blocker/root placeholder refill. The unresolved bottleneck is the refill primitive: V0 owner-hit reduces the damage but does not yet prevent the next SGP pass from generating another opener wave. Next work should replace V0 with owner/wave-scheduled root/gate blocker refill, or extract SGP growth-order data to guide that scheduler.

## SGP Growth-Order / Low-Opener Refill Probe - 2026-06-25

- Added `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPGrowthOrderReportV1.ps1` to compare parent vs SGP full state and label every added chain by append order, head layer/region, firstHit/directExit, firstAvailable/clearStep, and peel status.
- `loop1_sgp_c04_growth_order_v1`: from k4 parent to first SGP full sample, all 13 added chains were layer-0 direct-exit initial openers; 11/13 were peeled and 2 were kept. This proves the SGP density pass is creating a removable boundary opener wave, not primarily blocking future space.
- `loop1_second_sgp_c05_growth_order_v1`: after V0 refill, the next SGP pass repeated the pattern; all 11 added chains were layer-0 direct-exit initial openers, 9/11 peeled and 2 kept.
- `MinHeadLayer=1` control (`loop1_sgp_from_k4_minlayer1_v1`) still failed: SGP simply produced layer-1 direct-exit opener chains, with 15/18 added chains as initial direct exits and most rows unsolved/Drop. Do not treat min-head-layer as the solution.
- Extended `Build-WavePeelReleaseScaffoldGroupV0.ps1` to output predicted opener/direct-exit counts. `loop1_refill_after_peel_c02_lowopener_v1` confirmed generated refill chains can avoid being openers/direct exits themselves (`predictedAddedOpeners=0`, `predictedAddedDirectExits=0` for 32/32), but only 11/32 traced to A/supportDepth=4. Best remains c15 (`coverage=0.4058`, A, avg `3.36`, max `7`); higher-coverage c31 gave worse next SGP behavior.
- Current concrete next step: build owner/wave-scheduled root/gate/blocker refill for the peeled direct-exit opener wave. It should keep the V0 “new heads hit protected skeleton owner” positive control, but add release-wave/owner scheduling and choose for low predicted opener + board trace survivability before giving the board back to SGP.

## SGP Gate-Aware Copied Trial - 2026-06-25

- Per user request, did not modify the original `TryBuildPeelChain` SGP path; added a separate copied experiment path in `Assets/ArrowMagic/Editor/NoMaskProceduralGenerator.cs`.
- New menu: `Tools/ArrowMagic/Direct Procedural/Build SGP Gate-Aware Trial Pack`; output pack `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPGateAwareTrialPack.asset`, report `Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_gate_aware_trial_report.csv`, levels under `Assets/ArrowMagic/SOData/Levels/DirectProcedural/SGPGateAwareTrial/`.
- V1 owner-hit peel proved that chain-head gating alone is insufficient: initial chain openers dropped to `3-4`, but early move choices stayed about `18-21` because released long chains expose many clickable body tiles.
- V4/V5 added owner fanout caps, short early release chains, and anti-local/cross-region scoring. Current Demo activePack points to `SGPGateAwareTrialPack` GUID `bcc1a50a307b305449e29f21f70a75e6`.
- Current V5 metrics: 4/4 solved, coverage `0.951-0.954`, chains `84-96`, initial openers `2-3`, edge-heads `6-8`, early average choices about `8.4-10.9`, early max choices `20-30`. This is a real improvement over raw pressure SGP, but local/near release remains high; next step needs explicit release-wave scheduling, not just more score tuning.

## SGP Direct-Cap Negative / Peeled-Wave Refill Boundary - 2026-06-25

- GPT first suggested an opt-in SGP boundary direct-exit emission cap. Implemented `-MaxBoundaryDirectExitOpenersPerPass` in `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SeededDirectSGPFillBaselineV1.ps1`.
- Direct cap tests from `wavepeel_drop_c05_protect_v1_b01_c11_k4`: cap2/cap4/cap8 all kept coverage around `0.52-0.58` but produced `0/6` solved and `0/6` supportDepth4. The cap lowers openers/choices but changes SGP's packing trajectory and cuts temporal/support semantics.
- Added `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPOpenerRemovalSensitivityV1.ps1`. On `loop1_sgp_from_k4_v1_b01_c04`, deleting each of 13 non-skeleton openers one at a time yielded `13/13` solved and `13/13` supportDepth4. Thus the opener wave is removable after generation, but unsafe to forbid during generation.
- Continued low-opener owner-hit refill without returning to SGP: from c15 (`0.4058`, A/supportDepth4), add2 yielded 14/48 A/supportDepth4 up to `0.4231`; add3 from c44 reached `0.44-0.45` geometry but `0/24` solved; add2 from c44 yielded one A/supportDepth4 at `0.4346`; add1 beyond that produced `0/32` solved.
- Current boundary: V0 owner-hit refill can safely recompile peeled cells from `0.4058` to about `0.423` and occasionally `0.4346`, but fails beyond that. Next primitive must add release-wave/owner scheduling; more random owner-hit candidates or generation-time direct caps are not enough.

## SGP LDF / Block Relation Head Probe - 2026-06-25

- Extended `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SeededDirectSGPFillBaselineV1.ps1` with opt-in `-UseLocalDifficultyField`, `-EmitLdfHeadReport`, and `-UseLdfSupplementalBlockerHeads`.
- LDF V1 uses the current SGP candidate language but scores heads with direct-exit penalty, independent opener penalty, first-hit/owner rewards, and escape-ray blocker reward. Smoke reports write `*_ldf_head_report.csv`.
- Comparison on `pressure_gradient_compiler_v1_adaptive_smoke1_final_selected.csv` to target coverage ~0.52: raw SGP solved 4/4 with supportDepth min 4 but stayed Drop (`avgChoices≈7.16`, `maxChoices=25`, `outerAvg≈16.75`). Strong LDF solved 4/4 with supportDepth min 4 and improved to `avgChoices≈6.30`, `maxChoices=18`, `outerAvg≈14.25`, still Drop.
- Head report confirmed all native heads were direct exits (`448/448`), though `116/448` had blocker reward. This proves outward-only scoring has limited headroom.
- Supplemental blocker heads (`first-hit existing`, non-direct) created non-direct candidates and greatly reduced choices/outer, but broke solve/support: high count 64 gave `0/4 solved`, supportDepth `0`; low count 8 also `0/4 solved`. Treat this as evidence that blocker heads need release-wave/owner scheduling, not as a production path.

## SGP Release-Aware Head Provider V2 Probe - 2026-06-25

- Added opt-in `-UseReleaseAwareHeadProviderV2` to `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SeededDirectSGPFillBaselineV1.ps1`.
- V2 reads a trace/ray edge CSV and offers supplemental heads only from `criticalDependencyEdge` ray cells where the current wave map satisfies `ownerWave > hitWave`, `hitWave >= ReleaseAwareMinHitWave`, and `releaseGap <= ReleaseAwareMaxReleaseGap`; the head must currently first-hit the edge's `firstHitOwner` and must not be direct exit.
- A/B on `wavepeel_drop_c05_k4_selected_input.csv` vs `wavepeel_k4_raymap_p28_v1_edges.csv` to target ~0.52: Control LDF solved `4/4`, supportDepth `4/4`, avgChoices `7.41`, max `21`, outerAvg `15.75`.
- Release-aware V2 with 4 offered heads solved `4/4` and improved avgChoices to `5.72`, max `17`, outerAvg `12.5`, but supportDepth fell to `2` in all rows. Conservative 1-head/2-head versions solved `4/4`, preserved supportDepth `4` in `3/4`, but one row still fell to depth `2`; body-avoid penalty did not fix it.
- Current conclusion: release-aware head language has real pressure signal, but head-only is not production-safe. The next primitive must bind at least part of the new chain body to the release edge/corridor or owner/wave schedule; continuing to tune reward/cap is unlikely to solve support drift.

## SGP Head+Body Corridor Contract V1 Probe - 2026-06-25

- Extended the same script with opt-in body corridor bias for release-aware heads: `ReleaseAwareBodyCorridorSteps/Distance/Reward/Penalty`. This keeps normal SGP heads unchanged and only biases the first N body steps of `source=releaseAwareV2` chains near their trace edge corridor.
- A forced 2-head corridor run accepted 2 release-aware heads per candidate and preserved `4/4 solved`, `4/4 supportDepth=4`, while improving pressure vs LDF control: avgChoices `7.41 -> 6.01`, max `21 -> 17`, outerAvg `15.75 -> 13.75`.
- A 4-head corridor run improved avgChoices slightly further (`5.89`, outerAvg `12.5`) but collapsed supportDepth to `2` in all rows. This proves the new contract has a safe dose boundary.
- Current conclusion: head+body corridor is the first positive SGP-base control that preserves support while reducing choice/outer, but it is not yet hard-tier. Next step is not more reward tuning; add per-owner/per-wave budget and scheduled groups so release-aware heads are distributed instead of overloading the same support structure.

## SGP Release-Aware Budget / Wave Window Probe - 2026-06-25

- Added opt-in release-aware scheduling controls to `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SeededDirectSGPFillBaselineV1.ps1`: `ReleaseAwareAcceptedPerOwnerBudget`, `ReleaseAwareAcceptedPerHitOwnerBudget`, `ReleaseAwareAcceptedPerHitWaveBudget`, `ReleaseAwareAcceptedPerOwnerWaveBudget`, `ReleaseAwareAcceptedPerEdgeBudget`, `ReleaseAwareAcceptedTotalBudget`, plus provider-side `ReleaseAwareMaxHitWave` and `ReleaseAwareMaxOwnerWave`.
- Exact per-owner/per-wave/per-edge accepted budget did **not** fix the 4-head collapse: `sgp_release_budget_v1_forced4_budget_exact` matched old forced4 behavior (`4/4 solved`, `0/4 supportDepth=4`, support min `2`, avgChoices `5.892`). The 4 accepted heads were already naturally distributed across different owners/waves, so repeated-owner collision was not the root cause.
- `ReleaseAwareAcceptedTotalBudget=2` also was not enough: it accepted two heads but sometimes chose late waves (`hitWave=7/10`) and only preserved `1/4 supportDepth=4`.
- `ReleaseAwareMaxHitWave=3` with 4-head candidate capacity reproduced the safe 2-head corridor result: `4/4 solved`, `4/4 supportDepth=4`, avgChoices `6.012`, max `17`; candidate reports offered/accepted exactly 2 early-wave release heads.
- Current conclusion: the real control surface is not just “how many release-aware heads” or “which owner”, but the release-wave window. Late-wave release-aware heads are currently unsafe even when owner/corridor-bound. Next step should treat `hitWave<=3` as the safe V1 window, then try raising coverage/pressure inside that window before exploring late-wave contracts.

## SGP Wave Capacity Boundary V1 - 2026-06-25

- Ran the GPT-approved capacity probe with the primitive fixed: strong LDF + protected owners + release-aware head/body corridor + `ReleaseAwareMaxHitWave=3`; only target coverage changed (`0.55/0.60/0.65/0.70`).
- Baseline safe window at ~`0.52`: `4/4 solved`, `4/4 supportDepth=4`, avgChoices `6.012`, max `17`, outerAvg `13.75`.
- At `0.55`: still `4/4 solved`, but support already partial (`2/4 supportDepth=4`, min depth `2`) and choice/outer spiked (`avgChoices=9.02`, max `22`, outerAvg `19.5`). This is the first practical capacity failure.
- At `0.60`: support/solved looked stable (`4/4 solved/supportDepth=4`), but choice/outer collapsed harder (`avgChoices=11.63`, max `29`, outerAvg `23.75`). This means support alone is not sufficient acceptance.
- At `0.65/0.70`: solvability and support collapsed (`0.65` only `1/4 solved`, support min `0`; `0.70` `0/4 solved`).
- Current conclusion: early-wave release-aware control is a valid support-preserving kernel, but by itself cannot raise coverage beyond ~`0.52` without raw SGP opener/outer-wave explosion. The next primitive must control the additional non-release-aware SGP fill after the safe early-wave heads, likely via staged wave windows or a second mid-wave contract, not simply by pushing target coverage.

## SGP Stage-2 Emission Controller V1 Probe - 2026-06-25

- Added opt-in Stage-2 soft scoring controls to `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SeededDirectSGPFillBaselineV1.ps1`: `UsePostKernelEmissionController`, `PostKernelActivationReleaseAwareHeads`, and post-kernel direct-exit/opener/hits-existing/first-hit/protected-owner/ray-blocker scoring fields.
- Implementation note: after the activation threshold is reached, the script now restarts head ordering on the same layer so Stage-2 scoring actually applies to subsequent native SGP heads.
- Probe at target `0.55`: `sgp_stage2_emission_v1_cov055_rerank` preserved structure better than kernel-only (`4/4 solved`, `4/4 supportDepth=4` vs kernel-only `2/4 supportDepth=4`), but still stayed Drop (`avgChoices=8.915`, max `25`, outerAvg `20.25`).
- Key diagnosis: all accepted post-kernel chains were still direct-exit native heads (`67/67 postKernelDirectExitAccepted`). Therefore stronger soft scoring can improve support but cannot solve choice/outer collapse when SGP's Stage-2 candidate language remains direct-exit dominated.
- Negative control: enabling small `UseLdfSupplementalBlockerHeads` (`8`) supplied alternative non-direct blocker heads but made `0/4 solved` and support min `1`; uncontracted blocker heads are not a safe candidate language.
- Current conclusion: Stage-2 needs a release-compatible non-direct candidate language, not just stronger scoring over native direct-exit heads. The next candidate should be weak-owner / bounded-wave heads with a corridor or owner contract, rather than generic supplemental blockers.

## SGP Gate-Aware Lane Cluster V7 - 2026-06-25

- In main project `F:\Unityproject\ArrowLevel-Hand`, extended copied `NoMaskProceduralGenerator` gate-aware trial with a visual rail-lane cluster detector. It now counts long same-axis straight tile streaks across rows/columns, not only single long straight chains.
- Menu remains `Tools/ArrowMagic/Direct Procedural/Build SGP Gate-Aware Trial Pack`; pack remains `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPGateAwareTrialPack.asset` and Demo still points to GUID `bcc1a50a307b305449e29f21f70a75e6`.
- V7 report `Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_gate_aware_trial_report.csv`: 4/4 solved, coverage `0.953-0.963`, chains `84/88/96/90`, initial openers `3/3/3/2`, `Rails=0`, `Lanes=0`.
- User screenshot problem was a detector blind spot: multiple shorter chains can align into a long visual highway even when `Rails=0`; `Lanes=0` is the new guard for that.
- Remaining issue: early local release remains high (`EarlyChoices≈8.4-10.9`, `LocalRun≈7-11`, near rate high). Next step should target release-wave scheduling / local sequential unlocks, not just more straight-line filtering.

## SGP Gate-Aware Wave / Early-Length Trial V8f - 2026-06-25

- Continued only on copied `SGPGateAwareTrial` path in `Assets/ArrowMagic/Editor/NoMaskProceduralGenerator.cs`; original SGP path remains untouched.
- Added `GateAwareWaveMetrics` with chain-level and tile-level release-wave stats (`Wave`, `TileChoices`, `TileBand`, local region burst), stronger wave scoring, same-band head penalties, and report fields for `bodyBandReject/parentBandReject`.
- Hard body/parent band rejection V8d was too strong: it caused failed/over-fragmented candidates (`parentBandReject` in the thousands, chain count/coverage unstable). Kept only a much softer extreme body-band reject and moved parent same-band handling into scoring.
- V8f changed gate-aware construction order so early released chains stay shorter, with longer chains deferred to later fill. Latest report: 4/4 solved, coverage `0.950-0.953`, chains `95/100/103/99`, initial openers `3/3/3/4`, `Rails=0`, `Lanes=0`.
- V8f is usable for visual review but not final hard-tier quality: `EarlyChoices≈7.3-7.9`, max choices `10-18`, `TileChoices≈5.8-7.6`, `TileBand≈6-8`. Next likely primitive is an actual release-wave/micro-delay construction pass, not more pure score tuning.

## SGP Gate-Aware Temporal Scheduler V9c - 2026-06-25

- Implemented the GPT-reviewed minimal temporal scheduler on the copied Gate-Aware path: predicted owner wave (`direct=0`, `hit parent wave+1`), early wave budgets, preferred-wave scoring, early wave chain-length limits, and stronger tile-level wave scoring.
- V9 first run proved the scheduler was too soft (`waveReject=0`) but improved some levels. V9b hard band-wave rejection was too aggressive: coverage dropped in one level (`0.911`) and did not consistently reduce choices. Keep this as a negative result.
- V9c keeps hard limits for early wave count/early chain length, but moves band-wave balancing back to strong scoring. Latest report: 4/4 solved, coverage `0.952-0.961`, chains `94/104/108/105`, initial openers `3/2/3/3`, max predicted wave `11`.
- V9c metrics: `EarlyChoices≈6.8/7.9/6.9/7.4`, early max `10/21/13/14`, `TileChoices≈6.9/5.1/7.5/7.1`, `TileBand≈7/6/8/7`. This is a real scheduler insertion but still not a decisive gameplay-quality shift; local-run/near-rate remain high.
- Current conclusion: SGP can be nudged into temporal scheduling without breaking coverage, but the next true quality jump likely needs a local-run/micro-delay contract that changes *where the next best move appears*, not only how many predicted wave chains exist.

## SGP Gate-Aware Anti-Local Negative Probe V10 - 2026-06-25

- User screenshot confirmed the remaining defect: exits are lower, but play is still mostly continuous local clearing. Tried adding generation-time parent-child anti-local constraints to copied `SGPGateAwareTrial`.
- V10 hard anti-local from the first owner-hit stage was too strong: after 3 entry chains, owner-hit candidates were almost all rejected (`localReject` thousands), fill collapsed to about `0.013`. This proves the intervention point is real but cannot run before a skeleton exists.
- V10b delayed hard anti-local until after 22 placed chains. Coverage recovered (`0.950-0.955`) and tile choices improved on some rows, but LocalRun stayed high (`22/19/13/11`) and NearRate remained `0.80-0.97`.
- V10c strengthened LocalRun/NearRate scoring and narrowed head-pick randomness. It still did not pass: LocalRun remained `13/13/13/11`, and one row dropped coverage to `0.945`.
- Current conclusion: parent-child distance and scoring are insufficient because Greedy can still find nearby moves through chain bodies / local exposed tiles. Next step should be a true local-run shaping grammar: generate or reserve remote unlock pockets / delayed anchors, or simulate the first N greedy moves during construction and reject candidates that create adjacent next-best moves.

## SGP Gate-Aware LocalRun V11-V15 Negative Boundary - 2026-06-25

- Added V11 diagnostic-only `GateAwareLocalProbeMetrics` on copied `SGPGateAwareTrial`: k-step probe emits `LEP/SJD/RRR/Run/ProbeMaxChoices` into `sgp_gate_aware_trial_report.csv`; compile passed. V11 improved observability but did not produce a quality jump: sample rows still had `RRR=0.80-1.00`, LocalRun high, and one row dropped coverage.
- V12 tried generation-time parent/ancestor local-run proxy (`ParentOwner`, temporal local penalties/rejects, smaller head window). It was a negative result: first row coverage recovered but `LocalRun=15`, second row failed coverage at `0.893`; reverted the added proxy code.
- V13 tried aligning the proxy to real pressure `LocalRun` using bbox/4x6 micro patch lineage. It also failed: temporal rejects did not trigger meaningfully and `LocalRun` stayed about `14`; reverted.
- V14 tried same predicted-wave pressure-patch capacity and V15 tried stronger cross-region owner-hit scoring. Both failed to create a gameplay-level shift: first-row LocalRun stayed `16-21`, NearRate stayed high, and same-region owner hits still dominated. Reverted the failed V12-V15 code/weights; copied path is back to V11 diagnostic state.
- Current conclusion: low-cost head scoring, predicted wave budgets, parent-child distance, and same-wave patch penalties cannot fix the continuous local clear problem in this SGP language. The next real route should be a new chain-segment/slot grammar that owns head + body + release timing, or the multi-round sandwich cadence (small SGP batch -> immediately rewrite exact added slots -> trace gate), rather than more scoring on the copied SGP head provider.

## SGP Stage-2 Candidate Augmentor V1 - 2026-06-25

- Extended `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SeededDirectSGPFillBaselineV1.ps1` with `-UsePostKernelCandidateAugmentor`, `PostKernelAugmentor*` wave/protected controls, and `-PostKernelRecomputeAfterChain`.
- V1/V1B added non-direct, first-hit-owner post-kernel heads. At target `0.55`, they preserved `4/4 solved` and `4/4 supportDepth=4`; best aggregate `sgp_stage2_augment_v1_cov055`: avgChoices `7.978`, max `25`, outerAvg `20`, post-kernel direct ratio `64/70`.
- V1C added post-kernel state refresh after each committed chain. It preserved `4/4 solved/supportDepth=4` and reduced outer/max slightly (`outerAvg=18`, max `21`), but still stayed Drop/LocalEasy with avgChoices `7.958`; post-kernel direct ratio remained high (`60/64`).
- Concrete diagnosis: head-level non-direct owner-mediated candidates are safe but too scarce (`4-6` accepted total per 4 candidates). Native direct-exit heads still dominate Stage2, so this is not yet a production fill route.
- Next step should not be more scoring. Build a segment-level Stage2 grammar: owner-mediated / bridge mini-chain candidates that own both head and first body segment, then feed them into SGP as structured alternatives to direct-exit native heads.

## SGP Sandwich Slot-Refill V2 Probe - 2026-06-25

- Continued the separate “sandwich” line: validated skeleton/root -> raw SGP density fill -> peel non-skeleton opener wave -> refill peeled space with new root/gate chains -> return to SGP.
- Extended `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-WavePeelReleaseScaffoldGroupV0.ps1` with opt-in slot scheduling: `-UsePeeledSourceChainSlots`, `-FallbackToAllPeeledCells`, and `-MaxChainsPerSourceSlot`. The new mode groups peeled cells by the original SGP source chains, so refill targets the same corridors that SGP had just used.
- Immediate refill result improved materially over random owner-hit V0: `sandwich_v2_slot_refill_add2_probe1` produced 32 add2 candidates with 19 A/supportDepth4 rows, versus old V0 add2's 5 A/supportDepth4 out of 16. Best immediate row c17 had coverage `0.4058`, A, supportDepth4, avgChoices `3.22`, maxChoices `7`, outerExitHeadCount `6`.
- Returning slot-refill rows to raw SGP did not solve the sandwich loop. c17 -> raw SGP target `0.56` gave 6/6 solved but all Drop, avgChoices about `10.86`, max about `24.83`, outer about `23.5`. c26 -> raw SGP similarly gave 6/6 solved, all Drop, support4 only 4/6, avgChoices `11.43`, max `24.17`, outer `22.17`.
- Current conclusion: refill should indeed block/rewrite the SGP-created escape corridor, not just fill holes. Slot-refill helps immediate stability, but SGP still regrows direct-exit opener waves from adjacent/remaining corridor space. Next validation should inspect second-pass growth order and add a no-bypass/halo or next-SGP constraint around refilled source slots before considering a dedicated worktree.
- Small-step boundary validation changed the working hypothesis. From slot-refill c26, raw SGP with `MaxNewChains=2/4/8/12` showed a sharp boundary: step2 gave 4/4 solved/supportDepth4 with 3 A + 1 B and avgChoices `4.03-4.42`; step4 already produced 3 Drop; step8/12 were all Drop with outer/choice explosion. Growth reports show even step2 adds only layer0 direct-exit openers, but keeping the increment small makes it trace-stable.
- Targeted refill of just the two step2 SGP slots (`swv2_r3_step2_refill_add2`) produced 8/16 A/supportDepth4, avgChoices `3.55-3.58`, max `8`, outer `6`, coverage `0.4154`. A subsequent SGP step2 from c09 reached coverage `0.4635` with 4/4 solved/supportDepth4, 2 A + 2 B, no Drop. This is the first positive evidence for a multi-round sandwich cadence: SGP adds a small direct-exit layer, refill rewrites those exact slots, then SGP can continue.
- Important correction: do not require one refill to block all SGP exits or completely close the boundary. The viable cadence is likely “leave controlled exits visible to SGP, let it add a very small batch, then immediately rewrite that batch into protected owner-hit scaffolds.” Refill validation is still too slow because full board trace is used on all candidates; production needs cheap prechecks and full trace only at commit.
- Follow-up efficiency probe tested whether SGP can add 4-6 chains per cadence. Step4 from the same base had 4/4 solved/supportDepth4 with one A and three Drop; step6 had 4/4 solved/supportDepth4 but all Drop (`avgChoices≈5.2-6.1`, max `12-13`, outer `11-12`). Growth reports show all added step6 chains were again layer0 direct-exit openers.
- Partial peel on step6 c02 corrected a parameter misunderstanding: `KeepOpenerCounts` means rewrite-eligible openers to keep, not total openers. Keeping 4/5 of 6 added openers (remove 1-2) preserved 16/16 solved/supportDepth4 and reduced avg from `5.21` to as low as `4.42`, but all remained Drop. Refill add1 after partial peel still stayed Drop; loose add2 harmed support. Current conclusion: 6-chain SGP steps are too aggressive for current refill grammar; 4-chain steps may be the next useful cadence, while 2-chain steps are the proven-safe baseline.
- Standard-level attempt on the 4-chain cadence: starting from a step4 A sample at coverage `0.465`, another SGP step4 reached coverage `0.50` but all rows were Drop. Partial peel of 1-2 of the 4 new direct-exit openers produced one B/supportDepth4 at coverage `0.4846`. Refill add1 on that B row produced 10/12 B/supportDepth4, best coverage `0.4942`, avg `4.33`, max `10`, outer `10`; add2 strict was too tight to generate and loose add2 harmed support in adjacent probes. A subsequent SGP step4 from best B (`0.4942`) preserved solved/supportDepth4 but returned to all Drop (`avg≈5.45-6.04`, max `14`, outer `13-14`).
- Current boundary: the sandwich cadence has positive signal and can repeatedly preserve solve/support while SGP grows coverage, but with current refill language it does **not** yet reach a standard/full production level. It plateaus around coverage `0.49-0.50` as B/Drop because each next SGP step still emits only layer0 direct-exit heads. The next required primitive is a faster targeted refill that rewrites 4-chain SGP bursts into stronger owner-controlled blockers without relying on exhaustive full trace.
- Follow-up round validated a stronger “full peel + refill” sandwich variant. From r5/c03, peeling all 4 SGP direct-exit additions and refilling 4 owner-hit chains produced 24 candidates with 8 A/supportDepth4 rows; best rows reached coverage `0.496-0.506`, avg choices `3.64-4.68`, max `8`, outer `9`. This is materially better than keeping 2 bad exits and only refilling around them.
- Returning the best full-peel/refill row to SGP step4 pushed coverage to `0.546-0.569` and kept `4/4 solved/supportDepth4`, but all rows were Drop (`avg≈5.02-6.17`, max `11-12`, outer `11-13`). A second full peel/refill add4 could recover A-tier on some rows but supportDepth dropped to `2`, so 4-chain bursts are too aggressive for the current refill grammar.
- Small-step continuation is more stable: from the same A/support4 row, SGP step2 reached coverage `0.515-0.535` with `4/4 solved/supportDepth4` and all B; a step2 peel/refill add2 sample produced several A rows and some supportDepth4 rows around coverage `0.517-0.525`. Another SGP step2 from a support4 row reached coverage `0.55` with `4/4 solved/supportDepth4`, but the next refill exposed a new high-density bottleneck.
- High-density bottleneck: at coverage `~0.55`, the two peeled SGP opener chains left only `2` peeled cells and one usable source slot. Plain slot-refill and halo1 could only add 1 chain. Added `PeeledSlotHaloRadius` to `Build-WavePeelReleaseScaffoldGroupV0.ps1`; halo2 can add 2 chains quickly, but the sample was `4/4` unsolved/support collapse (`supportDepth 0-1`). Conclusion: beyond `~0.52`, refill needs a support-closure-aware halo/owner contract, not just a larger local construction area.
- Current sandwich verdict: valuable to continue in a dedicated worktree, but not yet production. Proven useful primitives are `small SGP step2`, `full peel of new unprotected openers`, and `slot refill against peeled SGP source slots`. Open problem is a support-preserving high-density refill grammar that can use halo space without cutting the hard-lock closure.
- Dedicated clean worktree opened for this line: `.worktrees/sgp-sandwich-refill`, branch `codex/sgp-sandwich-refill`, based on `codex/sgp-rhythm-lab` at `fedc26aa`. It is intentionally clean and does not yet contain the untracked `Tools/`, reports, levels, or generated assets from `sgp-rhythm-lab`; next step is to copy/import only the minimal sandwich toolchain and seed reports needed for a focused validation.
- New working hypothesis: refill and SGP must target the same outlet pocket instead of fighting globally. Add a `TargetedSGPZonePass` experiment: derive a target zone from the last peeled SGP chains + refill scaffolds + small halo, then force SGP heads to start in that zone, bias/limit body growth to the zone/halo, require first-hit to the intended owner/refill structure, and reject new direct-exit heads outside the zone. Success signal is high next-SGP overlap with the target zone while preserving solved/supportDepth and not spiking choice/outer.
- Refill optimization direction: stop full-tracing every refill candidate. Use cheap prechecks first (`source-slot/zone overlap`, owner hit, no protected skeleton cell touch, cavity/slot loss, direct-exit delta), trace only top-K candidates, then full trace only at commit. For cadence testing, keep `step2` as safe baseline and test `step4` only with targeted zone + top-K refill, not loose halo expansion.
- GPT/manual review correction: `TargetedSGPZonePass` should be treated as `FailurePocketAnchoredSGPPass`, not a spatial mask. The pocket is a closure-derived subgraph from the previous SGP failure: last-step direct-exit heads, upstream 1-2 hop support neighborhood, boundary escape rays, refill owners/scaffolds, and local support-disruption footprint. The hard rule should be “new SGP heads first-hit pocket anchors/refill owners” before “stay inside geometric zone”; geometry/halo is only a construction envelope. This prevents the zone pass from degenerating into previous brittle pin/lane/mask failures.
- `sgp-sandwich-refill` validation 2026-06-25: copied minimal tools into the clean worktree and added opt-in `FailurePocketAnchorMode/Owners` to `Build-SeededDirectSGPFillBaselineV1.ps1`. On the known refill parent `swv2_r6...c15`, raw historical step2 was 4/4 solved/support4 but B-tier (`avg≈4.77-5.50`, max `10`, outer `10-11`). Hard anchor to protected hit owners `30;42` stayed 4/4 solved/support4/A but only added 1 chain (`0.517->0.527`). Expanding anchors to pocket/refill owners `30;42;44;45` restored 2-chain throughput (`0.517->0.527-0.531`), 4/4 solved/support4/A, `boundaryDirectExitUsed=0`, outer fixed at `9`, avg `4.5-4.92`; growth report shows added chains first-hit refill owners `45/44`, no direct exit. A second anchored step from the best row with anchors `30;42;44;45;46;47` reached up to coverage `0.553846`, 4/4 solved/support4/A, outer still `9`, max `9`, and no new direct-exit. A third hard-anchor step with anchors `30;42;44;45;46;47;48;49` added 0 chains, establishing the current pocket capacity boundary.
- Current sandwich decision: `FailurePocketAnchoredSGPPass` is a real positive control surface and better than loose halo/mask. It can make SGP and refill cooperate inside one failure pocket. The next production step is automatic pocket extraction and rotation: when a hard pocket anchor adds 0 chains, detect the next SGP failure pocket or switch to a controlled `Reward` mode/top-K refill rather than continuing to hard-force the exhausted pocket.

## SGP-3L / Stage-2 Grammar Unit V0 - 2026-06-25

- Implemented opt-in grammar-unit experiment in `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SeededDirectSGPFillBaselineV1.ps1`: `UsePostKernelGrammarUnits`, `PostKernelGrammar*`, and fixed `fixedPrefixCells` support in `Try-BuildChain`.
- V0 grammar units are 2-4 cell post-kernel mini-chain prefixes with non-direct head, first-hit owner, wave window, optional protected hit, and score bonus. SGP still grows the remaining body after the fixed prefix.
- Aggressive V0 at target `0.55` accepted many grammar units (`54/85` post-kernel chains grammar, direct ratio `31/85=0.365`) and lowered avg/max/outer (`avgChoices=6.185`, max `13`, outerAvg `11.25`), but broke all rows (`0/4 solved`, supportDepth min `0`).
- Conservative/protected V0 also failed (`0/4 solved`, support4 `0/4`) despite reducing direct ratio to `33/82=0.402`; best row had low avg/max/outer (`4.39/9/7`) but supportDepth only `1`.
- Current conclusion: GPT's SGP-3L architecture is directionally correct, and grammar units have real pressure power, but V0 block/bridge units are not spine-safe. Next L2 grammar must bind mini-chain segments to trace-edge/spine-safe release semantics, not merely first-hit/protected owner.

## SGP-3L / TraceEdge Grammar Unit V1 - 2026-06-25

- Added opt-in `UsePostKernelTraceEdgeGrammarUnits` to `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SeededDirectSGPFillBaselineV1.ps1`. V1 units are fixed-prefix mini-chains anchored to original `criticalDependencyEdge` ray corridors, with current first-hit back to the edge's original `firstHitOwner`.
- Initial hybrid with the safe early-wave release kernel (`sgp_stage2_traceedge_grammar_v1_cov055`) preserved `4/4 solved/supportDepth=4`, but offered/accepted `0` trace-edge units. The safe kernel had already occupied/rewritten the available edge corridor slots.
- Isolated trace-edge grammar without release heads had real throughput (`18` accepted units, direct ratio `0.769`) but collapsed all rows (`0/4 solved`, supportDepth min `0`). This proves edge-corridor anchoring alone is still not enough.
- Added a cheap commit reachability check for V1: after provisional chain insertion, protected owners must remain wave-reachable, and the original edge owner must remain later than its hit owner. This restored solvability (`4/4 solved`) and improved pressure (`avgChoices=6.603`, outerHeadAvg `15.5`) but supportDepth still degraded (`0/4 supportDepth=4`, min `2`).
- Tightening V1 to `hitWave<=3` improved support stability (`3/4 supportDepth=4`) but lost most pressure gain (`avgChoices=8.852`, direct ratio `0.857`). Current conclusion: TraceEdge Grammar V1 is a useful diagnostic/control surface but not production-safe. Next step needs a support-closure light check or an explicit support-path non-interference contract, not more owner/first-hit/wave scoring.

## SGP-3L / TraceEdge Support Proxy V2 - 2026-06-25

- Added a support-path non-interference proxy to the TraceEdge grammar commit boundary. It compares affected original `criticalDependencyEdge` rows near the new path and allows only unchanged first-hit edges or a clean relay shape `edgeOwner -> newChain -> originalFirstHitOwner` with wave order preserved.
- Broad-wave V2 (`sgp_stage2_traceedge_grammar_v2_supportproxy_cov055`) rejected many units (`167` support rejects) and preserved `4/4 solved`, but still failed support closure (`0/4 supportDepth=4`, min `2`). It improved pressure versus baseline (`avgChoices=7.098`, max `20`, outerHeadAvg `15.75`) but did not fix the missing primitive.
- Early-wave V2 (`sgp_stage2_traceedge_grammar_v2_supportproxy_wave3_cov055`) also rejected candidates (`37` support rejects) but stayed almost identical to early-wave V1: `4/4 solved`, `3/4 supportDepth=4`, avgChoices `8.857`, direct ratio `0.87`.
- Current conclusion: local edge relay preservation is a useful filter but still not equivalent to supportClosure preservation. Next check must compare support-closure proxy directly, e.g. support root/depth/branch preservation, rather than only local first-hit basin invariance.

## SGP-3L / Closure Shadow Proxy V3 - 2026-06-25

- Added opt-in closure shadow controls to `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SeededDirectSGPFillBaselineV1.ps1`: `PostKernelTraceEdgeGrammarUseClosureShadow`, `PostKernelTraceEdgeGrammarClosureDepth`, and `PostKernelTraceEdgeGrammarMinShadowRatio`.
- First implementation only tracked each candidate's local first-hit basin. It was too local: broad-wave V3 still had `0/4 supportDepth=4`, and wave3 V3 stayed `3/4 supportDepth=4`.
- Updated V3b to include the current parent’s best protected closure-shadow roots, so the commit check compares support-root propagation, not just the candidate edge. This restored wave3 support stability: `sgp_stage2_traceedge_grammar_v3b_closureshadow_wave3_cov055` and ratio `0.80` both traced `4/4 solved`, `4/4 supportDepth=4`.
- Negative result: V3b is too strict as a pressure generator. It accepted only `4` TraceEdge grammar units, with `130` closure rejects, and regressed difficulty pressure close to baseline (`avgChoices=10.102`, `outerAvg=20`, direct ratio `0.948`).
- Broad-wave support-root shadow currently times out under the full 4-candidate run, so this layer is not production-scalable without caching/top-k/lazy evaluation.
- Current conclusion: closure shadow is the correct safety gate for supportClosure, but not yet a Stage2 candidate language. Next step should build a closure-safe grammar candidate pool or lazy TopK closure evaluation, instead of tuning wave/owner/radius/reward.

## SGP-3L / Closure-Compatible Proposal Prefilter V4 - 2026-06-25

- Added opt-in closure-compatible proposal controls to `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SeededDirectSGPFillBaselineV1.ps1`: `PostKernelTraceEdgeGrammarUseClosureCompatiblePrefilter`, `PostKernelTraceEdgeGrammarBasinDepth`, `PostKernelTraceEdgeGrammarBasinDistance`, and `PostKernelTraceEdgeGrammarMaxAffectedRoots`.
- V4 implementation builds an affected closure-basin field from protected support roots plus the trace edge owner/hit owner, then constrains TraceEdge grammar fixed prefixes and body extension to stay near that field. This changes candidate generation space before the V3 closure shadow commit gate.
- Wide V4 (`basinDepth=3`, `distance=1`) was ineffective as a prefilter (`prefilterRejects=0`), stayed at only 4 TraceEdge accepts, and degraded support (`2/4 supportDepth=4`). The field was too broad and did not meaningfully change candidate language.
- Narrow V4b/V4c (`basinDepth=2`, `distance=0`, `maxAffectedRoots=1`, shadow ratio `0.95/1.0`) made the prefilter active (`146` prefilter rejects, TraceEdge offered `8`) and preserved solvability (`4/4 solved`), but still leaked one support-collapse row (`3/4 supportDepth=4`, depths `4,4,4,2`).
- Key comparison at target `0.55`: Stage2 baseline kept `4/4 supportDepth=4` but directRatio `1.0` and avgChoices `8.915`; V3b kept `4/4 supportDepth=4` but accepted only 4 TraceEdge units and avgChoices `10.102`; V4b/c accepted 4 units, directRatio `0.946`, avgChoices `9.31`, but support dropped to `3/4`.
- Current conclusion: closure-compatible prefiltering is a real control surface only when narrow, but current closure-basin field still does not encode the full supportDepth invariant. Do not keep tuning shadow ratio/radius as the main route; next step should define a stricter support-depth-preserving grammar unit or add a direct supportDepth/light-trace commit check only for top-ranked grammar candidates.

## SGP-3L / Support Witness Edge Gate V1 - 2026-06-25

- GPT confirmed the next support guard should be edge-survival based rather than full witness-path preservation: keep a compact set of first-hit support witness edges from the support root reverse closure, and allow an optional relay shape `child -> newChain -> oldParent`.
- Implemented opt-in witness parameters in `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SeededDirectSGPFillBaselineV1.ps1`: `PostKernelTraceEdgeGrammarUseWitnessGate`, `WitnessDepth`, `WitnessMaxRoots`, `WitnessEdgesPerRoot`, `WitnessMinSurvivalRatio`, and `WitnessAllowRelay`.
- Exact witness and relay witness experiments at target `0.55` were identical to V4c: `4/4 solved`, `3/4 supportDepth=4`, depths `4,4,4,2`, avgChoices `9.31`, directRatio `0.946`, TraceEdge accepted `4`, witnessRejects `0`.
- Per-row diagnosis: every row accepted only `1` TraceEdge grammar unit, but also accepted `16-20` native direct post-kernel chains. The failing row had `supportDepth=2` with `traceAccepted=1`, `postDirect=17`, and `witnessRejects=0`.
- Current conclusion: the supportDepth leak is not caused by the TraceEdge grammar unit cutting the selected witness edge at commit time. It is more likely caused by later native direct-exit Stage2 SGP chains, which are still outside the support witness contract.
- Next target: apply the cheap witness survival gate, or an equivalent support-depth proxy, to native post-kernel direct chain commits as well as TraceEdge grammar commits. Do not keep adding stricter TraceEdge-only gates until Stage2 native direct chains share the same support contract.

## SGP-3L / Native Direct Commit Control Negative Boundary - 2026-06-25

- Extended `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SeededDirectSGPFillBaselineV1.ps1` with three opt-in native post-kernel direct controls: `UsePostKernelNativeSupportPressureBudget`, `UsePostKernelNativeDirectIdentityGate`, and `UsePostKernelNativeSupportShadowGate`.
- SPB hard budget was rejected as a primary mechanism. `spb24` reduced choices/outer (`avgChoices≈7.86`, `outerAvg≈12`) but broke all rows (`0/4 solved`, supportDepth mostly `0`). `spb48` partially recovered (`2/4 solved`) but did not fix the baseline support leak and introduced new support/solve drops.
- Native direct A/B identity tagging showed the anchor-reuse hypothesis is false for this sample: V7 matched V4c exactly (`4/4 solved`, `3/4 supportDepth=4`, avgChoices `9.31`), and `postKernelNativeDirectPollutionAnchorRejects=0`.
- Native support-shadow gate also failed to distinguish the leak: V8 evaluated all native direct commits (`70` evals) with `0` rejects and matched V4c exactly, including the `supportDepth=2` row.
- Current conclusion: the remaining Stage2 leak is not explained by direct-chain count, bad-anchor reuse, or the current support-root shadow proxy. The native direct commit changes closure/release geometry in a way these local proxies do not see.
- Next direction: stop tuning scalar budgets or current shadow/radius gates on this single-pass Stage2 path. Either build a stronger top-K direct support check that actually predicts `supportClosureBestDepth`, or return to the sandwich cadence where small SGP bursts are peeled/re-written as closure-safe slots before further density growth.

## SGP Gate-Aware WSCU Lite V16 Negative Boundary - 2026-06-25

- Tested V16 on copied `Assets/ArrowMagic/Editor/NoMaskProceduralGenerator.cs` `SGPGateAwareTrial`: anchor segment distance, early body turn/anchor penalties, local parent-head rejection, same-region dependency quota, and dependency length rhythm.
- V16/V16b produced only weak signal: one sample improved to `LocalRun=13` / `NearRate=0.86`, but coverage fell to `0.923`; other variants either rebounded to `LocalRun=17-25` or dropped coverage.
- Strong same-region length rhythm fired (`rhythmCap>100`) but exploded chain count to `130-148` and worsened real LocalRun. Cross-region hard quota killed fill entirely, stopping around `8-9` chains / `0.04-0.05` coverage.
- Cleaned the destructive V16 helpers back out. Restore check generated row 1 again with coverage `0.954`, but real `ComputePressureChoiceCurve` still had `LocalRun=18`, `NearRate=0.86`.
- Current conclusion: WSCU-lite patches inside the copied SGP head/body picker do not create the needed qualitative shift. Real breakthrough likely requires the separate SGP-3L / sandwich route: tiny SGP bursts followed by slot/trace-edge rewrite with closure-safe support checks.

## Reverse Unlock Skeleton Spike - 2026-06-25

- Added temporary offline probe `.codex-run/reverse_unlock_skeleton_probe.py` to test a Sokoban-style reverse-generation idea without touching Unity assets: start from empty board, add chains in reverse solve order, and require each newly added chain to be clearable against the current board.
- The probe mirrors the portable `GreedyEscapeSolver` rule: a chain is removable when its head ray to the board edge has no active occupied cells.
- Positive small-canvas results: `8x12` produced a planned+greedy solved skeleton at coverage `0.75` with planned initial choices `3`, avg `1.33`, max `3`; `10x14` produced coverage `0.707` with planned initial `5`, avg `1.95`, max `5`.
- Scaling boundary: `12x16` and `14x18` remain planned+greedy solved around coverage `0.58-0.70`, but opening choices typically rise to `7-8+`. Targeted reverse blocker construction helps solvability/coverage but does not yet build a low-opener full-board difficulty net.
- More important parent-based result: using the validated `0.307598` HardLock030 DynamicOuterGate parent as the seed, reverse blocker extension produced:
  - add12: coverage `0.4179`, planned+greedy solved, initial choices `3` vs base `5`, avg/max about `3.7/6`.
  - add20: coverage `0.4865`, planned+greedy solved, initial/max `6/6`, avg about `4.1-4.3`.
  - add30: coverage `0.5699`, planned+greedy solved, but opener/choice pressure returned (`initial=12`, greedy avg `6.82`, max `12`).
- Current conclusion: reverse generation is weak from empty board but promising as a parent-expansion primitive. It can preserve guaranteed solvability and add density/pressure around a 0.30 hard parent up to roughly `0.49` before opener回潮; production next step is to convert this offline probe into Unity LevelDefinition output + full board trace, then add structured blocker/gate templates for the `0.49 -> 0.6+` range.

## Reverse Sandwich Production Line V0 - 2026-06-25

- Added formal orchestrator `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-ReverseSandwichProductionLineV0.ps1` and formal reverse refill tool `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-ReverseSlotRefillV1.py`.
- Production route now matches the current target: 0.30 hard parent -> bounded SGP burst in real fill slots -> growth-order diagnosis -> peel all SGP-added slots -> reverse-valid refill in those slots -> full `Build-SGPRhythmTrace.ps1` -> final growth report.
- Smoke `reverse_sandwich_prod_v0_smoke02`: SGP reached coverage `0.4252451`, solved/supportDepth4, but growth report showed `addedInitialOpeners=7` and `addedDirectExit=7`; this confirms SGP slot discovery works but raw SGP difficulty collapses via direct-exit opener pollution.
- Old `WavePeelReleaseScaffoldGroupV0` refill reduced openers/outer but repeatedly became unsolved even with only 3 blockers, so it remains a negative boundary for this route.
- New `ReverseSlotRefillV1` standalone smoke using the same SGP slot field added 3 reverse-valid chains and formally traced as solved/A: openers `3`, avg/max `3.28/6`, outerExitHeadCount `3`, supportDepth `4`, coverage `0.3247549`.
- Updated `Build-ReverseSlotRefillV1.py` with `--unity-top-k`; orchestrator smoke `reverse_sandwich_prod_v0_smoke06` exported top8 reverse candidates and traced all 8.
- `smoke06` result: all top8 reverse candidates were solved/A with openers `3`, avg/max `2.95/6`, outerExitHeadCount `2-3`, coverage `0.3296569`, but this seed group had supportDepth only `2` across all top8. This means top-K export works, but the simplified reverse score/seed still does not guarantee official supportClosure preservation.
- Next concrete step: add reverse seed sweep / multi-prefix selection to `Build-ReverseSandwichProductionLineV0.ps1`, select by official trace (`solved`, supportDepth, choice/outer), and only then feed the chosen reverse candidate into the next small SGP burst. Do not increase SGP burst size until a supportDepth4 reverse-refill candidate is auto-selected.

## Tonight Full-Level Production Goal - 2026-06-26

- Active target: produce one complete playable level near coverage `0.9` while preserving difficulty; coverage alone is not sufficient.
- Current main route: SGP finds real fill slots/packing outlets, then reverse slot refill replaces SGP-added direct-exit meat with release-valid blocker/release chains that reduce opener/outer pollution.
- Official trace remains the commit truth: accept only solved candidates with preserved support/depth signal and controlled choices/outer exits.
- Immediate implementation target: add reverse seed/topK official-trace selection, then loop `small SGP burst -> peel new direct-exit slots -> reverse refill -> official trace commit`.
- If reverse cannot produce enough high-support candidates, switch to another difficulty-control layer, but keep the same goal: SGP for coverage/slot discovery, semantic refill/control for difficulty.
- 2026-06-26 run update:
  - `Build-ReverseSandwichProductionLineV0.ps1` now supports `ReverseSeedBatches/ReverseSeedStride`; seed sweep can merge reverse candidates and select by official trace.
  - Seed sweep proof: `reverse_sandwich_prod_v0_seed_sweep02_hit271001` selected a reverse refill at coverage `0.3296569`, `solved=True`, `processTier=A`, `supportDepth=4`, `openers=3`, `avg=3.3`, `max=6`, `outer=2`.
  - Round2 proof from that parent reached coverage `0.3541667`, `solved=True`, `A`, `supportDepth=4`, `openers=3`, `avg=3.14`, `max=6`, `outer=2`.
  - Large SGP slot field from a higher parent reaches coverage `0.86-0.88`, but raw SGP is `Drop/unsolved/support0` with openers/outer exploding; useful as slot discovery only.
  - Added `Build-TailExtensionFillV1.py`: extending existing chain tails without adding heads can raise density. Ray-blocking tail fill reached coverage `0.865` but became unsolved/support0; neutral-only tail fill reached coverage `0.5980392`, `solved=True`, `B`, `supportDepth=3`, `openers=10`, `avg=3.87`, `max=10`, `outer=11`.
  - Neutral tail slots are exhausted near `0.598`; allowing tail cells that touch even one existing ray caused official unsolved from `0.618+`.
  - Added `--max-after-choices` and `--slot-ray-density-max` to `Build-ReverseSlotRefillV1.py`. Bulk reverse with choice cap can preserve support but becomes Drop as coverage rises: `0.6225` solved/support4 with openers `14`; `0.6483` solved/support4 with openers `18`.
  - Ray-coupling diagnosis for `0.598 -> 0.877` SGP slot field: `228` SGP slot cells, `76` neutral slots, `152` ray-touching slots, `0` direct-opener-ray slots. New neutral-slot reverse reached only `0.615` and was Drop.
  - GPT review on conversation `6a3be215-05c8-83e8-b5c9-307a492fea69`: current route is realistically capped around `0.62-0.68` tonight; to approach `0.9`, insert a ray-aware thinning / release-safe constraint step before reverse instead of trying to repair the already bad SGP full state.

## SGP Sandwich Priority/Boudary Checkpoint - 2026-06-26

- 当前目标仍未完成：未跑通 coverage 接近 `0.9` 的完整难度关卡；今晚 sandwich/refill 线最新稳定点为 coverage `0.6846154`、`solved=True`、`processTier=B`、`supportDepth=4`，入口 CSV 为 `.worktrees/sgp-sandwich-refill/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/fpfast_from0673_break069_o67_candidates.csv` offset `0`。
- 已把 commit 优先级明确为：`solved + supportDepth4` 为硬门槛；A/S 最优；B 可作为可控债；support2/Drop 只可作为 repair source，不能提交；coverage 只在前述条件内排序。
- 新增 `Invoke-FailurePocketSchedulerV1.ps1 -AllowRewardDebtCommit`：允许 Reward 本身在 `solved/supportDepth4` 且 A/S/B 债务阈值内时直接进入 commit 候选，避免“SGP 一逃就必须堵”的错误循环。
- 从 `0.6558` 继续验证：owner65 rewrite 可到 `0.6615 B/support4`；owner66 rewrite 可到 `0.6731 B/support4`；owner67 rewrite 可到 `0.6846 B/support4`。
- `0.6846` 后出现固定边界：MaxNewChains=1、不同 seed 都只生成同一类新增 owner67 -> firstHit owner18 的非 direct-exit/base-owner-hit 链；该链会让 supportDepth 从 4 掉到 2。
- 对该固定 owner-hit 做 len6、len10 fast rewrite 均无法恢复 supportDepth4；pair rewrite 对两链变体也只能 supportDepth2；Hard anchor 禁止该口会变成 unsolved/support0。
- 临时接受 supportDepth2 债再继续补一根，仍只得到 supportDepth2 或 Drop，未观察到后续自然恢复 supportDepth4。
- 当前判断：sandwich 线在现有 SGP candidate language + fast rewrite 语法下到达局部容量边界；下一个有价值实验不是加深搜索，而是改变 `0.6846` 前后的 SGP 候选语言/owner-hit contract，或设计专门处理 `base-owner-hit but support-shallowing` 的新 rewrite primitive。
- 2026-06-26 后续验证：在 `Build-SeededDirectSGPFillBaselineV1.ps1` 增加 `FailurePocketAvoidHitMode` 与 `FailurePocketSoftAvoidHitOwners`。从 `0.6846` hard avoid owner18 后，SGP 找到新路并达到 `0.7096154/B/supportDepth4`；稳定入口为 `.worktrees/sgp-sandwich-refill/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/probe0684_avoid18_hard_m1_candidates.csv` offset `5`。
- 从 `0.7096154` 继续，hard avoid owner18 + soft avoid owner10/28 仍只得到 `0.7134615/B/supportDepth2`，growth 诊断显示新增链改打 firstHitOwner31。再将 31 加入 soft avoid 后 12/12 变 Drop/support0。结论：0.709 后存在 shallow-hit owner 群，单纯黑/灰名单不能继续保 supportDepth4；下一步必须做 owner-hit support-shallowing rewrite primitive 或 pocket-level atomic rewrite。
- 已在 `Build-FailurePocketFastRewriteV1.ps1` 增加 `SlotExpandRadius/MaxExpandedSlotCells`，把 rewrite 从“原 SGP 链格子内改写”扩展为“原链周围小 pocket 改写”。这修复了 2 格 shallow owner-hit 原本无空间堵链的问题。
- Expanded pocket rewrite 正结果：从 `0.7096` 的 `probe0709_hard18_soft10_28_m1_b01_c01`（新增 owner68 -> firstHitOwner31，`B/support2`）出发，`fpfast_expand_from0709_o68_r2_len8_b01_c04` 达到 coverage `0.7153846`、`solved=True`、`B`、supportDepth `4`、avg/max `4.97/12`。
- 继续一轮：从 `0.7154` SGP 暴露 owner69 -> firstHitOwner34 的 Drop/support0 口，expanded rewrite `fpfast_expand_from0715_o69_r2_len10_b01_c01` 修到 coverage `0.7269231`、`solved=True`、`B`、supportDepth `4`、avg/max `5.03/12`。这是当前 sandwich 研究线最新严格稳定点。
- 下一轮从 `0.7269` SGP 暴露 owner70 -> firstHitOwner10，expanded rewrite 最高可保 supportDepth4 到 `0.7326923`，但 processTier 为 Drop（avg/max `5.34/13`）；`B` 候选仍为 supportDepth2。因此 `0.7269` 是当前严格 commit 点，`0.7327/support4/Drop` 只能作为选择峰值 repair source。
- Debt-line 复测：从 `0.7327/support4/Drop` 继续 SGP 到 `0.7365` 后全部为 Drop/support2（max `14`）；新增 owner71 -> firstHitOwner28 的 expanded rewrite 只有两个 2-cell 选项，结果为 Drop/support2 或 unsolved/support0。结论：该 Drop 债不可自然回收，也缺少可用 pocket 空间；后续应从 `0.7269/B/support4` 严格线继续，并先控制选择峰值/owner70 口，而不是接受 Drop 父本滚动。
- 2026-06-26 深夜续测：严格线已推进到 `0.7365385/B/support4`，入口为 `.worktrees/sgp-sandwich-refill/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/fpfast_expand_from0727_o70_r4_len14_var_unique16_candidates.csv` offset `1`。从该点 SGP 新增 owner71，默认 firstHitOwner10；r4/len14 pair rewrite 与 r5/len18 probe24 都没有产生严格可提交的 `B/support4`，只出现 `Drop/support4` 或 `B/support2`。
- 对 `0.7365` 进行 SGP hard-avoid 复测：hard avoid `18;10` 后 SGP 改打 firstHitOwner28，但仍为 `Drop/support2`；hard avoid `18;10;28` 后 12/12 直接 `unsolved/support0`。结论：0.7365 后的 owner71 口是当前 fast-rewrite/avoid-hit 语法边界，不应继续靠扩大半径或黑名单堆叠盲搜；下一步若继续 sandwich 线，应改变“堵”的目标函数（围绕 opener/choice wave 或新的 owner-hit support-shallowing primitive），而不是把 `Drop/support4` 当正式父本提交。
- Drop 债恢复测试：从 r5/len18 probe24 中唯一 `support4/Drop` 候选（coverage `0.7423077`）继续 SGP 一链，8/8 变为 `Drop/support2`、coverage `0.7461538`、maxChoices `14`，未观察到回 B/A。当前 commit 规则保持：Drop 不能作为 sandwich strict parent，只能作为失败诊断或新 primitive 的输入。

## SGP Sandwich Tail-Safe Boundary - 2026-06-26

- 深夜继续 sandwich strict 线：从 `0.7365385/B/support4` 转入 no-new-head tail/body extension，不新增 head，只延长现有链尾，并用官方 trace 做单格安全 probe。
- tail-safe 路线把严格父本推进到 `0.8576923/B/support4`；替代尾部方向还能到 `0.8596154/B/support4`，但再加常见 tail 单格会掉到 `supportDepth2` 或 `unsolved/support0`。
- 从 `0.8596154/B/support4` 让 SGP raw 冲 `0.89-0.91`：8/8 几何高覆盖但全部 `unsolved/supportDepth0`。growth 诊断显示新增链不是 direct-exit，而是短 owner-hit 链，第一条新增链已使 `supportDepth4 -> supportDepth2`。
- 对第一条 toxic 新链 owner71 的 expanded rewrite（hit owner18）只得到 `B/support2` 或 `Drop/support2`；hard avoid owner18 会让 SGP 改打 owner63，并直接 `unsolved/support0`；对 hit63 的 expanded rewrite 也不能恢复 supportDepth4。
- 当前本地结论：sandwich + tail-safe 语法的严格稳定上限约 `0.86`；0.86 后矛盾不再是“SGP 外逃”，而是“SGP owner-hit 候选语言改写 release/support 时序”。继续上推需要新的 support-preserving owner-hit primitive 或让 SGP 在生成阶段具备 support-safe candidate language，不能只靠事后 fast rewrite/avoid hit。
- 人工体感复核：`tail0857B_alt_ray1_single_c42` 已被用户否决，反馈为“甚至不如量产的”。其 trace 虽为 `coverage=0.8596154/solved=True/B/supportDepth4`，但 `hardStructureV3Class=LocalEasy`，应作为 tail-safe/supportDepth 指标不足以证明高难的负例，不再作为高难候选展示。
- 已新增 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-OwnerHitGrammarFillV1.py`：主动枚举非直出、first-hit 旧骨架 owner、贪心不过峰的小链候选，再交给官方 trace 决定提交。该工具验证了 0.86 后不是“无路”，而是原生 SGP candidate language 太窄。
- Owner-hit grammar 连续推进：`0.8596154 -> 0.8673077 -> 0.8750000 -> 0.8826923 -> 0.8903846 -> 0.8980769`，每轮均找到 `solved=True/B/supportDepth4` 候选，且 choice/outer 未爆。
- 最终用 no-new-head tail 单格收尾得到今晚目标样本：`ownerhit0898_tail_single_c63`，coverage `0.9000000`，官方 trace `solved=True`、`processTier=B`、`supportClosureBestDepth=4`、`avgChoices=4.39`、`choiceP80=7`、`maxChoices=10`、`outerExitHeadCount=10`。
- Final selected CSV：`.worktrees/sgp-sandwich-refill/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ownerhit0900_sandwich_final_selected.csv`。Final level asset：`.worktrees/sgp-sandwich-refill/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/OwnerHitGrammarFrom0898B/tail_single/ownerhit0898_tail_single_c63.asset`。
- 已冻结 review pack 到 `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SandwichOwnerHit0900ReviewPack.asset`，并将 `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack 指向该 pack，方便人工查看。
- 当前生产判断：SGP sandwich 路线可跑通接近满覆盖关卡，但必须把“堵”升级为 support-safe owner-hit grammar selection；优先级为 `solved/supportDepth4` 硬门槛 > A/B tier > choice/outer 曲线 > coverage。
- 2026-06-26 复核校正：`ownerhit0898_tail_single_c63` 的官方 trace 为 `hardStructureV3Class=LocalEasy`、`hardStructureV3Score=0.071`，虽然 `coverage=0.9000/solved/B/supportDepth4`，但不能作为高难命中；只保留为“高覆盖、支撑未炸但体感/结构未过”的边界样本。

## Geometry Supply Owner-Hit Probe - 2026-06-26

- 目标：验证 `SGPPressureHard` 普通量产样本能否只作为几何/同链边供料，不把其 `LocalEasy` 难度逻辑带入高难验收。
- 已扩展 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-OwnerHitGrammarFillV1.py` 的 opt-in 参数：`--supply-level`、`--supply-fit exact|center-crop`、`--supply-require-edges`；默认行为不变。供料模式只允许新增 owner-hit 小链落在供料图映射后的空格中，并可要求相邻步沿供料关卡同链边。
- 小样父本：`orig_seed_usable_v1_01_rolegraph_next5_arrowz_level_154`，base coverage `0.291498`，官方 trace `S/TrueHardCandidate/supportDepth4`。供料图：`sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle`，center-crop `20x28 -> 19x26`，同链边约束。
- 五轮 traced 正信号：best strict coverage `0.3016194 -> 0.3117409 -> 0.3218623 -> 0.3421053 -> 0.3623482`，均保持 `solved=True/processTier=S/hardStructureV3Class=TrueHardCandidate/supportDepth4`；第五轮 84/84 solved、76/84 TrueHard、61/84 supportDepth4+、61 strict pass。
- 结论：几何供料 + owner-hit grammar 是高难专项正方向，至少能在低覆盖 root-growth 阶段保住 TrueHard，不像直接 PressureHard 那样 LocalEasy；但当前仍是低覆盖 proof，不挂 Demo，不与普通量产混线。下一步需要自动多轮 scheduler、anti-repeat/choice-wave 控制和更多 supply/root 组合验证。
- 报告入口：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_probe_summary.md`；selected CSV：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_probe_selected.csv`。
- 2026-06-26 multi-root 复测：`Build-OwnerHitGrammarFillV1.py` 追加 opt-in bundle 模式（`--bundle-count`、`--bundle-pool-count`、`--bundle-same-hit-cap`、`--bundle-require-old-hit`），一次候选可放多条互不重叠、最终 first-hit 老骨架 owner 的供料链，再由官方 trace 筛。
- 高覆盖对照 `root05 + core_burst` 可推到 `coverage=0.6419355`、`solved=True/S/MediumStructure/supportDepth4`，证明高覆盖段不必然 LocalEasy，但它不是 TrueHard。
- 最强高难线为 `root10 + dense_weave + bundle3`：`0.3637681 -> 0.4072464 -> 0.4434783 -> 0.4753623 -> 0.5000000` 均存在 `solved=True/A/TrueHardCandidate/supportDepth4` 候选；最高 coverage 行 `0.5028986` 降为 `HardPotential/support4`。0.50 附近 TrueHard 密度从 r4 的 24/24 降到 r5 的 5/24，说明下一步要加入 class-drift/choice-wave 选择压力。
- 硬但慢的对照：`root98 + dense_weave` 在 `0.3002451` 仍 24/24 `TrueHard/support4`，但吞吐太低；`root76 + dense_weave` 在 `0.3345588` 有 strict TrueHard/support4 候选，但通过密度低于 root10。
- Multi-root 报告：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_multi_root_probe_summary.md`；selected CSV：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_multi_root_probe_selected.csv`。未挂 Demo。

## Tonight High-Coverage Demo Pivot - 2026-06-26

- 本轮从 `0.30` HardLock 父本验证 release-aware SGP：即使 target `0.55/0.60/0.64/0.90`，官方 trace 均为 `solved=False`、`supportClosureBestDepth=0`；父本自身 retrace 为 `5/5 solved/A/supportDepth3`。结论：release-aware SGP 的失败不是 0.598 父本太脆，而是补肉阶段破坏 solve-order/closure。
- 使用 GPT 会话 `6a3be215-05c8-83e8-b5c9-307a492fea69` 复核：今晚若继续研究型产线，主路线应回到 small SGP burst + reverse commit；release-aware SGP 只能作为辅助，不应继续作为 0.9 直冲主线。
- 对 `0.5980392` tail-neutral 父本 + `0.877` SGP slot 做 reverse cap 复测：`max-after-choices=10` 补不上新链，保持 `0.5980392/B/support3/openers10/max10/outer11`；`max-after-choices=11` 仅加 1 链到 `0.6053922`，但变 `Drop/openers11/max11/outer12`。结论：当前 reverse slot refill 在 B-tier 约束下增量极小。
- 可交付 pivot：新增 `Tools/Production/Invoke-SGPPressureHardProductionV1.ps1`，可复跑 Unity `BuildSgpPressureHardTrialPack` 并生成官方 trace input；完整 Unity 生成日志 `.codex-run/sgp_pressure_hard_production_v1_fullrun_unity.log` 已出现 `SGP Pressure Hard Trial finished` 和 `Exiting batchmode successfully now!`；随后 `-SkipUnity` 官方 trace 复核成功，metrics 为 `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_hard_production_v1_fullrun_trace_metrics.csv`。
- 复核后严格筛选：`SGPPressureHardTrialPack` 4 个 high-coverage 候选里，干净命中今晚目标的是 `sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave`：source coverage `0.978`，official trace `solved=True/processTier=B/supportDepth3/openers3/avgChoices3.78/maxChoices6`。其它行要么 supportDepth2，要么 Drop，不作为 clean hit。
- Demo 已切到 filtered 单关包 `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardProductionV1Pack.asset`（GUID `afdb809ddc1a4502910d678912899a75`），用于今晚人工查看 0.9+ coverage 有难度成品关卡。
- 后续路线区分：`SGPPressureHardTrial` 是当前可演示高覆盖成品线；sandwich/reverse 是研究线，当前稳定上限约 `0.68 B/support4`，需新 owner-hit/support-shallowing rewrite primitive 才可能继续上推。
- 2026-06-26 再次运行 `Tools/Production/Invoke-SGPPressureHardProductionV1.ps1 -SkipUnity`，official trace 输出 `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_hard_production_v1_metrics.csv`。`dense_weave` 仍是 clean hit：source coverage `0.978`、`solved=True`、`B`、supportDepth `3`、avg/max `3.78/6`、outer `6`。

## V1.31 Family Pilot Readiness - 2026-06-26

- 当前推荐主库基线：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV131ExtendedBalancedReviewPack.asset`，GUID `91a29088725441d3b604fa2e66f8d71e`；当前 worktree Demo 已临时切到 Original Seed Merged Usable Root V1 review pack 供人工筛 root。
- V1.31 复核：108 关，6 个 family，每类 18 个；`root_variant_library_v1_31_extended_balanced_frozen_trace_joined_summary.md` 记录 108/108 solved、108/108 intendedFamilyGatePass。
- 已抽取每 family 一个 frozen 父本作为小规模生产输入：`.codex-run/v131_frozen_one_root_per_family_recommended.csv`；对应 trace：`.codex-run/v131_frozen_one_root_per_family_trace_metrics.csv`。
- 现有 `Build-RootCanvasVariantsV1.ps1`、`Build-RootPeripheralJitterVariantsV1.ps1`、`Build-RootClusterRemapVariantsV1.ps1` 能吃该 SourceCsv；jitter/remap 必须显式传 V1.31 frozen trace，不能用默认旧 trace。
- 性能判断：6-root pilot 不需要先做性能优化；先限 `MaxRoots=6`、小 `VariantsPerRoot`、只 trace 新候选。若扩到百级产量，再做 trace/cache/按 family 分批优化。

## Original Seed Root Merge Review - 2026-06-26

- 已合并原始 seed root 候选源：rolegraph library、rolegraph next/batch、extractable V1、all-seed extractable V2 和 strict role full scan；输出全量表 `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_merged_usability_v1_all_candidates.csv`。
- 筛选表 `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_merged_usable_v1_selected.csv`：按 solved、supportDepth>=3、choice 上限、tier 和 source-level 去重选出 16 个候选。
- 因 archive 中旧 LevelDefinition 长路径超过 Windows `MAX_PATH`，没有整目录恢复；改为逐条从 `_AssetArchive/20260624_assetdatabase_trim` 精确复制资产，重命名为短 ID 并生成新 GUID。
- 冻结包：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedMergedUsableRootV1Pack.asset`，GUID `1096eb72369f4630ba4b9a09bdac9c27`；冻结关卡目录 `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/OriginalSeedMergedUsableRootV1Frozen`。
- 官方 trace：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_merged_usable_root_v1_frozen_trace_metrics.csv`；结果 16/16 solved，7 S + 9 A，supportClosureBestDepth 全部 3-4。人工筛选 joined audit：`.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_merged_usable_root_v1_frozen_trace_joined.csv`。
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack 当前指向该原始 seed merged review pack，供用户肉眼筛掉同质/勉强样本。注意：该包是人工筛 root 库的候选池，不代表 16 个都已入最终量产 root 库。

## SGP PressureHard Normal Production Closure - 2026-06-26

- 本轮收尾结论：`Tools/Production/Invoke-SGPPressureHardProductionV1.ps1` + `SGPPressureHardTrialPack` 可作为“高覆盖、普通难度”量产线的 V1 基座；它不是高难产线，但已从研究线转为可生产普通关卡的实用突破。
- 当前主项目与 `.worktrees/sgp-rhythm-lab` 的 Demo activePack 均指向 `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardTrialPack.asset`，GUID `acd1590a350614a4e86c901d33b5c5dd`，用于 4 关人工评审；filtered 单关 `SGPPressureHardProductionV1Pack` 仍保留为 clean-hit 示例，不再是当前 Demo 目标。
- 完整 speedcheck：Unity 生成 4 关约 `198.491s`，trace-only 约 `59.986s`；合计约 4.3 分钟/4 关。日志 `.codex-run/sgp_pressure_hard_production_v1_speedcheck_unity.log` 成功退出，trace metrics 为 `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_hard_production_v1_speedcheck_trace_metrics.csv`。
- 4 关 source coverage 均接近满铺：`lock_buckle 0.991/58 chains`、`section_unlock 0.994/61`、`dense_weave 0.978/58`、`core_burst 0.990/61`，portable solved 全部 True。
- 官方 trace：4/4 solved；普通量产口径 `solved + coverage>=0.97 + processTier A/B` 可保留 3/4（`lock_buckle`、`dense_weave`、`core_burst`）；更严格 `supportDepth>=3 + A/B + maxChoices<=8` 仅 `dense_weave`。`section_unlock` 为 Drop，应作为诊断或人工挑选边界，不默认入库。
- 人工体验判断：样本不是无脑爽关，已有一定选择/节奏压力，但 trace 仍多判为 `LocalEasy`，因此定位为“普通难度批量生产”，而不是“hard-core 高难量产”。
- 后续正式拆两条线：普通线优先批量跑 `SGPPressureHardProductionV1`、人工/trace 筛出 A/B 且 coverage>=0.97 的关；高难线继续单独研究 validated root / sandwich / reverse / support-preserving owner-hit primitive，不再拿普通产线指标强行证明高难。
- 链条语言（长链、少弯折、蛇形等）可以作为普通量产线的风格控制层继续加，但先不阻塞当前产线收束；加完必须仍按 official trace + 人工体验筛选。

## PSG/SGP PressureHard as Sandwich Supply Review - 2026-06-26

- 复核 `Tools/Production/Invoke-SGPPressureHardProductionV1.ps1`：当前 PressureHard 生产线通过 Unity `NoMaskProceduralGenerator.BuildSgpPressureHardTrialPack` 生成 4 个高覆盖普通关，再由 `Build-SGPRhythmTrace.ps1` 官方 trace 复验。
- 生产逻辑核心在 `NoMaskProceduralGenerator.TryBuildPeelChain`：从 remaining 空域中选 head/second 并做 peel-style random walk；PressureHard profile 会惩罚外圈 out-head、早期选择峰、local run、近距离连续解，并有 `TryApplyPressureHardFlipGatePass` 后处理翻掉早期开口。
- 最新 source 指标：`SGPPressureHardTrialPack` 4 关 coverage `0.978-0.994`、portable solved 4/4；official trace 4/4 solved，3/4 为 A/B，但全部 `hardStructureV3Class=LocalEasy`。cleanest `dense_weave` 为 `B/supportDepth3/avg3.78/max6/outer6`，但 `localPatchSolveRunMax=13`、`dependencyBraidBadLocalRate=0.742`，不能当真高难。
- 补充复核 `SGPGateAwareTrial` 4 个 asset：official trace 4/4 solved，但 1 B + 3 Drop，supportDepth `0-2`，全部 `LocalEasy`，`localPatchSolveRunMax=10-24`、`dependencyBraidBadLocalRate=0.875-0.983`；GateAware 当前不是高难成品线。
- 正向结论：PressureHard/PSG 更适合作为 sandwich 的“几何/同链边供料器”，而不是直接继承其解题逻辑。已有 `Geometry Supply Owner-Hit Probe root154 + lock_buckle` 证明该用法可从 hard root coverage `0.2915` 推到 `0.3623`，仍保持 `S/TrueHardCandidate/supportDepth4`。
- 下一步若目标是真难关，应使用 validated hard root + PressureHard geometry supply + owner-hit grammar/scheduler：验收以 `TrueHardCandidate + supportDepth>=4 + localPatch/outer/dependencyBraid` 为硬线，coverage 后置；不要把 `coverage>=0.97 + A/B` 当高难验收。

## Mask Production Line Inventory Baseline - 2026-06-26

- 本对话 scope：只处理 mask 关卡产线；不继续 PSG/Pressure-SGP 普通量产线，也不继续 root/sandwich 高难研究线。
- 盘点到 mask 资源入口：`Assets/ArrowMagic/Masks/`、`Assets/ArrowMagic/Masks/Production/HoleLongOuterStrong/`、`Assets/ArrowMagic/SOData/Levels/Production/HoleMask/`、`Assets/ArrowMagic/SOData/Levels/Generated/MaskPreview/HoleV13Top5/`、`Assets/ArrowMagic/SOData/Levels/ShapeExperiment/`。
- 当前 HoleMask 资产池：`Production/HoleMask/Candidates` 68 个、`Early` 2 个、`Early30To40` 1 个；`hole_mask_early_front_manifest.csv` 有 70 行，playableFill `0.602-0.779`、boardFill `0.451-0.721`、chains `22-97`。
- 历史 manifest 引用的 `Assets/ArrowMagic/SOData/Packs/Production/HoleMask/HoleMask_FinalScreening_EarlyFront.asset` 在当前主项目缺失；不能假设旧 HoleMask 正式包已存在，应先重建一个非破坏性 review pack。
- `SeedMaskPatchWindow.RunHoleMaskProductionBatch` 是最接近现有正式 mask 生成的入口，依赖/目录存在；但它会清空 `Assets/ArrowMagic/SOData/Levels/Production/HoleLongOuterStrong/Candidates`，且当前 report 只看到一个 accepted 资产、未看到同步 pack，暂定为“可跑但未闭环稳定”。
- `CampaignHoleProceduralGenerator` 可生成固定 hole-blocker candidates；当前 HoleProcedural 报告显示高覆盖/可解，但 opener/初始可动偏高，适合作 baseline/参考，不等于任意 mask shape 产线。
- 建议 baseline：先从现有 HoleMask 资产重建小 review pack，用 `CampaignSingleLevelValidator.RunValidationForPack` 和 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1` 复核；之后再做隔离输出目录的 SeedMask 小批次 smoke。
