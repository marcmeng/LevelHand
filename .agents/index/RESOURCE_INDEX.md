# Resource Index

索引只做导航，不复制大段内容。路径以仓库根目录为基准；当前标准项目路径是 `F:\Unityproject\ArrowLevel-Hand`。

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `AGENTS.md` | 项目记忆总入口 | 每次任务开始、路径迁移、工作流规则变化 |
| `.agents/memory/PROJECT_CONTEXT.md` | 项目长期背景和目录职责 | 新对话恢复、项目结构判断、迁移记录 |
| `.agents/memory/WORKFLOW.md` | 统一工作流 | 任务开始、编辑前、验证和收尾 |
| `.agents/memory/DECISIONS.md` | 技术和协作决策 | 需要理解为什么这样组织、是否能改规则 |
| `.agents/memory/CURRENT_STATUS.md` | 当前状态、待办、接续点 | 接手任务、判断 dirty worktree 和下一步 |
| `.agents/memory/CONVERSATION_MEMORY.md` | 临时但重要的对话信息收件箱 | 对话里出现尚未归类的重要信息 |
| `.agents/index/SCRIPT_INDEX.md` | 编辑器工具和脚本入口 | 查找生成、导入、验证、UI、IO、测试脚本 |
| `.agents/index/LEVEL_INDEX.md` | 关卡、包、报告、配置入口 | 关卡/资源包/报告/掩码任务 |
| `ProjectSettings/ProjectVersion.txt` | Unity 编辑器版本 | 打开项目前确认 Unity 版本 |
| `Packages/manifest.json` | Unity package 依赖 | 依赖变化、包冲突、Unity package 排查 |
| `Packages/com.pixelbug.arrow-level-generator/` | 可移植关卡生成包 | DTO、验证、评分、规划、直接矩形生成逻辑 |
| `Assets/ArrowMagic/Docs/Arrow Magic Docs.pdf` | 原始项目文档 | 需要查游戏/资源说明而代码不足时 |
| `Assets/ArrowMagic/Scenes/` | Unity 场景入口 | 运行、UI、演示、创建流程相关任务 |
| `Assets/ArrowMagic/Scripts/` | 运行时代码 | Board、IO、UI、生成适配、ScriptableObject 逻辑 |
| `Assets/ArrowMagic/Editor/` | Unity 编辑器工具 | 批处理生成、导入、候选池、验证、导出、实验 |
| `Assets/ArrowMagic/Editor/Tests/` | 编辑器测试 | 修改核心生成/Board/渲染逻辑后验证 |
| `Assets/ArrowMagic/UI Toolkit/` | UXML/USS | HUD、按钮、棋盘和 UI Toolkit 样式任务 |
| `Assets/ArrowMagic/SOData/` | ScriptableObject 数据根 | 关卡、包、报告、调色板、音效/VFX 数据 |
| `Assets/ArrowMagic/Masks/` | 掩码资源根 | 生产/实验掩码、形状批次、mask 相关生成 |
| `Assets/ArrowMagic/Reports/` | 项目报告资源 | 生产报告和人工检查材料 |
| `Exports/` | 导出交付物 | 交付 zip/xlsx/readme 检查；注意可能有既有产物 |
| `.codex-run/` | Codex 运行产物 | 仅在排查本地自动化或清理临时产物时看 |
| `TempContactSheets/` | 临时 contact sheet 输出 | 图像审查或实验收尾清理时看 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/seed_parent_internal_structure_20260622_profile.csv` | 内部 951 seed 全量结构画像 | 分析原始 seed、R1 修复、R2 二次补肉的骨架差异 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/seed_parent_chokeo2_trace_top80_20260622_metrics.csv` | 静态父本 top80 的 board trace 结果 | 验证“骨架漂亮/覆盖高”是否真的动态难 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/excellent_seed_skeleton_candidates_o1_20260622.csv` | 动态优秀 seed 骨架候选排行 | Choke O2 前优先拆解优秀 seed 骨架语言 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pc34repair_dyn1_accepted.csv` | 0.30 父本承压 + near-miss repair accepted pool | 2026-06-23 已跑通 0.265->0.284->0.300 链路；明早批量去重/打包时优先看 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RP/pc34repair_dyn1/` | 0.30 repair accepted LevelDefinition 输出 | `pc34repair_dyn1_accepted.csv` 对应资产目录 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pcorig1_a_parent_pool.csv` | cov265 第 5 父本自动承压 A pool | 从原 0.265 父本自动 directed fill + near-miss repair 得到 0.299 accepted，验证完整链路不是中间态手工接线 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pcseed06a_a_parent_pool.csv` | headfix 原始第 6 父本自动承压 A pool | 从 `hard_lock_slot_sgp_fill_headfix_v0_base_09` 补到 0.299 accepted；用于扩 A 父本池 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/parent_capacity_o1_review5_selected.csv` | Parent Capacity O1 5 关 review 输入 | 从 `pcorig1` 和 `pcseed06a` accepted pool 合并选出的 5 关，selected coverage 约 0.299 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/parent_capacity_o1_review5_frozen_metrics.csv` | Parent Capacity O1 review5 frozen trace | 5/5 solved/A-tier，`avgChoices<=3.94`、`maxChoices<=7`、`anti>=0.645`、`localPatch<=1`、动态外口 pressure <=1 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_ParentCapacityO1Review5Pack.asset` | Parent Capacity O1 5 关 review pack | 2026-06-23 自动父本承压链路冻结包；用于明早人工复核“0.30 量产线第一版” |
| `Library/`, `Temp/`, `Logs/`, `obj/`, `UserSettings/` | Unity/IDE 生成目录 | 通常不作为源内容；不要提交 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/semantic_skeleton_o2_selected.csv` | Semantic skeleton O2 精选候选 | DependencySkeletonV3/RoomDoorSkeletonV2 的 choice-compression family 精选；按 semantic gate 选出 4 个 0.30 候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/mixed_family_production_pool_o1.csv` | Mixed family production O1 候选池 | 合并 5 个 causal hard-lock 候选与 4 个 semantic choice-compression 候选；用于 production scheduler/family cap |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/mixed_family_production_o1_frozen_trace_metrics.csv` | Mixed family production O1 frozen trace | 9/9 solved；3 TrueHardCandidate、5 HardPotential、1 MediumStructure；验证双 family gate 后指标不漂 |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_hardlock_diverse_review_v1_cov28_selected.csv` | Causal hardlock Cov28 diverse selected CSV | 12 个不同 parentGroup/geometryHash 的 causal-depth hardlock 候选，当前 Demo review 输入。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_hardlock_diverse_review_v1_selected.csv` | Causal hardlock Cov30 diverse selected CSV | 0.299+ 严格去重仅 4 个候选，说明当前 A pool 多样性不足。 |

| .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_skeleton_signature_v1_cov28_v2_summary.md | V2 Cov28 causal skeleton signature summary | 当前 7 关 V2 包：7 core / 5 skeleton family / 4 macro / 1 root skeleton；证明难度成立但 root 结构单峰。 |
| .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_skeleton_signature_v1_cov28_v1_summary.md | V1 Cov28 causal skeleton signature summary | 旧 12 关包：12 rows / 9 core / 6 family / 4 macro / 1 root skeleton；用于对照重复包问题。 |
| .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_skeleton_signature_v1_cov30_v1_summary.md | Cov30 causal skeleton signature summary | 0.30 4 关包：4 rows / 2 core / 2 family / 2 macro / 1 root skeleton；说明高覆盖严格池同样单 root。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_skeleton_species_sampler_v1_cov235_signature_summary.md` | Causal skeleton species sampler V1 signature summary | 12 source species -> 12 core / 12 family / 12 macro / 12 root；证明新 root source 可生成，但多数还不是 production hard。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_species_directed_fill_v1_try1_salvaged_signature_summary.md` | Species directed fill root-collapse 诊断 | 可被 directed fill 救成 hard-like 的新 species 候选 final root 回到旧 root `db4cc18089`；用于验证 species-preserving gate。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/species_preserving_v1_directed_b01_r1_gate_summary.md` | Species-preserving gate on directed fill batch | 18 个 directed-fill 候选中选出 3 个非旧 root 中间样本，证明 fill 过程中存在可保种候选。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/species_preserving_v1_cov26_gate_strictroot_summary.md` | Species-preserving cov26 strict root gate | 非旧 root 中间样本继续补到 0.254-0.262 后退化为 weak/local/sparse root，strict root gate 0/2 通过；用于下一步 species-quality fill 约束。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/species_preserving_compiler_v2_strict_try2_summary.md` | Species-preserving compiler V2 strict run summary | 输入 3 个非旧 root 中间候选，普通 fill 产 2 个可解候选，但 SCL 0/2，通过拒绝原因证明当前 fill 会导致 source root 不保留并退化 weak root。 |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/species_manifold_round3_select_v1_try1_manifoldgate_capfix_selected.csv` | Strong-root manifold round3 selected | 从 `fdbfe36461` source 经 fill 后允许强 root 迁移，选出 `a02280d338`，coverage `0.2757`、avg `2.82`、max `5`、support depth `4`。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/species_manifold_round4_repair_v1_capfix_manifoldgate_selected.csv` | Strong-root manifold 0.29 proof selected | 0.29 near-miss 经 orientation rescue 后，strong-manifold gate 选出 2 个 root（`a02280d338`、`0eea76b1ba`），coverage `0.290441`，solved/A，support depth `4`。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/species_manifold_round4_repair_v1_capfix_signature_summary.md` | 0.29 repair signature summary | 24 个 accepted repair variants -> 4 core / 2 skeleton family / 2 macro / 2 root；证明 repair 后没有单 root 坍塌。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_hard_lite_template_v1_directed_fill_strictfanout_smoke2_final_selected.csv` | 第二 causal backbone primitive proof | 判断 dual-gate hard-lite 是否已脱离 tri root、后续扩到 0.25+ 的起点。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_hard_lite_template_v1_directed_fill_strictfanout_smoke2_final_backbone_backbones.csv` | 第二 causal backbone 签名证明 | 对照当前主 tri root `6fc63698fd`，验证新 root `34771de5e2` 的 branch2/fanout2 因果骨架。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_library_initial_families_v1_catalog.csv` | 初始生成 root family 库目录 | 查看 support_lock/strict_dual/web/hub/cascade/split_key 的基准 root 样本和指标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_library_original_seed_rolegraph_v1_catalog.csv` | 原始 seed rolegraph root 库目录 | 查看原始 seed 抽出的 rootReviewClass，区分 RootReviewCandidate、ThinRoot、NucleusOnly |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_library_original_seed_strict_role_v1_catalog.csv` | 原始 seed strict role root 库目录 | 查看严格 role-root 结果；只包含有因果角色的链集合，不按 coverage 凑 filler |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_full_v1_catalog.csv` | 原始 seed strict role root 全量目录 | 查看 25 个 source family 的 nucleus/direct/strict 候选分类，区分 StrictRootReview、StrictThinRoot、NucleusOnly；用于 full review 后筛 root library 入库候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_full_v1_review21_selected.csv` | 原始 seed strict role root 全量 review21 输入 | Demo 当前全量审查包的排序/来源/coverage/admitted 明细；比 frozen trace 更适合人工筛选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_extractability_gate_v1_screen.csv` | 原始 seed root 可抽性筛选表 | 查看 21 个 StrictRootReview 中哪些是 RootExtractableA/B、ReferenceOnly 或 Reject；用于避免强行抽弱 root |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_extractable_v1_review_selected.csv` | 原始 seed 可抽 root V1 review 输入 | 当前 Demo 9 关候选 root 包的来源、tier、coverage 和指标明细 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_root_source_prefilter_v1_screen.csv` | 951 seed 全库 root-source cheap prefilter | 查看全库 seed 中哪些进入 DeepScan，哪些被 tiny/low-structure 筛掉 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_root_source_trace_eligibility_v1_screen.csv` | 全库 availability-peel trace 资格筛 | 区分 404 trace-eligible、392 TooDenseFullBody、2 screened；定位 dense seed 后续专门 extractor 输入 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_root_source_trace_eligible_v1_trace_metrics.csv` | 404 trace-eligible source trace 结果 | 查看全库 eligible sources 的 process tier、supportClosureDepth、hardStructureV3Class |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_strict_role_root_v1_catalog.csv` | 全库 strict role-root catalog | 查看 267 strict candidates 的 NucleusOnly/StrictThinRoot/StrictRootReview 分类 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_root_extractability_gate_v2_screen.csv` | 全库 root extractability V2 screen | 查看 57 StrictRootReview 中 RootExtractableA/B、ReferenceOnly、Reject 的 source-level 判断 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_root_extractable_v2_review_selected.csv` | 全库 root extractable V2 review 输入 | 当前 Demo 13 关候选 root 包的来源、tier、coverage 和指标明细 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_merged_usable_v1_selected.csv` | 原始 seed merged usable root V1 源筛结果 | 查看合并 rolegraph/extractable 扫描后选出的 16 个可用原始 seed root 候选、source 去重和指标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedMergedUsableRootV1Pack.asset` | 原始 seed merged usable root V1 review pack | 16 个源去重原始 seed root，冻结到短路径并通过官方 trace；用于人工筛掉真正不可用/同质 root |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_merged_usable_root_v1_frozen_trace_metrics.csv` | 原始 seed merged usable root V1 frozen trace | 复核 16/16 solved、7 S + 9 A、supportDepth 3-4；人工评审前看真实过程指标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_merged_usable_root_v1_frozen_trace_joined.csv` | 原始 seed merged usable root V1 joined audit | 人工筛选时同时看 rank、sourceLevelId、rootSource、trace tier、supportDepth、choice 和外口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_canvas_variant_v1b_review16_selected.csv` | Root Canvas Variant V1B review 输入 | 查看 4 个自生产 root family × 4 个几何/画布变体的来源、preset、coverage 和 trace 摘要 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_canvas_variant_v1b_review16_backbone_summary.md` | Root Canvas Variant V1B backbone 摘要 | 判断变体包是否保留多个 causal backbone root，避免全部坍塌为单一 root |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_mixed_v1_review16_selected.csv` | Root Variant Mixed V1 review 输入 | 当前 Demo 16 关 root 变体包；每个 family 含 2 个 canvas_embedding 和 2 个 peripheral_jitter_soft |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_mixed_v1_review16_backbone_summary.md` | Root Variant Mixed V1 backbone 摘要 | 查看混合变体是否保持多个 causal backbone / backbone variants，判断是否只是同 root 微扰 |
| `.worktrees/sgp-rhythm-lab/_AssetArchive/20260624_assetdatabase_trim/` | SGPRhythmLab cold asset archive | Unity database 卡顿后移出 `Assets` 的历史实验资产/报告/旧 pack；需要恢复旧实验包时查这里和 `reports_short_manifest.csv` |
| `.worktrees/sgp-building-grammar/_AssetArchive/20260624_assetdatabase_trim/` | SGP building grammar cold report archive | 2026-06-24 为减轻 Unity AssetDatabase 扫描，将未跟踪实验 reports 移出 `Assets`；恢复旧报告时查 `manifests/sgp_building_grammar_reports_sgprhythmlab_manifest.csv` |
| `.worktrees/sgp-rhythm-lab/_AssetArchive/20260624_assetdatabase_trim_round2/` | SGP Rhythm Lab hotset trim archive | 2026-06-24 第二轮收敛：移出未跟踪 `ReferenceSeeds`、旧 `Levels/SGPRhythmLab` review/source、旧 packs 和旧 reports；恢复时查 `manifests/sgp_rhythm_lab_round2_hotset_trim_manifest.csv` |
| `.worktrees/sgp-rhythm-lab/_AssetArchive/20260624_assetdatabase_trim_round3/` | SGP Rhythm Lab legacy procedural level archive | 2026-06-24 第三轮收敛：移出未跟踪 `Levels/DirectProcedural`、`Levels/NoMaskProcedural` 和 meta；恢复时查 `manifests/sgp_rhythm_lab_round3_legacy_procedural_levels_manifest.csv` |
| `.worktrees/sgp-rhythm-lab/_AssetArchive/20260624_assetdatabase_trim_round4/` | SGP Rhythm Lab reports tail archive | 2026-06-24 第四轮收敛：移出 `Reports/SGPRhythmLab` 下非当前 review3 的残留文件/子目录；恢复时查 `manifests/sgp_rhythm_lab_round4_reports_tail_manifest.csv` |

