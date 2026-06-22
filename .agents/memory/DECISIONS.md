# Decisions

## 2026-06-18 - Repository-Carried Project Memory

- 状态：已采用
- 决策：项目通过 `AGENTS.md`、`.agents/memory/`、`.agents/index/` 和 `.agents/skills/project-memory/` 携带上下文、工作流、资源索引和恢复协议。
- 原因：新对话不应依赖聊天记忆；仓库自己提供足够的恢复入口。
- 影响：后续重要背景、路径、决策、索引和接续点要写回项目文档。

## 2026-06-18 - Indexes Are Navigation, Not Encyclopedias

- 状态：已采用
- 决策：索引文件只记录路径、用途和什么时候该看，不复制大段代码、日志或资源内容。
- 原因：索引用于快速定位，避免膨胀成难维护的二手文档。

## 2026-06-18 - Experiment Convergence Required

- 状态：已采用
- 决策：实验可以在工作过程中保留；最终验证成功后，冗余实验产物必须回退、删除或移出工作目录，只把正式方案纳入最终改动。
- 原因：关卡、掩码、报告和一次性脚本容易堆积，必须把经验沉淀和正式内容分开。
- 约束：不确定是不是用户改动时，不擅自回退，先确认。

## 2026-06-18 - Secret and Noise Exclusion

- 状态：已采用
- 决策：项目记忆不得记录密码、token、私钥、证书内容、隐私、大段日志、未验证猜测和一次性噪声。
- 约束：未验证信息可以记录，但必须标记“状态：待验证”。

## 2026-06-19 - SGP Rhythm Requires Process Trace

- 状态：实验中
- 决策：SGP 候选不能只按开局可消数量和静态依赖筛选，必须增加整局过程曲线 trace。
- 原因：已观察到部分关卡开局数量不高，但中盘平均选择数、P80 和峰值过高，玩家仍会感到无脑连续消除。
- 当前门槛：普通/可读候选优先保留 `processTier=S/A`；核心指标包括 `openers`、`avgChoices`、`choiceP80`、`maxChoices`、`over10Rate`、`meaningfulUnlockRate`、`localConveyorRunMax`。
- 约束：`maxChoices >= 12` 只适合短暂 flow/爽关峰值，不应作为普通关常态。

## 2026-06-19 - PressureRead Needs Far Dependency, Not Only Low Choices

- 状态：实验中
- 决策：PressureRead 类难关必须同时约束低选择曲线和依赖距离，不能只压 `openers` / `avgChoices`。

## 2026-06-22 - Trace-Delta Filler Compiler Over FirstHit Preservation

- 状态：已采用为当前 SGP hard-lock 补肉主线
- 决策：hard-lock 低覆盖样本补链条时，不把 `firstHit` 是否变化作为硬拒绝；补链条必须通过 board-level trace 前后差值预算来验收。允许插入 relay/locked filler，只要最终 trace 证明可解、难度压力未明显变松。
- 原因：`firstHit` 改变不等于无解，插入的中继链可以先被消除并仍保持核心依赖；反过来，`firstHit` 不变也不能证明没有新增独立解路径。
- 执行模型：`hard-lock base -> slot-based candidate generation -> board-level trace -> delta gate`。slot 只作为候选空间收缩，分为 `bridge/noise/edge`；最终接受条件仍是 `maxChoices/avgChoices/causalAntiLocality/supportClosure/localPatch/dependencyFollow/outerExit` 的结果和 delta。
- 当前证据：`SGPRhythmLab_HardLockTraceDeltaFillV1SlotRun1Pack` 冻结复验 5/5 `TrueHardCandidate`，平均 `maxChoices=5.8`、`causalAntiLocalityScore=0.678`、`choicePeakCount=0`，说明 slot+trace-delta 能在提升覆盖时保持难度结构。
- 后续边界：当前覆盖仍只有约 `0.223-0.241`；继续提升覆盖时必须优化 candidate runner 性能和 delta budget，不能回到全局随机补肉或只靠静态规则筛选。
- 原因：附近连续 A->B->C 即使选择数低，也会变成顺手扫；真正的难度来自跨区域、换方向、远一点的解锁关系。
- 当前做法：在 `sgp-rhythm-lab` 的 PressureRead orientation 生成中优先选择远距离、跨区、换方向的 blocker，并记录 `nearDepRate`、`depMeaningfulSignal`、`avgSolveJumpDistance`、`nearSolveRunMax`。
- 约束顺序：先保证可解，再控选择曲线，再控远依赖/附近连续消除，最后再看外围覆盖和视觉质量。

## 2026-06-19 - Straight Chains Should Be Structured, Not Banned

- 状态：实验中
- 决策：SGP/PressureRead 里不要求把所有直链变成弯链；直链可以存在，但必须承担边界、走廊、门锁或跨区依赖功能。
- 原因：纯粹禁止直链会让链条语言失真；真正减分的是同一外边多根长直链连续消除、平行扫边、或几根直线没有空间结构地撑关卡。
- 当前做法：`sgp-rhythm-lab` 的 orientation 生成和真实 trace 都记录 `outerStraightRunMax`、`sameSideOuterStraightRunMax`、`outerStraightSolveRatio`，并在依赖评分中惩罚同边外圈长直链互挡、平行直链互挡，优先跨区/换方向/结构承载链。
- 约束：直链结构指标应和 choice curve、远依赖一起看；不要为了弯折率牺牲可读性和覆盖率。

## 2026-06-19 - Difficulty Breakthrough Requires Stage Locks, Not Only Choice Pressure

- 状态：实验中
- 决策：更难且有趣的 SGP 关卡要从“压平均可消数”升级到“阶段门锁”。核心指标包括 `stageLockScore`、`lateRegionCount`、`stageGateRate`、`activeRegionAvg`、`firstThirdRegionCount`。
- 原因：上一版 PressureRead 虽能把平均可消压到约 4.7，但玩家仍可能感觉没有质变，因为多数选择仍是平行可扫项；真正的难点来自区域延迟打开、跨区关键锁和低选择门锁段。
- 当前做法：新增 StageLock 实验路径，先按入口区生成阶段计划，再尝试把非入口区独立根链合并到相邻低阶段关键链尾部，减少平行入口，让链条变成门锁结构的一部分。
- 当前结果：小批 StageLock demo 产出 2 关，真实 trace 均为 `ProcessTier=S`，`maxChoices avg=6.5`，`stageLockScore avg=0.674`，`lateRegionCount avg=3`，`stageGateRate avg=0.322`。
- 约束：这是方法突破，不是量产突破；当前候选稀缺且生成较慢，后续应把阶段/门锁语义前置到 SGP 生成器，而不是主要依赖后处理硬捞。

## 2026-06-19 - Hard SGP Needs Many Medium-Long Structure Chains

- 状态：实验中
- 决策：难关结构不追求单根极长链，而是追求多根中长链与结构承载链的组合。
- 依据：外部 298 seed 结构画像显示，高复杂样本通常 `avgChain≈10-15`、`longChainRate≈0.4-0.55`、`structureCarrierRate≈0.5`，但 `maxChain` 多在几十格量级；`maxChain>100` 的单根巨链容易变成单结构支配，不等同于好难度。
- 当前做法：StageLock 候选输出记录 `avgChain`、`maxChain`、`longChainRate`、`structureCarrierRate`、`straightLikeRate`、`shortChainRate`、`complexChainScore`，并在 demo 排序中把 `maxChain>90` 的巨链风险样本放后面。
- 约束：参考 seed 仅用于结构画像与指标校准；正式生成仍使用当前 SGP/StageLock 生产能力，不把外部参考 seed 直接混入正式包。

