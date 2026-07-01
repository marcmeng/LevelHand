# Campaign500 Normal Pilot20 Plan V1

- Source: `campaign500_normal_4slot_plan_v1.csv`.
- Sections: 1, 5, 15, 30, 45.
- Rows: 20.
- Purpose: first production smoke across front, early-mid, mid, late, and final pacing.

## Counts

- By production lane:
  - FlowCurve: 1
  - FlowPatch: 3
  - FlowRail: 1
  - LongChainProbe: 1
  - NeutralMixed: 4
  - PeelHard: 3
  - PeelLight: 1
  - PeelMid: 5
  - PressurePeak: 1
- By chain language:
  - curve_chain: 4
  - mixed_chain: 5
  - patch_chain: 3
  - rail_chain: 8
- By original section:
  - section 1: orders 3, 5, 8, 10
  - section 5: orders 41, 44, 47, 50
  - section 15: orders 145, 146, 148, 149
  - section 30: orders 291, 295, 296, 299
  - section 45: orders 441, 445, 446, 449

## Gates To Apply In Production

- All rows: solved, official trace solved, coverage >= 0.97, plannedAspect 0.70-0.85.
- Flow rows: maxChoices <= 10, localPatchRun <= 6, directional risk low; prioritize smooth feel.
- NeutralMixed rows: maxChoices <= 9, localPatchRun <= 6, long/short chain blend.
- PeelMid/PeelHard rows: maxChoices <= 10/12, local/near run controlled, clear layered release.
- LongChainProbe rows: reviewOnly=1; require manual pass before final keep.
