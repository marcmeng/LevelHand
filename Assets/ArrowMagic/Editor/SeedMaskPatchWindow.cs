#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace PixelBug.ArrowMagic.EditorTools
{
    public sealed class SeedMaskPatchWindow : EditorWindow
    {
        private enum PlacementMode
        {
            Center,
            Auto
        }

        private enum PatchOutputMode
        {
            RawClip,
            GreedyRescue,
            FillRescue
        }

        private const string kWindowTitle = "Seed Mask Patch";
        private const string kDefaultOutputFolder = "Assets/ArrowMagic/SOData/Levels/Seeds";
        private const string kDemoComparePackPath = "Assets/ArrowMagic/SOData/Packs/Demo_Seed_Mask_Compare.asset";
        private const string kDemoComparePackFolder = "Assets/ArrowMagic/SOData/Packs";
        private const string kDemoComparePackPrefix = "Demo_Seed_Mask_Compare";
        private const string kProductionHoleMaskFolder = "Assets/ArrowMagic/Masks/Production/HoleLongOuterStrong";
        private const string kProductionHoleCandidateFolder = "Assets/ArrowMagic/SOData/Levels/Production/HoleLongOuterStrong/Candidates";
        private const string kProductionHoleSelectedFolder = "Assets/ArrowMagic/SOData/Levels/Production/HoleLongOuterStrong/Selected";
        private const string kProductionHoleReportFolder = "Assets/ArrowMagic/Reports/Production/HoleLongOuterStrong";
        private const string kProductionHolePackFolder = "Assets/ArrowMagic/SOData/Packs/Production/HoleLongOuterStrong";
        private const string kProductionHoleCandidatesPackPath = "Assets/ArrowMagic/SOData/Packs/Production/HoleLongOuterStrong/HoleLongOuterStrong_Production_Candidates.asset";
        private const string kProductionHoleSourcePackFolder = "Assets/ArrowMagic/SOData/Packs/R1FullPipeline/OuterStrongFinal";
        private const string kShapeExperimentMaskFolder = "Assets/ArrowMagic/Masks/ShapeExperiment";
        private const string kShapeExperimentCatalogPath = "Assets/ArrowMagic/SOData/Reports/ShapeExperiment/shape_experiment_mask_catalog.csv";
        private const string kShapeExperimentReportPath = "Assets/ArrowMagic/SOData/Reports/ShapeExperiment/shape_experiment_preview_pack_report.txt";
        private const string kShapeExperimentCandidateFolder = "Assets/ArrowMagic/SOData/Levels/ShapeExperiment/Candidates";
        private const string kShapeExperimentPackFolder = "Assets/ArrowMagic/SOData/Packs/ShapeExperiment";
        private const string kShapeExperimentPreviewPackPath = "Assets/ArrowMagic/SOData/Packs/ShapeExperiment/ShapeExperimentPreviewPack.asset";
        private const string kOriginalStarMaskPath = "Assets/ArrowMagic/Masks/Mask_19x19-Star.png";
        private const string kOriginalStarPreviewReportPath = "Assets/ArrowMagic/SOData/Reports/ShapeExperiment/original_star_preview_report.txt";
        private const string kOriginalStarPreviewPackPath = "Assets/ArrowMagic/SOData/Packs/ShapeExperiment/OriginalStarMaskPreviewPack.asset";
        private const string kReadableShapePreviewReportPath = "Assets/ArrowMagic/SOData/Reports/ShapeExperiment/readable_shape_preview_pack_report.txt";
        private const string kReadableShapeAttachReportPath = "Assets/ArrowMagic/SOData/Reports/ShapeExperiment/readable_shape_preview_attach_report.txt";
        private const string kReadableShapePreviewPackPath = "Assets/ArrowMagic/SOData/Packs/ShapeExperiment/ReadableShapePreviewPack.asset";
        private const string kEarlySymbolPreviewReportPath = "Assets/ArrowMagic/SOData/Reports/ShapeExperiment/early_symbol_preview_pack_report.txt";
        private const string kEarlySymbolPreviewPackPath = "Assets/ArrowMagic/SOData/Packs/ShapeExperiment/EarlySymbolPreviewPack.asset";
        private const string kAnimalShapePreviewReportPath = "Assets/ArrowMagic/SOData/Reports/ShapeExperiment/animal_shape_preview_pack_report.txt";
        private const string kAnimalShapeFollowupReportPath = "Assets/ArrowMagic/SOData/Reports/ShapeExperiment/animal_shape_followup_pack_report.txt";
        private const string kAnimalShapeAttachReportPath = "Assets/ArrowMagic/SOData/Reports/ShapeExperiment/animal_shape_preview_attach_report.txt";
        private const string kAnimalShapePreviewPackPath = "Assets/ArrowMagic/SOData/Packs/ShapeExperiment/AnimalShapePreviewPack.asset";
        private const string kAnimalBestPreviewReportPath = "Assets/ArrowMagic/SOData/Reports/ShapeExperiment/animal_best_preview_pack_report.txt";
        private const string kAnimalBestTightCropReportPath = "Assets/ArrowMagic/SOData/Reports/ShapeExperiment/animal_best_tight_crop_report.txt";
        private const string kAnimalBestTightCropFolder = "Assets/ArrowMagic/SOData/Levels/ShapeExperiment/Candidates/TightCrop";
        private const string kAnimalBestPreviewPackPath = "Assets/ArrowMagic/SOData/Packs/ShapeExperiment/AnimalBestPreviewPack.asset";
        private const string kTallFitShapePreviewReportPath = "Assets/ArrowMagic/SOData/Reports/ShapeExperiment/tall_fit_shape_preview_pack_report.txt";
        private const string kTallFitShapePreviewPackPath = "Assets/ArrowMagic/SOData/Packs/ShapeExperiment/TallFitShapePreviewPack.asset";
        private const string kTallFitEarlyKeepReportPath = "Assets/ArrowMagic/SOData/Reports/ShapeExperiment/tall_fit_early_shape_keep_report.txt";
        private const string kTallFitEarlyKeepLevelFolder = "Assets/ArrowMagic/SOData/Levels/ShapeExperiment/KeepEarly";
        private const string kTallFitEarlyKeepMaskFolder = "Assets/ArrowMagic/Masks/ShapeKeepEarly";
        private const string kTallFitEarlyKeepPackPath = "Assets/ArrowMagic/SOData/Packs/ShapeExperiment/KeepEarly/TallFitEarlyShapeKeepPack.asset";
        private const string kShapeEarlyKeepRetrospectiveReportPath = "Assets/ArrowMagic/SOData/Reports/ShapeExperiment/shape_early_keep_retrospective_report.txt";
        private const string kShapeEarlyKeepRetrospectivePackPath = "Assets/ArrowMagic/SOData/Packs/ShapeExperiment/KeepEarly/ShapeEarlyKeepRetrospectivePack.asset";
        private const string kShapeExperimentSourcePackFolder = "Assets/ArrowMagic/SOData/Packs/R1FullPipeline/OuterStrongFinal";
        private const string kDemoScenePath = "Assets/ArrowMagic/Scenes/Demo.unity";
        private const int kShapeExperimentPreviewMaskLimit = 10;
        private const int kProductionHoleWidth = 10;
        private const int kProductionHoleHeight = 14;
        private const float kProductionFinalFillRatio = 0.92f;
        private const int kProductionCandidatesPerMask = 3;
        private const int kProductionPreviewSeedLimit = 120;
        private const int kProductionDeepAttemptLimit = 16;
        private const float kHealTargetFillRatio = 1.0f;
        private const int kHealMaxPasses = 12;
        private const int kMicroFillMaxPasses = 16;
        private const int kMicroFillMaxPathLen = 10;
        private const int kMicroFillCandidateCap = 12000;
        private const int kMicroRewritePenalty = 80;
        private const int kGreedyTrimPerChain = 2;
        private const int kGreedyFallbackTrimPerChain = 2;
        private const int kGreedyFallbackLoopBreakCap = 8;
        private const int kGreedyFallbackChainClearCap = 20;
        private const int kGreedyCoreTakeoutMaxChains = 3;
        private const int kGreedyCoreCycleTakeoutMaxChains = 8;
        private const int kGreedyCoreIterativeTakeoutMaxPasses = 8;
        private const int kGreedyCoreIterativeTakeoutMaxChainsPerPass = 6;
        private const int kGreedyCoreTakeoutSearchChainCap = 7;
        private const int kGreedyCoreTakeoutCandidateCap = 80;
        private const float kGreedyCoreTakeoutMinAcceptRatio = 0.50f;
        private const float kGreedySafeRefillTargetRatio = 0.97f;
        private const int kGreedySafeRefillMaxPasses = 96;
        private const int kGreedySafeRefillMaxPathLen = 8;
        private const int kGreedySafeRefillCandidateEvalCap = 260;
        private const int kGreedySafeBoundaryRefillCandidateEvalCap = 520;
        private const int kDefaultCandidateTopCount = 3;
        private const int kGreedyCandidateFillWeight = 900;
        private const int kGreedyCandidateShapeWeight = 9;
        private const int kGreedyCandidateEdgeShapeWeight = 22;
        private const int kGreedyCandidateRemovalPenalty = 260;
        private const float kGreedyRepairKeepFloorRatio = 0.95f;
        private const float kGreedyRepairEmergencyFloorRatio = 0.90f;
        private const string kSeedMatchRoot = "Assets/ArrowMagic/SOData/Levels/Seeds";
        private const int kMaxMatchCandidates = 400;
        private const int kDefaultCandidatePreEvalLimit = 120;
        private const int kDefaultCandidatePreEvalTimeoutMs = 30000;
        private const float kDefaultCandidateMinFillRatio = 0.98f;
        private const float kRepairExperimentFillRatio = 1.00f;
        private const float kRepairExperimentFillRatioFloor = 0.97f;
        private const int kRepairExperimentFallbackSteps = 3;
        private const int kRepairExperimentInflationPasses = 1;
        private const int kRepairExperimentGreedyMultiplier = 16;
        private const int kRepairExperimentFallbackLoops = 3;
        private const int kRepairExperimentGenerationAttempts = 420;
        private const int kRepairExperimentTrimPerChain = 3;
        private const string kRepairExperimentOutputPrefix = "seed_mask_repairexp";
        private const string kSeedMatchCacheFileName = "SeedMaskPatchSeedMatchCache.json";
        private const string kRawClipOutputPrefixSuffix = "rawclip";

        [Serializable]
        private sealed class SeedMatchCacheEntry
        {
            public string guid;
            public string path;
            public string levelId;
            public int width;
            public int height;
            public bool authored;
            public long assetWriteTimeUtcTicks;
        }

        [Serializable]
        private sealed class SeedMatchCache
        {
            public long generatedUtcTicks;
            public SeedMatchCacheEntry[] entries;
        }

        private sealed class SeedCandidateResult
        {
            public LevelDefinition SourceSeed;
            public string SourceSeedPath;
            public bool IsFeasible;
            public int SourceArrowCount;
            public int PreservedTiles;
            public int FinalArrowCount;
            public int TargetFill;
            public int FinalFill;
            public float FillRatio;
            public int RemovedChains;
            public int RemovedArrows;
            public int Score;
            public string Details;

            public string NameOrPath => string.IsNullOrWhiteSpace(SourceSeed != null ? SourceSeed.name : null)
                ? SourceSeedPath
                : SourceSeed != null ? SourceSeed.name : SourceSeedPath;
        }

        private sealed class SeedMatchEntry
        {
            public LevelDefinition Seed;
            public string CachedPath;
            public int SizeScore;
        }

        private sealed class CompositeSeedPart
        {
            public readonly string SeedPath;
            public readonly Vector2Int Offset;

            public CompositeSeedPart(string seedPath, Vector2Int offset)
            {
                SeedPath = seedPath;
                Offset = offset;
            }
        }

        private sealed class BoundaryExitCell
        {
            public int Index;
            public Dir OutDir;
            public int SortKey;
        }

        private sealed class SafeRefillCandidate
        {
            public int[] Path;
            public int Gain;
            public int Score;
        }

        private sealed class ProductionHoleMaskSpec
        {
            public readonly string Id;
            public readonly int Width;
            public readonly int Height;
            public readonly string Shape;

            public ProductionHoleMaskSpec(string id, int width, int height, string shape)
            {
                Id = id;
                Width = width;
                Height = height;
                Shape = shape;
            }
        }

        private sealed class StraightnessStats
        {
            public int Chains;
            public int ArrowTiles;
            public int StraightChains;
            public int LongStraightChains;
            public int BentChains;
            public int LongestRun;
            public int LongestChain;
            public int LongStraightTiles;
            public float StraightChainRatio;
            public float LongStraightChainRatio;
            public float LongStraightTileRatio;
            public float BentChainRatio;
        }

        private sealed class HoleBlockSeedCandidate
        {
            public LevelDefinition Seed;
            public string SeedPath;
            public Vector2Int Offset;
            public int Preserved;
            public int CleanFill;
            public int CleanChains;
            public int BlockHits;
            public int BoundaryEmpty;
            public int Score;
            public string Details;
        }

        private sealed class HoleBlockRunResult
        {
            public HoleBlockSeedCandidate Candidate;
            public string RawPath;
            public string MidPath;
            public string FinalPath;
            public int FastFill;
            public int MidFill;
            public int FinalFill;
            public int MidBlockHits;
            public int FinalBlockHits;
            public int Score;
            public string Details;
        }

        [SerializeField]
        private LevelDefinition _seedDefinition;

        [SerializeField]
        private Texture2D _mask;

        [SerializeField]
        private PlacementMode _placementMode = PlacementMode.Center;

        [SerializeField]
        private bool _autoIfCenterFails = true;

        [SerializeField]
        private int _generationAttempts = 250;

        [SerializeField]
        private int _maxGreedyMovesMultiplier = 8;

        [SerializeField]
        private int _fallbackCleanupLoops = 2;

        [SerializeField]
        private bool _layerLikeMaskMode = true;

        [SerializeField]
        private bool _maskAlphaOnly = true;

        [SerializeField]
        private float _maskHealFillRatio = kHealTargetFillRatio;

        [SerializeField]
        private bool _adaptiveHealFillFallback = false;

        [SerializeField]
        private float _maskHealFillRatioFloor = 0.95f;

        [SerializeField]
        private int _maskHealFillFallbackSteps = 0;

        [SerializeField]
        private bool _inflateMaskForSolvability = false;

        [SerializeField]
        private int _maskInflationPasses = 0;

        [SerializeField]
        private string _outputFolder = kDefaultOutputFolder;

        [SerializeField]
        private string _outputPrefix = "seed_mask";

        [SerializeField]
        private int _candidateTopCount = kDefaultCandidateTopCount;
        [SerializeField]
        private int _candidatePreEvalLimit = kDefaultCandidatePreEvalLimit;
        [SerializeField]
        private int _candidatePreEvalTimeoutMs = kDefaultCandidatePreEvalTimeoutMs;
        [SerializeField]
        private float _candidateMinFillRatio = kDefaultCandidateMinFillRatio;

        [SerializeField]
        private bool _allowGreedyFullChainRemoval = true;

        [SerializeField]
        private bool _allowGreedyFallbackChainClear = true;

        [SerializeField]
        private bool _exportRawIfGreedyFails = true;

        [SerializeField]
        private PatchOutputMode _patchOutputMode = PatchOutputMode.GreedyRescue;

        [SerializeField]
        private int _greedyTrimPerChain = kGreedyTrimPerChain;

        [SerializeField]
        private int _preGreedyGeometryFixPasses = 8;

        [SerializeField]
        private int _greedyRepairCandidateEvalCap = 0;

        [SerializeField]
        private int _greedyRepairTimeBudgetMs = 0;

        [SerializeField]
        private bool _allowGreedyCoreTakeoutRefillRescue = true;

        private readonly List<SeedCandidateResult> _candidateResults = new List<SeedCandidateResult>();
        private readonly List<LevelDefinition> _matchedSeeds = new List<LevelDefinition>();
        private readonly List<SeedMatchCacheEntry> _seedMatchCacheEntries = new List<SeedMatchCacheEntry>();
        private string _candidateEvalSummary = string.Empty;
        private string _candidateMatchSummary = string.Empty;
        private string _matchedMaskPath = string.Empty;
        private string _seedMatchCacheSummary = string.Empty;

        private bool _seedMatchCacheLoaded;
        private bool _seedMatchCacheDirty = true;
        private string _latestRawClipPath = string.Empty;
        private string _latestGeneratedPath = string.Empty;

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch")]
        public static void Open()
        {
            GetWindow<SeedMaskPatchWindow>(kWindowTitle);
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Run FullSeed Patch Demo")]
        public static void RunFullSeedPatchDemo()
        {
            var seedPath = "Assets/ArrowMagic/SOData/Levels/Seeds/seed_Arrowz_level_015.asset";
            var maskPath = "Assets/ArrowMagic/Masks/Mask_11x11-Squares.png";

            var seed = AssetDatabase.LoadAssetAtPath<LevelDefinition>(seedPath);
            var mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);

            if (seed == null || mask == null)
            {
                Debug.LogError("[SeedMaskPatch] Demo seed or mask missing.");
                return;
            }

            var window = CreateInstance<SeedMaskPatchWindow>();
            window._seedDefinition = seed;
            window._mask = mask;
            window._placementMode = PlacementMode.Center;
            window._autoIfCenterFails = true;
            window._generationAttempts = 250;
            window._maxGreedyMovesMultiplier = 8;
            window._fallbackCleanupLoops = 2;
            window._outputFolder = kDefaultOutputFolder;
            window._outputPrefix = "seed_mask";
            window._maskHealFillRatio = kHealTargetFillRatio;
            window._inflateMaskForSolvability = false;
            window._adaptiveHealFillFallback = false;
            window._maskHealFillRatioFloor = 1.00f;
            window._maskHealFillFallbackSteps = 0;
            window._maskInflationPasses = 0;

            var generatedPath = window.ProcessSingle(seed, mask);
            if (string.IsNullOrWhiteSpace(generatedPath))
            {
                Debug.LogError("[SeedMaskPatch] FullSeed patch demo aborted: generation failed.");
                return;
            }

            if (!TrySyncDemoComparePack(seed, generatedPath, window._latestRawClipPath))
            {
                Debug.LogError("[SeedMaskPatch] FullSeed patch demo aborted: generated seed did not refresh demo compare pack.");
                return;
            }

            AssetDatabase.Refresh();
            Debug.Log("[SeedMaskPatch] FullSeed patch demo run finished.");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Run Selected Seed With Selected Mask")]
        public static void RunSelectedSeedWithSelectedMask()
        {
            if (!TryResolveSelectedSeedAndMask(out var seed, out var mask))
                return;

            var window = CreateInstance<SeedMaskPatchWindow>();
            ApplyMenuDefaultRuntimeSettings(window);
            window._seedDefinition = seed;
            window._mask = mask;

            Debug.Log($"[SeedMaskPatch] Manual run start: {AssetDatabase.GetAssetPath(seed)} + {AssetDatabase.GetAssetPath(mask)}");

            string result = window.ProcessSingle(seed, mask);
            if (string.IsNullOrWhiteSpace(result))
            {
                Debug.LogError("[SeedMaskPatch] Manual run did not produce output.");
                return;
            }

            if (!TrySyncDemoComparePack(seed, result, window._latestRawClipPath))
            {
                Debug.LogError("[SeedMaskPatch] Manual run did not update demo compare pack.");
                return;
            }

            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] Manual run finished: {result}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Run Selected Seed With Selected Mask (No UI)")]
        public static void RunSelectedSeedWithSelectedMaskNoWindow()
        {
            RunSelectedSeedWithSelectedMask();
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Run Selected Seed With Selected Mask (Repair Experiment)")]
        public static void RunSelectedSeedWithSelectedMaskRepairExperiment()
        {
            if (!TryResolveSelectedSeedAndMask(out var seed, out var mask))
            {
                OpenForSeedMaskSelection("repair experiment");
                return;
            }

            var window = CreateInstance<SeedMaskPatchWindow>();
            ApplyMenuDefaultRuntimeSettings(window);
            ApplyRepairExperimentRuntimeSettings(window);
            window._seedDefinition = seed;
            window._mask = mask;

            Debug.Log($"[SeedMaskPatch] Repair Experiment start: {AssetDatabase.GetAssetPath(seed)} + {AssetDatabase.GetAssetPath(mask)}");

            string result = window.ProcessSingle(seed, mask);
            if (string.IsNullOrWhiteSpace(result))
            {
                Debug.LogError("[SeedMaskPatch] Repair Experiment did not produce output.");
                return;
            }

            if (!TrySyncDemoComparePack(seed, result, window._latestRawClipPath))
            {
                Debug.LogError("[SeedMaskPatch] Repair Experiment did not update demo compare pack.");
                return;
            }

            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] Repair Experiment finished: {result}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Run 954 RocketSimple Experiment")]
        public static void Run954RocketSimpleExperiment()
        {
            const string seedPath = "Assets/ArrowMagic/SOData/Levels/Seeds/seed_Above300_level_954.asset";
            const string maskPath = "Assets/ArrowMagic/Masks/Mask_30x36-RocketSimple.png";
            const string reportPath = "Temp/seedmask_954_rocket_rawmask_report.txt";

            var seed = AssetDatabase.LoadAssetAtPath<LevelDefinition>(seedPath);
            var mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);
            if (seed == null || mask == null)
            {
                Debug.LogError($"[SeedMaskPatch] 954 RocketSimple experiment assets missing. seed={seedPath}, mask={maskPath}");
                return;
            }

            var window = CreateInstance<SeedMaskPatchWindow>();
            ApplyMenuDefaultRuntimeSettings(window);
            window._seedDefinition = seed;
            window._mask = mask;
            window._placementMode = PlacementMode.Center;
            window._autoIfCenterFails = false;
            window._patchOutputMode = PatchOutputMode.GreedyRescue;
            window._inflateMaskForSolvability = false;
            window._maskInflationPasses = 0;
            window._adaptiveHealFillFallback = false;
            window._maskHealFillRatio = 1f;
            window._maskHealFillRatioFloor = 1f;
            window._maskHealFillFallbackSteps = 0;
            window._maxGreedyMovesMultiplier = kRepairExperimentGreedyMultiplier;
            window._fallbackCleanupLoops = kRepairExperimentFallbackLoops;
            window._greedyTrimPerChain = kRepairExperimentTrimPerChain;
            window._preGreedyGeometryFixPasses = 10;
            window._allowGreedyFullChainRemoval = false;
            window._allowGreedyFallbackChainClear = false;
            window._outputPrefix = "seed_mask_954_rocket_rawmask_greedyrescue";

            Debug.Log($"[SeedMaskPatch] 954 RocketSimple Experiment start: {seedPath} + {maskPath}");
            string result = window.ProcessSingle(seed, mask);
            string report = $"SeedMaskPatch 954 RocketSimple Experiment\n" +
                            $"Seed={seedPath}\n" +
                            $"Mask={maskPath}\n" +
                            $"Mode={window._patchOutputMode}\n" +
                            $"Placement={window._placementMode}, AutoFallback={window._autoIfCenterFails}\n" +
                            $"InflateForRescue={window._inflateMaskForSolvability}, InflatePasses={window._maskInflationPasses}\n" +
                            $"Raw={window._latestRawClipPath}\n" +
                            $"Final={result}\n" +
                            $"Succeeded={!string.IsNullOrWhiteSpace(result)}\n";

            try
            {
                File.WriteAllText(reportPath, report);
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[SeedMaskPatch] Failed to write 954 RocketSimple report: {ex.Message}");
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                Debug.LogError($"[SeedMaskPatch] 954 RocketSimple Experiment failed. raw={window._latestRawClipPath}");
                return;
            }

            if (!TrySyncDemoComparePack(seed, result, window._latestRawClipPath))
            {
                Debug.LogError("[SeedMaskPatch] 954 RocketSimple Experiment did not update demo compare pack.");
                return;
            }

            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] 954 RocketSimple Experiment finished: raw={window._latestRawClipPath}, final={result}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Run 954 RocketKid Experiment")]
        public static void Run954RocketKidExperiment()
        {
            const string seedPath = "Assets/ArrowMagic/SOData/Levels/Seeds/seed_Above300_level_954.asset";
            const string maskPath = "Assets/ArrowMagic/Masks/Mask_30x36-RocketKid.png";
            const string reportPath = "Temp/seedmask_954_rocketkid_report.txt";

            var seed = AssetDatabase.LoadAssetAtPath<LevelDefinition>(seedPath);
            var mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);
            if (seed == null || mask == null)
            {
                Debug.LogError($"[SeedMaskPatch] 954 RocketKid experiment assets missing. seed={seedPath}, mask={maskPath}");
                return;
            }

            var window = CreateInstance<SeedMaskPatchWindow>();
            ApplyMenuDefaultRuntimeSettings(window);
            window._seedDefinition = seed;
            window._mask = mask;
            window._placementMode = PlacementMode.Center;
            window._autoIfCenterFails = false;
            window._patchOutputMode = PatchOutputMode.GreedyRescue;
            window._inflateMaskForSolvability = false;
            window._maskInflationPasses = 0;
            window._adaptiveHealFillFallback = false;
            window._maskHealFillRatio = 1f;
            window._maskHealFillRatioFloor = 1f;
            window._maskHealFillFallbackSteps = 0;
            window._maxGreedyMovesMultiplier = kRepairExperimentGreedyMultiplier;
            window._fallbackCleanupLoops = kRepairExperimentFallbackLoops;
            window._greedyTrimPerChain = kRepairExperimentTrimPerChain;
            window._preGreedyGeometryFixPasses = 10;
            window._allowGreedyFullChainRemoval = false;
            window._allowGreedyFallbackChainClear = false;
            window._outputPrefix = "seed_mask_954_rocketkid_greedyrescue";

            Debug.Log($"[SeedMaskPatch] 954 RocketKid Experiment start: {seedPath} + {maskPath}");
            string result = window.ProcessSingle(seed, mask);
            string report = $"SeedMaskPatch 954 RocketKid Experiment\n" +
                            $"Seed={seedPath}\n" +
                            $"Mask={maskPath}\n" +
                            $"Mode={window._patchOutputMode}\n" +
                            $"Placement={window._placementMode}, AutoFallback={window._autoIfCenterFails}\n" +
                            $"InflateForRescue={window._inflateMaskForSolvability}, InflatePasses={window._maskInflationPasses}\n" +
                            $"Raw={window._latestRawClipPath}\n" +
                            $"Final={result}\n" +
                            $"Succeeded={!string.IsNullOrWhiteSpace(result)}\n";

            try
            {
                File.WriteAllText(reportPath, report);
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[SeedMaskPatch] Failed to write 954 RocketKid report: {ex.Message}");
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                Debug.LogError($"[SeedMaskPatch] 954 RocketKid Experiment failed. raw={window._latestRawClipPath}");
                return;
            }

            if (!TrySyncDemoComparePack(seed, result, window._latestRawClipPath))
            {
                Debug.LogError("[SeedMaskPatch] 954 RocketKid Experiment did not update demo compare pack.");
                return;
            }

            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] 954 RocketKid Experiment finished: raw={window._latestRawClipPath}, final={result}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Run Shape Experiment Preview Pack")]
        public static void RunShapeExperimentPreviewPack()
        {
            var report = new List<string>
            {
                "SeedMaskPatch Shape Experiment Preview Pack",
                $"MaskFolder={kShapeExperimentMaskFolder}",
                $"SourcePackFolder={kShapeExperimentSourcePackFolder}",
                $"CandidateOutput={kShapeExperimentCandidateFolder}",
                $"Pack={kShapeExperimentPreviewPackPath}",
                $"MaskLimit={kShapeExperimentPreviewMaskLimit}",
                "Mode=SolidShapeMaskCropRepairOneFinalPerMask"
            };

            var maskPaths = LoadShapeExperimentMaskPaths(kShapeExperimentPreviewMaskLimit);
            report.Add($"LoadedMasks={maskPaths.Count}");
            if (maskPaths.Count == 0)
            {
                WriteReport(kShapeExperimentReportPath, report);
                Debug.LogError("[SeedMaskPatch] Shape Experiment Preview failed: no masks found.");
                return;
            }

            var sourceSeeds = LoadShapeExperimentSourceSeeds(report);
            report.Add($"LoadedSourceSeeds={sourceSeeds.Count}");
            if (sourceSeeds.Count == 0)
            {
                WriteReport(kShapeExperimentReportPath, report);
                Debug.LogError("[SeedMaskPatch] Shape Experiment Preview failed: no source seeds found.");
                return;
            }

            EnsureFolderExists(kShapeExperimentCandidateFolder);
            EnsureFolderExists(kShapeExperimentPackFolder);

            var generatedPaths = new List<string>();
            for (int i = 0; i < maskPaths.Count; i++)
            {
                string maskPath = maskPaths[i];
                var mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);
                if (mask == null)
                {
                    report.Add($"[{i + 1}] Mask={maskPath} | Missing");
                    WriteReport(kShapeExperimentReportPath, report);
                    continue;
                }

                var window = CreateInstance<SeedMaskPatchWindow>();
                ApplyMenuDefaultRuntimeSettings(window);
                window._mask = mask;
                window._placementMode = PlacementMode.Center;
                window._autoIfCenterFails = false;
                window._patchOutputMode = PatchOutputMode.GreedyRescue;
                window._inflateMaskForSolvability = false;
                window._maskInflationPasses = 0;
                window._adaptiveHealFillFallback = false;
                window._maskHealFillRatio = 1f;
                window._maskHealFillRatioFloor = 1f;
                window._maskHealFillFallbackSteps = 0;
                window._maxGreedyMovesMultiplier = kRepairExperimentGreedyMultiplier;
                window._fallbackCleanupLoops = kRepairExperimentFallbackLoops;
                window._greedyTrimPerChain = kRepairExperimentTrimPerChain;
                window._preGreedyGeometryFixPasses = 10;
                window._allowGreedyFullChainRemoval = false;
                window._allowGreedyFallbackChainClear = false;
                window._candidateTopCount = 3;
                window._candidatePreEvalLimit = 24;
                window._candidatePreEvalTimeoutMs = 60000;
                window._candidateMinFillRatio = 0.90f;
                window._outputFolder = kShapeExperimentCandidateFolder;
                window.EnsureOutputFolder();
                window._outputPrefix = $"shapeexp_{i + 1:00}_{SanitizeName(Path.GetFileNameWithoutExtension(maskPath)).ToLowerInvariant()}";

                string matchSummary;
                MatchShapeExperimentSeeds(window, mask, sourceSeeds, 80, out matchSummary);
                report.Add($"[{i + 1}] Mask={maskPath} | {matchSummary}");

                Debug.Log($"[SeedMaskPatch] Shape Experiment Preview match/eval start {i + 1}/{maskPaths.Count}: {maskPath}");
                window.RunCandidatePreEval();

                int previewCount = Math.Min(3, window._candidateResults.Count);
                for (int r = 0; r < previewCount; r++)
                    report.Add($"    Top{r + 1}: {BuildCandidateResultLine(r + 1, window._candidateResults[r])}");

                SeedCandidateResult chosen = null;
                for (int r = 0; r < window._candidateResults.Count; r++)
                {
                    var candidate = window._candidateResults[r];
                    if (candidate != null && candidate.IsFeasible && candidate.SourceSeed != null)
                    {
                        chosen = candidate;
                        break;
                    }
                }

                if (chosen == null)
                {
                    report.Add("    Generated=False | Reason=No feasible matched candidate");
                    WriteReport(kShapeExperimentReportPath, report);
                    continue;
                }

                string generated = window.ProcessSingle(chosen.SourceSeed, mask);
                int maskArea = 0;
                int finalFill = 0;
                float ratio = 0f;
                int chains = 0;
                bool straightRejected = false;
                string straightnessDetails = "straightness=not-evaluated";
                if (window.TryReadMask(mask, true, false, 0, out bool[] maskCanSpawn))
                    maskArea = CountMaskArea(maskCanSpawn);
                if (!string.IsNullOrWhiteSpace(generated))
                {
                    var generatedDef = AssetDatabase.LoadAssetAtPath<LevelDefinition>(generated);
                    if (generatedDef != null)
                    {
                        chains = CountAuthoredChains(generatedDef.authoredLevel);
                        straightRejected = IsStraightDominatedShape(generatedDef.authoredLevel, out straightnessDetails);
                        if (TryBuildBoardFromSeed(generatedDef, out BoardState generatedBoard))
                        {
                            finalFill = CountArrowTiles(generatedBoard);
                            ratio = maskArea > 0 ? (float)finalFill / maskArea : 0f;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(generated) && !straightRejected)
                    generatedPaths.Add(generated);

                report.Add($"    Generated={!string.IsNullOrWhiteSpace(generated)} | QualityRejected={straightRejected} | Source={chosen.SourceSeedPath} | MaskArea={maskArea} | FinalFill={finalFill} | Ratio={ratio:0.000} | Chains={chains} | {straightnessDetails} | Raw={window._latestRawClipPath} | Final={generated}");
                WriteReport(kShapeExperimentReportPath, report);
            }

            if (!TrySyncShapeExperimentPreviewPack(generatedPaths, out LevelPack pack, out string packDetails))
            {
                report.Add($"PackFailed={packDetails}");
                WriteReport(kShapeExperimentReportPath, report);
                Debug.LogError($"[SeedMaskPatch] Shape Experiment Preview pack failed: {packDetails}");
                return;
            }

            report.Add($"PackSynced={kShapeExperimentPreviewPackPath} | {packDetails}");
            AttachLevelPackToDemo(pack, "SeedMaskPatch ShapeExperimentPreview");
            report.Add("DemoAttached=True");
            WriteReport(kShapeExperimentReportPath, report);

            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] Shape Experiment Preview finished. levels={generatedPaths.Count}, pack={kShapeExperimentPreviewPackPath}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Run Early Symbol Preview Pack")]
        public static void RunEarlySymbolPreviewPack()
        {
            ShapeExperimentMaskBuilder.CreateReadableShapePreviewMasks();

            string[] maskPaths =
            {
                kOriginalStarMaskPath,
                "Assets/ArrowMagic/Masks/ShapeReadablePreview/MaskReadable_19x19-PlusBold.png",
                "Assets/ArrowMagic/Masks/ShapeReadablePreview/MaskReadable_21x21-XBold.png",
                "Assets/ArrowMagic/Masks/ShapeReadablePreview/MaskReadable_20x22-LightningSmall.png"
            };

            var report = new List<string>
            {
                "SeedMaskPatch Early Symbol Preview Pack",
                $"SourcePackFolder={kShapeExperimentSourcePackFolder}",
                $"CandidateOutput={kShapeExperimentCandidateFolder}",
                $"Pack={kEarlySymbolPreviewPackPath}",
                "Mode=EarlySuperSymbolMaskCropRepairOneFinalPerMask",
                "Design=original 19 star baseline plus 3 strong early symbols"
            };

            var sourceSeeds = LoadShapeExperimentSourceSeeds(report);
            report.Add($"LoadedSourceSeeds={sourceSeeds.Count}");
            if (sourceSeeds.Count == 0)
            {
                WriteReport(kEarlySymbolPreviewReportPath, report);
                Debug.LogError("[SeedMaskPatch] Early Symbol Preview failed: no source seeds found.");
                return;
            }

            EnsureFolderExists(kShapeExperimentCandidateFolder);
            EnsureFolderExists(kShapeExperimentPackFolder);

            var generatedPaths = new List<string>();
            for (int i = 0; i < maskPaths.Length; i++)
            {
                string maskPath = maskPaths[i];
                var mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);
                if (mask == null)
                {
                    report.Add($"[{i + 1}] Mask={maskPath} | Missing");
                    WriteReport(kEarlySymbolPreviewReportPath, report);
                    continue;
                }

                var window = CreateInstance<SeedMaskPatchWindow>();
                ApplyMenuDefaultRuntimeSettings(window);
                window._mask = mask;
                window._placementMode = PlacementMode.Center;
                window._autoIfCenterFails = false;
                window._patchOutputMode = PatchOutputMode.GreedyRescue;
                window._inflateMaskForSolvability = false;
                window._maskInflationPasses = 0;
                window._adaptiveHealFillFallback = false;
                window._maskHealFillRatio = 1f;
                window._maskHealFillRatioFloor = 1f;
                window._maskHealFillFallbackSteps = 0;
                window._maxGreedyMovesMultiplier = kRepairExperimentGreedyMultiplier;
                window._fallbackCleanupLoops = kRepairExperimentFallbackLoops;
                window._greedyTrimPerChain = kRepairExperimentTrimPerChain;
                window._preGreedyGeometryFixPasses = 10;
                window._allowGreedyFullChainRemoval = false;
                window._allowGreedyFallbackChainClear = false;
                window._candidateTopCount = 3;
                window._candidatePreEvalLimit = 64;
                window._candidatePreEvalTimeoutMs = 60000;
                window._candidateMinFillRatio = 0.90f;
                window._outputFolder = kShapeExperimentCandidateFolder;
                window.EnsureOutputFolder();
                window._outputPrefix = $"earlysym_{i + 1:00}_{SanitizeName(Path.GetFileNameWithoutExtension(maskPath)).ToLowerInvariant()}";

                MatchShapeExperimentSeeds(window, mask, sourceSeeds, 120, out string matchSummary);
                report.Add($"[{i + 1}] Mask={maskPath} | {matchSummary}");

                Debug.Log($"[SeedMaskPatch] Early Symbol Preview match/eval start {i + 1}/{maskPaths.Length}: {maskPath}");
                window.RunCandidatePreEval();

                int previewCount = Math.Min(3, window._candidateResults.Count);
                for (int r = 0; r < previewCount; r++)
                    report.Add($"    Top{r + 1}: {BuildCandidateResultLine(r + 1, window._candidateResults[r])}");

                SeedCandidateResult chosen = null;
                for (int r = 0; r < window._candidateResults.Count; r++)
                {
                    var candidate = window._candidateResults[r];
                    if (candidate != null && candidate.IsFeasible && candidate.SourceSeed != null)
                    {
                        chosen = candidate;
                        break;
                    }
                }

                if (chosen == null)
                {
                    report.Add("    Generated=False | Reason=No feasible matched candidate");
                    WriteReport(kEarlySymbolPreviewReportPath, report);
                    continue;
                }

                string generated = window.ProcessSingle(chosen.SourceSeed, mask);
                int maskArea = 0;
                int finalFill = 0;
                float ratio = 0f;
                int chains = 0;
                bool straightRejected = false;
                string straightnessDetails = "straightness=not-evaluated";
                if (window.TryReadMask(mask, true, false, 0, out bool[] maskCanSpawn))
                    maskArea = CountMaskArea(maskCanSpawn);
                if (!string.IsNullOrWhiteSpace(generated))
                {
                    var generatedDef = AssetDatabase.LoadAssetAtPath<LevelDefinition>(generated);
                    if (generatedDef != null)
                    {
                        chains = CountAuthoredChains(generatedDef.authoredLevel);
                        straightRejected = IsStraightDominatedShape(generatedDef.authoredLevel, out straightnessDetails);
                        if (TryBuildBoardFromSeed(generatedDef, out BoardState generatedBoard))
                        {
                            finalFill = CountArrowTiles(generatedBoard);
                            ratio = maskArea > 0 ? (float)finalFill / maskArea : 0f;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(generated) && !straightRejected)
                    generatedPaths.Add(generated);

                report.Add($"    Generated={!string.IsNullOrWhiteSpace(generated)} | QualityRejected={straightRejected} | Source={chosen.SourceSeedPath} | MaskArea={maskArea} | FinalFill={finalFill} | Ratio={ratio:0.000} | Chains={chains} | {straightnessDetails} | Raw={window._latestRawClipPath} | Final={generated}");
                WriteReport(kEarlySymbolPreviewReportPath, report);
            }

            if (!TrySyncLevelPack(generatedPaths, kEarlySymbolPreviewPackPath, "EarlySymbolPreviewPack", $"Early Symbol Preview ({generatedPaths.Count})", out LevelPack pack, out string packDetails))
            {
                report.Add($"PackFailed={packDetails}");
                WriteReport(kEarlySymbolPreviewReportPath, report);
                Debug.LogError($"[SeedMaskPatch] Early Symbol Preview pack failed: {packDetails}");
                return;
            }

            report.Add($"PackSynced={kEarlySymbolPreviewPackPath} | {packDetails}");
            AttachLevelPackToDemo(pack, "SeedMaskPatch EarlySymbolPreview");
            report.Add("DemoAttached=True");
            WriteReport(kEarlySymbolPreviewReportPath, report);

            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] Early Symbol Preview finished. levels={generatedPaths.Count}, pack={kEarlySymbolPreviewPackPath}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Run Animal Shape Preview Pack")]
        public static void RunAnimalShapePreviewPack()
        {
            ShapeExperimentMaskBuilder.CreateAnimalShapePreviewMasks();

            string[] maskPaths =
            {
                "Assets/ArrowMagic/Masks/ShapeAnimalPreview/MaskAnimal_42x42-DogSitSide.png",
                "Assets/ArrowMagic/Masks/ShapeAnimalPreview/MaskAnimal_40x44-CatSitSide.png",
                "Assets/ArrowMagic/Masks/ShapeAnimalPreview/MaskAnimal_42x34-DuckSide.png",
                "Assets/ArrowMagic/Masks/ShapeAnimalPreview/MaskAnimal_44x30-FishSideLarge.png"
            };

            var report = new List<string>
            {
                "SeedMaskPatch Animal Shape Preview Pack",
                $"SourcePackFolder={kShapeExperimentSourcePackFolder}",
                $"CandidateOutput={kShapeExperimentCandidateFolder}",
                $"Pack={kAnimalShapePreviewPackPath}",
                "Mode=AnimalShapeMaskCropRepairOneFinalPerMask",
                "Design=fit-to-canvas side-view animal silhouettes; approved short preview set"
            };

            var sourceSeeds = LoadShapeExperimentSourceSeeds(report);
            report.Add($"LoadedSourceSeeds={sourceSeeds.Count}");
            if (sourceSeeds.Count == 0)
            {
                WriteReport(kAnimalShapePreviewReportPath, report);
                Debug.LogError("[SeedMaskPatch] Animal Shape Preview failed: no source seeds found.");
                return;
            }

            EnsureFolderExists(kShapeExperimentCandidateFolder);
            EnsureFolderExists(kShapeExperimentPackFolder);

            var generatedPaths = new List<string>();
            for (int i = 0; i < maskPaths.Length; i++)
            {
                string maskPath = maskPaths[i];
                var mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);
                if (mask == null)
                {
                    report.Add($"[{i + 1}] Mask={maskPath} | Missing");
                    WriteReport(kAnimalShapePreviewReportPath, report);
                    continue;
                }

                var window = CreateInstance<SeedMaskPatchWindow>();
                ApplyMenuDefaultRuntimeSettings(window);
                window._mask = mask;
                window._placementMode = PlacementMode.Center;
                window._autoIfCenterFails = false;
                window._patchOutputMode = PatchOutputMode.GreedyRescue;
                window._inflateMaskForSolvability = false;
                window._maskInflationPasses = 0;
                window._adaptiveHealFillFallback = false;
                window._maskHealFillRatio = 1f;
                window._maskHealFillRatioFloor = 1f;
                window._maskHealFillFallbackSteps = 0;
                window._maxGreedyMovesMultiplier = 8;
                window._fallbackCleanupLoops = 1;
                window._greedyTrimPerChain = 1;
                window._preGreedyGeometryFixPasses = 8;
                window._allowGreedyFullChainRemoval = true;
                window._allowGreedyFallbackChainClear = true;
                window._greedyRepairCandidateEvalCap = 160;
                window._greedyRepairTimeBudgetMs = 7000;
                window._allowGreedyCoreTakeoutRefillRescue = false;
                window._exportRawIfGreedyFails = false;
                window._candidateTopCount = 8;
                window._candidatePreEvalLimit = 32;
                window._candidatePreEvalTimeoutMs = 12000;
                window._candidateMinFillRatio = 0.90f;
                window._outputFolder = kShapeExperimentCandidateFolder;
                window.EnsureOutputFolder();
                window._outputPrefix = $"animalshape_{i + 1:00}_{SanitizeName(Path.GetFileNameWithoutExtension(maskPath)).ToLowerInvariant()}";

                MatchShapeExperimentSeeds(window, mask, sourceSeeds, 48, out string matchSummary);
                report.Add($"[{i + 1}] Mask={maskPath} | {matchSummary}");

                Debug.Log($"[SeedMaskPatch] Animal Shape Preview direct-generate start {i + 1}/{maskPaths.Length}: {maskPath}");

                int previewCount = Math.Min(8, window._matchedSeeds.Count);
                for (int r = 0; r < previewCount; r++)
                    report.Add($"    MatchTop{r + 1}: Source={GetAssetPath(window._matchedSeeds[r])}");

                int maskArea = 0;
                if (window.TryReadMask(mask, true, false, 0, out bool[] maskCanSpawn))
                    maskArea = CountMaskArea(maskCanSpawn);

                bool accepted = false;
                int feasibleAttempts = 0;
                for (int r = 0; r < previewCount; r++)
                {
                    var sourceSeed = window._matchedSeeds[r];
                    if (sourceSeed == null)
                        continue;

                    feasibleAttempts++;
                    string sourcePath = GetAssetPath(sourceSeed);
                    report.Add($"    BeginAttempt{feasibleAttempts}=MatchTop{r + 1} | Source={sourcePath}");
                    WriteReport(kAnimalShapePreviewReportPath, report);

                    string generated = window.ProcessSingle(sourceSeed, mask);
                    int finalFill = 0;
                    float ratio = 0f;
                    int chains = 0;
                    bool straightRejected = false;
                    bool fillRejected = false;
                    string straightnessDetails = "straightness=not-evaluated";
                    if (!string.IsNullOrWhiteSpace(generated))
                    {
                        var generatedDef = AssetDatabase.LoadAssetAtPath<LevelDefinition>(generated);
                        if (generatedDef != null)
                        {
                            chains = CountAuthoredChains(generatedDef.authoredLevel);
                            straightRejected = IsStraightDominatedShape(generatedDef.authoredLevel, out straightnessDetails);
                            if (TryBuildBoardFromSeed(generatedDef, out BoardState generatedBoard))
                            {
                                finalFill = CountArrowTiles(generatedBoard);
                                ratio = maskArea > 0 ? (float)finalFill / maskArea : 0f;
                                fillRejected = maskArea > 0 && ratio < 0.90f;
                            }
                        }
                    }

                    bool qualityRejected = straightRejected || fillRejected;
                    string rejectReason = straightRejected ? "straight-dominated" : fillRejected ? "low-fill" : "none";
                    report.Add($"    Attempt{feasibleAttempts}=MatchTop{r + 1} | Generated={!string.IsNullOrWhiteSpace(generated)} | QualityRejected={qualityRejected} | RejectReason={rejectReason} | Source={sourcePath} | MaskArea={maskArea} | FinalFill={finalFill} | Ratio={ratio:0.000} | Chains={chains} | {straightnessDetails} | Raw={window._latestRawClipPath} | Final={generated}");
                    WriteReport(kAnimalShapePreviewReportPath, report);

                    if (!string.IsNullOrWhiteSpace(generated) && !qualityRejected)
                    {
                        generatedPaths.Add(generated);
                        accepted = true;
                        break;
                    }
                }

                if (feasibleAttempts == 0)
                    report.Add("    Generated=False | Reason=No matched candidate");
                else if (!accepted)
                    report.Add($"    Generated=False | Reason=All matched candidates rejected | Attempts={feasibleAttempts}");
                WriteReport(kAnimalShapePreviewReportPath, report);
            }

            if (!TrySyncLevelPack(generatedPaths, kAnimalShapePreviewPackPath, "AnimalShapePreviewPack", $"Animal Shape Preview ({generatedPaths.Count})", out LevelPack pack, out string packDetails))
            {
                report.Add($"PackFailed={packDetails}");
                WriteReport(kAnimalShapePreviewReportPath, report);
                Debug.LogError($"[SeedMaskPatch] Animal Shape Preview pack failed: {packDetails}");
                return;
            }

            report.Add($"PackSynced={kAnimalShapePreviewPackPath} | {packDetails}");
            AttachLevelPackToDemo(pack, "SeedMaskPatch AnimalShapePreview");
            report.Add("DemoAttached=True");
            WriteReport(kAnimalShapePreviewReportPath, report);

            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] Animal Shape Preview finished. levels={generatedPaths.Count}, pack={kAnimalShapePreviewPackPath}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Run Animal Best Preview Pack")]
        public static void RunAnimalBestPreviewPack()
        {
            ShapeExperimentMaskBuilder.CreateAnimalBestPreviewMasks();

            string[] maskPaths =
            {
                "Assets/ArrowMagic/Masks/ShapeAnimalBestPreview/MaskAnimalBest_32x26-WhaleBoldSide.png",
                "Assets/ArrowMagic/Masks/ShapeAnimalBestPreview/MaskAnimalBest_32x24-TurtleBoldSide.png",
                "Assets/ArrowMagic/Masks/ShapeAnimalBestPreview/MaskAnimalBest_29x22-SnailBoldSide.png"
            };

            var report = new List<string>
            {
                "SeedMaskPatch Animal Best Preview Pack",
                $"SourcePackFolder={kShapeExperimentSourcePackFolder}",
                $"CandidateOutput={kShapeExperimentCandidateFolder}",
                $"Pack={kAnimalBestPreviewPackPath}",
                "Mode=AnimalBestCompactMaskCropRepairOneFinalPerMask",
                "Design=compact cropped animal silhouettes; strong features, low detail, no unused canvas border"
            };

            var sourceSeeds = LoadShapeExperimentSourceSeeds(report);
            report.Add($"LoadedSourceSeeds={sourceSeeds.Count}");
            if (sourceSeeds.Count == 0)
            {
                WriteReport(kAnimalBestPreviewReportPath, report);
                Debug.LogError("[SeedMaskPatch] Animal Best Preview failed: no source seeds found.");
                return;
            }

            EnsureFolderExists(kShapeExperimentCandidateFolder);
            EnsureFolderExists(kShapeExperimentPackFolder);

            var generatedPaths = new List<string>();
            for (int i = 0; i < maskPaths.Length; i++)
            {
                string maskPath = maskPaths[i];
                var mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);
                if (mask == null)
                {
                    report.Add($"[{i + 1}] Mask={maskPath} | Missing");
                    WriteReport(kAnimalBestPreviewReportPath, report);
                    continue;
                }

                var window = CreateInstance<SeedMaskPatchWindow>();
                ApplyMenuDefaultRuntimeSettings(window);
                window._mask = mask;
                window._placementMode = PlacementMode.Center;
                window._autoIfCenterFails = false;
                window._patchOutputMode = PatchOutputMode.GreedyRescue;
                window._inflateMaskForSolvability = false;
                window._maskInflationPasses = 0;
                window._adaptiveHealFillFallback = false;
                window._maskHealFillRatio = 1f;
                window._maskHealFillRatioFloor = 1f;
                window._maskHealFillFallbackSteps = 0;
                window._maxGreedyMovesMultiplier = kRepairExperimentGreedyMultiplier;
                window._fallbackCleanupLoops = kRepairExperimentFallbackLoops;
                window._greedyTrimPerChain = kRepairExperimentTrimPerChain;
                window._preGreedyGeometryFixPasses = 10;
                window._allowGreedyFullChainRemoval = false;
                window._allowGreedyFallbackChainClear = false;
                window._candidateTopCount = 3;
                window._candidatePreEvalLimit = 16;
                window._candidatePreEvalTimeoutMs = 30000;
                window._candidateMinFillRatio = 0.90f;
                window._outputFolder = kShapeExperimentCandidateFolder;
                window.EnsureOutputFolder();
                window._outputPrefix = $"animalbest_{i + 1:00}_{SanitizeName(Path.GetFileNameWithoutExtension(maskPath)).ToLowerInvariant()}";

                MatchShapeExperimentSeeds(window, mask, sourceSeeds, 64, out string matchSummary);
                report.Add($"[{i + 1}] Mask={maskPath} | {matchSummary}");

                Debug.Log($"[SeedMaskPatch] Animal Best Preview direct-generate start {i + 1}/{maskPaths.Length}: {maskPath}");

                int previewCount = Math.Min(3, window._matchedSeeds.Count);
                for (int r = 0; r < previewCount; r++)
                    report.Add($"    MatchTop{r + 1}: Source={GetAssetPath(window._matchedSeeds[r])}");

                int maskArea = 0;
                if (window.TryReadMask(mask, true, false, 0, out bool[] maskCanSpawn))
                    maskArea = CountMaskArea(maskCanSpawn);

                bool accepted = false;
                int feasibleAttempts = 0;
                for (int r = 0; r < previewCount; r++)
                {
                    var sourceSeed = window._matchedSeeds[r];
                    if (sourceSeed == null)
                        continue;

                    feasibleAttempts++;
                    string sourcePath = GetAssetPath(sourceSeed);
                    report.Add($"    BeginAttempt{feasibleAttempts}=MatchTop{r + 1} | Source={sourcePath}");
                    WriteReport(kAnimalBestPreviewReportPath, report);

                    string generated = window.ProcessSingle(sourceSeed, mask);
                    int finalFill = 0;
                    float ratio = 0f;
                    int chains = 0;
                    bool straightRejected = false;
                    bool fillRejected = false;
                    string straightnessDetails = "straightness=not-evaluated";
                    if (!string.IsNullOrWhiteSpace(generated))
                    {
                        var generatedDef = AssetDatabase.LoadAssetAtPath<LevelDefinition>(generated);
                        if (generatedDef != null)
                        {
                            chains = CountAuthoredChains(generatedDef.authoredLevel);
                            straightRejected = IsStraightDominatedShape(generatedDef.authoredLevel, out straightnessDetails);
                            if (TryBuildBoardFromSeed(generatedDef, out BoardState generatedBoard))
                            {
                                finalFill = CountArrowTiles(generatedBoard);
                                ratio = maskArea > 0 ? (float)finalFill / maskArea : 0f;
                                fillRejected = maskArea > 0 && ratio < 0.90f;
                            }
                        }
                    }

                    bool qualityRejected = straightRejected || fillRejected;
                    string rejectReason = straightRejected ? "straight-dominated" : fillRejected ? "low-fill" : "none";
                    report.Add($"    Attempt{feasibleAttempts}=MatchTop{r + 1} | Generated={!string.IsNullOrWhiteSpace(generated)} | QualityRejected={qualityRejected} | RejectReason={rejectReason} | Source={sourcePath} | MaskArea={maskArea} | FinalFill={finalFill} | Ratio={ratio:0.000} | Chains={chains} | {straightnessDetails} | Raw={window._latestRawClipPath} | Final={generated}");
                    WriteReport(kAnimalBestPreviewReportPath, report);

                    if (!string.IsNullOrWhiteSpace(generated) && !qualityRejected)
                    {
                        generatedPaths.Add(generated);
                        accepted = true;
                        break;
                    }
                }

                if (feasibleAttempts == 0)
                    report.Add("    Generated=False | Reason=No matched candidate");
                else if (!accepted)
                    report.Add($"    Generated=False | Reason=All matched candidates rejected | Attempts={feasibleAttempts}");
                WriteReport(kAnimalBestPreviewReportPath, report);
            }

            if (!TrySyncLevelPack(generatedPaths, kAnimalBestPreviewPackPath, "AnimalBestPreviewPack", $"Animal Best Preview ({generatedPaths.Count})", out LevelPack pack, out string packDetails))
            {
                report.Add($"PackFailed={packDetails}");
                WriteReport(kAnimalBestPreviewReportPath, report);
                Debug.LogError($"[SeedMaskPatch] Animal Best Preview pack failed: {packDetails}");
                return;
            }

            report.Add($"PackSynced={kAnimalBestPreviewPackPath} | {packDetails}");
            AttachLevelPackToDemo(pack, "SeedMaskPatch AnimalBestPreview");
            report.Add("DemoAttached=True");
            WriteReport(kAnimalBestPreviewReportPath, report);

            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] Animal Best Preview finished. levels={generatedPaths.Count}, pack={kAnimalBestPreviewPackPath}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Tight Crop Animal Best Preview Pack")]
        public static void TightCropAnimalBestPreviewPack()
        {
            var report = new List<string>
            {
                "SeedMaskPatch Animal Best Tight Crop",
                $"SourcePack={kAnimalBestPreviewPackPath}",
                $"OutputFolder={kAnimalBestTightCropFolder}",
                $"Pack={kAnimalBestPreviewPackPath}",
                "Mode=CropCurrentAnimalBestPackByMaskBoundsThenAttachDemo"
            };

            var sourcePack = AssetDatabase.LoadAssetAtPath<LevelPack>(kAnimalBestPreviewPackPath);
            if (sourcePack?.levels == null || sourcePack.levels.Length == 0)
            {
                report.Add("Failed=no source levels");
                WriteReport(kAnimalBestTightCropReportPath, report);
                Debug.LogError($"[SeedMaskPatch] Animal Best tight crop failed: pack missing or empty: {kAnimalBestPreviewPackPath}");
                return;
            }

            EnsureFolderExists(kAnimalBestTightCropFolder);
            var outputPaths = new List<string>();
            for (int i = 0; i < sourcePack.levels.Length; i++)
            {
                var source = sourcePack.levels[i];
                if (source == null)
                {
                    report.Add($"[{i + 1}] skipped=null");
                    continue;
                }

                if (TryCreateTightCroppedAnimalBestLevel(source, i + 1, out string outputPath, out string details))
                {
                    outputPaths.Add(outputPath);
                    report.Add($"[{i + 1}] ok | {details} | Output={outputPath}");
                }
                else
                {
                    report.Add($"[{i + 1}] failed | {details}");
                }

                WriteReport(kAnimalBestTightCropReportPath, report);
            }

            if (!TrySyncLevelPack(outputPaths, kAnimalBestPreviewPackPath, "AnimalBestPreviewPack", $"Animal Best Preview Tight ({outputPaths.Count})", out LevelPack pack, out string packDetails))
            {
                report.Add($"PackFailed={packDetails}");
                WriteReport(kAnimalBestTightCropReportPath, report);
                Debug.LogError($"[SeedMaskPatch] Animal Best tight crop pack failed: {packDetails}");
                return;
            }

            report.Add($"PackSynced={kAnimalBestPreviewPackPath} | {packDetails}");
            AttachLevelPackToDemo(pack, "SeedMaskPatch AnimalBestTightCrop");
            report.Add("DemoAttached=True");
            WriteReport(kAnimalBestTightCropReportPath, report);

            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] Animal Best tight crop finished. levels={outputPaths.Count}, pack={kAnimalBestPreviewPackPath}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Run Tall Fit Shape Preview Pack")]
        public static void RunTallFitShapePreviewPack()
        {
            ShapeExperimentMaskBuilder.CreateTallFitShapePreviewMasks();

            string[] maskPaths =
            {
                "Assets/ArrowMagic/Masks/ShapeTallFitPreview/MaskTallFit_22x30-RocketMini.png",
                "Assets/ArrowMagic/Masks/ShapeTallFitPreview/MaskTallFit_22x30-ShieldTall.png",
                "Assets/ArrowMagic/Masks/ShapeTallFitPreview/MaskTallFit_20x30-BottleMini.png",
                "Assets/ArrowMagic/Masks/ShapeTallFitPreview/MaskTallFit_22x32-TorchTall.png"
            };

            var report = new List<string>
            {
                "SeedMaskPatch Tall Fit Shape Preview Pack",
                $"SourcePackFolder={kShapeExperimentSourcePackFolder}",
                $"CandidateOutput={kShapeExperimentCandidateFolder}",
                $"Pack={kTallFitShapePreviewPackPath}",
                "Mode=TallFitShapeMaskCropRepairOneFinalPerMask",
                "Design=smaller tall masks whose content bbox nearly equals the canvas; lighter silhouette coverage for Greedy stability"
            };

            var sourceSeeds = LoadShapeExperimentSourceSeeds(report);
            report.Add($"LoadedSourceSeeds={sourceSeeds.Count}");
            if (sourceSeeds.Count == 0)
            {
                WriteReport(kTallFitShapePreviewReportPath, report);
                Debug.LogError("[SeedMaskPatch] Tall Fit Shape Preview failed: no source seeds found.");
                return;
            }

            var tallFitSourceSeeds = new List<LevelDefinition>(sourceSeeds.Count);
            for (int i = 0; i < sourceSeeds.Count; i++)
            {
                var seed = sourceSeeds[i];
                string seedPath = GetAssetPath(seed);
                if (string.IsNullOrWhiteSpace(seedPath)
                    || seedPath.IndexOf("CompositeSeedVariantsABPlayabilityCandidates300", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    continue;
                }

                tallFitSourceSeeds.Add(seed);
            }

            if (tallFitSourceSeeds.Count >= 16)
            {
                report.Add($"TallFitSourceFilter=prefer_non_composite_play_candidates | Kept={tallFitSourceSeeds.Count}/{sourceSeeds.Count}");
                sourceSeeds = tallFitSourceSeeds;
            }
            else
            {
                report.Add($"TallFitSourceFilter=disabled_insufficient_filtered_sources | Kept={tallFitSourceSeeds.Count}/{sourceSeeds.Count}");
            }

            EnsureFolderExists(kShapeExperimentCandidateFolder);
            EnsureFolderExists(kShapeExperimentPackFolder);

            var generatedPaths = new List<string>();
            for (int i = 0; i < maskPaths.Length; i++)
            {
                string maskPath = maskPaths[i];
                var mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);
                if (mask == null)
                {
                    report.Add($"[{i + 1}] Mask={maskPath} | Missing");
                    WriteReport(kTallFitShapePreviewReportPath, report);
                    continue;
                }

                var window = CreateInstance<SeedMaskPatchWindow>();
                ApplyMenuDefaultRuntimeSettings(window);
                window._mask = mask;
                window._placementMode = PlacementMode.Center;
                window._autoIfCenterFails = false;
                window._patchOutputMode = PatchOutputMode.GreedyRescue;
                window._inflateMaskForSolvability = false;
                window._maskInflationPasses = 0;
                window._adaptiveHealFillFallback = false;
                window._maskHealFillRatio = 1f;
                window._maskHealFillRatioFloor = 1f;
                window._maskHealFillFallbackSteps = 0;
                window._maxGreedyMovesMultiplier = 8;
                window._fallbackCleanupLoops = 2;
                window._greedyTrimPerChain = 3;
                window._preGreedyGeometryFixPasses = 8;
                window._allowGreedyFullChainRemoval = true;
                window._allowGreedyFallbackChainClear = true;
                window._greedyRepairCandidateEvalCap = 1200;
                window._greedyRepairTimeBudgetMs = 24000;
                window._allowGreedyCoreTakeoutRefillRescue = true;
                window._exportRawIfGreedyFails = false;
                window._candidateTopCount = 18;
                window._candidatePreEvalLimit = 64;
                window._candidatePreEvalTimeoutMs = 12000;
                window._candidateMinFillRatio = 0.84f;
                window._outputFolder = kShapeExperimentCandidateFolder;
                window.EnsureOutputFolder();
                window._outputPrefix = $"tallfit_{i + 1:00}_{SanitizeName(Path.GetFileNameWithoutExtension(maskPath)).ToLowerInvariant()}";

                MatchShapeExperimentSeeds(window, mask, sourceSeeds, 64, out string matchSummary);
                report.Add($"[{i + 1}] Mask={maskPath} | {matchSummary}");

                Debug.Log($"[SeedMaskPatch] Tall Fit Shape Preview direct-generate start {i + 1}/{maskPaths.Length}: {maskPath}");

                var attemptSeeds = new List<LevelDefinition>();
                var attemptRanks = new List<int>();
                var seenAttempts = new HashSet<LevelDefinition>();

                void AddAttemptSeed(int index)
                {
                    if (index < 0 || index >= window._matchedSeeds.Count)
                        return;

                    var candidate = window._matchedSeeds[index];
                    if (candidate == null || !seenAttempts.Add(candidate))
                        return;

                    attemptSeeds.Add(candidate);
                    attemptRanks.Add(index + 1);
                }

                for (int r = 0; r < Math.Min(10, window._matchedSeeds.Count); r++)
                    AddAttemptSeed(r);

                int[] spreadRanks = { 12, 16, 20, 24, 32, 40, 48, 56, 64 };
                for (int r = 0; r < spreadRanks.Length && attemptSeeds.Count < 18; r++)
                    AddAttemptSeed(spreadRanks[r] - 1);

                for (int r = 0; r < window._matchedSeeds.Count && attemptSeeds.Count < 18; r++)
                    AddAttemptSeed(r);

                int previewCount = attemptSeeds.Count;
                for (int r = 0; r < previewCount; r++)
                    report.Add($"    MatchPick{r + 1}=Rank{attemptRanks[r]} | Source={GetAssetPath(attemptSeeds[r])}");

                int maskArea = 0;
                if (window.TryReadMask(mask, true, false, 0, out bool[] maskCanSpawn))
                    maskArea = CountMaskArea(maskCanSpawn);

                bool accepted = false;
                int feasibleAttempts = 0;
                for (int r = 0; r < previewCount; r++)
                {
                    var sourceSeed = attemptSeeds[r];
                    if (sourceSeed == null)
                        continue;

                    feasibleAttempts++;
                    string sourcePath = GetAssetPath(sourceSeed);
                    report.Add($"    BeginAttempt{feasibleAttempts}=Rank{attemptRanks[r]} | Source={sourcePath}");
                    WriteReport(kTallFitShapePreviewReportPath, report);

                    string generated = window.ProcessSingle(sourceSeed, mask);
                    int finalFill = 0;
                    float ratio = 0f;
                    int chains = 0;
                    bool straightRejected = false;
                    bool fillRejected = false;
                    string straightnessDetails = "straightness=not-evaluated";
                    if (!string.IsNullOrWhiteSpace(generated))
                    {
                        var generatedDef = AssetDatabase.LoadAssetAtPath<LevelDefinition>(generated);
                        if (generatedDef != null)
                        {
                            chains = CountAuthoredChains(generatedDef.authoredLevel);
                            straightRejected = IsStraightDominatedShape(generatedDef.authoredLevel, out straightnessDetails);
                            if (TryBuildBoardFromSeed(generatedDef, out BoardState generatedBoard))
                            {
                                finalFill = CountArrowTiles(generatedBoard);
                                ratio = maskArea > 0 ? (float)finalFill / maskArea : 0f;
                                fillRejected = maskArea > 0 && ratio < window._candidateMinFillRatio;
                            }
                        }
                    }

                    bool qualityRejected = straightRejected || fillRejected;
                    string rejectReason = straightRejected ? "straight-dominated" : fillRejected ? "low-fill" : "none";
                    report.Add($"    Attempt{feasibleAttempts}=Rank{attemptRanks[r]} | Generated={!string.IsNullOrWhiteSpace(generated)} | QualityRejected={qualityRejected} | RejectReason={rejectReason} | Source={sourcePath} | MaskArea={maskArea} | FinalFill={finalFill} | Ratio={ratio:0.000} | MinRatio={window._candidateMinFillRatio:0.00} | Chains={chains} | {straightnessDetails} | Raw={window._latestRawClipPath} | Final={generated}");
                    WriteReport(kTallFitShapePreviewReportPath, report);

                    if (!string.IsNullOrWhiteSpace(generated) && !qualityRejected)
                    {
                        generatedPaths.Add(generated);
                        accepted = true;
                        break;
                    }
                }

                if (feasibleAttempts == 0)
                    report.Add("    Generated=False | Reason=No matched candidate");
                else if (!accepted)
                    report.Add($"    Generated=False | Reason=All matched candidates rejected | Attempts={feasibleAttempts}");
                WriteReport(kTallFitShapePreviewReportPath, report);
            }

            if (!TrySyncLevelPack(generatedPaths, kTallFitShapePreviewPackPath, "TallFitShapePreviewPack", $"Tall Fit Shape Preview ({generatedPaths.Count})", out LevelPack pack, out string packDetails))
            {
                report.Add($"PackFailed={packDetails}");
                WriteReport(kTallFitShapePreviewReportPath, report);
                Debug.LogError($"[SeedMaskPatch] Tall Fit Shape Preview pack failed: {packDetails}");
                return;
            }

            report.Add($"PackSynced={kTallFitShapePreviewPackPath} | {packDetails}");
            AttachLevelPackToDemo(pack, "SeedMaskPatch TallFitShapePreview");
            report.Add("DemoAttached=True");
            WriteReport(kTallFitShapePreviewReportPath, report);

            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] Tall Fit Shape Preview finished. levels={generatedPaths.Count}, pack={kTallFitShapePreviewPackPath}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Archive Tall Fit Early Shape Keep Pack")]
        public static void ArchiveTallFitShapePreviewAsEarlyKeepPack()
        {
            var report = new List<string>
            {
                "SeedMaskPatch Tall Fit Early Shape Keep Archive",
                $"SourcePack={kTallFitShapePreviewPackPath}",
                $"KeepLevelFolder={kTallFitEarlyKeepLevelFolder}",
                $"KeepMaskFolder={kTallFitEarlyKeepMaskFolder}",
                $"KeepPack={kTallFitEarlyKeepPackPath}",
                "Usage=early-stage special-shape crop+repair samples; later Shape production should target higher chain counts"
            };

            var sourcePack = AssetDatabase.LoadAssetAtPath<LevelPack>(kTallFitShapePreviewPackPath);
            if (sourcePack == null || sourcePack.levels == null || sourcePack.levels.Length == 0)
            {
                report.Add("ArchiveFailed=source pack missing or empty");
                WriteReport(kTallFitEarlyKeepReportPath, report);
                Debug.LogError("[SeedMaskPatch] Tall Fit Early Shape Keep archive failed: source pack missing or empty.");
                return;
            }

            EnsureFolderExists(kTallFitEarlyKeepLevelFolder);
            EnsureFolderExists(kTallFitEarlyKeepMaskFolder);
            EnsureFolderExists(Path.GetDirectoryName(kTallFitEarlyKeepPackPath)?.Replace("\\", "/"));

            string[] maskPaths =
            {
                "Assets/ArrowMagic/Masks/ShapeTallFitPreview/MaskTallFit_22x30-RocketMini.png",
                "Assets/ArrowMagic/Masks/ShapeTallFitPreview/MaskTallFit_22x30-ShieldTall.png",
                "Assets/ArrowMagic/Masks/ShapeTallFitPreview/MaskTallFit_20x30-BottleMini.png",
                "Assets/ArrowMagic/Masks/ShapeTallFitPreview/MaskTallFit_22x32-TorchTall.png"
            };

            for (int i = 0; i < maskPaths.Length; i++)
            {
                string sourceMaskPath = maskPaths[i];
                string destinationPath = $"{kTallFitEarlyKeepMaskFolder}/{Path.GetFileName(sourceMaskPath)}";
                bool copied = TryCopyAssetIfMissing(sourceMaskPath, destinationPath, out string copyDetails);
                report.Add($"Mask{i + 1}={sourceMaskPath} -> {destinationPath} | ok={copied} | {copyDetails}");
            }

            string[] keepNames = { "RocketMini", "ShieldTall", "BottleMini", "TorchTall" };
            var keptLevelPaths = new List<string>();
            for (int i = 0; i < sourcePack.levels.Length; i++)
            {
                var sourceLevel = sourcePack.levels[i];
                string sourceLevelPath = GetAssetPath(sourceLevel);
                if (string.IsNullOrWhiteSpace(sourceLevelPath))
                {
                    report.Add($"Level{i + 1}=missing source reference");
                    continue;
                }

                string keepName = i < keepNames.Length ? keepNames[i] : $"Level{i + 1:00}";
                string destinationPath = $"{kTallFitEarlyKeepLevelFolder}/early_shape_keep_{i + 1:00}_{SanitizeName(keepName)}.asset";
                bool copied = TryCopyAssetIfMissing(sourceLevelPath, destinationPath, out string copyDetails);
                report.Add($"Level{i + 1}={sourceLevelPath} -> {destinationPath} | ok={copied} | {copyDetails}");

                if (copied && AssetDatabase.LoadAssetAtPath<LevelDefinition>(destinationPath) != null)
                    keptLevelPaths.Add(destinationPath);
            }

            if (!TrySyncLevelPack(keptLevelPaths, kTallFitEarlyKeepPackPath, "TallFitEarlyShapeKeepPack", $"Tall Fit Early Shape Keep ({keptLevelPaths.Count})", out LevelPack pack, out string packDetails))
            {
                report.Add($"PackFailed={packDetails}");
                WriteReport(kTallFitEarlyKeepReportPath, report);
                Debug.LogError($"[SeedMaskPatch] Tall Fit Early Shape Keep pack failed: {packDetails}");
                return;
            }

            report.Add($"PackSynced={kTallFitEarlyKeepPackPath} | {packDetails}");
            AttachLevelPackToDemo(pack, "SeedMaskPatch TallFitEarlyShapeKeep");
            report.Add("DemoAttached=True");
            WriteReport(kTallFitEarlyKeepReportPath, report);

            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] Tall Fit Early Shape Keep archive finished. levels={keptLevelPaths.Count}, pack={kTallFitEarlyKeepPackPath}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Archive Retrospective Early Shape Keeps")]
        public static void ArchiveRetrospectiveEarlyShapeKeeps()
        {
            var report = new List<string>
            {
                "SeedMaskPatch Retrospective Early Shape Keep Archive",
                $"KeepLevelFolder={kTallFitEarlyKeepLevelFolder}",
                $"KeepMaskFolder={kTallFitEarlyKeepMaskFolder}",
                $"Pack={kShapeEarlyKeepRetrospectivePackPath}",
                "Scope=confirmed early/special-shape keeps from previous visual review",
                "Excluded=EarlySymbolPreviewPack is noted but not archived because some levels were called out as straight-line dominated",
                "Excluded=AnimalShapePreview level 1/2 dog/cat were explicitly abandoned in visual review"
            };

            EnsureFolderExists(kTallFitEarlyKeepLevelFolder);
            EnsureFolderExists(kTallFitEarlyKeepMaskFolder);
            EnsureFolderExists(Path.GetDirectoryName(kShapeEarlyKeepRetrospectivePackPath)?.Replace("\\", "/"));

            var keptLevelPaths = new List<string>();
            AddExistingPackLevelsToKeepArchive(kTallFitEarlyKeepPackPath, "current tall-fit early keep", keptLevelPaths, report);

            ArchiveConfirmedKeep(
                "OriginalStar",
                "Original Star baseline; user said original star result was ok",
                "Assets/ArrowMagic/SOData/Levels/ShapeExperiment/Candidates/original_star_mask_preview_r1_outer_strong_168_r1_289_a985_Mask_19x19-Star_20260610_121712.asset",
                "Assets/ArrowMagic/Masks/Mask_19x19-Star.png",
                "early_shape_keep_05_OriginalStar.asset",
                keptLevelPaths,
                report);

            ArchiveConfirmedKeep(
                "ReadableStarBoldLarge",
                "Readable preview level 4; user said the last two were usable",
                "Assets/ArrowMagic/SOData/Levels/ShapeExperiment/Candidates/readableshape_04_maskreadable_28x28-starboldlarge_r2_outer_strong_126_r2_232_a845_MaskReadable_28x28-StarBoldLarge_20260610_123759.asset",
                "Assets/ArrowMagic/Masks/ShapeReadablePreview/MaskReadable_28x28-StarBoldLarge.png",
                "early_shape_keep_06_ReadableStarBoldLarge.asset",
                keptLevelPaths,
                report);

            ArchiveConfirmedKeep(
                "ReadableHouseBold",
                "Readable preview level 5; user said the last two were usable",
                "Assets/ArrowMagic/SOData/Levels/ShapeExperiment/Candidates/readableshape_05_maskreadable_28x30-housebold_r2_outer_strong_126_r2_232_a845_MaskReadable_28x30-HouseBold_20260610_124013.asset",
                "Assets/ArrowMagic/Masks/ShapeReadablePreview/MaskReadable_28x30-HouseBold.png",
                "early_shape_keep_07_ReadableHouseBold.asset",
                keptLevelPaths,
                report);

            ArchiveConfirmedKeep(
                "AnimalDuckSide",
                "Animal preview level 3; user said 3 and 4 can be kept",
                "Assets/ArrowMagic/SOData/Levels/ShapeExperiment/Candidates/animalshape_03_maskanimal_42x34-duckside_r2_outer_strong_006_r2_008_a409_MaskAnimal_42x34-DuckSide_20260610_182027.asset",
                "Assets/ArrowMagic/Masks/ShapeAnimalPreview/MaskAnimal_42x34-DuckSide.png",
                "early_shape_keep_08_AnimalDuckSide.asset",
                keptLevelPaths,
                report);

            ArchiveConfirmedKeep(
                "AnimalFishSideLarge",
                "Animal preview level 4; user said 3 and 4 can be kept",
                "Assets/ArrowMagic/SOData/Levels/ShapeExperiment/Candidates/animalshape_04_maskanimal_44x30-fishsidelarge_r1_outer_strong_006_r1_008_a409_MaskAnimal_44x30-FishSideLarge_20260610_182927.asset",
                "Assets/ArrowMagic/Masks/ShapeAnimalPreview/MaskAnimal_44x30-FishSideLarge.png",
                "early_shape_keep_09_AnimalFishSideLarge.asset",
                keptLevelPaths,
                report);

            ArchiveConfirmedKeep(
                "AnimalBestWhaleTight",
                "AnimalBest tight crop; user said this batch can be kept",
                "Assets/ArrowMagic/SOData/Levels/ShapeExperiment/Candidates/TightCrop/animalbest_tight_01_maskanimalbest_32x28-whaleboldside_32x26.asset",
                "Assets/ArrowMagic/Masks/ShapeAnimalBestPreview/MaskAnimalBest_32x26-WhaleBoldSide.png",
                "early_shape_keep_10_AnimalBestWhaleTight.asset",
                keptLevelPaths,
                report);

            ArchiveConfirmedKeep(
                "AnimalBestTurtleTight",
                "AnimalBest tight crop; user said this batch can be kept",
                "Assets/ArrowMagic/SOData/Levels/ShapeExperiment/Candidates/TightCrop/animalbest_tight_02_maskanimalbest_32x28-turtleboldside_32x24.asset",
                "Assets/ArrowMagic/Masks/ShapeAnimalBestPreview/MaskAnimalBest_32x24-TurtleBoldSide.png",
                "early_shape_keep_11_AnimalBestTurtleTight.asset",
                keptLevelPaths,
                report);

            ArchiveConfirmedKeep(
                "AnimalBestSnailTight",
                "AnimalBest tight crop; user said this batch can be kept",
                "Assets/ArrowMagic/SOData/Levels/ShapeExperiment/Candidates/TightCrop/animalbest_tight_03_maskanimalbest_30x28-snailboldside_29x22.asset",
                "Assets/ArrowMagic/Masks/ShapeAnimalBestPreview/MaskAnimalBest_29x22-SnailBoldSide.png",
                "early_shape_keep_12_AnimalBestSnailTight.asset",
                keptLevelPaths,
                report);

            report.Add("NeedsReview=EarlySymbolPreviewPack levels 1-4; visual comment mentioned straight-line dominated cases, so not archived");
            report.Add("NeedsReview=ReadableShapePreview levels 2-3; user said 2/3 were too ordinary/small, so not archived");
            report.Add("Rejected=AnimalShapePreview levels 1-2 dog/cat; user said 1/2 abandon");

            if (!TrySyncLevelPack(keptLevelPaths, kShapeEarlyKeepRetrospectivePackPath, "ShapeEarlyKeepRetrospectivePack", $"Shape Early Keep Retrospective ({keptLevelPaths.Count})", out LevelPack pack, out string packDetails))
            {
                report.Add($"PackFailed={packDetails}");
                WriteReport(kShapeEarlyKeepRetrospectiveReportPath, report);
                Debug.LogError($"[SeedMaskPatch] Shape Early Keep Retrospective pack failed: {packDetails}");
                return;
            }

            report.Add($"PackSynced={kShapeEarlyKeepRetrospectivePackPath} | {packDetails}");
            AttachLevelPackToDemo(pack, "SeedMaskPatch ShapeEarlyKeepRetrospective");
            report.Add("DemoAttached=True");
            WriteReport(kShapeEarlyKeepRetrospectiveReportPath, report);

            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] Shape Early Keep Retrospective archive finished. levels={keptLevelPaths.Count}, pack={kShapeEarlyKeepRetrospectivePackPath}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Run Animal Shape Followup Pack")]
        public static void RunAnimalShapeFollowupPack()
        {
            ShapeExperimentMaskBuilder.CreateAnimalShapePreviewMasks();

            string[] maskPaths =
            {
                "Assets/ArrowMagic/Masks/ShapeAnimalPreview/MaskAnimal_42x34-DuckSide.png",
                "Assets/ArrowMagic/Masks/ShapeAnimalPreview/MaskAnimal_44x30-FishSideLarge.png",
                "Assets/ArrowMagic/Masks/ShapeAnimalPreview/MaskAnimal_44x38-ElephantSide.png",
                "Assets/ArrowMagic/Masks/ShapeAnimalPreview/MaskAnimal_34x46-BunnyTall.png"
            };

            var report = new List<string>
            {
                "SeedMaskPatch Animal Shape Followup Pack",
                $"SourcePackFolder={kShapeExperimentSourcePackFolder}",
                $"CandidateOutput={kShapeExperimentCandidateFolder}",
                $"Pack={kAnimalShapePreviewPackPath}",
                "Mode=AnimalShapeSideRemainderFollowup",
                "Design=continue side-view animal silhouettes; run duck/fish/elephant first, bunny last"
            };

            var sourceSeeds = LoadShapeExperimentSourceSeeds(report);
            report.Add($"LoadedSourceSeeds={sourceSeeds.Count}");
            if (sourceSeeds.Count == 0)
            {
                WriteReport(kAnimalShapeFollowupReportPath, report);
                Debug.LogError("[SeedMaskPatch] Animal Shape Followup failed: no source seeds found.");
                return;
            }

            var generatedPaths = new List<string>();
            if (TryCollectAcceptedFinalLevelPathsFromReport(kAnimalShapePreviewReportPath, generatedPaths, report))
                report.Add($"ExistingAcceptedFromMain={generatedPaths.Count}");
            else
                report.Add("ExistingAcceptedFromMain=0");

            EnsureFolderExists(kShapeExperimentCandidateFolder);
            EnsureFolderExists(kShapeExperimentPackFolder);

            for (int i = 0; i < maskPaths.Length; i++)
            {
                string maskPath = maskPaths[i];
                var mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);
                if (mask == null)
                {
                    report.Add($"[{i + 1}] Mask={maskPath} | Missing");
                    WriteReport(kAnimalShapeFollowupReportPath, report);
                    continue;
                }

                var window = CreateInstance<SeedMaskPatchWindow>();
                ApplyMenuDefaultRuntimeSettings(window);
                window._mask = mask;
                window._placementMode = PlacementMode.Center;
                window._autoIfCenterFails = false;
                window._patchOutputMode = PatchOutputMode.GreedyRescue;
                window._inflateMaskForSolvability = false;
                window._maskInflationPasses = 0;
                window._adaptiveHealFillFallback = false;
                window._maskHealFillRatio = 1f;
                window._maskHealFillRatioFloor = 1f;
                window._maskHealFillFallbackSteps = 0;
                window._maxGreedyMovesMultiplier = kRepairExperimentGreedyMultiplier;
                window._fallbackCleanupLoops = kRepairExperimentFallbackLoops;
                window._greedyTrimPerChain = kRepairExperimentTrimPerChain;
                window._preGreedyGeometryFixPasses = 10;
                window._allowGreedyFullChainRemoval = false;
                window._allowGreedyFallbackChainClear = false;
                window._candidateTopCount = 3;
                window._candidatePreEvalLimit = 32;
                window._candidatePreEvalTimeoutMs = 60000;
                window._candidateMinFillRatio = 0.90f;
                window._outputFolder = kShapeExperimentCandidateFolder;
                window.EnsureOutputFolder();
                window._outputPrefix = $"animalfollowup_{i + 1:00}_{SanitizeName(Path.GetFileNameWithoutExtension(maskPath)).ToLowerInvariant()}";

                MatchShapeExperimentSeeds(window, mask, sourceSeeds, 80, out string matchSummary);
                report.Add($"[{i + 1}] Mask={maskPath} | {matchSummary}");

                Debug.Log($"[SeedMaskPatch] Animal Shape Followup match/eval start {i + 1}/{maskPaths.Length}: {maskPath}");
                window.RunCandidatePreEval();

                int previewCount = Math.Min(3, window._candidateResults.Count);
                for (int r = 0; r < previewCount; r++)
                    report.Add($"    Top{r + 1}: {BuildCandidateResultLine(r + 1, window._candidateResults[r])}");

                int maskArea = 0;
                if (window.TryReadMask(mask, true, false, 0, out bool[] maskCanSpawn))
                    maskArea = CountMaskArea(maskCanSpawn);

                bool accepted = false;
                int feasibleAttempts = 0;
                for (int r = 0; r < window._candidateResults.Count; r++)
                {
                    var candidate = window._candidateResults[r];
                    if (candidate == null || !candidate.IsFeasible || candidate.SourceSeed == null)
                        continue;

                    feasibleAttempts++;
                    report.Add($"    BeginAttempt{feasibleAttempts}=Top{r + 1} | Source={candidate.SourceSeedPath}");
                    WriteReport(kAnimalShapeFollowupReportPath, report);

                    string generated = window.ProcessSingle(candidate.SourceSeed, mask);
                    int finalFill = 0;
                    float ratio = 0f;
                    int chains = 0;
                    bool straightRejected = false;
                    bool fillRejected = false;
                    string straightnessDetails = "straightness=not-evaluated";
                    if (!string.IsNullOrWhiteSpace(generated))
                    {
                        var generatedDef = AssetDatabase.LoadAssetAtPath<LevelDefinition>(generated);
                        if (generatedDef != null)
                        {
                            chains = CountAuthoredChains(generatedDef.authoredLevel);
                            straightRejected = IsStraightDominatedShape(generatedDef.authoredLevel, out straightnessDetails);
                            if (TryBuildBoardFromSeed(generatedDef, out BoardState generatedBoard))
                            {
                                finalFill = CountArrowTiles(generatedBoard);
                                ratio = maskArea > 0 ? (float)finalFill / maskArea : 0f;
                                fillRejected = maskArea > 0 && ratio < 0.90f;
                            }
                        }
                    }

                    bool qualityRejected = straightRejected || fillRejected;
                    string rejectReason = straightRejected ? "straight-dominated" : fillRejected ? "low-fill" : "none";
                    report.Add($"    Attempt{feasibleAttempts}=Top{r + 1} | Generated={!string.IsNullOrWhiteSpace(generated)} | QualityRejected={qualityRejected} | RejectReason={rejectReason} | Source={candidate.SourceSeedPath} | MaskArea={maskArea} | FinalFill={finalFill} | Ratio={ratio:0.000} | Chains={chains} | {straightnessDetails} | Raw={window._latestRawClipPath} | Final={generated}");
                    WriteReport(kAnimalShapeFollowupReportPath, report);

                    if (!string.IsNullOrWhiteSpace(generated) && !qualityRejected)
                    {
                        generatedPaths.Add(generated);
                        accepted = true;
                        break;
                    }
                }

                if (feasibleAttempts == 0)
                    report.Add("    Generated=False | Reason=No feasible matched candidate");
                else if (!accepted)
                    report.Add($"    Generated=False | Reason=All feasible candidates rejected | Attempts={feasibleAttempts}");
                WriteReport(kAnimalShapeFollowupReportPath, report);
            }

            if (!TrySyncLevelPack(generatedPaths, kAnimalShapePreviewPackPath, "AnimalShapePreviewPack", $"Animal Shape Preview ({generatedPaths.Count})", out LevelPack pack, out string packDetails))
            {
                report.Add($"PackFailed={packDetails}");
                WriteReport(kAnimalShapeFollowupReportPath, report);
                Debug.LogError($"[SeedMaskPatch] Animal Shape Followup pack failed: {packDetails}");
                return;
            }

            report.Add($"PackSynced={kAnimalShapePreviewPackPath} | {packDetails}");
            AttachLevelPackToDemo(pack, "SeedMaskPatch AnimalShapeFollowup");
            report.Add("DemoAttached=True");
            WriteReport(kAnimalShapeFollowupReportPath, report);

            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] Animal Shape Followup finished. levels={generatedPaths.Count}, pack={kAnimalShapePreviewPackPath}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Run Readable Shape Preview Pack")]
        public static void RunReadableShapePreviewPack()
        {
            ShapeExperimentMaskBuilder.CreateReadableShapePreviewMasks();

            string[] maskPaths =
            {
                kOriginalStarMaskPath,
                "Assets/ArrowMagic/Masks/ShapeReadablePreview/MaskReadable_20x20-HeartBold.png",
                "Assets/ArrowMagic/Masks/ShapeReadablePreview/MaskReadable_21x21-GemBold.png",
                "Assets/ArrowMagic/Masks/ShapeReadablePreview/MaskReadable_28x28-StarBoldLarge.png",
                "Assets/ArrowMagic/Masks/ShapeReadablePreview/MaskReadable_28x30-HouseBold.png",
                "Assets/ArrowMagic/Masks/ShapeReadablePreview/MaskReadable_30x36-RocketBold.png",
                "Assets/ArrowMagic/Masks/ShapeReadablePreview/MaskReadable_36x28-CarBold.png"
            };

            var report = new List<string>
            {
                "SeedMaskPatch Readable Shape Preview Pack",
                $"SourcePackFolder={kShapeExperimentSourcePackFolder}",
                $"CandidateOutput={kShapeExperimentCandidateFolder}",
                $"Pack={kReadableShapePreviewPackPath}",
                "Mode=ReadableShapeMaskCropRepairOneFinalPerMask",
                "Design=early 19-21 plus recommended 28-36 sizes"
            };

            var sourceSeeds = LoadShapeExperimentSourceSeeds(report);
            report.Add($"LoadedSourceSeeds={sourceSeeds.Count}");
            if (sourceSeeds.Count == 0)
            {
                WriteReport(kReadableShapePreviewReportPath, report);
                Debug.LogError("[SeedMaskPatch] Readable Shape Preview failed: no source seeds found.");
                return;
            }

            EnsureFolderExists(kShapeExperimentCandidateFolder);
            EnsureFolderExists(kShapeExperimentPackFolder);

            var generatedPaths = new List<string>();
            for (int i = 0; i < maskPaths.Length; i++)
            {
                string maskPath = maskPaths[i];
                var mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);
                if (mask == null)
                {
                    report.Add($"[{i + 1}] Mask={maskPath} | Missing");
                    WriteReport(kReadableShapePreviewReportPath, report);
                    continue;
                }

                var window = CreateInstance<SeedMaskPatchWindow>();
                ApplyMenuDefaultRuntimeSettings(window);
                window._mask = mask;
                window._placementMode = PlacementMode.Center;
                window._autoIfCenterFails = false;
                window._patchOutputMode = PatchOutputMode.GreedyRescue;
                window._inflateMaskForSolvability = false;
                window._maskInflationPasses = 0;
                window._adaptiveHealFillFallback = false;
                window._maskHealFillRatio = 1f;
                window._maskHealFillRatioFloor = 1f;
                window._maskHealFillFallbackSteps = 0;
                window._maxGreedyMovesMultiplier = kRepairExperimentGreedyMultiplier;
                window._fallbackCleanupLoops = kRepairExperimentFallbackLoops;
                window._greedyTrimPerChain = kRepairExperimentTrimPerChain;
                window._preGreedyGeometryFixPasses = 10;
                window._allowGreedyFullChainRemoval = false;
                window._allowGreedyFallbackChainClear = false;
                window._candidateTopCount = 3;
                window._candidatePreEvalLimit = 64;
                window._candidatePreEvalTimeoutMs = 90000;
                window._candidateMinFillRatio = 0.90f;
                window._outputFolder = kShapeExperimentCandidateFolder;
                window.EnsureOutputFolder();
                window._outputPrefix = $"readableshape_{i + 1:00}_{SanitizeName(Path.GetFileNameWithoutExtension(maskPath)).ToLowerInvariant()}";

                MatchShapeExperimentSeeds(window, mask, sourceSeeds, 140, out string matchSummary);
                report.Add($"[{i + 1}] Mask={maskPath} | {matchSummary}");

                Debug.Log($"[SeedMaskPatch] Readable Shape Preview match/eval start {i + 1}/{maskPaths.Length}: {maskPath}");
                window.RunCandidatePreEval();

                int previewCount = Math.Min(3, window._candidateResults.Count);
                for (int r = 0; r < previewCount; r++)
                    report.Add($"    Top{r + 1}: {BuildCandidateResultLine(r + 1, window._candidateResults[r])}");

                SeedCandidateResult chosen = null;
                for (int r = 0; r < window._candidateResults.Count; r++)
                {
                    var candidate = window._candidateResults[r];
                    if (candidate != null && candidate.IsFeasible && candidate.SourceSeed != null)
                    {
                        chosen = candidate;
                        break;
                    }
                }

                if (chosen == null)
                {
                    report.Add("    Generated=False | Reason=No feasible matched candidate");
                    WriteReport(kReadableShapePreviewReportPath, report);
                    continue;
                }

                string generated = window.ProcessSingle(chosen.SourceSeed, mask);
                int maskArea = 0;
                int finalFill = 0;
                float ratio = 0f;
                int chains = 0;
                bool straightRejected = false;
                string straightnessDetails = "straightness=not-evaluated";
                if (window.TryReadMask(mask, true, false, 0, out bool[] maskCanSpawn))
                    maskArea = CountMaskArea(maskCanSpawn);
                if (!string.IsNullOrWhiteSpace(generated))
                {
                    var generatedDef = AssetDatabase.LoadAssetAtPath<LevelDefinition>(generated);
                    if (generatedDef != null)
                    {
                        chains = CountAuthoredChains(generatedDef.authoredLevel);
                        straightRejected = IsStraightDominatedShape(generatedDef.authoredLevel, out straightnessDetails);
                        if (TryBuildBoardFromSeed(generatedDef, out BoardState generatedBoard))
                        {
                            finalFill = CountArrowTiles(generatedBoard);
                            ratio = maskArea > 0 ? (float)finalFill / maskArea : 0f;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(generated) && !straightRejected)
                    generatedPaths.Add(generated);

                report.Add($"    Generated={!string.IsNullOrWhiteSpace(generated)} | QualityRejected={straightRejected} | Source={chosen.SourceSeedPath} | MaskArea={maskArea} | FinalFill={finalFill} | Ratio={ratio:0.000} | Chains={chains} | {straightnessDetails} | Raw={window._latestRawClipPath} | Final={generated}");
                WriteReport(kReadableShapePreviewReportPath, report);
            }

            if (!TrySyncLevelPack(generatedPaths, kReadableShapePreviewPackPath, "ReadableShapePreviewPack", $"Readable Shape Preview ({generatedPaths.Count})", out LevelPack pack, out string packDetails))
            {
                report.Add($"PackFailed={packDetails}");
                WriteReport(kReadableShapePreviewReportPath, report);
                Debug.LogError($"[SeedMaskPatch] Readable Shape Preview pack failed: {packDetails}");
                return;
            }

            report.Add($"PackSynced={kReadableShapePreviewPackPath} | {packDetails}");
            AttachLevelPackToDemo(pack, "SeedMaskPatch ReadableShapePreview");
            report.Add("DemoAttached=True");
            WriteReport(kReadableShapePreviewReportPath, report);

            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] Readable Shape Preview finished. levels={generatedPaths.Count}, pack={kReadableShapePreviewPackPath}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Run Original Star Mask Preview Pack")]
        public static void RunOriginalStarMaskPreviewPack()
        {
            var report = new List<string>
            {
                "SeedMaskPatch Original Star Mask Preview Pack",
                $"Mask={kOriginalStarMaskPath}",
                $"SourcePackFolder={kShapeExperimentSourcePackFolder}",
                $"CandidateOutput={kShapeExperimentCandidateFolder}",
                $"Pack={kOriginalStarPreviewPackPath}",
                "Mode=SingleOriginalStarMaskCropRepair"
            };

            var mask = AssetDatabase.LoadAssetAtPath<Texture2D>(kOriginalStarMaskPath);
            if (mask == null)
            {
                report.Add("Failed=missing mask");
                WriteReport(kOriginalStarPreviewReportPath, report);
                Debug.LogError($"[SeedMaskPatch] Original Star Preview failed: missing mask {kOriginalStarMaskPath}");
                return;
            }

            var sourceSeeds = LoadShapeExperimentSourceSeeds(report);
            report.Add($"LoadedSourceSeeds={sourceSeeds.Count}");
            if (sourceSeeds.Count == 0)
            {
                report.Add("Failed=no source seeds");
                WriteReport(kOriginalStarPreviewReportPath, report);
                Debug.LogError("[SeedMaskPatch] Original Star Preview failed: no source seeds.");
                return;
            }

            EnsureFolderExists(kShapeExperimentCandidateFolder);
            EnsureFolderExists(kShapeExperimentPackFolder);

            var window = CreateInstance<SeedMaskPatchWindow>();
            ApplyMenuDefaultRuntimeSettings(window);
            window._mask = mask;
            window._placementMode = PlacementMode.Center;
            window._autoIfCenterFails = false;
            window._patchOutputMode = PatchOutputMode.GreedyRescue;
            window._inflateMaskForSolvability = false;
            window._maskInflationPasses = 0;
            window._adaptiveHealFillFallback = false;
            window._maskHealFillRatio = 1f;
            window._maskHealFillRatioFloor = 1f;
            window._maskHealFillFallbackSteps = 0;
            window._maxGreedyMovesMultiplier = kRepairExperimentGreedyMultiplier;
            window._fallbackCleanupLoops = kRepairExperimentFallbackLoops;
            window._greedyTrimPerChain = kRepairExperimentTrimPerChain;
            window._preGreedyGeometryFixPasses = 10;
            window._allowGreedyFullChainRemoval = false;
            window._allowGreedyFallbackChainClear = false;
            window._candidateTopCount = 3;
            window._candidatePreEvalLimit = 80;
            window._candidatePreEvalTimeoutMs = 90000;
            window._candidateMinFillRatio = 0.90f;
            window._outputFolder = kShapeExperimentCandidateFolder;
            window.EnsureOutputFolder();
            window._outputPrefix = "original_star_mask_preview";

            MatchShapeExperimentSeeds(window, mask, sourceSeeds, 160, out string matchSummary);
            report.Add(matchSummary);
            Debug.Log($"[SeedMaskPatch] Original Star Preview match/eval start: {kOriginalStarMaskPath}");
            window.RunCandidatePreEval();

            int previewCount = Math.Min(8, window._candidateResults.Count);
            for (int r = 0; r < previewCount; r++)
                report.Add($"Top{r + 1}: {BuildCandidateResultLine(r + 1, window._candidateResults[r])}");

            SeedCandidateResult chosen = null;
            for (int r = 0; r < window._candidateResults.Count; r++)
            {
                var candidate = window._candidateResults[r];
                if (candidate != null && candidate.IsFeasible && candidate.SourceSeed != null)
                {
                    chosen = candidate;
                    break;
                }
            }

            if (chosen == null)
            {
                report.Add("Generated=False | Reason=No feasible matched candidate");
                WriteReport(kOriginalStarPreviewReportPath, report);
                Debug.LogError("[SeedMaskPatch] Original Star Preview failed: no feasible candidate.");
                return;
            }

            string generated = window.ProcessSingle(chosen.SourceSeed, mask);
            int maskArea = 0;
            int finalFill = 0;
            float ratio = 0f;
            int chains = 0;
            if (window.TryReadMask(mask, true, false, 0, out bool[] maskCanSpawn))
                maskArea = CountMaskArea(maskCanSpawn);
            if (!string.IsNullOrWhiteSpace(generated))
            {
                var generatedDef = AssetDatabase.LoadAssetAtPath<LevelDefinition>(generated);
                if (generatedDef != null)
                {
                    chains = CountAuthoredChains(generatedDef.authoredLevel);
                    if (TryBuildBoardFromSeed(generatedDef, out BoardState generatedBoard))
                    {
                        finalFill = CountArrowTiles(generatedBoard);
                        ratio = maskArea > 0 ? (float)finalFill / maskArea : 0f;
                    }
                }
            }

            report.Add($"Generated={!string.IsNullOrWhiteSpace(generated)} | Source={chosen.SourceSeedPath} | MaskArea={maskArea} | FinalFill={finalFill} | Ratio={ratio:0.000} | Chains={chains} | Raw={window._latestRawClipPath} | Final={generated}");

            if (string.IsNullOrWhiteSpace(generated))
            {
                WriteReport(kOriginalStarPreviewReportPath, report);
                Debug.LogError("[SeedMaskPatch] Original Star Preview failed during final generation.");
                return;
            }

            if (!TrySyncSingleLevelPack(generated, kOriginalStarPreviewPackPath, "OriginalStarMaskPreviewPack", "Original Star Mask Preview", out LevelPack pack, out string packDetails))
            {
                report.Add($"PackFailed={packDetails}");
                WriteReport(kOriginalStarPreviewReportPath, report);
                Debug.LogError($"[SeedMaskPatch] Original Star Preview pack failed: {packDetails}");
                return;
            }

            report.Add($"PackSynced={kOriginalStarPreviewPackPath} | {packDetails}");
            AttachLevelPackToDemo(pack, "SeedMaskPatch OriginalStarPreview");
            report.Add("DemoAttached=True");
            WriteReport(kOriginalStarPreviewReportPath, report);

            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] Original Star Preview finished. final={generated}, pack={kOriginalStarPreviewPackPath}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Attach Readable Shape Preview Pack From Report")]
        public static void AttachReadableShapePreviewPackFromReport()
        {
            var report = new List<string>
            {
                "SeedMaskPatch Attach Readable Shape Preview Pack From Report",
                $"SourceReport={kReadableShapePreviewReportPath}",
                $"Pack={kReadableShapePreviewPackPath}"
            };

            var levelPaths = new List<string>();
            if (!TryCollectAcceptedFinalLevelPathsFromReport(kReadableShapePreviewReportPath, levelPaths, report))
            {
                WriteReport(kReadableShapeAttachReportPath, report);
                Debug.LogError($"[SeedMaskPatch] Readable Shape attach failed: report missing {kReadableShapePreviewReportPath}");
                return;
            }

            report.Add($"FoundFinalLevels={levelPaths.Count}");
            for (int i = 0; i < levelPaths.Count; i++)
                report.Add($"Level{i + 1}={levelPaths[i]}");

            if (!TrySyncLevelPack(levelPaths, kReadableShapePreviewPackPath, "ReadableShapePreviewPack", $"Readable Shape Preview ({levelPaths.Count})", out LevelPack pack, out string packDetails))
            {
                report.Add($"PackFailed={packDetails}");
                WriteReport(kReadableShapeAttachReportPath, report);
                Debug.LogError($"[SeedMaskPatch] Readable Shape attach failed: {packDetails}");
                return;
            }

            report.Add($"PackSynced={kReadableShapePreviewPackPath} | {packDetails}");
            AttachLevelPackToDemo(pack, "SeedMaskPatch ReadableShapeAttach");
            report.Add("DemoAttached=True");
            WriteReport(kReadableShapeAttachReportPath, report);
            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] Readable Shape Preview attached from report. levels={levelPaths.Count}, pack={kReadableShapePreviewPackPath}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Attach Animal Shape Preview Pack From Report")]
        public static void AttachAnimalShapePreviewPackFromReport()
        {
            var report = new List<string>
            {
                "SeedMaskPatch Attach Animal Shape Preview Pack From Report",
                $"SourceReport={kAnimalShapePreviewReportPath}",
                $"Pack={kAnimalShapePreviewPackPath}"
            };

            var levelPaths = new List<string>();
            if (!TryCollectAcceptedFinalLevelPathsFromReport(kAnimalShapePreviewReportPath, levelPaths, report))
            {
                WriteReport(kAnimalShapeAttachReportPath, report);
                Debug.LogError($"[SeedMaskPatch] Animal Shape attach failed: report missing {kAnimalShapePreviewReportPath}");
                return;
            }
            int mainAccepted = levelPaths.Count;
            var followupPaths = new List<string>();
            if (TryCollectAcceptedFinalLevelPathsFromReport(kAnimalShapeFollowupReportPath, followupPaths, report))
            {
                for (int i = 0; i < followupPaths.Count; i++)
                {
                    if (!levelPaths.Contains(followupPaths[i]))
                        levelPaths.Add(followupPaths[i]);
                }
            }
            report.Add($"AcceptedFromMain={mainAccepted}");
            report.Add($"AcceptedFromFollowup={Mathf.Max(0, levelPaths.Count - mainAccepted)}");

            int beforeReject = levelPaths.Count;
            levelPaths.RemoveAll(IsRejectedAnimalShapePreviewLevel);
            report.Add($"RejectedByManualAnimalReview={beforeReject - levelPaths.Count}");

            report.Add($"FoundFinalLevels={levelPaths.Count}");
            for (int i = 0; i < levelPaths.Count; i++)
                report.Add($"Level{i + 1}={levelPaths[i]}");

            if (!TrySyncLevelPack(levelPaths, kAnimalShapePreviewPackPath, "AnimalShapePreviewPack", $"Animal Shape Preview ({levelPaths.Count})", out LevelPack pack, out string packDetails))
            {
                report.Add($"PackFailed={packDetails}");
                WriteReport(kAnimalShapeAttachReportPath, report);
                Debug.LogError($"[SeedMaskPatch] Animal Shape attach failed: {packDetails}");
                return;
            }

            report.Add($"PackSynced={kAnimalShapePreviewPackPath} | {packDetails}");
            AttachLevelPackToDemo(pack, "SeedMaskPatch AnimalShapeAttach");
            report.Add("DemoAttached=True");
            WriteReport(kAnimalShapeAttachReportPath, report);
            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] Animal Shape Preview attached from report. levels={levelPaths.Count}, pack={kAnimalShapePreviewPackPath}");
        }

        private static bool TryCollectAcceptedFinalLevelPathsFromReport(string sourceReportPath, List<string> levelPaths, List<string> report)
        {
            if (levelPaths == null)
                return false;

            string fullReportPath = Path.GetFullPath(Path.Combine(Application.dataPath, "..", sourceReportPath));
            if (!File.Exists(fullReportPath))
            {
                report?.Add("Failed=report missing");
                return false;
            }

            string[] lines = File.ReadAllLines(fullReportPath);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.IndexOf("QualityRejected=True", StringComparison.OrdinalIgnoreCase) >= 0)
                    continue;

                int idx = line.IndexOf("Final=", StringComparison.Ordinal);
                if (idx < 0)
                    continue;

                string path = line.Substring(idx + "Final=".Length).Trim();
                int pipe = path.IndexOf('|');
                if (pipe >= 0)
                    path = path.Substring(0, pipe).Trim();
                if (string.IsNullOrWhiteSpace(path) || !path.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase))
                    continue;
                if (AssetDatabase.LoadAssetAtPath<LevelDefinition>(path) != null)
                    levelPaths.Add(path);
            }

            return true;
        }

        private static bool IsRejectedAnimalShapePreviewLevel(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            return path.IndexOf("catheadlarge", StringComparison.OrdinalIgnoreCase) >= 0
                || path.IndexOf("pawbold", StringComparison.OrdinalIgnoreCase) >= 0
                || path.IndexOf("dogheadlarge", StringComparison.OrdinalIgnoreCase) >= 0
                || path.IndexOf("bunnyheadlarge", StringComparison.OrdinalIgnoreCase) >= 0
                || path.IndexOf("fishlarge", StringComparison.OrdinalIgnoreCase) >= 0
                || path.IndexOf("turtlelarge", StringComparison.OrdinalIgnoreCase) >= 0
                || path.IndexOf("dogsittingtall", StringComparison.OrdinalIgnoreCase) >= 0
                || path.IndexOf("horseheadtall", StringComparison.OrdinalIgnoreCase) >= 0
                || path.IndexOf("penguintall", StringComparison.OrdinalIgnoreCase) >= 0
                || path.IndexOf("giraffetall", StringComparison.OrdinalIgnoreCase) >= 0
                || path.IndexOf("foxsittingtall", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Run Kid Shape Matched Seed Batch")]
        public static void RunKidShapeMatchedSeedBatchExperiment()
        {
            const string reportPath = "Temp/seedmask_kidshape_matched_batch_report.txt";
            string[] maskPaths =
            {
                "Assets/ArrowMagic/Masks/Mask_24x24-HeartKid.png",
                "Assets/ArrowMagic/Masks/Mask_28x30-HouseKid.png",
                "Assets/ArrowMagic/Masks/Mask_32x32-StarKid.png",
                "Assets/ArrowMagic/Masks/Mask_36x28-CarKid.png",
                "Assets/ArrowMagic/Masks/Mask_40x44-TrophyKid.png"
            };

            var report = new List<string>
            {
                "SeedMaskPatch Kid Shape Matched Seed Batch",
                $"Count={maskPaths.Length}",
                "Mode=MatchSeedTop24GenerateBest"
            };

            string bestResult = string.Empty;
            string bestRaw = string.Empty;
            LevelDefinition bestSource = null;
            string bestMask = string.Empty;
            int bestFill = -1;
            int bestMaskArea = 0;
            float bestRatio = -1f;

            for (int i = 0; i < maskPaths.Length; i++)
            {
                string maskPath = maskPaths[i];
                var mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);
                if (mask == null)
                {
                    report.Add($"[{i + 1}] Mask={maskPath} | Missing");
                    Debug.LogError($"[SeedMaskPatch] Kid Shape Matched Batch mask missing. mask={maskPath}");
                    continue;
                }

                var window = CreateInstance<SeedMaskPatchWindow>();
                ApplyMenuDefaultRuntimeSettings(window);
                window._mask = mask;
                window._placementMode = PlacementMode.Center;
                window._autoIfCenterFails = false;
                window._patchOutputMode = PatchOutputMode.GreedyRescue;
                window._inflateMaskForSolvability = false;
                window._maskInflationPasses = 0;
                window._adaptiveHealFillFallback = false;
                window._maskHealFillRatio = 1f;
                window._maskHealFillRatioFloor = 1f;
                window._maskHealFillFallbackSteps = 0;
                window._maxGreedyMovesMultiplier = kRepairExperimentGreedyMultiplier;
                window._fallbackCleanupLoops = kRepairExperimentFallbackLoops;
                window._greedyTrimPerChain = kRepairExperimentTrimPerChain;
                window._preGreedyGeometryFixPasses = 10;
                window._allowGreedyFullChainRemoval = false;
                window._allowGreedyFallbackChainClear = false;
                window._candidateTopCount = 3;
                window._candidatePreEvalLimit = 24;
                window._candidatePreEvalTimeoutMs = 90000;
                window._candidateMinFillRatio = 0.90f;
                window._outputPrefix = $"seed_mask_matched_{SanitizeName(Path.GetFileNameWithoutExtension(maskPath)).ToLowerInvariant()}";

                Debug.Log($"[SeedMaskPatch] Kid Shape Matched Batch match/eval start {i + 1}/{maskPaths.Length}: {maskPath}");
                window.MatchCandidateSeeds();
                window.RunCandidatePreEval();

                report.Add($"[{i + 1}] Mask={maskPath} | MatchSummary={window._candidateMatchSummary}");
                int previewCount = Math.Min(3, window._candidateResults.Count);
                for (int r = 0; r < previewCount; r++)
                    report.Add($"    Top{r + 1}: {BuildCandidateResultLine(r + 1, window._candidateResults[r])}");

                SeedCandidateResult chosen = null;
                for (int r = 0; r < window._candidateResults.Count; r++)
                {
                    var candidate = window._candidateResults[r];
                    if (candidate != null && candidate.IsFeasible && candidate.SourceSeed != null)
                    {
                        chosen = candidate;
                        break;
                    }
                }

                if (chosen == null)
                {
                    report.Add("    Generated=False | Reason=No feasible matched candidate");
                    continue;
                }

                string generated = window.ProcessSingle(chosen.SourceSeed, mask);
                int maskArea = 0;
                int finalFill = 0;
                float ratio = 0f;
                if (window.TryReadMask(mask, true, false, 0, out bool[] maskCanSpawn))
                    maskArea = CountMaskArea(maskCanSpawn);
                if (!string.IsNullOrWhiteSpace(generated))
                {
                    var generatedDef = AssetDatabase.LoadAssetAtPath<LevelDefinition>(generated);
                    if (generatedDef != null && TryBuildBoardFromSeed(generatedDef, out BoardState generatedBoard))
                    {
                        finalFill = CountArrowTiles(generatedBoard);
                        ratio = maskArea > 0 ? (float)finalFill / maskArea : 0f;
                    }
                }

                report.Add($"    Generated=True | Source={chosen.SourceSeedPath} | MaskArea={maskArea} | FinalFill={finalFill} | Ratio={ratio:0.000} | Raw={window._latestRawClipPath} | Final={generated}");

                if (!string.IsNullOrWhiteSpace(generated) && ratio > bestRatio)
                {
                    bestRatio = ratio;
                    bestFill = finalFill;
                    bestMaskArea = maskArea;
                    bestMask = maskPath;
                    bestResult = generated;
                    bestRaw = window._latestRawClipPath;
                    bestSource = chosen.SourceSeed;
                }
            }

            report.Add($"BestMask={bestMask}");
            report.Add($"BestFinal={bestResult}");
            report.Add($"BestRaw={bestRaw}");
            report.Add($"BestFill={bestFill}/{bestMaskArea}({bestRatio:0.000})");

            try
            {
                File.WriteAllText(reportPath, string.Join("\n", report));
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[SeedMaskPatch] Failed to write Kid Shape Matched Batch report: {ex.Message}");
            }

            if (!string.IsNullOrWhiteSpace(bestResult) && bestSource != null && !TrySyncDemoComparePack(bestSource, bestResult, bestRaw))
            {
                Debug.LogError("[SeedMaskPatch] Kid Shape Matched Batch did not update demo compare pack.");
                return;
            }

            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] Kid Shape Matched Batch finished. best={bestResult} fill={bestFill}/{bestMaskArea} ratio={bestRatio:0.000}");
        }

        private static List<string> LoadShapeExperimentMaskPaths(int maxCount)
        {
            var result = new List<string>();
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            string catalogFullPath = Path.GetFullPath(Path.Combine(Application.dataPath, "..", kShapeExperimentCatalogPath));
            if (File.Exists(catalogFullPath))
            {
                string[] lines = File.ReadAllLines(catalogFullPath);
                for (int i = 1; i < lines.Length; i++)
                {
                    if (maxCount > 0 && result.Count >= maxCount)
                        break;

                    string line = lines[i];
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split(',');
                    if (parts.Length < 8)
                        continue;

                    string path = parts[7].Trim();
                    if (string.IsNullOrWhiteSpace(path) || !path.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase))
                        continue;
                    if (!seen.Add(path))
                        continue;

                    if (AssetDatabase.LoadAssetAtPath<Texture2D>(path) != null)
                        result.Add(path);
                }
            }

            if (result.Count == 0)
            {
                string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { kShapeExperimentMaskFolder });
                var paths = new List<string>();
                for (int i = 0; i < guids.Length; i++)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                    if (!string.IsNullOrWhiteSpace(path))
                        paths.Add(path);
                }

                paths.Sort(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < paths.Count; i++)
                {
                    if (maxCount > 0 && result.Count >= maxCount)
                        break;
                    if (seen.Add(paths[i]))
                        result.Add(paths[i]);
                }
            }

            return result;
        }

        private static List<LevelDefinition> LoadShapeExperimentSourceSeeds(List<string> report)
        {
            var result = new List<LevelDefinition>();
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string[] packGuids = AssetDatabase.FindAssets("t:LevelPack", new[] { kShapeExperimentSourcePackFolder });
            Array.Sort(packGuids, StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < packGuids.Length; i++)
            {
                string packPath = AssetDatabase.GUIDToAssetPath(packGuids[i]);
                var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(packPath);
                int added = 0;
                if (pack?.levels != null)
                {
                    for (int j = 0; j < pack.levels.Length; j++)
                    {
                        var level = pack.levels[j];
                        if (level == null)
                            continue;

                        string levelPath = AssetDatabase.GetAssetPath(level);
                        if (string.IsNullOrWhiteSpace(levelPath) || !seen.Add(levelPath))
                            continue;

                        result.Add(level);
                        added++;
                    }
                }

                report?.Add($"SourcePack={packPath} | Added={added}");
            }

            return result;
        }

        private static void MatchShapeExperimentSeeds(
            SeedMaskPatchWindow window,
            Texture2D mask,
            List<LevelDefinition> sourceSeeds,
            int maxMatches,
            out string summary)
        {
            summary = "Matched=0";
            if (window == null || mask == null || sourceSeeds == null || sourceSeeds.Count == 0)
                return;

            int maskW = Mathf.Max(1, mask.width);
            int maskH = Mathf.Max(1, mask.height);
            var fits = new List<SeedMatchEntry>();
            var fallback = new List<SeedMatchEntry>();

            for (int i = 0; i < sourceSeeds.Count; i++)
            {
                var seed = sourceSeeds[i];
                if (seed == null || seed.source != LevelDefinition.LevelSource.Authored)
                    continue;
                if (!TryBuildBoardFromSeed(seed, out BoardState board) || board == null)
                    continue;

                int seedW = Mathf.Max(1, board.width);
                int seedH = Mathf.Max(1, board.height);
                int score = (seedW - maskW) * (seedW - maskW) + (seedH - maskH) * (seedH - maskH);
                var entry = new SeedMatchEntry
                {
                    Seed = seed,
                    CachedPath = AssetDatabase.GetAssetPath(seed),
                    SizeScore = score
                };

                if (seedW >= maskW && seedH >= maskH)
                    fits.Add(entry);
                else
                    fallback.Add(entry);
            }

            Comparison<SeedMatchEntry> comparer = (a, b) =>
            {
                int scoreCmp = a.SizeScore.CompareTo(b.SizeScore);
                if (scoreCmp != 0)
                    return scoreCmp;
                return string.Compare(a.CachedPath, b.CachedPath, StringComparison.OrdinalIgnoreCase);
            };

            fits.Sort(comparer);
            fallback.Sort(comparer);

            window._matchedSeeds.Clear();
            window._candidateResults.Clear();
            window._matchedMaskPath = GetAssetPath(mask);

            var chosen = fits.Count > 0 ? fits : fallback;
            int count = Math.Min(Math.Max(1, maxMatches), chosen.Count);
            for (int i = 0; i < count; i++)
            {
                if (chosen[i].Seed != null)
                    window._matchedSeeds.Add(chosen[i].Seed);
            }

            summary = fits.Count > 0
                ? $"Matched={window._matchedSeeds.Count}/{fits.Count} fitting seeds for mask={maskW}x{maskH}"
                : $"Matched={window._matchedSeeds.Count}/{fallback.Count} fallback seeds for oversized mask={maskW}x{maskH}";
            window._candidateMatchSummary = summary;
        }

        private static bool TrySyncShapeExperimentPreviewPack(List<string> levelPaths, out LevelPack pack, out string details)
        {
            pack = null;
            details = string.Empty;
            if (levelPaths == null || levelPaths.Count == 0)
            {
                details = "no generated levels";
                return false;
            }

            EnsureFolderExists(kShapeExperimentPackFolder);
            var levels = new List<LevelDefinition>();
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < levelPaths.Count; i++)
            {
                string path = levelPaths[i];
                if (string.IsNullOrWhiteSpace(path) || !seen.Add(path))
                    continue;

                var level = AssetDatabase.LoadAssetAtPath<LevelDefinition>(path);
                if (level != null)
                    levels.Add(level);
            }

            if (levels.Count == 0)
            {
                details = "no loadable generated levels";
                return false;
            }

            pack = AssetDatabase.LoadAssetAtPath<LevelPack>(kShapeExperimentPreviewPackPath);
            bool isNewPack = pack == null;
            if (pack == null)
                pack = ScriptableObject.CreateInstance<LevelPack>();

            pack.packId = "ShapeExperimentPreviewPack";
            pack.displayName = $"Shape Experiment Preview ({levels.Count})";
            pack.levels = levels.ToArray();
            EditorUtility.SetDirty(pack);

            if (isNewPack)
                AssetDatabase.CreateAsset(pack, kShapeExperimentPreviewPackPath);

            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(kShapeExperimentPreviewPackPath);
            details = $"levels={levels.Count}";
            return true;
        }

        private static bool TrySyncLevelPack(
            List<string> levelPaths,
            string packPath,
            string packId,
            string displayName,
            out LevelPack pack,
            out string details)
        {
            pack = null;
            details = string.Empty;
            if (levelPaths == null || levelPaths.Count == 0)
            {
                details = "no generated levels";
                return false;
            }

            string folder = Path.GetDirectoryName(packPath)?.Replace("\\", "/");
            EnsureFolderExists(folder);

            var levels = new List<LevelDefinition>();
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < levelPaths.Count; i++)
            {
                string path = levelPaths[i];
                if (string.IsNullOrWhiteSpace(path) || !seen.Add(path))
                    continue;

                var level = AssetDatabase.LoadAssetAtPath<LevelDefinition>(path);
                if (level != null)
                    levels.Add(level);
            }

            if (levels.Count == 0)
            {
                details = "no loadable generated levels";
                return false;
            }

            pack = AssetDatabase.LoadAssetAtPath<LevelPack>(packPath);
            bool isNewPack = pack == null;
            if (pack == null)
                pack = ScriptableObject.CreateInstance<LevelPack>();

            pack.packId = packId;
            pack.displayName = displayName;
            pack.levels = levels.ToArray();
            EditorUtility.SetDirty(pack);

            if (isNewPack)
                AssetDatabase.CreateAsset(pack, packPath);

            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(packPath);
            details = $"levels={levels.Count}";
            return true;
        }

        private static bool TryCopyAssetIfMissing(string sourcePath, string destinationPath, out string details)
        {
            details = string.Empty;
            if (string.IsNullOrWhiteSpace(sourcePath) || string.IsNullOrWhiteSpace(destinationPath))
            {
                details = "empty source or destination";
                return false;
            }

            if (AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(sourcePath) == null)
            {
                details = $"source missing: {sourcePath}";
                return false;
            }

            string folder = Path.GetDirectoryName(destinationPath)?.Replace("\\", "/");
            EnsureFolderExists(folder);

            if (AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(destinationPath) != null)
            {
                details = "destination already exists; kept existing";
                return true;
            }

            if (!AssetDatabase.CopyAsset(sourcePath, destinationPath))
            {
                details = $"copy failed: {sourcePath} -> {destinationPath}";
                return false;
            }

            AssetDatabase.ImportAsset(destinationPath, ImportAssetOptions.ForceUpdate);
            details = "copied";
            return true;
        }

        private static void AddExistingPackLevelsToKeepArchive(string packPath, string label, List<string> keptLevelPaths, List<string> report)
        {
            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(packPath);
            if (pack == null || pack.levels == null || pack.levels.Length == 0)
            {
                report?.Add($"ExistingPack={label} | path={packPath} | missing-or-empty");
                return;
            }

            int added = 0;
            for (int i = 0; i < pack.levels.Length; i++)
            {
                string levelPath = GetAssetPath(pack.levels[i]);
                if (string.IsNullOrWhiteSpace(levelPath))
                    continue;

                keptLevelPaths?.Add(levelPath);
                added++;
                report?.Add($"ExistingKeep={label} | Level{i + 1}={levelPath}");
            }

            report?.Add($"ExistingPack={label} | path={packPath} | added={added}");
        }

        private static void ArchiveConfirmedKeep(
            string id,
            string reason,
            string sourceLevelPath,
            string sourceMaskPath,
            string destinationLevelFileName,
            List<string> keptLevelPaths,
            List<string> report)
        {
            string destinationLevelPath = $"{kTallFitEarlyKeepLevelFolder}/{destinationLevelFileName}";
            bool levelCopied = TryCopyAssetIfMissing(sourceLevelPath, destinationLevelPath, out string levelDetails);
            if (levelCopied && AssetDatabase.LoadAssetAtPath<LevelDefinition>(destinationLevelPath) != null)
                keptLevelPaths?.Add(destinationLevelPath);

            string destinationMaskPath = string.Empty;
            bool maskCopied = false;
            string maskDetails = "no source mask";
            if (!string.IsNullOrWhiteSpace(sourceMaskPath))
            {
                destinationMaskPath = $"{kTallFitEarlyKeepMaskFolder}/{Path.GetFileName(sourceMaskPath)}";
                maskCopied = TryCopyAssetIfMissing(sourceMaskPath, destinationMaskPath, out maskDetails);
            }

            report?.Add($"ConfirmedKeep={id} | reason={reason} | level={sourceLevelPath} -> {destinationLevelPath} | levelOk={levelCopied} | {levelDetails} | mask={sourceMaskPath} -> {destinationMaskPath} | maskOk={maskCopied} | {maskDetails}");
        }

        private static bool TrySyncSingleLevelPack(
            string levelPath,
            string packPath,
            string packId,
            string displayName,
            out LevelPack pack,
            out string details)
        {
            pack = null;
            details = string.Empty;
            if (string.IsNullOrWhiteSpace(levelPath))
            {
                details = "empty level path";
                return false;
            }

            var level = AssetDatabase.LoadAssetAtPath<LevelDefinition>(levelPath);
            if (level == null)
            {
                details = $"level not loadable: {levelPath}";
                return false;
            }

            string folder = Path.GetDirectoryName(packPath)?.Replace("\\", "/");
            EnsureFolderExists(folder);
            pack = AssetDatabase.LoadAssetAtPath<LevelPack>(packPath);
            bool isNewPack = pack == null;
            if (pack == null)
                pack = ScriptableObject.CreateInstance<LevelPack>();

            pack.packId = packId;
            pack.displayName = displayName;
            pack.levels = new[] { level };
            EditorUtility.SetDirty(pack);

            if (isNewPack)
                AssetDatabase.CreateAsset(pack, packPath);

            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(packPath);
            details = "levels=1";
            return true;
        }

        private static void AttachLevelPackToDemo(LevelPack pack, string logTag)
        {
            if (pack == null)
                return;

            var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(
                kDemoScenePath,
                UnityEditor.SceneManagement.OpenSceneMode.Single);
            var progression = UnityEngine.Object.FindFirstObjectByType<LevelProgression>();
            if (progression == null)
            {
                Debug.LogWarning($"[{logTag}] LevelProgression not found in {kDemoScenePath}");
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

        private static bool TryCreateTightCroppedAnimalBestLevel(
            LevelDefinition source,
            int index,
            out string outputPath,
            out string details)
        {
            outputPath = string.Empty;
            details = string.Empty;
            if (source == null || source.authoredLevel == null)
            {
                details = "invalid source";
                return false;
            }

            var authored = source.authoredLevel;
            int sourceW = Mathf.Max(1, authored.width);
            int sourceH = Mathf.Max(1, authored.height);
            const int padding = 1;

            bool hasContentBounds = TryFindAuthoredContentBounds(
                authored,
                padding,
                out int minX,
                out int minY,
                out int maxX,
                out int maxY);

            if (!hasContentBounds)
            {
                details = $"no authored content source={sourceW}x{sourceH}";
                return false;
            }

            string boundsSource = "authored";
            if (TryFindMaskContentBounds(source.masking != null ? source.masking.spawnMask : null, sourceW, sourceH, padding, out int maskMinX, out int maskMinY, out int maskMaxX, out int maskMaxY))
            {
                minX = Mathf.Min(minX, maskMinX);
                minY = Mathf.Min(minY, maskMinY);
                maxX = Mathf.Max(maxX, maskMaxX);
                maxY = Mathf.Max(maxY, maskMaxY);
                boundsSource = "mask+authored";
            }

            minX = Mathf.Clamp(minX, 0, sourceW - 1);
            minY = Mathf.Clamp(minY, 0, sourceH - 1);
            maxX = Mathf.Clamp(maxX, minX, sourceW - 1);
            maxY = Mathf.Clamp(maxY, minY, sourceH - 1);

            if (!TryCropAuthoredToBounds(authored, minX, minY, maxX, maxY, out AuthoredLevelData cropped, out string cropDetails))
            {
                details = $"crop failed source={sourceW}x{sourceH}, bounds={minX},{minY}-{maxX},{maxY}, {cropDetails}";
                return false;
            }

            if (!AuthoredLevelBuilder.TryBuildBoard(cropped, out BoardState croppedBoard, out string buildError))
            {
                details = $"cropped build failed source={sourceW}x{sourceH}, result={cropped.width}x{cropped.height}, {buildError}";
                return false;
            }

            int greedyMoves = Mathf.Max(512, cropped.width * cropped.height * 16);
            bool greedyOk = GreedyValidator.TryClearAllByGreedy(
                croppedBoard,
                new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty }),
                greedyMoves,
                out _);
            if (!greedyOk)
            {
                details = $"cropped greedy failed source={sourceW}x{sourceH}, result={cropped.width}x{cropped.height}, budget={greedyMoves}";
                return false;
            }

            string sourceMaskName = source.masking != null && source.masking.spawnMask != null
                ? source.masking.spawnMask.name
                : source.name;
            string safeShape = ShortenFileToken(SanitizeName(sourceMaskName), 44).ToLowerInvariant();
            string assetName = $"animalbest_tight_{index:00}_{safeShape}_{cropped.width}x{cropped.height}";
            outputPath = $"{kAnimalBestTightCropFolder}/{assetName}.asset";

            var croppedMask = FindMatchingAnimalBestMask(source.masking != null ? source.masking.spawnMask : null, cropped.width, cropped.height);
            var output = CreateTightCropDefinition(source, assetName, cropped, croppedMask);
            if (!SaveOrUpdateLevelDefinitionAsset(output, outputPath, out string saveDetails))
            {
                details = saveDetails;
                return false;
            }

            int oldTiles = GetAuthoredArrowCount(authored);
            int newTiles = GetAuthoredArrowCount(cropped);
            float oldCoverage = oldTiles / (float)Mathf.Max(1, sourceW * sourceH);
            float newCoverage = newTiles / (float)Mathf.Max(1, cropped.width * cropped.height);
            details = $"Source={GetAssetPath(source)} | boundsSource={boundsSource} | source={sourceW}x{sourceH} | bounds={minX},{minY}-{maxX},{maxY} | result={cropped.width}x{cropped.height} | chains={CountAuthoredChains(cropped)} | tiles={newTiles}/{oldTiles} | coverage={oldCoverage:0.000}->{newCoverage:0.000} | mask={(croppedMask != null ? GetAssetPath(croppedMask) : "<none>")} | {cropDetails} | greedyBudget={greedyMoves}";
            return true;
        }

        private static bool TryFindMaskContentBounds(
            Texture2D mask,
            int expectedW,
            int expectedH,
            int padding,
            out int minX,
            out int minY,
            out int maxX,
            out int maxY)
        {
            minX = minY = int.MaxValue;
            maxX = maxY = int.MinValue;
            if (mask == null || !mask.isReadable || mask.width != expectedW || mask.height != expectedH)
                return false;

            Color32[] pixels = mask.GetPixels32();
            for (int y = 0; y < mask.height; y++)
            {
                for (int x = 0; x < mask.width; x++)
                {
                    int idx = x + y * mask.width;
                    if (idx < 0 || idx >= pixels.Length || pixels[idx].a <= 127)
                        continue;

                    minX = Mathf.Min(minX, x);
                    minY = Mathf.Min(minY, y);
                    maxX = Mathf.Max(maxX, x);
                    maxY = Mathf.Max(maxY, y);
                }
            }

            return ExpandBounds(mask.width, mask.height, padding, ref minX, ref minY, ref maxX, ref maxY);
        }

        private static bool TryFindAuthoredContentBounds(
            AuthoredLevelData authored,
            int padding,
            out int minX,
            out int minY,
            out int maxX,
            out int maxY)
        {
            minX = minY = int.MaxValue;
            maxX = maxY = int.MinValue;
            if (authored == null || authored.width <= 0 || authored.height <= 0)
                return false;

            int localMinX = int.MaxValue;
            int localMinY = int.MaxValue;
            int localMaxX = int.MinValue;
            int localMaxY = int.MinValue;

            void IncludeIndex(int idx)
            {
                if (idx < 0 || idx >= authored.width * authored.height)
                    return;

                int x = idx % authored.width;
                int y = idx / authored.width;
                localMinX = Mathf.Min(localMinX, x);
                localMinY = Mathf.Min(localMinY, y);
                localMaxX = Mathf.Max(localMaxX, x);
                localMaxY = Mathf.Max(localMaxY, y);
            }

            if (authored.arrows != null)
            {
                for (int i = 0; i < authored.arrows.Count; i++)
                {
                    var indices = authored.arrows[i]?.indices;
                    if (indices == null)
                        continue;

                    for (int j = 0; j < indices.Count; j++)
                        IncludeIndex(indices[j]);
                }
            }

            if (authored.blockIndices != null)
            {
                for (int i = 0; i < authored.blockIndices.Count; i++)
                    IncludeIndex(authored.blockIndices[i]);
            }

            minX = localMinX;
            minY = localMinY;
            maxX = localMaxX;
            maxY = localMaxY;
            return ExpandBounds(authored.width, authored.height, padding, ref minX, ref minY, ref maxX, ref maxY);
        }

        private static bool ExpandBounds(
            int width,
            int height,
            int padding,
            ref int minX,
            ref int minY,
            ref int maxX,
            ref int maxY)
        {
            if (minX == int.MaxValue || minY == int.MaxValue || maxX == int.MinValue || maxY == int.MinValue)
                return false;

            int pad = Mathf.Max(0, padding);
            minX = Mathf.Clamp(minX - pad, 0, Mathf.Max(0, width - 1));
            minY = Mathf.Clamp(minY - pad, 0, Mathf.Max(0, height - 1));
            maxX = Mathf.Clamp(maxX + pad, minX, Mathf.Max(0, width - 1));
            maxY = Mathf.Clamp(maxY + pad, minY, Mathf.Max(0, height - 1));
            return true;
        }

        private static bool TryCropAuthoredToBounds(
            AuthoredLevelData source,
            int minX,
            int minY,
            int maxX,
            int maxY,
            out AuthoredLevelData cropped,
            out string details)
        {
            cropped = null;
            details = string.Empty;
            if (source == null || source.width <= 0 || source.height <= 0)
            {
                details = "invalid authored";
                return false;
            }

            int newW = Mathf.Max(1, maxX - minX + 1);
            int newH = Mathf.Max(1, maxY - minY + 1);
            cropped = new AuthoredLevelData
            {
                width = newW,
                height = newH,
                arrows = new List<AuthoredArrowData>(),
                blockIndices = new List<int>()
            };

            int droppedTiles = 0;
            int droppedChains = 0;
            if (source.arrows != null)
            {
                for (int i = 0; i < source.arrows.Count; i++)
                {
                    var arrow = source.arrows[i];
                    var indices = arrow?.indices;
                    if (indices == null || indices.Count == 0)
                    {
                        droppedChains++;
                        continue;
                    }

                    var remapped = new List<int>(indices.Count);
                    for (int j = 0; j < indices.Count; j++)
                    {
                        int oldIdx = indices[j];
                        if (oldIdx < 0 || oldIdx >= source.width * source.height)
                        {
                            droppedTiles++;
                            continue;
                        }

                        int x = oldIdx % source.width;
                        int y = oldIdx / source.width;
                        if (x < minX || x > maxX || y < minY || y > maxY)
                        {
                            droppedTiles++;
                            continue;
                        }

                        int nx = x - minX;
                        int ny = y - minY;
                        remapped.Add(nx + ny * newW);
                    }

                    if (remapped.Count < 2)
                    {
                        droppedChains++;
                        continue;
                    }

                    cropped.arrows.Add(new AuthoredArrowData
                    {
                        indices = remapped,
                        colorIndex = arrow.colorIndex
                    });
                }
            }

            if (droppedTiles > 0)
            {
                details = $"dropped arrow tiles={droppedTiles}";
                return false;
            }

            if (cropped.arrows.Count == 0)
            {
                details = $"no cropped arrows, droppedChains={droppedChains}";
                return false;
            }

            if (source.blockIndices != null)
            {
                var blocks = new HashSet<int>();
                for (int i = 0; i < source.blockIndices.Count; i++)
                {
                    int oldIdx = source.blockIndices[i];
                    if (oldIdx < 0 || oldIdx >= source.width * source.height)
                        continue;

                    int x = oldIdx % source.width;
                    int y = oldIdx / source.width;
                    if (x < minX || x > maxX || y < minY || y > maxY)
                        continue;

                    int nx = x - minX;
                    int ny = y - minY;
                    blocks.Add(nx + ny * newW);
                }
                cropped.blockIndices.AddRange(blocks);
            }

            details = $"croppedChains={cropped.arrows.Count}, droppedChains={droppedChains}, blocks={cropped.blockIndices.Count}";
            return true;
        }

        private static LevelDefinition CreateTightCropDefinition(
            LevelDefinition source,
            string id,
            AuthoredLevelData authored,
            Texture2D mask)
        {
            var output = ScriptableObject.CreateInstance<LevelDefinition>();
            output.levelId = id;
            output.name = id;
            output.source = LevelDefinition.LevelSource.Authored;
            output.board.width = authored.width;
            output.board.height = authored.height;
            output.board.seed = source != null ? source.board.seed : 0;

            if (source != null)
            {
                output.generation.initialMovableArrowCount = source.generation.initialMovableArrowCount;
                output.generation.targetDifficultyScore = source.generation.targetDifficultyScore;
                output.generation.fixedGenerationSeed = source.generation.fixedGenerationSeed;
                output.generation.minPathLen = Mathf.Max(2, source.generation.minPathLen);
                output.generation.maxPathLength = Mathf.Max(source.generation.maxPathLength, authored.width * authored.height);
                output.generation.twistiness = source.generation.twistiness;
                output.generation.validateWithGreedy = true;
                output.lose.blockedLoseLimit = source.lose.blockedLoseLimit;
                output.arrowColorMode = source.arrowColorMode;
                output.arrowColorMaskQuantizeSteps = source.arrowColorMaskQuantizeSteps;
                output.tintOnHit = source.tintOnHit;
                output.hitTint = source.hitTint;
                output.introSettings = source.introSettings;
                output.palette = source.palette;

                output.masking.alphaThreshold = source.masking.alphaThreshold;
                output.masking.useAlphaOnly = source.masking.useAlphaOnly;
                output.masking.luminanceThreshold = source.masking.luminanceThreshold;
                output.masking.useMaskToDefineBoardSize = source.masking.useMaskToDefineBoardSize;
            }

            output.generation.arrowCoverage = GetAuthoredArrowCount(authored) / (float)Mathf.Max(1, authored.width * authored.height);
            output.masking.spawnMask = mask;
            output.authoredLevel = authored;
            return output;
        }

        private static Texture2D FindMatchingAnimalBestMask(Texture2D sourceMask, int width, int height)
        {
            if (sourceMask == null)
                return null;

            string name = sourceMask.name;
            int dash = name.IndexOf('-');
            if (dash < 0 || dash >= name.Length - 1)
                return sourceMask;

            string shapeName = name.Substring(dash + 1);
            string path = $"Assets/ArrowMagic/Masks/ShapeAnimalBestPreview/MaskAnimalBest_{width}x{height}-{shapeName}.png";
            var mask = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            return mask != null ? mask : sourceMask;
        }

        private static bool SaveOrUpdateLevelDefinitionAsset(LevelDefinition output, string assetPath, out string details)
        {
            details = string.Empty;
            if (output == null || string.IsNullOrWhiteSpace(assetPath))
            {
                details = "invalid output asset";
                return false;
            }

            EnsureFolderExists(Path.GetDirectoryName(assetPath)?.Replace("\\", "/"));
            string assetName = Path.GetFileNameWithoutExtension(assetPath);
            output.name = assetName;

            var existing = AssetDatabase.LoadAssetAtPath<LevelDefinition>(assetPath);
            if (existing != null)
            {
                EditorUtility.CopySerialized(output, existing);
                existing.name = assetName;
                EditorUtility.SetDirty(existing);
                UnityEngine.Object.DestroyImmediate(output);
            }
            else
            {
                AssetDatabase.CreateAsset(output, assetPath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
            details = "saved";
            return true;
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Run Large Composite Mask Experiment")]
        public static void RunLargeCompositeMaskExperiment()
        {
            const string reportPath = "Temp/seedmask_large_composite_report.txt";
            const string maskPath = "Assets/ArrowMagic/Masks/Mask_84x60-BigBadgeSimple.png";
            const string compositeSeedPath = "Assets/ArrowMagic/SOData/Levels/Seeds/seed_mask_largecomposite_source_bigbadge.asset";
            const int width = 84;
            const int height = 60;

            var report = new List<string>
            {
                "SeedMaskPatch Large Composite Mask Experiment",
                "Goal=large mask from existing seed pool, around 200 chain groups",
                $"Mask={maskPath}",
                $"CompositeSeed={compositeSeedPath}"
            };

            Texture2D mask = CreateOrUpdateLargeBadgeMask(maskPath, width, height, out int maskArea, out string maskDetails);
            report.Add($"MaskDetails={maskDetails} | MaskArea={maskArea}/{width * height}");
            if (mask == null)
            {
                WriteReport(reportPath, report);
                Debug.LogError("[SeedMaskPatch] Large Composite Mask Experiment failed: mask creation failed.");
                return;
            }

            var parts = new[]
            {
                new CompositeSeedPart("Assets/ArrowMagic/SOData/Levels/Seeds/seed_Arrowz_level_134.asset", new Vector2Int(5, 6)),
                new CompositeSeedPart("Assets/ArrowMagic/SOData/Levels/Seeds/seed_Above300_level_500.asset", new Vector2Int(45, 7))
            };

            LevelDefinition compositeSeed = BuildLargeCompositeSeedAsset(compositeSeedPath, width, height, mask, parts, out string compositeDetails);
            report.Add($"CompositeDetails={compositeDetails}");
            if (compositeSeed == null)
            {
                WriteReport(reportPath, report);
                Debug.LogError("[SeedMaskPatch] Large Composite Mask Experiment failed: composite seed creation failed.");
                return;
            }

            var window = CreateInstance<SeedMaskPatchWindow>();
            ApplyRepairExperimentRuntimeSettings(window);
            window._seedDefinition = compositeSeed;
            window._mask = mask;
            window._placementMode = PlacementMode.Center;
            window._autoIfCenterFails = false;
            window._patchOutputMode = PatchOutputMode.GreedyRescue;
            window._inflateMaskForSolvability = false;
            window._maskInflationPasses = 0;
            window._adaptiveHealFillFallback = false;
            window._maskHealFillRatio = 0.72f;
            window._maskHealFillRatioFloor = 0.72f;
            window._maskHealFillFallbackSteps = 0;
            window._maxGreedyMovesMultiplier = 14;
            window._fallbackCleanupLoops = kRepairExperimentFallbackLoops;
            window._greedyTrimPerChain = kRepairExperimentTrimPerChain;
            window._preGreedyGeometryFixPasses = 12;
            window._allowGreedyFullChainRemoval = true;
            window._allowGreedyFallbackChainClear = true;
            window._outputPrefix = "seed_mask_largecomposite_bigbadge";

            Debug.Log("[SeedMaskPatch] Large Composite Mask Experiment start.");
            string result = string.Empty;
            if (!TryBuildBoardFromSeed(compositeSeed, out BoardState sourceBoard)
                || !window.TryReadMask(mask, true, false, 0, out bool[] maskCanSpawn))
            {
                report.Add("Process=False | Reason=Build composite board or read mask failed.");
                WriteReport(reportPath, report);
                Debug.LogError("[SeedMaskPatch] Large Composite Mask Experiment failed before fast path.");
                return;
            }

            if (!BuildMaskedBoardFromSeed(sourceBoard, maskCanSpawn, Vector2Int.zero, width, height, out BoardState workingBoard, out int preserved))
            {
                report.Add("Process=False | Reason=No composite arrows inside mask.");
                WriteReport(reportPath, report);
                Debug.LogError("[SeedMaskPatch] Large Composite Mask Experiment failed: no arrows inside mask.");
                return;
            }

            string clipDetails = string.Empty;
            if (!PreserveMaskedChainsBySplitting(sourceBoard, workingBoard, maskCanSpawn, Vector2Int.zero, out int keptTiles, out clipDetails))
            {
                report.Add($"SplitClip=False | {clipDetails}");
            }
            else
            {
                preserved = keptTiles;
                report.Add($"SplitClip=True | PreservedTiles={preserved} | {clipDetails}");
            }

            int clampRemoved = ClampBoardToMask(workingBoard, maskCanSpawn);
            report.Add($"ClampRemoved={clampRemoved}");

            var rawBoard = CloneBoard(workingBoard);
            LevelDefinition rawSeed = BuildAuthoredLevelDefinition(compositeSeed, mask, rawBoard, out string rawAuthoredDetails, "largecomposite_rawclip");
            if (rawSeed != null)
            {
                window._latestRawClipPath = window.SaveGeneratedSeedAsset(compositeSeed, mask, rawSeed, MakeRawClipOutputPrefix(window._outputPrefix));
            }
            report.Add($"Raw={window._latestRawClipPath} | RawAuthored={rawAuthoredDetails}");

            WriteReport(reportPath, report);

            int preTrimRemoved = TrimBoardChainsToTarget(ref workingBoard, maskCanSpawn, 212, out string preTrimDetails);
            report.Add($"PreTrim=removedChains={preTrimRemoved} | {preTrimDetails}");
            WriteReport(reportPath, report);

            int greedyMoves = Mathf.Max(512, CountBoardChains(workingBoard) * 6);
            bool greedyOk = TryFastDependencyTrimToGreedy(ref workingBoard, maskCanSpawn, greedyMoves, 140, 120, out string fastDetails);
            report.Add($"FastGreedy={greedyOk} | {fastDetails}");

            if (greedyOk)
            {
                LevelDefinition finalSeed = BuildAuthoredLevelDefinition(compositeSeed, mask, workingBoard, out string finalAuthoredDetails, "largecomposite_fast");
                report.Add($"FinalAuthored={finalAuthoredDetails}");
                if (finalSeed != null)
                    result = window.SaveGeneratedSeedAsset(compositeSeed, mask, finalSeed, window._outputPrefix);
            }
            else
            {
                if (TryBuildBoundaryExitFallbackBoard(
                    compositeSeed,
                    maskCanSpawn,
                    width,
                    height,
                    190,
                    out BoardState fallbackBoard,
                    out string fallbackDetails)
                    && GreedyValidator.TryClearAllByGreedy(
                        fallbackBoard,
                        new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty }),
                        Mathf.Max(512, CountBoardChains(fallbackBoard) * 6),
                        out _))
                {
                    report.Add($"BoundaryFallback=True | {fallbackDetails}");
                    LevelDefinition finalSeed = BuildAuthoredLevelDefinition(compositeSeed, mask, fallbackBoard, out string finalAuthoredDetails, "largecomposite_boundaryfallback");
                    report.Add($"FinalAuthored={finalAuthoredDetails}");
                    if (finalSeed != null)
                        result = window.SaveGeneratedSeedAsset(compositeSeed, mask, finalSeed, window._outputPrefix);
                }
                else
                {
                    report.Add($"BoundaryFallback=False | Reason=Fallback failed or greedy failed.");
                    report.Add("FinalSkipped=True | Reason=Fast greedy did not pass.");
                }
            }

            report.Add($"Final={result}");

            int finalFill = 0;
            int finalChains = 0;
            float finalRatio = 0f;
            if (!string.IsNullOrWhiteSpace(result))
            {
                var finalDef = AssetDatabase.LoadAssetAtPath<LevelDefinition>(result);
                if (finalDef != null)
                {
                    finalChains = CountAuthoredChains(finalDef.authoredLevel);
                    if (TryBuildBoardFromSeed(finalDef, out BoardState finalBoard))
                    {
                        finalFill = CountArrowTiles(finalBoard);
                        finalRatio = maskArea > 0 ? (float)finalFill / maskArea : 0f;
                    }
                }
            }

            int rawChains = 0;
            int rawFill = 0;
            if (!string.IsNullOrWhiteSpace(window._latestRawClipPath))
            {
                var rawDef = AssetDatabase.LoadAssetAtPath<LevelDefinition>(window._latestRawClipPath);
                if (rawDef != null)
                {
                    rawChains = CountAuthoredChains(rawDef.authoredLevel);
                    if (TryBuildBoardFromSeed(rawDef, out BoardState rawLoadedBoard))
                        rawFill = CountArrowTiles(rawLoadedBoard);
                }
            }

            report.Add($"RawStats=chains={rawChains}, fill={rawFill}");
            report.Add($"FinalStats=chains={finalChains}, fill={finalFill}/{maskArea}({finalRatio:0.000}), targetChainsAround=200");

            if (!string.IsNullOrWhiteSpace(result))
            {
                TrySyncDemoComparePack(compositeSeed, result, window._latestRawClipPath);
            }

            WriteReport(reportPath, report);
            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] Large Composite Mask Experiment finished. final={result}, chains={finalChains}, fill={finalFill}/{maskArea}({finalRatio:0.000})");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Run Hole Block Mask Experiment")]
        public static void RunHoleBlockMaskExperiment()
        {
            const string maskPath = "Assets/ArrowMagic/Masks/hole.png";
            const string reportPath = "Temp/seedmask_hole_block_report.txt";

            var report = new List<string>
            {
                "SeedMaskPatch Hole Block Mask Experiment",
                $"Mask={maskPath}",
                "Mode=clip-seed-then-add-interior-blocks"
            };

            EnsureTextureReadable(maskPath);
            var mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);
            if (mask == null)
            {
                report.Add("Failed=mask missing");
                WriteReport(reportPath, report);
                Debug.LogError($"[SeedMaskPatch] Hole Block Experiment mask missing: {maskPath}");
                return;
            }

            var window = CreateInstance<SeedMaskPatchWindow>();
            ApplyRepairExperimentRuntimeSettings(window);
            window._mask = mask;
            window._placementMode = PlacementMode.Center;
            window._autoIfCenterFails = false;
            window._patchOutputMode = PatchOutputMode.GreedyRescue;
            window._inflateMaskForSolvability = false;
            window._maskInflationPasses = 0;
            window._adaptiveHealFillFallback = false;
            window._maskHealFillRatio = 1f;
            window._maskHealFillRatioFloor = 1f;
            window._maskHealFillFallbackSteps = 0;
            window._maxGreedyMovesMultiplier = kRepairExperimentGreedyMultiplier;
            window._fallbackCleanupLoops = kRepairExperimentFallbackLoops;
            window._greedyTrimPerChain = kRepairExperimentTrimPerChain;
            window._preGreedyGeometryFixPasses = 10;
            window._allowGreedyFullChainRemoval = false;
            window._allowGreedyFallbackChainClear = false;
            window._candidateTopCount = 3;
            window._candidatePreEvalLimit = 48;
            window._candidatePreEvalTimeoutMs = 90000;
            window._candidateMinFillRatio = 0.85f;
            window._outputPrefix = "seed_mask_holeblock";

            if (!window.TryReadMask(mask, true, false, 0, out bool[] canSpawn))
            {
                report.Add("Failed=mask parse failed");
                WriteReport(reportPath, report);
                return;
            }

            var blockIndices = CollectInteriorTransparentBlockIndices(canSpawn, mask.width, mask.height);
            report.Add($"MaskStats=size={mask.width}x{mask.height}, spawn={CountMaskArea(canSpawn)}, blocks={blockIndices.Count}");

            const string seedPath = "Assets/ArrowMagic/SOData/Levels/Seeds/seed_Above300_level_954.asset";
            var chosenSeed = AssetDatabase.LoadAssetAtPath<LevelDefinition>(seedPath);
            if (chosenSeed == null)
            {
                report.Add($"Failed=seed missing {seedPath}");
                WriteReport(reportPath, report);
                Debug.LogError($"[SeedMaskPatch] Hole Block Experiment seed missing: {seedPath}");
                return;
            }

            if (!TryBuildBoardFromSeed(chosenSeed, out BoardState sourceBoard))
            {
                report.Add("Failed=source board build failed");
                WriteReport(reportPath, report);
                return;
            }

            if (!BuildMaskedBoardFromSeed(sourceBoard, canSpawn, Vector2Int.zero, mask.width, mask.height, out BoardState workingBoard, out int preserved))
            {
                report.Add("Failed=no seed arrows inside mask");
                WriteReport(reportPath, report);
                return;
            }

            string clipDetails = string.Empty;
            if (PreserveMaskedChainsBySplitting(sourceBoard, workingBoard, canSpawn, Vector2Int.zero, out int keptTiles, out clipDetails))
                preserved = keptTiles;
            report.Add($"Chosen={seedPath}");
            report.Add($"Clip=preserved={preserved} | {clipDetails}");

            int clampRemoved = ClampBoardToMask(workingBoard, canSpawn);
            ApplyBlockIndicesToBoard(workingBoard, blockIndices);
            report.Add($"ClampRemoved={clampRemoved}");

            bool blockRayClean = ApplyBlockRayCleanup(ref workingBoard, canSpawn, 48, 6, out int blockRayChanged, out string blockRayDetails);
            report.Add($"BlockRayCleanup={blockRayClean} | changed={blockRayChanged} | {blockRayDetails}");

            bool geometryClean = ApplyPreGreedyGeometryFix(ref workingBoard, canSpawn, 12, 6, out int geometryChanged, out string geometryDetails);
            report.Add($"PreGreedyGeometry={geometryClean} | changed={geometryChanged} | {geometryDetails}");

            var rawBoard = CloneBoard(workingBoard);
            LevelDefinition rawSeed = BuildAuthoredLevelDefinition(chosenSeed, mask, rawBoard, out string rawAuthoredDetails, "holeblock_rawclip");
            string rawBlockPath = string.Empty;
            if (rawSeed != null)
                rawBlockPath = window.SaveGeneratedSeedAsset(chosenSeed, mask, rawSeed, MakeRawClipOutputPrefix(window._outputPrefix));
            report.Add($"RawBlock={rawBlockPath} | {rawAuthoredDetails}");
            WriteReport(reportPath, report);

            int greedyMoves = Mathf.Max(512, CountBoardChains(workingBoard) * 8);
            bool greedyOk = TryFastDependencyTrimToGreedy(ref workingBoard, canSpawn, greedyMoves, 20, 160, out string fastDetails);
            report.Add($"FastGreedy={greedyOk} | {fastDetails}");

            string midBlockPath = string.Empty;
            string finalBlockPath = string.Empty;
            if (greedyOk)
            {
                var midBoard = CloneBoard(workingBoard);
                int midRefillTarget = ComputeTargetFillFromMask(canSpawn, 0.72f);
                bool midRefillOk = TryGreedySafePostCoreRefill(ref midBoard, canSpawn, greedyMoves, midRefillTarget, out string midRefillDetails);
                report.Add($"MidPostGreedyRefill={midRefillOk} | target={midRefillTarget} | {midRefillDetails}");
                ApplyBlockIndicesToBoard(midBoard, blockIndices);
                LevelDefinition midSeed = BuildAuthoredLevelDefinition(chosenSeed, mask, midBoard, out string midAuthoredDetails, "holeblock_mid");
                report.Add($"MidAuthored={midAuthoredDetails}");
                if (midSeed != null)
                    midBlockPath = window.SaveGeneratedSeedAsset(chosenSeed, mask, midSeed, $"{window._outputPrefix}_mid");

                int refillTarget = ComputeTargetFillFromMask(canSpawn, 0.88f);
                bool refillOk = TryGreedySafePostCoreRefill(ref workingBoard, canSpawn, greedyMoves, refillTarget, out string refillDetails);
                report.Add($"PostGreedyRefill={refillOk} | target={refillTarget} | {refillDetails}");
                ApplyBlockIndicesToBoard(workingBoard, blockIndices);
                LevelDefinition finalSeed = BuildAuthoredLevelDefinition(chosenSeed, mask, workingBoard, out string finalAuthoredDetails, "holeblock_fast");
                report.Add($"FinalAuthored={finalAuthoredDetails}");
                if (finalSeed != null)
                    finalBlockPath = window.SaveGeneratedSeedAsset(chosenSeed, mask, finalSeed, window._outputPrefix);
            }
            else
            {
                report.Add("FinalSkipped=True | Reason=Fast greedy did not pass.");
            }

            report.Add($"MidBlock={midBlockPath}");
            report.Add($"FinalBlock={finalBlockPath}");

            if (!string.IsNullOrWhiteSpace(finalBlockPath))
                TrySyncDemoComparePack(chosenSeed, finalBlockPath, !string.IsNullOrWhiteSpace(midBlockPath) ? midBlockPath : rawBlockPath);

            WriteReport(reportPath, report);
            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] Hole Block Experiment finished. final={finalBlockPath}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Run Hole Block Multi Seed Experiment")]
        public static void RunHoleBlockMultiSeedExperiment()
        {
            const string maskPath = "Assets/ArrowMagic/Masks/hole.png";
            const string reportPath = "Temp/seedmask_hole_block_multiseed_report.txt";

            var report = new List<string>
            {
                "SeedMaskPatch Hole Block Multi Seed Experiment",
                $"Mask={maskPath}",
                "Mode=multi-seed-preview-then-deep-run"
            };

            var mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);
            if (mask == null)
            {
                report.Add($"Failed=mask missing {maskPath}");
                WriteReport(reportPath, report);
                Debug.LogError($"[SeedMaskPatch] Hole Block Multi Seed mask missing: {maskPath}");
                return;
            }

            EnsureTextureReadable(maskPath);
            mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);

            var window = CreateInstance<SeedMaskPatchWindow>();
            ApplyRepairExperimentRuntimeSettings(window);
            window._mask = mask;
            window._placementMode = PlacementMode.Auto;
            window._autoIfCenterFails = true;
            window._patchOutputMode = PatchOutputMode.GreedyRescue;
            window._inflateMaskForSolvability = false;
            window._maskInflationPasses = 0;
            window._adaptiveHealFillFallback = false;
            window._maskHealFillRatio = 1f;
            window._maskHealFillRatioFloor = 1f;
            window._maskHealFillFallbackSteps = 0;
            window._maxGreedyMovesMultiplier = kRepairExperimentGreedyMultiplier;
            window._fallbackCleanupLoops = kRepairExperimentFallbackLoops;
            window._greedyTrimPerChain = kRepairExperimentTrimPerChain;
            window._preGreedyGeometryFixPasses = 10;
            window._allowGreedyFullChainRemoval = false;
            window._allowGreedyFallbackChainClear = false;
            window._candidateMinFillRatio = 0.80f;
            window._outputPrefix = "seed_mask_holeblock_multiseed";

            if (!window.TryReadMask(mask, true, false, 0, out bool[] canSpawn))
            {
                report.Add("Failed=mask parse failed");
                WriteReport(reportPath, report);
                return;
            }

            var blockIndices = CollectInteriorTransparentBlockIndices(canSpawn, mask.width, mask.height);
            int maskArea = CountMaskArea(canSpawn);
            report.Add($"MaskStats=size={mask.width}x{mask.height}, spawn={maskArea}, blocks={blockIndices.Count}");

            var seedEntries = CollectHoleBlockSeedEntries(mask.width, mask.height);
            report.Add($"SeedPool=matchedForPreview={seedEntries.Count}");
            WriteReport(reportPath, report);

            var previews = new List<HoleBlockSeedCandidate>();
            int previewLimit = Math.Min(120, seedEntries.Count);
            for (int i = 0; i < previewLimit; i++)
            {
                var entry = seedEntries[i];
                if (entry?.Seed == null)
                    continue;

                if (TryEvaluateHoleBlockSeedPreview(entry.Seed, mask, canSpawn, blockIndices, 4, out var candidate))
                    previews.Add(candidate);
            }

            previews.Sort((a, b) => b.Score.CompareTo(a.Score));
            report.Add($"Preview=checked={previewLimit}, valid={previews.Count}");
            int previewReportCount = Math.Min(12, previews.Count);
            for (int i = 0; i < previewReportCount; i++)
            {
                var c = previews[i];
                report.Add($"PreviewTop{i + 1}=seed={c.SeedPath}, offset={c.Offset.x},{c.Offset.y}, preserved={c.Preserved}, cleanFill={c.CleanFill}, chains={c.CleanChains}, blockHits={c.BlockHits}, boundaryEmpty={c.BoundaryEmpty}, score={c.Score} | {c.Details}");
            }
            WriteReport(reportPath, report);

            var results = new List<HoleBlockRunResult>();
            int deepLimit = Math.Min(6, previews.Count);
            for (int i = 0; i < deepLimit; i++)
            {
                var candidate = previews[i];
                if (candidate?.Seed == null)
                    continue;

                if (TryRunHoleBlockSeedDeep(window, candidate, mask, canSpawn, blockIndices, 0.92f, 0.92f, out var result))
                {
                    results.Add(result);
                    report.Add($"Deep{i + 1}=seed={candidate.SeedPath}, offset={candidate.Offset.x},{candidate.Offset.y}, raw={result.RawPath}, final={result.FinalPath}, fastFill={result.FastFill}, finalFill={result.FinalFill}, finalHits={result.FinalBlockHits}, score={result.Score} | {result.Details}");
                }
                else
                {
                    report.Add($"Deep{i + 1}=seed={candidate.SeedPath}, offset={candidate.Offset.x},{candidate.Offset.y}, failed");
                }

                WriteReport(reportPath, report);
            }

            results.Sort((a, b) => b.Score.CompareTo(a.Score));
            var best = results.Count > 0 ? results[0] : null;
            if (best != null && !string.IsNullOrWhiteSpace(best.FinalPath))
            {
                var second = results.Count > 1 ? results[1] : null;
                if (second != null && !string.IsNullOrWhiteSpace(second.FinalPath))
                    TrySyncDemoFinalPairPack(best.Candidate.Seed, second.FinalPath, best.FinalPath);
                else
                    TrySyncDemoComparePack(best.Candidate.Seed, best.FinalPath);

                report.Add($"Best=seed={best.Candidate.SeedPath}, final={best.FinalPath}, finalFill={best.FinalFill}/{maskArea}, score={best.Score}");
                if (second != null)
                    report.Add($"Second=seed={second.Candidate.SeedPath}, final={second.FinalPath}, finalFill={second.FinalFill}/{maskArea}, score={second.Score}");
            }
            else
            {
                report.Add("Best=None");
            }

            WriteReport(reportPath, report);
            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] Hole Block Multi Seed Experiment finished. best={best?.FinalPath}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Production/Create Hole Masks")]
        public static void CreateProductionHoleMasksMenu()
        {
            var report = new List<string>();
            CreateOrUpdateProductionHoleMasks(report);
            WriteReport($"{kProductionHoleReportFolder}/HoleLongOuterStrong_Production_Masks_Report.txt", report);
            AssetDatabase.Refresh();
            Debug.Log("[SeedMaskPatch] Production hole masks created.");
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Production/Run HoleMask Production Batch")]
        public static void RunHoleMaskProductionBatch()
        {
            string reportPath = $"{kProductionHoleReportFolder}/HoleLongOuterStrong_Production_Report.txt";
            var report = new List<string>
            {
                "HoleLongOuterStrong Production Batch",
                $"SourcePacks={kProductionHoleSourcePackFolder}",
                $"CandidateOutput={kProductionHoleCandidateFolder}",
                $"CandidatesPerMask={kProductionCandidatesPerMask}",
                $"DeepAttemptLimit={kProductionDeepAttemptLimit}",
                $"TargetFillRatio={kProductionFinalFillRatio:0.00}",
                "Rules=GreedyPass,BlockRayHits0,Chains40SmallOr50-120,HighFinalOnly"
            };

            CreateOrUpdateProductionHoleMasks(report);
            AssetDatabase.Refresh();
            ClearProductionCandidateOutput(report);

            var specs = GetProductionHoleMaskSpecs();
            var acceptedPaths = new List<string>();
            var acceptedSummary = new List<string>();

            var window = CreateInstance<SeedMaskPatchWindow>();
            ApplyRepairExperimentRuntimeSettings(window);
            window._placementMode = PlacementMode.Auto;
            window._autoIfCenterFails = true;
            window._patchOutputMode = PatchOutputMode.GreedyRescue;
            window._inflateMaskForSolvability = false;
            window._maskInflationPasses = 0;
            window._adaptiveHealFillFallback = false;
            window._maskHealFillRatio = 1f;
            window._maskHealFillRatioFloor = 1f;
            window._maskHealFillFallbackSteps = 0;
            window._maxGreedyMovesMultiplier = kRepairExperimentGreedyMultiplier;
            window._fallbackCleanupLoops = kRepairExperimentFallbackLoops;
            window._greedyTrimPerChain = kRepairExperimentTrimPerChain;
            window._preGreedyGeometryFixPasses = 10;
            window._allowGreedyFullChainRemoval = false;
            window._allowGreedyFallbackChainClear = false;
            window._candidateMinFillRatio = 0.90f;
            window._outputFolder = kProductionHoleCandidateFolder;
            window._exportRawIfGreedyFails = false;
            window.EnsureOutputFolder();

            for (int si = 0; si < specs.Count; si++)
            {
                var spec = specs[si];
                string maskPath = GetProductionHoleMaskPath(spec);
                var mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);
                if (mask == null)
                {
                    report.Add($"Mask[{spec.Id}]=Failed missing {maskPath}");
                    WriteReport(reportPath, report);
                    continue;
                }

                EnsureTextureReadable(maskPath);
                mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);
                window._mask = mask;
                window._outputPrefix = $"hole_prod_{spec.Id}";

                if (!window.TryReadMask(mask, true, false, 0, out bool[] canSpawn))
                {
                    report.Add($"Mask[{spec.Id}]=Failed parse");
                    WriteReport(reportPath, report);
                    continue;
                }

                var blockIndices = CollectInteriorTransparentBlockIndices(canSpawn, mask.width, mask.height);
                int maskArea = CountMaskArea(canSpawn);
                report.Add($"Mask[{si + 1}/{specs.Count}]={spec.Id}, size={mask.width}x{mask.height}, spawn={maskArea}, blocks={blockIndices.Count}, path={maskPath}");

                var seedEntries = CollectHoleBlockSeedEntries(mask.width, mask.height);
                var previews = new List<HoleBlockSeedCandidate>();
                int previewLimit = Math.Min(kProductionPreviewSeedLimit, seedEntries.Count);
                for (int i = 0; i < previewLimit; i++)
                {
                    var entry = seedEntries[i];
                    if (entry?.Seed == null)
                        continue;

                    if (TryEvaluateHoleBlockSeedPreview(entry.Seed, mask, canSpawn, blockIndices, 4, out var candidate))
                        previews.Add(candidate);
                }

                previews.Sort((a, b) => b.Score.CompareTo(a.Score));
                report.Add($"  Preview=checked={previewLimit}, valid={previews.Count}");
                for (int i = 0; i < Math.Min(5, previews.Count); i++)
                {
                    var c = previews[i];
                    report.Add($"  PreviewTop{i + 1}=seed={c.SeedPath}, offset={c.Offset.x},{c.Offset.y}, fill={c.CleanFill}, chains={c.CleanChains}, score={c.Score}");
                }

                int acceptedForMask = 0;
                int deepAttempts = 0;
                int deepLimit = Math.Min(kProductionDeepAttemptLimit, previews.Count);
                for (int i = 0; i < deepLimit; i++)
                {
                    if (acceptedForMask >= kProductionCandidatesPerMask)
                        break;

                    deepAttempts++;
                    var candidate = previews[i];
                    string outputStem = BuildProductionOutputStem(spec.Id, i + 1, candidate.Seed);
                    if (!TryRunHoleBlockSeedDeep(window, candidate, mask, canSpawn, blockIndices, kProductionFinalFillRatio, kProductionFinalFillRatio, out var result, outputStem)
                        || string.IsNullOrWhiteSpace(result.FinalPath))
                    {
                        report.Add($"  DeepAttempt{i + 1}=failed seed={candidate.SeedPath}");
                        WriteReport(reportPath, report);
                        continue;
                    }

                    var finalDef = AssetDatabase.LoadAssetAtPath<LevelDefinition>(result.FinalPath);
                    int chains = finalDef != null ? CountAuthoredChains(finalDef.authoredLevel) : 0;
                    int minChains = GetProductionMinChains(maskArea);
                    bool passChains = chains >= minChains && chains <= 120;
                    bool passFill = maskArea > 0 && result.FinalFill >= Mathf.CeilToInt(maskArea * 0.90f);
                    bool passHits = result.FinalBlockHits == 0;
                    bool accepted = passChains && passFill && passHits;

                    report.Add($"  DeepAttempt{i + 1}=accepted={accepted}, seed={candidate.SeedPath}, final={result.FinalPath}, fill={result.FinalFill}/{maskArea}, chains={chains}, minChains={minChains}, hits={result.FinalBlockHits}, score={result.Score}");
                    if (accepted)
                    {
                        acceptedForMask++;
                        acceptedPaths.Add(result.FinalPath);
                        acceptedSummary.Add($"{spec.Id}: {Path.GetFileNameWithoutExtension(result.FinalPath)} fill={result.FinalFill}/{maskArea} chains={chains}");
                    }
                    else
                    {
                        AssetDatabase.DeleteAsset(result.FinalPath);
                    }

                    WriteReport(reportPath, report);
                }

                report.Add($"  AcceptedForMask={acceptedForMask}/{kProductionCandidatesPerMask}, attempts={deepAttempts}/{deepLimit}");
                WriteReport(reportPath, report);
            }

            if (TrySyncProductionCandidatesPack(acceptedPaths, out string packDetails))
                report.Add($"CandidatesPack={kProductionHoleCandidatesPackPath} | {packDetails}");
            else
                report.Add($"CandidatesPackFailed={packDetails}");

            report.Add($"AcceptedTotal={acceptedPaths.Count}");
            for (int i = 0; i < acceptedSummary.Count; i++)
                report.Add($"Accepted[{i + 1}]={acceptedSummary[i]}");

            WriteReport(reportPath, report);
            AssetDatabase.Refresh();
            Debug.Log($"[SeedMaskPatch] HoleMask Production Batch finished. accepted={acceptedPaths.Count}");
        }

        private static List<ProductionHoleMaskSpec> GetProductionHoleMaskSpecs()
        {
            return new List<ProductionHoleMaskSpec>
            {
                new ProductionHoleMaskSpec("22x34_long", 22, 34, "long"),
                new ProductionHoleMaskSpec("24x36_long", 24, 36, "long"),
                new ProductionHoleMaskSpec("24x40_long", 24, 40, "long"),
                new ProductionHoleMaskSpec("26x38_long", 26, 38, "long"),
                new ProductionHoleMaskSpec("26x42_long", 26, 42, "long"),
                new ProductionHoleMaskSpec("28x40_long", 28, 40, "long"),
                new ProductionHoleMaskSpec("28x44_long", 28, 44, "long"),
                new ProductionHoleMaskSpec("30x46_long", 30, 46, "long")
            };
        }

        private static void ClearProductionCandidateOutput(List<string> report)
        {
            EnsureFolderExists(kProductionHoleCandidateFolder);
            string fullFolder = Path.GetFullPath(Path.Combine(Application.dataPath, "..", kProductionHoleCandidateFolder));
            if (!Directory.Exists(fullFolder))
            {
                report?.Add("CandidateFolderCleared=0");
                return;
            }

            int deleted = 0;
            foreach (string fullPath in Directory.GetFiles(fullFolder, "*.asset", SearchOption.TopDirectoryOnly))
            {
                string assetPath = FullPathToAssetPath(fullPath);
                if (string.IsNullOrWhiteSpace(assetPath))
                    continue;

                if (AssetDatabase.DeleteAsset(assetPath))
                    deleted++;
            }

            report?.Add($"CandidateFolderCleared={deleted}");
        }

        private static string GetProductionHoleMaskPath(ProductionHoleMaskSpec spec)
        {
            return $"{kProductionHoleMaskFolder}/hole_shell_{spec.Id}.png";
        }

        private static void CreateOrUpdateProductionHoleMasks(List<string> report)
        {
            EnsureFolderExists(kProductionHoleMaskFolder);
            EnsureFolderExists(kProductionHoleCandidateFolder);
            EnsureFolderExists(kProductionHoleSelectedFolder);
            EnsureFolderExists(kProductionHoleReportFolder);
            EnsureFolderExists(kProductionHolePackFolder);

            var specs = GetProductionHoleMaskSpecs();
            for (int i = 0; i < specs.Count; i++)
            {
                var spec = specs[i];
                string path = GetProductionHoleMaskPath(spec);
                if (CreateOrUpdateProductionHoleMask(spec, path, out int shellCells, out int holeCells, out string details))
                {
                    report?.Add($"MaskCreated={path}, size={spec.Width}x{spec.Height}, shape={spec.Shape}, shell={shellCells}, hole={holeCells} | {details}");
                }
                else
                {
                    report?.Add($"MaskCreateFailed={path} | {details}");
                }
            }
        }

        private static bool CreateOrUpdateProductionHoleMask(
            ProductionHoleMaskSpec spec,
            string assetPath,
            out int shellCells,
            out int holeCells,
            out string details)
        {
            shellCells = 0;
            holeCells = 0;
            details = string.Empty;
            if (spec == null || spec.Width <= 0 || spec.Height <= 0 || string.IsNullOrWhiteSpace(assetPath))
            {
                details = "invalid spec";
                return false;
            }

            int holeX0 = (spec.Width - kProductionHoleWidth) / 2;
            int holeY0 = (spec.Height - kProductionHoleHeight) / 2;
            if (holeX0 < 3 || holeY0 < 3 || holeX0 + kProductionHoleWidth > spec.Width - 3 || holeY0 + kProductionHoleHeight > spec.Height - 3)
            {
                details = $"fixed hole too large for shell margins: hole={kProductionHoleWidth}x{kProductionHoleHeight}, origin={holeX0},{holeY0}";
                return false;
            }

            var texture = new Texture2D(spec.Width, spec.Height, TextureFormat.RGBA32, false);
            var clear = new Color32(0, 0, 0, 0);
            var fill = new Color32(255, 255, 255, 255);

            for (int y = 0; y < spec.Height; y++)
            {
                for (int x = 0; x < spec.Width; x++)
                {
                    bool shell = IsProductionShellCell(spec, x, y);
                    bool hole = x >= holeX0 && x < holeX0 + kProductionHoleWidth
                        && y >= holeY0 && y < holeY0 + kProductionHoleHeight;

                    if (shell && !hole)
                    {
                        texture.SetPixel(x, y, fill);
                        shellCells++;
                    }
                    else
                    {
                        texture.SetPixel(x, y, clear);
                        if (hole)
                            holeCells++;
                    }
                }
            }

            texture.Apply(false, false);

            string fullPath = Path.GetFullPath(Path.Combine(Application.dataPath, "..", assetPath));
            string dir = Path.GetDirectoryName(fullPath);
            if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllBytes(fullPath, texture.EncodeToPNG());
            DestroyImmediate(texture);
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
            EnsureTextureReadable(assetPath);
            details = $"fixedHole={kProductionHoleWidth}x{kProductionHoleHeight}, holeOrigin={holeX0},{holeY0}";
            return true;
        }

        private static bool IsProductionShellCell(ProductionHoleMaskSpec spec, int x, int y)
        {
            int w = spec.Width;
            int h = spec.Height;
            if (x <= 0 || y <= 0 || x >= w - 1 || y >= h - 1)
                return false;

            string shape = spec.Shape ?? "standard";
            int cornerCut = Math.Max(2, Math.Min(w, h) / 7);
            if (x + y < cornerCut)
                return false;
            if ((w - 1 - x) + y < cornerCut)
                return false;
            if (x + (h - 1 - y) < cornerCut)
                return false;
            if ((w - 1 - x) + (h - 1 - y) < cornerCut)
                return false;

            if (string.Equals(shape, "long", StringComparison.OrdinalIgnoreCase))
            {
                float ny = (y + 0.5f) / h;
                int taper = ny < 0.18f
                    ? Mathf.RoundToInt(Mathf.Lerp(2f, 0f, ny / 0.18f))
                    : ny > 0.82f
                        ? Mathf.RoundToInt(Mathf.Lerp(0f, 2f, (ny - 0.82f) / 0.18f))
                        : 0;
                return x > taper && x < w - 1 - taper;
            }

            if (string.Equals(shape, "wide", StringComparison.OrdinalIgnoreCase))
            {
                float nx = (x + 0.5f) / w;
                int taper = nx < 0.16f
                    ? Mathf.RoundToInt(Mathf.Lerp(2f, 0f, nx / 0.16f))
                    : nx > 0.84f
                        ? Mathf.RoundToInt(Mathf.Lerp(0f, 2f, (nx - 0.84f) / 0.16f))
                        : 0;
                return y > taper && y < h - 1 - taper;
            }

            if (string.Equals(shape, "slant", StringComparison.OrdinalIgnoreCase))
            {
                if (x <= 3 && y >= h - 7 && y - (h - 7) > x)
                    return false;
                if (x >= w - 5 && y <= 6 && (w - 1 - x) + y < 5)
                    return false;
                if (x >= w - 4 && y >= h - 8 && y - (h - 8) > (w - 1 - x))
                    return false;
            }

            return true;
        }

        private static string BuildProductionOutputStem(string maskId, int rank, LevelDefinition seed)
        {
            string seedName = seed != null && !string.IsNullOrWhiteSpace(seed.name)
                ? seed.name
                : seed != null && !string.IsNullOrWhiteSpace(seed.levelId)
                    ? seed.levelId
                    : "seed";

            string safeMask = ShortenFileToken(SanitizeName(maskId), 32);
            string safeSeed = ShortenFileToken(SanitizeName(seedName), 44);
            string time = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            return $"hole_prod_{safeMask}_r{rank:00}_{safeSeed}_{time}";
        }

        private static int GetProductionMinChains(int maskArea)
        {
            return maskArea <= 300 ? 40 : 50;
        }

        private static string ShortenFileToken(string value, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "item";

            value = value.Trim();
            if (maxLength <= 0 || value.Length <= maxLength)
                return value;

            return value.Substring(0, maxLength).TrimEnd('_', '-', ' ');
        }

        private static string FullPathToAssetPath(string fullPath)
        {
            if (string.IsNullOrWhiteSpace(fullPath))
                return string.Empty;

            string normalized = fullPath.Replace("\\", "/");
            string assetsRoot = Application.dataPath.Replace("\\", "/");
            if (!normalized.StartsWith(assetsRoot, StringComparison.OrdinalIgnoreCase))
                return string.Empty;

            return "Assets" + normalized.Substring(assetsRoot.Length);
        }

        private static bool TrySyncProductionCandidatesPack(List<string> levelPaths, out string details)
        {
            details = string.Empty;
            if (levelPaths == null || levelPaths.Count == 0)
            {
                details = "no levels";
                return false;
            }

            EnsureFolderExists(kProductionHolePackFolder);
            var levels = new List<LevelDefinition>();
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < levelPaths.Count; i++)
            {
                string path = levelPaths[i];
                if (string.IsNullOrWhiteSpace(path) || !seen.Add(path))
                    continue;

                var level = AssetDatabase.LoadAssetAtPath<LevelDefinition>(path);
                if (level != null)
                    levels.Add(level);
            }

            if (levels.Count == 0)
            {
                details = "no loadable levels";
                return false;
            }

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(kProductionHoleCandidatesPackPath);
            bool isNewPack = pack == null;
            if (pack == null)
                pack = ScriptableObject.CreateInstance<LevelPack>();

            pack.packId = "HoleLongOuterStrong_Production_Candidates";
            pack.displayName = $"Hole Long OuterStrong Candidates ({levels.Count})";
            pack.levels = levels.ToArray();
            EditorUtility.SetDirty(pack);

            if (isNewPack)
                AssetDatabase.CreateAsset(pack, kProductionHoleCandidatesPackPath);

            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(kProductionHoleCandidatesPackPath);
            details = $"levels={levels.Count}";
            return true;
        }

        private static List<SeedMatchEntry> CollectHoleBlockSeedEntries(int maskW, int maskH)
        {
            var entries = new List<SeedMatchEntry>();
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string[] packGuids = AssetDatabase.FindAssets("t:LevelPack", new[] { kProductionHoleSourcePackFolder });
            Array.Sort(packGuids, StringComparer.OrdinalIgnoreCase);
            for (int pi = 0; pi < packGuids.Length; pi++)
            {
                string packPath = AssetDatabase.GUIDToAssetPath(packGuids[pi]);
                var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(packPath);
                if (pack?.levels == null)
                    continue;

                for (int i = 0; i < pack.levels.Length; i++)
                {
                    var seed = pack.levels[i];
                    string path = AssetDatabase.GetAssetPath(seed);
                    if (seed == null || string.IsNullOrWhiteSpace(path) || !seen.Add(path))
                        continue;

                    if (IsGeneratedSeedDefinition(path, seed))
                        continue;

                    int width = seed.board != null ? seed.board.width : 0;
                    int height = seed.board != null ? seed.board.height : 0;
                    if (width <= 0 || height <= 0)
                        continue;

                    int sizeScore = Math.Abs(width - maskW) * 12
                        + Math.Abs(height - maskH) * 12
                        + Math.Abs(width * height - maskW * maskH) / 8;

                    entries.Add(new SeedMatchEntry
                    {
                        Seed = seed,
                        CachedPath = path,
                        SizeScore = sizeScore
                    });
                }
            }

            entries.Sort((a, b) =>
            {
                int cmp = a.SizeScore.CompareTo(b.SizeScore);
                if (cmp != 0)
                    return cmp;

                return string.Compare(a.CachedPath, b.CachedPath, StringComparison.OrdinalIgnoreCase);
            });

            return entries;
        }

        private static bool TryEvaluateHoleBlockSeedPreview(
            LevelDefinition seed,
            Texture2D mask,
            bool[] canSpawn,
            List<int> blockIndices,
            int offsetLimit,
            out HoleBlockSeedCandidate result)
        {
            result = null;
            if (seed == null || mask == null || canSpawn == null)
                return false;

            if (!TryBuildBoardFromSeed(seed, out BoardState sourceBoard))
                return false;

            var offsets = BuildPlacementOffsets(sourceBoard.width, sourceBoard.height, mask.width, mask.height, PlacementMode.Auto, true);
            int limit = Math.Min(Math.Max(1, offsetLimit), offsets.Count);
            BoardState bestBoard = null;
            Vector2Int bestOffset = default;
            int bestPreserved = 0;
            string bestDetails = string.Empty;
            int bestScore = int.MinValue;
            int bestHits = int.MaxValue;
            int bestBoundaryEmpty = int.MaxValue;
            int bestChains = 0;
            int bestFill = 0;

            for (int i = 0; i < limit; i++)
            {
                Vector2Int offset = offsets[i];
                if (!BuildMaskedBoardFromSeed(sourceBoard, canSpawn, offset, mask.width, mask.height, out BoardState board, out int preserved))
                    continue;

                int clampRemoved = ClampBoardToMask(board, canSpawn);
                ApplyBlockIndicesToBoard(board, blockIndices);

                int fill = CountArrowTiles(board);
                int chains = CountBoardChains(board);
                int hits = 0;
                int boundaryEmpty = CountEmptyMaskBoundaryCells(board, canSpawn);
                int boundaryScore = ComputeMaskBoundaryArrowScore(board, canSpawn);
                int score = fill * 18
                    + preserved * 2
                    + chains * 18
                    + boundaryScore
                    - boundaryEmpty * 6
                    - clampRemoved * 2;

                if (score > bestScore)
                {
                    bestBoard = board;
                    bestOffset = offset;
                    bestPreserved = preserved;
                    bestScore = score;
                    bestHits = hits;
                    bestBoundaryEmpty = boundaryEmpty;
                    bestChains = chains;
                    bestFill = fill;
                    bestDetails = $"quickPreview=true, clampRemoved={clampRemoved}";
                }
            }

            if (bestBoard == null)
                return false;

            result = new HoleBlockSeedCandidate
            {
                Seed = seed,
                SeedPath = GetAssetPath(seed),
                Offset = bestOffset,
                Preserved = bestPreserved,
                CleanFill = bestFill,
                CleanChains = bestChains,
                BlockHits = bestHits,
                BoundaryEmpty = bestBoundaryEmpty,
                Score = bestScore,
                Details = bestDetails
            };
            return true;
        }

        private static bool TryRunHoleBlockSeedDeep(
            SeedMaskPatchWindow window,
            HoleBlockSeedCandidate candidate,
            Texture2D mask,
            bool[] canSpawn,
            List<int> blockIndices,
            float midRatio,
            float finalRatio,
            out HoleBlockRunResult result,
            string finalOutputStem = null)
        {
            result = null;
            if (window == null || candidate?.Seed == null || mask == null || canSpawn == null)
                return false;

            if (!TryBuildBoardFromSeed(candidate.Seed, out BoardState sourceBoard))
                return false;

            if (!BuildMaskedBoardFromSeed(sourceBoard, canSpawn, candidate.Offset, mask.width, mask.height, out BoardState workingBoard, out int preserved))
                return false;

            string splitDetails = string.Empty;
            if (PreserveMaskedChainsBySplitting(sourceBoard, workingBoard, canSpawn, candidate.Offset, out int keptTiles, out splitDetails))
                preserved = keptTiles;

            int clampRemoved = ClampBoardToMask(workingBoard, canSpawn);
            ApplyBlockIndicesToBoard(workingBoard, blockIndices);
            bool blockClean = ApplyBlockRayCleanup(ref workingBoard, canSpawn, 48, 6, out int blockChanged, out string blockDetails);
            bool geometryClean = ApplyPreGreedyGeometryFix(ref workingBoard, canSpawn, 12, 6, out int geometryChanged, out string geometryDetails);

            string rawAuthoredDetails = "raw skipped";
            string rawPath = string.Empty;
            if (window._exportRawIfGreedyFails)
            {
                var rawBoard = CloneBoard(workingBoard);
                LevelDefinition rawSeed = BuildAuthoredLevelDefinition(candidate.Seed, mask, rawBoard, out rawAuthoredDetails, "holeblock_multiseed_rawclip");
                if (rawSeed != null)
                    rawPath = window.SaveGeneratedSeedAsset(candidate.Seed, mask, rawSeed, MakeRawClipOutputPrefix(window._outputPrefix));
            }

            int greedyMoves = Mathf.Max(512, CountBoardChains(workingBoard) * 8);
            bool greedyOk = TryFastDependencyTrimToGreedy(ref workingBoard, canSpawn, greedyMoves, 20, 160, out string fastDetails);
            if (!greedyOk)
            {
                result = new HoleBlockRunResult
                {
                    Candidate = candidate,
                    RawPath = rawPath,
                    Details = $"greedy failed | preserved={preserved}, split={splitDetails}, clamp={clampRemoved}, block={blockClean}/{blockChanged}:{blockDetails}, geometry={geometryClean}/{geometryChanged}:{geometryDetails}, raw={rawAuthoredDetails}, fast={fastDetails}"
                };
                return false;
            }

            int fastFill = CountArrowTiles(workingBoard);

            int finalTarget = ComputeTargetFillFromMask(canSpawn, finalRatio);
            bool finalRefillOk = TryGreedySafePostCoreRefill(ref workingBoard, canSpawn, greedyMoves, finalTarget, out string finalRefillDetails);
            bool finalBlockClean = ApplyBlockRayCleanup(ref workingBoard, canSpawn, 24, 6, out int finalBlockChanged, out string finalBlockDetails);
            bool finalGreedyOk = GreedyValidator.TryClearAllByGreedy(
                workingBoard,
                new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty }),
                greedyMoves,
                out _);
            if (!finalGreedyOk)
                finalGreedyOk = TryFastDependencyTrimToGreedy(ref workingBoard, canSpawn, greedyMoves, 20, 60, out _);

            ApplyBlockIndicesToBoard(workingBoard, blockIndices);
            int finalFill = CountArrowTiles(workingBoard);
            int finalHits = CountBlockRayBlockedHeads(workingBoard, canSpawn);
            string finalPath = string.Empty;
            if (finalGreedyOk)
            {
                LevelDefinition finalSeed = BuildAuthoredLevelDefinition(candidate.Seed, mask, workingBoard, out string finalAuthoredDetails, "holeblock_multiseed_final");
                if (finalSeed != null)
                    finalPath = string.IsNullOrWhiteSpace(finalOutputStem)
                        ? window.SaveGeneratedSeedAsset(candidate.Seed, mask, finalSeed, window._outputPrefix)
                        : window.SaveGeneratedSeedAssetWithFileName(finalSeed, finalOutputStem);
                finalRefillDetails = $"{finalRefillDetails} | finalBlock={finalBlockClean}/{finalBlockChanged}:{finalBlockDetails} | finalAuthored={finalAuthoredDetails}";
            }

            int score = finalFill * 16
                + fastFill * 3
                + candidate.CleanFill
                - finalHits * 1200
                - blockChanged * 20
                - geometryChanged * 16;

            result = new HoleBlockRunResult
            {
                Candidate = candidate,
                RawPath = rawPath,
                MidPath = string.Empty,
                FinalPath = finalPath,
                FastFill = fastFill,
                MidFill = 0,
                FinalFill = finalFill,
                MidBlockHits = 0,
                FinalBlockHits = finalHits,
                Score = score,
                Details = $"preserved={preserved}, split={splitDetails}, clamp={clampRemoved}, block={blockClean}/{blockChanged}:{blockDetails}, geometry={geometryClean}/{geometryChanged}:{geometryDetails}, raw={rawAuthoredDetails}, fast={fastDetails}, finalRefill={finalRefillOk}:{finalRefillDetails}"
            };

            return !string.IsNullOrWhiteSpace(finalPath);
        }

        private static Texture2D CreateOrUpdateLargeBadgeMask(string assetPath, int width, int height, out int maskArea, out string details)
        {
            maskArea = 0;
            details = string.Empty;
            if (string.IsNullOrWhiteSpace(assetPath) || width <= 0 || height <= 0)
            {
                details = "invalid mask input";
                return null;
            }

            EnsureFolderExists(Path.GetDirectoryName(assetPath)?.Replace("\\", "/"));

            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            var clear = new Color32(0, 0, 0, 0);
            var fill = new Color32(255, 255, 255, 255);
            float centerX = (width - 1) * 0.5f;
            float centerY = (height - 1) * 0.5f;
            float topHalf = width * 0.25f;
            float bodyHalf = width * 0.44f;
            float bottomHalf = width * 0.18f;

            for (int y = 0; y < height; y++)
            {
                float ny = (y + 0.5f) / height;
                float halfWidth;
                if (ny < 0.18f)
                {
                    halfWidth = Mathf.Lerp(topHalf, bodyHalf, ny / 0.18f);
                }
                else if (ny < 0.70f)
                {
                    halfWidth = bodyHalf;
                }
                else
                {
                    halfWidth = Mathf.Lerp(bodyHalf, bottomHalf, Mathf.Clamp01((ny - 0.70f) / 0.26f));
                }

                float shoulder = Mathf.Abs(y - centerY) / centerY;
                halfWidth -= Mathf.Max(0f, shoulder - 0.72f) * 7f;

                for (int x = 0; x < width; x++)
                {
                    float dx = Mathf.Abs((x + 0.5f) - centerX);
                    bool inside = dx <= halfWidth && y >= 3 && y <= height - 4;
                    if (inside)
                    {
                        texture.SetPixel(x, y, fill);
                        maskArea++;
                    }
                    else
                    {
                        texture.SetPixel(x, y, clear);
                    }
                }
            }

            texture.Apply(false, false);

            string fullPath = Path.GetFullPath(Path.Combine(Application.dataPath, "..", assetPath));
            string dir = Path.GetDirectoryName(fullPath);
            if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllBytes(fullPath, texture.EncodeToPNG());
            DestroyImmediate(texture);
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);

            var importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (importer != null)
            {
                importer.textureType = TextureImporterType.Default;
                importer.alphaSource = TextureImporterAlphaSource.FromInput;
                importer.alphaIsTransparency = true;
                importer.isReadable = true;
                importer.mipmapEnabled = false;
                importer.textureCompression = TextureImporterCompression.Uncompressed;
                importer.npotScale = TextureImporterNPOTScale.None;
                importer.SaveAndReimport();
            }

            details = $"created {width}x{height}, broad badge silhouette";
            return AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
        }

        private static void EnsureTextureReadable(string assetPath)
        {
            if (string.IsNullOrWhiteSpace(assetPath))
                return;

            var importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (importer == null)
                return;

            bool dirty = false;
            if (!importer.isReadable)
            {
                importer.isReadable = true;
                dirty = true;
            }

            if (importer.alphaSource != TextureImporterAlphaSource.FromInput)
            {
                importer.alphaSource = TextureImporterAlphaSource.FromInput;
                dirty = true;
            }

            if (!importer.alphaIsTransparency)
            {
                importer.alphaIsTransparency = true;
                dirty = true;
            }

            if (importer.mipmapEnabled)
            {
                importer.mipmapEnabled = false;
                dirty = true;
            }

            if (dirty)
                importer.SaveAndReimport();
        }

        private static List<int> CollectInteriorTransparentBlockIndices(bool[] canSpawn, int width, int height)
        {
            var result = new List<int>();
            if (canSpawn == null || canSpawn.Length != width * height)
                return result;

            var outside = new bool[canSpawn.Length];
            var queue = new Queue<int>();

            void EnqueueOutside(int x, int y)
            {
                if (x < 0 || y < 0 || x >= width || y >= height)
                    return;

                int idx = x + y * width;
                if (canSpawn[idx] || outside[idx])
                    return;

                outside[idx] = true;
                queue.Enqueue(idx);
            }

            for (int x = 0; x < width; x++)
            {
                EnqueueOutside(x, 0);
                EnqueueOutside(x, height - 1);
            }

            for (int y = 0; y < height; y++)
            {
                EnqueueOutside(0, y);
                EnqueueOutside(width - 1, y);
            }

            while (queue.Count > 0)
            {
                int idx = queue.Dequeue();
                int x = idx % width;
                int y = idx / width;
                EnqueueOutside(x + 1, y);
                EnqueueOutside(x - 1, y);
                EnqueueOutside(x, y + 1);
                EnqueueOutside(x, y - 1);
            }

            for (int i = 0; i < canSpawn.Length; i++)
            {
                if (!canSpawn[i] && !outside[i])
                    result.Add(i);
            }

            return result;
        }

        private static void ApplyBlockIndicesToBoard(BoardState board, List<int> blockIndices)
        {
            if (board == null || board.tiles == null || blockIndices == null)
                return;

            for (int i = 0; i < blockIndices.Count; i++)
            {
                int idx = blockIndices[i];
                if (idx < 0 || idx >= board.tiles.Length)
                    continue;

                board.tiles[idx] = TileState.Block();
            }
        }

        private static bool ApplyBlockRayCleanup(
            ref BoardState board,
            bool[] maskCanSpawn,
            int maxPasses,
            int trimPerChainLimit,
            out int changedChains,
            out string details)
        {
            changedChains = 0;
            details = "block-ray: already clean";

            if (board == null || board.tiles == null)
            {
                details = "block-ray: invalid board";
                return false;
            }

            if (maskCanSpawn != null && maskCanSpawn.Length != board.width * board.height)
                maskCanSpawn = null;

            int totalReversed = 0;
            int totalTrimmed = 0;
            int totalClearedChains = 0;
            int totalClearedCells = 0;
            int lastHits = 0;
            int passes = 0;
            int trimLimit = Mathf.Max(1, trimPerChainLimit);

            var visited = new HashSet<int>(Mathf.Max(1, board.width * board.height));
            var chainSet = new HashSet<int>(Mathf.Max(1, board.width * board.height));
            var orderedTmp = new List<Vector2Int>(16);
            var chains = new List<int[]>();
            var chainFlags = new List<bool>();
            var orderedIndices = new List<int>(16);
            var reversed = new List<int>(16);

            for (passes = 0; passes < maxPasses; passes++)
            {
                lastHits = CountBlockRayBlockedHeads(board, maskCanSpawn);
                if (lastHits <= 0)
                {
                    details = $"block-ray: passes={passes}, changed={changedChains}, reversed={totalReversed}, trimmed={totalTrimmed}, clearedChains={totalClearedChains}, clearedCells={totalClearedCells}, finalHits=0";
                    return true;
                }

                if (!CollectMaskedChains(board, chains, chainFlags, visited, chainSet, orderedTmp) || chains.Count == 0)
                    break;

                BoardState bestBoard = null;
                int bestHits = lastHits;
                int bestRemoved = int.MaxValue;
                int bestFill = -1;
                int bestAction = 0; // 1=reverse, 2=trim, 3=clear

                for (int ci = 0; ci < chains.Count; ci++)
                {
                    int[] chain = chains[ci];
                    if (chain == null || chain.Length < 2)
                        continue;

                    chainSet.Clear();
                    for (int i = 0; i < chain.Length; i++)
                    {
                        int idx = chain[i];
                        if (idx >= 0 && idx < board.tiles.Length)
                            chainSet.Add(idx);
                    }

                    if (!TryBuildStableOrderedChain(
                        board,
                        chainSet,
                        new Vector2Int(chain[0] % board.width, chain[0] / board.width),
                        out var ordered))
                    {
                        continue;
                    }

                    orderedIndices.Clear();
                    for (int i = 0; i < ordered.Count; i++)
                    {
                        Vector2Int p = ordered[i];
                        if (board.InBounds(p.x, p.y))
                            orderedIndices.Add(board.Index(p.x, p.y));
                    }

                    if (orderedIndices.Count < 2 || !HeadRayHitsBlock(board, orderedIndices[0]))
                        continue;

                    reversed.Clear();
                    for (int i = orderedIndices.Count - 1; i >= 0; i--)
                        reversed.Add(orderedIndices[i]);

                    var reverseBoard = CloneBoard(board);
                    if (reverseBoard != null
                        && TryApplyOrderedChainToCandidate(reverseBoard, reversed))
                    {
                        TryAcceptBlockRayCleanupCandidate(
                            reverseBoard,
                            maskCanSpawn,
                            0,
                            1,
                            ref bestBoard,
                            ref bestHits,
                            ref bestRemoved,
                            ref bestFill,
                            ref bestAction);
                    }

                    bool isLoop = ci < chainFlags.Count && chainFlags[ci];
                    if (!isLoop)
                    {
                        int[] orderedChain = orderedIndices.ToArray();
                        int maxTrim = Mathf.Min(trimLimit, orderedChain.Length - 2);
                        for (int trim = 1; trim <= maxTrim; trim++)
                        {
                            if (TryBuildGreedyRepairCandidate(
                                board,
                                orderedChain,
                                false,
                                trim,
                                0,
                                out BoardState trimBoard,
                                out int trimRemoved) && trimBoard != null)
                            {
                                TryAcceptBlockRayCleanupCandidate(
                                    trimBoard,
                                    maskCanSpawn,
                                    trimRemoved,
                                    2,
                                    ref bestBoard,
                                    ref bestHits,
                                    ref bestRemoved,
                                    ref bestFill,
                                    ref bestAction);
                            }
                        }
                    }

                    var clearBoard = CloneBoard(board);
                    if (clearBoard != null)
                    {
                        ClearBoardCellsFromIndices(clearBoard, orderedIndices);
                        TryAcceptBlockRayCleanupCandidate(
                            clearBoard,
                            maskCanSpawn,
                            orderedIndices.Count,
                            3,
                            ref bestBoard,
                            ref bestHits,
                            ref bestRemoved,
                            ref bestFill,
                            ref bestAction);
                    }
                }

                if (bestBoard == null || bestHits >= lastHits)
                    break;

                board = bestBoard;
                changedChains++;
                if (bestAction == 1)
                    totalReversed++;
                else if (bestAction == 2)
                    totalTrimmed += Math.Max(0, bestRemoved);
                else if (bestAction == 3)
                {
                    totalClearedChains++;
                    totalClearedCells += Math.Max(0, bestRemoved);
                }
            }

            int finalHits = CountBlockRayBlockedHeads(board, maskCanSpawn);
            details = $"block-ray: passes={passes}, changed={changedChains}, reversed={totalReversed}, trimmed={totalTrimmed}, clearedChains={totalClearedChains}, clearedCells={totalClearedCells}, finalHits={finalHits}, lastHits={lastHits}";
            return finalHits <= 0;
        }

        private static void TryAcceptBlockRayCleanupCandidate(
            BoardState candidate,
            bool[] maskCanSpawn,
            int removed,
            int action,
            ref BoardState bestBoard,
            ref int bestHits,
            ref int bestRemoved,
            ref int bestFill,
            ref int bestAction)
        {
            if (candidate == null || candidate.tiles == null)
                return;

            int hits = CountBlockRayBlockedHeads(candidate, maskCanSpawn);
            int fill = CountArrowTiles(candidate);
            bool better = hits < bestHits
                || (hits == bestHits && removed < bestRemoved)
                || (hits == bestHits && removed == bestRemoved && fill > bestFill);

            if (!better)
                return;

            bestBoard = candidate;
            bestHits = hits;
            bestRemoved = removed;
            bestFill = fill;
            bestAction = action;
        }

        private static int CountBlockRayBlockedHeads(BoardState board, bool[] maskCanSpawn)
        {
            if (board == null || board.tiles == null)
                return 0;

            if (maskCanSpawn != null && maskCanSpawn.Length != board.width * board.height)
                maskCanSpawn = null;

            var visited = new HashSet<int>(Mathf.Max(1, board.width * board.height));
            var chainSet = new HashSet<int>(Mathf.Max(1, board.width * board.height));
            var orderedTmp = new List<Vector2Int>(16);
            var chains = new List<int[]>();
            var chainFlags = new List<bool>();
            if (!CollectMaskedChains(board, chains, chainFlags, visited, chainSet, orderedTmp) || chains.Count == 0)
                return 0;

            int hits = 0;
            for (int ci = 0; ci < chains.Count; ci++)
            {
                int[] chain = chains[ci];
                if (chain == null || chain.Length < 2)
                    continue;

                chainSet.Clear();
                for (int i = 0; i < chain.Length; i++)
                {
                    int idx = chain[i];
                    if (idx >= 0 && idx < board.tiles.Length)
                        chainSet.Add(idx);
                }

                if (!TryBuildStableOrderedChain(
                    board,
                    chainSet,
                    new Vector2Int(chain[0] % board.width, chain[0] / board.width),
                    out var ordered))
                {
                    continue;
                }

                if (ordered == null || ordered.Count < 2)
                    continue;

                int headIdx = board.Index(ordered[0].x, ordered[0].y);
                if (maskCanSpawn != null && (headIdx < 0 || headIdx >= maskCanSpawn.Length || !maskCanSpawn[headIdx]))
                    continue;

                if (HeadRayHitsBlock(board, headIdx))
                    hits++;
            }

            return hits;
        }

        private static bool HeadRayHitsBlock(BoardState board, int headIdx)
        {
            if (board == null || board.tiles == null || headIdx < 0 || headIdx >= board.tiles.Length)
                return false;

            TileState head = board.tiles[headIdx];
            if (head.type != TileType.Arrow)
                return false;

            Vector2Int step = DirToOffsetSafe(head.arrow.outDir);
            if (step.x == 0 && step.y == 0)
                return false;

            int x = (headIdx % board.width) + step.x;
            int y = (headIdx / board.width) + step.y;
            while (board.InBounds(x, y))
            {
                int idx = board.Index(x, y);
                TileState t = board.tiles[idx];
                if (t.type == TileType.Empty)
                {
                    x += step.x;
                    y += step.y;
                    continue;
                }

                return t.type == TileType.Block;
            }

            return false;
        }

        private static string CreateBlockHoleVariant(
            string sourceAssetPath,
            Texture2D mask,
            List<int> blockIndices,
            string suffix,
            out string details)
        {
            details = string.Empty;
            if (string.IsNullOrWhiteSpace(sourceAssetPath))
            {
                details = "source empty";
                return string.Empty;
            }

            var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourceAssetPath);
            if (source == null || source.authoredLevel == null)
            {
                details = "source missing or not authored";
                return string.Empty;
            }

            string folder = Path.GetDirectoryName(sourceAssetPath)?.Replace("\\", "/");
            string name = Path.GetFileNameWithoutExtension(sourceAssetPath);
            string outputPath = AssetDatabase.GenerateUniqueAssetPath($"{folder}/{name}_{suffix}.asset");

            var copy = ScriptableObject.CreateInstance<LevelDefinition>();
            copy.levelId = $"{source.levelId}_{suffix}";
            copy.source = LevelDefinition.LevelSource.Authored;
            copy.board.width = source.board.width;
            copy.board.height = source.board.height;
            copy.board.seed = source.board.seed;
            copy.generation.arrowCoverage = source.generation.arrowCoverage;
            copy.generation.initialMovableArrowCount = source.generation.initialMovableArrowCount;
            copy.generation.targetDifficultyScore = source.generation.targetDifficultyScore;
            copy.generation.fixedGenerationSeed = source.generation.fixedGenerationSeed;
            copy.generation.minPathLen = source.generation.minPathLen;
            copy.generation.maxPathLength = source.generation.maxPathLength;
            copy.generation.twistiness = source.generation.twistiness;
            copy.generation.validateWithGreedy = source.generation.validateWithGreedy;
            copy.lose.blockedLoseLimit = source.lose.blockedLoseLimit;
            copy.masking.spawnMask = mask != null ? mask : source.masking.spawnMask;
            copy.masking.alphaThreshold = source.masking.alphaThreshold;
            copy.masking.useAlphaOnly = source.masking.useAlphaOnly;
            copy.masking.luminanceThreshold = source.masking.luminanceThreshold;
            copy.masking.useMaskToDefineBoardSize = source.masking.useMaskToDefineBoardSize;
            copy.arrowColorMode = source.arrowColorMode;
            copy.arrowColorMaskQuantizeSteps = source.arrowColorMaskQuantizeSteps;
            copy.tintOnHit = source.tintOnHit;
            copy.hitTint = source.hitTint;
            copy.introSettings = source.introSettings;
            copy.palette = source.palette;

            copy.authoredLevel = CloneAuthoredWithBlocks(source.authoredLevel, blockIndices);
            if (!AuthoredLevelBuilder.TryBuildBoard(copy.authoredLevel, out BoardState board, out string buildError))
            {
                details = $"block authored build failed: {buildError}";
                DestroyImmediate(copy);
                return string.Empty;
            }

            int greedyMoves = Math.Max(512, copy.authoredLevel.width * copy.authoredLevel.height * 16);
            bool greedyOk = GreedyValidator.TryClearAllByGreedy(
                board,
                new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty }),
                greedyMoves,
                out _);

            if (!greedyOk)
            {
                details = $"block greedy failed blocks={copy.authoredLevel.blockIndices.Count}, arrows={GetAuthoredArrowCount(copy.authoredLevel)}";
                DestroyImmediate(copy);
                return string.Empty;
            }

            AssetDatabase.CreateAsset(copy, outputPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(outputPath);

            details = $"block greedy pass blocks={copy.authoredLevel.blockIndices.Count}, arrows={GetAuthoredArrowCount(copy.authoredLevel)}, chains={CountAuthoredChains(copy.authoredLevel)}";
            return outputPath;
        }

        private static AuthoredLevelData CloneAuthoredWithBlocks(AuthoredLevelData source, List<int> blockIndices)
        {
            var clone = new AuthoredLevelData
            {
                width = source != null ? source.width : 1,
                height = source != null ? source.height : 1,
                arrows = new List<AuthoredArrowData>(),
                blockIndices = blockIndices != null ? new List<int>(blockIndices) : new List<int>()
            };

            if (source?.arrows == null)
                return clone;

            var blockSet = new HashSet<int>(clone.blockIndices);
            for (int i = 0; i < source.arrows.Count; i++)
            {
                var arrow = source.arrows[i];
                if (arrow?.indices == null || arrow.indices.Count < 2)
                    continue;

                bool overlapsBlock = false;
                for (int j = 0; j < arrow.indices.Count; j++)
                {
                    if (blockSet.Contains(arrow.indices[j]))
                    {
                        overlapsBlock = true;
                        break;
                    }
                }

                if (overlapsBlock)
                    continue;

                clone.arrows.Add(new AuthoredArrowData
                {
                    indices = new List<int>(arrow.indices),
                    colorIndex = arrow.colorIndex
                });
            }

            return clone;
        }

        private static LevelDefinition BuildLargeCompositeSeedAsset(
            string assetPath,
            int width,
            int height,
            Texture2D mask,
            CompositeSeedPart[] parts,
            out string details)
        {
            details = string.Empty;
            if (string.IsNullOrWhiteSpace(assetPath) || width <= 0 || height <= 0 || parts == null || parts.Length == 0)
            {
                details = "invalid composite input";
                return null;
            }

            EnsureFolderExists(Path.GetDirectoryName(assetPath)?.Replace("\\", "/"));

            var authored = new AuthoredLevelData
            {
                width = width,
                height = height,
                arrows = new List<AuthoredArrowData>()
            };

            var occupied = new HashSet<int>();
            var report = new List<string>();
            LevelDefinition template = null;
            int skippedChains = 0;
            int sourceChains = 0;

            for (int p = 0; p < parts.Length; p++)
            {
                CompositeSeedPart part = parts[p];
                if (part == null || string.IsNullOrWhiteSpace(part.SeedPath))
                    continue;

                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(part.SeedPath);
                if (source == null || source.authoredLevel == null || source.authoredLevel.arrows == null)
                {
                    report.Add($"part{p}=missing:{part?.SeedPath}");
                    continue;
                }

                if (template == null)
                    template = source;

                int added = 0;
                int skipped = 0;
                int sourceW = Mathf.Max(1, source.authoredLevel.width);
                int sourceH = Mathf.Max(1, source.authoredLevel.height);
                for (int i = 0; i < source.authoredLevel.arrows.Count; i++)
                {
                    var arrow = source.authoredLevel.arrows[i];
                    sourceChains++;
                    if (arrow == null || arrow.indices == null || arrow.indices.Count < 2)
                    {
                        skipped++;
                        continue;
                    }

                    var translated = new List<int>(arrow.indices.Count);
                    bool valid = true;
                    for (int j = 0; j < arrow.indices.Count; j++)
                    {
                        int idx = arrow.indices[j];
                        int sx = idx % sourceW;
                        int sy = idx / sourceW;
                        if (sy < 0 || sy >= sourceH)
                        {
                            valid = false;
                            break;
                        }

                        int tx = sx + part.Offset.x;
                        int ty = sy + part.Offset.y;
                        if (tx < 0 || ty < 0 || tx >= width || ty >= height)
                        {
                            valid = false;
                            break;
                        }

                        int targetIdx = tx + ty * width;
                        if (occupied.Contains(targetIdx))
                        {
                            valid = false;
                            break;
                        }

                        translated.Add(targetIdx);
                    }

                    if (!valid)
                    {
                        skipped++;
                        continue;
                    }

                    for (int j = 0; j < translated.Count; j++)
                        occupied.Add(translated[j]);

                    authored.arrows.Add(new AuthoredArrowData
                    {
                        indices = translated,
                        colorIndex = authored.arrows.Count % 16
                    });
                    added++;
                }

                skippedChains += skipped;
                report.Add($"part{p + 1}={Path.GetFileNameWithoutExtension(part.SeedPath)} offset={part.Offset.x},{part.Offset.y} added={added}, skipped={skipped}");
            }

            if (template == null || authored.arrows.Count == 0)
            {
                details = $"no valid composite chains | {string.Join(" | ", report)}";
                return null;
            }

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out _, out string buildError))
            {
                details = $"composite build failed: {buildError} | {string.Join(" | ", report)}";
                return null;
            }

            var output = AssetDatabase.LoadAssetAtPath<LevelDefinition>(assetPath);
            bool isNew = output == null;
            if (output == null)
                output = ScriptableObject.CreateInstance<LevelDefinition>();

            output.levelId = "seed_mask_largecomposite_source_bigbadge";
            output.source = LevelDefinition.LevelSource.Authored;
            output.board.width = width;
            output.board.height = height;
            output.board.seed = 0;
            output.generation.arrowCoverage = 1f;
            output.generation.initialMovableArrowCount = 0;
            output.generation.targetDifficultyScore = template.generation.targetDifficultyScore;
            output.generation.fixedGenerationSeed = 0;
            output.generation.minPathLen = Mathf.Max(2, template.generation.minPathLen);
            output.generation.maxPathLength = width * height;
            output.generation.twistiness = template.generation.twistiness;
            output.generation.validateWithGreedy = true;
            output.lose.blockedLoseLimit = template.lose.blockedLoseLimit;
            output.masking.spawnMask = mask;
            output.masking.useMaskToDefineBoardSize = true;
            output.arrowColorMode = template.arrowColorMode;
            output.arrowColorMaskQuantizeSteps = template.arrowColorMaskQuantizeSteps;
            output.tintOnHit = template.tintOnHit;
            output.hitTint = template.hitTint;
            output.introSettings = template.introSettings;
            output.palette = template.palette;
            output.authoredLevel = authored;

            if (isNew)
            {
                AssetDatabase.CreateAsset(output, assetPath);
            }
            else
            {
                EditorUtility.SetDirty(output);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
            details = $"sourceChains={sourceChains}, compositeChains={authored.arrows.Count}, skipped={skippedChains}, occupied={occupied.Count}, parts=[{string.Join("; ", report)}]";
            return AssetDatabase.LoadAssetAtPath<LevelDefinition>(assetPath);
        }

        private static int CountAuthoredChains(AuthoredLevelData authored)
        {
            return authored?.arrows?.Count ?? 0;
        }

        private static StraightnessStats ComputeStraightnessStats(AuthoredLevelData authored)
        {
            var stats = new StraightnessStats();
            if (authored == null || authored.arrows == null || authored.width <= 0)
                return stats;

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var indices = authored.arrows[i]?.indices;
                if (indices == null || indices.Count == 0)
                    continue;

                int len = indices.Count;
                stats.Chains++;
                stats.ArrowTiles += len;
                stats.LongestChain = Mathf.Max(stats.LongestChain, len);

                int longestRun = LongestStraightRun(indices, authored.width);
                stats.LongestRun = Mathf.Max(stats.LongestRun, longestRun);

                bool straight = len >= 3 && longestRun >= len;
                bool longStraight = len >= 6 && longestRun >= Mathf.Max(6, Mathf.CeilToInt(len * 0.85f));
                if (straight)
                    stats.StraightChains++;
                else
                    stats.BentChains++;

                if (longStraight)
                {
                    stats.LongStraightChains++;
                    stats.LongStraightTiles += len;
                }
            }

            if (stats.Chains > 0)
            {
                stats.StraightChainRatio = stats.StraightChains / (float)stats.Chains;
                stats.LongStraightChainRatio = stats.LongStraightChains / (float)stats.Chains;
                stats.BentChainRatio = stats.BentChains / (float)stats.Chains;
            }

            if (stats.ArrowTiles > 0)
                stats.LongStraightTileRatio = stats.LongStraightTiles / (float)stats.ArrowTiles;

            return stats;
        }

        private static bool IsStraightDominatedShape(AuthoredLevelData authored, out string details)
        {
            var stats = ComputeStraightnessStats(authored);
            details = BuildStraightnessDetails(stats);
            if (stats.Chains < 8)
                return false;

            bool mostlyStraightChains = stats.StraightChainRatio >= 0.62f && stats.BentChainRatio <= 0.38f;
            bool longStraightDominant = stats.LongStraightChainRatio >= 0.34f || stats.LongStraightTileRatio >= 0.42f;
            bool veryLongRun = stats.LongestRun >= 10 && stats.LongStraightChains >= 3;
            return mostlyStraightChains && (longStraightDominant || veryLongRun);
        }

        private static string BuildStraightnessDetails(StraightnessStats stats)
        {
            if (stats == null)
                return "straightness=invalid";

            return $"straightness=chains={stats.Chains}, tiles={stats.ArrowTiles}, straight={stats.StraightChains}({stats.StraightChainRatio:0.00}), longStraight={stats.LongStraightChains}({stats.LongStraightChainRatio:0.00}), longTiles={stats.LongStraightTileRatio:0.00}, bent={stats.BentChains}({stats.BentChainRatio:0.00}), longestRun={stats.LongestRun}, longestChain={stats.LongestChain}";
        }

        private static int LongestStraightRun(List<int> indices, int width)
        {
            if (indices == null || indices.Count < 2 || width <= 0)
                return indices?.Count ?? 0;

            int longest = 2;
            int current = 2;
            int prevDx = indices[1] % width - indices[0] % width;
            int prevDy = indices[1] / width - indices[0] / width;

            for (int i = 2; i < indices.Count; i++)
            {
                int dx = indices[i] % width - indices[i - 1] % width;
                int dy = indices[i] / width - indices[i - 1] / width;
                if (dx == prevDx && dy == prevDy)
                {
                    current++;
                }
                else
                {
                    longest = Mathf.Max(longest, current);
                    current = 2;
                    prevDx = dx;
                    prevDy = dy;
                }
            }

            return Mathf.Max(longest, current);
        }

        private static bool TryFastDependencyTrimToGreedy(
            ref BoardState board,
            bool[] maskCanSpawn,
            int greedyMoves,
            int minChains,
            int maxPasses,
            out string details)
        {
            details = string.Empty;
            if (board == null || board.tiles == null)
            {
                details = "invalid board";
                return false;
            }

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int removedChains = 0;
            int removedCells = 0;
            var passReports = new List<string>();

            for (int pass = 0; pass <= maxPasses; pass++)
            {
                int chainCount = CountBoardChains(board);
                int fill = CountArrowTiles(board);
                if (GreedyValidator.TryClearAllByGreedy(board, rules, greedyMoves, out _))
                {
                    details = $"passAt={pass}, chains={chainCount}, fill={fill}, removedChains={removedChains}, removedCells={removedCells}"
                        + (passReports.Count > 0 ? $" | steps=[{string.Join("; ", passReports)}]" : string.Empty);
                    return true;
                }

                if (pass >= maxPasses)
                    break;

                if (chainCount <= minChains)
                {
                    details = $"below minChains before greedy pass: chains={chainCount}, fill={fill}, removedChains={removedChains}, removedCells={removedCells}"
                        + (passReports.Count > 0 ? $" | steps=[{string.Join("; ", passReports)}]" : string.Empty);
                    return false;
                }

                if (!TryCollectGreedyStuckChains(board, greedyMoves, out var stuckChains, out var cycleHeads, out var cycleGroups, out string stuckDetails)
                    || stuckChains == null
                    || stuckChains.Count == 0)
                {
                    details = $"no stuck chains but greedy failed: chains={chainCount}, fill={fill} | {stuckDetails}";
                    return false;
                }

                int removeIndex = -1;
                string mode = "stuck";
                if (cycleGroups != null && cycleGroups.Count > 0)
                {
                    int[] group = null;
                    for (int i = 0; i < cycleGroups.Count; i++)
                    {
                        if (cycleGroups[i] != null && cycleGroups[i].Length > 0)
                        {
                            group = cycleGroups[i];
                            break;
                        }
                    }

                    if (group != null)
                    {
                        mode = $"cycle({string.Join(",", group)})";
                        int bestScore = int.MaxValue;
                        for (int i = 0; i < group.Length; i++)
                        {
                            int candidate = group[i];
                            if (candidate < 0 || candidate >= stuckChains.Count)
                                continue;

                            int score = ScoreCoreTakeoutChain(board, stuckChains[candidate], maskCanSpawn);
                            if (score < bestScore)
                            {
                                bestScore = score;
                                removeIndex = candidate;
                            }
                        }
                    }
                }

                if (removeIndex < 0)
                {
                    int bestScore = int.MaxValue;
                    for (int i = 0; i < stuckChains.Count; i++)
                    {
                        int score = ScoreCoreTakeoutChain(board, stuckChains[i], maskCanSpawn);
                        if (score < bestScore)
                        {
                            bestScore = score;
                            removeIndex = i;
                        }
                    }
                }

                if (removeIndex < 0 || removeIndex >= stuckChains.Count || stuckChains[removeIndex] == null || stuckChains[removeIndex].Length == 0)
                {
                    details = $"no removable stuck chain: chains={chainCount}, fill={fill} | {stuckDetails}";
                    return false;
                }

                int before = CountArrowTiles(board);
                ClearBoardCellsFromIndices(board, stuckChains[removeIndex]);
                int after = CountArrowTiles(board);
                int removed = Math.Max(0, before - after);
                removedChains++;
                removedCells += removed;

                if (passReports.Count < 24)
                {
                    string cycleText = cycleHeads != null && cycleHeads.Count > 0 ? string.Join(",", cycleHeads) : "-";
                    passReports.Add($"p{pass}:{mode}->idx{removeIndex}, cells={removed}, chains={chainCount}, cycles={cycleText}");
                }

            }

            details = $"failed maxPasses={maxPasses}, chains={CountBoardChains(board)}, fill={CountArrowTiles(board)}, removedChains={removedChains}, removedCells={removedCells}"
                + (passReports.Count > 0 ? $" | steps=[{string.Join("; ", passReports)}]" : string.Empty);
            return false;
        }

        private static int CountBoardChains(BoardState board)
        {
            if (board == null || board.tiles == null)
                return 0;

            var chains = new List<int[]>();
            var flags = new List<bool>();
            var visited = new HashSet<int>(Mathf.Max(1, board.tiles.Length));
            var chainSet = new HashSet<int>(Mathf.Max(1, board.tiles.Length));
            var ordered = new List<Vector2Int>();
            return CollectMaskedChains(board, chains, flags, visited, chainSet, ordered) ? chains.Count : 0;
        }

        private static int TrimBoardChainsToTarget(ref BoardState board, bool[] maskCanSpawn, int targetChains, out string details)
        {
            details = string.Empty;
            if (board == null || board.tiles == null || targetChains <= 0)
            {
                details = "invalid trim input";
                return 0;
            }

            var chains = new List<int[]>();
            var flags = new List<bool>();
            var visited = new HashSet<int>(Mathf.Max(1, board.tiles.Length));
            var chainSet = new HashSet<int>(Mathf.Max(1, board.tiles.Length));
            var ordered = new List<Vector2Int>();
            if (!CollectMaskedChains(board, chains, flags, visited, chainSet, ordered) || chains.Count <= targetChains)
            {
                details = $"no trim needed chains={chains.Count}, target={targetChains}";
                return 0;
            }

            var order = new List<int>(chains.Count);
            for (int i = 0; i < chains.Count; i++)
                order.Add(i);

            BoardState scoreBoard = board;
            order.Sort((a, b) =>
            {
                int scoreA = ScoreCoreTakeoutChain(scoreBoard, chains[a], maskCanSpawn);
                int scoreB = ScoreCoreTakeoutChain(scoreBoard, chains[b], maskCanSpawn);
                int cmp = scoreA.CompareTo(scoreB);
                if (cmp != 0)
                    return cmp;

                int lenA = chains[a]?.Length ?? 0;
                int lenB = chains[b]?.Length ?? 0;
                return lenA.CompareTo(lenB);
            });

            int removeNeed = chains.Count - targetChains;
            int removedChains = 0;
            int removedCells = 0;
            for (int i = 0; i < order.Count && removedChains < removeNeed; i++)
            {
                int idx = order[i];
                if (idx < 0 || idx >= chains.Count || chains[idx] == null || chains[idx].Length == 0)
                    continue;

                int before = CountArrowTiles(board);
                ClearBoardCellsFromIndices(board, chains[idx]);
                int removed = Math.Max(0, before - CountArrowTiles(board));
                if (removed <= 0)
                    continue;

                removedChains++;
                removedCells += removed;
            }

            details = $"chains={chains.Count}->{CountBoardChains(board)}, target={targetChains}, removedCells={removedCells}";
            return removedChains;
        }

        private static bool TryBuildBoundaryExitFallbackBoard(
            LevelDefinition seedPoolSource,
            bool[] maskCanSpawn,
            int width,
            int height,
            int targetChains,
            out BoardState board,
            out string details)
        {
            board = null;
            details = string.Empty;
            if (maskCanSpawn == null || maskCanSpawn.Length != width * height || targetChains <= 0)
            {
                details = "invalid fallback input";
                return false;
            }

            var lengthSamples = CollectChainLengthSamples(seedPoolSource, 2, 5);
            if (lengthSamples.Count == 0)
            {
                lengthSamples.Add(4);
                lengthSamples.Add(5);
                lengthSamples.Add(6);
            }

            var boundary = new List<BoundaryExitCell>();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int idx = x + y * width;
                    if (!maskCanSpawn[idx])
                        continue;

                    for (int d = 0; d < 4; d++)
                    {
                        Dir outDir = (Dir)d;
                        Vector2Int step = DirToOffsetSafe(outDir);
                        int nx = x + step.x;
                        int ny = y + step.y;
                        if (nx >= 0 && ny >= 0 && nx < width && ny < height && maskCanSpawn[nx + ny * width])
                            continue;

                        boundary.Add(new BoundaryExitCell
                        {
                            Index = idx,
                            OutDir = outDir,
                            SortKey = PositiveModulo(x * 73 + y * 151 + d * 997, 100000)
                        });
                        break;
                    }
                }
            }

            boundary.Sort((a, b) =>
            {
                int cmp = a.SortKey.CompareTo(b.SortKey);
                if (cmp != 0)
                    return cmp;
                return a.Index.CompareTo(b.Index);
            });

            var occupied = new HashSet<int>();
            var authored = new AuthoredLevelData
            {
                width = width,
                height = height,
                arrows = new List<AuthoredArrowData>(targetChains)
            };

            int rejected = 0;
            for (int i = 0; i < boundary.Count && authored.arrows.Count < targetChains; i++)
            {
                BoundaryExitCell cell = boundary[i];
                int sample = lengthSamples[(authored.arrows.Count * 7 + i * 3) % lengthSamples.Count];
                int desiredLen = Mathf.Clamp(sample, 2, 5);
                if (!TryBuildBoundaryExitPath(cell, maskCanSpawn, width, height, occupied, desiredLen, out var path))
                {
                    rejected++;
                    continue;
                }

                for (int p = 0; p < path.Count; p++)
                    occupied.Add(path[p]);

                authored.arrows.Add(new AuthoredArrowData
                {
                    indices = path,
                    colorIndex = authored.arrows.Count % 16
                });
            }

            if (authored.arrows.Count < Mathf.Min(targetChains, 60))
            {
                details = $"too few fallback chains={authored.arrows.Count}, rejected={rejected}, boundary={boundary.Count}";
                return false;
            }

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out board, out string buildError))
            {
                details = $"fallback authored build failed: {buildError}, chains={authored.arrows.Count}";
                return false;
            }

            details = $"chains={authored.arrows.Count}, fill={CountArrowTiles(board)}, target={targetChains}, boundary={boundary.Count}, rejected={rejected}, lengthSamples={lengthSamples.Count}";
            return true;
        }

        private static bool TryBuildBoundaryExitPath(
            BoundaryExitCell cell,
            bool[] maskCanSpawn,
            int width,
            int height,
            HashSet<int> occupied,
            int desiredLen,
            out List<int> path)
        {
            path = null;
            if (cell == null || maskCanSpawn == null || occupied == null)
                return false;

            int x = cell.Index % width;
            int y = cell.Index / width;
            Vector2Int inward = DirToOffsetSafe(Opposite(cell.OutDir));
            var candidate = new List<int>(desiredLen);
            for (int i = 0; i < desiredLen; i++)
            {
                int px = x + inward.x * i;
                int py = y + inward.y * i;
                if (px < 0 || py < 0 || px >= width || py >= height)
                    break;

                int idx = px + py * width;
                if (!maskCanSpawn[idx] || occupied.Contains(idx))
                    break;

                candidate.Add(idx);
            }

            if (candidate.Count < 2)
                return false;

            path = candidate;
            return true;
        }

        private static List<int> CollectChainLengthSamples(LevelDefinition seedPoolSource, int minLen, int maxLen)
        {
            var samples = new List<int>();
            var authored = seedPoolSource != null ? seedPoolSource.authoredLevel : null;
            if (authored?.arrows == null)
                return samples;

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                int len = authored.arrows[i]?.indices?.Count ?? 0;
                if (len >= minLen && len <= maxLen)
                    samples.Add(len);
            }

            return samples;
        }

        private static int PositiveModulo(int value, int modulo)
        {
            if (modulo <= 0)
                return 0;
            int result = value % modulo;
            return result < 0 ? result + modulo : result;
        }

        private static void WriteReport(string reportPath, List<string> report)
        {
            if (string.IsNullOrWhiteSpace(reportPath) || report == null)
                return;

            try
            {
                string fullPath = Path.GetFullPath(Path.Combine(Application.dataPath, "..", reportPath));
                string dir = Path.GetDirectoryName(fullPath);
                if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                File.WriteAllText(fullPath, string.Join("\n", report));
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[SeedMaskPatch] Failed to write report {reportPath}: {ex.Message}");
            }
        }

        private static void OpenForSeedMaskSelection(string reason)
        {
            var window = CreateInstance<SeedMaskPatchWindow>();
            ApplyMenuDefaultRuntimeSettings(window);
            ApplyRepairExperimentRuntimeSettings(window);
            window.Show();
            window.Focus();
            window.minSize = new Vector2(520f, 500f);
            Debug.LogWarning($"[SeedMaskPatch] 未检测到完整 seed+mask 选择，已打开工具窗口，请在窗口选择 ({reason}) 后重试。");
        }

        private static bool TryResolveSelectedSeedAndMask(out LevelDefinition seedDefinition, out Texture2D maskTexture)
        {
            seedDefinition = null;
            maskTexture = null;

            var selected = Selection.objects ?? Array.Empty<UnityEngine.Object>();
            for (int i = 0; i < selected.Length; i++)
            {
                if (selected[i] == null)
                    continue;

                if (selected[i] is LevelDefinition selectedSeed && seedDefinition == null)
                    seedDefinition = selectedSeed;
                else if (selected[i] is Texture2D selectedMask && maskTexture == null)
                    maskTexture = selectedMask;

                if (seedDefinition != null && maskTexture != null)
                    break;
            }

            if (seedDefinition == null)
            {
                var selectedGuids = Selection.assetGUIDs;
                for (int i = 0; i < selectedGuids.Length; i++)
                {
                    var path = AssetDatabase.GUIDToAssetPath(selectedGuids[i]);
                    if (string.IsNullOrWhiteSpace(path))
                        continue;

                    var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
                    if (asset == null)
                        continue;

                    if (seedDefinition == null && asset is LevelDefinition seedFromGuid)
                        seedDefinition = seedFromGuid;
                    else if (maskTexture == null && asset is Texture2D maskFromGuid)
                        maskTexture = maskFromGuid;
                }
            }

            if (seedDefinition == null)
                seedDefinition = Selection.activeObject as LevelDefinition;
            if (maskTexture == null)
                maskTexture = Selection.activeObject as Texture2D;

            if (seedDefinition == null && Selection.activeObject != null)
            {
                var selectedSeed = Selection.activeObject as LevelDefinition;
                if (selectedSeed != null)
                    seedDefinition = selectedSeed;
            }

            if (maskTexture == null && Selection.activeObject != null)
            {
                var activeAsMask = Selection.activeObject as Texture2D;
                if (activeAsMask != null)
                    maskTexture = activeAsMask;
            }

            if (seedDefinition == null || maskTexture == null)
            {
                Debug.LogWarning("[SeedMaskPatch] 选择不完整，请重新选中一个 seed asset 与一个 mask 贴图。");
                return false;
            }

            return true;
        }

        private static void ApplyMenuDefaultRuntimeSettings(SeedMaskPatchWindow window)
        {
            if (window == null)
                return;

            window._placementMode = PlacementMode.Center;
            window._autoIfCenterFails = true;
            window._generationAttempts = 320;
            window._maxGreedyMovesMultiplier = 12;
            window._fallbackCleanupLoops = 0;
            window._layerLikeMaskMode = true;
            window._maskAlphaOnly = true;
            window._maskHealFillRatio = kHealTargetFillRatio;
            window._adaptiveHealFillFallback = false;
            window._maskHealFillRatioFloor = 1f;
            window._maskHealFillFallbackSteps = 0;
            window._inflateMaskForSolvability = false;
            window._maskInflationPasses = 0;
            window._outputFolder = kDefaultOutputFolder;
            window._outputPrefix = "seed_mask";
            window._candidateTopCount = 3;
            window._candidatePreEvalLimit = kDefaultCandidatePreEvalLimit;
            window._candidatePreEvalTimeoutMs = kDefaultCandidatePreEvalTimeoutMs;
            window._candidateMinFillRatio = 0.99f;
            window._allowGreedyFullChainRemoval = true;
            window._allowGreedyFallbackChainClear = true;
            window._greedyTrimPerChain = kGreedyTrimPerChain;
            window._exportRawIfGreedyFails = true;
            window._preGreedyGeometryFixPasses = 8;
            window._greedyRepairCandidateEvalCap = 0;
            window._greedyRepairTimeBudgetMs = 0;
            window._allowGreedyCoreTakeoutRefillRescue = true;
            window._patchOutputMode = PatchOutputMode.GreedyRescue;
        }

        private static void ApplyRepairExperimentRuntimeSettings(SeedMaskPatchWindow window)
        {
            if (window == null)
                return;

            window._placementMode = PlacementMode.Center;
            window._autoIfCenterFails = true;
            window._generationAttempts = kRepairExperimentGenerationAttempts;
            window._maxGreedyMovesMultiplier = kRepairExperimentGreedyMultiplier;
            window._fallbackCleanupLoops = kRepairExperimentFallbackLoops;
            window._layerLikeMaskMode = true;
            window._maskAlphaOnly = true;
            window._maskHealFillRatio = kRepairExperimentFillRatio;
            window._adaptiveHealFillFallback = true;
            window._maskHealFillRatioFloor = kRepairExperimentFillRatioFloor;
            window._maskHealFillFallbackSteps = kRepairExperimentFallbackSteps;
            window._inflateMaskForSolvability = true;
            window._maskInflationPasses = kRepairExperimentInflationPasses;
            window._outputFolder = kDefaultOutputFolder;
            window._outputPrefix = kRepairExperimentOutputPrefix;
            window._candidateTopCount = 3;
            window._candidatePreEvalLimit = Mathf.Max(kDefaultCandidatePreEvalLimit, 240);
            window._candidatePreEvalTimeoutMs = kDefaultCandidatePreEvalTimeoutMs;
            window._candidateMinFillRatio = kRepairExperimentFillRatioFloor;
            window._allowGreedyFullChainRemoval = false;
            window._allowGreedyFallbackChainClear = false;
            window._greedyTrimPerChain = kRepairExperimentTrimPerChain;
            window._exportRawIfGreedyFails = true;
            window._preGreedyGeometryFixPasses = 10;
            window._greedyRepairCandidateEvalCap = 0;
            window._greedyRepairTimeBudgetMs = 0;
            window._allowGreedyCoreTakeoutRefillRescue = true;
            window._patchOutputMode = PatchOutputMode.GreedyRescue;
        }

        private void OnEnable()
        {
            var active = Selection.activeObject;
            if (_seedDefinition == null)
                _seedDefinition = active as LevelDefinition;
            if (_mask == null)
                _mask = active as Texture2D;
        }

        private void OnSelectionChange()
        {
            var active = Selection.activeObject;
            if (_seedDefinition == null)
                _seedDefinition = active as LevelDefinition;
            if (_mask == null)
                _mask = active as Texture2D;

            Repaint();
        }

        private void OnGUI()
        {
            try
            {
                EditorGUILayout.Space(4);
                EditorGUILayout.LabelField("Seed + Mask Patch (Authored Seed / Alpha Mask)", EditorStyles.boldLabel);
                EditorGUILayout.Space(8);

                _seedDefinition = (LevelDefinition)EditorGUILayout.ObjectField(
                    "Seed LevelDefinition",
                    _seedDefinition,
                    typeof(LevelDefinition),
                    false);

                _mask = (Texture2D)EditorGUILayout.ObjectField(
                    "Mask Texture",
                    _mask,
                    typeof(Texture2D),
                    false);

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Pick Seed From Selection", GUILayout.ExpandWidth(true)))
                {
                    var active = Selection.activeObject as LevelDefinition;
                    if (active != null)
                    {
                        _seedDefinition = active;
                    }
                    else
                    {
                        Debug.LogWarning("[SeedMaskPatch] 当前未选中 LevelDefinition。");
                    }
                }

                if (GUILayout.Button("Pick Mask From Selection", GUILayout.ExpandWidth(true)))
                {
                    var active = Selection.activeObject as Texture2D;
                    if (active != null)
                    {
                        _mask = active;
                    }
                    else
                    {
                        Debug.LogWarning("[SeedMaskPatch] 当前未选中 Texture2D。");
                    }
                }

                if (GUILayout.Button("Clear", GUILayout.ExpandWidth(true)))
                {
                    _seedDefinition = null;
                    _mask = null;
                }
                EditorGUILayout.EndHorizontal();

                _placementMode = (PlacementMode)EditorGUILayout.EnumPopup("Placement", _placementMode);
                _autoIfCenterFails = EditorGUILayout.Toggle("Auto fallback if Center fails", _autoIfCenterFails);
                _generationAttempts = Math.Max(1, EditorGUILayout.IntField("Generation Attempts", _generationAttempts));
                _maxGreedyMovesMultiplier = Math.Max(1, EditorGUILayout.IntField("Greedy Moves Multiplier", _maxGreedyMovesMultiplier));
                _fallbackCleanupLoops = Math.Max(0, EditorGUILayout.IntField("Clipped cleanup Loops", _fallbackCleanupLoops));
                _layerLikeMaskMode = EditorGUILayout.Toggle("Layer-like mask mode", _layerLikeMaskMode);
                _patchOutputMode = (PatchOutputMode)EditorGUILayout.EnumPopup("Output mode", _patchOutputMode);
                _maskAlphaOnly = EditorGUILayout.Toggle("Mask uses Alpha only", _maskAlphaOnly);
                _exportRawIfGreedyFails = EditorGUILayout.Toggle("Export raw clip on fail", _exportRawIfGreedyFails);
                _maskHealFillRatio = Mathf.Clamp01(EditorGUILayout.Slider("Mask heal fill ratio", _maskHealFillRatio, 0.5f, 1f));
                _adaptiveHealFillFallback = EditorGUILayout.Toggle("Adaptive fill fallback on fail", _adaptiveHealFillFallback);
                _maskHealFillRatioFloor = Mathf.Clamp01(EditorGUILayout.Slider("Fill ratio floor", _maskHealFillRatioFloor, 0.5f, 1f));
                if (_maskHealFillRatioFloor > _maskHealFillRatio)
                {
                    _maskHealFillRatioFloor = _maskHealFillRatio;
                }

                _maskHealFillFallbackSteps = Math.Max(0, EditorGUILayout.IntField("Adaptive fallback steps", _maskHealFillFallbackSteps));
                _inflateMaskForSolvability = EditorGUILayout.Toggle("Inflate sparse mask before build", _inflateMaskForSolvability);
                _maskInflationPasses = Math.Max(0, EditorGUILayout.IntField("Mask inflate passes", _maskInflationPasses));
                _outputFolder = EditorGUILayout.TextField("Output Folder", _outputFolder);
                _outputPrefix = EditorGUILayout.TextField("Output Prefix", _outputPrefix);

                EditorGUILayout.Space(4);
                using (new EditorGUI.DisabledScope(_seedDefinition == null || _mask == null))
                {
                    if (GUILayout.Button("Process Selected Seed (Raw + Rescue)"))
                    {
                        string latestSeedPath = ProcessSingle();
                        if (!string.IsNullOrWhiteSpace(latestSeedPath))
                        {
                            TrySyncDemoComparePack(_seedDefinition, latestSeedPath, _latestRawClipPath);
                        }
                    }

                    if (GUILayout.Button("Process Selected LevelDefinition Assets"))
                    {
                        var selected = Selection.GetFiltered<LevelDefinition>(SelectionMode.Assets);
                        if (selected == null || selected.Length == 0)
                        {
                            Debug.LogWarning("[SeedMaskPatch] No LevelDefinition selected.");
                            return;
                        }

                        LevelDefinition latestSourceSeed = null;
                        string latestGeneratedPath = null;
                        for (int i = 0; i < selected.Length; i++)
                        {
                            string generated = ProcessSingle(selected[i], _mask);
                            if (!string.IsNullOrWhiteSpace(generated))
                            {
                                latestGeneratedPath = generated;
                                latestSourceSeed = selected[i];
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(latestGeneratedPath))
                        {
                            TrySyncDemoComparePack(latestSourceSeed, latestGeneratedPath, _latestRawClipPath);
                        }
                    }
                }

                EditorGUILayout.Space(6);
                EditorGUILayout.LabelField("Seed Candidate Evaluation");
                _candidateTopCount = Math.Max(1, EditorGUILayout.IntField("Top Candidates", _candidateTopCount));
                _candidatePreEvalLimit = Math.Max(_candidateTopCount, EditorGUILayout.IntField("Pre-Eval Limit", _candidatePreEvalLimit));
                _candidatePreEvalTimeoutMs = Mathf.Max(500, EditorGUILayout.IntField("Pre-Eval Timeout (ms)", _candidatePreEvalTimeoutMs));
                _candidateMinFillRatio = Mathf.Clamp01(EditorGUILayout.Slider("Candidate min fill ratio", _candidateMinFillRatio, 0.5f, 1f));
                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Match Seeds", GUILayout.ExpandWidth(true)))
                    {
                        MatchCandidateSeeds();
                        if (_matchedSeeds.Count > 0)
                        {
                            RunCandidatePreEval();
                        }
                    }

                    if (GUILayout.Button("Run Candidate Pre-Eval (matched seeds)", GUILayout.ExpandWidth(true)))
                    {
                        RunCandidatePreEval();
                    }

                    if (GUILayout.Button("Rebuild Seed Cache", GUILayout.ExpandWidth(true)))
                    {
                        BuildSeedMatchCache(forceRebuild: true);
                    }
                }

                if (!string.IsNullOrWhiteSpace(_candidateMatchSummary))
                {
                    EditorGUILayout.LabelField(_candidateMatchSummary);
                }

                if (!string.IsNullOrWhiteSpace(_seedMatchCacheSummary))
                {
                    EditorGUILayout.LabelField(_seedMatchCacheSummary);
                }

                if (!string.IsNullOrWhiteSpace(_candidateEvalSummary))
                {
                    EditorGUILayout.LabelField(_candidateEvalSummary);
                }

                if (_candidateResults.Count > 0)
                {
                    int previewCount = Math.Min(_candidateTopCount, _candidateResults.Count);
                    for (int i = 0; i < previewCount; i++)
                    {
                        var candidate = _candidateResults[i];
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            EditorGUILayout.LabelField(BuildCandidateResultLine(i + 1, candidate));
                            using (new EditorGUI.DisabledScope(candidate == null || candidate.SourceSeed == null))
                            {
                                if (GUILayout.Button("Generate", GUILayout.Width(90)))
                                {
                                    string generated = ProcessSingle(candidate.SourceSeed, _mask);
                                    if (!string.IsNullOrWhiteSpace(generated))
                                    {
                                        TrySyncDemoComparePack(candidate.SourceSeed, generated, _latestRawClipPath);
                                    }
                                }
                            }
                        }
                    }

                    if (GUILayout.Button($"Generate Top {previewCount} Candidates"))
                    {
                        GenerateTopCandidates(previewCount);
                    }
                }

                EditorGUILayout.Space(6);
                EditorGUILayout.HelpBox(
                    "Workflow: hard-overlay on the mask. Covered cells keep source content; non-covered cells are always empty. " +
                    "Layer-like mode: covered area keeps source content as-is and clipped chains are split+rewired when possible, otherwise removed. " +
                    "Non-generation area is always cleared to empty, never Block. " +
                    $"Mask read rule: spawn if alpha > 127{(_maskAlphaOnly ? " (alpha-only)" : " OR RGB luminance > 127")} . " +
                    $"Mask heal strategy: fill ratio {_maskHealFillRatio:0.00} " +
                    (_adaptiveHealFillFallback ? $"(with fallback floor {_maskHealFillRatioFloor:0.00}, steps {_maskHealFillFallbackSteps}) " : string.Empty) +
                    $"Mask inflation: {(_inflateMaskForSolvability ? $"enabled ({_maskInflationPasses})" : "disabled")}. " +
                    "Placement order: Center first. If Center fails, Auto candidate search is used automatically when enabled. " +
                    "Greedy pass is required.",
                    MessageType.Info);
            }
            catch (ExitGUIException)
            {
                return;
            }
            catch (Exception ex)
            {
                EditorGUILayout.HelpBox("SeedMaskPatch UI error. 请检查 Console，并点 Reset Seed/Mask Fields。", MessageType.Error);
                Debug.LogException(ex);
                if (GUILayout.Button("Reset Seed/Mask Fields"))
                {
                    _seedDefinition = null;
                    _mask = null;
                }
            }
        }

        private string ProcessSingle()
        {
            if (_seedDefinition == null || _mask == null)
                return null;

            return ProcessSingle(_seedDefinition, _mask);
        }

        private string ProcessSingle(LevelDefinition seedDef, Texture2D mask)
        {
            string outputPath = null;
            _latestRawClipPath = string.Empty;
            _latestGeneratedPath = string.Empty;

            try
            {
                if (seedDef == null || mask == null)
                    return null;

                if (!TryBuildBoardFromSeed(seedDef, out BoardState sourceBoard))
                {
                    Debug.LogError($"[SeedMaskPatch] Unable to build board from seed: {GetAssetPath(seedDef)}");
                    return null;
                }

                bool useInflation = _inflateMaskForSolvability;
                int inflationPasses = _maskInflationPasses;
                float healRatio = _maskHealFillRatio;
                bool adaptiveHeal = _adaptiveHealFillFallback;
                float healFloor = _maskHealFillRatioFloor;
                int healSteps = _maskHealFillFallbackSteps;

                if (!TryReadMask(
                    mask,
                    _maskAlphaOnly,
                    false,
                    0,
                    out var rawMaskCanSpawn))
                {
                    Debug.LogError($"[SeedMaskPatch] Cannot read raw mask texture: {GetAssetPath(mask)}");
                    return null;
                }

                if (!TryReadMask(
                    mask,
                    _maskAlphaOnly,
                    useInflation,
                    inflationPasses,
                    out var maskCanSpawn))
                {
                    Debug.LogError($"[SeedMaskPatch] Cannot read mask texture: {GetAssetPath(mask)}");
                    return null;
                }

                if (_patchOutputMode == PatchOutputMode.RawClip)
                {
                    if (!TryBuildRawClipSeed(
                        seedDef,
                        sourceBoard,
                        mask,
                        rawMaskCanSpawn,
                        _placementMode,
                        _autoIfCenterFails,
                        false,
                        default,
                        out LevelDefinition rawOnlySeed,
                        out string rawOnlyDetails))
                    {
                        Debug.LogError($"[SeedMaskPatch] Raw clip failed: {GetAssetPath(seedDef)}\n{rawOnlyDetails}");
                        return null;
                    }

                    EnsureOutputFolder();
                    outputPath = SaveGeneratedSeedAsset(seedDef, mask, rawOnlySeed, MakeRawClipOutputPrefix(_outputPrefix));
                    _latestRawClipPath = outputPath;
                    _latestGeneratedPath = outputPath;
                    Debug.Log($"[SeedMaskPatch] Raw clip done: {GetAssetPath(seedDef)} -> {outputPath}\n{rawOnlyDetails}");
                    return outputPath;
                }

                LevelDefinition outputSeed = BuildPatchedSeed(
                    seedDef,
                    sourceBoard,
                    mask,
                    maskCanSpawn,
                    _placementMode,
                    _autoIfCenterFails,
                    _generationAttempts,
                    _fallbackCleanupLoops,
                    _layerLikeMaskMode,
                    _maxGreedyMovesMultiplier,
                    healRatio,
                    adaptiveHeal,
                    healFloor,
                    healSteps,
                    _allowGreedyFullChainRemoval,
                    _allowGreedyFallbackChainClear,
                    _greedyTrimPerChain,
                    _candidateMinFillRatio,
                    _patchOutputMode,
                    out string details);

                if (outputSeed == null)
                {
                    if (_exportRawIfGreedyFails
                        && TryBuildRawClipSeed(
                            seedDef,
                            sourceBoard,
                            mask,
                            rawMaskCanSpawn,
                            _placementMode,
                            _autoIfCenterFails,
                            false,
                            default,
                            out LevelDefinition rawOnlySeed,
                            out string rawOnlyDetails))
                    {
                        EnsureOutputFolder();
                        _latestRawClipPath = SaveGeneratedSeedAsset(seedDef, mask, rawOnlySeed, MakeRawClipOutputPrefix(_outputPrefix));
                        Debug.Log($"[SeedMaskPatch] Raw clip exported for failed rescue: {_latestRawClipPath}\n{rawOnlyDetails}");
                    }

                    Debug.LogError($"[SeedMaskPatch] Process failed: {GetAssetPath(seedDef)}\n{details}");
                    return null;
                }

                EnsureOutputFolder();
                if (TryParseOffsetDetail(details, out Vector2Int finalOffset)
                    && TryBuildRawClipSeed(
                        seedDef,
                        sourceBoard,
                        mask,
                        rawMaskCanSpawn,
                        _placementMode,
                        _autoIfCenterFails,
                        true,
                        finalOffset,
                        out LevelDefinition rawClipSeed,
                        out string rawClipDetails))
                {
                    _latestRawClipPath = SaveGeneratedSeedAsset(seedDef, mask, rawClipSeed, MakeRawClipOutputPrefix(_outputPrefix));
                    Debug.Log($"[SeedMaskPatch] Raw clip saved: {_latestRawClipPath}\n{rawClipDetails}");
                }
                else
                {
                    Debug.LogWarning($"[SeedMaskPatch] Raw clip save skipped: {GetAssetPath(seedDef)}");
                }

                outputPath = SaveGeneratedSeedAsset(seedDef, mask, outputSeed, _outputPrefix);
                _latestGeneratedPath = outputPath;
                Debug.Log($"[SeedMaskPatch] Done: {GetAssetPath(seedDef)} -> {outputPath}\n{details}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[SeedMaskPatch] Processing exception: {GetAssetPath(seedDef)}\n{ex}");
                return null;
            }

            return outputPath;
        }

        private string SaveGeneratedSeedAsset(LevelDefinition seedDef, Texture2D mask, LevelDefinition outputSeed, string outputPrefix)
        {
            if (outputSeed == null)
                return null;

            string fileName = MakeOutputFileName(seedDef, mask, outputPrefix);
            string targetPath = Path.Combine(_outputFolder, fileName).Replace("\\", "/");
            targetPath = AssetDatabase.GenerateUniqueAssetPath(targetPath);

            AssetDatabase.CreateAsset(outputSeed, targetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return targetPath;
        }

        private string SaveGeneratedSeedAssetWithFileName(LevelDefinition outputSeed, string fileStem)
        {
            if (outputSeed == null)
                return null;

            string safeStem = SanitizeName(fileStem);
            if (string.IsNullOrWhiteSpace(safeStem))
                safeStem = "hole_prod_candidate";

            string fileName = safeStem.EndsWith(".asset", StringComparison.OrdinalIgnoreCase)
                ? safeStem
                : $"{safeStem}.asset";
            string targetPath = Path.Combine(_outputFolder, fileName).Replace("\\", "/");
            targetPath = AssetDatabase.GenerateUniqueAssetPath(targetPath);

            AssetDatabase.CreateAsset(outputSeed, targetPath);
            AssetDatabase.SaveAssets();
            return targetPath;
        }

        private void RunCandidatePreEval()
        {
            _candidateResults.Clear();
            _candidateEvalSummary = string.Empty;
            _candidateMatchSummary = string.Empty;

            if (_mask == null)
            {
                _candidateMatchSummary = "请先选择一个 Mask。";
                _candidateEvalSummary = "请先选择一个 Mask。";
                _matchedSeeds.Clear();
                _matchedMaskPath = string.Empty;
                return;
            }

            if (_matchedSeeds == null
                || _matchedSeeds.Count == 0
                || !string.Equals(_matchedMaskPath, GetAssetPath(_mask), StringComparison.Ordinal))
            {
                MatchCandidateSeeds();
            }

            if (_matchedSeeds == null || _matchedSeeds.Count == 0)
            {
                _candidateEvalSummary = "未匹配到可参与候选的 Seed。请先点击 Match Seeds。";
                return;
            }

            int topCount = Math.Max(1, _candidateTopCount);
            int timeoutMs = Mathf.Max(500, _candidatePreEvalTimeoutMs);
            int successCount = 0;
            int evalLimit = Math.Max(topCount, _candidatePreEvalLimit);
            int evalCount = Math.Min(evalLimit, _matchedSeeds.Count);
            int evaluated = 0;
            bool stoppedEarly = false;
            bool userCancelled = false;
            var sw = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                for (int i = 0; i < evalCount; i++)
                {
                    if (sw.ElapsedMilliseconds >= timeoutMs)
                    {
                        stoppedEarly = true;
                        break;
                    }

                    float progress = (float)i / (float)Mathf.Max(1, evalCount);
                    bool cancel = EditorUtility.DisplayCancelableProgressBar(
                        "Candidate Pre-Eval",
                        $"Evaluating seed {i + 1}/{evalCount} - {Mathf.RoundToInt(progress * 100f)}%",
                        progress);
                    if (cancel)
                    {
                        stoppedEarly = true;
                        userCancelled = true;
                        break;
                    }

                    if (_matchedSeeds[i] == null)
                        continue;

                    evaluated++;
                    SeedCandidateResult result = EvaluateSeedCandidate(_matchedSeeds[i], _mask, topCount);
                    if (result != null)
                    {
                        _candidateResults.Add(result);
                        if (result.IsFeasible)
                            successCount++;
                    }
                }
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }

            if (_candidateResults.Count == 0)
            {
                if (userCancelled)
                {
                    _candidateEvalSummary = $"已取消评估，已评估 {evaluated} 个。";
                    _candidateMatchSummary = _candidateEvalSummary;
                }
                else
                {
                    _candidateEvalSummary = "候选预估未产生有效记录。";
                    _candidateMatchSummary = _candidateEvalSummary;
                }
                return;
            }

            _candidateResults.Sort(CompareCandidateResults);
            float spentSec = sw.ElapsedMilliseconds / 1000f;
            _candidateEvalSummary = $"候选预估完成：匹配到 {_matchedSeeds.Count} 个，评估 {evaluated} 个，{_candidateResults.Count} 条有效结果，成功 {successCount} 个。";
            if (_matchedSeeds.Count > evalCount)
            {
                _candidateEvalSummary += $"（已达上限，仅评估前 {evalCount} 个）";
            }
            if (stoppedEarly && !userCancelled)
            {
                _candidateEvalSummary += $"（{timeoutMs}ms 超时后停止）";
            }
            if (userCancelled)
            {
                _candidateEvalSummary += "（已手动取消）";
            }
            if (spentSec > 0.1f)
            {
                _candidateEvalSummary += $" | 耗时 {spentSec:0.0}s";
            }

            _candidateMatchSummary = _candidateEvalSummary;
        }

        private void MatchCandidateSeeds()
        {
            _matchedSeeds.Clear();
            _candidateResults.Clear();
            _candidateEvalSummary = string.Empty;
            _seedMatchCacheSummary = string.Empty;

            EnsureSeedMatchCache();
            if (_seedMatchCacheEntries.Count == 0)
            {
                _candidateMatchSummary = "Seed 缓存为空：请先点击 Rebuild Seed Cache。";
                return;
            }

            if (_mask == null)
            {
                _candidateMatchSummary = "请先选择一个 Mask。";
                _matchedMaskPath = string.Empty;
                return;
            }

            int maskW = Mathf.Max(1, _mask.width);
            int maskH = Mathf.Max(1, _mask.height);
            _matchedMaskPath = GetAssetPath(_mask);
            var matches = new List<SeedMatchEntry>();
            var fallbackMatches = new List<SeedMatchEntry>();

            for (int i = 0; i < _seedMatchCacheEntries.Count; i++)
            {
                var cached = _seedMatchCacheEntries[i];
                if (cached == null || !cached.authored || string.IsNullOrWhiteSpace(cached.path))
                    continue;
                if (IsGeneratedSeedCacheEntry(cached))
                    continue;

                int seedW = Mathf.Max(1, cached.width);
                int seedH = Mathf.Max(1, cached.height);

                int score = (seedW - maskW) * (seedW - maskW) + (seedH - maskH) * (seedH - maskH);
                if (seedW >= maskW && seedH >= maskH)
                {
                    matches.Add(new SeedMatchEntry
                    {
                        Seed = null,
                        CachedPath = cached.path,
                        SizeScore = score
                    });
                }
                else
                {
                    fallbackMatches.Add(new SeedMatchEntry
                    {
                        Seed = null,
                        CachedPath = cached.path,
                        SizeScore = score
                    });
                }
            }

            if (matches.Count == 0)
            {
                if (fallbackMatches.Count == 0)
                {
                    _candidateMatchSummary = "未匹配到可参与候选的 Seed。";
                    return;
                }

                fallbackMatches.Sort((a, b) =>
                {
                    int scoreCmp = a.SizeScore.CompareTo(b.SizeScore);
                    if (scoreCmp != 0)
                        return scoreCmp;
                    if (string.IsNullOrWhiteSpace(a.CachedPath) || string.IsNullOrWhiteSpace(b.CachedPath))
                        return 0;
                    return string.Compare(a.CachedPath, b.CachedPath, StringComparison.Ordinal);
                });

                int fallbackCount = Math.Min(Math.Min(kMaxMatchCandidates, 80), fallbackMatches.Count);
                for (int i = 0; i < fallbackCount; i++)
                {
                    TryLoadCachedSeed(fallbackMatches[i]);
                    if (fallbackMatches[i].Seed != null)
                    {
                        _matchedSeeds.Add(fallbackMatches[i].Seed);
                    }
                }

                _candidateMatchSummary = $"未匹配到尺寸>=Mask的 Seed（mask={maskW}x{maskH}）。已按尺寸接近度回退匹配 { _matchedSeeds.Count} 个。";
                return;
            }

            matches.Sort((a, b) =>
            {
                int scoreCmp = a.SizeScore.CompareTo(b.SizeScore);
                if (scoreCmp != 0)
                    return scoreCmp;
                if (string.IsNullOrWhiteSpace(a.CachedPath) || string.IsNullOrWhiteSpace(b.CachedPath))
                    return 0;
                return string.Compare(a.CachedPath, b.CachedPath, StringComparison.Ordinal);
            });

            int matchCount = Math.Min(kMaxMatchCandidates, matches.Count);
            for (int i = 0; i < matchCount; i++)
            {
                TryLoadCachedSeed(matches[i]);
                if (matches[i].Seed != null)
                {
                    _matchedSeeds.Add(matches[i].Seed);
                }
            }

            _candidateMatchSummary = $"已匹配到 {_matchedSeeds.Count} 个候选 seed（按尺寸差排序）";
        }

        private void EnsureSeedMatchCache()
        {
            if (_seedMatchCacheLoaded && !_seedMatchCacheDirty)
                return;

            _seedMatchCacheLoaded = true;
            _seedMatchCacheEntries.Clear();

            if (!TryLoadSeedMatchCache(out var loadedCache))
            {
                BuildSeedMatchCache(forceRebuild: true);
                return;
            }

            if (!IsSeedMatchCacheUsable(loadedCache))
            {
                BuildSeedMatchCache(forceRebuild: true);
                return;
            }

            if (loadedCache?.entries != null)
            {
                for (int i = 0; i < loadedCache.entries.Length; i++)
                {
                    var entry = loadedCache.entries[i];
                    if (entry == null || IsGeneratedSeedCacheEntry(entry))
                        continue;

                    if (entry.levelId == null)
                        entry.levelId = string.Empty;

                    _seedMatchCacheEntries.Add(entry);
                }
            }

            if (_seedMatchCacheEntries.Count == 0)
            {
                BuildSeedMatchCache(forceRebuild: true);
            }
            else
            {
                _seedMatchCacheDirty = false;
                _seedMatchCacheSummary = $"已加载 seed 匹配缓存，共 {_seedMatchCacheEntries.Count} 条。";
            }
        }

        private void TryLoadCachedSeed(SeedMatchEntry entry)
        {
            if (entry == null || string.IsNullOrWhiteSpace(entry.CachedPath))
                return;

            if (IsGeneratedSeedPath(entry.CachedPath))
            {
                entry.Seed = null;
                return;
            }

            entry.Seed = AssetDatabase.LoadAssetAtPath<LevelDefinition>(entry.CachedPath);
            if (entry.Seed == null || entry.Seed.authoredLevel == null)
            {
                entry.Seed = null;
                return;
            }

            if (IsGeneratedSeedDefinition(entry.CachedPath, entry.Seed))
            {
                entry.Seed = null;
                return;
            }

            if (entry.Seed.source != LevelDefinition.LevelSource.Authored)
            {
                entry.Seed = null;
            }
        }

        private void BuildSeedMatchCache(bool forceRebuild)
        {
            if (_seedMatchCacheLoaded && !_seedMatchCacheDirty && _seedMatchCacheEntries.Count > 0 && !forceRebuild)
            {
                return;
            }

            _seedMatchCacheEntries.Clear();

            string[] guids = AssetDatabase.FindAssets("t:LevelDefinition", new[] { kSeedMatchRoot });
            if (guids == null || guids.Length == 0)
            {
                _candidateMatchSummary = "未找到 LevelDefinition 资源。";
                _seedMatchCacheSummary = "Seed 缓存构建失败：未找到 LevelDefinition 资源。";
                return;
            }

            for (int i = 0; i < guids.Length; i++)
            {
                string guid = guids[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                if (string.IsNullOrWhiteSpace(path))
                    continue;

                var seed = AssetDatabase.LoadAssetAtPath<LevelDefinition>(path);
                if (seed == null || seed.authoredLevel == null)
                    continue;
                if (IsGeneratedSeedDefinition(path, seed))
                    continue;

                _seedMatchCacheEntries.Add(new SeedMatchCacheEntry
                {
                    guid = guid,
                    path = path,
                    levelId = seed.levelId,
                    width = Mathf.Max(1, seed.authoredLevel.width),
                    height = Mathf.Max(1, seed.authoredLevel.height),
                    authored = seed.source == LevelDefinition.LevelSource.Authored,
                    assetWriteTimeUtcTicks = GetAssetWriteTimeUtcTicks(path)
                });
            }

            if (_seedMatchCacheEntries.Count > 0)
            {
                var entries = new SeedMatchCacheEntry[_seedMatchCacheEntries.Count];
                _seedMatchCacheEntries.CopyTo(entries);
                SaveSeedMatchCache(new SeedMatchCache
                {
                    generatedUtcTicks = DateTime.UtcNow.Ticks,
                    entries = entries
                });
                _seedMatchCacheSummary = $"已重建 seed 缓存，共 {_seedMatchCacheEntries.Count} 条。";
            }
            else
            {
                _seedMatchCacheSummary = "已重建 seed 缓存，但未命中可用资源。";
            }

            _seedMatchCacheDirty = false;
            _seedMatchCacheLoaded = true;
        }

        private bool TryLoadSeedMatchCache(out SeedMatchCache cache)
        {
            cache = null;
            try
            {
                string cachePath = GetSeedMatchCachePath();
                if (!File.Exists(cachePath))
                    return false;

                string json = File.ReadAllText(cachePath);
                if (string.IsNullOrWhiteSpace(json))
                    return false;

                cache = JsonUtility.FromJson<SeedMatchCache>(json);
                if (cache?.entries == null)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[SeedMaskPatch] Failed to load seed cache: {ex.Message}");
                return false;
            }
        }

        private bool IsSeedMatchCacheUsable(SeedMatchCache cache)
        {
            if (cache == null || cache.entries == null || cache.entries.Length == 0)
                return false;

            for (int i = 0; i < cache.entries.Length; i++)
            {
                var entry = cache.entries[i];
                if (entry == null || string.IsNullOrWhiteSpace(entry.path))
                {
                    return false;
                }
                if (IsGeneratedSeedCacheEntry(entry))
                    return false;

                long writeTime = GetAssetWriteTimeUtcTicks(entry.path);
                if (writeTime == 0 || (entry.assetWriteTimeUtcTicks > 0 && entry.assetWriteTimeUtcTicks != writeTime))
                {
                    return false;
                }
            }

            return true;
        }

        private void SaveSeedMatchCache(SeedMatchCache cache)
        {
            if (cache == null)
                return;

            try
            {
                string cachePath = GetSeedMatchCachePath();
                string dir = Path.GetDirectoryName(cachePath);
                if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                string json = JsonUtility.ToJson(cache);
                File.WriteAllText(cachePath, json);
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[SeedMaskPatch] Failed to save seed cache: {ex.Message}");
            }
        }

        private string GetSeedMatchCachePath()
        {
            return Path.GetFullPath(Path.Combine(Application.dataPath, "..", "Library", kSeedMatchCacheFileName));
        }

        private long GetAssetWriteTimeUtcTicks(string assetPath)
        {
            if (string.IsNullOrWhiteSpace(assetPath))
                return 0;

            string fullPath = Path.GetFullPath(Path.Combine(Application.dataPath, "..", assetPath));
            if (!File.Exists(fullPath))
                return 0;

            return File.GetLastWriteTimeUtc(fullPath).Ticks;
        }

        private SeedCandidateResult EvaluateSeedCandidate(LevelDefinition seedDef, Texture2D mask, int topKeepCount)
        {
            if (seedDef == null || mask == null)
                return null;

            bool useInflation = false;
            int inflationPasses = 0;
            float healRatio = 1f;
            bool adaptiveHeal = false;
            float healFloor = 1f;
            int healSteps = 0;

            if (!TryBuildBoardFromSeed(seedDef, out BoardState sourceBoard))
            {
                return new SeedCandidateResult
                {
                    SourceSeed = seedDef,
                    SourceSeedPath = GetAssetPath(seedDef),
                    IsFeasible = false,
                    Score = -99999,
                    Details = "Build board failed."
                };
            }

            if (!TryReadMask(mask, _maskAlphaOnly, useInflation, inflationPasses, out bool[] maskCanSpawn))
            {
                return new SeedCandidateResult
                {
                    SourceSeed = seedDef,
                    SourceSeedPath = GetAssetPath(seedDef),
                    IsFeasible = false,
                    Score = -99000,
                    Details = "Mask parse failed."
                };
            }

            string details;
            LevelDefinition previewSeed = BuildPatchedSeed(
                seedDef,
                sourceBoard,
                mask,
                maskCanSpawn,
                _placementMode,
                _autoIfCenterFails,
                _generationAttempts,
                _fallbackCleanupLoops,
                _layerLikeMaskMode,
                _maxGreedyMovesMultiplier,
                healRatio,
                adaptiveHeal,
                healFloor,
                healSteps,
                _allowGreedyFullChainRemoval,
                _allowGreedyFallbackChainClear,
                _greedyTrimPerChain,
                _candidateMinFillRatio,
                PatchOutputMode.GreedyRescue,
                out details);

            int sourceArrows = CountArrowTiles(sourceBoard);
            int targetFill = 0;
            int fill = 0;
            float fillRatio = 0f;
            bool hasFillMetrics = false;

            if (previewSeed == null)
            {
                int preserved = TryParseIntDetail(details, "PreservedTiles");
                int arrows = TryParseIntDetail(details, "Arrows");
                targetFill = TryParseIntDetail(details, "TargetFill");
                fill = TryParseIntDetail(details, "finalFill");
                if (fill == 0)
                {
                    fill = TryParseIntDetail(details, "preFill");
                }
                if (fill == 0)
                {
                    fill = TryParseIntDetail(details, "fill");
                }
                if (fill == 0 && arrows > 0)
                {
                    fill = arrows;
                }
                hasFillMetrics = targetFill > 0;
                fillRatio = hasFillMetrics ? Mathf.Clamp01((float)fill / targetFill) : 0f;
                int removedChains = TryParseIntDetail(details, "removedChains");
                int removedArrows = TryParseIntDetail(details, "removedArrows");
                string failedPreviewDetails = details;

                return new SeedCandidateResult
                {
                    SourceSeed = seedDef,
                    SourceSeedPath = GetAssetPath(seedDef),
                    IsFeasible = false,
                    SourceArrowCount = sourceArrows,
                    PreservedTiles = preserved,
                    FinalArrowCount = arrows,
                    TargetFill = targetFill,
                    FinalFill = fill,
                    FillRatio = fillRatio,
                    RemovedChains = removedChains,
                    RemovedArrows = removedArrows,
                    Score = ComputeCandidateScore(false, preserved, arrows, targetFill, fill, removedChains, removedArrows, topKeepCount),
                    Details = failedPreviewDetails
                };
            }

            int finalArrows = TryParseIntDetail(details, "Arrows");
            int preservedFinal = TryParseIntDetail(details, "PreservedTiles");
            int targetFillFinal = TryParseIntDetail(details, "TargetFill");
            int filledTiles = TryParseIntDetail(details, "finalFill");
            if (filledTiles == 0)
            {
                filledTiles = TryParseIntDetail(details, "preFill");
            }
            if (filledTiles == 0)
            {
                filledTiles = TryParseIntDetail(details, "fill");
            }
            if (filledTiles == 0 && finalArrows > 0)
            {
                filledTiles = finalArrows;
            }
            targetFill = targetFillFinal;
            fill = filledTiles;
            hasFillMetrics = targetFill > 0;
            fillRatio = hasFillMetrics ? Mathf.Clamp01((float)fill / targetFill) : 0f;
            bool passFillRatioFinal = !hasFillMetrics || fillRatio >= _candidateMinFillRatio;
            if (targetFillFinal <= 0)
                targetFillFinal = filledTiles;
            string finalPreviewDetails = details;
            if (!passFillRatioFinal && hasFillMetrics)
            {
                finalPreviewDetails = string.IsNullOrWhiteSpace(finalPreviewDetails)
                    ? $"FillRatio={fillRatio:0.00} below min {_candidateMinFillRatio:0.00}"
                    : $"{finalPreviewDetails} | FillRatio={fillRatio:0.00} below min {_candidateMinFillRatio:0.00}";
            }

            return new SeedCandidateResult
            {
                SourceSeed = seedDef,
                SourceSeedPath = GetAssetPath(seedDef),
                IsFeasible = passFillRatioFinal,
                SourceArrowCount = sourceArrows,
                PreservedTiles = preservedFinal,
                FinalArrowCount = finalArrows > 0 ? finalArrows : filledTiles,
                TargetFill = targetFillFinal,
                FinalFill = filledTiles,
                FillRatio = fillRatio,
                RemovedChains = TryParseIntDetail(details, "removedChains"),
                RemovedArrows = TryParseIntDetail(details, "removedArrows"),
                Score = ComputeCandidateScore(passFillRatioFinal, preservedFinal, finalArrows > 0 ? finalArrows : filledTiles, targetFillFinal, filledTiles, TryParseIntDetail(details, "removedChains"), TryParseIntDetail(details, "removedArrows"), topKeepCount),
                Details = finalPreviewDetails
            };
        }

        private void GenerateTopCandidates(int count)
        {
            if (_mask == null || _candidateResults.Count == 0 || count <= 0)
                return;

            count = Math.Min(count, _candidateResults.Count);
            string latestGenerated = null;
            LevelDefinition latestSource = null;
            int generatedCount = 0;
            for (int i = 0; i < count; i++)
            {
                var result = _candidateResults[i];
                if (result == null || !result.IsFeasible || result.SourceSeed == null)
                    continue;

                string generated = ProcessSingle(result.SourceSeed, _mask);
                if (!string.IsNullOrWhiteSpace(generated))
                {
                    latestGenerated = generated;
                    latestSource = result.SourceSeed;
                    generatedCount++;
                }
            }

            if (!string.IsNullOrWhiteSpace(latestGenerated) && latestSource != null)
            {
                TrySyncDemoComparePack(latestSource, latestGenerated, _latestRawClipPath);
                Debug.Log($"[SeedMaskPatch] Generated {generatedCount} feasible candidates, latest synced to demo: {latestGenerated}");
            }
            else
            {
                Debug.LogWarning("[SeedMaskPatch] 未生成可达标候选，未更新 demo 包。");
            }
        }

        private static string BuildCandidateResultLine(int index, SeedCandidateResult candidate)
        {
            if (candidate == null)
                return $"{index}. <invalid>";

            if (!candidate.IsFeasible)
                return $"{index}. {candidate.NameOrPath} | 不可用 | {candidate.Details}";

            int fill = candidate.TargetFill > 0 ? candidate.TargetFill : candidate.FinalFill;
            int gap = Math.Max(0, fill - candidate.FinalFill);
            float ratio = candidate.TargetFill > 0
                ? (float)candidate.FinalFill / candidate.TargetFill
                : candidate.FillRatio;
            string ratioText = ratio > 0f ? $"{ratio:0.00}" : "n/a";
            return $"{index}. {candidate.NameOrPath} | 可用 | 保留={candidate.PreservedTiles} | 箭头={candidate.FinalArrowCount} | 缺口={gap} | 填充={ratioText} | 去尾/改写={candidate.RemovedArrows}";
        }

        private static int ComputeCandidateScore(
            bool feasible,
            int preserved,
            int finalArrows,
            int targetFill,
            int fill,
            int removedChains,
            int removedArrows,
            int topKeepCount)
        {
            if (!feasible)
                return -100000 + (topKeepCount * 10);

            int gap = Math.Max(0, targetFill - fill);
            int score = preserved * 16 + finalArrows * 8 - gap * 22 - removedChains * 45 - removedArrows * 3;
            return score;
        }

        private static int TryParseIntDetail(string details, string key)
        {
            if (string.IsNullOrWhiteSpace(details) || string.IsNullOrWhiteSpace(key))
                return 0;

            string mark = key + "=";
            int idx = details.IndexOf(mark, StringComparison.Ordinal);
            if (idx < 0)
                return 0;

            idx += mark.Length;
            int sign = 1;
            if (idx < details.Length && details[idx] == '-')
            {
                sign = -1;
                idx++;
            }

            int start = idx;
            while (idx < details.Length)
            {
                char c = details[idx];
                if (c < '0' || c > '9')
                    break;

                idx++;
            }

            if (idx == start)
                return 0;

            int value;
            if (!int.TryParse(details.Substring(start, idx - start), out value))
                return 0;

            return value * sign;
        }

        private static int CompareCandidateResults(SeedCandidateResult a, SeedCandidateResult b)
        {
            if (a == null && b == null)
                return 0;
            if (a == null)
                return 1;
            if (b == null)
                return -1;

            if (a.IsFeasible != b.IsFeasible)
                return b.IsFeasible.CompareTo(a.IsFeasible);

            if (a.IsFeasible)
            {
                int result = b.PreservedTiles.CompareTo(a.PreservedTiles);
                if (result != 0)
                    return result;

                result = b.FinalArrowCount.CompareTo(a.FinalArrowCount);
                if (result != 0)
                    return result;

                result = (b.TargetFill - b.FinalFill).CompareTo(a.TargetFill - a.FinalFill);
                if (result != 0)
                    return result;

                return b.Score.CompareTo(a.Score);
            }

            return b.Score.CompareTo(a.Score);
        }

        private static bool IsGeneratedSeedCacheEntry(SeedMatchCacheEntry entry)
        {
            if (entry == null)
                return false;

            return IsGeneratedSeedPath(entry.path)
                || !string.IsNullOrWhiteSpace(entry.levelId)
                    && entry.levelId.IndexOf("_maskpatch", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static bool IsGeneratedSeedPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            string safePath = path.Replace('\\', '/').ToLowerInvariant();
            string fileName = Path.GetFileNameWithoutExtension(safePath);
            return safePath.Contains("/seed_mask_") || fileName.Contains("_maskpatch");
        }

        private static bool IsGeneratedSeedDefinition(string path, LevelDefinition seed)
        {
            if (IsGeneratedSeedPath(path))
                return true;

            return seed != null
                && !string.IsNullOrWhiteSpace(seed.levelId)
                && seed.levelId.IndexOf("_maskpatch", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static bool TryCreateDemoComparePack(LevelDefinition sourceSeed, string latestSeedPath)
        {
            return TrySyncDemoComparePack(sourceSeed, latestSeedPath);
        }

        private static bool TryUpdateDemoPackLatestSeed(LevelDefinition sourceSeed, string latestSeedPath)
        {
            return TrySyncDemoComparePack(sourceSeed, latestSeedPath);
        }

        private static bool TrySyncDemoComparePack(LevelDefinition sourceSeed, string latestSeedPath)
        {
            return TrySyncDemoComparePack(sourceSeed, latestSeedPath, null);
        }

        private static bool TrySyncDemoFinalPairPack(LevelDefinition sourceSeed, string firstFinalPath, string secondFinalPath)
        {
            if (string.IsNullOrWhiteSpace(firstFinalPath) || string.IsNullOrWhiteSpace(secondFinalPath))
                return false;

            var firstFinal = AssetDatabase.LoadAssetAtPath<LevelDefinition>(firstFinalPath);
            var secondFinal = AssetDatabase.LoadAssetAtPath<LevelDefinition>(secondFinalPath);
            if (firstFinal == null || secondFinal == null)
            {
                Debug.LogWarning($"[SeedMaskPatch] Final pair demo update failed: first={firstFinalPath}, second={secondFinalPath}");
                return false;
            }

            EnsureFolderExists(kDemoComparePackFolder);
            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(kDemoComparePackPath);
            bool isNewPack = pack == null;
            if (pack == null)
                pack = ScriptableObject.CreateInstance<LevelPack>();

            string safeSeed = "holeblock_multiseed";
            if (sourceSeed != null)
            {
                safeSeed = SanitizeName(sourceSeed.name);
                if (string.IsNullOrWhiteSpace(safeSeed))
                    safeSeed = SanitizeName(sourceSeed.levelId);
            }

            var levels = new List<LevelDefinition>(3);
            if (sourceSeed != null)
                levels.Add(sourceSeed);
            levels.Add(firstFinal);
            levels.Add(secondFinal);

            pack.levels = levels.ToArray();
            pack.packId = $"{kDemoComparePackPrefix}_{safeSeed}_FinalPair";
            pack.displayName = $"Seed Mask Compare - {safeSeed} (Source / Final A / Final B)";
            EditorUtility.SetDirty(pack);

            if (isNewPack)
                AssetDatabase.CreateAsset(pack, kDemoComparePackPath);

            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(kDemoComparePackPath);

            var checkPack = AssetDatabase.LoadAssetAtPath<LevelPack>(kDemoComparePackPath);
            if (checkPack == null || checkPack.levels == null || checkPack.levels.Length < 2)
            {
                Debug.LogWarning($"[SeedMaskPatch] Final pair demo pack verification failed: path {kDemoComparePackPath} invalid after sync.");
                return false;
            }

            string setPath = AssetDatabase.GetAssetPath(checkPack.levels[checkPack.levels.Length - 1]);
            if (!string.Equals(setPath, secondFinalPath, StringComparison.Ordinal))
            {
                Debug.LogWarning($"[SeedMaskPatch] Final pair demo pack verification failed: expected {secondFinalPath}, actual {setPath}");
                return false;
            }

            Debug.Log($"[SeedMaskPatch] Demo final pair updated: first={firstFinalPath}, second={secondFinalPath}");
            return true;
        }

        private static bool TrySyncDemoComparePack(LevelDefinition sourceSeed, string latestSeedPath, string rawClipSeedPath)
        {
            if (string.IsNullOrWhiteSpace(latestSeedPath))
                return false;

            var latestSeed = AssetDatabase.LoadAssetAtPath<LevelDefinition>(latestSeedPath);
            if (latestSeed == null)
            {
                Debug.LogWarning($"[SeedMaskPatch] Generated level not found for latest demo pack update: {latestSeedPath}");
                return false;
            }

            EnsureFolderExists(kDemoComparePackFolder);
            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(kDemoComparePackPath);
            bool isNewPack = pack == null;

            if (pack == null)
            {
                pack = ScriptableObject.CreateInstance<LevelPack>();
            }

            string safeSeed = "seed";
            if (sourceSeed != null)
            {
                safeSeed = SanitizeName(sourceSeed.name);
                if (string.IsNullOrWhiteSpace(safeSeed))
                    safeSeed = SanitizeName(sourceSeed.levelId);
                if (string.IsNullOrWhiteSpace(safeSeed))
                safeSeed = "seed";
            }

            var levels = new List<LevelDefinition>(3);
            if (sourceSeed != null)
                levels.Add(sourceSeed);

            if (!string.IsNullOrWhiteSpace(rawClipSeedPath)
                && !string.Equals(rawClipSeedPath, latestSeedPath, StringComparison.Ordinal))
            {
                var rawClipSeed = AssetDatabase.LoadAssetAtPath<LevelDefinition>(rawClipSeedPath);
                if (rawClipSeed != null)
                    levels.Add(rawClipSeed);
                else
                    Debug.LogWarning($"[SeedMaskPatch] Raw clip level not found for demo compare pack: {rawClipSeedPath}");
            }

            levels.Add(latestSeed);

            pack.levels = levels.ToArray();

            string modeTag = !string.IsNullOrWhiteSpace(rawClipSeedPath)
                ? string.Equals(rawClipSeedPath, latestSeedPath, StringComparison.Ordinal)
                    ? "Source / Raw Clip"
                    : "Source / Raw Clip / Rescue"
                : "Source / Rescue";

            pack.packId = $"{kDemoComparePackPrefix}_{safeSeed}";
            pack.displayName = $"Seed Mask Compare - {safeSeed} ({modeTag})";
            EditorUtility.SetDirty(pack);

            if (isNewPack)
            {
                AssetDatabase.CreateAsset(pack, kDemoComparePackPath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(kDemoComparePackPath);

            var checkPack = AssetDatabase.LoadAssetAtPath<LevelPack>(kDemoComparePackPath);
            if (checkPack == null || checkPack.levels == null || checkPack.levels.Length == 0 || checkPack.levels[checkPack.levels.Length - 1] == null)
            {
                Debug.LogWarning($"[SeedMaskPatch] Demo compare pack verification failed: path {kDemoComparePackPath} invalid after sync.");
                return false;
            }

            string setPath = AssetDatabase.GetAssetPath(checkPack.levels[checkPack.levels.Length - 1]);
            if (!string.Equals(setPath, latestSeedPath, StringComparison.Ordinal))
            {
                Debug.LogWarning($"[SeedMaskPatch] Demo compare pack verification failed: expected {latestSeedPath}, actual {setPath}");
                return false;
            }

            Debug.Log($"[SeedMaskPatch] Demo compare pack latest updated: {latestSeedPath}");
            return true;
        }

        private static void EnsureFolderExists(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
                return;

            string normalized = folderPath.Replace("\\", "/").Trim();
            if (string.IsNullOrWhiteSpace(normalized))
                return;

            string[] parts = normalized.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
                return;

            string current = parts[0];
            if (!current.Equals("Assets", StringComparison.OrdinalIgnoreCase))
                current = "Assets";

            for (int i = 1; i < parts.Length; i++)
            {
                string next = $"{current}/{parts[i]}";
                if (!AssetDatabase.IsValidFolder(next))
                    AssetDatabase.CreateFolder(current, parts[i]);
                current = next;
            }
        }

        private static string MakeOutputFileName(LevelDefinition seedDef, Texture2D mask, string prefix)
        {
            string safePrefix = SanitizeName(prefix);
            string seedName = !string.IsNullOrWhiteSpace(seedDef.name)
                ? seedDef.name
                : !string.IsNullOrWhiteSpace(seedDef.levelId)
                    ? seedDef.levelId
                    : "seed";

            string maskName = !string.IsNullOrWhiteSpace(mask.name)
                ? mask.name
                : "mask";

            if (!string.IsNullOrWhiteSpace(safePrefix))
            {
                safePrefix = $"{safePrefix}_";
            }

            string time = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            return $"{safePrefix}{SanitizeName(seedName)}_{SanitizeName(maskName)}_{time}.asset";
        }

        private static string MakeRawClipOutputPrefix(string outputPrefix)
        {
            string safePrefix = SanitizeName(outputPrefix);
            if (string.IsNullOrWhiteSpace(safePrefix))
                return kRawClipOutputPrefixSuffix;

            return $"{safePrefix}_{kRawClipOutputPrefixSuffix}";
        }

        private static bool TryParseOffsetDetail(string details, out Vector2Int offset)
        {
            offset = default;
            if (string.IsNullOrWhiteSpace(details))
                return false;

            const string mark = "Offset=";
            int idx = details.IndexOf(mark, StringComparison.Ordinal);
            if (idx < 0)
                return false;

            idx += mark.Length;
            int comma = details.IndexOf(',', idx);
            if (comma < 0)
                return false;

            int end = comma + 1;
            while (end < details.Length)
            {
                char c = details[end];
                if ((c < '0' || c > '9') && c != '-')
                    break;

                end++;
            }

            if (!int.TryParse(details.Substring(idx, comma - idx), out int x))
                return false;
            if (!int.TryParse(details.Substring(comma + 1, end - comma - 1), out int y))
                return false;

            offset = new Vector2Int(x, y);
            return true;
        }

        private static string SanitizeName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var safe = input;
            foreach (char c in Path.GetInvalidFileNameChars())
                safe = safe.Replace(c, '_');
            return safe.Trim();
        }

        private static string GetAssetPath(UnityEngine.Object obj)
        {
            return obj == null ? "<null>" : AssetDatabase.GetAssetPath(obj);
        }

        private void EnsureOutputFolder()
        {
            if (string.IsNullOrWhiteSpace(_outputFolder))
            {
                _outputFolder = kDefaultOutputFolder;
            }

            string normalized = _outputFolder.Replace("\\", "/").Trim();
            if (!normalized.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase) && normalized != "Assets")
            {
                normalized = kDefaultOutputFolder;
                _outputFolder = normalized;
            }

            string[] parts = normalized.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
            {
                _outputFolder = kDefaultOutputFolder;
                return;
            }

            string current = parts[0];
            if (!current.Equals("Assets", StringComparison.OrdinalIgnoreCase))
                current = "Assets";

            for (int i = 1; i < parts.Length; i++)
            {
                string next = $"{current}/{parts[i]}";
                if (!AssetDatabase.IsValidFolder(next))
                {
                    AssetDatabase.CreateFolder(current, parts[i]);
                }
                current = next;
            }
        }

        private static bool TryBuildBoardFromSeed(LevelDefinition seedDef, out BoardState sourceBoard)
        {
            sourceBoard = null;

            if (seedDef == null)
                return false;

            if (seedDef.source != LevelDefinition.LevelSource.Authored)
            {
                Debug.LogWarning("[SeedMaskPatch] Source seed must be Authored.");
                return false;
            }

            if (seedDef.authoredLevel == null)
            {
                Debug.LogWarning("[SeedMaskPatch] Seed has no AuthoredLevelData.");
                return false;
            }

            if (!AuthoredLevelBuilder.TryBuildBoard(seedDef.authoredLevel, out sourceBoard, out string err))
            {
                Debug.LogWarning($"[SeedMaskPatch] Authored build failed: {err}");
                return false;
            }

            return true;
        }

        private bool TryReadMask(
            Texture2D mask,
            bool useAlphaOnly,
            bool inflateForSolvability,
            int inflatePasses,
            out bool[] canSpawn)
        {
            canSpawn = null;
            if (mask == null)
                return false;

            if (!mask.isReadable)
            {
                Debug.LogWarning($"[SeedMaskPatch] Mask texture is not Read/Write enabled: {mask.name}");
                return false;
            }

            canSpawn = new bool[mask.width * mask.height];
            Color32[] pixels = mask.GetPixels32();
            int totalAllowed = 0;

            int allowedByAlpha = 0;
            int allowedByLuma = 0;

            for (int y = 0; y < mask.height; y++)
            {
                for (int x = 0; x < mask.width; x++)
                {
                    int idx = x + y * mask.width;
                    var pixel = pixels[idx];
                    byte luma = (byte)((pixel.r + pixel.g + pixel.b) / 3);
                    bool byAlpha = pixel.a > 127;
                    bool byLuma = luma > 127;

                    bool canSpawnCell = useAlphaOnly ? byAlpha : (byAlpha || byLuma);
                    canSpawn[idx] = canSpawnCell;
                    if (canSpawnCell)
                        totalAllowed++;
                    if (byAlpha && canSpawnCell)
                        allowedByAlpha++;
                    if (!useAlphaOnly && byLuma && canSpawnCell && !byAlpha)
                        allowedByLuma++;
                }
            }

            int rawAllowed = totalAllowed;
            if (inflateForSolvability && inflatePasses > 0)
            {
                canSpawn = InflateMask(canSpawn, mask.width, mask.height, inflatePasses);
                totalAllowed = CountMaskCells(canSpawn);
            }

            if (totalAllowed == 0)
            {
                string reason = rawAllowed == 0 ? "thresholding" : "thresholding+inflation";
                Debug.LogWarning($"[SeedMaskPatch] Mask has no spawn cells after {reason}: {mask.name}");
            }
            else
            {
                if (inflateForSolvability && inflatePasses > 0 && totalAllowed != rawAllowed)
                {
                    Debug.Log($"[SeedMaskPatch] Mask parse stats: raw={rawAllowed}/{canSpawn.Length}, inflated={totalAllowed}/{canSpawn.Length}, passes={inflatePasses}, mode={(useAlphaOnly ? "alpha-only" : "alpha-or-luma")}, alphaOnlyCells={allowedByAlpha}, lumaOnlyCells={allowedByLuma}");
                }
                else
                {
                    Debug.Log($"[SeedMaskPatch] Mask parse stats: allowed={totalAllowed}/{canSpawn.Length}, mode={(useAlphaOnly ? "alpha-only" : "alpha-or-luma")}, alphaOnlyCells={allowedByAlpha}, lumaOnlyCells={allowedByLuma}");
                }
            }

            return true;
        }

        private static int CountMaskCells(bool[] maskArea)
        {
            if (maskArea == null || maskArea.Length == 0)
                return 0;

            int count = 0;
            for (int i = 0; i < maskArea.Length; i++)
                if (maskArea[i])
                    count++;

            return count;
        }

        private static bool[] InflateMask(bool[] maskArea, int width, int height, int passes)
        {
            if (maskArea == null || width <= 0 || height <= 0)
                return maskArea;

            if (passes <= 0)
                return (bool[])maskArea.Clone();

            var current = (bool[])maskArea.Clone();
            var next = new bool[current.Length];

            for (int p = 0; p < passes; p++)
            {
                Array.Copy(current, next, current.Length);
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int idx = x + y * width;
                        if (current[idx])
                        {
                            next[idx] = true;
                            continue;
                        }

                        bool touch = false;
                        if (x > 0 && current[idx - 1]) touch = true;
                        if (x < width - 1 && current[idx + 1]) touch = true;
                        if (y > 0 && current[idx - width]) touch = true;
                        if (y < height - 1 && current[idx + width]) touch = true;

                        if (touch)
                            next[idx] = true;
                    }
                }

                var swap = current;
                current = next;
                next = swap;
                Array.Clear(next, 0, next.Length);
            }

            return current;
        }

        private static List<float> BuildAdaptiveFillRatios(float targetFillRatio, bool adaptiveFallback, float fillFloor, int fallbackSteps)
        {
            float target = Mathf.Clamp01(targetFillRatio);
            float floor = Mathf.Clamp01(fillFloor);
            if (floor > target)
                floor = target;
            if (target <= 0f)
                target = 1f;

            var ratios = new List<float>(Mathf.Max(1, fallbackSteps + 1))
            {
                target
            };

            if (!adaptiveFallback || fallbackSteps <= 0 || floor >= target)
                return ratios;

            int steps = Mathf.Max(1, fallbackSteps);
            for (int i = 1; i <= steps; i++)
            {
                float ratio = Mathf.Lerp(target, floor, i / (float)steps);
                ratio = Mathf.Clamp01((float)Math.Round(ratio, 4));
                if (ratio >= target - 0.0001f || ratio <= floor + 0.0001f)
                    continue;

                if (ratios[ratios.Count - 1] - ratio < 0.0001f)
                    continue;

                if (!ratios.Contains(ratio))
                    ratios.Add(ratio);
            }

            if (ratios.Count == 0 || ratios[ratios.Count - 1] > floor)
            {
                ratios.Add(floor);
            }

            return ratios;
        }

        private LevelDefinition BuildPatchedSeed(
            LevelDefinition sourceDef,
            BoardState sourceBoard,
            Texture2D mask,
            bool[] maskCanSpawn,
            PlacementMode mode,
            bool autoIfCenterFails,
            int generationAttempts,
            int cleanupLoops,
            bool layerLikeMaskMode,
            int greedyMultiplier,
            float healFillRatio,
            bool adaptiveHealFillFallback,
            float healFillRatioFloor,
            int healFallbackSteps,
            bool allowGreedyFullChainRemoval,
            bool allowGreedyFallbackChainClear,
            int greedyTrimPerChainOverride,
            float minPostGreedyFillRatio,
            PatchOutputMode outputMode,
            out string details)
        {
            details = string.Empty;
            int sourceW = sourceBoard.width;
            int sourceH = sourceBoard.height;
            int targetW = mask.width;
            int targetH = mask.height;

            var attempts = BuildPlacementOffsets(sourceW, sourceH, targetW, targetH, mode, autoIfCenterFails);

            string bestDetails = "No successful placement attempted.";
            int bestArrowCount = -1;
            int bestPreserve = -1;

            for (int i = 0; i < attempts.Count; i++)
            {
                Vector2Int offset = attempts[i];
                bool ok = TryBuildPatchedBoard(
                    sourceDef,
                    sourceBoard,
                    maskCanSpawn,
                    targetW,
                    targetH,
                    offset,
                    generationAttempts,
                    layerLikeMaskMode,
                    cleanupLoops,
                    greedyMultiplier,
                    healFillRatio,
                    adaptiveHealFillFallback,
                    healFillRatioFloor,
                    healFallbackSteps,
                    allowGreedyFullChainRemoval,
                    allowGreedyFallbackChainClear,
                    greedyTrimPerChainOverride,
                    minPostGreedyFillRatio,
                    outputMode,
                    out BoardState board,
                    out string attemptDetails,
                    out int preservedChainCount,
                    out int generatedArrowCount);

                if (!string.IsNullOrWhiteSpace(attemptDetails))
                {
                    bestDetails = attemptDetails;
                }

            if (ok && board != null && BoardStateHasArrows(board))
            {
                string authoredDetails;
                var outputSeed = BuildAuthoredLevelDefinition(sourceDef, mask, board, out authoredDetails);
                if (outputSeed == null)
                {
                        bestDetails = $"Offset={offset.x},{offset.y} | PreservedTiles={preservedChainCount} | Arrows={CountArrowTiles(board)} | {authoredDetails}";
                        continue;
                    }

                if (!string.IsNullOrWhiteSpace(authoredDetails))
                {
                    details = $"Offset={offset.x},{offset.y} | PreservedTiles={preservedChainCount} | Arrows={CountArrowTiles(board)} | {attemptDetails} | {authoredDetails}";
                }
                else
                {
                    details = !string.IsNullOrWhiteSpace(attemptDetails)
                        ? $"Offset={offset.x},{offset.y} | PreservedTiles={preservedChainCount} | Arrows={CountArrowTiles(board)} | {attemptDetails}"
                        : $"Offset={offset.x},{offset.y} | PreservedTiles={preservedChainCount} | Arrows={CountArrowTiles(board)}";
                }
                return outputSeed;
            }

                if (board != null)
                {
                    bool better = generatedArrowCount > bestArrowCount ||
                                  (generatedArrowCount == bestArrowCount && preservedChainCount > bestPreserve);

                    if (better)
                    {
                        bestArrowCount = generatedArrowCount;
                        bestPreserve = preservedChainCount;
                        bestDetails = attemptDetails;
                    }
                }
            }

            details = string.IsNullOrWhiteSpace(bestDetails)
                ? "All placements failed."
                : bestDetails;
            return null;
        }

        private static List<Vector2Int> BuildPlacementOffsets(
            int sourceW,
            int sourceH,
            int targetW,
            int targetH,
            PlacementMode mode,
            bool autoIfCenterFails)
        {
            var attempts = new List<Vector2Int>();
            var center = new Vector2Int((targetW - sourceW) / 2, (targetH - sourceH) / 2);
            var seen = new HashSet<Vector2Int>();

            void AddOffset(Vector2Int offset)
            {
                if (seen.Add(offset))
                    attempts.Add(offset);
            }

            AddOffset(center);
            if (mode == PlacementMode.Auto || (mode == PlacementMode.Center && autoIfCenterFails))
            {
                var autoCandidates = BuildAutoOffsets(sourceW, sourceH, targetW, targetH);
                for (int i = 0; i < autoCandidates.Count; i++)
                    AddOffset(autoCandidates[i]);
            }

            return attempts;
        }

        private static bool TryBuildRawClipSeed(
            LevelDefinition sourceDef,
            BoardState sourceBoard,
            Texture2D mask,
            bool[] maskCanSpawn,
            PlacementMode mode,
            bool autoIfCenterFails,
            bool preferOffset,
            Vector2Int preferredOffset,
            out LevelDefinition rawClipSeed,
            out string details)
        {
            rawClipSeed = null;
            details = string.Empty;

            if (sourceDef == null || sourceBoard == null || mask == null || maskCanSpawn == null)
            {
                details = "RawClip invalid input.";
                return false;
            }

            int targetW = mask.width;
            int targetH = mask.height;
            if (maskCanSpawn.Length != targetW * targetH)
            {
                details = "RawClip invalid mask shape.";
                return false;
            }

            var attempts = BuildPlacementOffsets(sourceBoard.width, sourceBoard.height, targetW, targetH, mode, autoIfCenterFails);
            if (preferOffset)
            {
                attempts.Remove(preferredOffset);
                attempts.Insert(0, preferredOffset);
            }

            int maskArea = CountMaskArea(maskCanSpawn);
            string bestDetails = string.Empty;
            int bestRawArrows = -1;
            int bestAuthoredArrows = -1;
            int bestPreserved = -1;
            LevelDefinition bestSeed = null;
            string bestSeedDetails = string.Empty;

            for (int i = 0; i < attempts.Count; i++)
            {
                Vector2Int offset = attempts[i];
                if (!BuildMaskedBoardFromSeed(sourceBoard, maskCanSpawn, offset, targetW, targetH, out BoardState rawBoard, out int preserved))
                {
                    bestDetails = $"RawClip Offset={offset.x},{offset.y} | No source arrow cells fall inside mask.";
                    continue;
                }

                int rawArrows = CountArrowTiles(rawBoard);
                if (rawArrows > bestRawArrows)
                {
                    bestRawArrows = rawArrows;
                    bestDetails = $"RawClip Offset={offset.x},{offset.y} | MaskCells={maskArea} | PreservedTiles={preserved} | RawArrows={rawArrows}";
                }

                var authoredBoard = CloneBoard(rawBoard);
                var output = BuildAuthoredLevelDefinition(sourceDef, mask, authoredBoard, out string authoredDetails, "rawclip");
                if (output == null)
                {
                    bestDetails = $"{bestDetails} | RawClipAuthoringFail={authoredDetails}";
                    continue;
                }

                int authoredArrows = CountArrowTiles(authoredBoard);
                int removedByAuthoring = Math.Max(0, rawArrows - authoredArrows);
                float maskFillRatio = maskArea > 0 ? (float)authoredArrows / maskArea : 0f;
                string candidateDetails = $"RawClip Offset={offset.x},{offset.y} | MaskCells={maskArea} | PreservedTiles={preserved} | RawArrows={rawArrows} | AuthoredArrows={authoredArrows} | RawAuthoredRemovedBadChains={removedByAuthoring} | MaskFill={authoredArrows}/{maskArea}({maskFillRatio:0.00})";
                if (!string.IsNullOrWhiteSpace(authoredDetails))
                {
                    candidateDetails = $"{candidateDetails} | {authoredDetails}";
                }

                if (preferOffset && offset == preferredOffset)
                {
                    rawClipSeed = output;
                    details = candidateDetails;
                    return true;
                }

                bool better = rawArrows > bestRawArrows
                    || (rawArrows == bestRawArrows && authoredArrows > bestAuthoredArrows)
                    || (rawArrows == bestRawArrows && authoredArrows == bestAuthoredArrows && preserved > bestPreserved);

                if (better)
                {
                    bestRawArrows = rawArrows;
                    bestAuthoredArrows = authoredArrows;
                    bestPreserved = preserved;
                    bestSeed = output;
                    bestSeedDetails = candidateDetails;
                }
            }

            if (bestSeed != null)
            {
                rawClipSeed = bestSeed;
                details = bestSeedDetails;
                if (preferOffset)
                    details = $"{details} | PreferredRawOffsetUnavailable={preferredOffset.x},{preferredOffset.y}";
                return true;
            }

            details = string.IsNullOrWhiteSpace(bestDetails) ? "RawClip failed." : bestDetails;
            return false;
        }

        private static bool BoardStateHasArrows(BoardState board)
        {
            if (board == null || board.tiles == null)
                return false;

            for (int i = 0; i < board.tiles.Length; i++)
            {
                if (board.tiles[i].type == TileType.Arrow)
                    return true;
            }

            return false;
        }

        private bool TryBuildPatchedBoard(
            LevelDefinition sourceDef,
            BoardState sourceBoard,
            bool[] maskCanSpawn,
            int targetW,
            int targetH,
            Vector2Int offset,
            int generationAttempts,
            bool layerLikeMaskMode,
            int cleanupLoops,
            int greedyMultiplier,
            float healFillRatio,
            bool adaptiveHealFillFallback,
            float healFillRatioFloor,
            int healFallbackSteps,
            bool allowGreedyFullChainRemoval,
            bool allowGreedyFallbackChainClear,
            int greedyTrimPerChainOverride,
            float minPostGreedyFillRatio,
            PatchOutputMode outputMode,
            out BoardState result,
            out string details,
            out int preservedChainCount,
            out int generatedArrowCount)
        {
            result = null;
            details = string.Empty;
            preservedChainCount = 0;
            generatedArrowCount = 0;

            if (sourceBoard == null || maskCanSpawn == null || maskCanSpawn.Length != targetW * targetH)
            {
                details = "Invalid source board or mask shape.";
                return false;
            }

            minPostGreedyFillRatio = Mathf.Clamp01(minPostGreedyFillRatio);
            if (outputMode == PatchOutputMode.GreedyRescue)
                minPostGreedyFillRatio = Mathf.Min(minPostGreedyFillRatio, kGreedyRepairEmergencyFloorRatio);

            bool ok = BuildMaskedBoardFromSeed(
                sourceBoard,
                maskCanSpawn,
                offset,
                targetW,
                targetH,
                out BoardState baseBoard,
                out preservedChainCount);

            if (!ok)
            {
                details = $"Offset={offset.x},{offset.y} | No source arrow cells fall inside mask after offset.";
                return false;
            }

            int greedyMoves = Math.Max(1, targetW * targetH * Mathf.Max(1, greedyMultiplier));
            var candidate = CloneBoard(baseBoard);
            int minPathLen = Mathf.Max(2, sourceDef != null ? sourceDef.generation.minPathLen : BoardController.DefaultMinArrowCellCount);
            int maskArea = CountMaskArea(maskCanSpawn);

            if (layerLikeMaskMode)
            {
                if (!PreserveMaskedChainsBySplitting(sourceBoard, candidate, maskCanSpawn, offset, out int keptTileCount, out string clipDetails))
                {
                    details = $"Offset={offset.x},{offset.y} | PreservedTiles={keptTileCount} | Arrows={CountArrowTiles(candidate)} | {clipDetails}";
                    return false;
                }

                preservedChainCount = keptTileCount;
            }
            else
            {
                for (int cleanup = 0; cleanup < cleanupLoops; cleanup++)
                {
                    ClearClippedChainsByMask(candidate, maskCanSpawn, minPathLen);
                }
            }

            int arrowsBeforeClamp = CountArrowTiles(candidate);
            int maskRemoved = ClampBoardToMask(candidate, maskCanSpawn);
            if (maskRemoved > 0)
            {
                string clampTag = $"MaskClampRemoved={maskRemoved}";
                details = string.IsNullOrWhiteSpace(details) ? clampTag : $"{details} | {clampTag}";
            }

            int arrowsAfterClamp = CountArrowTiles(candidate);
            if (arrowsAfterClamp == 0)
            {
                details = $"Offset={offset.x},{offset.y} | PreservedTiles={preservedChainCount} | Arrows={arrowsAfterClamp} | NoArrowsAfterMaskClamp";
                return false;
            }

            if (_preGreedyGeometryFixPasses > 0)
            {
                int geometryTrimCap = Mathf.Max(1, kGreedyTrimPerChain);
                int geometryRemoved;
                string geometryDetails;

                if (!ApplyPreGreedyGeometryFix(
                    ref candidate,
                    maskCanSpawn,
                    _preGreedyGeometryFixPasses,
                    geometryTrimCap,
                    out geometryRemoved,
                    out geometryDetails))
                {
                    string geometryTag = string.IsNullOrWhiteSpace(geometryDetails)
                        ? $"PreGreedyGeometryPassFail removed={geometryRemoved}"
                        : $"PreGreedyGeometryPassFail removed={geometryRemoved}|{geometryDetails}";
                    details = string.IsNullOrWhiteSpace(details) ? geometryTag : $"{details} | {geometryTag}";
                }
                else if (!string.IsNullOrWhiteSpace(geometryDetails))
                {
                    details = string.IsNullOrWhiteSpace(details) ? geometryDetails : $"{details} | {geometryDetails}";
                }
            }

            bool enableFillHeal = outputMode == PatchOutputMode.FillRescue;
            var fillRatios = enableFillHeal
                ? BuildAdaptiveFillRatios(
                    healFillRatio,
                    adaptiveHealFillFallback,
                    healFillRatioFloor,
                    healFallbackSteps)
                : new List<float>
                {
                    maskArea > 0
                        ? Mathf.Clamp01((float)Mathf.Max(1, CountArrowTiles(candidate)) / maskArea)
                        : 1f
                };
            var attemptReports = new List<string>(Mathf.Max(1, fillRatios.Count));

            for (int attempt = 0; attempt < fillRatios.Count; attempt++)
            {
                float ratio = fillRatios[attempt];
                var attemptBoard = CloneBoard(candidate);
                int attemptTargetFill = enableFillHeal
                    ? ComputeTargetFillFromMask(maskCanSpawn, ratio)
                    : CountArrowTiles(attemptBoard);
                string attemptDetails = $"targetFillRatio={ratio:0.00}(target={attemptTargetFill}/{maskArea})";

                if (attemptBoard == null)
                {
                    attemptReports.Add($"{attemptDetails} | CloneFailed");
                    continue;
                }

                if (!enableFillHeal)
                {
                    attemptDetails = $"{attemptDetails} | shape-heal: skipped({outputMode})";
                }
                else if (!TryHealMaskedShape(ref attemptBoard, maskCanSpawn, greedyMoves, ratio, out string healDetails))
                {
                    attemptDetails = $"{attemptDetails} | {(!string.IsNullOrWhiteSpace(healDetails) ? healDetails : "ShapeHealNoGain")}";
                }
                else
                {
                    attemptDetails = $"{attemptDetails} | {(!string.IsNullOrWhiteSpace(healDetails) ? healDetails : "ShapeHeal")}";
                }

                int arrowsAfterHeal = CountArrowTiles(attemptBoard);
                if (arrowsAfterHeal == 0)
                {
                    attemptReports.Add($"{attemptDetails} | NoArrowsAfterShapeHeal");
                    continue;
                }

                int preRepairArrowCount = arrowsAfterHeal;
                int targetFill = enableFillHeal
                    ? ComputeTargetFillFromMask(maskCanSpawn, ratio)
                    : preRepairArrowCount;
                if (targetFill <= 0)
                {
                    attemptReports.Add($"{attemptDetails} | InvalidTargetFill");
                    continue;
                }

                int minKeepArrowsHard = Mathf.CeilToInt(targetFill * minPostGreedyFillRatio);
                float softFloor = Mathf.Min(kGreedyRepairKeepFloorRatio, minPostGreedyFillRatio);
                float emergencyFloor = Mathf.Min(kGreedyRepairEmergencyFloorRatio, softFloor);
                int minKeepArrowsSoftHard = Mathf.CeilToInt(targetFill * softFloor);
                int minKeepArrowsEmergencyHard = Mathf.CeilToInt(targetFill * emergencyFloor);

                int minKeepArrows = Mathf.Min(minKeepArrowsHard, preRepairArrowCount);
                int minKeepArrowsSoft = Mathf.Min(minKeepArrowsSoftHard, preRepairArrowCount);
                int minKeepArrowsEmergency = Mathf.Min(minKeepArrowsEmergencyHard, preRepairArrowCount);

                minKeepArrows = Mathf.Max(1, minKeepArrows);
                minKeepArrowsSoft = Mathf.Max(1, minKeepArrowsSoft);
                minKeepArrowsEmergency = Mathf.Max(1, minKeepArrowsEmergency);
                bool usedGreedyTier = false;
                int greedyTrimCap = Mathf.Max(1, greedyTrimPerChainOverride > 0 ? greedyTrimPerChainOverride : kGreedyTrimPerChain);

                if (minKeepArrows < minKeepArrowsHard)
                {
                    attemptDetails = $"{attemptDetails} | adjustedKeep={minKeepArrows}/{minKeepArrowsHard}";
                }
                if (minKeepArrowsSoft < minKeepArrowsHard)
                {
                    attemptDetails = $"{attemptDetails} | softKeep={minKeepArrowsSoft}/{minKeepArrowsHard}";
                }
                if (minKeepArrowsEmergency < minKeepArrowsSoft)
                {
                    attemptDetails = $"{attemptDetails} | emergencyKeep={minKeepArrowsEmergency}/{minKeepArrowsSoft}";
                }

                string repairDetails = string.Empty;
                BoardState greedyBoard = null;
                int greedyKeepUsed = minKeepArrows;
                string greedyMode = "hard";
                bool usedCoreTakeoutRefill = false;

            bool TryGreedyByKeep(
                int keepArrows,
                string mode,
                bool allowFullRemoval,
                bool allowFallbackChainClear,
                int trimLimit,
                out BoardState repairedBoard,
                out string modeDetails)
            {
                repairedBoard = CloneBoard(attemptBoard);
                if (repairedBoard == null)
                {
                    modeDetails = $"GreedyBoardCloneFailed({mode})";
                    return false;
                }

                bool ok = EnsureBoardGreedyFriendly(
                    ref repairedBoard,
                    greedyMoves,
                    maskCanSpawn,
                    keepArrows,
                    allowFullRemoval,
                    allowFallbackChainClear,
                    trimLimit,
                    _greedyRepairCandidateEvalCap,
                    _greedyRepairTimeBudgetMs,
                    out modeDetails);
                if (!ok)
                {
                    modeDetails = $"GreedyRepair({mode})Fail" + (string.IsNullOrWhiteSpace(modeDetails) ? string.Empty : $" | {modeDetails}");
                }

                return ok;
            }

                bool greedyOk = TryGreedyByKeep(
                    minKeepArrows,
                    "hard",
                    allowGreedyFullChainRemoval,
                    allowGreedyFallbackChainClear,
                    greedyTrimCap,
                    out greedyBoard,
                    out repairDetails);
                if (!greedyOk && minKeepArrowsSoft < minKeepArrows)
                {
                    string relaxedRepairDetails;
                    if (TryGreedyByKeep(
                        minKeepArrowsSoft,
                        "soft",
                        allowGreedyFullChainRemoval,
                        allowGreedyFallbackChainClear,
                        greedyTrimCap,
                        out BoardState fallbackGreedyBoard,
                        out relaxedRepairDetails))
                    {
                        greedyBoard = fallbackGreedyBoard;
                        usedGreedyTier = true;
                        greedyKeepUsed = minKeepArrowsSoft;
                        greedyMode = "soft";
                        greedyOk = true;
                        repairDetails = $"{repairDetails} | relaxedGreedyMinKeep={minKeepArrowsSoft}/{minKeepArrows} | {relaxedRepairDetails}";
                    }
                    else
                    {
                        repairDetails = string.IsNullOrWhiteSpace(repairDetails)
                            ? relaxedRepairDetails
                            : $"{repairDetails} | {relaxedRepairDetails}";
                    }
                }

                if (!greedyOk && minKeepArrowsEmergency < minKeepArrowsSoft)
                {
                    string emergencyRepairDetails;
                    if (TryGreedyByKeep(
                        minKeepArrowsEmergency,
                        "emergency",
                        allowGreedyFullChainRemoval,
                        allowGreedyFallbackChainClear,
                        greedyTrimCap,
                        out BoardState emergencyGreedyBoard,
                        out emergencyRepairDetails))
                    {
                        greedyBoard = emergencyGreedyBoard;
                        usedGreedyTier = true;
                        greedyKeepUsed = minKeepArrowsEmergency;
                        greedyMode = "emergency";
                        greedyOk = true;
                        repairDetails = $"{repairDetails} | emergencyGreedyMinKeep={minKeepArrowsEmergency}/{minKeepArrowsSoft} | {emergencyRepairDetails}";
                    }
                    else
                    {
                        repairDetails = string.IsNullOrWhiteSpace(repairDetails)
                            ? emergencyRepairDetails
                            : $"{repairDetails} | {emergencyRepairDetails}";
                    }
                }

                if (!greedyOk && minKeepArrows > 0)
                {
                    string restrictiveDetails = $"destructiveFallbackDisabled(minKeep={minKeepArrows}/{minKeepArrowsEmergency})";
                    repairDetails = string.IsNullOrWhiteSpace(repairDetails)
                        ? restrictiveDetails
                        : $"{repairDetails} | {restrictiveDetails}";

                    // If strict mode is enabled (both full and fallback clear disabled),
                    // try one constrained rescue pass with loop-break/trim + limited full-clear allowed.
                    if (!allowGreedyFullChainRemoval && !allowGreedyFallbackChainClear)
                    {
                        if (TryGreedyByKeep(
                            minKeepArrowsEmergency,
                            "destructive-rescue",
                            allowFullRemoval: true,
                            allowFallbackChainClear: true,
                            Mathf.Max(greedyTrimCap, kGreedyFallbackTrimPerChain),
                            out BoardState rescueGreedyBoard,
                            out string rescueDetails))
                        {
                            greedyBoard = rescueGreedyBoard;
                            usedGreedyTier = true;
                            greedyKeepUsed = minKeepArrowsEmergency;
                            greedyMode = "destructive-rescue";
                            greedyOk = true;
                            repairDetails = $"{repairDetails} | destructiveRescueEnabled | {rescueDetails}";
                        }
                        else
                        {
                            repairDetails = string.IsNullOrWhiteSpace(repairDetails)
                                ? restrictiveDetails
                                : $"{repairDetails} | {rescueDetails}";
                        }
                    }
                }

                if (!greedyOk && _allowGreedyCoreTakeoutRefillRescue)
                {
                    string coreRescueDetails;
                    if (TryGreedyCoreTakeoutRefillRescue(
                        attemptBoard,
                        maskCanSpawn,
                        greedyMoves,
                        targetFill,
                        Mathf.Max(1, Mathf.FloorToInt(targetFill * kGreedyCoreTakeoutMinAcceptRatio)),
                        out BoardState coreRescueBoard,
                        out coreRescueDetails))
                    {
                        greedyBoard = coreRescueBoard;
                        greedyKeepUsed = Mathf.Max(1, Mathf.FloorToInt(targetFill * kGreedyCoreTakeoutMinAcceptRatio));
                        greedyMode = "core-takeout-greedy";
                        usedGreedyTier = true;
                        usedCoreTakeoutRefill = true;
                        greedyOk = true;
                        repairDetails = string.IsNullOrWhiteSpace(repairDetails)
                            ? coreRescueDetails
                            : $"{repairDetails} | {coreRescueDetails}";
                    }
                    else if (!string.IsNullOrWhiteSpace(coreRescueDetails))
                    {
                        repairDetails = string.IsNullOrWhiteSpace(repairDetails)
                            ? coreRescueDetails
                            : $"{repairDetails} | {coreRescueDetails}";
                    }
                }

                if (usedGreedyTier && greedyBoard != null)
                {
                    repairDetails = $"{repairDetails} | greedyTier={greedyMode}:{greedyKeepUsed}";
                }

                if (!greedyOk)
                {
                    attemptReports.Add($"{attemptDetails} | GreedyRepairFail{(!string.IsNullOrWhiteSpace(repairDetails) ? $" | {repairDetails}" : string.Empty)}");
                    continue;
                }

                attemptBoard = greedyBoard;

                if (usedCoreTakeoutRefill)
                {
                    int refillTarget = Mathf.Max(targetFill, ComputeTargetFillFromMask(maskCanSpawn, kGreedySafeRefillTargetRatio));
                    if (TryGreedySafePostCoreRefill(ref attemptBoard, maskCanSpawn, greedyMoves, refillTarget, out string refillDetails))
                    {
                        repairDetails = string.IsNullOrWhiteSpace(repairDetails)
                            ? refillDetails
                            : $"{repairDetails} | {refillDetails}";
                    }
                    else if (!string.IsNullOrWhiteSpace(refillDetails))
                    {
                        repairDetails = string.IsNullOrWhiteSpace(repairDetails)
                            ? refillDetails
                            : $"{repairDetails} | {refillDetails}";
                    }
                }

                int arrowCountAfterGreedy = CountArrowTiles(attemptBoard);
                if (greedyKeepUsed > 0 && arrowCountAfterGreedy < greedyKeepUsed)
                {
                    float finalRatio = targetFill > 0 ? (float)arrowCountAfterGreedy / targetFill : 0f;
                    attemptReports.Add($"{attemptDetails} | GreedyRepairTooDestructive final={arrowCountAfterGreedy} target={targetFill} minKeep={greedyKeepUsed} ratio={finalRatio:0.00}");
                    continue;
                }
                if (targetFill > 0)
                {
                    float finalFillRatio = (float)arrowCountAfterGreedy / targetFill;
                    float effectiveFloor = usedCoreTakeoutRefill
                        ? Mathf.Min(minPostGreedyFillRatio, kGreedyCoreTakeoutMinAcceptRatio)
                        : minPostGreedyFillRatio;
                    if (finalFillRatio < effectiveFloor)
                    {
                        attemptReports.Add($"{attemptDetails} | FinalFillBelowPostGreedyFloor final={arrowCountAfterGreedy} target={targetFill} ratio={finalFillRatio:0.00} floor={effectiveFloor:0.00}");
                        continue;
                    }
                }

                string validationDetails = string.Empty;
                if (!ValidateGeneratedBoard(ref attemptBoard, greedyMoves, out validationDetails))
                {
                    attemptReports.Add($"{attemptDetails} | ValidationFail{(!string.IsNullOrWhiteSpace(validationDetails) ? $" | {validationDetails}" : string.Empty)}");
                    continue;
                }

                int arrowCount = CountArrowTiles(attemptBoard);
                if (arrowCount == 0)
                {
                    attemptReports.Add($"{attemptDetails} | finalEmpty");
                    continue;
                }

                generatedArrowCount = arrowCount;
                result = attemptBoard;
                float finalMaskFillRatio = maskArea > 0 ? (float)arrowCount / maskArea : 0f;
                details = $"Offset={offset.x},{offset.y} | Mode={outputMode} | PreservedTiles={preservedChainCount} | Arrows={arrowCount} | TargetFill={targetFill} | MaskFill={arrowCount}/{maskArea}({finalMaskFillRatio:0.00}) | PreRepairArrows={preRepairArrowCount} | {attemptDetails} | {repairDetails} | {validationDetails} | finalFill={arrowCount}";
                return true;
            }

            float failedMaskFillRatio = maskArea > 0 ? (float)arrowsAfterClamp / maskArea : 0f;
            details = $"Offset={offset.x},{offset.y} | Mode={outputMode} | PreservedTiles={preservedChainCount} | ArrowsBeforeClamp={arrowsBeforeClamp} | ArrowsAfterClamp={arrowsAfterClamp} | MaskFillBeforeRepair={arrowsAfterClamp}/{maskArea}({failedMaskFillRatio:0.00}) | AdaptiveFillAttempts=[{string.Join("; ", attemptReports)}]";
            return false;
        }

        private static bool ApplyPreGreedyGeometryFix(
            ref BoardState board,
            bool[] maskCanSpawn,
            int maxPasses,
            int trimPerChainLimit,
            out int removedChains,
            out string details)
        {
            removedChains = 0;
            details = "pre-geometry: already clean";

            if (board == null || board.tiles == null)
            {
                details = "pre-geometry: invalid board";
                return false;
            }

            if (maxPasses <= 0)
            {
                details = "pre-geometry: skipped";
                return true;
            }

            if (trimPerChainLimit <= 0)
            {
                trimPerChainLimit = 1;
            }

            if (maskCanSpawn != null && maskCanSpawn.Length != board.width * board.height)
            {
                maskCanSpawn = null;
            }

            int cellBudget = Mathf.Max(1, board.width * board.height);
            var visited = new HashSet<int>(cellBudget);
            var chainSet = new HashSet<int>(cellBudget);
            var orderedTmp = new List<Vector2Int>(16);
            var chains = new List<int[]>();
            var chainFlags = new List<bool>();
            var orderedIndices = new List<int>(16);
            var reversed = new List<int>(16);

            int totalRemoved = 0;
            int totalReversals = 0;
            int totalTrims = 0;
            int passes = 0;
            int lastConflict = 0;
            int lastSelfConflict = 0;
            int lastOppositeConflict = 0;

            for (passes = 0; passes < maxPasses; passes++)
            {
                int currentConflicts = CountGeometryConflicts(board, out lastSelfConflict, out lastOppositeConflict);
                lastConflict = currentConflicts;

                int currentBoundary = ComputeMaskBoundaryArrowScore(board, maskCanSpawn);

                if (currentConflicts <= 0)
                {
                    details = $"pre-geometry: passes={passes}, removed={totalRemoved}, reversed={totalReversals}, trimmed={totalTrims}, finalConflicts=0(self={lastSelfConflict},rowcol={lastOppositeConflict})";
                    return true;
                }

                if (!CollectMaskedChains(board, chains, chainFlags, visited, chainSet, orderedTmp) || chains.Count == 0)
                {
                    break;
                }

                BoardState bestBoard = null;
                int bestRemoved = int.MaxValue;
                int bestConflicts = currentConflicts;
                int bestBoundary = currentBoundary;
                int bestTrim = 0;
                bool bestTrimHead = true;
                bool bestReverse = false;

                int chainTrimLimit = Mathf.Max(1, trimPerChainLimit);

                for (int ci = 0; ci < chains.Count; ci++)
                {
                    int[] chain = chains[ci];
                    if (chain == null || chain.Length < 2)
                    {
                        continue;
                    }

                    bool isLoop = ci < chainFlags.Count && chainFlags[ci];
                    chainSet.Clear();

                    for (int i = 0; i < chain.Length; i++)
                    {
                        int idx = chain[i];
                        if (idx >= 0 && idx < board.tiles.Length)
                        {
                            chainSet.Add(idx);
                        }
                    }

                    List<Vector2Int> ordered = null;
                    if (!TryBuildStableOrderedChain(
                        board,
                        chainSet,
                        new Vector2Int(chain[0] % board.width, chain[0] / board.width),
                        out ordered))
                    {
                        continue;
                    }

                    orderedIndices.Clear();
                    for (int i = 0; i < ordered.Count; i++)
                    {
                        Vector2Int p = ordered[i];
                        if (!board.InBounds(p.x, p.y))
                        {
                            continue;
                        }

                        orderedIndices.Add(board.Index(p.x, p.y));
                    }

                    if (orderedIndices.Count < 2)
                    {
                        continue;
                    }

                    int[] orderedChain = orderedIndices.ToArray();
                    bool headHasConflict = HasSelfBlockedHead(board, chainSet, ordered);
                    bool needReverse = headHasConflict || lastOppositeConflict > 0;

                    if (needReverse)
                    {
                        reversed.Clear();
                        for (int i = orderedIndices.Count - 1; i >= 0; i--)
                        {
                            reversed.Add(orderedIndices[i]);
                        }

                        BoardState reverseBoard = CloneBoard(board);
                        if (reverseBoard != null && TryApplyOrderedChainToCandidate(reverseBoard, reversed))
                        {
                            int reverseConflicts = CountGeometryConflicts(reverseBoard, out _, out _);
                            int reverseBoundary = ComputeMaskBoundaryArrowScore(reverseBoard, maskCanSpawn);
                            if (reverseConflicts < bestConflicts || (reverseConflicts == bestConflicts && reverseBoundary > bestBoundary))
                            {
                                bestBoard = reverseBoard;
                                bestRemoved = 0;
                                bestConflicts = reverseConflicts;
                                bestBoundary = reverseBoundary;
                                bestReverse = true;
                            }
                        }
                    }

                    if (isLoop)
                    {
                        continue;
                    }

                    int maxTrim = Mathf.Min(chainTrimLimit, orderedIndices.Count - 2);
                    for (int trim = 1; trim <= maxTrim; trim++)
                    {
                        int trimRemoved;
                        BoardState headCandidate;
                        if (TryBuildGreedyRepairCandidate(
                            board,
                            orderedChain,
                            false,
                            trim,
                            0,
                            out headCandidate,
                            out trimRemoved) && headCandidate != null)
                        {
                            int candidateConflict = CountGeometryConflicts(headCandidate, out _, out _);
                            int candidateBoundary = ComputeMaskBoundaryArrowScore(headCandidate, maskCanSpawn);
                            if (candidateConflict < bestConflicts
                                || (candidateConflict == bestConflicts && trimRemoved < bestRemoved)
                                || (candidateConflict == bestConflicts && trimRemoved == bestRemoved && candidateBoundary > bestBoundary))
                            {
                                bestBoard = headCandidate;
                                bestRemoved = trimRemoved;
                                bestConflicts = candidateConflict;
                                bestBoundary = candidateBoundary;
                                bestTrim = trim;
                                bestTrimHead = true;
                                bestReverse = false;
                            }
                        }

                        BoardState tailCandidate;
                        if (TryBuildGreedyRepairCandidate(
                            board,
                            orderedChain,
                            false,
                            0,
                            trim,
                            out tailCandidate,
                            out trimRemoved) && tailCandidate != null)
                        {
                            int candidateConflict = CountGeometryConflicts(tailCandidate, out _, out _);
                            int candidateBoundary = ComputeMaskBoundaryArrowScore(tailCandidate, maskCanSpawn);
                            if (candidateConflict < bestConflicts
                                || (candidateConflict == bestConflicts && trimRemoved < bestRemoved)
                                || (candidateConflict == bestConflicts && trimRemoved == bestRemoved && candidateBoundary > bestBoundary))
                            {
                                bestBoard = tailCandidate;
                                bestRemoved = trimRemoved;
                                bestConflicts = candidateConflict;
                                bestBoundary = candidateBoundary;
                                bestTrim = trim;
                                bestTrimHead = false;
                                bestReverse = false;
                            }
                        }
                    }
                }

                if (bestBoard == null)
                {
                    break;
                }

                if (bestConflicts > lastConflict)
                {
                    break;
                }

                board = bestBoard;
                removedChains++;

                if (bestRemoved > 0)
                {
                    totalRemoved += bestRemoved;
                    totalTrims += bestRemoved;
                }
                else if (bestReverse)
                {
                    totalReversals++;
                }

                if (bestTrimHead)
                {
                    _ = bestTrim;
                }
            }

            details = $"pre-geometry: passes={passes}, removed={totalRemoved}, reversed={totalReversals}, trimmed={totalTrims}, finalConflicts={lastConflict}(self={lastSelfConflict},rowcol={lastOppositeConflict})";
            return removedChains > 0 || (lastConflict <= 0);
        }

        private static int CountGeometryConflicts(BoardState board, out int selfBlockedHeadCount, out int oppositeRayConflicts)
        {
            selfBlockedHeadCount = 0;
            oppositeRayConflicts = 0;
            if (board == null || board.tiles == null)
                return 0;

            var visited = new HashSet<int>(Mathf.Max(1, board.width * board.height));
            var chainSet = new HashSet<int>(Mathf.Max(1, board.width * board.height));
            var ordered = new List<Vector2Int>(16);
            var chains = new List<int[]>();
            var chainFlags = new List<bool>();

            if (!CollectMaskedChains(board, chains, chainFlags, visited, chainSet, ordered) || chains.Count == 0)
                return 0;

            for (int i = 0; i < chains.Count; i++)
            {
                int[] chain = chains[i];
                if (chain == null || chain.Length < 2)
                    continue;

                chainSet.Clear();
                for (int j = 0; j < chain.Length; j++)
                {
                    int idx = chain[j];
                    if (idx >= 0 && idx < board.tiles.Length)
                        chainSet.Add(idx);
                }

                if (!TryBuildStableOrderedChain(
                    board,
                    chainSet,
                    new Vector2Int(chain[0] % board.width, chain[0] / board.width),
                    out var orderedPos))
                {
                    continue;
                }

                if (orderedPos == null || orderedPos.Count < 2)
                    continue;

                var orderedIdxs = new List<int>(orderedPos.Count);
                for (int p = 0; p < orderedPos.Count; p++)
                {
                    Vector2Int pos = orderedPos[p];
                    if (board.InBounds(pos.x, pos.y))
                        orderedIdxs.Add(board.Index(pos.x, pos.y));
                }

                if (orderedIdxs.Count >= 2 && HasSelfBlockedHead(board, chainSet, orderedPos))
                {
                    selfBlockedHeadCount++;
                }
            }

            oppositeRayConflicts = CountOppositeRayConflicts(board);
            return selfBlockedHeadCount + oppositeRayConflicts;
        }

        private static bool IsValidOrderedIndices(BoardState board, List<int> orderedIdxs, out int headIdx)
        {
            headIdx = -1;
            if (board == null || board.tiles == null || orderedIdxs == null || orderedIdxs.Count < 2)
                return false;

            headIdx = orderedIdxs[0];
            if (headIdx < 0 || headIdx >= board.tiles.Length)
                return false;

            return board.tiles[headIdx].type == TileType.Arrow;
        }

        private static int CountChainGeometryConflicts(
            BoardState board,
            HashSet<int> chainSet,
            List<int> orderedIdxs,
            bool[] maskCanSpawn)
        {
            if (board == null || board.tiles == null || chainSet == null || chainSet.Count == 0)
                return 0;

            int conflicts = 0;
            if (orderedIdxs != null && orderedIdxs.Count >= 2)
            {
                if (!IsValidOrderedIndices(board, orderedIdxs, out int headIdx))
                    return 0;

                if (HasSelfBlockedHead(board, chainSet, orderedIdxs, maskCanSpawn))
                    conflicts++;
                if (HasOppositeRayHitConflict(board, headIdx % board.width, headIdx / board.width, board.tiles[headIdx].arrow.outDir))
                    conflicts++;
            }

            return conflicts;
        }

        private static int CountOppositeRayConflicts(BoardState board)
        {
            if (board == null || board.tiles == null)
                return 0;

            int conflicts = 0;
            for (int y = 0; y < board.height; y++)
            {
                for (int x = 0; x < board.width; x++)
                {
                    int startIdx = board.Index(x, y);
                    TileState start = board.tiles[startIdx];
                    if (start.type != TileType.Arrow)
                        continue;

                    if (!HasOppositeRayHitConflict(board, x, y, start.arrow.outDir))
                        continue;

                    conflicts++;
                }
            }

            return conflicts;
        }

        private static bool HasOppositeRayHitConflict(BoardState board, int startX, int startY, Dir outDir)
        {
            if (board == null || board.tiles == null)
                return false;

            Vector2Int dir = DirToOffsetSafe(outDir);
            int x = startX + dir.x;
            int y = startY + dir.y;

            while (board.InBounds(x, y))
            {
                int idx = board.Index(x, y);
                TileState t = board.tiles[idx];
                if (t.type == TileType.Empty)
                {
                    x += dir.x;
                    y += dir.y;
                    continue;
                }

                if (t.type == TileType.Block)
                    return false;

                if (t.type == TileType.Arrow)
                {
                    return t.arrow.outDir == Opposite(outDir);
                }

                return false;
            }

            return false;
        }

        private static bool HasSelfBlockedHead(
            BoardState board,
            HashSet<int> chainSet,
            List<Vector2Int> ordered)
        {
            if (board == null || board.tiles == null || chainSet == null || ordered == null || ordered.Count < 2)
                return false;

            Vector2Int head = ordered[0];
            Vector2Int second = ordered[1];
            int headIdx = board.Index(head.x, head.y);
            int secondIdx = board.Index(second.x, second.y);
            return HasHeadForwardSelfBodyConflict(board, chainSet, headIdx, secondIdx);
        }

        private static bool HasSelfBlockedHead(
            BoardState board,
            HashSet<int> chainSet,
            List<int> orderedIdxs,
            bool[] maskCanSpawn)
        {
            if (board == null || board.tiles == null || chainSet == null || orderedIdxs == null || orderedIdxs.Count < 2)
                return false;

            int headIdx = orderedIdxs[0];
            if (!chainSet.Contains(headIdx))
                return false;

            if (maskCanSpawn != null && maskCanSpawn.Length == board.tiles.Length && headIdx >= 0 && headIdx < maskCanSpawn.Length && !maskCanSpawn[headIdx])
                return false;

            int secondIdx = orderedIdxs[1];
            return HasHeadForwardSelfBodyConflict(board, chainSet, headIdx, secondIdx);
        }

        private static bool HasHeadForwardSelfBodyConflict(
            BoardState board,
            HashSet<int> chainSet,
            int headIdx,
            int expectedSecondIdx)
        {
            if (board == null || board.tiles == null || chainSet == null || chainSet.Count == 0)
                return false;

            if (!board.InBounds(headIdx % board.width, headIdx / board.width))
                return false;

            var head = board.tiles[headIdx];
            if (head.type != TileType.Arrow)
                return false;

            Vector2Int step = DirToOffsetSafe(head.arrow.outDir);
            if (step.x == 0 && step.y == 0)
                return false;

            int x = (headIdx % board.width) + step.x;
            int y = (headIdx / board.width) + step.y;
            bool hitExpectedSecond = false;

            while (board.InBounds(x, y))
            {
                int idx = board.Index(x, y);
                if (idx < 0 || idx >= board.tiles.Length)
                    break;

                TileState t = board.tiles[idx];
                if (t.type == TileType.Empty)
                {
                    x += step.x;
                    y += step.y;
                    continue;
                }

                if (t.type == TileType.Block)
                    break;

                if (idx == expectedSecondIdx && !hitExpectedSecond)
                {
                    hitExpectedSecond = true;
                    x += step.x;
                    y += step.y;
                    continue;
                }

                if (chainSet.Contains(idx))
                    return true;

                return false;
            }

            return false;
        }

        private static Vector2Int DirToOffsetSafe(Dir dir)
        {
            switch (dir)
            {
                case Dir.Up:
                    return Vector2Int.up;
                case Dir.Right:
                    return Vector2Int.right;
                case Dir.Down:
                    return Vector2Int.down;
                case Dir.Left:
                    return Vector2Int.left;
                default:
                    return Vector2Int.up;
            }
        }

        private static bool TryApplyOrderedChainToCandidate(BoardState candidate, List<int> orderedIdxs)
        {
            if (candidate == null || orderedIdxs == null || orderedIdxs.Count < 2)
                return false;

            for (int i = 0; i < orderedIdxs.Count; i++)
            {
                int idx = orderedIdxs[i];
                if (idx < 0 || idx >= candidate.tiles.Length)
                    return false;
                if (candidate.tiles[idx].type != TileType.Arrow)
                    return false;
            }

            return ApplyOrderedIndexChain(candidate, orderedIdxs);
        }

        private static int ComputeMaskBoundaryArrowScore(BoardState board, bool[] maskCanSpawn)
        {
            if (board == null || board.tiles == null || board.width <= 0 || board.height <= 0)
                return 0;

            if (maskCanSpawn == null || maskCanSpawn.Length != board.tiles.Length)
                return 0;

            int w = board.width;
            int h = board.height;
            int score = 0;
            for (int i = 0; i < board.tiles.Length; i++)
            {
                if (board.tiles[i].type != TileType.Arrow || !maskCanSpawn[i])
                    continue;

                int x = i % w;
                int y = i / w;
                bool isBoundary = x == 0 || x == w - 1 || y == 0 || y == h - 1;
                if (!isBoundary && x > 0 && maskCanSpawn[i - 1] && x < w - 1 && maskCanSpawn[i + 1] && y > 0 && maskCanSpawn[i - w] && y < h - 1 && maskCanSpawn[i + w])
                {
                    isBoundary = false;
                }
                else
                {
                    bool leftInside = x > 0 && maskCanSpawn[i - 1];
                    bool rightInside = x < w - 1 && maskCanSpawn[i + 1];
                    bool upInside = y < h - 1 && maskCanSpawn[i + w];
                    bool downInside = y > 0 && maskCanSpawn[i - w];

                    if (!leftInside || !rightInside || !upInside || !downInside)
                    {
                        isBoundary = true;
                    }
                }

                if (isBoundary)
                    score += 1;
            }

            return score;
        }

        private static bool BuildMaskedBoardFromSeed(
            BoardState sourceBoard,
            bool[] maskCanSpawn,
            Vector2Int offset,
            int targetW,
            int targetH,
            out BoardState targetBoard,
            out int preservedTileCount)
        {
            targetBoard = new BoardState(targetW, targetH);
            for (int i = 0; i < targetBoard.tiles.Length; i++)
                targetBoard.tiles[i] = TileState.Empty();

            preservedTileCount = 0;

            int sourceW = sourceBoard.width;
            int sourceH = sourceBoard.height;
            var visited = new HashSet<int>(sourceW * sourceH);
            var chainSet = new HashSet<int>();

            for (int sy = 0; sy < sourceH; sy++)
            {
                for (int sx = 0; sx < sourceW; sx++)
                {
                    int sourceIdx = sx + sy * sourceW;
                    if (visited.Contains(sourceIdx))
                        continue;

                    if (sourceBoard.tiles[sourceIdx].type != TileType.Arrow)
                        continue;

                    chainSet.Clear();
                    ArrowChainUtility.CollectFullChain(sourceBoard, new Vector2Int(sx, sy), 0, chainSet);
                    if (chainSet.Count == 0)
                        continue;

                    foreach (int chainIdx in chainSet)
                        visited.Add(chainIdx);

                    foreach (int chainIdx in chainSet)
                    {
                        int cx = chainIdx % sourceW;
                        int cy = chainIdx / sourceW;
                        int tx = cx + offset.x;
                        int ty = cy + offset.y;

                        if (tx < 0 || tx >= targetW || ty < 0 || ty >= targetH)
                            continue;

                        int targetIdx = tx + ty * targetW;
                        if (!maskCanSpawn[targetIdx])
                            continue;

                        targetBoard.tiles[targetIdx] = sourceBoard.tiles[chainIdx];
                        preservedTileCount++;
                    }
                }
            }

            return preservedTileCount > 0;
        }

        private static bool TryHealMaskedShape(ref BoardState board, bool[] maskCanSpawn, int greedyMoves, float targetFillRatio, out string details)
        {
            details = "shape-heal: no action";

            if (board == null || board.tiles == null || maskCanSpawn == null || maskCanSpawn.Length != board.width * board.height)
                return false;

            int totalMaskCells = 0;
            for (int i = 0; i < maskCanSpawn.Length; i++)
            {
                if (maskCanSpawn[i])
                    totalMaskCells++;
            }

            if (totalMaskCells <= 0)
            {
                details = "shape-heal: invalid mask.";
                return false;
            }

            int currentArrowCount = CountArrowTiles(board);
            int targetFill = ComputeTargetFillFromMask(maskCanSpawn, targetFillRatio);
            if (targetFill <= 0)
            {
                details = "shape-heal: invalid target fill.";
                return false;
            }
            if (currentArrowCount >= targetFill)
            {
                details = $"shape-heal: current={currentArrowCount} target={targetFill} no fill gap";
                return false;
            }

            var orderedChains = new List<int[]>(8);
            var isLoopChain = new List<bool>(8);
            var orderedTemp = new List<Vector2Int>(16);
            var visited = new HashSet<int>();
            var chainSet = new HashSet<int>();
            var extensionCandidates = new List<int[]>();

            int totalAdded = 0;
            int extensionAdded = 0;
            int microAdded = 0;
            int passes = 0;
            int gap = targetFill - currentArrowCount;
            int maxPasses = Mathf.Min(kHealMaxPasses, gap);

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            while (passes < maxPasses && currentArrowCount < targetFill)
            {
                currentArrowCount = CountArrowTiles(board);
                if (currentArrowCount >= targetFill)
                    break;

                if (!CollectOrderedChainsForHealing(
                    board,
                    orderedChains,
                    isLoopChain,
                    visited,
                    chainSet,
                    orderedTemp))
                {
                    break;
                }

                if (orderedChains.Count == 0)
                    break;

                BoardState bestBoard = null;
                int bestGain = 0;
                int bestScore = int.MinValue;
                int quickBudget = Math.Max(1, greedyMoves / 4);

                for (int ci = 0; ci < orderedChains.Count; ci++)
                {
                    if (isLoopChain[ci])
                        continue;

                    int[] ordered = orderedChains[ci];
                    if (ordered == null || ordered.Length < 2)
                        continue;

                    int startIdx = ordered[0];
                    int secondIdx = ordered[1];
                    int endIdx = ordered[ordered.Length - 1];
                    int penultimateIdx = ordered[ordered.Length - 2];

                    extensionCandidates.Clear();
                    CollectEndpointHealExtensions(board, startIdx, secondIdx, maskCanSpawn, extensionCandidates);
                    CollectEndpointHealExtensions(board, endIdx, penultimateIdx, maskCanSpawn, extensionCandidates);

                    for (int e = 0; e < extensionCandidates.Count; e++)
                    {
                        int[] ext = extensionCandidates[e];
                        if (ext == null || ext.Length == 0)
                            continue;

                        int[] candidateChain = new int[ordered.Length + ext.Length];
                        Array.Copy(ordered, 0, candidateChain, 0, ordered.Length);
                        Array.Copy(ext, 0, candidateChain, ordered.Length, ext.Length);

                        var candidate = CloneBoard(board);
                        if (!ApplyOrderedIndexChain(candidate, candidateChain))
                            continue;

                        if (!ValidateArrowDegreeLimit(candidate))
                            continue;

                    int newCount = CountArrowTiles(candidate);
                    int gain = newCount - currentArrowCount;
                    int remain = targetFill - currentArrowCount;
                    if (gain <= 0 || gain > remain)
                        continue;

                        int score = gain * 100 + ScoreChainDirectionCompatibility(candidate, candidateChain);
                        if (GreedyValidator.TryClearAllByGreedy(candidate, rules, quickBudget, out _))
                            score += 300;

                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestGain = gain;
                            bestBoard = candidate;
                        }
                    }
                }

                if (bestBoard == null || bestGain <= 0)
                {
                    int beforeMicroCount = CountArrowTiles(board);
                    int microGain = TryMicroFillMaskedShape(ref board, maskCanSpawn, targetFill, greedyMoves, out string microDetails);
                    if (microGain <= 0)
                        break;

                    int afterMicroCount = CountArrowTiles(board);
                    int actualMicroGain = afterMicroCount - beforeMicroCount;
                    if (actualMicroGain > 0)
                    {
                        microAdded += actualMicroGain;
                        totalAdded += actualMicroGain;
                    }
                    currentArrowCount = afterMicroCount;
                    passes++;
                    details = string.IsNullOrWhiteSpace(microDetails) ? details : $"{details} | {microDetails}";
                    continue;
                }

                int beforeCount = currentArrowCount;
                board = bestBoard;
                int afterCount = CountArrowTiles(board);
                int actualGain = afterCount - beforeCount;
                if (actualGain > 0)
                {
                    extensionAdded += actualGain;
                    totalAdded += actualGain;
                }

                currentArrowCount = afterCount;
                passes++;
            }

            if (currentArrowCount > targetFill)
            {
                int overflow = currentArrowCount - targetFill;
                int trimmedArrows;
                TrimExcessChainsToTargetFill(ref board, maskCanSpawn, targetFill, out trimmedArrows);
                currentArrowCount = CountArrowTiles(board);
                details = string.IsNullOrWhiteSpace(details)
                    ? $"shape-heal-trim: overflow={overflow} | trimmed={trimmedArrows}"
                    : $"{details} | shape-heal-trim: overflow={overflow} | trimmed={trimmedArrows}";
            }

            details = $"shape-heal: target={targetFill}/{totalMaskCells} added={totalAdded} extension={extensionAdded} micro={microAdded} passes={passes} preFill={currentArrowCount}";
            return totalAdded > 0;
        }

        private static int ComputeTargetFillFromMask(bool[] maskCanSpawn, float fillRatio = kHealTargetFillRatio)
        {
            if (maskCanSpawn == null || maskCanSpawn.Length == 0)
                return 0;

            int totalMaskCells = 0;
            for (int i = 0; i < maskCanSpawn.Length; i++)
                if (maskCanSpawn[i])
                    totalMaskCells++;

            if (totalMaskCells <= 0)
                return 0;

            float ratio = Mathf.Clamp01(fillRatio);
            return Mathf.Clamp(
                Mathf.CeilToInt(totalMaskCells * ratio),
                2,
                totalMaskCells);
        }

        private static int TrimExcessChainsToTargetFill(
            ref BoardState board,
            bool[] maskCanSpawn,
            int targetFill,
            out int trimmedArrows)
        {
            trimmedArrows = 0;
            if (board == null || board.tiles == null || targetFill < 0)
                return 0;
            if (maskCanSpawn == null || maskCanSpawn.Length != board.width * board.height)
                return 0;

            int current = CountArrowTiles(board);
            if (current <= targetFill)
                return 0;

            var chains = new List<int[]>(Mathf.Max(1, board.width * board.height));
            var chainFlags = new List<bool>(Mathf.Max(1, board.width * board.height));
            var visited = new HashSet<int>(Mathf.Max(1, board.width * board.height));
            var chainSet = new HashSet<int>(Mathf.Max(1, board.width * board.height));
            var ordered = new List<Vector2Int>(16);

            int guard = board.width * board.height * 4;
            while (current > targetFill && guard-- > 0)
            {
                if (!CollectMaskedChains(board, chains, chainFlags, visited, chainSet, ordered) || chains.Count == 0)
                    break;

                int removeTarget = current - targetFill;
                int bestIndex = -1;
                int bestLen = int.MaxValue;
                int fallbackIndex = -1;
                int fallbackLen = int.MaxValue;

                for (int i = 0; i < chains.Count; i++)
                {
                    int[] chain = chains[i];
                    if (chain == null || chain.Length <= 0)
                        continue;

                    bool insideMask = true;
                    for (int j = 0; j < chain.Length; j++)
                    {
                        int idx = chain[j];
                        if (idx < 0 || idx >= maskCanSpawn.Length || !maskCanSpawn[idx])
                        {
                            insideMask = false;
                            break;
                        }
                    }

                    if (!insideMask)
                        continue;

                    int len = chain.Length;
                    if (len <= removeTarget && len < bestLen)
                    {
                        bestLen = len;
                        bestIndex = i;
                    }

                    if (len < fallbackLen)
                    {
                        fallbackLen = len;
                        fallbackIndex = i;
                    }
                }

                int chosenIndex = bestIndex >= 0 ? bestIndex : fallbackIndex;
                if (chosenIndex < 0)
                    break;

                int[] chainToClear = chains[chosenIndex];
                if (chainToClear == null || chainToClear.Length <= 0)
                    break;

                for (int i = 0; i < chainToClear.Length; i++)
                    board.tiles[chainToClear[i]] = TileState.Empty();

                trimmedArrows += chainToClear.Length;
                current -= chainToClear.Length;
            }

            return trimmedArrows;
        }

        private static int TryMicroFillMaskedShape(
            ref BoardState board,
            bool[] maskCanSpawn,
            int targetFill,
            int greedyMoves,
            out string details)
        {
            details = "shape-heal-micro: no action";

            if (board == null || board.tiles == null || maskCanSpawn == null || maskCanSpawn.Length != board.width * board.height)
                return 0;

            int current = CountArrowTiles(board);
            if (targetFill <= 0)
                return 0;
            if (current > targetFill)
                current = targetFill;
            if (current >= targetFill)
            {
                details = $"shape-heal-micro: current={current} target={targetFill} no gap";
                return 0;
            }

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int quickBudget = Math.Max(1, greedyMoves / 4);
            var microCandidates = new List<int[]>();
            int addedTotal = 0;
            int passes = 0;

            while (passes < kMicroFillMaxPasses && current < targetFill)
            {
                current = CountArrowTiles(board);
                if (current >= targetFill)
                    break;

                int maxPathLen = Mathf.Clamp(
                    targetFill - current + 1,
                    2,
                    kMicroFillMaxPathLen);
                int needLeft = targetFill - current;
                BoardState bestBoard = null;
                int bestBoardCount = current;
                int bestGain = 0;
                int bestScore = int.MinValue;
                bool usedRewrite = false;
                bool hadRewriteCandidates = false;
                bool hadAnyCandidates = false;

                for (int mode = 0; mode < 2; mode++)
                {
                    bool allowRewrite = mode == 1;
                    microCandidates.Clear();
                    CollectMicroChainCandidates(board, maskCanSpawn, microCandidates, maxPathLen, allowRewrite);
                    if (microCandidates.Count == 0)
                    {
                        continue;
                    }

                    hadAnyCandidates = true;
                    if (allowRewrite)
                        hadRewriteCandidates = true;

                    for (int ci = 0; ci < microCandidates.Count; ci++)
                    {
                        int[] candidatePath = microCandidates[ci];
                        if (candidatePath == null || candidatePath.Length == 0 || candidatePath.Length > needLeft)
                            continue;

                        int rewrittenCells = CountRewrittenCells(board, candidatePath);
                        if (rewrittenCells > 0 && !allowRewrite)
                            continue;

                        int maxRewrite = Mathf.Max(1, Mathf.Min(5, candidatePath.Length - 1));
                        if (allowRewrite && rewrittenCells > maxRewrite)
                            continue;

                        var candidate = CloneBoard(board);
                        if (!ApplyOrderedIndexChain(candidate, candidatePath))
                            continue;

                        if (!ValidateArrowDegreeLimit(candidate))
                            continue;

                        bool greedyOk = GreedyValidator.TryClearAllByGreedy(candidate, rules, quickBudget, out _);

                        int newCount = CountArrowTiles(candidate);
                        int gain = newCount - current;
                        if (gain <= 0 || gain > needLeft || newCount > targetFill)
                            continue;

                        bool connected = false;
                        var pathSet = new HashSet<int>(candidatePath.Length);
                        for (int i = 0; i < candidatePath.Length; i++)
                        {
                            pathSet.Add(candidatePath[i]);
                            if (PathTouchesExistingArrow(board, candidatePath[i], pathSet))
                            {
                                connected = true;
                                break;
                            }
                        }

                    int score = gain * 160;
                    if (greedyOk)
                        score += 200;
                    else
                        score -= 140;
                        if (connected)
                            score += 96;
                        if (allowRewrite)
                        {
                            score -= (rewrittenCells * kMicroRewritePenalty);
                            if (candidatePath.Length > 2)
                                score -= 20 * (candidatePath.Length - 2);
                        }
                        score += PathCompactness(candidate, candidatePath, maskCanSpawn);
                        if (gain == needLeft)
                            score += 400;

                    if (score <= bestScore)
                        continue;

                    bestScore = score;
                    bestGain = gain;
                    bestBoard = candidate;
                    bestBoardCount = newCount;
                    usedRewrite = allowRewrite;
                }

                    if (bestBoard != null)
                        break;
                }

                if (bestBoard == null)
                {
                    details = hadRewriteCandidates
                        ? "shape-heal-micro: no rewrite candidates"
                        : (hadAnyCandidates ? "shape-heal-micro: candidates rejected" : "shape-heal-micro: no candidates");
                    break;
                }

                if (bestGain <= 0)
                {
                    details = "shape-heal-micro: no feasible candidate";
                    break;
                }

                int beforeCount = current;
                board = bestBoard;
                current = bestBoardCount;
                int actualGain = current - beforeCount;
                addedTotal += Mathf.Max(0, actualGain);
                details = usedRewrite ? $"shape-heal-micro: used-overwrite gain={bestGain}" : "shape-heal-micro: add success";
                passes++;
            }

            details = $"shape-heal-micro: added={addedTotal} passes={passes}";
            return addedTotal;
        }

        private static bool TryGreedySafePostCoreRefill(
            ref BoardState board,
            bool[] maskCanSpawn,
            int greedyMoves,
            int targetFill,
            out string details)
        {
            details = "post-core-refill: no action";
            if (board == null || board.tiles == null || maskCanSpawn == null || maskCanSpawn.Length != board.width * board.height)
                return false;

            int current = CountArrowTiles(board);
            if (targetFill <= current)
            {
                details = $"post-core-refill: current={current} target={targetFill} no gap";
                return false;
            }

            var microCandidates = new List<int[]>();
            var ranked = new List<SafeRefillCandidate>();
            int addedTotal = 0;
            int committed = 0;
            int rejected = 0;
            int applyRejected = 0;
            int gainRejected = 0;
            int degreeWarnings = 0;
            int passes = 0;
            int boundaryBefore = CountEmptyMaskBoundaryCells(board, maskCanSpawn);
            int boundaryCommitted = 0;
            int boundarySwapCommitted = 0;
            int boundarySwapRejected = 0;
            int directExitCommitted = 0;
            int directExitRejected = 0;
            bool boundaryExhausted = boundaryBefore <= 0;
            BoardState bestBoard = CloneBoard(board);
            int bestFill = current;
            int bestBoundary = boundaryBefore;
            string lastReject = string.Empty;

            while (passes < kGreedySafeRefillMaxPasses)
            {
                current = CountArrowTiles(board);
                int gap = targetFill - current;
                if (gap < 2)
                    break;

                int maxPathLen = Mathf.Clamp(Mathf.Min(gap, kGreedySafeRefillMaxPathLen), 2, kGreedySafeRefillMaxPathLen);
                microCandidates.Clear();
                CollectMicroChainCandidates(board, maskCanSpawn, microCandidates, maxPathLen, allowRewrite: false);
                if (microCandidates.Count == 0)
                {
                    lastReject = "no candidates";
                    break;
                }

                bool boundaryOnly = !boundaryExhausted && CountEmptyMaskBoundaryCells(board, maskCanSpawn) > 0;
                ranked.Clear();
                for (int i = 0; i < microCandidates.Count; i++)
                {
                    int[] path = microCandidates[i];
                    if (path == null || path.Length < 2 || path.Length > gap)
                        continue;
                    if (CountRewrittenCells(board, path) > 0)
                        continue;

                    bool connected = false;
                    var pathSet = new HashSet<int>(path.Length);
                    for (int p = 0; p < path.Length; p++)
                    {
                        pathSet.Add(path[p]);
                        if (PathTouchesExistingArrow(board, path[p], pathSet))
                            connected = true;
                    }

                    int boundaryCells = CountPathMaskBoundaryCells(path, board.width, board.height, maskCanSpawn);
                    if (boundaryOnly && boundaryCells <= 0)
                        continue;

                    bool straight = IsStraightPath(path, board.width);
                    int score = path.Length * 220 + boundaryCells * (boundaryOnly ? 1800 : 900) + PathCompactness(board, path, maskCanSpawn);
                    if (straight)
                        score += (boundaryOnly ? 1100 : 650) + path.Length * 90;
                    if (connected)
                        score += 220;

                    ranked.Add(new SafeRefillCandidate
                    {
                        Path = path,
                        Gain = path.Length,
                        Score = score
                    });
                }

                if (ranked.Count == 0)
                {
                    lastReject = boundaryOnly ? "boundary candidates filtered" : "candidates filtered";
                    if (boundaryOnly)
                    {
                        boundaryExhausted = true;
                        continue;
                    }

                    if (CountEmptyMaskBoundaryCells(board, maskCanSpawn) > 0 &&
                        TryGreedySafeBoundarySwapRefill(
                            ref board,
                            maskCanSpawn,
                            greedyMoves,
                            current,
                            targetFill,
                            maxPathLen,
                            out int fallbackSwapGain,
                            out int fallbackSwapBoundaryGain,
                            out int fallbackSwapRejected,
                            out string fallbackSwapDetails))
                    {
                        addedTotal += Mathf.Max(0, fallbackSwapGain);
                        boundaryCommitted += fallbackSwapBoundaryGain;
                        boundarySwapCommitted++;
                        boundarySwapRejected += fallbackSwapRejected;
                        boundaryExhausted = false;
                        committed++;
                        passes++;
                        lastReject = fallbackSwapDetails;
                        continue;
                    }

                    break;
                }

                ranked.Sort((a, b) =>
                {
                    int cmp = b.Score.CompareTo(a.Score);
                    if (cmp != 0)
                        return cmp;
                    return b.Gain.CompareTo(a.Gain);
                });

                BoardState acceptedBoard = null;
                string acceptedDetails = string.Empty;
                int acceptedGain = 0;
                int acceptedBoundaryGain = 0;
                int evalLimit = boundaryOnly ? kGreedySafeBoundaryRefillCandidateEvalCap : kGreedySafeRefillCandidateEvalCap;
                int evalCount = Mathf.Min(evalLimit, ranked.Count);
                for (int i = 0; i < evalCount; i++)
                {
                    var entry = ranked[i];
                    if (entry?.Path == null || entry.Path.Length < 2)
                        continue;

                    var candidate = CloneBoard(board);
                    if (candidate == null)
                        continue;
                    if (!ApplyOrderedIndexChain(candidate, entry.Path))
                    {
                        applyRejected++;
                        continue;
                    }
                    if (!ValidateArrowDegreeLimit(candidate))
                        degreeWarnings++;

                    int newCount = CountArrowTiles(candidate);
                    int gain = newCount - current;
                    if (gain <= 0 || newCount > targetFill)
                    {
                        gainRejected++;
                        continue;
                    }

                    if (TryAcceptCoreRescueCandidate(
                        ref candidate,
                        maskCanSpawn,
                        greedyMoves,
                        newCount,
                        requireCleanGeometry: false,
                        out string acceptDetails))
                    {
                        acceptedBoard = candidate;
                        acceptedDetails = acceptDetails;
                        acceptedGain = gain;
                        acceptedBoundaryGain = CountPathMaskBoundaryCells(entry.Path, board.width, board.height, maskCanSpawn);
                        break;
                    }

                    rejected++;
                    lastReject = acceptDetails;
                }

                if (acceptedBoard == null)
                {
                    if (boundaryOnly)
                    {
                        if (TryGreedySafeBoundarySwapRefill(
                            ref board,
                            maskCanSpawn,
                            greedyMoves,
                            current,
                            targetFill,
                            maxPathLen,
                            out int swapGain,
                            out int swapBoundaryGain,
                            out int swapRejected,
                            out string swapDetails))
                        {
                            addedTotal += Mathf.Max(0, swapGain);
                            boundaryCommitted += swapBoundaryGain;
                            boundarySwapCommitted++;
                            boundarySwapRejected += swapRejected;
                            committed++;
                            passes++;
                            lastReject = swapDetails;
                            UpdateSafeRefillBest(board, maskCanSpawn, ref bestBoard, ref bestFill, ref bestBoundary);
                            continue;
                        }

                        boundarySwapRejected += swapRejected;
                        boundaryExhausted = true;
                        lastReject = string.IsNullOrWhiteSpace(lastReject)
                            ? $"boundary candidates rejected | {swapDetails}"
                            : $"boundary candidates rejected | {lastReject} | {swapDetails}";
                        continue;
                    }

                    if (CountEmptyMaskBoundaryCells(board, maskCanSpawn) > 0 &&
                        TryGreedySafeBoundarySwapRefill(
                            ref board,
                            maskCanSpawn,
                            greedyMoves,
                            current,
                            targetFill,
                            maxPathLen,
                            out int fallbackSwapGain,
                            out int fallbackSwapBoundaryGain,
                            out int fallbackSwapRejected,
                            out string fallbackSwapDetails))
                    {
                        addedTotal += Mathf.Max(0, fallbackSwapGain);
                        boundaryCommitted += fallbackSwapBoundaryGain;
                        boundarySwapCommitted++;
                        boundarySwapRejected += fallbackSwapRejected;
                        boundaryExhausted = false;
                        committed++;
                        passes++;
                        lastReject = fallbackSwapDetails;
                        UpdateSafeRefillBest(board, maskCanSpawn, ref bestBoard, ref bestFill, ref bestBoundary);
                        continue;
                    }

                    break;
                }

                board = acceptedBoard;
                addedTotal += acceptedGain;
                if (boundaryOnly)
                    boundaryCommitted += acceptedBoundaryGain;
                else
                    boundaryExhausted = false;
                UpdateSafeRefillBest(board, maskCanSpawn, ref bestBoard, ref bestFill, ref bestBoundary);
                committed++;
                passes++;
                lastReject = acceptedDetails;
            }

            if (bestBoard != null)
                board = bestBoard;

            if (TryGreedySafeDirectExitRefill(
                ref board,
                maskCanSpawn,
                greedyMoves,
                targetFill,
                out int directExitAdded,
                out directExitCommitted,
                out directExitRejected,
                out string directExitDetails))
            {
                addedTotal += directExitAdded;
                lastReject = string.IsNullOrWhiteSpace(lastReject)
                    ? directExitDetails
                    : $"{lastReject} | {directExitDetails}";
            }
            else if (!string.IsNullOrWhiteSpace(directExitDetails))
            {
                lastReject = string.IsNullOrWhiteSpace(lastReject)
                    ? directExitDetails
                    : $"{lastReject} | {directExitDetails}";
            }

            int finalFill = CountArrowTiles(board);
            int boundaryAfter = CountEmptyMaskBoundaryCells(board, maskCanSpawn);
            details = $"post-core-refill: target={targetFill}, added={addedTotal}, committed={committed}, rejected={rejected}, applyRejected={applyRejected}, gainRejected={gainRejected}, degreeWarnings={degreeWarnings}, boundaryEmpty={boundaryBefore}->{boundaryAfter}, boundaryCommitted={boundaryCommitted}, boundarySwap={boundarySwapCommitted}/{boundarySwapRejected}, directExit={directExitCommitted}/{directExitRejected}, passes={passes}, finalFill={finalFill} | {lastReject}";
            return addedTotal > 0;
        }

        private static void UpdateSafeRefillBest(
            BoardState board,
            bool[] maskCanSpawn,
            ref BoardState bestBoard,
            ref int bestFill,
            ref int bestBoundary)
        {
            if (board == null || board.tiles == null)
                return;

            int fill = CountArrowTiles(board);
            int boundary = CountEmptyMaskBoundaryCells(board, maskCanSpawn);
            if (bestBoard != null && fill < bestFill)
                return;
            if (bestBoard != null && fill == bestFill && boundary >= bestBoundary)
                return;

            bestBoard = CloneBoard(board);
            bestFill = fill;
            bestBoundary = boundary;
        }

        private static bool TryGreedySafeDirectExitRefill(
            ref BoardState board,
            bool[] maskCanSpawn,
            int greedyMoves,
            int targetFill,
            out int addedTotal,
            out int committed,
            out int rejected,
            out string details)
        {
            addedTotal = 0;
            committed = 0;
            rejected = 0;
            details = "direct-exit: no action";

            if (board == null || board.tiles == null || maskCanSpawn == null || maskCanSpawn.Length != board.width * board.height)
                return false;

            var ranked = new List<SafeRefillCandidate>();
            string lastReject = string.Empty;
            int passes = 0;
            int maxBoundaryEmpty = CountEmptyMaskBoundaryCells(board, maskCanSpawn);

            while (passes < kGreedySafeRefillMaxPasses)
            {
                int current = CountArrowTiles(board);
                int gap = targetFill - current;
                if (gap < 2)
                    break;

                ranked.Clear();
                CollectDirectExitCandidates(board, maskCanSpawn, kGreedySafeRefillMaxPathLen, ranked);
                if (ranked.Count == 0)
                {
                    lastReject = "direct-exit: no candidates";
                    break;
                }

                ranked.Sort((a, b) =>
                {
                    int cmp = b.Score.CompareTo(a.Score);
                    if (cmp != 0)
                        return cmp;
                    return b.Gain.CompareTo(a.Gain);
                });

                BoardState acceptedBoard = null;
                int acceptedGain = 0;
                string acceptedDetails = string.Empty;
                int evalCount = Mathf.Min(kGreedySafeBoundaryRefillCandidateEvalCap, ranked.Count);
                for (int i = 0; i < evalCount; i++)
                {
                    int[] path = ranked[i].Path;
                    if (path == null || path.Length < 2)
                        continue;

                    var candidate = CloneBoard(board);
                    if (candidate == null)
                        continue;
                    if (!ApplyOrderedIndexChain(candidate, path))
                        continue;

                    int newCount = CountArrowTiles(candidate);
                    int gain = newCount - current;
                    if (gain <= 0 || newCount > targetFill)
                        continue;

                    if (TryAcceptCoreRescueCandidate(
                        ref candidate,
                        maskCanSpawn,
                        greedyMoves,
                        current + 1,
                        requireCleanGeometry: false,
                        out string acceptDetails))
                    {
                        int candidateBoundaryEmpty = CountEmptyMaskBoundaryCells(candidate, maskCanSpawn);
                        if (candidateBoundaryEmpty > maxBoundaryEmpty)
                        {
                            rejected++;
                            lastReject = $"direct-exit boundary reject {candidateBoundaryEmpty}>{maxBoundaryEmpty}";
                            continue;
                        }

                        acceptedBoard = candidate;
                        acceptedGain = CountArrowTiles(candidate) - current;
                        acceptedDetails = acceptDetails;
                        break;
                    }

                    rejected++;
                    lastReject = acceptDetails;
                }

                if (acceptedBoard == null)
                    break;

                board = acceptedBoard;
                addedTotal += acceptedGain;
                committed++;
                passes++;
                lastReject = acceptedDetails;
            }

            int finalFill = CountArrowTiles(board);
            details = $"direct-exit: added={addedTotal}, committed={committed}, rejected={rejected}, passes={passes}, finalFill={finalFill}{(!string.IsNullOrWhiteSpace(lastReject) ? $" | {lastReject}" : string.Empty)}";
            return addedTotal > 0;
        }

        private static void CollectDirectExitCandidates(
            BoardState board,
            bool[] maskCanSpawn,
            int maxPathLen,
            List<SafeRefillCandidate> ranked)
        {
            if (board == null || board.tiles == null || maskCanSpawn == null || ranked == null)
                return;

            int maxLen = Mathf.Clamp(maxPathLen, 2, kGreedySafeRefillMaxPathLen);
            Vector2Int[] exits =
            {
                new Vector2Int(0, 1),
                new Vector2Int(1, 0),
                new Vector2Int(0, -1),
                new Vector2Int(-1, 0)
            };

            for (int idx = 0; idx < board.tiles.Length; idx++)
            {
                if (idx >= maskCanSpawn.Length || !maskCanSpawn[idx])
                    continue;
                if (board.tiles[idx].type != TileType.Empty)
                    continue;

                int headX = idx % board.width;
                int headY = idx / board.width;
                for (int d = 0; d < exits.Length; d++)
                {
                    Vector2Int exitDir = exits[d];
                    if (CanSeeBoardExit(board, headX, headY, exitDir))
                    {
                        AddDirectExitCandidate(board, maskCanSpawn, idx, exitDir, maxLen, ranked);
                    }
                    else
                    {
                        AddCorridorExitCandidate(board, maskCanSpawn, idx, exitDir, maxLen, ranked);
                    }
                }
            }
        }

        private static void AddCorridorExitCandidate(
            BoardState board,
            bool[] maskCanSpawn,
            int seedIdx,
            Vector2Int exitDir,
            int maxLen,
            List<SafeRefillCandidate> ranked)
        {
            if (board == null || board.tiles == null || ranked == null || maxLen < 2)
                return;
            if (Mathf.Abs(exitDir.x) + Mathf.Abs(exitDir.y) != 1)
                return;

            int seedX = seedIdx % board.width;
            int seedY = seedIdx / board.width;
            int maxRewrite = Mathf.Max(1, Mathf.Min(5, maxLen - 1));
            var outward = new List<int>(maxLen) { seedIdx };
            int rewritten = 0;
            int emptyCells = board.tiles[seedIdx].type == TileType.Empty ? 1 : 0;

            int x = seedX + exitDir.x;
            int y = seedY + exitDir.y;
            while (outward.Count < maxLen && board.InBounds(x, y))
            {
                int idx = board.Index(x, y);
                if (idx < 0 || idx >= maskCanSpawn.Length || !maskCanSpawn[idx])
                    break;
                if (board.tiles[idx].type == TileType.Block)
                    break;

                if (board.tiles[idx].type == TileType.Arrow)
                {
                    rewritten++;
                    if (rewritten > maxRewrite)
                        break;
                }
                else if (board.tiles[idx].type == TileType.Empty)
                {
                    emptyCells++;
                }

                outward.Add(idx);
                if (outward.Count >= 2 && emptyCells >= 2 && CanSeeBoardExit(board, x, y, exitDir))
                    AddCorridorExitPath(board, maskCanSpawn, outward, exitDir, maxLen, rewritten, emptyCells, ranked);

                x += exitDir.x;
                y += exitDir.y;
            }
        }

        private static void AddCorridorExitPath(
            BoardState board,
            bool[] maskCanSpawn,
            List<int> outward,
            Vector2Int exitDir,
            int maxLen,
            int rewritten,
            int emptyCells,
            List<SafeRefillCandidate> ranked)
        {
            if (outward == null || outward.Count < 2 || ranked == null)
                return;
            if (emptyCells < 2)
                return;

            var fromHead = new List<int>(maxLen);
            for (int i = outward.Count - 1; i >= 0 && fromHead.Count < maxLen; i--)
                fromHead.Add(outward[i]);

            int seedIdx = outward[0];
            int tailX = (seedIdx % board.width) - exitDir.x;
            int tailY = (seedIdx / board.width) - exitDir.y;
            while (fromHead.Count < maxLen && board.InBounds(tailX, tailY))
            {
                int tailIdx = board.Index(tailX, tailY);
                if (tailIdx < 0 || tailIdx >= maskCanSpawn.Length || !maskCanSpawn[tailIdx])
                    break;
                if (board.tiles[tailIdx].type != TileType.Empty)
                    break;

                fromHead.Add(tailIdx);
                tailX -= exitDir.x;
                tailY -= exitDir.y;
            }

            if (fromHead.Count < 2)
                return;

            int[] path = new int[fromHead.Count];
            for (int i = 0; i < fromHead.Count; i++)
                path[i] = fromHead[fromHead.Count - 1 - i];

            int gain = CountEmptyCellsInPath(board, path);
            if (gain < 2)
                return;

            int boundaryCells = CountPathMaskBoundaryCells(path, board.width, board.height, maskCanSpawn);
            int score = gain * 1300 + path.Length * 180 + boundaryCells * 120 + PathCompactness(board, path, maskCanSpawn);
            score -= rewritten * (kMicroRewritePenalty + 40);

            ranked.Add(new SafeRefillCandidate
            {
                Path = path,
                Gain = gain,
                Score = score
            });
        }

        private static void AddDirectExitCandidate(
            BoardState board,
            bool[] maskCanSpawn,
            int headIdx,
            Vector2Int exitDir,
            int maxLen,
            List<SafeRefillCandidate> ranked)
        {
            if (board == null || board.tiles == null || ranked == null || maxLen < 2)
                return;

            int headX = headIdx % board.width;
            int headY = headIdx / board.width;
            var fromHead = new List<int>(maxLen) { headIdx };
            int x = headX - exitDir.x;
            int y = headY - exitDir.y;
            while (fromHead.Count < maxLen && board.InBounds(x, y))
            {
                int idx = board.Index(x, y);
                if (idx < 0 || idx >= maskCanSpawn.Length || !maskCanSpawn[idx])
                    break;
                if (board.tiles[idx].type != TileType.Empty)
                    break;

                fromHead.Add(idx);
                x -= exitDir.x;
                y -= exitDir.y;
            }

            for (int len = fromHead.Count; len >= 2; len--)
            {
                int[] path = new int[len];
                for (int i = 0; i < len; i++)
                    path[i] = fromHead[len - 1 - i];

                int boundaryCells = CountPathMaskBoundaryCells(path, board.width, board.height, maskCanSpawn);
                int score = len * 1100 + boundaryCells * 180 + PathCompactness(board, path, maskCanSpawn);
                if (IsMaskBoundaryCell(headIdx, board.width, board.height, maskCanSpawn))
                    score += 250;

                ranked.Add(new SafeRefillCandidate
                {
                    Path = path,
                    Gain = len,
                    Score = score
                });
            }
        }

        private static bool CanSeeBoardExit(BoardState board, int startX, int startY, Vector2Int dir)
        {
            if (board == null || board.tiles == null)
                return false;
            if (Mathf.Abs(dir.x) + Mathf.Abs(dir.y) != 1)
                return false;

            int x = startX + dir.x;
            int y = startY + dir.y;
            while (board.InBounds(x, y))
            {
                int idx = board.Index(x, y);
                if (board.tiles[idx].type != TileType.Empty)
                    return false;

                x += dir.x;
                y += dir.y;
            }

            return true;
        }

        private static bool TryGreedySafeBoundarySwapRefill(
            ref BoardState board,
            bool[] maskCanSpawn,
            int greedyMoves,
            int minAcceptFill,
            int targetFill,
            int maxPathLen,
            out int acceptedGain,
            out int acceptedBoundaryGain,
            out int rejected,
            out string details)
        {
            acceptedGain = 0;
            acceptedBoundaryGain = 0;
            rejected = 0;
            details = "boundary-swap: no action";

            if (board == null || board.tiles == null || maskCanSpawn == null || maskCanSpawn.Length != board.width * board.height)
                return false;

            int beforeBoundary = CountEmptyMaskBoundaryCells(board, maskCanSpawn);
            if (beforeBoundary <= 0)
            {
                details = "boundary-swap: boundary already full";
                return false;
            }

            int current = CountArrowTiles(board);
            var microCandidates = new List<int[]>();
            var ranked = new List<SafeRefillCandidate>();
            int pathLimit = Mathf.Clamp(maxPathLen, 2, kGreedySafeRefillMaxPathLen);
            CollectBoundarySwapCandidates(board, maskCanSpawn, microCandidates, pathLimit);
            for (int i = 0; i < microCandidates.Count; i++)
            {
                AddBoundarySwapCandidate(board, maskCanSpawn, microCandidates[i], ranked);
                AddBoundarySwapCandidate(board, maskCanSpawn, ReversePathCopy(microCandidates[i]), ranked);
            }

            if (ranked.Count == 0)
            {
                details = "boundary-swap: no candidates";
                return false;
            }

            ranked.Sort((a, b) =>
            {
                int cmp = b.Score.CompareTo(a.Score);
                if (cmp != 0)
                    return cmp;
                return b.Gain.CompareTo(a.Gain);
            });

            string lastReject = string.Empty;
            int evalCount = Mathf.Min(kGreedySafeBoundaryRefillCandidateEvalCap, ranked.Count);
            for (int i = 0; i < evalCount; i++)
            {
                int[] path = ranked[i].Path;
                if (path == null || path.Length < 2)
                    continue;

                var candidate = CloneBoard(board);
                if (candidate == null)
                    continue;
                if (!ApplyOrderedIndexChain(candidate, path))
                    continue;

                int rawFill = CountArrowTiles(candidate);
                if (rawFill > targetFill)
                    continue;

                int rawBoundary = CountEmptyMaskBoundaryCells(candidate, maskCanSpawn);
                if (rawBoundary >= beforeBoundary)
                    continue;

                if (!TryAcceptCoreRescueCandidate(
                    ref candidate,
                    maskCanSpawn,
                    greedyMoves,
                    minAcceptFill,
                    requireCleanGeometry: false,
                    out string acceptDetails))
                {
                    rejected++;
                    lastReject = acceptDetails;
                    continue;
                }

                int finalFill = CountArrowTiles(candidate);
                int finalBoundary = CountEmptyMaskBoundaryCells(candidate, maskCanSpawn);
                if (finalFill < minAcceptFill || finalBoundary >= beforeBoundary)
                {
                    rejected++;
                    lastReject = $"accepted-shape-rejected fill={finalFill}/{minAcceptFill} boundary={beforeBoundary}->{finalBoundary}";
                    continue;
                }

                board = candidate;
                acceptedGain = finalFill - current;
                acceptedBoundaryGain = beforeBoundary - finalBoundary;
                details = $"boundary-swap: gain={acceptedGain}, boundary={beforeBoundary}->{finalBoundary}, eval={i + 1}, rejected={rejected} | {acceptDetails}";
                return true;
            }

            details = string.IsNullOrWhiteSpace(lastReject)
                ? $"boundary-swap: rejected eval={evalCount}"
                : $"boundary-swap: rejected eval={evalCount} | {lastReject}";
            return false;
        }

        private static void CollectBoundarySwapCandidates(
            BoardState board,
            bool[] maskCanSpawn,
            List<int[]> candidates,
            int maxPathLen)
        {
            candidates.Clear();
            if (board == null || board.tiles == null || maskCanSpawn == null || candidates == null)
                return;

            int w = board.width;
            int h = board.height;
            int maxLen = Mathf.Clamp(maxPathLen, 2, kGreedySafeRefillMaxPathLen);
            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };
            int[] path = new int[maxLen];
            int[] usedMark = new int[w * h];
            int mark = 1;

            for (int idx = 0; idx < board.tiles.Length; idx++)
            {
                if (idx >= maskCanSpawn.Length || !maskCanSpawn[idx])
                    continue;
                if (board.tiles[idx].type != TileType.Empty)
                    continue;
                if (!IsMaskBoundaryCell(idx, w, h, maskCanSpawn))
                    continue;

                mark++;
                path[0] = idx;
                usedMark[idx] = mark;
                CollectBoundarySwapPathCandidates(
                    board,
                    maskCanSpawn,
                    candidates,
                    usedMark,
                    mark,
                    dx,
                    dy,
                    w,
                    h,
                    path,
                    maxLen,
                    1,
                    idx % w,
                    idx / w,
                    hasRewrite: false);
                usedMark[idx] = 0;

                if (candidates.Count >= kMicroFillCandidateCap)
                    return;
            }
        }

        private static void CollectBoundarySwapPathCandidates(
            BoardState board,
            bool[] maskCanSpawn,
            List<int[]> candidates,
            int[] usedMark,
            int mark,
            int[] dx,
            int[] dy,
            int w,
            int h,
            int[] path,
            int maxLen,
            int len,
            int curX,
            int curY,
            bool hasRewrite)
        {
            if (len >= 2 && hasRewrite)
            {
                var copy = new int[len];
                Array.Copy(path, copy, len);
                AddUniqueIntArray(candidates, copy);
            }

            if (len >= maxLen || candidates.Count >= kMicroFillCandidateCap)
                return;

            for (int dir = 0; dir < 4; dir++)
            {
                int nx = curX + dx[dir];
                int ny = curY + dy[dir];
                if (nx < 0 || nx >= w || ny < 0 || ny >= h)
                    continue;

                int nextIdx = ny * w + nx;
                if (nextIdx < 0 || nextIdx >= maskCanSpawn.Length || !maskCanSpawn[nextIdx])
                    continue;
                if (usedMark[nextIdx] == mark)
                    continue;

                bool nextRewrite = hasRewrite || board.tiles[nextIdx].type == TileType.Arrow;
                usedMark[nextIdx] = mark;
                path[len] = nextIdx;
                CollectBoundarySwapPathCandidates(
                    board,
                    maskCanSpawn,
                    candidates,
                    usedMark,
                    mark,
                    dx,
                    dy,
                    w,
                    h,
                    path,
                    maxLen,
                    len + 1,
                    nx,
                    ny,
                    nextRewrite);
                usedMark[nextIdx] = 0;

                if (candidates.Count >= kMicroFillCandidateCap)
                    return;
            }
        }

        private static void AddBoundarySwapCandidate(
            BoardState board,
            bool[] maskCanSpawn,
            int[] path,
            List<SafeRefillCandidate> ranked)
        {
            if (board == null || board.tiles == null || maskCanSpawn == null || path == null || ranked == null)
                return;
            if (path.Length < 2 || path.Length > kGreedySafeRefillMaxPathLen)
                return;

            int emptyBoundaryCells = CountEmptyPathMaskBoundaryCells(board, path, board.width, board.height, maskCanSpawn);
            if (emptyBoundaryCells <= 0)
                return;

            int rewrittenCells = CountRewrittenCells(board, path);
            if (rewrittenCells <= 0)
                return;

            int maxRewrite = Mathf.Max(1, Mathf.Min(5, path.Length - 1));
            if (rewrittenCells > maxRewrite)
                return;

            int emptyCells = CountEmptyCellsInPath(board, path);
            if (emptyCells <= 0)
                return;

            int boundaryCells = CountPathMaskBoundaryCells(path, board.width, board.height, maskCanSpawn);
            bool straight = IsStraightPath(path, board.width);
            int score = emptyBoundaryCells * 4200 + boundaryCells * 1200 + emptyCells * 320;
            if (straight)
                score += 950 + path.Length * 90;
            score += PathCompactness(board, path, maskCanSpawn);
            score -= rewrittenCells * kMicroRewritePenalty;

            ranked.Add(new SafeRefillCandidate
            {
                Path = path,
                Gain = emptyBoundaryCells,
                Score = score
            });
        }

        private static bool PathTouchesMaskBoundary(int[] path, int width, int height, bool[] maskCanSpawn)
        {
            if (path == null || maskCanSpawn == null)
                return false;

            for (int i = 0; i < path.Length; i++)
            {
                int idx = path[i];
                if (idx >= 0 && idx < maskCanSpawn.Length && IsMaskBoundaryCell(idx, width, height, maskCanSpawn))
                    return true;
            }

            return false;
        }

        private static int CountPathMaskBoundaryCells(int[] path, int width, int height, bool[] maskCanSpawn)
        {
            if (path == null || maskCanSpawn == null)
                return 0;

            int count = 0;
            for (int i = 0; i < path.Length; i++)
            {
                int idx = path[i];
                if (idx >= 0 && idx < maskCanSpawn.Length && IsMaskBoundaryCell(idx, width, height, maskCanSpawn))
                    count++;
            }

            return count;
        }

        private static int CountEmptyMaskBoundaryCells(BoardState board, bool[] maskCanSpawn)
        {
            if (board == null || board.tiles == null || maskCanSpawn == null || maskCanSpawn.Length != board.width * board.height)
                return 0;

            int count = 0;
            for (int i = 0; i < maskCanSpawn.Length; i++)
            {
                if (!maskCanSpawn[i])
                    continue;
                if (board.tiles[i].type != TileType.Empty)
                    continue;
                if (IsMaskBoundaryCell(i, board.width, board.height, maskCanSpawn))
                    count++;
            }

            return count;
        }

        private static int CountEmptyPathMaskBoundaryCells(BoardState board, int[] path, int width, int height, bool[] maskCanSpawn)
        {
            if (board == null || board.tiles == null || path == null || maskCanSpawn == null)
                return 0;

            int count = 0;
            for (int i = 0; i < path.Length; i++)
            {
                int idx = path[i];
                if (idx < 0 || idx >= board.tiles.Length || idx >= maskCanSpawn.Length)
                    continue;
                if (board.tiles[idx].type != TileType.Empty)
                    continue;
                if (IsMaskBoundaryCell(idx, width, height, maskCanSpawn))
                    count++;
            }

            return count;
        }

        private static int CountEmptyCellsInPath(BoardState board, int[] path)
        {
            if (board == null || board.tiles == null || path == null)
                return 0;

            int count = 0;
            for (int i = 0; i < path.Length; i++)
            {
                int idx = path[i];
                if (idx >= 0 && idx < board.tiles.Length && board.tiles[idx].type == TileType.Empty)
                    count++;
            }

            return count;
        }

        private static int[] ReversePathCopy(int[] path)
        {
            if (path == null)
                return null;

            int[] reversed = new int[path.Length];
            for (int i = 0; i < path.Length; i++)
                reversed[i] = path[path.Length - 1 - i];
            return reversed;
        }

        private static bool IsStraightPath(int[] path, int width)
        {
            if (path == null || path.Length < 2 || width <= 0)
                return false;

            int first = path[0];
            int second = path[1];
            int dx = (second % width) - (first % width);
            int dy = (second / width) - (first / width);
            if (Mathf.Abs(dx) + Mathf.Abs(dy) != 1)
                return false;

            for (int i = 2; i < path.Length; i++)
            {
                int prev = path[i - 1];
                int cur = path[i];
                int curDx = (cur % width) - (prev % width);
                int curDy = (cur / width) - (prev / width);
                if (curDx != dx || curDy != dy)
                    return false;
            }

            return true;
        }

        private static bool TryClearSmallChainAndRetryMicroFill(
            ref BoardState board,
            bool[] maskCanSpawn,
            int targetFill,
            out string details)
        {
            details = "shape-heal-micro: no removable chain";

            if (board == null || board.tiles == null || maskCanSpawn == null || maskCanSpawn.Length != board.width * board.height)
                return false;

            var chains = new List<int[]>();
            var chainFlags = new List<bool>();
            var visited = new HashSet<int>(Mathf.Max(1, board.tiles.Length));
            var chainSet = new HashSet<int>(Mathf.Max(1, board.tiles.Length));
            var ordered = new List<Vector2Int>(16);

            if (!CollectMaskedChains(board, chains, chainFlags, visited, chainSet, ordered) || chains.Count == 0)
            {
                return false;
            }

            int bestIndex = -1;
            int bestLength = int.MaxValue;
            for (int i = 0; i < chains.Count; i++)
            {
                int[] chain = chains[i];
                if (chain == null || chain.Length < 2)
                    continue;

                bool allInMask = true;
                for (int j = 0; j < chain.Length; j++)
                {
                    int idx = chain[j];
                    if (idx < 0 || idx >= maskCanSpawn.Length || !maskCanSpawn[idx])
                    {
                        allInMask = false;
                        break;
                    }
                }

                if (!allInMask)
                    continue;

                if (chain.Length <= bestLength && chain.Length <= 4)
                {
                    bestLength = chain.Length;
                    bestIndex = i;
                }
            }

            if (bestIndex < 0)
            {
                int current = CountArrowTiles(board);
                if (current < targetFill)
                {
                    bestLength = int.MaxValue;
                    for (int i = 0; i < chains.Count; i++)
                    {
                        int[] chain = chains[i];
                        if (chain == null || chain.Length < 2)
                            continue;

                        if (chain.Length < bestLength)
                        {
                            bestLength = chain.Length;
                            bestIndex = i;
                        }
                    }
                }
            }

            if (bestIndex < 0)
                return false;

            int cleared = chains[bestIndex].Length;
            for (int i = 0; i < chains[bestIndex].Length; i++)
            {
                int idx = chains[bestIndex][i];
                if (idx >= 0 && idx < board.tiles.Length)
                {
                    board.tiles[idx] = TileState.Empty();
                }
            }

            details = $"shape-heal-micro: clear-small-chain len={cleared}";
            return true;
        }

        private static void CollectMicroChainCandidates(
            BoardState board,
            bool[] maskCanSpawn,
            List<int[]> microCandidates,
            int maxPathLen = 2,
            bool allowRewrite = false)
        {
            microCandidates.Clear();
            if (board == null || board.tiles == null || maskCanSpawn == null || microCandidates == null)
                return;

            int w = board.width;
            int h = board.height;
            int maxLen = Mathf.Clamp(maxPathLen, 2, kMicroFillMaxPathLen);
            if (maxLen < 2)
                return;

            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };
            int[] path = new int[maxLen];
            int[] usedMark = new int[w * h];
            int mark = 1;

            for (int startY = 0; startY < h; startY++)
            {
                for (int startX = 0; startX < w; startX++)
                {
                    int startIdx = board.Index(startX, startY);
                    if (!maskCanSpawn[startIdx])
                        continue;

                    if (!allowRewrite && board.tiles[startIdx].type != TileType.Empty)
                        continue;

                    if (microCandidates.Count >= kMicroFillCandidateCap)
                        return;

                    mark++;
                    path[0] = startIdx;
                    usedMark[startIdx] = mark;

                    CollectMicroPathCandidates(
                        board,
                        maskCanSpawn,
                        microCandidates,
                        usedMark,
                        mark,
                        dx,
                        dy,
                        w,
                        h,
                        path,
                        maxLen,
                        1,
                        startX,
                        startY,
                        allowRewrite);

                    usedMark[startIdx] = 0;
                }
            }
        }

        private static void CollectMicroPathCandidates(
            BoardState board,
            bool[] maskCanSpawn,
            List<int[]> microCandidates,
            int[] usedMark,
            int mark,
            int[] dx,
            int[] dy,
            int w,
            int h,
            int[] path,
            int maxLen,
            int len,
            int curX,
            int curY,
            bool allowRewrite)
        {
            if (len >= 2)
            {
                if (path[0] < path[len - 1])
                {
                    var copy = new int[len];
                    Array.Copy(path, copy, len);
                    AddUniqueIntArray(microCandidates, copy);
                }
            }

            if (len >= maxLen)
                return;

            if (microCandidates.Count >= kMicroFillCandidateCap)
                return;

            for (int dir = 0; dir < 4; dir++)
            {
                int nx = curX + dx[dir];
                int ny = curY + dy[dir];
                if (nx < 0 || nx >= w || ny < 0 || ny >= h)
                    continue;

                int nextIdx = board.Index(nx, ny);
                if (!maskCanSpawn[nextIdx])
                    continue;

                if (!allowRewrite && board.tiles[nextIdx].type != TileType.Empty)
                    continue;

                if (usedMark[nextIdx] == mark)
                    continue;

                usedMark[nextIdx] = mark;
                path[len] = nextIdx;
                CollectMicroPathCandidates(
                    board,
                    maskCanSpawn,
                    microCandidates,
                    usedMark,
                    mark,
                    dx,
                    dy,
                    w,
                    h,
                    path,
                    maxLen,
                    len + 1,
                    nx,
                    ny,
                    allowRewrite);
                usedMark[nextIdx] = 0;

                if (microCandidates.Count >= kMicroFillCandidateCap)
                    return;
            }

        }

        private static int CountRewrittenCells(BoardState board, int[] path)
        {
            if (board == null || board.tiles == null || path == null || path.Length == 0)
                return 0;

            int count = 0;
            for (int i = 0; i < path.Length; i++)
            {
                int idx = path[i];
                if (idx >= 0 && idx < board.tiles.Length && board.tiles[idx].type == TileType.Arrow)
                    count++;
            }

            return count;
        }

        private static bool PathTouchesExistingArrow(BoardState board, int idx, HashSet<int> excluded)
        {
            if (board == null || board.tiles == null || excluded == null)
                return false;

            int x = idx % board.width;
            int y = idx / board.width;

            if (IsArrowAtBoardPos(board, x + 1, y, excluded))
                return true;
            if (IsArrowAtBoardPos(board, x - 1, y, excluded))
                return true;
            if (IsArrowAtBoardPos(board, x, y + 1, excluded))
                return true;
            if (IsArrowAtBoardPos(board, x, y - 1, excluded))
                return true;

            return false;
        }

        private static bool IsArrowAtBoardPos(BoardState board, int x, int y, HashSet<int> excluded)
        {
            if (!board.InBounds(x, y))
                return false;

            int idx = board.Index(x, y);
            if (excluded.Contains(idx))
                return false;

            return board.tiles[idx].type == TileType.Arrow;
        }

        private static int PathCompactness(BoardState board, int[] path, bool[] maskCanSpawn)
        {
            if (board == null || path == null || path.Length == 0 || maskCanSpawn == null)
                return 0;

            int adjacencyScore = 0;
            for (int i = 0; i < path.Length; i++)
            {
                int p = path[i];
                int x = p % board.width;
                int y = p / board.width;

                if (x + 1 < board.width && maskCanSpawn[p + 1] && board.tiles[p + 1].type == TileType.Arrow)
                    adjacencyScore++;
                if (x - 1 >= 0 && maskCanSpawn[p - 1] && board.tiles[p - 1].type == TileType.Arrow)
                    adjacencyScore++;
                if (y + 1 < board.height && board.InBounds(x, y + 1))
                {
                    int n = p + board.width;
                    if (maskCanSpawn[n] && board.tiles[n].type == TileType.Arrow)
                        adjacencyScore++;
                }
                if (y - 1 >= 0 && board.InBounds(x, y - 1))
                {
                    int n = p - board.width;
                    if (maskCanSpawn[n] && board.tiles[n].type == TileType.Arrow)
                        adjacencyScore++;
                }
            }

            return adjacencyScore * 2;
        }

        private static bool CollectOrderedChainsForHealing(
            BoardState board,
            List<int[]> orderedChains,
            List<bool> isLoopChain,
            HashSet<int> visited,
            HashSet<int> chainSet,
            List<Vector2Int> ordered)
        {
            orderedChains.Clear();
            isLoopChain.Clear();
            visited.Clear();

            if (board == null || board.tiles == null)
                return false;

            for (int i = 0; i < board.tiles.Length; i++)
            {
                if (visited.Contains(i))
                    continue;

                if (board.tiles[i].type != TileType.Arrow)
                {
                    visited.Add(i);
                    continue;
                }

                chainSet.Clear();
                int sx = i % board.width;
                int sy = i / board.width;
                ArrowChainUtility.CollectFullChain(board, new Vector2Int(sx, sy), 0, chainSet);
                if (chainSet.Count < 2)
                {
                    foreach (int idx in chainSet)
                        visited.Add(idx);
                    continue;
                }

                foreach (int idx in chainSet)
                    visited.Add(idx);

                ordered.Clear();
                if (!TryBuildStableOrderedChain(board, chainSet, new Vector2Int(sx, sy), out ordered))
                    continue;

                if (ordered == null || ordered.Count < 2)
                    continue;

                int[] copy = new int[ordered.Count];
                for (int k = 0; k < ordered.Count; k++)
                {
                    copy[k] = ordered[k].x + ordered[k].y * board.width;
                }

                bool hasEndpoint = false;
                foreach (int idx in chainSet)
                {
                    if (CountArrowAdjacency(board, idx, chainSet) == 1)
                    {
                        hasEndpoint = true;
                        break;
                    }
                }

                orderedChains.Add(copy);
                isLoopChain.Add(!hasEndpoint);
            }

            return orderedChains.Count > 0;
        }

        private static void CollectEndpointHealExtensions(
            BoardState board,
            int endpointIdx,
            int neighborIdx,
            bool[] maskCanSpawn,
            List<int[]> extensions)
        {
            if (board == null || board.tiles == null)
                return;

            if (!board.InBounds(endpointIdx % board.width, endpointIdx / board.width))
                return;

            int ex = endpointIdx % board.width;
            int ey = endpointIdx / board.width;
            int nx = neighborIdx % board.width;
            int ny = neighborIdx / board.width;

            if (!board.InBounds(nx, ny))
                return;

            int dirX = ex - nx;
            int dirY = ey - ny;
            if (dirX == 0 && dirY == 0)
                return;

            int nextX = ex + dirX;
            int nextY = ey + dirY;
            int nextIdx = -1;
            if (TryGetEmptyMaskCell(board, maskCanSpawn, nextX, nextY, out nextIdx))
            {
                AddUniqueIntArray(extensions, new[] { nextIdx });
            }

            int turnX = dirY;
            int turnY = -dirX;

            int turn1X = ex + turnX;
            int turn1Y = ey + turnY;
            int turn2X = ex - turnX;
            int turn2Y = ey - turnY;

            TryAddTwoStepExtension(board, maskCanSpawn, ex, ey, turn1X, turn1Y, turn2X, turn2Y, extensions);
            TryAddTwoStepExtension(board, maskCanSpawn, ex, ey, turn2X, turn2Y, turn1X, turn1Y, extensions);

            int straight2X = nextX + dirX;
            int straight2Y = nextY + dirY;
            if (nextIdx >= 0 && TryGetEmptyMaskCell(board, maskCanSpawn, straight2X, straight2Y, out int straight2Idx))
            {
                AddUniqueIntArray(extensions, new[] { nextIdx, straight2Idx });
            }

            if (TryGetEmptyMaskCell(board, maskCanSpawn, turn1X, turn1Y, out int turn1Idx))
            {
                int nextAfterTurnX = turn1X + dirX;
                int nextAfterTurnY = turn1Y + dirY;
                if (TryGetEmptyMaskCell(board, maskCanSpawn, nextAfterTurnX, nextAfterTurnY, out int turn1ForwardIdx))
                {
                    AddUniqueIntArray(extensions, new[] { turn1Idx, turn1ForwardIdx });
                }
            }

            if (TryGetEmptyMaskCell(board, maskCanSpawn, turn2X, turn2Y, out int turn2Idx))
            {
                int nextAfterTurnX = turn2X + dirX;
                int nextAfterTurnY = turn2Y + dirY;
                if (TryGetEmptyMaskCell(board, maskCanSpawn, nextAfterTurnX, nextAfterTurnY, out int turn2ForwardIdx))
                {
                    AddUniqueIntArray(extensions, new[] { turn2Idx, turn2ForwardIdx });
                }
            }
        }

        private static void TryAddTwoStepExtension(
            BoardState board,
            bool[] maskCanSpawn,
            int fromX,
            int fromY,
            int step1X,
            int step1Y,
            int step2X,
            int step2Y,
            List<int[]> extensions)
        {
            if (!TryGetEmptyMaskCell(board, maskCanSpawn, step1X, step1Y, out int step1Idx))
                return;

            if (step1X == step2X && step1Y == step2Y)
                return;

            if (!TryGetEmptyMaskCell(board, maskCanSpawn, step2X, step2Y, out int step2Idx))
                return;

            if (step2Idx == step1Idx)
                return;

            if (!AreAdjEqualOrNear(board, fromX, fromY, step1X, step1Y, step2X, step2Y))
                return;

            int[] candidate = new[] { step1Idx, step2Idx };
            AddUniqueIntArray(extensions, candidate);
        }

        private static bool AreAdjEqualOrNear(BoardState board, int x0, int y0, int x1, int y1, int x2, int y2)
        {
            if (!board.InBounds(x1, y1) || !board.InBounds(x2, y2))
                return false;

            if (Mathf.Abs(x1 - x0) + Mathf.Abs(y1 - y0) != 1)
                return false;

            if (Mathf.Abs(x2 - x1) + Mathf.Abs(y2 - y1) != 1)
                return false;

            if (x2 == x0 && y2 == y0)
                return false;

            return true;
        }

        private static bool TryGetEmptyMaskCell(
            BoardState board,
            bool[] maskCanSpawn,
            int x,
            int y,
            out int idx)
        {
            idx = -1;
            if (!board.InBounds(x, y))
                return false;

            idx = board.Index(x, y);
            return maskCanSpawn[idx] && board.tiles[idx].type == TileType.Empty;
        }

        private static void AddUniqueIntArray(List<int[]> list, int[] path)
        {
            if (list == null || path == null || path.Length == 0)
                return;

            for (int i = 0; i < list.Count; i++)
            {
                int[] existing = list[i];
                if (existing == null || existing.Length != path.Length)
                    continue;

                bool same = true;
                for (int j = 0; j < existing.Length; j++)
                {
                    if (existing[j] != path[j])
                    {
                        same = false;
                        break;
                    }
                }

                if (same)
                    return;
            }

            list.Add(path);
        }

        private static bool ValidateArrowDegreeLimit(BoardState board)
        {
            if (board == null || board.tiles == null)
                return false;

            var componentSet = new HashSet<int>(board.width * board.height);
            for (int i = 0; i < board.tiles.Length; i++)
            {
                if (board.tiles[i].type == TileType.Arrow)
                    componentSet.Add(i);
            }

            for (int i = 0; i < board.tiles.Length; i++)
            {
                if (board.tiles[i].type != TileType.Arrow)
                    continue;

                if (CountArrowAdjacency(board, i, componentSet) > 2)
                    return false;
            }

            return true;
        }

        private static int CountMaskArea(bool[] maskCanSpawn)
        {
            if (maskCanSpawn == null)
                return 0;

            int area = 0;
            for (int i = 0; i < maskCanSpawn.Length; i++)
            {
                if (maskCanSpawn[i])
                    area++;
            }

            return area;
        }

        private static bool IsMaskBoundaryCell(int idx, int w, int h, bool[] maskCanSpawn)
        {
            if (maskCanSpawn == null || idx < 0 || idx >= maskCanSpawn.Length)
                return false;
            if (!maskCanSpawn[idx])
                return false;

            if (w <= 1 || h <= 1)
                return true;

            int x = idx % w;
            int y = idx / w;

            if (x == 0 || x == w - 1 || y == 0 || y == h - 1)
                return true;

            int up = idx - w;
            int down = idx + w;
            int left = idx - 1;
            int right = idx + 1;

            return !maskCanSpawn[up] || !maskCanSpawn[down] || !maskCanSpawn[left] || !maskCanSpawn[right];
        }

        private static bool PreserveMaskedChainsBySplitting(
            BoardState sourceBoard,
            BoardState targetBoard,
            bool[] maskCanSpawn,
            Vector2Int offset,
            out int keptTileCount,
            out string details)
        {
            int keptTileLocal = 0;
            keptTileCount = 0;
            details = "mask-preserve: no source arrows";

            if (sourceBoard == null || sourceBoard.tiles == null)
                return false;
            if (targetBoard == null || targetBoard.tiles == null)
                return false;
            if (maskCanSpawn == null || maskCanSpawn.Length != targetBoard.width * targetBoard.height)
                return false;

            for (int i = 0; i < targetBoard.tiles.Length; i++)
                targetBoard.tiles[i] = TileState.Empty();

            int keptSegments = 0;
            int removedCells = 0;
            int sourceChains = 0;

            var visited = new HashSet<int>(Mathf.Max(1, sourceBoard.width * sourceBoard.height));
            var sourceComponent = new HashSet<int>();
            var orderedSource = new List<int>(16);
            var orderedSourcePos = new List<Vector2Int>(16);
            var segment = new List<int>(16);

            void FlushSegment()
            {
                if (segment.Count < 2)
                {
                    removedCells += segment.Count;
                    segment.Clear();
                    return;
                }

                if (!ApplyOrderedIndexChain(targetBoard, segment))
                {
                    removedCells += segment.Count;
                    ClearBoardCellsFromIndices(targetBoard, segment);
                }
                else
                {
                    keptSegments++;
                    keptTileLocal += segment.Count;
                }

                segment.Clear();
            }

            for (int sy = 0; sy < sourceBoard.height; sy++)
            {
                for (int sx = 0; sx < sourceBoard.width; sx++)
                {
                    int sourceIdx = sx + sy * sourceBoard.width;
                    if (visited.Contains(sourceIdx))
                        continue;

                    if (sourceBoard.tiles[sourceIdx].type != TileType.Arrow)
                    {
                        visited.Add(sourceIdx);
                        continue;
                    }

                    sourceComponent.Clear();
                    orderedSource.Clear();
                    if (orderedSourcePos == null)
                        orderedSourcePos = new List<Vector2Int>(16);
                    else
                        orderedSourcePos.Clear();
                    ArrowChainUtility.CollectFullChain(sourceBoard, new Vector2Int(sx, sy), 0, sourceComponent);

                    if (sourceComponent.Count == 0)
                    {
                        visited.Add(sourceIdx);
                        continue;
                    }

                    sourceChains++;
                    foreach (int c in sourceComponent)
                        visited.Add(c);

                    if (!TryBuildStableOrderedChain(sourceBoard, sourceComponent, new Vector2Int(sx, sy), out var stableOrderedSourcePos))
                    {
                        orderedSource.Clear();
                        if (!TryOrderArrowComponentByConnectivity(sourceBoard, sourceComponent, orderedSource))
                        {
                            removedCells += sourceComponent.Count;
                            continue;
                        }
                    }
                    else
                    {
                        orderedSourcePos.Clear();
                        orderedSourcePos.AddRange(stableOrderedSourcePos);
                        orderedSource.Clear();
                        for (int p = 0; p < orderedSourcePos.Count; p++)
                        {
                            orderedSource.Add(orderedSourcePos[p].x + orderedSourcePos[p].y * sourceBoard.width);
                        }
                    }

                    if (orderedSource.Count < 2)
                    {
                        removedCells += orderedSource.Count;
                        continue;
                    }

                    foreach (int currentSourceIdx in orderedSource)
                    {
                        int cx = currentSourceIdx % sourceBoard.width;
                        int cy = currentSourceIdx / sourceBoard.width;
                        int tx = cx + offset.x;
                        int ty = cy + offset.y;

                        bool keep = tx >= 0 && tx < targetBoard.width &&
                                    ty >= 0 && ty < targetBoard.height;
                        if (keep)
                        {
                            int targetIdx = tx + ty * targetBoard.width;
                            keep = maskCanSpawn[targetIdx];
                            if (keep)
                            {
                                segment.Add(targetIdx);
                            }
                        }

                        if (!keep)
                        {
                            FlushSegment();
                        }
                    }

                    FlushSegment();
                }
            }

            keptTileCount = keptTileLocal;
            details = $"mask-preserve: sourceChains={sourceChains}, keptSegments={keptSegments}, keptCells={keptTileCount}, removedCells={removedCells}";
            return keptTileCount > 0;
        }

        private static bool TryOrderArrowComponentByConnectivity(
            BoardState board,
            HashSet<int> component,
            List<int> ordered)
        {
            if (board == null || component == null || component.Count < 2 || ordered == null)
                return false;

            ordered.Clear();

            int start = -1;
            int endpointCount = 0;
            foreach (int idx in component)
            {
                if (board.tiles[idx].type != TileType.Arrow)
                    return false;

                int degree = CountArrowAdjacency(board, idx, component);
                if (degree == 1)
                {
                    endpointCount++;
                    if (start < 0)
                        start = idx;
                }
                else if (degree == 0)
                {
                    return false;
                }
                else if (degree > 2)
                {
                    return false;
                }
            }

            bool isLoop = endpointCount == 0;
            if (!isLoop && endpointCount != 2)
                return false;

            var used = new HashSet<int>(Mathf.Max(4, component.Count));
            int current = isLoop ? GetFirstFromSet(component) : start;
            int previous = -1;

            while (ordered.Count < component.Count)
            {
                ordered.Add(current);
                used.Add(current);
                if (ordered.Count >= component.Count)
                    break;

                int next = FindNextInComponent(board, component, current, previous, used);
                if (next < 0)
                    return false;

                previous = current;
                current = next;
            }

            return ordered.Count == component.Count;
        }

        private static int FindNextInComponent(
            BoardState board,
            HashSet<int> component,
            int current,
            int previous,
            HashSet<int> used)
        {
            int cx = current % board.width;
            int cy = current / board.width;

            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };

            for (int dir = 0; dir < 4; dir++)
            {
                int nx = cx + dx[dir];
                int ny = cy + dy[dir];

                if (!board.InBounds(nx, ny))
                    continue;

                int nb = board.Index(nx, ny);
                if (!component.Contains(nb))
                    continue;

                if (nb == previous)
                    continue;

                if (!used.Contains(nb))
                    return nb;
            }

            return -1;
        }

        private static int GetFirstFromSet(HashSet<int> source)
        {
            foreach (int v in source)
                return v;

            return -1;
        }

        private static long MakeUndirectedEdgeKey(int a, int b)
        {
            int min = Math.Min(a, b);
            int max = Math.Max(a, b);
            return ((long)min << 32) | (uint)max;
        }

        private static int ScoreChainDirectionCompatibility(BoardState board, IList<int> ordered)
        {
            int score = 0;
            int n = ordered.Count;
            if (board == null || n < 2)
                return 0;

            for (int i = 0; i < n; i++)
            {
                int curIdx = ordered[i];
                int curX = curIdx % board.width;
                int curY = curIdx / board.width;

                Vector2Int outDelta;
                if (i < n - 1)
                {
                    int nextIdx = ordered[i + 1];
                    int nextX = nextIdx % board.width;
                    int nextY = nextIdx / board.width;
                    outDelta = new Vector2Int(nextX - curX, nextY - curY);
                }
                else
                {
                    int prevIdx = ordered[i - 1];
                    int prevX = prevIdx % board.width;
                    int prevY = prevIdx / board.width;
                    outDelta = new Vector2Int(curX - prevX, curY - prevY);
                }

                if (!TryDirFromOffset(outDelta, out Dir outDir))
                    return 0;

                Vector2Int inDelta;
                if (i == 0)
                {
                    inDelta = -outDelta;
                }
                else
                {
                    int prevIdx = ordered[i - 1];
                    int prevX = prevIdx % board.width;
                    int prevY = prevIdx / board.width;
                    inDelta = new Vector2Int(prevX - curX, prevY - curY);
                }

                var current = board.tiles[curIdx];
                if (current.type != TileType.Arrow)
                    return 0;

                if (!TryDirFromOffset(inDelta, out Dir inDir))
                    return 0;

                if (current.arrow.outDir == outDir)
                    score++;
                if (current.arrow.inDir == inDir)
                    score++;
            }

            return score;
        }

        private static bool ApplyOrderedIndexChain(BoardState board, IList<int> ordered)
        {
            if (board == null || ordered == null || ordered.Count < 2)
                return false;

            int n = ordered.Count;
            for (int i = 0; i < n; i++)
            {
                int curIdx = ordered[i];
                int curX = curIdx % board.width;
                int curY = curIdx / board.width;

                int outX;
                int outY;
                if (i < n - 1)
                {
                    int nextIdx = ordered[i + 1];
                    int nextX = nextIdx % board.width;
                    int nextY = nextIdx / board.width;
                    outX = nextX - curX;
                    outY = nextY - curY;
                }
                else
                {
                    int prevIdx = ordered[n - 2];
                    int prevX = prevIdx % board.width;
                    int prevY = prevIdx / board.width;
                    outX = curX - prevX;
                    outY = curY - prevY;
                }

                int inPrevIdx = (i > 0) ? ordered[i - 1] : -1;
                int inPrevX = (i > 0) ? inPrevIdx % board.width : 0;
                int inPrevY = (i > 0) ? inPrevIdx / board.width : 0;

                if (!TryDirFromOffset(new Vector2Int(outX, outY), out Dir outDir))
                    return false;

                Dir inDir = Opposite(outDir);
                if (i > 0)
                {
                    if (!TryDirFromOffset(new Vector2Int(inPrevX - curX, inPrevY - curY), out inDir))
                        return false;
                }

                board.tiles[curIdx] = TileState.Arrow(inDir, outDir);
            }

            return true;
        }

        private static bool TryOrderArrowComponentByAdjacency(
            BoardState board,
            IList<int> component,
            List<int> ordered)
        {
            ordered.Clear();

            if (board == null || component == null || component.Count == 0 || ordered == null)
                return false;

            var componentSet = new HashSet<int>(component);
            int start = -1;
            int endpointCount = 0;

            for (int i = 0; i < component.Count; i++)
            {
                int idx = component[i];
                int degree = CountArrowAdjacency(board, idx, componentSet);
                if (degree == 1 && start < 0)
                {
                    start = idx;
                }
                if (degree == 1)
                    endpointCount++;

                if (degree > 2)
                    return false;
            }

            if (start < 0)
                return false;
            if (endpointCount == 1)
                return false;

            var pathSet = new HashSet<int>(component.Count);
            int current = start;
            int previous = -1;
            int guard = component.Count + 4;

            while (ordered.Count < component.Count && guard-- > 0)
            {
                ordered.Add(current);
                pathSet.Add(current);

                int next = -1;
                int x = current % board.width;
                int y = current / board.width;

                TryPickNextNeighbor(board, componentSet, current, previous, x + 1, y, ref next);
                if (next < 0)
                    TryPickNextNeighbor(board, componentSet, current, previous, x - 1, y, ref next);
                if (next < 0)
                    TryPickNextNeighbor(board, componentSet, current, previous, x, y + 1, ref next);
                if (next < 0)
                    TryPickNextNeighbor(board, componentSet, current, previous, x, y - 1, ref next);

                if (next < 0)
                    break;

                if (pathSet.Contains(next) && ordered.Count + 1 < component.Count)
                    break;

                previous = current;
                current = next;
            }

            if (ordered.Count != component.Count)
                return false;

            return true;
        }

        private static int CountArrowAdjacency(BoardState board, int index, HashSet<int> componentSet)
        {
            if (board == null || board.tiles == null || board.tiles[index].type != TileType.Arrow || componentSet == null || !componentSet.Contains(index))
                return 0;

            int x = index % board.width;
            int y = index / board.width;
            int degree = 0;

            if (HasArrowIndex(board, x + 1, y, componentSet)) degree++;
            if (HasArrowIndex(board, x - 1, y, componentSet)) degree++;
            if (HasArrowIndex(board, x, y + 1, componentSet)) degree++;
            if (HasArrowIndex(board, x, y - 1, componentSet)) degree++;

            return degree;
        }

        private static void TryPickNextNeighbor(
            BoardState board,
            HashSet<int> componentSet,
            int current,
            int previous,
            int x,
            int y,
            ref int next)
        {
            if (!board.InBounds(x, y) || next >= 0)
                return;

            int idx = board.Index(x, y);
            if (idx == previous || !componentSet.Contains(idx))
                return;

            next = idx;
        }

        private static bool HasArrowIndex(BoardState board, int x, int y, HashSet<int> componentSet)
        {
            if (!board.InBounds(x, y))
                return false;

            int idx = board.Index(x, y);
            return componentSet.Contains(idx) && board.tiles[idx].type == TileType.Arrow;
        }

        private static void EnqueueArrowNeighborIfNeeded(
            int x,
            int y,
            BoardState board,
            HashSet<int> visited,
            Queue<int> queue)
        {
            if (!board.InBounds(x, y))
                return;

            int idx = board.Index(x, y);
            if (!visited.Add(idx))
                return;

            if (board.tiles[idx].type == TileType.Arrow)
            {
                queue.Enqueue(idx);
            }
        }

        private static void ClearBoardCellsFromIndices(BoardState board, IList<int> indices)
        {
            if (board == null || board.tiles == null || indices == null)
                return;

            for (int i = 0; i < indices.Count; i++)
            {
                int idx = indices[i];
                if (idx >= 0 && idx < board.tiles.Length)
                    board.tiles[idx] = TileState.Empty();
            }
        }

        private static bool TryDirFromOffset(Vector2Int offset, out Dir dir)
        {
            if (offset.x == 0 && offset.y == 1) { dir = Dir.Up; return true; }
            if (offset.x == 1 && offset.y == 0) { dir = Dir.Right; return true; }
            if (offset.x == 0 && offset.y == -1) { dir = Dir.Down; return true; }
            if (offset.x == -1 && offset.y == 0) { dir = Dir.Left; return true; }

            dir = default;
            return false;
        }

        private static Dir DirFromOffset(Vector2Int offset)
        {
            if (TryDirFromOffset(offset, out var dir))
                return dir;
            return Dir.Up;
        }

        private static Dir Opposite(Dir dir) => (Dir)(((int)dir + 2) & 3);

        private static List<Vector2Int> BuildAutoOffsets(int sourceW, int sourceH, int targetW, int targetH)
        {
            int minX = Mathf.Min(0, targetW - sourceW);
            int maxX = Mathf.Max(0, targetW - sourceW);
            int minY = Mathf.Min(0, targetH - sourceH);
            int maxY = Mathf.Max(0, targetH - sourceH);

            var all = new List<Vector2Int>((maxX - minX + 1) * (maxY - minY + 1));
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    all.Add(new Vector2Int(x, y));
                }
            }

            float centerX = (targetW - sourceW) * 0.5f;
            float centerY = (targetH - sourceH) * 0.5f;
            all.Sort((a, b) =>
            {
                float da = Vector2.Distance(new Vector2(a.x, a.y), new Vector2(centerX, centerY));
                float db = Vector2.Distance(new Vector2(b.x, b.y), new Vector2(centerX, centerY));
                return da.CompareTo(db);
            });

            return all;
        }

        private static LevelSpec BuildSpec(LevelDefinition sourceDef, int w, int h, int seed, bool[] canSpawn)
        {
            return new LevelSpec
            {
                width = w,
                height = h,
                seed = seed,
                arrowFill = sourceDef != null ? Mathf.Clamp01(sourceDef.generation.arrowCoverage) : 0.95f,
                minPathLen = sourceDef != null ? Mathf.Max(2, sourceDef.generation.minPathLen) : BoardController.DefaultMinArrowCellCount,
                maxPathLen = sourceDef != null ? Mathf.Max(2, sourceDef.generation.maxPathLength) : 20,
                twistiness = sourceDef != null ? sourceDef.generation.twistiness : 0.5f,
                canSpawnHere = canSpawn
            };
        }

        private static bool ValidateGeneratedBoard(ref BoardState board, int greedyMoves, out string validationDetails)
        {
            validationDetails = string.Empty;

            var authored = ConvertBoardToAuthored(board, out string warning);
            if (!string.IsNullOrWhiteSpace(warning))
            {
                validationDetails = $" | AuthoringWarning={warning}";
            }

            if (!EnsureAuthoredDataBuildable(
                board,
                authored,
                out var sanitizedAuthored,
                out var authoredBoard,
                out string authoredDetails))
            {
                validationDetails = $"{validationDetails} | {authoredDetails}";
                return false;
            }

            authored = sanitizedAuthored;

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            if (!GreedyValidator.TryClearAllByGreedy(authoredBoard, rules, greedyMoves, out _))
            {
                string authorRepair = string.Empty;
                BoardState repaired = authoredBoard;
                if (!EnsureBoardGreedyFriendly(
                    ref repaired,
                    greedyMoves,
                    null,
                    0,
                    allowFullChainRemoval: true,
                    allowChainClearFallback: true,
                    trimPerChainLimit: kGreedyTrimPerChain,
                    candidateEvalCap: 0,
                    timeBudgetMs: 0,
                    out authorRepair))
                {
                    validationDetails = $"{validationDetails} | AuthoringGreedyFail";
                    return false;
                }

                authoredBoard = repaired;
                board = repaired;
                if (!string.IsNullOrWhiteSpace(authoredDetails))
                {
                    validationDetails = $"{validationDetails} | {authoredDetails}";
                }
                validationDetails = $"{validationDetails} | {authorRepair}";
            }
            else if (!string.IsNullOrWhiteSpace(authoredDetails))
            {
                validationDetails = $"{validationDetails} | {authoredDetails}";
            }

            if (!string.IsNullOrWhiteSpace(authoredDetails) && authored != null)
            {
                validationDetails = $"{validationDetails} | AuthoringData={GetAuthoredArrowCount(authored)}";
            }

            return true;
        }

        private static bool TryValidateAuthoredGreedyOnly(ref BoardState board, int greedyMoves, out string validationDetails)
        {
            validationDetails = string.Empty;
            if (board == null || board.tiles == null)
            {
                validationDetails = "AuthoringBoardNull";
                return false;
            }

            var authored = ConvertBoardToAuthored(board, out string warning);
            if (!string.IsNullOrWhiteSpace(warning))
                validationDetails = $"AuthoringWarning={warning}";

            if (!EnsureAuthoredDataBuildable(
                board,
                authored,
                out _,
                out var authoredBoard,
                out string authoredDetails))
            {
                validationDetails = string.IsNullOrWhiteSpace(validationDetails)
                    ? authoredDetails
                    : $"{validationDetails} | {authoredDetails}";
                return false;
            }

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            if (!GreedyValidator.TryClearAllByGreedy(authoredBoard, rules, greedyMoves, out _))
            {
                validationDetails = string.IsNullOrWhiteSpace(validationDetails)
                    ? "AuthoringGreedyFail"
                    : $"{validationDetails} | AuthoringGreedyFail";
                if (!string.IsNullOrWhiteSpace(authoredDetails))
                    validationDetails = $"{validationDetails} | {authoredDetails}";
                return false;
            }

            board = authoredBoard;
            string passDetails = $"AuthoringGreedyPass authoredFill={CountArrowTiles(authoredBoard)}";
            if (!string.IsNullOrWhiteSpace(authoredDetails))
                passDetails = $"{authoredDetails} | {passDetails}";

            validationDetails = string.IsNullOrWhiteSpace(validationDetails)
                ? passDetails
                : $"{validationDetails} | {passDetails}";
            return true;
        }

        private static bool EnsureBoardGreedyFriendly(ref BoardState board, int greedyMoves, out string repairDetails)
        {
            return EnsureBoardGreedyFriendly(
                ref board,
                greedyMoves,
                null,
                0,
                allowFullChainRemoval: true,
                allowChainClearFallback: true,
                trimPerChainLimit: kGreedyTrimPerChain,
                candidateEvalCap: 0,
                timeBudgetMs: 0,
                out repairDetails);
        }

        private static bool TryGreedyCoreTakeoutRefillRescue(
            BoardState sourceBoard,
            bool[] maskCanSpawn,
            int greedyMoves,
            int targetFill,
            int minAcceptFill,
            out BoardState rescuedBoard,
            out string details)
        {
            rescuedBoard = null;
            details = "core-takeout-refill: no action";

            if (sourceBoard == null || sourceBoard.tiles == null || maskCanSpawn == null || maskCanSpawn.Length != sourceBoard.width * sourceBoard.height)
            {
                details = "core-takeout-refill: invalid input";
                return false;
            }

            if (!TryCollectGreedyStuckChains(sourceBoard, greedyMoves, out var stuckChains, out var dependencyCycleHeads, out var dependencyCycleGroups, out string stuckDetails) || stuckChains.Count == 0)
            {
                details = $"core-takeout-refill: no stuck chains | {stuckDetails}";
                return false;
            }

            string iterativeReject = string.Empty;
            if (TryIterativeDependencyTakeout(
                sourceBoard,
                maskCanSpawn,
                greedyMoves,
                minAcceptFill,
                targetFill,
                out rescuedBoard,
                out string iterativeDetails))
            {
                details = $"{iterativeDetails} | initialCore={stuckDetails}";
                return true;
            }
            iterativeReject = iterativeDetails;

            if (dependencyCycleGroups != null && dependencyCycleGroups.Count > 0)
            {
                var testedCycleCombos = new HashSet<string>();
                dependencyCycleGroups.Sort((a, b) =>
                {
                    int lenA = a?.Length ?? 0;
                    int lenB = b?.Length ?? 0;
                    return lenB.CompareTo(lenA);
                });

                for (int i = 0; i < dependencyCycleGroups.Count; i++)
                {
                    int[] group = dependencyCycleGroups[i];
                    if (group == null || group.Length == 0 || group.Length > kGreedyCoreTakeoutMaxChains)
                        continue;

                    Array.Sort(group);
                    string key = string.Join(",", group);
                    if (!testedCycleCombos.Add(key))
                        continue;

                    bool validGroup = true;
                    for (int g = 0; g < group.Length; g++)
                    {
                        if (group[g] < 0 || group[g] >= stuckChains.Count)
                        {
                            validGroup = false;
                            break;
                        }
                    }

                    if (!validGroup)
                        continue;

                    var cycleCombo = new List<int>(group);
                    int cycleBestFill = -1;
                    string cycleReject = string.Empty;
                    if (TryEvaluateCoreTakeoutCombination(
                        sourceBoard,
                        stuckChains,
                        cycleCombo,
                        maskCanSpawn,
                        greedyMoves,
                        minAcceptFill,
                        targetFill,
                        out rescuedBoard,
                        out string cycleDetails,
                        ref cycleBestFill,
                        ref cycleReject))
                    {
                        details = $"{cycleDetails} | dependencyCycleTakeout group=[{key}], cycleHeads=[{string.Join(",", dependencyCycleHeads)}] | {stuckDetails}";
                        return true;
                    }
                }

                int cycleGroupTested = 0;
                BoardState cycleFoundBoard = null;
                string cycleFoundDetails = string.Empty;
                var selectedCycleGroups = new List<int>();

                bool SearchCycleGroups(int start, int selectedChainCount)
                {
                    if (selectedCycleGroups.Count > 0)
                    {
                        var merged = new SortedSet<int>();
                        for (int s = 0; s < selectedCycleGroups.Count; s++)
                        {
                            int groupIndex = selectedCycleGroups[s];
                            if (groupIndex < 0 || groupIndex >= dependencyCycleGroups.Count)
                                continue;

                            int[] group = dependencyCycleGroups[groupIndex];
                            if (group == null)
                                continue;

                            for (int g = 0; g < group.Length; g++)
                                merged.Add(group[g]);
                        }

                        if (merged.Count > 0 && merged.Count <= kGreedyCoreCycleTakeoutMaxChains)
                        {
                            string key = string.Join(",", merged);
                            if (testedCycleCombos.Add(key))
                            {
                                cycleGroupTested++;
                                var cycleCombo = new List<int>(merged);
                                int cycleBestFill = -1;
                                string cycleReject = string.Empty;
                                if (TryEvaluateCoreTakeoutCombination(
                                    sourceBoard,
                                    stuckChains,
                                    cycleCombo,
                                    maskCanSpawn,
                                    greedyMoves,
                                    minAcceptFill,
                                    targetFill,
                                    out cycleFoundBoard,
                                    out string cycleDetails,
                                    ref cycleBestFill,
                                    ref cycleReject))
                                {
                                    cycleFoundDetails = $"{cycleDetails} | dependencyCycleTakeout group=[{key}], cycleGroupSearch={cycleGroupTested}, cycleHeads=[{string.Join(",", dependencyCycleHeads)}] | {stuckDetails}";
                                    return true;
                                }
                            }
                        }
                    }

                    for (int i = start; i < dependencyCycleGroups.Count; i++)
                    {
                        int[] group = dependencyCycleGroups[i];
                        int groupCount = group?.Length ?? 0;
                        if (groupCount <= 0 || selectedChainCount + groupCount > kGreedyCoreCycleTakeoutMaxChains)
                            continue;

                        selectedCycleGroups.Add(i);
                        if (SearchCycleGroups(i + 1, selectedChainCount + groupCount))
                            return true;
                        selectedCycleGroups.RemoveAt(selectedCycleGroups.Count - 1);
                    }

                    return false;
                }

                if (SearchCycleGroups(0, 0))
                {
                    rescuedBoard = cycleFoundBoard;
                    details = cycleFoundDetails;
                    return true;
                }
            }

            stuckChains.Sort((a, b) =>
            {
                int scoreA = ScoreCoreTakeoutChain(sourceBoard, a, maskCanSpawn);
                int scoreB = ScoreCoreTakeoutChain(sourceBoard, b, maskCanSpawn);
                int cmp = scoreB.CompareTo(scoreA);
                if (cmp != 0)
                    return cmp;
                int lenA = a?.Length ?? 9999;
                int lenB = b?.Length ?? 9999;
                return lenA.CompareTo(lenB);
            });

            int initialFill = CountArrowTiles(sourceBoard);
            int acceptFill = Mathf.Max(1, Mathf.Min(minAcceptFill, targetFill));
            string bestReject = string.Empty;
            int searchCount = Mathf.Min(kGreedyCoreTakeoutSearchChainCap, stuckChains.Count);
            int maxTakeout = Mathf.Min(kGreedyCoreTakeoutMaxChains, searchCount);
            int tested = 0;
            int bestFill = -1;
            var combo = new List<int>(kGreedyCoreTakeoutMaxChains);
            BoardState foundBoard = null;
            string foundDetails = string.Empty;

            bool Search(int start, int depth, int targetDepth)
            {
                if (depth == targetDepth)
                {
                    if (tested >= kGreedyCoreTakeoutCandidateCap)
                    {
                        bestReject = string.IsNullOrWhiteSpace(bestReject)
                            ? $"candidate cap reached ({kGreedyCoreTakeoutCandidateCap})"
                            : $"{bestReject} | candidate cap reached ({kGreedyCoreTakeoutCandidateCap})";
                        return false;
                    }

                    tested++;
                    return TryEvaluateCoreTakeoutCombination(
                        sourceBoard,
                        stuckChains,
                        combo,
                        maskCanSpawn,
                        greedyMoves,
                        acceptFill,
                        targetFill,
                        out foundBoard,
                        out foundDetails,
                        ref bestFill,
                        ref bestReject);
                }

                int remaining = targetDepth - depth;
                for (int i = start; i <= searchCount - remaining; i++)
                {
                    if (tested >= kGreedyCoreTakeoutCandidateCap)
                        return false;

                    combo.Add(i);
                    if (Search(i + 1, depth + 1, targetDepth))
                        return true;
                    combo.RemoveAt(combo.Count - 1);
                }

                return false;
            }

            for (int takeoutCount = 1; takeoutCount <= maxTakeout; takeoutCount++)
            {
                combo.Clear();
                if (Search(0, 0, takeoutCount))
                {
                    rescuedBoard = foundBoard;
                    details = $"{foundDetails} | coreSearch tested={tested}, searchChains={searchCount}, totalStuck={stuckChains.Count}, initialFill={initialFill} | {stuckDetails}";
                    return true;
                }
            }

            details = $"core-takeout-greedy: failed initialFill={initialFill}, target={targetFill}, minAccept={acceptFill}, stuck={stuckChains.Count}, searchChains={searchCount}, tested={tested}/{kGreedyCoreTakeoutCandidateCap}, bestFill={bestFill} | {stuckDetails} | {iterativeReject} | {bestReject}";
            return false;
        }

        private static bool TryIterativeDependencyTakeout(
            BoardState sourceBoard,
            bool[] maskCanSpawn,
            int greedyMoves,
            int minAcceptFill,
            int targetFill,
            out BoardState rescuedBoard,
            out string details)
        {
            rescuedBoard = null;
            details = "core-iterative-takeout: no action";
            BoardState candidate = CloneBoard(sourceBoard);
            if (candidate == null)
            {
                details = "core-iterative-takeout: clone failed";
                return false;
            }

            int totalRemovedChains = 0;
            int totalRemovedCells = 0;
            int bestFill = CountArrowTiles(candidate);
            var passReports = new List<string>();

            for (int pass = 0; pass < kGreedyCoreIterativeTakeoutMaxPasses; pass++)
            {
                BoardState accepted = candidate;
                if (TryAcceptCoreRescueCandidate(ref accepted, maskCanSpawn, greedyMoves, minAcceptFill, requireCleanGeometry: false, out string acceptDetails))
                {
                    rescuedBoard = accepted;
                    details = $"core-iterative-takeout: pass={pass}, removedChains={totalRemovedChains}, removedCells={totalRemovedCells}, fill={CountArrowTiles(accepted)}/{targetFill} | {string.Join(" / ", passReports)} | {acceptDetails}";
                    return true;
                }

                if (!TryCollectGreedyStuckChains(candidate, greedyMoves, out var stuckChains, out var cycleHeads, out var cycleGroups, out string stuckDetails) || stuckChains.Count == 0)
                {
                    passReports.Add($"pass={pass}: no stuck chains | {stuckDetails} | {acceptDetails}");
                    break;
                }

                var removeChainIndexes = new SortedSet<int>();
                if (cycleGroups != null && cycleGroups.Count > 0)
                {
                    cycleGroups.Sort((a, b) =>
                    {
                        int lenA = a?.Length ?? 0;
                        int lenB = b?.Length ?? 0;
                        return lenB.CompareTo(lenA);
                    });

                    for (int i = 0; i < cycleGroups.Count && removeChainIndexes.Count < kGreedyCoreIterativeTakeoutMaxChainsPerPass; i++)
                    {
                        int[] group = cycleGroups[i];
                        if (group == null || group.Length == 0)
                            continue;

                        Array.Sort(group);
                        int representative = group[0];
                        if (representative >= 0 && representative < stuckChains.Count)
                            removeChainIndexes.Add(representative);
                    }
                }

                if (removeChainIndexes.Count == 0)
                {
                    var ranked = new List<int>(stuckChains.Count);
                    for (int i = 0; i < stuckChains.Count; i++)
                        ranked.Add(i);

                    ranked.Sort((a, b) =>
                    {
                        int scoreA = ScoreCoreTakeoutChain(candidate, stuckChains[a], maskCanSpawn);
                        int scoreB = ScoreCoreTakeoutChain(candidate, stuckChains[b], maskCanSpawn);
                        return scoreB.CompareTo(scoreA);
                    });

                    for (int i = 0; i < ranked.Count && removeChainIndexes.Count < 1; i++)
                        removeChainIndexes.Add(ranked[i]);
                }

                if (removeChainIndexes.Count == 0)
                {
                    passReports.Add($"pass={pass}: no removable core | {stuckDetails} | {acceptDetails}");
                    break;
                }

                int removedCells = 0;
                foreach (int chainIndex in removeChainIndexes)
                {
                    if (chainIndex < 0 || chainIndex >= stuckChains.Count)
                        continue;

                    int[] chain = stuckChains[chainIndex];
                    if (chain == null)
                        continue;

                    for (int c = 0; c < chain.Length; c++)
                    {
                        int idx = chain[c];
                        if (idx >= 0 && idx < candidate.tiles.Length && candidate.tiles[idx].type == TileType.Arrow)
                        {
                            candidate.tiles[idx] = TileState.Empty();
                            removedCells++;
                        }
                    }
                }

                if (removedCells <= 0)
                {
                    passReports.Add($"pass={pass}: removed no cells chains=[{string.Join(",", removeChainIndexes)}] | {stuckDetails}");
                    break;
                }

                totalRemovedChains += removeChainIndexes.Count;
                totalRemovedCells += removedCells;

                ApplyPreGreedyGeometryFix(
                    ref candidate,
                    maskCanSpawn,
                    maxPasses: 6,
                    trimPerChainLimit: 1,
                    out int geometryRemoved,
                    out string geometryDetails);

                int fill = CountArrowTiles(candidate);
                bestFill = Mathf.Max(bestFill, fill);
                passReports.Add($"pass={pass}: remove=[{string.Join(",", removeChainIndexes)}], cells={removedCells}, geoRemoved={geometryRemoved}, fill={fill}/{targetFill}, cycles=[{string.Join(",", cycleHeads)}] | {geometryDetails} | {stuckDetails}");

                if (fill < minAcceptFill)
                {
                    passReports.Add($"pass={pass}: stop fill {fill} < min {minAcceptFill}");
                    break;
                }
            }

            details = $"core-iterative-takeout: failed passes={passReports.Count}, removedChains={totalRemovedChains}, removedCells={totalRemovedCells}, bestFill={bestFill}/{targetFill} | {string.Join(" / ", passReports)}";
            return false;
        }

        private static bool TryEvaluateCoreTakeoutCombination(
            BoardState sourceBoard,
            List<int[]> stuckChains,
            List<int> chainIndexes,
            bool[] maskCanSpawn,
            int greedyMoves,
            int minAcceptFill,
            int targetFill,
            out BoardState rescuedBoard,
            out string details,
            ref int bestFill,
            ref string bestReject)
        {
            rescuedBoard = null;
            details = string.Empty;
            var candidate = CloneBoard(sourceBoard);
            if (candidate == null || stuckChains == null || chainIndexes == null || chainIndexes.Count == 0)
                return false;

            int removed = 0;
            int removedCells = 0;
            for (int i = 0; i < chainIndexes.Count; i++)
            {
                int chainIndex = chainIndexes[i];
                if (chainIndex < 0 || chainIndex >= stuckChains.Count)
                    continue;

                int[] chain = stuckChains[chainIndex];
                if (chain == null)
                    continue;

                removed++;
                for (int c = 0; c < chain.Length; c++)
                {
                    int idx = chain[c];
                    if (idx >= 0 && idx < candidate.tiles.Length && candidate.tiles[idx].type == TileType.Arrow)
                    {
                        candidate.tiles[idx] = TileState.Empty();
                        removedCells++;
                    }
                }
            }

            if (removedCells <= 0)
                return false;

            ApplyPreGreedyGeometryFix(
                ref candidate,
                maskCanSpawn,
                maxPasses: 8,
                trimPerChainLimit: 2,
                out int geometryRemoved,
                out string geometryDetails);

            int fill = CountArrowTiles(candidate);
            if (fill > bestFill)
            {
                bestFill = fill;
                bestReject = $"combo=[{string.Join(",", chainIndexes)}], removedChains={removed}, removedCells={removedCells}, fill={fill}/{targetFill}";
            }

            if (!TryAcceptCoreRescueCandidate(ref candidate, maskCanSpawn, greedyMoves, minAcceptFill, requireCleanGeometry: false, out string acceptDetails))
            {
                bestReject = $"{bestReject} | {acceptDetails}";
                return false;
            }

            rescuedBoard = candidate;
            details = $"core-takeout-greedy: combo=[{string.Join(",", chainIndexes)}], removedChains={removed}, removedCells={removedCells}, geometryRemoved={geometryRemoved}, fill={fill}/{targetFill} | {geometryDetails} | {acceptDetails}";
            return true;
        }

        private static bool TryAcceptCoreRescueCandidate(
            ref BoardState candidate,
            bool[] maskCanSpawn,
            int greedyMoves,
            int minFill,
            bool requireCleanGeometry,
            out string details)
        {
            details = string.Empty;
            if (candidate == null || candidate.tiles == null)
                return false;

            int fill = CountArrowTiles(candidate);
            if (fill < minFill)
            {
                details = $"strict-reject: fill {fill} < min {minFill}";
                return false;
            }

            int conflicts = CountGeometryConflicts(candidate, out int selfBlocked, out int opposite);
            if (conflicts > 0 && requireCleanGeometry)
            {
                details = $"strict-reject: geometry conflicts={conflicts}(self={selfBlocked},opposite={opposite})";
                return false;
            }

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            if (!GreedyValidator.TryClearAllByGreedy(candidate, rules, greedyMoves, out _))
            {
                details = $"strict-reject: greedy fail fill={fill}";
                return false;
            }

            var authoredCandidate = candidate;
            if (!TryValidateAuthoredGreedyOnly(ref authoredCandidate, greedyMoves, out string validationDetails))
            {
                details = $"strict-reject: authoring validation fail fill={fill}{(!string.IsNullOrWhiteSpace(validationDetails) ? $" | {validationDetails}" : string.Empty)}";
                return false;
            }

            int authoredFill = CountArrowTiles(authoredCandidate);
            if (authoredFill < minFill)
            {
                details = $"strict-reject: authored fill {authoredFill} < min {minFill}";
                return false;
            }

            candidate = authoredCandidate;

            details = conflicts > 0
                ? $"greedy-accept-with-geometry-debt: fill={authoredFill}, rawFill={fill}, conflicts={conflicts}(self={selfBlocked},opposite={opposite}), greedy=pass{(!string.IsNullOrWhiteSpace(validationDetails) ? $" | {validationDetails}" : string.Empty)}"
                : $"strict-accept: fill={authoredFill}, rawFill={fill}, geometry=0, greedy=pass{(!string.IsNullOrWhiteSpace(validationDetails) ? $" | {validationDetails}" : string.Empty)}";
            return true;
        }

        private static bool TryCollectGreedyStuckChains(
            BoardState sourceBoard,
            int greedyMoves,
            out List<int[]> stuckChains,
            out List<int> dependencyCycleHeads,
            out List<int[]> dependencyCycleGroups,
            out string details)
        {
            stuckChains = new List<int[]>();
            dependencyCycleHeads = new List<int>();
            dependencyCycleGroups = new List<int[]>();
            details = string.Empty;
            if (sourceBoard == null || sourceBoard.tiles == null)
            {
                details = "stuck-analysis invalid board";
                return false;
            }

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            var state = CloneBoard(sourceBoard);
            if (state == null)
            {
                details = "stuck-analysis clone failed";
                return false;
            }

            int step = 0;
            int lastCleared = 0;
            for (; step < greedyMoves; step++)
            {
                if (rules.IsSolved(state))
                {
                    details = $"stuck-analysis solvedAt={step}";
                    return false;
                }

                if (!TryFindBestGreedyMoveForAnalysis(state, rules, out Move bestMove, out int cleared))
                {
                    break;
                }

                if (!rules.TryApplyMove(state, bestMove, out var applied))
                    break;

                lastCleared = CountClearedByMove(applied);
                if (lastCleared <= 0)
                    break;
            }

            var visited = new HashSet<int>(Mathf.Max(1, state.width * state.height));
            var chainSet = new HashSet<int>();
            var ordered = new List<Vector2Int>();
            var chainFlags = new List<bool>();
            CollectMaskedChains(state, stuckChains, chainFlags, visited, chainSet, ordered);
            string dependencyDetails = AnalyzeStuckChainDependencies(state, stuckChains, dependencyCycleHeads, dependencyCycleGroups);
            details = $"stuck-analysis step={step}, lastCleared={lastCleared}, remainingChains={stuckChains.Count}, remainingArrows={CountArrowTiles(state)} | {dependencyDetails}";
            return stuckChains.Count > 0;
        }

        private static string AnalyzeStuckChainDependencies(BoardState state, List<int[]> stuckChains, List<int> cycleHeads, List<int[]> cycleGroups)
        {
            if (cycleHeads == null)
                cycleHeads = new List<int>();
            cycleHeads.Clear();
            if (cycleGroups == null)
                cycleGroups = new List<int[]>();
            cycleGroups.Clear();

            if (state == null || state.tiles == null || stuckChains == null || stuckChains.Count == 0)
                return "dependency=none";

            var indexToChain = new Dictionary<int, int>();
            for (int i = 0; i < stuckChains.Count; i++)
            {
                int[] chain = stuckChains[i];
                if (chain == null)
                    continue;

                for (int j = 0; j < chain.Length; j++)
                {
                    int idx = chain[j];
                    if (idx >= 0 && idx < state.tiles.Length && !indexToChain.ContainsKey(idx))
                        indexToChain[idx] = i;
                }
            }

            var dependency = new int[stuckChains.Count];
            for (int i = 0; i < dependency.Length; i++)
                dependency[i] = -1;

            for (int i = 0; i < stuckChains.Count; i++)
            {
                int[] chain = stuckChains[i];
                if (chain == null || chain.Length == 0)
                    continue;

                var blockers = new Dictionary<int, int>();
                for (int j = 0; j < chain.Length; j++)
                {
                    int idx = chain[j];
                    if (idx < 0 || idx >= state.tiles.Length || state.tiles[idx].type != TileType.Arrow)
                        continue;

                    if (!TryFindFirstGreedyBlockerOnPath(state, idx, indexToChain, i, out int blockerChain))
                        continue;

                    if (blockerChain < 0 || blockerChain >= stuckChains.Count)
                        continue;

                    blockers.TryGetValue(blockerChain, out int count);
                    blockers[blockerChain] = count + 1;
                }

                int bestBlocker = -1;
                int bestVote = 0;
                foreach (var kv in blockers)
                {
                    bool bestIsSelf = bestBlocker == i;
                    bool candidateIsSelf = kv.Key == i;
                    if (kv.Value > bestVote || (kv.Value == bestVote && bestIsSelf && !candidateIsSelf))
                    {
                        bestBlocker = kv.Key;
                        bestVote = kv.Value;
                    }
                }

                dependency[i] = bestBlocker;
            }

            var seenCycleKeys = new HashSet<string>();
            for (int start = 0; start < dependency.Length; start++)
            {
                var pathIndex = new Dictionary<int, int>();
                var path = new List<int>();
                int current = start;
                while (current >= 0 && current < dependency.Length)
                {
                    if (pathIndex.TryGetValue(current, out int cycleStart))
                    {
                        var cycle = path.GetRange(cycleStart, path.Count - cycleStart);
                        if (cycle.Count > 0)
                        {
                            cycle.Sort();
                            string key = string.Join(",", cycle);
                            if (seenCycleKeys.Add(key))
                            {
                                cycleHeads.Add(cycle[0]);
                                cycleGroups.Add(cycle.ToArray());
                            }
                        }
                        break;
                    }

                    pathIndex[current] = path.Count;
                    path.Add(current);
                    current = dependency[current];
                }
            }

            var edges = new List<string>();
            for (int i = 0; i < dependency.Length; i++)
                if (dependency[i] >= 0)
                    edges.Add($"{i}->{dependency[i]}");

            return $"dependencyEdges=[{string.Join(",", edges)}], cycles=[{string.Join(",", cycleHeads)}]";
        }

        private static bool TryFindFirstGreedyBlockerOnPath(
            BoardState state,
            int startIdx,
            Dictionary<int, int> indexToChain,
            int selfChain,
            out int blockerChain)
        {
            blockerChain = -1;
            if (state == null || state.tiles == null || indexToChain == null || startIdx < 0 || startIdx >= state.tiles.Length)
                return false;

            TileState startTile = state.tiles[startIdx];
            if (startTile.type != TileType.Arrow)
                return false;

            int x = startIdx % state.width;
            int y = startIdx / state.width;
            Dir entryDir = startTile.arrow.inDir;
            Dir travelDir = default;
            bool hasTravelDir = false;
            int maxSteps = 1 + state.width * state.height * 4;
            var seen = new HashSet<int>();

            for (int step = 0; step < maxSteps; step++)
            {
                if (!state.InBounds(x, y))
                    return false;

                int idx = state.Index(x, y);
                int key = (idx << 2) | ((int)entryDir & 3);
                if (!seen.Add(key))
                {
                    blockerChain = selfChain;
                    return true;
                }

                TileState tile = state.tiles[idx];
                if (tile.type == TileType.Block)
                    return false;

                if (tile.type == TileType.Empty)
                {
                    if (!hasTravelDir)
                        return false;

                    Vector2Int stepDir = DirToOffsetSafe(travelDir);
                    int nextX = x + stepDir.x;
                    int nextY = y + stepDir.y;
                    if (state.InBounds(nextX, nextY))
                    {
                        int nextIdx = state.Index(nextX, nextY);
                        if (state.tiles[nextIdx].type == TileType.Arrow)
                        {
                            if (indexToChain.TryGetValue(nextIdx, out blockerChain))
                                return true;

                            return false;
                        }
                    }

                    x = nextX;
                    y = nextY;
                    entryDir = Opposite(travelDir);
                    continue;
                }

                if (tile.arrow.inDir != entryDir)
                {
                    if (indexToChain.TryGetValue(idx, out blockerChain))
                        return true;

                    return false;
                }

                travelDir = tile.arrow.outDir;
                hasTravelDir = true;

                Vector2Int outStep = DirToOffsetSafe(travelDir);
                x += outStep.x;
                y += outStep.y;
                entryDir = Opposite(travelDir);
            }

            blockerChain = selfChain;
            return true;
        }

        private static bool TryGetChainHeadAndOutDir(BoardState state, int[] chain, out int headIdx, out Dir outDir)
        {
            headIdx = -1;
            outDir = default;
            if (state == null || state.tiles == null || chain == null || chain.Length < 2)
                return false;

            var set = new HashSet<int>(chain);
            if (!TryBuildStableOrderedChain(state, set, new Vector2Int(chain[0] % state.width, chain[0] / state.width), out var ordered))
                return false;
            if (ordered == null || ordered.Count < 2)
                return false;

            Vector2Int head = ordered[0];
            if (!state.InBounds(head.x, head.y))
                return false;

            headIdx = state.Index(head.x, head.y);
            if (state.tiles[headIdx].type != TileType.Arrow)
                return false;

            outDir = state.tiles[headIdx].arrow.outDir;
            return true;
        }

        private static int FindFirstBlockingChainOnRay(
            BoardState state,
            int headIdx,
            Dir outDir,
            Dictionary<int, int> indexToChain,
            int selfChain)
        {
            if (state == null || state.tiles == null || indexToChain == null || headIdx < 0 || headIdx >= state.tiles.Length)
                return -1;

            Vector2Int dir = DirToOffsetSafe(outDir);
            int x = headIdx % state.width + dir.x;
            int y = headIdx / state.width + dir.y;
            while (state.InBounds(x, y))
            {
                int idx = state.Index(x, y);
                TileState tile = state.tiles[idx];
                if (tile.type == TileType.Empty)
                {
                    x += dir.x;
                    y += dir.y;
                    continue;
                }

                if (tile.type != TileType.Arrow)
                    return -1;

                if (indexToChain.TryGetValue(idx, out int chain) && chain != selfChain)
                    return chain;

                return -1;
            }

            return -1;
        }

        private static bool TryFindBestGreedyMoveForAnalysis(
            BoardState state,
            IRuleset rules,
            out Move bestMove,
            out int bestScore)
        {
            bestMove = default;
            bestScore = 0;
            foreach (var move in rules.GetLegalMoves(state))
            {
                if (!rules.TryApplyMove(state, move, out var delta))
                    continue;

                int score = CountClearedByMove(delta);
                delta.Undo(state);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }

            return bestScore > 0;
        }

        private static int CountClearedByMove(MoveDelta delta)
        {
            if (delta == null || delta.changes == null)
                return 0;

            int cleared = 0;
            for (int i = 0; i < delta.changes.Count; i++)
            {
                var change = delta.changes[i];
                if (change.before.type == TileType.Arrow && change.after.type == TileType.Empty)
                    cleared++;
            }

            return cleared;
        }

        private static int ScoreCoreTakeoutChain(BoardState board, int[] chain, bool[] maskCanSpawn)
        {
            if (board == null || board.tiles == null || chain == null || chain.Length == 0)
                return int.MinValue;

            var set = new HashSet<int>(chain);
            var orderedPos = new List<Vector2Int>();
            var orderedIdxs = new List<int>();
            int score = 0;
            if (TryBuildStableOrderedChain(board, set, new Vector2Int(chain[0] % board.width, chain[0] / board.width), out orderedPos))
            {
                for (int i = 0; i < orderedPos.Count; i++)
                {
                    Vector2Int p = orderedPos[i];
                    if (board.InBounds(p.x, p.y))
                        orderedIdxs.Add(board.Index(p.x, p.y));
                }

                score += CountChainGeometryConflicts(board, set, orderedIdxs, maskCanSpawn) * 500;
            }

            bool useMask = maskCanSpawn != null && maskCanSpawn.Length == board.width * board.height;
            int boundary = 0;
            for (int i = 0; i < chain.Length; i++)
            {
                int idx = chain[i];
                if (idx < 0 || idx >= board.tiles.Length)
                    continue;

                int x = idx % board.width;
                int y = idx / board.width;
                if (x <= 1 || x >= board.width - 2 || y <= 1 || y >= board.height - 2)
                    boundary++;
                if (useMask && IsMaskBoundaryCell(idx, board.width, board.height, maskCanSpawn))
                    boundary++;
            }

            score += Mathf.Max(0, 40 - chain.Length) * 8;
            score -= boundary * 12;
            return score;
        }

        private static bool EnsureBoardGreedyFriendly(
            ref BoardState board,
            int greedyMoves,
            bool[] maskCanSpawn,
            int minKeepArrows,
            bool allowFullChainRemoval,
            bool allowChainClearFallback,
            int trimPerChainLimit,
            int candidateEvalCap,
            int timeBudgetMs,
            out string repairDetails)
        {
            repairDetails = string.Empty;
            if (board == null || board.tiles == null)
                return false;

            if (maskCanSpawn != null && maskCanSpawn.Length != board.width * board.height)
                maskCanSpawn = null;

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            if (GreedyValidator.TryClearAllByGreedy(board, rules, greedyMoves, out _))
                return true;

            var visited = new HashSet<int>(Mathf.Max(1, board.width * board.height));
            var chainSet = new HashSet<int>();
            var ordered = new List<Vector2Int>();
            var chains = new List<int[]>();
            var chainFlags = new List<bool>();

            int maxPasses = Math.Max(1, (board.width * board.height) / 24 + 1);
            int removedChains = 0;
            int removedArrows = 0;
            int pass;
            string lastMode = "none";
            int shapeBaseline = ComputeGreedyRepairShapeScore(board, maskCanSpawn);
            int trimCap = Mathf.Max(1, trimPerChainLimit);
            int evalCap = Mathf.Max(0, candidateEvalCap);
            int evalCount = 0;
            int budgetMs = Mathf.Max(0, timeBudgetMs);
            var budgetTimer = budgetMs > 0 ? System.Diagnostics.Stopwatch.StartNew() : null;
            bool budgetExceeded = false;

            bool CanEvaluateCandidate()
            {
                if (budgetExceeded)
                    return false;

                if (evalCap > 0 && evalCount >= evalCap)
                {
                    budgetExceeded = true;
                    lastMode = "candidate-eval-budget";
                    return false;
                }

                if (budgetTimer != null && budgetTimer.ElapsedMilliseconds >= budgetMs)
                {
                    budgetExceeded = true;
                    lastMode = "candidate-time-budget";
                    return false;
                }

                return true;
            }

            bool TryCandidateGreedy(BoardState candidateBoard)
            {
                if (candidateBoard == null || !CanEvaluateCandidate())
                    return false;

                evalCount++;
                return GreedyValidator.TryClearAllByGreedy(candidateBoard, rules, greedyMoves, out _);
            }

            bool hasLoopChain = false;
            for (pass = 0; pass < maxPasses; pass++)
            {
                if (GreedyValidator.TryClearAllByGreedy(board, rules, greedyMoves, out _))
                    break;

                if (!CollectMaskedChains(board, chains, chainFlags, visited, chainSet, ordered) || chains.Count == 0)
                    break;

                hasLoopChain = false;
                for (int i = 0; i < chainFlags.Count; i++)
                {
                    if (chainFlags[i])
                    {
                        hasLoopChain = true;
                        break;
                    }
                }

                BoardState bestCandidate = null;
                int bestRemoved = 0;
                int bestScore = int.MinValue;
                string bestMode = "none";

                for (int i = 0; i < chains.Count; i++)
                {
                    if (budgetExceeded)
                        break;

                    int[] chain = chains[i];
                    if (chain == null || chain.Length < 2)
                        continue;

                    bool isLoop = chainFlags.Count > i && chainFlags[i];

                    if (isLoop)
                    {
                        for (int removeAt = 0; removeAt < chain.Length; removeAt++)
                        {
                            if (budgetExceeded)
                                break;

                            if (TryBuildGreedyLoopSingleBreakCandidate(
                                board,
                                chain,
                                removeAt,
                                out BoardState loopCandidate,
                                out int loopRemoved))
                            {
                                if (TryCandidateGreedy(loopCandidate))
                                {
                                    int candidateFill = CountArrowTiles(loopCandidate);
                                    if (minKeepArrows > 0 && candidateFill < minKeepArrows)
                                    {
                                        continue;
                                    }

                                    int candidateShape = ComputeGreedyRepairShapeScore(loopCandidate, maskCanSpawn);
                                    int score = candidateFill * 1200 + (candidateShape - shapeBaseline) - loopRemoved * 180;
                                    if (score > bestScore)
                                    {
                                        bestScore = score;
                                        bestCandidate = loopCandidate;
                                        bestRemoved = loopRemoved;
                                        bestMode = "loop-single-break";
                                    }
                                }
                            }
                        }
                    }

                    if (!isLoop && chain.Length < 2)
                        continue;

                    // full removal candidate
                    if (allowFullChainRemoval &&
                        TryBuildGreedyRepairCandidate(
                            board,
                            chain,
                            isLoop,
                            0,
                            chain.Length,
                            out BoardState fullCandidate,
                            out int removedFull))
                    {
                        if (TryCandidateGreedy(fullCandidate))
                        {
                            int candidateFill = CountArrowTiles(fullCandidate);
                            if (minKeepArrows > 0 && candidateFill < minKeepArrows)
                            {
                                continue;
                            }

                            int candidateShape = ComputeGreedyRepairShapeScore(fullCandidate, maskCanSpawn);
                            int score = ComputeGreedyRepairCandidateScore(candidateFill, shapeBaseline, candidateShape, removedFull);

                            if (score > bestScore)
                            {
                                bestScore = score;
                                bestCandidate = fullCandidate;
                                bestRemoved = removedFull;
                                bestMode = isLoop ? "loop-full" : "full";
                            }
                        }
                    }

                    // trim one side for non-loop chains before a full clear
                    if (!isLoop)
                    {
                        int maxTrim = Mathf.Min(trimCap, chain.Length - 2);
                        for (int trim = 1; trim <= maxTrim; trim++)
                        {
                            if (budgetExceeded)
                                break;

                            if (TryBuildGreedyRepairCandidate(
                                board,
                                chain,
                                isLoop,
                                trim,
                                0,
                                out BoardState headTrimCandidate,
                                out int headRemoved))
                            {
                                if (TryCandidateGreedy(headTrimCandidate))
                                {
                                    int candidateFill = CountArrowTiles(headTrimCandidate);
                                    if (minKeepArrows > 0 && candidateFill < minKeepArrows)
                                    {
                                        continue;
                                    }

                                    int candidateShape = ComputeGreedyRepairShapeScore(headTrimCandidate, maskCanSpawn);
                                    int score = ComputeGreedyRepairCandidateScore(candidateFill, shapeBaseline, candidateShape, headRemoved);
                                    if (score > bestScore)
                                    {
                                        bestScore = score;
                                        bestCandidate = headTrimCandidate;
                                        bestRemoved = headRemoved;
                                        bestMode = "trim-head";
                                    }
                                }
                            }

                            if (TryBuildGreedyRepairCandidate(
                                board,
                                chain,
                                isLoop,
                                0,
                                trim,
                                out BoardState tailTrimCandidate,
                                out int tailRemoved))
                            {
                                if (TryCandidateGreedy(tailTrimCandidate))
                                {
                                    int candidateFill = CountArrowTiles(tailTrimCandidate);
                                    if (minKeepArrows > 0 && candidateFill < minKeepArrows)
                                    {
                                        continue;
                                    }

                                    int candidateShape = ComputeGreedyRepairShapeScore(tailTrimCandidate, maskCanSpawn);
                                    int score = ComputeGreedyRepairCandidateScore(candidateFill, shapeBaseline, candidateShape, tailRemoved);
                                    if (score > bestScore)
                                    {
                                        bestScore = score;
                                        bestCandidate = tailTrimCandidate;
                                        bestRemoved = tailRemoved;
                                        bestMode = "trim-tail";
                                    }
                                }
                            }
                        }

                        int maxPairedTrim = Mathf.Min(trimCap, Mathf.Max(0, (chain.Length - 2) / 2));
                        if (maxPairedTrim > 0)
                        {
                        for (int trimHead = 1; trimHead <= maxPairedTrim; trimHead++)
                        {
                            if (budgetExceeded)
                                break;

                            for (int trimTail = 1; trimTail <= maxPairedTrim; trimTail++)
                            {
                                if (budgetExceeded)
                                    break;

                                if (trimHead + trimTail > chain.Length - 2)
                                    continue;

                                if (TryBuildGreedyRepairCandidate(
                                    board,
                                    chain,
                                    isLoop,
                                    trimHead,
                                    trimTail,
                                    out BoardState pairedCandidate,
                                    out int pairedRemoved))
                                {
                                    if (TryCandidateGreedy(pairedCandidate))
                                    {
                                        int candidateFill = CountArrowTiles(pairedCandidate);
                                        if (minKeepArrows > 0 && candidateFill < minKeepArrows)
                                        {
                                            continue;
                                        }

                                        int candidateShape = ComputeGreedyRepairShapeScore(pairedCandidate, maskCanSpawn);
                                        int score = ComputeGreedyRepairCandidateScore(candidateFill, shapeBaseline, candidateShape, pairedRemoved);
                                        if (score > bestScore)
                                        {
                                            bestScore = score;
                                            bestCandidate = pairedCandidate;
                                            bestRemoved = pairedRemoved;
                                            bestMode = "trim-both";
                                        }
                                    }
                                }
                            }
                        }
                        }
                    }
                }

                    if (bestCandidate == null)
                    {
                        if (budgetExceeded)
                            break;

                        int removeIndex = ChooseChainForFallback(board, chains, chainFlags, maskCanSpawn, hasLoopChain);
                        if (removeIndex < 0)
                            break;

                        int[] fallbackChain = chains[removeIndex];
                        bool fallbackIsLoop = chainFlags.Count > removeIndex && chainFlags[removeIndex];
                        if (fallbackChain == null || fallbackChain.Length <= 0)
                            break;

                        int fallbackTrimCap = Mathf.Min(kGreedyFallbackTrimPerChain, Mathf.Max(1, fallbackChain.Length - 2));
                        if (!TryBuildGreedyConservativeFallbackCandidate(
                            board,
                            fallbackChain,
                            fallbackIsLoop,
                            maskCanSpawn,
                            greedyMoves,
                            minKeepArrows,
                            shapeBaseline,
                            fallbackTrimCap,
                            trimCap,
                            out bestCandidate,
                            out bestRemoved,
                            out bestMode))
                        {
                            if (allowChainClearFallback && fallbackChain.Length <= kGreedyFallbackChainClearCap)
                            {
                                bestCandidate = CloneBoard(board);
                                if (bestCandidate == null)
                                    break;

                                ClearBoardCellsFromIndices(bestCandidate, fallbackChain);
                                bestRemoved = fallbackChain.Length;
                                bestMode = fallbackIsLoop ? "fallback-loop-clear" : "fallback-chain-clear";
                            }
                            else
                            {
                                lastMode = "fallback-disabled";
                                bestCandidate = null;
                                break;
                            }
                        }
                    }

                if (minKeepArrows > 0 && bestCandidate != null && CountArrowTiles(bestCandidate) < minKeepArrows)
                {
                    break;
                }

                if (bestRemoved <= 0)
                    break;

                if (bestCandidate == null)
                    break;

                board = bestCandidate;

                removedChains++;
                removedArrows += bestRemoved;
                lastMode = bestMode;
                shapeBaseline = ComputeGreedyRepairShapeScore(board, maskCanSpawn);
            }

            bool solvable = GreedyValidator.TryClearAllByGreedy(board, rules, greedyMoves, out _);
            string budgetDetails = evalCount > 0 || budgetExceeded
                ? $", evals={evalCount}{(budgetExceeded ? ", budgetExceeded" : string.Empty)}"
                : string.Empty;
            if (minKeepArrows > 0)
            {
                int finalFill = CountArrowTiles(board);
                if (finalFill < minKeepArrows)
                {
                    repairDetails = $"GreedyRepair passes={pass + 1}, removedChains={removedChains}, removedArrows={removedArrows}, minKeepArrows={minKeepArrows}, lastMode={lastMode}, solvable={solvable}, finalFill={finalFill}{budgetDetails}";
                    return false;
                }
            }

            repairDetails = $"GreedyRepair passes={pass + 1}, removedChains={removedChains}, removedArrows={removedArrows}, lastMode={lastMode}, solvable={solvable}{budgetDetails}";
            return solvable;
        }

        private static bool TryBuildGreedyConservativeFallbackCandidate(
            BoardState board,
            int[] chain,
            bool isLoop,
            bool[] maskCanSpawn,
            int greedyMoves,
            int minKeepArrows,
            int shapeBaseline,
            int trimCap,
            int chainTrimCap,
            out BoardState candidate,
            out int removedCount,
            out string fallbackMode)
        {
            candidate = null;
            removedCount = 0;
            fallbackMode = "fallback-none";

            if (board == null || board.tiles == null || chain == null || chain.Length < 2)
                return false;

            int chainLen = chain.Length;
            if (chainLen <= 2)
                return false;

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int bestScore = int.MinValue;
            BoardState bestCandidate = null;
            int bestRemoved = 0;
            string bestMode = "fallback-none";

            void TryConsider(BoardState candidateBoard, int removed, string mode)
            {
                if (candidateBoard == null)
                    return;

                if (!GreedyValidator.TryClearAllByGreedy(candidateBoard, rules, greedyMoves, out _))
                    return;

                int candidateFill = CountArrowTiles(candidateBoard);
                if (minKeepArrows > 0 && candidateFill < minKeepArrows)
                    return;

                int candidateShape = ComputeGreedyRepairShapeScore(candidateBoard, maskCanSpawn);
                int score = ComputeGreedyRepairCandidateScore(candidateFill, shapeBaseline, candidateShape, removed);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestCandidate = candidateBoard;
                    bestRemoved = removed;
                    bestMode = mode;
                }
            }

            int cappedTrim = Mathf.Max(1, Mathf.Min(trimCap, chainTrimCap));
            int maxTrim = Mathf.Max(1, Mathf.Min(cappedTrim, chainLen - 2));
            if (isLoop)
            {
                int maxLoopBreak = Mathf.Min(kGreedyFallbackLoopBreakCap, chainLen);
                for (int breakAt = 0; breakAt < maxLoopBreak; breakAt++)
                {
                    if (TryBuildGreedyLoopSingleBreakCandidate(
                        board,
                        chain,
                        breakAt,
                        out BoardState loopCandidate,
                        out int removed))
                    {
                        TryConsider(loopCandidate, removed, "fallback-loop-break");
                    }
                }
            }
            else
            {
                for (int trim = 1; trim <= maxTrim; trim++)
                {
                    if (TryBuildGreedyRepairCandidate(
                        board,
                        chain,
                        isLoop,
                        trim,
                        0,
                        out BoardState headCandidate,
                        out int headRemoved))
                    {
                        TryConsider(headCandidate, headRemoved, $"fallback-trim-head-{trim}");
                    }

                    if (TryBuildGreedyRepairCandidate(
                        board,
                        chain,
                        isLoop,
                        0,
                        trim,
                        out BoardState tailCandidate,
                        out int tailRemoved))
                    {
                        TryConsider(tailCandidate, tailRemoved, $"fallback-trim-tail-{trim}");
                    }
                }

                int maxPairedTrim = Mathf.Min(maxTrim, Mathf.Max(0, (chainLen - 2) / 2));
                for (int trimHead = 1; trimHead <= maxPairedTrim; trimHead++)
                {
                    for (int trimTail = 1; trimTail <= maxPairedTrim; trimTail++)
                    {
                        if (trimHead + trimTail > chainLen - 2)
                            continue;

                        if (TryBuildGreedyRepairCandidate(
                            board,
                            chain,
                            isLoop,
                            trimHead,
                            trimTail,
                            out BoardState bothCandidate,
                            out int bothRemoved))
                        {
                            TryConsider(bothCandidate, bothRemoved, $"fallback-trim-both-{trimHead}-{trimTail}");
                        }
                    }
                }
            }

            if (bestCandidate == null)
                return false;

            candidate = bestCandidate;
            removedCount = bestRemoved;
            fallbackMode = bestMode;
            return true;
        }

        private static int ComputeGreedyRepairCandidateScore(int candidateFill, int shapeBaseline, int candidateShape, int removedCount)
        {
            return candidateFill * kGreedyCandidateFillWeight
                + (candidateShape - shapeBaseline) * kGreedyCandidateShapeWeight
                - removedCount * kGreedyCandidateRemovalPenalty;
        }

        private static bool TryBuildGreedyLoopSingleBreakCandidate(
            BoardState board,
            int[] chain,
            int removeAt,
            out BoardState candidate,
            out int removedCount)
        {
            candidate = null;
            removedCount = 0;

            if (board == null || board.tiles == null || chain == null || chain.Length < 3)
                return false;

            if (removeAt < 0 || removeAt >= chain.Length)
                return false;

            removedCount = 1;
            candidate = CloneBoard(board);
            int removedCell = chain[removeAt];
            if (removedCell < 0 || removedCell >= candidate.tiles.Length)
            {
                candidate = null;
                return false;
            }

            candidate.tiles[removedCell] = TileState.Empty();

            var ordered = new List<int>(chain.Length - 1);
            var keep = new List<int>(chain.Length - 1);
            for (int i = 0; i < chain.Length; i++)
            {
                if (i == removeAt)
                    continue;

                keep.Add(chain[i]);
            }

            if (keep.Count < 2)
                return false;

            if (!TryOrderArrowComponentByAdjacency(candidate, keep, ordered))
            {
                candidate = null;
                return false;
            }

            if (!ApplyOrderedIndexChain(candidate, ordered))
            {
                candidate = null;
                return false;
            }

            return true;
        }

        private static bool TryBuildGreedyRepairCandidate(
            BoardState board,
            int[] chain,
            bool isLoop,
            int removeHead,
            int removeTail,
            out BoardState candidate,
            out int removedCount)
        {
            candidate = null;
            removedCount = 0;

            if (board == null || board.tiles == null || chain == null || chain.Length < 2)
                return false;

            if (isLoop && (removeHead > 0 || removeTail > 0))
                return false;

            if (removeHead < 0 || removeTail < 0 || removeHead + removeTail > chain.Length)
                return false;

            int keepStart = removeHead;
            int keepEnd = chain.Length - removeTail;
            int keepLen = keepEnd - keepStart;
            if (keepLen < 0)
                return false;

            removedCount = chain.Length - Mathf.Max(0, keepLen);
            candidate = CloneBoard(board);
            ClearBoardCellsFromIndices(candidate, chain);

            if (keepLen < 2)
                return true;

            var keep = new int[keepLen];
            Array.Copy(chain, keepStart, keep, 0, keepLen);
            if (!ApplyOrderedIndexChain(candidate, keep))
            {
                candidate = null;
                return false;
            }

            return true;
        }

        private static int ComputeGreedyRepairShapeScore(BoardState board, bool[] maskCanSpawn)
        {
            if (board == null || board.tiles == null)
                return 0;

            bool useMask = maskCanSpawn != null && maskCanSpawn.Length == board.tiles.Length;
            int score = 0;
            int boundaryKeepScore = 0;

            for (int i = 0; i < board.tiles.Length; i++)
            {
                if (board.tiles[i].type != TileType.Arrow)
                    continue;

                if (useMask && !maskCanSpawn[i])
                    score -= 3000;

                int x = i % board.width;
                int y = i / board.width;
                int neighborArrows = 0;

                if (x > 0 && board.tiles[i - 1].type == TileType.Arrow)
                    neighborArrows++;
                if (x < board.width - 1 && board.tiles[i + 1].type == TileType.Arrow)
                    neighborArrows++;
                if (y > 0 && board.tiles[i - board.width].type == TileType.Arrow)
                    neighborArrows++;
                if (y < board.height - 1 && board.tiles[i + board.width].type == TileType.Arrow)
                    neighborArrows++;

                score += neighborArrows * 3;
                if (useMask && IsMaskBoundaryCell(i, board.width, board.height, maskCanSpawn))
                {
                    boundaryKeepScore++;
                }
            }

            return score + boundaryKeepScore * kGreedyCandidateEdgeShapeWeight;
        }

        private static bool CollectMaskedChains(
            BoardState board,
            List<int[]> chains,
            List<bool> chainFlags,
            HashSet<int> visited,
            HashSet<int> chainSet,
            List<Vector2Int> ordered)
        {
            chains.Clear();
            chainFlags.Clear();
            visited.Clear();

            for (int i = 0; i < board.tiles.Length; i++)
            {
                if (visited.Contains(i))
                    continue;

                if (board.tiles[i].type != TileType.Arrow)
                {
                    visited.Add(i);
                    continue;
                }

                chainSet.Clear();
                int sx = i % board.width;
                int sy = i / board.width;
                ArrowChainUtility.CollectFullChain(board, new Vector2Int(sx, sy), 0, chainSet);
                if (chainSet.Count == 0)
                {
                    visited.Add(i);
                    continue;
                }

                foreach (int c in chainSet)
                    visited.Add(c);

                ordered.Clear();
                Vector2Int headPos;
                Dir headOutDir;
                bool isLoop;
                if (!ArrowChainUtility.TryBuildOrderedChain(
                    board,
                    chainSet,
                    new Vector2Int(sx, sy),
                    out ordered,
                    out headPos,
                    out headOutDir,
                    out isLoop))
                {
                    isLoop = true;
                }

                var copy = new int[chainSet.Count];
                chainSet.CopyTo(copy);
                chains.Add(copy);
                chainFlags.Add(isLoop);
            }

            return chains.Count > 0;
        }

        private static int ChooseChainForFallback(
            BoardState board,
            List<int[]> chains,
            List<bool> chainFlags,
            bool[] maskCanSpawn,
            bool loopOnly)
        {
            if (chains == null || chainFlags == null || chains.Count != chainFlags.Count)
                return -1;

            bool useMask = maskCanSpawn != null && board != null && maskCanSpawn.Length == board.width * board.height;
            int bestIndex = -1;
            int bestPenalty = int.MaxValue;

            for (int i = 0; i < chains.Count; i++)
            {
                if (loopOnly && chainFlags.Count > i && !chainFlags[i])
                    continue;

                int[] chain = chains[i];
                if (chain == null || chain.Length <= 0)
                    continue;

                int penalty = 0;
                if (chainFlags.Count > i && chainFlags[i])
                    penalty += 120;

                for (int j = 0; j < chain.Length; j++)
                {
                    int idx = chain[j];
                    if (idx < 0 || board == null || board.tiles == null || idx >= board.tiles.Length)
                        continue;

                    if (useMask && !maskCanSpawn[idx])
                    {
                        penalty += 90;
                    }

                    int x = idx % board.width;
                    int y = idx / board.width;

                    int neighbors = 0;
                    if (x > 0 && board.tiles[idx - 1].type == TileType.Arrow)
                        neighbors++;
                    if (x < board.width - 1 && board.tiles[idx + 1].type == TileType.Arrow)
                        neighbors++;
                    if (y > 0 && board.tiles[idx - board.width].type == TileType.Arrow)
                        neighbors++;
                    if (y < board.height - 1 && board.tiles[idx + board.width].type == TileType.Arrow)
                        neighbors++;

                    penalty += Math.Max(1, neighbors) * 4;
                }

                if (board != null && board.tiles != null)
                {
                    penalty += chain.Length * 3;
                }

                if (penalty < bestPenalty)
                {
                    bestPenalty = penalty;
                    bestIndex = i;
                }
            }

            return bestIndex;
        }

        private static void ClearClippedChainsByMask(BoardState board, bool[] maskArea, int minLengthToKeep)
        {
            if (board == null || maskArea == null)
                return;

            if (minLengthToKeep <= 1)
                minLengthToKeep = 2;

            var visited = new HashSet<int>();
            var chainSet = new HashSet<int>();
            int guard = Math.Max(1, board.width * board.height * 4);

            bool changed;
            do
            {
                changed = false;
                for (int i = 0; i < board.tiles.Length && guard-- > 0; i++)
                {
                    if (visited.Contains(i))
                        continue;

                    if (board.tiles[i].type != TileType.Arrow)
                    {
                        visited.Add(i);
                        continue;
                    }

                    chainSet.Clear();
                    int sx = i % board.width;
                    int sy = i / board.width;
                    ArrowChainUtility.CollectFullChain(board, new Vector2Int(sx, sy), 0, chainSet);

                    foreach (int c in chainSet)
                        visited.Add(c);

                    bool fullyInsideMask = true;
                    foreach (int c in chainSet)
                    {
                        if (!maskArea[c])
                        {
                            fullyInsideMask = false;
                            break;
                        }
                    }

                    if (fullyInsideMask && chainSet.Count >= minLengthToKeep)
                        continue;

                    foreach (int c in chainSet)
                        board.tiles[c] = TileState.Empty();

                    changed = true;
                }
            }
            while (changed && guard > 0);
        }

        private static int ClampBoardToMask(BoardState board, bool[] maskArea)
        {
            if (board == null || board.tiles == null || maskArea == null)
                return 0;

            int removed = 0;
            for (int i = 0; i < board.tiles.Length; i++)
            {
                if (board.tiles[i].type == TileType.Arrow && (i < 0 || i >= maskArea.Length || !maskArea[i]))
                {
                    board.tiles[i] = TileState.Empty();
                    removed++;
                }
            }

            return removed;
        }

        private static BoardState CloneBoard(BoardState src)
        {
            if (src == null)
                return null;

            var clone = new BoardState(src.width, src.height);
            Array.Copy(src.tiles, clone.tiles, src.tiles.Length);
            return clone;
        }

        private static int CountArrowTiles(BoardState board)
        {
            if (board == null)
                return 0;

            int count = 0;
            for (int i = 0; i < board.tiles.Length; i++)
            {
                if (board.tiles[i].type == TileType.Arrow)
                    count++;
            }

            return count;
        }

        private static LevelDefinition BuildAuthoredLevelDefinition(
            LevelDefinition sourceDef,
            Texture2D mask,
            BoardState board,
            out string details,
            string levelIdSuffix = "maskpatch")
        {
            details = string.Empty;
            var authored = ConvertBoardToAuthored(board, out string warning);
            if (!string.IsNullOrWhiteSpace(warning))
                Debug.LogWarning(warning);

            if (!EnsureAuthoredDataBuildable(
                board,
                authored,
                out var sanitizedAuthored,
                out var authoredBoard,
                out string authoredDetails))
            {
                details = string.IsNullOrWhiteSpace(authoredDetails)
                    ? "AuthoredDataBuildFailed"
                    : authoredDetails;
                return null;
            }

            if (!string.IsNullOrWhiteSpace(warning))
            {
                details = $"AuthoringWarning={warning}";
            }

            if (!string.IsNullOrWhiteSpace(authoredDetails))
            {
                details = string.IsNullOrWhiteSpace(details)
                    ? $"AuthoringSanity={authoredDetails}"
                    : $"{details} | AuthoringSanity={authoredDetails}";
            }

            authored = sanitizedAuthored;

            if (authoredBoard == null)
            {
                details = string.IsNullOrWhiteSpace(details)
                    ? "AuthoredBoardNull"
                    : $"{details} | AuthoredBoardNull";
                return null;
            }

            var output = ScriptableObject.CreateInstance<LevelDefinition>();
            string safeSuffix = string.IsNullOrWhiteSpace(levelIdSuffix) ? "maskpatch" : SanitizeName(levelIdSuffix);
            output.levelId = $"{sourceDef.levelId}_{safeSuffix}";
            output.source = LevelDefinition.LevelSource.Authored;

            output.board.width = board.width;
            output.board.height = board.height;
            output.board.seed = sourceDef.board.seed;

            output.generation.arrowCoverage = sourceDef.generation.arrowCoverage;
            output.generation.initialMovableArrowCount = sourceDef.generation.initialMovableArrowCount;
            output.generation.targetDifficultyScore = sourceDef.generation.targetDifficultyScore;
            output.generation.fixedGenerationSeed = sourceDef.generation.fixedGenerationSeed;
            output.generation.minPathLen = Mathf.Max(2, sourceDef.generation.minPathLen);
            output.generation.maxPathLength = sourceDef.generation.maxPathLength;
            output.generation.twistiness = sourceDef.generation.twistiness;
            output.generation.validateWithGreedy = sourceDef.generation.validateWithGreedy;

            output.lose.blockedLoseLimit = sourceDef.lose.blockedLoseLimit;
            output.masking.spawnMask = mask;
            output.arrowColorMode = sourceDef.arrowColorMode;
            output.arrowColorMaskQuantizeSteps = sourceDef.arrowColorMaskQuantizeSteps;
            output.tintOnHit = sourceDef.tintOnHit;
            output.hitTint = sourceDef.hitTint;
            output.introSettings = sourceDef.introSettings;
            output.palette = sourceDef.palette;
            output.authoredLevel = authored;
            return output;
        }

        private static bool EnsureAuthoredDataBuildable(
            BoardState board,
            AuthoredLevelData authored,
            out AuthoredLevelData sanitizedAuthored,
            out BoardState authoredBoard,
            out string details)
        {
            sanitizedAuthored = authored;
            authoredBoard = null;
            details = string.Empty;

            if (authored == null)
            {
                details = "AuthoredDataNull";
                return false;
            }

            if (AuthoredLevelBuilder.TryBuildBoard(authored, out authoredBoard, out string buildError))
            {
                return true;
            }

            if (authored.arrows == null || authored.arrows.Count == 0)
            {
                details = $"AuthoredBuildFailed={buildError}";
                return false;
            }

            var remaining = new List<AuthoredArrowData>(authored.arrows);
            int removedChains = 0;
            string lastError = buildError;

            while (remaining.Count > 0)
            {
                bool removedAny = false;

                for (int i = 0; i < remaining.Count; i++)
                {
                    var candidate = new AuthoredLevelData
                    {
                        width = authored.width,
                        height = authored.height,
                        arrows = new List<AuthoredArrowData>(remaining.Count - 1),
                        blockIndices = authored.blockIndices != null
                            ? new List<int>(authored.blockIndices)
                            : new List<int>()
                    };

                    for (int j = 0; j < remaining.Count; j++)
                    {
                        if (i != j)
                            candidate.arrows.Add(remaining[j]);
                    }

                    if (!AuthoredLevelBuilder.TryBuildBoard(candidate, out _, out string candidateError))
                    {
                        lastError = candidateError;
                        continue;
                    }

                    removedAny = true;
                    removedChains++;
                    if (board != null)
                    {
                        ClearBoardCellsFromAuthoredIndices(board, remaining[i]);
                    }

                    remaining.RemoveAt(i);
                    break;
                }

                if (!removedAny)
                {
                    if (remaining.Count > 1)
                    {
                        removedChains++;
                        if (board != null)
                        {
                            ClearBoardCellsFromAuthoredIndices(board, remaining[0]);
                        }

                        remaining.RemoveAt(0);
                        continue;
                    }

                    details = removedChains > 0
                        ? $"AuthoringSelfBlockRemediationFailed; Removed={removedChains}; LastError={lastError}"
                        : $"AuthoringBuildFailed={buildError}";
                    return false;
                }
            }

            sanitizedAuthored = new AuthoredLevelData
            {
                width = authored.width,
                height = authored.height,
                arrows = remaining,
                blockIndices = authored.blockIndices != null
                    ? new List<int>(authored.blockIndices)
                    : new List<int>()
            };

            if (!AuthoredLevelBuilder.TryBuildBoard(sanitizedAuthored, out authoredBoard, out string finalError))
            {
                details = $"AuthoringBuildRetryFailed={finalError}";
                return false;
            }

            details = $"RemovedBadChains={removedChains}";
            return true;
        }

        private static void ClearBoardCellsFromAuthoredIndices(BoardState board, AuthoredArrowData chain)
        {
            if (board == null || board.tiles == null || chain == null || chain.indices == null)
                return;

            for (int i = 0; i < chain.indices.Count; i++)
            {
                int idx = chain.indices[i];
                if (idx >= 0 && idx < board.tiles.Length)
                    board.tiles[idx] = TileState.Empty();
            }
        }

        private static int GetAuthoredArrowCount(AuthoredLevelData authored)
        {
            if (authored == null || authored.arrows == null)
                return 0;

            int total = 0;
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                if (authored.arrows[i] != null && authored.arrows[i].indices != null)
                    total += authored.arrows[i].indices.Count;
            }
            return total;
        }

        private static AuthoredLevelData ConvertBoardToAuthored(BoardState board, out string warning)
        {
            warning = string.Empty;

            var authored = new AuthoredLevelData
            {
                width = board.width,
                height = board.height,
                arrows = new List<AuthoredArrowData>(),
                blockIndices = new List<int>()
            };

            var visited = new HashSet<int>();
            var chainSet = new HashSet<int>();
            var ordered = new List<Vector2Int>();
            int removedChains = 0;

            for (int i = 0; i < board.tiles.Length; i++)
            {
                if (visited.Contains(i))
                    continue;

                if (board.tiles[i].type == TileType.Block)
                {
                    authored.blockIndices.Add(i);
                    visited.Add(i);
                    continue;
                }

                if (board.tiles[i].type != TileType.Arrow)
                    continue;

                int x = i % board.width;
                int y = i / board.width;
                chainSet.Clear();
                if (ordered == null)
                    ordered = new List<Vector2Int>();
                else
                    ordered.Clear();

                ArrowChainUtility.CollectFullChain(board, new Vector2Int(x, y), 0, chainSet);
                if (chainSet.Count == 0)
                {
                    board.tiles[i] = TileState.Empty();
                    removedChains++;
                    continue;
                }

                foreach (int n in chainSet)
                    visited.Add(n);

                if (chainSet.Count < 2)
                {
                    foreach (int n in chainSet)
                        board.tiles[n] = TileState.Empty();
                    removedChains++;
                    continue;
                }

                if (!TryBuildStableOrderedChain(board, chainSet, new Vector2Int(x, y), out var stableOrdered))
                {
                    foreach (int n in chainSet)
                        board.tiles[n] = TileState.Empty();
                    removedChains++;
                    continue;
                }

                ordered.Clear();
                ordered.AddRange(stableOrdered);

                var data = new AuthoredArrowData
                {
                    indices = new List<int>(ordered.Count),
                    colorIndex = 0
                };

                for (int j = 0; j < ordered.Count; j++)
                {
                    int idx = ordered[j].x + ordered[j].y * board.width;
                    data.indices.Add(idx);
                }

            authored.arrows.Add(data);
            }

            if (removedChains > 0)
            {
                int arrowCount = 0;
                for (int i = 0; i < board.tiles.Length; i++)
                {
                    if (board.tiles[i].type == TileType.Arrow)
                        arrowCount++;
                }
                warning = $"[SeedMaskPatch] Removed {removedChains} invalid/short chains. Final arrow count={arrowCount}.";
            }

            return authored;
        }

        private static bool TryFixChainOrderIfNeeded(
            BoardState board,
            HashSet<int> chainSet,
            ref List<Vector2Int> ordered)
        {
            if (ordered == null || ordered.Count < 2 || board == null || chainSet == null)
                return false;

            if (ordered.Count != chainSet.Count)
                return false;

            var reversed = new List<Vector2Int>(ordered.Count);
            for (int i = ordered.Count - 1; i >= 0; i--)
                reversed.Add(ordered[i]);

            if (TryBuildSingleAuthoredChain(board, ordered))
            {
                return true;
            }

            if (TryBuildSingleAuthoredChain(board, reversed))
            {
                ordered = reversed;
                return true;
            }

            return false;
        }

        private static bool TryBuildStableOrderedChain(
            BoardState board,
            HashSet<int> chainSet,
            Vector2Int preferredStart,
            out List<Vector2Int> ordered)
        {
            ordered = null;
            if (board == null || chainSet == null || chainSet.Count < 2)
                return false;

            var startIndices = new List<Vector2Int>(chainSet.Count);
            int preferredIndex = preferredStart.x + preferredStart.y * board.width;
            if (chainSet.Contains(preferredIndex))
                startIndices.Add(preferredStart);

            foreach (int idx in chainSet)
            {
                var start = new Vector2Int(idx % board.width, idx / board.width);
                if (start == preferredStart)
                    continue;

                startIndices.Add(start);
            }

            foreach (var start in startIndices)
            {
                var candidate = new List<Vector2Int>(chainSet.Count);
                if (!ArrowChainUtility.TryBuildOrderedChain(
                    board,
                    chainSet,
                    start,
                    out candidate,
                    out _,
                    out _,
                    out _))
                {
                    continue;
                }

                if (!TryFixChainOrderIfNeeded(board, chainSet, ref candidate))
                    continue;

                ordered = candidate;
                return true;
            }

            return false;
        }

        private static bool TryBuildSingleAuthoredChain(BoardState board, List<Vector2Int> ordered)
        {
            if (board == null || ordered == null || ordered.Count < 2)
                return false;

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
                    new AuthoredArrowData
                    {
                        indices = indices,
                        colorIndex = 0
                    }
                }
            };

            if (!AuthoredLevelBuilder.TryBuildBoard(temp, out var rebuilt, out string _))
                return false;

            if (rebuilt == null)
                return false;

            int rebuiltCount = 0;
            for (int i = 0; i < rebuilt.tiles.Length; i++)
            {
                if (rebuilt.tiles[i].type == TileType.Arrow)
                {
                    rebuiltCount++;
                    if (!expected.Contains(i))
                        return false;

                    TileState sourceTile = board.tiles[i];
                    TileState rebuiltTile = rebuilt.tiles[i];
                    if (sourceTile.type != TileType.Arrow ||
                        rebuiltTile.arrow.inDir != sourceTile.arrow.inDir ||
                        rebuiltTile.arrow.outDir != sourceTile.arrow.outDir)
                    {
                        return false;
                    }
                }
            }

            return rebuiltCount == ordered.Count;
        }

    }
}
#endif