## 2026-06-19 - Dependency-Child Merge Is a Door-Lock Operation

- 状态：实验中
- 决策：StageLock 可以把“被解锁后立刻成为独立选择”的子链并入其 blocker 关键链，而不是把它作为下一步平行选择暴露给玩家。
- 原因：玩家感受到的难度不只是可消数量低，还包括关键链是否真正承担开门作用。A 解锁 B 后 B 贴近出现，会像连续扫；A+B 合成一根关键长链后，选择曲线更低，结构链更强，也更接近“解锁一个区域”的节奏。
- 当前证据：`DepMerge5` 小批中 120 源生成 5 关，真实 trace 为 `S=4/A=1`；代表关 `dense_weave_g1_v08` 达到 `openers=2`、`avg=3.462`、`max=6`、`longChainRate=0.513`、`structureCarrierRate=0.641`。
- 约束：合并必须基于已存在依赖边和端点相邻关系，合并后必须重新计算依赖并通过可解与过程曲线评估；不能为了变长而盲目拼链。

## 2026-06-19 - Hard Source Sorting Must Balance Structure and Process

- 状态：实验中
- 决策：StageLock 源排序不能只追求硬结构或长链，应同时惩罚源关卡原始 `openers`、`avgChoices` 和 `maxChoices` 过高。
- 原因：纯硬结构排序会把许多“结构强但流程太散”的源排在前面，依赖合链 20 次后仍可能 `static curve too open`；平衡排序能更早命中 `dense_weave`、`sweep`、`section_unlock` 等可被压成门锁节奏的源。
- 当前证据：平衡排序 + 依赖合链预检在 60 源内生成 6 关，真实 trace `S=5/A=1`，优于之前 120 源生成 5 关的产率信号。
- 约束：仍保留结构指标门槛，不把源排序退化成低开口优先；最终必须继续通过 trace 和结构指标。

## 2026-06-19 - LoopMerge Is a Valid Hard-Level Production Route, But Needs Source Profiling

- 状态：实验中
- 决策：StageLock 依赖子链合并可以做小循环，而不是只做单次后处理；每轮合并后必须重新计算依赖和过程曲线，只有可解且不变差/更好的结果才保留。
- 原因：真实难度来自“关键链承担开门作用”，多轮合并可以把连续暴露的子链压成结构长链，降低中盘可选数量，同时增加链条复杂度。
- 当前证据：LoopMerge160 在 160 源中生成 11 关，真实 trace 为 `S=9/A=2`、`over10Rate avg=0`、`maxChoices avg=7.09`、`stageLockScore avg=0.667`。
- 约束：不能靠 `maxChain>90` 的单根巨链作为默认难度来源；量产排序应优先多根中长结构链，并补齐 SGP 源结构画像缓存，让排序前就能使用 `longChainRate/structureCarrierRate`。

## 2026-06-19 - Source Profiling Explains Production, But Does Not Expand It Alone

- 状态：实验中
- 决策：SGP 难关生产必须保留源结构画像作为前置诊断和排序输入，但不能把“画像排序”当成新的产能来源。
- 原因：画像显示 SGP 源平均 `longChainRate=0.199`，低于外部参考 seed 的 `0.314`；当前成功样本主要靠 LoopMerge 后处理补足中长链，而不是源本身已经具备足够门锁结构。
- 当前证据：画像排序同口径 160 源仍生成 11 关；`CachedPotential -ExcludeCachedGenerated` 扫描 160 个未出货高潜力源生成 0 关，失败集中在 `static curve too open` 和 `weak stage lock`。
- 约束：后续产能提升应把 stage/door 语义和中长链比例前置到 SGP 源生成；入口 root merge 默认关闭，仅保留为显式实验开关，因为它在 60 源小批中将产量从 6 降到 5。

## 2026-06-19 - Source Long-Chain Enhancement Must Be Direction Aware

- 状态：实验中
- 决策：可以在 StageLock 前轻量增强源链条长度，但不能盲目把相邻端点链条大量合并。
- 原因：重度预合链会减少可选择的头尾方向组合，直接破坏 StageLock/acyclic orientation 的搜索空间；温和预合链可以保留一部分可门锁化能力，但仍不是最终产能突破。
- 当前证据：重度预合链 20 源产 0；温和预合链 80 增强源产 7，trace `S=6/A=1`、`stageLockScore avg=0.675`。
- 约束：下一步源增强应先建立潜在 door/blocker 关系，再沿这个关系合链；中长链必须服务依赖语义，不能只服务结构指标。

## 2026-06-20 - Dependency-Aware Source Enhancement Is Quality-Positive But Throughput-Limited

- 状态：实验中
- 决策：StageLock 源增强新增 `DependencyAware` 模式，只有当链条的逃逸射线确实被另一根链阻挡、且端点可相邻合并时才拉长；优先跨区、带转折、结构承载链，避免同侧外圈纯直线互接。
- 原因：玩家需要的是“长链承担门锁/阻挡语义”，不是简单把相邻短链拼长。盲合链会破坏 StageLock orientation，而 DependencyAware 能把长链增长绑定到真实依赖关系上。
- 当前证据：40 源生成 67 个增强源，StageLock 产出 5 关；真实 trace 为 `S=4/A=1`、`over10Rate avg=0`、`maxChoices avg=8.2`、`stageLockScore avg=0.683`。候选结构平均 `avgChain=9.686`、`longChainRate=0.394`、`structureCarrierRate=0.546`、`straightLikeRate=0.038`。
- 约束：该路线质量明显提升，但速度慢且产率仍低；量产前需要 dependency option 缓存/候选链限缩，或把 door/stage 语义直接前置到 SGP 源生成。

## 2026-06-20 - Hard StageLock Production Needs Targeted Source Feed Plus Trace Selection

- 状态：实验中
- 决策：难关量产不能只靠 raw hard-structure 排序；应先用 StageLock-targeted source feed 选择 `section_unlock/dual_zone/sweep/lock_buckle/dense_weave/outer_shell` 等高转化源，再做 DependencyAware 增强和 trace 后精筛。
- 原因：DepAware 成功样本不是最长源，而是可被压成阶段门锁的空间骨架。把这种经验前置到源 feed 能显著提高单位扫描产率。
- 当前证据：targeted 前 40 源生成 68 个增强源，StageLock 产出 12 关；真实 trace `S=11/A=1`、`maxChoices avg=7`、`stageLockScore avg=0.721`，比上一轮非 targeted DepAware 的 5/67 明显提升。
- 约束：完整候选包和严格精选包要分开；候选包保留更多可人工看图样本，精选包用 `MaxPerBase/MaxPerFamily` 限制同父源重复，避免量产池同质化。

## 2026-06-20 - Multi-Slice Expansion Should Favor Quality Lanes Over Linear Scanning

