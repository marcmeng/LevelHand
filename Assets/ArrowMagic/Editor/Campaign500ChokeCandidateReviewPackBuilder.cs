#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PixelBug.ArrowMagic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace PixelBug.ArrowMagic.EditorTools
{
    public static class Campaign500ChokeCandidateReviewPackBuilder
    {
        const string ReviewCsvPath = "Exports/Campaign500_DesignPlanning_20260702/ChokeCandidateAudit_20260702_AllKnown/choke_candidate_audit_recommended_review_set_v1.csv";
        const string OutputPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/ChokeCandidateAuditReviewPack20260702.asset";
        const string ReportPath = "Exports/Campaign500_DesignPlanning_20260702/ChokeCandidateAudit_20260702_AllKnown/choke_candidate_audit_review_pack_build_report.csv";
        const string SummaryPath = "Exports/Campaign500_DesignPlanning_20260702/ChokeCandidateAudit_20260702_AllKnown/choke_candidate_audit_review_pack_build_summary.md";
        const string DemoScenePath = "Assets/ArrowMagic/Scenes/Demo.unity";

        [MenuItem("Tools/Arrow Magic/Campaign 500/Build Choke Candidate Review Pack")]
        public static void BuildChokeCandidateReviewPack()
        {
            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            string csvAbsPath = Path.Combine(projectRoot, ReviewCsvPath.Replace('/', Path.DirectorySeparatorChar));
            var rows = ReadCsv(csvAbsPath)
                .Select(ReviewRow.FromRecord)
                .Where(row => !string.IsNullOrWhiteSpace(row.AssetPath))
                .ToList();

            if (rows.Count == 0)
                throw new InvalidOperationException($"No review rows found: {ReviewCsvPath}");

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);

            var levels = new List<LevelDefinition>(rows.Count);
            var reportRows = new List<BuildReportRow>(rows.Count);
            var missing = new List<string>();

            for (int i = 0; i < rows.Count; i++)
            {
                ReviewRow row = rows[i];
                string assetPath = ToProjectAssetPath(row.AssetPath);
                LevelDefinition level = AssetDatabase.LoadAssetAtPath<LevelDefinition>(assetPath);
                if (level == null)
                {
                    missing.Add($"{i + 1}:{row.LevelId}:{assetPath}");
                    continue;
                }

                levels.Add(level);
                reportRows.Add(new BuildReportRow
                {
                    Index = i + 1,
                    ReviewGroup = row.ReviewGroup,
                    SourceFamily = row.SourceFamily,
                    ChokeClass = row.ChokeClass,
                    ChokeScore = row.ChokeScore,
                    CampaignOrder = row.Order,
                    LevelId = row.LevelId,
                    LoadedAssetPath = assetPath,
                    AvgChoices = row.AvgChoices,
                    MaxChoices = row.MaxChoices,
                    RemoteChokes = row.RemoteChokes,
                    CompositeBreakWindows = row.CompositeBreakWindows,
                    LocalRun = row.LocalRun,
                    RiskTags = row.RiskTags
                });
            }

            if (missing.Count > 0)
                throw new InvalidOperationException($"Missing level assets: {string.Join(" | ", missing.Take(12))}");

            LevelPack pack = SavePack(
                OutputPackPath,
                "campaign500_choke_candidate_review_20260702",
                $"Campaign500 Choke Candidate Review ({levels.Count})",
                levels);
            AttachPackToDemo(pack);

            WriteReport(Path.Combine(projectRoot, ReportPath.Replace('/', Path.DirectorySeparatorChar)), reportRows);
            WriteSummary(Path.Combine(projectRoot, SummaryPath.Replace('/', Path.DirectorySeparatorChar)), pack, reportRows);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            Debug.Log($"[Campaign500ChokeCandidateReviewPack] Built and attached {OutputPackPath}; levels={levels.Count}");
        }

        static string ToProjectAssetPath(string path)
        {
            string normalized = path.Replace('\\', '/');
            int index = normalized.IndexOf("/Assets/", StringComparison.OrdinalIgnoreCase);
            if (index >= 0)
                return normalized.Substring(index + 1);
            if (normalized.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase))
                return normalized;
            throw new InvalidOperationException($"Cannot convert to project asset path: {path}");
        }

        static LevelPack SavePack(string packPath, string packId, string displayName, IReadOnlyList<LevelDefinition> levels)
        {
            EnsureAssetFolder(Path.GetDirectoryName(packPath)?.Replace('\\', '/'));

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
            var scene = EditorSceneManager.OpenScene(DemoScenePath, OpenSceneMode.Single);
            var progression = UnityEngine.Object.FindFirstObjectByType<LevelProgression>();
            if (progression == null)
                throw new InvalidOperationException($"LevelProgression not found in {DemoScenePath}");

            var serialized = new SerializedObject(progression);
            SerializedProperty activePack = serialized.FindProperty("activePack");
            if (activePack == null)
                throw new InvalidOperationException("LevelProgression.activePack serialized field not found.");

            activePack.objectReferenceValue = pack;
            serialized.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(progression);
            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
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
            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine("index,reviewGroup,sourceFamily,chokeClass,chokeScore,campaignOrder,levelId,loadedAssetPath,avgChoices,maxChoices,remoteChokes,compositeBreakWindows,localRun,riskTags");
                foreach (BuildReportRow row in rows)
                {
                    writer.WriteLine(string.Join(",",
                        row.Index.ToString(),
                        Csv(row.ReviewGroup),
                        Csv(row.SourceFamily),
                        Csv(row.ChokeClass),
                        Csv(row.ChokeScore),
                        Csv(row.CampaignOrder),
                        Csv(row.LevelId),
                        Csv(row.LoadedAssetPath),
                        Csv(row.AvgChoices),
                        Csv(row.MaxChoices),
                        Csv(row.RemoteChokes),
                        Csv(row.CompositeBreakWindows),
                        Csv(row.LocalRun),
                        Csv(row.RiskTags)));
                }
            }
        }

        static void WriteSummary(string path, LevelPack pack, IReadOnlyList<BuildReportRow> rows)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? ".");
            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine("# Choke Candidate Review Pack Build");
                writer.WriteLine();
                writer.WriteLine($"- Pack: `{AssetDatabase.GetAssetPath(pack)}`");
                writer.WriteLine($"- DemoScene: `{DemoScenePath}`");
                writer.WriteLine($"- Levels: `{rows.Count}`");
                writer.WriteLine($"- Source CSV: `{ReviewCsvPath}`");
                writer.WriteLine();
                writer.WriteLine("## Levels");
                writer.WriteLine();
                writer.WriteLine("| index | class | score | family | order | levelId | risk |");
                writer.WriteLine("| ---: | --- | ---: | --- | ---: | --- | --- |");
                foreach (BuildReportRow row in rows)
                {
                    writer.WriteLine($"| {row.Index} | {row.ChokeClass} | {row.ChokeScore} | {row.SourceFamily} | {row.CampaignOrder} | `{row.LevelId}` | {row.RiskTags} |");
                }
            }
        }

        static List<Dictionary<string, string>> ReadCsv(string path)
        {
            string[] lines = File.ReadAllLines(path);
            if (lines.Length == 0)
                return new List<Dictionary<string, string>>();

            string[] header = SplitCsvLine(lines[0]).ToArray();
            var rows = new List<Dictionary<string, string>>();
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;
                string[] cells = SplitCsvLine(lines[i]).ToArray();
                var record = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                for (int c = 0; c < header.Length; c++)
                    record[header[c]] = c < cells.Length ? cells[c] : "";
                rows.Add(record);
            }
            return rows;
        }

        static IEnumerable<string> SplitCsvLine(string line)
        {
            var cells = new List<string>();
            var current = new System.Text.StringBuilder();
            bool quoted = false;
            for (int i = 0; i < line.Length; i++)
            {
                char ch = line[i];
                if (ch == '"')
                {
                    if (quoted && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        current.Append('"');
                        i++;
                    }
                    else
                    {
                        quoted = !quoted;
                    }
                }
                else if (ch == ',' && !quoted)
                {
                    cells.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(ch);
                }
            }
            cells.Add(current.ToString());
            return cells;
        }

        static string Csv(string value)
        {
            if (value == null)
                value = "";
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            return value;
        }

        sealed class ReviewRow
        {
            public string ReviewGroup;
            public string SourceFamily;
            public string ChokeClass;
            public string ChokeScore;
            public string Order;
            public string LevelId;
            public string AssetPath;
            public string AvgChoices;
            public string MaxChoices;
            public string RemoteChokes;
            public string CompositeBreakWindows;
            public string LocalRun;
            public string RiskTags;

            public static ReviewRow FromRecord(Dictionary<string, string> record)
            {
                string Get(string key) => record.TryGetValue(key, out string value) ? value : "";
                return new ReviewRow
                {
                    ReviewGroup = Get("reviewGroup"),
                    SourceFamily = Get("sourceFamily"),
                    ChokeClass = Get("chokeClass"),
                    ChokeScore = Get("chokeScore"),
                    Order = Get("order"),
                    LevelId = Get("levelId"),
                    AssetPath = Get("assetPath"),
                    AvgChoices = Get("avgChoices"),
                    MaxChoices = Get("maxChoices"),
                    RemoteChokes = Get("remoteChokes"),
                    CompositeBreakWindows = Get("compositeBreakWindows"),
                    LocalRun = Get("localRun"),
                    RiskTags = Get("riskTags")
                };
            }
        }

        sealed class BuildReportRow
        {
            public int Index;
            public string ReviewGroup;
            public string SourceFamily;
            public string ChokeClass;
            public string ChokeScore;
            public string CampaignOrder;
            public string LevelId;
            public string LoadedAssetPath;
            public string AvgChoices;
            public string MaxChoices;
            public string RemoteChokes;
            public string CompositeBreakWindows;
            public string LocalRun;
            public string RiskTags;
        }
    }
}
#endif
