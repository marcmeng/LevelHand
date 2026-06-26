# Decisions


## 2026-06-24 - Ray-First 0.46+ Fill Uses Old-Path Equivalence, Not Single-Chain Validity

- 状态：实验中
- 决策：在 0.46+ ray-saturated 区间，单链 C trace 失败不能直接视为 C 不可用；C 失败是 dependency/timing 分类信号。
- 证据：r02 12 个失败 C 中，12/12 都成为目标链新 firstHit，且 C 自己都有 firstHit 并非 free exit；11/12 为 direct dependency cut。失败 C 的 `C.firstHitOwner` 全部不在目标旧 `oldHit -> ancestors` 路径中。
- 决策：D repair 的第一版不应只“堵 C 出口”；若 C 已被已有 chain 控制但仍不可解，优先判断 C 是否改接到错误 causal basin，或是否压扁/绕过 oldHit 时序。
- 实现：`Build-RayFirstBlockerFillV1.ps1` 新增 opt-in `-RequireCandidateHitOldPath`，要求新增 C 自己的 firstHit 落在 target old dependency path；新增 `TargetOwnerFilter` 仅作 timing repair/诊断，不作为默认生产路径。
- 风险：old-path equivalence 当前只是必要条件，不是充分条件；初步 proof 恢复 solved/A 到 coverage `0.4706`，但 localPatch=4 且 hardStructure 降低，因此下一步要把 old-path 命中位置、localPatch 和 structure score 纳入排序/生成，而不是放宽 production gate。

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
## 2026-06-22 - Pressure Filler Over Raw Coverage Filler

- 状态：已采用为 SGP hard-lock 补肉主线。
- 决策：补肉阶段不再把 filler 只视为 coverage 增量；必须先区分 `pressure filler` 与普通 expansion/neutral filler。
- 定义：`relay/guard` 型 filler 会阻挡已有逃逸路径、增加依赖耦合或延迟释放，属于 pressure filler；`locked` 型 filler 主要是自己被挡住但不压制其他选择，容易增加 effective choice space。
- 生成规则：`avgChoices` 接近上沿时进入 pressure mode，只允许 pressure filler 组成候选组；普通状态下也要求候选组包含最低 pressure 比例。
- 验证规则：`avgChoices` 可在中间轮次形成“选择债务”，但最终 `final_selected` 必须还清债务，仍以 board-level trace 为最终真值。
- 依据：b02 大组一步补到 `coverage=0.2782` 会产生 `avgChoices=4.53/max=8`，但小步 relay/pressure 补链可保持 `coverage=0.2708`、`avgChoices=3.76/max=7`；五父本 pressure final5 冻结包 5/5 A 级、avg 最大 3.76、外出口头 0。

## 2026-06-22 - Choke Filler Requires Static Ray Hits Plus Dynamic Opener Guard

- 状态：已采用为 directed batch 补肉的下一层控制。
- 决策：`choke` filler 的最小工程定义为 `incomingRayHits >= 2`，即该候选链会被至少两条已有 escape ray 命中；但它不能单独作为真实难度证明，必须配合动态 gate：补链后 `openers` 不得增加，且最终仍通过 board-level trace。
- 原因：多 ray 命中是可在线计算的局部压缩信号，但只能说明“可能压住多个路径”，不能证明真实 effective choice space 下降。`openers` 不增长是当前最低成本动态保险，后续可扩展为 effective escape path reduction / EPRD。
- 落地：`Build-HardLockSlotDirectedBatchFillV1.ps1` 已新增 `ChokeRayHitThreshold`、`MinChokeFillerRatioWhenHigh`、`MaxDeltaOpeners`、候选/结果 CSV 中的 `incomingRayHits/chokeCount/chokeRatio/openers` 字段。
- 依据：Choke O1 mixed final5 冻结包 5/5 solved，平均 `avgChoices=3.146`、最大 `3.76`，`maxChoices<=7`，`antiLocal>=0.6`，`supportClosureBestDepth>=3`，`outerExitHeadCount=0`；b03 父本扩大候选仍无可解补肉，说明可补性本身需要作为父本筛选条件。

## 2026-06-22 - Extract Skeletons From Dynamically Excellent Seeds, Not Coverage

- 状态：已采用为 Choke O2 父本/骨架分析原则。
- 决策：优秀骨架应先从 board-level trace 表现优秀的 seed 中反推，再分析其链长、carrier、turn、ray/gate 结构；`coverage` 只能作为后续补肉/可扩张性参考，不能作为“骨架优秀”的主标准。
- 原因：内部 seed 全量结构画像显示 R1 修复能把 coverage 从约 `0.646` 拉到 `0.850` 且骨架指标基本保持；R2 二次补肉只把 coverage 从约 `0.850` 拉到 `0.860`，但 `avgChain/longRate/veryLong/carrier` 均下降，说明二次补肉可能稀释结构。另一方面，仅按静态骨架/coverage 选出的 top80 父本 trace 平均 `openers=6.75`、`avgChoices=4.15`、`maxChoices=8.39`、`antiLocal=0.261`、`localPatchRun=4.64`，动态上并不够硬。
- 落地：新增 `excellent_seed_skeleton_candidates_o1_20260622.csv`，按 trace 难度优先重排候选；`seed_Above300_level_664`、`seed_Arrowz_level_036`、`seed_Above300_level_891`、`seed_Arrowz_level_023` 等应优先作为“优秀 seed 骨架”拆解对象。
- 约束：后续父本筛选分两层：先找动态优秀 seed 的骨架语言，再单独评估该骨架是否有 Choke/Relay 补肉容量；不要把高覆盖、长链或二次修复形态直接等同于好父本。

## 2026-06-23 - Dynamic Outer Pressure Gate Over Static Outer-Head Zero

- 状态：已采用为 HardLock 0.30 量产线的临时生产 gate。
- 决策：0.30 高难候选不再用 `outerExitHeadCount==0` 作为唯一硬门槛；允许少量静态 outer-exit head，前提是它在 board-level trace 中没有形成动态白给入口：`outerExitAvailableChoiceMax<=1`、`outerExitSolveRunMax<=1`、`sameSideOuterExitSolveRunMax<=1`。
- 原因：对两个低 avg/max 的 0.30 候选做单外口头翻转后，静态 outer 清零且 avg/max 更低，但 `solved=False`；说明该 outer head 在当前几何里承担可达性/支撑作用，后验硬翻会破坏真实物理依赖。相反，冻结 trace 显示这些候选的动态外口可选和外口连续消都只有 1，不属于早期外圈白给。
- 落地：`SGPRhythmLab_HardLock030DynamicOuterGate5Pack` 已按动态外口压力筛出 5 关：5/5 solved/A-tier，coverage 约 `0.306+`，`avgChoices<=4.0`，`maxChoices<=6`，support closure depth=3。
- 约束：这是 0.30 量产线的临时 gate，不代表允许外圈传送带回流；若 `outerExitAvailableChoiceMax>1`、`outerExitSolveRunMax>1`、同侧外口连续消上升，仍必须拒绝。下一阶段应把动态外口压力前置到 parent capacity / directed fill，而不是靠冻结后补救。

## 2026-06-23 - Parent Capacity Should Include Near-Miss Repair Stage

- 状态：已采用为 0.30 量产线下一步。
- 决策：A 类父本不能只看“directed fill 一步到位是否 solved”；如果父本能稳定产生高覆盖、高结构、低 choice 的 `unsolved` near-miss，则应进入 bounded orientation repair stage，再由 board-level trace 和动态外口 gate 决定是否入池。
- 原因：`pc34to30p2` 从中间父本推到 `0.299~0.300` 时，大量候选 avg/max/anti/support/outer 都合格但 `solved=False`；直接翻新增 filler 组方向后，`pc34repair_dyn1` 在 51 个变体中救出 12 个 accepted，其中 top 为 `coverage=0.3002451`、`avg=3.58`、`max=6`、`anti=0.7`、`support=0.935/d3`。
- 约束：repair 只允许作用于新增 filler 组方向，不得手摆或改 core hard-lock；最终仍必须通过 board trace，且动态外口压力必须低。若 repair 仍全部失败，则该父本判为 capacity exhausted，不继续硬塞。
- 量产含义：父本筛选应输出两类有用结果：direct accepted A parent，以及 repairable near-miss parent。明早量产线应把 `directed fill rejected_steps` 中的 high-structure near-miss 自动送入 `Repair-NearMissFillerOrientationV1.ps1`，再合并 accepted pool 做去重选包。

## 2026-06-23 - Parent Pool Expansion Before Coverage Expansion

- 状态：已采用为明早量产线顺序。
- 决策：0.30 之后不先硬推单个父本到更高 coverage；先扩大 A 类父本池，再在每个 A 父本的承压容量内做 coverage expansion 和 family/source 去重。
- 原因：同一批 `cov265` hard-lock 父本经相同 probe 后表现分化明显：第 5 父本可自动跑成 A 类并修出 0.299/0.30 accepted；第 1 父本只能到 `0.2782`，第 2/3/4 父本为不可承压或补肉耗尽。新增 `base_09` 原始 headfix 骨架又可从 `0.2059` 跑到 0.299 accepted，证明“父本是否可承压”是独立变量，不能靠单父本调参代替。
- 工程定义：A 类父本 = 在 bounded directed fill + bounded near-miss orientation repair 后，仍通过 board-level trace、choice pressure、support closure、local/follow run 和动态外口 gate 的结构底盘。B/E 类不是坏关卡，而是当前补肉策略下的容量边界。
- 约束：`FillCapacityScore`/A-B-E 分类只用于父本筛选，不替代最终 trace。任何入包关卡仍必须冻结 LevelDefinition 后重放 board trace；动态外口 pressure 和 support closure 仍是 gate。
- 下一步：继续从 `hard_lock_slot_sgp_fill_headfix_v0_truehard_selected.csv`、动态优秀 seed 骨架候选和更多 SGP hard-lock body 中挖 A 父本；避免在 B/E 父本上反复提高 coverage 或放宽 difficulty gate。

## Dual Difficulty Family Gate - 2026-06-23

- 决策：高难量产线不再用单一 `supportClosureDepth>=3` 评价所有 family。
- 原因：hard-lock causal family 属于“因果链深度难”，semantic skeleton family 属于“选择空间压缩难”；两者都可在 board-level trace 中表现为低选择、低 local run、动态外口受控，但 support closure 语义不同。
- Causal-depth gate：`supportClosureDepth>=3`、`antiLocal>=0.6`、`avgChoices<=4`、`maxChoices<=7`、动态外口压力<=1。
- Semantic-compression gate：`hardStructureV3Class!=LocalEasy`、`avgChoices<=3.5`、`maxChoices<=7`、`causalAntiLocalityScore>=0.6`、`localPatchSolveRunMax<=2`、`dependencyFollowRunMax<=4`、动态外口压力<=1；`supportClosureDepth` 只作诊断。
- GPT 顾问与本地数据一致建议采用混合策略：hard-lock family 做因果深度主线，semantic skeleton family 做多样性和选择压缩 family；最终仍以 board-level trace 为唯一真值。

## Decision - Causal Skeleton Signature Dimensions - 2026-06-23

- 正式采用“认知结构去重”而不是仅靠文件、geometryHash 或 occupancy Jaccard 去重。
- `causalSkeletonSignature` 应由四类维度组成：
  1. causal topology：upstream/support/hub/target、depth、fanout、branching。
  2. spatial flow：region flow、跨区路径、主锁点空间分布。
  3. solve rhythm：choice wave、unlock timing、hub/support 打开顺序。
  4. dependency interaction density：多个骨架/锁点之间是否互相影响，避免多个局部 hardlock 互不耦合导致“看似难但 LocalEasy”。
- 后续 production pack 需要限制每个 causalSkeletonSignature/equivalence class 的出货数量；下一阶段目标从“修关卡”转为“生成不同 causal skeleton species”。

## Causal Skeleton Signature Gate - 2026-06-23

- 决策：geometryHash、occupancy Jaccard、parentGroup 只能解决低维重复，不能定义真正的“骨架”。生产去重需要引入 causal skeleton signature。
- 骨架分层：
  - coreSignature：细粒度诊断，包含因果拓扑、空间落点、节奏 wave、耦合密度细节。
  - skeletonFamily/speciesSignature：包内子家族去重，保留 support depth、fanout、cross lock、选择压力、follow/local 类型。
  - macroSkeletonSignature：解题流程宏型，弱化 D3/D4 和若干局部节奏差异。
  - ootSkeletonSignature：最高层结构盆地，用于判断是否只是同一大骨架的变体。
- 当前 causal-depth hardlock pool 的 root skeleton count=1；因此短期多样性瓶颈不是选择器，而是缺少新的 root skeleton source/generator。
- 生产 gate 原则：每个 review/production pack 至少先按 skeleton family cap，若目标是多物种包，则必须按 root skeleton cap；但 root cap 只有在有多个 root source 时才有意义。

## Causal Skeleton Species Preservation - 2026-06-23

- 决策：难度通过但 final root skeleton 回到旧 root `db4cc18089` 的候选，不能算作新骨架 family；它只能作为旧 hardlock family 的补充样本。
- `Build-CausalSkeletonSpeciesSamplerV1.ps1` 的 12 root probe 证明“不同 root skeleton 可生成”，但现有 directed fill 会把可用样本重新吸回旧 causal-depth hardlock root。
- 后续多样性产线必须加入 species-preserving / root-collapse gate：source species、final species 与 denied/known roots 都要被显式记录和筛选。

## Species-Preserving Gate Boundary - 2026-06-23

- 决策：多物种产线必须拆成两道 gate：`identity gate` 防止 final root 回旧 canonical root，`species-quality gate` 防止 final root 虽然不同但退化为 `depLocal/edgeSparse/braidWeak`。
- 单纯 `finalRoot != db4cc18089` 不足以进入 production；production 新 family 至少要保留跨区/耦合/交错结构信号，例如 `depCross` 或 `edgeCoupled`，且不能是 weak/local/sparse root。
- 当前旧 causal-depth hardlock root 仍可作为高难 backbone；新 species 线必须以 source species 为父本，但 fill selection 要以 root identity + root quality 双约束推进。