- 状态：实验中
- 决策：targeted feed 可以按切片扩池，但不能假设后续切片产率与第一切片相同；每个切片必须独立 trace、精选和记录产率。
- 原因：第二切片 `SourceOffset=40` 生成 65 个增强源但只产 2 个 StageLock 关卡，虽然质量达标，产率远低于第一切片 12/68。这说明 StageLock feed 的排序前段有明显“黄金区”。
- 当前证据：第一切片精选 8 关，第二切片精选 2 关，合并后形成 10 关硬关池，`traceAvgChoices avg=3.835`、`traceMaxChoices avg=7.1`、`stageLockScore avg=0.741`、`structureCarrierRate avg=0.574`。
- 约束：后续扩产优先优化 DepAware 搜索速度和 feed 评分，而不是盲扫完整 feed；多切片结果必须用 merge 脚本按 base/family 去重后再进入正式候选池。

## 2026-06-20 - LongChainBias Is a Hard-Lane Style, Not a Throughput Fix

- 状态：实验中
- 决策：StageLock 可以通过显式 `LongChainBias` 增强复杂长链结构，但它应作为硬关风格 lane 使用，不能替代源可改造性筛选。
- 原因：真正难关需要低选择曲线、远依赖、阶段门锁和多根复杂长链同时成立。只拉长链条会降低可读性或产率；只压选择数又容易变成“低选择但不复杂”。
- 当前证据：`HighYield + LongChainBias` 在 12 源小批中仍产 4 关，`S=4`、`avgChoices=3.96`、`maxChoices=7.75`，并把 `avgChain` 从 9.883 提升到 11.408、`longChainRate` 从 0.396 提升到 0.45。`ReferenceLong + LongChainBias` 产率低但结构更强，`avgChain=13.21`、`longChainRate=0.477`。
- 负结果：HighYield 后 19 源开 `LongChainBias` 仍只产 1 关，说明后段瓶颈是源太开放、不可门锁化，不是合链强度不足。
- 约束：`LongChainBias` 合并必须通过可解和真实 process trace；最终池仍需 base/family 去重，且 `maxChain>80` 的样本需要人工看图确认是否过度单链支配。

## 2026-06-20 - Symmetry Expansion Is a Supply Multiplier for Proven Hard Sources

- 状态：实验中
- 决策：可以对已经验证高产的 `HighYield` 源做 `FlipX/FlipY/Rot180` 几何扩展，再走原有 `DependencyAware -> StageLock -> trace` 验证链路，用作难关候选池扩容。
- 原因：StageLock 的瓶颈是可门锁化源不足。对高产源做几何变体能保留源的可改造性，同时提供不同视觉走向；这比继续线性扫开放度过高的 feed 尾部更有效。
- 当前证据：HighYield 头部 12 源扩展成 36 个 symmetry 源后，产出 7 个 `S` 级 hard selected；与原 hard10、LongBias 结果合并去重后得到 15 关候选池，`traceAvgChoices avg=3.918`、`traceMaxChoices avg=7.267`、`stageLockScore avg=0.705`、`longChainRate avg=0.409`。
- 补充证据：HighYield 尾部 19 源扩展成 57 个 symmetry 源后只产出 2 个 `S` 级 hard selected，说明 symmetry 能补一点供给，但不能根治开放度过高/不可门锁化的源。
- 约束：symmetry expansion 会带来同源结构风险；必须按 source hash 去重，并配合 `MaxPerFamily`、人工视觉筛选和后续新源生成。它是头部高产源的供给放大器，不是替代真正源生成能力的最终方案。

## 2026-06-20 - Successful Enhanced-Source Bootstrapping Is the Current Hard Production Lever

- 状态：实验中
- 决策：在真正把 stage/door 语义前置进 SGP 生成器之前，Hard StageLock 量产优先使用“已验证成功增强源 -> symmetry 扩展 -> DependencyAware 再增强 -> StageLock -> trace 精选”的自举路线。
- 原因：盲扫 targeted feed 后段、unprofiled Direct/NoMask 源池和随机 orientation rescue 都没有扩大产能；相反，从已证明可门锁化的 16 个 enhanced source 自举，48 个 symmetry source 产出 18 个 StageLock 候选并精选 12 个，产率和质量都明显更高。
- 当前证据：HardProduction23V3 合并后得到 23 关，`traceAvgChoices avg=3.635`、`traceMaxChoices avg=6.652`、`stageLockScore avg=0.727`、`avgChain avg=11.168`、`longChainRate avg=0.440`、`structureCarrierRate avg=0.600`。
- 负结果：`RandomAttemptsPerLevel=60` 对 HighYield 头部 12 源没有增加产量，只复现原 7 关且耗时更高；unprofiled Direct/NoMask head80 trace 为 `Drop=76/A=2/B=2`，HighYield/ReferenceLong StageLock 小样均产出 0。
- 约束：自举路线会带来同源风险，必须用 `MaxPerBase`、`MaxPerFamily`、source hash 去重和人工视觉筛选控制；它是当前量产杠杆，不代表最终源生成能力已经完全解决。

## 2026-06-20 - True Hard Production Needs StageDoor Source Semantics For 50+ Chains

- 状态：实验中
- 决策：当前 StageLock 后处理不再继续硬压 90 链级别大源；50+ 链的真正硬关需要在 SGP 源生成阶段前置 stage/door 语义。
- 原因：参考复杂 seed 的可用 S/A 子集平均约 `chains=50`、`avgChain=12`、`avgChoices=4.05`、`maxChoices=8.15`。我们的 V4Bootstrap 过程更硬，但 `chains≈34`。尝试用 `HardXL` 直接挑 95 链源后处理，`DependencyAware + StageLock` 产出 0；放大入口 root merge 后仍为 0。
- 当前证据：HardXL 失败分布为 `no stage-lock orientation=13`、`weak stage lock=9`、`static curve too open=6`，说明问题不是入口 merge 单点，而是大源没有从生成阶段建立可门锁的区域/门关系。
- 可用补充：高链成功源自举可产出少量复杂长链候选，`sym_highchain12` 得到 16 个 `S/Low` candidates，平均 `avgChain=10.706`、`longChainRate=0.493`、`structureCarrierRate=0.592`，但同源风险高，正式池必须启用 structure signature 限制。
- 约束：`HardXL` preset 保留为诊断入口；不要把它当默认量产路径。下一步要做 `StageDoorSGP`，即源生成时显式区分入口区、延迟区、门锁链、跨区 blocker 和区域解锁顺序。

## 2026-06-20 - Hard Candidate Selection Must Include Structure Signature Caps

- 状态：采用
- 决策：StageLock hard 精选除 `MaxPerBase` 和 `MaxPerFamily` 外，还需要 `MaxPerStructureSignature` 控制粗结构重复。
- 原因：symmetry/self-bootstrap 会产生 source hash 不同但结构指标几乎相同的镜像/旋转候选，单靠 base/family 去重无法阻止同构样本挤满池子。
- 当前证据：`sym_highchain12` 的 16 个 candidates 全部 `S/Low`，但严格看只有少数粗结构；`MaxPerStructureSignature=1` 后从 4 个 hard selected 压到 2 个，更适合作为正式长链补充。
- 约束：structure signature 是粗筛，不替代人工看图；它应该用于自动池合并和预精选，最终视觉同源感仍需人工审查。

## 2026-06-20 - StageDoor Source Enhancement Is a Variant Lane, Not Capacity by Itself

