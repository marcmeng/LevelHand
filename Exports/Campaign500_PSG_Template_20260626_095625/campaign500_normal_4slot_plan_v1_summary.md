# Campaign500 Normal 4-Slot Plan V1

- Source template: `campaign500_psg_regeneration_template.csv`.
- Scope: normal-only replacement/reference production; shape and hole slots are excluded.
- Tutorial/very tiny front slot is avoided when a section has enough non-tutorial normal slots.
- Contract: 50 sections x 4 normal slots = 200 slot-specific production targets.
- Per-section target: 2 ordinary, 1 hard, 1 peak/review peak.
- Canvas target: planned aspect 0.70-0.85, soft ceiling 0.90.

## Counts

- Total rows: 200
- By production lane:
  - FlowCurve: 14
  - FlowPatch: 13
  - FlowRail: 13
  - LongChainProbe: 10
  - NeutralMixed: 50
  - PeelHard: 20
  - PeelLight: 10
  - PeelMid: 50
  - PressurePeak: 20
- By chain language:
  - curve_chain: 48
  - mixed_chain: 70
  - patch_chain: 16
  - rail_chain: 66
- By production wave:
  - Batch01_001_100: 32
  - Batch02_101_200: 36
  - Batch03_201_300: 36
  - Batch04_301_400: 40
  - Batch05_401_500: 36
  - Pilot20: 20

## Flow Placement
- Flow total: 40. FlowCurve/FlowRail/FlowPatch are rotated to vary feel.
- Flow appears in every section 1-35, then as late relief at sections 36, 39, 42, 45, 48.

## Review Boundary
- LongChainProbe rows are marked `reviewOnly=1`; they are late peak candidates and should not enter final keep without manual体感 pass.
- PeelMid/PeelHard and NeutralMixed are the main production backbone. Flow is controlled relief, now raised to around 40 rows as requested.

## Pilot20 Sections
- Pilot sections: 1, 5, 15, 30, 45. This gives 20 rows across front, early mid, mid, late, and final pacing.
