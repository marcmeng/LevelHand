# Level Index

## Generated-Root WBP t185 Selector Calibration / Replay Assets - 2026-07-03

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t185_selector_calibration_v0/` - diagnostic LevelDefinition output dir for t185f wave1 selector calibration. These are replay probes, not review/product levels.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t185_calibrated_replay_v0/` - diagnostic LevelDefinition output dir for calibrated replay. Current successful row family is root154 full `12,4,4,1` under `--require-single-coverage-greedy-solved`.
- `t185g_root154_cov12_4_4_1_require_single_solved_c001-c008` - restored root154 historical capacity window, coverage `0.8684211`, Greedy max `5-6`, prior-wave release `1-2`. This is capacity replay proof that t185c/t185d drift was selector/gate calibration, not a final `0.95+` product candidate.
- c038 calibrated full micro replay has no accepted asset yet because interactive `micro24` and `micro8` runs timed out before output. Treat c038 as requiring segmented/offline replay rather than failed root capacity.

## Generated-Root WBP t184 Limited Probe Assets - 2026-07-03

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t184_limited_udg_probe_v0/` - diagnostic LevelDefinition output dir for t184 limited UDG probe execution. These are probe artifacts, not review/product candidates.
- `t184c_seedonly_entry_t182_boundary_owned_root_family_c001.asset` - seed-only UDG entry smoke for the t182 dirty-compatible generated root; coverage `0.5627530`, root+3-chain seedState materialized, no coverage wave. It proves UDG entry/root+seedState plumbing, not capacity.
- `t184c_limited_udg_probe_v0_c038_micro_capacity_control_c001.asset` - c038 no-dirty micro1 coverage probe; coverage `0.6457490`, one coverage duty accepted. Use as gray-zone control evidence only.
- `t184c_limited_udg_probe_v0_c043_stage_control_no_dirty_c001.asset` - c043 no-dirty micro1 coverage probe; coverage `0.6457490`, one coverage duty accepted. Use as gray-zone control evidence only.
- Root154 cross-basin t184c micro1 writes no LevelDefinition asset because the tiny one-chain budget has `rawOptions=20` but `bundleCount=0`; compare against its historical `12,4,4,1` successful wave shape before drawing capacity conclusions.

## Competitor-Hard Phase Ledger V14 Assets - 2026-07-03

- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Packs/DirectProcedural/CompetitorCoreSkeletonPhaseLedgerV14Pack.asset` - current V14 phase-ledger diagnostic pack, GUID `da4d9b3e658446879522416549ecf971`; Demo is mounted to this pack after the final V14 smoke.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Levels/DirectProcedural/CompetitorCoreSkeletonPhaseLedgerV14/` - generated V14 LevelDefinition assets.
- Current rows: `ccsf_v14_001...reserve_mid_body100_r1` coverage `0.760`, `ccsf_v14_002...reserve_wide_body120_r1` coverage `0.747`, `ccsf_v14_003...reserve_low_body80_r1` coverage `0.732`.
- Reports: `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Reports/DirectProcedural/competitor_core_skeleton_phase_ledger_v14_report.csv`, `_slots.csv`, `_wave_audit.csv`, `_sgp_plan.csv`, `_duty_commit.csv`, `_phase_ledger.csv`; summary/trace inputs under `.worktrees/competitor-hard-fresh/.codex-run/competitor_core_skeleton_phase_ledger_v14_*`.
- Official trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/competitor_core_skeleton_phase_ledger_v14_light_trace_metrics.csv`; result `3/3` solved but all `LocalEasy`, process `B/B/Drop`, hardStructureV3Score `0.099-0.137`. Top-coverage step trace is `competitor_core_skeleton_phase_ledger_v14_topcov_trace_steps.csv`.
- Boundary: V14 confirms Phase Ledger instrumentation but fails Experiment A success criteria. Do not promote as hard/high-coverage candidate; next candidate should use contiguous Phase Rooms + Frontier Break Contracts before interval.

## Competitor-Hard Planned Reserve V13 Assets - 2026-07-03

- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Packs/DirectProcedural/CompetitorCoreSkeletonPlannedReserveV13Pack.asset` - current Demo-mounted V13 planned-reserve pack, GUID `47045385ed7148ab9cf400beb8095598`.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Levels/DirectProcedural/CompetitorCoreSkeletonPlannedReserveV13/` - generated V13 LevelDefinition assets.
- Current review rows: `ccsf_v13_002_ccsf_v4_001_9142301_reserve_wide_body120_r2` coverage `0.785`, fixed future slots `13/13`, slot coverage `0.721`, SGP suffix `15`, internal max choices `10`; `ccsf_v13_003...reserve_low_body80_r1` coverage `0.743`, official `MediumStructure`; `ccsf_v13_001...reserve_wide_body120_r1` coverage `0.740`.
- Reports: `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Reports/DirectProcedural/competitor_core_skeleton_planned_reserve_v13_report.csv`, `_slots.csv`, `_wave_audit.csv`, `_sgp_plan.csv`, `_duty_commit.csv`; preview/summary under `.worktrees/competitor-hard-fresh/.codex-run/competitor_core_skeleton_planned_reserve_v13_*`.
- Official trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/competitor_core_skeleton_planned_reserve_v13_trace_metrics.csv`; `3/3` solved, process `A/B/B`, structure `1 MediumStructure / 2 LocalEasy`.
- Boundary: V13 is not final hard/high-coverage success. It is the first playable proof that pre-interval future-slot reservation can preserve planned outlets and beat V12 hard-preserve slightly. Next accepted candidate must add more structural outlet capacity before interval while retaining MediumStructure/hard-core evidence.

## Generated-Root WBP t182 Boundary-Owned Root / UDG Micro-Wave Assets - 2026-07-03

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t182_boundary_owned_root_gen_v0/` - generated `19x26` boundary-owned root assets. These are root candidates/proofs, not product levels.
- `t182a_boroot19x26_c002.asset` - current best boundary-owned root proof. It owns dirty `(5,0;5,1)` same-chain at root generation time, root coverage `0.5384615`, Greedy solved, and can enter the seedState materializer.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t182_boundary_owned_root_udg_v0/` - UDG micro-wave continuation assets from the boundary-owned root.
- `t182i_boroot19x26_udg_micro24_narrow_c001.asset` - highest t182 micro-wave coverage row so far, coverage `0.8036437`, official solved `B/Drop`, root/preplan true, but Product hard-core/tail fail (`LocalEasy`, dependencyFollowRunMax `12`, localPatchRun `7`). Not a review candidate.
- `t182j_boroot19x26_udg_micro24_historysoft_c001.asset` - history-soft quota row, coverage `0.7672065`, Product Hard-Core pass + Tail pass, PlayerStall Review. Useful route evidence; not product-complete.
- `t182k_boroot19x26_udg_micro24_historyquota_c001.asset` - moderate quota row, coverage `0.7874494`, Product Hard-Core pass but Tail fail. Useful evidence that quotas can create hard-core window but can still poison tail.
- `t182m_boroot19x26_udg_micro24_latequota_c001.asset` - late-quota row, coverage `0.7854251`, Tail pass/spine controlled but Hard-Core fail due missing frontier break. Useful evidence for phase-aware quota need.
- No t182 asset is Demo-mounted or final `0.95+` product evidence. The accepted next level attempt must add structural-support/frontier-break duties before scaling coverage.

## Generated-Root WBP t181 Dirty Boundary Ownership Entry Split - 2026-07-03

- t181 produced no accepted `0.95+` LevelDefinition assets and no Demo mount.
- c027 required-cell seedState probes (`t181d/t181g`) intentionally write no strict seed chain plan rows; their purpose is to prove dirty `(5,0;5,1)` is not seedState-solvable after the existing 19x26 root is fixed.
- root10_c036 is a useful positive topology reference but not a direct candidate: it is `23x30`, while the active c027/c038/c043 route is `19x26`.
- Next playable candidate should come only after a generated `19x26` root owns at-risk dirty boundary clusters before seedState/coverage. Do not Demo-mount t181 artifacts as product levels.

## Generated-Root WBP t180 Boundary Corner Topology / Ownership Audit - 2026-07-03

- t180 produced no accepted LevelDefinition assets and no Demo mount. It is an audit/probe phase for boundary ownership, not a playable candidate phase.
- Use `t180b/t180c` reports to show that c027/c038/c043 seed-only edge clusters have `0` empty accepted non-open boundary entries.
- Use `t180d_c027_top_x5_anchor12_*` to show dirty `(5,0;5,1)` cannot be made dependency-compatible by a longer anchored corridor after seed-only; all owner paths up to length `12` are Greedy-unsolved.
- Current playable/reference state remains the earlier c027 hygienic row `t174l_c027_after0921_open1_forbid5x0.asset` at coverage `0.9251012`. It is still not product-complete and should not be called a t180 candidate.

## Generated-Root WBP t179 CornerDuty / Dirty-Prefix Assets - 2026-07-03

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t179_dirty_prefix_early_duty_bcl_v1/` - diagnostic assets for dirty-corner early-prefix reproduction. Not final `0.95+` candidates and not Demo-mounted.
- `t179c_c027_dirtyprefix_ecduty_scan_v1_c001.asset` - forced `(5,0;5,1)` open prefix plus adapted c027 early-duty scan. Coverage `0.6740891`, Greedy solved, but it changes guard duties and subsequent BCL cannot form 2/3/4-chain bundles.
- `t179h_c027_dirtyprefix_ecduty_bcl1_boundary_c001/c002.asset` - only single-chain continuation from t179c; coverage `0.6842105`, release owner `0`. Use as a negative boundary showing open-prefix injection is not a true CornerDuty solution.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t179_cornerduty_activation_v1/` - diagnostic activation-beam assets for injected non-open corner duty. `t179k_c027_cornerduty_activation_v1_c001.asset` is solved at coverage `0.6558704` but does not select the corner duty, so it is not a valid B-group success. Required-corner probes `t179m/p/q/r/s` write no solved candidate assets beyond base/no-add rows.

## Generated-Root WBP t175 Dirty-Tail Diagnostics - 2026-07-03

- t175 produced no accepted LevelDefinition assets. It is a report-only boundary probe for the c027 dirty cluster `(5,0;5,1)`.
- Use `t175a/t175b/t175c` reports under `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/` as evidence that the cluster cannot be safely appended late as an open chain, non-open single chain, or dirty+one-support bundle from the tested contexts.
- t176d/t176e repeat the dirty-cluster test from the earlier `t173j` c027 base and also produce no accepted non-open or dirty+support bundle assets.
- Current best c027 review candidate remains `t174l_c027_after0921_open1_forbid5x0.asset` at coverage `0.9251012`, not a t175 asset.

## Generated-Root WBP t178 Forced Dirty-Open Prefix Assets - 2026-07-03

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t178_forced_dirty_open_prefix/` - diagnostic assets for moving `(5,0;5,1)` earlier as an open prefix. Not final `0.95+` candidates.
- `t178a_c027_seedonly_forced_dirty5_open_prefix.asset` - forced dirty-open prefix on seed-only `t155e`; Greedy solved, but direct closure only reaches `0.7793522`.
- `t178c_c027_t157e_forced_dirty5_open_prefix.asset` - forced dirty-open prefix on the stronger `t157e` c027 base.
- `t178d_c027_t157e_dirty5_prefix_psclosure_095.asset` - continuation from `t178c`; coverage `0.9251012`, official solved `B/Drop`, Hard-Core pass, Tail pass, coverage incomplete. Use as evidence that early dirty-open is hygiene-safe but not capacity-solving.
- `t178g_c027_dirty5_prefix_after0925_wide_tail.asset` - no-add continuation from `t178d`; coverage remains `0.9251012`.

## Generated-Root WBP t173 Hard-Core Matrix Smoke Assets - 2026-07-03

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t173_hardcore_matrix_smoke_v1/` - diagnostic output dir for t173 structure-family and clean-tail probes. Not a final `0.95+` review pack.
- `t173j_c027_after0906_noopen_tail_probe.asset` - no-open clean-tail probe from c027 `t158m` coverage `0.9068826`. It preserves propagated root/preplan lineage (`root154_core_sched0589_v1_r3_c027`, `rootPreserved=True`, `preMaterializationDutyCommit=1`) but adds `0` chains and remains at coverage `0.9068826`. Official/product recheck `t173k/t173l` is solved `B/B`, `CoverageIncompleteHardCore`, Hard-Core pass, Tail pass, and fails Overall only by coverage.
- Boundary: t173j proves the c027/maze-like lane has high hard-core capacity but cannot reach `0.95` by late no-open tail closure. The next c027 attempt needs earlier tail-space reservation or rewrite, not another after-the-fact closure pass.
- Product matrix slice `t173m` confirms there is still no product-complete row. Among executable current candidates, c027 is closest by coverage (`0.9068826`), root154 patchwork/cross-basin are stable current-grammar structures (`0.8684211`), and c038 micro24 has lower coverage (`0.8299595`) but useful micro-wave/tail-capacity evidence.
- Hub/spiral t173 structure attempts produced no accepted level assets under the current owner-hit grammar. Patchwork/cross-basin positives remain the t170/t172 root154 `0.8684211` assets and reports, not new t173 product rows.

## Generated-Root WBP t174 c027 Tail-Safe Open Assets - 2026-07-03

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t174_tail_safe_open_probe_v1/` - diagnostic output dir for c027 controlled open-tail probes. Not a final `0.95+` review pack.
- `t174l_c027_after0921_open1_forbid5x0.asset` - current best hygienic c027 product candidate by coverage. Coverage `0.9251012`, official solved `B/Drop`, root/preplan true, Hard-Core pass, Tail pass, Overall fails only because coverage is below `0.95`.
- `t174c/t174d/t174o/t174v` - official-solved negative rows where late open cluster `(5,0;5,1)` causes Tail Hygiene failure by `global_dependency_follow_run`. Use them as anti-spine training/boundary evidence, not review candidates.
- `t174ac_c027_after0919_dirty5_first_open1.asset` - dirty-cluster-first negative. It forces `(5,0;5,1)` earlier, still official-solved `B/B` but Tail Hygiene fails with `globalDependencyFollowRunMax=12`.
- `t174y_c027_after0925_forbid5x0_wide.asset` - no-add boundary from clean `t174l` with `(5,0;5,1)` forbidden; coverage stays `0.9251012`, proving the next step needs earlier reservation/rewrite rather than late clean rerank.

## Generated-Root WBP f018 Lower-Prefix Full-Slot Assets - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/f018_lower_prefix_fullslot_v0/` - diagnostic LevelDefinition output dir for f018 lower-prefix full-slot probes. Not Demo-mounted and not a production/review pack.
- `f018d_t156o_c001_smallstep_fullslot_t834_c001-c008` - accepted small-step calibration rows from `t156o_c027_ecduty_bcl4x6_c001`, coverage `0.8340081`, added `2` chains / `4` cells, Greedy max `4`. Useful only to prove V0 can still make small exact late steps.
- Boundary: `t156k/t156m/t156o -> 0.84/0.86`, `t156c -> 0.720`, and `t156s_c005 -> 0.86` produced `0` accepted rows under f018 smoke windows. Keep f018 as diagnostic evidence for staged re-enumeration need, not as final level content.

## Competitor Core Skeleton T145 + V7 Fill V11 Visual Diagnostic - 2026-07-02

- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Packs/DirectProcedural/CompetitorCoreSkeletonT145V7FillV11Pack.asset` - current `competitor-hard-fresh` Demo-mounted visual diagnostic pack, GUID `d4a450fc66b5433b931405b29cd1eb75`.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Levels/DirectProcedural/CompetitorCoreSkeletonT145V7FillV11/` - V11 LevelDefinition output dir. Rows are `ccsf_v11_001_ccsf_v10_001_ccsf_v4_001_9142301_p1_bcl8_interval_v7fill095` and `ccsf_v11_002_ccsf_v10_002_ccsf_v4_001_9142301_p2_bcl8_6_interval_v7fill095`.
- Static result: row 002 coverage `0.951` from source `0.699`, forced tail cells `157`, patch chains `9`; row 001 coverage `0.950` from source `0.714`, forced tail cells `146`, patch chains `9`.
- Official trace: both rows are `solved=False`, `Drop/Drop`, `WeakCausality`, hardStructureV3Score `0.027`, remote choke `0`, support closure depth `0`.
- Boundary: this is a visual answer to inspect V10 after V7-like filling. It proves late fill can make the board full but cannot preserve playability/hard structure.

## Competitor Core Skeleton T145 Hybrid V10 Diagnostic - 2026-07-02

- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Packs/DirectProcedural/CompetitorCoreSkeletonT145HybridV10Pack.asset` - current `competitor-hard-fresh` Demo-mounted diagnostic pack, GUID `f85d34cebed241ffa89dcc8f40769327`.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Levels/DirectProcedural/CompetitorCoreSkeletonT145HybridV10/` - V10 LevelDefinition output dir. Current rows are `ccsf_v10_001_ccsf_v4_001_9142301_p1_bcl8_interval` and `ccsf_v10_002_ccsf_v4_001_9142301_p2_bcl8_6_interval`.
- Static result: row 001 coverage `0.714`, `90` chains, BCL/interval/close `8/24/2`, avg/max choices `2.80/5`, connector contract `3/3`; row 002 coverage `0.699`, `91` chains, BCL/interval/close `8/24/3`, avg/max choices `2.92/6`, connector contract `3/3`.
- Official trace: row 001 solved `A/A`, `MediumStructure`, difficultyScoreV2 `0.697`, hardStructureV3Score `0.202`; row 002 solved `B/B`, `MediumStructure`, difficultyScoreV2 `0.656`, hardStructureV3Score `0.173`.
- Boundary: V10 proves early t145-style connector planning is structurally better than post-V8 V9 (`LocalEasy`), but it does not solve the coverage target. Treat it as route evidence; next level attempt needs a pre-interval body-basin / room-slot / support-control materializer.

## Generated-Root WBP t164 Closure-First Staged Basin Assets - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t164_closure_first_basin_reservation_v0/` - diagnostic LevelDefinition output dir for closure-first staged basin candidates. Not final `0.95+` levels and not Demo-mounted unless explicitly changed later.
- `t164v_c043_stage9_oldrelease_guard_v0_c001` - current c043 best route sample. Coverage `0.7550607`, added staged basin chains `10`, root preserved, official trace solved `A/A`, avg/max choices `2.48/5`, `MediumStructure`, DifficultyVerify `HardPotential 0.795`. Trace files: `t164w_c043_stage9_oldrelease_guard_v0_trace_*`; verifier: `t164x_c043_stage9_oldrelease_guard_v0_difficulty_verify*`.
- `t164y_c038_stage9_oldrelease_guard_v0_c001` - same-family c038 proof. Coverage `0.7530364`, added staged basin chains `10`, official trace solved `A/A`, avg/max choices `2.21/5`, `MediumStructure`, DifficultyVerify `HardPotential 0.797`. Trace files: `t164z_c038_stage9_oldrelease_guard_v0_trace_*`; verifier: `t164za_c038_stage9_oldrelease_guard_v0_difficulty_verify*`.
- `t164m_c043_closure3_staged_basin_2_1x5_v0_c001` - lower-coverage A/A positive before release policy tuning. Coverage `0.7348178`, official `A/A`, DifficultyVerify `HardPotential 0.77`.
- `t164p_c043_closure3_staged_basin_to082_v0_c001` - ungated higher-coverage boundary. Coverage `0.7955466`, official solved but `B/B`; use as evidence that raw staged fill can climb coverage but starts dependency-follow/local-run collapse.

## Generated-Root WBP t162 Future-Capacity Lookahead Diagnostics - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t162_coverage_basin_lookahead_v0/` - diagnostic output dir for coverage-basin future-capacity lookahead. Not Demo-mounted and not final review-pack content.
- `t162b_c043_covbasin2_geolook_targeted_v0` - targeted geometry lookahead smoke. It sees future basin geometry after first-basin candidates (`futureCoverageOptions` up to `19`, owners up to `7`), but writes `0` solved bundles, so geometry capacity alone does not solve the second basin.
- `t162d_c043_after_t161f_second_no0_v0` and `t162e_c043_after_t161f_second_allow0_v0` - continuation probes from the known t161f prefix. Both write `0` solved ActivationBeam bundles for a second basin; owner0 allowed does not help under the shallow ActivationBeam window.
- Boundary comparison: `t161j_c043_covbasin_auto_then_bcl1_c001` can add one deep BCL chain to coverage `0.6842105`, but only at option ranks `75/111` through release owner `0`. Treat this as afterfill evidence, not as a valid generator direction.

## Competitor Core Skeleton WBP Bundle V9 Diagnostic - 2026-07-02

- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Packs/DirectProcedural/CompetitorCoreSkeletonWBPBundleV9Pack.asset` - current `competitor-hard-fresh` Demo-mounted diagnostic pack, GUID `f6c96f602f844114b456732e77155e04`.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Levels/DirectProcedural/CompetitorCoreSkeletonWBPBundleV9/` - V9 LevelDefinition output dir. Current row is `ccsf_v9_001_ccsf_v8_038_ccsf_v4_001_9142301_psgwp10_wbp1`.
- Static result: coverage `0.765`, `95` chains, `551` arrows, base coverage `0.711`, accepted bundles `4/8`, openers `3`, avg/max choices `2.73/5`, connector contract `3/3` first-hit and `3/3` corridor-clear preserved.
- Official trace: solved `B/B`, but `hardStructureV3Class=LocalEasy`, hardStructureV3Score `0.126`, avg/max choices `3.33/7`, frontierDrainRemoteChokeCount `11`, choiceChokeAfterLocalFrontierBreakCount `4`, supportClosureBestDepth `4`.
- Boundary: V9 is not a successful hard/dense candidate. It proves post-V8 WBP bundle fill is too late; next candidate should plan WBP coverage-basin/support-closure duties from the V4 connector or earlier semantic core.

## Generated-Root WBP t161 Coverage-Basin Activation V0 Diagnostics - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t161_coverage_basin_activation_v0/` - diagnostic LevelDefinition output dir for coverage-basin-in-activation V0. Not Demo-mounted and not final review-pack content.
- `t161f_c043_covbasin_auto_no0_v0_c001` and `t161g_c038_covbasin_auto_no0_v0_c001` - current same-family positives. Each selects 3 early semantic duties plus one non-owner0 coverage-basin chain released by owner `35`, coverage `0.6740891`, Greedy max `8`/`7`.
- `t161j_c043_covbasin_auto_then_bcl1_c001` and `t161k_c038_covbasin_auto_then_bcl1_c001` - one-chain BCL continuations from the t161f/g prefixes, coverage `0.6842105`, but release owner is `0`; boundary evidence only.
- Boundary: two-basin assets from `t161c/t161d/t161e/t161l` write no accepted candidate. Do not present t161 as a complete level route; use it to guide earlier region/space rewrite for the second basin.

## Generated-Root WBP t160 Activation-Aware Early Closure V2 Diagnostics - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t160_early_closure_bundle_v2/` - diagnostic LevelDefinition output dir for t160 early-closure bundle/activation-beam probes. Not Demo-mounted and not final review-pack content.
- `t160t_c043_activation_autoshortlist_chain3_v2_c001` and `t160u_c038_activation_autoshortlist_chain3_v2_c001` - current t160 positive same-family prefixes. Each adds `3` semantic closure chains and reaches coverage `0.6639676`; useful as root-general early materializer evidence, not as a complete level.
- `t160x_c043_ecl3_bcl1_c001-c002` and `t160y_c038_ecl3_bcl1_c001-c002` - one-chain BCL continuations from the 3-chain prefixes, reaching coverage `0.6740891` through release owner `0`. Boundary evidence only.
- Boundary: `t160v_c043_ecl3_bcl2` and `t160w_c038_ecl3_bcl2` write `0` BCL exact2 bundles. Do not continue this as one-chain BCL drilling; next route must add coverage-basin / multi-release supply before materialization.

## Generated-Root WBP t159 Root-Generalization Same-Family Smoke - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t159_root_generalization_stage1_v1/` - diagnostic LevelDefinition output dir for root-general same-family smoke. Not Demo-mounted and not final review-pack content.
- `t159b_c038_seedonly_oldstate_c001-c004` and `t159c_c043_seedonly_oldstate_c001-c004` - seed-only same-pipeline smoke from stage1 roots c038/c043. Coverage `0.6397-0.6417`, Greedy solved; proves same-family seed/projection compatibility, not final coverage.
- `t159n_c038_c001_ecduty_scan24_c001` and `t159o_c043_c001_ecduty_scan24_c001` - early-closure V1 with candidate scanning. Both reach coverage `0.6558704` with 2 closure chains; useful evidence that candidate scanning helps but V1 is still not root-general.
- `t159p_c038_c003_ecduty_scan24_c001` and `t159q_c043_c003_ecduty_scan24_c001` - lower-choice seed group c003 with candidate scanning. Coverage `0.6538462` with 2 closure chains; confirms the remaining gap is not just seed group selection.
- Boundary: t159 levels are route diagnostics. Do not compare them to t158/t156 review candidates as final levels; their purpose is to prove root-generalization layers and expose that early-closure V2 must be bundle/activation-aware.

## Competitor Core Skeleton PSG Batch V8 - 2026-07-02

- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Packs/DirectProcedural/CompetitorCoreSkeletonPSGBatchV8Pack.asset` - current `competitor-hard-fresh` Demo-mounted structural review pack, GUID `2a2d7ad0f2664533aaacfd3b7392d521`.
- Pack is manually curated to contain only the official `MediumStructure` V8 rows: `ccsf_v8_038_ccsf_v4_001_9142301_psgwp10`, `ccsf_v8_036_ccsf_v4_001_9142301_psgwp8`, and `ccsf_v8_033_ccsf_v4_001_9142301_psgwp5`. Generated `ccsf_v8_040...` is excluded because official trace says `LocalEasy`.
- Static mounted band: coverage `0.694-0.711`, `83-88` chains, `500-512` arrows, `3/3` SkeletonPSG first-hit contracts and `3/3` corridor-clear contracts preserved, openers `3-4`, internal max choices `6-7`.
- Official trace: generated rows `4/4` solved; mounted rows are `2 A/A MediumStructure + 1 B/B MediumStructure`. Best official structural/choke signals include `frontierDrainRemoteChokeCount=11` on `ccsf_v8_038` and hardStructureV3Score `0.320` on `ccsf_v8_036`.
- Boundary: V8 is the correct-route structural pack after rejecting V7-style rank-field/tail closure. It does not meet the `~0.95` coverage target; current whole-batch capacity stops around `0.71`, so next work should increase preplanned corridor/topology capacity.

## Competitor V4 Skeleton PSG Direct-Logic Probe - 2026-07-02

- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/competitor_v4_skeleton_psg_feasibility_probe1_summary.csv` - direct feasibility probe on current V4 formal skeletons. Results: `ccsf_v4_009` strong/corridorStrong `108/108`, `ccsf_v4_001` `110/110`, `ccsf_v4_008` `110/110`; all `immediateStrong=0`, all verdict `PSGConnectableNeedsCorridorWavePlanner`.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/competitor_v4_skeleton_psg_corridor_wave_probe1_selected_corridors.csv` - selected corridor contracts from V4: strict no-overlap selected `4`, `3`, and `5` critical corridor units respectively, with `earlyBaseRayBlocks=0`.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/SkeletonPSGCorridorConnectorCutterV1/cc_ae4b7668/` - materialized low-coverage connector probe assets. Coverage range is about `0.468-0.541`; static contract passes (`firstHit`/`corridorClear` all pass). These are route probes, not review-pack levels.
- Boundary: no Demo pack was built or mounted for this probe. Use it as evidence that the existing SkeletonPSG corridor-duty logic can be reused for the next V4-based route; do not treat these low-coverage assets as final candidates.

## Competitor Core Coverage095 V7 Solved Review - 2026-07-02

- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Packs/DirectProcedural/CompetitorCoreCoverage095V7Pack.asset` - current `competitor-hard-fresh` Demo-mounted high-coverage review pack, GUID `7fb8a5739cf1463d90a911a1759f4cae`.
- Pack contains only `ccsf_v7_002_ccsf_v3_008_8638894_ne_cov095` after official trace curation. The other generated row `ccsf_v7_001...` is not mounted because official trace reports `solved=False`.
- Mounted row stats: `24x30`, `132` chains, `684` arrows, coverage `0.950`, tail closure `+32` cells, internal openers `7`, avg/max choices `4.47/8`.
- Official trace on mounted row: `solved=True`, process `B`, tight `Drop`, hardStructureV3Class `LocalEasy`, `frontierDrainRemoteChokeCount=0`.
- Boundary: this is the current `~0.95` visual coverage answer, not a hard-level success. Use V7 for visual inspection of coverage density; use V5/V6 evidence for the hard-structure tradeoff.

## Front20 Lite V1 Optimized20 Review - 2026-07-02

- `.worktrees/nutation-front20-slotwise-runner/Assets/ArrowMagic/SOData/Packs/Campaign500/Front20LiteV1Optimized20ReviewPack.asset` - current runner Demo-mounted 20-level review pack, GUID `f20f201020707e44a8ba4aef00000020`.
- `.worktrees/nutation-front20-slotwise-runner/.codex-run/front20_lite_v1_optimized20_selection.csv` - selected rows and reasons from the strict `Front20LiteV1 Final40` pool.
- Composition: 10 EarlyNormalLowPressure, 6 Front10ReadCheckLite, 4 Front20MiniBoss; selected styles include Flow, Peel-lite/Peel, Lock-lite, Mixed-lite/Mixed, and LongChain-lite.

## Front20 HardBody V1 Review - 2026-07-02

- `.worktrees/nutation-front20-slotwise-runner/Assets/ArrowMagic/SOData/Packs/Campaign500/Front20HardBodyV1ReviewPack.asset` - current runner Demo-mounted 20-level hardbody review pack, GUID `f20f20d120707e44a8ba4aef00000021`.
- `.worktrees/nutation-front20-slotwise-runner/.codex-run/front20_hardbody_v1_review20_selection.csv` - final strict review selection from two hardbody production batches.
- Composition: 10 EarlyHardBody, 6 ReadHardBody, 4 MiniHardBody; selected styles are PeelHard, PeelLight, PressurePeak, and NeutralMixed, with pure Flow rows excluded from the final review pack.
- Compared with Optimized20, average chains increase from `34.3` to `40.65`, avgChoices from `4.033` to `4.577`, and dependencyFollowRun from `3.1` to `3.7`.

## Competitor Core Skeleton WholePlan V6 Review Pack - 2026-07-02

- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Packs/DirectProcedural/CompetitorCoreSkeletonWholePlanV6Pack.asset` - current `competitor-hard-fresh` Demo-mounted review pack, GUID `c5177b438dbe4b83b5fdac6f714ef432`.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Levels/DirectProcedural/CompetitorCoreSkeletonWholePlanV6/` - generated V6 LevelDefinition assets using approved V4 skeletons plus whole-plan density.
- Pack order after trace curation: `ccsf_v6_009_ccsf_v4_001_9142301_wp1` (`MediumStructure`), `ccsf_v6_011_ccsf_v4_001_9142301_wp3` (`MediumStructure`), `ccsf_v6_021_ccsf_v4_008_9170994_wp5` (`LocalEasy` boundary).
- Static band: coverage `0.740-0.752`, `78-95` chains, openers `7-9`, avg/max choices `3.01-3.59/7-10`; first two rows are the intended human-review rows.
- Official trace: `3/3` solved, missing/failed `0`; process `1 A + 2 B`, tight `Drop` for all, hardStructureV3Class `2 MediumStructure + 1 LocalEasy`.
- Boundary: V6 is the current viewable complete-route pack, not final trace-hard parity. It improves visual fullness over V5 while showing the next issue: background density must become structural to preserve tight process.

## Competitor Core Skeleton Closure V5 Formal Pack - 2026-07-02

- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Packs/DirectProcedural/CompetitorCoreSkeletonClosureV5Pack.asset` - current `competitor-hard-fresh` Demo-mounted formal closure pack, GUID `1ac88c4d9ac345b189443d3d88644021`.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Levels/DirectProcedural/CompetitorCoreSkeletonClosureV5/` - generated LevelDefinition assets from accepted V4 skeleton anchors using overall planned closure.
- Current rows: `ccsf_v5_005_ccsf_v4_001_9142301_cl1`, `ccsf_v5_008_ccsf_v4_001_9142301_cl4`, and `ccsf_v5_002_ccsf_v4_009_9175093_cl2`.
- Static band: coverage `0.644-0.647`, `55-61` chains, openers `3-4`; closure adds `123-135` cells with recorded tail/detour/field-chain split.
- Official trace: `3/3` solved, all process/tight `A/A`; hardStructureV3Class is `1 TrueHardCandidate + 1 HardPotential + 1 MediumStructure`. Best row is `ccsf_v5_005_ccsf_v4_001_9142301_cl1`.
- Boundary: V5 is the first formal accepted-skeleton closure pack and should be reviewed visually/playably, but it is not final `0.9` competitor-density parity. Next density route must plan high-coverage capacity earlier rather than late-closing V4.

## Generated-Root WBP f014 Native Large-Root Review - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GeneratedRootWBPV12_f014NativeLargeRootReviewPack.asset` - current sgp-rhythm-lab Demo-mounted 3-level f014 review pack, GUID `f014b16c0dec4c2b9d9a5eed00000004`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/f014_native_largeframe_root_bcl_v1/` - native `23x30` generated-root continuation assets, not padded `19x26` levels.
- Pack order: `f014f_c038_native23x30_bcl8_ownercap_c001` (`0.6579710`, solved `A/A`, `MediumStructure`), `f014g_c038_native23x30_bcl8x2_ownercap_c001` (`0.6927536`, solved `A/A`, `MediumStructure`, current-layer boundary heads `0`), and `f014d_c036_native23x30_bcl8x3_ownercap_c001` (`0.7289855`, solved `B/B`, `LocalEasy`, current-layer boundary heads `0`).
- Boundary: f014 is a native large-root route proof and human review pack, not a production-complete `0.9+` pack. Fourth-layer rootlang rows become boundary-heavy and should not be promoted.

## Generated-Root WBP t158 Player-Stall Preserving Closure / c027 Open-Anchor Ceiling - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t158_playerstall_preserve_closure_v1/` - diagnostic LevelDefinition output dir for c027 player-stall preserving closure probes. No Demo pack was mounted in this checkpoint.
- `t158f_c027_c005_psclosure_open5_095` - best stricter opener review row. Coverage `0.8886640`, official solved `B/B`, `MediumStructure`, openers `5`, max choices `5`, PlayerStallPass `0.790`; use for human feel review when opener budget should stay at 5.
- `t158g_c027_c005_psclosure_open12_init6_095` - open-anchor capacity probe. Coverage `0.9028340`, official solved `B/B`, openers `6`, max choices `6`, PlayerStallPass under `--max-openers 6`; useful as the parent of t158m, not the cleanest review row.
- `t158m_c027_c005_psclosure_after_open12_noopen_095` - highest coverage c027 WBP row so far under the new player-stall acceptance. Coverage `0.9068826`, official solved `B/B`, openers `6`, max choices `6`, PlayerStallPass `0.780`, DifficultyVerify `HardPotential 0.677`; official HardStructure is still `LocalEasy`, so this is not final target success.
- Boundary: t158 proves the user's "few easy openers but can stall the player" criterion is feasible and raises c027 beyond the previous `0.8502` ceiling. It also fixes the current open-anchor closure ceiling around `0.90-0.91`; reaching `0.95+` still needs earlier whole-board capacity planning.

## Generated-Root WBP t157 Player-Stall / c027 BCL Ceiling - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t156_early_closure_duty_materializer_v1/` - also contains t157e continuation assets from the same c027 early-closure route.
- `t156s_c027_ecduty_bcl4x7_c005` and `c006` - strongest pre-extension player-stall rows at coverage `0.8421053`. They have openers `3`, max choices `4`, long mid-game 1-2 choice windows, and PlayerStall scores `0.906-0.921`.
- `t157e_c027_ecduty_bcl2_from_stall005_c001-c006` - exact2 continuation from `t156s_c005`, coverage `0.8502024`. Top traced rows all pass PlayerStall; c001 is the best official-structure compromise (`MediumStructure`, remote choke `12`, max choices `4`), c005 is the strongest player-stall row (`0.906`).
- No Demo pack was mounted for t157 in this checkpoint. These are diagnostic assets for choosing the next closure operator or a review pack later.

## Generated-Root WBP t156 Early Closure / c027 Capacity Recovery - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t156_early_closure_duty_materializer_v1/` - LevelDefinition output dir for t156 early-closure materializer and subsequent BCL continuations. These are diagnostic candidates, not final review-pack levels.
- `t156c_c027_ecduty_v1_noowner_len6_c001` - first useful early-closure materializer row from c027 seed-only, coverage `0.6801619`, Greedy solved. It commits four closure duties before later coverage.
- `t156o_c027_ecduty_bcl4x6_c001-c008` - intermediate BCLx6 candidates at coverage `0.8259109`. Top4 trace sample (`t156q`, selected by trace Read sorting) is `4/4` official solved, process/tight `B/B`, max choices `5`, and DifficultyVerify `HardPotential` under the intermediate B/0.82 gate.
- `t156s_c027_ecduty_bcl4x7_c001` - current best audited coverage endpoint: coverage `0.8421053`, Greedy `2.267/4`, space debt `12`, openRay `6`, raw options in the preceding BCL wave `14`. Use as the next base for bundle-aware early closure V2, not as a final level.
- `t156v_c027_bcl4x7_ecduty2_c001` - V1 second materializer boundary from t156s; adds `0` chains. Keep as evidence that remaining top duties require multi-chain bundled closure rather than another single-chain V1 pass.
- No Demo pack was mounted for t156 in this checkpoint. Existing Demo mount may still point to an older review pack unless explicitly changed later.

## Generated-Root WBP t155 Root Plan / Closure Duty Front-End - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t155_rootplan_viability_smoke/` - diagnostic LevelDefinition output dir for c027 seed-only and single-chain visibility probes. These are not final review-pack candidates.
- `t155e_c027_seedonly_oldstate_c001-c004` - c027 root plus old t142 seedState only, written with default-off empty-wave mode. Coverage `0.6397-0.6417`, Greedy solved, max choices `6-7`. Use as compatibility/debt audit base, not a full level.
- `t155h_c027_seedonly_bcl_cov1_visibility_c001-c012` - one-chain BCL visibility candidates from t155e c001. They prove BCL has some local option supply, but do not address the highest-priority closure duties.
- No Demo pack was mounted for t155. Current t155 result is a planner-front-end boundary: c027 is the best same-frame root to continue, but full generation needs a duty-specific early closure materializer before generic BCL waves.

## Generated-Root WBP f013 Positive Anchor Big Canvas Probe - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GeneratedRootWBPV12_f013BigCanvasAnchorReviewPack.asset` - current Demo-mounted 6-level comparison pack, GUID `f013b16c0dec4c2b9d9a5eed00000003`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/f013_positive_anchor_bigcanvas/` - generated f013 LevelDefinition assets. Important rows: `f013a_t103a_anchor_pad20x27_raw`, `f013b_t103a_f012f_pad20x27_raw`, `f013c_t103a_f012f_pad20x27_newstrip095`, `f013d_t103a_f012f_pad20x27_open095`, `f013g_t103a_f012f_pad20x27_continue_open095`, and `f013h_t103a_f012f_pad20x27_continue_open095_proxy`.
- Pack order: original `t114_generalization_probe_t103a_c001_fill090c`, raw padded `f013a`, f012 closure `f012f`, padded f012 closure `f013b`, new-strip fill `f013c`, relaxed open fill `f013d`.
- Official trace `f013_positive_anchor_bigcanvas_trace_metrics.csv`: original 6-row comparison solved. Later `f013g` and `f013h` closure traces also solve and reach `0.9500000` / `0.9518519`, but official process/tight remains `B/Drop`, `hardStructureV3Class=LocalEasy`, and DifficultyVerify rejects them.
- Boundary: this is a human-feel and closure-feasibility probe, not a Generated-Root WBP target success. It proves coverage can be closed mechanically, but late open/proxy fill does not preserve official hard structure.

## Competitor Core Skeleton Fill V2 Density Proof - 2026-07-02

- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Packs/DirectProcedural/CompetitorCoreSkeletonFillV2Pack.asset` - current fresh-worktree Demo pack, GUID `2e80e2ed17b9429cacd6bef0b20aaf6f`.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Levels/DirectProcedural/CompetitorCoreSkeletonFillV2/` - generated from-scratch V2 levels using V1 core skeleton plus DAG-preserving density fill.
- Current rows: `ccsf_v2_004_8033498`, `ccsf_v2_038_8172864`, `ccsf_v2_023_8111379`, `ccsf_v2_033_8152369`, `ccsf_v2_014_8074488`, and `ccsf_v2_009_8053993`.
- Static band: `54-74` chains, `0.608-0.662` coverage, opener count `3-5`, density filler adds `17-26` chains per row.
- Official trace: 6/6 solved; hardStructureV3Class is `5 MediumStructure / 1 LocalEasy`; process/tight is `1 A/A + 5 B/B`. Best review row: `ccsf_v2_014_8074488` (`A/A`, `MediumStructure`, remote choke `9`, after-local frontier break `2`).
- Boundary: this is the current correct-route proof and can be reviewed visually, but it is not a final competitor-equivalent hard pack. Next pack should push toward higher density with trace-aware filler selection and exclude LocalEasy rows after trace.

## Competitor Core Skeleton Fill V3 Batch High-Coverage Proof - 2026-07-02

- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Packs/DirectProcedural/CompetitorCoreSkeletonFillV3Pack.asset` - current fresh-worktree Demo pack, GUID `7075082cc6954667adbce6b54809ddb8`.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Levels/DirectProcedural/CompetitorCoreSkeletonFillV3/` - rank-field batch high-coverage LevelDefinition assets.
- Current rows: `ccsf_v3_002_8614300_se`, `ccsf_v3_008_8638894_ne`, and `ccsf_v3_003_8618399_sw`.
- Static band: `128-134` chains, `0.874-0.901` coverage, `112-118` bulk density chains, internal openers `6-7`, avg/max choices `3.69-4.73/6-10`.
- Official trace: 3/3 solved, but all `LocalEasy`; process/tight classes are `B/Drop`, `Drop/Drop`, and `B/B`.
- Boundary: V3 answers the coverage/fill question and should be inspected as a high-density visual proof, not as hard success. Next playable hard candidate should add batch-level remote/cross-basin duties before final trace.

## Competitor Core Skeleton Formal V4 - 2026-07-02

- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Packs/DirectProcedural/CompetitorCoreSkeletonFormalV4Pack.asset` - current fresh-worktree Demo pack, GUID `00adefdd18574cc19297db8ad4659bef`.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Levels/DirectProcedural/CompetitorCoreSkeletonFormalV4/` - generated formal skeleton-anchor LevelDefinition assets.
- Current rows: `ccsf_v4_009_9175093`, `ccsf_v4_001_9142301`, and `ccsf_v4_008_9170994`.
- Static band: coverage `0.460-0.526`, `45-55` chains, openers `4`, internal avg/max choices `1.89-2.65/4-6`, remote/cross first-hit counts up to `24/40`.
- Official trace: 3/3 solved; `ccsf_v4_001_9142301` is `TrueHardCandidate` with `A/A`; the other two rows are `MediumStructure`.
- Boundary: this is the current formal hard-skeleton anchor, not final competitor visual density. Next formal pack should add coverage while preserving this trace profile.

## Competitor Core Skeleton Fill V1 Proof - 2026-07-02

- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Packs/DirectProcedural/CompetitorCoreSkeletonFillV1Pack.asset` - 6-level from-scratch core-skeleton proof pack. It is no longer the current Demo mount after V2, but remains the low-coverage mechanism proof.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Levels/DirectProcedural/CompetitorCoreSkeletonFillV1/` - generated levels from opener/core chains plus dependency-preserving fill. No seed topology is reused.
- Current rows: `ccsf_v1_026_7504576`, `ccsf_v1_024_7496378`, `ccsf_v1_015_7459487`, `ccsf_v1_063_7656239`, `ccsf_v1_014_7455388`, and `ccsf_v1_109_7844793`.
- Official trace: 6/6 solved and all process/tight `A/A`; `ccsf_v1_026_7504576` is `TrueHardCandidate`, five rows are `MediumStructure`.
- Boundary: coverage is only `0.430-0.527`, so this is a structure proof, not final competitor visual parity. Next pack must raise density with a second-stage filler while preserving the DAG/trace structure.

## Competitor Seed Maze Grammar V1 Diagnostic - 2026-07-02

- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Packs/DirectProcedural/CompetitorSeedMazeGrammarV1Pack.asset` - 3-level visual-positive seed-grammar diagnostic pack, mounted in `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/Scenes/Demo.unity`.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Levels/DirectProcedural/CompetitorSeedMazeGrammarV1/` - generated levels from true-reference seed grammar: `csmg_v1_230_seed_aout_level_089_rot180_7534162`, `csmg_v1_091_r1_ab_109_above300_level_606_final_rot270_7393911`, and `csmg_v1_173_seed_aout_level_089_anti_transpose_7476649`.
- Visual metrics: coverage `0.908-0.917`, `55-88` chains, dense maze-room panels with corridors/rooms/folds; this is much closer to true competitor contact sheets than ShortRailRead/Gatehouse/Braid.
- Official trace: 3/3 solved, but all remain `LocalEasy`; process is `B/Drop/Drop`. Treat as visual grammar proof only, not a hard candidate pack.
- Boundary: user rejected this route as seed-derived. Keep it as a diagnostic negative only; the active route is core-skeleton bottom-up generation.

## Competitor Hard True Reference Correction - 2026-07-02

- Positive visual references remain the contact sheets listed below, not the user's colored short-rail image. True target: dense maze-room panels with nested rooms, corridors, gates, pockets, U-turns, and non-striped high coverage.
- `.codex-run/competitor_true_reference_seed_profile_top.csv` and `.codex-run/competitor_true_reference_seed_profile_top6_preview.png` - seed-reference scan/preview for grammar extraction. Use these as analysis references, not direct final level reuse.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Packs/DirectProcedural/CompetitorShortRailReadV1Pack.asset` - rejected negative. It was derived from the user's negative short-rail screenshot and should not be opened as a candidate.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Packs/DirectProcedural/CompetitorMazeRoomReadV1Pack.asset` - rejected/diagnostic. Current braid-room output is low-choice but visually mechanical stripe/ladder, not competitor maze-room structure.
- Boundary: do not mount or present ShortRailRead, MazeRoomRead V1, or SeedMazeGrammar V1 as accepted review packs. Next playable candidate should come from core-skeleton bottom-up generation with density fill.

## Competitor Hard Level Analysis - 2026-07-02

- `Exports/HardLevelCompetitorAnalysis_20260702/competitor_hard_level_landing_plan_v1.md` - Analysis/report for producing competitor-like hard levels. It maps base Arrow rules, current generator/trace capabilities, screenshot-derived visual targets, and a recommended `CompetitorMazeReadDemandV1` lane.
- Reference screenshots used by the analysis: `TempContactSheets/Arrowz_36.png`, `TempContactSheets/Korean_401_1000_complete_36.png`, `TempContactSheets/save_family_ypJOy09GT0I__36.png`, `TempContactSheets/AOut_relocated_clear_202_36.png`, `TempContactSheets/Above_300_Merged_2026052_36.png`, `TempContactSheets/all_even_36.png`.
- Boundary: this is a planning/report artifact only; no new playable LevelDefinition, LevelPack, or Demo mount was produced.

## Competitor Hard ScheduledBreak Loose12 Tryout - 2026-07-02

- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureReadDemandV1ScheduledBreakLoose12Pack.asset` - 4-level isolated ScheduledBreak Loose12 tryout pack, GUID `cef3a830c4aa0bb41ba718b083d77103`; rejected by human review because it reused already-rejected material and did not satisfy the requested shape/feel.
- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Levels/DirectProcedural/RDSBL12/` - LevelDefinition assets for the 4 accepted Loose12 rows.
- Rejected row: `rdsbl12_01_sgp_pressure_hard_rect_read_demand_v1_scheduled_break_loose_sbl12_lock_buckle_b1_01.asset`; despite official trace process `A`, avg/max choices `4.58/10`, openers `4`, remote choke count `4`, after-local frontier break `1`, user review says it is not a valid positive because the source/feel was already rejected.
- Other rows: `rdsbl12_05...lock_buckle_b1_05`, `rdsbl12_10...core_burst_b1_10`, and `rdsbl12_11...dense_weave_b1_11`; all are solved but process `Drop` and still `LocalEasy`.
- Report: `Exports/HardLevelCompetitorAnalysis_20260702/competitor_hard_tryout_scheduled_break_loose12_v1.md`.
- Boundary: this pack is negative/reference only. Do not use it as a positive specimen or generator-side promotion target.

## Competitor Hard Fresh Gatehouse Rejected Negative - 2026-07-02

- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Packs/DirectProcedural/CompetitorMazeReadDemandV1FreshPack.asset` - rejected 4-level fresh Gatehouse pack; do not use as a current review target or positive sample.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Levels/DirectProcedural/CompetitorMazeReadDemandV1/` - rejected generated LevelDefinition assets: `cmrd_gatehouse_v1_02_seed7302203`, `cmrd_gatehouse_v1_04_seed7302405`, `cmrd_gatehouse_v1_05_seed7302506`, `cmrd_gatehouse_v1_01_seed7302102`.
- Current source metrics: `52` chains, `230-233` arrows, coverage `0.348-0.353`, openers `4-5`, internal avg/max choices `4.46-5.46/8-9`, max chain `5-7`.
- Light official trace on top row `cmrd_gatehouse_v1_02_seed7302203`: solved, process/tight `B/B`, avg/max choices `4.23/6`, choice P80 `6`, local/near/same-region runs `4/4/1`, HardStructure V3 `LocalEasy`.
- Human rejection: this does not match competitor result, logic, or structure. The `LocalEasy` trace classification is not the problem; it is the correct rejection signal here.
- Boundary: do not tune this family forward. Next route should derive a visual/logic grammar from competitor screenshots and project seeds before generating again.

## HoleMask Direct Nutation Blocker Probe - 2026-07-02

- `F:\Unityproject\ArrowLevel-Hand-HoleExperiment\Assets\ArrowMagic\SOData\Packs\Production\HoleMask\HoleMask_DirectNutationHoleProbe.asset` - 5-level correct-route probe pack, mounted to experiment `Assets/ArrowMagic/Scenes/Demo.unity`. It is direct constrained Nutation-style generation with fixed center hole as real `blockIndices`, not Nutation seed crop/refill.
- `F:\Unityproject\ArrowLevel-Hand-HoleExperiment\Assets\ArrowMagic\SOData\Levels\Production\HoleMask\DirectNutationHoleProbe\` - current Top5 level assets only; stale unreferenced probe assets were pruned.
- Top5 rows: `n40x32` chains `136` fill `1175/1208`; `n36x30` chains `112` fill `989/1008`; `n40x32` chains `144` fill `1177/1208`; `n32x28` chains `97` fill `806/824`; `n36x30` chains `113` fill `989/1008`.
- Technical gates passed for Top5: Greedy solved, `holeHits=0`, edge straight run `3-4`, axis same-dir run `7-9`, initial clearable chains `14-18`.
- Report: `F:\Unityproject\ArrowLevel-Hand-HoleExperiment\Assets\ArrowMagic\Reports\Production\HoleMask\HoleMask_DirectNutationHoleProbe_Report.csv`; summary: `HoleMask_DirectNutationHoleProbe_Summary.txt`.
- Boundary: human visual review still decides whether this direct blocker route is usable. Keep the earlier `HoleMask_NutationTerrainProbe.asset` as a rejected false-positive reference, not a production source.

## Generated-Root WBP f005-f006 Safe-Prefix Coverage Layer - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/f005d_safe_prefix_bcl_cov3_len3_nogreedy_probe_*` - negative diagnostic: raw BCL geometry can combine and shows low choices, but all rows are Greedy unsolved deadlocks.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/f005f_safe_prefix_bcl_cov8_len3_singlegreedy_smoke_*` and level dir `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/f005f_safe_prefix_bcl_cov8_len3_singlegreedy_smoke/` - first safe BCL layer after f004d. Top4 official trace solved; best process row stayed `A/A` at coverage `0.6753623`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/f006a_bcl2_*` and level dir `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/f006a_bcl2/` - second safe BCL layer. Top4 official trace `4/4 A/A`, coverage `0.7087-0.7101`, localPatch `3-4`, nearOuter `2`, max choices `8`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/f006c_bcl3half_*` and `f006e_bcl4half_loctouch2_*` - phased half-layer probes. f006e is the best current f-line boundary: top4 trace has two `A/A` rows at coverage `0.7449275`, localPatch `4`, nearOuter `2`, dependencyFollow `6`, max choices `7`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/f006b_bcl3_*`, `f006d_bcl4half_*`, `f006f_bcl5half_loctouch2_*`, and `f006g_bcl5mini_loctouch2_*` - boundary/negative pushes. They remain solved but fall to `B/B` or `LocalEasy` as coverage approaches/exceeds `0.75`, usually through localPatch/dependencyFollow growth.
- Boundary: current best playable candidates to inspect are `f006e_bcl4half_loctouch2_c001` and `c002`. Do not treat f006g as a breakthrough despite higher coverage.

## Generated-Root WBP f002-f004 Demand-Carrier Safe Prefix - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/f002a_demand_carrier_chainseed_reject_audit_summary.md` / `_audit.csv` / `_option_audit.csv` - read-only rejection audit of f001 two-chain demand-carrier bundles. Strict result: `6` pre-release owner blockers and `6` Greedy unsolved; relaxed release-impact result: `12` Greedy unsolved.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/f003a_demand_carrier_single_seed_plan.csv` / `_summary.md` - compiled `17` single-carrier seed rows from `t139a_runbreak_demand_smoke_carrier_profile.csv`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/f003b_demand_carrier_single_reject_audit_summary.md` / `_audit.csv` / `_option_audit.csv` - single-carrier audit. Only `CLT97060` accepted (`7->11`, cells `5,17;4,17;4,16;3,16;2,16;1,16;0,16;0,17;0,18`).
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/f004_safe_single_carrier_seed_smoke/` and report prefix `f004a_safe_single_carrier_seed_smoke_*` - V12 validation that the accepted single carrier can be materialized as a chain-plan seed state.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/f004_safe_single_carrier_extension_smoke/` and report prefix `f004b_safe_single_carrier_extension_smoke_*` - safe single carrier plus one V12 extension. Official light trace `3/3` solved, process/tight `A`, max choices `8`, localPatch `3`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/f004_safe_single_carrier_extension3_relaxed_smoke/` and report prefix `f004d_safe_single_carrier_extension3_relaxed_smoke_*` - exact 3 added chains with relaxed same-release-owner diversity. V12 emitted `8` candidates; top4 official trace `4/4` solved, `A/HardPotential`, coverage about `0.641`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/f004_safe_single_carrier_extension4_relaxed_smoke/` and report prefix `f004e_safe_single_carrier_extension4_relaxed_smoke_*` - exact 4 relaxed smoke. It produced `0` candidates; current V12 generic extension is capped by cell overlap plus Greedy unsolved after 3 chains.
- Boundary: f004d is a mechanism-positive low-coverage sample, not a production candidate. Next useful route is demand duty / seedState planning or smarter safe-prefix expansion, not another forced direct carrier bundle pass.

## Generated-Root WBP f001 Demand-Carrier Seed Smoke - 2026-07-02

- Naming rule: this side-line uses `f###` prefixes so it does not collide with the main Generated-Root WBP `t###` sequence being advanced in another conversation.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/f001a_demand_carrier_seed_plan.csv` / `_summary.md` - compiled 12 disjoint 2-chain demand-carrier seed bundles from `t139a_runbreak_demand_smoke_carrier_profile.csv`; main edges are `7->11`, `54->11`, `10->4`, `12->4`, and `12->6`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/f001b_demand_carrier_chainseed_smoke_summary.md` / `_profile.csv` / `_planned_relations.csv` - V12 chain-plan-seed smoke using f001a seeds. Result: `0` candidates; chain-plan seed states remain `0`, with rejection summary `blocks_pre_release_owner=6;greedy_unsolved=6;row_reconstructed_option=24;rows=24`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/f001b_demand_carrier_chainseed_smoke/` - smoke output directory; no playable LevelDefinition candidate was accepted.
- Boundary: f001 is a side-line diagnostic showing that t139 demand overlap falls on `CARRIER_B1_TO_CHOKE` options rather than the old `STATE_FRONTIER_B1_TO_B2` hardbase pattern. Direct carrier insertion is not enough; the next f-line attempt needs pre-release-safe parent/delay pairing or a demand carrier seed mode that accounts for prereq owners.

## SGP Read-Demand Region Frontier Replay V1 Outputs - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_read_demand_v1_scheduled_break_region_replay_metrics.csv` / `_steps.csv` - two-level official trace rerun with step diagnostics for `rdsb_03` and `rdsb_11`.
- `.worktrees/read-demand-hardening/.codex-run/sgp_read_demand_region_frontier_replay_v1_scheduled_break_compare_summary.csv` - RegionFrontierReplay summary comparing break signal and continuity risk.
- `.worktrees/read-demand-hardening/.codex-run/sgp_read_demand_region_frontier_replay_v1_scheduled_break_compare_windows.csv` - ranked 4-step replay windows with low-choice, hard-break, remote-narrow, retained-frontier, and local-run fields.
- `.worktrees/read-demand-hardening/.codex-run/sgp_read_demand_region_frontier_replay_v1_scheduled_break_compare_steps_enriched.csv` - step-level enriched replay rows.
- Boundary: these are read-only diagnostics, not production keep outputs. Current conclusion is that both sampled ScheduledBreak rows have real break signals but still carry continuity risk.

## SGP Read-Demand CardPoint Review5 - 2026-07-02

- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureReadDemandV1ChokeMutationV2ReviewPack.asset` - current mounted 5-level read-demand review pack in `.worktrees/read-demand-hardening/Assets/ArrowMagic/Scenes/Demo.unity`; GUID `7f869d001abf3da438cff99f4b369b8d`. It was first used for CardPoint/remote-choke Review5 and is now overwritten with `Choice Value V2 Review (5)`.
- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_read_demand_v1_choke_mutation_v2_review_report.csv` - current pack manifest with 5 loaded `LevelDefinition` refs.
- `.worktrees/read-demand-hardening/.codex-run/sgp_pressure_read_demand_v1_cardpoint_review5_keep.csv` - selected rows for the focused pack; chosen from official trace rows with `frontierDrainRemoteChokeCount` evidence.
- `.worktrees/read-demand-hardening/.codex-run/sgp_pressure_read_demand_v1_cardpoint_review5_summary.md` - metric summary for the 5 selected rows.
- `.worktrees/read-demand-hardening/.codex-run/sgp_pressure_read_demand_v1_choice_value_v2_review_review_keep.csv` - current ChoiceValue V2 review keep; selected by useful branch tension, counterfactual divergence, meaningful option rate, and low local-only/flat-consequence risk.
- `.worktrees/read-demand-hardening/.codex-run/sgp_pressure_read_demand_v1_choice_value_v2_review_summary.md` - ChoiceValue V2 summary and selected-row metrics.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_read_demand_v1_choice_value_v2_review5_diag_metrics.csv` / `_steps.csv` - official trace plus step diagnostics for the current V2 review pack.
- Source trace metrics remain `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_read_demand_v1_choke_mutation_v2_drainbreak2_metrics.csv`.
- Boundary: this is a visual/playtest proof pack for the read-demand/card-point/choice-value route, not a production keep pack and not a PSG/Nutation replacement yet.

## RDSB03 Micro-Region Mutation Review6 - 2026-07-02

- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureReadDemandV1ChokeMutationV2ReviewPack.asset` - current mounted 6-level review pack in `.worktrees/read-demand-hardening/Assets/ArrowMagic/Scenes/Demo.unity`. Order: original `rdsb_03`, previous `c19`, previous `c19+c25`, micro top `sgp_rdcm_v2rp_r03_04_rgp10b199n219_c19`, micro second `sgp_rdcm_v2rp_r05_05_rfp55_h25n75`, guard visual control `sgp_rdcm_v2rp_r08_01_rgp21b394n374`.
- `.worktrees/read-demand-hardening/.codex-run/codex_try_rdsb03_micro_region_review6_keep.csv` - manifest copied to the builder's fixed `sgp_pressure_read_demand_v1_choke_mutation_v2_trace1_review_v1_review_keep.csv` before building the current review pack.
- `.worktrees/read-demand-hardening/.codex-run/codex_try_rdsb03_micro_region_review6_compare.csv` - compact metrics table for the 6 mounted levels. Best row is the micro top with strict `HardPotential 0.710`, process `A/A`, avg/max choices `3.68/7`, low2 `0.290`, but still `LocalEasyStructure`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/codex_try_rdsb03_micro_region_top16_metrics.csv` and `.worktrees/read-demand-hardening/.codex-run/codex_try_rdsb03_micro_region_top16_difficulty_verify_v1_strict.csv` - official trace and strict DifficultyVerify for top16 micro-region candidates plus baselines.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/codex_try_rdsb03_micro_region_review6_metrics.csv` / `_steps.csv` - step diagnostics for mounted review6.
- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Levels/DirectProcedural/SGPPressureReadDemandV1ChokeMutationV2RepairPlan/` - generated micro-region repair LevelDefinition assets. Treat these as experiment outputs, not production candidates yet.

## HoleMask HighChain 100-150 Candidates - 2026-07-02

- `F:\Unityproject\ArrowLevel-Hand-HoleExperiment\Assets\ArrowMagic\SOData\Packs\Production\HoleMask\HoleMask_HighChain_100To150_Candidates.asset` - 15-level large fixed-hole candidate pack. Specs are `36x30_standard`, `38x30_wide`, `40x30_wide`, `40x32_standard`, and `42x32_wide`, 3 candidates each; accepted chain range `102-128`.
- `F:\Unityproject\ArrowLevel-Hand-HoleExperiment\Assets\ArrowMagic\SOData\Levels\Production\HoleMask\HighChain100To150\` - candidate LevelDefinition assets produced by the seed-mask crop + GreedyRescue route.
- `F:\Unityproject\ArrowLevel-Hand-HoleExperiment\Assets\ArrowMagic\Reports\Production\HoleMask\HoleMask_HighChain_100To150_Candidates_Report.txt` - generation report with preview seeds, accepted attempts, fill, chain counts, and block-hit checks.
- `F:\Unityproject\ArrowLevel-Hand-HoleExperiment\Assets\ArrowMagic\Masks\Production\HoleHighChain\` - generated fixed-hole masks for the high-chain batch; all use the same `8x9` center hole and vary outer shell dimensions.
- Boundary: this pack is for visual/manual review of 100-150 chain HoleMask feasibility. It was not mounted to Demo during the recorded run.

## Generated-Root WBP t137 Bridge OK 3 Demo Pack - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GeneratedRootWBPV12_t137BridgeOk3Pack.asset` - Demo-mounted 3-level review pack for accepted t137 bridge-safe outer-breaker rows `r004/r005/r009`; GUID `5f9445b481a94fd8a2cb66dc5388c137`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` - activePack now points to the t137 Bridge OK 3 pack. This is for visual/playtest review, not a new t139 candidate.

## Generated-Root WBP t139 Run-Break Readiness Audit - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t139_overall_runbreak_readiness_audit_summary.md` - Overall t139 conclusion: t138c demand connects to V12 scoring, but the current hardbase seed pattern still uses old early-owner edges and produces `0` candidates.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t139_overall_runbreak_readiness_audit.csv` - Joined audit for t137 accepted levels, t138 contracts/demand, t139 planner smoke, and option-demand overlap.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t139a_runbreak_demand_smoke_summary.md` / `_profile.csv` / `_carrier_profile.csv` - Small V12 smoke with t138c demand and nonzero penalties on generated root `geosupply_sched_root10_from_40eb0da7_r1_c038`; demand loaded, `65/372` options overlapped demand, but seed states/candidates stayed `0`.
- No new playable LevelDefinition or pack was generated in t139. Next useful gate is a demand-seeded frontier/reservation smoke with nonzero seed states before full candidate generation.

## Generated-Root WBP t138 Generation-Side Run-Break Plan - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t138_generation_side_runbreak_plan_summary.md` - main t138 conclusion: move remaining right/outer run-breaking into pre-cut whole-board contracts; keep accepted `9>24` delay, avoid stacked `79>40`, and plan cross-basin/frontier break around `67>79` or `40>9`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t138a_t124b_base_runbreak_plan_runs.csv` / `_contracts.csv` / `_summary.md` - t124b r006 baseline run-break plan; top window steps `22-35` and first contract `9>24` as `planned_delay_break`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t138b_t137a_bridge_ok_runbreak_plan_runs.csv` / `_contracts.csv` / `_summary.md` - accepted t137a bridge-safe rows; `9>24` no longer appears as a contract anchor, remaining risk anchors are `67>79` and `40>9`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t138c_t137a_bridge_ok_runbreak_cell_demand.csv` / `_summary.md` - run-break contracts converted to V12 `cell_demand` rows for the next whole-board planner pass.
- No new playable LevelDefinition or pack was generated in t138; this is a planning/reporting checkpoint for the next generator pass.

## Generated-Root WBP t137 Bridge-Locked Outer Break Probe - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t137_outer_bridge_lock_probe_summary.md` - main t137 conclusion: accept single bridge-locked `9->24` breaker as a diagnostic baseline improvement; reject stacked upstream `79->40` breaker.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t137a_t124b_outer_bridge_lock_prblock_manifest.csv` / `_summary.md` - 16 generated bridge-lock candidates from t124b r006; all diagnostic outputs.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t137a_t124b_outer_bridge_lock_prblock_bridge_ok3_full_metrics.csv` / `_steps.csv` / `_relation_audit_summary.md` - accepted 3-candidate full trace/audit set. `r004/r005/r009` preserve `58->85->83->68->72->28`, reject `60->28`, and improve nearOuter `4->3`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t137b_t124b_double_outer_bridge_lock_manifest.csv`, `_full_metrics.csv`, `_full_relation_audit_summary.md` - negative double-breaker follow-up; solved and bridge-safe, but no local/nearOuter improvement and remote choke signal drops.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t137_outer_bridge_lock_probe/` - diagnostic t137 LevelDefinition assets; not a final accepted production pack.

## Generated-Root WBP t136 Outer Conveyor Break Probe - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t136_outer_conveyor_break_probe_summary.md` - Main t136 conclusion: outer/right-side conveyor should be the next hardening focus, but the breaker must be relation-locked to preserve the root bridge/victim contract.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t136a_t129a_outer_guard_manifest.csv` / `_summary.md`, `t136b_t129a_outer_guard_loose_manifest.csv` / `_summary.md`, `t136c_t126n_outer_guard_manifest.csv` / `_summary.md`, `t136d_t126j_outer_guard_manifest.csv` / `_summary.md` - late local-run guard probes; all produced `0` candidates.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t136e_t129a_outer_prblock_manifest.csv`, `_metrics.csv`, `_steps.csv`, `_relation_audit_summary.md` - t129a parent-release blocker probe. Inserts `9->82->24`, official solved, improves local/nearOuter to `6/6`, but loses bridge-victim quality.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t136f_t124b_outer_prblock_manifest.csv`, `_metrics.csv`, `_steps.csv`, `_relation_audit_summary.md` - t124b parent-release blocker probe. `r002` improves nearOuter `4->3` but weakens `72->28`; `r001` preserves `72->28` but does not improve nearOuter.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t136_outer_conveyor_break_probe/` - diagnostic t136 LevelDefinition assets; none are accepted final continuations yet.

路径以仓库根目录为基准。这里记录关卡、包、报告、掩码和配置入口，不复制资源内容。

## Primary Data Roots

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `Assets/ArrowMagic/SOData/Levels/` | 关卡 asset 根目录 | 查找 LevelDefinition、候选、生产关卡 |
| `Assets/ArrowMagic/SOData/Packs/` | LevelPack 根目录 | 查找 campaign/生产包引用 |
| `Assets/ArrowMagic/SOData/Reports/` | 生成/验证报告根目录 | 查找候选评分、优化轮次、验收报告 |
| `Assets/ArrowMagic/Masks/` | mask 图片和批次根目录 | 形状掩码、生产掩码、实验 mask |
| `Assets/ArrowMagic/SOData/Palettes/` | 调色板配置 | 视觉主题、颜色配置 |
| `Assets/ArrowMagic/SOData/SfxLibraries/` | 音效配置 | 音效库引用 |
| `Assets/ArrowMagic/SOData/VfxLibraries/` | VFX 配置 | 特效库引用 |

## Production and Campaign

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `Assets/ArrowMagic/SOData/Levels/Production/` | 生产关卡根 | 交付、正式包、生产候选核对 |
| `Assets/ArrowMagic/SOData/Levels/Production/Front20Polish/` | 前 20 polish 生产关卡 | 前段体验/难度 polish |
| `Assets/ArrowMagic/SOData/Levels/Production/HoleMask/` | HoleMask 生产候选 | hole mask 正式候选和 early 批次 |
| `Assets/ArrowMagic/SOData/Levels/Production/HoleProcedural/` | HoleProcedural 生产候选 | hole procedural 正式候选 |
| `Assets/ArrowMagic/SOData/Levels/Production/HoleLongOuterStrong/` | HoleLongOuterStrong 生产候选 | 长外圈强约束候选 |
| `Assets/ArrowMagic/SOData/Packs/Production/` | 生产 pack 根 | 正式包引用和交付检查 |
| `Assets/ArrowMagic/SOData/Reports/Campaign500/` | Campaign500 报告根 | campaign 优化、baseline、shape pass、外部主题报告 |
| `Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/` | 2026-06-18 Campaign500 优化轮次报告 | 排查当天优化结果和 shape refresh 批次 |
| `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardReview6Pack.asset` | PSG normal 6 关普通小批 review 包 | 人工审查普通 PSG baseline 小批链条语言和体感 |
| `Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_hard_review6_report.csv` | PSG normal Review6 source report | 查看 6 关 baseline 小批 coverage/chains/source status |
| `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardInterference6Pack.asset` | PSG normal 近邻同向扰动 6 关实验包 | 对比 Review6，人工审查“同方向/近距离箭头扰动”是否改善链条语言 |
| `Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_hard_interference6_report.csv` | PSG normal Interference6 source report | 查看扰动小批 coverage/chains/source status；需配合 trace 判断 choices 是否爆 |
| `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardInterferenceV2SixPack.asset` | PSG normal 动态 flow-spread 6 关实验包 | 对比 Interference6，人工审查“同区/同轴连续消除 run”是否被打散 |
| `Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_hard_interference_v2_six_report.csv` | PSG normal Flow-spread V2 source report | 查看 FlowRun/FlowJump/FlowNear source status；需配合 trace joined 表判断 keep |
| `.codex-run/psg_pressure_interference_v2_six_flow_20260627_joined.csv` | PSG normal Flow-spread V2 joined audit | 6/6 solved、6 B、source coverage `0.975-0.984`、5/6 keep；第 5 关 `maxChoices=11` 需人工特例或排除 |
| `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardInterferenceV3SixPack.asset` | PSG normal region-flow V3 6 关实验包 | 诊断 head-region/同区连续消除调度；当前 0/6 keep，不作为量产候选线 |
| `Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_hard_interference_v3_six_report.csv` | PSG normal region-flow V3 source report | 查看 V3 HeadFlow source coverage/status；第 4 关 coverage `0.968` 已低于普通门槛 |
| `.codex-run/psg_pressure_interference_v3_six_20260627_joined.csv` | PSG normal region-flow V3 joined audit | 6/6 solved，但 5 Drop + 1 B、maxChoicesMax `15`、普通 keep `0/6`；仅作负例/诊断 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/` | SGP 节奏/难度实验报告 | 查看静态节奏分类、真实过程 choice curve、伪深度风险和 process keep 候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadOrientationPreviewPack.asset` | PressureRead 结构化 demo pack | 查看远依赖/低选择/结构化直链小批 demo；Demo 场景当前挂此包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PressureReadOrientation/` | PressureRead 结构化候选关卡 | 查看通过头尾方向重组生成的候选 LevelDefinition |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockPreviewPack.asset` | PressureRead 阶段门锁实验 pack | 查看最新一次 StageLock 脚本输出；该包可能被后续试跑覆盖 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockCurated5Pack.asset` | PressureRead 阶段门锁精选 5 关稳定包 | 上一版稳定预览包；用于对比依赖合链前后的难关方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockDepMerge5Pack.asset` | PressureRead 阶段门锁依赖合链精选 5 关 | 上一版依赖合链稳定包；用于对比 BalancedDepMerge6 的产率和质量 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockBalancedDepMerge6Pack.asset` | PressureRead 平衡源排序 + 依赖合链精选 6 关 | 上一版稳定难关预览包；用于对比 LoopMerge160 的产率和质量 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockLoopMerge160Pack.asset` | PressureRead 多轮依赖合链 160 源精选 11 关 | 可覆盖目录时期的 11 关包；用于对比 Frozen 包，不建议作为当前稳定 demo 入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockLoopMerge160FrozenPack.asset` | PressureRead LoopMerge160 冻结稳定包 | 当前 Demo 挂载的稳定 11 关包；关卡已复制到独立目录，后续试跑不会覆盖 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockSourceEnhanced7Pack.asset` | PressureRead 温和源增强 7 关冻结包 | 源增强前置中长链后的实验结果包；trace 为 S=6/A=1，用于评估源语言前置方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockDepAware67Pack.asset` | PressureRead 依赖感知源增强 5 关冻结包 | `DependencyAware` 源增强后的高质量难关分支；trace 为 S=4/A=1，结构链更强但产能仍低 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockTargetedDepAware68Pack.asset` | PressureRead targeted DepAware 12 关候选包 | targeted 源 feed + DependencyAware 后的完整候选包；trace 为 S=11/A=1，用于人工看图和继续筛选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockTargetedDepAware68HardSelectedPack.asset` | PressureRead targeted DepAware 8 关精选硬关包 | trace 后按 family/base 限制精选；适合评估更接近正式入池的难关质量 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockTargetedDepAwareMergedHard10Pack.asset` | PressureRead targeted DepAware 多切片 10 关硬关池 | 合并第一切片精选 8 关和第二切片精选 2 关后的多 family 硬关池 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockLongBias5Pack.asset` | PressureRead StageLock 复杂长链 5 关硬关样本 | `HighYield/ReferenceLong + LongChainBias` 的去重小样包；用于人工评估更复杂长链难关是否成立 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction15Pack.asset` | PressureRead StageLock 15 关硬关生产候选池 | 合并 targeted hard、LongBias 和 symmetry expansion 后的当前最大去重硬关候选池 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction16V2Pack.asset` | PressureRead StageLock 16 关硬关生产候选池 v2 | 在 HardProduction15 基础上加入 HighYield 尾部 symmetry 补充后的当前最大去重硬关候选池 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction23V3Pack.asset` | PressureRead StageLock 23 关硬关生产候选池 v3 | 当前最佳硬关候选池；由 HardProduction16V2 加成功 enhanced source 二级自举扩产后合并去重得到 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction31V4BootstrapPack.asset` | PressureRead StageLock 31 关硬关自举候选池 v4 | 比 V3 更硬但 family 更集中；用于评估 hard-production 上限和同源风险 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction33V5PreviewPack.asset` | PressureRead StageLock 33 关硬关候选池 v5 preview | V4Bootstrap 加 highchain success bootstrap 严格结构去重补充后的预览包；用于评估更复杂长链补充 lane |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockStageDoorAdditions5Pack.asset` | PressureRead StageDoor 源增强 5 关对照包 | HardMidWide 与 minority success lane 生成的高质量 StageDoor 变体；用于看图评估同父本替换和少数 family 补强，不作为去重主池 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockStageDoorLane9Pack.asset` | PressureRead StageDoor 少数结构 9 关补强包 | minority success 源经 symmetry + StageDoor 后合并出的 dense/maze/section/lock 补强池；当前 Demo 已挂此包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockStageDoorPipeline10Pack.asset` | PressureRead StageDoor symmetry pipeline 10 关包 | 可复用流水线输出的 10 关 review pool；比 Lane9 多一个 highchain 长链样本，当前不作为 Demo 默认入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockStageDoorProduction24Pack.asset` | PressureRead StageDoor symmetry 24 关生产池 | V3 proven source 经 symmetry + StageDoor 产出的当前最强可重复生产池；当前 Demo 已挂此包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction45V6StageDoorStrictPack.asset` | PressureRead StageLock hard production 45 关 V6 strict 池 | V5Preview + StageDoorProduction24 合并后严格筛选的上一版主审查包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockReferenceComplexLong7Pack.asset` | PressureRead StageLock ReferenceComplex 7 关复杂长链补充包 | ReferenceComplex 小切片精选出的低产高质量补充样本；用于看复杂长链语言是否成立 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction52V7RefComplexLongPack.asset` | PressureRead StageLock 52 关 V7 复杂长链硬关池 | Strict45 + ReferenceComplexLong7 合并后的当前 V7 demo 审查包；Demo 场景已挂此包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardMidChainProbe5V1Pack.asset` | PressureRead StageLock 5 关中型链组 probe 包 | V7 中 `chains>=40` 的样本加 1 个新 47 链 HardMidWide 样本；用于验证 40-55 链、低选择、长结构链方向，不是最终量产包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_loopmerge160_notes.md` | LoopMerge160 结果说明 | 查看 160 源产率、trace 指标、当前瓶颈和下一步 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_source_profile_notes.md` | StageLock 源画像复盘 | 查看 SGP 源与外部 298 seed 的结构差距、画像排序结论和入口 root merge 实验结论 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_source_enhanced7_notes.md` | StageLock 源增强 7 关复盘 | 查看暴力预合链失败、温和预合链可行、下一步方向/依赖感知源增强结论 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_depaware67_notes.md` | StageLock 依赖感知源增强复盘 | 查看 DependencyAware 源增强的产率、trace、结构指标和下一步吞吐瓶颈 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_targeted_depaware68_notes.md` | StageLock targeted DepAware 复盘 | 查看 targeted feed 如何把 DepAware 产率从 5/67 提升到 12/68，以及 8 关精选标准 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_targeted_depaware_merged_hard10.csv` | StageLock targeted DepAware 多切片硬关池 CSV | 当前 10 关 merged hard pool 的冻结输入，含 trace 和 family 去重字段 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_longchain_bias_notes.md` | StageLock LongChainBias 复盘 | 查看 `HighYield`、`ReferenceLong`、`LongChainBias` 对产率、过程曲线和复杂长链结构的影响 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_longchain_bias_merged5.csv` | StageLock LongChainBias 5 关 CSV | `LongBias5Pack` 的冻结输入，已按 base/family 去重 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_symmetry_expansion_notes.md` | StageLock 高产源几何扩展复盘 | 查看 symmetry source expansion 对产率、trace 和最终硬关池的影响 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_pool.csv` | StageLock 15 关硬关生产候选池 CSV | `HardProduction15Pack` 的冻结输入，已按 source hash 和 family 去重 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_pool_v2.csv` | StageLock 16 关硬关生产候选池 v2 CSV | `HardProduction16V2Pack` 的冻结输入，已按 source hash 和 family 去重 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_pool_v3.csv` | StageLock 23 关硬关生产候选池 v3 CSV | `HardProduction23V3Pack` 的冻结输入，指标更硬，包含成功 enhanced source 二级自举新增候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_pool_v4_bootstrap.csv` | StageLock 31 关硬关自举候选池 v4 CSV | `HardProduction31V4BootstrapPack` 的冻结输入；更硬但 lock/section/dual family 占比高 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v3_notes.md` | StageLock HardProduction23V3 结果说明 | 查看 V3 指标、成功源自举扩产路线、unprofiled/随机救援负结果和下一步 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_pool_v5_preview.csv` | StageLock 33 关硬关候选池 v5 preview CSV | `HardProduction33V5PreviewPack` 的冻结输入，含 highchain strict 补充候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_stage_door_additions5.csv` | StageDoor 源增强 5 关对照 CSV | `StageDoorAdditions5Pack` 的冻结输入，family mix 为 section/lock/maze/dense；全部真实 trace S |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_stage_door_lane9.csv` | StageDoor 少数结构 9 关补强 CSV | `StageDoorLane9Pack` 的冻结输入；dense_weave=4、maze_long_chain=2、section_unlock=2、lock_buckle=1 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_stage_door_pipeline10.csv` | StageDoor symmetry pipeline 10 关 CSV | `StageDoorPipeline10Pack` 的冻结输入；Lane9 加一个 highchain long-chain review 样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_stage_door_production26.csv` | StageDoor symmetry 24 关生产池 CSV | `StageDoorProduction24Pack` 的冻结输入；文件名保留 26 目标但实际 source-hash 去重后为 24 关 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v5_plus_stagedoor_prod24_cap10.csv` | V5 + StageDoorProduction24 合并 47 关 CSV | `MaxPerFamily=10` 的合并池，V5=30、StageDoor=17；strict45 的基础输入 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v5_plus_stagedoor_prod24_cap10_strict45.csv` | V5 + StageDoorProduction24 strict 45 关 CSV | `HardProduction45V6StageDoorStrictPack` 的冻结输入；规则为 `traceMaxChoices<=8` 且 `traceStageLockScore>=0.60` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_reference_complex_long7.csv` | StageLock ReferenceComplex 7 关复杂长链补充 CSV | `ReferenceComplexLong7Pack` 的冻结输入；由 ReferenceComplex 小切片精选合并而来 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v7_strict45_plus_refcomplex_long.csv` | StageLock V7 52 关硬关池 CSV | `HardProduction52V7RefComplexLongPack` 的冻结输入；Strict45 加 7 个复杂长链补充样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v7_refcomplex_long_notes.md` | StageLock V7 ReferenceComplex 长链复盘 | 查看 ReferenceComplex preset、负结果、7 关补充包和 V7 52 关指标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_midchain_probe_v1.csv` | StageLock 中型链组 5 关 probe CSV | `HardMidChainProbe5V1Pack` 的冻结输入，链组范围 `41-53` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_midchain_probe_notes.md` | StageLock 中型链组 probe 复盘 | 查看 `MinOutputChains`、40/36 链底线实验、47 链候选指标和下一步 StageDoor gate 语义瓶颈 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_stage_door_minority_sym2_v1_selected_hard.csv` | StageDoor minority symmetry 精选 CSV | minority success 源经 symmetry + StageDoor 后精选出的 4 个 dense/maze S 级候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_sdsym_v3head12_r1_selected_hard.csv` | StageDoor V3 proven head12 精选 CSV | V3 proven head12 经 StageDoor symmetry 选出的 8 个 lock/section 高硬度候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_sdsym_v3tail11_r1_selected_hard.csv` | StageDoor V3 proven tail11 精选 CSV | V3 proven tail11 经 StageDoor symmetry 选出的 8 个 dual/section/maze 候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_sdsym_minority_r1_selected_hard.csv` | StageDoor symmetry pipeline minority 回归精选 CSV | 由 `Run-StageLockStageDoorSymmetrySlice.ps1` 自动复现出的 4 个 minority selected，用于验证流水线 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_sdsym_highchain_r1_selected_hard.csv` | StageDoor symmetry pipeline highchain 精选 CSV | highchain success feed 经 StageDoor symmetry 后仅精选 1 个，说明高链路线可用但低产 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_true_hard_mass_notes.md` | StageLock 真正硬关量产复盘 | 查看参考 top40 过程画像、HardXL 负结果、highchain bootstrap 正结果、V5Preview 指标和下一步 StageDoorSGP 方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PressureReadStageLock/` | PressureRead 阶段门锁候选关卡 | 查看阶段计划和合链后处理生成的候选 LevelDefinition |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PressureReadStageLockLoopMerge160Frozen/` | LoopMerge160 冻结关卡目录 | 当前 Demo 11 关的独立 LevelDefinition；避免可覆盖实验目录破坏稳定包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PressureReadStageLockSourceEnhanced/` | StageLock 源增强候选目录 | 存放轻量中长链源增强后的中间源 asset；供 StageLock 使用，不是最终关卡 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PressureReadStageLockSourceEnhanced7Frozen/` | StageLock 源增强 7 关冻结目录 | `SourceEnhanced7Pack` 对应的最终候选关卡 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PressureReadStageLockDepAware67Frozen/` | StageLock 依赖感知源增强 5 关冻结目录 | `DepAware67Pack` 对应的最终候选关卡 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PressureReadStageLockTargetedDepAware68Frozen/` | StageLock targeted DepAware 12 关冻结目录 | `TargetedDepAware68Pack` 对应的完整候选关卡 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDA68Hard/` | StageLock targeted DepAware 8 关精选冻结目录 | `TargetedDepAware68HardSelectedPack` 对应的严格精选关卡，目录名较短用于避开 Windows 路径长度 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAMerge10/` | StageLock targeted DepAware merged hard 10 关冻结目录 | `TargetedDepAwareMergedHard10Pack` 对应的多切片精选关卡 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDALongBias5/` | StageLock LongChainBias 5 关冻结目录 | `LongBias5Pack` 对应的复杂长链硬关样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction15/` | StageLock 15 关硬关生产候选冻结目录 | `HardProduction15Pack` 对应的当前最大去重硬关候选池 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction16V2/` | StageLock 16 关硬关生产候选冻结目录 | `HardProduction16V2Pack` 对应的当前最大去重硬关候选池 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction23V3/` | StageLock 23 关硬关生产候选冻结目录 | `HardProduction23V3Pack` 对应的当前最佳硬关候选池 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction31V4Bootstrap/` | StageLock 31 关硬关自举候选冻结目录 | `HardProduction31V4BootstrapPack` 对应冻结关卡；看图时重点检查同源感 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction33V5Preview/` | StageLock 33 关硬关候选冻结目录 | `HardProduction33V5PreviewPack` 对应冻结关卡；包含 highchain strict 补充样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAStageDoorAdditions5/` | StageDoor 源增强 5 关冻结目录 | `StageDoorAdditions5Pack` 对应冻结关卡；用于与 V5Preview 对照视觉和手感 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAStageDoorLane9/` | StageDoor 少数结构 9 关冻结目录 | `StageDoorLane9Pack` 对应冻结关卡；当前 Demo 验证入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAStageDoorPipeline10/` | StageDoor symmetry pipeline 10 关冻结目录 | `StageDoorPipeline10Pack` 对应冻结关卡；包含一个 highchain 长链补充样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAStageDoorProduction24/` | StageDoor symmetry 24 关生产池冻结目录 | `StageDoorProduction24Pack` 对应冻结关卡；当前 Demo 验证入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction45V6StageDoorStrict/` | V6 StageDoor strict 45 关冻结目录 | `HardProduction45V6StageDoorStrictPack` 对应冻结关卡；上一版 Demo 主审查入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAReferenceComplexLong7/` | ReferenceComplex 7 关复杂长链冻结目录 | `ReferenceComplexLong7Pack` 对应冻结关卡；用于单独审查复杂长链补充样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction52V7RefComplexLong/` | V7 52 关复杂长链硬关池冻结目录 | `HardProduction52V7RefComplexLongPack` 对应冻结关卡；当前 Demo 审查入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardMidChainProbe5V1/` | 中型链组 5 关 probe 冻结目录 | `HardMidChainProbe5V1Pack` 对应冻结关卡，用于专门审查 40+ 链低选择方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardMidGateTight3V8/` | V8 MidGate 3 关中型链组突破样本 | `HardMidGateTight3V8Pack` 对应冻结关卡；验证 `StageGateSearch + MinOutputChains>=36` 后的紧曲线/强长链小包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction55V8ProbeMidGate/` | V8Probe55 复杂长链硬关审查池 | `HardProduction55V8ProbeMidGatePack` 对应冻结关卡；`V7 52 + MidGate 3` 合并审查包，当前 Demo 审查入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDARefComplexSalvage3V9/` | V9 RefComplex salvage 3 关冻结目录 | `RefComplexSalvage3V9Pack` 对应冻结关卡；从已生成 refcomplex candidates 中严格回收的低选择补充样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction58V9ProbeRefSalvage/` | V9Probe58 复杂长链硬关审查池 | `HardProduction58V9ProbeRefSalvagePack` 对应冻结关卡；`V8Probe55 + RefComplexSalvage3` 合并审查包，当前 Demo 审查入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAAllHistorySalvage8V10/` | V10 all-history salvage 8 关冻结目录 | `AllHistorySalvage8V10Pack` 对应冻结关卡；从历史 StageLock candidates 中排除 V9 source hash 后严格回收的补充样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction66V10ProbeAllHistorySalvage/` | V10Probe66 复杂长链硬关审查池 | `HardProduction66V10ProbeAllHistorySalvagePack` 对应冻结关卡；`V9Probe58 + AllHistorySalvage8` 合并审查包，当前 Demo 审查入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction68V11ProbeHardMidWideDBStageGate/` | V11Probe68 中型长链硬关审查池 | `HardProduction68V11ProbeHardMidWideDBStageGatePack` 对应冻结关卡；`V10Probe66 + hardmid_wide DoorBalanced StageGate 2` 合并审查包，当前 Demo 审查入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction72V12ProbeHardMidWideDBMicro/` | V12Probe72 中型长链硬关审查池 | `HardProduction72V12ProbeHardMidWideDBMicroPack` 对应冻结关卡；`V11Probe68 + hardmid_wide DoorBalanced micro 4` 合并审查包，当前 Demo 审查入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction75V13HardMidWideAuto/` | V13Probe75 自动源筛中型长链硬关审查池 | `HardProduction75V13HardMidWideAutoPack` 对应冻结关卡；`V12Probe72 + hardmid_wide auto feed 3` 合并审查包，当前 Demo 审查入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_profile.csv` | SGP 源结构画像 CSV | 给 StageLock 源排序使用的 `longChainRate/structureCarrierRate/complexChainScore` 等结构指标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_gate_v2_profile.csv` | SGP 源结构 + gate potential 画像 CSV | 新增 `gatePotentialScore/gateLateRegionCount/gateCandidateCount` 等门锁潜力诊断；当前只能辅助筛源，不能单独预测出货 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_stage_lock_targeted.csv` | StageLock targeted 源 feed CSV | 由 `Build-StageLockTargetedSourceFeed.ps1` 输出，供 DepAware 源增强优先扫描 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_stage_lock_highyield.csv` | StageLock HighYield 源 feed CSV | 严控源开放度的高命中源 feed；适合主难关量产验证 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_stage_lock_reference_long.csv` | StageLock ReferenceLong 源 feed CSV | 参考 298 seed 中长链画像的低产高复杂补充 feed |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_stage_lock_reference_complex.csv` | StageLock ReferenceComplex 源 feed CSV | 参考 298 seed top complex 画像的复杂长链补充源 feed；低产精品 lane，不作为主产线 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_stage_lock_hardxl.csv` | StageLock HardXL 大源诊断 feed CSV | 直接挑 70+ 链大源测试后处理上限；当前结论为 0 产出，不作为默认量产入口 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_stage_lock_hardmid.csv` | StageLock HardMid 源 feed CSV | 45-90 链中等规模源，StageDoor 小批已产出 3 个 S 候选；用于验证比 HardXL 更可控的大一点硬关母体 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_stage_lock_hardmid_wide.csv` | StageLock HardMidWide 源 feed CSV | 放宽后的 40-95 链源 feed，StageDoor 小批已产出 section/lock 候选；用于补同父本替换样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hm36gate_stagegate30_tight_selected_hard.csv` | V8 MidGate 3 关精选 CSV | `StageGateSearch` 在 `GateStrong` 中链源上的 36+ 链突破样本，已 trace 全部 `S/Low` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v8_probe55_with_midgate.csv` | V8Probe55 合并 CSV | `V7 52 + V8 MidGate 3` 合并审查池，平均 `traceAvgChoices=3.428`、`traceMaxChoices=6.018`、`avgChain=12.111` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_refcomplex_salvage_selected_hard.csv` | V9 RefComplex salvage 3 关精选 CSV | 从 refcomplex candidates 中排除 V8 已有 hash 后回收的 3 个补充样本，trace 全部 `S/Low` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v9_probe58_refsalvage.csv` | V9Probe58 合并 CSV | `V8Probe55 + RefComplexSalvage3` 合并审查池，平均 `traceAvgChoices=3.418`、`traceMaxChoices=6.086`、`avgChain=12.016` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v9_refcomplex_salvage_notes.md` | V9 RefComplex salvage 复盘报告 | 查看 salvage filter、trace 结果、精选结果和 V9 汇总指标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_allhistory_salvage_v10_selected_hard.csv` | V10 all-history salvage 8 关精选 CSV | 扫描历史 StageLock candidates、排除 V9 source hash 后精选的 8 个补充样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v10_probe66_allhistorysalvage.csv` | V10Probe66 合并 CSV | `V9Probe58 + AllHistorySalvage8` 合并审查池，平均 `traceAvgChoices=3.438`、`traceMaxChoices=6.136`、`avgChain=11.956` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v10_allhistory_salvage_notes.md` | V10 all-history salvage 复盘报告 | 记录 196 行历史 candidates -> 42 trace 成功 -> 8 strict hard selected -> V10Probe66 的流程和指标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v11_probe68_hardmidwide_db_stagegate.csv` | V11Probe68 合并 CSV | `V10Probe66 + hardmid_wide DoorBalanced StageGate 2` 合并审查池，平均 `traceAvgChoices=3.434`、`traceMaxChoices=6.147`、`longChainRate=0.483` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v12_probe72_hardmidwide_db_micro.csv` | V12Probe72 合并 CSV | `V11Probe68 + hardmid_wide DoorBalanced micro 4` 合并审查池，平均 `traceAvgChoices=3.441`、`traceMaxChoices=6.181`、`chains avg=33.819` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v13_probe75_hardmidwide_auto.csv` | V13Probe75 合并 CSV | `V12Probe72 + hardmid_wide auto feed 3` 合并审查池，75 关，平均 `traceAvgChoices=3.447`、`traceMaxChoices=6.213`、`chains avg=33.987`、`longChainRate avg=0.486` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v11_hardmidwide_db_stagegate_notes.md` | V11 hardmid_wide DoorBalanced 复盘报告 | 记录 DoorBalanced 比 GateStrong 更适合 hardmid_wide 微切片，新增 37/40 链真实 S 候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v12_hardmidwide_db_micro_notes.md` | V12 hardmid_wide DoorBalanced micro 复盘报告 | 记录新增微切片脚本、existing next6 feed、4 个 36+ 链新增样本和 V12Probe72 指标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_source_structure_stage_lock_hardmid_wide_micro_auto_v13.csv` | V13 hardmid_wide 自动微切片源 feed | 由 `Build-HardMidWideMicroSourceFeed.ps1` 生成，排除 V12 next6 后选出 15 个可试父源，供 DoorBalanced micro-slice 使用 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v13_hardmidwide_auto_notes.md` | V13 hardmid_wide 自动源筛复盘报告 | 记录 auto feed、auto00/02 正结果、auto04/06/08/10 负结果、V13Probe75 指标和下一步 StageDoor trace 预筛方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_door_source_minority_success_v1.csv` | StageDoor 少数 family 成功源增强 feed | 从 `sweep/maze/dense/zig` 等少数成功源做 StageDoor 变体，已产出 maze/dense S 候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_lock_source_symmetry_minority_sd_sym2.csv` | StageDoor minority symmetry 源 feed | 7 个 minority success 源经 FlipX/FlipY/Rot180 生成的 21 个 symmetry source；注意需用显式数组传 transforms |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_door_source_minority_sym2_v1.csv` | StageDoor minority symmetry 源增强 feed | `stage_lock_source_symmetry_minority_sd_sym2.csv` 再经 StageDoor 生成的 39 个合法源 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_lock_source_symmetry_sdsym_minority_r1.csv` | StageDoor symmetry pipeline minority 源 feed | 新流水线自动生成的 21 个 minority symmetry source |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_door_source_sdsym_minority_r1.csv` | StageDoor symmetry pipeline minority 增强源 feed | 新流水线自动生成的 39 个合法 StageDoor source |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_lock_source_symmetry_sdsym_highchain_r1.csv` | StageDoor symmetry pipeline highchain 源 feed | 12 个 highchain success 源经 symmetry 生成的 36 个 source |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_door_source_sdsym_highchain_r1.csv` | StageDoor symmetry pipeline highchain 增强源 feed | highchain symmetry 再经 StageDoor 生成的 72 个合法源；最终仅产 1 个精选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_lock_source_symmetry_sdsym_v3head12_r1.csv` | StageDoor V3 proven head12 symmetry feed | V3 proven head12 生成的 36 个 symmetry source |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_door_source_sdsym_v3head12_r1.csv` | StageDoor V3 proven head12 增强源 feed | V3 proven head12 symmetry 再经 StageDoor 生成的 72 个合法源 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_lock_source_symmetry_sdsym_v3tail11_r1.csv` | StageDoor V3 proven tail11 symmetry feed | V3 proven tail11 生成的 33 个 symmetry source |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_door_source_sdsym_v3tail11_r1.csv` | StageDoor V3 proven tail11 增强源 feed | V3 proven tail11 symmetry 再经 StageDoor 生成的 63 个合法源 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_lock_success_highchain_source_feed_v1.csv` | StageLock 高链成功源自举 feed CSV | 从历史高链成功候选反查 sourcePath，用于 highchain symmetry/bootstrap 补充复杂长链样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_sym_highchain12_selected_hard_sig1.csv` | StageLock highchain strict 结构去重精选 CSV | `sym_highchain12` 候选按 `MaxPerStructureSignature=1` 精选出的 2 个复杂长链补充 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_ArchitecturalLineworkSGP01_ProbePack.asset` | ArchitecturalLinework constructive probe 包 | 建筑线稿反解式构造实验当前 Demo 包；Constructive02 为 3 关，链条 19-25、coverage 0.44-0.48、真实 trace 全部 S |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/architectural_linework_constructive02_report_20260620.md` | ArchitecturalLinework Constructive02 报告 | 记录建筑迷宫方向的切入点、指标、当前缺口和下一步 density/依赖环突破计划 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SGPBuildingArchlikePresentable5Pack.asset` | SGP-only 建筑线稿可看 probe 5 关包 | 从 643 个既有 SGP 源按建筑画像 + Trace + 视觉筛选冻结；Demo 当前挂载该包，作为纠偏后的可看方向样本 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_building_archlike_presentable5_frozen_trace_metrics.csv` | SGP-only 建筑线稿可看 probe trace 指标 | 查看 5 关冻结后真实过程曲线；结果为 A=3/B=2、missing=0 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_lock_source_symmetry_highyield_head12.csv` | StageLock HighYield 头部几何扩展源 feed | 由 `Build-StageLockSymmetrySourceVariants.ps1` 生成的 36 个 Flip/Rot 源变体 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_lock_source_symmetry_symhyrestbias.csv` | StageLock HighYield 尾部几何扩展源 feed | 由 `Run-StageLockSymmetryExpansionSlice.ps1` 生成的 57 个尾部 Flip/Rot 源变体，低产但补充了 1 个入池候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_lock_source_enhanced_targeted_depaware_slice02.csv` | StageLock targeted DepAware 第二切片源增强 CSV | 第二切片 `SourceOffset=40` 的增强源输出；用于分析后续切片产率 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/ReferenceSeeds/ArrowForge298/` | 外部 298 seed 结构参考副本 | 仅用于结构画像和指标校准，不直接混入正式生产包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/ReferenceSeeds/ArrowForge298Full_20260618_100431/` | 外部 298 seed 完整参考副本 | 从 `G:\Unityproject\ArrowCopy_DependencyGeneratorV0_20260609\ArrowForgePackExports\SingleLevelSeedLevelPacks_ByLevelName_298_20260618_100431` 复制的完整 298 seed，仅用于结构画像/指标校准 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/ReferenceSeeds/ArrowForge298FullLevels_20260618_100431/` | 外部 298 seed LevelDefinition 本体副本 | 从 G 盘源关卡复制的 298 个 LevelDefinition；用于真实过程 trace，不混入正式生成包 |

## Generated and Experimental

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `Assets/ArrowMagic/SOData/Levels/Campaign500Optimization/` | Campaign500 优化候选 | ShapeRefresh、DirectChainFlavor 等候选 |
| `Assets/ArrowMagic/SOData/Levels/Campaign500SingleLevelPool/` | 单关卡候选池 | 单关卡 pool、rejected、筛选任务 |
| `Assets/ArrowMagic/SOData/Levels/DirectProcedural/` | 直接 procedural 候选 | direct/normal/polish/topup 候选 |
| `Assets/ArrowMagic/SOData/Levels/HoleProcedural/` | hole procedural 候选 | hole 候选池、高 chain、topup |
| `Assets/ArrowMagic/SOData/Levels/NoMaskProcedural/` | 无 mask procedural 候选 | 无 mask 风格、类型、预览 |
| `Assets/ArrowMagic/SOData/Levels/LevelImportV1/` | 导入关卡批次 | 导入教程/正式数据排查 |
| `Assets/ArrowMagic/SOData/Levels/Generated/` | 历史/批量生成关卡 | R1/R2、CompositeSeedVariants、MaskPreview 等历史生成 |
| `Assets/ArrowMagic/SOData/Levels/ShapeExperiment/` | shape 实验关卡 | shape candidates、KeepEarly、Deprecated |
| `Assets/ArrowMagic/SOData/Packs/ShapeExperiment/` | shape 实验 pack | shape preview/keep/deprecated pack |
| `Assets/ArrowMagic/SOData/Reports/ShapeExperiment/` | shape 实验报告 | mask contact sheet、catalog、review、retrospective |
| `Assets/ArrowMagic/Masks/ShapeIconMaskOnlyBatch*/` | shape icon mask 批次 | 形状 mask 批量生成和审查；收尾时区分正式/实验 |

## Legacy or Baseline Data

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `Assets/ArrowMagic/SOData/Levels/Early_20_30/` | 早期 20-30 关卡 JSON | 对照早期难度/格式 |
| `Assets/ArrowMagic/SOData/Levels/Normal_40_70/` | normal 40-70 关卡 JSON | 对照 normal 难度/格式 |
| `Assets/ArrowMagic/SOData/Levels/Seeds/` | seed 候选 | R2 final candidate pool、seed 来源排查 |

## Export and Review

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `Exports/` | 交付 zip、xlsx、readme 和导出目录 | 交付检查、打包核对；不要把临时导出误认为源数据 |
| `TempContactSheets/` | 临时 contact sheets | 图片审查、实验收尾清理 |
| `Assets/ArrowMagic/Reports/` | Unity 项目内报告资源 | 生产报告、人工检查材料 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SGPBuildingMazeStageLock2Pack.asset` | SGP-only 建筑迷宫 StageLock 2 关突破包 | 当前 Demo 挂载包；2/2 真实 Trace 为 S，建筑画像源经 StageLock/依赖合链压难度得到，用于验收“神似 + 难度”交集方向 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_building_maze_stage_lock2_frozen_trace_metrics.csv` | SGP-only 建筑迷宫 StageLock2 trace 指标 | 查看当前 2 关冻结后真实过程曲线；`S=2`、`over10Rate=0`、`stageLockScore avg=0.593` |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/building_grammar_interleave_activation_v0_20260620.md` | Building Grammar interleave activation v0 报告 | 查看 GPT dual-spine 建议落地、离线 flip probe 结果、为什么后验 flip 不足以破局，以及下一步 Cross-Potential/source-orientation 前置方向 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/building_grammar_cps_interleave_v1_20260620.md` | Building Grammar CPS Interleave v1 报告 | 查看本轮目标更新、CPS 父源筛、dominance trim、dependency anchor、realized follow-run repair 等连续实验；v167-v170 已确认 `12,2` 可破 h91 follow-run，但 stage-aware pair/triple 批量为 0 selected，当前翻链会过早打开 late gate |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/DependencyFollowRunPairRepair/` | Building Grammar pair repair 导出关卡目录 | `Build-DependencyFollowRunReport.ps1` 导出的 follow-run pair repair LevelDefinition；当前 validated h91 样本仅 `12,2` 真可解 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/depanchor_v164_h91_pairrepair_validated_trace_metrics.csv` | h91 pair repair validated trace | 查看 `12,2` pair repair 的完整 trace：2/2 `S/S`、`avgChoices=2.86`、`maxChoices=4`、`dependencyFollowRunMax=2`、`stageLockScore=0.506` |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stageaware_pairrepair_depanchor_triple_v1_joined.csv` | depanchor stage-aware pair/triple repair 汇总 | v160-v166 depanchor repair 批量 full trace 汇总；10 个 repair 全部因 `stageLock/lateRegions` 等拒绝，证明当前翻链打断会破 late stage |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_D176StrictPack.asset` | DepAnchor v176 strict review pack | Building Grammar lane 第一次 strict 过线 pack；4 关，`dependencyFollowRunMax=2`、`stageLockScore≈0.822`、`lateRegionCount=3` |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/D176Strict/` | DepAnchor v176 strict 冻结关卡目录 | `SGPRhythmLab_D176StrictPack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/depanchor_v176_pairrepair_10_8_strict_selected.csv` | DepAnchor v176 strict selected CSV | 只保留 stage-preserving `10,8` pair repair，排除 `10,2` late-region collapse |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/trace_depanchor_v176_pairrepair_metrics.csv` | DepAnchor v176 pair repair full trace | 验证 `10,8` strict repair 4/4 solved、S/tight S；`10,2` 虽 followRun=2 但 stage collapse |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/depanchor_v177_smoke_summary.md` | DepAnchor strict runner smoke summary | 自动化 runner 小样，4 source -> 2 strict，验证流水线可复现 v176 top hcd5 族 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/depanchor_v178_offset4_summary.md` | DepAnchor strict runner offset4 summary | 后续源 close-but-not-strict 诊断；候选有低选择/高 stage，但没有可解 strict pair repair |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/depanchor_v179_offset12_summary.md` | DepAnchor strict runner offset12 summary | V20 中段源诊断；7 candidates、0 pair repair、0 strict |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/depanchor_v180_offset20_summary.md` | DepAnchor strict runner offset20 summary | V20 后段源诊断；5 candidates、0 pair repair、0 strict |



| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hardmidwide_stage_door_trace_cache_v14c.csv` | V14c HardMidWide StageDoor trace-aware cache | 父源级反馈缓存，23 个 parent rows；用于判断哪些 hardmid_wide 父源已证伪或已出货 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hardmidwide_reference_gap_v14c.csv` | hardmid_wide 与 298 参考 seed 结构差距报告 CSV | 对比 reference298 top complex、SGP hardmidwide、成功/失败父源的 avgChain、longChainRate、veryLongChainRate、straightLikeRate 等指标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v15_productive_refit_notes.md` | V15 productive refit 复盘报告 | 记录 reference-complex parent 负结果、productive refit 正结果、V15 76 关 pack 和下一步方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v15_probe_refit_merge.csv` | V15 productive refit 合并 CSV | V13 75 + productive refit 去重后 76 关 hard candidate pool |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction76V15ProductiveRefitPack.asset` | V15 productive refit 76 关冻结包 | 当前 Demo activePack，新增 1 个强合链 refit hard candidate，供人工审查 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction76V15ProductiveRefit/` | V15 productive refit 冻结关卡目录 | `HardProduction76V15ProductiveRefitPack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-ProductiveRefitStageDoorSourceFeed.ps1` | Productive Refit all-history 源筛脚本 | 以现有 hard pool 为排除集，从历史 StageDoor source 中找未入池且适合二次强合链的源 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_door_source_productive_refit_allhistory_v16.csv` | V16 productive refit all-history feed | 从 44 个 StageDoor source 文件筛出的 24 个未入池源；head12 负、tail12 正 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v16_productive_refit_allhistory_notes.md` | V16 productive refit all-history 报告 | 记录全历史源筛、head/tail 产率、2 个新增 hard candidate 和 V16 pack 指标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v16_probe_refit_allhistory_merge.csv` | V16 productive refit all-history 合并 CSV | V15 76 + all-history productive refit 2，合并后 78 关 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction78V16ProductiveRefitAllHistoryPack.asset` | V16 productive refit all-history 78 关冻结包 | 当前 Demo activePack，新增 2 个 all-history refit hard candidates |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAHardProduction78V16ProductiveRefitAllHistory/` | V16 productive refit all-history 冻结关卡目录 | `HardProduction78V16ProductiveRefitAllHistoryPack` 对应 LevelDefinition 副本 |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction81V17ProductiveRetryPack.asset` | V17 Productive Retry 81 关硬关审查包 | 当前 Demo activePack；V16 78 + productive retry 新增 3 个 S 级中长链 hard candidates |

## Campaign500 Normal Full V1 - 2026-06-30

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Levels/Campaign500NormalFullV1/` | Campaign500 normal full V1 LevelDefinition 候选目录 | 全量 200 normal slot x 3 variants 的 source assets；按 shard 子目录保存。 |
| `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Reports/Campaign500/NormalFullV1/campaign500_normal_full_v1_report.csv` | Campaign500 normal full V1 merged source report | 查看 600 行 source 候选、slot/order、lane、style/chain metadata 和 variant 信息。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_normal_full_v1_metrics.csv` | Campaign500 normal full V1 combined official trace metrics | 6 个 100-row chunk trace 合并结果；600/600 solved，process tier A/B/Drop 见 summary。 |
| `.worktrees/nutation-flow-peel-production/.codex-run/campaign500_normal_full_v1_trace_joined.csv` | Campaign500 normal full V1 source+trace joined audit | 查看每个候选的 trace、style、risk、rank、reject/keep 字段。 |
| `.worktrees/nutation-flow-peel-production/.codex-run/campaign500_normal_full_v1_trace_best_per_slot.csv` | Campaign500 normal full V1 best-per-slot CSV | 200 行，每个 normal slot 选 1 个机器 best，用于 ReviewPack。 |
| `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Reports/Campaign500/NormalFullV1/campaign500_normal_full_v1_review.csv` | Campaign500 normal full V1 review manifest | 200 行 ReviewPack 输入，LongChainProbe 已排除。 |
| `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Reports/Campaign500/NormalFullV1/campaign500_normal_full_v1_production_keep.csv` | Campaign500 normal full V1 machine ProductionKeep manifest | 86 行 trace-order preferred keep，用于候选池审查。 |
| `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Reports/Campaign500/NormalFullV1/campaign500_normal_full_v1_production_strict_keep.csv` | Campaign500 normal full V1 strict ProductionKeep manifest | 70 行更严格质量门槛 keep，用于更干净的投产候选池。 |
| `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500NormalFullV1ReviewPack.asset` | Campaign500 normal full V1 ReviewPack | 200 levels，每个 normal slot 一个 best candidate，适合人工全量审查。 |
| `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500NormalFullV1ProductionKeepPack.asset` | Campaign500 normal full V1 ProductionKeepPack | 86 levels，machine keep 候选池。 |
| `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500NormalFullV1ProductionStrictKeepPack.asset` | Campaign500 normal full V1 ProductionStrictKeepPack | 70 levels，严格质量门槛候选池；不自动挂 Demo。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v17_productive_retry_notes.md` | V17 Productive Retry 复盘报告 | 查看 orientable-history 筛源、strong-chain 负结果、3 个新增候选和下一步 orientation-risk cache 计划 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v17_probe_productive_retry_merge.csv` | V17 Productive Retry 合并 CSV | `HardProduction81V17ProductiveRetryPack` 的冻结输入，81 个去重 hard candidates |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction82V18RiskAwarePack.asset` | V18 RiskAware 82 关硬关审查包 | 当前 Demo activePack；V17 81 + risk-aware clean8 新增 1 个 S/S 中长链 hard candidate |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v18_riskaware_notes.md` | V18 RiskAware 复盘报告 | 查看 risk cache、clean8 小切片、V18 新增样本和当前 source orientability 瓶颈 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v18_probe_riskaware_merge.csv` | V18 RiskAware 合并 CSV | `HardProduction82V18RiskAwarePack` 的冻结输入，82 个去重 hard candidates |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction84V19BaseRiskAwarePack.asset` | V19 BaseRiskAware 84 关硬关审查包 | 当前 Demo activePack；V18 82 + source-hash risk-aware 低 orientation 子集新增 2 个 S/A section_unlock hard candidates |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v19_base_riskaware_notes.md` | V19 BaseRiskAware 复盘报告 | 查看 source-hash risk cache、低 orientation 子集 2 个新增样本和少数 family 0 产负结果 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v19_probe_base_riskaware_merge.csv` | V19 BaseRiskAware 合并 CSV | `HardProduction84V19BaseRiskAwarePack` 的冻结输入，84 个去重 hard candidates |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockHardProduction88V20HMWTraceAwarePack.asset` | V20 HardMidWide trace-aware 88 关硬关审查包 | 当前 Demo activePack；V19 84 + HardMidWide trace-aware 长链补强新增 4 个 S 级候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v20_hmw_traceaware_notes.md` | V20 HardMidWide trace-aware 复盘报告 | 查看 GateLite minority 负结果、HardMidWide trace-aware 正结果、4 个新增长链候选和下一步 chunk prefilter 方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v20_probe_hmw_traceaware_merge.csv` | V20 HardMidWide trace-aware 合并 CSV | `HardProduction88V20HMWTraceAwarePack` 的冻结输入，88 个去重 hard candidates |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_productive_refit_v21_probe_summary.csv` | V21 ProductiveRefit full probe 汇总 CSV | 记录 4 个 trace S 但未过复杂长链 strict select 的候选；可作为后续低选择压力 hard lane 的回收池 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_v21_trace_metrics_calibration_20260620.md` | Hard Lane V2.1 trace 指标校准报告 | 查看 V2.1 窗口化/事件化指标、V20/VeryLong/ProductiveRefit 校准结果、推荐阈值和为什么 V2.1 只能作为 rhythm gate 而不能替代链条结构门槛 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v20_v21metrics_selector_probe.csv` | V20 经 V2.1 rhythm gate 重筛结果 | V20 88 关用 `MaxBoringLinearScore=0.46`、`MinStructuredHardnessV21=0.58` 重筛后保留 73 关；用于分析节奏平铺/结构硬度不足样本，不是正式冻结包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_vlgate_v21metrics_selector_probe.csv` | VeryLong pressure + V2.1 selector probe | 验证 VeryLong 长尾门锁候选在 V2.1 节奏门槛和旧结构门槛下仍能保留；用于后续 HMW/VeryLong 小切片阈值参考 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_v21_nearmiss_catalog_20260620.csv` | Hard Lane V2.1 near-miss 目录 | 收集 `structuredHardnessV21>=0.70`、低 boring、低 maxChoices，但因 `avgChain/longChainRate/carrier` 被拒的节奏硬候选；用于后续 `NearMissChainRescue` 批量验证 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hmw_v20_nearmiss_rescue_breakthrough2.csv` | NearMissRescue breakthrough 2 关 CSV | V20 trace-aware c05/c06 经 `NearMissChainRescue` 推过 strict hard select 的 2 个 section_unlock 样本，均为真实 trace S，`mergedCarrierAbsorbCount=3` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockNearMissRescueBreakthrough2Pack.asset` | NearMissRescue breakthrough 2 关 review pack | 单独冻结的 2 关 review 包；未挂 Demo，未替换 V20，用于后续人工看图或继续合并 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDANearMissRescueBreakthrough2/` | NearMissRescue breakthrough 冻结关卡目录 | `NearMissRescueBreakthrough2Pack` 对应的 LevelDefinition 副本，避免后续 preview 覆盖 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_nearmiss_rescue_v21_budget_breakthrough3.csv` | NearMissRescue budget breakthrough 3 关 CSV | c05/c06 section_unlock 2 关 + ProductiveRefit dual_zone 1 关，均 strict hard selected 且真实 trace S；用于验证预算化 rescue 已跨 family 生效 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockNearMissRescueBudgetBreakthrough3Pack.asset` | NearMissRescue budget breakthrough 3 关 review pack | 单独冻结的 3 关 review 包；未挂 Demo，未替换 V20，用于后续人工看图或作为 source-level rescue prefilter 正样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDANearMissRescueBudgetBreakthrough3/` | NearMissRescue budget breakthrough 冻结关卡目录 | `NearMissRescueBudgetBreakthrough3Pack` 对应的 LevelDefinition 副本，避免后续 preview 覆盖 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/stage_door_source_nearmiss_rescue_prefilter_v1.csv` | NearMissRescue source prefilter v1 | 由 `Build-NearMissRescueSourceFeed.ps1` 从历史 rejected 中筛出的 22 个可救源；用于后续低预算单源 rescue 小批 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_nearmiss_rescue_v21_budget_breakthrough4.csv` | NearMissRescue budget breakthrough 4 关 CSV | breakthrough3 加 prefilter dense h4f810，覆盖 section_unlock/dual_zone/dense，均 strict hard selected 且真实 trace S |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockNearMissRescueBudgetBreakthrough4Pack.asset` | NearMissRescue budget breakthrough 4 关 review pack | 单独冻结的 4 关 review 包；未挂 Demo，未替换 V20，用于验证 source-level prefilter + budget rescue 正路线 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDANearMissRescueBudgetBreakthrough4/` | NearMissRescue budget breakthrough4 冻结关卡目录 | `NearMissRescueBudgetBreakthrough4Pack` 对应的 LevelDefinition 副本，避免后续 preview 覆盖 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_nearmiss_rescue_v22_review5.csv` | NearMissRescue V2.2 Review5 CSV | V2.2 初版精选 5 关；压掉 `choicePeak>=8`，限制外圈近距离/同边顺消，保留复杂长链结构 |

## SGP Rhythm Lab Support Closure V1 Reports

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/support_graph_closure_v1_notes_20260622.md` | Support Graph Closure V1 复盘报告 | 记录 bounded support closure 落地、静态 source gate、StageLock `SupportClosureBias`、planned-deps 假阳性、causal gate 纠偏和当前瓶颈：静态闭包必须保留为真实 solve-order closure |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_outerclean_anchor_supportclosure_v1b_probe40.csv` | SupportClosure source gate 5 源 CSV | `-SupportGraphClosureAnchor` 生成的强静态闭包源；静态分数高，但不能直接视为硬关 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_outerclean_anchor_supportclosure_v1b_probe40_trace_metrics.csv` | SupportClosure source gate trace | 5 个静态闭包源的真实 trace；仅 1/5 solved A，其余 Drop，证明静态 support graph 会在真实解链中塌掉 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_supportclosure_v1b_stagelock_closurebias_candidates.csv` | StageLock planned SupportClosureBias 候选 CSV | planned dependency closure 版本产出的 2 个候选；生成侧看似闭包成立，但 trace 后证明是假阳性 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_supportclosure_v1b_stagelock_closurebias_trace_metrics.csv` | StageLock planned SupportClosureBias trace | 2/2 solved B，但 trace-side closure 分数为 0，仍是 LocalEasy，用于对照 causal gate |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_supportclosure_v1b_stagelock_causal_lowbudget_rejected.csv` | StageLock causal SupportClosureBias 低预算拒绝 CSV | causal closure gate 小批复测；5 源 0 产，`support closure too weak=3`、`local patch burst too linear=2`，证明新 gate 能挡住 planned/static closure 假阳性 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_supportclosure_v1b_stagelock_bridge_diag_rejected.csv` | Support Feasibility Bridge V0 断点诊断 CSV | 5 源 0 产；输出 `supportClosureFailureClass`，定位失败主要为 `upstreamIsConveyor/supportIsConveyor/missingCrossRegionSupport` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_supportclosure_v1b_stagelock_bridge_penalty_rejected.csv` | Support closure failure penalty 复测 CSV | failure class 已参与 StageLock 搜索惩罚，但仍 0 产，证明不是简单选向排序问题 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_supportclosure_v1b_stagelock_preserve_target_rejected.csv` | Preserve anchor target 复测 CSV | 只保留 anchor target 方向后仍 0 产，且全部先被 `dependency follow too linear` 拒绝 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_supportclosure_v1b_stagelock_preserve_support_rejected.csv` | Preserve anchor support 复测 CSV | 同时保留 target 与 hub/support 源层方向后仍 0 产，失败集中 `upstreamIsConveyor=4/supportIsConveyor=1`；下一步应做 upstream support bridge 注入/重接 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_outerclean_anchor_supclosure_bridge_v0.csv` | Strict depth-2 support bridge source probe | 现有 source 生成器加 `HubSupportRequireInitialClearable/ValidateRealized` 后 20 源产 1 个 realized depth-2 support bridge source |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_supportclosure_bridge_v0_stagelock_rejected.csv` | Strict depth-2 support bridge StageLock probe | depth-2 source 接 StageLock 后 0 产，失败为 `upstreamIsConveyor`，说明 support 上游仍退化为 conveyor |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_outerclean_anchor_supclosure_bridge_d3_wide_v0.csv` | Strict depth-3 support bridge source probe | 80 源、3 variants、`SupportClosureMinDepth=3` 仍 0 产；说明当前池缺 realized depth-3 upstream bridge，不能靠筛选解决 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLaneV3SGPNativeLocalPatchReview3Pack.asset` | SGP-native LocalPatch Review3 对照包 | 2026-06-21 纠偏后冻结的 SGP/StageDoor/StageLock 主线对照包；Demo 已挂该包。第 1 关是当前最干净 SGP-native 样本，第 2/3 关保留为顺链/局部顺消对照，不是最终量产包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hardlane_v3_sgp_native_localpatch_review3_frozen_metrics.csv` | SGP-native LocalPatch Review3 冻结 trace | 查看 3 关冻结副本的真实过程指标；用于比较 `dependencyFollowRunMax`、`localPatchSolveRunMax`、`outerExitHeadCount` 与体感难度 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockNearMissRescueV22Review5Pack.asset` | NearMissRescue V2.2 Review5 pack | 当前 Demo activePack；5/5 trace S/S，`avgChoices=3.594`、`maxChoices max=7`、`choicePeakCount max=0` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDANearMissRescueV22Review5/` | NearMissRescue V2.2 Review5 冻结关卡目录 | `NearMissRescueV22Review5Pack` 对应 LevelDefinition 副本，后续试跑不会覆盖 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_nearmiss_rescue_v22_review5_frozen_trace_metrics.csv` | NearMissRescue V2.2 Review5 frozen trace | 冻结包实际展示关卡的真实过程指标；用于确认 Demo 包与 V2.2 gate 一致 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_v22_nearmiss_rescue_review5_notes_20260620.md` | Hard Lane V2.2 Review5 复盘报告 | 记录 V2.2 新指标、筛选阈值、补跑尝试、入选/拒绝原因和下一步非 section family 补强方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_v23_outer_exit_pressure_diagnosis_20260621.md` | Hard Lane V2.3 外出口诊断报告 | 解释 V2.2 为何控不住外出口/边缘连续消除，并记录 Review5 在 V2.3 外圈可见压力 gate 下 0/5 通过的诊断结果 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_nearmiss_rescue_v23_outerexit_retrace_metrics.csv` | Review5 V2.3 外出口 trace | 当前 V2.2 Review5 用 V2.3 新指标重放后的 trace；用于定位 `outerAvailableChoiceMax` 与 `sameSideOuterExitAvailableChoiceMax` 问题 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockV24NoSimpleShellPreview6Pack.asset` | Hard Lane V2.4 无简单外壳预览 6 关包 | 第一阶段验证包：`outerSimpleChainCount=0`、`outerLongStraightChainCount=0`，但动态外圈可见压力仍偏高；用于对比外圈简单壳链是否改善 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_v24_outer_shell_pressure_notes_20260621.md` | Hard Lane V2.4 外圈壳链复盘 | 查看为什么外圈问题不能只靠 V2.3 外出口指标，生成器新增 `OuterShellPressureGate` 做了什么，以及 V2.5 动态外圈压力待办 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v24_no_simple_shell_preview6_frozen_trace_metrics.csv` | V2.4 无简单外壳预览包 trace | 6 关冻结包真实过程指标；确认静态外壳已控住但 `outerAvailableChoiceMax/HeavyStepRate` 仍需继续压 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockV25OuterAvailReview3Pack.asset` | Hard Lane V2.5 动态外圈压力 Review3 包 | 3 关突破样本；普通外圈 heavy 明显下降，仍需继续压同侧外圈和外出口连续解 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDAV25OuterAvailReview3/` | V2.5 动态外圈压力 Review3 冻结关卡目录 | `V25OuterAvailReview3Pack` 对应 LevelDefinition；由 V20 broad source 经 V2.5 gate 和去重后冻结 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v25_outer_avail_review3_trace_metrics.csv` | V2.5 Review3 trace 指标 | 3/3 `S/S`，`outerAvailableHeavyStepRate avg=0.165`，用于对比 V2.4 的普通外圈动态压力 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_v25_outer_available_pressure_notes_20260621.md` | Hard Lane V2.5 动态外圈压力复盘 | 记录 strict gate 0 产、soft gate 正结果、Review3 指标、当前不足和 V2.6 建议 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockV26OuterExitRun2Pack.asset` | Hard Lane V2.6 外出口连续解对照 2 关包 | 2 关外出口 solve-run 控制突破样本；不替代当前 Demo review 包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_v26_outer_exit_run2_trace_metrics.csv` | V2.6 外出口连续解 trace 指标 | 2/2 trace `S`，`outerExitSolveRunMax=1`、`sameSideOuterExitSolveRunMax=1`，但其中一关 `tightProcessTier=A/maxChoices=8` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_v26_outer_exit_and_skeleton_v1_notes_20260621.md` | V2.6 外出口与 Skeleton V1 复盘 | 记录生成侧 outer-exit solve-run gate、V2.6 probe 指标，以及 GPT Skeleton Graph 建议如何收束为 adapter 实验 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockSkeletonGateV1Review2Pack.asset` | Skeleton Gate V1 Review2 2 关包 | 当前 Demo activePack；Skeleton -> StageDoor GateStrong -> StageLock 的突破样本，2/2 trace `S/S`，外出口动态指标已控住 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDASkeletonGateV1Review2/` | Skeleton Gate V1 Review2 冻结关卡目录 | `SkeletonGateV1Review2Pack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_skeleton_gate_v1_review2_frozen_trace_metrics.csv` | Skeleton Gate V1 Review2 frozen trace | 2/2 `S/S`，`avgChoices=2.46/2.78`、`maxChoices=5/5`、`outerExitAvailableChoiceMax=1/1` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_skeleton_gate_v1_review2_notes_20260621.md` | Skeleton Gate V1 Review2 复盘 | 查看 Skeleton-only 失败原因、StageDoor GateStrong 正结果和剩余 `outer simple shell too dominant` 卡点 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockSkeletonGateV1Final6Pack.asset` | Skeleton Gate V1 Final6 6 关包 | 当前 Demo activePack；Review2 卡点修复后的小样最终包，6/6 frozen trace `S/S`，允许少量建筑外壳但动态外圈压力受控 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDASkeletonGateV1Final6/` | Skeleton Gate V1 Final6 冻结关卡目录 | `SkeletonGateV1Final6Pack` 对应 LevelDefinition 副本；包含 15x24 与 16x25 两组尺寸 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_skeleton_gate_v1_final6_frozen_trace_metrics.csv` | Skeleton Gate V1 Final6 frozen trace | 6/6 `S/S`，`avgChoices=2.50-3.08`、`maxChoices=5-6`、`outerStraightRunMax=0`、`outerExitSolveRunMax=0-1` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_skeleton_gate_v1_final6_notes_20260621.md` | Skeleton Gate V1 Final6 复盘 | 查看 `-AllowArchitecturalOuterShell` 的作用、Final6 指标、剩余风险和下一步扩模板计划 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockSkeletonGateV1DenseDepReview2Pack.asset` | Skeleton Gate V1 DenseDep Review2 2 关包 | 当前 Demo activePack；针对 Final6 覆盖不足/依赖不足反馈的高覆盖强依赖小样，2/2 frozen trace `S/S` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDASkeletonGateV1DenseDepReview2/` | Skeleton Gate V1 DenseDep Review2 冻结关卡目录 | `SkeletonGateV1DenseDepReview2Pack` 对应 LevelDefinition 副本，15x24 与 15x25 各 1 关 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_skeleton_gate_v1_dense_dep_review2_frozen_trace_metrics.csv` | Skeleton Gate V1 DenseDep Review2 frozen trace | 2/2 `S/S`，coverage=0.822/0.805，`stageLockScore=0.767/0.927`，`mergedDependencyCount=8/4` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_skeleton_gate_v1_dense_dep_review2_notes_20260621.md` | Skeleton Gate V1 DenseDep Review2 复盘 | 查看高覆盖模式、DenseOuterGuard/StrongDoorBridge 结果、产率问题和下一步 template 扩展方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockRoomDoorSkeletonV2Review3Pack.asset` | RoomDoor Skeleton V2 Review3 3 关包 | 当前 Demo activePack；room/hub/door 语义骨架小样，3/3 process S，2/3 tight S，第三关为 S/A 视觉备选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDARoomDoorSkeletonV2Review3/` | RoomDoor Skeleton V2 Review3 冻结关卡目录 | `RoomDoorSkeletonV2Review3Pack` 对应 LevelDefinition 副本；包含 15x25 与 16x25 样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_roomdoor_skeleton_v2_review3_frozen_trace_metrics.csv` | RoomDoor Skeleton V2 Review3 frozen trace | 3/3 `processTier=S`，2/3 `tightProcessTier=S`，candidate coverage≈0.800，`stageLockScore=0.683-0.835` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_roomdoor_skeleton_v2_review3_notes_20260621.md` | RoomDoor Skeleton V2 Review3 复盘 | 查看 V2 room/door 源层、StageDoor/StageLock 结果、产率问题和下一步 template 扩展计划 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockDependencySkeletonV3Probe2Pack.asset` | Dependency Skeleton V3 Probe2 2 关包 | 当前 Demo activePack；先构造物理依赖骨架再 StageDoor/StageLock 的小样，2/2 true trace `S/S`，avg choices 2.19/2.43 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDADependencySkeletonV3Probe2/` | Dependency Skeleton V3 Probe2 冻结关卡目录 | `DependencySkeletonV3Probe2Pack` 对应 LevelDefinition 副本；用于人工对比 RoomDoor V2 的依赖强度 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_dependency_skeleton_v3_probe2_frozen_trace_metrics.csv` | Dependency Skeleton V3 Probe2 frozen trace | 2/2 `processTier=S`、2/2 `tightProcessTier=S`，`stageLockScore=0.867-0.879`，`outerExitSolveRunMax=1` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_dependency_skeleton_v3_probe2_notes_20260621.md` | Dependency Skeleton V3 Probe2 复盘 | 查看 V3 源生成、StageDoor/StageLock 指标、与 RoomDoor V2 的关键差异和下一步 realized-dependency 报告建议 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockTrueHardDependencyV7Curated5Pack.asset` | True Hard Dependency V7 Curated5 5 关包 | 当前 Demo activePack；从自然高覆盖 SGP/StageDoor/StageLock 候选中按真实依赖难度精选，5/5 trace `S`，4/5 tight `S` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDATrueHardDependencyV7Curated5/` | True Hard Dependency V7 Curated5 冻结关卡目录 | `TrueHardDependencyV7Curated5Pack` 对应 LevelDefinition 副本，覆盖 section/sweep/dense/maze_long_chain 多样性 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_true_hard_dependency_v7_curated5_selected.csv` | True Hard Dependency V7 Curated5 选择输入 | 5 关精选 CSV，包含 trueHardScore、familyKey、依赖强度、trace 曲线和外圈压力字段 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_true_hard_dependency_v7_curated5_frozen_trace_metrics.csv` | True Hard Dependency V7 Curated5 frozen trace | 5/5 solved，`avgChoices=2.59-3.63`、`maxChoices=4-6`、`stageLockScore=0.606-0.849`、`outerAvailableHeavyStepRate=0.091-0.257` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_true_hard_dependency_v7_curated5_notes_20260621.md` | True Hard Dependency V7 Curated5 复盘 | 记录目标校准、选择策略、冻结包指标、风险和下一步 dependency-anchor 注入方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockTrueHardDependencyV14OuterHeadZeroVisualPack.asset` | True Hard Dependency V14 外圈零出口头审查包 | 当前 Demo activePack；2 关 focused review，专门验证外圈没有直接朝外头是否能改善外围低难度观感 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDATrueHardDependencyV14OuterHeadZeroVisual/` | V14 外圈零出口头冻结关卡目录 | `TrueHardDependencyV14OuterHeadZeroVisualPack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_true_hard_dependency_v14_outer_head_zero_visual_frozen_trace_metrics.csv` | V14 外圈零出口头 frozen trace | 2/2 `processTier=S`，`outerExitHeadCount=0/0`，`outerExitAvailableChoiceAvg=0/0`，`outerExitSolveRatio=0/0` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_true_hard_dependency_v14_outer_head_zero_notes_20260621.md` | V14 外圈零出口头复盘 | 查看静态外出口头指标、头尾修复 probe 失败原因、V14 指标和下一步生成侧先验建议 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_difficulty_score_v1_notes_20260621.md` | Hard Lane DifficultyScoreV1 校准报告 | 查看为什么 Greedy 可解不等于真实难度、`dependencyFollowRunMax` 如何识别“顺着消”、以及 V14/V20 校准结论 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_structure_v3_causal_unlock_notes_20260621.md` | HardStructure V3 因果解锁报告 | 查看 causal unlock graph 判别器、V3 字段定义、outer-clean anchor 160 样本校准、为什么当前可解样本仍是 `LocalEasy`、以及下一步 anti-local/cross-critical 目标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_outerclean_anchor_source_v3_wide160_hardv3_trace_metrics.csv` | Outer-clean anchor HardStructure V3 trace | 160 个 outer-clean anchor source 的 V3 trace 结果；15 solved，但 `HardPotential/TrueHardCandidate=0` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dependency_braid_outerclean_anchor_hardv3_nearmiss_feed.csv` | Outer-clean anchor HardStructure V3 near-miss feed | V3 near-miss 3 行；用于下一步尝试从 anti-local 但不可解/关键锁不足的源切入 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_true_hard_dependency_v14_difficulty_v1_trace_metrics.csv` | V14 DifficultyScoreV1 trace | 外圈零出口头样本的难度判别重放；用于确认 V14 虽外圈干净但 `dependencyFollowRunMax=3/4` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v20_difficulty_v1_trace_metrics.csv` | V20 DifficultyScoreV1 trace | 88 关旧池重放；用于寻找同时满足外圈干净和 non-follow 的候选，并校准下一轮生成/合链 gate |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockNonFollowOuterRepairV15Review3Pack.asset` | NonFollow Outer Repair V15 Review3 包 | 当前 Demo activePack；从 V20 non-follow 样本做外圈头 subset repair 后冻结，3/3 `S/S`、`dependencyFollowRunMax=2`、外圈压力较原样本下降 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/NFOuterV15Frozen/` | V15 NonFollow 外圈修复冻结关卡目录 | `NonFollowOuterRepairV15Review3Pack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/nonfollow_outer_repair_v15_review_summary.md` | V15 NonFollow 外圈修复 runner 总结 | 查看 source 数、repair 候选数、自动精选数和 top selected 指标 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_nonfollow_outer_repair_v15_review3_frozen_trace_metrics.csv` | V15 NonFollow 外圈修复 frozen trace | 3/3 `S/S`，`avgChoices=3.20-3.76`、`maxChoices=5-6`、`dependencyFollowRunMax=2`、`outerExitHeadCount=3-4` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_nonfollow_outer_repair_v15_notes_20260621.md` | V15 NonFollow 外圈修复复盘 | 记录 generation-side non-follow 0 产、inverse repair lane 正结果和下一步稳定产线建议 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_nonfollow_outer_repair_v15_review3_localburst_retrace_metrics.csv` | V15 Local Patch Burst 重放 | 用新增 local patch 指标重放 V15 Review3；证明 `dependencyFollowRunMax=2` 仍会出现 `localWindow5NeighborMax=5` 的局部扫光 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_hard_production_v20_localburst_trace_metrics.csv` | V20 Local Patch Burst 源池重放 | 88 关旧 hard 池的 local patch 指标重放；用于判断旧源池是否存在“局部不扫光”的可修源 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/support_bridge_ray_probe_v1_source40.csv` | Support bridge ray 40 源静态物理桥 probe | 2026-06-22 按 `target.escapeRay -> hub -> support -> upstream` 物理碰撞扫描 outer-clean anchor 源；40 源找到 200 条 bridge row，其中 `FlipBlockedRay:support` 145 条、`ExistingRay` 55 条 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/support_bridge_ray_probe_v1_emit20_emitted_sources.csv` | Flip support bridge source 导出 CSV | 2026-06-22 导出 20 个 `FlipBlockedRay:support` source asset；直接 trace 代表 3 个均 `Drop` 且 `supportClosureBestScore=0`，说明盲翻 support 会破坏可达性 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/support_bridge_ray_probe_v1_existing_emit20_trace_metrics.csv` | ExistingRay bridge source trace | 2026-06-22 不翻链，仅导出已有物理桥 source 并 trace；20 个里 1 个 solved、5 个 depth-2 closure，但 solved 样本仍为 `LocalEasy`、`dependencyFollowRunMax=3`、`localPatchSolveRunMax=4` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/support_bridge_ray_probe_v1_multihop_emit20_trace_metrics.csv` | ExistingMultiHop bridge source trace | 2026-06-22 选择已有 `B -> U -> support -> hub -> targets` 多跳物理链；19 个导出 source trace 后 1/19 solved、4/19 depth-2 closure，最好 solved 仍为 `LocalEasy`，未突破 ExistingRay 上限 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/support_bridge_ray_probe_v1_immediate_hit_split_diagnostic.csv` | Immediate blocker split 诊断 | 2026-06-22 统计 support ray 贴脸命中 immediate blocker 的位置；20 源 768 个可用 support hit 全部 `hitDistance=1`，91 endpoint、87 near-endpoint，说明 dense body 无插入空位但存在端头切链机会 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/support_bridge_ray_probe_v1_split_diag_emit20_trace_metrics.csv` | SplitEndpointSidecar trace | 2026-06-22 将 immediate blocker 端头 3 格切成 sidecar 的诊断变体；20/20 `Drop`，仅 1/20 出现 depth-2 closure 且仍 `LocalEasy`，说明 endpoint split 不是可用主修复路线 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PressureReadStageLockSourceSupportBridgeRay/` | Support bridge ray probe 源资产目录 | `Build-SupportBridgeRayProbe.ps1 -EmitSources` 输出的实验 source assets；当前仅作物理桥验证，不是正式候选池 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_TraceBridgeProofV1Pack.asset` | Trace Bridge Proof V1 包 | 2026-06-22 proof-only 包；3 关用于验证 source-level ray-collision bridge 能 surviving StageLock、solve 和 independent trace replay，不是最终高难候选 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/TraceBridgeProofV1/` | Trace Bridge Proof V1 冻结关卡目录 | `SGPRhythmLab_TraceBridgeProofV1Pack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/trace_bridge_proof_v1_frozen_replay_input.csv` | Trace Bridge Proof V1 冻结 replay 输入 | 冻结 pack CSV 补回 planned bridge 的 T/H/S/U 字段，用于验证最终 asset 上 bridge 是否 trace-visible |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/trace_bridge_proof_v1_frozen_replay_with_bridge_metrics.csv` | Trace Bridge Proof V1 冻结 replay 指标 | 3/3 solved，3/3 planned bridge replay `ok`/depth=3，但仍 `LocalEasy`、`localPatchSolveRunMax=5`，说明只是物理因果桥 proof |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/trace_bridge_proof_v1_notes_20260622.md` | Trace Bridge Proof V1 复盘 | 记录 GPT 协作结论、代码改动、验证命令、结果表和下一步 stage-quality-aware source-slot bridge selection 方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLaneRealclosurePeakPruneV1Pack.asset` | Realclosure Peak Prune V1 hard-lane proof 包 | 2026-06-22 历史 proof 包；4/4 frozen board trace accepted，`maxChoices=7`、`localPatchSolveRunMax=3`、planned bridge replay `ok/d3`。这是控制中盘 unlock fanout 的 proof/review 包，不是最终量产策略 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLaneRealclosurePeakPruneV1Frozen/` | Realclosure Peak Prune V1 冻结关卡目录 | `SGPRhythmLab_HardLaneRealclosurePeakPruneV1Pack` 对应 LevelDefinition 副本；冻结 manifest 带 trace bridge anchor 字段，可二次 board trace 验证 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_realclosure_peak_prune_v1_frozen_board_gate_summary.md` | Realclosure Peak Prune V1 frozen board gate 总结 | 4/4 accepted；用于查看每关 avg/max choice、choice rise、stage/anti-local、follow/local run、support closure 和 planned bridge replay |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLaneRealclosureRelaySplitV2Pack.asset` | Realclosure Relay Split V2 hard-lane review 包 | 2026-06-22 当前 Demo activePack；4/4 frozen board trace accepted，最好两关 `maxChoices=5`、soft cap 5 hit 0、planned bridge replay `ok/d3`，用于人工验证 Fanout Dynamics V2 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLaneRealclosureRelaySplitV2Frozen/` | Realclosure Relay Split V2 冻结关卡目录 | `SGPRhythmLab_HardLaneRealclosureRelaySplitV2Pack` 对应 LevelDefinition 副本；chain 11 relay split 后覆盖不删格、链数 35 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_realclosure_relay_split_v2_frozen_board_gate_summary.md` | Realclosure Relay Split V2 frozen board gate 总结 | 4/4 accepted；记录 max choice、choice rise、stage/anti-local、local/follow run、support closure 与 planned bridge replay |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLaneRealclosureRelaySplitAutoV1Pack.asset` | Realclosure Relay Split Auto V1 5 关 hard-lane review 包 | 2026-06-22 当前 Demo activePack；从 2 个 realclosure orientation 源的 auto relay split 过线样本精选 5 关，frozen board gate 5/5 accepted，4 关 `maxChoices=5`、1 关 `maxChoices=6`、planned bridge replay 全部 `ok/d3` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLaneRealclosureRelaySplitAutoV1Frozen/` | Realclosure Relay Split Auto V1 冻结关卡目录 | `SGPRhythmLab_HardLaneRealclosureRelaySplitAutoV1Pack` 对应 LevelDefinition 副本；用于人工评估“至少 5 关真正有难度”的第一批 fanout dynamics 自动化样本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_realclosure_relay_split_auto_v1_frozen_board_gate_summary.md` | Realclosure Relay Split Auto V1 frozen board gate 总结 | 5/5 accepted；验收规则为 `maxChoices<=6`、`localPatchSolveRunMax<=3`、`dependencyFollowRunMax<=3`、support closure depth >= 2、planned bridge replay required |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLaneDiversePerturbProbeV1Pack.asset` | Diverse Perturb Probe V1 5 关诊断包 | 2026-06-22 当前 Demo activePack；用于验证结构扰动是否能跳出 Auto V1 同质问题。第 1 关为 `MediumStructure`，其余为多 source 对照，不是最终高难产线 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLaneDiversePerturbProbeV1Frozen/` | Diverse Perturb Probe V1 冻结关卡目录 | `SGPRhythmLab_HardLaneDiversePerturbProbeV1Pack` 对应 LevelDefinition 副本；冻结后已重放 trace |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lane_diverse_perturb_probe_v1_frozen_metrics.csv` | Diverse Perturb Probe V1 frozen trace | 5 关冻结指标；第 1 关 `MediumStructure/anti=0.471/maxChoices=6/local=3/follow=3/outerHead=6`，第 4/5 关外圈干净但仍 `LocalEasy` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/DesignedHardLockV0/` | Designed Hard Lock V0 proof 关卡目录 | 2026-06-22 物理依赖 breakthrough 原型；4 个镜像变体均由 `Build-DesignedHardLockV0.ps1` 生成，定位是 trace proof，不是正式高覆盖关卡 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/designed_hard_lock_v0_candidates.csv` | Designed Hard Lock V0 输入 CSV | 4 个 proof 候选的 trace 输入；路径为绝对 asset path，供 `Build-SGPRhythmTrace.ps1` 重放 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/designed_hard_lock_v0_trace_metrics.csv` | Designed Hard Lock V0 trace 指标 | 4/4 solved、4/4 `TrueHardCandidate`；`anti=0.688`、`supportClosure=0.921/d3`、`criticalLocks=3`、`maxChoices=5`、`outerExitHead=0`、`localPatch=0`，但 `dependencyFollowRunMax=4`、coverage 低 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLockDirectedBatchPressureFinal5Pack.asset` | HardLock DirectedBatch Pressure Final5 包 | 2026-06-22 pressure filler 比例控制的 5 关冻结包；5/5 board trace `solved=True` 且 `processTier=A`，3 `TrueHardCandidate` + 2 `HardPotential`，`avgChoices` 最大 3.76、`maxChoices` 最大 7、`outerExitHeadCount=0` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLockDirectedBatchPressureFinal5Frozen/` | HardLock DirectedBatch Pressure Final5 冻结关卡目录 | `SGPRhythmLab_HardLockDirectedBatchPressureFinal5Pack` 对应 LevelDefinition 副本；由 `Build-HardLockSlotDirectedBatchFillV1.ps1` 的 pressure mode 分父本输出冻结 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lock_directed_batch_pressure_final5_frozen_retrace_metrics.csv` | HardLock DirectedBatch Pressure Final5 frozen trace | 冻结后复验指标；5/5 A，平均 `avgChoices=3.28`，最低 `causalAntiLocalityScore=0.6`，`supportClosureBestDepth>=3`，`localPatchSolveRunMax<=2` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLockDirectedBatchChokeO1Final5Pack.asset` | HardLock DirectedBatch Choke O1 Final5 包 | 2026-06-22 choke filler + opener guard 的 5 关冻结包；4 关为补肉成功版，1 关为不可补父本原 hard-lock 对照；5/5 solved，`avgChoices` 平均 3.146、最大 3.76，`maxChoices<=7`，`outerExitHeadCount=0` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLockDirectedBatchChokeO1Final5Frozen/` | HardLock DirectedBatch Choke O1 冻结关卡目录 | `SGPRhythmLab_HardLockDirectedBatchChokeO1Final5Pack` 对应 LevelDefinition 副本；第 4/5 关为二轮补肉，coverage 约 0.277-0.281 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hard_lock_directed_batch_choke_o1_final5_frozen_retrace_metrics.csv` | HardLock DirectedBatch Choke O1 frozen trace | 冻结后复验指标；5/5 solved，`antiLocal>=0.6`，`supportClosureBestDepth>=3`，`localPatchSolveRunMax<=2`，`dependencyFollowRunMax<=4` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLock030ProofReview5Pack.asset` | HardLock 0.30 proof/review 5 关包 | 2026-06-23 证明 0.30 完整链路可落成 5 个可解候选；混合了“低 avg/max 但 outer=1”和“outer=0 但 avg debt”两类，用于诊断，不作为最终 production gate |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLock030ProofReview5Frozen/` | HardLock 0.30 proof/review 冻结关卡目录 | `SGPRhythmLab_HardLock030ProofReview5Pack` 对应 LevelDefinition 副本；冻结后 5/5 board trace solved/A-tier |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hardlock_030_proof_review5_frozen_trace_metrics.csv` | HardLock 0.30 proof/review frozen trace | 5/5 solved/A-tier；用于查看 0.30 后 outer/avg 两类残余模式 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HardLock030DynamicOuterGate5Pack.asset` | HardLock 0.30 Dynamic Outer Gate5 包 | 2026-06-23 当前 0.30 量产线基准包；采用动态外口压力 gate，5/5 solved/A-tier，coverage 约 0.306+，`avgChoices<=4.0`，`maxChoices<=6` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/HardLock030DynamicOuterGate5Frozen/` | HardLock 0.30 Dynamic Outer Gate5 冻结关卡目录 | `SGPRhythmLab_HardLock030DynamicOuterGate5Pack` 对应 LevelDefinition 副本；明早扩父本池/挂 Demo 时优先从这里接续 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hardlock_030_dynamic_outer_gate5_frozen_trace_metrics.csv` | HardLock 0.30 Dynamic Outer Gate5 frozen trace | 5/5 solved/A-tier，`supportClosureBestDepth=3`、`localPatchSolveRunMax=3`、`dependencyFollowRunMax=4`、`outerExitAvailableChoiceMax=1`、`outerExitSolveRunMax=1`；当前 0.30 production baseline |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pc34repair_dyn1_accepted.csv` | Parent capacity + near-miss repair 0.30 accepted pool | 2026-06-23 从 0.265 父本经 directed fill 到 0.299/0.300 near-miss，再翻新增 filler 组方向救援得到 12 个 accepted；top `pc34repair_dyn1_v003` coverage `0.3002451`、avg `3.58`、max `6`、anti `0.7`、support `0.935/d3`、local `2`、follow `4` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RP/pc34repair_dyn1/` | Parent capacity + near-miss repair 0.30 accepted levels | `pc34repair_dyn1_accepted.csv` 对应 LevelDefinition 输出目录；用于明早去重、打包和 Demo 复核 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pc34to30p2_rejected_steps.csv` | 0.30 near-miss source pool | `pc34to30p2` second-stage directed fill 的 high-structure unsolved 候选池；repair 脚本从这里筛 `coverage>=0.299` 的新增 filler 方向组合 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pcdo265p1_p05_c34_parent_feed.csv` | 0.277 intermediate parent feed | 从 `pcdo265p1_p05_s937101_b01_r1_trace_metrics.csv` 恢复出的单行中间父本 feed；coverage `0.277`、avg `2.91`、max `6`、anti `0.714`、support depth `4`、outer=0，作为 `pc34to30p2` 的输入 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pcauto1_a_parent_pool.csv` | 自动中间父本回归 A pool | 从 `pcdo265p1_p05_c34_parent_feed.csv` 自动 directed fill + repair；9 个 0.299+ near-miss，51 variants -> 12 accepted，验证探针能复现手工链路 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pcorig1_a_parent_pool.csv` | 原 cov265 第 5 父本 A pool | 从 `hard_lock_slot_trace_delta_fill_v1_cov265_final5_selected.csv` 第 5 父本直跑，14 variants -> 4 accepted；证明从原父本可自动到 0.299 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pcseed06a_a_parent_pool.csv` | 原 headfix 第 6 父本 A pool | 从 `hard_lock_slot_sgp_fill_headfix_v0_base_09` 直跑，6 variants -> 2 accepted；第二个 A 类结构底盘 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/parent_capacity_o1_review5_selected.csv` | Parent Capacity O1 review5 selected CSV | 从两个 A 父本 accepted pool 合并选出的 5 关，selected coverage 约 `0.2990196` |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_ParentCapacityO1Review5Pack.asset` | Parent Capacity O1 review5 pack | 2026-06-23 冻结 5 关包；自动父本承压链路第一版 review 包 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/ParentCapacityO1Review5Frozen/` | Parent Capacity O1 review5 frozen levels | `SGPRhythmLab_ParentCapacityO1Review5Pack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/parent_capacity_o1_review5_frozen_metrics.csv` | Parent Capacity O1 review5 frozen trace | 5/5 `solved=True`、5/5 `processTier=A`，`avgChoices<=3.94`、`maxChoices<=7`、`anti>=0.645`、`supportDepth>=3`、`localPatch<=1`、动态外口 pressure <=1 |

| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_D176StrictPack.asset` | Building Grammar D176 strict 4 关包 | 第一次 strict 过线；h231-like -> depanchor -> StageGateSearch+UltraLowChoice -> pair repair `10,8`，`avgChoices=2.64/maxChoices=5/followRun=2/stage=0.822/late=3` |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/depanchor_v176_pairrepair_10_8_strict_selected.csv` | D176 strict selected CSV | D176 冻结包输入；用于复查首批 strict repair 指标 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/building_maze_relaxed_source_v182_trace_feed.csv` | Building Grammar V182 relaxed source feed | V9/V23/V24/nonfollow 风格 trace 合并 165 rows；用于 near-strict depanchor 试跑 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/building_maze_allnear_source_v183_trace_feed.csv` | Building Grammar V183 all-near source feed | 156 个历史 trace metrics 去重后筛出的 36 个 low-choice/high-stage near 源 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d182b_strict_selected.csv` | Building Grammar d182b strict selected CSV | 9 near-strict 源 -> 3 strict；复现 hcd58-like homogeneous max-run 的 stage-preserving pair repair |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d184off_strict_selected.csv` | Building Grammar d184off strict selected CSV | all-near feed 后半段 -> 2 strict；仍为 hcd58-like geometry，证明后段源也可出货但多样性有限 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d182b_repairability.csv` | D182B repairability 画像 | 标记 `strict_pair_repairable` 正例：`labels=AAAA/maxLabelCharCount=1/maxHasO=False` |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d183all_repairability.csv` | D183All repairability 画像 | 标记 triple 能降 follow 但破 stage 的 `repair_breaks_stage` 负例：`AAAA|BBBB|OOOO` |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d184off_repairability.csv` | D184Off repairability 画像 | 同时包含 hcd58-like 正例和 mixed fragile 负例，用于后续 source gate 校准 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d185gate_strict_selected.csv` | Building Grammar d185 gate strict CSV | `-EnableRepairabilityGate` 正例验证；6 candidates -> 6 gated repairs -> 3 strict |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d185gate_repairability_prefilter.csv` | Building Grammar d185 gate repairability CSV | runner 内置 gate 输出；验证 homogeneous 正例不会被误杀 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d186fam1_strict_selected.csv` | Building Grammar d186 family-cap strict CSV | `SourceFamilyCap=1` 压同族复用后 8 candidates -> 2 repairs -> 1 strict，用于评估多样性瓶颈 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d186fam1_repairability_prefilter.csv` | Building Grammar d186 family-cap repairability CSV | 显示 1 个 `repair_candidate_homogeneous` 正例和 7 个 `mixed_fragile` 负例，是当前 source scarcity 的核心证据 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_D188D189PressureReview4Pack.asset` | Building Grammar D188/D189 pressure review 4 关包 | 当前 Demo activePack；D188 raw-pass 3 关 + D189 openers=4 压力 probe 1 关，用于人工评估“开局压力/依赖强度”修正后的观感 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/D188D189PressureReview4/` | D188/D189 pressure review 冻结关卡目录 | `SGPRhythmLab_D188D189PressureReview4Pack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d188_d189_pressure_review4_selected.csv` | D188/D189 pressure review selected CSV | 合并 `d188v182off9rawcross` 的 3 个 raw strict 和 `d189v182off9open4probe` 的 1 个 openers=4 pressure probe |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_d191v182off9pressureflipStrictPack.asset` | Building Grammar d191 pressure+interleave flip 2 关包 | 当前 Demo activePack；`openers=4/avg=3.32/max=5/followRun=2/stage=0.833/cross=0.043`，用于验证更强开局压力和依赖强度 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/d191v182off9pressureflipStrict/` | d191 pressure+interleave flip 冻结关卡目录 | `SGPRhythmLab_d191v182off9pressureflipStrictPack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d191v182off9pressureflip_strict_selected.csv` | d191 pressure+interleave flip selected CSV | d191 2 个 raw strict 的精选输入；同源相似，不代表稳定量产 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_D191D192PressureOpenerReview3Pack.asset` | Building Grammar D191/D192 pressure opener review 3 关包 | 当前 Demo activePack；d191 raw pressure 2 关 + d192 two-stage repair 1 关，验证 `triple 保 cross/follow` 后 `single flip 拉 openers=4` 的路线 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/D191D192PressureOpenerReview3/` | D191/D192 pressure opener review 冻结关卡目录 | `SGPRhythmLab_D191D192PressureOpenerReview3Pack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d191_d192_pressure_opener_review3_selected.csv` | D191/D192 pressure opener review selected CSV | 3 关精选输入；第三关指标 `openers=4/avg=2.68/max=5/followRun=2/stage=0.810/cross=0.043` |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_D198HardPressureRefine1Pack.asset` | Building Grammar D198 hard opening pressure 单关包 | 当前 Demo activePack；针对“外圈出口偏弱/开局压力偏低”反馈，收紧到 `openers=3`、`outerTouch=0.571`、`strictMeaningful=0.800`、`nearUnlock=0.200` 的 pair repair 样本 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/D198HardPressureRefine1/` | D198 hard pressure 冻结关卡目录 | `SGPRhythmLab_D198HardPressureRefine1Pack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d198_hardpressure_refine1_selected.csv` | D198 hard pressure selected CSV | 单关 review 输入；`openers=3/avg=2.64/max=5/followRun=2/stage=0.822/cross=0.043/spineBalance=0.571` |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_D199HardPressureOuterRun2Review1Pack.asset` | Building Grammar D199 hard outer-run2 单关包 | 当前 Demo activePack；D198 后的小幅外圈改良 review，`sameOuterSideSolveRunMax=2`、`outerTouch=0.571`、`strictMeaningful=0.840`，但 `spineAlternationRunMax=11`，不是稳定突破包 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/D199HardPressureOuterRun2Review1/` | D199 hard outer-run2 冻结关卡目录 | `SGPRhythmLab_D199HardPressureOuterRun2Review1Pack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d199_hardpressure_outerrun2_review1_selected.csv` | D199 hard outer-run2 selected CSV | 单关 review 输入；`openers=3/avg=2.68/max=5/followRun=2/stage=0.810/cross=0.043/sameOuterSideRun=2` |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_D214OuterRootNearMissReview2Pack.asset` | Building Grammar D214 outer-root near-miss 2 关包 | 当前 Demo activePack；原始 source 层 `OuterRootAnchorBias` 后的小样，`openers=3/avgChoices=2.32/max=4/stage=0.848/cross=0.043`，但 `followRun=3/sameOuterSideRun=3/earlyOuterAvailableMax=4`，不是正式过线包 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/D214OuterRootNearMissReview2/` | D214 outer-root near-miss 冻结关卡目录 | `SGPRhythmLab_D214OuterRootNearMissReview2Pack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-building-grammar/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/d214_outerroot_nearmiss_review2_selected.csv` | D214 outer-root near-miss selected CSV | 2 关 review 输入；用于人工看开局压力是否比 D199 更接近方向 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_MixedFamilyProductionO1Pack.asset` | Mixed Family Production O1 9 关 review 包 | 2026-06-23 冻结的双 family 包；包含 hard-lock causal family、DependencySkeletonV3、RoomDoorSkeletonV2，用于人工验证多样性和体感 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/MixedFamilyProductionO1Frozen/` | Mixed Family Production O1 冻结关卡目录 | `SGPRhythmLab_MixedFamilyProductionO1Pack` 对应 LevelDefinition 副本 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_MixedFamilyProductionO1Pack.csv` | Mixed Family Production O1 pack 输入 CSV | 冻结工具输出的 9 关 pack CSV；可用于二次 trace 或 Demo 挂包 |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_CausalHardlockDiverseReviewV1Cov28Pack.asset` | Causal Hardlock Diverse Review V1 Cov28 12 关包 | 2026-06-23 当前 Demo activePack；12 个不同父本/几何，coverage 约 0.28+，12/12 frozen trace solved/A，11 TrueHardCandidate；用于人工筛哪些父本值得继续推到 0.30。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_CausalHardlockDiverseReviewV1Cov30Pack.asset` | Causal Hardlock Diverse Review V1 Cov30 4 关事实包 | 2026-06-23 现有 0.299+ 严格去重后只剩 4 个不同父本/几何；4/4 frozen trace solved/A，证明 0.30 A pool 多样性仍不足。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_hardlock_diverse_review_v1_cov28_frozen_trace_metrics.csv` | Cov28 多样性包 frozen trace | 12/12 solved/A，11 TrueHardCandidate + 1 HardPotential；用于人工 review 后挑父本继续推到 0.30。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_hardlock_diverse_review_v1_cov30_frozen_trace_metrics.csv` | Cov30 严格去重包 frozen trace | 4/4 solved/A，3 TrueHardCandidate + 1 HardPotential；用于记录当前 0.30 去重产能事实。 |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_CausalHardlockDiverseReviewV2Cov28Pack.asset` | Causal Hardlock Diverse Review V2 Cov28 7 关包 | 2026-06-23 当前 Demo activePack；加入 occupancy Jaccard near-duplicate 去重和人工 reject 后得到 7 个更干净父本，7/7 frozen trace solved/A，最大 occupancy Jaccard=0.766。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_hardlock_diverse_review_v2_cov28_frozen_trace_metrics.csv` | V2 Cov28 frozen trace | 7/7 solved/A，6 TrueHardCandidate + 1 HardPotential；用于人工确认干净父本是否可继续推到 0.30。 |

## Recent SGP Rhythm Review Packs

| Path | 用途 | 什么时候看 |
| --- | --- | --- |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SpeciesManifoldRootPairReview2Pack.asset` | Species manifold 2-root review demo pack | 当前 Demo 挂载的两个强 root 候选；用于快速人工比较 `a02280d338` 与 `0eea76b1ba` 两个 root 的体感差异 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/species_manifold_root_pair_review2_frozen_trace_metrics.csv` | Species manifold 2-root frozen trace | 查看 Demo 挂载后冻结资源的真实 trace，2/2 solved，supportDepth=4 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SpeciesManifoldCanonicalRootReview1Pack.asset` | Species manifold canonical root 单关 demo pack | 当前 Demo 挂载；从 RootPairReview2 中去掉 rhythm/geometry near-duplicate 后只保留一个 canonical root 样本。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/species_manifold_canonical_root_review1_frozen_trace_metrics.csv` | Canonical root review1 frozen trace | 查看单关包冻结后真实 trace：1/1 solved，avg/max `3.66/6`，supportDepth `4`。 |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SpeciesNewRootReview2Pack.asset` | Species New Root Review2 2 关包 | 2026-06-23 当前 Demo activePack；从 `web_four_mirrory` 新 root source 经小步 fill 得到两个不同 rootSkeleton（`1179a2b946`、`77b588f85b`），用于人工判断真 root 多样性体感。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/species_new_root_review2_frozen_trace_metrics.csv` | Species New Root Review2 frozen trace | 2/2 solved/A；avg/max `3.52/6` 与 `3.69/7`，supportDepth `3/4`，localPatchRun `2`，dependencyFollowRun `3`，outerExitHead `0`。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/species_new_root_review2_frozen_signature_signatures.csv` | Species New Root Review2 canonical signature | 2 rows -> 2 rootSkeleton / 2 macro / 2 chainLanguage；用于确认不是 rhythm 或单链差异伪多样性。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_MixedRootTopologyReviewV1Pack.asset` | Mixed Root Topology Review V1 4 关包 | 2026-06-23 当前 Demo activePack；按 mixed-root gate + `MaxPerRootTopology=1` 从 MixedFamilyProductionO1 中冻结 4 个严格不同 rootTopology（hardlock tri、dependency_skeleton、room_door x2），4/4 frozen trace solved，3 S + 1 A。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/MixedRootTopologyReviewV1Frozen/` | Mixed Root Topology Review V1 冻结关卡目录 | `SGPRhythmLab_MixedRootTopologyReviewV1Pack` 对应 LevelDefinition 副本。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/mixed_root_topology_review_v1_frozen_trace_metrics.csv` | Mixed Root Topology Review V1 frozen trace | 4/4 solved，`avgChoices` 2.4-3.47，`maxChoices` 4-7，`causalAntiLocalityScore` 0.6-1.0；用于人工验证多 root 体感。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/mixed_root_topology_review_v1_frozen_root_topology_root_topology.csv` | Mixed Root Topology Review V1 root topology report | 4 rows -> 4 `rootTopologyHash` / 4 strictRoot / 3 sourceArchetype；当前最干净的多 root review 证据链。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_CausalMotifEmbeddingV1Smoke2Pack.asset` | Causal Motif Embedding V1 smoke proof 2 关包 | 2026-06-23 motif embedding compiler proof；2/2 frozen trace solved/A，`supportClosureBestDepth=4`、outer=0。仅证明 tri_convergent motif embedding 闭环，不代表多 root production 包。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_motif_embedding_v1_smoke2_frozen_trace_metrics.csv` | Causal Motif Embedding V1 smoke2 frozen trace | 2/2 solved/A；avg/max `2.96/5` 与 `3.76/6`，supportDepth=4，localPatch<=3，followRun<=4。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_motif_embedding_v1_strat_smoke1_summary.md` | Causal Motif Embedding V1 stratified smoke summary | 记录 source species 分层后包含 tri/web，但最终只有 tri 过线；web/dual 需要 species-aware fill。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_CausalMotifEmbeddingV1ReviewPool1Pack.asset` | Causal Motif Embedding V1 Review Pool1 5 关包 | 2026-06-23 当前 Demo activePack；同一大容器内 5 个 support-lock motif instance/rhythm variant，5/5 frozen trace solved/A，用于人工判断 motif-instance 体感差异是否足够进入短期高难池。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_motif_embedding_v1_review_pool1_frozen_trace_metrics.csv` | Causal Motif Embedding V1 Review Pool1 frozen trace | 5/5 solved/A，avg/max `2.88-3.76 / 5-7`，supportDepth `3-4`，outerExitHead=0；用于复核当前 Demo 包真实 trace。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_motif_embedding_v1_review_pool1_frozen_signature_summary.md` | Causal Motif Embedding V1 Review Pool1 signature summary | 5 core / 4 skeleton family / 4 macro / 3 rootSkeleton / 3 strictRoot / 5 rhythm variants；说明这是 motif-instance 多样性，不是严格 multi-root proof。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_hard_lite_template_v1_directed_fill_strictfanout_smoke2_final_selected.csv` | Dual-gate hard-lite strict fanout selected proof | 2026-06-23 第二 causal backbone primitive 的正式 smoke：1/1 retrace solved/A，coverage `0.2304`，branch2/fanout2，backbone `34771de5e2`。不是最终 review pack。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_hard_lite_template_v1_directed_fill_strictfanout_smoke2_final_backbone_backbones.csv` | Dual-gate hard-lite final causal backbone report | 记录 strict fanout fill 后仍保持 `serial_support_lock||dual_gate_hubfield||...fanout2|branch2|closureChain`，用于后续量产线 gate。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/DualGateHardLiteTemplateV1DirectedFillStrictFanoutSmoke2/` | Dual-gate hard-lite strict fanout proof levels | `dual_gate_hard_lite_template_v1_directed_fill_strictfanout_smoke2_final_selected.csv` 对应 LevelDefinition 输出目录。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DualGateVsTriRootReview1Pack.asset` | Dual Gate vs Tri Root 2关对照包 | 2026-06-23 当前 Demo activePack；Level1 旧 tri_convergent branch3/fanout4，Level2 新 dual_gate_hard_lite branch2/fanout2；用于用户肉眼判断是否属于不同 causal root。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_vs_tri_root_review1_frozen_trace_metrics.csv` | Dual Gate vs Tri Root frozen trace | 2/2 solved/A；Level1 avg/max `3.16/7`、branch3/fanout4；Level2 avg/max `3.04/6`、branch2/fanout2。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_vs_tri_root_review1_frozen_backbone_backbones.csv` | Dual Gate vs Tri Root causal backbone report | 2 rows -> 2 causalBackboneRoots；用于证明该对照包不是同 backbone 变体。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DualGateSpatialRootV1SkeletonReview1Pack.asset` | Dual Gate Spatial Root V1 skeleton 单关审查包 | 2026-06-23 当前 Demo activePack；低覆盖非对称双门控视觉 root 骨架，frozen trace `solved=False/supportDepth=1`，只用于肉眼判断 root 形态，不是高难生产包。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_spatial_root_v1_skeleton_review1_frozen_trace_metrics.csv` | Dual Gate Spatial Root V1 frozen trace | 记录该 skeleton 包不可解、非 hard 的 trace 事实，避免误入 production。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DualGateJointCoreRootV1Review2Pack.asset` | Dual Gate Joint-Core Root V1 单关审查包 | 2026-06-23 当前 Demo activePack；true dual_gate 定义修正版，两个分离 gate 分别打开同一中央 core 的两个入口；frozen trace solved/A，但 avgChoices 偏高，尚非 production hard。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_joint_core_root_v1_review2_frozen_trace_metrics.csv` | Dual Gate Joint-Core Root V1 frozen trace | 记录 solved/A、avg/max `4.93/6`，并证明该包可解；旧 supportClosure 指标不识别此 motif。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DualGateJointCoreRootV1Review5Pack.asset` | Dual Gate Joint-Core shared-lock 原型包 | 2026-06-23 当前 Demo activePack；B 路解 lockB、A 路解 lockA，两个锁都清后同一个 core chain 才打开。Solved/A，avg/max `4.55/6`，低覆盖原型，不是 production hard。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_joint_core_root_v1_review5_trace_metrics.csv` | Dual Gate shared-lock trace | 记录 true dual-gate shared-lock prototype 的 trace 指标；用于后续建立专门 dualGateJointCore gate。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_ValidatedRootExpansionO1ReviewPack.asset` | Validated Root Expansion O1 4 关 review 包 | 2026-06-23 当前 Demo activePack；已验证 hardlock/support-lock root 的 clean expansion，4/4 frozen trace solved/A。定位是已验证 root 产线扩张审查包，不是新 root proof。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/ValidatedRootExpansionO1Review/` | Validated Root Expansion O1 冻结关卡目录 | `SGPRhythmLab_ValidatedRootExpansionO1ReviewPack` 对应 LevelDefinition 副本。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/validated_root_expansion_o1/validated_root_expansion_o1_cov285_review_selected.csv` | Validated Root Expansion O1 clean selected CSV | 从现有 hard-gate 池 + parent capacity 新分片合并后，按 support-lock gate、动态外口 gate、parent/geometry/occupancy 去重选出的 4 个 clean review 候选。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/validated_root_expansion_o1_review_frozen_trace_metrics.csv` | Validated Root Expansion O1 frozen trace | 4/4 solved/A，avgChoices `3.23-3.69`、maxChoices `5-7`、supportDepth `3-4`、localPatch `1-2`、dependencyFollow `4`，动态外口 pressure <=1。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/validated_root_expansion_o1_review_frozen_signature_summary.md` | Validated Root Expansion O1 signature summary | 4 core / 4 skeleton family / 3 macro / 2 rootSkeleton / 2 strictRoot / 4 rhythm variants；用于说明这是同已验证 root 的 clean expansion，而不是新 root 证明。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_ValidatedRootBackgroundSGPV1T40ProofPack.asset` | Validated Root Background SGP V1 T40 proof 包 | 2026-06-23 当前 Demo activePack；单关 proof，把已验证 support-lock root 从 `coverage=0.305` 推到约 `0.398`，frozen trace `solved=True/processTier=A/supportDepth=3/avgChoices=3.58/maxChoices=7`。不是最终量产包，用于人工看接近 0.4 的填肉体感。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/ValidatedRootBackgroundSGPV1T40Proof/` | Validated Root Background SGP V1 T40 proof 冻结关卡目录 | `SGPRhythmLab_ValidatedRootBackgroundSGPV1T40ProofPack` 对应 LevelDefinition 副本。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/validatedroot_background_sgp_v1_t40_proof_frozen_trace_metrics.csv` | Validated Root Background SGP V1 T40 frozen trace | 1/1 solved/A；`supportClosureBestDepth=3`、`supportClosureBestScore=0.969`、`localPatchSolveRunMax=2`、`dependencyFollowRunMax=3`、动态外口 pressure=1。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/trace_placement_probe_v1_smoke5_summary.md` | Trace Placement Probe V1 smoke summary | 2026-06-23 对 0.398 proof 父本做 5 个 micro-chain 反向 placement probe，分类结果 `SafeNeutral=3`、`Deadlock=2`；用于诊断 0.40+ 填肉失败是中盘堵死还是可安全补位。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ray_constraint_map_v1_t40_summary.md` | Ray Constraint Map V1 T40 summary | 2026-06-23 对 0.398 proof 父本输出 ray/cell 角色图；`CriticalTimingZone=136`、`SafeFillZone=48`、`GuardSlot=2`，说明 0.40+ 剩余空间大量进入 ray-causal 时序敏感区。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ray_constraint_map_v1_t40_role_grid.txt` | Ray Constraint Map V1 T40 ASCII role grid | 用 `K/#/C/G/B/H/F/.` 标记 occupied critical、occupied、critical timing、guard、body-only、head-allowed、free-head risk、safe fill；用于肉眼查看当前父本可填空间分布。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ray_constraint_guided_fill_v1_step440_c01_exact_to442_onechain_summary.md` | Ray Constraint Guided Fill 0.441 proof summary | 2026-06-23 已验证 root 产线扩张 proof：从 0.398 proof 父本经多轮 map-guided 小步填肉推到 coverage `0.4411765`，最终样本 `ray_constraint_guided_fill_v1_step440_c01_exact_to442_onechain_b01_c15` 仍为 `solved=True/A/TrueHardCandidate`、supportDepth=3、avg/max `4.05/8`、local=2、follow=3。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RayConstraintGuidedFillV1/` | Ray Constraint Guided Fill V1 生成关卡目录 | 存放 0.398->0.441 迭代填肉 proof 的候选 LevelDefinition。当前最好 hard proof 为 `ray_constraint_guided_fill_v1_step440_c01_exact_to442_onechain_b01_c15.asset`；尚未冻结成 review/demo 包。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ray_first_blocker_fill_v1_step442_c15_to448_strict_critical_summary.md` | Ray First Blocker 0.445 proof summary | 2026-06-24 从最高 0.441 proof 父本按 ray-first blocker 单链补肉；严格 gate 1/20 accepted，样本 `ray_first_blocker_fill_v1_step442_c15_to448_strict_critical_b01_c17` coverage `0.4448529`、solved/A、supportDepth=3、avg/max `3.62/7`、local=2、follow=3。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RayFirstBlockerFillV1/` | Ray First Blocker Fill V1 生成关卡目录 | 存放从 RayConstraintMap edge/cell 反推 blocker 链的候选 LevelDefinition；当前 proof 资产为 `ray_first_blocker_fill_v1_step442_c15_to448_strict_critical_b01_c17.asset`，尚未冻结成 demo/review pack。 |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ray_first_blocker_fill_v2_balanced16_c09_to463_summary.md` | Ray First Blocker balanced V2 0.462 proof summary | 2026-06-24 balanced-anchor V2 连续 proof；从 `0.4411765` 经每步重算 map 的单链 ray-first blocker 迭代到 `0.4620098`，最高样本 `ray_first_blocker_fill_v2_balanced16_c09_to463_b01_c16` solved/A、supportDepth=4、avg/max `2.9/5`、local=3、follow=4。 |

## Strict Dual Gate Review Packs - 2026-06-23

- `SGPRhythmLab_DualGateJointCoreVariantV2FillReview8T018Pack.asset`: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DualGateJointCoreVariantV2FillReview8T018Pack.asset`, guid=`c1687786dc944d18bb719a04b489fb13`. 8-level strict dual T018 review pack, mounted in Demo.
- Source report: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_joint_core_variant_v2_fill_target018_review8_selected.csv`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_joint_core_variant_v2_fill_review8_t018_frozen_trace_metrics.csv`.

## Strict Dual Gate T030 Proof Pack - 2026-06-24

- `SGPRhythmLab_DualGateJointCoreVariantV2FillProof2T030Pack.asset`: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DualGateJointCoreVariantV2FillProof2T030Pack.asset`, guid=`6c8fdbedca9d4c56a4893b67b194c1d9`. 2-level strict dual shared-core proof at selected coverage `0.3015/0.3039`; Demo mounted here after freeze.
- Source report: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_joint_core_variant_v2_fill_target030_proof2_selected.csv`.
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_DualGateJointCoreVariantV2FillProof2T030Pack.csv`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dual_gate_joint_core_variant_v2_fill_proof2_t030_frozen_trace_metrics.csv`.

## Validated Root 0.512 Review Pack - 2026-06-24

- `SGPRhythmLab_PathAwareStep512C03ReviewPack.asset`: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PathAwareStep512C03ReviewPack.asset`, guid=`763823fa3f75436d9caf80a7d54b991f`. Single-level 0.512 validated-root expansion checkpoint; Demo activePack currently points here for user inspection.
- Frozen level dir: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PathAwareStep512C03Review/`.
- Source candidate: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/path_aware_probe_v1_step512_closed_debt_c03_base_map_input.csv`.
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_PathAwareStep512C03ReviewPack.csv`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/path_aware_step512_c03_review_frozen_trace_metrics.csv`; 1/1 solved/A, coverage `0.5122549`, avg/max `2.54/6`, antiLocal `0.515`, support `0.858/d3`, local/follow `3/4`.
- Ray map for next step: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/path_aware_probe_v1_step512_c03_map_*`.
- Ray-constrained cavity smoke conclusion: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ray_constrained_cavity_fill_v1_step512_smoke_conclusion.md`. Single-chain center-out candidates can be safe but only reach `0.5159-0.5172`; two-chain/three-chain blind batches reach `0.52+` coverage but 20/20 unsolved, so high-coverage fill must be staged with post-map/trace settlement.

## Web Crossover Root Proof Pack - 2026-06-24

- `SGPRhythmLab_WebCrossoverV1RootProofT030Pack.asset`: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_WebCrossoverV1RootProofT030Pack.asset`, guid=`8e92cc9a9d504068ae1c4893760dacb7`. 5-level third-root proof pack, mounted in Demo; selected coverage `0.288-0.2978`.
- Source selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/web_crossover_v1_fill_t030_merged5_selected.csv`.
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_WebCrossoverV1RootProofT030Pack.csv`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/web_crossover_v1_root_proof_t030_frozen_trace_metrics.csv`.
- Frozen web gate: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/web_crossover_v1_root_proof_t030_frozen_webgate_web_roots.csv` and summary `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/web_crossover_v1_root_proof_t030_frozen_webgate_summary.md`.

## Seeded Direct-SGP Full Coverage Baseline - 2026-06-24

- `SGPRhythmLab_SeededDirectSGPMicroFrom030To095SweepReview4Pack.asset`: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SeededDirectSGPMicroFrom030To095SweepReview4Pack.asset`, guid=`3c094f1b94234aa49f408977bd397081`. Deprecated/buggy pack: metadata says `0.94-0.95`, but authored arrows were not replaced due `paths:` vs `arrows:` writer bug; actual assets remain 37 chains / 251 cells / coverage `0.3076`.
- `SGPRhythmLab_SeededDirectSGPMicroFrom030To095SweepFix1Review4Pack.asset`: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SeededDirectSGPMicroFrom030To095SweepFix1Review4Pack.asset`, guid=`ca03633336ec4b20a42d659be39d01a5`. Current Demo visual check pack; starts from one validated `0.30` support-lock parent and lets seeded Direct-SGP + micro-fill push to real `0.9436-0.9510` coverage.
- Frozen level dir: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/SeededDirectSGPMicroFrom030To095SweepFix1Review4/`.
- Source candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/seeded_direct_sgp_micro_from030_to095_sweep_fix1_smoke4_candidates.csv`; real authored arrows `141-157` chains, occupied `770-776`.
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_SeededDirectSGPMicroFrom030To095SweepFix1Review4Pack.csv`.
- Source trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/seeded_direct_sgp_micro_from030_to095_sweep_fix1_smoke4_trace_metrics.csv`; 4/4 `solved=False/processTier=Drop`, avg/max roughly `19-21/38-41`. This pack proves geometric fill capacity, not hard/playable production quality.

## Small Canvas Seeded Direct-SGP Review Pack - 2026-06-24

- `SGPRhythmLab_SeededDirectSGPSmallCanvas18x24Max36Review6Pack.asset`: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SeededDirectSGPSmallCanvas18x24Max36Review6Pack.asset`, guid=`c2d629dd67e342c48cd0efc3a467112d`. Current Demo activePack; validates the “shrink whole canvas, preserve parent motif, then let SGP drill” route.
- Frozen level dir: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/SeededDirectSGPSmallCanvas18x24Max36Review6/`.
- Source candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/seeded_direct_sgp_smallcanvas_18x24_max36_smoke6_candidates.csv`; base compacted to 18x24 with 28 kept chains / 9 dropped chains, final totalChains=36, coverage `0.463-0.509`.
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_SeededDirectSGPSmallCanvas18x24Max36Review6Pack.csv`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/seeded_direct_sgp_smallcanvas_18x24_max36_review6_frozen_trace_metrics.csv`; 6/6 solved, 2 B-tier, supportDepth preserved on the best samples, but outer/choice still above production target.

## Hub-Spoke Root Proof Pack - 2026-06-24

- `SGPRhythmLab_HubSpokeTrueV5RootProofT0288Pack.asset`: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_HubSpokeTrueV5RootProofT0288Pack.asset`, guid=`4700f5c8ee954d7da359c1185256a872`. 3-level hub-spoke proof pack at selected coverage `0.288`; Demo activePack is mounted here.
- Source selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hub_spoke_true_v5_root_proof_t0288_selected.csv`.
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_HubSpokeTrueV5RootProofT0288Pack.csv`.
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hub_spoke_true_v5_root_proof_t0288_frozen_trace_metrics.csv`.
- Frozen hub-spoke gate: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/hub_spoke_true_v5_root_proof_t0288_frozen_gate_hub_spoke_roots.csv`.

## Cascade Relay V1 Root Proof T0207

- Pack: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_CascadeRelayV1RootProofT0207Pack.asset
- Pack GUID: c7d563624d224126937c70db12e42430
- Manifest: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_CascadeRelayV1RootProofT0207Pack.csv
- Selected source CSV: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/cascade_relay_v1_root_proof_t0207_selected.csv
- Frozen trace: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/cascade_relay_v1_root_proof_t0207_frozen_trace_metrics.csv
- Frozen gate: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/cascade_relay_v1_root_proof_t0207_frozen_gate_cascade_roots.csv
- Contents: 3 levels, coverage 0.1924-0.2071, all solved A, all cascadeRelayCandidate=True.

## Split Key V1 Root Proof T0203

- Pack: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SplitKeyV1RootProofT0203Pack.asset
- Pack GUID: fc53728dbb824b9d8e724b1586d97d0b
- Manifest: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_SplitKeyV1RootProofT0203Pack.csv
- Selected source CSV: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/split_key_v1_root_proof_t0203_selected.csv
- Frozen trace: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/split_key_v1_root_proof_t0203_frozen_trace_metrics.csv
- Frozen gate: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/split_key_v1_root_proof_t0203_frozen_gate_split_key_roots.csv
- Contents: 3 levels, coverage 0.2010-0.2034, all solved A, all splitKeyCandidate=True.


## Mixed Root Family Review V1

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_MixedRootFamilyReviewV1Pack.asset`
- Pack GUID: `1dc9459927164d23899ab69b33b9b9f6`
- Manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_MixedRootFamilyReviewV1Pack.csv`
- Selected source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/mixed_root_family_review_v1_selected.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/mixed_root_family_review_v1_frozen_trace_metrics.csv`
- Identity audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/mixed_root_family_review_v1_identity_audit.csv`
- Contents: 12 levels, 2 each from support_lock, strict_dual, web_crossover, hub_spoke, cascade_relay, split_key. Frozen trace: 12/12 solved A.

## Small Canvas Outer-Frame Seeded Direct-SGP Review4

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SC18F8Max44Review4Pack.asset`
- Pack GUID: `68c0042da95a42f688a0dc8f77f93581`
- Frozen level dir: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/SC18F8R4/`
- Source candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/seeded_direct_sgp_smallcanvas_18x24_frame8_max44_smoke8_candidates.csv`
- Selected source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/seeded_direct_sgp_smallcanvas_18x24_frame8_max44_review4_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_SC18F8Max44Review4Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/seeded_direct_sgp_smallcanvas_18x24_frame8_max44_review4_frozen_trace_metrics.csv`
- Contents: 4 levels from one compacted 18x24 support-lock parent with 8 preseeded outer-frame chains and totalChains=44. Selected coverage `0.5602-0.6134`; frozen trace 4/4 solved A with supportDepth=3. Demo scene in `.worktrees/sgp-rhythm-lab` is mounted to this pack.

## Parent030 To 0.60 Small Canvas Review4

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_Parent030To060SmallCanvasReview4Pack.asset`
- Pack GUID: `d638964e6b734b7ab45abb08dbd53630`
- Frozen level dir: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/Parent030To060SmallCanvasReview4/`
- Input parent CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/opendebt_parent030_single_input.csv`
- Source candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/parent030_to060_smallcanvas18x24_frame8_max45_try3_candidates.csv`
- Selected source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/parent030_to060_smallcanvas18x24_frame8_max45_review4_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_Parent030To060SmallCanvasReview4Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/parent030_to060_smallcanvas_review4_frozen_trace_metrics.csv`
- Contents: 4 levels from clean `near_miss_filler_orientation_v1_probe3_v006` parent compacted to 18x24 and filled to `coverage=0.6019-0.6134`; 4/4 solved, 2 A + 2 B, all supportDepth=3. Demo scene in `.worktrees/sgp-rhythm-lab` is mounted to this pack.

## Original Seed Long-Chain Review V1

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedLongChainReviewV1Pack.asset`
- Pack GUID: `9d4fe47b51bb4d11b2e5525bbbe360e2`
- Selected source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_long_chain_review_v1_selected.csv`
- Contents: 8 `ImportedOriginal` reference seeds selected from `reference_seed_structure_top_complex_298.csv` for long-chain language review. Metrics span 42-157 chains, coverage `0.851-0.982`, avgChain `8.952-15.911`, maxChain `28-63`, longChainRate `0.400-0.554`.
- Demo scene in `.worktrees/sgp-rhythm-lab` is mounted to this pack. This is not a hard/root proof pack.

## Original Seed Long-Chain Skeleton Review V2

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedLongChainSkeletonReviewV2Pack.asset`
- Pack GUID: `d313fdfd4c504b66be8826758a8bbba5`
- Selected source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_long_chain_skeleton_review_v2_selected.csv`
- Contents: 8 `ImportedOriginal` reference seeds selected for long-chain full-board skeleton language. This supersedes V1 for visual review because V1 rows 4/7/8 were too sparse/local to count under the user’s intended skeleton definition.
- Demo scene in `.worktrees/sgp-rhythm-lab` is mounted to this pack.

## Original Seed Difficulty Skeleton T050 Review8

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedDifficultySkeletonT050Review8Pack.asset`
- Pack GUID: `88aa67560fa7477ea7e6f784bcc70e4e`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_long_chain_difficulty_skeleton_trace_root_v1_t050_candidates.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedDifficultySkeletonT050Review8Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_difficulty_skeleton_t050_review8_frozen_trace_metrics.csv`
- Contents: 8 trace-root extracted skeletons from original long-chain seeds. Best true causal skeleton is Arrowz_level_074: 52 chains, solved A, avg/max `3.96/8`, support `0.838/d4`; other rows are mostly choice-pressure/d2 skeletons. Demo scene is mounted to this pack.

## Original Seed Availability Peel Min8 Review8

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedAvailabilityPeelMin8Review8Pack.asset`
- Pack GUID: `78403564a5c14a2789262cc9acfb24e2`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_long_chain_availability_peel_v1_min8_candidates.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedAvailabilityPeelMin8Review8Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_availability_peel_min8_review8_frozen_trace_metrics.csv`
- Contents: 8 original long-chain seeds after iterative availability-shell peel (`MinAvailableToPeel=8`). 8/8 solved; retained high-coverage difficulty cores, e.g. level_510 support `0.889/d4`, Arrowz_level_095 support `0.810/d4`. Demo scene is mounted to this pack.

## Availability Peel Batch2 Review12

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_AvailabilityPeelBatch2Review12Pack.asset`
- Pack GUID: `fa0b5e3d61b64548a7c99baf48dff472`
- Source input: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/availability_peel_next24_input.csv`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/availability_peel_next16_v1_min8_candidates.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/availability_peel_next16_v1_min8_review12_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_AvailabilityPeelBatch2Review12Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/availability_peel_batch2_review12_frozen_trace_metrics.csv`
- Contents: 12 selected availability-peel skeletons from the next high-complexity seed batch; all solved, support d2+, non-Drop before freeze. Demo scene is mounted to this pack.

## Availability Peel Batch2 True Skeleton Review2

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_AvailabilityPeelBatch2TrueSkeletonReview2Pack.asset`
- Pack GUID: `95abf1a1f9ce4e9eaad1043ea90878b3`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/availability_peel_batch2_true_skeleton_review2_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_AvailabilityPeelBatch2TrueSkeletonReview2Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/availability_peel_batch2_true_skeleton_review2_frozen_trace_metrics.csv`
- Contents: 2 corrected true-skeleton samples from Batch2 after availability peel + causal-core trim. 2/2 solved; 26/32 chains; supportDepth=3. Demo scene is mounted to this pack.

## Original Seed Root Batch3 Review9

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedRootBatch3Review9Pack.asset`
- Pack GUID: `f6a5e78fd7cd49bd86d36cf01d8f7f53`
- Input CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_batch3_input48.csv`
- Peel candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_batch3_peel_min8_candidates.csv`
- Merged peel trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_batch3_peel_min8_trace_merged_metrics.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_batch3_review9_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedRootBatch3Review9Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_batch3_review9_frozen_trace_metrics.csv`
- Contents: 9 original-seed root candidates extracted via availability-shell peel from high-complexity reference seeds, selected for trace-visible d3/d4 support motif and bounded choices. Demo scene is mounted to this pack.

## Original Seed RoleGraph Root Proof V1 Review1

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedRoleGraphRootProofV1Review1Pack.asset`
- Pack GUID: `7a9f31a602d64fd9ad535c8a878a45ac`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_v1_level944_minclosure_candidate.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedRoleGraphRootProofV1Review1Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_root_proof_v1_review1_frozen_trace_metrics.csv`
- Contents: 1 minimal original-seed role-graph root from level_944 closure nodes; 9 chains, solved S, supportDepth=4. Demo scene is mounted to this pack.

## Original Seed RoleGraph Level944 Root030 Review1

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedRoleGraphLevel944Root030Review1Pack.asset`
- Pack GUID: `a3cd8a75d6cb4fd591cf8d9828e0cb5d`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_level944_root030_review1_selected.csv`
- Carrier candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_level944_carrier_search_v1_candidates.csv`
- Carrier trace merged: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_level944_carrier_search_v1_trace_merged_metrics.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedRoleGraphLevel944Root030Review1Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_level944_root030_review1_frozen_trace_metrics.csv`
- Contents: 1 original-seed root from level_944, 35 chains, coverage `0.3024691`, solved A, support `0.976/d4`, TrueHardCandidate. Demo scene is mounted to this pack.

## Original Seed RoleGraph Next5 Review

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedRoleGraphNext5ReviewPack.asset`
- Pack GUID: `9e2e67f9fb8649cf9d2d481ab8fda5c7`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_next5_review_selected.csv`
- Carrier candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_next5_carrier_v1_candidates.csv`
- Carrier trace merged: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_next5_carrier_v1_trace_merged_metrics.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedRoleGraphNext5ReviewPack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_next5_review_frozen_trace_metrics.csv`
- Contents: 5 original-seed rolegraph roots extracted via fixed trace nucleus plus carrier search. 5/5 solved; source families: level_792, Arrowz_level_182, Arrowz_level_232, Arrowz_level_154, Arrowz_level_264. Demo scene is mounted to this pack.

## Root Library Initial Families V1

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootLibraryInitialFamiliesV1Pack.asset`
- Pack GUID: `7c4cf2078e27463ab3c640d82bdd67fa`
- Source selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/mixed_root_family_review_v1_selected.csv`
- Catalog: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_library_initial_families_v1_catalog.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootLibraryInitialFamiliesV1Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_library_initial_families_v1_frozen_trace_metrics.csv`
- Contents: 12 initial generated/proof roots, 2 each from support_lock, strict_dual, web_crossover, hub_spoke, cascade_relay, split_key. This is the baseline generated-root library, not a production campaign pack.

## Root Library Original Seed RoleGraph V1

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootLibraryOriginalSeedRoleGraphV1Pack.asset`
- Pack GUID: `f142bc1c67394ea5a9fc166f513a0a68`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_library_original_seed_rolegraph_v1_selected.csv`
- Catalog: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_library_original_seed_rolegraph_v1_catalog.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootLibraryOriginalSeedRoleGraphV1Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_library_original_seed_rolegraph_v1_frozen_trace_metrics.csv`
- Contents: 4 original-seed rolegraph RootReviewCandidate rows after excluding NucleusOnly/ThinRoot rows; source families level_944, level_792, Arrowz_level_182, Arrowz_level_232.

## Original Seed RoleGraph Batch4 Review5

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedRoleGraphBatch4Review5Pack.asset`
- Pack GUID: `4a70f5e8e2b04ad2b4e43c4ff0786a91`
- Source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_batch4_sources.csv`
- Carrier candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_batch4_carrier_v1_candidates.csv`
- Prefiltered candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_batch4_carrier_v1_prefilter_top.csv`
- Prefilter trace merged: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_batch4_prefilter_trace_merged_metrics.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_batch4_review5_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedRoleGraphBatch4Review5Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_rolegraph_batch4_review5_frozen_trace_metrics.csv`
- Contents: 5 original-seed rolegraph RootReviewCandidate rows from source families Arrowz_level_055, Arrowz_level_120, level_699, level_730, level_810. Demo scene is mounted to this pack for review.

## Original Seed Strict Role Root V1 Review8

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedStrictRoleRootV1Review8Pack.asset`
- Pack GUID: `2256b922b4464fe7b27c9e3a572b2796`
- Source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_v1_sources.csv`
- Candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_v1_candidates.csv`
- Candidate trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_v1_trace_metrics.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_v1_review8_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedStrictRoleRootV1Review8Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_v1_review8_frozen_trace_metrics.csv`
- Contents: 8 strict role roots extracted from original seed residuals using role-labeled chains only, no coverage target. Demo scene is mounted to this pack.

## Original Seed Strict Role Root Batch5 V1 Review8

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedStrictRoleRootBatch5V1Review8Pack.asset`
- Pack GUID: `e8b42fb7fb8946c4a21d4e8372d05fbb`
- Source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_batch5_sources.csv`
- Candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_batch5_v1_candidates.csv`
- Candidate trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_batch5_v1_trace_metrics.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_batch5_v1_review8_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedStrictRoleRootBatch5V1Review8Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_batch5_v1_review8_frozen_trace_metrics.csv`
- Contents: 8 strict role roots from additional original-seed sources. Demo scene is mounted to this pack.

## Original Seed Strict Role Root Full V1 Review21

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedStrictRoleRootFullV1Review21Pack.asset`
- Pack GUID: `3f5cf33e5b6e4b2d8e44b5f2a38aa729`
- Source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_full_sources.csv`
- Candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_full_v1_candidates.csv`
- Catalog: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_full_v1_catalog.csv`
- Selected review CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_full_v1_review21_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedStrictRoleRootFullV1Review21Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_strict_role_root_full_v1_review21_frozen_trace_metrics.csv`
- Contents: 21 full-run StrictRootReview candidates from original seed strict role extraction, no coverage filler target. Frozen trace 21/21 solved; Demo scene in `.worktrees/sgp-rhythm-lab` is mounted to this pack for broad human filtering.

## Original Seed Root Extractable V1 Review9

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedRootExtractableV1Review9Pack.asset`
- Pack GUID: `c1e9a01e09d34a78b8bf0670e958c7f1`
- Extractability screen: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_extractability_gate_v1_screen.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_extractable_v1_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedRootExtractableV1Review9Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_extractable_v1_review9_frozen_trace_metrics.csv`
- Contents: 9 source-gated original-seed strict roots, selected from RootExtractableA/B only; 9/9 solved, S=1/A=8, no LocalEasy rows. Demo scene is mounted to this pack.

## Dynamic RoleMap SmallCanvas Review1

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_DynamicRoleMapSmallCanvasReview1Pack.asset`
- Pack GUID: `91bbd2d947f34878b6301b6b49392e1b`
- Selected input: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dynamic_rolemap_smallcanvas_review_pack_input.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_DynamicRoleMapSmallCanvasReview1Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/dynamic_rolemap_smallcanvas_review1_frozen_trace_abs_metrics.csv`
- Contents: 6-level visual progression pack for dynamic RoleMap fill on the 18x24 small canvas. Level 1 is the 0.613 solved parent; Levels 2-6 are solved one-chain dynamic-fill candidates through step3, with supportDepth preserved or improved to d4.

## All Seed Root Extractable V2 Review13

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_AllSeedRootExtractableV2Review13Pack.asset`
- Pack GUID: `8db16fca66f9417f990df524607548b3`
- Prefilter screen: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_root_source_prefilter_v1_screen.csv`
- Trace eligibility screen: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_root_source_trace_eligibility_v1_screen.csv`
- Eligible source trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_root_source_trace_eligible_v1_trace_metrics.csv`
- Strict candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_strict_role_root_v1_shortid_candidates.csv`
- Strict candidate trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_strict_role_root_v1_shortid_trace_metrics.csv`
- Catalog: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_strict_role_root_v1_catalog.csv`
- Extractability screen: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_root_extractability_gate_v2_screen.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_root_extractable_v2_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_AllSeedRootExtractableV2Review13Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/all_seed_root_extractable_v2_review13_frozen_trace_metrics.csv`
- Contents: 13 one-per-source original-seed strict role roots from the all-951 seed scan. Frozen trace is 13/13 solved with S=3/A=9/B=1; Demo scene in `.worktrees/sgp-rhythm-lab` is mounted to this pack.

## Root Canvas Variant V1B Review16

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootCanvasVariantV1BReview16Pack.asset`
- Pack GUID: `dc94d42f0ca34ecfb9d265855f14f4b7`
- Source roots: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_canvas_variant_v1_source_roots.csv`
- Candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_canvas_variant_v1b_candidates.csv`
- Candidate trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_canvas_variant_v1b_trace_metrics.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_canvas_variant_v1b_review16_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootCanvasVariantV1BReview16Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_canvas_variant_v1b_review16_frozen_trace_metrics.csv`
- Signature summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_canvas_variant_v1b_review16_signature_summary.md`
- Backbone summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_canvas_variant_v1b_review16_backbone_summary.md`
- Contents: 16 self-produced root/canvas variants from support_lock, strict_dual, web_crossover and hub_spoke roots; 16/16 solved, S=1/A=15. Demo scene in `.worktrees/sgp-rhythm-lab` is mounted to this pack.

## Root Variant Mixed V1 Review16

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantMixedV1Review16Pack.asset`
- Pack GUID: `e09071087b31411381ecea8cb88168d9`
- Mixed selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_mixed_v1_review16_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantMixedV1Review16Pack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_mixed_v1_review16_frozen_trace_metrics.csv`
- Signature summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_mixed_v1_review16_signature_summary.md`
- Backbone summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_mixed_v1_review16_backbone_summary.md`
- Contents: 16 self-produced root variants: 4 root families x (2 canvas_embedding + 2 peripheral_jitter_soft). Frozen trace 16/16 solved A; Demo scene is mounted to this pack.

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_experience_variant_v1_review15_selected.csv` | Root Experience Variant V1 review input | 15 peripheral-jitter-only variants; no mirror/rotation/pure canvas embedding; duplicate moved signatures removed. |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootExperienceVariantV1Review15Pack.asset` | Root Experience Variant V1 review pack | Current Demo pack for experiential root variants; GUID `a41c7df3082c4e87bdd15e2aee3370d4`. |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_experience_variant_v1_review15_frozen_trace_metrics.csv` | Root Experience Variant V1 frozen trace | 15/15 solved, all A-tier; TrueHardCandidate=6, HardPotential=6, MediumStructure=3. |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_experience_variant_v1_review15_backbone_summary.md` | Root Experience Variant V1 backbone summary | Reports 5 causal backbone roots and 10 backbone variants for the mounted experiential variant pack. |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVisiblePeripheralJitterV1Review3Pack.asset` | Root Visible Peripheral Jitter V1 probe pack | Current Demo probe after soft jitter was judged too subtle; GUID `b8c910e29ce84f72b83905d577836553`. |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_visible_peripheral_jitter_v1_review3_frozen_trace_metrics.csv` | Visible jitter V1 frozen trace | 3/3 solved A; use only to judge whether aggressive single-chain jitter has enough visible difference. |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootClusterRemapV1BReview6Pack.asset` | Root Cluster Remap V1B probe pack | Current Demo pack for cluster-translation variant review; GUID `c2ce4a8cd84e4e6db1fd34d0b6e97ff0`. |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_cluster_remap_v1b_review6_frozen_trace_metrics.csv` | Root Cluster Remap V1B frozen trace | 6/6 solved A; use to judge whether non-core cluster translation creates enough visible/experiential difference. |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootRoleZoneSwapV1Review2Pack.asset` | Root Role-Zone Swap V1 probe pack | Current Demo pack for non-zero role-zone swap review; GUID `d44b9b5752e44d71b37b8d79cfa2a619`. |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_role_zone_swap_v1_review2_frozen_trace_metrics.csv` | Root Role-Zone Swap V1 frozen trace | 2/2 solved, tiers S/A, both HardPotential; use to judge whether role-zone swap creates enough visible/experiential difference. |

| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootRoleZoneSwapV1Roots12Review1Pack.asset` | Root Role-Zone Swap V1 single proof | Current Demo pack after duplicated Review2 was rejected; one non-zero hub_spoke proof only, GUID `edb2b3dbca7f426089050a3d829fb356`. |
| `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_role_zone_swap_v1_roots12_review1_frozen_trace_metrics.csv` | Single proof frozen trace | 1/1 solved, S-tier, TrueHardCandidate; demonstrates old-root role-zone mutation can work technically but not as a diverse production source. |

## Causal Root Family One Each V1 Review6

- Pack: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_CausalRootFamilyOneEachV1Pack.asset
- Pack GUID: 4d9c124509544bebb0d177676f89a8fa
- Selected CSV: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/causal_root_family_one_each_v1_selected.csv
- Source/audit: .worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/mixed_root_family_review_v1_identity_audit.csv
- Frozen trace/audits: mixed_root_family_review_v1_frozen_trace_metrics.csv, mixed_root_family_review_v1_frozen_*_gate_*.csv
- Contents: one representative each for support_lock, strict_dual, web_crossover, hub_spoke, cascade_relay, split_key. Demo scene is mounted to this pack for root-family visual review.

## Campaign500 Hardening Analyzer V1

- Report folder: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/`.
- Latest calibrated run: `campaign_hardening_final_and_candidate_pool_20260624_224354_summary.csv`, `campaign_hardening_final_and_candidate_pool_20260624_224354_leak_rank.csv`, `campaign_hardening_final_and_candidate_pool_20260624_224354_top20_plan.csv`, `campaign_hardening_final_and_candidate_pool_20260624_224354_notes.md`.
- Review pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningLeakReviewPack.asset`; contains top 20 leak candidates from the latest analyzer run and is not auto-mounted to Demo.
- Sandbox V1 pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV1Pack.asset`; contains original/light/heavy copies of top 10 leak-priority levels.
- Sandbox V1 levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V1/`.
- Sandbox V1 report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v1_20260624_224903.csv`.
- Sandbox V2 pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV2Pack.asset`; contains 10 original/strong pairs, currently mounted to Demo for review.
- Sandbox V2 levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V2/`.
- Sandbox V2 report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v2_20260624_234908.csv`.
- Use this when selecting levels for difficulty hardening sandbox, especially for early free outer exits, high opening choices, weak dependency proxy, and boundary straight/short outer leak cleanup.

## Gate Vocabulary V1 Light Review Pack

- Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GateVocabularyV1LightReviewPack.asset`
- Pack guid: `0a8b5ee614ec49deb72f2bb146324904`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_GateVocabularyV1LightReviewPack.csv`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocabulary_v1_light_review_candidates.csv`
- Trace CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocabulary_v1_light_review_trace_metrics.csv`
- Purpose: inspect distinct gate/door designs inside shared-core strict-dual family; levels 1,2,4 are strict-dual trace candidates, level 3 is a rejected right-facing visual probe.

## Gate Vocabulary V1 Solved Skeleton Growth Review Pack

- Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GateVocabularyV1SolvedSkeletonGrowthReviewPack.asset`
- Pack guid: `584221c803cf4d5fa98ac9ac479176ae`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_GateVocabularyV1SolvedSkeletonGrowthReviewPack.csv`
- Selected source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocabulary_v1_solved_skeleton_growth_review_selected.csv`
- Skeleton trace CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocabulary_v1_skeleton_only_trace_metrics.csv`
- Conservative growth trace CSVs: `gate_vocabulary_v1_strict_dual_from_solved_skeleton_t020_b01_r*_trace_metrics.csv`
- Purpose: review solved strict-dual gate skeleton vocabulary and first safe-fill growth steps before pushing coverage higher.

## Gate Vocabulary V1 One-Per-Door Production Probe Pack

- Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GateVocabularyV1OnePerDoorProdReviewPack.asset`
- Pack GUID: `724ca47d6f744c088f20d60fe3412dfa`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocab_prod_one_per_door_v1_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_GateVocabularyV1OnePerDoorProdReviewPack.csv`
- Frozen trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_GateVocabularyV1OnePerDoorProdReviewPack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocab_prod_one_per_door_v1_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GateVocabularyV1OnePerDoorProdReviewFrozen`
- Contents: 4 solved strict-dual candidates, one per gate/door design. This is a low-coverage vocabulary review pack (`coverage 0.1091-0.1348`), not a final 0.30 production pack.

## Gate Vocabulary Door Push020 Mid Review Pack

- Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GateVocabularyDoorPush020MidReviewPack.asset`
- Pack GUID: `9a4ff7b38d774e3a83b0d13b5294a2a7`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocab_door_push020_mid_review4_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_GateVocabularyDoorPush020MidReviewPack.csv`
- Frozen trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_GateVocabularyDoorPush020MidReviewPack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocab_door_push020_mid_review_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GateVocabularyDoorPush020MidReviewFrozen`
- Contents: 4 one-per-door strict-dual candidates at coverage `0.1679-0.1814`; all solved, strictDualGateCandidate, TrueHardCandidate, A-tier.

## Gate Vocabulary Door MultiVar V1 Review8 Pack

- Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GateVocabularyDoorMultiVarV1Review8Pack.asset`
- Pack GUID: `85a0f9a3cf9b4a80a712c7db013d4c4f`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocab_door_multivar_v1_review8_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_GateVocabularyDoorMultiVarV1Review8Pack.csv`
- Frozen trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_GateVocabularyDoorMultiVarV1Review8Pack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocab_door_multivar_v1_review8_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GateVocabularyDoorMultiVarV1ReviewFrozen`
- Contents: 8 strict-dual door variants (4 door designs x 2 seed/fill variants). Frozen trace: 8/8 solved, strictDualGateCandidate, A-tier; 7 TrueHardCandidate and 1 HardPotential.

## Non-Door Root MultiVar V1 Review11 Pack

- Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_NonDoorRootMultiVarV1Review11Pack.asset`
- Pack GUID: `d2b359c580fc48a7ab06e61af81a4499`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/non_door_root_multivar_v1_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_NonDoorRootMultiVarV1Review11Pack.csv`
- Frozen trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_NonDoorRootMultiVarV1Review11Pack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/non_door_root_multivar_v1_review11_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/NonDoorRootMultiVarV1ReviewFrozen`
- Contents: 11 non-door root review levels: support_lock, web_crossover, hub_spoke, cascade_relay, split_key existing family pairs plus one newly recovered support_lock fill variant. Current Demo activePack points here.

## Gate Vocabulary Door Size Smoke V1 Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GateVocabularyDoorSizeSmokeV1Pack.asset`
- Pack GUID: `5d0ff4be64c94ad48788fa3766f67593`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocab_door_size_smoke_v1_candidates.csv`
- Absolute trace input: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocab_door_size_smoke_v1_candidates_abs.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocab_door_size_smoke_v1_selected.csv`
- Trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/gate_vocab_door_size_smoke_v1_trace_abs_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GateVocabularyDoorSizeSmokeV1Frozen`
- Contents: 8 non-mirror size variants: first 4 strict-dual door variants x `wide30_shift`/`tall40_shift`. Solved/strictDual 8/8; tall shift looked safer than wide shift in class retention.

## Non-Door Root Size Smoke V1 Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_NonDoorRootSizeSmokeV1Pack.asset`
- Pack GUID: `6e4a8fbaf2dd4fe9ab2ff1f718737b62`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/non_door_root_size_smoke_v1_candidates.csv`
- Absolute trace input: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/non_door_root_size_smoke_v1_candidates_abs.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/non_door_root_size_smoke_v1_selected.csv`
- Candidate trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/non_door_root_size_smoke_v1_trace_abs_metrics.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_NonDoorRootSizeSmokeV1Pack.csv`
- Frozen trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_NonDoorRootSizeSmokeV1Pack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/non_door_root_size_smoke_v1_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/NonDoorRootSizeSmokeV1Frozen`
- Contents: 6 non-door non-mirror size variants: hub_spoke, split_key, web_crossover x wide30/tall40. All solved/A; 3 TrueHardCandidate and 3 HardPotential. Support_lock size variants from the old Medium baseline were excluded.

## Non-Door Root Size Smoke V1 Plus Support Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_NonDoorRootSizeSmokeV1PlusSupportPack.asset`
- Pack GUID: `be8f304517d54a58b8fcdb9295504ce0`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/non_door_root_size_smoke_v1_plus_support_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_NonDoorRootSizeSmokeV1PlusSupportPack.csv`
- Frozen trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_NonDoorRootSizeSmokeV1PlusSupportPack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/non_door_root_size_smoke_v1_plus_support_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/NonDoorRootSizeSmokeV1PlusSupportFrozen`
- Contents: 8 non-door non-mirror size variants: support_lock, web_crossover, hub_spoke, split_key x wide30/tall40. Frozen trace 8/8 solved/A; 4 TrueHardCandidate + 4 HardPotential. Not currently mounted to Demo.

## Tonight Fullish Max44 V1 Review Pack

- Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_TonightFullishMax44V1ReviewPack.asset`
- Pack GUID: `2f4a0a26d8b44ac99d8443914bd1a2de`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/tonight_smallcanvas_max44_v1_selected6.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_TonightFullishMax44V1ReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/tonight_fullish_max44_v1_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/TonightFullishMax44V1Review`
- Contents: 6 small-canvas full-ish candidates from the verified 0.338 parent route. Frozen trace: 6/6 solved, all supportClosureBestDepth=4, avgChoices `3.61-5.20`, maxChoices `7-9`, outerExitHeadCount `8-9`; 5/6 MediumStructure and 1/6 LocalEasy. Current Demo activePack points here.

## Root Variant Library V1 Core Pack

- Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV1CorePack.asset`
- Pack GUID: `a9802eed58384d9eb06618041ff1b457`
- Core selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_core_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV1CorePack.csv`
- Frozen trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV1CorePack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_core_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVariantLibraryV1CoreFrozen`
- Contents: 29 core root/variant candidates, filtered to solved/A and TrueHardCandidate/HardPotential only. Includes strict_dual_gate, support_lock, web_crossover, hub_spoke, split_key plus non-mirror size variants. Current Demo activePack points here.

## Tonight Fullchain Growth Review5 Pack

- Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_TonightFullchainGrowthReview5Pack.asset`
- Pack GUID: `17ae8e3c914a41b2be6399f7258be1bf`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/tonight_fullchain_growth_review5_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_TonightFullchainGrowthReview5Pack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/tonight_fullchain_growth_review5_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/TonightFullchainGrowthReview5`
- Contents: 6 growth/boundary nodes from one verified ~0.30 parent. First 4 are A-tier strict-ish growth nodes up to coverage `0.5686275`; last 2 are B-tier MediumStructure density-boundary nodes up to `0.5955882`. Current `.worktrees/sgp-rhythm-lab` Demo activePack points here.

## Tonight Fullchain Growth Review6 Pack

- Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_TonightFullchainGrowthReview6Pack.asset`
- Pack GUID: `a2f7314b49f84817970ec0a4a6c65b44`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/tonight_fullchain_growth_review6_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_TonightFullchainGrowthReview6Pack.csv`
- Absolute trace input: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/tonight_fullchain_growth_review6_trace_input.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/tonight_fullchain_growth_review6_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/TonightFullchainGrowthReview6`
- Contents: 10 high-density growth nodes from the same verified ~0.30 parent, spanning coverage `0.5992647 -> 0.6384804`. Frozen trace is 10/10 solved; B-tier holds through `0.629902`, while `0.6335784` and `0.6384804` are solved/depth4 but process Drop due high openers/choice peak/near-outer patch bursts. Current `.worktrees/sgp-rhythm-lab` Demo activePack points here.

## Campaign500 Hardening Sandbox V3

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV3Pack.asset`
- Pack GUID: `28ab2ac1c2c809d47a3d82be185cb2d9`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V3`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v3_20260625_011430.csv`
- Contents: 10 original/pressure pairs from Campaign500 leak-ranked levels. Odd entries are original copies, even entries are V3 pressure-hardening outputs.
- V3 pressure averages over 10 outputs: opening choices `27.3 -> 22.2`, direct clearable outer exits `27.3 -> 22.2`, early average choices `26.42 -> 21.72`, full average choices `14.32 -> 11.65`, leak score `602.5 -> 507.6`, chains `145.0 -> 144.2`. Greedy solved 10/10.
- Current `Assets/ArrowMagic/Scenes/Demo.unity` activePack points to this V3 pack.

## Campaign500 Hardening Sandbox V4

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV4Pack.asset`
- Pack GUID: `be23e479c2ed2c74987c99ebef164ab1`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V4`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v4_20260625_064105.csv`
- Contents: 10 original/gate-v4 pairs from Campaign500 leak-ranked levels. Odd entries are original copies, even entries are V4 gate-fold outputs.
- V4 gate averages over 10 outputs: opening choices `27.8 -> 18.1`, direct clearable outer exits `27.8 -> 18.1`, early average choices `26.78 -> 17.59`, full average choices `14.71 -> 10.87`, leak score `610.8 -> 413.2`, chains `156.1 -> 151.1`. Greedy solved 10/10.
- Superseded for current review by V4.1; keep this pack/report for V4 baseline comparison.

## Campaign500 Hardening Sandbox V4.1

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV41Pack.asset`
- Pack GUID: `23f5588cd74a74c488e7752bbd4f825e`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V41`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v41_20260625_071610.csv`
- Contents: 8 V4/V4.1 pairs from the V4 accepted outputs. Odd entries are V4 copies, even entries are V4.1 second-pass gate-fold outputs.
- V4.1 averages over 8 outputs: chains `141.875 -> 140.5`, opening choices `18.75 -> 15.5`, direct clearable outer exits `18.75 -> 15.5`, early average choices `18.50 -> 16.26`, full average choices `10.84 -> 10.05`, leak score `422 -> 358.5`. Greedy solved 8/8.
- Current `Assets/ArrowMagic/Scenes/Demo.unity` activePack points to this V4.1 pack for review.

## Campaign500 Hardening Sandbox V4.2 Boundary Probe

- Pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV42Pack.asset`
- Pack GUID: `a8bd54c2ac26d1141862d736a4c745a1`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V42`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v42_20260625_072958.csv`
- Contents: 1 accepted V4.1/V4.2 pair plus skipped rows in the report; this is a saturation/boundary artifact, not the main demo review pack.
- Result: only campaign order `449` improved (`opening/direct outer 14 -> 13`, avg choices `8.069 -> 7.908`, leak `301 -> 281`); 7/8 scanned rows had `ops=0`.

## Campaign500 Hardening Sandbox V5 Visible Gate

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV5Pack.asset`
- Pack GUID: `045e629960371db42b4407cfdf5e8752`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V5`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v5_20260625_075227.csv`
- Contents: 6 original/visible-gate pairs. Odd entries are original copies, even entries are V5 outputs with short inserted hook/bend gate chains on direct outer-exit rays.
- V5 averages over 6 outputs: chains `125 -> 128`, arrow tiles `917.17 -> 926.17`, opening choices `28 -> 21.83`, direct clearable outer exits `28 -> 21.83`, early average choices `26.90 -> 19.98`, full average choices `14.81 -> 9.70`, leak score `602.33 -> 455.83`. Greedy solved 6/6.
- Superseded for current review by V6; keep this pack/report for visible-gate baseline comparison.

## Campaign500 Hardening Sandbox V6 Early Peel Gate

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV6Pack.asset`
- Pack GUID: `6e5820f0c0e73ff4bae751486923559b`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V6`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v6_20260625_082646.csv`
- Contents: 6 original/early-peel-gate pairs. Odd entries are original copies, even entries are V6 outputs with short hook/bend gates selected from simulated early peel waves.
- V6 averages over 6 outputs: chains `125 -> 129`, arrow tiles `917.17 -> 929.17`, opening choices `28 -> 23.33`, direct clearable outer exits `28 -> 23.33`, early average choices `26.90 -> 21.75`, full average choices `14.81 -> 10.95`, leak score `602.33 -> 486.67`. Greedy solved 6/6.
- Superseded for current Demo review by V7; keep this pack as the deeper-peel comparison baseline.

## Campaign500 Hardening Sandbox V7 Opening Peel Gate

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV7Pack.asset`
- Pack GUID: `056a052d84fe8794c9d59e7b18ece827`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V7`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v7_20260625_091100.csv`
- Contents: 4 original/opening-peel-gate pairs. Odd entries are original copies, even entries are V7 outputs with short opening-gate chains inserted onto current opening clearable direct-exit rays.
- V7 averages over 4 outputs: chains `122.75 -> 127.75`, arrow tiles `912.75 -> 927.75`, opening choices `30.25 -> 22.5`, direct clearable outer exits `30.25 -> 22.5`, full average choices `14.41 -> 9.95`, leak score `620 -> 464.75`. Greedy solved 4/4.
- Superseded for current Demo review by V8. V7 is stricter than V6 on opening reduction but is an add-chain tactical sandbox, not a production pass.

## Campaign500 Hardening Sandbox V8 Opening Rewire Gate

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV8Pack.asset`
- Pack GUID: `ec493029142ef3d4c8b34f2c1f28afcf`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V8`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v8_20260625_092655.csv`
- Contents: 4 original/opening-rewire-gate pairs. Odd entries are original copies, even entries are V8 outputs with current opening free chains merged/rewired; one accepted sample uses a short bridge, most accepted operations are adjacent chain rewires.
- V8 averages over 4 outputs: chains `150 -> 147.25`, arrow tiles `963.25 -> 965`, opening choices `26.75 -> 20`, direct clearable outer exits `26.75 -> 20`, full average choices `14.70 -> 10.89`, leak score `596.75 -> 466`. Greedy solved 4/4.
- Superseded for current Demo review by V9. V8 is the first hardening sandbox in this series that primarily reduces opening pressure by changing existing chain structure instead of adding standalone gate chains.

## Campaign500 Hardening Sandbox V9 Opening Outer Rewire Gate

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV9Pack.asset`
- Pack GUID: `9b28502a2d25e23459733bd8fcd480d9`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V9`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v9_20260625_095945.csv`
- Contents: 4 original/opening-outer-rewire-gate pairs. Odd entries are original copies, even entries are V9 outputs.
- V9 operations: bulk-flip direct outer heads inward, then run a post opening-rewire pass without fallback add-gates. Report includes both clearable outer exits and all direct outer exits.
- V9 averages over 4 outputs: chains `151 -> 150`, arrow tiles `1019 -> 1020.25`, opening choices `28 -> 18.5`, direct clearable outer exits `28 -> 18.5`, all direct outer exits `28 -> 18.5`, full average choices `14.72 -> 9.87`, leak score `614.5 -> 409`. Greedy solved 4/4.
- Superseded for current Demo review by V10.
- Limitation: V9 reduces one-ended direct outer heads well, but remaining two-ended boundary chains still show many outer exits. Next breakthrough should add a dedicated endpoint-inset / boundary double-ended chain merge operator rather than more flipping.

## Campaign500 Hardening Sandbox V10 Outer Exit Endpoint

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV10Pack.asset`
- Pack GUID: `2c430fadb59a22249a45f1bf01814753`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V10`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v10_20260625_104309.csv`
- Source report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v9_20260625_095945.csv`
- Contents: 3 V9-before/outer-exit-endpoint pairs. Odd entries are V9 outputs before endpoint hardening, even entries are V10 endpoint outputs.
- V10 operations: endpoint reroute moves a direct outer chain head to a nearby empty cell around its neck; endpoint trim removes a short boundary-facing head segment. Every accepted step must keep Greedy solved and reduce all direct outer exits.
- V10 averages over 3 accepted outputs, measured from V9 source to V10 output: chains `139.67 -> 139.67`, arrow tiles `997 -> 988.33`, opening choices `18 -> 12.33`, direct clearable outer exits `18 -> 12.33`, all direct outer exits `18 -> 12.33`, full average choices `10.28 -> 7.31`, leak score `395 -> 275.33`. Greedy solved 3/3.
- Skipped sample: L449 only improved direct outer exits `20 -> 19`, so it was rejected under the outer-exit-focused acceptance rule.
- Superseded for current Demo review by V11.

## Campaign500 Hardening Sandbox V11 Multi-Layer Outer Exit

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV11Pack.asset`
- Pack GUID: `29a79c0bec2fdd740a603c8e8bf06340`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V11`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v11_20260625_111032.csv`
- Source report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v10_20260625_104309.csv`
- Contents: 3 V10-before/multi-layer-V11 pairs. Odd entries are V10 endpoint outputs before V11, even entries are V11 outputs.
- V11 operations: simulate the first 4 Greedy peel waves, collect chains that are clearable in those early waves and whose head ray reaches the outside after earlier layers clear, then apply future-outer orientation flip, endpoint reroute, and trim. Every accepted step keeps Greedy solved and reduces multi-layer peel outer risk.
- V11 averages over 3 outputs, measured from V10 source to V11 output: chains `139.67 -> 139.67`, arrow tiles `988.33 -> 981`, opening choices `12.33 -> 12.33`, all direct outer exits `12.33 -> 12.33`, peel-layer outer exits `51 -> 37`, future peel outer exits `38.67 -> 24.67`, full average choices `7.31 -> 5.72`. Greedy solved 3/3.
- Important interpretation: V11 does not yet reduce current-frame direct outer exits beyond V10; it reduces the second/third/fourth-layer outer exits that appear after the initial clearable layer is removed. Current `Assets/ArrowMagic/Scenes/Demo.unity` activePack points to this V11 pack for review.

## Campaign500 Hardening V12 PBE/NEE Classification

- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_v12_pbe_nee_20260625_112948.csv`
- Source report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v11_20260625_111032.csv`
- Contents: analysis-only CSV over the 3 V10-before/V11-after pairs; no new level pack and no Demo change.
- Definitions: `PBE` = persistent boundary exit whose head ray already reaches outside on the initial board; `NEE` = newly exposed exit whose head ray only reaches outside after earlier peel waves clear blockers.
- Result: V10-before averages direct outer `12.33`, peel outer `51`, future outer `38.67`, PBE `12.33`, future PBE `0`, NEE `38.67`. V11-after averages direct outer `12.33`, peel outer `37`, future outer `24.67`, PBE `12.33`, future PBE `0`, NEE `24.67`.
- Interpretation: in this sample, V11's future-layer improvement is entirely NEE reduction. The remaining current-frame direct outer problem is PBE/wave0 and needs boundary structure repair; the early continuous sweep problem is NEE and should be treated by peel-aware propagation gates.

## Campaign500 Hardening Sandbox V12 BDR-lite Boundary Rewire

- Demo pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV12BDRPack.asset`
- Pack GUID: `22d9ef7c9eba2844d8a2af3166daad93`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V12BDR`
- Report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v12_bdr_20260625_114050.csv`
- Source report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v11_20260625_111032.csv`
- Contents: 3 V11-before/V12BDR pairs. Odd entries are V11 outputs before boundary rewiring, even entries are V12BDR outputs.
- Operation: BDR-lite prepends a small one-cell hook at selected current direct-outer/PBE endpoints. It does not add chains; it adds 1-2 tiles per accepted sample and requires Greedy solvability, current direct outer/PBE drop, and NEE non-regression.
- Result over 3 outputs from V11 to V12BDR: chains `139.67 -> 139.67`, arrow tiles `981 -> 983`, opening choices `12.33 -> 10.67`, all direct outer exits `12.33 -> 10.67`, peel outer exits `37 -> 32`, future peel outer exits `24.67 -> 21.33`, full average choices `5.72 -> 5.36`. Greedy solved 3/3.
- Interpretation: BDR-lite is directionally correct but still mild. It proves current PBE can be reduced without NEE rebound, but production-level impact likely needs stronger boundary double-end restructuring or two-cell/merge-based stitching.

## Root Variant Library V1.2 Core Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV12CorePack.asset`
- Pack GUID: `1683b2f5c1aa4a129d36f4bd00e2efff`
- Core selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_2_core_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV12CorePack.csv`
- Absolute trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV12CorePack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_2_core_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVariantLibraryV12CoreFrozen`
- Contents: 35 root/variant candidates across strict_dual_gate, support_lock, web_crossover, hub_spoke, split_key, and cascade_relay. Current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.3 Core Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV13CorePack.asset`
- Pack GUID: `ba7c9cf303914fc695b55e69be80763e`
- Core selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_3_core_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV13CorePack.csv`
- Absolute trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV13CorePack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_3_core_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVariantLibraryV13CoreFrozen`
- Contents: 36 root/variant candidates across strict_dual_gate, support_lock, web_crossover, hub_spoke, split_key, and cascade_relay. V1.3 adds one trace/classifier-approved non-size `hub_spoke` variant to V1.2; current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.4 Core Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV14CorePack.asset`
- Pack GUID: `3c1bfa73bb1c4c2c885dfbfca5849f7b`
- Core selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_4_core_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV14CorePack.csv`
- Absolute trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV14CorePack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_4_core_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVariantLibraryV14CoreFrozen`
- Contents: 37 root/variant candidates. V1.4 adds the `wide30_shift` size variant of the newly admitted `hub_spoke` V1.3 root; current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.5 Core Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV15CorePack.asset`
- Pack GUID: `f1f40d935d264f729fa7390bde978ac9`
- Core selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_5_core_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV15CorePack.csv`
- Absolute trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV15CorePack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_5_core_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVariantLibraryV15CoreFrozen`
- Contents: 39 root/variant candidates. V1.5 adds two non-size `support_lock` variants to V1.4; current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.6 Core Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV16CorePack.asset`
- Pack GUID: `d6dbde4de6d94b07be26fcb17779ab7e`
- Core selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_6_core_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV16CorePack.csv`
- Absolute trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV16CorePack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_6_core_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVariantLibraryV16CoreFrozen`
- Contents: 43 root/variant candidates. V1.6 balances web_crossover and split_key to 6 each by adding unique wide/tall size variants that pass family classifiers; current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.7 Core Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV17CorePack.asset`
- Pack GUID: `80de53d213124a79b9e87fe1dd4cfe05`
- Core selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_7_core_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV17CorePack.csv`
- Absolute trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV17CorePack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_7_core_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVariantLibraryV17CoreFrozen`
- Contents: 44 root/variant candidates. Non-door families are balanced at 6 each; strict_dual_gate remains 14. Current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.8 Balanced Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV18BalancedReviewPack.asset`
- Pack GUID: `56cc167e1e3d4c7e9824595d39f67098`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_8_balanced_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV18BalancedReviewPack.csv`
- Absolute trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV18BalancedReviewPack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_8_balanced_review_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVariantLibraryV18BalancedReviewFrozen`
- Contents: 36 candidates, exactly 6 per family across support_lock, strict_dual_gate, hub_spoke, web_crossover, split_key, and cascade_relay. Current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here for manual review.

## Root Variant Library V1.9 New Variants Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV19NewVariantsReviewPack.asset`
- Pack GUID: `dd827c0c9cf148b3840c571946af7fc2`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_9_new_variants_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV19NewVariantsReviewPack.csv`
- Absolute trace manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV19NewVariantsReviewPack_trace_manifest_abs.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_9_new_variants_review_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVariantLibraryV19NewVariantsReviewFrozen`
- Audit inputs: `root_variant_library_v1_8_balanced_review_geometry_audit.csv`, `root_variant_library_v1_8_balanced_review_similarity.csv`, `root_variant_library_v1_8_balanced_review_guide.md`.
- Contents: 25 levels grouped as source controls plus new variants: cascade_relay 1+3, hub_spoke 1+1, split_key 1+4, strict_dual_gate 3+3, support_lock 1+1, web_crossover 1+5. Frozen trace: 25/25 solved, all A-tier, 21 TrueHardCandidate + 4 HardPotential. Current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.10 Hub/Support Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV110HubSupportReviewPack.asset`
- Pack GUID: `fd02a36230034d6ea401bed00ded456b`
- Source roots CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_10_hub_support_source_roots.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_10_hub_support_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV110HubSupportReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_10_hub_support_review_frozen_trace_metrics.csv`
- Joined frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_10_hub_support_review_frozen_trace_joined.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVariantLibraryV110HubSupportReviewFrozen`
- Intermediate candidate reports: `root_variant_library_v1_10_hub_support_jitter_mild_*` and `root_variant_library_v1_10_hub_support_cluster_mild_*` under `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- Contents: 20 levels = 7 source controls plus 13 non-size hub/support variants. Frozen trace: 20/20 solved, all A-tier; variant mix is hub_spoke 7 new and support_lock 6 new. Current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.11 Hub/Support Size Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV111HubSupportSizeReviewPack.asset`
- Pack GUID: `17f503e6c015427b8dc3e3812422ea66`
- Size source variants CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_10_hub_support_size_source_variants.csv`
- Size smoke candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_10_hub_support_size_smoke_candidates.csv`
- Size smoke trace join: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_10_hub_support_size_smoke_joined_trace.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_11_hub_support_size_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV111HubSupportSizeReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_11_hub_support_size_review_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV111SizeFrozen`
- Contents: 26 non-mirror size variants from V1.10 hub/support variants (`wide30_shift` + `tall40_shift`). Frozen trace: 26/26 solved, all A-tier, 8 TrueHardCandidate + 18 HardPotential. Current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.12 Other Roots Size Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV112OtherRootsSizeReviewPack.asset`
- Pack GUID: `a096dfe33bbc455b96ea4c2c284cdbc9`
- Size source variants CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_12_other_roots_size_source_variants.csv`
- Size smoke candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_12_other_roots_size_smoke_candidates.csv`
- Size smoke trace join: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_12_other_roots_size_smoke_joined_trace.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_12_other_roots_size_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV112OtherRootsSizeReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_12_other_roots_size_review_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV112SizeFrozen`
- Contents: 23 non-mirror size variants from V1.9 `cascade_relay`, `split_key`, `strict_dual_gate`, and `web_crossover` new variants. Frozen trace: 23/23 solved, all A-tier, 13 TrueHardCandidate + 10 HardPotential; strict-dual retained rows keep `strictDualGateCandidate=True`. Current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.13 Cascade/Strict Cluster Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV113CascadeStrictClusterReviewPack.asset`
- Pack GUID: `3d575d3a2da44401807ba42005285ff1`
- Frozen source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_13_cascade_strict_cluster_source_frozen.csv`
- Cluster candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_13_cascade_strict_cluster_mild_candidates.csv`
- Cluster joined trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_13_cascade_strict_cluster_mild_joined_trace.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_13_cascade_strict_cluster_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV113CascadeStrictClusterReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_13_cascade_strict_cluster_review_frozen_trace_metrics.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV113ClusterFrozen`
- Negative spatial recomposition diagnostics: `root_variant_library_v1_13_thin_roots_spatial_*` under `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- Contents: 7 levels = 3 source controls plus 4 conservative cluster variants for `cascade_relay` and `strict_dual_gate`. Frozen trace: 7/7 solved, all A-tier; strict-dual variants keep `strictDualGateCandidate=True`. Current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.14b Consolidated Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV114bConsolidatedReviewPack.asset`
- Pack GUID: `375e9f8eb8e6443d938e245fb558e400`
- Input pool CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_14_consolidated_pool_all.csv`
- First-pass selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_14_consolidated_review_selected.csv`
- Final selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_14b_consolidated_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV114bConsolidatedReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_14b_consolidated_review_frozen_trace_metrics.csv`
- Joined frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_14b_consolidated_review_frozen_trace_joined.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV114bConsolidatedFrozen`
- Contents: 45 deduped review candidates across six root families. Distribution: cascade_relay 8, hub_spoke 8, strict_dual_gate 8, support_lock 8, web_crossover 8, split_key 5. Frozen trace: 45/45 solved, all A-tier, 36 TrueHardCandidate + 9 HardPotential. All strict_dual rows keep `strictDualGateCandidate=True`. Current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Root Variant Library V1.15c Consolidated Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV115cConsolidatedReviewPack.asset`
- Pack GUID: `7f7307ed6ac9454186e7115eaaba24c4`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_15c_consolidated_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV115cConsolidatedReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_15c_consolidated_review_frozen_trace_metrics.csv`
- Joined frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_15c_consolidated_review_frozen_trace_joined.csv`
- Split gate audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_15c_consolidated_review_frozen_split_gate_split_key_roots.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV115cConsolidatedFrozen`
- Split supplementary reports: `root_variant_library_v1_15_split_canvas_more_*`, `root_variant_library_v1_15_split_canvas_spatial_joined_trace_gate.csv`; destructive diagnostics retained as `root_variant_library_v1_15_split_rolezone_*`, `root_variant_library_v1_15_split_cluster_*`, and `root_variant_library_v1_15_split_spatial_recompose_*`.
- Contents: 48 balanced review candidates, 8 each across cascade_relay, hub_spoke, split_key, strict_dual_gate, support_lock, and web_crossover. Frozen trace: 48/48 solved, all A-tier, 40 TrueHardCandidate + 8 HardPotential. All 8 `split_key` rows keep `splitKeyCandidate=True`. Current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Cascade Relay Recovery V1

- Base CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/cascade_relay_recovery_v1_base.csv`
- Recovery trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/cascade_relay_recovery_v1_t026_b02_r2_trace_metrics.csv`
- Cascade classifier: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/cascade_relay_recovery_v1_t026_b02_r2_gate_cascade_roots.csv`
- Size smoke candidates/trace: `cascade_relay_size_smoke_v1_candidates.csv`, `cascade_relay_size_smoke_v1_trace_metrics.csv`, `cascade_relay_size_smoke_v1_gate_cascade_roots.csv`
- Core-admitted rows: recovery c16/c19/c07 plus tall40_shift size variants c02/c04; wide30_shift size variants are solved/cascade but MediumStructure and excluded.

## Root Variant Library V1.16 Size Nonrecursive Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV116SizeNonrecursiveReviewPack.asset`
- Pack GUID: `fe5c4868615449c094b867da86e8417c`
- Source rows CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_16_size_source_nonrecursive.csv`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_16_size_nonrecursive_candidates.csv`
- Candidate trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_16_size_nonrecursive_metrics.csv`
- Joined candidate/gate audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_16_size_nonrecursive_joined_trace_gate.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_16_size_nonrecursive_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV116SizeNonrecursiveReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_16_size_nonrecursive_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_16_size_nonrecursive_review_frozen_trace_joined.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV116SizeNonrecursiveFrozen`
- Contents: 31 non-mirror wide/tall canvas variants from V1.15c source/non-size rows. Frozen trace: 31/31 solved, all A-tier, 19 TrueHardCandidate + 12 HardPotential. Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack for visual review.

## Root Variant Library V1.17 Cascade/Hub Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV117CascadeHubReviewPack.asset`
- Pack GUID: `e15f131e6a104b1aa3b3fe2093b0206c`
- Source rows CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_17_cascade_hub_source_frozen.csv`
- All first-pass candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_17_cascade_hub_candidates_all.csv`
- All first-pass trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_17_cascade_hub_metrics.csv`
- First-pass joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_17_cascade_hub_joined_trace_gate.csv`
- Hub second-pass candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_17_hub_candidates_all.csv`
- Hub second-pass joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_17_hub_joined_trace_gate.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_17_cascade_hub_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV117CascadeHubReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_17_cascade_hub_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_17_cascade_hub_review_frozen_trace_joined.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV117CascadeHubFrozen`
- Contents: 10 non-size variants, 5 cascade_relay and 5 hub_spoke. Frozen trace: 10/10 solved, all A-tier, 4 TrueHardCandidate + 6 HardPotential. Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack for visual review.

## Root Variant Library V1.18 Cascade/Hub Size Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV118CascadeHubSizeReviewPack.asset`
- Pack GUID: `a72095f2ae7d4b598da21dc7e0856ce8`
- Source rows CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_18_cascade_hub_size_source.csv`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_18_cascade_hub_size_candidates.csv`
- Candidate trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_18_cascade_hub_size_metrics.csv`
- Candidate joined gate audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_18_cascade_hub_size_joined_trace_gate.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_18_cascade_hub_size_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV118CascadeHubSizeReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_18_cascade_hub_size_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_18_cascade_hub_size_review_frozen_trace_joined.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV118CascadeHubSizeFrozen`
- Contents: 9 non-mirror size variants from V1.17 cascade/hub non-size variants. Frozen trace: 9/9 solved, all A-tier, 3 TrueHardCandidate + 6 HardPotential. Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack for visual review.

## Root Variant Library V1.19 Consolidated Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV119ConsolidatedReviewPack.asset`
- Pack GUID: `7bb6312a6fa34784be320bebdaad03ba`
- Input pool CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_19_consolidated_pool_all.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_19_consolidated_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV119ConsolidatedReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_19_consolidated_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_19_consolidated_review_frozen_trace_joined.csv`
- Family gate audits: `root_variant_library_v1_19_consolidated_review_frozen_gate_cascade_cascade_roots.csv`, `root_variant_library_v1_19_consolidated_review_frozen_gate_hub_hub_spoke_roots.csv`, `root_variant_library_v1_19_consolidated_review_frozen_gate_split_split_key_roots.csv`, `root_variant_library_v1_19_consolidated_review_frozen_gate_web_web_roots.csv` under `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV119ConsolidatedFrozen`
- Contents: 52 review candidates: cascade_relay 10, hub_spoke 10, split_key 8, strict_dual_gate 8, support_lock 8, web_crossover 8. Frozen trace: 52/52 solved, all A-tier, 42 TrueHardCandidate + 10 HardPotential. Intended-family gate pass after frozen trace: 47/52; cascade 8/10, hub 8/10, split 8/8, strict dual 8/8, support 8/8, web 7/8. Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack for visual review.

## Root Variant Library V1.20 Identity-Clean Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV120IdentityCleanReviewPack.asset`
- Pack GUID: `4adcf0892b1b44c4930e971011f750a6`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_20_identity_clean_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV120IdentityCleanReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_20_identity_clean_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_20_identity_clean_review_frozen_trace_joined.csv`
- Family gate audits: `root_variant_library_v1_20_identity_clean_review_frozen_gate_cascade_cascade_roots.csv`, `root_variant_library_v1_20_identity_clean_review_frozen_gate_hub_hub_spoke_roots.csv`, `root_variant_library_v1_20_identity_clean_review_frozen_gate_split_split_key_roots.csv`, `root_variant_library_v1_20_identity_clean_review_frozen_gate_web_web_roots.csv` under `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV120IdentityCleanFrozen`
- Contents: 47 identity-clean review candidates: cascade_relay 8, hub_spoke 8, split_key 8, strict_dual_gate 8, support_lock 8, web_crossover 7. Frozen trace: 47/47 solved, all A-tier, 37 TrueHardCandidate + 10 HardPotential. All intended family gates pass after frozen trace. Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack for visual review and future production-stat baselining.

## Root Variant Library V1.21 Balanced-Clean Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV121BalancedCleanReviewPack.asset`
- Pack GUID: `c6e32e5f87e94afba369d42eba62708e`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_21_balanced_clean_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV121BalancedCleanReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_21_balanced_clean_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_21_balanced_clean_review_frozen_trace_joined.csv`
- Family gate audits: `root_variant_library_v1_21_balanced_clean_review_frozen_gate_cascade_cascade_roots.csv`, `root_variant_library_v1_21_balanced_clean_review_frozen_gate_hub_hub_spoke_roots.csv`, `root_variant_library_v1_21_balanced_clean_review_frozen_gate_split_split_key_roots.csv`, `root_variant_library_v1_21_balanced_clean_review_frozen_gate_web_web_roots.csv` under `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV121BalancedCleanFrozen`
- Contents: 48 balanced identity-clean review candidates, 8 each across cascade_relay, hub_spoke, split_key, strict_dual_gate, support_lock, and web_crossover. Frozen trace: 48/48 solved, all A-tier, 38 TrueHardCandidate + 10 HardPotential. All intended family gates pass after frozen trace. Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack and this is the current best root-variant-library baseline.

## Root Variant Library V1.22 Size-Nonrecursive Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV122SizeNonrecursiveReviewPack.asset`
- Pack GUID: `ea936cfad65d418aa03d3b6c24855c0e`
- Source rows CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_22_size_source_nonrecursive.csv`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_22_size_nonrecursive_candidates.csv`
- Candidate trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_22_size_nonrecursive_metrics.csv`
- Joined candidate/gate audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_22_size_nonrecursive_joined_trace_gate.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_22_size_nonrecursive_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV122SizeNonrecursiveReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_22_size_nonrecursive_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_22_size_nonrecursive_review_frozen_trace_joined.csv`
- Family gate audits: `root_variant_library_v1_22_size_nonrecursive_review_frozen_gate_cascade_cascade_roots.csv`, `root_variant_library_v1_22_size_nonrecursive_review_frozen_gate_hub_hub_spoke_roots.csv`, `root_variant_library_v1_22_size_nonrecursive_review_frozen_gate_split_split_key_roots.csv`, `root_variant_library_v1_22_size_nonrecursive_review_frozen_gate_web_web_roots.csv` under `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV122SizeNonrecursiveFrozen`
- Contents: 38 non-mirror size variants from V1.21 source/non-size rows. Distribution: cascade_relay 7, hub_spoke 3, split_key 4, strict_dual_gate 8, support_lock 8, web_crossover 8. Frozen trace: 38/38 solved, 37 A + 1 S, 22 TrueHardCandidate + 16 HardPotential. All retained rows pass intended family gate after frozen trace. Demo in `.worktrees/sgp-rhythm-lab` is mounted to this pack for size-expansion visual review.

## Root Variant Library V1.23 Hub Conservative Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV123HubConservativeReviewPack.asset`
- Pack GUID: `a4f8f06e9b88473abf3ed4e58e416723`
- Source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_23_hub_conservative_source.csv`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_23_hub_conservative_candidates_all.csv`
- Joined candidate/gate audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_23_hub_conservative_joined_trace_gate.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_23_hub_conservative_review_selected.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_23_hub_conservative_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_23_hub_conservative_review_frozen_trace_joined.csv`
- Contents: 6 conservative hub-spoke non-size variants. Frozen trace: 6/6 solved, all A-tier, 6/6 hub-spoke identity pass.

## Root Variant Library V1.24 Hub Conservative Size Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV124HubConservativeSizeReviewPack.asset`
- Pack GUID: `d375fa699a2846f6a20c7a76c12dcc7e`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_24_hub_conservative_size_candidates.csv`
- Joined candidate/gate audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_24_hub_conservative_size_joined_trace_gate.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_24_hub_conservative_size_review_selected.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_24_hub_conservative_size_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_24_hub_conservative_size_review_frozen_trace_joined.csv`
- Contents: 4 non-mirror hub-spoke size variants. Frozen trace: 4/4 solved, all A-tier, 4/4 hub-spoke identity pass.

## Root Variant Library V1.25 Split Conservative Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV125SplitConservativeReviewPack.asset`
- Pack GUID: `bc9bff8f450149488a34f9a1347a7c60`
- Source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_25_split_conservative_source.csv`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_25_split_conservative_candidates_all.csv`
- Joined candidate/gate audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_25_split_conservative_joined_trace_gate.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_25_split_conservative_review_selected.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_25_split_conservative_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_25_split_conservative_review_frozen_trace_joined.csv`
- Contents: 4 source-capped split-key non-size variants. Frozen trace: 4/4 solved, all A-tier, 4/4 split-key identity pass.

## Root Variant Library V1.26 Split Conservative Size Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV126SplitConservativeSizeReviewPack.asset`
- Pack GUID: `e6ec4727c8524d9ca53d4aacb3295f98`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_26_split_conservative_size_candidates.csv`
- Joined candidate/gate audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_26_split_conservative_size_joined_trace_gate.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_26_split_conservative_size_review_selected.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_26_split_conservative_size_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_26_split_conservative_size_review_frozen_trace_joined.csv`
- Contents: 7 dynamic-size split-key variants. Frozen trace: 7/7 solved, all A-tier, 7/7 split-key identity pass.

## Root Variant Library V1.27 Hub/Split Tonight Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV127HubSplitTonightReviewPack.asset`
- Pack GUID: `f73fdf8cefb84ff1aa5571accdd4ccf5`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_27_hub_split_tonight_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV127HubSplitTonightReviewPack.csv`
- Frozen trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_27_hub_split_tonight_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_27_hub_split_tonight_review_frozen_trace_joined.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV127HubSplitTonightFrozen`
- Contents: 21 levels = hub root 6, hub size 4, split root 4, split size 7. Frozen trace: 21/21 solved, 21/21 intended family gate pass. Superseded by V1.28 as the current balanced review pack.

## Root Variant Library V1.28 Balanced Production Library Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV128BalancedProductionLibraryPack.asset`
- Pack GUID: `c81950ad0c0d43ac9dd91715c9d78ef7`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_28_balanced_production_library_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV128BalancedProductionLibraryPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_28_balanced_production_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_28_balanced_production_frozen_trace_joined.csv`
- Frozen joined summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_28_balanced_production_frozen_trace_joined_summary.md`
- Family gate audits: `root_variant_library_v1_28_balanced_production_frozen_gate_cascade_cascade_roots.csv`, `root_variant_library_v1_28_balanced_production_frozen_gate_hub_hub_spoke_roots.csv`, `root_variant_library_v1_28_balanced_production_frozen_gate_split_split_key_roots.csv`, `root_variant_library_v1_28_balanced_production_frozen_gate_web_web_roots.csv` under `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV128BalancedProductionFrozen`
- Contents: 72 levels, 12 each for `cascade_relay`, `hub_spoke`, `split_key`, `strict_dual_gate`, `support_lock`, and `web_crossover`. Frozen trace: 72/72 solved, 72/72 intended family gate pass. Superseded as Demo target by V1.30, but remains the balanced production-library baseline.

## Root Variant Library V1.29 Four-Family Non-Size Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV129FourFamReviewPack.asset`
- Pack GUID: `9f604bc426124186bab65ebaf9ccd478`
- Source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_29_expansion_source_four_families.csv`
- Candidate CSVs: `root_variant_library_v1_29_fourfam_candidates_all.csv`, `root_variant_library_v1_29_web_mild_jitter_candidates.csv`
- Candidate joined audits: `root_variant_library_v1_29_fourfam_joined_trace_gate.csv`, `root_variant_library_v1_29_fourfam_all_joined_trace_gate.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_29_fourfam_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV129FourFamReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_29_fourfam_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_29_fourfam_review_frozen_trace_joined.csv`
- Frozen joined summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_29_fourfam_review_frozen_trace_joined_summary.md`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV129FourFamFrozen`
- Contents: 16 non-size variants, 4 each for `cascade_relay`, `strict_dual_gate`, `support_lock`, and `web_crossover`. Frozen trace: 16/16 solved, 16/16 intended family gate pass.

## Root Variant Library V1.30 Four-Family Size Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV130FourFamSizeReviewPack.asset`
- Pack GUID: `0e14f4e499e54d089e0a92a1a81e7b27`
- Source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_30_fourfam_size_source.csv`
- Candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_30_fourfam_size_candidates.csv`
- Candidate joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_30_fourfam_size_joined_trace_gate.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_30_fourfam_size_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV130FourFamSizeReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_30_fourfam_size_review_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_30_fourfam_size_review_frozen_trace_joined.csv`
- Frozen joined summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_30_fourfam_size_review_frozen_trace_joined_summary.md`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV130FourFamSizeFrozen`
- Contents: 16 non-mirror size variants from V1.29 rows, 4 each for `cascade_relay`, `strict_dual_gate`, `support_lock`, and `web_crossover`. Frozen trace: 16/16 solved, 16/16 intended family gate pass. Superseded as Demo target by V1.31, but remains the focused four-family size-expansion review pack.

## Root Variant Library V1.31 Extended Balanced Review Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV131ExtendedBalancedReviewPack.asset`
- Pack GUID: `91a29088725441d3b604fa2e66f8d71e`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_31_extended_balanced_review_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV131ExtendedBalancedReviewPack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_31_extended_balanced_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_31_extended_balanced_frozen_trace_joined.csv`
- Frozen joined summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/root_variant_library_v1_31_extended_balanced_frozen_trace_joined_summary.md`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/RootVarV131ExtendedBalancedFrozen`
- Contents: 108 review levels, 18 each for `cascade_relay`, `hub_spoke`, `split_key`, `strict_dual_gate`, `support_lock`, and `web_crossover`. Built from V1.28 baseline plus V1.29 four-family non-size variants, V1.30 four-family size variants, and V1.27 hub/split extras. Frozen trace: 108/108 solved, 108/108 intended family gate pass. Superseded as current Demo target by Original Seed Merged Usable Root V1 review.

## Original Seed Merged Usable Root V1 Pack

- Pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_OriginalSeedMergedUsableRootV1Pack.asset`
- Pack GUID: `1096eb72369f4630ba4b9a09bdac9c27`
- Merged all-candidates CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_merged_usability_v1_all_candidates.csv`
- Selected source CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_root_merged_usable_v1_selected.csv`
- Frozen manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/SGPRhythmLab_OriginalSeedMergedUsableRootV1Pack.csv`
- Frozen trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_merged_usable_root_v1_frozen_trace_metrics.csv`
- Frozen joined audit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/original_seed_merged_usable_root_v1_frozen_trace_joined.csv`
- Frozen levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/OriginalSeedMergedUsableRootV1Frozen`
- Contents: 16 original-seed root candidates, source-deduped from rolegraph/extractable scans and copied into short active paths from cold archive. Frozen trace: 16/16 solved, 7 S + 9 A, supportClosureBestDepth all 3-4. `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack currently points here for human filtering of truly usable original-seed roots.
## Campaign500 Hardening Sandbox V13BDR2 Boundary Inset

- Pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV13BDR2Pack.asset`
- Pack GUID: `900364ecf4764fe49beacb4d41643f3b`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V13BDR2`
- Latest report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v13_bdr2_20260625_115819.csv`
- Source report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v11_20260625_111032.csv`
- Contents: 2 accepted before/after pairs, with V11 before and V13 after. L387: direct/PBE `18->12`, opening `18->12`, NEE `33->30`. L405: direct/PBE `10->8`, opening `10->8`, NEE `30->21`. L173 was skipped in the report because it only improved PBE `9->8`.
- Demo scene `Assets/ArrowMagic/Scenes/Demo.unity` activePack currently points to this V13BDR2 pack for review.

## Campaign500 Hardening Sandbox V14CMP Boundary Compression

- Pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV14CMPPack.asset`
- Pack GUID: `9d95355a84e2c6643a7adc5765763940`
- Levels: `Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V14CMP`
- Latest report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v14_cmp_20260625_121057.csv`
- Source report: `Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening/campaign_hardening_sandbox_v13_bdr2_20260625_115819.csv`
- Contents: 1 accepted before/after pair, with V13 before and V14 after. L405: chains `167->165`, direct/PBE `8->6`, opening `8->6`, NEE `21->21`, arrow tiles unchanged. L387 was skipped because there was no safe adjacent boundary-compression candidate.
- Demo scene `Assets/ArrowMagic/Scenes/Demo.unity` activePack currently points to this V14CMP pack for review.

## SGP Pressure Hard Trial / Benchmark

- Trial pack: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardTrialPack.asset`
- Trial pack GUID: `acd1590a350614a4e86c901d33b5c5dd`
- Trial report: `Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_hard_trial_report.csv`
- Trial trace report: `.codex-run/sgp_pressure_trace_v16_scorecurve/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_hard_trial_v16_scorecurve_metrics.csv`
- Benchmark pack: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardBenchmarkPack.asset`
- Benchmark pack GUID: `c8e516eece57cc94ca87c60d18b5b0d3`
- Benchmark levels: `Assets/ArrowMagic/SOData/Levels/DirectProcedural/SGPPressureHardBenchmark/`
- Trace report: `.codex-run/sgp_pressure_benchmark_trace/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_hard_benchmark_metrics.csv`
- Contents: 3 copied validated hard-library benchmark levels; trace result 3/3 solved A-tier, openers `3-5`, avg choices `2.57-2.98`, max choices `5-6`, outer exits `0/1`. Demo currently points to the benchmark pack for review.
- Current trial contents: v16 direct SGP small long-chain pressure trial, 4/4 solved B-tier; chains `57-63`, openers `3-5`, avg choices `5.26-6.64`, max choices `8-12`. Demo currently points to the trial pack after the latest run.

## PSG / Pressure-SGP Normal Production V1 - 2026-06-26

- Formal lane name: `PSG` / `Pressure-SGP`; core commit `aa1564bd Add PSG pressure hard production lane`.
- Production wrapper: `Tools/Production/Invoke-SGPPressureHardProductionV1.ps1`
- Unity generation method: `NoMaskProceduralGenerator.BuildSgpPressureHardTrialPack`
- Current review pack: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardTrialPack.asset`
- Current review pack GUID: `acd1590a350614a4e86c901d33b5c5dd`
- Current STS production keep pack: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardProductionKeepPack.asset`
- Current STS production keep CSV: `Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_hard_production_keep.csv`
- Current levels: `Assets/ArrowMagic/SOData/Levels/DirectProcedural/SGPPressureHardTrial/`
- Source report: `Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_hard_trial_report.csv`
- Official trace script: `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1`
- Four-pool production batch wrapper: `Tools/Production/Invoke-SGPPressureProductionBatchV1.ps1`
- Four-pool fast STS trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_batch4_faststs_20260627_metrics.csv`
- Four-pool final joined audit: `.codex-run/sgp_pressure_batch4_faststs_final2_pack_20260627_trace_joined.csv`
- Speedcheck Unity log: `.codex-run/sgp_pressure_hard_production_v1_speedcheck_unity.log`
- Speedcheck trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_hard_production_v1_speedcheck_trace_metrics.csv`
- Current contents: 4 high-coverage normal-production review candidates: `lock_buckle` coverage `0.991`, `section_unlock` `0.994`, `dense_weave` `0.978`, `core_burst` `0.990`; all portable solved.
- Verification result: official trace 4/4 solved. Practical normal-production filter `coverage>=0.97 + solved + processTier A/B` keeps 3/4 (`lock_buckle`, `dense_weave`, `core_burst`). Stricter high-support filter `supportDepth>=3 + A/B + maxChoices<=8` keeps only `dense_weave`.
- 2026-06-27 four-pool STS production result: Trial/Review6/Interference6/InterferenceV2Six 合并 22 关，fast STS trace 22/22 成功；修正 maxChoices 硬闸后 `TraceOrderKeep=15`（trial 2、review6 4、interference6 5、interference_v2_six 4），pack level refs=15，production keep 最大 `maxChoices=10`。
- 2026-06-28 style/flow tagging update: final joined audit and canonical production keep now include `styleFamily`、`generatorVariant`、`generatorGrammar`、`chainLanguage`、`chainTags`、`flowLanguage`、`flowTags`、`riskTags`、`styleRiskBand`。Current keep mix is core_burst 6 / lock_buckle 6 / dense_weave 3; flow mix is staged_unlock 7 / region_alternating_flow 3 / flow_spread 3 / local_collapse 2.
- 2026-06-28 diversity candidate: `.codex-run/psg_diversity_strict12_keep.csv` is a 12-row strict cap review output, with style mix core_burst 5 / lock_buckle 4 / dense_weave 3 and risk mix clean 4 / watch 5 / high_risk 3. It is not yet synced to canonical keep or Demo pack.
- Filtered clean-hit pack retained for single-level demo/reference: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardProductionV1Pack.asset`, GUID `afdb809ddc1a4502910d678912899a75`, containing `sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave.asset`.
- Demo scene `Assets/ArrowMagic/Scenes/Demo.unity` currently points to the 15-level `SGPPressureHardProductionKeepPack` after the latest pack build; `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` may still point to its lab review pack.

## SGP Sandwich Tail Safe 0859 Review Pack - 2026-06-26

- Lab Demo pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SandwichTailSafe0859ReviewPack.asset`
- Pack GUID: `73eb729f7ca4413cb0a1a3b1b8d20c7d`
- Level: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/SandwichTailSafe0859Review/tail0857B_alt_ray1_single_c42.asset`
- Level GUID: `415ba263e0054f3fe88215e819ea4b6e`
- Source trace: `.worktrees/sgp-sandwich-refill/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/tail0857B_alt_ray1_single_unique_trace_metrics.csv`
- Contents: single sandwich/tail-safe boundary candidate, coverage `0.8596154`, `solved=True`, `processTier=B`, `supportClosureBestDepth=4`, `hardStructureV3Class=LocalEasy`. Manual review rejected it as worse than the normal production samples; keep this pack only as a negative boundary/reference for why supportDepth alone does not prove high difficulty.

## SGP Sandwich Owner-Hit Grammar Final 0900 - 2026-06-26

- Final selected CSV: `.worktrees/sgp-sandwich-refill/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ownerhit0900_sandwich_final_selected.csv`
- Final level asset: `.worktrees/sgp-sandwich-refill/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/OwnerHitGrammarFrom0898B/tail_single/ownerhit0898_tail_single_c63.asset`
- Final candidate CSV: `.worktrees/sgp-sandwich-refill/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ownerhit0898_tail_single_unique_candidates.csv`
- Final official trace metrics: `.worktrees/sgp-sandwich-refill/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/ownerhit0898_tail_single_unique_trace_metrics.csv`
- Verification result: coverage `0.9000000`, `solved=True`, `processTier=B`, `supportClosureBestDepth=4`, `avgChoices=4.39`, `choiceP80=7`, `maxChoices=10`, `outerExitHeadCount=10`, but `hardStructureV3Class=LocalEasy` / `hardStructureV3Score=0.071`; keep as high-coverage support-preserved boundary, not as a proven high-difficulty candidate.
- Route summary: strict sandwich/refill parent -> no-new-head tail-safe fill to ~0.86 -> support-safe owner-hit grammar fill to ~0.898 -> tail single-cell finish to exactly `0.9`.
- Review pack mounted in `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity`: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_SandwichOwnerHit0900ReviewPack.asset`, GUID `3c8ae3e32cd54fa6b83ff2d7f8f09000`.

## Geometry Supply Owner-Hit Probe root154 + lock_buckle - 2026-06-26

- Summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_probe_summary.md`
- Summary CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_probe_summary.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_probe_selected.csv`
- Candidate/trace batches:
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_v1_candidates.csv`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_v1_trace_metrics.csv`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_r2_candidates.csv`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_r2_trace_metrics.csv`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_r3_candidates.csv`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_r3_trace_metrics.csv`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_r4_len10_candidates.csv`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_r4_len10_trace_metrics.csv`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_r5_len10_candidates.csv`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_root154_lockbuckle_r5_len10_trace_metrics.csv`
- Candidate levels root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeometrySupplyOwnerHitV1/`
- Base root: `orig_seed_usable_v1_01_rolegraph_next5_arrowz_level_154`, coverage `0.291498`, official trace `S/TrueHardCandidate/supportDepth4`.
- Supply level: `Assets/ArrowMagic/SOData/Levels/DirectProcedural/SGPPressureHardTrial/sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle.asset`; used center-crop `20x28 -> 19x26` and same-chain edge constraint.
- Current result: five traced rounds; best strict coverage reached `0.3623482` with `solved=True`, `processTier=S`, `hardStructureV3Class=TrueHardCandidate`, `supportClosureBestDepth=4`. Not mounted in Demo; this is a low-coverage high-difficulty growth proof.

## Geometry Supply Owner-Hit Multi-Root Probe - 2026-06-26

- Summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_multi_root_probe_summary.md`
- Summary CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_multi_root_probe_summary.csv`
- Selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_ownerhit_multi_root_probe_selected.csv`
- Candidate levels root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeometrySupplyOwnerHitV1/`
- Tested roots/supplies:
  - `root05 + core_burst`: reached `coverage=0.6419355`, `solved=True`, `processTier=S`, `hardStructureV3Class=MediumStructure`, `supportClosureBestDepth=4`; high-coverage nonLocal control, not hard enough.
  - `root10 + dense_weave + bundle3`: current strongest hard lane; strict TrueHard/support4 candidates reached `0.4072464`, `0.4434783`, `0.4753623`, and `0.5000000`. Highest row `0.5028986` downgraded to `HardPotential/support4`.
  - `root98 + dense_weave`: `0.3002451`, 24/24 `TrueHardCandidate/support4`, but low throughput.
  - `root76 + dense_weave`: strict `TrueHard/support4` candidate at `0.3345588`, lower pass density than root10.
- Demo status: not mounted. Use this as high-difficulty research evidence and scheduler input only.

## Geometry Supply Owner-Hit Scheduler root10 + dense_weave - 2026-06-26

- Scheduler: `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Invoke-GeometrySupplyOwnerHitSchedulerV1.ps1`
- Candidate levels root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeometrySupplyOwnerHitV1/`
- Supply level: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/DirectProcedural/SGPPressureHardTrial/sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave.asset`
- Key summaries:
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_sched_root10_denseweave_from0471_hardmargin_v1_summary.md`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_sched_root10_denseweave_from0495_ownerspread_tail_v1_summary.md`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_sched_root10_denseweave_from0515_ownerspread_tail_v1_summary.md`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_sched_root10_denseweave_from0534_ownerspread_tail_v1_summary.md`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_sched_root10_denseweave_from0549_ownerspread_tail_v1_summary.md`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_sched_root10_denseweave_from0562_ownerspread_tail_v1_summary.md`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_sched_root10_denseweave_from0576_ownerspread_tail_v1_summary.md`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_sched_root10_denseweave_from0576_all48_v1_metrics.csv`
- Current best strict selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_sched_root10_denseweave_from0562_ownerspread_tail_v1_selected.csv`
- Current best strict level: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeometrySupplyOwnerHitV1/geosupply_sched_root10_dens_be1afe84/r1/geosupply_sched_root10_dens_be1afe84_r1_c015.asset`
- Current best strict metrics: `coverage=0.5768116`, `solved=True`, `processTier=A`, `hardStructureV3Class=TrueHardCandidate`, `hardStructureV3Score=0.686`, `supportClosureBestDepth=4`, `openers=2`, `avgChoices=2.84`, `maxChoices=6`, `outerExitHeadCount=0`.
- Efficiency finding: Top-only tracing can miss the only strict row after `0.50` (`from0495` top16 all HardPotential, top48 found rank45 TrueHard). `TraceSelectionMode OwnerSpread` with head/tail hitOwner sampling re-finds the same class with top8/24 and continued the strict chain to `0.5768116`. Not mounted in Demo.
- Boundary check: from `0.5768116`, OwnerSpread top24 found no strict; full top48 trace also found no `TrueHardCandidate/supportDepth4`. The best TrueHard row had supportDepth3, while supportDepth4 rows were HardPotential/MediumStructure. Current strict chain is not considered runnable past `0.5768` with the same grammar/settings.

## Geometry Supply Owner-Hit Adaptive root10 + section_unlock - 2026-06-26

- Base parent: `geosupply_sched_root10_dens_be1afe84_r1_c015`, coverage `0.5768116`, `A/TrueHardCandidate/supportDepth4`.
- Supply level: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/DirectProcedural/SGPPressureHardTrial/sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock.asset`
- Key summaries:
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_sched_root10_from0576_section_bundle2_v1_summary.md`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_sched_root10_from0584_section_bundle2_v1_summary.md`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_sched_root10_from0589_section_bundle2_v1_summary.md`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_sched_root10_from0607_section_bundle1_v1_summary.md`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_sched_root10_from0610_section_bundle1_v1_summary.md`
  - `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_sched_root10_from0613_section_bundle1_v1_summary.md`
- Adaptive result: `section_unlock + bundle2` pushed `0.5768116 -> 0.5840580 -> 0.5898551 -> 0.6072464`; after bundle2 drifted to HardPotential, `section_unlock + bundle1` micro-commit pushed `0.6072464 -> 0.6101449 -> 0.6130435 -> 0.6159420`.
- Current best strict selected CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/geosupply_sched_root10_from0613_section_bundle1_v1_selected.csv`
- Current best strict level: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeometrySupplyOwnerHitV1/geosupply_sched_root10_from_40eb0da7/r1/geosupply_sched_root10_from_40eb0da7_r1_c038.asset`
- Current best strict metrics: `coverage=0.6159420`, `solved=True`, `processTier=A`, `hardStructureV3Class=TrueHardCandidate`, `hardStructureV3Score=0.725`, `supportClosureBestDepth=4`, `openers=2`, `avgChoices=3.00`, `maxChoices=8`, `outerExitHeadCount=0`.
- Flow finding: after `0.5768`, adding PSG supply difficulty by switching supply shape matters more than brute trace. Dense supply remains support4 but drifts to HardPotential; section_unlock restores TrueHard. After `0.607`, bundle2 is too coarse and bundle1 micro-commit is required. Not mounted in Demo.

## Generated-Root Whole-Board Planner V0 - 2026-06-27

- Candidate assets root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV0/`.
- Solved pair asset folder: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV0/smoke_solved_pair/`.
- Solved pair candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/generated_root_wbp_v0_solved_pair_candidates.csv`.
- Whole-board role plan: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/generated_root_wbp_v0_solved_pair_cell_plan.csv`.
- Chain plan and planned relation audit: `generated_root_wbp_v0_solved_pair_chain_plan.csv` and `generated_root_wbp_v0_solved_pair_planned_relations.csv` in the same report folder.
- Official trace: `generated_root_wbp_v0_solved_pair_trace_metrics.csv` and `generated_root_wbp_v0_solved_pair_trace_steps.csv`; result 6/6 solved, process A, 2 TrueHardCandidate + 4 HardPotential, supportDepth4.
- Relation audit: `generated_root_wbp_v0_solved_pair_relation_audit_summary.md` plus `_levels.csv`, `_parents.csv`, `_edges.csv`; c005/c006 pass `passesTrueHardRelationGate`.
- Boundary/negative outputs: `generated_root_wbp_v0_smoke1_*` structural 4-5 chain smoke exists as unsolved diagnostic only; do not treat it as accepted.

## Generated-Root Whole-Board Planner V1 - 2026-06-27

- Candidate assets root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV1/`.
- Guarded pair asset folder: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV1/pair_guarded/`.
- Guarded pair candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/generated_root_wbp_v1_pair_guarded_candidates.csv`.
- Whole-board role plan / chain plan / planned relation: `generated_root_wbp_v1_pair_guarded_cell_plan.csv`, `generated_root_wbp_v1_pair_guarded_chain_plan.csv`, `generated_root_wbp_v1_pair_guarded_planned_relations.csv`.
- Compatibility and single-option audit: `generated_root_wbp_v1_pair_guarded_compatibility_report.csv`; use it to inspect release-impact failures and B2 single-option deadlocks.
- Official trace: `generated_root_wbp_v1_pair_guarded_trace_metrics.csv` and `generated_root_wbp_v1_pair_guarded_trace_steps.csv`; result 8/8 solved, process A, 8/8 HardPotential, supportDepth4, coverage from candidate CSV `0.6275362-0.6289855`.
- Relation audit: `generated_root_wbp_v1_pair_guarded_relation_audit_summary.md` plus `_levels.csv`, `_parents.csv`, `_edges.csv`; added chains appear in official edges such as `20 -> 58`, `7 -> 59`, `59 -> 22`, `33 -> 60`.
- Boundary outputs: `generated_root_wbp_v1_triple_guarded_*` documents guarded 3-chain failure with 0 candidates. Treat this as the current next-step boundary, not as an accepted pack.

## Generated-Root Whole-Board Planner V2 - 2026-06-27

- Candidate assets root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV2/`.
- Main positive asset folder: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV2/b2safe_smoke5_fivechain_widepool/`.
- Main positive candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/generated_root_wbp_v2_b2safe_smoke5_fivechain_widepool_candidates.csv`; 3 candidates, rootPreserved=True, coverage `0.6463768`, 5 added short semantic chains, 4 contracts, 3 B2 safe chains.
- Whole-board role plan / selected chain plan / planned relation: `generated_root_wbp_v2_b2safe_smoke5_fivechain_widepool_cell_plan.csv`, `generated_root_wbp_v2_b2safe_smoke5_fivechain_widepool_chain_plan.csv`, `generated_root_wbp_v2_b2safe_smoke5_fivechain_widepool_planned_relations.csv`.
- Compatibility and B2 release audit: `generated_root_wbp_v2_b2safe_smoke5_fivechain_widepool_compatibility_report.csv` and `generated_root_wbp_v2_b2safe_smoke5_fivechain_widepool_b2_release_profile.csv`; use these to inspect `releaseProfile/releaseStackOwners/plannedSolved/keyBlocked`.
- Official trace: `generated_root_wbp_v2_b2safe_smoke5_fivechain_widepool_trace_metrics.csv` and `_trace_steps.csv`; result 3/3 solved, process A, supportDepth4, avg/max choices about `3.08-3.22/8-9`, outerExitHeadCount 0, but hardStructureV3Class remains MediumStructure.
- Relation audit: `generated_root_wbp_v2_b2safe_smoke5_fivechain_widepool_relation_audit_summary.md` plus `_levels.csv`, `_parents.csv`, `_edges.csv`; added chains appear in official edges such as `7 -> 60/61 -> 22`, `0 -> 59/60/62/63`, `59/60 -> 48/2`, `63 -> 30`.
- Boundary outputs: `generated_root_wbp_v2_b2safe_smoke6_sixchain_widepool_*` documents current 6-chain failure with 0 candidates. Treat this as evidence for B2 contract strength / spatial lane allocation next, not as a solved/high-coverage pack.

## Generated-Root Whole-Board Planner V3 - 2026-06-27

- Candidate assets root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV3/`.
- Instrumentation baseline outputs: `generated_root_wbp_v3_strength_smoke2_instrumented_*`; use these to compare against V2 5-chain and inspect B2 strength/lane fields without changing default selection behavior.
- Topology smoke asset folder: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV3/topology_smoke3_fourchain_diverse/`.
- Topology candidate CSV: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/generated_root_wbp_v3_topology_smoke3_fourchain_diverse_candidates.csv`; 4 candidates, rootPreserved=True, coverage `0.6449275-0.6463768`, 4 semantic contracts, 2 B2 safe/strong, `b2ReleaseOwnerCount=2`.
- Selected chain plan: `generated_root_wbp_v3_topology_smoke3_fourchain_diverse_chain_plan.csv`; selected B2 chains include owner0 `B2_DELAYS_B3` plus `B2T95004` with first-hit releaseOwner 14 and semanticReleaseOwner 30.
- B2 release profile: `generated_root_wbp_v3_topology_smoke3_fourchain_diverse_b2_release_profile.csv`; inspect `semanticReleaseOwner`, `semanticReleaseStep`, `releaseStackOwners`, `b2Strength`, and `laneKeys` to distinguish topology chains from old owner0-only B2S chains.
- Official trace: `generated_root_wbp_v3_topology_smoke3_fourchain_diverse_trace_metrics.csv`; result 4/4 solved, process A, supportDepth4, outerExitHeadCount 0, avg/max choices about `3.05-3.21/8-9`, but hardStructureV3Class remains MediumStructure.
- Relation audit: `generated_root_wbp_v3_topology_smoke3_fourchain_diverse_relation_audit_summary.md` plus `_levels.csv`, `_parents.csv`, `_edges.csv`; 244 edges / 164 parent rows, with added topology chain edges such as `30 -> 61` and `61 -> 53`.
- Difficulty attribution: `generated_root_wbp_v3_topology_smoke3_fourchain_diverse_difficulty_attribution_summary.md` plus `_chains.csv` and `_levels.csv`; confirms V3 B2 owner diversity (`0:1;30:1`) but also shows `supportCarrierCount=0` and `local_penalty_dense;no_added_support_carrier`.
- V2 comparison attribution: `generated_root_wbp_v2_b2safe_smoke5_fivechain_widepool_difficulty_attribution_summary.md`; shows V2 B2 owners collapsed to `0:3`, dominant share 1.0, with the same no-added-support-carrier boundary.
- Boundary: V3 proves release-topology allocation can break the owner0 B2 collapse, but it is still low coverage and MediumStructure. Treat it as the next primitive proof; do not treat it as final full-board success.

## Mask Line Inventory Baseline - 2026-06-26

- Current HoleMask assets: `Assets/ArrowMagic/SOData/Levels/Production/HoleMask/Candidates` (68), `Assets/ArrowMagic/SOData/Levels/Production/HoleMask/Early` (2), `Assets/ArrowMagic/SOData/Levels/Production/HoleMask/Early30To40` (1).
- Historical manifest: `Assets/ArrowMagic/SOData/Reports/LevelImportV1/hole_mask_early_front_manifest.csv` has 70 rows, playableFill `0.602-0.779`, boardFill `0.451-0.721`, chains `22-97`, and references missing pack `Assets/ArrowMagic/SOData/Packs/Production/HoleMask/HoleMask_FinalScreening_EarlyFront.asset`.
- Runnable/reference packs currently present: `Assets/ArrowMagic/SOData/Packs/HoleV13Top5DemoPack.asset`, `Assets/ArrowMagic/SOData/Packs/Production/HoleProcedural/HoleProceduralCandidatePack.asset`, `Assets/ArrowMagic/SOData/Packs/Production/HoleProcedural/HoleProceduralPreviewTop50Pack.asset`, `Assets/ArrowMagic/SOData/Packs/ShapeExperiment/ShapeIconMaskOnlyBatch11CandidatePack.asset`, `Assets/ArrowMagic/SOData/Packs/ShapeExperiment/ShapeIconMaskOnlyBatch12CandidatePack.asset`.
- Partial SeedMask production output: `Assets/ArrowMagic/Reports/Production/HoleLongOuterStrong/HoleLongOuterStrong_Production_Report.txt` records one accepted `22x34_long` candidate in `Assets/ArrowMagic/SOData/Levels/Production/HoleLongOuterStrong/Candidates`; no synced pack was found under `Assets/ArrowMagic/SOData/Packs/Production/HoleLongOuterStrong/`.
- Baseline pack should be rebuilt non-destructively from existing HoleMask assets, then checked with `CampaignSingleLevelValidator` and official trace before any Demo attachment or larger SeedMask generation.

## Mask PSG Baseline Assets - 2026-06-26

- Direct constrained PSG baseline pack: `Assets/ArrowMagic/SOData/Packs/Production/MaskPressure/MaskPressureBaselinePack.asset`; current contents 0 levels. Report: `Assets/ArrowMagic/SOData/Reports/MaskPressure/mask_pressure_baseline_report.csv`; result 3/3 failed Greedy despite high fill, so this is a negative baseline/reference only.
- PSG seed patch baseline pack: `Assets/ArrowMagic/SOData/Packs/Production/MaskPressure/MaskPressureSeedPatchBaselinePack.asset`; current contents 3 high-fill levels, generated in `Assets/ArrowMagic/SOData/Levels/Production/MaskPressureSeedPatchBaseline/Candidates/`. Baseline generation report: `Assets/ArrowMagic/SOData/Reports/MaskPressure/mask_pressure_seed_patch_baseline_report.txt`; current gates are `maskFill>=0.95` and `maskBoundaryFill>=0.98`.
- Current accepted high-fill levels: `psg_mask_patch_22x34_long_02_sgp_pressure_hard_trial_03_sgp_pressure_hard_rect_dense_weave.asset` (`456/480=0.950`), `psg_mask_patch_24x36_long_04_sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle.asset` (`559/588=0.951`), and `psg_mask_patch_24x40_long_01_sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock 1.asset` (`643/676=0.951`); all have `maskBoundaryFill=1.000`.
- Validation outputs: `Assets/ArrowMagic/SOData/Reports/MaskPressure/Validation/mask_pressure_seed_patch_validation_summary.csv` (0 Green / 3 Yellow / 0 Red), `mask_pressure_seed_patch_validation_flags.csv`, `mask_pressure_seed_patch_pressure_gate.csv` (0/3 internal pressure gate).
- Official trace outputs: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/mask_pressure_seed_patch_highfill_trace_metrics.csv` and `_summary.md`; result 3/3 solved but tiers Drop/Drop/Drop and all LocalEasy, so the pack is a high-fill visual/solvable baseline, not a formal PSG-standard production pack.

## Campaign500 PSG Regeneration Template - 2026-06-26

- Template folder: `Exports/Campaign500_PSG_Template_20260626_095625/`
- Zip: `Exports/Campaign500_PSG_Template_20260626_095625.zip`
- Main CSV: `Exports/Campaign500_PSG_Template_20260626_095625/campaign500_psg_regeneration_template.csv`
- Split CSVs: `campaign500_psg_template_normal.csv`, `campaign500_psg_template_shape.csv`, `campaign500_psg_template_hole.csv`
- Pacing summary: `campaign500_psg_template_section10_summary.csv`
- Source snapshots: `source_campaign500_final_v11_manifest.csv`, `source_campaign500_final_v11_shape_usage.csv`
- Purpose: 500-slot template for PSG regeneration, preserving order/category/difficulty/experience role/target size/target chains/shape and hole presentation fields without requiring old level assets.

## Campaign500 PSG Normal Pure Pilot - 2026-06-26

- Worktree: `.worktrees/campaign500-psg-normal`, branch `codex/campaign500-psg-normal`.
- Full pilot pack: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PSGNormalPureProfilePilotPack.asset` (19 generated candidates from 10 normal slots x strict/relaxed modes; one relaxed maze failed generation).
- Keep review pack: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PSGNormalPureProfilePilotKeepPack.asset` (12 trace-filtered ordinary PSG normal candidates; Demo in this worktree points here).
- Source report: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/Campaign500/PSGNormal/campaign500_psg_normal_pure_profile_pilot_report.csv`.
- Trace metrics: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_psg_pure_profile_pilot_metrics.csv`; joined filter table: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/Campaign500/PSGNormal/campaign500_psg_normal_pure_profile_pilot_trace_joined.csv`; keep CSV: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/Campaign500/PSGNormal/campaign500_psg_normal_pure_profile_pilot_keep.csv`.
- Result summary: 19/19 traced solved, process tiers 1 A / 16 B / 2 Drop; conservative keep rule `coverage>=0.97 && processTier in A/B && maxChoices<=10` kept 12. All are `LocalEasy`, expected for ordinary PSG normal, not high-difficulty root line.

## Campaign500 PSG Normal Language Pilot - 2026-06-26

- Worktree: `.worktrees/campaign500-psg-normal`, branch `codex/campaign500-psg-normal`.
- Full language pack: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PSGNormalLanguagePilotPack.asset` (10/10 generated from language-strict PSG normal profiles).
- Keep review pack: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PSGNormalLanguagePilotKeepPack.asset` (7 trace-filtered candidates, excludes Drop and maxChoices>10 rows).
- Candidate levels: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Levels/Campaign500PSGNormal/LanguagePilot/`.
- Source report: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/Campaign500/PSGNormal/campaign500_psg_normal_language_pilot_report.csv`.
- Trace metrics: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_psg_language_pilot_metrics.csv`; joined filter table: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/Campaign500/PSGNormal/campaign500_psg_normal_language_pilot_trace_joined.csv`; keep CSV: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/Campaign500/PSGNormal/campaign500_psg_normal_language_pilot_keep.csv`.
- Result summary: 10/10 solved, tiers 1 A / 8 B / 1 Drop; conservative keep kept 7/10. Use this pack for manual chain-language review before expanding PSG normal full production.

## Campaign500 PSG Normal Calibration50 - 2026-06-26

- Worktree: `.worktrees/campaign500-psg-normal`, branch `codex/campaign500-psg-normal`.
- Full calibration pack: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PSGNormalCalibration50Pack.asset` (131 generated candidates from 50 section-calibration slots / 160 attempted rows).
- Keep review pack: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PSGNormalCalibration50KeepPack.asset` (26 trace-filtered ordinary PSG normal candidates, max 2 per template order).
- Candidate levels: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Levels/Campaign500PSGNormal/Calibration50/`.
- Source report: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/Campaign500/PSGNormal/campaign500_psg_normal_calibration50_report.csv`.
- Trace metrics: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_psg_calibration50_metrics.csv`; joined filter table: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/Campaign500/PSGNormal/campaign500_psg_normal_calibration50_trace_joined.csv`; keep CSV: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/Campaign500/PSGNormal/campaign500_psg_normal_calibration50_keep.csv`.
- Result summary: 131/131 traced solved, tiers 2 A / 64 B / 65 Drop, all LocalEasy. Conservative keep rule `coverage>=0.97 && processTier in A/B && maxChoices<=10` found 30 pass rows; keep pack keeps 26 across 19 orders.

## Campaign500 PSG Normal Production20 - 2026-06-27

- Worktree: `.worktrees/campaign500-psg-normal`, branch `codex/campaign500-psg-normal`.
- Candidate pack: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PSGNormalProduction20Pack.asset` (48 generated candidates from the first 20 non-tutorial normal template slots; 61 attempts total).
- Keep review pack: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PSGNormalProduction20KeepPack.asset` (10 selected candidates; Demo in this worktree points here).
- Candidate levels: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Levels/Campaign500PSGNormal/Production20/`.
- Source report: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/Campaign500/PSGNormal/campaign500_psg_normal_production20_report.csv`.
- Trace metrics: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_psg_normal_production20_metrics.csv`; process keep: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_psg_normal_production20_process_keep.csv`; summary: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_psg_normal_production20_summary.md`.
- Joined filter table: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/Campaign500/PSGNormal/campaign500_psg_normal_production20_trace_joined.csv`; keep CSV: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/Campaign500/PSGNormal/campaign500_psg_normal_production20_keep.csv`.
- Result summary: 48/48 traced solved, tiers 7 A / 33 B / 8 Drop; hard keep 33; visual keep 16 after stripe/directional/local/near-outer filter; final keep 10 across normal/lock/section/sweep/maze/dense, shell skipped. `edgeInwardSweepSide/startSideHint` records the dominant clear-from side.

## Campaign500 PSG Normal ProductionNext10 - 2026-06-27

- Worktree: `.worktrees/campaign500-psg-normal`, branch `codex/campaign500-psg-normal`.
- Candidate pack: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PSGNormalProductionNext10Pack.asset` (39 generated candidates from template orders `31,33,34,35,36,39,40,41,42,44`; 44 attempts total).
- Keep review pack: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PSGNormalProductionNext10KeepPack.asset` (7 selected candidates; Demo in this worktree points here).
- Candidate levels: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Levels/Campaign500PSGNormal/ProductionNext10/`.
- Source report: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/Campaign500/PSGNormal/campaign500_psg_normal_production_next10_report.csv`.
- Trace metrics: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_psg_normal_production_next10_metrics.csv`; summary: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_psg_normal_production_next10_summary.md`.
- Joined filter table: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/Campaign500/PSGNormal/campaign500_psg_normal_production_next10_trace_joined.csv`; keep CSV: `.worktrees/campaign500-psg-normal/Assets/ArrowMagic/SOData/Reports/Campaign500/PSGNormal/campaign500_psg_normal_production_next10_keep.csv`.
- Result summary: 39/39 traced solved, tiers 29 B / 10 Drop; hard keep 14, visual keep 7. Final keep covers dense/lock/section and includes a visible-width `section layout_soft 23x29`; sweep order 31 skipped due high stripe risk despite one good choice curve.

## PSG Project Seed Style Profile V3 - 2026-06-28

- Seed source root: `Assets/ArrowMagic/SOData/Levels/Seeds/`; current fast profile parsed 951/951 assets with 0 missing.
- Profile output: `.worktrees/psg-long-lock-role-grammar/Assets/ArrowMagic/SOData/Reports/DirectProcedural/project_seed_style_v3_initial951_20260628_profile.csv`.
- Cluster output: `.worktrees/psg-long-lock-role-grammar/Assets/ArrowMagic/SOData/Reports/DirectProcedural/project_seed_style_v3_initial951_20260628_clusters.csv`; current cluster mix is `seed_long_maze` 574, `seed_sparse_tutorial` 155, `seed_flow_spread` 57, `seed_medium_long_patchwork` 45, `seed_long_weave` 31, `seed_dense_weave` 27, `seed_long_lock` 25, `seed_fragmented_lock_like` 16, plus small core/mixed/outer clusters.
- PSG match output: `.worktrees/psg-long-lock-role-grammar/Assets/ArrowMagic/SOData/Reports/DirectProcedural/project_seed_style_v3_initial951_20260628_psg_match.csv`; current PSG production keep 15 rows all map to `seed_fragmented_lock_like` / `medium_long_patchwork_carrier`, with low spine concentration versus original seed `long_maze/long_lock`.
- Summary: `.worktrees/psg-long-lock-role-grammar/Assets/ArrowMagic/SOData/Reports/DirectProcedural/project_seed_style_v3_initial951_20260628_summary.md`; use this before designing PSG StyleProfile soft generation targets.

## PSG Style Lane Export V1 - 2026-06-28

- Stable script: `Tools/Production/Export-PSGStyleLaneKeepsV1.ps1`; it exports lane CSVs from joined PSG source/trace output and does not change canonical keep or Demo pack.
- Current smoke input: `.codex-run/sgp_pressure_batch4_faststs_final2_pack_20260627_trace_joined.csv`.
- Current output index: `.codex-run/psg_style_lanes_v1_current_lane_index.csv`; summary: `.codex-run/psg_style_lanes_v1_current_lane_summary.md`.
- Lane outputs: `.codex-run/psg_style_lanes_v1_current_lane_patchwork_lock_keep.csv`, `_core_burst_keep.csv`, `_dense_weave_keep.csv`, `_flow_spread_keep.csv`, `_staged_unlock_keep.csv`.
- Current TraceOrderKeep counts: patchwork_lock 6, core_burst 6, dense_weave 3, flow_spread 6, staged_unlock 7. Lane CSVs are independent by default; use `-UniqueAcrossLanes` for a duplicate-free mixed pack candidate.

## Campaign500 PSG Normal Unified QTrace Prod200 - 2026-06-28

- Candidate pool: `_CodexRun/campaign500_psg_normal_prod200_unified_qtrace_candidate_pool.csv`; four official prod200 reports only, smoke excluded.
- Trace input: `_CodexRun/campaign500_psg_normal_prod200_unified_qtrace_trace_input.csv`; 200 rows after section quota, absolute asset paths point back to D worktrees.
- Trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_psg_normal_prod200_unified_qtrace_metrics.csv`; 200/200 solved, A=5/B=105/Drop=90.
- Joined audit: `_CodexRun/campaign500_psg_normal_prod200_unified_qtrace_trace_joined.csv`; source summary `_trace_joined_summary.md`, best-per-slot `_best_by_slot_raw.csv`, raw production keep `_production_keep_raw.csv`.
- Strict selected-by-slot manifest: `_CodexRun/campaign500_psg_normal_prod200_unified_qtrace_selected_by_slot.csv`; 10 non-high-risk TraceOrderKeep rows only, orders `16,41,44,66,68,100,102,106,116,254`.
- Style lanes: `_CodexRun/campaign500_psg_normal_prod200_unified_qtrace_style_lanes/`; eligible rows=10, dense_weave=2, flow_spread=3, staged_unlock=6, patchwork_lock/core_burst=0.
- Important caveat: 190/200 joined rows are `styleRiskBand=high_risk`, mostly same-axis/same-dir/high-choice/collapse risks; this is a diagnostic production run, not a complete 200-slot final pack.
- High-risk manual review pack in D c01 worktree: `D:\Unityproject\ArrowLevel-Hand-campaign500-psg-normal\Assets\ArrowMagic\SOData\Packs\Campaign500\Campaign500PSGNormal_UnifiedQTraceHighRiskReview12Pack.asset`; source CSV `D:\Unityproject\ArrowLevel-Hand-campaign500-psg-normal\Assets\ArrowMagic\SOData\Reports\Campaign500\PSGNormal\campaign500_psg_normal_unified_qtrace_highrisk_review12.csv`. Contains 12 high-risk rows for threshold/feel review and is mounted to that worktree's Demo.
- Strict keep10 manual review pack in D c01 worktree: `D:\Unityproject\ArrowLevel-Hand-campaign500-psg-normal\Assets\ArrowMagic\SOData\Packs\Campaign500\Campaign500PSGNormal_UnifiedQTraceKeep10Pack.asset`; source CSV `D:\Unityproject\ArrowLevel-Hand-campaign500-psg-normal\Assets\ArrowMagic\SOData\Reports\Campaign500\PSGNormal\campaign500_psg_normal_unified_qtrace_keep10.csv`. Contains the 10 non-high-risk selected rows and is currently mounted to that worktree's Demo.

## Campaign500 PSG Normal RhythmScore20 Cap120B - 2026-06-28

- Worktree: `D:\Unityproject\ArrowLevel-Hand-campaign500-psg-normal`.
- Candidate label: `rhythmscore20_cap120b_o011_s000_n020`; order 11 起 20 个 normal slots，source rhythm score-only，`ProfileMaxAttempts=120`，短 trace 前缀 `rg20c120b_trace`。
- Source report: `D:\Unityproject\ArrowLevel-Hand-campaign500-psg-normal\Assets\ArrowMagic\SOData\Reports\Campaign500\PSGNormal\campaign500_psg_normal_rhythmscore20_cap120b_o011_s000_n020_report.csv`; 76 attempt rows / 49 built / 16 orders built.
- Trace metrics: `F:\Unityproject\ArrowLevel-Hand\.worktrees\sgp-rhythm-lab\Assets\ArrowMagic\SOData\Reports\SGPRhythmLab\rg20c120b_trace_metrics.csv`; 49/49 traced after `subst P:` short root and 4-way parallel trace.
- Joined audit: `D:\Unityproject\ArrowLevel-Hand-campaign500-psg-normal\Assets\ArrowMagic\SOData\Reports\Campaign500\PSGNormal\campaign500_psg_normal_rhythmscore20_cap120b_o011_s000_n020_trace_joined.csv`; 49 rows, styleRiskBand `watch=4/high_risk=45`.
- Keep CSV/pack: `D:\Unityproject\ArrowLevel-Hand-campaign500-psg-normal\Assets\ArrowMagic\SOData\Reports\Campaign500\PSGNormal\campaign500_psg_normal_rhythmscore20_cap120b_o011_s000_n020_keep.csv`; `D:\Unityproject\ArrowLevel-Hand-campaign500-psg-normal\Assets\ArrowMagic\SOData\Packs\Campaign500\Campaign500PSGNormal_rhythmscore20_cap120b_o011_s000_n020_KeepPack.asset`. Contains 3 watch rows, orders `16,18,22`, mounted in D worktree Demo.
- Caveat: order 18 is the cleanest; order 16/22 still have same-axis/same-dir watch. This is a validation pack, not a green light to resume 200-slot production.

## PSG Long Seed Mutation Source Pool V1 - 2026-06-28

- Worktree: `.worktrees/psg-long-seed-mutation`, branch `codex/psg-long-seed-mutation`.
- Source selector: `.worktrees/psg-long-seed-mutation/Tools/Production/Export-PSGLongSeedMutationSourcePoolV1.ps1`.
- Input profile: `.worktrees/psg-long-seed-mutation/Assets/ArrowMagic/SOData/Reports/DirectProcedural/project_seed_style_v3_initial951_20260628_profile.csv`.
- Output pool: `.worktrees/psg-long-seed-mutation/.codex-run/psg_long_seed_mutation_source_pool_v1_pool.csv`; cluster summary: `_cluster_summary.csv`; summary: `_summary.md`.
- Default result: 951 source rows -> 222 eligible -> 49 selected mutation sources. Cluster mix: `seed_long_maze` 24, `seed_long_lock` 9, `seed_long_weave` 16. This is a read-only source pool; no mutated assets or pack yet.

## Generated-Root Whole-Board Planner V4 Assets - 2026-06-28

- Worktree asset root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV4/`. Use short run names such as `grwbp_v4_s5` to avoid Windows MAX_PATH when writing `.asset/.meta`.
- Current positive review assets: `grwbp_v4_s5/` (4-chain, coverage `0.6493-0.6507`, official 4/4 solved, process A, HardPotential `0.653`, supportDepth4). These are research candidates, not Demo-mounted levels.
- 5-chain negative/boundary assets: `grwbp_v4_s9_5c_safe/` (solved/A/support4 but MediumStructure `0.507` due B2 owner/lane stacking) and `grwbp_v4_s10_5c_cap_summary.md` (lane/release cap gives 0 candidates).
- Earlier diagnostics: `generated_root_wbp_v4_support_smoke2_fourchain_narrow/` produced greedy-unsolved 4-chain assets; keep as a cautionary negative that planned support alone can choose deadlock-prone combinations.
- Next asset-producing step should only happen after adding cross-lane safe carrier grammar; do not mount V4 research assets to Demo or treat them as high-coverage production content.

## Generated-Root Whole-Board Planner V5 Assets - 2026-06-28

- Worktree asset root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV5/`.
- `grwbp_v5_s14_topocarrier_candidate3/`: 2 solved/process A research assets, coverage `0.6406/0.6420`; positive proof that non-owner0 topology carrier can become an official parent, but hard class remains MediumStructure.
- `grwbp_v5_s20_stateactual_audit/`: stateActual audit assets for the same shallow carrier family; candidate rows show max state actual carrier depth `2` and score `0.294`.
- `grwbp_v5_s21_stateactual_gate/`: strict stateActual gate run; 0 candidates when requiring actual added carrier depth>=3/score>=0.45.

## Generated-Root Whole-Board Planner V6 Assets - 2026-06-28

- Worktree asset root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV6/`.
- `grwbp_v6_s7_pairrelay_anylate/`: first V6 pair relay research assets; 2 candidates, coverage `0.6536/0.6551`, official trace 2/2 solved/process A, but hard class MediumStructure and relay remains ReleasedLeaf.
- V6 negative strict relay diagnostics: `grwbp_v6_s3_pairrelay_targetblock/`, `grwbp_v6_s4_pairrelay_prefixblock/`, `grwbp_v6_s5_pairrelay_forwardclear/`, `grwbp_v6_s8_pairrelay_directunlock/`, and `grwbp_v6_s10_pairrelay_b2parent_fixed/` all produce 0 valid pair relays under stricter gates. Use their summaries to understand why direct support relay is not yet available.
- V6 assets are research outputs, not Demo-mounted production levels. Do not treat `pairRelay>0` as success unless pair/state audit or official attribution proves added support carrier.

## Generated-Root Whole-Board Planner V7 Assets - 2026-06-28

- Worktree asset root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV7/`.
- `grwbp_v7_s2_frontier_smoke.asset`: first state-frontier positive proof; coverage `0.6333333`, official solved/process A/HardPotential,新增链 `59/60` are official parents released by root owner `24`.
- `grwbp_v7_s3_frontier_diverse_c001.asset` and `_c002.asset`: frontier + B2 safe boundary assets; coverage `0.6449275`, official solved/process A but MediumStructure, used as negative proof that owner0 B2 capacity refill dilutes difficulty.
- V7 assets are research outputs, not Demo-mounted production levels. Current accepted continuation is target/basin diversity for state-frontier contracts, not mounting these assets or treating s3 coverage gain as success.

## Generated-Root Whole-Board Planner V8 Assets - 2026-06-28

- Worktree asset root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV8/`.
- `grwbp_v8_s1c_single_frontier_c001`..`c006`: single state-frontier proof assets, official 6/6 solved/process A/HardPotential; coverage `0.6217-0.6275`.
- `grwbp_v8_s2d_frontier_pair_seed_c001`..`c008`: current positive pair-seed assets, each with two `STATE_FRONTIER_B1_TO_B2` chains (`24->51` + `24->42`), official 8/8 solved/process A/HardPotential; coverage `0.6319-0.6348`.
- V8 assets are research outputs, not Demo-mounted production levels. Next asset-producing step should expand pair seed into bundle/all-owner frontier contracts while preserving official solved/A/HardPotential; do not use B2 safe/capacity refill to chase coverage.

## Generated-Root Whole-Board Planner V9 Assets - 2026-06-28

- Worktree asset root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV9/`.
- Positive bundle proof: `grwbp_v9_s1b_bundle3_b1b2_c001`..`c008`; each uses 3 state-frontier chains, coverage `0.6377-0.6391`, official trace `8/8 solved`, process `A`, hard class `6 HardPotential + 2 MediumStructure`.
- V9 s1b reports: candidates `grwbp_v9_s1b_bundle3_b1b2_candidates.csv`, chain plan `_chain_plan.csv`, official trace `_trace_metrics.csv` / `_trace_steps.csv`, relation audit `_relation_audit_summary.md`, difficulty attribution `_difficulty_attribution_summary.md`.
- 4-chain boundary diagnostics: `grwbp_v9_s1_bundle4_b1b2`, `grwbp_v9_s1c_bundle4_sameedge_diag`, and `grwbp_v9_s2_bundle4_allbasin` all produce 0 candidates; rejection is dominated by depth-4 cell overlap, not by distinct-edge, same-edge, or basin-filter choice.
- Edge audit: `grwbp_v9_s3_edgeaudit_allbasin_carrier_profile.csv` includes `state_frontier_edge_scan` rows. It shows 24 ranked root-graph edges but actual generated state-frontier options still only for `24->26/33/42/51`.
- V9 assets are research outputs, not Demo-mounted production levels. Next asset-producing step should implement slot/direction-aware bundle cutting before trying to climb added-chain count or coverage.

## Generated-Root Whole-Board Planner V10 Assets - 2026-06-28

- Worktree asset root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV10/`.
- Valid slot-aware baseline: `grwbp_v10_s4_slotaware_bundle4_wide_fixed_c001`..`c008`; each preserves generated root `geosupply_sched_root10_from_40eb0da7_r1_c038`, adds 4 short/medium `STATE_FRONTIER_*` chains with strict slot offsets `0/1/2/3`, coverage `0.6449275`, addedCells `20`, official trace 8/8 solved, process A, all HardPotential.
- s4 reports: candidates `grwbp_v10_s4_slotaware_bundle4_wide_fixed_candidates.csv`, chain plan `_chain_plan.csv`, official trace `_trace_metrics.csv` / `_trace_steps.csv`, relation audit `_relation_audit_summary.md`, difficulty attribution `_difficulty_attribution_summary.md`.
- Negative/boundary diagnostics: `grwbp_v10_s1_slotaware_bundle4` and `grwbp_v10_s2_slotaware_bundle4_sameedge` produce 0 candidates with per-edge 24; s3 wide run before the `option_int(0)` fix is diagnostic only and must not be used as a baseline.
- Current limitation: valid s4 still uses activation owner `24` only and target edges `24->26/33/42/51`; supportCarrierCount remains 0 and collapse risk is `local_penalty_dense;no_added_support_carrier`. V10 assets are research outputs, not Demo-mounted production levels.

## Generated-Root Whole-Board Planner V11 Assets - 2026-06-28

- Worktree asset root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV11/`.
- Negative/boundary run: `grwbp_v11_s6d_bundle4_extend1_v1fast_b1b2` generates 5-chain candidates by appending `B2_CONVERGE_CHOKE` to the V10-style 4 frontier bundle. Official trace solves/process A, but all rows are MediumStructure; use it as proof that B2 safe extension is not the coverage path.
- Positive 5-chain run: `grwbp_v11_s7_bundle4_extend1_v1only_b1b2` disables B2 safe and appends non-B2 owner10/owner7 V1 semantic chains. Official trace is 4/4 solved, process A, all HardPotential; attribution shows support carrier for 3/4 rows.
- Current positive 6-chain run: `grwbp_v11_s8_bundle4_extend2_v1only_b1b2` uses 4 owner24 state-frontier chains plus owner10 `B1_CONVERGE_CHOKE` and owner7 `B1_BLOCKS_B2`. Coverage `0.6565-0.6580`, official 4/4 solved/process A/HardPotential, all added chains official touched, difficulty attribution includes `SupportCrossCarrier=4`.
- Key reports for s8: candidates `_candidates.csv`, chain plan `_chain_plan.csv`, trace `_trace_metrics.csv` / `_trace_steps.csv`, relation audit `_relation_audit_summary.md`, difficulty attribution `_difficulty_attribution_summary.md`. These are research assets, not Demo-mounted production levels and not a 0.95 coverage solution.

## Generated-Root Whole-Board Planner V12 Assets - 2026-06-28

- Worktree asset root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/`.
- Current activation-pattern proof/boundary run: `grwbp_v12_s8y_activationpattern_frontier_edgegate_c001`..`c008`; each preserves generated root `geosupply_sched_root10_from_40eb0da7_r1_c038` and selects 4 short/medium `STATE_FRONTIER_B1_TO_B1` chains with activation owners `24/24/24/7`.
- s8y reports: candidates `grwbp_v12_s8y_activationpattern_frontier_edgegate_candidates.csv`, chain plan `_chain_plan.csv`, carrier profile `_carrier_profile.csv`, planned relations `_planned_relations.csv`, root identity `_root_identity.csv`, summary `_summary.md`.
- Official full8 trace: `grwbp_v12_s8y_activationpattern_frontier_edgegate_trace_full8_metrics.csv` / `_summary.md`; 8/8 solved, process A/tight A, maxChoices avg 6, but 8/8 MediumStructure with hardV3 avg about `0.406`.
- Current positive hard-preserving run: `grwbp_v12_s10e_edgepattern_extend2_v1only_candidate6_c001`..`c008`; each preserves generated root `geosupply_sched_root10_from_40eb0da7_r1_c038`, uses 6 short/medium semantic chains, and combines ordered V10 hard edges with owner10 `B1_CONVERGE_CHOKE` plus owner7 `B1_BLOCKS_B2`.
- s10e reports: candidates `grwbp_v12_s10e_edgepattern_extend2_v1only_candidate6_candidates.csv`, chain plan `_chain_plan.csv`, carrier profile `_carrier_profile.csv`, relation audit `_relation_audit_summary.md`, and difficulty attribution `_difficulty_attribution_summary.md`.
- s10e official full8 trace: `grwbp_v12_s10e_edgepattern_extend2_v1only_candidate6_trace_full8_metrics.csv` / `_summary.md`; 8/8 solved, process A, 8/8 HardPotential, coverage `0.6551-0.6580`, hardV3 avg about `0.568`, maxChoices avg 7.
- Runner positive reproduction: `grwbp_v12_runner_hardbase_ext_t4i_v1only_c001`..`c004`; generated by `Invoke-GeneratedRootWBPV12HardbaseProfile.ps1`, coverage `0.6565-0.6580`, 6 short/medium semantic chains, no B2 filler. Official trace `grwbp_v12_runner_hardbase_ext_t4i_v1only_trace_full4_metrics.csv` is 4/4 solved, process A, 4/4 HardPotential.
- Runner negative boundary: `grwbp_v12_runner_hardbase_ext_t4h_mincontract2` allows `CARRIER_*` extension and is official 4/4 solved/A but 4/4 MediumStructure; use it as proof that generic carrier/weak support extension is not hard-preserving.
- Secondary diagnostic baseline: `grwbp_v12_runner_secondary_t2a_v1only` produced activation and extension seed states but 0 secondary-source states/candidates; initial summary recorded `child_head_not_allowed`, `blocked_to_parent`, and `child_no_path`.
- Secondary duty-zone/multidir diagnostics: `grwbp_v12_secondary_duty_t2a`, `grwbp_v12_secondary_multidir_diag_t2a`, `grwbp_v12_secondary_relaxed_L_diag_t2a`, `grwbp_v12_secondary_geometryonly_L_diag_t2a`, and `grwbp_v12_secondary_addedhit_L_diag_t2a` are negative diagnostics. They show duty-zone adds 0 new cells because the V0 plan is already dense, `U/R/D/L` still produce secondarySource=0, and even relaxed geometry fails through root/selected/out-of-board occupation plus first-hit mismatch.
- Planned added-hit diagnostics: `grwbp_v12_secondary_plannedhit_L_diag_t2b` and `grwbp_v12_secondary_plannedhit_L_diag_t2c` are negative diagnostics. Enabling planned added-first-hit changes the terminal reject to `child_planned_added_hit_greedy_unsolved=6`; reject samples show child `SSF99501` accidentally blocks `O00089 / B1_BLOCKS_B2` at `(8,15)` and full Greedy is unsolved.
- Cluster exact6 positive/boundary run: `grwbp_v12_cluster_edge7_support_exact6_probe_t1_c001`..`c004`; each preserves generated root `geosupply_sched_root10_from_40eb0da7_r1_c038`, keeps chain legality, and uses 6 short/medium semantic chains: 4 owner24 hard-frontier chains, owner7 `STATE_FRONTIER_B1_TO_B1 7->22`, and owner7 `B1_BLOCKS_B2 7->53`. Coverage is `0.6579710-0.6594203`, addedDagCycleCount=0.
- Cluster exact6 reports: candidates `grwbp_v12_cluster_edge7_support_exact6_probe_t1_candidates.csv`, chain plan `_chain_plan.csv`, carrier profile `_carrier_profile.csv`, planned relations `_planned_relations.csv`, root identity `_root_identity.csv`, and summary `_summary.md`.
- Cluster exact6 official trace: `grwbp_v12_cluster_edge7_support_exact6_probe_t1_trace_full4_metrics.csv` / `_summary.md`; 4/4 solved, process A/tight A, maxChoices avg 7, but all MediumStructure with `CounterfactualLocalFlow`. Treat as source-frontier cluster mechanism proof, not a hard/coverage success.
- Cluster exact7 boundary: `grwbp_v12_cluster_edge7_support_exact7_probe_t1` produced 0 candidates; extra3 rejected mainly on cell overlap and greedy unsolved. Current plateau is geometry/ray/duty-plan limited, not a simple chain-count knob.
- Rootlang planned added-DAG proof: `grwbp_v12_rootlang_edgepattern5_plannedaddedhit_t1_c001`..`c004`; each preserves generated root `rootlang_root10_0615_section_short_r1_c024`, adds 5 short/medium `STATE_FRONTIER_*` chains from edges `7->22,24->55,24->26,24->42,24->33`, has chainLegalityOk=True, addedChainLoopRiskCount=0, plannedAddedFirstHitCount=1, addedDagCycleCount=0, and coverage `0.6536-0.6551`.
- Rootlang planned added-DAG reports: candidates `grwbp_v12_rootlang_edgepattern5_plannedaddedhit_t1_candidates.csv`, chain plan `_chain_plan.csv`, carrier profile `_carrier_profile.csv`, planned relations `_planned_relations.csv`, root identity `_root_identity.csv`, summary `_summary.md`, and official trace `grwbp_v12_rootlang_edgepattern5_plannedaddedhit_t1_trace_full4_metrics.csv` / `_summary.md`.
- Rootlang planned added-DAG official trace: 4/4 solved, process A/tight A, maxChoices avg 6, but all MediumStructure with hardV3 avg about `0.419` and risk `DependencyFollowRun`; treat as a formal planned-DAG mechanism proof, not a HardPotential or coverage success.
- Rootlang 6-edge boundary: `grwbp_v12_rootlang_edgepattern6_plannedaddedhit_t1` adds `24->51` to the planned 5-edge pattern and produces 0 candidates. Summary reports `disjointFull=0` and `disjoint_lookahead_dead=5`; carrier profile reports `maxDisjointDistinctEdge=5`. This confirms current rootlang cell plan cannot support a sixth independent semantic frontier edge.
- Rootlang strict duty/ray capacity boundary: `grwbp_v12_rootlang_dutyray_probe_t3_alldirs_allbasins_cuttable_noloop_impact.csv` is the current strict evidence file. It uses V12 cuttable preflight, added loop-risk rejection, and release-impact safety; current/all-empty modes both cap at `maxDisjointDistinctEdge=5`, with best edges `36->11,24->33,24->51,24->42,24->26`.
- Rootlang duty-seed diagnostic proof: `grwbp_v12_rootlang_dutyseed_onlypool_t1_carrier_profile.csv` injects probe-derived `SFD*` options and proves the old frontier option enumerator can miss globally friendly candidates. It is diagnostic only; the apparent sixth corridor is invalid after loop-risk/release-impact gates.
- Rootlang duty-seed negative outputs: `grwbp_v12_rootlang_edgepattern6_dutyseed_seedonly_t1_summary.md` shows the 6-edge seed-only attempt writes 0 candidates because one seed is added loop-risk, and `grwbp_v12_rootlang_edgepattern6_dutyseed_multidir_t1_summary.md` shows the multidir attempt is rejected by `blocks_pre_release_owner`.
- Duty/ray diagnostic output directories: `grwbp_v12_rootlang_dutyseed_onlypool_t1/`, `grwbp_v12_rootlang_edgepattern6_dutyseed_seedonly_t1/`, and `grwbp_v12_rootlang_edgepattern6_dutyseed_multidir_t1/` under the V12 GeneratedRootWholeBoardPlanner level root are no-candidate or diagnostic outputs, not production candidates.
- Strict duty/ray root gate top6: `grwbp_v12_strict_dutyray_root_gate_top6_t1_summary.csv` ranks the first 6 generated hard roots by strict duty/ray capacity. `geosupply_sched_root10_dens_c6277f51_r1_c007` is the best in this slice with `bestChainDisjoint=6`, `bestReserveDisjoint=6`, and bestEdges `21->11,24->32,17->8,24->26,24->29,5->15`; other top6 roots are 4-5.
- Strict duty/ray quality top12: `grwbp_v12_strict_dutyray_quality_top12_t1_summary.csv` adds relation-quality tags to the strict gate. Top12 distribution is strict chain capacity `6 x1 / 5 x8 / 4 x3`; every row is tagged `capacity_lt_8`. `c6277` has the only 6 but is tagged low root footprint, weak cross/choke share, same-release dense, early B1 cluster, and support proxy <=1. Rootlang-style rows have better footprint/cross material but still cap at 5 and repeat owner24 early B1.
- Strict duty/ray quality-aware top3: `grwbp_v12_strict_dutyray_qualityaware_top3_t1_summary.csv` uses probe-native `chain_quality/reserve_quality` set selection and wrapper `-UseQualityDisjointSet`. c6277 stays at `bestChainDisjoint=6` but chooses `21->11,24->32,17->9,5->15,24->26,24->29`, improving cross/choke/support proxy tags versus score-only selection. All top3 still have `bestChainQualityGatePass=False`; this is a gate/selection improvement, not a new asset-producing success.
- Trace-wide root pool: `grwbp_v12_root_pool_tracewide_excl12_t1.csv` and `_summary.md` are built from historical trace metrics while excluding the old 12-root shortlist. They select 48 generated roots and expose new `root154_*` candidates around root coverage `0.57-0.61`.
- Trace-wide strict scans: `grwbp_v12_strict_dutyray_tracewide_excl12_qualityaware_top4_t1_summary.csv` and `grwbp_v12_strict_dutyray_tracewide_windowed_r5_r8_qualityaware_t1_summary.csv` find additional strict-6 roots (`root154_core_sched0564_v1_r1_c016/c038`) with better relation quality, but still no 8+ quality pass.
- High-footprint negative: `grwbp_v12_strict_dutyray_tracewide_highfootprint_qualityaware_top4_t1_summary.csv` tests roots around coverage `0.66-0.70` and collapses to strict disjoint 1, proving root footprint cannot be rewarded monotonically; overfilled roots consume duty/ray corridor space.
- Occlusion audit reports: `grwbp_v12_occlusion_tracewide_top3_t1_summary.csv` and `grwbp_v12_occlusion_highfootprint_top1_t1_summary.csv` add root/out-of-board/plan blocker totals. Current/all_empty capacities are identical and planBlockedTotal is 0 on tested roots, so the missing corridor capacity must be fixed in generated root/cell-plan construction, not by broadening post-hoc allowed cells.
- Corridor demand bridge: `grwbp_v12_corridor_demand_occlusion_c016_vs_highfoot_t1.csv` and `_summary.md` convert occlusion probe rows into root/owner/cell/edge demand. Use them as generation-side reservation constraints (`root_generation_corridor_hole`, head-neighborhood reserve, boundary-safe head/second space), not as permission to delete root cells after verification.
- Reservation-fit bridge: `grwbp_v12_reservation_fit_tracewide_from_c016_demand_t1.csv`, `_summary.md`, and `_top12_root_pool.csv` rank real generated roots by same-board natural openness against c016 corridor demand. The completed strict reuse summary `grwbp_v12_reservationfit_strict_t1_summary.csv` reaches best strict `7/7` on low-footprint roots, still below 8+ and still far from 0.95 coverage; treat this as evidence for root-generation reservation, not as a cutter baseline.
- Reserved-root generated asset roots: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/ReservedRootGeneratorV1_diag1/`, `ReservedRootGeneratorV1_preserve_top16_t1/`, `ReservedRootGeneratorV1_preserve_top8_t1/`, `ReservedRootGeneratorV1_semantic_top8_t1/`, `ReservedRootGeneratorV1_semdiv_top8_t1/`. These are experimental root-generation diagnostics only; none are official WBP final levels or Demo-mounted packs. Key boundary: top8 preserve best reaches strict 5, semantic low-choice roots drop to strict 2-3, so next work must be source-basin-first generation.
- Source-basin generated asset roots: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/SourceBasinRootGeneratorV1_t1b/`, `SourceBasinRootGeneratorV1_t2b/`, `SourceBasinRootGeneratorV1_t3/`, `SourceBasinRootGeneratorV1_t3b/`, `SourceBasinRootGeneratorV1_t4a/`, `SourceBasinRootGeneratorV1_t4b/`, `SourceBasinRootGeneratorV1_t4c/`, `SourceBasinRootGeneratorV1_t4c_basinproxy/`, `SourceBasinRootGeneratorV1_t5/`, `SourceBasinRootGeneratorV1_t5_smoke/`, `SourceBasinRootGeneratorV1_t5_cuttable_smoke/`, `SourceBasinRootGeneratorV1_t6_cuttable/`, `SourceBasinRootGeneratorV1_t8_slot_nopreflight_smoke/`, `SourceBasinRootGeneratorV1_t9c_audit_smoke/`. These are experimental root-generation diagnostics only; none are official WBP final levels or Demo-mounted packs. Boundary: t1b is too open (16 openers, strict 0-1); t2b is low-choice/multi-top-root but strict only 2-3; t4/t5/t6 proxies overestimate strict capacity; t8 no-preflight semantic slots can pass source-basin chain audit but formal preflight collapses to 1, so the next root generator must plan release-order/root-DAG-compatible slots.
- Source-basin slot/preflight reports: `grwbp_v12_sourcebasin_rootgen_v1_t8_slot_nopreflight_smoke_root_pool.csv`, `_strict_summary.csv`, `_nopreflight_summary.csv`, `_corepreflight_summary.csv`, `_nofrontier_summary.csv`, `_nopreflight_source_basin_audit_summary.md`, and `grwbp_v12_sourcebasin_rootgen_v1_t9c_audit_smoke_root_pool.csv`. t8 no-preflight quality chain row reaches disjoint 6 with 4 activation top roots and 3 cross-top-root edges; release-order/first-hit core preflight and no-frontier both drop to quality 5, while full strict drops to 1. t9c is only an audit-field smoke.
- c6277 exact6 duty-seed output: `grwbp_v12_c6277_strictduty6_seedonly_t1_c001` is a 6-chain diagnostic candidate generated only from strict duty seed options. It preserves root, is chain-legal, has added loop-risk 0, Greedy solved, and coverage `0.5492754`.
- c6277 exact6 official trace: `grwbp_v12_c6277_strictduty6_seedonly_t1_trace_full1_metrics.csv` is solved/process A/tight A with maxChoices 5, but class is `MediumStructure` (`hardV3=0.429`, risk `DependencyFollowRun`). Attribution summary shows all 6 added chains official touched, but support carrier is only 1 and local penalty remains dense. Treat this as capacity/seed-injection proof, not a hard or coverage success.
- Current secondary conclusion: post-hoc secondary extension is rejected as the 0.95 route. Next asset-producing step should co-select added-to-added delay/block DAG clusters before hardbase is frozen, rather than appending a child chain after a 6-chain state.
- V12 assets are research outputs, not Demo-mounted production levels and not a 0.95 solution. Use s8y as activation-pattern/frontier-only boundary and s10e as current hard-preserving positive baseline; next work must expand whole-board duty grammar beyond the 0.658 coverage plateau.

## RCH Experiment Line Archive - 2026-06-27

- RCH/high-root and Reverse-CSSC level, pack, report, trace, and audit entries have been moved out of the active level index.
- See `.agents/memory/RCH_EXPERIMENT_LINE_ARCHIVE_20260627.md` for the high-level archive and `.agents/memory/RCH_LONGCHAIN_NO_ROOT_RECORDS.md` for long-chain/no-root/polluted-baseline negatives.
- Do not use old RCH/Reverse-CSSC level/report rows as current baseline or continuation point unless the user explicitly restarts that line.

## PSG Trace Order Balanced V1 Main Review Pack - 2026-06-28

- Pack: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/PSGTraceOrderBalancedV1ReviewPack.asset`; copied from `.worktrees/psg-long-seed-mutation`, current GUID `ff6cc93ffa894eaa9f152857e252318c`.
- Contents: 2 refs, PSG trial 02 `sgp_pressure_hard_trial_02_sgp_pressure_hard_rect_section_unlock` and trial 04 `sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst`.
- Demo: `Assets/ArrowMagic/Scenes/Demo.unity` previously pointed `activePack` to this pack for manual review; current mounted review pack is recorded in the NutationPeelV1 entry below.
- Boundary: this is a review selection of unchanged PSG trial assets using same-level `RegionDiverseBalanced` metrics; not a generator-side PSG topology change.

## NutationPeelV1 Smoke Pack - 2026-06-28

- Full source pack: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationPeelV1Pack.asset`; copied from `.worktrees/nutation-peel` v1c smoke run.
- Review keep pack: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationPeelV1ProductionKeepPack.asset`; 2 refs, current Demo active pack.
- GUID note: main project level refs are `a9e5408e3bef68243b086a3dede0e7cf` for lock_buckle and `aeb585a74758e7f4aa8ee2d84e18e87c` for core_burst; pack refs were repaired after a worktree-to-main GUID mismatch caused an empty runtime board.
- Level assets: `Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationPeelV1/`.
- Source report: `Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_peel_v1_report.csv`.
- Production keep CSV: `Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_peel_v1_production_keep.csv`.
- Joined comparison: `.codex-run/nutation_peel_v1c_wt_smoke_trace_joined.csv`; PSG comparison joined `.codex-run/nutation_compare_psg_trial_v1c_wt_trace_joined.csv`; summary `.codex-run/nutation_peel_v1c_wt_smoke_vs_psg_summary.md`.
- v1c result: 4/4 traced, 2/4 TraceOrderKeep. Keep rows are `nutation_peel_v1_01_lock_buckle` and `nutation_peel_v1_04_core_burst`; section/dense are diagnostic rejects due choices/process. This is a review/smoke pack, not final mass-production approval.

## PSG Long Stripe-Coil Negative Assets - 2026-06-28

- Worktree: `.worktrees/psg-long-seed-mutation`, branch `codex/psg-long-seed-mutation`.
- Rejected pack family: `PSGLongMazeSelfMadeV3FillProbeV2StagedReviewPack` and related `PSGLongMazeSelfMadeV2/V3`, `PSGStyleSkeletonLong*`, `CrossColumn`, `CrossColumnAlt`, and staged fill-probe outputs.
- Rejected level root: `.worktrees/psg-long-seed-mutation/Assets/ArrowMagic/SOData/Levels/DirectProcedural/PSGLongMazeSelfMadeV3FillProbeV2Staged/`.
- Rejected source report: `.worktrees/psg-long-seed-mutation/Assets/ArrowMagic/SOData/Reports/DirectProcedural/psg_long_maze_selfmade_v3_fill_probe_v2_staged_source_report.csv`.
- Rejected trace metrics: `.worktrees/psg-long-seed-mutation/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/psg_long_maze_selfmade_v3_fill_probe_v2_staged_trace_smoke_metrics.csv`.
- Status: negative only. Do not use these assets as visual target, production input, positive baseline, or Demo review candidate. They are retained only as evidence for the stripe/coil reject rule and for the `source: 1` authored LevelDefinition format pitfall.

## PeelRailV1 Review Pack - 2026-06-28

- Pack: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/PeelRailV1Pack.asset`; current GUID `df09839fe95847d4f8cf1bcece58f36d`.
- Level assets: `Assets/ArrowMagic/SOData/Levels/DirectProcedural/PeelRailV1/`.
- Source report: `Assets/ArrowMagic/SOData/Reports/DirectProcedural/peel_rail_v1_report.csv`.
- Production keep CSV: `Assets/ArrowMagic/SOData/Reports/DirectProcedural/peel_rail_v1_production_keep.csv`.
- Smoke/join reports: `.codex-run/peel_rail_v1_smoke4_trace_joined.csv` and `.codex-run/peel_rail_v1_smoke4_trace_joined_summary.md`.
- Demo: `Assets/ArrowMagic/Scenes/Demo.unity` currently points `activePack` to this pack for manual review.
- Smoke4 result: 4/4 solved; 4/4 `styleFamily=peel_layered`; 4/4 `chainLanguage=rail_chain`; rank classes are `TraceOrderKeep=2`, `VisualKeep=1`, `Reject=1`; production keep rows=2.
- Source shape summary: coverage average about `0.992`, chains average `59.5`, avgChain about `10.1`, maxChain `20`, straightness average about `0.700`. Compared with `NutationPeelV2` straightness about `0.118`, this is the current positive proof that Peel topology can be rendered with rail-chain language.

## NutationPeelRailV1 Worktree Review Pack - 2026-06-28

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationPeelRailV1Pack.asset`; current GUID `01eedf2fe3c8460448527f68a81bf6c4`.
- Level assets: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationPeelRailV1/`.
- Source report: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_peel_rail_v1_report.csv`.
- Production keep CSV: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_peel_rail_v1_production_keep.csv`.
- Smoke/join reports: `.worktrees/nutation-peel/.codex-run/nutation_peel_rail_v1_smoke4_trace_joined.csv` and `_summary.md`.
- Demo in worktree points `activePack` to `NutationPeelRailV1Pack.asset`.
- Smoke4 result: 4/4 solved; 4/4 `rail_chain`; rank classes `TraceOrderKeep=1`, `VisualKeep=1`, `Reject=2`; current status is review/prototype, not production approval.

## PSG Long Lock Planned Corridor Duty V1 Smoke Pack - 2026-06-28

- Worktree: `.worktrees/psg-long-lock-role-grammar`, branch `codex/psg-long-lock-role-grammar`.
- Pack: `.worktrees/psg-long-lock-role-grammar/Assets/ArrowMagic/SOData/Packs/DirectProcedural/PSGLongLockRoleGrammarV1Pack.asset`; worktree Demo activePack points to this pack.
- Level assets: `.worktrees/psg-long-lock-role-grammar/Assets/ArrowMagic/SOData/Levels/DirectProcedural/PSGLongLockRoleGrammarV1/psg_long_lock_role_v1_01_tall_lock_a.asset` and `.../psg_long_lock_role_v1_02_wide_lock_a.asset`.
- Source report: `.worktrees/psg-long-lock-role-grammar/Assets/ArrowMagic/SOData/Reports/DirectProcedural/psg_long_lock_role_grammar_v1_report.csv`; source summary same folder `_summary.md`.
- Source result: 2/2 accepted on first attempt. Tall/wide coverage `0.7009/0.7477`, maxChain `45/55`, initialOpeners `2/2`, max fanout `3/3`, supportCorridorChains `4/6`, crossRegionCarrierChains `2/2`, releaseCorridorEmptyCells `70/87`.
- Planned-duty fields: `plannedCorridorPlaced=1/3`, `plannedCrossRegionPlaced=1/3`, `plannedDutyReleaseCells=5/34`; longVisualCellShare `0.531/0.449`; midShortSupportChains `15/19`; solvableEmptyShare `0.781/0.829`.
- Official trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/psg_long_lock_role_grammar_v1_planduty_v1d_smoke2_metrics.csv`; missing/failed 0.
- Joined summary: `.worktrees/psg-long-lock-role-grammar/.codex-run/psg_long_lock_role_grammar_v1_planduty_v1d_smoke2_trace_joined_summary.md`; production keep: `.worktrees/psg-long-lock-role-grammar/.codex-run/psg_long_lock_role_grammar_v1_planduty_v1d_smoke2_production_keep.csv`.
- Trace/join result: 2/2 solved and 2/2 `TraceOrderKeep`. Wide row is A/clean, avg/max choices `3.68/6`, STS `0.917`, collapse `0.108`; tall row is B/watch, avg/max choices `4.25/7`, STS `0.950`, collapse `0.049`.
- Status: current best self-produced long-lock smoke pack; planned corridor duty works, but coverage is still below real seed_long_lock pool parity.

## NutationLongChainSpineV1 Worktree Review Pack - 2026-06-28

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationLongChainSpineV1Pack.asset`.
- Level assets: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationLongChainSpineV1/`.
- Source report: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_longchain_spine_v1_report.csv`.
- Production keep CSV: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_longchain_spine_v1_production_keep.csv`.
- Smoke/join reports: `.worktrees/nutation-peel/.codex-run/nutation_longchain_spine_v1_smoke3_trace_joined.csv` and `_summary.md`.
- Demo in worktree points `activePack` to `NutationLongChainSpineV1Pack.asset` after smoke3.
- Smoke3 result: 4/4 solved; 4/4 `styleFamily=maze_long_chain`; 4/4 `chainLanguage=spine_chain`; rank classes `TraceOrderKeep=2`, `Reject=2`; production keep rows=2.
- Source shape summary: average coverage about `0.928`, chains about `41.0`, avgChain about `14.56`, maxChain about `27.8`, openers about `4.75`. This is a style/language proof and not yet PSG-level coverage production.

## NutationHubSpokeV1 Worktree Style Proof Pack - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubSpokeV1Pack.asset`.
- Level assets: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationHubSpokeV1/`.
- Source report: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_spoke_v1_report.csv`.
- Production keep CSV: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_spoke_v1_production_keep.csv` currently has 0 rows under TraceOrderPreferred.
- Current trace/join reports: `.worktrees/nutation-peel/.codex-run/nutation_hub_spoke_v1_current_trace_trace_joined.csv` and `_summary.md`.
- Current result: 3/3 solved; 3/3 `styleFamily=hub_spoke`; 3/3 `chainLanguage=patch_chain`; rank classes `VisualKeep=1`, `ProcessKeep=1`, `Reject=1`; production keep rows=0.
- Source style signal: fanout `maxFanout=4-5`, `hubOwners=19-22`, `ownerHit=84-98`, `cross=11-19`. Boundary: joined rows remain `local_collapse/high_risk`, so this is a style proof and not production approval.

## NutationHubSpokeV2 Worktree Anti-Collapse Probe Pack - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubSpokeV2Pack.asset`.
- Level assets: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationHubSpokeV2/`.
- Source report: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_spoke_v2_report.csv`.
- Production keep CSV: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_spoke_v2_production_keep.csv` currently has 0 rows under TraceOrderPreferred.
- Smoke/join reports: `.worktrees/nutation-peel/.codex-run/nutation_hub_spoke_v2_smoke4_trace_joined.csv` and `_summary.md`.
- Smoke4 result: 3/4 source rows traceable; 3/3 solved; 3/3 `styleFamily=hub_spoke`; 3/3 `chainLanguage=patch_chain`; rank classes `VisualKeep=1`, `ProcessKeep=1`, `Reject=1`; production keep rows=0.
- Comparison note: V2 lowers V1 localPatchRun average `9.0 -> 8.33` and same-axis/same-dir max `15/12 -> 10/10`, but remains `local_collapse/high_risk` with 0 STS pass.

## NutationFlowCurveV1 Worktree Baseline Review Pack - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationFlowCurveV1Pack.asset`.
- Level assets: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationFlowCurveV1/`.
- Source report: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_flow_curve_v1_report.csv`.
- Production keep CSV: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_flow_curve_v1_production_keep.csv` uses Flow review mode, not strict production.
- Smoke/join reports: `.worktrees/nutation-peel/.codex-run/nutation_flow_curve_v1_smoke1_trace_joined.csv` and `_summary.md`.
- Smoke1 result: 4/4 solved; 4/4 `styleFamily=flow_continuous`; 4/4 `chainLanguage=curve_chain`; Flow review keep rows=4 under Drop-allowed VisualOnly mode.
- Source shape summary: coverage `0.982-0.992`, avgChain `9.12-9.84`, maxChain `18-19`, straightness `0.123-0.153`.

## NutationMazePatchV1 Worktree Style Proof Pack - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationMazePatchV1Pack.asset`.
- Level assets: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationMazePatchV1/`.
- Source report: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_maze_patch_v1_report.csv`.
- Production keep CSV: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_maze_patch_v1_production_keep.csv` currently has 0 rows under TraceOrderPreferred.
- Smoke/join reports: `.worktrees/nutation-peel/.codex-run/nutation_maze_patch_v1_smoke3_trace_joined.csv` and `_summary.md`.
- Smoke3 result: 4/4 solved; 4/4 `styleFamily=constraint_maze`; 4/4 `chainLanguage=patch_chain`; rank classes `Reject=4`; production keep rows=0.
- Source shape summary: coverage `0.905-0.924`, chains `92-101`, avgChain `5.93-6.10`, maxChain `10-11`, straightness `0.195-0.224`, openers `3`. Boundary: joined rows remain `local_collapse/high_risk`, so this is style proof only.

## NutationPeelPatchV1 Worktree Near-Miss Pack - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationPeelPatchV1Pack.asset`.
- Level assets: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationPeelPatchV1/`.
- Source report: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_peel_patch_v1_report.csv`.
- Production keep CSV: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_peel_patch_v1_production_keep.csv` currently has 0 rows under TraceOrderPreferred.
- Current smoke/join reports: `.worktrees/nutation-peel/.codex-run/nutation_peel_patch_v1_smoke6_trace_joined.csv` and `_summary.md`.
- Smoke6 result: 4/4 solved; 4/4 `styleFamily=peel_layered`; 4/4 `chainLanguage=patch_chain`; 4/4 STS pass; rank classes `Reject=4`; production keep rows=0.
- Source shape summary: coverage `0.979-0.987`, chain count about `75-85`, avgChain about `7.3-7.4`, maxChain `13`, straightness about `0.40-0.47`, short patch chains `25-29`.
- Best row `core_patch`: coverage `0.987`, choices `7.25/11`, local/nearOuter `4/4`, directionalRisk `0.155`, STS `0.858`, collapse `0.170`, sameAxis `6`, sameDir `5`. Boundary: only strict near-miss; not production-approved.
- Negative smoke5 note: stronger opening/edge-head 回压 got one `ProcessKeep` but regressed into `local_collapse` and STS pass 2/4, so do not use local opener crushing as the next route.

## Nutation Style Matrix V1 Worktree Reports - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Exporter: `.worktrees/nutation-peel/Tools/Production/Export-NutationStyleMatrixV1.ps1`.
- Matrix CSV: `.worktrees/nutation-peel/.codex-run/nutation_style_matrix_v1_current_matrix.csv`.
- Best rows CSV: `.worktrees/nutation-peel/.codex-run/nutation_style_matrix_v1_current_best_rows.csv`.
- Strict keep rows CSV: `.worktrees/nutation-peel/.codex-run/nutation_style_matrix_v1_current_strict_keep_rows.csv`.
- Combined rows CSV: `.worktrees/nutation-peel/.codex-run/nutation_style_matrix_v1_current_combined_rows.csv`.
- Summary: `.worktrees/nutation-peel/.codex-run/nutation_style_matrix_v1_current_summary.md`.
- Strict review pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationStyleMatrixStrictReviewPack.asset`; last refreshed pack still has 12 refs from before LongChainPatch, while current strict keep CSV has 15 rows.
- Default inputs: FlowCurve smoke1, FlowRail smoke1, FlowPatch smoke1, PeelCurve v1c, PeelRail smoke7, PeelPatch smoke6, LongChainSpine smoke3, LongChainCurve smoke1, LongChainRail smoke2, LongChainPatch smoke1, HubCurve smoke1, HubRail smoke2, HubSpokeV4 smoke4, HubSpokeV3 smoke1, HubSpokeV2 smoke4, MazeRail smoke4, MazeCurve smoke1, MazePatchV2 smoke2, MazePatchV1 smoke3 joined CSVs.
- Result: 19 lanes / 73 rows; FlowCurve/FlowRail/FlowPatch are review-only, PeelCurve/PeelRail/LongChainSpine/LongChainCurve/LongChainRail/LongChainPatch are strict-review-ready, PeelPatch is strict-near-miss, and HubCurve/HubRail/HubSpokeV4/V3/V2 plus MazeCurve/MazeRail/MazePatchV2/V1 need solve-time control or remain style-proof only.

## NutationFlowRailV1 Worktree Review Pack - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Wrapper: `.worktrees/nutation-peel/Tools/Production/Invoke-NutationFlowRailProductionV1.ps1`; Unity method `NoMaskProceduralGenerator.BuildNutationFlowRailV1Pack`.
- Pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationFlowRailV1Pack.asset`.
- Level assets: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationFlowRailV1/`.
- Source report: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_flow_rail_v1_report.csv`.
- Production keep CSV: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_flow_rail_v1_production_keep.csv` has 3 rows under VisualOnly review mode.
- Smoke/join reports: `.worktrees/nutation-peel/.codex-run/nutation_flow_rail_v1_smoke1_trace_joined.csv` and `_summary.md`; official metrics in sibling rhythm lab as `nutation_flow_rail_v1_smoke1_metrics.csv`.
- Smoke1 result: 4/4 traced solved, 4/4 `flow_continuous`, 4/4 `rail_chain`/`flow_rail_chain`, 4 TraceOrderKeep under Flow VisualOnly review mode. Source straightness `0.598-0.654`; review-only, not strict production.

## NutationFlowPatchV1 Worktree Review Pack - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Wrapper: `.worktrees/nutation-peel/Tools/Production/Invoke-NutationFlowPatchProductionV1.ps1`; Unity method `NoMaskProceduralGenerator.BuildNutationFlowPatchV1Pack`.
- Pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationFlowPatchV1Pack.asset`.
- Level assets: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationFlowPatchV1/`.
- Source report: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_flow_patch_v1_report.csv`.
- Production keep CSV: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_flow_patch_v1_production_keep.csv` has 3 rows under VisualOnly review mode.
- Smoke/join reports: `.worktrees/nutation-peel/.codex-run/nutation_flow_patch_v1_smoke1_trace_joined.csv` and `_summary.md`; official metrics in sibling rhythm lab as `nutation_flow_patch_v1_smoke1_metrics.csv`.
- Smoke1 result after light opener/chain-count retune: 4/4 traced solved, 4/4 `flow_continuous`, 4/4 `patch_chain`/`flow_patch_chain`, 2 TraceOrderKeep + 1 VisualKeep + 1 Reject. Review-only; use as Flow patch-language comparison, not strict production.

## NutationHubCurveV1 Worktree Style Proof Pack - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Wrapper: `.worktrees/nutation-peel/Tools/Production/Invoke-NutationHubCurveProductionV1.ps1`; Unity method `NoMaskProceduralGenerator.BuildNutationHubCurveV1Pack`.
- Pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubCurveV1Pack.asset`.
- Level assets: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationHubCurveV1/`.
- Source report: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_curve_v1_report.csv`.
- Production keep CSV: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_curve_v1_production_keep.csv` currently has 0 rows under TraceOrderPreferred.
- Smoke/join reports: `.worktrees/nutation-peel/.codex-run/nutation_hub_curve_v1_smoke1_trace_joined.csv` and `_summary.md`.
- Smoke1 result: 3 traceable rows / 3 solved; 3/3 `styleFamily=hub_spoke`; 3/3 `chainLanguage=curve_chain`; 3/3 `chainLanguageDetail=hub_curve_chain`; rank classes `ProcessKeep=2 / Reject=1`; production keep rows=0.
- Best proof row is `dual_curve`: STS `0.823`, collapse `0.262`, sameAxis `8`, sameDir `6`, source straightness `0.210`, but local/nearOuter `9/7`, directionalRisk `0.515`, dependencyLocal `0.677`; style-proof only, not production.

## NutationMazeCurveV1 Worktree Style Proof Pack - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Wrapper: `.worktrees/nutation-peel/Tools/Production/Invoke-NutationMazeCurveProductionV1.ps1`; Unity method `NoMaskProceduralGenerator.BuildNutationMazeCurveV1Pack`.
- Pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationMazeCurveV1Pack.asset`.
- Level assets: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationMazeCurveV1/`.
- Source report: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_maze_curve_v1_report.csv`.
- Production keep CSV: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_maze_curve_v1_production_keep.csv` currently has 0 rows under TraceOrderPreferred.
- Smoke/join reports: `.worktrees/nutation-peel/.codex-run/nutation_maze_curve_v1_smoke1_trace_joined.csv` and `_summary.md`.
- Smoke1 result: 4 specs, 1 traceable row / 1 solved; `styleFamily=constraint_maze`; `chainLanguage=curve_chain`; `chainLanguageDetail=maze_curve_chain`; rank class `VisualKeep`; production keep rows=0.
- Boundary: low-yield style/language proof only, not production-approved. Main blockers are same-axis, STS/collapse boundary, and local-collapse.

## NutationMazeRailV1 Worktree Style Proof Pack - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Wrapper: `.worktrees/nutation-peel/Tools/Production/Invoke-NutationMazeRailProductionV1.ps1`; Unity method `NoMaskProceduralGenerator.BuildNutationMazeRailV1Pack`.
- Pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationMazeRailV1Pack.asset`.
- Level assets: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationMazeRailV1/`.
- Source report: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_maze_rail_v1_report.csv`.
- Production keep CSV: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_maze_rail_v1_production_keep.csv` currently has 0 rows under TraceOrderPreferred.
- Smoke/join reports: `.worktrees/nutation-peel/.codex-run/nutation_maze_rail_v1_smoke4_trace_joined.csv` and `_summary.md`.
- Smoke4 result: 3 traceable rows / 3 solved; 3/3 `styleFamily=constraint_maze`; 3/3 `chainLanguage=rail_chain`; rank classes `ProcessKeep=1 / Reject=2`; production keep rows=0.
- Best proof row is `core_rail_maze`: STS `0.887`, collapse `0.206`, sameAxis `8`, sameDir `6`, stripeRisk `0.023`, but processTier `Drop`; style-proof only, not production.

## NutationLongChainCurveV1 Worktree Strict Review Pack - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Wrapper: `Tools/Production/Invoke-NutationLongChainCurveProductionV1.ps1`; Unity method `NoMaskProceduralGenerator.BuildNutationLongChainCurveV1Pack`.
- Pack: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationLongChainCurveV1Pack.asset`.
- Level assets: `Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationLongChainCurveV1/`.
- Source report: `Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_longchain_curve_v1_report.csv`.
- Production keep CSV: `Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_longchain_curve_v1_production_keep.csv` has 3 rows under TraceOrderPreferred.
- Smoke/join reports: `.codex-run/nutation_longchain_curve_v1_smoke1_trace_joined.csv` and `_summary.md`; official metrics in sibling rhythm lab as `nutation_longchain_curve_v1_smoke1_metrics.csv`.
- Smoke1 second pass result: 4/4 traced solved, 4/4 `maze_long_chain`, 4/4 `curve_chain`/`long_curve_chain`, 3 TraceOrderKeep + 1 Reject. Source straightness `0.343-0.389`, avgChain `11.55-12.56`, maxChain `21-24`.
- Style matrix: `.codex-run/nutation_style_matrix_v1_current_summary.md` now covers 15 lanes / 57 rows with 9 strict keeps: LongChainCurve 3, LongChainSpine 2, PeelCurve 2, PeelRail 2.

## NutationLongChainRailV1 Worktree Strict Review Pack - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Wrapper: `Tools/Production/Invoke-NutationLongChainRailProductionV1.ps1`; Unity method `NoMaskProceduralGenerator.BuildNutationLongChainRailV1Pack`.
- Pack: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationLongChainRailV1Pack.asset`.
- Level assets: `Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationLongChainRailV1/`.
- Source report: `Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_longchain_rail_v1_report.csv`.
- Production keep CSV: `Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_longchain_rail_v1_production_keep.csv` has 3 rows under TraceOrderPreferred.
- Smoke/join reports: `.codex-run/nutation_longchain_rail_v1_smoke2_trace_joined.csv` and `_summary.md`; official metrics in sibling rhythm lab as `nutation_longchain_rail_v1_smoke2_metrics.csv`.
- Smoke2 result: 4/4 traced solved, 4/4 `maze_long_chain`, 4/4 `rail_chain`/`long_rail_chain`, 4/4 visualPass, 3 TraceOrderKeep + 1 VisualKeep. Source straightness `0.408-0.490`, avgChain `12.45-13.43`, maxChain `22-23`.
- Style matrix at this checkpoint covered 16 lanes / 61 rows with 12 strict keeps: LongChainCurve 3, LongChainRail 3, LongChainSpine 2, PeelCurve 2, PeelRail 2. Current matrix summary is updated later to 19 lanes / 73 rows after FlowRail/FlowPatch and LongChainPatch.

## NutationLongChainPatchV1 Worktree Strict Review Pack - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Wrapper: `.worktrees/nutation-peel/Tools/Production/Invoke-NutationLongChainPatchProductionV1.ps1`; Unity method `NoMaskProceduralGenerator.BuildNutationLongChainPatchV1Pack`.
- Pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationLongChainPatchV1Pack.asset`.
- Level assets: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationLongChainPatchV1/`.
- Source report: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_longchain_patch_v1_report.csv`.
- Production keep CSV: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_longchain_patch_v1_production_keep.csv` has 3 rows under TraceOrderPreferred.
- Smoke/join reports: `.worktrees/nutation-peel/.codex-run/nutation_longchain_patch_v1_smoke1_trace_joined.csv` and `_summary.md`; official metrics in sibling rhythm lab as `nutation_longchain_patch_v1_smoke1_metrics.csv`.
- Smoke1 result: 4/4 traced solved, 4/4 `maze_long_chain`, 4/4 `patch_chain`/`long_patch_chain`, 3 TraceOrderKeep + 1 Reject. Source straightness `0.288-0.316`, avgChain `9.66-11.48`, maxChain `21-22`.
- Boundary: strict-review-ready for LongChain-specific style/language comparison, not general hard-feel proof.

## Nutation Hub/Maze Anti-Collapse Review V1 - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Exporter: `.worktrees/nutation-peel/Tools/Production/Export-NutationAntiCollapseReviewV1.ps1`.
- Wrapper: `.worktrees/nutation-peel/Tools/Production/Invoke-NutationHubMazeAntiCollapseReviewV1.ps1`.
- Ranked CSV: `.worktrees/nutation-peel/.codex-run/nutation_hubmaze_anticollapse_v1_current_ranked.csv`.
- Review rows CSV: `.worktrees/nutation-peel/.codex-run/nutation_hubmaze_anticollapse_v1_current_review_rows.csv`.
- Gap report: `.worktrees/nutation-peel/.codex-run/nutation_hubmaze_anticollapse_v1_current_gap_report.md`.
- Review pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubMazeAntiCollapseReviewPack.asset`; 6 refs and worktree Demo activePack points here.
- Result: 33 Hub/Maze rows ranked from 9 joined CSVs, 6 selected for controlled review by `Style x ChainLanguage`: Hub rail/patch/curve and Maze rail/curve/patch. HubRail `center_rail` is current best Hub row (`score=109.139`); Hub V3 patch remains a near-miss (`score=102.406`); HubCurve best score is `91.497`; MazeRail `core_rail_maze` is current best Maze row (`score=94.748`, only `tier_drop` gap); MazeCurve best score is `77.836`; MazePatchV2 best score is `49.725`.
- Boundary: this is not a production keep pack. Main blockers are collapse, same-axis/same-dir run, local run, directional risk, and `flow_local_collapse`.

## SGP Pressure Hard High-Coverage Probe Baseline - 2026-06-29

- Worktree: `.worktrees/psg-long-lock-role-grammar`; source method `NoMaskProceduralGenerator.BuildSgpPressureHardTrialPack` invoked by `Tools/Production/Invoke-SGPPressureHardProductionV1.ps1`.
- Source report: `.worktrees/psg-long-lock-role-grammar/Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_hard_trial_report.csv`.
- Level root: `.worktrees/psg-long-lock-role-grammar/Assets/ArrowMagic/SOData/Levels/DirectProcedural/SGPPressureHardTrial/`.
- Trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_hard_highcov_probe_v1_metrics.csv`.
- Source result: 4 rows, coverage `0.978-0.994`, chains `58-61`, avgChain `9.57-10.62`, maxChain `21`, portable solved true.
- Trace result: 4/4 solved. Keepable process examples: dense_weave B with avg/max choices `3.78/6`, core_burst B `4.93/8`, lock_buckle A `5.31/8`; section_unlock drops due maxChoices `16`.
- Status: coverage/densify capability baseline only. Do not treat as final long-lock product because the chain language is dense/medium, not `seed_long_lock` long-spine visual.

## PSG Long Lock Longify V1 0.95 Review Pack - 2026-06-29

- Worktree: `.worktrees/psg-long-lock-role-grammar`, branch `codex/psg-long-lock-role-grammar`.
- Pack: `.worktrees/psg-long-lock-role-grammar/Assets/ArrowMagic/SOData/Packs/DirectProcedural/PSGLongLockLongifyV1Pack.asset`.
- Level root: `.worktrees/psg-long-lock-role-grammar/Assets/ArrowMagic/SOData/Levels/DirectProcedural/PSGLongLockLongifyV1/`.
- Source report: `.worktrees/psg-long-lock-role-grammar/Assets/ArrowMagic/SOData/Reports/DirectProcedural/psg_long_lock_longify_v1_report.csv`; summary same folder `_summary.md`.
- Trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/psg_long_lock_longify_v1_trace_metrics.csv`.
- Joined summary: `.worktrees/psg-long-lock-role-grammar/.codex-run/psg_long_lock_longify_v1_trace_joined_summary.md`; production keep: `.worktrees/psg-long-lock-role-grammar/.codex-run/psg_long_lock_longify_v1_production_keep.csv`.
- Source result: 4/4 selected, coverage `0.9778-0.9951`, avg `0.9894`, chains `42`, maxChain `52-75`, longVisualCellShare avg `0.488`, midShortSupportChains avg `28.00`.
- Trace/join result: 4/4 solved, `TraceOrderKeep=2 / VisualKeep=1 / Reject=1`; two keep rows are core_burst (`coverage=0.992`, `maxChain=75`, A, avg/max choices `4.93/9`) and lock_buckle (`coverage=0.993`, `maxChain=52`, A, avg/max choices `5.24/10`).
- Demo in worktree points `activePack` to `PSGLongLockLongifyV1Pack.asset` for visual review.

## PSG Long Lock Longify V1 Production Keep Pack - 2026-06-29

- Worktree: `.worktrees/psg-long-lock-role-grammar`, branch `codex/psg-long-lock-role-grammar`.
- Full generated pack: `.worktrees/psg-long-lock-role-grammar/Assets/ArrowMagic/SOData/Packs/DirectProcedural/PSGLongLockLongifyV1Pack.asset`.
- Production keep pack: `.worktrees/psg-long-lock-role-grammar/Assets/ArrowMagic/SOData/Packs/DirectProcedural/PSGLongLockLongifyV1ProductionKeepPack.asset`; 32 refs, pack id `psg_long_lock_longify_v1_production_keep`.
- Level root: `.worktrees/psg-long-lock-role-grammar/Assets/ArrowMagic/SOData/Levels/DirectProcedural/PSGLongLockLongifyV1/`.
- Source report: `.worktrees/psg-long-lock-role-grammar/Assets/ArrowMagic/SOData/Reports/DirectProcedural/psg_long_lock_longify_v1_report.csv`; 48/48 selected, coverage `0.9778-0.9951`, avg `0.9894`.
- Official trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/psg_long_lock_longify_v1_full_split_trace_metrics.csv`; 48/48 solved.
- Strict joined summary: `.worktrees/psg-long-lock-role-grammar/.codex-run/psg_long_lock_longify_v1_full_split_trace_joined_summary.md`; production keep CSV: `.worktrees/psg-long-lock-role-grammar/.codex-run/psg_long_lock_longify_v1_full_split_trace_production_keep.csv`.
- Keep mix: `core_burst=12`, `lock_buckle=12`, `section_unlock=6`, `dense_weave=2`; dense rows require `runbreak` split and keep generated greedy axis/dir runs at `6/6`.
- Worktree `Demo.unity` activePack points to `PSGLongLockLongifyV1ProductionKeepPack.asset` for visual review.

## Generated-Root WBP V12 t11 Core-Guard Root - 2026-06-29

- Worktree: `.worktrees/sgp-rhythm-lab`.
- Level asset root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t11_coreguard/`.
- Root pool: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t11_coreguard_root_pool.csv`.
- Growth log / summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t11_coreguard_growth_log.csv`, `t11_coreguard_summary.md`.
- Strict summaries: `t11_coreguard_corepreflight_summary.csv` and `t11_coreguard_strict_summary.csv`.
- Frontier diagnostic summary: `t11_coreguard_frontierdiag_summary.csv`; detailed probe rows: `t11_coreguard_frontierdiag_01_t11_coreguard_c001.csv`.
- Source-basin audit: `t11_coreguard_corepreflight_source_basin_audit.csv` and `_summary.md`.
- Result: `t11_coreguard_c001` is authored-clean and Greedy solved at coverage `0.313765`; final planned slot core preflight is `4/4`. Corepreflight duty/ray gate reaches chain `7` / reserve `6` and source-basin audit passes; full strict still drops to chain `1`, so this is a frontier-profile blocker diagnostic, not a final candidate.

## Generated-Root WBP V12 t12 Frontier/Cross-Frontier Diagnostics - 2026-06-29

- Worktree: `.worktrees/sgp-rhythm-lab`.
- Level roots kept for review: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t12_frontierquad_seedscan1/`, `t12_frontiertriple_guard1/`, and failed generator smoke `t12_crossfrontier_slot5/`.
- Direct-child frontier reports: `t12_frontierquad_seedscan1_root_pool.csv`, `t12_frontierquad_seedscan1_strict_summary.csv`, and `t12_frontierquad_seedscan1_strict_source_basin_audit_summary.md`.
- Profile probes: `t12_crosstopq1_quad_frontier_allbasin_probe.csv`, `t12_crosstopq1_quad_fullstrict_allbasin_probe.csv`, `t12_crosstopq1_quad_nofrontier_allbasin_probe.csv`, and `t12_crosstopq1_quad_crossfrontier_allbasin_probe.csv`.
- Cross-frontier wrapper/audit: `t12_crossfrontier_slot5_gate_summary.csv` and `t12_crossfrontier_slot5_gate_source_basin_audit_summary.md`.
- Result: direct-child strict frontier can find local edges but fails source-basin audit with 0 cross-top-root edges. Cross-frontier profile passes the chain audit with disjoint 7, 3 activation top roots, and 4 cross-top-root edges. Generator `t12_crossfrontier_slot5` still selected 0, so this is a gate/profile proof and next-step blocker, not a final whole-board candidate.

## Generated-Root WBP V12 t13 Cross-Frontier Roots And Duty-Seed Smoke - 2026-06-29

- Worktree: `.worktrees/sgp-rhythm-lab`.
- Guarded generated roots: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t13_crossfrontier_chain_guard2/` and `t13_crossfrontier_chain_guard3_cov32/`.
- Key root: `t13_crossfrontier_chain_guard3_cov32_c001`, coverage `0.3259109`, 22 root chains, authored-clean, Greedy solved, final strict slot audit `5/0`, external chain source-basin audit pass.
- Duty-seed whole-board candidates: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t13_crossfrontier_cov32_dutyseed4_t1/`.
- Duty-seed 4-chain result: 4 root-preserved candidates at coverage `0.3765182`, 26 total chains, 4 added state-frontier contracts, official trace 4/4 solved and process A, but all `LocalEasy` due no support carrier / shallow closure.
- Duty-seed 5/6-chain result: no candidates, despite 9 exact disjoint seed capacity; final bundle layer fails by `greedy_unsolved`, added first-hit mismatch, overlap, and same-edge interaction.
- Status: mechanism proof plus hardness-collapse diagnostic only. Do not promote to pack or use as final baseline; next candidate should add support-closure/anti-local gates before raising chain count or coverage.

## Generated-Root WBP V12 t14 Closure-Core Extension Reports - 2026-06-29

- Worktree: `.worktrees/sgp-rhythm-lab`.
- Candidate level roots: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t14_crossfrontier_cov32_closurebias4_t1/`, `t14_crossfrontier_cov32_closurebias5_wide2_t1/`, `t14_crossfrontier_cov32_closurecore4_ext1_t1/`, and `t14_crossfrontier_cov32_closurecore4_ext2_t1/`.
- Report prefixes: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t14_crossfrontier_cov32_closurebias4_t1_*`, `...closurebias5_wide2_t1_*`, `...closurecore4_ext1_t1_*`, and `...closurecore4_ext2_t1_*`.
- Positive current sample: `t14_crossfrontier_cov32_closurecore4_ext2_t1` has coverage `0.427-0.429`, 28 total chains, 6 added semantic chains, official trace `4/4` solved, process S/tight A, supportDepth `2`, antiLocality `0.435`, and hardV3 `0.321`.
- Negative contrast: `t14_crossfrontier_cov32_closurebias5_wide2_t1` proves 5 frontier chains can be cut and official solved, but it drops to `LocalEasy` with supportDepth `0`; do not use pure frontier leaf expansion as the next baseline.
- Status: still below target coverage and HardPotential. It is the current best WBP route shape: closure-positive frontier core plus semantic non-frontier extensions, followed by a required real support-carrier/far-CUD improvement.

## Generated-Root WBP V12 t21-t24 Secondary Boundary Reports - 2026-06-29

- Worktree: `.worktrees/sgp-rhythm-lab`.
- Strict exact10 2-carrier negative: report prefix `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t16br_t21_seed10_recon2_*`; `summary.md` shows 4 chain-plan seed states accepted after option-id identity/row reconstruction, but 0 candidates due real stateActual/corridor blockers.
- Relaxed exact10 B1 local secondary: report prefix `.../t16br_t22_seed10_sec1_*`; candidates `t16br_t22_seed10_sec1_candidates.csv`, chain plan `_chain_plan.csv`, official trace `_trace_wt_metrics.csv` / `_steps.csv`, relation audit `_relation_audit_summary.md`, difficulty attribution `_difficulty_attribution_summary.md`. Result: coverage `0.4676-0.4696`, official 4/4 solved, process A, MediumStructure, but only 1 real added support carrier; B1->B1 secondary is weak/local.
- No-B1 exact10 secondary: report prefix `.../t16br_t23_seed10_no_b1sec_*`; candidates/chain plan/trace/relation audit/difficulty attribution all present. Result: coverage `0.4696356`, official 4/4 solved, process S/tight A, hardV21 `0.724`, same-axis/same-dir run `4`, but still MediumStructure with 1 support carrier; B1->CHOKE improves rhythm but is not a second hub.
- No-B1 exact11 boundary: report prefix `.../t16br_t24_seed11_no_b1sec_*`; no candidates. Summary/compatibility show nonlocal secondary capacity exhausted by occupied root/selected cells, no-path, duplicate-target, and target-not-blocked reasons.
- Status: these are research reports, not Demo-mounted production levels. They establish the current generated-root WBP scaling boundary around exact10 on `t13_crossfrontier_chain_guard3_cov32_c001`.

## Generated-Root WBP V12 t27-t29 Secondary Demand And Reserved-Root Reports - 2026-06-29

- Worktree: `.worktrees/sgp-rhythm-lab`.
- Demand diagnostic prefix: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t16br_t27_seed11_no_b1sec_demand_reason_*`; key files are `_carrier_profile.csv`, `_demand_analysis.csv`, `_demand_analysis.md`, `_summary.md`, and `_compatibility.csv`.
- Secondary cell demand: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t16br_t27_seed11_no_b1sec_secondary_cell_demand.csv`; roles include `release_corridor_conflict`, `body_corridor_gap`, `secondary_corridor`, and `target_ray_corridor`.
- Reservation fit: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t16br_t27_seed11_no_b1sec_secondary_reservation_fit.csv` and `_secondary_reservation_fit.md`; current t13 root has openDemandShare `0.737481` but occupied demand weight `865`.
- Scratch reserved-root smoke levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t28_secondary_reserved_root_smoke/`; reports `t28_secondary_reserved_root_smoke_root_pool.csv`, `_summary.md`, and `t28_secondary_reserved_root_gate_summary.csv`. Result: Greedy-solved generated roots at coverage `0.342-0.352`, but strict capacity only `0-2`; negative baseline.
- Preserve-nonreserve reserved-root smoke levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t29_secondary_preserve_reserved_root_smoke/`; reports `t29_secondary_preserve_reserved_root_smoke_root_pool.csv`, `_summary.md`, `t29_secondary_preserve_reserved_root_gate_01_t29_secondary_preserve_reserved_root_smoke_c001.csv`, and partial `t29_secondary_preserve_reserved_root_gate_summary.csv`.
- t29 partial result: c001 has root coverage `0.346154`, 23 root chains, strict `bestChainDisjoint=4` on edges `19->11,19->9,19->2,12->7`, but fails capacity/diversity tags. Full four-root gate timed out and was stopped; do not treat t29 as official or baseline.
- Status: these reports establish that secondary corridors must be co-planned before exact10 seed closure. They are not Demo-mounted production levels and have no final official trace/difficulty claim.

## Generated-Root WBP V12 t30-t48 Demand-Scored Root/Basin Reports - 2026-06-29

- Demand-scored t13 exact11/e10 reports: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t16br_t30_seed11_no_b1sec_demandscore_smoke_*`, `t16br_t31_seed10_no_b1sec_demandscore_*`, `t16br_t32_seed11_from_t31_demandscore_*`, `t16br_t33_seed10_demandcap700_probe_*`, `t16br_t34_seed9_from_t14_demandscore_*`, `t16br_t35_seed10_from_t34_demandscore_*`, and `t16br_t36_seed11_from_t35_demandscore_*`.
- t13 boundary probes: `t16br_t37_seed10_from_t34_delay_choke_probe_*`, `t16br_t38_seed10_from_t34_delay_choke_generic_probe_*`, `t16br_t39_seed10_from_t34_secondary_wide_probe_*`, and `t16br_t40_seed10_from_t34_secondary_all_empty_probe_*`. Result: CHOKE is required for exact10, and widened/empty secondary search still uses the same left-edge demand cells.
- Root-fit rankings: `t41_fit_t13_crossfrontier_chain_guard3_cov32_root_pool.*`, `t41_fit_t13_crossfrontier_chain_guard2_root_pool.*`, `t41_fit_t28_secondary_reserved_root_smoke_root_pool.*`, and `t41_fit_t29_secondary_preserve_reserved_root_smoke_root_pool.*`. Use `t29_secondary_preserve_reserved_root_smoke_c004` as a demand-fit probe only.
- t29 WBP probe levels/reports: `t16br_t42_t29c004_wbp_exact4_smoke_*`, `t16br_t43_t29c004_wbp_exact5_smoke_*`, `t16br_t44_t29c004_wbp_exact5_generic_smoke_*`, `t16br_t45_t29c004_wbp_exact6_generic_smoke_*`, `t16br_t46_t29c004_wbp_exact6_lowfrontier_probe_*`, `t16br_t47_t29c004_wbp_exact7_lowfrontier_probe_*`, and `t16br_t48_t29c004_wbp_exact8_lowfrontier_probe_*`.
- Best t29 probe: `t16br_t47_t29c004_wbp_exact7_lowfrontier_probe` writes 4 Greedy-solved exact7 candidates at coverage `0.449-0.451`, maxLen `9`, and `stateDemandOverlapWeight=0`. It is not official/difficulty-approved and stalls at exact8, but it proves the root-basin demand-fit direction is viable.
- Status: current active WBP research branch should build a new generated-root/root-basin pool with t13-like cross-frontier/support capacity plus t29-like demand openness; none of these t30-t48 outputs are production packs.

## Generated-Root WBP V12 t49 Root/Demand Audit Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- Lightpool root trace: `t49b_sbrg_t13_lightpool_root_trace_*`; 6/6 solved, process `4 A + 2 B`, but all `LocalEasy`. Treat as negative/root-quality diagnostic.
- c001 branch: `t49c_c001_wbp_exact4_smoke_*`, `t49d_c001_wbp_exact5_smoke_*`, `t49d2_c001_wbp_exact5_demand0_*`, `t49e_c001_wbp_exact4_demand0_*`, and `t49d_c001_wbp_exact5_nodemand_trace_*`. Result: no-demand exact5 solved/process A/S but LocalEasy and consumes t27 demand cells; demand0 exact4/exact5 has 0 candidates.
- c003 demand0 branch: `t49e_c003_wbp_exact4_demand0_*`, `t49f_c003_wbp_exact5_demand0_*`, `t49g2_c003_wbp_exact6_demand0_depth6_*`, `t49h2_c003_wbp_exact7_demand0_depth7_noseedbonus_*`, and `t49g2_c003_wbp_exact6_demand0_trace_*`. Result: exact6 coverage about `0.324-0.328`, demand overlap 0, official solved/process B, LocalEasy/supportDepth 0; exact7 has 0 candidates.
- c002 demand0 branch: `t49i_c002_wbp_exact4_demand0_*`, `t49j_c002_wbp_exact5_demand0_*`, `t49k_c002_wbp_exact6_demand0_depth6_*`, and `t49j_c002_wbp_exact5_demand0_trace_*`. Result: exact5 coverage about `0.385`, demand overlap 0, official solved/process A, LocalEasy/supportDepth 1; exact6 has 0 candidates.
- Hard-root references to resume from: `grwbp_v8_root_input.csv` includes `geosupply_sched_root10_from_40eb0da7_r1_c038` at coverage `0.615942`, process A, `TrueHardCandidate`, supportDepth 4. `grwbp_v12_rootlang_edgepattern5_plannedaddedhit_t1_*` and `grwbp_v12_rootlang_edgepattern5_plannedaddedhit_t1_trace_full4_metrics.csv` show a hard-root extension to coverage `0.653-0.655`, official A, `MediumStructure`.
- Status: t49 closes the lightpool/demand-only branch. Resume with hard-root pool selection plus demand/reservation/source-basin audit, not with these t49 candidates as production or baseline.

## Generated-Root WBP V12 t50 Hard-Root Demand-Fit Smoke Reports - 2026-06-29

- Root pool selector output: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t50_hardroot_pool_fromtrace.csv` and `_summary.md`; selected 80 official-hard generated roots from trace metrics.
- Reservation fit output: `t50_hardroot_pool_fromtrace_reservation_fit.csv`, `_reservation_fit.md`, and selected top12 `t50_hardroot_pool_fromtrace_fit_top12_root.csv`.
- Partial strict gate output: `t50_hardroot_fit_strict_t1_summary.csv`, `t50_hardroot_fit_strict_t1_01_root154_core_sched0564_v1_r1_c016.csv`, and `t50_hardroot_fit_strict_t1_02_root154_section_sched_v2_r5_c062.csv`. The top12 wrapper timed out on root 3 and the residual python probe was stopped.
- Best partial root: `root154_section_sched_v2_r5_c062`, coverage `0.546559`, process S, `TrueHardCandidate`, hardV3 `0.783`, supportDepth `4`, bestChainDisjoint `7`, reserve `6`; quality gate still fails `capacity_lt_8;low_root_footprint;early_b1_cluster`.
- Status: t50 is a positive route smoke, not a baseline. It should feed the next root selector/audit iteration, where hard identity, demand fit, root footprint, and strict semantic capacity are ranked together.

## Generated-Root WBP V12 t51-t52 Short Semantic Whole-Board Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- Duty/ray probe reports: `t51_highfoot_strict_root154_core0589_c043.csv`, `t51_highfoot_strict_geosupply40eb_c038.csv`, `t51_highfoot_strict_rootlang0615_c024.csv`, `t51_crossfrontier_strict_root154_section_r5_c062.csv`, and key positive `t52_shortsemantic_strict_root154_section_r5_c062.csv`.
- Key t52 probe result: `t52_shortsemantic_strict_root154_section_r5_c062.csv` reaches `chainQualityDisjoint=8` with short semantic chains (`min-chain-length=3`) and relation mix across B1/B2/CHOKE; it is a duty candidate pool, not a final level pack.
- Exact8 WBP levels: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t52c062_e8d/`; reports `t52c062_e8d_candidates.csv`, `_chain_plan.csv`, `_planned_relations.csv`, `_summary.md`, `_trace_wt_metrics.csv`, `_trace_wt_steps.csv`, `_relation_audit_*`, and `_difficulty_attribution_*`.
- Exact8 result: 4 candidates, coverage `0.6275304`, rootPreserved `True`, chainLegalityOk `True`, 8 added short/medium semantic chains, Greedy solved, official trace `4/4` solved with process/tight `S/S`, but all `LocalEasy` with hardV3 `0.356` due `antiLocal<0.55` / `local_penalty_dense`.
- Negative controls: `t52c062_e8q_*` forces the theoretical t52 chain-quality edge pattern and gets 0 candidates (`disjoint_lookahead_dead` under slot-aware placement); `t52c062_e9x_*` adds one non-frontier extension and traces solved/process A but worsens hardV3 to `0.351`; `t52c062_e10y_*` forms exact9 final states but no exact10 candidate.
- Status: mechanism proof and hardness-collapse diagnostic only. Resume from these reports when implementing anti-local/support-preserving whole-board scoring or new root/basin co-planning; do not use e9x/generic extension as coverage-scaling baseline.

## Generated-Root WBP V12 t53-t54 Gap-Aware Root Capacity Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- Anti-local controls for c062: `t53c062_e8local0_actual1_*`, `t53c062_e8local0_only_*`, `t53c062_e8antilocal_score1_*`, `t53c062_e8antilocal_mild1_*`, and `t53c062_e8antilocal_option1_*`. Only option-level mild scoring writes candidates, and it reproduces the same chain set as `t52c062_e8d`.
- Root probe reports: `t53_shortsemantic_strict_root154_core_sched0564_v1_r1_c016.csv`, `t53_shortsemantic_strict_geosupply_oh_root154_from055_section_c008.csv`, `t53_shortsemantic_strict_root154_core_sched0589_v1_r3_c043.csv`, `t53_shortsemantic_strict_root154_core_sched0589_v1_r3_c038.csv`, `t53_shortsemantic_strict_root154_section_sched_v2_r5_c056.csv`, `t53_shortsemantic_strict_root154_section_sched_v1_r3_c073.csv`, `t53_shortsemantic_strict_geosupply_sched_root10_from_40eb0da7_r1_c038.csv`, `t53_shortsemantic_strict_rootlang_root10_0615_section_short_r1_c024.csv`, and other nearby t53 strict probe CSVs.
- Best exact8 WBP so far: `t53c016_e8d_*` from root `root154_core_sched0564_v1_r1_c016`; level dir `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t53c016_e8d/`; reports include `_candidates.csv`, `_chain_plan.csv`, `_trace_wt_metrics.csv`, `_relation_audit_*`, and `_difficulty_attribution_*`. Result: coverage `0.6518219`, official solved `4/4`, class `MediumStructure 0.476`, still `local_penalty_dense`.
- Other exact8 WBP: `t53oh008_e8d_*` from root `geosupply_oh_root154_from055_section_c008`; official solved but weaker (`MediumStructure 0.359`, antiLocal `0.405`). `t53c016_e9x_*` exact9 has 0 candidates; `t53c016_e8local0_*` has 0 candidates.
- Gap-aware probe reports: `t54_shortsemantic_gap2all_*` and `t54_shortsemantic_gap3all_*`. Key result: with both root/closure and source-owner min step gap set to 2, tested exact8 roots drop to at most 6 disjoint; with gap 3 they drop to at most 4. Use these as evidence that nonlocal capacity must be generated/selected before WBP chain cutting.

## Generated-Root WBP V12 t55-t56 Gap2 Source-Basin Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- t55 gap-aware root audit reports: `t55_gapaware_smoke_gap2_top2.*`, `t55_gapaware_gap2_top32.*`, `t55_gapaware_gap2_rank33_80.*`, and combined `t55_gapaware_gap2_top80.csv`. Result: `t50_hardroot_pool_fromtrace` top80 has no gap2 capacity `>=8`; best capacity is `6`.
- t56 source-basin rootgen low-coverage cap8 assets: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t56_sbrg_gap2_cap8_smoke1/`; root reports `t56_sbrg_gap2_cap8_smoke1_root_pool.csv`, `_growth_log.csv`, `_summary.md`, and `_basin_plan.csv`.
- Selected generated roots: `t56_sbrg_gap2_cap8_smoke1_c001` coverage `0.2692308`, and `t56_sbrg_gap2_cap8_smoke1_c002` coverage `0.2834008`; both are mechanics-clean roots with gap2 `strictCuttableProxy=8`, but not official-hard baselines.
- Independent gap2 duty probes: `t56_shortsemantic_gap2_t56_sbrg_gap2_cap8_smoke1_c001.csv` and `t56_shortsemantic_gap2_t56_sbrg_gap2_cap8_smoke1_c002.csv`; c002 reaches `chainDisjoint=8 / chainQuality=8`, c001 reaches `7 / 8`.
- WBP exact8 negative: `t56c002_e8gap2_*` has 0 candidates. Summary shows depth7 can form, but final exact8 fails from slot-state conflicts (`cell_overlap`, `same_edge`, `slot_offset_mismatch`, `first_hit_owner_mismatch_added`).
- WBP exact7 diagnostic: level dir `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t56c002_e7gap2/`; reports include `t56c002_e7gap2_candidates.csv`, `_chain_plan.csv`, `_trace_wt_metrics.csv`, `_trace_wt_steps.csv`, `_relation_audit_*`, and `_difficulty_attribution_*`.
- Exact7 result: 4 candidates, coverage `0.3765182`, official `4/4` solved, process/tight `A/A`, but all `LocalEasy 0.168`; difficulty attribution reports added support `0`, local penalty `3`, and risk `local_penalty_dense;no_added_support_carrier`. Use this as a negative for solved/process-A weak-dependency plans.

## Generated-Root WBP V12 t57 Reserve-Aware Source-Basin Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- Strict growth guard negative: `t57_sbrg_gap2_slot8_smoke1_*`; early strict slot growth guard selected 0 and starved coverage, so do not use early strict guard as default.
- Late reserve slot audit: `t57_sbrg_gap2_slot8_late2_root_pool.csv`, `_growth_log.csv`, `_summary.md`, `_basin_plan.csv`; selected 0. It proves t56 seeds have chain-level cap8 but reserve/slot capacity only `4/5`.
- Chain-only false-positive control: `t57_sbrg_gap2_slot8_chainset2_root_pool.csv`, `_growth_log.csv`, `_summary.md`, `_basin_plan.csv`; selected `t57_sbrg_gap2_slot8_chainset2_c001` only when slot set is `chain`, with `cuttableSlotPlanned=8` but reserve-quality `5`.
- Reserve gate smoke after script update: `t57_sbrg_gap2_reservegate2_root_pool.csv`, `_growth_log.csv`, `_summary.md`, `_basin_plan.csv`; selected 0 and emits new growth fields `strictCuttableProxySelectedReserveDisjoint` / `strictCuttableProxyReserveQualityDisjoint`. The command hit 120s timeout, so use it as field/gate smoke rather than a full reproducible root scan.
- Status: t57 reports define the next resume point: scan/generated roots must satisfy reserve-aware gap2 cap8 or final strict slot preflight cap8 before WBP exact8+ chain cutting.

## Generated-Root WBP V12 t58-t59 Reserve-First Source-Basin Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- Reserve-first single-seed smoke: `t58_sbrg_gap2_reservefirst_seed560123_rq5_root_pool.csv`, `_growth_log.csv`, `_summary.md`, `_basin_plan.csv`; selected 0. This run verifies the new reserve-first fields/gate shape but does not reproduce the old t56 geometry, so do not treat it as a reserve-aware scan.
- Current-code scan: `t59_sbrg_gap2_reservefirst_scan3_root_pool.csv`, `_growth_log.csv`, `_summary.md`, `_basin_plan.csv`; generated levels under `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t59_sbrg_gap2_reservefirst_scan3/`.
- t59 result: 3 selected mechanics-clean generated roots, coverage `0.2732794-0.3117409`, authored/import OK and Greedy solved, but reserve-first strictCuttable is weak: best row `t59_sbrg_gap2_reservefirst_scan3_c001` has `selectedChain=2`, `selectedReserve=2`, `chainQuality=2`, `reserveQuality=2`, `crossTopRootEdges=0`, activation dominance `1.000`; other rows are `0/0`.
- Status: t58/t59 are boundary reports. They show final-only reserve auditing is not enough; next useful assets should come from a source-basin/root generator that plans reserve slots and cell roles before growth/cutting.

## Generated-Root WBP V12 t61-t67 Light-Role Reserve Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`; generated level root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/`.
- Negative probes: `t61_sbrg_gap2_lightrole_seed560120_*` soft penalty only planned 2 and consumed 4; `t62_sbrg_gap2_lightrole_hardprotect_seed560120_*` full reserve hard protect planned 8/consumed 0 but stalled at coverage `0.2226721`; `t63_sbrg_gap2_lightrole_raycore_seed560120_*` ray-only hard protect selected coverage `0.2631579` but reserveQuality `1`.
- Better single-seed probes: `t64_sbrg_gap2_lightrole_rayhard_anchorsoft_seed560120_*`, `t65_sbrg_gap2_lightrole_diverse_seed560120_*`, and `t66_sbrg_gap2_lightrole_diverse_nodirect_seed560120_*`. t65 has lightRole `8/8/0`, activationTopRoots `4`, dominance `0.375`, but direct-child strict reserveQuality `1`; t66 no-direct-child raises selectedChain to `4`, selectedReserve/reserveQuality to `3`, crossTop `3`.
- Main small scan: `t67_sbrg_gap2_lightrole_diverse_nodirect_scan3_root_pool.csv`, `_growth_log.csv`, `_summary.md`, `_basin_plan.csv`; generated levels under `.../GeneratedRootWBPV12/t67_sbrg_gap2_lightrole_diverse_nodirect_scan3/`.
- t67 result: selected 2/3 mechanics-clean generated roots. c001 coverage `0.2732794`, lightRole `8/8/0`, activationTopRoots `4`, reserveQuality `3`; c002 coverage `0.2813765`, lightRole `7/7/0`, activationTopRoots `3`, selectedChain `10` but selectedReserve/reserveQuality `3`. Treat as cell-plan progress, not WBP baseline.
- Status: t67 is the current resume point for light-role source-basin rootgen. Next report should target reserveQuality `>=8` by converting planned role edges into actual candidate-chain slot availability.

## Generated-Root WBP V12 t68-t69 Planned Role Slot Audit Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`; generated level root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/`.
- t68 planned-edge audit scan: `t68_sbrg_gap2_lightrole_slotaudit_scan3_root_pool.csv`, `_growth_log.csv`, `_summary.md`, `_basin_plan.csv`; selected 0 because the stricter reproduction had consumed-cell/coverage misses, but growth log contains the planned-edge audit metrics.
- t68 result from growth log: seeds `560120/560121/560122` have lightRole planned `8/6/7`, while `lightRoleSlotReserveQualityDisjoint` is only `1/3/1`. This is the key evidence that preserved light-role cells are not yet slot-fit role chains.
- t69 relaxed single-seed audit: `t69_sbrg_gap2_lightrole_slotaudit_relaxed_seed560120_root_pool.csv`, `_growth_log.csv`, `_summary.md`, `_basin_plan.csv`; generated level under `.../GeneratedRootWBPV12/t69_sbrg_gap2_lightrole_slotaudit_relaxed_seed560120/`.
- t69 result: coverage `0.2874494`, chains `20`, authored OK, Greedy `7/3.200/7`, lightRole `8/8/1`, but role slot only `1/1/1`; generic strictCuttableReserve remains `3/3`. It is a diagnostic row only, not a WBP baseline.
- Status: resume by making light-role planning slot-fit-aware before growth/cutting. Use `lightRoleSlotPreflightRejectReasons` and `lightRoleSlotGeometryRejectReasons` to target first-hit owner mismatch, release/pre-release blocking, and no-path/occupied-corridor blockers.

## Generated-Root WBP V12 t70-t74 Slot-Fit Selection Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`; generated level root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/`.
- Positive diagnostic: `t70_sbrg_gap2_lightrole_slotfit_seed560120_root_pool.csv`, `_growth_log.csv`, `_summary.md`, `_basin_plan.csv`; generated level under `.../GeneratedRootWBPV12/t70_sbrg_gap2_lightrole_slotfit_seed560120/`. Result: selected 1, coverage `0.2874494`, lightRole `8/8/1`, role slot `3/3/3`, generic strict reserve `3/3`.
- Slot-fit no-cap control: `t74_sbrg_gap2_lightrole_slotfit_nocap_seed560120_root_pool.csv`, `_growth_log.csv`, `_summary.md`, `_basin_plan.csv`; generated level under `.../GeneratedRootWBPV12/t74_sbrg_gap2_lightrole_slotfit_nocap_seed560120/`. Result matches t70, so activation-top cap is not the current bottleneck.
- Early slot-reserve packing control: `t73_sbrg_gap2_lightrole_slotfit_pack_once_seed560120_root_pool.csv`, `_growth_log.csv`, `_summary.md`, `_basin_plan.csv`; generated level under `.../GeneratedRootWBPV12/t73_sbrg_gap2_lightrole_slotfit_pack_once_seed560120/`. It completes faster but only reaches lightRole `2/2/0` and role slot `1/1/1`, so after-seed early lock is too restrictive.
- Timeout negatives: t71 slot reserve cell inclusion and t72 slot-reserve selection timed out; partial `basin_plan` files were removed, so there are no persistent t71/t72 report artifacts to resume from.
- Status: t70/t74 are route diagnostics, not baselines. Resume by exporting/auditing slot-fit edge supply and root-growth blockers, then make growth create more slot-fit edges before attempting WBP exact8+ or coverage scaling.

## Campaign500 Long-Chain Pilot3 V1 Worktree Review - 2026-06-29

- Worktree: `.worktrees/campaign500-longchain-pilot3`, branch `codex/campaign500-longchain-pilot3`.
- Replacement plan: `.codex-run/campaign500_longchain_pilot3_replacement_plan_v1.csv`; sections `3/25/45`, 9 target slots total.
- Full generated pack: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainPilot3V1Pack.asset`; level root `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Levels/Campaign500LongChainPilot3V1/`.
- Source report/summary: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Reports/Campaign500/LongChain/campaign500_longchain_pilot3_v1_report.csv` and `_summary.md`; selected 30/36 review candidates, coverage `0.9504-0.9766`, avg longVisualCellShare `0.626`.
- Full lightweight trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_longchain_pilot3_v1_full4_fast_metrics.csv`; result 30/30 solved, missing/failed `0`.
- Demo9 trace-aware keep CSV/summary: `.worktrees/campaign500-longchain-pilot3/.codex-run/campaign500_longchain_pilot3_v1_demo9_trace_keep.csv` and `_summary.md`; 9/9 solved, one selected level per target order.
- Demo9 review pack: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainPilot3V1Demo9Pack.asset`, GUID `be527d4b7cfa0934aa8dccd1f24a1d55`; worktree `Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.
- Boundary: Demo9 is for visual review of the first 3 campaign sections, not final production keep. Most rows are solved but process tier `Drop` because choice pressure is still high.

## Nutation Hub V3 / Maze V2 Worktree Review - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Hub V3 pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubSpokeV3Pack.asset`; source report `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_spoke_v3_report.csv`; joined `.worktrees/nutation-peel/.codex-run/nutation_hub_spoke_v3_smoke1_trace_joined.csv`.
- Hub V3 result: 7/7 traced solved; rank `VisualKeep=1 / ProcessKeep=6 / Reject=0`; production keep rows 0. Best row `nutation_hub_spoke_v3_07_nutation_hub_spoke_v3_rect_woven_hub_a` has STS `0.805`, collapse `0.320`, local `7`, but fails sameAxis/sameDir/dependency-local strict gates.
- Maze V2 pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationMazePatchV2Pack.asset`; source report `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_maze_patch_v2_report.csv`; joined `.worktrees/nutation-peel/.codex-run/nutation_maze_patch_v2_smoke2_trace_joined.csv`.
- Maze V2 result: 4/4 traced solved; rank `ProcessKeep=1 / Reject=3`; production keep rows 0. Best row `nutation_maze_patch_v2_03_nutation_maze_patch_v2_rect_dense_lattice` has coverage `0.908`, choices `3.82/9`, local `9`, but STS `0.678`, collapse `0.460`, sameAxis `14`, sameDir `11`.
- Updated Hub/Maze review pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubMazeAntiCollapseReviewPack.asset`; mounted in worktree Demo; style-proof/gap review only, not production.

## Nutation Hub V4 Worktree Anti-Axis Prototype - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Hub V4 pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubSpokeV4Pack.asset`; levels `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationHubSpokeV4/`; source report `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_spoke_v4_report.csv`.
- Wrapper: `.worktrees/nutation-peel/Tools/Production/Invoke-NutationHubSpokeProductionV4.ps1`; Unity method `NoMaskProceduralGenerator.BuildNutationHubSpokeV4Pack`.
- Current joined report: `.worktrees/nutation-peel/.codex-run/nutation_hub_spoke_v4_smoke4_trace_joined.csv`; summary `.worktrees/nutation-peel/.codex-run/nutation_hub_spoke_v4_smoke4_trace_joined_summary.md`; production keep CSV has 0 rows.
- Smoke4 result: 4/4 traced solved; rank `VisualKeep=2 / ProcessKeep=1 / Reject=1`; 4/4 `styleFamily=hub_spoke`, 4/4 `chainLanguage=patch_chain`.
- Best row `nutation_hub_spoke_v4_01_nutation_hub_spoke_v4_rect_woven_axis_a`: coverage `0.916`, choices `5.19/10`, local `5`, directionalRisk `0.157`, STS `0.809`, collapse `0.260`, sameDir `6`, dependencyLocal `0.545`, but sameAxisRun `20`; not production-approved.

## Campaign500 Long-Chain Pilot3 V2 Worktree Review - 2026-06-29

- Worktree: `.worktrees/campaign500-longchain-pilot3`, branch `codex/campaign500-longchain-pilot3`.
- Full V2 pack: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainPilot3V2Pack.asset`; level root `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Levels/Campaign500LongChainPilot3V2/`.
- Source report/summary: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Reports/Campaign500/LongChain/campaign500_longchain_pilot3_v2_report.csv` and `_summary.md`; selected `43/54`, coverage avg `0.9631`, maxChain avg `66.19`, maxChain range `34-94`.
- Full trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_longchain_pilot3_v2_full_fast_metrics.csv`; `43/43 solved`, process tiers `A=4/B=2/Drop=37`.
- Demo9 keep CSV/summary: `.worktrees/campaign500-longchain-pilot3/.codex-run/campaign500_longchain_pilot3_v2_demo9_trace_keep.csv` and `_summary.md`; `9/9 solved`, maxChain avg `54.78`, maxChain max `74`, edgeStraightRunMax avg `7.11`.
- Demo9 review pack: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainPilot3V2Demo9Pack.asset`, GUID `6d069bd44eb6790439cb69f27a485c2b`; worktree `Demo.unity` activePack points here.
- Boundary: visual/review only. V2 addresses “do not make chains too long” and “avoid continuous edge straight stacks”; most rows still process `Drop`, so it is not production keep.

## Campaign500 Long-Chain Pilot3 V3 Worktree Review - 2026-06-29

- Worktree: `.worktrees/campaign500-longchain-pilot3`, branch `codex/campaign500-longchain-pilot3`.
- Full V3 pack: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainPilot3V3Pack.asset`; level root `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Levels/Campaign500LongChainPilot3V3/`.
- Source report/summary: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Reports/Campaign500/LongChain/campaign500_longchain_pilot3_v3_report.csv` and `_summary.md`; selected `43/54`, coverage avg `0.9631`, maxChain avg `66.19`, `outerExitRunMax avg/max=1.00/1`, `outerExitSideMax avg/max=1.28/2`.
- Full trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_longchain_pilot3_v3_full_fast_metrics.csv`; `43/43 solved`, process tiers `A=4/B=2/Drop=37`.
- Demo9 keep CSV/summary: `.worktrees/campaign500-longchain-pilot3/.codex-run/campaign500_longchain_pilot3_v3_demo9_trace_keep.csv` and `_summary.md`; `9/9 solved`, maxChain range `34-74`, all rows `outerExitRunMax=1`.
- Demo9 review pack: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainPilot3V3Demo9Pack.asset`; worktree `Demo.unity` activePack points here. Review only: choice pressure remains high on many late/challenge rows.

## Nutation Reader-Rhythm V1 Worktree Review - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Review manifest: `.worktrees/nutation-peel/.codex-run/nutation_reader_rhythm_v1_review_rows.csv`; ranked candidates: `.worktrees/nutation-peel/.codex-run/nutation_reader_rhythm_v1_ranked.csv`; summary: `.worktrees/nutation-peel/.codex-run/nutation_reader_rhythm_v1_summary.md`.
- Review pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationReaderRhythmV1ReviewPack.asset`; pack GUID `24f51485e38d01c4ab417c0477b027e9`; worktree `Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.
- Rows: 10 total, all selected from solved trace rows; LongChain 6 (`NutationLongChainCurveV1` 2, `NutationLongChainSpineV1` 2, `NutationLongChainRailV1` 2) + Peel 4 (`NutationPeelRailV1` 1, `NutationPeelCurveCurrent` 2, `NutationPeelPatchV1` 1).
- Aggregate review metrics: `avgChoices=5.125`, `maxChoicesAvg=8.5`, `localPatchRunAvg=4.5`, `nearOuterRunAvg=3.8`, `stripeRiskAvg=0.085`, `directionalRiskAvg=0.132`; row 10 is a deliberate `Reject/high_risk` patch-chain near-miss for visual review only.
- Boundary: this pack is for user feel-check of "visual read-chain difficulty + rhythm break"; it is not production keep and should not replace `NutationStyleMatrixStrictReviewPack` until user approves the direction.

## Generated-Root WBP V12 t76 Slot-Fit Supply Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`; source roots remain t70/t74 generated roots under `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/`.
- t70 root-row supply audit: `t76_t70_lightrole_slotfit_supply.csv`, `_summary.csv`, `_summary.md`. Result: coverage `0.2874494`, lightRole `8/8/1`, only `3/8` planned edges have slot-fit candidates, planned-edge reserve remains `3/3/3`; successful edges are `8->12`, `3->10`, `12->11`.
- t74 no-cap root-row supply audit: `t76_t74_lightrole_slotfit_supply.csv`, `_summary.csv`, `_summary.md`. Result matches t70, so activation-top cap is not the bottleneck.
- Main blocker evidence: t70 top preflight rejects are `first_hit_owner_mismatch_base`, `blocks_release_owner`, `frontier_target_not_unlocked_after_carrier_clear`, and `first_hit_exits_board`; top blockers are owner12/owner8/owner13/owner3 corridor/head cells. This supports moving slot-fit blocker/corridor pressure earlier than WBP chain cutting.
- t77 blocker-penalty smoke produced only a partial `basin_plan` before timeout; the partial was deleted and should not be used. Next report should use a lighter offline selector or staged growth experiment rather than full rootgen with repeated blocker scans.

## Generated-Root WBP V12 t78-t80 Slot-Fit Feedback Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`; generated level root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/`.
- t78 old-root pressure map: `t78_slotfit_blocker_feedback_edges.csv`, `_cells.csv`, `_owners.csv`, `_details.csv`, `_summary.md`. It coalesces t70+t74 by generation and shows 7 failed planned edges; owner12 pressure `4932`, top cells `8,17 / 7,17 / 8,19 / 6,18`, secondary blockers owner8/13/6/9.
- t79 staged growth with static t78 map: root reports `t79_sbrg_lightrole_blockermap_growth_seed560120_root_pool.csv`, `_growth_log.csv`, `_summary.md`, `_basin_plan.csv`; generated level under `.../GeneratedRootWBPV12/t79_sbrg_lightrole_blockermap_growth_seed560120/`. Result: coverage `0.3016194`, chains `22`, Greedy `8/4.318/8`, lightRole `8/8/0`.
- t79 post-hoc supply audit: `t79_sbrg_lightrole_blockermap_growth_slotfit_supply.csv`, `_summary.csv`, `_summary.md`. It improves supply to `5/8` planned edges with slot-fit candidates and `66` candidates, but role-slot reserve/quality remains `2/2`; activationTopRoots is only `2`.
- t79 new blocker pressure: `t79_slotfit_blocker_feedback_edges.csv`, `_cells.csv`, `_owners.csv`, `_details.csv`, `_summary.md`. Failed edges shrink to 3 and move to owner5 group (`5->1/5->4/5->8`), with owner5 pressure `1766.7` and hotspot `0,15`.
- t80 combined-map + activation cap: root reports `t80_sbrg_lightrole_blockermap_cap_seed560120_root_pool.csv`, `_growth_log.csv`, `_summary.md`, `_basin_plan.csv`; generated level under `.../GeneratedRootWBPV12/t80_sbrg_lightrole_blockermap_cap_seed560120/`. Result: lightRole `8/8/9`, activationTopRoots `4`, dominance `0.250`, Greedy `6/3.350/6`.
- t80 post-hoc supply audit is a negative: `t80_sbrg_lightrole_blockermap_cap_slotfit_supply.csv`, `_summary.csv`, `_summary.md` reports `0/8` slot-fit edges and `0/0` reserve. Top reject is target-not-unlocked/exit/release timing/no-candidate-path, so activation cap alone is not a route.
- Status: t79 is a positive staged-growth signal; t80 is a complete negative control. Resume with a joint planned-edge selector that balances slot supply, reserve packing, activation spread, and first-hit/release preflight before running exact8+ or coverage scaling.

## Tight Choice Bottleneck V1 Worktree Review - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Review manifest: `.worktrees/nutation-peel/.codex-run/tight_choice_bottleneck_v1_review_rows.csv`; ranked candidates: `.worktrees/nutation-peel/.codex-run/tight_choice_bottleneck_v1_ranked.csv`; summary: `.worktrees/nutation-peel/.codex-run/tight_choice_bottleneck_v1_summary.md`.
- Review pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/TightChoiceBottleneckV1ReviewPack.asset`; pack GUID `d9d5bbf2ec25433fbe7985a3238fdb26`; worktree `Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.
- Rows: 10 total, selected from solved trace rows with real choice-control metrics; order is 5 clean LongChain rows (`NutationLongChainRailV1` 4 + `NutationLongChainSpineV1` 1), 4 Hub extreme-tight rows (`NutationHubSpokeV2/V3/V4`), and 1 PSG control row (`SGPPressureHardTrial` lock_buckle).
- Clean LongChain rows have `avgChoices 2.73-3.41`, `maxChoices 5-7`, `lowChoiceRunMax 4-7`, and controlled `local/near/outer` runs; Hub rows are stronger bottleneck probes with `lowChoiceRunMax 4-15` but remain risk-review rows because same-axis/same-dir/local-collapse can still be visible.
- Boundary: review-only pack for the user's “continuous 1-2 choices” feel-check; not production approval and not a new generator core yet.

## Tight Choice Bottleneck V1 Correction - 2026-06-29

- Correction: the LongChain rows above are no longer valid as general difficulty evidence. Their low-choice curve can be caused by chain length itself, so they may only be used for LongChain-specific comparison/control.
- Corrected manifest and summary remain at `.worktrees/nutation-peel/.codex-run/tight_choice_bottleneck_v1_review_rows.csv` and `_summary.md`; summary must show `long-chain included as general evidence: False`.
- Corrected pack remains `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/TightChoiceBottleneckV1ReviewPack.asset`, GUID `d9d5bbf2ec25433fbe7985a3238fdb26`, display name `Tight Choice Bottleneck V1 Review (10, Non-LongChain)`.
- Corrected rows are Hub/Maze diagnostic probes plus 1 PSG control. Several rows remain process `Drop` or carry local/same-axis risk, so this is a feel-check pack only and does not prove a production-ready general hard route.
- Current open `.worktrees/nutation-peel/Assets/ArrowMagic/Scenes/Demo.unity` is dirty and activePack points to `NutationHubRailV1Pack` GUID `da5940226b5d6914e83642b69162d3cd`; verify or remount before manual review.

## No-Long-Chain Causal Hardlock5 V1 Calibration - 2026-06-29

- Worktree: `.worktrees/sgp-rhythm-lab`.
- Review pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_NoLongChainCausalHardlock5V1Pack.asset`; GUID `b68b791db5d34c1bad43700d61c90ceb`; kept as calibration, no longer the current Demo mount.
- Level root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/NoLongChainCausalHardlock5V1/`; restored from `_AssetArchive/20260624_assetdatabase_trim/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/CausalHardlockDiverseReviewV1Cov28Frozen/`.
- Report: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/no_long_chain_causal_hardlock5_v1_selected.csv`; summary `no_long_chain_causal_hardlock5_v1_summary.md`.
- Metrics: 5 rows, `34` chains, `229-232` arrows, `maxChain=9-15`, `singleClearShare=0.039-0.065`, `avgChoices=2.79-3.12`, `maxChoices=5-6`, `localPatchSolveRunMax=1-2`, `nearOuterPatchSolveRunMax=0-1`.
- Use when checking whether short/mid carrier causal locks create real difficulty after rejecting LongChain low-choice false positives. Review-only, not production approval.

## Skeleton Gate V1 DenseDep Negative Reference - 2026-06-29

- Worktree: `.worktrees/sgp-rhythm-lab`.
- Negative reference pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_PressureReadStageLockSkeletonGateV1DenseDepReview2Pack.asset`; GUID `bf0bd14c0af14f15be0f52ea999855d0`; current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack at time of rejection.
- Level root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/PTDASkeletonGateV1DenseDepReview2/`; restored from `_AssetArchive/20260624_assetdatabase_trim`.
- Reports: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/pressure_read_stage_lock_skeleton_gate_v1_dense_dep_review2_frozen_trace_metrics.csv`, `pressure_read_stage_lock_skeleton_gate_v1_dense_dep_review2_input.csv`, and `hard_lane_skeleton_gate_v1_dense_dep_review2_notes_20260621.md`.
- Metrics: 2/2 solved, process `S/S`, 15x24 and 15x25, `24` chains each, prior notes coverage about `0.805-0.822`, `avgChoices=2.12/3.00`, `maxChoices=5/4`, `stageLockScore=0.927/0.767`.
- User rejection: this pack does not match the user's prior skeleton concept and feels like a long chain being blocked once. Do not use it as proof that skeletons can become complete hard levels; keep only as a negative example for misleading "skeleton" labels and long-chain block difficulty.

## V1.31 Extended Balanced Hardest Focus10 - 2026-06-29

- Worktree: `.worktrees/sgp-rhythm-lab`.
- Source pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV131ExtendedBalancedReviewPack.asset`; GUID `91a29088725441d3b604fa2e66f8d71e`; 108 refs from 6 families x 18.
- Focus pack: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_RootVariantLibraryV131HardestFocus10Pack.asset`; GUID `a9f34884d0a54b4ca10e3b42fa8aa131`; current `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` activePack on disk.
- Focus manifest: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/v131_extended_balanced_hardest_focus10_20260629.csv`; summary: `v131_extended_balanced_hardest_focus10_20260629_summary.md`.
- Static audit for all 108: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/v131_extended_balanced_review108_static_hardness_audit_20260629.csv`.
- Top24 trace: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/v131_extended_balanced_review108_top24_lighttrace_20260629_metrics.csv`; summary: `v131_extended_balanced_review108_top24_lighttrace_20260629_summary.md`.
- Shortlist ranks: `85, 88, 90, 106, 107, 103, 74, 86, 67, 70`; selected to mix short-chain low-choice, high-structure Web/FourFam delayed dependency, and short-chain stage-lock probes.
- Boundary: review-only. The selected rows avoid long-chain false positives (`maxChain=9-15`, `singleClearShare=0.0456-0.0657`) but still require manual feel review.

## Generated-Root WBP V12 t81-t86/t84/t85/t86 Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`; generated level root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/`.
- t81 staged selector over t79: `t81_offline_joint_t79_root_pool.csv`, `t81_offline_joint_t79_edges.csv`, `t81_offline_joint_t79_summary.md`; audit reports `t81_offline_joint_t79_slotfit_supply.csv`, `_summary.csv`, `_summary.md`. Result: selected 5 planned edges, `2/5` slot-fit edges, roleSlot reserve `2/2`.
- t81 feedback from patched t79: `t81_offline_joint_t79_slotfit_edge_pressure.csv`, `_cell_pressure.csv`, `_owner_pressure.csv`, `_blocker_detail.csv`, `_blocker_feedback_summary.md`. Top blocked edge is `5->1`; owner5/cell `0,15` dominates.
- t81 semantic cell plan artifact: `t81_offline_joint_t79_semantic_cell_plan.csv`, `t81_offline_joint_t79_semantic_edge_plan.csv`, `t81_offline_joint_t79_semantic_cell_plan_summary.md`. It records board `19x26`, rootCells `149`, selectedEdges `5`, slotFitSelectedEdges `2`, plannedNonRootCells `72`, rootPlanConflicts `0`.
- t82 owner5-feedback rootgen: root reports `t82_sbrg_lightrole_owner5_feedback_seed560120_root_pool.csv`, `_growth_log.csv`, `_summary.md`, `_basin_plan.csv`; level under `.../GeneratedRootWBPV12/t82_sbrg_lightrole_owner5_feedback_seed560120/`. Result: coverage `0.3178138`, chains `22`, Greedy `7/4.136/7`, lightRole `8/8/0`; supply audit only `3/8`, reserve `2/2`.
- t82 offline selector/audit: `t82_offline_joint_root_pool.csv`, `t82_offline_joint_edges.csv`, `t82_offline_joint_summary.md`, plus `t82_offline_joint_slotfit_supply.csv/_summary.*`. It selects 7 edges but still only `2/7` slot-fit edges, so it is a negative for diversity-without-supply.
- t83 multi-map hard feedback rootgen reports: `t83_sbrg_lightrole_multimap_feedback_seed560120_root_pool.csv`, `_growth_log.csv`, `_summary.md`, `_basin_plan.csv`; no selected roots because lightRole only reached `7/8`.
- t84 soft multi-map rootgen reports: `t84_sbrg_lightrole_multimap_soft_seed560120_root_pool.csv`, `_growth_log.csv`, `_summary.md`, `_basin_plan.csv`; level under `.../GeneratedRootWBPV12/t84_sbrg_lightrole_multimap_soft_seed560120/`. Result: coverage `0.3016194`, lightRole `8/8/12`, but supply audit `1/8`, reserve `1/1`. Feedback files use prefix `t84_sbrg_lightrole_multimap_soft_seed560120_slotfit_*`.
- t85/t86 hard reserve negatives: `t85_sbrg_lightrole_hardreserve_seed560120_*` and `t86_sbrg_lightrole_hardray_seed560120_*` reports. Full reserve hard-exclude stalls around coverage `0.208`; ray-only hard-exclude reaches about `0.287` but does not pass the 8-edge gate. Keep as negative evidence, not baselines.
- t87 low-budget live slot-fit scan timed out and its partial reports were deleted. Do not resume from t87 artifacts.

## Generated-Root WBP V12 t88 Semantic Slot Preplan Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`.
- t81 preplan outputs: `t81_offline_joint_t79_semantic_slot_preplan_cells.csv`, `_edges.csv`, `_constraints.csv`, `_summary.md`. Result: selectedEdges `5`, slotReady `2`, unmet `3`, constraints `72`, demandRootConflicts `6`; top demand owners are owner5/owner2, top cells `0,15`, `1,15`, `1,14`, `0,23-25`.
- t84 negative preplan outputs: `t84_offline_joint_semantic_slot_preplan_cells.csv`, `_edges.csv`, `_constraints.csv`, `_summary.md`. Result: selectedEdges `8`, slotReady `1`, unmet `7`, constraints `70`, demandRootConflicts `13`; demand spreads across owner9/21/7/1/11/8.
- Comparison summary: `t88_semantic_slot_preplan_compare_summary.md`. It records t81 as the stronger staged base because unmet constraints are concentrated and two cross-basin contracts are slot-ready; t84 confirms that pressure-map avoidance without slot creation broadens conflicts.
- Use these files as the next root-growth/cutter input: preserve slot-ready reserve/chain cells, and avoid or relocate top demand blocker cells before adding more semantic chains. They are not final levels and do not claim coverage/difficulty success.

## Generated-Root WBP V12 t89 Semantic Preplan Consumer Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`; generated level root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t89_sbrg_semantic_preplan_seed560120/`.
- Rootgen outputs: `t89_sbrg_semantic_preplan_seed560120_root_pool.csv`, `_basin_plan.csv`, `_growth_log.csv`, `_summary.md`; selected level `t89_sbrg_semantic_preplan_seed560120_c001` has coverage `0.3036437`, chains `21`, topRoots/openers `8/8`, lightRole `8/8/0`, Greedy `8/4.143/8`.
- Supply audit: `t89_sbrg_semantic_preplan_seed560120_slotfit_supply.csv`, `_summary.csv`, `_summary.md`; result `slotFit edges 3/8`, candidate count `36`, role-slot reserve/quality `3/3/3`.
- Offline 8-edge selector outputs: `t89_offline_joint_root_pool.csv`, `t89_offline_joint_edges.csv`, `t89_offline_joint_summary.md`; patched audit files `t89_offline_joint_slotfit_supply.csv`, `_summary.csv`, `_summary.md` still report `3/8` selected slot-ready edges and `3/3/3` reserve.
- Semantic preplan outputs: `t89_offline_joint_semantic_slot_preplan_cells.csv`, `_edges.csv`, `_constraints.csv`, `_summary.md`; result `selected=8`, `slotReady=3`, `unmet=5`, `constraints=131`, `demandRootConflicts=16`.
- Feedback outputs: `t89_offline_joint_slotfit_edge_pressure.csv`, `_cell_pressure.csv`, `_owner_pressure.csv`, `_blocker_detail.csv`, `_blocker_feedback_summary.md`; top blocked edge is `10->17` with slot `0` and pressure `540.6`.
- Status: diagnostic positive, not final candidate. Use for next rootgen/preplan pass aimed at increasing ready semantic contracts before high-coverage cutting.

## Generated-Root WBP V12 t90-t97 Repair / Slot Capacity Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`; generated negative root dirs include `.../GeneratedRootWBPV12/t90_sbrg_repair_preplan_seed560120/` and `t97_sbrg_t92_repair_soft_seed560120/`.
- t90 repair classifier outputs: `t90_semantic_repair_plan_edges.csv`, `_owners.csv`, `_cells.csv`, `_constraints.csv`, `_summary.md`. It classified four `10->*` edges as one shared activation corridor bundle and `13->1` as first-hit/exit timing. Direct t90 rootgen with these constraints was negative: coverage `0.2854251`, topRoots/openers `6/6`, supply `3/8`, reserve `2/2`.
- t91/t92 repair-aware selector outputs: `t91_repair_policy_offline_*`, `t91_repair_policy_semantic_slot_preplan_*`, `t92_semantic_repair_plan_*`, and `t92_drop_redundant_*`. Key results: t91 `3/5` ready, t92 detected `13->1` redundant with ready `13->15`, and t92 clean core is `3/4` ready with reserve `3/3` and demandRootConflicts `8`.
- t93 wide recompute supply outputs: `t93_recompute_from_t92_root_slotfit_supply.csv/_summary.*`. Recompute mode scanned `80` edge rows with `18` slot-fit edges and `100` candidates. Repair-aware wide selector `t93_wide_repair_policy_*` reached `4/8` slot-ready, role-slot reserve `4/4`, and semantic preplan `4 ready / 4 unmet`.
- t94 repair/wide selector outputs: `t94_semantic_repair_plan_*` and `t94_wide_repair_policy_*`. It identifies a new owner20 shared bundle; selector remains at `4/8` slot-ready but improves activationTopRoots to `6` and dominance to `0.250`.
- t96 candidate-variant supply/capacity outputs: `t96_recompute_variant_from_t92_root_slotfit_supply.csv/_summary.*`, `t96_bestrow_slot_capacity_*`, and `t96_variant_slot_capacity_*`. Variant audit sees `100` slot variants over `18` slot edges but max disjoint slot capacity is still `4`, selected edges `13->15,0->13,14->8,20->19`; top overlap cells are `8,12`, `9,12`, `7,12`, `6,12`, `5,12`, `8,16`.
- Status: best current generated root is still diagnostic only. Current blocker is proven slot-capacity ceiling `4`, so the next rootgen pass must explicitly create at least five independent slot/corridor bands before selector/preplan can pass the `5-6/8` feasibility gate.

## Generated-Root WBP V12 t98 Slot-Capacity Layout Breakthrough Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`; generated level root for positive smoke: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t98c_sbrg_slot_capacity_layout_seed560120/`.
- Layout outputs: `t98_slot_capacity_layout.csv` and `_summary.md`; inputs were t96 variant capacity selected/overlap CSV plus the t92/t89 root row. The layout preserves 60 existing selected reserve cells, avoids 37 overlap cells, and targets 3 new vertical open bands.
- Negative smoke outputs: `t98b_sbrg_slot_capacity_layout_seed560120_*`, `t98b_recompute_variant_slotfit_supply*`, and `t98b_variant_slot_capacity_*`. It hard-excluded old selected reserves plus new bands and dropped to coverage `0.2449393`, supply `9/76`, max disjoint `3`; use only as a boundary reference.
- Positive rootgen outputs: `t98c_sbrg_slot_capacity_layout_seed560120_root_pool.csv`, `_basin_plan.csv`, `_growth_log.csv`, `_summary.md`. Selected root `t98c_sbrg_slot_capacity_layout_seed560120_c001` has coverage `0.3056680`, 23 chains, topRoots/openers `7/7`, authored OK, Greedy `7/5.174/7`, lightRole `8/8/9`.
- Positive capacity outputs: `t98c_recompute_variant_slotfit_supply.csv/_summary.*` and `t98c_variant_slot_capacity_*`. Variant audit reports `112` variants, `19` slot edges, and max disjoint slot capacity `5` with selected edges `6->10,16->18,17->7,2->15,13->5`.
- Variant-aware selector outputs: `t98c_variant_slot_core5_*` reaches `5/5` slot core; `t98c_variant_joint8_*` reaches `selected=8`, `slotEdges=5`, `activationTopRoots=5`. Non-variant `t98c_joint_edges*` remains `4/8`, so use variant-aware selector for this line.
- Semantic preplan/repair outputs: `t98c_variant_joint8_semantic_slot_preplan_*` reports `selected=8`, `slotReady=5`, `unmet=3`, `constraints=141`, `demandRootConflicts=8`; `t98c_semantic_repair_plan_*` classifies the 3 unmet owner19 tail edges as `root_flex_relocate_blockers` / `repair_edge` and emits only preserve constraints.
- Status: diagnostic breakthrough, not final level. Next useful step is t99: either variant-aware replacement/defer for owner19 tail edges or another slot-capacity layout/rootgen pass targeting `6/8` before high-coverage chain cutting.

## Nutation Hub Dense AntiDir V1 Review - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Review pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubDenseAntiDirV1ReviewPack.asset`; GUID `40d17a2c8ce14dbdbdbf7f7b03a03848`; displayName `Nutation Hub Dense AntiDir V1 Review (10)`.
- Review manifest: `.worktrees/nutation-peel/.codex-run/nutation_hub_dense_antidir_v1_review_rows.csv`; ranked candidates: `.worktrees/nutation-peel/.codex-run/nutation_hub_dense_antidir_v1_ranked.csv`; summary: `.worktrees/nutation-peel/.codex-run/nutation_hub_dense_antidir_v1_summary.md`.
- Latest selector result: `18/18` joined files matched, `60` solved Hub/Maze rows audited, `25` unique levelIds, `1` strict Hub row, `10` watch Hub rows, `10` selected rows.
- Selected rows target user feedback by preferring better core fill and lower outer-exit clustering: core coverage `0.900-0.963`, center empty component max `1-6`, outer-exit spatial run `1-2`.
- Remaining risk: solve same-axis/same-dir runs still reach `18/12`, static same-dir component reaches `30`, and strict rows are too scarce. Review-only; do not treat as Hub production approval.
- Current open `.worktrees/nutation-peel/Assets/ArrowMagic/Scenes/Demo.unity` was dirty and still pointed at `NutationHubRailV1Pack`; this pack was created on disk but not force-mounted.

## Campaign500 Long-Chain Pilot3 V4 Perimeter Edge Repair - 2026-06-29

- Worktree: `.worktrees/campaign500-longchain-pilot3`, branch `codex/campaign500-longchain-pilot3`.
- Full/partial visual review pack: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainPilot3V4Pack.asset`; level root `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Levels/Campaign500LongChainPilot3V4/`.
- Source report/summary: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Reports/Campaign500/LongChain/campaign500_longchain_pilot3_v4_report.csv` and `_summary.md`; stopped after 38 rows, `16` selected across sections `3/25/45`.
- Official trace input: `.worktrees/campaign500-longchain-pilot3/.codex-run/campaign500_longchain_pilot3_v4_full_trace_input.csv`; trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_longchain_pilot3_v4_full_fast_metrics.csv`; result `16/16 solved`, missing/failed `0`.
- Demo9 keep CSV/summary: `.worktrees/campaign500-longchain-pilot3/.codex-run/campaign500_longchain_pilot3_v4_demo9_trace_keep.csv` and `_summary.md`; `9/9 solved`, `edgeStraightRunMax max=5`, process tiers `B=5, Drop=4`.
- Demo9 review pack: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainPilot3V4Demo9Pack.asset`, GUID `e720679226003104c837b62bb9156487`; worktree Demo activePack points here.

## Nutation Hub/Maze SolveFlow2 Reports - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Code under test: `Assets/ArrowMagic/Editor/NoMaskProceduralGenerator.cs` Hub/Maze `antiCollapseDebt` and `gateAwareSolveFlow` metadata; PSG assets and PSG source reports are unchanged.
- HubRail solveflow2 source copy: `.worktrees/nutation-peel/.codex-run/nutation_hub_rail_v1_solveflow2_source_report.csv`; joined: `.worktrees/nutation-peel/.codex-run/nutation_hub_rail_v1_solveflow2_trace_joined.csv`; summary: `.worktrees/nutation-peel/.codex-run/nutation_hub_rail_v1_solveflow2_trace_joined_summary.md`; production keep CSV has 0 rows.
- MazeRail solveflow2 source copy: `.worktrees/nutation-peel/.codex-run/nutation_maze_rail_v1_solveflow2_source_report.csv`; joined: `.worktrees/nutation-peel/.codex-run/nutation_maze_rail_v1_solveflow2_trace_joined.csv`; summary: `.worktrees/nutation-peel/.codex-run/nutation_maze_rail_v1_solveflow2_trace_joined_summary.md`; production keep CSV has 0 rows.
- HubSpokeV4 solveflow2 source copy: `.worktrees/nutation-peel/.codex-run/nutation_hub_spoke_v4_solveflow2_source_report.csv`; joined: `.worktrees/nutation-peel/.codex-run/nutation_hub_spoke_v4_solveflow2_trace_joined.csv`; summary: `.worktrees/nutation-peel/.codex-run/nutation_hub_spoke_v4_solveflow2_trace_joined_summary.md`; production keep CSV has 0 rows.
- MazePatchV2 solveflow2 source copy: `.worktrees/nutation-peel/.codex-run/nutation_maze_patch_v2_solveflow2_source_report.csv`; joined: `.worktrees/nutation-peel/.codex-run/nutation_maze_patch_v2_solveflow2_trace_joined.csv`; summary: `.worktrees/nutation-peel/.codex-run/nutation_maze_patch_v2_solveflow2_trace_joined_summary.md`; production keep CSV has 0 rows.
- Temporary generation worktree `F:\Unityproject\ArrowLevel-Hand\.worktrees\nutation-peel-runner` was removed after the source reports were copied back; paths inside the joined summaries may still reference the removed temp source root, so use the copied source CSVs above for durable lookup.

## Campaign500 Long-Chain Pilot3 V5 Perimeter Rail Review - 2026-06-29

- Worktree: `.worktrees/campaign500-longchain-pilot3`, branch `codex/campaign500-longchain-pilot3`.
- Full V5 pack/report: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainPilot3V5Pack.asset`; report `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Reports/Campaign500/LongChain/campaign500_longchain_pilot3_v5_report.csv`; level root `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Levels/Campaign500LongChainPilot3V5/`.
- Official trace input: `.worktrees/campaign500-longchain-pilot3/.codex-run/campaign500_longchain_pilot3_v5_full_trace_input.csv`; trace metrics `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_longchain_pilot3_v5_full_fast_metrics.csv`; result `25/25 solved`.
- Demo9 keep CSV/summary: `.worktrees/campaign500-longchain-pilot3/.codex-run/campaign500_longchain_pilot3_v5_demo9_trace_keep.csv` and `_summary.md`; `9/9 solved`, `B=7/Drop=2`, coverage avg `0.954`, `edgeRailRunMax avg=9.778/max=14`.
- Demo9 review pack: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainPilot3V5Demo9Pack.asset`, GUID `66d31b360efa8c943971353810116171`; worktree `Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.

## Generated-Root WBP V12 t99/t100 Capacity Rerank Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`; generated root level root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/`.
- t99b layout outputs: `t99b_slot_capacity_layout.csv` and `_summary.md`. This was a negative diagnostic because orientation cap alone still selected only vertical bands.
- t99c layout outputs: `t99c_slot_capacity_layout.csv` and `_summary.md`; true mixed layout with `BAND1 vertical 2`, `BAND2 horizontal 15`, `BAND3 vertical 5`, `BAND4 horizontal 19`.
- t99c rootgen outputs: `t99c_sbrg_slot_capacity_layout_seed560420_root_pool.csv`, `_summary.md`, `_basin_plan.csv`, `_growth_log.csv`; level dir `GeneratedRootWBPV12/t99c_sbrg_slot_capacity_layout_seed560420/`. It selected 3 Greedy-solved roots, but post-hoc supply was poor.
- t99c supply/capacity outputs: `t99c_c001_recompute_variant_slotfit_supply*`, `t99c_c002_recompute_variant_slotfit_supply*`, `t99c_c003_recompute_variant_slotfit_supply*`, and `t99c_c003_variant_slot_capacity_*`. Best c003 only reached `9/80` slot-fit edges and max disjoint slot capacity `2`.
- t100a partial output: `t100a_sbrg_slotaware_seed560120_basin_plan.csv` only. Rootgen-internal full slot-fit/audit timed out before writing a root pool; the residual process was stopped. Do not resume t100a as an in-loop full-audit route.
- Current continuation point: create a larger cheap generated-root pool with the t98c viable slot-capacity layout profile, then run post-hoc variant slot-fit/capacity audit per selected root and rerank before variant-aware selector/preplan.

## Generated-Root WBP V12 t101-t103 Target-Diversity Capacity Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`; generated root level root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/`.
- t101a cheap pool reports: `t101a_sbrg_cheap_pool_seed560520_root_pool.csv`, `_summary.md`, `_basin_plan.csv`, `_growth_log.csv`; per-root post-hoc supply/capacity outputs use `t101a_c00*_recompute_variant_slotfit_supply*` and `t101a_c00*_variant_slot_capacity_*`. Result: no root exceeded t98c capacity `5`.
- t102b local hot-bucket layout reports: `t102b_slot_capacity_hotbucket_layout.csv` and `_summary.md`. Rootgen reports use `t102b_sbrg_hotbucket_seed560720_*`; post-hoc supply/capacity outputs use `t102b_c00*_recompute_variant_slotfit_supply*` and `t102b_c00*_variant_slot_capacity_*`. Result: best raw supply reached `24/80`, but max disjoint stayed `3-4`.
- t103a target-diversity rootgen reports: `t103a_sbrg_targetdiv_hotbucket_seed560820_root_pool.csv`, `_summary.md`, `_basin_plan.csv`, `_growth_log.csv`; level dir `GeneratedRootWBPV12/t103a_sbrg_targetdiv_hotbucket_seed560820/`.
- t103a c001 post-hoc audit files: `t103a_c001_recompute_variant_slotfit_supply.csv/_summary.*` and `t103a_c001_variant_slot_capacity_*`; result max disjoint slot capacity `5`.
- t103a c001 wide selector/preplan files: `t103a_c001_variant_joint8_wide_*`; selector result `selected=8`, `slotEdges=5`, `activationTopRoots=6`; semantic preplan result `slotReady=5`, `unmet=3`, `constraints=155`, `demandRootConflicts=14`.
- t103a c001 repair files: `t103a_c001_variant_joint8_wide_semantic_repair_plan_*`; repair-drop selector files: `t103a_c001_variant_joint8_wide_repairdrop_*`. These confirm selector-only repair stays at `5` slot edges, so the next root pool should target capacity `6`.

## Nutation HubSpoke V5 Control Experiment Reports - 2026-06-29

- Worktree: `.worktrees/nutation-hub-v5-control`, branch `codex/nutation-hub-v5-control`; isolated from `.worktrees/nutation-peel`.
- Generated level root: `.worktrees/nutation-hub-v5-control/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationHubSpokeV5/`; pack path `.worktrees/nutation-hub-v5-control/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubSpokeV5Pack.asset`.
- Source report: `.worktrees/nutation-hub-v5-control/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_spoke_v5_report.csv`; production keep CSV is empty because smoke4 had no keep rows.
- Smoke2 joined outputs: `.worktrees/nutation-hub-v5-control/.codex-run/nutation_hub_spoke_v5_smoke2_trace_joined.csv` and `_summary.md`; `4/4` official solved, `0` production keep, same-axis/same-dir still high.
- Smoke4 joined outputs: `.worktrees/nutation-hub-v5-control/.codex-run/nutation_hub_spoke_v5_smoke4_trace_joined.csv` and `_summary.md`; `5/5` official solved, `5/5 Reject`, `5/5 local_collapse`, `0` process/visual/STS/production keep.
- Smoke4 static quality is useful as diagnostic evidence: core coverage roughly `0.915-0.954`, max core hole `1-3`, same-direction neighbor rate `~0.227-0.266`, outer-exit run `1-3`. Playability trace still fails due local/near runs `10-13`, same-axis `11-24`, same-dir `8-21`, and dependency-local rate `0.605-0.687`.
- Status: diagnostic only. Do not present the V5 pack for player review unless a future solve-order scheduler rerun produces strict keep rows.

## V1.31 Skeleton PSG Corridor/Fill Bridge Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`; level root for materialized probes: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/SkeletonPSGCorridorConnectorCutterV1/` and `.../SkeletonPSGSeededSGPFillV1/`.
- Current positive low-coverage proof: `v131_rank85_skeleton_psg_connector_cutter_v1_latehit18_rayclean_candidates.csv` / `_lighttrace_metrics.csv`. Row `cc_cea8273a_a403f2e932_top04` has 4 selected connector units, static contract `4/4 firstHit + 4/4 corridorClear`, official trace `solved=True`, `processTier=A`, `hardStructureV3Class=TrueHardCandidate`, hardV3 `0.762`, avg/max choices `2.89/5`, coverage `0.2441176`.
- Corridor plan inputs: `v131_rank85_skeleton_psg_corridor_wave_plan_v1_latehit18_rayclean_selected_corridors.csv` and `_summary.md`; key fix is dynamic base-ray-block rejection (`earlyBaseRayBlocks=0`) so connector cells do not stand on a base owner escape ray before that owner clears.
- Fill bridge constraints: `v131_rank85_skeleton_psg_fill_constraints_top04_v1_reserve_edges.csv`, `_reserved_cells.csv`, and `_minimal_rolemap_cells.csv`; these are compatible with `Build-SeededDirectSGPFillBaselineV1.ps1` reserve/rolemap inputs.
- Generic high-coverage fill negative: `v131_rank85_skeleton_psg_seeded_sgp_fill_top04_v1_reserveonly_candidates.csv` reached coverage `0.882-0.905`, but manual trace `..._reserveonly_manualtrace_metrics.csv` is `4/4 solved=False`, `4/4 Drop/LocalEasy`, max choices `53-61`. Contract audit `..._reserveonly_contract_audit_summary.md` says all rows are `ContractPreservedButFillerOpenerExplosion`: connector contract still `4/4`, but filler contributes `53-61` initial open heads and `40-41` dynamic base-ray blocks.
- Direct-exit cap boundary: `v131_rank85_skeleton_psg_seeded_sgp_fill_top04_v1_directcap8_candidates.csv` with `MaxBoundaryDirectExitOpenersPerPass=8` only reaches coverage `0.599-0.626`; manual trace `..._directcap8_manualtrace_metrics.csv` is still `4/4 Drop/LocalEasy/unsolved`, max choices `16-17`.
- Release-aware edge-provider boundary: `v131_rank85_skeleton_psg_seeded_sgp_fill_top04_v1_releaseaware_cap8_candidates.csv` consumes `v131_completefill_wholeplan_rank85_rolemap_smoke1_edges.csv` and accepts `16-19` release-aware heads, but coverage only reaches `0.622-0.654`; contract audit still reports `ContractPreservedButFillerOpenerExplosion` with `10-11` filler open heads and `38-39` dynamic base-ray blocks.
- Status: no complete high-coverage Skeleton->PSG level exists yet. Current evidence proves the integration point is real at low coverage, and also proves `reserve corridor + generic SeededDirectSGP fill` is not enough. Next route must add a generation-time PSG filler contract: bounded opener supply, dynamic base-ray block rejection/controlled timed blockers, and a scheduled filler DAG rather than native Dense mop-up.

## V1.31 Skeleton PSG Scheduled DAG Fill Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`; generated level root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/SkeletonPSGScheduledDAGFillV1/`.
- New script: `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SkeletonPSGScheduledDAGFillV1.py`. It is isolated from original PSG and adds filler through a PSG-style dependency contract: owner-hit DAG, opener budget, filler edge insertion, and optional full ray-blocker DAG cycle guard.
- Freeze negative/boundary: `v131_rank85_skeleton_psg_scheduled_dag_fill_v1_strict_smoke_candidates.csv` with strict base-ray and frozen filler corridors preserved all planned edges and kept filler openers at `0`, but stalled at coverage `~0.40`; freezing every filler corridor consumes too much board space.
- Unguarded insert boundary: `v131_rank85_skeleton_psg_scheduled_dag_fill_v1_insert_smoke_candidates.csv` reached coverage `0.55-0.58` with `0` filler openers and all planned edges passing, but `v131_rank85_skeleton_psg_scheduled_dag_fill_v1_insert_t550_*trace*` showed `solved=False`. Cause: first-hit DAG is insufficient because later blockers on the same ray create hidden full-ray dependencies/cycles.
- Guarded strict positive: `v131_rank85_skeleton_psg_scheduled_dag_fill_v1_insert_guarded_smoke_candidates.csv` with `insert + --full-ray-dag-guard + base-ray strict` produced solved B candidates. Official traces: `v131_rank85_skeleton_psg_scheduled_dag_fill_v1_insert_guarded_t500_official_trace_metrics.csv` for coverage `0.5029412` is `solved=True`, `processTier=B`, openers `3`, avg/max choices `3.91/11`; `..._t542_official_trace_metrics.csv` for coverage `0.5421569` is `solved=True`, `processTier=B`, openers `3`, avg/max `4.01/10`.
- Guarded head-ray positive: `v131_rank85_skeleton_psg_scheduled_dag_fill_v1_insert_guarded_headray_smoke_candidates.csv` allows filler bodies, but not heads, on base schedule ray cells. Row `sdag_rt_guarded_headray_smoke_32_top04_t700_v01` reaches coverage `0.6294118`, keeps `initialFillerOpenHeads=0`, and official trace `v131_rank85_skeleton_psg_scheduled_dag_fill_v1_insert_guarded_headray_t629_official_trace_metrics.csv` is `solved=True`, `processTier=B`, openers `2`, avg/max `4.15/10`.
- Base-ray-off negative: `v131_rank85_skeleton_psg_scheduled_dag_fill_v1_insert_guarded_baserayoff_smoke_candidates.csv` stalled lower (`0.465-0.473`) because allowing heads on base dynamic ray cells creates full-ray cycles quickly. Fan-in relaxed probe `..._headray_fanin4_smoke_candidates.csv` also did not beat the default head-ray run.
- Contract audit: `v131_rank85_skeleton_psg_scheduled_dag_fill_v1_insert_guarded_contract_audit_*` shows connector contract remains `4/4` and filler openers remain `0`. Its `baseScheduleAddedRayBlockCount=5` matches the top04 connector baseline recheck, so strict guarded filler did not add new base-ray pollution beyond the materialized connector baseline.
- Current status: route is proven for solvable B-level partial completion up to `0.629` coverage from the rank85 top04 skeleton, but not yet full/high coverage. Next useful work is timed base-ray blocker ownership and a capacity strategy for full ray-blocker DAG space; do not return to reserve+dense fill.

## V1.31 Skeleton PSG Solver Topology / Realizer Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`; realizer level root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/SkeletonPSGTopologyRealizerV1/`.
- Abstract release-lane topology outputs: `v131_rank85_skeleton_psg_solver_topology_v1_lanes4_smoke_*`, `v131_rank85_skeleton_psg_solver_topology_v1_lanes4_timed020_smoke_*`, and `v131_rank85_skeleton_psg_solver_topology_v1_lanes4_timed000_smoke_*`. The `lanes4` smoke proves abstract A/B full-ray topology at 0.88/0.94; `timed000` is the best current geometry source.
- Geometry realizer script: `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SkeletonPSGTopologyRealizerV1.py`; it writes candidates/chain plans and partial LevelDefinition assets without touching PSG production code.
- Stable positive route proof: `v131_skelpsg_toporeal_t940v02_t0_selfguard_neutral_pos1_candidates.csv` and `_chain_plan.csv`; generated asset `SkeletonPSGTopologyRealizerV1/real_guard_neutral_pos1__a_a403f2e932_top04_t940_v02.asset`; coverage `0.7274510`, `107` generated chains, `fullRayDagPass=True`.
- Official light trace for the positive: `v131_skelpsg_toporeal_t940v02_t0_selfguard_neutral_pos1_trace_light_metrics.csv`; result `solved=True`, process/tight tier `B/B`, openers `2`, avg/p80/max choices `2.86/4/7`, no choice peaks. Boundary: `LocalEasy`, `dependencyFollowRunMax=7`, `localPatchSolveRunMax=7`; not a final hard-feel keep.
- Capacity audit outputs for the positive: `v131_skelpsg_topocapaudit_t940v02_t0_pos1_summary.csv`, `_node_capacity.csv`, `_chain_risk.csv`, `_target_pressure.csv`, and `_summary.md`. Key result: placed `107/207`, failure at topo owner `155` rank `31`; failed node has only `5/980` planned-target ray checks and `0` path variants, while generated local first-hit rate is `0.8505`.
- V2 realizer probe outputs: `v131_skelpsg_toporeal_v2_t940v02_capaware_smoke1_lite_*`, `v131_skelpsg_toporeal_v2_t940v02_shadowcost_smoke6_lite_*`, `v131_skelpsg_toporeal_v2_t940v02_shadowcost_w64_smoke8_*`, plus strict-shadow/light-frontier negative smokes. `capaware_smoke1_lite` reached coverage `0.7333333` / `127` generated / planned local hit `0.4567`, but official trace remained `LocalEasy` with `localPatchSolveRunMax=9`; audit showed final actual first-hit relocalized. `shadowcost_smoke6_lite` solved/B at coverage `0.6745098`, but still `LocalEasy` and `dependencyFollowRunMax=10`.
- Negative/control outputs: `v131_rank85_skeleton_psg_topology_realizer_v1_t940v02_timed000_selfguard_antilocal_smoke180_loose_*` improves some collapse diagnostics but drops to coverage `0.7009804` and official `processTier=Drop`; timed-heavy realizer smokes (`timed020`, default timed) stall at timed base-ray slot intersections.
- Important audit: pre-selfguard timed000 realizer produced a `0.7245` full-ray-DAG-pass asset that official trace marked unsolved; self-ray audit found generated chains whose bodies occupied their own head ray. Future realizer/slot code must keep `rejectSelfRayBody`.

## Campaign500 Long-Chain Pilot3 V7 - 2026-06-29

- Worktree: `.worktrees/campaign500-longchain-pilot3`, branch `codex/campaign500-longchain-pilot3`.
- Full V7 pack: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainPilot3V7Pack.asset`; level root `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Levels/Campaign500LongChainPilot3V7/`.
- Source report/summary: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Reports/Campaign500/LongChain/campaign500_longchain_pilot3_v7_report.csv` and `_summary.md`; section3 selected orders `22/30/25`, coverage avg `0.9643`, source outer-exit head counts `8/10/16`.
- Official trace input: `.worktrees/campaign500-longchain-pilot3/.codex-run/campaign500_longchain_pilot3_v7_section3_trace_input.csv`; official trace metrics `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_longchain_pilot3_v7_section3_fast_metrics.csv`; result `3/3 solved`, process tiers `A/B/Drop`.
- Demo review pack: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainPilot3V7Demo9Pack.asset`, GUID `fc08a06ec1e285a4abd7f7ecc810bb70`; worktree `Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.
- Boundary: visual-review only. Use V7 to inspect whether total outward heads now feel acceptable; do not treat the challenge row as production keep until opener/outer-exit pressure improves beyond the current Drop trace.

## Generated-Root WBP V12 t104-t105 Target-Basin Capacity Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`; generated root level root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/`.
- t104d target-diversity gate reports: `t104d_sbrg_targetdiv_gate_hotbucket_seed560800_root_pool.csv`, `_summary.md`, `_basin_plan.csv`, and `_growth_log.csv`; generated levels under `GeneratedRootWBPV12/t104d_sbrg_targetdiv_gate_hotbucket_seed560800/`.
- t104d per-root post-hoc capacity reports: `t104d_sbrg_targetdiv_gate_hotbucket_seed560800_c00*_recompute_variant_slotfit_supply.csv` plus `t104d_sbrg_targetdiv_gate_hotbucket_seed560800_c00*_variant_slot_capacity_*`. Best repeatable result is c002 max disjoint `5`; c003/c004 are lower.
- t105 target-basin layout diagnostics: `t105a_target_basin_partition_layout*` through `t105e_target_basin_partition_layout*`. t105e is the useful layout: `targetBasinBuckets=6`, including owner4 center rank `4` at `5,4` to avoid owner0's `4,6` bucket.
- t105f rootgen smoke: `t105f_sbrg_targetbasin6_seed561120_root_pool.csv`, `_summary.md`, `_basin_plan.csv`, and `_growth_log.csv`; generated level dir `GeneratedRootWBPV12/t105f_sbrg_targetbasin6_seed561120/`. It selected `t105f_sbrg_targetbasin6_seed561120_c001` with coverage `0.3137652`, Greedy solved, strict-edge target owners/top roots `7/4`, and target dominance `0.438`.
- t105f post-hoc supply/capacity: `t105f_c001_recompute_variant_slotfit_supply.csv`, `_summary.*`, and `t105f_c001_variant_slot_capacity_*`. Result: raw supply `20/80` slot-fit edges and `114` candidates, but max disjoint slot capacity only `4`; use as evidence that old-root static target basins do not bind to new-root owner identity.

## Campaign500 Long-Chain Pilot3 V8 Opener Reduction Review - 2026-06-29

- Worktree: `.worktrees/campaign500-longchain-pilot3`, branch `codex/campaign500-longchain-pilot3`.
- Full V8 pack: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainPilot3V8Pack.asset`; level root `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Levels/Campaign500LongChainPilot3V8/`.
- Source report/summary: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Reports/Campaign500/LongChain/campaign500_longchain_pilot3_v8_report.csv` and `_summary.md`; section3 selected orders `22/25/30`, coverage avg `0.9643`, source outer-exit head counts `8/14/9`, source initial openers `8/14/9`.
- Official trace input: `.worktrees/campaign500-longchain-pilot3/.codex-run/campaign500_longchain_pilot3_v8_section3_trace_input.csv`; official trace metrics `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_longchain_pilot3_v8_section3_fast_metrics.csv`; result `3/3 solved`, process tiers by order `22=A`, `30=B`, `25=Drop`.
- Demo review pack: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainPilot3V8Demo9Pack.asset`, GUID `e69b0d21ce1cda0489bac71c2de49348`; worktree `Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.
- Boundary: visual-review only. V8 improves V7 challenge outward opener pressure but is not production strict because the challenge row still has `openers=14` and official `Drop`.

## Campaign500 Long-Chain Pilot3 V9 Challenge-Only Opener Review - 2026-06-29

- Worktree: `.worktrees/campaign500-longchain-pilot3`, branch `codex/campaign500-longchain-pilot3`.
- Full V9 pack: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainPilot3V9Pack.asset`; level root `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Levels/Campaign500LongChainPilot3V9/`.
- Source report/summary: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Reports/Campaign500/LongChain/campaign500_longchain_pilot3_v9_report.csv` and `_summary.md`; V9e selected `5/9`, orders `22/25/30`.
- Official trace input: `.worktrees/campaign500-longchain-pilot3/.codex-run/campaign500_longchain_pilot3_v9e_selected_trace_input.csv`; official trace metrics `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_longchain_pilot3_v9e_section3_metrics.csv`; selected rows all solved, process tiers include order22 `A`, order30 `B`, and challenge variants still `Drop`.
- Demo3 manifest: `.worktrees/campaign500-longchain-pilot3/.codex-run/campaign500_longchain_pilot3_v9e_demo3_trace_input.csv`; chosen challenge row is `campaign500_longchain_pilot3_v9_03_l025_lock_buckle_pressure_slot_balanced` with source openers/head `11/11`.
- Demo review pack: `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainPilot3V9Demo9Pack.asset`, GUID `e2a38df3a9e55d6488a272b642a9ca39`; worktree `Assets/ArrowMagic/Scenes/Demo.unity` activePack points here.
- Boundary: visual/metric review only. V9e improves challenge opener/max-choice metrics but does not improve process tier; do not expand until low-opener and low-choice branches are combined.

## Generated-Root WBP V12 t106 Current-Root Target-Basin Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`; generated root level root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/`.
- t106a recomputed t105f with current-root slot-fit target metrics: `t106a_c001_recompute_variant_slotfit_supply.csv`, `_summary.csv`, and `_summary.md`. Result: slot-fit `20/80`, current target owners/top roots/basin keys `5/4/5`, dominance `0.350`; role-slot reserve `3/3`.
- t106a current-root target-basin layout diagnostic: `t106a_current_root_target_basin_layout.csv` and `_summary.md`. It produced only `5` target-basin buckets (`layoutRows=120`, preserve `49`, avoid `46`, target `25`), proving t105f had already collapsed at the current-root basin layer.
- t106b current-target rootgen: `t106b_sbrg_currenttarget_seed561240_root_pool.csv`, `_summary.md`, `_basin_plan.csv`, and `_growth_log.csv`; level dir `GeneratedRootWBPV12/t106b_sbrg_currenttarget_seed561240/`. It selected `t106b_sbrg_currenttarget_seed561240_c001`, Greedy solved at coverage `0.3036`, but post-hoc supply/capacity (`t106b_c001_recompute_variant_slotfit_supply*`, `t106b_c001_variant_slot_capacity_*`) collapsed to current target owners/basin keys `3/3` and max disjoint `3`.
- t106c soft old-target-basin feedback: `t106c_sbrg_soft_targetbasin6_seed561360_root_pool.csv`, `_summary.md`, `_basin_plan.csv`, and `_growth_log.csv`; level dir `GeneratedRootWBPV12/t106c_sbrg_soft_targetbasin6_seed561360/`. Post-hoc outputs `t106c_c001_recompute_variant_slotfit_supply*` and `t106c_c001_variant_slot_capacity_*` collapsed harder to target owners/basin keys `2/2` and max disjoint `2`.
- t106d light-role activation gate fresh band: `t106d_sbrg_lightrole_gate_seed561520_root_pool.csv`, `_summary.md`, `_basin_plan.csv`, and `_growth_log.csv`; selected `0`. Use as boundary evidence only; fresh seed band mostly missed strict target diversity before the new gate mattered.
- t106e light-role activation gate replay band: `t106e_sbrg_lightrole_gate_seed560800_root_pool.csv`, `_summary.md`, `_basin_plan.csv`, and `_growth_log.csv`; level dir `GeneratedRootWBPV12/t106e_sbrg_lightrole_gate_seed560800/`. Selected `t106e_sbrg_lightrole_gate_seed560800_c001`, Greedy solved at coverage `0.3117`, with planned light-role activation top roots `2`, but post-hoc outputs `t106e_c001_recompute_variant_slotfit_supply*` and `t106e_c001_variant_slot_capacity_*` still collapsed to target owners/basin keys `3/3` and max disjoint `2`.
- Current conclusion: old-root target-basin layouts and cheap strict-edge diversity are false positives for the new generated root. The next root-pool rerank gate must use current-root post-hoc slot-fit target owners/basin keys plus max disjoint capacity; t103a/t104d c002 remain the best known max-disjoint `5` class, with t107 later refining their current supply target shape to `5/4/6`. The next milestone is a generated root with current-root capacity `6+`.

## Generated-Root WBP V12 t107 Batch Current-Root Rerank Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`; generated root level root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/`.
- t107a batch rerank outputs: `t107a_current_root_capacity_rerank.csv`, `_summary.md`, and per-root audit folder `t107a_current_root_capacity_rerank/`. It audits 14 selected roots from t103a/t104d/t105f/t106b/t106c/t106e. Result: `0/14` pass the current-root promotion gate; best rows are t103a c001 and t104d c002 at current supply targets `5/4/6`, dominance `0.455`, max disjoint `5`.
- t107b strict proxy reconstruction outputs: `t107b_sbrg_rerank_seed561680_root_pool.csv`, `_summary.md`, `_basin_plan.csv`, and `_growth_log.csv`; selected `0`. Use as evidence that proxy dominance `0.45` plus the reconstructed hotbucket layout is too narrow for this seed band.
- t107c relaxed proxy rootgen outputs: `t107c_sbrg_rerank_seed561680_root_pool.csv`, `_summary.md`, `_basin_plan.csv`, and `_growth_log.csv`; generated level dir `GeneratedRootWBPV12/t107c_sbrg_rerank_seed561680/`. It selected `t107c_sbrg_rerank_seed561680_c001`, Greedy solved at coverage `0.3036437`, but current-root rerank outputs `t107c_current_root_capacity_rerank.csv`, `_summary.md`, and folder `t107c_current_root_capacity_rerank/` show supply targets `3/2/3` and max disjoint `3`.
- t107c blocker diagnostics now appear in the rerank CSV: top blocked targets `7:12,10:6,16:6`, top blocked basins `B3->CHOKE:12,B2->CHOKE:6,B1->B2:6`, and top overlap cells `9,4:18,7,4:14,8,4:14,16,3:12,15,3:12`. Use these for the next current-root feedback layout/rootgen pass.
- Status: no `capacity 6+` generated root exists yet. t107 establishes the automated promotion/audit surface; next work should consume rerank blocker/overlap diagnostics rather than promoting strict-edge proxy roots directly.

## Generated-Root WBP V12 t108 Owner-Basin Feedback Layout Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`; generated root level root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/`.
- t108a feedback layout outputs: `t108a_current_root_ownerbasin_feedback_layout.csv` and `_summary.md`. It consumes t107a rank1 (`t103a_sbrg_targetdiv_hotbucket_seed560820_c001`) selected/overlap/supply, uses `target-basin-group-key=owner_basin`, and emits `6` target-basin buckets from current-root supply. This is a representation fix: owner20 appears as both `B2->B3` and `B1->B3`.
- t108b soft owner-basin feedback rootgen outputs: `t108b_sbrg_ownerbasin_soft_seed561840_root_pool.csv`, `_summary.md`, `_basin_plan.csv`, and `_growth_log.csv`; selected `0` at root coverage `0.30` / min `0.28`.
- t108c low-root soft feedback outputs: `t108c_sbrg_ownerbasin_soft_lowroot_seed561840_root_pool.csv`, `_summary.md`, `_basin_plan.csv`, `_growth_log.csv`; generated level dir `GeneratedRootWBPV12/t108c_sbrg_ownerbasin_soft_lowroot_seed561840/`. It selected `t108c_sbrg_ownerbasin_soft_lowroot_seed561840_c001`, but rerank outputs `t108c_current_root_capacity_rerank.csv`, `_summary.md`, and folder `t108c_current_root_capacity_rerank/` show current targets `3/2/3`, dominance `0.467`, max disjoint `3`.
- t108d hard owner-basin feedback outputs: `t108d_sbrg_ownerbasin_hard_seed561920_root_pool.csv`, `_summary.md`, `_basin_plan.csv`, `_growth_log.csv`; generated level dir `GeneratedRootWBPV12/t108d_sbrg_ownerbasin_hard_seed561920/`. Rerank outputs `t108d_current_root_capacity_rerank.csv`, `_summary.md`, and folder `t108d_current_root_capacity_rerank/`; best row c002 reaches only current targets `4/4/4`, dominance `0.600`, max disjoint `3`.
- Status: t108 proves owner-basin grouping is useful for diagnostics, but static migrated feedback layouts still do not bind the next generated root's owner identity. Continue with t107-style batch promotion or in-generation current-root slot-fit approximation, not more static bucket transfer alone.

## Generated-Root WBP V12 t109 Root-Pool Capacity Search Reports - 2026-06-29

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`; generated root level root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/`.
- t109 root-pool search script: `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Run-GeneratedRootWBPV12RootPoolCapacitySearchV1.py`.
- t109d smoke outputs: `t109d_rootpool_capacity_smoke_*`. One selected root at coverage `0.2854251`; current-root rerank was `3/3/3`, max disjoint `3`.
- t109g two-band search outputs: `t109g_rootcap2_aggregate_rerank.csv`, `t109g_rootcap2_bands.csv`, and `t109g_rootcap2_summary.md`; per-band reranks `t109g_rootcap2_s560800_crcap*` and `t109g_rootcap2_s560880_crcap*`.
- t109g best root: `t109g_rootcap2_s560880_c001`, coverage `0.2854251`, current-root supply targets `7/6/7`, dominance `0.467`, max disjoint `4`, selected edges `16->10,12->11,10->3,1->8`. It does not pass the capacity `6+` gate but proves relation diversity can be present while slot capacity still collapses.
- t109g best overlap diagnostics live under `t109g_rootcap2_s560880_crcap/001_t109g_rootcap2_s560880_c001_slot_capacity_overlap_cells.csv`; top cells include `9,3:38`, `8,3:32`, `4,3:20`, `7,3:18`, `4,19:18`, `4,20:18`.
- t109h overlap-feedback probe outputs: `t109h_overlap_feedback_*`; it consumed the t109g overlap map through the slot-fit blocker-map path and loaded `12` cells, but reproduced the same root and same capacity `4`. Treat it as evidence that soft overlap penalties alone are not enough.

## Generated-Root WBP V12 t110-t111 Low-Root Choke Plan - 2026-07-01

- Worktree/report root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/`; generated root level root: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/`.
- t110a low-root capacity pass outputs: `t110a_relaxed_rootpool_probe_summary.md`, `t110a_relaxed_rootpool_probe_aggregate_rerank.csv`, root pool `t110a_relaxed_rootpool_probe_s560880_root_pool.csv`, and current-root rerank folder `t110a_relaxed_rootpool_probe_s560880_crcap/`.
- t110a generated root asset: `GeneratedRootWBPV12/t110a_relaxed_rootpool_probe_s560880/t110a_relaxed_rootpool_probe_s560880_c001.asset`; root coverage `0.2408907`, current-root gate pass `6/6/6`, dominance `0.280`, `capacityMaxDisjoint=6`.
- t110b mid-coverage countercheck outputs: `t110b_midcov_rootpool_probe_summary.md`, `_bands.csv`, `_aggregate_rerank.csv`, and growth log `t110b_midcov_rootpool_probe_s560880_growth_log.csv`; with `minSelectCoverage=0.26`, selected roots were `0`.
- t111 semantic cell plan outputs: `t111_chokeplan_from_t110a_semantic_cell_plan.csv`, `t111_chokeplan_from_t110a_semantic_edge_plan.csv`, and `_summary.md`. The plan has board `19x26`, root cells `119`, selected capacity edges `6`, planned non-root duty cells `97`, root plan conflicts `5`.
- t111 planned choke audit outputs: `t111_chokeplan_from_t110a_choke_moment_plan.csv` and `_summary.md`. It marks status `ready_for_whole_board_fill_probe`, with root coverage `0.241`, planned duty coverage `0.437`, target `0.95` requiring `253` more filler/body cells after duty, and two ready 1-choice choke moments: `3->11` and `6->12`.
- t111a direct old-V12 cutting smoke outputs: `t111a_chokecut_smoke1_*`; result `0` candidates because old state-frontier cutter produced `frontier=0/beamStates=0` against the t110a capacity-selected edges. Treat as interface-boundary evidence, not root failure.
- t111b ordered slot-fit outputs live under `t111b_ordered_slotfit_from_t110a/`. It preserves the selected edge set `6->12,1->6,14->4,8->9,3->11,4->13` and adds ordered paths for all 6 chains; mechanical audit found `43` chain cells, no self-overlap, no inter-chain overlap, and all adjacent.
- t111c materialized semantic-core level: `GeneratedRootWBPV12/t111c_slotfit_materialized_from_t110a/t111c_slotfit_materialized_from_t110a.asset`; reports `t111c_slotfit_materialized_from_t110a_summary.md`, `_chain_plan.csv`, `_root_identity.csv`. Result: root prefix preserved, 21 chains, coverage `0.3279352`, Greedy solved, 6/6 semantic chains first-hit their planned activation owner.
- t111d-i protected fill levels live in `GeneratedRootWBPV12/t111d_protected_fill_from_t111c/`. Milestones: t111d `0.5506073` solved/maxChoice4, t111e `0.7570850` solved/maxChoice4, t111f `0.8906883` solved/maxChoice6, t111h `0.9453441` solved/maxChoice7, final t111i `0.9534413` solved/maxChoice7.
- Final t111i level: `GeneratedRootWBPV12/t111d_protected_fill_from_t111c/t111i_protected_fill_from_t111h_final095.asset`; trace input `t111i_grwbp095_trace_input.csv`; official trace `t111i_grwbp095_trace1_metrics.csv` and `_summary.md`. Trace result: solved `True`, process/tight `B/B`, openers `4`, avg/max choices `4.1/7`, `choiceRhythmScoreV1=0.595`, but HardStructure `LocalEasy`, `localPatchSolveRunMax=7`, `causalAntiLocalityScore=0.16`. Coverage/solvability/root identity breakthrough, not final A/Hard acceptance.
- t111i simultaneous wave-front audit outputs: `t111i_grwbp095_wave_front_audit.csv` and `_summary.md`. Under "clear every currently available chain as one wave", it solves in `39` waves with counts `4 3 3 3 2 2 2 1 2 3 3 1 1 3 5 3 2 1 2 1 2 1 1 3 1 1 2 1 1 1 1 1 3 4 2 3 2 1 1`; avg/p50/p80/max `2.026/2/3/5`, `26/39` waves are `<=2`, forced single waves `16`, and semantic-slot chains appear in waves `2,6,15,18,31,37`.
- Demo review pack for user playtest: `Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GeneratedRootWBPV12_t111i_DemoPack.asset`, GUID `4b8e0982d542436bacfdba5e6ccb131b`; `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` now points `activePack` at this single-level pack.

## Generated-Root WBP V12 t112 Placement Proxy Reports - 2026-07-01

- Summary report: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t112_generated_root_wbp_base_and_placement_proxy_summary.md`. It settles t111 as the base route and records t112 placement proxy findings.
- t112c endpoint closure asset/report: `GeneratedRootWBPV12/t112_placement_proxy_from_t111c/t112c_placement_proxy_from_t111h_final095.asset`, `t112c_placement_proxy_trace1_metrics.csv`, and `t112c_placement_proxy_from_t111h_final095_wave_front_audit_summary.md`. Same coverage as t111i (`0.9534413`), official solved, maxChoices `6` vs t111i `7`, but localPatch/nearOuter worsened to `11/11`; still `B/LocalEasy`.
- t112c Demo review pack: `Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GeneratedRootWBPV12_t112c_DemoPack.asset`, GUID `706dbb8c57b74800aa2da3f4aaecdb5f`; `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity` now points `activePack` at this single-level t112c pack for user difficulty comparison against t111i.
- t112e/t112f hard placement prefilter probes: `t112e_placement_prefilter_from_t111e_probe089_*` and `t112f_placement_prefilter_relaxed_from_t111e_probe089_*`. They preserved spread but stalled at coverage `0.779/0.828`, so placement proxy should not be used as a hard gate.
- t112g/t112h score-only mid-fill probes: `t112g_placement_scoreonly_from_t111e_probe089_*`, `t112h_placement_scoreonly_from_t112g_probe089_*`, and comparison trace `t112_midplacement_compare_trace1_metrics.csv`. t112h reached coverage `0.8927`, maxChoices `4` vs t111f `7`, but over-compressed choices (`low2Rate 0.723`, lowChoiceRunMax `13`, choke score `0`) and stayed `B/LocalEasy`.
- Placement conclusion: optimize for rhythm bands and anti-local interleave, not just lower choices. Next implementation target is a t113 two-stage bundle scheduler.

## Nutation HubSpoke V5 Pool Reports - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`. Active Hub V5 pool output is separate from the original `NutationHubSpokeV5Pack`.
- Full Pool level root: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationHubSpokeV5Pool/`; full Pool pack `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubSpokeV5PoolPack.asset`.
- Source report: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_spoke_v5_pool_report.csv`; it contains `24/24` generated rows from 6 seed-offset variants of 4 Hub V5 specs.
- Trace outputs: `.worktrees/nutation-peel/.codex-run/nutation_hub_spoke_v5_pool_smoke1_traceonly_trace_joined.csv`, `_summary.md`, `_trace_best_per_slot.csv`, and `_production_keep.csv`; official metrics live at `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/nutation_hub_spoke_v5_pool_smoke1_traceonly_metrics.csv`.
- Result: official trace `24/24 solved`, `9` processKeep, `2` TraceOrderKeep / production keep. The two keeps improve Hub V5 same-axis/same-dir to `7/6` and `6/6`, but both still carry local/directional review risks.
- Review pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubSpokeV5PoolProductionKeepPack.asset`, GUID `ca27d341c7fff2c4981fd411dbc9df75`; worktree `Assets/ArrowMagic/Scenes/Demo.unity` activePack points here. Boundary: review/prototype keep, not final strict production.

## Nutation HubSpoke V5 LocalBreak Reports - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`. LocalBreak is a Hub V5.1 review/prototype lane, separate from V5 and V5Pool.
- Full level root: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationHubSpokeV5LocalBreak/`; full pack `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubSpokeV5LocalBreakPack.asset`, GUID `fc62c0876adad0a42b083d855ca6226a`.
- Source report: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_spoke_v5_localbreak_report.csv`; smoke4 source has `16` generated rows.
- Trace outputs: `.worktrees/nutation-peel/.codex-run/nutation_hub_spoke_v5_localbreak_smoke4_trace_joined.csv`, `_summary.md`, `_trace_best_per_slot.csv`, and `_production_keep.csv`; official metrics live at `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/nutation_hub_spoke_v5_localbreak_smoke4_metrics.csv`.
- Smoke4 result: `16/16` official solved, `6` processKeep, `1` VisualKeep, `1` TraceOrderKeep, `1` TraceOrder production keep. No strict overlap yet: VisualKeep is local-cleaner but misses STS/same-axis/dependency gates; TraceOrderKeep has stronger STS but misses local/directional visual gates.
- Review manifest: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_spoke_v5_localbreak_review.csv` contains the VisualKeep and TraceOrderKeep representative rows. The separate review-pack build was blocked by an already-open Unity instance; the full LocalBreak pack exists but the currently open/dirty Demo activePack was not force-switched.

## Nutation Production Readiness Matrix / Representative Pack - 2026-06-29

- Worktree: `.worktrees/nutation-peel`, branch `codex/nutation-peel`.
- Matrix outputs: `.worktrees/nutation-peel/.codex-run/nutation_style_matrix_v1_current_matrix.csv`, `_summary.md`, `_best_rows.csv`, `_strict_keep_rows.csv`, `_representative_rows.csv`, and `_production_readiness.md`.
- Current matrix includes Hub V5 Pool and covers `20` joined CSVs / `97` rows. Strict keep rows are `17`; representative style-chain rows are `16`.
- Representative review pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationStyleMatrixRepresentativeReviewPack.asset`, GUID `2d8a4da775c84f85bc9575d170512392`, display name `Nutation Style Matrix Representative Review (16)`.
- Demo scene: `.worktrees/nutation-peel/Assets/ArrowMagic/Scenes/Demo.unity` activePack points to the representative pack. If Unity was already open, refresh/reopen the scene to see the new asset.
- Production-ready review lanes: LongChain curve/rail/patch/spine and Peel curve/rail. Controlled mix lanes: Flow curve/rail/patch. Hub, Maze, and PeelPatch are not production-ready yet.
- Main project production preview pack: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationProductionPreviewV1Pack.asset`, GUID `5c44ad9e7d9a43e9a602d115e4d99a71`, display name `Nutation Production Preview V1 (9)`. It contains one review sample each for Flow curve/patch/rail, Peel curve/rail, and LongChain curve/patch/rail/spine; main `Assets/ArrowMagic/Scenes/Demo.unity` points here.
- Main production preview manifest: `.codex-run/nutation_production_preview_v1_rows.csv`; summary: `.codex-run/nutation_production_preview_v1_summary.md`.
- Main Mixed canvas preview pack: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationMixedCanvasPreviewV1Pack.asset`, GUID `c25181a4948c4cdcb0a1af617f62c55c`, display name `Nutation Mixed Canvas Preview V1 (3)`. It contains 3 traced quality-gate rows for small/medium/large canvas review and current main `Demo.unity` points here.
- Main Mixed canvas preview manifest: `Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_mixed_canvas_preview_v1_rows.csv`.
- Main Mixed neutral preview pack V1: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationMixedNeutralPreviewV1Pack.asset`, GUID `5cfb432cf3fb4fcda59ec3c241bfdb3d`, display name `Nutation Mixed Neutral Preview V1 (3)`. It contains 3 PSG-like neutral rows with long/short chain blend; retained for comparison.
- Main Mixed neutral preview manifest: `Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_mixed_neutral_preview_v1_rows.csv`.
- Main Mixed neutral preview pack V2: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationMixedNeutralPreviewV2Pack.asset`, GUID `75478b0f8cae412ea77652e3c0b260fb`, display name `Nutation Mixed Neutral Preview V2 (3)`. It contains 3 additional PSG-like neutral rows and current main `Demo.unity` points here.
- Main Mixed neutral preview V2 manifest: `Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_mixed_neutral_preview_v2_rows.csv`.
- Campaign500 normal 4-slot plan V1: `Exports/Campaign500_PSG_Template_20260626_095625/campaign500_normal_4slot_plan_v1.csv` with summary `campaign500_normal_4slot_plan_v1_summary.md`. It defines 200 normal-only slot targets, 4 per section.
- Campaign500 normal Pilot20 plan V1: `Exports/Campaign500_PSG_Template_20260626_095625/campaign500_normal_pilot20_plan_v1.csv` with summary `campaign500_normal_pilot20_plan_v1_summary.md`. It is the first production smoke input for sections 1, 5, 15, 30, and 45.

## Campaign500 Long-Chain Pilot3 V10-V12 - 2026-06-29

- `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Levels/Campaign500LongChainPilot3V10/` - V10 lowchoice-pressure probe assets. Negative: `slot_lowchoice_pressure` was static-clean but official trace choice pressure worsened.
- `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Levels/Campaign500LongChainPilot3V11/` - V11 headmix-outerclean probe assets. `slot_headmix_outerclean` was not selected; outerRingRun/side failed review.
- `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Levels/Campaign500LongChainPilot3V12/` - Current V12 section3 assets. Selected review rows include order22 headmix, order30 balanced, order25 balanced, plus challenge spine alternative.
- `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainPilot3V12Demo9Pack.asset` - Current mounted worktree Demo3 pack, GUID `c603006a701c4034abfbf970e8016ea7`; contains order22 headmix, order25 balanced, order30 balanced.
- `.worktrees/campaign500-longchain-pilot3/.codex-run/campaign500_longchain_pilot3_v12_demo3_trace_input.csv` - Current Demo3 keep manifest for `BuildCampaign500Pilot3DemoPackBatch`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_longchain_pilot3_v12_section3_metrics.csv` - Official V12 selected trace metrics; result `4/4 solved`, process tiers `A/B/Drop/Drop`.

## Campaign500 Long-Chain Pilot3 V13 Style3x3 Preview - 2026-06-29

- `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Levels/Campaign500LongChainPilot3V13/` - V13 preview level assets; selected 9 rows across three roles: `section_long_lock`, `lock_light_negative_space`, `lock_buckle_pressure`.
- `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Reports/Campaign500/LongChain/campaign500_longchain_pilot3_v13_report.csv` - V13 source report; selected `9/21` before stopping generation after each role had 3 rows.
- `.worktrees/campaign500-longchain-pilot3/.codex-run/campaign500_longchain_pilot3_style3x3_plan_v1.csv` - 9-slot preview plan, 3 rows per role.
- `.worktrees/campaign500-longchain-pilot3/.codex-run/campaign500_longchain_pilot3_v13_style3x3_trace_input.csv` - V13 Demo9 keep/trace input used for official trace and demo-pack build.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_longchain_pilot3_v13_style3x3_metrics.csv` - Official trace metrics; `9/9 solved`, tiers `LongNormalA=A/A/B`, `LongNormalB=Drop/Drop/Drop`, `LongChallenge=Drop/Drop/Drop`.
- `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainPilot3V13Demo9Pack.asset` - Current mounted V13 style3x3 Demo pack, GUID `0ca183baef294684596e53d9591491d5`; worktree `Demo.unity` activePack points here.

## Nutation Hub V5 Hybrid Review Reports - 2026-06-29

- `.worktrees/nutation-peel/.codex-run/nutation_hub_v5_hybrid_review_v1_ranked.csv` - Hub V5 Pool + LocalBreak hybrid-ranked report; `40` rows, `0` HybridStrict.
- `.worktrees/nutation-peel/.codex-run/nutation_hub_v5_hybrid_review_v1_review_rows.csv` - Eight diagnostic near-miss review rows; not a production keep and no pack mounted.
- `.worktrees/nutation-peel/.codex-run/nutation_hub_v5_hybrid_review_v1_summary.md` - Summary showing the best LocalBreak row is VisualNearTrace and best Pool row is TraceNearVisual; next Hub V5 work should target generation-side convergence of those metrics.
- `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationHubSpokeV5Hybrid/` - Current Hub V5.2 Hybrid generated level assets, restored to smoke4 code/asset state after smoke5 negative test.
- `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubSpokeV5HybridPack.asset` - Full Hybrid pack from the restored smoke4-code generation run.
- `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_spoke_v5_hybrid_report.csv` - Current Hybrid source report, restored to smoke4-code behavior.
- `.worktrees/nutation-peel/.codex-run/nutation_hub_spoke_v5_hybrid_smoke2_trace_joined_summary.md` - Previous best Hybrid trace summary: `19` rows, `1` TraceOrderKeep, strict gap `nearOuterRun+1`.
- `.worktrees/nutation-peel/.codex-run/nutation_hub_spoke_v5_hybrid_smoke3_trace_joined_summary.md` - Negative local/near counterweight run: `20` rows, `0` production keep.
- `.worktrees/nutation-peel/.codex-run/nutation_hub_spoke_v5_hybrid_smoke4_trace_joined_summary.md` - Current kept Hybrid trace summary: `20` rows, `9` processKeep, `1` TraceOrderKeep / production keep, strict gap `localPatchRun+1`.
- `.worktrees/nutation-peel/.codex-run/nutation_hub_spoke_v5_hybrid_smoke5_trace_joined_summary.md` - Negative local/micro escalation run: `20` rows, `1` VisualKeep, `0` STS pass / `0` production keep.
- `.worktrees/nutation-peel/.codex-run/nutation_hub_spoke_v5_hybrid_smoke6_trace_joined_summary.md` - Negative recent-micro head-prior run: `20` rows, `4` processKeep, `0` STS pass / `0` production keep; code and assets were restored to smoke4 afterward.
- `.worktrees/nutation-peel/.codex-run/nutation_hub_v5_hybrid_review_with_v52_smoke4_summary.md` - Pool + LocalBreak + Hybrid smoke4 rerank: `60` rows, `0` HybridStrict, `10` near rows.
- `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationHubSpokeV5HybridSearch/` - Hub V5 Hybrid Search smoke1 diagnostic assets; `24/24` generated and traced, but `0` production keep.
- `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubSpokeV5HybridSearchPack.asset` - Full Search smoke1 pack, GUID `16fbc52dbee92724eae8efd25a7f41e9`; negative diagnostic only, not current Demo baseline.
- `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_spoke_v5_hybrid_search_report.csv` - Search smoke1 source report; coverage range around `0.881-0.907`.
- `.worktrees/nutation-peel/.codex-run/nutation_hub_spoke_v5_hybrid_search_smoke1_trace_joined.csv` and `_summary.md` - Search smoke1 joined audit: `24/24` solved, `9` processKeep, `0` production keep; local/directional/same-axis regressed versus smoke4.
- `.worktrees/nutation-peel/.codex-run/nutation_hub_v5_hybrid_review_with_search_smoke1_summary.md` - Pool + LocalBreak + smoke4 + Search rerank: `84` rows, `0` HybridStrict; smoke4 remains best same-row trace candidate.
- `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_spoke_v5_hybrid_production_keep.csv` - Current Hybrid 1-row review keep copied from smoke4 keep after smoke5 was rejected.
- `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubSpokeV5HybridProductionKeepPack.asset` - 1-row Hybrid smoke4 review pack, GUID `a2be47b7e6cfd6b488ae343b81e997b0`; worktree `Demo.unity` activePack points here.

## Campaign500 Long-Chain Pilot3 V14 Family Expansion - 2026-06-30

- `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Levels/Campaign500LongChainPilot3V14/` - Clean V14 family-expansion smoke assets. The selected rows prove `headmix_outerclean`, `seedlock_gate_carrier`, `seedmaze_chamber_corridor`, and `seedweave_braid_carrier` can all enter the pool.
- `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainPilot3V14Pack.asset` - Clean V14 selected pack from the filtered 3-slot family smoke; not mounted to Demo at this checkpoint.
- `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Reports/Campaign500/LongChain/campaign500_longchain_pilot3_v14_report.csv` - Clean V14 source report; selected `6/12`, coverage avg `0.9608`, chains avg `26.67`, maxChain avg `43.33`, longVisualCellShare avg `0.493`.
- `.worktrees/campaign500-longchain-pilot3/Assets/ArrowMagic/SOData/Reports/Campaign500/LongChain/campaign500_longchain_pilot3_v14_summary.md` - Clean V14 source summary.
- `.worktrees/campaign500-longchain-pilot3/.codex-run/campaign500_longchain_pilot3_v14_filter4_p3_trace_input.csv` - Trace input for the six selected V14 rows.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/campaign500_longchain_pilot3_v14_filter4_p3_metrics_metrics.csv` - Official trace metrics; `6/6 solved`, missing/failed `0`, process tiers `A=1 / B=3 / Drop=2`.

## Campaign500 Long-Chain Family Expand V2 - 2026-06-30

- `.worktrees/campaign500-longchain-family-expand-v2/.codex-run/campaign500_longchain_family_expand_v2_source_pool.csv` - V2 source-grammar pool, 120 rows / 12 lanes; no seed coordinate copy.
- `.worktrees/campaign500-longchain-family-expand-v2/.codex-run/campaign500_longchain_family_expand_v2_family_lanes.csv` - Lane count/metric summary for the 120-row source pool.
- `.worktrees/campaign500-longchain-family-expand-v2/.codex-run/campaign500_longchain_family_expand_v2_reverse_plan.csv` - Reverse Campaign500 plan, 50 sections / 200 rows / 4 candidates per section from normal-category slots.
- `.worktrees/campaign500-longchain-family-expand-v2/.codex-run/campaign500_longchain_family_expand_v2_reverse_plan_summary.md` - Plan summary: aspect `0.700-0.892`, role counts 50 each for LongNormalA/LongNormalB/LongHard/LongVeryHard.
- `.worktrees/campaign500-longchain-family-expand-v2/Assets/ArrowMagic/SOData/Reports/Campaign500/LongChain/campaign500_longchain_family_expand_v2_report.csv` - Current fixed Unity source report path; overwritten by each V2 batch, so durable copies live in `.codex-run`.
- `.worktrees/campaign500-longchain-family-expand-v2/.codex-run/campaign500_longchain_family_expand_v2_positive_vs_lowchoice_trace_compare.csv` - Source + official trace comparison for V2 reset audit; `16` selected rows, showing V14-style seed-source baseline `4 A / 1 B`, positive expanded lanes `1 A / 1 B / 3 Drop`, and lowchoice variants `6 Drop`.
- `.worktrees/campaign500-longchain-family-expand-v2/.codex-run/campaign500_longchain_family_expand_v2_goal_reset_execution_summary.md` - Short handoff for the reset execution, lowchoice negative, and stalled V2 Unity launch attempts.

## Nutation Hub/Maze Mixed Isolated - 2026-06-30

- `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationHubMixedV1/` - Hub mixed generated level assets from the latest isolated seed-retry run. Current source pack has all `36/36` logical slots built after per-slot seed retry; use joined CSV for keep selection rather than the full pack.
- `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubMixedV1Pack.asset` - Full Hub mixed pack from the isolated worktree; prototype/review, not strict production.
- `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubMixedV1ProductionKeepPack.asset` - 1-level Hub mixed TraceOrderKeep outer-band review pack built from smoke2 production keep CSV, GUID `7a3621e2f99f046449576dcecfd12941`; isolated worktree `Demo.unity` activePack points here.
- `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_mixed_v1_report.csv` - Latest Hub mixed source report from `seed36_retry_source1`: `36/36` built/portable-solved; retry distribution `1:27`, `2:7`, `3:1`, `4:1`.
- `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_mixed_v1_production_keep.csv` - Outer-band smoke2 1-row Hub mixed keep manifest feeding `NutationHubMixedV1ProductionKeepPack.asset`.
- `.worktrees/nutation-hub-maze-mixed/.codex-run/nutation_hub_mixed_v1_outerband_smoke2_trace_joined.csv` and `_summary.md` - Current official joined audit; `1` TraceOrderKeep row, coverage `0.953`, outer `0.949`, local/near `5/5`, STS/collapse `0.828/0.184`, sameAxis/sameDir `7/5`.
- `.worktrees/nutation-hub-maze-mixed/.codex-run/nutation_hub_mixed_v1_coverlift_smoke3_trace_joined.csv` and `_summary.md` - Previous comparison audit; `1` TraceOrderKeep row, coverage `0.940`, outer `0.903`, local/near `5/5`, STS/collapse `0.831/0.184`, sameAxis/sameDir `7/5`.
- `.worktrees/nutation-hub-maze-mixed/.codex-run/nutation_hub_mixed_v1_isolated_smoke5_trace_joined.csv` and `_summary.md` - Previous lower-coverage comparison; `2` TraceOrderKeep rows, best coverage `0.925/0.911`, sameAxis/sameDir `8/5`, residual stripe/local warnings.
- `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubMixedV1CandidateReviewPack.asset` - 12-level Hub mixed seed36 retry relaxed-trace review pack mounted in isolated Demo, GUID `046c506d72b03054bacaa7f592cb1f8d`; built from the current seed-retry TraceOrderKeep rows.
- `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_mixed_v1_candidate_review.csv` - 12-row candidate review manifest copied from `seed36_retry_fasttrace1_production_keep.csv`; all rows are `TraceOrderKeep` under relaxed same-axis/same-dir gates.
- `.worktrees/nutation-hub-maze-mixed/.codex-run/nutation_hub_mixed_v1_seed36_retry_fasttrace1_trace_joined.csv` and `_summary.md` - Current official trace joined audit with reduced counterfactual sampling for speed: `36/36` solved, `28` processKeep, `24` STS pass, `12` TraceOrderKeep / production keep; source yield is now `36/36`.
- `.worktrees/nutation-hub-maze-mixed/.codex-run/nutation_hub_mixed_v1_quality_probe_b14_trace1_joined.csv` and `_summary.md` - Minimal B14 seed-variant quality probe. Four source variants produced two buildable rows; official trace solved both, but r04 was `TraceOrderKeep` while r03 was only `ProcessKeep`, proving per-slot best-of selection can reduce local/collapse risk. Diagnostic only; Demo active pack was not changed.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/nutation_hub_mixed_v1_quality_probe_b14_trace1_metrics.csv` - Official fast trace metrics for the B14 quality probe (`2/2` solved).
- `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_mixed_v1_bestof_probe_report.csv` - Production-aligned B14 best-of probe source report. Current 6-retry run selected retry `4/6`, but only `1/6` retries source-succeeded, so it is a source-yield diagnostic rather than quality proof.
- `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubMixedV1BestOfProbePack.asset` - Diagnostic single-row best-of probe pack; not mounted to Demo and not production.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/nutation_hub_mixed_v1_bestof_probe_trace3_metrics.csv` - Official fast trace for the production-aligned B14 best-of probe; solved but Drop (`maxChoices=15`, `sameAxis=19`, STS `0.885`, collapse `0.272`).
- `.worktrees/nutation-hub-maze-mixed/.codex-run/nutation_hub_mixed_v1_seed36_unique_source_audit.csv` - Source audit for the 36-slot unique seed run: `27/36` generated and portable-solved; failures are source-generation yield/collapse, not official trace failure.
- `.worktrees/nutation-hub-maze-mixed/.codex-run/nutation_hub_mixed_v1_seed36_unique_relaxedtrace1_trace_joined.csv` and `_summary.md` - Official trace joined audit with reduced counterfactual sampling for speed: `27/27` solved, `23` processKeep, `18` STS pass, `11` TraceOrderKeep / production keep after relaxing `sameAxisRun` to `18` and `sameDirRun` to `14`.
- `.worktrees/nutation-hub-maze-mixed/.codex-run/nutation_hub_mixed_v1_prod10_aspect_fasttrace1_trace_joined.csv` and `_summary.md` - Previous fast trace audit for the old 10-row candidate pack: `12/12` solved, `9` processKeep, `2` VisualKeep, `0` TraceOrderKeep / production keep; blockers were same-axis/same-dir/local-collapse.
- `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationMazeMixedV1/` - Maze mixed generated level assets from smoke1.
- `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationMazeMixedV1Pack.asset` - Full Maze mixed diagnostic pack; not production-ready.
- `.worktrees/nutation-hub-maze-mixed/.codex-run/nutation_maze_mixed_v1_isolated_smoke1_trace_joined.csv` and `_summary.md` - Official joined audit; `0` production keep, all rows `local_collapse/high_risk`.

## Campaign500 Normal Pilot20 Nutation - 2026-06-30

- `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Levels/Campaign500NormalPilot20/` - Generated Pilot20 LevelDefinition assets, `20/20` rows from sections `1,5,15,30,45`.
- `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500NormalPilot20Pack.asset` - Source pack containing all 20 generated rows before trace quality selection.
- `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500NormalPilot20ReviewPack.asset` - 20-row visual review pack, GUID `553fb17c72739f1409149e1fc5c920ed`; worktree Demo activePack points here.
- `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500NormalPilot20ProductionKeepPack.asset` - 6-row strict production keep pack, GUID `8843a1f3452c91441b3a2bc17414f659`.
- `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Reports/Campaign500/NormalPilot20/campaign500_normal_pilot20_report.csv` - Source generation report; generated all 20 rows.
- `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Reports/Campaign500/NormalPilot20/campaign500_normal_pilot20_review.csv` - Review manifest feeding the 20-row ReviewPack.
- `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Reports/Campaign500/NormalPilot20/campaign500_normal_pilot20_production_keep.csv` - Production keep manifest; `6` TraceOrderKeep rows under current gates.
- `.worktrees/nutation-flow-peel-production/.codex-run/campaign500_normal_pilot20_v1b_trace_joined.csv` and `_summary.md` - Official joined audit; `20/20` solved, rank counts `TraceOrderKeep=6`, `VisualKeep=1`, `Reject=13`.
## Campaign500 Long-Chain Complement Pool V1 - 2026-06-30

- Worktree: `.worktrees/campaign500-longchain-complement-pool`, branch `codex/campaign500-longchain-complement-pool`.
- Complement purpose: supplement the separate `campaign500-longchain-prod200-pool` 200-candidate route with differentiated lock/support/runbreak/inner-carrier profiles.
- Generator output folder: `.worktrees/campaign500-longchain-complement-pool/Assets/ArrowMagic/SOData/Levels/Campaign500LongChainComplementPoolV1/`.
- Pack paths: `.worktrees/campaign500-longchain-complement-pool/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainComplementPoolV1Pack.asset` and `Campaign500LongChainComplementPoolV1ReviewPack.asset`.
- Report paths: `.worktrees/campaign500-longchain-complement-pool/Assets/ArrowMagic/SOData/Reports/Campaign500/LongChain/campaign500_longchain_complement_pool_v1_report.csv` and `_summary.md`.
- Handoff/checkpoint: `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_pool_v1_handoff.md`.
- Current validation is setup-only: PowerShell parser OK and plan-only s45-s50 generated; no official trace rows yet.

## Campaign500 Long-Chain Family Profile Lab - 2026-06-30

- Worktree: `.worktrees/campaign500-longchain-family-profile-lab`, branch `codex/campaign500-longchain-family-profile-lab`.
- Purpose: profile/family expansion only; this is not a final production pack.
- Handoff: `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_handoff.md`.
- Plan-only outputs: `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_planonly_s45_s50_plan.csv` and `_family_lanes.csv`; full plan check `campaign500_longchain_family_profile_lab_planonly_s01_s50_plan.csv` and `_family_lanes.csv`.
- Lane catalog is now `21` root lanes, adding `slot_seedlock_keyhole_carrier`, `slot_seedlock_staggered_gates`, `slot_seedmaze_broken_chambers`, `slot_inner_spine_branches`, and `slot_seedweave_support_lattice`.
- Valid short-path smoke assets: `.worktrees/campaign500-longchain-family-profile-lab/Assets/ArrowMagic/SOData/Levels/C5LCFamLabV1/`.
- Valid short-path source pack: `.worktrees/campaign500-longchain-family-profile-lab/Assets/ArrowMagic/SOData/Packs/Campaign500/C5LCFamLabV1Pack.asset`.
- Valid smoke summary: `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_s02_smoke2_shortpath_summary.md`; all-selected `10/10` solved and `6/10` trace-gate, postselected `4/4` solved and `3/4` trace-gate.
- Valid joined reports: `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_s02_smoke2_shortpath_allselected_trace_joined.csv` and `_postselected_trace_joined.csv`.
- Passing postselected lanes in this smoke: `slot_seedlock_keyhole_carrier`, `slot_seedweave_braid_carrier`, and `slot_seedweave_support_lattice`; `slot_seedlock_dual_gate_buckle` solved but failed choice gates.
- Ignore the earlier `campaign500_longchain_family_profile_lab_s02_smoke1_retry1` family-quality result; it used long asset names and hit Windows path-length trace misses.

## Campaign500 Long-Chain Complement 21-Family Merge Smoke - 2026-06-30

- `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_family21_merge_planonly_s01_s50_plan.csv` - Complement 21-lane full plan check; `200` rows and `21/21` unique planned root lanes.
- `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_family21_merge_planonly_s45_s50_plan.csv` - Late-window plan check; `24` rows for sections `45-50`, naturally covers `4/5` added lanes.
- `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_family21_s03_lock_smoke1_summary.md` - Lock-family smoke summary; `7` all-selected rows, `3/7` solved, `1/7` trace-gate. Keyhole/staggered generated and solved but failed choice gates.
- `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_family21_s03_lock_smoke1_allselected_trace_joined.csv` - Official joined rows for keyhole/staggered/cross-carrier smoke.
- `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_family21_s05_broken_smoke1_summary.md` - Broken/support smoke summary; `9` all-selected rows, `7/9` solved, `5/9` trace-gate. Support lattice passed; broken chambers generated but trace-missing.
- `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_family21_s05_broken_smoke1_allselected_trace_joined.csv` - Official joined rows for broken/support smoke.
- `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_family21_s43_partial_trace1_summary.md` - Partial late-window trace; `inner_spine_branches` solved but failed choice gates.

## Campaign500 Long-Chain Complement s50 Small Production - 2026-06-30

- `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_family21_s50_smallprod1_summary.md` - Completed one-section s50 complement small production. Source `5/5`, all-selected solved `1/5`, trace-gate `0/5`; postselected `1/1` solved but `0/1` gate. Treat as negative/diagnostic for high-section outer-ring and opener pressure, not production keep.
- `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_family21_s50_smallprod1_allselected_trace_joined.csv` - Official joined all-selected rows. `support_lattice` planned rows became near/fallback rows and failed trace; surviving solved spine-gate row rejected by `sourceOuterRing`.
- `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_family21_s50_smallprod1_postselected_trace_joined.csv` - Single postselected row, solved but rejected by `sourceOuterRing`.
- `.worktrees/campaign500-longchain-complement-pool/Assets/ArrowMagic/SOData/Reports/Campaign500/LongChain/campaign500_longchain_complement_pool_v1_report.csv` and `_summary.md` - Latest source report overwritten by `s50_smallprod1`; coverage avg `0.9417`, outerRingLineFill avg `0.873`, outerExitHeadCount avg `28.20`.
- `.worktrees/campaign500-longchain-complement-pool/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainComplementPoolV1Pack.asset` - Latest source pack from `s50_smallprod1`; diagnostic only, not a keep pack.

## Campaign500 Long-Chain Complement s50 Outerfix1 - 2026-06-30

- `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_family21_s50_outerfix1_summary.md` - Outerfix1 validation summary. Source selected `1/6`; all-selected `1/1` solved and `1/1` trace-gate; postselected `1/1` solved and `1/1` trace-gate.
- `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_family21_s50_outerfix1_allselected_trace_joined.csv` - Official joined all-selected row. Keep row is order `495`, `LongVeryHard`, `slot_seedlock_spine_gate`, coverage `0.9407`, outerRingLineFill/run `0.817/14`, outerExit heads/run/side `12/2/5`, choices avg/p80/max `10.87/14/17`.
- `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_family21_s50_outerfix1_postselected_trace_joined.csv` - Postselected exact trace joined row; same row, traceGate `True`.
- `.worktrees/campaign500-longchain-complement-pool/Assets/ArrowMagic/SOData/Reports/Campaign500/LongChain/campaign500_longchain_complement_pool_v1_report.csv` and `_summary.md` - Latest source report overwritten by `s50_outerfix1`; non-keep near rows are `selected=0` with `near_rejected=...`.
- `.worktrees/campaign500-longchain-complement-pool/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500LongChainComplementPoolV1Pack.asset` and `Campaign500LongChainComplementPoolV1ReviewPack.asset` - Latest source/review packs from `s50_outerfix1`; review pack contains only traceGate postselected rows after wrapper fix.

## Campaign500 Long-Chain Complement s50 Outerfix4 Diagnostic - 2026-06-30

- `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_family21_s50_outerfix4_plan.csv` - Section 50 plan used for the failed late/large outer-head diagnostic.
- `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_family21_s50_outerfix4_unity.log` - Unity log for the diagnostic run. Wrapper stopped before trace because no selected rows existed.
- `.worktrees/campaign500-longchain-complement-pool/Assets/ArrowMagic/SOData/Reports/Campaign500/LongChain/campaign500_longchain_complement_pool_v1_report.csv` - After outerfix4, overwritten source report contains `6` rejected rows and `0` selected. Treat as diagnostic only, not candidate supply.

## Campaign500 Long-Chain Complement s50 Outerfix5-8 Diagnostics - 2026-06-30

- `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_family21_s50_outerfix5_unity.log` - Direct section-50 smoke. It was stopped after the first two normal rows plus enough evidence; diagnostic only.
- `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_family21_s50_outerfix6_sourceprobe_unity.log` - One-row, one-merge source/crop probe before raising late source coverage; chamber order `491` crop coverage `0.8114`.
- `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_family21_s50_outerfix7_sourceprobe_unity.log` - One-row, one-merge source/crop probe after raising late source coverage/candidate count; chamber order `491` crop coverage improved to `0.8870` but still failed.
- `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_family21_s50_outerfix7_onefull_unity.log` - One-row full merge probe for chamber order `491`; chains/maxChain became acceptable (`87`/`68`) but coverage/openers/outer ring still failed.
- `.worktrees/campaign500-longchain-complement-pool/_CodexRun/campaign500_longchain_complement_family21_s50_outerfix8_sourceprobe_unity.log` - Lower source outer-band diagnostic; same chamber sourceprobe result as outerfix7, so not a candidate source.

## Campaign500 Long-Chain Family Profile Lab Target50 - 2026-06-30

- `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_target50_area_plan_v2.csv` - 50-row area-priority target plan, aspect `0.724-0.879`, avg `0.803`.
- `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_target50_area_v2_cut49_trace1_source_report.csv` and `_source_summary.md` - Preserved multi-lane partial source pool, `49/54` selected, avg coverage `0.9367`, avg chains `55.71`.
- `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_target50_area_v2_cut49_trace1_allselected_trace_joined.csv` - Official trace joined all-selected rows: `49/49` solved, `21/49` trace-gate.
- `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_target50_area_v2_cut49_trace1_postselected_trace_joined.csv` and `_postselected_trace_gate_keep.csv` - Postselected rows: `19/19` solved, `13/19` trace-gate; strongest lanes are keyhole, maze chamber, and dense support.
- `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_target50_singlelane_plan_v2.csv` - Corrective 50-row single-lane plan, one root lane per slot; lane mix `maze=19`, `keyhole=15`, `dense=6`, `support=8`, `dual_gate=2`.
- `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_target50_singlelane_v2_prod1_source_report.csv` and `_source_summary.md` - Completed singlelane source run: `45/50` selected, avg coverage `0.9419`, avg chains `65.84`, avg outerExitRunMax `2.44`.
- `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_target50_singlelane_v2_prod1_allselected_trace_joined.csv` and `_postselected_trace_joined.csv` - Completed singlelane official trace: `45/45` solved, `12/45` trace-gate. Main rejects are `choiceP80`, `maxChoices`, and `sourceOuterRing`.
- `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_target50_singlelane_v2_prod1_postselected_trace_gate_keep.csv` - 12 trace-gate rows from the completed singlelane run; useful as family evidence, not enough as a final 50 keep pool.
- `.worktrees/campaign500-longchain-family-profile-lab/Assets/ArrowMagic/SOData/Packs/Campaign500/C5LCFamLabV1ReviewPack.asset` - Mounted 12-row review pack from the singlelane trace-gate keep CSV; keep intact when testing new straight-spine language.
- `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_straight_spine_probe_plan_v1.csv` and `_summary.md` - Plan-only straight-spine primary probe (`13` rows, aspect `0.704-0.838`, area `391-1147`, planned chains `26-94`). No Unity assets or official trace yet; use isolated output args before generation.
- `.worktrees/campaign500-longchain-family-profile-lab/Assets/ArrowMagic/SOData/Packs/Campaign500/C5LCFamLabStraightProbeV1ReviewPack.asset` - Isolated straight-spine V1 gate-only review pack, rebuilt to `6` trace-gate rows. Useful for visual inspection, not production-ready straight-family evidence.
- `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_straight_spine_probe_v1_gen1_postselected_trace_joined.csv` and `_postselected_trace_gate_keep.csv` - V1 official trace joined/keep rows; `13/13` solved, `6/13` trace-gate.
- `.worktrees/campaign500-longchain-family-profile-lab/Assets/ArrowMagic/SOData/Packs/Campaign500/C5LCFamLabStraightProbeV2ReviewPack.asset` - Isolated straight-spine V2 gate-only review pack, `4` trace-gate rows. V2 improves choice pressure versus V1 but has lower yield and still does not improve straightness.
- `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_straight_spine_probe_v2_gen1_postselected_trace_joined.csv` and `_postselected_trace_gate_keep.csv` - V2 official trace joined/keep rows; `11/11` solved, `4/11` trace-gate.
- `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_candidate_pool_v1_plus_straight_v1_18.csv` and `_summary.md` - Main family-lab candidate pool after review: old 12 gate rows plus straight V1 6 rows tagged `light_clean_carrier`; `18` rows total. Treat straight V1 as differentiated light/clean flavor, not proven straight-chain family.
- `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_reserve_cut49_extra5_same_family.csv` and `_summary.md` - Reserve-only same-family hard/veryhard variants from cut49; `5` rows, not a new structure breakthrough.

## Campaign500 Long-Chain Family Profile Lab Inner-Straight Probe - 2026-06-30

- `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_inner_straight_probe_plan_v1.csv` and `_summary.md` - 8-row explicit `slot_inner_straight_carrier` probe plan; aspect `0.727-0.833`, area avg `770.4`, roles `LongNormalA=4`, `LongHard=2`, `LongVeryHard=2`.
- `.worktrees/campaign500-longchain-family-profile-lab/Assets/ArrowMagic/SOData/Levels/C5LCFamLabInnerStraightProbeV1/` - V1 generated LevelDefinition assets. Official trace `8/8` solved, `2/8` trace-gate.
- `.worktrees/campaign500-longchain-family-profile-lab/Assets/ArrowMagic/SOData/Packs/Campaign500/C5LCFamLabInnerStraightProbeV1ReviewPack.asset` - V1 gate-only review/proof pack, GUID `06bd1a1c4a1405a46b285ce4360bb630`; contains 2 trace-gate rows. Not mounted to Demo by default.
- `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_inner_straight_probe_v1_gen1_postselected_trace_joined.csv` and `_postselected_trace_gate_keep.csv` - V1 official joined/keep rows; all rows process `Drop`, rejects dominated by `choiceP80|maxChoices`.
- `.worktrees/campaign500-longchain-family-profile-lab/Assets/ArrowMagic/SOData/Levels/C5LCFamLabInnerStraightProbeV2/` - V2 generated LevelDefinition assets after opener/outer-head pressure retune. Official trace stayed `8/8` solved and `2/8` trace-gate.
- `.worktrees/campaign500-longchain-family-profile-lab/Assets/ArrowMagic/SOData/Packs/Campaign500/C5LCFamLabInnerStraightProbeV2ReviewPack.asset` - V2 gate-only review/proof pack, GUID `815b64309dce83d47864c2816433bb15`; passing rows are identical to V1. Not production-ready.
- `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_inner_straight_probe_v1_shape_summary.csv` - Shape audit: old12 avg maxStraightRun `7.33`, straight V1/V2 `6.33/6.25`, inner all8 `14.0`, inner gate2 `13.0`. Use this as the straight-language proof and the choice-pressure caveat.
- `.worktrees/campaign500-longchain-family-profile-lab/_CodexRun/campaign500_longchain_family_profile_lab_inner_straight_probe_v1_v2_compare_summary.csv` and `_shape_rows.csv` - V1/V2 comparison; V2 did not improve gate yield, so avoid further scalar score nudges before adding solve-order/low-choice scheduling.

## Nutation LongChain Strict50 / Holefix - 2026-07-01

- `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_longchain_strict50.csv` - Final 50-row strict candidate manifest. Selected from 320 audited rows across Candidate80 + Holefix S1/S2/S3; `StrictA=35`, `StrictB=15`.
- `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationLongChainStrict50Pack.asset` - Mounted Demo review pack, `50` levels, GUID `46de8f0720f60854ba75a7a8615864e1`.
- `.worktrees/nutation-peel/.codex-run/nutation_longchain_strict50_summary.md` and `_audit.csv` - Strict50 selection summary/audit. Key averages: coverage `0.9471`, maxChoices `8.42`, STS `0.8791`, collapse `0.1767`, maxHole avg/max `4.86/9`.
- `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationLongChainCandidate80HolefixV1/`, `...HolefixS2V1/`, `...HolefixS3V1/` - Holefix source candidate assets. Each batch has 80 generated LevelDefinition assets with independent seed offsets.
- `.worktrees/nutation-peel/.codex-run/nutation_longchain_candidate80_holefix_v1_trace_joined.csv`, `_holefix_s2_v1_trace_joined.csv`, `_holefix_s3_v1_trace_joined.csv` - Fast official trace joined inputs used by Strict50 selector.
- `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_longchain_strict_reserve10.csv` and `NutationLongChainStrictReserve10Pack.asset` - Next-best 10 strict rows as backup candidates. Demo is mounted to this pack for reserve review; GUID `b512f2a2510148cda13496fdbf7b0b11`.
- `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_longchain_strict60.csv` and `NutationLongChainStrict60Pack.asset` - Combined `50 + 10 reserve` pool for handoff/production bookkeeping.
- `.worktrees/nutation-peel/.codex-run/nutation_longchain_strict60_sync_handoff.md` - Detailed LongChain sync handoff for other Codex/GPT conversations, including Strict50, Reserve10, Strict60 paths, GUIDs, gates, metrics, and current Demo mount state.
## Nutation HubMixed Strict30 Transform Wide Pack - 2026-07-01

- Worktree: `.worktrees/nutation-hub-maze-mixed`, branch `codex/nutation-hub-maze-mixed`.
- Final pack: `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubMixedV1Strict30TransformWideProductionKeepPack.asset`; Demo in that worktree currently references this pack.
- Level assets: `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationHubMixedV1Strict30TransformWidePool/`.
- Candidate source report: `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_mixed_v1_strict30_transform_wide_pool_report.csv`; 144/144 portable solved, coverage `0.925-0.967`.
- Production keep CSV: `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_mixed_v1_strict30_transform_wide_production_keep.csv`; 30 rows, all `visualPass=True`, `keepCandidate=True`, `stsKeepCandidate=True`.
- Official trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/nutation_hub_mixed_v1_strict30_transform_wide_visual1_metrics.csv`.
- Joined audit: `.worktrees/nutation-hub-maze-mixed/.codex-run/nutation_hub_mixed_v1_strict30_transform_wide_visual1_trace_joined.csv`; summary `.worktrees/nutation-hub-maze-mixed/.codex-run/nutation_hub_mixed_v1_strict30_transform_wide_visual1_trace_joined_summary.md`.
- Gate note: `VisualOnly` with `MaxLocalPatchSolveRun=10`; pure `localPatchRun<=8` only gave 24/144 VisualKeep and did not meet the 30-candidate goal.
## Campaign500 Normal Full V1 Strict152 - 2026-06-30

- `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Reports/Campaign500/NormalFullV1/campaign500_normal_full_v1_production_strict_keep.csv` - final strict manifest, `152` rows, `152` unique target orders, all `TraceOrderKeep`.
- `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Reports/Campaign500/NormalFullV1/campaign500_normal_full_v1_production_keep.csv` - mirrored strict production keep manifest, also `152` rows, all `TraceOrderKeep`.
- `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Reports/Campaign500/NormalFullV1/campaign500_normal_full_v1_review.csv` - review manifest for visual pass, also `152` strict rows.
- `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500NormalFullV1ReviewPack.asset` - final visual review pack, display name `Campaign500 Normal full_v1_strict152 Review (152)`, mounted in the worktree Demo.
- `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500NormalFullV1ProductionKeepPack.asset` - production keep pack, `152` levels.
- `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500NormalFullV1ProductionStrictKeepPack.asset` - strict production keep pack, `152` levels.
- Main project copies live at the same `Assets/ArrowMagic/SOData/Reports/Campaign500/NormalFullV1/` and `Assets/ArrowMagic/SOData/Packs/Campaign500/` relative paths; main `Assets/ArrowMagic/Scenes/Demo.unity` points to `Campaign500NormalFullV1ReviewPack.asset`.

## Nutation HubMixed Strict30 Refill Pack - 2026-07-01

- Worktree: `.worktrees/nutation-hub-maze-mixed`, branch `codex/nutation-hub-maze-mixed`.
- Final refill pack: `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubMixedV1Strict30RefillProductionKeepPack.asset`; Demo in that worktree references this pack, GUID `da8a0f3e4d71f4f41a3a1ac875059c77`.
- Refill level assets: `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Levels/DirectProcedural/NutationHubMixedV1Strict30RefillPool/`.
- Refill pool report: `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_mixed_v1_strict30_refill_pool_report.csv`; `120` rows, `4` refill variants for each original strict30 source.
- Shortlist report: `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_mixed_v1_strict30_refill_shortlist_report.csv`; `60` rows, `2` static-best variants per original source.
- Final keep CSV: `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_mixed_v1_strict30_refill_production_keep.csv`; `30/30` solved, visualPass, and stsKeepCandidate under local9 postselection.
- Trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/nutation_hub_mixed_v1_strict30_refill_shortlist1_metrics.csv`; joined summary `.worktrees/nutation-hub-maze-mixed/.codex-run/nutation_hub_mixed_v1_strict30_refill_shortlist1_local9_summary.md`.
- Result summary: coverage avg `0.961`, max empty component avg `5.0`, outer empty cells avg `6.033`, localPatch avg/max `7.8/9`.

## Campaign500 HardGate Until0910 - 2026-07-01

- `Exports/Campaign500_HardGateUntil0910_20260701_0910/` - Final-prep delivery folder for this timeboxed run. Contains the no-quality-drop 500-row manifest, 145 replacements, 55 missing target slots, 28 excluded legacy rows, section summary, GUID replacement audit, pack manifest, and README.
- `.codex-run/campaign500_hardgate_until0910_v1.csv` - Current hard-gate normal replacement truth: `145` rows / `145` unique orders / `0` failures under the latest strict gate.
- `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500NormalHardGateUntil0910V1ReviewPack.asset` - 145-level normal candidate review pack.
- `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardGateUntil0910V1FinalPreviewPack.asset` - 500-level preview pack built from `Campaign500FinalPack.asset` plus the 145 hard-gate replacements; GUID `aa230c66e2734d8d9f4ceeb6bd7c079e`; main `Demo.unity` points here.
- `Exports/Campaign500_StrictUntil0910_20260701_0910/` and `.codex-run/campaign500_strict_until0910_v1.csv` - Audit-only 173-row timebox manifest. Do not treat as no-quality-drop production surface because 28 legacy TraceOrderKeep rows fail the current hard gate.

## Campaign500 First Assembly V1 - 2026-07-01

- `Exports/Campaign500_FirstAssembly_20260701_NutationSyncV1/` - CSV/README planning surface for the first integrated Campaign500 assembly after candidate sync. It does not copy external assets or build a Unity pack.
- `Exports/Campaign500_FirstAssembly_20260701_NutationSyncV1/campaign500_first_assembly_v1_500_manifest.csv` - 500-row planned manifest with `assembly*` source/gate/copy/gap columns.
- `Exports/Campaign500_FirstAssembly_20260701_NutationSyncV1/campaign500_first_assembly_v1_replacements.csv` - `286` active planned replacements with source labels. Order `62` is filled by an exact NormalFullV1 v01 variant that passes current Campaign hard gate.
- `Exports/Campaign500_FirstAssembly_20260701_NutationSyncV1/campaign500_first_assembly_v1_remaining_gaps.csv` - `14` remaining gaps: `13` ordinary normal target slots and shape `073` windmill fallback.
- `Exports/Campaign500_FirstAssembly_20260701_NutationSyncV1/campaign500_first_assembly_v1_gap_demand_by_lane.csv` - Lane-level demand for the next candidate sync pass.
- `Exports/Campaign500_FirstAssembly_20260701_NutationSyncV1/campaign500_first_assembly_v1_candidate_sync_request.md` - Human-readable request for the other production conversation: prioritize front300 ordinary NormalA/NormalB gaps; no Hard/Peak gap remains.
- `Exports/Campaign500_FirstAssembly_20260701_NutationSyncV1/campaign500_first_assembly_v1_external_asset_copy_queue.csv` - `140` rows that reference assets currently living in worktrees; copy/import these before building a playable pack.
- `Exports/Campaign500_FirstAssembly_20260701_NutationSyncV1/campaign500_first_assembly_v1_hard_peak_ramp.csv` - Hard/Peak source and metric audit for checking stage difficulty ramp.

## Campaign500 Front300 Priority V3 - 2026-07-01

- `Exports/Campaign500_FirstAssembly_20260701_Front300PriorityV3/` - Front300-first Campaign500 assembly folder. Use this version when reviewing the current first-300 quality pass; V1 is superseded for front300 normal placement.
- `Exports/Campaign500_FirstAssembly_20260701_Front300PriorityV3/campaign500_first_assembly_front300_priority_v3_500_manifest.csv` - 500-row V3 manifest. Front300 `category=normal` rows are `210/210` active replacements; all 145 `Campaign500_HardGateUntil0910` candidates are placed in front300.
- `Exports/Campaign500_FirstAssembly_20260701_Front300PriorityV3/campaign500_first_assembly_front300_priority_v3_front300_normal_plan.csv` - Front300 normal assignment table with source pool, source class, STS, local/choice, and old/new level ids.
- `Exports/Campaign500_FirstAssembly_20260701_Front300PriorityV3/campaign500_first_assembly_front300_priority_v3_after300_deferred.csv` - 47 after300 hard-gate placements reclaimed for front300 and reverted to original for now.
- `Exports/Campaign500_FirstAssembly_20260701_Front300PriorityV3/campaign500_first_assembly_front300_priority_v3_pack_build_report.csv` - Unity pack build report; confirms `210/210` front300 normal rows loaded as manifest assets and not fallback.
- `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500FirstAssemblyFront300PriorityV3PreviewPack.asset` - Current playable 500-level V3 preview pack, GUID `824de2923fd048c48ae83f881c760b4d`; main `Demo.unity` activePack points here.

## Campaign500 Rhythm V4 Placement - 2026-07-01

- `Exports/Campaign500_FirstAssembly_20260701_RhythmV4/` - Current rhythm-aware Campaign500 assembly folder. Use this when reviewing the version that keeps order 1 and rebalances front300 normal by template chain ramp, 10-level waves, long-chain effective load, and style/chain-language diversity.
- `Exports/Campaign500_FirstAssembly_20260701_RhythmV4/campaign500_first_assembly_rhythm_v4_500_manifest.csv` - 500-row V4 manifest. Front300 normal has `209` active replacements plus order `1` keep-current/base fallback.
- `Exports/Campaign500_FirstAssembly_20260701_RhythmV4/campaign500_first_assembly_rhythm_v4_front300_normal_plan.csv` - Slot-level V4 normal assignment table with stage target chains, actual chains, effective load, style group, chain language, and placement reason.
- `Exports/Campaign500_FirstAssembly_20260701_RhythmV4/campaign500_first_assembly_rhythm_v4_section10_profile.csv` - Section-by-section rhythm audit for target chain avg/min/max, actual chain avg/min/max, wave roles, style mix, and chain mix.
- `Exports/Campaign500_FirstAssembly_20260701_RhythmV4/campaign500_first_assembly_rhythm_v4_front20_compare.csv` - Front20 audit showing order `1` keep-current and softened low-load replacements for the remaining early normal rows.
- `Exports/Campaign500_FirstAssembly_20260701_RhythmV4/campaign500_first_assembly_rhythm_v4_pack_build_report.csv` - Unity pack build report; total load modes are `333` manifest assets and `167` base-preview fallbacks. Front300 normal is `209` manifest assets + order `1` fallback.
- `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500FirstAssemblyRhythmV4PreviewPack.asset` - Current playable 500-level V4 preview pack, GUID `b63fbf4040cd570418f60950bc21525a`; main `Demo.unity` activePack points here.

## Campaign500 Rhythm V4 Final - 2026-07-01

- `Exports/Campaign500_FirstAssembly_20260701_RhythmV4Final/` - Current final Campaign500 assembly folder after the order-19 benchmark adjustment.
- `Exports/Campaign500_FirstAssembly_20260701_RhythmV4Final/campaign500_first_assembly_rhythm_v4_final_500_manifest.csv` - 500-row final manifest. Same Rhythm V4 placement philosophy, with order `19` upshifted to a `79`-chain `strict_peak_chain` candidate and order `1` kept current.
- `Exports/Campaign500_FirstAssembly_20260701_RhythmV4Final/campaign500_first_assembly_rhythm_v4_final_front20_compare.csv` - Front20 final audit; order `19` is the front20 benchmark row.
- `Exports/Campaign500_FirstAssembly_20260701_RhythmV4Final/campaign500_first_assembly_rhythm_v4_final_front300_normal_plan.csv` - Final front300 assignment plan.
- `Exports/Campaign500_FirstAssembly_20260701_RhythmV4Final/campaign500_first_assembly_rhythm_v4_final_pack_build_report.csv` - Unity final pack build report.
- `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500FirstAssemblyRhythmV4FinalPreviewPack.asset` - Current final playable 500-level preview pack, GUID `6314e181422e089488110d180f31b44f`; main `Demo.unity` activePack points here.
- `Exports/C5V4F/` - Short-path sync/import bundle for other projects. `U/Assets/...` contains the final pack and all actual LevelDefinition config assets referenced by the pack, with file `.meta`.
- `Exports/C5V4F/Docs/campaign500_rhythm_v4_final_per_level_config_index.csv` - 500-row per-level config map; use this to see the exact `.asset` configuration file used by each order.
- `Exports/C5V4F/Docs/campaign500_rhythm_v4_final_template_replacement_diff.csv` - 500-row old-vs-final replacement map with explicit replacement marks and source/metric fields.
- `Exports/C5V4F.zip` - Zipped import bundle for transfer.
## Campaign500 Rhythm V4 Final StrictComplete - 2026-07-01

- `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500FirstAssemblyRhythmV4FinalStrictCompletePreviewPack.asset` - StrictComplete playable 500-level preview pack for C5V4F gapfill; GUID `719ae0a2f039476da86ceca58b1e7d8d`; main Demo points here.
- `Exports/Campaign500_FirstAssembly_20260701_RhythmV4FinalStrictComplete/campaign500_first_assembly_rhythm_v4_final_strict_complete_500_manifest.csv` - 500-row final manifest after filling all normal target gaps and after300 deferred normal rows.
- `Exports/Campaign500_FirstAssembly_20260701_RhythmV4FinalStrictComplete/campaign500_first_assembly_rhythm_v4_final_strict_complete_55_patch_audit.csv` - 55-row patch audit for the strict-complete fill; all rows pass the strict gate.
- `Exports/C5V4FSC/` - Short-path sync/import bundle for the strict-complete version. `U/Assets/...` contains the final pack and all referenced LevelDefinition assets plus `.meta` files.
- `Exports/C5V4FSC/Docs/campaign500_rhythm_v4_final_strict_complete_per_level_config_index.csv` - 500-row per-level config map for the strict-complete package.
- `Exports/C5V4FSC/Docs/campaign500_rhythm_v4_final_strict_complete_template_replacement_diff.csv` - 500-row old-vs-strict-complete replacement map; `KEPT_TARGET_GAP=0` and after300 deferred markers are cleared.
- `Exports/C5V4FSC.zip` - Zipped strict-complete import bundle for transfer.

## Generated-Root WBP Human-Read Difficulty Reports - 2026-07-01

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t112c_human_read_difficulty_v1.csv` and `_summary.md` - Human-read difficulty audit for the current t112c Demo candidate. Result: `readSearchPressureV1=0.974`, `humanReadDifficultyV1=0.942`, class `ReadHardSkeleton_LocalConveyorRisk`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t111i_human_read_difficulty_v1.csv` and `_summary.md` - Baseline comparison for t111i. Result: `readSearchPressureV1=0.942`, `humanReadDifficultyV1=0.933`, class `ReadHard`.

## Generated-Root WBP t113 Endpoint Bundle Resources - 2026-07-01

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t113_generated_root_wbp_endpoint_bundle_summary.md` - t113 endpoint bundle conclusion. Best candidate r004 repairs t112c local/near-outer risk from `11/11` to `7/5` while preserving `0.9534413` coverage and choke/read pressure.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t113g_endpoint_bundle_from_t111h_final095_manifest.csv` - Four legal final two-chain closure bundles from t111h, with proxy/bundle scores and fill cells.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t113g_endpoint_bundle_trace1_metrics.csv` - Official trace for all four t113g endpoint bundles; 4/4 solved.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t113g_endpoint_bundle_human_read_difficulty_v1.csv` - Human-read audit for all four endpoint bundles; r002/r004 classify as `ReadHard`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t113_endpoint_bundle_from_t111h/t113g_endpoint_bundle_from_t111h_final095_r004.asset` - Selected t113g endpoint repair candidate.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GeneratedRootWBPV12_t113g_r004_DemoPack.asset` - Single-level Demo pack mounted in the sgp-rhythm-lab Demo scene; GUID `f930ff42078543c3b074496065cd4ecb`.

## Generated-Root WBP t114 Generalization Probe Resources - 2026-07-01

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t114_generated_root_wbp_generalization_probe_summary.md` - Main t114 conclusion: non-t110 roots can preserve generated-root identity, materialize semantic chains, fill to `0.90+`, and official-trace solved; final `0.95 + A/Hard` remains unsolved.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t114_generalization_probe_trace_metrics.csv` - Official trace for t114 representatives; `3/3` solved, all process/tight `B/B`, official `LocalEasy`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t114_generalization_probe_human_read_difficulty_v1.csv` - Human-read audit for t114; all representatives classify as `SkeletonHard`, best `t104d_c001_fill095_open` has `humanReadDifficultyV1=0.779`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t114_generalization_probe_relation_audit_summary.md` - Relation audit for t114 official traces; planned semantic owners appear in the graph, but support closure remains shallow/local-polluted.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t114_generalization_probe/` - t114 generated LevelDefinition assets, including `t114_generalization_probe_t104d_c001_fill095_open.asset` at coverage `0.9311741`.

## Generated-Root WBP t115 Scheduled Bundle Fill Resources - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t115_scheduled_bundle_fill_summary.md` - Main t115 conclusion: bundle fill improves choice rhythm/collapse on t111 full-coverage samples but still remains official `B/LocalEasy`; support closure remains shallow.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t115_scheduled_bundle_trace_metrics.csv` - Official trace for t115 bundle samples; `4/4` solved, all process/tight `B/B`, all `LocalEasy`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t115_scheduled_bundle_human_read_difficulty_v1.csv` - Human-read audit; best below-cover sample `t115e` scores `0.941`, best full-cover balanced sample `t115c` scores `0.919`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t115_scheduled_bundle_relation_audit_summary.md` - Relation audit; support depth still `2`, anti-locality around `0.14-0.17`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t115_scheduled_bundle_fill/` - t115 generated LevelDefinition assets, including `t115c_single_close_from_t115a_final095.asset` and `t115f_single_close_from_t115e_final095.asset` at coverage `0.9534413`.

## SGP Read-Demand Drain-Break Review Pack - 2026-07-02

- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureReadDemandV1ChokeMutationV2ReviewPack.asset` - Current 8-level remote-drain review pack mounted in the `read-demand-hardening` Demo scene.
- `.worktrees/read-demand-hardening/.codex-run/sgp_pressure_read_demand_v1_choke_mutation_v2_trace1_review_v1_review_keep.csv` - Review CSV used by the hardcoded V2 review-pack builder. Selected only candidates with official `frontierDrainRemoteChokeCount>0`.
- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_read_demand_v1_choke_mutation_v2_review_report.csv` - Review-pack build report; current rows are `sgp_rdcm_v2_s02_07_c13`, `sgp_rdcm_v2_s01_01_c46`, `sgp_rdcm_v2_s02_01_c16`, `sgp_rdcm_v2_s02_03_c3`, `sgp_rdcm_v2_s01_03_c27`, `sgp_rdcm_v2_s01_02_c22`, `sgp_rdcm_v2_s03_09_c5`, and `sgp_rdcm_v2_s03_05_t42_c5`.
- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_read_demand_v1_choke_mutation_v2_report.csv` - V2 source generation report. New columns include `SourceDrainRemote`, `MutationDrainRemote`, `MutationDrainScore`, `MutationDrainBestJump`, and `MutationDrainPattern`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_read_demand_v1_choke_mutation_v2_drainbreak2_metrics.csv` - Official trace for the 35-row drain-break V2 generation; `35/35` solved, `8/35` have `frontierDrainRemoteChokeCount>0`.
- `.worktrees/read-demand-hardening/.codex-run/sgp_pressure_read_demand_v1_choke_mutation_v2_drainbreak2_review_v1_summary.md` - Standard rhythm review summary for the same generation. Useful for old choice-rhythm comparison, but the remote-drain review pack above is the user-targeted selection.

## Generated-Root WBP t117 Support-Closure Bridge Resources - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t117_support_closure_bridge_summary.md` - Main t117 conclusion: support bridge can be officially realized before late fill, but high-coverage closure currently steals the bridge->victim contract.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t117g_support_bridge_trace_metrics.csv` and `t117g_support_bridge_relation_audit_summary.md` - Official low-coverage bridge proof; `2/2` solved, support depth `3`, relation contains `58->68->72->28`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t117m2_bridge_0945_trace_metrics.csv` / `_relation_audit_summary.md` - High-ish coverage `0.9453441` continuation; solved and human-read `ReadHard`, but official support depth already drops back to `2`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t117n_bridge_final095_trace_metrics.csv` / `_relation_audit_summary.md` / `_human_read_difficulty_v1_summary.md` - Full coverage continuation at `0.9534413`; official solved and human-read `ReadHard`, but still `B/LocalEasy`, support depth `2`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t117_support_closure_bridge/` - t117 bridge and closure LevelDefinition assets, including `t117g_bridgevictim_from_t111f_owner68_r001.asset`, `t117m2_bridge_bundle_resample095.asset`, and `t117n_bridge_resample_single_close095_from_m2.asset`.

## Generated-Root WBP t118 Contract-Preserving Closure Probe - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t118_contract_preserving_closure_probe_summary.md` - t118 route feasibility conclusion: current t117 late-closure branch cannot currently satisfy `0.95+` and official `58->68->72->28` simultaneously.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t118_contract_close_probe/t118b_contract_ex57_close_s118201.asset` - Best contract-preserved intermediate, coverage `0.9453441`, official solved, support depth `3`, keeps `72->28`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t118b_contract_ex57_mid0945_metrics.csv` / `_relation_audit_summary.md` - Official validation for the `0.9453441` contract-preserved intermediate.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t118j_near949_contract_metrics.csv` / `_relation_audit_summary.md` - Near-miss `0.9493927` official traces; solved but assigns `2->28`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t118o_tailbundle_from_t117m2_metrics.csv` / `_relation_audit_summary.md` - Six `0.953+` official-solved continuations from t117m2; all support depth `2`, all assign `2->28`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t118_contract_close_probe/` - t118 deterministic closure probe assets; diagnostic only, not final accepted levels.

## Generated-Root WBP t119 Walkthrough Probe - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t119_walkthrough_probe_summary.md` - t119 conclusion: current support-bridge route can walk through `0.95+` coverage and preserve official `58->68->72->28`, but still fails A/Hard due local conveyor.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t119_walkthrough_probe/t119g_ex57_longer_from_t117g_s119301.asset` - Contract-preserved near-miss at coverage `0.9493927`; official solved; no legal new-chain/two-chain tail closure.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t119j_extend1_from_t119g_s119301_manifest.csv` - One-cell extension candidate manifest. All rows are `0.9514170` Greedy-solvable extension assets generated from `t119g`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t119j_extend1_from_t119g_s119301_trace_metrics.csv` - Official trace for t119j; `12/12` solved, all `B/LocalEasy`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t119j_extend1_from_t119g_s119301_relation_audit_summary.md` - Relation audit; `11/12` keep support depth `3` and `58->68->72->28`. Bad row is extension of chain `72`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t119_walkthrough_probe/t119j_extend1_from_t119g_c27_3_10.asset` - Representative good walk-through candidate, coverage `0.9514170`, official solved, support depth `3`, `frontierDrainRemoteChokeCount=4`; not final difficulty acceptance.

## Generated-Root WBP t120 Local-Run Guard Probe - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t120_t119j_c27_local_follow_attribution_summary.md` - Official-step attribution for the t119j representative's local conveyor; longest run is `16` local follows around steps `48-64`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t120c_local_guard_from_t119j_c27_manifest.csv` / `_trace_metrics.csv` / `_relation_audit_summary.md` - Pure added 2-cell guard diagnostic. Solved and reduces localPatch to `7`, but loses bridge-victim contract; use as partial positive/negative boundary.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t120e_reroute57_guard_from_t119j_c27_manifest.csv` / `_trace_metrics.csv` / `_relation_audit_summary.md` - Minimal reroute diagnostic. Three official-solved variants preserve `58->68->72->28`, support depth `3`, and reduce localPatch to `9`; still `B/LocalEasy`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t120_local_guard_probe/` - t120 generated diagnostic LevelDefinition assets, including `t120e_reroute57_guard_from_t119j_c27_short57_2_guard3_c.asset`.

## Generated-Root WBP t121 Tail-Trim Guard Probe - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t121_tail_trim_guard_probe_summary.md` - Main t121 conclusion: the tested late one-guard/tail-trim space cannot both reduce localRun below `9` and preserve support depth `3`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t121b_tailtrim_guard_from_t120e_manifest.csv` - 24 candidate assets generated from the t120e best board.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t121b_tailtrim_guard_from_t120e_rep_trace_metrics.csv` and `t121b_tailtrim_guard_from_t120e_remaining_trace_metrics.csv` - Official metrics covering all t121b rows; best localRun rows are `7/depth2`, best bridge rows remain `9/depth3`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t121c_trim55_guard3_from_t120e_trace_metrics.csv` / `t121d_delay72_from_r018_trace_metrics.csv` - Targeted repair checks for the localRun/bridge conflict.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t121_tail_trim_guard_probe/` - t121 diagnostic LevelDefinition assets. Do not treat as accepted production levels.

## Generated-Root WBP t122 Contract Guard Probe - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t122_contract_guard_probe_summary.md` - t122 conclusion: the route can preserve generated-root contract at `0.95+` coverage and official solve, but remains `B/LocalEasy` due to local/near-outer run `7`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t122_contract_guard_probe/t122a_t119g_tailtrim_guard_r019_trim57x1_t20.asset` - t122a representative seed for second guard work; coverage `0.9534413`, official solved, support depth `3`, localRun `8`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t122_contract_guard_probe/t122b_from_r019_second_guard_r004_trim27x2_t39.asset` - Best current candidate: coverage `0.9574899`, chains `82`, official solved, support root `58`, support depth `3`, contract `58->68->72->28`, localRun `7`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t122_contract_guard_probe/t122b_from_r019_second_guard_r010_trim27x2_t39.asset` - Alternate best candidate: coverage `0.9554656`, chains `82`, official solved, support root `58`, support depth `3`, contract `58->68->72->28`, localRun `7`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t122_contract_guard_probe/t122b_from_r019_second_guard_r001_trim49x2_t10.asset` - Useful contrast: localRun `7`, official solved, but support depth drops to `2` because `28` is unlocked by `2->28`.

## Generated-Root WBP t123 Run-Breaker/Shape-Recut Probe - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t123_right_run_breaker_probe_summary.md` - t123 conclusion: route improves to coverage `0.9595`, official solved, support depth `4`, localRun `5`, but still `B/LocalEasy`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t123_right_run_breaker_probe/t123a_r004_right_run_breaker_r010_trim40x3_t47.asset` - Right near-outer run breaker from t122b r004; official local/nearOuter `6/5`, support depth `3`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t123_right_run_breaker_probe/t123b_from_r010_left_run_breaker_r016_trim40x1_t31.asset` - Root interposer candidate; official relation becomes `58->83->68->72->28`, support depth `4`, local/nearOuter `6/5`, coverage `0.9595142`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t123_right_run_breaker_probe/t123d_trim31_recut_r002_trim31x3_orig.asset` - Best current t123 candidate: coverage `0.9595142`, official solved, support depth `4`, support score `0.874`, local/nearOuter `5/5`, antiLocal `0.198`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t123_right_run_breaker_probe/` - t123 diagnostic assets. Not final accepted production levels.

## Generated-Root WBP t124 Shape-Recut Walkthrough - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t124_right_shape_recut_probe/t124a_right_run_shape_recut_r001_trim09x2_rev.asset` - First t124 positive: coverage `0.9595142`, official solved, local/nearOuter `5/4`, antiLocal `0.244`, support depth `4`, relation `58->85->83->68->72->28`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t124_right_shape_recut_probe/t124b_right_run_shape_recut2_r006_trim21x4_orig.asset` - Current valid t124 basis: coverage `0.9595142`, chains `87`, official solved, local/nearOuter `5/4`, antiLocal `0.265`, CUD p20 `6.25`, support depth `4`, support score `0.944`, relation `58->85->83->68->72->28`, human-read `ReadHard`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t124_right_shape_recut_probe/` - t124 diagnostic shape-recut/relocation assets. Not final accepted production levels.

## Generated-Root WBP t125-t126 Early Reservation Probe - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t125a_t119g_right_run_shape_relocate_summary.md` / `_trace_metrics.csv` / `_relation_audit_summary.md` - Early relocation from `t119g` can move shape into `0,3;0,4`, but that consumes the later contract slot and loses `72->28`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t125e_t119g_relocate_forbid_contract_slot_summary.md` and `t125e_loose_t119g_relocate_forbid_contract_slot_summary.md` - Forbidding `0,3;0,4;1,4;1,5` leaves zero relocation candidates, even loose. This identifies the left slot as the only current early relocation slot.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t126_reserved_bundle_probe/t126j_reserved_open_from_t117g_s126102.asset` - Best reserved scheduled-fill basis: coverage `0.9453441`, official solved, support depth `3`, preserves `58->68->72->28`, reserves left contract and remote candidate slots.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t126j_reserved_open_trace_metrics.csv` and `t126j_reserved_open_relation_audit_summary.md` - Official validation for t126j; solved `True`, process `B`, LocalEasy, relation contains `58->68`, `68->72`, `72->28`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t126_reserved_bundle_probe/t126n_extend1_from_t126j_c76_14_22.asset` - Non-reserved one-cell extension from t126j; coverage `0.9473684`, official solved in top5 trace, preserves support depth `3` and `58->68->72->28`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t126n_extend1_from_t126j_nonreserved_top5_trace_metrics.csv` and `_relation_audit_summary.md` - Official trace/relation audit for five non-reserved `0.947` extension rows; all solve and preserve the bridge contract.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t126o_tailchain_from_t126n_c76_manifest.csv` through `t126s_tailchain_from_t126n_c38_manifest.csv` - Final `0.9514170` tail closures from reserved `0.947` rows; all use `0,3;0,4`, so they are rejected for the reserved-guard route.

## Generated-Root WBP t127 Non-Left Tail-Split Closure - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t127a_t126j_closure_slot_audit_summary.md` and `t127b_t126n_c76_closure_slot_audit_summary.md` - Closure slot audits proving direct clean endpoint closure is unavailable; the left slot is accepted, while right/remote reserved slots are Greedy-unsolved.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t127_tail_split_closure_probe/t127c_tail_split_closure_from_t126n_c76_r001_orig_orig.asset` - Non-left closure proof: trims chain `27`, recuts `3,8;3,9` as chain `80`, appends `3,10;3,11` as chain `81`, reaches coverage `0.9514170`, official solved.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t127c_tail_split_closure_manifest.csv` / `_trace_metrics.csv` / `_relation_audit_summary.md` - Official validation for t127c. Preserves protected root prefix and left slot cells, but support depth drops to `2`; relation includes `80->81->27` and loses `72->28`.

## Generated-Root WBP t129 Delay72 Tail-Split Walkthrough - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t129_tail_split_delay72_probe/t129a_tsc_delay72_c76_14_22_r001_orig_orig.asset` - Current t129a walkthrough candidate: coverage `0.9514170`, chains `82`, official solved, protected root prefix preserved, left reserved cells remain empty, relation preserves `58->68->72->28`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t129a_tail_split_delay72_manifest.csv` and `_summary.md` - Tail-split materialization manifest/summary. Operation: split chain `27` by 3 cells, extra-trim chain `31:2`, recut `2,8;3,8;3,9`, and close with `3,10;3,11;3,12;3,13`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t129a_tail_split_delay72_trace_metrics.csv` / `_trace_steps.csv` / `_trace_summary.md` - Official trace rerun: `1/1` solved, process `B/B`, hard class `LocalEasy`, local/nearOuter `7/7`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t129a_tail_split_delay72_relation_audit_summary.md` / `_levels.csv` / `_edges.csv` - Relation audit rerun: official support root `58`, support depth `3`, support score `0.743`, edges include `58->68`, `68->72`, `72->28`; no `60->28` steal in the filtered relation check.

## Generated-Root WBP t131-t132 Walkthrough Generalization - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t131_transfer_walkthrough_probe/` - t131b sibling-transfer assets. Four t126n sibling bases receive the same delay72 tail-split closure and reach coverage `0.9514170`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t131b_transfer_delay72_manifest.csv` - Combined t131b accepted manifest, 4 rows.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t131b_transfer_delay72_trace_metrics.csv` / `_trace_steps.csv` - Official trace for t131b; `4/4` solved, process `B`, hard class `LocalEasy`, local/nearOuter `7/7`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t131b_transfer_delay72_relation_audit_summary.md` / `_levels.csv` / `_edges.csv` - Relation audit for t131b; all four rows preserve `58->68`, `68->72`, `72->28`, and avoid `60->28`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t131_transfer_delay72_*_manifest.csv` - Strict reserved-left transfer attempts; all reject because `mustRemainEmptyCells=0,3;0,4;1,4;1,5` is already used by those sibling bases.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t132_t104d_open_bundle095_probe_summary.md` and `t132b_t104d_open_relaxed_bundle095_probe_summary.md` - True alternate-root t114/t104d closure probes. Both stall at coverage `0.9311741` with zero accepted bundles, even in relaxed mode.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t132_generalization_walkthrough_probe/` - t132 diagnostic output assets; they are stalled copies/continuations for analysis, not accepted `0.95+` candidates.

## Generated-Root WBP t133 Alternate-Root Near-Closure - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t133_t104d_open_closure_slot_audit_len6_summary.md` - True alternate-root t114/t104d residual empty-slot audit. Only 14 short new-chain slots exist and all are Greedy-unsolved (`cleanOk=0`).
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t133_t104d_open_tail1_probe_manifest.csv` / `_summary.md` - One-cell existing-tail extension diagnostic from t114. Finds 12 Greedy-solvable +1 candidates, proving body-capacity exists even though new-chain closure fails.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t133_t104d_open_multitail949_fast_manifest.csv` / `_summary.md` - Multi-tail +9 near-closure manifest. Four rows reach coverage `0.9493927` with 9 distinct existing-chain extensions.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t133_t104d_open_multitail949_fast_trace_metrics.csv` / `_trace_steps.csv` - Official trace for the +9 near-closure rows; `4/4` solved, process `B`, hard class `LocalEasy`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t133_t104d_open_multitail949_fast_relation_audit_summary.md` / `_levels.csv` / `_edges.csv` - Relation audit for +9 rows; confirms they are closure-capacity proofs, not hard candidates (`antiLocal=0.147`, best closure depth `2`).
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t133_t104d_open_multitail095_fast_summary.md` and `t133_t104d_open_multitail095_relaxed_summary.md` - +10/`0.95` multi-tail boundary. Best geometric frontier reaches `0.9514170`, but `0` rows pass final Greedy.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t133_t104d_open_from949r*_tail10_summary.md` and `t133_t104d_open_from949r1_closure_slot_audit_len6_summary.md` - From each +9 row, the 10th cell cannot be added by single-tail extension; short new-chain closure after +9 still has `cleanOk=0`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t133_t104d_open_949r1_split*_summary.md` - Targeted tail-split closure probes around `6,6;7,6` and `9,9-9,12`; all tested rows have `0` accepted. Do not repeat broad brute force without a scheduler.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t133_generalization_closure_probe/` - t133 diagnostic assets, including 12 one-cell probes and 4 official-solved +9 near-closure rows. Not final `0.95+` candidates.

## Generated-Root WBP t134 Tail-Path Scheduler Boundary - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-GeneratedRootWBPV12TailPathSchedulerV1.py` - Exhaustive short existing-tail path scheduler. It assigns all remaining tail cells up front before final Greedy, so use it to prove tail-only closure boundaries.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t134_t104d_open_tailpath095_summary.md` - t104d run summary: base coverage `0.9311741`, needed cells `10`, eligible tail chains `10`, path options `15`, exact target combinations `31`, accepted `0`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t134_t104d_open_tailpath095_manifest.csv` - Ranked exact `+10` rows. All are Greedy-unsolved, with common blockers around `7->40`, `12->40`, `40->26`, and `27->42`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWBPV12/t134_slot_scheduler_probe/` - Diagnostic output directory; no accepted assets were written for the t104d tail-path run.

## Generated-Root WBP t135 Current Review6 Pack - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GeneratedRootWBPV12_CurrentReview6Pack.asset` - Playable 6-level review pack for the current Generated-Root WBP route; mounted in worktree `Demo.unity` with GUID `f4229d0f49354cb48b3127c1bdb7ed31`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t135_current_review6_summary.csv` / `.md` - Official trace summary and weak-metric explanation for the review pack.
- Included levels: `t124b_right_run_shape_recut2_r006_trim21x4_orig`, `t129a_tsc_delay72_c76_14_22_r001_orig_orig`, `t131b_transfer_delay72_c56_5_13_r001_orig_orig`, `t131b_transfer_delay72_c38_1_16_r001_orig_orig`, `t133_t104d_open_multitail949_fast_r001_a09_d09`, and `t133_t104d_open_multitail949_fast_r003_a09_d09`.
- Use this pack when comparing human feel against official trace: all six are official solved but still `B/B` and `LocalEasy`; weak areas are local/near-outer conveyor runs, low anti-locality, and weak alternate-root support depth.

## Generated-Root WBP t141 SeedState Materialization Assets - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t141_seedstate_solvedprefix3_smoke/` - t141j strict solved-prefix3 diagnostic assets. Four candidates preserve generated root `root154_from0700_tail0_c01`, add `3` materialized seedState chains, reach coverage `0.7388664`, and Greedy solve with `3.966/10`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t141j_root154_seedstate_solvedprefix3_smoke_candidates.csv` - Candidate manifest for the four t141j assets; chain legality OK and demand overlap weight `752.152`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t141j_root154_seedstate_solvedprefix3_smoke_chain_plan.csv` - Reconstructed materialized seed chains. Semantic edges include `8->55->10`, `7->56->19`, and `20->57->20` for the accepted three-chain prefix.
- Boundary: t141j is not a review/final pack and does not target `0.95+`; it proves the new region-duty/reservation/materialization pipeline can produce strict solved seedState groups before high-coverage expansion.

## Generated-Root WBP t142 SeedState Front-End Assets - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t142_seedstate_exact4_no_r3c2_strict_smoke/` - t142f exact-4 diagnostic assets. Four candidates preserve generated root `root154_from0700_tail0_c01`, add semantic seedState chains for `r4c3/r2c3/r2c1/r3c3`, reach coverage `0.7489879`, and official trace `4/4 A`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t142_seedstate_exact5_orderA_strict_smoke/` - t142h exact-5 diagnostic assets. Current best front-end proof: ordered seedState backbone `r4c3/r2c3/r2c2/r2c1/r3c3`, coverage `0.7489879-0.7510121`, official trace `4/4 A`, avg/max choices about `2.9-3.22/6-7`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t142h_root154_seedstate_exact5_orderA_strict_smoke_candidates.csv` - Candidate manifest for t142h; root fingerprint remains `5f26bf2d9d40c90a`, added chains are short/medium semantic seedState chains, and this is not a final `0.95+` review pack.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t142h_root154_seedstate_exact5_orderA_official_trace_metrics.csv` - Official trace for t142h assets; all rows solved/A with low choice peak and no outer-exit regression.
- Boundary: t142h proves the region-duty front-end and official trace viability, not high-coverage closure. t142i/t142k show existing extension/cluster carrier options cannot add a safe sixth chain yet.

## SGP Read-Demand FrontierContract Review - 2026-07-02

- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureReadDemandV1FrontierContractReviewPack.asset` - Current 4-level FrontierContract review pack mounted in the `read-demand-hardening` Demo scene. Positive direction, but human feedback says it still plays as continuous bottom-to-top clearing.
- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureReadDemandV1FrontierContractPack.asset` - 12-row source pack for the isolated generation-time FrontierContract lane; not mounted to Demo by default.
- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_read_demand_v1_frontier_contract_report.csv` - Source generation report. Pool12 generated 12/12 candidates, coverage about `0.961-0.974`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_read_demand_v1_frontier_contract_pool12_trace1_metrics.csv` - Official trace for pool12: 12/12 solved, tiers `A=1`, `B=3`, `Drop=8`.
- `.worktrees/read-demand-hardening/.codex-run/sgp_pressure_read_demand_v1_frontier_contract_review_keep.csv` - Review selection CSV with `rdfc_12`, `rdfc_11`, `rdfc_05`, and `rdfc_07`; `rdfc_12` is the best low-choice composite-window sample.
- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_read_demand_v1_frontier_contract_review_report.csv` - Review-pack build report for the mounted 4-level pack.
- Boundary: strict `choiceChokeAfterLocalFrontierBreakCount` remains `0`; this pack should be treated as a partial positive/diagnostic, not a production keep pool. Next route should target explicit interruption-window scheduling.

## SGP Read-Demand ScheduledBreak Review - 2026-07-02

- Mounted review pack: `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureReadDemandV1ScheduledBreakReviewPack.asset`, GUID `3f068564ae8922a4b8b8ed1a1574e934`; current Demo in `.worktrees/read-demand-hardening` points here.
- Review pack contents: exactly one row, `rdsb_03_sgp_pressure_hard_rect_read_demand_v1_scheduled_break_sb_lba`, path `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Levels/DirectProcedural/RDSB/rdsb_03_sgp_pressure_hard_rect_read_demand_v1_scheduled_break_sb_lba.asset`.
- Review report: `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_read_demand_v1_scheduled_break_review_report.csv`.
- Keep CSV: `.worktrees/read-demand-hardening/.codex-run/sgp_pressure_read_demand_v1_scheduled_break_review_keep.csv`.
- Source smoke metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_read_demand_v1_scheduled_break_smoke1_metrics.csv`; 12/12 solved, tiers `A=1/B=6/Drop=5`.
- Step diagnostic proof for mounted row: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_read_demand_v1_scheduled_break_rdsb03_diag_steps.csv`; steps `30-33` have choices `1,1,1,1`, regions `3 -> 4 -> 1 -> 5`, and hard frontier breaks on steps `30-32`.
- Boundary: this pack is for human review of the first real interruption positive. It is not yet a bulk source or production keep pool because only 1/12 rows hit official after-local frontier break.

## SGP Read-Demand ScheduledBreak ChoiceCompress - 2026-07-02

- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureReadDemandV1ScheduledBreakReviewPack.asset` - current Demo-mounted one-level review pack after ChoiceCompress gating; GUID `3f068564ae8922a4b8b8ed1a1574e934`.
- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Levels/DirectProcedural/RDSB/rdsb_03_sgp_pressure_hard_rect_read_demand_v1_scheduled_break_sb_lba.asset` - rebuilt 61-chain ScheduledBreak sample; source pressure `EarlyChoices=25.0/72`, chain-choice wave `4.1/8`, portable openers `2`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/sgp_pressure_read_demand_v1_scheduled_break_smoke7_metrics.csv` - official trace for the rebuilt sample; `1/1` solved, tier `B`, avg/max choices `4.08/7`, after-local frontier breaks `2`, remote drain choke count `4`.
- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_read_demand_v1_scheduled_break_report.csv` and `_review_report.csv` - source generation and review-pack manifests for the current mounted sample.

## Campaign500 Choke Candidate Review Pack - 2026-07-02

- `Assets/ArrowMagic/SOData/Packs/Campaign500/ChokeCandidateAuditReviewPack20260702.asset` - 15-level main-project Demo-mounted review pack for choke/key-arrow candidates. GUID `988ff865ac71dbc4ea6aa98b5cd5eda8`.
- `Exports/Campaign500_DesignPlanning_20260702/ChokeCandidateAudit_20260702_AllKnown/choke_candidate_audit_review_pack_build_summary.md` / `_report.csv` - Build manifest listing level order, choke class, score, source family, loaded asset path, and risk tags.
- Contents prioritize ScheduledBreak (`rdsb_03`, `rdsb_11`, `rdsb_05`), ReadDemandMutation (`sgp_rdcm_v2_*`), NutationLongChain/Hub samples, and three weak current-order references (`order 1/2/10`) for comparison.

## Campaign500 First50 V2 Existing Review Pack - 2026-07-02

- `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500First50V2ExistingReviewPack.asset` - 50-level main-project Demo-mounted review pack for the first full V2 layout rehearsal. GUID `d686bcdceefd4bca856620dfe792fc29`.
- `Exports/Campaign500_First50_V2Existing_20260702/campaign500_first50_v2_existing_layout_v1.csv` - 50-row resource layout. Built from existing C5V4FSC resources only; no new generation.
- `Exports/Campaign500_First50_V2Existing_20260702/campaign500_first50_v2_existing_pack_build_summary.md` / `_report.csv` - Direct-yaml pack build manifest and GUID report.
- `Exports/Campaign500_First50_V2Existing_20260702/Audit_LayoutV1/campaign500_rhythm_audit_v1_summary.md` - Static rhythm audit result: `96/A`, no errors, two warnings. Remaining issue is low-load early normal supply, especially L3.

## Generated-Root WBP t143 Backbone Coverage Layer Assets - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t143_backbone_coverage_layer_v1_greedy12/` - Formal t143 V1 coverage-layer output assets. Source backbone is `t142h_root154_seedstate_exact5_orderA_strict_smoke_c004`; top rows preserve root `root154_from0700_tail0_c01` and add `12` short old-owner-release coverage chains.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t143g_root154_c004_bclv1_greedy_cov12_candidates.csv` - Candidate manifest for the formal V1 run. Top coverage is `0.8218623`, with `36` added coverage cells, generated Greedy `initial=3`, `avg/max=2.500/6`, and release owners recorded per coverage chain.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t143g_root154_c004_bclv1_greedy_cov12_options.csv` - Option audit for t143g; each coverage option records cells, release owner, base greedy step, blocker value, and single-chain Greedy proxy.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t143g_root154_c004_bclv1_greedy_cov12_summary.md` - Human-readable summary of the t143g coverage-layer run.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t143g_root154_c004_bclv1_greedy_cov12_top4_trace_light_metrics.csv` / `_summary.md` - Official light trace for t143g top4: `4/4` solved, process `A`, avg/max choices `2.49/6`, outerExit run `1`, localPatch run `3`, structuredHardnessV21 `0.745`, frontierDrainRemoteChokeCount `9`.
- Boundary: these are diagnostic/continuation assets, not final `0.95+` candidates. They prove the coverage layer can add cells without collapsing process A, but single-layer owner-hit supply currently caps near `0.82`.

## HoleMask Nutation Seed-Pool Rerun Assets - 2026-07-02

- `F:\Unityproject\ArrowLevel-Hand-HoleExperiment\Assets\ArrowMagic\SOData\Levels\Seeds\NutationCandidatePool\` - approved copied Nutation seed pool for the experiment project. Contains `106` LevelDefinition assets and matching `.meta` files from main DirectProcedural Nutation pools.
- `F:\Unityproject\ArrowLevel-Hand-HoleExperiment\Assets\ArrowMagic\SOData\Levels\Production\HoleMask\HighChain100To150\` - regenerated high-chain HoleMask candidates after Nutation seed-pool integration. Contains `15` final accepted technical-pass assets from the completed rerun, now marked as negative review examples because human review found them too simple/mechanical.
- `F:\Unityproject\ArrowLevel-Hand-HoleExperiment\Assets\ArrowMagic\SOData\Packs\Production\HoleMask\HoleMask_HighChain_100To150_Candidates.asset` - regenerated `15`-level pack. Negative review verdict: do not use as production. It passed Greedy/fill/chain technical gates but failed human-feel quality; all accepted rows remain R1/R2-sourced, and Nutation only appeared in preview Top8 on `3/5` masks.
- `F:\Unityproject\ArrowLevel-Hand-HoleExperiment\Assets\ArrowMagic\Reports\Production\HoleMask\HoleMask_HighChain_100To150_Candidates_Report.txt` - report for the rerun, including preview rankings and accepted list.

## HoleMask Nutation Terrain Probe Assets - 2026-07-02

- `F:\Unityproject\ArrowLevel-Hand-HoleExperiment\Assets\ArrowMagic\SOData\Packs\Production\HoleMask\HoleMask_NutationTerrainProbe.asset` - 2-level experiment pack mounted to experiment `Demo.unity`. Uses Nutation seed pool plus fixed-hole terrain blocker and dependency/outer-exit gates.
- `F:\Unityproject\ArrowLevel-Hand-HoleExperiment\Assets\ArrowMagic\SOData\Levels\Production\HoleMask\NutationTerrainProbe\` - accepted probe LevelDefinition assets. Current rows: `36x30_standard` with fill `764/868`, chains `96`; `40x32_standard` with fill `930/1056`, chains `79`.
- `F:\Unityproject\ArrowLevel-Hand-HoleExperiment\Assets\ArrowMagic\Reports\Production\HoleMask\HoleMask_NutationTerrainProbe_Report.txt` - generation report with preview/final metrics: hole hits, direct outer exits, dependency edges/depth, near-hole blocker edges, and accepted paths.
- `F:\Unityproject\ArrowLevel-Hand-HoleExperiment\Assets\ArrowMagic\SOData\Levels\Generated\MaskPreview\previews\hole_template_blocks_v13_top5_demo.txt` - direct template blocker rerun report. Strict Top5 accepted `0`; keep as negative boundary evidence for direct blocker template generation.

## Generated-Root WBP t144 Unified Duty Graph Assets - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t144_unified_duty_graph_v1_smoke/` - t144a V1 output assets. They are materialized from generated root plus jointly selected seedState and coverage duties, not from a prebuilt t142h backbone asset.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t144a_root154_udg_v1_seedcov12_candidates.csv` - Candidate manifest. Best coverage `0.8238866`, root chains `55`, seedState chains `5`, coverage duties `12`, generated Greedy `avg/max=2.667/6`, `preMaterializationDutyCommit=1`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t144a_root154_udg_v1_seedcov12_duty_commit.csv` - Duty-level commit table; for each candidate it lists causal seedState and coverage basin options, roles, basins, release owners/windows, and cells.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t144a_root154_udg_v1_seedcov12_cell_plan.csv` - Full-board cell plan. Each cell is assigned as root, causal seedState, coverage basin, or intentional empty / future supply before final materialization.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t144a_root154_udg_v1_seedcov12_top4_trace_light_metrics.csv` / `_summary.md` - Official light trace for t144a top4: `4/4` solved/A, avg/max `2.65/6`, outerExit run `1`, localPatch run `3`, structuredHardnessV21 `0.759`, solveTraceQuality `0.814`, frontierDrainRemoteChokeCount `8`.
- Boundary: t144a is the current unified-control proof, not final high coverage. Next work should increase coverage basin capacity/future release planning toward `0.88+`, while preserving the pre-materialization duty evidence.

## Generated-Root WBP t145 Multi-Wave Coverage Assets - 2026-07-02

- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t145_unified_duty_multiwave_v1_smoke/` - t145 multi-wave output assets. They are materialized from generated root plus seedState and all coverage waves in one pass, with release domains recorded before final asset write.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t145e_root154_udg_mw_cov12_4_4_1_candidates.csv` - Best accepted t145 manifest. Coverage `0.8684211`, chains `81`, seedState chains `5`, coverage duties `21`, waves `12,4,4,1`, root fingerprint `5f26bf2d9d40c90a`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t145e_root154_udg_mw_cov12_4_4_1_duty_commit.csv` / `_cell_plan.csv` / `_wave_audit.csv` - Pre-materialization proof tables. They show release owner/domain/window per coverage duty and the staged capacity boundary.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t145e_root154_udg_mw_cov12_4_4_1_top4_trace_light_metrics.csv` / `_summary.md` - Official light trace for t145e top4: `4/4` solved/A, avg/max `2.69-2.73/5`, outerExit `1`, local/nearOuter `3/3`, structuredHardnessV21 `0.622-0.665`, HardStructureV3 still `LocalEasy`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t145l_root154_udg_mw_cov12_4_4_1_beam_candidates.csv` - Larger beam variant; same coverage `0.8684211`, but prior-wave release count improves to `2`, proving topology changes without solving coverage.
- Boundary: t145 is not a final `0.95+` review pack. `12,4,4,1` is the current owner-hit multi-wave ceiling; larger tail waves fail or time out, and t145e tail-extension diagnostic finds only one extra Greedy-safe cell.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Packs/SGPRhythmLab/SGPRhythmLab_GeneratedRootWBPV12_t145eCov868Pack.asset` - 4-level t145e Cov868 human-review pack, GUID `145e0868421100000000000000000001`, mounted to `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/Scenes/Demo.unity`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t145e_root154_udg_mw_cov12_4_4_1_difficulty_verify_v1.csv` / `_summary.md` - New split difficulty verifier output for the mounted t145e Cov868 pack. All rows pass TraceGate and classify as `HardPotential`, not `TrueHardCandidate`, because structure remains LocalEasy with weak support/dual-gate/remote-choke evidence.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t146a_t145e_early_space_debt_summary.csv` / `_cells.csv` / `_summary.md` - Empty-space debt audit for t145e Cov868. It proves the remaining gap to `0.95` is not neutral empty space: `41` cells needed, safe capacity estimate `29`, debt `12`, high-risk empty count `30`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t146c_root154_udg_space_narrow_cov12_4_4_1_candidates.csv` - Narrow t146 space-budget smoke using the patched t145 scheduler. Reaches the same `0.8684211` / debt `12`; treat as a negative boundary showing wave-level ranking is too late.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t147_early_budget_option_v1_smoke/` - t147 budget-aware coverage-option smoke assets. Diagnostic only; not mounted as review pack.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t147b_root154_udg_budgetopt_balanced_cov12_4_4_1_candidates.csv` / `_trace_light_metrics.csv` / `_difficulty_verify_v1.csv` - Best t147 partial positive so far: coverage `0.8684211`, official `2/2 A`, DifficultyVerifier `HardPotential 0.730-0.731`, but same space debt and still `LocalEasy`.

## Generated-Root WBP t148 Structural Contract Diagnostics - 2026-07-02

- No new review pack or Demo mount was produced in t148. Existing t145e Cov868 pack remains the mounted WBP review pack unless changed later.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t148a_t147b_budgetopt_balanced_diag_metrics.csv` / `_steps.csv` - official step-diagnostic rerun for t147b c001/c002; both solved.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t148a_t147b_budgetopt_balanced_relation_audit_edges.csv` / `_parents.csv` / `_levels.csv` / `_summary.md` - relation audit showing why t147b remains `LocalEasy`: anti-locality and CUD p20 are low, best depth is a single-spine closure, and cross-critical fanouts are shallow.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t148_support_closure_bridge_probe/` - diagnostic late bridge output dir. Probes currently wrote no accepted LevelDefinition assets for the tested root/release pairs; treat the folder as probe scaffolding, not a review level source.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t148d_t147b_structural_contract_projection_v1_contracts.csv` / `_summary.md` - structural contract duties compiled from trace/relation/space evidence. This is the current input spec for the next structural materializer.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t148f_t147b_c001_structural_contract_option_audit_uncapped_v1.csv` / `_summary.md` - uncapped root+seedState option visibility audit. It shows BCL owner-hit grammar cannot see most high-priority structural contracts, so the next implementation should add a structural reservation/materialization primitive before coverage waves.

## Generated-Root WBP t149 Support-Closure Preservation Diagnostics - 2026-07-02

- No new review pack or Demo mount was produced. Existing WBP Demo mount remains the previous t145e Cov868 pack unless explicitly changed.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t148_support_closure_preserve_wave_smoke/` - diagnostic assets for t148j/t148k/t148l/t148m support-closure preservation probes. These are not final candidates; use them to inspect the coverage/structure tradeoff.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t148k_root154_udg_preserve_root50_closure_forbid012_cov12_4_candidates.csv` - best protected diagnostic manifest. Forbids release owners `0/1/12`, reaches coverage `0.8340081`, and preserves generated root metadata.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t148n_t148k_preserve012_cov12_4_diag_metrics.csv` / `_steps.csv` / `_relation_audit_summary.md` - official trace and relation proof. `3/3` solved; c001/c002 are `A/A`; root50 support closure is depth `3`, score `0.807`, `crossCrit`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t148m_root154_udg_preserve_root50_closure_forbid012_cov12_4_4_wave_audit.csv` - hard-protection boundary: third wave cannot form because the owner-hit pool collapses to one filtered option.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/t149c_root154_udg_stagedpreserve012_w2_cov12_4_4_1_wave_audit.csv` and `t149e_root154_udg_stagedpreserve012_w1_cov12_4_4_small_wave_audit.csv` - staged protection attempts; still fail at wave3, proving simple release-window gating is insufficient.
- Boundary: t148k proves coverage polluted the hard skeleton and protection restores it, but also proves current BCL grammar cannot scale from protected `0.834` toward `0.95`. Next candidate work should add a structural coverage outlet/materializer before generic coverage waves.

## Generated-Root WBP t150 Structural Coverage Outlet Diagnostics - 2026-07-02

- No review pack or Demo mount was produced. Existing WBP Demo mount remains unchanged.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t150_structural_coverage_outlet_probe/` - diagnostic LevelDefinition assets for release-window interposer and protected BCL continuation probes.
- `t150g_t142h_c004_release_interposer_cov755_m10_r012_rel10_tar56_trimnone.asset` - useful early interposer sample: `10` releases new chain, target window is `56`, coverage `0.7550607`, official `A/A`, root50 closure preserved.
- `t150j_t150i_c001_preserve012_bcl4_c001.asset` - best protected continuation so far: coverage `0.8360324`, official `A/A`, avg/max `2.22/5`, root50 closure score `0.839`, DifficultyVerify `HardPotential 0.746`.
- `t150p_t150n_c001_preserve012_bcl2_c001.asset` - double-interposer branch endpoint: feasible but only coverage `0.8340081`; keep as boundary evidence, not current best.
- Boundary: t150 validates front-loaded semantic interposers as a real outlet, but current one-off/double-interposer probes remain far from `0.95+`; next level generation should integrate a multi-duty structural-interposer scheduler before coverage waves.

## Generated-Root WBP t151-t152 Structural Interposer / Dual-Gate Slot Diagnostics - 2026-07-02

- No review pack or Demo mount was produced. Existing WBP Demo mount remains unchanged.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t151_structural_interposer_scheduler_v1/` - diagnostic LevelDefinition assets for t151 structural interposer scheduler and protected continuation. Best row remains `t151h_t151g_c001_preserve012_bcl4_c001` at coverage `0.8360324`; c001-c003 official trace `A/A`, root50 closure preserved, but still no strict dual gate.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Levels/SGPRhythmLab/GeneratedRootWholeBoardPlannerV12/t152_dual_gate_upstream_control_probe/` - t152 probe output directory. Current formal t152 upstream-control probe produced `0` accepted control candidates; any ad hoc late relocation artifacts in this folder are diagnostic negatives unless later official trace proves otherwise.
- `t152i_t151h_c003_dualgate_control_slot_projection.csv` is the important t152 handoff, not a playable level: all gate5 control slots around `(0,14)` have incumbent owner debt, so the next generator should reserve those cells/rays earlier instead of continuing late repair.
- Boundary: do not promote t151/t152 assets as final `0.95+` candidates. They are evidence that structural interposers are viable early, and strict dual gate now needs early control-slot/corridor reservation.

## SGP Read-Demand ScheduledBreak Tryout - 2026-07-02

- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_read_demand_v1_scheduled_break_report.csv` - existing ScheduledBreak source report; current traceable asset is `rdsb_11...sb_lbc`, coverage `0.956`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/codex_try_scheduled_break_confirm1_metrics.csv` - fresh `-SkipUnity` official trace confirmation for the existing ScheduledBreak asset: solved, `processTier=B`, `frontierDrainRemoteChokeCount=8`, `choiceChokeAfterLocalFrontierBreakCount=1`, still `LocalEasy`.
- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_read_demand_v1_scheduled_break_loose12_report.csv` - fresh Loose12 generation smoke; `12` specs produced `4` solved assets, coverage `0.957-0.964`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/codex_try_scheduled_break_loose12_1_metrics.csv` - official trace for the fresh Loose12 smoke. Best row `rdsbl12_01...lock_buckle_b1_01` is `processTier=A`, avg/max `4.58/10`, `frontierDrainRemoteChokeCount=4`, `choiceChokeAfterLocalFrontierBreakCount=1`, local/near/same-region runs `3/2/3`; other rows are solved but process `Drop`.

## SGP Read-Demand Next-Step Review Assets - 2026-07-02

- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureReadDemandV1ScheduledBreakReviewPack.asset` - 6-level Demo-mounted review pack for ScheduledBreak/read-demand feel inspection. Mounted to `.worktrees/read-demand-hardening/Assets/ArrowMagic/Scenes/Demo.unity`.
- `.worktrees/read-demand-hardening/.codex-run/codex_try_scheduled_break_nextstep_review_trace_input.csv` - exact 6-level manifest used for trace and review pack assembly. Contains `rdsb_03`, `rdsb_11`, and four Loose12 boundary probes.
- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_read_demand_v1_scheduled_break_review_report.csv` - Unity review-pack load report confirming all 6 LevelDefinitions were loaded.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/codex_try_scheduled_break_nextstep_review_metrics.csv` - official trace for the 6-level pack; `6/6` solved, missing/failed `0`.
- `.worktrees/read-demand-hardening/.codex-run/codex_try_scheduled_break_nextstep_review_difficulty_verify_v1_b9.csv` / `_summary.md` - comparable B/9 DifficultyVerify result. `rdsb_03...sb_lba` is `HardPotential` score `0.651`; `rdsb_11...sb_lbc` is `Review` score `0.591`; Loose12 rows are not production-pass due process/max-choice/openers.
- `.worktrees/read-demand-hardening/.codex-run/codex_try_scheduled_break_pool36_1_unity.log` and `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_read_demand_v1_scheduled_break_pool36_report.csv` - interrupted/negative Pool36 attempt. First rows failed hard ScheduledBreak gates and produced no assets.

## RDSB03 Same-Level Mutation A/B Assets - 2026-07-02

- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureReadDemandV1ChokeMutationV2ReviewPack.asset` - current 3-level Demo-mounted same-level A/B review pack. Order: original `rdsb_03`, best single mutation `sgp_rdcm_v2_s01_09_c19`, best double mutation `sgp_rdcm_v2_s01_10_c19_c25`.
- `.worktrees/read-demand-hardening/.codex-run/codex_try_rdsb03_same_level_ab_review_keep.csv` - exact 3-row review manifest used to build the current pack.
- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_read_demand_v1_choke_mutation_v2_report.csv` - 11-row same-source ChokeMutationV2 generation report from `rdsb_03`.
- `.worktrees/read-demand-hardening/.codex-run/codex_try_rdsb03_same_level_mutation_compare_trace_input.csv` - original plus 11 variants trace input.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/codex_try_rdsb03_same_level_mutation_compare_metrics.csv` - official trace for original plus variants; `12/12` solved.
- `.worktrees/read-demand-hardening/.codex-run/codex_try_rdsb03_same_level_mutation_compare_difficulty_verify_v1_b9.csv` and `_strict.csv` - verifier outputs. Best variants pass default strict gate as `HardPotential`; original remains B/9 `HardPotential`.

## RDSB03 Relation-Guided Repair Review6 - 2026-07-02

- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureReadDemandV1ChokeMutationV2ReviewPack.asset` - current 6-level Demo-mounted relation-guided review pack. Order: original `rdsb_03`, previous micro top `sgp_rdcm_v2rp_r03_04_rgp10b199n219_c19`, then relation-guided top rows `r08_05_rfp43`, `r08_03_rrp25n75_rfp43`, `r06_04_rrp29n150_rfp27`, `r06_02_rrp29n150_rfp27_rfp25`.
- `.worktrees/read-demand-hardening/.codex-run/codex_try_rdsb03_relation_guided_repair_plan.csv` - source relation-guided repair plan. It avoids the already-good `52/55/57/58/9` choke and targets `21/25/27/29/33/61` plus residual sweep owners.
- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_read_demand_v1_choke_mutation_v2_repair_plan_report.csv` - Unity generation report for the relation-guided run; `48/48` rows status `ok`.
- `.worktrees/read-demand-hardening/.codex-run/codex_try_rdsb03_relation_guided_repair48_trace_input.csv` - original + micro top + 48 relation-guided variants trace input.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/codex_try_rdsb03_relation_guided_repair48_metrics.csv` / `_steps.csv` - official trace outputs; `50/50` solved.
- `.worktrees/read-demand-hardening/.codex-run/codex_try_rdsb03_relation_guided_repair48_difficulty_verify_strict.csv` and `_b9.csv` - DifficultyVerify outputs. Best strict row is `sgp_rdcm_v2rp_r08_05_rfp43`, `HardPotential 0.716`, `A/A`, avg/max `3.65/7`, low2 `0.306`, still `LocalEasyStructure`.
- `.worktrees/read-demand-hardening/.codex-run/codex_try_rdsb03_relation_guided_repair48_relation_audit_*` and `_region_frontier_replay_*` - relation/frontier audit outputs for the full run. They confirm rhythm improved but support depth remains `2` and anti-locality remains far below true-hard thresholds.
- `.worktrees/read-demand-hardening/.codex-run/codex_try_rdsb03_relation_guided_review6_keep.csv` - exact 6-row review manifest used by the hardcoded review-pack builder.

## RDSB03 Continuity-Capped Review6 - 2026-07-02

- `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureReadDemandV1ChokeMutationV2ReviewPack.asset` - current Demo-mounted continuity-capped review pack, rebuilt after relation-guided review. Order: original, previous micro top, score-top `r08_05_rfp43` with `dependencyFollowRunMax=6`, then continuity-capped rows `r06_04_rrp29n150_rfp27`, `r06_03_rrp29n150_rrp25n75_rfp27`, and `r06_02_rrp29n150_rfp27_rfp25` with `dependencyFollowRunMax=4`.
- `.worktrees/read-demand-hardening/.codex-run/codex_try_rdsb03_continuity_capped_review6_keep.csv` - exact 6-row manifest for the current pack. This is a selection-layer optimization to compare max-continuity capped candidates against the highest-score relation-guided candidate.

## Competitor-Hard Fresh Direct SGP Trial - 2026-07-03

- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_hard_trial_report.csv` - fresh direct SGP trial report. It contains 4 solved high-coverage rows (`0.978-0.994`) generated by the built-in SGP Pressure Hard trial route.
- `.worktrees/competitor-hard-fresh/.codex-run/sgp_pressure_direct_fresh_v12b_trial_trace_summary.md` - trace/join summary for the fresh SGP trial; `productionKeep=2`, `processKeep=3`, `visualPass=1`, but keep rows remain `LocalEasy`.
- `.worktrees/competitor-hard-fresh/.codex-run/sgp_pressure_direct_fresh_v12b_trial_trace_joined.csv` - joined trace metrics for the 4 SGP trial rows. Top keep rows are `sgp_pressure_hard_trial_01_sgp_pressure_hard_rect_lock_buckle` and `sgp_pressure_hard_trial_04_sgp_pressure_hard_rect_core_burst`.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_hard_production_keep.csv` - canonical keep CSV copied from the SGP trial join output, used to build the mounted review pack.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureHardProductionKeepPack.asset` - 2-level Demo-mounted direct SGP keep pack, GUID `c3daf292d5b4bbb49861cc48c7defe5a`, active in `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/Scenes/Demo.unity`.

## Competitor-Hard V10 Seeded SGP Handoff V12 - 2026-07-03

- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Packs/DirectProcedural/CompetitorCoreSkeletonSGPHandoffV12Pack.asset` - current Demo-mounted V10->seeded-SGP hard-preserve pack, GUID `96b0f795080242edbdfbc14fd4f60b5e`; active in `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/Scenes/Demo.unity`.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Levels/DirectProcedural/CompetitorCoreSkeletonSGPHandoffV12/` - generated LevelDefinition assets. Current hard-preserve rows are coverage `0.754-0.776` from V10 coverage `0.714`, with prefix preserved and direct exits capped at `4`.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Reports/DirectProcedural/competitor_core_skeleton_sgp_handoff_v12_report.csv` - current hard-preserve report. It is the source manifest for the mounted pack.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Reports/DirectProcedural/competitor_core_skeleton_sgp_handoff_v12_hardpreserve_report.csv` - backup of the mounted hard-preserve report.
- `.worktrees/competitor-hard-fresh/Assets/ArrowMagic/SOData/Reports/DirectProcedural/competitor_core_skeleton_sgp_handoff_v12_fanout3_report.csv` and `_reservehit_report.csv` - comparison reports for higher-coverage corridor-overlap variants. They reach about `0.84-0.85` but trace `LocalEasy`, so use as negative/diagnostic coverage evidence.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/competitor_core_skeleton_sgp_handoff_v12_lowexit_trace_metrics.csv` - official trace for the hard-preserve/low-exit setup: `1 A / 3 B`, `2 MediumStructure / 2 LocalEasy`, max choices `8-9`.
- `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/competitor_core_skeleton_sgp_handoff_v12_fanout3_trace_metrics.csv` and `_reservehit_trace_metrics.csv` - official traces for higher-coverage variants; both show all rows `B/LocalEasy`, useful for the coverage-vs-skeleton tradeoff.