- 状态：实验中
- 决策：`StageDoor` 源增强可以用于定向生成更低选择、更长链、更远跳转的 hard variants，但暂不把它当成扩大父本上限的主产能方案。
- 原因：HardMid/HardMidWide/Minority success 小批均能产出真实 trace `S` 的候选，说明 door/stage 语义前置到源增强阶段是对的；但这些候选按 source hash 合入 V5Preview 时大多被视为已有成功父本的替代版本，不能显著扩大去重池。
- 当前证据：HardMid 23 源产 3 个 S；HardMidWide 43 源产 4 个 S；Minority success 7 源产 4 个 S。最终冻结 `StageDoorAdditions5Pack`，family mix 为 `section_unlock=2`、`lock_buckle=1`、`maze_long_chain=1`、`dense_weave=1`，过程 `avgChoices≈3.16-3.72`、`maxChoices=6-7`。
- 约束：StageDoor 增强后必须先跑真实 trace 验证源合法性，避免 HardXL 初期出现的非相邻链条假阳性；进入正式池前仍必须按 source hash、family、structure signature 去重。
- 下一步：用 StageDoor 作为 minority-family 替换/精品 lane；真正扩到 50+ 链 hard levels 需要在 SGP 源生成器中直接生成入口区、延迟区、门锁链和跨区 blocker。

## 2026-06-20 - Minority Symmetry Plus StageDoor Is the Better Sparse-Family Lane

- 状态：实验中
- 决策：对 `sweep/maze_long_chain/dense_weave/zig_river` 等少数成功源，优先尝试 `symmetry -> StageDoor -> StageLock`，而不是只对原源做 StageDoor。
- 原因：单纯 StageDoorAdditions5 只有 5 个高质量替换样本；加入 symmetry 后，7 个 minority success 源扩出 21 个 symmetry source，再经 StageDoor 得到 39 个合法源，最终产出 6 个全部 `S` 的 StageLock 候选，能更有效补 dense/maze。
- 当前证据：`StageDoorLane9Pack` 合并 9 关，family mix 为 `dense_weave=4`、`maze_long_chain=2`、`section_unlock=2`、`lock_buckle=1`，`traceAvgChoices avg=3.636`、`traceMaxChoices avg=6.889`、`avgChain avg=10.640`、`longChainRate avg=0.437`。
- 约束：该路线仍是 proven-parent 扩展，可能有同源视觉风险；正式池中应作为 replacement/variety pool 使用，并继续依赖人工看图筛选。
- 操作注意：通过 PowerShell 调用 symmetry 脚本时，`-Transforms` 要用显式数组，否则可能只跑 `FlipX`。

## 2026-06-20 - Use Strict45 as the Current Hard Candidate Review Pool

- 状态：采用
- 决策：当前 Demo 和人工审查入口使用 `HardProduction45V6StageDoorStrictPack`，而不是更宽的 cap10 47 关或 cap12 53 关合并池。
- 原因：cap10 47 关证明 StageDoorProduction24 对 V5 有真实新增容量，但其中包含一个 `traceMaxChoices=10` 的 sweep 和一个 `stageLockScore=0.555` 的低分 sweep。Strict45 通过 `traceMaxChoices<=8` 和 `stageLockScore>=0.60` 去掉这两个风险样本，同时保留 1 个高质量 sweep 和 `zig_river`。
- 当前证据：Strict45 仍保留 45 关，输入贡献为 V5=28、StageDoor=17；指标为 `traceAvgChoices avg=3.356`、`traceMaxChoices avg=5.956`、`stageLockScore avg=0.755`、`avgChain avg=12.047`、`longChainRate avg=0.475`。
- 约束：Strict45 是当前 hard candidate pool 的审查入口，不代表 raw 50+ chain 新源生成完成；后续若继续扩源，应仍按相同或更严格的 process/structure gate 合入。

## 2026-06-20 - ReferenceComplex Is a Long-Chain Supplement Lane, Not the Main Hard Production Lane

- 状态：采用
- 决策：新增 `ReferenceComplex` 源筛和结构化精选阈值，用于给 hard pool 补充复杂长链样本；但不把它作为主量产路线。
- 原因：参考 298 seed 的 top complex 画像显示，强结构样本通常有 `avgChain≈12-15`、`longChainRate≈0.45-0.60`、`structureCarrierRate≈0.55+`。`ReferenceComplex` 能抓到这种链条语言，但源越复杂越容易丢失 StageLock orientation，因此产率低。
- 当前证据：24 个 ReferenceComplex 源按 4 源小切片跑完，只精选 7 关；但这 7 关质量高，代表样本达到 `avgChain=15.036`、`longChainRate=0.536`、`structureCarrierRate=0.750`、`traceMaxChoices=5`。合入 Strict45 后形成 V7 52 关池，平均 `avgChain=12.100`、`longChainRate=0.479`、`structureCarrierRate=0.623`，且 `traceMaxChoices avg=5.962` 没有变松。
- 负结果：直接跑 `ReferenceComplex` 12 源、4 变体、120 StageDoor 源会在 StageLock 阶段超时；只筛最长源切片产出 0，主要失败是 `no stage-lock orientation`。
- 约束：ReferenceComplex 后续应按小切片运行，`StageDoorVariantsPerSource=3`，精选阈值以 `traceMaxChoices<=8`、`MinAvgChain>=10`、`MinLongChainRate>=0.38`、`MinStructureCarrierRate>=0.50`、`MinTraceStageLockScore≈0.58-0.60` 为基准。不要为了长链先把源过度合并。

## 2026-06-20 - Do Not Use Rot90/Rot270 on Current Proven Portrait Sources

- 状态：采用
- 决策：当前 `FlipX/FlipY/Rot180` 可继续用于 proven source 扩展；暂不加入 `Rot90/Rot270` 作为默认扩展。
- 原因：检查 V3 proven source feed 后，所有可解析源都是瘦长竖屏，90 度旋转会变成横屏宽关，不符合竖屏游戏体验。
- 当前证据：22 个 proven source 尺寸包括 `16x24`、`15x22`、`17x27`、`15x25` 等，`rotatedAspect<=1.15` 的合格源为 0。
- 约束：只有当未来出现接近正方形或横向源，且旋转后仍满足竖屏比例时，才考虑加入 Rot90/Rot270。

## 2026-06-20 - Mid-Chain Hard Levels Need Chain Preservation Plus Door Semantics

- 状态：实验中
- 决策：中型难关实验使用 `MinOutputChains`/`MinChains` 保留最终链组规模，当前建议底线为 36；40 链以上先作为诊断目标，不作为默认量产门槛。
- 原因：V7 难关池过程曲线已经很紧，但平均链组数偏低。直接把父本做大后，StageLock 会通过重合链把最终链组压回 25-35；如果强制 40 链底线，失败集中为 `weak stage lock` 和 `no stage-lock orientation`。
- 当前证据：`HardMidWide + MinOutputChains=40` 在 70 个合法 StageDoor 源中产出 0；降到 `MinOutputChains=36` 后得到 1 个 47 链 `S` 级候选，`traceAvgChoices=4.32`、`traceMaxChoices=8`、`avgChain=13.681`、`longChainRate=0.596`、`structureCarrierRate=0.723`。
- 约束：不能靠放宽选择曲线换链组规模；中型链组候选仍必须通过真实 trace。下一步产能突破应强化 StageDoor 源生成里的显式 gate/door 语义，而不是继续盲扫更大的 HardXL 源。

## 2026-06-20 - StageGateSearch Adds a Second Hardness Lane Beyond Pure GateRate

