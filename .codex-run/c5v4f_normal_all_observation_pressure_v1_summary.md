# Campaign Observation Pressure V1

- Input index: Exports/C5V4F/Docs/campaign500_rhythm_v4_final_per_level_config_index.csv
- Package root: Exports/C5V4F/U
- Selected rows: all normal rows
- Max rows: unlimited
- Result CSV: .codex-run/c5v4f_normal_all_observation_pressure_v1.csv
- Average observation score: 74.65
- Average read-demand score: 21.85

## Class Distribution
- HighObservation: 332
- LowObservation: 2
- ObservationHard: 10
- WatchLight: 6

## Read Demand Distribution
- ReadLight: 350

## By Section Role
- Hard: count=81, avgScore=73.96, avgReadDemand=21.57
- Normal: count=135, avgScore=75.29, avgReadDemand=21.61
- PeakExtreme: count=56, avgScore=73.02, avgReadDemand=21.46
- Recovery: count=78, avgScore=75.43, avgReadDemand=22.83

## By Production Lane
- (blank): count=108, avgScore=74.63, avgReadDemand=21.95
- Campaign500_HardGateUntil0910: count=21, avgScore=76.08, avgReadDemand=24.09
- CampaignLongChainLab: count=1, avgScore=75.02, avgReadDemand=12.35
- FlowCurve: count=12, avgScore=77.23, avgReadDemand=19.84
- FlowPatch: count=14, avgScore=77.89, avgReadDemand=20.73
- FlowRail: count=12, avgScore=76.99, avgReadDemand=16.03
- hub_spoke: count=3, avgScore=76.67, avgReadDemand=28.17
- HubMixed: count=1, avgScore=76.96, avgReadDemand=28.46
- LongChainProbe: count=9, avgScore=74.88, avgReadDemand=23.46
- maze_long_chain: count=24, avgScore=76.18, avgReadDemand=23.62
- NeutralMixed: count=47, avgScore=76.56, avgReadDemand=24.98
- PeelHard: count=8, avgScore=76.7, avgReadDemand=25.2
- PeelLight: count=11, avgScore=75.69, avgReadDemand=22.25
- PeelMid: count=37, avgScore=76.07, avgReadDemand=24.65
- PressurePeak: count=20, avgScore=76.19, avgReadDemand=25.28
- slot_headmix_outerclean: count=4, avgScore=59.45, avgReadDemand=5.67
- slot_seedlock_gate_carrier: count=2, avgScore=53.79, avgReadDemand=0
- slot_seedlock_section_bridge: count=1, avgScore=44.32, avgReadDemand=0
- slot_seedmaze_chamber_corridor: count=6, avgScore=60.37, avgReadDemand=6.36
- slot_seedmaze_section_weave: count=2, avgScore=65.62, avgReadDemand=5.46
- slot_seedweave_braid_carrier: count=6, avgScore=52.72, avgReadDemand=5.19
- slot_seedweave_dense_support: count=1, avgScore=61.93, avgReadDemand=5.19

## Lowest 10
- order 1 [Normal] score=16.8, read=0, avgRay=1, shortRay=1, depth=1, openerRatio=0.6667, edges=2, notes=many_openers;shallow_dependency;short_head_rays;adjacent_blockers_dominate
- order 299 [PeakExtreme] score=26.97, read=0, avgRay=0.667, shortRay=1, depth=2, openerRatio=0.5333, edges=7, notes=hard_role_low_observation;many_openers;mostly_same_region_dependencies;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 290 [Recovery] score=40.06, read=7.06, avgRay=0.8, shortRay=0.9333, depth=2, openerRatio=0.4667, edges=8, notes=many_openers;short_head_rays;adjacent_blockers_dominate
- order 294 [Recovery] score=40.06, read=7.06, avgRay=0.8, shortRay=0.9333, depth=2, openerRatio=0.4667, edges=8, notes=many_openers;short_head_rays;adjacent_blockers_dominate
- order 296 [Hard] score=40.29, read=0.29, avgRay=0.588, shortRay=1, depth=4, openerRatio=0.5294, edges=8, notes=hard_role_low_observation;many_openers;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 199 [Hard] score=44.32, read=0, avgRay=0.708, shortRay=1, depth=3, openerRatio=0.375, edges=15, notes=hard_role_low_observation;many_openers;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 208 [Hard] score=47.55, read=0, avgRay=0.704, shortRay=1, depth=3, openerRatio=0.3704, edges=17, notes=hard_role_low_observation;many_openers;mostly_same_region_dependencies;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 269 [PeakExtreme] score=48.83, read=0, avgRay=0.591, shortRay=1, depth=4, openerRatio=0.4091, edges=13, notes=hard_role_low_observation;many_openers;mostly_same_region_dependencies;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 272 [Normal] score=52.62, read=0, avgRay=0.619, shortRay=1, depth=4, openerRatio=0.381, edges=13, notes=many_openers;short_head_rays;adjacent_blockers_dominate
- order 276 [PeakExtreme] score=57.19, read=0.98, avgRay=0.652, shortRay=1, depth=4, openerRatio=0.3478, edges=15, notes=many_openers;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand

## Highest 10
- order 300 [Recovery] score=82.97, read=20.07, avgRay=0.861, shortRay=1, depth=6, openerRatio=0.1389, edges=31, crossRate=0.4194
- order 11 [Normal] score=82.86, read=19.4, avgRay=0.853, shortRay=1, depth=5, openerRatio=0.1471, edges=29, crossRate=0.4138
- order 297 [Normal] score=82.59, read=22.6, avgRay=0.947, shortRay=1, depth=6, openerRatio=0.0526, edges=36, crossRate=0.4167
- order 41 [Recovery] score=81.8, read=26.46, avgRay=0.887, shortRay=1, depth=9, openerRatio=0.1132, edges=47, crossRate=0.383
- order 8 [Hard] score=81.48, read=22.27, avgRay=0.935, shortRay=1, depth=6, openerRatio=0.0652, edges=43, crossRate=0.3721
- order 249 [Hard] score=81.2, read=29.14, avgRay=0.94, shortRay=1, depth=8, openerRatio=0.06, edges=47, crossRate=0.3617
- order 56 [PeakExtreme] score=81.13, read=29.72, avgRay=0.949, shortRay=1, depth=7, openerRatio=0.0506, edges=75, crossRate=0.36
- order 81 [Recovery] score=81.07, read=23.67, avgRay=0.907, shortRay=1, depth=7, openerRatio=0.093, edges=39, crossRate=0.359
- order 261 [Recovery] score=80.58, read=23.86, avgRay=0.985, shortRay=1, depth=8, openerRatio=0.0154, edges=64, crossRate=0.3438
- order 197 [Normal] score=80.27, read=32.35, avgRay=0.943, shortRay=1, depth=11, openerRatio=0.0571, edges=66, crossRate=0.3333

## Lowest Read-Demand 10
- order 1 [Normal] read=0, obs=16.8, avgRay=1, shortRay=1, crossRate=0.5, notes=many_openers;shallow_dependency;short_head_rays;adjacent_blockers_dominate
- order 164 [Hard] read=0, obs=60.03, avgRay=0.811, shortRay=1, crossRate=0.1786, notes=many_openers;mostly_same_region_dependencies;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 199 [Hard] read=0, obs=44.32, avgRay=0.708, shortRay=1, crossRate=0.2, notes=hard_role_low_observation;many_openers;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 208 [Hard] read=0, obs=47.55, avgRay=0.704, shortRay=1, crossRate=0.1765, notes=hard_role_low_observation;many_openers;mostly_same_region_dependencies;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 269 [PeakExtreme] read=0, obs=48.83, avgRay=0.591, shortRay=1, crossRate=0.1538, notes=hard_role_low_observation;many_openers;mostly_same_region_dependencies;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 272 [Normal] read=0, obs=52.62, avgRay=0.619, shortRay=1, crossRate=0.2308, notes=many_openers;short_head_rays;adjacent_blockers_dominate
- order 286 [Normal] read=0, obs=60.55, avgRay=0.667, shortRay=1, crossRate=0.1875, notes=many_openers;short_head_rays;adjacent_blockers_dominate
- order 299 [PeakExtreme] read=0, obs=26.97, avgRay=0.667, shortRay=1, crossRate=0, notes=hard_role_low_observation;many_openers;mostly_same_region_dependencies;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 296 [Hard] read=0.29, obs=40.29, avgRay=0.588, shortRay=1, crossRate=0.25, notes=hard_role_low_observation;many_openers;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 189 [PeakExtreme] read=0.58, obs=59.29, avgRay=0.773, shortRay=1, crossRate=0.25, notes=many_openers;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand

## Highest Read-Demand 10
- order 197 [Normal] read=32.35, obs=80.27, avgRay=0.943, shortRay=1, crossRate=0.3333, depth=11
- order 29 [PeakExtreme] read=31.88, obs=79.59, avgRay=0.967, shortRay=1, crossRate=0.3103, depth=11
- order 160 [Normal] read=30.77, obs=78.93, avgRay=0.957, shortRay=1, crossRate=0.2879, depth=13
- order 194 [Normal] read=30.64, obs=79.96, avgRay=0.971, shortRay=1, crossRate=0.3235, depth=9
- order 371 [Normal] read=30.64, obs=79.96, avgRay=0.971, shortRay=1, crossRate=0.3235, depth=9
- order 109 [PeakExtreme] read=30.22, obs=78.58, avgRay=0.956, shortRay=1, crossRate=0.2791, depth=12
- order 241 [Recovery] read=29.94, obs=78.8, avgRay=0.957, shortRay=1, crossRate=0.2879, depth=12
- order 56 [PeakExtreme] read=29.72, obs=81.13, avgRay=0.949, shortRay=1, crossRate=0.36, depth=7
- order 145 [Recovery] read=29.6, obs=78.17, avgRay=0.961, shortRay=1, crossRate=0.2703, depth=10
- order 124 [Normal] read=29.54, obs=78.17, avgRay=0.963, shortRay=1, crossRate=0.2658, depth=11

## Notes
- This is a static authored-geometry proxy, not a replacement for official Greedy/trace validation.
- observationPressureScore measures static dependency density.
- readDemandScore is stricter: adjacent/short blockers are penalized, while long lookahead rays and cross-region dependencies are rewarded.
