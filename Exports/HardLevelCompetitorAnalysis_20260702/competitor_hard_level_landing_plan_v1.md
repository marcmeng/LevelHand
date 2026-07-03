# Competitor Hard Level Landing Plan V1

Date: 2026-07-02

Scope: read project memory, inspect current rule/generation code, inspect in-project competitor/reference screenshots, and define a practical route for producing levels with comparable perceived difficulty.

## Inputs Read

- Project memory and workflow: `AGENTS.md`, `.agents/memory/PROJECT_CONTEXT.md`, `.agents/memory/WORKFLOW.md`, `.agents/memory/CURRENT_STATUS.md`, `.agents/memory/DECISIONS.md`.
- Indexes: `.agents/index/RESOURCE_INDEX.md`, `.agents/index/SCRIPT_INDEX.md`, `.agents/index/LEVEL_INDEX.md`.
- Core rules/code: `Assets/ArrowMagic/Scripts/ArrowMagicRuleset.cs`, `Assets/ArrowMagic/Scripts/Utils/ArrowChainUtility.cs`, `Assets/ArrowMagic/Scripts/Authoring/AuthoredLevelBuilder.cs`, `Assets/ArrowMagic/Scripts/ClearAllArrowsGenerator.cs`, `Assets/ArrowMagic/Scripts/BoardGenerationTuning.cs`.
- Portable generator/validator: `Packages/com.pixelbug.arrow-level-generator/Runtime/**`.
- Main procedural logic: `Assets/ArrowMagic/Editor/NoMaskProceduralGenerator.cs`, `.worktrees/read-demand-hardening/Assets/ArrowMagic/Editor/NoMaskProceduralGenerator.cs`.
- Trace/difficulty tools: `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-SGPRhythmTrace.ps1`, `.worktrees/sgp-rhythm-lab/Tools/SGPRhythmLab/Build-GeneratedRootWBPV12DifficultyVerifierV1.py`.
- Competitor/reference screenshots: `TempContactSheets/Arrowz_36.png`, `TempContactSheets/Korean_401_1000_complete_36.png`, `TempContactSheets/save_family_ypJOy09GT0I__36.png`, `TempContactSheets/AOut_relocated_clear_202_36.png`, `TempContactSheets/Above_300_Merged_2026052_36.png`, `TempContactSheets/all_even_36.png`.

## Base Gameplay Rules

The actual puzzle rules are stricter than the screenshots suggest.

- Tile types are only `Empty`, `Arrow`, and `Block`.
- Authored arrow chains store `indices` as `head -> tail`; `AuthoredLevelBuilder` writes runtime arrows in tail-to-head direction.
- A valid chain has at least 2 cells, no duplicate cells, no overlap with blocks or other chains, and every adjacent pair must be cardinally adjacent.
- Runtime chain membership is adjacent-only. Empty cells never connect chain bodies.
- Click/signal propagation and chain membership are different graphs:
  - signal can travel through empty cells under `SignalTravelMode.ThroughEmpty`;
  - signal stops on block, wrong-direction arrow, loop, or another arrow after empty travel;
  - if the signal exits the board, the clicked arrow's whole adjacent chain is removed.
- Victory is board has no arrows.
- A visually blocked ray is only real difficulty if the first-hit / release-owner / solve-order relation survives authored build and runtime trace.

Implication: hard levels must be designed as dependency/reread structures, not merely dense drawings.

## Current Generation Logic

### 1. Basic Clear-All Generator

`ClearAllArrowsGenerator` is a mechanical base generator:

- chooses allowed empty start cells;
- builds random exit paths with outward bias and no U-turns;
- targets coverage by filling arrow cells;
- enforces max chain length and removes too-short chains;
- can be Greedy-validated by `BoardGenerationTuning` / `GreedyValidator`.

Useful for legality, mask fill, and simple solvable boards. Not enough for competitor-hard output because it does not plan read gates, remote release, or meaningful wrong-choice consequences.

### 2. Portable Package

The package generator builds layered rectangle families: `Shell`, `Section`, `Lock`, `Maze`, `Dense`, `Sweep`, `Shape`.

Validation and scoring cover:

- geometry validity;
- Greedy escape solve;
- coverage and outer-band coverage;
- initial openers;
- edge-short chains;
- max straight run;
- average choices and max choices.

This is a good baseline for "valid and not visually broken." It is not a sufficient hard-level gate. It does not directly measure local fake choke, remote dependency, key arrow value, support closure, dual gate, or readable reread pressure.

### 3. No-Mask / PSG / Nutation

`NoMaskProceduralGenerator` adds style specs and pressure profiles:

- `StyleSpec`: width, height, target chains, length band, target coverage, turn bias, edge opening bias, max initial movable chains, short-edge caps, motif/type.
- SGP/Pressure: uses peel/head scoring, pressure gates, early choice curve, local-run, jump distance, region/layer transitions.
- Nutation: useful for Flow/Peel/LongChain/Hub/Maze style language, but project memory says production-ready lanes are mostly normal/review, not true hard peaks.

