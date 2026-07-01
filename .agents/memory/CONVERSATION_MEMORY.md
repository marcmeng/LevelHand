# Conversation Memory

## 2026-06-18 - Project Memory Protocol

- 状态：已归档到项目工作流
- 用户要求项目记忆机制可复用于其他项目，核心思想是：不依赖对话记忆，让项目仓库自己携带上下文、工作流、资源索引和交接规则。
- 分层沉淀规则：长期事实到 `PROJECT_CONTEXT.md`，技术决策到 `DECISIONS.md`，当前进度到 `CURRENT_STATUS.md`，临时但重要信息到 `CONVERSATION_MEMORY.md`，资源路径到 `RESOURCE_INDEX.md`，脚本入口到 `SCRIPT_INDEX.md`，关卡/配置入口到 `LEVEL_INDEX.md`。
- 收尾规则：实验可以保留在尝试过程中，但最终验证成功后必须清理、回退或移出冗余实验产物，只保留正式方案。

## 2026-06-22 - GPT Collaboration Thread For SGP Hard Lane

- 状态：已提升为通用 Rosetta GPT Advisor 协议；本条保留 SGP hard lane 的特定上下文。
- 用户指定：针对 SGP 高难关 / hard-lane / causal bridge / SupportClosure / RayFirstBridgeSlot 这条问题线，后续向 GPT 咨询时优先使用用户 Chrome 中已打开并切到 Pro 扩展/强智能模式的 ChatGPT 对话。
- 对话标题截图显示为：`高难关卡设计建议`。
- 原因：该对话已有本问题长期上下文，用户可手动控制智能模式；比 Proxima 泛 `chatgpt` provider 更稳定、更可控。
- 协作原则：Codex 负责整理实测数据、发窄问题、裁剪 GPT 建议、执行脚本和验证；GPT 只作为路线审稿/架构 second opinion，不直接决定实现。

## 2026-06-24 - HeadSlot / OpenDebt Fill Logic

- 状态：待实现验证。
- 用户指出当前高覆盖补肉逻辑仍不严密：新增链当前可直接消不一定坏，因为后续链可能再堵住它；平均选择数也不应逐步硬卡，难关允许阶段性选择起伏。
- 已通过 Rosetta 咨询 GPT 并二审修正：不要用“可消/不可消”二分 HeadSlot；应使用 `CLOSED_SLOT / OPEN_DEBT_SLOT / INVALID_SLOT / PROTECTED_BREAK_SLOT` 状态机。
- 关键取舍：`free head` 不再自动 hard reject；若同一逃逸射线上存在 bounded future blocker candidate，则归为 `OPEN_DEBT_SLOT`，必须在同一小 batch 内用一个补偿链 D 结清。
- V1 边界：debt depth 不递归，C 产生 open debt 可找一次 D；若 D 继续产生 open debt 或 motif/support-lock 被破坏，整批 reject。
- 下一步实验建议：以已验证约 0.50 覆盖父本为输入，做 `OPEN_DEBT_SLOT + single D compensation` 小实验，只验证 solved、motif preserved、无 debt cascade、选择曲线不爆。
## 2026-06-25 - GPT advisor sync for Campaign500 V12 hardening

- User asked to sync the current V10/V11 hardening result and next plan into ChatGPT conversation `6a3be215-05c8-83e8-b5c9-307a492fea69`.
- Sent a safe abstract context pack: target is reducing free sweep/outer exits while preserving Greedy solvability and visual quality; V11 reduced early peel-layer outer exits but not current-frame direct outer exits.
- GPT agreed with dual gating: current-frame direct outer + early peel-layer outer must be separate metrics.
- GPT's key V12 recommendation: split future peel outer into `Persistent Boundary Exit (PBE)` and `Newly Exposed Exit (NEE)`.
  - PBE: direct outer already exists in the initial board but only becomes clearable in a later peel wave; this is a boundary topology/structure issue.
  - NEE: not direct outer initially, but becomes direct outer after earlier layers clear; this is propagation/release control.
- Status: 待验证. V12 should first be analysis-only: add PBE/NEE columns and distribution logs before routing different operators to each category.
- Status update: 已验证小样. V12 analysis report `campaign_hardening_v12_pbe_nee_20260625_112948.csv` showed future-layer leaks in the 3-pair V11 sample are all NEE (`futurePBE avg=0`), while current direct outer/PBE remains unchanged. Next implementation should route current PBE to boundary structure repair and NEE to peel-aware propagation gates.
# GPT Alignment - Release Lane Constraint Direction - 2026-06-25

- User asked Codex to align the PinField failure summary and next plan with GPT in ChatGPT conversation `6a3c8ed6-765c-83e8-a56b-386eca569a68`.
- GPT agreed with the main diagnosis: packing/SGP capacity is not the bottleneck; cell-level Pin/RoleMap is the wrong abstraction because it cannot preserve trace-level temporal release semantics.
- GPT cautioned that `release-lane ownership/order field` is still a strong hypothesis, not yet proven sufficient. It must stay a projection constraint layer from trace, not become a second trace/enumerator.
- Suggested minimal V1 fields: `lane_id`, `owner_node`, `release_wave`, `protected_cells`, `allowed_interaction_mode` with modes like `BLOCK_ONLY`, `PASS_THROUGH`, `DEPENDENCY_ATTACH`, `NO_TOUCH`.
- Suggested validation: compare raw SGP vs soft-lane vs hard-lane, and add a lane-shuffle test. Success requires solved/supportDepth retention plus bounded outer/choice; low outer/choice alone is not enough.
- Status:待验证；not yet promoted to DECISIONS until local lane-only experiment confirms or rejects it.

