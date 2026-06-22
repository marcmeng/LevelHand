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

## Long-Term Working Agreements

- 资源查找顺序：先看 `.agents/index/`，再做针对性搜索。
- 索引只做导航：路径 + 用途 + 什么时候该看它，不复制大段文件内容。
- Unity 资源改动要注意 `.meta` 配套文件；不要无意触发大面积资源重导入。
- 不擅自回退或清理已有工作树改动，尤其是未确认来源的关卡、掩码、报告和导出产物。
- 生成、导入、验证相关工作完成后，要把正式结果和实验产物分离。

## Path Migration Notes

- 当前标准项目路径：`F:\Unityproject\ArrowLevel-Hand`
- 旧路径：未确认。
- 状态：已确认当前工作目录；旧路径信息待后续明确后再记录。
