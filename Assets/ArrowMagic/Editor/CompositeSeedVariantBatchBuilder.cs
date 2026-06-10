#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace PixelBug.ArrowMagic.EditorTools
{
    public static class CompositeSeedVariantBatchBuilder
    {
        private const string SeedRoot = "Assets/ArrowMagic/SOData/Levels/Seeds";
        private const string OutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsAB";
        private const string RawOutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsABRaw";
        private const string GreedyOutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsABGreedy";
        private const string GreedySmokeOutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsABGreedySmoke";
        private const string ScoreTopOutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsABScoreTop";
        private const string ScoreTop10OutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsABScoreTop10";
        private const string ScoreTop50OutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsABScoreTop50";
        private const string ProductionParentOutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsABProductionParents20";
        private const string Production100OutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsABProduction100";
        private const string FinalParentOutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsABFinalParents40";
        private const string Final100OutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsABFinal100";
        private const string CapacityProbeParentOutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsABCapacityProbeParents80";
        private const string CapacityProbe200OutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsABCapacityProbe200";
        private const string CapacityProbe200FromFinal40OutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsABCapacityProbe200FromFinal40";
        private const string CapacityAuditParent150OutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsABCapacityAuditParents150";
        private const string CapacityAuditParent150PortraitOutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsABCapacityAuditParents150Portrait";
        private const string PortraitVariant50OutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsABPortraitVariant50";
        private const string PlayabilityCandidateOutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsABPlayabilityCandidates300";
        private const string R1OuterStrongOutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/R1OuterStrongRepair";
        private const string R2OuterStrongOutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/R2OuterStrongRepair";
        private const string DiverseParentOutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsABDiverseParents30";
        private const string AbcProbeOutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsABCProbe10";
        private const string DependencySmokeOutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/CompositeSeedVariantsABDependencySmoke10";
        private const string DemoScenePath = "Assets/ArrowMagic/Scenes/Demo.unity";
        private const string R1FormalFinalPackPath = "Assets/ArrowMagic/SOData/Packs/R1FullPipeline/FormalCandidateAB/R1FormalCandidateABFinalPack.asset";
        private const string R2FinalRotationPackPath = "Assets/ArrowMagic/SOData/Packs/R1FullPipeline/R2FinalCandidatePool/R2FromR1FormalRotationFinalPack.asset";
        private const string PackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_50.asset";
        private const string ComparePackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_CompareTriplets.asset";
        private const string RawComparePackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_RawPreserveTriplets.asset";
        private const string GreedyComparePackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_GreedyRepairTriplets.asset";
        private const string GreedySmokeComparePackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_GreedyRepairSmokeTriplets.asset";
        private const string ScoreTopComparePackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_ScoreTopTriplets.asset";
        private const string ScoreTop10PackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_ScoreTop10.asset";
        private const string ScoreTop50PackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_ScoreTop50.asset";
        private const string ProductionParentPackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_ProductionParents20.asset";
        private const string Production100PackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_Production100.asset";
        private const string FinalParentPackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_FinalParents40.asset";
        private const string Final100PackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_Final100.asset";
        private const string CapacityProbeParentPackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_CapacityProbeParents80.asset";
        private const string CapacityProbe200PackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_CapacityProbe200.asset";
        private const string CapacityProbe200FromFinal40PackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_CapacityProbe200FromFinal40.asset";
        private const string CapacityAuditParent150PackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_CapacityAuditParents150.asset";
        private const string CapacityAuditParent150PortraitPackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_CapacityAuditParents150Portrait.asset";
        private const string PortraitVariant50PackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_PortraitVariant50.asset";
        private const string PlayabilityCandidatePackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_PlayabilityCandidates300.asset";
        private const string PlayabilityFinal150PackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_PlayabilityFinal150.asset";
        private const string PlayabilityCurated100PackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_PlayabilityCurated100.asset";
        private const string PlayabilityCurated85PackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_PlayabilityCurated85.asset";
        private const string PlayabilityBalanced120PackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_PlayabilityBalanced120.asset";
        private const string R1OuterStrongPackPath = "Assets/ArrowMagic/SOData/Packs/R1FullPipeline/OuterStrongRepair/R1OuterStrongRepairPack.asset";
        private const string R2OuterStrongPackPath = "Assets/ArrowMagic/SOData/Packs/R1FullPipeline/OuterStrongRepair/R2OuterStrongRepairPack.asset";
        private const string R1R2OuterStrongPreviewPackPath = "Assets/ArrowMagic/SOData/Packs/R1FullPipeline/OuterStrongRepair/R1R2OuterStrongRepairPreviewPack.asset";
        private const string R1OuterStrongFinalPackPath = "Assets/ArrowMagic/SOData/Packs/R1FullPipeline/OuterStrongFinal/R1OuterStrongFinalPack.asset";
        private const string R2OuterStrongFinalPackPath = "Assets/ArrowMagic/SOData/Packs/R1FullPipeline/OuterStrongFinal/R2OuterStrongFinalPack.asset";
        private const string R2OuterStrongA484LevelPath = "Assets/ArrowMagic/SOData/Levels/Generated/R2OuterStrongRepair/r2_outer_strong_030_r2_046_a484.asset";
        private const string R2OuterStrongA484SinglePackPath = "Assets/ArrowMagic/SOData/Packs/R1FullPipeline/OuterStrongFinal/R2OuterStrong_A484_SinglePack.asset";
        private const string DiverseParentPackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_DiverseParents30.asset";
        private const string AbcProbePackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsABC_Probe10.asset";
        private const string DependencySmokePackPath = "Assets/ArrowMagic/SOData/Packs/CompositeSeedVariantsAB/CompositeSeedVariantsAB_DependencySmoke10.asset";
        private const string ReportPath = "Temp/composite_seed_variants_ab_50_report.csv";
        private const string CompareReportPath = "Temp/composite_seed_variants_ab_compare_triplets_report.csv";
        private const string RawCompareReportPath = "Temp/composite_seed_variants_ab_raw_preserve_triplets_report.csv";
        private const string GreedyCompareReportPath = "Temp/composite_seed_variants_ab_greedy_repair_triplets_report.csv";
        private const string GreedySmokeCompareReportPath = "Temp/composite_seed_variants_ab_greedy_repair_smoke_triplets_report.csv";
        private const string CandidateScoreReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_candidate_scores.csv";
        private const string ScoreTopCompareReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_score_top_triplets_report.csv";
        private const string ScoreTop10ReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_score_top10_report.csv";
        private const string ScoreTop50ReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_score_top50_report.csv";
        private const string ProductionParentReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_production_parents20_report.csv";
        private const string Production100ReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_production100_report.csv";
        private const string FinalParentReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_final_parents40_report.csv";
        private const string Final100ReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_final100_report.csv";
        private const string CapacityProbeParentReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_capacity_probe_parents80_report.csv";
        private const string CapacityProbe200ReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_capacity_probe200_report.csv";
        private const string CapacityProbe200FromFinal40ReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_capacity_probe200_from_final40_report.csv";
        private const string CapacityAuditParent150ReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_capacity_audit_parents150_report.csv";
        private const string CapacityAuditParent150PortraitReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_capacity_audit_parents150_portrait_report.csv";
        private const string PortraitVariant50ReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_portrait_variant50_report.csv";
        private const string PlayabilityCandidateReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_playability_candidates300_report.csv";
        private const string PlayabilityFinal150ReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_playability_final150_report.csv";
        private const string PlayabilityCurated100ReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_playability_curated100_report.csv";
        private const string PlayabilityCurated85ReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_playability_curated85_report.csv";
        private const string PlayabilityBalanced120ReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_playability_balanced120_report.csv";
        private const string R1OuterStrongReportPath = "Assets/ArrowMagic/SOData/Reports/r1_outer_strong_repair_report.csv";
        private const string R2OuterStrongReportPath = "Assets/ArrowMagic/SOData/Reports/r2_outer_strong_repair_report.csv";
        private const string R1R2OuterStrongSummaryPath = "Assets/ArrowMagic/SOData/Reports/r1_r2_outer_strong_repair_summary.csv";
        private const string R1R2OuterStrongFinalSummaryPath = "Assets/ArrowMagic/SOData/Reports/r1_r2_outer_strong_final_summary.csv";
        private const string R1R2OuterStrongFinalRestoreReportPath = "Assets/ArrowMagic/SOData/Reports/r1_r2_outer_strong_final_restore_report.csv";
        private const string CapacityAuditParent150FailuresPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_capacity_audit_parents150_failures.txt";
        private const string CapacityAuditParent150PlanOrderPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_capacity_audit_parents150_plan_order.txt";
        private const string DiverseParentReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_diverse_parents30_report.csv";
        private const string AbcProbeReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_abc_probe10_report.csv";
        private const string DependencySmokeReportPath = "Assets/ArrowMagic/SOData/Reports/composite_seed_variants_ab_dependency_smoke10_report.csv";
        private const int TargetCount = 50;
        private const int ProductionParentCount = 20;
        private const int ProductionPackCount = 100;
        private const int FinalParentCount = 40;
        private const int FinalPackCount = 100;
        private const int CapacityProbeParentCount = 80;
        private const int CapacityAuditParentCount = 150;
        private const int PortraitVariantPreviewCount = 50;
        private const int PortraitVariantFastPreviewCount = 10;
        private const int PlayabilityCandidateCount = 300;
        private const int PlayabilityFinalCount = 150;
        private const int PlayabilityCuratedCount = 100;
        private const int PlayabilityCuratedHoleTrimCount = 85;
        private const int PlayabilityBalancedCount = 120;
        private const int PlayabilityHoleTrimRemoveCount = 15;
        private const int R1R2OuterStrongPreviewCount = 100;
        private const int PlayabilityMaxVariantsPerSource = 4;
        private const int PlayabilityStrictMaxFamilyUse = 8;
        private const int PlayabilityRelaxedMaxFamilyUse = 10;
        private const int CapacityProbePackCount = 200;
        private const int DiverseParentCount = 30;
        private const int AbcProbeCount = 10;
        private const int DependencySmokeCount = 10;
        private const int CapacityAuditParentSegmentSize = 6;
        private const int CapacityAuditParentSegmentMaxPlanChecks = 1200;
        private const int FinalMaxVariantsPerSource = 6;
        private const int CapacityProbeMaxVariantsPerSource = 4;
        private const int CapacityProbeMaxFamilyUse = 10;
        private const int CapacityAuditParentMaxFamilyUse = 9;
        private const float MaxSourceStructureMismatch = 0.68f;
        private const float WideParentPortraitTransformAspect = 1.25f;
        private const float MaxLocalEmptyRatio = 0.46f;
        private const int LocalEmptyWindowSize = 8;
        private const int MinChains = 120;
        private const int RepairMinChains = 108;
        private const int MaxChains = 200;
        private const float MinCoverage = 0.90f;
        private const float RepairMinCoverage = 0.80f;
        private const float MaxAspect = 1.65f;
        private const int MaxGreedyRepairIterationsPerPlan = 20;
        private const int MaxTacticalReversalsPerPlan = 6;
        private const int MaxParentEdgeFillChains = 20;
        private const int MinOuterFillRunLength = 5;
        private const int MinPortraitOuterFillRunLength = 4;
        private const int MinPortraitOuterPatchRunLength = 2;
        private const int MaxPortraitOuterFillRunLength = 18;
        private const int PortraitOuterSnakeBandDepth = 5;
        private const int MaxPortraitOuterShortPatchChains = 16;
        private const int MaxPortraitOuterShortClusterTrim = 8;
        private const int PortraitOuterShortClusterRadius = 4;
        private const int MaxOuterShortChains = 10;
        private const float StrictPortraitOuterBand1Coverage = 0.98f;
        private const float BalancedPortraitOuterBand1Coverage = 0.96f;
        private const float FallbackPortraitOuterBand1Coverage = 0.94f;
        private const float FloorPortraitOuterBand1Coverage = 0.92f;
        private const int StrictPortraitMaxOuterEmptyRun = 2;
        private const int BalancedPortraitMaxOuterEmptyRun = 3;
        private const int FallbackPortraitMaxOuterEmptyRun = 4;
        private const int FloorPortraitMaxOuterEmptyRun = 5;
        private const int OuterStrongRestoreEdgeLongThreshold = 20;
        private const int OuterStrongRestoreEdgeLongDeltaThreshold = 12;
        private const int OuterStrongRestoreSoftEdgeLongThreshold = 15;
        private const int OuterStrongRestoreMaxEdgeRunThreshold = 18;
        private const int OuterStrongFinalMinChains = 20;
        private const int OuterStrongFinalMinTiles = 80;
        private const float MaxSeamEmptyRatio = 0.28f;
        private const int MaxSeamStraightRunRatioPercent = 72;
        private const int MaxMeaninglessBridgeChains = 3;
        private const int MaxMeaninglessBridgeCorridorChains = 4;
        private const int MinMeaninglessBridgeRunRatioPercent = 25;
        private const int SeamBridgeCorridorPadding = 4;
        private const int DominantSeamChannelRunRatioPercent = 25;
        private const int MinSeamChannelInterlockRun = 4;
        private const int MaxDominantSeamChannelInterlocks = 2;
        private const int FinalCentralBridgePadding = 5;
        private const int MaxFinalCentralChannelInterlocks = 4;
        private const int MaxFinalCentralSoftChannelInterlocks = 3;
        private const int SeamSegmentLength = 16;
        private const int FastGreedyBudgetMin = 450;
        private const int FastGreedyBudgetPerChain = 3;
        private static readonly Regex PlainSeedName = new Regex(@"^seed_(Above300|above300|Arrowz)_level_\d+$", RegexOptions.Compiled);
        private static readonly Regex R1FinalSeedName = new Regex(@"^r1_ab_\d+_.+_final$", RegexOptions.Compiled);
        private static readonly Regex R2FinalSeedName = new Regex(@"^(r2_.+|r1_ab_\d+_.+_final_.+rotation.*)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex ShortR1Name = new Regex(@"^r1_ab_(\d+).*?(?:above300_level_|arrowz_level_|seed_)?(\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex BaseLevelNumberName = new Regex(@"(?:above300_level_|arrowz_level_|seed_)(\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex CompositeFamilyToken = new Regex(@"(?:^|_)a(\d+)(?=_|$)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly string[] StandardFinalTransforms = { "identity", "hflip", "vflip", "rot180", "rot90", "rot270", "transpose", "antiTranspose" };
        private static readonly string[] WidePortraitPreferredTransforms = { "rot90", "rot270", "transpose", "antiTranspose", "identity", "hflip", "vflip", "rot180" };

        private sealed class SourceSeed
        {
            public LevelDefinition Def;
            public string Path;
            public string Name;
            public int Width;
            public int Height;
            public int Chains;
            public int Tiles;
            public float Coverage;
            public float Aspect;
            public int HeadLeft;
            public int HeadRight;
            public int HeadUp;
            public int HeadDown;
            public int SourceTier;
            public float CenterX;
            public float CenterY;
            public float LeftDensity;
            public float RightDensity;
            public float TopDensity;
            public float BottomDensity;
            public float MaxLocalEmptyRatio;
        }

        private sealed class Plan
        {
            public SourceSeed A;
            public SourceSeed B;
            public bool Horizontal;
            public int Gap;
            public int Width;
            public int Height;
            public int Chains;
            public int Tiles;
            public float Coverage;
            public float Aspect;
            public float Score;
        }

        private sealed class LevelRef
        {
            public LevelDefinition Asset;
            public string Path;
        }

        private sealed class ReportRow
        {
            public string Id;
            public string Path;
            public string SourceA;
            public string SourceB;
        }

        private sealed class ChainMeta
        {
            public int SourceIndex;
            public int TileCount;
        }

        private sealed class CandidateStats
        {
            public Plan Plan;
            public int PairPriority;
            public int InwardPressure;
            public int CrossBlocks;
            public int BlockedByOtherMax;
            public int BlocksOtherMax;
            public int BlockedChains;
            public int BlockingChains;
            public int TopBlockedIndex;
            public int TopBlockedOwner;
            public int TopBlockedHits;
            public int TopBlockerIndex;
            public int TopBlockerOwner;
            public int TopBlockerHits;
            public float StructureMismatch;
            public float RiskScore;
            public string Details;
        }

        private sealed class PlayabilityMetrics
        {
            public bool Solved;
            public int Steps;
            public int Openers;
            public float AvgChoices;
            public int MinChoices;
            public int MaxChoices;
            public int LowChoiceSteps;
            public int HighChoiceSteps;
            public int UnlockBursts;
            public int MaxUnlockBurst;
            public int KeyLockSteps;
            public float FirstOuterRatio;
            public float LastOuterRatio;
            public float EndingShortRatio;
            public float AvgChainLength;
            public int MaxChainLength;
            public int RegionSwitches;
            public int FirstRegion;
            public int LastRegion;
            public float FirstRegionShare;
            public float LastRegionShare;
            public int DependencyBlocks;
            public int DependencyCycles;
            public int DependencyLayerStuck;
            public string TypeId;
            public float Score;
            public string Details;
        }

        private sealed class PlayabilitySelectionCandidate
        {
            public LevelDefinition Level;
            public string Path;
            public string Source;
            public List<string> Families;
            public int Width;
            public int Height;
            public int Chains;
            public float Coverage;
            public OuterFillMetrics Outer;
            public VisualQualityMetrics Visual;
            public HoleTrimCandidate Hole;
            public PlayabilityMetrics Playability;
        }

        private sealed class OuterStrongCandidate
        {
            public AuthoredLevelData Authored;
            public string Mode;
            public int TrimmedOuterExit;
            public int TrimmedOuterShort;
            public int Filled;
            public int Chains;
            public int Tiles;
            public float Coverage;
            public OuterFillMetrics Outer;
            public VisualQualityMetrics Visual;
            public bool Greedy;
            public string GreedyDetails;
            public float Score;
        }

        private sealed class OuterStrongPreviewCandidate
        {
            public LevelDefinition Level;
            public string PackLabel;
            public string SourceId;
            public string Path;
            public OuterFillMetrics BeforeOuter;
            public OuterFillMetrics AfterOuter;
            public VisualQualityMetrics BeforeVisual;
            public VisualQualityMetrics AfterVisual;
            public float Priority;
        }

        private sealed class OuterStrongPackResult
        {
            public string Label;
            public string InputPath;
            public string OutputPath;
            public int SourceCount;
            public int OutputCount;
            public int ChangedCount;
            public int ImprovedCount;
            public int GreedyFailCount;
            public int MissingCount;
            public float BeforeOuter1Sum;
            public float AfterOuter1Sum;
            public float BeforeOuter2Sum;
            public float AfterOuter2Sum;
            public int BeforeMaxOuterEmptyRun;
            public int AfterMaxOuterEmptyRun;
            public int BeforeEdgeShortSum;
            public int AfterEdgeShortSum;
            public int BeforeEdgeLongSum;
            public int AfterEdgeLongSum;
        }

        private sealed class OuterStrongRepairReportRow
        {
            public int Rank;
            public string Status;
            public string SourceId;
            public string SourcePath;
            public string OutputId;
            public string OutputPath;
            public string Mode;
            public int EdgeLongBefore;
            public int EdgeLongAfter;
            public int MaxEdgeRunAfter;
        }

        private sealed class PlayabilityChainStats
        {
            public int Length;
            public float CenterX;
            public float CenterY;
            public bool TouchesOuter;
            public int Region;
        }

        private sealed class HoleTrimCandidate
        {
            public LevelDefinition Level;
            public int Rank;
            public int Width;
            public int Height;
            public int Chains;
            public float Coverage;
            public float LocalEmpty8;
            public int LocalEmpty8Cells;
            public int LocalEmpty8Total;
            public float LocalEmpty6;
            public int LocalEmpty6Cells;
            public int LocalEmpty6Total;
            public float CenterEmpty;
            public float HoleScore;
            public bool Remove;
        }

        private struct MoveScore
        {
            public Move Move;
            public int ChainIndex;
            public int ClearedTiles;
        }

        private sealed class ParentDiversityOptions
        {
            public int StrictGroups;
            public int StrictMaxBaseSourceUse;
            public int RelaxedMaxBaseSourceUse;
            public int StrictMaxExactSourceUse;
            public int RelaxedMaxExactSourceUse;
            public bool UseFamilySourceLimit;
        }

        private struct VisualQualityMetrics
        {
            public int Penalty;
            public int EdgeLong;
            public int CornerLong;
            public int EdgeShort;
            public int MaxEdgeRun;
        }

        private struct OuterFillMetrics
        {
            public int Band1Filled;
            public int Band1Total;
            public int Band2Filled;
            public int Band2Total;
            public int MaxEmptyRun;
            public float Band1Coverage => Band1Filled / (float)Mathf.Max(1, Band1Total);
            public float Band2Coverage => Band2Filled / (float)Mathf.Max(1, Band2Total);
        }

        private struct PortraitOuterProfile
        {
            public string Name;
            public float MinBand1Coverage;
            public int MaxEmptyRun;
        }

        private sealed class DependencySnapshot
        {
            public int TotalBlocks;
            public int FullRayBlockCount;
            public int MaxRayBlockers;
            public int CycleCount;
            public int CycleMemberCount;
            public int LargestCycleLength;
            public int LayerClearedCount;
            public int LayerStuckCount;
            public int MaxClearLayer;
            public int GateChainIndex;
            public int GateChainBlocks;
            public int[] BlockedByAny;
            public int[] BlocksAny;
            public int[] FullBlockedByAny;
            public int[] FullBlocksAny;
            public int[] BlockerByChain;
            public bool[] CycleMembers;
            public int[] CycleIds;
            public int[] CycleLengths;
            public int[] ClearLayers;
            public List<int>[] BlockerQueues;
            public string SampleCycle;
            public string StuckSample;
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build 50 Composite AB Seeds")]
        public static void Build50CompositeABSeeds()
        {
            EnsureFolder(OutputFolder);
            EnsureFolder(Path.GetDirectoryName(PackPath)?.Replace("\\", "/"));
            ClearOutputFolder();

            var sources = LoadPlainSources();
            var plans = BuildPlans(sources);
            var generated = new List<LevelDefinition>(TargetCount);
            var report = new List<string>
            {
                "rank,id,path,sourceA,sourceB,layout,width,height,chains,tiles,coverage,aspect,greedy"
            };

            var usedPairs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            int serial = 1;
            for (int i = 0; i < plans.Count && generated.Count < TargetCount; i++)
            {
                Plan plan = plans[i];
                string pairKey = MakePairKey(plan);
                if (!usedPairs.Add(pairKey))
                    continue;

                if (!TryBuildComposite(plan, serial, out var def, out string id, out string details))
                {
                    Debug.LogWarning($"[CompositeSeedVariant] Skip {pairKey}: {details}");
                    continue;
                }

                if (!AuthoredLevelBuilder.TryBuildBoard(def.authoredLevel, out var board, out string buildError))
                {
                    Debug.LogWarning($"[CompositeSeedVariant] Authored build failed {id}: {buildError}");
                    continue;
                }

                var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
                int greedyBudget = Mathf.Max(1200, plan.Chains * 10);
                bool greedy = GreedyValidator.TryClearAllByGreedy(board, rules, greedyBudget, out _);
                if (!greedy)
                    continue;

                int actualChains = def.authoredLevel?.arrows?.Count ?? 0;
                int actualTiles = CountTiles(def.authoredLevel);
                float actualCoverage = actualTiles / (float)Mathf.Max(1, def.authoredLevel.width * def.authoredLevel.height);
                string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{OutputFolder}/{id}.asset");
                AssetDatabase.CreateAsset(def, assetPath);
                generated.Add(def);
                report.Add($"{generated.Count},{id},{assetPath},{plan.A.Name},{plan.B.Name},{LayoutLabel(plan)},{plan.Width},{plan.Height},{actualChains},{actualTiles},{actualCoverage:0.000},{plan.Aspect:0.000},{greedy}");
                serial++;
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            SavePack(generated);
            WriteReport(report);

            Debug.Log($"[CompositeSeedVariant] Generated {generated.Count}/{TargetCount} composite AB seeds. sources={sources.Count}, plans={plans.Count}, report={ReportPath}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build 50 Composite AB Seeds And Compare Triplets")]
        public static void Build50CompositeABSeedsAndCompareTriplets()
        {
            Build50CompositeABSeeds();
            BuildCompositeABCompareTriplets();
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Composite AB Compare Triplets")]
        public static void BuildCompositeABCompareTriplets()
        {
            EnsureFolder(Path.GetDirectoryName(ComparePackPath)?.Replace("\\", "/"));

            string reportFullPath = Path.GetFullPath(Path.Combine(Application.dataPath, "..", ReportPath));
            if (!File.Exists(reportFullPath))
            {
                Debug.LogError($"[CompositeSeedVariant] Missing report: {ReportPath}. Build 50 composites first.");
                return;
            }

            var byName = LoadLevelDefinitionsByFileName();
            var rows = LoadReportRows(reportFullPath);
            var levels = new List<LevelDefinition>(rows.Count * 3);
            var compareLines = new List<string>
            {
                "slot,group,kind,levelId,path,sourceA,sourceB,variant"
            };

            for (int i = 0; i < rows.Count; i++)
            {
                ReportRow row = rows[i];
                if (!byName.TryGetValue(row.SourceA, out var sourceA))
                {
                    Debug.LogWarning($"[CompositeSeedVariant] Missing source A {row.SourceA} for {row.Id}");
                    continue;
                }

                if (!byName.TryGetValue(row.SourceB, out var sourceB))
                {
                    Debug.LogWarning($"[CompositeSeedVariant] Missing source B {row.SourceB} for {row.Id}");
                    continue;
                }

                var variant = AssetDatabase.LoadAssetAtPath<LevelDefinition>(row.Path);
                if (variant == null)
                {
                    Debug.LogWarning($"[CompositeSeedVariant] Missing variant {row.Path}");
                    continue;
                }

                int group = i + 1;
                AppendCompareLevel(levels, compareLines, group, "A", sourceA.Asset, sourceA.Path, row);
                AppendCompareLevel(levels, compareLines, group, "B", sourceB.Asset, sourceB.Path, row);
                AppendCompareLevel(levels, compareLines, group, "A+B", variant, row.Path, row);
            }

            SavePackAt(ComparePackPath, "composite_seed_variants_ab_compare_triplets", "Composite Seed Variants AB Compare Triplets", levels);
            WriteLines(CompareReportPath, compareLines);
            Debug.Log($"[CompositeSeedVariant] Compare triplet pack levels={levels.Count}, groups={levels.Count / 3}, path={ComparePackPath}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Raw Preserve AB Compare Triplets")]
        public static void BuildRawPreserveABCompareTriplets()
        {
            EnsureFolder(RawOutputFolder);
            EnsureFolder(Path.GetDirectoryName(RawComparePackPath)?.Replace("\\", "/"));
            ClearFolder(RawOutputFolder);

            var sources = LoadPlainSources();
            var plans = BuildPlans(sources);
            var levels = new List<LevelDefinition>(TargetCount * 3);
            var report = new List<string>
            {
                "slot,group,kind,levelId,path,sourceA,sourceB,variant,layout,width,height,chains,coverage,crossBlocks"
            };

            int group = 0;
            for (int i = 0; i < plans.Count && group < TargetCount; i++)
            {
                Plan plan = plans[i];
                if (!TryBuildRawComposite(plan, group + 1, out var variant, out string id, out int crossBlocks, out string details))
                {
                    Debug.LogWarning($"[CompositeSeedVariantRaw] Skip {MakePairKey(plan)}: {details}");
                    continue;
                }

                string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{RawOutputFolder}/{id}.asset");
                AssetDatabase.CreateAsset(variant, assetPath);
                group++;

                AppendRawCompareLevel(levels, report, group, "A", plan.A.Def, plan.A.Path, plan, variant, assetPath, crossBlocks);
                AppendRawCompareLevel(levels, report, group, "B", plan.B.Def, plan.B.Path, plan, variant, assetPath, crossBlocks);
                AppendRawCompareLevel(levels, report, group, "A+B raw", variant, assetPath, plan, variant, assetPath, crossBlocks);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            SavePackAt(RawComparePackPath, "composite_seed_variants_ab_raw_preserve_triplets", "Composite Seed Variants AB Raw Preserve Triplets", levels);
            WriteLines(RawCompareReportPath, report);
            Debug.Log($"[CompositeSeedVariantRaw] Raw preserve triplet pack levels={levels.Count}, groups={levels.Count / 3}, path={RawComparePackPath}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Greedy Repair AB Compare Triplets")]
        public static void BuildGreedyRepairABCompareTriplets()
        {
            BuildGreedyRepairTriplets(
                GreedyOutputFolder,
                GreedyComparePackPath,
                GreedyCompareReportPath,
                "composite_seed_variants_ab_greedy_repair_triplets",
                "Composite Seed Variants AB Greedy Repair Triplets",
                TargetCount,
                2500,
                "CompositeSeedVariantGreedy");
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Greedy Repair Smoke Triplets")]
        public static void BuildGreedyRepairSmokeTriplets()
        {
            BuildGreedyRepairTriplets(
                GreedySmokeOutputFolder,
                GreedySmokeComparePackPath,
                GreedySmokeCompareReportPath,
                "composite_seed_variants_ab_greedy_repair_smoke_triplets",
                "Composite Seed Variants AB Greedy Repair Smoke Triplets",
                3,
                120,
                "CompositeSeedVariantGreedySmoke");
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Score Top Greedy Repair Triplets")]
        public static void BuildScoreTopGreedyRepairTriplets()
        {
            BuildGreedyRepairTriplets(
                ScoreTopOutputFolder,
                ScoreTopComparePackPath,
                ScoreTopCompareReportPath,
                "composite_seed_variants_ab_score_top_triplets",
                "Composite Seed Variants AB Score Top Triplets",
                10,
                5000,
                "CompositeSeedVariantScoreTop",
                true,
                24000);
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Production Parents 20")]
        public static void BuildProductionParents20()
        {
            BuildGreedyRepairTriplets(
                ProductionParentOutputFolder,
                ProductionParentPackPath,
                ProductionParentReportPath,
                "composite_seed_variants_ab_production_parents20",
                "Composite Seed Variants AB Production Parents 20",
                ProductionParentCount,
                12000,
                "CompositeSeedVariantProductionParents20",
                true,
                40000);
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Production 100 Expanded Pack")]
        public static void BuildProduction100ExpandedPack()
        {
            BuildExpandedPackFromSources(
                Production100OutputFolder,
                Production100PackPath,
                Production100ReportPath,
                "composite_seed_variants_ab_production100",
                "Composite Seed Variants AB Production 100",
                "CompositeSeedVariantProduction100",
                "composite_production100",
                ProductionPackCount,
                ProductionParentCount,
                int.MaxValue,
                false,
                ProductionParentOutputFolder,
                0,
                ScoreTopOutputFolder,
                GreedySmokeOutputFolder,
                GreedyOutputFolder);
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Final Parents 40")]
        public static void BuildFinalParents40()
        {
            BuildGreedyRepairTriplets(
                FinalParentOutputFolder,
                FinalParentPackPath,
                FinalParentReportPath,
                "composite_seed_variants_ab_final_parents40",
                "Composite Seed Variants AB Final Parents 40",
                FinalParentCount,
                40000,
                "CompositeSeedVariantFinalParents40",
                true,
                0,
                diversity: CreateFinalParentDiversityOptions(FinalParentCount));
        }

        public static void BuildFinalParents25()
        {
            BuildFinalParents40();
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Capacity Probe Parents 80")]
        public static void BuildCapacityProbeParents80()
        {
            BuildGreedyRepairTriplets(
                CapacityProbeParentOutputFolder,
                CapacityProbeParentPackPath,
                CapacityProbeParentReportPath,
                "composite_seed_variants_ab_capacity_probe_parents80",
                "Composite Seed Variants AB Capacity Probe Parents 80",
                CapacityProbeParentCount,
                120000,
                "CompositeSeedVariantCapacityProbeParents80",
                true,
                0);
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Capacity Audit Parents 150 Family LT10")]
        public static void BuildCapacityAuditParents150FamilyLt10()
        {
            BuildGreedyRepairTriplets(
                CapacityAuditParent150OutputFolder,
                CapacityAuditParent150PackPath,
                CapacityAuditParent150ReportPath,
                "composite_seed_variants_ab_capacity_audit_parents150",
                "Composite Seed Variants AB Capacity Audit Parents 150 Family < 10",
                CapacityAuditParentCount,
                120000,
                "CompositeSeedVariantCapacityAuditParents150",
                true,
                0,
                diversity: CreateCapacityAuditParentDiversityOptions());
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Capacity Audit Parents 150 Folder Pack")]
        public static void BuildCapacityAuditParents150FolderPack()
        {
            BuildGeneratedFolderPack(
                CapacityAuditParent150OutputFolder,
                CapacityAuditParent150PackPath,
                CapacityAuditParent150ReportPath,
                "composite_seed_variants_ab_capacity_audit_parents150",
                "Composite Seed Variants AB Capacity Audit Parents 150",
                "CompositeSeedVariantCapacityAuditParents150");
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Resume Capacity Audit Parents 150 Segment")]
        public static void ResumeCapacityAuditParents150FamilyLt10Segment()
        {
            BuildGreedyRepairTripletsResumeSegment(
                CapacityAuditParent150OutputFolder,
                CapacityAuditParent150PackPath,
                CapacityAuditParent150ReportPath,
                CapacityAuditParent150FailuresPath,
                "composite_seed_variants_ab_capacity_audit_parents150",
                "Composite Seed Variants AB Capacity Audit Parents 150",
                CapacityAuditParentCount,
                CapacityAuditParentSegmentSize,
                CapacityAuditParentSegmentMaxPlanChecks,
                "CompositeSeedVariantCapacityAuditParents150Resume",
                true,
                0,
                CapacityAuditParent150PlanOrderPath,
                CreateCapacityAuditParentDiversityOptions());
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Capacity Audit Parents 150 Portrait Pack")]
        public static void BuildCapacityAuditParents150PortraitPack()
        {
            BuildPortraitPackFromGeneratedSources(
                CapacityAuditParent150OutputFolder,
                CapacityAuditParent150PortraitOutputFolder,
                CapacityAuditParent150PortraitPackPath,
                CapacityAuditParent150PortraitReportPath,
                "composite_seed_variants_ab_capacity_audit_parents150_portrait",
                "Composite Seed Variants AB Capacity Audit Parents 150 Portrait",
                "CompositeSeedVariantCapacityAuditParents150Portrait",
                "composite_portrait150");
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Portrait Variant 50 Preview Pack")]
        public static void BuildPortraitVariant50PreviewPack()
        {
            BuildPortraitVariantPackFromSources(
                CapacityAuditParent150PortraitOutputFolder,
                PortraitVariant50OutputFolder,
                PortraitVariant50PackPath,
                PortraitVariant50ReportPath,
                "composite_seed_variants_ab_portrait_variant50",
                "Composite Seed Variants AB Portrait Variant 50",
                "CompositeSeedVariantPortraitVariant50",
                "composite_portrait_v50",
                PortraitVariantPreviewCount);
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Portrait Variant 10 Preview Pack")]
        public static void BuildPortraitVariant10PreviewPack()
        {
            BuildPortraitVariantPackFromSources(
                CapacityAuditParent150PortraitOutputFolder,
                PortraitVariant50OutputFolder,
                PortraitVariant50PackPath,
                PortraitVariant50ReportPath,
                "composite_seed_variants_ab_portrait_variant_preview10",
                "Composite Seed Variants AB Portrait Variant Preview 10",
                "CompositeSeedVariantPortraitVariant10",
                "composite_portrait_v10",
                PortraitVariantFastPreviewCount);
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build R1 R2 Outer Strong Repair Preview Pack")]
        public static void BuildR1R2OuterStrongRepairPreviewPack()
        {
            AssetDatabase.Refresh();

            var preview = new List<OuterStrongPreviewCandidate>();
            var summary = new List<string>
            {
                "pack,inputPack,outputPack,sourceCount,outputCount,changed,improved,greedyFail,missing,avgOuter1Before,avgOuter1After,avgOuter2Before,avgOuter2After,maxEmptyBefore,maxEmptyAfter,edgeShortBefore,edgeShortAfter,edgeLongBefore,edgeLongAfter"
            };

            var r1 = BuildOuterStrongRepairPack(
                "R1",
                R1FormalFinalPackPath,
                R1OuterStrongOutputFolder,
                R1OuterStrongPackPath,
                R1OuterStrongReportPath,
                "r1_outer_strong",
                "R1 Outer Strong Repair Pack",
                preview);
            AppendOuterStrongSummary(summary, r1);

            var r2 = BuildOuterStrongRepairPack(
                "R2",
                R2FinalRotationPackPath,
                R2OuterStrongOutputFolder,
                R2OuterStrongPackPath,
                R2OuterStrongReportPath,
                "r2_outer_strong",
                "R2 Outer Strong Repair Pack",
                preview);
            AppendOuterStrongSummary(summary, r2);

            preview.Sort((a, b) => b.Priority.CompareTo(a.Priority));
            var previewLevels = new List<LevelDefinition>(Mathf.Min(R1R2OuterStrongPreviewCount, preview.Count));
            for (int i = 0; i < preview.Count && previewLevels.Count < R1R2OuterStrongPreviewCount; i++)
                previewLevels.Add(preview[i].Level);

            EnsureFolder(Path.GetDirectoryName(R1R2OuterStrongPreviewPackPath)?.Replace("\\", "/"));
            var previewPack = SavePackAt(
                R1R2OuterStrongPreviewPackPath,
                "r1_r2_outer_strong_repair_preview",
                "R1 R2 Outer Strong Repair Preview",
                previewLevels);
            AttachPackToDemo(previewPack, "R1R2OuterStrongRepair");
            WriteLines(R1R2OuterStrongSummaryPath, summary);

            Debug.Log($"[R1R2OuterStrongRepair] Preview levels={previewLevels.Count}, R1={r1.OutputCount}/{r1.SourceCount}, R2={r2.OutputCount}/{r2.SourceCount}, path={R1R2OuterStrongPreviewPackPath}");
        }

        private static void FinalizeOuterStrongPack(
            string label,
            string sourcePackPath,
            string finalPackPath,
            string packId,
            string displayName,
            List<string> summary)
        {
            var sourcePack = AssetDatabase.LoadAssetAtPath<LevelPack>(sourcePackPath);
            if (sourcePack?.levels == null || sourcePack.levels.Length == 0)
            {
                summary.Add($"{EscapeCsv(label)},{EscapeCsv(sourcePackPath)},{EscapeCsv(finalPackPath)},0,missing_source");
                Debug.LogWarning($"[R1R2OuterStrongFinal] Missing source pack {label}: {sourcePackPath}");
                return;
            }

            var levels = new List<LevelDefinition>(sourcePack.levels.Length);
            for (int i = 0; i < sourcePack.levels.Length; i++)
            {
                var level = sourcePack.levels[i];
                if (level != null)
                    levels.Add(level);
            }

            SavePackAt(finalPackPath, packId, displayName, levels);
            summary.Add($"{EscapeCsv(label)},{EscapeCsv(sourcePackPath)},{EscapeCsv(finalPackPath)},{levels.Count},ok");
            Debug.Log($"[R1R2OuterStrongFinal] {label} final levels={levels.Count}, path={finalPackPath}");
        }

        private static void FinalizeOuterStrongPackWithRestore(
            string label,
            string repairedPackPath,
            string repairReportPath,
            string finalPackPath,
            string packId,
            string displayName,
            List<string> summary,
            List<string> restoreReport)
        {
            var rows = LoadOuterStrongRepairRows(repairReportPath);
            if (rows.Count == 0)
            {
                summary.Add($"{EscapeCsv(label)},{EscapeCsv(repairedPackPath)},{EscapeCsv(finalPackPath)},0,0,0,missing_report");
                Debug.LogWarning($"[R1R2OuterStrongFinal] Missing or empty repair report {label}: {repairReportPath}");
                FinalizeOuterStrongPack(label, repairedPackPath, finalPackPath, packId, displayName, new List<string>());
                return;
            }

            var levels = new List<LevelDefinition>(rows.Count);
            int restored = 0;
            int keptRepair = 0;
            int missing = 0;
            int filteredSparse = 0;
            for (int i = 0; i < rows.Count; i++)
            {
                var row = rows[i];
                if (row.Status == "missing_level" || string.IsNullOrWhiteSpace(row.SourceId))
                {
                    missing++;
                    restoreReport.Add($"{EscapeCsv(label)},{row.Rank},skip_missing,{EscapeCsv(row.SourceId)},{EscapeCsv(row.SourcePath)},{EscapeCsv(row.OutputPath)},{EscapeCsv(row.Mode)},{row.EdgeLongBefore},{row.EdgeLongAfter},{row.MaxEdgeRunAfter},0,0,missing_level");
                    continue;
                }

                bool restoreOriginal = ShouldRestoreOuterStrongRow(row);
                string selectedPath = restoreOriginal ? row.SourcePath : row.OutputPath;
                var selected = AssetDatabase.LoadAssetAtPath<LevelDefinition>(selectedPath);
                if (selected == null && restoreOriginal)
                {
                    selectedPath = row.OutputPath;
                    selected = AssetDatabase.LoadAssetAtPath<LevelDefinition>(selectedPath);
                }

                if (selected == null && !restoreOriginal)
                {
                    selectedPath = row.SourcePath;
                    selected = AssetDatabase.LoadAssetAtPath<LevelDefinition>(selectedPath);
                    restoreOriginal = selected != null;
                }

                if (selected == null)
                {
                    missing++;
                    restoreReport.Add($"{EscapeCsv(label)},{row.Rank},missing_asset,{EscapeCsv(row.SourceId)},{EscapeCsv(row.SourcePath)},{EscapeCsv(row.OutputPath)},{EscapeCsv(row.Mode)},{row.EdgeLongBefore},{row.EdgeLongAfter},{row.MaxEdgeRunAfter},0,0,selected_missing");
                    continue;
                }

                int selectedChains = selected.authoredLevel?.arrows?.Count ?? 0;
                int selectedTiles = CountTiles(selected.authoredLevel);
                if (selectedChains < OuterStrongFinalMinChains || selectedTiles < OuterStrongFinalMinTiles)
                {
                    filteredSparse++;
                    restoreReport.Add($"{EscapeCsv(label)},{row.Rank},filter_sparse,{EscapeCsv(row.SourceId)},{EscapeCsv(row.SourcePath)},{EscapeCsv(selectedPath)},{EscapeCsv(row.Mode)},{row.EdgeLongBefore},{row.EdgeLongAfter},{row.MaxEdgeRunAfter},{selectedChains},{selectedTiles},too_small");
                    continue;
                }

                levels.Add(selected);
                if (restoreOriginal)
                    restored++;
                else
                    keptRepair++;

                string decision = restoreOriginal ? "restore_original" : "keep_repair";
                string reason = restoreOriginal ? "straightened_by_outer_fill" : "ok";
                restoreReport.Add($"{EscapeCsv(label)},{row.Rank},{decision},{EscapeCsv(row.SourceId)},{EscapeCsv(row.SourcePath)},{EscapeCsv(selectedPath)},{EscapeCsv(row.Mode)},{row.EdgeLongBefore},{row.EdgeLongAfter},{row.MaxEdgeRunAfter},{selectedChains},{selectedTiles},{reason}");
            }

            SavePackAt(finalPackPath, packId, displayName, levels);
            summary.Add($"{EscapeCsv(label)},{EscapeCsv(repairedPackPath)},{EscapeCsv(finalPackPath)},{levels.Count},{restored},{filteredSparse},{missing},ok");
            Debug.Log($"[R1R2OuterStrongFinal] {label} final levels={levels.Count}, restored={restored}, keptRepair={keptRepair}, filteredSparse={filteredSparse}, missing={missing}, path={finalPackPath}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build R1 R2 Outer Strong Final Packs")]
        public static void BuildR1R2OuterStrongFinalPacks()
        {
            AssetDatabase.Refresh();
            EnsureFolder(Path.GetDirectoryName(R1OuterStrongFinalPackPath)?.Replace("\\", "/"));
            EnsureFolder(Path.GetDirectoryName(R2OuterStrongFinalPackPath)?.Replace("\\", "/"));

            var summary = new List<string>
            {
                "pack,sourcePack,finalPack,count,restoredStraightened,filteredSparse,missing,status"
            };
            var restoreReport = new List<string>
            {
                "pack,rank,decision,sourceId,sourcePath,selectedPath,mode,edgeLongBefore,edgeLongAfter,maxEdgeRunAfter,selectedChains,selectedTiles,reason"
            };

            FinalizeOuterStrongPackWithRestore(
                "R1",
                R1OuterStrongPackPath,
                R1OuterStrongReportPath,
                R1OuterStrongFinalPackPath,
                "r1_outer_strong_final_pack",
                "R1 Outer Strong Final Pack",
                summary,
                restoreReport);

            FinalizeOuterStrongPackWithRestore(
                "R2",
                R2OuterStrongPackPath,
                R2OuterStrongReportPath,
                R2OuterStrongFinalPackPath,
                "r2_outer_strong_final_pack",
                "R2 Outer Strong Final Pack",
                summary,
                restoreReport);

            WriteLines(R1R2OuterStrongFinalSummaryPath, summary);
            WriteLines(R1R2OuterStrongFinalRestoreReportPath, restoreReport);
            Debug.Log($"[R1R2OuterStrongFinal] Final packs updated: R1={R1OuterStrongFinalPackPath}, R2={R2OuterStrongFinalPackPath}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Select R2 Outer Strong Final Pack")]
        public static void SelectR2OuterStrongFinalPack()
        {
            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(R2OuterStrongFinalPackPath);
            if (pack == null)
            {
                Debug.LogError($"[R2OuterStrongFinal] Missing pack: {R2OuterStrongFinalPackPath}");
                return;
            }

            AttachPackToDemo(pack, "R2OuterStrongFinal");
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Select R2 Outer Strong A484 Single Pack")]
        public static void SelectR2OuterStrongA484SinglePack()
        {
            var level = ResolveOuterStrongSingleLevel(R2OuterStrongReportPath, 30, R2OuterStrongA484LevelPath);
            if (level == null)
            {
                Debug.LogError($"[R2OuterStrongA484] Missing level: {R2OuterStrongA484LevelPath}");
                return;
            }

            EnsureFolder(Path.GetDirectoryName(R2OuterStrongA484SinglePackPath)?.Replace("\\", "/"));
            var pack = SavePackAt(
                R2OuterStrongA484SinglePackPath,
                "r2_outer_strong_a484_single_pack",
                "R2 Outer Strong A484 Single",
                new List<LevelDefinition> { level });
            AttachPackToDemo(pack, "R2OuterStrongA484");
        }

        private static LevelDefinition ResolveOuterStrongSingleLevel(string reportPath, int rank, string fallbackPath)
        {
            var rows = LoadOuterStrongRepairRows(reportPath);
            for (int i = 0; i < rows.Count; i++)
            {
                var row = rows[i];
                if (row.Rank != rank)
                    continue;

                string selectedPath = ShouldRestoreOuterStrongRow(row) ? row.SourcePath : row.OutputPath;
                var selected = AssetDatabase.LoadAssetAtPath<LevelDefinition>(selectedPath);
                if (selected != null)
                    return selected;
            }

            return AssetDatabase.LoadAssetAtPath<LevelDefinition>(fallbackPath);
        }

        private static bool ShouldRestoreOuterStrongRow(OuterStrongRepairReportRow row)
        {
            if (row == null || row.Status != "repaired")
                return false;

            int edgeLongDelta = row.EdgeLongAfter - row.EdgeLongBefore;
            return row.EdgeLongAfter >= OuterStrongRestoreEdgeLongThreshold ||
                   (row.EdgeLongAfter >= OuterStrongRestoreSoftEdgeLongThreshold &&
                    edgeLongDelta >= OuterStrongRestoreEdgeLongDeltaThreshold &&
                    row.MaxEdgeRunAfter >= OuterStrongRestoreMaxEdgeRunThreshold);
        }

        private static List<OuterStrongRepairReportRow> LoadOuterStrongRepairRows(string projectRelativeReportPath)
        {
            var rows = new List<OuterStrongRepairReportRow>();
            string full = ProjectRelativeToFullPath(projectRelativeReportPath);
            if (!File.Exists(full))
                return rows;

            string[] lines = File.ReadAllLines(full);
            if (lines.Length <= 1)
                return rows;

            var header = SplitCsvLine(lines[0]);
            var index = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < header.Count; i++)
            {
                if (!index.ContainsKey(header[i]))
                    index.Add(header[i], i);
            }

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                var parts = SplitCsvLine(lines[i]);
                rows.Add(new OuterStrongRepairReportRow
                {
                    Rank = ReadCsvInt(parts, index, "rank"),
                    Status = ReadCsvString(parts, index, "status"),
                    SourceId = ReadCsvString(parts, index, "sourceId"),
                    SourcePath = ReadCsvString(parts, index, "sourcePath"),
                    OutputId = ReadCsvString(parts, index, "outputId"),
                    OutputPath = ReadCsvString(parts, index, "outputPath"),
                    Mode = ReadCsvString(parts, index, "mode"),
                    EdgeLongBefore = ReadCsvInt(parts, index, "edgeLongBefore"),
                    EdgeLongAfter = ReadCsvInt(parts, index, "edgeLongAfter"),
                    MaxEdgeRunAfter = ReadCsvInt(parts, index, "maxEdgeRunAfter")
                });
            }

            return rows;
        }

        private static string ReadCsvString(List<string> parts, Dictionary<string, int> index, string key)
        {
            if (parts == null || index == null || !index.TryGetValue(key, out int i) || i < 0 || i >= parts.Count)
                return string.Empty;
            return parts[i];
        }

        private static int ReadCsvInt(List<string> parts, Dictionary<string, int> index, string key)
        {
            string value = ReadCsvString(parts, index, key);
            return int.TryParse(value, out int result) ? result : 0;
        }

        private static List<string> SplitCsvLine(string line)
        {
            var parts = new List<string>();
            if (line == null)
                return parts;

            var current = new System.Text.StringBuilder();
            bool inQuotes = false;
            for (int i = 0; i < line.Length; i++)
            {
                char ch = line[i];
                if (ch == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        current.Append('"');
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
                    parts.Add(current.ToString());
                    current.Length = 0;
                    continue;
                }

                current.Append(ch);
            }

            parts.Add(current.ToString());
            return parts;
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Playability Production 150 Pack")]
        public static void BuildPlayabilityProduction150Pack()
        {
            BuildPortraitVariantPackFromSources(
                CapacityAuditParent150PortraitOutputFolder,
                PlayabilityCandidateOutputFolder,
                PlayabilityCandidatePackPath,
                PlayabilityCandidateReportPath,
                "composite_seed_variants_ab_playability_candidates360",
                "Composite Seed Variants AB Playability Candidates 360",
                "CompositeSeedVariantPlayabilityCandidates360",
                "composite_play_cand",
                PlayabilityCandidateCount,
                attachToDemo: false,
                maxVariantsPerSource: PlayabilityMaxVariantsPerSource);

            BuildPlayabilitySelectedPackFromFolder(
                PlayabilityCandidateOutputFolder,
                PlayabilityFinal150PackPath,
                PlayabilityFinal150ReportPath,
                "composite_seed_variants_ab_playability_final150",
                "Composite Seed Variants AB Playability Final 150",
                "CompositeSeedVariantPlayabilityFinal150",
                PlayabilityFinalCount,
                attachToDemo: true);
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Select Existing Playability Final 150 Pack")]
        public static void SelectExistingPlayabilityFinal150Pack()
        {
            BuildPlayabilitySelectedPackFromFolder(
                PlayabilityCandidateOutputFolder,
                PlayabilityFinal150PackPath,
                PlayabilityFinal150ReportPath,
                "composite_seed_variants_ab_playability_final150",
                "Composite Seed Variants AB Playability Final 150",
                "CompositeSeedVariantPlayabilityFinal150",
                PlayabilityFinalCount,
                attachToDemo: true);
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Select Existing Playability Curated 100 Pack")]
        public static void SelectExistingPlayabilityCurated100Pack()
        {
            BuildPlayabilitySelectedPackFromFolder(
                PlayabilityCandidateOutputFolder,
                PlayabilityCurated100PackPath,
                PlayabilityCurated100ReportPath,
                "composite_seed_variants_ab_playability_curated100",
                "Composite Seed Variants AB Playability Curated 100",
                "CompositeSeedVariantPlayabilityCurated100",
                PlayabilityCuratedCount,
                attachToDemo: true,
                curatedMode: true);
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Playability Curated 85 Hole Trim Pack")]
        public static void BuildPlayabilityCurated85HoleTrimPack()
        {
            BuildHoleTrimmedPackFromPack(
                PlayabilityCurated100PackPath,
                PlayabilityCurated85PackPath,
                PlayabilityCurated85ReportPath,
                "composite_seed_variants_ab_playability_curated85",
                "Composite Seed Variants AB Playability Curated 85",
                "CompositeSeedVariantPlayabilityCurated85",
                PlayabilityCuratedHoleTrimCount,
                PlayabilityHoleTrimRemoveCount,
                attachToDemo: true);
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Select Existing Playability Balanced 120 Pack")]
        public static void SelectExistingPlayabilityBalanced120Pack()
        {
            BuildPlayabilityBalancedPackFromFolder(
                PlayabilityCandidateOutputFolder,
                PlayabilityBalanced120PackPath,
                PlayabilityBalanced120ReportPath,
                "composite_seed_variants_ab_playability_balanced120",
                "Composite Seed Variants AB Playability Balanced 120",
                "CompositeSeedVariantPlayabilityBalanced120",
                PlayabilityBalancedCount,
                attachToDemo: true);
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Final 100 Expanded Pack")]
        public static void BuildFinal100ExpandedPack()
        {
            BuildExpandedPackFromSources(
                Final100OutputFolder,
                Final100PackPath,
                Final100ReportPath,
                "composite_seed_variants_ab_final100",
                "Composite Seed Variants AB Final 100",
                "CompositeSeedVariantFinal100",
                "composite_final100",
                FinalPackCount,
                FinalParentCount,
                FinalMaxVariantsPerSource,
                true,
                FinalParentOutputFolder,
                0,
                ProductionParentOutputFolder,
                ScoreTopOutputFolder,
                GreedySmokeOutputFolder,
                GreedyOutputFolder);
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Capacity Probe 200 Pack")]
        public static void BuildCapacityProbe200Pack()
        {
            BuildExpandedPackFromSources(
                CapacityProbe200OutputFolder,
                CapacityProbe200PackPath,
                CapacityProbe200ReportPath,
                "composite_seed_variants_ab_capacity_probe200",
                "Composite Seed Variants AB Capacity Probe 200",
                "CompositeSeedVariantCapacityProbe200",
                "composite_probe200",
                CapacityProbePackCount,
                CapacityProbeParentCount,
                CapacityProbeMaxVariantsPerSource,
                true,
                CapacityProbeParentOutputFolder,
                maxSourceFamilyUse: CapacityProbeMaxFamilyUse);
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Capacity Probe 200 From Final Parents 40 Pack")]
        public static void BuildCapacityProbe200FromFinalParents40Pack()
        {
            BuildExpandedPackFromSources(
                CapacityProbe200FromFinal40OutputFolder,
                CapacityProbe200FromFinal40PackPath,
                CapacityProbe200FromFinal40ReportPath,
                "composite_seed_variants_ab_capacity_probe200_from_final40",
                "Composite Seed Variants AB Capacity Probe 200 From Final Parents 40",
                "CompositeSeedVariantCapacityProbe200FromFinal40",
                "composite_probe200f40",
                CapacityProbePackCount,
                FinalParentCount,
                FinalMaxVariantsPerSource,
                true,
                FinalParentOutputFolder,
                maxSourceFamilyUse: CapacityProbeMaxFamilyUse);
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Final Parent Folder Pack")]
        public static void BuildFinalParentFolderPack()
        {
            BuildGeneratedFolderPack(
                FinalParentOutputFolder,
                FinalParentPackPath,
                FinalParentReportPath,
                "composite_seed_variants_ab_final_parents40",
                "Composite Seed Variants AB Final Parents 40",
                "CompositeSeedVariantFinalParents40");
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Final Parents 40 And 100 Pack")]
        public static void BuildFinalParents40And100Pack()
        {
            BuildFinalParents40();
            BuildFinal100ExpandedPack();
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Capacity Probe Parents 80 And 200 Pack")]
        public static void BuildCapacityProbeParents80And200Pack()
        {
            BuildCapacityProbeParents80();
            BuildCapacityProbe200Pack();
        }

        public static void BuildFinalParents25And100Pack()
        {
            BuildFinalParents40And100Pack();
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Diverse Parents 30")]
        public static void BuildDiverseParents30()
        {
            BuildGreedyRepairTriplets(
                DiverseParentOutputFolder,
                DiverseParentPackPath,
                DiverseParentReportPath,
                "composite_seed_variants_ab_diverse_parents30",
                "Composite Seed Variants AB Diverse Parents 30",
                DiverseParentCount,
                26000,
                "CompositeSeedVariantDiverseParents30",
                true,
                80000,
                diversity: new ParentDiversityOptions
                {
                    StrictGroups = 12,
                    StrictMaxBaseSourceUse = 4,
                    RelaxedMaxBaseSourceUse = 6,
                    StrictMaxExactSourceUse = 2,
                    RelaxedMaxExactSourceUse = 3
                });
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build ABC Probe 10")]
        public static void BuildABCProbe10()
        {
            BuildABCProbe(
                AbcProbeOutputFolder,
                AbcProbePackPath,
                AbcProbeReportPath,
                "composite_seed_variants_abc_probe10",
                "Composite Seed Variants ABC Probe 10",
                AbcProbeCount);
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Dependency Repair Smoke 10")]
        public static void BuildDependencyRepairSmoke10()
        {
            BuildGreedyRepairTriplets(
                DependencySmokeOutputFolder,
                DependencySmokePackPath,
                DependencySmokeReportPath,
                "composite_seed_variants_ab_dependency_smoke10",
                "Composite Seed Variants AB Dependency Smoke 10",
                DependencySmokeCount,
                6000,
                "CompositeSeedVariantDependencySmoke10",
                true,
                16000,
                diversity: new ParentDiversityOptions
                {
                    StrictGroups = 6,
                    StrictMaxBaseSourceUse = 3,
                    RelaxedMaxBaseSourceUse = 5,
                    StrictMaxExactSourceUse = 2,
                    RelaxedMaxExactSourceUse = 3
                });
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Production Parent Folder Pack")]
        public static void BuildProductionParentFolderPack()
        {
            BuildGeneratedFolderPack(
                ProductionParentOutputFolder,
                ProductionParentPackPath,
                ProductionParentReportPath,
                "composite_seed_variants_ab_production_parents20",
                "Composite Seed Variants AB Production Parents",
                "CompositeSeedVariantProductionParents");
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Production Parents 20 And 100 Pack")]
        public static void BuildProductionParents20And100Pack()
        {
            BuildProductionParents20();
            BuildProduction100ExpandedPack();
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Score Top 10 Expanded Pack")]
        public static void BuildScoreTop10ExpandedPack()
        {
            EnsureFolder(ScoreTop10OutputFolder);
            EnsureFolder(Path.GetDirectoryName(ScoreTop10PackPath)?.Replace("\\", "/"));
            ClearFolder(ScoreTop10OutputFolder);

            var sourcePaths = new List<string>();
            AddGeneratedCompositeSources(sourcePaths, ScoreTop10OutputFolder);
            AddGeneratedCompositeSources(sourcePaths, ScoreTopOutputFolder);
            AddGeneratedCompositeSources(sourcePaths, GreedySmokeOutputFolder);
            AddGeneratedCompositeSources(sourcePaths, GreedyOutputFolder);

            var levels = new List<LevelDefinition>(10);
            var report = new List<string>
            {
                "rank,id,path,source,mode,width,height,chains,coverage,greedy,details"
            };
            var signatures = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string[] modes = { "identity", "hflip", "vflip", "rot180", "rot90", "rot270" };

            for (int s = 0; s < sourcePaths.Count && levels.Count < 10; s++)
            {
                string sourcePath = sourcePaths[s];
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source?.authoredLevel?.arrows == null)
                    continue;

                for (int m = 0; m < modes.Length && levels.Count < 10; m++)
                {
                    string mode = modes[m];
                    var authored = TransformAuthored(source.authoredLevel, mode);
                    string signature = MakeAuthoredSignature(authored);
                    if (!signatures.Add(signature))
                        continue;

                    if (!ValidateAuthoredGreedy(authored, out string details))
                        continue;

                    int rank = levels.Count + 1;
                    string id = $"composite_scoretop10_{rank:000}_{ShortName(source.levelId)}_{mode}";
                    var def = CreateDerivedDefinition(source, id, authored);
                    string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{ScoreTop10OutputFolder}/{id}.asset");
                    AssetDatabase.CreateAsset(def, assetPath);
                    levels.Add(def);

                    int chains = authored.arrows?.Count ?? 0;
                    float coverage = CountTiles(authored) / (float)Mathf.Max(1, authored.width * authored.height);
                    report.Add($"{rank},{id},{assetPath},{source.levelId},{mode},{authored.width},{authored.height},{chains},{coverage:0.000},True,{EscapeCsv(details)}");
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            SavePackAt(ScoreTop10PackPath, "composite_seed_variants_ab_score_top10", "Composite Seed Variants AB Score Top 10", levels);
            WriteLines(ScoreTop10ReportPath, report);
            Debug.Log($"[CompositeSeedVariantScoreTop10] Pack levels={levels.Count}, sourceAssets={sourcePaths.Count}, path={ScoreTop10PackPath}");
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Build Score Top 50 Expanded Pack")]
        public static void BuildScoreTop50ExpandedPack()
        {
            BuildExpandedPackFromSources(
                ScoreTop50OutputFolder,
                ScoreTop50PackPath,
                ScoreTop50ReportPath,
                "composite_seed_variants_ab_score_top50",
                "Composite Seed Variants AB Score Top 50",
                "CompositeSeedVariantScoreTop50",
                "composite_scoretop50",
                TargetCount,
                10,
                int.MaxValue,
                false,
                ScoreTopOutputFolder,
                0,
                GreedySmokeOutputFolder,
                GreedyOutputFolder,
                ScoreTop10OutputFolder);
        }

        private static void BuildExpandedPackFromSources(
            string outputFolder,
            string packPath,
            string reportPath,
            string packId,
            string displayName,
            string logTag,
            string idPrefix,
            int targetCount,
            int minimumPrimarySources,
            int maxVariantsPerSource,
            bool finalQualityFilter,
            string primarySourceFolder,
            int maxSourceFamilyUse = 0,
            params string[] fallbackSourceFolders)
        {
            EnsureFolder(outputFolder);
            EnsureFolder(Path.GetDirectoryName(packPath)?.Replace("\\", "/"));
            ClearFolder(outputFolder);

            var sourcePaths = new List<string>();
            AddGeneratedCompositeSources(sourcePaths, primarySourceFolder);
            if (sourcePaths.Count < minimumPrimarySources && fallbackSourceFolders != null)
            {
                for (int i = 0; i < fallbackSourceFolders.Length; i++)
                    AddGeneratedCompositeSources(sourcePaths, fallbackSourceFolders[i]);
            }

            var levels = new List<LevelDefinition>(targetCount);
            var report = new List<string>
            {
                "rank,id,path,source,transform,reversePattern,reversed,variantMode,addedChains,width,height,chains,coverage,greedy,visualPenalty,edgeLong,cornerLong,edgeShort,maxEdgeRun,details"
            };
            var signatures = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var sourceCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var sourceFamilyCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            string[] fillModes = { "none", "split4", "split8", "split12", "edge1", "edge2", "edge3", "edge4", "edge6", "edge8", "edge12", "edge16" };
            string[] reversePatterns = { "keep", "rev_all", "rev_even", "rev_odd", "rev_mod3_0", "rev_mod3_1", "rev_mod3_2", "rev_long", "rev_short", "rev_edge" };

            for (int r = 0; r < reversePatterns.Length && levels.Count < targetCount; r++)
            {
                for (int f = 0; f < fillModes.Length && levels.Count < targetCount; f++)
                {
                    for (int t = 0; t < StandardFinalTransforms.Length && levels.Count < targetCount; t++)
                    {
                        for (int s = 0; s < sourcePaths.Count && levels.Count < targetCount; s++)
                        {
                            string sourcePath = sourcePaths[s];
                            var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                            if (source?.authoredLevel?.arrows == null)
                                continue;
                            if (maxVariantsPerSource > 0 &&
                                maxVariantsPerSource < int.MaxValue &&
                                sourceCounts.TryGetValue(source.levelId, out int sourceCount) &&
                                sourceCount >= maxVariantsPerSource)
                                continue;

                            var sourceFamilies = maxSourceFamilyUse > 0
                                ? ExtractSourceFamiliesFromName(source.levelId)
                                : null;
                            if (maxSourceFamilyUse > 0 && !SourceFamiliesBelowLimit(sourceFamilies, sourceFamilyCounts, maxSourceFamilyUse))
                                continue;

                            string transform = GetPreferredFinalTransform(source.authoredLevel, t);
                            string reversePattern = reversePatterns[r];
                            string fillMode = fillModes[f];
                            var authored = TransformAuthored(source.authoredLevel, transform);
                            int reversed = ApplyReversePattern(authored, reversePattern);
                            int filled = ApplyEdgeFillMode(authored, fillMode);
                            string qualityDetails = string.Empty;
                            if (finalQualityFilter && !PassFinalPackQuality(authored, out qualityDetails))
                                continue;

                            string signature = MakeAuthoredSignature(authored);
                            if (!signatures.Add(signature))
                                continue;

                            if (!ValidateAuthoredGreedy(authored, out string details))
                                continue;
                            if (!string.IsNullOrEmpty(qualityDetails))
                                details = $"{details}; {qualityDetails}";

                            int rank = levels.Count + 1;
                            string id = $"{idPrefix}_{rank:000}_{ShortName(source.levelId)}_{transform}_{reversePattern}_{fillMode}";
                            var def = CreateDerivedDefinition(source, id, authored);
                            string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{outputFolder}/{id}.asset");
                            AssetDatabase.CreateAsset(def, assetPath);
                            levels.Add(def);
                            sourceCounts[source.levelId] = sourceCounts.TryGetValue(source.levelId, out int currentCount) ? currentCount + 1 : 1;
                            if (maxSourceFamilyUse > 0)
                                RecordSourceFamilies(sourceFamilies, sourceFamilyCounts);

                            int chains = authored.arrows?.Count ?? 0;
                            float coverage = CountTiles(authored) / (float)Mathf.Max(1, authored.width * authored.height);
                            VisualQualityMetrics visual = CalculateVisualQualityMetrics(authored);
                            report.Add($"{rank},{id},{assetPath},{source.levelId},{transform},{reversePattern},{reversed},{fillMode},{filled},{authored.width},{authored.height},{chains},{coverage:0.000},True,{visual.Penalty},{visual.EdgeLong},{visual.CornerLong},{visual.EdgeShort},{visual.MaxEdgeRun},{EscapeCsv(details)}");
                        }
                    }
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            SavePackAt(packPath, packId, displayName, levels);
            WriteLines(reportPath, report);
            Debug.Log($"[{logTag}] Pack levels={levels.Count}, sourceAssets={sourcePaths.Count}, path={packPath}");
        }

        private static void BuildABCProbe(
            string outputFolder,
            string packPath,
            string reportPath,
            string packId,
            string displayName,
            int targetCount)
        {
            EnsureFolder(outputFolder);
            EnsureFolder(Path.GetDirectoryName(packPath)?.Replace("\\", "/"));
            ClearFolder(outputFolder);

            var abSources = LoadGeneratedCompositeSourceSeeds(
                FinalParentOutputFolder,
                ProductionParentOutputFolder,
                ScoreTopOutputFolder,
                GreedyOutputFolder,
                GreedySmokeOutputFolder);
            var cSources = LoadPlainSources();
            cSources.RemoveAll(source => source.Chains < 40 || source.Chains > 70);

            var plans = new List<Plan>();
            for (int a = 0; a < abSources.Count; a++)
            {
                for (int c = 0; c < cSources.Count; c++)
                {
                    SourceSeed ab = abSources[a];
                    SourceSeed extra = cSources[c];
                    if (CompositeNameContainsBaseLevel(ab.Name, extra))
                        continue;

                    for (int gap = 0; gap <= 1; gap++)
                    {
                        AddPlanIfValid(plans, ab, extra, horizontal: true, gap);
                        AddPlanIfValid(plans, extra, ab, horizontal: true, gap);
                        AddPlanIfValid(plans, ab, extra, horizontal: false, gap);
                        AddPlanIfValid(plans, extra, ab, horizontal: false, gap);
                    }
                }
            }

            plans.Sort(CompareGreedySmokePlans);
            var levels = new List<LevelDefinition>(targetCount);
            var report = new List<string>
            {
                "rank,id,path,sourceA,sourceB,layout,width,height,chains,coverage,crossBlocks,greedy,repair,details"
            };
            var signatures = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var exactSourceCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            int checkedPlans = 0;
            for (int i = 0; i < plans.Count && levels.Count < targetCount && checkedPlans < 8000; i++)
            {
                Plan plan = plans[i];
                if (!SourceUseBelowLimit(plan.A.Name, exactSourceCounts, 2) ||
                    !SourceUseBelowLimit(plan.B.Name, exactSourceCounts, 2))
                    continue;

                checkedPlans++;
                if (!TryBuildGreedyRepairComposite(plan, levels.Count + 1, out var def, out string id, out int crossBlocks, out string repair, out string details))
                    continue;

                id = id.Replace("composite_greedy_ab_", "composite_greedy_abc_");
                def.levelId = id;
                def.name = id;
                string signature = MakeAuthoredSignature(def.authoredLevel);
                if (!signatures.Add(signature))
                    continue;

                string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{outputFolder}/{id}.asset");
                AssetDatabase.CreateAsset(def, assetPath);
                levels.Add(def);
                IncrementCount(exactSourceCounts, plan.A.Name);
                IncrementCount(exactSourceCounts, plan.B.Name);

                int chains = def.authoredLevel?.arrows?.Count ?? 0;
                float coverage = CountTiles(def.authoredLevel) / (float)Mathf.Max(1, def.authoredLevel.width * def.authoredLevel.height);
                report.Add($"{levels.Count},{id},{assetPath},{plan.A.Name},{plan.B.Name},{LayoutLabel(plan)},{def.authoredLevel.width},{def.authoredLevel.height},{chains},{coverage:0.000},{crossBlocks},True,{EscapeCsv(repair)},{EscapeCsv(details)}");
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            SavePackAt(packPath, packId, displayName, levels);
            WriteLines(reportPath, report);
            Debug.Log($"[CompositeSeedVariantABCProbe] Pack levels={levels.Count}, checkedPlans={checkedPlans}/{plans.Count}, abSources={abSources.Count}, cSources={cSources.Count}, path={packPath}");
        }

        private static void BuildGeneratedFolderPack(
            string sourceFolder,
            string packPath,
            string reportPath,
            string packId,
            string displayName,
            string logTag)
        {
            EnsureFolder(Path.GetDirectoryName(packPath)?.Replace("\\", "/"));

            var sourcePaths = new List<string>();
            AddGeneratedCompositeSources(sourcePaths, sourceFolder);
            var levels = new List<LevelDefinition>(sourcePaths.Count);
            var report = new List<string>
            {
                "rank,id,path,width,height,chains,coverage,greedy,details"
            };

            for (int i = 0; i < sourcePaths.Count; i++)
            {
                string sourcePath = sourcePaths[i];
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source?.authoredLevel?.arrows == null)
                    continue;

                if (!ValidateAuthoredGreedy(source.authoredLevel, out string details))
                    continue;

                levels.Add(source);
                int chains = source.authoredLevel.arrows?.Count ?? 0;
                float coverage = CountTiles(source.authoredLevel) / (float)Mathf.Max(1, source.authoredLevel.width * source.authoredLevel.height);
                report.Add($"{levels.Count},{source.levelId},{sourcePath},{source.authoredLevel.width},{source.authoredLevel.height},{chains},{coverage:0.000},True,{EscapeCsv(details)}");
            }

            SavePackAt(packPath, packId, displayName, levels);
            WriteLines(reportPath, report);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"[{logTag}] Folder pack levels={levels.Count}, sourceAssets={sourcePaths.Count}, path={packPath}");
        }

        private static void BuildPortraitPackFromGeneratedSources(
            string sourceFolder,
            string outputFolder,
            string packPath,
            string reportPath,
            string packId,
            string displayName,
            string logTag,
            string idPrefix)
        {
            EnsureFolder(outputFolder);
            EnsureFolder(Path.GetDirectoryName(packPath)?.Replace("\\", "/"));
            ClearFolder(outputFolder);

            var sourcePaths = new List<string>();
            AddGeneratedCompositeSources(sourcePaths, sourceFolder);
            var levels = new List<LevelDefinition>(sourcePaths.Count);
            var report = new List<string>
            {
                "rank,id,path,source,transform,width,height,chains,coverage,greedy,quality,details"
            };

            for (int i = 0; i < sourcePaths.Count; i++)
            {
                string sourcePath = sourcePaths[i];
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source?.authoredLevel?.arrows == null)
                    continue;

                string[] modes = GetPortraitTransformOrder(source.authoredLevel);
                bool accepted = false;
                string lastDetails = string.Empty;
                for (int m = 0; m < modes.Length; m++)
                {
                    string mode = modes[m];
                    var authored = TransformAuthored(source.authoredLevel, mode);
                    if (authored.width > authored.height)
                    {
                        lastDetails = $"not portrait {authored.width}x{authored.height}";
                        continue;
                    }

                    if (!PassFinalPackQuality(authored, out string qualityDetails))
                    {
                        lastDetails = qualityDetails;
                        continue;
                    }

                    if (!ValidateAuthoredGreedy(authored, out string greedyDetails))
                    {
                        lastDetails = greedyDetails;
                        continue;
                    }

                    int rank = levels.Count + 1;
                    string id = $"{idPrefix}_{rank:000}_{ShortName(source.levelId)}_{mode}";
                    var def = CreateDerivedDefinition(source, id, authored);
                    string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{outputFolder}/{id}.asset");
                    AssetDatabase.CreateAsset(def, assetPath);
                    levels.Add(def);

                    int chains = authored.arrows?.Count ?? 0;
                    float coverage = CountTiles(authored) / (float)Mathf.Max(1, authored.width * authored.height);
                    report.Add($"{rank},{id},{assetPath},{source.levelId},{mode},{authored.width},{authored.height},{chains},{coverage:0.000},True,{EscapeCsv(qualityDetails)},{EscapeCsv(greedyDetails)}");
                    accepted = true;
                    break;
                }

                if (!accepted)
                    Debug.LogWarning($"[{logTag}] Portrait reject source={source.levelId}, details={lastDetails}");
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            SavePackAt(packPath, packId, displayName, levels);
            WriteLines(reportPath, report);
            Debug.Log($"[{logTag}] Portrait pack levels={levels.Count}, sourceAssets={sourcePaths.Count}, path={packPath}");
        }

        private static string[] GetPortraitTransformOrder(AuthoredLevelData source)
        {
            int width = Mathf.Max(1, source?.width ?? 1);
            int height = Mathf.Max(1, source?.height ?? 1);
            return width > height
                ? new[] { "rot90", "rot270", "transpose", "antiTranspose" }
                : new[] { "identity", "hflip", "vflip", "rot180" };
        }

        private static void BuildPortraitVariantPackFromSources(
            string sourceFolder,
            string outputFolder,
            string packPath,
            string reportPath,
            string packId,
            string displayName,
            string logTag,
            string idPrefix,
            int targetCount,
            bool attachToDemo = true,
            int maxVariantsPerSource = 2)
        {
            EnsureFolder(outputFolder);
            EnsureFolder(Path.GetDirectoryName(packPath)?.Replace("\\", "/"));
            ClearFolder(outputFolder);

            var sourcePaths = new List<string>();
            AddGeneratedCompositeSources(sourcePaths, sourceFolder);
            var levels = new List<LevelDefinition>(targetCount);
            var report = new List<string>
            {
                "rank,id,path,source,transform,reversePattern,reversed,trimmedOuterExit,trimmedOuterShort,fillMode,outerProfile,addedChains,width,height,chains,coverage,outer1,outer2,maxOuterEmptyRun,visualPenalty,edgeLong,cornerLong,edgeShort,maxEdgeRun,greedy,details"
            };
            var signatures = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var sourceCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            string[] transforms = { "identity", "hflip", "vflip", "rot180" };
            string[] reversePatterns = { "keep", "rev_edge", "rev_long", "rev_even", "rev_odd" };
            string[] fillModes = { "edge120", "edge96", "edge84", "edge72", "edge60", "edge48", "edge40", "edge32", "edge24", "edge20", "edge16", "edge12", "edge8", "edge6", "edge4", "edge2", "none" };
            PortraitOuterProfile[] outerProfiles =
            {
                new PortraitOuterProfile { Name = "strict", MinBand1Coverage = StrictPortraitOuterBand1Coverage, MaxEmptyRun = StrictPortraitMaxOuterEmptyRun },
                new PortraitOuterProfile { Name = "balanced", MinBand1Coverage = BalancedPortraitOuterBand1Coverage, MaxEmptyRun = BalancedPortraitMaxOuterEmptyRun },
                new PortraitOuterProfile { Name = "fallback", MinBand1Coverage = FallbackPortraitOuterBand1Coverage, MaxEmptyRun = FallbackPortraitMaxOuterEmptyRun },
                new PortraitOuterProfile { Name = "floor", MinBand1Coverage = FloorPortraitOuterBand1Coverage, MaxEmptyRun = FloorPortraitMaxOuterEmptyRun }
            };
            var failureSummary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            for (int p = 0; p < outerProfiles.Length && levels.Count < targetCount; p++)
            {
                PortraitOuterProfile outerProfile = outerProfiles[p];
                for (int f = 0; f < fillModes.Length && levels.Count < targetCount; f++)
                {
                    string fillMode = fillModes[f];
                    for (int t = 0; t < transforms.Length && levels.Count < targetCount; t++)
                    {
                        string transform = transforms[t];
                        for (int r = 0; r < reversePatterns.Length && levels.Count < targetCount; r++)
                        {
                            string reversePattern = reversePatterns[r];
                            for (int s = 0; s < sourcePaths.Count && levels.Count < targetCount; s++)
                            {
                                string sourcePath = sourcePaths[s];
                                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                                if (source?.authoredLevel?.arrows == null)
                                    continue;
                                if (sourceCounts.TryGetValue(source.levelId, out int sourceCount) && sourceCount >= maxVariantsPerSource)
                                    continue;

                                var authored = TransformAuthored(source.authoredLevel, transform);
                                if (authored.width > authored.height)
                                {
                                    AddFailureSummary(failureSummary, "landscape");
                                    continue;
                                }

                                int reversed = ApplyReversePattern(authored, reversePattern);
                                int trimmedOuterExit = TrimOuterStraightExitChains(authored, maxRemove: 10, minLength: 7);
                                int trimmedOuterShort = TrimDenseOuterShortChains(
                                    authored,
                                    MaxPortraitOuterShortClusterTrim,
                                    PortraitOuterShortClusterRadius,
                                    maxNearbyShortChains: 3);
                                int filled = ApplyEdgeFillMode(authored, fillMode);
                                if (fillMode != "none" && filled <= 0)
                                {
                                    AddFailureSummary(failureSummary, "no edge fill");
                                    continue;
                                }

                                int chains = authored.arrows?.Count ?? 0;
                                if (chains < MinChains || chains > MaxChains)
                                {
                                    AddFailureSummary(failureSummary, $"chain count {chains}");
                                    continue;
                                }

                                OuterFillMetrics outer = CalculateOuterFillMetrics(authored);
                                if (outer.Band1Coverage < outerProfile.MinBand1Coverage || outer.MaxEmptyRun > outerProfile.MaxEmptyRun)
                                {
                                    AddFailureSummary(failureSummary, $"outer {outerProfile.Name}");
                                    continue;
                                }

                                if (!PassFinalPackQuality(authored, out string qualityDetails))
                                {
                                    AddFailureSummary(failureSummary, qualityDetails);
                                    continue;
                                }

                                int outerShort = CountOuterShortChains(authored);
                                if (outerShort > MaxOuterShortChains)
                                {
                                    AddFailureSummary(failureSummary, "outer short");
                                    continue;
                                }

                                float coverage = CountTiles(authored) / (float)Mathf.Max(1, authored.width * authored.height);
                                if (coverage < MinCoverage)
                                {
                                    AddFailureSummary(failureSummary, "coverage");
                                    continue;
                                }

                                string signature = MakeAuthoredSignature(authored);
                                if (!signatures.Add(signature))
                                    continue;

                                if (!ValidateAuthoredGreedy(authored, out string greedyDetails))
                                {
                                    AddFailureSummary(failureSummary, greedyDetails);
                                    continue;
                                }

                                int rank = levels.Count + 1;
                                string id = $"{idPrefix}_{rank:000}_{ShortName(source.levelId)}_{transform}_{reversePattern}_{fillMode}";
                                var def = CreateDerivedDefinition(source, id, authored);
                                string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{outputFolder}/{id}.asset");
                                AssetDatabase.CreateAsset(def, assetPath);
                                levels.Add(def);
                                sourceCounts[source.levelId] = sourceCounts.TryGetValue(source.levelId, out int currentCount) ? currentCount + 1 : 1;

                                VisualQualityMetrics visual = CalculateVisualQualityMetrics(authored);
                                string details = $"{qualityDetails}; {greedyDetails}";
                                report.Add($"{rank},{id},{assetPath},{source.levelId},{transform},{reversePattern},{reversed},{trimmedOuterExit},{trimmedOuterShort},{fillMode},{outerProfile.Name},{filled},{authored.width},{authored.height},{chains},{coverage:0.000},{outer.Band1Coverage:0.000},{outer.Band2Coverage:0.000},{outer.MaxEmptyRun},{visual.Penalty},{visual.EdgeLong},{visual.CornerLong},{visual.EdgeShort},{visual.MaxEdgeRun},True,{EscapeCsv(details)}");
                            }
                        }
                    }
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            var pack = SavePackAt(packPath, packId, displayName, levels);
            if (attachToDemo)
                AttachPackToDemo(pack, logTag);
            WriteLines(reportPath, report);
            if (failureSummary.Count > 0)
                Debug.Log($"[{logTag}] Failure summary: {FormatFailureSummary(failureSummary)}");
            Debug.Log($"[{logTag}] Portrait variant pack levels={levels.Count}, sourceAssets={sourcePaths.Count}, path={packPath}");
        }

        private static void BuildPlayabilitySelectedPackFromFolder(
            string sourceFolder,
            string packPath,
            string reportPath,
            string packId,
            string displayName,
            string logTag,
            int targetCount,
            bool attachToDemo,
            bool curatedMode = false)
        {
            EnsureFolder(Path.GetDirectoryName(packPath)?.Replace("\\", "/"));

            var sourcePaths = new List<string>();
            AddGeneratedCompositeSources(sourcePaths, sourceFolder);
            var candidates = new List<PlayabilitySelectionCandidate>(sourcePaths.Count);
            var rejected = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < sourcePaths.Count; i++)
            {
                string path = sourcePaths[i];
                var level = AssetDatabase.LoadAssetAtPath<LevelDefinition>(path);
                var authored = level?.authoredLevel;
                if (authored?.arrows == null)
                {
                    AddFailureSummary(rejected, "missing authored");
                    continue;
                }

                int chains = authored.arrows.Count;
                int area = Mathf.Max(1, authored.width * authored.height);
                float coverage = CountTiles(authored) / (float)area;
                var outer = CalculateOuterFillMetrics(authored);
                var visual = CalculateVisualQualityMetrics(authored);
                var playability = AnalyzePlayability(authored, outer, visual);
                if (!playability.Solved)
                {
                    AddFailureSummary(rejected, "playability unsolved");
                    continue;
                }

                if (curatedMode && !PassCuratedPlayabilityFilter(playability, visual, out string curatedReject))
                {
                    AddFailureSummary(rejected, curatedReject);
                    continue;
                }

                if (outer.Band1Coverage < BalancedPortraitOuterBand1Coverage || outer.MaxEmptyRun > BalancedPortraitMaxOuterEmptyRun)
                {
                    AddFailureSummary(rejected, "outer");
                    continue;
                }

                candidates.Add(new PlayabilitySelectionCandidate
                {
                    Level = level,
                    Path = path,
                    Source = level.levelId,
                    Families = ExtractSourceFamiliesFromName(level.levelId),
                    Width = authored.width,
                    Height = authored.height,
                    Chains = chains,
                    Coverage = coverage,
                    Outer = outer,
                    Visual = visual,
                    Playability = playability
                });
            }

            candidates.Sort((a, b) => b.Playability.Score.CompareTo(a.Playability.Score));

            var selected = new List<PlayabilitySelectionCandidate>(targetCount);
            var selectedPaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var familyCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var typeCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var typeQuotas = CreatePlayabilityTypeQuotas(targetCount);
            var typeCaps = curatedMode ? CreatePlayabilityTypeCaps(targetCount) : null;

            SelectPlayabilityCandidates(candidates, selected, selectedPaths, familyCounts, typeCounts, typeQuotas, typeCaps, targetCount, PlayabilityStrictMaxFamilyUse, enforceTypeQuota: true);
            SelectPlayabilityCandidates(candidates, selected, selectedPaths, familyCounts, typeCounts, typeQuotas, typeCaps, targetCount, PlayabilityStrictMaxFamilyUse, enforceTypeQuota: false);
            SelectPlayabilityCandidates(candidates, selected, selectedPaths, familyCounts, typeCounts, typeQuotas, typeCaps, targetCount, PlayabilityRelaxedMaxFamilyUse, enforceTypeQuota: false);
            if (curatedMode && selected.Count < targetCount)
                SelectPlayabilityCandidates(candidates, selected, selectedPaths, familyCounts, typeCounts, typeQuotas, null, targetCount, PlayabilityRelaxedMaxFamilyUse, enforceTypeQuota: false);
            SelectPlayabilityCandidates(candidates, selected, selectedPaths, familyCounts, typeCounts, typeQuotas, null, targetCount, int.MaxValue, enforceTypeQuota: false);

            var levels = new List<LevelDefinition>(selected.Count);
            var report = new List<string>
            {
                "rank,id,path,type,score,width,height,chains,coverage,outer1,outer2,maxOuterEmptyRun,openers,avgChoices,minChoices,maxChoices,lowChoiceSteps,highChoiceSteps,unlockBursts,maxUnlockBurst,keyLockSteps,firstOuter,lastOuter,endingShort,avgChain,maxChain,regionSwitches,dependencyBlocks,dependencyCycles,dependencyLayerStuck,visualPenalty,edgeShort,maxEdgeRun,families,details"
            };

            for (int i = 0; i < selected.Count; i++)
            {
                var c = selected[i];
                levels.Add(c.Level);
                var p = c.Playability;
                report.Add(string.Join(",",
                    (i + 1).ToString(),
                    EscapeCsv(c.Level.levelId),
                    EscapeCsv(c.Path),
                    EscapeCsv(p.TypeId),
                    p.Score.ToString("0.0"),
                    c.Width.ToString(),
                    c.Height.ToString(),
                    c.Chains.ToString(),
                    c.Coverage.ToString("0.000"),
                    c.Outer.Band1Coverage.ToString("0.000"),
                    c.Outer.Band2Coverage.ToString("0.000"),
                    c.Outer.MaxEmptyRun.ToString(),
                    p.Openers.ToString(),
                    p.AvgChoices.ToString("0.00"),
                    p.MinChoices.ToString(),
                    p.MaxChoices.ToString(),
                    p.LowChoiceSteps.ToString(),
                    p.HighChoiceSteps.ToString(),
                    p.UnlockBursts.ToString(),
                    p.MaxUnlockBurst.ToString(),
                    p.KeyLockSteps.ToString(),
                    p.FirstOuterRatio.ToString("0.000"),
                    p.LastOuterRatio.ToString("0.000"),
                    p.EndingShortRatio.ToString("0.000"),
                    p.AvgChainLength.ToString("0.00"),
                    p.MaxChainLength.ToString(),
                    p.RegionSwitches.ToString(),
                    p.DependencyBlocks.ToString(),
                    p.DependencyCycles.ToString(),
                    p.DependencyLayerStuck.ToString(),
                    c.Visual.Penalty.ToString(),
                    c.Visual.EdgeShort.ToString(),
                    c.Visual.MaxEdgeRun.ToString(),
                    EscapeCsv(FormatFamilies(c.Families)),
                    EscapeCsv(p.Details)));
            }

            var pack = SavePackAt(packPath, packId, displayName, levels);
            if (attachToDemo)
                AttachPackToDemo(pack, logTag);
            WriteLines(reportPath, report);

            Debug.Log($"[{logTag}] Selected levels={selected.Count}/{targetCount}, candidates={candidates.Count}/{sourcePaths.Count}, typeCounts={FormatTypeCounts(typeCounts)}, rejected={FormatFailureSummary(rejected)}, path={packPath}");
        }

        private static void BuildPlayabilityBalancedPackFromFolder(
            string sourceFolder,
            string packPath,
            string reportPath,
            string packId,
            string displayName,
            string logTag,
            int targetCount,
            bool attachToDemo)
        {
            EnsureFolder(Path.GetDirectoryName(packPath)?.Replace("\\", "/"));

            var sourcePaths = new List<string>();
            AddGeneratedCompositeSources(sourcePaths, sourceFolder);
            var candidates = new List<PlayabilitySelectionCandidate>(sourcePaths.Count);
            var rejected = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var availableTypeCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var holeRejectedTypeCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < sourcePaths.Count; i++)
            {
                string path = sourcePaths[i];
                var level = AssetDatabase.LoadAssetAtPath<LevelDefinition>(path);
                var authored = level?.authoredLevel;
                if (authored?.arrows == null)
                {
                    AddFailureSummary(rejected, "missing authored");
                    continue;
                }

                int chains = authored.arrows.Count;
                int area = Mathf.Max(1, authored.width * authored.height);
                float coverage = CountTiles(authored) / (float)area;
                var outer = CalculateOuterFillMetrics(authored);
                if (outer.Band1Coverage < BalancedPortraitOuterBand1Coverage || outer.MaxEmptyRun > BalancedPortraitMaxOuterEmptyRun)
                {
                    AddFailureSummary(rejected, "outer");
                    continue;
                }

                var visual = CalculateVisualQualityMetrics(authored);
                var playability = AnalyzePlayability(authored, outer, visual);
                if (!playability.Solved)
                {
                    AddFailureSummary(rejected, "playability unsolved");
                    continue;
                }

                bool targetPool =
                    IsBalancedSectionTarget(playability, authored, outer, visual) ||
                    IsBalancedDenseTarget(playability, authored, outer, visual) ||
                    IsBalancedShellTarget(playability, authored, outer, visual);
                if (!PassBalancedPlayabilityFilter(playability, visual, targetPool, out string balancedReject))
                {
                    AddFailureSummary(rejected, balancedReject);
                    continue;
                }

                var hole = CalculateHoleTrimCandidate(level, i + 1);
                if (IsLargeBalancedHole(hole, out string holeReject))
                {
                    AddFailureSummary(rejected, holeReject);
                    IncrementCount(holeRejectedTypeCounts, ResolveBalancedTargetType(playability, authored, outer, visual));
                    continue;
                }

                IncrementCount(availableTypeCounts, playability.TypeId);
                candidates.Add(new PlayabilitySelectionCandidate
                {
                    Level = level,
                    Path = path,
                    Source = level.levelId,
                    Families = ExtractSourceFamiliesFromName(level.levelId),
                    Width = authored.width,
                    Height = authored.height,
                    Chains = chains,
                    Coverage = coverage,
                    Outer = outer,
                    Visual = visual,
                    Hole = hole,
                    Playability = playability
                });
            }

            candidates.Sort((a, b) => b.Playability.Score.CompareTo(a.Playability.Score));

            var selected = new List<PlayabilitySelectionCandidate>(targetCount);
            var selectedPaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var familyCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var typeCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var typeQuotas = CreateBalancedPlayabilityTypeQuotas(targetCount);
            var typeCaps = CreateBalancedPlayabilityTypeCaps(targetCount);
            int targetPerType = Mathf.Min(20, Mathf.Max(1, targetCount / 6));

            SelectBalancedTargetCandidates(candidates, selected, selectedPaths, familyCounts, typeCounts, "section", targetPerType, PlayabilityStrictMaxFamilyUse);
            SelectBalancedTargetCandidates(candidates, selected, selectedPaths, familyCounts, typeCounts, "dense", targetPerType, PlayabilityStrictMaxFamilyUse);
            SelectBalancedTargetCandidates(candidates, selected, selectedPaths, familyCounts, typeCounts, "shell", targetPerType, PlayabilityStrictMaxFamilyUse);
            SelectBalancedTargetCandidates(candidates, selected, selectedPaths, familyCounts, typeCounts, "section", targetPerType, PlayabilityRelaxedMaxFamilyUse);
            SelectBalancedTargetCandidates(candidates, selected, selectedPaths, familyCounts, typeCounts, "dense", targetPerType, PlayabilityRelaxedMaxFamilyUse);
            SelectBalancedTargetCandidates(candidates, selected, selectedPaths, familyCounts, typeCounts, "shell", targetPerType, PlayabilityRelaxedMaxFamilyUse);

            SelectPlayabilityCandidates(candidates, selected, selectedPaths, familyCounts, typeCounts, typeQuotas, typeCaps, targetCount, PlayabilityRelaxedMaxFamilyUse, enforceTypeQuota: false);
            SelectPlayabilityCandidates(candidates, selected, selectedPaths, familyCounts, typeCounts, typeQuotas, typeCaps, targetCount, int.MaxValue, enforceTypeQuota: false);

            var levels = new List<LevelDefinition>(selected.Count);
            var report = new List<string>
            {
                "rank,id,path,type,score,width,height,chains,coverage,outer1,outer2,maxOuterEmptyRun,holeScore,localEmpty8,localEmpty6,centerEmpty,openers,avgChoices,minChoices,maxChoices,lowChoiceSteps,highChoiceSteps,unlockBursts,maxUnlockBurst,keyLockSteps,firstOuter,lastOuter,endingShort,avgChain,maxChain,regionSwitches,dependencyBlocks,dependencyCycles,dependencyLayerStuck,visualPenalty,edgeShort,maxEdgeRun,families,details"
            };

            for (int i = 0; i < selected.Count; i++)
            {
                var c = selected[i];
                levels.Add(c.Level);
                var p = c.Playability;
                report.Add(string.Join(",",
                    (i + 1).ToString(),
                    EscapeCsv(c.Level.levelId),
                    EscapeCsv(c.Path),
                    EscapeCsv(p.TypeId),
                    p.Score.ToString("0.0"),
                    c.Width.ToString(),
                    c.Height.ToString(),
                    c.Chains.ToString(),
                    c.Coverage.ToString("0.000"),
                    c.Outer.Band1Coverage.ToString("0.000"),
                    c.Outer.Band2Coverage.ToString("0.000"),
                    c.Outer.MaxEmptyRun.ToString(),
                    c.Hole != null ? c.Hole.HoleScore.ToString("0.0") : "0.0",
                    c.Hole != null ? c.Hole.LocalEmpty8.ToString("0.000") : "0.000",
                    c.Hole != null ? c.Hole.LocalEmpty6.ToString("0.000") : "0.000",
                    c.Hole != null ? c.Hole.CenterEmpty.ToString("0.000") : "0.000",
                    p.Openers.ToString(),
                    p.AvgChoices.ToString("0.00"),
                    p.MinChoices.ToString(),
                    p.MaxChoices.ToString(),
                    p.LowChoiceSteps.ToString(),
                    p.HighChoiceSteps.ToString(),
                    p.UnlockBursts.ToString(),
                    p.MaxUnlockBurst.ToString(),
                    p.KeyLockSteps.ToString(),
                    p.FirstOuterRatio.ToString("0.000"),
                    p.LastOuterRatio.ToString("0.000"),
                    p.EndingShortRatio.ToString("0.000"),
                    p.AvgChainLength.ToString("0.00"),
                    p.MaxChainLength.ToString(),
                    p.RegionSwitches.ToString(),
                    p.DependencyBlocks.ToString(),
                    p.DependencyCycles.ToString(),
                    p.DependencyLayerStuck.ToString(),
                    c.Visual.Penalty.ToString(),
                    c.Visual.EdgeShort.ToString(),
                    c.Visual.MaxEdgeRun.ToString(),
                    EscapeCsv(FormatFamilies(c.Families)),
                    EscapeCsv(p.Details)));
            }

            var pack = SavePackAt(packPath, packId, displayName, levels);
            if (attachToDemo)
                AttachPackToDemo(pack, logTag);
            WriteLines(reportPath, report);

            Debug.Log($"[{logTag}] Selected levels={selected.Count}/{targetCount}, candidates={candidates.Count}/{sourcePaths.Count}, availableTypeCounts={FormatTypeCounts(availableTypeCounts)}, selectedTypeCounts={FormatTypeCounts(typeCounts)}, holeRejectedTypes={FormatTypeCounts(holeRejectedTypeCounts)}, rejected={FormatFailureSummary(rejected)}, path={packPath}");
        }

        private static OuterStrongPackResult BuildOuterStrongRepairPack(
            string label,
            string inputPackPath,
            string outputFolder,
            string outputPackPath,
            string reportPath,
            string idPrefix,
            string displayName,
            List<OuterStrongPreviewCandidate> preview)
        {
            EnsureFolder(outputFolder);
            EnsureFolder(Path.GetDirectoryName(outputPackPath)?.Replace("\\", "/"));
            ClearFolder(outputFolder);

            var result = new OuterStrongPackResult
            {
                Label = label,
                InputPath = inputPackPath,
                OutputPath = outputPackPath
            };
            var report = new List<string>
            {
                "rank,status,pack,sourceId,sourcePath,outputId,outputPath,mode,width,height,chainsBefore,chainsAfter,tilesBefore,tilesAfter,coverageBefore,coverageAfter,outer1Before,outer1After,outer2Before,outer2After,maxOuterEmptyBefore,maxOuterEmptyAfter,edgeShortBefore,edgeShortAfter,edgeLongBefore,edgeLongAfter,maxEdgeRunBefore,maxEdgeRunAfter,trimmedOuterExit,trimmedOuterShort,filled,greedy,score,details"
            };

            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(inputPackPath);
            if (pack?.levels == null)
            {
                result.MissingCount++;
                report.Add($"0,missing_pack,{EscapeCsv(label)},{EscapeCsv(inputPackPath)},,,,,,,,,,,,,,,,,,,,,,,,,,,False,0,{EscapeCsv("missing input pack")}");
                WriteLines(reportPath, report);
                Debug.LogWarning($"[R1R2OuterStrongRepair] Missing {label} pack: {inputPackPath}");
                return result;
            }

            var outputLevels = new List<LevelDefinition>(pack.levels.Length);
            for (int i = 0; i < pack.levels.Length; i++)
            {
                var source = pack.levels[i];
                result.SourceCount++;
                if (source?.authoredLevel?.arrows == null)
                {
                    result.MissingCount++;
                    report.Add($"{i + 1},missing_level,{EscapeCsv(label)},{EscapeCsv(source != null ? source.levelId : "null")},{EscapeCsv(source != null ? AssetDatabase.GetAssetPath(source) : string.Empty)},,,,,,,,,,,,,,,,,,,,,,,,,,,False,0,{EscapeCsv("missing authored level")}");
                    continue;
                }

                string sourcePath = AssetDatabase.GetAssetPath(source);
                var beforeOuter = CalculateOuterFillMetrics(source.authoredLevel);
                var beforeVisual = CalculateVisualQualityMetrics(source.authoredLevel);
                int chainsBefore = source.authoredLevel.arrows.Count;
                int tilesBefore = CountTiles(source.authoredLevel);
                float coverageBefore = tilesBefore / (float)Mathf.Max(1, source.authoredLevel.width * source.authoredLevel.height);
                result.BeforeOuter1Sum += beforeOuter.Band1Coverage;
                result.BeforeOuter2Sum += beforeOuter.Band2Coverage;
                result.BeforeMaxOuterEmptyRun = Mathf.Max(result.BeforeMaxOuterEmptyRun, beforeOuter.MaxEmptyRun);
                result.BeforeEdgeShortSum += beforeVisual.EdgeShort;
                result.BeforeEdgeLongSum += beforeVisual.EdgeLong;

                var candidates = new List<OuterStrongCandidate>
                {
                    EvaluateOuterStrongCandidate(source.authoredLevel, "original", false, false, "none", false),
                    EvaluateOuterStrongCandidate(source.authoredLevel, "fill_outer", false, false, "edge160", true),
                    EvaluateOuterStrongCandidate(source.authoredLevel, "trim_refill", true, true, "edge160", true)
                };

                OuterStrongCandidate best = SelectBestOuterStrongCandidate(candidates);
                if (best == null)
                {
                    result.GreedyFailCount++;
                    string failureDetails = FormatOuterStrongCandidateDetails(candidates);
                    report.Add($"{i + 1},greedy_fail,{EscapeCsv(label)},{EscapeCsv(source.levelId)},{EscapeCsv(sourcePath)},,,,,{source.authoredLevel.width},{source.authoredLevel.height},{chainsBefore},,{tilesBefore},,{coverageBefore:0.000},,{beforeOuter.Band1Coverage:0.000},,{beforeOuter.Band2Coverage:0.000},,{beforeOuter.MaxEmptyRun},,{beforeVisual.EdgeShort},,{beforeVisual.EdgeLong},,{beforeVisual.MaxEdgeRun},,,,,False,0,{EscapeCsv(failureDetails)}");
                    continue;
                }

                int rank = outputLevels.Count + 1;
                string outputId = $"{idPrefix}_{rank:000}_{ShortName(source.levelId)}";
                var def = CreateDerivedDefinition(source, outputId, best.Authored);
                string outputPath = AssetDatabase.GenerateUniqueAssetPath($"{outputFolder}/{outputId}.asset");
                AssetDatabase.CreateAsset(def, outputPath);
                outputLevels.Add(def);
                result.OutputCount++;

                bool changed = best.Mode != "original" && !string.Equals(MakeAuthoredSignature(source.authoredLevel), MakeAuthoredSignature(best.Authored), StringComparison.Ordinal);
                bool improved = IsOuterStrongImproved(beforeOuter, best.Outer, beforeVisual, best.Visual);
                if (changed)
                    result.ChangedCount++;
                if (improved)
                    result.ImprovedCount++;

                result.AfterOuter1Sum += best.Outer.Band1Coverage;
                result.AfterOuter2Sum += best.Outer.Band2Coverage;
                result.AfterMaxOuterEmptyRun = Mathf.Max(result.AfterMaxOuterEmptyRun, best.Outer.MaxEmptyRun);
                result.AfterEdgeShortSum += best.Visual.EdgeShort;
                result.AfterEdgeLongSum += best.Visual.EdgeLong;

                string status = changed ? "repaired" : "kept";
                string candidateDetails = FormatOuterStrongCandidateDetails(candidates);
                report.Add(string.Join(",",
                    (i + 1).ToString(),
                    status,
                    EscapeCsv(label),
                    EscapeCsv(source.levelId),
                    EscapeCsv(sourcePath),
                    EscapeCsv(outputId),
                    EscapeCsv(outputPath),
                    EscapeCsv(best.Mode),
                    best.Authored.width.ToString(),
                    best.Authored.height.ToString(),
                    chainsBefore.ToString(),
                    best.Chains.ToString(),
                    tilesBefore.ToString(),
                    best.Tiles.ToString(),
                    coverageBefore.ToString("0.000"),
                    best.Coverage.ToString("0.000"),
                    beforeOuter.Band1Coverage.ToString("0.000"),
                    best.Outer.Band1Coverage.ToString("0.000"),
                    beforeOuter.Band2Coverage.ToString("0.000"),
                    best.Outer.Band2Coverage.ToString("0.000"),
                    beforeOuter.MaxEmptyRun.ToString(),
                    best.Outer.MaxEmptyRun.ToString(),
                    beforeVisual.EdgeShort.ToString(),
                    best.Visual.EdgeShort.ToString(),
                    beforeVisual.EdgeLong.ToString(),
                    best.Visual.EdgeLong.ToString(),
                    beforeVisual.MaxEdgeRun.ToString(),
                    best.Visual.MaxEdgeRun.ToString(),
                    best.TrimmedOuterExit.ToString(),
                    best.TrimmedOuterShort.ToString(),
                    best.Filled.ToString(),
                    best.Greedy.ToString(),
                    best.Score.ToString("0.0"),
                    EscapeCsv(candidateDetails)));

                preview?.Add(new OuterStrongPreviewCandidate
                {
                    Level = def,
                    PackLabel = label,
                    SourceId = source.levelId,
                    Path = outputPath,
                    BeforeOuter = beforeOuter,
                    AfterOuter = best.Outer,
                    BeforeVisual = beforeVisual,
                    AfterVisual = best.Visual,
                    Priority = ScoreOuterStrongPreviewPriority(beforeOuter, best.Outer, beforeVisual, best.Visual)
                });
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            SavePackAt(outputPackPath, $"{idPrefix}_pack", displayName, outputLevels);
            WriteLines(reportPath, report);
            Debug.Log($"[R1R2OuterStrongRepair] {label} output={result.OutputCount}/{result.SourceCount}, changed={result.ChangedCount}, improved={result.ImprovedCount}, report={reportPath}");
            return result;
        }

        private static OuterStrongCandidate EvaluateOuterStrongCandidate(
            AuthoredLevelData source,
            string mode,
            bool trimOuter,
            bool trimShort,
            string fillMode,
            bool extraFillPass)
        {
            var authored = CloneAuthored(source);
            var candidate = new OuterStrongCandidate
            {
                Authored = authored,
                Mode = mode
            };

            if (trimOuter)
                candidate.TrimmedOuterExit = TrimOuterStraightExitChains(authored, maxRemove: 10, minLength: 7);
            if (trimShort)
            {
                candidate.TrimmedOuterShort = TrimDenseOuterShortChains(
                    authored,
                    MaxPortraitOuterShortClusterTrim,
                    PortraitOuterShortClusterRadius,
                    maxNearbyShortChains: 3);
            }

            if (!string.IsNullOrWhiteSpace(fillMode) && fillMode != "none")
            {
                candidate.Filled += ApplyEdgeFillMode(authored, fillMode);
                var outerAfterFirstPass = CalculateOuterFillMetrics(authored);
                if (extraFillPass &&
                    (outerAfterFirstPass.Band1Coverage < StrictPortraitOuterBand1Coverage ||
                     outerAfterFirstPass.MaxEmptyRun > StrictPortraitMaxOuterEmptyRun))
                {
                    candidate.Filled += ApplyEdgeFillMode(authored, "edge80");
                }
            }

            candidate.Chains = authored.arrows?.Count ?? 0;
            candidate.Tiles = CountTiles(authored);
            candidate.Coverage = candidate.Tiles / (float)Mathf.Max(1, authored.width * authored.height);
            candidate.Outer = CalculateOuterFillMetrics(authored);
            candidate.Visual = CalculateVisualQualityMetrics(authored);
            candidate.Greedy = ValidateAuthoredGreedy(authored, out candidate.GreedyDetails);
            candidate.Score = ScoreOuterStrongCandidate(candidate);
            return candidate;
        }

        private static OuterStrongCandidate SelectBestOuterStrongCandidate(List<OuterStrongCandidate> candidates)
        {
            OuterStrongCandidate best = null;
            for (int i = 0; i < candidates.Count; i++)
            {
                var candidate = candidates[i];
                if (candidate == null || !candidate.Greedy)
                    continue;

                if (best == null || candidate.Score > best.Score)
                    best = candidate;
            }

            return best;
        }

        private static float ScoreOuterStrongCandidate(OuterStrongCandidate candidate)
        {
            if (candidate == null || !candidate.Greedy)
                return -1000000f;

            float score =
                candidate.Outer.Band1Coverage * 10000f +
                candidate.Outer.Band2Coverage * 800f -
                candidate.Outer.MaxEmptyRun * 120f +
                Mathf.Min(1f, candidate.Coverage) * 200f -
                candidate.Visual.Penalty * 9f -
                candidate.Visual.EdgeShort * 7f -
                candidate.Visual.EdgeLong * 15f -
                Mathf.Max(0, candidate.Visual.MaxEdgeRun - 14) * 6f;
            if (candidate.Mode == "original")
                score -= 25f;
            return score;
        }

        private static bool IsOuterStrongImproved(
            OuterFillMetrics beforeOuter,
            OuterFillMetrics afterOuter,
            VisualQualityMetrics beforeVisual,
            VisualQualityMetrics afterVisual)
        {
            return afterOuter.Band1Coverage > beforeOuter.Band1Coverage + 0.001f ||
                   afterOuter.Band2Coverage > beforeOuter.Band2Coverage + 0.001f ||
                   afterOuter.MaxEmptyRun < beforeOuter.MaxEmptyRun ||
                   afterVisual.EdgeShort < beforeVisual.EdgeShort ||
                   afterVisual.EdgeLong < beforeVisual.EdgeLong ||
                   afterVisual.MaxEdgeRun < beforeVisual.MaxEdgeRun;
        }

        private static float ScoreOuterStrongPreviewPriority(
            OuterFillMetrics beforeOuter,
            OuterFillMetrics afterOuter,
            VisualQualityMetrics beforeVisual,
            VisualQualityMetrics afterVisual)
        {
            float originalNeed =
                (1f - beforeOuter.Band1Coverage) * 1000f +
                beforeOuter.MaxEmptyRun * 55f +
                beforeVisual.EdgeShort * 7f +
                beforeVisual.EdgeLong * 14f +
                Mathf.Max(0, beforeVisual.MaxEdgeRun - 14) * 5f;
            float gain =
                Mathf.Max(0f, afterOuter.Band1Coverage - beforeOuter.Band1Coverage) * 1200f +
                Mathf.Max(0, beforeOuter.MaxEmptyRun - afterOuter.MaxEmptyRun) * 90f +
                Mathf.Max(0, beforeVisual.Penalty - afterVisual.Penalty) * 8f;
            return originalNeed + gain;
        }

        private static string FormatOuterStrongCandidateDetails(List<OuterStrongCandidate> candidates)
        {
            var parts = new List<string>(candidates.Count);
            for (int i = 0; i < candidates.Count; i++)
            {
                var c = candidates[i];
                if (c == null)
                    continue;

                parts.Add($"{c.Mode}:greedy={c.Greedy}:outer1={c.Outer.Band1Coverage:0.000}:outer2={c.Outer.Band2Coverage:0.000}:empty={c.Outer.MaxEmptyRun}:edgeShort={c.Visual.EdgeShort}:edgeLong={c.Visual.EdgeLong}:filled={c.Filled}:trim={c.TrimmedOuterExit}/{c.TrimmedOuterShort}:{c.GreedyDetails}");
            }

            return string.Join(" | ", parts);
        }

        private static void AppendOuterStrongSummary(List<string> lines, OuterStrongPackResult result)
        {
            if (lines == null || result == null)
                return;

            int beforeCount = Mathf.Max(1, result.SourceCount - result.MissingCount);
            int afterCount = Mathf.Max(1, result.OutputCount);
            lines.Add(string.Join(",",
                EscapeCsv(result.Label),
                EscapeCsv(result.InputPath),
                EscapeCsv(result.OutputPath),
                result.SourceCount.ToString(),
                result.OutputCount.ToString(),
                result.ChangedCount.ToString(),
                result.ImprovedCount.ToString(),
                result.GreedyFailCount.ToString(),
                result.MissingCount.ToString(),
                (result.BeforeOuter1Sum / beforeCount).ToString("0.000"),
                (result.AfterOuter1Sum / afterCount).ToString("0.000"),
                (result.BeforeOuter2Sum / beforeCount).ToString("0.000"),
                (result.AfterOuter2Sum / afterCount).ToString("0.000"),
                result.BeforeMaxOuterEmptyRun.ToString(),
                result.AfterMaxOuterEmptyRun.ToString(),
                result.BeforeEdgeShortSum.ToString(),
                result.AfterEdgeShortSum.ToString(),
                result.BeforeEdgeLongSum.ToString(),
                result.AfterEdgeLongSum.ToString()));
        }

        private static void SelectPlayabilityCandidates(
            List<PlayabilitySelectionCandidate> candidates,
            List<PlayabilitySelectionCandidate> selected,
            HashSet<string> selectedPaths,
            Dictionary<string, int> familyCounts,
            Dictionary<string, int> typeCounts,
            Dictionary<string, int> typeQuotas,
            Dictionary<string, int> typeCaps,
            int targetCount,
            int maxFamilyUse,
            bool enforceTypeQuota)
        {
            for (int i = 0; i < candidates.Count && selected.Count < targetCount; i++)
            {
                var candidate = candidates[i];
                if (candidate == null || string.IsNullOrWhiteSpace(candidate.Path) || selectedPaths.Contains(candidate.Path))
                    continue;

                string type = string.IsNullOrWhiteSpace(candidate.Playability.TypeId) ? "sweep" : candidate.Playability.TypeId;
                if (typeCaps != null &&
                    typeCaps.TryGetValue(type, out int cap) &&
                    typeCounts.TryGetValue(type, out int currentTypeUse) &&
                    currentTypeUse >= cap)
                {
                    continue;
                }

                if (enforceTypeQuota &&
                    typeQuotas.TryGetValue(type, out int quota) &&
                    typeCounts.TryGetValue(type, out int currentTypeCount) &&
                    currentTypeCount >= quota)
                {
                    continue;
                }

                if (maxFamilyUse < int.MaxValue && !SourceFamiliesBelowLimit(candidate.Families, familyCounts, maxFamilyUse))
                    continue;

                selected.Add(candidate);
                selectedPaths.Add(candidate.Path);
                RecordSourceFamilies(candidate.Families, familyCounts);
                IncrementCount(typeCounts, type);
            }
        }

        private static void SelectBalancedTargetCandidates(
            List<PlayabilitySelectionCandidate> candidates,
            List<PlayabilitySelectionCandidate> selected,
            HashSet<string> selectedPaths,
            Dictionary<string, int> familyCounts,
            Dictionary<string, int> typeCounts,
            string targetType,
            int targetTypeCount,
            int maxFamilyUse)
        {
            if (string.IsNullOrWhiteSpace(targetType) || targetTypeCount <= 0)
                return;

            typeCounts.TryGetValue(targetType, out int current);
            for (int i = 0; i < candidates.Count && current < targetTypeCount; i++)
            {
                var candidate = candidates[i];
                if (candidate == null || string.IsNullOrWhiteSpace(candidate.Path) || selectedPaths.Contains(candidate.Path))
                    continue;
                if (!IsBalancedTarget(candidate, targetType))
                    continue;
                if (maxFamilyUse < int.MaxValue && !SourceFamiliesBelowLimit(candidate.Families, familyCounts, maxFamilyUse))
                    continue;

                UpdatePlayabilityType(candidate.Playability, targetType);
                selected.Add(candidate);
                selectedPaths.Add(candidate.Path);
                RecordSourceFamilies(candidate.Families, familyCounts);
                IncrementCount(typeCounts, targetType);
                current++;
            }
        }

        private static bool IsBalancedTarget(PlayabilitySelectionCandidate candidate, string targetType)
        {
            if (candidate?.Playability == null || candidate.Level?.authoredLevel == null)
                return false;

            var authored = candidate.Level.authoredLevel;
            if (string.Equals(targetType, "section", StringComparison.OrdinalIgnoreCase))
                return IsBalancedSectionTarget(candidate.Playability, authored, candidate.Outer, candidate.Visual);
            if (string.Equals(targetType, "dense", StringComparison.OrdinalIgnoreCase))
                return IsBalancedDenseTarget(candidate.Playability, authored, candidate.Outer, candidate.Visual);
            if (string.Equals(targetType, "shell", StringComparison.OrdinalIgnoreCase))
                return IsBalancedShellTarget(candidate.Playability, authored, candidate.Outer, candidate.Visual);
            return false;
        }

        private static Dictionary<string, int> CreatePlayabilityTypeQuotas(int targetCount)
        {
            var quotas = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                ["sweep"] = Mathf.RoundToInt(targetCount * 0.20f),
                ["lock"] = Mathf.RoundToInt(targetCount * 0.20f),
                ["section"] = Mathf.RoundToInt(targetCount * 0.20f),
                ["shell"] = Mathf.RoundToInt(targetCount * 0.15f),
                ["maze"] = Mathf.RoundToInt(targetCount * 0.15f),
                ["dense"] = Mathf.RoundToInt(targetCount * 0.10f)
            };
            return quotas;
        }

        private static Dictionary<string, int> CreatePlayabilityTypeCaps(int targetCount)
        {
            return new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                ["sweep"] = Mathf.RoundToInt(targetCount * 0.30f),
                ["lock"] = Mathf.RoundToInt(targetCount * 0.24f),
                ["maze"] = Mathf.RoundToInt(targetCount * 0.20f),
                ["section"] = Mathf.RoundToInt(targetCount * 0.22f),
                ["dense"] = Mathf.RoundToInt(targetCount * 0.12f),
                ["shell"] = Mathf.RoundToInt(targetCount * 0.08f)
            };
        }

        private static Dictionary<string, int> CreateBalancedPlayabilityTypeQuotas(int targetCount)
        {
            int targetPerType = Mathf.Min(20, Mathf.Max(1, targetCount / 6));
            return new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                ["section"] = targetPerType,
                ["dense"] = targetPerType,
                ["shell"] = targetPerType,
                ["lock"] = targetPerType,
                ["maze"] = targetPerType,
                ["sweep"] = targetPerType
            };
        }

        private static Dictionary<string, int> CreateBalancedPlayabilityTypeCaps(int targetCount)
        {
            int cap = Mathf.Max(24, Mathf.RoundToInt(targetCount * 0.22f));
            return new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                ["section"] = cap,
                ["dense"] = cap,
                ["shell"] = cap,
                ["lock"] = cap,
                ["maze"] = cap,
                ["sweep"] = cap
            };
        }

        private static bool PassCuratedPlayabilityFilter(PlayabilityMetrics playability, VisualQualityMetrics visual, out string reason)
        {
            reason = string.Empty;
            if (playability.Score < 69.0f)
            {
                reason = "score";
                return false;
            }

            if (playability.Openers > 29)
            {
                reason = "too many openers";
                return false;
            }

            if (playability.AvgChoices > 11.5f)
            {
                reason = "too many choices";
                return false;
            }

            if (playability.MaxChoices > 31)
            {
                reason = "choice spike";
                return false;
            }

            if (playability.UnlockBursts < 22)
            {
                reason = "weak unlock";
                return false;
            }

            if (playability.EndingShortRatio > 0.46f)
            {
                reason = "ending short";
                return false;
            }

            if (visual.EdgeShort > 22)
            {
                reason = "edge short";
                return false;
            }

            return true;
        }

        private static bool PassBalancedPlayabilityFilter(PlayabilityMetrics playability, VisualQualityMetrics visual, bool targetPool, out string reason)
        {
            reason = string.Empty;
            if (playability.Score < (targetPool ? 66.5f : 68.0f))
            {
                reason = "score";
                return false;
            }

            if (playability.Openers > 34)
            {
                reason = "too many openers";
                return false;
            }

            if (playability.AvgChoices > 12.5f)
            {
                reason = "too many choices";
                return false;
            }

            if (playability.MaxChoices > 36)
            {
                reason = "choice spike";
                return false;
            }

            if (playability.UnlockBursts < (targetPool ? 14 : 18))
            {
                reason = "weak unlock";
                return false;
            }

            if (playability.EndingShortRatio > 0.54f)
            {
                reason = "ending short";
                return false;
            }

            if (visual.EdgeShort > 28)
            {
                reason = "edge short";
                return false;
            }

            return true;
        }

        private static bool IsLargeBalancedHole(HoleTrimCandidate hole, out string reason)
        {
            reason = string.Empty;
            if (hole == null)
                return false;

            if (hole.CenterEmpty >= 0.13f)
            {
                reason = "large center hole";
                return true;
            }

            if (hole.HoleScore >= 60.0f)
            {
                reason = "large hole score";
                return true;
            }

            if (hole.LocalEmpty8 >= 0.36f && hole.LocalEmpty6 >= 0.48f)
            {
                reason = "large local hole";
                return true;
            }

            if (hole.LocalEmpty6 >= 0.56f)
            {
                reason = "large local6 hole";
                return true;
            }

            return false;
        }

        private static string ResolveBalancedTargetType(PlayabilityMetrics metrics, AuthoredLevelData authored, OuterFillMetrics outer, VisualQualityMetrics visual)
        {
            if (metrics == null)
                return "unknown";

            if (IsBalancedSectionTarget(metrics, authored, outer, visual))
                return "section";
            if (IsBalancedDenseTarget(metrics, authored, outer, visual))
                return "dense";
            if (IsBalancedShellTarget(metrics, authored, outer, visual))
                return "shell";
            return string.IsNullOrWhiteSpace(metrics.TypeId) ? "sweep" : metrics.TypeId;
        }

        private static bool IsBalancedDenseTarget(PlayabilityMetrics metrics, AuthoredLevelData authored, OuterFillMetrics outer, VisualQualityMetrics visual)
        {
            float coverage = CountTiles(authored) / (float)Mathf.Max(1, authored.width * authored.height);
            int chains = authored?.arrows?.Count ?? 0;
            return
                (coverage >= 0.935f && metrics.DependencyBlocks >= 105 && metrics.AvgChoices <= 9.2f && metrics.Openers <= 25) ||
                (coverage >= 0.955f && metrics.DependencyBlocks >= 90 && metrics.AvgChoices <= 8.7f && visual.EdgeShort <= 24) ||
                (coverage >= 0.915f && chains >= 138 && metrics.DependencyBlocks >= 100 && metrics.AvgChoices <= 8.8f);
        }

        private static bool IsBalancedSectionTarget(PlayabilityMetrics metrics, AuthoredLevelData authored, OuterFillMetrics outer, VisualQualityMetrics visual)
        {
            return
                string.Equals(metrics.TypeId, "section", StringComparison.OrdinalIgnoreCase) ||
                (metrics.FirstRegion != metrics.LastRegion && metrics.FirstRegionShare >= 0.38f && metrics.LastRegionShare >= 0.38f) ||
                metrics.RegionSwitches <= Mathf.Max(10, metrics.Steps * 0.48f);
        }

        private static bool IsBalancedShellTarget(PlayabilityMetrics metrics, AuthoredLevelData authored, OuterFillMetrics outer, VisualQualityMetrics visual)
        {
            float outerDrop = metrics.FirstOuterRatio - metrics.LastOuterRatio;
            return
                (metrics.FirstOuterRatio >= 0.72f && metrics.LastOuterRatio <= 0.26f && outerDrop >= 0.42f) ||
                (metrics.FirstOuterRatio >= 0.64f && metrics.LastOuterRatio <= 0.18f && outerDrop >= 0.40f && metrics.UnlockBursts >= 18);
        }

        private static PlayabilityMetrics AnalyzePlayability(AuthoredLevelData authored, OuterFillMetrics outer, VisualQualityMetrics visual)
        {
            var metrics = new PlayabilityMetrics
            {
                Solved = false,
                MinChoices = int.MaxValue,
                TypeId = "unknown",
                Details = string.Empty
            };

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out var startBoard, out string buildError))
            {
                metrics.Details = $"build failed {buildError}";
                return metrics;
            }

            var chainStats = BuildPlayabilityChainStats(authored, out int[] tileToChain);
            var dependency = BuildDependencySnapshot(authored);
            metrics.DependencyBlocks = dependency?.TotalBlocks ?? 0;
            metrics.DependencyCycles = dependency?.CycleCount ?? 0;
            metrics.DependencyLayerStuck = dependency?.LayerStuckCount ?? 0;

            int totalChains = Mathf.Max(1, authored.arrows?.Count ?? 0);
            int totalTiles = CountTiles(authored);
            metrics.AvgChainLength = totalTiles / (float)totalChains;
            metrics.MaxChainLength = CalculateMaxChainLength(chainStats);

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            var board = CloneBoard(startBoard);
            var moves = new List<MoveScore>(256);
            var clearedLengths = new List<int>(totalChains);
            var clearedOuter = new List<bool>(totalChains);
            var clearedRegions = new List<int>(totalChains);
            int choiceSum = 0;
            int previousRegion = -1;

            int maxSteps = Mathf.Max(FastGreedyBudgetMin, totalChains * FastGreedyBudgetPerChain);
            for (int step = 0; step < maxSteps; step++)
            {
                if (rules.IsSolved(board))
                {
                    metrics.Solved = true;
                    break;
                }

                moves.Clear();
                int choices = CollectClearableMoves(board, rules, tileToChain, moves);
                if (step == 0)
                    metrics.Openers = choices;
                if (choices <= 0 || moves.Count == 0)
                {
                    metrics.Details = $"stuck step={step}";
                    return metrics;
                }

                metrics.MinChoices = Mathf.Min(metrics.MinChoices, choices);
                metrics.MaxChoices = Mathf.Max(metrics.MaxChoices, choices);
                if (choices <= 1)
                    metrics.LowChoiceSteps++;
                if (choices >= 9)
                    metrics.HighChoiceSteps++;
                choiceSum += choices;

                MoveScore best = SelectBestPlayabilityMove(moves);
                if (!rules.TryApplyMove(board, best.Move, out var applied))
                {
                    metrics.Details = $"apply failed step={step}";
                    return metrics;
                }

                int chainIndex = best.ChainIndex;
                if (chainIndex < 0)
                    chainIndex = ResolveClearedChain(applied, tileToChain);

                int length = chainIndex >= 0 && chainIndex < chainStats.Length
                    ? chainStats[chainIndex].Length
                    : CountClearedTiles(applied);
                bool touchesOuter = chainIndex >= 0 && chainIndex < chainStats.Length && chainStats[chainIndex].TouchesOuter;
                int region = chainIndex >= 0 && chainIndex < chainStats.Length ? chainStats[chainIndex].Region : 0;

                clearedLengths.Add(length);
                clearedOuter.Add(touchesOuter);
                clearedRegions.Add(region);
                if (previousRegion >= 0 && previousRegion != region)
                    metrics.RegionSwitches++;
                previousRegion = region;

                int afterChoices = rules.IsSolved(board) ? 0 : CollectClearableMoves(board, rules, tileToChain, null);
                int unlock = afterChoices - Mathf.Max(0, choices - 1);
                if (unlock >= 2)
                    metrics.UnlockBursts++;
                if (unlock >= 3)
                    metrics.KeyLockSteps++;
                metrics.MaxUnlockBurst = Mathf.Max(metrics.MaxUnlockBurst, unlock);
                metrics.Steps++;
            }

            if (!metrics.Solved && rules.IsSolved(board))
                metrics.Solved = true;
            if (!metrics.Solved)
            {
                metrics.Details = $"move budget hit steps={metrics.Steps}";
                return metrics;
            }

            if (metrics.MinChoices == int.MaxValue)
                metrics.MinChoices = 0;
            metrics.AvgChoices = metrics.Steps > 0 ? choiceSum / (float)metrics.Steps : 0f;
            CalculatePhaseMetrics(clearedOuter, clearedRegions, clearedLengths, metrics);
            metrics.TypeId = ClassifyPlayability(metrics, authored, outer, visual);
            metrics.Score = ScorePlayability(metrics, outer, visual, authored);
            metrics.Details = $"type={metrics.TypeId}; score={metrics.Score:0.0}; deps={metrics.DependencyBlocks}/{metrics.DependencyCycles}/{metrics.DependencyLayerStuck}";
            return metrics;
        }

        private static BoardState CloneBoard(BoardState src)
        {
            var dst = new BoardState(src.width, src.height);
            Array.Copy(src.tiles, dst.tiles, src.tiles.Length);
            return dst;
        }

        private static PlayabilityChainStats[] BuildPlayabilityChainStats(AuthoredLevelData authored, out int[] tileToChain)
        {
            int area = Mathf.Max(1, authored.width * authored.height);
            tileToChain = new int[area];
            for (int i = 0; i < tileToChain.Length; i++)
                tileToChain[i] = -1;

            int count = authored.arrows?.Count ?? 0;
            var stats = new PlayabilityChainStats[count];
            for (int i = 0; i < count; i++)
            {
                var indices = authored.arrows[i]?.indices;
                var stat = new PlayabilityChainStats();
                stats[i] = stat;
                if (indices == null || indices.Count == 0)
                    continue;

                stat.Length = indices.Count;
                float sumX = 0f;
                float sumY = 0f;
                bool outer = false;
                for (int j = 0; j < indices.Count; j++)
                {
                    int index = indices[j];
                    if ((uint)index < (uint)tileToChain.Length)
                        tileToChain[index] = i;

                    int x = index % authored.width;
                    int y = index / authored.width;
                    sumX += x;
                    sumY += y;
                    outer |= IsOuterBandCell(authored.width, authored.height, x, y, 2);
                }

                stat.CenterX = sumX / indices.Count;
                stat.CenterY = sumY / indices.Count;
                stat.TouchesOuter = outer;
                stat.Region = ClassifyRegion(authored.width, authored.height, stat.CenterX, stat.CenterY);
            }

            return stats;
        }

        private static int CollectClearableMoves(BoardState board, IRuleset rules, int[] tileToChain, List<MoveScore> scoredMoves)
        {
            var uniqueChains = new HashSet<int>();
            int fallback = -1;
            foreach (var move in rules.GetLegalMoves(board))
            {
                if (!rules.TryApplyMove(board, move, out var delta))
                    continue;

                int cleared = CountClearedTiles(delta);
                int chain = ResolveClearedChain(delta, tileToChain);
                delta.Undo(board);

                if (cleared <= 0)
                    continue;

                int key = chain >= 0 ? chain : fallback--;
                uniqueChains.Add(key);
                if (scoredMoves != null)
                {
                    scoredMoves.Add(new MoveScore
                    {
                        Move = move,
                        ChainIndex = chain,
                        ClearedTiles = cleared
                    });
                }
            }

            return uniqueChains.Count;
        }

        private static MoveScore SelectBestPlayabilityMove(List<MoveScore> moves)
        {
            MoveScore best = moves[0];
            for (int i = 1; i < moves.Count; i++)
            {
                var candidate = moves[i];
                if (candidate.ClearedTiles > best.ClearedTiles)
                    best = candidate;
            }

            return best;
        }

        private static int CountClearedTiles(MoveDelta delta)
        {
            int cleared = 0;
            var changes = delta?.changes;
            if (changes == null)
                return 0;

            for (int i = 0; i < changes.Count; i++)
            {
                var change = changes[i];
                if (change.before.type == TileType.Arrow && change.after.type == TileType.Empty)
                    cleared++;
            }

            return cleared;
        }

        private static int ResolveClearedChain(MoveDelta delta, int[] tileToChain)
        {
            var changes = delta?.changes;
            if (changes == null || tileToChain == null)
                return -1;

            for (int i = 0; i < changes.Count; i++)
            {
                int index = changes[i].index;
                if ((uint)index < (uint)tileToChain.Length && tileToChain[index] >= 0)
                    return tileToChain[index];
            }

            return -1;
        }

        private static void CalculatePhaseMetrics(List<bool> clearedOuter, List<int> clearedRegions, List<int> clearedLengths, PlayabilityMetrics metrics)
        {
            int count = clearedOuter?.Count ?? 0;
            if (count == 0)
                return;

            int window = Mathf.Max(1, Mathf.CeilToInt(count * 0.25f));
            int firstOuter = 0;
            int lastOuter = 0;
            for (int i = 0; i < window && i < count; i++)
            {
                if (clearedOuter[i])
                    firstOuter++;
                if (clearedOuter[count - 1 - i])
                    lastOuter++;
            }

            metrics.FirstOuterRatio = firstOuter / (float)window;
            metrics.LastOuterRatio = lastOuter / (float)window;

            int endingWindow = Mathf.Max(1, Mathf.CeilToInt(count * 0.20f));
            int shortEnding = 0;
            for (int i = Mathf.Max(0, count - endingWindow); i < count; i++)
            {
                if (clearedLengths[i] <= 3)
                    shortEnding++;
            }
            metrics.EndingShortRatio = shortEnding / (float)endingWindow;

            CalculateDominantRegions(clearedRegions, 0, window, out metrics.FirstRegion, out metrics.FirstRegionShare);
            CalculateDominantRegions(clearedRegions, Mathf.Max(0, count - window), window, out metrics.LastRegion, out metrics.LastRegionShare);
        }

        private static void CalculateDominantRegions(List<int> regions, int start, int length, out int region, out float share)
        {
            region = 0;
            share = 0f;
            if (regions == null || regions.Count == 0 || length <= 0)
                return;

            int[] counts = new int[5];
            int end = Mathf.Min(regions.Count, start + length);
            int total = 0;
            for (int i = start; i < end; i++)
            {
                int r = Mathf.Clamp(regions[i], 0, counts.Length - 1);
                counts[r]++;
                total++;
            }

            int best = 0;
            for (int i = 1; i < counts.Length; i++)
            {
                if (counts[i] > counts[best])
                    best = i;
            }

            region = best;
            share = total > 0 ? counts[best] / (float)total : 0f;
        }

        private static int ClassifyRegion(int width, int height, float centerX, float centerY)
        {
            float nx = width <= 1 ? 0.5f : centerX / (width - 1);
            float ny = height <= 1 ? 0.5f : centerY / (height - 1);
            if (nx < 0.33f)
                return 1;
            if (nx > 0.67f)
                return 2;
            if (ny < 0.33f)
                return 3;
            if (ny > 0.67f)
                return 4;
            return 0;
        }

        private static int CalculateMaxChainLength(PlayabilityChainStats[] stats)
        {
            int max = 0;
            if (stats == null)
                return max;

            for (int i = 0; i < stats.Length; i++)
                max = Mathf.Max(max, stats[i]?.Length ?? 0);
            return max;
        }

        private static string ClassifyPlayability(PlayabilityMetrics metrics, AuthoredLevelData authored, OuterFillMetrics outer, VisualQualityMetrics visual)
        {
            float coverage = CountTiles(authored) / (float)Mathf.Max(1, authored.width * authored.height);
            if (metrics.Openers >= 24 || metrics.AvgChoices >= 9.5f)
                return "sweep";
            if (metrics.FirstRegion != metrics.LastRegion &&
                metrics.FirstRegionShare >= 0.45f &&
                metrics.LastRegionShare >= 0.45f)
                return "section";
            if (metrics.RegionSwitches <= Mathf.Max(8, metrics.Steps * 0.38f))
                return "section";
            if (metrics.MaxChainLength >= 58 || metrics.AvgChainLength >= 9.8f)
                return "maze";
            if (coverage >= 0.955f && metrics.DependencyBlocks >= 120 && metrics.AvgChoices <= 8.5f)
                return "dense";
            if (metrics.MaxUnlockBurst >= 5 || metrics.KeyLockSteps >= 4 || metrics.DependencyCycles >= 2)
                return "lock";
            if (metrics.FirstOuterRatio >= 0.65f && metrics.LastOuterRatio <= 0.45f)
                return "shell";
            return "sweep";
        }

        private static void UpdatePlayabilityType(PlayabilityMetrics metrics, string typeId)
        {
            if (metrics == null)
                return;

            metrics.TypeId = string.IsNullOrWhiteSpace(typeId) ? "sweep" : typeId;
            metrics.Details = $"type={metrics.TypeId}; score={metrics.Score:0.0}; deps={metrics.DependencyBlocks}/{metrics.DependencyCycles}/{metrics.DependencyLayerStuck}";
        }

        private static float ScorePlayability(PlayabilityMetrics metrics, OuterFillMetrics outer, VisualQualityMetrics visual, AuthoredLevelData authored)
        {
            float openerScore = Mathf.Clamp(22f - Mathf.Abs(metrics.Openers - 4) * 4f, 0f, 22f);
            float choiceScore = 28f;
            if (metrics.AvgChoices < 2f)
                choiceScore -= (2f - metrics.AvgChoices) * 10f;
            if (metrics.AvgChoices > 5f)
                choiceScore -= (metrics.AvgChoices - 5f) * 5f;
            choiceScore -= metrics.LowChoiceSteps * 0.45f;
            choiceScore -= metrics.HighChoiceSteps * 0.35f;
            choiceScore = Mathf.Clamp(choiceScore, 0f, 28f);

            float unlockScore = Mathf.Min(24f, metrics.UnlockBursts * 4f + metrics.MaxUnlockBurst * 2.5f + metrics.KeyLockSteps * 1.5f);
            float phaseScore = Mathf.Clamp((metrics.FirstOuterRatio - metrics.LastOuterRatio + 0.25f) * 18f, 0f, 18f);
            float endingScore = Mathf.Clamp((1f - metrics.EndingShortRatio) * 10f, 0f, 10f);
            float regionScore = Mathf.Clamp(10f - metrics.RegionSwitches * 0.12f, 0f, 10f);
            float visualScore =
                outer.Band1Coverage * 18f +
                outer.Band2Coverage * 8f -
                outer.MaxEmptyRun * 1.5f -
                visual.EdgeShort * 0.25f -
                Mathf.Max(0, visual.MaxEdgeRun - 22) * 0.5f;
            visualScore = Mathf.Clamp(visualScore, 0f, 28f);

            float dependencyScore = Mathf.Clamp(metrics.DependencyBlocks * 0.03f + metrics.DependencyCycles * 1.5f - metrics.DependencyLayerStuck * 0.2f, 0f, 8f);
            return openerScore + choiceScore + unlockScore + phaseScore + endingScore + regionScore + visualScore + dependencyScore;
        }

        private static string FormatFamilies(List<string> families)
        {
            return families == null || families.Count == 0 ? string.Empty : string.Join("|", families);
        }

        private static string FormatTypeCounts(Dictionary<string, int> counts)
        {
            if (counts == null || counts.Count == 0)
                return string.Empty;

            var parts = new List<string>(counts.Count);
            foreach (var pair in counts)
                parts.Add($"{pair.Key}:{pair.Value}");
            parts.Sort(StringComparer.OrdinalIgnoreCase);
            return string.Join(" ", parts);
        }

        private static void BuildHoleTrimmedPackFromPack(
            string sourcePackPath,
            string outputPackPath,
            string reportPath,
            string packId,
            string displayName,
            string logTag,
            int targetCount,
            int removeCount,
            bool attachToDemo)
        {
            var sourcePack = AssetDatabase.LoadAssetAtPath<LevelPack>(sourcePackPath);
            var sourceLevels = sourcePack?.levels;
            if (sourceLevels == null || sourceLevels.Length == 0)
            {
                Debug.LogWarning($"[{logTag}] Source pack has no levels: {sourcePackPath}");
                SavePackAt(outputPackPath, packId, displayName, new List<LevelDefinition>());
                WriteLines(reportPath, new List<string> { "status,rank,id,reason" });
                return;
            }

            int actualRemove = Mathf.Clamp(Mathf.Max(removeCount, sourceLevels.Length - targetCount), 0, Mathf.Max(0, sourceLevels.Length - 1));
            var candidates = new List<HoleTrimCandidate>(sourceLevels.Length);
            for (int i = 0; i < sourceLevels.Length; i++)
            {
                var level = sourceLevels[i];
                var authored = level?.authoredLevel;
                if (authored?.arrows == null)
                    continue;

                candidates.Add(CalculateHoleTrimCandidate(level, i + 1));
            }

            var removalOrder = new List<HoleTrimCandidate>(candidates);
            removalOrder.Sort((a, b) => b.HoleScore.CompareTo(a.HoleScore));
            for (int i = 0; i < actualRemove && i < removalOrder.Count; i++)
                removalOrder[i].Remove = true;

            var kept = new List<LevelDefinition>(Mathf.Min(targetCount, candidates.Count));
            for (int i = 0; i < candidates.Count && kept.Count < targetCount; i++)
            {
                if (!candidates[i].Remove)
                    kept.Add(candidates[i].Level);
            }

            var report = new List<string>
            {
                "status,rank,newRank,id,type,width,height,chains,coverage,holeScore,localEmpty8,localEmpty8Cells,localEmpty8Total,localEmpty6,localEmpty6Cells,localEmpty6Total,centerEmpty"
            };
            int newRank = 0;
            for (int i = 0; i < candidates.Count; i++)
            {
                var c = candidates[i];
                string status = c.Remove ? "remove" : "keep";
                int keptRank = c.Remove ? 0 : ++newRank;
                report.Add(string.Join(",",
                    status,
                    c.Rank.ToString(),
                    keptRank.ToString(),
                    EscapeCsv(c.Level.levelId),
                    EscapeCsv(ClassifyHoleTrimType(c)),
                    c.Width.ToString(),
                    c.Height.ToString(),
                    c.Chains.ToString(),
                    c.Coverage.ToString("0.000"),
                    c.HoleScore.ToString("0.0"),
                    c.LocalEmpty8.ToString("0.000"),
                    c.LocalEmpty8Cells.ToString(),
                    c.LocalEmpty8Total.ToString(),
                    c.LocalEmpty6.ToString("0.000"),
                    c.LocalEmpty6Cells.ToString(),
                    c.LocalEmpty6Total.ToString(),
                    c.CenterEmpty.ToString("0.000")));
            }

            var pack = SavePackAt(outputPackPath, packId, displayName, kept);
            if (attachToDemo)
                AttachPackToDemo(pack, logTag);
            WriteLines(reportPath, report);
            Debug.Log($"[{logTag}] Hole trimmed kept={kept.Count}/{sourceLevels.Length}, removed={actualRemove}, path={outputPackPath}");
        }

        private static HoleTrimCandidate CalculateHoleTrimCandidate(LevelDefinition level, int rank)
        {
            var authored = level.authoredLevel;
            int width = Mathf.Max(1, authored.width);
            int height = Mathf.Max(1, authored.height);
            int chains = authored.arrows?.Count ?? 0;
            int area = Mathf.Max(1, width * height);
            float coverage = CountTiles(authored) / (float)area;

            float local8 = CalculateMaxLocalEmptyRatio(authored, Mathf.Min(8, Mathf.Min(width, height)), out int empty8, out int total8);
            float local6 = CalculateMaxLocalEmptyRatio(authored, Mathf.Min(6, Mathf.Min(width, height)), out int empty6, out int total6);
            float centerEmpty = CalculateCenterEmptyRatio(authored);
            float holeScore =
                (1f - coverage) * 120f +
                local8 * 75f +
                local6 * 45f +
                centerEmpty * 55f;

            return new HoleTrimCandidate
            {
                Level = level,
                Rank = rank,
                Width = width,
                Height = height,
                Chains = chains,
                Coverage = coverage,
                LocalEmpty8 = local8,
                LocalEmpty8Cells = empty8,
                LocalEmpty8Total = total8,
                LocalEmpty6 = local6,
                LocalEmpty6Cells = empty6,
                LocalEmpty6Total = total6,
                CenterEmpty = centerEmpty,
                HoleScore = holeScore
            };
        }

        private static float CalculateCenterEmptyRatio(AuthoredLevelData authored)
        {
            if (authored == null || authored.width <= 0 || authored.height <= 0)
                return 1f;

            int rectWidth = Mathf.Max(4, Mathf.RoundToInt(authored.width * 0.58f));
            int rectHeight = Mathf.Max(4, Mathf.RoundToInt(authored.height * 0.58f));
            rectWidth = Mathf.Min(rectWidth, authored.width);
            rectHeight = Mathf.Min(rectHeight, authored.height);
            int startX = Mathf.Max(0, (authored.width - rectWidth) / 2);
            int startY = Mathf.Max(0, (authored.height - rectHeight) / 2);
            var occupied = BuildOccupiedSet(authored);
            int empty = CountEmptyCellsInWindow(authored, occupied, startX, startY, rectWidth, rectHeight);
            return empty / (float)Mathf.Max(1, rectWidth * rectHeight);
        }

        private static string ClassifyHoleTrimType(HoleTrimCandidate candidate)
        {
            if (candidate == null)
                return string.Empty;
            if (candidate.CenterEmpty >= 0.12f)
                return "center-hole";
            if (candidate.LocalEmpty8 >= 0.22f)
                return "local-hole";
            if (candidate.Coverage <= 0.935f)
                return "low-coverage";
            return "mild-hole";
        }

        [MenuItem("Tools/ArrowMagic/Seed Variants/Export AB Candidate Scores")]
        public static void ExportABCandidateScores()
        {
            var sources = LoadPlainSources();
            var plans = BuildPlans(sources);
            PromoteKnownGreedySmokeCandidates(plans);

            var rows = new List<CandidateStats>(Mathf.Min(plans.Count, 5000));
            var seenBasePairs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            int checkedPlans = 0;
            int maxChecks = Mathf.Min(plans.Count, 5000);
            for (int i = 0; i < plans.Count && checkedPlans < maxChecks; i++)
            {
                Plan plan = plans[i];
                string basePairKey = MakeBasePairKey(plan);
                if (!seenBasePairs.Add(basePairKey))
                    continue;

                checkedPlans++;
                rows.Add(AnalyzeCandidate(plan));
            }

            rows.Sort(CompareCandidateStats);

            var lines = new List<string>(rows.Count + 1)
            {
                "rank,pairPriority,riskScore,sourceAType,sourceBType,sourceA,sourceB,basePair,layout,gap,width,height,chains,coverage,aspect,inwardPressure,crossBlocks,blockedChains,blockingChains,blockedByOtherMax,blocksOtherMax,topBlockedIndex,topBlockedOwner,topBlockedHits,topBlockerIndex,topBlockerOwner,topBlockerHits,planScore,details"
            };

            for (int i = 0; i < rows.Count; i++)
            {
                CandidateStats stat = rows[i];
                Plan plan = stat.Plan;
                lines.Add(string.Join(",",
                    i + 1,
                    stat.PairPriority,
                    stat.RiskScore.ToString("0.###"),
                    SourceTypeLabel(plan.A),
                    SourceTypeLabel(plan.B),
                    EscapeCsv(plan.A.Name),
                    EscapeCsv(plan.B.Name),
                    EscapeCsv(MakeBasePairKey(plan)),
                    LayoutLabel(plan),
                    plan.Gap,
                    plan.Width,
                    plan.Height,
                    plan.Chains,
                    plan.Coverage.ToString("0.000"),
                    plan.Aspect.ToString("0.000"),
                    stat.InwardPressure,
                    stat.CrossBlocks,
                    stat.BlockedChains,
                    stat.BlockingChains,
                    stat.BlockedByOtherMax,
                    stat.BlocksOtherMax,
                    stat.TopBlockedIndex,
                    stat.TopBlockedOwner,
                    stat.TopBlockedHits,
                    stat.TopBlockerIndex,
                    stat.TopBlockerOwner,
                    stat.TopBlockerHits,
                    plan.Score.ToString("0.###"),
                    EscapeCsv(stat.Details)));
            }

            WriteLines(CandidateScoreReportPath, lines);
            Debug.Log($"[CompositeSeedVariantScore] Candidate score report rows={rows.Count}, checkedPlans={checkedPlans}/{plans.Count}, path={CandidateScoreReportPath}");
        }

        private static void SortPlansByCandidateScore(List<Plan> plans, int analyzeLimit, string logTag)
        {
            int totalPlans = plans.Count;
            int maxChecks = analyzeLimit > 0 ? Mathf.Min(totalPlans, analyzeLimit) : totalPlans;
            var rows = new List<CandidateStats>(maxChecks);
            int checkedPlans = 0;

            for (int i = 0; i < plans.Count && checkedPlans < maxChecks; i++)
            {
                Plan plan = plans[i];
                checkedPlans++;
                rows.Add(AnalyzeCandidate(plan));
            }

            rows.Sort(CompareCandidateStats);
            plans.Clear();
            for (int i = 0; i < rows.Count; i++)
                plans.Add(rows[i].Plan);

            Debug.Log($"[{logTag}] Score sorted candidate plans={plans.Count}, checkedPlans={checkedPlans}/{totalPlans}, analyzeLimit={maxChecks}");
        }

        private static bool TryApplyPlanOrderCache(List<Plan> plans, string planOrderPath, string logTag)
        {
            if (plans == null || string.IsNullOrWhiteSpace(planOrderPath))
                return false;

            string full = ProjectRelativeToFullPath(planOrderPath);
            if (!File.Exists(full))
                return false;

            string[] lines = File.ReadAllLines(full);
            if (lines.Length < Mathf.Max(100, plans.Count / 2))
                return false;

            var byKey = new Dictionary<string, Plan>(plans.Count, StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < plans.Count; i++)
            {
                string key = MakePairKey(plans[i]);
                if (!byKey.ContainsKey(key))
                    byKey.Add(key, plans[i]);
            }

            var ordered = new List<Plan>(plans.Count);
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < lines.Length; i++)
            {
                string key = lines[i]?.Trim();
                if (string.IsNullOrWhiteSpace(key) || !seen.Add(key))
                    continue;
                if (byKey.TryGetValue(key, out var plan))
                    ordered.Add(plan);
            }

            if (ordered.Count == 0)
                return false;

            for (int i = 0; i < plans.Count; i++)
            {
                string key = MakePairKey(plans[i]);
                if (seen.Add(key))
                    ordered.Add(plans[i]);
            }

            plans.Clear();
            plans.AddRange(ordered);
            Debug.Log($"[{logTag}] Applied cached candidate order matched={ordered.Count}/{byKey.Count}, path={planOrderPath}");
            return true;
        }

        private static void SavePlanOrderCache(List<Plan> plans, string planOrderPath)
        {
            var lines = new List<string>(plans?.Count ?? 0);
            if (plans != null)
            {
                for (int i = 0; i < plans.Count; i++)
                    lines.Add(MakePairKey(plans[i]));
            }

            WriteLines(planOrderPath, lines);
        }

        private static void BuildGreedyRepairTriplets(
            string outputFolder,
            string packPath,
            string reportPath,
            string packId,
            string displayName,
            int targetGroups,
            int maxPlanChecks,
            string logTag,
            bool scoreSort = false,
            int scoreAnalyzeLimit = 0,
            string planOrderPath = null,
            ParentDiversityOptions diversity = null)
        {
            EnsureFolder(outputFolder);
            EnsureFolder(Path.GetDirectoryName(packPath)?.Replace("\\", "/"));
            ClearFolder(outputFolder);

            var sources = LoadPlainSources();
            var plans = BuildPlans(sources);
            PromoteKnownGreedySmokeCandidates(plans);
            if (scoreSort)
            {
                if (string.IsNullOrWhiteSpace(planOrderPath) || !TryApplyPlanOrderCache(plans, planOrderPath, logTag))
                {
                    SortPlansByCandidateScore(plans, scoreAnalyzeLimit, logTag);
                    if (!string.IsNullOrWhiteSpace(planOrderPath))
                        SavePlanOrderCache(plans, planOrderPath);
                }
            }

            var levels = new List<LevelDefinition>(targetGroups * 3);
            var report = new List<string>
            {
                "slot,group,kind,levelId,path,sourceA,sourceB,variant,layout,width,height,chains,coverage,crossBlocks,greedy,repair"
            };

            int group = 0;
            int checkedPlans = 0;
            var acceptedBasePairs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var failedPlanKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var acceptedBaseSources = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var acceptedExactSources = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var failureSummary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < plans.Count && group < targetGroups && checkedPlans < maxPlanChecks; i++)
            {
                Plan plan = plans[i];
                string basePairKey = MakeBasePairKey(plan);
                string planKey = MakePairKey(plan);
                if (acceptedBasePairs.Contains(basePairKey) || failedPlanKeys.Contains(planKey))
                    continue;
                if (!CanAcceptByParentDiversity(plan, diversity, group, acceptedBaseSources, acceptedExactSources))
                    continue;

                checkedPlans++;
                if (!TryBuildGreedyRepairComposite(plan, group + 1, out var variant, out string id, out int crossBlocks, out string repair, out string details))
                {
                    failedPlanKeys.Add(planKey);
                    AddFailureSummary(failureSummary, details);
                    if (i < 120)
                        Debug.LogWarning($"[{logTag}] Skip {planKey}: {details}");
                    continue;
                }

                string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{outputFolder}/{id}.asset");
                AssetDatabase.CreateAsset(variant, assetPath);
                acceptedBasePairs.Add(basePairKey);
                RecordAcceptedParentSources(plan, diversity, acceptedBaseSources, acceptedExactSources);
                group++;

                AppendGreedyCompareLevel(levels, report, group, "A", plan.A.Def, plan.A.Path, plan, variant, assetPath, crossBlocks, repair);
                AppendGreedyCompareLevel(levels, report, group, "B", plan.B.Def, plan.B.Path, plan, variant, assetPath, crossBlocks, repair);
                AppendGreedyCompareLevel(levels, report, group, "A+B greedy", variant, assetPath, plan, variant, assetPath, crossBlocks, repair);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            SavePackAt(packPath, packId, displayName, levels);
            WriteLines(reportPath, report);
            if (failureSummary.Count > 0)
                Debug.Log($"[{logTag}] Failure summary: {FormatFailureSummary(failureSummary)}");
            Debug.Log($"[{logTag}] Greedy repair triplet pack levels={levels.Count}, groups={levels.Count / 3}, checkedPlans={checkedPlans}/{plans.Count}, path={packPath}");
        }

        private static void BuildGreedyRepairTripletsResumeSegment(
            string outputFolder,
            string packPath,
            string reportPath,
            string failuresPath,
            string packId,
            string displayName,
            int targetGroups,
            int segmentGroups,
            int maxPlanChecks,
            string logTag,
            bool scoreSort = false,
            int scoreAnalyzeLimit = 0,
            string planOrderPath = null,
            ParentDiversityOptions diversity = null)
        {
            EnsureFolder(outputFolder);
            EnsureFolder(Path.GetDirectoryName(packPath)?.Replace("\\", "/"));

            var existingSuffixes = LoadExistingGreedyVariantSuffixes(outputFolder, out int existingAssets);
            if (existingAssets >= targetGroups)
            {
                BuildGeneratedFolderPack(outputFolder, packPath, reportPath, packId, displayName, logTag);
                Debug.Log($"[{logTag}] Already complete existingAssets={existingAssets}, target={targetGroups}");
                return;
            }

            var sources = LoadPlainSources();
            var plans = BuildPlans(sources);
            PromoteKnownGreedySmokeCandidates(plans);
            if (scoreSort)
            {
                if (string.IsNullOrWhiteSpace(planOrderPath) || !TryApplyPlanOrderCache(plans, planOrderPath, logTag))
                {
                    SortPlansByCandidateScore(plans, scoreAnalyzeLimit, logTag);
                    if (!string.IsNullOrWhiteSpace(planOrderPath))
                        SavePlanOrderCache(plans, planOrderPath);
                }
            }

            var acceptedBasePairs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var acceptedPlanKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var acceptedBaseSources = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var acceptedExactSources = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < plans.Count; i++)
            {
                Plan plan = plans[i];
                if (!existingSuffixes.Contains(MakeGreedyVariantSuffix(plan)))
                    continue;

                acceptedPlanKeys.Add(MakePairKey(plan));
                if (acceptedBasePairs.Add(MakeBasePairKey(plan)))
                    RecordAcceptedParentSources(plan, diversity, acceptedBaseSources, acceptedExactSources);
            }

            int matchedExistingPlans = acceptedPlanKeys.Count;
            var failedPlanKeys = LoadPlanKeySet(failuresPath);
            failedPlanKeys.ExceptWith(acceptedPlanKeys);
            var failureSummary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            int added = 0;
            int checkedPlans = 0;
            int startingGroups = Mathf.Max(existingAssets, acceptedBasePairs.Count);
            int targetThisSegment = Mathf.Min(targetGroups, startingGroups + Mathf.Max(1, segmentGroups));

            for (int i = 0; i < plans.Count &&
                 startingGroups + added < targetThisSegment &&
                 checkedPlans < maxPlanChecks; i++)
            {
                Plan plan = plans[i];
                string planKey = MakePairKey(plan);
                string basePairKey = MakeBasePairKey(plan);
                if (acceptedPlanKeys.Contains(planKey) ||
                    acceptedBasePairs.Contains(basePairKey) ||
                    failedPlanKeys.Contains(planKey))
                    continue;
                if (!CanAcceptByParentDiversity(plan, diversity, startingGroups + added, acceptedBaseSources, acceptedExactSources))
                    continue;

                checkedPlans++;
                int serial = existingAssets + added + 1;
                if (!TryBuildGreedyRepairComposite(plan, serial, out var variant, out string id, out int crossBlocks, out string repair, out string details))
                {
                    failedPlanKeys.Add(planKey);
                    AddFailureSummary(failureSummary, details);
                    if (checkedPlans <= 8)
                        Debug.Log($"[{logTag}] Segment skip {planKey}: {details}");
                    if (checkedPlans % 10 == 0)
                        SavePlanKeySet(failuresPath, failedPlanKeys);
                    continue;
                }

                string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{outputFolder}/{id}.asset");
                AssetDatabase.CreateAsset(variant, assetPath);
                AssetDatabase.SaveAssets();

                acceptedPlanKeys.Add(planKey);
                acceptedBasePairs.Add(basePairKey);
                RecordAcceptedParentSources(plan, diversity, acceptedBaseSources, acceptedExactSources);
                added++;

                Debug.Log($"[{logTag}] Segment accepted serial={serial}, total={existingAssets + added}/{targetGroups}, id={id}, crossBlocks={crossBlocks}, repair={repair}");
            }

            SavePlanKeySet(failuresPath, failedPlanKeys);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            BuildGeneratedFolderPack(outputFolder, packPath, reportPath, packId, displayName, logTag);
            if (failureSummary.Count > 0)
                Debug.Log($"[{logTag}] Segment failure summary: {FormatFailureSummary(failureSummary)}");
            Debug.Log($"[{logTag}] Segment done existingAssets={existingAssets}, matchedExisting={matchedExistingPlans}, added={added}, total={existingAssets + added}/{targetGroups}, checkedPlans={checkedPlans}/{plans.Count}, targetThisSegment={targetThisSegment}, path={packPath}");
        }

        private static ParentDiversityOptions CreateFinalParentDiversityOptions(int strictGroups)
        {
            return new ParentDiversityOptions
            {
                StrictGroups = strictGroups,
                StrictMaxBaseSourceUse = 3,
                RelaxedMaxBaseSourceUse = 4,
                StrictMaxExactSourceUse = 2,
                RelaxedMaxExactSourceUse = 3,
                UseFamilySourceLimit = true
            };
        }

        private static ParentDiversityOptions CreateCapacityProbeParentDiversityOptions()
        {
            return new ParentDiversityOptions
            {
                StrictGroups = 0,
                StrictMaxBaseSourceUse = 5,
                RelaxedMaxBaseSourceUse = 5,
                StrictMaxExactSourceUse = 3,
                RelaxedMaxExactSourceUse = 3,
                UseFamilySourceLimit = true
            };
        }

        private static ParentDiversityOptions CreateCapacityAuditParentDiversityOptions()
        {
            return new ParentDiversityOptions
            {
                StrictGroups = CapacityAuditParentCount,
                StrictMaxBaseSourceUse = CapacityAuditParentMaxFamilyUse,
                RelaxedMaxBaseSourceUse = CapacityAuditParentMaxFamilyUse,
                StrictMaxExactSourceUse = 0,
                RelaxedMaxExactSourceUse = 0,
                UseFamilySourceLimit = true
            };
        }

        private static bool CanAcceptByParentDiversity(
            Plan plan,
            ParentDiversityOptions diversity,
            int acceptedGroups,
            Dictionary<string, int> baseSourceCounts,
            Dictionary<string, int> exactSourceCounts)
        {
            if (diversity == null)
                return true;

            int maxBaseUse = acceptedGroups < diversity.StrictGroups
                ? diversity.StrictMaxBaseSourceUse
                : diversity.RelaxedMaxBaseSourceUse;
            int maxExactUse = acceptedGroups < diversity.StrictGroups
                ? diversity.StrictMaxExactSourceUse
                : diversity.RelaxedMaxExactSourceUse;

            return SourceUseBelowLimit(MakeDiversitySourceKey(plan.A, diversity), baseSourceCounts, maxBaseUse) &&
                   SourceUseBelowLimit(MakeDiversitySourceKey(plan.B, diversity), baseSourceCounts, maxBaseUse) &&
                   SourceUseBelowLimit(plan.A.Name, exactSourceCounts, maxExactUse) &&
                   SourceUseBelowLimit(plan.B.Name, exactSourceCounts, maxExactUse);
        }

        private static bool SourceUseBelowLimit(string key, Dictionary<string, int> counts, int maxUse)
        {
            if (maxUse <= 0 || string.IsNullOrWhiteSpace(key))
                return true;

            return !counts.TryGetValue(key, out int count) || count < maxUse;
        }

        private static void RecordAcceptedParentSources(
            Plan plan,
            ParentDiversityOptions diversity,
            Dictionary<string, int> baseSourceCounts,
            Dictionary<string, int> exactSourceCounts)
        {
            IncrementCount(baseSourceCounts, MakeDiversitySourceKey(plan.A, diversity));
            IncrementCount(baseSourceCounts, MakeDiversitySourceKey(plan.B, diversity));
            IncrementCount(exactSourceCounts, plan.A.Name);
            IncrementCount(exactSourceCounts, plan.B.Name);
        }

        private static void IncrementCount(Dictionary<string, int> counts, string key)
        {
            if (counts == null || string.IsNullOrWhiteSpace(key))
                return;

            counts[key] = counts.TryGetValue(key, out int count) ? count + 1 : 1;
        }

        private static void AddFailureSummary(Dictionary<string, int> summary, string details)
        {
            if (summary == null)
                return;

            string reason = SummarizeFailureReason(details);
            summary[reason] = summary.TryGetValue(reason, out int count) ? count + 1 : 1;
        }

        private static string SummarizeFailureReason(string details)
        {
            if (string.IsNullOrWhiteSpace(details))
                return "unknown";

            int space = details.IndexOf(' ');
            int equals = details.IndexOf('=');
            int cut = space < 0 ? equals : equals < 0 ? space : Mathf.Min(space, equals);
            return cut > 0 ? details.Substring(0, cut) : details;
        }

        private static string FormatFailureSummary(Dictionary<string, int> summary)
        {
            var items = new List<KeyValuePair<string, int>>(summary);
            items.Sort((a, b) =>
            {
                int c = b.Value.CompareTo(a.Value);
                if (c != 0) return c;
                return string.Compare(a.Key, b.Key, StringComparison.OrdinalIgnoreCase);
            });

            int count = Mathf.Min(8, items.Count);
            var parts = new List<string>(count);
            for (int i = 0; i < count; i++)
                parts.Add($"{items[i].Key}:{items[i].Value}");

            return string.Join("; ", parts);
        }

        private static List<SourceSeed> LoadPlainSources()
        {
            var result = new List<SourceSeed>();
            string[] guids = AssetDatabase.FindAssets("t:LevelDefinition", new[] { SeedRoot });
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                if (string.IsNullOrWhiteSpace(path))
                    continue;

                string name = Path.GetFileNameWithoutExtension(path);
                if (!IsAllowedSourceName(name))
                    continue;

                string lower = path.ToLowerInvariant();
                if (lower.Contains("seed_mask") || lower.Contains("rawclip") || lower.Contains("largecomposite") || lower.Contains("compositeseedvariants"))
                    continue;

                var def = AssetDatabase.LoadAssetAtPath<LevelDefinition>(path);
                if (def == null || def.source != LevelDefinition.LevelSource.Authored || def.authoredLevel?.arrows == null)
                    continue;

                int width = Mathf.Max(1, def.authoredLevel.width);
                int height = Mathf.Max(1, def.authoredLevel.height);
                int chains = def.authoredLevel.arrows.Count;
                int tiles = CountTiles(def.authoredLevel);
                float coverage = tiles / (float)Mathf.Max(1, width * height);
                float aspect = Mathf.Max(width, height) / (float)Mathf.Max(1, Mathf.Min(width, height));
                if (chains < 40 || chains > 115 || coverage < 0.76f || aspect > 1.95f)
                    continue;

                CountHeadDirections(def.authoredLevel, out int left, out int right, out int up, out int down);
                ComputeSourceProfile(
                    def.authoredLevel,
                    out float centerX,
                    out float centerY,
                    out float leftDensity,
                    out float rightDensity,
                    out float topDensity,
                    out float bottomDensity,
                    out float maxLocalEmptyRatio);
                result.Add(new SourceSeed
                {
                    Def = def,
                    Path = path,
                    Name = name,
                    Width = width,
                    Height = height,
                    Chains = chains,
                    Tiles = tiles,
                    Coverage = coverage,
                    Aspect = aspect,
                    HeadLeft = left,
                    HeadRight = right,
                    HeadUp = up,
                    HeadDown = down,
                    SourceTier = GetSourceTier(path, name),
                    CenterX = centerX,
                    CenterY = centerY,
                    LeftDensity = leftDensity,
                    RightDensity = rightDensity,
                    TopDensity = topDensity,
                    BottomDensity = bottomDensity,
                    MaxLocalEmptyRatio = maxLocalEmptyRatio
                });
            }

            result.Sort((a, b) =>
            {
                int c = b.Chains.CompareTo(a.Chains);
                if (c != 0) return c;
                c = b.Coverage.CompareTo(a.Coverage);
                if (c != 0) return c;
                return string.Compare(a.Name, b.Name, StringComparison.Ordinal);
            });
            return result;
        }

        private static void ComputeSourceProfile(
            AuthoredLevelData authored,
            out float centerX,
            out float centerY,
            out float leftDensity,
            out float rightDensity,
            out float topDensity,
            out float bottomDensity,
            out float maxLocalEmptyRatio)
        {
            int width = Mathf.Max(1, authored?.width ?? 1);
            int height = Mathf.Max(1, authored?.height ?? 1);
            var occupied = BuildOccupiedSet(authored);
            if (occupied.Count == 0)
            {
                centerX = 0.5f;
                centerY = 0.5f;
                leftDensity = rightDensity = topDensity = bottomDensity = 0f;
                maxLocalEmptyRatio = 1f;
                return;
            }

            long sumX = 0;
            long sumY = 0;
            int left = 0;
            int right = 0;
            int top = 0;
            int bottom = 0;
            int splitX = Mathf.Max(1, width / 2);
            int splitY = Mathf.Max(1, height / 2);

            foreach (int index in occupied)
            {
                int x = index % width;
                int y = index / width;
                sumX += x;
                sumY += y;
                if (x < splitX) left++; else right++;
                if (y < splitY) top++; else bottom++;
            }

            centerX = sumX / (float)Mathf.Max(1, occupied.Count) / Mathf.Max(1, width - 1);
            centerY = sumY / (float)Mathf.Max(1, occupied.Count) / Mathf.Max(1, height - 1);
            leftDensity = left / (float)Mathf.Max(1, splitX * height);
            rightDensity = right / (float)Mathf.Max(1, (width - splitX) * height);
            topDensity = top / (float)Mathf.Max(1, width * splitY);
            bottomDensity = bottom / (float)Mathf.Max(1, width * (height - splitY));
            maxLocalEmptyRatio = CalculateMaxLocalEmptyRatio(authored, Mathf.Min(6, Mathf.Min(width, height)), out _, out _);
        }

        private static float SourceStructureMismatch(SourceSeed a, SourceSeed b)
        {
            if (a == null || b == null)
                return 1f;

            float aspectDiff = Mathf.Abs(a.Aspect - b.Aspect) / 0.75f;
            float coverageDiff = Mathf.Abs(a.Coverage - b.Coverage) / 0.20f;
            float centerDiff = (Mathf.Abs(a.CenterX - b.CenterX) + Mathf.Abs(a.CenterY - b.CenterY)) * 0.5f;
            float densityDiff =
                (Mathf.Abs(a.LeftDensity - b.LeftDensity) +
                 Mathf.Abs(a.RightDensity - b.RightDensity) +
                 Mathf.Abs(a.TopDensity - b.TopDensity) +
                 Mathf.Abs(a.BottomDensity - b.BottomDensity)) * 0.25f;
            float localDiff = Mathf.Abs(a.MaxLocalEmptyRatio - b.MaxLocalEmptyRatio);

            return aspectDiff * 0.18f +
                   coverageDiff * 0.18f +
                   centerDiff * 0.28f +
                   densityDiff * 0.48f +
                   localDiff * 0.12f;
        }

        private static void AddGeneratedCompositeSources(List<string> paths, string folder)
        {
            if (string.IsNullOrWhiteSpace(folder) || !AssetDatabase.IsValidFolder(folder))
                return;

            string[] guids = AssetDatabase.FindAssets("t:LevelDefinition", new[] { folder });
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                if (string.IsNullOrWhiteSpace(path) || paths.Contains(path))
                    continue;

                string name = Path.GetFileNameWithoutExtension(path);
                bool generatedComposite =
                    name.StartsWith("composite_greedy_ab_", StringComparison.OrdinalIgnoreCase) ||
                    name.StartsWith("composite_scoretop10_", StringComparison.OrdinalIgnoreCase) ||
                    name.StartsWith("composite_portrait150_", StringComparison.OrdinalIgnoreCase) ||
                    name.StartsWith("composite_portrait_v", StringComparison.OrdinalIgnoreCase) ||
                    name.StartsWith("composite_play_cand_", StringComparison.OrdinalIgnoreCase);
                if (!generatedComposite)
                    continue;

                paths.Add(path);
            }

            paths.Sort(StringComparer.OrdinalIgnoreCase);
        }

        private static List<SourceSeed> LoadGeneratedCompositeSourceSeeds(params string[] folders)
        {
            var paths = new List<string>();
            if (folders != null)
            {
                for (int i = 0; i < folders.Length; i++)
                    AddGeneratedCompositeSources(paths, folders[i]);
            }

            var result = new List<SourceSeed>(paths.Count);
            for (int i = 0; i < paths.Count; i++)
            {
                string path = paths[i];
                var def = AssetDatabase.LoadAssetAtPath<LevelDefinition>(path);
                if (def == null || def.authoredLevel?.arrows == null)
                    continue;

                string name = Path.GetFileNameWithoutExtension(path);
                if (!TryCreateSourceSeed(
                        def,
                        path,
                        name,
                        MinChains,
                        MaxChains,
                        MinCoverage,
                        MaxAspect,
                        sourceTier: 0,
                        out var source))
                    continue;

                result.Add(source);
            }

            result.Sort((a, b) =>
            {
                int c = b.Coverage.CompareTo(a.Coverage);
                if (c != 0) return c;
                c = a.Chains.CompareTo(b.Chains);
                if (c != 0) return c;
                return string.Compare(a.Name, b.Name, StringComparison.Ordinal);
            });
            return result;
        }

        private static bool TryCreateSourceSeed(
            LevelDefinition def,
            string path,
            string name,
            int minChains,
            int maxChains,
            float minCoverage,
            float maxAspect,
            int sourceTier,
            out SourceSeed source)
        {
            source = null;
            if (def == null || def.authoredLevel?.arrows == null)
                return false;

            int width = Mathf.Max(1, def.authoredLevel.width);
            int height = Mathf.Max(1, def.authoredLevel.height);
            int chains = def.authoredLevel.arrows.Count;
            int tiles = CountTiles(def.authoredLevel);
            float coverage = tiles / (float)Mathf.Max(1, width * height);
            float aspect = Mathf.Max(width, height) / (float)Mathf.Max(1, Mathf.Min(width, height));
            if (chains < minChains || chains > maxChains || coverage < minCoverage || aspect > maxAspect)
                return false;

            CountHeadDirections(def.authoredLevel, out int left, out int right, out int up, out int down);
            ComputeSourceProfile(
                def.authoredLevel,
                out float centerX,
                out float centerY,
                out float leftDensity,
                out float rightDensity,
                out float topDensity,
                out float bottomDensity,
                out float maxLocalEmptyRatio);

            source = new SourceSeed
            {
                Def = def,
                Path = path,
                Name = name,
                Width = width,
                Height = height,
                Chains = chains,
                Tiles = tiles,
                Coverage = coverage,
                Aspect = aspect,
                HeadLeft = left,
                HeadRight = right,
                HeadUp = up,
                HeadDown = down,
                SourceTier = sourceTier,
                CenterX = centerX,
                CenterY = centerY,
                LeftDensity = leftDensity,
                RightDensity = rightDensity,
                TopDensity = topDensity,
                BottomDensity = bottomDensity,
                MaxLocalEmptyRatio = maxLocalEmptyRatio
            };
            return true;
        }

        private static bool CompositeNameContainsBaseLevel(string compositeName, SourceSeed source)
        {
            if (string.IsNullOrWhiteSpace(compositeName) || source == null)
                return false;

            string key = MakeBaseSourceKey(source);
            var match = Regex.Match(key, @"a(\d+)", RegexOptions.IgnoreCase);
            return match.Success &&
                   compositeName.IndexOf($"a{match.Groups[1].Value}", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static AuthoredLevelData TransformAuthored(AuthoredLevelData source, string mode)
        {
            bool rotate = mode == "rot90" || mode == "rot270" || mode == "transpose" || mode == "antiTranspose";
            int sourceWidth = Mathf.Max(1, source.width);
            int sourceHeight = Mathf.Max(1, source.height);
            int width = rotate ? sourceHeight : sourceWidth;
            int height = rotate ? sourceWidth : sourceHeight;
            var result = new AuthoredLevelData
            {
                width = width,
                height = height,
                arrows = new List<AuthoredArrowData>(source.arrows?.Count ?? 0)
            };

            if (source.arrows == null)
                return result;

            for (int i = 0; i < source.arrows.Count; i++)
            {
                var arrow = source.arrows[i];
                var indices = new List<int>(arrow?.indices?.Count ?? 0);
                if (arrow?.indices != null)
                {
                    for (int j = 0; j < arrow.indices.Count; j++)
                    {
                        int index = arrow.indices[j];
                        int x = index % sourceWidth;
                        int y = index / sourceWidth;
                        TransformPoint(sourceWidth, sourceHeight, mode, x, y, out int tx, out int ty);
                        indices.Add(ty * width + tx);
                    }
                }

                result.arrows.Add(new AuthoredArrowData
                {
                    indices = indices,
                    colorIndex = arrow?.colorIndex ?? 0
                });
            }

            return result;
        }

        private static string GetPreferredFinalTransform(AuthoredLevelData source, int passIndex)
        {
            string[] order = IsWideLandscape(source) ? WidePortraitPreferredTransforms : StandardFinalTransforms;
            return order[Mathf.Clamp(passIndex, 0, order.Length - 1)];
        }

        private static bool IsWideLandscape(AuthoredLevelData source)
        {
            int width = Mathf.Max(1, source?.width ?? 1);
            int height = Mathf.Max(1, source?.height ?? 1);
            return width > height && width / (float)height >= WideParentPortraitTransformAspect;
        }

        private static void TransformPoint(int width, int height, string mode, int x, int y, out int tx, out int ty)
        {
            switch (mode)
            {
                case "hflip":
                    tx = width - 1 - x;
                    ty = y;
                    break;
                case "vflip":
                    tx = x;
                    ty = height - 1 - y;
                    break;
                case "rot180":
                    tx = width - 1 - x;
                    ty = height - 1 - y;
                    break;
                case "rot90":
                    tx = height - 1 - y;
                    ty = x;
                    break;
                case "rot270":
                    tx = y;
                    ty = width - 1 - x;
                    break;
                case "transpose":
                    tx = y;
                    ty = x;
                    break;
                case "antiTranspose":
                    tx = height - 1 - y;
                    ty = width - 1 - x;
                    break;
                default:
                    tx = x;
                    ty = y;
                    break;
            }
        }

        private static int ApplyReversePattern(AuthoredLevelData authored, string pattern)
        {
            if (authored?.arrows == null || pattern == "keep")
                return 0;

            int reversed = 0;
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var arrow = authored.arrows[i];
                if (arrow?.indices == null || arrow.indices.Count < 2)
                    continue;

                if (!ShouldReverseChain(authored, arrow, i, pattern))
                    continue;

                arrow.indices.Reverse();
                reversed++;
            }

            return reversed;
        }

        private static int TrimOuterStraightExitChains(AuthoredLevelData authored, int maxRemove, int minLength)
        {
            if (authored?.arrows == null || maxRemove <= 0)
                return 0;

            int removed = 0;
            for (int i = authored.arrows.Count - 1; i >= 0 && removed < maxRemove; i--)
            {
                var arrow = authored.arrows[i];
                if (!IsOuterStraightExitChain(authored, arrow, minLength))
                    continue;

                authored.arrows.RemoveAt(i);
                removed++;
            }

            return removed;
        }

        private static int TrimDenseOuterShortChains(
            AuthoredLevelData authored,
            int maxRemove,
            int radius,
            int maxNearbyShortChains)
        {
            if (authored?.arrows == null || maxRemove <= 0)
                return 0;

            var candidates = new List<ShortChainClusterCandidate>();
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var arrow = authored.arrows[i];
                int length = arrow?.indices?.Count ?? 0;
                if (length <= 0 || length > 3 || !ChainTouchesEdge(authored, arrow))
                    continue;

                int nearby = 0;
                for (int j = 0; j < authored.arrows.Count; j++)
                {
                    if (i == j)
                        continue;

                    var other = authored.arrows[j];
                    int otherLength = other?.indices?.Count ?? 0;
                    if (otherLength <= 0 || otherLength > 3 || !ChainTouchesEdge(authored, other))
                        continue;
                    if (ChainsWithinRadius(authored, arrow.indices, other.indices, radius))
                        nearby++;
                }

                if (nearby >= maxNearbyShortChains)
                {
                    candidates.Add(new ShortChainClusterCandidate
                    {
                        Index = i,
                        Length = length,
                        NearbyShortChains = nearby
                    });
                }
            }

            candidates.Sort((a, b) =>
            {
                int byNearby = b.NearbyShortChains.CompareTo(a.NearbyShortChains);
                if (byNearby != 0)
                    return byNearby;
                return a.Length.CompareTo(b.Length);
            });

            int removeCount = Mathf.Min(maxRemove, candidates.Count);
            if (removeCount <= 0)
                return 0;

            var removeIndices = new List<int>(removeCount);
            for (int i = 0; i < removeCount; i++)
                removeIndices.Add(candidates[i].Index);
            removeIndices.Sort((a, b) => b.CompareTo(a));

            for (int i = 0; i < removeIndices.Count; i++)
                authored.arrows.RemoveAt(removeIndices[i]);

            return removeIndices.Count;
        }

        private struct ShortChainClusterCandidate
        {
            public int Index;
            public int Length;
            public int NearbyShortChains;
        }

        private static bool ChainsWithinRadius(AuthoredLevelData authored, List<int> a, List<int> b, int radius)
        {
            if (a == null || b == null || authored == null || authored.width <= 0)
                return false;

            for (int i = 0; i < a.Count; i++)
            {
                int ax = a[i] % authored.width;
                int ay = a[i] / authored.width;
                for (int j = 0; j < b.Count; j++)
                {
                    int bx = b[j] % authored.width;
                    int by = b[j] / authored.width;
                    if (Mathf.Abs(ax - bx) <= radius && Mathf.Abs(ay - by) <= radius)
                        return true;
                }
            }

            return false;
        }

        private static bool IsOuterStraightExitChain(AuthoredLevelData authored, AuthoredArrowData arrow, int minLength)
        {
            var indices = arrow?.indices;
            if (indices == null || indices.Count < Mathf.Max(2, minLength) || authored.width <= 0 || authored.height <= 0)
                return false;

            int head = indices[0];
            int neck = indices[1];
            int hx = head % authored.width;
            int hy = head / authored.width;
            int nx = neck % authored.width;
            int ny = neck / authored.width;
            int dx = Math.Sign(hx - nx);
            int dy = Math.Sign(hy - ny);
            if (Mathf.Abs(dx) + Mathf.Abs(dy) != 1)
                return false;

            bool exitsBoard =
                (hx == 0 && dx < 0) ||
                (hx == authored.width - 1 && dx > 0) ||
                (hy == 0 && dy < 0) ||
                (hy == authored.height - 1 && dy > 0);
            if (!exitsBoard)
                return false;

            for (int i = 1; i < indices.Count; i++)
            {
                int prev = indices[i - 1];
                int current = indices[i];
                int px = prev % authored.width;
                int py = prev / authored.width;
                int cx = current % authored.width;
                int cy = current / authored.width;
                if (cx - px != -dx || cy - py != -dy)
                    return false;
            }

            return true;
        }

        private static int ApplyEdgeFillMode(AuthoredLevelData authored, string fillMode)
        {
            if (authored?.arrows == null || string.IsNullOrWhiteSpace(fillMode) || fillMode == "none")
                return 0;

            if (fillMode.StartsWith("split", StringComparison.OrdinalIgnoreCase))
            {
                if (!int.TryParse(fillMode.Substring(5), out int maxSplits) || maxSplits <= 0)
                    return 0;

                return SplitStraightPrefixChains(authored, maxSplits);
            }

            if (!fillMode.StartsWith("edge", StringComparison.OrdinalIgnoreCase))
                return 0;

            if (!int.TryParse(fillMode.Substring(4), out int maxNewChains) || maxNewChains <= 0)
                return 0;

            bool strongPortraitFill = maxNewChains >= 20;
            int maxRunLength = strongPortraitFill ? MaxPortraitOuterFillRunLength : 10;
            int minRunLength = strongPortraitFill ? MinPortraitOuterFillRunLength : MinOuterFillRunLength;
            var metas = new List<ChainMeta>(authored.arrows.Count + maxNewChains);
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                metas.Add(new ChainMeta
                {
                    SourceIndex = 0,
                    TileCount = authored.arrows[i]?.indices?.Count ?? 0
                });
            }

            int filled = TryFillEdgeVisibleEmptyRuns(authored, metas, maxNewChains, maxRunLength, minRunLength);
            if (strongPortraitFill && filled < maxNewChains)
            {
                filled += TryExtendOuterEndpointGaps(authored, maxExtensions: maxNewChains * 2);
                filled += TryFillOuterSnakeGaps(
                    authored,
                    metas,
                    maxNewChains - filled,
                    maxRunLength,
                    minRunLength,
                    PortraitOuterSnakeBandDepth);
            }

            if (filled < maxNewChains)
                filled += TryFillVisibleExitEmptyRuns(authored, metas, maxNewChains - filled, maxRunLength, minRunLength);
            if (strongPortraitFill && filled < maxNewChains)
            {
                filled += TrySplitOuterNeighborChains(
                    authored,
                    metas,
                    maxSplits: Mathf.Min(16, maxNewChains - filled),
                    minDetachedLength: 4,
                    maxDetachedLength: 14,
                    minRemainingLength: 4);
            }

            if (strongPortraitFill && filled < maxNewChains)
            {
                int shortBudget = Mathf.Min(MaxPortraitOuterShortPatchChains, maxNewChains - filled);
                filled += TryFillOuterShortEdgeGaps(
                    authored,
                    metas,
                    shortBudget,
                    maxLength: 3,
                    minLength: MinPortraitOuterPatchRunLength,
                    PortraitOuterShortClusterRadius,
                    maxNearbyShortChains: 2);
            }

            return filled;
        }

        private static int SplitStraightPrefixChains(AuthoredLevelData authored, int maxSplits)
        {
            if (authored?.arrows == null || maxSplits <= 0)
                return 0;

            int splits = 0;
            for (int i = 0; i < authored.arrows.Count && splits < maxSplits; i++)
            {
                var arrow = authored.arrows[i];
                var indices = arrow?.indices;
                if (indices == null || indices.Count < 5)
                    continue;

                int splitAt = FindStraightPrefixSplit(indices, authored.width);
                if (splitAt < 2 || splitAt > indices.Count - 2)
                    continue;

                var first = indices.GetRange(0, splitAt);
                var second = indices.GetRange(splitAt, indices.Count - splitAt);
                if (first.Count < 2 || second.Count < 2)
                    continue;

                arrow.indices = first;
                authored.arrows.Insert(i + 1, new AuthoredArrowData
                {
                    indices = second,
                    colorIndex = (arrow.colorIndex + 7 + splits) % 16
                });
                splits++;
                i++;
            }

            return splits;
        }

        private static int FindStraightPrefixSplit(List<int> indices, int width)
        {
            if (indices == null || indices.Count < 5 || width <= 0)
                return -1;

            int firstStepX = indices[1] % width - indices[0] % width;
            int firstStepY = indices[1] / width - indices[0] / width;
            if (Mathf.Abs(firstStepX) + Mathf.Abs(firstStepY) != 1)
                return -1;

            int straightPrefix = 2;
            for (int i = 2; i < indices.Count; i++)
            {
                int stepX = indices[i] % width - indices[i - 1] % width;
                int stepY = indices[i] / width - indices[i - 1] / width;
                if (stepX != firstStepX || stepY != firstStepY)
                    break;

                straightPrefix++;
            }

            int maxSplit = Mathf.Min(straightPrefix - 2, indices.Count - 2);
            if (maxSplit < 2)
                return -1;

            return Mathf.Min(maxSplit, 6);
        }

        private static bool ShouldReverseChain(AuthoredLevelData authored, AuthoredArrowData arrow, int index, string pattern)
        {
            switch (pattern)
            {
                case "rev_all":
                    return true;
                case "rev_even":
                    return (index & 1) == 0;
                case "rev_odd":
                    return (index & 1) == 1;
                case "rev_mod3_0":
                    return index % 3 == 0;
                case "rev_mod3_1":
                    return index % 3 == 1;
                case "rev_mod3_2":
                    return index % 3 == 2;
                case "rev_long":
                    return arrow.indices.Count >= 8;
                case "rev_short":
                    return arrow.indices.Count <= 4;
                case "rev_edge":
                    return ChainTouchesEdge(authored, arrow);
                default:
                    return false;
            }
        }

        private static bool ChainTouchesEdge(AuthoredLevelData authored, AuthoredArrowData arrow)
        {
            int width = Mathf.Max(1, authored.width);
            int height = Mathf.Max(1, authored.height);
            for (int i = 0; i < arrow.indices.Count; i++)
            {
                int index = arrow.indices[i];
                int x = index % width;
                int y = index / width;
                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    return true;
            }

            return false;
        }

        private static VisualQualityMetrics CalculateVisualQualityMetrics(AuthoredLevelData authored)
        {
            var metrics = new VisualQualityMetrics();
            if (authored?.arrows == null || authored.width <= 0 || authored.height <= 0)
                return metrics;

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var arrow = authored.arrows[i];
                var indices = arrow?.indices;
                if (indices == null || indices.Count == 0 || !ChainTouchesOuterBand(authored, arrow, 1))
                    continue;

                int run = Mathf.Max(
                    LongestDirectionalRunInRect(indices, authored.width, 0, authored.width - 1, 0, authored.height - 1, vertical: true),
                    LongestDirectionalRunInRect(indices, authored.width, 0, authored.width - 1, 0, authored.height - 1, vertical: false));
                metrics.MaxEdgeRun = Mathf.Max(metrics.MaxEdgeRun, run);

                if (indices.Count <= 4)
                    metrics.EdgeShort++;

                if (indices.Count >= 10 && run >= 10)
                {
                    metrics.EdgeLong++;
                    if (ChainTouchesCornerBand(authored, arrow, 1))
                        metrics.CornerLong++;
                }
            }

            metrics.Penalty =
                metrics.EdgeLong * 5 +
                metrics.CornerLong * 4 +
                metrics.EdgeShort +
                Mathf.Max(0, metrics.MaxEdgeRun - 14);
            return metrics;
        }

        private static bool ChainTouchesOuterBand(AuthoredLevelData authored, AuthoredArrowData arrow, int band)
        {
            int width = Mathf.Max(1, authored.width);
            int height = Mathf.Max(1, authored.height);
            int maxX = width - 1;
            int maxY = height - 1;
            for (int i = 0; i < arrow.indices.Count; i++)
            {
                int index = arrow.indices[i];
                int x = index % width;
                int y = index / width;
                if (x <= band || y <= band || x >= maxX - band || y >= maxY - band)
                    return true;
            }

            return false;
        }

        private static bool ChainTouchesCornerBand(AuthoredLevelData authored, AuthoredArrowData arrow, int band)
        {
            int width = Mathf.Max(1, authored.width);
            int height = Mathf.Max(1, authored.height);
            int maxX = width - 1;
            int maxY = height - 1;
            bool left = false;
            bool right = false;
            bool top = false;
            bool bottom = false;
            for (int i = 0; i < arrow.indices.Count; i++)
            {
                int index = arrow.indices[i];
                int x = index % width;
                int y = index / width;
                left |= x <= band;
                right |= x >= maxX - band;
                top |= y <= band;
                bottom |= y >= maxY - band;
            }

            return (left && top) || (left && bottom) || (right && top) || (right && bottom);
        }

        private static OuterFillMetrics CalculateOuterFillMetrics(AuthoredLevelData authored)
        {
            var metrics = new OuterFillMetrics();
            if (authored == null || authored.width <= 0 || authored.height <= 0)
                return metrics;

            var occupied = BuildOccupiedSet(authored);
            for (int y = 0; y < authored.height; y++)
            {
                for (int x = 0; x < authored.width; x++)
                {
                    int index = x + y * authored.width;
                    if (IsOuterBandCell(authored.width, authored.height, x, y, 1))
                    {
                        metrics.Band1Total++;
                        if (occupied.Contains(index))
                            metrics.Band1Filled++;
                    }

                    if (IsOuterBandCell(authored.width, authored.height, x, y, 2))
                    {
                        metrics.Band2Total++;
                        if (occupied.Contains(index))
                            metrics.Band2Filled++;
                    }
                }
            }

            metrics.MaxEmptyRun = CalculateMaxOuterEmptyRun(authored, occupied);
            return metrics;
        }

        private static bool IsOuterBandCell(int width, int height, int x, int y, int band)
        {
            int maxX = Mathf.Max(0, width - 1);
            int maxY = Mathf.Max(0, height - 1);
            return x < band || y < band || x > maxX - band || y > maxY - band;
        }

        private static int CalculateMaxOuterEmptyRun(AuthoredLevelData authored, HashSet<int> occupied)
        {
            int maxRun = 0;
            int current = 0;
            for (int x = 0; x < authored.width; x++)
                AccumulateOuterEmpty(authored, occupied, x, 0, ref current, ref maxRun);
            for (int y = 1; y < authored.height; y++)
                AccumulateOuterEmpty(authored, occupied, authored.width - 1, y, ref current, ref maxRun);
            for (int x = authored.width - 2; x >= 0; x--)
                AccumulateOuterEmpty(authored, occupied, x, authored.height - 1, ref current, ref maxRun);
            for (int y = authored.height - 2; y > 0; y--)
                AccumulateOuterEmpty(authored, occupied, 0, y, ref current, ref maxRun);

            return maxRun;
        }

        private static void AccumulateOuterEmpty(AuthoredLevelData authored, HashSet<int> occupied, int x, int y, ref int current, ref int maxRun)
        {
            int index = x + y * authored.width;
            if (occupied.Contains(index))
            {
                current = 0;
                return;
            }

            current++;
            maxRun = Mathf.Max(maxRun, current);
        }

        private static string MakeAuthoredSignature(AuthoredLevelData authored)
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + authored.width;
                hash = hash * 31 + authored.height;
                if (authored.arrows != null)
                {
                    for (int i = 0; i < authored.arrows.Count; i++)
                    {
                        var arrow = authored.arrows[i];
                        hash = hash * 31 + (arrow?.colorIndex ?? 0);
                        if (arrow?.indices == null)
                            continue;
                        for (int j = 0; j < arrow.indices.Count; j++)
                            hash = hash * 31 + arrow.indices[j];
                    }
                }

                return $"{authored.width}x{authored.height}:{authored.arrows?.Count ?? 0}:{hash}";
            }
        }

        private static bool ValidateAuthoredGreedy(AuthoredLevelData authored, out string details)
        {
            details = string.Empty;
            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out var board, out string buildError))
            {
                details = $"build failed {buildError}";
                return false;
            }

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int greedyBudget = Mathf.Max(FastGreedyBudgetMin, (authored.arrows?.Count ?? 0) * FastGreedyBudgetPerChain);
            if (!GreedyValidator.TryClearAllByGreedy(board, rules, greedyBudget, out _))
            {
                details = $"fast greedy failed budget={greedyBudget}";
                return false;
            }

            details = $"fast greedy budget={greedyBudget}";
            return true;
        }

        private static LevelDefinition CreateDerivedDefinition(LevelDefinition source, string id, AuthoredLevelData authored)
        {
            var def = ScriptableObject.CreateInstance<LevelDefinition>();
            def.levelId = id;
            def.name = id;
            def.source = LevelDefinition.LevelSource.Authored;
            def.board.width = authored.width;
            def.board.height = authored.height;
            def.board.seed = source.board.seed;
            def.generation = source.generation;
            def.generation.arrowCoverage = CountTiles(authored) / (float)Mathf.Max(1, authored.width * authored.height);
            def.generation.maxPathLength = authored.width * authored.height;
            def.generation.validateWithGreedy = true;
            def.lose = source.lose;
            def.arrowColorMode = source.arrowColorMode;
            def.arrowColorMaskQuantizeSteps = source.arrowColorMaskQuantizeSteps;
            def.tintOnHit = source.tintOnHit;
            def.hitTint = source.hitTint;
            def.introSettings = source.introSettings;
            def.palette = source.palette;
            def.authoredLevel = authored;
            return def;
        }

        private static List<Plan> BuildPlans(List<SourceSeed> sources)
        {
            var plans = new List<Plan>();
            for (int i = 0; i < sources.Count; i++)
            {
                for (int j = i + 1; j < sources.Count; j++)
                {
                    SourceSeed a = sources[i];
                    SourceSeed b = sources[j];
                    int chains = a.Chains + b.Chains;
                    if (chains < MinChains || chains > MaxChains)
                        continue;

                    for (int gap = 0; gap <= 2; gap++)
                    {
                        AddPlanIfValid(plans, a, b, horizontal: true, gap);
                        AddPlanIfValid(plans, a, b, horizontal: false, gap);
                        AddPlanIfValid(plans, b, a, horizontal: true, gap);
                        AddPlanIfValid(plans, b, a, horizontal: false, gap);
                    }
                }
            }

            plans.Sort((a, b) =>
            {
                int c = PairVariantPriority(a).CompareTo(PairVariantPriority(b));
                if (c != 0) return c;
                c = b.Score.CompareTo(a.Score);
                if (c != 0) return c;
                c = Mathf.Abs(160 - a.Chains).CompareTo(Mathf.Abs(160 - b.Chains));
                if (c != 0) return c;
                return string.Compare(MakePairKey(a), MakePairKey(b), StringComparison.Ordinal);
            });
            return DiversifyPairClasses(DiversifyPlans(plans));
        }

        private static bool IsAllowedSourceName(string name)
        {
            return PlainSeedName.IsMatch(name) || R1FinalSeedName.IsMatch(name) || R2FinalSeedName.IsMatch(name);
        }

        private static List<Plan> DiversifyPlans(List<Plan> sortedPlans)
        {
            var horizontal = new List<Plan>();
            var vertical = new List<Plan>();
            for (int i = 0; i < sortedPlans.Count; i++)
            {
                if (sortedPlans[i].Horizontal)
                    horizontal.Add(sortedPlans[i]);
                else
                    vertical.Add(sortedPlans[i]);
            }

            var result = new List<Plan>(sortedPlans.Count);
            int h = 0;
            int v = 0;
            while (h < horizontal.Count || v < vertical.Count)
            {
                bool preferVertical = result.Count % 3 == 2;
                if (preferVertical && v < vertical.Count)
                {
                    result.Add(vertical[v++]);
                    continue;
                }

                if (!preferVertical && h < horizontal.Count)
                {
                    result.Add(horizontal[h++]);
                    continue;
                }

                if (h < horizontal.Count)
                    result.Add(horizontal[h++]);
                else if (v < vertical.Count)
                    result.Add(vertical[v++]);
            }

            return result;
        }

        private static List<Plan> DiversifyPairClasses(List<Plan> sortedPlans)
        {
            var buckets = new List<Plan>[5];
            for (int i = 0; i < buckets.Length; i++)
                buckets[i] = new List<Plan>();

            for (int i = 0; i < sortedPlans.Count; i++)
                buckets[Mathf.Clamp(PairVariantPriority(sortedPlans[i]), 0, buckets.Length - 1)].Add(sortedPlans[i]);

            int[] order = { 0, 1, 2, 3, 4 };
            int[] cursors = new int[buckets.Length];
            var result = new List<Plan>(sortedPlans.Count);
            bool added;
            do
            {
                added = false;
                for (int i = 0; i < order.Length; i++)
                {
                    int bucketIndex = order[i];
                    if (cursors[bucketIndex] >= buckets[bucketIndex].Count)
                        continue;

                    result.Add(buckets[bucketIndex][cursors[bucketIndex]++]);
                    added = true;
                }
            }
            while (added);

            return result;
        }

        private static void PromoteKnownGreedySmokeCandidates(List<Plan> plans)
        {
            plans.Sort(CompareGreedySmokePlans);

            var buckets = new List<Plan>[5];
            for (int i = 0; i < buckets.Length; i++)
                buckets[i] = new List<Plan>();

            for (int i = 0; i < plans.Count; i++)
                buckets[Mathf.Clamp(PairVariantPriority(plans[i]), 0, buckets.Length - 1)].Add(plans[i]);

            int[] order = { 0, 2, 1, 3, 4 };
            int[] cursors = new int[buckets.Length];
            plans.Clear();
            bool added;
            do
            {
                added = false;
                for (int i = 0; i < order.Length; i++)
                {
                    int bucketIndex = order[i];
                    if (cursors[bucketIndex] >= buckets[bucketIndex].Count)
                        continue;

                    plans.Add(buckets[bucketIndex][cursors[bucketIndex]++]);
                    added = true;
                }
            }
            while (added);
        }

        private static int CompareGreedySmokePlans(Plan a, Plan b)
        {
            int c = KnownGreedyPriority(a).CompareTo(KnownGreedyPriority(b));
            if (c != 0) return c;

            c = InwardPressure(a).CompareTo(InwardPressure(b));
            if (c != 0) return c;

            if (a.Gap != b.Gap)
                return a.Gap.CompareTo(b.Gap);

            c = b.Coverage.CompareTo(a.Coverage);
            if (c != 0) return c;

            return b.Score.CompareTo(a.Score);
        }

        private static int KnownGreedyPriority(Plan plan)
        {
            bool knownPair =
                IsNameMatch(plan.A.Name, "691") &&
                IsNameMatch(plan.B.Name, "946") &&
                plan.Horizontal &&
                plan.Gap == 0;

            return knownPair ? 0 : 1;
        }

        private static CandidateStats AnalyzeCandidate(Plan plan)
        {
            var stat = new CandidateStats
            {
                Plan = plan,
                PairPriority = PairVariantPriority(plan),
                InwardPressure = InwardPressure(plan),
                TopBlockedIndex = -1,
                TopBlockedOwner = -1,
                TopBlockerIndex = -1,
                TopBlockerOwner = -1
            };

            if (!TryBuildRawAuthored(plan, out var authored, out var metas, out string details))
            {
                stat.Details = details;
                stat.RiskScore = 100000f;
                return stat;
            }

            stat.StructureMismatch = SourceStructureMismatch(plan.A, plan.B);
            stat.CrossBlocks = CountCrossSourceBlocks(authored, metas, out var blockedByOther, out var blocksOther);
            for (int i = 0; i < blockedByOther.Length; i++)
            {
                if (blockedByOther[i] > 0)
                    stat.BlockedChains++;
                if (blocksOther[i] > 0)
                    stat.BlockingChains++;

                if (blockedByOther[i] > stat.BlockedByOtherMax)
                {
                    stat.BlockedByOtherMax = blockedByOther[i];
                    stat.TopBlockedIndex = i;
                    stat.TopBlockedOwner = SafeMetaOwner(metas, i);
                    stat.TopBlockedHits = blockedByOther[i];
                }

                if (blocksOther[i] > stat.BlocksOtherMax)
                {
                    stat.BlocksOtherMax = blocksOther[i];
                    stat.TopBlockerIndex = i;
                    stat.TopBlockerOwner = SafeMetaOwner(metas, i);
                    stat.TopBlockerHits = blocksOther[i];
                }
            }

            float coveragePenalty = Mathf.Max(0f, MinCoverage - plan.Coverage) * 10000f;
            float aspectPenalty = Mathf.Max(0f, plan.Aspect - 1.35f) * 60f;
            float chainPenalty = Mathf.Abs(155 - plan.Chains) * 0.35f;
            float structurePenalty = stat.StructureMismatch * 120f;
            if (stat.StructureMismatch > MaxSourceStructureMismatch)
                structurePenalty += (stat.StructureMismatch - MaxSourceStructureMismatch) * 360f;
            stat.RiskScore =
                stat.CrossBlocks * 18f +
                stat.BlockedChains * 3.5f +
                stat.BlockingChains * 5f +
                stat.BlocksOtherMax * 32f +
                stat.BlockedByOtherMax * 12f +
                stat.InwardPressure * 4f -
                plan.Gap * 26f +
                aspectPenalty +
                chainPenalty +
                structurePenalty +
                coveragePenalty -
                plan.Coverage * 80f;

            stat.Details = $"rawOk tiles={CountTiles(authored)} mismatch={stat.StructureMismatch:0.000}";
            return stat;
        }

        private static int CompareCandidateStats(CandidateStats a, CandidateStats b)
        {
            int c = a.RiskScore.CompareTo(b.RiskScore);
            if (c != 0) return c;

            c = a.PairPriority.CompareTo(b.PairPriority);
            if (c != 0) return c;

            c = b.Plan.Coverage.CompareTo(a.Plan.Coverage);
            if (c != 0) return c;

            c = Mathf.Abs(155 - a.Plan.Chains).CompareTo(Mathf.Abs(155 - b.Plan.Chains));
            if (c != 0) return c;

            return string.Compare(MakePairKey(a.Plan), MakePairKey(b.Plan), StringComparison.Ordinal);
        }

        private static int SafeMetaOwner(List<ChainMeta> metas, int index)
        {
            if (metas == null || index < 0 || index >= metas.Count)
                return -1;
            return metas[index]?.SourceIndex ?? -1;
        }

        private static string SourceTypeLabel(SourceSeed source)
        {
            return source?.SourceTier switch
            {
                0 => "R2",
                1 => "R1",
                _ => "Seed"
            };
        }

        private static bool IsNameMatch(string name, string levelNumber)
        {
            return !string.IsNullOrEmpty(name) && name.IndexOf(levelNumber, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static int GetSourceTier(string path, string name)
        {
            string lowerPath = path?.ToLowerInvariant() ?? string.Empty;
            string lowerName = name?.ToLowerInvariant() ?? string.Empty;
            if (lowerPath.Contains("r2finalcandidatepool") || lowerName.StartsWith("r2_") || lowerName.Contains("rotation"))
                return 0;
            if (R1FinalSeedName.IsMatch(name ?? string.Empty))
                return 1;
            return 2;
        }

        private static int PairVariantPriority(Plan plan)
        {
            int a = plan.A.SourceTier;
            int b = plan.B.SourceTier;
            bool aVariant = a <= 1;
            bool bVariant = b <= 1;
            bool hasR2 = a == 0 || b == 0;
            bool hasR1 = a == 1 || b == 1;

            if (hasR1 && hasR2)
                return 0;
            if (aVariant && bVariant)
                return 1;
            if (hasR2 && (a == 2 || b == 2))
                return 2;
            if ((hasR1 || hasR2) && (a == 2 || b == 2))
                return 3;
            return 4;
        }

        private static int InwardPressure(Plan plan)
        {
            return plan.Horizontal
                ? plan.A.HeadRight + plan.B.HeadLeft
                : plan.A.HeadUp + plan.B.HeadDown;
        }

        private static void AddPlanIfValid(List<Plan> plans, SourceSeed a, SourceSeed b, bool horizontal, int gap)
        {
            int width = horizontal ? a.Width + gap + b.Width : Mathf.Max(a.Width, b.Width);
            int height = horizontal ? Mathf.Max(a.Height, b.Height) : a.Height + gap + b.Height;
            int area = Mathf.Max(1, width * height);
            int gapTiles = gap * (horizontal ? height : width);
            int gapChains = gap;
            int tiles = a.Tiles + b.Tiles + gapTiles;
            int chains = a.Chains + b.Chains + gapChains;
            float coverage = tiles / (float)area;
            float aspect = Mathf.Max(width, height) / (float)Mathf.Max(1, Mathf.Min(width, height));
            if (coverage < MinCoverage || aspect > MaxAspect)
                return;

            float dimensionHarmony = horizontal
                ? 1f - Mathf.Abs(a.Height - b.Height) / (float)Mathf.Max(a.Height, b.Height)
                : 1f - Mathf.Abs(a.Width - b.Width) / (float)Mathf.Max(a.Width, b.Width);
            float chainCenter = 1f - Mathf.Abs(160 - chains) / 80f;
            float squareBias = 1f / aspect;
            int inwardPressure = horizontal ? a.HeadRight + b.HeadLeft : a.HeadUp + b.HeadDown;
            float structureMismatch = SourceStructureMismatch(a, b);
            float score = coverage * 1000f + dimensionHarmony * 180f + chainCenter * 90f + squareBias * 60f + gap * 35f - inwardPressure * 24f - structureMismatch * 220f;
            plans.Add(new Plan
            {
                A = a,
                B = b,
                Horizontal = horizontal,
                Gap = gap,
                Width = width,
                Height = height,
                Chains = chains,
                Tiles = tiles,
                Coverage = coverage,
                Aspect = aspect,
                Score = score
            });
        }

        private static bool TryBuildComposite(Plan plan, int serial, out LevelDefinition def, out string id, out string details)
        {
            def = null;
            details = string.Empty;
            id = $"composite_ab_{serial:000}_{ShortName(plan.A.Name)}_{ShortName(plan.B.Name)}_{LayoutLabel(plan).ToLowerInvariant()}";

            var authored = new AuthoredLevelData
            {
                width = plan.Width,
                height = plan.Height,
                arrows = new List<AuthoredArrowData>(plan.Chains)
            };

            var metas = new List<ChainMeta>(plan.Chains);
            Vector2Int offsetA;
            Vector2Int offsetB;
            if (plan.Horizontal)
            {
                offsetA = new Vector2Int(0, (plan.Height - plan.A.Height) / 2);
                offsetB = new Vector2Int(plan.A.Width + plan.Gap, (plan.Height - plan.B.Height) / 2);
            }
            else
            {
                offsetA = new Vector2Int((plan.Width - plan.A.Width) / 2, 0);
                offsetB = new Vector2Int((plan.Width - plan.B.Width) / 2, plan.A.Height + plan.Gap);
            }

            var occupied = new HashSet<int>(plan.Tiles);
            if (!AppendTranslated(plan.A, offsetA, authored, occupied, metas, 0, out details))
                return false;
            if (!AppendTranslated(plan.B, offsetB, authored, occupied, metas, 1, out details))
                return false;

            if (!TryRepairCompositeToGreedy(authored, metas, plan, out details))
                return false;

            def = ScriptableObject.CreateInstance<LevelDefinition>();
            def.levelId = id;
            def.name = id;
            def.source = LevelDefinition.LevelSource.Authored;
            def.board.width = authored.width;
            def.board.height = authored.height;
            def.board.seed = 0;
            def.generation.arrowCoverage = CountTiles(authored) / (float)Mathf.Max(1, authored.width * authored.height);
            def.generation.initialMovableArrowCount = 0;
            def.generation.targetDifficultyScore = plan.A.Def.generation.targetDifficultyScore + plan.B.Def.generation.targetDifficultyScore;
            def.generation.fixedGenerationSeed = 0;
            def.generation.minPathLen = Mathf.Max(2, Mathf.Min(plan.A.Def.generation.minPathLen, plan.B.Def.generation.minPathLen));
            def.generation.maxPathLength = authored.width * authored.height;
            def.generation.twistiness = (plan.A.Def.generation.twistiness + plan.B.Def.generation.twistiness) * 0.5f;
            def.generation.validateWithGreedy = true;
            def.lose.blockedLoseLimit = Mathf.Max(plan.A.Def.lose.blockedLoseLimit, plan.B.Def.lose.blockedLoseLimit);
            def.arrowColorMode = plan.A.Def.arrowColorMode;
            def.arrowColorMaskQuantizeSteps = plan.A.Def.arrowColorMaskQuantizeSteps;
            def.tintOnHit = plan.A.Def.tintOnHit;
            def.hitTint = plan.A.Def.hitTint;
            def.introSettings = plan.A.Def.introSettings;
            def.palette = plan.A.Def.palette;
            def.authoredLevel = authored;
            return true;
        }

        private static bool TryBuildRawComposite(Plan plan, int serial, out LevelDefinition def, out string id, out int crossBlocks, out string details)
        {
            def = null;
            crossBlocks = 0;
            details = string.Empty;
            id = $"composite_raw_ab_{serial:000}_{ShortName(plan.A.Name)}_{ShortName(plan.B.Name)}_{LayoutLabel(plan).ToLowerInvariant()}";

            var authored = new AuthoredLevelData
            {
                width = plan.Width,
                height = plan.Height,
                arrows = new List<AuthoredArrowData>(plan.Chains)
            };

            Vector2Int offsetA;
            Vector2Int offsetB;
            if (plan.Horizontal)
            {
                offsetA = new Vector2Int(0, (plan.Height - plan.A.Height) / 2);
                offsetB = new Vector2Int(plan.A.Width + plan.Gap, (plan.Height - plan.B.Height) / 2);
            }
            else
            {
                offsetA = new Vector2Int((plan.Width - plan.A.Width) / 2, 0);
                offsetB = new Vector2Int((plan.Width - plan.B.Width) / 2, plan.A.Height + plan.Gap);
            }

            var metas = new List<ChainMeta>(plan.Chains);
            var occupied = new HashSet<int>(plan.Tiles);
            if (!AppendTranslated(plan.A, offsetA, authored, occupied, metas, 0, out details))
                return false;
            if (!AppendTranslated(plan.B, offsetB, authored, occupied, metas, 1, out details))
                return false;

            float coverage = CountTiles(authored) / (float)Mathf.Max(1, plan.Width * plan.Height);
            if (coverage < MinCoverage)
            {
                details = $"raw coverage below minimum {coverage:0.000}";
                return false;
            }

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out _, out string buildError))
            {
                details = $"raw build failed {buildError}";
                return false;
            }

            crossBlocks = CountCrossSourceBlocks(authored, metas, out _, out _);
            def = CreateCompositeDefinition(plan, id, authored);
            return true;
        }

        private static bool TryBuildGreedyRepairComposite(Plan plan, int serial, out LevelDefinition def, out string id, out int crossBlocks, out string repair, out string details)
        {
            def = null;
            crossBlocks = 0;
            repair = string.Empty;
            details = string.Empty;
            id = $"composite_greedy_ab_{serial:000}_{ShortName(plan.A.Name)}_{ShortName(plan.B.Name)}_{LayoutLabel(plan).ToLowerInvariant()}";

            if (!TryBuildRawAuthored(plan, out var authored, out var metas, out details))
                return false;

            if (HasMeaninglessSeamBridge(authored, plan, metas, out string rawBridgeDetails))
            {
                details = $"raw {rawBridgeDetails}";
                return false;
            }

            if (!TryRepairCompositeToGreedy(authored, metas, plan, out repair))
            {
                details = repair;
                return false;
            }

            var fillCandidate = CloneAuthored(authored);
            var fillMetas = CloneMetas(metas);
            int maxFillChains = Mathf.Min(MaxParentEdgeFillChains, Mathf.Max(0, MaxChains - fillCandidate.arrows.Count));
            int filled = TryFillEdgeVisibleEmptyRuns(fillCandidate, fillMetas, maxNewChains: maxFillChains);
            if (filled < maxFillChains)
                filled += TryFillVisibleExitEmptyRuns(fillCandidate, fillMetas, maxFillChains - filled);
            if (filled > 0)
            {
                bool fillAccepted = false;
                if (AuthoredLevelBuilder.TryBuildBoard(fillCandidate, out var filledBoard, out string filledBuildError))
                {
                    var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
                    int greedyBudget = Mathf.Max(FastGreedyBudgetMin, fillCandidate.arrows.Count * FastGreedyBudgetPerChain);
                    fillAccepted = GreedyValidator.TryClearAllByGreedy(filledBoard, rules, greedyBudget, out _);
                }

                if (fillAccepted)
                {
                    authored = fillCandidate;
                    metas = fillMetas;
                    repair = $"{repair}; edgeFill={filled}";
                }
                else
                {
                    repair = $"{repair}; edgeFill=0/{filled}";
                }
            }

            Plan finalPlan = plan;
            if (TryCompactMeaninglessGap(ref authored, ref metas, plan, out Plan compactedPlan, out string compactDetails))
            {
                finalPlan = compactedPlan;
                repair = $"{repair}; {compactDetails}";
            }

            crossBlocks = CountCrossSourceBlocks(authored, metas, out _, out _);
            float coverage = CountTiles(authored) / (float)Mathf.Max(1, authored.width * authored.height);
            if (coverage < MinCoverage)
            {
                details = $"greedy repair coverage below minimum {coverage:0.000}";
                return false;
            }

            if (authored.arrows.Count < MinChains || authored.arrows.Count > MaxChains)
            {
                details = $"greedy repair chains out of range {authored.arrows.Count}";
                return false;
            }

            if (!PassCompositeQualityFilters(authored, finalPlan, metas, out string qualityDetails))
            {
                details = qualityDetails;
                return false;
            }

            def = CreateCompositeDefinition(finalPlan, id, authored);
            return true;
        }

        private static bool TryCompactMeaninglessGap(ref AuthoredLevelData authored, ref List<ChainMeta> metas, Plan plan, out Plan compactedPlan, out string details)
        {
            compactedPlan = plan;
            details = string.Empty;
            if (plan.Gap <= 0)
                return false;

            if (!TryCreateGapCompactedAuthored(authored, metas, plan, out var candidate, out var candidateMetas, out int removedChains, out details))
                return false;

            if (candidate.arrows.Count < MinChains || candidate.arrows.Count > MaxChains)
            {
                details = $"gapCompact rejected chains={candidate.arrows.Count}";
                return false;
            }

            float coverage = CountTiles(candidate) / (float)Mathf.Max(1, candidate.width * candidate.height);
            if (coverage < MinCoverage)
            {
                details = $"gapCompact rejected coverage={coverage:0.000}";
                return false;
            }

            if (!AuthoredLevelBuilder.TryBuildBoard(candidate, out var board, out string buildError))
            {
                details = $"gapCompact build failed {buildError}";
                return false;
            }

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int greedyBudget = Mathf.Max(FastGreedyBudgetMin, candidate.arrows.Count * FastGreedyBudgetPerChain);
            if (!GreedyValidator.TryClearAllByGreedy(board, rules, greedyBudget, out _))
            {
                details = $"gapCompact greedy failed budget={greedyBudget}";
                return false;
            }

            compactedPlan = CreateCompactedPlan(plan, candidate);
            if (!PassCompositeQualityFilters(candidate, compactedPlan, candidateMetas, out string qualityDetails))
            {
                details = $"gapCompact {qualityDetails}";
                return false;
            }

            authored = candidate;
            metas = candidateMetas;
            details = $"gapCompact={plan.Gap} removedChains={removedChains} coverage={coverage:0.000}";
            return true;
        }

        private static bool TryCreateGapCompactedAuthored(
            AuthoredLevelData source,
            List<ChainMeta> metas,
            Plan plan,
            out AuthoredLevelData compacted,
            out List<ChainMeta> compactedMetas,
            out int removedChains,
            out string details)
        {
            compacted = null;
            compactedMetas = null;
            removedChains = 0;
            details = string.Empty;

            int removeStart = plan.Horizontal ? plan.A.Width : plan.A.Height;
            int removeCount = plan.Gap;
            int newWidth = plan.Horizontal ? source.width - removeCount : source.width;
            int newHeight = plan.Horizontal ? source.height : source.height - removeCount;
            if (newWidth <= 0 || newHeight <= 0)
            {
                details = "gapCompact invalid size";
                return false;
            }

            compacted = new AuthoredLevelData
            {
                width = newWidth,
                height = newHeight,
                arrows = new List<AuthoredArrowData>(source.arrows?.Count ?? 0)
            };
            compactedMetas = new List<ChainMeta>(metas?.Count ?? 0);

            var occupied = new HashSet<int>();
            for (int i = 0; i < (source.arrows?.Count ?? 0); i++)
            {
                var arrow = source.arrows[i];
                var indices = arrow?.indices;
                if (indices == null || indices.Count < 2)
                    continue;

                int owner = SafeMetaOwner(metas, i);
                bool touchesRemovedLane = false;
                var translated = new List<int>(indices.Count);
                for (int j = 0; j < indices.Count; j++)
                {
                    int index = indices[j];
                    int x = index % source.width;
                    int y = index / source.width;
                    int lane = plan.Horizontal ? x : y;
                    if (lane >= removeStart && lane < removeStart + removeCount)
                    {
                        touchesRemovedLane = true;
                        continue;
                    }

                    int tx = plan.Horizontal && x >= removeStart + removeCount ? x - removeCount : x;
                    int ty = !plan.Horizontal && y >= removeStart + removeCount ? y - removeCount : y;
                    if ((uint)tx >= (uint)newWidth || (uint)ty >= (uint)newHeight)
                    {
                        details = "gapCompact translated out of bounds";
                        return false;
                    }

                    translated.Add(tx + ty * newWidth);
                }

                if (touchesRemovedLane)
                {
                    if (owner == 0 || owner == 1)
                    {
                        details = "gapCompact would remove source chain";
                        return false;
                    }

                    removedChains++;
                    continue;
                }

                if (translated.Count < 2)
                    continue;

                for (int j = 0; j < translated.Count; j++)
                {
                    if (!occupied.Add(translated[j]))
                    {
                        details = "gapCompact collision after shift";
                        return false;
                    }
                }

                compacted.arrows.Add(new AuthoredArrowData
                {
                    indices = translated,
                    colorIndex = arrow.colorIndex
                });
                compactedMetas.Add(new ChainMeta
                {
                    SourceIndex = owner,
                    TileCount = translated.Count
                });
            }

            if (removedChains <= 0)
            {
                details = "gapCompact no generated gap chain removed";
                return false;
            }

            return true;
        }

        private static Plan CreateCompactedPlan(Plan source, AuthoredLevelData authored)
        {
            int tiles = CountTiles(authored);
            int area = Mathf.Max(1, authored.width * authored.height);
            return new Plan
            {
                A = source.A,
                B = source.B,
                Horizontal = source.Horizontal,
                Gap = 0,
                Width = authored.width,
                Height = authored.height,
                Chains = authored.arrows?.Count ?? 0,
                Tiles = tiles,
                Coverage = tiles / (float)area,
                Aspect = Mathf.Max(authored.width, authored.height) / (float)Mathf.Max(1, Mathf.Min(authored.width, authored.height)),
                Score = source.Score
            };
        }

        private static bool PassCompositeQualityFilters(AuthoredLevelData authored, Plan plan, List<ChainMeta> metas, out string details)
        {
            int outerShort = CountOuterShortChains(authored);
            if (outerShort > MaxOuterShortChains)
            {
                details = $"quality outer short chains {outerShort}>{MaxOuterShortChains}";
                return false;
            }

            int longestSeamStraight = LongestSeamStraightRun(authored, plan);
            int seamLimit = Mathf.Max(2, (plan.Horizontal ? authored.height : authored.width) * MaxSeamStraightRunRatioPercent / 100);
            if (longestSeamStraight > seamLimit)
            {
                details = $"quality seam straight run {longestSeamStraight}>{seamLimit}";
                return false;
            }

            float seamEmpty = CalculateSeamEmptyRatio(authored, plan, out int seamEmptyCells, out int seamCells);
            if (seamCells > 0 && seamEmpty > MaxSeamEmptyRatio)
            {
                details = $"quality seam empty {seamEmpty:0.000}>{MaxSeamEmptyRatio:0.000} empty={seamEmptyCells}/{seamCells}";
                return false;
            }

            if (HasMeaninglessSeamBridge(authored, plan, metas, out string bridgeDetails))
            {
                details = bridgeDetails;
                return false;
            }

            details = $"quality ok outerShort={outerShort} seamStraight={longestSeamStraight} seamEmpty={seamEmpty:0.000}";
            return true;
        }

        private static int CountOuterShortChains(AuthoredLevelData authored)
        {
            if (authored?.arrows == null)
                return 0;

            int count = 0;
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var arrow = authored.arrows[i];
                int length = arrow?.indices?.Count ?? 0;
                if (length > 0 && length <= 3 && ChainTouchesEdge(authored, arrow))
                    count++;
            }

            return count;
        }

        private static int LongestSeamStraightRun(AuthoredLevelData authored, Plan plan)
        {
            if (authored?.arrows == null || plan == null || plan.Gap <= 0)
                return 0;

            int longest = 0;
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var indices = authored.arrows[i]?.indices;
                if (indices == null || indices.Count < 2)
                    continue;
                if (!ChainLivesInGap(authored, plan, indices))
                    continue;

                longest = Mathf.Max(longest, LongestStraightRun(indices, authored.width));
            }

            return longest;
        }

        private static bool ChainLivesInGap(AuthoredLevelData authored, Plan plan, List<int> indices)
        {
            if (indices == null || indices.Count == 0)
                return false;

            for (int i = 0; i < indices.Count; i++)
            {
                int index = indices[i];
                int x = index % authored.width;
                int y = index / authored.width;
                bool inGap = plan.Horizontal
                    ? x >= plan.A.Width && x < plan.A.Width + plan.Gap
                    : y >= plan.A.Height && y < plan.A.Height + plan.Gap;
                if (!inGap)
                    return false;
            }

            return true;
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

        private static float CalculateSeamEmptyRatio(AuthoredLevelData authored, Plan plan, out int emptyCells, out int totalCells)
        {
            emptyCells = 0;
            totalCells = 0;
            if (authored == null || plan == null)
                return 0f;

            Vector2Int offsetA;
            Vector2Int offsetB;
            if (plan.Horizontal)
            {
                offsetA = new Vector2Int(0, (plan.Height - plan.A.Height) / 2);
                offsetB = new Vector2Int(plan.A.Width + plan.Gap, (plan.Height - plan.B.Height) / 2);
            }
            else
            {
                offsetA = new Vector2Int((plan.Width - plan.A.Width) / 2, 0);
                offsetB = new Vector2Int((plan.Width - plan.B.Width) / 2, plan.A.Height + plan.Gap);
            }

            int minX;
            int maxX;
            int minY;
            int maxY;
            if (plan.Horizontal)
            {
                minX = Mathf.Max(0, plan.A.Width - 2);
                maxX = Mathf.Min(authored.width - 1, plan.A.Width + plan.Gap + 1);
                minY = Mathf.Max(offsetA.y, offsetB.y);
                maxY = Mathf.Min(offsetA.y + plan.A.Height, offsetB.y + plan.B.Height) - 1;
            }
            else
            {
                minX = Mathf.Max(offsetA.x, offsetB.x);
                maxX = Mathf.Min(offsetA.x + plan.A.Width, offsetB.x + plan.B.Width) - 1;
                minY = Mathf.Max(0, plan.A.Height - 2);
                maxY = Mathf.Min(authored.height - 1, plan.A.Height + plan.Gap + 1);
            }

            if (minX > maxX || minY > maxY)
                return 0f;

            var occupied = BuildOccupiedSet(authored);
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    totalCells++;
                    if (!occupied.Contains(x + y * authored.width))
                        emptyCells++;
                }
            }

            return emptyCells / (float)Mathf.Max(1, totalCells);
        }

        private static bool HasMeaninglessSeamBridge(AuthoredLevelData authored, Plan plan, List<ChainMeta> metas, out string details)
        {
            details = string.Empty;
            if (authored?.arrows == null || plan == null)
                return false;

            GetSeamBand(authored, plan, out int minX, out int maxX, out int minY, out int maxY);
            if (minX > maxX || minY > maxY)
                return false;

            bool verticalBridge = plan.Horizontal;
            if (TryCountMeaninglessBridgeChains(
                    authored,
                    metas,
                    verticalBridge,
                    minX,
                    maxX,
                    minY,
                    maxY,
                    MinMeaninglessBridgeRunRatioPercent,
                    minBandSharePercent: 40,
                    out int narrowChains,
                    out int narrowGenerated,
                    out int narrowMaxRun,
                    out int narrowThreshold) &&
                narrowChains > MaxMeaninglessBridgeChains)
            {
                details = $"quality meaningless bridge chains={narrowChains}>{MaxMeaninglessBridgeChains} generated={narrowGenerated} maxRun={narrowMaxRun} threshold={narrowThreshold}";
                return true;
            }

            ExpandSeamBand(authored, plan, SeamBridgeCorridorPadding, ref minX, ref maxX, ref minY, ref maxY);
            if (TryCountMeaninglessBridgeChains(
                    authored,
                    metas,
                    verticalBridge,
                    minX,
                    maxX,
                    minY,
                    maxY,
                    MinMeaninglessBridgeRunRatioPercent,
                    minBandSharePercent: 30,
                    out int corridorChains,
                    out int corridorGenerated,
                    out int corridorMaxRun,
                    out int corridorThreshold) &&
                corridorChains > MaxMeaninglessBridgeCorridorChains)
            {
                details = $"quality meaningless bridge corridor chains={corridorChains}>{MaxMeaninglessBridgeCorridorChains} generated={corridorGenerated} maxRun={corridorMaxRun} threshold={corridorThreshold}";
                return true;
            }

            if (HasDominantSeamChannel(
                    authored,
                    verticalBridge,
                    minX,
                    maxX,
                    minY,
                    maxY,
                    out int dominantChains,
                    out int interlockChains,
                    out int dominantThreshold,
                    out int dominantMaxRun))
            {
                details = $"quality dominant seam channel chains={dominantChains}>{dominantThreshold} interlocks={interlockChains}<={MaxDominantSeamChannelInterlocks} maxRun={dominantMaxRun}";
                return true;
            }

            return false;
        }

        private static bool HasDominantSeamChannel(
            AuthoredLevelData authored,
            bool verticalBridge,
            int minX,
            int maxX,
            int minY,
            int maxY,
            out int dominantChains,
            out int interlockChains,
            out int dominantThreshold,
            out int maxRun)
        {
            dominantChains = 0;
            interlockChains = 0;
            maxRun = 0;

            int span = verticalBridge ? maxY - minY + 1 : maxX - minX + 1;
            dominantThreshold = Mathf.Max(6, span * DominantSeamChannelRunRatioPercent / 100);
            if (authored?.arrows == null || span <= 0)
                return false;

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var indices = authored.arrows[i]?.indices;
                if (indices == null || indices.Count < 2)
                    continue;

                int bandCells = CountCellsInRect(indices, authored.width, minX, maxX, minY, maxY);
                if (bandCells < 3)
                    continue;

                int alongRun = LongestDirectionalRunInRect(indices, authored.width, minX, maxX, minY, maxY, verticalBridge);
                int crossRun = LongestDirectionalRunInRect(indices, authored.width, minX, maxX, minY, maxY, !verticalBridge);
                maxRun = Mathf.Max(maxRun, alongRun);

                if (crossRun >= MinSeamChannelInterlockRun)
                    interlockChains++;

                if (alongRun >= MinOuterFillRunLength && alongRun >= Mathf.Max(MinOuterFillRunLength, crossRun * 2))
                    dominantChains++;
            }

            return dominantChains > dominantThreshold && interlockChains <= MaxDominantSeamChannelInterlocks;
        }

        private static bool TryCountMeaninglessBridgeChains(
            AuthoredLevelData authored,
            List<ChainMeta> metas,
            bool verticalBridge,
            int minX,
            int maxX,
            int minY,
            int maxY,
            int runRatioPercent,
            int minBandSharePercent,
            out int longBridgeChains,
            out int generatedBridgeChains,
            out int maxRun,
            out int runThreshold)
        {
            longBridgeChains = 0;
            generatedBridgeChains = 0;
            maxRun = 0;
            int span = verticalBridge ? maxY - minY + 1 : maxX - minX + 1;
            runThreshold = Mathf.Max(MinOuterFillRunLength, span * runRatioPercent / 100);
            if (span <= 0)
                return false;

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var indices = authored.arrows[i]?.indices;
                if (indices == null || indices.Count < 2)
                    continue;

                int bandCells = CountCellsInRect(indices, authored.width, minX, maxX, minY, maxY);
                if (bandCells < Mathf.Max(3, indices.Count * minBandSharePercent / 100))
                    continue;

                int run = LongestDirectionalRunInRect(indices, authored.width, minX, maxX, minY, maxY, verticalBridge);
                maxRun = Mathf.Max(maxRun, run);
                if (run < runThreshold)
                    continue;

                longBridgeChains++;
                if (SafeMetaOwner(metas, i) >= 2)
                    generatedBridgeChains++;
            }

            return longBridgeChains > 0;
        }

        private static void GetSeamBand(AuthoredLevelData authored, Plan plan, out int minX, out int maxX, out int minY, out int maxY)
        {
            if (plan.Horizontal)
            {
                int seamStart = Mathf.Clamp(plan.A.Width - 1, 0, authored.width - 1);
                int seamEnd = Mathf.Clamp(plan.A.Width + Mathf.Max(0, plan.Gap), 0, authored.width - 1);
                minX = Mathf.Max(0, Mathf.Min(seamStart, seamEnd) - 1);
                maxX = Mathf.Min(authored.width - 1, Mathf.Max(seamStart, seamEnd) + 1);
                minY = 0;
                maxY = authored.height - 1;
            }
            else
            {
                int seamStart = Mathf.Clamp(plan.A.Height - 1, 0, authored.height - 1);
                int seamEnd = Mathf.Clamp(plan.A.Height + Mathf.Max(0, plan.Gap), 0, authored.height - 1);
                minX = 0;
                maxX = authored.width - 1;
                minY = Mathf.Max(0, Mathf.Min(seamStart, seamEnd) - 1);
                maxY = Mathf.Min(authored.height - 1, Mathf.Max(seamStart, seamEnd) + 1);
            }
        }

        private static void ExpandSeamBand(AuthoredLevelData authored, Plan plan, int padding, ref int minX, ref int maxX, ref int minY, ref int maxY)
        {
            if (plan.Horizontal)
            {
                minX = Mathf.Max(0, minX - padding);
                maxX = Mathf.Min(authored.width - 1, maxX + padding);
            }
            else
            {
                minY = Mathf.Max(0, minY - padding);
                maxY = Mathf.Min(authored.height - 1, maxY + padding);
            }
        }

        private static int CountCellsInRect(List<int> indices, int width, int minX, int maxX, int minY, int maxY)
        {
            int count = 0;
            for (int i = 0; i < indices.Count; i++)
            {
                int x = indices[i] % width;
                int y = indices[i] / width;
                if (x >= minX && x <= maxX && y >= minY && y <= maxY)
                    count++;
            }

            return count;
        }

        private static int LongestDirectionalRunInRect(List<int> indices, int width, int minX, int maxX, int minY, int maxY, bool vertical)
        {
            int best = 0;
            int current = 0;
            for (int i = 0; i < indices.Count; i++)
            {
                int x = indices[i] % width;
                int y = indices[i] / width;
                bool inRect = x >= minX && x <= maxX && y >= minY && y <= maxY;
                if (!inRect)
                {
                    current = 0;
                    continue;
                }

                if (current == 0)
                {
                    current = 1;
                }
                else
                {
                    int prev = indices[i - 1];
                    int px = prev % width;
                    int py = prev / width;
                    bool directional = vertical ? x == px && Mathf.Abs(y - py) == 1 : y == py && Mathf.Abs(x - px) == 1;
                    current = directional ? current + 1 : 1;
                }

                best = Mathf.Max(best, current);
            }

            return best;
        }

        private static bool TryBuildRawAuthored(Plan plan, out AuthoredLevelData authored, out List<ChainMeta> metas, out string details)
        {
            details = string.Empty;
            authored = new AuthoredLevelData
            {
                width = plan.Width,
                height = plan.Height,
                arrows = new List<AuthoredArrowData>(plan.Chains)
            };
            metas = new List<ChainMeta>(plan.Chains);

            Vector2Int offsetA;
            Vector2Int offsetB;
            if (plan.Horizontal)
            {
                offsetA = new Vector2Int(0, (plan.Height - plan.A.Height) / 2);
                offsetB = new Vector2Int(plan.A.Width + plan.Gap, (plan.Height - plan.B.Height) / 2);
            }
            else
            {
                offsetA = new Vector2Int((plan.Width - plan.A.Width) / 2, 0);
                offsetB = new Vector2Int((plan.Width - plan.B.Width) / 2, plan.A.Height + plan.Gap);
            }

            var occupied = new HashSet<int>(plan.Tiles);
            if (!AppendTranslated(plan.A, offsetA, authored, occupied, metas, 0, out details))
                return false;
            if (!AppendTranslated(plan.B, offsetB, authored, occupied, metas, 1, out details))
                return false;
            FillSeamGapIfNeeded(plan, authored, occupied, metas);

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out _, out string buildError))
            {
                details = $"raw authored build failed {buildError}";
                return false;
            }

            return true;
        }

        private static void FillSeamGapIfNeeded(Plan plan, AuthoredLevelData authored, HashSet<int> occupied, List<ChainMeta> metas)
        {
            if (plan.Gap <= 0)
                return;

            if (plan.Horizontal)
            {
                int startX = plan.A.Width;
                for (int gx = 0; gx < plan.Gap; gx++)
                {
                    AddSegmentedGapColumn(authored, metas, occupied, startX + gx, gx);
                }
            }
            else
            {
                int startY = plan.A.Height;
                for (int gy = 0; gy < plan.Gap; gy++)
                {
                    AddSegmentedGapRow(authored, metas, occupied, startY + gy, gy);
                }
            }
        }

        private static void AddSegmentedGapColumn(AuthoredLevelData authored, List<ChainMeta> metas, HashSet<int> occupied, int x, int phase)
        {
            int segmentLength = GetSeamSegmentLength(authored.height);
            int segmentIndex = 0;
            for (int y = 0; y < authored.height; y += segmentLength)
            {
                int len = Mathf.Min(segmentLength, authored.height - y);
                var indices = new List<int>(len);
                bool forward = ((segmentIndex + phase) & 1) == 0;
                if (forward)
                {
                    for (int i = 0; i < len; i++)
                        AddGapIndex(x, y + i, authored, occupied, indices);
                }
                else
                {
                    for (int i = len - 1; i >= 0; i--)
                        AddGapIndex(x, y + i, authored, occupied, indices);
                }

                AddGapArrow(authored, metas, indices);
                segmentIndex++;
            }
        }

        private static void AddSegmentedGapRow(AuthoredLevelData authored, List<ChainMeta> metas, HashSet<int> occupied, int y, int phase)
        {
            int segmentLength = GetSeamSegmentLength(authored.width);
            int segmentIndex = 0;
            for (int x = 0; x < authored.width; x += segmentLength)
            {
                int len = Mathf.Min(segmentLength, authored.width - x);
                var indices = new List<int>(len);
                bool forward = ((segmentIndex + phase) & 1) == 0;
                if (forward)
                {
                    for (int i = 0; i < len; i++)
                        AddGapIndex(x + i, y, authored, occupied, indices);
                }
                else
                {
                    for (int i = len - 1; i >= 0; i--)
                        AddGapIndex(x + i, y, authored, occupied, indices);
                }

                AddGapArrow(authored, metas, indices);
                segmentIndex++;
            }
        }

        private static int GetSeamSegmentLength(int totalLength)
        {
            if (totalLength <= 0)
                return SeamSegmentLength;

            int seamLimit = Mathf.Max(2, totalLength * MaxSeamStraightRunRatioPercent / 100);
            int halfLength = Mathf.CeilToInt(totalLength / 2f);
            return Mathf.Clamp(halfLength, MinOuterFillRunLength, seamLimit);
        }

        private static void AddGapIndex(int x, int y, AuthoredLevelData authored, HashSet<int> occupied, List<int> indices)
        {
            if ((uint)x >= (uint)authored.width || (uint)y >= (uint)authored.height)
                return;

            int index = x + y * authored.width;
            if (!occupied.Add(index))
                return;

            indices.Add(index);
        }

        private static void AddGapArrow(AuthoredLevelData authored, List<ChainMeta> metas, List<int> indices)
        {
            if (indices.Count < 2)
                return;

            authored.arrows.Add(new AuthoredArrowData
            {
                indices = indices,
                colorIndex = authored.arrows.Count % 16
            });
            metas.Add(new ChainMeta
            {
                SourceIndex = 2,
                TileCount = indices.Count
            });
        }

        private static AuthoredLevelData CloneAuthored(AuthoredLevelData source)
        {
            var clone = new AuthoredLevelData
            {
                width = source.width,
                height = source.height,
                arrows = new List<AuthoredArrowData>(source.arrows?.Count ?? 0)
            };

            if (source.arrows == null)
                return clone;

            for (int i = 0; i < source.arrows.Count; i++)
            {
                var arrow = source.arrows[i];
                clone.arrows.Add(new AuthoredArrowData
                {
                    indices = arrow?.indices != null ? new List<int>(arrow.indices) : new List<int>(),
                    colorIndex = arrow?.colorIndex ?? 0
                });
            }

            return clone;
        }

        private static List<ChainMeta> CloneMetas(List<ChainMeta> source)
        {
            var clone = new List<ChainMeta>(source?.Count ?? 0);
            if (source == null)
                return clone;

            for (int i = 0; i < source.Count; i++)
            {
                var meta = source[i];
                clone.Add(new ChainMeta
                {
                    SourceIndex = meta?.SourceIndex ?? -1,
                    TileCount = meta?.TileCount ?? 0
                });
            }

            return clone;
        }

        private static int TryFillEdgeVisibleEmptyRuns(
            AuthoredLevelData authored,
            List<ChainMeta> metas,
            int maxNewChains,
            int maxRunLength = 10,
            int minRunLength = MinOuterFillRunLength)
        {
            if (authored == null || maxNewChains <= 0)
                return 0;

            int filled = 0;
            var occupied = BuildOccupiedSet(authored);

            for (int y = 0; y < authored.height && filled < maxNewChains; y++)
            {
                filled += TryAddEdgeRun(authored, metas, occupied, 0, y, 1, 0, maxRunLength, minRunLength, maxNewChains - filled);
                if (filled >= maxNewChains)
                    break;
                filled += TryAddEdgeRun(authored, metas, occupied, authored.width - 1, y, -1, 0, maxRunLength, minRunLength, maxNewChains - filled);
            }

            for (int x = 0; x < authored.width && filled < maxNewChains; x++)
            {
                filled += TryAddEdgeRun(authored, metas, occupied, x, 0, 0, 1, maxRunLength, minRunLength, maxNewChains - filled);
                if (filled >= maxNewChains)
                    break;
                filled += TryAddEdgeRun(authored, metas, occupied, x, authored.height - 1, 0, -1, maxRunLength, minRunLength, maxNewChains - filled);
            }

            return filled;
        }

        private static int TryFillVisibleExitEmptyRuns(
            AuthoredLevelData authored,
            List<ChainMeta> metas,
            int maxNewChains,
            int maxRunLength = 10,
            int minRunLength = MinOuterFillRunLength)
        {
            if (authored == null || maxNewChains <= 0)
                return 0;

            int filled = 0;
            var occupied = BuildOccupiedSet(authored);
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            for (int y = 0; y < authored.height && filled < maxNewChains; y++)
            {
                for (int x = 0; x < authored.width && filled < maxNewChains; x++)
                {
                    int index = x + y * authored.width;
                    if (occupied.Contains(index))
                        continue;

                    for (int d = 0; d < dx.Length && filled < maxNewChains; d++)
                    {
                        if (!HasClearRayToEdge(authored, occupied, x, y, dx[d], dy[d]))
                            continue;

                        if (TryAddVisibleExitRun(authored, metas, occupied, x, y, -dx[d], -dy[d], maxRunLength, minRunLength))
                            filled++;
                    }
                }
            }

            return filled;
        }

        private static int TryFillOuterSnakeGaps(
            AuthoredLevelData authored,
            List<ChainMeta> metas,
            int maxNewChains,
            int maxLength,
            int minLength,
            int bandDepth)
        {
            if (authored == null || maxNewChains <= 0)
                return 0;

            int filled = 0;
            var occupied = BuildOccupiedSet(authored);

            for (int y = 0; y < authored.height && filled < maxNewChains; y++)
            {
                if (TryAddOuterSnakeRun(authored, metas, occupied, 0, y, 1, 0, maxLength, minLength, bandDepth))
                    filled++;
                if (filled >= maxNewChains)
                    break;
                if (TryAddOuterSnakeRun(authored, metas, occupied, authored.width - 1, y, -1, 0, maxLength, minLength, bandDepth))
                    filled++;
            }

            for (int x = 0; x < authored.width && filled < maxNewChains; x++)
            {
                if (TryAddOuterSnakeRun(authored, metas, occupied, x, 0, 0, 1, maxLength, minLength, bandDepth))
                    filled++;
                if (filled >= maxNewChains)
                    break;
                if (TryAddOuterSnakeRun(authored, metas, occupied, x, authored.height - 1, 0, -1, maxLength, minLength, bandDepth))
                    filled++;
            }

            return filled;
        }

        private static bool TryAddOuterSnakeRun(
            AuthoredLevelData authored,
            List<ChainMeta> metas,
            HashSet<int> occupied,
            int headX,
            int headY,
            int inwardStepX,
            int inwardStepY,
            int maxLength,
            int minLength,
            int bandDepth)
        {
            if ((uint)headX >= (uint)authored.width || (uint)headY >= (uint)authored.height)
                return false;

            int neckX = headX + inwardStepX;
            int neckY = headY + inwardStepY;
            if ((uint)neckX >= (uint)authored.width || (uint)neckY >= (uint)authored.height)
                return false;

            int headIndex = headX + headY * authored.width;
            int neckIndex = neckX + neckY * authored.width;
            if (occupied.Contains(headIndex) || occupied.Contains(neckIndex))
                return false;

            var indices = new List<int>(maxLength) { headIndex, neckIndex };
            var path = new HashSet<int> { headIndex, neckIndex };
            int currentX = neckX;
            int currentY = neckY;

            while (indices.Count < maxLength)
            {
                if (!TryChooseOuterSnakeNext(
                        authored,
                        occupied,
                        path,
                        currentX,
                        currentY,
                        inwardStepX,
                        inwardStepY,
                        bandDepth,
                        out int nextX,
                        out int nextY))
                {
                    break;
                }

                int nextIndex = nextX + nextY * authored.width;
                indices.Add(nextIndex);
                path.Add(nextIndex);
                currentX = nextX;
                currentY = nextY;
            }

            if (indices.Count < minLength)
                return false;

            for (int i = 0; i < indices.Count; i++)
                occupied.Add(indices[i]);

            authored.arrows.Add(new AuthoredArrowData
            {
                indices = indices,
                colorIndex = authored.arrows.Count % 16
            });
            metas.Add(new ChainMeta
            {
                SourceIndex = 3,
                TileCount = indices.Count
            });
            return true;
        }

        private static bool TryChooseOuterSnakeNext(
            AuthoredLevelData authored,
            HashSet<int> occupied,
            HashSet<int> path,
            int currentX,
            int currentY,
            int inwardStepX,
            int inwardStepY,
            int bandDepth,
            out int nextX,
            out int nextY)
        {
            nextX = 0;
            nextY = 0;
            int bestScore = int.MinValue;

            int[] dx =
            {
                RotateLeftX(inwardStepX, inwardStepY),
                RotateRightX(inwardStepX, inwardStepY),
                inwardStepX,
                -inwardStepX
            };
            int[] dy =
            {
                RotateLeftY(inwardStepX, inwardStepY),
                RotateRightY(inwardStepX, inwardStepY),
                inwardStepY,
                -inwardStepY
            };

            for (int i = 0; i < dx.Length; i++)
            {
                int x = currentX + dx[i];
                int y = currentY + dy[i];
                if ((uint)x >= (uint)authored.width || (uint)y >= (uint)authored.height)
                    continue;
                if (!IsOuterBandCell(authored.width, authored.height, x, y, bandDepth))
                    continue;

                int index = x + y * authored.width;
                if (occupied.Contains(index) || path.Contains(index))
                    continue;

                int distance = DistanceToNearestEdge(authored, x, y);
                int emptyNeighbors = CountEmptyNeighborsInOuterBand(authored, occupied, path, x, y, bandDepth);
                int score = emptyNeighbors * 8 - distance * 2;
                if (i < 2)
                    score += 8;
                if (i == 2)
                    score += 3;

                if (score <= bestScore)
                    continue;

                bestScore = score;
                nextX = x;
                nextY = y;
            }

            return bestScore > int.MinValue;
        }

        private static int TryFillOuterShortEdgeGaps(
            AuthoredLevelData authored,
            List<ChainMeta> metas,
            int maxNewChains,
            int maxLength,
            int minLength,
            int clusterRadius,
            int maxNearbyShortChains)
        {
            if (authored == null || maxNewChains <= 0)
                return 0;

            int filled = 0;
            var occupied = BuildOccupiedSet(authored);

            for (int y = 0; y < authored.height && filled < maxNewChains; y++)
            {
                filled += TryAddShortEdgeRun(authored, metas, occupied, 0, y, 1, 0, maxLength, minLength, clusterRadius, maxNearbyShortChains, maxNewChains - filled);
                if (filled >= maxNewChains)
                    break;
                filled += TryAddShortEdgeRun(authored, metas, occupied, authored.width - 1, y, -1, 0, maxLength, minLength, clusterRadius, maxNearbyShortChains, maxNewChains - filled);
            }

            for (int x = 0; x < authored.width && filled < maxNewChains; x++)
            {
                filled += TryAddShortEdgeRun(authored, metas, occupied, x, 0, 0, 1, maxLength, minLength, clusterRadius, maxNearbyShortChains, maxNewChains - filled);
                if (filled >= maxNewChains)
                    break;
                filled += TryAddShortEdgeRun(authored, metas, occupied, x, authored.height - 1, 0, -1, maxLength, minLength, clusterRadius, maxNearbyShortChains, maxNewChains - filled);
            }

            return filled;
        }

        private static int TryAddShortEdgeRun(
            AuthoredLevelData authored,
            List<ChainMeta> metas,
            HashSet<int> occupied,
            int startX,
            int startY,
            int stepX,
            int stepY,
            int maxLength,
            int minLength,
            int clusterRadius,
            int maxNearbyShortChains,
            int remainingChains)
        {
            if (remainingChains <= 0)
                return 0;

            var indices = BuildStraightEmptyRun(authored, occupied, startX, startY, stepX, stepY, maxLength);
            if (indices.Count < minLength || indices.Count > 3)
                return 0;
            if (CountShortOuterChainsNear(authored, indices, clusterRadius, maxNearbyShortChains + 1) > maxNearbyShortChains)
                return 0;

            for (int i = 0; i < indices.Count; i++)
                occupied.Add(indices[i]);

            authored.arrows.Add(new AuthoredArrowData
            {
                indices = indices,
                colorIndex = authored.arrows.Count % 16
            });
            metas.Add(new ChainMeta
            {
                SourceIndex = 3,
                TileCount = indices.Count
            });
            return 1;
        }

        private static int TryExtendOuterEndpointGaps(AuthoredLevelData authored, int maxExtensions)
        {
            if (authored?.arrows == null || maxExtensions <= 0)
                return 0;

            int extended = 0;
            var occupied = BuildOccupiedSet(authored);
            for (int y = 0; y < authored.height && extended < maxExtensions; y++)
            {
                extended += TryExtendOuterEndpointAt(authored, occupied, 0, y, maxExtensions - extended);
                if (extended >= maxExtensions)
                    break;
                extended += TryExtendOuterEndpointAt(authored, occupied, authored.width - 1, y, maxExtensions - extended);
            }

            for (int x = 0; x < authored.width && extended < maxExtensions; x++)
            {
                extended += TryExtendOuterEndpointAt(authored, occupied, x, 0, maxExtensions - extended);
                if (extended >= maxExtensions)
                    break;
                extended += TryExtendOuterEndpointAt(authored, occupied, x, authored.height - 1, maxExtensions - extended);
            }

            return extended;
        }

        private static int TryExtendOuterEndpointAt(AuthoredLevelData authored, HashSet<int> occupied, int edgeX, int edgeY, int remainingExtensions)
        {
            if (remainingExtensions <= 0 || (uint)edgeX >= (uint)authored.width || (uint)edgeY >= (uint)authored.height)
                return 0;

            int edgeIndex = edgeX + edgeY * authored.width;
            if (occupied.Contains(edgeIndex))
                return 0;

            int[] stepX = new int[2];
            int[] stepY = new int[2];
            int stepCount = GetOuterInwardSteps(authored, edgeX, edgeY, stepX, stepY);
            for (int i = 0; i < stepCount; i++)
            {
                int neckX = edgeX + stepX[i];
                int neckY = edgeY + stepY[i];
                if ((uint)neckX >= (uint)authored.width || (uint)neckY >= (uint)authored.height)
                    continue;

                int neckIndex = neckX + neckY * authored.width;
                if (!TryFindChainPosition(authored, neckIndex, out int chainIndex, out int position))
                    continue;

                var indices = authored.arrows[chainIndex]?.indices;
                if (indices == null || indices.Count < 2)
                    continue;

                if (position == 0)
                {
                    indices.Insert(0, edgeIndex);
                    occupied.Add(edgeIndex);
                    return 1;
                }

                if (position == indices.Count - 1)
                {
                    indices.Reverse();
                    indices.Insert(0, edgeIndex);
                    occupied.Add(edgeIndex);
                    return 1;
                }
            }

            return 0;
        }

        private static int TrySplitOuterNeighborChains(
            AuthoredLevelData authored,
            List<ChainMeta> metas,
            int maxSplits,
            int minDetachedLength,
            int maxDetachedLength,
            int minRemainingLength)
        {
            if (authored?.arrows == null || maxSplits <= 0)
                return 0;

            int splits = 0;
            var occupied = BuildOccupiedSet(authored);
            for (int y = 0; y < authored.height && splits < maxSplits; y++)
            {
                splits += TrySplitOuterNeighborAt(authored, metas, occupied, 0, y, minDetachedLength, maxDetachedLength, minRemainingLength, maxSplits - splits);
                if (splits >= maxSplits)
                    break;
                splits += TrySplitOuterNeighborAt(authored, metas, occupied, authored.width - 1, y, minDetachedLength, maxDetachedLength, minRemainingLength, maxSplits - splits);
            }

            for (int x = 0; x < authored.width && splits < maxSplits; x++)
            {
                splits += TrySplitOuterNeighborAt(authored, metas, occupied, x, 0, minDetachedLength, maxDetachedLength, minRemainingLength, maxSplits - splits);
                if (splits >= maxSplits)
                    break;
                splits += TrySplitOuterNeighborAt(authored, metas, occupied, x, authored.height - 1, minDetachedLength, maxDetachedLength, minRemainingLength, maxSplits - splits);
            }

            return splits;
        }

        private static int TrySplitOuterNeighborAt(
            AuthoredLevelData authored,
            List<ChainMeta> metas,
            HashSet<int> occupied,
            int edgeX,
            int edgeY,
            int minDetachedLength,
            int maxDetachedLength,
            int minRemainingLength,
            int remainingSplits)
        {
            if (remainingSplits <= 0 || (uint)edgeX >= (uint)authored.width || (uint)edgeY >= (uint)authored.height)
                return 0;

            int edgeIndex = edgeX + edgeY * authored.width;
            if (occupied.Contains(edgeIndex))
                return 0;

            int[] stepX = new int[2];
            int[] stepY = new int[2];
            int stepCount = GetOuterInwardSteps(authored, edgeX, edgeY, stepX, stepY);
            for (int i = 0; i < stepCount; i++)
            {
                int neckX = edgeX + stepX[i];
                int neckY = edgeY + stepY[i];
                if ((uint)neckX >= (uint)authored.width || (uint)neckY >= (uint)authored.height)
                    continue;

                int neckIndex = neckX + neckY * authored.width;
                if (!TryFindChainPosition(authored, neckIndex, out int chainIndex, out int position))
                    continue;

                var arrow = authored.arrows[chainIndex];
                var indices = arrow?.indices;
                if (indices == null || indices.Count < minRemainingLength + minDetachedLength)
                    continue;
                if (position <= 0 || position >= indices.Count - 1)
                    continue;

                if (!TryBuildOuterSplit(indices, edgeIndex, position, minDetachedLength, maxDetachedLength, minRemainingLength, out var remaining, out var detached))
                    continue;

                arrow.indices = remaining;
                authored.arrows.Add(new AuthoredArrowData
                {
                    indices = detached,
                    colorIndex = (arrow.colorIndex + 5 + authored.arrows.Count) % 16
                });
                metas.Add(new ChainMeta
                {
                    SourceIndex = 3,
                    TileCount = detached.Count
                });
                occupied.Add(edgeIndex);
                return 1;
            }

            return 0;
        }

        private static bool TryBuildOuterSplit(
            List<int> indices,
            int edgeIndex,
            int position,
            int minDetachedLength,
            int maxDetachedLength,
            int minRemainingLength,
            out List<int> remaining,
            out List<int> detached)
        {
            remaining = null;
            detached = null;

            var suffixDetached = new List<int>(indices.Count - position + 1) { edgeIndex };
            for (int i = position; i < indices.Count; i++)
                suffixDetached.Add(indices[i]);
            var suffixRemaining = indices.GetRange(0, position);

            var prefixDetached = new List<int>(position + 2) { edgeIndex };
            for (int i = position; i >= 0; i--)
                prefixDetached.Add(indices[i]);
            var prefixRemaining = indices.GetRange(position + 1, indices.Count - position - 1);

            bool suffixOk = IsValidOuterSplitCandidate(suffixDetached, suffixRemaining, minDetachedLength, maxDetachedLength, minRemainingLength);
            bool prefixOk = IsValidOuterSplitCandidate(prefixDetached, prefixRemaining, minDetachedLength, maxDetachedLength, minRemainingLength);
            if (!suffixOk && !prefixOk)
                return false;

            if (suffixOk && (!prefixOk || suffixDetached.Count <= prefixDetached.Count))
            {
                detached = suffixDetached;
                remaining = suffixRemaining;
                return true;
            }

            detached = prefixDetached;
            remaining = prefixRemaining;
            return true;
        }

        private static bool IsValidOuterSplitCandidate(List<int> detached, List<int> remaining, int minDetachedLength, int maxDetachedLength, int minRemainingLength)
        {
            return detached != null &&
                   remaining != null &&
                   detached.Count >= minDetachedLength &&
                   detached.Count <= maxDetachedLength &&
                   remaining.Count >= minRemainingLength;
        }

        private static bool TryFindChainPosition(AuthoredLevelData authored, int index, out int chainIndex, out int position)
        {
            chainIndex = -1;
            position = -1;
            if (authored?.arrows == null)
                return false;

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var indices = authored.arrows[i]?.indices;
                if (indices == null)
                    continue;

                for (int j = 0; j < indices.Count; j++)
                {
                    if (indices[j] != index)
                        continue;

                    chainIndex = i;
                    position = j;
                    return true;
                }
            }

            return false;
        }

        private static int GetOuterInwardSteps(AuthoredLevelData authored, int x, int y, int[] stepX, int[] stepY)
        {
            int count = 0;
            if (x == 0 && authored.width > 1)
            {
                stepX[count] = 1;
                stepY[count] = 0;
                count++;
            }
            else if (x == authored.width - 1 && authored.width > 1)
            {
                stepX[count] = -1;
                stepY[count] = 0;
                count++;
            }

            if (y == 0 && authored.height > 1)
            {
                stepX[count] = 0;
                stepY[count] = 1;
                count++;
            }
            else if (y == authored.height - 1 && authored.height > 1)
            {
                stepX[count] = 0;
                stepY[count] = -1;
                count++;
            }

            return count;
        }

        private static bool HasClearRayToEdge(AuthoredLevelData authored, HashSet<int> occupied, int startX, int startY, int stepX, int stepY)
        {
            int x = startX + stepX;
            int y = startY + stepY;
            while ((uint)x < (uint)authored.width && (uint)y < (uint)authored.height)
            {
                int index = x + y * authored.width;
                if (occupied.Contains(index))
                    return false;

                x += stepX;
                y += stepY;
            }

            return true;
        }

        private static bool TryAddVisibleExitRun(
            AuthoredLevelData authored,
            List<ChainMeta> metas,
            HashSet<int> occupied,
            int headX,
            int headY,
            int inwardStepX,
            int inwardStepY,
            int maxLength,
            int minLength)
        {
            var indices = new List<int>(maxLength);
            int x = headX;
            int y = headY;
            while ((uint)x < (uint)authored.width && (uint)y < (uint)authored.height && indices.Count < maxLength)
            {
                int index = x + y * authored.width;
                if (occupied.Contains(index))
                    break;

                indices.Add(index);
                x += inwardStepX;
                y += inwardStepY;
            }

            if (indices.Count < minLength)
                return false;

            for (int i = 0; i < indices.Count; i++)
                occupied.Add(indices[i]);

            authored.arrows.Add(new AuthoredArrowData
            {
                indices = indices,
                colorIndex = authored.arrows.Count % 16
            });
            metas.Add(new ChainMeta
            {
                SourceIndex = 3,
                TileCount = indices.Count
            });
            return true;
        }

        private static int RotateLeftX(int x, int y)
        {
            return -y;
        }

        private static int RotateLeftY(int x, int y)
        {
            return x;
        }

        private static int RotateRightX(int x, int y)
        {
            return y;
        }

        private static int RotateRightY(int x, int y)
        {
            return -x;
        }

        private static int DistanceToNearestEdge(AuthoredLevelData authored, int x, int y)
        {
            return Mathf.Min(Mathf.Min(x, y), Mathf.Min(authored.width - 1 - x, authored.height - 1 - y));
        }

        private static int CountEmptyNeighborsInOuterBand(
            AuthoredLevelData authored,
            HashSet<int> occupied,
            HashSet<int> path,
            int x,
            int y,
            int bandDepth)
        {
            int count = 0;
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };
            for (int i = 0; i < dx.Length; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];
                if ((uint)nx >= (uint)authored.width || (uint)ny >= (uint)authored.height)
                    continue;
                if (!IsOuterBandCell(authored.width, authored.height, nx, ny, bandDepth))
                    continue;

                int index = nx + ny * authored.width;
                if (!occupied.Contains(index) && !path.Contains(index))
                    count++;
            }

            return count;
        }

        private static List<int> BuildStraightEmptyRun(
            AuthoredLevelData authored,
            HashSet<int> occupied,
            int startX,
            int startY,
            int stepX,
            int stepY,
            int maxLength)
        {
            var indices = new List<int>(maxLength);
            int x = startX;
            int y = startY;
            while ((uint)x < (uint)authored.width && (uint)y < (uint)authored.height && indices.Count < maxLength)
            {
                int index = x + y * authored.width;
                if (occupied.Contains(index))
                    break;

                indices.Add(index);
                x += stepX;
                y += stepY;
            }

            return indices;
        }

        private static int CountShortOuterChainsNear(
            AuthoredLevelData authored,
            List<int> candidateIndices,
            int radius,
            int stopAfter)
        {
            if (authored?.arrows == null || candidateIndices == null || candidateIndices.Count == 0)
                return 0;

            int count = 0;
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var arrow = authored.arrows[i];
                int length = arrow?.indices?.Count ?? 0;
                if (length <= 0 || length > 3 || !ChainTouchesEdge(authored, arrow))
                    continue;
                if (!ChainsWithinRadius(authored, arrow.indices, candidateIndices, radius))
                    continue;

                count++;
                if (count >= stopAfter)
                    return count;
            }

            return count;
        }

        private static HashSet<int> BuildOccupiedSet(AuthoredLevelData authored)
        {
            var occupied = new HashSet<int>();
            if (authored?.arrows == null)
                return occupied;

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var indices = authored.arrows[i]?.indices;
                if (indices == null)
                    continue;

                for (int j = 0; j < indices.Count; j++)
                    occupied.Add(indices[j]);
            }

            return occupied;
        }

        private static bool PassFinalPackQuality(AuthoredLevelData authored, out string details)
        {
            float maxLocalEmpty = CalculateMaxLocalEmptyRatio(authored, LocalEmptyWindowSize, out int emptyCells, out int windowCells);
            if (windowCells > 0 && maxLocalEmpty > MaxLocalEmptyRatio)
            {
                details = $"local empty {maxLocalEmpty:0.000}>{MaxLocalEmptyRatio:0.000} empty={emptyCells}/{windowCells}";
                return false;
            }

            if (HasDominantCentralChannel(authored, out string channelDetails))
            {
                details = channelDetails;
                return false;
            }

            details = $"localEmpty={maxLocalEmpty:0.000}";
            return true;
        }

        private static bool HasDominantCentralChannel(AuthoredLevelData authored, out string details)
        {
            details = string.Empty;
            if (authored?.arrows == null || authored.width <= 0 || authored.height <= 0)
                return false;

            if (HasDominantCentralChannel(
                    authored,
                    verticalBridge: true,
                    out int verticalDominant,
                    out int verticalThreshold,
                    out int verticalInterlocks,
                    out int verticalMaxRun))
            {
                details = $"central channel vertical dominant={verticalDominant}>{verticalThreshold} interlocks={verticalInterlocks} maxRun={verticalMaxRun}";
                return true;
            }

            if (HasDominantCentralChannel(
                    authored,
                    verticalBridge: false,
                    out int horizontalDominant,
                    out int horizontalThreshold,
                    out int horizontalInterlocks,
                    out int horizontalMaxRun))
            {
                details = $"central channel horizontal dominant={horizontalDominant}>{horizontalThreshold} interlocks={horizontalInterlocks} maxRun={horizontalMaxRun}";
                return true;
            }

            return false;
        }

        private static bool HasDominantCentralChannel(
            AuthoredLevelData authored,
            bool verticalBridge,
            out int dominantChains,
            out int dominantThreshold,
            out int interlockChains,
            out int maxRun)
        {
            int minX;
            int maxX;
            int minY;
            int maxY;
            if (verticalBridge)
            {
                int centerX = authored.width / 2;
                minX = Mathf.Max(0, centerX - FinalCentralBridgePadding);
                maxX = Mathf.Min(authored.width - 1, centerX + FinalCentralBridgePadding);
                minY = 0;
                maxY = authored.height - 1;
            }
            else
            {
                minX = 0;
                maxX = authored.width - 1;
                int centerY = authored.height / 2;
                minY = Mathf.Max(0, centerY - FinalCentralBridgePadding);
                maxY = Mathf.Min(authored.height - 1, centerY + FinalCentralBridgePadding);
            }

            if (!MeasureDominantChannel(
                    authored,
                    verticalBridge,
                    minX,
                    maxX,
                    minY,
                    maxY,
                    out dominantChains,
                    out dominantThreshold,
                    out interlockChains,
                    out maxRun))
            {
                return false;
            }

            return (dominantChains >= dominantThreshold + 4 && interlockChains <= MaxFinalCentralChannelInterlocks) ||
                   (dominantChains >= dominantThreshold + 3 && interlockChains <= MaxFinalCentralSoftChannelInterlocks);
        }

        private static bool MeasureDominantChannel(
            AuthoredLevelData authored,
            bool verticalBridge,
            int minX,
            int maxX,
            int minY,
            int maxY,
            out int dominantChains,
            out int dominantThreshold,
            out int interlockChains,
            out int maxRun)
        {
            dominantChains = 0;
            dominantThreshold = 0;
            interlockChains = 0;
            maxRun = 0;
            if (authored?.arrows == null)
                return false;

            int span = verticalBridge ? maxY - minY + 1 : maxX - minX + 1;
            dominantThreshold = Mathf.Max(6, span * DominantSeamChannelRunRatioPercent / 100);
            if (span <= 0)
                return false;

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var indices = authored.arrows[i]?.indices;
                if (indices == null || indices.Count < 2)
                    continue;

                int bandCells = CountCellsInRect(indices, authored.width, minX, maxX, minY, maxY);
                if (bandCells < 3)
                    continue;

                int alongRun = LongestDirectionalRunInRect(indices, authored.width, minX, maxX, minY, maxY, verticalBridge);
                int crossRun = LongestDirectionalRunInRect(indices, authored.width, minX, maxX, minY, maxY, !verticalBridge);
                maxRun = Mathf.Max(maxRun, alongRun);

                if (crossRun >= MinSeamChannelInterlockRun)
                    interlockChains++;

                if (alongRun >= MinOuterFillRunLength && alongRun >= Mathf.Max(MinOuterFillRunLength, crossRun * 2))
                    dominantChains++;
            }

            return true;
        }

        private static float CalculateMaxLocalEmptyRatio(AuthoredLevelData authored, int requestedWindowSize, out int worstEmptyCells, out int worstWindowCells)
        {
            worstEmptyCells = 0;
            worstWindowCells = 0;
            if (authored == null || authored.width <= 0 || authored.height <= 0)
                return 1f;

            int window = Mathf.Min(requestedWindowSize, authored.width, authored.height);
            if (window < 4)
                return 0f;

            var occupied = BuildOccupiedSet(authored);
            int stride = Mathf.Max(1, window / 2);
            float worst = 0f;
            for (int y = 0; y <= authored.height - window; y += stride)
            {
                for (int x = 0; x <= authored.width - window; x += stride)
                {
                    int empty = CountEmptyCellsInWindow(authored, occupied, x, y, window, window);
                    int cells = window * window;
                    float ratio = empty / (float)Mathf.Max(1, cells);
                    if (ratio > worst)
                    {
                        worst = ratio;
                        worstEmptyCells = empty;
                        worstWindowCells = cells;
                    }
                }
            }

            return worst;
        }

        private static int CountEmptyCellsInWindow(AuthoredLevelData authored, HashSet<int> occupied, int startX, int startY, int width, int height)
        {
            int empty = 0;
            for (int y = startY; y < startY + height; y++)
            {
                for (int x = startX; x < startX + width; x++)
                {
                    if (!occupied.Contains(x + y * authored.width))
                        empty++;
                }
            }

            return empty;
        }

        private static int TryAddEdgeRun(
            AuthoredLevelData authored,
            List<ChainMeta> metas,
            HashSet<int> occupied,
            int startX,
            int startY,
            int stepX,
            int stepY,
            int maxLength,
            int minLength,
            int remainingChains)
        {
            if (remainingChains <= 0)
                return 0;

            var indices = new List<int>(maxLength);
            int x = startX;
            int y = startY;
            while ((uint)x < (uint)authored.width && (uint)y < (uint)authored.height && indices.Count < maxLength)
            {
                int index = x + y * authored.width;
                if (occupied.Contains(index))
                    break;

                indices.Add(index);
                x += stepX;
                y += stepY;
            }

            if (indices.Count < minLength)
                return 0;

            for (int i = 0; i < indices.Count; i++)
                occupied.Add(indices[i]);

            authored.arrows.Add(new AuthoredArrowData
            {
                indices = indices,
                colorIndex = authored.arrows.Count % 16
            });
            metas.Add(new ChainMeta
            {
                SourceIndex = 3,
                TileCount = indices.Count
            });

            return 1;
        }

        private static LevelDefinition CreateCompositeDefinition(Plan plan, string id, AuthoredLevelData authored)
        {
            var def = ScriptableObject.CreateInstance<LevelDefinition>();
            def.levelId = id;
            def.name = id;
            def.source = LevelDefinition.LevelSource.Authored;
            def.board.width = plan.Width;
            def.board.height = plan.Height;
            def.board.seed = 0;
            def.generation.arrowCoverage = CountTiles(authored) / (float)Mathf.Max(1, plan.Width * plan.Height);
            def.generation.initialMovableArrowCount = 0;
            def.generation.targetDifficultyScore = plan.A.Def.generation.targetDifficultyScore + plan.B.Def.generation.targetDifficultyScore;
            def.generation.fixedGenerationSeed = 0;
            def.generation.minPathLen = Mathf.Max(2, Mathf.Min(plan.A.Def.generation.minPathLen, plan.B.Def.generation.minPathLen));
            def.generation.maxPathLength = plan.Width * plan.Height;
            def.generation.twistiness = (plan.A.Def.generation.twistiness + plan.B.Def.generation.twistiness) * 0.5f;
            def.generation.validateWithGreedy = true;
            def.lose.blockedLoseLimit = Mathf.Max(plan.A.Def.lose.blockedLoseLimit, plan.B.Def.lose.blockedLoseLimit);
            def.arrowColorMode = plan.A.Def.arrowColorMode;
            def.arrowColorMaskQuantizeSteps = plan.A.Def.arrowColorMaskQuantizeSteps;
            def.tintOnHit = plan.A.Def.tintOnHit;
            def.hitTint = plan.A.Def.hitTint;
            def.introSettings = plan.A.Def.introSettings;
            def.palette = plan.A.Def.palette;
            def.authoredLevel = authored;
            return def;
        }

        private static bool TryBuildBentFallback(Plan plan, out AuthoredLevelData authored, out string details)
        {
            authored = null;
            details = string.Empty;

            if (!TryPickBentSegmentLength(plan, out int segmentLength, out int expectedChains))
            {
                details = $"no bent segment length plan={plan.Width}x{plan.Height}";
                return false;
            }

            authored = new AuthoredLevelData
            {
                width = plan.Width,
                height = plan.Height,
                arrows = new List<AuthoredArrowData>(expectedChains)
            };

            if (plan.Horizontal)
            {
                FillBentHorizontalZone(authored, 0, plan.A.Width, 0, plan.Height, segmentLength, fromLeft: true);
                FillBentHorizontalZone(authored, plan.A.Width, plan.B.Width, 0, plan.Height, segmentLength, fromLeft: false);
            }
            else
            {
                FillBentVerticalZone(authored, 0, plan.Width, 0, plan.A.Height, segmentLength, fromBottom: true);
                FillBentVerticalZone(authored, 0, plan.Width, plan.A.Height, plan.B.Height, segmentLength, fromBottom: false);
            }

            if (authored.arrows.Count < MinChains || authored.arrows.Count > MaxChains)
            {
                details = $"bent fallback chains out of range {authored.arrows.Count}";
                return false;
            }

            if (CountTiles(authored) < plan.Width * plan.Height)
            {
                details = $"bent fallback did not fill board tiles={CountTiles(authored)} area={plan.Width * plan.Height}";
                return false;
            }

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out var board, out string buildError))
            {
                details = $"bent fallback build failed {buildError}";
                return false;
            }

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int greedyBudget = Mathf.Max(FastGreedyBudgetMin, authored.arrows.Count * FastGreedyBudgetPerChain);
            if (!GreedyValidator.TryClearAllByGreedy(board, rules, greedyBudget, out _))
            {
                details = "bent fallback greedy failed";
                return false;
            }

            details = $"bent fallback segment={segmentLength} chains={authored.arrows.Count}";
            return true;
        }

        private static bool TryPickBentSegmentLength(Plan plan, out int segmentLength, out int expectedChains)
        {
            segmentLength = 0;
            expectedChains = 0;
            int bestDistance = int.MaxValue;

            for (int len = 3; len <= 12; len++)
            {
                int count = plan.Horizontal
                    ? CountBentHorizontalZoneChains(plan.A.Width, plan.Height, len) + CountBentHorizontalZoneChains(plan.B.Width, plan.Height, len)
                    : CountBentVerticalZoneChains(plan.Width, plan.A.Height, len) + CountBentVerticalZoneChains(plan.Width, plan.B.Height, len);

                if (count < MinChains || count > MaxChains)
                    continue;

                int distance = Mathf.Abs(count - plan.Chains);
                if (distance >= bestDistance)
                    continue;

                bestDistance = distance;
                segmentLength = len;
                expectedChains = count;
            }

            return segmentLength > 0;
        }

        private static int CountBentHorizontalZoneChains(int width, int height, int segmentLength)
        {
            return ((height + 1) / 2) * CountSegments(width, segmentLength);
        }

        private static int CountBentVerticalZoneChains(int width, int height, int segmentLength)
        {
            return ((width + 1) / 2) * CountSegments(height, segmentLength);
        }

        private static void FillBentHorizontalZone(AuthoredLevelData authored, int startX, int width, int startY, int height, int segmentLength, bool fromLeft)
        {
            for (int y = startY; y < startY + height; y += 2)
            {
                bool hasSecondRow = y + 1 < startY + height;
                if (fromLeft)
                {
                    int x = startX;
                    foreach (int len in EnumerateSegmentLengths(width, segmentLength))
                    {
                        var indices = new List<int>(hasSecondRow ? len * 2 : len);
                        for (int i = 0; i < len; i++)
                            indices.Add((x + i) + y * authored.width);
                        if (hasSecondRow)
                        {
                            for (int i = len - 1; i >= 0; i--)
                                indices.Add((x + i) + (y + 1) * authored.width);
                        }
                        AddArrow(authored, indices);
                        x += len;
                    }
                }
                else
                {
                    int x = startX + width - 1;
                    foreach (int len in EnumerateSegmentLengths(width, segmentLength))
                    {
                        var indices = new List<int>(hasSecondRow ? len * 2 : len);
                        for (int i = 0; i < len; i++)
                            indices.Add((x - i) + y * authored.width);
                        if (hasSecondRow)
                        {
                            for (int i = len - 1; i >= 0; i--)
                                indices.Add((x - i) + (y + 1) * authored.width);
                        }
                        AddArrow(authored, indices);
                        x -= len;
                    }
                }
            }
        }

        private static void FillBentVerticalZone(AuthoredLevelData authored, int startX, int width, int startY, int height, int segmentLength, bool fromBottom)
        {
            for (int x = startX; x < startX + width; x += 2)
            {
                bool hasSecondColumn = x + 1 < startX + width;
                if (fromBottom)
                {
                    int y = startY;
                    foreach (int len in EnumerateSegmentLengths(height, segmentLength))
                    {
                        var indices = new List<int>(hasSecondColumn ? len * 2 : len);
                        for (int i = 0; i < len; i++)
                            indices.Add(x + (y + i) * authored.width);
                        if (hasSecondColumn)
                        {
                            for (int i = len - 1; i >= 0; i--)
                                indices.Add((x + 1) + (y + i) * authored.width);
                        }
                        AddArrow(authored, indices);
                        y += len;
                    }
                }
                else
                {
                    int y = startY + height - 1;
                    foreach (int len in EnumerateSegmentLengths(height, segmentLength))
                    {
                        var indices = new List<int>(hasSecondColumn ? len * 2 : len);
                        for (int i = 0; i < len; i++)
                            indices.Add(x + (y - i) * authored.width);
                        if (hasSecondColumn)
                        {
                            for (int i = len - 1; i >= 0; i--)
                                indices.Add((x + 1) + (y - i) * authored.width);
                        }
                        AddArrow(authored, indices);
                        y -= len;
                    }
                }
            }
        }

        private static bool TryPickChunkLength(Plan plan, int preferredChains, out int chunkLength, out int expectedChains)
        {
            chunkLength = 0;
            expectedChains = 0;
            int bestDistance = int.MaxValue;

            for (int len = 2; len <= 32; len++)
            {
                int count = plan.Horizontal
                    ? CountHorizontalZoneChains(plan.A.Width, plan.Height, len) + CountRightZoneChains(plan, len)
                    : CountVerticalZoneChains(plan.Width, plan.A.Height, len) + CountVerticalZoneChains(plan.Width, plan.B.Height, len);

                if (count < MinChains || count > MaxChains)
                    continue;

                int distance = Mathf.Abs(count - preferredChains);
                if (distance >= bestDistance)
                    continue;

                bestDistance = distance;
                chunkLength = len;
                expectedChains = count;
            }

            return chunkLength > 0;
        }

        private static int CountRightZoneChains(Plan plan, int chunkLength)
        {
            return UseMixedRightZone(plan)
                ? CountVerticalZoneChains(plan.B.Width, plan.Height, chunkLength)
                : CountHorizontalZoneChains(plan.B.Width, plan.Height, chunkLength);
        }

        private static bool UseMixedRightZone(Plan plan)
        {
            return plan.Horizontal && (StableHash(plan.A.Name + "|" + plan.B.Name) & 1) == 0;
        }

        private static string LayoutLabel(Plan plan)
        {
            return $"{(plan.Horizontal ? "H" : "V")}{plan.Gap}";
        }

        private static int StableHash(string value)
        {
            unchecked
            {
                int hash = 17;
                for (int i = 0; i < value.Length; i++)
                    hash = hash * 31 + value[i];
                return hash;
            }
        }

        private static int CountHorizontalZoneChains(int zoneWidth, int zoneHeight, int chunkLength)
        {
            return zoneHeight * CountSegments(zoneWidth, chunkLength);
        }

        private static int CountVerticalZoneChains(int zoneWidth, int zoneHeight, int chunkLength)
        {
            return zoneWidth * CountSegments(zoneHeight, chunkLength);
        }

        private static int CountSegments(int length, int chunkLength)
        {
            int count = 0;
            foreach (int _ in EnumerateSegmentLengths(length, chunkLength))
                count++;
            return count;
        }

        private static void FillHorizontalZone(AuthoredLevelData authored, int startX, int width, int startY, int height, int chunkLength, Dir exitDir)
        {
            for (int y = startY; y < startY + height; y++)
            {
                if (exitDir == Dir.Left)
                {
                    int x = startX;
                    foreach (int len in EnumerateSegmentLengths(width, chunkLength))
                    {
                        var indices = new List<int>(len);
                        for (int i = 0; i < len; i++)
                            indices.Add((x + i) + y * authored.width);
                        AddArrow(authored, indices);
                        x += len;
                    }
                }
                else
                {
                    int x = startX + width - 1;
                    foreach (int len in EnumerateSegmentLengths(width, chunkLength))
                    {
                        var indices = new List<int>(len);
                        for (int i = 0; i < len; i++)
                            indices.Add((x - i) + y * authored.width);
                        AddArrow(authored, indices);
                        x -= len;
                    }
                }
            }
        }

        private static void FillVerticalZone(AuthoredLevelData authored, int startX, int width, int startY, int height, int chunkLength, Dir exitDir)
        {
            for (int x = startX; x < startX + width; x++)
            {
                if (exitDir == Dir.Down)
                {
                    int y = startY;
                    foreach (int len in EnumerateSegmentLengths(height, chunkLength))
                    {
                        var indices = new List<int>(len);
                        for (int i = 0; i < len; i++)
                            indices.Add(x + (y + i) * authored.width);
                        AddArrow(authored, indices);
                        y += len;
                    }
                }
                else
                {
                    int y = startY + height - 1;
                    foreach (int len in EnumerateSegmentLengths(height, chunkLength))
                    {
                        var indices = new List<int>(len);
                        for (int i = 0; i < len; i++)
                            indices.Add(x + (y - i) * authored.width);
                        AddArrow(authored, indices);
                        y -= len;
                    }
                }
            }
        }

        private static IEnumerable<int> EnumerateSegmentLengths(int length, int chunkLength)
        {
            int remaining = length;
            while (remaining > 0)
            {
                int len = Mathf.Min(chunkLength, remaining);
                if (remaining - len == 1 && len > 2)
                    len--;
                else if (remaining == 1)
                    yield break;

                yield return len;
                remaining -= len;
            }
        }

        private static void AddArrow(AuthoredLevelData authored, List<int> indices)
        {
            if (indices.Count < 2)
                return;

            authored.arrows.Add(new AuthoredArrowData
            {
                indices = indices,
                colorIndex = authored.arrows.Count % 16
            });
        }

        private static bool TryRepairCompositeToGreedy(AuthoredLevelData authored, List<ChainMeta> metas, Plan plan, out string details)
        {
            details = string.Empty;
            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int area = Mathf.Max(1, authored.width * authored.height);
            int greedyBudget = Mathf.Max(FastGreedyBudgetMin, authored.arrows.Count * FastGreedyBudgetPerChain);
            int removed = 0;
            int reversed = 0;
            int tacticalReversed = 0;
            int iterations = 0;
            int maxRemove = PairVariantPriority(plan) <= 1 ? 14 : 10;
            string lastDependencyRepair = string.Empty;

            reversed += ReverseInwardFacingChains(authored, metas, plan);

            while (authored.arrows.Count >= RepairMinChains && removed <= maxRemove && iterations < MaxGreedyRepairIterationsPerPlan)
            {
                iterations++;
                int tiles = CountTiles(authored);
                float coverage = tiles / (float)area;
                if (coverage < RepairMinCoverage)
                {
                    details = $"coverage below minimum after repair {coverage:0.000}";
                    return false;
                }

                if (!AuthoredLevelBuilder.TryBuildBoard(authored, out var board, out string buildError))
                {
                    details = $"build after repair failed {buildError}";
                    return false;
                }

                if (GreedyValidator.TryClearAllByGreedy(board, rules, greedyBudget, out _))
                {
                    details = $"preserved-repaired reversed={reversed} removed={removed} chains={authored.arrows.Count} coverage={coverage:0.000} dep={lastDependencyRepair}";
                    return true;
                }

                if (tacticalReversed < MaxTacticalReversalsPerPlan && TryReverseOneCrossSourceBlockedChain(authored, metas))
                {
                    reversed++;
                    tacticalReversed++;
                    continue;
                }

                if (tacticalReversed < MaxTacticalReversalsPerPlan && TryReverseOneDependencyBlockedChain(authored, metas, out string dependencyReverse))
                {
                    reversed++;
                    tacticalReversed++;
                    lastDependencyRepair = dependencyReverse;
                    continue;
                }

                int removeIndex = PickDependencyCycleBreaker(authored, metas, out string dependencyDelete);
                if (removeIndex < 0)
                    removeIndex = PickCrossSourceBlockedChain(authored, metas);
                if (removeIndex < 0)
                {
                    details = $"greedy failed and no dependency blocked chain found reversed={reversed} removed={removed} dep={lastDependencyRepair}";
                    return false;
                }

                lastDependencyRepair = dependencyDelete;
                authored.arrows.RemoveAt(removeIndex);
                metas.RemoveAt(removeIndex);
                removed++;
            }

            details = $"repair exhausted chains={authored.arrows.Count} reversed={reversed} removed={removed} iterations={iterations}/{MaxGreedyRepairIterationsPerPlan} dep={lastDependencyRepair}";
            return false;
        }

        private static int ReverseInwardFacingChains(AuthoredLevelData authored, List<ChainMeta> metas, Plan plan)
        {
            int reversed = 0;
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                if (!IsInwardFacingChain(authored, metas, plan, i))
                    continue;

                var indices = authored.arrows[i]?.indices;
                if (indices == null || indices.Count < 2)
                    continue;

                indices.Reverse();
                bool valid = AuthoredLevelBuilder.TryBuildBoard(authored, out _, out _) && !IsInwardFacingChain(authored, metas, plan, i);
                if (valid)
                {
                    reversed++;
                    continue;
                }

                indices.Reverse();
            }

            return reversed;
        }

        private static bool IsInwardFacingChain(AuthoredLevelData authored, List<ChainMeta> metas, Plan plan, int chainIndex)
        {
            if (chainIndex < 0 || chainIndex >= authored.arrows.Count)
                return false;

            Dir dir = GetHeadDir(authored.arrows[chainIndex], authored.width);
            int source = metas[chainIndex].SourceIndex;
            if (source != 0 && source != 1)
                return false;
            if (plan.Horizontal)
                return source == 0 ? dir == Dir.Right : dir == Dir.Left;

            return source == 0 ? dir == Dir.Up : dir == Dir.Down;
        }

        private static bool TryReverseOneCrossSourceBlockedChain(AuthoredLevelData authored, List<ChainMeta> metas)
        {
            int before = CountCrossSourceBlocks(authored, metas, out var blockedByOther, out var blocksOther);
            if (before <= 0)
                return false;

            int best = -1;
            int bestAfter = before;
            for (int pass = 0; pass < 2; pass++)
            {
                for (int i = 0; i < authored.arrows.Count; i++)
                {
                    bool candidate = pass == 0 ? blockedByOther[i] > 0 : blocksOther[i] > 0;
                    if (!candidate || authored.arrows[i]?.indices == null || authored.arrows[i].indices.Count < 2)
                        continue;

                    authored.arrows[i].indices.Reverse();
                    bool buildOk = AuthoredLevelBuilder.TryBuildBoard(authored, out _, out _);
                    int after = buildOk ? CountCrossSourceBlocks(authored, metas, out _, out _) : int.MaxValue;
                    authored.arrows[i].indices.Reverse();

                    if (after >= bestAfter)
                        continue;

                    bestAfter = after;
                    best = i;
                    if (bestAfter == 0)
                        break;
                }

                if (bestAfter == 0)
                    break;
            }

            if (best < 0)
                return false;

            authored.arrows[best].indices.Reverse();
            return true;
        }

        private static bool TryReverseOneDependencyBlockedChain(AuthoredLevelData authored, List<ChainMeta> metas, out string details)
        {
            details = string.Empty;
            var before = BuildDependencySnapshot(authored);
            if (before.TotalBlocks <= 0)
                return false;

            int best = -1;
            int bestScore = int.MinValue;
            DependencySnapshot bestAfter = null;
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var indices = authored.arrows[i]?.indices;
                if (!IsDependencyRepairCandidate(before, i) || indices == null || indices.Count < 2)
                    continue;

                indices.Reverse();
                bool buildOk = AuthoredLevelBuilder.TryBuildBoard(authored, out _, out _);
                DependencySnapshot after = buildOk ? BuildDependencySnapshot(authored) : null;
                indices.Reverse();
                if (after == null || !DependencyRepairImproves(before, after))
                    continue;

                int score = ScoreDependencyTransition(before, after) +
                            ScoreDependencyRepairCandidate(authored, metas, before, i);
                if (score <= bestScore)
                    continue;

                best = i;
                bestScore = score;
                bestAfter = after;
            }

            if (best < 0)
                return false;

            authored.arrows[best].indices.Reverse();
            details = FormatDependencyRepair("rev", best, before, bestAfter);
            return true;
        }

        private static int PickDependencyCycleBreaker(AuthoredLevelData authored, List<ChainMeta> metas, out string details)
        {
            details = string.Empty;
            var before = BuildDependencySnapshot(authored);
            if (before.TotalBlocks <= 0)
                return -1;

            int best = -1;
            int bestScore = int.MinValue;
            DependencySnapshot bestAfter = null;
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                if (!IsDependencyRepairCandidate(before, i))
                    continue;

                var arrow = authored.arrows[i];
                var meta = metas[i];
                authored.arrows.RemoveAt(i);
                metas.RemoveAt(i);
                bool buildOk = AuthoredLevelBuilder.TryBuildBoard(authored, out _, out _);
                DependencySnapshot after = buildOk ? BuildDependencySnapshot(authored) : null;
                metas.Insert(i, meta);
                authored.arrows.Insert(i, arrow);
                if (after == null)
                    continue;

                int score = ScoreDependencyTransition(before, after) +
                            ScoreDependencyRepairCandidate(authored, metas, before, i);
                if (!DependencyRepairImproves(before, after))
                    score -= 5000;
                if (score <= bestScore)
                    continue;

                best = i;
                bestScore = score;
                bestAfter = after;
            }

            if (best >= 0)
                details = FormatDependencyRepair("del", best, before, bestAfter);
            return best;
        }

        private static int ScoreDependencyRepairCandidate(AuthoredLevelData authored, List<ChainMeta> metas, DependencySnapshot snapshot, int chainIndex)
        {
            int tiles = authored.arrows[chainIndex]?.indices?.Count ?? 0;
            int source = SafeMetaOwner(metas, chainIndex);
            int blockedByAny = SafeArrayValue(snapshot?.BlockedByAny, chainIndex);
            int blocksAny = SafeArrayValue(snapshot?.BlocksAny, chainIndex);
            int fullBlockedByAny = SafeArrayValue(snapshot?.FullBlockedByAny, chainIndex);
            int fullBlocksAny = SafeArrayValue(snapshot?.FullBlocksAny, chainIndex);
            bool inCycle = SafeArrayValue(snapshot?.CycleMembers, chainIndex);
            bool gate = snapshot != null && snapshot.GateChainIndex == chainIndex;
            int score = blocksAny * 130 + blockedByAny * 55 - tiles * 3;
            score += fullBlocksAny * 28 + fullBlockedByAny * 9;
            if (inCycle)
                score += 1000;
            if (gate)
                score += 1800 + SafeArrayValue(snapshot?.FullBlocksAny, chainIndex) * 35;
            if (source >= 2)
                score += 220;
            var arrow = authored.arrows[chainIndex];
            if (arrow?.indices != null && ChainTouchesEdge(authored, arrow))
                score -= 90;
            return score;
        }

        private static int SafeArrayValue(int[] values, int index)
        {
            return values != null && index >= 0 && index < values.Length ? values[index] : 0;
        }

        private static bool SafeArrayValue(bool[] values, int index)
        {
            return values != null && index >= 0 && index < values.Length && values[index];
        }

        private static DependencySnapshot BuildDependencySnapshot(AuthoredLevelData authored)
        {
            int total = CountAllHeadRayBlocks(authored, out var blockedByAny, out var blocksAny, out _, out var blockerByChain);
            var blockerQueues = BuildHeadRayBlockerQueues(authored);
            AnalyzeBlockerQueues(
                blockerQueues,
                out int fullRayBlockCount,
                out int maxRayBlockers,
                out int[] fullBlockedByAny,
                out int[] fullBlocksAny,
                out int[] clearLayers,
                out int layerClearedCount,
                out int layerStuckCount,
                out int maxClearLayer,
                out int gateChainIndex,
                out int gateChainBlocks,
                out string stuckSample);
            MarkDependencyCycles(
                blockerByChain,
                out var cycleMembers,
                out var cycleIds,
                out var cycleLengths,
                out int cycleCount,
                out int largestCycleLength,
                out string sampleCycle);

            return new DependencySnapshot
            {
                TotalBlocks = total,
                FullRayBlockCount = fullRayBlockCount,
                MaxRayBlockers = maxRayBlockers,
                CycleCount = cycleCount,
                CycleMemberCount = CountTrue(cycleMembers),
                LargestCycleLength = largestCycleLength,
                LayerClearedCount = layerClearedCount,
                LayerStuckCount = layerStuckCount,
                MaxClearLayer = maxClearLayer,
                GateChainIndex = gateChainIndex,
                GateChainBlocks = gateChainBlocks,
                BlockedByAny = blockedByAny,
                BlocksAny = blocksAny,
                FullBlockedByAny = fullBlockedByAny,
                FullBlocksAny = fullBlocksAny,
                BlockerByChain = blockerByChain,
                CycleMembers = cycleMembers,
                CycleIds = cycleIds,
                CycleLengths = cycleLengths,
                ClearLayers = clearLayers,
                BlockerQueues = blockerQueues,
                SampleCycle = sampleCycle,
                StuckSample = stuckSample
            };
        }

        private static bool IsDependencyRepairCandidate(DependencySnapshot snapshot, int chainIndex)
        {
            if (snapshot == null || chainIndex < 0 || chainIndex >= (snapshot.BlockedByAny?.Length ?? 0))
                return false;

            return snapshot.BlockedByAny[chainIndex] > 0 ||
                   snapshot.BlocksAny[chainIndex] > 0 ||
                   SafeArrayValue(snapshot.FullBlockedByAny, chainIndex) > 0 ||
                   SafeArrayValue(snapshot.FullBlocksAny, chainIndex) > 0 ||
                   snapshot.GateChainIndex == chainIndex ||
                   (snapshot.CycleMembers != null &&
                    chainIndex < snapshot.CycleMembers.Length &&
                    snapshot.CycleMembers[chainIndex]);
        }

        private static bool DependencyRepairImproves(DependencySnapshot before, DependencySnapshot after)
        {
            if (before == null || after == null)
                return false;

            return after.CycleMemberCount < before.CycleMemberCount ||
                   after.CycleCount < before.CycleCount ||
                   after.LargestCycleLength < before.LargestCycleLength ||
                   after.LayerStuckCount < before.LayerStuckCount ||
                   after.LayerClearedCount > before.LayerClearedCount ||
                   after.GateChainBlocks < before.GateChainBlocks ||
                   after.MaxRayBlockers < before.MaxRayBlockers ||
                   after.FullRayBlockCount < before.FullRayBlockCount ||
                   after.TotalBlocks < before.TotalBlocks;
        }

        private static int ScoreDependencyTransition(DependencySnapshot before, DependencySnapshot after)
        {
            if (before == null || after == null)
                return int.MinValue / 4;

            return
                (before.LayerStuckCount - after.LayerStuckCount) * 7200 +
                (after.LayerClearedCount - before.LayerClearedCount) * 5200 +
                (before.CycleMemberCount - after.CycleMemberCount) * 6000 +
                (before.CycleCount - after.CycleCount) * 4200 +
                (before.LargestCycleLength - after.LargestCycleLength) * 1400 +
                (before.GateChainBlocks - after.GateChainBlocks) * 720 +
                (before.MaxRayBlockers - after.MaxRayBlockers) * 480 +
                (before.FullRayBlockCount - after.FullRayBlockCount) * 70 +
                (before.TotalBlocks - after.TotalBlocks) * 260;
        }

        private static string FormatDependencyRepair(string mode, int chainIndex, DependencySnapshot before, DependencySnapshot after)
        {
            if (before == null || after == null)
                return $"{mode}#{chainIndex}";

            string cycle = string.IsNullOrEmpty(before.SampleCycle) ? "none" : before.SampleCycle;
            string stuck = string.IsNullOrEmpty(before.StuckSample) ? "none" : before.StuckSample;
            return $"{mode}#{chainIndex} blocks={before.TotalBlocks}->{after.TotalBlocks} rayBlocks={before.FullRayBlockCount}->{after.FullRayBlockCount} layers={before.LayerClearedCount}/{before.LayerStuckCount}->{after.LayerClearedCount}/{after.LayerStuckCount} gate={before.GateChainIndex}:{before.GateChainBlocks}->{after.GateChainIndex}:{after.GateChainBlocks} cycles={before.CycleCount}/{before.CycleMemberCount}->{after.CycleCount}/{after.CycleMemberCount} maxCycle={before.LargestCycleLength}->{after.LargestCycleLength} sample={cycle} stuck={stuck}";
        }

        private static int CountAllHeadRayBlocks(
            AuthoredLevelData authored,
            out int[] blockedByAny,
            out int[] blocksAny,
            out bool[] cycleMembers,
            out int[] blockerByChain)
        {
            int count = authored?.arrows?.Count ?? 0;
            blockedByAny = new int[count];
            blocksAny = new int[count];
            blockerByChain = new int[count];
            for (int i = 0; i < blockerByChain.Length; i++)
                blockerByChain[i] = -1;

            if (authored?.arrows == null)
            {
                cycleMembers = Array.Empty<bool>();
                return 0;
            }

            var chainByCell = BuildChainByCell(authored);
            int total = 0;
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                int blocker = FindAnyHeadRayBlocker(authored.width, authored.height, authored.arrows[i], chainByCell);
                if (blocker < 0 || blocker >= count)
                    continue;

                blockerByChain[i] = blocker;
                blockedByAny[i]++;
                blocksAny[blocker]++;
                total++;
            }

            cycleMembers = MarkDependencyCycleMembers(blockerByChain);
            return total;
        }

        private static List<int>[] BuildHeadRayBlockerQueues(AuthoredLevelData authored)
        {
            int count = authored?.arrows?.Count ?? 0;
            var queues = new List<int>[count];
            for (int i = 0; i < count; i++)
                queues[i] = new List<int>();

            if (authored?.arrows == null)
                return queues;

            var chainByCell = BuildChainByCell(authored);
            for (int i = 0; i < authored.arrows.Count; i++)
                queues[i] = FindAllHeadRayBlockers(authored.width, authored.height, authored.arrows[i], chainByCell);

            return queues;
        }

        private static void AnalyzeBlockerQueues(
            List<int>[] blockerQueues,
            out int fullRayBlockCount,
            out int maxRayBlockers,
            out int[] fullBlockedByAny,
            out int[] fullBlocksAny,
            out int[] clearLayers,
            out int layerClearedCount,
            out int layerStuckCount,
            out int maxClearLayer,
            out int gateChainIndex,
            out int gateChainBlocks,
            out string stuckSample)
        {
            int count = blockerQueues?.Length ?? 0;
            fullBlockedByAny = new int[count];
            fullBlocksAny = new int[count];
            clearLayers = new int[count];
            for (int i = 0; i < clearLayers.Length; i++)
                clearLayers[i] = -1;

            fullRayBlockCount = 0;
            maxRayBlockers = 0;
            for (int i = 0; i < count; i++)
            {
                var queue = blockerQueues[i] ?? new List<int>();
                fullBlockedByAny[i] = queue.Count;
                fullRayBlockCount += queue.Count;
                maxRayBlockers = Mathf.Max(maxRayBlockers, queue.Count);
                for (int q = 0; q < queue.Count; q++)
                {
                    int blocker = queue[q];
                    if ((uint)blocker < (uint)count)
                        fullBlocksAny[blocker]++;
                }
            }

            var cleared = new bool[count];
            int remaining = count;
            int layer = 0;
            maxClearLayer = -1;
            while (remaining > 0)
            {
                var next = new List<int>();
                for (int i = 0; i < count; i++)
                {
                    if (cleared[i])
                        continue;
                    if (AllBlockersCleared(blockerQueues[i], cleared))
                        next.Add(i);
                }

                if (next.Count == 0)
                    break;

                for (int i = 0; i < next.Count; i++)
                {
                    int chain = next[i];
                    cleared[chain] = true;
                    clearLayers[chain] = layer;
                    remaining--;
                }

                maxClearLayer = layer;
                layer++;
            }

            layerClearedCount = count - remaining;
            layerStuckCount = remaining;
            PickGateChain(blockerQueues, cleared, fullBlocksAny, out gateChainIndex, out gateChainBlocks);
            stuckSample = FormatStuckSample(cleared, maxItems: 8);
        }

        private static bool AllBlockersCleared(List<int> blockers, bool[] cleared)
        {
            if (blockers == null || blockers.Count == 0)
                return true;

            int count = cleared?.Length ?? 0;
            for (int i = 0; i < blockers.Count; i++)
            {
                int blocker = blockers[i];
                if ((uint)blocker < (uint)count && !cleared[blocker])
                    return false;
            }

            return true;
        }

        private static void PickGateChain(List<int>[] blockerQueues, bool[] cleared, int[] fullBlocksAny, out int gateChainIndex, out int gateChainBlocks)
        {
            int count = blockerQueues?.Length ?? 0;
            var blockedStuckCount = new int[count];
            for (int i = 0; i < count; i++)
            {
                if (cleared != null && i < cleared.Length && cleared[i])
                    continue;

                var queue = blockerQueues[i];
                if (queue == null)
                    continue;

                for (int q = 0; q < queue.Count; q++)
                {
                    int blocker = queue[q];
                    if ((uint)blocker >= (uint)count)
                        continue;
                    if (cleared != null && blocker < cleared.Length && cleared[blocker])
                        continue;

                    blockedStuckCount[blocker]++;
                }
            }

            gateChainIndex = -1;
            gateChainBlocks = 0;
            for (int i = 0; i < count; i++)
            {
                int score = blockedStuckCount[i];
                if (score <= gateChainBlocks)
                    continue;

                gateChainIndex = i;
                gateChainBlocks = score;
            }

            if (gateChainIndex < 0 && fullBlocksAny != null)
            {
                for (int i = 0; i < fullBlocksAny.Length; i++)
                {
                    if (fullBlocksAny[i] <= gateChainBlocks)
                        continue;

                    gateChainIndex = i;
                    gateChainBlocks = fullBlocksAny[i];
                }
            }
        }

        private static string FormatStuckSample(bool[] cleared, int maxItems)
        {
            if (cleared == null)
                return string.Empty;

            var sample = new List<int>(maxItems);
            for (int i = 0; i < cleared.Length && sample.Count < maxItems; i++)
            {
                if (!cleared[i])
                    sample.Add(i);
            }

            return sample.Count > 0 ? string.Join(">", sample) : string.Empty;
        }

        private static Dictionary<int, int> BuildChainByCell(AuthoredLevelData authored)
        {
            var chainByCell = new Dictionary<int, int>((authored?.arrows?.Count ?? 0) * 8);
            if (authored?.arrows == null)
                return chainByCell;

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var indices = authored.arrows[i]?.indices;
                if (indices == null)
                    continue;

                for (int j = 0; j < indices.Count; j++)
                    chainByCell[indices[j]] = i;
            }

            return chainByCell;
        }

        private static int FindAnyHeadRayBlocker(int width, int height, AuthoredArrowData arrow, Dictionary<int, int> chainByCell)
        {
            if (arrow?.indices == null || arrow.indices.Count < 2)
                return -1;

            int head = arrow.indices[0];
            int neck = arrow.indices[1];
            int hx = head % width;
            int hy = head / width;
            int nx = neck % width;
            int ny = neck / width;
            int dx = Math.Sign(hx - nx);
            int dy = Math.Sign(hy - ny);
            if (Mathf.Abs(dx) + Mathf.Abs(dy) != 1)
                return -1;

            int x = hx + dx;
            int y = hy + dy;
            while ((uint)x < (uint)width && (uint)y < (uint)height)
            {
                int idx = x + y * width;
                if (chainByCell.TryGetValue(idx, out int blocker))
                    return blocker;

                x += dx;
                y += dy;
            }

            return -1;
        }

        private static List<int> FindAllHeadRayBlockers(int width, int height, AuthoredArrowData arrow, Dictionary<int, int> chainByCell)
        {
            var blockers = new List<int>();
            if (arrow?.indices == null || arrow.indices.Count < 2)
                return blockers;

            int head = arrow.indices[0];
            int neck = arrow.indices[1];
            int hx = head % width;
            int hy = head / width;
            int nx = neck % width;
            int ny = neck / width;
            int dx = Math.Sign(hx - nx);
            int dy = Math.Sign(hy - ny);
            if (Mathf.Abs(dx) + Mathf.Abs(dy) != 1)
                return blockers;

            var seen = new HashSet<int>();
            int x = hx + dx;
            int y = hy + dy;
            while ((uint)x < (uint)width && (uint)y < (uint)height)
            {
                int idx = x + y * width;
                if (chainByCell.TryGetValue(idx, out int blocker) && seen.Add(blocker))
                    blockers.Add(blocker);

                x += dx;
                y += dy;
            }

            return blockers;
        }

        private static bool[] MarkDependencyCycleMembers(int[] blockerByChain)
        {
            int count = blockerByChain?.Length ?? 0;
            var inCycle = new bool[count];
            var visited = new bool[count];
            for (int start = 0; start < count; start++)
            {
                if (visited[start])
                    continue;

                var localStep = new Dictionary<int, int>();
                int node = start;
                int step = 0;
                while ((uint)node < (uint)count)
                {
                    if (localStep.TryGetValue(node, out _))
                    {
                        int cycleNode = node;
                        do
                        {
                            inCycle[cycleNode] = true;
                            cycleNode = blockerByChain[cycleNode];
                        }
                        while ((uint)cycleNode < (uint)count && cycleNode != node);
                        break;
                    }

                    if (visited[node])
                        break;

                    localStep[node] = step++;
                    node = blockerByChain[node];
                }

                foreach (var item in localStep)
                    visited[item.Key] = true;
            }

            return inCycle;
        }

        private static void MarkDependencyCycles(
            int[] blockerByChain,
            out bool[] inCycle,
            out int[] cycleIds,
            out int[] cycleLengths,
            out int cycleCount,
            out int largestCycleLength,
            out string sampleCycle)
        {
            int count = blockerByChain?.Length ?? 0;
            inCycle = new bool[count];
            cycleIds = new int[count];
            cycleLengths = new int[count];
            for (int i = 0; i < cycleIds.Length; i++)
                cycleIds[i] = -1;

            cycleCount = 0;
            largestCycleLength = 0;
            sampleCycle = string.Empty;
            if (count == 0)
                return;

            var visited = new bool[count];
            for (int start = 0; start < count; start++)
            {
                if (visited[start])
                    continue;

                var localStep = new Dictionary<int, int>();
                int node = start;
                int step = 0;
                while ((uint)node < (uint)count)
                {
                    if (localStep.TryGetValue(node, out _))
                    {
                        var cycle = new List<int>();
                        int cycleNode = node;
                        do
                        {
                            cycle.Add(cycleNode);
                            cycleNode = blockerByChain[cycleNode];
                        }
                        while ((uint)cycleNode < (uint)count && cycleNode != node);

                        int id = cycleCount++;
                        int length = cycle.Count;
                        largestCycleLength = Mathf.Max(largestCycleLength, length);
                        if (string.IsNullOrEmpty(sampleCycle))
                            sampleCycle = string.Join(">", cycle);

                        for (int i = 0; i < cycle.Count; i++)
                        {
                            int chain = cycle[i];
                            inCycle[chain] = true;
                            cycleIds[chain] = id;
                            cycleLengths[chain] = length;
                        }
                        break;
                    }

                    if (visited[node])
                        break;

                    localStep[node] = step++;
                    node = blockerByChain[node];
                }

                foreach (var item in localStep)
                    visited[item.Key] = true;
            }
        }

        private static int CountTrue(bool[] values)
        {
            int count = 0;
            if (values == null)
                return count;

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i])
                    count++;
            }

            return count;
        }

        private static int CountCrossSourceBlocks(AuthoredLevelData authored, List<ChainMeta> metas, out int[] blockedByOther, out int[] blocksOther)
        {
            BuildCellMaps(authored, metas, out var ownerByCell, out var chainByCell);
            blockedByOther = new int[authored.arrows.Count];
            blocksOther = new int[authored.arrows.Count];
            int total = 0;

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var arrow = authored.arrows[i];
                if (arrow?.indices == null || arrow.indices.Count < 2)
                    continue;

                int blocker = FindHeadRayBlocker(authored.width, authored.height, arrow, ownerByCell, chainByCell, metas[i].SourceIndex);
                if (blocker < 0)
                    continue;

                blockedByOther[i]++;
                blocksOther[blocker]++;
                total++;
            }

            return total;
        }

        private static int PickCrossSourceBlockedChain(AuthoredLevelData authored, List<ChainMeta> metas)
        {
            CountCrossSourceBlocks(authored, metas, out var blockedByOther, out var blocksOther);
            int[] tileCounts = new int[authored.arrows.Count];
            for (int i = 0; i < authored.arrows.Count; i++)
                tileCounts[i] = authored.arrows[i]?.indices?.Count ?? 0;

            int best = -1;
            int bestScore = int.MinValue;
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                if (blockedByOther[i] == 0 && blocksOther[i] == 0)
                    continue;

                int score = blocksOther[i] * 100 + blockedByOther[i] * 35 - tileCounts[i];
                if (score <= bestScore)
                    continue;

                bestScore = score;
                best = i;
            }

            return best;
        }

        private static void BuildCellMaps(AuthoredLevelData authored, List<ChainMeta> metas, out Dictionary<int, int> ownerByCell, out Dictionary<int, int> chainByCell)
        {
            ownerByCell = new Dictionary<int, int>(authored.arrows.Count * 8);
            chainByCell = new Dictionary<int, int>(authored.arrows.Count * 8);
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var arrow = authored.arrows[i];
                if (arrow?.indices == null)
                    continue;

                for (int j = 0; j < arrow.indices.Count; j++)
                {
                    int idx = arrow.indices[j];
                    ownerByCell[idx] = metas[i].SourceIndex;
                    chainByCell[idx] = i;
                }
            }
        }

        private static int FindHeadRayBlocker(int width, int height, AuthoredArrowData arrow, Dictionary<int, int> ownerByCell, Dictionary<int, int> chainByCell, int owner)
        {
            int head = arrow.indices[0];
            int neck = arrow.indices[1];
            int hx = head % width;
            int hy = head / width;
            int nx = neck % width;
            int ny = neck / width;
            int dx = Math.Sign(hx - nx);
            int dy = Math.Sign(hy - ny);
            if (Mathf.Abs(dx) + Mathf.Abs(dy) != 1)
                return -1;

            int x = hx + dx;
            int y = hy + dy;
            while ((uint)x < (uint)width && (uint)y < (uint)height)
            {
                int idx = x + y * width;
                if (ownerByCell.TryGetValue(idx, out int blockerOwner))
                {
                    if (blockerOwner != owner && chainByCell.TryGetValue(idx, out int blocker))
                        return blocker;
                }

                x += dx;
                y += dy;
            }

            return -1;
        }

        private static bool AppendTranslated(SourceSeed source, Vector2Int offset, AuthoredLevelData target, HashSet<int> occupied, out string details)
        {
            return AppendTranslated(source, offset, target, occupied, null, -1, out details);
        }

        private static bool AppendTranslated(SourceSeed source, Vector2Int offset, AuthoredLevelData target, HashSet<int> occupied, List<ChainMeta> metas, int sourceIndex, out string details)
        {
            details = string.Empty;
            for (int i = 0; i < source.Def.authoredLevel.arrows.Count; i++)
            {
                var arrow = source.Def.authoredLevel.arrows[i];
                if (arrow?.indices == null || arrow.indices.Count < 2)
                    continue;

                var translated = new List<int>(arrow.indices.Count);
                for (int j = 0; j < arrow.indices.Count; j++)
                {
                    int idx = arrow.indices[j];
                    int sx = idx % source.Width;
                    int sy = idx / source.Width;
                    int tx = sx + offset.x;
                    int ty = sy + offset.y;
                    if (tx < 0 || ty < 0 || tx >= target.width || ty >= target.height)
                    {
                        details = $"out of bounds {source.Name} chain={i}";
                        return false;
                    }

                    int targetIdx = tx + ty * target.width;
                    if (occupied.Contains(targetIdx))
                    {
                        details = $"overlap {source.Name} chain={i} idx={targetIdx}";
                        return false;
                    }

                    translated.Add(targetIdx);
                }

                for (int j = 0; j < translated.Count; j++)
                    occupied.Add(translated[j]);

                target.arrows.Add(new AuthoredArrowData
                {
                    indices = translated,
                    colorIndex = target.arrows.Count % 16
                });

                metas?.Add(new ChainMeta
                {
                    SourceIndex = sourceIndex,
                    TileCount = translated.Count
                });
            }

            return true;
        }

        private static void SavePack(List<LevelDefinition> levels)
        {
            SavePackAt(PackPath, "composite_seed_variants_ab_50", "Composite Seed Variants AB 50", levels);
        }

        private static LevelPack SavePackAt(string path, string packId, string displayName, List<LevelDefinition> levels)
        {
            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(path);
            bool isNew = pack == null;
            if (pack == null)
                pack = ScriptableObject.CreateInstance<LevelPack>();

            pack.packId = packId;
            pack.displayName = displayName;
            pack.levels = levels.ToArray();
            EditorUtility.SetDirty(pack);
            if (isNew)
                AssetDatabase.CreateAsset(pack, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(path);
            return pack;
        }

        private static void AttachPackToDemo(LevelPack pack, string logTag)
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
            Debug.Log($"[{logTag}] Attached latest pack to demo: {AssetDatabase.GetAssetPath(pack)}");
        }

        private static Dictionary<string, LevelRef> LoadLevelDefinitionsByFileName()
        {
            var result = new Dictionary<string, LevelRef>(StringComparer.OrdinalIgnoreCase);
            string[] guids = AssetDatabase.FindAssets("t:LevelDefinition", new[] { SeedRoot, OutputFolder });
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                if (string.IsNullOrWhiteSpace(path))
                    continue;

                var def = AssetDatabase.LoadAssetAtPath<LevelDefinition>(path);
                if (def == null)
                    continue;

                string fileName = Path.GetFileNameWithoutExtension(path);
                if (!result.ContainsKey(fileName))
                    result.Add(fileName, new LevelRef { Asset = def, Path = path });
            }

            return result;
        }

        private static List<ReportRow> LoadReportRows(string reportFullPath)
        {
            var rows = new List<ReportRow>();
            string[] lines = File.ReadAllLines(reportFullPath);
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                string[] parts = lines[i].Split(',');
                if (parts.Length < 5)
                    continue;

                rows.Add(new ReportRow
                {
                    Id = parts[1],
                    Path = parts[2],
                    SourceA = parts[3],
                    SourceB = parts[4]
                });
            }

            return rows;
        }

        private static void AppendCompareLevel(List<LevelDefinition> levels, List<string> report, int group, string kind, LevelDefinition level, string path, ReportRow row)
        {
            levels.Add(level);
            report.Add($"{levels.Count},{group},{kind},{level.levelId},{path},{row.SourceA},{row.SourceB},{row.Id}");
        }

        private static void AppendRawCompareLevel(
            List<LevelDefinition> levels,
            List<string> report,
            int group,
            string kind,
            LevelDefinition level,
            string path,
            Plan plan,
            LevelDefinition variant,
            string variantPath,
            int crossBlocks)
        {
            levels.Add(level);
            int chains = variant.authoredLevel?.arrows?.Count ?? 0;
            float coverage = CountTiles(variant.authoredLevel) / (float)Mathf.Max(1, variant.authoredLevel.width * variant.authoredLevel.height);
            report.Add($"{levels.Count},{group},{kind},{level.levelId},{path},{plan.A.Name},{plan.B.Name},{variant.levelId},{LayoutLabel(plan)},{plan.Width},{plan.Height},{chains},{coverage:0.000},{crossBlocks}");
        }

        private static void AppendGreedyCompareLevel(
            List<LevelDefinition> levels,
            List<string> report,
            int group,
            string kind,
            LevelDefinition level,
            string path,
            Plan plan,
            LevelDefinition variant,
            string variantPath,
            int crossBlocks,
            string repair)
        {
            levels.Add(level);
            int chains = variant.authoredLevel?.arrows?.Count ?? 0;
            int width = variant.authoredLevel?.width ?? plan.Width;
            int height = variant.authoredLevel?.height ?? plan.Height;
            float coverage = CountTiles(variant.authoredLevel) / (float)Mathf.Max(1, width * height);
            report.Add($"{levels.Count},{group},{kind},{level.levelId},{path},{plan.A.Name},{plan.B.Name},{variant.levelId},{LayoutLabel(plan)},{width},{height},{chains},{coverage:0.000},{crossBlocks},True,{EscapeCsv(repair)}");
        }

        private static void ClearOutputFolder()
        {
            ClearFolder(OutputFolder);
        }

        private static void ClearFolder(string folder)
        {
            if (!AssetDatabase.IsValidFolder(folder))
                return;

            string[] guids = AssetDatabase.FindAssets("t:LevelDefinition", new[] { folder });
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                if (!string.IsNullOrWhiteSpace(path) && path.StartsWith(folder, StringComparison.OrdinalIgnoreCase))
                    AssetDatabase.DeleteAsset(path);
            }
        }

        private static void WriteReport(List<string> lines)
        {
            WriteLines(ReportPath, lines);
        }

        private static void WriteLines(string projectRelativePath, List<string> lines)
        {
            string full = ProjectRelativeToFullPath(projectRelativePath);
            string dir = Path.GetDirectoryName(full);
            if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            File.WriteAllText(full, string.Join("\n", lines));
            Debug.Log($"[CompositeSeedVariant] Wrote report path={projectRelativePath}, fullPath={full}, lines={lines.Count}");
        }

        private static string ProjectRelativeToFullPath(string projectRelativePath)
        {
            return Path.GetFullPath(Path.Combine(Application.dataPath, "..", projectRelativePath));
        }

        private static HashSet<string> LoadPlanKeySet(string projectRelativePath)
        {
            var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (string.IsNullOrWhiteSpace(projectRelativePath))
                return result;

            string full = ProjectRelativeToFullPath(projectRelativePath);
            if (!File.Exists(full))
                return result;

            string[] lines = File.ReadAllLines(full);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i]?.Trim();
                if (!string.IsNullOrWhiteSpace(line))
                    result.Add(line);
            }

            return result;
        }

        private static void SavePlanKeySet(string projectRelativePath, HashSet<string> keys)
        {
            var lines = new List<string>();
            if (keys != null)
            {
                foreach (string key in keys)
                {
                    if (!string.IsNullOrWhiteSpace(key))
                        lines.Add(key);
                }
            }

            lines.Sort(StringComparer.OrdinalIgnoreCase);
            WriteLines(projectRelativePath, lines);
        }

        private static int CountTiles(AuthoredLevelData authored)
        {
            int total = 0;
            if (authored?.arrows == null)
                return total;
            for (int i = 0; i < authored.arrows.Count; i++)
                total += authored.arrows[i]?.indices?.Count ?? 0;
            return total;
        }

        private static void CountHeadDirections(AuthoredLevelData authored, out int left, out int right, out int up, out int down)
        {
            left = 0;
            right = 0;
            up = 0;
            down = 0;

            if (authored?.arrows == null)
                return;

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                Dir dir = GetHeadDir(authored.arrows[i], authored.width);
                switch (dir)
                {
                    case Dir.Left:
                        left++;
                        break;
                    case Dir.Right:
                        right++;
                        break;
                    case Dir.Up:
                        up++;
                        break;
                    case Dir.Down:
                        down++;
                        break;
                }
            }
        }

        private static Dir GetHeadDir(AuthoredArrowData arrow, int width)
        {
            if (arrow?.indices == null || arrow.indices.Count < 2)
                return Dir.Right;

            int head = arrow.indices[0];
            int neck = arrow.indices[1];
            int hx = head % width;
            int hy = head / width;
            int nx = neck % width;
            int ny = neck / width;
            int dx = Math.Sign(hx - nx);
            int dy = Math.Sign(hy - ny);
            if (dx > 0) return Dir.Right;
            if (dx < 0) return Dir.Left;
            if (dy > 0) return Dir.Up;
            return Dir.Down;
        }

        private static string MakePairKey(Plan plan)
        {
            return $"{plan.A.Name}|{plan.B.Name}|{(plan.Horizontal ? "h" : "v")}|g{plan.Gap}";
        }

        private static string MakeGreedyVariantSuffix(Plan plan)
        {
            return $"{ShortName(plan.A.Name)}_{ShortName(plan.B.Name)}_{LayoutLabel(plan).ToLowerInvariant()}";
        }

        private static HashSet<string> LoadExistingGreedyVariantSuffixes(string outputFolder, out int assetCount)
        {
            var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            assetCount = 0;
            var sourcePaths = new List<string>();
            AddGeneratedCompositeSources(sourcePaths, outputFolder);

            for (int i = 0; i < sourcePaths.Count; i++)
            {
                string sourcePath = sourcePaths[i];
                var source = AssetDatabase.LoadAssetAtPath<LevelDefinition>(sourcePath);
                if (source?.authoredLevel?.arrows == null)
                    continue;

                assetCount++;
                string fileName = Path.GetFileNameWithoutExtension(sourcePath);
                if (TryExtractGreedyVariantSuffix(fileName, out string suffix))
                    result.Add(suffix);
            }

            return result;
        }

        private static bool TryExtractGreedyVariantSuffix(string fileName, out string suffix)
        {
            suffix = string.Empty;
            const string prefix = "composite_greedy_ab_";
            if (string.IsNullOrWhiteSpace(fileName) || !fileName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return false;

            string rest = fileName.Substring(prefix.Length);
            int firstUnderscore = rest.IndexOf('_');
            if (firstUnderscore < 0 || firstUnderscore + 1 >= rest.Length)
                return false;

            suffix = rest.Substring(firstUnderscore + 1);
            return true;
        }

        private static string MakeBasePairKey(Plan plan)
        {
            string a = MakeBaseSourceKey(plan.A);
            string b = MakeBaseSourceKey(plan.B);
            if (string.Compare(a, b, StringComparison.OrdinalIgnoreCase) > 0)
            {
                string temp = a;
                a = b;
                b = temp;
            }

            return $"{a}|{b}|{(plan.Horizontal ? "h" : "v")}";
        }

        private static string MakeBaseSourceKey(SourceSeed source)
        {
            string name = source?.Name ?? string.Empty;
            string tier = source?.SourceTier switch
            {
                0 => "r2",
                1 => "r1",
                _ => "seed"
            };
            var r1 = ShortR1Name.Match(name.ToLowerInvariant());
            if (r1.Success)
                return $"{tier}:a{r1.Groups[2].Value}";

            var level = BaseLevelNumberName.Match(name);
            if (level.Success)
                return $"{tier}:a{level.Groups[1].Value}";

            return $"{tier}:{name.ToLowerInvariant()}";
        }

        private static string MakeDiversitySourceKey(SourceSeed source, ParentDiversityOptions diversity)
        {
            return diversity != null && diversity.UseFamilySourceLimit
                ? MakeSourceFamilyKey(source)
                : MakeBaseSourceKey(source);
        }

        private static string MakeSourceFamilyKey(SourceSeed source)
        {
            string name = source?.Name ?? string.Empty;
            string lower = name.ToLowerInvariant();
            var r1 = ShortR1Name.Match(lower);
            if (r1.Success)
                return $"a{r1.Groups[2].Value}";

            var level = BaseLevelNumberName.Match(lower);
            if (level.Success)
                return $"a{level.Groups[1].Value}";

            return lower;
        }

        private static List<string> ExtractSourceFamiliesFromName(string name)
        {
            var families = new List<string>();
            if (string.IsNullOrWhiteSpace(name))
                return families;

            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var matches = CompositeFamilyToken.Matches(name);
            for (int i = 0; i < matches.Count; i++)
            {
                string family = $"a{matches[i].Groups[1].Value}";
                if (seen.Add(family))
                    families.Add(family);
            }

            if (families.Count == 0)
            {
                string lower = name.ToLowerInvariant();
                var r1 = ShortR1Name.Match(lower);
                if (r1.Success)
                    families.Add($"a{r1.Groups[2].Value}");
                else
                {
                    var level = BaseLevelNumberName.Match(lower);
                    families.Add(level.Success ? $"a{level.Groups[1].Value}" : lower);
                }
            }

            return families;
        }

        private static bool SourceFamiliesBelowLimit(List<string> families, Dictionary<string, int> counts, int maxUse)
        {
            if (maxUse <= 0 || families == null || families.Count == 0)
                return true;

            for (int i = 0; i < families.Count; i++)
            {
                string family = families[i];
                if (!string.IsNullOrWhiteSpace(family) &&
                    counts.TryGetValue(family, out int count) &&
                    count >= maxUse)
                    return false;
            }

            return true;
        }

        private static void RecordSourceFamilies(List<string> families, Dictionary<string, int> counts)
        {
            if (families == null || counts == null)
                return;

            for (int i = 0; i < families.Count; i++)
                IncrementCount(counts, families[i]);
        }

        private static string ShortName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "seed";

            string lower = name.ToLowerInvariant();
            var r1 = ShortR1Name.Match(lower);
            if (r1.Success)
            {
                string prefix = lower.Contains("rotation") ? "r2" : "r1";
                return $"{prefix}_{r1.Groups[1].Value}_a{r1.Groups[2].Value}";
            }

            string shortName = name
                .Replace("seed_", string.Empty)
                .Replace("Above300_level_", "a")
                .Replace("above300_level_", "a")
                .Replace("Arrowz_level_", "z")
                .ToLowerInvariant();
            shortName = Regex.Replace(shortName, @"[^a-z0-9_]+", "_");
            return shortName.Length <= 48 ? shortName : shortName.Substring(0, 48);
        }

        private static string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            if (value.IndexOfAny(new[] { ',', '"', '\n', '\r' }) < 0)
                return value;

            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        private static void EnsureFolder(string assetFolder)
        {
            if (string.IsNullOrWhiteSpace(assetFolder))
                return;

            assetFolder = assetFolder.Replace("\\", "/").Trim('/');
            string[] parts = assetFolder.Split('/');
            if (parts.Length == 0)
                return;

            string current = parts[0].Equals("Assets", StringComparison.OrdinalIgnoreCase) ? "Assets" : parts[0];
            for (int i = 1; i < parts.Length; i++)
            {
                string next = $"{current}/{parts[i]}";
                if (!AssetDatabase.IsValidFolder(next))
                    AssetDatabase.CreateFolder(current, parts[i]);
                current = next;
            }
        }
    }
}
#endif
