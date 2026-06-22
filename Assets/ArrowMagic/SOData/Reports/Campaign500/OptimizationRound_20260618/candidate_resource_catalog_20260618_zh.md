# 500关资源汇总 - 20260618

## 当前正式包

- 正式 500 关包：`F:/Unityproject/ArrowLevel-Hand/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PlanPreviewPack.asset`
- 总关卡数：500
- 关卡类型：普通 450，hole 50
- 难度分布：1=302，2=102，3=58，4=38
- 主要结构类型：section 119，lock 92，dense 52，hole_rescue 50，sweep 46，maze 32

## 普通候选池

- 完整来源：`F:/Unityproject/ArrowLevel-Hand/.worktrees/architectural-linework/Exports/SGPManualCuratedFinal40_Levels.zip`
- 可用候选：40 关，zip 内包含完整 `.asset` 和 `.meta`
- 与当前 500 包重复：0
- 链条组范围：49 / 90.8 / 164（最小 / 平均 / 最大）
- 画布宽度范围：18 / 21.9 / 34
- 画布高度范围：25 / 35.0 / 48
- 来源组成：QUALITY_FILTERED26_HARDER 26 关，MANUAL8_REPAIRED 8 关，NEWSEED100431_FINAL20 6 关
- 注意：原 worktree 里的 Unity pack 直接解析只有 6 关可用、38 关断引用；后续应以 zip 导出的 40 关作为完整普通候选来源。

## Shape 候选池

- 包：`F:/Unityproject/ArrowLevel-Hand/.worktrees/shape-procedural-mask-fill/Assets/ArrowMagic/SOData/Packs/ShapeExperiment/MaskOnlyCollected/ShapeIconMaskOnlyAcceptedPoolPack.asset`
- 可用候选：347 关
- 图形 family：116 种
- 链条组范围：33 / 96.0 / 166
- 画布宽度范围：24 / 36.6 / 44
- 画布高度范围：24 / 38.6 / 49
- 命名结构比较清楚，包含 mushroom、umbrella、compass、musicnote、snowflake、heart、rocket、treasurechest 等；通常有 compact / main / large 三档。

## 生成的资源表

- 普通候选 40 关明细：`F:/Unityproject/ArrowLevel-Hand/Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/normal_sgp40_zip_complete_inventory.csv`
- Shape 候选 347 关明细：`F:/Unityproject/ArrowLevel-Hand/Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/shape_maskonly347_inventory_v2.csv`
- Shape family 汇总：`F:/Unityproject/ArrowLevel-Hand/Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/shape_maskonly347_shape_family_summary.csv`
- 普通 + Shape 合并候选表：`F:/Unityproject/ArrowLevel-Hand/Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/candidate_resource_combined_normal_shape.csv`
- 英文机器摘要：`F:/Unityproject/ArrowLevel-Hand/Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/candidate_resource_catalog_20260618.md`
