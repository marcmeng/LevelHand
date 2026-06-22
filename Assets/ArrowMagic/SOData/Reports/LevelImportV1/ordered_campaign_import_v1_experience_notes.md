# Ordered Campaign Import V1 Notes

This is a first-pass ordered CSV for review, not a final imported LevelPack.

## Mix

- Total: 500
- Normal: 396
- Shape: 80 (16.0%)
- Hole: 24 (4.8%)
- Bonus side events: 1 (do not consume campaign order)
- Usable no-risk pool: 676/723

## Rules

- First 20 levels are director-authored with fixed tutorial, rescue, prop unlock, pressure, and recovery beats.
- First-20 chain scale is intentionally small: normal slots prefer roughly 50 chains, early peaks are stage-relative, and the level-19 peak is capped around 120 chains.
- Beginner prop Shape levels must come from `shape_early_prop` and are not reused in later Shape slots.
- The first two Hole candidates are reserved for the front-20 rescue beats in this preview; these explicit onboarding slots may bypass the global `NoRisk` filter if they have no build error.
- Bonus levels are written to a separate side-event CSV and do not consume the 500 main campaign slots.
- Ten-level blocks rotate through five templates instead of using the same rhythm every time.
- Shape slots average about 1-2 per ten levels, capped by template placement.
- After the front-20 rescues, Hole slots use irregular gaps around twenty levels.
- `targetDifficultyTag` records the intended stage difficulty before candidate matching.
- `stageRelativeDifficultyTag` records how hard the selected level feels inside its campaign stage.
- `experienceRole` records the slot purpose: Flow, Observe, Pressure, Recovery, VisualSpice, RescueIntro, Peak, etc.
- `contentSeries`, `unlockTag`, and `storyBeat` describe authored onboarding beats.
- `riskTags != NoRisk` candidates are excluded from this preview.
- Recent same-theme Shape, repeated normal structure, adjacent high-pressure levels, and adjacent specials are penalized.

## Review Focus

- Manually inspect levels 1-20 first.
- Check whether Shape frequency feels delightful or too loud.
- Check Hole placement around first introduction and after difficulty spikes.
- Check late-stage non-Extreme slots: if Flow appears in Pressure/Bottleneck/Core slots, the pool needs more late Normal/Hard/VeryHard or real Bonus levels.
- Use this CSV to build the first actual preview LevelPack only after the ordering feels right.
