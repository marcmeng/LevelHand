# Project Memory

本仓库使用项目内建记忆机制。不要依赖对话记忆来恢复上下文，优先让仓库里的文档说明当前项目背景、工作流、资源入口和交接规则。

## Start Here

- 每次开始任务前，优先阅读 `.agents/memory/PROJECT_CONTEXT.md`、`.agents/memory/WORKFLOW.md`、`.agents/memory/CURRENT_STATUS.md`、`.agents/index/RESOURCE_INDEX.md`。
- 如果用户说“按项目记忆继续”“同步背景”“沿用项目工作流”，使用 `.agents/skills/project-memory/SKILL.md`。
- 如果任务涉及脚本、编辑器工具、关卡、配置、资源包或报告，再读取 `.agents/index/SCRIPT_INDEX.md` 和 `.agents/index/LEVEL_INDEX.md`。
- 查找资源前先看索引，避免全仓库盲翻；索引不足时再用 `rg` 或针对性目录搜索。

## Memory Rules

- 长期事实写入 `.agents/memory/PROJECT_CONTEXT.md`。
- 技术决策写入 `.agents/memory/DECISIONS.md`。
- 当前进度、待办、接续点写入 `.agents/memory/CURRENT_STATUS.md`。
- 临时但重要的对话信息先放入 `.agents/memory/CONVERSATION_MEMORY.md`，后续再归档。
- 资源路径写入 `.agents/index/RESOURCE_INDEX.md`。
- 脚本入口写入 `.agents/index/SCRIPT_INDEX.md`。
- 关卡、配置、资源包入口写入 `.agents/index/LEVEL_INDEX.md`。

禁止记录密码、token、私钥、证书内容、隐私、大段日志、未验证猜测和一次性噪声。未验证信息必须标记“状态：待验证”。

## Context Compression Rule

- 不把对话压缩摘要当成长期记忆来源；未来还要依赖的信息必须写入项目 md。
- 在阶段性完成、长时间实验、上下文可能压缩、切换 worktree/分支、合并/清理/交接前，先做一次 5-10 行 checkpoint。
- checkpoint 优先写入 `.agents/memory/CURRENT_STATUS.md`；若是原则或取舍，写入 `.agents/memory/DECISIONS.md`；若尚未归类，先写入 `.agents/memory/CONVERSATION_MEMORY.md`。
- checkpoint 只记录结论、路径、commit/branch/worktree、验证结果、风险和下一步，不复制大段日志。

## Rosetta GPT Advisor Rule

- 需要外部方案审稿、架构 second opinion、或多轮实验仍没有收敛时，可以通过 Rosetta 咨询 GPT，把 GPT 当作方案顾问。
- GPT 不直接决定项目方向；Codex 仍必须结合项目记忆、仓库事实、实测数据和用户目标做最终判断。
- 给 GPT 的 prompt 必须带上高信号上下文：目标、当前证据、关键路径、已失败方案、硬约束、验收指标和希望它审查的问题。
- 如果 GPT 方案与项目判断、实测数据或用户目标不一致，不要直接落地；必须补充缺失细节继续追问，直到形成可验证共识，或明确记录未达成一致。
- 只有在方案被项目侧认可、验收方式清楚、风险和边界明确后，才能把它写入 `DECISIONS.md`、`WORKFLOW.md`、相关 index 或正式实现。

## Path Rule

- 当前标准项目路径：`F:\Unityproject\ArrowLevel-Hand`
- 旧路径：未在本仓库记忆中确认；如后续发现，只能作为参考路径记录，不能作为默认工作目录。
- 如果项目换盘、换目录或换仓库名，同步更新 `AGENTS.md`、`.agents/memory/PROJECT_CONTEXT.md`、`.agents/memory/DECISIONS.md`、`.agents/memory/CURRENT_STATUS.md`、`.agents/index/RESOURCE_INDEX.md` 和 `.agents/skills/project-memory/SKILL.md`。

## Experiment Convergence

尝试性解决问题时可以保留临时实验；最终验证成功后，必须回退、删除或移出工作目录中的冗余实验产物，只把正式方案纳入最终改动。实验经验可以沉淀到记忆，不能作为正式内容提交。

收尾时检查：

- `git status`
- 临时文件
- 备份文件
- 调试入口
- 一次性脚本
- 废弃资源
- 失败实验代码

不确定是不是用户改动时，不要擅自回退，先确认。
