# RCH Experiment Line Archive - 2026-06-27

状态：已从主接续线移出。新对话默认不要把本档案内容当当前任务方向、baseline、clean floor 或生产母体；只有用户明确要求继续 RCH/high-root 研究时才读取。

## Archive Reason

- 这条线在一天内产生了大量相互修正的 RCH 实验结论，已经开始污染“当前主线”的默认上下文。
- 用户决定开新对话重新做，因此项目核心记忆只保留归档指针；RCH 细节移到本档案和 `RCH_LONGCHAIN_NO_ROOT_RECORDS.md`。
- 实际脚本、报告、关卡资产暂不移动/删除；很多位于 dirty worktree 或未跟踪目录，后续若要清理必须单独确认来源。

## High-Level Goal That Was Explored

- RCH 目标不是普通 PSG 的“筛得更难”，而是高覆盖、可解、同时保持 A/Hard/TrueHard 倾向的高难生成。
- 普通 PSG 已证明能接近满铺，但会变爽关/LocalEasy；RCH 的核心问题是“满铺后难度不塌”。
- 用户反复纠偏：不能按“一条链一条链加、每一步卡难度”的方式推进；应该先整关规划 solve waves、relation contracts、basin/choke duties、chain heads/bodies/block cells、intentional empty，再一次性 materialize，最后用 official trace/audit 验收。

## What Was Actually Proven

- Reverse-CSSC predecessor line proved full-grid reverse cutting can make `1.0` coverage solved boards, and centered two-gate contracts can reach official `S/TrueHardCandidate` on a constructed full grid. It is archived with this RCH line because it shares the same risk: full-board compile-first ideas can look like the right direction while still failing to become the current root-preserving, high-coverage production baseline. Do not treat Reverse-CSSC assets or decisions as active continuation points unless the user explicitly restarts this experiment family.
- 低覆盖 hard skeleton 成立：`Build-RCHSkeletonRelationGraphV0.py` 的 t22 relation skeleton 可稳定到 coverage 约 `0.29-0.316`，22 条短/中链，official trace 多数/全部 TrueHardCandidate，supportDepth 高。难度可以来自 planned parent-child 阻挡关系图。
- Corridor-wave 成立但容量低：`Build-RCHSkeletonCorridorWaveCompilerV1.py` 能把 skeleton 的 child->parent ray corridor 编译成 early guard wave，coverage 约 `0.42-0.46`，难度强。
- Global wave-slot 成立但仍只覆盖 skeleton corridor：`Build-RCHGlobalWaveSlotCompilerV1.py` 全局分配 direct seed guard / relay guard，可到约 `0.50-0.52` 且强，但容量不上去。
- Slot/rank contract 可扩到中覆盖：`Build-RCHFullBoardWavePlanCompilerV1.py` 的 `release owner -> new chain -> target` rank contract 可到约 `0.72`，official solved，A/B，但主要是 MediumStructure。
- Cell-rank 能高覆盖可解但难度塌：`Build-RCHCellRankPlanCompilerV1.py` 可到约 `0.82` 且可解，但 official 全 LocalEasy，说明高覆盖可解不是瓶颈，切链语言才是瓶颈。
- Temporal fill 能到 `0.95+` 可解但难度塌：`Build-RCHWholeBoardTemporalFillV1.py` interval packing 可到 `0.9522727`、official 6/6 solved，但全 LocalEasy。链级 blame 显示 early capacity-only temporal fillers 是主要污染源。
- Contract Segment Grammar 低覆盖 proof 成立：`Build-RCHContractSegmentGrammarV0.py` smoke7 低覆盖生成 5 个 cross-basin contract segments，official 6/6 S/TrueHardCandidate、antiLocal 高、supportDepth4。它证明“链条有语义责任”方向正确，但不是高覆盖突破。

## Negative Boundaries