## Species-Aware Fill Compiler Boundary - 2026-06-23

- 决策：新 species 产线的 fill 结果必须通过 identity + quality 双 gate；仅 board trace 可解或 avg/max 良好不足以进入下一轮。
- `sourceRootNotPreserved + weakFinalRootSkeleton` 是当前新 species fill 的主要失败模式，说明 fill candidate selection 仍是 species-blind projection operator。
- 后续修改 fill 时，评分目标应前移到 root quality：奖励保留 source root、`depCross`、`edgeCoupled`、`braidMed+`，惩罚 `depLocal`、`edgeSparse`、`braidWeak`，再交给 board trace 复验。

## Strong-Root Manifold Preservation - 2026-06-23

- 决策：species production gate 不再硬要求 `finalRoot == sourceRoot`；exact source-root preservation 只作为诊断/高保真模式。
- 正式方向采用 `strong-root manifold preserve`：允许 fill 将 root 从 A 迁移到 B，但 B 必须处于强结构流形，且禁止旧 canonical root、weak/local/sparse root。
- 最小强 root 条件（当前 V1）：final root signature 不含 `depLocal|edgeSparse|braidWeak`，且包含 `depCross|depMixed`、`traceCross`、`regionWide`、`edgeCoupled|edgeNetwork`、`braidMed|braidStrong`；最终可解和选择/局部顺消仍由 board trace gate 决定。
- 选择器 root/source cap 必须在排序后执行；排序前 cap 会让低分先到候选抢占 root 名额，造成 production 误选。
- 风险：strong-root manifold 不能放太松，否则会变成随机 root drift；下一步需要按 root/macro/skeleton family cap 做并行抽样，而不是沿单个体连续漂移。

## Root Skeleton Identity Split - 2026-06-23

- 决策：`rootSkeletonSignature/rootSkeletonHash` 不再包含 `choiceControlled/choiceEdge/choiceLoose` 等节奏字段；root 只表示因果拓扑、空间流和结构耦合。
- 节奏差异改由 `rhythmVariantSignature/rhythmVariantHash` 记录；同一 root 可以有多个 rhythm variant，但不能因此被当成不同结构物种。
- 原因：`SpeciesManifoldRootPairReview2` 两关 35 根链中 34 根完全一致、occupancy Jaccard=0.9506，仅最后一根链不同；旧签名因 `choiceControlled` vs `choiceEdge` 误判成两个 root。
- 生产选择器新增 `MaxOccupancyJaccard` 终选去重，默认 0.90；即使签名未覆盖，身体高度重叠的候选也不能同时进入 review/production pack。

## Chain Language Role - 2026-06-23

- `chainLanguageFamily`（例如 low-turn、bent、snake、长短链比例）不再参与 `rootSkeleton` 身份判定；它是风格/表现层和生产配比层。
- `rootSkeleton` 只表达因果拓扑、空间流和 dependency interaction；`rhythmVariant` 只表达 choice/timing；`chainLanguageFamily` 用于避免同一结构全是少弯折/同链条语言的视觉重复。
- 生产选择应同时 cap rootSkeleton、rhythmVariant/occupancy near-duplicate 和 chainLanguageFamily，但不能把链条语言单独当成新结构物种。

## Root Skeleton Requires Archetype Identity - 2026-06-23

- 仅靠 causal topology aggregate、spatial bucket、interaction bucket 不能代表玩家认知上的 root；这些字段会把同一 source archetype 的少量 filler/链条变化误分成多个 root。
- 生产级 root diversity gate 必须增加 `sourceArchetype/templateFamily` 或等价的 role-layout signature；同一 archetype 默认最多 1 个 review/production entry，除非后续证明其 macro role layout 和 anchor topology 有大幅变化。
- `rootSkeletonHash` 可作为结构指标，不再单独作为“物种不同”的充分条件；真正不同物种至少需要 archetype/template、角色布局、占用/链条编辑距离和 trace 因果形态同时分离。

## Tri-Branch Generation Boundary - 2026-06-23

- 决策：`tri_branch` 是 dependency topology template，不是随机形状或普通 filler family；正确方向是 source/generator 前置可解结构，而不是依赖 directed fill 或 repair 后修。
- 最小 tri_branch V1 应表达 `Root -> A/B/C -> Hub`，三分支跨区/不同 escape direction，且至少 2 个 branch 在 hub 前发生互锁或汇聚控制；SGP 只能填几何，不能随机决定结构身份。
- `-TriBranchSolvableBridge` 只是 smoke prior，用来证明 source 前置可解性方向；若要量产，后续应实现 graph-first tri_branch generator，而不是继续扩大 post-hoc repair。

## Archetype-Aware Diversity Gate - 2026-06-23

- 决策：`rootSkeletonHash` 是结构指标但不是“物种不同”的充分条件；同一 rootSkeleton 聚合桶内可能包含不同 source archetype，也可能把同 source 的局部 fill 误分。
- production/review 多样性 gate 必须组合使用：`sourceArchetype/templateFamily`、`rootArchetypeSignature`、occupancy/edit distance、board trace hard metrics 和人工视觉反馈。
- 同一 source archetype 默认最多 1 个 review entry；跨 archetype demo/production 包优先采样不同 source template（例如 canonical/web_four vs tri_branch），再用 trace 验证难度。

## Strict Root Boundary - 2026-06-23

- 决策：`sourceArchetypeFamily`、`rootArchetypeSignature`、occupancy Jaccard 都不能单独作为 strict root 判定；它们只是候选多样性信号。
- 严格 root 必须要求全局 role-layout/macro path 变化：关键 hub/support/upstream/target 群的区域关系、解题宏流向、主要结构块之间的拓扑关系发生变化。
- `SpeciesCrossArchetypeReview1` 被人工判退：虽然 source archetype 一个是 canonical/unknown、一个是 tri_branch，但实际是同一全局骨架中的局部模块上下换位。
- 后续 review/production 包必须新增 `strictRootSignature` 或等价人工/脚本 gate；当前包作为负样本回归使用。

## Strict Root Gate V1 - 2026-06-23

- 决策：production/review 多样性 gate 增加 `strictRootSignature/Hash`，用于合并“同全局骨架 + 局部模块换位”的假多样性。
- `strictRoot` 当前定义为 `rootSkeletonSignature + coarse 2x2 global presence layout`；它不包含 `sourceArchetypeFamily`、rhythm、chainLanguage，也不使用 2x2 centroid 密度，因为这些会把局部上下换位误判成新 root。
- `MaxPerStrictRoot` 是 review/production 包的默认推荐 cap；`sourceArchetype` 和 `rootArchetype` 只作为辅助诊断，不能绕过 strict root cap。
- 真正不同 root 的生成方向必须前置改变 graph-first role layout（例如 tri_branch 的 Root->A/B/C->Hub 分区关系），而不是在同一布局盆地内继续 fill/repair。

## Root Diversity Model Boundary - 2026-06-23

- 决策：root diversity 不是 hash 问题，而是 `role-layout + dependency-topology + causal-trace-pattern` 的结构族问题。
- `strictRootSignature` 继续作为 safety gate，负责挡掉同骨架局部变体；但后续 generator/production 不能依赖 strictRootHash 发现新物种。
- 下一阶段核心模块为 `RootTopologyExtractor V1` 和 graph-first family generator；tri_branch/dual/web 等必须先表达因果拓扑模板，再由 SGP/fill 实现几何。

## Root Topology Gate and Mixed Motif Boundary - 2026-06-23

- 决策：strict root 去重后，production/review 多样性必须进一步按 `rootTopologyHash` 限流；`rootTopologyHash` 来自 role-layout + dependency-topology + causal-trace-pattern，而不是 source 名、occupancy 或链条语言。
- 决策：`supportDepth=3` 与 `supportDepth=4` 属于同一高深度 topology band（`depth3p`），不能单独构成新 root；否则会放过“只差一根链条/延迟层”的伪多样性。
- 决策：hardlock family 可以使用 `supportClosureDepth>=3/supportScore` 作为硬 gate；但 mixed motif family（dependency_skeleton/room_door）不能用 hardlock 单尺，否则会误杀低选择、高 antiLocal、trace 可解的不同结构。Mixed-root review gate 以 board trace、choice、antiLocal、local/follow run 和 `MaxPerRootTopology` 为主，support closure 作为 family-specific signal。
- 决策：纯 geometry topology template 不足以生成新 root；每个 template 必须内置 trace-visible causal motif（upstream/support/hub/target 或其它已验证 motif），SGP/fill 只负责补全和微调。

## False-Hard Root Topology Boundary - 2026-06-23

- 决策：`rootTopologyHash` 不同不是高难充分条件；它只用于多样性/身份分类。进入高难 review/production 还必须满足 validated difficulty mechanism。
- 当前 validated difficulty mechanism：trace-visible support closure / hardlock motif（V1 gate：`supportClosureBestDepth>=3` 且 `supportClosureBestScore>=0.70`，再叠加 solved、choice、antiLocal、local/follow run）。
- `dependency_skeleton` 与 `room_door` 这类 `supportDepth=0/1`、immediate trace 的结构目前不能作为高难 root；它们可作为 motif template 原料，但必须嵌入 causal hardlock 后再验证。
- mixed-root gate 仅可用于诊断“结构身份是否不同”，不能替代 high-difficulty gate。

## RootTopology Production Quality Gate - 2026-06-23

- 决策：任何用于“高难 review/production”的 rootTopology 多样性选择，都必须开启或等价满足 production-quality gate；`solvedDiagnostic` 只能进入诊断报告，不能进入玩家试玩包。
- `rootTopologyNotProductionQuality` 是防 false-hard 的正式 reject reason；它解决了 dependency_skeleton/room_door 低选择但体感简单的问题。

## Causal Motif Over RootTopology Difficulty - 2026-06-23

- 决策：`rootTopology` 不是难度来源，只能作为容器/身份/多样性维度；高难 production 必须由 validated causal motif 提供难度机制。
- 当前唯一已验证的高难 motif gate：board trace 中 `supportClosureBestDepth>=3`、`supportClosureBestScore>=0.70`，再叠加 solved、choice pressure、anti-local、local/follow run 和动态外口 pressure。
- `dependency_skeleton`、`room_door`、以及其它 supportDepth 0/1 的 topology 即使低选择/高 antiLocal，也只能作为 motif template 原料或诊断，不得直接进入高难 review。
- `Causal Motif Embedding Compiler V1` 的职责是把 support-lock motif 嵌入不同 SGP/template 容器；fill 是 constrained augmentation，不是把 topology 自由优化成另一个 attractor。
- 当前实测边界：tri_convergent 可稳定完成 source->fill->trace；web_four 可出现合格 source，但旧 hardlock directed fill 对 web 容器仍 species-blind，容易无候选或不过 gate。下一步应做 web/dual species-aware filler，而不是降低 support motif gate。

## Causal Backbone Defines Strict Root - 2026-06-23

- 用户反馈与 GPT 顾问一致：当前 `CausalMotifEmbeddingV1ReviewPool1` 虽然机器分出多个 rootSkeleton/strictRoot/rootTopology，但玩家认知上应视为 `1 causal root backbone + 多个 motif/rhythm realization`。
- 决策：严格 root 多样性不能再由 geometryHash、strictRootHash、rootTopologyHash、rhythm、chainLanguage 或 fill 变体判定；这些只能作为 realization/variant 去重信号。
- 新定义：strict root 必须由 `causal backbone graph` 区分，即 support-lock 的 upstream/support/hub/target 角色关系、依赖分叉/汇聚方式、关键锁点分布和解题宏路径不同，才算不同 root。
- selector 只能 prune/去重，不能创造 root；真正多 root 必须由 graph-first root template / causal backbone generator 产生，再嵌入 support-lock motif 并经 board trace 验证。

## Root Metric Calibration Result - 2026-06-23
Causal root identity must be validated against trace-visible causal backbone, not layout/rootTopology. Synthetic control proves `causalBackboneSignature` can split true backbone archetypes and merge old hash/rootSkeleton variants. Real template calibration shows current layout templates are not enough: only tri-convergent forms a hard causal backbone; other templates are layout diversity without hard causal motif. Production root expansion therefore requires new graph-first causal backbone templates, not more hash/filter tuning.

## Decision - Dual-Gate Hard-Lite Root Boundary - 2026-06-23

- `causalBackboneSignature` 作为严格 root 判断优先级高于 `rootSkeletonHash/strictRootHash/rootTopologyHash`；后三者可诊断几何/布局/节奏，但不能证明玩家认知上的新因果 root。
- 第二 root 方向先固定为 `dual_gate_hard_lite`：source primitive 必须 trace-visible `supportDepth>=3`、`supportClosureBestBranchMax<=2`、`causalFanoutMax<=2`，并保持 `dual_gate_hubfield`；任何变成 `branch3/fanout3/closureGraph` 的样本都视为投影回 tri root。
- `dual_gate_hard_lite` 的 hard reject：unsolved、supportDepth<3、supportBranchMax>2、causalFanoutMax>2、outerExitHead>0、localPatchRun>3、dependencyFollowRun>4、或 causalBackboneHash 回到 `6fc63698fd`。
- `dual_gate_hard_lite` 的 soft target：coverage 逐步从 `0.19 -> 0.23 -> 0.25+`，avgChoices `2.5-3.5`，maxChoices `<=6/7`，antiLocal `>=0.6`，保留 branch2/fanout2。
- 直接 SGP top-up 到 0.245 会把部分样本推向 fanout3/tri 或不可解；后续必须走 backbone-preserving directed fill，而不是让 topology generator 自己补满。

