#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace PixelBug.ArrowMagic.EditorTools
{
    public static class LevelImportDifficultyAnalyzer
    {
        const string ImportReportFolder = "Assets/ArrowMagic/SOData/Reports/LevelImportV1";
        const string OutputManifestPath = ImportReportFolder + "/level_import_v1_difficulty_manifest.csv";
        const string OutputSummaryPath = ImportReportFolder + "/level_import_v1_difficulty_summary.csv";
        const string OutputNotesPath = ImportReportFolder + "/difficulty_v1_notes.md";

        const string NormalManifestPath = ImportReportFolder + "/normal_campaign500_manifest.csv";
        const string ShapeManifestPath = ImportReportFolder + "/shape_manifest.csv";
        const string HoleManifestPath = ImportReportFolder + "/hole_mask_early_front_manifest.csv";

        static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

        [MenuItem("Tools/Arrow Magic/Level Import/Build Difficulty V1 Manifest")]
        public static void BuildDifficultyManifest()
        {
            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            string normalRoot = Path.Combine(projectRoot, ".worktrees", "nomask-procedural-generator");
            string shapeRoot = Path.Combine(projectRoot, ".worktrees", "shape-procedural-mask-fill");
            string holeRoot = Path.GetFullPath(Path.Combine(projectRoot, "..", "ArrowLevel-Hand-HoleExperiment"));

            var candidates = new List<SourceCandidate>(800);
            candidates.AddRange(ReadNormalCandidates(projectRoot, normalRoot));
            candidates.AddRange(ReadShapeCandidates(projectRoot, shapeRoot));
            candidates.AddRange(ReadHoleCandidates(projectRoot, holeRoot));

            var rows = new List<DifficultyRow>(candidates.Count);
            for (int i = 0; i < candidates.Count; i++)
            {
                SourceCandidate candidate = candidates[i];
                if ((i % 25) == 0)
                {
                    EditorUtility.DisplayProgressBar(
                        "Level Import Difficulty V1",
                        $"{i + 1}/{candidates.Count}: {candidate.LevelId}",
                        candidates.Count > 0 ? i / (float)candidates.Count : 0f);
                }

                rows.Add(AnalyzeCandidateFast(candidate));
            }

            EditorUtility.ClearProgressBar();

            Directory.CreateDirectory(ToAbsolutePath(projectRoot, ImportReportFolder));
            WriteManifest(ToAbsolutePath(projectRoot, OutputManifestPath), rows);
            WriteSummary(ToAbsolutePath(projectRoot, OutputSummaryPath), rows);
            WriteNotes(ToAbsolutePath(projectRoot, OutputNotesPath), rows);

            AssetDatabase.Refresh();
            Debug.Log($"[LevelImportDifficultyAnalyzer] Wrote {rows.Count} rows to {OutputManifestPath}");
        }

        static IEnumerable<SourceCandidate> ReadNormalCandidates(string projectRoot, string normalRoot)
        {
            string path = ToAbsolutePath(projectRoot, NormalManifestPath);
            foreach (var record in ReadCsv(path))
            {
                string assetPath = Get(record, "assetPath");
                yield return new SourceCandidate
                {
                    SourceKind = "normal",
                    SourcePool = "normal_campaign500",
                    Order = ParseInt(Get(record, "order")),
                    LevelId = Get(record, "levelId"),
                    SourceDifficultyTag = MapNormalBucket(Get(record, "bucket")),
                    ExistingBucket = Get(record, "bucket"),
                    ExistingType = Get(record, "type"),
                    Theme = "",
                    SourceQualityFlags = Get(record, "qualityFlags"),
                    CoverageMetricName = "coverage",
                    CoverageMetric = ParseFloat(Get(record, "coverage")),
                    SourceScore = ParseFloat(Get(record, "score")),
                    Width = ParseInt(Get(record, "width")),
                    Height = ParseInt(Get(record, "height")),
                    Chains = ParseInt(Get(record, "chains")),
                    ArrowTiles = Mathf.RoundToInt(ParseFloat(Get(record, "coverage")) *
                                                  Mathf.Max(1, ParseInt(Get(record, "width")) * ParseInt(Get(record, "height")))),
                    BlockTiles = 0,
                    BoardFill = ParseFloat(Get(record, "coverage")),
                    PlayableFill = ParseFloat(Get(record, "coverage")),
                    InitialMovable = ParseInt(Get(record, "openers")),
                    AvgChoices = ParseFloat(Get(record, "avgChoices")),
                    UnlockBursts = ParseInt(Get(record, "unlockBursts")),
                    DependencyBlocks = ParseInt(Get(record, "dependencyBlocks")),
                    Outer1 = ParseFloat(Get(record, "outer1")),
                    SourcePath = ResolveSourcePath(normalRoot, assetPath),
                    DisplayAssetPath = assetPath
                };
            }
        }

        static IEnumerable<SourceCandidate> ReadShapeCandidates(string projectRoot, string shapeRoot)
        {
            string path = ToAbsolutePath(projectRoot, ShapeManifestPath);
            foreach (var record in ReadCsv(path))
            {
                string assetPath = Get(record, "assetPath");
                yield return new SourceCandidate
                {
                    SourceKind = "shape",
                    SourcePool = Get(record, "pool"),
                    Order = ParseInt(Get(record, "order")),
                    LevelId = Get(record, "levelId"),
                    SourceDifficultyTag = "",
                    ExistingBucket = "",
                    ExistingType = "",
                    Theme = Get(record, "theme"),
                    SourceQualityFlags = "",
                    CoverageMetricName = "maskFill",
                    CoverageMetric = ParseFloat(Get(record, "maskFill")),
                    SourceScore = 0f,
                    Width = ParseInt(Get(record, "width")),
                    Height = ParseInt(Get(record, "height")),
                    Chains = ParseInt(Get(record, "chains")),
                    ArrowTiles = ParseInt(Get(record, "arrowTiles")),
                    BlockTiles = ParseInt(Get(record, "blockTiles")),
                    BoardFill = ParseFloat(Get(record, "boardFill")),
                    PlayableFill = ParseFloat(Get(record, "playableFill")),
                    InitialMovable = ParseInt(Get(record, "initialMovable")),
                    TargetDifficultyScore = ParseFloat(Get(record, "targetDifficultyScore")),
                    SourcePath = ResolveSourcePath(shapeRoot, assetPath),
                    DisplayAssetPath = assetPath,
                    ExtraIdentity = Get(record, "maskName")
                };
            }
        }

        static IEnumerable<SourceCandidate> ReadHoleCandidates(string projectRoot, string holeRoot)
        {
            string path = ToAbsolutePath(projectRoot, HoleManifestPath);
            foreach (var record in ReadCsv(path))
            {
                string assetPath = Get(record, "assetPath");
                yield return new SourceCandidate
                {
                    SourceKind = "hole",
                    SourcePool = Get(record, "pool"),
                    Order = ParseInt(Get(record, "order")),
                    LevelId = Get(record, "levelId"),
                    SourceDifficultyTag = "",
                    ExistingBucket = "",
                    ExistingType = "",
                    Theme = "HoleBlocker",
                    SourceQualityFlags = "",
                    CoverageMetricName = "playableFill",
                    CoverageMetric = ParseFloat(Get(record, "playableFill")),
                    SourceScore = 0f,
                    Width = ParseInt(Get(record, "width")),
                    Height = ParseInt(Get(record, "height")),
                    Chains = ParseInt(Get(record, "chains")),
                    ArrowTiles = ParseInt(Get(record, "arrowTiles")),
                    BlockTiles = ParseInt(Get(record, "blockTiles")),
                    BoardFill = ParseFloat(Get(record, "boardFill")),
                    PlayableFill = ParseFloat(Get(record, "playableFill")),
                    InitialMovable = ParseInt(Get(record, "initialMovable")),
                    TargetDifficultyScore = ParseFloat(Get(record, "targetDifficultyScore")),
                    SourcePath = ResolveSourcePath(holeRoot, assetPath),
                    DisplayAssetPath = assetPath
                };
            }
        }

        static DifficultyRow AnalyzeCandidateFast(SourceCandidate candidate)
        {
            var row = new DifficultyRow
            {
                SourceKind = candidate.SourceKind,
                SourcePool = candidate.SourcePool,
                Order = candidate.Order,
                LevelId = candidate.LevelId,
                AssetName = !string.IsNullOrEmpty(candidate.ExtraIdentity)
                    ? candidate.ExtraIdentity
                    : Path.GetFileNameWithoutExtension(candidate.DisplayAssetPath ?? ""),
                SourceDifficultyTag = candidate.SourceDifficultyTag,
                ExistingBucket = candidate.ExistingBucket,
                ExistingType = candidate.ExistingType,
                Theme = candidate.Theme,
                CoverageMetricName = candidate.CoverageMetricName,
                CoverageMetric = candidate.CoverageMetric,
                SourceScore = candidate.SourceScore,
                SourceQualityFlags = candidate.SourceQualityFlags,
                AssetPath = candidate.DisplayAssetPath,
                MechanicTag = candidate.SourceKind switch
                {
                    "shape" => "ShapePuzzle",
                    "hole" => "HoleBlocker",
                    _ => "None"
                },
                NoveltyTag = candidate.SourceKind switch
                {
                    "shape" => "VisualSpice",
                    "hole" => "MechanicSpice",
                    _ => "Standard"
                },
                Width = Mathf.Max(1, candidate.Width),
                Height = Mathf.Max(1, candidate.Height),
                Chains = Mathf.Max(0, candidate.Chains),
                ArrowTiles = Mathf.Max(0, candidate.ArrowTiles),
                BlockTiles = Mathf.Max(0, candidate.BlockTiles),
                BoardFill = candidate.BoardFill > 0f
                    ? candidate.BoardFill
                    : candidate.ArrowTiles / Mathf.Max(1f, candidate.Width * candidate.Height),
                PlayableFill = candidate.PlayableFill > 0f
                    ? candidate.PlayableFill
                    : candidate.ArrowTiles / Mathf.Max(1f, candidate.Width * candidate.Height - candidate.BlockTiles),
                InitialMovableChains = Mathf.Max(0, candidate.InitialMovable),
                DifficultyScoreV0 = Mathf.RoundToInt(candidate.TargetDifficultyScore > 0f ? candidate.TargetDifficultyScore : candidate.SourceScore)
            };

            float averageChainLength = row.Chains > 0 ? row.ArrowTiles / (float)row.Chains : 0f;
            row.MaxChainLength = Mathf.Max(2, Mathf.RoundToInt(averageChainLength * 1.85f));

            DifficultyTraceSummary estimatedTrace = EstimateTrace(candidate, row, averageChainLength);
            DifficultyComponents components = CalculateComponents(row, estimatedTrace);

            row.AverageStepsToNextUnlock = EstimateStepsToNextUnlock(candidate, row, estimatedTrace);
            row.OpeningMoves = estimatedTrace.OpeningMoves;
            row.OpeningGoodMoves = estimatedTrace.OpeningGoodMoves;
            row.OpeningGoodMoveRatio = estimatedTrace.OpeningGoodMoveRatio;
            row.OpeningBestClear = estimatedTrace.OpeningBestClear;
            row.AvgAvailableMoves = estimatedTrace.AvgAvailableMoves;
            row.AvgGoodMoves = estimatedTrace.AvgGoodMoves;
            row.AvgGoodMoveRatio = estimatedTrace.AvgGoodMoveRatio;
            row.BottleneckStepRatio = estimatedTrace.BottleneckStepRatio;
            row.HardBottleneckStepRatio = estimatedTrace.HardBottleneckStepRatio;
            row.MaxLowClearStreak = estimatedTrace.MaxLowClearStreak;
            row.LowClearRatio = estimatedTrace.LowClearRatio;
            row.SingleClearRatio = estimatedTrace.SingleClearRatio;
            row.AvgClearPerMove = estimatedTrace.AvgClearPerMove;
            row.AvgNewUnlocksPerMove = estimatedTrace.AvgNewUnlocksPerMove;
            row.ChoiceWaveStdDev = estimatedTrace.ChoiceWaveStdDev;
            row.GreedySolved = true;
            row.GreedySteps = row.Chains;

            row.ScaleScore = components.ScaleScore;
            row.OpeningPressureScore = components.OpeningPressureScore;
            row.BottleneckScore = components.BottleneckScore;
            row.DecisionComplexityScore = components.DecisionComplexityScore;
            row.ClearEfficiencyPressureScore = components.ClearEfficiencyPressureScore;
            row.VisualStructureScore = components.VisualStructureScore;
            row.FlowReliefScore = components.FlowReliefScore;
            row.DifficultyScoreV1 = CalibrateFastDifficultyScore(candidate, row, components, estimatedTrace);

            row.DifficultyTagV1 = ClassifyDifficulty(row, components, estimatedTrace);
            row.DifficultyConfidence = candidate.SourceKind == "normal" ? "HeuristicHigh" : "HeuristicMedium";
            row.ScaleTag = ClassifyScale(row.Chains);
            row.PressureTag = ClassifyPressure(components, estimatedTrace);
            row.PaceTag = ClassifyPace(components, estimatedTrace);
            row.StartTag = ClassifyStart(estimatedTrace);
            row.ClearTag = ClassifyClear(estimatedTrace);
            row.ChoiceTag = ClassifyChoice(estimatedTrace);
            row.ShapeTag = ClassifyShape(row, candidate);
            row.VisualTag = ClassifyVisual(row, components);
            row.RiskTags = BuildRiskTags(row, candidate, estimatedTrace);
            row.ReasonTags = BuildReasonTags(row, components, estimatedTrace);
            row.TagMismatch = !string.IsNullOrEmpty(row.SourceDifficultyTag) &&
                              !IsCompatibleSourceTag(row.SourceDifficultyTag, row.DifficultyTagV1)
                ? "source-v1-mismatch"
                : "";

            return row;
        }

        static DifficultyTraceSummary EstimateTrace(SourceCandidate candidate, DifficultyRow row, float averageChainLength)
        {
            int chains = Mathf.Max(1, row.Chains);
            int openingMoves = candidate.InitialMovable > 0
                ? candidate.InitialMovable
                : EstimateOpeningMoves(candidate, row);

            float openerRatio = openingMoves / (float)chains;
            float avgAvailable = candidate.AvgChoices > 0f
                ? candidate.AvgChoices
                : Mathf.Clamp(openingMoves * (candidate.SourceKind == "hole" ? 0.72f : 0.86f), 1f, Mathf.Max(1f, openingMoves));

            float typePressure = candidate.ExistingType switch
            {
                "lock" => 0.16f,
                "section" => 0.10f,
                "maze" => 0.08f,
                "dense" => 0.06f,
                "sweep" => 0.04f,
                _ => 0f
            };
            if (candidate.SourceKind == "hole")
                typePressure += 0.18f;
            if (candidate.SourceKind == "shape" && candidate.SourcePool != "shape_early_prop")
                typePressure += 0.04f;

            float dependencyPressure = candidate.DependencyBlocks > 0
                ? Mathf.Clamp01(candidate.DependencyBlocks / Mathf.Max(1f, chains * 20f))
                : 0f;
            float sourceDifficultyPressure = EstimateSourceDifficultyPressure(candidate, row);
            float openingPressure = Mathf.Clamp01((0.16f - openerRatio) * 2.6f);

            float bottleneckRatio = Mathf.Clamp01(
                0.06f +
                typePressure +
                dependencyPressure * 0.32f +
                sourceDifficultyPressure +
                openingPressure * 0.36f);

            float hardBottleneckRatio = Mathf.Clamp01(
                bottleneckRatio * 0.45f +
                (openerRatio < 0.04f ? 0.08f : 0f) +
                (candidate.SourceKind == "hole" ? 0.04f : 0f));

            float avgClear = Mathf.Max(1f, averageChainLength);
            float lowClearRatio = Mathf.Clamp01((4.2f - avgClear) / 4.2f + bottleneckRatio * 0.22f);
            float singleClearRatio = Mathf.Clamp01((2.4f - avgClear) / 2.4f + hardBottleneckRatio * 0.18f);
            int maxLowClearStreak = Mathf.Clamp(
                Mathf.RoundToInt(lowClearRatio * 8f + hardBottleneckRatio * 6f),
                0,
                12);

            int openingGood = Mathf.Clamp(
                Mathf.RoundToInt(openingMoves * Mathf.Lerp(0.38f, 0.72f, Mathf.Clamp01(avgClear / 9f)) * (1f - bottleneckRatio * 0.35f)),
                openingMoves > 0 ? 1 : 0,
                Mathf.Max(1, openingMoves));
            if (openingMoves <= 2)
                openingGood = Mathf.Min(openingGood, openingMoves);

            float avgGoodRatio = Mathf.Clamp01(0.62f - bottleneckRatio * 0.45f - lowClearRatio * 0.16f);
            float avgGoodMoves = Mathf.Max(1f, avgAvailable * avgGoodRatio);
            float stdDev = Mathf.Clamp(
                avgAvailable * (0.18f + bottleneckRatio * 0.45f + (candidate.SourceKind == "hole" ? 0.10f : 0f)),
                0.2f,
                12f);

            return new DifficultyTraceSummary
            {
                OpeningMoves = openingMoves,
                OpeningGoodMoves = openingGood,
                OpeningGoodMoveRatio = openingMoves > 0 ? openingGood / (float)openingMoves : 0f,
                OpeningBestClear = Mathf.Max(1, Mathf.RoundToInt(avgClear * 1.25f)),
                AvgAvailableMoves = avgAvailable,
                AvgGoodMoves = avgGoodMoves,
                AvgGoodMoveRatio = avgGoodRatio,
                BottleneckStepRatio = bottleneckRatio,
                HardBottleneckStepRatio = hardBottleneckRatio,
                MaxLowClearStreak = maxLowClearStreak,
                LowClearRatio = lowClearRatio,
                SingleClearRatio = singleClearRatio,
                AvgClearPerMove = avgClear,
                AvgNewUnlocksPerMove = Mathf.Clamp(0.9f - bottleneckRatio * 1.2f, 0.05f, 0.9f),
                ChoiceWaveStdDev = stdDev,
                Solved = true,
                StepCount = chains
            };
        }

        static float EstimateSourceDifficultyPressure(SourceCandidate candidate, DifficultyRow row)
        {
            if (candidate.SourceKind == "normal")
            {
                return candidate.ExistingBucket switch
                {
                    "refresh" => 0.02f,
                    "normal" => 0.08f,
                    "hard" => 0.14f,
                    "very_hard" => 0.20f,
                    "extreme" => 0.26f,
                    _ => 0.08f
                };
            }

            if (candidate.SourceKind == "hole")
                return Norm(candidate.TargetDifficultyScore, 520f, 1500f) * 0.22f + 0.08f;

            if (candidate.SourceKind == "shape")
                return Norm(row.Chains, 70f, 210f) * 0.12f + (candidate.SourcePool == "shape_early_prop" ? 0f : 0.03f);

            return Norm(candidate.SourceScore, 90f, 170f) * 0.12f;
        }

        static int EstimateOpeningMoves(SourceCandidate candidate, DifficultyRow row)
        {
            float ratio = candidate.SourceKind switch
            {
                "hole" => 0.10f,
                "shape" => candidate.SourcePool == "shape_early_prop" ? 0.22f : 0.14f,
                _ => 0.15f
            };

            int estimated = Mathf.RoundToInt(row.Chains * ratio);
            if (candidate.SourceKind == "hole")
                estimated = Mathf.Max(2, estimated);
            else if (candidate.SourceKind == "shape_early_prop")
                estimated = Mathf.Max(5, estimated);
            else
                estimated = Mathf.Max(4, estimated);

            return Mathf.Clamp(estimated, 1, Mathf.Max(1, row.Chains));
        }

        static float EstimateStepsToNextUnlock(SourceCandidate candidate, DifficultyRow row, DifficultyTraceSummary trace)
        {
            float baseGap = Mathf.Lerp(0.6f, 4.5f, trace.BottleneckStepRatio);
            if (candidate.SourceKind == "hole")
                baseGap += 0.45f;
            if (row.ScaleTag is "Large" or "Huge")
                baseGap += 0.35f;
            return baseGap;
        }

        static int CalibrateFastDifficultyScore(SourceCandidate candidate, DifficultyRow row, DifficultyComponents components, DifficultyTraceSummary trace)
        {
            float raw = components.FinalScore;
            float anchor = raw;
            float anchorWeight = 0.55f;

            if (candidate.SourceKind == "normal")
            {
                anchor = candidate.ExistingBucket switch
                {
                    "refresh" => 175f,
                    "normal" => 340f,
                    "hard" => 600f,
                    "very_hard" => 790f,
                    "extreme" => 980f,
                    _ => raw
                };
                anchorWeight = 0.65f;
            }
            else if (candidate.SourceKind == "shape")
            {
                if (candidate.SourcePool == "shape_early_prop")
                {
                    anchor = 170f + Mathf.Clamp(row.Chains, 0, 60) * 1.2f;
                    anchorWeight = 0.62f;
                }
                else
                {
                    float scale = Norm(row.Chains, 45f, 210f);
                    float fillPenalty = row.CoverageMetric > 0f ? (1f - Mathf.Clamp01(row.CoverageMetric)) * 140f : 0f;
                    anchor = 260f + scale * 420f + fillPenalty;
                    anchorWeight = 0.58f;
                }
            }
            else if (candidate.SourceKind == "hole")
            {
                float target = candidate.TargetDifficultyScore > 0f ? candidate.TargetDifficultyScore : 800f;
                anchor = 300f + Norm(target, 520f, 1500f) * 520f + Norm(row.BlockTiles, 30f, 100f) * 70f;
                anchorWeight = 0.65f;
            }

            float pressureBonus =
                Mathf.Max(0f, components.OpeningPressureScore - 55f) * 1.2f +
                Mathf.Max(0f, components.BottleneckScore - 55f) * 1.1f +
                Mathf.Max(0f, components.ClearEfficiencyPressureScore - 58f) * 0.9f;
            float flowDiscount = Mathf.Max(0f, components.FlowReliefScore - 62f) * 1.4f;
            float calibrated = raw * (1f - anchorWeight) + anchor * anchorWeight + pressureBonus - flowDiscount;

            if (trace.OpeningGoodMoves <= 2 && row.Chains >= 80)
                calibrated += 45f;
            if (trace.MaxLowClearStreak >= 7)
                calibrated += 35f;

            return Mathf.Max(0, Mathf.RoundToInt(calibrated));
        }

        static DifficultyRow AnalyzeCandidate(SourceCandidate candidate, IRuleset ruleset)
        {
            var row = new DifficultyRow
            {
                SourceKind = candidate.SourceKind,
                SourcePool = candidate.SourcePool,
                Order = candidate.Order,
                LevelId = candidate.LevelId,
                SourceDifficultyTag = candidate.SourceDifficultyTag,
                ExistingBucket = candidate.ExistingBucket,
                ExistingType = candidate.ExistingType,
                Theme = candidate.Theme,
                CoverageMetricName = candidate.CoverageMetricName,
                CoverageMetric = candidate.CoverageMetric,
                SourceScore = candidate.SourceScore,
                SourceQualityFlags = candidate.SourceQualityFlags,
                AssetPath = candidate.DisplayAssetPath,
                MechanicTag = candidate.SourceKind switch
                {
                    "shape" => "ShapePuzzle",
                    "hole" => "HoleBlocker",
                    _ => "None"
                },
                NoveltyTag = candidate.SourceKind switch
                {
                    "shape" => "VisualSpice",
                    "hole" => "MechanicSpice",
                    _ => "Standard"
                }
            };

            if (!TryReadAuthoredLevelAsset(candidate.SourcePath, out AuthoredLevelData authored, out string readError))
            {
                row.BuildError = readError;
                ApplyFallbackTags(row, candidate);
                return row;
            }

            row.AssetName = Path.GetFileNameWithoutExtension(candidate.SourcePath);
            row.Width = Mathf.Max(1, authored.width);
            row.Height = Mathf.Max(1, authored.height);
            row.Chains = authored.arrows?.Count ?? 0;
            row.ArrowTiles = CountArrowTiles(authored);
            row.BlockTiles = authored.blockIndices?.Count ?? 0;
            row.BoardFill = row.ArrowTiles / Mathf.Max(1f, row.Width * row.Height);
            row.PlayableFill = row.ArrowTiles / Mathf.Max(1f, row.Width * row.Height - row.BlockTiles);

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out string buildError))
            {
                row.BuildError = buildError;
                ApplyFallbackTags(row, candidate);
                return row;
            }

            BoardGenerationTuning.BoardGenerationStats v0 = BoardGenerationTuning.CalculateBoardGenerationStats(board, ruleset);
            DifficultyTraceSummary trace = AnalyzeTrace(board, ruleset);
            DifficultyComponents components = CalculateComponents(row, trace);

            row.DifficultyScoreV0 = v0.DifficultyScore;
            row.InitialMovableChains = v0.InitialMovableArrowChainCount;
            row.MaxChainLength = v0.MaxChainLength;
            row.AverageStepsToNextUnlock = v0.AverageStepsToNextUnlock;

            row.OpeningMoves = trace.OpeningMoves;
            row.OpeningGoodMoves = trace.OpeningGoodMoves;
            row.OpeningGoodMoveRatio = trace.OpeningGoodMoveRatio;
            row.OpeningBestClear = trace.OpeningBestClear;
            row.AvgAvailableMoves = trace.AvgAvailableMoves;
            row.AvgGoodMoves = trace.AvgGoodMoves;
            row.AvgGoodMoveRatio = trace.AvgGoodMoveRatio;
            row.BottleneckStepRatio = trace.BottleneckStepRatio;
            row.HardBottleneckStepRatio = trace.HardBottleneckStepRatio;
            row.MaxLowClearStreak = trace.MaxLowClearStreak;
            row.LowClearRatio = trace.LowClearRatio;
            row.SingleClearRatio = trace.SingleClearRatio;
            row.AvgClearPerMove = trace.AvgClearPerMove;
            row.AvgNewUnlocksPerMove = trace.AvgNewUnlocksPerMove;
            row.ChoiceWaveStdDev = trace.ChoiceWaveStdDev;
            row.GreedySolved = trace.Solved;
            row.GreedySteps = trace.StepCount;

            row.ScaleScore = components.ScaleScore;
            row.OpeningPressureScore = components.OpeningPressureScore;
            row.BottleneckScore = components.BottleneckScore;
            row.DecisionComplexityScore = components.DecisionComplexityScore;
            row.ClearEfficiencyPressureScore = components.ClearEfficiencyPressureScore;
            row.VisualStructureScore = components.VisualStructureScore;
            row.FlowReliefScore = components.FlowReliefScore;
            row.DifficultyScoreV1 = components.FinalScore;

            row.DifficultyTagV1 = ClassifyDifficulty(row, components, trace);
            row.DifficultyConfidence = trace.Solved ? "High" : "Low";
            row.ScaleTag = ClassifyScale(row.Chains);
            row.PressureTag = ClassifyPressure(components, trace);
            row.PaceTag = ClassifyPace(components, trace);
            row.StartTag = ClassifyStart(trace);
            row.ClearTag = ClassifyClear(trace);
            row.ChoiceTag = ClassifyChoice(trace);
            row.ShapeTag = ClassifyShape(row, candidate);
            row.VisualTag = ClassifyVisual(row, components);
            row.RiskTags = BuildRiskTags(row, candidate, trace);
            row.ReasonTags = BuildReasonTags(row, components, trace);
            row.TagMismatch = !string.IsNullOrEmpty(row.SourceDifficultyTag) &&
                              !IsCompatibleSourceTag(row.SourceDifficultyTag, row.DifficultyTagV1)
                ? "source-v1-mismatch"
                : "";

            return row;
        }

        static DifficultyTraceSummary AnalyzeTrace(BoardState start, IRuleset ruleset)
        {
            var s = CloneBoard(start);
            var steps = new List<DifficultyTraceStep>(256);
            int maxSteps = Mathf.Max(512, s.width * s.height * 4);
            bool solved = false;

            for (int stepIndex = 0; stepIndex < maxSteps; stepIndex++)
            {
                if (ruleset.IsSolved(s))
                {
                    solved = true;
                    break;
                }

                var beforeClearable = BuildClearableChainMap(s, ruleset);
                var candidates = BuildMoveCandidates(s, ruleset);
                if (candidates.Count == 0)
                    break;

                int bestClear = candidates.Max(c => c.ClearCount);
                int goodMoves = 0;
                float goodClearThreshold = Mathf.Max(2f, bestClear * 0.65f);

                foreach (var candidate in candidates)
                {
                    bool oneClearTrap = bestClear <= 1;
                    if (!oneClearTrap && candidate.ClearCount >= goodClearThreshold)
                        goodMoves++;
                }

                MoveCandidate chosen = candidates
                    .OrderByDescending(c => c.ClearCount)
                    .ThenByDescending(c => c.MoveScore)
                    .ThenBy(c => c.ChainId)
                    .First();

                if (!ruleset.TryApplyMove(s, chosen.Move, out MoveDelta previewDelta))
                    break;

                Dictionary<int, int> afterClearable = BuildClearableChainMap(s, ruleset);
                previewDelta.Undo(s);

                foreach (var pair in afterClearable)
                {
                    if (beforeClearable.ContainsKey(pair.Key))
                        continue;

                    chosen.NewlyClearableChains++;
                    chosen.NewlyClearableCells += pair.Value;
                }
                chosen.UnlockFanout = chosen.NewlyClearableChains;
                chosen.MoveScore =
                    chosen.ClearCount * 1.0f +
                    chosen.NewlyClearableChains * 2.0f +
                    chosen.NewlyClearableCells * 0.4f +
                    chosen.UnlockFanout * 0.8f -
                    (afterClearable.Count <= 1 && afterClearable.Count > 0 ? 1.2f : 0f);

                float avgClear = candidates.Count > 0 ? (float)candidates.Average(c => c.ClearCount) : 0f;
                int available = candidates.Count;
                float goodRatio = available > 0 ? goodMoves / (float)available : 0f;

                steps.Add(new DifficultyTraceStep
                {
                    StepIndex = stepIndex,
                    AvailableMoves = available,
                    GoodMoves = goodMoves,
                    GoodMoveRatio = goodRatio,
                    BestClearCount = bestClear,
                    AvgClearCount = avgClear,
                    ChosenClearCount = chosen.ClearCount,
                    NewlyClearableChains = chosen.NewlyClearableChains,
                    NewlyClearableCells = chosen.NewlyClearableCells,
                    UnlockFanout = chosen.UnlockFanout,
                    IsBottleneckStep = goodMoves <= 1 || goodRatio <= 0.22f || available <= 2,
                    IsHardBottleneckStep = (goodMoves <= 1 && bestClear <= 3) || available <= 1,
                    IsLowClearStep = chosen.ClearCount <= 2 || avgClear <= 2.25f,
                    IsSingleClearStep = chosen.ClearCount <= 1
                });

                if (!ruleset.TryApplyMove(s, chosen.Move, out _))
                    break;
            }

            if (!solved && ruleset.IsSolved(s))
                solved = true;

            return DifficultyTraceSummary.FromSteps(steps, solved);
        }

        static List<MoveCandidate> BuildMoveCandidates(
            BoardState state,
            IRuleset ruleset)
        {
            var byChain = new Dictionary<int, MoveCandidate>();

            foreach (Move move in ruleset.GetLegalMoves(state))
            {
                if (!ruleset.TryApplyMove(state, move, out MoveDelta delta))
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
                    MoveScore = cleared
                };

                if (!byChain.TryGetValue(chainId, out MoveCandidate existing) ||
                    candidate.ClearCount > existing.ClearCount)
                {
                    byChain[chainId] = candidate;
                }
            }

            return byChain.Values.ToList();
        }

        static Dictionary<int, int> BuildClearableChainMap(BoardState state, IRuleset ruleset)
        {
            var result = new Dictionary<int, int>();
            var visited = new HashSet<int>();
            var chain = new HashSet<int>();

            for (int i = 0; i < state.tiles.Length; i++)
            {
                if (visited.Contains(i) || state.tiles[i].type != TileType.Arrow)
                    continue;

                Vector2Int pos = new Vector2Int(i % state.width, i / state.width);
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
                    if (!ruleset.TryApplyMove(state, move, out MoveDelta delta))
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

        static DifficultyComponents CalculateComponents(DifficultyRow row, DifficultyTraceSummary trace)
        {
            float mechanicPressure = row.SourceKind switch
            {
                "hole" => 10f,
                "shape" => 3f,
                _ => 0f
            };

            float scaleScore =
                100f * (Norm(row.Chains, 30f, 280f) * 0.62f +
                        Norm(row.ArrowTiles, 140f, 1500f) * 0.38f);

            float openingGoodTarget = Mathf.Clamp(row.Chains * 0.08f, 4f, 14f);
            float goodScarcity = 100f - 100f * Norm(trace.OpeningGoodMoves, 1f, openingGoodTarget);
            float openingRatioPressure = 100f - Mathf.Clamp01(trace.OpeningGoodMoveRatio) * 100f;
            float openingBestWeakness = 100f - 100f * Norm(trace.OpeningBestClear, 1f, 10f);
            float openingPressure = Clamp01Score(
                goodScarcity * 0.48f +
                openingRatioPressure * 0.34f +
                openingBestWeakness * 0.18f);

            float bottleneckScore = Clamp01Score(
                trace.BottleneckStepRatio * 48f +
                trace.HardBottleneckStepRatio * 42f +
                Norm(trace.MaxLowClearStreak, 1f, 9f) * 35f +
                trace.SingleClearRatio * 20f);

            float fakeChoicePressure = trace.AvgAvailableMoves >= 7f && trace.AvgGoodMoveRatio < 0.32f
                ? (trace.AvgAvailableMoves - 6f) * 5f + (0.32f - trace.AvgGoodMoveRatio) * 90f
                : 0f;
            float decisionComplexity = Clamp01Score(
                fakeChoicePressure +
                Norm(trace.ChoiceWaveStdDev, 1f, 9f) * 28f +
                (1f - Mathf.Clamp01(trace.AvgGoodMoveRatio)) * 34f +
                mechanicPressure * 0.7f);

            float clearEfficiencyPressure = Clamp01Score(
                (1f - Norm(trace.AvgClearPerMove, 2f, 9f)) * 62f +
                trace.LowClearRatio * 34f +
                trace.SingleClearRatio * 22f);

            float area = Mathf.Max(1f, row.Width * row.Height);
            float chainDensity = row.Chains / area;
            float blockRatio = row.BlockTiles / area;
            float visualStructure = Clamp01Score(
                Norm(row.Chains, 70f, 260f) * 36f +
                Norm(chainDensity, 0.045f, 0.16f) * 24f +
                Norm(row.MaxChainLength, 10f, 28f) * 12f +
                blockRatio * 80f +
                mechanicPressure);

            float flowRelief = Clamp01Score(
                Mathf.Clamp01(trace.AvgGoodMoveRatio) * 36f +
                Norm(trace.AvgClearPerMove, 3f, 9f) * 34f +
                (1f - Mathf.Clamp01(trace.BottleneckStepRatio)) * 22f +
                Norm(trace.OpeningGoodMoves, 2f, openingGoodTarget) * 20f);

            float pressureAverage = (openingPressure + bottleneckScore + clearEfficiencyPressure) / 3f;
            float flowReliefCapped = Mathf.Min(flowRelief, pressureAverage * 0.75f + 25f);

            int finalScore = Mathf.RoundToInt(10f * (
                scaleScore * 0.18f +
                openingPressure * 0.22f +
                bottleneckScore * 0.24f +
                decisionComplexity * 0.14f +
                clearEfficiencyPressure * 0.12f +
                visualStructure * 0.10f -
                flowReliefCapped * 0.08f));

            if (!trace.Solved)
                finalScore += 120;

            return new DifficultyComponents
            {
                ScaleScore = Round1(scaleScore),
                OpeningPressureScore = Round1(openingPressure),
                BottleneckScore = Round1(bottleneckScore),
                DecisionComplexityScore = Round1(decisionComplexity),
                ClearEfficiencyPressureScore = Round1(clearEfficiencyPressure),
                VisualStructureScore = Round1(visualStructure),
                FlowReliefScore = Round1(flowRelief),
                FinalScore = Mathf.Max(0, finalScore)
            };
        }

        static string ClassifyDifficulty(DifficultyRow row, DifficultyComponents components, DifficultyTraceSummary trace)
        {
            int score = row.DifficultyScoreV1;
            bool realExtremePressure =
                trace.HardBottleneckStepRatio >= 0.22f ||
                trace.MaxLowClearStreak >= 8 ||
                trace.OpeningGoodMoves <= 1 ||
                (components.BottleneckScore >= 70f && components.OpeningPressureScore >= 62f);

            if (score < 240 &&
                components.OpeningPressureScore < 35f &&
                components.BottleneckScore < 30f &&
                components.ClearEfficiencyPressureScore < 40f &&
                components.FlowReliefScore >= 58f &&
                trace.MaxLowClearStreak <= 3)
            {
                return "Flow";
            }

            if (score < 430)
                return "Normal";

            if (score < 620)
                return "Hard";

            if (score < 780)
                return "VeryHard";

            if (score >= 840 || realExtremePressure)
                return "Extreme";

            return "VeryHard";
        }

        static string ClassifyScale(int chains)
        {
            if (chains < 40) return "Tiny";
            if (chains < 80) return "Small";
            if (chains < 130) return "Medium";
            if (chains < 190) return "Large";
            return "Huge";
        }

        static string ClassifyPressure(DifficultyComponents c, DifficultyTraceSummary t)
        {
            if (c.BottleneckScore >= 70f || t.HardBottleneckStepRatio >= 0.22f) return "HighPressure";
            if (c.OpeningPressureScore >= 62f && c.BottleneckScore >= 48f) return "MixedPressure";
            if (c.OpeningPressureScore >= 62f) return "OpeningPressure";
            if (c.BottleneckScore >= 48f) return "BottleneckPressure";
            if (c.ClearEfficiencyPressureScore >= 55f) return "LowClearPressure";
            if (c.DecisionComplexityScore >= 55f) return "FakeChoicePressure";
            return "LowPressure";
        }

        static string ClassifyPace(DifficultyComponents c, DifficultyTraceSummary t)
        {
            if (t.HardBottleneckStepRatio >= 0.2f || c.BottleneckScore >= 66f) return "Spike";
            if (t.BottleneckStepRatio >= 0.28f) return "Bottleneck";
            if (t.LowClearRatio >= 0.32f) return "Grind";
            if (t.ChoiceWaveStdDev >= 5f) return "StopGo";
            if (c.FlowReliefScore >= 62f && c.BottleneckScore < 35f) return "Flow";
            return "Steady";
        }

        static string ClassifyStart(DifficultyTraceSummary t)
        {
            if (t.OpeningMoves >= 8 && t.OpeningGoodMoveRatio < 0.25f) return "StartFakeWide";
            if (t.OpeningGoodMoves <= 2 || t.OpeningMoves <= 2) return "StartNarrow";
            if (t.OpeningGoodMoves >= 8 && t.OpeningGoodMoveRatio >= 0.45f) return "StartWide";
            return "StartMedium";
        }

        static string ClassifyClear(DifficultyTraceSummary t)
        {
            if (t.LowClearRatio >= 0.34f || t.MaxLowClearStreak >= 7) return "ClearGrind";
            if (t.AvgClearPerMove >= 8f) return "ClearBurst";
            if (t.AvgClearPerMove >= 5f) return "ClearFlow";
            if (t.AvgClearPerMove >= 3f) return "ClearRhythm";
            return "ClearTight";
        }

        static string ClassifyChoice(DifficultyTraceSummary t)
        {
            if (t.AvgAvailableMoves >= 9f && t.AvgGoodMoveRatio < 0.3f) return "ChoiceFakeMany";
            if (t.AvgGoodMoves >= 5f && t.AvgGoodMoveRatio >= 0.45f) return "ChoiceManyGood";
            if (t.AvgGoodMoves <= 2f) return "ChoiceTactical";
            if (t.AvgAvailableMoves <= 3f) return "ChoiceSimple";
            return "ChoiceReadable";
        }

        static string ClassifyShape(DifficultyRow row, SourceCandidate candidate)
        {
            if (candidate.SourceKind == "shape") return "SpecialShape";
            if (candidate.SourceKind == "hole") return "HoleBlocker";

            float ratio = row.Width / Mathf.Max(1f, row.Height);
            if (ratio >= 1.35f) return "Wide";
            if (ratio <= 0.74f) return "Tall";
            if (row.BlockTiles > 0) return "Frame";
            return "Square";
        }

        static string ClassifyVisual(DifficultyRow row, DifficultyComponents components)
        {
            if (components.VisualStructureScore >= 78f) return "VisualChaotic";
            if (components.VisualStructureScore >= 58f || row.Chains >= 160) return "VisualDense";
            if (components.VisualStructureScore <= 24f || row.Chains < 45) return "VisualSparse";
            return "VisualMedium";
        }

        static string BuildRiskTags(DifficultyRow row, SourceCandidate candidate, DifficultyTraceSummary trace)
        {
            var tags = new List<string>();
            if (!string.IsNullOrEmpty(candidate.SourceQualityFlags) && candidate.SourceQualityFlags != "ok")
                tags.Add(candidate.SourceQualityFlags);
            if (!trace.Solved)
                tags.Add("GreedyTraceNotSolved");
            if (row.StartTag == "StartNarrow" && row.DifficultyTagV1 is "Hard" or "VeryHard" or "Extreme")
                tags.Add("StartNarrow");
            if (row.ClearTag == "ClearGrind")
                tags.Add("TooGrindy");
            if (row.VisualTag == "VisualChaotic")
                tags.Add("TooDense");
            if (candidate.SourceKind == "shape" && row.CoverageMetric > 0f && row.CoverageMetric < 0.94f)
                tags.Add("LowMaskFill");
            if (candidate.SourceKind == "hole" && row.PlayableFill < 0.62f)
                tags.Add("LowPlayableFill");
            return tags.Count == 0 ? "NoRisk" : string.Join("|", tags.Distinct());
        }

        static string BuildReasonTags(DifficultyRow row, DifficultyComponents c, DifficultyTraceSummary t)
        {
            var tags = new List<string>();
            if (row.StartTag == "StartNarrow") tags.Add("StartNarrow");
            if (row.StartTag == "StartFakeWide") tags.Add("StartFakeWide");
            if (c.BottleneckScore >= 55f) tags.Add("BottleneckHigh");
            if (t.MaxLowClearStreak >= 5) tags.Add("LowClearStreak");
            if (row.ChoiceTag == "ChoiceFakeMany") tags.Add("FakeChoiceHeavy");
            if (row.ClearTag == "ClearGrind") tags.Add("LowClearGrind");
            if (row.VisualTag is "VisualDense" or "VisualChaotic") tags.Add("VisualDense");
            if (row.ScaleTag is "Large" or "Huge") tags.Add("LargeScale");
            if (c.FlowReliefScore >= 60f) tags.Add("FlowRelief");
            return tags.Count == 0 ? "None" : string.Join("|", tags.Distinct());
        }

        static bool IsCompatibleSourceTag(string sourceTag, string v1Tag)
        {
            int s = DifficultyTagRank(sourceTag);
            int v = DifficultyTagRank(v1Tag);
            return s < 0 || v < 0 || Mathf.Abs(s - v) <= 1;
        }

        static int DifficultyTagRank(string tag) => tag switch
        {
            "Flow" => 0,
            "Normal" => 1,
            "Bottleneck" => 2,
            "Hard" => 3,
            "VeryHard" => 4,
            "Extreme" => 5,
            _ => -1
        };

        static void ApplyFallbackTags(DifficultyRow row, SourceCandidate candidate)
        {
            row.DifficultyScoreV1 = Mathf.RoundToInt(candidate.SourceScore);
            row.DifficultyTagV1 = !string.IsNullOrEmpty(candidate.SourceDifficultyTag)
                ? candidate.SourceDifficultyTag
                : InferFallbackDifficulty(candidate);
            row.DifficultyConfidence = "Low";
            row.ScaleTag = "Unknown";
            row.PressureTag = "MixedPressure";
            row.PaceTag = "Steady";
            row.StartTag = "StartMedium";
            row.ClearTag = "ClearRhythm";
            row.ChoiceTag = "ChoiceReadable";
            row.ShapeTag = candidate.SourceKind switch
            {
                "shape" => "SpecialShape",
                "hole" => "HoleBlocker",
                _ => "Square"
            };
            row.VisualTag = "VisualMedium";
            row.RiskTags = string.IsNullOrEmpty(candidate.SourceQualityFlags) ? "LowConfidence" : candidate.SourceQualityFlags + "|LowConfidence";
            row.ReasonTags = "FallbackOnly";
        }

        static string InferFallbackDifficulty(SourceCandidate candidate)
        {
            if (candidate.SourceKind == "shape")
            {
                if (candidate.SourcePool == "shape_early_prop") return "Flow";
                return "Normal";
            }

            if (candidate.SourceKind == "hole")
                return "Hard";

            return "Normal";
        }

        static string MapNormalBucket(string bucket) => bucket switch
        {
            "refresh" => "Flow",
            "normal" => "Normal",
            "hard" => "Hard",
            "very_hard" => "VeryHard",
            "extreme" => "Extreme",
            _ => ""
        };

        static bool TryReadAuthoredLevelAsset(string absolutePath, out AuthoredLevelData level, out string error)
        {
            level = null;
            error = "";

            if (string.IsNullOrEmpty(absolutePath) || !File.Exists(absolutePath))
            {
                error = $"missing asset: {absolutePath}";
                return false;
            }

            string text = File.ReadAllText(absolutePath);
            int authoredStart = text.IndexOf("  authoredLevel:", StringComparison.Ordinal);
            if (authoredStart < 0)
            {
                error = "missing authoredLevel section";
                return false;
            }

            int authoredEnd = text.IndexOf("\n  lose:", authoredStart, StringComparison.Ordinal);
            if (authoredEnd < 0)
                authoredEnd = text.IndexOf("\n  masking:", authoredStart, StringComparison.Ordinal);
            if (authoredEnd < 0)
                authoredEnd = text.Length;

            string section = text.Substring(authoredStart, authoredEnd - authoredStart);
            Match widthMatch = Regex.Match(section, @"(?m)^\s{4}width:\s*(\d+)\s*$");
            Match heightMatch = Regex.Match(section, @"(?m)^\s{4}height:\s*(\d+)\s*$");
            if (!widthMatch.Success || !heightMatch.Success)
            {
                error = "missing authored width/height";
                return false;
            }

            level = new AuthoredLevelData
            {
                width = int.Parse(widthMatch.Groups[1].Value, Inv),
                height = int.Parse(heightMatch.Groups[1].Value, Inv),
                arrows = new List<AuthoredArrowData>(),
                blockIndices = new List<int>()
            };

            foreach (Match match in Regex.Matches(section, @"(?m)^\s*-\s*indices:\s*([0-9a-fA-F]*)\s*$"))
            {
                if (!TryDecodeHexIndices(match.Groups[1].Value, out List<int> indices, out error))
                    return false;

                level.arrows.Add(new AuthoredArrowData
                {
                    indices = indices,
                    colorIndex = 0
                });
            }

            Match blockMatch = Regex.Match(section, @"(?m)^\s*blockIndices:\s*([0-9a-fA-F]*)\s*$");
            if (blockMatch.Success && !string.IsNullOrWhiteSpace(blockMatch.Groups[1].Value))
            {
                if (!TryDecodeHexIndices(blockMatch.Groups[1].Value, out List<int> blocks, out error))
                    return false;

                level.blockIndices.AddRange(blocks);
            }

            if (level.arrows.Count == 0)
            {
                error = "authoredLevel has no arrows";
                return false;
            }

            return true;
        }

        static bool TryDecodeHexIndices(string hex, out List<int> indices, out string error)
        {
            indices = new List<int>();
            error = "";

            if (string.IsNullOrWhiteSpace(hex))
                return true;

            if ((hex.Length % 8) != 0)
            {
                error = $"invalid index hex length: {hex.Length}";
                return false;
            }

            for (int i = 0; i < hex.Length; i += 8)
            {
                int b0 = ParseHexByte(hex, i);
                int b1 = ParseHexByte(hex, i + 2);
                int b2 = ParseHexByte(hex, i + 4);
                int b3 = ParseHexByte(hex, i + 6);
                indices.Add(b0 | (b1 << 8) | (b2 << 16) | (b3 << 24));
            }

            return true;
        }

        static int ParseHexByte(string hex, int offset)
        {
            return int.Parse(hex.Substring(offset, 2), NumberStyles.HexNumber, Inv);
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

        static float Clamp01Score(float value) => Mathf.Clamp(value, 0f, 100f);
        static float Round1(float value) => Mathf.Round(value * 10f) / 10f;

        static string ToAbsolutePath(string projectRoot, string projectRelativePath)
        {
            return Path.GetFullPath(Path.Combine(projectRoot, projectRelativePath.Replace('/', Path.DirectorySeparatorChar)));
        }

        static string ResolveSourcePath(string sourceRoot, string assetPath)
        {
            if (string.IsNullOrWhiteSpace(assetPath))
                return "";

            if (Path.IsPathRooted(assetPath))
                return assetPath;

            return Path.GetFullPath(Path.Combine(sourceRoot, assetPath.Replace('/', Path.DirectorySeparatorChar)));
        }

        static string Get(Dictionary<string, string> record, string key)
        {
            return record.TryGetValue(key, out string value) ? value : "";
        }

        static int ParseInt(string value)
        {
            return int.TryParse(value, NumberStyles.Integer, Inv, out int parsed) ? parsed : 0;
        }

        static float ParseFloat(string value)
        {
            return float.TryParse(value, NumberStyles.Float, Inv, out float parsed) ? parsed : 0f;
        }

        static List<Dictionary<string, string>> ReadCsv(string path)
        {
            var result = new List<Dictionary<string, string>>();
            if (!File.Exists(path))
                return result;

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

        static void WriteManifest(string path, IReadOnlyList<DifficultyRow> rows)
        {
            var lines = new List<string>(rows.Count + 1)
            {
                string.Join(",", ManifestHeaders)
            };

            foreach (DifficultyRow row in rows)
                lines.Add(string.Join(",", ManifestHeaders.Select(h => EscapeCsv(GetManifestValue(row, h)))));

            File.WriteAllLines(path, lines, new UTF8Encoding(false));
        }

        static void WriteSummary(string path, IReadOnlyList<DifficultyRow> rows)
        {
            var lines = new List<string>
            {
                "group,count,avgScore,minScore,maxScore,avgChains,avgOpeningGood,avgBottleneckRatio,difficultyTags,mechanicTags"
            };

            foreach (var group in rows.GroupBy(r => r.SourcePool).OrderBy(g => g.Key))
                lines.Add(BuildSummaryLine(group.Key, group));

            lines.Add(BuildSummaryLine("ALL", rows));
            File.WriteAllLines(path, lines, new UTF8Encoding(false));
        }

        static string BuildSummaryLine(string groupName, IEnumerable<DifficultyRow> rowsEnumerable)
        {
            var rows = rowsEnumerable.ToList();
            string tags = string.Join(" ",
                rows.GroupBy(r => r.DifficultyTagV1)
                    .OrderBy(g => DifficultyTagRank(g.Key))
                    .Select(g => $"{g.Key}:{g.Count()}"));
            string mechanics = string.Join(" ",
                rows.GroupBy(r => r.MechanicTag)
                    .OrderByDescending(g => g.Count())
                    .Select(g => $"{g.Key}:{g.Count()}"));

            return string.Join(",",
                EscapeCsv(groupName),
                rows.Count.ToString(Inv),
                F(rows.Average(r => r.DifficultyScoreV1)),
                rows.Min(r => r.DifficultyScoreV1).ToString(Inv),
                rows.Max(r => r.DifficultyScoreV1).ToString(Inv),
                F(rows.Average(r => r.Chains)),
                F(rows.Average(r => r.OpeningGoodMoves)),
                F(rows.Average(r => r.BottleneckStepRatio)),
                EscapeCsv(tags),
                EscapeCsv(mechanics));
        }

        static void WriteNotes(string path, IReadOnlyList<DifficultyRow> rows)
        {
            var sb = new StringBuilder();
            sb.AppendLine("# Level Import Difficulty V1 Notes");
            sb.AppendLine();
            sb.AppendLine("This report is generated by `LevelImportDifficultyAnalyzer`.");
            sb.AppendLine("It is a fast calibrated V1 manifest: use it for ordering and review, then trace-sample the risky edges before final import.");
            sb.AppendLine();
            sb.AppendLine("## Difficulty Tags");
            sb.AppendLine();
            foreach (var group in rows.GroupBy(r => r.DifficultyTagV1).OrderBy(g => DifficultyTagRank(g.Key)))
                sb.AppendLine($"- {group.Key}: {group.Count()}");
            sb.AppendLine();
            sb.AppendLine("## Source Pools");
            sb.AppendLine();
            foreach (var group in rows.GroupBy(r => r.SourcePool).OrderBy(g => g.Key))
                sb.AppendLine($"- {group.Key}: {group.Count()}");
            sb.AppendLine();
            sb.AppendLine("## Important Fields");
            sb.AppendLine();
            sb.AppendLine("- `DifficultyScoreV1`: calibrated pressure score from scale, opening pressure, bottlenecks, decision complexity, clear efficiency, visual structure, and flow relief.");
            sb.AppendLine("- `DifficultyTagV1`: Flow / Normal / Hard / VeryHard / Extreme.");
            sb.AppendLine("- `SourceDifficultyTag`: existing normal-campaign bucket mapping, kept for comparison.");
            sb.AppendLine("- `TagMismatch`: normal levels whose existing bucket differs from V1 by more than one tier.");
            sb.AppendLine("- `PressureTag`: records bottleneck, opening pressure, fake-choice pressure, and low-clear pressure separately from difficulty tier.");
            sb.AppendLine("- `RiskTags`: existing quality flags plus heuristic risks such as narrow start, grind, low fill, or visual density.");
            File.WriteAllText(path, sb.ToString(), new UTF8Encoding(false));
        }

        static string GetManifestValue(DifficultyRow row, string header) => header switch
        {
            "sourcePool" => row.SourcePool,
            "sourceKind" => row.SourceKind,
            "order" => row.Order.ToString(Inv),
            "levelId" => row.LevelId,
            "assetName" => row.AssetName,
            "sourceDifficultyTag" => row.SourceDifficultyTag,
            "existingBucket" => row.ExistingBucket,
            "existingType" => row.ExistingType,
            "difficultyScoreV0" => row.DifficultyScoreV0.ToString(Inv),
            "difficultyScoreV1" => row.DifficultyScoreV1.ToString(Inv),
            "difficultyTagV1" => row.DifficultyTagV1,
            "difficultyConfidence" => row.DifficultyConfidence,
            "tagMismatch" => row.TagMismatch,
            "scaleTag" => row.ScaleTag,
            "pressureTag" => row.PressureTag,
            "paceTag" => row.PaceTag,
            "startTag" => row.StartTag,
            "clearTag" => row.ClearTag,
            "choiceTag" => row.ChoiceTag,
            "shapeTag" => row.ShapeTag,
            "visualTag" => row.VisualTag,
            "noveltyTag" => row.NoveltyTag,
            "mechanicTag" => row.MechanicTag,
            "riskTags" => row.RiskTags,
            "reasonTags" => row.ReasonTags,
            "theme" => row.Theme,
            "width" => row.Width.ToString(Inv),
            "height" => row.Height.ToString(Inv),
            "chains" => row.Chains.ToString(Inv),
            "arrowTiles" => row.ArrowTiles.ToString(Inv),
            "blockTiles" => row.BlockTiles.ToString(Inv),
            "boardFill" => F(row.BoardFill),
            "playableFill" => F(row.PlayableFill),
            "coverageMetricName" => row.CoverageMetricName,
            "coverageMetric" => F(row.CoverageMetric),
            "sourceScore" => F(row.SourceScore),
            "initialMovableChains" => row.InitialMovableChains.ToString(Inv),
            "maxChainLength" => row.MaxChainLength.ToString(Inv),
            "averageStepsToNextUnlock" => F(row.AverageStepsToNextUnlock),
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
            "choiceWaveStdDev" => F(row.ChoiceWaveStdDev),
            "greedySolved" => row.GreedySolved ? "True" : "False",
            "greedySteps" => row.GreedySteps.ToString(Inv),
            "scaleScore" => F(row.ScaleScore),
            "openingPressureScore" => F(row.OpeningPressureScore),
            "bottleneckScore" => F(row.BottleneckScore),
            "decisionComplexityScore" => F(row.DecisionComplexityScore),
            "clearEfficiencyPressureScore" => F(row.ClearEfficiencyPressureScore),
            "visualStructureScore" => F(row.VisualStructureScore),
            "flowReliefScore" => F(row.FlowReliefScore),
            "assetPath" => row.AssetPath,
            "buildError" => row.BuildError,
            _ => ""
        };

        static string F(float value) => value.ToString("0.###", Inv);
        static string F(double value) => value.ToString("0.###", Inv);

        static string EscapeCsv(string value)
        {
            value ??= "";
            if (value.Contains('"') || value.Contains(',') || value.Contains('\n') || value.Contains('\r'))
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            return value;
        }

        static readonly string[] ManifestHeaders =
        {
            "sourcePool", "sourceKind", "order", "levelId", "assetName",
            "sourceDifficultyTag", "existingBucket", "existingType",
            "difficultyScoreV0", "difficultyScoreV1", "difficultyTagV1", "difficultyConfidence", "tagMismatch",
            "scaleTag", "pressureTag", "paceTag", "startTag", "clearTag", "choiceTag", "shapeTag", "visualTag",
            "noveltyTag", "mechanicTag", "riskTags", "reasonTags", "theme",
            "width", "height", "chains", "arrowTiles", "blockTiles", "boardFill", "playableFill",
            "coverageMetricName", "coverageMetric", "sourceScore",
            "initialMovableChains", "maxChainLength", "averageStepsToNextUnlock",
            "openingMoves", "openingGoodMoves", "openingGoodMoveRatio", "openingBestClear",
            "avgAvailableMoves", "avgGoodMoves", "avgGoodMoveRatio",
            "bottleneckStepRatio", "hardBottleneckStepRatio", "maxLowClearStreak", "lowClearRatio", "singleClearRatio",
            "avgClearPerMove", "avgNewUnlocksPerMove", "choiceWaveStdDev", "greedySolved", "greedySteps",
            "scaleScore", "openingPressureScore", "bottleneckScore", "decisionComplexityScore",
            "clearEfficiencyPressureScore", "visualStructureScore", "flowReliefScore",
            "assetPath", "buildError"
        };

        sealed class SourceCandidate
        {
            public string SourceKind;
            public string SourcePool;
            public int Order;
            public string LevelId;
            public string SourceDifficultyTag;
            public string ExistingBucket;
            public string ExistingType;
            public string Theme;
            public string SourceQualityFlags;
            public string CoverageMetricName;
            public float CoverageMetric;
            public float SourceScore;
            public int Width;
            public int Height;
            public int Chains;
            public int ArrowTiles;
            public int BlockTiles;
            public float BoardFill;
            public float PlayableFill;
            public int InitialMovable;
            public float TargetDifficultyScore;
            public float AvgChoices;
            public int UnlockBursts;
            public int DependencyBlocks;
            public float Outer1;
            public string SourcePath;
            public string DisplayAssetPath;
            public string ExtraIdentity;
        }

        sealed class DifficultyRow
        {
            public string SourceKind = "";
            public string SourcePool = "";
            public int Order;
            public string LevelId = "";
            public string AssetName = "";
            public string SourceDifficultyTag = "";
            public string ExistingBucket = "";
            public string ExistingType = "";
            public int DifficultyScoreV0;
            public int DifficultyScoreV1;
            public string DifficultyTagV1 = "";
            public string DifficultyConfidence = "";
            public string TagMismatch = "";
            public string ScaleTag = "";
            public string PressureTag = "";
            public string PaceTag = "";
            public string StartTag = "";
            public string ClearTag = "";
            public string ChoiceTag = "";
            public string ShapeTag = "";
            public string VisualTag = "";
            public string NoveltyTag = "";
            public string MechanicTag = "";
            public string RiskTags = "";
            public string ReasonTags = "";
            public string Theme = "";
            public int Width;
            public int Height;
            public int Chains;
            public int ArrowTiles;
            public int BlockTiles;
            public float BoardFill;
            public float PlayableFill;
            public string CoverageMetricName = "";
            public float CoverageMetric;
            public float SourceScore;
            public string SourceQualityFlags = "";
            public int InitialMovableChains;
            public int MaxChainLength;
            public float AverageStepsToNextUnlock;
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
            public float ChoiceWaveStdDev;
            public bool GreedySolved;
            public int GreedySteps;
            public float ScaleScore;
            public float OpeningPressureScore;
            public float BottleneckScore;
            public float DecisionComplexityScore;
            public float ClearEfficiencyPressureScore;
            public float VisualStructureScore;
            public float FlowReliefScore;
            public string AssetPath = "";
            public string BuildError = "";
        }

        struct DifficultyComponents
        {
            public float ScaleScore;
            public float OpeningPressureScore;
            public float BottleneckScore;
            public float DecisionComplexityScore;
            public float ClearEfficiencyPressureScore;
            public float VisualStructureScore;
            public float FlowReliefScore;
            public int FinalScore;
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

        struct DifficultyTraceStep
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

        struct DifficultyTraceSummary
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
            public float ChoiceWaveStdDev;
            public bool Solved;
            public int StepCount;

            public static DifficultyTraceSummary FromSteps(IReadOnlyList<DifficultyTraceStep> steps, bool solved)
            {
                if (steps == null || steps.Count == 0)
                    return new DifficultyTraceSummary { Solved = solved };

                int count = steps.Count;
                int bottleneck = 0;
                int hardBottleneck = 0;
                int lowClear = 0;
                int singleClear = 0;
                int lowStreak = 0;
                int maxLowStreak = 0;
                float available = 0f;
                float good = 0f;
                float goodRatio = 0f;
                float clear = 0f;
                float newUnlock = 0f;

                for (int i = 0; i < count; i++)
                {
                    DifficultyTraceStep step = steps[i];
                    available += step.AvailableMoves;
                    good += step.GoodMoves;
                    goodRatio += step.GoodMoveRatio;
                    clear += step.ChosenClearCount;
                    newUnlock += step.NewlyClearableChains;

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

                DifficultyTraceStep opening = steps[0];
                return new DifficultyTraceSummary
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
                    ChoiceWaveStdDev = Mathf.Sqrt(variance),
                    Solved = solved,
                    StepCount = count
                };
            }
        }
    }
}
#endif
