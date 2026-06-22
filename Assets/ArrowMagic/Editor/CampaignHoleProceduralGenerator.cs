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
    public static class CampaignHoleProceduralGenerator
    {
        const string OutputFolder = "Assets/ArrowMagic/SOData/Levels/Production/HoleProcedural/Candidates";
        const string Front20OutputFolder = "Assets/ArrowMagic/SOData/Levels/Production/Front20Polish";
        const string PackFolder = "Assets/ArrowMagic/SOData/Packs/Production/HoleProcedural";
        const string AllPackPath = PackFolder + "/HoleProceduralCandidatePack.asset";
        const string PreviewPackPath = PackFolder + "/HoleProceduralPreviewTop50Pack.asset";
        const string Front20FeedbackPreviewPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500Front20FeedbackPreviewPack.asset";
        const string NoHolePreviewPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PlanPreviewNoHolePack.asset";
        const string Level3TinyPreviewPackPath = "Assets/ArrowMagic/SOData/Packs/NoMaskProcedural/Level3TinyPreviewPack.asset";
        const string HoleReferenceMaskPath = "Assets/ArrowMagic/Masks/hole.png";
        const string ReportFolder = "Assets/ArrowMagic/SOData/Reports/Campaign500";
        const string ReportPath = ReportFolder + "/hole_procedural_candidates_report.csv";
        const string ReplacementReportPath = ReportFolder + "/campaign500_hole_procedural_replacements.csv";
        const string FrontHoleReplacementReportPath = ReportFolder + "/campaign500_front_newbie_hole_replacements.csv";
        const string Front20FeedbackReportPath = ReportFolder + "/campaign500_front20_feedback_replacements.csv";
        const string Front20FinalizedSummaryPath = ReportFolder + "/campaign500_front20_finalized_summary.csv";
        const string HoleReservedSlotsPath = ReportFolder + "/campaign500_hole_slots_reserved.csv";
        const string NoHolePreviewMapPath = ReportFolder + "/campaign500_nohole_preview_map.csv";
        const string Level3TinyPatchReportPath = ReportFolder + "/campaign500_level3_tiny_patch.csv";
        const string YellowReplacementReportPath = ReportFolder + "/campaign500_yellow_replacements.csv";
        const string SingleLevelValidationSummaryPath = ReportFolder + "/campaign500_single_level_validation_summary.csv";
        const string CandidateScorePath = ReportFolder + "/campaign500_candidate_scores.csv";
        const string DifficultyManifestPath = "Assets/ArrowMagic/SOData/Reports/LevelImportV1/level_import_v1_difficulty_manifest.csv";
        const string CampaignPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PlanPreviewPack.asset";
        const string SelectionPath = ReportFolder + "/campaign500_plan_selection.csv";
        const string RhythmPlanPath = ReportFolder + "/campaign500_21_500_relative_peaks_plan.csv";
        const string DemoScenePath = "Assets/ArrowMagic/Scenes/Demo.unity";

        const int TargetAccepted = 50;
        const int PreviewCount = 50;
        const int MaxAttempts = 900;

        static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

        [MenuItem("Tools/Arrow Magic/Campaign 500/Build Procedural Hole Candidates")]
        public static void BuildProceduralHoleCandidates()
        {
            var result = BuildCandidatesAndPacks();
            AttachPackToDemo(result.PreviewPack, "HoleProceduralPreview");
            Debug.Log($"[CampaignHoleProcedural] accepted={result.Accepted.Count}/{result.Attempts}, all={AllPackPath}, preview={PreviewPackPath}, report={ReportPath}");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Build 50 Procedural Holes And Replace Campaign After 20")]
        public static void Build50ProceduralHolesAndReplaceCampaignAfter20()
        {
            var result = BuildCandidatesAndPacks();
            ReplaceCampaignHoles(result.Sorted, includeFront: false);
            CampaignSingleLevelValidator.RunCampaign500Validation();

            var campaignPack = AssetDatabase.LoadAssetAtPath<LevelPack>(CampaignPackPath);
            AttachPackToDemo(campaignPack, "Campaign500ProceduralHoleReplace");
            Debug.Log($"[CampaignHoleProcedural] replaced campaign 20+ holes using generated={result.Sorted.Count}, replacementReport={ReplacementReportPath}");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Build 50 Procedural Holes And Replace All Campaign Holes")]
        public static void Build50ProceduralHolesAndReplaceAllCampaignHoles()
        {
            var result = BuildCandidatesAndPacks();
            ReplaceCampaignHoles(result.Sorted, includeFront: true);
            CampaignSingleLevelValidator.RunCampaign500Validation();

            var campaignPack = AssetDatabase.LoadAssetAtPath<LevelPack>(CampaignPackPath);
            AttachPackToDemo(campaignPack, "Campaign500ProceduralHoleReplaceAll");
            Debug.Log($"[CampaignHoleProcedural] replaced all campaign holes using generated={result.Sorted.Count}, replacementReport={ReplacementReportPath}");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Patch Newbie Holes, Replace Yellow, Validate, Attach")]
        public static void PatchNewbieHolesReplaceYellowValidateAttach()
        {
            EnsureAssetFolder(OutputFolder);
            Directory.CreateDirectory(ToAbsolutePath(ReportFolder));

            var newbieHoles = BuildFrontNewbieHoleCandidates();
            PatchFrontNewbieHoles(newbieHoles);

            if (!File.Exists(ToAbsolutePath(SingleLevelValidationSummaryPath)))
                CampaignSingleLevelValidator.RunCampaign500Validation();

            int yellowReplaced = ReplaceYellowRowsFromPools(skipFrontOrders: true);
            CampaignSingleLevelValidator.RunCampaign500Validation();

            var campaignPack = AssetDatabase.LoadAssetAtPath<LevelPack>(CampaignPackPath);
            AttachPackToDemo(campaignPack, "Campaign500NewbieHoleYellowPatch");
            Debug.Log($"[CampaignHoleProcedural] newbieHoles={newbieHoles.Count}, yellowReplaced={yellowReplaced}, frontReport={FrontHoleReplacementReportPath}, yellowReport={YellowReplacementReportPath}");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Patch Front20 Feedback, Validate, Attach")]
        public static void PatchFront20FeedbackValidateAttach()
        {
            EnsureAssetFolder(OutputFolder);
            EnsureAssetFolder(Front20OutputFolder);
            Directory.CreateDirectory(ToAbsolutePath(ReportFolder));

            var patches = BuildFront20FeedbackCandidates();
            ApplyFront20FeedbackPatches(patches);
            CampaignSingleLevelValidator.RunCampaign500Validation();

            var previewPack = SavePack(
                Front20FeedbackPreviewPackPath,
                "campaign500_front20_feedback_preview",
                $"Campaign 500 Front20 Feedback Preview ({patches.Count})",
                patches.Select(p => p.Candidate).ToList());
            AttachPackToDemo(previewPack, "Campaign500Front20FeedbackPreview");
            Debug.Log($"[CampaignHoleProcedural] front20FeedbackPatched={patches.Count}, preview={Front20FeedbackPreviewPackPath}, report={Front20FeedbackReportPath}");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Export Hole PNG And Empty Hole Slots")]
        public static void ExportHolePngAndEmptyHoleSlots()
        {
            EnsureAssetFolder(PackFolder);
            EnsureAssetFolder(Path.GetDirectoryName(NoHolePreviewPackPath)?.Replace('\\', '/'));
            EnsureAssetFolder(Path.GetDirectoryName(HoleReferenceMaskPath)?.Replace('\\', '/'));
            Directory.CreateDirectory(ToAbsolutePath(ReportFolder));

            ExportReferenceHoleMask();

            var campaignPack = AssetDatabase.LoadAssetAtPath<LevelPack>(CampaignPackPath);
            if (campaignPack == null || campaignPack.levels == null || campaignPack.levels.Length == 0)
                throw new InvalidOperationException($"Missing campaign pack: {CampaignPackPath}");

            var selection = ReadCsvTable(ToAbsolutePath(SelectionPath));
            var holeRows = selection.Rows
                .Where(r => TryInt(Get(r, "order"), out _) && IsHoleRow(r))
                .OrderBy(r => int.Parse(Get(r, "order"), Inv))
                .ToList();

            var holeOrders = new HashSet<int>(
                holeRows.Select(r => int.Parse(Get(r, "order"), Inv)));

            var keptLevels = new List<LevelDefinition>(campaignPack.levels.Length);
            var noHoleMap = new List<string>
            {
                "previewOrder,originalOrder,type,levelId,path,chains"
            };
            int previewOrder = 1;
            for (int i = 0; i < campaignPack.levels.Length; i++)
            {
                int originalOrder = i + 1;
                if (holeOrders.Contains(originalOrder))
                    continue;

                var level = campaignPack.levels[i];
                if (level == null)
                    continue;

                keptLevels.Add(level);
                var row = selection.Rows.FirstOrDefault(r => Get(r, "order") == originalOrder.ToString(Inv));
                noHoleMap.Add(string.Join(",",
                    previewOrder.ToString(Inv),
                    originalOrder.ToString(Inv),
                    EscapeCsv(row != null ? Get(row, "type") : ""),
                    EscapeCsv(level.levelId),
                    EscapeCsv(row != null ? Get(row, "path") : AssetDatabase.GetAssetPath(level)),
                    EscapeCsv(row != null ? Get(row, "chains") : "")));
                previewOrder++;
            }

            var reserved = new List<string>
            {
                "order,type,levelId,path,width,height,chains,bucket,families,qualityFlags"
            };
            foreach (var row in holeRows)
            {
                reserved.Add(string.Join(",",
                    EscapeCsv(Get(row, "order")),
                    EscapeCsv(Get(row, "type")),
                    EscapeCsv(Get(row, "levelId")),
                    EscapeCsv(Get(row, "path")),
                    EscapeCsv(Get(row, "width")),
                    EscapeCsv(Get(row, "height")),
                    EscapeCsv(Get(row, "chains")),
                    EscapeCsv(Get(row, "bucket")),
                    EscapeCsv(Get(row, "families")),
                    EscapeCsv(Get(row, "qualityFlags"))));
            }

            File.WriteAllLines(ToAbsolutePath(HoleReservedSlotsPath), reserved, new UTF8Encoding(false));
            File.WriteAllLines(ToAbsolutePath(NoHolePreviewMapPath), noHoleMap, new UTF8Encoding(false));

            var noHolePack = SaveLevelPack(
                NoHolePreviewPackPath,
                "campaign500_plan_preview_no_hole",
                $"Campaign 500 Plan Preview - Hole Slots Empty ({keptLevels.Count})",
                keptLevels);
            AttachPackToDemo(noHolePack, "Campaign500NoHolePreview");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"[CampaignHoleProcedural] Exported {HoleReferenceMaskPath}; reservedHoleSlots={holeRows.Count}; noHoleLevels={keptLevels.Count}; pack={NoHolePreviewPackPath}; reserved={HoleReservedSlotsPath}");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Patch Formal Level 3 From Tiny Pack")]
        public static void PatchFormalLevel3FromTinyPack()
        {
            Directory.CreateDirectory(ToAbsolutePath(ReportFolder));

            var tinyPack = AssetDatabase.LoadAssetAtPath<LevelPack>(Level3TinyPreviewPackPath);
            if (tinyPack == null || tinyPack.levels == null || tinyPack.levels.Length < 3 || tinyPack.levels[2] == null)
                throw new InvalidOperationException($"Missing third level in {Level3TinyPreviewPackPath}");

            var level = tinyPack.levels[2];
            string levelPath = AssetDatabase.GetAssetPath(level);

            var campaignPack = AssetDatabase.LoadAssetAtPath<LevelPack>(CampaignPackPath);
            if (campaignPack == null || campaignPack.levels == null || campaignPack.levels.Length < 3)
                throw new InvalidOperationException($"Missing or short campaign pack: {CampaignPackPath}");

            string oldCampaignLevel = campaignPack.levels[2] != null ? campaignPack.levels[2].levelId : "";
            campaignPack.levels[2] = level;
            EditorUtility.SetDirty(campaignPack);

            var metrics = MeasureLevel(level);
            PatchCampaignSelectionRowForLevel(3, level, levelPath, metrics, "front20", "normal", "level3_tiny_preview", "level3-tiny-pack-third");
            PatchFront20SummaryRowForLevel(3, level, levelPath, metrics, "refresh", "normal", "level3_tiny_preview", "level3-tiny-pack-third");
            PatchNoHolePreviewLevelAndMap(3, level, levelPath, metrics);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var noHolePack = AssetDatabase.LoadAssetAtPath<LevelPack>(NoHolePreviewPackPath);
            AttachPackToDemo(noHolePack != null ? noHolePack : campaignPack, "Campaign500PatchLevel3Tiny");

            File.WriteAllLines(ToAbsolutePath(Level3TinyPatchReportPath), new[]
            {
                "order,oldLevelId,newLevelId,path,width,height,chains,arrowTiles,coverage,openers,avgClear",
                string.Join(",",
                    "3",
                    EscapeCsv(oldCampaignLevel),
                    EscapeCsv(level.levelId),
                    EscapeCsv(levelPath),
                    metrics.Width.ToString(Inv),
                    metrics.Height.ToString(Inv),
                    metrics.Chains.ToString(Inv),
                    metrics.ArrowTiles.ToString(Inv),
                    F(metrics.BoardFill),
                    metrics.OpeningMoves.ToString(Inv),
                    F(metrics.AvgClearPerOpening))
            }, new UTF8Encoding(false));

            Debug.Log($"[CampaignHoleProcedural] Patched formal level 3: {oldCampaignLevel} -> {level.levelId}; chains={metrics.Chains}; pack={CampaignPackPath}; noHole={NoHolePreviewPackPath}");
        }

        static BuildResult BuildCandidatesAndPacks()
        {
            EnsureAssetFolder(OutputFolder);
            EnsureAssetFolder(PackFolder);
            Directory.CreateDirectory(ToAbsolutePath(ReportFolder));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            var profiles = BuildProfiles();
            var accepted = new List<HoleCandidate>();
            var report = new List<string>
            {
                "rank,status,profile,blocker,levelId,path,width,height,chains,arrowTiles,blockTiles,boardFill,playableFill,openingMoves,avgClearPerOpening,maxChain,seed,score,details"
            };

            int attempt = 0;
            try
            {
                for (int round = 0; accepted.Count < TargetAccepted && attempt < MaxAttempts; round++)
                {
                    HoleProfile profile = profiles[round % profiles.Count];
                    int seed = 910000 + attempt * 37 + profile.Index * 1009;
                    attempt++;

                    if ((attempt % 20) == 1)
                    {
                        EditorUtility.DisplayProgressBar(
                            "Procedural Hole Candidates",
                            $"accepted {accepted.Count}/{TargetAccepted}, attempt {attempt}/{MaxAttempts}",
                            attempt / (float)MaxAttempts);
                    }

                    var candidate = TryBuildCandidate(profile, seed, rules, out string status);
                    if (candidate == null)
                    {
                        report.Add(string.Join(",",
                            "0",
                            "reject",
                            EscapeCsv(profile.Id),
                            "",
                            "",
                            "",
                            profile.Width.ToString(Inv),
                            profile.Height.ToString(Inv),
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            seed.ToString(Inv),
                            "0",
                            EscapeCsv(status)));
                        continue;
                    }

                    candidate.Rank = accepted.Count + 1;
                    SaveCandidateAsset(candidate);
                    accepted.Add(candidate);
                    report.Add(ToReportLine(candidate, "accepted", status));
                }
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }

            var sorted = accepted
                .OrderBy(c => c.Profile.OrderBand)
                .ThenBy(c => c.Profile.Index)
                .ThenBy(c => c.Chains)
                .ThenByDescending(c => c.Score)
                .ToList();
            var preview = SelectDiversePreview(sorted, PreviewCount);

            var allPack = SavePack(AllPackPath, "hole_procedural_candidates", $"Hole Procedural Candidates ({sorted.Count})", sorted);
            var previewPack = SavePack(PreviewPackPath, "hole_procedural_preview_top50", $"Hole Procedural Preview Diverse {preview.Count}", preview);

            File.WriteAllLines(ToAbsolutePath(ReportPath), report, new UTF8Encoding(false));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return new BuildResult
            {
                Accepted = accepted,
                Sorted = sorted,
                Preview = preview,
                AllPack = allPack,
                PreviewPack = previewPack,
                Attempts = attempt
            };
        }

        static HoleCandidate TryBuildCandidate(
            HoleProfile profile,
            int seed,
            ArrowMagicRuleset rules,
            out string status)
        {
            bool[] canSpawn = BuildCanSpawn(profile, seed, out List<int> blocks, out string blockDetails);
            int playableCells = canSpawn.Count(v => v);
            if (playableCells < profile.MinPlayableCells)
            {
                status = $"low playable cells: {playableCells}; {blockDetails}";
                return null;
            }

            if (!TryBuildStandardHoleAuthored(profile, seed, blocks, out AuthoredLevelData authored, out string authoredDetails))
            {
                status = "authored build failed: " + authoredDetails;
                return null;
            }

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState roundTrip, out string buildError))
            {
                status = "roundtrip failed: " + buildError;
                return null;
            }

            if (!GreedyValidator.TryClearAllByGreedy(CloneBoard(roundTrip), rules, profile.GreedyBudget, out _))
            {
                status = "greedy failed: " + authoredDetails;
                return null;
            }

            var metrics = Measure(roundTrip, rules);
            float playableFill = metrics.ArrowTiles / Mathf.Max(1f, profile.Width * profile.Height - metrics.BlockTiles);

            if (metrics.Chains < profile.MinChains || metrics.Chains > profile.MaxChains)
            {
                status = $"chain range miss: {metrics.Chains}, target={profile.MinChains}-{profile.MaxChains}";
                return null;
            }

            if (playableFill < profile.MinPlayableFill)
            {
                status = $"low playable fill: {playableFill:0.000}";
                return null;
            }

            if (metrics.OpeningMoves < profile.MinOpeningMoves)
            {
                status = $"narrow start: {metrics.OpeningMoves}";
                return null;
            }

            if (metrics.AvgClearPerOpening < profile.MinAvgClearPerOpening)
            {
                status = $"low opening clear: {metrics.AvgClearPerOpening:0.000}";
                return null;
            }

            float score =
                playableFill * 120f +
                Mathf.Clamp(metrics.OpeningMoves, 0, 18) * 5f +
                Mathf.Clamp(metrics.AvgClearPerOpening, 0f, 12f) * 8f -
                Mathf.Abs(metrics.Chains - profile.IdealChains) * 1.2f -
                Mathf.Max(0, metrics.MaxChain - profile.MaxChainLen) * 3f;

            string id = $"hole_proc_{profile.Id}_{seed}_{profile.Width}x{profile.Height}_c{metrics.Chains}";
            string assetPath = $"{OutputFolder}/{id}.asset";
            status = "ok";

            return new HoleCandidate
            {
                Profile = profile,
                LevelId = id,
                AssetName = id,
                AssetPath = assetPath,
                Authored = authored,
                Seed = seed,
                Width = profile.Width,
                Height = profile.Height,
                Chains = metrics.Chains,
                ArrowTiles = metrics.ArrowTiles,
                BlockTiles = metrics.BlockTiles,
                BoardFill = metrics.ArrowTiles / Mathf.Max(1f, profile.Width * profile.Height),
                PlayableFill = playableFill,
                OpeningMoves = metrics.OpeningMoves,
                AvgClearPerOpening = metrics.AvgClearPerOpening,
                MaxChain = metrics.MaxChain,
                Score = score,
                BlockerId = "standard_8x9_center",
                Details = blockDetails + "; " + authoredDetails
            };
        }

        static List<HoleProfile> BuildProfiles()
        {
            return new List<HoleProfile>
            {
                Profile(1, "standard_16x18_early", 16, 18, 45, 64, 52, 0.96f, 3, 9, 0.90f, 10, 4.0f, 1500, 0),
                Profile(2, "standard_17x19_early", 17, 19, 46, 68, 55, 0.96f, 3, 10, 0.90f, 10, 4.0f, 1600, 0),
                Profile(3, "standard_18x20_early", 18, 20, 48, 72, 58, 0.96f, 3, 10, 0.90f, 10, 4.1f, 1700, 0),
                Profile(4, "standard_18x22_small", 18, 22, 52, 78, 62, 0.97f, 4, 11, 0.91f, 11, 4.2f, 1800, 1),
                Profile(5, "standard_19x23_small", 19, 23, 55, 84, 66, 0.97f, 4, 11, 0.91f, 11, 4.3f, 1900, 1),
                Profile(6, "standard_20x24_small", 20, 24, 58, 90, 70, 0.97f, 4, 12, 0.91f, 12, 4.4f, 2000, 1),
                Profile(7, "standard_21x25_small", 21, 25, 60, 96, 74, 0.97f, 4, 12, 0.91f, 12, 4.5f, 2200, 1),
                Profile(8, "standard_22x26_mid", 22, 26, 62, 102, 78, 0.98f, 4, 13, 0.92f, 13, 4.6f, 2400, 2),
                Profile(9, "standard_23x28_mid", 23, 28, 70, 112, 86, 0.98f, 4, 13, 0.92f, 13, 4.7f, 2600, 2),
                Profile(10, "standard_24x28_mid", 24, 28, 74, 118, 90, 0.98f, 4, 14, 0.92f, 14, 4.8f, 2800, 2),
                Profile(11, "standard_24x30_mid", 24, 30, 78, 126, 96, 0.98f, 4, 14, 0.92f, 14, 4.9f, 3000, 2),
                Profile(12, "standard_24x32_late", 24, 32, 82, 134, 102, 0.98f, 4, 14, 0.92f, 15, 5.0f, 3200, 3),
                Profile(13, "standard_26x34_late", 26, 34, 92, 150, 116, 0.98f, 4, 15, 0.92f, 16, 5.1f, 3600, 3),
                Profile(14, "standard_28x36_late", 28, 36, 100, 170, 128, 0.98f, 4, 15, 0.92f, 17, 5.2f, 4000, 3)
            };
        }

        static HoleProfile Profile(
            int index,
            string id,
            int width,
            int height,
            int minChains,
            int maxChains,
            int idealChains,
            float coverage,
            int minLen,
            int maxLen,
            float minFill,
            int minOpening,
            float minAvgClear,
            int greedyBudget,
            int orderBand)
        {
            return new HoleProfile
            {
                Index = index,
                Id = id,
                Width = width,
                Height = height,
                MinChains = minChains,
                MaxChains = maxChains,
                IdealChains = idealChains,
                Coverage = coverage,
                MinChainLen = minLen,
                MaxChainLen = maxLen,
                MinPlayableFill = minFill,
                MinOpeningMoves = minOpening,
                MinAvgClearPerOpening = minAvgClear,
                GreedyBudget = greedyBudget,
                OrderBand = orderBand,
                OutwardBias = 0.66f,
                MinPlayableCells = Mathf.RoundToInt(width * height * 0.62f)
            };
        }

        static bool[] BuildCanSpawn(HoleProfile profile, int seed, out List<int> blocks, out string details)
        {
            int width = profile.Width;
            int height = profile.Height;
            var canSpawn = Enumerable.Repeat(true, width * height).ToArray();
            blocks = new List<int>();

            RectInt rect = GetStandardBlockerRect(width, height);

            for (int y = rect.yMin; y < rect.yMax; y++)
            for (int x = rect.xMin; x < rect.xMax; x++)
            {
                int idx = x + y * width;
                canSpawn[idx] = false;
            }

            for (int i = 0; i < canSpawn.Length; i++)
                if (!canSpawn[i])
                    blocks.Add(i);

            details = $"blocker=standard_8x9_center; blockCells={blocks.Count}; rect={rect.x}:{rect.y}:{rect.width}:{rect.height}";
            return canSpawn;
        }

        static RectInt GetStandardBlockerRect(int width, int height)
        {
            const int blockerWidth = 8;
            const int blockerHeight = 9;
            int startX = Mathf.Clamp((width - blockerWidth) / 2, 2, Mathf.Max(2, width - blockerWidth - 2));
            int startY = Mathf.Clamp((height - blockerHeight) / 2, 2, Mathf.Max(2, height - blockerHeight - 2));
            return new RectInt(startX, startY, blockerWidth, blockerHeight);
        }

        static bool TryBuildStandardHoleAuthored(
            HoleProfile profile,
            int seed,
            IReadOnlyList<int> blocks,
            out AuthoredLevelData authored,
            out string details)
        {
            authored = new AuthoredLevelData
            {
                width = profile.Width,
                height = profile.Height,
                blockIndices = new List<int>(blocks),
                arrows = new List<AuthoredArrowData>()
            };

            var rng = new System.Random(seed);
            var occupied = new bool[profile.Width * profile.Height];
            for (int i = 0; i < blocks.Count; i++)
                occupied[blocks[i]] = true;

            RectInt block = GetStandardBlockerRect(profile.Width, profile.Height);
            int playableCells = profile.Width * profile.Height - blocks.Count;
            int desiredSegmentLength = Mathf.Clamp(
                Mathf.RoundToInt(playableCells / (float)Mathf.Max(1, profile.IdealChains)),
                profile.MinChainLen,
                profile.MaxChainLen);

            int before = authored.arrows.Count;
            for (int y = 0; y < profile.Height; y++)
            {
                AddHorizontalLineChains(authored, occupied, rng, 0, block.xMin - 1, y, headAtLeft: true, desiredSegmentLength, profile);
                AddHorizontalLineChains(authored, occupied, rng, block.xMax, profile.Width - 1, y, headAtLeft: false, desiredSegmentLength, profile);
            }

            for (int x = block.xMin; x < block.xMax; x++)
            {
                AddVerticalLineChains(authored, occupied, rng, x, 0, block.yMin - 1, headAtBottom: true, desiredSegmentLength, profile);
                AddVerticalLineChains(authored, occupied, rng, x, block.yMax, profile.Height - 1, headAtBottom: false, desiredSegmentLength, profile);
            }

            SplitLongChains(authored, profile.MinChains, profile.MinChainLen);

            if (authored.arrows.Count < profile.MinChains || authored.arrows.Count > profile.MaxChains)
            {
                details = $"chain count out of range after frame build: {authored.arrows.Count}, target={profile.MinChains}-{profile.MaxChains}, desiredSegment={desiredSegmentLength}";
                authored = null;
                return false;
            }

            details = $"framePeelChains={authored.arrows.Count}; desiredSegment={desiredSegmentLength}; baseChains={before}";
            return true;
        }

        static void AddHorizontalLineChains(
            AuthoredLevelData authored,
            bool[] occupied,
            System.Random rng,
            int xMin,
            int xMax,
            int y,
            bool headAtLeft,
            int desiredSegmentLength,
            HoleProfile profile)
        {
            if (xMax - xMin + 1 < 2)
                return;

            var cells = new List<int>(xMax - xMin + 1);
            if (headAtLeft)
            {
                for (int x = xMin; x <= xMax; x++)
                    cells.Add(x + y * authored.width);
            }
            else
            {
                for (int x = xMax; x >= xMin; x--)
                    cells.Add(x + y * authored.width);
            }

            AddLineChains(authored, occupied, rng, cells, desiredSegmentLength, profile);
        }

        static void AddVerticalLineChains(
            AuthoredLevelData authored,
            bool[] occupied,
            System.Random rng,
            int x,
            int yMin,
            int yMax,
            bool headAtBottom,
            int desiredSegmentLength,
            HoleProfile profile)
        {
            if (yMax - yMin + 1 < 2)
                return;

            var cells = new List<int>(yMax - yMin + 1);
            if (headAtBottom)
            {
                for (int y = yMin; y <= yMax; y++)
                    cells.Add(x + y * authored.width);
            }
            else
            {
                for (int y = yMax; y >= yMin; y--)
                    cells.Add(x + y * authored.width);
            }

            AddLineChains(authored, occupied, rng, cells, desiredSegmentLength, profile);
        }

        static void AddLineChains(
            AuthoredLevelData authored,
            bool[] occupied,
            System.Random rng,
            List<int> cells,
            int desiredSegmentLength,
            HoleProfile profile)
        {
            var chunks = new List<List<int>>();
            int cursor = 0;
            while (cursor < cells.Count)
            {
                int remaining = cells.Count - cursor;
                if (remaining == 1)
                {
                    if (chunks.Count > 0)
                        chunks[chunks.Count - 1].Add(cells[cursor]);
                    break;
                }

                int segmentLength = PickSegmentLength(rng, remaining, desiredSegmentLength, profile);
                if (remaining - segmentLength == 1)
                    segmentLength = remaining;

                var chunk = new List<int>(segmentLength);
                for (int i = 0; i < segmentLength; i++)
                    chunk.Add(cells[cursor + i]);
                chunks.Add(chunk);
                cursor += segmentLength;
            }

            for (int i = 0; i < chunks.Count; i++)
                AddAuthoredChain(authored, occupied, chunks[i]);
        }

        static int PickSegmentLength(System.Random rng, int remaining, int desiredSegmentLength, HoleProfile profile)
        {
            if (remaining <= profile.MaxChainLen && remaining <= desiredSegmentLength + 1)
                return remaining;

            if (remaining <= profile.MaxChainLen && rng.NextDouble() < 0.48)
                return remaining;

            int jitter = rng.Next(-1, 2);
            if (rng.NextDouble() < 0.18)
                jitter += rng.Next(0, 2) == 0 ? -1 : 1;

            return Mathf.Clamp(desiredSegmentLength + jitter, profile.MinChainLen, Mathf.Min(profile.MaxChainLen, remaining));
        }

        static void AddAuthoredChain(AuthoredLevelData authored, bool[] occupied, List<int> indices)
        {
            if (indices == null || indices.Count < 2)
                return;

            for (int i = 0; i < indices.Count; i++)
            {
                int idx = indices[i];
                if (idx < 0 || idx >= occupied.Length || occupied[idx])
                    return;
            }

            for (int i = 0; i < indices.Count; i++)
                occupied[indices[i]] = true;

            authored.arrows.Add(new AuthoredArrowData
            {
                indices = indices,
                colorIndex = authored.arrows.Count
            });
        }

        static void SplitLongChains(AuthoredLevelData authored, int targetChains, int minChainLength)
        {
            for (int i = 0; authored.arrows.Count < targetChains && i < authored.arrows.Count; i++)
            {
                var arrow = authored.arrows[i];
                int length = arrow?.indices?.Count ?? 0;
                if (length < minChainLength * 2)
                    continue;

                int split = length / 2;
                var first = arrow.indices.GetRange(0, split);
                var second = arrow.indices.GetRange(split, length - split);
                if (first.Count < 2 || second.Count < 2)
                    continue;

                arrow.indices = first;
                authored.arrows.Insert(i + 1, new AuthoredArrowData
                {
                    indices = second,
                    colorIndex = authored.arrows.Count
                });
                i++;
            }

            for (int i = 0; i < authored.arrows.Count; i++)
                authored.arrows[i].colorIndex = i;
        }

        static void ReplaceCampaignHoles(IReadOnlyList<HoleCandidate> candidates, bool includeFront)
        {
            if (candidates == null || candidates.Count == 0)
                throw new InvalidOperationException("No procedural hole candidates available for campaign replacement.");

            var campaignPack = AssetDatabase.LoadAssetAtPath<LevelPack>(CampaignPackPath);
            if (campaignPack == null || campaignPack.levels == null || campaignPack.levels.Length < 500)
                throw new InvalidOperationException($"Missing or short campaign pack: {CampaignPackPath}");

            var selection = ReadCsvTable(ToAbsolutePath(SelectionPath));
            var rhythm = ReadCsvTable(ToAbsolutePath(RhythmPlanPath));
            var holeRows = selection.Rows
                .Where(r => TryInt(Get(r, "order"), out int order) && (includeFront || order > 20) && IsHoleRow(r))
                .OrderBy(r => int.Parse(Get(r, "order"), Inv))
                .ToList();

            if (holeRows.Count == 0)
                throw new InvalidOperationException(includeFront ? "No campaign hole rows found." : "No campaign hole rows after level 20 found.");

            var replacements = PickCampaignHoleReplacements(holeRows, candidates);
            var rhythmByOrder = rhythm.Rows
                .Where(r => TryInt(Get(r, "order"), out _))
                .ToDictionary(r => int.Parse(Get(r, "order"), Inv), r => r);

            var replacementReport = new List<string>
            {
                "order,oldLevelId,oldWidth,oldHeight,oldChains,newLevelId,newWidth,newHeight,newChains,newPlayableFill,newOpeningMoves,newProfile,newPath"
            };

            for (int i = 0; i < replacements.Count; i++)
            {
                var replacement = replacements[i];
                var row = replacement.Row;
                var candidate = replacement.Candidate;
                int order = int.Parse(Get(row, "order"), Inv);
                string oldLevelId = Get(row, "levelId");
                string oldWidth = Get(row, "width");
                string oldHeight = Get(row, "height");
                string oldChains = Get(row, "chains");

                campaignPack.levels[order - 1] = candidate.Level;
                ApplyCampaignHoleRow(row, candidate);
                if (rhythmByOrder.TryGetValue(order, out var rhythmRow))
                    ApplyCampaignHoleRow(rhythmRow, candidate);

                replacementReport.Add(string.Join(",",
                    order.ToString(Inv),
                    EscapeCsv(oldLevelId),
                    EscapeCsv(oldWidth),
                    EscapeCsv(oldHeight),
                    EscapeCsv(oldChains),
                    EscapeCsv(candidate.LevelId),
                    candidate.Width.ToString(Inv),
                    candidate.Height.ToString(Inv),
                    candidate.Chains.ToString(Inv),
                    F(candidate.PlayableFill),
                    candidate.OpeningMoves.ToString(Inv),
                    EscapeCsv(candidate.Profile.Id),
                    EscapeCsv(candidate.AssetPath)));
            }

            EditorUtility.SetDirty(campaignPack);
            WriteCsvTable(ToAbsolutePath(SelectionPath), selection);
            WriteCsvTable(ToAbsolutePath(RhythmPlanPath), rhythm);
            File.WriteAllLines(ToAbsolutePath(ReplacementReportPath), replacementReport, new UTF8Encoding(false));

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        static Dictionary<int, HoleCandidate> BuildFrontNewbieHoleCandidates()
        {
            var result = new Dictionary<int, HoleCandidate>
            {
                { 2, BuildFrontNewbieHoleCandidate(2, "newbie_16x18_front20", 16, 18, 930002, 3, 2, 18, 24, 20) },
                { 17, BuildFrontNewbieHoleCandidate(17, "newbie_17x19_front30", 17, 19, 930017, 3, 1, 26, 32, 30) }
            };

            return result;
        }

        static HoleCandidate BuildFrontNewbieHoleCandidate(
            int order,
            string profileId,
            int width,
            int height,
            int seed,
            int rowGroup,
            int columnGroup,
            int minChains,
            int maxChains,
            int idealChains)
        {
            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            var profile = Profile(
                100 + order,
                profileId,
                width,
                height,
                minChains,
                maxChains,
                idealChains,
                1f,
                4,
                18,
                0.99f,
                Mathf.Max(6, minChains / 2),
                7f,
                900,
                0);

            BuildCanSpawn(profile, seed, out List<int> blocks, out string blockDetails);
            if (!TryBuildFrontNewbieHoleAuthored(profile, blocks, rowGroup, columnGroup, out AuthoredLevelData authored, out string authoredDetails))
                throw new InvalidOperationException($"Failed to build front newbie hole {profileId}: {authoredDetails}");

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out string buildError))
                throw new InvalidOperationException($"Front newbie hole roundtrip failed {profileId}: {buildError}");

            if (!GreedyValidator.TryClearAllByGreedy(CloneBoard(board), rules, profile.GreedyBudget, out _))
                throw new InvalidOperationException($"Front newbie hole greedy failed {profileId}: {authoredDetails}");

            var metrics = Measure(board, rules);
            if (metrics.Chains < minChains || metrics.Chains > maxChains)
                throw new InvalidOperationException($"Front newbie hole chain miss {profileId}: {metrics.Chains}, target={minChains}-{maxChains}");

            float playableFill = metrics.ArrowTiles / Mathf.Max(1f, width * height - metrics.BlockTiles);
            if (playableFill < 0.99f)
                throw new InvalidOperationException($"Front newbie hole fill miss {profileId}: {playableFill:0.000}");

            float score =
                playableFill * 120f +
                Mathf.Clamp(metrics.OpeningMoves, 0, 30) * 3.5f +
                Mathf.Clamp(metrics.AvgClearPerOpening, 0f, 18f) * 8f -
                Mathf.Abs(metrics.Chains - idealChains) * 2f;

            string id = $"hole_proc_{profileId}_{width}x{height}_c{metrics.Chains}";
            var candidate = new HoleCandidate
            {
                Rank = order,
                Profile = profile,
                LevelId = id,
                AssetName = id,
                AssetPath = $"{OutputFolder}/{id}.asset",
                Authored = authored,
                Seed = seed,
                Width = width,
                Height = height,
                Chains = metrics.Chains,
                ArrowTiles = metrics.ArrowTiles,
                BlockTiles = metrics.BlockTiles,
                BoardFill = metrics.ArrowTiles / Mathf.Max(1f, width * height),
                PlayableFill = playableFill,
                OpeningMoves = metrics.OpeningMoves,
                AvgClearPerOpening = metrics.AvgClearPerOpening,
                MaxChain = metrics.MaxChain,
                Score = score,
                BlockerId = "standard_8x9_center",
                Details = blockDetails + "; " + authoredDetails
            };

            SaveCandidateAsset(candidate);
            return candidate;
        }

        static bool TryBuildFrontNewbieHoleAuthored(
            HoleProfile profile,
            IReadOnlyList<int> blocks,
            int rowGroup,
            int columnGroup,
            out AuthoredLevelData authored,
            out string details)
        {
            authored = new AuthoredLevelData
            {
                width = profile.Width,
                height = profile.Height,
                blockIndices = new List<int>(blocks),
                arrows = new List<AuthoredArrowData>()
            };

            var occupied = new bool[profile.Width * profile.Height];
            for (int i = 0; i < blocks.Count; i++)
                occupied[blocks[i]] = true;

            RectInt block = GetStandardBlockerRect(profile.Width, profile.Height);
            rowGroup = Mathf.Max(1, rowGroup);
            columnGroup = Mathf.Max(1, columnGroup);

            for (int y = 0; y < profile.Height; y += rowGroup)
            {
                int yMax = Mathf.Min(profile.Height - 1, y + rowGroup - 1);
                AddSerpentineRectChain(authored, occupied, 0, block.xMin - 1, y, yMax, ExitSide.Left);
                AddSerpentineRectChain(authored, occupied, block.xMax, profile.Width - 1, y, yMax, ExitSide.Right);
            }

            for (int x = block.xMin; x < block.xMax; x += columnGroup)
            {
                int xMax = Mathf.Min(block.xMax - 1, x + columnGroup - 1);
                AddSerpentineRectChain(authored, occupied, x, xMax, 0, block.yMin - 1, ExitSide.Bottom);
                AddSerpentineRectChain(authored, occupied, x, xMax, block.yMax, profile.Height - 1, ExitSide.Top);
            }

            int playableCells = profile.Width * profile.Height - blocks.Count;
            int arrowCells = CountArrowTiles(authored);
            if (arrowCells != playableCells)
            {
                details = $"front newbie fill miss: arrows={arrowCells}, playable={playableCells}";
                authored = null;
                return false;
            }

            for (int i = 0; i < authored.arrows.Count; i++)
                authored.arrows[i].colorIndex = i;

            details = $"frontNewbieChains={authored.arrows.Count}; rowGroup={rowGroup}; columnGroup={columnGroup}";
            return true;
        }

        static void AddSerpentineRectChain(
            AuthoredLevelData authored,
            bool[] occupied,
            int xMin,
            int xMax,
            int yMin,
            int yMax,
            ExitSide exitSide)
        {
            if (xMin > xMax || yMin > yMax)
                return;

            var cells = new List<int>((xMax - xMin + 1) * (yMax - yMin + 1));
            switch (exitSide)
            {
                case ExitSide.Left:
                    for (int y = yMin; y <= yMax; y++)
                    {
                        bool forward = ((y - yMin) & 1) == 0;
                        if (forward)
                        {
                            for (int x = xMin; x <= xMax; x++)
                                cells.Add(x + y * authored.width);
                        }
                        else
                        {
                            for (int x = xMax; x >= xMin; x--)
                                cells.Add(x + y * authored.width);
                        }
                    }
                    break;
                case ExitSide.Right:
                    for (int y = yMin; y <= yMax; y++)
                    {
                        bool forward = ((y - yMin) & 1) == 0;
                        if (forward)
                        {
                            for (int x = xMax; x >= xMin; x--)
                                cells.Add(x + y * authored.width);
                        }
                        else
                        {
                            for (int x = xMin; x <= xMax; x++)
                                cells.Add(x + y * authored.width);
                        }
                    }
                    break;
                case ExitSide.Bottom:
                    for (int x = xMin; x <= xMax; x++)
                    {
                        bool forward = ((x - xMin) & 1) == 0;
                        if (forward)
                        {
                            for (int y = yMin; y <= yMax; y++)
                                cells.Add(x + y * authored.width);
                        }
                        else
                        {
                            for (int y = yMax; y >= yMin; y--)
                                cells.Add(x + y * authored.width);
                        }
                    }
                    break;
                case ExitSide.Top:
                    for (int x = xMin; x <= xMax; x++)
                    {
                        bool forward = ((x - xMin) & 1) == 0;
                        if (forward)
                        {
                            for (int y = yMax; y >= yMin; y--)
                                cells.Add(x + y * authored.width);
                        }
                        else
                        {
                            for (int y = yMin; y <= yMax; y++)
                                cells.Add(x + y * authored.width);
                        }
                    }
                    break;
            }

            AddAuthoredChain(authored, occupied, cells);
        }

        static void PatchFrontNewbieHoles(IReadOnlyDictionary<int, HoleCandidate> frontHoles)
        {
            var campaignPack = AssetDatabase.LoadAssetAtPath<LevelPack>(CampaignPackPath);
            if (campaignPack == null || campaignPack.levels == null || campaignPack.levels.Length < 20)
                throw new InvalidOperationException($"Missing or short campaign pack: {CampaignPackPath}");

            var selection = ReadCsvTable(ToAbsolutePath(SelectionPath));
            var rhythm = ReadCsvTable(ToAbsolutePath(RhythmPlanPath));
            var selectionByOrder = selection.Rows
                .Where(r => TryInt(Get(r, "order"), out _))
                .ToDictionary(r => int.Parse(Get(r, "order"), Inv), r => r);
            var rhythmByOrder = rhythm.Rows
                .Where(r => TryInt(Get(r, "order"), out _))
                .ToDictionary(r => int.Parse(Get(r, "order"), Inv), r => r);

            var report = new List<string>
            {
                "order,oldLevelId,oldChains,newLevelId,newChains,newWidth,newHeight,newPath"
            };

            foreach (var pair in frontHoles.OrderBy(p => p.Key))
            {
                int order = pair.Key;
                var candidate = pair.Value;
                string oldLevelId = campaignPack.levels[order - 1] != null ? campaignPack.levels[order - 1].levelId : "";
                string oldChains = "";

                campaignPack.levels[order - 1] = candidate.Level;
                if (selectionByOrder.TryGetValue(order, out var row))
                {
                    oldChains = Get(row, "chains");
                    ApplyCampaignHoleRow(row, candidate);
                    ApplyNewbieHoleRowTag(row, candidate);
                }

                if (rhythmByOrder.TryGetValue(order, out var rhythmRow))
                {
                    ApplyCampaignHoleRow(rhythmRow, candidate);
                    ApplyNewbieHoleRowTag(rhythmRow, candidate);
                    Set(rhythmRow, "note", AppendNote(Get(rhythmRow, "note"), "newbie-hole-front"));
                }

                report.Add(string.Join(",",
                    order.ToString(Inv),
                    EscapeCsv(oldLevelId),
                    EscapeCsv(oldChains),
                    EscapeCsv(candidate.LevelId),
                    candidate.Chains.ToString(Inv),
                    candidate.Width.ToString(Inv),
                    candidate.Height.ToString(Inv),
                    EscapeCsv(candidate.AssetPath)));
            }

            EditorUtility.SetDirty(campaignPack);
            WriteCsvTable(ToAbsolutePath(SelectionPath), selection);
            WriteCsvTable(ToAbsolutePath(RhythmPlanPath), rhythm);
            File.WriteAllLines(ToAbsolutePath(FrontHoleReplacementReportPath), report, new UTF8Encoding(false));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        static void ApplyNewbieHoleRowTag(Dictionary<string, string> row, HoleCandidate candidate)
        {
            Set(row, "qualityFlags", "procedural_hole_newbie");
            Set(row, "portableQuality", "procedural_hole_newbie");
            Set(row, "families", $"hole_procedural:newbie:{candidate.Profile.Id}");
            Set(row, "selectionReason", $"procedural-hole-front-newbie|standard_8x9_center|profile={candidate.Profile.Id}");
        }

        static List<Front20PatchCandidate> BuildFront20FeedbackCandidates()
        {
            var result = new List<Front20PatchCandidate>
            {
                BuildMaskGeneratedFront20PatchCandidate(
                    2,
                    "hole_rescue",
                    "front20_maskgen_hole_rescue_02",
                    18,
                    21,
                    951002,
                    22,
                    18,
                    28,
                    0.78f,
                    true,
                    "front20_mask_generated_hole_easy"),
                BuildMaskGeneratedFront20PatchCandidate(
                    3,
                    "normal",
                    "front20_maskgen_open_03",
                    16,
                    22,
                    951003,
                    24,
                    16,
                    34,
                    0.54f,
                    false,
                    "front20_mask_generated_open"),
                BuildMaskGeneratedFront20PatchCandidate(
                    11,
                    "sweep",
                    "front20_maskgen_recovery_11",
                    17,
                    24,
                    951011,
                    30,
                    20,
                    40,
                    0.60f,
                    false,
                    "front20_mask_generated_recovery"),
                BuildMaskGeneratedFront20PatchCandidate(
                    17,
                    "hole_rescue",
                    "front20_maskgen_hole_rescue_17",
                    20,
                    24,
                    951017,
                    28,
                    24,
                    34,
                    0.68f,
                    true,
                    "front20_mask_generated_hole_mid")
            };

            return result;
        }

        static Front20PatchCandidate BuildMaskGeneratedFront20PatchCandidate(
            int order,
            string type,
            string id,
            int width,
            int height,
            int seedBase,
            int targetChains,
            int minChains,
            int maxChains,
            float minPlayableFill,
            bool hasCenterBlocker,
            string quality)
        {
            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            Front20PatchCandidate best = null;
            float bestScore = float.MinValue;
            string bestDetails = "";

            for (int attempt = 0; attempt < 420; attempt++)
            {
                int seed = seedBase + attempt * 9973;
                var rng = new System.Random(seed);
                var allowed = BuildFront20MaskCells(width, height, hasCenterBlocker, rng, out var blocks, out string maskDetails);
                if (!TryBuildMaskGeneratedAuthored(
                        width,
                        height,
                        seed,
                        allowed,
                        blocks,
                        targetChains,
                        minChains,
                        maxChains,
                        minPlayableFill,
                        out AuthoredLevelData authored,
                        out string genDetails))
                {
                    bestDetails = $"generateReject; {maskDetails}; {genDetails}";
                    continue;
                }

                if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out string buildError))
                {
                    bestDetails = $"buildError={buildError}; {maskDetails}; {genDetails}";
                    continue;
                }

                if (!GreedyValidator.TryClearAllByGreedy(CloneBoard(board), rules, 1600, out _))
                {
                    bestDetails = $"greedyFail; {maskDetails}; {genDetails}";
                    continue;
                }

                var metrics = Measure(board, rules);
                float playableFill = metrics.ArrowTiles / Mathf.Max(1f, width * height - metrics.BlockTiles);
                int longestRun = LongestAuthoredStraightRun(authored);
                int shortChains = CountShortAuthoredChains(authored, 4);
                if (metrics.Chains < minChains || metrics.Chains > maxChains)
                    continue;
                if (playableFill < minPlayableFill)
                    continue;
                if (longestRun > Mathf.Max(13, width / 2 + 5) && order != 11)
                    continue;
                if (shortChains > Mathf.Max(3, metrics.Chains / 3))
                    continue;

                float score =
                    playableFill * 240f +
                    metrics.OpeningMoves * 5f +
                    metrics.AvgClearPerOpening * 10f -
                    Mathf.Abs(metrics.Chains - targetChains) * 6f -
                    longestRun * 5f -
                    shortChains * 18f;

                if (score <= bestScore)
                    continue;

                var profile = Profile(400 + order, id, width, height, minChains, maxChains, targetChains, 1f, 3, 16, minPlayableFill, 1, 1f, 1600, 0);
                string assetId = $"{id}_c{metrics.Chains}";
                var candidate = new HoleCandidate
                {
                    Rank = order,
                    Profile = profile,
                    LevelId = assetId,
                    AssetName = assetId,
                    AssetPath = $"{Front20OutputFolder}/{assetId}.asset",
                    Authored = authored,
                    Seed = seed,
                    Width = width,
                    Height = height,
                    Chains = metrics.Chains,
                    ArrowTiles = metrics.ArrowTiles,
                    BlockTiles = metrics.BlockTiles,
                    BoardFill = metrics.ArrowTiles / Mathf.Max(1f, width * height),
                    PlayableFill = playableFill,
                    OpeningMoves = metrics.OpeningMoves,
                    AvgClearPerOpening = metrics.AvgClearPerOpening,
                    MaxChain = metrics.MaxChain,
                    Score = score,
                    BlockerId = hasCenterBlocker ? "front20_center_rescue_blocker" : "",
                    Details = $"{quality}; seed={seed}; longestRun={longestRun}; shortChains={shortChains}; {maskDetails}; {genDetails}"
                };

                bestScore = score;
                bestDetails = candidate.Details;
                best = new Front20PatchCandidate
                {
                    Order = order,
                    Type = type,
                    Bucket = "front20",
                    Quality = quality,
                    Candidate = candidate
                };
            }

            if (best == null)
                throw new InvalidOperationException($"No mask-generated front20 candidate accepted for {id}. Last={bestDetails}");

            SaveCandidateAsset(best.Candidate);
            return best;
        }

        static bool[] BuildFront20MaskCells(int width, int height, bool hasCenterBlocker, System.Random rng, out List<int> blocks, out string details)
        {
            var cells = new bool[width * height];
            blocks = new List<int>();

            int marginX = 0;
            int marginBottom = 0;
            int marginTop = 0;
            int radius = hasCenterBlocker ? 2 : 3;
            for (int y = marginBottom; y < height - marginTop; y++)
            for (int x = marginX; x < width - marginX; x++)
            {
                bool corner =
                    (x < marginX + radius && y < marginBottom + radius && (marginX + radius - x) + (marginBottom + radius - y) > radius + 1) ||
                    (x >= width - marginX - radius && y < marginBottom + radius && (x - (width - marginX - radius - 1)) + (marginBottom + radius - y) > radius + 1) ||
                    (x < marginX + radius && y >= height - marginTop - radius && (marginX + radius - x) + (y - (height - marginTop - radius - 1)) > radius + 1) ||
                    (x >= width - marginX - radius && y >= height - marginTop - radius && (x - (width - marginX - radius - 1)) + (y - (height - marginTop - radius - 1)) > radius + 1);
                if (!corner)
                    cells[x + y * width] = true;
            }

            if (hasCenterBlocker)
            {
                int bw = Mathf.Clamp(Mathf.RoundToInt(width * 0.36f), 6, 8);
                int bh = Mathf.Clamp(Mathf.RoundToInt(height * 0.36f), 7, 9);
                int bx = (width - bw) / 2;
                int by = (height - bh) / 2;
                for (int y = by; y < by + bh; y++)
                for (int x = bx; x < bx + bw; x++)
                {
                    int idx = x + y * width;
                    cells[idx] = false;
                    blocks.Add(idx);
                }

                details = $"mask=rounded-rect-with-center-blocker:{bw}x{bh}@{bx},{by}";
            }
            else
            {
                int bites = 0;
                for (int i = 0; i < bites; i++)
                {
                    int bx = rng.Next(2, Mathf.Max(3, width - 3));
                    int by = rng.Next(2, Mathf.Max(3, height - 3));
                    int rx = rng.Next(1, 3);
                    int ry = rng.Next(1, 3);
                    if (rng.NextDouble() < 0.45)
                        continue;
                    for (int y = Mathf.Max(1, by - ry); y <= Mathf.Min(height - 2, by + ry); y++)
                    for (int x = Mathf.Max(1, bx - rx); x <= Mathf.Min(width - 2, bx + rx); x++)
                    {
                        if (Math.Abs(x - bx) + Math.Abs(y - by) <= rx + ry - 1)
                            cells[x + y * width] = false;
                    }
                }

                details = "mask=rounded-rect-soft-random";
            }

            return cells;
        }

        static bool TryBuildMaskGeneratedAuthored(
            int width,
            int height,
            int seed,
            bool[] allowed,
            IReadOnlyList<int> blocks,
            int targetChains,
            int minChains,
            int maxChains,
            float minPlayableFill,
            out AuthoredLevelData authored,
            out string details)
        {
            authored = new AuthoredLevelData
            {
                width = width,
                height = height,
                blockIndices = blocks != null ? new List<int>(blocks) : new List<int>(),
                arrows = new List<AuthoredArrowData>()
            };

            details = "";
            if (allowed == null || allowed.Length != width * height)
            {
                details = "invalid mask";
                return false;
            }

            var rng = new System.Random(seed);
            var occupied = new bool[width * height];
            for (int i = 0; i < allowed.Length; i++)
                occupied[i] = !allowed[i];
            if (blocks != null)
            {
                for (int i = 0; i < blocks.Count; i++)
                {
                    int idx = blocks[i];
                    if (idx >= 0 && idx < occupied.Length)
                        occupied[idx] = true;
                }
            }

            int playable = CountAllowedCells(allowed);
            int targetTiles = Mathf.CeilToInt(playable * Mathf.Clamp01(minPlayableFill + 0.03f));
            int minLen = Mathf.Clamp(Mathf.RoundToInt(playable / (float)Mathf.Max(1, targetChains)) - 3, 3, 8);
            int maxLen = Mathf.Clamp(Mathf.RoundToInt(playable / (float)Mathf.Max(1, targetChains)) + 6, 7, 17);
            int guard = Mathf.Max(600, targetChains * 80);

            while (authored.arrows.Count < maxChains && CountArrowTiles(authored) < targetTiles && guard-- > 0)
            {
                if (!TryBuildMaskRandomChain(width, height, allowed, occupied, rng, minLen, maxLen, out var chain))
                    continue;

                AddAuthoredChain(authored, occupied, chain);
            }

            for (int i = 0; i < authored.arrows.Count; i++)
                authored.arrows[i].colorIndex = i;

            int arrows = CountArrowTiles(authored);
            float playableFill = arrows / Mathf.Max(1f, playable);
            details = $"maskGenerated chains={authored.arrows.Count}; tiles={arrows}/{playable}; fill={playableFill:0.000}; len={minLen}-{maxLen}";
            return authored.arrows.Count >= minChains &&
                   authored.arrows.Count <= maxChains &&
                   playableFill >= minPlayableFill;
        }

        static bool TryBuildMaskRandomChain(
            int width,
            int height,
            bool[] allowed,
            bool[] occupied,
            System.Random rng,
            int minLen,
            int maxLen,
            out List<int> chain)
        {
            chain = null;
            var starts = CollectMaskBoundaryStarts(width, height, allowed, occupied);
            if (starts.Count == 0)
                return false;

            var start = starts[rng.Next(starts.Count)];
            int targetLen = rng.Next(minLen, maxLen + 1);
            var path = new List<int>(targetLen) { start.Head, start.Second };
            var used = new HashSet<int>(path);
            Vector2Int previous = new Vector2Int(start.Head % width, start.Head / width);
            Vector2Int current = new Vector2Int(start.Second % width, start.Second / width);
            Vector2Int previousDelta = current - previous;
            int straightRun = 1;

            while (path.Count < targetLen)
            {
                var candidates = new List<StepCandidate>(4);
                for (int i = 0; i < Directions.Length; i++)
                {
                    Vector2Int dir = Directions[i];
                    Vector2Int next = current + dir;
                    if (next.x < 0 || next.x >= width || next.y < 0 || next.y >= height)
                        continue;

                    int nextIdx = next.x + next.y * width;
                    if (!allowed[nextIdx] || occupied[nextIdx] || used.Contains(nextIdx))
                        continue;

                    int freeNeighbors = CountFreeMaskNeighbors(width, height, allowed, occupied, used, nextIdx);
                    float score = (float)rng.NextDouble();
                    if (dir == previousDelta)
                        score += straightRun >= 3 ? -0.8f : 0.18f;
                    else
                        score += 0.34f;
                    score += freeNeighbors * 0.08f;
                    score += DistanceToCenterScore(width, height, next) * 0.12f;
                    candidates.Add(new StepCandidate { Direction = dir, Index = nextIdx, Score = score });
                }

                if (candidates.Count == 0)
                    break;

                candidates.Sort((a, b) => b.Score.CompareTo(a.Score));
                var picked = candidates[Mathf.Min(candidates.Count - 1, rng.Next(0, Mathf.Min(2, candidates.Count)))];
                if (picked.Direction == previousDelta)
                    straightRun++;
                else
                    straightRun = 1;

                previousDelta = picked.Direction;
                current = new Vector2Int(picked.Index % width, picked.Index / width);
                path.Add(picked.Index);
                used.Add(picked.Index);
            }

            if (path.Count < minLen)
                return false;

            chain = path;
            return true;
        }

        static List<BoundaryStart> CollectMaskBoundaryStarts(int width, int height, bool[] allowed, bool[] occupied)
        {
            var starts = new List<BoundaryStart>();
            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                int head = x + y * width;
                if (!allowed[head] || occupied[head])
                    continue;

                for (int d = 0; d < Directions.Length; d++)
                {
                    Vector2Int outward = Directions[d];
                    Vector2Int secondPos = new Vector2Int(x, y) - outward;
                    if (secondPos.x < 0 || secondPos.x >= width || secondPos.y < 0 || secondPos.y >= height)
                        continue;

                    int second = secondPos.x + secondPos.y * width;
                    if (!allowed[second] || occupied[second])
                        continue;

                    if (!HasClearMaskRayToOutside(width, height, allowed, occupied, x, y, outward))
                        continue;

                    starts.Add(new BoundaryStart { Head = head, Second = second, Outward = outward });
                }
            }

            return starts;
        }

        static bool HasClearMaskRayToOutside(int width, int height, bool[] allowed, bool[] occupied, int x, int y, Vector2Int dir)
        {
            int cx = x + dir.x;
            int cy = y + dir.y;
            bool sawOutsideMask = false;
            while (cx >= 0 && cx < width && cy >= 0 && cy < height)
            {
                int idx = cx + cy * width;
                if (occupied[idx])
                    return false;
                if (allowed[idx])
                    return false;

                sawOutsideMask = true;
                cx += dir.x;
                cy += dir.y;
            }

            return sawOutsideMask || cx < 0 || cx >= width || cy < 0 || cy >= height;
        }

        static int CountFreeMaskNeighbors(int width, int height, bool[] allowed, bool[] occupied, HashSet<int> used, int idx)
        {
            int x = idx % width;
            int y = idx / width;
            int count = 0;
            for (int i = 0; i < Directions.Length; i++)
            {
                int nx = x + Directions[i].x;
                int ny = y + Directions[i].y;
                if (nx < 0 || nx >= width || ny < 0 || ny >= height)
                    continue;

                int n = nx + ny * width;
                if (allowed[n] && !occupied[n] && !used.Contains(n))
                    count++;
            }

            return count;
        }

        static float DistanceToCenterScore(int width, int height, Vector2Int pos)
        {
            float cx = (width - 1) * 0.5f;
            float cy = (height - 1) * 0.5f;
            float dx = Mathf.Abs(pos.x - cx) / Mathf.Max(1f, cx);
            float dy = Mathf.Abs(pos.y - cy) / Mathf.Max(1f, cy);
            return 1f - Mathf.Clamp01((dx + dy) * 0.5f);
        }

        static int CountAllowedCells(bool[] allowed)
        {
            int count = 0;
            if (allowed == null)
                return count;
            for (int i = 0; i < allowed.Length; i++)
                if (allowed[i])
                    count++;
            return count;
        }

        static int CountShortAuthoredChains(AuthoredLevelData authored, int threshold)
        {
            int count = 0;
            if (authored?.arrows == null)
                return 0;
            for (int i = 0; i < authored.arrows.Count; i++)
                if ((authored.arrows[i]?.indices?.Count ?? 0) <= threshold)
                    count++;
            return count;
        }

        static int LongestAuthoredStraightRun(AuthoredLevelData authored)
        {
            int best = 0;
            if (authored?.arrows == null)
                return best;

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var indices = authored.arrows[i]?.indices;
                if (indices == null || indices.Count < 2)
                    continue;

                int run = 1;
                Vector2Int previous = IndexToPoint(indices[1], authored.width) - IndexToPoint(indices[0], authored.width);
                for (int j = 2; j < indices.Count; j++)
                {
                    Vector2Int delta = IndexToPoint(indices[j], authored.width) - IndexToPoint(indices[j - 1], authored.width);
                    if (delta == previous)
                        run++;
                    else
                    {
                        best = Mathf.Max(best, run + 1);
                        run = 1;
                        previous = delta;
                    }
                }

                best = Mathf.Max(best, run + 1);
            }

            return best;
        }

        static Vector2Int IndexToPoint(int index, int width)
        {
            return new Vector2Int(index % width, index / width);
        }

        static readonly Vector2Int[] Directions =
        {
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, -1)
        };

        static AuthoredLevelData BuildFront20HoleAuthored(int width, int height, bool front17, out string details)
        {
            var profile = Profile(200 + width + height, "front20_hole_layout", width, height, 1, 80, 24, 1f, 3, 24, 0.95f, 4, 4f, 900, 0);
            BuildCanSpawn(profile, 0, out List<int> blocks, out string blockDetails);
            var authored = new AuthoredLevelData
            {
                width = width,
                height = height,
                blockIndices = new List<int>(blocks),
                arrows = new List<AuthoredArrowData>()
            };

            var occupied = new bool[width * height];
            for (int i = 0; i < blocks.Count; i++)
                occupied[blocks[i]] = true;

            RectInt block = GetStandardBlockerRect(width, height);
            if (!front17)
            {
                AddChunkedSide(authored, occupied, 0, block.xMin - 1, new[] { 4, 3, 5, 2, 4 }, ExitSide.Left);
                AddChunkedSide(authored, occupied, block.xMax, width - 1, new[] { 3, 4, 4, 3, 4 }, ExitSide.Right);
                AddChunkedBand(authored, occupied, block.xMin, block.xMax - 1, 0, block.yMin - 1, new[] { 3, 2, 3 }, ExitSide.Bottom);
                AddChunkedBand(authored, occupied, block.xMin, block.xMax - 1, block.yMax, height - 1, new[] { 2, 3, 3 }, ExitSide.Top);
            }
            else
            {
                AddChunkedSide(authored, occupied, 0, block.xMin - 1, new[] { 2, 2, 2, 3, 2, 2, 2, 2, 2 }, ExitSide.Left);
                AddChunkedSide(authored, occupied, block.xMax, width - 1, new[] { 3, 2, 2, 2, 3, 2, 2, 3 }, ExitSide.Right);
                AddChunkedBand(authored, occupied, block.xMin, block.xMax - 1, 0, block.yMin - 1, new[] { 2, 2, 2, 2 }, ExitSide.Bottom);
                AddChunkedBand(authored, occupied, block.xMin, block.xMax - 1, block.yMax, height - 1, new[] { 2, 2, 2, 2 }, ExitSide.Top);
            }

            for (int i = 0; i < authored.arrows.Count; i++)
                authored.arrows[i].colorIndex = i;

            details = $"{blockDetails}; softRingChains={authored.arrows.Count}; front17={front17}";
            return authored;
        }

        static AuthoredLevelData BuildVerticalWaterfallAuthored(int width, int height, int[] bandWidths, int[] segmentHeights, out string details)
        {
            var authored = new AuthoredLevelData
            {
                width = width,
                height = height,
                blockIndices = new List<int>(),
                arrows = new List<AuthoredArrowData>()
            };
            var occupied = new bool[width * height];
            int x = 0;
            for (int band = 0; band < bandWidths.Length; band++)
            {
                int xMin = x;
                int xMax = Mathf.Min(width - 1, x + bandWidths[band] - 1);
                x = xMax + 1;
                bool fromTop = (band & 1) == 0;
                int cursor = fromTop ? height - 1 : 0;
                for (int i = 0; i < segmentHeights.Length; i++)
                {
                    int segment = segmentHeights[i];
                    if (fromTop)
                    {
                        int yMax = cursor;
                        int yMin = Mathf.Max(0, cursor - segment + 1);
                        AddSerpentineRectChain(authored, occupied, xMin, xMax, yMin, yMax, ExitSide.Top);
                        cursor = yMin - 1;
                    }
                    else
                    {
                        int yMin = cursor;
                        int yMax = Mathf.Min(height - 1, cursor + segment - 1);
                        AddSerpentineRectChain(authored, occupied, xMin, xMax, yMin, yMax, ExitSide.Bottom);
                        cursor = yMax + 1;
                    }
                }
            }

            for (int i = 0; i < authored.arrows.Count; i++)
                authored.arrows[i].colorIndex = i;

            details = $"verticalWaterfallChains={authored.arrows.Count}; bands={bandWidths.Length}; segments={segmentHeights.Length}";
            return authored;
        }

        static AuthoredLevelData BuildHorizontalRecoverySweepAuthored(int width, int height, int[] rowGroups, out string details)
        {
            var authored = new AuthoredLevelData
            {
                width = width,
                height = height,
                blockIndices = new List<int>(),
                arrows = new List<AuthoredArrowData>()
            };
            var occupied = new bool[width * height];
            int y = 0;
            for (int i = 0; i < rowGroups.Length && y < height; i++)
            {
                int yMin = y;
                int yMax = Mathf.Min(height - 1, y + rowGroups[i] - 1);
                AddSerpentineRectChain(authored, occupied, 0, width - 1, yMin, yMax, (i & 1) == 0 ? ExitSide.Left : ExitSide.Right);
                y = yMax + 1;
            }

            if (y < height)
                AddSerpentineRectChain(authored, occupied, 0, width - 1, y, height - 1, (authored.arrows.Count & 1) == 0 ? ExitSide.Left : ExitSide.Right);

            for (int i = 0; i < authored.arrows.Count; i++)
                authored.arrows[i].colorIndex = i;

            details = $"horizontalRecoveryChains={authored.arrows.Count}; rowGroups={rowGroups.Length}";
            return authored;
        }

        static void AddChunkedSide(AuthoredLevelData authored, bool[] occupied, int xMin, int xMax, int[] heights, ExitSide side)
        {
            int y = 0;
            for (int i = 0; i < heights.Length && y < authored.height; i++)
            {
                int yMin = y;
                int yMax = Mathf.Min(authored.height - 1, y + Mathf.Max(1, heights[i]) - 1);
                AddSerpentineRectChain(authored, occupied, xMin, xMax, yMin, yMax, side);
                y = yMax + 1;
            }

            if (y < authored.height)
                AddSerpentineRectChain(authored, occupied, xMin, xMax, y, authored.height - 1, side);
        }

        static void AddChunkedBand(AuthoredLevelData authored, bool[] occupied, int xMin, int xMax, int yMin, int yMax, int[] widths, ExitSide side)
        {
            int x = xMin;
            for (int i = 0; i < widths.Length && x <= xMax; i++)
            {
                int chunkXMin = x;
                int chunkXMax = Mathf.Min(xMax, x + Mathf.Max(1, widths[i]) - 1);
                AddSerpentineRectChain(authored, occupied, chunkXMin, chunkXMax, yMin, yMax, side);
                x = chunkXMax + 1;
            }

            if (x <= xMax)
                AddSerpentineRectChain(authored, occupied, x, xMax, yMin, yMax, side);
        }

        static Front20PatchCandidate BuildFront20PatchCandidate(
            int order,
            string type,
            string id,
            int width,
            int height,
            int seed,
            AuthoredLevelData authored,
            string quality,
            string details)
        {
            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out string buildError))
                throw new InvalidOperationException($"Front20 authored build failed {id}: {buildError}");

            if (!GreedyValidator.TryClearAllByGreedy(CloneBoard(board), rules, 1200, out _))
                throw new InvalidOperationException($"Front20 greedy failed {id}: {details}");

            var metrics = Measure(board, rules);
            var profile = Profile(300 + order, id, width, height, 1, 120, Mathf.Max(1, metrics.Chains), 1f, 2, Mathf.Max(24, metrics.MaxChain), 0.8f, 1, 1f, 1200, 0);
            string assetPath = $"{(type.StartsWith("hole", StringComparison.OrdinalIgnoreCase) ? OutputFolder : Front20OutputFolder)}/{id}.asset";
            var candidate = new HoleCandidate
            {
                Rank = order,
                Profile = profile,
                LevelId = id,
                AssetName = id,
                AssetPath = assetPath,
                Authored = authored,
                Seed = seed,
                Width = width,
                Height = height,
                Chains = metrics.Chains,
                ArrowTiles = metrics.ArrowTiles,
                BlockTiles = metrics.BlockTiles,
                BoardFill = metrics.ArrowTiles / Mathf.Max(1f, width * height),
                PlayableFill = metrics.ArrowTiles / Mathf.Max(1f, width * height - metrics.BlockTiles),
                OpeningMoves = metrics.OpeningMoves,
                AvgClearPerOpening = metrics.AvgClearPerOpening,
                MaxChain = metrics.MaxChain,
                Score = metrics.AvgClearPerOpening * 18f + metrics.OpeningMoves * 5f + metrics.Chains,
                BlockerId = metrics.BlockTiles > 0 ? "standard_8x9_center" : "",
                Details = details
            };

            SaveCandidateAsset(candidate);
            return new Front20PatchCandidate
            {
                Order = order,
                Type = type,
                Bucket = "front20",
                Quality = quality,
                Candidate = candidate
            };
        }

        static void ApplyFront20FeedbackPatches(IReadOnlyList<Front20PatchCandidate> patches)
        {
            var campaignPack = AssetDatabase.LoadAssetAtPath<LevelPack>(CampaignPackPath);
            if (campaignPack == null || campaignPack.levels == null || campaignPack.levels.Length < 20)
                throw new InvalidOperationException($"Missing or short campaign pack: {CampaignPackPath}");

            var selection = ReadCsvTable(ToAbsolutePath(SelectionPath));
            var rhythm = ReadCsvTable(ToAbsolutePath(RhythmPlanPath));
            var selectionByOrder = selection.Rows
                .Where(r => TryInt(Get(r, "order"), out _))
                .ToDictionary(r => int.Parse(Get(r, "order"), Inv), r => r);
            var rhythmByOrder = rhythm.Rows
                .Where(r => TryInt(Get(r, "order"), out _))
                .ToDictionary(r => int.Parse(Get(r, "order"), Inv), r => r);

            var report = new List<string>
            {
                "order,oldType,oldLevelId,oldChains,newType,newLevelId,newChains,newOpeners,newAvgClear,newPath,quality"
            };

            foreach (var patch in patches.OrderBy(p => p.Order))
            {
                int order = patch.Order;
                var candidate = patch.Candidate;
                string oldType = "";
                string oldLevelId = campaignPack.levels[order - 1] != null ? campaignPack.levels[order - 1].levelId : "";
                string oldChains = "";

                campaignPack.levels[order - 1] = candidate.Level;
                if (selectionByOrder.TryGetValue(order, out var row))
                {
                    oldType = Get(row, "type");
                    oldChains = Get(row, "chains");
                    ApplyFront20FeedbackRow(row, patch);
                }

                if (rhythmByOrder.TryGetValue(order, out var rhythmRow))
                {
                    Set(rhythmRow, "bucket", patch.Bucket);
                    Set(rhythmRow, "type", patch.Type);
                    Set(rhythmRow, "chains", candidate.Chains.ToString(Inv));
                    Set(rhythmRow, "levelId", candidate.LevelId);
                    Set(rhythmRow, "note", AppendNote(Get(rhythmRow, "note"), patch.Quality));
                }

                report.Add(string.Join(",",
                    order.ToString(Inv),
                    EscapeCsv(oldType),
                    EscapeCsv(oldLevelId),
                    EscapeCsv(oldChains),
                    EscapeCsv(patch.Type),
                    EscapeCsv(candidate.LevelId),
                    candidate.Chains.ToString(Inv),
                    candidate.OpeningMoves.ToString(Inv),
                    F(candidate.AvgClearPerOpening),
                    EscapeCsv(candidate.AssetPath),
                    EscapeCsv(patch.Quality)));
            }

            EditorUtility.SetDirty(campaignPack);
            WriteCsvTable(ToAbsolutePath(SelectionPath), selection);
            WriteCsvTable(ToAbsolutePath(RhythmPlanPath), rhythm);
            File.WriteAllLines(ToAbsolutePath(Front20FeedbackReportPath), report, new UTF8Encoding(false));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        static void ApplyFront20FeedbackRow(Dictionary<string, string> row, Front20PatchCandidate patch)
        {
            var candidate = patch.Candidate;
            Set(row, "bucket", patch.Bucket);
            Set(row, "type", patch.Type);
            Set(row, "score", Mathf.RoundToInt(candidate.Score).ToString(Inv));
            Set(row, "levelId", candidate.LevelId);
            Set(row, "path", candidate.AssetPath);
            Set(row, "assetPath", candidate.AssetPath);
            Set(row, "packs", "front20_feedback");
            Set(row, "width", candidate.Width.ToString(Inv));
            Set(row, "height", candidate.Height.ToString(Inv));
            Set(row, "chains", candidate.Chains.ToString(Inv));
            Set(row, "tiles", candidate.ArrowTiles.ToString(Inv));
            Set(row, "arrowTiles", candidate.ArrowTiles.ToString(Inv));
            Set(row, "blockTiles", candidate.BlockTiles.ToString(Inv));
            Set(row, "coverage", F(candidate.BoardFill));
            Set(row, "boardFill", F(candidate.BoardFill));
            Set(row, "playableFill", F(candidate.PlayableFill));
            Set(row, "openers", candidate.OpeningMoves.ToString(Inv));
            Set(row, "openingMoves", candidate.OpeningMoves.ToString(Inv));
            Set(row, "avgChoices", F(candidate.OpeningMoves));
            Set(row, "families", patch.Quality);
            Set(row, "qualityFlags", patch.Quality);
            Set(row, "portableSolved", "True");
            Set(row, "portableQuality", patch.Quality);
            Set(row, "portableScore", Mathf.RoundToInt(candidate.Score).ToString(Inv));
            Set(row, "selectionReason", $"front20-feedback|{patch.Quality}");
        }

        static int ReplaceYellowRowsFromPools(bool skipFrontOrders)
        {
            string validationPath = ToAbsolutePath(SingleLevelValidationSummaryPath);
            if (!File.Exists(validationPath))
                return 0;

            var validation = ReadCsvTable(validationPath);
            var selection = ReadCsvTable(ToAbsolutePath(SelectionPath));
            var rhythm = ReadCsvTable(ToAbsolutePath(RhythmPlanPath));
            var campaignPack = AssetDatabase.LoadAssetAtPath<LevelPack>(CampaignPackPath);
            if (campaignPack == null || campaignPack.levels == null || campaignPack.levels.Length < 500)
                throw new InvalidOperationException($"Missing or short campaign pack: {CampaignPackPath}");

            var selectionByOrder = selection.Rows
                .Where(r => TryInt(Get(r, "order"), out _))
                .ToDictionary(r => int.Parse(Get(r, "order"), Inv), r => r);
            var rhythmByOrder = rhythm.Rows
                .Where(r => TryInt(Get(r, "order"), out _))
                .ToDictionary(r => int.Parse(Get(r, "order"), Inv), r => r);

            var usedLevelIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var usedPaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var row in selection.Rows)
            {
                AddIfNotEmpty(usedLevelIds, Get(row, "levelId"));
                AddIfNotEmpty(usedPaths, Get(row, "path"));
                AddIfNotEmpty(usedPaths, Get(row, "assetPath"));
            }

            var options = LoadYellowReplacementOptions(usedLevelIds, usedPaths);
            var report = new List<string>
            {
                "order,type,flags,oldLevelId,oldChains,newLevelId,newChains,newSource,newScore,newPath,decision"
            };

            int replaced = 0;
            foreach (var yellow in validation.Rows
                         .Where(r => string.Equals(Get(r, "status"), "Yellow", StringComparison.OrdinalIgnoreCase))
                         .OrderBy(r => TryInt(Get(r, "order"), out int order) ? order : int.MaxValue))
            {
                if (!TryInt(Get(yellow, "order"), out int order))
                    continue;
                if (skipFrontOrders && order <= 20)
                {
                    report.Add(SkippedYellowLine(order, Get(yellow, "type"), Get(yellow, "flags"), Get(yellow, "levelId"), Get(yellow, "chains"), "front-fixed"));
                    continue;
                }

                if (!selectionByOrder.TryGetValue(order, out var selectedRow))
                {
                    report.Add(SkippedYellowLine(order, Get(yellow, "type"), Get(yellow, "flags"), Get(yellow, "levelId"), Get(yellow, "chains"), "missing-selection-row"));
                    continue;
                }

                string targetType = Get(selectedRow, "type");
                if (string.Equals(targetType, "tutorial", StringComparison.OrdinalIgnoreCase))
                {
                    report.Add(SkippedYellowLine(order, targetType, Get(yellow, "flags"), Get(selectedRow, "levelId"), Get(selectedRow, "chains"), "manual-tutorial"));
                    continue;
                }

                var candidate = PickYellowReplacementCandidate(order, targetType, yellow, options, usedLevelIds, usedPaths, out string decision);
                if (candidate == null)
                {
                    report.Add(SkippedYellowLine(order, targetType, Get(yellow, "flags"), Get(selectedRow, "levelId"), Get(selectedRow, "chains"), decision));
                    continue;
                }

                string oldLevelId = Get(selectedRow, "levelId");
                string oldChains = Get(selectedRow, "chains");
                campaignPack.levels[order - 1] = candidate.Level;
                string finalType = GetReplacementRowType(targetType, candidate);
                ApplyYellowCandidateRow(selectedRow, candidate, finalType, Get(yellow, "flags"), decision);
                if (rhythmByOrder.TryGetValue(order, out var rhythmRow))
                    ApplyYellowRhythmRow(rhythmRow, candidate, finalType, Get(yellow, "flags"));

                AddIfNotEmpty(usedLevelIds, candidate.LevelId);
                AddIfNotEmpty(usedPaths, candidate.AssetPath);
                replaced++;

                report.Add(string.Join(",",
                    order.ToString(Inv),
                    EscapeCsv(targetType),
                    EscapeCsv(Get(yellow, "flags")),
                    EscapeCsv(oldLevelId),
                    EscapeCsv(oldChains),
                    EscapeCsv(candidate.LevelId),
                    candidate.Chains.ToString(Inv),
                    EscapeCsv(candidate.SourceKind),
                    F(candidate.Score),
                    EscapeCsv(candidate.AssetPath),
                    EscapeCsv(decision)));
            }

            EditorUtility.SetDirty(campaignPack);
            WriteCsvTable(ToAbsolutePath(SelectionPath), selection);
            WriteCsvTable(ToAbsolutePath(RhythmPlanPath), rhythm);
            File.WriteAllLines(ToAbsolutePath(YellowReplacementReportPath), report, new UTF8Encoding(false));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return replaced;
        }

        static List<ReplacementOption> LoadYellowReplacementOptions(HashSet<string> usedLevelIds, HashSet<string> usedPaths)
        {
            var options = new List<ReplacementOption>();
            string candidateScorePath = ToAbsolutePath(CandidateScorePath);
            if (File.Exists(candidateScorePath))
            {
                var table = ReadCsvTable(candidateScorePath);
                foreach (var row in table.Rows)
                {
                    if (!string.Equals(Get(row, "selected"), "0", StringComparison.OrdinalIgnoreCase))
                        continue;
                    if (!string.Equals(Get(row, "portableSolved"), "True", StringComparison.OrdinalIgnoreCase))
                        continue;

                    string path = Get(row, "path");
                    string levelId = Get(row, "levelId");
                    if (string.IsNullOrWhiteSpace(path) || usedPaths.Contains(path) || usedLevelIds.Contains(levelId))
                        continue;

                    var level = AssetDatabase.LoadAssetAtPath<LevelDefinition>(path);
                    if (level == null)
                        continue;

                    options.Add(new ReplacementOption
                    {
                        SourceKind = "normal-candidate",
                        Bucket = Get(row, "bucket"),
                        Type = Get(row, "type"),
                        Score = ParseFloat(Get(row, "score")),
                        LevelId = string.IsNullOrWhiteSpace(levelId) ? level.levelId : levelId,
                        AssetPath = path,
                        PackId = Get(row, "packs"),
                        Width = ParseInt(Get(row, "width")),
                        Height = ParseInt(Get(row, "height")),
                        Chains = ParseInt(Get(row, "chains")),
                        ArrowTiles = ParseInt(Get(row, "tiles")),
                        Coverage = ParseFloat(Get(row, "coverage")),
                        Openers = ParseInt(Get(row, "openers")),
                        AvgChoices = ParseFloat(Get(row, "avgChoices")),
                        UnlockBursts = Get(row, "unlockBursts"),
                        DependencyBlocks = Get(row, "dependencyBlocks"),
                        Families = Get(row, "families"),
                        QualityFlags = Get(row, "qualityFlags"),
                        PortableScore = ParseFloat(Get(row, "portableScore")),
                        PortableQuality = Get(row, "portableQuality"),
                        Level = level
                    });
                }
            }

            string manifestPath = ToAbsolutePath(DifficultyManifestPath);
            if (File.Exists(manifestPath))
            {
                var table = ReadCsvTable(manifestPath);
                foreach (var row in table.Rows)
                {
                    if (!string.Equals(Get(row, "sourceKind"), "shape", StringComparison.OrdinalIgnoreCase))
                        continue;
                    if (!string.Equals(Get(row, "greedySolved"), "True", StringComparison.OrdinalIgnoreCase))
                        continue;
                    if (!string.IsNullOrWhiteSpace(Get(row, "buildError")))
                        continue;

                    string risk = Get(row, "riskTags");
                    if (!string.IsNullOrWhiteSpace(risk) && risk.IndexOf("NoRisk", StringComparison.OrdinalIgnoreCase) < 0)
                        continue;

                    string path = Get(row, "assetPath");
                    string levelId = Get(row, "levelId");
                    if (string.IsNullOrWhiteSpace(path) || usedPaths.Contains(path) || usedLevelIds.Contains(levelId))
                        continue;

                    var level = AssetDatabase.LoadAssetAtPath<LevelDefinition>(path);
                    if (level == null)
                        continue;

                    levelId = string.IsNullOrWhiteSpace(levelId) ? level.levelId : levelId;
                    options.Add(new ReplacementOption
                    {
                        SourceKind = "shape-manifest",
                        Bucket = "shape",
                        Type = MapShapeCampaignType(row),
                        Score = ParseFloat(Get(row, "difficultyScoreV1")),
                        LevelId = levelId,
                        AssetPath = path,
                        PackId = Get(row, "sourcePool"),
                        Width = ParseInt(Get(row, "width")),
                        Height = ParseInt(Get(row, "height")),
                        Chains = ParseInt(Get(row, "chains")),
                        ArrowTiles = ParseInt(Get(row, "arrowTiles")),
                        Coverage = ParseFloat(Get(row, "playableFill")),
                        Openers = ParseInt(Get(row, "openingMoves")),
                        AvgChoices = ParseFloat(Get(row, "avgAvailableMoves")),
                        AvgClearPerMove = ParseFloat(Get(row, "avgClearPerMove")),
                        OpeningGoodMoveRatio = ParseFloat(Get(row, "openingGoodMoveRatio")),
                        BottleneckStepRatio = ParseFloat(Get(row, "bottleneckStepRatio")),
                        HardBottleneckStepRatio = ParseFloat(Get(row, "hardBottleneckStepRatio")),
                        UnlockBursts = Get(row, "largestUnlockBurst"),
                        DependencyBlocks = Get(row, "bottleneckStepRatio"),
                        Families = $"shape:{Get(row, "theme")}:{Get(row, "shapeTag")}",
                        QualityFlags = string.IsNullOrWhiteSpace(risk) ? "shape_manifest_ok" : risk,
                        PortableScore = ParseFloat(Get(row, "difficultyScoreV1")),
                        PortableQuality = Get(row, "difficultyTagV1"),
                        Level = level
                    });
                }
            }

            return options;
        }

        static ReplacementOption PickYellowReplacementCandidate(
            int order,
            string targetType,
            Dictionary<string, string> yellow,
            IReadOnlyList<ReplacementOption> options,
            HashSet<string> usedLevelIds,
            HashSet<string> usedPaths,
            out string decision)
        {
            string flags = Get(yellow, "flags");
            int oldChains = ParseInt(Get(yellow, "chains"));
            bool shapeTarget = targetType.StartsWith("shape", StringComparison.OrdinalIgnoreCase);
            float targetChains = oldChains > 0 ? oldChains : Mathf.Lerp(55f, 150f, Mathf.Clamp01(order / 500f));
            if (flags.IndexOf("GrindyLowClear", StringComparison.OrdinalIgnoreCase) >= 0)
                targetChains *= shapeTarget ? 0.78f : 0.88f;
            if (flags.IndexOf("Bottleneck", StringComparison.OrdinalIgnoreCase) >= 0)
                targetChains *= shapeTarget ? 0.86f : 0.92f;
            if (flags.IndexOf("PeakTooFlat", StringComparison.OrdinalIgnoreCase) >= 0)
                targetChains *= shapeTarget ? 0.9f : 0.96f;
            if (flags.IndexOf("StartFakeWide", StringComparison.OrdinalIgnoreCase) >= 0)
                targetChains *= 0.96f;

            float softCap = GetOrderChainSoftCap(order);
            var scoped = options
                .Where(c => !usedLevelIds.Contains(c.LevelId) && !usedPaths.Contains(c.AssetPath))
                .Where(c => string.Equals(c.Type, targetType, StringComparison.OrdinalIgnoreCase))
                .ToList();

            string scope = "same-type";
            bool shapeWide = false;
            if (shapeTarget)
            {
                var wideShape = options
                    .Where(c => !usedLevelIds.Contains(c.LevelId) && !usedPaths.Contains(c.AssetPath))
                    .Where(c => c.Type.StartsWith("shape", StringComparison.OrdinalIgnoreCase))
                    .ToList();
                if (flags.IndexOf("GrindyLowClear", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    flags.IndexOf("Bottleneck", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    flags.IndexOf("PeakTooFlat", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    scoped.Count < 3)
                {
                    var normalQuality = options
                        .Where(c => !usedLevelIds.Contains(c.LevelId) && !usedPaths.Contains(c.AssetPath))
                        .Where(c => !c.Type.StartsWith("shape", StringComparison.OrdinalIgnoreCase) &&
                                    !c.Type.StartsWith("hole", StringComparison.OrdinalIgnoreCase) &&
                                    !string.Equals(c.Type, "tutorial", StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    scoped = wideShape.Concat(normalQuality).ToList();
                    scope = "shape-quality-wide";
                    shapeWide = true;
                }
            }

            if (scoped.Count == 0 && shapeTarget)
            {
                scoped = options
                    .Where(c => !usedLevelIds.Contains(c.LevelId) && !usedPaths.Contains(c.AssetPath))
                    .Where(c => c.Type.StartsWith("shape", StringComparison.OrdinalIgnoreCase))
                    .ToList();
                scope = "shape-fallback";
            }

            if (scoped.Count == 0 && !shapeTarget)
            {
                scoped = options
                    .Where(c => !usedLevelIds.Contains(c.LevelId) && !usedPaths.Contains(c.AssetPath))
                    .Where(c => !c.Type.StartsWith("shape", StringComparison.OrdinalIgnoreCase) && !c.Type.StartsWith("hole", StringComparison.OrdinalIgnoreCase))
                    .ToList();
                scope = "normal-fallback";
            }

            ReplacementOption best = null;
            float bestScore = float.MaxValue;
            foreach (var candidate in scoped)
            {
                float score =
                    Mathf.Abs(candidate.Chains - targetChains) * 2.4f +
                    Mathf.Max(0f, candidate.Chains - softCap) * 8f -
                    candidate.Score * 0.08f -
                    candidate.Openers * 1.1f -
                    candidate.AvgChoices * 1.4f;

                if (shapeWide && !string.Equals(candidate.Type, targetType, StringComparison.OrdinalIgnoreCase))
                    score += candidate.Type.StartsWith("shape", StringComparison.OrdinalIgnoreCase) ? 38f : 72f;
                if (shapeWide && !candidate.Type.StartsWith("shape", StringComparison.OrdinalIgnoreCase))
                    score -= 560f;

                if (flags.IndexOf("GrindyLowClear", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    score -= candidate.AvgClearPerMove * (shapeTarget ? 34f : 18f);
                    if (candidate.AvgClearPerMove > 0f && candidate.AvgClearPerMove < (shapeTarget ? 7.1f : 5.5f))
                        score += ((shapeTarget ? 7.1f : 5.5f) - candidate.AvgClearPerMove) * (shapeTarget ? 85f : 35f);
                }

                if (flags.IndexOf("Bottleneck", StringComparison.OrdinalIgnoreCase) >= 0)
                    score += candidate.BottleneckStepRatio * (shapeTarget ? 520f : 210f) + candidate.HardBottleneckStepRatio * (shapeTarget ? 760f : 420f);

                if (flags.IndexOf("StartFakeWide", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    score -= candidate.OpeningGoodMoveRatio * (shapeTarget ? 180f : 100f);
                    if (candidate.Openers > 20)
                        score += (candidate.Openers - 20) * 5f;
                }

                if (shapeTarget)
                {
                    if (candidate.BottleneckStepRatio > 0.32f)
                        score += (candidate.BottleneckStepRatio - 0.32f) * 500f;
                    if (candidate.OpeningGoodMoveRatio > 0f && candidate.OpeningGoodMoveRatio < 0.34f)
                        score += (0.34f - candidate.OpeningGoodMoveRatio) * 240f;
                }

                if (scope != "same-type" && scope != "shape-quality-wide")
                    score += 180f;
                if (!string.IsNullOrWhiteSpace(candidate.QualityFlags) &&
                    candidate.QualityFlags.IndexOf("ok", StringComparison.OrdinalIgnoreCase) < 0 &&
                    candidate.QualityFlags.IndexOf("NoRisk", StringComparison.OrdinalIgnoreCase) < 0)
                    score += 45f;

                if (score < bestScore)
                {
                    bestScore = score;
                    best = candidate;
                }
            }

            decision = best == null
                ? $"no-candidate|scope={scope}|targetType={targetType}"
                : $"yellow-replace|scope={scope}|target={targetChains:0.0}|softCap={softCap:0.0}|score={bestScore:0.0}";
            return best;
        }

        static string GetReplacementRowType(string targetType, ReplacementOption candidate)
        {
            if (candidate == null || string.IsNullOrWhiteSpace(candidate.Type))
                return targetType;
            if (!candidate.Type.StartsWith("hole", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(candidate.Type, "tutorial", StringComparison.OrdinalIgnoreCase))
                return candidate.Type;
            if (targetType.StartsWith("shape", StringComparison.OrdinalIgnoreCase) &&
                candidate.Type.StartsWith("shape", StringComparison.OrdinalIgnoreCase))
                return candidate.Type;
            if (!targetType.StartsWith("shape", StringComparison.OrdinalIgnoreCase) &&
                !candidate.Type.StartsWith("shape", StringComparison.OrdinalIgnoreCase) &&
                !candidate.Type.StartsWith("hole", StringComparison.OrdinalIgnoreCase))
                return candidate.Type;
            return targetType;
        }

        static void ApplyYellowCandidateRow(Dictionary<string, string> row, ReplacementOption candidate, string targetType, string flags, string decision)
        {
            Set(row, "bucket", candidate.Bucket);
            Set(row, "type", targetType);
            Set(row, "score", Mathf.RoundToInt(candidate.Score).ToString(Inv));
            Set(row, "levelId", candidate.LevelId);
            Set(row, "path", candidate.AssetPath);
            Set(row, "assetPath", candidate.AssetPath);
            Set(row, "packs", candidate.PackId);
            Set(row, "width", candidate.Width.ToString(Inv));
            Set(row, "height", candidate.Height.ToString(Inv));
            Set(row, "chains", candidate.Chains.ToString(Inv));
            Set(row, "tiles", candidate.ArrowTiles.ToString(Inv));
            Set(row, "coverage", F(candidate.Coverage));
            Set(row, "openers", candidate.Openers.ToString(Inv));
            Set(row, "avgChoices", F(candidate.AvgChoices));
            Set(row, "unlockBursts", candidate.UnlockBursts);
            Set(row, "dependencyBlocks", candidate.DependencyBlocks);
            Set(row, "families", candidate.Families);
            Set(row, "qualityFlags", AppendNote(candidate.QualityFlags, $"yellow_replacement:{flags}"));
            Set(row, "portableSolved", "True");
            Set(row, "portableScore", Mathf.RoundToInt(candidate.PortableScore > 0f ? candidate.PortableScore : candidate.Score).ToString(Inv));
            Set(row, "portableQuality", string.IsNullOrWhiteSpace(candidate.PortableQuality) ? "yellow_replacement" : candidate.PortableQuality);
            Set(row, "selectionReason", decision);
        }

        static void ApplyYellowRhythmRow(Dictionary<string, string> row, ReplacementOption candidate, string targetType, string flags)
        {
            Set(row, "bucket", candidate.Bucket);
            Set(row, "type", targetType);
            Set(row, "chains", candidate.Chains.ToString(Inv));
            Set(row, "levelId", candidate.LevelId);
            Set(row, "note", AppendNote(Get(row, "note"), $"yellow-replaced:{flags}"));
        }

        static string SkippedYellowLine(int order, string type, string flags, string levelId, string chains, string decision)
        {
            return string.Join(",",
                order.ToString(Inv),
                EscapeCsv(type),
                EscapeCsv(flags),
                EscapeCsv(levelId),
                EscapeCsv(chains),
                "",
                "",
                "",
                "",
                "",
                EscapeCsv(decision));
        }

        static string MapShapeCampaignType(Dictionary<string, string> row)
        {
            string theme = Get(row, "theme").Trim().ToLowerInvariant();
            string haystack = (theme + " " + Get(row, "subtheme") + " " + Get(row, "shapeTag") + " " + Get(row, "assetName") + " " + Get(row, "assetPath")).ToLowerInvariant();
            if (theme.Contains("magic"))
                return "shape_magic";
            if (theme.Contains("landmark"))
                return "shape_landmark";
            if (theme.Contains("nature"))
                return "shape_nature";
            if (theme.Contains("ocean"))
                return "shape_ocean";
            if (theme.Contains("vehicle"))
                return "shape_vehicle";
            if (theme.Contains("tool") || haystack.Contains("tool") || haystack.Contains("eraser") || haystack.Contains("ruler"))
                return "shape_toolui";
            if (theme.Contains("space"))
                return "shape_space";
            if (theme.Contains("symbol"))
                return "shape_symbol";
            return "shape_object";
        }

        static float GetOrderChainSoftCap(int order)
        {
            if (order <= 50)
                return 92f;
            if (order <= 100)
                return 108f;
            if (order <= 200)
                return 126f;
            if (order <= 300)
                return 145f;
            if (order <= 400)
                return 165f;
            return 185f;
        }

        static void AddIfNotEmpty(HashSet<string> set, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                set.Add(value);
        }

        static string AppendNote(string current, string note)
        {
            if (string.IsNullOrWhiteSpace(note))
                return current ?? "";
            if (string.IsNullOrWhiteSpace(current))
                return note;
            return current + "|" + note;
        }

        static List<CampaignHoleReplacement> PickCampaignHoleReplacements(
            IReadOnlyList<Dictionary<string, string>> holeRows,
            IReadOnlyList<HoleCandidate> candidates)
        {
            var result = new List<CampaignHoleReplacement>(holeRows.Count);
            var used = new HashSet<HoleCandidate>();
            var profileUse = new Dictionary<string, int>(StringComparer.Ordinal);
            var sizeUse = new Dictionary<string, int>(StringComparer.Ordinal);
            bool canStayUnderMainlineCap = candidates.Count(c => c.Level != null && c.Chains <= 98) >= holeRows.Count;

            for (int i = 0; i < holeRows.Count; i++)
            {
                var row = holeRows[i];
                int order = int.Parse(Get(row, "order"), Inv);
                float t = holeRows.Count <= 1 ? 0f : i / (float)(holeRows.Count - 1);
                int oldChains = TryInt(Get(row, "chains"), out int parsedChains) ? parsedChains : Mathf.RoundToInt(Mathf.Lerp(55f, 115f, t));
                float targetChains = Mathf.Lerp(52f, 118f, Mathf.Clamp01((order - 37f) / (492f - 37f)));
                targetChains = Mathf.Lerp(targetChains, oldChains, 0.35f);
                targetChains = Mathf.Min(targetChains, 96f);

                HoleCandidate best = null;
                float bestScore = float.MaxValue;
                foreach (var candidate in candidates)
                {
                    if (candidate.Level == null || used.Contains(candidate))
                        continue;
                    if (canStayUnderMainlineCap && candidate.Chains > 98)
                        continue;

                    string profileKey = candidate.Profile.Id;
                    string sizeKey = $"{candidate.Width}x{candidate.Height}";
                    profileUse.TryGetValue(profileKey, out int profileCount);
                    sizeUse.TryGetValue(sizeKey, out int sizeCount);

                    float score =
                        Mathf.Abs(candidate.Chains - targetChains) * 2.1f +
                        profileCount * 28f +
                        sizeCount * 18f -
                        candidate.Score * 0.03f;

                    if (candidate.Chains > 96)
                        score += (candidate.Chains - 96) * 18f + 80f;

                    if (score < bestScore)
                    {
                        bestScore = score;
                        best = candidate;
                    }
                }

                if (best == null)
                    throw new InvalidOperationException($"Not enough procedural hole candidates for order {order}.");

                used.Add(best);
                Increment(profileUse, best.Profile.Id);
                Increment(sizeUse, $"{best.Width}x{best.Height}");
                result.Add(new CampaignHoleReplacement { Row = row, Candidate = best });
            }

            return result;
        }

        static void ApplyCampaignHoleRow(Dictionary<string, string> row, HoleCandidate candidate)
        {
            Set(row, "bucket", "normal");
            Set(row, "type", "hole_rescue");
            Set(row, "score", Mathf.RoundToInt(candidate.Score).ToString(Inv));
            Set(row, "levelId", candidate.LevelId);
            Set(row, "path", candidate.AssetPath);
            Set(row, "assetPath", candidate.AssetPath);
            Set(row, "packs", "hole_procedural_candidates");
            Set(row, "width", candidate.Width.ToString(Inv));
            Set(row, "height", candidate.Height.ToString(Inv));
            Set(row, "chains", candidate.Chains.ToString(Inv));
            Set(row, "tiles", candidate.ArrowTiles.ToString(Inv));
            Set(row, "arrowTiles", candidate.ArrowTiles.ToString(Inv));
            Set(row, "blockTiles", candidate.BlockTiles.ToString(Inv));
            Set(row, "coverage", F(candidate.BoardFill));
            Set(row, "boardFill", F(candidate.BoardFill));
            Set(row, "playableFill", F(candidate.PlayableFill));
            Set(row, "openers", candidate.OpeningMoves.ToString(Inv));
            Set(row, "openingMoves", candidate.OpeningMoves.ToString(Inv));
            Set(row, "avgChoices", F(candidate.OpeningMoves));
            Set(row, "families", $"hole_procedural:{candidate.Profile.Id}");
            Set(row, "qualityFlags", "procedural_hole_standard");
            Set(row, "portableSolved", "True");
            Set(row, "portableQuality", "procedural_hole_standard");
            Set(row, "portableScore", Mathf.RoundToInt(candidate.Score).ToString(Inv));
            Set(row, "selectionReason", $"procedural-hole-replace|standard_8x9_center|profile={candidate.Profile.Id}");
        }

        static Metrics MeasureLevel(LevelDefinition level)
        {
            if (level == null || level.authoredLevel == null)
                throw new InvalidOperationException("Level has no authored data.");

            if (!AuthoredLevelBuilder.TryBuildBoard(level.authoredLevel, out BoardState board, out string buildError))
                throw new InvalidOperationException($"Failed to build board for {level.levelId}: {buildError}");

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            var metrics = Measure(board, rules);
            metrics.Width = board.width;
            metrics.Height = board.height;
            metrics.BoardFill = metrics.ArrowTiles / Mathf.Max(1f, board.width * board.height);
            return metrics;
        }

        static void PatchCampaignSelectionRowForLevel(
            int order,
            LevelDefinition level,
            string levelPath,
            Metrics metrics,
            string bucket,
            string type,
            string packId,
            string reason)
        {
            var table = ReadCsvTable(ToAbsolutePath(SelectionPath));
            var row = table.Rows.FirstOrDefault(r => Get(r, "order") == order.ToString(Inv));
            if (row == null)
                throw new InvalidOperationException($"Selection row not found for order {order}.");

            ApplyGenericLevelRow(row, level, levelPath, metrics, bucket, type, packId, reason);
            WriteCsvTable(ToAbsolutePath(SelectionPath), table);
        }

        static void PatchFront20SummaryRowForLevel(
            int order,
            LevelDefinition level,
            string levelPath,
            Metrics metrics,
            string bucket,
            string type,
            string packId,
            string reason)
        {
            string path = ToAbsolutePath(Front20FinalizedSummaryPath);
            if (!File.Exists(path))
                return;

            var table = ReadCsvTable(path);
            var row = table.Rows.FirstOrDefault(r => Get(r, "order") == order.ToString(Inv));
            if (row == null)
                return;

            ApplyGenericLevelRow(row, level, levelPath, metrics, bucket, type, packId, reason);
            WriteCsvTable(path, table);
        }

        static void PatchNoHolePreviewLevelAndMap(int originalOrder, LevelDefinition level, string levelPath, Metrics metrics)
        {
            var noHolePack = AssetDatabase.LoadAssetAtPath<LevelPack>(NoHolePreviewPackPath);
            if (noHolePack == null || noHolePack.levels == null)
                return;

            string mapPath = ToAbsolutePath(NoHolePreviewMapPath);
            if (!File.Exists(mapPath))
                return;

            var map = ReadCsvTable(mapPath);
            var row = map.Rows.FirstOrDefault(r => Get(r, "originalOrder") == originalOrder.ToString(Inv));
            if (row == null || !TryInt(Get(row, "previewOrder"), out int previewOrder))
                return;

            int index = previewOrder - 1;
            if (index >= 0 && index < noHolePack.levels.Length)
            {
                noHolePack.levels[index] = level;
                EditorUtility.SetDirty(noHolePack);
            }

            Set(row, "type", "normal");
            Set(row, "levelId", level.levelId);
            Set(row, "path", levelPath);
            Set(row, "chains", metrics.Chains.ToString(Inv));
            WriteCsvTable(mapPath, map);
        }

        static void ApplyGenericLevelRow(
            Dictionary<string, string> row,
            LevelDefinition level,
            string levelPath,
            Metrics metrics,
            string bucket,
            string type,
            string packId,
            string reason)
        {
            int score = Mathf.RoundToInt(metrics.AvgClearPerOpening * 18f + metrics.OpeningMoves * 5f + metrics.Chains);
            Set(row, "bucket", bucket);
            Set(row, "type", type);
            Set(row, "score", score.ToString(Inv));
            Set(row, "levelId", level.levelId);
            Set(row, "path", levelPath);
            Set(row, "assetPath", levelPath);
            Set(row, "packs", packId);
            Set(row, "width", metrics.Width.ToString(Inv));
            Set(row, "height", metrics.Height.ToString(Inv));
            Set(row, "chains", metrics.Chains.ToString(Inv));
            Set(row, "tiles", metrics.ArrowTiles.ToString(Inv));
            Set(row, "arrowTiles", metrics.ArrowTiles.ToString(Inv));
            Set(row, "blockTiles", metrics.BlockTiles.ToString(Inv));
            Set(row, "coverage", F(metrics.BoardFill));
            Set(row, "boardFill", F(metrics.BoardFill));
            Set(row, "playableFill", F(metrics.BoardFill));
            Set(row, "openers", metrics.OpeningMoves.ToString(Inv));
            Set(row, "openingMoves", metrics.OpeningMoves.ToString(Inv));
            Set(row, "avgChoices", F(metrics.AvgClearPerOpening));
            Set(row, "families", reason);
            Set(row, "qualityFlags", reason);
            Set(row, "portableSolved", "True");
            Set(row, "portableQuality", reason);
            Set(row, "portableScore", score.ToString(Inv));
            Set(row, "selectionReason", reason);
        }

        static bool IsHoleRow(Dictionary<string, string> row)
        {
            string type = Get(row, "type");
            string path = Get(row, "path");
            string assetPath = Get(row, "assetPath");
            return type.StartsWith("hole", StringComparison.OrdinalIgnoreCase) ||
                   path.IndexOf("/HoleMask/", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   path.IndexOf("\\HoleMask\\", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   assetPath.IndexOf("/HoleMask/", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   assetPath.IndexOf("\\HoleMask\\", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        static Metrics Measure(BoardState board, ArrowMagicRuleset rules)
        {
            var metrics = new Metrics();
            var visited = new HashSet<int>();
            var chainSet = new HashSet<int>();

            for (int i = 0; i < board.tiles.Length; i++)
            {
                var tile = board.tiles[i];
                if (tile.type == TileType.Block)
                {
                    metrics.BlockTiles++;
                    continue;
                }

                if (tile.type != TileType.Arrow)
                    continue;

                metrics.ArrowTiles++;
                if (visited.Contains(i))
                    continue;

                chainSet.Clear();
                ArrowChainUtility.CollectFullChain(board, new Vector2Int(i % board.width, i / board.width), 0, chainSet);
                foreach (int idx in chainSet)
                    visited.Add(idx);
                if (chainSet.Count > 0)
                {
                    metrics.Chains++;
                    metrics.MaxChain = Mathf.Max(metrics.MaxChain, chainSet.Count);
                }
            }

            CountOpeningMoves(board, rules, out metrics.OpeningMoves, out metrics.AvgClearPerOpening);
            return metrics;
        }

        static List<HoleCandidate> SelectDiversePreview(IReadOnlyList<HoleCandidate> source, int count)
        {
            var result = new List<HoleCandidate>(count);
            var used = new HashSet<HoleCandidate>();
            var perProfile = new Dictionary<string, int>(StringComparer.Ordinal);
            var perSize = new Dictionary<string, int>(StringComparer.Ordinal);

            int maxPerProfile = Mathf.Max(2, Mathf.CeilToInt(count / 10f));
            int maxPerSize = Mathf.Max(2, Mathf.CeilToInt(count / 8f));

            var bands = source
                .GroupBy(c => c.Profile.OrderBand)
                .OrderBy(g => g.Key)
                .Select(g => g.OrderByDescending(c => c.Score).ToList())
                .ToList();

            bool added;
            do
            {
                added = false;
                foreach (var band in bands)
                {
                    if (result.Count >= count)
                        break;

                    HoleCandidate picked = null;
                    foreach (var candidate in band)
                    {
                        if (used.Contains(candidate))
                            continue;

                        string profileKey = candidate.Profile.Id;
                        string sizeKey = $"{candidate.Width}x{candidate.Height}";
                        perProfile.TryGetValue(profileKey, out int profileCount);
                        perSize.TryGetValue(sizeKey, out int sizeCount);

                        if (profileCount >= maxPerProfile || sizeCount >= maxPerSize)
                            continue;

                        picked = candidate;
                        break;
                    }

                    if (picked == null)
                        picked = band.FirstOrDefault(c => !used.Contains(c));

                    if (picked == null)
                        continue;

                    used.Add(picked);
                    result.Add(picked);
                    Increment(perProfile, picked.Profile.Id);
                    Increment(perSize, $"{picked.Width}x{picked.Height}");
                    added = true;
                }
            } while (added && result.Count < count);

            return result;
        }

        static void CountOpeningMoves(BoardState board, ArrowMagicRuleset rules, out int openingMoves, out float avgClear)
        {
            openingMoves = 0;
            int clearTotal = 0;
            var seenChains = new HashSet<int>();
            var chain = new HashSet<int>();

            for (int i = 0; i < board.tiles.Length; i++)
            {
                if (board.tiles[i].type != TileType.Arrow)
                    continue;

                chain.Clear();
                ArrowChainUtility.CollectFullChain(board, new Vector2Int(i % board.width, i / board.width), 0, chain);
                int chainId = ChainId(chain);
                if (chainId < 0 || !seenChains.Add(chainId))
                    continue;

                var clone = CloneBoard(board);
                var move = new Move(new Vector2Int(i % board.width, i / board.width));
                if (!rules.TryApplyMove(clone, move, out MoveDelta delta))
                    continue;

                int cleared = CountCleared(delta);
                if (cleared <= 0)
                    continue;

                openingMoves++;
                clearTotal += cleared;
            }

            avgClear = openingMoves > 0 ? clearTotal / (float)openingMoves : 0f;
        }

        static int CountCleared(MoveDelta delta)
        {
            int count = 0;
            if (delta == null)
                return count;

            for (int i = 0; i < delta.changes.Count; i++)
            {
                var change = delta.changes[i];
                if (change.before.type == TileType.Arrow && change.after.type == TileType.Empty)
                    count++;
            }

            return count;
        }

        static bool TryConvertBoardToAuthored(BoardState board, out AuthoredLevelData authored, out string error)
        {
            authored = null;
            error = "";
            if (board == null)
            {
                error = "board null";
                return false;
            }

            var result = new AuthoredLevelData
            {
                width = board.width,
                height = board.height,
                arrows = new List<AuthoredArrowData>(),
                blockIndices = new List<int>()
            };

            var visited = new HashSet<int>();
            var chainSet = new HashSet<int>();

            for (int i = 0; i < board.tiles.Length; i++)
            {
                if (board.tiles[i].type == TileType.Block)
                {
                    result.blockIndices.Add(i);
                    continue;
                }

                if (visited.Contains(i) || board.tiles[i].type != TileType.Arrow)
                    continue;

                var start = new Vector2Int(i % board.width, i / board.width);
                chainSet.Clear();
                ArrowChainUtility.CollectFullChain(board, start, 0, chainSet);
                foreach (int idx in chainSet)
                    visited.Add(idx);

                if (chainSet.Count < 2)
                    continue;

                if (!TryBuildStableOrderedChain(board, chainSet, start, out var ordered))
                    continue;

                var arrow = new AuthoredArrowData
                {
                    indices = new List<int>(ordered.Count),
                    colorIndex = result.arrows.Count
                };
                for (int j = 0; j < ordered.Count; j++)
                    arrow.indices.Add(ordered[j].x + ordered[j].y * board.width);

                result.arrows.Add(arrow);
            }

            if (result.arrows.Count == 0)
            {
                error = "no authored arrows";
                return false;
            }

            authored = result;
            return true;
        }

        static bool TryBuildStableOrderedChain(BoardState board, HashSet<int> chainSet, Vector2Int preferredStart, out List<Vector2Int> ordered)
        {
            ordered = null;
            var starts = new List<Vector2Int>(chainSet.Count);
            int preferredIndex = preferredStart.x + preferredStart.y * board.width;
            if (chainSet.Contains(preferredIndex))
                starts.Add(preferredStart);

            foreach (int idx in chainSet)
            {
                var start = new Vector2Int(idx % board.width, idx / board.width);
                if (start != preferredStart)
                    starts.Add(start);
            }

            foreach (var start in starts)
            {
                if (!ArrowChainUtility.TryBuildOrderedChain(board, chainSet, start, out var candidate, out _, out _, out _))
                    continue;

                if (TryFixChainOrder(board, chainSet, ref candidate))
                {
                    ordered = candidate;
                    return true;
                }
            }

            return false;
        }

        static bool TryFixChainOrder(BoardState board, HashSet<int> chainSet, ref List<Vector2Int> ordered)
        {
            if (ordered == null || ordered.Count != chainSet.Count || ordered.Count < 2)
                return false;

            if (TryBuildSingleAuthoredChain(board, ordered))
                return true;

            var reversed = new List<Vector2Int>(ordered.Count);
            for (int i = ordered.Count - 1; i >= 0; i--)
                reversed.Add(ordered[i]);

            if (!TryBuildSingleAuthoredChain(board, reversed))
                return false;

            ordered = reversed;
            return true;
        }

        static bool TryBuildSingleAuthoredChain(BoardState board, List<Vector2Int> ordered)
        {
            var indices = new List<int>(ordered.Count);
            var expected = new HashSet<int>(ordered.Count);
            for (int i = 0; i < ordered.Count; i++)
            {
                Vector2Int pos = ordered[i];
                if (!board.InBounds(pos.x, pos.y))
                    return false;

                int idx = board.Index(pos.x, pos.y);
                if (board.tiles[idx].type != TileType.Arrow)
                    return false;

                indices.Add(idx);
                expected.Add(idx);
            }

            var temp = new AuthoredLevelData
            {
                width = board.width,
                height = board.height,
                arrows = new List<AuthoredArrowData>
                {
                    new AuthoredArrowData { indices = indices, colorIndex = 0 }
                },
                blockIndices = new List<int>()
            };

            if (!AuthoredLevelBuilder.TryBuildBoard(temp, out BoardState rebuilt, out _))
                return false;

            int rebuiltCount = 0;
            for (int i = 0; i < rebuilt.tiles.Length; i++)
            {
                if (rebuilt.tiles[i].type != TileType.Arrow)
                    continue;
                rebuiltCount++;
                if (!expected.Contains(i))
                    return false;
            }

            return rebuiltCount == ordered.Count;
        }

        static void SaveCandidateAsset(HoleCandidate candidate)
        {
            var level = AssetDatabase.LoadAssetAtPath<LevelDefinition>(candidate.AssetPath);
            bool isNew = level == null;
            if (level == null)
                level = ScriptableObject.CreateInstance<LevelDefinition>();

            level.name = candidate.AssetName;
            level.levelId = candidate.LevelId;
            level.source = LevelDefinition.LevelSource.Authored;
            level.board.width = candidate.Width;
            level.board.height = candidate.Height;
            level.board.seed = candidate.Seed;
            level.generation.arrowCoverage = candidate.PlayableFill;
            level.generation.initialMovableArrowCount = candidate.OpeningMoves;
            level.generation.targetDifficultyScore = Mathf.RoundToInt(candidate.Score);
            level.generation.fixedGenerationSeed = candidate.Seed;
            level.generation.minPathLen = candidate.Profile.MinChainLen;
            level.generation.maxPathLength = candidate.Profile.MaxChainLen;
            level.generation.twistiness = 1f - candidate.Profile.OutwardBias;
            level.generation.validateWithGreedy = true;
            level.authoredLevel = CloneAuthored(candidate.Authored);
            level.lose.blockedLoseLimit = 3;
            level.arrowColorMode = BoardController.ArrowColorMode.UsePalette;
            level.arrowColorMaskQuantizeSteps = 16;
            level.tintOnHit = true;
            level.hitTint = Color.white;
            EditorUtility.SetDirty(level);

            if (isNew)
                AssetDatabase.CreateAsset(level, candidate.AssetPath);

            candidate.Level = level;
        }

        static LevelPack SavePack(string path, string packId, string displayName, IReadOnlyList<HoleCandidate> candidates)
        {
            EnsureAssetFolder(Path.GetDirectoryName(path)?.Replace('\\', '/'));
            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(path);
            bool isNew = pack == null;
            if (pack == null)
                pack = ScriptableObject.CreateInstance<LevelPack>();

            pack.packId = packId;
            pack.displayName = displayName;
            pack.levels = candidates.Where(c => c.Level != null).Select(c => c.Level).ToArray();
            EditorUtility.SetDirty(pack);

            if (isNew)
                AssetDatabase.CreateAsset(pack, path);

            return pack;
        }

        static LevelPack SaveLevelPack(string path, string packId, string displayName, IReadOnlyList<LevelDefinition> levels)
        {
            EnsureAssetFolder(Path.GetDirectoryName(path)?.Replace('\\', '/'));
            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(path);
            bool isNew = pack == null;
            if (pack == null)
                pack = ScriptableObject.CreateInstance<LevelPack>();

            pack.packId = packId;
            pack.displayName = displayName;
            pack.levels = levels.Where(l => l != null).ToArray();
            EditorUtility.SetDirty(pack);

            if (isNew)
                AssetDatabase.CreateAsset(pack, path);

            return pack;
        }

        static void ExportReferenceHoleMask()
        {
            const int width = 24;
            const int height = 36;
            const int blockerWidth = 8;
            const int blockerHeight = 9;

            int blockerX = (width - blockerWidth) / 2;
            int blockerY = (height - blockerHeight) / 2;
            var tex = new Texture2D(width, height, TextureFormat.RGBA32, false)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp
            };

            var transparent = new Color32(0, 0, 0, 0);
            var white = new Color32(255, 255, 255, 255);
            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                bool roundedCorner =
                    (x == 0 && (y == 0 || y == height - 1)) ||
                    (x == width - 1 && (y == 0 || y == height - 1));
                bool centerBlocker =
                    x >= blockerX && x < blockerX + blockerWidth &&
                    y >= blockerY && y < blockerY + blockerHeight;

                tex.SetPixel(x, y, roundedCorner || centerBlocker ? transparent : white);
            }

            tex.Apply(false, false);
            File.WriteAllBytes(ToAbsolutePath(HoleReferenceMaskPath), tex.EncodeToPNG());
            UnityEngine.Object.DestroyImmediate(tex);
            AssetDatabase.ImportAsset(HoleReferenceMaskPath);
        }

        static AuthoredLevelData CloneAuthored(AuthoredLevelData source)
        {
            return new AuthoredLevelData
            {
                width = source.width,
                height = source.height,
                blockIndices = source.blockIndices != null ? new List<int>(source.blockIndices) : new List<int>(),
                arrows = source.arrows?.Select(a => new AuthoredArrowData
                {
                    colorIndex = a.colorIndex,
                    indices = a.indices != null ? new List<int>(a.indices) : new List<int>()
                }).ToList() ?? new List<AuthoredArrowData>()
            };
        }

        static BoardState CloneBoard(BoardState source)
        {
            var clone = new BoardState(source.width, source.height);
            Array.Copy(source.tiles, clone.tiles, source.tiles.Length);
            return clone;
        }

        static int ChainId(HashSet<int> chain)
        {
            int id = int.MaxValue;
            foreach (int idx in chain)
                id = Mathf.Min(id, idx);
            return id == int.MaxValue ? -1 : id;
        }

        static int CountArrowTiles(AuthoredLevelData authored)
        {
            int total = 0;
            if (authored?.arrows == null)
                return 0;
            for (int i = 0; i < authored.arrows.Count; i++)
                total += authored.arrows[i]?.indices?.Count ?? 0;
            return total;
        }

        static string ToReportLine(HoleCandidate c, string status, string details)
        {
            return string.Join(",",
                c.Rank.ToString(Inv),
                EscapeCsv(status),
                EscapeCsv(c.Profile.Id),
                EscapeCsv(c.BlockerId),
                EscapeCsv(c.LevelId),
                EscapeCsv(c.AssetPath),
                c.Width.ToString(Inv),
                c.Height.ToString(Inv),
                c.Chains.ToString(Inv),
                c.ArrowTiles.ToString(Inv),
                c.BlockTiles.ToString(Inv),
                F(c.BoardFill),
                F(c.PlayableFill),
                c.OpeningMoves.ToString(Inv),
                F(c.AvgClearPerOpening),
                c.MaxChain.ToString(Inv),
                c.Seed.ToString(Inv),
                F(c.Score),
                EscapeCsv(details + "; " + c.Details));
        }

        static void Increment(Dictionary<string, int> counts, string key)
        {
            counts.TryGetValue(key, out int value);
            counts[key] = value + 1;
        }

        static void AttachPackToDemo(LevelPack pack, string logTag)
        {
            if (pack == null)
                return;

            var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(
                DemoScenePath,
                UnityEditor.SceneManagement.OpenSceneMode.Single);
            var progression = UnityEngine.Object.FindFirstObjectByType<LevelProgression>();
            if (progression == null)
            {
                Debug.LogWarning($"[{logTag}] LevelProgression not found in {DemoScenePath}");
                return;
            }

            var so = new SerializedObject(progression);
            var activePack = so.FindProperty("activePack");
            if (activePack == null)
            {
                Debug.LogWarning($"[{logTag}] LevelProgression.activePack serialized field not found.");
                return;
            }

            activePack.objectReferenceValue = pack;
            so.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(progression);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(scene);
            UnityEditor.SceneManagement.EditorSceneManager.SaveScene(scene);
        }

        static void EnsureAssetFolder(string folder)
        {
            if (string.IsNullOrWhiteSpace(folder))
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

        static string ToAbsolutePath(string assetPath)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            return Path.GetFullPath(Path.Combine(projectRoot, assetPath.Replace('/', Path.DirectorySeparatorChar)));
        }

        static CsvTable ReadCsvTable(string fullPath)
        {
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("CSV file not found.", fullPath);

            var lines = File.ReadAllLines(fullPath);
            var table = new CsvTable();
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
                {
                    string value = h < values.Count ? values[h] : "";
                    row[table.Headers[h]] = value;
                }

                table.Rows.Add(row);
            }

            return table;
        }

        static void WriteCsvTable(string fullPath, CsvTable table)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath) ?? ".");

            var lines = new List<string>(table.Rows.Count + 1)
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
            if (row == null || string.IsNullOrEmpty(key))
                return;

            row[key] = value ?? "";
        }

        static bool TryInt(string value, out int result)
        {
            return int.TryParse(value, NumberStyles.Integer, Inv, out result);
        }

        static int ParseInt(string value)
        {
            return int.TryParse(value, NumberStyles.Integer, Inv, out int result) ? result : 0;
        }

        static float ParseFloat(string value)
        {
            return float.TryParse(value, NumberStyles.Float, Inv, out float result) ? result : 0f;
        }

        static string EscapeCsv(string value)
        {
            value ??= "";
            if (value.Contains('"') || value.Contains(',') || value.Contains('\n') || value.Contains('\r'))
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            return value;
        }

        static string F(float value) => value.ToString("0.###", Inv);

        sealed class CsvTable
        {
            public List<string> Headers = new List<string>();
            public List<Dictionary<string, string>> Rows = new List<Dictionary<string, string>>();
        }

        sealed class BuildResult
        {
            public List<HoleCandidate> Accepted = new List<HoleCandidate>();
            public List<HoleCandidate> Sorted = new List<HoleCandidate>();
            public List<HoleCandidate> Preview = new List<HoleCandidate>();
            public LevelPack AllPack;
            public LevelPack PreviewPack;
            public int Attempts;
        }

        sealed class CampaignHoleReplacement
        {
            public Dictionary<string, string> Row;
            public HoleCandidate Candidate;
        }

        sealed class Front20PatchCandidate
        {
            public int Order;
            public string Type = "";
            public string Bucket = "";
            public string Quality = "";
            public HoleCandidate Candidate;
        }

        struct BoundaryStart
        {
            public int Head;
            public int Second;
            public Vector2Int Outward;
        }

        struct StepCandidate
        {
            public int Index;
            public Vector2Int Direction;
            public float Score;
        }

        enum ExitSide
        {
            Left,
            Right,
            Bottom,
            Top
        }

        sealed class ReplacementOption
        {
            public string SourceKind = "";
            public string Bucket = "";
            public string Type = "";
            public float Score;
            public string LevelId = "";
            public string AssetPath = "";
            public string PackId = "";
            public int Width;
            public int Height;
            public int Chains;
            public int ArrowTiles;
            public float Coverage;
            public int Openers;
            public float AvgChoices;
            public float AvgClearPerMove;
            public float OpeningGoodMoveRatio;
            public float BottleneckStepRatio;
            public float HardBottleneckStepRatio;
            public string UnlockBursts = "";
            public string DependencyBlocks = "";
            public string Families = "";
            public string QualityFlags = "";
            public float PortableScore;
            public string PortableQuality = "";
            public LevelDefinition Level;
        }

        sealed class HoleProfile
        {
            public int Index;
            public string Id = "";
            public int Width;
            public int Height;
            public int MinChains;
            public int MaxChains;
            public int IdealChains;
            public float Coverage;
            public int MinChainLen;
            public int MaxChainLen;
            public float OutwardBias;
            public float MinPlayableFill;
            public int MinOpeningMoves;
            public float MinAvgClearPerOpening;
            public int GreedyBudget;
            public int OrderBand;
            public int MinPlayableCells;
        }

        sealed class HoleCandidate
        {
            public int Rank;
            public HoleProfile Profile;
            public string LevelId = "";
            public string AssetName = "";
            public string AssetPath = "";
            public LevelDefinition Level;
            public AuthoredLevelData Authored;
            public int Seed;
            public int Width;
            public int Height;
            public int Chains;
            public int ArrowTiles;
            public int BlockTiles;
            public float BoardFill;
            public float PlayableFill;
            public int OpeningMoves;
            public float AvgClearPerOpening;
            public int MaxChain;
            public float Score;
            public string BlockerId = "";
            public string Details = "";
        }

        struct Metrics
        {
            public int Width;
            public int Height;
            public int Chains;
            public int ArrowTiles;
            public int BlockTiles;
            public int OpeningMoves;
            public float AvgClearPerOpening;
            public int MaxChain;
            public float BoardFill;
        }
    }
}
#endif
