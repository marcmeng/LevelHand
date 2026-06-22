#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using PixelBug.ArrowMagic;
using UnityEditor;
using UnityEngine;

namespace PixelBug.ArrowMagic.EditorTools
{
    public static class Campaign500OptimizationRoundRunner
    {
        const string SourcePackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PlanPreviewPack.asset";
        const string ShapeRefreshV1PackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV1Pack.asset";
        const string ShapeRefreshV1ReplacementPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/campaign500_shape_refresh_v1_replacements.csv";
        const string ShapeRefreshV1ImportedFolder = "Assets/ArrowMagic/SOData/Levels/Campaign500Optimization/ShapeRefreshV1";
        const string ShapeRefreshV2PackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV2Pack.asset";
        const string ShapeRefreshV2ReplacementPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/campaign500_shape_refresh_v2_replacement_plan.csv";
        const string ShapeRefreshV3PackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV3Pack.asset";
        const string ShapeRefreshV3ReplacementPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/campaign500_shape_refresh_v3_replacement_plan.csv";
        const string ShapeRefreshV4PackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV4Pack.asset";
        const string ShapeRefreshV4ReplacementPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/campaign500_shape_refresh_v4_replacement_plan.csv";
        const string ShapeRefreshV5PackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV5Pack.asset";
        const string ShapeRefreshV5ReplacementPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/campaign500_shape_refresh_v5_replacement_plan.csv";
        const string ShapeRefreshV6PackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV6Pack.asset";
        const string ShapeRefreshV6ReplacementPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/campaign500_shape_refresh_v6_replacement_plan.csv";
        const string ShapeRefreshV7PackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV7Pack.asset";
        const string ShapeRefreshV7ReplacementPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/campaign500_shape_refresh_v7_replacement_plan.csv";
        const string ShapeRefreshV8PackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV8Pack.asset";
        const string ShapeRefreshV8ReplacementPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/campaign500_shape_refresh_v8_replacement_plan.csv";
        const string ShapeRefreshV9PackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV9Pack.asset";
        const string ShapeRefreshV9ReplacementPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/campaign500_shape_refresh_v9_replacement_plan.csv";
        const string ShapeRefreshV10PackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV10Pack.asset";
        const string ShapeRefreshV10ReplacementPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/campaign500_shape_refresh_v10_replacement_plan.csv";
        const string ShapeRefreshV11PackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV11Pack.asset";
        const string ShapeRefreshV11ReplacementPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/campaign500_shape_refresh_v11_replacement_plan.csv";
        const string Campaign500FinalPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500FinalPack.asset";
        const string SelectionPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/campaign500_plan_selection.csv";
        const string RhythmPlanPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/campaign500_21_500_relative_peaks_plan.csv";
        const string ReportFolder = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/ShapeRefreshV1Validation";
        const string SummaryPath = ReportFolder + "/campaign500_shape_refresh_v1_validation_summary.csv";
        const string FlaggedPath = ReportFolder + "/campaign500_shape_refresh_v1_validation_flags.csv";
        const string NotesPath = ReportFolder + "/campaign500_shape_refresh_v1_validation_notes.md";
        const string ReviewPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV1ReviewPack.asset";
        const string V2ReportFolder = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/ShapeRefreshV2Validation";
        const string V2SummaryPath = V2ReportFolder + "/campaign500_shape_refresh_v2_validation_summary.csv";
        const string V2FlaggedPath = V2ReportFolder + "/campaign500_shape_refresh_v2_validation_flags.csv";
        const string V2NotesPath = V2ReportFolder + "/campaign500_shape_refresh_v2_validation_notes.md";
        const string V2ReviewPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV2ReviewPack.asset";
        const string V3ReportFolder = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/ShapeRefreshV3Validation";
        const string V3SummaryPath = V3ReportFolder + "/campaign500_shape_refresh_v3_validation_summary.csv";
        const string V3FlaggedPath = V3ReportFolder + "/campaign500_shape_refresh_v3_validation_flags.csv";
        const string V3NotesPath = V3ReportFolder + "/campaign500_shape_refresh_v3_validation_notes.md";
        const string V3ReviewPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV3ReviewPack.asset";
        const string V4ReportFolder = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/ShapeRefreshV4Validation";
        const string V4SummaryPath = V4ReportFolder + "/campaign500_shape_refresh_v4_validation_summary.csv";
        const string V4FlaggedPath = V4ReportFolder + "/campaign500_shape_refresh_v4_validation_flags.csv";
        const string V4NotesPath = V4ReportFolder + "/campaign500_shape_refresh_v4_validation_notes.md";
        const string V4ReviewPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV4ReviewPack.asset";
        const string V5ReportFolder = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/ShapeRefreshV5Validation";
        const string V5SummaryPath = V5ReportFolder + "/campaign500_shape_refresh_v5_validation_summary.csv";
        const string V5FlaggedPath = V5ReportFolder + "/campaign500_shape_refresh_v5_validation_flags.csv";
        const string V5NotesPath = V5ReportFolder + "/campaign500_shape_refresh_v5_validation_notes.md";
        const string V5ReviewPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV5ReviewPack.asset";
        const string V6ReportFolder = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/ShapeRefreshV6Validation";
        const string V6SummaryPath = V6ReportFolder + "/campaign500_shape_refresh_v6_validation_summary.csv";
        const string V6FlaggedPath = V6ReportFolder + "/campaign500_shape_refresh_v6_validation_flags.csv";
        const string V6NotesPath = V6ReportFolder + "/campaign500_shape_refresh_v6_validation_notes.md";
        const string V6ReviewPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV6ReviewPack.asset";
        const string V7ReportFolder = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/ShapeRefreshV7Validation";
        const string V7SummaryPath = V7ReportFolder + "/campaign500_shape_refresh_v7_validation_summary.csv";
        const string V7FlaggedPath = V7ReportFolder + "/campaign500_shape_refresh_v7_validation_flags.csv";
        const string V7NotesPath = V7ReportFolder + "/campaign500_shape_refresh_v7_validation_notes.md";
        const string V7ReviewPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV7ReviewPack.asset";
        const string V8ReportFolder = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/ShapeRefreshV8Validation";
        const string V8SummaryPath = V8ReportFolder + "/campaign500_shape_refresh_v8_validation_summary.csv";
        const string V8FlaggedPath = V8ReportFolder + "/campaign500_shape_refresh_v8_validation_flags.csv";
        const string V8NotesPath = V8ReportFolder + "/campaign500_shape_refresh_v8_validation_notes.md";
        const string V8ReviewPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV8ReviewPack.asset";
        const string V9ReportFolder = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/ShapeRefreshV9Validation";
        const string V9SummaryPath = V9ReportFolder + "/campaign500_shape_refresh_v9_validation_summary.csv";
        const string V9FlaggedPath = V9ReportFolder + "/campaign500_shape_refresh_v9_validation_flags.csv";
        const string V9NotesPath = V9ReportFolder + "/campaign500_shape_refresh_v9_validation_notes.md";
        const string V9ReviewPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV9ReviewPack.asset";
        const string V10ReportFolder = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/ShapeRefreshV10Validation";
        const string V10SummaryPath = V10ReportFolder + "/campaign500_shape_refresh_v10_validation_summary.csv";
        const string V10FlaggedPath = V10ReportFolder + "/campaign500_shape_refresh_v10_validation_flags.csv";
        const string V10NotesPath = V10ReportFolder + "/campaign500_shape_refresh_v10_validation_notes.md";
        const string V10ReviewPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV10ReviewPack.asset";
        const string V11ReportFolder = "Assets/ArrowMagic/SOData/Reports/Campaign500/OptimizationRound_20260618/ShapeRefreshV11Validation";
        const string V11SummaryPath = V11ReportFolder + "/campaign500_shape_refresh_v11_validation_summary.csv";
        const string V11FlaggedPath = V11ReportFolder + "/campaign500_shape_refresh_v11_validation_flags.csv";
        const string V11NotesPath = V11ReportFolder + "/campaign500_shape_refresh_v11_validation_notes.md";
        const string V11ReviewPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500ShapeRefreshV11ReviewPack.asset";
        const string DemoScenePath = "Assets/ArrowMagic/Scenes/Demo.unity";

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Validate Shape Refresh V1")]
        public static void ValidateShapeRefreshV1()
        {
            CampaignSingleLevelValidator.RunValidationForPack(
                ShapeRefreshV1PackPath,
                SelectionPath,
                RhythmPlanPath,
                ReportFolder,
                SummaryPath,
                FlaggedPath,
                NotesPath,
                ReviewPackPath,
                "Campaign500ShapeRefreshV1");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Rebuild And Validate Shape Refresh V1")]
        public static void RebuildAndValidateShapeRefreshV1()
        {
            if (!TryRebuildShapeRefreshV1PackFromReplacementCsv())
                return;

            ValidateShapeRefreshV1();
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Validate Shape Refresh V2")]
        public static void ValidateShapeRefreshV2()
        {
            CampaignSingleLevelValidator.RunValidationForPack(
                ShapeRefreshV2PackPath,
                SelectionPath,
                RhythmPlanPath,
                V2ReportFolder,
                V2SummaryPath,
                V2FlaggedPath,
                V2NotesPath,
                V2ReviewPackPath,
                "Campaign500ShapeRefreshV2");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Rebuild And Validate Shape Refresh V2")]
        public static void RebuildAndValidateShapeRefreshV2()
        {
            if (!TryRebuildPackFromReplacementCsv(
                    ShapeRefreshV1PackPath,
                    ShapeRefreshV2ReplacementPath,
                    ShapeRefreshV2PackPath,
                    "campaign500_shape_refresh_v2",
                    "Campaign 500 Shape Refresh V2",
                    "Campaign500ShapeRefreshV2"))
            {
                return;
            }

            ValidateShapeRefreshV2();
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Validate Shape Refresh V3")]
        public static void ValidateShapeRefreshV3()
        {
            CampaignSingleLevelValidator.RunValidationForPack(
                ShapeRefreshV3PackPath,
                SelectionPath,
                RhythmPlanPath,
                V3ReportFolder,
                V3SummaryPath,
                V3FlaggedPath,
                V3NotesPath,
                V3ReviewPackPath,
                "Campaign500ShapeRefreshV3");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Rebuild And Validate Shape Refresh V3")]
        public static void RebuildAndValidateShapeRefreshV3()
        {
            if (!TryRebuildPackFromReplacementCsv(
                    ShapeRefreshV2PackPath,
                    ShapeRefreshV3ReplacementPath,
                    ShapeRefreshV3PackPath,
                    "campaign500_shape_refresh_v3",
                    "Campaign 500 Shape Refresh V3",
                    "Campaign500ShapeRefreshV3"))
            {
                return;
            }

            ValidateShapeRefreshV3();
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Validate Shape Refresh V4")]
        public static void ValidateShapeRefreshV4()
        {
            CampaignSingleLevelValidator.RunValidationForPack(
                ShapeRefreshV4PackPath,
                SelectionPath,
                RhythmPlanPath,
                V4ReportFolder,
                V4SummaryPath,
                V4FlaggedPath,
                V4NotesPath,
                V4ReviewPackPath,
                "Campaign500ShapeRefreshV4");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Rebuild And Validate Shape Refresh V4")]
        public static void RebuildAndValidateShapeRefreshV4()
        {
            if (!TryRebuildPackFromReplacementCsv(
                    ShapeRefreshV3PackPath,
                    ShapeRefreshV4ReplacementPath,
                    ShapeRefreshV4PackPath,
                    "campaign500_shape_refresh_v4",
                    "Campaign 500 Shape Refresh V4",
                    "Campaign500ShapeRefreshV4"))
            {
                return;
            }

            ValidateShapeRefreshV4();
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Validate Shape Refresh V5")]
        public static void ValidateShapeRefreshV5()
        {
            CampaignSingleLevelValidator.RunValidationForPack(
                ShapeRefreshV5PackPath,
                SelectionPath,
                RhythmPlanPath,
                V5ReportFolder,
                V5SummaryPath,
                V5FlaggedPath,
                V5NotesPath,
                V5ReviewPackPath,
                "Campaign500ShapeRefreshV5");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Rebuild And Validate Shape Refresh V5")]
        public static void RebuildAndValidateShapeRefreshV5()
        {
            if (!TryRebuildPackFromReplacementCsv(
                    ShapeRefreshV3PackPath,
                    ShapeRefreshV5ReplacementPath,
                    ShapeRefreshV5PackPath,
                    "campaign500_shape_refresh_v5",
                    "Campaign 500 Shape Refresh V5",
                    "Campaign500ShapeRefreshV5"))
            {
                return;
            }

            ValidateShapeRefreshV5();
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Validate Shape Refresh V6")]
        public static void ValidateShapeRefreshV6()
        {
            CampaignSingleLevelValidator.RunValidationForPack(
                ShapeRefreshV6PackPath,
                SelectionPath,
                RhythmPlanPath,
                V6ReportFolder,
                V6SummaryPath,
                V6FlaggedPath,
                V6NotesPath,
                V6ReviewPackPath,
                "Campaign500ShapeRefreshV6");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Rebuild And Validate Shape Refresh V6")]
        public static void RebuildAndValidateShapeRefreshV6()
        {
            if (!TryRebuildPackFromReplacementCsv(
                    ShapeRefreshV5PackPath,
                    ShapeRefreshV6ReplacementPath,
                    ShapeRefreshV6PackPath,
                    "campaign500_shape_refresh_v6",
                    "Campaign 500 Shape Refresh V6",
                    "Campaign500ShapeRefreshV6"))
            {
                return;
            }

            ValidateShapeRefreshV6();
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Validate Shape Refresh V7")]
        public static void ValidateShapeRefreshV7()
        {
            CampaignSingleLevelValidator.RunValidationForPack(
                ShapeRefreshV7PackPath,
                SelectionPath,
                RhythmPlanPath,
                V7ReportFolder,
                V7SummaryPath,
                V7FlaggedPath,
                V7NotesPath,
                V7ReviewPackPath,
                "Campaign500ShapeRefreshV7");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Rebuild And Validate Shape Refresh V7")]
        public static void RebuildAndValidateShapeRefreshV7()
        {
            if (!TryRebuildPackFromReplacementCsv(
                    ShapeRefreshV6PackPath,
                    ShapeRefreshV7ReplacementPath,
                    ShapeRefreshV7PackPath,
                    "campaign500_shape_refresh_v7",
                    "Campaign 500 Shape Refresh V7",
                    "Campaign500ShapeRefreshV7"))
            {
                return;
            }

            ValidateShapeRefreshV7();
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Validate Shape Refresh V8")]
        public static void ValidateShapeRefreshV8()
        {
            CampaignSingleLevelValidator.RunValidationForPack(
                ShapeRefreshV8PackPath,
                SelectionPath,
                RhythmPlanPath,
                V8ReportFolder,
                V8SummaryPath,
                V8FlaggedPath,
                V8NotesPath,
                V8ReviewPackPath,
                "Campaign500ShapeRefreshV8");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Rebuild And Validate Shape Refresh V8")]
        public static void RebuildAndValidateShapeRefreshV8()
        {
            if (!TryRebuildPackFromReplacementCsv(
                    ShapeRefreshV6PackPath,
                    ShapeRefreshV8ReplacementPath,
                    ShapeRefreshV8PackPath,
                    "campaign500_shape_refresh_v8",
                    "Campaign 500 Shape Refresh V8",
                    "Campaign500ShapeRefreshV8"))
            {
                return;
            }

            ValidateShapeRefreshV8();
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Validate Shape Refresh V9")]
        public static void ValidateShapeRefreshV9()
        {
            CampaignSingleLevelValidator.RunValidationForPack(
                ShapeRefreshV9PackPath,
                SelectionPath,
                RhythmPlanPath,
                V9ReportFolder,
                V9SummaryPath,
                V9FlaggedPath,
                V9NotesPath,
                V9ReviewPackPath,
                "Campaign500ShapeRefreshV9");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Rebuild And Validate Shape Refresh V9")]
        public static void RebuildAndValidateShapeRefreshV9()
        {
            if (!TryRebuildPackFromReplacementCsv(
                    ShapeRefreshV8PackPath,
                    ShapeRefreshV9ReplacementPath,
                    ShapeRefreshV9PackPath,
                    "campaign500_shape_refresh_v9",
                    "Campaign 500 Shape Refresh V9",
                    "Campaign500ShapeRefreshV9"))
            {
                return;
            }

            ValidateShapeRefreshV9();
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Validate Shape Refresh V10")]
        public static void ValidateShapeRefreshV10()
        {
            CampaignSingleLevelValidator.RunValidationForPack(
                ShapeRefreshV10PackPath,
                SelectionPath,
                RhythmPlanPath,
                V10ReportFolder,
                V10SummaryPath,
                V10FlaggedPath,
                V10NotesPath,
                V10ReviewPackPath,
                "Campaign500ShapeRefreshV10");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Rebuild And Validate Shape Refresh V10")]
        public static void RebuildAndValidateShapeRefreshV10()
        {
            if (!TryRebuildPackFromReplacementCsv(
                    Campaign500FinalPackPath,
                    ShapeRefreshV10ReplacementPath,
                    ShapeRefreshV10PackPath,
                    "campaign500_shape_refresh_v10",
                    "Campaign 500 Shape Refresh V10",
                    "Campaign500ShapeRefreshV10"))
            {
                return;
            }

            ValidateShapeRefreshV10();
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Validate Shape Refresh V11")]
        public static void ValidateShapeRefreshV11()
        {
            CampaignSingleLevelValidator.RunValidationForPack(
                ShapeRefreshV11PackPath,
                SelectionPath,
                RhythmPlanPath,
                V11ReportFolder,
                V11SummaryPath,
                V11FlaggedPath,
                V11NotesPath,
                V11ReviewPackPath,
                "Campaign500ShapeRefreshV11");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Rebuild And Validate Shape Refresh V11")]
        public static void RebuildAndValidateShapeRefreshV11()
        {
            if (!TryRebuildPackFromReplacementCsv(
                    Campaign500FinalPackPath,
                    ShapeRefreshV11ReplacementPath,
                    ShapeRefreshV11PackPath,
                    "campaign500_shape_refresh_v11",
                    "Campaign 500 Shape Refresh V11",
                    "Campaign500ShapeRefreshV11"))
            {
                return;
            }

            ValidateShapeRefreshV11();
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Attach Shape Refresh V1 To Demo")]
        public static void AttachShapeRefreshV1ToDemo()
        {
            AttachPackToDemo(ShapeRefreshV1PackPath, "Campaign500ShapeRefreshV1");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Attach Shape Refresh V2 To Demo")]
        public static void AttachShapeRefreshV2ToDemo()
        {
            AttachPackToDemo(ShapeRefreshV2PackPath, "Campaign500ShapeRefreshV2");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Attach Shape Refresh V3 To Demo")]
        public static void AttachShapeRefreshV3ToDemo()
        {
            AttachPackToDemo(ShapeRefreshV3PackPath, "Campaign500ShapeRefreshV3");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Attach Shape Refresh V4 To Demo")]
        public static void AttachShapeRefreshV4ToDemo()
        {
            AttachPackToDemo(ShapeRefreshV4PackPath, "Campaign500ShapeRefreshV4");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Attach Shape Refresh V5 To Demo")]
        public static void AttachShapeRefreshV5ToDemo()
        {
            AttachPackToDemo(ShapeRefreshV5PackPath, "Campaign500ShapeRefreshV5");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Attach Shape Refresh V6 To Demo")]
        public static void AttachShapeRefreshV6ToDemo()
        {
            AttachPackToDemo(ShapeRefreshV6PackPath, "Campaign500ShapeRefreshV6");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Attach Shape Refresh V7 To Demo")]
        public static void AttachShapeRefreshV7ToDemo()
        {
            AttachPackToDemo(ShapeRefreshV7PackPath, "Campaign500ShapeRefreshV7");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Attach Shape Refresh V8 To Demo")]
        public static void AttachShapeRefreshV8ToDemo()
        {
            AttachPackToDemo(ShapeRefreshV8PackPath, "Campaign500ShapeRefreshV8");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Attach Shape Refresh V9 To Demo")]
        public static void AttachShapeRefreshV9ToDemo()
        {
            AttachPackToDemo(ShapeRefreshV9PackPath, "Campaign500ShapeRefreshV9");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Attach Shape Refresh V10 To Demo")]
        public static void AttachShapeRefreshV10ToDemo()
        {
            AttachPackToDemo(ShapeRefreshV10PackPath, "Campaign500ShapeRefreshV10");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Attach Shape Refresh V11 To Demo")]
        public static void AttachShapeRefreshV11ToDemo()
        {
            AttachPackToDemo(ShapeRefreshV11PackPath, "Campaign500ShapeRefreshV11");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Publish Shape Refresh V9 Final To Demo")]
        public static void PublishShapeRefreshV9FinalToDemo()
        {
            if (!TryClonePack(
                    ShapeRefreshV9PackPath,
                    Campaign500FinalPackPath,
                    "campaign500_final",
                    "Campaign 500 Final",
                    "Campaign500Final"))
            {
                return;
            }

            AttachPackToDemo(Campaign500FinalPackPath, "Campaign500Final");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Publish Shape Refresh V10 Final To Demo")]
        public static void PublishShapeRefreshV10FinalToDemo()
        {
            if (!TryClonePack(
                    ShapeRefreshV10PackPath,
                    Campaign500FinalPackPath,
                    "campaign500_final",
                    "Campaign 500 Final",
                    "Campaign500Final"))
            {
                return;
            }

            AttachPackToDemo(Campaign500FinalPackPath, "Campaign500Final");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Optimization/Publish Shape Refresh V11 Final To Demo")]
        public static void PublishShapeRefreshV11FinalToDemo()
        {
            if (!TryClonePack(
                    ShapeRefreshV11PackPath,
                    Campaign500FinalPackPath,
                    "campaign500_final",
                    "Campaign 500 Final",
                    "Campaign500Final"))
            {
                return;
            }

            AttachPackToDemo(Campaign500FinalPackPath, "Campaign500Final");
        }

        static void AttachPackToDemo(string packPath, string logTag)
        {
            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(packPath);
            if (pack == null || pack.levels == null || pack.levels.Length == 0)
            {
                Debug.LogError($"[{logTag}] Missing or empty pack: {packPath}");
                return;
            }

            var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(
                DemoScenePath,
                UnityEditor.SceneManagement.OpenSceneMode.Single);
            var progression = Object.FindFirstObjectByType<LevelProgression>();
            if (progression == null)
            {
                Debug.LogWarning($"[Campaign500ShapeRefreshV1] LevelProgression not found in {DemoScenePath}");
                return;
            }

            var so = new SerializedObject(progression);
            var activePack = so.FindProperty("activePack");
            if (activePack == null)
            {
                Debug.LogWarning("[Campaign500ShapeRefreshV1] LevelProgression.activePack serialized field not found.");
                return;
            }

            activePack.objectReferenceValue = pack;
            so.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(progression);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(scene);
            UnityEditor.SceneManagement.EditorSceneManager.SaveScene(scene);
            Debug.Log($"[{logTag}] Attached pack to demo: {packPath}");
        }

        static bool TryClonePack(string sourcePackPath, string targetPackPath, string packId, string displayName, string logTag)
        {
            AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
            var sourcePack = AssetDatabase.LoadAssetAtPath<LevelPack>(sourcePackPath);
            if (sourcePack == null || sourcePack.levels == null || sourcePack.levels.Length == 0)
            {
                Debug.LogError($"[{logTag}] Missing source pack: {sourcePackPath}");
                return false;
            }

            AssetDatabase.DeleteAsset(targetPackPath);
            var pack = ScriptableObject.CreateInstance<LevelPack>();
            pack.packId = packId;
            pack.displayName = displayName;
            pack.levels = sourcePack.levels;
            AssetDatabase.CreateAsset(pack, targetPackPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"[{logTag}] Published final pack: {targetPackPath}, levels={pack.levels.Length}");
            return true;
        }

        static bool TryRebuildShapeRefreshV1PackFromReplacementCsv()
        {
            return TryRebuildPackFromReplacementCsv(
                SourcePackPath,
                ShapeRefreshV1ReplacementPath,
                ShapeRefreshV1PackPath,
                "campaign500_shape_refresh_v1",
                "Campaign 500 Shape Refresh V1",
                "Campaign500ShapeRefreshV1");
        }

        static bool TryRebuildPackFromReplacementCsv(
            string sourcePackPath,
            string replacementPath,
            string targetPackPath,
            string packId,
            string displayName,
            string logTag)
        {
            AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);

            var sourcePack = AssetDatabase.LoadAssetAtPath<LevelPack>(sourcePackPath);
            if (sourcePack == null || sourcePack.levels == null || sourcePack.levels.Length == 0)
            {
                Debug.LogError($"[{logTag}] Missing source pack: {sourcePackPath}");
                return false;
            }

            var levels = new LevelDefinition[sourcePack.levels.Length];
            sourcePack.levels.CopyTo(levels, 0);

            var replacementAbsolutePath = Path.GetFullPath(replacementPath);
            if (!File.Exists(replacementAbsolutePath))
            {
                Debug.LogError($"[{logTag}] Missing replacement CSV: {replacementPath}");
                return false;
            }

            int replaced = 0;
            var missing = new List<string>();
            foreach (var row in ReadReplacementRows(replacementAbsolutePath))
            {
                if (row.Order < 1 || row.Order > levels.Length)
                {
                    missing.Add($"bad-order:{row.Order}");
                    continue;
                }

                var level = AssetDatabase.LoadAssetAtPath<LevelDefinition>(row.DestAssetPath);
                if (level == null)
                {
                    AssetDatabase.ImportAsset(row.DestAssetPath, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport);
                    level = AssetDatabase.LoadAssetAtPath<LevelDefinition>(row.DestAssetPath);
                }

                if (level == null)
                {
                    missing.Add($"{row.Order}:{row.DestAssetPath}");
                    continue;
                }

                levels[row.Order - 1] = level;
                replaced++;
            }

            if (missing.Count > 0)
            {
                Debug.LogError($"[{logTag}] Missing replacement assets: {missing.Count}. First: {missing[0]}");
                return false;
            }

            AssetDatabase.DeleteAsset(targetPackPath);
            var pack = ScriptableObject.CreateInstance<LevelPack>();
            pack.packId = packId;
            pack.displayName = displayName;
            pack.levels = levels;
            AssetDatabase.CreateAsset(pack, targetPackPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"[{logTag}] Rebuilt pack from source pack + replacements. levels={levels.Length}, replaced={replaced}");
            return true;
        }

        static bool TryRebuildShapeRefreshV1PackFromSerializedRefs()
        {
            var absolutePackPath = Path.GetFullPath(ShapeRefreshV1PackPath);
            if (!File.Exists(absolutePackPath))
            {
                Debug.LogError($"[Campaign500ShapeRefreshV1] Pack file missing on disk: {ShapeRefreshV1PackPath}");
                return false;
            }

            var levelGuids = ReadLevelGuidsFromPackYaml(absolutePackPath);
            if (levelGuids.Count == 0)
            {
                Debug.LogError($"[Campaign500ShapeRefreshV1] No level refs found in pack YAML: {ShapeRefreshV1PackPath}");
                return false;
            }

            var levels = new List<LevelDefinition>(levelGuids.Count);
            var missing = new List<string>();
            foreach (var guid in levelGuids)
            {
                var levelPath = AssetDatabase.GUIDToAssetPath(guid);
                var level = string.IsNullOrEmpty(levelPath) ? null : AssetDatabase.LoadAssetAtPath<LevelDefinition>(levelPath);
                if (level == null)
                {
                    missing.Add(guid);
                    continue;
                }

                levels.Add(level);
            }

            if (missing.Count > 0)
            {
                Debug.LogError($"[Campaign500ShapeRefreshV1] Missing level refs while rebuilding pack: {missing.Count}/{levelGuids.Count}");
                return false;
            }

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(ShapeRefreshV1PackPath);
            if (pack == null)
            {
                AssetDatabase.DeleteAsset(ShapeRefreshV1PackPath);
                pack = ScriptableObject.CreateInstance<LevelPack>();
                pack.packId = "campaign500_shape_refresh_v1";
                pack.displayName = "Campaign 500 Shape Refresh V1";
                pack.levels = levels.ToArray();
                AssetDatabase.CreateAsset(pack, ShapeRefreshV1PackPath);
            }
            else
            {
                pack.packId = "campaign500_shape_refresh_v1";
                pack.displayName = "Campaign 500 Shape Refresh V1";
                pack.levels = levels.ToArray();
                EditorUtility.SetDirty(pack);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"[Campaign500ShapeRefreshV1] Rebuilt pack through Unity serialization. levels={levels.Count}");
            return true;
        }

        static List<string> ReadLevelGuidsFromPackYaml(string absolutePackPath)
        {
            var result = new List<string>();
            foreach (var rawLine in File.ReadLines(absolutePackPath))
            {
                var line = rawLine.Trim();
                if (!line.StartsWith("- {fileID: 11400000, guid: "))
                    continue;

                const string marker = "guid: ";
                var start = line.IndexOf(marker);
                if (start < 0)
                    continue;

                start += marker.Length;
                var end = line.IndexOf(',', start);
                if (end <= start)
                    continue;

                result.Add(line.Substring(start, end - start));
            }

            return result;
        }

        static IEnumerable<ReplacementRow> ReadReplacementRows(string absolutePath)
        {
            int orderIndex = -1;
            int destAssetIndex = -1;
            int actionIndex = -1;
            bool readHeader = false;
            foreach (var rawLine in File.ReadLines(absolutePath))
            {
                if (!readHeader)
                {
                    var header = rawLine.Split(',');
                    for (int i = 0; i < header.Length; i++)
                        header[i] = CleanCsvCell(header[i]);

                    orderIndex = System.Array.IndexOf(header, "order");
                    destAssetIndex = System.Array.IndexOf(header, "destAsset");
                    actionIndex = System.Array.IndexOf(header, "action");
                    readHeader = true;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(rawLine))
                    continue;

                var cells = rawLine.Split(',');
                for (int i = 0; i < cells.Length; i++)
                    cells[i] = CleanCsvCell(cells[i]);

                if (orderIndex < 0 || destAssetIndex < 0 || cells.Length <= orderIndex || cells.Length <= destAssetIndex)
                    continue;

                if (actionIndex >= 0 && cells.Length > actionIndex && cells[actionIndex] != "replace")
                    continue;

                if (!int.TryParse(cells[orderIndex], out var order))
                    continue;

                yield return new ReplacementRow
                {
                    Order = order,
                    DestAssetPath = NormalizeAssetPath(cells[destAssetIndex])
                };
            }
        }

        static string CleanCsvCell(string value)
        {
            value = (value ?? string.Empty).Trim();
            if (value.Length >= 2 && value[0] == '"' && value[value.Length - 1] == '"')
                value = value.Substring(1, value.Length - 2).Replace("\"\"", "\"");

            return value;
        }

        static string NormalizeAssetPath(string path)
        {
            path = path.Trim().Replace('\\', '/');
            const string assetsMarker = "/Assets/";
            var markerIndex = path.IndexOf(assetsMarker, System.StringComparison.OrdinalIgnoreCase);
            if (markerIndex >= 0)
                return path.Substring(markerIndex + 1);

            return path.StartsWith("Assets/", System.StringComparison.OrdinalIgnoreCase) ? path : path;
        }

        struct ReplacementRow
        {
            public int Order;
            public string DestAssetPath;
        }
    }
}
#endif
