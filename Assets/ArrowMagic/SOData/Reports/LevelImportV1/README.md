# Level Import V1 候选池汇总

Updated: 2026-06-11

这个目录是第一版关卡导入前的独立盘点区。当前不移动、不改写任何关卡资产，只把普通、Shape、Hole 三条候选线的来源、数量和关卡级配置统一记录下来，方便下一步做正式导入 LevelPack。

## 来源池

| Pool | 数量 | 定位 | 来源 |
|---|---:|---|---|
| `normal_campaign500` | 500 | 主线普通关卡骨架，已经按难度分桶 | `F:/Unityproject/ArrowLevel-Hand/.worktrees/nomask-procedural-generator/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PlanPreviewPack.asset` |
| `shape_final_with_supplement` | 147 | 可读特殊图形关卡；不含 hole/blocker mask | `F:/Unityproject/ArrowLevel-Hand/.worktrees/shape-procedural-mask-fill/Assets/ArrowMagic/SOData/Packs/ShapeExperiment/ProceduralMaskFillFinalWithSupplementPack.asset` |
| `shape_early_prop` | 4 | 非常早期的小道具特殊图形关卡 | `F:/Unityproject/ArrowLevel-Hand/.worktrees/shape-procedural-mask-fill/Assets/ArrowMagic/SOData/Packs/ShapeExperiment/EarlyPropSatisfyingPack.asset` |
| `hole_mask_early_front` | 70 | 中空 blocker 机制关卡；空洞区域是 blocker/场地约束 | `F:/Unityproject/ArrowLevel-Hand-HoleExperiment/Assets/ArrowMagic/SOData/Packs/Production/HoleMask/HoleMask_FinalScreening_EarlyFront.asset` |

当前总索引候选数：**721**。

## 生成文件

| 文件 | 作用 |
|---|---|
| `candidate_pool_summary.csv` | 所有池子的简表：数量、尺寸范围、链条范围、平均链条数、block 总量。 |
| `normal_campaign500_manifest.csv` | 普通 500 关完整选择清单。字段包括 bucket、type、score、coverage、outer1、openers、family、qualityFlags。 |
| `shape_manifest.csv` | Shape pack 反查后的关卡清单。字段包括 theme、maskName、mask 尺寸/面积、maskFill、关卡尺寸、链条数、源 asset 路径。 |
| `hole_mask_early_front_manifest.csv` | Hole pack 反查后的关卡清单。字段包括画布尺寸、链条数、箭头格数、block 格数、可玩区域填充率、源 asset 路径。 |
| `source_shape_candidate_pool_inventory.md` | Shape 候选池原始盘点文档的冻结副本。 |

## 当前理解

### 普通 500

- 难度桶已经补满：refresh 120、normal 220、hard 110、very_hard 40、extreme 10。
- 类型分布：section 162、dense 111、maze 88、lock 85、sweep 46、shell 8。
- 链条范围：8-366，平均 108.6。
- 平均 coverage：0.976。
- 当前质量标签：454 个 `ok`；剩余主要是 `weak-outer`、`low-coverage`、`edge-short-heavy`、`too-open`。作为候选池可以用，但正式定序前建议再做一轮尾部剔除或人工确认。

### Shape

- Shape 正式池 147 关；早期小道具池 4 关。
- 主题覆盖：Magic 29、Ocean 23、Landmark 22、Object 14、Nature 14、Symbol 10、Vehicle 8、Space 8、Music 7、Character 7、ToolUI 6、Art 3。
- 链条范围：30-207，平均 112.4。
- Shape 正式池平均 maskFill：0.980；早期小道具平均 maskFill：0.987。
- 关键规则：Shape 是“可见轮廓内填箭头”的特殊图形关卡；hole/blocker mask 明确不属于 Shape。

### Hole / Blocker

- EarlyFront pack 一共 70 关。
- 画布范围：16x18 到 34x30。
- 链条范围：22-97，平均 66.0。
- 平均 playableFill：0.747。
- 读取到的每关 blocker 数都是 72 格。
- 这是一条独立机制线，不应该和 Shape 混成同一类特殊图形。

## 导入建议

- `normal_campaign500` 作为第一版 500 关主骨架。
- `shape_early_prop` 只放在非常前期，用来制造“看得懂又爽”的小惊喜。
- `shape_final_with_supplement` 作为周期性视觉特殊关卡插入，注意相近主题不要挨太近。
- `hole_mask_early_front` 建议在玩家理解普通消除后再引入，因为它的认知模型是 blocker 机制，不是单纯外形轮廓。
- 正式生成导入包前，需要决定普通 500 中带质量标签的尾部关卡是立即剔除，还是先保留为备选。
