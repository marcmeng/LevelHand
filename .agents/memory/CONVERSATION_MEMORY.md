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
