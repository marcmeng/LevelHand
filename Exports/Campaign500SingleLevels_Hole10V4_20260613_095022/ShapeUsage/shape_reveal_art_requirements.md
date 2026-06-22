# Shape 通关展示美术需求

## 目标

Shape 关卡通关后，在进入下一关前短暂展示该关对应的 Shape 形象，让玩家意识到“刚刚消掉的是一个具体图形”，增强特殊关卡的记忆点和奖励感。

本批次当前包含：

- Shape 关卡：100 关
- 唯一 Shape 形象：100 个
- 关卡到形象映射：见 `campaign500_shape_usage.csv`
- 美术交付清单：见 `shape_reveal_art_asset_list.csv`

## 触发规则

只在 `type` 以 `shape` 开头的关卡触发展示。

不触发的类型：

- 普通关
- hole 救援关
- bonus 关
- 教学关

推荐以 `levelId -> displayShapeId` 做映射，不建议只依赖关卡序号，因为后续排布可能会调整。

## 展示流程

1. 玩家完成最后一条链条消除。
2. 棋盘箭头淡出或轻微下沉。
3. 显示 Shape reveal 图。
4. 图形做轻微缩放出现，停留约 1.0-1.5 秒。
5. 玩家点击或自动进入结算/下一关。

推荐表现：

- 背景使用当前游戏深色背景或轻微暗化遮罩。
- Shape 图形居中展示。
- 可以加少量粒子、闪光或光圈，但不要盖住图形轮廓。
- 不要在美术图里内嵌文字，文字由 UI 层控制。

## 素材规格

主展示图：

- 格式：透明 PNG
- 建议尺寸：1024x1024
- 画面安全边距：四周至少 10%-12%
- 图形位置：居中
- 图形占比：长边约占画布 72%-82%
- 背景：透明
- 不带文字
- 不带按钮
- 不带复杂投影，投影和光效由 UI 统一处理

缩略图：

- 格式：透明 PNG
- 建议尺寸：256x256
- 用途：关卡预览、收集册、活动入口
- 命名和主展示图保持同一 `displayShapeId`

## 命名规则

主展示图：

```text
Art/ShapeReveal/{displayShapeId}.png
```

缩略图：

```text
Art/ShapeRevealThumb/{displayShapeId}_thumb.png
```

本地化名字 Key：

```text
shape.name.{displayShapeId}
```

示例：

```text
displayShapeId = shape_main_magic_quill_tall
主展示图 = Art/ShapeReveal/shape_main_magic_quill_tall.png
缩略图 = Art/ShapeRevealThumb/shape_main_magic_quill_tall_thumb.png
名字Key = shape.name.shape_main_magic_quill_tall
```

## 优先级

P0：

- 前 120 关内出现的 Shape
- 新手专用四个：水晶球、橡皮擦、标尺、魔法杖
- 早期魔法系列：羽毛笔、沙漏、盾牌、钥匙、蝙蝠、黑猫、火箭等

P1：

- 121-300 关出现的 Shape
- 中期主题切换用的 Ocean、Landmark、Object、Nature

P2：

- 301-500 关出现的 Shape
- 后期补充变化用的高阶形象

具体优先级已写入 `shape_reveal_art_asset_list.csv`。

## 风格要求

整体风格：

- 清晰、饱满、偏童话/休闲游戏风
- 轮廓第一，细节第二
- 需要玩家一眼能看出大概是什么
- 不要过度写实
- 不要过度复杂纹理

魔法类：

- 可偏发光、水晶、星光、卷轴、法杖、魔法书等视觉语言
- 保持轮廓明确，不要只有抽象光效

海洋类：

- 鲸鱼、水滴、锚、漂流瓶、水母等要保证剪影可读
- 避免太多细碎触须或小气泡影响识别

地标类：

- 灯塔、塔、城堡门、纪念碑等需要竖向稳定
- 顶部轮廓和主体比例要明确

符号类：

- 星星、心形、闪电、音符等可以更图标化
- 边缘要干净，不要做成太薄的线框

动物/角色类：

- 优先表现整体姿态和头部特征
- 不需要复杂表情，但要能读出类别

## 程序接入建议

新增一份运行时映射数据：

```text
levelId, displayShapeId, revealSpritePath, thumbSpritePath, nameKey
```

或者在关卡配置中增加字段：

```text
shapeDisplayId
shapeRevealSprite
shapeNameKey
```

通关后逻辑：

```text
if level.shapeDisplayId not empty:
    show ShapeRevealPanel
else:
    normal complete flow
```

不要硬编码关卡序号；使用 `levelId` 或关卡配置字段更稳。

## 验收标准

- 100 个 `displayShapeId` 都有对应主展示图。
- 每个 Shape 关卡通关后展示正确形象。
- 普通关和 hole 关不会弹出 Shape reveal。
- 9:16 手机画面中，图形不被按钮、标题、刘海区遮挡。
- 图形在 360px 宽度手机上仍能读出大轮廓。
- 前 20 关的新手 Shape 优先完成，并且辨识度高。
- 素材不包含文字，不影响多语言。

