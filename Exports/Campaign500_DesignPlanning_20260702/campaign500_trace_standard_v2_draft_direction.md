# Campaign500 Trace Standard V2 Draft Direction

生成日期：2026-07-02

## 结论

暂时不要全项目替换 official trace。应该新增一个 `Trace Standard V2` 叠加层：

```text
Official Trace V1 = solved + safety + comparable baseline
Trace Standard V2 = slot-aware difficulty evidence
Rhythm Audit V1 = campaign placement / pacing
Unity Review = human feel
```

## 为什么不能只靠现有 TraceOrderKeep

`TraceOrderKeep` 说明候选比较健康：

- solved。
- coverage 达标。
- choices 不爆。
- 没有明显方向/局部/塌缩风险。
- STS 指标达标。

但它没有回答：

> 这关作为当前 slot 是否真的承担了它的职责？

一个 `TraceOrderKeep` 可能适合普通恢复关，也可能适合困难关，但它需要不同证据。

## V2 应该新增的输出字段

### 1. slotDifficultyFitClass

建议枚举：

```text
RecoveryFit
NormalFit
HardFit
SpecialHardFit
ExtremeFit
MisfitTooEasy
MisfitTooNoisy
MisfitTooHeavy
```

### 2. difficultyEvidenceTags

用于解释为什么它适合这个 slot：

```text
smooth_recovery
stable_practice
language_variation
region_switch
axis_switch
local_run_break
low_choice_choke
dependency_depth
cross_region_dependency
frontier_rebuild
longchain_backbone
hub_convergence
shape_spatial_constraint
hole_spatial_constraint
```

### 3. difficultyRiskTags

用于解释为什么它可能不适合：

```text
too_local_easy
too_linear
too_many_choices
choice_noise
same_region_sweep
same_axis_sweep
collapse_to_single_region
visual_overload
raw_count_only
no_peak_contrast
no_post_peak_release
style_repetition
```

### 4. effectiveLoadScore

不要只看 raw chain：

```text
effectiveLoadScore =
  countLoad
+ canvasLoad
+ choiceLoad
+ readDemandLoad
+ dependencyLoad
+ spatialConstraintLoad
+ styleComplexityLoad
- recoveryDiscount
```

### 5. readDemandScore

专门判断“是否需要读题”：

候选信号：

- midgame choice window 是否进入 1-4。
- 低选择窗口前是否发生局部清空/局部 frontier drain。
- 低选择窗口后是否跳到另一区域。
- 当前可选目标是否来自不同区域/方向。
- counterfactual 错选是否导致明显延迟或死路。

### 6. localBreakScore

专门判断“是否打断同区连续消除”：

- localPatchRun 是否被控制。
- 是否存在 `local -> remote` 转移。
- 转移发生时 choices 是否可读。
- 转移后是否释放新 frontier，而不是继续同一区域扫。

### 7. dependencyEvidenceScore

专门判断依赖：

- dependency graph depth。
- cross-region dependency edge。
- direction-change dependency edge。
- dependency delay。
- hub/convergence branch。
- conveyor edge rate 低。

### 8. peakContrastScore

和 campaign slot 相邻关系有关：

- 本关 effectiveLoad 是否高于同 10 关普通均值。
- 本关 readDemand 是否高于同 10 关普通。
- 本关是否有独特主题。
- 本关后是否存在 release。

### 9. chokeArrowScore

专门判断是否存在“卡点箭头 / key choke arrow”。

这个指标独立于平均选择数。它回答的问题是：

> 玩家是否需要识别某个关键箭头，清掉它以后局面才明显推进？

候选信号：

- 清掉该箭头后，frontier 明显变化。
- 新可消链数量或新可消区域发生明显变化。
- 从局部区域切换到远区。
- 释放了一个被阻挡的依赖目标。
- 它是 hub / longchain backbone / peel layer / shape-hole 空间约束里的关键开关。
- counterfactual 中，其他可选箭头的推进价值明显低于它。

建议字段：

```text
chokeArrowCount
chokeArrowStrongCount
chokeArrowMaxStrength
chokeArrowFirstStepRatio
chokeArrowMidgameCount
chokeArrowEvidenceTags
chokeArrowRiskTags
chokeArrowFairnessScore
```

建议 strength 公式：