- 状态：实验中
- 决策：`Build-PressureReadStageLockVariants.ps1` 增加 `-StageGateSearch`，用于中型链组/复杂长链实验时搜索入口区域和根链组合；验收上同时承认两类难关：强门锁型和紧曲线型。
- 原因：只看 `stageGateRate` 会误杀一类真实有难度的关卡：`openers≈6`、`avgChoices≈3.6-4.0`、`maxChoices≈6`、区域仍有晚开，但 gate 步比例不高。这类关卡玩家每步选择少，不是无脑扫，应作为 hard lane 保留。
- 当前证据：`GateStrong` 源在静态上有大量门候选，但真实 StageLock 原验收 0 产出；加 `StageGateSearch` 和紧曲线验收后，30 个源产出 3 个 36+ 链 `S/Low` 候选，并可合入 V8Probe55。
- 约束：紧曲线型只能在 `StageGateSearch + MinOutputChains>=36` 下启用，且必须满足低选择压力（如 `avgChoices<=4.15/maxChoices<=7` 或更低）和真实 trace。不要把它变成普通开放关的放水入口。

## 2026-06-20 - RefComplex Candidate Salvage Is a Review Addition Lane

- 状态：实验中
- 决策：允许从已生成但未入池的 `ReferenceComplex` candidates 中做严格 salvage，但只作为 review addition lane，不替代主生产路线。
- 原因：旧门槛 `avgChain>=10`、`structureCarrierRate>=0.5` 会挡掉少量实际过程很强的样本。例如 V9 salvage 的 5 个候选全部真实 trace `S/Low`，平均 `stageGateRate=0.491`，外圈直链连续风险为 0；其中一个 dual-zone 样本达到 `avgChoices=3/maxChoices=6`，只是 `avgChain=9.758`。
- 当前证据：Salvage 严格去重后精选 3 关并合入 V9Probe58，整体池仍保持 `traceAvgChoices avg=3.418`、`traceMaxChoices avg=6.086`、`longChainRate avg=0.479`。
- 约束：Salvage 必须 source-hash 排除当前池已有样本，并且必须重新跑真实 trace；family 和 structure signature 需要限额，避免 outer_shell/同构样本过量。它是补漏工具，不代表生产能力提升。后续执行优先使用 `Select-StageLockSalvageCandidates.ps1`，不要回到不可复盘的一次性扫描命令。

## 2026-06-20 - HardMidWide Uses DoorBalanced StageDoor Before StageGateSearch

- 状态：实验中
- 决策：`hardmid_wide` 中型长链扩产优先使用 `DoorBalanced -> StageGateSearch -> MinOutputChains=36` 的小切片流程；暂不使用 `GateStrong` 作为默认 profile。
- 原因：`GateStrong` 对 hardmid_wide 源过窄且慢，4-16 源验证中没有形成稳定 StageDoor 输出；`DoorBalanced` 能产出合法增强源，并在两个 4 源微切片中各产 1 个 36+ 链 hard selected。
- 当前证据：新增 40 链 maze 候选为 `S/A`，`traceAvgChoices=3.6/max=7/stage=0.630`；新增 37 链 dense 候选为 `S/S`，`traceAvgChoices=3.0/max=6/stage=0.606`。合入 V11Probe68 后整体 `traceAvgChoices avg=3.434`，没有明显变松。
- 约束：该 lane 产率慢，应按 4-8 源小切片运行并及时归档输出；不要一次性跑大 wrapper。进入主池仍要 source hash/family/structure signature 去重，并人工看图排除拼接感或外出口异常。

## 2026-06-20 - Use Chunked Micro-Slices for HardMidWide DoorBalanced Production

- 状态：采用
- 决策：hardmid_wide 的中型长链扩产默认采用 `Run-HardMidWideDoorBalancedStageGateMicroSlice.ps1`，按 1-2 个源/每批、StageLock chunk 6-8 个增强源的方式推进。
- 原因：大 wrapper 容易超时或在某个阶段中断；chunked micro-slice 已经证明能把失败限制在单个 chunk，并保留 symmetry、StageDoor source、source trace、StageLock candidate、trace/select 的中间证据。
- 当前证据：`existing_next6` 三个 2 源微切片新增 4 个 hard selected，其中一个达到 53 链，且合入 V12Probe72 后整体 `traceAvgChoices avg=3.441`、`longChainRate avg=0.484`，没有明显变松。
- 约束：该流程仍然产率慢；不要用它盲扫缺源或低长链信号的 offset。优先先构造“存在且长链/door signal 较强”的 small feed，再微切片运行。所有新增样本仍需 trace/select 和最终人工看图。

## 2026-06-20 - HardMidWide Auto Feed Needs StageDoor Trace Feedback

- 状态：实验中
- 决策：hardmid_wide 中型长链扩产新增 `Build-HardMidWideMicroSourceFeed.ps1`，先自动筛父源，再用 `Run-HardMidWideDoorBalancedStageGateMicroSlice.ps1` 小切片验证；但下一步不能只靠静态源画像，必须加入 StageDoor 中间源 trace 反馈/缓存。
- 原因：自动源筛排除 V12 next6 后，从 43 个 hardmid_wide 源中选出 15 个可试父源，前 4 个父源新增 3 个真实 hard 候选，说明它能替代手动 offset；但后续 auto04/06/08/10 暴露出 weak stage lock、dependency too local、static curve too open、no symmetry rows 等问题，说明静态长链/结构承载/门潜力不能单独预测可门锁化。
- 当前证据：V13 新增 3 个候选：两个 37 链 dense_weave，分别为 `traceAvgChoices=4.19/max=8/S/A` 与 `3.00/max=6/S/S`；一个 40 链 maze_long_chain 为 `traceAvgChoices=3.60/max=7/S/A`。合入后 V13Probe75 平均 `traceAvgChoices=3.447`、`traceMaxChoices=6.213`、`longChainRate=0.486`、`structureCarrierRate=0.620`，没有明显变松。
- 约束：继续使用 1-2 父源微切片和小 StageLock chunk；不要把 auto feed 后段盲目跑完。下一步应记录每个父源经 DoorBalanced 后的 source trace 分布，例如中间源 `processTier/tightProcessTier/stageLockScore/maxChoices`，用于惩罚 tight Drop、局部依赖和静态曲线太开的父源。

## 2026-06-20 - Complex Long Chains Should Be Refit Onto Proven StageDoor Skeletons

- 状态：实验中
- 决策：提升 hard level 链条复杂度时，优先使用“已证明可出 hard 的 StageDoor 源 -> AllowEntryRootMerge + LongChainBias + StageGateSearch 二次 refit -> 真实 trace/select”，不要把更像参考 seed 的长链父源直接作为默认前置筛选。
- 原因：参考 298 top complex 的父源画像确实更长链，但直接筛这种父源会压缩 StageLock 可定向空间，导致 `no stage-lock orientation` 和 `weak stage lock`。相反，成功 StageDoor 源已经具备可解门锁骨架，在其上受控合链能增加 avgChain/veryLongChainRate，同时保持低选择曲线。
- 当前证据：V15 reference-complex parent 两个小切片 27 个 StageDoor 源产出 0，失败集中为 `no stage-lock orientation=17`；productive refit 两批 24 个成功 StageDoor 源产出 6 candidates，真实 trace 全部成功，strict selected 4，其中 1 个是 V13 之外的新 source hash。新样本 `src_hash_a584d049` 达到 `chains=37`、`avgChain=11.784`、`veryLongChainRate=0.297`、`structureCarrierRate=0.730`、trace `S/A`。
- 约束：productive refit 当前更像 quality/refinement lane，不是大规模扩父源 lane；扩量上限受 proven StageDoor 源池限制，并且必须按 source hash/family/structure signature 去重。