Current boundary from memory: PSG/Nutation can make good normal-plus candidates, but many high-coverage or long-chain outputs still collapse into readable local sweeps.

### 4. Read-Demand / ScheduledBreak

The read-demand branch is closest to the desired "hard-feel" direction.

Relevant mechanisms:

- reserves front gaps / intentional empties;
- scores blocked rays and cross-region dependencies;
- uses FrontierContract and ScheduledBreak variants;
- rewards choice compression, switch flow, window flow, remote drain choke, after-local frontier breaks;
- has review packs and trace evidence for `rdsb_03`, `rdsb_11`, `rdsb_05`, and `sgp_rdcm_v2_*` rows.

Current best evidence:

- ScheduledBreak rows are the strongest choke/key-arrow candidates in `ChokeCandidateAudit`.
- `rdsb_03` has strong choke review evidence: avg/max choices about `4.08/7`, remote chokes `4`, composite break windows `2`, after-local frontier breaks `2`.
- ChoiceValue V2 rows improve useful tension and meaningful option rate, but do not yet prove a robust bulk production lane.

### 5. Generated-Root WBP

Generated-Root WBP is the long-term true-hard architecture path:

- it separates `TraceGate` from `DifficultyVerify`;
- requires solved/process health plus root preservation and pre-materialization duty evidence;
- evaluates low choice, switch breaks, remote dependency, anti-flow, structural contract, visual risk, preplan evidence.

Current boundary:

- t145/t147 rows pass TraceGate and are `HardPotential`;
- they still fail `TrueHardCandidate` mainly due to `LocalEasyStructure`, weak structural contract, no qualified support closure, and no strict dual gate.

Implication: WBP is not the fastest route to competitor-like medium-hard visual levels, but it is the correct route for future special hard / extreme peaks.

## Competitor Screenshot Findings

These are visual findings from contact sheets, not decompiled competitor data.

### Arrowz / Korean / Above / All Even

Common features:

- Dense rectangular maze fields, often 18x24 to 24x32-ish in mobile framing.
- Many short and medium segments, not one or two giant chains.
- Frequent nested rooms, spiral/回字 structures, U-shaped corridors, long straight halls, side pockets, and compartment breaks.
- Later levels mix dense maze panels with sparse fractured panels; both can be hard if solve focus jumps across regions.
- High-level boards often have obvious local "roads," but the hard feel comes from deciding which road opens which other road.
- Bad visual examples in our history match what the screenshots avoid: parallel rail dominance, all-one-direction strips, and simple edge-out sweeps.

### Save Family / Icy Arrow

Common features:

- Board is embedded in a goal/rescue wrapper: character/card/ice object acts as a visual anchor.
- Difficulty comes from arrows around the anchor, long corridors touching the anchor, and staged release into or away from the anchor.
- Density is lower than the maze-only screenshots in many samples, but the anchor creates a readable objective and makes dependencies easier to perceive.
- Good for "read-check" and "mini-boss" slots, not necessarily for every normal level.

### Arrow Out / AOut

Common features:

- Shape silhouettes and level-card presentation provide rhythm and novelty.
- Shape alone is not hard; many screenshots are visual wrappers around simple or medium-density arrow structures.
- Shape should host underlying read/dependency evidence when used for hard slots.

## What "Competitor-Like Hard" Should Mean Here

Do not target raw density alone. Target a combined visual + trace profile.

Visual target:

- Dense but readable maze texture.
- 50-80 chains for normal-hard/mobile rectangle, 60-95 for hard/peak, adjusted by board size.
- Coverage roughly `0.92-0.97`, but avoid high coverage if it creates outer sweeps or parallel rails.
- Mix of chain lengths: many 3-9 cell chains, some 10-20 cell corridors, very few giant chains.
- At least 5-9 visible regions/rooms with 2-4 narrow gates or chokepoints.
- Some long corridors and nested loops, but cap same-direction visual components.

Trace target:

- solved by official trace / Greedy;
- initial openers in a controlled band, usually `2-6` for hard-read samples;
- average choices in the readable tension band, roughly `3.5-5.5`;
- max choices ideally `<= 9` or `<= 10` for hard, stricter for small boards;
- at least one midgame low-choice window with choices `1-4`;
- `choiceChokeAfterLocalFrontierBreakCount >= 1` for hard-read slots;
- `frontierDrainRemoteChokeCount >= 3` for strong hard-read candidates;
- local/near/outer sweeps controlled: target `localPatchSolveRunMax <= 5`, `nearOuterPatchSolveRunMax <= 4`, `dependencyFollowRunMax <= 6`;
- no `LocalEasy` classification for true hard/peak supply;
- for special hard/extreme, require structural contract evidence: support closure, dual gate, planned bridge pressure, or cross-region critical locks.