## Dual Gate Spatial Review Boundary - 2026-06-23

- `dual_gate_spatial` 被定义为视觉 root skeleton review template，而不是 production hard template。
- 进入 production 的条件仍不变：必须 trace-visible causal motif 过线（例如 supportClosureDepth >= 3/supportScore 足够）并通过 board trace；单纯空间/布局差异不能算高难成果。

## True Dual Gate Definition - 2026-06-23

- `dual_gate` 不能定义为两个空间岛或左右翻转；必须是两个空间分离的控制系统共同控制同一核心区。
- 最小 trace 形态：Gate A 与 Gate B 分别在不同区域形成串行控制链，并分别解锁同一 central core 的不同入口；缺少任一路时 core 不完整。
- 旧 supportClosure 指标不再作为 dual_gate 的充分判断；它仍用于 support-lock/hardlock motif。dual_gate 需要独立的 `dualGateJointCore` 诊断/gate。

## Dual Gate Shared-Lock Gate - 2026-06-23

- True dual gate 的最小定义更新为 shared-lock：两个空间分离 gate 系统必须共同解除同一个 core chain/出口的锁，不能只是分别解锁核心附近两根不同链。
- 当前 validated prototype 是 `dual_gate_joint_core_root_v1_review5`；它证明 shared-lock motif 可解且结构语义成立。后续 production 需要在保留 `GateA->LockA + GateB->LockB -> same Core` 的前提下压低开局选择并补覆盖。

## Strict Dual Gate Root Definition - 2026-06-23

- `dual_gate` root 不等于“两条分支/两个区域/两个 blocker 看起来汇合”；必须验证两个因果独立 gate 系统共同控制同一个 core。
- 判定层分为 raw 与 root-level：raw 只说明 core escape ray 存在两个必要 blocker；root-level 还必须排除 tri-convergent branch3/fanout3p 主骨架支配，否则属于旧 root 内的双 blocker 变体。
- strict dual gate 的核心 counterfactual：A-only 不开 core、B-only 不开 core、A+B 开 core；A/B ancestry 不重叠，不互相解锁，空间上不在同一区域。
- `strictDualGateCandidate` 以后可作为 dual_gate root/fill gate；`strictDualGateRawCandidate` 只作为诊断信号，不能作为新 root 生产验收。
# Validated Root Fill Strategy - 2026-06-23

- `0.30` coverage is treated as a parent/skeleton watershed, not as the final level-completeness target.
- When the validated support-lock root reaches the directed slot-filler ceiling (`~0.305`), continue with motif-preserving background SGP fill instead of forcing more bridge/noise/edge pressure candidates.
- Protection must be capacity-aware: protecting no critical rays makes high-coverage candidates unsolved; protecting too many rays prevents candidate generation. Current empirical sweet spot for the validated root proof is `ProtectedChainCount=14`, which reaches `coverage≈0.398` while preserving `supportClosureDepth=3`.
- Full acceptance still belongs to board-level trace. Background fill can generate neutral/cosmetic meat, but every growth step must pass solved/tier/support-lock/choice/local/outer-pressure gates before it becomes the next parent.

## Strict Dual Gate Temporal Non-Interference - 2026-06-23

- GPT second opinion 确认：strict dual_gate 不能只靠空间分离、A/B counterfactual 和 ancestry 不重叠，还需要 trace timeline 上的 temporal non-interference。
- 新增约束：清 A-side ancestry 不得让 B gate 可用；清 B-side ancestry 不得让 A gate 可用。该约束用于防止 fill 后的 hidden shared causality / dependency merge。
- `triConvergentBackboneDominates` 暂时保留为保守 V1 hard reject；它不是最终理论定义，只是为了防止当前已知 tri_convergent 负样本通过 strict dual root gate。

## Strict Dual Gate Fill Strategy - 2026-06-23

- strict dual-gate 的补肉不能沿用 support-lock 的 `avgChoices high -> pressure-only filler` 规则；其困难来源是两个因果独立 gate 共同控制同一 core，而不是 supportClosure/pressure filler 本身。
- 在 `RequireStrictDualGate` 模式下，directed fill 可以接受 neutral/locked filler，只要 board trace 仍满足 strict dual identity、solved/tier、choice ceiling、anti-local、local/follow run 和动态外口 pressure gate。
- `supportClosureBestDepth/Score` 在 strict dual 模式下仍可作为诊断或额外收益，但不能作为硬 gate；严格 dual 身份由 `strictDualGateCandidate=True` 与 A-only/B-only/A+B/temporal non-interference 负责。

## Strict Dual Gate Variant Boundary - 2026-06-23

- strict dual shared-core 的 base/vertical/right/down 空间实现都算同一 root family 的 realization/variant；只有 gate role topology 或 core-control causal relation 改变，才可能算新 root。
- 当前 `DualGateJointCoreVariantV2FillReview` 是 strict dual motif 变体审查包，不作为多 root diversity 的证明。

## Strict Dual Gate Accepted As Second Root Family - 2026-06-23

- 用户肉眼确认 `DualGateJointCoreVariantV2FillReview` 与旧 support-lock/root family 有明显差异，且 4 个 strict dual shared-core 变体不算太同质化。
- 决策：`strict_dual_gate_shared_core` 从 root proof/review 升级为已认可的第二 root family，可进入 production-style expansion。
- 后续 strict dual 产线仍必须保持 `strictDualGateCandidate=True` / raw true / temporal non-interference / solved trace gate；它是 root identity gate，不得被 supportClosureDepth 替代。
- 下一阶段重点不是继续证明 dual root，而是提高 coverage、生成 8-12 关 review pack，并观察肉眼重复度与难度稳定性。

## Strict Dual Gate Density Expansion Boundary - 2026-06-23

- For `strict_dual_gate_shared_core`, initial production-style expansion target is `coverage 0.16-0.21`; this is the identity-preserving density zone confirmed by GPT review and local T018 trace.
- Do not push directly to `0.25+` until T018/T021 review proves identity, feel, and diversity hold; high coverage risks tri/support-lock attractor leakage.
- Strict dual expansion hard gates: `solved=True`, `processTier=A`, `strictDualGateCandidate=True`, raw true, A-only false, B-only false, A+B opens core, temporal non-interference false/false, localPatch <= 3, dependencyFollow <= 4, maxChoices <= 8, dynamic outer pressure clean.
- Coverage/antiLocal/avgChoices are control signals inside this zone; root identity is owned by strict dual trace fields, not supportClosureDepth.

## Ray-Field Fill Debt Model Correction - 2026-06-24

- Local proof at `coverage≈0.488-0.502` corrected the earlier C/D debt model: `FreeHeadDebt` is not automatically a debt. A free-head C can be accepted if it blocks an existing ray and board trace keeps solved/support/choice/local/follow stable.
- `WrongBasinDebt` remains a true structural risk: current D repair that blocks `C.escapeRay` and hits the target oldPath does not reliably restore solved state. The failure is target causal re-binding, not merely C escape leakage.
- Decision: move validated-root high coverage fill from repair-driven to basin-aware generation. Prefer clean blocker C or clean C-pairs; allow free-head C as neutral/positive when trace-stable; reject wrong-basin candidates unless a future target-oldPath repair proves otherwise.
- At `0.50+`, target/owner history becomes a production control signal: bad basins that repeatedly produce unsolved, anti-local collapse, support collapse, or local/follow spikes should enter cooldown/deny before candidate generation.

## Boundary Cascade Guard Boundary - 2026-06-24

- `OuterPressureDebt` is not equivalent to a generic C+D repair opportunity. The target64/owner72 proof at `coverage≈0.506` showed D can block the C ray at boundary cell 408, but the successful D shape becomes a new boundary direct-exit owner with a long perimeter ray and `dependencyFollowRunMax=7`; D shapes that make D itself blocked by owner12 collapse support closure and become unsolved.
- Decision: treat this as a `boundary cascade` class, not as production-ready outer guard repair. A C that generates boundary cascade may be kept as an exploratory state, but not promoted to the production parent pool unless the cascade depth/path and follow-run remain bounded.
- Static `outerExitHeadCount` remains a risk signal, not the only truth. Dynamic outer pressure can be low while boundary propagation/follow-run is already unhealthy; production acceptance must continue to include `dependencyFollowRunMax` and should add boundary cascade diagnostics before relaxing static outer head.
- Next high-coverage validated-root fill should prefer non-boundary internal C candidates and use target/owner cooldown for boundary cascade owners. Perimeter shell guard is deferred; it risks expanding the outer dependency layer and causing cycles/deadlocks.


## Strict Dual Gate 0.30 Root Proof Cleared - 2026-06-24

- `strict_dual_gate_shared_core` is now considered cleared for the root-family expansion target at roughly `0.30` coverage, not just low-density review.
- Frozen proof pack `SGPRhythmLab_DualGateJointCoreVariantV2FillProof2T030Pack` has 2/2 board-trace pass: `solved=True`, `processTier=A`, `strictDualGateCandidate=True`, raw true, A-only false, B-only false, A+B true, and temporal non-interference false/false.
- Source selected coverage values are `0.3015` and `0.3039`; frozen trace does not currently emit coverage for copied assets, so coverage for this proof is recorded from the selected CSV.
- Next root-family expansion should prioritize a third causal root candidate, currently `web_crossover`, while keeping the high-coverage/fullness work on a separate lane.

## Web Crossover Root V1 Definition - 2026-06-24

- `web_crossover` is accepted as a root candidate only when defined by trace-visible independent causal closure hubs, not geometry, fanout, or visual dispersion alone.
- V1 hard gate: solved A/S, `independentCausalHubCount>=2`, `qualifiedCausalHubCount>=2`, low closure overlap, cross-region dependency/trace movement, anti-locality floor, local/follow run bounds, and not `strictDualGateCandidate`.
- Failure labels for rejected candidates include `split_dual_masquerade`, `fake_web_tri_collapse`, `decorative_web`, `layout_web_only`, `single_causal_basin`, and `weak_web_candidate`.
- `web_crossover` proof pack passed this V1 gate at `0.288-0.2978` coverage; it is now the third root-family proof after support-lock/tri and strict dual shared-core.

## Ray-Constrained Cavity Fill Boundary - 2026-06-24

- For validated-root high-coverage expansion, `ray-first blocker` is demoted from primary generator to constraint/probe layer. It can explain physical dependency effects, but main fill candidates should be SGP-style cavity fills constrained by the local ray-interference field.
- `center-out cavity fill` is not sufficient by itself: generated chains must be conditioned on local ray roles and then verified by board trace/path-aware probe. Free/direct head remains a risk signal, not a hard reject.
- Empirical boundary at the 0.512 parent: single-chain cavity candidates can be trace-safe (`SoftDebtNoRetarget`), but simultaneous two-chain/three-chain blind batches collapse solved state. Therefore high-coverage meat must be staged as trace-settled micro-fill or generated as a true debt-closing pair after the first chain's post-map is known.
- Partial sequential solving is a soft debt, not a hard reject. `dependencyFollowRun`/`localPatchRun` should rank candidates (`SoftLinearizedDebt`) unless they become extreme conveyor collapse or coincide with motif/support damage.
- Production direction: iterate `single safe/soft-debt fill -> recompute RayConstraintMap -> trace/path-aware gate`, or build a pair-fill operator that chooses chain B from A's post-map debt. Do not batch multiple center-cavity chains without intermediate causal settlement.

## Full-Coverage Baseline Must Use Seeded Direct-SGP + Micro-Fill - 2026-06-24

- User correction accepted: `Build-ValidatedRootBackgroundSGPFillV1` is a later background filler, not the original SGP. Its `0.74-0.75` ceiling must not be used as evidence that SGP cannot reach full coverage.
- Decision: before adding difficulty constraints, validate geometry fullness with seeded Direct-SGP rules plus the project-style micro-fill/heal layer. Direct-SGP provides layer-head/inward-growth/family scoring; micro-fill closes residual fragmented cavities.
- Empirical baseline corrected after the `paths:` vs `authoredLevel.arrows:` writer bug fix: seeded Direct-SGP + micro-fill can geometrically push the `0.3076` validated support-lock parent to real coverage `0.9436-0.9510`, but 4/4 board trace as `solved=False/processTier=Drop` with huge choice pressure. The old `seeded_direct_sgp_micro_from030_to095_sweep_smoke4` evidence is invalid because it only changed coverage metadata.
- Constraint order after this baseline: preserve root/motif/critical owners and board solvability first, then dynamic outer pressure and conveyor collapse; average/max choices are whole-run curve controls, not per-step hard gates.

## Decision - 2026-06-24 - Small canvas production should predefine the outer frame before SGP fill

- User correction accepted: for the validated support-lock production lane, do not place a few local cages in a 24x34 board. Shrink the whole board while preserving the parent causal motif, then let seeded Direct-SGP fill inside that smaller world.
- Raw small-canvas fill still drills outward. A few controlled exit teeth are not enough; pre-seeding short perimeter frame segments before SGP fill converts the boundary from uncontrolled outer exits into a bounded dynamic outer-pressure surface.
- Static `outerExitHeadCount` can remain high in frame-based boards because frame chains themselves count as heads. Use dynamic outer pressure (`outerExitAvailableChoiceMax`, `outerExitSolveRunMax`) plus solved/support/local/follow gates for judgement.
- Current best tool path: `Build-SeededDirectSGPFillBaselineV1.ps1` with `CanvasWidth/CanvasHeight`, `MaxTotalChains`, `PreseedOuterFrame`, `OuterFrameSegmentLength`, `OuterFrameMaxChains`, and `MinHeadLayer`. This is the active experiment for "outer first, SGP fill second".

## Decision - 2026-06-24 - Near-Full Coverage Is the Real Production Gate