## 2026-06-20 - Productive Refit AllHistory Uses Medium-Long Orientable StageDoor Sources

- 状态：实验中
- 决策：Productive Refit 的默认扩产源应来自历史 StageDoor source 的中等长、可定向 siblings，而不是分数最高的 refcomplex 超长 siblings。
- 原因：全历史 feed 的 head12 结构分最高但 0 产，失败集中 `no stage-lock orientation=10`；tail12 来自 hardmid36/hm36db/v3head-like 源，产出 2 个真实 trace `S/A` hard candidates，并与 V15 合并后把池子从 76 扩到 78。
- 当前证据：新增 `src_hash_7192ee4b`（section_unlock，42 chains，avgChain=10.643，traceAvg=4.02/max=7/stage=0.716）和 `src_hash_642f316a`（dense，43 chains，avgChain=10.140，traceAvg=4.33/max=7/stage=0.607）。
- 约束：后续 all-history refit 要按 12 行小切片跑；source score 必须加入 orientation-risk 反馈，避免反复跑 refcomplex-overlong head。进入正式包继续 source hash/family 去重。

## Productive Retry Is Preferred Over Broad Strong-Chain Scanning - 2026-06-20

- 决策：SGP StageLock 难关量产继续以 productive-parent retry 为主线，而不是盲扫所有强长链源。
- 理由：V17 strong-chain feed 平均结构指标更强，但失败集中为 `no stage-lock orientation`；复杂长链本身不能保证能形成阶段门锁。
- 落地：`Build-ProductiveRefitStageDoorSourceFeed.ps1 -PreferOrientableHistory` 保留长链奖励，同时惩罚过开放源曲线和历史低产 refcomplex 头部源。
- 约束：参考 298 seed 只作为结构指标校准，不直接混入正式生产包。

## StageLock Risk Cache For Productive Refit - 2026-06-20

- 决策：Productive Refit 源筛增加 `-UseStageLockRiskCache`，把历史 rejected/selected 反馈回 StageDoor 源排序。
- 理由：V17 strong-chain 和 risk-aware clean 实验都显示，当前瓶颈是 source orientability；重复尝试 `no stage-lock orientation` 源会浪费时间。
- 落地：对 exact StageDoor source row 的 `no stage-lock orientation`、`weak stage lock`、`static curve too open` 等历史失败扣分，对曾出 selected hard 的 source row 加分。

## StageLock Risk Cache Uses Source Hash - 2026-06-20

- 决策：`Build-ProductiveRefitStageDoorSourceFeed.ps1 -UseStageLockRiskCache` 的风险 key 从 exact LevelId 升级为 `Get-BaseKey(...)`，即 `src_hash_xxxxxxxx`。
- 理由：同一个 StageDoor source hash 的 v2/v3/v4 或不同 run label 变体应共享 `no stage-lock orientation`、`weak stage lock` 等历史风险；否则会反复尝试同源变体。
- 结果：V19 feed 暴露剩余 source 空间已经没有 clean row，但低 orientation 风险 section 子集仍产出 2 个 strict hard candidates。

## NearMiss Rescue Uses Budgeted Single-Source Probes - 2026-06-20

- 状态：采用为实验默认。
- 决策：`NearMissChainRescue` 的 carrier absorb 搜索必须支持显式预算，并优先以单源/小块快筛推进；不要对 maze/dual 等复杂源直接满预算盲跑。
- 理由：c03/c04 类源在满预算 carrier absorb 下容易爆搜或超时；预算化后，ProductiveRefit dual_zone `h0bd649` 可在 21 秒内产出 1 个 strict hard selected，证明低预算足以发现部分正样本。
- 当前证据：预算化 dual_zone 样本真实 trace `S`，`avgChoices=3.92`、`maxChoices=7`、`stageLockScore=0.562`、`avgChain=10.556`、`longChainRate=0.444`、`structureCarrierRate=0.583`、`straightLikeRate=0`。新增 `Build-NearMissRescueSourceFeed.ps1` 后，prefilter dense `h4f810` 低预算 43 秒产出 strict hard selected，`avgChain=12.111`、`longChainRate=0.444`、`structureCarrierRate=0.722`。ProductiveRefit maze `h62e1b` 即使中预算到 `mergedCarrierAbsorbCount=5`，仍只有 `longChainRate=0.375`、`structureCarrierRate=0.450`，不继续硬烧。
- 约束：rescue 不能替代源级 stage/door 几何。后续应先用 `Build-NearMissRescueSourceFeed.ps1` 做 source-level rescue prefilter，优先 `processTier=S/A`、`stageLockScore>=0.55`、`longChainRate 0.36-0.42`、`structureCarrierRate>=0.50`、`mergePotentialStructure/Long` 高的 near-miss 源，再低预算重试；先跳过 `maze/maze_long_chain` mid profile，除非单独探索低选择压力 lane。

## Hard Lane V2.2 Separates Choice Peaks From Edge Follow - 2026-06-20

- 状态：实验中。
- 决策：Hard Lane V2.2 把 `choices>=8` 单独作为峰值信号，使用 `choicePeakCount/Rate/Excess`，不再只依赖 `avgChoices/maxChoices`。
- 决策：外圈顺消感拆成 solve follow 与 dependency follow 两类指标：`outerNearFollow*`/`sameSideOuterFollow*` 观察连续解题中的近边连消，`outerNearDependency*`/`sameSideOuterDependency*` 观察上一根是否直接解锁下一根。
- 当前门槛：Review5 初版允许 `outerNearFollowRunMax<=2`，但保持 `sameSideOuterFollowRunMax<=1`、`choicePeakCount=0`、`choicePeakExcess=0`、`maxChoices<=7`。
- 理由：Review5 中 `outerNearFollowRunMax=2` 的两个 section 样本仍为 `S/S`，同边 follow 和直接近依赖都低；而 dual/sweep diversity 样本虽低选择但外圈近依赖偏高且链条结构不足，不能为多样性放进来。

## 2026-06-21 - Difficulty Judgement Must Guide Generation And Merging

- 状态：实验中。
- 决策：Greedy 只作为可解验收和 solve trace 来源，不再把 `processTier=S` 或低 `avgChoices` 直接等同于“真的有难度”。后续生成和合链必须参考 `difficultyScoreV1`、浅层反事实指标和 `dependencyFollowRunMax/Rate`。
- 原因：V14 证明外圈可以做到干净，但仍会出现 `dependencyFollowRunMax=3/4` 的顺着消；V20 证明旧池里同时满足外圈干净和 non-follow 的候选为 0。也就是说，难度瓶颈不是“能不能解”，而是“玩家是否被迫读结构，而不是沿 parent-child 一路扫”。
- 当前约束：合链/修复后的候选如果把 `dependencyFollowRunMax` 推到 3 以上，应拒绝或加入 non-gate interruptor；目标 hard lane 优先把 `dependencyFollowRunMax<=2` 作为 acceptance floor，同时继续约束外圈出口头和外圈动态可消压力。
- 影响：下一步生成侧要同时前置 `outer-head-zero` 和 `non-follow interruptor`，不能继续先产出再靠后验筛选硬捞。

