#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace PixelBug.ArrowMagic.EditorTools
{
    public static class CampaignHardeningAnalyzer
    {
        const string FinalPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500FinalPack.asset";
        const string CandidatePoolPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/SingleLevelCandidatePoolPack.asset";
        const string SelectionPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/campaign500_plan_selection.csv";
        const string RhythmPlanPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/campaign500_21_500_relative_peaks_plan.csv";
        const string ReportFolder = "Assets/ArrowMagic/SOData/Reports/Campaign500/Hardening";
        const string ReviewPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningLeakReviewPack.asset";
        const string SandboxLevelFolder = "Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V1";
        const string SandboxPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV1Pack.asset";
        const string QualitativeSandboxLevelFolder = "Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V2";
        const string QualitativeSandboxPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV2Pack.asset";
        const string PressureSandboxLevelFolder = "Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V3";
        const string PressureSandboxPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV3Pack.asset";
        const string GateSandboxLevelFolder = "Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V4";
        const string GateSandboxPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV4Pack.asset";
        const string GateSecondPassLevelFolder = "Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V41";
        const string GateSecondPassPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV41Pack.asset";
        const string GateThirdPassLevelFolder = "Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V42";
        const string GateThirdPassPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV42Pack.asset";
        const string VisibleGateLevelFolder = "Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V5";
        const string VisibleGatePackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV5Pack.asset";
        const string EarlyPeelGateLevelFolder = "Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V6";
        const string EarlyPeelGatePackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV6Pack.asset";
        const string OpeningPeelGateLevelFolder = "Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V7";
        const string OpeningPeelGatePackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV7Pack.asset";
        const string OpeningRewireGateLevelFolder = "Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V8";
        const string OpeningRewireGatePackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV8Pack.asset";
        const string OpeningOuterRewireGateLevelFolder = "Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V9";
        const string OpeningOuterRewireGatePackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV9Pack.asset";
        const string OuterExitEndpointLevelFolder = "Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V10";
        const string OuterExitEndpointPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV10Pack.asset";
        const string MultiLayerOuterExitLevelFolder = "Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V11";
        const string MultiLayerOuterExitPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV11Pack.asset";
        const string BoundaryRewireLevelFolder = "Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V12BDR";
        const string BoundaryRewirePackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV12BDRPack.asset";
        const string BoundaryInsetLevelFolder = "Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V13BDR2";
        const string BoundaryInsetPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV13BDR2Pack.asset";
        const string BoundaryCompressionLevelFolder = "Assets/ArrowMagic/SOData/Levels/Campaign500HardeningSandbox/V14CMP";
        const string BoundaryCompressionPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardeningSandboxV14CMPPack.asset";
        const string DemoScenePath = "Assets/ArrowMagic/Scenes/Demo.unity";

        static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hardening/Analyze Final Pack")]
        public static void AnalyzeFinalPack()
        {
            Run(new[] { new PackSource("final", FinalPackPath, true) }, "final");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hardening/Analyze Candidate Pool")]
        public static void AnalyzeCandidatePool()
        {
            Run(new[] { new PackSource("candidate_pool", CandidatePoolPackPath, false) }, "candidate_pool");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hardening/Analyze Final + Candidate Pool")]
        public static void AnalyzeFinalAndCandidatePool()
        {
            Run(
                new[]
                {
                    new PackSource("final", FinalPackPath, true),
                    new PackSource("candidate_pool", CandidatePoolPackPath, false)
                },
                "final_and_candidate_pool");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hardening/Build Top Leak Sandbox V1")]
        public static void BuildTopLeakSandboxV1()
        {
            BuildSandboxFromLatestRank(topCount: 10);
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hardening/Build Qualitative Sandbox V2")]
        public static void BuildQualitativeSandboxV2()
        {
            BuildQualitativeSandboxFromLatestRank(targetPairs: 10, scanLimit: 50);
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hardening/Build Pressure Sandbox V3")]
        public static void BuildPressureSandboxV3()
        {
            BuildPressureSandboxFromLatestRank(targetPairs: 10, scanLimit: 40);
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hardening/Build Gate Sandbox V4")]
        public static void BuildGateSandboxV4()
        {
            BuildGateSandboxFromLatestRank(targetPairs: 10, scanLimit: 60);
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hardening/Build Gate Sandbox V4.1")]
        public static void BuildGateSandboxV41()
        {
            BuildGateSecondPassFromLatestV4(targetPairs: 10);
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hardening/Build Gate Sandbox V4.2")]
        public static void BuildGateSandboxV42()
        {
            BuildGateThirdPassFromLatestV41(targetPairs: 10);
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hardening/Build Visible Gate Sandbox V5")]
        public static void BuildVisibleGateSandboxV5()
        {
            BuildVisibleGateSandboxFromLatestRank(targetPairs: 6, scanLimit: 80);
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hardening/Build Early Peel Gate Sandbox V6")]
        public static void BuildEarlyPeelGateSandboxV6()
        {
            BuildEarlyPeelGateSandboxFromLatestRank(targetPairs: 6, scanLimit: 90);
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hardening/Build Opening Peel Gate Sandbox V7")]
        public static void BuildOpeningPeelGateSandboxV7()
        {
            BuildOpeningPeelGateSandboxFromLatestRank(targetPairs: 4, scanLimit: 80);
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hardening/Build Opening Rewire Gate Sandbox V8")]
        public static void BuildOpeningRewireGateSandboxV8()
        {
            BuildOpeningRewireGateSandboxFromLatestRank(targetPairs: 4, scanLimit: 100);
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hardening/Build Opening Outer Rewire Gate Sandbox V9")]
        public static void BuildOpeningOuterRewireGateSandboxV9()
        {
            BuildOpeningOuterRewireGateSandboxFromLatestRank(targetPairs: 4, scanLimit: 40);
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hardening/Build Outer Exit Endpoint Sandbox V10")]
        public static void BuildOuterExitEndpointSandboxV10()
        {
            BuildOuterExitEndpointSandboxFromLatestV9(targetPairs: 4, scanLimit: 12);
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hardening/Build Multi Layer Outer Exit Sandbox V11")]
        public static void BuildMultiLayerOuterExitSandboxV11()
        {
            BuildMultiLayerOuterExitSandboxFromLatestV10(targetPairs: 4, scanLimit: 8);
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hardening/Build PBE NEE Classification Report V12")]
        public static void BuildPbeNeeClassificationReportV12()
        {
            BuildPbeNeeClassificationReportFromLatestV11();
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hardening/Build Boundary Rewire Sandbox V12")]
        public static void BuildBoundaryRewireSandboxV12()
        {
            BuildBoundaryRewireSandboxFromLatestV11(targetPairs: 3, scanLimit: 8);
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hardening/Build Boundary Inset Sandbox V13")]
        public static void BuildBoundaryInsetSandboxV13()
        {
            BuildBoundaryInsetSandboxFromLatestV11(targetPairs: 3, scanLimit: 8);
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hardening/Build Boundary Compression Sandbox V14")]
        public static void BuildBoundaryCompressionSandboxV14()
        {
            BuildBoundaryCompressionSandboxFromLatestV13(targetPairs: 3, scanLimit: 8);
        }

        public static IReadOnlyList<string> Run(IReadOnlyList<PackSource> sources, string runLabel)
        {
            var rows = new List<Row>(1024);
            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var selectionByOrder = ReadCsvByOrder(SelectionPath);
            var rhythmByOrder = ReadCsvByOrder(RhythmPlanPath);

            try
            {
                int totalLevels = 0;
                for (int s = 0; s < sources.Count; s++)
                {
                    var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(sources[s].PackPath);
                    totalLevels += pack?.levels?.Length ?? 0;
                }

                int processed = 0;
                bool showProgressBar = !Application.isBatchMode;
                Debug.Log($"[CampaignHardeningAnalyzer] Start run={runLabel}, totalLevels={totalLevels}, batch={Application.isBatchMode}");
                for (int s = 0; s < sources.Count; s++)
                {
                    PackSource source = sources[s];
                    var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(source.PackPath);
                    if (pack == null || pack.levels == null || pack.levels.Length == 0)
                    {
                        Debug.LogWarning($"[CampaignHardeningAnalyzer] Missing or empty pack: {source.PackPath}");
                        continue;
                    }

                    for (int i = 0; i < pack.levels.Length; i++)
                    {
                        processed++;
                        if (showProgressBar && (processed % 20) == 0)
                        {
                            EditorUtility.DisplayProgressBar(
                                "Campaign Hardening Analyzer",
                                $"{source.Label} {i + 1}/{pack.levels.Length}",
                                totalLevels > 0 ? processed / (float)totalLevels : 0f);
                        }
                        else if (Application.isBatchMode && ((processed % 25) == 0 || processed == 1 || processed == totalLevels))
                        {
                            Debug.Log($"[CampaignHardeningAnalyzer] Progress {processed}/{totalLevels}: {source.Label} {i + 1}/{pack.levels.Length}");
                        }

                        int order = source.IsCampaignOrdered ? i + 1 : 0;
                        selectionByOrder.TryGetValue(order, out var selectionMeta);
                        rhythmByOrder.TryGetValue(order, out var rhythmMeta);
                        var levelWatch = System.Diagnostics.Stopwatch.StartNew();
                        rows.Add(AnalyzeLevel(source, i + 1, order, pack.levels[i], rules, selectionMeta, rhythmMeta));
                        levelWatch.Stop();
                        if (Application.isBatchMode && levelWatch.ElapsedMilliseconds >= 2000)
                            Debug.LogWarning($"[CampaignHardeningAnalyzer] Slow level {source.Label}#{i + 1} order={order}, ms={levelWatch.ElapsedMilliseconds}, id={(pack.levels[i] != null ? pack.levels[i].levelId : "<null>")}");
                    }
                }
            }
            finally
            {
                if (!Application.isBatchMode)
                    EditorUtility.ClearProgressBar();
            }

            Directory.CreateDirectory(ToAbsolutePath(ReportFolder));
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", Inv);
            string summaryPath = $"{ReportFolder}/campaign_hardening_{runLabel}_{stamp}_summary.csv";
            string rankedPath = $"{ReportFolder}/campaign_hardening_{runLabel}_{stamp}_leak_rank.csv";
            string planPath = $"{ReportFolder}/campaign_hardening_{runLabel}_{stamp}_top20_plan.csv";
            string notesPath = $"{ReportFolder}/campaign_hardening_{runLabel}_{stamp}_notes.md";

            WriteRows(summaryPath, rows.OrderBy(r => r.PackLabel).ThenBy(r => r.IndexInPack).ToList());
            var ranked = rows
                .Where(r => r.Level != null && r.GreedySolved)
                .OrderByDescending(r => r.LeakScore)
                .ThenByDescending(r => r.DirectClearableOuterExits)
                .ThenByDescending(r => r.EarlyAvgChoices)
                .ToList();
            WriteRows(rankedPath, ranked);
            WriteRows(planPath, ranked.Take(20).ToList());
            WriteNotes(notesPath, rows, ranked, sources, runLabel);
            SaveReviewPack(ranked.Take(20).ToList());

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"[CampaignHardeningAnalyzer] Done. rows={rows.Count}, summary={summaryPath}, rank={rankedPath}, top20={planPath}");
            return new[] { summaryPath, rankedPath, planPath, notesPath };
        }

        static void BuildSandboxFromLatestRank(int topCount)
        {
            string latestRank = FindLatestReport("*_leak_rank.csv");
            if (string.IsNullOrEmpty(latestRank))
            {
                Debug.LogError("[CampaignHardeningAnalyzer] No leak rank report found. Run analyzer first.");
                return;
            }

            var records = ReadCsvRows(latestRank)
                .Where(r => Get(r, "packLabel") == "final")
                .Where(r => Get(r, "greedySolved").Equals("True", StringComparison.OrdinalIgnoreCase))
                .Where(r => Get(r, "status") == "HighLeak" || Get(r, "status") == "MediumLeak")
                .Take(topCount)
                .ToList();

            if (records.Count == 0)
            {
                Debug.LogWarning($"[CampaignHardeningAnalyzer] No sandbox records found in {latestRank}");
                return;
            }

            Directory.CreateDirectory(ToAbsolutePath(SandboxLevelFolder));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var report = new List<SandboxReportRow>();
            var packLevels = new List<LevelDefinition>(records.Count * 3);
            int sequence = 0;
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", Inv);

            for (int r = 0; r < records.Count; r++)
            {
                Dictionary<string, string> record = records[r];
                string sourcePath = Get(record, "assetPath");
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source == null)
                {
                    Debug.LogWarning($"[CampaignHardeningAnalyzer] Missing sandbox source: {sourcePath}");
                    continue;
                }

                int order = ParseInt(Get(record, "campaignOrder"));
                string orderLabel = order > 0 ? $"L{order:000}" : $"I{r + 1:000}";
                Row baseline = AnalyzeLevel(new PackSource("sandbox_baseline", "", false), r + 1, order, source, rules, null, null);

                LevelDefinition original = CreateSandboxCopy(source, $"{orderLabel}_original", stamp);
                Row originalRow = AnalyzeLevel(new PackSource("sandbox_original", "", false), ++sequence, order, original, rules, null, null);
                packLevels.Add(original);
                report.Add(SandboxReportRow.From(order, "original", sourcePath, AssetDatabase.GetAssetPath(original), baseline, originalRow, 0, "copy"));

                LevelDefinition light = CreateSandboxCopy(source, $"{orderLabel}_light", stamp);
                int lightOps = ApplyReverseOuterHeadHardening(light, rules, maxAcceptedOps: 2, out string lightOpsText);
                EditorUtility.SetDirty(light);
                Row lightRow = AnalyzeLevel(new PackSource("sandbox_light", "", false), ++sequence, order, light, rules, null, null);
                packLevels.Add(light);
                report.Add(SandboxReportRow.From(order, "light", sourcePath, AssetDatabase.GetAssetPath(light), baseline, lightRow, lightOps, lightOpsText));

                LevelDefinition heavy = CreateSandboxCopy(source, $"{orderLabel}_heavy", stamp);
                int heavyOps = ApplyReverseOuterHeadHardening(heavy, rules, maxAcceptedOps: 4, out string heavyOpsText);
                EditorUtility.SetDirty(heavy);
                Row heavyRow = AnalyzeLevel(new PackSource("sandbox_heavy", "", false), ++sequence, order, heavy, rules, null, null);
                packLevels.Add(heavy);
                report.Add(SandboxReportRow.From(order, "heavy", sourcePath, AssetDatabase.GetAssetPath(heavy), baseline, heavyRow, heavyOps, heavyOpsText));
            }

            SavePackAt(SandboxPackPath, "campaign500_hardening_sandbox_v1", $"Campaign 500 Hardening Sandbox V1 ({packLevels.Count})", packLevels);

            string reportPath = $"{ReportFolder}/campaign_hardening_sandbox_v1_{stamp}.csv";
            WriteSandboxReport(reportPath, report);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"[CampaignHardeningAnalyzer] Sandbox V1 done. levels={packLevels.Count}, report={reportPath}, pack={SandboxPackPath}");
        }

        static void BuildQualitativeSandboxFromLatestRank(int targetPairs, int scanLimit)
        {
            string latestRank = FindLatestReport("*_leak_rank.csv");
            if (string.IsNullOrEmpty(latestRank))
            {
                Debug.LogError("[CampaignHardeningAnalyzer] No leak rank report found. Run analyzer first.");
                return;
            }

            var records = ReadCsvRows(latestRank)
                .Where(r => Get(r, "packLabel") == "final")
                .Where(r => Get(r, "greedySolved").Equals("True", StringComparison.OrdinalIgnoreCase))
                .Where(r => Get(r, "status") == "HighLeak" || Get(r, "status") == "MediumLeak")
                .Take(Mathf.Max(targetPairs, scanLimit))
                .ToList();

            if (records.Count == 0)
            {
                Debug.LogWarning($"[CampaignHardeningAnalyzer] No qualitative sandbox records found in {latestRank}");
                return;
            }

            Directory.CreateDirectory(ToAbsolutePath(QualitativeSandboxLevelFolder));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var report = new List<SandboxReportRow>();
            var packLevels = new List<LevelDefinition>(targetPairs * 2);
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", Inv);
            int acceptedPairs = 0;
            int scanned = 0;

            for (int r = 0; r < records.Count && acceptedPairs < targetPairs; r++)
            {
                scanned++;
                Dictionary<string, string> record = records[r];
                string sourcePath = Get(record, "assetPath");
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source == null)
                {
                    Debug.LogWarning($"[CampaignHardeningAnalyzer] Missing qualitative source: {sourcePath}");
                    continue;
                }

                int order = ParseInt(Get(record, "campaignOrder"));
                string orderLabel = order > 0 ? $"L{order:000}" : $"I{r + 1:000}";
                Row baseline = AnalyzeLevel(new PackSource("qualitative_baseline", "", false), r + 1, order, source, rules, null, null);

                LevelDefinition strong = CreateSandboxCopy(source, $"{orderLabel}_strong", stamp, QualitativeSandboxLevelFolder);
                int ops = ApplyQualitativeHardeningV2(strong, rules, maxAcceptedOps: 10, out string opsText);
                EditorUtility.SetDirty(strong);
                Row strongRow = AnalyzeLevel(new PackSource("qualitative_strong", "", false), r + 1, order, strong, rules, null, null);

                bool meaningful =
                    strongRow.GreedySolved &&
                    ops >= 3 &&
                    strongRow.Chains <= baseline.Chains - 3 &&
                    (strongRow.DirectClearableOuterExits <= baseline.DirectClearableOuterExits - 3 ||
                     strongRow.OpeningChoices <= baseline.OpeningChoices - 3 ||
                     strongRow.EarlyAvgChoices <= baseline.EarlyAvgChoices - 2f);

                if (!meaningful)
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(strong));
                    report.Add(SandboxReportRow.From(order, "skipped", sourcePath, "", baseline, strongRow, ops, $"not_meaningful:{opsText}"));
                    continue;
                }

                LevelDefinition original = CreateSandboxCopy(source, $"{orderLabel}_original", stamp, QualitativeSandboxLevelFolder);
                Row originalRow = AnalyzeLevel(new PackSource("qualitative_original", "", false), packLevels.Count + 1, order, original, rules, null, null);
                packLevels.Add(original);
                report.Add(SandboxReportRow.From(order, "original", sourcePath, AssetDatabase.GetAssetPath(original), baseline, originalRow, 0, "copy"));

                packLevels.Add(strong);
                report.Add(SandboxReportRow.From(order, "strong_v2", sourcePath, AssetDatabase.GetAssetPath(strong), baseline, strongRow, ops, opsText));
                acceptedPairs++;
            }

            SavePackAt(QualitativeSandboxPackPath, "campaign500_hardening_sandbox_v2", $"Campaign 500 Hardening Sandbox V2 ({packLevels.Count})", packLevels);

            string reportPath = $"{ReportFolder}/campaign_hardening_sandbox_v2_{stamp}.csv";
            WriteSandboxReport(reportPath, report);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(QualitativeSandboxPackPath);
            AttachPackToDemo(pack, "CampaignHardeningAnalyzer V2");

            Debug.Log($"[CampaignHardeningAnalyzer] Sandbox V2 done. scanned={scanned}, pairs={acceptedPairs}, levels={packLevels.Count}, report={reportPath}, pack={QualitativeSandboxPackPath}");
        }

        static void BuildPressureSandboxFromLatestRank(int targetPairs, int scanLimit)
        {
            string latestRank = FindLatestReport("*_leak_rank.csv");
            if (string.IsNullOrEmpty(latestRank))
            {
                Debug.LogError("[CampaignHardeningAnalyzer] No leak rank report found. Run analyzer first.");
                return;
            }

            var records = ReadCsvRows(latestRank)
                .Where(r => Get(r, "packLabel") == "final")
                .Where(r => Get(r, "greedySolved").Equals("True", StringComparison.OrdinalIgnoreCase))
                .Where(r => Get(r, "status") == "HighLeak" || Get(r, "status") == "MediumLeak")
                .Take(Mathf.Max(targetPairs, scanLimit))
                .ToList();

            if (records.Count == 0)
            {
                Debug.LogWarning($"[CampaignHardeningAnalyzer] No pressure sandbox records found in {latestRank}");
                return;
            }

            if (AssetDatabase.IsValidFolder(PressureSandboxLevelFolder))
                AssetDatabase.DeleteAsset(PressureSandboxLevelFolder);
            Directory.CreateDirectory(ToAbsolutePath(PressureSandboxLevelFolder));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var report = new List<SandboxReportRow>();
            var packLevels = new List<LevelDefinition>(targetPairs * 2);
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", Inv);
            int acceptedPairs = 0;
            int scanned = 0;

            for (int r = 0; r < records.Count && acceptedPairs < targetPairs; r++)
            {
                scanned++;
                Dictionary<string, string> record = records[r];
                string sourcePath = Get(record, "assetPath");
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source == null)
                {
                    Debug.LogWarning($"[CampaignHardeningAnalyzer] Missing pressure source: {sourcePath}");
                    continue;
                }

                int order = ParseInt(Get(record, "campaignOrder"));
                string orderLabel = order > 0 ? $"L{order:000}" : $"I{r + 1:000}";
                Row baseline = AnalyzeLevel(new PackSource("pressure_baseline", "", false), r + 1, order, source, rules, null, null);

                LevelDefinition pressure = CreateSandboxCopy(source, $"{orderLabel}_pressure", stamp, PressureSandboxLevelFolder);
                int ops = ApplyPressureHardeningV3(pressure, rules, maxAcceptedOps: 16, out string opsText);
                EditorUtility.SetDirty(pressure);
                Row pressureRow = AnalyzeLevel(new PackSource("pressure_strong", "", false), r + 1, order, pressure, rules, null, null);

                bool meaningful =
                    pressureRow.GreedySolved &&
                    ops >= 1 &&
                    pressureRow.OpeningChoices >= 2 &&
                    (
                        pressureRow.Chains <= baseline.Chains - 2 ||
                        pressureRow.AvgChoices <= baseline.AvgChoices - 1f ||
                        pressureRow.OpeningChoices <= baseline.OpeningChoices - 3 ||
                        pressureRow.DirectClearableOuterExits <= baseline.DirectClearableOuterExits - 3 ||
                        pressureRow.LeakScore <= baseline.LeakScore - 50
                    );

                Debug.Log(
                    $"[CampaignHardeningAnalyzer] V3 scan {orderLabel}: meaningful={meaningful}, ops={ops}, solved={pressureRow.GreedySolved}, chains {baseline.Chains}->{pressureRow.Chains}, open {baseline.OpeningChoices}->{pressureRow.OpeningChoices}, outer {baseline.DirectClearableOuterExits}->{pressureRow.DirectClearableOuterExits}, avg {F(baseline.AvgChoices)}->{F(pressureRow.AvgChoices)}, leak {baseline.LeakScore}->{pressureRow.LeakScore}");

                if (!meaningful)
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(pressure));
                    report.Add(SandboxReportRow.From(order, "skipped_v3", sourcePath, "", baseline, pressureRow, ops, $"not_pressure_enough:{opsText}"));
                    continue;
                }

                LevelDefinition original = CreateSandboxCopy(source, $"{orderLabel}_original", stamp, PressureSandboxLevelFolder);
                Row originalRow = AnalyzeLevel(new PackSource("pressure_original", "", false), packLevels.Count + 1, order, original, rules, null, null);
                packLevels.Add(original);
                report.Add(SandboxReportRow.From(order, "original", sourcePath, AssetDatabase.GetAssetPath(original), baseline, originalRow, 0, "copy"));

                packLevels.Add(pressure);
                report.Add(SandboxReportRow.From(order, "pressure_v3", sourcePath, AssetDatabase.GetAssetPath(pressure), baseline, pressureRow, ops, opsText));
                acceptedPairs++;
            }

            SavePackAt(PressureSandboxPackPath, "campaign500_hardening_sandbox_v3", $"Campaign 500 Hardening Sandbox V3 ({packLevels.Count})", packLevels);

            string reportPath = $"{ReportFolder}/campaign_hardening_sandbox_v3_{stamp}.csv";
            WriteSandboxReport(reportPath, report);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(PressureSandboxPackPath);
            AttachPackToDemo(pack, "CampaignHardeningAnalyzer V3");

            Debug.Log($"[CampaignHardeningAnalyzer] Sandbox V3 done. scanned={scanned}, pairs={acceptedPairs}, levels={packLevels.Count}, report={reportPath}, pack={PressureSandboxPackPath}");
        }

        static void BuildGateSandboxFromLatestRank(int targetPairs, int scanLimit)
        {
            string latestRank = FindLatestReport("*_leak_rank.csv");
            if (string.IsNullOrEmpty(latestRank))
            {
                Debug.LogError("[CampaignHardeningAnalyzer] No leak rank report found. Run analyzer first.");
                return;
            }

            var records = ReadCsvRows(latestRank)
                .Where(r => Get(r, "packLabel") == "final")
                .Where(r => Get(r, "greedySolved").Equals("True", StringComparison.OrdinalIgnoreCase))
                .Where(r => Get(r, "status") == "HighLeak" || Get(r, "status") == "MediumLeak")
                .Take(Mathf.Max(targetPairs, scanLimit))
                .ToList();

            if (records.Count == 0)
            {
                Debug.LogWarning($"[CampaignHardeningAnalyzer] No gate sandbox records found in {latestRank}");
                return;
            }

            if (AssetDatabase.IsValidFolder(GateSandboxLevelFolder))
                AssetDatabase.DeleteAsset(GateSandboxLevelFolder);
            Directory.CreateDirectory(ToAbsolutePath(GateSandboxLevelFolder));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var report = new List<SandboxReportRow>();
            var packLevels = new List<LevelDefinition>(targetPairs * 2);
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", Inv);
            int acceptedPairs = 0;
            int scanned = 0;

            for (int r = 0; r < records.Count && acceptedPairs < targetPairs; r++)
            {
                scanned++;
                Dictionary<string, string> record = records[r];
                string sourcePath = Get(record, "assetPath");
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source == null)
                {
                    Debug.LogWarning($"[CampaignHardeningAnalyzer] Missing gate source: {sourcePath}");
                    continue;
                }

                int order = ParseInt(Get(record, "campaignOrder"));
                string orderLabel = order > 0 ? $"L{order:000}" : $"I{r + 1:000}";
                Row baseline = AnalyzeLevel(new PackSource("gate_baseline", "", false), r + 1, order, source, rules, null, null);
                if (baseline.Chains > 200)
                {
                    report.Add(SandboxReportRow.From(order, "skipped_v4", sourcePath, "", baseline, baseline, 0, "skip_large_source_for_gate_probe"));
                    Debug.Log($"[CampaignHardeningAnalyzer] V4 scan {orderLabel}: skip large source chains={baseline.Chains}");
                    continue;
                }

                LevelDefinition gate = CreateSandboxCopy(source, $"{orderLabel}_gate", stamp, GateSandboxLevelFolder);
                int ops = ApplyGateHardeningV4(gate, rules, maxAcceptedOps: 8, out string opsText);
                EditorUtility.SetDirty(gate);
                Row gateRow = AnalyzeLevel(new PackSource("gate_v4", "", false), r + 1, order, gate, rules, null, null);

                bool meaningful =
                    gateRow.GreedySolved &&
                    ops >= 2 &&
                    gateRow.OpeningChoices >= 2 &&
                    (
                        gateRow.Chains <= baseline.Chains - 2 ||
                        gateRow.AvgChoices <= baseline.AvgChoices - 1.2f ||
                        gateRow.OpeningChoices <= baseline.OpeningChoices - 4 ||
                        gateRow.DirectClearableOuterExits <= baseline.DirectClearableOuterExits - 4 ||
                        gateRow.LeakScore <= baseline.LeakScore - 60
                    );

                Debug.Log(
                    $"[CampaignHardeningAnalyzer] V4 scan {orderLabel}: meaningful={meaningful}, ops={ops}, solved={gateRow.GreedySolved}, chains {baseline.Chains}->{gateRow.Chains}, open {baseline.OpeningChoices}->{gateRow.OpeningChoices}, outer {baseline.DirectClearableOuterExits}->{gateRow.DirectClearableOuterExits}, avg {F(baseline.AvgChoices)}->{F(gateRow.AvgChoices)}, leak {baseline.LeakScore}->{gateRow.LeakScore}");

                if (!meaningful)
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(gate));
                    report.Add(SandboxReportRow.From(order, "skipped_v4", sourcePath, "", baseline, gateRow, ops, $"not_gate_enough:{opsText}"));
                    continue;
                }

                LevelDefinition original = CreateSandboxCopy(source, $"{orderLabel}_original", stamp, GateSandboxLevelFolder);
                Row originalRow = AnalyzeLevel(new PackSource("gate_original", "", false), packLevels.Count + 1, order, original, rules, null, null);
                packLevels.Add(original);
                report.Add(SandboxReportRow.From(order, "original", sourcePath, AssetDatabase.GetAssetPath(original), baseline, originalRow, 0, "copy"));

                packLevels.Add(gate);
                report.Add(SandboxReportRow.From(order, "gate_v4", sourcePath, AssetDatabase.GetAssetPath(gate), baseline, gateRow, ops, opsText));
                acceptedPairs++;
            }

            SavePackAt(GateSandboxPackPath, "campaign500_hardening_sandbox_v4", $"Campaign 500 Hardening Sandbox V4 ({packLevels.Count})", packLevels);

            string reportPath = $"{ReportFolder}/campaign_hardening_sandbox_v4_{stamp}.csv";
            WriteSandboxReport(reportPath, report);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(GateSandboxPackPath);
            AttachPackToDemo(pack, "CampaignHardeningAnalyzer V4");

            Debug.Log($"[CampaignHardeningAnalyzer] Sandbox V4 done. scanned={scanned}, pairs={acceptedPairs}, levels={packLevels.Count}, report={reportPath}, pack={GateSandboxPackPath}");
        }

        static void BuildGateSecondPassFromLatestV4(int targetPairs)
        {
            string latestV4Report = FindLatestReport("campaign_hardening_sandbox_v4_*.csv");
            if (string.IsNullOrEmpty(latestV4Report))
            {
                Debug.LogError("[CampaignHardeningAnalyzer] No V4 report found. Run Gate Sandbox V4 first.");
                return;
            }

            var records = ReadCsvRows(latestV4Report)
                .Where(r => Get(r, "variant") == "gate_v4")
                .Take(targetPairs)
                .ToList();

            if (records.Count == 0)
            {
                Debug.LogWarning($"[CampaignHardeningAnalyzer] No gate_v4 rows found in {latestV4Report}");
                return;
            }

            if (AssetDatabase.IsValidFolder(GateSecondPassLevelFolder))
                AssetDatabase.DeleteAsset(GateSecondPassLevelFolder);
            Directory.CreateDirectory(ToAbsolutePath(GateSecondPassLevelFolder));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var report = new List<SandboxReportRow>();
            var packLevels = new List<LevelDefinition>(targetPairs * 2);
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", Inv);
            int acceptedPairs = 0;
            int scanned = 0;

            for (int r = 0; r < records.Count && acceptedPairs < targetPairs; r++)
            {
                scanned++;
                Dictionary<string, string> record = records[r];
                string sourcePath = Get(record, "assetPath");
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source == null)
                {
                    Debug.LogWarning($"[CampaignHardeningAnalyzer] Missing V4.1 source: {sourcePath}");
                    continue;
                }

                int order = ParseInt(Get(record, "campaignOrder"));
                string orderLabel = order > 0 ? $"L{order:000}" : $"I{r + 1:000}";
                Row baseline = AnalyzeLevel(new PackSource("gate_v4_baseline", "", false), r + 1, order, source, rules, null, null);

                LevelDefinition next = CreateSandboxCopy(source, $"{orderLabel}_gate41", stamp, GateSecondPassLevelFolder);
                int ops = ApplyGateHardeningV4(next, rules, maxAcceptedOps: 8, out string opsText);
                EditorUtility.SetDirty(next);
                Row nextRow = AnalyzeLevel(new PackSource("gate_v41", "", false), r + 1, order, next, rules, null, null);

                bool meaningful =
                    nextRow.GreedySolved &&
                    ops >= 1 &&
                    nextRow.OpeningChoices >= 2 &&
                    (
                        nextRow.Chains <= baseline.Chains - 1 ||
                        nextRow.AvgChoices <= baseline.AvgChoices - 0.35f ||
                        nextRow.OpeningChoices <= baseline.OpeningChoices - 2 ||
                        nextRow.DirectClearableOuterExits <= baseline.DirectClearableOuterExits - 2 ||
                        nextRow.LeakScore <= baseline.LeakScore - 25
                    );

                Debug.Log(
                    $"[CampaignHardeningAnalyzer] V4.1 scan {orderLabel}: meaningful={meaningful}, ops={ops}, solved={nextRow.GreedySolved}, chains {baseline.Chains}->{nextRow.Chains}, open {baseline.OpeningChoices}->{nextRow.OpeningChoices}, outer {baseline.DirectClearableOuterExits}->{nextRow.DirectClearableOuterExits}, avg {F(baseline.AvgChoices)}->{F(nextRow.AvgChoices)}, leak {baseline.LeakScore}->{nextRow.LeakScore}");

                if (!meaningful)
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(next));
                    report.Add(SandboxReportRow.From(order, "skipped_v41", sourcePath, "", baseline, nextRow, ops, $"not_gate41_enough:{opsText}"));
                    continue;
                }

                LevelDefinition original = CreateSandboxCopy(source, $"{orderLabel}_v4", stamp, GateSecondPassLevelFolder);
                Row originalRow = AnalyzeLevel(new PackSource("gate_v4_copy", "", false), packLevels.Count + 1, order, original, rules, null, null);
                packLevels.Add(original);
                report.Add(SandboxReportRow.From(order, "gate_v4", sourcePath, AssetDatabase.GetAssetPath(original), baseline, originalRow, 0, "copy"));

                packLevels.Add(next);
                report.Add(SandboxReportRow.From(order, "gate_v41", sourcePath, AssetDatabase.GetAssetPath(next), baseline, nextRow, ops, opsText));
                acceptedPairs++;
            }

            SavePackAt(GateSecondPassPackPath, "campaign500_hardening_sandbox_v41", $"Campaign 500 Hardening Sandbox V4.1 ({packLevels.Count})", packLevels);

            string reportPath = $"{ReportFolder}/campaign_hardening_sandbox_v41_{stamp}.csv";
            WriteSandboxReport(reportPath, report);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(GateSecondPassPackPath);
            AttachPackToDemo(pack, "CampaignHardeningAnalyzer V4.1");

            Debug.Log($"[CampaignHardeningAnalyzer] Sandbox V4.1 done. scanned={scanned}, pairs={acceptedPairs}, levels={packLevels.Count}, report={reportPath}, pack={GateSecondPassPackPath}");
        }

        static void BuildGateThirdPassFromLatestV41(int targetPairs)
        {
            string latestV41Report = FindLatestReport("campaign_hardening_sandbox_v41_*.csv");
            if (string.IsNullOrEmpty(latestV41Report))
            {
                Debug.LogError("[CampaignHardeningAnalyzer] No V4.1 report found. Run Gate Sandbox V4.1 first.");
                return;
            }

            var records = ReadCsvRows(latestV41Report)
                .Where(r => Get(r, "variant") == "gate_v41")
                .Take(targetPairs)
                .ToList();

            if (records.Count == 0)
            {
                Debug.LogWarning($"[CampaignHardeningAnalyzer] No gate_v41 rows found in {latestV41Report}");
                return;
            }

            if (AssetDatabase.IsValidFolder(GateThirdPassLevelFolder))
                AssetDatabase.DeleteAsset(GateThirdPassLevelFolder);
            Directory.CreateDirectory(ToAbsolutePath(GateThirdPassLevelFolder));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var report = new List<SandboxReportRow>();
            var packLevels = new List<LevelDefinition>(targetPairs * 2);
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", Inv);
            int acceptedPairs = 0;
            int scanned = 0;

            for (int r = 0; r < records.Count && acceptedPairs < targetPairs; r++)
            {
                scanned++;
                Dictionary<string, string> record = records[r];
                string sourcePath = Get(record, "assetPath");
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source == null)
                {
                    Debug.LogWarning($"[CampaignHardeningAnalyzer] Missing V4.2 source: {sourcePath}");
                    continue;
                }

                int order = ParseInt(Get(record, "campaignOrder"));
                string orderLabel = order > 0 ? $"L{order:000}" : $"I{r + 1:000}";
                Row baseline = AnalyzeLevel(new PackSource("gate_v41_baseline", "", false), r + 1, order, source, rules, null, null);

                LevelDefinition next = CreateSandboxCopy(source, $"{orderLabel}_gate42", stamp, GateThirdPassLevelFolder);
                int ops = ApplyGateHardeningV4(next, rules, maxAcceptedOps: 6, out string opsText);
                EditorUtility.SetDirty(next);
                Row nextRow = AnalyzeLevel(new PackSource("gate_v42", "", false), r + 1, order, next, rules, null, null);

                bool meaningful =
                    nextRow.GreedySolved &&
                    ops >= 1 &&
                    nextRow.OpeningChoices >= 2 &&
                    (
                        nextRow.Chains <= baseline.Chains - 1 ||
                        nextRow.AvgChoices <= baseline.AvgChoices - 0.25f ||
                        nextRow.OpeningChoices <= baseline.OpeningChoices - 2 ||
                        nextRow.DirectClearableOuterExits <= baseline.DirectClearableOuterExits - 2 ||
                        nextRow.LeakScore <= baseline.LeakScore - 20
                    );

                Debug.Log(
                    $"[CampaignHardeningAnalyzer] V4.2 scan {orderLabel}: meaningful={meaningful}, ops={ops}, solved={nextRow.GreedySolved}, chains {baseline.Chains}->{nextRow.Chains}, open {baseline.OpeningChoices}->{nextRow.OpeningChoices}, outer {baseline.DirectClearableOuterExits}->{nextRow.DirectClearableOuterExits}, avg {F(baseline.AvgChoices)}->{F(nextRow.AvgChoices)}, leak {baseline.LeakScore}->{nextRow.LeakScore}");

                if (!meaningful)
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(next));
                    report.Add(SandboxReportRow.From(order, "skipped_v42", sourcePath, "", baseline, nextRow, ops, $"not_gate42_enough:{opsText}"));
                    continue;
                }

                LevelDefinition original = CreateSandboxCopy(source, $"{orderLabel}_v41", stamp, GateThirdPassLevelFolder);
                Row originalRow = AnalyzeLevel(new PackSource("gate_v41_copy", "", false), packLevels.Count + 1, order, original, rules, null, null);
                packLevels.Add(original);
                report.Add(SandboxReportRow.From(order, "gate_v41", sourcePath, AssetDatabase.GetAssetPath(original), baseline, originalRow, 0, "copy"));

                packLevels.Add(next);
                report.Add(SandboxReportRow.From(order, "gate_v42", sourcePath, AssetDatabase.GetAssetPath(next), baseline, nextRow, ops, opsText));
                acceptedPairs++;
            }

            SavePackAt(GateThirdPassPackPath, "campaign500_hardening_sandbox_v42", $"Campaign 500 Hardening Sandbox V4.2 ({packLevels.Count})", packLevels);

            string reportPath = $"{ReportFolder}/campaign_hardening_sandbox_v42_{stamp}.csv";
            WriteSandboxReport(reportPath, report);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(GateThirdPassPackPath);
            AttachPackToDemo(pack, "CampaignHardeningAnalyzer V4.2");

            Debug.Log($"[CampaignHardeningAnalyzer] Sandbox V4.2 done. scanned={scanned}, pairs={acceptedPairs}, levels={packLevels.Count}, report={reportPath}, pack={GateThirdPassPackPath}");
        }

        static void BuildVisibleGateSandboxFromLatestRank(int targetPairs, int scanLimit)
        {
            string latestRank = FindLatestReport("*_leak_rank.csv");
            if (string.IsNullOrEmpty(latestRank))
            {
                Debug.LogError("[CampaignHardeningAnalyzer] No leak rank report found. Run analyzer first.");
                return;
            }

            var records = ReadCsvRows(latestRank)
                .Where(r => Get(r, "packLabel") == "final")
                .Where(r => Get(r, "greedySolved").Equals("True", StringComparison.OrdinalIgnoreCase))
                .Where(r => Get(r, "status") == "HighLeak" || Get(r, "status") == "MediumLeak")
                .Take(Mathf.Max(targetPairs, scanLimit))
                .ToList();

            if (records.Count == 0)
            {
                Debug.LogWarning($"[CampaignHardeningAnalyzer] No visible gate sandbox records found in {latestRank}");
                return;
            }

            if (AssetDatabase.IsValidFolder(VisibleGateLevelFolder))
                AssetDatabase.DeleteAsset(VisibleGateLevelFolder);
            Directory.CreateDirectory(ToAbsolutePath(VisibleGateLevelFolder));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var report = new List<SandboxReportRow>();
            var packLevels = new List<LevelDefinition>(targetPairs * 2);
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", Inv);
            int acceptedPairs = 0;
            int scanned = 0;

            for (int r = 0; r < records.Count && acceptedPairs < targetPairs; r++)
            {
                scanned++;
                Dictionary<string, string> record = records[r];
                string sourcePath = Get(record, "assetPath");
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source == null)
                {
                    Debug.LogWarning($"[CampaignHardeningAnalyzer] Missing V5 source: {sourcePath}");
                    continue;
                }

                int order = ParseInt(Get(record, "campaignOrder"));
                string orderLabel = order > 0 ? $"L{order:000}" : $"I{r + 1:000}";
                Row baseline = AnalyzeLevel(new PackSource("visible_gate_baseline", "", false), r + 1, order, source, rules, null, null);
                if (baseline.Chains > 180 || baseline.ArrowTiles > 1150 || baseline.OpeningChoices < 10)
                {
                    report.Add(SandboxReportRow.From(order, "skipped_v5", sourcePath, "", baseline, baseline, 0, "skip_size_or_low_opening_for_visible_gate_probe"));
                    continue;
                }

                LevelDefinition visible = CreateSandboxCopy(source, $"{orderLabel}_visible_gate", stamp, VisibleGateLevelFolder);
                int ops = ApplyVisibleGateInjectionV5(visible, rules, maxAcceptedOps: 3, out string opsText);
                EditorUtility.SetDirty(visible);
                Row visibleRow = AnalyzeLevel(new PackSource("visible_gate_v5", "", false), r + 1, order, visible, rules, null, null);

                bool meaningful =
                    visibleRow.GreedySolved &&
                    ops >= 2 &&
                    visibleRow.OpeningChoices >= 2 &&
                    visibleRow.ArrowTiles >= baseline.ArrowTiles + 4 &&
                    visibleRow.AvgChoices <= baseline.AvgChoices + 1.2f &&
                    visibleRow.MaxChoices <= baseline.MaxChoices + 6 &&
                    visibleRow.DirectClearableOuterExits <= baseline.DirectClearableOuterExits + 2 &&
                    (
                        visibleRow.OpeningChoices <= baseline.OpeningChoices - 1 ||
                        visibleRow.DirectClearableOuterExits <= baseline.DirectClearableOuterExits - 1 ||
                        visibleRow.LeakScore <= baseline.LeakScore + 30 ||
                        ops >= 3
                    );

                Debug.Log(
                    $"[CampaignHardeningAnalyzer] V5 scan {orderLabel}: meaningful={meaningful}, ops={ops}, solved={visibleRow.GreedySolved}, chains {baseline.Chains}->{visibleRow.Chains}, tiles {baseline.ArrowTiles}->{visibleRow.ArrowTiles}, open {baseline.OpeningChoices}->{visibleRow.OpeningChoices}, outer {baseline.DirectClearableOuterExits}->{visibleRow.DirectClearableOuterExits}, avg {F(baseline.AvgChoices)}->{F(visibleRow.AvgChoices)}, leak {baseline.LeakScore}->{visibleRow.LeakScore}");

                if (!meaningful)
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(visible));
                    report.Add(SandboxReportRow.From(order, "skipped_v5", sourcePath, "", baseline, visibleRow, ops, $"not_visible_gate_enough:{opsText}"));
                    continue;
                }

                LevelDefinition original = CreateSandboxCopy(source, $"{orderLabel}_original", stamp, VisibleGateLevelFolder);
                Row originalRow = AnalyzeLevel(new PackSource("visible_gate_original", "", false), packLevels.Count + 1, order, original, rules, null, null);
                packLevels.Add(original);
                report.Add(SandboxReportRow.From(order, "original", sourcePath, AssetDatabase.GetAssetPath(original), baseline, originalRow, 0, "copy"));

                packLevels.Add(visible);
                report.Add(SandboxReportRow.From(order, "visible_gate_v5", sourcePath, AssetDatabase.GetAssetPath(visible), baseline, visibleRow, ops, opsText));
                acceptedPairs++;
            }

            SavePackAt(VisibleGatePackPath, "campaign500_hardening_sandbox_v5", $"Campaign 500 Hardening Sandbox V5 ({packLevels.Count})", packLevels);

            string reportPath = $"{ReportFolder}/campaign_hardening_sandbox_v5_{stamp}.csv";
            WriteSandboxReport(reportPath, report);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(VisibleGatePackPath);
            AttachPackToDemo(pack, "CampaignHardeningAnalyzer V5");

            Debug.Log($"[CampaignHardeningAnalyzer] Sandbox V5 done. scanned={scanned}, pairs={acceptedPairs}, levels={packLevels.Count}, report={reportPath}, pack={VisibleGatePackPath}");
        }

        static void BuildEarlyPeelGateSandboxFromLatestRank(int targetPairs, int scanLimit)
        {
            string latestRank = FindLatestReport("*_leak_rank.csv");
            if (string.IsNullOrEmpty(latestRank))
            {
                Debug.LogError("[CampaignHardeningAnalyzer] No leak rank report found. Run analyzer first.");
                return;
            }

            var records = ReadCsvRows(latestRank)
                .Where(r => Get(r, "packLabel") == "final")
                .Where(r => Get(r, "greedySolved").Equals("True", StringComparison.OrdinalIgnoreCase))
                .Where(r => Get(r, "status") == "HighLeak" || Get(r, "status") == "MediumLeak")
                .Take(Mathf.Max(targetPairs, scanLimit))
                .ToList();

            if (records.Count == 0)
            {
                Debug.LogWarning($"[CampaignHardeningAnalyzer] No early-peel sandbox records found in {latestRank}");
                return;
            }

            if (AssetDatabase.IsValidFolder(EarlyPeelGateLevelFolder))
                AssetDatabase.DeleteAsset(EarlyPeelGateLevelFolder);
            Directory.CreateDirectory(ToAbsolutePath(EarlyPeelGateLevelFolder));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var report = new List<SandboxReportRow>();
            var packLevels = new List<LevelDefinition>(targetPairs * 2);
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", Inv);
            int acceptedPairs = 0;
            int scanned = 0;

            for (int r = 0; r < records.Count && acceptedPairs < targetPairs; r++)
            {
                scanned++;
                Dictionary<string, string> record = records[r];
                string sourcePath = Get(record, "assetPath");
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source == null)
                {
                    Debug.LogWarning($"[CampaignHardeningAnalyzer] Missing V6 source: {sourcePath}");
                    continue;
                }

                int order = ParseInt(Get(record, "campaignOrder"));
                string orderLabel = order > 0 ? $"L{order:000}" : $"I{r + 1:000}";
                Row baseline = AnalyzeLevel(new PackSource("early_peel_gate_baseline", "", false), r + 1, order, source, rules, null, null);
                if (baseline.Chains > 190 || baseline.ArrowTiles > 1180 || baseline.OpeningChoices < 12)
                {
                    report.Add(SandboxReportRow.From(order, "skipped_v6", sourcePath, "", baseline, baseline, 0, "skip_size_or_low_opening_for_early_peel_gate_probe"));
                    continue;
                }

                LevelDefinition gated = CreateSandboxCopy(source, $"{orderLabel}_early_peel_gate", stamp, EarlyPeelGateLevelFolder);
                int ops = ApplyEarlyPeelGateInjectionV6(gated, rules, maxAcceptedOps: 4, out string opsText);
                EditorUtility.SetDirty(gated);
                Row gatedRow = AnalyzeLevel(new PackSource("early_peel_gate_v6", "", false), r + 1, order, gated, rules, null, null);

                bool meaningful =
                    gatedRow.GreedySolved &&
                    ops >= 2 &&
                    gatedRow.OpeningChoices >= 2 &&
                    gatedRow.ArrowTiles >= baseline.ArrowTiles + 5 &&
                    gatedRow.AvgChoices <= baseline.AvgChoices + 1.0f &&
                    gatedRow.MaxChoices <= baseline.MaxChoices + 5 &&
                    gatedRow.DirectClearableOuterExits <= baseline.DirectClearableOuterExits + 1 &&
                    (
                        gatedRow.OpeningChoices <= baseline.OpeningChoices - 2 ||
                        gatedRow.DirectClearableOuterExits <= baseline.DirectClearableOuterExits - 2 ||
                        gatedRow.AvgChoices <= baseline.AvgChoices - 1.5f ||
                        gatedRow.LeakScore <= baseline.LeakScore - 25 ||
                        ops >= 3
                    );

                Debug.Log(
                    $"[CampaignHardeningAnalyzer] V6 scan {orderLabel}: meaningful={meaningful}, ops={ops}, solved={gatedRow.GreedySolved}, chains {baseline.Chains}->{gatedRow.Chains}, tiles {baseline.ArrowTiles}->{gatedRow.ArrowTiles}, open {baseline.OpeningChoices}->{gatedRow.OpeningChoices}, outer {baseline.DirectClearableOuterExits}->{gatedRow.DirectClearableOuterExits}, avg {F(baseline.AvgChoices)}->{F(gatedRow.AvgChoices)}, leak {baseline.LeakScore}->{gatedRow.LeakScore}");

                if (!meaningful)
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(gated));
                    report.Add(SandboxReportRow.From(order, "skipped_v6", sourcePath, "", baseline, gatedRow, ops, $"not_early_peel_gate_enough:{opsText}"));
                    continue;
                }

                LevelDefinition original = CreateSandboxCopy(source, $"{orderLabel}_original", stamp, EarlyPeelGateLevelFolder);
                Row originalRow = AnalyzeLevel(new PackSource("early_peel_gate_original", "", false), packLevels.Count + 1, order, original, rules, null, null);
                packLevels.Add(original);
                report.Add(SandboxReportRow.From(order, "original", sourcePath, AssetDatabase.GetAssetPath(original), baseline, originalRow, 0, "copy"));

                packLevels.Add(gated);
                report.Add(SandboxReportRow.From(order, "early_peel_gate_v6", sourcePath, AssetDatabase.GetAssetPath(gated), baseline, gatedRow, ops, opsText));
                acceptedPairs++;
            }

            SavePackAt(EarlyPeelGatePackPath, "campaign500_hardening_sandbox_v6", $"Campaign 500 Hardening Sandbox V6 ({packLevels.Count})", packLevels);

            string reportPath = $"{ReportFolder}/campaign_hardening_sandbox_v6_{stamp}.csv";
            WriteSandboxReport(reportPath, report);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(EarlyPeelGatePackPath);
            AttachPackToDemo(pack, "CampaignHardeningAnalyzer V6");

            Debug.Log($"[CampaignHardeningAnalyzer] Sandbox V6 done. scanned={scanned}, pairs={acceptedPairs}, levels={packLevels.Count}, report={reportPath}, pack={EarlyPeelGatePackPath}");
        }

        static void BuildOpeningPeelGateSandboxFromLatestRank(int targetPairs, int scanLimit)
        {
            string latestRank = FindLatestReport("*_leak_rank.csv");
            if (string.IsNullOrEmpty(latestRank))
            {
                Debug.LogError("[CampaignHardeningAnalyzer] No leak rank report found. Run analyzer first.");
                return;
            }

            var records = ReadCsvRows(latestRank)
                .Where(r => Get(r, "packLabel") == "final")
                .Where(r => Get(r, "greedySolved").Equals("True", StringComparison.OrdinalIgnoreCase))
                .Where(r => Get(r, "status") == "HighLeak" || Get(r, "status") == "MediumLeak")
                .Take(Mathf.Max(targetPairs, scanLimit))
                .ToList();

            if (records.Count == 0)
            {
                Debug.LogWarning($"[CampaignHardeningAnalyzer] No opening-peel sandbox records found in {latestRank}");
                return;
            }

            if (AssetDatabase.IsValidFolder(OpeningPeelGateLevelFolder))
                AssetDatabase.DeleteAsset(OpeningPeelGateLevelFolder);
            Directory.CreateDirectory(ToAbsolutePath(OpeningPeelGateLevelFolder));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var report = new List<SandboxReportRow>();
            var packLevels = new List<LevelDefinition>(targetPairs * 2);
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", Inv);
            int acceptedPairs = 0;
            int scanned = 0;

            for (int r = 0; r < records.Count && acceptedPairs < targetPairs; r++)
            {
                scanned++;
                Dictionary<string, string> record = records[r];
                string sourcePath = Get(record, "assetPath");
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source == null)
                {
                    Debug.LogWarning($"[CampaignHardeningAnalyzer] Missing V7 source: {sourcePath}");
                    continue;
                }

                int order = ParseInt(Get(record, "campaignOrder"));
                string orderLabel = order > 0 ? $"L{order:000}" : $"I{r + 1:000}";
                Row baseline = AnalyzeLevel(new PackSource("opening_peel_gate_baseline", "", false), r + 1, order, source, rules, null, null);
                if (baseline.Chains > 190 || baseline.ArrowTiles > 1180 || baseline.OpeningChoices < 14)
                {
                    report.Add(SandboxReportRow.From(order, "skipped_v7", sourcePath, "", baseline, baseline, 0, "skip_size_or_low_opening_for_opening_peel_gate_probe"));
                    continue;
                }

                LevelDefinition gated = CreateSandboxCopy(source, $"{orderLabel}_opening_peel_gate", stamp, OpeningPeelGateLevelFolder);
                int ops = ApplyOpeningPeelGateInjectionV7(gated, rules, maxOpeningOps: 5, maxPeelOps: 0, out string opsText);
                EditorUtility.SetDirty(gated);
                Row gatedRow = AnalyzeLevel(new PackSource("opening_peel_gate_v7", "", false), r + 1, order, gated, rules, null, null);

                bool meaningful =
                    gatedRow.GreedySolved &&
                    ops >= 3 &&
                    gatedRow.OpeningChoices >= 2 &&
                    gatedRow.ArrowTiles >= baseline.ArrowTiles + 6 &&
                    gatedRow.AvgChoices <= baseline.AvgChoices + 0.6f &&
                    gatedRow.MaxChoices <= baseline.MaxChoices + 4 &&
                    gatedRow.DirectClearableOuterExits <= baseline.DirectClearableOuterExits &&
                    (
                        gatedRow.OpeningChoices <= baseline.OpeningChoices - 5 ||
                        gatedRow.DirectClearableOuterExits <= baseline.DirectClearableOuterExits - 5 ||
                        gatedRow.AvgChoices <= baseline.AvgChoices - 2.0f ||
                        gatedRow.LeakScore <= baseline.LeakScore - 70
                    );

                Debug.Log(
                    $"[CampaignHardeningAnalyzer] V7 scan {orderLabel}: meaningful={meaningful}, ops={ops}, solved={gatedRow.GreedySolved}, chains {baseline.Chains}->{gatedRow.Chains}, tiles {baseline.ArrowTiles}->{gatedRow.ArrowTiles}, open {baseline.OpeningChoices}->{gatedRow.OpeningChoices}, outer {baseline.DirectClearableOuterExits}->{gatedRow.DirectClearableOuterExits}, avg {F(baseline.AvgChoices)}->{F(gatedRow.AvgChoices)}, leak {baseline.LeakScore}->{gatedRow.LeakScore}");

                if (!meaningful)
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(gated));
                    report.Add(SandboxReportRow.From(order, "skipped_v7", sourcePath, "", baseline, gatedRow, ops, $"not_opening_peel_gate_enough:{opsText}"));
                    continue;
                }

                LevelDefinition original = CreateSandboxCopy(source, $"{orderLabel}_original", stamp, OpeningPeelGateLevelFolder);
                Row originalRow = AnalyzeLevel(new PackSource("opening_peel_gate_original", "", false), packLevels.Count + 1, order, original, rules, null, null);
                packLevels.Add(original);
                report.Add(SandboxReportRow.From(order, "original", sourcePath, AssetDatabase.GetAssetPath(original), baseline, originalRow, 0, "copy"));

                packLevels.Add(gated);
                report.Add(SandboxReportRow.From(order, "opening_peel_gate_v7", sourcePath, AssetDatabase.GetAssetPath(gated), baseline, gatedRow, ops, opsText));
                acceptedPairs++;
            }

            SavePackAt(OpeningPeelGatePackPath, "campaign500_hardening_sandbox_v7", $"Campaign 500 Hardening Sandbox V7 ({packLevels.Count})", packLevels);

            string reportPath = $"{ReportFolder}/campaign_hardening_sandbox_v7_{stamp}.csv";
            WriteSandboxReport(reportPath, report);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(OpeningPeelGatePackPath);
            AttachPackToDemo(pack, "CampaignHardeningAnalyzer V7");

            Debug.Log($"[CampaignHardeningAnalyzer] Sandbox V7 done. scanned={scanned}, pairs={acceptedPairs}, levels={packLevels.Count}, report={reportPath}, pack={OpeningPeelGatePackPath}");
        }

        static void BuildOpeningRewireGateSandboxFromLatestRank(int targetPairs, int scanLimit)
        {
            string latestRank = FindLatestReport("*_leak_rank.csv");
            if (string.IsNullOrEmpty(latestRank))
            {
                Debug.LogError("[CampaignHardeningAnalyzer] No leak rank report found. Run analyzer first.");
                return;
            }

            var records = ReadCsvRows(latestRank)
                .Where(r => Get(r, "packLabel") == "final")
                .Where(r => Get(r, "greedySolved").Equals("True", StringComparison.OrdinalIgnoreCase))
                .Where(r => Get(r, "status") == "HighLeak" || Get(r, "status") == "MediumLeak")
                .Take(Mathf.Max(targetPairs, scanLimit))
                .ToList();

            if (records.Count == 0)
            {
                Debug.LogWarning($"[CampaignHardeningAnalyzer] No opening-rewire sandbox records found in {latestRank}");
                return;
            }

            if (AssetDatabase.IsValidFolder(OpeningRewireGateLevelFolder))
                AssetDatabase.DeleteAsset(OpeningRewireGateLevelFolder);
            Directory.CreateDirectory(ToAbsolutePath(OpeningRewireGateLevelFolder));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var report = new List<SandboxReportRow>();
            var packLevels = new List<LevelDefinition>(targetPairs * 2);
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", Inv);
            int acceptedPairs = 0;
            int scanned = 0;

            for (int r = 0; r < records.Count && acceptedPairs < targetPairs; r++)
            {
                scanned++;
                Dictionary<string, string> record = records[r];
                string sourcePath = Get(record, "assetPath");
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source == null)
                {
                    Debug.LogWarning($"[CampaignHardeningAnalyzer] Missing V8 source: {sourcePath}");
                    continue;
                }

                int order = ParseInt(Get(record, "campaignOrder"));
                string orderLabel = order > 0 ? $"L{order:000}" : $"I{r + 1:000}";
                Row baseline = AnalyzeLevel(new PackSource("opening_rewire_gate_baseline", "", false), r + 1, order, source, rules, null, null);
                if (baseline.Chains > 190 || baseline.ArrowTiles > 1180 || baseline.OpeningChoices < 14)
                {
                    report.Add(SandboxReportRow.From(order, "skipped_v8", sourcePath, "", baseline, baseline, 0, "skip_size_or_low_opening_for_opening_rewire_gate_probe"));
                    continue;
                }

                LevelDefinition rewired = CreateSandboxCopy(source, $"{orderLabel}_opening_rewire_gate", stamp, OpeningRewireGateLevelFolder);
                int ops = ApplyOpeningRewireGateInjectionV8(rewired, rules, maxRewireOps: 4, maxFallbackGateOps: 1, out string opsText);
                EditorUtility.SetDirty(rewired);
                Row rewiredRow = AnalyzeLevel(new PackSource("opening_rewire_gate_v8", "", false), r + 1, order, rewired, rules, null, null);

                bool hasRewire = opsText.Contains("openingRewire:", StringComparison.Ordinal);
                bool meaningful =
                    rewiredRow.GreedySolved &&
                    hasRewire &&
                    ops >= 2 &&
                    rewiredRow.OpeningChoices >= 2 &&
                    rewiredRow.AvgChoices <= baseline.AvgChoices + 0.6f &&
                    rewiredRow.MaxChoices <= baseline.MaxChoices + 4 &&
                    rewiredRow.DirectClearableOuterExits <= baseline.DirectClearableOuterExits &&
                    (
                        rewiredRow.OpeningChoices <= baseline.OpeningChoices - 5 ||
                        rewiredRow.DirectClearableOuterExits <= baseline.DirectClearableOuterExits - 5 ||
                        rewiredRow.AvgChoices <= baseline.AvgChoices - 2.0f ||
                        rewiredRow.LeakScore <= baseline.LeakScore - 70
                    );

                Debug.Log(
                    $"[CampaignHardeningAnalyzer] V8 scan {orderLabel}: meaningful={meaningful}, ops={ops}, solved={rewiredRow.GreedySolved}, chains {baseline.Chains}->{rewiredRow.Chains}, tiles {baseline.ArrowTiles}->{rewiredRow.ArrowTiles}, open {baseline.OpeningChoices}->{rewiredRow.OpeningChoices}, outer {baseline.DirectClearableOuterExits}->{rewiredRow.DirectClearableOuterExits}, avg {F(baseline.AvgChoices)}->{F(rewiredRow.AvgChoices)}, leak {baseline.LeakScore}->{rewiredRow.LeakScore}");

                if (!meaningful)
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(rewired));
                    report.Add(SandboxReportRow.From(order, "skipped_v8", sourcePath, "", baseline, rewiredRow, ops, $"not_opening_rewire_gate_enough:{opsText}"));
                    continue;
                }

                LevelDefinition original = CreateSandboxCopy(source, $"{orderLabel}_original", stamp, OpeningRewireGateLevelFolder);
                Row originalRow = AnalyzeLevel(new PackSource("opening_rewire_gate_original", "", false), packLevels.Count + 1, order, original, rules, null, null);
                packLevels.Add(original);
                report.Add(SandboxReportRow.From(order, "original", sourcePath, AssetDatabase.GetAssetPath(original), baseline, originalRow, 0, "copy"));

                packLevels.Add(rewired);
                report.Add(SandboxReportRow.From(order, "opening_rewire_gate_v8", sourcePath, AssetDatabase.GetAssetPath(rewired), baseline, rewiredRow, ops, opsText));
                acceptedPairs++;
            }

            SavePackAt(OpeningRewireGatePackPath, "campaign500_hardening_sandbox_v8", $"Campaign 500 Hardening Sandbox V8 ({packLevels.Count})", packLevels);

            string reportPath = $"{ReportFolder}/campaign_hardening_sandbox_v8_{stamp}.csv";
            WriteSandboxReport(reportPath, report);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(OpeningRewireGatePackPath);
            AttachPackToDemo(pack, "CampaignHardeningAnalyzer V8");

            Debug.Log($"[CampaignHardeningAnalyzer] Sandbox V8 done. scanned={scanned}, pairs={acceptedPairs}, levels={packLevels.Count}, report={reportPath}, pack={OpeningRewireGatePackPath}");
        }

        static void BuildOpeningOuterRewireGateSandboxFromLatestRank(int targetPairs, int scanLimit)
        {
            string latestRank = FindLatestReport("*_leak_rank.csv");
            if (string.IsNullOrEmpty(latestRank))
            {
                Debug.LogError("[CampaignHardeningAnalyzer] No leak rank report found. Run analyzer first.");
                return;
            }

            var records = ReadCsvRows(latestRank)
                .Where(r => Get(r, "packLabel") == "final")
                .Where(r => Get(r, "greedySolved").Equals("True", StringComparison.OrdinalIgnoreCase))
                .Where(r => Get(r, "status") == "HighLeak" || Get(r, "status") == "MediumLeak")
                .Take(Mathf.Max(targetPairs, scanLimit))
                .ToList();

            if (records.Count == 0)
            {
                Debug.LogWarning($"[CampaignHardeningAnalyzer] No opening-outer-rewire sandbox records found in {latestRank}");
                return;
            }

            if (AssetDatabase.IsValidFolder(OpeningOuterRewireGateLevelFolder))
                AssetDatabase.DeleteAsset(OpeningOuterRewireGateLevelFolder);
            Directory.CreateDirectory(ToAbsolutePath(OpeningOuterRewireGateLevelFolder));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var report = new List<SandboxReportRow>();
            var packLevels = new List<LevelDefinition>(targetPairs * 2);
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", Inv);
            int acceptedPairs = 0;
            int scanned = 0;

            for (int r = 0; r < records.Count && acceptedPairs < targetPairs; r++)
            {
                scanned++;
                Dictionary<string, string> record = records[r];
                string sourcePath = Get(record, "assetPath");
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source == null)
                {
                    Debug.LogWarning($"[CampaignHardeningAnalyzer] Missing V9 source: {sourcePath}");
                    continue;
                }

                int order = ParseInt(Get(record, "campaignOrder"));
                string orderLabel = order > 0 ? $"L{order:000}" : $"I{r + 1:000}";
                Row baseline = AnalyzeLevel(new PackSource("opening_outer_rewire_gate_baseline", "", false), r + 1, order, source, rules, null, null);
                if (baseline.Chains > 190 || baseline.ArrowTiles > 1180 || baseline.OpeningChoices < 14)
                {
                    report.Add(SandboxReportRow.From(order, "skipped_v9", sourcePath, "", baseline, baseline, 0, "skip_size_or_low_opening_for_opening_outer_rewire_gate_probe"));
                    continue;
                }

                LevelDefinition rewired = CreateSandboxCopy(source, $"{orderLabel}_opening_outer_rewire_gate", stamp, OpeningOuterRewireGateLevelFolder);
                int ops = ApplyOpeningOuterRewireGateInjectionV9(rewired, rules, maxRewireOps: 3, maxOuterFlipOps: 18, out string opsText);
                EditorUtility.SetDirty(rewired);
                Row rewiredRow = AnalyzeLevel(new PackSource("opening_outer_rewire_gate_v9", "", false), r + 1, order, rewired, rules, null, null);

                bool hasOuterInward = opsText.Contains("outerInward:", StringComparison.Ordinal);
                int directOuterDrop = baseline.DirectOuterExits - rewiredRow.DirectOuterExits;
                int clearOuterDrop = baseline.DirectClearableOuterExits - rewiredRow.DirectClearableOuterExits;
                bool meaningful =
                    rewiredRow.GreedySolved &&
                    hasOuterInward &&
                    ops >= 6 &&
                    rewiredRow.OpeningChoices >= 2 &&
                    rewiredRow.AvgChoices <= baseline.AvgChoices + 1.2f &&
                    rewiredRow.MaxChoices <= baseline.MaxChoices + 7 &&
                    rewiredRow.DirectClearableOuterExits <= baseline.DirectClearableOuterExits + 1 &&
                    directOuterDrop >= 8 &&
                    (
                        rewiredRow.OpeningChoices <= baseline.OpeningChoices - 6 ||
                        clearOuterDrop >= 5 ||
                        directOuterDrop >= 12 ||
                        rewiredRow.AvgChoices <= baseline.AvgChoices - 1.6f ||
                        rewiredRow.LeakScore <= baseline.LeakScore - 60
                    );

                Debug.Log(
                    $"[CampaignHardeningAnalyzer] V9 scan {orderLabel}: meaningful={meaningful}, ops={ops}, solved={rewiredRow.GreedySolved}, chains {baseline.Chains}->{rewiredRow.Chains}, tiles {baseline.ArrowTiles}->{rewiredRow.ArrowTiles}, open {baseline.OpeningChoices}->{rewiredRow.OpeningChoices}, clearOuter {baseline.DirectClearableOuterExits}->{rewiredRow.DirectClearableOuterExits}, directOuter {baseline.DirectOuterExits}->{rewiredRow.DirectOuterExits}, avg {F(baseline.AvgChoices)}->{F(rewiredRow.AvgChoices)}, leak {baseline.LeakScore}->{rewiredRow.LeakScore}");

                if (!meaningful)
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(rewired));
                    report.Add(SandboxReportRow.From(order, "skipped_v9", sourcePath, "", baseline, rewiredRow, ops, $"not_opening_outer_rewire_gate_enough:{opsText}"));
                    continue;
                }

                LevelDefinition original = CreateSandboxCopy(source, $"{orderLabel}_original", stamp, OpeningOuterRewireGateLevelFolder);
                Row originalRow = AnalyzeLevel(new PackSource("opening_outer_rewire_gate_original", "", false), packLevels.Count + 1, order, original, rules, null, null);
                packLevels.Add(original);
                report.Add(SandboxReportRow.From(order, "original", sourcePath, AssetDatabase.GetAssetPath(original), baseline, originalRow, 0, "copy"));

                packLevels.Add(rewired);
                report.Add(SandboxReportRow.From(order, "opening_outer_rewire_gate_v9", sourcePath, AssetDatabase.GetAssetPath(rewired), baseline, rewiredRow, ops, opsText));
                acceptedPairs++;
            }

            SavePackAt(OpeningOuterRewireGatePackPath, "campaign500_hardening_sandbox_v9", $"Campaign 500 Hardening Sandbox V9 ({packLevels.Count})", packLevels);

            string reportPath = $"{ReportFolder}/campaign_hardening_sandbox_v9_{stamp}.csv";
            WriteSandboxReport(reportPath, report);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(OpeningOuterRewireGatePackPath);
            AttachPackToDemo(pack, "CampaignHardeningAnalyzer V9");

            Debug.Log($"[CampaignHardeningAnalyzer] Sandbox V9 done. scanned={scanned}, pairs={acceptedPairs}, levels={packLevels.Count}, report={reportPath}, pack={OpeningOuterRewireGatePackPath}");
        }

        static void BuildOuterExitEndpointSandboxFromLatestRank(int targetPairs, int scanLimit)
        {
            string latestRank = FindLatestReport("*_leak_rank.csv");
            if (string.IsNullOrEmpty(latestRank))
            {
                Debug.LogError("[CampaignHardeningAnalyzer] No leak rank report found. Run analyzer first.");
                return;
            }

            var records = ReadCsvRows(latestRank)
                .Where(r => Get(r, "packLabel") == "final")
                .Where(r => Get(r, "greedySolved").Equals("True", StringComparison.OrdinalIgnoreCase))
                .Where(r => Get(r, "status") == "HighLeak" || Get(r, "status") == "MediumLeak")
                .Take(Mathf.Max(targetPairs, scanLimit))
                .ToList();

            if (records.Count == 0)
            {
                Debug.LogWarning($"[CampaignHardeningAnalyzer] No outer-exit endpoint sandbox records found in {latestRank}");
                return;
            }

            if (AssetDatabase.IsValidFolder(OuterExitEndpointLevelFolder))
                AssetDatabase.DeleteAsset(OuterExitEndpointLevelFolder);
            Directory.CreateDirectory(ToAbsolutePath(OuterExitEndpointLevelFolder));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var report = new List<SandboxReportRow>();
            var packLevels = new List<LevelDefinition>(targetPairs * 2);
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", Inv);
            int acceptedPairs = 0;
            int scanned = 0;

            for (int r = 0; r < records.Count && acceptedPairs < targetPairs; r++)
            {
                scanned++;
                Dictionary<string, string> record = records[r];
                string sourcePath = Get(record, "assetPath");
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source == null)
                {
                    Debug.LogWarning($"[CampaignHardeningAnalyzer] Missing V10 source: {sourcePath}");
                    continue;
                }

                int order = ParseInt(Get(record, "campaignOrder"));
                string orderLabel = order > 0 ? $"L{order:000}" : $"I{r + 1:000}";
                Row baseline = AnalyzeLevel(new PackSource("outer_exit_endpoint_baseline", "", false), r + 1, order, source, rules, null, null);
                if (baseline.Chains > 190 || baseline.ArrowTiles > 1180 || baseline.DirectOuterExits < 18)
                {
                    report.Add(SandboxReportRow.From(order, "skipped_v10", sourcePath, "", baseline, baseline, 0, "skip_size_or_low_outer_for_outer_endpoint_probe"));
                    continue;
                }

                LevelDefinition hardened = CreateSandboxCopy(source, $"{orderLabel}_outer_exit_endpoint", stamp, OuterExitEndpointLevelFolder);
                int ops = ApplyOuterExitEndpointHardeningV10(hardened, rules, includeBaseV9: true, out string opsText);
                EditorUtility.SetDirty(hardened);
                Row hardenedRow = AnalyzeLevel(new PackSource("outer_exit_endpoint_v10", "", false), r + 1, order, hardened, rules, null, null);

                int directOuterDrop = baseline.DirectOuterExits - hardenedRow.DirectOuterExits;
                int clearOuterDrop = baseline.DirectClearableOuterExits - hardenedRow.DirectClearableOuterExits;
                bool hasEndpoint = opsText.Contains("endpointReroute:", StringComparison.Ordinal) || opsText.Contains("endpointTrim:", StringComparison.Ordinal);
                bool meaningful =
                    hardenedRow.GreedySolved &&
                    hasEndpoint &&
                    ops >= 8 &&
                    hardenedRow.OpeningChoices >= 2 &&
                    hardenedRow.AvgChoices <= baseline.AvgChoices + 1.6f &&
                    hardenedRow.MaxChoices <= baseline.MaxChoices + 9 &&
                    hardenedRow.DirectClearableOuterExits <= baseline.DirectClearableOuterExits + 1 &&
                    (
                        directOuterDrop >= 12 ||
                        hardenedRow.DirectOuterExits <= 14 ||
                        clearOuterDrop >= 10
                    );

                Debug.Log(
                    $"[CampaignHardeningAnalyzer] V10 scan {orderLabel}: meaningful={meaningful}, ops={ops}, solved={hardenedRow.GreedySolved}, chains {baseline.Chains}->{hardenedRow.Chains}, tiles {baseline.ArrowTiles}->{hardenedRow.ArrowTiles}, open {baseline.OpeningChoices}->{hardenedRow.OpeningChoices}, clearOuter {baseline.DirectClearableOuterExits}->{hardenedRow.DirectClearableOuterExits}, directOuter {baseline.DirectOuterExits}->{hardenedRow.DirectOuterExits}, avg {F(baseline.AvgChoices)}->{F(hardenedRow.AvgChoices)}, leak {baseline.LeakScore}->{hardenedRow.LeakScore}");

                if (!meaningful)
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(hardened));
                    report.Add(SandboxReportRow.From(order, "skipped_v10", sourcePath, "", baseline, hardenedRow, ops, $"not_outer_endpoint_enough:{opsText}"));
                    continue;
                }

                LevelDefinition original = CreateSandboxCopy(source, $"{orderLabel}_original", stamp, OuterExitEndpointLevelFolder);
                Row originalRow = AnalyzeLevel(new PackSource("outer_exit_endpoint_original", "", false), packLevels.Count + 1, order, original, rules, null, null);
                packLevels.Add(original);
                report.Add(SandboxReportRow.From(order, "original", sourcePath, AssetDatabase.GetAssetPath(original), baseline, originalRow, 0, "copy"));

                packLevels.Add(hardened);
                report.Add(SandboxReportRow.From(order, "outer_exit_endpoint_v10", sourcePath, AssetDatabase.GetAssetPath(hardened), baseline, hardenedRow, ops, opsText));
                acceptedPairs++;
            }

            SavePackAt(OuterExitEndpointPackPath, "campaign500_hardening_sandbox_v10", $"Campaign 500 Hardening Sandbox V10 ({packLevels.Count})", packLevels);

            string reportPath = $"{ReportFolder}/campaign_hardening_sandbox_v10_{stamp}.csv";
            WriteSandboxReport(reportPath, report);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(OuterExitEndpointPackPath);
            AttachPackToDemo(pack, "CampaignHardeningAnalyzer V10");

            Debug.Log($"[CampaignHardeningAnalyzer] Sandbox V10 done. scanned={scanned}, pairs={acceptedPairs}, levels={packLevels.Count}, report={reportPath}, pack={OuterExitEndpointPackPath}");
        }

        static void BuildOuterExitEndpointSandboxFromLatestV9(int targetPairs, int scanLimit)
        {
            string latestV9 = FindLatestReport("campaign_hardening_sandbox_v9_*.csv");
            if (string.IsNullOrEmpty(latestV9))
            {
                Debug.LogWarning("[CampaignHardeningAnalyzer] No V9 sandbox report found. Falling back to final leak rank for V10.");
                BuildOuterExitEndpointSandboxFromLatestRank(targetPairs, scanLimit);
                return;
            }

            var records = ReadCsvRows(latestV9)
                .Where(r => Get(r, "variant") == "opening_outer_rewire_gate_v9")
                .Where(r => Get(r, "greedySolved").Equals("True", StringComparison.OrdinalIgnoreCase))
                .Take(Mathf.Max(targetPairs, scanLimit))
                .ToList();

            if (records.Count == 0)
            {
                Debug.LogWarning($"[CampaignHardeningAnalyzer] No V9 rows found in {latestV9}. Falling back to final leak rank for V10.");
                BuildOuterExitEndpointSandboxFromLatestRank(targetPairs, scanLimit);
                return;
            }

            if (AssetDatabase.IsValidFolder(OuterExitEndpointLevelFolder))
                AssetDatabase.DeleteAsset(OuterExitEndpointLevelFolder);
            Directory.CreateDirectory(ToAbsolutePath(OuterExitEndpointLevelFolder));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var report = new List<SandboxReportRow>();
            var packLevels = new List<LevelDefinition>(targetPairs * 2);
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", Inv);
            int acceptedPairs = 0;
            int scanned = 0;

            for (int r = 0; r < records.Count && acceptedPairs < targetPairs; r++)
            {
                scanned++;
                Dictionary<string, string> record = records[r];
                string sourcePath = Get(record, "assetPath");
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source == null)
                {
                    Debug.LogWarning($"[CampaignHardeningAnalyzer] Missing V10 V9-source: {sourcePath}");
                    continue;
                }

                int order = ParseInt(Get(record, "campaignOrder"));
                string orderLabel = order > 0 ? $"L{order:000}" : $"I{r + 1:000}";
                Row baseline = AnalyzeLevel(new PackSource("outer_exit_endpoint_v9_source", "", false), r + 1, order, source, rules, null, null);
                if (baseline.DirectOuterExits < 10)
                {
                    report.Add(SandboxReportRow.From(order, "skipped_v10", sourcePath, "", baseline, baseline, 0, "skip_low_outer_after_v9"));
                    continue;
                }

                LevelDefinition hardened = CreateSandboxCopy(source, $"{orderLabel}_outer_exit_endpoint", stamp, OuterExitEndpointLevelFolder);
                int ops = ApplyOuterExitEndpointHardeningV10(hardened, rules, includeBaseV9: false, out string opsText);
                EditorUtility.SetDirty(hardened);
                Row hardenedRow = AnalyzeLevel(new PackSource("outer_exit_endpoint_v10", "", false), r + 1, order, hardened, rules, null, null);

                int directOuterDrop = baseline.DirectOuterExits - hardenedRow.DirectOuterExits;
                int clearOuterDrop = baseline.DirectClearableOuterExits - hardenedRow.DirectClearableOuterExits;
                bool hasEndpoint = opsText.Contains("endpointReroute:", StringComparison.Ordinal) || opsText.Contains("endpointTrim:", StringComparison.Ordinal);
                bool meaningful =
                    hardenedRow.GreedySolved &&
                    hasEndpoint &&
                    ops >= 1 &&
                    hardenedRow.OpeningChoices >= 2 &&
                    hardenedRow.AvgChoices <= baseline.AvgChoices + 1.2f &&
                    hardenedRow.MaxChoices <= baseline.MaxChoices + 8 &&
                    hardenedRow.DirectClearableOuterExits <= baseline.DirectClearableOuterExits + 1 &&
                    (
                        directOuterDrop >= 4 ||
                        hardenedRow.DirectOuterExits <= 12 ||
                        clearOuterDrop >= 4
                    );

                Debug.Log(
                    $"[CampaignHardeningAnalyzer] V10 scan {orderLabel}: meaningful={meaningful}, ops={ops}, solved={hardenedRow.GreedySolved}, chains {baseline.Chains}->{hardenedRow.Chains}, tiles {baseline.ArrowTiles}->{hardenedRow.ArrowTiles}, open {baseline.OpeningChoices}->{hardenedRow.OpeningChoices}, clearOuter {baseline.DirectClearableOuterExits}->{hardenedRow.DirectClearableOuterExits}, directOuter {baseline.DirectOuterExits}->{hardenedRow.DirectOuterExits}, avg {F(baseline.AvgChoices)}->{F(hardenedRow.AvgChoices)}, leak {baseline.LeakScore}->{hardenedRow.LeakScore}");

                if (!meaningful)
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(hardened));
                    report.Add(SandboxReportRow.From(order, "skipped_v10", sourcePath, "", baseline, hardenedRow, ops, $"not_outer_endpoint_enough:{opsText}"));
                    continue;
                }

                LevelDefinition before = CreateSandboxCopy(source, $"{orderLabel}_v9_before_endpoint", stamp, OuterExitEndpointLevelFolder);
                Row beforeRow = AnalyzeLevel(new PackSource("outer_exit_endpoint_v9_before", "", false), packLevels.Count + 1, order, before, rules, null, null);
                packLevels.Add(before);
                report.Add(SandboxReportRow.From(order, "v9_before_endpoint", sourcePath, AssetDatabase.GetAssetPath(before), baseline, beforeRow, 0, "copy"));

                packLevels.Add(hardened);
                report.Add(SandboxReportRow.From(order, "outer_exit_endpoint_v10", sourcePath, AssetDatabase.GetAssetPath(hardened), baseline, hardenedRow, ops, opsText));
                acceptedPairs++;
            }

            SavePackAt(OuterExitEndpointPackPath, "campaign500_hardening_sandbox_v10", $"Campaign 500 Hardening Sandbox V10 ({packLevels.Count})", packLevels);

            string reportPath = $"{ReportFolder}/campaign_hardening_sandbox_v10_{stamp}.csv";
            WriteSandboxReport(reportPath, report);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(OuterExitEndpointPackPath);
            AttachPackToDemo(pack, "CampaignHardeningAnalyzer V10");

            Debug.Log($"[CampaignHardeningAnalyzer] Sandbox V10 done from V9. scanned={scanned}, pairs={acceptedPairs}, levels={packLevels.Count}, report={reportPath}, pack={OuterExitEndpointPackPath}, sourceReport={latestV9}");
        }

        static void BuildMultiLayerOuterExitSandboxFromLatestV10(int targetPairs, int scanLimit)
        {
            string latestV10 = FindLatestReport("campaign_hardening_sandbox_v10_*.csv");
            if (string.IsNullOrEmpty(latestV10))
            {
                Debug.LogWarning("[CampaignHardeningAnalyzer] No V10 sandbox report found. Run V10 before V11.");
                return;
            }

            var records = ReadCsvRows(latestV10)
                .Where(r => Get(r, "variant") == "outer_exit_endpoint_v10")
                .Where(r => Get(r, "greedySolved").Equals("True", StringComparison.OrdinalIgnoreCase))
                .Take(Mathf.Max(targetPairs, scanLimit))
                .ToList();

            if (records.Count == 0)
            {
                Debug.LogWarning($"[CampaignHardeningAnalyzer] No V10 output rows found in {latestV10}.");
                return;
            }

            if (AssetDatabase.IsValidFolder(MultiLayerOuterExitLevelFolder))
                AssetDatabase.DeleteAsset(MultiLayerOuterExitLevelFolder);
            Directory.CreateDirectory(ToAbsolutePath(MultiLayerOuterExitLevelFolder));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var report = new List<SandboxReportRow>();
            var packLevels = new List<LevelDefinition>(targetPairs * 2);
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", Inv);
            int acceptedPairs = 0;
            int scanned = 0;

            for (int r = 0; r < records.Count && acceptedPairs < targetPairs; r++)
            {
                scanned++;
                Dictionary<string, string> record = records[r];
                string sourcePath = Get(record, "assetPath");
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source == null)
                {
                    Debug.LogWarning($"[CampaignHardeningAnalyzer] Missing V11 V10-source: {sourcePath}");
                    continue;
                }

                int order = ParseInt(Get(record, "campaignOrder"));
                string orderLabel = order > 0 ? $"L{order:000}" : $"I{r + 1:000}";
                Row baseline = AnalyzeLevel(new PackSource("multi_layer_v10_source", "", false), r + 1, order, source, rules, null, null);
                PeelOuterStatsV11 baselinePeel = AnalyzePeelOuterStatsV11(source, rules, maxWaves: 4, maxRemovedChains: 96);
                if (baselinePeel.TotalOuter < 6 && baseline.DirectOuterExits < 8)
                {
                    report.Add(SandboxReportRow.FromWithPeel(order, "skipped_v11", sourcePath, "", baseline, baseline, 0, $"skip_low_peel_outer:{baselinePeel.Summary}", baselinePeel, baselinePeel));
                    continue;
                }

                LevelDefinition hardened = CreateSandboxCopy(source, $"{orderLabel}_multi_layer_outer_exit", stamp, MultiLayerOuterExitLevelFolder);
                int ops = ApplyMultiLayerOuterExitHardeningV11(hardened, rules, out string opsText);
                EditorUtility.SetDirty(hardened);
                Row hardenedRow = AnalyzeLevel(new PackSource("multi_layer_outer_exit_v11", "", false), r + 1, order, hardened, rules, null, null);
                PeelOuterStatsV11 hardenedPeel = AnalyzePeelOuterStatsV11(hardened, rules, maxWaves: 4, maxRemovedChains: 96);

                int peelDrop = baselinePeel.TotalOuter - hardenedPeel.TotalOuter;
                int futureDrop = baselinePeel.FutureOuter - hardenedPeel.FutureOuter;
                int riskDrop = baselinePeel.RiskScore - hardenedPeel.RiskScore;
                bool meaningful =
                    hardenedRow.GreedySolved &&
                    ops >= 1 &&
                    hardenedRow.OpeningChoices >= 2 &&
                    hardenedRow.AvgChoices <= baseline.AvgChoices + 0.9f &&
                    hardenedRow.MaxChoices <= baseline.MaxChoices + 6 &&
                    hardenedRow.DirectClearableOuterExits <= baseline.DirectClearableOuterExits + 1 &&
                    hardenedRow.DirectOuterExits <= baseline.DirectOuterExits + 1 &&
                    (
                        futureDrop >= 2 ||
                        peelDrop >= 3 ||
                        riskDrop >= 450 ||
                        hardenedPeel.FutureOuter <= Mathf.Max(4, baselinePeel.FutureOuter - 3)
                    );

                Debug.Log(
                    $"[CampaignHardeningAnalyzer] V11 scan {orderLabel}: meaningful={meaningful}, ops={ops}, solved={hardenedRow.GreedySolved}, chains {baseline.Chains}->{hardenedRow.Chains}, tiles {baseline.ArrowTiles}->{hardenedRow.ArrowTiles}, open {baseline.OpeningChoices}->{hardenedRow.OpeningChoices}, clearOuter {baseline.DirectClearableOuterExits}->{hardenedRow.DirectClearableOuterExits}, directOuter {baseline.DirectOuterExits}->{hardenedRow.DirectOuterExits}, peelOuter {baselinePeel.TotalOuter}->{hardenedPeel.TotalOuter}, futurePeelOuter {baselinePeel.FutureOuter}->{hardenedPeel.FutureOuter}, peelRisk {baselinePeel.RiskScore}->{hardenedPeel.RiskScore}, avg {F(baseline.AvgChoices)}->{F(hardenedRow.AvgChoices)}, leak {baseline.LeakScore}->{hardenedRow.LeakScore}");

                if (!meaningful)
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(hardened));
                    report.Add(SandboxReportRow.FromWithPeel(order, "skipped_v11", sourcePath, "", baseline, hardenedRow, ops, $"not_multilayer_enough:{opsText}", baselinePeel, hardenedPeel));
                    continue;
                }

                LevelDefinition before = CreateSandboxCopy(source, $"{orderLabel}_v10_before_multilayer", stamp, MultiLayerOuterExitLevelFolder);
                Row beforeRow = AnalyzeLevel(new PackSource("multi_layer_v10_before", "", false), packLevels.Count + 1, order, before, rules, null, null);
                PeelOuterStatsV11 beforePeel = AnalyzePeelOuterStatsV11(before, rules, maxWaves: 4, maxRemovedChains: 96);
                packLevels.Add(before);
                report.Add(SandboxReportRow.FromWithPeel(order, "v10_before_multilayer", sourcePath, AssetDatabase.GetAssetPath(before), baseline, beforeRow, 0, $"copy:{beforePeel.Summary}", baselinePeel, beforePeel));

                packLevels.Add(hardened);
                report.Add(SandboxReportRow.FromWithPeel(order, "multi_layer_outer_exit_v11", sourcePath, AssetDatabase.GetAssetPath(hardened), baseline, hardenedRow, ops, opsText, baselinePeel, hardenedPeel));
                acceptedPairs++;
            }

            SavePackAt(MultiLayerOuterExitPackPath, "campaign500_hardening_sandbox_v11", $"Campaign 500 Hardening Sandbox V11 ({packLevels.Count})", packLevels);

            string reportPath = $"{ReportFolder}/campaign_hardening_sandbox_v11_{stamp}.csv";
            WriteSandboxReport(reportPath, report);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(MultiLayerOuterExitPackPath);
            AttachPackToDemo(pack, "CampaignHardeningAnalyzer V11");

            Debug.Log($"[CampaignHardeningAnalyzer] Sandbox V11 done from V10. scanned={scanned}, pairs={acceptedPairs}, levels={packLevels.Count}, report={reportPath}, pack={MultiLayerOuterExitPackPath}, sourceReport={latestV10}");
        }

        static void BuildPbeNeeClassificationReportFromLatestV11()
        {
            string latestV11 = FindLatestReport("campaign_hardening_sandbox_v11_*.csv");
            if (string.IsNullOrEmpty(latestV11))
            {
                Debug.LogWarning("[CampaignHardeningAnalyzer] No V11 sandbox report found. Run V11 before V12 classification.");
                return;
            }

            var records = ReadCsvRows(latestV11)
                .Where(r => Get(r, "variant") == "v10_before_multilayer" || Get(r, "variant") == "multi_layer_outer_exit_v11")
                .Where(r => !string.IsNullOrEmpty(Get(r, "assetPath")))
                .ToList();

            if (records.Count == 0)
            {
                Debug.LogWarning($"[CampaignHardeningAnalyzer] No V11 before/after rows found in {latestV11}.");
                return;
            }

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var rows = new List<PeelLeakClassificationRowV12>();
            for (int i = 0; i < records.Count; i++)
            {
                Dictionary<string, string> record = records[i];
                string assetPath = Get(record, "assetPath");
                var level = AssetDatabase.LoadAssetAtPath<LevelDefinition>(assetPath);
                if (level == null)
                {
                    rows.Add(PeelLeakClassificationRowV12.Missing(ParseInt(Get(record, "campaignOrder")), Get(record, "variant"), assetPath));
                    continue;
                }

                int order = ParseInt(Get(record, "campaignOrder"));
                Row analysis = AnalyzeLevel(new PackSource("pbe_nee_v12", "", false), i + 1, order, level, rules, null, null);
                PeelOuterStatsV11 peel = AnalyzePeelOuterStatsV11(level, rules, maxWaves: 4, maxRemovedChains: 96);
                rows.Add(PeelLeakClassificationRowV12.From(order, Get(record, "variant"), assetPath, analysis, peel));
            }

            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", Inv);
            string reportPath = $"{ReportFolder}/campaign_hardening_v12_pbe_nee_{stamp}.csv";
            WritePeelLeakClassificationReportV12(reportPath, rows);

            foreach (var group in rows.Where(r => r.Status != "Missing").GroupBy(r => r.Variant))
            {
                int count = group.Count();
                float direct = count > 0 ? (float)group.Average(r => r.DirectOuter) : 0f;
                float peel = count > 0 ? (float)group.Average(r => r.PeelOuter) : 0f;
                float future = count > 0 ? (float)group.Average(r => r.FutureOuter) : 0f;
                float pbe = count > 0 ? (float)group.Average(r => r.PbeOuter) : 0f;
                float pbeFuture = count > 0 ? (float)group.Average(r => r.PbeFutureOuter) : 0f;
                float nee = count > 0 ? (float)group.Average(r => r.NeeOuter) : 0f;
                Debug.Log($"[CampaignHardeningAnalyzer] V12 classify {group.Key}: n={count}, directOuter avg={F(direct)}, peelOuter avg={F(peel)}, futureOuter avg={F(future)}, PBE avg={F(pbe)}, futurePBE avg={F(pbeFuture)}, NEE avg={F(nee)}");
            }

            Debug.Log($"[CampaignHardeningAnalyzer] V12 PBE/NEE classification done. rows={rows.Count}, report={reportPath}, sourceReport={latestV11}");
        }

        static void BuildBoundaryRewireSandboxFromLatestV11(int targetPairs, int scanLimit)
        {
            string latestV11 = FindLatestReport("campaign_hardening_sandbox_v11_*.csv");
            if (string.IsNullOrEmpty(latestV11))
            {
                Debug.LogWarning("[CampaignHardeningAnalyzer] No V11 sandbox report found. Run V11 before V12 BDR.");
                return;
            }

            var records = ReadCsvRows(latestV11)
                .Where(r => Get(r, "variant") == "multi_layer_outer_exit_v11")
                .Where(r => Get(r, "greedySolved").Equals("True", StringComparison.OrdinalIgnoreCase))
                .Take(Mathf.Max(targetPairs, scanLimit))
                .ToList();

            if (records.Count == 0)
            {
                Debug.LogWarning($"[CampaignHardeningAnalyzer] No V11 output rows found in {latestV11}.");
                return;
            }

            if (AssetDatabase.IsValidFolder(BoundaryRewireLevelFolder))
                AssetDatabase.DeleteAsset(BoundaryRewireLevelFolder);
            Directory.CreateDirectory(ToAbsolutePath(BoundaryRewireLevelFolder));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var report = new List<SandboxReportRow>();
            var packLevels = new List<LevelDefinition>(targetPairs * 2);
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", Inv);
            int acceptedPairs = 0;
            int scanned = 0;

            for (int r = 0; r < records.Count && acceptedPairs < targetPairs; r++)
            {
                scanned++;
                Dictionary<string, string> record = records[r];
                string sourcePath = Get(record, "assetPath");
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source == null)
                {
                    Debug.LogWarning($"[CampaignHardeningAnalyzer] Missing V12 BDR V11-source: {sourcePath}");
                    continue;
                }

                int order = ParseInt(Get(record, "campaignOrder"));
                string orderLabel = order > 0 ? $"L{order:000}" : $"I{r + 1:000}";
                Row baseline = AnalyzeLevel(new PackSource("boundary_rewire_v11_source", "", false), r + 1, order, source, rules, null, null);
                PeelOuterStatsV11 baselinePeel = AnalyzePeelOuterStatsV11(source, rules, maxWaves: 4, maxRemovedChains: 96);
                if (baseline.DirectOuterExits < 4 || baselinePeel.PersistentOuter < 4)
                {
                    report.Add(SandboxReportRow.FromWithPeel(order, "skipped_v12_bdr", sourcePath, "", baseline, baseline, 0, $"skip_low_pbe:{baselinePeel.Summary}", baselinePeel, baselinePeel));
                    continue;
                }

                LevelDefinition hardened = CreateSandboxCopy(source, $"{orderLabel}_boundary_rewire_v12", stamp, BoundaryRewireLevelFolder);
                int ops = ApplyBoundaryDirectOuterRewireV12(hardened, rules, out string opsText);
                EditorUtility.SetDirty(hardened);
                Row hardenedRow = AnalyzeLevel(new PackSource("boundary_rewire_v12", "", false), r + 1, order, hardened, rules, null, null);
                PeelOuterStatsV11 hardenedPeel = AnalyzePeelOuterStatsV11(hardened, rules, maxWaves: 4, maxRemovedChains: 96);

                int directDrop = baseline.DirectOuterExits - hardenedRow.DirectOuterExits;
                int pbeDrop = baselinePeel.PersistentOuter - hardenedPeel.PersistentOuter;
                int neeIncrease = hardenedPeel.NewlyExposedOuter - baselinePeel.NewlyExposedOuter;
                bool meaningful =
                    hardenedRow.GreedySolved &&
                    ops >= 1 &&
                    hardenedRow.OpeningChoices >= 2 &&
                    directDrop >= 1 &&
                    pbeDrop >= 1 &&
                    (
                        directDrop >= 2 ||
                        pbeDrop >= 2 ||
                        hardenedRow.DirectOuterExits <= 8 ||
                        hardenedRow.AvgChoices <= baseline.AvgChoices - 0.25f
                    ) &&
                    neeIncrease <= Mathf.Max(2, Mathf.CeilToInt(baselinePeel.NewlyExposedOuter * 0.08f)) &&
                    hardenedRow.AvgChoices <= baseline.AvgChoices + 0.8f &&
                    hardenedRow.MaxChoices <= baseline.MaxChoices + 5 &&
                    hardenedRow.ArrowTiles <= baseline.ArrowTiles + Mathf.Max(8, Mathf.CeilToInt(baseline.ArrowTiles * 0.02f));

                Debug.Log(
                    $"[CampaignHardeningAnalyzer] V12 BDR scan {orderLabel}: meaningful={meaningful}, ops={ops}, solved={hardenedRow.GreedySolved}, chains {baseline.Chains}->{hardenedRow.Chains}, tiles {baseline.ArrowTiles}->{hardenedRow.ArrowTiles}, open {baseline.OpeningChoices}->{hardenedRow.OpeningChoices}, directOuter {baseline.DirectOuterExits}->{hardenedRow.DirectOuterExits}, PBE {baselinePeel.PersistentOuter}->{hardenedPeel.PersistentOuter}, NEE {baselinePeel.NewlyExposedOuter}->{hardenedPeel.NewlyExposedOuter}, future {baselinePeel.FutureOuter}->{hardenedPeel.FutureOuter}, avg {F(baseline.AvgChoices)}->{F(hardenedRow.AvgChoices)}, leak {baseline.LeakScore}->{hardenedRow.LeakScore}");

                if (!meaningful)
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(hardened));
                    report.Add(SandboxReportRow.FromWithPeel(order, "skipped_v12_bdr", sourcePath, "", baseline, hardenedRow, ops, $"not_bdr_enough:{opsText}", baselinePeel, hardenedPeel));
                    continue;
                }

                LevelDefinition before = CreateSandboxCopy(source, $"{orderLabel}_v11_before_bdr", stamp, BoundaryRewireLevelFolder);
                Row beforeRow = AnalyzeLevel(new PackSource("boundary_rewire_v11_before", "", false), packLevels.Count + 1, order, before, rules, null, null);
                PeelOuterStatsV11 beforePeel = AnalyzePeelOuterStatsV11(before, rules, maxWaves: 4, maxRemovedChains: 96);
                packLevels.Add(before);
                report.Add(SandboxReportRow.FromWithPeel(order, "v11_before_bdr", sourcePath, AssetDatabase.GetAssetPath(before), baseline, beforeRow, 0, $"copy:{beforePeel.Summary}", baselinePeel, beforePeel));

                packLevels.Add(hardened);
                report.Add(SandboxReportRow.FromWithPeel(order, "boundary_rewire_v12", sourcePath, AssetDatabase.GetAssetPath(hardened), baseline, hardenedRow, ops, opsText, baselinePeel, hardenedPeel));
                acceptedPairs++;
            }

            SavePackAt(BoundaryRewirePackPath, "campaign500_hardening_sandbox_v12_bdr", $"Campaign 500 Hardening Sandbox V12 BDR ({packLevels.Count})", packLevels);

            string reportPath = $"{ReportFolder}/campaign_hardening_sandbox_v12_bdr_{stamp}.csv";
            WriteSandboxReport(reportPath, report);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(BoundaryRewirePackPath);
            AttachPackToDemo(pack, "CampaignHardeningAnalyzer V12 BDR");

            Debug.Log($"[CampaignHardeningAnalyzer] Sandbox V12 BDR done from V11. scanned={scanned}, pairs={acceptedPairs}, levels={packLevels.Count}, report={reportPath}, pack={BoundaryRewirePackPath}, sourceReport={latestV11}");
        }

        static void BuildBoundaryInsetSandboxFromLatestV11(int targetPairs, int scanLimit)
        {
            string latestV11 = FindLatestReport("campaign_hardening_sandbox_v11_*.csv");
            if (string.IsNullOrEmpty(latestV11))
            {
                Debug.LogWarning("[CampaignHardeningAnalyzer] No V11 sandbox report found. Run V11 before V13 inset.");
                return;
            }

            var records = ReadCsvRows(latestV11)
                .Where(r => Get(r, "variant") == "multi_layer_outer_exit_v11")
                .Where(r => Get(r, "greedySolved").Equals("True", StringComparison.OrdinalIgnoreCase))
                .Take(Mathf.Max(targetPairs, scanLimit))
                .ToList();

            if (records.Count == 0)
            {
                Debug.LogWarning($"[CampaignHardeningAnalyzer] No V11 output rows found in {latestV11}.");
                return;
            }

            if (AssetDatabase.IsValidFolder(BoundaryInsetLevelFolder))
                AssetDatabase.DeleteAsset(BoundaryInsetLevelFolder);
            Directory.CreateDirectory(ToAbsolutePath(BoundaryInsetLevelFolder));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var report = new List<SandboxReportRow>();
            var packLevels = new List<LevelDefinition>(targetPairs * 2);
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", Inv);
            int acceptedPairs = 0;
            int scanned = 0;

            for (int r = 0; r < records.Count && acceptedPairs < targetPairs; r++)
            {
                scanned++;
                Dictionary<string, string> record = records[r];
                string sourcePath = Get(record, "assetPath");
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source == null)
                {
                    Debug.LogWarning($"[CampaignHardeningAnalyzer] Missing V13 inset V11-source: {sourcePath}");
                    continue;
                }

                int order = ParseInt(Get(record, "campaignOrder"));
                string orderLabel = order > 0 ? $"L{order:000}" : $"I{r + 1:000}";
                Row baseline = AnalyzeLevel(new PackSource("boundary_inset_v11_source", "", false), r + 1, order, source, rules, null, null);
                PeelOuterStatsV11 baselinePeel = AnalyzePeelOuterStatsV11(source, rules, maxWaves: 4, maxRemovedChains: 96);
                if (baseline.DirectOuterExits < 4 || baselinePeel.PersistentOuter < 4)
                {
                    report.Add(SandboxReportRow.FromWithPeel(order, "skipped_v13_bdr2", sourcePath, "", baseline, baseline, 0, $"skip_low_pbe:{baselinePeel.Summary}", baselinePeel, baselinePeel));
                    continue;
                }

                LevelDefinition hardened = CreateSandboxCopy(source, $"{orderLabel}_boundary_inset_v13", stamp, BoundaryInsetLevelFolder);
                int ops = ApplyBoundaryDirectOuterInsetV13(hardened, rules, out string opsText);
                EditorUtility.SetDirty(hardened);
                Row hardenedRow = AnalyzeLevel(new PackSource("boundary_inset_v13", "", false), r + 1, order, hardened, rules, null, null);
                PeelOuterStatsV11 hardenedPeel = AnalyzePeelOuterStatsV11(hardened, rules, maxWaves: 4, maxRemovedChains: 96);

                int directDrop = baseline.DirectOuterExits - hardenedRow.DirectOuterExits;
                int pbeDrop = baselinePeel.PersistentOuter - hardenedPeel.PersistentOuter;
                int neeIncrease = hardenedPeel.NewlyExposedOuter - baselinePeel.NewlyExposedOuter;
                int requiredPbeDrop = Mathf.Max(1, Mathf.CeilToInt(baselinePeel.PersistentOuter * 0.15f));
                int requiredDirectDrop = Mathf.Max(1, Mathf.CeilToInt(baseline.DirectOuterExits * 0.12f));
                bool meaningful =
                    hardenedRow.GreedySolved &&
                    ops >= 1 &&
                    hardenedRow.OpeningChoices >= 2 &&
                    directDrop >= requiredDirectDrop &&
                    pbeDrop >= requiredPbeDrop &&
                    neeIncrease <= Mathf.Max(1, Mathf.CeilToInt(baselinePeel.NewlyExposedOuter * 0.03f)) &&
                    hardenedRow.OpeningChoices <= baseline.OpeningChoices + 1 &&
                    hardenedRow.AvgChoices <= baseline.AvgChoices + 0.55f &&
                    hardenedRow.MaxChoices <= baseline.MaxChoices + 4 &&
                    hardenedRow.ArrowTiles <= baseline.ArrowTiles + Mathf.Max(12, Mathf.CeilToInt(baseline.ArrowTiles * 0.025f));

                Debug.Log(
                    $"[CampaignHardeningAnalyzer] V13 BDR2 scan {orderLabel}: meaningful={meaningful}, ops={ops}, solved={hardenedRow.GreedySolved}, chains {baseline.Chains}->{hardenedRow.Chains}, tiles {baseline.ArrowTiles}->{hardenedRow.ArrowTiles}, open {baseline.OpeningChoices}->{hardenedRow.OpeningChoices}, directOuter {baseline.DirectOuterExits}->{hardenedRow.DirectOuterExits}, PBE {baselinePeel.PersistentOuter}->{hardenedPeel.PersistentOuter}, NEE {baselinePeel.NewlyExposedOuter}->{hardenedPeel.NewlyExposedOuter}, future {baselinePeel.FutureOuter}->{hardenedPeel.FutureOuter}, avg {F(baseline.AvgChoices)}->{F(hardenedRow.AvgChoices)}, requiredDrop direct/PBE={requiredDirectDrop}/{requiredPbeDrop}, leak {baseline.LeakScore}->{hardenedRow.LeakScore}");

                if (!meaningful)
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(hardened));
                    report.Add(SandboxReportRow.FromWithPeel(order, "skipped_v13_bdr2", sourcePath, "", baseline, hardenedRow, ops, $"not_bdr2_enough:{opsText}", baselinePeel, hardenedPeel));
                    continue;
                }

                LevelDefinition before = CreateSandboxCopy(source, $"{orderLabel}_v11_before_inset", stamp, BoundaryInsetLevelFolder);
                Row beforeRow = AnalyzeLevel(new PackSource("boundary_inset_v11_before", "", false), packLevels.Count + 1, order, before, rules, null, null);
                PeelOuterStatsV11 beforePeel = AnalyzePeelOuterStatsV11(before, rules, maxWaves: 4, maxRemovedChains: 96);
                packLevels.Add(before);
                report.Add(SandboxReportRow.FromWithPeel(order, "v11_before_inset", sourcePath, AssetDatabase.GetAssetPath(before), baseline, beforeRow, 0, $"copy:{beforePeel.Summary}", baselinePeel, beforePeel));

                packLevels.Add(hardened);
                report.Add(SandboxReportRow.FromWithPeel(order, "boundary_inset_v13", sourcePath, AssetDatabase.GetAssetPath(hardened), baseline, hardenedRow, ops, opsText, baselinePeel, hardenedPeel));
                acceptedPairs++;
            }

            SavePackAt(BoundaryInsetPackPath, "campaign500_hardening_sandbox_v13_bdr2", $"Campaign 500 Hardening Sandbox V13 BDR2 ({packLevels.Count})", packLevels);

            string reportPath = $"{ReportFolder}/campaign_hardening_sandbox_v13_bdr2_{stamp}.csv";
            WriteSandboxReport(reportPath, report);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(BoundaryInsetPackPath);
            AttachPackToDemo(pack, "CampaignHardeningAnalyzer V13 BDR2");

            Debug.Log($"[CampaignHardeningAnalyzer] Sandbox V13 BDR2 done from V11. scanned={scanned}, pairs={acceptedPairs}, levels={packLevels.Count}, report={reportPath}, pack={BoundaryInsetPackPath}, sourceReport={latestV11}");
        }

        static void BuildBoundaryCompressionSandboxFromLatestV13(int targetPairs, int scanLimit)
        {
            string latestV13 = FindLatestReport("campaign_hardening_sandbox_v13_bdr2_*.csv");
            if (string.IsNullOrEmpty(latestV13))
            {
                Debug.LogWarning("[CampaignHardeningAnalyzer] No V13 BDR2 sandbox report found. Run V13 before V14 compression.");
                return;
            }

            var records = ReadCsvRows(latestV13)
                .Where(r => Get(r, "variant") == "boundary_inset_v13")
                .Where(r => Get(r, "greedySolved").Equals("True", StringComparison.OrdinalIgnoreCase))
                .Take(Mathf.Max(targetPairs, scanLimit))
                .ToList();

            if (records.Count == 0)
            {
                Debug.LogWarning($"[CampaignHardeningAnalyzer] No V13 accepted output rows found in {latestV13}.");
                return;
            }

            if (AssetDatabase.IsValidFolder(BoundaryCompressionLevelFolder))
                AssetDatabase.DeleteAsset(BoundaryCompressionLevelFolder);
            Directory.CreateDirectory(ToAbsolutePath(BoundaryCompressionLevelFolder));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var report = new List<SandboxReportRow>();
            var packLevels = new List<LevelDefinition>(targetPairs * 2);
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", Inv);
            int acceptedPairs = 0;
            int scanned = 0;

            for (int r = 0; r < records.Count && acceptedPairs < targetPairs; r++)
            {
                scanned++;
                Dictionary<string, string> record = records[r];
                string sourcePath = Get(record, "assetPath");
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source == null)
                {
                    Debug.LogWarning($"[CampaignHardeningAnalyzer] Missing V14 compression V13-source: {sourcePath}");
                    continue;
                }

                int order = ParseInt(Get(record, "campaignOrder"));
                string orderLabel = order > 0 ? $"L{order:000}" : $"I{r + 1:000}";
                Row baseline = AnalyzeLevel(new PackSource("boundary_compress_v13_source", "", false), r + 1, order, source, rules, null, null);
                PeelOuterStatsV11 baselinePeel = AnalyzePeelOuterStatsV11(source, rules, maxWaves: 4, maxRemovedChains: 96);
                if (baseline.DirectOuterExits < 4 || baselinePeel.PersistentOuter < 4)
                {
                    report.Add(SandboxReportRow.FromWithPeel(order, "skipped_v14_cmp", sourcePath, "", baseline, baseline, 0, $"skip_low_pbe:{baselinePeel.Summary}", baselinePeel, baselinePeel));
                    continue;
                }

                LevelDefinition hardened = CreateSandboxCopy(source, $"{orderLabel}_boundary_compress_v14", stamp, BoundaryCompressionLevelFolder);
                int ops = ApplyBoundaryEndpointCompressionV14(hardened, rules, out string opsText);
                EditorUtility.SetDirty(hardened);
                Row hardenedRow = AnalyzeLevel(new PackSource("boundary_compress_v14", "", false), r + 1, order, hardened, rules, null, null);
                PeelOuterStatsV11 hardenedPeel = AnalyzePeelOuterStatsV11(hardened, rules, maxWaves: 4, maxRemovedChains: 96);

                int directDrop = baseline.DirectOuterExits - hardenedRow.DirectOuterExits;
                int pbeDrop = baselinePeel.PersistentOuter - hardenedPeel.PersistentOuter;
                int neeIncrease = hardenedPeel.NewlyExposedOuter - baselinePeel.NewlyExposedOuter;
                int requiredPbeDrop = Mathf.Max(1, Mathf.CeilToInt(baselinePeel.PersistentOuter * 0.08f));
                int requiredDirectDrop = Mathf.Max(1, Mathf.CeilToInt(baseline.DirectOuterExits * 0.08f));
                bool meaningful =
                    hardenedRow.GreedySolved &&
                    ops >= 1 &&
                    hardenedRow.Chains <= baseline.Chains - 1 &&
                    hardenedRow.ArrowTiles == baseline.ArrowTiles &&
                    hardenedRow.OpeningChoices >= 2 &&
                    hardenedRow.OpeningChoices <= baseline.OpeningChoices &&
                    directDrop >= requiredDirectDrop &&
                    pbeDrop >= requiredPbeDrop &&
                    neeIncrease <= Mathf.Max(1, Mathf.CeilToInt(baselinePeel.NewlyExposedOuter * 0.02f)) &&
                    hardenedRow.AvgChoices <= baseline.AvgChoices + 0.15f &&
                    hardenedRow.MaxChoices <= baseline.MaxChoices + 2 &&
                    hardenedRow.EdgeShortDirectOuterExits <= baseline.EdgeShortDirectOuterExits &&
                    hardenedRow.BoundaryStraightOuterExits <= baseline.BoundaryStraightOuterExits;

                Debug.Log(
                    $"[CampaignHardeningAnalyzer] V14 CMP scan {orderLabel}: meaningful={meaningful}, ops={ops}, solved={hardenedRow.GreedySolved}, chains {baseline.Chains}->{hardenedRow.Chains}, tiles {baseline.ArrowTiles}->{hardenedRow.ArrowTiles}, open {baseline.OpeningChoices}->{hardenedRow.OpeningChoices}, directOuter {baseline.DirectOuterExits}->{hardenedRow.DirectOuterExits}, PBE {baselinePeel.PersistentOuter}->{hardenedPeel.PersistentOuter}, NEE {baselinePeel.NewlyExposedOuter}->{hardenedPeel.NewlyExposedOuter}, future {baselinePeel.FutureOuter}->{hardenedPeel.FutureOuter}, avg {F(baseline.AvgChoices)}->{F(hardenedRow.AvgChoices)}, edgeShortOuter {baseline.EdgeShortDirectOuterExits}->{hardenedRow.EdgeShortDirectOuterExits}, boundaryStraight {baseline.BoundaryStraightOuterExits}->{hardenedRow.BoundaryStraightOuterExits}, requiredDrop direct/PBE={requiredDirectDrop}/{requiredPbeDrop}, leak {baseline.LeakScore}->{hardenedRow.LeakScore}");

                if (!meaningful)
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(hardened));
                    report.Add(SandboxReportRow.FromWithPeel(order, "skipped_v14_cmp", sourcePath, "", baseline, hardenedRow, ops, $"not_cmp_enough:{opsText}", baselinePeel, hardenedPeel));
                    continue;
                }

                LevelDefinition before = CreateSandboxCopy(source, $"{orderLabel}_v13_before_compress", stamp, BoundaryCompressionLevelFolder);
                Row beforeRow = AnalyzeLevel(new PackSource("boundary_compress_v13_before", "", false), packLevels.Count + 1, order, before, rules, null, null);
                PeelOuterStatsV11 beforePeel = AnalyzePeelOuterStatsV11(before, rules, maxWaves: 4, maxRemovedChains: 96);
                packLevels.Add(before);
                report.Add(SandboxReportRow.FromWithPeel(order, "v13_before_compress", sourcePath, AssetDatabase.GetAssetPath(before), baseline, beforeRow, 0, $"copy:{beforePeel.Summary}", baselinePeel, beforePeel));

                packLevels.Add(hardened);
                report.Add(SandboxReportRow.FromWithPeel(order, "boundary_compress_v14", sourcePath, AssetDatabase.GetAssetPath(hardened), baseline, hardenedRow, ops, opsText, baselinePeel, hardenedPeel));
                acceptedPairs++;
            }

            SavePackAt(BoundaryCompressionPackPath, "campaign500_hardening_sandbox_v14_cmp", $"Campaign 500 Hardening Sandbox V14 CMP ({packLevels.Count})", packLevels);

            string reportPath = $"{ReportFolder}/campaign_hardening_sandbox_v14_cmp_{stamp}.csv";
            WriteSandboxReport(reportPath, report);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(BoundaryCompressionPackPath);
            AttachPackToDemo(pack, "CampaignHardeningAnalyzer V14 CMP");

            Debug.Log($"[CampaignHardeningAnalyzer] Sandbox V14 CMP done from V13. scanned={scanned}, pairs={acceptedPairs}, levels={packLevels.Count}, report={reportPath}, pack={BoundaryCompressionPackPath}, sourceReport={latestV13}");
        }

        static Row AnalyzeLevel(
            PackSource source,
            int indexInPack,
            int campaignOrder,
            LevelDefinition level,
            IRuleset rules,
            Dictionary<string, string> selectionMeta,
            Dictionary<string, string> rhythmMeta)
        {
            var row = new Row
            {
                PackLabel = source.Label,
                PackPath = source.PackPath,
                IndexInPack = indexInPack,
                CampaignOrder = campaignOrder,
                Level = level,
                LevelId = level != null ? level.levelId : "",
                AssetPath = level != null ? AssetDatabase.GetAssetPath(level) : "",
                Bucket = Get(selectionMeta, "bucket"),
                PlannedType = Get(selectionMeta, "type"),
                RelativeDifficulty = Get(rhythmMeta, "relativeDifficulty"),
                SlotRole = Get(rhythmMeta, "slotRole")
            };

            if (level == null)
            {
                row.Status = "Red";
                row.Flags = "MissingLevel";
                row.Details = "null level reference";
                return row;
            }

            AuthoredLevelData authored = level.authoredLevel;
            row.Width = Mathf.Max(1, authored?.width ?? 0);
            row.Height = Mathf.Max(1, authored?.height ?? 0);
            row.Chains = authored?.arrows?.Count ?? 0;
            row.ArrowTiles = CountArrowTiles(authored);
            row.BlockTiles = authored?.blockIndices?.Count ?? 0;
            row.BoardFill = row.ArrowTiles / Mathf.Max(1f, row.Width * row.Height);
            row.PlayableFill = row.ArrowTiles / Mathf.Max(1f, row.Width * row.Height - row.BlockTiles);
            row.MaxChainLength = MaxChainLength(authored);
            row.AvgChainLength = row.Chains > 0 ? row.ArrowTiles / (float)row.Chains : 0f;

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out string buildError))
            {
                row.Status = "Red";
                row.Flags = "BuildError";
                row.Details = buildError;
                return row;
            }

            BoardGenerationTuning.BoardGenerationStats stats = BoardGenerationTuning.CalculateBoardGenerationStats(board, rules);
            row.InitialClearableChains = stats.InitialMovableArrowChainCount;
            row.AverageStepsToNextUnlock = stats.AverageStepsToNextUnlock;
            row.DependencyDepthProxy = stats.AverageStepsToNextUnlock;
            row.TuningDifficultyScore = stats.DifficultyScore;

            OuterExitStats outer = AnalyzeOuterExits(authored, board, rules);
            row.DirectOuterExits = outer.DirectOuterExits;
            row.DirectClearableOuterExits = outer.DirectClearableOuterExits;
            row.LongRayOuterExits = outer.LongRayOuterExits;
            row.EdgeShortChains = outer.EdgeShortChains;
            row.EdgeShortDirectOuterExits = outer.EdgeShortDirectOuterExits;
            row.BoundaryStraightOuterExits = outer.BoundaryStraightOuterExits;
            row.DirectOuterExitRatio = row.Chains > 0 ? row.DirectOuterExits / (float)row.Chains : 0f;
            row.DirectClearableOuterExitRatio = row.Chains > 0 ? row.DirectClearableOuterExits / (float)row.Chains : 0f;

            TraceSummary trace = Simulate(board, rules);
            row.GreedySolved = trace.Solved;
            row.GreedySteps = trace.StepCount;
            row.OpeningChoices = trace.OpeningChoices;
            row.OpeningGoodMoves = trace.OpeningGoodMoves;
            row.OpeningGoodMoveRatio = trace.OpeningGoodMoveRatio;
            row.AvgChoices = trace.AvgChoices;
            row.MaxChoices = trace.MaxChoices;
            row.EarlyAvgChoices = trace.EarlyAvgChoices;
            row.EarlyMaxChoices = trace.EarlyMaxChoices;
            row.BottleneckRatio = trace.BottleneckRatio;
            row.ForcedMoveRatio = trace.ForcedMoveRatio;
            row.AvgClearPerMove = trace.AvgClearPerMove;
            row.AvgNewUnlocksPerMove = trace.AvgNewUnlocksPerMove;
            row.LargestUnlockBurst = trace.LargestUnlockBurst;
            row.ChoiceWaveStdDev = trace.ChoiceWaveStdDev;
            row.EntropyCurve = trace.EntropyCurve;
            row.EarlyEntropyClass = ClassifyEntropy(trace);

            Score(row);
            return row;
        }

        static OuterExitStats AnalyzeOuterExits(AuthoredLevelData authored, BoardState board, IRuleset rules)
        {
            var stats = new OuterExitStats();
            if (authored?.arrows == null)
                return stats;

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                AuthoredArrowData arrow = authored.arrows[i];
                if (arrow?.indices == null || arrow.indices.Count < 2)
                    continue;

                bool edgeTouch = false;
                foreach (int idx in arrow.indices)
                {
                    int x = idx % board.width;
                    int y = idx / board.width;
                    if (x == 0 || y == 0 || x == board.width - 1 || y == board.height - 1)
                    {
                        edgeTouch = true;
                        break;
                    }
                }

                int length = arrow.indices.Count;
                if (edgeTouch && length <= 3)
                    stats.EdgeShortChains++;

                int head = arrow.indices[0];
                int neck = arrow.indices[1];
                Vector2Int headPos = new Vector2Int(head % board.width, head / board.width);
                Vector2Int neckPos = new Vector2Int(neck % board.width, neck / board.width);
                Vector2Int forward = headPos - neckPos;
                if (!IsCardinal(forward))
                    continue;

                bool directExit = TraceHeadRayToExit(board, headPos, forward, out int emptyRayLength);
                if (!directExit)
                    continue;

                stats.DirectOuterExits++;
                if (emptyRayLength >= 4)
                    stats.LongRayOuterExits++;
                if (edgeTouch && length <= 3)
                    stats.EdgeShortDirectOuterExits++;
                if (IsStraight(arrow, board.width) && edgeTouch && length <= 6)
                    stats.BoundaryStraightOuterExits++;

                bool clearable = false;
                for (int c = 0; c < arrow.indices.Count; c++)
                {
                    int idx = arrow.indices[c];
                    var move = new Move(new Vector2Int(idx % board.width, idx / board.width));
                    if (!rules.TryApplyMove(board, move, out MoveDelta delta))
                        continue;

                    int cleared = CountCleared(delta);
                    delta.Undo(board);
                    if (cleared <= 0)
                        continue;

                    clearable = true;
                    break;
                }

                if (clearable)
                    stats.DirectClearableOuterExits++;
            }

            return stats;
        }

        static bool TraceHeadRayToExit(BoardState board, Vector2Int headPos, Vector2Int forward, out int emptyRayLength)
        {
            emptyRayLength = 0;
            Vector2Int pos = headPos + forward;

            int cap = 1 + board.width + board.height;
            for (int i = 0; i < cap; i++)
            {
                if (!board.InBounds(pos.x, pos.y))
                    return true;

                TileState tile = board.tiles[board.Index(pos.x, pos.y)];
                if (tile.type != TileType.Empty)
                    return false;

                emptyRayLength++;
                pos += forward;
            }

            return false;
        }

        static TraceSummary Simulate(BoardState start, IRuleset rules)
        {
            BoardState state = CloneBoard(start);
            var steps = new List<TraceStep>(512);
            int maxSteps = Mathf.Max(512, state.width * state.height * 4);
            bool solved = false;
            List<MoveCandidate> candidates = null;

            for (int stepIndex = 0; stepIndex < maxSteps; stepIndex++)
            {
                if (rules.IsSolved(state))
                {
                    solved = true;
                    break;
                }

                candidates ??= BuildMoveCandidates(state, rules);
                if (candidates.Count == 0)
                    break;

                int bestClear = candidates.Max(c => c.ClearCount);
                float goodClearThreshold = Mathf.Max(2f, bestClear * 0.65f);
                int goodMoves = 0;
                for (int i = 0; i < candidates.Count; i++)
                {
                    bool oneClearTrap = bestClear <= 1;
                    if (!oneClearTrap && candidates[i].ClearCount >= goodClearThreshold)
                        goodMoves++;
                }

                MoveCandidate chosen = ChooseBalanced(candidates);
                float avgClear = candidates.Count > 0 ? (float)candidates.Average(c => c.ClearCount) : 0f;
                int available = candidates.Count;
                float goodRatio = available > 0 ? goodMoves / (float)available : 0f;

                if (!rules.TryApplyMove(state, chosen.Move, out _))
                    break;

                candidates = BuildMoveCandidates(state, rules);
                int newlyClearable = Mathf.Max(0, candidates.Count - Mathf.Max(0, available - 1));

                steps.Add(new TraceStep
                {
                    AvailableChoices = available,
                    GoodMoves = goodMoves,
                    GoodMoveRatio = goodRatio,
                    BestClearCount = bestClear,
                    ChosenClearCount = chosen.ClearCount,
                    AvgClearCount = avgClear,
                    NewlyClearableChains = newlyClearable
                });
            }

            if (!solved && rules.IsSolved(state))
                solved = true;

            return TraceSummary.FromSteps(steps, solved);
        }

        static MoveCandidate ChooseBalanced(List<MoveCandidate> candidates)
        {
            return candidates
                .OrderByDescending(c => c.MoveScore)
                .ThenByDescending(c => c.ClearCount)
                .ThenBy(c => c.ChainId)
                .First();
        }

        static List<MoveCandidate> BuildMoveCandidates(BoardState state, IRuleset rules)
        {
            var byChain = new Dictionary<int, MoveCandidate>();
            foreach (Move move in rules.GetLegalMoves(state))
            {
                if (!rules.TryApplyMove(state, move, out MoveDelta delta))
                    continue;

                int cleared = CountCleared(delta);
                delta.Undo(state);
                if (cleared <= 0)
                    continue;

                int chainId = GetChainId(state, move.pos, out int chainLength);
                if (chainId < 0)
                    continue;

                var candidate = new MoveCandidate
                {
                    Move = move,
                    ChainId = chainId,
                    ChainLength = chainLength,
                    ClearCount = cleared,
                    MoveScore = cleared + chainLength * 0.2f
                };

                if (!byChain.TryGetValue(chainId, out MoveCandidate existing) ||
                    candidate.ClearCount > existing.ClearCount)
                {
                    byChain[chainId] = candidate;
                }
            }

            return byChain.Values.ToList();
        }

        static string ClassifyEntropy(TraceSummary trace)
        {
            if (!trace.Solved)
                return "unsolved";
            if (trace.OpeningChoices >= 10 || trace.EarlyAvgChoices >= 9f)
                return trace.AvgNewUnlocksPerMove >= 1.4f ? "front_loaded_burst" : "front_loaded_flat";
            if (trace.OpeningChoices <= 2 && trace.ForcedMoveRatio >= 0.35f)
                return "too_narrow";
            if (trace.BottleneckRatio >= 0.35f && trace.LargestUnlockBurst >= 5)
                return "gated_release";
            if (trace.AvgChoices >= 7f)
                return "wide_open";
            return "controlled";
        }

        static void Score(Row row)
        {
            var flags = new List<string>();
            float score = 0f;

            if (!row.GreedySolved)
            {
                flags.Add("GreedyFail");
                row.LeakScore = 9999;
                row.HardeningBudget = 0;
                row.RecommendedOperators = "reject_or_repair_first";
                row.Status = "Red";
                row.Flags = string.Join("|", flags);
                return;
            }

            bool boundaryLeak =
                row.DirectClearableOuterExits >= 6 ||
                row.DirectClearableOuterExitRatio >= 0.16f ||
                row.DirectOuterExits >= 10 ||
                row.DirectOuterExitRatio >= 0.22f ||
                row.EdgeShortDirectOuterExits >= 4 ||
                row.EdgeShortChains >= 8 ||
                row.BoundaryStraightOuterExits >= 3;

            if (boundaryLeak)
            {
                flags.Add("BoundaryLeak");
                float boundaryScore =
                    row.DirectClearableOuterExits * 8f +
                    row.DirectOuterExitRatio * 80f +
                    row.EdgeShortDirectOuterExits * 6f +
                    row.EdgeShortChains * 1.5f +
                    row.BoundaryStraightOuterExits * 10f;
                score += Mathf.Min(boundaryScore, 220f);
            }

            if (row.DirectClearableOuterExits >= 6 || row.DirectClearableOuterExitRatio >= 0.16f)
                flags.Add("DirectEscapeLeak");

            if (row.DirectOuterExits >= 10 || row.DirectOuterExitRatio >= 0.22f)
                flags.Add("OuterExitHeavy");

            if (row.EdgeShortDirectOuterExits >= 4 || row.EdgeShortChains >= 8)
                flags.Add("EdgeShortLeak");

            if (row.BoundaryStraightOuterExits >= 3)
                flags.Add("BoundaryStraightLeak");

            if (row.OpeningChoices >= 8 || row.EarlyAvgChoices >= 7.5f || row.EarlyMaxChoices >= 12)
            {
                flags.Add("ChoiceExplosion");
                score += row.OpeningChoices * 3.5f + row.EarlyAvgChoices * 5f + row.EarlyMaxChoices * 2f;
            }

            if (row.AvgChoices >= 7.5f && row.DependencyDepthProxy <= 2f)
            {
                flags.Add("WeakDependency");
                score += (row.AvgChoices - 5f) * 10f + Mathf.Max(0f, 2f - row.DependencyDepthProxy) * 12f;
            }

            if (row.OpeningGoodMoveRatio >= 0.55f && row.OpeningChoices >= 6)
            {
                flags.Add("TooManyGoodOpeners");
                score += row.OpeningGoodMoveRatio * 45f;
            }

            score -= row.BottleneckRatio * 20f;
            score -= row.ForcedMoveRatio * 15f;
            score = Mathf.Max(0f, score);

            row.LeakScore = Mathf.RoundToInt(score);
            row.Flags = flags.Count > 0 ? string.Join("|", flags.Distinct()) : "None";
            row.Status = row.LeakScore >= 820 ? "CriticalLeak" :
                row.LeakScore >= 650 ? "HighLeak" :
                row.LeakScore >= 450 ? "MediumLeak" :
                row.LeakScore >= 250 ? "LowLeak" :
                "Ok";
            row.HardeningBudget = row.LeakScore >= 820 ? 6 :
                row.LeakScore >= 650 ? 5 :
                row.LeakScore >= 450 ? 4 :
                row.LeakScore >= 250 ? 3 :
                0;
            row.RecommendedOperators = RecommendOperators(row);
        }

        static string RecommendOperators(Row row)
        {
            var ops = new List<string>();
            if (row.DirectClearableOuterExits >= 4 || row.DirectClearableOuterExitRatio >= 0.12f)
                ops.Add("redirect_outer_heads");
            if (row.EdgeShortDirectOuterExits >= 3 || row.EdgeShortChains >= 8)
                ops.Add("edge_merge_short_chains");
            if (row.BoundaryStraightOuterExits >= 3)
                ops.Add("wrap_boundary_straights");
            if (row.OpeningChoices >= 8 || row.EarlyAvgChoices >= 7.5f)
                ops.Add("gate_opening_choices");
            if (row.AvgChoices >= 7.5f && row.DependencyDepthProxy <= 2f)
                ops.Add("inject_region_gate");
            return ops.Count > 0 ? string.Join("|", ops) : "none";
        }

        static void WriteRows(string assetPath, IReadOnlyList<Row> rows)
        {
            var lines = new List<string>(rows.Count + 1) { string.Join(",", Headers) };
            foreach (Row row in rows)
                lines.Add(string.Join(",", Headers.Select(h => EscapeCsv(GetValue(row, h)))));
            File.WriteAllLines(ToAbsolutePath(assetPath), lines, new UTF8Encoding(false));
        }

        static void WriteNotes(string assetPath, IReadOnlyList<Row> rows, IReadOnlyList<Row> ranked, IReadOnlyList<PackSource> sources, string runLabel)
        {
            var sb = new StringBuilder();
            sb.AppendLine("# Campaign Hardening Analyzer");
            sb.AppendLine();
            sb.AppendLine($"Run: `{runLabel}`");
            sb.AppendLine($"Rows: {rows.Count}");
            sb.AppendLine();
            sb.AppendLine("## Sources");
            foreach (PackSource source in sources)
                sb.AppendLine($"- {source.Label}: `{source.PackPath}`");
            sb.AppendLine();
            sb.AppendLine("## Leak Bands");
            foreach (var group in rows.GroupBy(r => r.Status).OrderByDescending(g => g.Count()))
                sb.AppendLine($"- {group.Key}: {group.Count()}");
            sb.AppendLine();
            sb.AppendLine("## Top Flags");
            foreach (var group in rows.SelectMany(r => r.Flags.Split('|')).Where(f => !string.IsNullOrEmpty(f) && f != "None").GroupBy(f => f).OrderByDescending(g => g.Count()))
                sb.AppendLine($"- {group.Key}: {group.Count()}");
            sb.AppendLine();
            sb.AppendLine("## Top 20 Hardening Sandbox Candidates");
            foreach (Row row in ranked.Take(20))
            {
                string levelName = row.CampaignOrder > 0 ? $"L{row.CampaignOrder}" : $"{row.PackLabel}#{row.IndexInPack}";
                sb.AppendLine(
                    $"- {levelName}: score={row.LeakScore}, H={row.HardeningBudget}, chains={row.Chains}, opening={row.OpeningChoices}, earlyAvg={F(row.EarlyAvgChoices)}, directClearOuter={row.DirectClearableOuterExits}, avgChoices={F(row.AvgChoices)}, deps={F(row.DependencyDepthProxy)}, ops={row.RecommendedOperators}, id={row.LevelId}");
            }
            sb.AppendLine();
            sb.AppendLine("## Interpretation");
            sb.AppendLine();
            sb.AppendLine("- HighLeak: best sandbox targets for light/heavy hardening.");
            sb.AppendLine("- BoundaryLeak is scored once with a capped boundary channel, so edge/direct/straight leaks do not double-count the same freedom.");
            sb.AppendLine("- DirectEscapeLeak means early no-cost outer exits are likely making the level too sweepable.");
            sb.AppendLine("- ChoiceExplosion means the player probably has too many simultaneous valid removals.");
            sb.AppendLine("- WeakDependency means the level stays wide while average unlock delay is low.");
            sb.AppendLine("- This pass is analysis only; it does not mutate level assets.");

            File.WriteAllText(ToAbsolutePath(assetPath), sb.ToString(), new UTF8Encoding(false));
        }

        static void SaveReviewPack(IReadOnlyList<Row> rows)
        {
            var levels = rows
                .Where(r => r.Level != null)
                .Select(r => r.Level)
                .Distinct()
                .ToArray();

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(ReviewPackPath);
            if (pack == null)
            {
                pack = ScriptableObject.CreateInstance<LevelPack>();
                string folder = Path.GetDirectoryName(ReviewPackPath)?.Replace('\\', '/');
                if (!string.IsNullOrEmpty(folder) && !AssetDatabase.IsValidFolder(folder))
                    Directory.CreateDirectory(ToAbsolutePath(folder));
                AssetDatabase.CreateAsset(pack, ReviewPackPath);
            }

            pack.packId = "campaign500_hardening_leak_review";
            pack.displayName = $"Campaign 500 Hardening Leak Review ({levels.Length})";
            pack.levels = levels;
            EditorUtility.SetDirty(pack);
        }

        static void SavePackAt(string packPath, string packId, string displayName, IReadOnlyList<LevelDefinition> levels)
        {
            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(packPath);
            if (pack == null)
            {
                pack = ScriptableObject.CreateInstance<LevelPack>();
                string folder = Path.GetDirectoryName(packPath)?.Replace('\\', '/');
                if (!string.IsNullOrEmpty(folder) && !AssetDatabase.IsValidFolder(folder))
                    Directory.CreateDirectory(ToAbsolutePath(folder));
                AssetDatabase.CreateAsset(pack, packPath);
            }

            pack.packId = packId;
            pack.displayName = displayName;
            pack.levels = levels.Where(l => l != null).Distinct().ToArray();
            EditorUtility.SetDirty(pack);
        }

        static LevelDefinition CreateSandboxCopy(LevelDefinition source, string label, string stamp, string levelFolder = SandboxLevelFolder)
        {
            string safeLabel = MakeSafeFileName(label);
            string assetPath = $"{levelFolder}/hardening_{safeLabel}_{stamp}.asset";
            AssetDatabase.DeleteAsset(assetPath);

            var clone = ScriptableObject.CreateInstance<LevelDefinition>();
            EditorUtility.CopySerialized(source, clone);
            clone.name = Path.GetFileNameWithoutExtension(assetPath);
            clone.levelId = $"{source.levelId}_hardening_{safeLabel}_{stamp}";
            clone.source = LevelDefinition.LevelSource.Authored;
            clone.authoredLevel = CloneAuthored(source.authoredLevel);
            AssetDatabase.CreateAsset(clone, assetPath);
            return clone;
        }

        static AuthoredLevelData CloneAuthored(AuthoredLevelData source)
        {
            var clone = new AuthoredLevelData
            {
                width = source != null ? Mathf.Max(1, source.width) : 1,
                height = source != null ? Mathf.Max(1, source.height) : 1
            };

            if (source?.blockIndices != null)
                clone.blockIndices.AddRange(source.blockIndices);

            if (source?.arrows != null)
            {
                for (int i = 0; i < source.arrows.Count; i++)
                {
                    AuthoredArrowData arrow = source.arrows[i];
                    var copy = new AuthoredArrowData
                    {
                        colorIndex = arrow?.colorIndex ?? 0,
                        indices = arrow?.indices != null ? new List<int>(arrow.indices) : new List<int>()
                    };
                    clone.arrows.Add(copy);
                }
            }

            return clone;
        }

        static int ApplyReverseOuterHeadHardening(LevelDefinition level, IRuleset rules, int maxAcceptedOps, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null || maxAcceptedOps <= 0)
                return 0;

            var accepted = new List<string>();
            Row current = AnalyzeLevel(new PackSource("sandbox_current", "", false), 0, 0, level, rules, null, null);
            if (!current.GreedySolved)
                return 0;

            for (int op = 0; op < maxAcceptedOps; op++)
            {
                List<int> candidates = FindDirectClearableOuterArrowIndices(level, rules);
                bool changed = false;

                for (int c = 0; c < candidates.Count; c++)
                {
                    int arrowIndex = candidates[c];
                    if (arrowIndex < 0 || arrowIndex >= level.authoredLevel.arrows.Count)
                        continue;

                    AuthoredArrowData arrow = level.authoredLevel.arrows[arrowIndex];
                    if (arrow?.indices == null || arrow.indices.Count < 2)
                        continue;

                    var beforeIndices = new List<int>(arrow.indices);
                    arrow.indices.Reverse();

                    Row next = AnalyzeLevel(new PackSource("sandbox_candidate", "", false), 0, 0, level, rules, null, null);
                    if (AcceptHardeningStep(current, next))
                    {
                        accepted.Add($"reverse:{arrowIndex}:score {current.LeakScore}->{next.LeakScore}:outer {current.DirectClearableOuterExits}->{next.DirectClearableOuterExits}:open {current.OpeningChoices}->{next.OpeningChoices}");
                        current = next;
                        changed = true;
                        break;
                    }

                    arrow.indices = beforeIndices;
                }

                if (!changed)
                    break;
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return accepted.Count;
        }

        static bool AcceptHardeningStep(Row before, Row after)
        {
            if (!after.GreedySolved)
                return false;
            if (after.OpeningChoices < 2)
                return false;
            if (after.ForcedMoveRatio > before.ForcedMoveRatio + 0.15f)
                return false;
            if (after.DependencyDepthProxy + 0.25f < before.DependencyDepthProxy)
                return false;
            if (after.LeakScore > before.LeakScore - 15)
                return false;

            bool leakImproved =
                after.DirectClearableOuterExits < before.DirectClearableOuterExits ||
                after.OpeningChoices < before.OpeningChoices ||
                after.EarlyAvgChoices <= before.EarlyAvgChoices - 1f;

            return leakImproved;
        }

        static int ApplyQualitativeHardeningV2(LevelDefinition level, IRuleset rules, int maxAcceptedOps, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null || maxAcceptedOps <= 0)
                return 0;

            var accepted = new List<string>();
            Row current = AnalyzeLevel(new PackSource("qualitative_current", "", false), 0, 0, level, rules, null, null);
            if (!current.GreedySolved)
                return 0;

            for (int op = 0; op < maxAcceptedOps; op++)
            {
                List<MergeCandidate> candidates = FindQualitativeMergeCandidates(level, rules);
                bool changed = false;

                for (int c = 0; c < candidates.Count; c++)
                {
                    MergeCandidate candidate = candidates[c];
                    AuthoredLevelData beforeAuthored = CloneAuthored(level.authoredLevel);
                    if (!TryApplyMergeCandidate(level, candidate, out string mergeSummary))
                        continue;

                    Row next = AnalyzeLevel(new PackSource("qualitative_candidate", "", false), 0, 0, level, rules, null, null);
                    if (AcceptQualitativeHardeningStep(current, next))
                    {
                        accepted.Add(
                            $"merge:{candidate.A}+{candidate.B}:{mergeSummary}:score {current.LeakScore}->{next.LeakScore}:chains {current.Chains}->{next.Chains}:outer {current.DirectClearableOuterExits}->{next.DirectClearableOuterExits}:open {current.OpeningChoices}->{next.OpeningChoices}");
                        current = next;
                        changed = true;
                        break;
                    }

                    level.authoredLevel = beforeAuthored;
                }

                if (!changed)
                    break;
            }

            if (accepted.Count < 3)
            {
                int fallback = ApplyReverseOuterHeadHardening(level, rules, Mathf.Max(0, 3 - accepted.Count), out string fallbackOps);
                if (fallback > 0)
                    accepted.Add($"fallback_reverse:{fallbackOps}");
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return accepted.Count;
        }

        static int ApplyPressureHardeningV3(LevelDefinition level, IRuleset rules, int maxAcceptedOps, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null || maxAcceptedOps <= 0)
                return 0;

            var accepted = new List<string>();
            int acceptedOpCount = 0;
            Row current = AnalyzeLevel(new PackSource("pressure_current", "", false), 0, 0, level, rules, null, null);
            if (!current.GreedySolved)
                return 0;

            int redirectOps = ApplyPressureReverseOuterHeadV3(level, rules, maxAcceptedOps: Mathf.Min(4, maxAcceptedOps), out string redirectText);
            if (redirectOps > 0)
            {
                accepted.Add($"redirect:{redirectText}");
                acceptedOpCount += redirectOps;
                current = AnalyzeLevel(new PackSource("pressure_after_redirect", "", false), 0, 0, level, rules, null, null);
            }

            for (int op = 0; op < maxAcceptedOps; op++)
            {
                PressureSnapshot quickCurrent = AnalyzePressureQuick(level, rules);
                List<MergeCandidate> candidates = FindQualitativeMergeCandidates(
                    level,
                    rules,
                    maxInfoLength: 28,
                    maxTotalLength: 40,
                    takeLimit: 260);

                int quickLimit = op < 6 ? 50 : 75;
                var quickChoices = new List<PressureCandidateChoice>(8);
                for (int c = 0; c < candidates.Count && c < quickLimit; c++)
                {
                    MergeCandidate candidate = candidates[c];
                    AuthoredLevelData beforeAuthored = CloneAuthored(level.authoredLevel);
                    if (!TryApplyMergeCandidate(level, candidate, out string mergeSummary))
                    {
                        level.authoredLevel = beforeAuthored;
                        continue;
                    }

                    PressureSnapshot quickNext = AnalyzePressureQuick(level, rules);
                    if (AcceptPressureQuickStep(quickCurrent, quickNext))
                    {
                        quickChoices.Add(new PressureCandidateChoice
                        {
                            Candidate = candidate,
                            Authored = CloneAuthored(level.authoredLevel),
                            MergeSummary = mergeSummary,
                            QuickScore = ScorePressureQuickStep(quickCurrent, quickNext, candidate),
                            QuickAfter = quickNext
                        });
                    }

                    level.authoredLevel = beforeAuthored;
                }

                bool found = false;
                float bestScore = float.NegativeInfinity;
                Row bestRow = null;
                AuthoredLevelData bestAuthored = null;
                string bestText = "";
                AuthoredLevelData iterationAuthored = CloneAuthored(level.authoredLevel);

                foreach (PressureCandidateChoice choice in quickChoices
                    .OrderByDescending(c => c.QuickScore)
                    .Take(2))
                {
                    level.authoredLevel = CloneAuthored(choice.Authored);
                    Row next = AnalyzeLevel(new PackSource("pressure_candidate", "", false), 0, 0, level, rules, null, null);
                    if (!AcceptPressureHardeningStep(current, next))
                        continue;

                    float pressureScore = ScorePressureStep(current, next, choice.Candidate) + choice.QuickScore * 0.2f;
                    if (pressureScore <= bestScore)
                        continue;

                    found = true;
                    bestScore = pressureScore;
                    bestRow = next;
                    bestAuthored = CloneAuthored(level.authoredLevel);
                    bestText =
                        $"merge:{choice.Candidate.A}+{choice.Candidate.B}:{choice.MergeSummary}:score {current.LeakScore}->{next.LeakScore}:chains {current.Chains}->{next.Chains}:outer {current.DirectClearableOuterExits}->{next.DirectClearableOuterExits}:open {current.OpeningChoices}->{next.OpeningChoices}:avg {F(current.AvgChoices)}->{F(next.AvgChoices)}";
                }

                if (!found || bestAuthored == null || bestRow == null)
                {
                    level.authoredLevel = iterationAuthored;
                    break;
                }

                level.authoredLevel = bestAuthored;
                current = bestRow;
                accepted.Add(bestText);
                acceptedOpCount++;
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return acceptedOpCount;
        }

        static int ApplyGateHardeningV4(LevelDefinition level, IRuleset rules, int maxAcceptedOps, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null || maxAcceptedOps <= 0)
                return 0;

            var accepted = new List<string>();
            int acceptedOpCount = 0;

            Row current = AnalyzeLevel(new PackSource("gate_current", "", false), 0, 0, level, rules, null, null);
            if (!current.GreedySolved)
                return 0;

            int redirectOps = ApplyPressureReverseOuterHeadV3(level, rules, maxAcceptedOps: Mathf.Min(4, maxAcceptedOps), out string redirectText);
            if (redirectOps > 0)
            {
                accepted.Add($"redirect:{redirectText}");
                acceptedOpCount += redirectOps;
                current = AnalyzeLevel(new PackSource("gate_after_redirect", "", false), 0, 0, level, rules, null, null);
            }

            for (int op = acceptedOpCount; op < maxAcceptedOps; op++)
            {
                List<MergeCandidate> candidates = FindGateFoldCandidates(
                    level,
                    rules,
                    maxOpeningLength: 34,
                    maxOtherLength: 34,
                    maxTotalLength: 46,
                    takeLimit: 120);

                bool found = false;
                float bestScore = float.NegativeInfinity;
                Row bestRow = null;
                AuthoredLevelData bestAuthored = null;
                string bestText = "";
                AuthoredLevelData iterationAuthored = CloneAuthored(level.authoredLevel);

                for (int c = 0; c < candidates.Count && c < 18; c++)
                {
                    MergeCandidate candidate = candidates[c];
                    level.authoredLevel = CloneAuthored(iterationAuthored);
                    if (!TryApplyMergeCandidate(level, candidate, out string mergeSummary))
                        continue;

                    Row next = AnalyzeLevel(new PackSource("gate_candidate", "", false), 0, 0, level, rules, null, null);
                    if (!AcceptGateFoldStep(current, next))
                        continue;

                    float score = ScoreGateFoldStep(current, next, candidate);
                    if (score <= bestScore)
                        continue;

                    found = true;
                    bestScore = score;
                    bestRow = next;
                    bestAuthored = CloneAuthored(level.authoredLevel);
                    bestText =
                        $"gateFold:{candidate.A}+{candidate.B}:{mergeSummary}:score {current.LeakScore}->{next.LeakScore}:chains {current.Chains}->{next.Chains}:open {current.OpeningChoices}->{next.OpeningChoices}:outer {current.DirectClearableOuterExits}->{next.DirectClearableOuterExits}:avg {F(current.AvgChoices)}->{F(next.AvgChoices)}";
                }

                if (!found || bestAuthored == null || bestRow == null)
                {
                    level.authoredLevel = iterationAuthored;
                    break;
                }

                level.authoredLevel = bestAuthored;
                current = bestRow;
                accepted.Add(bestText);
                acceptedOpCount++;
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return acceptedOpCount;
        }

        static bool AcceptGateFoldStep(Row before, Row after)
        {
            if (!after.GreedySolved)
                return false;
            if (after.Chains != before.Chains - 1)
                return false;
            if (after.ArrowTiles != before.ArrowTiles)
                return false;
            if (after.OpeningChoices < 2)
                return false;
            if (after.ForcedMoveRatio > before.ForcedMoveRatio + 0.3f)
                return false;
            if (after.DependencyDepthProxy + 0.5f < before.DependencyDepthProxy)
                return false;
            if (after.MaxChoices > before.MaxChoices + 3)
                return false;
            if (after.AvgChoices > before.AvgChoices + 0.2f)
                return false;
            if (after.DirectClearableOuterExits > before.DirectClearableOuterExits + 1)
                return false;

            return
                after.OpeningChoices <= before.OpeningChoices - 1 ||
                after.DirectClearableOuterExits <= before.DirectClearableOuterExits - 1 ||
                after.AvgChoices <= before.AvgChoices - 0.45f ||
                after.EarlyAvgChoices <= before.EarlyAvgChoices - 0.8f ||
                after.LeakScore <= before.LeakScore - 18;
        }

        static float ScoreGateFoldStep(Row before, Row after, MergeCandidate candidate)
        {
            float score = 0f;
            score += (before.OpeningChoices - after.OpeningChoices) * 95f;
            score += (before.DirectClearableOuterExits - after.DirectClearableOuterExits) * 85f;
            score += (before.AvgChoices - after.AvgChoices) * 170f;
            score += (before.EarlyAvgChoices - after.EarlyAvgChoices) * 80f;
            score += (before.LeakScore - after.LeakScore) * 0.5f;
            score += (before.Chains - after.Chains) * 120f;
            score += Mathf.Min(candidate.TotalLength, 52) * 1.2f;
            if (after.OpeningChoices <= 18)
                score += 120f;
            else if (after.OpeningChoices <= 22)
                score += 60f;
            if (after.AvgChoices <= 10f)
                score += 140f;
            else if (after.AvgChoices <= 11.5f)
                score += 75f;
            return score;
        }

        static int ApplyVisibleGateInjectionV5(LevelDefinition level, IRuleset rules, int maxAcceptedOps, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null || maxAcceptedOps <= 0)
                return 0;

            var accepted = new List<string>();
            int acceptedOpCount = 0;

            Row current = AnalyzeLevel(new PackSource("visible_gate_current", "", false), 0, 0, level, rules, null, null);
            if (!current.GreedySolved)
                return 0;

            for (int op = 0; op < maxAcceptedOps; op++)
            {
                List<VisibleGateCandidate> candidates = FindVisibleGateCandidatesV5(level, rules, takeLimit: 90);
                bool found = false;
                float bestScore = float.NegativeInfinity;
                Row bestRow = null;
                AuthoredLevelData bestAuthored = null;
                string bestText = "";
                AuthoredLevelData iterationAuthored = CloneAuthored(level.authoredLevel);

                for (int c = 0; c < candidates.Count && c < 30; c++)
                {
                    VisibleGateCandidate candidate = candidates[c];
                    level.authoredLevel = CloneAuthored(iterationAuthored);
                    if (!TryApplyVisibleGateCandidateV5(level, candidate, out string summary))
                        continue;

                    Row next = AnalyzeLevel(new PackSource("visible_gate_candidate", "", false), 0, 0, level, rules, null, null);
                    if (!AcceptVisibleGateStepV5(current, next, candidate))
                        continue;

                    float score = ScoreVisibleGateStepV5(current, next, candidate);
                    if (score <= bestScore)
                        continue;

                    found = true;
                    bestScore = score;
                    bestRow = next;
                    bestAuthored = CloneAuthored(level.authoredLevel);
                    bestText =
                        $"visibleGate:t{candidate.TargetArrowIndex}:{candidate.Kind}:path {summary}:score {current.LeakScore}->{next.LeakScore}:chains {current.Chains}->{next.Chains}:tiles {current.ArrowTiles}->{next.ArrowTiles}:open {current.OpeningChoices}->{next.OpeningChoices}:outer {current.DirectClearableOuterExits}->{next.DirectClearableOuterExits}:avg {F(current.AvgChoices)}->{F(next.AvgChoices)}";
                }

                if (!found || bestAuthored == null || bestRow == null)
                {
                    level.authoredLevel = iterationAuthored;
                    break;
                }

                level.authoredLevel = bestAuthored;
                current = bestRow;
                accepted.Add(bestText);
                acceptedOpCount++;
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return acceptedOpCount;
        }

        static bool AcceptVisibleGateStepV5(Row before, Row after, VisibleGateCandidate candidate)
        {
            if (!after.GreedySolved)
                return false;
            if (after.Chains != before.Chains + 1)
                return false;
            if (after.ArrowTiles != before.ArrowTiles + candidate.Path.Count)
                return false;
            if (after.OpeningChoices < 2)
                return false;
            if (after.ForcedMoveRatio > before.ForcedMoveRatio + 0.32f)
                return false;
            if (after.DependencyDepthProxy + 0.5f < before.DependencyDepthProxy)
                return false;
            if (after.MaxChoices > before.MaxChoices + 6)
                return false;
            if (after.AvgChoices > before.AvgChoices + 1.2f)
                return false;
            if (after.DirectClearableOuterExits > before.DirectClearableOuterExits + 2)
                return false;

            return
                after.OpeningChoices <= before.OpeningChoices - 1 ||
                after.DirectClearableOuterExits <= before.DirectClearableOuterExits - 1 ||
                after.AvgChoices <= before.AvgChoices - 0.2f ||
                after.EarlyAvgChoices <= before.EarlyAvgChoices - 0.5f ||
                after.LeakScore <= before.LeakScore + 20 ||
                (candidate.Path.Count >= 3 &&
                 after.OpeningChoices <= before.OpeningChoices + 1 &&
                 after.DirectClearableOuterExits <= before.DirectClearableOuterExits + 1);
        }

        static float ScoreVisibleGateStepV5(Row before, Row after, VisibleGateCandidate candidate)
        {
            float score = 0f;
            score += (before.OpeningChoices - after.OpeningChoices) * 150f;
            score += (before.DirectClearableOuterExits - after.DirectClearableOuterExits) * 130f;
            score += (before.AvgChoices - after.AvgChoices) * 170f;
            score += (before.EarlyAvgChoices - after.EarlyAvgChoices) * 90f;
            score += (before.LeakScore - after.LeakScore) * 0.55f;
            score += candidate.Path.Count * 55f;
            score += candidate.StaticScore * 0.15f;
            if (candidate.Path.Count >= 3)
                score += 90f;
            if (candidate.Kind.Contains("hook"))
                score += 75f;
            if (after.OpeningChoices > before.OpeningChoices)
                score -= 220f;
            if (after.DirectClearableOuterExits > before.DirectClearableOuterExits)
                score -= 180f;
            if (after.AvgChoices > before.AvgChoices)
                score -= 120f;
            return score;
        }

        static int ApplyEarlyPeelGateInjectionV6(LevelDefinition level, IRuleset rules, int maxAcceptedOps, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null || maxAcceptedOps <= 0)
                return 0;

            var accepted = new List<string>();
            int acceptedOpCount = 0;

            Row current = AnalyzeLevel(new PackSource("early_peel_gate_current", "", false), 0, 0, level, rules, null, null);
            if (!current.GreedySolved)
                return 0;

            for (int op = 0; op < maxAcceptedOps; op++)
            {
                EarlyPeelResult peel = BuildEarlyPeelResultV6(level, rules, targetChoices: 8, maxWaves: 4, maxRemovedChains: 70);
                List<VisibleGateCandidate> candidates = FindEarlyPeelGateCandidatesV6(level, rules, peel, takeLimit: 120);
                bool found = false;
                float bestScore = float.NegativeInfinity;
                Row bestRow = null;
                AuthoredLevelData bestAuthored = null;
                string bestText = "";
                AuthoredLevelData iterationAuthored = CloneAuthored(level.authoredLevel);

                for (int c = 0; c < candidates.Count && c < 36; c++)
                {
                    VisibleGateCandidate candidate = candidates[c];
                    level.authoredLevel = CloneAuthored(iterationAuthored);
                    if (!TryApplyVisibleGateCandidateV5(level, candidate, out string summary))
                        continue;

                    Row next = AnalyzeLevel(new PackSource("early_peel_gate_candidate", "", false), 0, 0, level, rules, null, null);
                    if (!AcceptEarlyPeelGateStepV6(current, next, candidate, peel))
                        continue;

                    float score = ScoreEarlyPeelGateStepV6(current, next, candidate, peel);
                    if (score <= bestScore)
                        continue;

                    found = true;
                    bestScore = score;
                    bestRow = next;
                    bestAuthored = CloneAuthored(level.authoredLevel);
                    bestText =
                        $"earlyPeelGate:w{candidate.PeelWave}:t{candidate.TargetArrowIndex}:{candidate.Kind}:path {summary}:peel {peel.Summary}:score {current.LeakScore}->{next.LeakScore}:chains {current.Chains}->{next.Chains}:tiles {current.ArrowTiles}->{next.ArrowTiles}:open {current.OpeningChoices}->{next.OpeningChoices}:outer {current.DirectClearableOuterExits}->{next.DirectClearableOuterExits}:avg {F(current.AvgChoices)}->{F(next.AvgChoices)}";
                }

                if (!found || bestAuthored == null || bestRow == null)
                {
                    level.authoredLevel = iterationAuthored;
                    break;
                }

                level.authoredLevel = bestAuthored;
                current = bestRow;
                accepted.Add(bestText);
                acceptedOpCount++;
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return acceptedOpCount;
        }

        static bool AcceptEarlyPeelGateStepV6(Row before, Row after, VisibleGateCandidate candidate, EarlyPeelResult peel)
        {
            if (!after.GreedySolved)
                return false;
            if (after.Chains != before.Chains + 1)
                return false;
            if (after.ArrowTiles != before.ArrowTiles + candidate.Path.Count)
                return false;
            if (after.OpeningChoices < 2)
                return false;
            if (after.ForcedMoveRatio > before.ForcedMoveRatio + 0.28f)
                return false;
            if (after.DependencyDepthProxy + 0.45f < before.DependencyDepthProxy)
                return false;
            if (after.MaxChoices > before.MaxChoices + 5)
                return false;
            if (after.AvgChoices > before.AvgChoices + 1.0f)
                return false;
            if (after.DirectClearableOuterExits > before.DirectClearableOuterExits + 1)
                return false;

            return
                after.OpeningChoices <= before.OpeningChoices - 1 ||
                after.DirectClearableOuterExits <= before.DirectClearableOuterExits - 1 ||
                after.AvgChoices <= before.AvgChoices - 0.35f ||
                after.EarlyAvgChoices <= before.EarlyAvgChoices - 0.75f ||
                after.LeakScore <= before.LeakScore - 14 ||
                (peel.RemovedCount >= 30 &&
                 candidate.PeelWave >= 1 &&
                 candidate.Path.Count >= 3 &&
                 after.OpeningChoices <= before.OpeningChoices + 1 &&
                 after.DirectClearableOuterExits <= before.DirectClearableOuterExits);
        }

        static float ScoreEarlyPeelGateStepV6(Row before, Row after, VisibleGateCandidate candidate, EarlyPeelResult peel)
        {
            float score = ScoreVisibleGateStepV5(before, after, candidate);
            score += Mathf.Min(peel.RemovedCount, 80) * 2.2f;
            score += Mathf.Min(peel.InitialChoices, 40) * 5f;
            score += candidate.PeelWave * 130f;
            score += candidate.PeelChoiceCount * 7f;
            if (candidate.PeelWave >= 1)
                score += 140f;
            if (peel.FinalChoices <= 10)
                score += 75f;
            if (after.OpeningChoices <= before.OpeningChoices - 3)
                score += 150f;
            if (after.AvgChoices <= before.AvgChoices - 2f)
                score += 150f;
            return score;
        }

        static int ApplyOpeningPeelGateInjectionV7(
            LevelDefinition level,
            IRuleset rules,
            int maxOpeningOps,
            int maxPeelOps,
            out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null || maxOpeningOps <= 0)
                return 0;

            var accepted = new List<string>();
            int acceptedOpCount = 0;

            Row current = AnalyzeLevel(new PackSource("opening_peel_gate_current", "", false), 0, 0, level, rules, null, null);
            if (!current.GreedySolved)
                return 0;

            int targetOpening = Mathf.Max(10, Mathf.RoundToInt(current.OpeningChoices * 0.58f));
            for (int op = 0; op < maxOpeningOps; op++)
            {
                List<VisibleGateCandidate> candidates = FindOpeningGateCandidatesV7(level, rules, takeLimit: 120);
                bool found = false;
                float bestScore = float.NegativeInfinity;
                Row bestRow = null;
                AuthoredLevelData bestAuthored = null;
                string bestText = "";
                AuthoredLevelData iterationAuthored = CloneAuthored(level.authoredLevel);
                PressureSnapshot quickBefore = AnalyzePressureQuick(level, rules);
                var quickChoices = new List<GateCandidateChoice>(12);

                for (int c = 0; c < candidates.Count && c < 28; c++)
                {
                    VisibleGateCandidate candidate = candidates[c];
                    level.authoredLevel = CloneAuthored(iterationAuthored);
                    if (!TryApplyVisibleGateCandidateV5(level, candidate, out string summary))
                        continue;

                    PressureSnapshot quickAfter = AnalyzePressureQuick(level, rules);
                    if (!AcceptOpeningGateQuickStepV7(quickBefore, quickAfter, candidate))
                        continue;

                    quickChoices.Add(new GateCandidateChoice
                    {
                        Candidate = candidate,
                        Authored = CloneAuthored(level.authoredLevel),
                        Summary = summary,
                        QuickScore = ScoreOpeningGateQuickStepV7(quickBefore, quickAfter, candidate),
                        QuickAfter = quickAfter
                    });
                }

                foreach (GateCandidateChoice choice in quickChoices
                    .OrderByDescending(c => c.QuickScore)
                    .ThenByDescending(c => c.Candidate.Path.Count)
                    .Take(2))
                {
                    VisibleGateCandidate candidate = choice.Candidate;
                    level.authoredLevel = CloneAuthored(choice.Authored);
                    Row next = AnalyzeLevel(new PackSource("opening_peel_gate_candidate", "", false), 0, 0, level, rules, null, null);
                    if (!AcceptOpeningGateStepV7(current, next, candidate))
                        continue;

                    float score = ScoreOpeningGateStepV7(current, next, candidate, targetOpening);
                    score += choice.QuickScore * 0.35f;
                    if (score <= bestScore)
                        continue;

                    found = true;
                    bestScore = score;
                    bestRow = next;
                    bestAuthored = CloneAuthored(level.authoredLevel);
                    bestText =
                        $"openingGate:t{candidate.TargetArrowIndex}:{candidate.Kind}:path {choice.Summary}:score {current.LeakScore}->{next.LeakScore}:chains {current.Chains}->{next.Chains}:tiles {current.ArrowTiles}->{next.ArrowTiles}:open {current.OpeningChoices}->{next.OpeningChoices}:outer {current.DirectClearableOuterExits}->{next.DirectClearableOuterExits}:avg {F(current.AvgChoices)}->{F(next.AvgChoices)}";
                }

                if (!found || bestAuthored == null || bestRow == null)
                {
                    level.authoredLevel = iterationAuthored;
                    break;
                }

                level.authoredLevel = bestAuthored;
                current = bestRow;
                accepted.Add(bestText);
                acceptedOpCount++;

                if (current.OpeningChoices <= targetOpening && current.DirectClearableOuterExits <= targetOpening + 1)
                    break;
            }

            if (maxPeelOps > 0 && current.GreedySolved && current.OpeningChoices <= targetOpening + 4)
            {
                int peelOps = ApplyEarlyPeelGateInjectionV7(level, rules, maxPeelOps, ref current, accepted);
                acceptedOpCount += peelOps;
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return acceptedOpCount;
        }

        static int ApplyEarlyPeelGateInjectionV7(
            LevelDefinition level,
            IRuleset rules,
            int maxAcceptedOps,
            ref Row current,
            List<string> accepted)
        {
            if (level?.authoredLevel?.arrows == null || maxAcceptedOps <= 0 || accepted == null)
                return 0;

            int acceptedOpCount = 0;
            for (int op = 0; op < maxAcceptedOps; op++)
            {
                EarlyPeelResult peel = BuildEarlyPeelResultV6(level, rules, targetChoices: 8, maxWaves: 4, maxRemovedChains: 70);
                List<VisibleGateCandidate> candidates = FindEarlyPeelGateCandidatesV6(level, rules, peel, takeLimit: 140);
                bool found = false;
                float bestScore = float.NegativeInfinity;
                Row bestRow = null;
                AuthoredLevelData bestAuthored = null;
                string bestText = "";
                AuthoredLevelData iterationAuthored = CloneAuthored(level.authoredLevel);

                for (int c = 0; c < candidates.Count && c < 42; c++)
                {
                    VisibleGateCandidate candidate = candidates[c];
                    level.authoredLevel = CloneAuthored(iterationAuthored);
                    if (!TryApplyVisibleGateCandidateV5(level, candidate, out string summary))
                        continue;

                    Row next = AnalyzeLevel(new PackSource("opening_peel_gate_deep_candidate", "", false), 0, 0, level, rules, null, null);
                    if (!AcceptEarlyPeelGateStepV7(current, next, candidate, peel))
                        continue;

                    float score = ScoreEarlyPeelGateStepV6(current, next, candidate, peel);
                    score += (current.AvgChoices - next.AvgChoices) * 120f;
                    if (score <= bestScore)
                        continue;

                    found = true;
                    bestScore = score;
                    bestRow = next;
                    bestAuthored = CloneAuthored(level.authoredLevel);
                    bestText =
                        $"deepPeelGate:w{candidate.PeelWave}:t{candidate.TargetArrowIndex}:{candidate.Kind}:path {summary}:peel {peel.Summary}:score {current.LeakScore}->{next.LeakScore}:chains {current.Chains}->{next.Chains}:tiles {current.ArrowTiles}->{next.ArrowTiles}:open {current.OpeningChoices}->{next.OpeningChoices}:outer {current.DirectClearableOuterExits}->{next.DirectClearableOuterExits}:avg {F(current.AvgChoices)}->{F(next.AvgChoices)}";
                }

                if (!found || bestAuthored == null || bestRow == null)
                {
                    level.authoredLevel = iterationAuthored;
                    break;
                }

                level.authoredLevel = bestAuthored;
                current = bestRow;
                accepted.Add(bestText);
                acceptedOpCount++;
            }

            return acceptedOpCount;
        }

        static bool AcceptOpeningGateStepV7(Row before, Row after, VisibleGateCandidate candidate)
        {
            if (!after.GreedySolved)
                return false;
            if (after.Chains != before.Chains + 1)
                return false;
            if (after.ArrowTiles != before.ArrowTiles + candidate.Path.Count)
                return false;
            if (after.OpeningChoices < 2)
                return false;
            if (after.OpeningChoices > before.OpeningChoices - 1)
                return false;
            if (after.DirectClearableOuterExits > before.DirectClearableOuterExits)
                return false;
            if (after.ForcedMoveRatio > before.ForcedMoveRatio + 0.24f)
                return false;
            if (after.DependencyDepthProxy + 0.35f < before.DependencyDepthProxy)
                return false;
            if (after.MaxChoices > before.MaxChoices + 4)
                return false;
            if (after.AvgChoices > before.AvgChoices + 0.65f)
                return false;

            return
                after.DirectClearableOuterExits <= before.DirectClearableOuterExits - 1 ||
                after.AvgChoices <= before.AvgChoices - 0.2f ||
                after.EarlyAvgChoices <= before.EarlyAvgChoices - 0.5f ||
                 after.LeakScore <= before.LeakScore - 18;
        }

        static bool AcceptOpeningGateQuickStepV7(PressureSnapshot before, PressureSnapshot after, VisibleGateCandidate candidate)
        {
            if (after.Chains != before.Chains + 1)
                return false;
            if (after.ArrowTiles != before.ArrowTiles + candidate.Path.Count)
                return false;
            if (after.OpeningChoices < 2)
                return false;
            if (after.OpeningChoices > before.OpeningChoices - 1)
                return false;
            if (after.DirectClearableOuterExits > before.DirectClearableOuterExits)
                return false;
            if (after.PressureScore > before.PressureScore + 10f)
                return false;

            return
                after.DirectClearableOuterExits <= before.DirectClearableOuterExits - 1 ||
                after.EdgeShortDirectOuterExits <= before.EdgeShortDirectOuterExits - 1 ||
                after.BoundaryStraightOuterExits <= before.BoundaryStraightOuterExits - 1 ||
                after.PressureScore <= before.PressureScore - 35f;
        }

        static bool AcceptEarlyPeelGateStepV7(Row before, Row after, VisibleGateCandidate candidate, EarlyPeelResult peel)
        {
            if (!after.GreedySolved)
                return false;
            if (after.Chains != before.Chains + 1)
                return false;
            if (after.ArrowTiles != before.ArrowTiles + candidate.Path.Count)
                return false;
            if (after.OpeningChoices < 2)
                return false;
            if (after.OpeningChoices > before.OpeningChoices)
                return false;
            if (after.DirectClearableOuterExits > before.DirectClearableOuterExits)
                return false;
            if (after.ForcedMoveRatio > before.ForcedMoveRatio + 0.22f)
                return false;
            if (after.DependencyDepthProxy + 0.35f < before.DependencyDepthProxy)
                return false;
            if (after.MaxChoices > before.MaxChoices + 3)
                return false;
            if (after.AvgChoices > before.AvgChoices + 0.35f)
                return false;

            return
                after.AvgChoices <= before.AvgChoices - 0.4f ||
                after.EarlyAvgChoices <= before.EarlyAvgChoices - 0.6f ||
                after.LeakScore <= before.LeakScore - 18 ||
                 (peel.RemovedCount >= 30 &&
                 candidate.PeelWave >= 2 &&
                 after.OpeningChoices <= before.OpeningChoices - 1);
        }

        static float ScoreOpeningGateQuickStepV7(PressureSnapshot before, PressureSnapshot after, VisibleGateCandidate candidate)
        {
            float score = 0f;
            score += (before.OpeningChoices - after.OpeningChoices) * 300f;
            score += (before.DirectClearableOuterExits - after.DirectClearableOuterExits) * 260f;
            score += (before.EdgeShortDirectOuterExits - after.EdgeShortDirectOuterExits) * 90f;
            score += (before.BoundaryStraightOuterExits - after.BoundaryStraightOuterExits) * 110f;
            score += (before.PressureScore - after.PressureScore) * 0.7f;
            score += candidate.StaticScore * 0.04f;
            if (candidate.Kind.Contains("closed"))
                score += 120f;
            if (candidate.Path.Count >= 3)
                score += 65f;
            return score;
        }

        static float ScoreOpeningGateStepV7(Row before, Row after, VisibleGateCandidate candidate, int targetOpening)
        {
            float score = 0f;
            score += (before.OpeningChoices - after.OpeningChoices) * 420f;
            score += (before.DirectClearableOuterExits - after.DirectClearableOuterExits) * 360f;
            score += (before.AvgChoices - after.AvgChoices) * 190f;
            score += (before.EarlyAvgChoices - after.EarlyAvgChoices) * 120f;
            score += (before.LeakScore - after.LeakScore) * 0.65f;
            score += candidate.StaticScore * 0.08f;
            score += candidate.Path.Count * 45f;
            if (candidate.Kind.Contains("closed"))
                score += 160f;
            if (candidate.Kind.Contains("bend") || candidate.Kind.Contains("hook"))
                score += 80f;
            if (after.OpeningChoices <= targetOpening)
                score += 240f;
            if (after.DirectClearableOuterExits <= targetOpening + 1)
                score += 160f;
            if (after.OpeningChoices > before.OpeningChoices - 1)
                score -= 1000f;
            if (after.DirectClearableOuterExits > before.DirectClearableOuterExits)
                score -= 700f;
            return score;
        }

        static int ApplyOpeningRewireGateInjectionV8(
            LevelDefinition level,
            IRuleset rules,
            int maxRewireOps,
            int maxFallbackGateOps,
            out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null || maxRewireOps <= 0)
                return 0;

            var accepted = new List<string>();
            int acceptedOpCount = 0;

            Row current = AnalyzeLevel(new PackSource("opening_rewire_gate_current", "", false), 0, 0, level, rules, null, null);
            if (!current.GreedySolved)
                return 0;

            int targetOpening = Mathf.Max(10, Mathf.RoundToInt(current.OpeningChoices * 0.55f));
            for (int op = 0; op < maxRewireOps; op++)
            {
                List<RewireCandidate> candidates = FindOpeningRewireCandidatesV8(
                    level,
                    rules,
                    maxBridgeCells: 5,
                    maxTotalLength: 64,
                    takeLimit: 180);

                bool found = false;
                float bestScore = float.NegativeInfinity;
                Row bestRow = null;
                AuthoredLevelData bestAuthored = null;
                string bestText = "";
                AuthoredLevelData iterationAuthored = CloneAuthored(level.authoredLevel);
                PressureSnapshot quickBefore = AnalyzePressureQuick(level, rules);
                var quickChoices = new List<RewireCandidateChoice>(10);

                for (int c = 0; c < candidates.Count && c < 42; c++)
                {
                    RewireCandidate candidate = candidates[c];
                    level.authoredLevel = CloneAuthored(iterationAuthored);
                    if (!TryApplyRewireCandidateV8(level, candidate, out string summary))
                        continue;

                    PressureSnapshot quickAfter = AnalyzePressureQuick(level, rules);
                    if (!AcceptOpeningRewireQuickStepV8(quickBefore, quickAfter, candidate))
                        continue;

                    quickChoices.Add(new RewireCandidateChoice
                    {
                        Candidate = candidate,
                        Authored = CloneAuthored(level.authoredLevel),
                        Summary = summary,
                        QuickScore = ScoreOpeningRewireQuickStepV8(quickBefore, quickAfter, candidate),
                        QuickAfter = quickAfter
                    });
                }

                foreach (RewireCandidateChoice choice in quickChoices
                    .OrderByDescending(c => c.QuickScore)
                    .ThenByDescending(c => c.Candidate.TotalLength)
                    .Take(3))
                {
                    RewireCandidate candidate = choice.Candidate;
                    level.authoredLevel = CloneAuthored(choice.Authored);
                    Row next = AnalyzeLevel(new PackSource("opening_rewire_gate_candidate", "", false), 0, 0, level, rules, null, null);
                    if (!AcceptOpeningRewireStepV8(current, next, candidate))
                        continue;

                    float score = ScoreOpeningRewireStepV8(current, next, candidate, targetOpening);
                    score += choice.QuickScore * 0.3f;
                    if (score <= bestScore)
                        continue;

                    found = true;
                    bestScore = score;
                    bestRow = next;
                    bestAuthored = CloneAuthored(level.authoredLevel);
                    bestText =
                        $"openingRewire:{candidate.A}+{candidate.B}:{candidate.Kind}:bridge {candidate.Bridge.Count}:path {choice.Summary}:score {current.LeakScore}->{next.LeakScore}:chains {current.Chains}->{next.Chains}:tiles {current.ArrowTiles}->{next.ArrowTiles}:open {current.OpeningChoices}->{next.OpeningChoices}:outer {current.DirectClearableOuterExits}->{next.DirectClearableOuterExits}:avg {F(current.AvgChoices)}->{F(next.AvgChoices)}";
                }

                if (!found || bestAuthored == null || bestRow == null)
                {
                    level.authoredLevel = iterationAuthored;
                    break;
                }

                level.authoredLevel = bestAuthored;
                current = bestRow;
                accepted.Add(bestText);
                acceptedOpCount++;

                if (current.OpeningChoices <= targetOpening && current.DirectClearableOuterExits <= targetOpening + 1)
                    break;
            }

            if (maxFallbackGateOps > 0 && current.GreedySolved && current.OpeningChoices > targetOpening + 2)
            {
                int fallback = ApplyOpeningPeelGateInjectionV7(level, rules, maxFallbackGateOps, maxPeelOps: 0, out string fallbackOps);
                if (fallback > 0)
                {
                    accepted.Add($"fallbackOpeningGate:{fallbackOps}");
                    acceptedOpCount += fallback;
                }
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return acceptedOpCount;
        }

        static bool AcceptOpeningRewireQuickStepV8(PressureSnapshot before, PressureSnapshot after, RewireCandidate candidate)
        {
            if (after.Chains != before.Chains - 1)
                return false;
            if (after.ArrowTiles != before.ArrowTiles + candidate.Bridge.Count)
                return false;
            if (after.OpeningChoices < 2)
                return false;
            if (after.OpeningChoices > before.OpeningChoices - 1)
                return false;
            if (after.DirectClearableOuterExits > before.DirectClearableOuterExits)
                return false;
            if (after.PressureScore > before.PressureScore + 8f)
                return false;

            return
                after.OpeningChoices <= before.OpeningChoices - 2 ||
                after.DirectClearableOuterExits <= before.DirectClearableOuterExits - 1 ||
                after.EdgeShortDirectOuterExits <= before.EdgeShortDirectOuterExits - 1 ||
                after.BoundaryStraightOuterExits <= before.BoundaryStraightOuterExits - 1 ||
                after.PressureScore <= before.PressureScore - 45f;
        }

        static bool AcceptOpeningRewireStepV8(Row before, Row after, RewireCandidate candidate)
        {
            if (!after.GreedySolved)
                return false;
            if (after.Chains != before.Chains - 1)
                return false;
            if (after.ArrowTiles != before.ArrowTiles + candidate.Bridge.Count)
                return false;
            if (after.OpeningChoices < 2)
                return false;
            if (after.OpeningChoices > before.OpeningChoices - 1)
                return false;
            if (after.DirectClearableOuterExits > before.DirectClearableOuterExits)
                return false;
            if (after.ForcedMoveRatio > before.ForcedMoveRatio + 0.24f)
                return false;
            if (after.DependencyDepthProxy + 0.35f < before.DependencyDepthProxy)
                return false;
            if (after.MaxChoices > before.MaxChoices + 4)
                return false;
            if (after.AvgChoices > before.AvgChoices + 0.6f)
                return false;

            return
                after.OpeningChoices <= before.OpeningChoices - 2 ||
                after.DirectClearableOuterExits <= before.DirectClearableOuterExits - 2 ||
                after.AvgChoices <= before.AvgChoices - 0.55f ||
                after.EarlyAvgChoices <= before.EarlyAvgChoices - 0.9f ||
                after.LeakScore <= before.LeakScore - 26;
        }

        static float ScoreOpeningRewireQuickStepV8(PressureSnapshot before, PressureSnapshot after, RewireCandidate candidate)
        {
            float score = 0f;
            score += (before.OpeningChoices - after.OpeningChoices) * 340f;
            score += (before.DirectClearableOuterExits - after.DirectClearableOuterExits) * 290f;
            score += (before.EdgeShortDirectOuterExits - after.EdgeShortDirectOuterExits) * 100f;
            score += (before.BoundaryStraightOuterExits - after.BoundaryStraightOuterExits) * 120f;
            score += (before.PressureScore - after.PressureScore) * 0.75f;
            score += Mathf.Min(candidate.TotalLength, 70) * 4f;
            score += candidate.StaticScore * 0.05f;
            score -= candidate.Bridge.Count * 18f;
            return score;
        }

        static float ScoreOpeningRewireStepV8(Row before, Row after, RewireCandidate candidate, int targetOpening)
        {
            float score = 0f;
            score += (before.OpeningChoices - after.OpeningChoices) * 460f;
            score += (before.DirectClearableOuterExits - after.DirectClearableOuterExits) * 390f;
            score += (before.AvgChoices - after.AvgChoices) * 210f;
            score += (before.EarlyAvgChoices - after.EarlyAvgChoices) * 140f;
            score += (before.LeakScore - after.LeakScore) * 0.75f;
            score += (before.Chains - after.Chains) * 260f;
            score += Mathf.Min(candidate.TotalLength, 70) * 6f;
            score += candidate.StaticScore * 0.08f;
            score -= candidate.Bridge.Count * 22f;
            if (after.OpeningChoices <= targetOpening)
                score += 260f;
            if (after.DirectClearableOuterExits <= targetOpening + 1)
                score += 180f;
            if (candidate.Bridge.Count > 0)
                score += 120f;
            return score;
        }

        static int ApplyOpeningOuterRewireGateInjectionV9(
            LevelDefinition level,
            IRuleset rules,
            int maxRewireOps,
            int maxOuterFlipOps,
            out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null)
                return 0;

            var accepted = new List<string>();
            int acceptedOpCount = 0;

            int rewireOps = ApplyOpeningRewireGateInjectionV8(level, rules, maxRewireOps, maxFallbackGateOps: 0, out string rewireText);
            if (rewireOps > 0)
            {
                accepted.Add(rewireText);
                acceptedOpCount += rewireOps;
            }

            int inwardOps = ApplyOuterHeadInwardPassV9(level, rules, maxOuterFlipOps, out string inwardText);
            if (inwardOps > 0)
            {
                accepted.Add($"outerInward:{inwardText}");
                acceptedOpCount += inwardOps;
            }

            int postRewireOps = ApplyOpeningRewireGateInjectionV8(level, rules, maxRewireOps, maxFallbackGateOps: 0, out string postRewireText);
            if (postRewireOps > 0)
            {
                accepted.Add($"postOpeningRewire:{postRewireText}");
                acceptedOpCount += postRewireOps;
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return acceptedOpCount;
        }

        static int ApplyOuterHeadInwardPassV9(LevelDefinition level, IRuleset rules, int maxAcceptedOps, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null || maxAcceptedOps <= 0)
                return 0;

            var accepted = new List<string>();
            Row current = AnalyzeLevel(new PackSource("outer_inward_current", "", false), 0, 0, level, rules, null, null);
            if (!current.GreedySolved)
                return 0;

            int acceptedFlipCount = 0;
            var attemptedSignatures = new HashSet<string>(StringComparer.Ordinal);
            while (acceptedFlipCount < maxAcceptedOps)
            {
                List<int> candidates = FindDirectOuterArrowIndices(level, rules, includeNonClearable: true);
                if (candidates.Count == 0)
                    break;

                bool found = false;
                float bestScore = float.NegativeInfinity;
                Row bestRow = null;
                AuthoredLevelData bestAuthored = null;
                string bestText = "";
                int bestFlipCount = 0;

                int remaining = maxAcceptedOps - acceptedFlipCount;
                int[] batchSizes =
                {
                    Mathf.Min(12, remaining),
                    Mathf.Min(8, remaining),
                    Mathf.Min(6, remaining),
                    Mathf.Min(4, remaining),
                    Mathf.Min(3, remaining),
                    Mathf.Min(2, remaining),
                    1
                };

                int attempts = 0;
                for (int b = 0; b < batchSizes.Length; b++)
                {
                    int batchSize = batchSizes[b];
                    if (batchSize <= 0)
                        continue;

                    int maxStart = Mathf.Min(candidates.Count, 24);
                    int stride = Mathf.Max(1, batchSize);
                    for (int start = 0; start < maxStart && attempts < 24; start += stride)
                    {
                        List<int> batch = candidates
                            .Skip(start)
                            .Take(batchSize)
                            .Where(i => i >= 0 && i < level.authoredLevel.arrows.Count)
                            .Distinct()
                            .ToList();
                        if (batch.Count == 0)
                            continue;

                        string signature = string.Join("+", batch.OrderBy(i => i));
                        if (!attemptedSignatures.Add(signature))
                            continue;
                        attempts++;

                        AuthoredLevelData beforeAuthored = CloneAuthored(level.authoredLevel);
                        int totalLength = 0;
                        foreach (int arrowIndex in batch)
                        {
                            AuthoredArrowData arrow = level.authoredLevel.arrows[arrowIndex];
                            if (arrow?.indices == null || arrow.indices.Count < 2)
                                continue;
                            totalLength += arrow.indices.Count;
                            arrow.indices.Reverse();
                        }

                        Row next = AnalyzeLevel(new PackSource("outer_inward_candidate", "", false), 0, 0, level, rules, null, null);
                        if (AcceptOuterInwardStepV9(current, next, batch.Count))
                        {
                            float score = ScoreOuterInwardStepV9(current, next, batch.Count, totalLength);
                            if (score > bestScore)
                            {
                                found = true;
                                bestScore = score;
                                bestRow = next;
                                bestAuthored = CloneAuthored(level.authoredLevel);
                                bestFlipCount = batch.Count;
                                bestText =
                                    $"bulkFlip:{batch.Count}:directOuter {current.DirectOuterExits}->{next.DirectOuterExits}:clearOuter {current.DirectClearableOuterExits}->{next.DirectClearableOuterExits}:open {current.OpeningChoices}->{next.OpeningChoices}:avg {F(current.AvgChoices)}->{F(next.AvgChoices)}";
                            }
                        }

                        level.authoredLevel = beforeAuthored;
                    }
                }

                if (!found || bestAuthored == null || bestRow == null)
                    break;

                level.authoredLevel = bestAuthored;
                current = bestRow;
                acceptedFlipCount += bestFlipCount;
                accepted.Add(bestText);
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return acceptedFlipCount;
        }

        static bool AcceptOuterInwardStepV9(Row before, Row after, int attemptedFlipCount)
        {
            if (!after.GreedySolved)
                return false;
            if (after.Chains != before.Chains)
                return false;
            if (after.ArrowTiles != before.ArrowTiles)
                return false;
            if (after.OpeningChoices < 2)
                return false;
            int expectedDrop = Mathf.Clamp(Mathf.CeilToInt(attemptedFlipCount * 0.45f), 1, 6);
            if (after.DirectOuterExits > before.DirectOuterExits - expectedDrop)
                return false;
            if (after.DirectClearableOuterExits > before.DirectClearableOuterExits + 2)
                return false;
            if (after.OpeningChoices > before.OpeningChoices + 2)
                return false;
            if (after.AvgChoices > before.AvgChoices + 1.25f)
                return false;
            if (after.MaxChoices > before.MaxChoices + 8)
                return false;
            if (after.ForcedMoveRatio > before.ForcedMoveRatio + 0.35f)
                return false;
            if (after.DependencyDepthProxy + 1.0f < before.DependencyDepthProxy)
                return false;

            return true;
        }

        static float ScoreOuterInwardStepV9(Row before, Row after, int flipCount, int totalLength)
        {
            float score = 0f;
            score += (before.DirectOuterExits - after.DirectOuterExits) * 260f;
            score += (before.DirectClearableOuterExits - after.DirectClearableOuterExits) * 170f;
            score += (before.OpeningChoices - after.OpeningChoices) * 150f;
            score += (before.AvgChoices - after.AvgChoices) * 150f;
            score += (before.EarlyAvgChoices - after.EarlyAvgChoices) * 80f;
            score += (before.LeakScore - after.LeakScore) * 0.45f;
            score += flipCount * 28f;
            score += Mathf.Min(totalLength, 160) * 1.5f;
            if (after.OpeningChoices > before.OpeningChoices)
                score -= 180f;
            if (after.DirectClearableOuterExits > before.DirectClearableOuterExits)
                score -= 240f;
            return score;
        }

        static int ApplyOuterExitEndpointHardeningV10(LevelDefinition level, IRuleset rules, bool includeBaseV9, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null)
                return 0;

            var accepted = new List<string>();
            int acceptedOpCount = 0;

            if (includeBaseV9)
            {
                int v9Ops = ApplyOpeningOuterRewireGateInjectionV9(level, rules, maxRewireOps: 3, maxOuterFlipOps: 18, out string v9Text);
                if (v9Ops > 0)
                {
                    accepted.Add($"v9Base:{v9Text}");
                    acceptedOpCount += v9Ops;
                }
            }

            int rerouteOps = ApplyOuterEndpointReroutePassV10(level, rules, maxAcceptedOps: 6, out string rerouteText);
            if (rerouteOps > 0)
            {
                accepted.Add($"endpointReroute:{rerouteText}");
                acceptedOpCount += rerouteOps;
            }

            int trimOps = ApplyOuterEndpointTrimPassV10(level, rules, maxAcceptedOps: 4, maxTilesRemoved: 18, out string trimText);
            if (trimOps > 0)
            {
                accepted.Add($"endpointTrim:{trimText}");
                acceptedOpCount += trimOps;
            }

            int secondRerouteOps = ApplyOuterEndpointReroutePassV10(level, rules, maxAcceptedOps: 3, out string secondRerouteText);
            if (secondRerouteOps > 0)
            {
                accepted.Add($"endpointReroute2:{secondRerouteText}");
                acceptedOpCount += secondRerouteOps;
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return acceptedOpCount;
        }

        static int ApplyOuterEndpointReroutePassV10(LevelDefinition level, IRuleset rules, int maxAcceptedOps, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null || maxAcceptedOps <= 0)
                return 0;

            var accepted = new List<string>();
            Row current = AnalyzeLevel(new PackSource("endpoint_reroute_current", "", false), 0, 0, level, rules, null, null);
            if (!current.GreedySolved)
                return 0;

            for (int op = 0; op < maxAcceptedOps; op++)
            {
                List<int> candidates = FindDirectOuterArrowIndices(level, rules, includeNonClearable: true);
                if (candidates.Count == 0)
                    break;

                bool found = false;
                float bestScore = float.NegativeInfinity;
                Row bestRow = null;
                AuthoredLevelData bestAuthored = null;
                string bestText = "";
                AuthoredLevelData iterationAuthored = CloneAuthored(level.authoredLevel);
                PressureSnapshot quickBefore = AnalyzePressureQuick(level, rules);
                int attempts = 0;

                for (int c = 0; c < candidates.Count && c < 10 && attempts < 18; c++)
                {
                    int arrowIndex = candidates[c];
                    if (arrowIndex < 0 || arrowIndex >= level.authoredLevel.arrows.Count)
                        continue;

                    AuthoredArrowData sourceArrow = level.authoredLevel.arrows[arrowIndex];
                    if (sourceArrow?.indices == null || sourceArrow.indices.Count < 3)
                        continue;

                    for (int reverse = 0; reverse < 2 && attempts < 18; reverse++)
                    {
                        List<int> oriented = OrientedCopy(sourceArrow.indices, reverse != 0);
                        if (oriented.Count < 3)
                            continue;

                        int oldHead = oriented[0];
                        int neck = oriented[1];
                        int width = Mathf.Max(1, level.authoredLevel.width);
                        int height = Mathf.Max(1, level.authoredLevel.height);
                        Vector2Int neckPos = IndexToPos(neck, width);
                        Vector2Int oldHeadPos = IndexToPos(oldHead, width);
                        Vector2Int oldForward = oldHeadPos - neckPos;
                        var dirs = new[]
                        {
                            new Vector2Int(oldForward.y, -oldForward.x),
                            new Vector2Int(-oldForward.y, oldForward.x),
                            -oldForward
                        };

                            foreach (Vector2Int dir in dirs)
                            {
                            if (!IsCardinal(dir))
                                continue;

                            Vector2Int newHeadPos = neckPos + dir;
                            if ((uint)newHeadPos.x >= (uint)width || (uint)newHeadPos.y >= (uint)height)
                                continue;
                            int newHead = newHeadPos.y * width + newHeadPos.x;
                            if (newHead == oldHead || oriented.Contains(newHead))
                                continue;

                            level.authoredLevel = CloneAuthored(iterationAuthored);
                            if (!AuthoredLevelBuilder.TryBuildBoard(level.authoredLevel, out BoardState board, out _))
                                continue;
                            if (board.tiles[newHead].type != TileType.Empty)
                                continue;

                            AuthoredArrowData arrow = level.authoredLevel.arrows[arrowIndex];
                            arrow.indices = OrientedCopy(arrow.indices, reverse != 0);
                            arrow.indices[0] = newHead;
                            if (!IsUniqueContinuousPath(arrow.indices, width))
                                continue;

                            attempts++;
                            PressureSnapshot quickAfter = AnalyzePressureQuick(level, rules);
                            if (quickAfter.DirectOuterExits > quickBefore.DirectOuterExits - 1)
                                continue;
                            if (quickAfter.DirectClearableOuterExits > quickBefore.DirectClearableOuterExits + 1)
                                continue;
                            if (quickAfter.PressureScore > quickBefore.PressureScore + 18f)
                                continue;

                            Row next = AnalyzeLevel(new PackSource("endpoint_reroute_candidate", "", false), 0, 0, level, rules, null, null);
                            if (!AcceptOuterEndpointStepV10(current, next))
                                continue;

                            float score = ScoreOuterEndpointStepV10(current, next, removedTiles: 0);
                            if (newHeadPos.x != 0 && newHeadPos.y != 0 && newHeadPos.x != width - 1 && newHeadPos.y != height - 1)
                                score += 180f;
                            if (reverse != 0)
                                score += 40f;
                            if (score <= bestScore)
                                continue;

                            found = true;
                            bestScore = score;
                            bestRow = next;
                            bestAuthored = CloneAuthored(level.authoredLevel);
                            bestText =
                                $"reroute:{arrowIndex}:rev{reverse}:head {oldHead}->{newHead}:directOuter {current.DirectOuterExits}->{next.DirectOuterExits}:clearOuter {current.DirectClearableOuterExits}->{next.DirectClearableOuterExits}:open {current.OpeningChoices}->{next.OpeningChoices}:avg {F(current.AvgChoices)}->{F(next.AvgChoices)}";
                        }
                    }
                }

                level.authoredLevel = iterationAuthored;
                if (!found || bestAuthored == null || bestRow == null)
                    break;

                level.authoredLevel = bestAuthored;
                current = bestRow;
                accepted.Add(bestText);
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return accepted.Count;
        }

        static int ApplyOuterEndpointTrimPassV10(LevelDefinition level, IRuleset rules, int maxAcceptedOps, int maxTilesRemoved, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null || maxAcceptedOps <= 0 || maxTilesRemoved <= 0)
                return 0;

            var accepted = new List<string>();
            Row current = AnalyzeLevel(new PackSource("endpoint_trim_current", "", false), 0, 0, level, rules, null, null);
            if (!current.GreedySolved)
                return 0;

            int totalRemoved = 0;
            for (int op = 0; op < maxAcceptedOps && totalRemoved < maxTilesRemoved; op++)
            {
                List<int> candidates = FindDirectOuterArrowIndices(level, rules, includeNonClearable: true);
                if (candidates.Count == 0)
                    break;

                bool found = false;
                float bestScore = float.NegativeInfinity;
                Row bestRow = null;
                AuthoredLevelData bestAuthored = null;
                string bestText = "";
                int bestRemoved = 0;
                AuthoredLevelData iterationAuthored = CloneAuthored(level.authoredLevel);
                PressureSnapshot quickBefore = AnalyzePressureQuick(level, rules);
                int attempts = 0;

                for (int c = 0; c < candidates.Count && c < 10 && attempts < 22; c++)
                {
                    int arrowIndex = candidates[c];
                    if (arrowIndex < 0 || arrowIndex >= level.authoredLevel.arrows.Count)
                        continue;

                    AuthoredArrowData sourceArrow = level.authoredLevel.arrows[arrowIndex];
                    if (sourceArrow?.indices == null || sourceArrow.indices.Count < 5)
                        continue;

                    for (int reverse = 0; reverse < 2 && attempts < 22; reverse++)
                    {
                        List<int> oriented = OrientedCopy(sourceArrow.indices, reverse != 0);
                        int maxSkip = Mathf.Min(6, oriented.Count - 3);
                        for (int skip = 1; skip <= maxSkip && attempts < 22; skip++)
                        {
                            if (totalRemoved + skip > maxTilesRemoved)
                                continue;

                            var trimmed = oriented.Skip(skip).ToList();
                            if (trimmed.Count < 3)
                                continue;

                            int width = Mathf.Max(1, level.authoredLevel.width);
                            if (!IsUniqueContinuousPath(trimmed, width))
                                continue;

                            level.authoredLevel = CloneAuthored(iterationAuthored);
                            AuthoredArrowData arrow = level.authoredLevel.arrows[arrowIndex];
                            arrow.indices = trimmed;
                            attempts++;

                            PressureSnapshot quickAfter = AnalyzePressureQuick(level, rules);
                            if (quickAfter.DirectOuterExits > quickBefore.DirectOuterExits - 1)
                                continue;
                            if (quickAfter.DirectClearableOuterExits > quickBefore.DirectClearableOuterExits + 1)
                                continue;
                            if (quickAfter.PressureScore > quickBefore.PressureScore + 18f)
                                continue;

                            Row next = AnalyzeLevel(new PackSource("endpoint_trim_candidate", "", false), 0, 0, level, rules, null, null);
                            if (!AcceptOuterEndpointStepV10(current, next))
                                continue;
                            if (next.ArrowTiles != current.ArrowTiles - skip)
                                continue;

                            float score = ScoreOuterEndpointStepV10(current, next, skip);
                            score -= skip * 60f;
                            if (reverse != 0)
                                score += 35f;
                            if (score <= bestScore)
                                continue;

                            found = true;
                            bestScore = score;
                            bestRow = next;
                            bestAuthored = CloneAuthored(level.authoredLevel);
                            bestRemoved = skip;
                            bestText =
                                $"trim:{arrowIndex}:rev{reverse}:skip {skip}:directOuter {current.DirectOuterExits}->{next.DirectOuterExits}:clearOuter {current.DirectClearableOuterExits}->{next.DirectClearableOuterExits}:open {current.OpeningChoices}->{next.OpeningChoices}:avg {F(current.AvgChoices)}->{F(next.AvgChoices)}";
                        }
                    }
                }

                level.authoredLevel = iterationAuthored;
                if (!found || bestAuthored == null || bestRow == null)
                    break;

                level.authoredLevel = bestAuthored;
                current = bestRow;
                totalRemoved += bestRemoved;
                accepted.Add(bestText);
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return accepted.Count;
        }

        static bool AcceptOuterEndpointStepV10(Row before, Row after)
        {
            if (!after.GreedySolved)
                return false;
            if (after.Chains > before.Chains)
                return false;
            if (after.ArrowTiles > before.ArrowTiles)
                return false;
            if (after.OpeningChoices < 2)
                return false;
            if (after.DirectOuterExits > before.DirectOuterExits - 1)
                return false;
            if (after.DirectClearableOuterExits > before.DirectClearableOuterExits + 1)
                return false;
            if (after.OpeningChoices > before.OpeningChoices + 2)
                return false;
            if (after.AvgChoices > before.AvgChoices + 1.2f)
                return false;
            if (after.MaxChoices > before.MaxChoices + 8)
                return false;
            if (after.ForcedMoveRatio > before.ForcedMoveRatio + 0.38f)
                return false;
            if (after.DependencyDepthProxy + 1.2f < before.DependencyDepthProxy)
                return false;

            return true;
        }

        static float ScoreOuterEndpointStepV10(Row before, Row after, int removedTiles)
        {
            float score = 0f;
            score += (before.DirectOuterExits - after.DirectOuterExits) * 520f;
            score += (before.DirectClearableOuterExits - after.DirectClearableOuterExits) * 360f;
            score += (before.OpeningChoices - after.OpeningChoices) * 180f;
            score += (before.AvgChoices - after.AvgChoices) * 180f;
            score += (before.LeakScore - after.LeakScore) * 0.75f;
            score += (before.BoundaryStraightOuterExits - after.BoundaryStraightOuterExits) * 210f;
            score += (before.EdgeShortDirectOuterExits - after.EdgeShortDirectOuterExits) * 160f;
            score -= removedTiles * 90f;
            if (after.DirectOuterExits <= 14)
                score += 650f;
            if (after.DirectOuterExits <= 10)
                score += 900f;
            if (after.DirectClearableOuterExits > before.DirectClearableOuterExits)
                score -= 340f;
            if (after.OpeningChoices > before.OpeningChoices)
                score -= 220f;
            return score;
        }

        static int ApplyMultiLayerOuterExitHardeningV11(LevelDefinition level, IRuleset rules, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null)
                return 0;

            var accepted = new List<string>();
            int acceptedOpCount = 0;

            int flipOps = ApplyPeelLayerOrientationFlipPassV11(level, rules, maxAcceptedOps: 8, out string flipText);
            if (flipOps > 0)
            {
                accepted.Add($"peelOrientationFlip:{flipText}");
                acceptedOpCount += flipOps;
            }

            int rerouteOps = ApplyPeelLayerEndpointReroutePassV11(level, rules, maxAcceptedOps: 7, out string rerouteText);
            if (rerouteOps > 0)
            {
                accepted.Add($"peelEndpointReroute:{rerouteText}");
                acceptedOpCount += rerouteOps;
            }

            int trimOps = ApplyPeelLayerEndpointTrimPassV11(level, rules, maxAcceptedOps: 4, maxTilesRemoved: 14, out string trimText);
            if (trimOps > 0)
            {
                accepted.Add($"peelEndpointTrim:{trimText}");
                acceptedOpCount += trimOps;
            }

            int secondRerouteOps = ApplyPeelLayerEndpointReroutePassV11(level, rules, maxAcceptedOps: 4, out string secondRerouteText);
            if (secondRerouteOps > 0)
            {
                accepted.Add($"peelEndpointReroute2:{secondRerouteText}");
                acceptedOpCount += secondRerouteOps;
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return acceptedOpCount;
        }

        static int ApplyPeelLayerOrientationFlipPassV11(LevelDefinition level, IRuleset rules, int maxAcceptedOps, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null || maxAcceptedOps <= 0)
                return 0;

            var accepted = new List<string>();
            Row current = AnalyzeLevel(new PackSource("peel_orientation_flip_current", "", false), 0, 0, level, rules, null, null);
            if (!current.GreedySolved)
                return 0;

            for (int op = 0; op < maxAcceptedOps; op++)
            {
                PeelOuterStatsV11 peelBefore = AnalyzePeelOuterStatsV11(level, rules, maxWaves: 4, maxRemovedChains: 96);
                List<int> candidates = GetPeelOuterTargetArrowIndicesV11(peelBefore, takeLimit: 18);
                if (candidates.Count == 0)
                    break;

                bool found = false;
                float bestScore = float.NegativeInfinity;
                Row bestRow = null;
                PeelOuterStatsV11 bestPeel = null;
                AuthoredLevelData bestAuthored = null;
                string bestText = "";
                AuthoredLevelData iterationAuthored = CloneAuthored(level.authoredLevel);
                PressureSnapshot quickBefore = AnalyzePressureQuick(level, rules);
                int attempts = 0;

                for (int c = 0; c < candidates.Count && c < 16 && attempts < 24; c++)
                {
                    int arrowIndex = candidates[c];
                    if (arrowIndex < 0 || arrowIndex >= level.authoredLevel.arrows.Count)
                        continue;

                    AuthoredArrowData sourceArrow = level.authoredLevel.arrows[arrowIndex];
                    if (sourceArrow?.indices == null || sourceArrow.indices.Count < 3)
                        continue;

                    int width = Mathf.Max(1, level.authoredLevel.width);
                    var reversed = OrientedCopy(sourceArrow.indices, true);
                    if (!IsUniqueContinuousPath(reversed, width))
                        continue;

                    level.authoredLevel = CloneAuthored(iterationAuthored);
                    level.authoredLevel.arrows[arrowIndex].indices = reversed;
                    attempts++;

                    PressureSnapshot quickAfter = AnalyzePressureQuick(level, rules);
                    if (quickAfter.DirectOuterExits > quickBefore.DirectOuterExits + 1)
                        continue;
                    if (quickAfter.DirectClearableOuterExits > quickBefore.DirectClearableOuterExits + 1)
                        continue;
                    if (quickAfter.PressureScore > quickBefore.PressureScore + 30f)
                        continue;

                    Row next = AnalyzeLevel(new PackSource("peel_orientation_flip_candidate", "", false), 0, 0, level, rules, null, null);
                    PeelOuterStatsV11 peelAfter = AnalyzePeelOuterStatsV11(level, rules, maxWaves: 4, maxRemovedChains: 96);
                    if (!AcceptPeelLayerEndpointStepV11(current, next, peelBefore, peelAfter, removedTiles: 0))
                        continue;

                    float score = ScorePeelLayerEndpointStepV11(current, next, peelBefore, peelAfter, removedTiles: 0) + 110f;
                    if (score <= bestScore)
                        continue;

                    found = true;
                    bestScore = score;
                    bestRow = next;
                    bestPeel = peelAfter;
                    bestAuthored = CloneAuthored(level.authoredLevel);
                    bestText =
                        $"flip:{arrowIndex}:directOuter {current.DirectOuterExits}->{next.DirectOuterExits}:peelOuter {peelBefore.TotalOuter}->{peelAfter.TotalOuter}:futurePeel {peelBefore.FutureOuter}->{peelAfter.FutureOuter}:peelRisk {peelBefore.RiskScore}->{peelAfter.RiskScore}:open {current.OpeningChoices}->{next.OpeningChoices}:avg {F(current.AvgChoices)}->{F(next.AvgChoices)}";
                }

                level.authoredLevel = iterationAuthored;
                if (!found || bestAuthored == null || bestRow == null || bestPeel == null)
                    break;

                level.authoredLevel = bestAuthored;
                current = bestRow;
                accepted.Add(bestText);
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return accepted.Count;
        }

        static int ApplyPeelLayerEndpointReroutePassV11(LevelDefinition level, IRuleset rules, int maxAcceptedOps, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null || maxAcceptedOps <= 0)
                return 0;

            var accepted = new List<string>();
            Row current = AnalyzeLevel(new PackSource("peel_endpoint_reroute_current", "", false), 0, 0, level, rules, null, null);
            if (!current.GreedySolved)
                return 0;

            for (int op = 0; op < maxAcceptedOps; op++)
            {
                PeelOuterStatsV11 peelBefore = AnalyzePeelOuterStatsV11(level, rules, maxWaves: 4, maxRemovedChains: 96);
                List<int> candidates = GetPeelOuterTargetArrowIndicesV11(peelBefore, takeLimit: 14);
                if (candidates.Count == 0)
                    break;

                bool found = false;
                float bestScore = float.NegativeInfinity;
                Row bestRow = null;
                PeelOuterStatsV11 bestPeel = null;
                AuthoredLevelData bestAuthored = null;
                string bestText = "";
                AuthoredLevelData iterationAuthored = CloneAuthored(level.authoredLevel);
                PressureSnapshot quickBefore = AnalyzePressureQuick(level, rules);
                int attempts = 0;

                for (int c = 0; c < candidates.Count && c < 12 && attempts < 30; c++)
                {
                    int arrowIndex = candidates[c];
                    if (arrowIndex < 0 || arrowIndex >= level.authoredLevel.arrows.Count)
                        continue;

                    AuthoredArrowData sourceArrow = level.authoredLevel.arrows[arrowIndex];
                    if (sourceArrow?.indices == null || sourceArrow.indices.Count < 3)
                        continue;

                    for (int reverse = 0; reverse < 2 && attempts < 30; reverse++)
                    {
                        List<int> oriented = OrientedCopy(sourceArrow.indices, reverse != 0);
                        if (oriented.Count < 3)
                            continue;

                        int oldHead = oriented[0];
                        int neck = oriented[1];
                        int width = Mathf.Max(1, level.authoredLevel.width);
                        int height = Mathf.Max(1, level.authoredLevel.height);
                        Vector2Int neckPos = IndexToPos(neck, width);
                        Vector2Int oldHeadPos = IndexToPos(oldHead, width);
                        Vector2Int oldForward = oldHeadPos - neckPos;
                        var dirs = new[]
                        {
                            new Vector2Int(oldForward.y, -oldForward.x),
                            new Vector2Int(-oldForward.y, oldForward.x),
                            -oldForward
                        };

                        foreach (Vector2Int dir in dirs)
                        {
                            if (attempts >= 30)
                                break;
                            if (!IsCardinal(dir))
                                continue;

                            Vector2Int newHeadPos = neckPos + dir;
                            if ((uint)newHeadPos.x >= (uint)width || (uint)newHeadPos.y >= (uint)height)
                                continue;
                            int newHead = newHeadPos.y * width + newHeadPos.x;
                            if (newHead == oldHead || oriented.Contains(newHead))
                                continue;

                            level.authoredLevel = CloneAuthored(iterationAuthored);
                            if (!AuthoredLevelBuilder.TryBuildBoard(level.authoredLevel, out BoardState board, out _))
                                continue;
                            if (board.tiles[newHead].type != TileType.Empty)
                                continue;

                            AuthoredArrowData arrow = level.authoredLevel.arrows[arrowIndex];
                            arrow.indices = OrientedCopy(arrow.indices, reverse != 0);
                            arrow.indices[0] = newHead;
                            if (!IsUniqueContinuousPath(arrow.indices, width))
                                continue;

                            attempts++;
                            PressureSnapshot quickAfter = AnalyzePressureQuick(level, rules);
                            if (quickAfter.DirectOuterExits > quickBefore.DirectOuterExits + 1)
                                continue;
                            if (quickAfter.DirectClearableOuterExits > quickBefore.DirectClearableOuterExits + 1)
                                continue;
                            if (quickAfter.PressureScore > quickBefore.PressureScore + 28f)
                                continue;

                            Row next = AnalyzeLevel(new PackSource("peel_endpoint_reroute_candidate", "", false), 0, 0, level, rules, null, null);
                            PeelOuterStatsV11 peelAfter = AnalyzePeelOuterStatsV11(level, rules, maxWaves: 4, maxRemovedChains: 96);
                            if (!AcceptPeelLayerEndpointStepV11(current, next, peelBefore, peelAfter, removedTiles: 0))
                                continue;

                            float score = ScorePeelLayerEndpointStepV11(current, next, peelBefore, peelAfter, removedTiles: 0);
                            if (newHeadPos.x != 0 && newHeadPos.y != 0 && newHeadPos.x != width - 1 && newHeadPos.y != height - 1)
                                score += 160f;
                            if (reverse != 0)
                                score += 35f;
                            if (score <= bestScore)
                                continue;

                            found = true;
                            bestScore = score;
                            bestRow = next;
                            bestPeel = peelAfter;
                            bestAuthored = CloneAuthored(level.authoredLevel);
                            bestText =
                                $"reroute:{arrowIndex}:rev{reverse}:head {oldHead}->{newHead}:directOuter {current.DirectOuterExits}->{next.DirectOuterExits}:peelOuter {peelBefore.TotalOuter}->{peelAfter.TotalOuter}:futurePeel {peelBefore.FutureOuter}->{peelAfter.FutureOuter}:peelRisk {peelBefore.RiskScore}->{peelAfter.RiskScore}:open {current.OpeningChoices}->{next.OpeningChoices}:avg {F(current.AvgChoices)}->{F(next.AvgChoices)}";
                        }
                    }
                }

                level.authoredLevel = iterationAuthored;
                if (!found || bestAuthored == null || bestRow == null || bestPeel == null)
                    break;

                level.authoredLevel = bestAuthored;
                current = bestRow;
                accepted.Add(bestText);
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return accepted.Count;
        }

        static int ApplyPeelLayerEndpointTrimPassV11(LevelDefinition level, IRuleset rules, int maxAcceptedOps, int maxTilesRemoved, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null || maxAcceptedOps <= 0 || maxTilesRemoved <= 0)
                return 0;

            var accepted = new List<string>();
            Row current = AnalyzeLevel(new PackSource("peel_endpoint_trim_current", "", false), 0, 0, level, rules, null, null);
            if (!current.GreedySolved)
                return 0;

            int totalRemoved = 0;
            for (int op = 0; op < maxAcceptedOps && totalRemoved < maxTilesRemoved; op++)
            {
                PeelOuterStatsV11 peelBefore = AnalyzePeelOuterStatsV11(level, rules, maxWaves: 4, maxRemovedChains: 96);
                List<int> candidates = GetPeelOuterTargetArrowIndicesV11(peelBefore, takeLimit: 14);
                if (candidates.Count == 0)
                    break;

                bool found = false;
                float bestScore = float.NegativeInfinity;
                Row bestRow = null;
                PeelOuterStatsV11 bestPeel = null;
                AuthoredLevelData bestAuthored = null;
                string bestText = "";
                int bestRemoved = 0;
                AuthoredLevelData iterationAuthored = CloneAuthored(level.authoredLevel);
                PressureSnapshot quickBefore = AnalyzePressureQuick(level, rules);
                int attempts = 0;

                for (int c = 0; c < candidates.Count && c < 12 && attempts < 34; c++)
                {
                    int arrowIndex = candidates[c];
                    if (arrowIndex < 0 || arrowIndex >= level.authoredLevel.arrows.Count)
                        continue;

                    AuthoredArrowData sourceArrow = level.authoredLevel.arrows[arrowIndex];
                    if (sourceArrow?.indices == null || sourceArrow.indices.Count < 5)
                        continue;

                    for (int reverse = 0; reverse < 2 && attempts < 34; reverse++)
                    {
                        List<int> oriented = OrientedCopy(sourceArrow.indices, reverse != 0);
                        int maxSkip = Mathf.Min(5, oriented.Count - 3);
                        for (int skip = 1; skip <= maxSkip && attempts < 34; skip++)
                        {
                            if (totalRemoved + skip > maxTilesRemoved)
                                continue;

                            var trimmed = oriented.Skip(skip).ToList();
                            if (trimmed.Count < 3)
                                continue;

                            int width = Mathf.Max(1, level.authoredLevel.width);
                            if (!IsUniqueContinuousPath(trimmed, width))
                                continue;

                            level.authoredLevel = CloneAuthored(iterationAuthored);
                            AuthoredArrowData arrow = level.authoredLevel.arrows[arrowIndex];
                            arrow.indices = trimmed;
                            attempts++;

                            PressureSnapshot quickAfter = AnalyzePressureQuick(level, rules);
                            if (quickAfter.DirectOuterExits > quickBefore.DirectOuterExits + 1)
                                continue;
                            if (quickAfter.DirectClearableOuterExits > quickBefore.DirectClearableOuterExits + 1)
                                continue;
                            if (quickAfter.PressureScore > quickBefore.PressureScore + 28f)
                                continue;

                            Row next = AnalyzeLevel(new PackSource("peel_endpoint_trim_candidate", "", false), 0, 0, level, rules, null, null);
                            if (next.ArrowTiles != current.ArrowTiles - skip)
                                continue;

                            PeelOuterStatsV11 peelAfter = AnalyzePeelOuterStatsV11(level, rules, maxWaves: 4, maxRemovedChains: 96);
                            if (!AcceptPeelLayerEndpointStepV11(current, next, peelBefore, peelAfter, removedTiles: skip))
                                continue;

                            float score = ScorePeelLayerEndpointStepV11(current, next, peelBefore, peelAfter, skip);
                            score -= skip * 55f;
                            if (reverse != 0)
                                score += 30f;
                            if (score <= bestScore)
                                continue;

                            found = true;
                            bestScore = score;
                            bestRow = next;
                            bestPeel = peelAfter;
                            bestAuthored = CloneAuthored(level.authoredLevel);
                            bestRemoved = skip;
                            bestText =
                                $"trim:{arrowIndex}:rev{reverse}:skip {skip}:directOuter {current.DirectOuterExits}->{next.DirectOuterExits}:peelOuter {peelBefore.TotalOuter}->{peelAfter.TotalOuter}:futurePeel {peelBefore.FutureOuter}->{peelAfter.FutureOuter}:peelRisk {peelBefore.RiskScore}->{peelAfter.RiskScore}:open {current.OpeningChoices}->{next.OpeningChoices}:avg {F(current.AvgChoices)}->{F(next.AvgChoices)}";
                        }
                    }
                }

                level.authoredLevel = iterationAuthored;
                if (!found || bestAuthored == null || bestRow == null || bestPeel == null)
                    break;

                level.authoredLevel = bestAuthored;
                current = bestRow;
                totalRemoved += bestRemoved;
                accepted.Add(bestText);
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return accepted.Count;
        }

        static bool AcceptPeelLayerEndpointStepV11(Row before, Row after, PeelOuterStatsV11 beforePeel, PeelOuterStatsV11 afterPeel, int removedTiles)
        {
            if (!after.GreedySolved)
                return false;
            if (after.Chains > before.Chains)
                return false;
            if (after.ArrowTiles > before.ArrowTiles)
                return false;
            if (after.OpeningChoices < 2)
                return false;
            if (after.DirectOuterExits > before.DirectOuterExits + 1)
                return false;
            if (after.DirectClearableOuterExits > before.DirectClearableOuterExits + 1)
                return false;
            if (after.OpeningChoices > before.OpeningChoices + 1)
                return false;
            if (after.AvgChoices > before.AvgChoices + 0.9f)
                return false;
            if (after.MaxChoices > before.MaxChoices + 6)
                return false;
            if (after.ForcedMoveRatio > before.ForcedMoveRatio + 0.35f)
                return false;
            if (after.DependencyDepthProxy + 1.4f < before.DependencyDepthProxy)
                return false;
            if (afterPeel.FutureOuter > beforePeel.FutureOuter + 1)
                return false;
            if (afterPeel.TotalOuter > beforePeel.TotalOuter)
                return false;

            int directDrop = before.DirectOuterExits - after.DirectOuterExits;
            int totalDrop = beforePeel.TotalOuter - afterPeel.TotalOuter;
            int futureDrop = beforePeel.FutureOuter - afterPeel.FutureOuter;
            int riskDrop = beforePeel.RiskScore - afterPeel.RiskScore;
            if (futureDrop < 1 && totalDrop < 2 && directDrop < 2 && riskDrop < 220)
                return false;
            if (removedTiles > 0 && totalDrop <= 0 && riskDrop < 260)
                return false;

            return true;
        }

        static float ScorePeelLayerEndpointStepV11(Row before, Row after, PeelOuterStatsV11 beforePeel, PeelOuterStatsV11 afterPeel, int removedTiles)
        {
            float score = 0f;
            score += (beforePeel.RiskScore - afterPeel.RiskScore) * 1.35f;
            score += (beforePeel.FutureOuter - afterPeel.FutureOuter) * 620f;
            score += (beforePeel.TotalOuter - afterPeel.TotalOuter) * 430f;
            score += (beforePeel.Wave0Outer - afterPeel.Wave0Outer) * 260f;
            score += (before.DirectOuterExits - after.DirectOuterExits) * 300f;
            score += (before.DirectClearableOuterExits - after.DirectClearableOuterExits) * 220f;
            score += (before.OpeningChoices - after.OpeningChoices) * 140f;
            score += (before.AvgChoices - after.AvgChoices) * 210f;
            score += (before.LeakScore - after.LeakScore) * 0.35f;
            score -= removedTiles * 85f;
            if (afterPeel.FutureOuter <= Mathf.Max(3, beforePeel.FutureOuter - 4))
                score += 650f;
            if (afterPeel.TotalOuter <= Mathf.Max(6, beforePeel.TotalOuter - 6))
                score += 520f;
            if (after.OpeningChoices > before.OpeningChoices)
                score -= 220f;
            if (after.AvgChoices > before.AvgChoices)
                score -= 180f;
            return score;
        }

        static PeelOuterStatsV11 AnalyzePeelOuterStatsV11(LevelDefinition level, IRuleset rules, int maxWaves, int maxRemovedChains)
        {
            var stats = new PeelOuterStatsV11();
            AuthoredLevelData authored = level?.authoredLevel;
            if (authored?.arrows == null || !AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out _))
                return stats;

            Dictionary<int, int> chainToArrow = BuildChainIdToArrowIndex(authored);
            BoardState state = CloneBoard(board);
            var removedArrows = new HashSet<int>();
            int finalChoices = 0;

            for (int wave = 0; wave < maxWaves && stats.RemovedCount < maxRemovedChains; wave++)
            {
                List<MoveCandidate> candidates = BuildMoveCandidates(state, rules)
                    .OrderByDescending(c => c.MoveScore)
                    .ThenBy(c => c.ChainId)
                    .ToList();

                if (wave == 0)
                    stats.InitialChoices = candidates.Count;
                finalChoices = candidates.Count;
                if (candidates.Count == 0)
                    break;

                int removedThisWave = 0;
                for (int c = 0; c < candidates.Count && stats.RemovedCount < maxRemovedChains; c++)
                {
                    MoveCandidate candidate = candidates[c];
                    if (!chainToArrow.TryGetValue(candidate.ChainId, out int arrowIndex))
                        continue;
                    if (removedArrows.Contains(arrowIndex))
                        continue;
                    if (!state.InBounds(candidate.Move.pos.x, candidate.Move.pos.y))
                        continue;
                    if (state.tiles[state.Index(candidate.Move.pos.x, candidate.Move.pos.y)].type != TileType.Arrow)
                        continue;

                    if (TryBuildPeelOuterCandidateV11(authored, board, state, arrowIndex, wave, candidates.Count, candidate, out PeelOuterCandidateV11 outerCandidate))
                    {
                        stats.Candidates.Add(outerCandidate);
                        stats.TotalOuter++;
                        if (wave == 0)
                            stats.Wave0Outer++;
                        else
                            stats.FutureOuter++;
                        if (outerCandidate.InitialDirectOuter)
                        {
                            stats.PersistentOuter++;
                            stats.PersistentRiskScore += outerCandidate.Score;
                            if (wave > 0)
                                stats.PersistentFutureOuter++;
                        }
                        else
                        {
                            stats.NewlyExposedOuter++;
                            stats.NewlyExposedRiskScore += outerCandidate.Score;
                        }
                        if (outerCandidate.EdgeShort)
                            stats.EdgeShortOuter++;
                        if (outerCandidate.BoundaryStraight)
                            stats.BoundaryStraightOuter++;
                        if (outerCandidate.EmptyRayLength >= 4)
                            stats.LongRayOuter++;
                        stats.RiskScore += outerCandidate.Score;
                    }

                    if (!rules.TryApplyMove(state, candidate.Move, out MoveDelta delta))
                        continue;

                    int cleared = CountCleared(delta);
                    if (cleared <= 0)
                        continue;

                    removedArrows.Add(arrowIndex);
                    stats.RemovedCount++;
                    removedThisWave++;
                }

                stats.Waves = wave + 1;
                if (removedThisWave == 0)
                    break;
            }

            finalChoices = BuildMoveCandidates(state, rules).Count;
            stats.FinalChoices = finalChoices;
            return stats;
        }

        static bool TryBuildPeelOuterCandidateV11(
            AuthoredLevelData authored,
            BoardState initialState,
            BoardState state,
            int arrowIndex,
            int wave,
            int waveChoices,
            MoveCandidate moveCandidate,
            out PeelOuterCandidateV11 candidate)
        {
            candidate = default;
            if (authored?.arrows == null || arrowIndex < 0 || arrowIndex >= authored.arrows.Count)
                return false;

            AuthoredArrowData arrow = authored.arrows[arrowIndex];
            if (arrow?.indices == null || arrow.indices.Count < 2)
                return false;

            int width = Mathf.Max(1, authored.width);
            int height = Mathf.Max(1, authored.height);
            int headIndex = arrow.indices[0];
            Vector2Int head = IndexToPos(headIndex, width);
            if ((uint)head.x >= (uint)width || (uint)head.y >= (uint)height)
                return false;
            if (!state.InBounds(head.x, head.y))
                return false;
            if (state.tiles[state.Index(head.x, head.y)].type != TileType.Arrow)
                return false;

            Vector2Int neck = IndexToPos(arrow.indices[1], width);
            Vector2Int forward = head - neck;
            if (!IsCardinal(forward))
                return false;
            if (!TraceHeadRayToExit(state, head, forward, out int emptyRayLength))
                return false;
            bool initialDirectOuter = initialState != null && TraceHeadRayToExit(initialState, head, forward, out _);

            bool edgeHead = head.x == 0 || head.y == 0 || head.x == width - 1 || head.y == height - 1;
            bool edgeTouch = false;
            for (int i = 0; i < arrow.indices.Count; i++)
            {
                Vector2Int pos = IndexToPos(arrow.indices[i], width);
                if (pos.x == 0 || pos.y == 0 || pos.x == width - 1 || pos.y == height - 1)
                {
                    edgeTouch = true;
                    break;
                }
            }

            bool straight = IsStraight(arrow, width);
            bool edgeShort = edgeTouch && arrow.indices.Count <= 4;
            bool boundaryStraight = edgeTouch && straight && arrow.indices.Count <= 7;
            int score = 0;
            score += wave == 0 ? 900 : 1600;
            score += Mathf.Max(0, 4 - wave) * 120;
            score += Mathf.Min(waveChoices, 30) * 12;
            score += edgeHead ? 360 : 0;
            score += edgeTouch ? 260 : 0;
            score += edgeShort ? 360 : 0;
            score += boundaryStraight ? 300 : 0;
            score += Mathf.Min(emptyRayLength, 12) * 28;
            score += Mathf.Max(0, 7 - arrow.indices.Count) * 35;
            score -= Mathf.Min(arrow.indices.Count, 30) * 8;

            candidate = new PeelOuterCandidateV11
            {
                ArrowIndex = arrowIndex,
                ChainId = moveCandidate.ChainId,
                Wave = wave,
                WaveChoices = waveChoices,
                ClearCount = moveCandidate.ClearCount,
                ChainLength = moveCandidate.ChainLength,
                EmptyRayLength = emptyRayLength,
                EdgeHead = edgeHead,
                EdgeTouch = edgeTouch,
                EdgeShort = edgeShort,
                BoundaryStraight = boundaryStraight,
                InitialDirectOuter = initialDirectOuter,
                Score = score
            };
            return true;
        }

        static List<int> GetPeelOuterTargetArrowIndicesV11(PeelOuterStatsV11 stats, int takeLimit)
        {
            if (stats?.Candidates == null || stats.Candidates.Count == 0)
                return new List<int>();

            return stats.Candidates
                .OrderByDescending(c => c.Score)
                .ThenByDescending(c => c.Wave > 0)
                .ThenBy(c => c.Wave)
                .ThenBy(c => c.ArrowIndex)
                .Select(c => c.ArrowIndex)
                .Distinct()
                .Take(Mathf.Max(1, takeLimit))
                .ToList();
        }

        static int ApplyBoundaryDirectOuterRewireV12(LevelDefinition level, IRuleset rules, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null)
                return 0;

            var accepted = new List<string>();
            int hookOps = ApplyBoundaryHookPrependPassV12(level, rules, maxAcceptedOps: 8, maxAddedTiles: 14, out string hookText);
            if (hookOps > 0)
                accepted.Add($"boundaryHook:{hookText}");

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return hookOps;
        }

        static int ApplyBoundaryHookPrependPassV12(LevelDefinition level, IRuleset rules, int maxAcceptedOps, int maxAddedTiles, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null || maxAcceptedOps <= 0 || maxAddedTiles <= 0)
                return 0;

            var accepted = new List<string>();
            Row current = AnalyzeLevel(new PackSource("boundary_hook_current", "", false), 0, 0, level, rules, null, null);
            if (!current.GreedySolved)
                return 0;

            int addedTiles = 0;
            for (int op = 0; op < maxAcceptedOps && addedTiles < maxAddedTiles; op++)
            {
                PeelOuterStatsV11 peelBefore = AnalyzePeelOuterStatsV11(level, rules, maxWaves: 4, maxRemovedChains: 96);
                List<int> candidates = FindDirectOuterArrowIndices(level, rules, includeNonClearable: true);
                if (candidates.Count == 0)
                    break;

                bool found = false;
                float bestScore = float.NegativeInfinity;
                Row bestRow = null;
                PeelOuterStatsV11 bestPeel = null;
                AuthoredLevelData bestAuthored = null;
                string bestText = "";
                AuthoredLevelData iterationAuthored = CloneAuthored(level.authoredLevel);
                PressureSnapshot quickBefore = AnalyzePressureQuick(level, rules);
                int attempts = 0;

                for (int c = 0; c < candidates.Count && c < 14 && attempts < 42; c++)
                {
                    int arrowIndex = candidates[c];
                    if (arrowIndex < 0 || arrowIndex >= level.authoredLevel.arrows.Count)
                        continue;

                    AuthoredArrowData sourceArrow = level.authoredLevel.arrows[arrowIndex];
                    if (sourceArrow?.indices == null || sourceArrow.indices.Count < 3)
                        continue;

                    for (int reverse = 0; reverse < 2 && attempts < 42; reverse++)
                    {
                        List<int> oriented = OrientedCopy(sourceArrow.indices, reverse != 0);
                        if (oriented.Count < 3)
                            continue;

                        int width = Mathf.Max(1, level.authoredLevel.width);
                        int height = Mathf.Max(1, level.authoredLevel.height);
                        int oldHead = oriented[0];
                        int neck = oriented[1];
                        Vector2Int oldHeadPos = IndexToPos(oldHead, width);
                        Vector2Int neckPos = IndexToPos(neck, width);
                        Vector2Int oldForward = oldHeadPos - neckPos;
                        if (!IsCardinal(oldForward))
                            continue;

                        if (!AuthoredLevelBuilder.TryBuildBoard(iterationAuthored, out BoardState sourceBoard, out _))
                            continue;
                        if (!TraceHeadRayToExit(sourceBoard, oldHeadPos, oldForward, out _))
                            continue;

                        var hookDirs = new[]
                        {
                            new Vector2Int(oldForward.y, -oldForward.x),
                            new Vector2Int(-oldForward.y, oldForward.x),
                            -oldForward
                        };

                        foreach (Vector2Int hookDir in hookDirs)
                        {
                            if (attempts >= 42)
                                break;
                            if (!IsCardinal(hookDir))
                                continue;

                            Vector2Int newHeadPos = oldHeadPos + hookDir;
                            if ((uint)newHeadPos.x >= (uint)width || (uint)newHeadPos.y >= (uint)height)
                                continue;

                            int newHead = newHeadPos.y * width + newHeadPos.x;
                            if (newHead == neck || oriented.Contains(newHead))
                                continue;

                            level.authoredLevel = CloneAuthored(iterationAuthored);
                            if (!AuthoredLevelBuilder.TryBuildBoard(level.authoredLevel, out BoardState board, out _))
                                continue;
                            if (board.tiles[newHead].type != TileType.Empty)
                                continue;

                            AuthoredArrowData arrow = level.authoredLevel.arrows[arrowIndex];
                            arrow.indices = OrientedCopy(arrow.indices, reverse != 0);
                            arrow.indices.Insert(0, newHead);
                            if (!IsUniqueContinuousPath(arrow.indices, width))
                                continue;

                            attempts++;
                            PressureSnapshot quickAfter = AnalyzePressureQuick(level, rules);
                            if (quickAfter.DirectOuterExits > quickBefore.DirectOuterExits - 1)
                                continue;
                            if (quickAfter.DirectClearableOuterExits > quickBefore.DirectClearableOuterExits + 1)
                                continue;
                            if (quickAfter.PressureScore > quickBefore.PressureScore + 30f)
                                continue;

                            Row next = AnalyzeLevel(new PackSource("boundary_hook_candidate", "", false), 0, 0, level, rules, null, null);
                            PeelOuterStatsV11 peelAfter = AnalyzePeelOuterStatsV11(level, rules, maxWaves: 4, maxRemovedChains: 96);
                            if (!AcceptBoundaryHookStepV12(current, next, peelBefore, peelAfter))
                                continue;

                            float score = ScoreBoundaryHookStepV12(current, next, peelBefore, peelAfter);
                            bool newHeadOnBoundary = newHeadPos.x == 0 || newHeadPos.y == 0 || newHeadPos.x == width - 1 || newHeadPos.y == height - 1;
                            if (!newHeadOnBoundary)
                                score += 220f;
                            if (reverse != 0)
                                score += 30f;
                            if (score <= bestScore)
                                continue;

                            found = true;
                            bestScore = score;
                            bestRow = next;
                            bestPeel = peelAfter;
                            bestAuthored = CloneAuthored(level.authoredLevel);
                            bestText =
                                $"hook:{arrowIndex}:rev{reverse}:head {oldHead}->{newHead}:directOuter {current.DirectOuterExits}->{next.DirectOuterExits}:PBE {peelBefore.PersistentOuter}->{peelAfter.PersistentOuter}:NEE {peelBefore.NewlyExposedOuter}->{peelAfter.NewlyExposedOuter}:future {peelBefore.FutureOuter}->{peelAfter.FutureOuter}:open {current.OpeningChoices}->{next.OpeningChoices}:avg {F(current.AvgChoices)}->{F(next.AvgChoices)}";
                        }
                    }
                }

                level.authoredLevel = iterationAuthored;
                if (!found || bestAuthored == null || bestRow == null || bestPeel == null)
                    break;

                level.authoredLevel = bestAuthored;
                current = bestRow;
                addedTiles++;
                accepted.Add(bestText);
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return accepted.Count;
        }

        static bool AcceptBoundaryHookStepV12(Row before, Row after, PeelOuterStatsV11 beforePeel, PeelOuterStatsV11 afterPeel)
        {
            if (!after.GreedySolved)
                return false;
            if (after.Chains != before.Chains)
                return false;
            if (after.ArrowTiles != before.ArrowTiles + 1)
                return false;
            if (after.OpeningChoices < 2)
                return false;
            if (after.DirectOuterExits > before.DirectOuterExits - 1)
                return false;
            if (afterPeel.PersistentOuter > beforePeel.PersistentOuter - 1)
                return false;
            if (afterPeel.NewlyExposedOuter > beforePeel.NewlyExposedOuter + Mathf.Max(1, Mathf.CeilToInt(beforePeel.NewlyExposedOuter * 0.05f)))
                return false;
            if (after.OpeningChoices > before.OpeningChoices + 1)
                return false;
            if (after.AvgChoices > before.AvgChoices + 0.8f)
                return false;
            if (after.MaxChoices > before.MaxChoices + 5)
                return false;
            if (after.ForcedMoveRatio > before.ForcedMoveRatio + 0.35f)
                return false;

            return true;
        }

        static float ScoreBoundaryHookStepV12(Row before, Row after, PeelOuterStatsV11 beforePeel, PeelOuterStatsV11 afterPeel)
        {
            float score = 0f;
            score += (before.DirectOuterExits - after.DirectOuterExits) * 780f;
            score += (beforePeel.PersistentOuter - afterPeel.PersistentOuter) * 760f;
            score += (beforePeel.PersistentRiskScore - afterPeel.PersistentRiskScore) * 0.8f;
            score += (beforePeel.NewlyExposedOuter - afterPeel.NewlyExposedOuter) * 180f;
            score += (beforePeel.FutureOuter - afterPeel.FutureOuter) * 120f;
            score += (before.OpeningChoices - after.OpeningChoices) * 120f;
            score += (before.AvgChoices - after.AvgChoices) * 180f;
            score -= Mathf.Max(0, afterPeel.NewlyExposedOuter - beforePeel.NewlyExposedOuter) * 520f;
            score -= Mathf.Max(0, after.OpeningChoices - before.OpeningChoices) * 180f;
            return score;
        }

        static int ApplyBoundaryDirectOuterInsetV13(LevelDefinition level, IRuleset rules, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null)
                return 0;

            var accepted = new List<string>();
            int insetOps = ApplyBoundaryTwoCellInsetPassV13(level, rules, maxAcceptedOps: 10, maxAddedTiles: 20, out string insetText);
            if (insetOps > 0)
                accepted.Add($"boundaryInset2:{insetText}");

            int hookOps = ApplyBoundaryHookPrependPassV12(level, rules, maxAcceptedOps: 6, maxAddedTiles: 8, out string hookText);
            if (hookOps > 0)
                accepted.Add($"boundaryHookFallback:{hookText}");

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return insetOps + hookOps;
        }

        static int ApplyBoundaryTwoCellInsetPassV13(LevelDefinition level, IRuleset rules, int maxAcceptedOps, int maxAddedTiles, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null || maxAcceptedOps <= 0 || maxAddedTiles < 2)
                return 0;

            var accepted = new List<string>();
            Row current = AnalyzeLevel(new PackSource("boundary_inset_current", "", false), 0, 0, level, rules, null, null);
            if (!current.GreedySolved)
                return 0;

            int addedTiles = 0;
            for (int op = 0; op < maxAcceptedOps && addedTiles + 2 <= maxAddedTiles; op++)
            {
                PeelOuterStatsV11 peelBefore = AnalyzePeelOuterStatsV11(level, rules, maxWaves: 4, maxRemovedChains: 96);
                List<int> candidates = FindDirectOuterArrowIndices(level, rules, includeNonClearable: true);
                if (candidates.Count == 0)
                    break;

                bool found = false;
                float bestScore = float.NegativeInfinity;
                Row bestRow = null;
                PeelOuterStatsV11 bestPeel = null;
                AuthoredLevelData bestAuthored = null;
                string bestText = "";
                AuthoredLevelData iterationAuthored = CloneAuthored(level.authoredLevel);
                PressureSnapshot quickBefore = AnalyzePressureQuick(level, rules);
                int attempts = 0;

                for (int c = 0; c < candidates.Count && c < 18 && attempts < 72; c++)
                {
                    int arrowIndex = candidates[c];
                    if (arrowIndex < 0 || arrowIndex >= level.authoredLevel.arrows.Count)
                        continue;

                    AuthoredArrowData sourceArrow = level.authoredLevel.arrows[arrowIndex];
                    if (sourceArrow?.indices == null || sourceArrow.indices.Count < 3)
                        continue;

                    for (int reverse = 0; reverse < 2 && attempts < 72; reverse++)
                    {
                        List<int> oriented = OrientedCopy(sourceArrow.indices, reverse != 0);
                        if (oriented.Count < 3)
                            continue;

                        int width = Mathf.Max(1, level.authoredLevel.width);
                        int height = Mathf.Max(1, level.authoredLevel.height);
                        int oldHead = oriented[0];
                        int neck = oriented[1];
                        Vector2Int oldHeadPos = IndexToPos(oldHead, width);
                        Vector2Int neckPos = IndexToPos(neck, width);
                        Vector2Int oldForward = oldHeadPos - neckPos;
                        if (!IsCardinal(oldForward))
                            continue;

                        if (!AuthoredLevelBuilder.TryBuildBoard(iterationAuthored, out BoardState sourceBoard, out _))
                            continue;
                        if (!TraceHeadRayToExit(sourceBoard, oldHeadPos, oldForward, out _))
                            continue;

                        Vector2Int sideA = new Vector2Int(oldForward.y, -oldForward.x);
                        Vector2Int sideB = new Vector2Int(-oldForward.y, oldForward.x);
                        var patterns = new[]
                        {
                            (Bridge: oldHeadPos + sideA, NewHead: oldHeadPos + sideA - oldForward, Kind: "insetA"),
                            (Bridge: oldHeadPos + sideB, NewHead: oldHeadPos + sideB - oldForward, Kind: "insetB"),
                            (Bridge: oldHeadPos + sideA, NewHead: oldHeadPos + sideA + sideA, Kind: "sideA2"),
                            (Bridge: oldHeadPos + sideB, NewHead: oldHeadPos + sideB + sideB, Kind: "sideB2")
                        };

                        foreach (var pattern in patterns)
                        {
                            if (attempts >= 72)
                                break;

                            Vector2Int bridgePos = pattern.Bridge;
                            Vector2Int newHeadPos = pattern.NewHead;
                            if ((uint)bridgePos.x >= (uint)width || (uint)bridgePos.y >= (uint)height)
                                continue;
                            if ((uint)newHeadPos.x >= (uint)width || (uint)newHeadPos.y >= (uint)height)
                                continue;
                            if (!IsCardinal(bridgePos - oldHeadPos) || !IsCardinal(newHeadPos - bridgePos))
                                continue;

                            int bridge = bridgePos.y * width + bridgePos.x;
                            int newHead = newHeadPos.y * width + newHeadPos.x;
                            if (newHead == bridge || newHead == oldHead || bridge == oldHead || bridge == neck || newHead == neck)
                                continue;
                            if (oriented.Contains(bridge) || oriented.Contains(newHead))
                                continue;

                            level.authoredLevel = CloneAuthored(iterationAuthored);
                            if (!AuthoredLevelBuilder.TryBuildBoard(level.authoredLevel, out BoardState board, out _))
                                continue;
                            if (board.tiles[bridge].type != TileType.Empty || board.tiles[newHead].type != TileType.Empty)
                                continue;

                            AuthoredArrowData arrow = level.authoredLevel.arrows[arrowIndex];
                            arrow.indices = OrientedCopy(arrow.indices, reverse != 0);
                            arrow.indices.Insert(0, bridge);
                            arrow.indices.Insert(0, newHead);
                            if (!IsUniqueContinuousPath(arrow.indices, width))
                                continue;

                            attempts++;
                            PressureSnapshot quickAfter = AnalyzePressureQuick(level, rules);
                            if (quickAfter.DirectOuterExits > quickBefore.DirectOuterExits - 1)
                                continue;
                            if (quickAfter.DirectClearableOuterExits > quickBefore.DirectClearableOuterExits + 1)
                                continue;
                            if (quickAfter.EdgeShortChains > quickBefore.EdgeShortChains)
                                continue;
                            if (quickAfter.EdgeShortDirectOuterExits > quickBefore.EdgeShortDirectOuterExits)
                                continue;
                            if (quickAfter.PressureScore > quickBefore.PressureScore + 24f)
                                continue;

                            Row next = AnalyzeLevel(new PackSource("boundary_inset_candidate", "", false), 0, 0, level, rules, null, null);
                            PeelOuterStatsV11 peelAfter = AnalyzePeelOuterStatsV11(level, rules, maxWaves: 4, maxRemovedChains: 96);
                            if (!AcceptBoundaryInsetStepV13(current, next, peelBefore, peelAfter))
                                continue;

                            float score = ScoreBoundaryInsetStepV13(current, next, peelBefore, peelAfter);
                            bool newHeadOnBoundary = newHeadPos.x == 0 || newHeadPos.y == 0 || newHeadPos.x == width - 1 || newHeadPos.y == height - 1;
                            if (!newHeadOnBoundary)
                                score += 260f;
                            if (pattern.Kind.StartsWith("inset", StringComparison.Ordinal))
                                score += 220f;
                            if (reverse != 0)
                                score += 35f;
                            if (score <= bestScore)
                                continue;

                            found = true;
                            bestScore = score;
                            bestRow = next;
                            bestPeel = peelAfter;
                            bestAuthored = CloneAuthored(level.authoredLevel);
                            bestText =
                                $"inset2:{arrowIndex}:rev{reverse}:{pattern.Kind}:head {oldHead}->{newHead}:bridge {bridge}:directOuter {current.DirectOuterExits}->{next.DirectOuterExits}:PBE {peelBefore.PersistentOuter}->{peelAfter.PersistentOuter}:NEE {peelBefore.NewlyExposedOuter}->{peelAfter.NewlyExposedOuter}:future {peelBefore.FutureOuter}->{peelAfter.FutureOuter}:open {current.OpeningChoices}->{next.OpeningChoices}:avg {F(current.AvgChoices)}->{F(next.AvgChoices)}";
                        }
                    }
                }

                level.authoredLevel = iterationAuthored;
                if (!found || bestAuthored == null || bestRow == null || bestPeel == null)
                    break;

                level.authoredLevel = bestAuthored;
                current = bestRow;
                addedTiles += 2;
                accepted.Add(bestText);
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return accepted.Count;
        }

        static bool AcceptBoundaryInsetStepV13(Row before, Row after, PeelOuterStatsV11 beforePeel, PeelOuterStatsV11 afterPeel)
        {
            if (!after.GreedySolved)
                return false;
            if (after.Chains != before.Chains)
                return false;
            if (after.ArrowTiles != before.ArrowTiles + 2)
                return false;
            if (after.OpeningChoices < 2)
                return false;
            if (after.DirectOuterExits > before.DirectOuterExits - 1)
                return false;
            if (afterPeel.PersistentOuter > beforePeel.PersistentOuter - 1)
                return false;
            if (afterPeel.NewlyExposedOuter > beforePeel.NewlyExposedOuter + Mathf.Max(1, Mathf.CeilToInt(beforePeel.NewlyExposedOuter * 0.03f)))
                return false;
            if (after.OpeningChoices > before.OpeningChoices + 1)
                return false;
            if (after.AvgChoices > before.AvgChoices + 0.55f)
                return false;
            if (after.MaxChoices > before.MaxChoices + 4)
                return false;
            if (after.ForcedMoveRatio > before.ForcedMoveRatio + 0.3f)
                return false;

            return true;
        }

        static float ScoreBoundaryInsetStepV13(Row before, Row after, PeelOuterStatsV11 beforePeel, PeelOuterStatsV11 afterPeel)
        {
            float score = 0f;
            score += (before.DirectOuterExits - after.DirectOuterExits) * 980f;
            score += (beforePeel.PersistentOuter - afterPeel.PersistentOuter) * 960f;
            score += (beforePeel.PersistentRiskScore - afterPeel.PersistentRiskScore) * 0.95f;
            score += (beforePeel.NewlyExposedOuter - afterPeel.NewlyExposedOuter) * 220f;
            score += (beforePeel.FutureOuter - afterPeel.FutureOuter) * 140f;
            score += (before.OpeningChoices - after.OpeningChoices) * 155f;
            score += (before.AvgChoices - after.AvgChoices) * 220f;
            score -= Mathf.Max(0, afterPeel.NewlyExposedOuter - beforePeel.NewlyExposedOuter) * 700f;
            score -= Mathf.Max(0, after.OpeningChoices - before.OpeningChoices) * 260f;
            score -= Mathf.Max(0f, after.AvgChoices - before.AvgChoices) * 240f;
            return score;
        }

        static int ApplyBoundaryEndpointCompressionV14(LevelDefinition level, IRuleset rules, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null)
                return 0;

            return ApplyBoundaryEndpointMergePassV14(level, rules, maxAcceptedOps: 3, out operations);
        }

        static int ApplyBoundaryEndpointMergePassV14(LevelDefinition level, IRuleset rules, int maxAcceptedOps, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null || maxAcceptedOps <= 0)
                return 0;

            var accepted = new List<string>();
            Row current = AnalyzeLevel(new PackSource("boundary_compress_current", "", false), 0, 0, level, rules, null, null);
            if (!current.GreedySolved)
                return 0;

            for (int op = 0; op < maxAcceptedOps; op++)
            {
                PeelOuterStatsV11 peelBefore = AnalyzePeelOuterStatsV11(level, rules, maxWaves: 4, maxRemovedChains: 96);
                PressureSnapshot quickBefore = AnalyzePressureQuick(level, rules);
                List<MergeCandidate> candidates = FindBoundaryEndpointCompressionCandidatesV14(
                    level,
                    rules,
                    maxInfoLength: 18,
                    maxTotalLength: 38,
                    takeLimit: 160);

                bool found = false;
                float bestScore = float.NegativeInfinity;
                Row bestRow = null;
                PeelOuterStatsV11 bestPeel = null;
                AuthoredLevelData bestAuthored = null;
                string bestText = "";
                AuthoredLevelData iterationAuthored = CloneAuthored(level.authoredLevel);

                for (int c = 0; c < candidates.Count && c < 46; c++)
                {
                    MergeCandidate candidate = candidates[c];
                    level.authoredLevel = CloneAuthored(iterationAuthored);
                    if (!TryApplyMergeCandidate(level, candidate, out string mergeSummary))
                        continue;

                    PressureSnapshot quickAfter = AnalyzePressureQuick(level, rules);
                    if (!AcceptBoundaryCompressionQuickStepV14(quickBefore, quickAfter))
                        continue;

                    Row next = AnalyzeLevel(new PackSource("boundary_compress_candidate", "", false), 0, 0, level, rules, null, null);
                    PeelOuterStatsV11 peelAfter = AnalyzePeelOuterStatsV11(level, rules, maxWaves: 4, maxRemovedChains: 96);
                    if (!AcceptBoundaryCompressionStepV14(current, next, peelBefore, peelAfter))
                        continue;

                    float score = ScoreBoundaryCompressionStepV14(current, next, peelBefore, peelAfter, candidate);
                    if (score <= bestScore)
                        continue;

                    found = true;
                    bestScore = score;
                    bestRow = next;
                    bestPeel = peelAfter;
                    bestAuthored = CloneAuthored(level.authoredLevel);
                    bestText =
                        $"edgeMerge:{candidate.A}+{candidate.B}:{mergeSummary}:directOuter {current.DirectOuterExits}->{next.DirectOuterExits}:PBE {peelBefore.PersistentOuter}->{peelAfter.PersistentOuter}:NEE {peelBefore.NewlyExposedOuter}->{peelAfter.NewlyExposedOuter}:future {peelBefore.FutureOuter}->{peelAfter.FutureOuter}:open {current.OpeningChoices}->{next.OpeningChoices}:avg {F(current.AvgChoices)}->{F(next.AvgChoices)}";
                }

                level.authoredLevel = iterationAuthored;
                if (!found || bestAuthored == null || bestRow == null || bestPeel == null)
                    break;

                level.authoredLevel = bestAuthored;
                current = bestRow;
                accepted.Add(bestText);
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return accepted.Count;
        }

        static List<MergeCandidate> FindBoundaryEndpointCompressionCandidatesV14(
            LevelDefinition level,
            IRuleset rules,
            int maxInfoLength,
            int maxTotalLength,
            int takeLimit)
        {
            var result = new List<MergeCandidate>();
            AuthoredLevelData authored = level?.authoredLevel;
            if (authored?.arrows == null)
                return result;

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out _))
                return result;

            var directClearable = new HashSet<int>(FindDirectClearableOuterArrowIndices(level, rules));
            var infos = new List<ChainInfo>();
            int width = authored.width;
            int height = authored.height;

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                AuthoredArrowData arrow = authored.arrows[i];
                if (arrow?.indices == null || arrow.indices.Count < 2)
                    continue;

                ChainInfo info = BuildChainInfo(i, arrow, board, width, height, directClearable.Contains(i));
                if (!info.EdgeTouch && !info.DirectOuter && !info.DirectClearableOuter)
                    continue;
                if (info.Length > maxInfoLength)
                    continue;
                infos.Add(info);
            }

            for (int a = 0; a < infos.Count; a++)
            {
                for (int b = a + 1; b < infos.Count; b++)
                {
                    ChainInfo first = infos[a];
                    ChainInfo second = infos[b];
                    int totalLength = first.Length + second.Length;
                    if (totalLength > maxTotalLength)
                        continue;

                    bool sameSide = (first.SideMask & second.SideMask) != 0;
                    bool bothBoundary = first.EdgeTouch && second.EdgeTouch;
                    bool hasOuter = first.DirectOuter || second.DirectOuter || first.DirectClearableOuter || second.DirectClearableOuter;
                    if (!hasOuter)
                        continue;
                    if (!bothBoundary && !sameSide)
                        continue;
                    if (sameSide && first.Straight && second.Straight && totalLength >= 10)
                        continue;

                    AddMergeCandidateIfConnected(result, first, second, false, false, totalLength, width);
                    AddMergeCandidateIfConnected(result, first, second, false, true, totalLength, width);
                    AddMergeCandidateIfConnected(result, first, second, true, false, totalLength, width);
                    AddMergeCandidateIfConnected(result, first, second, true, true, totalLength, width);
                    AddMergeCandidateIfConnected(result, second, first, false, false, totalLength, width);
                    AddMergeCandidateIfConnected(result, second, first, false, true, totalLength, width);
                    AddMergeCandidateIfConnected(result, second, first, true, false, totalLength, width);
                    AddMergeCandidateIfConnected(result, second, first, true, true, totalLength, width);
                }
            }

            return result
                .OrderByDescending(c => c.Score)
                .ThenBy(c => c.TotalLength)
                .ThenBy(c => Mathf.Min(c.A, c.B))
                .ThenBy(c => Mathf.Max(c.A, c.B))
                .Take(takeLimit)
                .ToList();
        }

        static bool AcceptBoundaryCompressionQuickStepV14(PressureSnapshot before, PressureSnapshot after)
        {
            if (after.Chains != before.Chains - 1)
                return false;
            if (after.ArrowTiles != before.ArrowTiles)
                return false;
            if (after.OpeningChoices < 2)
                return false;
            if (after.OpeningChoices > before.OpeningChoices)
                return false;
            if (after.DirectOuterExits > before.DirectOuterExits - 1)
                return false;
            if (after.DirectClearableOuterExits > before.DirectClearableOuterExits)
                return false;
            if (after.EdgeShortChains > before.EdgeShortChains)
                return false;
            if (after.EdgeShortDirectOuterExits > before.EdgeShortDirectOuterExits)
                return false;
            if (after.BoundaryStraightOuterExits > before.BoundaryStraightOuterExits)
                return false;
            if (after.PressureScore > before.PressureScore + 8f)
                return false;

            return true;
        }

        static bool AcceptBoundaryCompressionStepV14(Row before, Row after, PeelOuterStatsV11 beforePeel, PeelOuterStatsV11 afterPeel)
        {
            if (!after.GreedySolved)
                return false;
            if (after.Chains != before.Chains - 1)
                return false;
            if (after.ArrowTiles != before.ArrowTiles)
                return false;
            if (after.OpeningChoices < 2)
                return false;
            if (after.OpeningChoices > before.OpeningChoices)
                return false;
            if (after.DirectOuterExits > before.DirectOuterExits - 1)
                return false;
            if (afterPeel.PersistentOuter > beforePeel.PersistentOuter - 1)
                return false;
            if (afterPeel.NewlyExposedOuter > beforePeel.NewlyExposedOuter + Mathf.Max(1, Mathf.CeilToInt(beforePeel.NewlyExposedOuter * 0.02f)))
                return false;
            if (after.AvgChoices > before.AvgChoices + 0.15f)
                return false;
            if (after.MaxChoices > before.MaxChoices + 2)
                return false;
            if (after.ForcedMoveRatio > before.ForcedMoveRatio + 0.2f)
                return false;
            if (after.EdgeShortDirectOuterExits > before.EdgeShortDirectOuterExits)
                return false;
            if (after.BoundaryStraightOuterExits > before.BoundaryStraightOuterExits)
                return false;

            return true;
        }

        static float ScoreBoundaryCompressionStepV14(Row before, Row after, PeelOuterStatsV11 beforePeel, PeelOuterStatsV11 afterPeel, MergeCandidate candidate)
        {
            float score = 0f;
            score += (before.DirectOuterExits - after.DirectOuterExits) * 880f;
            score += (beforePeel.PersistentOuter - afterPeel.PersistentOuter) * 860f;
            score += (beforePeel.NewlyExposedOuter - afterPeel.NewlyExposedOuter) * 260f;
            score += (beforePeel.FutureOuter - afterPeel.FutureOuter) * 150f;
            score += (before.OpeningChoices - after.OpeningChoices) * 180f;
            score += (before.AvgChoices - after.AvgChoices) * 210f;
            score += (before.Chains - after.Chains) * 90f;
            score += Mathf.Min(candidate.TotalLength, 28) * 4f;
            score -= Mathf.Max(0, afterPeel.NewlyExposedOuter - beforePeel.NewlyExposedOuter) * 760f;
            score -= Mathf.Max(0, after.BoundaryStraightOuterExits - before.BoundaryStraightOuterExits) * 620f;
            score -= Mathf.Max(0, after.EdgeShortDirectOuterExits - before.EdgeShortDirectOuterExits) * 620f;
            return score;
        }

        static int ApplyPressureReverseOuterHeadV3(LevelDefinition level, IRuleset rules, int maxAcceptedOps, out string operations)
        {
            operations = "";
            if (level?.authoredLevel?.arrows == null || maxAcceptedOps <= 0)
                return 0;

            var accepted = new List<string>();
            Row current = AnalyzeLevel(new PackSource("pressure_redirect_current", "", false), 0, 0, level, rules, null, null);
            if (!current.GreedySolved)
                return 0;

            for (int op = 0; op < maxAcceptedOps; op++)
            {
                List<int> candidates = FindDirectClearableOuterArrowIndices(level, rules);
                bool found = false;
                float bestScore = float.NegativeInfinity;
                Row bestRow = null;
                AuthoredLevelData bestAuthored = null;
                string bestText = "";

                for (int c = 0; c < candidates.Count && c < 8; c++)
                {
                    int arrowIndex = candidates[c];
                    if (arrowIndex < 0 || arrowIndex >= level.authoredLevel.arrows.Count)
                        continue;

                    AuthoredArrowData arrow = level.authoredLevel.arrows[arrowIndex];
                    if (arrow?.indices == null || arrow.indices.Count < 2)
                        continue;

                    AuthoredLevelData beforeAuthored = CloneAuthored(level.authoredLevel);
                    arrow.indices.Reverse();

                    Row next = AnalyzeLevel(new PackSource("pressure_redirect_candidate", "", false), 0, 0, level, rules, null, null);
                    if (AcceptPressureRedirectStep(current, next))
                    {
                        float score = ScorePressureRedirectStep(current, next, arrowIndex);
                        if (score > bestScore)
                        {
                            found = true;
                            bestScore = score;
                            bestRow = next;
                            bestAuthored = CloneAuthored(level.authoredLevel);
                            bestText =
                                $"reverse:{arrowIndex}:score {current.LeakScore}->{next.LeakScore}:outer {current.DirectClearableOuterExits}->{next.DirectClearableOuterExits}:open {current.OpeningChoices}->{next.OpeningChoices}:avg {F(current.AvgChoices)}->{F(next.AvgChoices)}";
                        }
                    }

                    level.authoredLevel = beforeAuthored;
                }

                if (!found || bestAuthored == null || bestRow == null)
                    break;

                level.authoredLevel = bestAuthored;
                current = bestRow;
                accepted.Add(bestText);
            }

            operations = accepted.Count > 0 ? string.Join("|", accepted) : "none";
            return accepted.Count;
        }

        static bool AcceptPressureRedirectStep(Row before, Row after)
        {
            if (!after.GreedySolved)
                return false;
            if (after.Chains != before.Chains)
                return false;
            if (after.ArrowTiles != before.ArrowTiles)
                return false;
            if (after.OpeningChoices < 2)
                return false;
            if (after.ForcedMoveRatio > before.ForcedMoveRatio + 0.25f)
                return false;
            if (after.DependencyDepthProxy + 0.4f < before.DependencyDepthProxy)
                return false;
            if (after.MaxChoices > before.MaxChoices + 3)
                return false;
            if (after.AvgChoices > before.AvgChoices + 0.45f)
                return false;

            return
                after.DirectClearableOuterExits <= before.DirectClearableOuterExits - 1 ||
                after.OpeningChoices <= before.OpeningChoices - 1 ||
                after.AvgChoices <= before.AvgChoices - 0.25f ||
                after.EarlyAvgChoices <= before.EarlyAvgChoices - 0.5f ||
                after.LeakScore <= before.LeakScore - 20;
        }

        static float ScorePressureRedirectStep(Row before, Row after, int arrowIndex)
        {
            float score = 0f;
            score += (before.DirectClearableOuterExits - after.DirectClearableOuterExits) * 95f;
            score += (before.OpeningChoices - after.OpeningChoices) * 75f;
            score += (before.AvgChoices - after.AvgChoices) * 160f;
            score += (before.EarlyAvgChoices - after.EarlyAvgChoices) * 70f;
            score += (before.LeakScore - after.LeakScore) * 0.45f;
            score -= arrowIndex * 0.01f;
            if (after.OpeningChoices <= 9)
                score += 80f;
            if (after.DirectClearableOuterExits <= 9)
                score += 80f;
            return score;
        }

        static PressureSnapshot AnalyzePressureQuick(LevelDefinition level, IRuleset rules)
        {
            var snapshot = new PressureSnapshot();
            AuthoredLevelData authored = level?.authoredLevel;
            snapshot.Chains = authored?.arrows?.Count ?? 0;
            snapshot.ArrowTiles = CountArrowTiles(authored);
            if (authored == null || !AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out _))
                return snapshot;

            OuterExitStats outer = AnalyzeOuterExits(authored, board, rules);
            snapshot.DirectOuterExits = outer.DirectOuterExits;
            snapshot.DirectClearableOuterExits = outer.DirectClearableOuterExits;
            snapshot.LongRayOuterExits = outer.LongRayOuterExits;
            snapshot.EdgeShortChains = outer.EdgeShortChains;
            snapshot.EdgeShortDirectOuterExits = outer.EdgeShortDirectOuterExits;
            snapshot.BoundaryStraightOuterExits = outer.BoundaryStraightOuterExits;
            snapshot.OpeningChoices = BuildMoveCandidates(board, rules).Count;
            snapshot.PressureScore =
                snapshot.OpeningChoices * 32f +
                snapshot.DirectClearableOuterExits * 58f +
                snapshot.DirectOuterExits * 16f +
                snapshot.LongRayOuterExits * 18f +
                snapshot.EdgeShortDirectOuterExits * 32f +
                snapshot.EdgeShortChains * 8f +
                snapshot.BoundaryStraightOuterExits * 36f;
            return snapshot;
        }

        static bool AcceptPressureQuickStep(PressureSnapshot before, PressureSnapshot after)
        {
            if (after.Chains != before.Chains - 1)
                return false;
            if (after.ArrowTiles != before.ArrowTiles)
                return false;
            if (after.OpeningChoices < 2)
                return false;
            if (after.OpeningChoices > before.OpeningChoices + 1)
                return false;
            if (after.DirectClearableOuterExits > before.DirectClearableOuterExits + 1)
                return false;
            if (after.PressureScore > before.PressureScore + 25f)
                return false;

            return
                after.OpeningChoices <= before.OpeningChoices - 1 ||
                after.DirectClearableOuterExits <= before.DirectClearableOuterExits - 1 ||
                after.EdgeShortDirectOuterExits <= before.EdgeShortDirectOuterExits - 1 ||
                after.BoundaryStraightOuterExits <= before.BoundaryStraightOuterExits - 1 ||
                after.PressureScore <= before.PressureScore - 45f ||
                (after.OpeningChoices <= before.OpeningChoices + 1 &&
                 after.DirectClearableOuterExits <= before.DirectClearableOuterExits + 1);
        }

        static float ScorePressureQuickStep(PressureSnapshot before, PressureSnapshot after, MergeCandidate candidate)
        {
            float score = 0f;
            score += (before.OpeningChoices - after.OpeningChoices) * 95f;
            score += (before.DirectClearableOuterExits - after.DirectClearableOuterExits) * 90f;
            score += (before.DirectOuterExits - after.DirectOuterExits) * 18f;
            score += (before.EdgeShortDirectOuterExits - after.EdgeShortDirectOuterExits) * 60f;
            score += (before.BoundaryStraightOuterExits - after.BoundaryStraightOuterExits) * 65f;
            score += (before.PressureScore - after.PressureScore) * 0.8f;
            score += Mathf.Min(candidate.TotalLength, 40) * 1.5f;

            if (after.OpeningChoices <= 8)
                score += 120f;
            else if (after.OpeningChoices <= 10)
                score += 70f;

            if (after.DirectClearableOuterExits <= 8)
                score += 120f;
            else if (after.DirectClearableOuterExits <= 10)
                score += 70f;

            if (after.OpeningChoices > before.OpeningChoices)
                score -= 140f;
            if (after.DirectClearableOuterExits > before.DirectClearableOuterExits)
                score -= 140f;
            return score;
        }

        static bool AcceptPressureHardeningStep(Row before, Row after)
        {
            if (!after.GreedySolved)
                return false;
            if (after.Chains != before.Chains - 1)
                return false;
            if (after.ArrowTiles != before.ArrowTiles)
                return false;
            if (after.OpeningChoices < 2)
                return false;
            if (after.ForcedMoveRatio > before.ForcedMoveRatio + 0.28f)
                return false;
            if (after.DependencyDepthProxy + 0.5f < before.DependencyDepthProxy)
                return false;
            if (after.AvgChoices > before.AvgChoices + 0.8f)
                return false;
            if (after.MaxChoices > before.MaxChoices + 4)
                return false;

            bool pressureImproved =
                after.DirectClearableOuterExits <= before.DirectClearableOuterExits - 1 ||
                after.OpeningChoices <= before.OpeningChoices - 1 ||
                after.AvgChoices <= before.AvgChoices - 0.35f ||
                after.EarlyAvgChoices <= before.EarlyAvgChoices - 0.75f ||
                after.LeakScore <= before.LeakScore - 8 ||
                (after.Chains < before.Chains &&
                 after.AvgChoices <= before.AvgChoices + 0.1f &&
                 after.OpeningChoices <= before.OpeningChoices + 1);

            return pressureImproved;
        }

        static float ScorePressureStep(Row before, Row after, MergeCandidate candidate)
        {
            float score = 0f;
            score += (before.OpeningChoices - after.OpeningChoices) * 55f;
            score += (before.DirectClearableOuterExits - after.DirectClearableOuterExits) * 55f;
            score += (before.AvgChoices - after.AvgChoices) * 110f;
            score += (before.EarlyAvgChoices - after.EarlyAvgChoices) * 60f;
            score += (before.LeakScore - after.LeakScore) * 0.35f;
            score += (before.Chains - after.Chains) * 90f;
            score += Mathf.Min(candidate.TotalLength, 46) * 0.8f;

            if (after.AvgChoices <= 10.5f)
                score += 120f;
            else if (after.AvgChoices <= 12f)
                score += 60f;

            if (after.OpeningChoices <= 10)
                score += 150f;
            else if (after.OpeningChoices <= 12)
                score += 85f;

            if (after.DirectClearableOuterExits <= 10)
                score += 150f;
            else if (after.DirectClearableOuterExits <= 12)
                score += 85f;

            if (after.AvgChoices > before.AvgChoices)
                score -= 80f;
            if (after.OpeningChoices > before.OpeningChoices)
                score -= 120f;

            return score;
        }

        static bool AcceptQualitativeHardeningStep(Row before, Row after)
        {
            if (!after.GreedySolved)
                return false;
            if (after.Chains != before.Chains - 1)
                return false;
            if (after.ArrowTiles != before.ArrowTiles)
                return false;
            if (after.OpeningChoices < 2)
                return false;
            if (after.ForcedMoveRatio > before.ForcedMoveRatio + 0.22f)
                return false;
            if (after.DependencyDepthProxy + 0.35f < before.DependencyDepthProxy)
                return false;
            if (after.AvgChoices > before.AvgChoices + 1.2f)
                return false;

            bool stronger =
                after.DirectClearableOuterExits <= before.DirectClearableOuterExits - 1 ||
                after.OpeningChoices <= before.OpeningChoices - 1 ||
                after.EarlyAvgChoices <= before.EarlyAvgChoices - 0.75f ||
                after.LeakScore <= before.LeakScore - 12;

            return stronger;
        }

        static List<MergeCandidate> FindQualitativeMergeCandidates(
            LevelDefinition level,
            IRuleset rules,
            int maxInfoLength = 18,
            int maxTotalLength = 26,
            int takeLimit = 240)
        {
            var result = new List<MergeCandidate>();
            AuthoredLevelData authored = level?.authoredLevel;
            if (authored?.arrows == null)
                return result;

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out _))
                return result;

            var directClearable = new HashSet<int>(FindDirectClearableOuterArrowIndices(level, rules));
            var infos = new List<ChainInfo>();
            int width = authored.width;
            int height = authored.height;

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                AuthoredArrowData arrow = authored.arrows[i];
                if (arrow?.indices == null || arrow.indices.Count < 2)
                    continue;

                ChainInfo info = BuildChainInfo(i, arrow, board, width, height, directClearable.Contains(i));
                if (!info.EdgeTouch && !info.DirectOuter && !info.DirectClearableOuter)
                    continue;
                if (info.Length > maxInfoLength)
                    continue;
                infos.Add(info);
            }

            for (int a = 0; a < infos.Count; a++)
            {
                for (int b = a + 1; b < infos.Count; b++)
                {
                    ChainInfo first = infos[a];
                    ChainInfo second = infos[b];
                    int totalLength = first.Length + second.Length;
                    if (totalLength > maxTotalLength)
                        continue;

                    AddMergeCandidateIfConnected(result, first, second, false, false, totalLength, width);
                    AddMergeCandidateIfConnected(result, first, second, false, true, totalLength, width);
                    AddMergeCandidateIfConnected(result, first, second, true, false, totalLength, width);
                    AddMergeCandidateIfConnected(result, first, second, true, true, totalLength, width);
                    AddMergeCandidateIfConnected(result, second, first, false, false, totalLength, width);
                    AddMergeCandidateIfConnected(result, second, first, false, true, totalLength, width);
                    AddMergeCandidateIfConnected(result, second, first, true, false, totalLength, width);
                    AddMergeCandidateIfConnected(result, second, first, true, true, totalLength, width);
                }
            }

            return result
                .OrderByDescending(c => c.Score)
                .ThenBy(c => Mathf.Min(c.A, c.B))
                .ThenBy(c => Mathf.Max(c.A, c.B))
                .Take(takeLimit)
                .ToList();
        }

        static List<MergeCandidate> FindGateFoldCandidates(
            LevelDefinition level,
            IRuleset rules,
            int maxOpeningLength,
            int maxOtherLength,
            int maxTotalLength,
            int takeLimit)
        {
            var result = new List<MergeCandidate>();
            AuthoredLevelData authored = level?.authoredLevel;
            if (authored?.arrows == null)
                return result;

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out _))
                return result;

            var openingChainIds = new HashSet<int>(BuildMoveCandidates(board, rules).Select(c => c.ChainId));
            var directClearable = new HashSet<int>(FindDirectClearableOuterArrowIndices(level, rules));
            var infos = new List<ChainInfo>();
            int width = authored.width;
            int height = authored.height;

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                AuthoredArrowData arrow = authored.arrows[i];
                if (arrow?.indices == null || arrow.indices.Count < 2)
                    continue;

                bool opening = openingChainIds.Contains(arrow.indices.Min());
                int maxLength = opening ? maxOpeningLength : maxOtherLength;
                if (arrow.indices.Count > maxLength)
                    continue;

                ChainInfo info = BuildChainInfo(i, arrow, board, width, height, directClearable.Contains(i), opening);
                if (!info.Opening && !info.EdgeTouch && !info.DirectOuter && !info.DirectClearableOuter)
                    continue;

                infos.Add(info);
            }

            for (int a = 0; a < infos.Count; a++)
            {
                for (int b = a + 1; b < infos.Count; b++)
                {
                    ChainInfo first = infos[a];
                    ChainInfo second = infos[b];
                    if (!first.Opening && !second.Opening)
                        continue;

                    int totalLength = first.Length + second.Length;
                    if (totalLength > maxTotalLength)
                        continue;

                    AddMergeCandidateIfConnected(result, first, second, false, false, totalLength, width);
                    AddMergeCandidateIfConnected(result, first, second, false, true, totalLength, width);
                    AddMergeCandidateIfConnected(result, first, second, true, false, totalLength, width);
                    AddMergeCandidateIfConnected(result, first, second, true, true, totalLength, width);
                    AddMergeCandidateIfConnected(result, second, first, false, false, totalLength, width);
                    AddMergeCandidateIfConnected(result, second, first, false, true, totalLength, width);
                    AddMergeCandidateIfConnected(result, second, first, true, false, totalLength, width);
                    AddMergeCandidateIfConnected(result, second, first, true, true, totalLength, width);
                }
            }

            return result
                .OrderByDescending(c => c.Score)
                .ThenByDescending(c => c.TotalLength)
                .ThenBy(c => Mathf.Min(c.A, c.B))
                .ThenBy(c => Mathf.Max(c.A, c.B))
                .Take(takeLimit)
                .ToList();
        }

        static List<VisibleGateCandidate> FindVisibleGateCandidatesV5(LevelDefinition level, IRuleset rules, int takeLimit)
        {
            var result = new List<VisibleGateCandidate>();
            AuthoredLevelData authored = level?.authoredLevel;
            if (authored?.arrows == null)
                return result;

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out _))
                return result;

            List<int> directClearable = FindDirectClearableOuterArrowIndices(level, rules);
            int width = authored.width;
            int height = authored.height;
            var seen = new HashSet<string>();

            for (int d = 0; d < directClearable.Count && d < 12; d++)
            {
                int targetIndex = directClearable[d];
                if (targetIndex < 0 || targetIndex >= authored.arrows.Count)
                    continue;

                AuthoredArrowData target = authored.arrows[targetIndex];
                if (target?.indices == null || target.indices.Count < 2)
                    continue;

                Vector2Int head = IndexToPos(target.indices[0], width);
                Vector2Int neck = IndexToPos(target.indices[1], width);
                Vector2Int forward = head - neck;
                if (!IsCardinal(forward))
                    continue;

                var ray = new List<Vector2Int>(8);
                Vector2Int p = head + forward;
                for (int step = 0; step < 6 && board.InBounds(p.x, p.y); step++, p += forward)
                {
                    if (board.tiles[board.Index(p.x, p.y)].type != TileType.Empty)
                        break;
                    ray.Add(p);
                }

                if (ray.Count == 0)
                    continue;

                Vector2Int perpA = new Vector2Int(-forward.y, forward.x);
                Vector2Int perpB = new Vector2Int(forward.y, -forward.x);
                int targetScore = 1800 - d * 35 + Mathf.Min(target.indices.Count, 24) * 4;

                for (int r = 0; r < ray.Count && r < 3; r++)
                {
                    Vector2Int block = ray[r];
                    int rayScore = targetScore - r * 150;
                    AddVisibleGateLaneCandidatesV5(result, seen, board, width, height, targetIndex, block, forward, perpA, rayScore, "hookA");
                    AddVisibleGateLaneCandidatesV5(result, seen, board, width, height, targetIndex, block, forward, perpB, rayScore, "hookB");

                    Vector2Int next = block + forward;
                    Vector2Int next2 = next + forward;
                    TryAddVisibleGatePathV5(result, seen, board, width, height, targetIndex, new[] { next, block }, rayScore - 120, "rayReverse2");
                    TryAddVisibleGatePathV5(result, seen, board, width, height, targetIndex, new[] { block, next }, rayScore - 170, "rayForward2");
                    TryAddVisibleGatePathV5(result, seen, board, width, height, targetIndex, new[] { next2, next, block }, rayScore - 70, "rayReverse3");
                    TryAddVisibleGatePathV5(result, seen, board, width, height, targetIndex, new[] { block, next, next2 }, rayScore - 150, "rayForward3");
                }
            }

            return result
                .OrderByDescending(c => c.StaticScore)
                .ThenByDescending(c => c.Path.Count)
                .Take(takeLimit)
                .ToList();
        }

        static List<VisibleGateCandidate> FindOpeningGateCandidatesV7(LevelDefinition level, IRuleset rules, int takeLimit)
        {
            var result = new List<VisibleGateCandidate>();
            AuthoredLevelData authored = level?.authoredLevel;
            if (authored?.arrows == null)
                return result;

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out _))
                return result;

            int width = authored.width;
            int height = authored.height;
            Dictionary<int, int> chainToArrow = BuildChainIdToArrowIndex(authored);
            var directClearable = new HashSet<int>(FindDirectClearableOuterArrowIndices(level, rules));
            var seen = new HashSet<string>();

            List<MoveCandidate> openingMoves = BuildMoveCandidates(board, rules)
                .OrderByDescending(c => directClearable.Contains(chainToArrow.TryGetValue(c.ChainId, out int ai) ? ai : -1) ? 1 : 0)
                .ThenByDescending(c => c.ChainLength)
                .ThenByDescending(c => c.ClearCount)
                .ThenBy(c => c.ChainId)
                .ToList();

            int openingCount = openingMoves.Count;
            int inspected = 0;
            foreach (MoveCandidate move in openingMoves)
            {
                if (inspected++ >= 28)
                    break;
                if (!chainToArrow.TryGetValue(move.ChainId, out int targetIndex))
                    continue;
                if (targetIndex < 0 || targetIndex >= authored.arrows.Count)
                    continue;

                AuthoredArrowData target = authored.arrows[targetIndex];
                if (target?.indices == null || target.indices.Count < 2)
                    continue;

                Vector2Int head = IndexToPos(target.indices[0], width);
                Vector2Int neck = IndexToPos(target.indices[1], width);
                Vector2Int forward = head - neck;
                if (!IsCardinal(forward))
                    continue;
                if (!TraceHeadRayToExit(board, head, forward, out int emptyRayLength))
                    continue;

                var ray = new List<Vector2Int>(8);
                Vector2Int p = head + forward;
                for (int step = 0; step < 8 && board.InBounds(p.x, p.y); step++, p += forward)
                {
                    if (board.tiles[board.Index(p.x, p.y)].type != TileType.Empty)
                        break;
                    ray.Add(p);
                }

                if (ray.Count == 0)
                    continue;

                Vector2Int perpA = new Vector2Int(-forward.y, forward.x);
                Vector2Int perpB = new Vector2Int(forward.y, -forward.x);
                int baseScore =
                    3400 +
                    openingCount * 18 +
                    (directClearable.Contains(targetIndex) ? 520 : 0) +
                    Mathf.Min(move.ChainLength, 36) * 7 +
                    Mathf.Min(emptyRayLength, 10) * 40;

                for (int r = 0; r < ray.Count && r < 4; r++)
                {
                    Vector2Int block = ray[r];
                    int rayScore = baseScore - r * 90;
                    AddOpeningGateLaneCandidatesV7(result, seen, board, width, height, targetIndex, block, forward, perpA, rayScore, "openingClosedA");
                    AddOpeningGateLaneCandidatesV7(result, seen, board, width, height, targetIndex, block, forward, perpB, rayScore, "openingClosedB");

                    Vector2Int next = block + forward;
                    Vector2Int next2 = next + forward;
                    Vector2Int sideA = block + perpA;
                    Vector2Int sideB = block + perpB;
                    TryAddOpeningGatePathV7(result, seen, board, width, height, targetIndex, new[] { block, next }, rayScore - 170, "openingClosedRay2");
                    TryAddOpeningGatePathV7(result, seen, board, width, height, targetIndex, new[] { next, block }, rayScore - 135, "openingClosedRay2Reverse");
                    TryAddOpeningGatePathV7(result, seen, board, width, height, targetIndex, new[] { next2, next, block }, rayScore - 55, "openingClosedRay3Reverse");
                    TryAddOpeningGatePathV7(result, seen, board, width, height, targetIndex, new[] { sideA, block, sideB }, rayScore + 130, "openingClosedCross3");
                    TryAddOpeningGatePathV7(result, seen, board, width, height, targetIndex, new[] { sideB, block, sideA }, rayScore + 130, "openingClosedCross3");
                }
            }

            return result
                .OrderByDescending(c => c.StaticScore)
                .ThenByDescending(c => c.Path.Count)
                .Take(takeLimit)
                .ToList();
        }

        static List<RewireCandidate> FindOpeningRewireCandidatesV8(
            LevelDefinition level,
            IRuleset rules,
            int maxBridgeCells,
            int maxTotalLength,
            int takeLimit)
        {
            var result = new List<RewireCandidate>();
            AuthoredLevelData authored = level?.authoredLevel;
            if (authored?.arrows == null)
                return result;

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out _))
                return result;

            int width = authored.width;
            int height = authored.height;
            var openingChainIds = new HashSet<int>(BuildMoveCandidates(board, rules).Select(c => c.ChainId));
            var directClearable = new HashSet<int>(FindDirectClearableOuterArrowIndices(level, rules));
            var infos = new List<ChainInfo>();

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                AuthoredArrowData arrow = authored.arrows[i];
                if (arrow?.indices == null || arrow.indices.Count < 2)
                    continue;
                if (arrow.indices.Count > 38)
                    continue;

                bool opening = openingChainIds.Contains(arrow.indices.Min());
                ChainInfo info = BuildChainInfo(i, arrow, board, width, height, directClearable.Contains(i), opening);
                if (!info.Opening && !info.EdgeTouch && !info.DirectOuter && !info.DirectClearableOuter)
                    continue;
                infos.Add(info);
            }

            List<ChainInfo> openers = infos
                .Where(i => i.Opening)
                .OrderByDescending(i => i.DirectClearableOuter ? 1 : 0)
                .ThenByDescending(i => i.DirectOuter ? 1 : 0)
                .ThenByDescending(i => i.EdgeTouch ? 1 : 0)
                .ThenByDescending(i => i.Length)
                .Take(32)
                .ToList();

            List<ChainInfo> partners = infos
                .OrderByDescending(i => i.Opening ? 1 : 0)
                .ThenByDescending(i => i.DirectClearableOuter ? 1 : 0)
                .ThenByDescending(i => i.EdgeTouch ? 1 : 0)
                .ThenByDescending(i => i.Length)
                .Take(72)
                .ToList();

            var seen = new HashSet<string>();
            for (int o = 0; o < openers.Count; o++)
            {
                ChainInfo opener = openers[o];
                for (int p = 0; p < partners.Count; p++)
                {
                    ChainInfo partner = partners[p];
                    if (opener.ArrowIndex == partner.ArrowIndex)
                        continue;
                    if (!opener.DirectClearableOuter && !partner.DirectClearableOuter)
                        continue;
                    int minTotal = opener.Length + partner.Length;
                    if (minTotal > maxTotalLength)
                        continue;

                    AddOpeningRewireOrientationsV8(result, seen, board, opener, partner, width, height, maxBridgeCells, maxTotalLength);
                    AddOpeningRewireOrientationsV8(result, seen, board, partner, opener, width, height, maxBridgeCells, maxTotalLength);
                }
            }

            return result
                .OrderByDescending(c => c.StaticScore)
                .ThenBy(c => c.Bridge.Count)
                .ThenByDescending(c => c.TotalLength)
                .Take(takeLimit)
                .ToList();
        }

        static void AddOpeningRewireOrientationsV8(
            List<RewireCandidate> result,
            HashSet<string> seen,
            BoardState board,
            ChainInfo a,
            ChainInfo b,
            int width,
            int height,
            int maxBridgeCells,
            int maxTotalLength)
        {
            AddOpeningRewireCandidateV8(result, seen, board, a, b, false, false, width, height, maxBridgeCells, maxTotalLength);
            AddOpeningRewireCandidateV8(result, seen, board, a, b, false, true, width, height, maxBridgeCells, maxTotalLength);
            AddOpeningRewireCandidateV8(result, seen, board, a, b, true, false, width, height, maxBridgeCells, maxTotalLength);
            AddOpeningRewireCandidateV8(result, seen, board, a, b, true, true, width, height, maxBridgeCells, maxTotalLength);
        }

        static void AddOpeningRewireCandidateV8(
            List<RewireCandidate> result,
            HashSet<string> seen,
            BoardState board,
            ChainInfo a,
            ChainInfo b,
            bool reverseA,
            bool reverseB,
            int width,
            int height,
            int maxBridgeCells,
            int maxTotalLength)
        {
            List<int> pathA = OrientedCopy(a.Indices, reverseA);
            List<int> pathB = OrientedCopy(b.Indices, reverseB);
            if (pathA.Count < 2 || pathB.Count < 2)
                return;

            int from = pathA[pathA.Count - 1];
            int to = pathB[0];
            if (!TryFindBridgeCellsV8(board, width, height, from, to, maxBridgeCells, out List<int> bridge))
                return;

            int totalLength = pathA.Count + bridge.Count + pathB.Count;
            if (totalLength > maxTotalLength)
                return;

            string key = $"{a.ArrowIndex}:{b.ArrowIndex}:{reverseA}:{reverseB}:{string.Join("-", bridge)}";
            if (!seen.Add(key))
                return;

            int score = 0;
            score += a.Opening ? 1200 : 0;
            score += b.Opening ? 900 : 0;
            score += a.DirectClearableOuter ? 950 : 0;
            score += b.DirectClearableOuter ? 650 : 0;
            score += a.DirectOuter ? 260 : 0;
            score += b.DirectOuter ? 200 : 0;
            score += a.EdgeTouch ? 160 : 0;
            score += b.EdgeTouch ? 120 : 0;
            score += (a.SideMask & b.SideMask) != 0 ? 180 : 0;
            score += Mathf.Min(totalLength, 70) * 7;
            score -= bridge.Count * 55;
            if (bridge.Count > 0)
                score += 260;

            result.Add(new RewireCandidate
            {
                A = a.ArrowIndex,
                B = b.ArrowIndex,
                ReverseA = reverseA,
                ReverseB = reverseB,
                Bridge = bridge,
                StaticScore = score,
                TotalLength = totalLength,
                Kind = bridge.Count == 0 ? "adjacentMerge" : "bridgeRewire"
            });
        }

        static bool TryFindBridgeCellsV8(
            BoardState board,
            int width,
            int height,
            int fromIndex,
            int toIndex,
            int maxBridgeCells,
            out List<int> bridge)
        {
            bridge = null;
            Vector2Int from = IndexToPos(fromIndex, width);
            Vector2Int to = IndexToPos(toIndex, width);
            if (AreAdjacent(fromIndex, toIndex, width))
            {
                bridge = new List<int>();
                return true;
            }

            var dirs = new[]
            {
                new Vector2Int(1, 0),
                new Vector2Int(-1, 0),
                new Vector2Int(0, 1),
                new Vector2Int(0, -1)
            };

            var queue = new Queue<BridgeSearchNode>();
            var visited = new HashSet<int> { fromIndex };
            queue.Enqueue(new BridgeSearchNode { Pos = from, Path = new List<int>() });

            while (queue.Count > 0)
            {
                BridgeSearchNode node = queue.Dequeue();
                if (node.Path.Count >= maxBridgeCells)
                    continue;

                for (int d = 0; d < dirs.Length; d++)
                {
                    Vector2Int next = node.Pos + dirs[d];
                    if ((uint)next.x >= (uint)width || (uint)next.y >= (uint)height)
                        continue;
                    int nextIndex = board.Index(next.x, next.y);
                    if (nextIndex == toIndex)
                    {
                        bridge = node.Path;
                        return true;
                    }
                    if (!visited.Add(nextIndex))
                        continue;
                    if (board.tiles[nextIndex].type != TileType.Empty)
                        continue;

                    var nextPath = new List<int>(node.Path.Count + 1);
                    nextPath.AddRange(node.Path);
                    nextPath.Add(nextIndex);
                    queue.Enqueue(new BridgeSearchNode { Pos = next, Path = nextPath });
                }
            }

            return false;
        }

        static bool TryApplyRewireCandidateV8(LevelDefinition level, RewireCandidate candidate, out string summary)
        {
            summary = "";
            AuthoredLevelData authored = level?.authoredLevel;
            if (authored?.arrows == null)
                return false;

            int a = candidate.A;
            int b = candidate.B;
            if (a == b || a < 0 || b < 0 || a >= authored.arrows.Count || b >= authored.arrows.Count)
                return false;

            AuthoredArrowData arrowA = authored.arrows[a];
            AuthoredArrowData arrowB = authored.arrows[b];
            if (arrowA?.indices == null || arrowB?.indices == null || arrowA.indices.Count < 2 || arrowB.indices.Count < 2)
                return false;

            int width = Mathf.Max(1, authored.width);
            List<int> pathA = OrientedCopy(arrowA.indices, candidate.ReverseA);
            List<int> pathB = OrientedCopy(arrowB.indices, candidate.ReverseB);
            var merged = new List<int>(pathA.Count + candidate.Bridge.Count + pathB.Count);
            merged.AddRange(pathA);
            merged.AddRange(candidate.Bridge);
            merged.AddRange(pathB);
            if (!IsUniqueContinuousPath(merged, width))
                return false;

            var occupied = new HashSet<int>();
            if (authored.blockIndices != null)
            {
                for (int i = 0; i < authored.blockIndices.Count; i++)
                    occupied.Add(authored.blockIndices[i]);
            }

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                if (i == a || i == b)
                    continue;
                AuthoredArrowData arrow = authored.arrows[i];
                if (arrow?.indices == null)
                    continue;
                for (int j = 0; j < arrow.indices.Count; j++)
                    occupied.Add(arrow.indices[j]);
            }

            for (int i = 0; i < candidate.Bridge.Count; i++)
            {
                if (occupied.Contains(candidate.Bridge[i]))
                    return false;
            }

            arrowA.indices = merged;
            authored.arrows.RemoveAt(b);
            summary = $"{pathA.Count}+{candidate.Bridge.Count}+{pathB.Count}->{merged.Count}";
            return true;
        }

        static Dictionary<int, int> BuildChainIdToArrowIndex(AuthoredLevelData authored)
        {
            var result = new Dictionary<int, int>();
            if (authored?.arrows == null)
                return result;

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                AuthoredArrowData arrow = authored.arrows[i];
                if (arrow?.indices == null || arrow.indices.Count == 0)
                    continue;
                int chainId = arrow.indices.Min();
                if (!result.ContainsKey(chainId))
                    result.Add(chainId, i);
            }

            return result;
        }

        static void AddOpeningGateLaneCandidatesV7(
            List<VisibleGateCandidate> result,
            HashSet<string> seen,
            BoardState board,
            int width,
            int height,
            int targetIndex,
            Vector2Int block,
            Vector2Int forward,
            Vector2Int side,
            int baseScore,
            string sideLabel)
        {
            Vector2Int side1 = block + side;
            Vector2Int side2 = side1 + side;
            Vector2Int sideForward = side1 + forward;
            Vector2Int sideBack = side1 - forward;
            Vector2Int forward1 = block + forward;
            Vector2Int back1 = block - forward;

            TryAddOpeningGatePathV7(result, seen, board, width, height, targetIndex, new[] { block, side1 }, baseScore + 150, $"{sideLabel}_closedSide2");
            TryAddOpeningGatePathV7(result, seen, board, width, height, targetIndex, new[] { side1, block }, baseScore + 110, $"{sideLabel}_side2");
            TryAddOpeningGatePathV7(result, seen, board, width, height, targetIndex, new[] { block, side1, side2 }, baseScore + 235, $"{sideLabel}_closedHook3");
            TryAddOpeningGatePathV7(result, seen, board, width, height, targetIndex, new[] { side2, side1, block }, baseScore + 215, $"{sideLabel}_hook3");
            TryAddOpeningGatePathV7(result, seen, board, width, height, targetIndex, new[] { forward1, block, side1 }, baseScore + 260, $"{sideLabel}_closedBendF3");
            TryAddOpeningGatePathV7(result, seen, board, width, height, targetIndex, new[] { sideForward, side1, block }, baseScore + 230, $"{sideLabel}_bendF3");
            TryAddOpeningGatePathV7(result, seen, board, width, height, targetIndex, new[] { back1, block, side1 }, baseScore + 240, $"{sideLabel}_closedBendB3");
            TryAddOpeningGatePathV7(result, seen, board, width, height, targetIndex, new[] { sideBack, side1, block }, baseScore + 215, $"{sideLabel}_bendB3");
        }

        static void TryAddOpeningGatePathV7(
            List<VisibleGateCandidate> result,
            HashSet<string> seen,
            BoardState board,
            int width,
            int height,
            int targetIndex,
            IReadOnlyList<Vector2Int> points,
            int baseScore,
            string kind)
        {
            int before = result.Count;
            TryAddVisibleGatePathV5(result, seen, board, width, height, targetIndex, points, baseScore, kind);
            if (result.Count <= before)
                return;

            VisibleGateCandidate candidate = result[result.Count - 1];
            candidate.PeelWave = -1;
            candidate.PeelChoiceCount = 0;
            candidate.SourceChainId = 0;
            candidate.StaticScore += kind.Contains("closed") ? 260 : 0;
            result[result.Count - 1] = candidate;
        }

        static EarlyPeelResult BuildEarlyPeelResultV6(LevelDefinition level, IRuleset rules, int targetChoices, int maxWaves, int maxRemovedChains)
        {
            var result = new EarlyPeelResult();
            AuthoredLevelData authored = level?.authoredLevel;
            if (authored?.arrows == null || !AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out _))
                return result;

            var chainToArrow = new Dictionary<int, int>();
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                AuthoredArrowData arrow = authored.arrows[i];
                if (arrow?.indices == null || arrow.indices.Count < 2)
                    continue;
                chainToArrow[arrow.indices.Min()] = i;
            }

            BoardState state = CloneBoard(board);
            var removedArrows = new HashSet<int>();
            int waveCount = 0;
            int finalChoices = 0;

            for (int wave = 0; wave < maxWaves && result.RemovedCount < maxRemovedChains; wave++)
            {
                List<MoveCandidate> candidates = BuildMoveCandidates(state, rules)
                    .OrderByDescending(c => c.MoveScore)
                    .ThenBy(c => c.ChainId)
                    .ToList();

                if (wave == 0)
                    result.InitialChoices = candidates.Count;
                finalChoices = candidates.Count;
                if (candidates.Count <= targetChoices)
                    break;

                int removedThisWave = 0;
                int limit = Mathf.Min(candidates.Count, Mathf.Max(10, targetChoices * 3));
                for (int c = 0; c < limit && result.RemovedCount < maxRemovedChains; c++)
                {
                    MoveCandidate candidate = candidates[c];
                    if (!chainToArrow.TryGetValue(candidate.ChainId, out int arrowIndex))
                        continue;
                    if (removedArrows.Contains(arrowIndex))
                        continue;
                    if (!state.InBounds(candidate.Move.pos.x, candidate.Move.pos.y))
                        continue;
                    if (state.tiles[state.Index(candidate.Move.pos.x, candidate.Move.pos.y)].type != TileType.Arrow)
                        continue;
                    if (!rules.TryApplyMove(state, candidate.Move, out MoveDelta delta))
                        continue;

                    int cleared = CountCleared(delta);
                    if (cleared <= 0)
                        continue;

                    removedArrows.Add(arrowIndex);
                    result.Removed.Add(new EarlyPeelRemoved
                    {
                        ArrowIndex = arrowIndex,
                        ChainId = candidate.ChainId,
                        Wave = wave,
                        WaveChoices = candidates.Count,
                        ClearCount = cleared,
                        ChainLength = candidate.ChainLength
                    });
                    removedThisWave++;
                }

                waveCount++;
                if (removedThisWave == 0)
                    break;
            }

            finalChoices = BuildMoveCandidates(state, rules).Count;
            result.Waves = waveCount;
            result.FinalChoices = finalChoices;
            return result;
        }

        static List<VisibleGateCandidate> FindEarlyPeelGateCandidatesV6(LevelDefinition level, IRuleset rules, EarlyPeelResult peel, int takeLimit)
        {
            var result = new List<VisibleGateCandidate>();
            AuthoredLevelData authored = level?.authoredLevel;
            if (authored?.arrows == null || peel == null || peel.Removed.Count == 0)
                return result;

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out _))
                return result;

            int width = authored.width;
            int height = authored.height;
            int[] owner = BuildArrowOwnerMap(authored);
            var removedByArrow = new Dictionary<int, EarlyPeelRemoved>();
            for (int i = 0; i < peel.Removed.Count; i++)
            {
                EarlyPeelRemoved removed = peel.Removed[i];
                if (!removedByArrow.ContainsKey(removed.ArrowIndex))
                    removedByArrow[removed.ArrowIndex] = removed;
            }

            var directClearable = new HashSet<int>(FindDirectClearableOuterArrowIndices(level, rules));
            var seen = new HashSet<string>();
            foreach (EarlyPeelRemoved removed in peel.Removed
                .OrderByDescending(r => r.Wave)
                .ThenByDescending(r => r.WaveChoices)
                .ThenByDescending(r => directClearable.Contains(r.ArrowIndex) ? 1 : 0)
                .Take(36))
            {
                int targetIndex = removed.ArrowIndex;
                if (targetIndex < 0 || targetIndex >= authored.arrows.Count)
                    continue;

                AuthoredArrowData target = authored.arrows[targetIndex];
                if (target?.indices == null || target.indices.Count < 2)
                    continue;

                Vector2Int head = IndexToPos(target.indices[0], width);
                Vector2Int neck = IndexToPos(target.indices[1], width);
                Vector2Int forward = head - neck;
                if (!IsCardinal(forward))
                    continue;

                var ray = new List<Vector2Int>(8);
                Vector2Int p = head + forward;
                for (int step = 0; step < 8 && board.InBounds(p.x, p.y); step++, p += forward)
                {
                    int idx = board.Index(p.x, p.y);
                    TileState tile = board.tiles[idx];
                    if (tile.type == TileType.Empty)
                    {
                        ray.Add(p);
                        continue;
                    }

                    int blockingArrow = owner != null && idx >= 0 && idx < owner.Length ? owner[idx] : -1;
                    if (blockingArrow >= 0 &&
                        removedByArrow.TryGetValue(blockingArrow, out EarlyPeelRemoved blocker) &&
                        blocker.Wave < removed.Wave)
                    {
                        continue;
                    }

                    break;
                }

                if (ray.Count == 0)
                    continue;

                Vector2Int perpA = new Vector2Int(-forward.y, forward.x);
                Vector2Int perpB = new Vector2Int(forward.y, -forward.x);
                int baseScore =
                    2200 +
                    removed.Wave * 260 +
                    Mathf.Min(removed.WaveChoices, 40) * 12 +
                    (directClearable.Contains(targetIndex) ? 160 : 0) +
                    Mathf.Min(removed.ChainLength, 28) * 4;

                for (int r = 0; r < ray.Count && r < 4; r++)
                {
                    Vector2Int block = ray[r];
                    int rayScore = baseScore - r * 105;
                    AddVisibleGateLaneCandidatesV6(result, seen, board, width, height, targetIndex, removed, block, forward, perpA, rayScore, "peelHookA");
                    AddVisibleGateLaneCandidatesV6(result, seen, board, width, height, targetIndex, removed, block, forward, perpB, rayScore, "peelHookB");

                    Vector2Int next = block + forward;
                    Vector2Int next2 = next + forward;
                    TryAddVisibleGatePathV6(result, seen, board, width, height, targetIndex, removed, new[] { next2, next, block }, rayScore + 35, "peelRayReverse3");
                    TryAddVisibleGatePathV6(result, seen, board, width, height, targetIndex, removed, new[] { block, next, next2 }, rayScore - 35, "peelRayForward3");
                    TryAddVisibleGatePathV6(result, seen, board, width, height, targetIndex, removed, new[] { next, block }, rayScore - 80, "peelRayReverse2");
                }
            }

            return result
                .OrderByDescending(c => c.StaticScore)
                .ThenByDescending(c => c.PeelWave)
                .ThenByDescending(c => c.Path.Count)
                .Take(takeLimit)
                .ToList();
        }

        static int[] BuildArrowOwnerMap(AuthoredLevelData authored)
        {
            if (authored == null)
                return null;

            int width = Mathf.Max(1, authored.width);
            int height = Mathf.Max(1, authored.height);
            var owner = Enumerable.Repeat(-1, width * height).ToArray();
            if (authored.arrows == null)
                return owner;

            for (int a = 0; a < authored.arrows.Count; a++)
            {
                AuthoredArrowData arrow = authored.arrows[a];
                if (arrow?.indices == null)
                    continue;
                for (int i = 0; i < arrow.indices.Count; i++)
                {
                    int idx = arrow.indices[i];
                    if ((uint)idx < (uint)owner.Length)
                        owner[idx] = a;
                }
            }

            return owner;
        }

        static void AddVisibleGateLaneCandidatesV6(
            List<VisibleGateCandidate> result,
            HashSet<string> seen,
            BoardState board,
            int width,
            int height,
            int targetIndex,
            EarlyPeelRemoved removed,
            Vector2Int block,
            Vector2Int forward,
            Vector2Int side,
            int baseScore,
            string sideLabel)
        {
            Vector2Int side1 = block + side;
            Vector2Int side2 = side1 + side;
            Vector2Int sideForward = side1 + forward;
            Vector2Int sideBack = side1 - forward;

            TryAddVisibleGatePathV6(result, seen, board, width, height, targetIndex, removed, new[] { side1, block }, baseScore + 95, $"{sideLabel}_side2");
            TryAddVisibleGatePathV6(result, seen, board, width, height, targetIndex, removed, new[] { side2, side1, block }, baseScore + 205, $"{sideLabel}_hook3");
            TryAddVisibleGatePathV6(result, seen, board, width, height, targetIndex, removed, new[] { sideForward, side1, block }, baseScore + 190, $"{sideLabel}_bendF3");
            TryAddVisibleGatePathV6(result, seen, board, width, height, targetIndex, removed, new[] { sideBack, side1, block }, baseScore + 170, $"{sideLabel}_bendB3");
            TryAddVisibleGatePathV6(result, seen, board, width, height, targetIndex, removed, new[] { block, side1, side2 }, baseScore + 115, $"{sideLabel}_hook3_reverse");
        }

        static void TryAddVisibleGatePathV6(
            List<VisibleGateCandidate> result,
            HashSet<string> seen,
            BoardState board,
            int width,
            int height,
            int targetIndex,
            EarlyPeelRemoved removed,
            IReadOnlyList<Vector2Int> points,
            int baseScore,
            string kind)
        {
            int before = result.Count;
            TryAddVisibleGatePathV5(result, seen, board, width, height, targetIndex, points, baseScore, kind);
            if (result.Count <= before)
                return;

            VisibleGateCandidate candidate = result[result.Count - 1];
            candidate.PeelWave = removed.Wave;
            candidate.PeelChoiceCount = removed.WaveChoices;
            candidate.SourceChainId = removed.ChainId;
            candidate.StaticScore += removed.Wave * 220 + Mathf.Min(removed.WaveChoices, 40) * 5;
            result[result.Count - 1] = candidate;
        }

        static void AddVisibleGateLaneCandidatesV5(
            List<VisibleGateCandidate> result,
            HashSet<string> seen,
            BoardState board,
            int width,
            int height,
            int targetIndex,
            Vector2Int block,
            Vector2Int forward,
            Vector2Int side,
            int baseScore,
            string sideLabel)
        {
            Vector2Int side1 = block + side;
            Vector2Int side2 = side1 + side;
            Vector2Int sideForward = side1 + forward;
            Vector2Int sideBack = side1 - forward;

            TryAddVisibleGatePathV5(result, seen, board, width, height, targetIndex, new[] { side1, block }, baseScore + 90, $"{sideLabel}_side2");
            TryAddVisibleGatePathV5(result, seen, board, width, height, targetIndex, new[] { block, side1 }, baseScore + 40, $"{sideLabel}_side2_reverse");
            TryAddVisibleGatePathV5(result, seen, board, width, height, targetIndex, new[] { side2, side1, block }, baseScore + 180, $"{sideLabel}_hook3");
            TryAddVisibleGatePathV5(result, seen, board, width, height, targetIndex, new[] { block, side1, side2 }, baseScore + 125, $"{sideLabel}_hook3_reverse");
            TryAddVisibleGatePathV5(result, seen, board, width, height, targetIndex, new[] { sideForward, side1, block }, baseScore + 165, $"{sideLabel}_bendF3");
            TryAddVisibleGatePathV5(result, seen, board, width, height, targetIndex, new[] { block, side1, sideForward }, baseScore + 120, $"{sideLabel}_bendF3_reverse");
            TryAddVisibleGatePathV5(result, seen, board, width, height, targetIndex, new[] { sideBack, side1, block }, baseScore + 145, $"{sideLabel}_bendB3");
            TryAddVisibleGatePathV5(result, seen, board, width, height, targetIndex, new[] { block, side1, sideBack }, baseScore + 115, $"{sideLabel}_bendB3_reverse");
        }

        static void TryAddVisibleGatePathV5(
            List<VisibleGateCandidate> result,
            HashSet<string> seen,
            BoardState board,
            int width,
            int height,
            int targetIndex,
            IReadOnlyList<Vector2Int> points,
            int baseScore,
            string kind)
        {
            if (points == null || points.Count < 2)
                return;

            var indices = new List<int>(points.Count);
            var unique = new HashSet<int>();
            for (int i = 0; i < points.Count; i++)
            {
                Vector2Int point = points[i];
                if ((uint)point.x >= (uint)width || (uint)point.y >= (uint)height)
                    return;
                int idx = board.Index(point.x, point.y);
                if (board.tiles[idx].type != TileType.Empty)
                    return;
                if (!unique.Add(idx))
                    return;
                if (i > 0 && !IsCardinal(point - points[i - 1]))
                    return;
                indices.Add(idx);
            }

            string key = $"{targetIndex}:{string.Join("-", indices)}";
            if (!seen.Add(key))
                return;

            int turns = CountPathTurns(points);
            int edgeTouch = 0;
            for (int i = 0; i < points.Count; i++)
            {
                Vector2Int point = points[i];
                if (point.x == 0 || point.y == 0 || point.x == width - 1 || point.y == height - 1)
                    edgeTouch++;
            }

            int score = baseScore + points.Count * 80 + turns * 95 + edgeTouch * 20;
            if (turns == 0 && points.Count >= 3)
                score -= 85;
            if (kind.Contains("hook") || kind.Contains("bend"))
                score += 85;

            result.Add(new VisibleGateCandidate
            {
                TargetArrowIndex = targetIndex,
                Path = indices,
                StaticScore = score,
                Kind = kind
            });
        }

        static bool TryApplyVisibleGateCandidateV5(LevelDefinition level, VisibleGateCandidate candidate, out string summary)
        {
            summary = "";
            AuthoredLevelData authored = level?.authoredLevel;
            if (authored?.arrows == null || candidate.Path == null || candidate.Path.Count < 2)
                return false;

            int width = Mathf.Max(1, authored.width);
            if (!IsUniqueContinuousPath(candidate.Path, width))
                return false;

            var occupied = new HashSet<int>();
            if (authored.blockIndices != null)
            {
                for (int i = 0; i < authored.blockIndices.Count; i++)
                    occupied.Add(authored.blockIndices[i]);
            }

            for (int a = 0; a < authored.arrows.Count; a++)
            {
                AuthoredArrowData arrow = authored.arrows[a];
                if (arrow?.indices == null)
                    continue;
                for (int i = 0; i < arrow.indices.Count; i++)
                    occupied.Add(arrow.indices[i]);
            }

            for (int i = 0; i < candidate.Path.Count; i++)
            {
                if (!occupied.Add(candidate.Path[i]))
                    return false;
            }

            authored.arrows.Add(new AuthoredArrowData
            {
                colorIndex = authored.arrows.Count % 6,
                indices = new List<int>(candidate.Path)
            });

            summary = $"{string.Join("-", candidate.Path)}";
            return true;
        }

        static int CountPathTurns(IReadOnlyList<Vector2Int> points)
        {
            if (points == null || points.Count < 3)
                return 0;

            int turns = 0;
            Vector2Int prev = points[1] - points[0];
            for (int i = 2; i < points.Count; i++)
            {
                Vector2Int next = points[i] - points[i - 1];
                if (next != prev)
                    turns++;
                prev = next;
            }

            return turns;
        }

        static ChainInfo BuildChainInfo(int arrowIndex, AuthoredArrowData arrow, BoardState board, int width, int height, bool directClearable, bool opening = false)
        {
            bool edgeTouch = false;
            int sideMask = 0;
            for (int i = 0; i < arrow.indices.Count; i++)
            {
                Vector2Int pos = IndexToPos(arrow.indices[i], width);
                if (pos.x == 0)
                    sideMask |= 1;
                if (pos.x == width - 1)
                    sideMask |= 2;
                if (pos.y == 0)
                    sideMask |= 4;
                if (pos.y == height - 1)
                    sideMask |= 8;
                edgeTouch |= sideMask != 0;
            }

            Vector2Int head = IndexToPos(arrow.indices[0], width);
            Vector2Int neck = IndexToPos(arrow.indices[1], width);
            Vector2Int forward = head - neck;
            bool directOuter = IsCardinal(forward) && TraceHeadRayToExit(board, head, forward, out _);

            return new ChainInfo
            {
                ArrowIndex = arrowIndex,
                Indices = arrow.indices,
                Length = arrow.indices.Count,
                EdgeTouch = edgeTouch,
                SideMask = sideMask,
                DirectOuter = directOuter,
                DirectClearableOuter = directClearable,
                Straight = IsStraight(arrow, width),
                Opening = opening
            };
        }

        static void AddMergeCandidateIfConnected(List<MergeCandidate> result, ChainInfo a, ChainInfo b, bool reverseA, bool reverseB, int totalLength, int width)
        {
            int aStart = reverseA ? a.Indices[a.Indices.Count - 1] : a.Indices[0];
            int aEnd = reverseA ? a.Indices[0] : a.Indices[a.Indices.Count - 1];
            int bStart = reverseB ? b.Indices[b.Indices.Count - 1] : b.Indices[0];
            int bEnd = reverseB ? b.Indices[0] : b.Indices[b.Indices.Count - 1];

            if (!AreAdjacent(aEnd, bStart, width))
                return;

            int score = 0;
            if (a.Opening)
                score += 900;
            if (b.Opening)
                score += 900;
            if (a.DirectClearableOuter)
                score += 700;
            if (b.DirectClearableOuter)
                score += 700;
            if (a.DirectOuter)
                score += 220;
            if (b.DirectOuter)
                score += 220;
            if (a.EdgeTouch)
                score += 120;
            if (b.EdgeTouch)
                score += 120;
            if (a.Straight)
                score += 80;
            if (b.Straight)
                score += 80;
            if ((a.SideMask & b.SideMask) != 0)
                score += 120;
            score += Mathf.Clamp(totalLength, 0, 26);
            score -= Mathf.Abs(a.Length - b.Length) * 2;

            result.Add(new MergeCandidate
            {
                A = a.ArrowIndex,
                B = b.ArrowIndex,
                ReverseA = reverseA,
                ReverseB = reverseB,
                Score = score,
                TotalLength = totalLength,
                StartIndex = aStart,
                JointA = aEnd,
                JointB = bStart,
                EndIndex = bEnd
            });
        }

        static bool TryApplyMergeCandidate(LevelDefinition level, MergeCandidate candidate, out string summary)
        {
            summary = "";
            AuthoredLevelData authored = level?.authoredLevel;
            if (authored?.arrows == null)
                return false;

            int a = candidate.A;
            int b = candidate.B;
            if (a == b || a < 0 || b < 0 || a >= authored.arrows.Count || b >= authored.arrows.Count)
                return false;

            AuthoredArrowData arrowA = authored.arrows[a];
            AuthoredArrowData arrowB = authored.arrows[b];
            if (arrowA?.indices == null || arrowB?.indices == null || arrowA.indices.Count < 2 || arrowB.indices.Count < 2)
                return false;

            List<int> pathA = OrientedCopy(arrowA.indices, candidate.ReverseA);
            List<int> pathB = OrientedCopy(arrowB.indices, candidate.ReverseB);
            int width = Mathf.Max(1, authored.width);
            if (!AreAdjacent(pathA[pathA.Count - 1], pathB[0], width))
                return false;

            var merged = new List<int>(pathA.Count + pathB.Count);
            merged.AddRange(pathA);
            merged.AddRange(pathB);
            if (!IsUniqueContinuousPath(merged, width))
                return false;

            arrowA.indices = merged;
            authored.arrows.RemoveAt(b);
            summary = $"len {pathA.Count}+{pathB.Count}->{merged.Count}";
            return true;
        }

        static List<int> OrientedCopy(IReadOnlyList<int> source, bool reverse)
        {
            var result = new List<int>(source.Count);
            if (reverse)
            {
                for (int i = source.Count - 1; i >= 0; i--)
                    result.Add(source[i]);
            }
            else
            {
                for (int i = 0; i < source.Count; i++)
                    result.Add(source[i]);
            }

            return result;
        }

        static bool IsUniqueContinuousPath(IReadOnlyList<int> path, int width)
        {
            var seen = new HashSet<int>();
            for (int i = 0; i < path.Count; i++)
            {
                if (!seen.Add(path[i]))
                    return false;
                if (i > 0 && !AreAdjacent(path[i - 1], path[i], width))
                    return false;
            }

            return true;
        }

        static bool AreAdjacent(int a, int b, int width)
        {
            Vector2Int pa = IndexToPos(a, width);
            Vector2Int pb = IndexToPos(b, width);
            return IsCardinal(pb - pa);
        }

        static void AttachPackToDemo(LevelPack pack, string logTag)
        {
            if (pack == null)
            {
                Debug.LogWarning($"[{logTag}] Cannot attach null pack to Demo.");
                return;
            }

            var scene = EditorSceneManager.OpenScene(DemoScenePath, OpenSceneMode.Single);
            var progression = UnityEngine.Object.FindFirstObjectByType<LevelProgression>();
            if (progression == null)
            {
                Debug.LogWarning($"[{logTag}] LevelProgression not found in {DemoScenePath}");
                return;
            }

            var so = new SerializedObject(progression);
            SerializedProperty activePack = so.FindProperty("activePack");
            if (activePack == null)
            {
                Debug.LogWarning($"[{logTag}] LevelProgression.activePack serialized field not found.");
                return;
            }

            activePack.objectReferenceValue = pack;
            so.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(progression);
            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
            Debug.Log($"[{logTag}] Demo activePack -> {AssetDatabase.GetAssetPath(pack)}");
        }

        static List<int> FindDirectClearableOuterArrowIndices(LevelDefinition level, IRuleset rules)
        {
            var result = new List<int>();
            AuthoredLevelData authored = level?.authoredLevel;
            if (authored?.arrows == null)
                return result;

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out _))
                return result;
            var legalChainIds = new HashSet<int>(BuildMoveCandidates(board, rules).Select(c => c.ChainId));
            int width = authored.width;
            int height = authored.height;

            var scored = new List<(int ArrowIndex, int Score)>();
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                AuthoredArrowData arrow = authored.arrows[i];
                if (arrow?.indices == null || arrow.indices.Count < 2)
                    continue;

                int chainId = arrow.indices.Min();
                if (!legalChainIds.Contains(chainId))
                    continue;

                Vector2Int head = IndexToPos(arrow.indices[0], width);
                Vector2Int next = IndexToPos(arrow.indices[1], width);
                Vector2Int forward = head - next;
                if (!IsCardinal(forward))
                    continue;
                if (!TraceHeadRayToExit(board, head, forward, out _))
                    continue;

                bool edgeHead = head.x == 0 || head.y == 0 || head.x == width - 1 || head.y == height - 1;
                int score = 0;
                score += edgeHead ? 1000 : 0;
                score += IsStraight(arrow, width) ? 100 : 0;
                score += arrow.indices.Count <= 3 ? 60 : 0;
                score -= Mathf.Min(arrow.indices.Count, 30);
                scored.Add((i, score));
            }

            scored.Sort((a, b) => b.Score.CompareTo(a.Score));
            result.AddRange(scored.Select(s => s.ArrowIndex));
            return result;
        }

        static List<int> FindDirectOuterArrowIndices(LevelDefinition level, IRuleset rules, bool includeNonClearable)
        {
            var result = new List<int>();
            AuthoredLevelData authored = level?.authoredLevel;
            if (authored?.arrows == null)
                return result;

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out _))
                return result;

            var legalChainIds = includeNonClearable
                ? null
                : new HashSet<int>(BuildMoveCandidates(board, rules).Select(c => c.ChainId));

            int width = authored.width;
            int height = authored.height;
            var scored = new List<(int ArrowIndex, int Score)>();
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                AuthoredArrowData arrow = authored.arrows[i];
                if (arrow?.indices == null || arrow.indices.Count < 2)
                    continue;

                int chainId = arrow.indices.Min();
                bool clearable = legalChainIds == null || legalChainIds.Contains(chainId);
                if (!includeNonClearable && !clearable)
                    continue;

                Vector2Int head = IndexToPos(arrow.indices[0], width);
                Vector2Int next = IndexToPos(arrow.indices[1], width);
                Vector2Int forward = head - next;
                if (!IsCardinal(forward))
                    continue;
                if (!TraceHeadRayToExit(board, head, forward, out int emptyRayLength))
                    continue;

                bool edgeHead = head.x == 0 || head.y == 0 || head.x == width - 1 || head.y == height - 1;
                bool edgeTouch = false;
                for (int p = 0; p < arrow.indices.Count; p++)
                {
                    Vector2Int pos = IndexToPos(arrow.indices[p], width);
                    if (pos.x == 0 || pos.y == 0 || pos.x == width - 1 || pos.y == height - 1)
                    {
                        edgeTouch = true;
                        break;
                    }
                }

                int score = 0;
                score += clearable ? 1200 : 0;
                score += edgeHead ? 900 : 0;
                score += edgeTouch ? 300 : 0;
                score += IsStraight(arrow, width) ? 180 : 0;
                score += arrow.indices.Count <= 4 ? 120 : 0;
                score += Mathf.Min(emptyRayLength, 10) * 22;
                score -= Mathf.Min(arrow.indices.Count, 40);
                scored.Add((i, score));
            }

            scored.Sort((a, b) => b.Score.CompareTo(a.Score));
            result.AddRange(scored.Select(s => s.ArrowIndex));
            return result;
        }

        static string FindLatestReport(string pattern)
        {
            string fullFolder = ToAbsolutePath(ReportFolder);
            if (!Directory.Exists(fullFolder))
                return "";

            string file = Directory.GetFiles(fullFolder, pattern)
                .OrderByDescending(File.GetLastWriteTimeUtc)
                .FirstOrDefault();
            return string.IsNullOrEmpty(file)
                ? ""
                : ToAssetPath(file);
        }

        static List<Dictionary<string, string>> ReadCsvRows(string assetPath)
        {
            var rows = new List<Dictionary<string, string>>();
            string fullPath = ToAbsolutePath(assetPath);
            if (!File.Exists(fullPath))
                return rows;

            string[] lines = File.ReadAllLines(fullPath, Encoding.UTF8);
            if (lines.Length == 0)
                return rows;

            var headers = ParseCsvLine(lines[0]);
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                var fields = ParseCsvLine(lines[i]);
                var row = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                for (int c = 0; c < headers.Count; c++)
                    row[headers[c]] = c < fields.Count ? fields[c] : "";
                rows.Add(row);
            }

            return rows;
        }

        static void WriteSandboxReport(string assetPath, IReadOnlyList<SandboxReportRow> rows)
        {
            string[] headers =
            {
                "campaignOrder", "variant", "sourcePath", "assetPath", "acceptedOps", "operations",
                "beforeStatus", "afterStatus", "beforeScore", "afterScore", "deltaScore",
                "beforeChains", "afterChains", "beforeArrowTiles", "afterArrowTiles",
                "beforeOpening", "afterOpening", "beforeEarlyAvg", "afterEarlyAvg",
                "beforeAvgChoices", "afterAvgChoices", "beforeOuter", "afterOuter",
                "beforeDirectOuter", "afterDirectOuter",
                "beforePeelOuter", "afterPeelOuter", "beforeFuturePeelOuter", "afterFuturePeelOuter",
                "beforeDependency", "afterDependency", "greedySolved"
            };

            var lines = new List<string> { string.Join(",", headers) };
            foreach (SandboxReportRow row in rows)
            {
                lines.Add(string.Join(",", new[]
                {
                    row.CampaignOrder.ToString(Inv),
                    EscapeCsv(row.Variant),
                    EscapeCsv(row.SourcePath),
                    EscapeCsv(row.AssetPath),
                    row.AcceptedOps.ToString(Inv),
                    EscapeCsv(row.Operations),
                    EscapeCsv(row.BeforeStatus),
                    EscapeCsv(row.AfterStatus),
                    row.BeforeScore.ToString(Inv),
                    row.AfterScore.ToString(Inv),
                    row.DeltaScore.ToString(Inv),
                    row.BeforeChains.ToString(Inv),
                    row.AfterChains.ToString(Inv),
                    row.BeforeArrowTiles.ToString(Inv),
                    row.AfterArrowTiles.ToString(Inv),
                    row.BeforeOpening.ToString(Inv),
                    row.AfterOpening.ToString(Inv),
                    F(row.BeforeEarlyAvg),
                    F(row.AfterEarlyAvg),
                    F(row.BeforeAvgChoices),
                    F(row.AfterAvgChoices),
                    row.BeforeOuter.ToString(Inv),
                    row.AfterOuter.ToString(Inv),
                    row.BeforeDirectOuter.ToString(Inv),
                    row.AfterDirectOuter.ToString(Inv),
                    row.BeforePeelOuter.ToString(Inv),
                    row.AfterPeelOuter.ToString(Inv),
                    row.BeforeFuturePeelOuter.ToString(Inv),
                    row.AfterFuturePeelOuter.ToString(Inv),
                    F(row.BeforeDependency),
                    F(row.AfterDependency),
                    row.GreedySolved.ToString()
                }));
            }

            File.WriteAllLines(ToAbsolutePath(assetPath), lines, new UTF8Encoding(false));
        }

        static void WritePeelLeakClassificationReportV12(string assetPath, IReadOnlyList<PeelLeakClassificationRowV12> rows)
        {
            string[] headers =
            {
                "campaignOrder", "variant", "assetPath", "status", "greedySolved",
                "chains", "arrowTiles", "openingChoices", "maxChoices", "avgChoices",
                "directOuter", "clearableOuter", "peelOuter", "wave0Outer", "futureOuter",
                "pbeOuter", "pbeFutureOuter", "neeOuter",
                "edgeShortOuter", "boundaryStraightOuter",
                "riskScore", "pbeRiskScore", "neeRiskScore",
                "initialChoices", "finalChoices", "waves",
                "dominantLeak", "recommendation", "summary"
            };

            var lines = new List<string> { string.Join(",", headers) };
            foreach (PeelLeakClassificationRowV12 row in rows)
            {
                lines.Add(string.Join(",", new[]
                {
                    row.CampaignOrder.ToString(Inv),
                    EscapeCsv(row.Variant),
                    EscapeCsv(row.AssetPath),
                    EscapeCsv(row.Status),
                    row.GreedySolved.ToString(),
                    row.Chains.ToString(Inv),
                    row.ArrowTiles.ToString(Inv),
                    row.OpeningChoices.ToString(Inv),
                    row.MaxChoices.ToString(Inv),
                    F(row.AvgChoices),
                    row.DirectOuter.ToString(Inv),
                    row.ClearableOuter.ToString(Inv),
                    row.PeelOuter.ToString(Inv),
                    row.Wave0Outer.ToString(Inv),
                    row.FutureOuter.ToString(Inv),
                    row.PbeOuter.ToString(Inv),
                    row.PbeFutureOuter.ToString(Inv),
                    row.NeeOuter.ToString(Inv),
                    row.EdgeShortOuter.ToString(Inv),
                    row.BoundaryStraightOuter.ToString(Inv),
                    row.RiskScore.ToString(Inv),
                    row.PbeRiskScore.ToString(Inv),
                    row.NeeRiskScore.ToString(Inv),
                    row.InitialChoices.ToString(Inv),
                    row.FinalChoices.ToString(Inv),
                    row.Waves.ToString(Inv),
                    EscapeCsv(row.DominantLeak),
                    EscapeCsv(row.Recommendation),
                    EscapeCsv(row.Summary)
                }));
            }

            File.WriteAllLines(ToAbsolutePath(assetPath), lines, new UTF8Encoding(false));
        }

        static Dictionary<int, Dictionary<string, string>> ReadCsvByOrder(string assetPath)
        {
            var result = new Dictionary<int, Dictionary<string, string>>();
            string fullPath = ToAbsolutePath(assetPath);
            if (!File.Exists(fullPath))
                return result;

            string[] lines = File.ReadAllLines(fullPath);
            if (lines.Length == 0)
                return result;

            var headers = ParseCsvLine(lines[0]);
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                var values = ParseCsvLine(lines[i]);
                var record = new Dictionary<string, string>(StringComparer.Ordinal);
                for (int c = 0; c < headers.Count; c++)
                    record[headers[c]] = c < values.Count ? values[c] : "";

                if (int.TryParse(Get(record, "order"), NumberStyles.Integer, Inv, out int order))
                    result[order] = record;
            }

            return result;
        }

        static List<string> ParseCsvLine(string line)
        {
            var values = new List<string>();
            var sb = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char ch = line[i];
                if (ch == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        sb.Append('"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                    continue;
                }

                if (ch == ',' && !inQuotes)
                {
                    values.Add(sb.ToString());
                    sb.Length = 0;
                    continue;
                }

                sb.Append(ch);
            }

            values.Add(sb.ToString());
            return values;
        }

        static string GetValue(Row row, string header) => header switch
        {
            "packLabel" => row.PackLabel,
            "packPath" => row.PackPath,
            "indexInPack" => row.IndexInPack.ToString(Inv),
            "campaignOrder" => row.CampaignOrder > 0 ? row.CampaignOrder.ToString(Inv) : "",
            "status" => row.Status,
            "leakScore" => row.LeakScore.ToString(Inv),
            "hardeningBudget" => row.HardeningBudget.ToString(Inv),
            "flags" => row.Flags,
            "recommendedOperators" => row.RecommendedOperators,
            "levelId" => row.LevelId,
            "assetPath" => row.AssetPath,
            "bucket" => row.Bucket,
            "type" => row.PlannedType,
            "relativeDifficulty" => row.RelativeDifficulty,
            "slotRole" => row.SlotRole,
            "width" => row.Width.ToString(Inv),
            "height" => row.Height.ToString(Inv),
            "chains" => row.Chains.ToString(Inv),
            "arrowTiles" => row.ArrowTiles.ToString(Inv),
            "blockTiles" => row.BlockTiles.ToString(Inv),
            "boardFill" => F(row.BoardFill),
            "playableFill" => F(row.PlayableFill),
            "avgChainLength" => F(row.AvgChainLength),
            "maxChainLength" => row.MaxChainLength.ToString(Inv),
            "greedySolved" => row.GreedySolved ? "True" : "False",
            "greedySteps" => row.GreedySteps.ToString(Inv),
            "tuningDifficultyScore" => row.TuningDifficultyScore.ToString(Inv),
            "initialClearableChains" => row.InitialClearableChains.ToString(Inv),
            "openingChoices" => row.OpeningChoices.ToString(Inv),
            "openingGoodMoves" => row.OpeningGoodMoves.ToString(Inv),
            "openingGoodMoveRatio" => F(row.OpeningGoodMoveRatio),
            "avgChoices" => F(row.AvgChoices),
            "maxChoices" => row.MaxChoices.ToString(Inv),
            "earlyAvgChoices" => F(row.EarlyAvgChoices),
            "earlyMaxChoices" => row.EarlyMaxChoices.ToString(Inv),
            "bottleneckRatio" => F(row.BottleneckRatio),
            "forcedMoveRatio" => F(row.ForcedMoveRatio),
            "avgClearPerMove" => F(row.AvgClearPerMove),
            "avgNewUnlocksPerMove" => F(row.AvgNewUnlocksPerMove),
            "largestUnlockBurst" => row.LargestUnlockBurst.ToString(Inv),
            "choiceWaveStdDev" => F(row.ChoiceWaveStdDev),
            "averageStepsToNextUnlock" => F(row.AverageStepsToNextUnlock),
            "dependencyDepthProxy" => F(row.DependencyDepthProxy),
            "directOuterExits" => row.DirectOuterExits.ToString(Inv),
            "directClearableOuterExits" => row.DirectClearableOuterExits.ToString(Inv),
            "directOuterExitRatio" => F(row.DirectOuterExitRatio),
            "directClearableOuterExitRatio" => F(row.DirectClearableOuterExitRatio),
            "longRayOuterExits" => row.LongRayOuterExits.ToString(Inv),
            "edgeShortChains" => row.EdgeShortChains.ToString(Inv),
            "edgeShortDirectOuterExits" => row.EdgeShortDirectOuterExits.ToString(Inv),
            "boundaryStraightOuterExits" => row.BoundaryStraightOuterExits.ToString(Inv),
            "earlyEntropyClass" => row.EarlyEntropyClass,
            "entropyCurve" => row.EntropyCurve,
            "details" => row.Details,
            _ => ""
        };

        static readonly string[] Headers =
        {
            "packLabel", "packPath", "indexInPack", "campaignOrder",
            "status", "leakScore", "hardeningBudget", "flags", "recommendedOperators",
            "levelId", "assetPath", "bucket", "type", "relativeDifficulty", "slotRole",
            "width", "height", "chains", "arrowTiles", "blockTiles", "boardFill", "playableFill", "avgChainLength", "maxChainLength",
            "greedySolved", "greedySteps", "tuningDifficultyScore",
            "initialClearableChains", "openingChoices", "openingGoodMoves", "openingGoodMoveRatio",
            "avgChoices", "maxChoices", "earlyAvgChoices", "earlyMaxChoices",
            "bottleneckRatio", "forcedMoveRatio", "avgClearPerMove", "avgNewUnlocksPerMove", "largestUnlockBurst", "choiceWaveStdDev",
            "averageStepsToNextUnlock", "dependencyDepthProxy",
            "directOuterExits", "directClearableOuterExits", "directOuterExitRatio", "directClearableOuterExitRatio",
            "longRayOuterExits", "edgeShortChains", "edgeShortDirectOuterExits", "boundaryStraightOuterExits",
            "earlyEntropyClass", "entropyCurve", "details"
        };

        static int CountArrowTiles(AuthoredLevelData authored)
        {
            int total = 0;
            if (authored?.arrows == null)
                return 0;

            foreach (AuthoredArrowData arrow in authored.arrows)
                total += arrow?.indices?.Count ?? 0;
            return total;
        }

        static int MaxChainLength(AuthoredLevelData authored)
        {
            int max = 0;
            if (authored?.arrows == null)
                return 0;

            foreach (AuthoredArrowData arrow in authored.arrows)
                max = Mathf.Max(max, arrow?.indices?.Count ?? 0);
            return max;
        }

        static bool IsStraight(AuthoredArrowData arrow, int width)
        {
            if (arrow?.indices == null || arrow.indices.Count < 3)
                return true;

            Vector2Int first = IndexToPos(arrow.indices[0], width);
            Vector2Int second = IndexToPos(arrow.indices[1], width);
            Vector2Int dir = second - first;
            if (!IsCardinal(dir))
                return false;

            for (int i = 2; i < arrow.indices.Count; i++)
            {
                Vector2Int prev = IndexToPos(arrow.indices[i - 1], width);
                Vector2Int cur = IndexToPos(arrow.indices[i], width);
                if (cur - prev != dir)
                    return false;
            }

            return true;
        }

        static Vector2Int IndexToPos(int index, int width) => new Vector2Int(index % width, index / width);

        static bool IsCardinal(Vector2Int v) => Mathf.Abs(v.x) + Mathf.Abs(v.y) == 1;

        static int CountCleared(MoveDelta delta)
        {
            int cleared = 0;
            if (delta == null)
                return 0;

            for (int i = 0; i < delta.changes.Count; i++)
            {
                CellChange change = delta.changes[i];
                if (change.before.type == TileType.Arrow && change.after.type == TileType.Empty)
                    cleared++;
            }
            return cleared;
        }

        static int GetChainId(BoardState state, Vector2Int pos, out int chainLength)
        {
            var chain = new HashSet<int>();
            ArrowChainUtility.CollectFullChain(state, pos, 0, chain);
            chainLength = chain.Count;
            int id = int.MaxValue;
            foreach (int idx in chain)
                id = Mathf.Min(id, idx);
            return id == int.MaxValue ? -1 : id;
        }

        static BoardState CloneBoard(BoardState source)
        {
            var clone = new BoardState(source.width, source.height);
            Array.Copy(source.tiles, clone.tiles, source.tiles.Length);
            return clone;
        }

        static string Get(Dictionary<string, string> record, string key)
        {
            return record != null && record.TryGetValue(key, out string value) ? value : "";
        }

        static string EscapeCsv(string value)
        {
            value ??= "";
            if (value.Contains('"') || value.Contains(',') || value.Contains('\n') || value.Contains('\r'))
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            return value;
        }

        static string ToAbsolutePath(string assetPath)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            return Path.GetFullPath(Path.Combine(projectRoot, assetPath.Replace('/', Path.DirectorySeparatorChar)));
        }

        static string ToAssetPath(string fullPath)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            string relative = Path.GetRelativePath(projectRoot, Path.GetFullPath(fullPath));
            return relative.Replace('\\', '/');
        }

        static int ParseInt(string value)
        {
            return int.TryParse(value, NumberStyles.Integer, Inv, out int parsed) ? parsed : 0;
        }

        static string MakeSafeFileName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "level";

            var sb = new StringBuilder(value.Length);
            foreach (char ch in value)
            {
                if (char.IsLetterOrDigit(ch) || ch == '_' || ch == '-')
                    sb.Append(ch);
                else
                    sb.Append('_');
            }

            return sb.ToString();
        }

        static string F(float value) => value.ToString("0.###", Inv);

        public readonly struct PackSource
        {
            public readonly string Label;
            public readonly string PackPath;
            public readonly bool IsCampaignOrdered;

            public PackSource(string label, string packPath, bool isCampaignOrdered)
            {
                Label = label;
                PackPath = packPath;
                IsCampaignOrdered = isCampaignOrdered;
            }
        }

        struct OuterExitStats
        {
            public int DirectOuterExits;
            public int DirectClearableOuterExits;
            public int LongRayOuterExits;
            public int EdgeShortChains;
            public int EdgeShortDirectOuterExits;
            public int BoundaryStraightOuterExits;
        }

        struct ChainInfo
        {
            public int ArrowIndex;
            public List<int> Indices;
            public int Length;
            public bool EdgeTouch;
            public int SideMask;
            public bool DirectOuter;
            public bool DirectClearableOuter;
            public bool Straight;
            public bool Opening;
        }

        struct MergeCandidate
        {
            public int A;
            public int B;
            public bool ReverseA;
            public bool ReverseB;
            public int Score;
            public int TotalLength;
            public int StartIndex;
            public int JointA;
            public int JointB;
            public int EndIndex;
        }

        struct RewireCandidate
        {
            public int A;
            public int B;
            public bool ReverseA;
            public bool ReverseB;
            public List<int> Bridge;
            public int StaticScore;
            public int TotalLength;
            public string Kind;
        }

        struct VisibleGateCandidate
        {
            public int TargetArrowIndex;
            public List<int> Path;
            public int StaticScore;
            public string Kind;
            public int PeelWave;
            public int PeelChoiceCount;
            public int SourceChainId;
        }

        sealed class EarlyPeelResult
        {
            public readonly List<EarlyPeelRemoved> Removed = new List<EarlyPeelRemoved>();
            public int InitialChoices;
            public int FinalChoices;
            public int Waves;
            public int RemovedCount => Removed.Count;
            public string Summary => $"waves={Waves};removed={RemovedCount};choices={InitialChoices}->{FinalChoices}";
        }

        struct EarlyPeelRemoved
        {
            public int ArrowIndex;
            public int ChainId;
            public int Wave;
            public int WaveChoices;
            public int ClearCount;
            public int ChainLength;
        }

        struct PressureSnapshot
        {
            public int Chains;
            public int ArrowTiles;
            public int OpeningChoices;
            public int DirectOuterExits;
            public int DirectClearableOuterExits;
            public int LongRayOuterExits;
            public int EdgeShortChains;
            public int EdgeShortDirectOuterExits;
            public int BoundaryStraightOuterExits;
            public float PressureScore;
        }

        sealed class PressureCandidateChoice
        {
            public MergeCandidate Candidate;
            public AuthoredLevelData Authored;
            public string MergeSummary;
            public float QuickScore;
            public PressureSnapshot QuickAfter;
        }

        sealed class RewireCandidateChoice
        {
            public RewireCandidate Candidate;
            public AuthoredLevelData Authored;
            public string Summary;
            public float QuickScore;
            public PressureSnapshot QuickAfter;
        }

        struct BridgeSearchNode
        {
            public Vector2Int Pos;
            public List<int> Path;
        }

        sealed class GateCandidateChoice
        {
            public VisibleGateCandidate Candidate;
            public AuthoredLevelData Authored;
            public string Summary;
            public float QuickScore;
            public PressureSnapshot QuickAfter;
        }

        struct MoveCandidate
        {
            public Move Move;
            public int ChainId;
            public int ChainLength;
            public int ClearCount;
            public float MoveScore;
        }

        struct TraceStep
        {
            public int AvailableChoices;
            public int GoodMoves;
            public float GoodMoveRatio;
            public int BestClearCount;
            public int ChosenClearCount;
            public float AvgClearCount;
            public int NewlyClearableChains;
        }

        struct SandboxReportRow
        {
            public int CampaignOrder;
            public string Variant;
            public string SourcePath;
            public string AssetPath;
            public int AcceptedOps;
            public string Operations;
            public string BeforeStatus;
            public string AfterStatus;
            public int BeforeScore;
            public int AfterScore;
            public int DeltaScore;
            public int BeforeChains;
            public int AfterChains;
            public int BeforeArrowTiles;
            public int AfterArrowTiles;
            public int BeforeOpening;
            public int AfterOpening;
            public float BeforeEarlyAvg;
            public float AfterEarlyAvg;
            public float BeforeAvgChoices;
            public float AfterAvgChoices;
            public int BeforeOuter;
            public int AfterOuter;
            public int BeforeDirectOuter;
            public int AfterDirectOuter;
            public int BeforePeelOuter;
            public int AfterPeelOuter;
            public int BeforeFuturePeelOuter;
            public int AfterFuturePeelOuter;
            public float BeforeDependency;
            public float AfterDependency;
            public bool GreedySolved;

            public static SandboxReportRow From(
                int campaignOrder,
                string variant,
                string sourcePath,
                string assetPath,
                Row before,
                Row after,
                int acceptedOps,
                string operations)
            {
                return new SandboxReportRow
                {
                    CampaignOrder = campaignOrder,
                    Variant = variant,
                    SourcePath = sourcePath,
                    AssetPath = assetPath,
                    AcceptedOps = acceptedOps,
                    Operations = operations,
                    BeforeStatus = before.Status,
                    AfterStatus = after.Status,
                    BeforeScore = before.LeakScore,
                    AfterScore = after.LeakScore,
                    DeltaScore = after.LeakScore - before.LeakScore,
                    BeforeChains = before.Chains,
                    AfterChains = after.Chains,
                    BeforeArrowTiles = before.ArrowTiles,
                    AfterArrowTiles = after.ArrowTiles,
                    BeforeOpening = before.OpeningChoices,
                    AfterOpening = after.OpeningChoices,
                    BeforeEarlyAvg = before.EarlyAvgChoices,
                    AfterEarlyAvg = after.EarlyAvgChoices,
                    BeforeAvgChoices = before.AvgChoices,
                    AfterAvgChoices = after.AvgChoices,
                    BeforeOuter = before.DirectClearableOuterExits,
                    AfterOuter = after.DirectClearableOuterExits,
                    BeforeDirectOuter = before.DirectOuterExits,
                    AfterDirectOuter = after.DirectOuterExits,
                    BeforeDependency = before.DependencyDepthProxy,
                    AfterDependency = after.DependencyDepthProxy,
                    GreedySolved = after.GreedySolved
                };
            }

            public static SandboxReportRow FromWithPeel(
                int campaignOrder,
                string variant,
                string sourcePath,
                string assetPath,
                Row before,
                Row after,
                int acceptedOps,
                string operations,
                PeelOuterStatsV11 beforePeel,
                PeelOuterStatsV11 afterPeel)
            {
                SandboxReportRow row = From(campaignOrder, variant, sourcePath, assetPath, before, after, acceptedOps, operations);
                row.BeforePeelOuter = beforePeel?.TotalOuter ?? 0;
                row.AfterPeelOuter = afterPeel?.TotalOuter ?? 0;
                row.BeforeFuturePeelOuter = beforePeel?.FutureOuter ?? 0;
                row.AfterFuturePeelOuter = afterPeel?.FutureOuter ?? 0;
                return row;
            }
        }

        sealed class PeelOuterStatsV11
        {
            public readonly List<PeelOuterCandidateV11> Candidates = new List<PeelOuterCandidateV11>();
            public int InitialChoices;
            public int FinalChoices;
            public int Waves;
            public int RemovedCount;
            public int TotalOuter;
            public int Wave0Outer;
            public int FutureOuter;
            public int PersistentOuter;
            public int PersistentFutureOuter;
            public int NewlyExposedOuter;
            public int EdgeShortOuter;
            public int BoundaryStraightOuter;
            public int LongRayOuter;
            public int RiskScore;
            public int PersistentRiskScore;
            public int NewlyExposedRiskScore;
            public string Summary => $"waves={Waves};removed={RemovedCount};choices={InitialChoices}->{FinalChoices};peelOuter={TotalOuter};future={FutureOuter};pbe={PersistentOuter};nee={NewlyExposedOuter};risk={RiskScore}";
        }

        struct PeelOuterCandidateV11
        {
            public int ArrowIndex;
            public int ChainId;
            public int Wave;
            public int WaveChoices;
            public int ClearCount;
            public int ChainLength;
            public int EmptyRayLength;
            public bool EdgeHead;
            public bool EdgeTouch;
            public bool EdgeShort;
            public bool BoundaryStraight;
            public bool InitialDirectOuter;
            public int Score;
        }

        struct PeelLeakClassificationRowV12
        {
            public int CampaignOrder;
            public string Variant;
            public string AssetPath;
            public string Status;
            public bool GreedySolved;
            public int Chains;
            public int ArrowTiles;
            public int OpeningChoices;
            public int MaxChoices;
            public float AvgChoices;
            public int DirectOuter;
            public int ClearableOuter;
            public int PeelOuter;
            public int Wave0Outer;
            public int FutureOuter;
            public int PbeOuter;
            public int PbeFutureOuter;
            public int NeeOuter;
            public int EdgeShortOuter;
            public int BoundaryStraightOuter;
            public int RiskScore;
            public int PbeRiskScore;
            public int NeeRiskScore;
            public int InitialChoices;
            public int FinalChoices;
            public int Waves;
            public string DominantLeak;
            public string Recommendation;
            public string Summary;

            public static PeelLeakClassificationRowV12 Missing(int campaignOrder, string variant, string assetPath)
            {
                return new PeelLeakClassificationRowV12
                {
                    CampaignOrder = campaignOrder,
                    Variant = variant,
                    AssetPath = assetPath,
                    Status = "Missing",
                    Summary = "missing asset"
                };
            }

            public static PeelLeakClassificationRowV12 From(int campaignOrder, string variant, string assetPath, Row row, PeelOuterStatsV11 peel)
            {
                string dominant;
                string recommendation;
                if (peel.PersistentOuter >= peel.NewlyExposedOuter + 3 || peel.PersistentFutureOuter >= peel.NewlyExposedOuter)
                {
                    dominant = "PBE";
                    recommendation = "boundary_structure_repair";
                }
                else if (peel.NewlyExposedOuter >= peel.PersistentFutureOuter + 3)
                {
                    dominant = "NEE";
                    recommendation = "peel_gate_propagation_control";
                }
                else if (peel.FutureOuter > 0)
                {
                    dominant = "Mixed";
                    recommendation = "split_route_pbe_then_nee";
                }
                else
                {
                    dominant = "Low";
                    recommendation = "no_v12_operator_needed";
                }

                return new PeelLeakClassificationRowV12
                {
                    CampaignOrder = campaignOrder,
                    Variant = variant,
                    AssetPath = assetPath,
                    Status = row.Status,
                    GreedySolved = row.GreedySolved,
                    Chains = row.Chains,
                    ArrowTiles = row.ArrowTiles,
                    OpeningChoices = row.OpeningChoices,
                    MaxChoices = row.MaxChoices,
                    AvgChoices = row.AvgChoices,
                    DirectOuter = row.DirectOuterExits,
                    ClearableOuter = row.DirectClearableOuterExits,
                    PeelOuter = peel.TotalOuter,
                    Wave0Outer = peel.Wave0Outer,
                    FutureOuter = peel.FutureOuter,
                    PbeOuter = peel.PersistentOuter,
                    PbeFutureOuter = peel.PersistentFutureOuter,
                    NeeOuter = peel.NewlyExposedOuter,
                    EdgeShortOuter = peel.EdgeShortOuter,
                    BoundaryStraightOuter = peel.BoundaryStraightOuter,
                    RiskScore = peel.RiskScore,
                    PbeRiskScore = peel.PersistentRiskScore,
                    NeeRiskScore = peel.NewlyExposedRiskScore,
                    InitialChoices = peel.InitialChoices,
                    FinalChoices = peel.FinalChoices,
                    Waves = peel.Waves,
                    DominantLeak = dominant,
                    Recommendation = recommendation,
                    Summary = peel.Summary
                };
            }
        }

        struct TraceSummary
        {
            public bool Solved;
            public int StepCount;
            public int OpeningChoices;
            public int OpeningGoodMoves;
            public float OpeningGoodMoveRatio;
            public float AvgChoices;
            public int MaxChoices;
            public float EarlyAvgChoices;
            public int EarlyMaxChoices;
            public float BottleneckRatio;
            public float ForcedMoveRatio;
            public float AvgClearPerMove;
            public float AvgNewUnlocksPerMove;
            public int LargestUnlockBurst;
            public float ChoiceWaveStdDev;
            public string EntropyCurve;

            public static TraceSummary FromSteps(IReadOnlyList<TraceStep> steps, bool solved)
            {
                if (steps == null || steps.Count == 0)
                    return new TraceSummary { Solved = solved, EntropyCurve = "" };

                int count = steps.Count;
                int maxChoices = 0;
                int bottlenecks = 0;
                int forced = 0;
                int largestUnlock = 0;
                float available = 0f;
                float clear = 0f;
                float unlock = 0f;
                int earlyCount = Mathf.Min(10, count);
                float earlyAvailable = 0f;
                int earlyMax = 0;

                for (int i = 0; i < count; i++)
                {
                    TraceStep step = steps[i];
                    available += step.AvailableChoices;
                    clear += step.ChosenClearCount;
                    unlock += step.NewlyClearableChains;
                    maxChoices = Mathf.Max(maxChoices, step.AvailableChoices);
                    largestUnlock = Mathf.Max(largestUnlock, step.NewlyClearableChains);
                    if (step.AvailableChoices <= 2 || step.GoodMoves <= 1 || step.GoodMoveRatio <= 0.22f)
                        bottlenecks++;
                    if (step.AvailableChoices <= 1)
                        forced++;
                    if (i < earlyCount)
                    {
                        earlyAvailable += step.AvailableChoices;
                        earlyMax = Mathf.Max(earlyMax, step.AvailableChoices);
                    }
                }

                float avgAvailable = available / count;
                float variance = 0f;
                for (int i = 0; i < count; i++)
                {
                    float diff = steps[i].AvailableChoices - avgAvailable;
                    variance += diff * diff;
                }
                variance /= count;

                string curve = string.Join(">", steps.Take(16).Select(s => s.AvailableChoices.ToString(Inv)));
                if (steps.Count > 16)
                    curve += ">...";

                TraceStep opening = steps[0];
                return new TraceSummary
                {
                    Solved = solved,
                    StepCount = count,
                    OpeningChoices = opening.AvailableChoices,
                    OpeningGoodMoves = opening.GoodMoves,
                    OpeningGoodMoveRatio = opening.GoodMoveRatio,
                    AvgChoices = avgAvailable,
                    MaxChoices = maxChoices,
                    EarlyAvgChoices = earlyCount > 0 ? earlyAvailable / earlyCount : 0f,
                    EarlyMaxChoices = earlyMax,
                    BottleneckRatio = bottlenecks / (float)count,
                    ForcedMoveRatio = forced / (float)count,
                    AvgClearPerMove = clear / count,
                    AvgNewUnlocksPerMove = unlock / count,
                    LargestUnlockBurst = largestUnlock,
                    ChoiceWaveStdDev = Mathf.Sqrt(variance),
                    EntropyCurve = curve
                };
            }
        }

        sealed class Row
        {
            public string PackLabel = "";
            public string PackPath = "";
            public int IndexInPack;
            public int CampaignOrder;
            public LevelDefinition Level;
            public string LevelId = "";
            public string AssetPath = "";
            public string Bucket = "";
            public string PlannedType = "";
            public string RelativeDifficulty = "";
            public string SlotRole = "";
            public string Status = "Ok";
            public int LeakScore;
            public int HardeningBudget;
            public string Flags = "None";
            public string RecommendedOperators = "none";
            public string Details = "";
            public int Width;
            public int Height;
            public int Chains;
            public int ArrowTiles;
            public int BlockTiles;
            public float BoardFill;
            public float PlayableFill;
            public float AvgChainLength;
            public int MaxChainLength;
            public bool GreedySolved;
            public int GreedySteps;
            public int TuningDifficultyScore;
            public int InitialClearableChains;
            public int OpeningChoices;
            public int OpeningGoodMoves;
            public float OpeningGoodMoveRatio;
            public float AvgChoices;
            public int MaxChoices;
            public float EarlyAvgChoices;
            public int EarlyMaxChoices;
            public float BottleneckRatio;
            public float ForcedMoveRatio;
            public float AvgClearPerMove;
            public float AvgNewUnlocksPerMove;
            public int LargestUnlockBurst;
            public float ChoiceWaveStdDev;
            public float AverageStepsToNextUnlock;
            public float DependencyDepthProxy;
            public int DirectOuterExits;
            public int DirectClearableOuterExits;
            public float DirectOuterExitRatio;
            public float DirectClearableOuterExitRatio;
            public int LongRayOuterExits;
            public int EdgeShortChains;
            public int EdgeShortDirectOuterExits;
            public int BoundaryStraightOuterExits;
            public string EarlyEntropyClass = "";
            public string EntropyCurve = "";
        }
    }
}
#endif