```text
chokeArrowStrength =
  choiceImpactGap
+ frontierChangeImpact
+ remoteRegionUnlockImpact
+ dependencyUnlockImpact
+ nextWindowReadability
- hiddennessPenalty
- noisePenalty
```

其中：

- `choiceImpactGap`：当前最佳推进箭头与其他候选的推进差距。
- `frontierChangeImpact`：清除后可消 frontier 是否重构。
- `remoteRegionUnlockImpact`：是否把解题焦点带到远区。
- `dependencyUnlockImpact`：是否释放依赖链/关键主干。
- `nextWindowReadability`：后续选择数是否进入可读范围，例如 1-4。
- `hiddennessPenalty`：卡点是否太隐蔽、没有视觉/结构提示。
- `noisePenalty`：是否只是很多乱选里的随机正确答案。

## 卡点箭头的好坏区分

好的卡点：

```text
readable_choke
structural_choke
region_switch_choke
dependency_choke
hub_choke
longchain_choke
shape_spatial_choke
hole_spatial_choke
```

特点：

- 玩家能从局面结构读出它重要。
- 它清除后有明确反馈。
- 它不依赖随机试错。
- 它通常发生在中段或峰值段，而不是新手关突然出现。

坏的卡点：

```text
hidden_choke
random_choke
choice_noise_choke
unfair_singleton
visual_overload_choke
```

特点：

- 可选项很多，但没有结构线索。
- 正确箭头只是随机藏在一堆相似箭头里。
- 清掉后反馈不明显。
- 它让玩家卡住，但不是因为理解难，而是因为信息不公平。

## V2 门槛建议

### Universal Gate

所有正式候选：

```text
solved = true
coverage / fill 达标
maxChoices 不爆
visual risk 不爆
local/near outer run 不越硬上限
```

### RecoveryFlow

```text
effectiveLoad: low or medium-low
readDemandScore: low
localBreakScore: optional
choiceNoise: low
risk: no visual overload
```

### NormalPractice

```text
effectiveLoad: stage baseline
readDemandScore: low-medium
style/language differs from neighbors
local run not too high
```

### ReadCheck / Hard

必须满足至少 2 个：

```text
region_switch evidence
axis_switch evidence
local_run_break evidence
low_choice_choke evidence
controlled dependency evidence
key_choke_arrow evidence
```

同时不能：

```text
too_many_choices
raw_count_only
too_local_easy
```

### DependencyPeak / SpecialHard

必须满足：

```text
effectiveLoad above local normal
dependencyEvidenceScore medium-high
readDemandScore medium-high
peakContrastScore positive
chokeArrowScore medium or above
```

至少一个主题：

```text
longchain_backbone
hub_convergence
peel_layer_unlock
shape_spatial_constraint
hole_spatial_constraint
cross_region_dependency
```

### ExtremeMemory

必须满足：

```text
peakContrastScore high
effectiveLoad top in nearby 20/50 levels
readDemand or dependency evidence high
at least one strong readable choke arrow
postPeakRelease exists
theme is explicit
```

## 实施方式

短期不改 `Build-SGPRhythmTrace.ps1` 主逻辑。

先新增一个 join/audit 脚本：

```text
Build-Campaign500TraceStandardV2.ps1
```

输入：

```text
layout csv
slot duty csv
official trace metrics csv
source report csv
```

输出：

```text
per-level trace standard v2 csv
slot fit summary csv
reject/misfit report
section difficulty truth report
```

这样风险低，且可和旧 trace 并行对比。

## 最小可落地版本

第一版不需要一步到位做完整 AI 玩家模型。可以先加一个轻量 choke proxy：

```text
For each official trace step:
  1. 记录当前 available choices。
  2. 对前 N 个候选做 one-step counterfactual。
  3. 计算每个候选的 impact：
     - next choices 是否变少到可读窗口。
     - 新 unlock 数量。
     - 新 unlock 区域数量。
     - 是否跨 region / axis。
     - 是否触发 dependency / frontier rebuild。
  4. 如果 chosen 或最佳候选 impact 明显高于其他候选，标记 choke arrow。
```

轻量版 CSV 字段：

```text
chokeArrowCount
chokeArrowMaxStrength
chokeArrowStepPattern
chokeArrowEvidenceTags
chokeArrowRiskTags
chokeArrowFitForSlot
```

这个版本就足以避免只看 `avgChoices` 的误判。