## 2026-06-22 - Compression Summaries Are Not Project Memory

- 状态：已采用
- 决策：对话压缩摘要只能作为临时恢复辅助，不能作为长期事实来源；后续仍要依赖的核心信息必须写入 `.agents/memory/` 或 `.agents/index/`。
- 原因：压缩可能保留大方向，但不保证保留精确路径、commit、branch、worktree、验证结果、失败原因、用户约束和下一步。
- 影响：阶段完成、长时间实验、上下文可能压缩、切换 worktree/分支、合并/清理/交接前，先写 5-10 行 checkpoint。
- 约束：checkpoint 只记录结论、路径、commit/branch/worktree、验证结果、风险和下一步，不复制大段日志，不记录敏感信息。

## 2026-06-22 - Rosetta GPT Is An Advisor, Not The Decision Owner

- 状态：已采用
- 决策：需要方案顾问、架构 second opinion 或实验路线分歧时，可以通过 Rosetta 咨询 GPT；GPT 只作为外部方案顾问，不直接决定项目方向。
- 原因：GPT 可以帮助审稿、指出盲点、提出替代路线，但它不拥有项目上下文、用户偏好、完整实测数据和本地工作树风险。
- 工作流：咨询前整理高信号 prompt；回复后先做项目侧审查；若方案不认可，补充细节继续追问，直到形成可验证共识或明确记录未达成一致。
- 落地条件：只有当方案被项目侧认可、验收方式清楚、风险边界明确后，才能写入 `DECISIONS.md`、`WORKFLOW.md`、index 或正式实现。
- 约束：不得把敏感信息、大段日志、未验证猜测直接发给 GPT；不得把 GPT 建议未经验证地写成项目规范。

## 2026-06-22 - GPT Consultation Uses Sanitized Abstract Prompts By Default

- 状态：已采用。
- 决策：通过 Rosetta 咨询 GPT 时，默认使用 `GPT-Safe Abstract Prompt`，把本地路径、脚本名、资源包名、GUID、精确 CSV/报告名、专有项目结构和大段实验数据抽象成角色、趋势和约束；只有用户明确授权后，才发送可识别的项目细节。
- 原因：Rosetta 安全审查会拒绝把非公开项目结构、内部实验指标和 workflow 细节发送到外部 ChatGPT 对话。默认抽象化能保留方案讨论价值，同时避免每次在关键节点被安全策略拦住。
- 分级：`safe_to_send` 可直接发送；`requires_user_approval` 发送前必须提示风险并获得明确授权；`never_send` 包括密码、token、私钥、证书、个人隐私、大段未筛日志、未验证猜测和可复制核心资产的内容。
- 工作方式：GPT 负责抽象策略审稿，例如 operator 优先级、reject rule、acceptance gate 和下一实验；Codex 负责把 GPT 建议映射回本地文件、真实指标、Unity 资源和 board-level trace 验证。
- 失败处理：若 Rosetta 拒绝，先降级为更抽象的 prompt；不能通过等价外部发送绕过。若确需细节且工具仍拒绝，则由用户手动决定是否把内容发给 GPT，Codex 继续本地执行和验证。

## 2026-06-22 - Trace Bridge Proof Requires Frozen Execution Replay

- 状态：已采用。
- 决策：SGP hard-lane 的 support/trace bridge 不能只看 static dependency graph、source-side bridge probe 或生成时内部分数；必须在最终/冻结 LevelDefinition 上通过 trace replay 验证 `target -> hub -> support -> upstream` 的 ray-collision 静态存在和解题顺序。
- 原因：本轮 proof 发现生成侧 `traceAnchorBridgeScore` 可以为 `0.96+`、depth=3，但如果后续 support compatibility search 覆盖 fixed orientation，独立 trace 会丢失 upstream；另一个坑是冻结 pack CSV 若不携带 T/H/S/U 字段，planned replay 会误报 `missingStaticTargetRay`。
- 落地：`Build-PressureReadStageLockVariants.ps1` 在 compatibility search 后重套 fixed target/support orientation；`Build-SGPRhythmTrace.ps1` 新增 planned trace bridge replay 字段；正式判断使用带桥字段的 frozen replay input 与 `trace_bridge_proof_v1_frozen_replay_with_bridge_metrics.csv`。
- 约束：`-AllowWeakStageForTraceBridgeProbe` 只能作为 proof/诊断开关，不能作为正式 hard-lane 生产入口。只有同时满足可解、trace-visible bridge、stage quality、anti-local 和 local patch 限制的样本才可进入正式候选。

## 2026-06-22 - Board-Level Trace Is The Final Production Gate

- 状态：已采用。
- 决策：SGP hard-lane 最终生产门使用 board-level trace replay，即 `Build-SGPRhythmTrace` 语义；`Evaluate-Orientation` 只允许作为 cheap prefilter/heuristic，不再拥有最终接收权。
- 原因：本轮 realclosure 实验证明 internal graph/evaluator 可以认为 accepted/bridge valid，但写入 asset 后 replay 可能出现 `solved=False` 或 `missingStaticTargetRay`。只有 board trace 是 geometry-driven、ray-collision real、replay-based 的 gameplay truth。
- 落地：新增 `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Invoke-StageLockBoardTraceGate.ps1`，批量调用 `Build-SGPRhythmTrace.ps1` 后输出 accepted/rejected/summary；最终 gate 检查 solved、process tier、choice peak、local patch、dependency follow、outer exit、stage/anti-locality、support closure 和 planned bridge replay。
- 约束：不要尝试“让 Evaluate-Orientation 匹配 trace”或把抽象 graph 结论写成最终验收；这会重新引入 false positive 和 hard/local-easy 误标。

## 2026-06-22 - Choice Spike Must Be Fixed By Physical SGP Operations

- 状态：实验中。
- 决策：realclosure 当前剩余瓶颈是中盘 unlock fanout spike，而不是 bridge 断裂。`choiceWave=... 3 4 8 7 7 ...` 这种峰值不能只靠 graph-side `releasePhase`/activation time 修复，因为 LevelDefinition 资产没有抽象激活时间。
- 原因：最终依赖只由 `B.escapeRay hits A.geometry` 产生；如果不改几何、箭头方向或链条切分，`Build-SGPRhythmTrace` 不会看到任何 target release stagger。
- 当前可用过渡：`SGPRhythmLab_HardLaneTraceGateLowPeakV1Pack` 4 关通过 board trace low-peak gate（`maxChoices<=7`、soft cap 6 hit 1 次、support closure depth=2），但结构证明弱于 realclosure depth=3。
- 下一步优先级：在 realclosure 上做最小物理 stagger 实验，优先考虑 target orientation flip 或 relay split；sidecar insert/move 作为 fallback。必须保持 `U->S->H` bridge 静态 ray 与 trace order，并用 board trace 验证峰值是否从 8 降到 7/6。

## 2026-06-22 - Peak Prune Is A Proof, Not The Production Strategy