- `0.60` coverage is demoted to a diagnostic waypoint. It can prove a local or small-canvas fill idea has some room to grow, but it does not validate a finished-level production route.
- Production feasibility now requires testing near-full coverage: use `0.85+` as the minimum full-board feasibility line and `0.95+` as the near-product target.
- Outer-frame small-canvas fill is useful as a proof that a support-lock motif can survive extra density, but it should not be treated as the main path to finished levels because it can create a non-expandable constrained sandbox.
- Average and max choices should not be hard-gated at every step for near-full boards. Near-full levels can have staged choice waves; hard gates should be board solved, trace-visible motif/root persistence, no dependency collapse, and no dominant linear/conveyor solve. Choice metrics become distribution/curve signals.
- The next generator direction is `flow-pressure constrained SGP`: candidate heads/bodies must be selected from the current escape-flow/ray-pressure field so high density is achieved by stabilizing escape flow, not by SGP drilling many independent outer exits.

## Decision - 2026-06-24 - Head-Hit Flow Pressure Alone Is Not Enough

- Minimal flow pressure (`RequireHeadHitsExisting`) is useful as a diagnostic but not a near-full solution. In batch mode it can lower choices while making the board unsolved and deleting support closure.
- `RequireHeadHitsExisting + microfill` can reach `0.90` geometry with low avg/max choices, proving the failure is not just “too many choices”; the failure is motif disconnection / wrong causal basin.
- Trace-settled micro-growth can preserve the support-lock motif to about `0.42` coverage on the 18x24 compact root, but current SGP-style candidates fail around `0.44` by reducing supportDepth to `1` and becoming unsolved.
- Therefore the next layer must not merely filter heads. It must allocate fill by causal lane/flow component: before adding dense filler, determine which escape-flow lanes are allowed to be closed, which must remain as motif carriers, and which need new relay/guard blockers to preserve reachability.

## Decision - 2026-06-24 - Exit role is terminal-capable, not reserved-empty

- In local role-grid fill, `E` cells should not be modeled as mandatory empty holes. A/B testing on identical 8x10 rooms showed terminal-capable `E` raises local fill coverage without reducing solved rate.
- `E` is still not an ordinary safe-fill cell. It should mean "terminal/head-capable corridor" and must be selected by a path-cover or flow-planning layer so it becomes a controlled endpoint/outlet, not a free branch that weakens the support motif.
- Production role semantics now stand as: `K` fixed parent, `M` must-block/pressure anchor, `B` body-only corridor, `H` head-allowed interior, `E` terminal-capable endpoint corridor, `S` safe fill.

## Decision - 2026-06-24 - High local coverage requires frontier-facing heads

- Ring-only head enumeration is too weak for full local fill; it stalls around `0.7x` coverage because the remaining holes are not aligned with rectangular rings.
- Unconstrained role-any head enumeration proves the role grid can reach `0.9+` coverage, but collapses solvability because internal heads can point into future/unknown space and create cycles or dead flow.
- The current production direction for dense local filling is frontier-facing: candidate heads may come from role-valid interior cells, but their escape direction must face the existing occupied/boundary frontier, while their body grows inward into the hole. This is the first mode that reached `0.9+` coverage with non-trivial solved rate in local probes.
- Next gate must add trace/topological ordering or parent-motif preservation on top of frontier fill; coverage alone is no longer the blocker.

## Decision - Hub-Spoke Density Boundary - 2026-06-24

- `hub_spoke` is accepted as a distinct causal root family when it satisfies: one central clearable hub, multiple spatially separated independent spokes unlocked by that hub, and no hidden convergent-core/web/tri collapse.
- Current production proof freezes at `~0.288` coverage. Generic fill can create solved/A `0.30+` candidates, but observed candidates fail hub-spoke identity and are treated as root drift, not valid hub-spoke production.
- Near-term policy: root discovery lane should freeze identity-stable root proofs before pushing density. Coverage expansion for each root requires a root-specific identity-preserving fill objective after the root definition is accepted.

## Decision - 2026-06-24 09:00 - Cascade Relay is a bounded propagation root

- cascade_relay is classified as a bounded linear-propagation root family: value is distinct causal propagation shape, not high coverage capacity.
- Hard push beyond ~0.21 coverage is rejected for V1 because measured candidates transition to fanout=3, branch/hub emergence, localPatchRun=4, or unsolved states.
- Future cascade growth must use relay-lane-constrained extension, not generic pressure/choke fill: preserve fanout <= 2, no in>=2/out>=2 hub, dependencyFollowRunMax >= 4, localPatchSolveRunMax <= 3, and single propagation-chain integrity.
- Root goal interpretation updated: not every root family must reach 0.30; growth target is root-specific and bounded by identity-preserving capacity.

## Decision - 2026-06-24 09:24 - Split Key is a bounded condition-composition root

- split_key_dependency root identity is defined as a core ray requiring at least three independent lock states; it is not dual-gate, hub-spoke, cascade-relay, or ordinary support-lock.
- Split-key V1 gate should use qualified hub evidence, not raw supportClosureHubCount, because raw hub count can misclassify a multi-lock core ray.
- Current V1 stable proof boundary is ~0.20 coverage; >0.22 begins to show unsolved spikes or identity loss, so V1 is also a bounded root family.


## Decision - 2026-06-24 09:33 - Stop independent root primitive discovery under current physics

- Under current static straight-ray collision + clear-trace model, independent root expressiveness is saturated by support_lock, strict_dual, web_crossover, hub_spoke, cascade_relay, and split_key.
- Orbit-delay is a modifier, not a root. Blocker-mask, conflict-dependency, and reverse-block require dynamic/abstract state not present in current mechanics and would collapse into cascade/split/dual variants.
- Next work should focus on production mixing, per-family capacity bounds, identity-preserving fill, and collapse detection rather than naming more root primitives.

## Decision - 2026-06-24 09:41 - Primary root identity must dominate secondary motif tags

- A level can satisfy multiple motif gates, but production diversity must be counted by player-facing primaryRootFamily, not by any true secondary tag.
- web_crossover is not independent when the same level also has dominant support-lock closure and visually reads like the support-lock macro skeleton; in that case it is a support-lock variant/modifier.
- Future mixed-family packs need stricter standalone web criteria and pairwise visual/causal macro-layout dedup before claiming root diversity.

## 2026-06-24 - Availability Peel Is the Preferred Original-Seed Skeleton Extractor

- Decision: For full/high-coverage original seeds, difficulty skeleton extraction should start with availability-shell peeling, not pure static long-chain/fanout scoring.
- Reason: Static low-coverage extraction kept long-chain pressure but often destroyed trace-visible support closure. Availability peel removes continuous free/easy chains and isolated no-blocker chains while preserving the dense dependency core.
- Evidence: On 8 original long-chain seeds, availability peel min8 preserved 8/8 solvability and retained d3-d4 support closure on several rows; best rows include level_510 support `0.889/d4` and Arrowz_level_095 support `0.810/d4` after peeling.
- Boundary: This extractor returns high-coverage cores (roughly 55-129 chains in the first smoke), not tiny 0.3 motifs. That is expected for original long-chain seeds whose difficulty comes from global dependency density.

## 2026-06-24 - Availability Peel Is Shell Removal, Not Final Skeleton Extraction

Availability peel alone only removes currently available/free shells. It can leave a dense residual board and must not be labeled as a true difficulty skeleton by itself. The accepted extraction model is now two-stage: availability peel for shell removal, then trace-root causal-core trim for a visibly smaller skeleton. Final skeleton review requires board-level trace evidence that the causal motif survives, currently `supportClosureBestDepth>=3` for support-lock style skeletons.

## 2026-06-24 - Rename Original Seed Skeleton Extraction to Original Seed Root Extraction

User clarified that the extracted structure from high-coverage original seeds should be treated as a `root`, not merely a `skeleton`. `Skeleton` was ambiguous and sounded like a visual/chain-thinning artifact. The intended object is `original_seed_root`: a player-facing causal structure type extracted from an original full seed by removing easy/free shell and then preserving the trace-visible causal core. It is distinct from the full original level, from a residual dense board, and from low-coverage generated root primitives.

Accepted definition: an original-seed root is the minimal-enough causal core that preserves the original seed's dominant difficulty motif under board-level trace. It may retain some surrounding carrier chains if they are necessary for the motif to survive, but it must not be presented as the full level or as a mere visual long-chain sample.

## 2026-06-24 - Root Requires Role-Graph Minimality, Not Only Support Closure

A root is a reusable causal topology/template with explicit role-bearing chains and ray-derived blocker relationships. Trace metrics such as `supportClosureBestDepth>=3` are necessary diagnostic signals for some root families, but they are not sufficient. Dense residual boards extracted from original seeds can contain strong support closure while still not being roots. Future original-seed root extraction must minimize to a role graph: keep only chains necessary for the dominant causal motif plus minimal carriers required for trace survival; every retained non-cosmetic chain should have a named role or be proven necessary by ablation.

## 2026-06-24 - Original Seed Root Has Nucleus + Carrier Shell

A minimal trace closure graph is a `root nucleus`, not necessarily a reviewable root. A production/review root should usually be the nucleus plus a bounded carrier shell that preserves the motif while reaching a comparable root-scale coverage around 0.25-0.30. Carrier expansion must be trace-tested because naive static expansion can collapse support closure from d4 to d2 and become LocalEasy. The accepted method is: fix the nucleus, search candidate carrier additions from the original seed residual, and accept only variants whose board trace preserves the motif and difficulty class.
# Decision - 2026-06-24 - Flow Lane Gate Must Be Temporal, Not Static

- Evidence: accepted `0.4259` and failed `0.4444` samples have near-identical static core blocker edges, but the failed sample never releases the support-lock core owners in either greedy trace or wave trace.
- Static first-hit preservation or average-choice bounds are insufficient as production gates for near-full fill.
- The meaningful invariant is temporal core reachability: after fill, the selected support-lock / motif core owners and required relay owners must still become available under a lightweight trace/wave simulation.
- A fill operation may add blockers and may create temporary free/debt heads, but it must not replace a required temporal relay with a dead relay that does not flow back into the core.
- Near-full generation should therefore treat candidate fill as temporal flow routing: preserve or deliberately reroute relay lanes, then confirm the core is still releasable before running the expensive final board trace.

# Decision - 2026-06-24 - Temporal Gate Needs Manifold-Aware Candidate Language

- When temporal core wave reachability is correct but ordinary background SGP fill produces no candidates, do not relax the gate or revert to static first-hit/outer-exit rules.
- The failure means the ordinary candidate language is outside the temporal-feasible manifold. Generation must propose candidates from that manifold: cheap raw chain enumeration/score, top-K temporal wave test, then full board trace.
- `-UseTemporalFeasibleGenerator` on `Build-ValidatedRootBackgroundSGPFillV1.ps1` proved the point by advancing the accepted `0.435` parent to `0.5116` while keeping all specified core owners releasable in both greedy and wave diagnostics.
- Temporal reachability alone is not a hard-quality guarantee: the `0.5116` proof is `LocalEasy` with low antiLocal and high dependency follow run. Next ranking must combine temporal feasibility with hard-pressure heuristics before wave/full-trace selection.

## Original Seed Root Extraction Requires Source-Level Eligibility - 2026-06-24

- Broad strict-role extraction over all d3+ original seed sources is useful for diagnosis, but it over-admits forced/weak roots.
- A root source must first pass a `root extractability` screen before extraction: coherent causal nucleus, enough role-chain mass, visible root-shaped organization, trace-visible d3+ motif, and bounded local/choice looseness.
- Current full review pack `SGPRhythmLab_OriginalSeedStrictRoleRootFullV1Review21Pack` is retained as a review/reference pool, not as root-library admission.

## Root Extractability Gate V1 - 2026-06-24

- Current source-level gate: A/B extractable roots must be StrictRootReview, admitted, 20-40 role chains, trace solved, supportDepth >= 3, supportScore >= 0.78, avgChoices <= 4.5, maxChoices <= 9, and not `LocalEasy`.
- `RootExtractableA` additionally requires localPatchSolveRunMax <= 4 and dependencyFollowRunMax <= 4.
- `RootExtractableB` allows local/follow debt up to 5 for visual review, but still excludes LocalEasy and weak motifs.
- `ReferenceOnly` keeps causal signals that are easy/local for learning, but they do not enter root-library review by default.

## Decision - 2026-06-24 - Incremental RoleMap Fill Is V1 Direction, Not Final Form

- The current near-full fill direction should be treated as an incremental state-transition compiler, not a one-shot whole-board solver and not a per-chain full-simulation search.
- Accepted V1 loop: SGP-style bounded candidates -> cheap risk filter (`CriticalTimingZone` hit, cavity split, head-slot loss, future connectivity) -> trace limited TopK batches -> commit one chain -> rebuild RoleMap.
- Trace is a commit validator, not the primary search engine. Use `MaxTraceBatches` to avoid first-batch false negatives, but keep full trace only at commit boundaries.
- Per-chain difficulty drift is allowed when the committed state remains solved/motif-preserving and later commits can recover pressure; final/periodic trace metrics judge the evolving solution space, not single-chain labels.
- This is validated only through `0.3504902` coverage on the 0.30 parent. It is not yet a terminal production architecture for `0.6+` or `0.9+`; next work must improve room scheduling, batching, and pressure-aware recovery while keeping dynamic RoleMap recomputation.

## Decision - 2026-06-24 - Region-Aware Scheduling Is the Next Fill Control Layer

- Fixed center-first room offsets are insufficient after V1; the compiler should schedule rooms by region and structural opportunity, then keep the same commit/recompute safety boundary.
- Region scheduling is an orchestration layer, not a new truth gate. It may rank Core/Mid/Outer rooms by available capacity, pressure slots, critical timing risk, current room coverage, and recent-room penalty, but final acceptance still requires board trace.
- First acceptable trace candidate is not always the best commit. When exploring a region-aware batch, `TraceAllBatchesBeforeCommit + CommitSelectionMode PressureFirst` can choose a stronger candidate from later batches while preserving the same hard trace gates.
- Current smoke supports this: first-accept region scheduling produced a looser `4.21/8` choice curve, while pressure-first all-batch selection reached higher coverage and kept `3.63/7`, supportDepth=4.

