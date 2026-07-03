# Workflow

## Task Startup

1. 阅读根目录 `AGENTS.md`。
2. 阅读 `.agents/memory/PROJECT_CONTEXT.md`、`.agents/memory/WORKFLOW.md`、`.agents/memory/CURRENT_STATUS.md`、`.agents/index/RESOURCE_INDEX.md`。
3. 如果任务涉及脚本、编辑器菜单、关卡、配置、资源包或报告，再读 `.agents/index/SCRIPT_INDEX.md` 和 `.agents/index/LEVEL_INDEX.md`。
4. 先用索引定位资源，再用 `rg` 或精准目录搜索补充上下文。

## Before Editing

- 运行或查看 `git status --short`，区分本次改动和既有改动。
- 读取相关代码、资源索引和相邻实现，优先沿用现有命名、目录和 Unity 资源组织方式。
- 不覆盖、不回退、不删除用户已有改动；如果已有改动影响任务，先理解并顺着它工作。

## Implementation

- 改动保持小范围、可解释、可验证。
- C# 逻辑优先放在现有职责边界内：运行时逻辑在 `Scripts/` 或 package `Runtime/`，Unity 编辑器批处理在 `Assets/ArrowMagic/Editor/`。
- 关卡/包/报告产物放回对应 `SOData/Levels`、`SOData/Packs`、`SOData/Reports` 或交付目录，避免散落在根目录。
- 临时实验可以存在于工作过程中，但不能作为最终正式内容混入提交。

## Validation

- C# 或 package 改动优先做编译验证；Unity 项目可用现有 `.sln` 或 Unity 测试入口，按任务风险选择。
- 编辑器工具改动要检查菜单入口、输出路径、失败处理和是否会覆盖已有资源。
- 关卡生成/导入任务要验证候选资源、包引用和报告是否一致。
- Generated-Root WBP 难度验收按两层走：先用 official trace / Greedy / coverage / root identity 做 `TraceGate`，再用独立 `DifficultyVerify` 和 `PlayerStallAudit` 判断真难证据。需要判断 player-stall 时优先带上 dependency spine audit，用 structural stall 与 spine stall 分开看；不要把 official `A`、低平均可选、高 coverage 或单一 top-path 指标当成 hard 成功。
- Generated-Root WBP 成品化 `0.95+` 验收按新三段产品口径走：official solved/root/pre-materialization/coverage 是 `Overall`，前中段真实卡点是 `Hard-Core Window`，尾端不污染结构是 `Tail Hygiene`。最终全局 process 可以是 `B`，但必须用 `HighCoverageHardCoreProductVerifierV1` 证明 hard core 保留且 tail hygiene 过。
- Generated-Root WBP 的 `dependencyFollowRunMax` 归类为 solver-policy diagnostic，不再作为 standalone 难度门槛或生成失败门槛。只有在同一 board 下做过 fixed-board policy perturbation 后，才能解释 dep-run：若 anti-follow/random 等策略仍 100% solved 且显著降低 dep-run，则高 official dep-run 只是 greedy/official execution footprint；它只能用于 solver 对比、执行偏置调试和 player-model 校准。

## Memory Update

- 长期项目事实更新到 `PROJECT_CONTEXT.md`。
- 明确的技术取舍更新到 `DECISIONS.md`。
- 当前进度、剩余待办、下一步入口更新到 `CURRENT_STATUS.md`。
- 对话里重要但未归类的信息先放到 `CONVERSATION_MEMORY.md`。
- 新增关键路径、脚本、关卡批次时更新对应 index。
- 未验证信息必须写明“状态：待验证”。

## Context Compression Checkpoint

- 对话压缩摘要不能作为可靠长期记忆；只要后续任务需要依赖，就必须沉淀到项目 md。
- 在阶段完成、长时间实验、上下文可能压缩、切换 worktree/分支、合并/清理/交接前，先写 5-10 行 checkpoint。
- checkpoint 写入位置：
  - 当前状态、下一步、风险：`.agents/memory/CURRENT_STATUS.md`
  - 原则、技术取舍、实验结论：`.agents/memory/DECISIONS.md`
  - 暂时重要但未归类的信息：`.agents/memory/CONVERSATION_MEMORY.md`
  - 新路径、新脚本、新关卡/包/报告入口：`.agents/index/`
