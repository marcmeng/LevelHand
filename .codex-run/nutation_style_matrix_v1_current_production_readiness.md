# Nutation Production Readiness - 2026-06-29

Source matrix: `.codex-run/nutation_style_matrix_v1_current_matrix.csv`
Representative pack: `Assets/ArrowMagic/SOData/Packs/DirectProcedural/NutationStyleMatrixRepresentativeReviewPack.asset`
Representative CSV: `.codex-run/nutation_style_matrix_v1_current_representative_rows.csv`

## Production Tiers

| Tier | Style / chain | Status | Notes |
| --- | --- | --- | --- |
| Production-ready review | LongChain / curve | strict_review_ready | 3 TraceOrderKeep rows; good staged/flow mix; suitable for first batch. |
| Production-ready review | LongChain / rail | strict_review_ready | 3 TraceOrderKeep + 1 VisualKeep; strong low-choice long-chain lane. |
| Production-ready review | LongChain / patch | strict_review_ready | 3 TraceOrderKeep; patch support works best inside long-chain topology. |
| Production-ready review | LongChain / spine | strict_review_ready | 2 TraceOrderKeep; useful as harder/deeper flavor, lower volume than curve/rail/patch. |
| Production-ready review | Peel / curve | strict_review_ready | 2 TraceOrderKeep; stable layered peel baseline. |
| Production-ready review | Peel / rail | strict_review_ready | 2 TraceOrderKeep + 1 VisualKeep; stable straight-chain peel flavor. |
| Controlled mix | Flow / curve | flow_review_ready | Simple/continuous; can seed easy pacing, keep low ratio. |
| Controlled mix | Flow / rail | flow_review_ready | Simple and more directional; good for easy rail contrast, keep low ratio. |
| Controlled mix | Flow / patch | flow_review_ready | Has some local-collapse risk; use sparingly and visually review. |
| Probe only | Hub / patch | strict_probe_ready via V5Pool | 2 TraceOrderKeep but 24/24 high_risk/local_collapse; not production standard yet. |
| Optimize | Hub / curve | needs_solve_time_control | Quality not ready; local-collapse dominates. |
| Optimize | Hub / rail | style_proof_only | Shape proof only; quality not ready. |
| Optimize | Maze / curve | needs_solve_time_control | One visual row only; quality and yield not ready. |
| Optimize | Maze / patch | needs_solve_time_control | Local-collapse/high-risk; not production. |
| Optimize | Maze / rail | style_proof_only | Shape proof only; quality not ready. |
| Optimize | Peel / patch | strict_near_miss | Good structural direction but currently 0 TraceOrderKeep. |

## Suggested First Batch Mix

- LongChain: 45-55% of Nutation batch.
- Peel: 25-35%.
- Flow: 10-20% for easier pacing, gradually reduce if体感 too simple.
- Hub/Maze/PeelPatch: 0% formal production for now; keep as review/prototype samples only.

## Efficiency Notes

- Flow / Peel / LongChain lanes are currently suitable for batch expansion.
- Hub V5 Pool is expensive: first pool was 24 candidates and took about 30 minutes for Unity generation, then trace-only took about 13 minutes; it still outputs only 2 probe keeps with local/directional warnings.
- Maze curve has low yield; Maze patch/rail and Hub curve/rail are not ready for production efficiency or quality.
- Next optimization priority: Hub V5 LocalBreak, then PeelPatch, then Maze solve-time control.