## All-Seed Root Scan Boundary - 2026-06-24

- The all-seed pass must be interpreted as a staged screening funnel, not as brute-force extraction from every seed body. We scanned 951 seeds at profile/prefilter level, traced 404 trace-eligible availability-peel sources, and froze 13 selected source-gated strict roots.
- `TooDenseFullBody` sources are deliberately not final failures. They are dense full-body candidates whose root source cannot be cleanly extracted with the current availability-peel + strict role-root method. Mining them should be a separate dense-seed extractor task, not mixed into the strict role-root review pack.
- `AllSeedRootExtractableV2Review13` supersedes `OriginalSeedRootExtractableV1Review9` as the broadest current original-seed root review target, but still requires human duplicate/quality filtering before root-library admission.

## Root Canvas Variant Pipeline Boundary - 2026-06-24

- Root/canvas variants should be treated as identity-preserving transformations of known good roots, not as new root discovery. Safe V1 operations are mirror/rotation/larger-canvas offset transforms that preserve chain continuity and relative blocker topology.
- A generated root variant is only accepted after board-level trace; visual transform alone does not prove root identity or difficulty. Signature/backbone reports are diagnostics, not the final gate.
- Canvas variants may lower raw coverage because the same root is embedded into a larger canvas. That is acceptable for root-variant validation; later high-coverage work should refill around the preserved root rather than force the root itself to cover the whole board.

## Root Variant Generation Boundary - 2026-06-24

- Pure canvas shrink/embedding is a valid low-risk variant mode, but it mainly changes framing/coverage, not player experience, because relative chain distribution and dependency structure are preserved.
- Independent per-chain spatial recomposition is invalid as a default method: first V1 attempt produced 14/14 Drop by breaking ray-causal dependencies. Spatial variation must be core-aware.
- Current accepted V1 methods are: (1) canvas embedding/mirror/rotation as stable presentation variants; (2) peripheral jitter that locks trace core chains and only moves non-core chains, with board trace as final gate.

## Unity AssetDatabase Hot/Cold Split - 2026-06-24

- SGPRhythmLab experimental outputs must not all stay under `Assets/` indefinitely. Unity imports every `.asset`, `.csv`, `.md`, `.txt` and `.meta` there; leaving tens of thousands of historical experiment files in `Assets/ArrowMagic/SOData` makes project startup/database refresh slow and fragile.
- Keep only active review packs, current source root libraries, and reports needed by the immediate workflow in `Assets`. Move cold historical experiments to `_AssetArchive` outside `Assets` and record restore paths/manifests.
- This is reversible archiving, not deletion. If an archived pack is needed again, restore both the asset/level files and their `.meta` files from `_AssetArchive` back to the original relative path under `Assets`.

## Root Variant Semantics - 2026-06-24

- Mirror, rotation, and whole-pack canvas transforms are weak/presentation variants, not official experiential variants. They can be kept for debug, embedding, or later full-level rotation, but should not be counted as production diversity.
- Official root variants should change playable experience while preserving the causal core. Current accepted V1 method is trace-core-preserving peripheral jitter: lock support/dual core chains, move only non-core peripheral chains, then accept through board-level trace and signature/backbone diagnostics.
- RootExperienceVariantV1Review15 is the first mounted review pack following this rule: no mirror/rotation/pure canvas embedding; only peripheral jitter variants with duplicate perturbation signatures removed.

## Variant Strength Boundary - 2026-06-24

- Soft peripheral jitter preserves difficulty but is visually too weak. Aggressive single-chain jitter can create more visible movement but survival rate is low (3/48 in the first probe).
- If `RootVisiblePeripheralJitterV1Review3` is still not visibly different enough, the next official variant direction should be causal cluster / role-anchor remap, not more independent single-chain jitter.

## Variant Direction After Single-Chain Jitter - 2026-06-24

- Single-chain jitter has been rejected by human review as visually/experientially indistinguishable.
- Cluster translation is now under review via `RootClusterRemapV1BReview6`. It has better structural movement but lower survival and still may be too subtle if offsets are small.
- If cluster translation is rejected, the next direction is role-anchor remap or causal cluster swap: move/replace role zones as semantic units rather than translating individual chains or nearby non-core clusters.

## Role-Zone Variant Boundary - 2026-06-24

- Variant scripts must reject no-op transforms (`swapDistance=0`) before reporting diversity. Prior role-zone metrics had false positives because many candidates did not actually move.
- Meaningful variants now require at least a role-zone or causal-zone remap; single-chain jitter and simple cluster translation are not sufficient by human review.
- If non-zero role-zone swap still fails human review, stop trying to mutate old roots and return to new root generation/admission as the diversity source.

## Old-Root Variant Route Limit - 2026-06-24

- Human review rejected single-chain jitter, cluster translation, and duplicated role-zone swap variants as insufficient. Non-zero role-zone swaps can survive only sparsely and mostly within hub_spoke.
- A valid review pack must enforce source/root dedup plus operation/geometry dedup; multiple same-source variants with high chain overlap are not acceptable even if metrics differ.
- Current decision: old-root variants are not the main route to production diversity. Use them as technical probes only; focus production work on new causal root generation/admission.

## Decision - 2026-06-24 - Review new causal roots one family at a time

- Old-root mutations (jitter, cluster remap, role-zone swap) are technical proofs only and should not be used as production diversity claims.
- For root-family review, present one representative per causal family before presenting variants; otherwise within-family duplicates obscure whether the root definition itself is meaningful.
- Current accepted review families are support_lock, strict_dual, web_crossover, hub_spoke, cascade_relay, and split_key. Cascade/split remain bounded roots and are not required to match support-lock density.

## Gate Vocabulary Review Pack Is Allowed To Include Diagnostic Candidates - 2026-06-24

For root-family expansion, first review can include visual/diagnostic gate candidates before final production gating. A candidate must be clearly labeled if trace rejects its strict root identity. Final production packs still require board-level trace validation and root-family identity checks.

## Strict-Dual Gate Production Must Start From Solved Skeletons - 2026-06-24

Do not run production fill on unsolved light-fill gate templates. Strict-dual gate variants must first pass a pre-fill skeleton gate: `solved=True`, `strictDualGateCandidate=True`, `AOnly/BOnly=False`, `APlusB=True`. Fill is then allowed only as a conservative growth transform that preserves strict-dual identity and board-level solvability.

## 2026-06-24 - Gate vocabulary review must be one-per-door, not growth sequence

- Decision: when reviewing strict-dual gate vocabulary, present one independently filled candidate per door/gate design. Do not present multiple growth steps from the same door as if they were different roots.
- Reason: user correctly identified that growth steps preserve the same root identity and only add local chains; they are useful for coverage scaling but not for root/gate diversity review.
- Boundary: `SGPRhythmLab_GateVocabularyV1OnePerDoorProdReviewPack` is a distinctness review pack at coverage `0.1091-0.1348`. Production-hard coverage work starts only after the user accepts which door designs are genuinely distinct.

## 2026-06-24 - Full Release Lanes, Not First-Hit Fragments

- For high-density SGP fill around a verified support-lock parent, preserving only `rayCellsBeforeHit` is insufficient. When an original blocker clears, later cells on the same escape ray must also remain available or be occupied only by chains that release in the correct temporal order.
- A full-ray reserve experiment proved the boundary: full release-lane closure restores solved/supportDepth=4, while first-hit/region masks collapse to LocalEasy/Drop. However, treating full lanes as hard forbidden cells caps coverage too low (~0.45-0.47 in the current 0.338 parent test).
- Next production direction should model release lanes as controlled temporal lanes, not static forbidden masks: SGP may fill lane cells only when the inserted chain itself participates in the lane's release order. Do not continue tuning static head whitelist / outer-exit budget as the main route.

## 2026-06-25 - Root Variant Library V1 core admission rule

- Decision: `RootVariantLibraryV1CorePack` admits only board-trace solved/A candidates with `TrueHardCandidate` or `HardPotential`. `MediumStructure` rows are allowed in diagnostic/review packs but not in the core root/variant library.
- Current core library coverage: strict_dual_gate is thick enough for now; web_crossover, hub_spoke, and split_key each have existing + size variants; support_lock has one recovered fill parent and size variants; cascade_relay has not entered the core library because current samples remain MediumStructure.
- Next production-library priority: (1) recover/admit a stronger cascade_relay parent, (2) add at least one more support_lock non-size variant from the recovered TrueHard parent, (3) only then broaden web/hub/split seed variants. Do not keep adding strict-dual variants until thinner families catch up.

## Root Variant Library V1.2 Admission Rule - 2026-06-25

- Core library admission remains trace-first: only board-trace `solved=True`, `processTier=A`, and `hardStructureV3Class in {TrueHardCandidate, HardPotential}` are admitted.
- Root-family classifiers are family guards, not replacements for trace. For cascade_relay, `cascadeRelayCandidate=True` is required before admission, but MediumStructure cascade rows remain review/diagnostic only.
- Size expansion is admitted only if the transformed level remains hard-class after frozen board trace. In the first cascade smoke, tall40_shift survived as HardPotential, while wide30_shift fell to MediumStructure and was excluded.

## Family-Specific Fill Policy - 2026-06-25

- Shared directed-fill is not a universal production operator for every causal root family.
- `hub_spoke`, `cascade_relay`, `support_lock`, and `strict_dual_gate` are relatively fill-friendly under the current operator.
- `web_crossover` and `split_key` are fill-fragile: web may expose no legal filler groups, while split can preserve split identity but weaken into MediumStructure by losing constraint coupling.
- Decision: do not admit web/split outputs produced by shared fill unless they remain core-hard after frozen trace and family classifier. Treat failures as a signal for family-specific fill policy rather than continuing brute-force shared fill.

## 2026-06-25 - WaveRecovery Is Diagnostic, Not Production Commit Policy

- `WaveRecovery` can prove a high-density boundary is bridgeable while preserving support-lock motif, but it must not be counted as a production-quality commit unless a follow-up strict/sink payback step exists.
- Current evidence: `0.6384804 -> 0.6421569` via WaveRecovery stayed solved/depth4, but immediate strict and Sink-first payback attempts from `0.6421569` failed to find an accepted next step.
- Decision: do not keep expanding beam size around the same candidate language as the main route. Next production direction must either generate sink/choice-compressor candidates before accepting pressure bridges, or generate pressure+sink pairs as a single trace-evaluated unit.

## 2026-06-25 - Campaign500 hardening outer exits need endpoint operators

- V9 showed bulk head flipping can reduce one-ended direct outer exits while preserving Greedy solvability: accepted samples averaged direct outer exits `28 -> 18.5` and avg choices `14.72 -> 9.87`.
- Remaining visible outer exits are often double-ended boundary chains. Reversing those chains only moves the exit head to the opposite boundary, so further head flipping has diminishing returns.
- Decision: the next hardening breakthrough should add endpoint-inset, boundary double-ended chain merge, or trim-rebuild operators that move/remove boundary endpoints while preserving shape and Greedy solvability. Do not spend more time simply increasing the head-flip budget.

## 2026-06-25 - Endpoint hardening works, but trim needs refill before production

- V10 proved endpoint-level hardening is the right next layer after V9: on accepted samples, direct outer exits dropped from `18 -> 12.33` while Greedy remained solved and avg choices dropped from `10.28 -> 7.31`.
- Effective operators were `endpointReroute` and `endpointTrim`. Reroute is production-friendly because it keeps tile count; trim is powerful but removes boundary-facing arrow tiles.
- Decision: keep endpoint hardening as the main outer-exit treatment. For production, pair trim with a controlled refill/rebuild pass, or gate trim by visual/coverage loss. Do not accept weak endpoint changes like `20 -> 19` just because they pass Greedy; outer-exit reduction must be material.

## 2026-06-25 - Outer-exit hardening must include early peel layers

- Current-frame direct outer exits are not enough to explain “still feels easy” after V10. A level can have controlled initial exits, but after the first clearable layer is removed, many second/third/fourth-layer chains can immediately become direct exits and create a continuous sweep.
- V11 validates a multi-layer peel metric: simulate the first 4 Greedy clear waves and count clearable chains whose head ray reaches outside in those waves. On 3 V10 samples, current direct outer stayed `12.33 -> 12.33`, but peel-layer outer exits dropped `51 -> 37`, future peel outer exits dropped `38.67 -> 24.67`, and avg choices dropped `7.31 -> 5.72`.
- Decision: future Campaign500 hardening should gate on both current direct outer exits and early peel-layer outer exits. Operators that only improve wave 0 are insufficient; future-outer orientation flips, endpoint reroute, and trim/rebuild are valid only when Greedy remains solved and the early peel risk materially drops.

## 2026-06-25 - Route PBE and NEE separately

- V12 classification splits early peel outer exits into persistent boundary exits (`PBE`: the head ray already reaches outside on the initial board) and newly exposed exits (`NEE`: the head ray reaches outside only after earlier peel waves clear blockers).
- Current 3-pair V11 sample shows future-layer leaks are entirely NEE: future PBE average `0`, while NEE drops `38.67 -> 24.67` after V11. Current-frame direct outer/PBE remains `12.33 -> 12.33`.
- Decision: do not use one combined future-leak score to choose all operators. Route PBE to boundary topology/endpoint-structure repair, and route NEE to peel-aware propagation/gate control. This prevents using propagation patches on structural boundary leaks or using boundary rewrites for newly exposed release leaks.

## 2026-06-25 - BDR-lite proves the PBE route but is not enough for production impact

