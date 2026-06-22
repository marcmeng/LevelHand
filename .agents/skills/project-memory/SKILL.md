# Project Memory Skill

当用户说“按项目记忆继续”“同步背景”“沿用项目工作流”，或任务需要恢复项目上下文时，使用本 skill。

## Required Reading Order

1. 读取 `AGENTS.md`。
2. 读取 `.agents/memory/PROJECT_CONTEXT.md`。
3. 读取 `.agents/memory/WORKFLOW.md`。
4. 读取 `.agents/memory/CURRENT_STATUS.md`。
5. 读取 `.agents/index/RESOURCE_INDEX.md`。

如果任务涉及脚本、编辑器工具、批处理、导入、验证或测试，再读取 `.agents/index/SCRIPT_INDEX.md`。

如果任务涉及关卡、配置、资源包、报告、掩码或导出交付，再读取 `.agents/index/LEVEL_INDEX.md`。

## Operating Rules

- 当前标准项目路径：`F:\Unityproject\ArrowLevel-Hand`
- 先按索引定位，再做针对性搜索。
- 不依赖聊天记忆补全关键事实；必要事实必须能在仓库文件中找到，或明确标记“状态：待验证”。
- 不记录密码、token、私钥、证书内容、隐私、大段日志、未验证猜测和一次性噪声。
- 不擅自回退、删除或覆盖用户已有改动。
- 工作结束时按 `WORKFLOW.md` 的 Experiment Convergence 检查正式改动和实验产物是否分离。

## Memory Writeback

- 长期事实：`.agents/memory/PROJECT_CONTEXT.md`
- 技术决策：`.agents/memory/DECISIONS.md`
- 当前进度、待办、接续点：`.agents/memory/CURRENT_STATUS.md`
- 临时但重要对话信息：`.agents/memory/CONVERSATION_MEMORY.md`
- 资源路径：`.agents/index/RESOURCE_INDEX.md`
- 脚本入口：`.agents/index/SCRIPT_INDEX.md`
- 关卡/配置入口：`.agents/index/LEVEL_INDEX.md`

## Compression Safety

- 不依赖对话压缩摘要保留关键细节；未来仍要用的信息必须写回项目 md。
- 如果上下文可能压缩，或正在阶段收尾、切换 worktree/分支、合并/清理/交接，先写一个 5-10 行 checkpoint。
- checkpoint 只记录结论、路径、commit/branch/worktree、验证结果、风险和下一步；不要复制大段日志。

## Rosetta GPT Advisor

- 需要外部方案顾问时，优先通过 `mcp__rosetta.consult` 咨询 GPT；GPT 是 second opinion，不是项目决策者。
- 咨询 prompt 必须包含目标、证据、关键路径、失败尝试、约束、验收指标和具体问题。
- GPT 方案若不被项目侧认可，不落地；补充细节继续追问，直到形成可验证共识或记录未达成一致。
- 达成共识后才写入项目规范或实现：决策到 `DECISIONS.md`，流程到 `WORKFLOW.md`，路径到 index，当前状态到 `CURRENT_STATUS.md`。

## Path Migration

如果项目换盘、换目录或换仓库名，同时更新：

- `AGENTS.md`
- `.agents/memory/PROJECT_CONTEXT.md`
- `.agents/memory/DECISIONS.md`
- `.agents/memory/CURRENT_STATUS.md`
- `.agents/index/RESOURCE_INDEX.md`
- `.agents/skills/project-memory/SKILL.md`

必须明确写：

- 当前标准项目路径：新路径
- 旧路径：旧路径，仅作参考，不再作为默认工作目录
