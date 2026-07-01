# Campaign Read-Demand Gate V1

- Metrics CSV: .codex-run/c5v4f_hardpeak_all_observation_pressure_v1.csv
- Output CSV: .codex-run/c5v4f_hardpeak_read_demand_gate_v1.csv
- Keep threshold: readDemand >= 28 plus static dependency, opener, cross-region, and depth guards
- Near threshold: readDemand >= 25 with relaxed cross-region/ray guards

## Gate Counts
- ReadKeep: 14
- ReadNear: 39
- RegenPriority: 65
- RegenPriorityHigh: 19

## ReadKeep
- order 29 [PeakExtreme] read=31.88, obs=79.59, cross=0.3103, depth=11, lane=PressurePeak
- order 109 [PeakExtreme] read=30.22, obs=78.58, cross=0.2791, depth=12, lane=PressurePeak
- order 56 [PeakExtreme] read=29.72, obs=81.13, cross=0.36, depth=7, lane=maze_long_chain
- order 305 [Hard] read=29.54, obs=78.17, cross=0.2658, depth=11, lane=
- order 249 [Hard] read=29.14, obs=81.2, cross=0.3617, depth=8, lane=maze_long_chain
- order 432 [Hard] read=28.62, obs=77.08, cross=0.2292, depth=15, lane=PeelMid
- order 229 [Hard] read=28.58, obs=78.07, cross=0.2676, depth=10, lane=Campaign500_HardGateUntil0910
- order 107 [PeakExtreme] read=28.46, obs=76.96, cross=0.2247, depth=16, lane=HubMixed
- order 105 [Hard] read=28.27, obs=76.72, cross=0.2174, depth=16, lane=hub_spoke
- order 412 [Hard] read=28.27, obs=76.94, cross=0.2245, depth=15, lane=PeelMid
- order 149 [PeakExtreme] read=28.18, obs=76.68, cross=0.2151, depth=16, lane=hub_spoke
- order 476 [Hard] read=28.13, obs=77.33, cross=0.2361, depth=10, lane=
- order 312 [Hard] read=28.1, obs=76.77, cross=0.2188, depth=15, lane=PeelMid
- order 154 [PeakExtreme] read=28.05, obs=76.61, cross=0.2128, depth=16, lane=hub_spoke

## ReadNear
- order 76 [PeakExtreme] read=27.88, obs=77.17, cross=0.2319, depth=11, lane=maze_long_chain
- order 352 [Hard] read=27.83, obs=76.94, cross=0.2245, depth=15, lane=PeelMid
- order 464 [Hard] read=27.72, obs=78.27, cross=0.2698, depth=11, lane=
- order 216 [PeakExtreme] read=27.67, obs=77.85, cross=0.2545, depth=10, lane=maze_long_chain
- order 396 [PeakExtreme] read=27.35, obs=76.85, cross=0.2222, depth=11, lane=PressurePeak
- order 49 [Hard] read=27.09, obs=77.64, cross=0.2468, depth=12, lane=PeelLight
- order 112 [PeakExtreme] read=27.07, obs=76.69, cross=0.2159, depth=15, lane=maze_long_chain
- order 69 [Hard] read=27, obs=77.5, cross=0.2418, depth=9, lane=PressurePeak
- order 329 [PeakExtreme] read=27, obs=77.5, cross=0.2418, depth=9, lane=
- order 429 [Hard] read=26.78, obs=76.52, cross=0.2099, depth=12, lane=PeelMid
- order 227 [PeakExtreme] read=26.44, obs=77.89, cross=0.2593, depth=13, lane=maze_long_chain
- order 132 [Hard] read=26.31, obs=76.59, cross=0.2179, depth=10, lane=PeelMid
- order 394 [Hard] read=26.31, obs=76.59, cross=0.2179, depth=10, lane=
- order 75 [Hard] read=26.24, obs=76.18, cross=0.2, depth=12, lane=PressurePeak
- order 47 [PeakExtreme] read=26.16, obs=76.12, cross=0.2, depth=15, lane=PeelMid
- order 185 [Hard] read=26.1, obs=76.11, cross=0.1972, depth=11, lane=PeelMid
- order 94 [Hard] read=26.05, obs=76.07, cross=0.1959, depth=15, lane=PressurePeak
- order 336 [PeakExtreme] read=26.05, obs=76.07, cross=0.1959, depth=15, lane=
- order 347 [PeakExtreme] read=25.91, obs=75.98, cross=0.194, depth=13, lane=PressurePeak
- order 70 [Hard] read=25.78, obs=75.89, cross=0.1923, depth=12, lane=PeelMid

## RegenPriorityHigh First 30
- order 104 [Hard] read=9.95, obs=74.06, reasons=read_below_near;read_very_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;dependency_depth_low
- order 119 [PeakExtreme] read=12.35, obs=75.02, reasons=read_below_near;read_very_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;dependency_depth_low
- order 144 [Hard] read=17.15, obs=74.17, reasons=read_below_near;too_many_openers;ray_too_local
- order 148 [Hard] read=12.79, obs=78.4, reasons=read_below_near;read_very_low;too_many_openers;ray_too_local;adjacent_blockers_saturated
- order 164 [Hard] read=0, obs=60.03, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;cross_region_low;dependency_depth_low
- order 189 [PeakExtreme] read=0.58, obs=59.29, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;dependency_depth_low
- order 199 [Hard] read=0, obs=44.32, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;cross_region_low;dependency_depth_low;dependency_density_low
- order 204 [Hard] read=0.66, obs=60.41, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;dependency_depth_low
- order 208 [Hard] read=0, obs=47.55, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;cross_region_low;dependency_depth_low;dependency_density_low
- order 239 [PeakExtreme] read=3.2, obs=58.29, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;dependency_depth_low
- order 256 [PeakExtreme] read=6.92, obs=62.65, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;dependency_depth_low
- order 264 [Hard] read=6.92, obs=62.65, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;dependency_depth_low
- order 269 [PeakExtreme] read=0, obs=48.83, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;cross_region_low;dependency_depth_low;dependency_density_low
- order 276 [PeakExtreme] read=0.98, obs=57.19, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;dependency_depth_low
- order 284 [Hard] read=9.32, obs=73.13, reasons=read_below_near;read_very_low;too_many_openers;ray_too_local;adjacent_blockers_saturated
- order 287 [PeakExtreme] read=5.19, obs=61.93, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;ray_too_local;adjacent_blockers_saturated
- order 289 [Hard] read=6.73, obs=75.05, reasons=read_below_near;read_very_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;dependency_depth_low
- order 296 [Hard] read=0.29, obs=40.29, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;dependency_depth_low;dependency_density_low
- order 299 [PeakExtreme] read=0, obs=26.97, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;cross_region_low;dependency_depth_low;dependency_density_low

## Interpretation
- ReadKeep rows are the best current hard/peak survivors for visual read demand.
- RegenPriority rows need replacement if the goal is harder observation-first play.
- Adjacent blocker saturation is still reported as a reason even for some ReadKeep rows, because current pool has almost no long lookahead rays.