- V12 BDR-lite prepends small hooks to current direct-outer/PBE endpoints. On 3 V11 samples it kept Greedy solved and reduced direct outer exits `12.33 -> 10.67`, peel outer `37 -> 32`, future peel outer `24.67 -> 21.33`, and avg choices `5.72 -> 5.36` without NEE rebound.
- Decision: BDR-lite is a valid low-risk PBE repair primitive and should remain in the sandbox toolkit, but it is not yet a large visual/feel breakthrough. The next PBE operator should be stronger boundary double-end restructuring or endpoint stitching/merge that converts edge terminal chains into boundary dependency structures, while preserving shape and limiting fragmentation.

## 2026-06-25 - Physical Pins Beat Generic Head Filters, But Must Be Trace-Derived

- Small-canvas probe showed that generic physical outer-frame pins can materially change SGP output: compared with no frame, an 8-chain outer frame on a 20x26 support-lock parent reduced average openers and outer exits while keeping all candidates solved and mostly preserving supportDepth=4.
- The same probe showed generic head restrictions (`RequireHeadHitsExisting`) are not a safe substitute; they broke solvability/support closure and still did not reliably control outer exits.
- Decision: the promising production direction is not “limit SGP heads globally” and not “use a universal outer frame”. It is “derive gate pins from the parent trace/escape/unlock structure, pre-place those physical pins, then let SGP fill the remaining constrained space.”
- Boundary: outer-frame pins are a diagnostic/scaffold only. They prove pre-seeded physical structure can steer SGP, but they are too boundary-like and tend to become exit buffers; do not treat them as final hard-level pin design.

## 2026-06-25 - Pin Debt Must Be Composite-Gated, Not Auto-Paid

- A paired `C-only` vs `C+Pin` probe on a 20x26 support-lock parent showed that automatically paying every generated direct ray with a physical Pin is unsafe: the Pin lowered choices/openers but frequently collapsed support closure and solvability.
- Decision: a new geometric direct ray may create a pin candidate/debt, but not an automatic commit. A Pin must be accepted only as part of a composite candidate that preserves trace-level core release/support closure.
- Boundary: if the direct ray is not trace-visible outer pressure, prefer soft penalty or alternative candidate ranking over forced blocking. Future Pin generation should produce multiple Pin alternatives per chain and select only those passing a lightweight core/release gate before full trace.

## 2026-06-25 - Cell RoleMap Is Not Enough For PinField Production

- PinField V2 batch probes showed two failure modes of the current adapter vocabulary: hard pre-action role maps can force `outerExitHeadCount=0` and low choices but make levels unsolved/supportDepth collapse; soft role maps keep SGP fill capacity but reopen massive outer exits and LocalEasy collapse.
- Decision: do not promote the current cell-level `HeadAllowed/BodyOnly/CriticalTiming` role-map into the production PinField interface. It lacks temporal release-lane ownership/order semantics.
- Boundary: a successful field must preserve solved/support closure while controlling outer pressure. `outer=0`, low openers, or low avgChoices alone are invalid success signals if supportDepth collapses.

## 2026-06-25 - Lane V0 Head-Only Reserve Failed Shuffle Test

- Lane V0 projected top direct critical rays into protected lanes, then compared raw, soft-lane, hard head-only, and shuffled hard head-only SGP fills.
- True hard-lane and shuffled hard-lane produced nearly identical outcomes: low choices and outer=0, but 0 solved and supportDepth collapse. Soft-lane did not improve over raw and still reopened outer exits.
- Decision: a lane field cannot be implemented as “reserve these lane cells as no-head zones” on top of the current SGP adapter. That only reduces head freedom and does not preserve temporal release semantics.
- Next viable interface must generate candidates that explicitly attach to, continue, or respect a lane’s release order. Do not continue tuning top-K lane owners, HeadOnly masks, or shuffled/no-shuffle reserve lists as the main route.

## 2026-06-25 - Trace-bound First-Hit Owner Is Insufficient For Production Fill

- Trace-bound SGP V0 constrained new SGP-style chains so their head ray first-hits an existing trace `firstHitOwner`. This is a stronger interface than cell/lane masks, but it still does not encode release order.
- Evidence: on the 20x26 support-lock parent, `add02` produced 1/8 solved/S/supportDepth=4 samples, but `add04`, `add08`, and `add12` all produced 0/8 solved/supportDepth=4. Choices and outer exits stayed low, so the failure mode is temporal support collapse rather than outer-pressure explosion.
- Owner de-duplication via `MaxHitsPerTargetOwner=1` did not fix the collapse. The missing variable is not only target owner uniqueness; it is owner ancestry/release wave/allowed interaction mode.
- Decision: do not treat `firstHitOwner` binding as a production fill policy by itself. SGP is still useful as a geometry/body sampler, but future candidate language must consume a trace-edge contract such as `target edge + owner ancestry + release wave + allowed interaction mode`, not just `target owner`.

## 2026-06-25 - Trace-edge Contract V1 Is Promising But Needs Scheduling

- GPT/Rosetta agreed that the next step after Pin/Lane/firstHitOwner failures is a minimal trace-edge contract, not more geometry-mask tuning.
- Evidence: `-UseTraceEdgeContract` inserts new chains into the actual dependency edge corridor. With moderate release filtering (`MinHitWave=1`, `MaxReleaseGap=4`), `add04` improved to 3/8 solved and 2/8 supportDepth>=4 while keeping outer=0; this is a meaningful improvement over firstHitOwner-only `add04` at 0/8 solved.
- Boundary: over-narrow wave filtering (`MinHitWave=2`, `MaxReleaseGap=2`) lost supportDepth, and hard body protection over all critical ray cells caused search timeout. Therefore, do not make `ProtectCriticalRayBodyCells` a default production gate.
- Decision: keep V1b trace-edge corridor continuation plus moderate release-wave filtering as the next baseline. The next improvement should schedule which edges/waves receive insertions and bias body generation away from destructive cells softly, instead of hard-forbidding every critical ray body cell.

## 2026-06-25 - V1e Edge Scheduling Preserves Support But Is Low Throughput

- `EdgeSchedulePreset=WaveBridgeV1` plus `AvoidCriticalRayBodyCells` materially improves survival: 10/12 candidates preserved solved S/A and supportDepth=4 while keeping outer=0.
- However, it averaged only 2.58 added chains against a target of 4, because some scheduled trace edges cannot be realized by the current body sampler. This should be read as a candidate-language throughput problem, not as a solved fill policy.
- Decision: V1e is the current safest trace-semantic baseline. Do not increase coverage by simply lengthening bodies or hard-forbidding body cells; longer body smoke created higher-coverage samples but those collapsed. The next production-facing work should broaden realizable edge insertion geometry or compose small edge groups while preserving the V1e scheduling semantics.

## 2026-06-25 - Hard Motif Migration Is Allowed But Must Be Proven

- Decision: future fill acceptance may allow old supportDepth/root to weaken if board trace proves a new hard motif. This is not a blanket relaxation of supportDepth.
- Minimal gate: solved + choices/outer under limit + (`SupportPreserved` or `HardMotifMigrated` or `SupportRootMigrated`). A migrated motif must be trace-visible, for example `hardStructureV3Class` becomes `TrueHardCandidate/HardPotential`, or a different qualified support root appears with sufficient depth.
- Current evidence: running `Select-MigrationAwareTraceCandidatesV1.ps1` on the V1b/V1e trace-edge batches selected only `SupportPreserved` rows; no `HardMotifMigrated` rows were found, and recent trace-edge metrics contain no `TrueHardCandidate/HardPotential` rows.
- Implication: the migration-aware gate is correct, but current candidate generation has not created migration. Do not count rejected LocalEasy/MediumStructure rows as migration just because old support collapsed.

## 2026-06-25 - Sandwich Fill Needs Generation-Time Outer Pressure, Not Post-Hoc Repair

- SGP can raise density into a solved but loose middle state; the tested sample at coverage `0.4807692` preserved supportDepth `4` but collapsed to Drop because `avgChoices=7.11`, `maxChoices=18`, and `outerExitHeadCount=15`.
- Re-anchoring this state with small trace-edge additions did not materially change the choice curve because only `0-1` chains could be inserted. Critical-edge micro-fill is too weak once outer exits have already proliferated.
- Post-hoc outer-head flips confirmed the pressure surface but also the danger: flipping two heads slightly reduced choices while preserving support, but still Drop; flipping all outer heads solved choice pressure but made the level unsolved and supportDepth `0`.
- Decision: do not use post-hoc geometry flips as the production re-anchor. The next viable sandwich interface must feed outer-exit pressure into SGP before commit, with per-head/edge release ownership and a budget, so SGP does not create uncontrolled exits in the first place.
- Boundary: the sandwich pipeline is still the preferred production shape (`SGP density pass -> pressure re-anchor -> SGP continue`), but the re-anchor primitive must be trace/owner-bound, not a blind edge or boundary geometry edit.

## 2026-06-25 - Peel Non-Skeleton Opener Waves, Do Not Reverse Them

- User correction accepted: when SGP creates a large clearable opener wave, the correct sandwich move is not to add a patch on top and not to reverse those chains. First protect the original difficulty skeleton (`baseChains` and supportClosure owners), then peel only the non-skeleton opener wave.
- Evidence: reversing eligible openers compressed choices but broke solvability/support for aggressive rewrites and still stayed Drop for loose rewrites. It is a negative-control proxy, not a production method.
- Evidence: dropping eligible non-skeleton openers reliably restored A-tier trace while preserving supportDepth=4. The same pattern repeated after SGP refilled the board to ~0.56 and collapsed again; a second peel restored A-tier rows.
- Decision: treat the SGP filler layer as an `opener wave` that can be peeled. This is the first stable control handle on SGP's difficulty collapse.
- Boundary: peeling alone is not enough for production because it lowers coverage. The next production primitive must refill the peeled cells with trace-bound root/gate chains, then hand the board back to SGP. Do not present peel-only rows as finished full-coverage levels.

## 2026-06-25 - Refill Must Be Release-Compatible Group Scaffold, Not SGP Preference

- After peeling `wavepeel_drop_c05_protect_v1_b01_c11_k4`, several refill routes were tested on the same protected skeleton. Trace-edge contract refill preserved support but added only `0-1` chain; directed batch could reach coverage `0.4000` but became LocalEasy/outer-pressure weak; release-scaffold SGP and scaffold+soft-adapter SGP filled to about `0.5+` but all rows collapsed to Drop/LocalEasy.
- Decision: do not interpret preseed release scaffold, soft adapter penalties, or one-chain trace-edge micro-fill as sufficient production refill. They either preserve support with too little throughput or keep coverage while recreating high-choice opener waves.
- Current implication: the missing unit is a release-compatible group scaffold: `release owner/wave -> gate chain group -> protected skeleton rejoin`. This must be generated as a group and become trace-visible before SGP gets another density pass.
- Boundary: do not go back to geometric reversal or post-hoc outer-head flipping as the refill mechanism. Those are useful negative controls only.

## 2026-06-25 - Owner-Hit Is a Positive Control, Not the Final Refill Contract

- `Build-WavePeelReleaseScaffoldGroupV0.ps1` refilled only the cells removed by wave peel and required each new head to first-hit a protected skeleton owner.
- Result: V0 can add higher-throughput groups than trace-edge micro-fill and iterate from coverage `0.3577` to `0.3923` while preserving A/supportDepth=4, with a narrow B-tier continuation to `0.4038`. Larger blind groups (`add4/add6`) and later random owner-hit steps break release order.
- Decision: owner-hit into protected skeleton is necessary but insufficient. Keep it as a positive-control candidate language, while the next production-facing version must add owner/wave scheduling and avoid treating all protected owners as equivalent.
- Boundary: do not turn V0 into one-chain greedy production. Use it to learn which release owners/waves are safe, then compile small scheduled groups.

## 2026-06-25 - Sandwich Production Loop Shape

- User-approved loop shape: `validated skeleton/root -> SGP density fill -> trace detects choice/outer/opener explosion -> protect skeleton owners -> peel current non-skeleton clearable opener wave -> if still above difficulty target, peel another non-skeleton wave -> refill peeled cells with root/gate/blocker chains -> give result back to SGP`.
- Skeleton/root owners must be explicitly protected through the peel step. Do not remove original difficulty skeleton chains or supportClosure owners while clearing SGP-created opener waves.
- The refill step should eventually use the root/gate/blocker generator, not blind reversal and not generic owner-hit alone. Owner-hit is only a diagnostic positive control until owner/wave scheduling is implemented.
- Acceptance must be board-trace based after each phase: coverage, solved, process tier, supportDepth/hard motif, avg/max choices, localPatch/followRun, and dynamic outer pressure.

## 2026-06-25 - SGP Density Failure Is Boundary Opener Wave, Not Space Deadlock

- Growth-order reports on two sandwich SGP passes showed the added SGP chains were almost entirely boundary direct-exit initial openers: first pass 13/13 added chains, second pass 11/11 added chains. Most were exactly the chains later removed by protected peel.
- A `MinHeadLayer=1` control did not fix the issue; it shifted the same pattern inward to layer 1 and caused more unsolved/support loss. Therefore, do not solve this by simply banning outermost heads or moving SGP inward.
- Decision: treat SGP as a density engine that tends to emit a removable direct-exit opener wave. The production middle layer should deliberately recompile that wave into release-compatible root/gate/blocker chains, creating difficulty wave peaks while SGP supplies density between peaks.
- Refill candidate priority is not maximum immediate coverage. A higher-coverage refill base can make the next SGP pass worse. Prefer low predicted added opener/direct-exit count, preserved support/hard motif, and lower avg/max choices; then hand back to SGP.

## 2026-06-25 - Do Not Hard-Cap SGP Direct-Exit Heads Globally

