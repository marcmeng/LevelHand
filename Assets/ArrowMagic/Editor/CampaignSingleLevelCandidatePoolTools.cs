#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace PixelBug.ArrowMagic.EditorTools
{
    public static class CampaignSingleLevelCandidatePoolTools
    {
        const string CandidateFolder = "Assets/ArrowMagic/SOData/Levels/Campaign500SingleLevelPool/CandidatePool";
        const string RejectedFolder = "Assets/ArrowMagic/SOData/Levels/Campaign500SingleLevelPool/Rejected";
        const string CampaignPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PlanPreviewPack.asset";
        const string NoHolePreviewPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PlanPreviewNoHolePack.asset";
        const string ReviewPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/SingleLevelCandidatePoolPack.asset";
        const string SelectionPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/campaign500_plan_selection.csv";
        const string Front20SummaryPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/campaign500_front20_finalized_summary.csv";
        const string NoHolePreviewMapPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/campaign500_nohole_preview_map.csv";
        const string ReportPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/campaign500_single_level_candidate_pool.csv";
        const string FrontPatchReportPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/campaign500_front_2_11_17_local_patch.csv";
        const string DemoScenePath = "Assets/ArrowMagic/Scenes/Demo.unity";
        const string Level3TinyPreviewPackPath = "Assets/ArrowMagic/SOData/Packs/NoMaskProcedural/Level3TinyPreviewPack.asset";
        const string Front2EarlyHolePath = "Assets/ArrowMagic/SOData/Levels/Production/HoleMask/Early/hole_early_16x18_standard_r01_seed_Arrowz_level_035_20260611_143152.asset";
        const string Front11NomaskPath = "Assets/ArrowMagic/SOData/Levels/NoMaskProcedural/Level3ThirtyPreview/level3_thirty_03_level3_thirty_mini_maze_30.asset";
        const string Front17EarlyHolePath = "Assets/ArrowMagic/SOData/Levels/Production/HoleMask/Early30To40/hole_early3040_18x20_standard_r02_r1_ab_091_above300_level_571_final_20260612_163001.asset";

        static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

        [MenuItem("Tools/Arrow Magic/Campaign 500/Single Level Pool/Setup And Import Level3 Tiny")]
        public static void SetupAndImportLevel3Tiny()
        {
            EnsureFolders();

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(Level3TinyPreviewPackPath);
            if (pack == null || pack.levels == null || pack.levels.Length < 3 || pack.levels[2] == null)
                throw new InvalidOperationException($"Missing third level in {Level3TinyPreviewPackPath}");

            string copiedPath = CopyIntoCandidatePool(pack.levels[2], "L003");
            BuildCandidatePoolPackAndReport();

            Debug.Log($"[CampaignSingleLevelCandidatePool] Setup done. Imported={copiedPath}");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Single Level Pool/Patch Front 2, 11, 17 From Approved Sources")]
        public static void PatchFront2_11_17FromApprovedSources()
        {
            EnsureFolders();

            var patches = new[]
            {
                new FrontPatch(2, "L002", Front2EarlyHolePath, "front20", "hole_rescue", "hole_mask_early_front", "restored-early-hole-2"),
                new FrontPatch(11, "L011", Front11NomaskPath, "front20", "normal", "normal_campaign500", "level3-thirty-mini-maze-30"),
                new FrontPatch(17, "L017", Front17EarlyHolePath, "front20", "hole_rescue", "hole_mask_early_30_40", "early30to40-hole-17")
            };

            var campaignPack = AssetDatabase.LoadAssetAtPath<LevelPack>(CampaignPackPath);
            if (campaignPack == null || campaignPack.levels == null || campaignPack.levels.Length < 17)
                throw new InvalidOperationException($"Missing or short campaign pack: {CampaignPackPath}");

            var report = new List<string>
            {
                "order,label,oldLevelId,newLevelId,sourcePath,candidatePath,width,height,chains,arrowTiles,blockTiles,boardFill,playableFill,greedySolved,status"
            };

            foreach (var patch in patches)
            {
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(patch.SourcePath);
                if (source == null)
                    throw new InvalidOperationException($"Missing approved source level: {patch.SourcePath}");

                string candidatePath = CopyIntoCandidatePool(source, patch.Label);
                var row = Analyze(source, new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty }));
                string oldLevelId = campaignPack.levels[patch.Order - 1] != null ? campaignPack.levels[patch.Order - 1].levelId : "";
                campaignPack.levels[patch.Order - 1] = source;

                PatchCsvRow(SelectionPath, patch, source, row);
                PatchCsvRow(Front20SummaryPath, patch, source, row);
                PatchNoHolePreviewIfPresent(patch, source, row);

                report.Add(string.Join(",",
                    patch.Order.ToString(Inv),
                    EscapeCsv(patch.Label),
                    EscapeCsv(oldLevelId),
                    EscapeCsv(source.levelId),
                    EscapeCsv(patch.SourcePath),
                    EscapeCsv(candidatePath),
                    row.Width.ToString(Inv),
                    row.Height.ToString(Inv),
                    row.Chains.ToString(Inv),
                    row.ArrowTiles.ToString(Inv),
                    row.BlockTiles.ToString(Inv),
                    F(row.BoardFill),
                    F(row.PlayableFill),
                    row.GreedySolved ? "True" : "False",
                    EscapeCsv(row.Status)));
            }

            EditorUtility.SetDirty(campaignPack);
            File.WriteAllLines(ToAbsolutePath(FrontPatchReportPath), report, new UTF8Encoding(false));

            BuildCandidatePoolPackAndReport();
            Debug.Log($"[CampaignSingleLevelCandidatePool] Patched front 2/11/17 from approved sources. report={FrontPatchReportPath}");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Single Level Pool/Add Selected Levels To Candidate Pool")]
        public static void AddSelectedLevelsToCandidatePool()
        {
            EnsureFolders();

            int copied = 0;
            foreach (var level in Selection.objects.OfType<LevelDefinition>())
            {
                CopyIntoCandidatePool(level, "manual");
                copied++;
            }

            BuildCandidatePoolPackAndReport();
            Debug.Log($"[CampaignSingleLevelCandidatePool] Added selected levels. copied={copied}");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Single Level Pool/Move Selected Candidates To Rejected")]
        public static void MoveSelectedCandidatesToRejected()
        {
            EnsureFolders();

            int moved = 0;
            foreach (var level in Selection.objects.OfType<LevelDefinition>())
            {
                string path = AssetDatabase.GetAssetPath(level);
                if (string.IsNullOrWhiteSpace(path))
                    continue;

                string normalized = path.Replace('\\', '/');
                if (!normalized.StartsWith(CandidateFolder + "/", StringComparison.OrdinalIgnoreCase))
                    continue;

                string dst = AssetDatabase.GenerateUniqueAssetPath($"{RejectedFolder}/{Path.GetFileName(normalized)}");
                string error = AssetDatabase.MoveAsset(normalized, dst);
                if (!string.IsNullOrWhiteSpace(error))
                    Debug.LogError($"[CampaignSingleLevelCandidatePool] Move failed: {normalized} -> {dst}: {error}");
                else
                    moved++;
            }

            BuildCandidatePoolPackAndReport();
            Debug.Log($"[CampaignSingleLevelCandidatePool] Moved selected candidates to rejected. moved={moved}");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Single Level Pool/Rebuild Candidate Pool Pack And Report")]
        public static void BuildCandidatePoolPackAndReport()
        {
            EnsureFolders();

            var levels = LoadCandidateLevels();
            SavePack(levels);
            WriteReport(levels);

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(ReviewPackPath);
            AttachPackToDemo(pack);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"[CampaignSingleLevelCandidatePool] Rebuilt candidate pool. levels={levels.Count}, pack={ReviewPackPath}, report={ReportPath}");
        }

        static string CopyIntoCandidatePool(LevelDefinition level, string label)
        {
            string source = AssetDatabase.GetAssetPath(level);
            if (string.IsNullOrWhiteSpace(source))
                throw new InvalidOperationException($"Level has no asset path: {level?.levelId}");

            string safeId = Sanitize(level.levelId);
            string dst = $"{CandidateFolder}/candidate_{Sanitize(label)}_{safeId}.asset";
            if (AssetDatabase.LoadAssetAtPath<LevelDefinition>(dst) != null)
                return dst;

            if (!AssetDatabase.CopyAsset(source, dst))
                throw new InvalidOperationException($"Failed to copy candidate level: {source} -> {dst}");

            return dst;
        }

        static void PatchCsvRow(string assetPath, FrontPatch patch, LevelDefinition level, CandidateReportRow metrics)
        {
            string fullPath = ToAbsolutePath(assetPath);
            if (!File.Exists(fullPath))
                return;

            var table = ReadCsvTable(fullPath);
            var row = table.Rows.FirstOrDefault(r => Get(r, "order") == patch.Order.ToString(Inv));
            if (row == null)
                return;

            ApplyPatchRow(row, patch, level, metrics);
            WriteCsvTable(fullPath, table);
        }

        static void PatchNoHolePreviewIfPresent(FrontPatch patch, LevelDefinition level, CandidateReportRow metrics)
        {
            string fullPath = ToAbsolutePath(NoHolePreviewMapPath);
            if (!File.Exists(fullPath))
                return;

            var table = ReadCsvTable(fullPath);
            var row = table.Rows.FirstOrDefault(r => Get(r, "originalOrder") == patch.Order.ToString(Inv));
            if (row == null || !int.TryParse(Get(row, "previewOrder"), NumberStyles.Integer, Inv, out int previewOrder))
                return;

            Set(row, "type", patch.Type);
            Set(row, "levelId", level.levelId);
            Set(row, "path", patch.SourcePath);
            Set(row, "chains", metrics.Chains.ToString(Inv));
            WriteCsvTable(fullPath, table);

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(NoHolePreviewPackPath);
            int index = previewOrder - 1;
            if (pack != null && pack.levels != null && index >= 0 && index < pack.levels.Length)
            {
                pack.levels[index] = level;
                EditorUtility.SetDirty(pack);
            }
        }

        static void ApplyPatchRow(Dictionary<string, string> row, FrontPatch patch, LevelDefinition level, CandidateReportRow metrics)
        {
            int score = Mathf.RoundToInt(metrics.Chains + metrics.BoardFill * 100f);
            Set(row, "bucket", patch.Bucket);
            Set(row, "type", patch.Type);
            Set(row, "score", score.ToString(Inv));
            Set(row, "levelId", level.levelId);
            Set(row, "path", patch.SourcePath);
            Set(row, "assetPath", patch.SourcePath);
            Set(row, "packs", patch.PackId);
            Set(row, "width", metrics.Width.ToString(Inv));
            Set(row, "height", metrics.Height.ToString(Inv));
            Set(row, "chains", metrics.Chains.ToString(Inv));
            Set(row, "tiles", metrics.ArrowTiles.ToString(Inv));
            Set(row, "arrowTiles", metrics.ArrowTiles.ToString(Inv));
            Set(row, "blockTiles", metrics.BlockTiles.ToString(Inv));
            Set(row, "coverage", F(metrics.BoardFill));
            Set(row, "boardFill", F(metrics.BoardFill));
            Set(row, "playableFill", F(metrics.PlayableFill));
            Set(row, "openers", "");
            Set(row, "openingMoves", "");
            Set(row, "avgChoices", "");
            Set(row, "families", patch.Reason);
            Set(row, "qualityFlags", patch.Reason);
            Set(row, "portableSolved", metrics.GreedySolved ? "True" : "False");
            Set(row, "portableScore", score.ToString(Inv));
            Set(row, "portableQuality", patch.Reason);
            Set(row, "selectionReason", patch.Reason);
        }

        static List<LevelDefinition> LoadCandidateLevels()
        {
            string[] guids = AssetDatabase.FindAssets("t:LevelDefinition", new[] { CandidateFolder });
            return guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .OrderBy(p => p, StringComparer.OrdinalIgnoreCase)
                .Select(AssetDatabase.LoadAssetAtPath<LevelDefinition>)
                .Where(l => l != null)
                .ToList();
        }

        static void SavePack(IReadOnlyList<LevelDefinition> levels)
        {
            EnsureAssetFolder(Path.GetDirectoryName(ReviewPackPath)?.Replace('\\', '/'));
            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(ReviewPackPath);
            bool isNew = pack == null;
            if (pack == null)
                pack = ScriptableObject.CreateInstance<LevelPack>();

            pack.packId = "campaign500_single_level_candidate_pool";
            pack.displayName = $"Campaign 500 Single Level Candidate Pool ({levels.Count})";
            pack.levels = levels.Where(l => l != null).ToArray();
            EditorUtility.SetDirty(pack);

            if (isNew)
                AssetDatabase.CreateAsset(pack, ReviewPackPath);
        }

        static void WriteReport(IReadOnlyList<LevelDefinition> levels)
        {
            Directory.CreateDirectory(ToAbsolutePath(Path.GetDirectoryName(ReportPath)?.Replace('\\', '/') ?? "Assets"));

            var lines = new List<string>
            {
                "index,status,levelId,path,width,height,chains,arrowTiles,blockTiles,boardFill,playableFill,greedySolved,details"
            };

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            for (int i = 0; i < levels.Count; i++)
            {
                var level = levels[i];
                string path = AssetDatabase.GetAssetPath(level);
                var row = Analyze(level, rules);
                lines.Add(string.Join(",",
                    (i + 1).ToString(Inv),
                    EscapeCsv(row.Status),
                    EscapeCsv(level != null ? level.levelId : ""),
                    EscapeCsv(path),
                    row.Width.ToString(Inv),
                    row.Height.ToString(Inv),
                    row.Chains.ToString(Inv),
                    row.ArrowTiles.ToString(Inv),
                    row.BlockTiles.ToString(Inv),
                    F(row.BoardFill),
                    F(row.PlayableFill),
                    row.GreedySolved ? "True" : "False",
                    EscapeCsv(row.Details)));
            }

            File.WriteAllLines(ToAbsolutePath(ReportPath), lines, new UTF8Encoding(false));
        }

        static CandidateReportRow Analyze(LevelDefinition level, ArrowMagicRuleset rules)
        {
            var row = new CandidateReportRow();
            if (level == null || level.authoredLevel == null)
            {
                row.Status = "Red";
                row.Details = "missing level or authored data";
                return row;
            }

            var authored = level.authoredLevel;
            row.Width = Mathf.Max(1, authored.width);
            row.Height = Mathf.Max(1, authored.height);
            row.Chains = authored.arrows?.Count ?? 0;
            row.ArrowTiles = CountArrowTiles(authored);
            row.BlockTiles = authored.blockIndices?.Count ?? 0;
            row.BoardFill = row.ArrowTiles / Mathf.Max(1f, row.Width * row.Height);
            row.PlayableFill = row.ArrowTiles / Mathf.Max(1f, row.Width * row.Height - row.BlockTiles);

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out string buildError))
            {
                row.Status = "Red";
                row.Details = $"buildError={buildError}";
                return row;
            }

            row.GreedySolved = GreedyValidator.TryClearAllByGreedy(CloneBoard(board), rules, 1600, out _);
            row.Status = row.GreedySolved ? "Green" : "Red";
            row.Details = row.GreedySolved ? "single-level-greedy-ok" : "single-level-greedy-fail";
            return row;
        }

        static int CountArrowTiles(AuthoredLevelData authored)
        {
            int total = 0;
            if (authored?.arrows == null)
                return total;

            foreach (var arrow in authored.arrows)
                total += arrow?.indices?.Count ?? 0;
            return total;
        }

        static BoardState CloneBoard(BoardState source)
        {
            var clone = new BoardState(source.width, source.height);
            Array.Copy(source.tiles, clone.tiles, source.tiles.Length);
            return clone;
        }

        static void AttachPackToDemo(LevelPack pack)
        {
            if (pack == null || pack.levels == null || pack.levels.Length == 0)
                return;

            var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(
                DemoScenePath,
                UnityEditor.SceneManagement.OpenSceneMode.Single);
            var progression = UnityEngine.Object.FindFirstObjectByType<LevelProgression>();
            if (progression == null)
                return;

            var so = new SerializedObject(progression);
            var activePack = so.FindProperty("activePack");
            if (activePack == null)
                return;

            activePack.objectReferenceValue = pack;
            so.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(progression);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(scene);
            UnityEditor.SceneManagement.EditorSceneManager.SaveScene(scene);
        }

        static void EnsureFolders()
        {
            EnsureAssetFolder(CandidateFolder);
            EnsureAssetFolder(RejectedFolder);
            EnsureAssetFolder(Path.GetDirectoryName(ReviewPackPath)?.Replace('\\', '/'));
            Directory.CreateDirectory(ToAbsolutePath(Path.GetDirectoryName(ReportPath)?.Replace('\\', '/') ?? "Assets"));
        }

        static void EnsureAssetFolder(string folder)
        {
            if (string.IsNullOrWhiteSpace(folder))
                return;

            string normalized = folder.Replace('\\', '/');
            string[] parts = normalized.Split('/');
            string current = parts[0];
            for (int i = 1; i < parts.Length; i++)
            {
                string next = current + "/" + parts[i];
                if (!AssetDatabase.IsValidFolder(next))
                    AssetDatabase.CreateFolder(current, parts[i]);
                current = next;
            }
        }

        static string ToAbsolutePath(string assetPath)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            return Path.GetFullPath(Path.Combine(projectRoot, assetPath.Replace('/', Path.DirectorySeparatorChar)));
        }

        static CsvTable ReadCsvTable(string fullPath)
        {
            var table = new CsvTable();
            if (!File.Exists(fullPath))
                return table;

            var lines = File.ReadAllLines(fullPath);
            if (lines.Length == 0)
                return table;

            table.Headers = ParseCsvLine(lines[0]);
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                var values = ParseCsvLine(lines[i]);
                var row = new Dictionary<string, string>(StringComparer.Ordinal);
                for (int h = 0; h < table.Headers.Count; h++)
                    row[table.Headers[h]] = h < values.Count ? values[h] : "";
                table.Rows.Add(row);
            }

            return table;
        }

        static void WriteCsvTable(string fullPath, CsvTable table)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath) ?? ".");
            var lines = new List<string>
            {
                string.Join(",", table.Headers.Select(EscapeCsv))
            };

            foreach (var row in table.Rows)
            {
                lines.Add(string.Join(",",
                    table.Headers.Select(header => EscapeCsv(row.TryGetValue(header, out string value) ? value : ""))));
            }

            File.WriteAllLines(fullPath, lines, new UTF8Encoding(false));
        }

        static List<string> ParseCsvLine(string line)
        {
            var result = new List<string>();
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
                }
                else if (ch == ',' && !inQuotes)
                {
                    result.Add(sb.ToString());
                    sb.Length = 0;
                }
                else
                {
                    sb.Append(ch);
                }
            }

            result.Add(sb.ToString());
            return result;
        }

        static string Get(Dictionary<string, string> row, string key)
        {
            return row != null && row.TryGetValue(key, out string value) ? value ?? "" : "";
        }

        static void Set(Dictionary<string, string> row, string key, string value)
        {
            if (row == null || string.IsNullOrWhiteSpace(key))
                return;

            row[key] = value ?? "";
        }

        static string Sanitize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "level";

            var chars = value.Select(ch => char.IsLetterOrDigit(ch) || ch == '_' || ch == '-' ? ch : '_').ToArray();
            return new string(chars);
        }

        static string EscapeCsv(string value)
        {
            value ??= "";
            if (value.Contains('"') || value.Contains(',') || value.Contains('\n') || value.Contains('\r'))
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            return value;
        }

        static string F(float value) => value.ToString("0.###", Inv);

        struct CandidateReportRow
        {
            public string Status;
            public int Width;
            public int Height;
            public int Chains;
            public int ArrowTiles;
            public int BlockTiles;
            public float BoardFill;
            public float PlayableFill;
            public bool GreedySolved;
            public string Details;
        }

        sealed class CsvTable
        {
            public List<string> Headers = new List<string>();
            public List<Dictionary<string, string>> Rows = new List<Dictionary<string, string>>();
        }

        readonly struct FrontPatch
        {
            public readonly int Order;
            public readonly string Label;
            public readonly string SourcePath;
            public readonly string Bucket;
            public readonly string Type;
            public readonly string PackId;
            public readonly string Reason;

            public FrontPatch(int order, string label, string sourcePath, string bucket, string type, string packId, string reason)
            {
                Order = order;
                Label = label;
                SourcePath = sourcePath;
                Bucket = bucket;
                Type = type;
                PackId = packId;
                Reason = reason;
            }
        }
    }
}
#endif
