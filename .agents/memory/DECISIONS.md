# Decisions

# Generated-Root WBP Decision: Root-First Reservation Feasibility Precedes Matrix Evaluation - 2026-07-03

- Decision: after t183 evaluation semantics freeze, the active Generated-Root WBP mainline is `Root Family Capacity Discovery`, not further scorer/dep-run tuning.
- Decision: the next loop must be root-first: `root candidate -> early reservation feasibility gate -> accepted root -> UDG/duty graph -> product matrix evaluation`. Do not run a matrix-first selection loop that scores roots only after late planning, because it can hide root/reservation incompatibility and reintroduce post-hoc selection bias.
- Decision: split root-first gating into two stages. Stage 1 is a hard `root/reservation compatibility gate`, not a quality filter. Stage 2 is a soft `capacity preflight` that estimates whether enough future release/tail-safe reservation exists before paying for full UDG/product evaluation.
- Evidence: c027-like references have the best capacity shape (`~0.90-0.925`) but weak/dirty root-preplan evidence; t182 boundary-owned roots have clean generated-root identity and dirty-boundary ownership but lower coverage/hard-core capacity; c038/root154 are stable controls with known capacity ceilings. This makes root capacity plus early reservation coupling the unresolved bottleneck.
- Evidence: early feasibility pass does not guarantee UDG success. t182 boundary-owned roots pass dirty-boundary ownership and seedState plumbing but later micro-wave UDG underfits/collapses around the `0.80` band; c038/root154 pass stable structural controls but show capacity ceilings around the `0.83-0.868` band. Therefore the gate must not reject capacity references as "bad roots" too early, but must classify whether the failure is compatibility, capacity, or downstream UDG construction.
- Current evidence does not yet show a confirmed `Compatibility Pass + Capacity High + UDG Collapse` case. Known failures are explained by one of the two axes: c027-like is high capacity but compatibility/root-ownership fails; t182 is compatibility pass but capacity underfits; c038/root154 are compatibility/stability pass but capacity-ceiling controls. Therefore no third hard `UDG feasibility gate` is justified yet.
- Implementation implication: build or run a `root x early reservation feasibility` audit before expensive UDG/product tracing. Stage 1 checks dirty boundary/corner ownership, basin membership, release candidates, dependency-compatible entry, and SSWD/frontier-break support anchors; only Stage-1 compatibility fail rejects a root. Stage 2 reports soft capacity score/risk flags for tail-safe/future-release reservation capacity and can prioritize UDG runs, but must not kill a root by itself.
- Final freeze: gray-zone roots are expected (`Compatibility Borderline Pass + Capacity Borderline/High`), but they should use priority routing and audit labels, not a third hard gate. The finalized architecture is a root-first dual-axis feasibility system: hard compatibility, soft capacity, downstream UDG, post-hoc product evaluation.
- Negative route: do not resume UDG wave13/scorer tuning, official `dependencyFollowRunMax` optimization, post-hoc trace repair, or late filler/closure search as the main path.

# Generated-Root WBP Final Decision: Dependency Follow Run Is A Solver-Policy Artifact - 2026-07-03

- Decision: freeze the t183 system interpretation as a four-layer model: `Generation (SSWD + UDG)`, `Structural Difficulty (anti-spine / discontinuity)`, `Solvability Invariant`, and `Solver Policy Artifacts`.
- Decision: `dependencyFollowRunMax` is no longer a hard difficulty/generation failure gate by itself. It is a solver-policy-conditioned observable that describes the chosen trace executor's traversal footprint.
- Evidence: on the fixed t183o generated board, official/max-cleared style policies produce high dependency run (`9-10`) while anti-follow solves the same board with run `4`. Random available-chain and anti-follow-random policies both solve `5000/5000` trials with `minRemoved=48` and no deadlocks; anti-follow-random has worst dep-run `7`.
- Evidence: generation and ranking were separately closed before this: SSWD is stable, anti-spine scoring works, t183o wave13 pool300 top10/top20 are all root-domain cross-region `singleBoardGate=Pass`, and t183p parent-run-break re-rank adds valid discontinuity score but does not alter saturated top-k.
- Implementation implication: do not continue tuning UDG/wave13/scorer merely to reduce official `dependencyFollowRunMax`. Future product validation must separate structural difficulty evidence from solver policy diagnostics. Use dep-run for solver comparison, traversal behavior debugging, and player-model calibration, not as a standalone difficulty grade.

# Generated-Root WBP Decision: Residual t183 Failure Is Temporal Parent-Cascade, Not Full Graph Spine Dominance - 2026-07-03

- Decision: continue the A route, but refine it as a root parent-run-break scorer before adding heavyweight graph-level reservation. The next fix should target temporal dependency-follow collapse, not rebuild SSWD/UDG or split a second tail generator.
- Evidence: t183n/t183o root-parent audit shows `41` total parent edges, `24` root-root edges, and the longest root-root path `31->22->21->20->19->18->17->16->15->14->13->12` has `11` edges. This is only `45.8%` of root-root edges and `26.8%` of all parent edges, below the `>60%` single-chain dominance threshold.
- Evidence: the official max dependency-follow runs are still long: t183l/t183n run through `22->21->20->19->18->17->16->15->14->13->12`, while t183o shifts to `16->15->14->13->12->43->7->6->5->4`. This means anti-spine scoring reduced spatial/local collapse, but the trace still follows a temporally adjacent parent cascade.
- Evidence: graph-only traversal shuffle keeps the same parent DAG but changes tail order. For t183o, preserving the first `13/20/25` steps and random topological tail order reduces run from official `10` to median `6/6/5`; anti-follow greedy order reaches `4/4/5`. This supports "traversal policy too linear" over "graph structure too narrow".
- Implementation implication: add default-off tail scoring features for root parent-step memory, parent-step proximity penalty, monotonic/adjacent cascade penalty, and discontinuity reward. Use explicit parent-edge graph analysis for audit/calibration where available, but do not promote graph-level hard constraints until scorer-level run-break fails.
- Historical implementation status: the default-off scorer now exists as `--tail-parent-run-break-*` on the UDG multi-wave scheduler. It is intentionally a scoring/audit layer over existing options, preserving SSWD and UDG architecture. The earlier t183p pool300 decision point has since been superseded by the final fixed-board solve-policy calibration below; keep this scorer as an audit/reinforcement signal, not as the main route for reducing official dep-run.
- Final calibration: t183o wave13 ranking is already saturated with valid break candidates, and t183p re-rank does not change top-k. Fixed-board solve-policy perturbation then reproduces the residual layer: `max_cleared` solves with `dependencyFollowRunMax=10`, while `anti_follow` solves the same board with run `4`. Larger stability check keeps the same board and changes only step selection: random available-chain policy solves `5000/5000` trials, and `anti_follow_random` solves `5000/5000` with worst dep-run `7` and no deadlock. Therefore the remaining high dep-run is an official/greedy solve-order traversal artifact, not a generator/scorer failure.
- Updated implication: do not continue tuning wave13 scorer as the main path. Treat t183 parent-run-break as useful audit/reinforcement, but shift the next validation work to solver/trace policy calibration or product verifier interpretation: compare official max-cleared trace against anti-follow/random feasible traces on the same board before using `dependencyFollowRunMax` as a hard product blocker.

# Competitor-Hard Decision: Future Slots Must Be Planned Before Interval Fill - 2026-07-03

- Decision: accept the user's diagnosis that V10/V12 coverage stalls are caused by early interval/BCL fill consuming future outlets. The next competitor-hard route must plan future empty/body/ray capacity before interval fill, not append chains to an already packed V10 board.
- Evidence: V10 interval waves drop from large option pools to very small pools by coverage `0.70`; V12 hard-preserve suffix then stalls around `0.754-0.776` and reports cycle/head exhaustion. V13 front-loads acyclic future-slot reservation and can materialize `12-13` planned slots as a solved batch before suffix fill.
- Current V13 result is route evidence, not product success. It reaches coverage `0.785` best, `3/3` official solved, and preserves connector contract, but trace is only `1 MediumStructure / 2 LocalEasy`; the best coverage row is still `LocalEasy`.
- Negative evidence: naive reserved-room expansion creates easy/high-choice tail noise and reduces interval capacity. BCL-derived slot fallback may find more options but is too slow as a default path. Do not promote either as the final route without phase-aware structural support/frontier-break contracts.
- Implementation implication: future work should promote "planned room/outlet" to a first-class structural duty with acyclic preflight, release-owner/region diversity, and phase-aware materialization. The suffix filler can finish a plan, but it cannot create the plan after earlier chains have blocked the board.

# Generated-Root WBP Decision: Boundary-Owned Roots Are Necessary, But Need Structural Support Duties Afterward - 2026-07-03

- Decision: keep the t182 `19x26 boundary-owned root` route alive. It is the correct fix for dirty `(5,0;5,1)` becoming an open-only sink, because the target cells are now root-owned before seedState and coverage planning.
- Evidence: t182a/t182b generated 3 frame-compatible roots that own `(5,0;5,1)` same-chain; t182c shows accepted non-open release and dependency link with tailGreedyUnsolvedRate `0`; t182d/t182e then connect the best root to solved seedState groups.
- Decision: do not count t182 micro-wave coverage as product success. t182i reaches coverage `0.8036437` and official solved `B`, but hardStructureV3 is `LocalEasy`, dependencyFollowRunMax is `12`, localPatchRun is `7`, and Product hard-core/tail gates fail.
- Evidence: t182j/t182k/t182m quota probes separate the failure modes. Soft diversity can produce Hard-Core+Tail pass at lower coverage (`0.7672065`); moderate quotas can recover a stronger hard-core window but tail fails; late quotas can restore Tail Hygiene/spine control but hard-core frontier-break support disappears.
- Implementation implication: the next scheduler control must be a pre-materialization structural-support/frontier-break duty, not more late tail search and not pure micro-wave coverage scaling. Coverage waves need phase-aware release-owner/basin quotas, but quotas are brakes; they do not create the missing support/frontier-break structure by themselves.

# Generated-Root WBP Decision: Dirty Top Edge Must Be Owned At 19x26 Root Generation Time - 2026-07-03

- Decision: for the active 19x26 c027/c038/c043 route, dirty `(5,0;5,1)` must be handled by root-level boundary ownership/regeneration, not by seedState injection after the existing root is fixed.
- Evidence: t181 narrow and broad seedState injections on c027 root-only make the reservation actionable and enumerate required-cell owner paths, but strict solved-prefix output remains `0`; all required-cell options are `option_single_greedy_unsolved`.
- Evidence: t181 root-pool scan shows the only roots that naturally own `(5,0;5,1)` are `23x30` root10-style roots. All scanned `19x26` roots, including c027/c038/c043 and other root154 families, leave the cluster empty.
- Evidence: root10_c036 proves the topology itself is viable: top_x5 is a same-chain root-owned segment with accepted non-open owner release, dependency link, and Greedy-solved root behavior; the audit only rejects it for missing planner basin/region contract. A micro seedState smoke also starts on root10, but it is frame-mismatched and cannot be counted as current product evidence.
- Implementation implication: next work should add a `19x26 boundary ownership` gate/operator to root generation or root selection. The target is a generated 19x26 root that already owns at-risk top-edge clusters with dependency-compatible release, then assigns basin/region contracts before seedState and coverage waves.

# Generated-Root WBP Decision: Boundary Fill Needs Root/SeedState Ownership, Not Post-Seed Tail Repair - 2026-07-03

- Decision: accept t180 as a stronger boundary than t179. For dirty boundary cells such as `(5,0;5,1)`, do not continue late bundle search, short support injection, or longer anchored corridor repair after the seed-only topology is fixed.
- Evidence: t180b/t180c scan c027/c038/c043 seed-only states across near-corner and full-boundary two-cell clusters. All empty target boundary clusters have `0` accepted non-open candidates; full-boundary empty rows are all `RejectNoDependencyCompatibleEntry`.
- Evidence: accepted non-open boundary entries are almost entirely already occupied: `129/132` are root-chain-owned and `3/132` are seedState/later-owned. This means the current grammar can explain edge clusters that the root/seedState already owns, but cannot turn empty boundary edge cells into dependency-compatible fill later.
- Evidence: t180d targeted anchored-path probing for `(5,0;5,1)` enumerates `184` paths up to length `12`; owner-release paths exist but every one is Greedy-unsolved, so the failure is not just a missing longer local corridor.
- Implementation implication: the next valid route is a pre-materialization boundary ownership/reservation pass. At-risk edge clusters must receive basin membership plus a root/seedState owner and dependency edge/region contract before coverage waves. If the current root family cannot do that, switch root/canvas/topology generation instead of widening post-seed repair.

# Generated-Root WBP Decision: Dirty Corner Needs A Non-Open CornerDuty Contract - 2026-07-03

- Decision: accept the GPT/user review direction. Stop late bundle/tail search for c027 dirty corner `(5,0;5,1)` and treat it as an early duty-graph failure: the cluster has basin membership and potential release owners, but no dependency-compatible accepted entry.
- Evidence: t179 early-open-prefix reproduction proves moving `(5,0;5,1)` earlier is not sufficient. After forcing the open prefix, early-duty can be partially/reproduced only with wider scan, but BCL collapses: 2/3/4-chain bundles are `0`; the only boundary continuation is a single owner0 chain to coverage `0.6842105`.
- Evidence: `t179j_corner_duty_abc_audit` gives A/B/C evidence. A c027 seed-only and C c038 seed-only both have basin `r0c0` and `6` non-open candidates, but accepted non-open is `0` and non-open greedy-unsolved rate is `1.0`. B0 early open-prefix is explicitly `RejectOpenSink`.
- Implementation implication: B-group experiments must inject a real non-open `CornerDuty` / `corner_release_contract` with accepted dependency edge or region contract before BCL. Do not count open-prefix injection as success. If non-open CornerDuty bundle still fails, pivot to root/canvas/topology reservation rather than returning to late tail repair.
- Follow-up evidence: t179k-t179s added a required-duty acceptance gate and tested injected non-open corner duty with nearby support duties. Normal activation still bypasses the corner; required-corner solved bundles are `0`. Therefore current c027 grammar cannot repair the corner by small support injection. The next valid route is stronger root/canvas/topology reservation, not wider late search.

# Generated-Root WBP Decision: Dirty-Open Prefix Is Hygiene-Safe But Not Capacity-Solving - 2026-07-03

- Decision: do not continue trying to solve c027's `(5,0;5,1)` by late repair, non-open single-chain insertion, dirty+one-support bundle, or simply moving the same open chain earlier within the existing closure grammar.
- Evidence: t177 audits the same cluster across c027 lineage stages. Seed-only variants `t155e c001-c004` expose only open candidates and `0` accepted non-open. After early duty/BCL (`t156c/t156o/t156s/t157e`) owner-release-looking candidates appear, but accepted non-open remains `0`, and dirty+support bundle probes remain `0`.
- Evidence: t178 proves an early open-prefix version is not structurally toxic by itself. Forcing `(5,0;5,1)` on `t157e` then continuing closure produces `t178d` at coverage `0.9251012`, official solved `B/Drop`, Hard-Core pass (`0.918`) and Tail pass (`1`).
- Negative evidence: t178g wide continuation from `t178d` adds `0` chains. The early-open prefix preserves hygiene but does not unlock new tail-safe capacity beyond the existing `0.925` band.
- Implementation implication: the next useful operator is not another tail closure probe. It needs to create a real `corner_release_contract` or choose a root/canvas/structure family where this corner cluster has a dependency-compatible non-open basin before BCL/closure materialization. If using c027, the reservation must happen at root/seedState/topology level, not after the current seedState/BCL pipeline.

# Generated-Root WBP Decision: Dirty Tail Cells Need Earlier Reservation, Not Late Bundle Repair - 2026-07-03

- Decision: for c027-style high-capacity rows, the remaining dirty tail cells should be handled by early duty reservation/rewrite or by regenerating the root/structure state, not by repeatedly appending late open chains or small late support bundles.
- Evidence: t174 shows direct open placement of `(5,0;5,1)` can keep the board officially solved but fails Tail Hygiene through `global_dependency_follow_run=12`; forcing it earlier inside the late-tail sequence does not fix the spine risk.
- Evidence: t175a finds owner-release variants for `(5,0;5,1)` through owners `79` and `9`, but all non-open variants are Greedy-unsolved as single additions from the clean `t174l` context. The only Greedy-solved option is the already-known dirty open chain.
- Evidence: t175b/t175c test dirty owner-release plus one disjoint support chain from both clean `t174l` and earlier `t174b`; both produce `0` accepted candidates, with all sampled bundles rejected as `greedy_unsolved`.
- Evidence: t176d/t176e move the same test back to the earlier `t173j` base at coverage `0.9068826`. The cluster still has `0` accepted non-open single-chain forms, and dirty owner-release plus one support chain produces `0` accepted candidates (`greedy_unsolved:102`).
- Implementation implication: this is a forward-planning failure, not a local rerank failure. The next product attempt should reserve or rewrite this tail cluster before the board reaches the near-full tail state, and the scheduler should expose dirty-tail cells as pre-materialization duties with anti-spine / release-owner diversity constraints.

# Generated-Root WBP Decision: Use Micro-Wave Re-Enumeration For Low-Coverage Causal Roots - 2026-07-03

- Decision: for low-coverage high-causal roots like c038, the unified scheduler should use many small preplanned micro-waves with re-enumeration and tail-solved lookahead, not large 8/12-chain coverage waves.
- Evidence: c038 large-wave runs `t171d/t171e` fail at wave1 with rawOptions `60` but bundleCount `0`, while single/micro-wave runs climb through `0.716`, `0.755`, `0.781`, `0.799`, and current best `0.8299595`.
- Evidence: `t171v` reaches coverage `0.8299595`, official `A/A`, Hard-Core Window pass, Tail Hygiene pass, and late lookahead still positive. By contrast, the t142h/cross-basin high-coverage route `t171c` has zero tail-safe options near coverage `0.864`.
- Decision: hard local-touch caps are not valid anti-local controls by themselves. Global and late `max-local-touch=2` variants starve capacity, so anti-local must be soft or expressed through release-owner diversity, head-region diversity, support/dual-gate reservation, and tail-safe lookahead.
- Implementation implication: make micro-wave scheduling an explicit UDG mode and keep it inside the single duty scheduler. Do not turn it into a second-stage filler or a separate post-hoc repair pass.

# Generated-Root WBP Decision: Soft History Diversity Is A Brake, Not A Capacity Engine - 2026-07-03

- Decision: cross-wave history diversity should remain a default-off soft scoring layer used to reduce spine/local risk, not the primary route to `0.95`.
- Evidence: `t171ac_c038_crossbasin_micro12_historysoft` keeps official `A/A` and Hard-Core/Tail pass, and raises hardStructureV3 versus old 12-wave rows, but coverage drops from old `~0.755` to `~0.743`.
- Evidence: `t171af_c038_crossbasin_micro16_historysoft` keeps official `A/A` and Hard-Core/Tail pass at coverage `0.7773279`, but still trails old `t171m` coverage `0.7813765` and does not beat the best old hardStructureV3. Its positive is lower dependencyFollowRun in one row (`4`), not new capacity.
- Implementation implication: use soft diversity as a tie-breaker/risk guard near the tail, while the next capacity step must come from reservation/dual-gate/future tail-safe planning or a better root/structure family matrix. Do not keep increasing diversity penalties as a coverage search strategy.

# Generated-Root WBP Decision: Structure Templates Must Preserve Option Diversity - 2026-07-03

- Decision: use `hub/spiral/patchwork/cross-basin` as pre-materialization structure-template/reservation controls, but do not implement them as large scalar rewards that reorder the whole option pool. Strong template scoring can crowd the top pool with overlapping or Greedy-incompatible local options and produce false `0 candidate` results.
- Evidence: early `t170e/t170f/t170g` patchwork runs produced `0` rows, but calibrated reproduction showed the issue was not patchwork impossibility. With `coverage-option-early-stop` restored and gentle template reward, `t170n_patchwork_gentle_cov12_4_4_1` reaches the known `0.8684211` capacity node with official `4/4 A/A`; `t170o_crossbasin_gentle_cov12_4_4_1` does the same.
- Evidence: product verifier at formal `minCoverage=0.95` reports `Overall Fail` only for coverage; hard-core/tail remain viable. Cross-basin gives `4/4 Hard-Core Pass + Tail Pass`; patchwork gives `3/4 Hard-Core Pass + 4/4 Tail Pass` and includes one more topology-shifted row with prior-wave release count `2`.
- Evidence: `t170t_structure_template_invariance_audit.csv` classifies `t142h -> t170n`, `t142h -> t170o`, `t145e -> t170n`, and `t145e -> t170o` all as `TailPreservesHardCore`. Gentle structure templates do not pollute hard-core, but they also do not break the `0.868` capacity ceiling.
- Evidence: future-lattice and wave-shape probes (`t170v/t170w/t170x/t170y`) do not break the `0.868` ceiling. The t170z reject audit shows the final sampled tail options fail because they make the board `greedy_unsolved` (`160/160` sampled options), not because they exceed max choices. This means late tail cells are solver-poisoned, not just scarce or too easy.
- Implementation implication: the next scheduler change should be reservation/bucket-aware template planning: keep template-aligned candidates in competition with non-template capacity candidates, enforce per-region/per-owner diversity, and reserve future tail-safe cells. It also needs early tail-closure owner / solvability-preserving reservation. Do not raise template/future-lattice reward or split into many one-chain tail waves as the main mechanism.

# Generated-Root WBP Decision: Search Hard-Core Invariance Under Tail Fill - 2026-07-03

- Decision: the next product route is `hard-core invariance search`, not more single-root coverage search. A `0.95+` row is acceptable with an easier tail only if the front/mid hard-core window remains visible after tail extension and tail hygiene passes.
- Decision: tail fill must be audited as a before/after structural intervention. Compare hard-core-window dependency entropy, region entropy, branch diversity, support/contract evidence, and spine risk before vs after the tail; classify the route as tail-preserving, tail-eroding, tail-spine-risk, root-core-insufficient, or needing step diagnostics.
- Decision: do not count parameter-only UDG profiles as proof of a structure family. The smoke matrix labels `hub`, `spiral`, `patchwork`, and `cross-basin` must correspond to pre-materialization structure-template/reservation inputs, not just scoring weights after the same option pool is formed.
- Evidence: t169 shows high coverage solved rows can wash out hard-core (`competitor_v7_coverage095`), while generated-root c043/c038 rows preserve hard-core below product coverage. The missing proof is not "can add cells", but "can add cells without erasing the hard-core window".
- Evidence: t170 re-traced t145e with official step diagnostics and classifies `t142h -> t145e` as `TailPreservesHardCore` despite official `LocalEasy`: coverage rises `0.751 -> 0.868`, hard-core score drops within tolerance (`0.921 -> 0.831`), dependency entropy and branch diversity rise, and tail hygiene passes. This makes t145e/root154 a valid tail-safe capacity node, not a final product.
- Evidence: t170d preflight shows the current UDG can only approximate `patchwork`/`cross-basin` and blocks `hub`/`spiral` as real structures. Running the 32 rows immediately would overstate capability; the correct next step is a structure-template/reservation layer in the same scheduler.
- Implementation implication: every root/structure smoke must emit the same three product gates (`Overall`, `Hard-Core Window`, `Tail Hygiene`) and an invariance delta before visual review. The search matrix should vary root, structure family, canvas, and duty plan instead of drilling one current root family.

# Generated-Root WBP Decision: Use Product Family Matrix Before Single-Root Optimization - 2026-07-02

- Decision: after the High-Coverage Hard-Core Product goal pivot, root/family selection must be matrix-driven. Do not continue optimizing one current root unless it is justified by the family matrix against other root/body styles.
- Evidence: t169 product family matrix over 17 families / 31 trace rows produced `0` ProductPass. The only `0.95` solved row (`competitor_v7_coverage095`) is `HighCoverageCoreWashedOut`, while current generated-root WBP rows preserve root/preplan and hard-core/tail only up to about `0.82`.
- Evidence: c027/t158 and seed-maze references reach higher solved coverage (`~0.90-0.917`) with hard-core/tail signals, but they lack generated-root identity and pre-materialization evidence. This means the likely breakthrough is not more c043 coverage drilling, but finding or creating a generated-root family with c027/maze-like capacity while preserving root/preplan.
- Implementation implication: every new candidate route should be added to `Build-GeneratedRootWBPV12ProductFamilyMatrixV1.py` spec/output before visual review. A route can be advanced only if it improves at least one product dimension without washing out hard-core: solved coverage, root/preplan evidence, hard-core window, tail hygiene, or spine/local risk.

# Generated-Root WBP Decision: Product Gate Is Hard-Core Window Plus Tail Hygiene, Not All-Trace A - 2026-07-02

- Decision: Generated-Root WBP final product acceptance should target `High-Coverage Hard-Core Product Level`: coverage `>=0.95`, official solved, generated root preserved, pre-materialization duty evidence preserved, and a real hard-core window retained in the first `60-75%` of solve trace. Overall process may be `B` if the hard-core survives and the tail is hygienic.
- Decision: split product validation into three gates: `Overall` (coverage/solved/root/preplan), `Hard-Core Window` (low-choice `1-2/2-3`, region/direction/remote switching, after-local frontier break, and at least one choke/support/cross-basin/dual-gate/parent-edge contract), and `Tail Hygiene` (no long outer same-side sweep, no single dependency spine, no material worsening of dependencyFollow/localPatch, no loss of root/support identity).
- Evidence: t167 calibration shows c043 `t165d` can be global `B/B` while still retaining a hard-core window and passing tail hygiene; under the new goal it is not a categorical failure, only coverage-incomplete. In contrast, c043 `t165m` has a hard-core window but fails tail hygiene through `dependencyFollowRunMax=11`, matching the t166 spine-risk diagnosis.
- Evidence: c027 `t158m` is the closest current prototype for the new product shape: official `B/B`, coverage `0.9068826`, hard-core window at trace steps `31-50` (`~37%-60%`), and tail hygiene pass. It still fails the product gate because coverage is below `0.95` and its manifest lacks root/pre-materialization evidence.
- Implementation implication: do not keep optimizing for all-trace `A`; optimize for `0.95+` solved rows that preserve a front/mid hard-core window and pass tail hygiene. Any tail-fill route must carry root identity and pre-materialization duty fields through its final manifest.

# Competitor Hard Decision: V7-Style Fill On V10 Is Visual-Only, Not A Valid Closure Route - 2026-07-02

- Decision: do not use V7-style tail/patch fill on top of V10 as a production or hard-level route. Keep V11 as a visual diagnostic only.
- Evidence: strict V7 `multi_tail_close` adds `0` cells to V10 because the first full tail wave makes the board unsolved and is rolled back. Relaxed/forced tail growth can make the board visually dense, but official trace marks both V11 rows `solved=False`, `Drop/Drop`, `WeakCausality`.
- Evidence: forced visual V11 reaches `0.950-0.951` coverage, but hardStructureV3Score collapses to `0.027`, frontierDrainRemoteChokeCount becomes `0`, and supportClosureBestDepth becomes `0`.
- Implementation implication: V7's success depended on starting from an already high-coverage V3 exterior; it is not a general solver-preserving closure operator for V10. The next real route still needs pre-interval region/body-basin planning, not late visual tail/patch closure.

# Generated-Root WBP Decision: Player-Stall Must Split Structural Stall From Spine Stall - 2026-07-02

- Decision: do not treat dependency spine dominance as the sole difficulty signal. Generated-Root WBP validation must separate `structural stall` from `spine stall`.
- Evidence: t166 dependency spine audit on t165 rows shows simple top-1 path share does not explain process drop. A rows (`t165a` c043 and `t165j` c038) already have high top-1 path share around `0.48-0.52` and dominant component share around `0.94`, yet remain process `A/A` because support closure is still structurally effective.
- Evidence: c043 `t165d` reaches coverage `0.8198381` but drops to `B/B`; its top-1 path share is lower (`0.246`), so the failure is not more spine dominance. The actual signal is support erosion: hardV3 falls to `0.139`, structuredHardness to `0.573`, and localPatchRun rises to `5`.
- Evidence: c043 `t165m` anti-follow negative proves the separate spine risk path: top-1 path share `0.485`, dependencyFollowRun `11`, localPatchRun `5`, and the new verifier labels it `StructuralSupportEroded + SpineRiskWeakStructure`.
- Implementation decision: `DifficultyVerify` now emits `structuralStallClass/Score` and `spineStallClass/Risk`, with optional join from `DependencySpineAuditV1`. `PlayerStallAuditV1` now has the same split via `--spine-audit-csv`; default player-stall pass requires spine risk `<=0.55`, so `SpineRiskBalanced` dependency-follow rows become Review even if their low-choice window is strong.
- Validation implication: a good player stall must pass both sides: structural support (`StructuralSupportClosure` or better, or an explicit structural partial with strong remote/choke evidence) and controlled spine. Do not use top-1 path share alone as a reject rule because good A rows can have high top-path share; use it as part of the spine risk score together with dependency-follow/local/sweep continuity.
- Scheduler implication: anti-spine alone is not a valid next route. The scheduler must preserve support closure / reserve dual-gate control slots while also limiting long low-choice dependency-follow runs and over-reuse of recent release owners/regions. Coverage climb should resume only through these front-end constraints, not by more late basin stages.

# Competitor Hard Decision: T145 Early Connector Planning Beats Post-Fill, But Needs Region Materialization - 2026-07-02

- Decision: keep the t145-style early control-surface route for competitor-hard generation, but do not treat V10 as a final high-density solution. It should supersede post-V8 WBP fill as a structural diagnostic because it restores `MediumStructure`, but coverage still stalls near V8 scale.
- Evidence: V10 starts from the solved V4 SkeletonPSG connector and plans BCL/t145 owner-hit structure before interval density. Official trace solves both rows as `MediumStructure`; best row `ccsf_v10_001_ccsf_v4_001_9142301_p1_bcl8_interval` is `A/A`, difficultyScoreV2 `0.697`, hardStructureV3Score `0.202`, openers `3`, avg/max choices `2.72/6`.
- Evidence: the static wave audit shows the early BCL layer only moves coverage from `0.468` to `0.519`; interval waves climb to about `0.700` and then stop at `small_interval_wave`. This proves the t145 control surface improves structure, but owner-hit BCL plus interval fill does not create enough body capacity for the user's `~0.95` target.
- Boundary: do not continue by widening post-fill close bundles, copying competitor screenshots, or validating one chain/few chains at a time. Those paths either reproduce LocalEasy filler or lack a planned board-level grammar.
- Implementation implication: next competitor-hard generator should introduce a pre-interval body-basin / room-slot / support-control materializer. The generator must allocate multi-cell region duties and support owners from the V4 connector or semantic core before interval density, then validate the completed board and contracts.

# Generated-Root WBP Decision: Do Not Scale Static Full-Slot Beam For Low Prefixes - 2026-07-02

- Decision: do not treat `Build-GeneratedRootWBPV12FullClosureSlotPlannerV0.py` as the next production route for low-coverage prefixes by simply widening pool/beam/search. It remains a diagnostic tool.
- Evidence: f018 lower-prefix probes show static full-slot V0 can do small late exact steps (`t156o -> 0.834`) but fails from `t156k/t156m/t156o` to `0.84/0.86`, from `t156c` to historical `0.720`, and from `t156s_c005` to `0.86` even with maxLen6 smoke.
- Evidence: historical `t156e` proves `t156c -> 0.7206478` is possible by staged BCL. The winning cells are visible in V0's maxLen6 option audit but rank deep, and the solved combination is lost by high-score beam/final-plan selection.
- Implementation implication: the next Generated-Root WBP coverage climb should be staged re-enumeration: commit a small solved bundle, re-enumerate first-hit slots, preserve future owner/space reservations, and only then continue. Static one-shot full-slot planning from low coverage has too many false-positive complete plans.

# Generated-Root WBP Decision: Closure-First Solved Staged Basins Are The Current Root-General Route - 2026-07-02

- Decision: upgrade the Generated-Root WBP route from "reserve basins before closure" to "materialize required closure/hard-body duties first, then add coverage basins in solved stages under preplanned release policy." This still counts as duty-first planning because each basin stage is selected as a planned batch before chain cutting and must pass whole-board Greedy before the next stage.
- Evidence: t164 closure-visibility audit shows every tested pre-closure basin reservation plan preserves `cmp7/cmp11` but kills `cmp2` visibility. Even a single protected reservation from the current owner-hit basin grammar makes `planned_danger_outer_body cmp2` invisible, so generic basin reservation cannot precede `cmp2`.
- Evidence: after the c043/c038 closure3 bases (`t160t/t160u`), requiring `--require-plan-greedy-solved` unlocks two-basin solved plans that previous ranking missed. The old failure was not pure capacity absence; high-scoring geometric options were unsolved and polluted the beam.
- Decision: treat unsolved prefixes as invalid for staged basin growth. Do not keep them as candidate state just because first-hit contracts look stable or coverage improves.
- Decision: cap coverage-basin self-release. A small amount of new-owner release can be useful, but consecutive newly added basin owners create dependency-follow/local-run collapse. Current accepted policy is `--max-new-release-owner-uses 1 --avoid-consecutive-new-release-owner --max-release-owner-reuse 2`.
- Evidence: ungated c043 staged basin reaches `0.7955466` but official trace drops to `B/B`; policy-gated c043 reaches `0.7550607` with official `A/A` and DifficultyVerify `HardPotential 0.795`. The same policy generalizes to c038 at `0.7530364`, official `A/A`, DifficultyVerify `HardPotential 0.797`.
- Boundary: the current route is a root-general breakthrough around `0.75` coverage, not the final `0.95+` endpoint. Remaining blockers are `no_strict_dual_gate` and `dependency_follow_run_risk`, so next work should add strict dual-gate / anti-follow duties before further coverage climb.

# Generated-Root WBP Decision: Root-General Generator Requires Region-Duty Rewrite Before More Coverage Basins - 2026-07-02

- Decision: keep the root-general generator goal split into root capacity, region/cell duty graph, semantic materialization, TraceGate, DifficultyVerify/PlayerStall, and coverage climb. Coverage climb is not allowed to become a post-hoc filler route; it must be committed as duty/reservation before chain cutting.
- Implementation decision: keep coverage-basin future-capacity lookahead inside `Build-GeneratedRootWBPV12EarlyClosureDutyActivationBeamV2.py` as an opt-in audit/scoring tool. Defaults remain off so earlier t160/t161 behavior is preserved.
- Evidence: t162 geometry lookahead can see future basin geometry after candidate placement (`futureCoverageOptions` up to `19`, future owners up to `7`), but no two-basin solved bundle appears from the current c043 prefix.
- Evidence: continuing from the accepted t161f prefix still produces `0` solved ActivationBeam bundles for the second basin even when owner0 is allowed; the only known continuation is BCL exact1 at deep ranks `75/111`, release owner `0`, coverage `0.6842105`.
- Boundary: this proves the second-basin issue is not simply lack of geometric empty space or lack of option ranking. The current root/region state has already consumed clean release capacity; chasing deeper owner0 afterfill would re-enter the old LocalEasy/filler route.
- Implementation implication: the next generator change should reserve/rewrite second-basin space before the first basin and early closure duties are materialized, probably by promoting coverage basins to region-level duty units in the root/region planner. Do not treat t162 outputs as review levels or continue widening the current beam as the main path.

# Generated-Root WBP Decision: Coverage-Basin Supply Must Be Planned In Activation, Not BCL Afterfill - 2026-07-02

- Decision: keep coverage-basin selection inside ActivationBeam V2 as the correct next control surface. Do not revert to post-prefix BCL as the primary coverage mechanism.
- Evidence: t161f/t161g automatically add one non-owner0 coverage-basin chain (release owner `35`) together with the early semantic duty bundle, reaching coverage `0.6740891` on both c043 and c038. This proves coverage supply can be committed before materialization and is not inherently owner0-only.
- Evidence: post-t161 BCL exact1 still only uses owner `0`, and exact2 remains `0`. Therefore BCL can be used as a diagnostic continuation, but it should not define the generator route.
- Boundary: adding two coverage-basin chains still fails even with owner0 allowed, shorter basin chains, or unsolved-prefix search. The second basin requires earlier region/space rewrite, not more beam width.
- Implementation implication: next work should turn coverage-basin from "append one owner-hit supply chain" into region-level basin reservation/rewiring before early closure selection, so the second coverage basin has clean release capacity before the first basin consumes the local geometry.

# Competitor Hard Decision: Post-V8 WBP Fill Is Too Late For 0.95 Hard Density - 2026-07-02

- Decision: do not continue the route of applying WBP/T111 protected fill after V8's dense interval-duty body. Keep V9 as a diagnostic negative, not as a review/acceptance pack.
- Evidence: V9 starts from the best V8 row at coverage `0.711` and reaches only `0.765` with `4` accepted bundles / `8` chains. The V4/V8 connector contract is preserved (`3/3` first-hit and `3/3` corridor-clear), but all accepted bundles are `open_close`; anchored and mixed phases mostly fail as `greedy_unsolved`.
- Evidence: official trace solves the row as `B/B`, but still classifies it `LocalEasy` with hardStructureV3Score `0.126`. The good signals (`frontierDrainRemoteChokeCount=11`, choice-choke-after-local-frontier-break `4`, support depth `4`) do not overcome local/dependency-follow collapse.
- Interpretation: T111's `0.953` coverage path worked because protected fill began from a semantic root while future capacity was still available. V8 already spends the corridor/body space, so adding WBP afterward can only add open-close closure, not the anchored/mixed coverage basin needed for dense hard structure.
- Implementation implication: next competitor-hard generator should start from the V4 SkeletonPSG connector or semantic core and plan WBP-style coverage basins, support closure, and anti-local slots before interval-duty density is materialized. Do not try to rescue V8 by widening post-fill search, and do not return to one-chain/few-chain validation.

# Generated-Root WBP Decision: Activation-Aware V2 Is Valid, But Coverage Basin Must Move Forward - 2026-07-02

- Decision: keep ActivationBeam V2 as the next early-closure materializer control surface, but do not use ordinary BCL continuation as the next coverage route after the V2 prefix.
- Evidence: t160 owner-diverse solved-prefix auto-shortlist reproduces the V1 2-chain result and improves same-family c038/c043 to a 3-chain semantic prefix. Both `t160t` and `t160u` reach coverage `0.6639676` with release owners `19/22/20` and include a `future_release_guard_cluster`, so this is not just path-chunk filler.
- Evidence: the same c038/c043 V2 prefixes still have no BCL exact2 capacity; BCL exact1 succeeds only through release owner `0` and reaches `0.6740891`. This means the current prefix still leaves only one narrow coverage outlet.
- Boundary: widening static bundle search, broad unsolved-prefix beam, or one-chain BCL continuation is the wrong next step. They either time out, produce no solved bundle, or reproduce the old one-outlet behavior.
- Implementation implication: the next generator step must plan coverage-basin / future-release supply inside the activation scheduler before chain materialization. Coverage must be a first-class duty alongside early closure, not a post-prefix BCL layer.

# Generated-Root WBP Decision: Root-Generalization Requires A Stronger Materializer Than V1 - 2026-07-02

- Decision: keep the root-general WBP route, but treat current early-closure V1 as a diagnostic/materializer baseline rather than the final generator core.
- Evidence: t159a matrix proves the direct same-pipeline batch is not c027-only at the root/seed/projection layers. `root154_core_sched0589_v1_r3_c027/c038/c043` are all stage1 roots; c038/c043 seed-only rows solve at `0.6397-0.6417`, and their early-space debt/duty projection is nearly identical to c027.
- Evidence: V1 materialization does not fully generalize. c038/c043 only reach 1 closure chain before patch, and 2 closure chains with `--candidate-scan-options 24`, while c027 t156c reaches 4 closure chains and `0.6801619`.
- Implementation implication: the next useful work is a bundle-aware / activation-aware early closure V2 that can select mutually supporting guard/corridor duties and alternate release owners. Do not continue tuning c027-specific duty order or call same-family seed/projection success a complete root-general generator.

# Competitor Hard Decision: V8 Restores Structure But Exposes A Whole-Batch Capacity Ceiling - 2026-07-02

- Decision: use `CompetitorCoreSkeletonPSGBatchV8Pack` as the current correct-route structural review pack. It supersedes V7 for hard-structure review, while V7 remains only a high-coverage visual boundary.
- Evidence: V8 uses V4 skeleton + SkeletonPSG corridor contracts and whole interval-duty waves. The generated rows preserve connector contracts (`3/3 firstHit`, `3/3 corridorClear`) and official trace solves `4/4`, with `3 MediumStructure + 1 LocalEasy`; the mounted pack keeps only the three MediumStructure rows.
- Evidence: mounted V8 rows have official `A/A` or `B/B`, max choices `6-8`, frontierDrainRemoteChokeCount up to `11`, and solveTraceQuality around `0.806-0.864`, avoiding the V7 pattern of `frontierDrainRemoteChokeCount=0` and tight `Drop`.
- Boundary: V8 coverage is only `0.694-0.711`. The whole-batch planner stops at `small_wave_capacity`, so this is not the final competitor-density result.
- Implementation implication: the next improvement should increase pre-materialized corridor/topology capacity and bring more V4 sources into solved connector state before fill. Do not raise coverage by reintroducing V3 rank-field background, tail closure, direct SeededSGP, or any one-chain/few-chain validate loop.

# Competitor Hard Decision: Reuse SkeletonPSG Corridor-Duty Logic For The Next V4-Based Route - 2026-07-02

- Decision: the next competitor-hard attempt should reuse the existing SkeletonPSG corridor/duty pipeline from `.worktrees/sgp-rhythm-lab` rather than continue V3/V7 rank-field density or tail-closure fill.
- Evidence: `Build-SkeletonPSGFeasibilityV1.py` runs directly on the current V4 formal skeleton report and marks all three V4 rows as `PSGConnectableNeedsCorridorWavePlanner`. Strong connector capacity exists (`108-110` strong slots per row), but `immediateStrong=0`, so the pressure graph depends on preserving line-of-sight corridors.
- Evidence: `Build-SkeletonPSGCorridorWavePlanV1.py` can select real critical corridor units from V4 under strict no-overlap, and `Build-SkeletonPSGCorridorConnectorCutterV1.py` materializes them as actual chains with static `firstHit`/`corridorClear` contracts passing.
- Boundary: this is not a finished high-coverage pack. The strict V4 corridor plan currently selects only `3-5` units per row, so reaching `0.95` still requires scheduled DAG/topology fill that preserves those corridor contracts and avoids filler opener explosion/dynamic ray pollution.
- Implementation implication: treat selected corridor units as hard semantic contracts, then use/adapt `Build-SkeletonPSGScheduledDAGFillV1.py`, `Build-SkeletonPSGSolverTopologyV1.py`, and `Build-SkeletonPSGTopologyRealizerV2.py` style logic for coverage. Do not call generic dense fill, direct SeededSGP, or multi-tail closure a valid hard route unless contract audit and official trace stay clean.
- User constraint: the next generator must not be a one-chain/few-chain incremental validate loop. It should preplan a whole wave/topology/cell-plan, materialize the planned batch as a coherent board, then validate the completed board and contract surfaces.

# Competitor Hard Decision: V7 Meets 0.95 Coverage But Is Visual-Coverage Only - 2026-07-02

- Decision: mount `CompetitorCoreCoverage095V7Pack` for high-coverage visual review because it reaches the user's requested `~0.95` coverage and has one official-solved row.
- Evidence: selected row `ccsf_v7_002_ccsf_v3_008_8638894_ne_cov095` is `24x30`, `132` chains, `684` arrows, coverage `0.950`, official `solved=True`, process `B`.
- Boundary: do not call V7 hard. Official trace classifies the mounted row as `LocalEasy`, tight process `Drop`, with `frontierDrainRemoteChokeCount=0`. The other generated row is not mounted because official trace reports `solved=False`.
- Implementation implication: coverage closure by dense rank-field exterior plus multi-tail closure can hit the visual target, but it erases the hard skeleton signals. The next true-hard route must reserve remote choke/support duties inside the 0.95 body plan instead of closing coverage after a LocalEasy high-density exterior.

# Competitor Hard Decision: V6 Is The Current Viewable Whole-Plan Pack, Not The Final Hard/Dense Endpoint - 2026-07-02

- Decision: mount and review `CompetitorCoreSkeletonWholePlanV6Pack` as the current viewable competitor-hard attempt. It is the first pack using the full route: approved hard skeleton, rank-interval density planning before materialization, and batch-pruned background density.
- Evidence: V6 reaches coverage `0.740-0.752` with `78-95` chains and official trace solves `3/3`. The first two mounted rows are `MediumStructure`; the third row is kept as a boundary sample.
- Evidence: visually, V6 is denser and more maze-room-like than V5 while preserving the V4 skeleton as a visible blue core in previews.
- Boundary: V6 loses V5's tight process: official process is `1 A + 2 B`, tight process is `Drop` for all, and hardStructureV3 is `2 MediumStructure + 1 LocalEasy`. It is a human-review pack, not a final trace-hard approval.
- Implementation implication: high coverage must become structural, not only visual background. The next generator should convert the background density wave into planned cross-basin/remote-choke duties with rank intervals, instead of overfill/prune background chains after the hard skeleton.

# Competitor Hard Decision: V5 Preserves Hard Skeleton Under Overall Planned Closure But Is Not The 0.9 Density Endpoint - 2026-07-02

- Decision: keep `CompetitorCoreSkeletonClosureV5` as the current formal review pack for the approved V4 skeleton closure route. It replaces V4 as the mounted Demo pack for human review.
- Evidence: V5 uses the user-approved V4 skeleton anchors and fills by whole-board planning: topology-safe multi-tail waves, batch U-detours, and rank-interval dependent field-chain waves. It does not use screenshot copying, seed topology reuse, or one-chain-at-a-time filler acceptance.
- Evidence: official trace solves `3/3`; all rows are process/tight `A/A`, with hardStructureV3 classes `1 TrueHardCandidate + 1 HardPotential + 1 MediumStructure`. Best row `ccsf_v5_005_ccsf_v4_001_9142301_cl1` is `TrueHardCandidate` at coverage `0.644`.
- Boundary: V5 coverage is only `0.644-0.647`, so it is not competitor visual parity with the `~0.9` dense maze-room references. Do not present it as the final density solution.
- Implementation implication: the approved skeleton can survive overall closure, but late closure has a hard capacity ceiling. The next route to true competitor density should plan high-coverage body capacity before materialization, while preserving V4-style remote/cross-basin skeleton duties and official A/A trace signals.

# Generated-Root WBP Decision: A Few Easy Openers Are Acceptable Only If Midgame Stall Survives - 2026-07-02

- Decision: for Generated-Root WBP review, opener count is a tolerance gate, not the difficulty definition. Opener `5` is acceptable by default, and opener `6` can be reviewed when the user explicitly accepts a few easy opening clears and the trace still has a strong midgame stall.
- Evidence: `t158f` reaches coverage `0.8886640` with openers `5`, official `B/B MediumStructure`, max choices `5`, and PlayerStallPass `0.790`. Its best low-choice window is 15 steps and remote choke count is `13`.
- Evidence: `t158m` reaches coverage `0.9068826` with openers `6`, official solved `B/B`, max choices `6`, PlayerStallPass `0.780` under `--max-openers 6`, and DifficultyVerify `HardPotential 0.677` at the intermediate `B/0.90` gate.
- Boundary: this does not relax the final `0.95+` coverage goal or final hard-structure goal. The open-anchor closure route currently ceilings around `0.90-0.91`; it proves the play-feel acceptance is viable, but not that late open fill can finish the level.
- Implementation implication: future WBP closure should use early planned open anchors/delayed owner chains as capacity sources, then preserve the PlayerStallAudit window. Do not continue by adding unlimited direct-open filler, and do not call official `LocalEasy` a final success without the separate player-stall and DifficultyVerify context.

# Generated-Root WBP Decision: Player-Stall Is A Separate Acceptance Layer From Official LocalEasy - 2026-07-02

- Decision: accept a few easy opening clears if the level creates a meaningful mid-game stall. Do not reject a candidate solely because openers are 3-5 or official `HardStructureV3Class` says `LocalEasy`.
- Evidence: t156s/t157e candidates have openers `3`, max choices `4-5`, and official process `B/B`; official structure often remains `LocalEasy`, but traces show long mid-game windows with only 1-2 options, cross-region switches, direction breaks, and remote choke counts up to `12`.
- Decision: use `PlayerStallAuditV1` as a review/selection layer alongside official trace and DifficultyVerify. It is not a replacement for final coverage/root/process gates; it answers the user's play-feel question: "will this make a player stop and read?"
- Boundary: t157 proves player-stall can survive low-impact BCL filling to `0.8502`, but BCL is exhausted there. Further progress toward `0.95+` must use a new closure/openRay/bridge operator that preserves the stall window.

# Generated-Root WBP Decision: Early Closure Restores Capacity, But Late BCL Plateau Requires Bundle-Aware Duty Materialization - 2026-07-02

- Decision: keep the c027 early-closure route alive. It is no longer a speculative patch: t156 proves a generated root plus early closure duties can restore owner-hit coverage capacity that was previously unavailable.
- Evidence: `t156c` raises c027 seed-only coverage from about `0.6417` to `0.6802`; subsequent short BCL waves reach `0.8421` while preserving root identity and staying Greedy/official solved on traced samples.
- Evidence: the t156o trace sample is `4/4` solved, process/tight `B/B`, max choices `5`, and DifficultyVerify classifies all four rows as intermediate `HardPotential` once pre-materialization evidence is propagated.
- Decision: do not continue treating BCL as the endpoint. By t156s, BCL still raises coverage but space debt no longer improves (`debt=12`, openRay still `6`, raw options down to `14`). This is a plateau signal, not a full-board success.
- Decision: the next materializer must be bundle-aware. V1 can only accept one closure-duty chain if that chain is independently Greedy-solved. Remaining high-priority corridors have first-hit-matching paths, but every single-chain candidate is Greedy-unsolved; `cmp16` is a one-cell duty that needs a bridge. Therefore V2 must commit small multi-chain duty bundles per component before validation.
- Implementation implication: do not add a second filler system. Extend the same duty pipeline so remaining open-ray/danger boundary components are assigned as component-level contracts, then materialized by a bounded beam of small chains. Official trace/DifficultyVerify remain final gates, not per-chain generation drivers.

# Generated-Root WBP Decision: Root Selection Is Necessary But Not Sufficient; Closure Duties Need A New Materializer - 2026-07-02

- Decision: keep the t155 root-plan viability selector as the next gate before heavy Generated-Root WBP generation. Do not feed arbitrary roots into UDG just because they have high coverage or a high root hard score.
- Evidence: `t155a` found only 3 same-frame roots ready for native UDG. The best is `root154_core_sched0589_v1_r3_c027`, with coverage `0.6052632`, chain/reserve `5/5`, and a clean control slot at `18,11;18,12`.
- Evidence: c027 plus the old t142 seedState is legal and solved as seed-only (`t155e`, coverage about `0.641`, max choices `6-7`), but still has early-space debt around `69-70` cells. This means root selection helps, but old seedState plus old coverage grammar does not solve closure.
- Evidence: `t155g` projects the remaining space into explicit closure duties. `t155i` then proves current BCL owner-hit options are mostly misaligned with those duties: the highest closure corridor depends on owners `38;17`, with zero overlap from visible BCL options; the control-slot owner is visible but consumes the wrong cells; one danger body duty is fully invisible.
- Decision: do not continue by increasing BCL max length or wave size. A `maxChainLength=12` single-chain visibility probe timed out, and this also violates the target language of many short/medium semantic chains.
- Implementation implication: the next accepted generator primitive should be a duty-specific early closure materializer: it must consume closure-duty components, reserve control slots, assign release owner/window, and produce short/medium chains before generic coverage waves. BCL remains a secondary coverage primitive only after the closure duties are committed.

# Generated-Root WBP Decision: Full Coverage Closure Is Not Enough Without Official Process Structure - 2026-07-02

- Decision: do not promote `f013g` or `f013h` as target baselines even though they reach `0.95+` coverage and are official solved.
- Evidence: `f013g` reaches coverage `0.9500000`; `f013h` reaches `0.9518519`. Both preserve the protected prefix/root identity and solve officially, proving the remaining visual coverage can be closed mechanically.
- Rejection evidence: both rows remain process `B`, tight process `Drop`, `hardStructureV3Class=LocalEasy`, with no strict dual gate and no qualified support closure. `DifficultyVerify` rejects them because the official tight process/structure evidence collapses.
- Decision: use `f013h` only as a closure/feel review artifact and as proof that late open/proxy fill can finish coverage. It is not a Generated-Root WBP success under the current target.
- Decision: root control-slot readiness and early-space/closure capacity are now root-stage gates. A root that cannot reserve future control slots or safe closure cells before materialization should not be carried forward just because it can be late-filled.
- Implementation implication: the next route must combine coverage closure and structural contracts before chain cutting: control-slot reservation, support outlet preservation, closure-reachable components, and planned blockers/delays belong in the unified duty graph, not in a final repair/filler pass.

# Competitor Hard Decision: Two-Stage Core Skeleton Density Fill Is Viable But Not Final - 2026-07-02

- Decision: after rejecting seed-derived generation, the active competitor-hard route is now `core skeleton -> DAG-preserving density fill`. `CompetitorCoreSkeletonFillV2` is the current proof pack for this route.
- Evidence: V2 starts from V1's from-scratch dependency skeleton and then adds short room/corridor density chains only if the actual ray-blocker graph remains acyclic and bounded by solve-pressure gates. It does not copy screenshots or reuse seed topology.
- Evidence: V2 raises the candidate band from V1's `0.430-0.527` coverage to `0.608-0.662` with `54-74` chains while official trace still solves 6/6; hardStructureV3Class is `5 MediumStructure / 1 LocalEasy`.
- Boundary: V2 is not a final hard/competitor-equivalent pack. It still has only one `A/A` process row, no current `TrueHardCandidate`, and visual density remains below the high-coverage competitor references. The next accepted improvement should make the density filler trace-aware and push coverage without losing Medium/Hard structure.

# Competitor Hard Decision: Batch Rank-Field Fill Solves Coverage But Not Difficulty - 2026-07-02

- Decision: near-`0.9` coverage should be pursued with batch/wave construction, not V2's one-chain-at-a-time density acceptance. `CompetitorCoreSkeletonFillV3` proves that batch high coverage is feasible.
- Evidence: V3 generates `0.874-0.901` coverage rows with `128-134` chains and `112-118` bulk density chains per row; the density stage is rank-field construction, not per-chain validation. Official trace solves 3/3.
- Boundary: V3 is not accepted as hard. All traced rows are `LocalEasy`, with weak remote choke (`0-2`) and regular wave-front behavior. Treat V3 as a coverage mechanism proof only.
- Next implementation direction: keep the batch construction model but add batch-level remote-corridor, cross-basin, and staged gate duties before final validation. Do not regress to per-chain filler validation, and do not present high coverage alone as hard success.

# Competitor Hard Decision: V4 Formal Skeleton Replaces Rank-Field As The Hard Anchor - 2026-07-02

- Decision: use `CompetitorCoreSkeletonFormalV4` as the current formal skeleton anchor. It is not final density, but it fixes the V3 same-direction/rank-field flaw and produces official hard-structure evidence.
- Evidence: V4 official trace solves 3/3 with `1 TrueHardCandidate + 2 MediumStructure`; best row `ccsf_v4_001_9142301` is `A/A`, openers `4`, avg/max `2.89/6`, local/near `2/1`, remote choke `4`, and difficultyScoreV2 `0.712`.
- Boundary: V4 coverage is only `0.460-0.526`, so it is not competitor visual parity. Do not ship it as a dense competitor-like final level pack.
- Next implementation direction: keep V4's multi-direction hard skeleton and add a separate high-coverage closure layer that preserves official trace signals. Do not use V3's single rank-field fill as the final closure; it is allowed only as evidence that batch coverage can be mechanically achieved.

# Competitor Hard Decision: Core-Skeleton DAG Is The Active Route - 2026-07-02

- Decision: the active route for competitor-hard generation is now bottom-up core-skeleton generation with dependency-preserving fill, not seed-derived variants and not visual-only maze drawing.
- Evidence: `CompetitorCoreSkeletonFillV1` creates from-scratch chains, checks every added chain against the actual ray-blocker dependency graph, topologically sorts the DAG, and writes authored LevelDefinitions without reading seed topology.
- Evidence: the first proof pack official-traces 6/6 solved with all process/tight `A/A`; one row is `TrueHardCandidate` and five rows are `MediumStructure`, unlike the rejected visual-only variants that remained `LocalEasy`.
- Boundary: current proof coverage is only `0.430-0.527`, so it is not visually competitor-equivalent yet. Do not present it as final. The next decision point is whether a second-stage filler can raise density while preserving DAG structure and official hard signals.

# Competitor Hard Decision: Reject Seed-Derived Route, Return To Core-Skeleton Generation - 2026-07-02

- Decision: `CompetitorSeedMazeGrammarV1` is rejected as a solution route. It is not acceptable to build the competitor-hard lane by transforming existing seed topology, even when the visual distribution is closer.
- Reason: the required path is bottom-up generation: either explicitly build core chains / skeleton first and then fill around them, or derive the competitor logic at the rule level and generate it from scratch.
- Boundary: keep SeedMazeGrammar only as a diagnostic negative showing that seed-derived variants can recover visual maze-room shape but do not satisfy the user's route requirement or hard-structure proof.
- Next implementation direction: generate a small set of core/skeleton chains with planned dependencies, gates, release order, and remote choke roles; then add coverage/filler chains only if they preserve the skeleton's solve-order contract and visual maze-room density.

# Competitor Hard Decision: Separate Visual Grammar From Hard Structure - 2026-07-02

- Decision: `CompetitorSeedMazeGrammarV1` is a rejected visual diagnostic, not the corrected route. It can be used only as evidence about visual grammar, not as a baseline or proof of difficulty.
- Evidence: SeedMazeGrammar V1 generated 3 dense maze-room candidates that visually match the true contact sheets much better than short-rail, Gatehouse, or braid outputs; official trace solved 3/3 but classified all as `LocalEasy` with `B/Drop/Drop` process.
- Evidence: original true-reference seeds also trace as 5/5 solved but `LocalEasy`, so the hard-structure metric is stricter/different than visual competitor similarity. Visual match and hard proof must remain separate gates.
- Implementation implication: the next accepted competitor-hard generator must preserve maze-room seed grammar while adding explicit structural duties: cross-region gates, remote choke/frontier breaks, support closure, dual-gate-like control slots, or WBP-style interposers. Do not claim hard success from visual maze density alone.

# Competitor Hard Decision: True Target Is Maze-Room Seed Grammar, Not Short-Rail Or Stripe Prototypes - 2026-07-02

- Decision: the user's uploaded colored short-rail screenshot is a rejected negative, not a competitor target. Do not use it to define shape, layout, or success criteria.
- Decision: the current competitor-hard target is the true in-project competitor/contact-sheet distribution: high-coverage maze-room linework with nested rooms, corridors, U-turns, gates, pockets, and dense non-striped panels.
- Decision: `CompetitorShortRailReadV1` is invalid because it was built from the negative screenshot. `CompetitorMazeRoomReadV1` is also not an accepted candidate: the layered version is visually closer but trace-collapses into `Drop/LocalEasy`; the braid version lowers choices but is visually mechanical stripe/ladder output.
- Evidence: top project seed references such as `seed_Above300_level_844`, `seed_Above300_level_987`, `seed_Arrowz_level_199`, and `seed_AOut_level_089` already show the desired maze-room family with coverage about `0.91-0.93`, many medium chains, visible room/corridor structure, and nontrivial choice curves.
- Implementation implication: the next generator must learn/extract the maze-room grammar from true seeds/contact sheets before materializing new levels. Do not tune short rails, generic gatehouses, or low-choice braid strips forward.

# Competitor Hard Decision: Loose12 And Fresh Gatehouse Are Rejected Negatives - 2026-07-02

- Decision: the ScheduledBreak Loose12 tryout is a rejected negative after human review. Do not use it as a positive sample, proof seed, or generator promotion target; it reused already-rejected material and failed the requested shape/feel standard.
- Decision: `CompetitorMazeReadDemandV1` must start from fresh authored geometry or an explicitly approved skeleton/contract, not from repackaging old read-demand candidates.
- Decision: the fresh Gatehouse prototype in `.worktrees/competitor-hard-fresh` is also rejected. Do not tune it forward or call it a review candidate; it has neither the competitor visual result nor the intended logic/structure.
- Evidence: the current Gatehouse row `cmrd_gatehouse_v1_02_seed7302203` light-traces as solved `B/B` with avg/max choices `4.23/6`, but HardStructure V3 is `LocalEasy`, `frontierDrainRemoteChokeCount=0`, and `choiceChokeAfterLocalFrontierBreakCount=0`. Human review agrees this is not a false negative; `LocalEasy` is the correct rejection signal here.
- Boundary: max-choice control and visible staged gates are insufficient. The next route must be based on true competitor/seed maze-room grammar: dense non-striped panels, nested rooms, corridors, gates, pockets, and cross-region read order. Do not continue by adding generic room-fill, gate ladders, short rails, or braid stripes to Gatehouse.

# HoleMask Decision: Direct Nutation Must Treat Hole Blocks As Head-Ray Constraints - 2026-07-02

- Decision: for HoleMask + Nutation, the correct experiment route is direct constrained generation with the hole present as both `blockIndices` and `canSpawn=false` from the start. Do not use `Nutation finished seed -> mask crop -> repair/refill` as the route being evaluated.
- Decision: a center hole blocker is not only empty/no-spawn geometry; head escape rays must reject any direction that sees the hole block. Otherwise direct peel can fill well but still create arrows aimed into the hole.
- Evidence: first direct probe generated high-fill rows but `0` final accepts because every row had `holeBlockHits>0`. After moving blocker-ray rejection into head selection, the same direct family produced `5` Greedy-passing rows with `holeHits=0`, fill `0.973-0.981`, chains `97-144`, and bounded edge/axis runs.
- Acceptance gates for this probe: Greedy pass, `holeBlockHits=0`, chain/fill band, edge straight run `<=5`, axis same-dir run `<=9`, max straight run `<=17`, bounded initial clearable chain count. Human visual review remains required before production approval.

# Generated-Root WBP Decision: Safe Prefix Plus Single-Greedy BCL Is Viable, Raw BCL Is Not - 2026-07-02

- Decision: after a solved semantic safe prefix, use coverage layers only with single-chain Greedy safety and local-touch caps. Do not apply raw owner-hit BCL directly to f004d-style bases.
- Evidence: f005b/f005c produced `0` solved bundles from raw BCL; f005d proved the geometry can combine but creates low-choice unsolved deadlocks. This matches the user's warning that low choice metrics can be fake.
- Evidence: f005f with `--require-single-greedy-solved` restored solved bundles and official trace; f006a reached coverage `0.7101449` with `4/4 A/A`, localPatch `3-4`, nearOuter `2`, and max choices `8`.
- Evidence: f006e with phased half-layer and `--max-local-touch 2` reached the best current balance: coverage `0.7449275`, two `A/A` rows, localPatch `4`, nearOuter `2`, dependencyFollow `6`, max choices `7`.
- Boundary: f006g shows that simply pushing beyond `0.75` is not currently safe; all top rows became `B/B` and one was `LocalEasy`. Next work should add anti-local wave selection / release-owner diversity to the coverage layer before chasing more coverage.

# Generated-Root WBP Decision: Demand Carriers Must Be Safe Prefixes, Not Forced Direct Bundles - 2026-07-02

- Decision: do not force f001 high-demand 2-chain carrier bundles directly into V12 chain-plan seeds. Treat demand-carrier rows as candidates for safe-prefix / duty-front-end planning, not as guaranteed insertable chains.
- Evidence: f002a relaxed release-impact checking and still got `12/12` Greedy unsolved for the f001 two-chain bundles, so the failure is not just missing prerelease-safe parent/delay timing.
- Evidence: f003b found only one safe single carrier among `17`: `CLT97060` (`7->11`). f004a/f004b showed this single can seed V12 and extend to 2 chains with official trace `A`.
- Evidence: f004d showed a real positive when same-release-owner diversity is relaxed carefully: exact 3 added chains, official trace `A/HardPotential`, localPatch `3`, max choices `8`, remote choke signal strong. But f004e exact 4 fails, so this is not yet a coverage route.
- Implementation implication: next f-line work should either port the t142/t143 seedState-front-end style to demand duties / active cells, or build a custom safe-prefix expansion that allows bounded repeated release owners only when the target/frontier/demand role differs. More direct bundle forcing is a known negative path.

# Generated-Root WBP Decision: Use f-line Prefix For Independent Side Experiments - 2026-07-02

- Decision: independent Codex-side Generated-Root WBP probes should use `f###` prefixes, leaving the main `t###` sequence to the other active conversation/user-facing t-line.
- Reason: the main route has already advanced through `t142+`; reusing `t###` locally risks confusing reports, assets, and handoff notes.
- f001 boundary: compiling t139 demand-overlap carriers into V12 chain-plan seeds is useful as a feeder diagnostic, but direct 2-chain carrier insertion is currently rejected by `blocks_pre_release_owner` or `greedy_unsolved`. The next f-line probe should add pre-release-safe parent/delay pairing or a demand-carrier seed mode, not just force the same carrier rows harder.

# Generated-Root WBP Decision: Promote Run-Break Demand Into Active Cell/Region Duties - 2026-07-02

- Decision: do not continue by only increasing `cell-demand` penalty weights or by directly forcing t138 late-owner edges into the early generated-root hardbase pattern.
- Reason: t139a proved V12 can load and score t138c demand, but the strongest t138 contracts (`67->79`, `40->9`, `83->5`) refer to owners created late in the full-board chain cutting. The early generated-root planner still works on earlier owners, so direct owner-edge forcing is the wrong layer.
- Required next change: convert run-break contracts into active cell/region duties or demand-seeded frontier/reservation candidates before chain cutting. The acceptance smoke for that layer is nonzero seed states plus visible `choke/remote_guard/delay` overlap in planned relations, before official trace.
- Boundary: t137 accepted rows remain playable diagnostics, not a final baseline. They validate the target behavior (`9>24` delay and preserved bridge), but the next candidate should be planned from the whole-board duty layer rather than another post-hoc repair.

# Generated-Root WBP Decision: Move Outer Run-Breaking Into Generation-Side Contracts - 2026-07-02

- Decision: do not continue by adding post-hoc parent-release blockers one at a time. The remaining right/outer sequential-clear issue should be represented as whole-board planning contracts before chain cutting.
- Evidence: t138a run-break planning on `t124b_right_run_shape_recut2_r006_trim21x4_orig` identifies the exact visible conveyor window `78->52->7->1->36->64->67->79->40->9->24->47->21->58` and recommends `9>24` as `planned_delay_break`, matching the accepted t137 improvement.
- Evidence: t138b run-break planning on accepted t137a rows no longer anchors on `9>24`; it instead flags the remaining right-side run around `67>79` / `40>9` as `cross_basin_frontier_break_around_rejected_stack`.
- Implementation proof: t138c converts these contracts into the existing V12 `cell_demand` format (`144` cell-demand rows, roles `choke/remote_guard/guard/delay`), so the next pass can bias the whole-board planner directly instead of deriving a repair after trace.
- Counter-evidence: t137b already showed that stacking a second parent-release breaker on `79>40` keeps the bridge but does not improve the headline local/near-outer run and reduces remote choke signal, so `79>40` should be an avoid edge for the next pass.
- Implementation implication: the next WBP generator pass should ingest run-break contracts as semantic duties (`cross_basin_release`, `remote_guard`, `choke`, `delay`) in the whole-board cell plan, while preserving `58->85->83->68->72->28` and rejecting `60->28`.

# Generated-Root WBP Decision: Use Single Bridge-Locked 9->24 Breaker, Reject Stacked Upstream Breakers - 2026-07-02

- Decision: accept the single bridge-locked `9->24` parent-release breaker as a diagnostic baseline improvement for the current t124b branch; do not continue by stacking more upstream parent-release breakers.
- Evidence: t137a `r004/r005/r009` all solve officially, keep coverage `0.9574899-0.9615385`, reduce `nearOuterPatchSolveRunMax` from `4` to `3`, preserve support depth `4`, and keep the exact audit tail `58->85->83->68->72->28` with no `60->28` steal.
- Evidence: the accepted breaker changes the concrete run from immediate `9 -> 24 -> 47 -> 21 -> 58` into a delayed `9 -> 47 -> 21 -> 58 -> ... -> 87 -> 24` pattern, matching the user's concern about visible outer sequential clearing.
- Evidence: t137a `r014` proves metric-only acceptance is unsafe: it loses `72->28` and introduces `60->28`, so relation audit remains mandatory.
- Counter-evidence: t137b stacked an additional `79->40` breaker and still solved with the bridge intact, but it did not improve `nearOuter` or `localPatch` and reduced `frontierDrainRemoteChokeCount` from `2` to `0`.
- Implementation implication: the next WBP operator should not add another local parent-release blocker. It should re-plan the remaining right-side run as a semantic cross-basin/hard-frontier break, targeting `localPatchSolveRunMax<=4` and better anti-locality while preserving the accepted bridge tail.

# Generated-Root WBP Decision: Outer Conveyor Breaker Must Lock The Bridge Victim - 2026-07-02

- Decision: prioritize the outer/right-side conveyor problem identified by playtest, but do not accept an outer breaker unless it also preserves the root bridge/victim contract in relation audit.
- Evidence: the visible t129/t124 run is not captured by `outerNearFollowRunMax` alone; it appears as local/near-outer patch sequence and conveyor relation edges around `9->24->47->21->58`.
- Evidence: late guard insertion found `0` candidates from t129a, t126n, and t126j, so a simple final guard patch is not a viable geometric route.
- Evidence: parent-release blocker `9->82/87->24` works mechanically and can reduce local/near-outer pressure (`t136e`: `7/7 -> 6/6`; `t136f_r002`: nearOuter `4 -> 3`), but it weakens or breaks the accepted bridge tail by letting `60->28` replace `72->28`.
- Counter-evidence: `t136f_r001` preserves `72->28` and support score `0.944`, proving the contract can survive a blocker-style recut, but it does not improve the visible near-outer run.
- Implementation implication: the next WBP operator should be an outer breaker plus bridge-victim lock. It should target the `9->24/47` area, reserve/recut cells earlier if needed, and use official relation audit gates requiring `58->85->83->68->72->28` or an explicitly approved equivalent while rejecting `60->28` steals.

## Campaign500 Normal Full V1 Keep Policy - 2026-06-30

- 决策：Campaign500 Normal Full V1 不把 `LongChainProbe` 作为当前正式 production lane；原计划中的 LongChainProbe slot 用 `PeelHard` replacement 进入 source pool，并在 merge/join 层继续显式排除 `LongChainProbe`。
- 原因：用户人工反馈 LongChain 体感暂不适合量产；当前生产目标先稳定产出 Flow/Peel/Mixed/PressurePeak 类 normal 候选，LongChain 继续独立优化。
- 决策：保留两个 keep 出口。`ProductionKeep` 是 trace-order preferred machine keep（86 rows），用于候选池审查；`ProductionStrictKeep` 是更严格质量门槛池（70 rows），额外限制 sourceCoverage、maxChoices、localPatchRun、nearOuterRun、directionalRisk、stripeRisk、solveTraceQuality 和 collapse risk。
- 原因：machine keep 里仍存在少量 riskTags/watch 行；StrictKeep 更接近“可直接进入生产候选池”的干净口径，但 slot 覆盖更少（57 个 order），因此不覆盖原 ReviewPack/ProductionKeepPack。


## Generated-Root WBP V12 Decision: Root Gate Must Score Relation Quality - 2026-06-28

- 状态：实验中
- Decision: Generated-Root Whole-Board Planner 的下一阶段不能只看 strict disjoint capacity；root gate 必须同时评分 root footprint、release owner/source basin diversity、target basin diversity、cross-basin/choke/delay 合约、support proxy、same-owner/early-B1 聚集风险。
- Evidence: `grwbp_v12_strict_dutyray_quality_top12_t1_summary.csv` 显示当前前 12 个 root/cell-plan 里最高 strict chain capacity 只有 6，且全部低于目标 semantic capacity 8；`c6277` exact-6 仍有低 root footprint、弱 cross/choke/support proxy 和 early-B1 聚集问题。
- Evidence: `rootlang/40eb` 类 root 的 footprint 与 cross/delay proxy 更好，但 capacity 仍卡在 5，且 release owner/source basin diversity 偏弱、owner24 early-B1 聚集明显；它们是机制诊断，不是最终 baseline。
- Decision: 下一步必须改 root/cell-plan generation 与 reservation，让多 source basin、多 target basin、release/choke/delay/support 合约在切链前已经存在；不要继续把当前 shortlist 当成 exact6/exact7 chain cutting 问题硬捞。
- Gate direction: 后续候选至少朝 `targetSemanticCapacity>=8`、`rootCoverage/base footprint>=0.60`、release owner/source basin diversity、same early-B1 cap、cross/choke/delay/support proxy floor 过滤；最终仍用 official trace 验收 solved/process/difficulty。
- Follow-up decision: `chain_quality/reserve_quality` disjoint selection is adopted as a root/cell-plan gate refinement. It may choose a better relation set within the same capacity, but it does not count as coverage progress unless the root also passes semantic capacity/root-footprint/diversity gates.
- Follow-up decision: root footprint is now treated as a preferred window rather than a monotonic score. Trace-wide scans show `0.66-0.70` root coverage can collapse strict duty/ray capacity to 1 because the generated root consumes corridor space. The next root generator should reserve semantic corridor/ray space while targeting a balanced base footprint, not simply maximize root coverage.

## Generated-Root WBP V12 Decision: Cluster Seed Is The Next Unit, But Needs New Rays - 2026-06-28

- 状态：实验中
- Decision: V12 should move from post-hoc secondary/extension toward front-loaded added-chain cluster seed selection. The selectable unit is now a small semantic cluster with source owner, target owner, contract, slot, and added-chain DAG audit before candidate emission.
- Evidence: `parent_child_cycle` diagnostics turn the old planned-added-hit secondary samples into explicit cycles: child `SSF99501` and parent/support geometry first-hit each other and fail Greedy, so post-hoc child insertion is rejected.
- Evidence: `grwbp_v12_cluster_edge7_support_exact6_probe_t1` proves the cluster entry can preserve a generated root, chain legality, Greedy solve, and official process A while adding owner7 as a first-class source-frontier edge (`7->22`) plus owner7 `B1_BLOCKS_B2`.
- Boundary: the same exact-6 cluster is still 4/4 MediumStructure in official trace, and exact-7 fails mainly through overlap/Greedy unsolved. It is a mechanism step, not the 0.95/HardPotential solution.
- Rejected path: do not continue with slot-only widening, same-owner frontier repetition, weak generic carrier, B2 safe/capacity filler, or post-hoc secondary. These either reproduce MediumStructure or fail first-hit/Greedy geometry.
- Follow-up direction: the next planner must generate new whole-board duty/ray geometry for more source owners and cross-basin/choke targets. In practice that means reserving source basin corridors, target rays, release blockers, delayed blockers, convergence/choke duties, and intentional empty escape/probe space before chain cutting.

## 2026-06-28 - Nutation Uses Separate Solve-Order Primitives

- 状态：已采用
- 决策：关卡风格不再用 `Flow Peel` 这类混合命名；`Flow` 与 `Peel` 是不同 solve-order topology primitives。
- 决策：`Flow` 定义为连续传播/低 strict/noise baseline，天然容易连消；不作为当前 PSG 体感提升的主优化方向。
- 决策：`Peel` 定义为外层逐步解锁内层的 layer scaffold；当前第一条 Nutation 正式 lane 命名为 `NutationPeelV1`。
- 目标：`NutationPeelV1` 做“分层 peel + anti-linear peel”，保持 PSG coverage/可解稳定性，同时降低单轴、同向、局部连续剥穿。
- 后续命名：独立使用 `NutationLongChainV1`、`NutationHubV1`、`NutationBasinLiteV1`、`NutationMazeV1`、`NutationFlowBaselineV1` 等，不再混成一个 PSG 风格池。

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
- Core commit: `aa1564bd Add PSG pressure hard production lane`.
- Core flow: Unity calls `NoMaskProceduralGenerator.BuildSgpPressureHardTrialPack`, writes `SGPPressureHardTrialPack.asset` and `sgp_pressure_hard_trial_report.csv`, then `Build-SGPRhythmTrace.ps1` replays official trace for normal-production selection.
- Evidence: the full generation + trace recheck produced four near-full boards (`0.978-0.994` coverage), all solved; 3/4 pass the practical normal-production filter `coverage>=0.97 + solved + processTier A/B`.
- Boundary: this lane should not be advertised as the high-difficulty/root-hard line. Official trace still labels the candidates `LocalEasy`, and stricter support/depth gates leave only `dense_weave` as a clean high-support hit.
- Operating split: use this lane to mass-produce ordinary playable levels with some pressure, then run a separate targeted high-difficulty line for validated roots, sandwich/reverse support-preserving rewrites, and future owner-hit/support-shallowing primitives.
- Acceptance for normal batches: prefer `solved=True`, `coverage>=0.97`, `processTier in {A,B}`, reasonable max choices, and human review for chain-language/readability. `Drop` rows may be retained only as diagnostics or manual edge cases.
- Chain-language knobs such as long-chain, low-turn, or snake-like bias are allowed as a production styling layer, but they must remain downstream of the same trace/human review gates rather than replacing difficulty validation.
- Continuous-clear optimization for PSG normal must be dynamic/trace-aware. Do not hard-optimize static "first blocker on escape ray must be far" at `0.96+` coverage, because dense fill naturally makes first blockers adjacent and the static constraint pushes generation toward high opener/high choice failures. Use solve-flow/localPatch/nearOuter/directional run metrics plus human review instead.

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

## Geometry Supply Trace Thrift Must Preserve Candidate Diversity - 2026-06-26

- Decision: for geometry-supply owner-hit scheduling, do not use Top-only candidate tracing as the main efficiency strategy after `0.50`. Use strict gates plus owner/hit diversity sampling.
- Evidence: from the `0.4956522/A/TrueHard/support4` parent, Top-only top16 traced rows were all `HardPotential`; full top48 found the only strict row at rank45 (`0.5159420/A/TrueHardCandidate/support4`). After changing `TraceSelectionMode OwnerSpread` to head/tail hitOwner sampling, the same class of rank45 strict row is found with top8/24.
- Evidence: the optimized scheduler continued root10+dense_weave strict growth through `0.5159420 -> 0.5347826 -> 0.5492754 -> 0.5623188 -> 0.5768116`, all `solved=True`, `processTier=A`, `hardStructureV3Class=TrueHardCandidate`, `supportClosureBestDepth=4`.
- Boundary: this is scheduler efficiency and parent-selection improvement, not final high-coverage proof. Current best remains `0.5768116`, and future pushes still require official trace plus human feel; no Demo mount until a review-worthy candidate passes the high-difficulty gate.
- Next accepted route: keep `TraceTopCount` small, use owner-spread fallback before full trace, and only full-trace calibrate when strict density or sampling behavior is uncertain. If strict candidates keep appearing in low grammarScore tails, add a learned/hand-ranked tail prior rather than increasing brute-force trace volume.

## Geometry Supply Needs Adaptive Supply And Bundle Size - 2026-06-26

- Decision: after a hard root reaches a local geometry-supply boundary, do not keep the same supply/bundle settings and simply trace more. Switch the supply shape and shrink bundle size before treating the lane as exhausted.
- Evidence: root10+dense_weave at `0.5768116` failed with dense bundle3 and dense bundle2; supportDepth often stayed `4`, but hard class drifted to `HardPotential` or `MediumStructure`, so the failure was hard-structure dilution rather than raw solvability.
- Evidence: switching the supply to `section_unlock` and using bundle2 pushed the same parent to `0.6072464/A/TrueHardCandidate/supportDepth4`. When section bundle2 then drifted to HardPotential, bundle1 micro-commit continued to `0.6159420/A/TrueHardCandidate/supportDepth4`.
- Decision boundary: `bundle3` is a throughput tool for earlier growth, not a universal high-coverage strategy. Past drift points, use bundle2 and then bundle1; accept slower coverage if it preserves `TrueHardCandidate/supportDepth4`.
- Next accepted route: codify an adaptive schedule: dense/bundle3 for mid growth, section_unlock/bundle2 for the first post-0.576 escape, section_unlock/bundle1 for micro-commits, then only try other supplies or support-preserving prefix gates when micro-commits stop producing strict rows.

## Mask Production Requires Trace-Gated PSG Standard - 2026-06-26

- Decision: mask production must not accept `generated + GreedyPass + maskFill` as sufficient. Every candidate intended to meet PSG/PressureHard standard must pass official trace selection after mask patching.
- Evidence: direct mask-constrained PSG baseline produced 0/3 due GreedyFailed. The first PSG seed -> mask patch smoke produced 3/3 visually filled and Greedy-pass candidates at `maskFill≈0.90`, but official trace was only `B/Drop/Drop`, all `LocalEasy`, supportDepth `2`. After raising shape gates to `maskFill>=0.95` and `maskBoundaryFill>=0.98`, rerun produced 3/3 high-fill/edge-complete/solved candidates, but official trace was still `Drop/Drop/Drop`, all `LocalEasy`.
- Boundary: `SeedMaskPatchWindow` is the preferred reusable generator entry for mask smoke because it preserves/repairs existing seeds under mask shape; however, its accepted output is only a candidate pool until trace rejects Drop/too-wide openings.
- Minimum production gate for normal PSG-style mask levels: `solved=True`, `maskFill>=0.95`, `maskBoundaryFill>=0.98`, `blockRayHits=0`, `CampaignSingleLevelValidator` not Red, `processTier in {A,B}`, and max choices bounded (current first pass `<=10`). Stricter support/non-LocalEasy gates belong to the high-difficulty line and should not be silently mixed into this mask baseline.

## RSG Is A Compiler Line; SIG Is The Later Conflict Layer - 2026-06-26

- Decision: treat `RSG` / Root-Solve Generator as a compiler architecture, not another SGP patch. The structure order is `root -> solve-order graph -> slot plan -> grammar unit -> geometry supply -> official trace`.
- Decision: do not let SGP/PSG decide root-preserving structure in this line. They may provide geometry/body language, but RSG owns wave, owner, target ray, slot budget, exit mode, and acceptance contract.
- Decision: implement RSG V1 as a larger-slot throughput proof first. Target stable `0.55-0.65` root-aware A/B or better candidates before trying full `0.9+ TrueHard`.
- Decision: reserve `SIG` / Slot Interaction Graph for RSG V2. SIG is required for TrueHard upper bound because hard difficulty needs inter-slot conflict, not just sequential assignment, but adding it before larger-slot throughput is proven would overbuild the system.

## RSG Compiler V1 Needs Conflict Slots, Not More Runtime Trace - 2026-06-26

- Decision: keep the RSG pipeline compile-first: `SolveOrderPlan` and `SlotPlan` are computed once, SGP/geometry only realizes slot units, and official trace remains a final gate for top candidates rather than a per-candidate planner.
- Evidence: `Build-RSGCompilerPipelineV1.py` with single-slot Greedy survival and candidate Greedy gate produced stable first/second stage candidates from the V1.31 `support_lock` root parent; official trace showed solved/supportDepth 3-4 through the second stage.
- Boundary: the third stage can remain solved but drops supportDepth to 2 or dilutes to B/MediumStructure; cheap Greedy sequence proxy helps rank candidates but does not guarantee supportClosure.
- Decision: do not fix this by reintroducing full trace into ranking or by expanding ordinary sequential slots. The missing primitive is inter-slot conflict/closure pressure (`SIG-lite`): slots must delay/block/anti-shortcut each other, not merely assign releaseOwner -> targetOwner segments.

## CSSC Proof Reframes RSG As A Sub-Compiler - 2026-06-26

- Decision: treat CSSC / Constrained Solution Space Compiler as the current root-hard/high-coverage research architecture, with RSG as the per-basin slot/segment realization sub-compiler rather than the top-level generator.
- Evidence: `Build-CSSCMinimalProofV1.py` took root10 geometry-supply parent `0.615942/A/TrueHardCandidate/supportDepth4` and, after adding 3-basin asymmetric contracts, produced `cssc_root10_0615_smoke2_wide_c010` at `0.6507246/A/TrueHardCandidate/supportDepth4/outer0`. This is the first positive proof that multi-basin contracts can raise coverage while preserving TrueHard/support.
- Boundary: continuation from that best row to `0.668-0.671` still stayed solved/A/supportDepth4/outer0, but all traced top3 rows became MediumStructure because `B2_DELAYS_B3` disappeared and only B1-driven contracts remained. Support preservation alone is therefore not enough evidence of high difficulty.
- Decision: the next variable is not more PSG fill, ordinary RSG slot expansion, or full trace in the ranking loop. The missing piece is closure-compatible CSSC contract capacity, especially mid-basin/B2 contracts and convergence choke language that survives after 0.65 coverage.
- Acceptance for the next CSSC step: require `solved=True`, `processTier=A/B`, `supportDepth>=4`, non-LocalEasy/TrueHard-or-HardPotential class, and explicit presence of at least one mid-basin/B2 contract in selected rows. Rows that only preserve support but flatten to B1-only MediumStructure are diagnostics, not production candidates.

## CSSC Contract Persistence Is A Guardrail, Not A Generator - 2026-06-26

- Decision: add Contract Persistence Layer V0 to `Build-CSSCMinimalProofV1.py` as an explicit guardrail. Persistent contracts such as `B2_DELAYS_B3` must have available options and must be included in every emitted candidate unless `--allow-missing-persistent-contracts` is explicitly set.
- Evidence: with `--persistent-contracts B2_DELAYS_B3`, the 0.615 root10 parent still emits candidates and official top3 remains 3/3 solved/A/support4/outer0. The same setting on the 0.6507 parent emits 0 candidates and reports `B2_DELAYS_B3=decayed/options=0`, matching the observed MediumStructure flattening boundary.
- Boundary: CPL V0 prevents false continuation after a required contract disappears, but it does not create new B2/mid-basin contract material. The next generator work must increase B2 contract capacity and convergence choke realizability; otherwise CSSC will stay capped around first-pass 0.65 strict/near-strict growth.

## CSSC CTG Restores B2 Capacity But Not TrueHard Strength - 2026-06-26

- Decision: treat `B2_DELAYS_B3` as a basin-level generative primitive, not an exact release-owner first-hit condition. CTG V0 may allow hits into the B2 basin owner set when the planned B2 release owner cannot directly realize the delay contract.
- Evidence: r2 diagnostics on the 0.6507 CSSC parent found raw segment geometry existed, but 955/1028 sampled raw options were killed by exact release-owner first-hit. Enabling `--enable-b2-contract-propagation --ctg-allow-basin-hit` restored `B2_DELAYS_B3` from 0 options to 4 options and emitted 8 full 4-contract candidates.
- Evidence: official trace on those 8 CTG candidates was 8/8 solved, 8/8 processTier A, 8/8 supportDepth4, and 8/8 outer0; coverage reached `0.6695-0.6754`. However only 1/8 reached HardPotential and the rest stayed MediumStructure, so CTG V0 restores capacity and stability but not enough hard-pressure strength.
- Boundary: do not count CTG V0 as a production-hard breakthrough yet. The next accepted variable is stronger B2 contract dynamics: propagation quality, non-local influence, and `B2_CONVERGE_CHOKE` material that raises hard class instead of merely preserving support and solvability.

## PSG Is Geometry Supply For CSSC, Not A Direct Cleanup Generator - 2026-06-26

- Decision: for CSSC/root-hard high-coverage work, do not hand a CSSC parent directly back to PSG/SeededDirectSGP as a cleanup generator, even with pre-action masks, release-aware heads, and local difficulty scoring. Use PSG output as geometry supply only; CSSC/RSG/owner-hit grammar must decide which chains are committed.
- Evidence: from `cssc_root10_0615_smoke2_wide_c010` (`0.6507/A/TrueHardCandidate/support4`), contract-preserving PSG target `0.75` reached `0.70-0.735` with `boundaryDirectExitUsed=0` but official trace was 4/4 unsolved/support0. A smaller target `0.67` accepted 3-4 release-aware heads per candidate and still produced 8/8 unsolved/Drop.
- Evidence: using PSG dense-weave as geometry supply through `Invoke-GeometrySupplyOwnerHitSchedulerV1.ps1` succeeded on the same parent: bundle4 produced `0.6638/A/TrueHardCandidate/support4/outer0`, with top12 trace all solved/A/support4 or fallback-hard. Increasing to bundle8 raised coverage near `0.69` and kept solved/support, but all traced candidates fell to MediumStructure.
- Boundary: the viable combination is adaptive bundle owner-hit/CSSC submission: smaller bundles preserve TrueHard but grow slowly, larger bundles improve coverage but dilute hard class. To approach PSG fill density, the next variable is not more PSG masking; it is stronger B2/choke contract generation plus adaptive bundle sizing.

## Reverse-CSSC Predecessor Archived With RCH - 2026-06-27

- Decision: Reverse-CSSC / full-grid reverse compiler decisions from 2026-06-26 are no longer part of the active project decision line. They are archived with RCH in `.agents/memory/RCH_EXPERIMENT_LINE_ARCHIVE_20260627.md`.
- Do not cite `Build-ReverseCSSCWaveCompilerV0.py`, `Build-ReverseCSSCContractCompilerV1.py`, `reverse_cssc_*` reports, `staged_four_basin_full_tree`, `medium_direction_spread_single_side_full_tree`, or `medium_two_gate_centered_single_left_full_tree` as current baseline, clean floor, or next implementation direction.
- The durable lesson kept here is negative and methodological: full coverage plus solvability, or even a constructed full-grid TrueHard sample, does not automatically define the production route. Any restarted line must be re-scoped against the user's requirement for whole-board planning, real root usage, non-long-chain chain language, and official trace/relation-audit validation.

## RCH Experiment Line Archived - 2026-06-27

- Decision: RCH/high-root experiment decisions from 2026-06-26/27 are no longer part of the active project decision line. They are archived in `.agents/memory/RCH_EXPERIMENT_LINE_ARCHIVE_20260627.md` and `.agents/memory/RCH_LONGCHAIN_NO_ROOT_RECORDS.md`.
- Do not cite old RCH route locks, “current mainline” labels, `probe9_rootcontract`, WholeBoardPathCover/TemporalFill, ContractField, RootGraph, RSO split, tile/fold/port-pair, or long-band results as current baseline or production direction.
- If a future task explicitly restarts RCH, read the archive first, then restate a fresh plan. The only durable lesson kept in active decisions is negative: coverage/solvability alone is not enough; any high-root candidate needs final official trace plus source-side contract validation and relation-audit evidence of planned non-local contract edges.

## PSG Normal V3 Region Flow Is Diagnostic, Not Production - 2026-06-27

- Decision: do not promote `BuildSgpPressureHardInterferenceV3SixPack` to the PSG normal candidate generator. Keep it as an opt-in diagnostic branch for region/flow features only.
- Evidence: V3 improves visible-risk metrics versus Flow-spread V2 (`nearOuterMax 5->3`, `directionalRiskMax 0.234->0.176`, `stripeRiskMax 0.112->0.012`) but raises choice pressure too much (`avgChoicesAvg 5.69->8.37`, `maxChoicesMax 11->15`), drops source coverage min to `0.968`, and yields ordinary keep `0/6`.
- Boundary: region/head-flow disruption should be used as trace post-selection or a softer ranking term, not as a hard generation-side scheduler for PSG normal. The current fallback remains base PSG Trial/Review and V2-style candidate expansion plus trace filtering.

## Campaign500 PSG Normal Canvas Variation Is Candidate-Only - 2026-06-27

- Decision: for ordinary Campaign500 PSG normal production, do not force every tall/thin template slot into a wider canvas. Generate `layout_soft`/soft-wide variants as extra candidates, then let the same trace + visual gates decide whether they replace the original aspect.
- Evidence: Production20 `layout_soft` proved feasible but not yet keep-stable. The 17x23 section candidate had good curve and low visual risk but coverage was `0.969`, just under the normal gate; the 23x33 lock candidate passed coverage/tier/maxChoices but had directional risk `0.356`, above the visual threshold. The other two soft-wide rows were Drop by maxChoices.
- Boundary: canvas variety is still desirable for consecutive campaign feel, especially to avoid too many thin vertical levels in a row. Apply it through slot-level candidate competition and manual review, not through a generator-side hard rule that can break coverage or reintroduce one-side sweep language.

## Generated-Root Whole-Board Planner Starts A New Line - 2026-06-27

- Decision: start a separate `Generated-Root Whole-Board Planner` line. It uses a real generated/validated root as the preserved causal core, plans every board cell role first, then cuts semantic chains from that role plan.
- Decision: the planner must not continue RCH/Reverse-CSSC, PSG cleanup, capacity-only filler, rank-only tags, path chunks, long bands, slabs, or few-chain full-board motherboards. Official trace is a final gate and audit source, not a per-chain generation decision loop.
- Implementation V0: `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-GeneratedRootWholeBoardPlannerV0.py` writes root identity, whole-board `cell_plan`, semantic `chain_plan`, planned relation audit, candidate assets, and candidate CSVs.
- V0 positive smoke: from generated root `geosupply_sched_root10_from_40eb0da7_r1_c038` (`0.615942/A/TrueHardCandidate/supportDepth4`), pair candidates preserve root identity and add two short semantic chains. Official trace: 6/6 solved, process `A`, 2 TrueHardCandidate + 4 HardPotential, supportDepth4; relation audit shows `passesTrueHardRelationGate` for c005/c006.
- V0 boundary: this is not a high-coverage proof. Current solved smoke reaches only `0.6275-0.6290` coverage; 3+ chain strict candidates currently fail solved gate, while structural-only 4-5 chain candidates can be generated but are unsolved. Next work needs release-order compatibility and mid-basin/B2 capacity before chasing `0.95+`.

## Generated-Root WBP V1 Adds Compatibility Gates But B2 Remains The Boundary - 2026-06-27

- Decision: V1 should remain compile-first. It adds release-compatible beam selection, B2 target/release variants, release-impact gating, and single-option audit; official trace is still final validation only.
- Implementation V1: `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-GeneratedRootWholeBoardPlannerV1.py`.
- Positive result: guarded pair smoke on `geosupply_sched_root10_from_40eb0da7_r1_c038` emits 8 two-chain candidates with preserved root identity, coverage `0.6275362-0.6289855`, short added chains, and release-impact gate enabled. Official trace: 8/8 solved, process `A`, HardPotential, supportDepth4. Relation audit sees planned-added official edges such as `20 -> 58`, `7 -> 59`, `59 -> 22`, and `33 -> 60`.
- Boundary: guarded triple smoke emits 0 candidates. Beam depth 3 rejects by `blocks_release_owner=3490`, `cell_overlap=2020`, `first_hit_owner_mismatch=194`, and `greedy_unsolved=1976` on the tested pool.
- B2 finding: V1 restores B2 geometry capacity (`B2_DELAYS_B3=52`, `B2_CONVERGE_CHOKE=52` options), but all B2 single options are `single_greedy_unsolved`; 44/52 in each B2 contract also block their own release owner / pre-release path. This is not a beam tuning problem.
- Decision: the next accepted variable is B2 release-safe / multi-key grammar: a B2 chain must know whether it is immediate-release, delayed-by-secondary-owner, or multi-key, and relation audit must expose those edges. Do not chase 3+ by widening beam, relaxing greedy, or treating B2 geometry-hit options as valid semantic contracts.

## Generated-Root WBP V2 Proves B2 Release-Safe Grammar, Not Final Hardness - 2026-06-27

- Decision: keep the Generated-Root WBP line compile-first. V2 adds B2 release-safe / multi-key grammar before beam selection; official trace remains a final gate and relation-audit source, not a per-chain generator.
- Implementation V2: `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-GeneratedRootWholeBoardPlannerV2.py`. B2 options now carry release stack/profile fields (`releaseProfile`, `releaseStackOwners`, `plannedSolved`, `singleGreedySolved`, `blockedOwners`, `keyBlocked`) and are rejected when they block their own key path.
- Evidence: the V1 3-chain boundary is broken without relaxing solved gates. `generated_root_wbp_v2_b2safe_smoke5_fivechain_widepool_*` emits 3 candidates with preserved generated root identity, 5 short added semantic chains, 4 contract types, 3 B2 release-safe chains, and coverage `0.6463768`; official trace is 3/3 solved, process `A`, supportDepth4, outerExitHeadCount 0.
- Relation evidence: official relation audit sees the added chains in real unlock edges, including early B1 edges (`7 -> 60/61 -> 22`) and late B2/choke edges (`0 -> 59/60/62/63`, `59/60 -> 48/2`, `63 -> 30`). This validates that B2 release-safe chains are not just planned labels or filler.
- Boundary: the 5-chain positives remain MediumStructure with hardStructureV3Score only `0.498-0.510`; 6-chain wide-pool emits 0 candidates and reintroduces `blocks_release_owner` plus cell-overlap failures. V2 proves B2 contract viability and short-chain scalability to 5 chains, but it does not yet solve the target `0.95+`/TrueHard problem.
- Decision: the next accepted variable is not more beam width, lower greedy pressure, long chains, slabs, bands, or PSG cleanup. The next generator change should add B2 contract strength and spatial lane allocation: prefer cross-region/delayed B2 edges, reduce same-region local B2 releases, reserve lanes in the whole-board cell plan, and make convergence choke/delay strength visible in planned relation + official relation audit.

## Generated-Root WBP V3 Uses Release Topology, Not Static Strength Alone - 2026-06-27

- Decision: static B2 strength/lane metrics are useful instrumentation but should default to non-operative weights until they are proven. Ranking only by high B2 strength can steer the beam into overlaps/deadlocks and is not a replacement for release topology planning.
- Evidence: V3 instrumentation preserved V2 five-chain solved/A/support4 behavior and exposed the collapse: old B2 safe selected chains all had final semantic release owner `0` despite high coverage and official solvability.
- Evidence: a whole-board B2 head scan found many possible final B2 release owners outside owner0. Adding opt-in B2 topology scan (`B2T`) produced candidates where first-hit release owner differs from semantic final release owner, e.g. `B2T95004` uses first-hit owner14 but semantic release owner30 at step32.
- Decision: represent `releaseOwner` and `semanticReleaseOwner` separately for multi-key/topology B2 chains. `releaseOwner` remains the immediate legality/first-hit key used by existing validators, while `semanticReleaseOwner` carries the planned basin-release contract for audit, diversity, and future selection.
- Evidence: `generated_root_wbp_v3_topology_smoke3_fourchain_diverse_*` emitted 4 root-preserving candidates with two B2 semantic release owners, official trace 4/4 solved/A/support4, and relation audit edges involving the new owner30 B2T chain (`30 -> 61 -> 53`).
- Boundary: V3 topology smoke still traces as MediumStructure (`hardStructureV3Score 0.514-0.526`) and coverage stays near `0.646`, so it is a primitive proof, not the final whole-board planner. Next accepted work should improve release-topology allocation at 5/6 chains and make cross-basin/choke contracts stronger before chasing coverage.

## Generated-Root WBP Needs Chain-Level Hardness Attribution - 2026-06-27

- Decision: use official trace/relation-audit attribution before changing V4 generation objectives. A planned contract is not considered hard-pressure material just because it appears in `chain_plan`; it must show up as useful official parent/child relation, cross/delay pressure, or support closure.
- Implementation: `Build-GeneratedRootWBPDifficultyAttributionV1.py` joins selected `chain_plan`, `trace_steps`, `trace_metrics`, and relation audit edges/parents/levels, then classifies added chains as `CrossDelayPressure`, `DelayChokePressure`, `ReleasedLeaf`, support carrier, local penalty, direct opener, or dead filler.
- Evidence: V2 five-chain and V3 topology four-chain both have all selected added chains touched by official relation graph, but `supportCarrierCount=0`. They preserve solved/A/support4 because the original root still carries support closure; added chains are not yet increasing the closure backbone.
- Evidence: V3 improves B2 release topology versus V2 (`0:1;30:1` instead of V2 `0:3`), but B2T `B2T95004` is still `DelayChokePressureWithLocalPenalty`, i.e. semantic topology plus delay/choke but shallow/local/conveyor in official closure terms.
- Decision: V4 should target metric alignment, not just more semantic labels. Candidate selection should reward added `support_closure` carriers, closure-valid cross-basin B2T edges, and non-local delay/choke, while penalizing dense local/conveyor relations and released leaves that only add coverage.

## PSG Normal Trace-Order Sketch First - 2026-06-27

- Decision: do not directly modify the PSG/Pressure-SGP rectangle peel generator to fight same-direction local collapse yet. The first accepted step is a read-only trace-side `Solve Trace Sketch` layer that makes solve-order shape visible.
- Implementation target: `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1`; PSG Unity generation entry points and pack/source-report paths remain unchanged.
- New metrics should separate spatial dispersion from directional inertia: high `solveRegionEntropy` alone is not enough if `solveSameAxisRunMax`, `solveSameDirHeadRunMax`, or `solveTraceCollapseRiskScore` remain high.
- Use these fields first for joined CSV ranking / post-selection and then, if validated by manual review, as soft inputs to FlipGate or candidate scoring. Do not turn them into generation-side hard constraints until they are calibrated on Review6/V2/V3 and Campaign500 PSG normal batches.

## PSG Production Batch Uses Trace-Order Keep Before Generator Rewrite - 2026-06-27

- Decision: the next PSG production step is candidate-pool expansion plus STS trace-order keep, not another generation-side peel rule. Default batch pool is Trial + Review6 + Interference6 + InterferenceV2Six; InterferenceV3 remains opt-in diagnostic only.
- Decision: production keep should be `TraceOrderKeep` when STS metrics exist. The old visual gate remains compatibility fallback for old metrics, and severe visual risks still hard-block STS keep, but mild stripe alone should not kill a clean solve-order candidate.
- Implementation: `Tools/Production/Invoke-SGPPressureProductionBatchV1.ps1` merges source reports, runs `Build-SGPRhythmTrace.ps1`, calls `Join-SGPPressureTraceMetrics.ps1`, writes canonical `sgp_pressure_hard_production_keep.csv`, then Unity builds `SGPPressureHardProductionKeepPack`.
- Production-speed choice: the batch wrapper defaults to lightweight counterfactual trace (`TraceMaxCounterfactualMovesPerStep=1`, `TraceCounterfactualStepStride=4`) because PSG STS keep depends on solve-order/region/direction/dependency sketch, not deep counterfactual fields. For exact metric parity, rerun with `5/1`.
- Evidence: four-pool fast STS batch `sgp_pressure_batch4_faststs_20260627` traced 22/22 candidates. After fixing the PowerShell case-insensitive `$MaxChoices`/`$maxChoices` collision in the join script, final pack build `sgp_pressure_batch4_faststs_final2_pack_20260627` selected 15 `TraceOrderKeep` rows, max `maxChoices=10`, and attached a 15-level `SGPPressureHardProductionKeepPack` to Demo.

## PSG Style Language Starts As A Tagging Layer - 2026-06-28

- Decision: add PSG style language as a report/selection layer first, not as generator-side hard control. The rectangle peel core and coverage-first production lane stay unchanged for this step.
- Taxonomy: keep `styleFamily` for macro family (`lock_buckle`, `dense_weave`, `core_burst`, `section_unlock`, etc.), `chainLanguage` for local chain shape (`lock_cluster`, `short_patchwork`, `woven_medium`, etc.), and `flowLanguage` for solve-order behavior (`staged_unlock`, `region_alternating_flow`, `flow_spread`, `local_collapse`, `single_axis_sweep`).
- Implementation: `Join-SGPPressureTraceMetrics.ps1` now derives `styleFamily`, `generatorVariant`, `generatorGrammar`, `chainLanguage/chainTags`, `flowLanguage/flowTags`, `riskTags`, and `styleRiskBand` from source report fields plus STS/visual trace metrics.
- Boundary: these tags are descriptive and selection-ready; they do not yet change `TraceOrderKeep`, `psgRankScore`, head selection, chain extension, or FlipGate behavior. Strong style generation still requires a later `StyleProfile` layer if manual review confirms the tags map to visible gameplay feel.
- Verification: rejoined the current four-pool batch with existing STS metrics using `sgp_pressure_batch4_faststs_final2_pack_20260627`; counts stayed stable at 22 joined rows and 15 production keep rows, and canonical `sgp_pressure_hard_production_keep.csv` now carries the tag fields.

## PSG Diversity Cap Is A Pack Selection Layer - 2026-06-28

- Decision: use style/chain/flow tags first for pack-level diversity, not for generation-side constraints. Default production behavior remains unchanged unless diversity parameters are explicitly enabled.
- Implementation: `Join-SGPPressureTraceMetrics.ps1` supports `-EnableProductionDiversity`, `-MaxProductionKeepRows`, per-style/flow/chain/style-flow caps, approximate `styleSignature` duplicate caps, high-risk caps, and `-StrictProductionDiversity`.
- Wrapper support: both `Invoke-SGPPressureProductionBatchV1.ps1` and `Invoke-SGPPressureHardProductionV1.ps1` pass the diversity parameters through to the join tool.
- Verification: default four-pool wrapper still returns 22 joined rows and 15 production keep rows. A strict review profile (`maxRows=12`, per-style family 5, per-flow 6, per-chain 7, per-style-flow 4, per-style-signature 1, max high-risk 4) returns 12 rows without backfill.
- Boundary: the strict12 result is a diversity review candidate, not yet mounted as the canonical Demo pack. Current canonical keep and pack remain the 15-row production keep until deliberately rebuilt from a diversity keep.

## PSG Style Lanes Are Stable Export Contracts - 2026-06-28

- Decision: split PSG review/production candidates into stable lane CSVs before changing generation. Lane export is a selection/report layer and must not modify canonical `sgp_pressure_hard_production_keep.csv`, Demo, or PSG generator core.
- Implementation: `Tools/Production/Export-PSGStyleLaneKeepsV1.ps1` reads a joined CSV, or can first call `Join-SGPPressureTraceMetrics.ps1` from source+trace metrics, then emits five independent lane keep CSVs: `patchwork_lock`, `core_burst`, `dense_weave`, `flow_spread`, and `staged_unlock`.
- Lane contract: `patchwork_lock` uses `lock_buckle/lock_cluster`; `core_burst` uses `core_burst/core_cluster`; `dense_weave` uses `dense_weave` family or `dense_weave_chain`; `flow_spread` uses primary `flow_spread/region_alternating_flow`; `staged_unlock` uses primary `staged_unlock`.
- Verification: smoke on `.codex-run/sgp_pressure_batch4_faststs_final2_pack_20260627_trace_joined.csv` with `TraceOrderKeep` eligibility exported current lane rows as patchwork_lock 6, core_burst 6, dense_weave 3, flow_spread 6, staged_unlock 7.
- Boundary: lane CSVs are independent by default, so the same level may appear in both a style lane and a flow lane. Use `-UniqueAcrossLanes` only when building a mixed review pack that must avoid cross-lane duplicates.

## PSG Longchain Starts As Seed Mutation, Not PSG Core Rewrite - 2026-06-28

- Decision: long-chain maze/lock/weave work starts in `.worktrees/psg-long-seed-mutation`, not in the main PSG production worktree. This isolates experimental assets, reports, and future mutation scripts from the current canonical PSG keep/pack.
- Decision: first generate a read-only source pool from real project seeds before mutating assets. The accepted source styles are `seed_long_maze`, `seed_long_lock`, and `seed_long_weave`, gated by high spine concentration rather than generic long-chain rate.
- Implementation: `Export-PSGLongSeedMutationSourcePoolV1.ps1` selects mutation sources from `project_seed_style_v3_initial951_20260628_profile.csv`; default gates are coverage>=0.72, p90>=20, maxChain>=34, top3ChainCellShare>=0.18, spineConcentrationScore>=0.82.
- Evidence: default run selected 49 sources from 222 eligible rows: `seed_long_maze` 24, `seed_long_lock` 9, and `seed_long_weave` 16. Selected averages are spine-heavy (`long_maze` avg maxChain 71.25/spine 1.0, `long_lock` avg maxChain 49.11/spine 0.9883, `long_weave` avg maxChain 46.06/spine 0.979).
- Boundary: this selector does not mutate LevelDefinition assets. Next accepted step is cheap geometry variants plus strict dedupe/trace/style checks, then preserve-spine local refill/rewrite.

## PSG Seed Style Learning Uses Initial Seeds Only - 2026-06-28

- Decision: seed-derived PSG style learning should default to the initial seed pool, not generated/composite experiment outputs. The default source is `Assets/ArrowMagic/SOData/Levels/Seeds`, which currently yields 951 parsed seed assets.
- Implementation: `Build-ProjectSeedStyleProfileV1.ps1` defaults to fast static style profiling: chain length, turn density, straight-like rate, region entropy, head direction mix, outer head rate, and derived style clusters. Deep ray dependency / gate potential scan is opt-in via `-DeepDependencyScan`.
- Decision: do not use chain length alone as a seed style proxy. The accepted style axes are chain morphology, topology language, and spine concentration. Long-chain seeds can be `long_maze`, `long_lock`, `long_weave`, `dense_weave`, `medium_long_patchwork`, or `fragmented_lock_like`.
- Evidence: current fast initial-seed run `project_seed_style_v3_initial951_20260628` parsed 951/951 with 0 missing in about 107 seconds and produced 11 clusters. Main seed clusters are `seed_long_maze` 574/951, `seed_sparse_tutorial` 155, `seed_flow_spread` 57, `seed_medium_long_patchwork` 45, `seed_long_weave` 31, `seed_dense_weave` 27, `seed_long_lock` 25, and `seed_fragmented_lock_like` 16.
- Finding: current PSG 15 keep rows now classify as `seed_fragmented_lock_like` / `medium_long_patchwork_carrier`, not as true seed `long_maze` or `long_lock`. The key gap is spine concentration: seed long-maze averages `maxChain=45.86`, `top3ChainCellShare=0.2215`, `spineConcentrationScore=0.9112`; PSG keep rows sit around `maxChain=19-21`, `top3ChainCellShare=0.09-0.11`, `spineConcentrationScore=0.405-0.474`.
- Boundary: this is a read-only profiling/selection input. Do not use it to rewrite PSG generation core until the seed-derived ranges are validated against trace-order STS and manual gameplay feel.

## Generated-Root WBP V4 Must Gate Option Safety And Lane Collapse - 2026-06-28

- Decision: V4 planned support proxy is useful, but it must be paired with option-level safety. Any semantic option can be high-score yet invalid if `plannedSolved=False`, `singleGreedySolved=False`, or `keyBlocked=True`; this is not only a B2 problem.
- Evidence: 5-chain relaxed s7 selected non-B2 options such as `O00174/O00114` with high planned support but `plannedSolved=False/singleGreedySolved=False`; official trace dropped in 2 steps. V4 now supports `--require-option-planned-solved` and `--require-option-single-greedy`.
- Decision: 4-chain V4/s5 is the current positive baseline for Generated-Root WBP: solved/A/HardPotential/supportDepth4 with hardV3 `0.653`, root preserved, short semantic chains, B2 semantic owners `0:2;30:1`, and localMax under the planned threshold.
- Boundary: 5-chain solved is not automatically progress. s9 proves safe + solved + higher coverage can still collapse to MediumStructure when the added chain piles onto owner0/lane `0,2`, raising localPenaltyCount and localPatchRun.
- Decision: the next generator variable is not more beam width, lower greedy gates, more B2 convergence, or coverage-first filling. V5 should create cross-lane safe carrier options: plannedSolved/singleGreedy, non-owner0-heavy, non-lane-stacked, and likely B1/B2 bridge/carrier chains whose official out edges become closure-valid.
## Generated-Root WBP V5 Decision: Single-Carrier Ceiling, Move to Pair/State Contracts - 2026-06-28

- 决策：Generated-Root Whole-Board Planner 不再继续用单链 carrier targeting 微调来追 supportCarrier；V5 已证明单链/单 option carrier 可以成为 official parent 和 closure-valid out edge，但 actual closure 常停在 depth 2，不足以触发 support_closure。
- 依据：`grwbp_v5_s14_topocarrier_candidate3` 的 `CARRIER_B1_TO_CHOKE`（旧输出名 `B1_CARRIES_CHOKE`）official outClosureValid=1/outCUD≈26，但 parent closureDepth=2、closureScore=0.294；`grwbp_v5_s19_require_actual_with_b2diag` 40k attempts 下无 actualDepth>=3/score>=0.45 carrier。
- 约束：V5/V6 仍必须保留真实 generated root、whole-board cell plan、global option safety、official trace 只做最终验收；不得回到逐链 trace 决策、长 band/slab、owner0/lane0,2 堆叠或 capacity filler。
- 后续方向：V6 应引入 pair/state-level carrier contract：先在 planner 内模拟 selected state 的 actual closure，再生成/选择 relay pair（carrier A -> relay B -> closure subtree C）。验收信号是 stateActualCarrierCandidateCount>0、official supportCarrierCount>0 或至少 added parent closureDepth>=3/score>=0.45。
## Generated-Root WBP V6 Decision: Pair Relay Must Prove Actual Closure, Not Just Parent Release - 2026-06-28

- Decision: Pair/state carrier 不能只以 `A releases added relay B` 为成功；必须在 state-level actual graph 中证明 `B` 继续释放 base owner/subtree，目标是 added closureDepth>=3/score>=0.45 或 official attribution `support_closure`。
- Evidence: V6 s2/s7 能生成并选择 `PAIR_RELAY_B1_TO_B3`，official solved/process A，但 pair relay 在 relation audit 中是 `ReleasedLeaf`，pairActual 仍 depth2/score0.298，difficulty risk 仍 `no_added_support_carrier`。
- Rejected interpretation: `pairRelay>0` 或 coverage 从约 `0.642` 到 `0.655` 不是突破；它仍可能是外缘/局部 leaf filler。
- Follow-up direction: 下一代应使用 state-activated carrier target selection，从 root support corridor / actual frontier / official best closure ancestors-descendants 中找 relay target；避免以 CLC 外边缘 parent 继续生成空 relay。

## Generated-Root WBP V7 Decision: State-Frontier Is Valid, B2 Capacity Refill Still Collapses Difficulty - 2026-06-28

- Decision: `STATE_FRONTIER_*` 是当前 Generated-Root Whole-Board Planner 的有效新语法：候选链必须由真实 root owner first-hit 释放，并在 planned activation state 中证明它是 target owner 的最后阻挡，移除后 target 直接打开。这个证据应进入 chain/relation/profile CSV，不能只靠 proxy 分数。
- Evidence: V7 s2 `grwbp_v7_s2_frontier_smoke` official `solved/A/HardPotential`；relation audit 确认 `24 -> 59/60` 且 `59/60` 继续作为 official parent 释放 base owners。Difficulty attribution 把两条新增链归为 `CrossDelayPressureWithLocalPenalty`，不是 V6 的 `ReleasedLeaf`。
- Boundary: s2 的 frontier target 仍集中在 `24->51`，且新增 closureDepth 最高只有 2；它是方向验证，不是 0.95 覆盖率方案。
- Rejected path: s3 通过 B2 safe/capacity 链把 coverage 提到 `0.6449275`，但 official hard class 降到 MediumStructure，风险回到 `b2_single_owner;local_penalty_dense;no_added_support_carrier`。这验证“补链条/补容量后难度不够再补难度链”的三明治路线会稀释指标。
- Follow-up direction: 后续 V7/V8 应扩展 state-frontier target diversity：支持更多 activation->target 边、support-corridor/ray-prefix 替代目标、跨 basin target quota 和 B2 owner/lane diversity；不要用 owner0 B2 convergence 作为 coverage filler。

## Generated-Root WBP V8 Decision: Frontier Diversity Must Be Seeded, Not Hoped From Beam - 2026-06-28

- Decision: state-frontier 多样性必须在候选生成/seed state 层显式表达；不能只靠 beam 排序或 `state_frontier_candidate_bonus` 希望它自动组合。V8 增加 per-edge cap、edge/target diversity score 和 deterministic pair seed states。
- Evidence: V7/V8 诊断显示 `24->42` 本身有 37 条 accepted option，且与 `24->51` 存在 506 个 disjoint + greedy-solved pair。失败点是 `24->51` 连续吃满 pool/beam，而不是 target 语义无效。
- Accepted proof: `grwbp_v8_s2d_frontier_pair_seed` official 8/8 solved、process A、HardPotential，且每个候选均含 `24->51` 与 `24->42` 两条 state-frontier 语义链；relation/difficulty attribution 可见 official parent/child、cross/delay/choke 信号。
- Rejected implementation path: wide weighted beam 会变重且不稳定；普通 V1 option 混入会产生 unsolved/Drop（s1b）。后续应继续用 deterministic bundle seed / explicit semantic state construction，而不是让普通补肉链进入 early proof。
- Boundary: V8 s2d coverage 仍只有约 `0.633`，且 `no_added_support_carrier` 未解决；它证明了多 edge frontier core 保难度，不是高覆盖最终方案。下一步覆盖扩张必须来自更多 semantic frontier/bundle/all-owner pairs，而不是 B2 capacity refill。

## Generated-Root WBP V9 Decision: Bundle Seed Works, 4-Chain Needs Slot-Aware Cutting - 2026-06-28

- Decision: frontier bundle seed is an accepted continuation of the Generated-Root Whole-Board Planner line. V9 3-chain bundle keeps the real generated root, cuts semantic state-frontier chains before final trace, and preserves official `solved/A` with `6/8 HardPotential`.
- Evidence: `grwbp_v9_s1b_bundle3_b1b2` selects 3 state-frontier chains per level, relation audit shows every added chain is official touched, and attribution classes are `CrossDelayPressureWithLocalPenalty`, `CrossBasinPressure`, and `DelayChokePressureWithLocalPenalty`.
- Boundary: 3-chain bundle is still a direction proof, not the 0.95 solution. Coverage is only about `0.638-0.639`, supportCarrierCount remains 0, and best support root remains original root chain 40.
- Rejected interpretation: 4-chain failure is not solved by allowing same-edge chains, broadening basin filters, or widening beam. Distinct-edge, same-edge, and all-basin diagnostics all return 0 candidates; rejection is dominated by cell overlap at depth 4.
- Decision: next generator step must be slot/direction-aware whole-board chain cutting. Current state-frontier generation builds options using a fixed first added-chain direction, while final candidate construction assigns directions by added slot. Future bundle planning should generate option geometry per intended slot offset and bind selected chains to those slots before validation.
- Rejected path: do not chase coverage by B2 safe/capacity filler, LocalEasy high coverage, long bands/slabs, or process-only metrics. V9 success criteria remain semantic bundle edges plus official solved/A/HardPotential and relation audit visibility.

## Generated-Root WBP V10 Decision: Slot-Aware Cutting Is Valid, Spatial/Source Diversity Is Next - 2026-06-28

- Decision: slot/direction-aware chain cutting is now accepted as the next foundation for Generated-Root Whole-Board Planner. V10 options carry `intendedSlotOffset/intendedAddedDir`, bundle selection can enforce slot order, and candidate writing/validation must reject slot mismatch before official trace.
- Evidence: `grwbp_v10_s4_slotaware_bundle4_wide_fixed` produces 8 strict 4-chain candidates with slot offsets `0/1/2/3`, root preserved, coverage `0.6449275`, official trace 8/8 solved, process A, all HardPotential, and relation audit 488 edges / 336 parent rows. Difficulty attribution shows all 32 added chains official touched.
- Important bug record: any V10 run before the `option_int` zero fix can contain false slot-order positives because `intendedSlotOffset=0` was treated as unset. `grwbp_v10_s3_slotaware_bundle4_wide` is diagnostic only; `grwbp_v10_s4_slotaware_bundle4_wide_fixed` is the first valid V10 baseline.
- Boundary: V10 still does not solve 0.95 coverage. The valid 4-chain bundle adds only 20 cells, uses activation owner 24 only, and remains `supportCarrierCount=0` with `local_penalty_dense;no_added_support_carrier` risk. HardPotential is preserved, but the added chains are not yet new support closure roots.
- Rejected path: do not interpret the per-edge-72 run as “just increase caps until coverage rises.” The cap diagnostic proved the old per-edge-24 pool was spatially too narrow; V11 should make spatial diversity and source diversity first-class generator logic, not a manual budget crank.
- Follow-up direction: V11 should combine slot-aware cutting with spatially diverse option retention, wider activation/source contracts (support-corridor/all-owner frontier/non-24 root edges), and support-carrier/anti-local gates. Coverage growth must come from many short/medium semantic contracts across basins, not from long slabs, B2 capacity refill, or local pollution chains.

## Generated-Root WBP V11 Decision: Multi-Source Extension Is Valid Only When Non-B2 And Official-Touched - 2026-06-28

- Decision: `state_frontier_bundle_extension` is accepted as a transitional mechanism for testing source/basin expansion, but not as a final coverage strategy. It may extend a strict 4-chain state-frontier bundle with non-frontier semantic chains only when the resulting state remains greedy solved and official trace validates it.
- Evidence: s6d shows the bad version: letting extension choose `B2_CONVERGE_CHOKE` yields 5-chain solved/process A but all MediumStructure, with `local_penalty_dense;no_added_support_carrier`. B2 safe extension is therefore a rejected coverage shortcut.
- Evidence: s7/s8 show the good version: rejecting B2 and using V1 non-B2 chains produces owner10 `B1_CONVERGE_CHOKE` and owner7 `B1_BLOCKS_B2` after the owner24 frontier bundle. s7 5-chain and s8 6-chain both official `solved/A/HardPotential`; s8 has 3 release owners, all added chains official touched, and attribution includes `SupportCrossCarrier`.
- Boundary: s8 coverage only reaches `0.6565-0.6580`, so extension pass count is not the route to `0.95+`. It proves that multi-source short/medium semantic chains can preserve difficulty, then points to a next grammar step: generate source-frontier bundles for the new owners/basins rather than appending more old V1 options.
- Follow-up direction: V12/V11-next should make owner10/owner7-like non-B2 sources first-class: infer their source basin duties, produce their own state-frontier/choke/delay contracts, and add an anti-local/support-carrier gate so coverage growth comes from multiple basin bundles rather than from local filler.

## Generated-Root WBP V12 Decision: Separate Chain Body From Signal-Ray Contracts - 2026-06-28

- Decision: Whole-board planner semantics must separate physical chain connectivity from signal propagation. A planned chain body is always a contiguous non-overlapping adjacent-arrow chain; empty cells may only participate in first-hit/escape-ray reasoning, never as chain connectors.
- Decision: `state_frontier` and source-owner contracts should use planned first-hit/ray state: earlier/prereq root owners may be cleared for context, while the activation owner remains active as the blocker that releases the added chain. This matches `ThroughEmpty` signal behavior better than treating path chunks or rank labels as dependencies.
- Decision: direct-unlock retarget is allowed only as a semantic correction after state simulation: if a generated source-frontier option physically blocks one scanned target but actually opens another direct owner in the planned state, it can be relabeled to that actual unlocked owner and re-profiled.
- Decision: an added chain first-hitting another added chain is not automatically invalid, but it is accepted only when greedy/order evidence proves the added blocker clears earlier than the current chain. This models a planned added-to-added delay/choke edge rather than an accidental collision.
- Decision: generic beam selection is insufficient for multi-source frontier bundles. V12 needs deterministic activation-pattern seed states such as `24,24,24,7` so owner7/owner10 source-frontier options are tested in their intended slot instead of being starved by owner24-local high-score options.

## Generated-Root WBP V12 Decision: Activation Patterns Need Hard-Preserving Edge Gates - 2026-06-28

- Decision: deterministic activation-pattern seed states are accepted as the correct mechanism for testing first-class multi-source frontier bundles. They should enumerate intended activation owner slots before candidate writing, rather than relying on generic beam ranking.
- Evidence: `grwbp_v12_s8y_activationpattern_frontier_edgegate` generates 8 candidates with three owner24 source-frontier chains plus one owner7 `STATE_FRONTIER_B1_TO_B1 7->22` chain; all are greedy solved, official trace 8/8 solved, process A/tight A, and root identity remains traceable.
- Boundary: s8y is not a difficulty success. All 8 official rows are MediumStructure with hardV3 avg about `0.406`, despite good process curves. This confirms that process A + multi-source frontier + coverage gain can still lose hard attribution when the edge mix behaves like weak/followable B1 frontier.
- Rejected interpretation: do not treat s8y as a baseline to extend with more frontier-only chains, long bands, B2 safe extension, or coverage-first filler. It is a mechanism proof and a hard-preservation negative boundary.
- Follow-up direction: V12-next should select activation patterns using hard-preserving relation gates before final candidate emission: owner/source mix, support-corridor participation, non-local delay/choke targets, added support-carrier evidence, and anti dependency-follow-run scoring. Official trace remains final validation, not the per-chain generation decision.

## Reject PSG Long FillProbe V1 As Style Direction - 2026-06-28

- Decision: `PSGLongMazeSelfMadeV2FillProbeV1ReviewPack` is a negative result. It must not be treated as a competitor-like longchain lane, production candidate, or positive baseline despite being solved and high coverage.
- Evidence: the review pack has 6 levels with `43-46` authored chains but `624-778` arrow cells and `0.913-0.918` coverage. Manual screenshot review shows a dense rainbow line mesh rather than readable long-maze/lock structure.
- Decision: for style lanes, manual visual/topology review overrides `TraceOrderKeep`, B-tier process metrics, and coverage. A candidate that is solved and high coverage can still be rejected if it loses macro structure.
- Rejected path: coverage-first refill that protects only dependency corridors and fills all remaining cells. This preserves playability but destroys negative space, main-chain hierarchy, and corridor/lock readability.
- Required future gate: any longchain self-made/hybrid pack must report and gate visual density and morphology, including `arrowCells`, chain length distribution, top-chain share/spine concentration, empty corridor readability, local fill density, and screenshot/manual feel. Do not use authored chain count alone.
- Runtime pitfall: review packs with 600+ arrow cells can also stress Demo rendering because snake overlay segment counts scale with arrow cells and `segmentsPerTile`. Large style packs need preview/perf guards before manual review, but rendering guards do not fix bad level structure.

## Generated-Root WBP V12 Decision: Hard Edges Need Non-B2 Support Contracts - 2026-06-28

- Decision: V12 activation-pattern generation should preserve the proven V10 hard-edge owner24 bundle, then add non-B2 support/block contracts such as owner10 `B1_CONVERGE_CHOKE` and owner7 `B1_BLOCKS_B2`. This is a hard-preserving pattern; frontier-only or generic carrier extension is not enough.
- Evidence: `grwbp_v12_s10e_edgepattern_extend2_v1only_candidate6` uses ordered hard edges plus two V1-only non-B2 extensions. Official trace is 8/8 solved, process A, 8/8 HardPotential, coverage `0.6551-0.6580`, maxChoices avg 7, and root identity is preserved.
- Evidence: difficulty attribution reports all 48 added-chain rows official touched, supportCarrierCount=1 per level, no B2 safe/capacity owners, and added classes include `SupportCrossCarrier`, `CrossDelayPressure`, `CrossDelayPressureWithLocalPenalty`, and `DelayChokePressureWithLocalPenalty`.
- Boundary: this is still a coverage plateau, not the 0.95 solution. It proves that hard-preserving semantic contracts can be combined in V12, but added support still rides the original root closure and the best support root remains chain 40.
- Rejected path: do not continue with frontier-only owner mix (`s8y`), generic `CARRIER_` options, B2 safe extension, or chain-count/coverage-first filler. These solved/process-A candidates can remain MediumStructure when support-carrier and non-local delay/choke attribution are weak.
- Follow-up direction: promote owner10/owner7 support contracts into first-class whole-board duty planning. The next planner should allocate source basins, choke rays, support corridors, delay/block duties, and intentional empty cells across the whole board before cutting chains, then use official trace only as final validation.

## Generated-Root WBP V12 Decision: Hardbase Runner Gates And Secondary Duty Grammar - 2026-06-28

- Decision: V12 hardbase diagnostics should skip generic beam and start from deterministic activation-pattern seed states. Generic beam is too slow and tends to test capacity-like mixtures before the intended semantic bundle.
- Decision: hardbase edge pool must use spatial retention. Pure score order lets local high-score `24->26` variants overlap earlier hard edges; a per-slot spatial cap keeps lower-score but globally compatible chains such as the `21,2..21,5` 26-chain alive.
- Evidence: `grwbp_v12_runner_hardbase_ext_t4i_v1only` reproduces the s10e positive with runner defaults: official `4/4 solved/A/HardPotential`, hardV3 about `0.568-0.569`, maxChoices 7, no B2 filler.
- Negative evidence: allowing `CARRIER_*` extension on the same hardbase gives `grwbp_v12_runner_hardbase_ext_t4h_mincontract2`, official `4/4 solved/A` but `4/4 MediumStructure`. This reinforces that weak support/outer carrier extension is not equivalent to owner10/owner7 non-B2 support/block contracts.
- Decision: secondary-source expansion must not be solved by `allow-all-empty` or arbitrary filler. The first secondary smoke rejected mainly on `child_head_not_allowed` / `blocked_to_parent` / `child_no_path`, which means the whole-board cell plan lacks planned secondary source/choke/delay duty zones.
- Follow-up direction: add a source-basin duty grammar that reserves secondary parent/child head corridors, blocker rays, support corridors, and intentional empty escape spaces before chain cutting. Only after those duties exist should secondary-source chains be generated and official traced.

## Generated-Root WBP V12 Decision: Reject Post-Hoc Secondary, Move To Added-Chain DAG - 2026-06-28

- Decision update: the earlier “missing secondary duty zone” hypothesis is insufficient. V12 duty-zone diagnostics show the board already has nearly all non-root cells planned; post-hoc secondary fails because feasible parent-child first-hit geometry is mostly occupied by the generated root, selected hardbase/extension chains, or board edges.
- Evidence: `grwbp_v12_secondary_multidir_diag_t2a` with `U/R/D/L` secondary directions still produced secondarySource=0. Rejection is dominated by `child_head_not_allowed_root_occupied/out_of_board/selected`, `child_no_path`, and `child_not_parent_released`, not by `role_intentional_empty`.
- Evidence: even after disabling target blocked/direct/context requirements and full greedy requirement, `grwbp_v12_secondary_geometryonly_L_diag_t2a` remains secondarySource=0; the only terminal cases fail `child_first_hit_owner_mismatch`, meaning adding child chains after the fact changes existing selected-chain first-hit contracts.
- Rejected path: do not keep trying to rescue post-hoc secondary with wider direction sets, all-empty allowance, generic carrier extension, or weaker target semantics. Those either still fail first-hit geometry or would become weak/filler chains that collapse difficulty.
- New direction: the next planner step must represent added-to-added delay/block contracts as a first-class DAG during base/cluster selection. Parent, child, target blocker, and any selected chain whose first-hit is intentionally changed must be co-selected before chain cutting and validated as a planned relation, not treated as accidental mismatch after hardbase is frozen.

## Generated-Root WBP V12 Decision: Planned Added-Hit Requires Solved Cluster, Not A Gate Toggle - 2026-06-28

- Decision: allowing added-chain first-hit is valid only when the entire selected cluster remains greedy solvable and the edge is semantically intended. It must not be used as a broad mismatch suppressor.
- Evidence: `--enable-planned-added-chain-first-hit` did not rescue secondary L diagnostics. The terminal samples all became `planned_added_hit_greedy_unsolved`: child `SSF99501` first-hits parent but then blocks selected support chain `O00089 / B1_BLOCKS_B2`, causing full greedy unsolved.
- Decision: this proves the needed unit is not “parent + child appended after hardbase”; it is a cluster contract that reserves and validates parent -> child -> target/support/recovery ordering before the hardbase/extension state is frozen.
- Rejected interpretation: do not treat accidental child-to-O00089 first-hit as useful relation audit material. Because `blockedOwners` and `directUnlockedOwners` are empty and greedy fails, it is local pollution, not cross-basin/delay/choke pressure.
- Required next gate: cluster co-selection must emit sample/summary rows containing planned added-hit edges, victim/target option or owner, greedy solved/order evidence, and relation type. A candidate cannot pass merely because a first-hit mismatch points to an added chain.

## Reject PSG Long Stripe-Coil Format Completely - 2026-06-28

- Decision: completely reject the current self-made PSG longchain direction that produces neat repeated stripe/coil modules. This includes `GridStrip`, `CrossColumn`, `CrossColumnAlt`, staged region refill over those skeletons, and any similar "parallel serpentine block grid" output.
- Evidence: manual Unity screenshots show separated bands of identical U-turn coils and dotted snake rows/columns, which read as debug pattern or texture fill rather than a real competitor-like arrow-chain level. This is a structural failure, not a renderer-only issue and not a tuning miss.
- Rejected interpretation: `solved=True`, B process tier, acceptable `maxChoices`, or moderate coverage cannot rescue this format. A level that looks like repeated coils/stripes is rejected before production, even if trace metrics pass.
- Future hard gate: longchain/style lanes must reject visible regular tiling, equal-sized coil columns, repeated ladder/comb bands, stripe carpets, and any board whose macro structure is "many small identical loops" instead of a readable lock/maze/weave dependency shape.
- Boundary: the seed style learning result remains useful. The bad part is the self-made CrossColumn/skeleton+refill grammar; the retained direction is to learn one real seed type first and derive a non-copying generative grammar from its macro topology, dependency roles, negative space, and chain hierarchy.

## Campaign500 PSG Normal Rhythm Gate Production Decision - 2026-06-28

- Decision: Campaign PSG normal source-rhythm controls must default to score-only and trace preselection, not generation hard gates. Early/final rhythm hard gates are opt-in switches only.
- Evidence: strict final gate and early gate validation runs from order 11 produced near-zero built rows or unacceptable throughput. The same source-rhythm logic as score-only can still influence candidate ranking without collapsing production capacity.
- Decision: profile attempts must be configurable per validation/production run. `ProfileMaxAttempts=120` produced 76 attempt rows / 49 built for 20 slots, while uncapped profiles can spend 220-420 attempts per mode and make multi-mode slots too slow.
- Decision: PSG normal production labels and trace paths must stay short. Long chunk labels created asset absolute paths around 267 characters and caused official trace to mark existing assets missing. Use short output prefixes, short labels where possible, `subst` short roots, and normalized backslash trace paths.
- Current boundary: the `rhythmscore20_cap120b` keep pack still has only 3 watch rows and does not solve same-axis/same-dir continuity. Do not resume 200-slot production until mode budget and failed-slot deep rerun are added.

## Style Topology And Chain Language Taxonomy - 2026-06-28

- Decision: chain language must stay coarse as a first-class production/generation axis. Fine labels are useful for diagnostics, but too many first-class chain categories would fragment tuning and create one-off lanes.
- Decision: style topology and chain language are separate axes. Style names such as Flow, Peel, LongChain, Hub, Cascade, Maze, and Basin describe solve-order topology; chain language describes local geometry/flavor inside that topology.
- Current main chain-language buckets: `rail_chain`, `curve_chain`, `hook_chain`, `spine_chain`, `patch_chain`, and `mixed_chain`.
- Reporting rule: `chainLanguage` stores the coarse bucket; `chainLanguageDetail` and `chainTags` keep fine labels such as straight lane, long chain, peel layer, woven medium, lock/core cluster, and edge-headed traits.
- Boundary: `hook_chain` is currently an approximate report bucket derived from style-family and aggregate chain metrics. Do not overfit generation to hook details until per-chain turn-shape / bend-position metrics exist.
- Generation direction: future lanes should be style x coarse-chain-language profiles, e.g. `Peel + rail_chain` or `Peel + curve_chain`, rather than a separate generator for every fine chain subtype.

## Generated-Root WBP V12 Decision: Chain Rules Are Hard Planner Constraints - 2026-06-28

- Decision: WBP must keep chain connection and signal propagation separate. `intentional_empty` cells may reserve escape/probe ray space, but they cannot be treated as hidden chain connectors or as a way to bridge two arrow chunks.
- Decision: authored `indices` remain head-to-tail in Python outputs. Unity may build runtime arrows in the opposite internal direction, but the planner/writer must not reverse the list again.
- Decision: WBP candidate proof must use the round-tripped board, not only the raw authored path list. `AuthoredLevelBuilder` intentionally rewrites `inDir` when a different authored chain would become an external predecessor, so cross-chain adjacency/direction accidents can change actual chain membership and first-hit behavior.
- Decision: WBP role names such as `blocker`, `guard`, `choke`, and `release` are semantic contracts over arrow chains/rays unless a design explicitly writes `blockIndices`. A semantic blocker chain must still be an authored arrow chain that can eventually clear; it is not free static `Block` terrain.
- Decision: signal-ray reservations are part of the whole-board cell plan. Because `ThroughEmpty` can travel through empty cells but stops before another arrow, later chain cutting must not place unplanned arrows into intentional escape/probe rays.
- Decision: root preservation and legality audit are not enough if a candidate relies on loop-like authored geometry. Existing `chainLoopRiskCount` should become a stronger rejection or warning gate before any formal seed pack export.
- Decision: official difficulty review must account for greedy best-clear behavior. The built-in `GreedyValidator` and tuning metrics repeatedly choose the move that clears the most arrows, so a planned dependency that exists under one hand-picked order can still be invalid for production if greedy bypasses or smooths it.
- Evidence: V12 `state_frontier_edge_option_summary` diagnostics show many ranked source edges have no realizable option because head/activation/target corridors are already occupied or disallowed. Therefore scan edge count is only a feasibility hint, not root capacity proof.
- Follow-up direction: future WBP root/cell-plan selection should optimize for actual per-edge option capacity and reserved corridors before chain cutting, then use official trace as final validation rather than a per-chain generator.

## Generated-Root WBP V12 Decision: Capacity Must Include Corridor Diversity - 2026-06-28

- Decision: for Generated-Root WBP, root/edge/cell-plan selection must gate on actual option capacity plus spatial corridor diversity. A high `capacityScore` or many accepted options from one corridor is not sufficient for whole-board expansion.
- Evidence: default root capacity ranking correctly preferred `24->33` over weak `7->54`, but per-edge caps still showed the old four hard edges cannot form a 4-chain non-overlapping pattern: each slot had options for all targets, yet 160000 checked combinations had no disjoint 4-chain solution.
- Evidence: source expansion and two root-shortlist probes still concentrated accepted options around owner24 corridor (`b4,0/b4,1` or nearby), with only tiny secondary pools such as `7->22` or `19->43`. This explains why exact6 can work but exact7/coverage expansion collapses into overlap or weak local flow.
- Rejected path: do not treat old hard edge pattern `24->33/51/42/26`, high ranked-edge scan count, extension passes, B2 safe chains, or same-corridor short-chain pools as evidence that the planner can reach `0.95+` while staying hard.
- Required next step: whole-board cell planning must explicitly reserve multiple release corridors, target rays, source basins, choke/delay lanes, and intentional empty probe/escape space before chain cutting. The planner should then cut semantic clusters/DAGs across those reserved regions, not greedily consume the highest-capacity corridor first.

## Generated-Root WBP V12 Decision: Max-Disjoint Capacity Is A Root Gate - 2026-06-28

- Decision: root shortlist ranking for WBP must include `maxDisjointDistinctEdge` or an equivalent disjoint corridor metric. Hard score, support depth, raw frontier option count, and single-edge capacity are insufficient.
- Evidence: V12 now emits `state_frontier_disjoint_capacity_summary`; default root open/source pool has rawOptions=204 but maxDisjointDistinctEdge=3. The first three shortlist roots scored hardV3 `0.765/0.761/0.743`, but maxDisjoint was only `2/3/2`.
- Decision: a root/cell-plan pair with maxDisjoint below the intended semantic chain cluster size should not enter high-cost activation-pattern or candidate-generation stages except as a diagnostic. It proves the current whole-board plan lacks enough independent corridors before official trace is even relevant.
- Follow-up direction: implement multi-corridor reservation in the cell-plan stage, then rerun the root-disjoint probe. The acceptance signal for this stage is not coverage yet; it is seeing maxDisjoint reach at least the planned cluster size with sources/spatials spread across multiple basins.

## PSG Long Lock Decision: Planned Corridor Duty Is The Production Core - 2026-06-28

- Decision: accept front-loaded `CorridorDuty` planning as the current positive core for self-produced `seed_long_lock` style levels. The generator should plan cross/support carrier duties before generic carrier/filler placement, then validate authored output with Greedy and official trace.
- Evidence: worktree run `psg_long_lock_role_grammar_v1_planduty_v1d_smoke2` produced 2/2 source rows and 2/2 `TraceOrderKeep`, improving over the role-metrics baseline where `crossRegionCarrierChains=0/0` and only 1/2 rows kept.
- Required reporting: future long-lock candidates should carry `plannedCorridorPlaced`, `plannedCrossRegionPlaced`, `plannedDutyReleaseCells`, `supportCorridorChains`, and `crossRegionCarrierChains` through source and joined/keep CSVs.
- Boundary: calibrated smoke seeds are acceptable for fast deterministic review, but scale-up must search and score through this planned-duty grammar rather than handplacing coordinates or copying real seed cells.
- Rejected paths remain rejected: GridStrip/CrossColumn/staged refill, repeated stripe/coil visual formats, broad online structured search inside every attempt, and raw fanout tightening without a constructive corridor/support plan.

## Generated-Root WBP V12 Decision: Disjoint Geometry Must Be Slot/First-Hit Feasible - 2026-06-28

- Decision: keep max-disjoint capacity as a root gate, but do not treat pure cell-disjoint geometry as sufficient for a seed state. A planned bundle also needs slot-compatible `intendedAddedDir` and first-hit contracts that still point to the intended root owner after earlier added chains are present.
- Evidence: `rootlang_root10_0615_section_short_r1_c024` is the first shortlist root with a real 4-edge hardbase: `grwbp_v12_rootlang_edgepattern_t1` produced 4 Greedy-solved, chain-legal candidates from explicit edges `24->29,7->22,24->33,24->42`.
- Evidence: the same root's wide pool reaches maxDisjointDistinctEdge=5 (`7->22,24->55,24->26,24->42,24->33`), but `grwbp_v12_rootlang_edgepattern5_t1` produced 0 states because order/slot validation hit `target_mismatch` and `first_hit_owner_mismatch_added`. Therefore the next planner unit must be a slot/order/DAG bundle, not a post-hoc extension pass.
- Decision: corridor reservation remains useful only if it improves disjoint/slot diversity. The current greedy reservation can add duty cells on rootlang, but it biased options back into b4/b5 corridors and reduced the exact disjoint example from 4/5 to 3 in one probe; reservation selection must become max-disjoint-aware before it is enabled by default.
- Rejected path: do not continue trying to raise coverage by loosening extension pool, allowing same release owner, or treating added-chain first-hit mismatches as acceptable. Those experiments failed by cell overlap or accidental added hits and would recreate sandwich/post-hoc filler pollution.

## Generated-Root WBP V12 Decision: Planned Added-Hit Is A Formal DAG Edge - 2026-06-28

- Decision: a state-frontier option may first-hit another added chain only when `--enable-planned-added-chain-first-hit` validates it as a solved, ordered added-to-added DAG edge. The broad `--state-frontier-allow-added-chain-first-hit` switch remains diagnostic only and should not define a formal candidate line.
- Evidence: `grwbp_v12_rootlang_edgepattern5_plannedaddedhit_t1` reproduces the 5-edge rootlang candidates without the broad mismatch toggle. Each candidate has `plannedAddedFirstHitCount=1`, `addedDagEdgeCount=1`, `addedDagCycleCount=0`, `chainLegalityOk=True`, `addedChainLoopRiskCount=0`, and Greedy solved.
- Evidence: official trace for the planned-added run is `4/4 solved`, process A/tight A, maxChoices avg 6, missing/failed=0. This proves the authored assets import and replay under the real trace gate, not only under Python-side option validation.
- Boundary: all four planned-added candidates remain MediumStructure with hardV3 avg about `0.419` and risk `DependencyFollowRun`. The planned DAG edge proves a semantic mechanism, not the final hard/coverage target.
- Rejected path: do not interpret a clean planned added DAG edge as permission to keep adding more same-corridor frontier edges. The 6-edge planned probe with added `24->51` reports `disjointFull=0` and `maxDisjointDistinctEdge=5`; the current root/cell-plan lacks a sixth independent authored corridor.
- Next direction: the planner must allocate multi-corridor whole-board duties before chain cutting: release basin lanes, target/choke rays, delay/block DAG edges, and intentional empty escape/probe cells. The acceptance signal for this stage is a larger disjoint semantic corridor set plus HardPotential/TrueHard attribution after official trace.

## Generated-Root WBP V12 Decision: Raw Duty Capacity Is Diagnostic, Strict Duty Capacity Is A Gate - 2026-06-28

- Decision: whole-board duty/ray probes must report raw geometry capacity separately from strict cuttable capacity. Raw release->target-ray paths are useful for seeing unused space, but they are not valid chain candidates until they pass occupancy, first-hit, frontier candidate, single Greedy, added loop-risk, and release-impact safety.
- Evidence: rootlang raw/all-direction duty capacity reached well above 5, and seed injection proved some paths were missing from the old frontier option pool. However the apparent sixth chain collapsed either as `addedChainLoopRisk` (`24->55` 2x2 loop-like seed) or as `blocks_pre_release_owner` (`40->36` blocking owner48 before release owner40).
- Decision: `maxDisjointDistinctEdge` used as a root/cell-plan gate must be computed under the strict safety口径, not from raw cell-disjoint geometry. For rootlang, strict no-loop + no-pre-release-block capacity is 5; treat this as a real upper bound for the current root/cell-plan.
- Decision: duty seed injection is acceptable as an opt-in diagnostic/bridge only when it revalidates all formal gates. It should not become a backdoor that imports probe rows as authority.
- Rejected path: do not use loop-risk chains, pre-release blockers, broad added-hit allowance, or same-corridor option retention tricks to claim 6+ chain capacity. They reproduce the old sandwich/RCH problem: coverage mechanics improve while difficulty/causal integrity collapses.
- Required next step: rank or generate roots by strict duty/ray capacity and source/spatial diversity before chain cutting. A root that cannot support the planned semantic cluster size under strict gates should not be sent to high-cost activation-pattern/coverage expansion runs.

## Generated-Root WBP V12 Decision: Strict Capacity Is Necessary But Not Sufficient - 2026-06-28

- Decision: strict duty/ray capacity should become an early root/cell-plan gate, but passing it does not prove hard-preserving coverage growth. It must be paired with root footprint/base coverage, support-closure strength, anti-locality, and explicit cross-basin/choke/delay contract requirements.
- Evidence: new batch wrapper `Invoke-GeneratedRootWBPV12StrictDutyRayRootGate.ps1` found `geosupply_sched_root10_dens_c6277f51_r1_c007` as the best top6 root with strict `bestChainDisjoint=6` and `bestReserveDisjoint=6`; other top6 hard roots were 4-5. This improves over rootlang's strict 5, but still does not approach the 8+ multi-basin capacity likely needed for 0.95.
- Evidence: c6277 duty-seed exact6 produced one chain-legal, root-preserved, Greedy-solved 6-chain candidate with no added loop-risk and all 6 added chains official touched. Official trace solved with process A/tight A and maxChoices 5, proving the seed bridge is real.
- Boundary: the same candidate has coverage only `0.5492754` and official `hardStructureV3Class=MediumStructure` (`hardV3=0.429`, risk `DependencyFollowRun`). Attribution shows `support=1`, local penalty dense, and three early B1 chains from owner24; the added chains create pressure but not enough persistent support/choke depth.
- Rejected path: do not treat a 6-corridor root as a new baseline if it lowers coverage/root footprint or preserves only shallow state-frontier contracts. The next generator mutation must create multi-source capacity and hard contracts together, not first maximize capacity then hope official trace upgrades it.
- Required next step: extend root/cell-plan selection or generation to require strict capacity plus relation-quality proxies: fewer same-release early B1 slots, higher cross-basin/choke/delay share, planned support carrier count above 1, and enough root/base coverage to make 0.95 reachable without filler.

## Generated-Root WBP V12 Decision: Corridor Occlusion Belongs To Root Generation - 2026-06-28

- Decision: do not rely on `all_empty` / broader mutable space to recover missing semantic corridors after the root exists. If `current` and `all_empty` produce the same strict duty capacity, the blocker is the generated root geometry and preflight contracts, not a role-label omission.
- Evidence: `grwbp_v12_occlusion_tracewide_top3_t1` shows the best trace-wide strict6 root has identical current/all_empty capacity, planBlockedTotal=0, and heavy root/out-of-board blocker counts. The 0.605 coverage strict5 roots show the same pattern.
- Evidence: high-footprint root `root154_core_sched0657_v1_r1_c018` has coverage 0.661943 but strict disjoint 1; 18 raw edge rows collapse to 1 post-preflight edge. Overfilled root geometry consumes or destabilizes head/corridor/second positions before chain cutting.
- Decision: root/cell-plan generation must reserve corridor holes, target-ray duty space, and boundary-safe head/second neighborhoods as part of generated root construction or mutation. Post-hoc chain cutting should only use roots whose occlusion audit leaves enough formal corridor capacity.
- Rejected path: do not score root footprint monotonically upward, and do not interpret raw candidate count as capacity when post-preflight edge rows collapse. Coverage pressure must be balanced against corridor openness and relation diversity.

## Generated-Root WBP V12 Decision: Corridor Demand Is A Generation Constraint - 2026-06-29

- Decision: occlusion-derived owner/cell demand rows are constraints for the next generated-root pass, not permission to edit away cells from an already verified root. The root must remain generated and traceable after any mutation/regeneration.
- Evidence: `grwbp_v12_corridor_demand_occlusion_c016_vs_highfoot_t1` converts top blockers into `root_generation_corridor_hole`, head-neighborhood, and boundary-safe recommendations. These recommendations explain where future root generation should reserve holes before whole-board chain cutting.
- Decision: demand reports should preserve the distinction between authored chain legality and signal propagation. Empty cells may be demanded as signal/escape/reserve space, but they are not authored chain connectors and cannot justify non-adjacent arrow chunks.
- Rejected path: do not turn corridor demand into post-hoc root deletion, capacity-only filler, or a hand-tuned mother-board. A valid next root must still pass Greedy, official trace, root identity, and chain legality with authored head-to-tail chains.

## Generated-Root WBP V12 Decision: Reservation-Fit Is A Pre-Gate, Not A Cutter Baseline - 2026-06-29

- Decision: demand-hotspot root ranking is allowed only as a read-only pre-gate over real generated roots. It may choose which roots enter strict duty/ray probing, but it does not prove chain capacity, hard difficulty, or 0.95 viability.
- Decision: corridor demand coordinates are board-coordinate constraints. Unless a normalized coordinate transform is explicitly implemented and verified, demand-fit comparisons must require matching board size; cross-size roots are marked `size_mismatch` by default.
- Evidence: same-board reservation-fit from c016 demand improves historical root selection only slightly: top roots leave about `3-8%` of hotspot weight open, while c016/high-footprint leave `0%`. Strict reuse over the first 8 fit roots reaches best chain/reserve `7/7`, still below the target 8+ semantic corridors and still tagged `low_root_footprint`.
- Interpretation: this result supports the user hypothesis that late weak exits / weak dependency chains cannot be fixed by after-the-fact filler. The missing layer is root-generation and whole-board cell-plan reservation, not another pass of local chain cutting.
- Required next step: move from selecting historical roots to generating or mutating roots with explicit same-board corridor-hole, head/second safety, source-basin diversity, target-ray, delay/block/choke/support reservations before chain cutting.

## Generated-Root WBP V12 Decision: Next Unit Is Source-Basin-First Root Co-Generation - 2026-06-29

- 状态：实验中
- Decision: the next implementation unit should be a source-basin-first root/whole-board co-generator, not further reserve-mask tuning, low-choice semantic fill, or edge-level corridor representation changes.
- Evidence: reserved-root scratch generation with 51 demand cells kept empty produced Greedy-solved roots around coverage `0.56`, but strict capacity stayed `0/1`; empty holes do not create causality domains.
- Evidence: preserve-nonreserve mutation of `geosupply_oh_root154_multi_r12_dense_len10_c013` can reach coverage `0.62+`, but strict capacity tops at `5`; the best strict5 edges have 4 activation owners yet collapse to 2 top root ancestors after reverse parent tracing.
- Evidence: semantic fill creates low-choice roots (`greedyInitial 3-4`, `greedyMax 4-5`) but strict capacity drops to `2-3` because generated edges concentrate around a few activation owners/top-root ancestors. Low choices are not equivalent to multi-basin strict capacity.
- Evidence: `Build-GeneratedRootWBPV12SourceBasinAuditV1.py` audited 46 selected chain/reserve edge rows from reservation-fit strict7 and reserved-root probes; 0 passed source-basin gate. The best strict7 rows have 6 activation owners but only 1 activation top root ancestor and 0 cross-top-root edges, proving owner diversity alone is not a valid progress metric.
- Evidence: `Build-GeneratedRootWBPV12SourceBasinRootGeneratorV1.py` proves the first source-basin-first root-generation primitive, but also sets the boundary. Loose multi-basin roots reach coverage around `0.58` but open 16 choices and strict capacity `0-1`; pressured hybrid roots keep Greedy max `6-8` and topRoots `6-8`, but strict capacity only `2-3` because candidate edges are not planned around target-ray/choke outlets.
- GPT review: Rosetta/ChatGPT Pro agreed the missing variable is independent activation causality domains. Recommended gates for the next prototype: basin independence >=3, cross-basin interference edges >=4, no single activation owner >30-35% dominance, Greedy max <=10, and strict capacity must first break 6 before treating the route as progress.
- Rejected next step: do not next replace hard cell reserve with edge-level corridor contracts. That may clean up geometry but will likely reinforce the same collapsed activation spine unless source basins are generated first.
- Gate update: future WBP root/co-generator candidates must report top-root ancestor diversity, not only release owner diversity. A candidate that cannot reach at least 3 activation top roots and several cross-top-root/cross-basin contract edges is diagnostic only, even if strict capacity, coverage, or low-choice Greedy looks better.
- Design update: source basin generation must include planned basin outlets and target-ray/choke duty zones, not just spatial partitioning. A valid next prototype should require each source basin to contribute at least one strict-probe-visible release->target candidate before root selection, then raise disjoint capacity toward 6+.

## Generated-Root WBP V12 Decision: Outlet Proxy Is Diagnostic, Strict Edge Eligibility Is The Gate - 2026-06-29

- Decision: cheap outlet/target-ray proxy may be used for root diagnostics and sorting, but it is not an acceptance gate. It must not replace `Probe-WholeBoardDutyRayCapacityV1.py` strict duty/ray results or source-basin audit.
- Evidence: `Build-GeneratedRootWBPV12SourceBasinRootGeneratorV1.py` t4 added deterministic cell ordering and `outletProxy*` fields. `t4c_c001` looked promising at proxy `4/3/3` with Greedy max `6`, but strict capacity was only `2` and source-basin audit found 1 activation top root / 0 cross-top-root edges.
- Evidence: `t4b_c005` had stronger proxy `10/6/9` and Greedy max `11`; strict improved only to `3`, then audit still rejected it with 2 activation top roots and 0 cross-top-root edges. This shows proxy has weak ranking signal but overestimates formal capacity.
- Decision: the next generator iteration must front-load strict edge eligibility: source/target owner pairs should be created or selected using the same basin labels, activation/target profile filters, target-ray context, first-hit geometry, and preflight assumptions as the strict probe, or a validated cheap equivalent.
- Guardrail: do not tune `ray_corridor_bonus`, target coverage, opener count, or proxy thresholds as the main path. Those can make nicer diagnostics while preserving the same top-root collapse.
- Reproducibility rule: generated-root experiments must avoid unordered set iteration in candidate pools. If a root seed cannot be replayed to the same authored chains, it is not acceptable as a root-identity baseline.

## Generated-Root WBP V12 Decision: Cuttable Proxy Is The Earliest Gate - 2026-06-29

- Decision: for source-basin generated roots, `strictEdgeProxy*` is only a cheap relation audit. It must not be interpreted as strict capacity, source-basin pass, or whole-board chain-cut feasibility.
- Evidence: t5 selected `grwbp_v12_sourcebasin_rootgen_v1_t5_c001` with `strictEdgeProxy=62/6/16`, but official strict duty/ray gate reduced it to `bestChainDisjoint=1` and audit found only 1 activation top root and 0 cross-top-root edges.
- Decision: opt-in `strictCuttableProxy*`, which reuses the strict duty/ray probe's collect_edges, cell-plan scan, candidate preflight and disjoint selection, is the earliest acceptable generation-side gate for this line.
- Boundary: `strictCuttableProxy` is still a root-pool preflight, not a final level proof. Final candidates still require authored chain legality, builder/round-trip actual-chain audit, Greedy/import, strict duty/ray gate, source-basin audit, official trace, process tier, difficulty class, and root identity preservation.
- Next design implication: root growth must reserve or generate concrete head/second/path duty slots for cuttable candidates while the root is being built. Planning many owner/target relations after root growth is too late because root cells have already consumed the cuttable geometry.

## Generated-Root WBP V12 Decision: Spatial Slot Reservation Alone Is Not Enough - 2026-06-29

- Decision: no-preflight cuttable slot capacity is a useful desired-contract signal, but it is not an acceptance gate. A slot counts only if it survives formal cuttable preflight or if the generator explicitly plans the root-DAG/added-DAG state that makes the preflight true.
- Evidence: t8 source-basin root with no-preflight slot reservation planned `5` slots across `3` activation top roots and `2` cross-top-root edges, yet final strict cuttable proxy remained `1/1/0`.
- Evidence: the same t8 root under `-NoCuttablePreflight` reaches quality `chainDisjoint=6` with 6 release owners, 4 source basins, 5 cross/choke/delay/support proxy edges; source-basin audit passes the chain row with 4 activation top roots and 3 cross-top-root edges.
- Evidence: formal preflight failure is dominated by `frontier_candidate_failed`, `first_hit_exits_board`, `blocks_pre_release_owner`, `blocks_release_owner`, and `first_hit_owner_mismatch_base`. This confirms the current blocker is release-order/root-DAG/first-hit compatibility, not simply missing empty cells.
- Evidence: t8 preflight ablation shows priority. `NoCuttablePreflight` gives quality 6; release-order/first-hit core preflight gives quality 5; `NoFrontierCandidatePreflight` also gives quality 5; full strict gives quality 1. Therefore single-Greedy is not the bottleneck, frontier profile is a later bottleneck, and the first generator-side target is release-order/first-hit compatibility.
- Rejected path: do not keep adding slot reservations, empty holes, or no-preflight capacity as progress once strict preflight collapses. That repeats the old capacity-only filler failure mode.
- Required next step: implement release-order-aware desired-slot generation. For each desired edge, root growth must either avoid blocking its release owner and all pre-release owners, or deliberately schedule the blocker as a later cleared owner; it must also preserve first-hit ownership and frontier profile validity before chain cutting.

## PeelRailV1 Decision: Validate Style x Chain-Language Axis - 2026-06-28

- Decision: treat `PeelRailV1` as the first explicit style-topology x chain-language lane: `Peel + rail_chain`. It should stay separate from PSG and Nutation rather than being folded into a mixed PSG pool.
- Evidence: `peel_rail_v1_smoke4` produced 4/4 solved levels with `chainLanguage=rail_chain` 4/4; average source straightness is about `0.700` compared with `NutationPeelV2` about `0.118`.
- Boundary: this validates controllability, not production readiness. Smoke4 still has elevated opener/edge-head and near-outer/same-axis watch signals, so the next acceptance step is reducing local exterior/axis continuity while preserving rail identity.
- Rejected path: do not fix rail identity by making all chains maximally straight. Earlier smoke2/smoke3 pushed straightness into the `0.80+` range and became visually too striped/extreme.

## Nutation Decision: Family x Style x Chain-Language Matrix - 2026-06-28

- Decision update: `Nutation` is a production/generation family parallel to PSG, not a synonym for curve/snake chains.
- Classification: `Family=Nutation`; `Style=Flow/Peel/LongChain/Hub/Maze/...`; `ChainLanguage=curve_chain/rail_chain/hook_chain/spine_chain/patch_chain/mixed_chain`; concrete lanes are named `Nutation{Style}{Chain}Vn`.
- Current mapping: `NutationPeelV2` is the curve baseline (`Nutation / Peel / curve_chain`, alias `NutationPeelCurveV2`); old `PeelRailV1` is reclassified and renamed in the Nutation worktree as `NutationPeelRailV1`.
- Storage decision: all Nutation family experiments continue in `.worktrees/nutation-peel` for now. Main project receives only stable review packs or final candidates.
- Boundary: do not split rail into a separate worktree/family. The earlier empty `.worktrees/peel-rail-v1` was removed after clean verification.
- Negative evidence: rail smoke3, which reduced boundary/outer profile too strongly, produced 0 keep rows and maxChoices up to 18. Therefore rail needs boundary release; edge-head control should come from candidate scoring/search and opener role control, not simply weakening peel boundary grammar.

## NutationLongChainSpine Decision: Prove Style Axis Before Production Coverage - 2026-06-28

- Decision: `NutationLongChainSpineV1` is accepted as a style/language proof lane (`LongChain / spine_chain`) inside `.worktrees/nutation-peel`, but not yet as a production lane.
- Evidence: smoke3 gives 4/4 traced solved, 4/4 `spine_chain`, and 2 TraceOrderKeep rows with avg chain length about `14.6` and max chain about `28`, clearly separated from Peel curve/rail lanes.
- Decision: LongChainSpine may stop filling near the style target and reject very short tail chains. This is preferable to forcing PSG-like coverage with 2-4 cell filler chains, which erases the long-chain identity.
- Boundary: coverage is currently around `0.91-0.95` rather than PSG's normal `0.97+`. Future work should raise coverage through medium/role-aware side chains, not by reviving stripe/coil skeletons or allowing tiny filler fragments.

## NutationHubSpoke Decision: Style Proof Needs Hub-Specific Gates - 2026-06-29

- Decision: `NutationHubSpokeV1` is accepted only as a Hub style proof lane, not as a production-ready TraceOrder lane.
- Evidence: current trace validates 3/3 solved and the source status shows real fanout structure (`maxFanout=4-5`, `hubOwners=19-22`, `ownerHit=84-98`, `cross=11-19`), but joined trace still reports `flowLanguage=local_collapse`, `riskBand=high_risk`, and `stsPass=0`.
- Decision: Hub should not be forced through the same PSG TraceOrderKeep criteria as Peel/normal PSG. Hub needs a dedicated gate using fanout strength, hub owner count, cross-region hit ratio, same-region pollution, and acceptable local-collapse bounds.
- Rejected path: pushing strict local rejection and long spoke lengths into construction killed yield or hung generation. For Hub, keep generation-side controls soft until a Hub-specific post-trace acceptance model exists.

## NutationHubSpokeV2 Decision: Scoring Migration Is Not Enough - 2026-06-29

- Decision: keep `NutationHubSpokeV2` as an anti-collapse probe and review artifact, not as a production lane.
- Evidence: smoke4 validates 3/3 solved and keeps the `hub_spoke / patch_chain` label. It improves some process-shape metrics versus V1 (`localPatchRun` average `9.0 -> 8.33`, same-axis max `15 -> 10`, same-dir max `12 -> 10`), but still has `stsPass=0`, `productionKeep=0`, and all rows remain `local_collapse/high_risk`.
- Decision: do not continue tuning only head scoring, fanout scoring, or edge-head penalties for Hub. The failure mode is solve-order collapse after a valid hub graph exists.
- Follow-up: next Hub attempt should introduce solve-time anti-collapse or explicit hub-stage scheduling, then feed that back into final candidate selection. Local construction penalties can remain soft but should not be the main strategy.

## NutationFlowCurve Decision: Keep Flow As Drop-Allowed Baseline - 2026-06-29

- Decision: `NutationFlowCurveV1` is a baseline/review lane for `Flow / curve_chain`, not a strict production lane.
- Evidence: smoke1 gives 4/4 solved, 4/4 `flow_continuous`, 4/4 `curve_chain`, coverage `0.982-0.992`, and low straightness `0.123-0.153`; it cleanly proves the Flow+curve bucket.
- Boundary: all four rows are official `processTier=Drop` because Flow naturally has high choice counts (`maxChoices=12-15`, `avgChoices=8.77-9.82`). The wrapper intentionally allows Drop under `VisualOnly` review mode, and this must not be confused with PSG/Nutation strict production acceptance.
- Follow-up: future productionization should add a Flow-specific label/gate instead of relying on generic `TraceOrderKeep` rank text when Drop is allowed for review.

## NutationMazePatch Decision: Keep Maze As Style Proof Until Anti-Collapse Exists - 2026-06-29

- Decision: `NutationMazePatchV1` proves the `Maze / patch_chain` bucket but is not a production lane yet.
- Evidence: smoke3 gives 4/4 official trace solved, 4/4 `constraint_maze`, 4/4 `patch_chain`, coverage `0.905-0.924`, avgChain about `6`, and maxChain `10-11`, clearly separated from Flow/Peel/LongChain/Hub.
- Boundary: all rows remain `local_collapse/high_risk` with localPatchRun `12-18`, directionalRisk `0.607-0.742`, and 0 production keep. The current gate-aware owner-hit grammar creates distributed patch dependencies, but solve order still collapses into local same-axis patches.
- Follow-up: Maze needs a dedicated anti-collapse layer: reward cross-region owner-hit ratio and region alternation, cap same-region owner-hit density, and add a trace-sketch gate before calling it production-ready.

## Nutation Style Matrix Decision: Use Readiness Export Before More Production - 2026-06-29

- Decision: use `Export-NutationStyleMatrixV1.ps1` as the current Nutation family readiness/export surface before copying packs or producing larger batches.
- Evidence: the matrix aggregates 6 current lanes / 23 rows and separates readiness cleanly: Flow review-only; PeelCurve, PeelRail, and LongChainSpine strict-review-ready; HubSpokeV2 and MazePatch need solve-time control.
- Boundary: the matrix is a read-only reporting/export layer over joined CSVs. It does not certify new production rows by itself and must not be treated as a generator-side fix for Hub/Maze.
- Follow-up: production should scale first from strict keep rows in PeelCurve/PeelRail/LongChainSpine. Hub/Maze require solve-time anti-collapse selection or explicit stage scheduling before production claims.

## Nutation Hub/Maze Decision: Keep Anti-Collapse Review Separate From Production - 2026-06-29

- Decision: Hub/Maze candidates must go through `.worktrees/nutation-peel/Tools/Production/Export-NutationAntiCollapseReviewV1.ps1` before any visual review, and the resulting pack remains style-proof only.
- Evidence: the exporter ranks 7 Hub/Maze rows and selects 4 review rows. Hub has two plausible style-proof rows, but still carries collapse/same-axis/same-dir gaps. Maze remains diagnostic with much larger local/directional gaps.
- Boundary: `NutationHubMazeAntiCollapseReviewPack.asset` must not be mixed into strict Nutation production packs. It exists to inspect style identity and quantify blockers.
- Follow-up: the next real generator improvement for Hub/Maze should target dominant gap tags (`collapse`, `same_axis`, `same_dir`, `local`, `directional`) through solve-time selection or explicit stage scheduling, not generic head/fanout weight tuning.

## PSG Long Lock Decision: Random Patch Fill Is Not The 0.95 Route - 2026-06-29

- Decision: do not continue raising `PSGLongLockRoleGrammarV1` coverage by adding random/free micro patch fill after the planned-duty grammar. It can raise local density, but it breaks the acyclic release structure and produces Greedy failures around `0.92+` coverage.
- Evidence: high-coverage probes added short release caps, edge support carriers, loose dependent filler, free filler, micro support, and NoMask-style patch-head scanning. They did not produce a source keep at `0.915+`; observed high-cover candidates reached about `0.927` but failed Greedy, while Greedy-passing candidates stayed around `0.919` or lower.
- Evidence: the mature SGP/NoMask pressure-hard path generated 4/4 source rows at `0.978-0.994` coverage and trace solved 4/4, proving the repository already has a complete high-coverage production mechanism.
- Boundary: SGP pressure-hard is not a long-lock final answer by itself (`maxChain=21`, avg chain length about `10`). It should be treated as the coverage/densify mechanism to reuse or adapt, not as the target style product.
- Next implementation should make high coverage and solve order a single planned-chain process: either migrate SGP placed-chain densify/patch logic into a long-lock role planner with acyclic blocker ownership, or longify/merge SGP candidates with Greedy and official trace validation after every merge.

## PSG Long Lock Decision: Verified SGP Longify Is The 0.95 Route - 2026-06-29

- Decision: accept SGP high-coverage source + verified chain-longification as the current 0.95+ `seed_long_lock`-oriented route. It reuses the proven SGP density machinery and adds long-spine visual language by merging chains under strict validation.
- Guardrail: a merge is allowed only if the rebuilt authored level passes board build and Greedy immediately after the merge; official trace/join remains mandatory before any production keep claim.
- Evidence: `PSGLongLockLongifyV1` generated 4/4 source rows at coverage `0.9778-0.9951`, maxChain `52-75`, and official trace solved 4/4; strict join with `MinCoverage=0.95` produced 2 TraceOrderKeep production rows at coverage `0.992/0.993`.
- This is not seed-coordinate copying or mirror mutation. It starts from generated SGP high-cover products and restructures their chain language while preserving solvability.
- Boundary: do not treat the one-off longify tool as full production packaging yet. Next work should add a wrapper, keep-pack export/order, diversity/risk gates, and adapt the same complete route to the remaining lock types.

## PSG Long Lock Decision: Runbreak Split Is Required For Dense-Weave Production - 2026-06-29

- Decision: keep `MaxSolveSameDirHeadRun=7` as a real production gate and solve dense_weave with structural runbreak split, not threshold relaxation.
- Evidence: full Longify V1 split run generated 48 source rows, 48/48 official trace solved, and strict join produced 32 `TraceOrderKeep` production rows at `MinCoverage=0.95`.
- Evidence: the keep mix now covers `core_burst=12`, `lock_buckle=12`, `section_unlock=6`, and `dense_weave=2`; dense rows only passed after split runbreak reduced generated greedy axis/dir runs to `6/6`.
- Boundary: whole-chain reversal and random retry can create high coverage but fail trace-order rhythm for dense_weave. Treat structural split as required production grammar for dense/high-run sources.
- Follow-up: scale dense diversity by adding more split profiles or sources, but preserve the same strict trace/order gates.

## Generated-Root WBP Decision: Core Slot Preservation Before Frontier Work - 2026-06-29

- Decision: Generated-Root WBP root generation must reject authored/import contract risks before using a root as baseline. Python chain legality is not enough if `AuthoredLevelBuilder` would break external predecessor links or a head ray hits its own chain.
- Decision: planned cuttable slots must be audited twice: at reservation time and again after subsequent root growth. Final selected roots may require `cuttableSlotFinalCorePreflightPass` before they are treated as release-order/first-hit-valid.
- Evidence: t10 no-preflight slot planning produced 4 desired slots but strict audit rejected them for release-order/first-hit reasons (`blocks_pre_release_owner`, `blocks_release_owner`, `first_hit_exits_board`). Core-preflight slot planning selected 4 slots whose strict failures were only `frontier_candidate_failed`.
- Evidence: t11 `t11_coreguard_c001` preserves 4 final core-valid slots at coverage `0.313765`; corepreflight duty/ray gate reaches chain 7/reserve 6 and passes source-basin audit, while full strict still drops to 1.
- Boundary: do not call t11 a whole-board success. The next accepted work must target frontier-profile preservation/generation for planned slots; relaxing local growth gates or raising coverage alone recreates the old LocalEasy/high-coverage failure mode.

## Generated-Root WBP Decision: Treat Base Game Rules As Hard Contract Layers - 2026-06-29

- Decision: WBP candidate acceptance is three-layered: authored/import chain legality, runtime signal/clear legality, then whole-board semantic relation quality. A semantic chain contract cannot bypass base rules about 2+ cells, in-bounds indices, no overlap, cardinal adjacency, authored head->tail order, and no empty-cell chain bridging.
- Decision: `SignalTravelMode.ThroughEmpty` may be used for escape-ray reasoning only. It does not allow empty cells to connect authored chains, and WBP frontier/ray audits must not count a path chunk through empty cells as chain continuity.
- Decision: final promotion still needs import/pack gates: at least one arrow chain, at least one initial clearable chain, non-zero difficulty score, Greedy solved within budget, and official trace/process validation. These gates are separate from WBP relation-audit success.
- Evidence: the current t11 root is authored-clean and final-core-valid, but full strict drops to one frontier edge. This means root identity and release-order core are now stable enough to diagnose frontier/runtime blockers, not enough to claim final WBP success.

## Generated-Root WBP Decision: Split Local Frontier And Cross-Frontier Contracts - 2026-06-29

- Decision: do not require cross-top-root/source-basin diversity from the direct-child frontier profile. Direct-child frontier is a local parent-child gate by definition, so it naturally stays within the same top-root family.
- Decision: cross-basin/source-basin relation quality needs a separate cross-frontier profile: keep carrier-open, target-blocked/unlocked, release-impact and single-Greedy checks, but allow `--no-state-frontier-require-direct-child`.
- Evidence: t12 direct-child runs found strict local frontier edges but 0 cross-top-root edges; no-frontier all-basin probe found strong cross-top capacity; cross-frontier probe produced chainQualityDisjoint 7 and passed source-basin audit on the chain row.
- Evidence: `t12_crossfrontier_slot5` generator smoke selected 0 despite the successful gate, so the current blocker is preserving selected cross-frontier slots through root growth and reservation timing, not proving relation existence.
- Required planner shape: the whole-board cell plan must carry both contract types before chain cutting: local choke/frontier contracts for opening control, and cross-basin delay/block contracts for true source-basin difficulty.

## Generated-Root WBP Decision: Relation Existence Is Not Enough Without Support Closure - 2026-06-29

- Decision: after t13, cross-frontier state-frontier contract count should not be used as the main success gate. A candidate must also create official support closure or anti-locality improvement, otherwise relation audit may show real edges while HardStructure still collapses to LocalEasy.
- Evidence: `t13_crossfrontier_chain_guard3_cov32_c001` preserves five strict cross-frontier slots through root growth and passes source-basin audit in the external gate. The root is real generated output, authored-clean, Greedy solved, and root identity remains traceable.
- Evidence: consuming the t13 strict duty seed in V12 produces a 4-chain whole-board candidate with real planned edges (`B1/B2/CHOKE` source-frontier contracts), official trace 4/4 solved, process A, and all added chains officially touched. Difficulty attribution still reports `supportCarrierCount=0`, `supportDepth=1`, anti-locality `0.25`, and `LocalEasy`.
- Decision: the next planner/generator gate should require at least one added support carrier / closure-depth contributor and cap local/conveyor pollution before scaling to 5/6 chains or higher coverage.
- Boundary: do not fall back to generic frontier filler, capacity-only added chains, or coverage tuning. The positive route is support-closure-aware whole-board DAG/cluster selection over the preserved cross-frontier slot pool.

## Generated-Root WBP Decision: Closure Core First, Then Semantic Extensions - 2026-06-29

- Decision: for the current t13 root, the planner should preserve a closure-positive cross-frontier core before adding more chains. The proven core is the ordered edge set `3->14,18->10,16->14,0->14`; it keeps official supportDepth `2` and MediumStructure.
- Evidence: pure 5-chain state-frontier widening (`t14_crossfrontier_cov32_closurebias5_wide2_t1`) is official solved but regresses to `LocalEasy`, supportDepth `0`, and many `ReleasedLeaf` added chains. It validates whole-board cut capacity, but not difficulty.
- Evidence: fixed closure-core + one/two non-frontier semantic extensions (`t14_crossfrontier_cov32_closurecore4_ext1_t1`, `...ext2_t1`) keeps official solved/process S/tight A and supportDepth `2`; ext2 reaches coverage `0.427-0.429`, hardV3 `0.321`, antiLocality `0.435`, and planner `stateActual=1`.
- Rejected path: do not keep increasing chain count with state-frontier leaf exits around the same choke target. These chains can be legal and official-touched while lowering support closure and HardStructure.
- Required next step: add or require a real added support-carrier / far-CUD contributor. The gate should prefer candidates that raise `supportCarrierCount`, `supportClosureBestDepth`, antiLocality, and `causalCudP20`, rather than only increasing added chain count or coverage.

## Nutation Hub V3 / Maze V2 Decision: Near-Miss Is Not Production - 2026-06-29

- Decision: `NutationHubSpokeV3` is now the current Hub review pool, but it remains style-proof/near-miss and must not enter strict production packs until TraceOrderKeep gates pass.
- Evidence: Hub V3 best row has STS `0.805` and collapse `0.320`, but still fails same-axis/same-dir/dependency-local gates.
- Decision: `NutationMazePatchV2` is the active Maze probe and improves over V1, but remains non-production. It can be used for review/gap analysis only.
- Evidence: Maze V2 smoke2 produces 1 ProcessKeep where V1 produced 0, but best row still has STS `0.678`, collapse `0.460`, sameAxis `14`, sameDir `11`, and high-risk/local-collapse tags.
- Production rule: for Nutation, current strict-review source remains PeelCurve/PeelRail/LongChainSpine only; Flow review-only, Hub/Maze anti-collapse review only.

## Nutation Hub V4 Decision: Anti-Axis Needs Solve-Order Scheduling - 2026-06-29

- Decision: keep `NutationHubSpokeV4` as a Hub anti-axis prototype/style-proof lane, not as production. It should not replace Hub V3 as the best anti-collapse near-miss because V3 still has the smaller strict gap.
- Evidence: V4 smoke4 validates 4/4 solved and 2 VisualKeep rows. Best V4 row improves same-direction/dependency/collapse (`sameDir=6`, `dependencyLocal=0.545`, STS `0.809`, collapse `0.260`, directionalRisk `0.157`) but fails hard on solve-order same-axis (`sameAxisRun=20`).
- Rejected path: parent child-axis quota and stronger anti-axis scoring can lower same-axis in isolated rows, but it damages collapse/directional/local metrics and does not reliably reduce dependencySameAxis. Do not keep escalating local score weights as the main Hub strategy.
- Follow-up: next Hub/Maze improvement should introduce an explicit solve-order axis/stage scheduler or solve-time selector that scores the actual greedy solve sequence, then feed winning candidates back into packs. Local head scoring can remain a weak prior only.

## Generated-Root WBP Decision: Secondary Source Needs StateActual Support Closure - 2026-06-29

- Decision: secondary-source frontier chains are not sufficient unless the whole selected state contains at least one `stateActual` support-carrier candidate, using the same closure-depth/score threshold that attribution expects for support-carrier credit.
- Evidence: `t15_crossfrontier_cov32_secondary5_candidate5_t1` proves a secondary B1->B2 child can be chain-legal, Greedy-solved, official-touched, and process A while still staying `LocalEasy` with risk `no_added_support_carrier`. The added secondary edge had cross-basin pressure but only shallow closure.
- Tooling consequence: `Invoke-GeneratedRootWBPV12HardbaseProfile.ps1 -Mode Secondary` now passes `--min-state-actual-carrier-candidates 1` by default. Diagnostics can override this explicitly, but the default route must not emit shallow secondary candidates as progress.
- Required next step: implement/support a grammar that builds a real closure subtree under an added chain, not just an extra child edge after the closure core.
# Generated-Root WBP StateActual Support Gate - 2026-06-29

- `closureBranchMax` alone is not a sufficient support-carrier gate for Generated-Root WBP. It can count a descendant fork while the candidate carrier itself is still `singleChild` in official relation audit.
- WBP support-closure gating should prefer official-like features: direct `closureRootFanout`, root child region diversity, closure depth/score, and later anti-local/CUD floors. A candidate that is deep but root fanout 1 is a support line, not a qualified support hub.
- Coverage scaling is blocked until a non-frontier support carrier can be both directly fanout-qualified and depth/score-qualified after official trace. Shallow direct fanout may be useful diagnostically, but it should not be accepted as HardPotential evidence by itself.
- Use short Unity asset labels for WBP experiments in `.worktrees/sgp-rhythm-lab`; long labels can hit Windows `.asset.meta` path length failures near 260 chars. Reports may keep descriptive prefixes, but asset directory/level ids should stay compact.

# Generated-Root WBP Official Support Preservation - 2026-06-29

- `t16br_t9_7hub` establishes the current positive route: a closure-positive 4-chain cross-frontier core plus three semantic non-frontier cluster chains can produce an official `SupportCrossCarrier` with direct fanout `2`, child region count `2`, closure depth `3`, and official support score about `0.826`.
- The next scaling gate is support preservation, not raw exact-8 capacity. Exact-8 loose secondary can improve coverage and lower choices while destroying the official support hub, which returns the level to `LocalEasy`.
- The planner `stateActual` proxy can still over-credit false hubs such as `O00217`; official relation audit is currently the authority for whether a selected support carrier is real.
- A crude B2 strength floor is not accepted as a general fix. It can remove a false hub but also remove viable geometry, so any future filter must be structural: preserve the official-positive support-core relationship or approximate relation-audit semantics more directly.
- Post-hub secondary generation is allowed only as a semantic extension from an already planned support core, and must prove it does not reduce official supportClosureQualifiedHubCount/support depth. Loose secondary leaf expansion is a negative baseline.
- Future exact-8+ and coverage-scaling runs should either seed from official-positive support cores (`t9 c003/c004` style) or add an in-planner support-preservation gate before candidate promotion.

# Generated-Root WBP Chain-Plan Seed Rehydration - 2026-06-29

- Decision: chain-plan seed state loading must not trust raw `optionId` alone. Option ids can drift between planner runs, especially `SFD*`/`SSF*` rows generated from duty/secondary pools.
- Required behavior: match by stable identity first where possible; if an id exists but the row identity differs, count it as `option_id_identity_mismatch` and fall back to identity match or row reconstruction. If no current option matches, reconstruct from row-level chain cells and semantic fields, then re-run selected-state validation.
- Evidence: before this guard, t20 exact10 seed reuse failed with `cell_overlap` because current option ids pointed at different cells/slots. After the fix, t21/t22/t23 seed states accept cleanly with row reconstruction and still pass Greedy/legality validation before candidate output.
- Boundary: row reconstruction is only a rehydration bridge for an already selected whole-board chain plan. It is not permission to skip authored chain legality, first-hit/ray checks, Greedy solved, official trace, or relation/difficulty audit.

# Generated-Root WBP Secondary Boundary - 2026-06-29

- Decision: `STATE_FRONTIER_SECONDARY_B1_TO_B1` should be treated as a weak/local diagnostic target, not a scaling baseline. It can raise coverage and preserve solved status while adding local penalty and no support closure.
- Evidence: `t16br_t22_seed10_sec1` exact10 is solved/process A and coverage `0.4676-0.4696`, but attribution still finds only one real added `SupportCrossCarrier`; the B1->B1 secondary is `CrossBasinPressureWithLocalPenalty` with shallow/no carrier closure.
- Decision: the better immediate secondary target is nonlocal B1->CHOKE/B3, but even that is not enough by itself. `t16br_t23_seed10_no_b1sec` improves process to S, same-axis/same-dir run to 4, hardV21 to `0.724`, and crossCrit to 6, yet remains `MediumStructure` with one support carrier.
- Evidence: exact11 from t23 excluding B1 (`t16br_t24_seed11_no_b1sec`) produces 0 candidates; rejection is dominated by occupied head/second cells, no path, duplicate target paths, and targets no longer blocked. Current t13/t23 corridor capacity is exhausted around exact10 for nonlocal secondary chains.
- Required next step: change the whole-board root/corridor plan or support-closure grammar so a second real support carrier can be cut. Do not recover coverage by re-allowing B1 local leaves, local filler, long bands, or generic frontier chains.

# Generated-Root WBP Secondary Corridors Must Be Co-Planned - 2026-06-29

- Decision: exact11 secondary expansion failure should be treated as a corridor co-planning/root-and-seed-selection problem, not as a need to add one more local secondary chain after exact10.
- Evidence: `t16br_t27_seed11_no_b1sec_demand_reason` shows 0 candidates with demand dominated by `child_no_path`, occupied child head/second cells, and left-edge selected/root conflicts. Target-ray failures exist but are not the main mass of the failure.
- Decision: demand diagnostics with per-reason row quotas are now accepted as a useful bridge from planner rejection to root/seed planning. They expose low-count target-ray blockers without hiding the dominant no-path/head conflicts.
- Decision: post-hoc duty-zone mutation is too late once the whole-board cell plan is already saturated. `secondary_duty_cells=0` in t27 is a design signal: secondary corridors need reserved roles before exact10 seed closure, not after selected chains occupy the board.
- Negative evidence: scratch demand-reserved roots (`t28_secondary_reserved_root_smoke`) can be generated and Greedy-solved but lose strict cross-basin capacity. Preserve-nonreserve roots (`t29_secondary_preserve_reserved_root_smoke`) improve to a partial `bestChainDisjoint=4` on c001, but still fail capacity/diversity and are too slow/weak as a baseline.
- Required next step: integrate secondary corridor demand into either source-basin-first root generation or chain-plan seed scoring, while preserving the existing official-positive support hub. Empty/reserved corridor cells may shape signal rays and first-hit geometry, but must never be counted as authored chain continuity.

# NutationPeelPatch Decision: Patch Chain Is Viable Only As Near-Miss For Now - 2026-06-29

- Decision: keep `NutationPeelPatchV1` as `Nutation / Peel / patch_chain` near-miss/prototype, not as strict production. It should appear in the style matrix as `strict_near_miss`, but must not enter `NutationStyleMatrixStrictReviewPack` until it has real TraceOrderKeep rows.
- Evidence: smoke6 gives 4/4 official solved and 4/4 STS pass with non-collapse flow mix (`flow_spread=2;region_alternating_flow=2`), avg STS `0.843`, avg collapse `0.176`, max local run `6`, and best row `core_patch` only misses strict by `maxChoices=11`.
- Boundary: all smoke6 rows still rank `Reject` because choices are too broad (`maxChoices=11-16`), so this is not ready for batch production.
- Rejected path: smoke5 tightened opening/edge-head gates and scoring; it produced one `ProcessKeep` with `maxChoices=10`, but flow regressed to `local_collapse`, STS pass fell to 2/4, and production keep stayed 0. Do not solve PeelPatch by simply crushing edge heads/openers.
- Follow-up: if PeelPatch is revisited, use candidate-pool expansion plus trace-order selection or a mild solve-time choice-shaping layer. Keep patch-chain identity and non-collapse flow above local max-choice tuning.

# Generated-Root WBP Root/Basin Re-Layout Decision - 2026-06-29

- Decision: the t13 exact11 boundary is now treated as a root/source-basin geometry collapse, not a secondary-source enumeration or demand-weight tuning problem.
- Evidence: demand-aware scoring changes t30/t31 ordering but exact11 remains `0`; hard demand cap `<=700` removes all exact10 candidates; no-CHOKE exact10 also has `0` candidates; widened secondary search and `allow-all-empty` still produce only the same left-edge `SSF10150x` paths through `0,18`.
- Decision: secondary-source demand should remain as a scoring/audit signal, but the next implementation route is a new generated-root/root-basin pool that co-plans secondary corridors before seed closure.
- Evidence: t29 demand-fit root `c004` keeps t27 demand overlap at `0` through exact5 and exact7 after lowering the frontier-count gate, proving root/basin direction is real. It still stalls at exact8 and lacks t13 depth, so it is a probe, not a baseline.
- Boundary: do not solve this by re-allowing weak B1 local leaves, long chains/bands, post-hoc corridor mutation, or more secondary enumeration caps. The target is a root generator/selector with both t13 cross-frontier support capacity and t29 demand openness.

# NutationHubRail Decision: Rail Language Proof, Not Production - 2026-06-29

- Decision: keep `.worktrees/nutation-peel` `NutationHubRailV1` as `Nutation / Hub / rail_chain` style/language proof, not as production or strict review. It should appear in the style matrix as `style_proof_only`.
- Evidence: smoke2 has 4/4 official solved, 4/4 `hub_spoke`, 4/4 `rail_chain`, source straightness avg `0.848`, and source status Rails `4-19`, proving that Hub topology can be rendered with a clear rail-chain language.
- Negative evidence: smoke2 has 0 production keep rows, all rows `high_risk`, stripeRisk `0.446-0.740`, and many same-axis/same-dir blockers. The best `center_rail` is strong on STS (`0.860` / collapse `0.177`) but fails the visual hard stripe gate.
- Implementation boundary: the HubRail-specific early segment extension override is allowed because it is lane-scoped. Do not apply it to HubSpoke/patch lanes, where early bend pressure is part of the anti-sweep control.
- Follow-up: if HubRail must become production, add stripe/axis-run control or solve-order stage scheduling after style proof; do not simply weaken visual gates to admit straight sweeps.

# Generated-Root WBP Decision: Demand-Fit Roots Must Also Be Official-Hard - 2026-06-29

- Decision: demand-fit or Greedy-solved roots are not acceptable WBP baselines unless the root identity itself is official-hard enough: prefer HardPotential/TrueHardCandidate, supportDepth around 4, and no local-patch collapse before extension.
- Evidence: t49 lightpool roots are all `LocalEasy` after official trace. c002 demand0 exact5 reaches coverage about `0.385` and process A, while c003 demand0 exact6 preserves demand to coverage about `0.328`; both stay LocalEasy. This proves demand preservation and process tier are necessary but not sufficient.
- Decision: root selection must be two-axis before chain cutting: hard root identity/support closure plus reserved source-basin/secondary-demand/choke capacity. A root that is hard but consumes future corridors, or demand-open but LocalEasy, is only diagnostic.
- Evidence: c001 can grow but consumes future secondary demand cells and still traces LocalEasy; c002/c003 preserve demand better but lack hard support. Existing hard generated roots around coverage `0.616` and V12 hard-root extension to `0.655` show the better axis, but pure B1 frontier edge patterns are too narrow for 0.95 scaling.
- Required planner shape: use official-trace root-pool selection plus reservation/demand-fit and strict duty/ray/source-basin audit before WBP, then cut multi-basin B2/B3/CHOKE/support-preserving contracts. Do not continue with lightpool tuning, weak B1 leaves, coverage-only exact-N growth, long bands/slabs, or LocalEasy process-A proofs as success.
- Follow-up evidence: t50 hard-root+demand-fit smoke selected 80 official-hard roots and ranked top12 by t27 demand. The first completed strict root `root154_section_sched_v2_r5_c062` reached bestChainDisjoint `7` with cross/choke/support proxy edges, but failed semantic capacity `8` and root footprint gates. Therefore demand-fit ranking must be combined with strict relation-quality pre-rank or a wider coverage/root-footprint window before WBP chain cutting.

# Generated-Root WBP Decision: Short Semantic Exact8 Is A Mechanism Proof, Not A Baseline - 2026-06-29

- Decision: lowering WBP state-frontier `min-chain-length` to 3 is accepted for semantic duty chains because the game/import rule only requires chain length at least 2, and the user explicitly wants many short/medium semantic chains rather than long bands.
- Evidence: `t52_shortsemantic_strict_root154_section_r5_c062.csv` reaches `chainQualityDisjoint=8` on a real official-hard generated root using short semantic chains, with cross-basin/choke/delay/support-proxy relation mix.
- Decision: duty-probe disjoint capacity is not equivalent to final whole-board cut capacity. Final acceptance must remain slot-aware and must pass chain legality, first-hit, Greedy, official trace, and relation/difficulty audit.
- Evidence: forced theoretical best edge pattern `t52c062_e8q` has every edge represented but fails with `disjoint_lookahead_dead` once slot-aware chain cells are considered. The valid exact8 is `t52c062_e8d`, not the edge-level best set.
- Decision: `t52c062_e8d` proves the new route can cut 8 short/medium semantic chains around a preserved generated root, but it is not a hard baseline because official HardStructure remains `LocalEasy` (`hardV3 0.356`, `antiLocal 0.389`, local penalty dense).
- Negative evidence: `t52c062_e9x` adds a non-frontier `B1_BLOCKS_B2` extension and remains solved, but worsens hardV3/antiLocal and adds local penalty; `t52c062_e10y` cannot reach exact10. Therefore ordinary extension/generic coverage growth repeats the sandwich failure mode.
- Required next step: add anti-local and official-like support-carrier preservation to the whole-board plan before coverage scaling, or generate/select a new root/basin plan with lower local penalty and at least two real support carriers. Do not promote process-S/A LocalEasy, theoretical disjoint summaries, or generic extension chains as success.

# Generated-Root WBP Decision: Gap-Aware Nonlocal Capacity Must Be A Pre-Cut Gate - 2026-06-29

- Decision: exact8 short semantic capacity is not sufficient unless the selected edge pool remains viable after removing stepGap=1 source-owner/local releases. Root selection and whole-board cell planning must include a gap-aware/nonlocal relation-capacity gate before chain cutting.
- Evidence: `t53c016_e8d` improves over `t52c062_e8d` (`MediumStructure 0.476` vs `LocalEasy 0.356`, coverage `0.6518219` vs `0.6275304`) but still fails A/HardPotential because local penalty remains `4` and antiLocal is only `0.474`.
- Evidence: hard anti-local exact8 gates (`max-planned-local-penalty-count=0`) return 0 candidates for both `root154_section_sched_v2_r5_c062` and `root154_core_sched0564_v1_r1_c016`. This indicates the current slot-aware exact8 path depends on local edges, not just poor scoring.
- Evidence: when both `state-frontier-min-step-gap` and `state-frontier-source-min-step-gap` are raised to `2`, the best strict shortsemantic capacity among tested exact8 roots drops to `6`; at `3`, it drops to `4`. The missing capacity corresponds to near-immediate source-owner releases such as `14->15` and `0->4`.
- Decision: do not solve the 0.95/A target by adding generic exact9+ leaves, sandwich difficulty chains, long bands/slabs, or post-hoc local filler. These increase coverage/solvability but either fail exact capacity or reduce official antiLocal/hardV3.
- Required planner shape: add or select for gap-aware relation capacity, official-like local-edge budget, parent fanout/child-region diversity, and at least two real support carriers before final WBP candidate promotion. Official relation audit remains the authority until planner proxies match it.

# NutationMazeRail Decision: Folded Rail Proof, Not Production - 2026-06-29

- Decision: keep `NutationMazeRailV1` as `Nutation / Maze / rail_chain` folded-rail style/language proof. It should appear in the style matrix as `style_proof_only`, not as production or strict review.
- Evidence: smoke4 has 3/3 traceable rows solved, 3/3 `constraint_maze`, 3/3 `rail_chain`, source straightness avg `0.434`, and stripe avg `0.031`. This proves Maze topology can carry a rail-like chain language without falling back to straight stripe sweep.
- Negative evidence: production keep remains 0. The best trace-shape row `core_rail_maze` has STS `0.887`, collapse `0.206`, sameAxis `8`, sameDir `6`, and stripeRisk `0.023`, but processTier is `Drop`; it must not be admitted by weakening process gates.
- Boundary: smoke1/smoke2 are too straight and stripe-prone, while smoke3 is too bent to remain rail-like. smoke4 is the current balanced proof and should be the matrix input unless a better solved + non-Drop row replaces it.

# NutationHubCurve Decision: Curve Language Proof, Not Production - 2026-06-29

- Decision: keep `NutationHubCurveV1` as `Nutation / Hub / curve_chain` style/language proof. It fills the Hub chain-language matrix gap between patch/spoke and rail, but it is not a strict or production lane.
- Evidence: smoke1 has 3/3 official solved rows, 3/3 `hub_spoke`, 3/3 `curve_chain`/`hub_curve_chain`, and source straightness avg `0.227`, clearly separated from HubRail's straight rail language.
- Negative evidence: production keep remains 0 and all rows are `local_collapse/high_risk`. Best `dual_curve` reaches STS `0.823` / collapse `0.262` but still fails directional/local/near-outer/dependency-local gates.
- Boundary: do not productionize HubCurve by lowering strict gates or by adding stronger local head penalties. The next useful Hub/Maze work is solve-time stage/axis/region scheduling; HubCurve scoring can remain a weak chain-language prior only.
- Follow-up: Maze/Hub productionization now needs an explicit solve-time stage/axis scheduler or trace-aware anti-collapse layer. Further local head/segment scoring alone is unlikely to make the style both distinct and strict-safe.

# NutationMazeCurve Decision: Low-Yield Curve Proof, Not Production - 2026-06-29

- Decision: keep `NutationMazeCurveV1` as `Nutation / Maze / curve_chain` low-yield style/language proof. It fills the Maze curve-chain matrix cell but is not a bulk candidate or production lane.
- Evidence: smoke1 produced 1 traceable solved row from 4 specs; that row is correctly tagged `constraint_maze` + `curve_chain`/`maze_curve_chain`, with rank `VisualKeep` and 0 production keep.
- Negative evidence: source coverage is only `0.892`, anti-collapse score is `77.836`, and main gaps are same-axis, STS low by about `0.030`, collapse over by about `0.006`, plus `flow_local_collapse`.
- Boundary: further MazeCurve improvement should not be more local head scoring. It needs the same solve-time axis/region scheduling layer as Hub/Maze productionization, or a separate higher-throughput curve-source recipe.

# NutationLongChainCurve Decision: Add As Strict Review Lane - 2026-06-29

- Decision: keep `NutationLongChainCurveV1` as a strict-review `Nutation / LongChain / curve_chain` lane alongside `NutationLongChainSpineV1`.
- Evidence: smoke1 second pass has 4/4 solved, 4/4 STS pass, 3 TraceOrderKeep rows, and matrix readiness `strict_review_ready`. It increases current strict keep rows from 6 to 9 without touching PSG or the existing spine lane.
- Language boundary: the first pass was too snake-like (`straightness 0.11-0.14`), so the profile now rewards moderate straightness and penalizes `straightness < 0.22`. The accepted pass lands at source straightness `0.343-0.389`, avgChain `11.55-12.56`, maxChain `21-24`, which is curve-longchain rather than rail or spine.
- Follow-up: if producing review packs, include LongChainCurve as a separate chain-language lane. Do not merge it into `spine_chain`, and do not weaken Hub/Maze gates based on LongChainCurve success because it uses the direct peel route, not gate-aware owner-hit topology.

# NutationLongChainRail Decision: Add As Strict Review Lane - 2026-06-29

- Decision: keep `NutationLongChainRailV1` as a strict-review `Nutation / LongChain / rail_chain` lane. It is a sibling of LongChainSpine and LongChainCurve, not a replacement.
- Evidence: smoke2 has 4/4 official solved, 4/4 visualPass, 3 TraceOrderKeep rows, 3 production keep rows, and matrix readiness `strict_review_ready`.
- Language boundary: smoke1 proved that high straightness (`0.700-0.772`) becomes stripe/high-risk. The accepted profile raises chain count and softens same-direction inertia, landing at source straightness `0.408-0.490`, avgChain `12.45-13.43`, maxChain `22-23`, and keep-row stripeRisk `0.135-0.191`.
- Follow-up: LongChainRail can be included in review/production candidate packs as its own chain-language lane. Do not loosen stripe gates to make it "more rail"; if stronger straight visual language is needed, use a new lane or add solve-time stripe/axis control first.

# Generated-Root WBP Decision: Runtime Rule Gate And Slot-State Capacity - 2026-06-29

- Decision: the user's base game rules are now WBP hard gates, not background docs. A final WBP candidate must separately pass authored/import legality, runtime actual-chain round-trip, first-hit/signal-ray legality under `ThroughEmpty`, self-ray/loop risk, Greedy, official trace, and relation/difficulty audit.
- Decision: each planned semantic chain should carry an activation/click context and actual-chain identity, not just an authored `indices` path. The game clears the actual adjacent chain containing the clicked arrow, while authored `indices` are `head -> tail` and then reversed into runtime direction; WBP must not let authored ids hide actual chain merge/split.
- Decision: intentional empty/corridor cells are first-class cell-plan roles because signal can travel through empty cells, but they must never be counted as chain body continuity. Any later arrow placed into an empty corridor can change first-hit ownership and should be treated as a semantic conflict.
- Evidence: t56 source-basin generator can create low-coverage roots with gap2 edge-level `strictCuttableProxy=8`, but WBP exact8 still fails at slot-state depth 8. The blocker is not raw edge count; it is simultaneous non-overlapping chain/signal/ray placement under actual first-hit semantics.
- Evidence: t56 c002 exact7 is authored/Greedy/official solved and process A, but relation audit reports `LocalEasy 0.168`, antiLocal `0.286`, support depth `2`, and difficulty attribution says added support carrier count `0`. This validates the user's suspicion that late outer-exit/weak dependency chains get discounted.
- Required next step: move slot-state reservation and official-like support-carrier/anti-local gates into source-basin root generation or pre-cut whole-board cell planning. Do not accept edge-level cap8, process A, or solved LocalEasy as evidence toward the 0.95/A target.

# Generated-Root WBP Decision: Reserve-Disjoint Capacity Must Gate Rootgen - 2026-06-29

- Decision: strict cuttable root selection must distinguish chain-body disjoint capacity from reserve-disjoint capacity. `selectedChainDisjoint` / `chainQualityDisjoint` are not enough; `selectedReserveDisjoint` / `reserveQualityDisjoint` must be available as hard gates and ranking signals.
- Evidence: t56 roots reached gap2 chain-level cap8, but reserve capacity collapsed to `4` and `5`: `t56_sbrg_gap2_cap8_smoke1_c001` is `selectedChain=8 / reserveQuality=4`, and `c002` is `selectedChain=8 / reserveQuality=5`.
- Evidence: the t57 chain-only slot8 control selected `t57_sbrg_gap2_slot8_chainset2_c001` with `cuttableSlotPlanned=8`, but its reserve-quality capacity was still only `5`, matching the WBP exact8 failure modes (`cell_overlap`, `same_edge`, `slot_offset_mismatch`, first-hit conflicts).
- Implementation decision: `Build-GeneratedRootWBPV12SourceBasinRootGeneratorV1.py` now exposes `--min-strict-cuttable-proxy-reserve-disjoint` and `--min-strict-cuttable-proxy-reserve-quality-disjoint`, rewards reserve-disjoint capacity in rank scoring, penalizes chain-vs-reserve gaps, and logs reserve fields in growth reports and summaries.
- Boundary: do not use chain-only slot reservation or edge-level cap8 as baseline evidence. Next viable rootgen scans must require reserve-aware cap8 or an equivalent slot-state preflight, then only afterwards run WBP chain cutting.

# Generated-Root WBP Decision: Reserve-First Audit Replaces Chain-Best Audit - 2026-06-29

- Decision: `strictCuttableProxyBestEdges`, `crossTopRootEdges`, and activation dominance must be derived from the reserve-best edge set, not the chain-best edge set. Chain-best edges can still be logged for diagnosis, but they are not final rootgen selection evidence.
- Evidence: historical t56/t57 records reached `chainQuality=8` while reserve-quality was only `4/5`; WBP exact8 then failed from slot-state overlap and first-hit conflicts. This is exactly the false positive caused by treating chain-body disjoint capacity as if signal/ray reserve capacity were also disjoint.
- Implementation decision: the source-basin root generator now emits `strictCuttableProxyChainBestEdges` and `strictCuttableProxyReserveBestEdges`, ranks best modes reserve-first, and makes `bestEdges` point at the reserve-best edge set.
- Evidence: current-code t59 scan (`t59_sbrg_gap2_reservefirst_scan3`) selected 3 mechanics-clean generated roots at coverage `0.273-0.312`, but reserve-first strict cuttable capacity was only `2/2`, `0/0`, and `0/0`. The best row had edges `16->15,15->1`, `crossTopRootEdges=0`, and activation dominance `1.000`, so it is not a viable WBP baseline.
- Decision: the next generator change should move from final-only reserve auditing to pre-planned whole-board slot/role generation: release/blocker/choke/corridor cells must be assigned before chain cutting, and root growth must preserve future reserve slots. Do not continue by widening exact chain count, tuning coverage, or accepting old chain-best cap8 roots.

# Generated-Root WBP Decision: Light Role Reserve Is Necessary But Not Sufficient - 2026-06-29

- Decision: source-basin rootgen may use a cheap light-role reserve plan before chain cutting. This plan can reserve target-ray and anchor cells for intended cross-basin/choke/delay slots and steer root growth away from them. It is a cell-plan primitive, not final proof that chains are cuttable.
- Implementation decision: `Build-GeneratedRootWBPV12SourceBasinRootGeneratorV1.py` now supports `--enable-light-role-reserve`, replan-until-target controls, ray/anchor/reserve overlap modes, consumed-cell gates, and activation-top diversity (`--light-role-reserve-max-per-activation-top`). Use ray-hard / anchor-soft protection to avoid both no protection and overblocking.
- Evidence: t62 with full reserve hard protection planned 8 cross/choke slots and consumed 0 cells, but protected 77 cells and stalled at coverage `0.2226721`; this is too broad and repeats the “reserve everything, grow nothing” failure mode.
- Evidence: t65/t67 with ray-hard + anchor-soft + activation-top diversity planned real whole-board role slots: t67 selected rows have lightRole `8/8/0` and `7/7/0`, activationTopRoots `4/3`, activationDominance `0.375/0.429`, and authored/Greedy clean roots at coverage `0.273-0.281`.
- Evidence: t67 improves cross-frontier strictCuttable over t59 only when `--no-strict-cuttable-proxy-state-frontier-require-direct-child` is used: t59 reserveQuality was `2,0,0`; t67 reaches selectedChain `4/10`, selectedReserve `3`, reserveQuality `3`, crossTop `3`. Direct-child t65 on the same shape remains reserveQuality `1`.
- Boundary: t67 is not a WBP baseline and must not enter exact chain cutting as success. The remaining gap is reserve-disjoint semantic chain geometry: light-role cells are preserved, but they are not yet converted into 8 non-overlapping actual candidate chains with valid first-hit/owner semantics.
- Required next step: make the planned role edges feed the root growth / slot candidate search directly, either through a fast role-edge candidate-slot planner or by scoring root growth for future reserve-disjoint chain-body availability. Do not go back to chain-best cap8, local direct-child-only frontier, or coverage-only scans.

# Generated-Root WBP Decision: Planned Role Edges Need Slot-Fit Audit - 2026-06-29

- Decision: a whole-board role plan is not accepted just because it reserves target-ray/anchor cells. Each planned role edge must be audited against actual candidate-chain slot availability using the same strict duty/ray `scan_edge` / reserve-disjoint / preflight semantics as the final cuttable proxy.
- Implementation decision: `Build-GeneratedRootWBPV12SourceBasinRootGeneratorV1.py` now preserves structured light-role `edgeRows` and exposes optional `--enable-light-role-slot-audit` plus `lightRoleSlot*` CSV/summary fields. These fields audit only the planned role edges, while `strictCuttableProxy*` still audits the generic edge pool.
- Evidence: t69 relaxed seed560120 has lightRole `8/8/1`, but planned-edge slot audit is only `selectedReserve=1 / reserveQuality=1 / crossTop=1`; generic strict cuttable on the same root is `selectedReserve=3 / reserveQuality=3`. Planned cells are being kept, but the selected planned edges are mostly not slot-fit.
- Evidence: t68 scan3 growth log shows the same pattern across attempts: planned-edge reserveQuality is `1`, `3`, and `1` for seeds 560120-560122, while light-role planned counts are `8`, `6`, and `7`. This confirms the missing step is slot-fit-aware role selection, not more role-cell reservation.
- Required next step: change light-role planning from cell-reserve-only ranking to slot-fit-aware edge selection or a two-stage planner that prefilters planned edges by candidate-chain availability and first-hit preflight reasons. Do not promote planned-edge strings or preserved empty cells as relation proof.

# Generated-Root WBP Decision: Slot-Fit Selection Is A Diagnostic Lever, Not Enough Capacity - 2026-06-29

- Decision: slot-fit-aware light-role selection is adopted as a diagnostic/ranking lever, but it is not by itself a capacity solution. It can pick better planned edges among the current root geometry; it cannot create missing candidate-chain slots.
- Implementation decision: `Build-GeneratedRootWBPV12SourceBasinRootGeneratorV1.py` now supports `--enable-light-role-slot-fit-selection`, emits `lightRoleReserveSlotFit*` fields, and can reward or require planned edges with strict duty/ray candidate slots. The default safe use is selection scoring plus audit, not hard-protecting slot reserve cells during every growth step.
- Evidence: t70 improves seed560120 planned-edge slot audit from t69 `1/1/1` to `3/3/3` while keeping coverage `0.2874494`, authored/Greedy clean root output, and lightRole `8/8/1`. This proves slot-fit-aware selection fixed part of the planned-edge mismatch.
- Negative evidence: t74 removing activation-top cap still stays at `3/3/3`; t73 early slot-reserve packing drops lightRole to `2/2/0`; t71/t72 slot-reserve hard inclusion/packing timed out. Therefore the current bottleneck is not top-root cap or selected edge text, but actual slot supply and growth-time geometry consumption.
- Required next step: add a slot-fit edge supply audit and feed it back into root growth/cell planning. The planner should score or preserve the concrete prerequisites for more slot-fit edges: head/second availability, signal corridor cells, target-ray reachability, first-hit owner correctness, release/pre-release nonblocking, and support-carrier/anti-local quality. Do not treat `lightRoleReservePlanned=8` or process-A Greedy-solved roots as progress unless reserveQuality and relation/difficulty gates move with them.
- Implementation decision: the first supply audit output is `--light-role-slot-fit-supply-csv`. It is deliberately read-only and selected-root scoped; it should be used to design the next growth scoring/gating change, not as a new acceptance metric. Temporary smoke artifacts for this exporter are not retained as route evidence.

# Campaign500 Long-Chain Decision: Gate Continuous Outer-Exit Heads - 2026-06-29

- Decision: continuous outward-facing edge exits are a generator quality failure for Campaign500 long-chain packs, not a base gameplay requirement. The base rule only needs clicked chains/signals to eventually escape the board.
- Evidence: V2 already capped same-chain edge straight runs but still produced adjacent outward-exit heads from different chains on the perimeter, matching the user's screenshot.
- Implementation decision: V3 tracks `outerExitHeadCount`, `outerExitRunMax`, and `outerExitSideMax` from authored boundary chain heads whose direction points directly out of board; generation accepts normal rows at run cap `2` and challenge rows at cap `3`, while the actual V3 selected pool reached `outerExitRunMax=1` for all 43 selected rows.
- Boundary: do not ban all edge arrows. A small number of edge exits is acceptable and often useful for readability; the reject target is repeated perimeter escape heads or one side becoming an exit rail. Continue to preserve long-chain main visual plus mid/short support chains.

# Nutation Hub/Maze Decision: Controlled Review Covers Chain Languages, Not Production - 2026-06-29

- Decision: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubMazeAntiCollapseReviewPack.asset` should select Hub/Maze rows by `Style x ChainLanguage` coverage for visual comparison, not only by global score. Current default keeps Hub curve/rail/patch and Maze curve/rail/patch, one row each.
- Evidence: current anti-collapse review ranks 33 rows from 9 joined CSVs and refreshes a 6-ref pack. Best rows are HubRail `109.139`, HubSpokeV3 patch `102.406`, HubCurve `91.497`, MazeRail `94.748`, MazeCurve `77.836`, and MazePatchV2 `49.725`.
- Boundary: this makes style/language differences easier to inspect, but it is still style-proof/gap review. Do not mix this pack into strict Nutation production or treat single-gap MazeRail `tier_drop` as solved.
- Follow-up: productionization still requires a solve-time stage/axis/region scheduler or trace-aware selector that changes the actual solve order, then feeds winners back into normal trace gates.

# Campaign500 PSG Normal Decision: Abandon Interval100 Review Batch - 2026-06-29

- Decision: abandon the current D-worktree `Campaign500PSGNormal_psg100i_s001_e050_v004_fb1_Keep100Pack` as a production candidate set after manual review. It is a diagnostic/review artifact only.
- Evidence: although the run achieved 100/100 interval coverage and all selected rows were trace-joined, the manual feel was poor and the trace risk split was overwhelmingly high-risk (`97/100` high_risk).
- Boundary: do not spend more effort scaling this worktree or this interval100 selection recipe. Future PSG normal batching should restart after improving generator/selector capability, especially against same-axis/same-dir chain collapse, high choice curves, and broad high-risk fallback dependence.

# Generated-Root WBP Decision: Slot-Fit Blockers Need Staged Feedback - 2026-06-29

- Decision: planned-edge slot-fit blocker data should feed root growth through a staged/offline selector or cheap precomputed pressure map, not by running full strict slot-fit scans inside every full rootgen loop.
- Evidence: t76 root-row supply audit on t70/t74 shows only `3/8` planned light-role edges have slot-fit candidates; failed edges are dominated by first-hit owner mismatch, release/pre-release blocking, frontier target not unlocking after carrier clear, and corridor blockers from owner12/owner8/owner13/owner3.
- Implementation decision: `Export-GeneratedRootWBPV12LightRoleSlotFitSupplyV1.py` is the canonical read-only existing-root supply audit for this phase. `Build-GeneratedRootWBPV12SourceBasinRootGeneratorV1.py` has a default-off `--light-role-slot-fit-blocker-penalty` switch and blocker logging, but this switch is not accepted as a default route.
- Negative evidence: t77 direct full rootgen smoke with blocker penalty timed out after only writing a partial basin plan; the partial was deleted. This means the feedback loop must be lighter or staged before it can become a production root-growth mechanism.
- Required next step: build a lightweight root-row/supply selector or stage-split growth experiment that uses t76 hot blocker owners/cells to choose or shape generated roots before WBP exact8+ and coverage scaling. Do not rerun full rootgen with expensive blocker scans as the primary loop.

# Difficulty Feel Decision: Process Constraints Beat Visual Bait - 2026-06-29

- Decision: visible blocked heads, bait exits, or sparse “looks locked” layouts are not accepted as a hard-level route unless the real solve trace also shows tight choices, meaningful dependency unlocks, low local/near-outer sweep runs, and cross-region rhythm.
- Evidence: the `Visible Key / bait-head` prototype was manually rejected as not meaningful and not difficult despite having many near blocked heads. Its weakness was that the player did not need a new solving strategy.
- Calibration reference update: `SGPRhythmLab_PressureReadStageLockTrueHardDependencyV7Curated5Pack` remains useful as a process-metric reference (5/5 solved, process `S`, openers `2-5`, avg choices `2.59-3.63`, max choices `4-6`), but user review showed it is not a sufficient feel target because long chains can clear too much in one click.
- Boundary: this decision does not mean every future hard route must be StageLock. It means any SGP/PSG/Nutation hard experiment should first prove comparable process pressure before relying on visual chain language.

# Difficulty Feel Decision: Long-Chain Low-Choice Is A False Positive - 2026-06-29

- Decision: low `avgChoices`, low `maxChoices`, or long low-choice runs are not accepted as hard evidence when the board contains chains that clear a large amount in one click. Future hard-feel review must gate `maxChain` and preferably `singleClearShare` before trusting choice metrics.
- Evidence: user reviewed the StageLock/TrueHardDependency calibration pack and reported that it still did not feel hard because long chains disappeared immediately; the low process metrics were misleading.
- Current acceptance direction: prefer short/mid carrier locks with many small clears, e.g. `maxChain <= 15-18` for calibration, low `singleClearShare`, `avgChoices/maxChoices` still bounded, and low local/near-outer sweep runs. LongChain lanes remain visual/style lanes, not difficulty proof by themselves.
- Boundary: this does not ban all long or readable chains from future levels. It rejects using long-chain one-click clears as the reason a level appears tight or difficult.

# Generated-Root WBP Decision: Slot-Fit Feedback Needs Joint Edge-Supply Selection - 2026-06-29

- Decision: slot-fit blocker feedback may steer generated-root growth, but it must be paired with planned-edge supply and reserve-packing selection. Single-axis fixes such as blocker penalty, static hot-cell avoidance, or activation-top caps are diagnostic tools only.
- Evidence: t78 pressure maps from t70/t74 show the old planned-edge bottleneck is highly structured, not random: failed role edges are dominated by owner12/owner8 corridor and first-hit/release timing, with only `3/8` planned edges having slot-fit candidates.
- Positive evidence: t79 used a precomputed pressure map as a cheap growth signal and moved the root away from the old owner12/8 hotspot. Post-hoc supply improved to `5/8` edges with slot-fit candidates and `66` candidates, while preserving a generated, Greedy-solved root at coverage `0.3016194`.
- Negative evidence: t79 still collapsed reserve packing to `2/2` and activation diversity to `2` top roots; t80 fixed activation diversity (`8/8/9`, activationTopRoots `4`, dominance `0.250`) but killed slot-fit supply entirely (`0/8`). This proves activation diversity without slot geometry is a false positive.
- Implementation decision: `Build-GeneratedRootWBPV12SourceBasinRootGeneratorV1.py` can now read a static blocker cell map with `--light-role-slot-fit-blocker-map-csv`, but default behavior remains unchanged. Use it for staged experiments, not as acceptance.
- Required next step: build a joint planned-edge selector or staged planner that scores role edges by candidate availability, reserve-disjoint compatibility, activation-top spread, and first-hit/release preflight before root growth consumes the board. Do not continue by only raising coverage, hard-protecting reserve cells, or adding caps after a role plan is already chosen.

# Difficulty Feel Decision: LongChain Low Choice Is Not General Hardness Evidence - 2026-06-29

- Decision: LongChain low-choice curves must not be used as general high-difficulty evidence. A long chain naturally compresses later choices because many cells are locked into one chain body, so low `lowChoiceRunMax` or low avg choices can be a length artifact.
- Boundary: LongChain rows may still be used for LongChain-specific review, visual-chain language, Campaign long-chain slots, or as an explicit control lane, but they cannot validate a general SGP/PSG/Nutation difficulty route by themselves.
- Correction: `TightChoiceBottleneckV1` now excludes `maze_long_chain` by default and only includes it with explicit `-IncludeLongChain`; memory/index notes that described 5 clean LongChain rows as the clean evidence are superseded by this decision.
- Current evidence: after excluding LongChain, the existing traced pool can still assemble a 10-row diagnostic pack, but it depends on Hub/Maze probes and a PSG control row. Only about 5 rows are relatively strict-ish non-LongChain candidates, so this is not a mature general high-difficulty production lane.
- Follow-up: the next real general-hard route should create short/mid-chain bottlenecks through solve-time dependency/stage/axis/region scheduling, then verify against low choices plus meaningful non-local unlocks and low local/near-outer sweep. Chain length alone is not a substitute for that.

# Nutation Hub Decision: Dense AntiDir Selector Is Diagnostic, Generator-Side V5 Needed - 2026-06-29

- Decision: Hub remains promising as a non-LongChain hard-feel direction, but the current Hub pool still has visible center holes and same-direction / escape-arrow clustering. A selector can expose and reduce those defects for review, but it cannot be treated as a solved generator route.
- Evidence: `NutationHubDenseAntiDirV1` audited `60` solved Hub/Maze rows from `18/18` joined files and found only `1` strict Hub row. The 10-row review pack improves core fill and outer-exit clustering, but still has solve same-axis/same-dir runs up to `18/12` and static same-direction components up to `30`.
- Implementation decision: keep `Export-NutationHubDenseAntiDirReviewV1.ps1` as a read-only audit/selector and `NutationHubDenseAntiDirV1ReviewPack.asset` as a manual review pack. Do not silently mutate `NutationHubSpokeV1/V2/V3/V4`, HubCurve, or HubRail assets to hide drift in existing comparisons.
- Required next step: if the user likes the Hub feel, open a new Hub V5-style generator pass with generation-side gates for core fill, no large center hole, static same-direction neighbor/component limits, outer-exit spatial run and side caps, solve-time same-axis/same-dir caps, and likely a nonzero Hub direction-grammar mode. Post-filtering old Hub rows is no longer enough.

# Difficulty Feel Decision: Skeleton Name Is Not Skeleton Contract - 2026-06-29

- Decision: a pack or generator named `Skeleton` is not accepted as evidence for the user's intended skeleton route unless its actual role/dependency graph traces back to the user-provided skeleton. The skeleton must be the solving contract, not just a visual source or a historical label.
- Negative evidence: user rejected `SGPRhythmLab_PressureReadStageLockSkeletonGateV1DenseDepReview2Pack` because it did not relate to the prior skeleton concept and felt like a long chain being blocked once.
- Boundary: `SkeletonGateV1DenseDepReview2` is now a negative reference only. Do not use its `S/S` trace, high coverage, or low choice metrics as proof that the current skeleton route works.
- Required next step: build from the actual skeleton specification: preserve blocker/target/door/role ownership, audit relation survival after fill, cap long-chain/single-clear false positives, and only then package completed levels for review.

# Generated-Root WBP Decision: Use Staged Semantic Cell Plan Before Chain Cutting - 2026-06-29

- Decision: live slot-fit scans must not run inside the source-basin rootgen growth loop as the primary decision engine. They are too expensive and violate the intended staged planner shape; use offline supply audits and staged selectors instead.
- Evidence: t81 live joint selection and t87 low-budget live scan both timed out, while the staged selector plus exporter completes quickly and gives auditable edge/cell evidence.
- Decision: hard-excluding broad light-role reserve is not the next route. Full reserve hard exclusion stalls coverage around `0.208`; ray-only hard exclusion still fails the `8` planned-edge gate. This repeats the old "reserve everything, grow nothing" failure mode.
- Evidence: t82/t84 feedback shows pressure maps and activation caps move bottlenecks between owners but do not create more slot-fit capacity. The blocker moves from owner5 to owner8/11-like self-blocking patterns, while slot-fit supply stays at `1-3/8`.
- Implementation decision: introduce and maintain whole-board semantic cell-plan artifacts before chain cutting. `Build-GeneratedRootWBPV12SemanticCellPlanV1.py` records generated root cells, selected relation edges, slot chain/reserve cells, and cell responsibilities without modifying assets.
- Required next step: build a staged semantic slot preplanner that assigns concrete slot corridors/cells and relation contracts before final chain cutting. The cutter should consume this plan; it should not invent path chunks or use trace as a per-chain generator.

# Generated-Root WBP Decision: Preplan Constraints Are The Next Contract Surface - 2026-06-29

- Decision: the next WBP implementation surface is a staged semantic slot preplan with explicit cell-level constraints, not another scalar blocker map or a larger selected-edge count.
- Implementation decision: `Build-GeneratedRootWBPV12SemanticSlotPreplanV1.py` emits three read-only artifacts: per-cell preplan, per-edge preplan, and per-cell constraints. Slot-ready edges create `reserve_empty` and `cut_chain_body` constraints; unmet edges create `clear_or_replan_blocker` constraints from `topBlockedCells` and first-hit/release preflight signals.
- Evidence: t81 has only 5 selected edges but yields 2 slot-ready contracts and only 6 unique demand root conflicts, concentrated at owner5/owner2 cells. t84 has 8 selected edges but only 1 slot-ready contract and 13 demand root conflicts spread over more owners. Therefore edge count/diversity alone is not a progress metric.
- Required next step: root growth or chain cutting must consume preplan constraints directly: preserve slot-ready reserve/chain cells, avoid or relocate top demand blocker cells, and re-audit relation survival. Do not treat pressure avoidance, activation spread, or post-hoc semantic labels as sufficient if they do not reduce unmet slot constraints.

# Difficulty Feel Decision: Skeleton Fill Cannot Be Per-Chain Local Acceptance - 2026-06-29

- Decision: filling a hard skeleton by adding one chain or one tiny bundle, tracing it, and accepting local non-regression is not the right route for the user's skeleton-completion problem.
- Evidence: user指出新增链条本身也必须提升或保持难度；到后段时剩余空间常常只剩外出口链条，局部接受会把未来空间和后段出口预算提前吃掉。
- Evidence: 项目历史已有类似失败边界：SmallSemanticSlotFill V0 可从约 `0.502` 推到 `0.627`，但继续冲 `0.66` 会外出口爆；Ray-first 0.46+ 之后单链失败变成 dependency/timing 信号；WBP 也确认必须先做 staged semantic cell plan，而不是 live/per-chain scan。
- 2026-06-29 user correction: SOG/OwnerHit/short bundle attempts are still part of the rejected "place one or a few chains" family. The project has already pushed this local-fill family to about `0.7` coverage without a breakthrough, so do not continue it as the skeleton-to-complete route.
- Boundary: do not propose "place one chain, trace, keep if OK" as the primary skeleton fill strategy. Trace仍用于阶段性整板验收，但生成侧必须先批量规划 solve-order roles, exit budget, future slot capacity, and difficulty contribution.
- Required next step: if continuing this skeleton route, treat the skeleton as a whole-board answer graph and build a batch semantic slot/exit-budget plan before cutting chains. Added chains need planned dependency roles, not just empty-cell coverage.

# Difficulty Feel Decision: Hybrid Skeleton Completion Is Pilot-Only - 2026-06-29

- Decision: combining current generators can justify a pilot production test, but not a claim of mass production for skeleton-to-complete hard/B levels.
- Evidence: `SeededDirectSGPFillBaselineV1` has the knobs needed for high-coverage protected fill, but raw V1.31 full095 traces are Drop/LocalEasy with extreme choice pressure. Release-aware/post-kernel probes reduce some risks but still do not preserve hard structure reliably.
- Evidence: `ValidatedRootBackgroundSGPFillV1` preserves root/background constraints more safely, but its default target and strongest historical proof are conservative expansion, not stable `0.90-0.95` complete-level production.
- Evidence: whole-plan smoke on V1.31 rank85/rank67 consumed an actual `Build-RayConstraintMapV1` role-map in `SeededDirectSGPFillBaselineV1` with pre-action mask, release-aware heads, LDF protected owners, and post-kernel/native support pressure. The generator could not approach target `0.75` under those protections and emitted only `0.35-0.40` coverage candidates; official light trace was `12/12 solved=False/Drop/LocalEasy`. Chain-length guards passed, so the failure is not long-chain false-positive pollution.
- Boundary: do not equate "can generate many candidates" or "processTier B" with player-felt difficulty. Any pilot must gate maxChain/singleClearShare, local/near/outer runs, support depth, relation survival, and non-LocalEasy structure before calling output a production candidate.
- Required next step: before Pilot20/Pilot50, implement or identify a real complete-fill planner that plans solvability/role survival at the board level. Current role-map + DirectSGP protected fill is a negative baseline, not a pilot candidate.

# PSG/Skeleton Decision: Connection Is Corridor-Wave Planner, Not Direct Seeded Fill - 2026-06-29

- Decision: the PSG connection point for user-provided skeletons is generation-side pressure scheduling over the skeleton's remaining ray corridors, not post-filtering and not directly handing the skeleton to SGP/PSG as occupied cells.
- Evidence: `Build-SkeletonPSGFeasibilityV1.py` on V1.31 rank85/rank67 found many useful PSG slots (rank85 `349` useful / `66` strong, rank67 `540` useful / `140` strong), but `0` immediate-strong slots in both. All strong connectors are corridor-based, while immediate owner hits are local-patch risks.
- Interpretation: PSG alone works because it owns the pressure graph and can preserve release corridors while shaping heads/regions/axis shifts. Once a skeleton is pre-fixed and DirectSGP fills the residual space, those corridors get consumed as generic filler, causing outer/local collapse or unsolved states.
- Required next step: build a skeleton-seeded corridor-wave PSG planner that first reserves/plans critical/cross-region corridors and release waves, then cuts chain groups against a global exit/axis/local budget. Do not pursue "PSG direct fill after skeleton" unless a feasibility pass shows non-local immediate-strong capacity.

# PSG/Skeleton Decision: Corridor Plan Is A Hard Contract For The Cutter - 2026-06-29

- Decision: after `Build-SkeletonPSGCorridorWavePlanV1.py`, selected corridor units must be treated as hard semantic contracts for the next cutter/preblocker stage, not as soft scoring hints for generic density fill.
- Evidence: rank85 and rank67 both selected `12/12` strict no-overlap corridor-wave units. rank85 uses `48` reserved corridor cells over `11` hit owners and `7/7` head/hit regions; rank67 uses `40` reserved cells over `11` hit owners and `9/9` regions. This proves the skeletons have sufficient non-local PSG pressure capacity before chain cutting.
- Boundary: if a later fill pass occupies a selected corridor cell without proving it clears before the connector wave, it is breaking the planned PSG pressure graph. That is the same failure mode as DirectSGP filling corridors as generic empty cells.
- Required next step: implement a corridor-plan consumer/cutter that creates connector heads/seconds at their assigned wave and optionally timed preblocker chains inside `timed_preblocker_allowed` corridor cells. Verification should first audit contract survival (`firstHitOwner`, corridor clear-by-wave, owner/region/axis budgets) before any high-coverage or Demo packaging attempt.

# Generated-Root WBP Decision: Semantic Preplan Consumption Is Viable But Not Sufficient - 2026-06-29

- Decision: keep the Generated-Root Whole-Board Planner line active and continue through semantic preplan consumption. The current viable route is staged whole-board cell/slot planning feeding root growth, then relation-preserving chain cutting; do not revert to per-chain trace acceptance, long bands/slabs, coverage-only tuning, or no-root baselines.
- Positive evidence: t89 consumed t81 semantic slot-preplan constraints inside source-basin rootgen and produced a generated-root, Greedy-solved root with coverage `0.3036437`, lightRole `8/8/0`, topRoots/openers `8/8`, and role-slot reserve `3/3/3`. This is a real improvement over t81/t79's `2/5` ready/reserve baseline.
- Boundary: t89 is not a candidate level and does not prove the final `0.95+` coverage/hard target. After offline 8-edge selection it still has only `3/8` slot-ready semantic contracts, `5` unmet contracts, and `16` demand-root conflicts.
- Required next step: treat `3/8 -> 5-6/8` ready semantic contracts as the next feasibility gate. Only after that gate improves should the project attempt high-coverage chain cutting or final official trace validation.

# Generated-Root WBP Decision: Slot-Capacity Objective Must Enter Root Generation - 2026-06-29

- Decision: the next breakthrough surface is explicit disjoint slot-capacity during root generation, not stronger semantic-preplan demand/preserve penalties and not selector-only tuning.
- Evidence: t91/t92 repair-aware selector proves the old 8-edge plan contained false capacity: after removing shared-bundle and redundant-with-ready contracts, the same root yields a clean `3/4` ready core. t93 wide recompute supply can find `4/8` ready and reserve `4/4`, but cannot pass `4`.
- Negative evidence: t90 and t97 direct rootgen runs with repair constraints both collapsed to a lower-diversity root at coverage `0.2854251`, topRoots/openers `6/6`, and no better slot-ready result. This means scalar penalty feedback alone damages source-basin diversity before it creates capacity.
- Capacity evidence: t96 candidate-variant audit sees `100` slot variants over `18` edges, yet max disjoint slot edges remains `4`. Top overlap cells concentrate in the same slot corridor band around `8,12`, `9,12`, `7,12`, `6,12`, `5,12`, `8,16`.
- Required next step: rootgen must plan/reserve at least five independent slot basins/corridor bands before edge selection. The selector should remain repair-aware, but it cannot manufacture a fifth disjoint slot from a root whose audited capacity is four.

# Campaign500 Long-Chain Decision: Split Perimeter Arrow Problems Into Three Metrics - 2026-06-29

- Decision: Campaign500 long-chain perimeter cleanup must distinguish three cases: direct outward heads (`outerExit*`), same-chain boundary straight runs (`edgeStraightRunMax/edgeStraightChainCount`), and cross-chain perimeter rail accumulation (`edgeRailRunMax/edgeRailCellShare`).
- Evidence: V3 fixed `outerExitRunMax=1`, but user screenshots still showed continuous outer-edge arrows. Those screenshots matched same-chain/cross-chain edge rails rather than direct outward-head runs.
- Implementation decision: V4 treats `edgeStraightRunMax<=5` as the visual-review hard target and uses deterministic `hardEdgeSplit` to cut the longest same-chain boundary run. `edgeRailRunMax/edgeRailCellShare` stay as soft scoring and manual-review metrics because a hard `edgeRail<=4/5` produced zero useful candidates at 0.95 coverage.
- Boundary: do not ban all edge arrows and do not lower coverage by deleting perimeter cells. The goal is readable, releasable perimeter structure: long-chain main visual remains, mid/short support chains are allowed, and visible continuous outer-frame rails should be avoided.

# Nutation Hub/Maze SolveFlow2 Decision: Keep Debt As Guard, Do Not Overclaim - 2026-06-29

- Decision: keep the new Hub/Maze `antiCollapseDebt` scoring term in `NoMaskProceduralGenerator.cs` as a lightweight regression guard, not as a productionization solution.
- Evidence: solveflow2 smoke restored HubRail's better dual-rail candidate and avoided the worse lane-7/choice-peak regression, but HubRail still has 0 production keep and sameAxis/sameDir `16/14` on the best row.
- Evidence: HubSpokeV4 remains the best Hub continuation point after solveflow2, with 2 VisualKeep rows and best local/near `5/4`, directional `0.157`, STS `0.809`, collapse `0.260`; however sameAxis `20` still blocks strict keep.
- Negative evidence: MazeRail and MazePatchV2 remain local-collapse/high-risk after solveflow2. Best MazeRail has local/near `14/10`, directional `0.593`; best MazePatchV2 has local/near `9/8`, directional `0.618`, STS `0.678`.
- Rejected path: do not continue escalating local head scoring, local reject penalties, or selector-only thresholds for Hub/Maze. They can reshuffle candidates but do not change the solve-order axis/stage structure enough.
- Required next step: implement a Hub V5 or equivalent generator-side pass with solve-order axis/stage/region scheduling and direction grammar. Use HubPatch/V4 as the starting family; keep rail and maze variants as style-proof/diagnostic until the scheduler exists.

# Nutation Flow Decision: Complete Chain-Language Review Cells, Keep Out Of Strict - 2026-06-29

- Decision: Flow can have multiple chain languages (`curve_chain`, `rail_chain`, `patch_chain`) for style/language comparison, but Flow remains review/noise/baseline and should not be treated as a strict difficulty proof.
- Implementation decision: `NutationFlowCurveV1` now has `-Lane Curve|Rail|Patch` support in its wrapper, with thin FlowRail/FlowPatch wrappers for stable entry points. `Join-SGPPressureTraceMetrics.ps1` tags `flow_rail_chain` and `flow_patch_chain`; the style matrix includes all three Flow lanes.
- Evidence: FlowRail smoke1 is stable and clean as a language control (4/4 solved, 4 TraceOrderKeep, straightness `0.598-0.654`). FlowPatch smoke1 is usable after light retune (4/4 solved, 2 TraceOrderKeep + 1 VisualKeep + 1 Reject) but remains more volatile.
- Boundary: Flow's natural solve topology is continuous propagation and local release, so even good Flow rows are not evidence that Nutation has solved non-local difficulty. Strict rows should continue to come from Peel/LongChain until Hub/Maze or another solve-order-controlled style is genuinely productionized.
- Required next step: use Flow lanes for matrix coverage and player-feel comparison, then return engineering effort to Hub V5 / solve-order axis-stage-region scheduler or another non-Flow style primitive.

# Generated-Root WBP Decision: Slot-Capacity Layout Plus Variants Is The Breakthrough Surface - 2026-06-29

- Decision: continue WBP through explicit slot-capacity layout plus variant-aware edge planning. This is now the preferred route over scalar blocker pressure, repair-demand-only rootgen, or selector-only tuning.
- Evidence: t98c generated root preserved real generated-root identity and reached coverage `0.3056680`, authored OK, Greedy solved, and max disjoint slot capacity `5` (`112` variants / `19` slot edges). This breaks the t96/t93 ceiling of `4`.
- Boundary: hard-excluding old ready slots is too aggressive. t98b remained legal but collapsed supply to `9/76` and max disjoint capacity `3`; the viable profile hard-excludes only new target open bands while old ready slot/overlap cells stay soft-scored.
- Selector decision: edge planning must be variant-aware. The old selector only saw each edge's best reserve cells and still selected `4/8`; with `--include-slot-fit-variants`, t98c selects a `5/5` slot core and an 8-edge plan with `5/8` slot-ready contracts.
- Current unmet edges are not a blocker-pressure feed problem. t98c repair classifier groups `19->8`, `19->10`, and `19->15` under owner19 root-flex repair and emits no new demand constraints. Next work should replace/defer those tail edges or generate another root/layout pass to reach `6/8`, not push more demand penalties into rootgen.

# Campaign500 Long-Chain Decision: Fix Perimeter Rails In Generation First - 2026-06-29

- Decision: continuous outer-frame rails in Campaign500 long-chain levels must be controlled by generation-side source selection, merge scoring, and acceptance gates. A bounded notch/split pass is acceptable only as a final deterministic guard, not as the primary strategy.
- Evidence: V4 fixed same-chain boundary straight runs but its Demo9 still had cross-chain `edgeRailRunMax avg=23.33`. V5 selected direct sources by perimeter score, penalized edge rails during merge, gated `edgeRailRunMax/edgeRailCellShare`, and reduced Demo9 to `edgeRailRunMax avg=9.778/max=14` while keeping coverage avg `0.954`.
- Boundary: do not ban all edge arrows and do not lower coverage by blanket perimeter deletion. The target is a readable perimeter with no obvious continuous outer frame; long-chain main visual and mid/short support chains remain required.
- Current limitation: V5 excludes order `22` and `450` from Demo9 because this pass did not find clean 0.95 candidates for those slots. Do not force those slots by relaxing rail/share gates; expand source profiles or improve perimeter-aware generation first.

# Nutation LongChain Decision: Patch Lane Completes Language Matrix But Stays LongChain-Specific - 2026-06-29

- Decision: `NutationLongChainPatchV1` is accepted as a strict-review-ready LongChain chain-language lane, not as general hard-feel proof.
- Implementation decision: LongChainPatch uses direct rectangle peel with a new spec/profile, a patch-aware long-chain score/gate, explicit `long_patch_chain` tagging, and a stable wrapper `.worktrees/nutation-peel/Tools/Production/Invoke-NutationLongChainPatchProductionV1.ps1`.
- Evidence: smoke1 produced 4/4 solved, 3 TraceOrderKeep + 1 Reject, and 3 production keep rows. The source profile has maxChain `21-22`, avgChain `9.66-11.48`, straightness `0.288-0.316`, and joined tags `maze_long_chain + patch_chain`.
- Boundary: this lane is useful for style/language variety and LongChain review packs. It should not override the project decision that long-chain low choices can be a false-positive hardness signal.
- Required next step: use the matrix for player-feel comparison across LongChain spine/curve/rail/patch; continue Hub/Maze productionization through solve-order scheduler work rather than more LongChain-only evidence.

# Generated-Root WBP Decision: Capacity Selection Must Be Two-Stage, Not Hard Mixed Bands Or Full In-Loop Scan - 2026-06-29

- Decision: the next WBP breakthrough attempt should use cheap generated-root pool expansion followed by post-hoc variant slot-capacity reranking. Do not put full slot-fit audit inside every root-growth attempt, and do not treat mixed target bands as a sufficient root-generation proxy.
- Evidence: t99c proved that mixed orientation `target_open_band` layout can be generated, but hard-excluding those bands collapsed actual semantic supply. The best t99c root had only `9/80` slot-fit edges and max disjoint slot capacity `2`, far below t98c's `19/80` and max disjoint `5`.
- Evidence: t100a with rootgen-internal `light-role-slot-fit-selection` and `light-role-slot-audit` timed out after about 10 minutes on only 8 attempts. This is too expensive for the generation inner loop, even though slot-fit remains the right objective.
- Boundary: t98c remains the current positive checkpoint, but it is still only `5/8` semantic slot-ready and not a final level. Do not jump to high-coverage chain cutting until a root/edge plan reaches at least `6/8` ready or shows an equivalent relation-rich contract surface.
- Required next step: generate a larger cheap root pool using the t98c viable profile, keep real generated-root identity, then audit each selected root post-hoc with variant supply/capacity. Only the best audited root should feed variant-aware edge selection, semantic preplan, repair, and eventual whole-board chain cutting.

# Generated-Root WBP Decision: Breakthrough Needs Target-Owner Diversity Plus Capacity Gates - 2026-06-29

- Decision: continue WBP by moving target-owner diversity into root selection/gating, while keeping full slot-fit/capacity audit as post-hoc validation. Do not continue by only adding local target buckets, selector repair penalties, or more coverage pressure.
- Evidence: t101a proves cheap root pools are affordable but not automatically better than t98c; audited max disjoint capacities stayed at or below `4` except the prior t98c root.
- Evidence: t102b local hot buckets increased raw supply (`24/80` on one root) but not disjoint capacity, because many candidate blockers still targeted the same owner/choke. Raw slot supply is therefore insufficient; target-owner distribution matters.
- Evidence: t103a target-diversity rank reproduced max disjoint capacity `5` on a different generated root and improved activation spread to `activationTopRoots=6`, confirming that target diversity is a real signal rather than noise.
- Boundary: t103a still stops at `5/8`; repair-drop selection cannot manufacture a sixth slot edge from the same root. The missing capacity is a root-generation/layout problem, not a final selector problem.
- Required next step: add or use explicit root-pool gates for `strictEdgeProxyDistinctTargetOwners`, `strictEdgeProxyDistinctTargetTopRoots`, and `strictEdgeProxyTargetOwnerDominance`, then run a larger cheap pool and post-hoc capacity audit. The acceptance target for the next stage is at least one root with max disjoint slot capacity `6` or a clean semantic plan exceeding `5/8`.

# Nutation Hub V5 Decision: Static Anti-Cluster Is Not Enough For Hard Feel - 2026-06-29

- Decision: keep the `.worktrees/nutation-hub-v5-control` V5 line as diagnostic evidence, not as a review/production pack. Do not continue by merely tightening same-direction/static hub thresholds or escalating head-direction penalties.
- Evidence: V5 smoke4 achieved much better static hub shape than the prior review rows (`coreCoverage ~0.915-0.954`, core hole max `1-3`, same-dir neighbor rate `~0.227-0.266`, outer-exit run `1-3`), proving center-hole and outer-exit clustering can be controlled in the current generator.
- Negative evidence: official trace still rejected all `5/5` smoke4 rows as `local_collapse`; local/near runs were `10-13`, same-axis `11-24`, same-dir `8-21`, and dependency-local rate `0.605-0.687`. Smoke2 showed the same pattern with `4/4` solved but `0` production keep.
- Interpretation: Hub static readability and visible direction dispersion are separate from solve-order hardness. The generator can make a denser, cleaner-looking hub while the playable release order still collapses into same-region/same-axis sweeps.
- Required next step: implement a generator-side solve-order/stage scheduler for Hub/Maze that budgets region, axis, and release stage before or during chain cutting. Treat HubSpokeV4/V5 reports as fixtures for validating that scheduler, not as evidence that selector-only tuning is sufficient.

# Skeleton PSG Decision: Reserve-Then-Dense Fill Is A Negative, Contracted Filler Is Required - 2026-06-29

- Decision: keep the Skeleton->PSG line alive, but reject `reserve selected corridors + generic SeededDirectSGP/Dense mop-up` as the completion route. It can preserve the visible connector contract while destroying playable pressure.
- Positive evidence: rank85 rayclean `top04` materialized only 4 PSG connector units and still official-traced as solved/process A/TrueHardCandidate with hardV3 `0.762`, avg/max choices `2.89/5`, and no outer-exit run. This proves the PSG integration point exists when the skeleton is treated as a pressure graph.
- Negative evidence: reserve-only SeededDirectSGP pushed coverage to `0.882-0.905`, but official trace was `4/4 solved=False`, `4/4 Drop/LocalEasy`, max choices `53-61`. `Build-SkeletonPSGContractAuditV1.py` shows the connector contract still passes `4/4`; the failure is not losing the corridor but creating `53-61` filler initial openers and `40-41` added dynamic base-ray blockers.
- Boundary evidence: `MaxBoundaryDirectExitOpenersPerPass=8` reduces the choice explosion but stalls coverage at `0.599-0.626` and remains unsolved Drop. Adding rank85 `criticalDependencyEdge` release-aware heads accepts `16-19` semantic heads but only reaches `0.622-0.654`; it still leaves `10-11` filler openers and `38-39` dynamic base-ray blocks.
- Implementation decision: the next generator must make filler chains part of the PSG contract at placement time. Required gates are: bounded opener supply as state, dynamic base-ray block rejection or explicit timed-blocker ownership, per-wave/per-owner budgets, and a planned filler DAG. Post-hoc trace per chain and coverage-only dense fill should not be resumed as the main path.

# Skeleton PSG Decision: Full-Ray DAG Guard Is Required For Contracted Filler - 2026-06-29

- Decision: a Skeleton->PSG filler contract must model the full ray-blocker dependency graph, not only the immediate first-hit owner. Filler edge insertion is viable only when a full-ray DAG guard rejects cycles before asset write.
- Evidence: `Build-SkeletonPSGScheduledDAGFillV1.py` unguarded insert preserved planned first-hit edges and kept filler openers at `0`, but the 0.55 coverage candidate official-traced as `solved=False`. The hidden failure was later blockers on the same ray, not opener explosion.
- Positive evidence: enabling `--full-ray-dag-guard` produced official solved/process B candidates at coverage `0.5029412`, `0.5421569`, and `0.6294118` from the same rank85 top04 skeleton. These keep selected connector contracts `4/4` and filler initial opener heads `0`.
- Base-ray policy decision: `strict` is clean but caps around `0.54`; `head` is the best current coverage tradeoff (`0.629` solved/B) because filler bodies can act as controlled blockers while heads avoid base dynamic ray cells. `off` is a negative because allowing heads on base dynamic rays creates full-ray cycles and stalls lower.
- Boundary: this is not yet a full/high-coverage completion route. Next work should introduce timed base-ray blocker ownership and capacity planning for full-ray DAG space. Do not replace this with trace-per-chain search or generic dense mop-up.

# Skeleton PSG Decision: Solver Topology Needs Geometry-Capacity Feedback - 2026-06-29

- Decision: the right abstraction is still “complete solver/precondition topology first, then geometry realization,” but the topology cannot be accepted on abstract A/B metrics alone. It must consume geometry slot capacity and final board trace semantics before being treated as a viable level plan.
- Positive evidence: `Build-SkeletonPSGSolverTopologyV1.py --release-lanes 4` produced 0.88/0.94 abstract topologies with openers `3`, avg choices about `4.2-4.8`, max `6-8`, and no cycles. This proves the release-budget layer that generic fill lacked.
- Geometry evidence: `Build-SkeletonPSGTopologyRealizerV1.py` can materialize a timed=0 release-lane topology to coverage `0.7274510`; official trace of `real_guard_neutral_pos1__a_a403f2e932_top04_t940_v02` is `solved=True/processTier=B`, openers `2`, avg/max `2.86/7`. This beats the earlier scheduled-DAG `0.629` ceiling and proves topology-to-geometry is not a dead end.
- Boundary evidence: the same positive is still `LocalEasy` with `dependencyFollowRunMax=7` and `localPatchSolveRunMax=7`, so it is not yet a player-feel hard level. Anti-local scoring improved some collapse diagnostics but reduced coverage and still failed process tier, so scoring-only is not the final fix.
- Timed blocker decision: timed base-ray blockers are useful conceptually but cannot be scheduled as abstract dependencies first. Timed-heavy topologies stall geometrically because “owner-hit target + base full-ray intersection + no full-ray cycle” often has no slot. Future timed blockers must be selected from feasible ray/slot capacity, not assigned randomly by abstract rank.
- Implementation guard: geometry realizer must reject self-ray body blockers. Full-ray DAG checks that ignore `hit == owner` can pass assets that official trace cannot solve.
- Required next step: make the topology emitter/realizer co-design release lanes from feasible owner-hit/timed-ray slot pools, then validate with `Build-SGPRhythmTrace.ps1`. Do not go back to one-chain trace fill, reserve+dense fill, or treating low abstract choices as sufficient hardness.

# Skeleton PSG Decision: Capacity Audit Is The V2 Feedback Surface - 2026-06-29

- Decision: TopologyRealizer V2 should promote topology targets only through a geometry-capacity audit surface: planned target ray availability, path/body fit, full-ray DAG viability, target dominance, and local-hit pressure. Do not continue by only tweaking realizer score weights after abstract topology is fixed.
- Evidence: `Build-SkeletonPSGTopologyCapacityAuditV1.py` on `real_guard_neutral_pos1__a_a403f2e932_top04_t940_v02` shows the first hard stall is not generic board fullness. Failed topo owner `155` rank `31` had only `5/980` rays that hit planned targets and `0` legal path variants.
- Evidence: the same board is official solved/process B but `LocalEasy`; generated local first-hit rate is `0.8505` and placed sequence local-hit run is `15`, matching `dependencyFollowRunMax=7` and `localPatchSolveRunMax=7`.
- Required V2 gates: per-target slot capacity before node selection, cap target-owner/full-ray dominance, penalize same-region/local first-hit chains as hard state rather than score-only, and keep self-ray/full-ray DAG guards. Coverage can rise only after these gates remain healthy.

# Skeleton PSG Decision: Realizer Penalties Cannot Rescue A Local Late Topology - 2026-06-29

- Decision: stop treating TopologyRealizer V2 scoring/guard tuning as the main breakthrough path. It is useful as a diagnostic/probe, but the next real implementation belongs in topology generation: release-lane seed/anchor semantics plus geometry-capacity-aware target selection before late ranks are emitted.
- Evidence: V2 no-shadow changed planned placement language substantially (`generatedLocalHitRate=0.4567` at coverage `0.7333333`), but final actual first-hit audit relocalized to `0.8976`, and official trace remained `LocalEasy`.
- Evidence: hard first-hit shadow guards preserve intent but destroy capacity almost immediately (`1-2` placed chains for all-owner/distance guards, `0.3039` coverage for generated-only guard). Shadow must be budgeted and scheduled, not blanket rejected.
- Evidence: shadow-cost scoring can improve some braid/direction signals but still fails hard-feel (`shadowcost_smoke6_lite`: solved/B, coverage `0.6745`, `LocalEasy`, `dependencyFollowRunMax=10`). Increasing candidate window worsened coverage, so penalty-only search is not converging.
- Required next step: build a topology emitter/realizer loop that treats late filler targets as capacity-constrained slots, protects release-lane seed anchors, and refuses rank bands whose feasible non-local slot pool has already collapsed. Do not spend more cycles on realizer penalty sweeps without changing topology semantics.

# Campaign500 Long-Chain Decision: Outer-Exit Heads Are A Separate Budget - 2026-06-29

- Decision: after fixing same-chain edge straight runs and cross-chain perimeter rails, Campaign500 long-chain generation also needs an explicit total outward-head/opener budget. `outerExitRunMax` alone is insufficient because many isolated outward heads can still make the perimeter feel like free exits.
- Implementation decision: V7 adds an outer-exit head cap, stronger source/merge penalties, and a legal reverse pass for boundary outward-head chains. This is still generation-side control; do not solve it by cropping the outer row/column or deleting perimeter cells.
- Evidence: V6 section3 already fixed the outer-frame fill-band bug, but source rows still had outer-exit head counts `10/12/25`. V7 reduced those to `8/10/16` while keeping coverage `0.9599-0.9674` and official trace `3/3 solved`.
- Boundary: V7 is a visual-review checkpoint, not production keep. The challenge row remains Drop/high-opener in official trace, so the next pass should reduce challenge outer exits/openers further while preserving the readable long-chain main visual and mid/short support chains.

# Generated-Root WBP Decision: Slot Means Target Basin, Not Just Edge Diversity - 2026-06-29

- Decision: the WBP capacity breakthrough must model slots as current-root target basins: target owner/target top root, choke or escape-ray family, and the reserve/corridor cells that keep that basin independent. Target-owner diversity is only a search/ranking signal, not an acceptance proof.
- Evidence: t104d selected roots with strict-edge target diversity (`7` owners and `4` target top roots) but post-hoc variant capacity still capped at `5`. This means edge-rich and owner-diverse roots can still be basin-poor.
- Evidence: t105e target-basin prepartition can produce `6` spatial buckets when multiple centers per owner are allowed; owner4 had to use alternate center `5,4` because owner0 already occupied the stronger `4,6` center.
- Negative evidence: t105f consumed that 6-bucket layout and selected a real generated root, but post-hoc capacity was only `4` despite `20/80` raw slot-fit edges. The old-root spatial basin layout did not bind to the new root's owner identity, so supply concentrated around new targets `6/5/16/7`.
- Required next step: make target-basin layout root-current or owner-bound before using it for chain cutting. Acceptable loops are whole-board cell-plan iterations from current-root supply/capacity audits; unacceptable loops are per-chain trace acceptance, coverage-only tuning, local bucket-only tuning, or selector-only repair after the root has already collapsed.

# Campaign500 Long-Chain Decision: V8 Is Review-Ready, Not Production-Strict - 2026-06-29

- Decision: preserve the V7/V8 long-chain visual direction for manual review, but do not call it production keep until challenge-slot opener pressure drops further. V8 reduced the challenge row's source openers/outward heads from `16/16` to `14/14`, but strict cap is still lower.
- Implementation decision: continue treating initial openers and total outward heads as generation-time budgets, not as a post-export crop/delete problem. Legal chain reversal (`outerExitReverse`/`openerReverse`) is acceptable only when Greedy, coverage, perimeter, and long-visual-share gates survive.
- Evidence: V8 section3 trace is `3/3 solved` with process tiers `A/B/Drop` and coverage avg `0.9643`; normal rows are usable review rows, while the challenge row remains high-choice/high-opener (`openers=14`, official `Drop`).
- Required next step: for expansion beyond section3, add a stronger challenge-row source/merge budget or carrier/support rewrite that lowers openers and outward heads without flattening long-chain main visual or removing mid/short support chains.

# Campaign500 Long-Chain Decision: Strong Opener Budget Must Be Challenge-Scoped - 2026-06-29

- Decision: keep crop-aware opener pressure as a hard/challenge generator constraint, not a global Campaign500 long-chain rule. Normal rows should preserve the V8 path unless their own metrics regress.
- Evidence: global V9 crop-preview/source pressure produced `0` order30 review rows; best normalB coverage stayed around `0.9472`, below review min `0.955`, even though opener pressure improved. This is an unacceptable trade for normal slots.
- Positive evidence: challenge-scoped V9e restored order22/order30 to V8-level rows and produced a challenge row with source openers/head `11/11`. Official trace improved challenge openers `14 -> 11`, p80 choices `17 -> 15`, max choices `19 -> 16`, and outerExitHead `13 -> 10`.
- Boundary: V9e still leaves challenge as official `Drop` and avgChoices essentially unchanged (`10.73 -> 10.67`). The next valid route is not more global pressure; it is combining low-opener `slot_balanced` with lower choice-curve `slot_headmix` while keeping challenge opener budget under about `12`.

# Generated-Root WBP Decision: Current-Root Supply Metrics Are The Post-Hoc Gate - 2026-06-29

- Decision: root-pool acceptance for the next WBP stage must use current-root slot-fit supply metrics, not only strict-edge proxy target diversity or planned light-role edges. Required post-hoc rerank fields are target owners, target top roots, target basin keys, target-owner dominance, and max disjoint slot capacity.
- Evidence: t106b passed cheap strict target diversity well enough to be selected, but post-hoc supply was only `targets 3/3/3` and capacity `3`. t106e also showed a selected root with strong cheap strict target diversity collapsing to `targets 3/3/3` and capacity `2`.
- Evidence: t106c shows that a 6-basin layout from an older root cannot be recycled as a soft target-ray field; it collapsed to `targets 2/2/2` and capacity `2`. This rejects both hard and soft static old-root target-basin transfer.
- Implementation decision: `Export-GeneratedRootWBPV12LightRoleSlotFitSupplyV1.py` now emits current-root target-basin summary fields, and `Build-GeneratedRootWBPV12SourceBasinRootGeneratorV1.py` has default-off `--min-light-role-reserve-activation-top-roots` as a cheap hygiene gate. The hygiene gate can reduce obvious single-source planned roles, but it is not an acceptance proxy.
- Required next step: batch cheap roots, export current-root supply, then rerank by `slotFitCandidateTargetOwners >= 6`, `slotFitCandidateTargetBasinKeys >= 6`, low target-owner dominance, and capacity `6+`. Only roots passing that post-hoc gate should proceed to semantic preplan and whole-board chain cutting.

# Generated-Root WBP Decision: Rerank CSV Is The Root-Pool Acceptance Surface - 2026-06-29

- Decision: use `Run-GeneratedRootWBPV12CurrentRootCapacityRerankV1.py` as the standard acceptance surface between cheap generated-root pools and semantic preplan/cutting. A root is not promoted because its strict-edge proxy looks diverse; it is promoted only after current-root slot-fit supply and variant capacity audit pass the rerank gate.
- Evidence: t107a reranked 14 historical selected roots and found `0/14` pass for `targetOwners>=6,targetBasinKeys>=6,maxDisjoint>=6,dominance<=0.45`. The best known class remains max disjoint `5`, now measured as current supply targets `5/4/6` rather than a manually inferred `6+` target-owner pass.
- Evidence: t107c deliberately relaxed strict proxy dominance to select a root with apparent target diversity, but current-root rerank collapsed it to `targets 3/2/3` and max disjoint `3`. This confirms relaxed proxy selection produces false positives and should not feed semantic preplan directly.
- Implementation decision: the rerank CSV must carry not only pass/fail and max disjoint, but also capacity blocker feedback (`capacityTopBlockedTargets`, `capacityTopBlockedBasins`, `capacityTopOverlapCells`). These diagnostics are the correct input for the next current-root feedback layout.
- Required next step: convert t107 blocker/overlap diagnostics into a layout/rootgen feedback pass or a multi-seed batch runner that uses the rerank gate as the only promotion criterion. Do not resume selector-only repair or full-board cutting until at least one root reaches current-root capacity `6+` or equivalent relation-rich capacity evidence.

# Generated-Root WBP Decision: Owner-Basin Layout Is Diagnostic, Not A Transfer Proof - 2026-06-29

- Decision: keep target-basin layout grouping by owner/basin as a useful representation fix, but do not treat a migrated owner-basin feedback layout as proof that the next generated root will preserve capacity. Current-root owner identity still has to be verified after generation.
- Evidence: t108a showed why owner-only grouping was too lossy: the best t107a root had `targetOwners=5` but `basinKeys=6`, and the new `owner_basin` grouping produced six distinct target-basin buckets including two independent owner20 basin roles.
- Negative evidence: t108c selected the high-proxy near miss created by soft owner-basin feedback, but post-hoc current-root rerank collapsed to `targets 3/2/3` and max disjoint `3`. t108d hard-excluded the owner-basin target buckets and still collapsed to max disjoint `3` at best.
- Interpretation: spatial feedback from one generated root can preserve visible bucket positions, but it does not bind the next root's owner graph. The failure is not merely bucket count; it is root-current owner assignment and slot-fit capacity after generation.
- Required next step: either run larger cheap root pools with t107 rerank as the only promotion gate, or design a cheaper in-generation approximation of current-root slot-fit/capacity. Do not spend more cycles on static migrated target-basin layouts alone.

# Generated-Root WBP Decision: Capacity Collapse Is Now Slot Overlap, Not Missing Semantics - 2026-06-29

- Decision: after t109, stop treating target-owner/top-root diversity as the main bottleneck. The current bottleneck is disjoint slot capacity: many valid semantic edges compete for the same reserve/corridor cells.
- Evidence: t109g found a fresh generated root with current-root supply `7/6/7` and dominance `0.467`, but max disjoint capacity was only `4`. This means the generator can create relation diversity, but those relations are not spatially independent enough for whole-board cutting.
- Evidence: the t109g best root's capacity blockers are concentrated: blocked target `11` accounts for `32` blocked variants, blocked basin `B1->B2` accounts for `32`, and overlap cells `9,3` / `8,3` dominate. This is a reserve-slot collision problem.
- Negative evidence: t109h soft feedback from the overlap map loaded correctly but selected the same root with the same capacity `4`. Soft cell penalties are too weak or too late when the root-growth objective still promotes the same owner graph.
- Required next step: move a cheap current-root capacity approximation into rootgen selection, or add a hard/structured lane allocator for reserve slots. The acceptance surface remains post-hoc current-root rerank; semantic preplan/cutting waits until capacity `6+` or an equivalent disjoint relation proof exists.

# Nutation HubSpoke V5 Decision: Candidate Pool Can Cross Trace-Order Gates, But Local Feel Still Needs Generator Control - 2026-06-29

- Decision: keep the HubSpoke V5 soft dependency-stage generator, but use a candidate-pool + official trace selection layer for review/prototype output. Do not declare Hub V5 strict production solely because TraceOrderPreferred produces keep rows.
- Evidence: the single-winner smoke16 V5 run produced `0` production keep and best same-axis `9`; the new `NutationHubSpokeV5Pool` smoke traced `24/24` solved rows and found `2` TraceOrderKeep rows with same-axis/same-dir `7/6` and `6/6`.
- Evidence: the first keep row also lowered dependency-local same-region rate to `0.484`, showing the soft dependency-stage counters are directionally useful when the selector can see more candidates.
- Boundary: both keep rows still fail visual/local strict reasons (`directionalRisk>0.34`, `localPatchRun>6`, one `nearOuterRun>5`) and are classified as `local_collapse/high_risk`. Treat `NutationHubSpokeV5PoolProductionKeepPack` as a review/prototype pack, not a final production lane.
- Required next step: either add official-trace-guided pool selection as a formal Hub V5 production surface, or move the remaining local/directional budgets into generation-time solve-order scheduling. Do not return to trace-like local replay scoring inside candidate construction; smoke14/15 showed that degraded selection.

# Nutation HubSpoke V5 LocalBreak Decision: Soft Priors Help, Hard Same-Region Rejects Break Coverage - 2026-06-29

- Decision: keep `NutationHubSpokeV5LocalBreak` as a Hub V5.1 review/prototype lane, not a strict production lane. It should be used to inspect the local-break direction and compare visual feel against V5 Pool.
- Evidence: smoke4 traced `16/16` solved rows and produced `1` VisualKeep plus `1` TraceOrderKeep. The VisualKeep row hit the visual/local gates (`localPatchRun=6`, `nearOuterRun=5`, `directionalRisk=0.307`) but missed trace-order gates (`collapse=0.408`, same-axis/same-dir `9/9`, dependency-local `0.636`). The TraceOrderKeep row passed STS shape better (`0.798/0.257`, same-dir `5`) but still failed local/directional gates (`local=8`, `nearOuter=6`, `directionalRisk=0.435`).
- Negative evidence: a hard reject version for repeated same-region/micro/same-axis dependency candidates collapsed coverage to about `0.08-0.10`; a later strong-penalty version restored coverage but produced `0` visual/production keep. Hub V5 still needs soft multi-objective scheduling or pool reranking, not hard local geometry bans.
- Acceptance target remains unchanged: do not promote Hub V5 until a row passes both visual/local gates and trace-order gates in the same candidate.

# Nutation Production Decision: First Batch Uses LongChain + Peel, Flow As Controlled Easy Mix - 2026-06-29

- Decision: start Nutation production from `LongChain curve/rail/patch/spine` and `Peel curve/rail`. These are `strict_review_ready` in the current matrix and form the production-standard core.
- Decision: allow `Flow curve/rail/patch` as controlled easy-content mix, not as strict production core. Suggested ratio is `10-20%`, then reduce if manual review finds the batch too simple or too sweep-like.
- Decision: keep `Hub`, `Maze`, and `PeelPatch` out of formal production for now. They may appear only in representative/review packs until local-collapse, solve-time control, and production efficiency improve.
- Evidence: current matrix including Hub V5 Pool has `20` joined CSVs / `97` rows / `16` style-chain representatives. Strict-ready rows come from LongChain and Peel; Hub V5 Pool has `2` TraceOrderKeep rows but all `24/24` rows remain `high_risk/local_collapse`.
- Review surface: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationStyleMatrixRepresentativeReviewPack.asset` gives one sample per style-chain combo. Use it to visually compare style/language coverage, not as a production approval pack.

# Campaign500 Long-Chain Decision: Low Opener Count Is Not A Choice-Curve Proxy - 2026-06-29

- Decision: do not keep pushing challenge rows with opener-only pressure. Initial openers and outer-exit heads are necessary visual/entry budgets, but they do not predict official trace choice pressure by themselves.
- Evidence: V10 `slot_lowchoice_pressure` lowered static openers/head to `11/11` and longVisualCellShare to `0.582`, but official trace worsened to avgChoices `11.64`, p80 `16`, max `19`, process `Drop`. The lower-opener branch created more midgame choice pressure than V9e balanced.
- Evidence: V9/V10 `slot_headmix` has worse opener/head `14/14` but a better choice curve (`avgChoices=8.30`, p80 `12`, max `14`) than balanced. Its problem is visual/outer pressure, not dynamic choice alone.
- Decision: the viable route is `headmix_outerclean`: preserve headmix's lower choice-curve language while constraining outer-head/outer-side/outer-ring runs. V11/V12 show this can reduce challenge static pressure to about `12/11`, but current throughput is just below acceptance (`coverage=0.9490` in V12).
- Boundary: do not relax the 0.95 review floor or outer-ring acceptance just to keep V12 outerclean. Treat it as a near-miss and improve generation throughput/perimeter repair instead.

# Campaign500 Long-Chain Decision: Style3x3 Preview Can Run Before Performance Rewrite - 2026-06-29

- Decision: for small visual preview packs, use the current verified generator/trace/pack pipeline instead of blocking on performance optimization. V13 produced `9` preview levels (`3` per role) and mounted a Demo pack successfully.
- Decision: full量产 still needs a performance pass before large candidate pools: cache source/candidate analysis, reduce repeated Unity AssetDatabase writes, run official trace as a pooled selector rather than per-small-batch manual step, and avoid concurrent hidden Unity batch jobs on the same worktree.
- Evidence: V13 preview generation reached `section_long_lock=3`, `lock_light_negative_space=3`, `lock_buckle_pressure=3` selected rows; official trace solved `9/9`. The preview was blocked only by a conflicting hidden `v14_family_expand` batch, not by generator correctness.
- Production boundary: `section_long_lock` is the strongest current role (`A/A/B`, avgChoices `5.46`). `lock_light_negative_space` is visually useful but still official `Drop`; `lock_buckle_pressure` is review/challenge-only because openers `13-14`, official outerExitHead up to `18`, and process tier remain `Drop`.
- Operational boundary: keep `Campaign500LongChainPilot3V13Demo9Pack.asset` as the current visual review surface. Do not treat interrupted `v14_family_expand*` logs or assets as a mounted baseline until they are intentionally rerun and traced.

# Nutation Production Decision: Mixed Is A Quality-Only Global Pool - 2026-06-29

- Decision: correct the Mixed definition. `Mixed` is not Flow/Peel composition and must not apply style-family or chain-language caps by default.
- Decision: Mixed means an unconstrained Nutation candidate pool. Flow, Peel, LongChain, Hub, Maze, and curve/rail/patch/spine/mixed chain-language variants may all compete; rows survive only by quality gates.
- Decision: duplicate control is allowed as a quality gate, not as style/chain quota. Mixed defaults to a repeated style-signature cap so near-identical rows do not crowd the keep pack.
- Quality gates: solved, coverage, process tier, choice peak, local/near-outer run, directional/stripe risk, STS quality/collapse, same-axis/same-dir run, dependency locality, exact duplicate removal, and repeated style-signature cap.
- Implementation: `.worktrees/nutation-flow-peel-production/Tools/Production/Invoke-NutationMixedProductionV1.ps1` is the correct Mixed entry. It builds a global source pool from the Nutation lane catalog, traces once, joins once, and writes `nutation_mixed_production_v1_production_keep.csv`.
- Boundary: `Invoke-NutationFlowPeelProductionBatchV1.ps1` remains only for dedicated Flow/Peel batches. Its old `-EnableMixedPostSelection` path is deprecated and should not be used as Mixed production.

# Nutation HubSpoke V5 Decision: Hybrid Rerank Confirms Post-Filter Alone Is Not Enough - 2026-06-29

- Decision: keep the Hub V5 hybrid rerank as the current diagnosis surface, but do not promote any Hub V5 row to production until visual/local and trace-order gates pass in the same row.
- Evidence: `Export-NutationHubV5HybridReviewV1.ps1` ranked `40` Pool + LocalBreak rows and found `0` HybridStrict. It found one `VisualNearTrace`, one `TraceNearVisual`, and six `BalancedNearMiss` rows.
- Interpretation: Pool and LocalBreak are solving opposite halves of the problem. Pool crosses same-axis/same-dir/STS on its best row but remains local/directional heavy; LocalBreak crosses local/near/directional on its best row but misses collapse and same-axis/same-dir dependency flow.
- Required next step: update generation-side scoring/scheduling so LocalBreak's local visual cleanliness and Pool's trace-order spread can coexist. The next change should be soft multi-objective scheduling or larger pool/rerank with generator feedback, not hard same-region rejects and not review-pack promotion.

# Campaign500 Long-Chain Decision: Expand Families Through Seed Source Grammars, Not Coordinate Copy - 2026-06-30

- Decision: promote `slot_headmix_outerclean` into the active Campaign500 long-chain family pool and expand new families from seed-source grammar traits. The first accepted grammar set is `seed_long_lock`, `seed_long_maze`, and `seed_long_weave`.
- Implementation rule: the 49-row seed source pool is used as a grammar/role prior through generator knobs (`SourceGrammar`, `SourceFamilies`, source chain/coverage/candidate deltas, and scoring), not by copying seed coordinates or mirroring seed layouts.
- Evidence: V14 clean targeted smoke selected `6/12` rows and official trace solved `6/6`; selected rows include `headmix_outerclean`, `seedlock_gate_carrier`, `seedmaze_chamber_corridor`, and `seedweave_braid_carrier`.
- Boundary: this proves family-pool expansion feasibility, not final production readiness. V14 still has Drop rows and outward-head pressure; official trace remains mandatory before any 200-level selection.
- Operational rule: use `-campaignLongChainProfileFilter` only for targeted smoke/debug. Full production should keep the full family pool and then select by report + official trace, preferably after a performance pass.

# Campaign500 Normal Production Decision: 4 Normal Slots Per Section With Flow 40 And Peel Backbone - 2026-06-30

- Decision: for the final Campaign500 template, the first normal-only production target is `50 sections x 4 normal slots = 200` slot-specific candidates, not all `350` normal rows at once. Shape and hole slots remain excluded from this replacement/reference lane.
- Decision: each 10-level section gets two ordinary slots, one hard slot, and one peak/review-peak slot. Flow is raised to about `40` rows and uses FlowCurve/FlowRail/FlowPatch to create feel variation.
- Decision: Peel is a production backbone, not just a style showcase. The plan uses PeelMid for all hard slots, PeelHard for early/mid peak slots, and late PeelLight support; NeutralMixed provides PSG-like ordinary mixed texture.
- Decision: late LongChain appears only as `LongChainProbe` with `reviewOnly=1` until manual体感 and official trace prove it can enter final keep. PressurePeak covers mid/late peak pressure without relying on untrusted longchain.
- Canvas rule: planned normal production canvases target aspect `0.70-0.85` with soft ceiling `0.90`, because the template's original normal targets are mostly too thin for the desired portrait feel.
- Output contract: `Exports/Campaign500_PSG_Template_20260626_095625/campaign500_normal_4slot_plan_v1.csv` is the slot plan; `campaign500_normal_pilot20_plan_v1.csv` is the first 20-row smoke input.
## Nutation Hub V5 Hybrid Smoke2 Kept, Smoke3 Rejected - 2026-06-30

- Decision: keep Hub V5 Hybrid smoke2 weights as the current Hub V5 prototype baseline.
- Reason: smoke2 produced the first same-row trace-order candidate for Hybrid (`TraceOrderKeep`, sameAxis/sameDir `6/6`, dependencyLocal `0.559`) and only missed strict visual by `nearOuterRun+1`.
- Rejected alternative: smoke3 strengthened local/near counterweight. It traced `20/20` but produced `0` STS keep / `0` production keep and worsened same-axis/same-dir/local collapse, so those weights were reverted.
- Boundary: do not call smoke2 strict production. The next accepted change must reduce nearOuter from `6` to `5` while preserving smoke2's trace-order/dependency gains.

# Nutation Mixed Decision: Early Head Axis Forcing Breaks Coverage - 2026-06-30

- Decision: for Hub/Maze mixed-chain generation, do not put strong axis/direction diversity control into early or mid head selection. The generator is still coverage-first, and early head forcing removes viable future heads.
- Evidence: isolated Hub mixed smoke3/4 in `.worktrees/nutation-hub-maze-mixed` repeatedly collapsed to low fill (`0.08-0.30`) or coverage below gate when head axis/direction penalties were active before the board was mostly filled.
- Accepted compromise: late-only head shaping plus moderate body turn/straightness tuning can produce Hub mixed TraceOrderKeep rows. Hub smoke5 produced `2` TraceOrderKeep rows with sameAxis/sameDir `8/5`, but with lower coverage/throughput and residual visual risks.
- Boundary: Maze mixed did not benefit enough from this shared tuning. Maze smoke1 produced `0` TraceOrderKeep and remained `local_collapse/high_risk`, so Maze needs a distinct constraint-maze solve-order primitive rather than copied Hub mixed controls.

# Campaign500 Normal Pilot20 Decision: Keep Review Pack Separate From Production Keep - 2026-06-30

- Decision: Pilot20 normal production uses two outputs: a 20-row ReviewPack for visual pacing review and a smaller ProductionKeepPack for rows that pass official trace/order gates.
- Evidence: the first plan-driven run generated `20/20` assets and official trace solved `20/20`, but strict `TraceOrderPreferred` selection kept only `6/20` rows. This proves the generation surface works, while also showing single-candidate-per-slot is not enough for 200-row production.
- Decision: before full 200-row expansion, change the production strategy to multi-candidate-per-slot with trace-based selection. Use the 20-row review pack to inspect style pacing, but use `campaign500_normal_pilot20_production_keep.csv` as the production-quality truth.
- Boundary: LongChainProbe remains review/probe unless the user explicitly promotes it; one Pilot20 LongChainProbe row passed trace gates, but the lane is still not approved as a normal production backbone.

# Nutation Hub V5 Hybrid Decision: Keep Smoke4, Reject Smoke5 - 2026-06-30

- Decision: current Hub V5.2 Hybrid baseline is smoke4, not smoke2/smoke3/smoke5.
- Evidence: smoke4 moved the best strict gap from `nearOuterRun+1` to `localPatchRun+1` while preserving trace-order: `20` rows, `1` TraceOrderKeep, STS/collapse `0.802/0.281`, local/near `7/5`, sameAxis/sameDir `8/7`, dependencyLocal `0.620`.
- Negative evidence: smoke5 tightened sameMicro/parent distance and expanded specs, producing `1` VisualKeep but `0` STS pass / `0` keep; visual/local pressure alone breaks trace-order.
- Boundary: do not weaken strict gates to admit smoke4, and do not continue escalating local penalties. Next iteration should add solve-order local-break scheduling or a trace-aware candidate selector to reduce localPatch by one while preserving STS.

# Nutation Hub V5 Hybrid Decision: Reject Recent-Micro Head Prior - 2026-06-30

- Decision: reject smoke6's generation-time recent-micro local-break prior and keep smoke4 as the baseline.
- Evidence: smoke6 produced `20` rows, `4` processKeep, `0` STS pass / `0` production keep. It worsened the top-row local/near to `11/9` and sameAxis/sameDir to `13/13`; the best near row still had `local=7` and sameAxis `10`.
- Interpretation: pushing micro-region dispersion during head construction disturbs trace-order more than it reduces playable localPatch. It behaves like smoke5's local pressure family rather than a true solve-order scheduler.
- Boundary: the next Hub V5 improvement should happen after candidate generation, or through an explicit solve-order local-break scheduler that can see predicted trace windows. Do not repeat placement-time micro dispersion as the main fix.
## Do Not Scale Campaign500 V2 High-Density Source Plus Many-Merge Path - 2026-06-30

- Decision: `Campaign500LongChainFamilyExpandV2` should not scale the current high-density rectangular source plus many-step longify merge loop as the final 200-candidate production core.
- Evidence: the path can repair continuous outer-ring fill (`outerRingRun` improved from `25` to `10-13`) and reach around `0.95` coverage, but single-slot smokes take several minutes and still overproduce outer-exit heads (`15+`) on the tested section50/order499 seedweave slot.
- Required next direction: move boundary-head budgeting and interior support/cross-carrier allocation into source generation or early source shaping, then use longify as a lighter refinement step. Do not keep adding random late-stage retries as the main route.

# Nutation Hub V5 Hybrid Decision: Reject Pure Seed-Band Search - 2026-06-30

- Decision: keep `NutationHubSpokeV5HybridSearch` as diagnostic WIP only; do not promote pure seed-band expansion as the next Hub V5 production route.
- Evidence: Search smoke1 generated `24/24` rows and official trace solved all rows, but produced `0` visualPass, `1` STS pass, `0` TraceOrderKeep, and `0` production keep.
- Four-lane rerank with Pool + LocalBreak + smoke4 + Search still had `0` HybridStrict; smoke4 best row remains the top same-row trace candidate with only `localPatchRun+1` visual gap.
- Rationale: pure seed expansion finds more candidates but does not align proxy local flow with official trace localPatch. Several Search rows had clean-looking proxy head/trace runs while official local/directional/same-axis metrics regressed.
- Next principle: Hub V5 needs final-candidate solve-order scheduler/proxy calibration, or a stronger official-trace-informed selection surface; do not spend more time on blind seed bands.

# Campaign500 Long-Chain Decision: Preserve Seed-Source Bodies Before Longify - 2026-06-30

- Decision: `SourceGrammar=seed*` / `slot_seed*` profiles must skip source preview head notches. The seed-source expansion route should preserve cropable source body and enforce outer cleanliness with longify/final gates.
- Evidence: with source preview notches, expanded section3 smoke produced `0` selected rows, coverage `0.926-0.945`, and cropCoverage `0.898-0.917`. After skipping notches and using 32 source candidates, `seedweave_cross_carrier` and `seedlock_dual_gate_buckle` reached coverage `0.9592` and official trace solved.
- Boundary: this does not certify every expanded lane. `seedmaze_pocket_spine` stayed at coverage `0.9279`; hold it until source/crop grammar is redesigned.
- Operational rule: do not return to low/high rectangular source-density tuning as the mainline. Expand positive seed-source grammars in small traced batches, then scale by selector.

# Campaign500 Long-Chain Decision: Static Selection Is Not Enough For V2 Family Expansion - 2026-06-30

- Decision: V2 family expansion rows must be promoted by official trace, not by source-side `selected=True`, coverage, opener count, or visual metrics alone.
- Evidence: `slot_seedweave_cross_carrier_lowchoice_pressure` and `slot_seedlock_dual_gate_buckle_lowchoice_pressure` produced `6/6` static-selected rows, but official trace classified all `6` as `Drop`; average maxChoices rose to `15.17` and average over10Rate to `0.394`.
- Decision: keep lowchoice variants only as opt-in diagnostic/negative controls. They must not enter default family order or batch expansion until a future trace proves otherwise.
- Positive baseline remains seed-source-like V14/longify grammar: the verified V14-style normalA sample traced `5/5` solved with `4 A / 1 B`. New `seedweave_cross_carrier` and `seedlock_dual_gate_buckle` are usable expansion candidates only under official trace selection, especially beyond early sections.

# Campaign500 Long-Chain Decision: Use Short Asset Paths For Family Lab Trace - 2026-06-30

- Decision: Campaign500 long-chain family-lab outputs must use short LevelDefinition folders and level ids before official trace. The valid lab output folder is `Assets/ArrowMagic/SOData/Levels/C5LCFamLabV1/`, with `c5lcf_*` level ids.
- Evidence: the first family-profile-lab smoke wrote long paths under `Campaign500LongChainComplementPoolV1/campaign500_longchain_complement_pool_v1_*`; official trace read only `1/10` rows and marked `9` missing because most absolute paths exceeded Windows legacy path limits.
- Fix: shorten asset folder/id and keep the postselection rule that rejects empty-path, `Missing`, or unsolved rows before per-slot selection.
- Boundary: missing trace from the long-path smoke is an infrastructure bug, not family quality evidence. Use `campaign500_longchain_family_profile_lab_s02_smoke2_shortpath` as the valid traced evidence.

# Campaign500 Long-Chain Complement Decision: Pack Only Trace-Gate Rows - 2026-06-30

- Decision: complement postselected/review packs must only include rows with `traceGate=True`. A solved row with `processTier=Drop` is diagnostic unless it also passes the complement trace gate.
- Evidence: `campaign500_longchain_complement_family21_s50_smallprod1` built a postselected pack from a solved spine row that still failed `sourceOuterRing`; this made the pack look more production-like than it was.
- Fix: `Invoke-Campaign500LongChainComplementPoolV1.ps1` postselection now filters on `traceGate=True`, and `PSGLongLockLongifyV1Generator.cs` rejects bad late-section near rows before they become `selected=1`.
- Boundary: source reports may still contain `selected=0` rejected rows for diagnosis. Do not count them as candidate pool supply.

# Campaign500 Long-Chain Complement Decision: Fix Late Outer Heads Upstream - 2026-06-30

- Decision: late/large Campaign500 complement rows must control outer-exit heads and initial openers during source construction and merge selection, not rely on post-crop single-chain reversal as the main repair.
- Evidence: `campaign500_longchain_complement_family21_s50_outerfix4` produced `0` selected rows; all six rows had `outerExitReverse=0`, `openerReverse=0`, and `lateOuterCleanup=0`, while failures were dominated by `chains_high`, `outerExitHeads`, `outerExitSide`, and `openers`.
- Fix direction: reduce late/large source overbuild, lower source opener caps, search a larger source pool, and make the merge loop choose dynamically analyzed candidates that reduce chains/openers/outer heads instead of accepting the first static long-chain merge.
- Boundary: outer-frame crop and perimeter notches remain valid for breaking continuous edge bands, but they are not sufficient when many chain heads are born on the edge pointing outward.

# Campaign500 Long-Chain Complement Decision: Avoid Chamber Corridor For Late Large Normal Slots - 2026-06-30

- Decision: `slot_seedmaze_chamber_corridor` should not be the default rescue lane for section-50 / late-large normal slots until its source grammar is redesigned.
- Evidence: outerfix5-8 probes on order `491` showed the chamber lane can trade one failure for another but does not reach keep quality: clean-head variant had coverage `0.7154`; higher source coverage variant reached only `0.8976` after full merge and still failed openers/head `23/23`, outerRing `0.855/16`, and outerExitSide `9`.
- Boundary: this does not reject chamber corridor globally. It remains potentially useful in smaller or non-cropped slots; the rejection is specifically for late/large normal slots using current crop/source grammar.
- Next direction: prefer a support/lock lane that exists in the selected profile window for late-large normal slots, or build an inner-fill source grammar before crop instead of trying more scalar threshold tuning on chamber.

# Campaign500 Long-Chain Decision: Add Straight Language As Isolated Generation Profile - 2026-06-30

- Decision: add a little straight-chain language through a dedicated generation-side profile (`slot_straight_spine_carrier`), not by post-fixing the current 12-row review pack and not by allowing boundary rails.
- Rationale: the user wants long-chain visual variety, but previous edge straight runs/outer exit rails were a rejected visual defect. Straight language must be inner-carrier style with mid/short support and explicit penalties on boundary rails, outer-exit heads, and outer-ring runs.
- Operational rule: preserve the current 12-row `C5LCFamLabV1ReviewPack.asset`; any straight-spine smoke must use isolated output paths and official trace before being mounted or counted as keep supply.

# Campaign500 Long-Chain Decision: Straight-Spine V1/V2 Are Not Production-Ready - 2026-06-30

- Decision: do not promote the current `slot_straight_spine_carrier` V1/V2 probe rows as a proven straight-chain family, despite official solved/gate rows.
- Evidence: V1 produced `6/13` trace-gate and V2 produced `4/11` trace-gate, but chain-shape audit did not improve straightness over the old 12-row keep. Old keep avg maxStraightRun was `6.33`; V1 was `5.33`; V2 was `5.25`. TurnRate also worsened (`0.6056` old vs `0.6233/0.6246`).
- Interpretation: current changes can make clean, solvable inner-carrier candidates, but they do not create visibly lower-bend straight-chain language. Next work should alter source construction/chain primitives, not just longify merge scoring.

# Campaign500 Long-Chain Decision: Inner-Straight Carrier Is Structure Proof Only - 2026-06-30

- Decision: keep `slot_inner_straight_carrier` as a probe-only source primitive for now; do not promote it into the production candidate pool or default 200-candidate scheduling.
- Evidence: profile-lab V1/V2 probes both produced `8/8` official solved and `2/8` trace-gate. All traced rows remained process `Drop`, and rejects were dominated by `choiceP80` and `maxChoices`.
- Structural evidence is positive: inner all8 avg maxStraightRun `14.0`, gate2 `13.0`, compared with old12 `7.33` and straight V1/V2 `6.33/6.25`; turnRate improved to roughly `0.576-0.584`.
- Interpretation: source-side straight carrier injection solves the visual language problem that `slot_straight_spine_carrier` did not solve, but it creates too much choice pressure. Next accepted improvement needs solve-order / low-choice scheduling around the carrier, not another small scalar source-score retune.
## 2026-07-01 - Nutation HubMixed Strict30 Uses VisualOnly With LocalPatchRun<=10

- 状态：已采用用于本批 30 候选。
- 决策：`NutationHubMixedV1Strict30TransformWideProductionKeepPack` 使用 `ProductionKeepMode=VisualOnly`，但将 `MaxLocalPatchSolveRun` 从 8 轻微放宽到 10；其他 gate 保持 HubMixed 当前生产口径（coverage、choice、near-outer、directional risk、same-axis/same-dir、dependency-local）。
- 证据：48 行小 transform 池在 `TraceOrderPreferred` 下可得 30，但只有 16 行 visual green；144 行 wide transform 池在 `localPatchRun<=8` 下只有 24 行 VisualKeep，不足 30；放宽到 `<=10` 后有 55 行 eligible，并按 diversity 裁出 30。
- 边界：这是 HubMixed strict30 候选批的筛选取舍，不代表 PSG 或其他 Nutation 风格的默认 gate；后续若要更硬，应继续优化生成侧减少 localPatchRun，而不是长期依赖筛选放宽。

## 2026-07-01 - HubMixed Strict30 Refill Uses LocalPatchRun<=9 Final Keep

- Decision: use secondary refill as a post-generation repair lane for the current 30 HubMixed candidates, not as a replacement for HubMixed generation logic.
- Evidence: refill pool generated `120` candidates from the 30-row transform-wide keep; tracing a 60-row shortlist solved `60/60`. Local10 VisualOnly produced 30 keep rows but slightly raised localPatch average; rejoining the same trace with `MaxLocalPatchSolveRun=9` still produced 30 keep rows.
- Final acceptance gate for this refill pack: VisualOnly, solved, allowed tiers `S/A/B`, `MaxChoices=14`, `MaxLocalPatchSolveRun=9`, `MaxNearOuterPatchSolveRun=6`, `MaxDirectionalSweepRisk=0.46`, same-axis `<=18`, same-dir-head `<=14`, dependency-local `<=0.70`, max rows `30`.
- Result: coverage avg `0.945 -> 0.961`, max empty component avg `13.467 -> 5.0`, outer empty cells avg `11.433 -> 6.033`, while localPatch avg/max is `7.8/9`. STS avg dips modestly from `0.821` to `0.805`, so future refill should keep local9 as the default guard and tune for less STS loss.
## Decision - Choice Rhythm Is Not Avg Choices Only - 2026-07-01

- Campaign normal difficulty tuning should not treat `avgChoices` as the main difficulty proxy by itself.
- Use three layers together: solve-path `choiceWave`, one-step branch next-choice profile, and frontier-drain next layer width.
- Desired normal-hard rhythm is usually many steps in the 3-5 choice band, occasional 1-2 choice choke, low 8+ branch explosion, and low flat-consequence/fake-choice rate.
- For production selection, new choice-rhythm fields are diagnostic-only until calibrated on multiple packs; do not silently change existing keep/rank gates without a separate validation run.

# Generated-Root WBP Decision: t110 Low-Coverage Capacity 6 Is A Structure Signal, Not A Scalar-Tuning Path - 2026-07-01

- Decision: the t110 side-line proves generated roots can reach current-root capacity `6+`, but only after relaxing root coverage to about `0.24` in the tested seed band. Treat this as a structure signal, not as permission to keep scalar gate tuning.
- Evidence: `t110a_relaxed_rootpool_probe_s560880_c001` passed the post-hoc current-root gate with supply `6/6/6`, target-owner dominance `0.280`, and `capacityMaxDisjoint=6`.
- Counter-evidence: the same relaxed setup at `minSelectCoverage=0.26` selected `0` roots. Higher-coverage growth-log candidates around `0.314` existed but had weak strict-edge target-owner/top-root diversity (`2-4`), so they are not relation-rich roots.
- Boundary: do not resume old RCH/RCH-like coverage chasing, and do not keep tuning `coverage/rank/direct budget` as the main move. The next valid WBP route is either a low-coverage generated root causal core followed by whole-board lane/fill planning, or a hard/structured root lane allocator that explicitly preserves disjoint reserve capacity while raising root coverage.

# Generated-Root WBP Decision: Design Hardness Around Planned 1-2 Choice Choke Moments - 2026-07-01

- Decision: WBP hardness should explicitly design one or two meaningful solve steps where available choices contract to `1-2`, then reopen afterward. Do not treat global low average choices or full-board coverage as a substitute for these planned choke moments.
- Rationale: a hard arrow-chain level often feels difficult because the player must recognize a specific forced or near-forced release at a critical wave, not because every step is equally narrow.
- Implementation implication: whole-board cell planning needs scheduled choke/release contracts tied to root basins: before a choke, several visible options may exist but most are guarded/blocked; at the choke wave, only the intended release chain or one alternative should be valid/productive; after clearing it, a basin or cross-basin contract opens.
- Metrics implication: track choice valleys in the official trace (`min choices`, `count of 1-2 choice waves`, and whether the following wave expands), alongside coverage and current-root relation capacity. Avoid optimizing only `avgChoices`, which can hide the absence of real choke moments.

# Generated-Root WBP Decision: Coverage Breakthrough Must Now Be Anti-Local Fill - 2026-07-01

- Decision: after t111i, do not keep optimizing the protected filler only for coverage, Greedy solved, and max choice. The next WBP fill objective must include anti-local/interleave/localPatch pressure, because the current route can reach `0.95+` coverage but still traces as `B/LocalEasy`.
- Evidence: t111i preserves the generated root prefix and reaches coverage `0.9534413` with official solved `True`, openers `4`, avg/max choices `4.1/7`, over10 `0`, and meaningfulUnlockRate `0.867`; however official process is `B`, HardStructure is `LocalEasy`, localPatchSolveRunMax is `7`, dependencyFollowRunMax is `7`, and causalAntiLocality is only `0.16`.
- Boundary: small numbers of intentional early filler are acceptable only as a late coverage closure tool and must remain capped/audited. They are not a difficulty solution.
- Next direction: keep the low-coverage generated root + semantic slot materialization path, but score/accept filler bundles by reducing local patch runs, same-region follow, and dependency conveyor behavior before final trace. The acceptance target remains coverage `>=0.95`, official solved, process `A+`, and root identity preserved.

# Generated-Root WBP Decision: Use Simultaneous Wave-Front Width As Skeleton-B Diagnostic - 2026-07-01

- Decision: add simultaneous wave-front width as a diagnostic layer for WBP candidates. It clears all currently available chains as one wave, records the current/next wave choice width, and classifies available chains as root, semantic slot, or filler.
- Evidence: t111i is official `B/LocalEasy`, but the wave-front audit solves it in `39` waves with avg/p80/max width `2.026/3/5`, `26/39` waves at `<=2`, and `16` forced-single waves. This shows a real low-width skeleton rhythm that is not captured by process tier alone.
- Boundary: this metric does not replace official trace or the A/Hard target. It should prevent false rejection of "skeleton B" candidates and guide the next fill objective: preserve narrow waves while reducing localPatch/follow/local-conveyor collapse.
- Next direction: compare future WBP fills by both official metrics and wave-front rhythm. Good candidates should keep coverage `>=0.95`, official solved, root identity, and many meaningful narrow waves, while improving anti-locality enough to reach process `A` or HardPotential.

# Generated-Root WBP Decision: t111 Is The Base Route, Placement Must Target Rhythm Not Narrowness - 2026-07-01

- Decision: treat t111 as the current Generated-Root WBP base route: low-coverage real generated root with high causal capacity -> semantic slot materialization -> protected high-coverage fill -> official trace, wave-front audit, and human playtest. Do not return to old RCH, rootless high coverage, or scalar coverage tuning as the main route.
- Evidence: t111i preserves generated-root identity, reaches coverage `0.9534413`, official solved `True`, wave-front avg/p80/max `2.026/3/5`, and received human feedback of clear "good / 优" quality even though official hard class remains `LocalEasy`.
- Placement experiment evidence: t112 placement proxy can lower choice width (`t112c` maxChoices `6` vs t111i `7`; `t112h` maxChoices `4` vs t111f `7`), but over-compression worsens local/boring signals (`t112c` localPatch/nearOuter `11/11`; `t112h` low2Rate `0.723`, lowChoiceRunMax `13`, choke score `0`).
- Boundary: placement proxy should be a soft scoring signal, not a hard gate. Hard prefilter runs t112e/t112f preserved spread but stalled coverage at `0.779/0.828`. The next placement objective is not "fewer choices everywhere"; it is meaningful choke moments plus reopen, with caps on long low-choice/local-patch runs.
- Next direction: t113 should keep the t111 base route and replace single-chain greedy acceptance with a two-stage bundle scheduler: cheap candidate pool -> bundle selection by coverage gain, wave-front rhythm band, local-run proxy, region/owner/direction alternation -> Greedy/official trace.

## Decision - 2026-07-01 21:12 - PSG difficulty shift requires dependency-aware generation
- Avg choices alone is insufficient, but branch/frontier rhythm proved useful for deciding generator direction.
- Same-slot proof rejected the few-long-chain/low-fragmentation fix: it worsened branch explosion despite longer chains.
- Same-slot proof supports dependency-aware placement as the next generator family: it sharply reduced branchNextChoiceExplosionRate and frontierDrainExplosionRate.
- Dependency-aware work must explicitly prevent local conveyor collapse and same-axis/near-region solve runs; otherwise it becomes controlled but still LocalEasy.

## Decision - 2026-07-01 22:25 - DependencyRhythm Needs Scheduled Windows, Not More Cross-Region Weight
- Decision: do not promote `DependencyRhythm` to production yet. Keep it as an experimental Nutation lane until it has solve-window/frontier-width control.
- Evidence: same-slot v4/v6 reduced official same-axis/same-dir runs close to PSG baseline (`57/24` proof -> `17/14`) and raised source cross-region hits (`34`), but branchNextChoiceExplosionRate rose to `0.768`, avg/max choices to `9.27/16`, and process stayed Drop/LocalEasy.
- Evidence: v5 proved that simply forcing longer dependency chains breaks early-wave construction (`fill 8/1452`) because early wave length guards are structurally required.
- Interpretation: cross-region dependency placement and anti-local scoring are necessary but not sufficient. They must be paired with scheduled dependency windows: only a bounded number of fronts should be productive in each solve wave, with later reopen/interleave.
- Next implementation direction: add a stage/window or bundle scheduler above gate-aware placement that chooses candidate chains by coverage gain, predicted wave, region/direction alternation, and next-frontier width. Avoid another round of scalar score retuning.

# Generated-Root WBP Decision: Trace Hard Labels Are Proxy Signals, Not Player-Feel Truth - 2026-07-01

- Decision: for t111/t112 style Generated-Root WBP candidates, do not treat official `LocalEasy` as equivalent to "the level feels easy". Treat it as a specific proxy failure: anti-locality, local patch run, near-outer patch run, and dependency-follow/conveyor structure are below the current hard gate.
- Evidence: t112c keeps coverage `0.9534413`, official solved, maxChoices `6`, choiceRhythmScoreV1 `0.688`, and `choiceChokeMomentScoreV1=1`; user playtest still reads it as not simple even though hardStructureV3 remains `LocalEasy`.
- Metric interpretation: `hardStructureV3` and `processTier A` heavily penalize long `localPatchSolveRunMax` / `nearOuterPatchSolveRunMax` and low `causalAntiLocalityScore`. This is useful for catching old high-coverage local-peel false positives, but it can under-credit a real low-width/root-skeleton rhythm.
- Operational rule: keep official trace as validation, but split judgement into axes: coverage/root identity, solved/process tier, choice rhythm/choke-reopen, local-collapse risk, and human playtest. Future t113 work should improve local/anti-local proxies without destroying the proven choke rhythm or over-compressing every wave.

# Generated-Root WBP Decision: Human-Read Difficulty Separates Search Pressure From Local Risk - 2026-07-01

- Decision: add a WBP human-read difficulty layer that scores tight meaningful choices, choke/reopen wave rhythm, solve-location/direction switching, and far/cross-basin dependencies as the main player-read pressure.
- Decision: local patch runs and near-outer runs are tracked as `localConveyorRisk`, not as an automatic proof that the level is easy. Consecutive local clears are allowed when they are short rhythm segments followed by a required place/direction/dependency switch.
- Evidence: t112c scores higher than t111i on `readSearchPressureV1` because it has stronger choice/choke pressure, while still carrying `LocalConveyorRisk` due `localPatchRun=11` and `nearOuterRun=11`.
- Operational rule: future t113 scoring should optimize `readSearchPressureV1` and reduce `localConveyorRisk` together; do not reduce difficulty to either average choices or official `LocalEasy` alone.

# Generated-Root WBP Decision: t114 Generalizes The Route But Moves The Bottleneck To Final Closure - 2026-07-01

- Decision: treat t114 as evidence that the low-coverage generated-root + semantic materialization + protected fill route is reusable across generated roots, not a one-off t110 accident.
- Evidence: non-t110 roots `t103a_c001` and `t104d_c001` both preserved root identity, materialized real semantic chains, filled to high coverage, and official-traced solved. Best alternate root `t104d_c001_fill095_open` reached coverage `0.9311741`, process/tight `B/B`, and human-read class `SkeletonHard`.
- Boundary: t114 did not reach the final acceptance target. It stalled below `0.95`, remained official `LocalEasy`, and relation audit showed shallow support closure plus local pollution.
- Next direction: focus on the `0.90 -> 0.95` phase with planned closure bundles, support-closure scoring, and anti-local/interleave constraints. Do not restart scalar root tuning or judge the route by root coverage alone.
## Generated-Root WBP Decision: t115 Bundle Fill Is A Subtool, Not The Final A/Hard Mechanism - 2026-07-02

- Decision: keep scheduled bundle fill as a diagnostic/high-coverage closure subtool, but do not promote it to the final Generated-Root WBP generator by itself.
- Evidence: t115 full-coverage sample `t115c` reaches `0.9534413`, official solved, max choice `6`, choice rhythm `0.753`, and lower collapse risk than t113, but still remains `B/B` and `LocalEasy`.
- Evidence: closure-aware below-cover sample `t115e` has stronger human-read/collapse (`humanReadDifficultyV1=0.941`, collapse risk `0.249`) but the final closure to `0.9534413` can reintroduce local conveyor (`t115f` local/near `11/11`).
- Evidence: relation audit remains nearly unchanged in the official hard bottleneck: support depth `2`, low anti-locality, and conveyor-heavy strong parents. Late bundle/filler placement improves rhythm but does not create deep support-closure structure.
- Boundary: t115 did not solve t114 alternate-root closure; closure-aware bundle reached only `0.9271255` vs the previous open-fill best `0.9311741`.
- Next technical direction: support-closure-aware bundle planning should target relation-audit weak parents and anti-local closure edges before the final high-coverage phase; endpoint or single-chain closure should remain a final repair, not the source of hardness.

# Generated-Root WBP Decision: Support Bridges Must Be Planned Before Late Closure - 2026-07-02

- Decision: support-closure bridges are a real mechanism and should be planned before high-coverage closure consumes bridge slots; do not treat them as a late repair step.
- Evidence: t117 early bridge insertion from `t111f` created official `58->68->72->28` with support depth `3`, solved trace, and closure-valid `68->72` plus `72->28` edges.
- Counter-evidence: trying the same bridge insertion after t116c produced no accepted candidates: `72`/`68` first-hit bridge candidates became Greedy-unsolved, and `58` had no usable first-hit slot.
- Decision: high-coverage closure must preserve bridge contracts under official trace, not just Greedy/relation proxy. t117 high-coverage continuations reached `0.945` and `0.953` solved/ReadHard, but official support depth dropped back to `2` because victim `28` was reassigned from `72->28` to `2->28`.
- Implementation implication: next WBP closure planner needs explicit contract ownership constraints such as "after filler, official relation must still contain parent->bridge and bridge->victim", plus solve-order protection for secondary blockers. Scoring `supportDepth>=3` in a local Greedy proxy is insufficient by itself.

# Generated-Root WBP Decision: t118 Late Contract Postfilter Is Not Enough - 2026-07-02

- Decision: do not continue trying to rescue the current t117 branch by endpoint postfiltering or late guard chains. The contract-preserving requirement has to move earlier into the closure planner.
- Evidence: t118 proved official `58->68->72->28` can survive to `0.9453441` (`t118b_contract_ex57_close_s118201`), but exhaustive single-tail and final-bundle enumeration found no Greedy-solvable continuation from that state to `0.95+`.
- Evidence: from `t117i`, the only `0.9493927` near-miss uses the `57` lane and official trace reassigns the victim to `2->28`. From `t117m2`, all six enumerated `0.953+` closures official-solve but remain support depth `2` with `2->28`.
- Evidence: adding a late guard after `t117n` found no Greedy-solvable single-chain candidate, so "fix after full coverage" is not currently available.
- Implementation implication: next WBP planner must reserve non-polluting closure capacity and protect the official order `13->10->66->2` before `72` during the `0.89 -> 0.94` phase. The acceptance gate should include an official-trace contract check, not only a relation proxy score.

## Decision - 2026-07-02 - Read-Demand Hardness Needs Drain-Break Choke, Not Just Low Choices

- Decision: for SGP read-demand hardening, do not accept "low average choices" or "direction switch after local" as sufficient. A candidate should show at least one moment where draining the current small frontier leaves a remote, narrow next frontier (`1-2` choices).
- Evidence: the earlier ChokeBreak review felt positive but still played as continuous bottom-to-top clearing. Its strict trace had `choiceChokeAfterLocalFrontierBreakCount=0`, so low-choice windows did not reliably break the solve flow.
- Evidence: the new V2 drain-break proxy plus official trace produced 8 candidates with `frontierDrainRemoteChokeCount>0`; these are better aligned with the desired "clear a small patch, then move elsewhere with only one or two options" feel.
- Boundary: remote-drain choke is still a proxy. If playtest still reads as continuous, the next step is to force scheduled solve windows or dependency-window planning, not to keep flipping individual arrows or chasing lower `avgChoices`.
- Operational rule: future read-demand review packs should rank/gate by `frontierDrainRemoteChokeCount/Score` together with `choiceChokeLow2ContinuityRunMax`, `maxChoices`, and human playtest. Keep `choiceChokeAfterLocalFrontierBreakCount` as a warning signal because it remains zero in the current drain-break sample.

# Generated-Root WBP Decision: t119 Proves Coverage And Official Bridge Contract Can Coexist - 2026-07-02

- Decision: treat t119 as the first walk-through proof for the current support-bridge line's coverage + official contract gate. It is not final A/Hard acceptance.
- Evidence: one-cell existing-chain extension from the contract-preserved `t119g` near-miss produced 12 candidates at coverage `0.9514170`; official trace solved all 12, and relation audit preserved `58->68->72->28` for 11 of them.
- Evidence: earlier new-chain tail closure from `t119g` had zero legal candidates, so the viable closure move is not "add another chain at the end" but "reserve/extend existing body capacity" without changing the release graph.
- Boundary: extending chain `72` itself breaks the bridge contract and drops support depth to `2`; avoid modifying the bridge chain as a final coverage repair unless official trace proves otherwise.
- Remaining blocker: all t119j successful candidates remain `B/LocalEasy` with localPatchSolveRunMax `16` and antiLocality around `0.156`. Next work should optimize scheduled closure for local-run reduction and anti-locality, not re-prove coverage.

# Generated-Root WBP Decision: Local-Run Guards Work Only If Planned Before Closure - 2026-07-02

- Decision: use delayed local-run guards as a real WBP hardening primitive, but do not treat them as a late one-chain patch. They must be allocated in the whole-board cell plan with bridge-contract protection.
- Evidence: t120c added a 2-cell guard on the local-run `26` ray and reduced official localPatchSolveRunMax from `16` to `7`, proving the official trace is responsive to guard/reroute pressure rather than immovable.
- Counter-evidence: the same t120c guard let official trace clear bridge chain `72` before victim `28`, dropping support depth to `2`. Local-run reduction alone can destroy the root/bridge contract.
- Evidence: t120e shortened chain `57` by a small tail amount and made the guard 3/4 cells long; official trace solved, preserved `58->68->72->28`, preserved support depth `3`, and reduced localPatchSolveRunMax to `9`.
- Implementation implication: the next Generated-Root WBP planner should reserve guard-length capacity and local-run breaker cells during the `0.89 -> 0.95` fill phase. Acceptance should require both local-run improvement and official relation edges `58->68`, `68->72`, `72->28`, not one without the other.

## Decision - 2026-07-02 - DependencyRhythm Needs Placement/Bundles, Not More Scalar Tuning

- Decision: do not promote the current Nutation `DependencyRhythm` experiment to Campaign500 production yet. Keep it isolated in `.worktrees/nutation-flow-peel-production`.
- Evidence: owner-aligned source solve-flow is a necessary fix; before it, generation undercounted same-axis/same-dir runs because Greedy can click non-head cells while the proxy read only authored heads.
- Evidence: v12/v14/v15 slot-316 probes prove score/window changes can move individual official metrics, but cannot yet satisfy them together. v15 best choice row reached avg/max choices `5.76/10` and frontier explosion `0.009`, but sameAxis/sameDir were `36/26`; better sameAxis rows stayed choice-heavy or localPatch-heavy.
- Interpretation: the remaining blocker is structural same-region parent ownership (`dependencyLocalSameRegionRate` about `0.71-0.75`) and local conveyor geometry. Score-only penalties mostly trade one failure mode for another.
- Next implementation direction: add a real dependency-window placement primitive or bundle scheduler that allocates parent-child ownership across regions before chains are committed. Candidate selection should optimize a bundle for coverage gain, wave width, parent-region quota, head-axis alternation, and official-like localPatch risk.

# Generated-Root WBP Decision: Second Local-Run Guard Must Be Co-Planned With Bridge Timing - 2026-07-02

- Decision: do not continue treating the second local-run breaker as a late one-guard patch on the t120e board. It must be co-planned with the bridge timing and `2/28` ownership before final high-coverage closure.
- Evidence: t121b tail-trim search produced 24 Greedy-solvable high-coverage candidates, but none satisfied both `supportClosureBestDepth>=3` and `localPatchSolveRunMax<9` under official trace.
- Evidence: the strongest local-run rows (`r006/r011/r018/r023`) reached localPatchSolveRunMax `7`, proving the second breaker can reduce official conveyor risk, but all dropped support depth to `2` because official relation reassigned victim `28` to `2->28`.
- Counter-evidence: bridge-preserving rows kept official `58->68->72->28` and support depth `3`, but stayed at localPatchSolveRunMax `9`; a 3-cell guard repair preserved bridge but worsened localRun to `11`.
- Implementation implication: the next Generated-Root Whole-Board Planner pass should reserve cells for local-run break, 72-delay, and `2/28` protection as a combined contract. Acceptance must check the pair of conditions together, not in separate late repair probes.

# Generated-Root WBP Decision: T122 Proves The Route Is Viable But Still Local-Run Bound - 2026-07-02

- Decision: continue the Generated-Root Whole-Board Planner route. It has now walked through coverage `0.95+`, official solve, generated-root identity, and official root contract in the same candidate family.
- Evidence: `t122b_from_r019_second_guard_r004_trim27x2_t39` and `t122b_from_r019_second_guard_r010_trim27x2_t39` are official solved at coverage `0.9574899` / `0.9554656`, chains `82`, process `B`, support root `58`, support depth `3`, and support score `0.743`.
- Evidence: relation audit confirms the intended contract edges `58->68`, `68->72`, and `72->28` for the best t122b rows. The contrast row `r001` demonstrates why this matters: it also reaches localRun `7`, but assigns `28` to `2->28` and drops support depth to `2`.
- Boundary: t122b is not final A/Hard. It remains `LocalEasy` because `localPatchSolveRunMax=7`, `nearOuterPatchSolveRunMax=7`, `causalAntiLocalityScore=0.167`, and `causalCudP20=5.667`.
- Implementation implication: next optimization should target the specific official local sequence `40->64->9->24->47->21->58->44`, especially the right-side near-outer run before root entry. Do not resume broad coverage/rank/direct-budget tuning or late one-off guard stacking.

# Generated-Root WBP Decision: Shape Re-Cut Is A Valid Hardening Primitive - 2026-07-02

- Decision: add shape re-cut to the Generated-Root WBP hardening toolbox. It is distinct from adding a guard: it trims the cells causing local geometric adjacency and recuts those cells into a new short semantic chain so coverage is preserved while localPatch geometry changes.
- Evidence: `t123d_trim31_recut_r002_trim31x3_orig` keeps coverage `0.9595142`, official solved, process `B`, support root `58`, support depth `4`, and support score `0.874`, while reducing localPatchSolveRunMax to `5` and raising antiLocality to `0.198`.
- Evidence: the specific `5->31` edge changed from local to non-local after trimming chain `31` by three tail cells and recutting them; measured box gap becomes `3`, and the official max local run moves back to the right-side near-outer sequence.
- Decision: treat the t123b interposer as a valid contract extension: the official relation is now `58->83->68->72->28`, not a failure of the earlier direct `58->68` edge. Chain `83` is released by root `58` and releases bridge `68`, increasing support depth from `3` to `4`.
- Boundary: strict late parent-release blocker insertion for the left run is not enough. The narrow `5->31` search found no candidates; the wide search produced candidates but did not reduce localRun below `6`.
- Implementation implication: the next WBP planner should reserve shape-recut candidates before final closure, especially around the remaining `9->24->47->21->58->44` near-outer run. Do not rely on generic guard stacking to reach A/Hard.

# Generated-Root WBP Decision: t124 Walks Through, Late Relocation Does Not - 2026-07-02

- Decision: continue the current Generated-Root WBP route, but move interruption-slot reservation earlier. t124 proves another hardening step can walk through while preserving coverage, official solve, generated-root identity, and the root contract.
- Evidence: `t124b_right_run_shape_recut2_r006_trim21x4_orig` is the current valid t124 basis: coverage `0.9595142`, official solved, process `B`, hard class `LocalEasy`, local/nearOuter `5/4`, antiLocal `0.265`, CUD p20 `6.25`, support depth `4`, support score `0.944`, and official relation `58->85->83->68->72->28`.
- Decision: reject candidates that only look good in aggregate metrics but break the contract. `t124b_r005` has a slightly better official score and bestRoot `58`, but relation audit assigns `28` to `2->28`, so it is not an accepted WBP continuation.
- Evidence: late shape relocation after `0.9595` coverage has no capacity on this board. The probe found `0` Greedy-solvable candidates even with loose gates; empty-space audit shows only one connected 3-cell empty component and otherwise isolated singletons.
- Implementation implication: same-place re-cut can keep improving anti-locality incrementally, but reaching A/Hard likely needs remote interruption/relocation cells reserved during the `0.90 -> 0.95` planning phase, before final fill consumes connected empty geometry.

# Generated-Root WBP Decision: Endpoint Closure Must Respect Reserved Guard Slots - 2026-07-02

- Decision: do not accept a `0.95+` endpoint closure if it consumes cells reserved for the later contract/local-run guard plan. Coverage that steals the guard slot is a false positive for the Generated-Root WBP route.
- Evidence: t126 early reservation from `t117g` can preserve `0,3;0,4;1,4;1,5;3,10;3,11;11,19;11,20;11,21` through scheduled fill to `0.9453441`, then through a non-reserved one-cell extension to `0.9473684`, while official trace remains solved and relation audit preserves `58->68->72->28`.
- Counter-evidence: all current final `0.9514170` closures from those reserved `0.947` boards cut the same `0,3;0,4` chain released by owner `57`. That reaches the visual coverage floor but consumes the left contract slot, so it cannot serve as the final route basis.
- Evidence: strict scheduled continuation from the reserved `0.947` board with the left slot forbidden produces no further bundle; reserving only the left contract slot from the earlier `t117g` fill also stalls below `0.95`.
- Implementation implication: the final 2-3 cells needed for `0.95+` must be planned earlier as part of whole-board bundle allocation, or the planner must create an alternate non-left closure slot before the board reaches the `0.94` phase. Do not spend more time on late tail-chain enumeration for this exact geometry unless a new reserved slot source is introduced.

# Generated-Root WBP Decision: t129 Proves Reserved Non-Left Closure Can Preserve The Contract - 2026-07-02

- Decision: continue from the t129a reserved non-left closure route. It proves the route can walk through `0.95+` coverage, official solve, preserved generated-root prefix, reserved left slot survival, and official `58->68->72->28` in one candidate.
- Evidence: t129a uses tail-split plus extra tail trim: chain `27` is trimmed/re-cut, chain `31` is extra-trimmed by `2`, and the longer right closure `3,10;3,11;3,12;3,13` reaches coverage `0.9514170`. Official trace rerun solved `1/1`; relation audit reports support root `58`, depth `3`, and edges `58->68`, `68->72`, `72->28`.
- Counter-evidence: t127's shorter right closure was a capacity proof only; it lost the bridge-victim contract to `60->28`. The t129a delay is the reason this branch is now acceptable as a walkthrough checkpoint.
- Boundary: t129a is still not final A/Hard. It remains process `B/B`, hard class `LocalEasy`, local/nearOuter `7/7`, antiLocal `0.2`, and CUD p20 `5.5`.
- Implementation implication: next work should harden local/near-outer rhythm from t129a while preserving the restored contract. Do not go back to endpoint closure probing unless the hardening step consumes the t129 right-slot structure.

# SGP Read-Demand Decision: FrontierContract Is Positive But Still Too Continuous - 2026-07-02

- Decision: keep `SGPPressureReadDemandV1FrontierContract` as a partial positive generation-time route, but do not treat it as the final read-demand hardening answer.
- Evidence: the lane can generate solved B/A candidates and low-choice switch/composite windows without late arrow-flip repair. The best review row has a `2 1 1 1 2 2 1 1` low-choice window with region/direction/frontier-break signals.
- Counter-evidence: human playtest still reads as continuous clearing, just moving bottom-to-top. Exact strict `choiceChokeAfterLocalFrontierBreakCount` remains `0`, and the desired "small patch drained -> forced remote switch with only 1-2 choices" moment is not reliably present.
- Operational rule: future read-demand review should rank composite/switch windows and max choices, but acceptance must require a real interruption window by human feel or an explicit scheduled trace contract. Do not promote broad FrontierContract pools into production; only curated A/B rows may be mounted for review.
- Next technical direction: move from scalar head scoring to a scheduled frontier/window DAG. The generator should reserve/assign at least one interruption window before dense fill, with owner-region order, blocker ownership, and post-drain next-frontier width as first-class state.

# Generated-Root WBP Decision: Current Walkthrough Is Family-Portable, Not Yet Root-General - 2026-07-02

- Decision: treat the current delay72 tail-split closure as a real reusable closure primitive inside the t126/t129 high-coverage skeleton family, but do not claim general alternate-root `0.95+` capability yet.
- Evidence: t131b applied the same t129a operation to four sibling bases and official trace solved all four at coverage `0.9514170`. Relation audit preserved `58->68->72->28` in all four and found no `60->28` steal.
- Boundary: the sibling rows still share the same underlying skeleton and remain `B/LocalEasy` with local/nearOuter `7/7`. This proves walk-through portability, not final hardness or broad root generalization.
- Counter-evidence: true alternate-root t114/t104d continuation is still blocked. Both strict and relaxed scheduled bundle probes from `t114_generalization_probe_t104d_c001_fill095_open` stayed at `0.9311741` with zero accepted bundles, even with relaxed bundle size `1-3`, chain length up to `8`, and max choice `10`.
- Implementation implication: next work should add a planner-level closure/slot scheduler for the `0.90 -> 0.95` phase on true new roots. Continuing to optimize t129/t126 details is useful only after we decide whether to first generalize closure capacity or chase final A/Hard on the current family.

# Generated-Root WBP Decision: Alternate-Root Closure Needs Earlier Slot Scheduling - 2026-07-02

- Decision: treat t133 as a near-closure breakthrough and a late-repair boundary, not as a final accepted route. The true alternate-root t114/t104d board can official-solve at `0.9493927`, but current late operators cannot cross `0.95`.
- Evidence: direct short new-chain closure from t114 and from the +9 near-closure rows has `cleanOk=0`; the available short empty components all become Greedy-unsolved if cut as late chains.
- Evidence: multi-tail extension proves the right mechanism class exists: extending existing semantic chain bodies yields 4 official-solved rows at `0.9493927`. These rows keep the board playable but remain `B/LocalEasy`, so they prove closure capacity rather than difficulty.
- Boundary: adding the 10th cell by late multi-tail extension, single-cell extension from +9, or targeted tail-split closure around the two residual components failed. The best +10 frontiers reach `0.9514170` geometrically but deadlock under Greedy.
- Implementation implication: the next root-general planner must reserve or assign the last 1-2 coverage cells before the board reaches the `0.949` near-full state. The final cells should be part of the whole-board cell responsibility plan, with release owner and escape-ray effects known before chain cutting.
- Operational rule: do not repeat broad tail-split brute force for t114/t104d unless a new scheduler narrows the candidate set by planned release/target ownership. Blind final repair wastes time and has already hit the relevant boundary.

# Generated-Root WBP Decision: Tail-Only Scheduling Is Not Enough For t104d - 2026-07-02

- Decision: do not treat exhaustive existing-tail path scheduling as the missing root-general closure solution. It is a useful diagnostic, but t104d still needs earlier basin ownership or recut planning.
- Evidence: t134 enumerated all short tail paths from `t114_generalization_probe_t104d_c001_fill095_open`: `10` eligible chains, `15` path options, and `31` exact `+10` combinations to reach `0.9514170`. Accepted rows: `0`.
- Evidence: all exact `+10` assignments deadlock under Greedy after only `37-38` removed chains, with stable blockers around `7->40`, `12->40`, `40->26`, and `27->42`. This is a structural basin lock, not a missed tail ordering.
- Boundary: t133's `0.9493927` official-solved rows remain a meaningful near-closure proof. The negative t134 result only rejects tail-only closure to `0.95+` on this alternate root; it does not reject the generated-root WBP direction.
- Implementation implication: the next root-general route should schedule the chain40/42 basin earlier, likely by reserving a non-tail release/recut slot or changing a semantic owner assignment during the `0.90 -> 0.94` phase. Re-running final tail extension, final new-chain closure, or unconstrained tail-split brute force is now a known dead end for this geometry.

# ScheduledBreak Needs Official Step Proof Before Review - 2026-07-02

- Decision: for read-demand hardening, a candidate is not considered a true break unless official trace shows `choiceChokeAfterLocalFrontierBreakCount >= 1` or equivalent step diagnostics with `choices <= 2`, region/frontier change, and `frontierHardBreakAfterChosen=True` after a local patch drains.
- Evidence: `SGPPressureReadDemandV1ScheduledBreak` produced a positive row, `rdsb_03`, with tier `B`, avg/max `4.85/11`, window `30-34:2 1 1 1 1`, and step-level hard breaks at steps `30-32`. This matches the intended interruption signal better than earlier ChokeSwitch/FrontierContract packs.
- Boundary: one positive row out of twelve is not量产. Keep ScheduledBreak isolated and use it as a proof target; do not merge it into PSG/PSG-derived production gates until hit rate and human playtest improve.
- Next accepted route: make scheduled old-frontier removal a first-class generation contract, not only a scoring bonus. A good next smoke should require at least one official after-local frontier break and keep max choices near `<=12`.

# SGP Read-Demand Decision: Choice Peak Compression Is A Hard Gate, Not A Cosmetic Score - 2026-07-02

- Decision: for ScheduledBreak/read-demand hardening, `maxChoices` and concentrated local clearing must be controlled as acceptance gates, especially around the interruption window. A candidate with a low average choice count can still feel easy if one local patch opens too many simultaneous removals.
- Evidence: the old ScheduledBreak review row already had a valid low-choice window, but official max choices were `11` and source early choices were `44.4/119`; the rebuilt ChoiceCompress row keeps the break signal while official avg/max choices improve to `4.08/7`.
- Evidence: source-side pressure also moved in the desired direction: early choices `25.0/72`, chain-choice wave `4.1/8`, portable openers `2`, and official after-local frontier breaks `2`.
- Boundary: this is not production-yield solved. The hard gate selected `1/12` rows. The next route should increase hit rate and enforce forced area-switch/old-frontier removal as a planned contract, not keep tightening scalar penalties until yield collapses.

## ScheduledBreak ChokeWindow Gate - 2026-07-02

- Decision: ScheduledBreak forced-switch detection uses a new source-side `ChokeWindow` metric, but it does not replace the global `SwitchFlow` gate.
- Reason: smoke11 showed multiple failed candidates have strong low-choice windows (`1/2` choices with switch/frontier/new-region evidence), including the old row 3 negative-feel sample; row 3 must remain rejected by global region/local continuity (`RegionRun=5`) despite window strength.
- Accepted gate shape: a kept ScheduledBreak candidate must pass `ChoiceGate`, global `SwitchFlow`, and `ChokeWindow`. `ChokeWindow` proves a local "only 1-2 choices then forced switch" moment; `SwitchFlow` prevents whole-board regional sweeping.
- Next tuning target: reduce choice peak/opener pressure (`maxChoices` ideally `<=8`, current row 11 official max `9`) and `localPatchSolveRunMax` (current `6`) by changing generation pressure/placement, not by loosening gates.

## RegionFrontierReplay Must Pair Break Signal With Continuity Risk - 2026-07-02

- Decision: do not use a static/coarse region graph or ChokeWindow alone as the next ScheduledBreak gate. Use dynamic owner-region frontier replay and explicitly pair break evidence with continuity-risk fields.
- Evidence: first replay comparison classifies both `rdsb_03` and `rdsb_11` as having real break signals, but both retain continuity risk in different ways. `rdsb_03` has lower choices and more remote-narrow breaks, yet still has dependency/region-collapse continuity risk; `rdsb_11` has stronger switch feel but still has localPatch/switch-continuity and one post-break explosion.
- Implementation rule: a future kept candidate should not just prove `frontierHardBreak + narrow reopen`; it must also limit post-break explosion, dependency follow run, region-collapse run, and localPatch run. Otherwise the player can still feel continuous sweeping after a nominal break.

# Generated-Root WBP Decision: Move To Region-First Constraint Projection - 2026-07-02

- Decision: t137/t138 local run-break work has reached a local patch ceiling. The next Generated-Root WBP lane should use region-first constraint flow as the planning primitive, then compile to seedState/frontier reservations before chain cutting.
- Decision: late-owner trace edges are evidence, not direct generation instructions. Map edges such as `67->79`, `40->9`, and `83->5` to region pressure, delay, choke, guard, and cross-basin duties instead of trying `edge -> edge` or `edge -> chain` replay.
- Evidence: t140a projects `144` t138 cell-demand rows into `9` stable region-duty rows and `16` seedState-like rows before chain generation. Top-region Jaccard remains `1.000` under +/-10% role-weight perturbations.
- Boundary: the current t138 demand is `19x26`, while the current generated-root input `geosupply_sched_root10_from_40eb0da7_r1_c038` is `23x30`; therefore t139's direct demand smoke is diagnostic-only and cannot be treated as a valid root planner baseline.
- Evidence: t140b finds a compatible `19x26` generated-root input, `root154_from0700_tail0_c01`, where the same projection yields `16/16` planner-ready seedStates. This means the next step can test reservation integration without solving coordinate normalization first.
- Implementation implication: before V12 materialization, feed compatible seedStates into frontier reservation and require forward evidence: nonzero reservations, ready seedStates, and early trace/topology divergence before accepting any chain-cut candidate. Add coordinate/frame normalization only when reusing a differently sized root is necessary.

# Generated-Root WBP Decision: SeedState Materialization Channel Is Viable But Needs Solved-Prefix Expansion - 2026-07-02

- Decision: keep the t140/t141 region-duty route as the current Generated-Root WBP baseline. The viable pipeline is `region duty -> seedState reservation -> materialized chain-plan seed rows -> V12 strict validation`, not demand penalty alone and not late local repair.
- Evidence: t141a maps `16/16` ready seedStates on generated root `root154_from0700_tail0_c01` to `44` concrete empty reservation demand cells; t141b proves V12 loads those cells and scores overlapping options.
- Evidence: t141c/t141h/t141j prove materialization works: V12 reconstructs materialized seed rows through `--chain-plan-seed-csv`, accepts strict solved-prefix seed states, and writes legal candidate assets. Best strict prefix currently has `3` planned seedState chains, coverage `0.7388664`, Greedy `True/3.966/10`, and demand overlap weight `752.152`.
- Boundary: this is not the final target. Coverage is still far from `0.95+`, contracts are still mostly choke-region seed chains, and naive `4`-chain region assembly is Greedy-unsolved. A broad unbounded 3-chain search also timed out, so future expansion must be staged and bounded.
- Implementation implication: next work should make solved-prefix expansion first-class: add release-impact-safe pruning, region/basin diversity constraints, and incremental official/Greedy gates while growing from the accepted 3-chain prefix. Do not treat the old V12 `cell_demand` penalty path as sufficient, and do not continue brute-force group search without pruning.

# Generated-Root WBP Decision: t142 Front-End Works, Coverage Layer Must Be Replanned - 2026-07-02

- Decision: the Generated-Root WBP front-end is now viable through `region-duty -> ordered seedState materialization -> V12 chain-plan seed -> official trace`. The current accepted backbone is the t142h exact-5 seedState route on generated root `root154_from0700_tail0_c01`.
- Evidence: t142f exact-4 and t142h exact-5 both preserve root fingerprint `5f26bf2d9d40c90a`, pass V12 strict Greedy, write semantic planned relations, and replay in official trace as `4/4 A`. t142h has `5` short/medium semantic chains, coverage up to `0.7510121`, openers `3`, max choices `6-7`, localPatch `3`, and outerExit `1`.
- Decision: do not continue trying to reach `0.95+` by adding more current seedState groups. Current one-region-one-seedState capacity is effectively capped at `5` viable regions; the next work must introduce a coverage-layer option supply generated against the accepted causal backbone.
- Evidence: r3c2 is a negative boundary (`singleGreedySolved=False`, depth-4 exact-prefix rejects by Greedy deadlock), while r3c0/r3c1 currently produce no materialized options. Old V12 carrier extension and cluster checks from the 5-chain backbone also rejected all +1 attempts by Greedy unsolved, loop risk, or overlap.
- Implementation implication: the next planner stage should compile coverage duties from the t142h backbone before chain cutting, with protected escape/first-hit contracts and official trace validation. Reusing old carrier/cluster options as a late coverage patch is a diagnostic only, not the main path.

# Generated-Root WBP Decision: t143 Coverage Layer Is Viable But Single-Layer Owner-Hit Caps Near 0.82 - 2026-07-02

- Decision: keep the t143 direction as the current continuation: a generated-root causal backbone plus a planned coverage-duty layer is one planner control surface, not two unrelated placement systems. The backbone remains the immutable causal core; coverage chains must declare old-owner release contracts and preserve root identity.
- Evidence: `Build-GeneratedRootWBPV12BackboneCoverageLayerV1.py` formalizes the layer and reproduces the owner-hit probe in a root-preserving manifest. t143g on `t142h_root154_seedstate_exact5_orderA_strict_smoke_c004` adds `12` short coverage chains / `36` cells, raising coverage from `0.7489879` to `0.8218623`.
- Evidence: official trace for t143g top4 remains `4/4 solved`, process `A`, avg/max choices `2.49/6`, outerExit run `1`, localPatch run `3`, structuredHardnessV21 `0.745`, and frontierDrainRemoteChokeCount `9`. This proves the coverage layer can add substantial cells without collapsing into LocalEasy or outer sweep.
- Boundary: old-owner-hit coverage supply by itself is not enough for the final `0.95+` goal. On this backbone the practical single-layer ceiling is currently about `0.82`; reaching `0.95+` requires either a second-stage region/coverage supply, earlier recut/reservation of coverage duties, or broader source-basin planning before final chain cutting.
- Risk/metric note: t143g improves coverage and keeps process A, but some human-read quality metrics drop from t142h (`hardStructureV3Score ~0.32`, `solveTraceQualityScore ~0.784`, collapse risk `~0.312`). The next layer should include switch/choke quality in scoring, not only coverage and low choices.
- Operational rule: use `--selection-mode greedy` for t143 V1 throughput. Beam mode currently runs for small smoke but times out on 12-chain searches until option/Greedy evaluation is cached or staged.

# Generated-Root WBP Decision: t144 Unified Duty Graph Replaces Post-Backbone Filler As Current Baseline - 2026-07-02

- Decision: t144 is now the preferred Generated-Root WBP continuation. The accepted route is `root input -> unified duty scheduler -> seedState duty + coverage basin duty selection -> single materialization -> official trace`. Do not treat t143 as the default production path; t143 remains a diagnostic proof that coverage duties are mechanically viable.
- Evidence: `Build-GeneratedRootWBPV12UnifiedDutyGraphSchedulerV1.py` starts from the generated root asset via `rootPath`, not from a prebuilt t142h backbone asset. It selects a seedState group and coverage bundle in one scheduler and emits duty/cell-plan CSVs before writing candidate assets.
- Evidence: t144a reaches coverage `0.8238866` with `5` seedState chains and `12` coverage duties, official trace top4 `4/4 A`, avg/max `2.65/6`, outerExit `1`, localPatch `3`, structuredHardnessV21 `0.759`, solveTraceQuality `0.814`, and frontierDrainRemoteChokeCount `8`.
- Anti-post-hoc proof: t144a candidate rows carry `preMaterializationDutyCommit=1`, `dutyGraphMode=unified_seedstate_coverage_v1`, and full-board cell-plan assignment. The selected seedState groups differ from t143g's fixed t142h c004 source, so this is not identical backbone plus extra chains.
- Boundary: t144 is still a V1 control-surface proof. It does not solve the `0.95+` gap; single-layer old-owner-hit coverage still caps near `0.82`. The next technical problem is coverage basin capacity and future release reservation, not more same-style owner-hit bundles.
- Implementation implication: next t145 should add region/basin coverage planning before option materialization: assign remaining empty regions to coverage basins with release windows, then let scheduler select options that satisfy basin capacity and future-release diversity. Keep official trace as validation, but require the pre-materialization duty/cell-plan evidence to remain nonzero and explainable.

# Generated-Root WBP Decision: Multi-Wave Owner-Hit Improves Coverage But Caps Near 0.868 - 2026-07-02

- Decision: keep t145 multi-wave unified duty graph as a positive architecture proof, but do not keep drilling same-style owner-hit waves as the main route to `0.95+`.
- Evidence: t145e plans seedState plus coverage waves `12,4,4,1` before materialization and reaches coverage `0.8684211`, official top4 `4/4 A`, max choices `5`, outerExit `1`, local/nearOuter `3/3`, and root fingerprint `5f26bf2d9d40c90a` preserved.
- Evidence: t145l with broader beam increases planned prior-wave release edges from `1` to `2`, proving the scheduler changes dependency topology, but coverage remains capped at `0.8684211`.
- Boundary: after the first 12-chain owner-hit wave, a second 12-chain wave has insufficient valid bundle capacity; after `12,4,4,1`, tail extension finds only one safe cell and extra `+2` or `+1+1` waves fail or time out. Relaxing later-wave single-chain solved filtering produces many options but no valid bundle.
- Implementation implication: the next complete-level attempt needs a broader coverage-basin primitive before chain cutting: area/basin ownership, body extension, recut/reservation, or a root/seedState layout that creates future coverage capacity. It should still be one scheduler and one materialization, not post-hoc filler.

# Generated-Root WBP Decision: Split Official Trace Gate From Difficulty Verification - 2026-07-02

- Decision: official trace is a hard acceptance gate, not the final difficulty truth. Use `TraceGate` for validity/process health and a separate `DifficultyVerify` verdict for real hard-read evidence.
- TraceGate answers: official solved, process tier, choice peak/openers, coverage threshold, root preservation, and pre-materialization duty commit. A candidate failing this gate is not eligible for difficulty review.
- DifficultyVerify answers: whether low-choice windows are meaningful, whether they force region/direction/jump changes, whether local/outer sweeping is controlled, whether remote dependencies/chokes exist, and whether hard structure has support closure, dual gate, bridge, or cross-basin contract evidence.
- Do not optimize only for official `A`, low average choices, or coverage. Those can produce LocalEasy false positives. A true hard candidate needs both tight choices and structural contract evidence.
- New class interpretation: `Fail` means invalid or weak difficulty; `Review` means some readable pressure but not enough hard evidence; `HardPotential` means playable hard direction with blockers; `TrueHardCandidate` requires non-LocalEasy structure plus strong remote/anti-flow evidence.
- Evidence: t145e Cov868 rows all pass TraceGate but verify only as `HardPotential` (`0.707-0.716`), blocked by `LocalEasyStructure`, weak structural contract, no qualified support closure, no strict dual gate, weak remote choke count, and sweep-continuation risk. This confirms the split prevents treating official `A` as final hard proof.

# Generated-Root WBP Decision: Coverage Gap Is Early Space Debt, Not Just Late Fill Search - 2026-07-02

- Decision: the current `0.868 -> 0.95` gap should be treated as early space-debt planning, not as a late coverage-combination problem.
- Evidence: t145e Cov868 has `65` empty cells and needs `41` more cells to reach `0.95`; EarlySpaceDebt V1 estimates only `29` safe fill capacity, leaving `12` debt cells. `30/65` empty cells are high-risk, `18` are boundary cells, and `27-28` are dangerous outer-fill roles.
- Evidence: t142h seedState front-end already has large debt (`coverage 0.7489879`, needs `100`, safe capacity `64`, debt `36`), t144a after the first 12-chain coverage layer still has debt `22-23`, and t145e reduces this to `12` but cannot eliminate it.
- Evidence: late-wave option supply collapses before the final wave. In t145e/t146c, wave 4 starts around coverage `0.8643725` with `rawOptions=1`; by then the scheduler cannot choose a different space shape.
- Negative boundary: adding space-debt scoring inside the existing t145 coverage-wave scheduler reproduces the same `0.8684211` candidates and the same debt. This proves the intervention is too late if applied only during coverage-wave bundle ranking.
- Implementation implication: t147 should move outer/high-risk cell duties before or alongside seedState/frontier planning: classify cells as safe body buffer, guard/choke-only, intentional empty, or danger fill before owner-hit coverage options are enumerated. The generator should not leave a concentrated outer/debt tail for the last waves.

# Generated-Root WBP Decision: Budget-Aware Options Improve Rhythm But Not Structural Contract - 2026-07-02

- Decision: budget-aware coverage-option scoring is useful but not sufficient. It can improve remote choke/anti-flow metrics, but it does not reduce the `0.95` space debt or create true hard structure by itself.
- Evidence: t147b keeps coverage `0.8684211`, official trace `2/2 solved/A`, avg/max choices `2.35/5`, low2 rate `0.605`, after-local frontier breaks `5`, remote choke count `4`, remote choke score `0.916`, local/nearOuter runs `3/3`.
- Evidence: DifficultyVerifier improves from t145e `0.707-0.716` to t147b `0.730-0.731`, and blockers shrink from `LocalEasyStructure;weak_structural_contract;no_qualified_support_closure;no_strict_dual_gate;weak_remote_choke_count;sweep_continuation_risk` to `LocalEasyStructure;weak_structural_contract;no_qualified_support_closure;no_strict_dual_gate`.
- Boundary: t147b still has the same space debt as t145e (`need 41`, safe capacity `29`, debt `12`, high-risk empties `30`, boundary empties `18`) and remains `HardStructureV3=LocalEasy` with no qualified support closure or strict dual gate.
- Negative subcase: t147a's stronger danger-cell rewards dropped coverage to `0.8562753` and worsened debt to `14`, showing that budget rewards can select short guard-like chains that feel tighter but do not solve coverage.
- Implementation implication: next work should combine early cell budget with structural-contract generation: support closure, dual gate, bridge pressure, and cross-basin required edges must become planned duties before coverage waves. Do not keep tuning budget scalar weights alone.

# Generated-Root WBP Decision: t148 Structural Contracts Need New Materializer, Not BCL Scoring Alone - 2026-07-02

- Decision: the next WBP route should add a structural-contract reservation/materializer before coverage waves. Do not try to solve t147b by only increasing BCL owner-hit scoring.
- Evidence: t148 relation audit on t147b c001/c002 confirms `LocalEasy` is not caused by missing trace data. The board has deep single-spine closure (`bestClosureRoot=25`, depth `4`, score `0.686`) but no qualified hub; actual cross-critical fanout roots like `50` and `33` stay shallow (`depth=2`, scores about `0.357/0.355`).
- Evidence: targeted late support-closure bridge probes from t147b c001 produced `0` candidates for root50 release `46/51`, root33 release `30/54`, root25 release `25/77`, root77 release `22`, and root22 release `21`. Rejects are mainly `wrong_release_owner`/`bad_second`, with some `greedy_unsolved`; the near-full board has already spent the useful ray/entry space.
- Evidence: `StructuralContractProjectionV1` compiles the gap into `34` forward duties: `coverage_space_debt`, `strict_dual_gate_sync`, `planned_bridge_pressure_missing`, `support_hub_extend`, `deep_closure_needs_branch`, and `anti_local_edge_rewrite`.
- Evidence: uncapped `StructuralContractOptionAuditV1` over root+seedState prefix enumerated `780` current BCL options. High-priority support owners `28/42/76`, `46/51`, and `3/70` are not visible to owner-hit grammar; dual-gate owners `69/20/48` are visible only as `5` single-unsolved options; only lower-priority deep-closure `21/40` has one solved visible option.
- Implementation implication: the next generator primitive should create/reserve structural chains that can block or delay a target owner's escape ray while being released by an earlier owner, plus reserve space for support child-of-child closure. BCL can remain a coverage primitive, but it cannot be the only chain grammar for hard structure.

# Generated-Root WBP Decision: Coverage Must Preserve Existing Support Closure - 2026-07-02

- Decision: treat support-closure preservation as a first-class coverage constraint. The route is not "find hard structure after coverage"; the generator must avoid spending hard-structure downstream release owners as generic coverage capacity.
- Evidence: t142h c004 already had a qualified support closure at root `50` (`50->46`, `50->51`, then `51->1`, `1->0`, `1->12`), with closure depth `3` and score about `0.802`.
- Evidence: t147b reached coverage `0.8684211` but used `1/0/12` as coverage release owners. Relation audit shows root50 degraded to depth `2`, score `0.357`, and `conveyorHeavy`, while the board stayed `LocalEasy`.
- Evidence: t148k forbids coverage release owners `0,1,12` and preserves root50: official trace `3/3` solved, best process `A/A`, coverage `0.8340081`, relation audit root50 depth `3`, score `0.807`, `crossCrit`, and overall class `MediumStructure`.
- Boundary: hard protection caps the current owner-hit multi-wave route around `0.834`; t148m/t149c/t149e cannot build wave3 even after later waves reopen the protected owners. This proves the conflict is structural, not a missing scalar weight.
- Implementation implication: future WBP coverage must introduce a structural coverage outlet such as protected-body expansion, support-aware recut, or parent-release blocker/bridge materialization before generic BCL waves. Do not solve this by post-hoc filler, late repair, or just making coverage chains longer.
## Campaign500 Planning Method Baseline - 2026-07-02

- Decision: use `Campaign500 Slot Spec V2` as the stable planning baseline before resource assignment.
- The fixed method is: keep the original template skeleton, define each slot by 10-level micro-loop role and 50-level macro arc, plan with `targetEffectiveLoad` instead of raw chain count, then match/generate candidates by duty and required evidence.
- Shape slots are visual/perspective wrappers and hole slots are spatial anchors; neither should be used as filler for normal peak slots.
- Front20 is treated as hand-authored: L1 remains tutorial, L10 is a light quiz, and L19 is the first mini-boss around effective load `56-66`.
- Current choke/key-arrow review candidates are classified as `NormalPlus/ReadCheck-lite`, not hard/peak supply. True hard+ still requires forced reread, remote narrow frontier, and meaningful downstream consequence.
- Future changes should update slot fields, load bands, evidence requirements, or candidate matching against `campaign500_slot_spec_v2_locked.csv`; do not restart the whole 500-level pacing logic unless the user explicitly invalidates this baseline.

## HoleMask Decision: Nutation Is An Approved Seed Pool, Not A Replacement Generator - 2026-07-02

- Decision: for mask-crop HoleMask production, use updated Nutation outputs as a whitelisted external seed pool under `Assets/ArrowMagic/SOData/Levels/Seeds/NutationCandidatePool`, not as a wholesale replacement for seed crop + repair + Greedy validation.
- Reason: the product rule is still "crop/mask an existing seed, repair, refill, and Greedy-validate"; Nutation direct procedural logic has useful long-chain/flow priors but does not preserve arbitrary mask shape by itself.
- Evidence: after copying `106` Nutation seed assets into the experiment project and rerunning the high-chain 100-150 batch, Nutation seeds appeared in preview Top8 on `3/5` masks but did not enter final accepted Top3. Current R1/R2 seeds still dominate by fill, chain target fit, and deep-run score.
- Implementation implication: the low-risk integration is correct, but quality impact needs a dedicated Nutation deep-run lane or reserved per-mask Nutation attempt. Avoid simply increasing Nutation score bonus so much that lower-quality previews displace better R1/R2 candidates.

## HoleMask Decision: Greedy + Fill + Chain Count Is Not A Production Quality Gate - 2026-07-02

- Decision: do not promote `HoleMask_HighChain_100To150_Candidates.asset` or similar rows based only on Greedy pass, fill ratio, chain count, and block-hit checks.
- Evidence: the reviewed high-chain pack passed the current technical screen (`100-150` chains, fill >= `80%`, hole/block hits `0`, Greedy-solvable) but was rejected by human review as too simple and mechanical.
- Reason: the current screen measures "can be solved" and "is dense"; it does not measure whether the solve path has real dependency, reread, choke/switch moments, or varied decision rhythm. High chain count can be fake complexity when many chains are direct exits or parallel strips.
- Required future gate: before calling HoleMask output production-ready, add quality filters for direct/outer exit ratio, initial choice count, local clearing run length, same-direction strip dominance, dependency depth, and official/Greedy trace rhythm. Human review remains the final arbiter for this family until those metrics correlate with feel.

## HoleMask Decision: Hole Terrain Works Best As A Constraint On Seed Structure - 2026-07-02

- Decision: do not treat the first `Nutation seed structure + fixed-hole terrain blocker + dependency/outer-exit gates` probe as production-ready. It is only a constraint experiment and its first review pack is rejected.
- Evidence: the direct template blocker V13 rerun accepted `0` strict Top5 rows and the debug candidate looked mechanically striped. In contrast, `RunHoleMaskNutationTerrainProbeBatch` produced `2` Greedy-valid probe levels with hole ray hits `0`, direct outer exits only `5-6`, final dependency depth `11`, and fill around `88%`.
- Human review correction: the two-level probe still looked like obvious mechanical rails/parallel strips and should be marked as a false positive. Numeric dependency/outer-exit metrics alone are not enough.
- Boundary: hole-as-terrain can cooperate with Nutation seed structure technically, but the lane is invalid until it adds visual/mechanical rejection gates for parallel strip dominance, boundary ring/rail dominance, large empty components, hole engagement, and trace/local-run rhythm.
- Implementation implication: future expansion should reserve a dedicated Nutation-terrain lane only after these visual/trace-feel filters exist. Do not add current probe rows to formal HoleMask production pools.

## Campaign500 Shape Slot Role - 2026-07-02

- Decision: shape is first a visual/perspective wrapper, not a standalone difficulty source. It plays the same slot roles as other levels: reward, release, practice, read-check, hard, or occasional bottleneck cameo.
- Calibration: current Nutation-generated shape feels slightly harder than ordinary peel, but only by a small premium. Do not use shape geometry alone to carry core hard/extreme difficulty.
- Implementation: `Campaign500 Slot Spec V2` now marks shape styles as `ShapeHosted/...`, uses shape-specific production needs such as `use_existing_shape_anchor_pool`, `partial_supply_need_shape_readcheck`, and `needs_underlying_hard_supply_shape_cameo`, and requires `underlying_non_shape_read_evidence` for shape-hosted peak slots.
- Production implication: if a shape slot is code 3/4, select or generate a real hard-read/dependency candidate and apply shape as a theme lens. Shape can occasionally guest as a bottleneck, but the proof must come from trace/read/dependency evidence.
## Generated-Root WBP: Structural Outlets Must Be Front-Loaded - 2026-07-02

- Decision: do not treat parent-release blockers or release-window interposers as late repair/fill operators on an already covered board.
- Evidence: on protected t148k (`0/1/12` release owners preserved), PRB produced `0` raw candidates; release-window interposer produced valid-looking contract attempts but all contract candidates were Greedy-unsolved after coverage was already packed.
- Positive boundary: the same release-window interposer grammar works on the earlier t142h skeleton. A `10 -> interposer -> target 56 window` candidate official-traced `A/A` and preserved root50 support closure.
- Consequence: structural outlets belong before generic coverage waves, inside a scheduler layer that allocates delay/block/dual-gate duties with spatial diversity. Late insertion can be used only as feasibility evidence.
- Current limit: single or double front-loaded interposers plus protected BCL reaches only about `0.836` coverage, with `HardPotential` but no strict dual gate. Further progress needs multi-duty scheduling, not scalar BCL tuning.

## Generated-Root WBP: Strict Dual Gate Needs Early Control-Slot Reservation - 2026-07-02

- Decision: do not keep trying to create strict dual gate by late control-chain insertion on t151h/t150j-style boards. The missing dual-gate control slot must be planned before coverage and likely before seedState materialization.
- Evidence: t151 remote-gap structural interposer keeps process `A/A`, root50 support depth `3`, and DifficultyVerify `HardPotential`, but still fails `no_strict_dual_gate`.
- Evidence: t152 added official `newParentEdges` diagnostics and used the real trace `parentOf` for dual-gate probing. It found `4` upstream-control near-pairs, all requiring target gate `5`, but `0` late materialized control candidates.
- Evidence: every gate5 control slot around `(0,14)` has incumbent debt. `0,14;0,13` is blocked by body owner `34` plus release-ray owners `14;32;41`; `0,14;0,15` is blocked by body owner `41` plus release-ray owners `15;34;54;70`. The projected slots are `8/8` debt and `0/8` clean.
- Interpretation: the failure is not "candidate search missed a chain"; it is a whole-board duty allocation failure. Coverage/root/seedState have already occupied the control body cell and release corridor needed for the gate contract.
- Implementation implication: next WBP generator work should introduce `dual_gate_control_slot` and `control_release_corridor` reservations in the early duty graph. Owner-hit BCL may fill around those reservations, but must not consume them. Late relocation/repair of t151h is a negative boundary unless used only for diagnostics.

## Generated-Root WBP Decision: Product Gate Is Hard-Core Window Plus Tail-Safe Fill - 2026-07-03

- Decision: accept the product definition `0.95+ coverage = hard-core invariant window + tail-safe fill`; do not require the whole solve trace to remain `A` if the hard-core window survives and the tail is hygienic.
- Verification split: every candidate must still output `Overall`, `Hard-Core Window`, and `Tail Hygiene`. `Overall` includes coverage/root/pre-materialization evidence; Hard-Core and Tail are separate sub-gates.
- Evidence from t172b: c038 micro-wave extensions from `0.755` to `0.830` preserve dependency entropy, region entropy, branch diversity, and spine risk within small deltas. Tail is not currently polluting the c038 hard-core; capacity is the blocker.
- Evidence from t172a/t171: root154/t142 style cross-basin and patchwork variants can reach `0.8684211` with root/preplan evidence but become tail-safe capacity poisoned near `0.864`, so blind continuation is not the right search.
- Evidence from c027: t158m has the strongest hard-core/tail shape so far (`0.9068826`, Hard-Core pass, Tail pass), but it failed product Overall due coverage below `0.95` and missing propagated root/preplan fields. Upstream t157e c005 preserves root/preplan, so the shape is useful as a capacity reference, not as a completed candidate.
- Implementation implication: next work should search for hard-core invariance and capacity together. Preferred routes are c027/maze-like capacity reproduced with generated-root product metadata, or an early reservation/rewrite operator that gives c038/root154 more safe tail capacity without spending hard-core support slots.

## Generated-Root WBP Decision: Hub/Spiral Are Negative Under Current Owner-Hit Grammar - 2026-07-03

- Decision: do not make hub-like or spiral templates the next main route while using the current owner-hit coverage grammar and `12,4,4,1` multi-wave materializer.
- Evidence: t173 same-root structure-family smoke on `root154_from0700_tail0_c01` shows patchwork and cross-basin both reach `0.8684211` with official `A/A`, Hard-Core pass, Tail pass, root/preplan true. Hub and spiral both fail at wave1 with `rawOptions=500`, `filteredOptions=500`, and `bundleCount=0`.
- Interpretation: hub/spiral are not merely under-ranked; their structure-template pressure selects option sets that cannot form a Greedy-valid first coverage bundle in this grammar. Tuning scalar rewards is unlikely to produce the needed `0.95+` product.
- Implementation implication: treat patchwork/cross-basin as the viable current-grammar structures, but acknowledge their capacity ceiling. For the next breakthrough, either transfer the c027/maze-like capacity into product-compliant generated-root evidence, or add an early reservation/rewrite primitive that increases tail-safe capacity before coverage waves.

## Generated-Root WBP Decision: c027 Clean-Tail Closure Is Capped After 0.9069 - 2026-07-03

- Decision: use c027/maze-like output as high-capacity hard-core reference, not as proof that the remaining `0.95` tail can be solved by late clean closure.
- Evidence: `t173j_c027_after0906_noopen_tail_probe` starts from `t158m_c027_c005_psclosure_after_open12_noopen_095` at coverage `0.9068826`, keeps propagated `rootPreserved=True` and `preMaterializationDutyCommit=1`, remains Greedy solved, but adds `0` chains under no-open tail constraints.
- Evidence: `t173k/t173l` official/product recheck classifies the lineage-propagated row as solved `B/B`, `CoverageIncompleteHardCore`, Hard-Core pass, Tail pass, and `Overall Fail` only for `coverage_below_product`. This removes the earlier t167 lineage blocker for c027, but the coverage gap remains.
- Evidence: the closure probe stalls before target with reject top `greedy_unsolved:65, open_cap:13`, so the missing cells are not just unsearched easy filler. Allowing easier open-tail behavior may increase coverage, but would risk product tail hygiene and player-stall meaning.
- Implementation implication: the c027 path needs earlier tail-space reservation/rewrite inside the duty graph, or a regenerated c027-like root family whose hard-core and tail capacity are planned together. Do not continue by repeatedly appending late no-open closure probes to the same `0.9069` board.

## Generated-Root WBP Decision: c027 Tail Needs Option-Level Anti-Spine, Not Blind Open Fill - 2026-07-03

- Decision: controlled open-tail is allowed as a product mechanism only when product verifier keeps Hard-Core and Tail Hygiene pass. It should be selected with anti-spine evidence, not by raw Greedy/coverage score.
- Evidence: t174 controlled open-tail raises c027 from `0.9068826` to a hygienic `0.9251012` (`t174l`) while preserving root/preplan, Hard-Core pass, and Tail pass. This proves `no-open` was too strict as a product tail rule.
- Negative evidence: the default third open `(5,0;5,1)` produces official-solved rows (`t174c/t174d/t174o/t174v`) but fails Tail Hygiene through `global_dependency_follow_run`, with `globalDependencyFollowRunMax=12`.
- Negative evidence: forcing `(5,0;5,1)` as the first open-tail chain (`t174ac`) still fails Tail Hygiene with the same global dependency-follow risk, so the problem is not solved by simply moving the same cluster earlier in the late-tail order.
- Boundary: from clean `t174l`, forbidding `(5,0;5,1)` leaves `0` accepted next-chain alternatives under wide enumeration. The cell cluster is not just badly ranked; it needs earlier duty/reservation/rewrite or a different release context before materialization.
- Implementation implication: the next generator step should promote tail option audit into a scheduler constraint: detect dirty late-open clusters, reserve or rewrite them earlier, and score against dependency-spine risk. Do not treat open-tail count alone as either success or failure.

## Competitor-Hard Decision: Direct SGP Is A Control Sample, Not The Hard Target - 2026-07-03

- Decision: direct SGP/Pressure can be used to compare full-board high-coverage shape and playable fill, but should not be treated as a completed competitor-hard route unless trace/dependency evidence improves beyond `LocalEasy`.
- Evidence: fresh `.worktrees/competitor-hard-fresh` SGP trial generated 4 solved rows at coverage `0.978-0.994`; trace/join kept 2 rows with process `A/B` and max choices `8`, but both keep rows still classify `LocalEasy`.
- Implementation implication: future competitor-hard work should borrow SGP's whole-board high-coverage fill behavior, while adding a real skeleton/core-chain dependency plan that creates remote choke/support/dual-gate evidence. Do not regress to V7-style tail fill, and do not call SGP direct hard just because coverage is high.

## Competitor-Hard Decision: Preserve Skeleton Difficulty Before Coverage Tail - 2026-07-03

- Decision: for V10/T145 skeleton handoff, prioritize preserving the hard skeleton's choice pressure and medium-structure window over maximizing coverage with late outer exits.
- Evidence: V12 high-exit SGP suffix pushed coverage to `0.822-0.835`, but official trace was `4/4 Drop` with max choices `18-20`. User playtest matched this: late coverage fill created too many outer exits and made the perimeter easy even though the mid/late skeleton still existed.
- Evidence: V12 low-exit hard-preserve suffix limits direct exits to `4`, keeps V10 prefix unchanged, and official trace gives `1 A / 3 B`, including `A/MediumStructure` at coverage `0.754`. This better preserves skeleton difficulty even with lower coverage.
- Evidence: allowing owner-hit chains to overlap prefix corridors raises coverage to `0.844-0.854` with max choices `7-8`, but official hardStructure falls to `LocalEasy` for all rows because corridor occupation shortens/linearizes the causal skeleton.
- Implementation implication: do not continue suffix-only coverage pushing. The next real generator change should front-load exit/corridor ownership and structural outlets before generic coverage waves, borrowing the t179-t181 boundary-ownership conclusion: dirty/tail boundary cells need root/seedState/reservation context, not late repair.

## Generated-Root WBP Decision: Hard-Core Window Needs Explicit SSWD Plus Protection - 2026-07-03

- Decision: `hard-core window` is now a first-class front-loaded duty (`SSWD_FRONTIER_BREAK_SUPPORT`), not a post-hoc trace label. SSWD specs are scheduler-eligible only when the source window has `hardCoreGate=Pass` and the anchor step has `frontierHardBreakAfterChosen=True`.
- Evidence: t183a showed old t182 hard-core windows were not explicit SSWD (`CoverageDutyOnly` or `TraceOnlyNoDuty`). After adding eligible SSWD scoring, t183c produced `sswdPreCommitCount=9` and Hard-Core pass at coverage `0.6842105`, proving a forward SSWD channel can create the desired window.
- Negative boundary: including t182m as SSWD evidence polluted generation because its source hard-core failed and anchor frontier break was false. Do not use failed/fuzzy hard-core windows as scheduler specs.
- Decision: post-SSWD coverage must protect the support window. Later waves that reuse SSWD release owners or basins can erase `frontierHardBreakAfterChosen` even while the board remains solved.
- Evidence: t183g 16-wave without SSWD protection raised coverage to `0.7226721` but lost Hard-Core; t183j soft protection restored Hard-Core at `0.7186235`; t183l hard protection with wider pool found outside protected options and retained Hard-Core at `0.6882591`.
- Implementation implication: the next generator stage should not blindly extend micro-waves. It needs `SSWD protection + outside-supply candidate expansion + anti-spine tail selection`; hard filtering works only if the post-SSWD candidate pool is broad enough.

## Generated-Root WBP Decision: Post-SSWD Tail Failure Is Scorer Bias, Not SSWD Failure - 2026-07-03

- Decision: keep SSWD and UDG as the same unified scheduler; fix post-SSWD tail selection with structural-divergence scoring rather than adding a second generator.
- Evidence: t183m rank audit showed the bad tail option that yields `dependencySpineDominanceScore=1.0` was ranked `#1` by the current scorer. Top9 were all `r1c0->r1c0 / releaseOwner46`, so this is a ranking bias toward spine continuation.
- Evidence: t183n/t183o added default-off tail anti-spine scoring. t183o redirected wave13 to `r1c1->r2c1` released by root owner `27`, kept Hard-Core pass, reduced local run `11 -> 5`, and reduced spine dominance `1.0 -> 0.89`.
- Boundary: t183o still fails Tail Hygiene because dependency-follow run remains `10`, and PlayerStall low-choice strength drops. Anti-spine scoring alone is helpful but incomplete; the next scoring proxy needs dependency-follow/run-break awareness, not only region/basin/owner diversity.
- Implementation implication: scale this path by making the wider candidate pool late-only and adding a dependency-follow proxy for post-SSWD tail options before attempting 16/20/24-wave coverage pushes.