- `-MaxBoundaryDirectExitOpenersPerPass` was added to `Build-SeededDirectSGPFillBaselineV1.ps1` as an opt-in diagnostic, then tested with cap2/cap4/cap8 from the same k4 hard parent.
- Evidence: cap2/cap4/cap8 all retained high geometry coverage around `0.52-0.58`, but produced `0/6` solved and `0/6` supportDepth4 in each batch. The cap lowered openers/choices but changed the SGP packing trajectory enough to break temporal/support semantics.
- Counter-evidence against “some individual direct-exit heads are required”: `Build-SGPOpenerRemovalSensitivityV1.ps1` removed each of 13 non-skeleton openers from the raw full SGP state one at a time; `13/13` variants stayed solved and supportDepth4.
- Decision: do not keep tuning a global direct-exit cap as the production route. Let SGP emit the removable opener wave, then peel and recompile it. The production primitive should be release-wave/owner-scheduled refill, not generation-time direct-exit hard rejection.

## 2026-06-25 - SGP LDF Head Scoring Helps But Does Not Create Release Semantics

- Added opt-in Local Difficulty Field parameters to `Build-SeededDirectSGPFillBaselineV1.ps1`: `-UseLocalDifficultyField`, LDF head weights, `-EmitLdfHeadReport`, and `-UseLdfSupplementalBlockerHeads`.
- LDF V1 keeps the native SGP candidate language and only scores existing outward heads by direct-exit penalty, existing-hit/owner rewards, and whether the head/second cell blocks an existing escape ray. On a 4-candidate comparison, raw SGP kept solved/supportDepth4 but stayed Drop (`avgChoices≈7.16`, `maxChoices=25`, `outerAvg≈16.75`). Strong LDF preserved solved/supportDepth4 and improved pressure modestly (`avgChoices≈6.30`, `maxChoices=18`, `outerAvg≈14.25`) but still stayed Drop.
- The head report confirmed the user's concern: without supplemental heads, all scored candidates were still direct-exit heads (`448/448` direct), though `116/448` had blocker potential. Therefore scoring alone can only pick less-bad outward openers; it cannot create a true blocker/release language.
- LDF V1.5 added supplemental blocker heads from direct escape ray cells whose head ray first-hits existing chains. This created non-direct candidates and strongly compressed pressure (`outerAvg≈8.5`, `maxChoices=11` at high supplemental count), but it broke solvability/support (`0/4 solved`, supportDepth `0`). Even a low-dose supplemental count of 8 stayed `0/4 solved`.
- Decision: keep LDF as a diagnostic/scoring layer, not a production fix. Supplemental blocker heads must be release-wave/owner scheduled before they can be selected. Pure block relation scoring without temporal release semantics either improves too little (outward-only) or cuts the support spine (supplemental blockers).

## 2026-06-25 - V13BDR2 uses two-cell boundary inset before lightweight hook fallback

- GPT second opinion agreed that the next PBE/main direct-outer route should be `two-cell inward hook / endpoint inset`; `double-end stitch` is too risky for the current stage, and endpoint merge/compression should remain low-frequency auxiliary only.
- Decision: V13BDR2 starts from V11 outputs and replaces V12BDR as a stronger branch, rather than stacking two-cell inset after V12BDR. Evidence: stacking after V12 only produced one accepted pair because V12 had already consumed the endpoint slack.
- The production-safe order is: two-cell inset first, one-cell V12 hook fallback second, Greedy validation after each accepted step. Do not add independent edge chains for this pass.
- Acceptance gate for review packs: Greedy solved, chains unchanged, opening choices not increased beyond tolerance, PBE/direct outer meaningfully down (target around 15%), and NEE not rebounding. Weak directional changes can remain in reports but should not enter demo packs.

## 2026-06-25 - V14CMP is cleanup-only boundary compression

- GPT second opinion accepted V14 endpoint merge/compression only as a low-frequency cleanup pass after V13, not as a replacement for inset/hook hardening.
- Decision: V14CMP may merge adjacent edge/direct-outer chains only when it reduces boundary redundancy without adding tiles or new chains. It must keep Greedy solved, avoid opening-choice increases, freeze or reduce NEE, and avoid increasing edge-short or boundary-straight outer exits.
- V14CMP should not be used to force every level to improve. If a level has no safe adjacent boundary-compression candidate, skip it rather than manufacturing straight sweep bands or changing dependency topology.

## 2026-06-25 - Release-Aware Head Provider Needs Body/Corridor Binding

- Decision: continue on the SGP base. Do not switch to a full trace planner; SGP still owns packing/coverage, while trace-derived semantics should condition its candidate language.
- Evidence: opt-in `UseReleaseAwareHeadProviderV2` offered non-direct heads bound to `criticalDependencyEdge` owner/wave contracts. It improved pressure versus LDF control (`avgChoices 7.41 -> 5.72`, `maxChoices 21 -> 17`, `outerAvg 15.75 -> 12.5`) while keeping `4/4 solved`.
- Negative evidence: head-only release binding caused support drift (`supportDepth` fell to `2` in the aggressive run; conservative 1/2-head runs still had one row at depth `2`). A body avoid penalty over critical ray cells did not repair this.
- Decision: treat head-only V2 as a positive diagnostic, not production. The next implementation should bind the generated chain body to a release corridor / trace-edge contract, or explicitly schedule owner/wave body placement, instead of only changing head selection reward/cap.

## 2026-06-25 - Corridor-Bound Release Heads Have a Safe Dose Boundary

- Decision: body corridor bias is a valid next layer for SGP-based generation. It should remain a soft sampling bias first, not a hard lane/mask, because prior hard geometry constraints broke solvability.
- Evidence: with two accepted corridor-bound release heads per candidate, the run preserved `4/4 solved` and `4/4 supportDepth=4`, while reducing avgChoices from `7.41` to `6.01`, maxChoices from `21` to `17`, and outerAvg from `15.75` to `13.75`.
- Negative evidence: accepting four corridor-bound release heads improved pressure slightly more but collapsed supportDepth to `2` in all rows. Therefore the limiting factor is release-head dose and distribution, not whether the corridor concept works.
- Decision: next work should add per-owner/per-wave budgets or a small edge schedule before increasing count. Do not blindly raise `ReleaseAwareMaxHeadsPerPass`; treat 2-head corridor as the current safe positive-control baseline.

## 2026-06-25 - Release-Aware Scheduling Is Wave-Window Driven

- Decision: for the current k4 parent, treat `ReleaseAwareMaxHitWave=3` as the safe V1 release-aware scheduling window. Owner/wave accounting remains useful diagnostics, but exact per-owner or exact per-wave dedup is not the primary control surface.
- Evidence: exact per-owner/per-hit-owner/per-hit-wave/per-owner-wave/per-edge accepted budgets did not improve the 4-head collapse because accepted heads were already naturally distributed across different owners and waves. `ReleaseAwareAcceptedTotalBudget=2` also remained unstable when the two accepted heads included late waves (`hitWave=7/10`).
- Positive control: `ReleaseAwareMaxHitWave=3` with a 4-head candidate pool reproduced the safe 2-head corridor result: `4/4 solved`, `4/4 supportDepth=4`, avgChoices about `6.01`, maxChoices `17`, and exactly two early-wave release heads offered/accepted per candidate.
- Implication: late-wave release-aware heads are currently unsafe even with corridor-bound bodies. Raise coverage/pressure first inside the safe wave window; only later revisit late-wave heads with a stronger contract.

## 2026-06-25 - Safe Wave Kernel Capacity Fails By Choice/Outer Before Support

- Decision: do not use `supportDepth` alone as the capacity success signal for SGP wave-window generation. The first failure mode when pushing coverage is choice/outer explosion, not always support loss.
- Evidence: with `ReleaseAwareMaxHitWave=3` fixed, coverage ~`0.52` preserved `4/4 solved/supportDepth=4` and avgChoices about `6.01`. Pushing to `0.55` kept solved but raised avgChoices to `9.02` and outerAvg to `19.5`, with partial support drift. Pushing to `0.60` still kept `4/4 solved/supportDepth=4`, but avgChoices rose to `11.63`, maxChoices to `29`, and outerAvg to `23.75`. At `0.65+`, solved/support collapsed.
- Implication: early-wave release-aware control is a stable kernel, not a full high-coverage fill policy. Further coverage requires controlling the remaining native SGP heads/opener wave, likely by staged wave windows or a second contract for mid-wave fill. Simply raising target coverage after the safe early-wave kernel is not production viable.

## 2026-06-25 - Stage-2 Soft Scoring Is Not Enough Without New Candidate Language

- Decision: keep Stage-2 emission scoring as a diagnostic/control knob, but do not treat it as the full solution for `0.52 -> 0.55+` coverage. The next meaningful primitive must add release-compatible non-direct Stage-2 candidates.
- Evidence: after fixing Stage-2 reranking, `sgp_stage2_emission_v1_cov055_rerank` improved structure stability (`4/4 solved`, `4/4 supportDepth=4`) compared with kernel-only at `0.55`, but difficulty still failed (`avgChoices=8.915`, max `25`, outerAvg `20.25`). All accepted post-kernel chains were direct-exit heads (`67/67`), proving the native SGP candidate language stayed outward dominated.
- Negative evidence: generic supplemental blocker heads are not the answer; a small supplemental blocker probe (`8` heads) gave `0/4 solved` and support min `1`.
- Implication: Stage-2 must be a weak release-aware / bounded-wave continuation language, not just a higher penalty on native direct-exit heads and not uncontracted blocker injection.

## 2026-06-25 - Stage-2 Head-Level Augmentor Is Safe But Too Weak

- Decision: do not continue optimizing Stage-2 by only adding more head-level scores or generic non-direct heads. The next primitive must be a segment-level candidate grammar that owns the head plus early body, such as owner-mediated mini-chains or bridge-head mini-segments.
- Evidence: `UsePostKernelCandidateAugmentor` supplied non-direct first-hit-owner heads without breaking solved/support. V1/V1B at target `0.55` kept `4/4 solved/supportDepth=4`, accepted `6` augmentor heads across 4 candidates, and reduced post-kernel direct-exit ratio from `67/67` to `64/70`. V1C with post-kernel relation refresh kept `4/4 solved/supportDepth=4` and lowered outer/max slightly (`outerAvg=18`, max `21`), but direct-exit native heads still dominated (`60/64` post-kernel direct).
- Interpretation: the contract is safe, but the language is too sparse. The SGP head/body geometry offers only a few valid non-direct first-hit-owner heads after the early-wave kernel; widening wave bounds did not increase throughput.
- Implication: Stage2 failure is now a grammar throughput problem, not a gate/scoring problem. Keep SGP as the packing engine, but add structured Stage2 alternatives that generate a small owner/wave-bound segment before handing remaining body growth back to SGP.

## 2026-06-25 - Segment Grammar Needs Spine-Safe Trace Binding

- Decision: accept the SGP-3L framing as the next production architecture direction, but reject naive Stage-2 `block/bridge` grammar units as production. L2 grammar units must be trace-edge/spine-safe, not just non-direct first-hit-owner mini-chains.
- Evidence: `UsePostKernelGrammarUnits` with fixed 2-4 cell prefixes dramatically changed Stage2 distribution: aggressive V0 lowered direct-exit ratio to `31/85`, avgChoices to `6.185`, maxChoices to `13`, and outerAvg to `11.25`, but produced `0/4 solved` and support min `0`. A conservative protected-owner V0 still produced `0/4 solved` and support min `0/1` despite lower outer/choice.
- Interpretation: the GPT architecture correctly identifies the missing layer (structure grammar), but a block unit that only first-hits an owner can still sever temporal release/spine. It has pressure power but not identity preservation.
- Next grammar unit contract should include one of: original trace-edge insertion, protected support-path non-interference, or pre/post wave reachability check at candidate construction time. Do not return to pure scoring or generic blockers.

## 2026-06-25 - TraceEdge Grammar Needs Support-Closure Contract

- Decision: original dependency-edge corridor binding is necessary but not sufficient for Stage-2 grammar. Do not promote `UsePostKernelTraceEdgeGrammarUnits` to production until it also protects support closure, not merely wave reachability.
- Evidence: V1 edge-corridor units have throughput when run early (`18/78` post-kernel chains accepted as trace-edge units) and reduce direct-exit dominance versus pure Stage-2 scoring (`directRatio 1.0 -> 0.769`), but without a commit check they produced `0/4 solved` and supportDepth min `0`.
- A cheap wave reachability commit check is helpful but incomplete: it restored `4/4 solved` and lowered avg/max/outer (`avgChoices=6.603`, max `19`, outerHeadAvg `15.5`) but still downgraded support closure (`0/4 supportDepth=4`, min `2`). Restricting to early `hitWave<=3` preserved support in `3/4` rows but mostly reverted pressure (`avgChoices=8.852`, directRatio `0.857`).
- Interpretation: supportClosureDepth is not implied by “all protected owners still wave-reachable” or by edge owner/hit ordering. The grammar unit must preserve the support-closure path/edge set or pass a cheap support-closure proxy before commit.
- Next accepted direction: add a support-path non-interference / support-closure light check for trace-edge grammar, or generate units only on support-approved edge subtrees. Avoid adding more generic block units, looser late-wave windows, or stronger score bonuses.

## 2026-06-25 - Local Edge Relay Proxy Is Not Enough For Support Closure

- Decision: do not treat the first support-path proxy as sufficient. It should remain as a diagnostic filter, but the next production candidate needs a direct support-closure proxy.
- Evidence: the new proxy allowed only local affected edges that either kept the original first-hit owner or became a clean relay through the new chain back to the original first-hit owner, with wave order preserved. It rejected many trace-edge candidates (`167` rejects in the broad-wave run), so it is active.
- Negative result: broad-wave V2 still gave `0/4 supportDepth=4` despite `4/4 solved`; early-wave V2 stayed at `3/4 supportDepth=4`, essentially matching the earlier early-wave result. Therefore local edge/basin preservation does not capture the supportClosureDepth failure.
- Next direction: compare support closure itself at a cheap/proxy level before commit: preserve the original support root, keep at least the original support depth floor, and/or require enough original closure edges/branching to survive. Avoid further tuning of local edge radius, reward, owner-only gates, or generic wave windows until a closure-level proxy exists.