| `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardTrialPack.asset` | SGP PressureHard 普通难度量产 review pack | 2026-06-26 当前 4 关高覆盖普通难度 review 包；Demo activePack 指向此包，GUID `acd1590a350614a4e86c901d33b5c5dd` |
| `Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_hard_trial_report.csv` | SGP PressureHard production source report | 查看 4 关 source coverage/chains/portable solved/attempts；当前 coverage `0.978-0.994`，4/4 portable solved |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_hard_production_v1_speedcheck_trace_metrics.csv` | SGP PressureHard production official trace metrics | 2026-06-26 speedcheck trace；4/4 solved，3/4 满足普通量产 A/B 筛选，供批量生产验收口径参考 |
| `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardProductionV1Pack.asset` | SGP PressureHard filtered clean-hit reference pack | 单关 clean-hit reference，GUID `afdb809ddc1a4502910d678912899a75`；保留 `dense_weave` 作为 supportDepth3/B-tier 高覆盖样本 |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootExperienceVariantV1Review15Pack.asset` | Root Experience Variant V1 Demo pack | Mounted Demo pack after mirror/rotation were rejected as production variants; use for visual review of peripheral-jitter experiential variants. |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_experience_variant_v1_review15_signature_summary.md` | Root Experience Variant V1 signature summary | Check whether experiential variants create core/skeleton variation without claiming new root generation. |
