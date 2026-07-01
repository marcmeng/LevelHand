# Campaign Read-Demand Gate V1

- Metrics CSV: .codex-run/c5v4f_normal_all_observation_pressure_v1.csv
- Output CSV: .codex-run/c5v4f_normal_all_read_demand_gate_v1.csv
- Keep threshold: readDemand >= 28 plus static dependency, opener, cross-region, and depth guards
- Near threshold: readDemand >= 25 with relaxed cross-region/ray guards

## Gate Counts
- ReadKeep: 32
- ReadNear: 84
- RegenPriority: 201
- RegenPriorityHigh: 33

## ReadKeep
- order 197 [Normal] read=32.35, obs=80.27, cross=0.3333, depth=11, lane=NeutralMixed
- order 29 [PeakExtreme] read=31.88, obs=79.59, cross=0.3103, depth=11, lane=PressurePeak
- order 160 [Normal] read=30.77, obs=78.93, cross=0.2879, depth=13, lane=NeutralMixed
- order 194 [Normal] read=30.64, obs=79.96, cross=0.3235, depth=9, lane=NeutralMixed
- order 371 [Normal] read=30.64, obs=79.96, cross=0.3235, depth=9, lane=
- order 109 [PeakExtreme] read=30.22, obs=78.58, cross=0.2791, depth=12, lane=PressurePeak
- order 241 [Recovery] read=29.94, obs=78.8, cross=0.2879, depth=12, lane=PeelHard
- order 56 [PeakExtreme] read=29.72, obs=81.13, cross=0.36, depth=7, lane=maze_long_chain
- order 145 [Recovery] read=29.6, obs=78.17, cross=0.2703, depth=10, lane=PeelMid
- order 124 [Normal] read=29.54, obs=78.17, cross=0.2658, depth=11, lane=PeelMid
- order 305 [Hard] read=29.54, obs=78.17, cross=0.2658, depth=11, lane=
- order 186 [Recovery] read=29.46, obs=78.88, cross=0.2899, depth=10, lane=Campaign500_HardGateUntil0910
- order 195 [Recovery] read=29.42, obs=78.85, cross=0.2899, depth=10, lane=FlowPatch
- order 79 [Recovery] read=29.36, obs=79.01, cross=0.2917, depth=10, lane=NeutralMixed
- order 249 [Hard] read=29.14, obs=81.2, cross=0.3617, depth=8, lane=maze_long_chain
- order 231 [Recovery] read=29.03, obs=77.84, cross=0.2576, depth=10, lane=NeutralMixed
- order 345 [Recovery] read=29.03, obs=77.84, cross=0.2576, depth=10, lane=
- order 206 [Normal] read=28.98, obs=77.84, cross=0.2535, depth=10, lane=PeelHard
- order 191 [Normal] read=28.93, obs=77.76, cross=0.2571, depth=11, lane=Campaign500_HardGateUntil0910
- order 432 [Hard] read=28.62, obs=77.08, cross=0.2292, depth=15, lane=PeelMid

