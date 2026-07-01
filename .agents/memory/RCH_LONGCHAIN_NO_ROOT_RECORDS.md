# RCH Long-Chain And No-Root Records

本文件专门收纳 RCH 过程中已经证明“有边界价值，但不能作为当前 root-hard 主线底座”的记录。

使用规则：

- 这里的记录不能直接作为当前 baseline、生产母体或成功证据。
- 若要重新使用其中任何规则，必须在当前 root/source-contract validator 下重新生成，并重新跑 official trace + relation audit。
- `probe9_rootcontract` 也属于本档案：它是 validator-clean 的长 band/低链数边界，不是当前 root-hard 主线底座。

## Long-Chain / Mother-Board Boundary

- `Build-RCHReverseSolveOrderWBCV0.py`
  - 结果：`rch_reverse_solve_order_wbc_v0_target095_A` 可生成 coverage `1.0`，official 3/3 solved/S；`centered_two_gate_single_left` 为 `TrueHardCandidate`，hardRaw `0.75`，antiLocal `0.7`，localRun `2`，avg/max choices `2.64/5`，crossCrit `2`，CUD p20 `15`。
  - 问题：代表样本只有 14 chains，avgChain `31.43`，maxChain `80`。这是数值可达边界，不是 RCH 生产母体。
  - 决策：不能用它做 slab/蛇形长链母板再拆短；只能作为“coverage=1.0 + official hard 可存在”的边界证明。

- `Build-RCHRSOSemanticSplitV1.py`
  - 结果：从 RSO full-board boundary 做语义拆分。`probe3 rowgroups2_first4_m40` 可到 coverage `1.0`、17 chains、maxChain `40`、S/TrueHardCandidate；`probe4 rows1/rows2` 可到 18-19 chains、A/HardPotential。
  - 问题：它仍然从 RSO 母板/长 band 关系提取规则，不是直接 root 生成。机械短切到 38-50 chains 会 LocalEasy/Drop；纯 row split 又会 opener/maxChoices 爆。
  - 决策：只保留“2-row group / selective row split”作为规则提取，不作为当前 baseline。

## No-Root / Compact Full-Cover Negative Proofs

- `Build-RCHRootBoardTileContractV1.py`
  - 结果：直接 55 条 4x2 semantic chains，coverage `1.0`，avg/max chain `8/8`，official 8/8 solved，7A+1B。
  - 问题：relation audit 8/8 `LocalEasy`，antiLocal `0.074-0.167`，383/432 relation edges 为 local/same-side。
  - 决策：这是 process-A/full-cover proof，不是 root-hard proof。

- `Build-RCHRootBoardTileContractV2.py`
  - 结果：multi-root / remote-last-blocker scoring 后仍 coverage `1.0`、55 chains、official solved/A。
  - 问题：仍 8/8 LocalEasy，best antiLocal 约 `0.304`。
  - 决策：不要继续调 root count、local budget、tile size。

- `Build-RCHRootBoardRandomPathContractV1.py`
  - 结果：全板 55 条 random length-8 path chains，coverage `1.0`，official solved。
  - 问题：8/8 LocalEasy，process 退到 B，relation audit 为 `363/407` local/same-side edges。
  - 决策：随机短链不等于 root contract segment。

- `Build-RCHRootBoardStripContractV1.py`
  - 结果：尝试 56 条 length 6/8/10 strip chains。
  - 问题：小 root budget 下无法完整规划，开大 roots 退化为多独立入口。
  - 决策：保留为 negative probe。

## Root-Graph / Guard-Fold Negative Proofs

- `Build-RCHRootGraphContractMeshV1.py`
  - 结果：whole-board root graph -> hard/guard contract -> 4x2 geometry 可 full-cover。
  - 问题：official 4/4 LocalEasy；best antiLocal `0.353`、CUD p20 `3.0`。merged graph 显示大量 `guard_contract -> guard_contract` official edges。
  - 决策：独立 guard/corridor chains 会形成 conveyor，不作为主线。

- `Build-RCHRootGraphFoldedContractMeshV1.py`
  - 结果：fold guard 后可 full-cover，20-22 chains/max24 仍 LocalEasy；18-19 chains/max32 可到 MediumStructure、antiLocal `0.533-0.571`。
  - 问题：supportDepth 仍 0；strict local-latest reject 后 0 built。
  - 决策：rect-fold/tile-fold 依赖 local latest blocker，不作为 root route。

- `Build-RCHWholeBoardPortPairPlanV1.py`
  - 结果：把 port-pair 提前到 occupied-set 阶段。
  - 问题：`probe1_paironly` 0 pair / coverage `0.2886`；`smoke2_tiny` pair=0 / coverage `0.4795`。
  - 决策：它仍是在找可放 segment，不是 root-first DAG。

## Polluted Direct Mesh Records

- `Build-RCHDirectNonLocalLatestMeshV1.py` probe1/probe2/probe3/probe8 早期记录
  - 结果：旧表里出现过 coverage `1.0`、17-23 chains、S/A、HardPotential/TrueHardCandidate 的行。
  - 问题：这些记录生成于 source contract validator 之前。后续发现旧 `gate_quarters` 元数据把 `mid_gate_q*` parent 写成不存在的 `structural_root_left/right`，bottom basin 也可指向不存在或不实际 blocking 的 gate。也就是说部分旧行是“official trace 过了，但 source root graph 断根”。
  - 决策：旧 direct rows 不再作为可信 baseline。若要引用，必须用当前 validator 重新生成并确认 `contractRoleOk=True`、`contractLatestOk=True`。

## Validator-Clean But Still Long-Band Boundary

- `Build-RCHDirectNonLocalLatestMeshV1.py` probe9 rootcontract
  - 结果：`rch_direct_nonlocal_latest_mesh_v1_probe9_rootcontract` 12/12 official solved，coverage `1.0`，全部 `contractRoleOk=True`、`contractLatestOk=True`。
  - 19/21 chains：A/TrueHardCandidate，score `0.756`。
  - 23 chains：A/TrueHardCandidate，score `0.75`。
  - 25/27 chains：hard class 尚可，但 maxChoices `11` 导致 process Drop。
  - 问题：19-23 chains、maxChain40，仍是低链数/长 band 结构；虽然 source contract graph 干净，但链条语言不满足“整关多根短/中语义链”的目标。
  - 决策：probe9 只能证明 source validator 抓回了 root contract，不能作为当前 RCH clean floor、成功底座或生产母体。后续必须从整关 root graph 直接切短/中 contract segments，而不是继续在 probe9 的 band 上拆/加 tail。
