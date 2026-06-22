# Campaign500 Shape Refresh V1 Static Check

## Hard Rule Checks
- Front 1-15 changed: 0
- Hole locked slots changed: 0
- Duplicate new shape families: 0
- Shape replacements checked: 96

## Difficulty Proxy By Replaced Shape Slots
- difficulty 1: count=36, newChains min/avg/max=64/102.5/149
- difficulty 2: count=33, newChains min/avg/max=68/109.6/166
- difficulty 3: count=19, newChains min/avg/max=67/124.6/160
- difficulty 4: count=8, newChains min/avg/max=69/108.6/161

## Static Risk Flags
- Flagged replacements: 16
- L23: diff=4, chains 80->69, shape=train:compact, flags=HardSlotTooLowChain|PressureSlotTooLowChain
- L32: diff=3, chains 83->67, shape=wavecrest:compact, flags=PressureSlotTooLowChain
- L78: diff=4, chains 92->87, shape=pencil:large, flags=HardSlotTooLowChain|PressureSlotTooLowChain
- L87: diff=4, chains 95->97, shape=umbrella:large, flags=HardSlotTooLowChain
- L113: diff=4, chains 98->97, shape=guitar:large, flags=HardSlotTooLowChain
- L403: diff=4, chains 170->153, shape=payphone:large, flags=OldAboveNewPoolMax
- L408: diff=3, chains 167->153, shape=arcadecabinet:main, flags=OldAboveNewPoolMax
- L413: diff=2, chains 179->155, shape=magicbook:large, flags=OldAboveNewPoolMax
- L418: diff=3, chains 186->157, shape=crystalcluster:main, flags=DifficultyMayDrop|OldAboveNewPoolMax
- L423: diff=3, chains 186->160, shape=camera:large, flags=DifficultyMayDrop|OldAboveNewPoolMax
- L427: diff=4, chains 191->161, shape=treasurechest:large, flags=DifficultyMayDrop|OldAboveNewPoolMax
- L433: diff=2, chains 195->161, shape=filmroll:large, flags=DifficultyMayDrop|OldAboveNewPoolMax
- L438: diff=2, chains 207->166, shape=giftbox:large, flags=DifficultyMayDrop|OldAboveNewPoolMax
- L458: diff=1, chains 86->112, shape=star:main, flags=FlowMayBecomeHeavy
- L478: diff=1, chains 86->112, shape=flag:main, flags=FlowMayBecomeHeavy
- L487: diff=1, chains 84->113, shape=sun:main, flags=FlowMayBecomeHeavy

## Unity Validation
- Unity batch validation was attempted but did not run because the Unity LicensingClient timed out before project load.
- Once Unity opens normally, run menu: Tools/Arrow Magic/Campaign 500/Optimization/Validate Shape Refresh V1.

## Files
- static flags: `F:/Unityproject/ArrowLevel-Hand/Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/campaign500_shape_refresh_v1_static_check_flags.csv`
- replacements: `F:/Unityproject/ArrowLevel-Hand/Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/campaign500_shape_refresh_v1_replacements.csv`
