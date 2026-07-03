# Competitor Hard Tryout - ScheduledBreak Loose12 V1

Date: 2026-07-02

Scope: first practical tryout after `Competitor Hard Level Landing Plan V1`. This uses the existing isolated ReadDemand/ScheduledBreak lane instead of changing the main project generator.

## Review Entry

- Worktree: `.worktrees/read-demand-hardening`
- Demo scene mounted pack: `.worktrees/read-demand-hardening/Assets/ArrowMagic/Scenes/Demo.unity`
- Active pack: `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Packs/DirectProcedural/SGPPressureReadDemandV1ScheduledBreakLoose12Pack.asset`
- Pack GUID: `cef3a830c4aa0bb41ba718b083d77103`
- Source report: `.worktrees/read-demand-hardening/Assets/ArrowMagic/SOData/Reports/DirectProcedural/sgp_pressure_read_demand_v1_scheduled_break_loose12_report.csv`
- Official trace summary: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/codex_try_scheduled_break_loose12_1_summary.md`
- Official trace metrics: `.worktrees/sgp-rhythm-lab/Assets/ArrowMagic/SOData/Reports/SGPRhythmLab/codex_try_scheduled_break_loose12_1_metrics.csv`

## Candidate Read

| Level | Role | Source shape | Source chains / coverage | Official process | Key trace evidence | Judgment |
| --- | --- | --- | ---: | --- | --- | --- |
| `rdsbl12_01...lock_buckle_b1_01` | primary positive | `20x28` dense lock-buckle | `57 / 0.964` | `A` | avg/max choices `4.58/10`, initial openers `4`, frontier drain remote choke `4`, after-local frontier break `1`, local/near/same-region runs `3/3/3` | Best first review sample. Closest to the desired competitor-like hard-read feel. |
| `rdsbl12_05...lock_buckle_b1_05` | negative/control | `21x30` denser lock-buckle | `70 / 0.963` | `Drop` | avg/max `6.53/13`, openers `13`, remote choke `5`, after-local frontier break `0` | Looks pressure-rich but likely too open/hot at the start. Use as opener-count failure reference. |
| `rdsbl12_10...core_burst_b1_10` | negative/control | `20x28` core-burst | `53 / 0.957` | `Drop` | avg/max `7.72/14`, remote choke `6`, after-local frontier break `0`, local run `4` | Has choke signals, but process curve is too hot and peak choices are too high. |
| `rdsbl12_11...dense_weave_b1_11` | visual/control | `21x30` dense-weave | `66 / 0.957` | `Drop` | avg/max `6.59/13`, remote choke `1`, after-local frontier break `0`, local/near/same-region runs `3/2/2` | Useful visual comparison, but current trace is not hard-proof. |

## Result

This tryout produced one credible review candidate, not a finished production lane.

Positive signal:

- `rdsbl12_01` sits in the planned hard-read band: high coverage, 50-70 chains, low initial openers, avg choices around `4-5`, max choices capped at `10`, and at least one after-local frontier break.
- The best window is a real low-choice interruption: `2 1 1 1 2`, with region switches, frontier breaks, new-region picks, and direction breaks.

Boundary:

- All four rows still classify as `LocalEasy` in HardStructure V3, so this is not true-hard proof.
- The three Drop rows show why raw choke count is insufficient: they have remote/choke-looking metrics but too many openers or too much choice heat.
- This confirms the next generator-side work should tighten opener/choice heat and preserve after-local frontier breaks, not merely add chains or density.

## Next Implementation Direction

For `CompetitorMazeReadDemandV1`, promote `rdsbl12_01` as the seed specimen and build the next smoke around these gates:

- keep `initial openers <= 5`;
- require `choiceChokeAfterLocalFrontierBreakCount >= 1`;
- require `frontierDrainRemoteChokeCount >= 4`;
- cap official `maxChoices <= 10`;
- cap `localPatchSolveRunMax`, `nearOuterPatchSolveRunMax`, and `sameRegionSolveRunMax` around `3`;
- reject rows whose source or official trace starts with hot openers like `rdsbl12_05`.

This should be treated as a playtest pack for feel calibration. If the positive sample still feels easy by hand, the next step is generation-side structural contract materialization, not another loose density pass.
