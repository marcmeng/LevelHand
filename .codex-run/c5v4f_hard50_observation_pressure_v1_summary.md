# Campaign Observation Pressure V1

- Input index: Exports/C5V4F/Docs/campaign500_rhythm_v4_final_per_level_config_index.csv
- Package root: Exports/C5V4F/U
- Selected rows: first 50 normal rows with sectionWaveRole Hard/PeakExtreme
- Result CSV: .codex-run/c5v4f_hard50_observation_pressure_v1.csv
- Average observation score: 75.79
- Average read-demand score: 23.57

## Class Distribution
- HighObservation: 49
- ObservationHard: 1

## Read Demand Distribution
- ReadLight: 50

## By Section Role
- Hard: count=28, avgScore=75.35, avgReadDemand=22.34
- PeakExtreme: count=22, avgScore=76.34, avgReadDemand=25.13

## By Production Lane
- Campaign500_HardGateUntil0910: count=4, avgScore=75.11, avgReadDemand=22.69
- CampaignLongChainLab: count=1, avgScore=75.02, avgReadDemand=12.35
- FlowPatch: count=1, avgScore=81.48, avgReadDemand=22.27
- hub_spoke: count=3, avgScore=76.67, avgReadDemand=28.17
- HubMixed: count=1, avgScore=76.96, avgReadDemand=28.46
- maze_long_chain: count=11, avgScore=75.79, avgReadDemand=24.8
- NeutralMixed: count=2, avgScore=75.88, avgReadDemand=24.13
- PeelHard: count=2, avgScore=74.3, avgReadDemand=22.44
- PeelLight: count=1, avgScore=77.64, avgReadDemand=27.09
- PeelMid: count=8, avgScore=75.98, avgReadDemand=24.88
- PressurePeak: count=11, avgScore=76.48, avgReadDemand=26.18
- slot_headmix_outerclean: count=1, avgScore=78.4, avgReadDemand=12.79
- slot_seedlock_gate_carrier: count=1, avgScore=60.03, avgReadDemand=0
- slot_seedmaze_chamber_corridor: count=2, avgScore=76.08, avgReadDemand=18.6
- slot_seedmaze_section_weave: count=1, avgScore=74.06, avgReadDemand=9.95

## Lowest 10
- order 164 [Hard] score=60.03, read=0, avgRay=0.811, shortRay=1, depth=3, openerRatio=0.2432, edges=28, notes=many_openers;mostly_same_region_dependencies;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 125 [Hard] score=73.42, read=18.86, avgRay=0.947, shortRay=1, depth=8, openerRatio=0.0526, edges=72, notes=mostly_same_region_dependencies;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 136 [PeakExtreme] score=73.58, read=21.96, avgRay=0.939, shortRay=1, depth=10, openerRatio=0.0606, edges=93, notes=mostly_same_region_dependencies;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 116 [Hard] score=73.83, read=20.98, avgRay=0.963, shortRay=1, depth=9, openerRatio=0.0366, edges=79, notes=mostly_same_region_dependencies;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 176 [Hard] score=73.99, read=21.19, avgRay=0.959, shortRay=1, depth=9, openerRatio=0.0411, edges=70, notes=mostly_same_region_dependencies;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 104 [Hard] score=74.06, read=9.95, avgRay=0.806, shortRay=1, depth=5, openerRatio=0.25, edges=27, notes=many_openers;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 144 [Hard] score=74.17, read=17.15, avgRay=0.875, shortRay=0.975, depth=8, openerRatio=0.25, edges=30, notes=many_openers;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 156 [PeakExtreme] score=74.35, read=23.23, avgRay=0.946, shortRay=1, depth=10, openerRatio=0.0541, edges=70, notes=mostly_same_region_dependencies;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 169 [PeakExtreme] score=74.41, read=23.3, avgRay=0.963, shortRay=1, depth=11, openerRatio=0.0367, edges=105, notes=mostly_same_region_dependencies;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 152 [Hard] score=74.46, read=22.01, avgRay=0.915, shortRay=1, depth=9, openerRatio=0.0854, edges=75, notes=mostly_same_region_dependencies;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand

## Highest 10
- order 8 [Hard] score=81.48, read=22.27, avgRay=0.935, shortRay=1, depth=6, openerRatio=0.0652, edges=43, crossRate=0.3721
- order 56 [PeakExtreme] score=81.13, read=29.72, avgRay=0.949, shortRay=1, depth=7, openerRatio=0.0506, edges=75, crossRate=0.36
- order 29 [PeakExtreme] score=79.59, read=31.88, avgRay=0.967, shortRay=1, depth=11, openerRatio=0.0333, edges=87, crossRate=0.3103
- order 109 [PeakExtreme] score=78.58, read=30.22, avgRay=0.956, shortRay=1, depth=12, openerRatio=0.0444, edges=86, crossRate=0.2791
- order 148 [Hard] score=78.4, read=12.79, avgRay=0.824, shortRay=1, depth=7, openerRatio=0.2353, edges=26, crossRate=0.5
- order 129 [PeakExtreme] score=77.99, read=20.04, avgRay=0.905, shortRay=0.9762, depth=6, openerRatio=0.2143, edges=33, crossRate=0.3333
- order 49 [Hard] score=77.64, read=27.09, avgRay=0.975, shortRay=1, depth=12, openerRatio=0.0253, edges=77, crossRate=0.2468
- order 69 [Hard] score=77.5, read=27, avgRay=0.968, shortRay=1, depth=9, openerRatio=0.0319, edges=91, crossRate=0.2418
- order 19 [PeakExtreme] score=77.22, read=25, avgRay=0.975, shortRay=1, depth=9, openerRatio=0.0253, edges=77, crossRate=0.2338
- order 76 [PeakExtreme] score=77.17, read=27.88, avgRay=0.958, shortRay=1, depth=11, openerRatio=0.0417, edges=69, crossRate=0.2319

## Lowest Read-Demand 10
- order 164 [Hard] read=0, obs=60.03, avgRay=0.811, shortRay=1, crossRate=0.1786, notes=many_openers;mostly_same_region_dependencies;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 104 [Hard] read=9.95, obs=74.06, avgRay=0.806, shortRay=1, crossRate=0.3704, notes=many_openers;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 119 [PeakExtreme] read=12.35, obs=75.02, avgRay=0.814, shortRay=1, crossRate=0.3333, notes=short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 148 [Hard] read=12.79, obs=78.4, avgRay=0.824, shortRay=1, crossRate=0.5, notes=short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 144 [Hard] read=17.15, obs=74.17, avgRay=0.875, shortRay=0.975, crossRate=0.3667, notes=many_openers;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 125 [Hard] read=18.86, obs=73.42, avgRay=0.947, shortRay=1, crossRate=0.1111, notes=mostly_same_region_dependencies;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 129 [PeakExtreme] read=20.04, obs=77.99, avgRay=0.905, shortRay=0.9762, crossRate=0.3333, notes=short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 84 [Hard] read=20.71, obs=75.54, avgRay=0.975, shortRay=1, crossRate=0.1795, notes=mostly_same_region_dependencies;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 116 [Hard] read=20.98, obs=73.83, avgRay=0.963, shortRay=1, crossRate=0.1266, notes=mostly_same_region_dependencies;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand
- order 176 [Hard] read=21.19, obs=73.99, avgRay=0.959, shortRay=1, crossRate=0.1286, notes=mostly_same_region_dependencies;short_head_rays;adjacent_blockers_dominate;hard_role_low_read_demand

## Highest Read-Demand 10
- order 29 [PeakExtreme] read=31.88, obs=79.59, avgRay=0.967, shortRay=1, crossRate=0.3103, depth=11
- order 109 [PeakExtreme] read=30.22, obs=78.58, avgRay=0.956, shortRay=1, crossRate=0.2791, depth=12
- order 56 [PeakExtreme] read=29.72, obs=81.13, avgRay=0.949, shortRay=1, crossRate=0.36, depth=7
- order 107 [PeakExtreme] read=28.46, obs=76.96, avgRay=0.968, shortRay=0.9894, crossRate=0.2247, depth=16
- order 105 [Hard] read=28.27, obs=76.72, avgRay=0.979, shortRay=0.9897, crossRate=0.2174, depth=16
- order 149 [PeakExtreme] read=28.18, obs=76.68, avgRay=0.98, shortRay=0.9898, crossRate=0.2151, depth=16
- order 154 [PeakExtreme] read=28.05, obs=76.61, avgRay=0.98, shortRay=0.9899, crossRate=0.2128, depth=16
- order 76 [PeakExtreme] read=27.88, obs=77.17, avgRay=0.958, shortRay=1, crossRate=0.2319, depth=11
- order 49 [Hard] read=27.09, obs=77.64, avgRay=0.975, shortRay=1, crossRate=0.2468, depth=12
- order 112 [PeakExtreme] read=27.07, obs=76.69, avgRay=0.957, shortRay=1, crossRate=0.2159, depth=15

## Notes
- This is a static authored-geometry proxy, not a replacement for official Greedy/trace validation.
- observationPressureScore measures static dependency density.
- readDemandScore is stricter: adjacent/short blockers are penalized, while long lookahead rays and cross-region dependencies are rewarded.
