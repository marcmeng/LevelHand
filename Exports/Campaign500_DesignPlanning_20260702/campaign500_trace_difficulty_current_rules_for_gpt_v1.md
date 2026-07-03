# Campaign500 Trace / Difficulty Rules Current State For GPT V1

生成日期：2026-07-02

## 背景

我们在做一个 500 关竖屏箭头链清除游戏的正式编排。现在已经有：

- 静态排版审查工具：检查 500 关节奏是否合理。
- 官方 trace 工具：模拟解题过程，输出选择数、局部连消、方向/区域/依赖等指标。
- 生成候选池：Flow / Peel / LongChain / Hub / Shape / Hole 等风格。

当前问题：

> 现有 trace 比较适合判断“能不能清、有没有明显风险”，但不够适合判断“困难 slot 是否真的有困难职责”。

我们希望 GPT 帮忙优化的是：如何把 trace 标准升级成一个 **slot-aware difficulty validation**，也就是不同职责的关卡用不同证据证明它合格。

## 游戏基础规则简述

- 棋盘是 `width x height` 网格。
- 格子类型：`Empty / Arrow / Block`。
- 箭头构成若干不重叠链条。
- 点击某个可逃逸箭头后，会清除它所在的整条相邻链。
- 胜利条件：清空所有 Arrow。
- 正式关卡必须能被 GreedyValidator / official trace 在预算内清完。

玩家体验不是“点哪里清哪里”，而是看当前哪些链可以逃逸、清掉以后哪些链被释放。

## 当前排版层规则

500 关不再直接按链条数排序，而是分成：

- 10 关微循环。
- 50 关章节弧。
- 500 关长期节奏。

每关有一个 `slotDuty`：

- `RecoveryFlow`：峰值后的释放。
- `NormalPractice`：普通练习。
- `LanguageVariation`：换链条语言/画布手感。
- `BridgeRamp`：普通上沿，为困难铺垫。
- `ReadCheck`：困难读题。
- `LocalRunBreaker`：打断同一区域连续消除。
- `CanvasStep`：画布/密度抬压。
- `DependencyPeak`：依赖关系峰值。
- `StylePeak`：风格主题峰值。
- `ExtremeMemory`：章节级极难/记忆点。
- `ShapeAnchor` / `HoleSpatialAnchor`：特殊视觉/空间 anchor。

目标不是所有关都越来越大，而是：

```text
爽感 -> 新鲜感 -> 轻读题 -> 小峰值 -> 释放
```

## 当前 Rhythm Audit 工具

脚本：

```text
Tools/Production/Audit-Campaign500RhythmV1.ps1
Tools/Production/Campaign500RhythmAuditV1.py
```

它是静态排版审查，不跑 Unity 解法。

检查内容：

- 500 行完整性、order/section 是否连续。
- 前 20 关教学压力是否过高。
- 每 10 关普通/困难/峰值的链条平均是否倒挂。
- 早期是否有过大跳变。
- 峰值之后是否有释放。
- 非峰值关是否箭头/链条过重。
- 风格/来源是否重复。

上一版最终排布 `C5V5F100` 审查：

- `68/100`，等级 `C`。
- 无结构错误。
- 主要问题：部分段普通比困难/峰值更重；前期跳变；极难后释放不足；后段非峰值箭头量偏重。

V3 粗排审查：

- `72/100`，等级 `B`。
- 说明节奏结构略好，但仍只是职责草案，不是正式候选排布。

## 当前 Official Trace / Join 规则

主要入口：

```text
.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1
Tools/Production/Join-SGPPressureTraceMetrics.ps1
```

`Build-SGPRhythmTrace.ps1` 做的事情大致是：

- 解析 Unity LevelDefinition asset。
- 构建棋盘和链条。
- 模拟清除过程。
- 记录每一步选择数、区域、方向、局部/外圈、依赖释放、frontier 等。
- 输出 metrics CSV。

`Join-SGPPressureTraceMetrics.ps1` 把 source report 和 trace metrics 合并，并给出 keep/rank。

### 当前默认 hard gate

Process gate：

```text
solved == true
coverage >= 0.97
processTier in S/A/B
maxChoices <= 10
```

Visual / local risk gate：

