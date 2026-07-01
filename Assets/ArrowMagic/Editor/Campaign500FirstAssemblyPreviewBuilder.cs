#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using PixelBug.ArrowMagic;
using UnityEditor;
using UnityEngine;

namespace PixelBug.ArrowMagic.EditorTools
{
    public static class Campaign500FirstAssemblyPreviewBuilder
    {
        const string ManifestPath = "Exports/Campaign500_FirstAssembly_20260701_NutationSyncV1/campaign500_first_assembly_v1_500_manifest.csv";
        const string ReportPath = "Exports/Campaign500_FirstAssembly_20260701_NutationSyncV1/campaign500_first_assembly_v1_pack_build_report.csv";
        const string SummaryPath = "Exports/Campaign500_FirstAssembly_20260701_NutationSyncV1/campaign500_first_assembly_v1_pack_build_summary.md";
        const string BasePreviewPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HardGateUntil0910V1FinalPreviewPack.asset";
        const string OutputPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500FirstAssemblyV1PreviewPack.asset";
        const string Front300PriorityV3ManifestPath = "Exports/Campaign500_FirstAssembly_20260701_Front300PriorityV3/campaign500_first_assembly_front300_priority_v3_500_manifest.csv";
        const string Front300PriorityV3ReportPath = "Exports/Campaign500_FirstAssembly_20260701_Front300PriorityV3/campaign500_first_assembly_front300_priority_v3_pack_build_report.csv";
        const string Front300PriorityV3SummaryPath = "Exports/Campaign500_FirstAssembly_20260701_Front300PriorityV3/campaign500_first_assembly_front300_priority_v3_pack_build_summary.md";
        const string Front300PriorityV3OutputPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500FirstAssemblyFront300PriorityV3PreviewPack.asset";
        const string RhythmV4ManifestPath = "Exports/Campaign500_FirstAssembly_20260701_RhythmV4/campaign500_first_assembly_rhythm_v4_500_manifest.csv";
        const string RhythmV4ReportPath = "Exports/Campaign500_FirstAssembly_20260701_RhythmV4/campaign500_first_assembly_rhythm_v4_pack_build_report.csv";
        const string RhythmV4SummaryPath = "Exports/Campaign500_FirstAssembly_20260701_RhythmV4/campaign500_first_assembly_rhythm_v4_pack_build_summary.md";
        const string RhythmV4OutputPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500FirstAssemblyRhythmV4PreviewPack.asset";
        const string RhythmV4FinalManifestPath = "Exports/Campaign500_FirstAssembly_20260701_RhythmV4Final/campaign500_first_assembly_rhythm_v4_final_500_manifest.csv";
        const string RhythmV4FinalReportPath = "Exports/Campaign500_FirstAssembly_20260701_RhythmV4Final/campaign500_first_assembly_rhythm_v4_final_pack_build_report.csv";
        const string RhythmV4FinalSummaryPath = "Exports/Campaign500_FirstAssembly_20260701_RhythmV4Final/campaign500_first_assembly_rhythm_v4_final_pack_build_summary.md";
        const string RhythmV4FinalOutputPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500FirstAssemblyRhythmV4FinalPreviewPack.asset";
        const string RhythmV4FinalStrictCompleteManifestPath = "Exports/Campaign500_FirstAssembly_20260701_RhythmV4FinalStrictComplete/campaign500_first_assembly_rhythm_v4_final_strict_complete_500_manifest.csv";
        const string RhythmV4FinalStrictCompleteReportPath = "Exports/Campaign500_FirstAssembly_20260701_RhythmV4FinalStrictComplete/campaign500_first_assembly_rhythm_v4_final_strict_complete_pack_build_report.csv";
        const string RhythmV4FinalStrictCompleteSummaryPath = "Exports/Campaign500_FirstAssembly_20260701_RhythmV4FinalStrictComplete/campaign500_first_assembly_rhythm_v4_final_strict_complete_pack_build_summary.md";
        const string RhythmV4FinalStrictCompleteOutputPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500FirstAssemblyRhythmV4FinalStrictCompletePreviewPack.asset";
        const string DemoScenePath = "Assets/ArrowMagic/Scenes/Demo.unity";
        const int ExpectedLevelCount = 500;

        static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

