# Resource Index

索引只做导航，不复制大段内容。路径以仓库根目录为基准；当前标准项目路径是 `F:\Unityproject\ArrowLevel-Hand`。

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `AGENTS.md` | 项目记忆总入口 | 每次任务开始、路径迁移、工作流规则变化 |
| `.agents/memory/PROJECT_CONTEXT.md` | 项目长期背景和目录职责 | 新对话恢复、项目结构判断、迁移记录 |
| `.agents/memory/WORKFLOW.md` | 统一工作流 | 任务开始、编辑前、验证和收尾 |
| `.agents/memory/DECISIONS.md` | 技术和协作决策 | 需要理解为什么这样组织、是否能改规则 |
| `.agents/memory/CURRENT_STATUS.md` | 当前状态、待办、接续点 | 接手任务、判断 dirty worktree 和下一步 |
| `.agents/memory/CONVERSATION_MEMORY.md` | 临时但重要的对话信息收件箱 | 对话里出现尚未归类的重要信息 |
| `.agents/index/SCRIPT_INDEX.md` | 编辑器工具和脚本入口 | 查找生成、导入、验证、UI、IO、测试脚本 |
| `.agents/index/LEVEL_INDEX.md` | 关卡、包、报告、配置入口 | 关卡/资源包/报告/掩码任务 |
| `ProjectSettings/ProjectVersion.txt` | Unity 编辑器版本 | 打开项目前确认 Unity 版本 |
| `Packages/manifest.json` | Unity package 依赖 | 依赖变化、包冲突、Unity package 排查 |
| `Packages/com.pixelbug.arrow-level-generator/` | 可移植关卡生成包 | DTO、验证、评分、规划、直接矩形生成逻辑 |
| `Assets/ArrowMagic/Docs/Arrow Magic Docs.pdf` | 原始项目文档 | 需要查游戏/资源说明而代码不足时 |
| `Assets/ArrowMagic/Scenes/` | Unity 场景入口 | 运行、UI、演示、创建流程相关任务 |
| `Assets/ArrowMagic/Scripts/` | 运行时代码 | Board、IO、UI、生成适配、ScriptableObject 逻辑 |
| `Assets/ArrowMagic/Editor/` | Unity 编辑器工具 | 批处理生成、导入、候选池、验证、导出、实验 |
| `Assets/ArrowMagic/Editor/Tests/` | 编辑器测试 | 修改核心生成/Board/渲染逻辑后验证 |
| `Assets/ArrowMagic/UI Toolkit/` | UXML/USS | HUD、按钮、棋盘和 UI Toolkit 样式任务 |
| `Assets/ArrowMagic/SOData/` | ScriptableObject 数据根 | 关卡、包、报告、调色板、音效/VFX 数据 |
| `Assets/ArrowMagic/Masks/` | 掩码资源根 | 生产/实验掩码、形状批次、mask 相关生成 |
| `Assets/ArrowMagic/Reports/` | 项目报告资源 | 生产报告和人工检查材料 |
| `Exports/` | 导出交付物 | 交付 zip/xlsx/readme 检查；注意可能有既有产物 |
| `.codex-run/` | Codex 运行产物 | 仅在排查本地自动化或清理临时产物时看 |
| `TempContactSheets/` | 临时 contact sheet 输出 | 图像审查或实验收尾清理时看 |
| `Library/`, `Temp/`, `Logs/`, `obj/`, `UserSettings/` | Unity/IDE 生成目录 | 通常不作为源内容；不要提交 |
