# Choke Candidate Audit All Known Sources 2026-07-02

- Scope: existing known candidate/trace/rerank CSVs only; no new generation.
- Input CSV files: 366
- Raw scored rows: 9162
- Unique asset candidates: 3049
- StrongChokeReview: 29
- ChokeReview including strong: 108
- ChokeSignalRisky: 96
- Campaign/order-tagged review candidates: 0

## Top Sources For Review Candidates
- sgp_pressure_read_demand_v1_choke_mutation_v2_drainbreak5_composite_review_v1_srcapped4_ranked.csv: 17
- sgp_pressure_read_demand_v1_choke_mutation_v2_repair_plan_trace1_review_v1_ranked.csv: 15
- sgp_pressure_read_demand_v1_choke_mutation_v2_drainbreak2_metrics.csv: 12
- sgp_pressure_read_demand_v1_choke_mutation_v2_overlap_bundle_repair_trace1_review_v1_ranked.csv: 11
- sgp_pressure_read_demand_v1_choke_mutation_v2_residual2_review_v1_ranked.csv: 11
- sgp_pressure_read_demand_v1_choice_value_v2_review_ranked.csv: 6
- sgp_pressure_read_demand_v1_pool50_smoke1_strict_review_keep.csv: 5
- sgp_pressure_read_demand_v1_choke_mutation_v2_localbreak1_review_v1_ranked.csv: 4
- campaign500_trace_v2_calibration30_chunk2_metrics.csv: 3
- sgp_pressure_read_demand_v1_choke_mutation_v2_localbreak3_review_v1_ranked.csv: 3
- sgp_pressure_read_demand_v1_choke_mutation_v2_drainbreak5_composite_review_v1_review_keep.csv: 3
- sgp_pressure_read_demand_v1_choke_break_wrapper_smoke2_remote_metrics_ranked.csv: 2
- sgp_pressure_read_demand_v1_choice_value_v2_review5_diag_metrics.csv: 2
- sgp_pressure_read_demand_v1_choke_mutation_v2_residual2_review_v1_review_keep.csv: 2
- campaign500_trace_v2_calibration30_chunk1_process_keep.csv: 2
- campaign500_trace_v2_calibration30_chunk1_metrics.csv: 2
- sgp_pressure_read_demand_v1_choke_mutation_v2_drainbreak5_composite_review_v1_srcapped4_review_keep.csv: 1
- sgp_pressure_read_demand_v1_choke_mutation_v2_drainbreak5_composite_review_v1_ranked.csv: 1
- campaign500_trace_v2_calibration30_chunk4_metrics.csv: 1
- sgp_pressure_read_demand_v1_scheduled_break_smoke1_metrics.csv: 1