- 状态：已采用。
- 决策：允许用“移除 burst target 中单条链”的方式验证 choice peak 根因和 board trace gate，但不能把直接删链作为最终量产策略。
- 原因：Realclosure Peak Prune V1 已证明删除 `14/23/26/28` 中单条链可把 `maxChoices` 从 8 降到 7、`choice rise` 从 4 降到 3，同时保留 `solved=True`、`ProcessTier=A`、planned bridge replay `ok/d3`；但删除链会牺牲覆盖、形状和关卡完整性，不能代表最终设计能力。
- 量产方向：把同一物理效果升级为不损覆盖的 SGP-native 操作，包括 `relay split`（把一次解锁拆成两批）、`target stagger`（改变部分 target 的真实阻挡关系/方向）或 `bridge-safe relink`（只分散中盘 fanout，保留 `U->S->H->T` 桥）。
- 验收：任何 V2 操作都必须在冻结 LevelDefinition 上通过 board-level trace gate；至少满足可解、`maxChoices<=7`、choice soft cap 6 hit 不超过 3、`localPatchSolveRunMax<=3`、`dependencyFollowRunMax<=3`、planned bridge replay `ok/d>=2`，并且不明显降低覆盖/形状。

## 2026-06-22 - Relay Split Is The First Valid Fanout Dynamics Operator

- 状态：已采用为下一阶段主线。
- 决策：在当前 realclosure 主线上，优先使用 `relay split parent chain` 作为 Fanout Dynamics V2 的第一类物理操作；直接翻 burst target 只能作为诊断，不作为主线。
- 原因：直接翻 `14/23/24/26/28` 的 25 个 target-stagger 组合全部不可解并触发 planned bridge `targetOrderFailed`；而切分 fanout parent chain `11` 的 relay split 能在不删除覆盖的情况下，将 `maxChoices` 从 8 压到 5/6，并保持 `solved=True`、`ProcessTier=A`、planned bridge replay `ok/d3`。
- 约束：relay split 只能算真实物理操作，不能引入抽象 `releasePhase`。切分后必须保持 LevelDefinition 覆盖不降、chain id remap 正确、bridge anchor 字段可冻结复验。
- 下一步：把单点 `Build-ChoicePeakRelaySplitVariants.ps1` 推广为通用 detector/repair：从 step diagnostics 或 trace 中找 `unlockCount` 高、choice rise 高的 parent chain，批量试切点和段方向，用 board trace gate 自动选 `maxChoices<=5/6`、local/follow run 合格、planned bridge replay ok 的结果。

## 2026-06-22 - Relay Split Auto V1 Is A Review Pack, Not Yet A Diverse Production Pool

- 状态：已采用。
- 决策：`SGPRhythmLab_HardLaneRealclosureRelaySplitAutoV1Pack` 作为“至少 5 关真正有难度”的第一版 review pack；它证明多 source-row relay split 能稳定产出 5 关严格过线样本，但还不能代表多样性量产完成。
- 原因：s01/s02 分片各自 4/40 过线，冻结后 5/5 board trace accepted；4 关 `maxChoices=5`、1 关 `maxChoices=6`，全部保 planned bridge replay `ok/d3`，说明 fanout dynamics 自动化方向有效。风险是这些样本仍来自同一 realclosure family 和同一个 parent chain `11` 的 orientation 扩展，体感可能相似。
- 约束：短期人工评估可以看 Auto V1；后续量产必须把 parent 选择从手写 `11` 升级为 trace-driven detector，并扩大到更多 source/family。验收继续以 board-level trace gate 为准，不允许用 graph/evaluator 结果替代冻结 replay。

## 2026-06-22 - True Hardness Requires Trace-Visible Physical Lock Modules

- 状态：已采用为下一阶段突破方向。
- 决策：停止把旧 SGP 成品的后验筛选、外圈翻链、sidecar 微修或 relay split 当作“真正高难”的主突破口；下一阶段应先构造 board trace 可见的物理锁模块，再把该模块前移到 SGP source/body 生成和填充里。
- 原因：`DesignedHardLockV0` 证明只要按 `Propagate-Exits` 的真实逃逸传播语义放置 `upstream/support/hub/target`，可以稳定得到 `TrueHardCandidate`：4/4 solved、`causalAntiLocalityScore=0.688`、`supportClosureBestScore=0.921`、`supportClosureBestDepth=3`、`maxChoices=5`、`outerExitHeadCount=0`。这类结构是旧筛选池和后验修补一直缺失的“难度本体”。
- 关键纠偏：运行时可消除不是抽象 graph 关系，也不是只看 head ray 首个 blocker；必须以 board-level `Propagate-Exits` 结果为准。任何所谓依赖必须在最终 trace 中表现为真实可达因果链。
- 当前限制：`DesignedHardLockV0` 覆盖率低、`dependencyFollowRunMax=4`，不能直接量产或挂正式包；它只是证明“真难结构可以成立”。
- 下一步：将物理锁模块变成 SGP source slot/constraint，再由 SGP 做高覆盖填充、外圈整理和多 family 变体；最终仍用 board-level trace gate 验收 `TrueHardCandidate`、低外出口、低 local patch、合理 choice wave。

## 2026-06-22 - Runtime Chain Head Is indices[0]

- 状态：已采用。
- 决策：所有 SGP Rhythm Lab 生成/修复/保留射线逻辑必须按运行时 trace 语义处理链条方向：`indices[0]` 是 head，`indices[1]` 是 neck，head forward = `indices[0] - indices[1]`。不得再把 path 末尾当 head。
- 原因：`Build-SGPRhythmTrace.ps1` 的 `Build-Board` 明确用 `$head = $indices[0]`、`$neck = $indices[1]`，`Propagate-Exits` 从 `headIndexByOwner` 开始。此前 `HardLockSlotSGPFillV0` 的 corridor reserve 误保留尾部射线，导致 filler 侵入真正头部 escape corridor，使 `0.20+` coverage 时因果/可解性崩塌。
- 落地：已修 `Build-HardLockSlotSGPFillV0.ps1` 与 `Build-HardLockSlotSourceOverlayV0.ps1` 的 corridor reserve；`HardLockSlotSGPFillHeadFixV0` 在 `TargetCoverage=0.20` 已跑出 7/12 个 `solved=True + TrueHardCandidate`，证明修正有效。
- 约束：后续任何 hard-lock slot、ray corridor mask、outer exit head 判断、chain flip/merge/split 和 overlay 删除冲突逻辑都必须显式检查 head/tail 语义，避免再次出现指标正常但运行时逃逸方向错位。

## 2026-06-22 - Trace-Delta Filler Compiler Over FirstHit Preservation

- 状态：已采用。
- 决策：补肉/加链时不再把既有核心链条的 `firstHit` 保持不变作为硬约束；`firstHit` 变化可以是合法的 relay/support 结构，只要新增链在 trace 中先被解决，且最终 hard-lock 因果压力没有坍塌。
- 原因：用户指出“中间插入一根可先消除的链条并不必然无解”，这是正确的。`firstHit` 是 ray-collision 的局部结果，不等价于难度、可解性或真实依赖是否保留；将其硬锁会误杀合法的中继/错峰补肉。
- 落地：采用 Trace-Delta Filler Compiler：先从 hard-lock 骨架生成 slot-aware filler 候选，再比较 board-level trace 前后变化。接收标准看 `solved`、`processTier`、choice delta、anti-local drop、support closure、local patch run、dependency follow run 和 outer exit，而不是单一 `firstHit`。
- 约束：slot/placement field 只用于收缩搜索空间和减少随机试错，不是最终验收；最终仍以冻结 LevelDefinition 的 board-level trace replay 为真值。若补肉导致 `maxChoices/avgChoices` 爆、anti-local 明显下降、support closure 降级或外圈新入口增加，即使 `firstHit` 看似合理也必须拒绝。