## 2026-06-25 - Authoritative GPT Thread For SGP-3L / Closure Work

- User explicitly specified ChatGPT conversation URL `https://chatgpt.com/c/6a3c8ed6-765c-83e8-a56b-386eca569a68` and said to remember it.
- For future GPT alignment on SGP-3L, closure shadow, support witness, post-kernel native direct chains, sandwich/root refill, and related high-coverage difficulty generation work, Codex should pass `conversationId=6a3c8ed6-765c-83e8-a56b-386eca569a68` to Rosetta when possible.
- Do not default this problem line to a separate Rosetta `recall` thread unless the explicit conversation ID is unavailable or the user asks for a new thread.
## 2026-06-25 - GPT sync for Campaign500 hardening V13

- User asked to sync conclusion/plan with GPT and continue. Codex asked GPT conversation `6a3be215-05c8-83e8-b5c9-307a492fea69` a compact follow-up after V12BDR: one-cell hook solved 3/3 and reduced direct/PBE `12.33->10.67`, but was still visually mild.
- GPT recommended next implementing `two-cell inward hook / endpoint inset` first, deferring double-end stitch and using endpoint merge only as a low-frequency auxiliary.
- Codex implemented V13BDR2 as a stronger branch from V11: two-cell inset first, then V12 one-cell hook fallback. Latest accepted demo pack has two strong pairs and is mounted in Demo.

## 2026-06-25 - GPT sync for Campaign500 hardening V14

- User asked to sync GPT result/plan again and continue. Codex sent V13BDR2 results to GPT: L387 accepted `18->12` PBE/direct, L405 accepted `10->8`, L173 skipped `9->8`.
- GPT agreed that V14 endpoint merge/compression is appropriate only as a low-frequency boundary redundancy cleanup pass, with PBE target `8-12%`, NEE near-frozen, opening not increasing, and no new edge fragments/straight sweep bands.
- Codex implemented V14CMP from latest V13 accepted outputs. L405 accepted with chains `167->165`, direct/PBE `8->6`, opening `8->6`, NEE unchanged `21->21`; L387 skipped.

## 2026-06-25 - Sandwich Line Compression Before V2 Validation

- User wants a separate continuation focused on the "sandwich" line: validated skeleton/root -> SGP density fill -> difficulty/outer opener wave explodes -> protect skeleton/support owners -> peel current non-skeleton clearable chains -> refill peeled space with new pins/root/gate chains -> hand back to SGP.
- Prior evidence: raw SGP can raise coverage to about `0.52-0.56` while keeping supportDepth, but creates a boundary direct-exit opener wave and Drop choice curve. Protected peel reliably restores A-tier/supportDepth=4 but lowers coverage.
- Growth-order reports showed first and second SGP passes both added almost entirely layer-0 direct-exit initial openers (`13/13`, then `11/11`). Min-head-layer and direct-exit caps are negative controls, not solutions.
- V0 refill (`Build-WavePeelReleaseScaffoldGroupV0`) proved peeled cells can be refilled with owner-hit chains without making the added chains immediate direct exits, but it stalls around `0.42-0.43` and the next SGP pass still recreates opener waves.
- Current validation hypothesis: the missing step is not generic pins, but "peeled opener wave -> role/cluster/owner schedule -> gate/blocker scaffold". Before opening a new worktree, run a small V2 validation in existing `sgp-rhythm-lab` to see whether scheduled refill beats random owner-hit V0.

## 2026-06-29 - Public Git Scan For Skeleton PSG Reverse Route

- 状态：待验证；本条是公开代码调研结论，不是已落地实现。
- 扫描公开仓库：`sergev/goarrows`、`amidos2006/GraphDungeonGenerator`、`tcoxon/metazelda`、`mxgmn/MarkovJunior`、`miki151/sokoban`，临时 clone 在 `.codex-run/public_git_scan/`。
- 结论：没有可直接搬的 ArrowMagic 完整生成器；可借的是架构。GraphDungeonGenerator/MetaZelda 都支持“先生成 mission/precondition graph，再 layout/space realization，再验收”的路线。
- 对当前 Skeleton->PSG：反解对象应是 full-ray solver topology / precondition DAG，而不是逐格或逐链反向补肉。几何实现应作为 topology realizer 的后端。
- Sokoban 反向生成只适合动作可逆、局部校验充分的状态空间；ArrowMagic 的 ray blocker 会产生全局 full-ray 依赖，不能照搬“从终局拉回”的局部反向。
- goarrows 的同类规则提供一个有用单调性：只删除不新增时，ray clear 一旦成立不会失效；但它用 grow + greedy accept，适合可解性，不适合难度生产。