## ReadNear
- order 259 [Recovery] read=27.97, obs=78.84, cross=0.2857, depth=10, lane=NeutralMixed
- order 422 [Normal] read=27.97, obs=78.84, cross=0.2857, depth=10, lane=
- order 211 [Recovery] read=27.93, obs=77.19, cross=0.2353, depth=11, lane=NeutralMixed
- order 222 [Normal] read=27.93, obs=77.11, cross=0.2424, depth=11, lane=NeutralMixed
- order 271 [Recovery] read=27.93, obs=78, cross=0.2656, depth=9, lane=Campaign500_HardGateUntil0910
- order 495 [Recovery] read=27.93, obs=77.11, cross=0.2424, depth=11, lane=
- order 76 [PeakExtreme] read=27.88, obs=77.17, cross=0.2319, depth=11, lane=maze_long_chain
- order 352 [Hard] read=27.83, obs=76.94, cross=0.2245, depth=15, lane=PeelMid
- order 250 [Normal] read=27.72, obs=78.27, cross=0.2698, depth=11, lane=NeutralMixed
- order 464 [Hard] read=27.72, obs=78.27, cross=0.2698, depth=11, lane=
- order 216 [PeakExtreme] read=27.67, obs=77.85, cross=0.2545, depth=10, lane=maze_long_chain
- order 57 [Normal] read=27.62, obs=77.01, cross=0.2278, depth=10, lane=PeelLight
- order 455 [Normal] read=27.62, obs=77.01, cross=0.2278, depth=10, lane=
- order 265 [Recovery] read=27.57, obs=76.95, cross=0.2295, depth=13, lane=Campaign500_HardGateUntil0910
- order 396 [PeakExtreme] read=27.35, obs=76.85, cross=0.2222, depth=11, lane=PressurePeak
- order 111 [Recovery] read=27.34, obs=77.69, cross=0.25, depth=9, lane=PressurePeak
- order 80 [Normal] read=27.27, obs=77.31, cross=0.2394, depth=11, lane=FlowRail
- order 414 [Recovery] read=27.27, obs=77.31, cross=0.2394, depth=11, lane=
- order 49 [Hard] read=27.09, obs=77.64, cross=0.2468, depth=12, lane=PeelLight
- order 482 [Normal] read=27.09, obs=77.64, cross=0.2468, depth=12, lane=

## RegenPriorityHigh First 30
- order 1 [Normal] read=0, obs=16.8, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;adjacent_blockers_saturated;dependency_depth_low;dependency_density_low
- order 3 [Normal] read=4.1, obs=72.99, reasons=read_below_near;read_very_low;ray_too_local;adjacent_blockers_saturated;cross_region_low;dependency_depth_low
- order 18 [Normal] read=11.34, obs=77.12, reasons=read_below_near;read_very_low;ray_too_local;adjacent_blockers_saturated
- order 20 [Normal] read=11.72, obs=78.58, reasons=read_below_near;read_very_low;too_many_openers;ray_too_local;adjacent_blockers_saturated
- order 86 [Normal] read=14.67, obs=77.22, reasons=read_below_near;read_very_low;ray_too_local;adjacent_blockers_saturated
- order 100 [Normal] read=5.82, obs=72.89, reasons=read_below_near;read_very_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;cross_region_low
- order 104 [Hard] read=9.95, obs=74.06, reasons=read_below_near;read_very_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;dependency_depth_low
- order 119 [PeakExtreme] read=12.35, obs=75.02, reasons=read_below_near;read_very_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;dependency_depth_low
- order 134 [Normal] read=14.25, obs=75.91, reasons=read_below_near;read_very_low;ray_too_local;adjacent_blockers_saturated;cross_region_low
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
- order 272 [Normal] read=0, obs=52.62, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;dependency_depth_low;dependency_density_low
- order 276 [PeakExtreme] read=0.98, obs=57.19, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;dependency_depth_low
- order 284 [Hard] read=9.32, obs=73.13, reasons=read_below_near;read_very_low;too_many_openers;ray_too_local;adjacent_blockers_saturated
- order 286 [Normal] read=0, obs=60.55, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;cross_region_low;dependency_depth_low
- order 287 [PeakExtreme] read=5.19, obs=61.93, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;ray_too_local;adjacent_blockers_saturated
- order 289 [Hard] read=6.73, obs=75.05, reasons=read_below_near;read_very_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;dependency_depth_low
- order 290 [Recovery] read=7.06, obs=40.06, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;ray_too_local;dependency_depth_low;dependency_density_low
- order 294 [Recovery] read=7.06, obs=40.06, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;ray_too_local;dependency_depth_low;dependency_density_low
- order 295 [Normal] read=13.87, obs=77.09, reasons=read_below_near;read_very_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;dependency_depth_low
- order 296 [Hard] read=0.29, obs=40.29, reasons=read_below_near;read_very_low;static_dependency_low;too_many_openers;ray_too_local;adjacent_blockers_saturated;dependency_depth_low;dependency_density_low

## Interpretation
- ReadKeep rows are the best current hard/peak survivors for visual read demand.
- RegenPriority rows need replacement if the goal is harder observation-first play.
- Adjacent blocker saturation is still reported as a reason even for some ReadKeep rows, because current pool has almost no long lookahead rays.
