# Ordered Campaign Import V1 Notes

This is a first-pass ordered CSV for review, not a final imported LevelPack.

## Mix

- Total: 500
- Normal: 376
- Shape: 100 (20.0%)
- Hole: 24 (4.8%)
- Usable no-risk pool: 674/721

## Rules

- First 20 levels are tuned separately: no Hole, low score targets, and light visual density.
- Shape slots target two per ten levels.
- Hole slots start after level 20 and target roughly one per twenty levels.
- `targetDifficultyTag` records the intended stage difficulty before candidate matching.
- `riskTags != NoRisk` candidates are excluded from this preview.
- Recent same-theme Shape, repeated normal structure, adjacent high-pressure levels, and adjacent specials are penalized.

## Review Focus

- Manually inspect levels 1-20 first.
- Check whether Shape frequency feels delightful or too loud.
- Check Hole placement around first introduction and after difficulty spikes.
- Use this CSV to build the first actual preview LevelPack only after the ordering feels right.