```text
directionalSweepRisk <= 0.32
stripeVisualRisk <= 0.18
localPatchSolveRunMax <= 8
nearOuterPatchSolveRunMax <= 5
```

STS / solve trace gate：

```text
solveTraceQualityScore >= 0.72
solveTraceCollapseRiskScore <= 0.35
solveSameAxisRunMax <= 8
solveSameDirHeadRunMax <= 7
dependencyLocalSameRegionRate <= 0.65
```

Nutation 某些 lane 更严格：

```text
directionalSweepRisk <= 0.28
localPatchSolveRunMax <= 7
nearOuterPatchSolveRunMax <= 5
solveSameAxisRunMax <= 7
solveSameDirHeadRunMax <= 7
dependencyLocalSameRegionRate <= 0.60
```

### 当前 rankClass

```text
TraceOrderKeep = processKeep + stsPass + not hard visual reject
VisualKeep     = visualPass
ProcessKeep    = processKeep
Reject         = otherwise
```

### 当前综合 rankScore

目前综合分由以下部分组成：

- keepBonus。
- coverageScore。
- choiceScore。
- visualScore。
- stsRankScore。

STS rank 内部会看：

- solveTraceQualityScore。
- 1 - collapse risk。
- same axis run clean。
- same dir run clean。
- solveRegionEntropy。
- solveFrontWidthAvg。
- dependencyLocalSameRegionRate clean。

## 我们对当前 trace 的不信任点

### 1. 它更像“安全/质量 gate”，不是“难度职责证明”

`TraceOrderKeep` 代表这关 trace 顺序更健康，但不代表它适合困难、特别困难、极难 slot。

一关可以很安全、很分散、很顺，但并不难。

### 2. 同一套门槛用于不同 slot

现在普通、困难、特别困难、极难基本都在共享同一组 trace 门槛。

但不同 slot 需要证明的东西不同：

- RecoveryFlow 要证明顺畅。
- ReadCheck 要证明玩家需要读一下。
- LocalRunBreaker 要证明局部连消被打断。
- DependencyPeak 要证明有依赖深度/跨区释放。
- ExtremeMemory 要证明是阶段记忆点。

### 3. choice 指标容易误判

`maxChoices <= 10` 是安全门槛，但不是难度判断。

- 选择多可能只是混乱，不一定难。
- 选择少可能是线性爽关，也可能是真正 choke。
- 真正有价值的是“什么时候选择少、为什么选择少、它是否发生在局部消解后/跨区切换时”。

更准确地说，我们目前缺一个核心维度：

> 是否存在“卡点箭头 / key choke arrow”。

玩家卡住往往不是因为平均选择数高或低，而是因为：

- 当前有很多可点箭头，但只有 1 个是真正推进局面的关键箭头。
- 当前选择数不高，但关键箭头不显眼，玩家需要读出它为什么重要。
- 清掉某个箭头后，局面从一个区域切到另一个区域，或释放出新的依赖/frontier。
- 某个箭头是小门、开关、hub、长链主干、shape/hole 空间约束的关键点。

所以不能只看 `avgChoices` / `maxChoices`。应该单独标记：

```text
chokeArrowCount
chokeArrowStrength
chokeArrowStepRatio
chokeArrowEvidenceTags
chokeArrowFairness
```

这类指标应该给困难、特别困难、极难 slot 一定加权；普通/恢复关则只允许轻量或不要求。

### 4. local run 指标只能证明“不太局部”，不能证明“有读题”

`localPatchRun` 低是好事，但低不代表难。

它需要和下面这些一起看：

- 区域切换是否发生在关键窗口。
- 当前 frontier 是否被清空后转移到远区。
- 选择数是否进入 1-3 的读题窗口。
- 清除后是否释放新的非局部目标。

### 5. solveTraceQuality 可能奖励“均衡”，但难度不一定来自均衡

有些好玩的困难关可能是：

- 一个长依赖主干。
- 一个 hub 汇聚。
- 一个局部 choke。
- 一个 shape/hole 空间约束。

它们未必在 entropy/axis drift 上全面漂亮。

同样，卡点箭头也未必会让全局 trace 指标漂亮。一个合理的困难关可能只有 1-2 个关键 choke window，其余时间都很顺。如果只看平均值，这类关会被误判成普通。