        [MenuItem("Tools/Arrow Magic/Campaign 500/Build First Assembly V1 Preview Pack")]
        public static void BuildFirstAssemblyV1PreviewPack()
        {
            BuildPreviewPack(
                ManifestPath,
                ReportPath,
                SummaryPath,
                OutputPackPath,
                "campaign500_first_assembly_v1_preview",
                "Campaign 500 First Assembly V1 Preview (500)",
                "Campaign500FirstAssemblyV1");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Build Front300 Priority V3 Preview Pack")]
        public static void BuildFront300PriorityV3PreviewPack()
        {
            BuildPreviewPack(
                Front300PriorityV3ManifestPath,
                Front300PriorityV3ReportPath,
                Front300PriorityV3SummaryPath,
                Front300PriorityV3OutputPackPath,
                "campaign500_first_assembly_front300_priority_v3_preview",
                "Campaign 500 First Assembly Front300 Priority V3 Preview (500)",
                "Campaign500Front300PriorityV3");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Build Rhythm V4 Preview Pack")]
        public static void BuildRhythmV4PreviewPack()
        {
            BuildPreviewPack(
                RhythmV4ManifestPath,
                RhythmV4ReportPath,
                RhythmV4SummaryPath,
                RhythmV4OutputPackPath,
                "campaign500_first_assembly_rhythm_v4_preview",
                "Campaign 500 First Assembly Rhythm V4 Preview (500)",
                "Campaign500RhythmV4");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Build Rhythm V4 Final Preview Pack")]
        public static void BuildRhythmV4FinalPreviewPack()
        {
            BuildPreviewPack(
                RhythmV4FinalManifestPath,
                RhythmV4FinalReportPath,
                RhythmV4FinalSummaryPath,
                RhythmV4FinalOutputPackPath,
                "campaign500_first_assembly_rhythm_v4_final_preview",
                "Campaign 500 First Assembly Rhythm V4 Final Preview (500)",
                "Campaign500RhythmV4Final");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Build Rhythm V4 Final StrictComplete Preview Pack")]
        public static void BuildRhythmV4FinalStrictCompletePreviewPack()
        {
            BuildPreviewPack(
                RhythmV4FinalStrictCompleteManifestPath,
                RhythmV4FinalStrictCompleteReportPath,
                RhythmV4FinalStrictCompleteSummaryPath,
                RhythmV4FinalStrictCompleteOutputPackPath,
                "campaign500_first_assembly_rhythm_v4_final_strict_complete_preview",
                "Campaign 500 First Assembly Rhythm V4 Final StrictComplete Preview (500)",
                "Campaign500RhythmV4FinalStrictComplete");
        }

        static void BuildPreviewPack(
            string manifestPath,
            string reportPath,
            string summaryPath,
            string outputPackPath,
            string packId,
            string displayName,
            string logTag)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            string manifestAbsPath = ToAbsolutePath(projectRoot, manifestPath);
            var rows = ReadCsv(manifestAbsPath)
                .Select(ManifestRow.FromRecord)
                .OrderBy(row => row.Order)
                .ToList();

            ValidateManifestRows(rows);

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);

            LevelPack basePack = AssetDatabase.LoadAssetAtPath<LevelPack>(BasePreviewPackPath);
            if (basePack == null || basePack.levels == null || basePack.levels.Length < ExpectedLevelCount)
                throw new InvalidOperationException($"Base preview pack must contain {ExpectedLevelCount} levels: {BasePreviewPackPath}");

            var levels = new LevelDefinition[ExpectedLevelCount];
            var reportRows = new List<BuildReportRow>(ExpectedLevelCount);
            var hardErrors = new List<string>();
            int manifestLoaded = 0;
            int baseFallback = 0;
            int activeReplaceRows = 0;
            int remainingGapRows = 0;
            int externalCopyRows = 0;

            foreach (ManifestRow row in rows)
            {
                int index = row.Order - 1;
                if (row.AssemblyAction == "ActiveReplace")
                    activeReplaceRows++;
                if (row.AssemblyRemainingGap)
                    remainingGapRows++;
                if (row.AssemblyCopyRequired)
                    externalCopyRows++;

                LevelDefinition level = null;
                string loadMode = "";
                string loadedAssetPath = "";
                string note = "";

                if (!string.IsNullOrWhiteSpace(row.FinalAssetPath))
                {
                    level = AssetDatabase.LoadAssetAtPath<LevelDefinition>(row.FinalAssetPath);
                    if (level != null)
                    {
                        manifestLoaded++;
                        loadMode = "ManifestAsset";
                        loadedAssetPath = AssetDatabase.GetAssetPath(level);
                    }
                }

                if (level == null)
                {
                    if (!AllowsBaseFallback(row))
                    {
                        hardErrors.Add($"{row.Order:000}:{row.FinalLevelId}:{row.FinalAssetPath}");
                    }
                    else
                    {
                        level = basePack.levels[index];
                        if (level == null)
                        {
                            hardErrors.Add($"{row.Order:000}:base fallback is null");
                        }
                        else
                        {
                            baseFallback++;
                            loadMode = "BasePreviewFallback";
                            loadedAssetPath = AssetDatabase.GetAssetPath(level);
                            note = string.IsNullOrWhiteSpace(row.FinalAssetPath)
                                ? "emptyFinalAssetPath"
                                : "missingFinalAssetPath";
                        }
                    }
                }

                levels[index] = level;
                reportRows.Add(new BuildReportRow
                {
                    Order = row.Order,
                    Category = row.Category,
                    FinalStatus = row.FinalStatus,
                    FinalLevelId = row.FinalLevelId,
                    FinalAssetPath = row.FinalAssetPath,
                    LoadedAssetPath = loadedAssetPath,
                    LoadMode = loadMode,
                    AssemblyAction = row.AssemblyAction,
                    AssemblySourcePool = row.AssemblySourcePool,
                    AssemblyGateClass = row.AssemblyGateClass,
                    AssemblyRemainingGap = row.AssemblyRemainingGap,
                    AssemblyCopyRequired = row.AssemblyCopyRequired,
                    ProductionLane = row.ProductionLane,
                    ProductionStyle = row.AssemblyCandidateStyle,
                    ProductionChainLanguage = row.ProductionChainLanguage,
                    Chains = row.AssemblyCandidateChains,
                    SourceCoverage = row.SourceCoverage,
                    MaxChoices = row.MaxChoices,
                    LocalPatchSolveRunMax = row.LocalPatchSolveRunMax,
                    SolveTraceQualityScore = row.SolveTraceQualityScore,
                    Note = note
                });
            }

            if (hardErrors.Count > 0)
                throw new InvalidOperationException($"FirstAssembly pack has {hardErrors.Count} missing non-fallback levels: {string.Join(" | ", hardErrors.Take(12))}");
            if (levels.Any(level => level == null))
                throw new InvalidOperationException("FirstAssembly pack still contains null level references.");

            LevelPack outputPack = SavePack(outputPackPath, packId, displayName, levels);
            AttachPackToDemo(outputPack);

            WriteReport(ToAbsolutePath(projectRoot, reportPath), reportRows);
            WriteSummary(
                ToAbsolutePath(projectRoot, summaryPath),
                outputPack,
                manifestPath,
                outputPackPath,
                rows.Count,
                manifestLoaded,
                baseFallback,
                activeReplaceRows,
                remainingGapRows,
                externalCopyRows);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            Debug.Log($"[{logTag}] Built {outputPackPath} with {levels.Length} levels. manifestLoaded={manifestLoaded}, baseFallback={baseFallback}");
        }

        static void ValidateManifestRows(IReadOnlyList<ManifestRow> rows)
        {
            if (rows.Count != ExpectedLevelCount)
                throw new InvalidOperationException($"Expected {ExpectedLevelCount} manifest rows, got {rows.Count}");

            var duplicateOrders = rows
                .GroupBy(row => row.Order)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key)
                .ToList();
            if (duplicateOrders.Count > 0)
                throw new InvalidOperationException($"Duplicate manifest orders: {string.Join(", ", duplicateOrders)}");

            for (int order = 1; order <= ExpectedLevelCount; order++)
            {
                if (rows[order - 1].Order != order)
                    throw new InvalidOperationException($"Manifest order gap at {order}: found {rows[order - 1].Order}");
            }
        }

        static bool AllowsBaseFallback(ManifestRow row)
        {
            if (row.AssemblyRemainingGap)
                return true;
            if (row.AssemblyAction == "KeepCurrent")
                return true;
            if (string.IsNullOrWhiteSpace(row.AssemblyAction))
                return true;
            if (row.AssemblyGateClass == "NoStrictCandidateYet")
                return true;
            if (row.AssemblyGateClass == "ShapePendingGeneration")
                return true;
            return false;
        }

        static LevelPack SavePack(string packPath, string packId, string displayName, IReadOnlyList<LevelDefinition> levels)
        {
            string folder = Path.GetDirectoryName(packPath)?.Replace("\\", "/");
            EnsureAssetFolder(folder);

            LevelPack pack = AssetDatabase.LoadAssetAtPath<LevelPack>(packPath);
            bool isNew = pack == null;
            if (pack == null)
                pack = ScriptableObject.CreateInstance<LevelPack>();

            pack.packId = packId;
            pack.displayName = displayName;
            pack.levels = levels.ToArray();
            EditorUtility.SetDirty(pack);

            if (isNew)
                AssetDatabase.CreateAsset(pack, packPath);

            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(packPath, ImportAssetOptions.ForceUpdate);
            return pack;
        }

        static void AttachPackToDemo(LevelPack pack)
        {
            var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(
                DemoScenePath,
                UnityEditor.SceneManagement.OpenSceneMode.Single);
            var progression = UnityEngine.Object.FindFirstObjectByType<LevelProgression>();
            if (progression == null)
                throw new InvalidOperationException($"LevelProgression not found in {DemoScenePath}");

            var so = new SerializedObject(progression);
            var activePack = so.FindProperty("activePack");
            if (activePack == null)
                throw new InvalidOperationException("LevelProgression.activePack serialized field not found.");

            activePack.objectReferenceValue = pack;
            so.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(progression);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(scene);
            UnityEditor.SceneManagement.EditorSceneManager.SaveScene(scene);
            Debug.Log($"[Campaign500FirstAssemblyPreviewBuilder] Attached pack to demo: {AssetDatabase.GetAssetPath(pack)}");
        }

        static void EnsureAssetFolder(string folder)
        {
            if (string.IsNullOrWhiteSpace(folder) || AssetDatabase.IsValidFolder(folder))
                return;

            string[] parts = folder.Split('/');
            string current = parts[0];
            for (int i = 1; i < parts.Length; i++)
            {
                string next = current + "/" + parts[i];
                if (!AssetDatabase.IsValidFolder(next))
                    AssetDatabase.CreateFolder(current, parts[i]);
                current = next;
            }
        }

        static void WriteReport(string path, IReadOnlyList<BuildReportRow> rows)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? ".");
            var lines = new List<string>(rows.Count + 1)
            {
                "order,category,finalStatus,finalLevelId,finalAssetPath,loadedAssetPath,loadMode,assemblyAction,assemblySourcePool,assemblyGateClass,assemblyRemainingGap,assemblyCopyRequired,productionLane,productionStyle,productionChainLanguage,chains,sourceCoverage,maxChoices,localPatchSolveRunMax,solveTraceQualityScore,note"
            };

