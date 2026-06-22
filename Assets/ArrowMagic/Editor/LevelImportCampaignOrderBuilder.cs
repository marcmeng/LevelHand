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
    public static class LevelImportCampaignOrderBuilder
    {
        const string ReportFolder = "Assets/ArrowMagic/SOData/Reports/LevelImportV1";
        const string DifficultyManifestPath = ReportFolder + "/level_import_v1_difficulty_manifest.csv";
        const string OutputOrderPath = ReportFolder + "/ordered_campaign_import_v1_experience.csv";
        const string OutputFront20Path = ReportFolder + "/ordered_campaign_import_v1_experience_front20.csv";
        const string OutputFront30Path = ReportFolder + "/ordered_campaign_import_v1_experience_front30.csv";
        const string OutputFront50Path = ReportFolder + "/ordered_campaign_import_v1_experience_front50.csv";
        const string OutputSummaryPath = ReportFolder + "/ordered_campaign_import_v1_experience_summary.csv";
        const string OutputNotesPath = ReportFolder + "/ordered_campaign_import_v1_experience_notes.md";
        const string OutputBonusPath = ReportFolder + "/ordered_campaign_import_v1_experience_bonus.csv";
        const string DemoScenePath = "Assets/ArrowMagic/Scenes/Demo.unity";
        const string PreviewPackFolder = "Assets/ArrowMagic/SOData/Packs/LevelImportV1";
        const string Front20PreviewPackPath = PreviewPackFolder + "/CampaignFront20PreviewPack.asset";
        const string Front30PreviewPackPath = PreviewPackFolder + "/CampaignFront30PreviewPack.asset";
        const string Front50PreviewPackPath = PreviewPackFolder + "/CampaignFront50PreviewPack.asset";
        const string FullPreviewPackPath = PreviewPackFolder + "/Campaign500PreviewPack.asset";
        const string OutputPackReportPath = ReportFolder + "/ordered_campaign_import_v1_pack_report.txt";
        const string TutorialSimpleSourcePool = "tutorial_simple";
        const string TutorialSimpleFolder = "Assets/ArrowMagic/SOData/Levels/LevelImportV1/TutorialSimple";

        const int TargetCount = 500;
        const int TargetShapeCount = 80;
        const int TargetHoleCount = 24;
        const int FrontTwentyForcedShapeCount = 4;
        const int FrontTwentyForcedHoleCount = 2;

        static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

        [MenuItem("Tools/Arrow Magic/Level Import/Build Campaign Order Preview V1")]
        public static void BuildCampaignOrderPreview()
        {
            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            string manifestPath = ToAbsolutePath(projectRoot, DifficultyManifestPath);
            var all = ReadCsv(manifestPath).Select(Candidate.FromRecord).ToList();
            all.AddRange(EnsureTutorialSimpleCandidates());

            var usable = all
                .Where(c => string.IsNullOrEmpty(c.BuildError))
                .Where(c => c.RiskTags == "NoRisk")
                .ToList();
            var noBuildError = all
                .Where(c => string.IsNullOrEmpty(c.BuildError))
                .ToList();

            var normalPool = usable.Where(c => c.SourceKind == "normal").ToList();
            var shapePool = usable.Where(c => c.SourceKind == "shape").ToList();
            var holePool = usable.Where(c => c.SourceKind == "hole").ToList();

            if (normalPool.Count < TargetCount - TargetShapeCount - TargetHoleCount)
                throw new InvalidOperationException($"Not enough no-risk normal candidates: {normalPool.Count}");
            if (shapePool.Count < TargetShapeCount)
                throw new InvalidOperationException($"Not enough no-risk shape candidates: {shapePool.Count}");
            if (holePool.Count < TargetHoleCount)
                throw new InvalidOperationException($"Not enough no-risk hole candidates: {holePool.Count}");

            var plan = BuildSlotPlan();
            var used = new HashSet<string>(StringComparer.Ordinal);
            var ordered = new List<OrderedLevel>(TargetCount);

            for (int level = 1; level <= TargetCount; level++)
            {
                SlotPlan slot = plan[level];
                SlotKind kind = slot.Kind;
                float idealScore = slot.ForcedIdealScore ?? IdealDifficultyScore(level, kind, slot.ExperienceRole);
                string targetDifficulty = slot.ForcedTargetDifficulty ?? TargetDifficultyTag(level, kind, slot.ExperienceRole);
                IReadOnlyList<Candidate> pool = kind switch
                {
                    SlotKind.Shape => shapePool,
                    SlotKind.Hole => holePool,
                    _ => normalPool
                };
                if (!string.IsNullOrEmpty(slot.ForcedSourcePool))
                {
                    pool = noBuildError
                        .Where(c => c.SourceKind == SourceKindForSlot(kind))
                        .Where(c => c.SourcePool == slot.ForcedSourcePool)
                        .ToList();
                }

                Candidate selected = PickBestCandidate(pool, used, ordered, level, slot, idealScore, targetDifficulty);
                used.Add(selected.UniqueKey);
                ordered.Add(new OrderedLevel
                {
                    CampaignOrder = level,
                    SlotKind = kind.ToString(),
                    TemplateId = slot.TemplateId,
                    ExperienceRole = slot.ExperienceRole,
                    ContentSeries = slot.ContentSeries,
                    UnlockTag = slot.UnlockTag,
                    StoryBeat = slot.StoryBeat,
                    IdealScore = idealScore,
                    TargetDifficultyTag = targetDifficulty,
                    StageRelativeDifficultyTag = StageRelativeDifficultyTag(level, selected, targetDifficulty, slot.ExperienceRole),
                    Candidate = selected,
                    SelectionReason = BuildReason(level, kind, slot.TemplateId, slot.ExperienceRole, idealScore, targetDifficulty, selected)
                });
            }

            var bonusEvents = BuildBonusSideEvents();

            Directory.CreateDirectory(ToAbsolutePath(projectRoot, ReportFolder));
            WriteOrder(ToAbsolutePath(projectRoot, OutputOrderPath), ordered);
            WriteOrder(ToAbsolutePath(projectRoot, OutputFront20Path), ordered.Take(20).ToList());
            WriteOrder(ToAbsolutePath(projectRoot, OutputFront30Path), ordered.Take(30).ToList());
            WriteOrder(ToAbsolutePath(projectRoot, OutputFront50Path), ordered.Take(50).ToList());
            WriteBonusEvents(ToAbsolutePath(projectRoot, OutputBonusPath), bonusEvents);
            WriteSummary(ToAbsolutePath(projectRoot, OutputSummaryPath), ordered, all, usable, bonusEvents);
            WriteNotes(ToAbsolutePath(projectRoot, OutputNotesPath), ordered, all, usable, bonusEvents);

            AssetDatabase.Refresh();
            Debug.Log($"[LevelImportCampaignOrderBuilder] Wrote {ordered.Count} rows to {OutputOrderPath}");
        }

        [MenuItem("Tools/Arrow Magic/Level Import/Build Front20 Preview Pack And Attach Demo")]
        public static void BuildFront20PreviewPackAndAttachDemo()
        {
            BuildCampaignOrderPreview();

            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            var front20Rows = ReadCsv(ToAbsolutePath(projectRoot, OutputFront20Path));
            var fullRows = ReadCsv(ToAbsolutePath(projectRoot, OutputOrderPath));

            LevelPack front20Pack = BuildPackFromRows(
                front20Rows,
                Front20PreviewPackPath,
                "campaign_front20_preview_v1",
                "Campaign Front20 Preview V1");

            AttachPackToDemo(front20Pack, "LevelImportFront20Preview");

            LevelPack fullPack = null;
            try
            {
                fullPack = BuildPackFromRows(
                    fullRows,
                    FullPreviewPackPath,
                    "campaign_500_preview_v1",
                    "Campaign 500 Preview V1");
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[LevelImportCampaignOrderBuilder] Full 500 preview pack skipped: {ex.Message}");
            }

            WritePackReport(projectRoot, front20Pack, fullPack, front20Rows, "Front20", Front20PreviewPackPath);

            AssetDatabase.Refresh();
            Debug.Log($"[LevelImportCampaignOrderBuilder] Built preview packs. front20={Front20PreviewPackPath}, full={FullPreviewPackPath}");
        }

        [MenuItem("Tools/Arrow Magic/Level Import/Build Front30 Preview Pack And Attach Demo")]
        public static void BuildFront30PreviewPackAndAttachDemo()
        {
            BuildCampaignOrderPreview();

            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            var front30Rows = ReadCsv(ToAbsolutePath(projectRoot, OutputFront30Path));
            var fullRows = ReadCsv(ToAbsolutePath(projectRoot, OutputOrderPath));

            LevelPack front30Pack = BuildPackFromRows(
                front30Rows,
                Front30PreviewPackPath,
                "campaign_front30_preview_v1",
                "Campaign Front30 Preview V1");

            AttachPackToDemo(front30Pack, "LevelImportFront30Preview");

            LevelPack fullPack = null;
            try
            {
                fullPack = BuildPackFromRows(
                    fullRows,
                    FullPreviewPackPath,
                    "campaign_500_preview_v1",
                    "Campaign 500 Preview V1");
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[LevelImportCampaignOrderBuilder] Full 500 preview pack skipped: {ex.Message}");
            }

            WritePackReport(projectRoot, front30Pack, fullPack, front30Rows, "Front30", Front30PreviewPackPath);

            AssetDatabase.Refresh();
            Debug.Log($"[LevelImportCampaignOrderBuilder] Built preview packs. front30={Front30PreviewPackPath}, full={FullPreviewPackPath}");
        }

        [MenuItem("Tools/Arrow Magic/Level Import/Build Front50 Preview Pack And Attach Demo")]
        public static void BuildFront50PreviewPackAndAttachDemo()
        {
            BuildCampaignOrderPreview();

            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            var front50Rows = ReadCsv(ToAbsolutePath(projectRoot, OutputFront50Path));
            var previewRows = InsertPreviewBonusRows(projectRoot, front50Rows);
            var fullRows = ReadCsv(ToAbsolutePath(projectRoot, OutputOrderPath));

            LevelPack front50Pack = BuildPackFromRows(
                previewRows,
                Front50PreviewPackPath,
                "campaign_front50_preview_with_bonus_v1",
                "Campaign Front50 Preview With Bonus V1");

            AttachPackToDemo(front50Pack, "LevelImportFront50Preview");

            LevelPack fullPack = null;
            try
            {
                fullPack = BuildPackFromRows(
                    fullRows,
                    FullPreviewPackPath,
                    "campaign_500_preview_v1",
                    "Campaign 500 Preview V1");
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[LevelImportCampaignOrderBuilder] Full 500 preview pack skipped: {ex.Message}");
            }

            WritePackReport(projectRoot, front50Pack, fullPack, previewRows, "Front50WithBonus", Front50PreviewPackPath);

            AssetDatabase.Refresh();
            Debug.Log($"[LevelImportCampaignOrderBuilder] Built preview packs. front50={Front50PreviewPackPath}, full={FullPreviewPackPath}");
        }

        static IReadOnlyList<Dictionary<string, string>> InsertPreviewBonusRows(
            string projectRoot,
            IReadOnlyList<Dictionary<string, string>> mainRows)
        {
            var result = new List<Dictionary<string, string>>(mainRows.Count + 1);
            Dictionary<string, string> bonusRow = BuildPreviewBonusRow(projectRoot);
            bool inserted = false;

            foreach (var row in mainRows)
            {
                result.Add(row);
                if (!inserted && Get(row, "campaignOrder") == "19")
                {
                    result.Add(bonusRow);
                    inserted = true;
                }
            }

            if (!inserted)
                result.Add(bonusRow);

            return result;
        }

        static Dictionary<string, string> BuildPreviewBonusRow(string projectRoot)
        {
            var bonusSource = ReadCsv(ToAbsolutePath(projectRoot, DifficultyManifestPath))
                .FirstOrDefault(row => Get(row, "sourcePool") == "normal_campaign500" && Get(row, "order") == "31");
            if (bonusSource == null)
                throw new InvalidOperationException("Bonus preview source not found: normal_campaign500 order 31");

            Candidate candidate = Candidate.FromRecord(bonusSource);
            const int afterLevel = 19;
            const string role = "BonusCandidate";
            const string target = "Flow";

            return new Dictionary<string, string>(StringComparer.Ordinal)
            {
                ["campaignOrder"] = "19B",
                ["slotKind"] = "Bonus",
                ["templateId"] = "BonusSide",
                ["experienceRole"] = role,
                ["contentSeries"] = "BonusReward",
                ["unlockTag"] = "None",
                ["storyBeat"] = "FirstBonusAfterVeryHard",
                ["idealScore"] = "95",
                ["targetDifficultyTag"] = target,
                ["stageRelativeDifficultyTag"] = StageRelativeDifficultyTag(afterLevel, candidate, target, role),
                ["stageDifficultyScore"] = StageDifficultyScore(afterLevel, candidate, target, role).ToString(Inv),
                ["sourcePool"] = candidate.SourcePool,
                ["sourceKind"] = candidate.SourceKind,
                ["sourceOrder"] = candidate.SourceOrder.ToString(Inv),
                ["levelId"] = candidate.LevelId,
                ["assetName"] = "BonusReward_" + candidate.AssetName,
                ["difficultyTagV1"] = candidate.DifficultyTagV1,
                ["difficultyScoreV1"] = candidate.DifficultyScoreV1.ToString(Inv),
                ["pressureTag"] = candidate.PressureTag,
                ["paceTag"] = candidate.PaceTag,
                ["startTag"] = candidate.StartTag,
                ["clearTag"] = candidate.ClearTag,
                ["choiceTag"] = candidate.ChoiceTag,
                ["shapeTag"] = candidate.ShapeTag,
                ["visualTag"] = candidate.VisualTag,
                ["noveltyTag"] = candidate.NoveltyTag,
                ["mechanicTag"] = candidate.MechanicTag,
                ["riskTags"] = candidate.RiskTags,
                ["reasonTags"] = candidate.ReasonTags,
                ["theme"] = "Bonus",
                ["width"] = candidate.Width.ToString(Inv),
                ["height"] = candidate.Height.ToString(Inv),
                ["chains"] = candidate.Chains.ToString(Inv),
                ["assetPath"] = candidate.AssetPath,
                ["selectionReason"] = "bonus-side-event|after=19|placeholder-main-rules"
            };
        }

        static LevelPack BuildPackFromRows(
            IReadOnlyList<Dictionary<string, string>> rows,
            string packPath,
            string packId,
            string displayName)
        {
            if (rows == null || rows.Count == 0)
                throw new InvalidOperationException($"No rows for pack {packPath}");

            string folder = Path.GetDirectoryName(packPath)?.Replace("\\", "/");
            EnsureAssetFolder(folder);

            var levels = new List<LevelDefinition>(rows.Count);
            var missing = new List<string>();
            foreach (var row in rows)
            {
                string path = row.TryGetValue("assetPath", out string value) ? value : "";
                if (string.IsNullOrWhiteSpace(path))
                {
                    missing.Add("<empty assetPath>");
                    continue;
                }

                var level = AssetDatabase.LoadAssetAtPath<LevelDefinition>(path);
                if (level == null)
                {
                    missing.Add(path);
                    continue;
                }

                levels.Add(level);
            }

            if (missing.Count > 0)
                throw new InvalidOperationException($"Missing {missing.Count} levels for {packPath}: {string.Join(" | ", missing.Take(8))}");
            if (levels.Count == 0)
                throw new InvalidOperationException($"No loadable levels for {packPath}");

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(packPath);
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
            Debug.Log($"[{logTag}] Attached pack to demo: {AssetDatabase.GetAssetPath(pack)}");
        }

        static void WritePackReport(
            string projectRoot,
            LevelPack previewPack,
            LevelPack fullPack,
            IReadOnlyList<Dictionary<string, string>> previewRows,
            string previewLabel,
            string previewPackPath)
        {
            var lines = new List<string>
            {
                "Level Import Preview Pack Report",
                $"Updated={DateTime.Now:yyyy-MM-dd HH:mm:ss}",
                $"{previewLabel}Pack={previewPackPath}",
                $"{previewLabel}Levels={(previewPack?.levels?.Length ?? 0).ToString(Inv)}",
                $"FullPack={FullPreviewPackPath}",
                $"FullLevels={(fullPack?.levels?.Length ?? 0).ToString(Inv)}",
                $"DemoAttached={previewPackPath}",
                "",
                $"{previewLabel}:"
            };

            foreach (var row in previewRows)
            {
                lines.Add(string.Join(" | ",
                    $"L{Get(row, "campaignOrder")}",
                    Get(row, "storyBeat"),
                    $"kind={Get(row, "slotKind")}",
                    $"chains={Get(row, "chains")}",
                    $"score={Get(row, "stageDifficultyScore")}",
                    $"target={Get(row, "targetDifficultyTag")}",
                    $"stage={Get(row, "stageRelativeDifficultyTag")}",
                    $"actual={Get(row, "difficultyTagV1")}",
                    Get(row, "assetName")));
            }

            WriteAllLinesWithRetry(ToAbsolutePath(projectRoot, OutputPackReportPath), lines);
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

        static IReadOnlyList<Candidate> EnsureTutorialSimpleCandidates()
        {
            EnsureAssetFolder(TutorialSimpleFolder);

            var specs = BuildTutorialSimpleSpecs();
            var candidates = new List<Candidate>(specs.Count);
            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });

            foreach (TutorialLevelSpec spec in specs)
            {
                if (!AuthoredLevelBuilder.TryBuildBoard(spec.Authored, out var board, out string buildError))
                    throw new InvalidOperationException($"Tutorial simple level build failed: {spec.LevelId} {buildError}");

                if (!GreedyValidator.TryClearAllByGreedy(board, rules, 120, out _))
                    throw new InvalidOperationException($"Tutorial simple level greedy failed: {spec.LevelId}");

                string assetPath = $"{TutorialSimpleFolder}/{spec.LevelId}.asset";
                var level = AssetDatabase.LoadAssetAtPath<LevelDefinition>(assetPath);
                bool isNew = level == null;
                if (level == null)
                    level = ScriptableObject.CreateInstance<LevelDefinition>();

                level.name = spec.AssetName;
                level.levelId = spec.LevelId;
                level.source = LevelDefinition.LevelSource.Authored;
                level.board.width = spec.Authored.width;
                level.board.height = spec.Authored.height;
                level.board.seed = 1000 + spec.Order;
                level.generation.arrowCoverage = CountArrowTiles(spec.Authored) / (float)Mathf.Max(1, spec.Authored.width * spec.Authored.height);
                level.generation.initialMovableArrowCount = spec.InitialMovableChains;
                level.generation.targetDifficultyScore = spec.DifficultyScore;
                level.generation.fixedGenerationSeed = 1000 + spec.Order;
                level.generation.minPathLen = 2;
                level.generation.maxPathLength = Mathf.Max(2, MaxArrowLength(spec.Authored));
                level.generation.twistiness = 0.2f;
                level.generation.validateWithGreedy = true;
                level.authoredLevel = CloneAuthored(spec.Authored);
                level.lose.blockedLoseLimit = 3;
                level.arrowColorMode = BoardController.ArrowColorMode.UsePalette;
                level.arrowColorMaskQuantizeSteps = 16;
                level.tintOnHit = true;
                level.hitTint = Color.white;
                EditorUtility.SetDirty(level);

                if (isNew)
                    AssetDatabase.CreateAsset(level, assetPath);

                candidates.Add(new Candidate
                {
                    SourcePool = TutorialSimpleSourcePool,
                    SourceKind = "normal",
                    SourceOrder = spec.Order,
                    LevelId = spec.LevelId,
                    AssetName = spec.AssetName,
                    ExistingType = "tutorial",
                    DifficultyScoreV1 = spec.DifficultyScore,
                    DifficultyTagV1 = "Flow",
                    PressureTag = "LowPressure",
                    PaceTag = "Flow",
                    StartTag = "StartWide",
                    ClearTag = "ClearBurst",
                    ChoiceTag = "ChoiceReadable",
                    ShapeTag = spec.ShapeTag,
                    VisualTag = "VisualSparse",
                    NoveltyTag = "Tutorial",
                    MechanicTag = spec.MechanicTag,
                    RiskTags = "NoRisk",
                    ReasonTags = "TutorialSimple",
                    Theme = "Tutorial",
                    Width = spec.Authored.width,
                    Height = spec.Authored.height,
                    Chains = spec.Authored.arrows.Count,
                    AssetPath = assetPath,
                    BuildError = ""
                });
            }

            AssetDatabase.SaveAssets();
            return candidates;
        }

        static List<TutorialLevelSpec> BuildTutorialSimpleSpecs()
        {
            return new List<TutorialLevelSpec>
            {
                new TutorialLevelSpec
                {
                    Order = 1,
                    LevelId = "tutorial_simple_01_hi",
                    AssetName = "TutorialSimple01Hi",
                    DifficultyScore = 45,
                    InitialMovableChains = 4,
                    ShapeTag = "SpecialShape",
                    MechanicTag = "CoreRule",
                    Authored = MakeAuthored(10, 7,
                        Arrow(0, (1, 5), (1, 4), (1, 3), (1, 2), (1, 1)),
                        Arrow(1, (4, 5), (4, 4), (4, 3), (4, 2), (4, 1)),
                        Arrow(2, (3, 3), (2, 3)),
                        Arrow(3, (8, 5), (7, 5), (6, 5)),
                        Arrow(4, (7, 4), (7, 3), (7, 2)),
                        Arrow(5, (8, 1), (7, 1), (6, 1)))
                },
                new TutorialLevelSpec
                {
                    Order = 2,
                    LevelId = "tutorial_simple_02_compact_gate",
                    AssetName = "TutorialSimple02CompactGate",
                    DifficultyScore = 85,
                    InitialMovableChains = 8,
                    ShapeTag = "Square",
                    MechanicTag = "ReadableMiniField",
                    Authored = MakeAuthored(9, 9,
                        Arrow(0, (8, 1), (7, 1), (6, 1), (5, 1), (4, 1), (3, 1), (2, 1), (1, 1)),
                        Arrow(1, (0, 2), (1, 2), (2, 2), (3, 2)),
                        Arrow(2, (8, 2), (7, 2), (6, 2), (5, 2)),
                        Arrow(3, (0, 3), (1, 3), (2, 3), (3, 3)),
                        Arrow(4, (8, 3), (7, 3), (6, 3), (5, 3)),
                        Arrow(5, (0, 4), (1, 4), (2, 4), (3, 4)),
                        Arrow(6, (8, 4), (7, 4), (6, 4), (5, 4)),
                        Arrow(7, (1, 8), (1, 7), (1, 6), (1, 5)),
                        Arrow(8, (3, 8), (3, 7), (3, 6), (3, 5)),
                        Arrow(9, (5, 8), (5, 7), (5, 6), (5, 5)),
                        Arrow(10, (7, 8), (7, 7), (7, 6), (7, 5)))
                }
            };
        }

        static AuthoredLevelData MakeAuthored(int width, int height, params AuthoredArrowData[] arrows)
        {
            var authored = new AuthoredLevelData
            {
                width = width,
                height = height,
                arrows = arrows.ToList(),
                blockIndices = new List<int>()
            };

            for (int a = 0; a < authored.arrows.Count; a++)
            {
                var indices = authored.arrows[a].indices;
                for (int i = 0; i < indices.Count; i++)
                {
                    Vector2Int pos = new Vector2Int(indices[i] % 64, indices[i] / 64);
                    indices[i] = AuthoredLevelBuilder.PosToIndex(pos, width);
                }
            }

            return authored;
        }

        static AuthoredArrowData Arrow(int colorIndex, params (int x, int y)[] headToTail)
        {
            if (headToTail == null || headToTail.Length < 2)
                throw new ArgumentException("Tutorial arrow needs at least two cells.", nameof(headToTail));

            int width = 64;
            return new AuthoredArrowData
            {
                colorIndex = colorIndex,
                indices = headToTail.Select(p => p.x + p.y * width).ToList()
            };
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

        static int CountArrowTiles(AuthoredLevelData authored)
        {
            return authored?.arrows?.Sum(a => a?.indices?.Count ?? 0) ?? 0;
        }

        static int MaxArrowLength(AuthoredLevelData authored)
        {
            return authored?.arrows?.Select(a => a?.indices?.Count ?? 0).DefaultIfEmpty(0).Max() ?? 0;
        }

        static string Get(Dictionary<string, string> record, string key)
        {
            return record != null && record.TryGetValue(key, out string value) ? value : "";
        }

        static string SourceKindForSlot(SlotKind kind) => kind switch
        {
            SlotKind.Shape => "shape",
            SlotKind.Hole => "hole",
            _ => "normal"
        };

        static Dictionary<int, SlotPlan> BuildSlotPlan()
        {
            var plan = Enumerable.Range(1, TargetCount)
                .ToDictionary(i => i, i => new SlotPlan
                {
                    Kind = SlotKind.Normal,
                    ExperienceRole = RoleForLevel(i),
                    TemplateId = TemplateForLevel(i).Id,
                    ContentSeries = "Auto",
                    UnlockTag = "None",
                    StoryBeat = "Auto"
                });

            var holeSlots = BuildHoleSlots();
            foreach (int slot in holeSlots)
            {
                plan[slot].Kind = SlotKind.Hole;
                plan[slot].ExperienceRole = slot <= 50 ? "TutorialVariant" : "MechanicSpice";
                plan[slot].ContentSeries = "HoleMechanic";
                plan[slot].StoryBeat = "HoleMechanic";
            }

            var shapeSlots = BuildShapeSlots(holeSlots);
            foreach (int slot in shapeSlots)
            {
                plan[slot].Kind = SlotKind.Shape;
                plan[slot].ContentSeries = "MainShape";
                if (plan[slot].ExperienceRole == "Recovery")
                    plan[slot].ExperienceRole = "VisualRecovery";
                else if (plan[slot].ExperienceRole != "Peak")
                    plan[slot].ExperienceRole = "VisualSpice";
                plan[slot].StoryBeat = plan[slot].ExperienceRole;
            }

            ApplyEarlyDirectorPlan(plan);
            return plan;
        }

        static void ApplyEarlyDirectorPlan(Dictionary<int, SlotPlan> plan)
        {
            OverrideSlot(plan, 1, SlotKind.Normal, "TutorialIntro", "TutorialSimple", "CoreRule", "HiHello", "Flow", 45f, TutorialSimpleSourcePool, 1);
            OverrideSlot(plan, 2, SlotKind.Hole, "RescueIntro", "Front20Rescue", "None", "RescueBabyDragon", "Flow", 115f, "hole_mask_early_front", 1);
            OverrideSlot(plan, 3, SlotKind.Normal, "Observe", "Front20Intro", "None", "FormerLevel1OpenField", "Flow", 90f, "normal_campaign500", 24, 20, 35);
            OverrideSlot(plan, 4, SlotKind.Shape, "ToolUnlock", "NewbieProp", "CrystalBall", "UnlockCrystalBall", "Flow", 105f, "shape_early_prop", 1);
            OverrideSlot(plan, 5, SlotKind.Normal, "Flow", "Front20Normal", "PiggyBank", "UnlockPiggyBankAfter", "Flow", 105f, "normal_campaign500", 58, 26, 30);
            OverrideSlot(plan, 6, SlotKind.Shape, "ToolUnlock", "NewbieProp", "Eraser", "UnlockEraser", "Flow", 115f, "shape_early_prop", 2);
            OverrideSlot(plan, 7, SlotKind.Normal, "Flow", "Front20Normal", "None", "NormalA", "Flow", 120f, "", 0, 40, 60);
            OverrideSlot(plan, 8, SlotKind.Normal, "Observe", "Front20Normal", "None", "NormalB", "Flow", 135f, "", 0, 45, 65);
            OverrideSlot(plan, 9, SlotKind.Shape, "ToolPreview", "NewbieProp", "Ruler", "PreviewRuler", "Flow", 135f, "shape_early_prop", 3);
            OverrideSlot(plan, 10, SlotKind.Normal, "Pressure", "Front20Normal", "None", "FirstSmallPressure", "Hard", 165f, "", 0, 60, 80);
            OverrideSlot(plan, 11, SlotKind.Normal, "Recovery", "Front20Normal", "None", "RecoverAfterSmallPressure", "Flow", 145f, "normal_campaign500", 104, 48, 55);
            OverrideSlot(plan, 12, SlotKind.Normal, "Observe", "Front20Normal", "None", "PostHardObserve", "Flow", 130f, "normal_campaign500", 63, 50, 55);
            OverrideSlot(plan, 13, SlotKind.Shape, "ToolUnlock", "NewbieProp", "MagicWand", "UnlockMagicWand", "Flow", 150f, "shape_early_prop", 4);
            OverrideSlot(plan, 14, SlotKind.Normal, "Recovery", "Front20Normal", "None", "RecoverAfterMagicWand", "Flow", 125f, "normal_campaign500", 83, 42, 50);
            OverrideSlot(plan, 15, SlotKind.Normal, "Observe", "Front20Normal", "None", "NormalC", "Flow", 140f, "normal_campaign500", 71, 50, 55);
            OverrideSlot(plan, 16, SlotKind.Normal, "Pressure", "Front20Normal", "None", "SecondSmallPressure", "Normal", 165f, "normal_campaign500", 85, 53, 58);
            OverrideSlot(plan, 17, SlotKind.Hole, "RescueIntro", "Front20Rescue", "LoginReward", "RescueProudCat", "Normal", 360f, "hole_mask_early_front", 3);
            OverrideSlot(plan, 18, SlotKind.Normal, "Observe", "Front20Normal", "FirstPurchase", "UnlockFirstPurchaseAfter", "Flow", 150f, "normal_campaign500", 13, 53, 58);
            OverrideSlot(plan, 19, SlotKind.Normal, "Peak", "Front20Peak", "BonusAfter", "FirstMajorPeakBeforeBonus", "VeryHard", 280f, "normal_campaign500", 127, 96, 105);
            OverrideSlot(plan, 20, SlotKind.Normal, "Recovery", "Front20Normal", "BattlePassStore", "UnlockBattlePassStoreAfter", "Flow", 110f, "normal_campaign500", 23, 41, 50);
            OverrideSlot(plan, 21, SlotKind.Normal, "Recovery", "Front30Normal", "None", "ReturnAfterFirstBonus", "Flow", 135f, "normal_campaign500", 77, 58, 65);
            OverrideSlot(plan, 22, SlotKind.Normal, "Observe", "Front30Normal", "None", "MazeObserveB", "Flow", 150f, "normal_campaign500", 116, 60, 68);
            OverrideSlot(plan, 23, SlotKind.Normal, "Normal", "Front30Normal", "None", "SectionNormalB", "Normal", 165f, "normal_campaign500", 90, 66, 72);
            OverrideSlot(plan, 24, SlotKind.Normal, "Pressure", "Front30Normal", "None", "SmallPressureA", "Normal", 180f, "normal_campaign500", 64, 71, 76);
            OverrideSlot(plan, 25, SlotKind.Normal, "Bottleneck", "Front30Normal", "None", "MidBlockHardA", "Hard", 245f, "normal_campaign500", 323, 89, 96);
            OverrideSlot(plan, 26, SlotKind.Normal, "Recovery", "Front30Normal", "None", "SecondRecoveryDip", "Flow", 150f, "normal_campaign500", 96, 70, 75);
            OverrideSlot(plan, 27, SlotKind.Normal, "Normal", "Front30Normal", "None", "SectionNormalC", "Normal", 200f, "normal_campaign500", 204, 78, 84);
            OverrideSlot(plan, 28, SlotKind.Normal, "Bottleneck", "Front30Normal", "None", "SmallBottleneckA", "Normal", 225f, "normal_campaign500", 185, 81, 88);
            OverrideSlot(plan, 29, SlotKind.Shape, "VisualSpice", "MainShape", "None", "FirstMainShapePreview", "Normal", 220f, "shape_final_with_supplement", 77, 80, 90);
            OverrideSlot(plan, 30, SlotKind.Normal, "Peak", "Front30Peak", "None", "SecondRampVeryHardPeak", "VeryHard", 295f, "normal_campaign500", 291, 110, 116);
            OverrideSlot(plan, 31, SlotKind.Normal, "Recovery", "Front50Normal", "None", "PostSecondPeakRecovery", "Flow", 185f, "normal_campaign500", 93, 75, 86);
            OverrideSlot(plan, 32, SlotKind.Shape, "VisualSpice", "MagicShape", "None", "MagicSeriesShield", "Normal", 205f, "shape_final_with_supplement", 2, 78, 90);
            OverrideSlot(plan, 33, SlotKind.Normal, "Observe", "Front50Normal", "None", "DenseObserveB", "Normal", 220f, "normal_campaign500", 103, 88, 98);
            OverrideSlot(plan, 34, SlotKind.Normal, "Bottleneck", "Front50Normal", "None", "MidBlockHardB", "Hard", 265f, "normal_campaign500", 193, 100, 110);
            OverrideSlot(plan, 35, SlotKind.Normal, "Recovery", "Front50Normal", "None", "PreOffbeatPeakRecoveryA", "Flow", 200f, "normal_campaign500", 34, 78, 90);
            OverrideSlot(plan, 36, SlotKind.Normal, "Peak", "Front50Peak", "None", "OffbeatVeryHardPeakA", "VeryHard", 315f, "normal_campaign500", 383, 118, 130);
            OverrideSlot(plan, 37, SlotKind.Hole, "MechanicSpice", "Front50Hole", "None", "HolePracticeAfterPeak", "Normal", 210f, "hole_mask_early_front", 6, 48, 55);
            OverrideSlot(plan, 38, SlotKind.Normal, "Recovery", "Front50Normal", "None", "PostOffbeatPeakRecoveryA", "Flow", 205f, "normal_campaign500", 67, 80, 92);
            OverrideSlot(plan, 39, SlotKind.Shape, "VisualSpice", "MagicShape", "None", "MagicSeriesBat", "Normal", 225f, "shape_final_with_supplement", 79, 82, 95);
            OverrideSlot(plan, 40, SlotKind.Normal, "Observe", "Front50Normal", "None", "ChapterBridgeA", "Normal", 235f, "normal_campaign500", 254, 96, 108);
            OverrideSlot(plan, 41, SlotKind.Normal, "Recovery", "Front50Normal", "None", "PostBridgeRecoveryA", "Flow", 210f, "normal_campaign500", 150, 80, 92);
            OverrideSlot(plan, 42, SlotKind.Shape, "VisualSpice", "MagicShape", "None", "MagicSeriesBlackCat", "Normal", 235f, "shape_final_with_supplement", 80, 82, 96);
            OverrideSlot(plan, 43, SlotKind.Normal, "Observe", "Front50Normal", "None", "MazeObserveC", "Normal", 240f, "normal_campaign500", 280, 96, 108);
            OverrideSlot(plan, 44, SlotKind.Normal, "Pressure", "Front50Normal", "None", "PressureC", "Normal", 255f, "normal_campaign500", 211, 96, 110);
            OverrideSlot(plan, 45, SlotKind.Normal, "Recovery", "Front50Normal", "None", "PreOffbeatPeakRecoveryB", "Flow", 215f, "normal_campaign500", 152, 82, 94);
            OverrideSlot(plan, 46, SlotKind.Normal, "Peak", "Front50Peak", "None", "OffbeatVeryHardPeakB", "VeryHard", 330f, "normal_campaign500", 411, 118, 132);
            OverrideSlot(plan, 47, SlotKind.Normal, "Recovery", "Front50Normal", "None", "PostOffbeatPeakRecoveryB", "Flow", 215f, "normal_campaign500", 27, 78, 92);
            OverrideSlot(plan, 48, SlotKind.Shape, "VisualSpice", "MagicShape", "None", "MagicSeriesKey", "Normal", 240f, "shape_final_with_supplement", 6, 82, 96);
            OverrideSlot(plan, 49, SlotKind.Normal, "Normal", "Front50Normal", "None", "MazeNormalC", "Normal", 255f, "normal_campaign500", 311, 100, 112);
            OverrideSlot(plan, 50, SlotKind.Normal, "Observe", "Front50Normal", "None", "ChapterBridgeB", "Normal", 250f, "normal_campaign500", 232, 100, 114);
        }

        static void OverrideSlot(
            Dictionary<int, SlotPlan> plan,
            int level,
            SlotKind kind,
            string role,
            string series,
            string unlock,
            string storyBeat,
            string targetDifficulty,
            float idealScore,
            string forcedSourcePool = "",
            int forcedSourceOrder = 0,
            int preferredChains = 0,
            int maxChains = 0)
        {
            SlotPlan slot = plan[level];
            slot.Kind = kind;
            slot.TemplateId = level <= 20 ? "F20-Director" : level <= 30 ? "F30-Director" : "F50-Director";
            slot.ExperienceRole = role;
            slot.ContentSeries = series;
            slot.UnlockTag = unlock;
            slot.StoryBeat = storyBeat;
            slot.ForcedTargetDifficulty = targetDifficulty;
            slot.ForcedIdealScore = idealScore;
            slot.ForcedSourcePool = forcedSourcePool;
            slot.ForcedSourceOrder = forcedSourceOrder;
            slot.ForcedPreferredChains = preferredChains;
            slot.ForcedMaxChains = maxChains;
        }

        static HashSet<int> BuildHoleSlots()
        {
            var result = new HashSet<int>();
            int[] gaps = { 20, 21, 19, 22, 20, 18, 23 };
            int slot = 37;
            int gapIndex = 0;
            int lateHoleTarget = Mathf.Max(0, TargetHoleCount - FrontTwentyForcedHoleCount);
            while (result.Count < lateHoleTarget && slot <= TargetCount)
            {
                while (slot <= TargetCount && (IsTrueExtremeSlot(slot) || IsTrueExtremeSlot(slot - 1) || IsTrueExtremeSlot(slot + 1)))
                    slot++;

                if (slot <= TargetCount)
                    result.Add(slot);

                slot += gaps[gapIndex % gaps.Length];
                gapIndex++;
            }

            return result;
        }

        static HashSet<int> BuildShapeSlots(HashSet<int> holeSlots)
        {
            var result = new HashSet<int>();
            int lateShapeTarget = Mathf.Max(0, TargetShapeCount - FrontTwentyForcedShapeCount);

            for (int block = 2; block < TargetCount / 10 && result.Count < lateShapeTarget; block++)
            {
                int start = block * 10 + 1;
                DecadeTemplate template = TemplateForBlock(block);
                foreach (int offset in template.ShapeOffsets)
                {
                    if (result.Count >= lateShapeTarget)
                        break;

                    int preferred = start + offset - 1;
                    int slot = FindShapeSlotInBlock(start, preferred, holeSlots, result);
                    result.Add(slot);
                }
            }

            return result;
        }

        static int FindShapeSlotInBlock(int blockStart, int preferred, HashSet<int> holeSlots, HashSet<int> existing)
        {
            int blockEnd = Mathf.Min(TargetCount, blockStart + 9);
            if (IsGoodShapeSlot(preferred, blockStart, blockEnd, holeSlots, existing))
                return preferred;

            int[] offsets = { -1, 1, -2, 2, -3, 3, -4, 4 };
            foreach (int offset in offsets)
            {
                int candidate = preferred + offset;
                if (IsGoodShapeSlot(candidate, blockStart, blockEnd, holeSlots, existing))
                    return candidate;
            }

            for (int slot = blockStart; slot <= blockEnd; slot++)
            {
                if (!holeSlots.Contains(slot) && !existing.Contains(slot))
                    return slot;
            }

            return preferred;
        }

        static bool IsGoodShapeSlot(int slot, int blockStart, int blockEnd, HashSet<int> holeSlots, HashSet<int> existing)
        {
            if (slot < blockStart || slot > blockEnd || existing.Contains(slot) || holeSlots.Contains(slot))
                return false;
            return !holeSlots.Contains(slot - 1) && !holeSlots.Contains(slot + 1);
        }

        static DecadeTemplate TemplateForLevel(int level)
        {
            return TemplateForBlock((level - 1) / 10);
        }

        static DecadeTemplate TemplateForBlock(int block)
        {
            DecadeTemplate[] templates =
            {
                new DecadeTemplate("A-Standard", new[] { 3, 7 }, new[]
                {
                    "Flow", "Observe", "VisualSpice", "Pressure", "Recovery",
                    "Core", "Variation", "Bottleneck", "Recovery", "Peak"
                }),
                new DecadeTemplate("B-Recovery", new[] { 4 }, new[]
                {
                    "Flow", "Observe", "Pressure", "VisualSpice", "Recovery",
                    "Core", "Bottleneck", "Normal", "Recovery", "Peak"
                }),
                new DecadeTemplate("C-Visual", new[] { 3, 9 }, new[]
                {
                    "Flow", "Observe", "VisualSpice", "Normal", "Pressure",
                    "Recovery", "Core", "Bottleneck", "VisualRecovery", "Peak"
                }),
                new DecadeTemplate("D-Pressure", new[] { 6 }, new[]
                {
                    "Flow", "Pressure", "Observe", "Bottleneck", "Recovery",
                    "VisualSpice", "Core", "Pressure", "Recovery", "Peak"
                }),
                new DecadeTemplate("E-Chapter", new[] { 2, 7 }, new[]
                {
                    "Flow", "VisualSpice", "Observe", "Pressure", "Recovery",
                    "Core", "VisualSpice", "Bottleneck", "Recovery", "Peak"
                })
            };

            return templates[block % templates.Length];
        }

        static string RoleForLevel(int level)
        {
            DecadeTemplate template = TemplateForLevel(level);
            int offset = (level - 1) % 10;
            return template.Roles[Mathf.Clamp(offset, 0, template.Roles.Length - 1)];
        }

        static bool IsTrueExtremeSlot(int level)
        {
            return level is 300 or 330 or 360 or 390 or 420 or 450 or 470 or 490 or 500;
        }

        static float IdealDifficultyScore(int level, SlotKind kind, string role)
        {
            if (level <= 20)
            {
                float[] front =
                {
                    110, 120, 125, 120, 135,
                    150, 160, 145, 170, 185,
                    195, 205, 165, 220, 235,
                    245, 260, 185, 270, 285
                };
                return front[level - 1];
            }

            float score;
            if (level <= 50)
                score = Mathf.Lerp(190f, 380f, (level - 21) / 29f);
            else if (level <= 150)
                score = Mathf.Lerp(280f, 540f, (level - 51) / 99f);
            else if (level <= 300)
                score = Mathf.Lerp(360f, 680f, (level - 151) / 149f);
            else if (level <= 420)
                score = Mathf.Lerp(440f, 810f, (level - 301) / 119f);
            else
                score = Mathf.Lerp(540f, 910f, (level - 421) / 79f);

            score += Mathf.Sin(level * 0.53f) * 34f;

            if (kind == SlotKind.Shape)
                score = Mathf.Min(score, 520f);
            else if (kind == SlotKind.Hole)
                score = Mathf.Min(Mathf.Max(score, 330f), 680f);

            if (role == "Peak")
                score += level <= 120 ? 55f : 85f;
            else if (role is "Recovery" or "BonusCandidate" or "VisualRecovery")
                score -= level <= 120 ? 65f : 95f;
            else if (role is "Pressure" or "Bottleneck")
                score += 35f;

            return score;
        }

        static string TargetDifficultyTag(int level, SlotKind kind, string role)
        {
            string tag = RoleTargetDifficulty(level, role);

            if (kind == SlotKind.Shape && DifficultyRank(tag) > DifficultyRank("Hard"))
                tag = level < 220 ? "Normal" : "Hard";
            if (kind == SlotKind.Hole)
            {
                if (tag == "Flow") tag = "Normal";
                if (tag == "Extreme") tag = "VeryHard";
            }

            return tag;
        }

        static string RoleTargetDifficulty(int level, string role)
        {
            if (level <= 20)
                return role == "Peak" ? "Normal" : role is "Pressure" or "Bottleneck" ? "Normal" : "Flow";

            if (role is "Flow" or "Recovery" or "BonusCandidate" or "VisualRecovery")
                return level <= 150 ? "Flow" : "Normal";

            if (role is "Observe" or "Normal" or "VisualSpice" or "Variation")
                return level <= 220 ? "Normal" : level <= 360 ? "Hard" : "Normal";

            if (role is "Pressure" or "Bottleneck" or "Core")
            {
                if (level <= 120) return "Normal";
                if (level <= 260) return "Hard";
                if (level <= 420) return level >= 340 && role == "Bottleneck" ? "VeryHard" : "Hard";
                return role == "Bottleneck" ? "VeryHard" : "Hard";
            }

            if (role == "Peak")
            {
                if (IsTrueExtremeSlot(level)) return "Extreme";
                if (level >= 100 && ((level / 10) % 2 == 0)) return "VeryHard";
                if (level <= 240) return "Hard";
                return "VeryHard";
            }

            return "Normal";
        }

        static string StageRelativeDifficultyTag(int level, Candidate candidate, string targetDifficulty, string role)
        {
            if (level <= 120)
                return EarlyStageRelativeDifficultyTag(level, candidate, targetDifficulty, role);

            float score = candidate.DifficultyScoreV1;
            float flowMax;
            float normalMax;
            float hardMax;
            float veryHardMax;

            if (level <= 50)
            {
                flowMax = 210f; normalMax = 340f; hardMax = 470f; veryHardMax = 580f;
            }
            else if (level <= 100)
            {
                flowMax = 240f; normalMax = 380f; hardMax = 520f; veryHardMax = 650f;
            }
            else if (level <= 200)
            {
                flowMax = 280f; normalMax = 450f; hardMax = 600f; veryHardMax = 730f;
            }
            else if (level <= 300)
            {
                flowMax = 330f; normalMax = 530f; hardMax = 700f; veryHardMax = 820f;
            }
            else if (level <= 400)
            {
                flowMax = 380f; normalMax = 600f; hardMax = 780f; veryHardMax = 900f;
            }
            else
            {
                flowMax = 420f; normalMax = 680f; hardMax = 840f; veryHardMax = 960f;
            }

            string relative = score <= flowMax ? "StageFlow" :
                score <= normalMax ? "StageNormal" :
                score <= hardMax ? "StageHard" :
                score <= veryHardMax ? "StageVeryHard" :
                "StageExtreme";

            if (role == "Peak" && DifficultyRank(targetDifficulty) >= DifficultyRank("VeryHard"))
                return relative + "|Peak";
            if (role == "BonusCandidate")
                return relative + "|BonusCandidate";
            return relative;
        }

        static string EarlyStageRelativeDifficultyTag(int level, Candidate candidate, string targetDifficulty, string role)
        {
            string relative;

            if (candidate.SourceKind == "hole")
            {
                relative = candidate.Chains >= 65 ? "StageHard" :
                    candidate.Chains >= 40 ? "StageNormal" :
                    "StageFlow";
                return relative + "|Mechanic";
            }

            int flowMax;
            int normalMax;
            int hardMax;

            if (level <= 10)
            {
                flowMax = 45; normalMax = 58; hardMax = 85;
            }
            else if (level <= 20)
            {
                flowMax = 50; normalMax = 65; hardMax = 90;
            }
            else if (level <= 30)
            {
                flowMax = 62; normalMax = 84; hardMax = 112;
            }
            else if (level <= 40)
            {
                flowMax = 78; normalMax = 112; hardMax = 125;
            }
            else if (level <= 60)
            {
                flowMax = 82; normalMax = 118; hardMax = 135;
            }
            else if (level <= 80)
            {
                flowMax = 90; normalMax = 125; hardMax = 145;
            }
            else
            {
                flowMax = 100; normalMax = 135; hardMax = 160;
            }

            if (candidate.SourceKind == "shape")
            {
                flowMax += 8;
                normalMax += 10;
                hardMax += 12;
            }

            relative = candidate.Chains <= flowMax ? "StageFlow" :
                candidate.Chains <= normalMax ? "StageNormal" :
                candidate.Chains <= hardMax ? "StageHard" :
                "StageVeryHard";

            if (role == "Peak")
            {
                string floor = DifficultyRank(targetDifficulty) >= DifficultyRank("VeryHard")
                    ? "StageVeryHard"
                    : "StageHard";
                relative = MaxStageRelative(relative, floor);
            }
            else if (role is "Pressure" or "Bottleneck" or "Core")
            {
                if (candidate.Chains >= normalMax)
                    relative = MaxStageRelative(relative, "StageHard");
            }

            if (DifficultyRank(targetDifficulty) >= DifficultyRank("VeryHard"))
                relative = MaxStageRelative(relative, "StageVeryHard");
            else if (DifficultyRank(targetDifficulty) >= DifficultyRank("Hard"))
                relative = MaxStageRelative(relative, "StageHard");

            if (role == "BonusCandidate")
                return relative + "|BonusCandidate";
            if (role == "Peak")
                return relative + "|Peak";
            return relative;
        }

        static string MaxStageRelative(string a, string b)
        {
            return StageRelativeRank(a) >= StageRelativeRank(b) ? a : b;
        }

        static int StageDifficultyScore(int level, Candidate candidate, string targetDifficulty, string role)
        {
            if (level > 120)
                return candidate.DifficultyScoreV1;

            float score;
            if (candidate.SourceKind == "hole")
            {
                score = candidate.Chains * 2.25f + 45f;
                if (candidate.Chains >= 40)
                    score += 18f;
                if (role is "RescueIntro" or "TutorialVariant" or "MechanicSpice")
                    score += 12f;
            }
            else if (candidate.SourceKind == "shape")
            {
                score = candidate.Chains * 2.25f + 42f;
                if (role is "VisualSpice" or "ToolUnlock" or "ToolPreview")
                    score += 10f;
            }
            else
            {
                score = candidate.Chains * 2.35f + candidate.DifficultyScoreV1 * 0.32f;
            }

            if (role is "Pressure" or "Bottleneck" or "Core")
                score += 24f;
            else if (role == "Peak")
                score += 42f;
            else if (role is "Recovery" or "VisualRecovery")
                score -= 12f;

            if (DifficultyRank(targetDifficulty) >= DifficultyRank("VeryHard"))
                score += 45f;
            else if (DifficultyRank(targetDifficulty) >= DifficultyRank("Hard"))
                score += 22f;

            if (level <= 10)
                score *= 0.88f;
            else if (level <= 30)
                score *= 0.96f;

            return Mathf.Max(0, Mathf.RoundToInt(score));
        }

        static int StageRelativeRank(string tag)
        {
            string baseTag = tag.Split('|')[0];
            return baseTag switch
            {
                "StageFlow" => 0,
                "StageNormal" => 1,
                "StageHard" => 2,
                "StageVeryHard" => 3,
                "StageExtreme" => 4,
                _ => -1
            };
        }

        static Candidate PickBestCandidate(
            IReadOnlyList<Candidate> pool,
            HashSet<string> used,
            IReadOnlyList<OrderedLevel> ordered,
            int level,
            SlotPlan slot,
            float idealScore,
            string targetDifficulty)
        {
            Candidate best = null;
            float bestScore = float.PositiveInfinity;
            SlotKind kind = slot.Kind;

            foreach (Candidate candidate in pool)
            {
                if (used.Contains(candidate.UniqueKey))
                    continue;
                if (!MatchesForcedSlot(candidate, slot, level))
                    continue;

                float penalty = Mathf.Abs(candidate.DifficultyScoreV1 - idealScore);
                penalty += DifficultyTagPenalty(candidate.DifficultyTagV1, targetDifficulty, level);
                penalty += RecentPenalty(candidate, ordered, kind);
                penalty += EarlyCampaignPenalty(candidate, slot, kind, level);

                if (candidate.DifficultyTagV1 == "Extreme" && targetDifficulty != "Extreme")
                    penalty += 10000f;
                if (targetDifficulty == "Extreme" && candidate.DifficultyTagV1 != "Extreme")
                    penalty += 520f;
                if (level >= 300 && candidate.DifficultyTagV1 == "Flow" && targetDifficulty != "Flow")
                {
                    penalty += kind == SlotKind.Shape || IsRecoveryRole(ordered.Count + 1)
                        ? 650f
                        : 4000f;
                }

                if (level <= 20)
                {
                    penalty += FrontTwentyPenalty(candidate, slot, targetDifficulty);
                }

                if (candidate.SourceKind == "normal" && level < 40 && candidate.DifficultyTagV1 is "Hard" or "VeryHard" or "Extreme")
                {
                    penalty += 300f;
                    if (slot.ExperienceRole == "Peak" && DifficultyRank(targetDifficulty) >= DifficultyRank(candidate.DifficultyTagV1))
                        penalty -= 280f;
                }
                if (candidate.SourceKind == "hole" && level < 21 && slot.ContentSeries != "Front20Rescue")
                    penalty += 1000f;
                if (candidate.SourceKind == "shape" && candidate.Theme == "Art" && level <= 20)
                    penalty += 35f;
                if (candidate.SourceKind == "shape" && candidate.SourcePool == "shape_early_prop" && slot.ContentSeries != "NewbieProp")
                    penalty += 10000f;
                if (candidate.SourcePool == TutorialSimpleSourcePool && slot.ContentSeries != "TutorialSimple")
                    penalty += 10000f;

                if (penalty < bestScore)
                {
                    bestScore = penalty;
                    best = candidate;
                }
            }

            if (best == null)
                throw new InvalidOperationException($"No candidate left for level {level} ({kind})");
            return best;
        }

        static bool MatchesForcedSlot(Candidate candidate, SlotPlan slot, int level)
        {
            if (!string.IsNullOrEmpty(slot.ForcedSourcePool) &&
                !string.Equals(candidate.SourcePool, slot.ForcedSourcePool, StringComparison.Ordinal))
                return false;

            if (slot.ForcedSourceOrder > 0 && candidate.SourceOrder != slot.ForcedSourceOrder)
                return false;

            if (candidate.SourceKind == "shape" && candidate.SourcePool == "shape_early_prop" && slot.ContentSeries != "NewbieProp")
                return false;

            if (candidate.SourcePool == TutorialSimpleSourcePool && slot.ContentSeries != "TutorialSimple")
                return false;

            if (candidate.SourceKind == "hole" && level > 20 && candidate.SourceOrder <= FrontTwentyForcedHoleCount)
                return false;

            return true;
        }

        static bool IsRecoveryRole(int level)
        {
            string role = RoleForLevel(level);
            return role is "Recovery" or "BonusCandidate" or "VisualRecovery" or "Flow";
        }

        static float DifficultyTagPenalty(string candidateTag, string targetTag, int level)
        {
            int candidateRank = DifficultyRank(candidateTag);
            int targetRank = DifficultyRank(targetTag);
            if (candidateRank < 0 || targetRank < 0)
                return 0f;

            int diff = Mathf.Abs(candidateRank - targetRank);
            float penalty = diff * 190f;

            if (candidateRank > targetRank)
                penalty += diff * (level < 160 ? 120f : 55f);
            else if (level >= 260)
                penalty += diff * 45f;

            return penalty;
        }

        static int DifficultyRank(string tag) => tag switch
        {
            "Flow" => 0,
            "Normal" => 1,
            "Hard" => 2,
            "VeryHard" => 3,
            "Extreme" => 4,
            _ => -1
        };

        static float RecentPenalty(Candidate candidate, IReadOnlyList<OrderedLevel> ordered, SlotKind kind)
        {
            float penalty = 0f;
            foreach (OrderedLevel recent in ordered.Skip(Mathf.Max(0, ordered.Count - 30)))
            {
                int gap = ordered.Count + 1 - recent.CampaignOrder;
                Candidate prev = recent.Candidate;

                if (candidate.SourceKind == "shape" && prev.SourceKind == "shape" && candidate.Theme == prev.Theme)
                    penalty += gap <= 10 ? 240f : 85f;

                if (candidate.SourceKind == "normal" && prev.SourceKind == "normal" && candidate.ExistingType == prev.ExistingType)
                    penalty += gap <= 2 ? 90f : gap <= 6 ? 25f : 0f;

                if (candidate.DifficultyTagV1 is "VeryHard" or "Extreme" &&
                    prev.DifficultyTagV1 is "VeryHard" or "Extreme" &&
                    gap <= 3)
                    penalty += 180f;

                if (candidate.PressureTag == "HighPressure" && prev.PressureTag == "HighPressure" && gap <= 3)
                    penalty += 140f;

                if (kind == SlotKind.Shape && prev.SourceKind == "shape" && gap <= 2)
                    penalty += 200f;

                if (kind == SlotKind.Hole && prev.SourceKind == "hole" && gap <= 18)
                    penalty += 300f;
            }

            return penalty;
        }

        static float EarlyCampaignPenalty(Candidate candidate, SlotPlan slot, SlotKind kind, int level)
        {
            if (level <= 20)
                return 0f;

            float penalty = 0f;

            if (slot.ForcedPreferredChains > 0)
                penalty += Mathf.Abs(candidate.Chains - slot.ForcedPreferredChains) * 7.5f;
            if (slot.ForcedMaxChains > 0 && candidate.Chains > slot.ForcedMaxChains)
                penalty += 5000f + (candidate.Chains - slot.ForcedMaxChains) * 120f;

            int softMin = SoftMinChainsForLevel(level, kind, slot.ExperienceRole);
            if (softMin > 0 && candidate.Chains < softMin)
                penalty += (softMin - candidate.Chains) * (level <= 80 ? 12f : 7f);

            if (kind == SlotKind.Shape && level > 20 && level <= 120)
            {
                if (candidate.Theme == "Magic")
                    penalty -= 180f;
                else
                    penalty += level <= 60 ? 90f : 35f;
            }

            return penalty;
        }

        static int SoftMinChainsForLevel(int level, SlotKind kind, string role)
        {
            if (level <= 20)
                return 0;
            if (kind == SlotKind.Hole)
                return level <= 60 ? 45 : 60;
            if (kind == SlotKind.Shape)
                return level <= 60 ? 75 : 90;
            if (level <= 30)
                return role is "Recovery" or "Flow" ? 50 : 55;
            if (level <= 50)
                return role is "Recovery" or "Flow" or "VisualRecovery" ? 55 : 70;
            if (level <= 100)
                return role is "Recovery" or "Flow" or "VisualRecovery" ? 60 : 85;
            if (level <= 200)
                return role is "Recovery" or "Flow" or "VisualRecovery" ? 80 : 110;
            return role is "Recovery" or "Flow" or "VisualRecovery" ? 100 : 130;
        }

        static float FrontTwentyPenalty(Candidate candidate, SlotPlan slot, string targetDifficulty)
        {
            float penalty = 0f;
            int candidateRank = DifficultyRank(candidate.DifficultyTagV1);
            int targetRank = DifficultyRank(targetDifficulty);

            if (candidate.DifficultyTagV1 is "Hard" or "VeryHard" or "Extreme")
            {
                penalty += 300f;
                if (slot.ExperienceRole == "Peak" && candidateRank <= targetRank)
                    penalty -= 300f;
            }
            if (candidate.Chains > 95)
                penalty += (candidate.Chains - 95) * 2.5f;
            if (slot.ExperienceRole == "Peak" && targetRank >= DifficultyRank("Hard"))
            {
                penalty -= Mathf.Min(140f, Mathf.Max(0, candidate.Chains - 90) * 2f);
                if (candidate.Chains > 180)
                    penalty += (candidate.Chains - 180) * 4f;
            }
            if (slot.ForcedPreferredChains > 0)
                penalty += Mathf.Abs(candidate.Chains - slot.ForcedPreferredChains) * 7.5f;
            if (slot.ForcedMaxChains > 0 && candidate.Chains > slot.ForcedMaxChains)
                penalty += 5000f + (candidate.Chains - slot.ForcedMaxChains) * 120f;
            if (candidate.VisualTag == "VisualDense")
                penalty += 60f;
            if (candidate.StartTag == "StartNarrow")
                penalty += 80f;
            if (slot.Kind == SlotKind.Shape && candidate.DifficultyTagV1 != "Flow")
                penalty += 45f;
            return penalty;
        }

        static string BuildReason(int level, SlotKind kind, string templateId, string experienceRole, float idealScore, string targetDifficulty, Candidate selected)
        {
            var tags = new List<string>
            {
                $"ideal={idealScore.ToString("0", Inv)}",
                $"target={targetDifficulty}",
                $"kind={kind}",
                $"template={templateId}",
                $"role={experienceRole}"
            };
            if (level <= 20)
                tags.Add("front20");
            if (selected.SourcePool == "shape_early_prop")
                tags.Add("newbie-shape-series");
            if (selected.SourcePool == TutorialSimpleSourcePool)
                tags.Add("tutorial-simple");
            if (selected.SourceKind == "shape")
                tags.Add($"theme={selected.Theme}");
            if (selected.SourceKind == "hole")
                tags.Add("hole-spacing");
            tags.Add($"scoreDiff={Mathf.Abs(selected.DifficultyScoreV1 - idealScore).ToString("0", Inv)}");
            return string.Join("|", tags);
        }

        static void WriteOrder(string path, IReadOnlyList<OrderedLevel> ordered)
        {
            string[] headers =
            {
                "campaignOrder", "slotKind", "templateId", "experienceRole", "contentSeries", "unlockTag", "storyBeat",
                "idealScore", "targetDifficultyTag", "stageRelativeDifficultyTag", "stageDifficultyScore",
                "sourcePool", "sourceKind", "sourceOrder", "levelId",
                "assetName", "difficultyTagV1", "difficultyScoreV1", "pressureTag", "paceTag", "startTag",
                "clearTag", "choiceTag", "shapeTag", "visualTag", "noveltyTag", "mechanicTag", "riskTags",
                "reasonTags", "theme", "width", "height", "chains", "assetPath", "selectionReason"
            };

            var lines = new List<string>(ordered.Count + 1) { string.Join(",", headers) };
            foreach (OrderedLevel item in ordered)
            {
                Candidate c = item.Candidate;
                var values = new[]
                {
                    item.CampaignOrder.ToString(Inv),
                    item.SlotKind,
                    item.TemplateId,
                    item.ExperienceRole,
                    item.ContentSeries,
                    item.UnlockTag,
                    item.StoryBeat,
                    item.IdealScore.ToString("0.###", Inv),
                    item.TargetDifficultyTag,
                    item.StageRelativeDifficultyTag,
                    StageDifficultyScore(item.CampaignOrder, c, item.TargetDifficultyTag, item.ExperienceRole).ToString(Inv),
                    c.SourcePool,
                    c.SourceKind,
                    c.SourceOrder.ToString(Inv),
                    c.LevelId,
                    c.AssetName,
                    c.DifficultyTagV1,
                    c.DifficultyScoreV1.ToString(Inv),
                    c.PressureTag,
                    c.PaceTag,
                    c.StartTag,
                    c.ClearTag,
                    c.ChoiceTag,
                    c.ShapeTag,
                    c.VisualTag,
                    c.NoveltyTag,
                    c.MechanicTag,
                    c.RiskTags,
                    c.ReasonTags,
                    c.Theme,
                    c.Width.ToString(Inv),
                    c.Height.ToString(Inv),
                    c.Chains.ToString(Inv),
                    c.AssetPath,
                    item.SelectionReason
                };
                lines.Add(string.Join(",", values.Select(EscapeCsv)));
            }

            WriteAllLinesWithRetry(path, lines);
        }

        static IReadOnlyList<BonusSideEvent> BuildBonusSideEvents()
        {
            return new[]
            {
                new BonusSideEvent
                {
                    InsertAfterCampaignOrder = 19,
                    BonusOrder = 1,
                    BonusId = "bonus_001_after_first_veryhard",
                    ExperienceRole = "BonusReward",
                    TriggerReason = "AfterFirstVeryHard",
                    TargetDifficultyTag = "Flow",
                    Notes = "Side-mode reward level; does not consume campaignOrder."
                }
            };
        }

        static void WriteBonusEvents(string path, IReadOnlyList<BonusSideEvent> events)
        {
            string[] headers =
            {
                "insertAfterCampaignOrder", "bonusOrder", "bonusId", "experienceRole", "triggerReason", "targetDifficultyTag", "notes"
            };

            var lines = new List<string>(events.Count + 1) { string.Join(",", headers) };
            foreach (BonusSideEvent item in events)
            {
                var values = new[]
                {
                    item.InsertAfterCampaignOrder.ToString(Inv),
                    item.BonusOrder.ToString(Inv),
                    item.BonusId,
                    item.ExperienceRole,
                    item.TriggerReason,
                    item.TargetDifficultyTag,
                    item.Notes
                };
                lines.Add(string.Join(",", values.Select(EscapeCsv)));
            }

            WriteAllLinesWithRetry(path, lines);
        }

        static void WriteSummary(
            string path,
            IReadOnlyList<OrderedLevel> ordered,
            IReadOnlyList<Candidate> all,
            IReadOnlyList<Candidate> usable,
            IReadOnlyList<BonusSideEvent> bonusEvents)
        {
            var lines = new List<string>
            {
                "group,count,details"
            };

            lines.Add($"total,{ordered.Count},target={TargetCount}");
            lines.Add($"bonusSideEvents,{bonusEvents.Count},doesNotConsumeCampaignOrder=true");
            lines.Add($"usablePool,{usable.Count},all={all.Count}");
            AddGroup(lines, "sourceKind", ordered.GroupBy(o => o.Candidate.SourceKind));
            AddGroup(lines, "slotKind", ordered.GroupBy(o => o.SlotKind));
            AddGroup(lines, "templateId", ordered.GroupBy(o => o.TemplateId));
            AddGroup(lines, "experienceRole", ordered.GroupBy(o => o.ExperienceRole));
            AddGroup(lines, "contentSeries", ordered.GroupBy(o => o.ContentSeries));
            AddGroup(lines, "unlockTag", ordered.Where(o => o.UnlockTag != "None").GroupBy(o => o.UnlockTag));
            AddGroup(lines, "difficultyTagV1", ordered.GroupBy(o => o.Candidate.DifficultyTagV1));
            AddGroup(lines, "targetDifficultyTag", ordered.GroupBy(o => o.TargetDifficultyTag));
            AddGroup(lines, "stageRelativeDifficultyTag", ordered.GroupBy(o => o.StageRelativeDifficultyTag));
            AddGroup(lines, "pressureTag", ordered.GroupBy(o => o.Candidate.PressureTag));
            AddGroup(lines, "shapeTheme", ordered.Where(o => o.Candidate.SourceKind == "shape").GroupBy(o => o.Candidate.Theme));
            AddGroup(lines, "first20SourceKind", ordered.Take(20).GroupBy(o => o.Candidate.SourceKind));
            AddGroup(lines, "first20Difficulty", ordered.Take(20).GroupBy(o => o.Candidate.DifficultyTagV1));
            AddGroup(lines, "first20Risk", ordered.Take(20).GroupBy(o => o.Candidate.RiskTags));
            var front20 = ordered.Take(20).ToList();
            lines.Add($"first20Chains,,min={front20.Min(o => o.Candidate.Chains)} avg={front20.Average(o => o.Candidate.Chains):0.0} max={front20.Max(o => o.Candidate.Chains)} peakMax={front20.Where(o => o.ExperienceRole == "Peak").Max(o => o.Candidate.Chains)}");

            for (int start = 1; start <= TargetCount; start += 10)
            {
                var block = ordered.Where(o => o.CampaignOrder >= start && o.CampaignOrder < start + 10).ToList();
                int shapes = block.Count(o => o.Candidate.SourceKind == "shape");
                int holes = block.Count(o => o.Candidate.SourceKind == "hole");
                int peaks = block.Count(o => o.ExperienceRole == "Peak");
                int bonus = bonusEvents.Count(e => e.InsertAfterCampaignOrder >= start && e.InsertAfterCampaignOrder < start + 10);
                string template = block.Count > 0 ? block[0].TemplateId : "";
                lines.Add($"levels_{start:000}_{start + 9:000},{block.Count},template={template} shape={shapes} hole={holes} peak={peaks} sideBonusAfter={bonus}");
            }

            WriteAllLinesWithRetry(path, lines);
        }

        static void AddGroup(List<string> lines, string label, IEnumerable<IGrouping<string, OrderedLevel>> groups)
        {
            string details = string.Join(" ",
                groups.OrderByDescending(g => g.Count()).ThenBy(g => g.Key)
                    .Select(g => $"{g.Key}:{g.Count()}"));
            lines.Add($"{EscapeCsv(label)},,{EscapeCsv(details)}");
        }

        static void WriteNotes(
            string path,
            IReadOnlyList<OrderedLevel> ordered,
            IReadOnlyList<Candidate> all,
            IReadOnlyList<Candidate> usable,
            IReadOnlyList<BonusSideEvent> bonusEvents)
        {
            int shapes = ordered.Count(o => o.Candidate.SourceKind == "shape");
            int holes = ordered.Count(o => o.Candidate.SourceKind == "hole");
            int normals = ordered.Count(o => o.Candidate.SourceKind == "normal");

            var sb = new StringBuilder();
            sb.AppendLine("# Ordered Campaign Import V1 Notes");
            sb.AppendLine();
            sb.AppendLine("This is a first-pass ordered CSV for review, not a final imported LevelPack.");
            sb.AppendLine();
            sb.AppendLine("## Mix");
            sb.AppendLine();
            sb.AppendLine($"- Total: {ordered.Count}");
            sb.AppendLine($"- Normal: {normals}");
            sb.AppendLine($"- Shape: {shapes} ({shapes / (float)ordered.Count:0.0%})");
            sb.AppendLine($"- Hole: {holes} ({holes / (float)ordered.Count:0.0%})");
            sb.AppendLine($"- Bonus side events: {bonusEvents.Count} (do not consume campaign order)");
            sb.AppendLine($"- Usable no-risk pool: {usable.Count}/{all.Count}");
            sb.AppendLine();
            sb.AppendLine("## Rules");
            sb.AppendLine();
            sb.AppendLine("- First 20 levels are director-authored with fixed tutorial, rescue, prop unlock, pressure, and recovery beats.");
            sb.AppendLine("- First-20 chain scale is intentionally small: normal slots prefer roughly 50 chains, early peaks are stage-relative, and the level-19 peak is capped around 120 chains.");
            sb.AppendLine("- Beginner prop Shape levels must come from `shape_early_prop` and are not reused in later Shape slots.");
            sb.AppendLine("- The first two Hole candidates are reserved for the front-20 rescue beats in this preview; these explicit onboarding slots may bypass the global `NoRisk` filter if they have no build error.");
            sb.AppendLine("- Bonus levels are written to a separate side-event CSV and do not consume the 500 main campaign slots.");
            sb.AppendLine("- Ten-level blocks rotate through five templates instead of using the same rhythm every time.");
            sb.AppendLine("- Shape slots average about 1-2 per ten levels, capped by template placement.");
            sb.AppendLine("- After the front-20 rescues, Hole slots use irregular gaps around twenty levels.");
            sb.AppendLine("- `targetDifficultyTag` records the intended stage difficulty before candidate matching.");
            sb.AppendLine("- `stageRelativeDifficultyTag` records how hard the selected level feels inside its campaign stage.");
            sb.AppendLine("- `experienceRole` records the slot purpose: Flow, Observe, Pressure, Recovery, VisualSpice, RescueIntro, Peak, etc.");
            sb.AppendLine("- `contentSeries`, `unlockTag`, and `storyBeat` describe authored onboarding beats.");
            sb.AppendLine("- `riskTags != NoRisk` candidates are excluded from this preview.");
            sb.AppendLine("- Recent same-theme Shape, repeated normal structure, adjacent high-pressure levels, and adjacent specials are penalized.");
            sb.AppendLine();
            sb.AppendLine("## Review Focus");
            sb.AppendLine();
            sb.AppendLine("- Manually inspect levels 1-20 first.");
            sb.AppendLine("- Check whether Shape frequency feels delightful or too loud.");
            sb.AppendLine("- Check Hole placement around first introduction and after difficulty spikes.");
            sb.AppendLine("- Check late-stage non-Extreme slots: if Flow appears in Pressure/Bottleneck/Core slots, the pool needs more late Normal/Hard/VeryHard or real Bonus levels.");
            sb.AppendLine("- Use this CSV to build the first actual preview LevelPack only after the ordering feels right.");
            WriteAllTextWithRetry(path, sb.ToString());
        }

        static void WriteAllLinesWithRetry(string path, IReadOnlyList<string> lines)
        {
            IOException last = null;
            for (int attempt = 0; attempt < 8; attempt++)
            {
                try
                {
                    File.WriteAllLines(path, lines, new UTF8Encoding(false));
                    return;
                }
                catch (IOException ex)
                {
                    last = ex;
                    System.Threading.Thread.Sleep(150 + attempt * 100);
                }
            }

            throw last ?? new IOException($"Failed to write {path}");
        }

        static void WriteAllTextWithRetry(string path, string text)
        {
            IOException last = null;
            for (int attempt = 0; attempt < 8; attempt++)
            {
                try
                {
                    File.WriteAllText(path, text, new UTF8Encoding(false));
                    return;
                }
                catch (IOException ex)
                {
                    last = ex;
                    System.Threading.Thread.Sleep(150 + attempt * 100);
                }
            }

            throw last ?? new IOException($"Failed to write {path}");
        }

        static string ToAbsolutePath(string projectRoot, string assetPath)
        {
            return Path.Combine(projectRoot, assetPath.Replace('/', Path.DirectorySeparatorChar));
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

        enum SlotKind
        {
            Normal,
            Shape,
            Hole
        }

        sealed class SlotPlan
        {
            public SlotKind Kind;
            public string TemplateId;
            public string ExperienceRole;
            public string ContentSeries;
            public string UnlockTag;
            public string StoryBeat;
            public string ForcedSourcePool;
            public int ForcedSourceOrder;
            public int ForcedPreferredChains;
            public int ForcedMaxChains;
            public float? ForcedIdealScore;
            public string ForcedTargetDifficulty;
        }

        sealed class DecadeTemplate
        {
            public readonly string Id;
            public readonly int[] ShapeOffsets;
            public readonly string[] Roles;

            public DecadeTemplate(string id, int[] shapeOffsets, string[] roles)
            {
                Id = id;
                ShapeOffsets = shapeOffsets;
                Roles = roles;
            }
        }

        sealed class OrderedLevel
        {
            public int CampaignOrder;
            public string SlotKind;
            public string TemplateId;
            public string ExperienceRole;
            public string ContentSeries;
            public string UnlockTag;
            public string StoryBeat;
            public float IdealScore;
            public string TargetDifficultyTag;
            public string StageRelativeDifficultyTag;
            public Candidate Candidate;
            public string SelectionReason;
        }

        sealed class BonusSideEvent
        {
            public int InsertAfterCampaignOrder;
            public int BonusOrder;
            public string BonusId;
            public string ExperienceRole;
            public string TriggerReason;
            public string TargetDifficultyTag;
            public string Notes;
        }

        sealed class TutorialLevelSpec
        {
            public int Order;
            public string LevelId;
            public string AssetName;
            public int DifficultyScore;
            public int InitialMovableChains;
            public string ShapeTag;
            public string MechanicTag;
            public AuthoredLevelData Authored;
        }

        sealed class Candidate
        {
            public string SourcePool;
            public string SourceKind;
            public int SourceOrder;
            public string LevelId;
            public string AssetName;
            public string ExistingType;
            public int DifficultyScoreV1;
            public string DifficultyTagV1;
            public string PressureTag;
            public string PaceTag;
            public string StartTag;
            public string ClearTag;
            public string ChoiceTag;
            public string ShapeTag;
            public string VisualTag;
            public string NoveltyTag;
            public string MechanicTag;
            public string RiskTags;
            public string ReasonTags;
            public string Theme;
            public int Width;
            public int Height;
            public int Chains;
            public string AssetPath;
            public string BuildError;

            public string UniqueKey => $"{SourcePool}/{AssetPath}";

            public static Candidate FromRecord(Dictionary<string, string> record)
            {
                return new Candidate
                {
                    SourcePool = Get(record, "sourcePool"),
                    SourceKind = Get(record, "sourceKind"),
                    SourceOrder = ParseInt(Get(record, "order")),
                    LevelId = Get(record, "levelId"),
                    AssetName = Get(record, "assetName"),
                    ExistingType = Get(record, "existingType"),
                    DifficultyScoreV1 = ParseInt(Get(record, "difficultyScoreV1")),
                    DifficultyTagV1 = Get(record, "difficultyTagV1"),
                    PressureTag = Get(record, "pressureTag"),
                    PaceTag = Get(record, "paceTag"),
                    StartTag = Get(record, "startTag"),
                    ClearTag = Get(record, "clearTag"),
                    ChoiceTag = Get(record, "choiceTag"),
                    ShapeTag = Get(record, "shapeTag"),
                    VisualTag = Get(record, "visualTag"),
                    NoveltyTag = Get(record, "noveltyTag"),
                    MechanicTag = Get(record, "mechanicTag"),
                    RiskTags = Get(record, "riskTags"),
                    ReasonTags = Get(record, "reasonTags"),
                    Theme = Get(record, "theme"),
                    Width = ParseInt(Get(record, "width")),
                    Height = ParseInt(Get(record, "height")),
                    Chains = ParseInt(Get(record, "chains")),
                    AssetPath = Get(record, "assetPath"),
                    BuildError = Get(record, "buildError")
                };
            }

            static string Get(Dictionary<string, string> record, string key)
            {
                return record.TryGetValue(key, out string value) ? value : "";
            }
        }
    }
}
#endif