## Recommended Landing Route

### Short-Term: Produce A Reviewable Competitor-Hard Pack From Existing Strong Sources

Use existing evidence to assemble a 12-20 level review pack:

- ScheduledBreak: `rdsb_03`, `rdsb_11`, `rdsb_05`.
- ReadDemandMutation: `sgp_rdcm_v2_s02_01_c16`, `sgp_rdcm_v2_s02_07_c13`, `sgp_rdcm_v2_s01_01_c46`, `sgp_rdcm_v2_s02_03_c3`.
- A few NutationLongChain/Hub rows only as visual-language references, not hard proof.

Purpose:

- compare against the contact sheets in Unity;
- classify which samples feel closest to "competitor hard";
- calibrate human labels against trace fields.

This can be done without changing generation core.

### Mid-Term: Build `CompetitorMazeReadDemandV1`

Create a new isolated lane, preferably in `read-demand-hardening` or a fresh worktree:

```text
CompetitorMazeReadDemandV1 =
  ScheduledBreak / FrontierContract read-demand scoring
+ maze-room visual grammar
+ anti-rail visual gates
+ official trace + DifficultyVerify + human review
```

Generation shape:

- board sizes: `20x28`, `21x29`, `22x30`, plus a few wider `24x30` tests;
- target chains: `58-78` for hard-read, `75-95` for peak;
- target coverage: `0.94-0.965`;
- style grammar: `DenseWeave`, `RoomCorridor`, `NestedRooms`, `LongCorridor`, `CircuitBoard`;
- reserve 2-4 corridor/gate cells per region before generic fill;
- schedule target regions so the solve path alternates local cluster -> remote choke -> local release -> different remote cluster.

Generator-side gates:

- cap initial openers, edge heads, direct outer exit ratio;
- enforce region entropy and direction/axis shift;
- require at least one planned after-local frontier break;
- score first-hit owner relationships, not just local chain shape;
- reject static same-direction components, parallel rails, and simple boundary strips;
- keep intentional empty corridors as signal/ray space, not chain body.

Trace gates:

- official solved;
- process tier `A` or high `B`;
- avg/max choices inside the readable tension band;
- remote choke and after-local frontier break evidence;
- anti-flow gates for local/near/outer runs;
- reject `flatConsequenceHigh` unless human review confirms the board is still interesting.

### Long-Term: Promote Structural Contracts For True Hard / Extreme

Use WBP only when the target is real hard+:

- add structural-contract reservation/materializer before coverage waves;
- materialize support closure, dual gate, bridge pressure, and cross-basin critical locks before filler coverage;
- continue using `TraceGate` and `DifficultyVerify`;
- do not call a candidate true hard if it remains `LocalEasy`.

This route is heavier, but it is the right answer for Campaign500 `DependencyPeak`, `SpecialHard`, and `ExtremeMemory` slots.

## What Not To Do

- Do not treat `TraceOrderKeep`, high coverage, or low avg choices as hard proof.
- Do not promote high-chain HoleMask rows that are Greedy-solvable but visually mechanical.
- Do not rely on LongChain as a bulk hard lane; user review already found current LongChain feel unsuitable for production.
- Do not use shape geometry alone as hard evidence.
- Do not add late one-chain blockers after the board is nearly full and call it structural hard; prior WBP work repeatedly shows late repair space is usually exhausted.
- Do not optimize only for fewer choices; fake low-choice deadlocks and local-only chokes are known negatives.

## Concrete First Experiment

Recommended next implementation unit:

```text
Lane: CompetitorMazeReadDemandV1
Worktree: .worktrees/read-demand-hardening or fresh codex/competitor-maze-read-demand
Source count: 36-60
Review keep: top 12
Board sizes: 20x28, 21x29, 22x30
Chains: 58-78
Coverage: 0.94-0.965
Required evidence:
  official solved
  avgChoices 3.5-5.5
  maxChoices <= 10
  initialOpeners 2-6
  frontierDrainRemoteChokeCount >= 3
  choiceChokeAfterLocalFrontierBreakCount >= 1
  localPatchSolveRunMax <= 5
  nearOuterPatchSolveRunMax <= 4
  dependencyFollowRunMax <= 6
  no severe stripe / same-direction rail / outer sweep risk
```

Expected output:

- source pack;
- trace metrics + step diagnostics;
- `DifficultyVerify` or Trace Standard V2 join;
- 12-level review pack mounted in an isolated Demo;
- human review sheet with columns: visual competitor match, actual difficulty, unfairness/noise, local sweep risk, best/worst sample.

## Bottom Line

The fastest credible path to competitor-like difficulty is not a new random maze generator. It is ScheduledBreak/ReadDemand with a competitor-inspired maze-room visual grammar and strict trace gates. WBP remains the future path for true hard/extreme, but for near-term reviewable difficult levels, start with `CompetitorMazeReadDemandV1` and validate against the screenshot contact sheets plus human playtest.