            foreach (BuildReportRow row in rows)
            {
                string[] values =
                {
                    row.Order.ToString(Inv),
                    row.Category,
                    row.FinalStatus,
                    row.FinalLevelId,
                    row.FinalAssetPath,
                    row.LoadedAssetPath,
                    row.LoadMode,
                    row.AssemblyAction,
                    row.AssemblySourcePool,
                    row.AssemblyGateClass,
                    row.AssemblyRemainingGap ? "True" : "False",
                    row.AssemblyCopyRequired ? "True" : "False",
                    row.ProductionLane,
                    row.ProductionStyle,
                    row.ProductionChainLanguage,
                    row.Chains,
                    row.SourceCoverage,
                    row.MaxChoices,
                    row.LocalPatchSolveRunMax,
                    row.SolveTraceQualityScore,
                    row.Note
                };
                lines.Add(string.Join(",", values.Select(EscapeCsv)));
            }

            File.WriteAllLines(path, lines, new UTF8Encoding(false));
        }

        static void WriteSummary(
            string path,
            LevelPack pack,
            string manifestPath,
            string outputPackPath,
            int totalRows,
            int manifestLoaded,
            int baseFallback,
            int activeReplaceRows,
            int remainingGapRows,
            int externalCopyRows)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? ".");
            var sb = new StringBuilder();
            sb.AppendLine("# Campaign500 First Assembly Pack Build Summary");
            sb.AppendLine();
            sb.AppendLine($"- BuiltAt: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"- Manifest: `{manifestPath}`");
            sb.AppendLine($"- BaseFallbackPack: `{BasePreviewPackPath}`");
            sb.AppendLine($"- OutputPack: `{outputPackPath}`");
            sb.AppendLine($"- OutputPackGuid: `{AssetDatabase.AssetPathToGUID(outputPackPath)}`");
            sb.AppendLine($"- DemoScene: `{DemoScenePath}`");
            sb.AppendLine($"- ManifestRows: {totalRows}");
            sb.AppendLine($"- PackLevels: {pack.levels.Length}");
            sb.AppendLine($"- ActiveReplacementRows: {activeReplaceRows}");
            sb.AppendLine($"- LoadedFromManifestAsset: {manifestLoaded}");
            sb.AppendLine($"- LoadedFromBaseFallback: {baseFallback}");
            sb.AppendLine($"- RemainingGapRowsKeptFromBase: {remainingGapRows}");
            sb.AppendLine($"- ExternalCopyRowsInManifest: {externalCopyRows}");
            sb.AppendLine();
            sb.AppendLine("Rows loaded from the base preview pack are intentional fallback rows for unchanged slots or current candidate gaps.");
            File.WriteAllText(path, sb.ToString(), new UTF8Encoding(false));
        }

        static List<Dictionary<string, string>> ReadCsv(string path)
        {
            var result = new List<Dictionary<string, string>>();
            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            string[] lines = File.ReadAllLines(path);
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
                result.Add(record);
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

        static string Get(Dictionary<string, string> record, string key)
        {
            return record.TryGetValue(key, out string value) ? value : "";
        }

        static int ParseInt(string value)
        {
            return int.TryParse(value, NumberStyles.Integer, Inv, out int result) ? result : 0;
        }

        static bool ParseBool(string value)
        {
            return value.Equals("true", StringComparison.OrdinalIgnoreCase)
                || value.Equals("yes", StringComparison.OrdinalIgnoreCase)
                || value == "1";
        }

        static string ToAbsolutePath(string projectRoot, string path)
        {
            return Path.Combine(projectRoot, path.Replace('/', Path.DirectorySeparatorChar));
        }

        static string EscapeCsv(string value)
        {
            value ??= "";
            if (value.Contains('"') || value.Contains(',') || value.Contains('\n') || value.Contains('\r'))
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            return value;
        }

        sealed class ManifestRow
        {
            public int Order;
            public string Category;
            public string FinalStatus;
            public string FinalLevelId;
            public string FinalAssetPath;
            public string AssemblyAction;
            public string AssemblySourcePool;
            public string AssemblyGateClass;
            public bool AssemblyRemainingGap;
            public bool AssemblyCopyRequired;
            public string ProductionLane;
            public string AssemblyCandidateStyle;
            public string ProductionChainLanguage;
            public string AssemblyCandidateChains;
            public string SourceCoverage;
            public string MaxChoices;
            public string LocalPatchSolveRunMax;
            public string SolveTraceQualityScore;

            public static ManifestRow FromRecord(Dictionary<string, string> record)
            {
                return new ManifestRow
                {
                    Order = ParseInt(Get(record, "order")),
                    Category = Get(record, "category"),
                    FinalStatus = Get(record, "finalStatus"),
                    FinalLevelId = Get(record, "finalLevelId"),
                    FinalAssetPath = Get(record, "finalAssetPath"),
                    AssemblyAction = Get(record, "assemblyAction"),
                    AssemblySourcePool = Get(record, "assemblySourcePool"),
                    AssemblyGateClass = Get(record, "assemblyGateClass"),
                    AssemblyRemainingGap = ParseBool(Get(record, "assemblyRemainingGap")),
                    AssemblyCopyRequired = ParseBool(Get(record, "assemblyCopyRequired")),
                    ProductionLane = Get(record, "productionLane"),
                    AssemblyCandidateStyle = Get(record, "assemblyCandidateStyle"),
                    ProductionChainLanguage = Get(record, "productionChainLanguage"),
                    AssemblyCandidateChains = Get(record, "assemblyCandidateChains"),
                    SourceCoverage = Get(record, "sourceCoverage"),
                    MaxChoices = Get(record, "maxChoices"),
                    LocalPatchSolveRunMax = Get(record, "localPatchSolveRunMax"),
                    SolveTraceQualityScore = Get(record, "solveTraceQualityScore")
                };
            }
        }

        sealed class BuildReportRow
        {
            public int Order;
            public string Category;
            public string FinalStatus;
            public string FinalLevelId;
            public string FinalAssetPath;
            public string LoadedAssetPath;
            public string LoadMode;
            public string AssemblyAction;
            public string AssemblySourcePool;
            public string AssemblyGateClass;
            public bool AssemblyRemainingGap;
            public bool AssemblyCopyRequired;
            public string ProductionLane;
            public string ProductionStyle;
            public string ProductionChainLanguage;
            public string Chains;
            public string SourceCoverage;
            public string MaxChoices;
            public string LocalPatchSolveRunMax;
            public string SolveTraceQualityScore;
            public string Note;
        }
    }
}
#endif
