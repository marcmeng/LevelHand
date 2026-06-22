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
    public static class CampaignSingleLevelValidator
    {
        const string PackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500PlanPreviewPack.asset";
        const string SelectionPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/campaign500_plan_selection.csv";
        const string RhythmPlanPath = "Assets/ArrowMagic/SOData/Reports/Campaign500/campaign500_21_500_relative_peaks_plan.csv";
        const string ReportFolder = "Assets/ArrowMagic/SOData/Reports/Campaign500";
        const string SummaryPath = ReportFolder + "/campaign500_single_level_validation_summary.csv";
        const string FlaggedPath = ReportFolder + "/campaign500_single_level_validation_flags.csv";
        const string NotesPath = ReportFolder + "/campaign500_single_level_validation_notes.md";
        const string ReviewPackPath = "Assets/ArrowMagic/SOData/Packs/Campaign500/Campaign500SingleLevelReviewPack.asset";
        const string DemoScenePath = "Assets/ArrowMagic/Scenes/Demo.unity";
        const string Front20SummaryPath = ReportFolder + "/campaign500_front20_finalized_summary.csv";
        const string Front20ReplacementPath = ReportFolder + "/campaign500_front20_replacements.csv";
        const string Level3ReplacementPath = "Assets/ArrowMagic/SOData/Levels/LevelImportV1/TutorialSimple/tutorial_simple_02_compact_gate.asset";
        const string Level5ReplacementPath = "Assets/ArrowMagic/SOData/Levels/Generated/R2OuterStrongRepair/r2_outer_strong_166_r2_295_a003.asset";

        static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

        [MenuItem("Tools/Arrow Magic/Campaign 500/Run Single-Level Validation")]
        public static void RunCampaign500Validation()
        {
            RunValidationForPack(
                PackPath,
                SelectionPath,
                RhythmPlanPath,
                ReportFolder,
                SummaryPath,
                FlaggedPath,
                NotesPath,
                ReviewPackPath,
                "CampaignSingleLevelValidator");
        }

        public static IReadOnlyList<string> RunValidationForPack(
            string packPath,
            string selectionPath,
            string rhythmPlanPath,
            string reportFolder,
            string summaryPath,
            string flaggedPath,
            string notesPath,
            string reviewPackPath,
            string logTag)
        {
            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(packPath);
            if (pack == null || pack.levels == null || pack.levels.Length == 0)
            {
                Debug.LogError($"[{logTag}] Missing or empty pack: {packPath}");
                return Array.Empty<string>();
            }

            var selection = ReadCsvByOrder(selectionPath);
            var rhythm = ReadCsvByOrder(rhythmPlanPath);
            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var rows = new List<ValidationRow>(pack.levels.Length);
            try
            {
                for (int i = 0; i < pack.levels.Length; i++)
                {
                    if ((i % 20) == 0)
                    {
                        EditorUtility.DisplayProgressBar(
                            "Campaign Single-Level Validation",
                            $"{i + 1}/{pack.levels.Length}",
                            pack.levels.Length > 0 ? i / (float)pack.levels.Length : 0f);
                    }

                    int order = i + 1;
                    selection.TryGetValue(order, out var selectionMeta);
                    rhythm.TryGetValue(order, out var rhythmMeta);
                    rows.Add(AnalyzeLevel(order, pack.levels[i], rules, selectionMeta, rhythmMeta));
                }
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }

            Directory.CreateDirectory(ToAbsolutePath(reportFolder));
            WriteSummary(summaryPath, rows);
            WriteFlagged(flaggedPath, rows);
            WriteNotes(notesPath, rows, pack, packPath, reviewPackPath);
            SaveReviewPack(rows, reviewPackPath);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            int green = rows.Count(r => r.Status == "Green");
            int yellow = rows.Count(r => r.Status == "Yellow");
            int red = rows.Count(r => r.Status == "Red");
            Debug.Log($"[{logTag}] Done. Green={green}, Yellow={yellow}, Red={red}, summary={summaryPath}, flags={flaggedPath}");
            return new[] { summaryPath, flaggedPath, notesPath };
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Attach Single-Level Review Pack To Demo")]
        public static void AttachReviewPackToDemo()
        {
            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(ReviewPackPath);
            if (pack == null || pack.levels == null || pack.levels.Length == 0)
            {
                Debug.LogError($"[CampaignSingleLevelValidator] Missing or empty review pack: {ReviewPackPath}");
                return;
            }

            AttachPackToDemo(pack, "CampaignSingleLevelReview");
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Patch Front 3 And 5, Validate, Attach Review")]
        public static void PatchFront3And5ValidateAndAttachReview()
        {
            PatchFront3And5();
            RunCampaign500Validation();
            AttachReviewPackToDemo();
        }

        [MenuItem("Tools/Arrow Magic/Campaign 500/Patch Front 3 And 5")]
        public static void PatchFront3And5()
        {
            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(PackPath);
            if (pack == null || pack.levels == null || pack.levels.Length < 5)
            {
                Debug.LogError($"[CampaignSingleLevelValidator] Missing or short pack: {PackPath}");
                return;
            }

            var level3 = AssetDatabase.LoadAssetAtPath<LevelDefinition>(Level3ReplacementPath);
            var level5 = AssetDatabase.LoadAssetAtPath<LevelDefinition>(Level5ReplacementPath);
            if (level3 == null)
            {
                Debug.LogError($"[CampaignSingleLevelValidator] Missing level 3 replacement: {Level3ReplacementPath}");
                return;
            }
            if (level5 == null)
            {
                Debug.LogError($"[CampaignSingleLevelValidator] Missing level 5 replacement: {Level5ReplacementPath}");
                return;
            }

            string old3 = pack.levels[2] != null ? pack.levels[2].levelId : "<null>";
            string old5 = pack.levels[4] != null ? pack.levels[4].levelId : "<null>";
            pack.levels[2] = level3;
            pack.levels[4] = level5;
            EditorUtility.SetDirty(pack);

            UpdateFrontPatchCsvs(level3, level5);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"[CampaignSingleLevelValidator] Patched L3 {old3} -> {level3.levelId}; L5 {old5} -> {level5.levelId}");
        }

        static ValidationRow AnalyzeLevel(
            int order,
            LevelDefinition level,
            IRuleset rules,
            Dictionary<string, string> selectionMeta,
            Dictionary<string, string> rhythmMeta)
        {
            var row = new ValidationRow
            {
                Order = order,
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
                row.BuildError = "null level reference";
                return row;
            }

            var authored = level.authoredLevel;
            row.Width = Mathf.Max(1, authored?.width ?? 0);
            row.Height = Mathf.Max(1, authored?.height ?? 0);
            row.Chains = authored?.arrows?.Count ?? 0;
            row.ArrowTiles = CountArrowTiles(authored);
            row.BlockTiles = authored?.blockIndices?.Count ?? 0;
            row.MaxChainLength = MaxChainLength(authored);
            row.BoardFill = row.ArrowTiles / Mathf.Max(1f, row.Width * row.Height);
            row.PlayableFill = row.ArrowTiles / Mathf.Max(1f, row.Width * row.Height - row.BlockTiles);

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out string buildError))
            {
                row.Status = "Red";
                row.Flags = "BuildError";
                row.BuildError = buildError;
                return row;
            }

            TraceSummary balanced = Simulate(board, rules, PlayerPolicy.Balanced);
            TraceSummary flow = Simulate(board, rules, PlayerPolicy.Flow);
            TraceSummary planner = Simulate(board, rules, PlayerPolicy.Planner);

            row.GreedySolved = balanced.Solved;
            row.GreedySteps = balanced.StepCount;
            row.FlowSolved = flow.Solved;
            row.FlowSteps = flow.StepCount;
            row.PlannerSolved = planner.Solved;
            row.PlannerSteps = planner.StepCount;

            row.OpeningMoves = balanced.OpeningMoves;
            row.OpeningGoodMoves = balanced.OpeningGoodMoves;
            row.OpeningGoodMoveRatio = balanced.OpeningGoodMoveRatio;
            row.OpeningBestClear = balanced.OpeningBestClear;
            row.AvgAvailableMoves = balanced.AvgAvailableMoves;
            row.AvgGoodMoves = balanced.AvgGoodMoves;
            row.AvgGoodMoveRatio = balanced.AvgGoodMoveRatio;
            row.BottleneckStepRatio = balanced.BottleneckStepRatio;
            row.HardBottleneckStepRatio = balanced.HardBottleneckStepRatio;
            row.MaxLowClearStreak = balanced.MaxLowClearStreak;
            row.LowClearRatio = balanced.LowClearRatio;
            row.SingleClearRatio = balanced.SingleClearRatio;
            row.AvgClearPerMove = balanced.AvgClearPerMove;
            row.AvgNewUnlocksPerMove = balanced.AvgNewUnlocksPerMove;
            row.ChoiceWaveStdDev = balanced.ChoiceWaveStdDev;
            row.LastQuarterAvgClear = balanced.LastQuarterAvgClear;
            row.LastQuarterLowClearRatio = balanced.LastQuarterLowClearRatio;
            row.LargestUnlockBurst = balanced.LargestUnlockBurst;
            row.DifficultyScore = CalculateExperienceScore(row);

            ApplyVerdict(row);
            return row;
        }

        static TraceSummary Simulate(BoardState start, IRuleset rules, PlayerPolicy policy)
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
                foreach (var candidate in candidates)
                {
                    bool oneClearTrap = bestClear <= 1;
                    bool goodByClear = !oneClearTrap && candidate.ClearCount >= goodClearThreshold;
                    if (goodByClear)
                        goodMoves++;
                }

                MoveCandidate chosen = Choose(candidates, policy);
                float avgClear = candidates.Count > 0 ? (float)candidates.Average(c => c.ClearCount) : 0f;
                int available = candidates.Count;
                float goodRatio = available > 0 ? goodMoves / (float)available : 0f;

                if (!rules.TryApplyMove(state, chosen.Move, out _))
                    break;

                candidates = BuildMoveCandidates(state, rules);
                int newlyClearable = Mathf.Max(0, candidates.Count - Mathf.Max(0, available - 1));

                steps.Add(new TraceStep
                {
                    StepIndex = stepIndex,
                    AvailableMoves = available,
                    GoodMoves = goodMoves,
                    GoodMoveRatio = goodRatio,
                    BestClearCount = bestClear,
                    AvgClearCount = avgClear,
                    ChosenClearCount = chosen.ClearCount,
                    NewlyClearableChains = newlyClearable,
                    NewlyClearableCells = 0,
                    UnlockFanout = newlyClearable,
                    IsBottleneckStep = goodMoves <= 1 || goodRatio <= 0.22f || available <= 2,
                    IsHardBottleneckStep = (goodMoves <= 1 && bestClear <= 3) || available <= 1,
                    IsLowClearStep = chosen.ClearCount <= 2 || avgClear <= 2.25f,
                    IsSingleClearStep = chosen.ClearCount <= 1
                });
            }

            if (!solved && rules.IsSolved(state))
                solved = true;

            return TraceSummary.FromSteps(steps, solved);
        }

        static MoveCandidate Choose(List<MoveCandidate> candidates, PlayerPolicy policy)
        {
            IEnumerable<MoveCandidate> ordered = policy switch
            {
                PlayerPolicy.Flow => candidates
                    .OrderByDescending(c => c.ClearCount)
                    .ThenByDescending(c => c.ChainLength)
                    .ThenBy(c => c.ChainId),
                PlayerPolicy.Planner => candidates
                    .OrderByDescending(c => c.NewlyClearableChains)
                    .ThenByDescending(c => c.MoveScore)
                    .ThenByDescending(c => c.ClearCount)
                    .ThenBy(c => c.ChainId),
                _ => candidates
                    .OrderByDescending(c => c.MoveScore)
                    .ThenByDescending(c => c.ClearCount)
                    .ThenBy(c => c.ChainId)
            };

            return ordered.First();
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

        static Dictionary<int, int> BuildClearableChainMap(BoardState state, IRuleset rules)
        {
            var result = new Dictionary<int, int>();
            var visited = new HashSet<int>();
            var chain = new HashSet<int>();

            for (int i = 0; i < state.tiles.Length; i++)
            {
                if (visited.Contains(i) || state.tiles[i].type != TileType.Arrow)
                    continue;

                var pos = new Vector2Int(i % state.width, i / state.width);
                ArrowChainUtility.CollectFullChain(state, pos, 0, chain);

                if (chain.Count == 0)
                {
                    visited.Add(i);
                    continue;
                }

                foreach (int idx in chain)
                    visited.Add(idx);

                bool clearable = false;
                foreach (int idx in chain)
                {
                    var move = new Move(new Vector2Int(idx % state.width, idx / state.width));
                    if (!rules.TryApplyMove(state, move, out MoveDelta delta))
                        continue;

                    int cleared = CountCleared(delta);
                    delta.Undo(state);
                    if (cleared <= 0)
                        continue;

                    clearable = true;
                    break;
                }

                if (clearable)
                    result[GetChainId(chain)] = chain.Count;
            }

            return result;
        }

        static int CalculateExperienceScore(ValidationRow row)
        {
            float scale = Norm(row.Chains, 30f, 280f) * 100f;
            float openingPressure =
                (1f - Mathf.Clamp01(row.OpeningGoodMoveRatio)) * 60f +
                (1f - Norm(row.OpeningGoodMoves, 1f, Mathf.Clamp(row.Chains * 0.08f, 4f, 16f))) * 40f;
            float bottleneck = row.BottleneckStepRatio * 55f + row.HardBottleneckStepRatio * 55f + Norm(row.MaxLowClearStreak, 1f, 10f) * 30f;
            float grind = row.LowClearRatio * 45f + row.SingleClearRatio * 30f + (1f - Norm(row.AvgClearPerMove, 2f, 8f)) * 35f;
            float choice = row.AvgAvailableMoves >= 8f && row.AvgGoodMoveRatio < 0.32f
                ? (row.AvgAvailableMoves - 7f) * 6f + (0.32f - row.AvgGoodMoveRatio) * 90f
                : Norm(row.ChoiceWaveStdDev, 1f, 8f) * 22f;
            float relief =
                Mathf.Clamp01(row.AvgGoodMoveRatio) * 34f +
                Norm(row.AvgClearPerMove, 3f, 9f) * 34f +
                (1f - Mathf.Clamp01(row.BottleneckStepRatio)) * 22f +
                Norm(row.LargestUnlockBurst, 1f, 8f) * 18f;

            return Mathf.Max(0, Mathf.RoundToInt(
                scale * 1.8f +
                openingPressure * 2.1f +
                bottleneck * 2.4f +
                grind * 1.4f +
                choice * 1.0f -
                Mathf.Min(relief, 70f) * 1.1f));
        }

        static void ApplyVerdict(ValidationRow row)
        {
            var flags = new List<string>();
            int severity = 0;

            if (!row.GreedySolved)
            {
                flags.Add("GreedyNotSolved");
                severity += 100;
            }

            if (!row.FlowSolved || !row.PlannerSolved)
            {
                flags.Add("PolicyMismatch");
                severity += 18;
            }

            if (row.OpeningMoves <= 1 || row.OpeningGoodMoves <= 1)
            {
                flags.Add("StartTooNarrow");
                severity += IsPeak(row) ? 12 : 24;
            }
            else if (row.OpeningMoves >= 8 && row.OpeningGoodMoveRatio < 0.25f)
            {
                flags.Add("StartFakeWide");
                severity += 16;
            }

            if (row.AvgAvailableMoves >= 9f && row.AvgGoodMoveRatio < 0.28f)
            {
                flags.Add("FakeManyChoices");
                severity += 18;
            }

            if (row.HardBottleneckStepRatio >= 0.25f)
            {
                flags.Add("HardBottleneckHeavy");
                severity += IsPeak(row) ? 14 : 24;
            }
            else if (row.BottleneckStepRatio >= 0.42f)
            {
                flags.Add("BottleneckHeavy");
                severity += IsPeak(row) ? 8 : 14;
            }

            if (row.MaxLowClearStreak >= 9 || row.LowClearRatio >= 0.42f)
            {
                flags.Add("GrindyLowClear");
                severity += 18;
            }

            if (row.LastQuarterLowClearRatio >= 0.55f && row.LastQuarterAvgClear <= 2.4f && row.Chains >= 70)
            {
                flags.Add("BadEndingFragments");
                severity += 16;
            }

            if (IsRecovery(row) && row.DifficultyScore >= 560)
            {
                flags.Add("RecoveryTooPressured");
                severity += 20;
            }

            if (IsPeak(row) && row.DifficultyScore < 340 && row.Chains < 120)
            {
                flags.Add("PeakTooFlat");
                severity += 12;
            }

            if (IsShape(row) && row.PlayableFill < 0.45f)
            {
                flags.Add("ShapeLowFill");
                severity += 12;
            }

            if (IsHole(row) && row.PlayableFill < 0.45f)
            {
                flags.Add("HoleLowPlayableFill");
                severity += 12;
            }

            row.Severity = severity;
            row.Flags = flags.Count == 0 ? "None" : string.Join("|", flags.Distinct());
            row.Status = severity >= 45 ? "Red" : severity >= 16 ? "Yellow" : "Green";
        }

        static bool IsPeak(ValidationRow row) =>
            row.RelativeDifficulty == "Hard" ||
            row.RelativeDifficulty == "VeryHard" ||
            row.RelativeDifficulty == "RelativeExtreme" ||
            row.Bucket == "hard" ||
            row.Bucket == "very_hard" ||
            row.Bucket == "extreme";

        static bool IsRecovery(ValidationRow row) =>
            row.RelativeDifficulty == "Recovery" ||
            row.SlotRole == "Recovery" ||
            row.Bucket == "refresh";

        static bool IsShape(ValidationRow row) =>
            !string.IsNullOrEmpty(row.PlannedType) && row.PlannedType.StartsWith("shape", StringComparison.Ordinal);

        static bool IsHole(ValidationRow row) =>
            !string.IsNullOrEmpty(row.PlannedType) && row.PlannedType.StartsWith("hole", StringComparison.Ordinal);

        static void WriteSummary(string path, IReadOnlyList<ValidationRow> rows)
        {
            var lines = new List<string>(rows.Count + 1) { string.Join(",", SummaryHeaders) };
            foreach (var row in rows)
                lines.Add(string.Join(",", SummaryHeaders.Select(h => EscapeCsv(GetSummaryValue(row, h)))));
            File.WriteAllLines(ToAbsolutePath(path), lines, new UTF8Encoding(false));
        }

        static void WriteFlagged(string path, IReadOnlyList<ValidationRow> rows)
        {
            var flagged = rows
                .Where(r => r.Status != "Green")
                .OrderBy(r => r.Status == "Red" ? 0 : 1)
                .ThenByDescending(r => r.Severity)
                .ThenBy(r => r.Order)
                .ToList();

            var lines = new List<string>(flagged.Count + 1) { string.Join(",", SummaryHeaders) };
            foreach (var row in flagged)
                lines.Add(string.Join(",", SummaryHeaders.Select(h => EscapeCsv(GetSummaryValue(row, h)))));
            File.WriteAllLines(ToAbsolutePath(path), lines, new UTF8Encoding(false));
        }

        static void WriteNotes(string path, IReadOnlyList<ValidationRow> rows, LevelPack pack)
        {
            WriteNotes(path, rows, pack, PackPath, ReviewPackPath);
        }

        static void WriteNotes(
            string path,
            IReadOnlyList<ValidationRow> rows,
            LevelPack pack,
            string packPath,
            string reviewPackPath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("# Campaign 500 Single-Level Validation");
            sb.AppendLine();
            sb.AppendLine($"Pack: `{packPath}`");
            sb.AppendLine($"Levels: {rows.Count}");
            sb.AppendLine($"Green: {rows.Count(r => r.Status == "Green")}");
            sb.AppendLine($"Yellow: {rows.Count(r => r.Status == "Yellow")}");
            sb.AppendLine($"Red: {rows.Count(r => r.Status == "Red")}");
            sb.AppendLine();
            sb.AppendLine("## By Type");
            foreach (var group in rows.GroupBy(r => string.IsNullOrEmpty(r.PlannedType) ? "unknown" : r.PlannedType).OrderByDescending(g => g.Count()))
                sb.AppendLine($"- {group.Key}: {group.Count()} (Y={group.Count(r => r.Status == "Yellow")}, R={group.Count(r => r.Status == "Red")})");
            sb.AppendLine();
            sb.AppendLine("## By Relative Difficulty");
            foreach (var group in rows.GroupBy(r => string.IsNullOrEmpty(r.RelativeDifficulty) ? "fixed/front" : r.RelativeDifficulty).OrderBy(g => g.Key))
                sb.AppendLine($"- {group.Key}: {group.Count()} (Y={group.Count(r => r.Status == "Yellow")}, R={group.Count(r => r.Status == "Red")})");
            sb.AppendLine();
            sb.AppendLine("## Top Flags");
            foreach (var group in rows.SelectMany(r => r.Flags.Split('|')).Where(f => f != "None" && !string.IsNullOrEmpty(f)).GroupBy(f => f).OrderByDescending(g => g.Count()))
                sb.AppendLine($"- {group.Key}: {group.Count()}");
            sb.AppendLine();
            sb.AppendLine("## Red / Yellow Review List");
            foreach (var row in rows.Where(r => r.Status != "Green").OrderBy(r => r.Status == "Red" ? 0 : 1).ThenByDescending(r => r.Severity).ThenBy(r => r.Order).Take(80))
                sb.AppendLine($"- L{row.Order}: {row.Status}, severity={row.Severity}, type={row.PlannedType}, rel={row.RelativeDifficulty}, chains={row.Chains}, flags={row.Flags}, id={row.LevelId}");
            sb.AppendLine();
            sb.AppendLine($"Review pack: `{reviewPackPath}`");
            sb.AppendLine();
            sb.AppendLine("Interpretation: Green can stay in the main flow; Yellow should be sampled by screenshot/playtest; Red is recommended for replacement or manual confirmation.");
            File.WriteAllText(ToAbsolutePath(path), sb.ToString(), new UTF8Encoding(false));
        }

        static void SaveReviewPack(IReadOnlyList<ValidationRow> rows)
        {
            SaveReviewPack(rows, ReviewPackPath);
        }

        static void SaveReviewPack(IReadOnlyList<ValidationRow> rows, string reviewPackPath)
        {
            var reviewLevels = rows
                .Where(r => r.Status != "Green" && r.Level != null)
                .OrderBy(r => r.Status == "Red" ? 0 : 1)
                .ThenByDescending(r => r.Severity)
                .ThenBy(r => r.Order)
                .Select(r => r.Level)
                .ToArray();

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(reviewPackPath);
            if (pack == null)
            {
                pack = ScriptableObject.CreateInstance<LevelPack>();
                string folder = Path.GetDirectoryName(reviewPackPath)?.Replace('\\', '/');
                if (!string.IsNullOrEmpty(folder) && !AssetDatabase.IsValidFolder(folder))
                    Directory.CreateDirectory(ToAbsolutePath(folder));
                AssetDatabase.CreateAsset(pack, reviewPackPath);
            }

            pack.packId = "campaign500_single_level_review";
            pack.displayName = $"Campaign 500 Single-Level Review ({reviewLevels.Length})";
            pack.levels = reviewLevels;
            EditorUtility.SetDirty(pack);
        }

        static void AttachPackToDemo(LevelPack pack, string logTag)
        {
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

        static void UpdateFrontPatchCsvs(LevelDefinition level3, LevelDefinition level5)
        {
            var replacements = new[]
            {
                new FrontPatchRow
                {
                    Order = 3,
                    Bucket = "refresh",
                    Type = "tutorial",
                    Score = "85",
                    Level = level3,
                    Packs = "tutorial_simple",
                    Families = "front20_patch:tutorial_simple:compact_gate",
                    QualityFlags = "fixed_front20_patch",
                    PortableScore = "85",
                    PortableQuality = "fixed_front20_patch"
                },
                new FrontPatchRow
                {
                    Order = 5,
                    Bucket = "refresh",
                    Type = "sweep",
                    Score = "93",
                    Level = level5,
                    Packs = "normal_campaign500",
                    Families = "front20_patch:normal_campaign500:sweep",
                    QualityFlags = "fixed_front20_patch",
                    PortableScore = "93",
                    PortableQuality = "fixed_front20_patch"
                }
            };

            PatchCsvRows(SelectionPath, replacements, isPlanSelection: true);
            PatchCsvRows(Front20SummaryPath, replacements, isPlanSelection: false);
            AppendFrontReplacementNotes(replacements);
        }

        static void PatchCsvRows(string assetPath, IReadOnlyList<FrontPatchRow> replacements, bool isPlanSelection)
        {
            string fullPath = ToAbsolutePath(assetPath);
            if (!File.Exists(fullPath))
                return;

            var table = ReadCsvTable(fullPath);
            if (table.Headers.Count == 0)
                return;

            var byOrder = replacements.ToDictionary(r => r.Order, r => r);
            foreach (var row in table.Rows)
            {
                if (!int.TryParse(Get(row, "order"), NumberStyles.Integer, Inv, out int order))
                    continue;
                if (!byOrder.TryGetValue(order, out var patch))
                    continue;

                ApplyFrontPatch(row, patch, isPlanSelection);
            }

            WriteCsvTable(fullPath, table);
        }

        static void ApplyFrontPatch(Dictionary<string, string> row, FrontPatchRow patch, bool isPlanSelection)
        {
            var authored = patch.Level.authoredLevel;
            Set(row, "bucket", patch.Bucket);
            Set(row, "type", patch.Type);
            Set(row, "score", patch.Score);
            Set(row, "levelId", patch.Level.levelId);
            Set(row, "path", AssetDatabase.GetAssetPath(patch.Level));
            Set(row, "assetPath", AssetDatabase.GetAssetPath(patch.Level));
            Set(row, "packs", patch.Packs);
            Set(row, "width", authored.width.ToString(Inv));
            Set(row, "height", authored.height.ToString(Inv));
            Set(row, "chains", (authored.arrows?.Count ?? 0).ToString(Inv));
            Set(row, "families", patch.Families);
            Set(row, "qualityFlags", patch.QualityFlags);
            Set(row, "portableSolved", "True");
            Set(row, "portableScore", patch.PortableScore);
            Set(row, "portableQuality", patch.PortableQuality);

            if (isPlanSelection)
            {
                Set(row, "selected", "1");
                Set(row, "selectStatus", "front20_patch");
            }
        }

        static void AppendFrontReplacementNotes(IReadOnlyList<FrontPatchRow> replacements)
        {
            string fullPath = ToAbsolutePath(Front20ReplacementPath);
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            bool needsHeader = !File.Exists(fullPath) || new FileInfo(fullPath).Length == 0;
            var lines = new List<string>();
            if (needsHeader)
                lines.Add("\"order\",\"oldLevelId\",\"oldChains\",\"newLevelId\",\"newChains\",\"newType\",\"newCoverage\",\"newOpeners\",\"newAvgChoices\"");

            foreach (var patch in replacements)
            {
                var authored = patch.Level.authoredLevel;
                float coverage = CountArrowTiles(authored) / Mathf.Max(1f, authored.width * authored.height);
                lines.Add(string.Join(",",
                    EscapeCsv(patch.Order.ToString(Inv)),
                    EscapeCsv("manual_front_patch"),
                    EscapeCsv(""),
                    EscapeCsv(patch.Level.levelId),
                    EscapeCsv((authored.arrows?.Count ?? 0).ToString(Inv)),
                    EscapeCsv(patch.Type),
                    EscapeCsv(F(coverage)),
                    EscapeCsv(""),
                    EscapeCsv("")));
            }

            File.AppendAllLines(fullPath, lines, new UTF8Encoding(false));
        }

        static CsvTable ReadCsvTable(string fullPath)
        {
            string[] lines = File.ReadAllLines(fullPath);
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
                for (int c = 0; c < table.Headers.Count; c++)
                    row[table.Headers[c]] = c < values.Count ? values[c] : "";
                table.Rows.Add(row);
            }

            return table;
        }

        static void WriteCsvTable(string fullPath, CsvTable table)
        {
            var lines = new List<string>(table.Rows.Count + 1)
            {
                string.Join(",", table.Headers.Select(EscapeCsv))
            };

            foreach (var row in table.Rows)
                lines.Add(string.Join(",", table.Headers.Select(h => EscapeCsv(Get(row, h)))));

            File.WriteAllLines(fullPath, lines, new UTF8Encoding(false));
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

        static string Get(Dictionary<string, string> record, string key)
        {
            return record != null && record.TryGetValue(key, out string value) ? value : "";
        }

        static void Set(Dictionary<string, string> record, string key, string value)
        {
            if (record != null && record.ContainsKey(key))
                record[key] = value ?? "";
        }

        static int CountArrowTiles(AuthoredLevelData authored)
        {
            int total = 0;
            if (authored?.arrows == null)
                return 0;

            foreach (var arrow in authored.arrows)
                total += arrow?.indices?.Count ?? 0;
            return total;
        }

        static int MaxChainLength(AuthoredLevelData authored)
        {
            int max = 0;
            if (authored?.arrows == null)
                return 0;

            foreach (var arrow in authored.arrows)
                max = Mathf.Max(max, arrow?.indices?.Count ?? 0);
            return max;
        }

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
            return GetChainId(chain);
        }

        static int GetChainId(HashSet<int> chain)
        {
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

        static float Norm(float value, float min, float max)
        {
            if (max <= min)
                return 0f;
            return Mathf.Clamp01((value - min) / (max - min));
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

        static string F(float value) => value.ToString("0.###", Inv);
        static string F(double value) => value.ToString("0.###", Inv);

        static string GetSummaryValue(ValidationRow row, string header) => header switch
        {
            "order" => row.Order.ToString(Inv),
            "status" => row.Status,
            "severity" => row.Severity.ToString(Inv),
            "flags" => row.Flags,
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
            "maxChainLength" => row.MaxChainLength.ToString(Inv),
            "difficultyScore" => row.DifficultyScore.ToString(Inv),
            "greedySolved" => row.GreedySolved ? "True" : "False",
            "greedySteps" => row.GreedySteps.ToString(Inv),
            "flowSolved" => row.FlowSolved ? "True" : "False",
            "flowSteps" => row.FlowSteps.ToString(Inv),
            "plannerSolved" => row.PlannerSolved ? "True" : "False",
            "plannerSteps" => row.PlannerSteps.ToString(Inv),
            "openingMoves" => row.OpeningMoves.ToString(Inv),
            "openingGoodMoves" => row.OpeningGoodMoves.ToString(Inv),
            "openingGoodMoveRatio" => F(row.OpeningGoodMoveRatio),
            "openingBestClear" => row.OpeningBestClear.ToString(Inv),
            "avgAvailableMoves" => F(row.AvgAvailableMoves),
            "avgGoodMoves" => F(row.AvgGoodMoves),
            "avgGoodMoveRatio" => F(row.AvgGoodMoveRatio),
            "bottleneckStepRatio" => F(row.BottleneckStepRatio),
            "hardBottleneckStepRatio" => F(row.HardBottleneckStepRatio),
            "maxLowClearStreak" => row.MaxLowClearStreak.ToString(Inv),
            "lowClearRatio" => F(row.LowClearRatio),
            "singleClearRatio" => F(row.SingleClearRatio),
            "avgClearPerMove" => F(row.AvgClearPerMove),
            "avgNewUnlocksPerMove" => F(row.AvgNewUnlocksPerMove),
            "largestUnlockBurst" => row.LargestUnlockBurst.ToString(Inv),
            "choiceWaveStdDev" => F(row.ChoiceWaveStdDev),
            "lastQuarterAvgClear" => F(row.LastQuarterAvgClear),
            "lastQuarterLowClearRatio" => F(row.LastQuarterLowClearRatio),
            "buildError" => row.BuildError,
            _ => ""
        };

        static readonly string[] SummaryHeaders =
        {
            "order", "status", "severity", "flags", "levelId", "assetPath",
            "bucket", "type", "relativeDifficulty", "slotRole",
            "width", "height", "chains", "arrowTiles", "blockTiles", "boardFill", "playableFill", "maxChainLength",
            "difficultyScore",
            "greedySolved", "greedySteps", "flowSolved", "flowSteps", "plannerSolved", "plannerSteps",
            "openingMoves", "openingGoodMoves", "openingGoodMoveRatio", "openingBestClear",
            "avgAvailableMoves", "avgGoodMoves", "avgGoodMoveRatio",
            "bottleneckStepRatio", "hardBottleneckStepRatio", "maxLowClearStreak", "lowClearRatio", "singleClearRatio",
            "avgClearPerMove", "avgNewUnlocksPerMove", "largestUnlockBurst", "choiceWaveStdDev",
            "lastQuarterAvgClear", "lastQuarterLowClearRatio", "buildError"
        };

        enum PlayerPolicy
        {
            Flow,
            Balanced,
            Planner
        }

        struct MoveCandidate
        {
            public Move Move;
            public int ChainId;
            public int ChainLength;
            public int ClearCount;
            public int NewlyClearableChains;
            public int NewlyClearableCells;
            public int UnlockFanout;
            public float MoveScore;
        }

        struct TraceStep
        {
            public int StepIndex;
            public int AvailableMoves;
            public int GoodMoves;
            public float GoodMoveRatio;
            public int BestClearCount;
            public float AvgClearCount;
            public int ChosenClearCount;
            public int NewlyClearableChains;
            public int NewlyClearableCells;
            public int UnlockFanout;
            public bool IsBottleneckStep;
            public bool IsHardBottleneckStep;
            public bool IsLowClearStep;
            public bool IsSingleClearStep;
        }

        struct TraceSummary
        {
            public int OpeningMoves;
            public int OpeningGoodMoves;
            public float OpeningGoodMoveRatio;
            public int OpeningBestClear;
            public float AvgAvailableMoves;
            public float AvgGoodMoves;
            public float AvgGoodMoveRatio;
            public float BottleneckStepRatio;
            public float HardBottleneckStepRatio;
            public int MaxLowClearStreak;
            public float LowClearRatio;
            public float SingleClearRatio;
            public float AvgClearPerMove;
            public float AvgNewUnlocksPerMove;
            public int LargestUnlockBurst;
            public float ChoiceWaveStdDev;
            public float LastQuarterAvgClear;
            public float LastQuarterLowClearRatio;
            public bool Solved;
            public int StepCount;

            public static TraceSummary FromSteps(IReadOnlyList<TraceStep> steps, bool solved)
            {
                if (steps == null || steps.Count == 0)
                    return new TraceSummary { Solved = solved };

                int count = steps.Count;
                int bottleneck = 0;
                int hardBottleneck = 0;
                int lowClear = 0;
                int singleClear = 0;
                int lowStreak = 0;
                int maxLowStreak = 0;
                int largestUnlock = 0;
                float available = 0f;
                float good = 0f;
                float goodRatio = 0f;
                float clear = 0f;
                float newUnlock = 0f;

                for (int i = 0; i < count; i++)
                {
                    TraceStep step = steps[i];
                    available += step.AvailableMoves;
                    good += step.GoodMoves;
                    goodRatio += step.GoodMoveRatio;
                    clear += step.ChosenClearCount;
                    newUnlock += step.NewlyClearableChains;
                    largestUnlock = Mathf.Max(largestUnlock, step.NewlyClearableChains);

                    if (step.IsBottleneckStep) bottleneck++;
                    if (step.IsHardBottleneckStep) hardBottleneck++;
                    if (step.IsLowClearStep)
                    {
                        lowClear++;
                        lowStreak++;
                        maxLowStreak = Mathf.Max(maxLowStreak, lowStreak);
                    }
                    else
                    {
                        lowStreak = 0;
                    }

                    if (step.IsSingleClearStep) singleClear++;
                }

                float avgAvailable = available / count;
                float variance = 0f;
                for (int i = 0; i < count; i++)
                {
                    float diff = steps[i].AvailableMoves - avgAvailable;
                    variance += diff * diff;
                }
                variance /= count;

                int lastStart = Mathf.Clamp(Mathf.FloorToInt(count * 0.75f), 0, count - 1);
                int lastCount = count - lastStart;
                float lastClear = 0f;
                int lastLow = 0;
                for (int i = lastStart; i < count; i++)
                {
                    lastClear += steps[i].ChosenClearCount;
                    if (steps[i].IsLowClearStep)
                        lastLow++;
                }

                TraceStep opening = steps[0];
                return new TraceSummary
                {
                    OpeningMoves = opening.AvailableMoves,
                    OpeningGoodMoves = opening.GoodMoves,
                    OpeningGoodMoveRatio = opening.GoodMoveRatio,
                    OpeningBestClear = opening.BestClearCount,
                    AvgAvailableMoves = avgAvailable,
                    AvgGoodMoves = good / count,
                    AvgGoodMoveRatio = goodRatio / count,
                    BottleneckStepRatio = bottleneck / (float)count,
                    HardBottleneckStepRatio = hardBottleneck / (float)count,
                    MaxLowClearStreak = maxLowStreak,
                    LowClearRatio = lowClear / (float)count,
                    SingleClearRatio = singleClear / (float)count,
                    AvgClearPerMove = clear / count,
                    AvgNewUnlocksPerMove = newUnlock / count,
                    LargestUnlockBurst = largestUnlock,
                    ChoiceWaveStdDev = Mathf.Sqrt(variance),
                    LastQuarterAvgClear = lastCount > 0 ? lastClear / lastCount : 0f,
                    LastQuarterLowClearRatio = lastCount > 0 ? lastLow / (float)lastCount : 0f,
                    Solved = solved,
                    StepCount = count
                };
            }
        }

        sealed class ValidationRow
        {
            public int Order;
            public LevelDefinition Level;
            public string LevelId = "";
            public string AssetPath = "";
            public string Bucket = "";
            public string PlannedType = "";
            public string RelativeDifficulty = "";
            public string SlotRole = "";
            public string Status = "Green";
            public int Severity;
            public string Flags = "None";
            public string BuildError = "";
            public int Width;
            public int Height;
            public int Chains;
            public int ArrowTiles;
            public int BlockTiles;
            public float BoardFill;
            public float PlayableFill;
            public int MaxChainLength;
            public int DifficultyScore;
            public bool GreedySolved;
            public int GreedySteps;
            public bool FlowSolved;
            public int FlowSteps;
            public bool PlannerSolved;
            public int PlannerSteps;
            public int OpeningMoves;
            public int OpeningGoodMoves;
            public float OpeningGoodMoveRatio;
            public int OpeningBestClear;
            public float AvgAvailableMoves;
            public float AvgGoodMoves;
            public float AvgGoodMoveRatio;
            public float BottleneckStepRatio;
            public float HardBottleneckStepRatio;
            public int MaxLowClearStreak;
            public float LowClearRatio;
            public float SingleClearRatio;
            public float AvgClearPerMove;
            public float AvgNewUnlocksPerMove;
            public int LargestUnlockBurst;
            public float ChoiceWaveStdDev;
            public float LastQuarterAvgClear;
            public float LastQuarterLowClearRatio;
        }

        sealed class FrontPatchRow
        {
            public int Order;
            public string Bucket = "";
            public string Type = "";
            public string Score = "";
            public LevelDefinition Level;
            public string Packs = "";
            public string Families = "";
            public string QualityFlags = "";
            public string PortableScore = "";
            public string PortableQuality = "";
        }

        sealed class CsvTable
        {
            public List<string> Headers = new();
            public List<Dictionary<string, string>> Rows = new();
        }
    }
}
#endif
