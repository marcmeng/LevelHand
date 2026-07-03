# Campaign500 Slot Planning Method V2

生成日期：2026-07-02

## 核心顺序

后续编排按两层走：

1. 先确定每 10 关的“小剧本”。
2. 再把这个小剧本拆到每一关的具体职责。

这样做的目的，是避免直接按候选池或链条数排布。每个候选进入正式 500 关之前，必须回答：它服务哪一个 10 关段？承担哪一关的职责？玩家在这一关应该感受到什么变化？

## 第一层：10 关小剧本

每 10 关先定义：

- `sectionDuty`：这一组是进入、练习、变化、加压，还是小峰值。
- `sectionIntent`：玩家这一组要体验什么。
- `sectionTheme`：这一组主风格和主学习点。
- `flowShareGuidance`：爽关/恢复关占比。
- `preferredStyleMix`：可用风格范围。
- `effectiveLoadGuidance`：普通、困难、峰值的有效负载区间。
- `releaseRule`：峰值之后怎么释放。

对应文件：

- `campaign500_10level_section_plan_v1.csv`

## 第二层：逐关职责

每一关再落到两个 beat：

- `positionBeatRole`：这个位置在 10 关循环里的默认节奏。
- `actualBeatRole`：结合模板 `difficultyCode`、`category`、shape/hole 后的真实职责。

后续生产以 `actualBeatRole + slotDutyV2` 为准，不直接按 `positionBeatRole`。

例如第 8 位通常是后段压力窗口，但如果模板里它是普通关，它的实际职责可以是 `PracticeBeat / NormalPractice`，而不是硬做困难。

对应文件：

- `campaign500_per_level_duty_v2.csv`
- `campaign500_first100_per_level_duty_preview_v2.csv`

## Slot Duty V2

当前逐关职责包括：

- `RecoveryFlow`：释放、爽感、低挫败。
- `NormalPractice`：练习本段主语言。
- `LanguageVariation`：换链条语言或画布手感。
- `BridgeRamp`：普通上沿，为困难铺垫。
- `ReadCheck`：要求玩家停一下读区域/方向。
- `LocalRunBreaker`：专门打断同一区域连续消除。
- `CanvasStep`：画布/密度抬压。
- `DependencyPeak`：本组依赖读题峰值。
- `StylePeak`：以风格为主题的峰值。
- `ExtremeMemory`：章节级记忆点。
- `ShapeAnchor` / `ShapeReadAnchor` / `ShapePeakAnchor`：shape 相关职责。
- `HoleSpatialAnchor` / `HoleReadAnchor` / `HolePeakAnchor`：hole 相关职责。

## 读取方式

排布时先看：

```text
section10 -> sectionDuty -> sectionIntent -> sectionTheme
```

再看具体行：

```text
order -> actualBeatRole -> slotDutyV2 -> playerTask -> acceptanceFocus
```

最后才看候选：

```text
styleTargetV2 -> chainLanguageTargetV2 -> pressureIntent -> candidate metrics
```

## 生产原则

同一个候选不是“好”就能放，它必须匹配 slot。

普通关要判断：

- 是恢复？
- 是练习？
- 是换语言？
- 是困难前的桥？

困难关要判断：

- 难在读题？
- 难在打断局部连消？
- 难在画布空间？
- 难在依赖顺序？

峰值关要判断：

- 是 shape/hole 视觉记忆点？
- 是 LongChain/Hub/Peel 的结构峰？
- 是章节级极难？

## 下一步

下一步应该先人工确认前 100 关：

1. 每 10 关的小剧本是否合理。
2. 每关 `actualBeatRole` 是否符合模板和玩家体验。
3. `slotDutyV2` 是否需要手动调整。
4. 再开始按这些职责重新挑候选和补生产。

