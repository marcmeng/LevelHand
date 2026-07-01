# Nutation / Campaign Candidate Sync - 2026-07-01

This handoff summarizes the candidate outputs that are ready to sync to another Codex/GPT conversation. Paths are relative to `F:\Unityproject\ArrowLevel-Hand` unless noted.

## Current Priority Outputs

| Line | Status | Count | Pack | Manifest / Report | Notes |
| --- | --- | ---: | --- | --- | --- |
| Nutation HubMixed Strict30 Refill | review-ready / current mounted worktree demo | 30 | `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationHubMixedV1Strict30RefillProductionKeepPack.asset` | `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_mixed_v1_strict30_refill_production_keep.csv` | Latest HubMixed pack after secondary refill. Best current answer to the outer-hole problem. |
| Nutation LongChain Strict50 | strict-review-ready | 50 | `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationLongChainStrict50Pack.asset` | `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_longchain_strict50.csv` | Selected from Candidate80 + Holefix S1/S2/S3. LongChain style review pool. |
| Nutation LongChain Reserve10 | backup candidates | 10 | `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationLongChainStrictReserve10Pack.asset` | `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_longchain_strict_reserve10.csv` | Next-best reserve rows for LongChain bookkeeping or replacement. |
| Campaign500 Normal Full V1 Strict152 | production-quality normal set | 152 | `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500NormalFullV1ReviewPack.asset` | `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Reports/Campaign500/NormalFullV1/campaign500_normal_full_v1_production_strict_keep.csv` | 152 unique normal slots, all TraceOrderKeep. Main project also has copied packs/reports at the same relative `Assets/...` paths. |
| Campaign500 HardGate Until0910 | timeboxed final-preview surface | 145 replacements / 500 preview | `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardGateUntil0910V1FinalPreviewPack.asset` | `.codex-run/campaign500_hardgate_until0910_v1.csv` | No-quality-drop hard-gate replacement truth for that timebox. Main Demo points to this preview per project memory. |

## HubMixed Refill Details

- Worktree: `.worktrees/nutation-hub-maze-mixed`
- Branch: `codex/nutation-hub-maze-mixed`
- Final pack GUID: `da8a0f3e4d71f4f41a3a1ac875059c77`
- Source pool report: `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_mixed_v1_strict30_refill_pool_report.csv`
- Static shortlist: `.worktrees/nutation-hub-maze-mixed/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_hub_mixed_v1_strict30_refill_shortlist_report.csv`
- Official trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/nutation_hub_mixed_v1_strict30_refill_shortlist1_metrics.csv`
- Local9 joined summary: `.worktrees/nutation-hub-maze-mixed/.codex-run/nutation_hub_mixed_v1_strict30_refill_shortlist1_local9_summary.md`
- Key result: `30/30` solved, visual pass, and STS keep candidate.
- Refill impact: average coverage about `0.961`; max empty component average `5.0`; outer empty cells average `6.033`; localPatch avg/max `7.8/9`.
- Tradeoff: STS average dipped slightly versus pre-refill, but local-collapse and choices remained acceptable for the visual refill goal.

## LongChain Strict50 Details

- Worktree: `.worktrees/nutation-peel`
- Branch: `codex/nutation-peel`
- Strict50 pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationLongChainStrict50Pack.asset`
- Strict50 GUID: `46de8f0720f60854ba75a7a8615864e1`
- Strict50 manifest: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_longchain_strict50.csv`
- Strict60 bookkeeping manifest: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Reports/DirectProcedural/nutation_longchain_strict60.csv`
- Strict60 bookkeeping pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationLongChainStrict60Pack.asset`
- Strict60 GUID: `a39d5f081f1f4a1aaa0f643f67c3944d`
- Reserve10 pack: `.worktrees/nutation-peel/Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationLongChainStrictReserve10Pack.asset`
- Reserve10 GUID: `b512f2a2510148cda13496fdbf7b0b11`
- Detailed LongChain sync doc: `.worktrees/nutation-peel/.codex-run/nutation_longchain_strict60_sync_handoff.md`
- Summary/audit: `.worktrees/nutation-peel/.codex-run/nutation_longchain_strict50_summary.md` and `.worktrees/nutation-peel/.codex-run/nutation_longchain_strict50_audit.csv`
- Strict50 result: `50` strict rows; `StrictA=35`, `StrictB=15`; chain mix `spine=20`, `rail=11`, `curve=11`, `patch=8`; coverage avg `0.9471`; maxChoices avg/max `8.42/11`; STS avg `0.8791`; collapse avg `0.1767`; maxHole avg/max `4.86/9`.
- Reserve10 result: `10` backup rows; all `StrictB`; chain mix `spine=4`, `patch=4`, `rail=1`, `curve=1`; coverage avg `0.9455`; maxChoices avg/max `9.80/11`; STS avg `0.8478`; collapse avg `0.2339`; maxHole avg/max `6.40/9`.
- Strict60 bookkeeping result: `60` total rows; `StrictA=35`, `StrictB=25`; chain mix `spine=24`, `rail=12`, `curve=12`, `patch=12`; coverage avg `0.9468`; maxChoices avg/max `8.65/11`; STS avg `0.8739`; collapse avg `0.1862`; maxHole avg/max `5.12/9`.
- Current `.worktrees/nutation-peel` Demo is mounted to Reserve10 for reviewing the newly added backup levels.
- Caveat: LongChain visual feel still needs manual review before treating it as broad normal production style.

## Campaign500 Normal / HardGate Details

- Normal Full V1 worktree: `.worktrees/nutation-flow-peel-production`
- Final strict manifest: `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Reports/Campaign500/NormalFullV1/campaign500_normal_full_v1_production_strict_keep.csv`
- Review pack: `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500NormalFullV1ReviewPack.asset`
- Production strict pack: `.worktrees/nutation-flow-peel-production/Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500NormalFullV1ProductionStrictKeepPack.asset`
- Main project copies exist under `Assets/ArrowMagic/SOData/Reports/Campaign500/NormalFullV1/` and `Assets/ArrowMagic/SOData/Packs/Campaign500/`.
- HardGate preview pack: `Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardGateUntil0910V1FinalPreviewPack.asset`
- HardGate truth CSV: `.codex-run/campaign500_hardgate_until0910_v1.csv`
- HardGate export folder: `Exports/Campaign500_HardGateUntil0910_20260701_0910/`
- StrictUntil0910 export folder is audit-only: `Exports/Campaign500_StrictUntil0910_20260701_0910/`; do not treat it as the no-quality-drop production surface.

## Recommended Sync Order

1. Sync HubMixed Refill30 first if the other conversation is focused on Hub/Maze/Hole refill quality.
2. Sync LongChain Strict50 + Reserve10 if the other conversation is focused on style/language candidate review.
3. Sync Campaign500 Normal Strict152 and HardGate only if the other conversation is coordinating campaign replacement or final preview assembly.

## Boundaries

- Do not overwrite PSG core with Nutation changes.
- HubMixed Refill30 is a review-ready candidate pack, not yet a committed canonical production lane.
- Flow lanes remain controlled/easy mix or review/noise lanes, not strict normal production by default.
- Maze mixed is still diagnostic; current useful Hub/Maze output is HubMixed Refill30, not Maze production.
- The repository is dirty with many pre-existing generated assets and reports; do not clean or revert unrelated changes during sync.
