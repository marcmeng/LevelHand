# Campaign500 Final V11 正式项目交付说明

## 基线

- 对比基线：`Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PlanPreviewPack.asset`
- 基线含义：2026-06-13 生成的上一版最终 500 关包，不包含今天的 V1-V11 中间实验版本。
- 当前最终包：`Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500FinalPack.asset`

## 资源

- `ProjectAssets/`：可复制进正式项目的 Unity 资源，包含最终 LevelPack 和 500 个单关 LevelDefinition。
- 单关资源放在：`ProjectAssets/Assets/ArrowMagic/SOData/Levels/Campaign500FinalV11/`
- 为避免 Windows 长路径，单关资源已重命名为 `L001_xxx.asset` 形式，但 `.meta` GUID 保留，LevelPack 引用不会丢。

## 明细表

- `campaign500_final_v11_manifest.csv`：500 关完整明细，含类型、难度、验证标记、shapeId/中文名、复制后资源路径。
- `campaign500_v11_diff_vs_20260613_final500.csv`：相对上一版最终 500 关的逐关差异。
- `campaign500_final_v11_shape_usage.csv`：仅 shape 关卡，标注具体 shape。

## 统计

- 总关卡：500
- 相对上一版修改关卡：153
- Shape 关卡：100

### 当前类型分布

- hole: 50
- normal: 350
- shape: 100

### 修改关卡类型分布

- hole: 12
- normal: 45
- shape: 96

### Shape 示例

- L4: crystalball / 水晶球
- L6: eraser / 橡皮擦
- L9: ruler / 标尺
- L13: magicwand / 魔法杖
- L23: train / 火车
- L27: crownvarianta / 皇冠
- L32: wavecrest / 浪花
- L38: suspensionbridge / 悬索桥
- L43: sunglasses / 太阳镜
- L48: kite / 风筝
- L53: camera / 相机
- L58: bicycle / 自行车
- L63: sailboat / 帆船
- L67: pager / 寻呼机
- L73: windmill / 风车
- L78: pencil / 铅笔
- L83: ballmouse / 轨迹球鼠标
- L87: racingkart / 卡丁车
- L93: wrappedcandy / 糖果
- L98: phonehandset / 电话听筒
