# Choke Candidate Audit 2026-07-02

- Scope: existing trace/rerank outputs only; no new generation.
- Input CSV files: 26
- Raw scored rows: 433
- Unique asset candidates: 219
- StrongChokeReview: 29
- ChokeReview including strong: 107
- ChokeSignalRisky: 56

## Interpretation

- Strong means solved/process A/B/S, maxChoices<=9, local/near runs controlled, and at least one strong remote/composite/choice-value choke signal.
- Review means there is a usable choke signal but it is weaker or closer to risk bounds.
- Risky means the row has some choke-like evidence but likely still suffers from local runs, choice peaks, or weak solved/process evidence.

## Top 20 By Choke Score

| rank | class | score | levelId | order | avg/max | remote | composite | afterFrontier | meaningful | local/near/follow | risk | source |
| ---: | --- | ---: | --- | ---: | --- | ---: | ---: | ---: | ---: | --- | --- | --- |
| 1 | StrongChokeReview | 105.9 | rdsb_03_sgp_pressure_hard_rect_read_demand_v1_scheduled_break_sb_lba |  | 4.08/7 | 4 | 2 | 2 | 0.363 | 4/4/6 | flatConsequenceHigh | sgp_pressure_read_demand_v1_scheduled_break_smoke7_metrics.csv |
| 2 | ChokeReview | 88.445 | rdsb_11_sgp_pressure_hard_rect_read_demand_v1_scheduled_break_sb_lbc |  | 4.98/9 | 8 | 2 | 1 | 0.324 | 6/3/4 |  | sgp_pressure_read_demand_v1_scheduled_break_smoke11_metrics.csv |
| 3 | ChokeReview | 75.563 | rdsb_05_sgp_pressure_hard_rect_read_demand_v1_scheduled_break_sb_dwb |  | 5.29/10 | 5 | 2 | 0 | 0.316 | 6/2/4 |  | sgp_pressure_read_demand_v1_scheduled_break_smoke1_metrics.csv |
| 4 | StrongChokeReview | 57.344 | sgp_rdcm_v2_s02_01_c16 |  | 4.62/8 | 2 | 1 | 0 | 0.378 | 5/3/4 |  | sgp_pressure_read_demand_v1_choice_value_v2_review5_diag_metrics.csv |
| 5 | StrongChokeReview | 55.42 | sgp_rdcm_v2_s02_07_c13 |  | 4.56/7 | 3 | 0 | 0 | 0.326 | 5/3/5 |  | sgp_pressure_read_demand_v1_choke_mutation_v2_drainbreak2_metrics.csv |
| 6 | ChokeSignalRisky | 53.253 | nutation_longchain_candidate80_v1_72_nutation_longchain_spine_v1_rect_c80_18_d4_weave_spine | 112 | 3.71/8 | 2 | 3 | 0 | 0 | 7/4/5 | localRunHigh | campaign500_trace_v2_calibration30_metrics_partial20_joined_selection.csv |
| 7 | StrongChokeReview | 51.709 | sgp_rdcm_v2_s01_01_c46 |  | 4.42/8 | 2 | 0 | 0 | 0.391 | 4/4/4 |  | sgp_pressure_read_demand_v1_choice_value_v2_review_ranked.csv |
| 8 | StrongChokeReview | 51.104 | c5nep_w10smp_35_nutation_strict_mixed_peak_v1_rect_s22_o216_v02_strictmixedpeak_mixed_chain | 100 | 4.87/9 | 2 | 2 | 0 | 0 | 5/5/4 |  | campaign500_trace_v2_calibration30_metrics_partial20_joined_selection.csv |
| 9 | StrongChokeReview | 45.114 | sgp_rdcm_v2_s02_03_c3 |  | 4.94/8 | 2 | 0 | 0 | 0.415 | 4/3/5 |  | sgp_pressure_read_demand_v1_choice_value_v2_review_ranked.csv |
| 10 | ChokeSignalRisky | 41.361 | rdsb_08_sgp_pressure_hard_rect_read_demand_v1_scheduled_break_sb_sub |  | 5.84/11 | 2 | 1 | 0 | 0.317 | 4/3/3 | choicePeakHigh;localOnlyHigh;flatConsequenceHigh | sgp_pressure_read_demand_v1_scheduled_break_smoke1_metrics.csv |
| 11 | StrongChokeReview | 39.174 | nutation_longchain_candidate80_holefix_v1_03_nutation_longchain_patch_v1_rect_c80hf_01_d1_lock_patch | 25 | 4.62/8 | 2 | 1 | 0 | 0 | 5/5/3 |  | campaign500_trace_v2_calibration30_metrics_partial20_joined_selection.csv |
| 12 | ChokeSignalRisky | 38.637 | rdsb_01_sgp_pressure_hard_rect_read_demand_v1_scheduled_break_sb_dwa |  | 7.24/13 | 4 | 0 | 0 | 0.208 | 4/3/3 | choicePeakHigh;localOnlyHigh;flatConsequenceHigh | sgp_pressure_read_demand_v1_scheduled_break_smoke1_metrics.csv |
| 13 | ChokeReview | 37.021 | sgp_rdcm_v2_s03_05_t42_c5 |  | 4.92/9 | 1 | 0 | 0 | 0.446 | 4/3/4 | flatConsequenceHigh | sgp_pressure_read_demand_v1_choke_mutation_v2_drainbreak2_metrics.csv |
| 14 | ChokeReview | 37.014 | sgp_rdcm_v2_s01_03_c27 |  | 4.58/8 | 1 | 0 | 0 | 0.397 | 4/4/4 |  | sgp_pressure_read_demand_v1_choice_value_v2_review_ranked.csv |
| 15 | ChokeReview | 36.931 | sgp_rdcm_v2_s03_09_c5 |  | 4.86/9 | 1 | 0 | 0 | 0.446 | 4/3/4 | flatConsequenceHigh | sgp_pressure_read_demand_v1_choke_mutation_v2_drainbreak2_metrics.csv |
| 16 | ChokeReview | 36.685 | sgp_rdcm_v2_s01_02_c22 |  | 4.61/8 | 1 | 0 | 0 | 0.392 | 4/4/4 |  | sgp_pressure_read_demand_v1_choice_value_v2_review_ranked.csv |
| 17 | StrongChokeReview | 35.548 | sgp_rdcm_v2rp_r01_04_rfp55_c33 |  | 4.14/7 | 0 | 3 | 0 | 0 | 3/3/4 |  | sgp_pressure_read_demand_v1_choke_mutation_v2_repair_plan_trace1_review_v1_ranked.csv |
| 18 | StrongChokeReview | 35.548 | sgp_rdcm_v2rp_r01_01_rfp55_t36_c33 |  | 4.14/7 | 0 | 3 | 0 | 0 | 3/3/4 |  | sgp_pressure_read_demand_v1_choke_mutation_v2_repair_plan_trace1_review_v1_ranked.csv |
| 19 | StrongChokeReview | 35.548 | sgp_rdcm_v2rp_r01_06_rfp55_c33 |  | 4.14/7 | 0 | 3 | 0 | 0 | 3/3/4 |  | sgp_pressure_read_demand_v1_choke_mutation_v2_overlap_bundle_repair_trace1_review_v1_ranked.csv |
| 20 | StrongChokeReview | 30.885 | sgp_rdcm_v2_s04_02_c51 |  | 4.73/8 | 0 | 1 | 0 | 0.616 | 5/3/4 |  | sgp_pressure_read_demand_v1_choice_value_v2_review5_diag_metrics.csv |

## Output Files

- `choke_candidate_audit_review_candidates.csv`: practical review shortlist.
- `choke_candidate_audit_strong_candidates.csv`: stricter subset.
- `choke_candidate_audit_best_by_asset.csv`: deduped all candidates by asset.
- `choke_candidate_audit_raw_scored.csv`: all scored rows before dedupe.