## 2026-06-25 - Copied SGP Head Scoring Cannot Produce Hard LocalRun Shift

- Decision: stop pursuing quality breakthrough through more head scoring/proxy tuning in the copied `SGPGateAwareTrial` path. Keep its diagnostics, but do not treat it as the next production solution for “outer exits low but continuous local clearing”.
- Evidence: V11 k-step local probe exposed the issue but did not fix it (`RRR` stayed high). V12 parent/ancestor local-run proxy, V13 bbox/4x6 patch-lineage proxy, V14 predicted-wave pressure-patch capacity, and V15 stronger cross-region owner-hit scoring all failed to materially reduce real `ComputePressureChoiceCurve` LocalRun/NearRate. Some variants preserved coverage, some collapsed coverage, but none created the desired gameplay shift.
- Interpretation: the defect is not just “which head is selected”; the underlying SGP chain language still creates local available clusters through chain bodies and nearby independent releases. Predicted owner wave does not reliably equal true Greedy release wave.
- Next accepted direction: build a chain-segment/slot grammar that owns head + early body + release timing, or continue the multi-round sandwich cadence where SGP adds only a tiny batch and the exact added slots are immediately rewritten into protected owner-hit scaffolds. Do not keep tightening same-region/patch penalties on the copied SGP head provider.
## Closure Shadow Is A Safety Gate, Not Yet A Pressure Language - 2026-06-25

- Decision: treat bounded closure shadow as the missing supportClosure safety gate for TraceEdge grammar units, but do not treat it as the final Stage2 fill/generation mechanism.
- Evidence: local edge relay V2 preserved solvability but still let `supportClosureBestDepth` fall to 2. V3b support-root closure shadow restored `4/4 supportDepth=4` on the wave3 0.55 probe.
- Negative evidence: V3b accepted only 4 TraceEdge units and pushed direct ratio/difficulty back toward the Stage2 baseline, while rejecting 130 units through closure shadow.
- Engineering boundary: closure-level checks must be lazy/cached/top-k if used in broader windows; running support-root shadow on every broad-wave candidate is too expensive and timed out in the 4-candidate probe.
- Next implication: stop tuning owner/wave/radius/reward as if they preserve closure. The next useful primitive is a closure-safe candidate grammar/pool that has throughput before closure validation, or a lazy closure evaluator that only runs on top-ranked candidates.

## Closure-Compatible Prefilter Is Necessary But Not Sufficient - 2026-06-25

- Decision: keep closure-compatible proposal prefiltering as a diagnostic/control surface, but do not treat the current closure-basin field as a production-safe generative manifold.
- Evidence: wide V4 prefilter did not reject anything and degraded support; narrow V4b/V4c did change the candidate space (`146` prefilter rejects, TraceEdge offered only `8`) but still allowed one full-trace supportDepth collapse (`3/4 supportDepth=4`) even with closure shadow ratio `1.0`.
- Interpretation: the current basin field controls geometric proximity to a support shadow, not the emergent supportDepth invariant. It can reduce bad proposal space, but it cannot prove closure stability.
- Decision boundary: do not continue tuning `PostKernelTraceEdgeGrammarMinShadowRatio`, local edge radius, owner/wave bounds, or basin distance as the main route. These parameters are useful probes only.
- Next accepted route: either make grammar units explicitly support-depth preserving, or run a direct supportDepth/light trace check lazily on top-ranked grammar candidates after cheap closure-compatible ranking. The production primitive should be `rank in closure-compatible space -> top-K direct support check -> commit`, not `generate many -> broad closure shadow reject`.

## Stage2 Native Direct Chains Must Share The Support Contract - 2026-06-25

- Decision: do not rely on TraceEdge-only support guards to preserve supportDepth after the early-wave kernel. The native post-kernel SGP chains remain a support-risk surface and must eventually share a cheap support witness/closure contract.
- Evidence: `Support Witness Edge Gate V1` on TraceEdge grammar commits produced `witnessRejects=0` and the exact same metrics as V4c, including one `supportDepth=2` row. Per-row data showed each candidate accepted only one TraceEdge grammar unit but 16-20 native direct post-kernel chains.
- Interpretation: the current support leak is likely a cumulative Stage2 native-direct effect, not a single TraceEdge grammar edge cut. Protecting only the grammar unit leaves the dominant direct-exit candidate language unconstrained.
- Next accepted route: extend cheap support witness survival or an equivalent direct support proxy to post-kernel native direct chain commits, preferably after cheap ranking/top-K, instead of adding more TraceEdge-only owner/wave/closure filters.

## Native Direct Support Leak Is Not Solved By Scalar Budgets Or Current Local Proxies - 2026-06-25

- Decision: reject hard `Support Pressure Budget` as the primary Stage2 native-direct control. It treats native direct chains as one scalar population; experiments show that low budgets kill solvability while higher budgets drift back toward the baseline without fixing the support leak.
- Evidence: SPB24 produced `0/4 solved` and supportDepth mostly `0`; SPB48 produced only `2/4 solved` and still did not repair the baseline `supportDepth=2` row.
- Decision: reject the simple bad-direct anchor-reuse hypothesis for this sample. A/B identity tagging found release-compatible and opener-pollution direct chains, but blocking B-class chains from future first-hit anchor reuse caused `0` rejects and reproduced the baseline exactly.
- Decision: current support-root closure shadow is not sensitive enough for native direct commits. Native SSD V8 evaluated all native direct commits but rejected none, again reproducing the baseline and its support leak.
- Implication: the failure is a commit-time closure/release geometry effect not captured by direct count, first-hit reuse, or the current bounded shadow proxy. The next useful work is either a stronger top-K supportDepth/light-trace gate for native direct commits, or the sandwich cadence that rewrites small SGP bursts into closure-safe slots. Do not keep tuning SPB budget values, B-class anchor rules, or the existing shadow depth/ratio as the main route.

## Reverse Sandwich Production Line Becomes Main Refill Route - 2026-06-25

- Decision: promote the user-proposed cadence to a formal production route: a bounded SGP burst first identifies real fill slots; if the burst opens too many direct-exit/opener chains, peel the SGP-added slot field and refill it with reverse-valid blocker/release chains.
- Decision: default refill should be `ReverseSlotRefillV1`, not the older random `WavePeelReleaseScaffoldGroupV0`. The old scaffold can reduce choices/outer, but it does not guarantee the inserted blocker chains are themselves release-valid and repeatedly produced unsolved states in the new V0 smoke.
- Evidence: bounded SGP from the 0.30 HardLock parent reached coverage `0.4252451`, solved/supportDepth4, but all 7 added chains were direct-exit initial openers (`addedInitialOpeners=7`, `addedDirectExit=7`), confirming SGP is useful for finding slots but still pollutes difficulty.
- Evidence: `ReverseSlotRefillV1` using those SGP-added slots added 3 reverse-valid chains and produced a formally traced solved/A candidate with openers `3`, avg/max `3.28/6`, outerExitHeadCount `3`, supportDepth `4` in standalone smoke. The orchestrator top-K smoke solved all 8 exported reverse candidates with openers `3`, avg/max `2.95/6`, outer `2-3`, but that seed group only reached supportDepth `2`.
- Implication: the next production fix is trace-aware reverse seed/top-K sweep. Do not trust the simplified reverse/greedy scorer as the final selector; use it only to generate candidates in the SGP slot manifold, then select by official trace supportDepth/choice/outer metrics before the next SGP burst.

## Copied SGP WSCU-Lite Patch Path Is A Dead End - 2026-06-25

- Decision: do not keep pursuing qualitative LocalRun reduction inside the copied `SGPGateAwareTrial` picker by adding anchor spacing, local parent rejection, same-region quotas, or length rhythm caps.
- Evidence: V16 anchor/body WSCU-lite only produced weak or unstable improvements; local parent rejection hurt coverage; same-region length capping created too many short chains and worsened LocalRun; cross-region hard quotas collapsed fill before the board was built.
- Interpretation: the copied SGP picker can report and slightly shape openings, but its chain language still regrows local clear clusters through bodies and nearby releases. True `ComputePressureChoiceCurve` behavior is not controlled by head scoring or simple segment distance.
- Next accepted route: move effort to SGP-3L/sandwich grammar work where small SGP bursts are rewritten as closure-safe trace/slot units, with direct support/light-trace validation on top-ranked candidates.

## Tonight High-Coverage Demo Uses Pressure-Hard SGP Pack, Not Reverse Sandwich - 2026-06-26

- Decision: for the immediate high-coverage demo target, mount `SGPPressureHardTrialPack` rather than continuing to force the 0.30-parent reverse sandwich line to 0.9 coverage tonight.
- Evidence: direct release-aware SGP from the validated 0.30 HardLock parent failed the official trace at targets `0.55`, `0.60`, `0.64`, and `0.90`, dropping `supportClosureBestDepth` to `0` despite the parent retracing as solved/A with supportDepth `3`.
- Evidence: reverse slot refill from the strongest solved `tail_ext_neutral_t070_c01` parent at coverage `0.5980392` could not raise coverage materially under difficulty caps: cap10 added `0` chains and cap11 added only `1` chain, with the cap11 top row already dropping to `Drop`.
- Decision boundary: keep SGP+reverse/sandwich as the research production route for closure-safe slot rewriting, but do not treat it as tonight's completed 0.9 route until a higher-throughput closure-safe rewrite primitive exists.
- Demo deliverable: `Tools/Production/Invoke-SGPPressureHardProductionV1.ps1` now captures the reusable route. Full Unity generation succeeded in `.codex-run/sgp_pressure_hard_production_v1_fullrun_unity.log`, and `-SkipUnity` official trace verification produced `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_hard_production_v1_fullrun_trace_metrics.csv`. The filtered demo pack `SGPPressureHardProductionV1Pack` mounts only the clean high-coverage hit `dense_weave` (`coverage=0.978`, `solved=True`, `processTier=B`, `supportClosureBestDepth=3`, `openers=3`, `avgChoices=3.78`, `maxChoices=6`). Other pressure-hard trial rows remain useful diagnostics but are not the clean tonight target because they are supportDepth2 or Drop.

## Promote PressureHard SGP V1 To Normal-Difficulty Production - 2026-06-26

- Decision: promote `Tools/Production/Invoke-SGPPressureHardProductionV1.ps1` and `SGPPressureHardTrialPack` from a one-off demo pivot to the current normal-difficulty high-coverage production lane.
- Naming: call this formal lane `PSG` / `Pressure-SGP` in future notes. Meaning: SGP remains the packing engine, while the production wrapper adds pressure/trace selection for high-coverage normal-difficulty levels. Do not call it the hard/root-hard line.
- Evidence: the full generation + trace recheck produced four near-full boards (`0.978-0.994` coverage), all solved; 3/4 pass the practical normal-production filter `coverage>=0.97 + solved + processTier A/B`.
- Boundary: this lane should not be advertised as the high-difficulty/root-hard line. Official trace still labels the candidates `LocalEasy`, and stricter support/depth gates leave only `dense_weave` as a clean high-support hit.
- Operating split: use this lane to mass-produce ordinary playable levels with some pressure, then run a separate targeted high-difficulty line for validated roots, sandwich/reverse support-preserving rewrites, and future owner-hit/support-shallowing primitives.
- Acceptance for normal batches: prefer `solved=True`, `coverage>=0.97`, `processTier in {A,B}`, reasonable max choices, and human review for chain-language/readability. `Drop` rows may be retained only as diagnostics or manual edge cases.
- Chain-language knobs such as long-chain, low-turn, or snake-like bias are allowed as a production styling layer, but they must remain downstream of the same trace/human review gates rather than replacing difficulty validation.

## Geometry Supply Is Shape Input, Not Difficulty Evidence - 2026-06-26

- Decision: use PressureHard/PSG outputs as geometry and same-chain edge supply only for high-difficulty experiments; never treat their high coverage or B/A tier as high-difficulty evidence.
- Evidence: current PressureHard samples are near-full and solved, but official trace marks them `LocalEasy`; the manually reviewed `tail0857B_alt_ray1_single_c42` and later `ownerhit0898_tail_single_c63` also show that `coverage/supportDepth` without hardStructure/non-local/manual feel can be misleading.
- Positive signal: adding supply constraints to `Build-OwnerHitGrammarFillV1.py` and growing a validated original-seed TrueHard root along `lock_buckle` supply edges preserved `TrueHardCandidate/supportDepth4` through five traced rounds, reaching strict coverage `0.3623482`.
- Boundary: this is not a high-coverage solution yet. The geometry supply lane must remain a separate high-difficulty research path with acceptance `solved + supportDepth + non-LocalEasy/hardStructure + choice/outer + human feel`.
- Next accepted route: automate multi-round geometry-supplied owner-hit scheduling, add anti-repeat/choice-wave control, and test multiple root/supply pairs before mounting any demo candidate.

## Geometry Supply Bundle Scheduler Needs Class-Drift Control - 2026-06-26

- Decision: promote `root10 + dense_weave + bundle3 owner-hit` as the current leading geometry-supply high-difficulty lane, but do not accept coverage-only continuation after `0.50`.
- Evidence: root10 bundle reached `coverage=0.5000000` with `solved=True`, `processTier=A`, `hardStructureV3Class=TrueHardCandidate`, and `supportClosureBestDepth=4`. This is materially stronger than the earlier low-coverage proof.
- Boundary: in the same r5 batch the highest coverage row (`0.5028986`) downgraded to `HardPotential`, and TrueHard density fell from `24/24` in r4 to `5/24` in r5. This indicates class drift before outright support/solvability failure.
- Next accepted route: scheduler ranking should maximize coverage only inside a hard-class floor, adding penalties for hardStructureV3Score drop, TrueHard density drop, repeated bundle zones, and choice/outer wave drift. Do not keep picking the highest coverage row if it downgrades from TrueHard to HardPotential.