### 6. chain/arrow/canvas 量级只应作为 effective load，不应单独代表难度

LongChain raw chain count 可能低，但体感难。

大画布高箭头可能只是疲劳，不一定难。

所以需要：

```text
effectiveLoad =
  raw chain/arrow/canvas
+ readDemand
+ dependencyDepth
+ regionSwitch
+ controlledChoiceChoke
+ style/space constraint
- recoveryFlowDiscount
```

## 希望升级成的新标准

建议把全项目 trace 标准分成四层：

### Layer 0：合法性 / solved gate

所有正式关卡必须过：

- authored legality。
- no overlap / no out-of-board。
- 至少有 arrow。
- 至少有初始可消链。
- Greedy / official trace solved。

### Layer 1：安全 / comfort gate

所有关卡必须不过度坏：

- maxChoices 不爆。
- directional sweep 不过高。
- stripe visual risk 不过高。
- local/near-outer run 不过长。
- collapse 不过强。
- 非峰值箭头/链条不过重。

### Layer 2：slot fit gate

按 slotDuty 判断是否合格。

#### RecoveryFlow

要证明：

- 低挫败。
- 可连续推进。
- 选择数适中或偏低。
- 没有高压 choke。
- 不承担依赖峰值。

#### NormalPractice

要证明：

- 稳定练习。
- 与前后关有风格/语言差异。
- 不过度局部扫穿。
- 不比同组困难还重。

#### ReadCheck

要证明至少有一个真实读题信号：

- 区域切换窗口。
- 方向切换窗口。
- 选择收窄但不是线性无脑。
- 清掉一个局部后被迫看另一区。
- 至少一个轻量 key choke arrow，且能解释为什么它推进局面。

#### LocalRunBreaker

要证明：

- 早/中段出现局部连续消除中断。
- 中断后选择数进入可读范围，例如 1-3 或 2-4。
- 下一个有效区域与前一个局部不同。
- 中断点最好能标记出具体 choke arrow，而不是只统计 local run 下降。

#### DependencyPeak

要证明：

- 依赖图有深度。
- 至少一个跨区/跨方向依赖边。
- 释放不是纯 local conveyor。
- 关键节点清除后改变后续 frontier。
- 至少一个中强度 choke arrow，最好对应依赖释放、hub 汇聚或长链主干。

#### ExtremeMemory

要证明：

- 是当前 20/50 关内的有效峰值。
- 不只是 chain/arrow 多。
- 有明确主题：longchain / hub / peel / shape / hole / dependency。
- 后面有释放关。

### Layer 3：campaign placement gate

候选过 trace 不代表能放在任意位置。

还要检查：

- 同一 10 关内普通/困难/峰值是否拉开。
- 峰值后是否释放。
- 同风格/同链条语言是否重复。
- 前 20 是否教学合理。
- 前 100 是否曲线平滑。

## 希望 GPT 帮忙的问题

请帮我们设计一个更合理的 `Trace Standard V2`：

1. 如何把 trace 指标从“全局 keep gate”改成“slot-aware difficulty evidence”？
2. 普通 / 困难 / 特别困难 / 极难分别应该看哪些指标？
3. 如何判断“困难 slot 真的难”，而不是只是大、密、或者 trace 分散？
4. 如何把 `choice` 指标改成更接近玩家体感的指标？
5. 如何处理 Flow / Peel / LongChain / Hub / Shape / Hole 这种不同风格的难度差异？
6. 如何避免过度依赖官方 greedy path，因为 greedy path 不一定等于玩家路径？
7. 哪些指标应该是硬门槛，哪些应该是 soft score，哪些只做 warning？
8. 能否给出一套工程可落地的 CSV 字段和门槛表？
9. 如何定义并检测 “key choke arrow / 卡点箭头”，让它独立于平均选择数参与难度判断？
10. 如何区分“好的可读卡点”和“坏的隐藏卡点/随机卡点”？

## 我们倾向的方向

不要直接废掉现有 trace。

建议：

```text
Old Trace = solved / safety / comparable baseline
Trace Standard V2 = slot-aware difficulty evidence layer
Campaign Rhythm Audit = placement / pacing layer
Unity Playtest = final human feel validation
```

也就是说，新标准应该是叠加层，而不是替换所有旧工具。