## Top 30 By Choke Score
| rank | class | score | levelId | order | avg/max | remote | composite | afterFrontier | meaningful | local/near/follow | risk | source |
| ---: | --- | ---: | --- | ---: | --- | ---: | ---: | ---: | ---: | --- | --- | --- |
| 1 | StrongChokeReview | 105.9 | rdsb_03_sgp_pressure_hard_rect_read_demand_v1_scheduled_break_sb_lba |  | 4.08/7 | 4 | 2 | 2 | 0.363 | 4/4/6 | flatConsequenceHigh | sgp_pressure_read_demand_v1_scheduled_break_smoke7_metrics.csv |
| 2 | ChokeReview | 88.445 | rdsb_11_sgp_pressure_hard_rect_read_demand_v1_scheduled_break_sb_lbc |  | 4.98/9 | 8 | 2 | 1 | 0.324 | 6/3/4 |  | sgp_pressure_read_demand_v1_scheduled_break_smoke11_metrics.csv |
| 3 | ChokeSignalRisky | 76 | nutation_longchain_candidate80_v1_72_nutation_longchain_spine_v1_rect_c80_18_d4_weave_spine |  | 3.71/8 | 2 | 3 | 0 | 0.434 | 7/4/5 | localRunHigh | campaign500_trace_v2_calibration30_chunk4_metrics.csv |
| 4 | ChokeReview | 75.563 | rdsb_05_sgp_pressure_hard_rect_read_demand_v1_scheduled_break_sb_dwb |  | 5.29/10 | 5 | 2 | 0 | 0.316 | 6/2/4 |  | sgp_pressure_read_demand_v1_scheduled_break_smoke1_metrics.csv |
| 5 | StrongChokeReview | 69.703 | c5nep_w10smp_35_nutation_strict_mixed_peak_v1_rect_s22_o216_v02_strictmixedpeak_mixed_chain |  | 4.87/9 | 2 | 2 | 0 | 0.491 | 5/5/4 |  | campaign500_trace_v2_calibration30_chunk4_metrics.csv |
| 6 | StrongChokeReview | 59.633 | nutation_longchain_candidate80_holefix_v1_03_nutation_longchain_patch_v1_rect_c80hf_01_d1_lock_patch |  | 4.62/8 | 2 | 1 | 0 | 0.397 | 5/5/3 |  | campaign500_trace_v2_calibration30_chunk2_metrics.csv |
| 7 | StrongChokeReview | 57.344 | sgp_rdcm_v2_s02_01_c16 |  | 4.62/8 | 2 | 1 | 0 | 0.378 | 5/3/4 |  | sgp_pressure_read_demand_v1_choice_value_v2_review5_diag_metrics.csv |
| 8 | StrongChokeReview | 55.42 | sgp_rdcm_v2_s02_07_c13 |  | 4.56/7 | 3 | 0 | 0 | 0.326 | 5/3/5 |  | sgp_pressure_read_demand_v1_choke_mutation_v2_drainbreak2_metrics.csv |
| 9 | StrongChokeReview | 51.709 | sgp_rdcm_v2_s01_01_c46 |  | 4.42/8 | 2 | 0 | 0 | 0.391 | 4/4/4 |  | sgp_pressure_read_demand_v1_choice_value_v2_review_ranked.csv |
| 10 | ChokeSignalRisky | 47.501 | nutation_longchain_candidate80_holefix_s2_v1_47_nutation_longchain_patch_v1_rect_c80hf2_12_d3_maze_patch |  | 6.77/11 | 2 | 1 | 0 | 0.587 | 6/4/6 | choicePeakHigh | campaign500_trace_v2_calibration30_chunk4_metrics.csv |
| 11 | ChokeReview | 46.798 | nutation_hub_mixed_v1_strict30_transform_wide_08_s02_rot180 |  | 5.99/10 | 2 | 1 | 0 | 0.478 | 6/5/5 | flatConsequenceHigh | campaign500_trace_v2_calibration30_chunk3_metrics.csv |
| 12 | StrongChokeReview | 45.114 | sgp_rdcm_v2_s02_03_c3 |  | 4.94/8 | 2 | 0 | 0 | 0.415 | 4/3/5 |  | sgp_pressure_read_demand_v1_choice_value_v2_review_ranked.csv |
| 13 | ChokeReview | 41.394 | campaign500_longchain_prod200_pool_v1_02_l003_long_normal_a_normal_s01_slot_seedweave_braid_carrier |  | 4.07/8 | 1 | 1 | 0 | 0.306 | 3/3/1 |  | campaign500_trace_v2_calibration30_chunk1_process_keep.csv |
| 14 | ChokeSignalRisky | 41.361 | rdsb_08_sgp_pressure_hard_rect_read_demand_v1_scheduled_break_sb_sub |  | 5.84/11 | 2 | 1 | 0 | 0.317 | 4/3/3 | choicePeakHigh;localOnlyHigh;flatConsequenceHigh | sgp_pressure_read_demand_v1_scheduled_break_smoke1_metrics.csv |
| 15 | ChokeReview | 40.446 | nutation_longchain_candidate80_holefix_v1_15_nutation_longchain_patch_v1_rect_c80hf_04_d1_core_patch |  | 6.17/10 | 2 | 0 | 0 | 0.397 | 4/4/3 |  | campaign500_trace_v2_calibration30_chunk2_metrics.csv |
| 16 | ChokeSignalRisky | 38.637 | rdsb_01_sgp_pressure_hard_rect_read_demand_v1_scheduled_break_sb_dwa |  | 7.24/13 | 4 | 0 | 0 | 0.208 | 4/3/3 | choicePeakHigh;localOnlyHigh;flatConsequenceHigh | sgp_pressure_read_demand_v1_scheduled_break_smoke1_metrics.csv |
| 17 | ChokeSignalRisky | 38.406 | campaign500_shape_psg_next10_tail9_02_L083_ballmouse |  | 3.76/8 | 0 | 3 | 1 | 0.324 | 9/2/7 | localRunHigh;depFollowHigh | campaign500_trace_v2_calibration30_chunk3_metrics.csv |
| 18 | ChokeReview | 37.021 | sgp_rdcm_v2_s03_05_t42_c5 |  | 4.92/9 | 1 | 0 | 0 | 0.446 | 4/3/4 | flatConsequenceHigh | sgp_pressure_read_demand_v1_choke_mutation_v2_drainbreak2_metrics.csv |
| 19 | ChokeReview | 37.014 | sgp_rdcm_v2_s01_03_c27 |  | 4.58/8 | 1 | 0 | 0 | 0.397 | 4/4/4 |  | sgp_pressure_read_demand_v1_choice_value_v2_review_ranked.csv |
| 20 | ChokeReview | 36.931 | sgp_rdcm_v2_s03_09_c5 |  | 4.86/9 | 1 | 0 | 0 | 0.446 | 4/3/4 | flatConsequenceHigh | sgp_pressure_read_demand_v1_choke_mutation_v2_drainbreak2_metrics.csv |
| 21 | ChokeReview | 36.685 | sgp_rdcm_v2_s01_02_c22 |  | 4.61/8 | 1 | 0 | 0 | 0.392 | 4/4/4 |  | sgp_pressure_read_demand_v1_choice_value_v2_review_ranked.csv |
| 22 | StrongChokeReview | 35.548 | sgp_rdcm_v2rp_r01_06_rfp55_c33 |  | 4.14/7 | 0 | 3 | 0 | 0 | 3/3/4 |  | sgp_pressure_read_demand_v1_choke_mutation_v2_overlap_bundle_repair_trace1_review_v1_ranked.csv |
| 23 | StrongChokeReview | 35.548 | sgp_rdcm_v2rp_r01_04_rfp55_c33 |  | 4.14/7 | 0 | 3 | 0 | 0 | 3/3/4 |  | sgp_pressure_read_demand_v1_choke_mutation_v2_repair_plan_trace1_review_v1_ranked.csv |
| 24 | StrongChokeReview | 35.548 | sgp_rdcm_v2rp_r01_01_rfp55_t36_c33 |  | 4.14/7 | 0 | 3 | 0 | 0 | 3/3/4 |  | sgp_pressure_read_demand_v1_choke_mutation_v2_repair_plan_trace1_review_v1_ranked.csv |
| 25 | ChokeReview | 31.406 | campaign500_normal_full_v1_s01_s13_123_nutation_flow_rail_v1_rect_s11_o102_v02_flowrail_rail_chain |  | 4.79/8 | 1 | 0 | 0 | 0.469 | 6/3/4 |  | campaign500_trace_v2_calibration30_chunk1_metrics.csv |
| 26 | StrongChokeReview | 30.885 | sgp_rdcm_v2_s04_02_c51 |  | 4.73/8 | 0 | 1 | 0 | 0.616 | 5/3/4 |  | sgp_pressure_read_demand_v1_choice_value_v2_review5_diag_metrics.csv |
| 27 | StrongChokeReview | 27.753 | sgp_rdcm_v2_s04_06_t12 |  | 4.77/8 | 0 | 0 | 0 | 0.585 | 5/3/4 |  | sgp_pressure_read_demand_v1_choke_mutation_v2_trace1_review_v1_review_keep.csv |
| 28 | StrongChokeReview | 27.556 | sgp_rdcm_v2rp_r01_01_rfp55_rfp52_t36 |  | 4.25/8 | 0 | 2 | 0 | 0 | 3/3/4 |  | sgp_pressure_read_demand_v1_choke_mutation_v2_overlap_bundle_repair_trace1_review_v1_ranked.csv |
| 29 | StrongChokeReview | 27.556 | sgp_rdcm_v2rp_r01_03_rfp55_c22 |  | 4.25/8 | 0 | 2 | 0 | 0 | 3/3/4 |  | sgp_pressure_read_demand_v1_choke_mutation_v2_repair_plan_trace1_review_v1_ranked.csv |
| 30 | StrongChokeReview | 27.556 | sgp_rdcm_v2rp_r01_05_rfp55 |  | 4.25/8 | 0 | 2 | 0 | 0 | 3/3/4 |  | sgp_pressure_read_demand_v1_choke_mutation_v2_repair_plan_trace1_review_v1_ranked.csv |

## Campaign/Order-Tagged Review Candidates
| rank | class | score | order | levelId | avg/max | remote | composite | local/near/follow | risk |
| ---: | --- | ---: | ---: | --- | --- | ---: | ---: | --- | --- |