- checkpoint 只写高信号内容：结论、路径、commit/branch/worktree、验证结果、风险、下一步。不复制大段日志。

## Rosetta GPT Advisor Workflow

- 使用场景：方案分歧、架构取舍、高风险生成器改动、连续实验无突破、需要外部审稿时，通过 Rosetta 咨询 GPT。
- 工具入口：优先使用 `mcp__rosetta.consult`；需要长期 GPT 上下文时，可使用 Rosetta 的命名会话/recall 机制。
- 咨询前先整理 prompt，不发送散乱聊天。默认使用 `GPT-Safe Abstract Prompt`：
  - 当前目标和不可变约束：保留，但抽象成产品/算法问题，不暴露仓库名、客户名、商业计划和项目专有代号。
  - 代码/脚本/报告：不发送本地路径、文件名、GUID、包名、分支名；改用“生成器”“最终 trace gate”“候选包”等角色名。
  - 实测指标和失败分布：优先发送归一化/四舍五入/相对趋势，例如“峰值从 8 降到 5/6”可改成“峰值显著下降且小样通过最终 replay”；只有用户明确授权时才发送精确内部指标。
  - 已尝试并放弃的路线：保留工程结论，但去掉唯一资产名、关卡编号、CSV 名和大段日志。
  - 希望 GPT 判断的问题和可接受输出格式：保留，要求 GPT 给优先级、reject rule、acceptance gate 和下一实验。
- Prompt 分级：
  - `safe_to_send`：抽象算法/工程问题、匿名 operator、趋势性指标、无本地路径、无专有文件名，可直接用 Rosetta。
  - `requires_user_approval`：本地路径、脚本/资源包名称、精确实验表、内部指标全量、截图/附件、可识别项目结构；发送前必须告诉用户风险并获得明确授权。
  - `never_send`：密码、token、私钥、证书、个人隐私、大段未筛日志、未验证猜测、可直接复制项目核心资产的内容。
- 若 Rosetta 因安全审查拒绝：
  - 不绕过、不改用等价外部发送。
  - 先降级为 `GPT-Safe Abstract Prompt` 重写问题。
  - 如果仍必须发送细节，停下来让用户明确授权；若工具仍拒绝，则把安全 prompt 交给用户手动询问，Codex 继续本地验证。
- GPT 回复后先做项目侧审查：是否符合仓库事实、是否满足用户目标、是否可验证、是否会引入过大实验产物。
- 若不认可 GPT 方案，不直接实现；指出分歧点，补充证据或约束，再通过 Rosetta 追问，直到得到可验证共识或明确记录“未达成一致”。
- 达成共识后再落地：原则/取舍写入 `DECISIONS.md`，流程变化写入 `WORKFLOW.md`，新入口写入 index，执行结果写入 `CURRENT_STATUS.md`。
- 若多轮追问仍无法一致，暂停落地，把分歧、证据和下一步问题写入 `CONVERSATION_MEMORY.md` 或 `CURRENT_STATUS.md`，必要时请用户裁决。

## Experiment Convergence

最终收尾前检查：

- `git status`
- 临时文件
- 备份文件
- 调试入口
- 一次性脚本
- 废弃资源
- 失败实验代码

验证成功后，回退、删除或移出工作目录中的冗余实验产物，只保留正式方案。若不确定某项是否为用户改动，不擅自处理，先确认。

## Handoff

- 交接前确认是否有未沉淀的核心对话内容；有则先写入 `.agents/memory/` 或 `.agents/index/`。
- 最终说明要包含：改动文件、验证结果、未处理风险或需要用户确认的既有改动。
- 如果没有运行验证，明确说明原因。
