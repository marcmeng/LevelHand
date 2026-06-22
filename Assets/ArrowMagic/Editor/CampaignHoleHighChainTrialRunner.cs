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
    public static class CampaignHoleHighChainTrialRunner
    {
        const string CampaignPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PlanPreviewPack.asset";
        const string TrialPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HoleHighChainTrialPack.asset";
        const string TrialReviewPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500HoleHighChainTrialReviewPack.asset";
        const string SelectionPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/campaign500_plan_selection.csv";
        const string RhythmPlanPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/campaign500_21_500_relative_peaks_plan.csv";
        const string ReportFolder = "Assets/ArrowMagic/SOData/Reports/Campaign500/HoleHighChainTrial";
        const string TrialPlanPath = "Exports/HoleHighChainSingleLevels_20260613_135235/hole_highchain_trial_slot_plan.csv";
        const string TrialMappingPath = ReportFolder + "/campaign500_hole_highchain_trial_mapping.csv";
        const string TrialSummaryPath = ReportFolder + "/campaign500_hole_highchain_trial_validation_summary.csv";
        const string TrialFlaggedPath = ReportFolder + "/campaign500_hole_highchain_trial_validation_flags.csv";
        const string TrialNotesPath = ReportFolder + "/campaign500_hole_highchain_trial_validation_notes.md";

        static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hole High Chain/Build Trial Pack And Validate")]
        public static void BuildTrialPackAndValidate()
        {
            var replacements = ReadTrialPlan();
            BuildPackWithReplacements(TrialPackPath, replacements, mutateCampaignPack: false);

            CampaignSingleLevelValidator.RunValidationForPack(
                TrialPackPath,
                SelectionPath,
                RhythmPlanPath,
                ReportFolder,
                TrialSummaryPath,
                TrialFlaggedPath,
                TrialNotesPath,
                TrialReviewPackPath,
                "HoleHighChainTrialValidation");

            Debug.Log($"[HoleHighChainTrial] Trial validation done. summary={TrialSummaryPath}, mapping={TrialMappingPath}");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Hole High Chain/Apply Trial Plan To Campaign Pack")]
        public static void ApplyTrialPlanToCampaignPack()
        {
            var replacements = ReadTrialPlan();
            BuildPackWithReplacements(CampaignPackPath, replacements, mutateCampaignPack: true);
            Debug.Log($"[HoleHighChainTrial] Applied high-chain holes to campaign pack. mapping={TrialMappingPath}");
        }

        static void BuildPackWithReplacements(
            string targetPackPath,
            IReadOnlyList<ReplacementRow> replacements,
            bool mutateCampaignPack)
        {
            var sourcePack = AssetDatabase.LoadAssetAtPath<LevelPack>(CampaignPackPath);
            if (sourcePack == null || sourcePack.levels == null || sourcePack.levels.Length == 0)
                throw new InvalidOperationException($"Missing campaign pack: {CampaignPackPath}");

            LevelPack targetPack;
            bool isNew = false;
            if (mutateCampaignPack)
            {
                targetPack = sourcePack;
            }
            else
            {
                targetPack = AssetDatabase.LoadAssetAtPath<LevelPack>(targetPackPath);
                if (targetPack == null)
                {
                    EnsureAssetFolder(Path.GetDirectoryName(targetPackPath)?.Replace('\\', '/') ?? "Assets");
                    targetPack = ScriptableObject.CreateInstance<LevelPack>();
                    isNew = true;
                }
            }

            var levels = sourcePack.levels.ToArray();
            var report = new List<string>
            {
                "targetOrder,candidateOrder,oldLevelId,newLevelId,oldPath,newPath,width,height,chains,note"
            };

            foreach (var replacement in replacements)
            {
                if (replacement.TargetOrder < 1 || replacement.TargetOrder > levels.Length)
                    throw new InvalidOperationException($"Invalid target order: {replacement.TargetOrder}");

                var candidate = AssetDatabase.LoadAssetAtPath<LevelDefinition>(replacement.OriginalAssetPath);
                if (candidate == null)
                    throw new InvalidOperationException($"Missing high-chain hole candidate: {replacement.OriginalAssetPath}");

                var oldLevel = levels[replacement.TargetOrder - 1];
                string oldPath = oldLevel != null ? AssetDatabase.GetAssetPath(oldLevel) : "";
                levels[replacement.TargetOrder - 1] = candidate;

                report.Add(string.Join(",",
                    replacement.TargetOrder.ToString(Inv),
                    replacement.CandidateOrder.ToString(Inv),
                    EscapeCsv(oldLevel != null ? oldLevel.levelId : ""),
                    EscapeCsv(candidate.levelId),
                    EscapeCsv(oldPath),
                    EscapeCsv(AssetDatabase.GetAssetPath(candidate)),
                    GetWidth(candidate).ToString(Inv),
                    GetHeight(candidate).ToString(Inv),
                    GetChains(candidate).ToString(Inv),
                    EscapeCsv(replacement.Note)));
            }

            targetPack.packId = mutateCampaignPack ? "campaign500_plan_preview" : "campaign500_hole_highchain_trial";
            targetPack.displayName = mutateCampaignPack
                ? "Campaign 500 Plan Preview"
                : $"Campaign 500 Hole High-Chain Trial ({replacements.Count})";
            targetPack.levels = levels;

            if (isNew)
                AssetDatabase.CreateAsset(targetPack, targetPackPath);

            EditorUtility.SetDirty(targetPack);
            EnsureAssetFolder(ReportFolder);
            File.WriteAllLines(ToAbsolutePath(TrialMappingPath), report, new UTF8Encoding(false));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        static List<ReplacementRow> ReadTrialPlan()
        {
            string path = ToAbsolutePath(TrialPlanPath);
            if (!File.Exists(path))
                throw new FileNotFoundException($"Missing trial plan: {path}");

            var lines = File.ReadAllLines(path, Encoding.UTF8)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToArray();
            if (lines.Length <= 1)
                throw new InvalidOperationException($"Empty trial plan: {path}");

            var headers = SplitCsvLine(lines[0]);
            var rows = new List<ReplacementRow>();
            for (int i = 1; i < lines.Length; i++)
            {
                var values = SplitCsvLine(lines[i]);
                var row = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                for (int j = 0; j < headers.Count && j < values.Count; j++)
                    row[headers[j]] = values[j];

                rows.Add(new ReplacementRow
                {
                    TargetOrder = ParseInt(Get(row, "targetOrder")),
                    CandidateOrder = ParseInt(Get(row, "candidateOrder")),
                    OriginalAssetPath = Get(row, "originalAssetPath"),
                    Note = Get(row, "note")
                });
            }

            return rows.OrderBy(r => r.TargetOrder).ToList();
        }

        static int GetWidth(LevelDefinition level) => Mathf.Max(0, level?.authoredLevel?.width ?? 0);
        static int GetHeight(LevelDefinition level) => Mathf.Max(0, level?.authoredLevel?.height ?? 0);
        static int GetChains(LevelDefinition level) => level?.authoredLevel?.arrows?.Count ?? 0;

        static int ParseInt(string value)
        {
            if (!int.TryParse(value, NumberStyles.Integer, Inv, out int parsed))
                throw new InvalidOperationException($"Invalid integer value: {value}");
            return parsed;
        }

        static string Get(IReadOnlyDictionary<string, string> row, string key) =>
            row.TryGetValue(key, out string value) ? value : "";

        static List<string> SplitCsvLine(string line)
        {
            var result = new List<string>();
            var sb = new StringBuilder();
            bool inQuotes = false;
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '"')
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
                else if (c == ',' && !inQuotes)
                {
                    result.Add(sb.ToString());
                    sb.Length = 0;
                }
                else
                {
                    sb.Append(c);
                }
            }
            result.Add(sb.ToString());
            return result;
        }

        static void EnsureAssetFolder(string assetFolder)
        {
            if (string.IsNullOrWhiteSpace(assetFolder) || AssetDatabase.IsValidFolder(assetFolder))
                return;

            Directory.CreateDirectory(ToAbsolutePath(assetFolder));
            AssetDatabase.Refresh();
        }

        static string ToAbsolutePath(string assetPath)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            return Path.GetFullPath(Path.Combine(projectRoot, assetPath.Replace('/', Path.DirectorySeparatorChar)));
        }

        static string EscapeCsv(string value)
        {
            value ??= "";
            if (value.Contains('"') || value.Contains(',') || value.Contains('\n') || value.Contains('\r'))
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            return value;
        }

        sealed class ReplacementRow
        {
            public int TargetOrder;
            public int CandidateOrder;
            public string OriginalAssetPath;
            public string Note;
        }
    }
}
#endif