- 从零满铺/普通 full-grid relation compiler 可满铺可解，但会 Drop/LocalEasy；高覆盖可解本身不是 RCH 成功。
- Generic filler、free leaf、capacity-only temporal shell 会把难度改写成局部清扫。
- 只给链贴 basin/rank/region 标签不够；segment 本身必须承担 trace-visible 因果职责。
- 4x2 tile、random path、root graph folded guard、port-pair post-fill、DirectNonLocalLatestMesh band variants 都不是当前 clean baseline。长链/无 root/污染记录见 `RCH_LONGCHAIN_NO_ROOT_RECORDS.md`。
- `rch_direct_nonlocal_latest_mesh_v1_probe9_rootcontract` 已降级为 validator-clean long-band negative boundary：12/12 solved、contract validator 通过，但 hard rows 只有 19/21/23 chains、maxChain40。它只证明 validator 能抓回 root/latest-blocker 正确性，不证明路线可作为 RCH baseline。

## Last Useful Rule Set

- 每条新增 segment 必须在生成时明确：
  - source basin / release owner；
  - target owner 或 choke；
  - body 实际挡住哪个 owner ray；
  - head 由哪个更早 wave 释放；
  - 是否形成 cross-basin delay / block / suppress；
  - head ray 上所有 blocker 都早于本 segment；
  - body 不能自挡自己的 head ray；
  - 早期 segment 的完整 release head ray 不能被后续 segment 隐藏。
- 新增链不能只是 path chunk、rank bucket、basin 标签或 cleanup tail；它必须成为 basin/owner/choke 之间 trace-visible 的 contract segment。

## Main Scripts And Reports

- Reverse-CSSC predecessor archive-only scripts: `Build-ReverseCSSCWaveCompilerV0.py`, `Build-ReverseCSSCContractCompilerV1.py`, and the Reverse-CSSC use of `Build-ChainRelationAuditV1.py`. These are not current mainline script entries; search them directly only when explicitly复盘或重启 Reverse-CSSC/RCH.
- `Build-RCHSkeletonRelationGraphV0.py`：t22 低覆盖 hard skeleton proof。
- `Build-RCHSkeletonCorridorWaveCompilerV1.py`：corridor guard/wave proof。
- `Build-RCHGlobalWaveSlotCompilerV1.py`：全局 corridor slot proof。
- `Build-RCHFullBoardWavePlanCompilerV1.py`、`Build-RCHCellRankPlanCompilerV1.py`、`Build-RCHGlobalInterferenceCompilerV1.py`：全局排序/覆盖/调度诊断。
- `Build-RCHFullBoardConstraintCompilerV0.py`、`Build-RCHContractSegmentGrammarV0.py`、`Build-RCHContractFieldCompilerV1.py`、`Build-RCHWholeBoardContractPlanV0.py`、`Build-RCHWholeBoardPathCoverV0.py`：从 segment grammar 到 whole-board plan 的中间 proof。
- `Build-RCHWholeBoardTemporalFillV1.py`、`Build-RCHTemporalFillBlameAuditV1.py`：0.95 coverage solvability proof 与 LocalEasy blame。
- `Build-RCHDirectNonLocalLatestMeshV1.py`：source validator 修复和 long-band negative boundary。
- 具体路径、CSV、trace/audit 名称留在历史 git diff 或旧 index 中；主索引不再展开。

## Safe New-Conversation Starting Point

- 不要从本档案自动继续实验。
- 如果用户明确要重启 RCH，应从问题定义重新开始：设计整关 contract segment grammar / whole-board planner，而不是接着调 coverage、rank、direct budget、tail、tile、fold、port-pair、RSO split 或 DirectNonLocalLatestMesh band。
- 新 RCH 验收至少要同时满足：coverage 接近目标、official solved、process A 或更好、non-LocalEasy/HardPotential 或更好、source-side contract validator 通过、official relation audit 能看到 planned cross-basin/choke contract edges、链条形态不是长 slab/少数长 band。
