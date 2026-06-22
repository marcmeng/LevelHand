#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using PixelBug.ArrowLevelGenerator;
using UnityEditor;
using UnityEngine;

namespace PixelBug.ArrowMagic.EditorTools
{
    public static class NoMaskProceduralGenerator
    {
        private const string DemoScenePath = "Assets/ArrowMagic/Scenes/Demo.unity";
        private const string OutputFolder = "Assets/ArrowMagic/SOData/Levels/NoMaskProcedural/TypePreview";
        private const string PackPath = "Assets/ArrowMagic/SOData/Packs/NoMaskProcedural/NoMaskTypePreviewPack.asset";
        private const string ReportPath = "Assets/ArrowMagic/SOData/Reports/NoMaskProcedural/no_mask_type_preview_report.csv";
        private const string StyleOutputFolder = "Assets/ArrowMagic/SOData/Levels/NoMaskProcedural/StylePreview";
        private const string StylePackPath = "Assets/ArrowMagic/SOData/Packs/NoMaskProcedural/NoMaskStylePreviewPack.asset";
        private const string StyleReportPath = "Assets/ArrowMagic/SOData/Reports/NoMaskProcedural/no_mask_style_preview_report.csv";
        private const string StyleExpansionOutputFolder = "Assets/ArrowMagic/SOData/Levels/NoMaskProcedural/StyleExpansionPreview";
        private const string StyleExpansionPackPath = "Assets/ArrowMagic/SOData/Packs/NoMaskProcedural/NoMaskStyleExpansionPreviewPack.asset";
        private const string StyleExpansionReportPath = "Assets/ArrowMagic/SOData/Reports/NoMaskProcedural/no_mask_style_expansion_preview_report.csv";
        private const string Level3TinyOutputFolder = "Assets/ArrowMagic/SOData/Levels/NoMaskProcedural/Level3TinyPreview";
        private const string Level3TinyPackPath = "Assets/ArrowMagic/SOData/Packs/NoMaskProcedural/Level3TinyPreviewPack.asset";
        private const string Level3TinyReportPath = "Assets/ArrowMagic/SOData/Reports/NoMaskProcedural/level3_tiny_preview_report.csv";
        private const string Level3ThirtyOutputFolder = "Assets/ArrowMagic/SOData/Levels/NoMaskProcedural/Level3ThirtyPreview";
        private const string Level3ThirtyPackPath = "Assets/ArrowMagic/SOData/Packs/NoMaskProcedural/Level3ThirtyPreviewPack.asset";
        private const string Level3ThirtyReportPath = "Assets/ArrowMagic/SOData/Reports/NoMaskProcedural/level3_thirty_preview_report.csv";
        private const string DirectHoleFrontOutputFolder = "Assets/ArrowMagic/SOData/Levels/HoleProcedural/FrontPreview";
        private const string DirectHoleFrontPackPath = "Assets/ArrowMagic/SOData/Packs/HoleProcedural/HoleFrontPreviewPack.asset";
        private const string DirectHoleFrontReportPath = "Assets/ArrowMagic/SOData/Reports/HoleProcedural/hole_front_preview_report.csv";
        private const string DirectHoleCandidateOutputFolder = "Assets/ArrowMagic/SOData/Levels/HoleProcedural/CandidatePool";
        private const string DirectHoleCandidatePackPath = "Assets/ArrowMagic/SOData/Packs/HoleProcedural/HoleCandidatePoolPack.asset";
        private const string DirectHoleCandidateReportPath = "Assets/ArrowMagic/SOData/Reports/HoleProcedural/hole_candidate_pool_report.csv";
        private const string DirectHoleTopupOutputFolder = "Assets/ArrowMagic/SOData/Levels/HoleProcedural/TopupCandidates";
        private const string DirectHoleTopupPackPath = "Assets/ArrowMagic/SOData/Packs/HoleProcedural/HoleTopupCandidatesPack.asset";
        private const string DirectHoleTopupReportPath = "Assets/ArrowMagic/SOData/Reports/HoleProcedural/hole_topup_candidates_report.csv";
        private const string DirectHoleCombinedPackPath = "Assets/ArrowMagic/SOData/Packs/HoleProcedural/HoleCombinedCandidatesPack.asset";
        private const string DirectHoleHighChainOutputFolder = "Assets/ArrowMagic/SOData/Levels/HoleProcedural/HighChainCandidates";
        private const string DirectHoleHighChainPackPath = "Assets/ArrowMagic/SOData/Packs/HoleProcedural/HoleHighChainCandidatesPack.asset";
        private const string DirectHoleHighChainReportPath = "Assets/ArrowMagic/SOData/Reports/HoleProcedural/hole_high_chain_candidates_report.csv";
        private const string DirectHoleHighChainExtraReportPath = "Assets/ArrowMagic/SOData/Reports/HoleProcedural/hole_high_chain_extra_candidates_report.csv";
        private const string DirectNormalOutputFolder = "Assets/ArrowMagic/SOData/Levels/DirectProcedural/NormalCandidates";
        private const string DirectNormalPackPath = "Assets/ArrowMagic/SOData/Packs/DirectProcedural/DirectNormalCandidatesPack.asset";
        private const string DirectNormalReportPath = "Assets/ArrowMagic/SOData/Reports/DirectProcedural/direct_normal_candidates_report.csv";
        private const string DirectNormalTopupOutputFolder = "Assets/ArrowMagic/SOData/Levels/DirectProcedural/NormalTopupCandidates";
        private const string DirectNormalTopupPackPath = "Assets/ArrowMagic/SOData/Packs/DirectProcedural/DirectNormalTopupCandidatesPack.asset";
        private const string DirectNormalTopupReportPath = "Assets/ArrowMagic/SOData/Reports/DirectProcedural/direct_normal_topup_candidates_report.csv";
        private const string DirectAdvancedOutputFolder = "Assets/ArrowMagic/SOData/Levels/DirectProcedural/AdvancedCandidates";
        private const string DirectAdvancedPackPath = "Assets/ArrowMagic/SOData/Packs/DirectProcedural/DirectAdvancedCandidatesPack.asset";
        private const string DirectAdvancedReportPath = "Assets/ArrowMagic/SOData/Reports/DirectProcedural/direct_advanced_candidates_report.csv";
        private const string DirectPolishOutputFolder = "Assets/ArrowMagic/SOData/Levels/DirectProcedural/PolishCandidates";
        private const string DirectPolishPackPath = "Assets/ArrowMagic/SOData/Packs/DirectProcedural/DirectPolishCandidatesPack.asset";
        private const string DirectPolishReportPath = "Assets/ArrowMagic/SOData/Reports/DirectProcedural/direct_polish_candidates_report.csv";
        private const string DirectPolishCleanOutputFolder = "Assets/ArrowMagic/SOData/Levels/DirectProcedural/PolishCleanCandidates";
        private const string DirectPolishCleanPackPath = "Assets/ArrowMagic/SOData/Packs/DirectProcedural/DirectPolishCleanCandidatesPack.asset";
        private const string DirectPolishCleanReportPath = "Assets/ArrowMagic/SOData/Reports/DirectProcedural/direct_polish_clean_candidates_report.csv";
        private const string DirectPolishHardOutputFolder = "Assets/ArrowMagic/SOData/Levels/DirectProcedural/PolishHardCandidates";
        private const string DirectPolishHardPackPath = "Assets/ArrowMagic/SOData/Packs/DirectProcedural/DirectPolishHardCandidatesPack.asset";
        private const string DirectPolishHardReportPath = "Assets/ArrowMagic/SOData/Reports/DirectProcedural/direct_polish_hard_candidates_report.csv";
        private const string DirectPolishExtremeOutputFolder = "Assets/ArrowMagic/SOData/Levels/DirectProcedural/PolishExtremeLongCandidates";
        private const string DirectPolishExtremePackPath = "Assets/ArrowMagic/SOData/Packs/DirectProcedural/DirectPolishExtremeLongCandidatesPack.asset";
        private const string DirectPolishExtremeReportPath = "Assets/ArrowMagic/SOData/Reports/DirectProcedural/direct_polish_extreme_long_candidates_report.csv";
        private const string DirectDifferentiationOutputFolder = "Assets/ArrowMagic/SOData/Levels/DirectProcedural/Differentiation120Candidates";
        private const string DirectDifferentiationPackPath = "Assets/ArrowMagic/SOData/Packs/DirectProcedural/DirectDifferentiation120CandidatesPack.asset";
        private const string DirectDifferentiationReportPath = "Assets/ArrowMagic/SOData/Reports/DirectProcedural/direct_differentiation120_candidates_report.csv";
        private const int DirectDifferentiationTargetCount = 120;
        private const int DirectDifferentiationBandQuota = 30;
        private const string DirectStructurePreviewOutputFolder = "Assets/ArrowMagic/SOData/Levels/DirectProcedural/StructurePreview16";
        private const string DirectStructurePreviewPackPath = "Assets/ArrowMagic/SOData/Packs/DirectProcedural/DirectStructurePreview16Pack.asset";
        private const string DirectStructurePreviewReportPath = "Assets/ArrowMagic/SOData/Reports/DirectProcedural/direct_structure_preview16_report.csv";
        private const int DirectStructurePreviewTargetPerKind = 2;
        private const int DirectStructurePreviewKindCount = 8;
        private const string DirectArchitecturePreviewOutputFolder = "Assets/ArrowMagic/SOData/Levels/DirectProcedural/ArchitecturePreview";
        private const string DirectArchitecturePreviewPackPath = "Assets/ArrowMagic/SOData/Packs/DirectProcedural/DirectArchitecturePreviewPack.asset";
        private const string DirectArchitecturePreviewReportPath = "Assets/ArrowMagic/SOData/Reports/DirectProcedural/direct_architecture_preview_report.csv";
        private const int DirectArchitecturePreviewTargetPerKind = 2;
        private const int DirectArchitecturePreviewKindCount = 4;
        private const string DirectPureTopupOutputRoot = "Assets/ArrowMagic/SOData/Levels/DirectProcedural/PureTopupCandidates";
        private const string DirectPureTopupPackRoot = "Assets/ArrowMagic/SOData/Packs/DirectProcedural";
        private const string DirectPureTopupReportRoot = "Assets/ArrowMagic/SOData/Reports/DirectProcedural";

        private enum NoMaskType
        {
            OuterShell,
            SectionUnlock,
            LockBuckle,
            MazeLongChain,
            DenseWeave,
            Sweep,
            CoreBurst,
            DualZone,
            StairPush,
            QuasiSymmetry
        }

        private enum NoMaskMotifKind
        {
            HuiSpiral,
            SnakeSpine,
            DoubleRoomLock,
            CenterCross,
            DoubleShell,
            StairLadder,
            KeyDoor,
            FourPockets,
            VerticalGate,
            ZigRiver,
            DenseKernel,
            LongCorridor,
            CircuitBoard,
            ParallelHighway,
            RoomCorridor,
            NestedRooms
        }

        private sealed class StyleSpec
        {
            public NoMaskType Type;
            public string Id;
            public string DisplayName;
            public int Width;
            public int Height;
            public int TargetChains;
            public int MinLength;
            public int MaxLength;
            public float TargetCoverage;
            public float TurnBias;
            public float BlockWeight;
            public float EdgeOpeningBias;
            public float OuterBandTarget;
            public int MaxInitialMovableChains;
            public int MaxShortEdgePatchChains;
            public int MaxShortFillPatchChains;
            public int Seed;
            public int MotifVariant;
            public bool MotifMirrorX;
            public bool MotifMirrorY;
            public int OuterCutKind;
        }

        private sealed class MotifSpec
        {
            public StyleSpec Style;
            public NoMaskMotifKind Motif;
            public string Id;
            public string DisplayName;
            public int Seed;
        }

        private sealed class ChainCandidate
        {
            public readonly List<Vector2Int> HeadToTail = new();
            public Dir OutDir;
            public int BlocksExisting;
            public int Turns;
            public float Score;
        }

        private sealed class TailExtensionCandidate
        {
            public int ChainIndex;
            public Vector2Int Cell;
            public float Score;
        }

        private sealed class PeelHeadCandidate
        {
            public int Head;
            public int Second;
            public Dir OutDir;
            public int Score;
        }

        private sealed class PeelStyleProfile
        {
            public readonly string Id;
            public readonly string DisplayName;
            public readonly int SeedSalt;
            public readonly int Attempts;
            public readonly int MinLengthAdd;
            public readonly int MaxLengthAdd;
            public readonly int ShortRollPercent;
            public readonly int LongRollPercent;
            public readonly int LongMinAdd;
            public readonly int HeadPickWindow;
            public readonly int HeadBoardEdgeBonus;
            public readonly int HeadBoundaryBonus;
            public readonly int HeadClearRayMultiplier;
            public readonly int HeadNeighborPenalty;
            public readonly int CenterHeadBonus;
            public readonly int SameDirectionEarlyBonus;
            public readonly int SameDirectionLaterBonus;
            public readonly int TurnEarlyBonus;
            public readonly int TurnLaterBonus;
            public readonly int BoundaryStepBonus;
            public readonly int SparseNeighborWeight;
            public readonly int CenterStepBonus;
            public readonly float TargetInitialRatio;
            public readonly int TargetInitialMin;
            public readonly int TargetInitialMax;
            public readonly float OpeningCeiling;
            public readonly int ChainCountPenalty;
            public readonly int StraightPenalty;

            public PeelStyleProfile(
                string id,
                string displayName,
                int seedSalt,
                int attempts,
                int minLengthAdd,
                int maxLengthAdd,
                int shortRollPercent,
                int longRollPercent,
                int longMinAdd,
                int headPickWindow,
                int headBoardEdgeBonus,
                int headBoundaryBonus,
                int headClearRayMultiplier,
                int headNeighborPenalty,
                int centerHeadBonus,
                int sameDirectionEarlyBonus,
                int sameDirectionLaterBonus,
                int turnEarlyBonus,
                int turnLaterBonus,
                int boundaryStepBonus,
                int sparseNeighborWeight,
                int centerStepBonus,
                float targetInitialRatio,
                int targetInitialMin,
                int targetInitialMax,
                float openingCeiling,
                int chainCountPenalty,
                int straightPenalty)
            {
                Id = id;
                DisplayName = displayName;
                SeedSalt = seedSalt;
                Attempts = attempts;
                MinLengthAdd = minLengthAdd;
                MaxLengthAdd = maxLengthAdd;
                ShortRollPercent = shortRollPercent;
                LongRollPercent = longRollPercent;
                LongMinAdd = longMinAdd;
                HeadPickWindow = headPickWindow;
                HeadBoardEdgeBonus = headBoardEdgeBonus;
                HeadBoundaryBonus = headBoundaryBonus;
                HeadClearRayMultiplier = headClearRayMultiplier;
                HeadNeighborPenalty = headNeighborPenalty;
                CenterHeadBonus = centerHeadBonus;
                SameDirectionEarlyBonus = sameDirectionEarlyBonus;
                SameDirectionLaterBonus = sameDirectionLaterBonus;
                TurnEarlyBonus = turnEarlyBonus;
                TurnLaterBonus = turnLaterBonus;
                BoundaryStepBonus = boundaryStepBonus;
                SparseNeighborWeight = sparseNeighborWeight;
                CenterStepBonus = centerStepBonus;
                TargetInitialRatio = targetInitialRatio;
                TargetInitialMin = targetInitialMin;
                TargetInitialMax = targetInitialMax;
                OpeningCeiling = openingCeiling;
                ChainCountPenalty = chainCountPenalty;
                StraightPenalty = straightPenalty;
            }
        }

        private sealed class PlacedChain
        {
            public readonly List<Vector2Int> HeadToTail = new();
            public readonly HashSet<int> EscapeRay = new();
            public Dir OutDir;
        }

        private sealed class BuildMetrics
        {
            public int Attempts;
            public int ArrowCount;
            public int Chains;
            public int InitialMovableChains;
            public int GreedyMoves;
            public int BlockLinks;
            public int EdgeHeadChains;
            public int ShortEdgeChains;
            public int MaxChainLength;
            public float AverageChainLength;
            public float Coverage;
            public float OuterBandCoverage;
            public float Straightness;
            public bool PortableGreedySolved;
            public int PortableInitialOpeners;
            public float PortableScore;
            public string PortableQualityFlags = "not-run";
        }

        [MenuItem("Tools/ArrowMagic/No Mask Procedural/Build Type Preview Pack")]
        public static void BuildTypePreviewPack()
        {
            EnsureFolderExists(OutputFolder);
            EnsureFolderExists(Path.GetDirectoryName(PackPath)?.Replace("\\", "/"));
            EnsureFolderExists(Path.GetDirectoryName(ReportPath)?.Replace("\\", "/"));

            var levels = new List<LevelDefinition>();
            var report = new List<string>
            {
                "Index,Type,LevelId,AssetPath,Width,Height,Chains,Arrows,Coverage,TargetCoverage,OuterBandCoverage,InitialMovableChains,EdgeHeadChains,ShortChains,GreedyMoves,AvgChain,MaxChain,Straightness,BlockLinks,PortableSolved,PortableOpeners,PortableScore,PortableQuality,Attempts,Status"
            };

            StyleSpec[] specs = CreateSpecs();
            for (int i = 0; i < specs.Length; i++)
            {
                StyleSpec spec = specs[i];
                Debug.Log($"[NoMaskProcedural] Building {i + 1}/{specs.Length}: {spec.Id}");
                if (!TryBuildLevel(spec, i + 1, out LevelDefinition level, out string assetPath, out BuildMetrics metrics, out string status))
                {
                    report.Add($"{i + 1},{spec.Id},,,{spec.Width},{spec.Height},0,0,0,{spec.TargetCoverage:0.000},0,0,0,0,0,0,0,0,0,False,0,0,fail,0,{EscapeCsv(status)}");
                    File.WriteAllLines(ReportPath, report);
                    Debug.LogWarning($"[NoMaskProcedural] Failed {spec.Id}: {status}");
                    continue;
                }

                levels.Add(level);
                Debug.Log($"[NoMaskProcedural] Built {spec.Id}: chains={metrics.Chains}, coverage={metrics.Coverage:0.000}, outer={metrics.OuterBandCoverage:0.000}, initial={metrics.InitialMovableChains}, edgeHeads={metrics.EdgeHeadChains}");
                report.Add(
                    $"{i + 1},{spec.Id},{EscapeCsv(level.levelId)},{EscapeCsv(assetPath)},{spec.Width},{spec.Height}," +
                    $"{metrics.Chains},{metrics.ArrowCount},{metrics.Coverage:0.000},{spec.TargetCoverage:0.000},{metrics.OuterBandCoverage:0.000}," +
                    $"{metrics.InitialMovableChains},{metrics.EdgeHeadChains},{metrics.ShortEdgeChains},{metrics.GreedyMoves}," +
                    $"{metrics.AverageChainLength:0.00},{metrics.MaxChainLength},{metrics.Straightness:0.000}," +
                    $"{metrics.BlockLinks},{metrics.PortableGreedySolved},{metrics.PortableInitialOpeners},{metrics.PortableScore:0.0},{EscapeCsv(metrics.PortableQualityFlags)}," +
                    $"{metrics.Attempts},ok");
                File.WriteAllLines(ReportPath, report);
            }

            File.WriteAllLines(ReportPath, report);
            AssetDatabase.ImportAsset(ReportPath);

            LevelPack pack = SavePack(levels);
            AttachPackToDemo(pack, "NoMaskProcedural");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"[NoMaskProcedural] Type preview finished. levels={levels.Count}, pack={PackPath}, report={ReportPath}");
        }

        [MenuItem("Tools/ArrowMagic/No Mask Procedural/Build Style Preview Pack")]
        public static void BuildStylePreviewPack()
        {
            BuildMotifPreviewPack(
                CreateMotifSpecs(),
                StyleOutputFolder,
                StylePackPath,
                StyleReportPath,
                "nomask_style",
                "no_mask_style_preview",
                "No Mask Style Preview",
                "NoMaskProceduralStyle");
        }

        [MenuItem("Tools/ArrowMagic/No Mask Procedural/Build Style Expansion Preview Pack")]
        public static void BuildStyleExpansionPreviewPack()
        {
            BuildMotifPreviewPack(
                CreateMotifExpansionSpecs(),
                StyleExpansionOutputFolder,
                StyleExpansionPackPath,
                StyleExpansionReportPath,
                "nomask_style_expansion",
                "no_mask_style_expansion_preview",
                "No Mask Style Expansion Preview",
                "NoMaskProceduralStyleExpansion");
        }

        [MenuItem("Tools/ArrowMagic/No Mask Procedural/Build Level 3 Tiny Preview Pack")]
        public static void BuildLevel3TinyPreviewPack()
        {
            BuildStyleSpecPack(
                CreateLevel3TinySpecs(),
                Level3TinyOutputFolder,
                Level3TinyPackPath,
                Level3TinyReportPath,
                "level3_tiny",
                "level3_tiny_preview",
                "Level 3 Tiny Preview",
                "Level3TinyPreview");
        }

        [MenuItem("Tools/ArrowMagic/No Mask Procedural/Build Level 3 Thirty Preview Pack")]
        public static void BuildLevel3ThirtyPreviewPack()
        {
            BuildStyleSpecPack(
                CreateLevel3ThirtySpecs(),
                Level3ThirtyOutputFolder,
                Level3ThirtyPackPath,
                Level3ThirtyReportPath,
                "level3_thirty",
                "level3_thirty_preview",
                "Level 3 Thirty Preview",
                "Level3ThirtyPreview");
        }

        [MenuItem("Tools/ArrowMagic/Hole Procedural/Build Front Preview Pack (Direct Logic)")]
        public static void BuildDirectHoleFrontPreviewPack()
        {
            EnsureFolderExists(DirectHoleFrontOutputFolder);
            EnsureFolderExists(Path.GetDirectoryName(DirectHoleFrontPackPath)?.Replace("\\", "/"));
            EnsureFolderExists(Path.GetDirectoryName(DirectHoleFrontReportPath)?.Replace("\\", "/"));

            StyleSpec[] specs = CreateDirectHoleFrontSpecs();
            var levels = new List<LevelDefinition>(specs.Length);
            var report = new List<string>
            {
                "Index,Spec,LevelId,AssetPath,Width,Height,Chains,Arrows,Blocks,AllowedCoverage,BoardCoverage,OuterBandCoverage,InitialMovableChains,EdgeHeadChains,ShortChains,GreedyMoves,AvgChain,MaxChain,Straightness,BlockLinks,PortableSolved,PortableOpeners,PortableScore,PortableQuality,Attempts,Status"
            };

            for (int i = 0; i < specs.Length; i++)
            {
                StyleSpec spec = specs[i];
                string levelId = $"hole_direct_front_{i + 1:00}_{spec.Id}";
                string assetPath = $"{DirectHoleFrontOutputFolder}/{levelId}.asset";
                if (!TryBuildDirectHoleLevelAt(spec, i + 1, assetPath, levelId, out LevelDefinition level, out BuildMetrics metrics, out int blockCount, out int allowedCount, out string status))
                {
                    report.Add($"{i + 1},{spec.Id},,{assetPath},{spec.Width},{spec.Height},0,0,{blockCount},0,0,0,0,0,0,0,0,0,0,0,False,0,0,,0,{EscapeCsv(status)}");
                    Debug.LogWarning($"[NoMaskProceduralGenerator] Direct hole failed {spec.Id}: {status}");
                    continue;
                }

                levels.Add(level);
                float boardCoverage = metrics.ArrowCount / (float)Mathf.Max(1, spec.Width * spec.Height);
                report.Add(
                    $"{i + 1},{spec.Id},{levelId},{assetPath},{spec.Width},{spec.Height},{metrics.Chains},{metrics.ArrowCount},{blockCount}," +
                    $"{metrics.Coverage:0.000},{boardCoverage:0.000},{metrics.OuterBandCoverage:0.000},{metrics.InitialMovableChains},{metrics.EdgeHeadChains},{metrics.ShortEdgeChains},{metrics.GreedyMoves}," +
                    $"{metrics.AverageChainLength:0.00},{metrics.MaxChainLength},{metrics.Straightness:0.000},{metrics.BlockLinks},{metrics.PortableGreedySolved},{metrics.PortableInitialOpeners}," +
                    $"{metrics.PortableScore:0.0},{EscapeCsv(metrics.PortableQualityFlags)},{metrics.Attempts},{EscapeCsv(status)}");
            }

            File.WriteAllLines(DirectHoleFrontReportPath, report);
            AssetDatabase.ImportAsset(DirectHoleFrontReportPath);

            LevelPack pack = SavePackAt(levels, DirectHoleFrontPackPath, "hole_front_preview", $"Hole Front Preview ({levels.Count})");
            AttachPackToDemo(pack, "DirectHoleFrontPreview");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"[NoMaskProceduralGenerator] Built direct hole front preview: levels={levels.Count}, pack={DirectHoleFrontPackPath}, report={DirectHoleFrontReportPath}");
        }

        [MenuItem("Tools/ArrowMagic/Hole Procedural/Build Candidate Pool Pack (Direct Logic)")]
        public static void BuildDirectHoleCandidatePoolPack()
        {
            EnsureFolderExists(DirectHoleCandidateOutputFolder);
            EnsureFolderExists(Path.GetDirectoryName(DirectHoleCandidatePackPath)?.Replace("\\", "/"));
            EnsureFolderExists(Path.GetDirectoryName(DirectHoleCandidateReportPath)?.Replace("\\", "/"));

            StyleSpec[] specs = CreateDirectHoleCandidatePoolSpecs();
            var levels = new List<LevelDefinition>(specs.Length);
            var report = new List<string>
            {
                "Index,Spec,Structure,LevelId,AssetPath,Width,Height,Chains,TargetChains,Arrows,Blocks,OuterCutCells,AllowedCells,AllowedCoverage,BoardCoverage,OuterBandCoverage,InitialMovableChains,EdgeHeadChains,ShortChains,GreedyMoves,AvgChain,MaxChain,Straightness,BlockLinks,PortableSolved,PortableOpeners,PortableScore,PortableQuality,Attempts,Status"
            };

            for (int i = 0; i < specs.Length; i++)
            {
                StyleSpec spec = specs[i];
                string levelId = $"hole_candidate_{i + 1:000}_{spec.Id}";
                string assetPath = $"{DirectHoleCandidateOutputFolder}/{levelId}.asset";
                Debug.Log($"[NoMaskProceduralGenerator] Building direct hole candidate {i + 1}/{specs.Length}: {spec.Id}");
                if (!TryBuildDirectHoleLevelAt(spec, i + 1, assetPath, levelId, out LevelDefinition level, out BuildMetrics metrics, out int blockCount, out int allowedCount, out string status))
                {
                    int outerCutCells = Mathf.Max(0, spec.Width * spec.Height - blockCount - allowedCount);
                    report.Add($"{i + 1},{spec.Id},{spec.Type},,{assetPath},{spec.Width},{spec.Height},0,{spec.TargetChains},0,{blockCount},{outerCutCells},{allowedCount},0,0,0,0,0,0,0,0,0,0,0,False,0,0,,0,{EscapeCsv(status)}");
                    File.WriteAllLines(DirectHoleCandidateReportPath, report);
                    Debug.LogWarning($"[NoMaskProceduralGenerator] Direct hole candidate failed {spec.Id}: {status}");
                    continue;
                }

                levels.Add(level);
                float boardCoverage = metrics.ArrowCount / (float)Mathf.Max(1, spec.Width * spec.Height);
                int cutCells = Mathf.Max(0, spec.Width * spec.Height - blockCount - allowedCount);
                report.Add(
                    $"{i + 1},{spec.Id},{spec.Type},{levelId},{assetPath},{spec.Width},{spec.Height},{metrics.Chains},{spec.TargetChains},{metrics.ArrowCount},{blockCount},{cutCells},{allowedCount}," +
                    $"{metrics.Coverage:0.000},{boardCoverage:0.000},{metrics.OuterBandCoverage:0.000},{metrics.InitialMovableChains},{metrics.EdgeHeadChains},{metrics.ShortEdgeChains},{metrics.GreedyMoves}," +
                    $"{metrics.AverageChainLength:0.00},{metrics.MaxChainLength},{metrics.Straightness:0.000},{metrics.BlockLinks},{metrics.PortableGreedySolved},{metrics.PortableInitialOpeners}," +
                    $"{metrics.PortableScore:0.0},{EscapeCsv(metrics.PortableQualityFlags)},{metrics.Attempts},{EscapeCsv(status)}");
                File.WriteAllLines(DirectHoleCandidateReportPath, report);
            }

            File.WriteAllLines(DirectHoleCandidateReportPath, report);
            AssetDatabase.ImportAsset(DirectHoleCandidateReportPath);

            LevelPack pack = SavePackAt(levels, DirectHoleCandidatePackPath, "hole_candidate_pool", $"Hole Candidate Pool ({levels.Count})");
            AttachPackToDemo(pack, "DirectHoleCandidatePool");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"[NoMaskProceduralGenerator] Built direct hole candidate pool: levels={levels.Count}/{specs.Length}, pack={DirectHoleCandidatePackPath}, report={DirectHoleCandidateReportPath}");
        }

        [MenuItem("Tools/ArrowMagic/Hole Procedural/Build Topup Candidates Pack (Direct Logic)")]
        public static void BuildDirectHoleTopupCandidatesPack()
        {
            EnsureFolderExists(DirectHoleTopupOutputFolder);
            EnsureFolderExists(Path.GetDirectoryName(DirectHoleTopupPackPath)?.Replace("\\", "/"));
            EnsureFolderExists(Path.GetDirectoryName(DirectHoleTopupReportPath)?.Replace("\\", "/"));

            StyleSpec[] specs = CreateDirectHoleTopupCandidateSpecs();
            var levels = new List<LevelDefinition>(specs.Length);
            var report = new List<string>
            {
                "Index,Spec,Structure,LevelId,AssetPath,Width,Height,Chains,TargetChains,Arrows,Blocks,OuterCutCells,AllowedCells,AllowedCoverage,BoardCoverage,OuterBandCoverage,InitialMovableChains,EdgeHeadChains,ShortChains,GreedyMoves,AvgChain,MaxChain,Straightness,BlockLinks,PortableSolved,PortableOpeners,PortableScore,PortableQuality,Attempts,Status"
            };

            for (int i = 0; i < specs.Length; i++)
            {
                StyleSpec spec = specs[i];
                string levelId = $"hole_topup_{i + 1:000}_{spec.Id}";
                string assetPath = $"{DirectHoleTopupOutputFolder}/{levelId}.asset";
                Debug.Log($"[NoMaskProceduralGenerator] Building direct hole topup {i + 1}/{specs.Length}: {spec.Id}");
                if (!TryBuildDirectHoleLevelAt(spec, i + 1, assetPath, levelId, out LevelDefinition level, out BuildMetrics metrics, out int blockCount, out int allowedCount, out string status))
                {
                    int outerCutCells = Mathf.Max(0, spec.Width * spec.Height - blockCount - allowedCount);
                    report.Add($"{i + 1},{spec.Id},{spec.Type},,{assetPath},{spec.Width},{spec.Height},0,{spec.TargetChains},0,{blockCount},{outerCutCells},{allowedCount},0,0,0,0,0,0,0,0,0,0,0,False,0,0,,0,{EscapeCsv(status)}");
                    File.WriteAllLines(DirectHoleTopupReportPath, report);
                    Debug.LogWarning($"[NoMaskProceduralGenerator] Direct hole topup failed {spec.Id}: {status}");
                    continue;
                }

                levels.Add(level);
                float boardCoverage = metrics.ArrowCount / (float)Mathf.Max(1, spec.Width * spec.Height);
                int cutCells = Mathf.Max(0, spec.Width * spec.Height - blockCount - allowedCount);
                report.Add(
                    $"{i + 1},{spec.Id},{spec.Type},{levelId},{assetPath},{spec.Width},{spec.Height},{metrics.Chains},{spec.TargetChains},{metrics.ArrowCount},{blockCount},{cutCells},{allowedCount}," +
                    $"{metrics.Coverage:0.000},{boardCoverage:0.000},{metrics.OuterBandCoverage:0.000},{metrics.InitialMovableChains},{metrics.EdgeHeadChains},{metrics.ShortEdgeChains},{metrics.GreedyMoves}," +
                    $"{metrics.AverageChainLength:0.00},{metrics.MaxChainLength},{metrics.Straightness:0.000},{metrics.BlockLinks},{metrics.PortableGreedySolved},{metrics.PortableInitialOpeners}," +
                    $"{metrics.PortableScore:0.0},{EscapeCsv(metrics.PortableQualityFlags)},{metrics.Attempts},{EscapeCsv(status)}");
                File.WriteAllLines(DirectHoleTopupReportPath, report);
            }

            File.WriteAllLines(DirectHoleTopupReportPath, report);
            AssetDatabase.ImportAsset(DirectHoleTopupReportPath);

            LevelPack pack = SavePackAt(levels, DirectHoleTopupPackPath, "hole_topup_candidates", $"Hole Topup Candidates ({levels.Count})");
            AttachPackToDemo(pack, "DirectHoleTopupCandidates");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"[NoMaskProceduralGenerator] Built direct hole topup candidates: levels={levels.Count}/{specs.Length}, pack={DirectHoleTopupPackPath}, report={DirectHoleTopupReportPath}");
        }

        [MenuItem("Tools/ArrowMagic/Hole Procedural/Build Combined Candidates Pack")]
        public static void BuildDirectHoleCombinedCandidatesPack()
        {
            EnsureFolderExists(Path.GetDirectoryName(DirectHoleCombinedPackPath)?.Replace("\\", "/"));

            var levels = new List<LevelDefinition>(128);
            AppendLevelsFromFolder(levels, DirectHoleCandidateOutputFolder);
            AppendLevelsFromFolder(levels, DirectHoleTopupOutputFolder);

            LevelPack pack = SavePackAt(levels, DirectHoleCombinedPackPath, "hole_combined_candidates", $"Hole Combined Candidates ({levels.Count})");
            AttachPackToDemo(pack, "DirectHoleCombinedCandidates");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"[NoMaskProceduralGenerator] Built direct hole combined candidates: levels={levels.Count}, pack={DirectHoleCombinedPackPath}");
        }

        [MenuItem("Tools/ArrowMagic/Hole Procedural/Build High Chain Candidates Pack (80-100)")]
        public static void BuildDirectHoleHighChainCandidatesPack()
        {
            EnsureFolderExists(DirectHoleHighChainOutputFolder);
            EnsureFolderExists(Path.GetDirectoryName(DirectHoleHighChainPackPath)?.Replace("\\", "/"));
            EnsureFolderExists(Path.GetDirectoryName(DirectHoleHighChainReportPath)?.Replace("\\", "/"));

            const int minChains = 80;
            const int maxChains = 100;
            const int maxPackLevels = 12;
            StyleSpec[] specs = CreateDirectHoleHighChainCandidateSpecs();
            var levels = new List<LevelDefinition>(maxPackLevels);
            var report = new List<string>
            {
                "Index,Spec,Structure,LevelId,AssetPath,Width,Height,Chains,TargetChains,Arrows,Blocks,OuterCutCells,AllowedCells,AllowedCoverage,BoardCoverage,OuterBandCoverage,InitialMovableChains,EdgeHeadChains,ShortChains,GreedyMoves,AvgChain,MaxChain,Straightness,BlockLinks,PortableSolved,PortableOpeners,PortableScore,PortableQuality,Attempts,Status"
            };

            for (int i = 0; i < specs.Length; i++)
            {
                StyleSpec spec = specs[i];
                string levelId = $"hole_highchain_{i + 1:000}_{spec.Id}";
                string assetPath = $"{DirectHoleHighChainOutputFolder}/{levelId}.asset";
                Debug.Log($"[NoMaskProceduralGenerator] Building direct hole high-chain {i + 1}/{specs.Length}: {spec.Id}");
                if (!TryBuildDirectHoleLevelAt(spec, i + 1, assetPath, levelId, out LevelDefinition level, out BuildMetrics metrics, out int blockCount, out int allowedCount, out string status))
                {
                    int outerCutCells = Mathf.Max(0, spec.Width * spec.Height - blockCount - allowedCount);
                    report.Add($"{i + 1},{spec.Id},{spec.Type},,{assetPath},{spec.Width},{spec.Height},0,{spec.TargetChains},0,{blockCount},{outerCutCells},{allowedCount},0,0,0,0,0,0,0,0,0,0,0,False,0,0,,0,{EscapeCsv(status)}");
                    File.WriteAllLines(DirectHoleHighChainReportPath, report);
                    Debug.LogWarning($"[NoMaskProceduralGenerator] Direct hole high-chain failed {spec.Id}: {status}");
                    continue;
                }

                float boardCoverage = metrics.ArrowCount / (float)Mathf.Max(1, spec.Width * spec.Height);
                int cutCells = Mathf.Max(0, spec.Width * spec.Height - blockCount - allowedCount);
                bool inRange = metrics.Chains >= minChains && metrics.Chains <= maxChains;
                bool underCap = levels.Count < maxPackLevels;
                string rowStatus = inRange && underCap ? status : inRange ? $"SkippedAfterCap | {status}" : $"OutOfChainRange({metrics.Chains}) | {status}";
                if (inRange && underCap)
                {
                    levels.Add(level);
                }
                else
                {
                    AssetDatabase.DeleteAsset(assetPath);
                }

                report.Add(
                    $"{i + 1},{spec.Id},{spec.Type},{(inRange && underCap ? levelId : string.Empty)},{assetPath},{spec.Width},{spec.Height},{metrics.Chains},{spec.TargetChains},{metrics.ArrowCount},{blockCount},{cutCells},{allowedCount}," +
                    $"{metrics.Coverage:0.000},{boardCoverage:0.000},{metrics.OuterBandCoverage:0.000},{metrics.InitialMovableChains},{metrics.EdgeHeadChains},{metrics.ShortEdgeChains},{metrics.GreedyMoves}," +
                    $"{metrics.AverageChainLength:0.00},{metrics.MaxChainLength},{metrics.Straightness:0.000},{metrics.BlockLinks},{metrics.PortableGreedySolved},{metrics.PortableInitialOpeners}," +
                    $"{metrics.PortableScore:0.0},{EscapeCsv(metrics.PortableQualityFlags)},{metrics.Attempts},{EscapeCsv(rowStatus)}");
                File.WriteAllLines(DirectHoleHighChainReportPath, report);

                if (levels.Count >= maxPackLevels)
                    break;
            }

            File.WriteAllLines(DirectHoleHighChainReportPath, report);
            AssetDatabase.ImportAsset(DirectHoleHighChainReportPath);

            LevelPack pack = SavePackAt(levels, DirectHoleHighChainPackPath, "hole_high_chain_candidates", $"Hole High Chain Candidates ({levels.Count})");
            AttachPackToDemo(pack, "DirectHoleHighChainCandidates");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"[NoMaskProceduralGenerator] Built direct hole high-chain candidates: levels={levels.Count}/{specs.Length}, pack={DirectHoleHighChainPackPath}, report={DirectHoleHighChainReportPath}");
        }

        [MenuItem("Tools/ArrowMagic/Hole Procedural/Build High Chain Extra Candidates Pack (80-100)")]
        public static void BuildDirectHoleHighChainExtraCandidatesPack()
        {
            EnsureFolderExists(DirectHoleHighChainOutputFolder);
            EnsureFolderExists(Path.GetDirectoryName(DirectHoleHighChainPackPath)?.Replace("\\", "/"));
            EnsureFolderExists(Path.GetDirectoryName(DirectHoleHighChainExtraReportPath)?.Replace("\\", "/"));

            const int minChains = 80;
            const int maxChains = 100;
            const int maxPackLevels = 12;
            StyleSpec[] specs = CreateDirectHoleHighChainExtraCandidateSpecs();
            var report = new List<string>
            {
                "Index,Spec,Structure,LevelId,AssetPath,Width,Height,Chains,TargetChains,Arrows,Blocks,OuterCutCells,AllowedCells,AllowedCoverage,BoardCoverage,OuterBandCoverage,InitialMovableChains,EdgeHeadChains,ShortChains,GreedyMoves,AvgChain,MaxChain,Straightness,BlockLinks,PortableSolved,PortableOpeners,PortableScore,PortableQuality,Attempts,Status"
            };

            for (int i = 0; i < specs.Length; i++)
            {
                StyleSpec spec = specs[i];
                string levelId = $"hole_highchain_extra_{i + 1:000}_{spec.Id}";
                string assetPath = $"{DirectHoleHighChainOutputFolder}/{levelId}.asset";
                Debug.Log($"[NoMaskProceduralGenerator] Building direct hole high-chain extra {i + 1}/{specs.Length}: {spec.Id}");
                if (!TryBuildDirectHoleLevelAt(spec, i + 1, assetPath, levelId, out LevelDefinition level, out BuildMetrics metrics, out int blockCount, out int allowedCount, out string status))
                {
                    int outerCutCells = Mathf.Max(0, spec.Width * spec.Height - blockCount - allowedCount);
                    report.Add($"{i + 1},{spec.Id},{spec.Type},,{assetPath},{spec.Width},{spec.Height},0,{spec.TargetChains},0,{blockCount},{outerCutCells},{allowedCount},0,0,0,0,0,0,0,0,0,0,0,False,0,0,,0,{EscapeCsv(status)}");
                    File.WriteAllLines(DirectHoleHighChainExtraReportPath, report);
                    Debug.LogWarning($"[NoMaskProceduralGenerator] Direct hole high-chain extra failed {spec.Id}: {status}");
                    continue;
                }

                float boardCoverage = metrics.ArrowCount / (float)Mathf.Max(1, spec.Width * spec.Height);
                int cutCells = Mathf.Max(0, spec.Width * spec.Height - blockCount - allowedCount);
                bool inRange = metrics.Chains >= minChains && metrics.Chains <= maxChains;
                string rowStatus = inRange ? status : $"OutOfChainRange({metrics.Chains}) | {status}";
                if (!inRange)
                    AssetDatabase.DeleteAsset(assetPath);

                report.Add(
                    $"{i + 1},{spec.Id},{spec.Type},{(inRange ? levelId : string.Empty)},{assetPath},{spec.Width},{spec.Height},{metrics.Chains},{spec.TargetChains},{metrics.ArrowCount},{blockCount},{cutCells},{allowedCount}," +
                    $"{metrics.Coverage:0.000},{boardCoverage:0.000},{metrics.OuterBandCoverage:0.000},{metrics.InitialMovableChains},{metrics.EdgeHeadChains},{metrics.ShortEdgeChains},{metrics.GreedyMoves}," +
                    $"{metrics.AverageChainLength:0.00},{metrics.MaxChainLength},{metrics.Straightness:0.000},{metrics.BlockLinks},{metrics.PortableGreedySolved},{metrics.PortableInitialOpeners}," +
                    $"{metrics.PortableScore:0.0},{EscapeCsv(metrics.PortableQualityFlags)},{metrics.Attempts},{EscapeCsv(rowStatus)}");
                File.WriteAllLines(DirectHoleHighChainExtraReportPath, report);
            }

            File.WriteAllLines(DirectHoleHighChainExtraReportPath, report);
            AssetDatabase.ImportAsset(DirectHoleHighChainExtraReportPath);

            var levels = new List<LevelDefinition>(maxPackLevels);
            AppendLevelsFromFolder(levels, DirectHoleHighChainOutputFolder);
            while (levels.Count > maxPackLevels)
                levels.RemoveAt(levels.Count - 1);

            LevelPack pack = SavePackAt(levels, DirectHoleHighChainPackPath, "hole_high_chain_candidates", $"Hole High Chain Candidates ({levels.Count})");
            AttachPackToDemo(pack, "DirectHoleHighChainExtraCandidates");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"[NoMaskProceduralGenerator] Built direct hole high-chain extra candidates: levels={levels.Count}, pack={DirectHoleHighChainPackPath}, report={DirectHoleHighChainExtraReportPath}");
        }

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Normal Candidates Pack")]
        public static void BuildDirectNormalCandidatesPack()
        {
            BuildStyleSpecPack(
                CreateDirectNormalRectangleSpecs(),
                DirectNormalOutputFolder,
                DirectNormalPackPath,
                DirectNormalReportPath,
                "direct_normal",
                "direct_normal_candidates",
                "Direct Normal Candidates",
                "DirectProceduralNormal");
        }

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Normal Topup Candidates Pack")]
        public static void BuildDirectNormalTopupCandidatesPack()
        {
            BuildStyleSpecPack(
                CreateDirectNormalTopupRectangleSpecs(),
                DirectNormalTopupOutputFolder,
                DirectNormalTopupPackPath,
                DirectNormalTopupReportPath,
                "direct_normal_topup",
                "direct_normal_topup_candidates",
                "Direct Normal Topup Candidates",
                "DirectProceduralNormalTopup");
        }

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Advanced Candidates Pack")]
        public static void BuildDirectAdvancedCandidatesPack()
        {
            BuildStyleSpecPack(
                CreateDirectAdvancedRectangleSpecs(),
                DirectAdvancedOutputFolder,
                DirectAdvancedPackPath,
                DirectAdvancedReportPath,
                "direct_advanced",
                "direct_advanced_candidates",
                "Direct Advanced Candidates",
                "DirectProceduralAdvanced");
        }

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Polish Candidates Pack")]
        public static void BuildDirectPolishCandidatesPack()
        {
            BuildStyleSpecPack(
                CreateDirectPolishRectangleSpecs(),
                DirectPolishOutputFolder,
                DirectPolishPackPath,
                DirectPolishReportPath,
                "direct_polish",
                "direct_polish_candidates",
                "Direct Polish Candidates",
                "DirectProceduralPolish");
        }

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Polish Clean Candidates Pack")]
        public static void BuildDirectPolishCleanCandidatesPack()
        {
            BuildStyleSpecPack(
                SliceSpecs(CreateDirectPolishRectangleSpecs(), 0, 24),
                DirectPolishCleanOutputFolder,
                DirectPolishCleanPackPath,
                DirectPolishCleanReportPath,
                "direct_polish_clean",
                "direct_polish_clean_candidates",
                "Direct Polish Clean Candidates",
                "DirectProceduralPolishClean");
        }

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Polish Hard Candidates Pack")]
        public static void BuildDirectPolishHardCandidatesPack()
        {
            BuildStyleSpecPack(
                SliceSpecs(CreateDirectPolishRectangleSpecs(), 36, 24),
                DirectPolishHardOutputFolder,
                DirectPolishHardPackPath,
                DirectPolishHardReportPath,
                "direct_polish_hard",
                "direct_polish_hard_candidates",
                "Direct Polish Hard Candidates",
                "DirectProceduralPolishHard");
        }

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Polish Extreme Long Candidates Pack")]
        public static void BuildDirectPolishExtremeLongCandidatesPack()
        {
            BuildStyleSpecPack(
                SliceSpecs(CreateDirectPolishRectangleSpecs(), 81, 24),
                DirectPolishExtremeOutputFolder,
                DirectPolishExtremePackPath,
                DirectPolishExtremeReportPath,
                "direct_polish_extreme_long",
                "direct_polish_extreme_long_candidates",
                "Direct Polish Extreme Long Candidates",
                "DirectProceduralPolishExtremeLong");
        }

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Structural Differentiation 120 Pack")]
        public static void BuildDirectStructuralDifferentiation120Pack()
        {
            EnsureFolderExists(DirectDifferentiationOutputFolder);
            EnsureFolderExists(Path.GetDirectoryName(DirectDifferentiationPackPath)?.Replace("\\", "/"));
            EnsureFolderExists(Path.GetDirectoryName(DirectDifferentiationReportPath)?.Replace("\\", "/"));

            MotifSpec[] specs = CreateDirectDifferentiationMotifSpecs();
            var levels = new List<LevelDefinition>(DirectDifferentiationTargetCount);
            var bandCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var report = new List<string>
            {
                "Attempt,SelectedIndex,Band,Structure,Motif,BuildMode,Spec,LevelId,AssetPath,Width,Height,Chains,TargetChains,Arrows,Coverage,TargetCoverage,OuterBandCoverage,InitialMovableChains,EdgeHeadChains,ShortChains,GreedyMoves,AvgChain,MaxChain,Straightness,BlockLinks,PortableSolved,PortableOpeners,PortableScore,PortableQuality,Attempts,Status"
            };

            for (int i = 0; i < specs.Length && levels.Count < DirectDifferentiationTargetCount; i++)
            {
                MotifSpec spec = specs[i];
                string band = DirectDiffBand(spec.Id);
                string structure = DirectDiffStructure(spec.Id);
                bandCounts.TryGetValue(band, out int bandCount);
                if (bandCount >= DirectDifferentiationBandQuota)
                    continue;

                Debug.Log($"[NoMaskProcedural] Building structural diff {i + 1}/{specs.Length}: {spec.Id}");
                bool preferFallback = PreferDirectDiffFallbackFirst(band);
                string buildMode = preferFallback ? "peel_fallback" : "motif";
                bool built;
                LevelDefinition level;
                string assetPath;
                BuildMetrics metrics;
                string status;
                int motifCells;

                if (preferFallback)
                {
                    built = TryBuildLevelAt(spec.Style, i + 1, DirectDifferentiationOutputFolder, "direct_diff120", out level, out assetPath, out metrics, out status);
                    motifCells = 0;
                    if (!built)
                    {
                        string fallbackStatus = status;
                        buildMode = "motif_after_fallback_fail";
                        Debug.LogWarning($"[NoMaskProcedural] Structural diff fallback failed {spec.Id}, trying motif: {fallbackStatus}");
                        built = TryBuildMotifLevel(spec, i + 1, DirectDifferentiationOutputFolder, "direct_diff120", out level, out assetPath, out metrics, out motifCells, out status);
                        if (!built)
                            status = $"fallback={fallbackStatus} | motif={status}";
                    }
                }
                else
                {
                    built = TryBuildMotifLevel(spec, i + 1, DirectDifferentiationOutputFolder, "direct_diff120", out level, out assetPath, out metrics, out motifCells, out status);
                    if (!built)
                    {
                        string motifStatus = status;
                        buildMode = "peel_fallback";
                        Debug.LogWarning($"[NoMaskProcedural] Structural diff motif failed {spec.Id}, trying peel fallback: {motifStatus}");
                        built = TryBuildLevelAt(spec.Style, i + 1, DirectDifferentiationOutputFolder, "direct_diff120", out level, out assetPath, out metrics, out status);
                        if (!built)
                            status = $"motif={motifStatus} | fallback={status}";
                        else
                            status = $"fallback-after-motif-fail | {status}";
                    }
                }

                if (!built)
                {
                    report.Add($"{i + 1},,{band},{structure},{spec.Motif},{buildMode},{spec.Id},,{assetPath},{spec.Style.Width},{spec.Style.Height},0,{spec.Style.TargetChains},0,0,{spec.Style.TargetCoverage:0.000},0,0,0,0,0,0,0,0,0,False,0,0,fail,0,{EscapeCsv(status)}");
                    File.WriteAllLines(DirectDifferentiationReportPath, report);
                    Debug.LogWarning($"[NoMaskProcedural] Structural diff failed {spec.Id}: {status}");
                    continue;
                }

                levels.Add(level);
                bandCounts[band] = bandCount + 1;
                int selectedIndex = levels.Count;
                report.Add(
                    $"{i + 1},{selectedIndex},{band},{structure},{spec.Motif},{buildMode},{spec.Id},{EscapeCsv(level.levelId)},{EscapeCsv(assetPath)},{spec.Style.Width},{spec.Style.Height}," +
                    $"{metrics.Chains},{spec.Style.TargetChains},{metrics.ArrowCount},{metrics.Coverage:0.000},{spec.Style.TargetCoverage:0.000},{metrics.OuterBandCoverage:0.000}," +
                    $"{metrics.InitialMovableChains},{metrics.EdgeHeadChains},{metrics.ShortEdgeChains},{metrics.GreedyMoves},{metrics.AverageChainLength:0.00},{metrics.MaxChainLength}," +
                    $"{metrics.Straightness:0.000},{metrics.BlockLinks},{metrics.PortableGreedySolved},{metrics.PortableInitialOpeners},{metrics.PortableScore:0.0}," +
                    $"{EscapeCsv(metrics.PortableQualityFlags)},{metrics.Attempts},{EscapeCsv(status)}");
                File.WriteAllLines(DirectDifferentiationReportPath, report);
            }

            File.WriteAllLines(DirectDifferentiationReportPath, report);
            AssetDatabase.ImportAsset(DirectDifferentiationReportPath);

            LevelPack pack = SavePackAt(levels, DirectDifferentiationPackPath, "direct_differentiation120_candidates", $"Direct Differentiation 120 Candidates ({levels.Count})");
            AttachPackToDemo(pack, "DirectDifferentiation120");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            if (levels.Count < DirectDifferentiationTargetCount)
                Debug.LogWarning($"[NoMaskProcedural] Structural differentiation finished below target: levels={levels.Count}/{DirectDifferentiationTargetCount}, pack={DirectDifferentiationPackPath}, report={DirectDifferentiationReportPath}");
            else
                Debug.Log($"[NoMaskProcedural] Structural differentiation finished. levels={levels.Count}, pack={DirectDifferentiationPackPath}, report={DirectDifferentiationReportPath}");
        }

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Structure Preview 16 Pack")]
        public static void BuildDirectStructurePreview16Pack()
        {
            EnsureFolderExists(DirectStructurePreviewOutputFolder);
            EnsureFolderExists(Path.GetDirectoryName(DirectStructurePreviewPackPath)?.Replace("\\", "/"));
            EnsureFolderExists(Path.GetDirectoryName(DirectStructurePreviewReportPath)?.Replace("\\", "/"));

            MotifSpec[] specs = CreateDirectStructurePreviewSpecs();
            int targetCount = DirectStructurePreviewKindCount * DirectStructurePreviewTargetPerKind;
            var levels = new List<LevelDefinition>(targetCount);
            var kindCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var report = new List<string>
            {
                "Attempt,SelectedIndex,Structure,Motif,BuildMode,Spec,LevelId,AssetPath,Width,Height,Chains,TargetChains,Arrows,Coverage,TargetCoverage,OuterBandCoverage,InitialMovableChains,EdgeHeadChains,ShortChains,GreedyMoves,AvgChain,MaxChain,Straightness,BlockLinks,PortableSolved,PortableOpeners,PortableScore,PortableQuality,Attempts,Status"
            };

            for (int i = 0; i < specs.Length && levels.Count < targetCount; i++)
            {
                MotifSpec spec = specs[i];
                string structure = DirectStructurePreviewKind(spec.Id);
                kindCounts.TryGetValue(structure, out int kindCount);
                if (kindCount >= DirectStructurePreviewTargetPerKind)
                    continue;

                Debug.Log($"[NoMaskProcedural] Building structure preview {i + 1}/{specs.Length}: {spec.Id}");
                string buildMode = "motif";
                bool built = TryBuildMotifLevel(spec, i + 1, DirectStructurePreviewOutputFolder, "direct_struct16", out LevelDefinition level, out string assetPath, out BuildMetrics metrics, out int motifCells, out string status);
                if (!built)
                {
                    string motifStatus = status;
                    buildMode = "peel_fallback";
                    Debug.LogWarning($"[NoMaskProcedural] Structure preview motif failed {spec.Id}, trying peel fallback: {motifStatus}");
                    built = TryBuildLevelAt(spec.Style, i + 1, DirectStructurePreviewOutputFolder, "direct_struct16", out level, out assetPath, out metrics, out status);
                    motifCells = 0;
                    if (!built)
                        status = $"motif={motifStatus} | fallback={status}";
                    else
                        status = $"fallback-after-motif-fail | {status}";
                }

                if (!built)
                {
                    report.Add($"{i + 1},,{structure},{spec.Motif},{buildMode},{spec.Id},,{assetPath},{spec.Style.Width},{spec.Style.Height},0,{spec.Style.TargetChains},0,0,{spec.Style.TargetCoverage:0.000},0,0,0,0,0,0,0,0,0,False,0,0,fail,0,{EscapeCsv(status)}");
                    File.WriteAllLines(DirectStructurePreviewReportPath, report);
                    Debug.LogWarning($"[NoMaskProcedural] Structure preview failed {spec.Id}: {status}");
                    continue;
                }

                levels.Add(level);
                kindCounts[structure] = kindCount + 1;
                int selectedIndex = levels.Count;
                report.Add(
                    $"{i + 1},{selectedIndex},{structure},{spec.Motif},{buildMode},{spec.Id},{EscapeCsv(level.levelId)},{EscapeCsv(assetPath)},{spec.Style.Width},{spec.Style.Height}," +
                    $"{metrics.Chains},{spec.Style.TargetChains},{metrics.ArrowCount},{metrics.Coverage:0.000},{spec.Style.TargetCoverage:0.000},{metrics.OuterBandCoverage:0.000}," +
                    $"{metrics.InitialMovableChains},{metrics.EdgeHeadChains},{metrics.ShortEdgeChains},{metrics.GreedyMoves},{metrics.AverageChainLength:0.00},{metrics.MaxChainLength}," +
                    $"{metrics.Straightness:0.000},{metrics.BlockLinks},{metrics.PortableGreedySolved},{metrics.PortableInitialOpeners},{metrics.PortableScore:0.0}," +
                    $"{EscapeCsv(metrics.PortableQualityFlags)},{metrics.Attempts},{EscapeCsv(status)}");
                File.WriteAllLines(DirectStructurePreviewReportPath, report);
            }

            File.WriteAllLines(DirectStructurePreviewReportPath, report);
            AssetDatabase.ImportAsset(DirectStructurePreviewReportPath);

            LevelPack pack = SavePackAt(levels, DirectStructurePreviewPackPath, "direct_structure_preview16", $"Direct Structure Preview 16 ({levels.Count})");
            AttachPackToDemo(pack, "DirectStructurePreview16");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            if (levels.Count < targetCount)
                Debug.LogWarning($"[NoMaskProcedural] Structure preview finished below target: levels={levels.Count}/{targetCount}, pack={DirectStructurePreviewPackPath}, report={DirectStructurePreviewReportPath}");
            else
                Debug.Log($"[NoMaskProcedural] Structure preview finished. levels={levels.Count}, pack={DirectStructurePreviewPackPath}, report={DirectStructurePreviewReportPath}");
        }

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Architecture Preview Pack")]
        public static void BuildDirectArchitecturePreviewPack()
        {
            EnsureFolderExists(DirectArchitecturePreviewOutputFolder);
            EnsureFolderExists(Path.GetDirectoryName(DirectArchitecturePreviewPackPath)?.Replace("\\", "/"));
            EnsureFolderExists(Path.GetDirectoryName(DirectArchitecturePreviewReportPath)?.Replace("\\", "/"));

            MotifSpec[] specs = CreateDirectArchitecturePreviewSpecs();
            int targetCount = DirectArchitecturePreviewKindCount * DirectArchitecturePreviewTargetPerKind;
            var levels = new List<LevelDefinition>(targetCount);
            var kindCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var report = new List<string>
            {
                "Attempt,SelectedIndex,Architecture,Motif,Spec,LevelId,AssetPath,Width,Height,Chains,TargetChains,Arrows,Coverage,TargetCoverage,OuterBandCoverage,InitialMovableChains,EdgeHeadChains,ShortChains,GreedyMoves,AvgChain,MaxChain,Straightness,BlockLinks,PortableSolved,PortableOpeners,PortableScore,PortableQuality,Attempts,Status"
            };

            for (int i = 0; i < specs.Length && levels.Count < targetCount; i++)
            {
                MotifSpec spec = specs[i];
                string architecture = DirectArchitectureKind(spec.Id);
                kindCounts.TryGetValue(architecture, out int kindCount);
                if (kindCount >= DirectArchitecturePreviewTargetPerKind)
                    continue;

                Debug.Log($"[NoMaskProcedural] Building architecture preview {i + 1}/{specs.Length}: {spec.Id}");
                if (!TryBuildMotifLevel(spec, i + 1, DirectArchitecturePreviewOutputFolder, "direct_arch", out LevelDefinition level, out string assetPath, out BuildMetrics metrics, out int motifCells, out string status))
                {
                    report.Add($"{i + 1},,{architecture},{spec.Motif},{spec.Id},,{assetPath},{spec.Style.Width},{spec.Style.Height},0,{spec.Style.TargetChains},0,0,{spec.Style.TargetCoverage:0.000},0,0,0,0,0,0,0,0,0,False,0,0,fail,0,{EscapeCsv(status)}");
                    File.WriteAllLines(DirectArchitecturePreviewReportPath, report);
                    Debug.LogWarning($"[NoMaskProcedural] Architecture preview failed {spec.Id}: {status}");
                    continue;
                }

                levels.Add(level);
                kindCounts[architecture] = kindCount + 1;
                int selectedIndex = levels.Count;
                report.Add(
                    $"{i + 1},{selectedIndex},{architecture},{spec.Motif},{spec.Id},{EscapeCsv(level.levelId)},{EscapeCsv(assetPath)},{spec.Style.Width},{spec.Style.Height}," +
                    $"{metrics.Chains},{spec.Style.TargetChains},{metrics.ArrowCount},{metrics.Coverage:0.000},{spec.Style.TargetCoverage:0.000},{metrics.OuterBandCoverage:0.000}," +
                    $"{metrics.InitialMovableChains},{metrics.EdgeHeadChains},{metrics.ShortEdgeChains},{metrics.GreedyMoves},{metrics.AverageChainLength:0.00},{metrics.MaxChainLength}," +
                    $"{metrics.Straightness:0.000},{metrics.BlockLinks},{metrics.PortableGreedySolved},{metrics.PortableInitialOpeners},{metrics.PortableScore:0.0}," +
                    $"{EscapeCsv(metrics.PortableQualityFlags)},{metrics.Attempts},{EscapeCsv(status)}");
                File.WriteAllLines(DirectArchitecturePreviewReportPath, report);
            }

            File.WriteAllLines(DirectArchitecturePreviewReportPath, report);
            AssetDatabase.ImportAsset(DirectArchitecturePreviewReportPath);

            LevelPack pack = SavePackAt(levels, DirectArchitecturePreviewPackPath, "direct_architecture_preview", $"Direct Architecture Preview ({levels.Count})");
            AttachPackToDemo(pack, "DirectArchitecturePreview");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            if (levels.Count < targetCount)
                Debug.LogWarning($"[NoMaskProcedural] Architecture preview finished below target: levels={levels.Count}/{targetCount}, pack={DirectArchitecturePreviewPackPath}, report={DirectArchitecturePreviewReportPath}");
            else
                Debug.Log($"[NoMaskProcedural] Architecture preview finished. levels={levels.Count}, pack={DirectArchitecturePreviewPackPath}, report={DirectArchitecturePreviewReportPath}");
        }

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Pure Topup Slice 01 Pack")]
        public static void BuildDirectPureTopupSlice01Pack() => BuildDirectPureTopupSlicePack(0);

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Pure Topup Slice 02 Pack")]
        public static void BuildDirectPureTopupSlice02Pack() => BuildDirectPureTopupSlicePack(1);

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Pure Topup Slice 03 Pack")]
        public static void BuildDirectPureTopupSlice03Pack() => BuildDirectPureTopupSlicePack(2);

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Pure Topup Slice 04 Pack")]
        public static void BuildDirectPureTopupSlice04Pack() => BuildDirectPureTopupSlicePack(3);

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Pure Topup Slice 05 Pack")]
        public static void BuildDirectPureTopupSlice05Pack() => BuildDirectPureTopupSlicePack(4);

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Pure Topup Slice 06 Pack")]
        public static void BuildDirectPureTopupSlice06Pack() => BuildDirectPureTopupSlicePack(5);

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Pure Topup Slice 07 Pack")]
        public static void BuildDirectPureTopupSlice07Pack() => BuildDirectPureTopupSlicePack(6);

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Pure Topup Slice 08 Pack")]
        public static void BuildDirectPureTopupSlice08Pack() => BuildDirectPureTopupSlicePack(7);

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Pure Topup Slice 09 Pack")]
        public static void BuildDirectPureTopupSlice09Pack() => BuildDirectPureTopupSlicePack(8);

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Pure Topup Slice 10 Pack")]
        public static void BuildDirectPureTopupSlice10Pack() => BuildDirectPureTopupSlicePack(9);

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Pure Topup Slice 11 Pack")]
        public static void BuildDirectPureTopupSlice11Pack() => BuildDirectPureTopupSlicePack(10);

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Pure Topup Slice 12 Pack")]
        public static void BuildDirectPureTopupSlice12Pack() => BuildDirectPureTopupSlicePack(11);

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Pure Normal Extra Slice 01 Pack")]
        public static void BuildDirectPureNormalExtraSlice01Pack() => BuildDirectPureNormalExtraSlicePack(0);

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Pure Normal Extra Slice 02 Pack")]
        public static void BuildDirectPureNormalExtraSlice02Pack() => BuildDirectPureNormalExtraSlicePack(1);

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Pure Normal Extra Slice 03 Pack")]
        public static void BuildDirectPureNormalExtraSlice03Pack() => BuildDirectPureNormalExtraSlicePack(2);

        [MenuItem("Tools/ArrowMagic/Direct Procedural/Build Pure Normal Extra Slice 04 Pack")]
        public static void BuildDirectPureNormalExtraSlice04Pack() => BuildDirectPureNormalExtraSlicePack(3);

        private static void BuildDirectPureNormalExtraSlicePack(int sliceIndex)
        {
            int safeIndex = Mathf.Max(0, sliceIndex);
            string suffix = $"{safeIndex + 1:00}";
            BuildStyleSpecPack(
                SliceSpecs(CreateDirectPureNormalExtraRectangleSpecs(), safeIndex * 24, 24),
                $"{DirectPureTopupOutputRoot}/NormalExtraSlice{suffix}",
                $"{DirectPureTopupPackRoot}/DirectPureNormalExtraSlice{suffix}CandidatesPack.asset",
                $"{DirectPureTopupReportRoot}/direct_pure_normal_extra_slice{suffix}_candidates_report.csv",
                $"direct_pure_normal_extra_{suffix}",
                $"direct_pure_normal_extra_slice{suffix}_candidates",
                $"Direct Pure Normal Extra Slice {suffix} Candidates",
                $"DirectProceduralPureNormalExtra{suffix}");
        }

        private static void BuildDirectPureTopupSlicePack(int sliceIndex)
        {
            int safeIndex = Mathf.Max(0, sliceIndex);
            string suffix = $"{safeIndex + 1:00}";
            BuildStyleSpecPack(
                SliceSpecs(CreateDirectPureTopupRectangleSpecs(), safeIndex * 24, 24),
                $"{DirectPureTopupOutputRoot}/Slice{suffix}",
                $"{DirectPureTopupPackRoot}/DirectPureTopupSlice{suffix}CandidatesPack.asset",
                $"{DirectPureTopupReportRoot}/direct_pure_topup_slice{suffix}_candidates_report.csv",
                $"direct_pure_topup_{suffix}",
                $"direct_pure_topup_slice{suffix}_candidates",
                $"Direct Pure Topup Slice {suffix} Candidates",
                $"DirectProceduralPureTopup{suffix}");
        }

        private static StyleSpec[] SliceSpecs(StyleSpec[] specs, int start, int count)
        {
            if (specs == null || specs.Length == 0 || count <= 0 || start >= specs.Length)
                return Array.Empty<StyleSpec>();

            int actual = Mathf.Min(count, specs.Length - Mathf.Max(0, start));
            var slice = new StyleSpec[actual];
            for (int i = 0; i < actual; i++)
                slice[i] = specs[start + i];
            return slice;
        }

        private static void BuildStyleSpecPack(
            StyleSpec[] specs,
            string outputFolder,
            string packPath,
            string reportPath,
            string levelIdPrefix,
            string packId,
            string displayName,
            string logTag)
        {
            EnsureFolderExists(outputFolder);
            EnsureFolderExists(Path.GetDirectoryName(packPath)?.Replace("\\", "/"));
            EnsureFolderExists(Path.GetDirectoryName(reportPath)?.Replace("\\", "/"));

            var levels = new List<LevelDefinition>();
            var report = new List<string>
            {
                "Index,Type,LevelId,AssetPath,Width,Height,Chains,Arrows,Coverage,TargetCoverage,OuterBandCoverage,InitialMovableChains,EdgeHeadChains,ShortChains,GreedyMoves,AvgChain,MaxChain,Straightness,BlockLinks,PortableSolved,PortableOpeners,PortableScore,PortableQuality,Attempts,Status"
            };

            for (int i = 0; i < specs.Length; i++)
            {
                StyleSpec spec = specs[i];
                Debug.Log($"[NoMaskProcedural] Building direct {i + 1}/{specs.Length}: {spec.Id}");
                if (!TryBuildLevelAt(spec, i + 1, outputFolder, levelIdPrefix, out LevelDefinition level, out string assetPath, out BuildMetrics metrics, out string status))
                {
                    report.Add($"{i + 1},{spec.Id},,,{spec.Width},{spec.Height},0,0,0,{spec.TargetCoverage:0.000},0,0,0,0,0,0,0,0,0,False,0,0,fail,0,{EscapeCsv(status)}");
                    File.WriteAllLines(reportPath, report);
                    Debug.LogWarning($"[NoMaskProcedural] Failed direct {spec.Id}: {status}");
                    continue;
                }

                levels.Add(level);
                Debug.Log($"[NoMaskProcedural] Built direct {spec.Id}: chains={metrics.Chains}, coverage={metrics.Coverage:0.000}, outer={metrics.OuterBandCoverage:0.000}, initial={metrics.InitialMovableChains}, edgeHeads={metrics.EdgeHeadChains}");
                report.Add(
                    $"{i + 1},{spec.Id},{EscapeCsv(level.levelId)},{EscapeCsv(assetPath)},{spec.Width},{spec.Height}," +
                    $"{metrics.Chains},{metrics.ArrowCount},{metrics.Coverage:0.000},{spec.TargetCoverage:0.000},{metrics.OuterBandCoverage:0.000}," +
                    $"{metrics.InitialMovableChains},{metrics.EdgeHeadChains},{metrics.ShortEdgeChains},{metrics.GreedyMoves}," +
                    $"{metrics.AverageChainLength:0.00},{metrics.MaxChainLength},{metrics.Straightness:0.000}," +
                    $"{metrics.BlockLinks},{metrics.PortableGreedySolved},{metrics.PortableInitialOpeners},{metrics.PortableScore:0.0},{EscapeCsv(metrics.PortableQualityFlags)}," +
                    $"{metrics.Attempts},ok");
                File.WriteAllLines(reportPath, report);
            }

            File.WriteAllLines(reportPath, report);
            AssetDatabase.ImportAsset(reportPath);

            LevelPack pack = SavePackAt(levels, packPath, packId, $"{displayName} ({levels.Count})");
            AttachPackToDemo(pack, logTag);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"[NoMaskProcedural] {displayName} finished. levels={levels.Count}, pack={packPath}, report={reportPath}");
        }

        private static void BuildMotifPreviewPack(
            MotifSpec[] specs,
            string outputFolder,
            string packPath,
            string reportPath,
            string levelIdPrefix,
            string packId,
            string displayName,
            string logTag)
        {
            EnsureFolderExists(outputFolder);
            EnsureFolderExists(Path.GetDirectoryName(packPath)?.Replace("\\", "/"));
            EnsureFolderExists(Path.GetDirectoryName(reportPath)?.Replace("\\", "/"));

            var levels = new List<LevelDefinition>();
            var report = new List<string>
            {
                "Index,Motif,LevelId,AssetPath,Width,Height,Chains,Arrows,MotifCells,Coverage,TargetCoverage,OuterBandCoverage,InitialMovableChains,EdgeHeadChains,ShortChains,GreedyMoves,AvgChain,MaxChain,Straightness,BlockLinks,PortableSolved,PortableOpeners,PortableScore,PortableQuality,Attempts,Status"
            };

            for (int i = 0; i < specs.Length; i++)
            {
                MotifSpec spec = specs[i];
                Debug.Log($"[NoMaskProcedural] Building style {i + 1}/{specs.Length}: {spec.Id}");
                if (!TryBuildMotifLevel(spec, i + 1, outputFolder, levelIdPrefix, out LevelDefinition level, out string assetPath, out BuildMetrics metrics, out int motifCells, out string status))
                {
                    report.Add($"{i + 1},{spec.Id},,,{spec.Style.Width},{spec.Style.Height},0,0,0,0,{spec.Style.TargetCoverage:0.000},0,0,0,0,0,0,0,0,0,False,0,0,fail,0,{EscapeCsv(status)}");
                    File.WriteAllLines(reportPath, report);
                    Debug.LogWarning($"[NoMaskProcedural] Failed style {spec.Id}: {status}");
                    continue;
                }

                levels.Add(level);
                Debug.Log($"[NoMaskProcedural] Built style {spec.Id}: chains={metrics.Chains}, coverage={metrics.Coverage:0.000}, outer={metrics.OuterBandCoverage:0.000}, initial={metrics.InitialMovableChains}, motifCells={motifCells}");
                report.Add(
                    $"{i + 1},{spec.Id},{EscapeCsv(level.levelId)},{EscapeCsv(assetPath)},{spec.Style.Width},{spec.Style.Height}," +
                    $"{metrics.Chains},{metrics.ArrowCount},{motifCells},{metrics.Coverage:0.000},{spec.Style.TargetCoverage:0.000},{metrics.OuterBandCoverage:0.000}," +
                    $"{metrics.InitialMovableChains},{metrics.EdgeHeadChains},{metrics.ShortEdgeChains},{metrics.GreedyMoves}," +
                    $"{metrics.AverageChainLength:0.00},{metrics.MaxChainLength},{metrics.Straightness:0.000}," +
                    $"{metrics.BlockLinks},{metrics.PortableGreedySolved},{metrics.PortableInitialOpeners},{metrics.PortableScore:0.0},{EscapeCsv(metrics.PortableQualityFlags)}," +
                    $"{metrics.Attempts},ok");
                File.WriteAllLines(reportPath, report);
            }

            File.WriteAllLines(reportPath, report);
            AssetDatabase.ImportAsset(reportPath);

            LevelPack pack = SavePackAt(levels, packPath, packId, $"{displayName} ({levels.Count})");
            AttachPackToDemo(pack, logTag);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"[NoMaskProcedural] {displayName} finished. levels={levels.Count}, pack={packPath}, report={reportPath}");
        }

        private static StyleSpec[] CreateSpecs()
        {
            return new[]
            {
                new StyleSpec
                {
                    Type = NoMaskType.OuterShell,
                    Id = "outer_shell",
                    DisplayName = "Outer Shell Peel",
                    Width = 15,
                    Height = 22,
                    TargetChains = 32,
                    MinLength = 5,
                    MaxLength = 9,
                    TargetCoverage = 0.92f,
                    TurnBias = 0.48f,
                    BlockWeight = 3.2f,
                    EdgeOpeningBias = 0.42f,
                    OuterBandTarget = 0.86f,
                    MaxInitialMovableChains = 14,
                    MaxShortEdgePatchChains = 6,
                    MaxShortFillPatchChains = 10,
                    Seed = 71001
                },
                new StyleSpec
                {
                    Type = NoMaskType.SectionUnlock,
                    Id = "section_unlock",
                    DisplayName = "Section Unlock",
                    Width = 16,
                    Height = 23,
                    TargetChains = 34,
                    MinLength = 5,
                    MaxLength = 9,
                    TargetCoverage = 0.92f,
                    TurnBias = 0.55f,
                    BlockWeight = 3.5f,
                    EdgeOpeningBias = 0.36f,
                    OuterBandTarget = 0.86f,
                    MaxInitialMovableChains = 13,
                    MaxShortEdgePatchChains = 6,
                    MaxShortFillPatchChains = 10,
                    Seed = 71029
                },
                new StyleSpec
                {
                    Type = NoMaskType.LockBuckle,
                    Id = "lock_buckle",
                    DisplayName = "Lock Buckle",
                    Width = 15,
                    Height = 22,
                    TargetChains = 31,
                    MinLength = 6,
                    MaxLength = 10,
                    TargetCoverage = 0.92f,
                    TurnBias = 0.62f,
                    BlockWeight = 5.0f,
                    EdgeOpeningBias = 0.32f,
                    OuterBandTarget = 0.86f,
                    MaxInitialMovableChains = 12,
                    MaxShortEdgePatchChains = 5,
                    MaxShortFillPatchChains = 9,
                    Seed = 71057
                },
                new StyleSpec
                {
                    Type = NoMaskType.MazeLongChain,
                    Id = "maze_long_chain",
                    DisplayName = "Maze Long Chain",
                    Width = 17,
                    Height = 24,
                    TargetChains = 30,
                    MinLength = 6,
                    MaxLength = 10,
                    TargetCoverage = 0.90f,
                    TurnBias = 0.68f,
                    BlockWeight = 2.8f,
                    EdgeOpeningBias = 0.28f,
                    OuterBandTarget = 0.86f,
                    MaxInitialMovableChains = 12,
                    MaxShortEdgePatchChains = 5,
                    MaxShortFillPatchChains = 8,
                    Seed = 71083
                },
                new StyleSpec
                {
                    Type = NoMaskType.DenseWeave,
                    Id = "dense_weave",
                    DisplayName = "Dense Weave",
                    Width = 16,
                    Height = 22,
                    TargetChains = 34,
                    MinLength = 5,
                    MaxLength = 9,
                    TargetCoverage = 0.91f,
                    TurnBias = 0.58f,
                    BlockWeight = 2.4f,
                    EdgeOpeningBias = 0.30f,
                    OuterBandTarget = 0.86f,
                    MaxInitialMovableChains = 12,
                    MaxShortEdgePatchChains = 5,
                    MaxShortFillPatchChains = 10,
                    Seed = 71111
                },
                new StyleSpec
                {
                    Type = NoMaskType.Sweep,
                    Id = "sweep",
                    DisplayName = "Sweep",
                    Width = 15,
                    Height = 21,
                    TargetChains = 30,
                    MinLength = 5,
                    MaxLength = 9,
                    TargetCoverage = 0.91f,
                    TurnBias = 0.42f,
                    BlockWeight = 2.2f,
                    EdgeOpeningBias = 0.40f,
                    OuterBandTarget = 0.86f,
                    MaxInitialMovableChains = 13,
                    MaxShortEdgePatchChains = 6,
                    MaxShortFillPatchChains = 9,
                    Seed = 71137
                },
                new StyleSpec
                {
                    Type = NoMaskType.CoreBurst,
                    Id = "core_burst",
                    DisplayName = "Core Burst",
                    Width = 16,
                    Height = 23,
                    TargetChains = 34,
                    MinLength = 5,
                    MaxLength = 10,
                    TargetCoverage = 0.92f,
                    TurnBias = 0.60f,
                    BlockWeight = 4.6f,
                    EdgeOpeningBias = 0.32f,
                    OuterBandTarget = 0.86f,
                    MaxInitialMovableChains = 12,
                    MaxShortEdgePatchChains = 5,
                    MaxShortFillPatchChains = 9,
                    Seed = 71171
                },
                new StyleSpec
                {
                    Type = NoMaskType.DualZone,
                    Id = "dual_zone",
                    DisplayName = "Dual Zone Interlock",
                    Width = 16,
                    Height = 22,
                    TargetChains = 32,
                    MinLength = 5,
                    MaxLength = 9,
                    TargetCoverage = 0.91f,
                    TurnBias = 0.55f,
                    BlockWeight = 3.8f,
                    EdgeOpeningBias = 0.34f,
                    OuterBandTarget = 0.86f,
                    MaxInitialMovableChains = 13,
                    MaxShortEdgePatchChains = 5,
                    MaxShortFillPatchChains = 8,
                    Seed = 71197
                },
                new StyleSpec
                {
                    Type = NoMaskType.StairPush,
                    Id = "stair_push",
                    DisplayName = "Stair Push",
                    Width = 15,
                    Height = 23,
                    TargetChains = 31,
                    MinLength = 5,
                    MaxLength = 10,
                    TargetCoverage = 0.92f,
                    TurnBias = 0.50f,
                    BlockWeight = 3.0f,
                    EdgeOpeningBias = 0.38f,
                    OuterBandTarget = 0.86f,
                    MaxInitialMovableChains = 13,
                    MaxShortEdgePatchChains = 6,
                    MaxShortFillPatchChains = 9,
                    Seed = 71221
                },
                new StyleSpec
                {
                    Type = NoMaskType.QuasiSymmetry,
                    Id = "quasi_symmetry",
                    DisplayName = "Quasi Symmetry",
                    Width = 17,
                    Height = 22,
                    TargetChains = 35,
                    MinLength = 5,
                    MaxLength = 9,
                    TargetCoverage = 0.92f,
                    TurnBias = 0.50f,
                    BlockWeight = 2.8f,
                    EdgeOpeningBias = 0.34f,
                    OuterBandTarget = 0.86f,
                    MaxInitialMovableChains = 13,
                    MaxShortEdgePatchChains = 5,
                    MaxShortFillPatchChains = 9,
                    Seed = 71249
                }
            };
        }

        private static MotifSpec[] CreateMotifSpecs()
        {
            return new[]
            {
                Motif(NoMaskMotifKind.HuiSpiral, "hui_spiral", "Hui Spiral Long Chain", NoMaskType.MazeLongChain, 16, 23, 46, 5, 10, 0.96f, 0.94f, 82001),
                Motif(NoMaskMotifKind.SnakeSpine, "snake_spine", "Snake Spine", NoMaskType.Sweep, 16, 24, 46, 5, 10, 0.96f, 0.94f, 82037),
                Motif(NoMaskMotifKind.DoubleRoomLock, "double_room_lock", "Double Room Lock", NoMaskType.DualZone, 17, 23, 48, 5, 10, 0.96f, 0.94f, 82073),
                Motif(NoMaskMotifKind.CenterCross, "center_cross", "Center Cross Lock", NoMaskType.LockBuckle, 16, 23, 48, 5, 10, 0.96f, 0.94f, 82109),
                Motif(NoMaskMotifKind.DoubleShell, "double_shell", "Double Shell", NoMaskType.OuterShell, 17, 24, 50, 5, 10, 0.96f, 0.94f, 82145),
                Motif(NoMaskMotifKind.StairLadder, "stair_ladder", "Stair Ladder", NoMaskType.StairPush, 16, 24, 46, 5, 10, 0.96f, 0.94f, 82181),
                Motif(NoMaskMotifKind.KeyDoor, "key_door", "Key Door", NoMaskType.LockBuckle, 17, 24, 50, 5, 10, 0.96f, 0.94f, 82217),
                Motif(NoMaskMotifKind.FourPockets, "four_pockets", "Four Pockets", NoMaskType.SectionUnlock, 17, 24, 50, 5, 10, 0.96f, 0.94f, 82253),
                Motif(NoMaskMotifKind.VerticalGate, "vertical_gate", "Vertical Gate", NoMaskType.CoreBurst, 16, 25, 50, 5, 10, 0.96f, 0.94f, 82289),
                Motif(NoMaskMotifKind.ZigRiver, "zig_river", "Z River", NoMaskType.Sweep, 18, 25, 54, 5, 11, 0.96f, 0.94f, 82325),
                Motif(NoMaskMotifKind.DenseKernel, "dense_kernel", "Dense Kernel", NoMaskType.DenseWeave, 18, 26, 56, 5, 11, 0.96f, 0.94f, 82361),
                Motif(NoMaskMotifKind.LongCorridor, "long_corridor", "Long Corridor", NoMaskType.MazeLongChain, 19, 27, 58, 5, 12, 0.96f, 0.94f, 82397)
            };
        }

        private static MotifSpec[] CreateMotifExpansionSpecs()
        {
            return new[]
            {
                Motif(NoMaskMotifKind.HuiSpiral, "hui_spiral_a", "Hui Spiral A", NoMaskType.MazeLongChain, 16, 23, 46, 5, 10, 0.96f, 0.94f, 83001, 0, false, false),
                Motif(NoMaskMotifKind.HuiSpiral, "hui_spiral_b", "Hui Spiral B", NoMaskType.MazeLongChain, 17, 24, 50, 5, 10, 0.96f, 0.94f, 83037, 1, true, false),
                Motif(NoMaskMotifKind.HuiSpiral, "hui_spiral_mid", "Hui Spiral Mid", NoMaskType.MazeLongChain, 18, 26, 58, 5, 11, 0.96f, 0.94f, 83073, 2, false, true),
                Motif(NoMaskMotifKind.SnakeSpine, "snake_spine_a", "Snake Spine A", NoMaskType.Sweep, 16, 24, 46, 5, 10, 0.96f, 0.94f, 83109, 0, false, false),
                Motif(NoMaskMotifKind.SnakeSpine, "snake_spine_b", "Snake Spine B", NoMaskType.Sweep, 17, 25, 50, 5, 10, 0.96f, 0.94f, 83145, 1, true, false),
                Motif(NoMaskMotifKind.SnakeSpine, "snake_spine_mid", "Snake Spine Mid", NoMaskType.Sweep, 18, 27, 58, 5, 11, 0.96f, 0.94f, 83181, 2, false, true),
                Motif(NoMaskMotifKind.DoubleRoomLock, "double_room_lock_a", "Double Room Lock A", NoMaskType.DualZone, 17, 23, 48, 5, 10, 0.96f, 0.94f, 83217, 0, false, false),
                Motif(NoMaskMotifKind.DoubleRoomLock, "double_room_lock_b", "Double Room Lock B", NoMaskType.DualZone, 18, 24, 52, 5, 10, 0.96f, 0.94f, 83253, 1, true, false),
                Motif(NoMaskMotifKind.DoubleRoomLock, "double_room_lock_mid", "Double Room Lock Mid", NoMaskType.DualZone, 19, 27, 62, 5, 11, 0.96f, 0.94f, 83289, 2, false, true),
                Motif(NoMaskMotifKind.KeyDoor, "key_door_a", "Key Door A", NoMaskType.LockBuckle, 17, 24, 50, 5, 10, 0.96f, 0.94f, 83325, 0, false, false),
                Motif(NoMaskMotifKind.KeyDoor, "key_door_b", "Key Door B", NoMaskType.LockBuckle, 18, 25, 54, 5, 11, 0.96f, 0.94f, 83361, 1, true, false),
                Motif(NoMaskMotifKind.KeyDoor, "key_door_mid", "Key Door Mid", NoMaskType.LockBuckle, 19, 28, 64, 5, 12, 0.96f, 0.94f, 83397, 2, false, true),
                Motif(NoMaskMotifKind.DenseKernel, "dense_kernel_a", "Dense Kernel A", NoMaskType.DenseWeave, 18, 26, 56, 5, 11, 0.96f, 0.94f, 83433, 0, false, false),
                Motif(NoMaskMotifKind.DenseKernel, "dense_kernel_b", "Dense Kernel B", NoMaskType.DenseWeave, 19, 27, 60, 5, 11, 0.96f, 0.94f, 83469, 1, true, false),
                Motif(NoMaskMotifKind.DenseKernel, "dense_kernel_mid", "Dense Kernel Mid", NoMaskType.DenseWeave, 20, 29, 70, 5, 12, 0.96f, 0.94f, 83505, 2, false, true),
                Motif(NoMaskMotifKind.ZigRiver, "zig_river_a", "Z River A", NoMaskType.Sweep, 18, 25, 54, 5, 11, 0.96f, 0.94f, 83541, 0, false, false),
                Motif(NoMaskMotifKind.ZigRiver, "zig_river_b", "Z River B", NoMaskType.Sweep, 18, 27, 58, 5, 11, 0.96f, 0.94f, 83577, 1, true, false),
                Motif(NoMaskMotifKind.ZigRiver, "zig_river_mid", "Z River Mid", NoMaskType.Sweep, 20, 29, 70, 5, 12, 0.96f, 0.94f, 83613, 2, false, true),
                Motif(NoMaskMotifKind.LongCorridor, "long_corridor_a", "Long Corridor A", NoMaskType.MazeLongChain, 19, 27, 58, 5, 12, 0.96f, 0.94f, 83649, 0, false, false),
                Motif(NoMaskMotifKind.LongCorridor, "long_corridor_b", "Long Corridor B", NoMaskType.MazeLongChain, 20, 28, 64, 5, 12, 0.96f, 0.94f, 83685, 1, true, false),
                Motif(NoMaskMotifKind.LongCorridor, "long_corridor_mid", "Long Corridor Mid", NoMaskType.MazeLongChain, 20, 29, 66, 5, 12, 0.96f, 0.94f, 83721, 2, false, true),
                Motif(NoMaskMotifKind.CenterCross, "center_cross_a", "Center Cross A", NoMaskType.LockBuckle, 16, 23, 48, 5, 10, 0.96f, 0.94f, 83757, 0, false, false),
                Motif(NoMaskMotifKind.CenterCross, "center_cross_b", "Center Cross B", NoMaskType.LockBuckle, 17, 24, 52, 5, 10, 0.96f, 0.94f, 83793, 1, true, false),
                Motif(NoMaskMotifKind.DoubleShell, "double_shell_a", "Double Shell A", NoMaskType.OuterShell, 17, 24, 50, 5, 10, 0.96f, 0.94f, 83829, 0, false, false),
                Motif(NoMaskMotifKind.DoubleShell, "double_shell_b", "Double Shell B", NoMaskType.OuterShell, 19, 27, 62, 5, 11, 0.96f, 0.94f, 83865, 1, false, true),
                Motif(NoMaskMotifKind.StairLadder, "stair_ladder_a", "Stair Ladder A", NoMaskType.StairPush, 16, 24, 46, 5, 10, 0.96f, 0.94f, 83901, 0, false, false),
                Motif(NoMaskMotifKind.StairLadder, "stair_ladder_b", "Stair Ladder B", NoMaskType.StairPush, 17, 25, 52, 5, 10, 0.96f, 0.94f, 83937, 2, true, false),
                Motif(NoMaskMotifKind.FourPockets, "four_pockets_a", "Four Pockets A", NoMaskType.SectionUnlock, 17, 24, 50, 5, 10, 0.96f, 0.94f, 83973, 0, false, false),
                Motif(NoMaskMotifKind.FourPockets, "four_pockets_b", "Four Pockets B", NoMaskType.SectionUnlock, 17, 24, 50, 5, 10, 0.96f, 0.94f, 84009, 1, true, true),
                Motif(NoMaskMotifKind.VerticalGate, "vertical_gate_a", "Vertical Gate A", NoMaskType.CoreBurst, 16, 25, 50, 5, 10, 0.96f, 0.94f, 84045, 0, false, false),
                Motif(NoMaskMotifKind.VerticalGate, "vertical_gate_b", "Vertical Gate B", NoMaskType.CoreBurst, 18, 28, 62, 5, 11, 0.96f, 0.94f, 84081, 1, true, false),
                Motif(NoMaskMotifKind.DenseKernel, "dense_kernel_tall", "Dense Kernel Tall", NoMaskType.DenseWeave, 17, 29, 62, 5, 12, 0.96f, 0.94f, 84117, 3, true, true),
                Motif(NoMaskMotifKind.DoubleRoomLock, "double_room_lock_tall", "Double Room Lock Tall", NoMaskType.DualZone, 18, 29, 62, 5, 12, 0.96f, 0.94f, 84153, 3, false, false),
                Motif(NoMaskMotifKind.DoubleShell, "double_shell_tall", "Double Shell Tall", NoMaskType.OuterShell, 18, 29, 64, 5, 12, 0.96f, 0.94f, 84189, 2, true, false),
                Motif(NoMaskMotifKind.ZigRiver, "zig_river_tall", "Z River Tall", NoMaskType.Sweep, 18, 30, 66, 5, 12, 0.96f, 0.94f, 84225, 3, false, true),
                Motif(NoMaskMotifKind.SnakeSpine, "snake_spine_tall", "Snake Spine Tall", NoMaskType.Sweep, 17, 30, 64, 5, 12, 0.96f, 0.94f, 84261, 3, true, true)
            };
        }

        private static StyleSpec[] CreateLevel3TinySpecs()
        {
            return new[]
            {
                new StyleSpec
                {
                    Type = NoMaskType.OuterShell,
                    Id = "level3_tiny_outer_shell_peel_18",
                    DisplayName = "Level 3 Tiny Outer Shell",
                    Width = 9,
                    Height = 13,
                    TargetChains = 17,
                    MinLength = 4,
                    MaxLength = 8,
                    TargetCoverage = 0.90f,
                    TurnBias = 0.50f,
                    BlockWeight = 3.0f,
                    EdgeOpeningBias = 0.34f,
                    OuterBandTarget = 0.88f,
                    MaxInitialMovableChains = 7,
                    MaxShortEdgePatchChains = 4,
                    MaxShortFillPatchChains = 5,
                    Seed = 251003
                },
                new StyleSpec
                {
                    Type = NoMaskType.LockBuckle,
                    Id = "level3_tiny_soft_lock_18",
                    DisplayName = "Level 3 Tiny Soft Lock",
                    Width = 9,
                    Height = 13,
                    TargetChains = 17,
                    MinLength = 4,
                    MaxLength = 8,
                    TargetCoverage = 0.90f,
                    TurnBias = 0.62f,
                    BlockWeight = 4.4f,
                    EdgeOpeningBias = 0.28f,
                    OuterBandTarget = 0.88f,
                    MaxInitialMovableChains = 7,
                    MaxShortEdgePatchChains = 3,
                    MaxShortFillPatchChains = 5,
                    Seed = 251037
                },
                new StyleSpec
                {
                    Type = NoMaskType.MazeLongChain,
                    Id = "level3_tiny_mini_maze_17",
                    DisplayName = "Level 3 Tiny Mini Maze",
                    Width = 9,
                    Height = 14,
                    TargetChains = 16,
                    MinLength = 5,
                    MaxLength = 9,
                    TargetCoverage = 0.89f,
                    TurnBias = 0.70f,
                    BlockWeight = 2.6f,
                    EdgeOpeningBias = 0.25f,
                    OuterBandTarget = 0.88f,
                    MaxInitialMovableChains = 7,
                    MaxShortEdgePatchChains = 3,
                    MaxShortFillPatchChains = 5,
                    Seed = 251071
                }
            };
        }

        private static StyleSpec[] CreateLevel3ThirtySpecs()
        {
            return new[]
            {
                new StyleSpec
                {
                    Type = NoMaskType.SectionUnlock,
                    Id = "level3_thirty_section_unlock_30",
                    DisplayName = "Level 3 Thirty Section Unlock",
                    Width = 12,
                    Height = 17,
                    TargetChains = 28,
                    MinLength = 4,
                    MaxLength = 9,
                    TargetCoverage = 0.93f,
                    TurnBias = 0.56f,
                    BlockWeight = 3.6f,
                    EdgeOpeningBias = 0.30f,
                    OuterBandTarget = 0.90f,
                    MaxInitialMovableChains = 9,
                    MaxShortEdgePatchChains = 4,
                    MaxShortFillPatchChains = 7,
                    Seed = 252003
                },
                new StyleSpec
                {
                    Type = NoMaskType.LockBuckle,
                    Id = "level3_thirty_soft_lock_31",
                    DisplayName = "Level 3 Thirty Soft Lock",
                    Width = 12,
                    Height = 18,
                    TargetChains = 29,
                    MinLength = 4,
                    MaxLength = 9,
                    TargetCoverage = 0.93f,
                    TurnBias = 0.64f,
                    BlockWeight = 4.6f,
                    EdgeOpeningBias = 0.26f,
                    OuterBandTarget = 0.90f,
                    MaxInitialMovableChains = 8,
                    MaxShortEdgePatchChains = 4,
                    MaxShortFillPatchChains = 7,
                    Seed = 252037
                },
                new StyleSpec
                {
                    Type = NoMaskType.MazeLongChain,
                    Id = "level3_thirty_mini_maze_30",
                    DisplayName = "Level 3 Thirty Mini Maze",
                    Width = 12,
                    Height = 18,
                    TargetChains = 28,
                    MinLength = 5,
                    MaxLength = 10,
                    TargetCoverage = 0.92f,
                    TurnBias = 0.70f,
                    BlockWeight = 2.7f,
                    EdgeOpeningBias = 0.24f,
                    OuterBandTarget = 0.90f,
                    MaxInitialMovableChains = 8,
                    MaxShortEdgePatchChains = 4,
                    MaxShortFillPatchChains = 7,
                    Seed = 252071
                }
            };
        }

        private static StyleSpec[] CreateDirectHoleFrontSpecs()
        {
            return new[]
            {
                new StyleSpec
                {
                    Type = NoMaskType.LockBuckle,
                    Id = "level3_tiny_hole_lock_16x18",
                    DisplayName = "Front Hole Lock 16x18",
                    Width = 16,
                    Height = 18,
                    TargetChains = 18,
                    MinLength = 6,
                    MaxLength = 14,
                    TargetCoverage = 0.82f,
                    TurnBias = 0.64f,
                    BlockWeight = 4.8f,
                    EdgeOpeningBias = 0.25f,
                    OuterBandTarget = 0.88f,
                    MaxInitialMovableChains = 6,
                    MaxShortEdgePatchChains = 4,
                    MaxShortFillPatchChains = 5,
                    Seed = 461003
                },
                new StyleSpec
                {
                    Type = NoMaskType.SectionUnlock,
                    Id = "level3_tiny_hole_section_17x18",
                    DisplayName = "Front Hole Section 17x18",
                    Width = 17,
                    Height = 18,
                    TargetChains = 20,
                    MinLength = 6,
                    MaxLength = 14,
                    TargetCoverage = 0.82f,
                    TurnBias = 0.58f,
                    BlockWeight = 4.2f,
                    EdgeOpeningBias = 0.28f,
                    OuterBandTarget = 0.88f,
                    MaxInitialMovableChains = 7,
                    MaxShortEdgePatchChains = 4,
                    MaxShortFillPatchChains = 5,
                    Seed = 461079
                }
            };
        }

        private static StyleSpec[] CreateDirectHoleCandidatePoolSpecs()
        {
            var list = new List<StyleSpec>(112)
            {
                CreateDirectHoleSpec(NoMaskType.LockBuckle, "front20_01_lock_16x18", "Front20-01 Hole Lock", 16, 18, 18, 461003, 0.94f, 0.90f, 6, 0),
                CreateDirectHoleSpec(NoMaskType.SectionUnlock, "front20_02_section_17x18", "Front20-02 Hole Section", 17, 18, 20, 461079, 0.94f, 0.90f, 7, 0)
            };

            int[,] standard =
            {
                { 18, 22 }, { 18, 24 }, { 20, 22 }, { 20, 24 }, { 20, 26 }, { 22, 24 },
                { 22, 26 }, { 22, 28 }, { 24, 26 }, { 24, 28 }, { 24, 30 }, { 26, 24 },
                { 26, 26 }, { 26, 28 }, { 28, 24 }, { 28, 26 }, { 28, 28 }, { 30, 30 }
            };

            int[,] tall =
            {
                { 18, 26 }, { 20, 28 }, { 20, 30 }, { 28, 30 }
            };

            int[,] wide =
            {
                { 26, 22 }, { 28, 22 }, { 30, 24 }, { 30, 26 }, { 30, 28 },
                { 32, 24 }, { 32, 26 }, { 32, 28 }, { 32, 30 }, { 34, 26 }, { 34, 28 }
            };

            int seed = 472001;
            AppendHoleSizeCandidates(list, standard, "std", seed, includeThirdStructure: true, outerCutKind: 0);
            seed += 50000;
            AppendHoleSizeCandidates(list, tall, "tall", seed, includeThirdStructure: true, outerCutKind: 0);
            seed += 50000;
            AppendHoleSizeCandidates(list, wide, "wide", seed, includeThirdStructure: true, outerCutKind: 0);

            list.Add(CreateDirectHoleSpec(
                NoMaskType.LockBuckle,
                "notch_24x28_lock_a",
                "Hole Notch 24x28 Lock",
                24,
                28,
                EstimateHoleTargetChains(24, 28, 0, NoMaskType.LockBuckle),
                seed + 9001,
                0.965f,
                0.925f,
                EstimateHoleMaxInitial(EstimateHoleTargetChains(24, 28, 0, NoMaskType.LockBuckle), NoMaskType.LockBuckle),
                1));
            list.Add(CreateDirectHoleSpec(
                NoMaskType.SectionUnlock,
                "notch_24x28_section_b",
                "Hole Notch 24x28 Section",
                24,
                28,
                EstimateHoleTargetChains(24, 28, 0, NoMaskType.SectionUnlock),
                seed + 9107,
                0.965f,
                0.925f,
                EstimateHoleMaxInitial(EstimateHoleTargetChains(24, 28, 0, NoMaskType.SectionUnlock), NoMaskType.SectionUnlock),
                1));

            return list.ToArray();
        }

        private static StyleSpec[] CreateDirectHoleTopupCandidateSpecs()
        {
            var list = new List<StyleSpec>(64);
            NoMaskType[] richTypes =
            {
                NoMaskType.DenseWeave,
                NoMaskType.DualZone,
                NoMaskType.MazeLongChain,
                NoMaskType.OuterShell
            };
            NoMaskType[] stableTypes =
            {
                NoMaskType.DenseWeave,
                NoMaskType.DualZone,
                NoMaskType.MazeLongChain
            };

            int[,] tall =
            {
                { 18, 26 }, { 20, 28 }, { 20, 30 }, { 28, 30 }
            };

            int[,] standard =
            {
                { 22, 26 }, { 22, 28 }, { 24, 28 }, { 24, 30 },
                { 26, 26 }, { 26, 28 }, { 28, 28 }, { 30, 30 }
            };

            int[,] wide =
            {
                { 26, 22 }, { 28, 22 }, { 30, 24 }, { 30, 26 },
                { 32, 24 }, { 34, 26 }
            };

            AppendHoleTopupSizeCandidates(list, tall, "topup_tall", 681001, richTypes, outerCutKind: 0);
            AppendHoleTopupSizeCandidates(list, standard, "topup_std", 711001, stableTypes, outerCutKind: 0);
            AppendHoleTopupSizeCandidates(list, wide, "topup_wide", 751001, stableTypes, outerCutKind: 0);

            int seed = 791001;
            AddHoleCandidate(list, NoMaskType.DenseWeave, "topup_notch", 24, 28, "dense_a", seed + 11, 0.968f, 0.925f, outerCutKind: 1);
            AddHoleCandidate(list, NoMaskType.DualZone, "topup_notch", 24, 28, "dual_a", seed + 97, 0.968f, 0.925f, outerCutKind: 1);
            AddHoleCandidate(list, NoMaskType.MazeLongChain, "topup_notch", 24, 28, "maze_a", seed + 193, 0.968f, 0.925f, outerCutKind: 1);
            AddHoleCandidate(list, NoMaskType.OuterShell, "topup_notch", 24, 28, "shell_a", seed + 389, 0.968f, 0.925f, outerCutKind: 1);
            AddHoleCandidate(list, NoMaskType.DenseWeave, "topup_notch", 24, 28, "dense_b", seed + 587, 0.968f, 0.925f, outerCutKind: 1);
            AddHoleCandidate(list, NoMaskType.DualZone, "topup_notch", 24, 28, "dual_b", seed + 769, 0.968f, 0.925f, outerCutKind: 1);

            return list.ToArray();
        }

        private static StyleSpec[] CreateDirectHoleHighChainCandidateSpecs()
        {
            var list = new List<StyleSpec>(24);
            AddHoleHighChainCandidate(list, NoMaskType.DenseWeave, "hc_30x30_dense_a", 30, 30, 88, 831011);
            AddHoleHighChainCandidate(list, NoMaskType.DualZone, "hc_30x30_dual_a", 30, 30, 86, 831097);
            AddHoleHighChainCandidate(list, NoMaskType.DenseWeave, "hc_32x28_dense_a", 32, 28, 88, 832011);
            AddHoleHighChainCandidate(list, NoMaskType.DualZone, "hc_32x28_dual_a", 32, 28, 86, 832097);
            AddHoleHighChainCandidate(list, NoMaskType.MazeLongChain, "hc_32x28_maze_a", 32, 28, 82, 832193);
            AddHoleHighChainCandidate(list, NoMaskType.DenseWeave, "hc_32x30_dense_a", 32, 30, 96, 833011);
            AddHoleHighChainCandidate(list, NoMaskType.DualZone, "hc_32x30_dual_a", 32, 30, 92, 833097);
            AddHoleHighChainCandidate(list, NoMaskType.OuterShell, "hc_32x30_shell_a", 32, 30, 88, 833389);
            AddHoleHighChainCandidate(list, NoMaskType.DenseWeave, "hc_34x26_dense_a", 34, 26, 88, 834011);
            AddHoleHighChainCandidate(list, NoMaskType.DualZone, "hc_34x26_dual_a", 34, 26, 86, 834097);
            AddHoleHighChainCandidate(list, NoMaskType.MazeLongChain, "hc_34x26_maze_a", 34, 26, 82, 834193);
            AddHoleHighChainCandidate(list, NoMaskType.DenseWeave, "hc_34x28_dense_a", 34, 28, 96, 835011);
            AddHoleHighChainCandidate(list, NoMaskType.DualZone, "hc_34x28_dual_a", 34, 28, 92, 835097);
            AddHoleHighChainCandidate(list, NoMaskType.OuterShell, "hc_34x28_shell_a", 34, 28, 88, 835389);
            AddHoleHighChainCandidate(list, NoMaskType.DenseWeave, "hc_30x28_dense_a", 30, 28, 84, 836011);
            AddHoleHighChainCandidate(list, NoMaskType.DualZone, "hc_30x28_dual_a", 30, 28, 82, 836097);
            AddHoleHighChainCandidate(list, NoMaskType.DenseWeave, "hc_28x30_dense_a", 28, 30, 84, 837011);
            AddHoleHighChainCandidate(list, NoMaskType.DualZone, "hc_28x30_dual_a", 28, 30, 82, 837097);
            return list.ToArray();
        }

        private static StyleSpec[] CreateDirectHoleHighChainExtraCandidateSpecs()
        {
            var list = new List<StyleSpec>(32);
            AddHoleHighChainCandidate(list, NoMaskType.DualZone, "hc2_32x28_dual_b", 32, 28, 86, 841097);
            AddHoleHighChainCandidate(list, NoMaskType.SectionUnlock, "hc2_32x28_section_a", 32, 28, 86, 841211);
            AddHoleHighChainCandidate(list, NoMaskType.LockBuckle, "hc2_32x28_lock_a", 32, 28, 86, 841337);
            AddHoleHighChainCandidate(list, NoMaskType.DualZone, "hc2_34x26_dual_b", 34, 26, 86, 842097);
            AddHoleHighChainCandidate(list, NoMaskType.SectionUnlock, "hc2_34x26_section_a", 34, 26, 86, 842211);
            AddHoleHighChainCandidate(list, NoMaskType.LockBuckle, "hc2_34x26_lock_a", 34, 26, 86, 842337);
            AddHoleHighChainCandidate(list, NoMaskType.DualZone, "hc2_30x28_dual_b", 30, 28, 82, 843097);
            AddHoleHighChainCandidate(list, NoMaskType.SectionUnlock, "hc2_30x28_section_a", 30, 28, 82, 843211);
            AddHoleHighChainCandidate(list, NoMaskType.LockBuckle, "hc2_30x28_lock_a", 30, 28, 82, 843337);
            AddHoleHighChainCandidate(list, NoMaskType.DualZone, "hc2_28x30_dual_b", 28, 30, 82, 844097);
            AddHoleHighChainCandidate(list, NoMaskType.SectionUnlock, "hc2_28x30_section_a", 28, 30, 82, 844211);
            AddHoleHighChainCandidate(list, NoMaskType.LockBuckle, "hc2_28x30_lock_a", 28, 30, 82, 844337);
            AddHoleHighChainCandidate(list, NoMaskType.DualZone, "hc2_30x30_dual_b", 30, 30, 88, 845097);
            AddHoleHighChainCandidate(list, NoMaskType.SectionUnlock, "hc2_30x30_section_a", 30, 30, 88, 845211);
            AddHoleHighChainCandidate(list, NoMaskType.LockBuckle, "hc2_30x30_lock_a", 30, 30, 88, 845337);
            AddHoleHighChainCandidate(list, NoMaskType.DualZone, "hc2_32x30_dual_b", 32, 30, 94, 846097);
            AddHoleHighChainCandidate(list, NoMaskType.SectionUnlock, "hc2_32x30_section_a", 32, 30, 94, 846211);
            AddHoleHighChainCandidate(list, NoMaskType.LockBuckle, "hc2_32x30_lock_a", 32, 30, 94, 846337);
            AddHoleHighChainCandidate(list, NoMaskType.DualZone, "hc2_34x28_dual_b", 34, 28, 94, 847097);
            AddHoleHighChainCandidate(list, NoMaskType.SectionUnlock, "hc2_34x28_section_a", 34, 28, 94, 847211);
            AddHoleHighChainCandidate(list, NoMaskType.LockBuckle, "hc2_34x28_lock_a", 34, 28, 94, 847337);
            AddHoleHighChainCandidate(list, NoMaskType.DenseWeave, "hc2_28x30_dense_b", 28, 30, 84, 848011);
            AddHoleHighChainCandidate(list, NoMaskType.DenseWeave, "hc2_30x28_dense_b", 30, 28, 84, 848113);
            AddHoleHighChainCandidate(list, NoMaskType.OuterShell, "hc2_34x28_shell_b", 34, 28, 88, 848389);
            return list.ToArray();
        }

        private static void AddHoleHighChainCandidate(
            List<StyleSpec> list,
            NoMaskType type,
            string id,
            int width,
            int height,
            int targetChains,
            int seed)
        {
            StyleSpec spec = CreateDirectHoleSpec(
                type,
                id,
                $"Hole High Chain {id}",
                width,
                height,
                targetChains,
                seed,
                0.972f,
                0.935f,
                EstimateHoleMaxInitial(targetChains, type),
                outerCutKind: 0);
            spec.MinLength = type == NoMaskType.MazeLongChain ? 7 : 6;
            spec.MaxLength = type == NoMaskType.MazeLongChain ? 24 : 19;
            list.Add(spec);
        }

        private static void AppendHoleTopupSizeCandidates(
            List<StyleSpec> list,
            int[,] sizes,
            string group,
            int seedBase,
            NoMaskType[] types,
            int outerCutKind)
        {
            if (types == null || types.Length == 0)
                return;

            for (int i = 0; i < sizes.GetLength(0); i++)
            {
                int width = sizes[i, 0];
                int height = sizes[i, 1];
                int areaRank = width * height;
                float targetCoverage = areaRank >= 780 ? 0.974f : areaRank >= 620 ? 0.970f : 0.966f;
                float outerTarget = areaRank >= 780 ? 0.940f : 0.930f;
                int seed = seedBase + i * 1487;
                for (int t = 0; t < types.Length; t++)
                    AddHoleCandidate(list, types[t], group, width, height, GetHoleTopupVariantName(types[t]), seed + t * 173, targetCoverage, outerTarget, outerCutKind);
            }
        }

        private static string GetHoleTopupVariantName(NoMaskType type)
        {
            return type == NoMaskType.DenseWeave ? "dense"
                : type == NoMaskType.DualZone ? "dual"
                : type == NoMaskType.MazeLongChain ? "maze"
                : type == NoMaskType.OuterShell ? "shell"
                : type.ToString().ToLowerInvariant();
        }

        private static void AppendHoleSizeCandidates(
            List<StyleSpec> list,
            int[,] sizes,
            string group,
            int seedBase,
            bool includeThirdStructure,
            int outerCutKind)
        {
            for (int i = 0; i < sizes.GetLength(0); i++)
            {
                int width = sizes[i, 0];
                int height = sizes[i, 1];
                int areaRank = width * height;
                float targetCoverage = areaRank >= 780 ? 0.972f : areaRank >= 620 ? 0.968f : 0.962f;
                float outerTarget = areaRank >= 780 ? 0.940f : 0.930f;
                int seed = seedBase + i * 1009;

                AddHoleCandidate(list, NoMaskType.LockBuckle, group, width, height, "lock", seed + 11, targetCoverage, outerTarget, outerCutKind);
                AddHoleCandidate(list, NoMaskType.SectionUnlock, group, width, height, "section", seed + 97, targetCoverage, outerTarget, outerCutKind);
                if (includeThirdStructure)
                {
                    NoMaskType third = i % 4 == 0 ? NoMaskType.MazeLongChain
                        : i % 4 == 1 ? NoMaskType.DenseWeave
                        : i % 4 == 2 ? NoMaskType.OuterShell
                        : NoMaskType.DualZone;
                    string thirdId = third == NoMaskType.MazeLongChain ? "maze"
                        : third == NoMaskType.DenseWeave ? "dense"
                        : third == NoMaskType.OuterShell ? "shell"
                        : "dual";
                    AddHoleCandidate(list, third, group, width, height, thirdId, seed + 193, targetCoverage, outerTarget, outerCutKind);
                }
            }
        }

        private static void AddHoleCandidate(
            List<StyleSpec> list,
            NoMaskType type,
            string group,
            int width,
            int height,
            string variant,
            int seed,
            float targetCoverage,
            float outerBandTarget,
            int outerCutKind)
        {
            int targetChains = EstimateHoleTargetChains(width, height, outerCutKind, type);
            list.Add(CreateDirectHoleSpec(
                type,
                $"{group}_{width}x{height}_{variant}",
                $"Hole {group} {width}x{height} {variant}",
                width,
                height,
                targetChains,
                seed,
                targetCoverage,
                outerBandTarget,
                EstimateHoleMaxInitial(targetChains, type),
                outerCutKind));
        }

        private static StyleSpec CreateDirectHoleSpec(
            NoMaskType type,
            string id,
            string displayName,
            int width,
            int height,
            int targetChains,
            int seed,
            float targetCoverage,
            float outerBandTarget,
            int maxInitial,
            int outerCutKind)
        {
            return new StyleSpec
            {
                Type = type,
                Id = id,
                DisplayName = displayName,
                Width = width,
                Height = height,
                TargetChains = targetChains,
                MinLength = type == NoMaskType.MazeLongChain ? 8 : 6,
                MaxLength = type == NoMaskType.MazeLongChain ? 26 : type == NoMaskType.OuterShell ? 22 : 20,
                TargetCoverage = targetCoverage,
                TurnBias = type == NoMaskType.MazeLongChain ? 0.68f : type == NoMaskType.DenseWeave ? 0.66f : 0.60f,
                BlockWeight = type == NoMaskType.LockBuckle ? 5.0f : type == NoMaskType.SectionUnlock ? 4.4f : 3.8f,
                EdgeOpeningBias = type == NoMaskType.OuterShell ? 0.20f : 0.24f,
                OuterBandTarget = outerBandTarget,
                MaxInitialMovableChains = maxInitial,
                MaxShortEdgePatchChains = 4,
                MaxShortFillPatchChains = 8,
                Seed = seed,
                OuterCutKind = outerCutKind
            };
        }

        private static int EstimateHoleTargetChains(int width, int height, int outerCutKind, NoMaskType type)
        {
            int cutEstimate = outerCutKind == 1 ? 18 : 0;
            int allowed = Mathf.Max(1, width * height - 72 - cutEstimate);
            float avgLength = type == NoMaskType.MazeLongChain ? 11.6f
                : type == NoMaskType.OuterShell ? 10.8f
                : type == NoMaskType.DenseWeave ? 9.0f
                : type == NoMaskType.DualZone ? 9.8f
                : 9.4f;
            int estimate = Mathf.RoundToInt(allowed / avgLength);
            return Mathf.Clamp(estimate, 18, 112);
        }

        private static int EstimateHoleMaxInitial(int targetChains, NoMaskType type)
        {
            float ratio = type == NoMaskType.OuterShell ? 0.20f : type == NoMaskType.MazeLongChain ? 0.22f : 0.24f;
            return Mathf.Clamp(Mathf.CeilToInt(targetChains * ratio), 6, 22);
        }

        private static StyleSpec[] CreateDirectNormalRectangleSpecs()
        {
            NoMaskType[] types =
            {
                NoMaskType.OuterShell,
                NoMaskType.SectionUnlock,
                NoMaskType.LockBuckle,
                NoMaskType.DenseWeave,
                NoMaskType.Sweep,
                NoMaskType.CoreBurst,
                NoMaskType.DualZone,
                NoMaskType.StairPush,
                NoMaskType.QuasiSymmetry,
                NoMaskType.MazeLongChain
            };
            string[] ids =
            {
                "outer_shell",
                "section_unlock",
                "lock_buckle",
                "dense_weave",
                "sweep",
                "core_burst",
                "dual_zone",
                "stair_push",
                "quasi_symmetry",
                "maze_long_chain"
            };
            string[] display =
            {
                "Outer Shell",
                "Section Unlock",
                "Lock Buckle",
                "Dense Weave",
                "Sweep",
                "Core Burst",
                "Dual Zone",
                "Stair Push",
                "Quasi Symmetry",
                "Maze Long Chain"
            };

            var list = new List<StyleSpec>(160);
            int seed = 93001;
            for (int variant = 0; variant < 16; variant++)
            {
                for (int i = 0; i < types.Length; i++)
                {
                    int width = 20 + ((variant + i * 2) % 4);
                    int height = 34 + ((variant * 2 + i * 3) % 7);
                    if (types[i] == NoMaskType.MazeLongChain || types[i] == NoMaskType.Sweep)
                        height += 1;
                    if (variant >= 10)
                        height += 1;

                    int targetChains = Mathf.Clamp(106 + (variant % 5) * 3 + (i % 4) * 2, 106, 118);
                    int maxLength = Mathf.Clamp(8 + (i % 3) + (variant % 2), 8, 11);
                    list.Add(new StyleSpec
                    {
                        Type = types[i],
                        Id = $"direct_normal_rect_{ids[i]}_g{variant % 6}_v{variant + 1:00}",
                        DisplayName = $"Direct Normal {display[i]} {variant + 1:00}",
                        Width = width,
                        Height = height,
                        TargetChains = targetChains,
                        MinLength = 3,
                        MaxLength = maxLength,
                        TargetCoverage = 0.975f,
                        TurnBias = types[i] == NoMaskType.MazeLongChain ? 0.68f : 0.56f,
                        BlockWeight = types[i] == NoMaskType.LockBuckle ? 4.7f : 3.2f,
                        EdgeOpeningBias = 0.26f,
                        OuterBandTarget = 0.95f,
                        MaxInitialMovableChains = Mathf.CeilToInt(targetChains * 0.26f),
                        MaxShortEdgePatchChains = 5,
                        MaxShortFillPatchChains = 8,
                        Seed = seed + variant * 1009 + i * 43
                    });
                }
            }

            return list.ToArray();
        }

        private static StyleSpec[] CreateDirectNormalTopupRectangleSpecs()
        {
            NoMaskType[] types =
            {
                NoMaskType.SectionUnlock,
                NoMaskType.LockBuckle,
                NoMaskType.DenseWeave,
                NoMaskType.CoreBurst,
                NoMaskType.DualZone,
                NoMaskType.StairPush,
                NoMaskType.QuasiSymmetry,
                NoMaskType.Sweep
            };
            string[] ids =
            {
                "section_unlock",
                "lock_buckle",
                "dense_weave",
                "core_burst",
                "dual_zone",
                "stair_push",
                "quasi_symmetry",
                "sweep"
            };
            string[] display =
            {
                "Section Unlock",
                "Lock Buckle",
                "Dense Weave",
                "Core Burst",
                "Dual Zone",
                "Stair Push",
                "Quasi Symmetry",
                "Sweep"
            };

            var list = new List<StyleSpec>(80);
            int seed = 97001;
            for (int variant = 0; variant < 10; variant++)
            {
                for (int i = 0; i < types.Length; i++)
                {
                    int width = 20 + ((variant * 3 + i) % 4);
                    int height = 34 + ((variant + i * 2) % 6);
                    if (types[i] == NoMaskType.Sweep)
                        height += 1;
                    if (variant >= 6)
                        height += 1;

                    int targetChains = Mathf.Clamp(104 + (variant % 4) * 3 + (i % 3) * 2, 104, 116);
                    list.Add(new StyleSpec
                    {
                        Type = types[i],
                        Id = $"direct_normal_topup_rect_{ids[i]}_g{6 + (variant % 6)}_v{variant + 1:00}",
                        DisplayName = $"Direct Normal Topup {display[i]} {variant + 1:00}",
                        Width = width,
                        Height = height,
                        TargetChains = targetChains,
                        MinLength = 3,
                        MaxLength = 9 + (i % 2),
                        TargetCoverage = 0.975f,
                        TurnBias = 0.56f,
                        BlockWeight = types[i] == NoMaskType.LockBuckle ? 4.7f : 3.2f,
                        EdgeOpeningBias = 0.25f,
                        OuterBandTarget = 0.95f,
                        MaxInitialMovableChains = Mathf.CeilToInt(targetChains * 0.25f),
                        MaxShortEdgePatchChains = 5,
                        MaxShortFillPatchChains = 8,
                        Seed = seed + variant * 1031 + i * 47
                    });
                }
            }

            return list.ToArray();
        }

        private static StyleSpec[] CreateDirectAdvancedRectangleSpecs()
        {
            NoMaskType[] types =
            {
                NoMaskType.LockBuckle,
                NoMaskType.DenseWeave,
                NoMaskType.CoreBurst,
                NoMaskType.DualZone,
                NoMaskType.StairPush,
                NoMaskType.QuasiSymmetry,
                NoMaskType.Sweep,
                NoMaskType.SectionUnlock
            };
            string[] ids =
            {
                "lock_buckle",
                "dense_weave",
                "core_burst",
                "dual_zone",
                "stair_push",
                "quasi_symmetry",
                "sweep",
                "section_unlock"
            };
            string[] display =
            {
                "Lock Buckle",
                "Dense Weave",
                "Core Burst",
                "Dual Zone",
                "Stair Push",
                "Quasi Symmetry",
                "Sweep",
                "Section Unlock"
            };

            var list = new List<StyleSpec>(72);
            int seed = 101001;
            for (int variant = 0; variant < 6; variant++)
            {
                for (int i = 0; i < types.Length; i++)
                {
                    int width = 24 + ((variant + i * 2) % 5);
                    int height = 44 + ((variant * 3 + i) % 8);
                    int targetChains = Mathf.Clamp(190 + (variant % 4) * 8 + (i % 4) * 5, 190, 225);
                    list.Add(CreateDirectAdvancedSpec(
                        types[i],
                        $"direct_advanced_veryhard_rect_{ids[i]}_g{variant % 4}_v{variant + 1:00}",
                        $"Direct Advanced Very Hard {display[i]} {variant + 1:00}",
                        width,
                        height,
                        targetChains,
                        seed + variant * 1201 + i * 53));
                }
            }

            for (int variant = 0; variant < 3; variant++)
            {
                for (int i = 0; i < types.Length; i++)
                {
                    int width = 30 + ((variant + i) % 5);
                    int height = 54 + ((variant * 2 + i * 3) % 7);
                    int targetChains = Mathf.Clamp(275 + variant * 12 + (i % 4) * 6, 275, 315);
                    list.Add(CreateDirectAdvancedSpec(
                        types[i],
                        $"direct_advanced_extreme_rect_{ids[i]}_g{4 + (variant % 3)}_v{variant + 1:00}",
                        $"Direct Advanced Extreme {display[i]} {variant + 1:00}",
                        width,
                        height,
                        targetChains,
                        seed + 50000 + variant * 1601 + i * 59));
                }
            }

            return list.ToArray();
        }

        private static StyleSpec CreateDirectAdvancedSpec(
            NoMaskType type,
            string id,
            string displayName,
            int width,
            int height,
            int targetChains,
            int seed)
        {
            return new StyleSpec
            {
                Type = type,
                Id = id,
                DisplayName = displayName,
                Width = width,
                Height = height,
                TargetChains = targetChains,
                MinLength = 3,
                MaxLength = targetChains >= 240 ? 8 : 9,
                TargetCoverage = 0.972f,
                TurnBias = type == NoMaskType.Sweep ? 0.50f : 0.58f,
                BlockWeight = type == NoMaskType.LockBuckle ? 4.8f : 3.3f,
                EdgeOpeningBias = 0.22f,
                OuterBandTarget = 0.94f,
                MaxInitialMovableChains = Mathf.CeilToInt(targetChains * 0.20f),
                MaxShortEdgePatchChains = 5,
                MaxShortFillPatchChains = 8,
                Seed = seed
            };
        }

        private static StyleSpec[] CreateDirectPolishRectangleSpecs()
        {
            var list = new List<StyleSpec>(104);

            NoMaskType[] cleanTypes =
            {
                NoMaskType.SectionUnlock,
                NoMaskType.LockBuckle,
                NoMaskType.DenseWeave,
                NoMaskType.CoreBurst,
                NoMaskType.DualZone,
                NoMaskType.StairPush,
                NoMaskType.QuasiSymmetry,
                NoMaskType.Sweep,
                NoMaskType.MazeLongChain
            };
            string[] cleanIds =
            {
                "section_unlock",
                "lock_buckle",
                "dense_weave",
                "core_burst",
                "dual_zone",
                "stair_push",
                "quasi_symmetry",
                "sweep",
                "maze_long_chain"
            };
            string[] cleanDisplay =
            {
                "Section Unlock",
                "Lock Buckle",
                "Dense Weave",
                "Core Burst",
                "Dual Zone",
                "Stair Push",
                "Quasi Symmetry",
                "Sweep",
                "Maze Long Chain"
            };

            int seed = 132001;
            for (int variant = 0; variant < 4; variant++)
            {
                for (int i = 0; i < cleanTypes.Length; i++)
                {
                    int width = 20 + ((variant * 2 + i) % 4);
                    int height = 36 + ((variant + i * 3) % 6);
                    int targetChains = Mathf.Clamp(106 + (variant % 3) * 3 + (i % 4) * 2, 106, 118);
                    list.Add(CreateDirectPolishSpec(
                        cleanTypes[i],
                        $"direct_polish_normal_rect_{cleanIds[i]}_g{variant % 4}_v{variant + 1:00}",
                        $"Direct Polish Normal {cleanDisplay[i]} {variant + 1:00}",
                        width,
                        height,
                        targetChains,
                        seed + variant * 1103 + i * 41,
                        0.980f,
                        0.965f,
                        cleanTypes[i] == NoMaskType.MazeLongChain ? 10 : 9,
                        0.20f));
                }
            }

            seed = 145001;
            for (int variant = 0; variant < 5; variant++)
            {
                for (int i = 0; i < cleanTypes.Length; i++)
                {
                    int width = 23 + ((variant * 3 + i) % 5);
                    int height = 40 + ((variant * 2 + i * 3) % 8);
                    int targetChains = Mathf.Clamp(126 + (variant % 5) * 6 + (i % 4) * 4, 126, 168);
                    list.Add(CreateDirectPolishSpec(
                        cleanTypes[i],
                        $"direct_polish_hard_rect_{cleanIds[i]}_g{4 + (variant % 5)}_v{variant + 1:00}",
                        $"Direct Polish Hard {cleanDisplay[i]} {variant + 1:00}",
                        width,
                        height,
                        targetChains,
                        seed + variant * 1301 + i * 47,
                        0.980f,
                        0.965f,
                        cleanTypes[i] == NoMaskType.MazeLongChain ? 11 : 10,
                        0.18f));
                }
            }

            NoMaskType[] extremeTypes =
            {
                NoMaskType.MazeLongChain,
                NoMaskType.MazeLongChain,
                NoMaskType.MazeLongChain,
                NoMaskType.MazeLongChain,
                NoMaskType.Sweep,
                NoMaskType.OuterShell
            };
            string[] extremeIds =
            {
                "maze_long_chain",
                "long_corridor",
                "hui_spiral",
                "snake_spine",
                "long_sweep",
                "outer_ribbon"
            };
            string[] extremeDisplay =
            {
                "Maze Long Chain",
                "Long Corridor",
                "Hui Spiral",
                "Snake Spine",
                "Long Sweep",
                "Outer Ribbon"
            };

            seed = 161001;
            for (int variant = 0; variant < 4; variant++)
            {
                for (int i = 0; i < extremeTypes.Length; i++)
                {
                    int width = 34 + ((variant + i * 2) % 5);
                    int height = 60 + ((variant * 3 + i) % 8);
                    int targetChains = Mathf.Clamp(256 + variant * 9 + (i % 3) * 5, 256, 296);
                    list.Add(CreateDirectPolishSpec(
                        extremeTypes[i],
                        $"direct_polish_extreme_rect_{extremeIds[i]}_g{9 + (variant % 4)}_v{variant + 1:00}",
                        $"Direct Polish Extreme {extremeDisplay[i]} {variant + 1:00}",
                        width,
                        height,
                        targetChains,
                        seed + variant * 1601 + i * 59,
                        0.978f,
                        0.960f,
                        extremeTypes[i] == NoMaskType.MazeLongChain ? 13 : 12,
                        0.08f));
                }
            }

            return list.ToArray();
        }

        private static StyleSpec CreateDirectPolishSpec(
            NoMaskType type,
            string id,
            string displayName,
            int width,
            int height,
            int targetChains,
            int seed,
            float targetCoverage,
            float outerBandTarget,
            int maxLength,
            float edgeOpeningBias)
        {
            return new StyleSpec
            {
                Type = type,
                Id = id,
                DisplayName = displayName,
                Width = width,
                Height = height,
                TargetChains = targetChains,
                MinLength = 3,
                MaxLength = maxLength,
                TargetCoverage = targetCoverage,
                TurnBias = type == NoMaskType.MazeLongChain ? 0.70f : type == NoMaskType.Sweep ? 0.52f : 0.58f,
                BlockWeight = type == NoMaskType.LockBuckle ? 4.8f : 3.4f,
                EdgeOpeningBias = edgeOpeningBias,
                OuterBandTarget = outerBandTarget,
                MaxInitialMovableChains = Mathf.CeilToInt(targetChains * (id.Contains("_extreme_") ? 0.12f : 0.20f)),
                MaxShortEdgePatchChains = 4,
                MaxShortFillPatchChains = 10,
                Seed = seed
            };
        }

        private static StyleSpec[] CreateDirectPureTopupRectangleSpecs()
        {
            var list = new List<StyleSpec>(288);
            NoMaskType[] lightTypes =
            {
                NoMaskType.OuterShell,
                NoMaskType.SectionUnlock,
                NoMaskType.LockBuckle,
                NoMaskType.DenseWeave,
                NoMaskType.Sweep,
                NoMaskType.CoreBurst,
                NoMaskType.DualZone,
                NoMaskType.MazeLongChain
            };
            string[] lightIds =
            {
                "outer_shell",
                "section_unlock",
                "lock_buckle",
                "dense_weave",
                "sweep",
                "core_burst",
                "dual_zone",
                "maze_long_chain"
            };
            string[] lightDisplay =
            {
                "Outer Shell",
                "Section Unlock",
                "Lock Buckle",
                "Dense Weave",
                "Sweep",
                "Core Burst",
                "Dual Zone",
                "Maze Long Chain"
            };

            int seed = 181001;
            for (int variant = 0; variant < 9; variant++)
            {
                for (int i = 0; i < lightTypes.Length; i++)
                {
                    int width = 14 + ((variant + i * 2) % 4);
                    int height = 24 + ((variant * 2 + i) % 6);
                    int targetChains = Mathf.Clamp(42 + (variant % 4) * 2 + (i % 3), 42, 53);
                    list.Add(CreateDirectPolishSpec(
                        lightTypes[i],
                        $"direct_refresh_topup_rect_{lightIds[i]}_g{variant % 6}_v{variant + 1:00}",
                        $"Direct Refresh Topup {lightDisplay[i]} {variant + 1:00}",
                        width,
                        height,
                        targetChains,
                        seed + variant * 907 + i * 37,
                        0.982f,
                        0.968f,
                        lightTypes[i] == NoMaskType.MazeLongChain ? 9 : 8,
                        0.30f));
                }
            }

            seed = 193001;
            for (int variant = 0; variant < 9; variant++)
            {
                for (int i = 0; i < lightTypes.Length; i++)
                {
                    int width = 18 + ((variant * 3 + i) % 5);
                    int height = 31 + ((variant + i * 3) % 8);
                    int targetChains = Mathf.Clamp(84 + (variant % 5) * 5 + (i % 4) * 3, 84, 116);
                    list.Add(CreateDirectPolishSpec(
                        lightTypes[i],
                        $"direct_normal_pure_rect_{lightIds[i]}_g{6 + (variant % 6)}_v{variant + 1:00}",
                        $"Direct Normal Pure {lightDisplay[i]} {variant + 1:00}",
                        width,
                        height,
                        targetChains,
                        seed + variant * 1013 + i * 43,
                        0.980f,
                        0.965f,
                        lightTypes[i] == NoMaskType.MazeLongChain ? 10 : 9,
                        0.24f));
                }
            }

            NoMaskType[] hardTypes =
            {
                NoMaskType.SectionUnlock,
                NoMaskType.LockBuckle,
                NoMaskType.DenseWeave,
                NoMaskType.CoreBurst,
                NoMaskType.DualZone,
                NoMaskType.StairPush,
                NoMaskType.QuasiSymmetry,
                NoMaskType.Sweep,
                NoMaskType.MazeLongChain,
                NoMaskType.OuterShell
            };
            string[] hardIds =
            {
                "section_unlock",
                "lock_buckle",
                "dense_weave",
                "core_burst",
                "dual_zone",
                "stair_push",
                "quasi_symmetry",
                "sweep",
                "maze_long_chain",
                "outer_shell"
            };
            string[] hardDisplay =
            {
                "Section Unlock",
                "Lock Buckle",
                "Dense Weave",
                "Core Burst",
                "Dual Zone",
                "Stair Push",
                "Quasi Symmetry",
                "Sweep",
                "Maze Long Chain",
                "Outer Shell"
            };

            seed = 207001;
            for (int variant = 0; variant < 12; variant++)
            {
                for (int i = 0; i < hardTypes.Length; i++)
                {
                    int width = 21 + ((variant * 2 + i) % 5);
                    int height = 38 + ((variant * 3 + i * 2) % 7);
                    int targetChains = Mathf.Clamp(112 + (variant % 6) * 4 + (i % 5) * 2, 112, 142);
                    list.Add(CreateDirectPolishSpec(
                        hardTypes[i],
                        $"direct_polish_hard_pure_rect_{hardIds[i]}_g{9 + (variant % 6)}_v{variant + 1:00}",
                        $"Direct Polish Hard Pure {hardDisplay[i]} {variant + 1:00}",
                        width,
                        height,
                        targetChains,
                        seed + variant * 1217 + i * 47,
                        0.980f,
                        0.966f,
                        hardTypes[i] == NoMaskType.MazeLongChain ? 11 : 10,
                        0.17f));
                }
            }

            seed = 223001;
            for (int variant = 0; variant < 3; variant++)
            {
                for (int i = 0; i < lightTypes.Length; i++)
                {
                    int width = 12 + ((variant + i) % 4);
                    int height = 21 + ((variant * 2 + i) % 5);
                    int targetChains = Mathf.Clamp(34 + variant * 3 + (i % 3), 34, 43);
                    list.Add(CreateDirectPolishSpec(
                        lightTypes[i],
                        $"direct_refresh_low_rect_{lightIds[i]}_g{12 + variant}_v{variant + 1:00}",
                        $"Direct Refresh Low {lightDisplay[i]} {variant + 1:00}",
                        width,
                        height,
                        targetChains,
                        seed + variant * 887 + i * 31,
                        0.982f,
                        0.968f,
                        lightTypes[i] == NoMaskType.MazeLongChain ? 8 : 7,
                        0.34f));
                }
            }

            return list.ToArray();
        }

        private static StyleSpec[] CreateDirectPureNormalExtraRectangleSpecs()
        {
            NoMaskType[] types =
            {
                NoMaskType.SectionUnlock,
                NoMaskType.LockBuckle,
                NoMaskType.DenseWeave,
                NoMaskType.CoreBurst,
                NoMaskType.DualZone,
                NoMaskType.StairPush,
                NoMaskType.QuasiSymmetry,
                NoMaskType.MazeLongChain
            };
            string[] ids =
            {
                "section_unlock",
                "lock_buckle",
                "dense_weave",
                "core_burst",
                "dual_zone",
                "stair_push",
                "quasi_symmetry",
                "maze_long_chain"
            };
            string[] display =
            {
                "Section Unlock",
                "Lock Buckle",
                "Dense Weave",
                "Core Burst",
                "Dual Zone",
                "Stair Push",
                "Quasi Symmetry",
                "Maze Long Chain"
            };

            var list = new List<StyleSpec>(96);
            int seed = 241001;
            for (int variant = 0; variant < 12; variant++)
            {
                for (int i = 0; i < types.Length; i++)
                {
                    int width = 18 + ((variant * 2 + i) % 4);
                    int height = 31 + ((variant + i * 2) % 5);
                    int targetChains = Mathf.Clamp(108 + (variant % 4) * 3 + (i % 3) * 2, 108, 119);
                    StyleSpec spec = CreateDirectPolishSpec(
                        types[i],
                        $"direct_normal_pure_extra_rect_{ids[i]}_g{15 + (variant % 6)}_v{variant + 1:00}",
                        $"Direct Normal Pure Extra {display[i]} {variant + 1:00}",
                        width,
                        height,
                        targetChains,
                        seed + variant * 977 + i * 43,
                        0.980f,
                        0.966f,
                        types[i] == NoMaskType.MazeLongChain ? 10 : 9,
                        0.12f);
                    spec.MaxInitialMovableChains = Mathf.CeilToInt(targetChains * 0.16f);
                    list.Add(spec);
                }
            }

            return list.ToArray();
        }

        private static MotifSpec[] CreateDirectDifferentiationMotifSpecs()
        {
            var list = new List<MotifSpec>(160);
            AppendDirectDifferentiationBand(list, "refresh", 14, 24, 17, 20, 44, 2, 4, 8, 0.982f, 0.968f, 281001);
            AppendDirectDifferentiationBand(list, "normal", 18, 32, 23, 27, 78, 4, 3, 9, 0.980f, 0.966f, 291001);
            AppendDirectDifferentiationBand(list, "hard", 22, 40, 28, 33, 112, 5, 3, 10, 0.980f, 0.966f, 301001);
            AppendDirectDifferentiationBand(list, "veryhard", 25, 46, 32, 39, 145, 8, 3, 11, 0.978f, 0.962f, 311001);
            return list.ToArray();
        }

        private static void AppendDirectDifferentiationBand(
            List<MotifSpec> list,
            string band,
            int baseWidth,
            int baseHeight,
            int squareWidth,
            int squareHeight,
            int targetBase,
            int targetStep,
            int minLength,
            int maxLength,
            float targetCoverage,
            float outerBandTarget,
            int seedBase)
        {
            NoMaskMotifKind[] longMotifs =
            {
                NoMaskMotifKind.HuiSpiral,
                NoMaskMotifKind.SnakeSpine,
                NoMaskMotifKind.LongCorridor,
                NoMaskMotifKind.ZigRiver
            };
            string[] longIds = { "hui_spiral", "snake_spine", "long_corridor", "zig_river" };
            NoMaskType[] longTypes =
            {
                NoMaskType.MazeLongChain,
                NoMaskType.Sweep,
                NoMaskType.MazeLongChain,
                NoMaskType.Sweep
            };

            NoMaskMotifKind[] roomMotifs =
            {
                NoMaskMotifKind.DoubleRoomLock,
                NoMaskMotifKind.KeyDoor,
                NoMaskMotifKind.FourPockets,
                NoMaskMotifKind.VerticalGate
            };
            string[] roomIds = { "double_room_lock", "key_door", "four_pockets", "vertical_gate" };
            NoMaskType[] roomTypes =
            {
                NoMaskType.DualZone,
                NoMaskType.LockBuckle,
                NoMaskType.SectionUnlock,
                NoMaskType.CoreBurst
            };

            NoMaskMotifKind[] squareMotifs =
            {
                NoMaskMotifKind.CenterCross,
                NoMaskMotifKind.DoubleShell,
                NoMaskMotifKind.DenseKernel,
                NoMaskMotifKind.StairLadder
            };
            string[] squareIds = { "center_cross", "double_shell", "dense_kernel", "stair_ladder" };
            NoMaskType[] squareTypes =
            {
                NoMaskType.QuasiSymmetry,
                NoMaskType.OuterShell,
                NoMaskType.DenseWeave,
                NoMaskType.StairPush
            };

            NoMaskMotifKind[] dagMotifs =
            {
                NoMaskMotifKind.StairLadder,
                NoMaskMotifKind.ZigRiver,
                NoMaskMotifKind.FourPockets,
                NoMaskMotifKind.CenterCross
            };
            string[] dagIds = { "stair_ladder", "zig_river", "four_pockets", "center_cross" };
            NoMaskType[] dagTypes =
            {
                NoMaskType.StairPush,
                NoMaskType.Sweep,
                NoMaskType.SectionUnlock,
                NoMaskType.QuasiSymmetry
            };

            for (int variant = 0; variant < 10; variant++)
            {
                int longIndex = variant % longMotifs.Length;
                AddDirectDiffMotif(
                    list,
                    band,
                    "long_spine",
                    longIds[longIndex],
                    longMotifs[longIndex],
                    longTypes[longIndex],
                    baseWidth + ((variant + 1) % 3),
                    baseHeight + (variant % 4),
                    targetBase + (variant % 5) * targetStep,
                    minLength,
                    maxLength + 4,
                    targetCoverage,
                    outerBandTarget,
                    seedBase + variant * 1009 + 11,
                    variant,
                    blockWeight: 2.5f,
                    edgeOpeningBias: 0.12f,
                    turnBias: 0.72f,
                    initialRatio: 0.16f,
                    mirrorX: (variant & 1) == 1,
                    mirrorY: (variant % 5) == 0);

                int roomIndex = variant % roomMotifs.Length;
                AddDirectDiffMotif(
                    list,
                    band,
                    "room_gate",
                    roomIds[roomIndex],
                    roomMotifs[roomIndex],
                    roomTypes[roomIndex],
                    baseWidth + 2 + ((variant * 2) % 3),
                    baseHeight - 1 + ((variant + 1) % 4),
                    targetBase + 2 + (variant % 5) * targetStep,
                    minLength,
                    maxLength + 2,
                    targetCoverage,
                    outerBandTarget,
                    seedBase + variant * 1009 + 37,
                    variant,
                    blockWeight: 4.7f,
                    edgeOpeningBias: 0.18f,
                    turnBias: 0.60f,
                    initialRatio: 0.19f,
                    mirrorX: (variant & 1) == 0,
                    mirrorY: (variant % 4) == 0);

                int squareIndex = variant % squareMotifs.Length;
                int nearWidth = squareWidth + (variant % 4);
                int nearHeight = squareHeight + ((variant * 2) % 4);
                if (variant % 5 == 0)
                {
                    nearWidth += 2;
                    nearHeight = Mathf.Max(nearWidth, nearHeight - 1);
                }

                AddDirectDiffMotif(
                    list,
                    band,
                    "near_square",
                    squareIds[squareIndex],
                    squareMotifs[squareIndex],
                    squareTypes[squareIndex],
                    nearWidth,
                    nearHeight,
                    targetBase + 3 + (variant % 5) * targetStep,
                    minLength,
                    maxLength + 1,
                    targetCoverage,
                    outerBandTarget,
                    seedBase + variant * 1009 + 61,
                    variant,
                    blockWeight: 3.2f,
                    edgeOpeningBias: 0.22f,
                    turnBias: 0.58f,
                    initialRatio: 0.20f,
                    mirrorX: (variant & 1) == 1,
                    mirrorY: (variant % 3) == 0);

                int dagIndex = variant % dagMotifs.Length;
                AddDirectDiffMotif(
                    list,
                    band,
                    "dag_unlock",
                    dagIds[dagIndex],
                    dagMotifs[dagIndex],
                    dagTypes[dagIndex],
                    baseWidth + 1 + ((variant + 2) % 3),
                    baseHeight + ((variant * 3) % 5),
                    targetBase + 1 + (variant % 5) * targetStep,
                    minLength,
                    maxLength,
                    targetCoverage,
                    outerBandTarget,
                    seedBase + variant * 1009 + 89,
                    variant,
                    blockWeight: 2.1f,
                    edgeOpeningBias: 0.32f,
                    turnBias: 0.50f,
                    initialRatio: 0.26f,
                    mirrorX: (variant & 1) == 0,
                    mirrorY: (variant % 4) == 1);
            }
        }

        private static void AddDirectDiffMotif(
            List<MotifSpec> list,
            string band,
            string structure,
            string motifId,
            NoMaskMotifKind motif,
            NoMaskType type,
            int width,
            int height,
            int targetChains,
            int minLength,
            int maxLength,
            float targetCoverage,
            float outerBandTarget,
            int seed,
            int variant,
            float blockWeight,
            float edgeOpeningBias,
            float turnBias,
            float initialRatio,
            bool mirrorX,
            bool mirrorY)
        {
            string id = $"direct_diff_{band}_{structure}_{motifId}_g{variant % 5}_v{variant + 1:00}";
            MotifSpec spec = Motif(
                motif,
                id,
                $"Direct Diff {band} {structure} {motifId} {variant + 1:00}",
                type,
                width,
                height,
                targetChains,
                minLength,
                maxLength,
                targetCoverage,
                outerBandTarget,
                seed,
                variant,
                mirrorX,
                mirrorY);

            spec.Style.BlockWeight = blockWeight;
            spec.Style.EdgeOpeningBias = edgeOpeningBias;
            spec.Style.TurnBias = turnBias;
            spec.Style.MaxInitialMovableChains = Mathf.CeilToInt(targetChains * initialRatio);
            spec.Style.MaxShortEdgePatchChains = 4;
            spec.Style.MaxShortFillPatchChains = 10;
            list.Add(spec);
        }

        private static MotifSpec[] CreateDirectStructurePreviewSpecs()
        {
            var list = new List<MotifSpec>(32);

            AddDirectStructurePreview(list, "shell_peel", "a", NoMaskMotifKind.DoubleShell, NoMaskType.OuterShell, 18, 27, 82, 4, 10, 0.981f, 0.968f, 410101, 0, 3.6f, 0.16f, 0.62f, 0.18f, false, false);
            AddDirectStructurePreview(list, "shell_peel", "b", NoMaskMotifKind.DoubleShell, NoMaskType.OuterShell, 20, 29, 98, 4, 11, 0.980f, 0.968f, 410137, 1, 3.8f, 0.18f, 0.60f, 0.18f, true, false);
            AddDirectStructurePreview(list, "shell_peel", "c", NoMaskMotifKind.FourPockets, NoMaskType.OuterShell, 19, 28, 94, 4, 10, 0.981f, 0.968f, 410173, 2, 3.5f, 0.18f, 0.58f, 0.20f, false, true);
            AddDirectStructurePreview(list, "shell_peel", "d", NoMaskMotifKind.DoubleShell, NoMaskType.OuterShell, 21, 31, 112, 4, 12, 0.979f, 0.966f, 410209, 3, 3.9f, 0.16f, 0.61f, 0.17f, true, true);

            AddDirectStructurePreview(list, "core_lock", "a", NoMaskMotifKind.CenterCross, NoMaskType.LockBuckle, 18, 27, 86, 4, 10, 0.981f, 0.968f, 411101, 0, 4.8f, 0.16f, 0.54f, 0.15f, false, false);
            AddDirectStructurePreview(list, "core_lock", "b", NoMaskMotifKind.VerticalGate, NoMaskType.CoreBurst, 18, 29, 96, 4, 11, 0.980f, 0.968f, 411137, 1, 4.5f, 0.15f, 0.55f, 0.15f, true, false);
            AddDirectStructurePreview(list, "core_lock", "c", NoMaskMotifKind.KeyDoor, NoMaskType.LockBuckle, 19, 29, 104, 4, 11, 0.980f, 0.966f, 411173, 2, 5.0f, 0.14f, 0.52f, 0.14f, false, true);
            AddDirectStructurePreview(list, "core_lock", "d", NoMaskMotifKind.DenseKernel, NoMaskType.DenseWeave, 19, 30, 112, 4, 12, 0.979f, 0.966f, 411209, 3, 4.2f, 0.16f, 0.56f, 0.15f, true, true);

            AddDirectStructurePreview(list, "dual_interlock", "a", NoMaskMotifKind.DoubleRoomLock, NoMaskType.DualZone, 20, 28, 92, 4, 11, 0.981f, 0.968f, 412101, 0, 4.6f, 0.17f, 0.58f, 0.17f, false, false);
            AddDirectStructurePreview(list, "dual_interlock", "b", NoMaskMotifKind.DoubleRoomLock, NoMaskType.DualZone, 21, 30, 106, 4, 12, 0.980f, 0.966f, 412137, 1, 4.8f, 0.16f, 0.57f, 0.16f, true, false);
            AddDirectStructurePreview(list, "dual_interlock", "c", NoMaskMotifKind.KeyDoor, NoMaskType.DualZone, 20, 30, 102, 4, 11, 0.980f, 0.968f, 412173, 2, 4.4f, 0.18f, 0.58f, 0.17f, false, true);
            AddDirectStructurePreview(list, "dual_interlock", "d", NoMaskMotifKind.FourPockets, NoMaskType.DualZone, 21, 31, 116, 4, 12, 0.979f, 0.966f, 412209, 3, 4.5f, 0.18f, 0.59f, 0.18f, true, true);

            AddDirectStructurePreview(list, "spine_pockets", "a", NoMaskMotifKind.LongCorridor, NoMaskType.MazeLongChain, 19, 30, 96, 4, 13, 0.980f, 0.966f, 413101, 0, 2.6f, 0.15f, 0.74f, 0.16f, false, false);
            AddDirectStructurePreview(list, "spine_pockets", "b", NoMaskMotifKind.FourPockets, NoMaskType.SectionUnlock, 20, 31, 104, 4, 12, 0.980f, 0.966f, 413137, 1, 2.8f, 0.18f, 0.68f, 0.17f, true, false);
            AddDirectStructurePreview(list, "spine_pockets", "c", NoMaskMotifKind.SnakeSpine, NoMaskType.MazeLongChain, 20, 32, 108, 4, 13, 0.979f, 0.966f, 413173, 2, 2.7f, 0.16f, 0.75f, 0.16f, false, true);
            AddDirectStructurePreview(list, "spine_pockets", "d", NoMaskMotifKind.LongCorridor, NoMaskType.MazeLongChain, 21, 33, 116, 4, 14, 0.978f, 0.964f, 413209, 3, 2.6f, 0.15f, 0.76f, 0.16f, true, true);

            AddDirectStructurePreview(list, "entry_funnel", "a", NoMaskMotifKind.SnakeSpine, NoMaskType.Sweep, 18, 27, 78, 4, 10, 0.981f, 0.968f, 414101, 0, 2.4f, 0.34f, 0.58f, 0.22f, false, false);
            AddDirectStructurePreview(list, "entry_funnel", "b", NoMaskMotifKind.ZigRiver, NoMaskType.Sweep, 19, 28, 88, 4, 11, 0.981f, 0.968f, 414137, 1, 2.5f, 0.34f, 0.56f, 0.22f, true, false);
            AddDirectStructurePreview(list, "entry_funnel", "c", NoMaskMotifKind.StairLadder, NoMaskType.StairPush, 19, 29, 94, 4, 11, 0.980f, 0.968f, 414173, 2, 2.7f, 0.32f, 0.55f, 0.23f, false, true);
            AddDirectStructurePreview(list, "entry_funnel", "d", NoMaskMotifKind.CenterCross, NoMaskType.SectionUnlock, 20, 29, 100, 4, 11, 0.980f, 0.966f, 414209, 3, 2.8f, 0.30f, 0.55f, 0.22f, true, true);

            AddDirectStructurePreview(list, "layered_lock", "a", NoMaskMotifKind.DenseKernel, NoMaskType.DenseWeave, 21, 32, 116, 4, 12, 0.979f, 0.966f, 415101, 0, 4.8f, 0.14f, 0.56f, 0.14f, false, false);
            AddDirectStructurePreview(list, "layered_lock", "b", NoMaskMotifKind.KeyDoor, NoMaskType.LockBuckle, 21, 33, 124, 4, 12, 0.978f, 0.964f, 415137, 1, 5.1f, 0.13f, 0.54f, 0.13f, true, false);
            AddDirectStructurePreview(list, "layered_lock", "c", NoMaskMotifKind.CenterCross, NoMaskType.LockBuckle, 22, 33, 128, 4, 13, 0.978f, 0.964f, 415173, 2, 5.0f, 0.13f, 0.55f, 0.13f, false, true);
            AddDirectStructurePreview(list, "layered_lock", "d", NoMaskMotifKind.DoubleShell, NoMaskType.OuterShell, 22, 34, 132, 4, 13, 0.978f, 0.964f, 415209, 3, 4.6f, 0.14f, 0.57f, 0.14f, true, true);

            AddDirectStructurePreview(list, "rhythm_mix", "a", NoMaskMotifKind.StairLadder, NoMaskType.StairPush, 18, 28, 82, 3, 12, 0.981f, 0.968f, 416101, 0, 2.8f, 0.24f, 0.64f, 0.20f, false, false);
            AddDirectStructurePreview(list, "rhythm_mix", "b", NoMaskMotifKind.ZigRiver, NoMaskType.Sweep, 19, 29, 92, 3, 13, 0.981f, 0.968f, 416137, 1, 2.6f, 0.25f, 0.66f, 0.20f, true, false);
            AddDirectStructurePreview(list, "rhythm_mix", "c", NoMaskMotifKind.SnakeSpine, NoMaskType.Sweep, 20, 30, 102, 3, 13, 0.980f, 0.966f, 416173, 2, 2.7f, 0.24f, 0.67f, 0.20f, false, true);
            AddDirectStructurePreview(list, "rhythm_mix", "d", NoMaskMotifKind.FourPockets, NoMaskType.SectionUnlock, 20, 31, 108, 3, 12, 0.980f, 0.966f, 416209, 3, 2.9f, 0.23f, 0.63f, 0.20f, true, true);

            AddDirectStructurePreview(list, "wide_square", "a", NoMaskMotifKind.CenterCross, NoMaskType.QuasiSymmetry, 24, 22, 86, 4, 10, 0.981f, 0.968f, 417101, 0, 3.3f, 0.20f, 0.56f, 0.19f, false, false);
            AddDirectStructurePreview(list, "wide_square", "b", NoMaskMotifKind.DoubleShell, NoMaskType.OuterShell, 25, 23, 98, 4, 11, 0.980f, 0.968f, 417137, 1, 3.4f, 0.20f, 0.57f, 0.18f, true, false);
            AddDirectStructurePreview(list, "wide_square", "c", NoMaskMotifKind.DenseKernel, NoMaskType.DenseWeave, 26, 24, 108, 4, 11, 0.980f, 0.966f, 417173, 2, 3.7f, 0.19f, 0.56f, 0.18f, false, true);
            AddDirectStructurePreview(list, "wide_square", "d", NoMaskMotifKind.DoubleRoomLock, NoMaskType.DualZone, 26, 23, 104, 4, 11, 0.980f, 0.966f, 417209, 3, 3.8f, 0.18f, 0.57f, 0.17f, true, true);

            return list.ToArray();
        }

        private static void AddDirectStructurePreview(
            List<MotifSpec> list,
            string structure,
            string suffix,
            NoMaskMotifKind motif,
            NoMaskType type,
            int width,
            int height,
            int targetChains,
            int minLength,
            int maxLength,
            float targetCoverage,
            float outerBandTarget,
            int seed,
            int variant,
            float blockWeight,
            float edgeOpeningBias,
            float turnBias,
            float initialRatio,
            bool mirrorX,
            bool mirrorY)
        {
            string band = targetChains >= 115 ? "hard" : "normal";
            string id = $"direct_diff_{band}_preview_{structure}_{suffix}";
            MotifSpec spec = Motif(
                motif,
                id,
                $"Direct Structure Preview {structure} {suffix}",
                type,
                width,
                height,
                targetChains,
                minLength,
                maxLength,
                targetCoverage,
                outerBandTarget,
                seed,
                variant,
                mirrorX,
                mirrorY);

            spec.Style.BlockWeight = blockWeight;
            spec.Style.EdgeOpeningBias = edgeOpeningBias;
            spec.Style.TurnBias = turnBias;
            spec.Style.MaxInitialMovableChains = Mathf.CeilToInt(targetChains * initialRatio);
            spec.Style.MaxShortEdgePatchChains = 4;
            spec.Style.MaxShortFillPatchChains = 10;
            list.Add(spec);
        }

        private static string DirectStructurePreviewKind(string id)
        {
            string text = id ?? string.Empty;
            if (text.Contains("_preview_shell_peel_"))
                return "shell_peel";
            if (text.Contains("_preview_core_lock_"))
                return "core_lock";
            if (text.Contains("_preview_dual_interlock_"))
                return "dual_interlock";
            if (text.Contains("_preview_spine_pockets_"))
                return "spine_pockets";
            if (text.Contains("_preview_entry_funnel_"))
                return "entry_funnel";
            if (text.Contains("_preview_layered_lock_"))
                return "layered_lock";
            if (text.Contains("_preview_rhythm_mix_"))
                return "rhythm_mix";
            if (text.Contains("_preview_wide_square_"))
                return "wide_square";
            return "unknown";
        }

        private static MotifSpec[] CreateDirectArchitecturePreviewSpecs()
        {
            var list = new List<MotifSpec>(16);

            AddDirectArchitecturePreview(list, "circuit_board", "a", NoMaskMotifKind.CircuitBoard, NoMaskType.SectionUnlock, 23, 31, 68, 4, 16, 0.66f, 0.42f, 510101, 0, 0.62f, false, false);
            AddDirectArchitecturePreview(list, "circuit_board", "b", NoMaskMotifKind.CircuitBoard, NoMaskType.SectionUnlock, 25, 34, 82, 4, 17, 0.68f, 0.42f, 510137, 1, 0.60f, true, false);
            AddDirectArchitecturePreview(list, "circuit_board", "c", NoMaskMotifKind.CircuitBoard, NoMaskType.SectionUnlock, 24, 33, 76, 4, 16, 0.64f, 0.40f, 510173, 2, 0.64f, false, true);
            AddDirectArchitecturePreview(list, "circuit_board", "d", NoMaskMotifKind.CircuitBoard, NoMaskType.SectionUnlock, 26, 35, 88, 4, 18, 0.69f, 0.42f, 510209, 3, 0.61f, true, true);

            AddDirectArchitecturePreview(list, "parallel_highway", "a", NoMaskMotifKind.ParallelHighway, NoMaskType.MazeLongChain, 24, 32, 62, 5, 20, 0.58f, 0.34f, 511101, 0, 0.78f, false, false);
            AddDirectArchitecturePreview(list, "parallel_highway", "b", NoMaskMotifKind.ParallelHighway, NoMaskType.MazeLongChain, 26, 34, 74, 5, 22, 0.60f, 0.34f, 511137, 1, 0.80f, true, false);
            AddDirectArchitecturePreview(list, "parallel_highway", "c", NoMaskMotifKind.ParallelHighway, NoMaskType.MazeLongChain, 25, 35, 78, 5, 22, 0.61f, 0.34f, 511173, 2, 0.79f, false, true);
            AddDirectArchitecturePreview(list, "parallel_highway", "d", NoMaskMotifKind.ParallelHighway, NoMaskType.MazeLongChain, 27, 36, 86, 5, 24, 0.62f, 0.34f, 511209, 3, 0.80f, true, true);

            AddDirectArchitecturePreview(list, "room_corridor", "a", NoMaskMotifKind.RoomCorridor, NoMaskType.DualZone, 23, 32, 70, 4, 16, 0.65f, 0.40f, 512101, 0, 0.60f, false, false);
            AddDirectArchitecturePreview(list, "room_corridor", "b", NoMaskMotifKind.RoomCorridor, NoMaskType.DualZone, 25, 34, 82, 4, 17, 0.67f, 0.40f, 512137, 1, 0.59f, true, false);
            AddDirectArchitecturePreview(list, "room_corridor", "c", NoMaskMotifKind.RoomCorridor, NoMaskType.DualZone, 24, 35, 86, 4, 18, 0.68f, 0.40f, 512173, 2, 0.58f, false, true);
            AddDirectArchitecturePreview(list, "room_corridor", "d", NoMaskMotifKind.RoomCorridor, NoMaskType.DualZone, 26, 36, 94, 4, 18, 0.69f, 0.42f, 512209, 3, 0.58f, true, true);

            AddDirectArchitecturePreview(list, "nested_rooms", "a", NoMaskMotifKind.NestedRooms, NoMaskType.OuterShell, 22, 31, 72, 4, 15, 0.66f, 0.44f, 513101, 0, 0.58f, false, false);
            AddDirectArchitecturePreview(list, "nested_rooms", "b", NoMaskMotifKind.NestedRooms, NoMaskType.OuterShell, 24, 33, 84, 4, 16, 0.68f, 0.44f, 513137, 1, 0.57f, true, false);
            AddDirectArchitecturePreview(list, "nested_rooms", "c", NoMaskMotifKind.NestedRooms, NoMaskType.OuterShell, 25, 34, 90, 4, 17, 0.69f, 0.44f, 513173, 2, 0.56f, false, true);
            AddDirectArchitecturePreview(list, "nested_rooms", "d", NoMaskMotifKind.NestedRooms, NoMaskType.OuterShell, 26, 36, 98, 4, 18, 0.70f, 0.44f, 513209, 3, 0.56f, true, true);

            return list.ToArray();
        }

        private static void AddDirectArchitecturePreview(
            List<MotifSpec> list,
            string architecture,
            string suffix,
            NoMaskMotifKind motif,
            NoMaskType type,
            int width,
            int height,
            int targetChains,
            int minLength,
            int maxLength,
            float targetCoverage,
            float outerBandTarget,
            int seed,
            int variant,
            float turnBias,
            bool mirrorX,
            bool mirrorY)
        {
            string id = $"direct_arch_{architecture}_{suffix}";
            MotifSpec spec = Motif(
                motif,
                id,
                $"Direct Architecture {architecture} {suffix}",
                type,
                width,
                height,
                targetChains,
                minLength,
                maxLength,
                targetCoverage,
                outerBandTarget,
                seed,
                variant,
                mirrorX,
                mirrorY);

            spec.Style.BlockWeight = 2.2f;
            spec.Style.EdgeOpeningBias = 0.16f;
            spec.Style.TurnBias = turnBias;
            spec.Style.MaxInitialMovableChains = Mathf.CeilToInt(targetChains * 0.18f);
            spec.Style.MaxShortEdgePatchChains = 3;
            spec.Style.MaxShortFillPatchChains = 4;
            list.Add(spec);
        }

        private static string DirectArchitectureKind(string id)
        {
            string text = id ?? string.Empty;
            if (text.Contains("direct_arch_circuit_board_"))
                return "circuit_board";
            if (text.Contains("direct_arch_parallel_highway_"))
                return "parallel_highway";
            if (text.Contains("direct_arch_room_corridor_"))
                return "room_corridor";
            if (text.Contains("direct_arch_nested_rooms_"))
                return "nested_rooms";
            return "unknown";
        }

        private static string DirectDiffBand(string id)
        {
            string text = id ?? string.Empty;
            if (text.Contains("_refresh_"))
                return "refresh_30_60";
            if (text.Contains("_normal_"))
                return "normal_60_100";
            if (text.Contains("_hard_"))
                return "hard_100_140";
            if (text.Contains("_veryhard_"))
                return "veryhard_140_200";
            return "unknown";
        }

        private static string DirectDiffStructure(string id)
        {
            string text = id ?? string.Empty;
            if (text.Contains("_long_spine_"))
                return "long_spine";
            if (text.Contains("_room_gate_"))
                return "room_gate";
            if (text.Contains("_near_square_"))
                return "near_square";
            if (text.Contains("_dag_unlock_"))
                return "dag_unlock";
            return "unknown";
        }

        private static bool PreferDirectDiffFallbackFirst(string band)
        {
            return string.Equals(band, "hard_100_140", StringComparison.OrdinalIgnoreCase)
                || string.Equals(band, "veryhard_140_200", StringComparison.OrdinalIgnoreCase);
        }

        private static MotifSpec[] CreateDirectNormalCandidateSpecs()
        {
            var list = new List<MotifSpec>(140);
            NoMaskMotifKind[] motifs =
            {
                NoMaskMotifKind.CenterCross,
                NoMaskMotifKind.StairLadder,
                NoMaskMotifKind.KeyDoor,
                NoMaskMotifKind.FourPockets,
                NoMaskMotifKind.VerticalGate,
                NoMaskMotifKind.ZigRiver,
                NoMaskMotifKind.DenseKernel,
                NoMaskMotifKind.DoubleShell,
                NoMaskMotifKind.SnakeSpine,
                NoMaskMotifKind.LongCorridor
            };
            NoMaskType[] types =
            {
                NoMaskType.LockBuckle,
                NoMaskType.StairPush,
                NoMaskType.LockBuckle,
                NoMaskType.SectionUnlock,
                NoMaskType.CoreBurst,
                NoMaskType.Sweep,
                NoMaskType.DenseWeave,
                NoMaskType.OuterShell,
                NoMaskType.Sweep,
                NoMaskType.MazeLongChain,
            };
            string[] ids =
            {
                "center_cross",
                "stair_ladder",
                "key_door",
                "four_pockets",
                "vertical_gate",
                "zig_river",
                "dense_kernel",
                "double_shell",
                "snake_spine",
                "long_corridor"
            };
            string[] display =
            {
                "Center Cross",
                "Stair Ladder",
                "Key Door",
                "Four Pockets",
                "Vertical Gate",
                "Z River",
                "Dense Kernel",
                "Double Shell",
                "Snake Spine",
                "Long Corridor"
            };

            int seed = 91001;
            for (int variant = 0; variant < 14; variant++)
            {
                for (int i = 0; i < motifs.Length; i++)
                {
                    int width = 17 + ((variant * 2 + i) % 4);
                    int height = 29 + ((variant * 3 + i * 2) % 6);
                    if (variant >= 8)
                    {
                        width += (i % 5 == 0) ? 1 : 0;
                        height += 1;
                    }

                    if (types[i] == NoMaskType.MazeLongChain)
                        width = Mathf.Min(width, 20);

                    int targetChains = Mathf.Clamp(104 + (variant % 6) * 3 + (i % 4) * 2, 104, 118);
                    int maxLength = Mathf.Clamp(8 + (variant % 2) + (i % 3 == 0 ? 1 : 0), 8, 10);
                    int familyGroup = variant % 5;
                    string id = $"direct_normal_{ids[i]}_g{familyGroup}_v{variant + 1:00}";
                    list.Add(Motif(
                        motifs[i],
                        id,
                        $"Direct Normal {display[i]} {variant + 1:00}",
                        types[i],
                        width,
                        height,
                        targetChains,
                        3,
                        maxLength,
                        0.975f,
                        0.95f,
                        seed + variant * 997 + i * 37,
                        variant,
                        ((variant + i) & 1) == 1,
                        ((variant * 2 + i) % 5) == 0));
                }
            }

            return list.ToArray();
        }

        private static MotifSpec Motif(
            NoMaskMotifKind motif,
            string id,
            string displayName,
            NoMaskType type,
            int width,
            int height,
            int targetChains,
            int minLength,
            int maxLength,
            float targetCoverage,
            float outerBandTarget,
            int seed,
            int motifVariant = 0,
            bool motifMirrorX = false,
            bool motifMirrorY = false)
        {
            return new MotifSpec
            {
                Motif = motif,
                Id = id,
                DisplayName = displayName,
                Seed = seed,
                Style = new StyleSpec
                {
                    Type = type,
                    Id = id,
                    DisplayName = displayName,
                    Width = width,
                    Height = height,
                    TargetChains = targetChains,
                    MinLength = minLength,
                    MaxLength = maxLength,
                    TargetCoverage = targetCoverage,
                    TurnBias = type == NoMaskType.MazeLongChain ? 0.68f : 0.55f,
                    BlockWeight = type == NoMaskType.LockBuckle ? 4.6f : 3.0f,
                    EdgeOpeningBias = 0.30f,
                    OuterBandTarget = outerBandTarget,
                    MaxInitialMovableChains = Mathf.CeilToInt(targetChains * 0.34f),
                    MaxShortEdgePatchChains = 5,
                    MaxShortFillPatchChains = 8,
                    Seed = seed,
                    MotifVariant = motifVariant,
                    MotifMirrorX = motifMirrorX,
                    MotifMirrorY = motifMirrorY
                }
            };
        }

        private static bool TryBuildLevel(
            StyleSpec spec,
            int index,
            out LevelDefinition level,
            out string assetPath,
            out BuildMetrics metrics,
            out string status)
        {
            return TryBuildLevelAt(spec, index, OutputFolder, "nomask", out level, out assetPath, out metrics, out status);
        }

        private static bool TryBuildDirectHoleLevelAt(
            StyleSpec spec,
            int index,
            string assetPath,
            string levelId,
            out LevelDefinition level,
            out BuildMetrics metrics,
            out int blockCount,
            out int allowedCount,
            out string status)
        {
            level = null;
            metrics = new BuildMetrics();
            status = string.Empty;

            List<int> blockIndices = BuildCenteredHoleBlockIndices(spec.Width, spec.Height, 8, 9);
            blockCount = blockIndices.Count;
            bool[] canSpawn = BuildCanSpawnForDirectHole(spec.Width, spec.Height, blockIndices, spec.OuterCutKind);
            allowedCount = Mathf.Max(1, CountTrue(canSpawn));

            var ruleset = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            AuthoredLevelData bestAuthored = null;
            BuildMetrics bestMetrics = null;
            int bestSeed = spec.Seed;
            int bestScore = int.MinValue;
            int attemptsTried = 0;
            string bestDetails = string.Empty;
            string lastFailure = string.Empty;
            PeelStyleProfile[] profiles = GetDirectHolePeelProfiles();

            for (int profileIndex = 0; profileIndex < profiles.Length; profileIndex++)
            {
                PeelStyleProfile profile = profiles[profileIndex];
                for (int attempt = 0; attempt < profile.Attempts; attempt++)
                {
                    attemptsTried++;
                    int candidateSeed = spec.Seed + index * 11003 + profile.SeedSalt + profileIndex * 1000003 + attempt * 104729;
                    if (!TryBuildConstrainedRectanglePeelAuthored(spec, canSpawn, blockIndices, candidateSeed, profile, out AuthoredLevelData authored, out string buildDetails))
                    {
                        lastFailure = buildDetails;
                        continue;
                    }

                    if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out string buildError))
                    {
                        lastFailure = $"BoardBuildFailed={buildError} | {buildDetails}";
                        continue;
                    }

                    if (!GreedyValidator.TryClearAllByGreedy(CloneBoard(board), ruleset, maxMoves: 700, out List<Move> moves))
                    {
                        lastFailure = $"GreedyFailed | {buildDetails}";
                        continue;
                    }

                    BuildMetrics current = BuildMetricsFromAuthored(spec, authored);
                    current.Coverage = current.ArrowCount / (float)Mathf.Max(1, allowedCount);
                    current.InitialMovableChains = CountInitialMovableChains(board, authored);
                    current.GreedyMoves = moves != null ? moves.Count : 0;
                    current.Attempts = attemptsTried;

                    int directBlockHeads = CountHeadsAimingAtBlocks(authored);
                    if (directBlockHeads > 0)
                    {
                        lastFailure = $"DirectBlockHeads={directBlockHeads} | {buildDetails}";
                        continue;
                    }

                    float openingRatio = current.Chains > 0 ? current.InitialMovableChains / (float)current.Chains : 1f;
                    int chainPenalty = Mathf.Abs(current.Chains - spec.TargetChains) * 260;
                    int targetInitial = Mathf.Clamp(Mathf.CeilToInt(current.Chains * 0.18f), 5, Mathf.Max(5, spec.MaxInitialMovableChains));
                    int openingPenalty = Mathf.Abs(current.InitialMovableChains - targetInitial) * 160
                        + Mathf.Max(0, current.InitialMovableChains - spec.MaxInitialMovableChains) * 700
                        + Mathf.RoundToInt(Mathf.Max(0f, openingRatio - 0.34f) * 4200f);
                    int shortPenalty = Mathf.Max(0, current.ShortEdgeChains - 6) * 460;
                    int edgeHeadPenalty = Mathf.Max(0, current.EdgeHeadChains - Mathf.CeilToInt(current.Chains * 0.38f)) * 520;
                    int straightPenalty = current.Straightness > 0.74f ? 1200 : 0;
                    int fillPenalty = Mathf.RoundToInt(Mathf.Max(0f, spec.TargetCoverage - current.Coverage) * 6000f);
                    int score = current.ArrowCount * 100
                        - chainPenalty
                        - openingPenalty
                        - shortPenalty
                        - edgeHeadPenalty
                        - straightPenalty
                        - fillPenalty
                        + Mathf.RoundToInt(current.OuterBandCoverage * 1000f)
                        + current.BlockLinks * 5;

                    if (score > bestScore)
                    {
                        bestAuthored = authored;
                        bestMetrics = current;
                        bestSeed = candidateSeed;
                        bestScore = score;
                        bestDetails = $"DirectHole style={profile.DisplayName} score={score} openingRatio={openingRatio:0.000} | {buildDetails}";
                    }

                    if (current.Coverage >= spec.TargetCoverage
                        && current.OuterBandCoverage >= spec.OuterBandTarget
                        && Mathf.Abs(current.Chains - spec.TargetChains) <= 5
                        && current.InitialMovableChains <= spec.MaxInitialMovableChains
                        && current.ShortEdgeChains <= 7
                        && current.Straightness <= 0.74f)
                    {
                        profileIndex = profiles.Length;
                        break;
                    }
                }
            }

            if (bestAuthored == null || bestMetrics == null)
            {
                status = string.IsNullOrWhiteSpace(lastFailure) ? "No direct hole candidate" : lastFailure;
                return false;
            }

            int openingHardLimit = Mathf.Max(spec.MaxInitialMovableChains, Mathf.CeilToInt(bestMetrics.Chains * 0.34f));
            if (bestMetrics.InitialMovableChains > openingHardLimit)
            {
                status = $"InitialTooHigh best={bestMetrics.InitialMovableChains}/{openingHardLimit} soft={spec.MaxInitialMovableChains} | {bestDetails}";
                return false;
            }

            metrics = bestMetrics;
            if (!TryPreparePortableLevel(spec, null, bestAuthored, levelId, bestSeed, metrics, out ArrowLevelData portable, out string portableStatus))
            {
                status = $"{portableStatus} | {bestDetails}";
                return false;
            }

            portable.metadata["generator"] = "NoMaskProceduralGenerator.DirectHole";
            portable.metadata["hole"] = "center-8x9-blocker";
            portable.metadata["outerCutKind"] = spec.OuterCutKind.ToString();
            level = SavePortableGeneratedLevel(assetPath, portable, spec, bestSeed, metrics, levelId);
            status = bestDetails;
            return true;
        }

        private static bool TryBuildLevelAt(
            StyleSpec spec,
            int index,
            string outputFolder,
            string levelIdPrefix,
            out LevelDefinition level,
            out string assetPath,
            out BuildMetrics metrics,
            out string status)
        {
            level = null;
            assetPath = $"{outputFolder}/{levelIdPrefix}_{index:00}_{spec.Id}.asset";
            metrics = new BuildMetrics();
            status = string.Empty;

            var ruleset = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            AuthoredLevelData bestAuthored = null;
            BuildMetrics bestMetrics = null;
            int bestSeed = spec.Seed;
            int bestScore = int.MinValue;
            int attemptsTried = 0;
            string bestDetails = string.Empty;
            PeelStyleProfile[] profiles = GetPeelStyleProfilesForSpec(spec);

            for (int profileIndex = 0; profileIndex < profiles.Length; profileIndex++)
            {
                PeelStyleProfile profile = profiles[profileIndex];
                for (int attempt = 0; attempt < profile.Attempts; attempt++)
                {
                    attemptsTried++;
                    int candidateSeed = spec.Seed + index * 9176 + profile.SeedSalt + profileIndex * 1000003 + attempt * 104729;
                    if (!TryBuildRectanglePeelAuthored(spec, candidateSeed, profile, out AuthoredLevelData authored, out string buildDetails))
                    {
                        if (bestAuthored == null)
                            bestDetails = buildDetails;
                        continue;
                    }

                    if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out string buildError))
                    {
                        if (bestAuthored == null)
                            bestDetails = $"BoardBuildFailed={buildError} | {buildDetails}";
                        continue;
                    }

                    if (!GreedyValidator.TryClearAllByGreedy(CloneBoard(board), ruleset, maxMoves: 700, out List<Move> moves))
                    {
                        if (bestAuthored == null)
                            bestDetails = $"GreedyFailed | {buildDetails}";
                        continue;
                    }

                    BuildMetrics current = BuildMetricsFromAuthored(spec, authored);
                    current.InitialMovableChains = CountInitialMovableChains(board, authored);
                    current.GreedyMoves = moves != null ? moves.Count : 0;
                    current.Attempts = attemptsTried;

                    int targetInitial = Mathf.Clamp(
                        Mathf.RoundToInt(current.Chains * profile.TargetInitialRatio),
                        profile.TargetInitialMin,
                        profile.TargetInitialMax);
                    float openingRatio = current.Chains > 0 ? current.InitialMovableChains / (float)current.Chains : 0f;
                    float openingCeiling = current.Chains <= 50 ? Mathf.Max(profile.OpeningCeiling, 0.32f) : profile.OpeningCeiling;
                    int openingPenalty = Mathf.RoundToInt(
                        Mathf.Abs(current.InitialMovableChains - targetInitial) * 125f
                        + Mathf.Max(0f, openingRatio - openingCeiling) * 4200f);
                    int chainPenalty = Mathf.Abs(current.Chains - spec.TargetChains) * 220;
                    int shortPenalty = Mathf.Max(0, current.ShortEdgeChains - 5) * 360;
                    int edgeHeadCeiling = Mathf.Max(10, Mathf.CeilToInt(current.Chains * 0.32f));
                    int edgeHeadPenalty = Mathf.Max(0, current.EdgeHeadChains - edgeHeadCeiling) * 520;
                    int straightPenalty = current.Straightness > 0.72f ? profile.StraightPenalty : 0;
                    int score = current.ArrowCount * 100
                        - chainPenalty
                        - openingPenalty
                        - shortPenalty
                        - edgeHeadPenalty
                        - straightPenalty
                        + Mathf.RoundToInt(current.OuterBandCoverage * 1200f);

                    if (score > bestScore)
                    {
                        bestAuthored = authored;
                        bestMetrics = current;
                        bestSeed = candidateSeed;
                        bestScore = score;
                        bestDetails = $"Style={profile.DisplayName} | Score={score} | TargetInitial={targetInitial} | OpeningRatio={openingRatio:0.000} | {buildDetails}";
                    }

                    if (current.Coverage >= spec.TargetCoverage
                        && current.OuterBandCoverage >= spec.OuterBandTarget
                        && DirectChainTargetSatisfied(spec, current.Chains)
                        && openingRatio <= openingCeiling
                        && current.Straightness <= 0.70f
                        && current.ShortEdgeChains <= 7)
                    {
                        profileIndex = profiles.Length;
                        break;
                    }
                }
            }

            if (bestAuthored == null || bestMetrics == null)
            {
                status = string.IsNullOrWhiteSpace(bestDetails) ? "No peel candidate" : bestDetails;
                return false;
            }

            if (!DirectChainTargetSatisfied(spec, bestMetrics.Chains))
            {
                status = $"ChainTargetMiss chains={bestMetrics.Chains} target={spec.TargetChains} | {bestDetails}";
                return false;
            }

            metrics = bestMetrics;
            string levelId = $"{levelIdPrefix}_{index:00}_{spec.Id}";
            if (!TryPreparePortableLevel(spec, null, bestAuthored, levelId, bestSeed, metrics, out ArrowLevelData portable, out string portableStatus))
            {
                status = $"{portableStatus} | {bestDetails}";
                return false;
            }

            level = SavePortableGeneratedLevel(assetPath, portable, spec, bestSeed, metrics, levelId);
            status = bestDetails;
            return true;
        }

        private static bool TryBuildMotifLevel(
            MotifSpec motifSpec,
            int index,
            string outputFolder,
            string levelIdPrefix,
            out LevelDefinition level,
            out string assetPath,
            out BuildMetrics metrics,
            out int motifCells,
            out string status)
        {
            level = null;
            StyleSpec spec = motifSpec.Style;
            assetPath = $"{outputFolder}/{levelIdPrefix}_{index:00}_{motifSpec.Id}.asset";
            metrics = new BuildMetrics();
            motifCells = 0;
            status = string.Empty;

            var ruleset = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            AuthoredLevelData bestAuthored = null;
            BuildMetrics bestMetrics = null;
            int bestSeed = motifSpec.Seed;
            int bestMotifCells = 0;
            int bestScore = int.MinValue;
            int attemptsTried = 0;
            string bestDetails = string.Empty;
            PeelStyleProfile[] profiles = GetPeelStyleProfilesForSpec(spec);

            for (int profileIndex = 0; profileIndex < profiles.Length; profileIndex++)
            {
                PeelStyleProfile profile = profiles[profileIndex];
                for (int attempt = 0; attempt < profile.Attempts + 8; attempt++)
                {
                    attemptsTried++;
                    int candidateSeed = motifSpec.Seed + index * 13177 + profile.SeedSalt + profileIndex * 1000003 + attempt * 104729;
                    if (!TryBuildMotifPeelAuthored(motifSpec, candidateSeed, profile, out AuthoredLevelData authored, out int currentMotifCells, out string buildDetails))
                    {
                        if (bestAuthored == null)
                            bestDetails = buildDetails;
                        continue;
                    }

                    if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out string buildError))
                    {
                        if (bestAuthored == null)
                            bestDetails = $"BoardBuildFailed={buildError} | {buildDetails}";
                        continue;
                    }

                    if (!GreedyValidator.TryClearAllByGreedy(CloneBoard(board), ruleset, maxMoves: 850, out List<Move> moves))
                    {
                        if (bestAuthored == null)
                            bestDetails = $"GreedyFailed | {buildDetails}";
                        continue;
                    }

                    BuildMetrics current = BuildMetricsFromAuthored(spec, authored);
                    current.InitialMovableChains = CountInitialMovableChains(board, authored);
                    current.GreedyMoves = moves != null ? moves.Count : 0;
                    current.Attempts = attemptsTried;

                    int targetInitial = Mathf.Clamp(
                        Mathf.RoundToInt(current.Chains * profile.TargetInitialRatio),
                        profile.TargetInitialMin,
                        profile.TargetInitialMax);
                    float openingRatio = current.Chains > 0 ? current.InitialMovableChains / (float)current.Chains : 0f;
                    float openingCeiling = current.Chains <= 60 ? Mathf.Max(profile.OpeningCeiling, 0.32f) : profile.OpeningCeiling;
                    int openingPenalty = Mathf.RoundToInt(
                        Mathf.Abs(current.InitialMovableChains - targetInitial) * 120f
                        + Mathf.Max(0f, openingRatio - openingCeiling) * 4200f);
                    int chainPenalty = Mathf.Abs(current.Chains - spec.TargetChains) * 180;
                    int shortPenalty = Mathf.Max(0, current.ShortEdgeChains - 8) * 300;
                    int edgeHeadCeiling = Mathf.Max(9, Mathf.CeilToInt(current.Chains * 0.32f));
                    int edgeHeadPenalty = Mathf.Max(0, current.EdgeHeadChains - edgeHeadCeiling) * 420;
                    int motifBonus = Mathf.Clamp(currentMotifCells, 0, spec.Width * spec.Height / 3) * 70;
                    bool architectureSpec = IsDirectArchitectureSpec(spec);
                    int straightPenalty = !architectureSpec && current.Straightness > 0.72f ? profile.StraightPenalty : 0;
                    int score = current.ArrowCount * 100
                        + motifBonus
                        - chainPenalty
                        - openingPenalty
                        - shortPenalty
                        - edgeHeadPenalty
                        - straightPenalty
                        + Mathf.RoundToInt(current.OuterBandCoverage * 1200f);

                    if (score > bestScore)
                    {
                        bestAuthored = authored;
                        bestMetrics = current;
                        bestSeed = candidateSeed;
                        bestMotifCells = currentMotifCells;
                        bestScore = score;
                        bestDetails = $"Motif={motifSpec.DisplayName} | Style={profile.DisplayName} | Score={score} | MotifCells={currentMotifCells} | TargetInitial={targetInitial} | OpeningRatio={openingRatio:0.000} | {buildDetails}";
                    }

                    if (current.Coverage >= spec.TargetCoverage
                        && current.OuterBandCoverage >= spec.OuterBandTarget
                        && DirectChainTargetSatisfied(spec, current.Chains)
                        && openingRatio <= openingCeiling
                        && current.Straightness <= (architectureSpec ? 0.92f : 0.70f)
                        && current.ShortEdgeChains <= (architectureSpec ? 14 : 9))
                    {
                        profileIndex = profiles.Length;
                        break;
                    }
                }
            }

            if (bestAuthored == null || bestMetrics == null)
            {
                status = string.IsNullOrWhiteSpace(bestDetails) ? "No motif candidate" : bestDetails;
                return false;
            }

            if (!DirectChainTargetSatisfied(spec, bestMetrics.Chains))
            {
                status = $"ChainTargetMiss chains={bestMetrics.Chains} target={spec.TargetChains} | {bestDetails}";
                return false;
            }

            metrics = bestMetrics;
            motifCells = bestMotifCells;
            string levelId = $"{levelIdPrefix}_{index:00}_{motifSpec.Id}";
            if (!TryPreparePortableLevel(spec, motifSpec, bestAuthored, levelId, bestSeed, metrics, out ArrowLevelData portable, out string portableStatus))
            {
                status = $"{portableStatus} | {bestDetails}";
                return false;
            }

            level = SavePortableGeneratedLevel(assetPath, portable, spec, bestSeed, metrics, levelId);
            status = bestDetails;
            return true;
        }

        private static bool TryPreparePortableLevel(
            StyleSpec spec,
            MotifSpec motifSpec,
            AuthoredLevelData authored,
            string levelId,
            int seed,
            BuildMetrics metrics,
            out ArrowLevelData portable,
            out string status)
        {
            portable = ArrowLevelGeneratorAdapter.ToPortable(
                levelId,
                authored,
                PortableDifficultyFromSpec(spec),
                motifSpec != null ? PortableFamilyFromMotif(motifSpec.Motif, spec) : PortableFamilyFromSpec(spec));

            status = string.Empty;
            if (portable == null)
            {
                status = "PortableConvertFailed";
                return false;
            }

            portable.metadata["generator"] = "NoMaskProceduralGenerator";
            portable.metadata["specId"] = spec.Id;
            portable.metadata["seed"] = seed.ToString();
            portable.metadata["targetChains"] = spec.TargetChains.ToString();
            portable.metadata["targetCoverage"] = spec.TargetCoverage.ToString("0.000");
            if (motifSpec != null)
                portable.metadata["motif"] = motifSpec.Motif.ToString();

            ArrowLevelValidationReport validation = ArrowLevelValidator.Validate(portable);
            if (!validation.isValid)
            {
                status = $"PortableInvalid={JoinMessages(validation.errors, 4)}";
                return false;
            }

            var solver = new GreedyEscapeSolver();
            ArrowSolveReport solve = solver.Solve(portable);
            if (!solve.solved)
            {
                status = $"PortableGreedyFail removed={solve.removedChains}/{solve.totalChains} stuck={JoinInts(solve.stuckChains, 8)}";
                return false;
            }

            var evaluator = new ArrowLevelQualityEvaluator(solver);
            ArrowLevelMetrics portableMetrics = evaluator.Evaluate(portable, ArrowQualityPolicy.ForDifficulty(portable.difficulty));
            metrics.PortableGreedySolved = solve.solved;
            metrics.PortableInitialOpeners = solve.initialOpeners;
            metrics.PortableScore = portableMetrics.playabilityScore;
            metrics.PortableQualityFlags = portableMetrics.qualityFlags;
            return true;
        }

        private static LevelDefinition SavePortableGeneratedLevel(
            string assetPath,
            ArrowLevelData portable,
            StyleSpec spec,
            int seed,
            BuildMetrics metrics,
            string levelId)
        {
            LevelDefinition level = AssetDatabase.LoadAssetAtPath<LevelDefinition>(assetPath);
            bool isNew = level == null;
            if (level == null)
                level = ScriptableObject.CreateInstance<LevelDefinition>();

            ArrowLevelGeneratorAdapter.FillLevelDefinition(level, portable, levelId);
            level.board.seed = seed;
            level.generation.arrowCoverage = metrics.Coverage;
            level.generation.initialMovableArrowCount = metrics.InitialMovableChains;
            level.generation.targetDifficultyScore = Mathf.RoundToInt(metrics.BlockLinks * 1.5f + metrics.Chains * 0.6f);
            level.generation.fixedGenerationSeed = seed;
            level.generation.minPathLen = spec.MinLength;
            level.generation.maxPathLength = Mathf.Max(spec.MaxLength, metrics.MaxChainLength);
            level.generation.twistiness = spec.TurnBias;
            level.generation.validateWithGreedy = true;
            level.masking.spawnMask = null;
            level.masking.useMaskToDefineBoardSize = false;

            EditorUtility.SetDirty(level);
            if (isNew)
                AssetDatabase.CreateAsset(level, assetPath);

            AssetDatabase.ImportAsset(assetPath);
            return level;
        }

        private static ArrowDifficultyBand PortableDifficultyFromSpec(StyleSpec spec)
        {
            string id = spec.Id ?? string.Empty;
            if (id.Contains("_extreme_") || spec.TargetChains >= 250)
                return ArrowDifficultyBand.Extreme;
            if (id.Contains("_veryhard_") || spec.TargetChains >= 170)
                return ArrowDifficultyBand.VeryHard;
            if (id.Contains("_hard_") || spec.TargetChains >= 115)
                return ArrowDifficultyBand.Hard;
            if (id.Contains("_normal_") || spec.TargetChains >= 55)
                return ArrowDifficultyBand.Normal;
            return ArrowDifficultyBand.Refresh;
        }

        private static ArrowLevelFamily PortableFamilyFromSpec(StyleSpec spec)
        {
            switch (spec.Type)
            {
                case NoMaskType.OuterShell:
                    return ArrowLevelFamily.Shell;
                case NoMaskType.LockBuckle:
                case NoMaskType.CoreBurst:
                    return ArrowLevelFamily.Lock;
                case NoMaskType.MazeLongChain:
                    return ArrowLevelFamily.Maze;
                case NoMaskType.DenseWeave:
                    return ArrowLevelFamily.Dense;
                case NoMaskType.Sweep:
                    return ArrowLevelFamily.Sweep;
                case NoMaskType.SectionUnlock:
                case NoMaskType.DualZone:
                case NoMaskType.StairPush:
                case NoMaskType.QuasiSymmetry:
                    return ArrowLevelFamily.Section;
                default:
                    return ArrowLevelFamily.Unknown;
            }
        }

        private static ArrowLevelFamily PortableFamilyFromMotif(NoMaskMotifKind motif, StyleSpec fallbackSpec)
        {
            switch (motif)
            {
                case NoMaskMotifKind.HuiSpiral:
                case NoMaskMotifKind.LongCorridor:
                    return ArrowLevelFamily.Maze;
                case NoMaskMotifKind.SnakeSpine:
                case NoMaskMotifKind.ZigRiver:
                    return ArrowLevelFamily.Sweep;
                case NoMaskMotifKind.DoubleRoomLock:
                case NoMaskMotifKind.KeyDoor:
                    return ArrowLevelFamily.Lock;
                case NoMaskMotifKind.DoubleShell:
                case NoMaskMotifKind.FourPockets:
                    return ArrowLevelFamily.Shell;
                case NoMaskMotifKind.DenseKernel:
                    return ArrowLevelFamily.Dense;
                case NoMaskMotifKind.CenterCross:
                case NoMaskMotifKind.StairLadder:
                case NoMaskMotifKind.VerticalGate:
                case NoMaskMotifKind.CircuitBoard:
                case NoMaskMotifKind.RoomCorridor:
                    return ArrowLevelFamily.Section;
                case NoMaskMotifKind.ParallelHighway:
                    return ArrowLevelFamily.Maze;
                case NoMaskMotifKind.NestedRooms:
                    return ArrowLevelFamily.Shell;
                default:
                    return PortableFamilyFromSpec(fallbackSpec);
            }
        }

        private static PeelStyleProfile[] GetPeelStyleProfilesForSpec(StyleSpec spec)
        {
            var mediumNormal = new PeelStyleProfile(
                "medium_normal",
                "MediumNormal",
                12011,
                34,
                -1,
                -4,
                28,
                90,
                1,
                20,
                28,
                18,
                4,
                2,
                8,
                10,
                4,
                4,
                16,
                22,
                4,
                4,
                0.10f,
                5,
                13,
                0.26f,
                4,
                900);

            var compactNormal = new PeelStyleProfile(
                "compact_normal",
                "CompactNormal",
                15991,
                36,
                -2,
                -6,
                40,
                88,
                1,
                22,
                22,
                18,
                4,
                3,
                6,
                8,
                3,
                5,
                18,
                24,
                5,
                6,
                0.08f,
                4,
                11,
                0.22f,
                5,
                950);

            var highChain = new PeelStyleProfile(
                "high_chain",
                "HighChain",
                22937,
                16,
                -2,
                -7,
                58,
                180,
                1,
                28,
                18,
                18,
                4,
                3,
                4,
                7,
                4,
                8,
                22,
                18,
                6,
                4,
                0.08f,
                6,
                18,
                0.28f,
                6,
                950);

            var extremeLongChain = new PeelStyleProfile(
                "extreme_long_chain",
                "ExtremeLongChain",
                28817,
                24,
                -1,
                -3,
                18,
                84,
                2,
                30,
                14,
                18,
                4,
                3,
                5,
                7,
                4,
                10,
                22,
                16,
                6,
                6,
                0.055f,
                6,
                16,
                0.13f,
                4,
                900);

            var onion = new PeelStyleProfile(
                "onion",
                "Onion",
                0,
                26,
                0,
                -1,
                16,
                82,
                2,
                14,
                34,
                20,
                4,
                2,
                0,
                10,
                1,
                3,
                17,
                26,
                3,
                0,
                0.18f,
                4,
                18,
                0.36f,
                3,
                850);

            var longChain = new PeelStyleProfile(
                "long",
                "LongChain",
                3571,
                28,
                1,
                3,
                4,
                62,
                3,
                20,
                18,
                12,
                2,
                1,
                5,
                16,
                7,
                2,
                10,
                10,
                2,
                2,
                0.15f,
                3,
                16,
                0.32f,
                1,
                950);

            var lockStyle = new PeelStyleProfile(
                "lock",
                "Lock",
                8111,
                20,
                0,
                1,
                8,
                75,
                3,
                18,
                12,
                16,
                2,
                2,
                18,
                8,
                2,
                4,
                18,
                12,
                4,
                12,
                0.13f,
                3,
                14,
                0.30f,
                3,
                850);

            if (IsDirectAdvancedSpec(spec))
                return new[] { highChain, compactNormal, mediumNormal };

            if (IsDirectPolishExtremeSpec(spec) && spec.Type == NoMaskType.MazeLongChain)
                return new[] { extremeLongChain, mediumNormal, compactNormal };

            if (IsDirectPolishSpec(spec))
                return new[] { highChain, compactNormal, mediumNormal };

            if (IsDirectTunedSpec(spec))
                return new[] { mediumNormal, onion, compactNormal, lockStyle };

            return spec.Type switch
            {
                NoMaskType.MazeLongChain => new[] { longChain, onion },
                NoMaskType.LockBuckle => new[] { lockStyle, onion, longChain },
                NoMaskType.CoreBurst => new[] { lockStyle, longChain, onion },
                NoMaskType.DenseWeave => new[] { onion, lockStyle, longChain },
                NoMaskType.Sweep => new[] { onion, longChain },
                _ => new[] { onion, longChain, lockStyle }
            };
        }

        private static bool IsDirectNormalSpec(StyleSpec spec)
        {
            return spec?.Id != null && spec.Id.StartsWith("direct_normal_", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsDirectDiffSpec(StyleSpec spec)
        {
            return spec?.Id != null && spec.Id.StartsWith("direct_diff_", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsDirectArchitectureSpec(StyleSpec spec)
        {
            return spec?.Id != null && spec.Id.StartsWith("direct_arch_", StringComparison.OrdinalIgnoreCase);
        }

        private static bool ShouldChunkDirectDiffMotif(StyleSpec spec)
        {
            if (!IsDirectDiffSpec(spec))
                return false;

            string id = spec.Id ?? string.Empty;
            return !id.Contains("_refresh_");
        }

        private static bool IsDirectTunedSpec(StyleSpec spec)
        {
            if (spec?.Id == null)
                return false;

            return spec.Id.StartsWith("direct_normal_", StringComparison.OrdinalIgnoreCase)
                || spec.Id.StartsWith("direct_refresh_", StringComparison.OrdinalIgnoreCase)
                || spec.Id.StartsWith("direct_advanced_", StringComparison.OrdinalIgnoreCase)
                || spec.Id.StartsWith("direct_polish_", StringComparison.OrdinalIgnoreCase)
                || spec.Id.StartsWith("direct_diff_", StringComparison.OrdinalIgnoreCase)
                || spec.Id.StartsWith("direct_arch_", StringComparison.OrdinalIgnoreCase)
                || spec.Id.StartsWith("level3_tiny_", StringComparison.OrdinalIgnoreCase)
                || spec.Id.StartsWith("level3_thirty_", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsDirectAdvancedSpec(StyleSpec spec)
        {
            return spec?.Id != null && spec.Id.StartsWith("direct_advanced_", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsDirectPolishSpec(StyleSpec spec)
        {
            return spec?.Id != null && spec.Id.StartsWith("direct_polish_", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsDirectPolishExtremeSpec(StyleSpec spec)
        {
            return spec?.Id != null && spec.Id.StartsWith("direct_polish_extreme_", StringComparison.OrdinalIgnoreCase);
        }

        private static int DirectChainTolerance(StyleSpec spec)
        {
            if (spec == null)
                return 18;

            if (spec.Id != null && spec.Id.StartsWith("direct_polish_extreme_", StringComparison.OrdinalIgnoreCase))
                return 150;
            if (spec.Id != null && spec.Id.StartsWith("direct_polish_hard_", StringComparison.OrdinalIgnoreCase))
                return 46;
            if (spec.Id != null && spec.Id.StartsWith("direct_diff_refresh_", StringComparison.OrdinalIgnoreCase))
                return 16;
            if (spec.Id != null && spec.Id.StartsWith("direct_diff_normal_", StringComparison.OrdinalIgnoreCase))
                return 24;
            if (spec.Id != null && spec.Id.StartsWith("direct_diff_hard_", StringComparison.OrdinalIgnoreCase))
                return 36;
            if (spec.Id != null && spec.Id.StartsWith("direct_diff_veryhard_", StringComparison.OrdinalIgnoreCase))
                return 50;
            if (spec.Id != null && spec.Id.StartsWith("direct_arch_", StringComparison.OrdinalIgnoreCase))
                return 34;
            if (spec.Id != null && spec.Id.StartsWith("level3_tiny_", StringComparison.OrdinalIgnoreCase))
                return 3;
            if (spec.Id != null && spec.Id.StartsWith("level3_thirty_", StringComparison.OrdinalIgnoreCase))
                return 6;
            if (spec.Id != null && spec.Id.StartsWith("direct_normal_pure_extra_", StringComparison.OrdinalIgnoreCase))
                return 20;
            if (spec.Id != null && spec.Id.StartsWith("direct_normal_pure_", StringComparison.OrdinalIgnoreCase))
                return 34;
            if (spec.Id != null && spec.Id.StartsWith("direct_refresh_", StringComparison.OrdinalIgnoreCase))
                return 22;
            if (spec.Id != null && spec.Id.StartsWith("direct_polish_normal_", StringComparison.OrdinalIgnoreCase))
                return 38;

            if (spec.TargetChains < 170)
                return 18;

            return spec.TargetChains >= 240 ? 70 : 55;
        }

        private static int DirectMinAcceptedChains(StyleSpec spec)
        {
            if (!IsDirectTunedSpec(spec))
                return int.MinValue;

            if (spec.Id.StartsWith("direct_advanced_extreme_", StringComparison.OrdinalIgnoreCase))
                return 251;
            if (spec.Id.StartsWith("direct_advanced_veryhard_", StringComparison.OrdinalIgnoreCase))
                return 170;
            if (spec.Id.StartsWith("direct_polish_extreme_", StringComparison.OrdinalIgnoreCase))
                return 251;
            if (spec.Id.StartsWith("direct_polish_hard_", StringComparison.OrdinalIgnoreCase))
                return 120;
            if (spec.Id.StartsWith("direct_diff_refresh_", StringComparison.OrdinalIgnoreCase))
                return 30;
            if (spec.Id.StartsWith("direct_diff_normal_", StringComparison.OrdinalIgnoreCase))
                return 60;
            if (spec.Id.StartsWith("direct_diff_hard_", StringComparison.OrdinalIgnoreCase))
                return 100;
            if (spec.Id.StartsWith("direct_diff_veryhard_", StringComparison.OrdinalIgnoreCase))
                return 135;
            if (spec.Id.StartsWith("direct_arch_", StringComparison.OrdinalIgnoreCase))
                return Mathf.Max(36, spec.TargetChains - 28);
            if (spec.Id.StartsWith("level3_tiny_", StringComparison.OrdinalIgnoreCase))
                return 15;
            if (spec.Id.StartsWith("level3_thirty_", StringComparison.OrdinalIgnoreCase))
                return 24;
            if (spec.Id.StartsWith("direct_normal_pure_extra_", StringComparison.OrdinalIgnoreCase))
                return 101;
            if (spec.Id.StartsWith("direct_normal_pure_", StringComparison.OrdinalIgnoreCase))
                return 72;
            if (spec.Id.StartsWith("direct_refresh_", StringComparison.OrdinalIgnoreCase))
                return 36;

            return 100;
        }

        private static bool DirectChainTargetSatisfied(StyleSpec spec, int chains)
        {
            if (!IsDirectTunedSpec(spec))
                return true;

            return chains >= DirectMinAcceptedChains(spec)
                && Mathf.Abs(chains - spec.TargetChains) <= DirectChainTolerance(spec);
        }

        private static bool TryBuildRectanglePeelAuthored(
            StyleSpec spec,
            int seed,
            PeelStyleProfile profile,
            out AuthoredLevelData authored,
            out string details)
        {
            authored = null;
            details = string.Empty;

            int area = Mathf.Max(1, spec.Width * spec.Height);
            var remaining = new bool[area];
            for (int i = 0; i < remaining.Length; i++)
                remaining[i] = true;

            var rng = new System.Random(seed);
            var chains = new List<List<int>>(Mathf.Max(8, area / 8));
            int remainingCount = area;
            int rejectedShort = 0;
            int failedPeels = 0;
            int guard = area * 3;

            while (remainingCount >= 2 && guard-- > 0)
            {
                int targetLength = PickPeelTargetLength(area, rng, profile);
                if (!TryBuildPeelChain(remaining, spec.Width, spec.Height, rng, targetLength, profile, out List<int> chain))
                {
                    failedPeels++;
                    break;
                }

                if (chain.Count < 2)
                {
                    rejectedShort++;
                    break;
                }

                for (int i = 0; i < chain.Count; i++)
                    remaining[chain[i]] = false;

                remainingCount -= chain.Count;
                chains.Add(chain);
            }

            authored = new AuthoredLevelData
            {
                width = spec.Width,
                height = spec.Height,
                arrows = new List<AuthoredArrowData>(chains.Count),
                blockIndices = new List<int>()
            };

            int filled = 0;
            for (int i = 0; i < chains.Count; i++)
            {
                if (chains[i] == null || chains[i].Count < 2)
                    continue;

                authored.arrows.Add(new AuthoredArrowData
                {
                    indices = new List<int>(chains[i]),
                    colorIndex = i % 8
                });
                filled += chains[i].Count;
            }

            float ratio = filled / (float)area;
            details = $"Peel style={profile.DisplayName} seed={seed} fill={filled}/{area}({ratio:0.000}) chains={authored.arrows.Count} remaining={remainingCount} rejectedShort={rejectedShort} failedPeels={failedPeels}";
            return authored.arrows.Count > 0 && ratio >= 0.88f;
        }

        private static bool TryBuildConstrainedRectanglePeelAuthored(
            StyleSpec spec,
            bool[] canSpawn,
            List<int> blockIndices,
            int seed,
            PeelStyleProfile profile,
            out AuthoredLevelData authored,
            out string details)
        {
            authored = null;
            details = string.Empty;

            int area = Mathf.Max(1, spec.Width * spec.Height);
            if (canSpawn == null || canSpawn.Length != area)
            {
                details = "Invalid canSpawn";
                return false;
            }

            var remaining = new bool[area];
            int remainingCount = 0;
            for (int i = 0; i < remaining.Length; i++)
            {
                remaining[i] = canSpawn[i];
                if (remaining[i])
                    remainingCount++;
            }

            int allowedCount = Mathf.Max(1, remainingCount);
            var rng = new System.Random(seed);
            var chains = new List<List<int>>(Mathf.Max(8, allowedCount / 8));
            int rejectedShort = 0;
            int failedPeels = 0;
            int guard = area * 4;

            while (remainingCount >= 2 && guard-- > 0)
            {
                int targetLength = PickPeelTargetLength(allowedCount, rng, profile);
                if (!TryBuildPeelChain(remaining, spec.Width, spec.Height, rng, targetLength, profile, out List<int> chain))
                {
                    failedPeels++;
                    break;
                }

                if (chain.Count < 2)
                {
                    rejectedShort++;
                    break;
                }

                for (int i = 0; i < chain.Count; i++)
                    remaining[chain[i]] = false;

                remainingCount -= chain.Count;
                chains.Add(chain);
            }

            authored = new AuthoredLevelData
            {
                width = spec.Width,
                height = spec.Height,
                arrows = new List<AuthoredArrowData>(chains.Count),
                blockIndices = new List<int>(blockIndices ?? new List<int>())
            };

            int filled = 0;
            for (int i = 0; i < chains.Count; i++)
            {
                if (chains[i] == null || chains[i].Count < 2)
                    continue;

                authored.arrows.Add(new AuthoredArrowData
                {
                    indices = new List<int>(chains[i]),
                    colorIndex = i % 8
                });
                filled += chains[i].Count;
            }

            float ratio = filled / (float)allowedCount;
            float minRatio = Mathf.Max(0.76f, spec.TargetCoverage - 0.08f);
            details = $"ConstrainedPeel style={profile.DisplayName} seed={seed} fill={filled}/{allowedCount}({ratio:0.000}) chains={authored.arrows.Count} blocks={authored.blockIndices.Count} remaining={remainingCount} rejectedShort={rejectedShort} failedPeels={failedPeels}";
            return authored.arrows.Count > 0 && ratio >= minRatio;
        }

        private static PeelStyleProfile[] GetDirectHolePeelProfiles()
        {
            var balanced = new PeelStyleProfile(
                "hole_balanced",
                "HoleBalanced",
                41813,
                96,
                2,
                6,
                6,
                64,
                4,
                24,
                -18,
                28,
                4,
                3,
                12,
                10,
                3,
                5,
                18,
                20,
                5,
                8,
                0.22f,
                3,
                6,
                0.34f,
                5,
                900);

            var lockProfile = new PeelStyleProfile(
                "hole_lock",
                "HoleLock",
                53117,
                180,
                1,
                5,
                8,
                70,
                4,
                20,
                -22,
                28,
                3,
                3,
                20,
                8,
                3,
                5,
                20,
                16,
                5,
                12,
                0.20f,
                3,
                6,
                0.32f,
                4,
                850);

            var maze = new PeelStyleProfile(
                "hole_maze",
                "HoleMaze",
                66791,
                180,
                3,
                8,
                4,
                58,
                5,
                28,
                -16,
                24,
                3,
                2,
                8,
                14,
                5,
                8,
                20,
                12,
                4,
                8,
                0.18f,
                3,
                6,
                0.30f,
                3,
                900);

            return new[] { balanced, lockProfile, maze };
        }

        private static List<int> BuildCenteredHoleBlockIndices(int width, int height, int holeWidth, int holeHeight)
        {
            int startX = Mathf.Max(0, (width - holeWidth) / 2);
            int startY = Mathf.Max(0, (height - holeHeight) / 2);
            int endX = Mathf.Min(width, startX + holeWidth);
            int endY = Mathf.Min(height, startY + holeHeight);

            var blocks = new List<int>(holeWidth * holeHeight);
            for (int y = startY; y < endY; y++)
            for (int x = startX; x < endX; x++)
                blocks.Add(x + y * width);

            return blocks;
        }

        private static bool[] BuildCanSpawnFromBlocks(int width, int height, List<int> blockIndices)
        {
            var canSpawn = new bool[Mathf.Max(1, width * height)];
            for (int i = 0; i < canSpawn.Length; i++)
                canSpawn[i] = true;

            if (blockIndices != null)
            {
                for (int i = 0; i < blockIndices.Count; i++)
                {
                    int idx = blockIndices[i];
                    if ((uint)idx < (uint)canSpawn.Length)
                        canSpawn[idx] = false;
                }
            }

            return canSpawn;
        }

        private static bool[] BuildCanSpawnForDirectHole(int width, int height, List<int> blockIndices, int outerCutKind)
        {
            bool[] canSpawn = BuildCanSpawnFromBlocks(width, height, blockIndices);
            ApplyDirectHoleOuterCut(canSpawn, width, height, outerCutKind);
            return canSpawn;
        }

        private static void ApplyDirectHoleOuterCut(bool[] canSpawn, int width, int height, int outerCutKind)
        {
            if (canSpawn == null || outerCutKind <= 0)
                return;

            if (outerCutKind == 1)
            {
                int cut = Mathf.Clamp(Mathf.Min(width, height) / 6, 3, 5);
                for (int y = 0; y < cut; y++)
                for (int x = 0; x < cut - y; x++)
                    ClearSpawnCell(canSpawn, width, height, x, y);

                for (int y = height - cut; y < height; y++)
                {
                    int localY = height - 1 - y;
                    for (int x = width - (cut - localY); x < width; x++)
                        ClearSpawnCell(canSpawn, width, height, x, y);
                }

                int notchHeight = Mathf.Clamp(height / 9, 2, 3);
                int notchWidth = Mathf.Clamp(width / 8, 2, 3);
                int startY = Mathf.Max(1, height / 2 - notchHeight / 2);
                for (int y = startY; y < Mathf.Min(height - 1, startY + notchHeight); y++)
                for (int x = 0; x < notchWidth; x++)
                    ClearSpawnCell(canSpawn, width, height, x, y);
            }
        }

        private static void ClearSpawnCell(bool[] canSpawn, int width, int height, int x, int y)
        {
            if ((uint)x >= (uint)width || (uint)y >= (uint)height)
                return;

            int idx = x + y * width;
            if ((uint)idx < (uint)canSpawn.Length)
                canSpawn[idx] = false;
        }

        private static int CountTrue(bool[] values)
        {
            if (values == null)
                return 0;

            int count = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i])
                    count++;
            }

            return count;
        }

        private static int CountHeadsAimingAtBlocks(AuthoredLevelData authored)
        {
            if (authored?.arrows == null || authored.blockIndices == null || authored.blockIndices.Count == 0)
                return 0;

            var blocks = new HashSet<int>(authored.blockIndices);
            int count = 0;
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                AuthoredArrowData arrow = authored.arrows[i];
                if (arrow?.indices == null || arrow.indices.Count < 2)
                    continue;

                Vector2Int head = Pos(arrow.indices[0], authored.width);
                Vector2Int second = Pos(arrow.indices[1], authored.width);
                if (!TryGetOutDir(head, second, out Dir outDir))
                    continue;

                Vector2Int p = head + DirToOffset(outDir);
                while ((uint)p.x < (uint)authored.width && (uint)p.y < (uint)authored.height)
                {
                    int idx = p.x + p.y * authored.width;
                    if (blocks.Contains(idx))
                    {
                        count++;
                        break;
                    }

                    p += DirToOffset(outDir);
                }
            }

            return count;
        }

        private static bool TryBuildMotifPeelAuthored(
            MotifSpec motifSpec,
            int seed,
            PeelStyleProfile profile,
            out AuthoredLevelData authored,
            out int motifCells,
            out string details)
        {
            authored = null;
            motifCells = 0;
            details = string.Empty;

            StyleSpec spec = motifSpec.Style;
            int area = Mathf.Max(1, spec.Width * spec.Height);
            var remaining = new bool[area];
            for (int i = 0; i < remaining.Length; i++)
                remaining[i] = true;

            var chains = new List<List<int>>(Mathf.Max(8, area / 8));
            var reserved = new HashSet<int>();
            if (!TryBuildMotifChains(spec, motifSpec.Motif, chains, reserved, out motifCells, out string motifDetails))
            {
                details = motifDetails;
                return false;
            }

            for (int i = 0; i < chains.Count; i++)
            {
                for (int p = 0; p < chains[i].Count; p++)
                    remaining[chains[i][p]] = false;
            }

            int remainingCount = area - motifCells;
            var rng = new System.Random(seed);
            int rejectedShort = 0;
            int failedPeels = 0;
            int guard = area * 3;
            int filledCount = motifCells;
            bool architectureSpec = IsDirectArchitectureSpec(spec);
            int targetFillCells = architectureSpec
                ? Mathf.Clamp(Mathf.CeilToInt(area * spec.TargetCoverage), motifCells, area)
                : area;

            while (remainingCount >= 2 && guard-- > 0)
            {
                if (architectureSpec && filledCount >= targetFillCells)
                    break;

                int targetLength = PickPeelTargetLength(area, rng, profile);
                if (!TryBuildPeelChain(remaining, spec.Width, spec.Height, rng, targetLength, profile, out List<int> chain))
                {
                    failedPeels++;
                    break;
                }

                if (chain.Count < 2)
                {
                    rejectedShort++;
                    break;
                }

                for (int i = 0; i < chain.Count; i++)
                    remaining[chain[i]] = false;

                remainingCount -= chain.Count;
                filledCount += chain.Count;
                chains.Add(chain);
            }

            authored = new AuthoredLevelData
            {
                width = spec.Width,
                height = spec.Height,
                arrows = new List<AuthoredArrowData>(chains.Count),
                blockIndices = new List<int>()
            };

            int filled = 0;
            for (int i = 0; i < chains.Count; i++)
            {
                if (chains[i] == null || chains[i].Count < 2)
                    continue;

                authored.arrows.Add(new AuthoredArrowData
                {
                    indices = new List<int>(chains[i]),
                    colorIndex = i % 8
                });
                filled += chains[i].Count;
            }

            float ratio = filled / (float)area;
            details = $"MotifPeel motif={motifSpec.Id} style={profile.DisplayName} seed={seed} fill={filled}/{area}({ratio:0.000}) motifCells={motifCells} chains={authored.arrows.Count} remaining={remainingCount} rejectedShort={rejectedShort} failedPeels={failedPeels} | {motifDetails}";
            float minRatio = architectureSpec ? spec.TargetCoverage : 0.88f;
            int minMotifCells = architectureSpec ? Mathf.Max(18, area / 16) : Mathf.Max(12, area / 12);
            return authored.arrows.Count > 0 && ratio >= minRatio && motifCells >= minMotifCells;
        }

        private static bool TryBuildMotifChains(
            StyleSpec spec,
            NoMaskMotifKind motif,
            List<List<int>> chains,
            HashSet<int> reserved,
            out int motifCells,
            out string details)
        {
            motifCells = 0;
            details = string.Empty;
            bool ok = motif switch
            {
                NoMaskMotifKind.HuiSpiral => BuildHuiSpiralMotif(spec, chains, reserved),
                NoMaskMotifKind.SnakeSpine => BuildSnakeSpineMotif(spec, chains, reserved),
                NoMaskMotifKind.DoubleRoomLock => BuildDoubleRoomLockMotif(spec, chains, reserved),
                NoMaskMotifKind.CenterCross => BuildCenterCrossMotif(spec, chains, reserved),
                NoMaskMotifKind.DoubleShell => BuildDoubleShellMotif(spec, chains, reserved),
                NoMaskMotifKind.StairLadder => BuildStairLadderMotif(spec, chains, reserved),
                NoMaskMotifKind.KeyDoor => BuildKeyDoorMotif(spec, chains, reserved),
                NoMaskMotifKind.FourPockets => BuildFourPocketsMotif(spec, chains, reserved),
                NoMaskMotifKind.VerticalGate => BuildVerticalGateMotif(spec, chains, reserved),
                NoMaskMotifKind.ZigRiver => BuildZigRiverMotif(spec, chains, reserved),
                NoMaskMotifKind.DenseKernel => BuildDenseKernelMotif(spec, chains, reserved),
                NoMaskMotifKind.LongCorridor => BuildLongCorridorMotif(spec, chains, reserved),
                NoMaskMotifKind.CircuitBoard => BuildCircuitBoardMotif(spec, chains, reserved),
                NoMaskMotifKind.ParallelHighway => BuildParallelHighwayMotif(spec, chains, reserved),
                NoMaskMotifKind.RoomCorridor => BuildRoomCorridorMotif(spec, chains, reserved),
                NoMaskMotifKind.NestedRooms => BuildNestedRoomsMotif(spec, chains, reserved),
                _ => false
            };

            if (!ok)
            {
                details = $"MotifBuildFailed={motif}";
                return false;
            }

            motifCells = reserved.Count;
            details = $"Motif={motif} motifCells={motifCells}";
            return motifCells > 0;
        }

        private static bool BuildHuiSpiralMotif(StyleSpec spec, List<List<int>> chains, HashSet<int> reserved)
        {
            int sidePad = 2 + (spec.MotifVariant % 2);
            int verticalPad = 3 + (spec.MotifVariant % 3 == 2 ? 1 : 0);
            var path = BuildRectSpiralPath(sidePad, spec.Width - sidePad - 1, verticalPad, spec.Height - verticalPad - 1);
            return TryAddMotifChain(spec, path, chains, reserved);
        }

        private static bool BuildSnakeSpineMotif(StyleSpec spec, List<List<int>> chains, HashSet<int> reserved)
        {
            var path = new List<Vector2Int>();
            int left = 2 + (spec.MotifVariant % 2);
            int right = spec.Width - 3 - (spec.MotifVariant % 3 == 2 ? 1 : 0);
            int bottom = 3 + (spec.MotifVariant % 3 == 1 ? 1 : 0);
            int stepY = spec.MotifVariant % 3 == 2 ? 3 : 2;
            int y = spec.Height - 4 - (spec.MotifVariant % 2);
            bool toRight = true;
            while (y >= bottom)
            {
                if (toRight)
                    AppendLine(path, left, y, right, y);
                else
                    AppendLine(path, right, y, left, y);

                int connectorY = Mathf.Max(bottom, y - stepY + 1);
                if (connectorY < y)
                    AppendLine(path, toRight ? right : left, y - 1, toRight ? right : left, connectorY);
                y -= stepY;
                toRight = !toRight;
            }

            return TryAddMotifChain(spec, path, chains, reserved);
        }

        private static bool BuildDoubleRoomLockMotif(StyleSpec spec, List<List<int>> chains, HashSet<int> reserved)
        {
            int mid = spec.Width / 2;
            int pad = spec.MotifVariant % 2;
            bool ok = TryAddMotifChain(spec, BuildRectSpiralPath(2 + pad, mid - 1, 4, spec.Height - 5 - pad), chains, reserved);
            ok |= TryAddMotifChain(spec, BuildRectSpiralPath(mid + 1, spec.Width - 3 - pad, 4 + pad, spec.Height - 5), chains, reserved);
            var bridge = new List<Vector2Int>();
            int bridgeOffset = spec.MotifVariant % 3 == 2 ? 1 : 0;
            AppendLine(bridge, mid + bridgeOffset, spec.Height / 2 + 3, mid + bridgeOffset, spec.Height / 2 - 3);
            ok |= TryAddMotifChain(spec, bridge, chains, reserved);
            return ok;
        }

        private static bool BuildCenterCrossMotif(StyleSpec spec, List<List<int>> chains, HashSet<int> reserved)
        {
            int cx = spec.Width / 2;
            int cy = spec.Height / 2;
            bool ok = false;
            var top = new List<Vector2Int>();
            AppendLine(top, cx, spec.Height - 1, cx, cy + 2);
            ok |= TryAddMotifChain(spec, top, chains, reserved);

            var bottom = new List<Vector2Int>();
            AppendLine(bottom, cx - 1, 0, cx - 1, cy - 2);
            ok |= TryAddMotifChain(spec, bottom, chains, reserved);

            var left = new List<Vector2Int>();
            AppendLine(left, 0, cy, cx - 3, cy);
            ok |= TryAddMotifChain(spec, left, chains, reserved);

            var right = new List<Vector2Int>();
            AppendLine(right, spec.Width - 1, cy + 1, cx + 2, cy + 1);
            ok |= TryAddMotifChain(spec, right, chains, reserved);
            return ok;
        }

        private static bool BuildDoubleShellMotif(StyleSpec spec, List<List<int>> chains, HashSet<int> reserved)
        {
            int outerInset = 1 + (spec.MotifVariant % 2);
            int innerInset = 4 + (spec.MotifVariant % 2);
            int topInset = spec.MotifVariant % 3 == 2 ? 4 : 3;
            bool ok = TryAddMotifChain(spec, BuildRectFramePath(outerInset, spec.Width - outerInset - 1, 2, spec.Height - topInset), chains, reserved);
            ok |= TryAddMotifChain(spec, BuildRectFramePath(innerInset, spec.Width - innerInset - 1, 6, spec.Height - 7), chains, reserved);
            return ok;
        }

        private static bool BuildStairLadderMotif(StyleSpec spec, List<List<int>> chains, HashSet<int> reserved)
        {
            bool ok = false;
            for (int step = 0; step < 6; step++)
            {
                int y = spec.Height - 4 - step * (spec.MotifVariant % 2 == 0 ? 3 : 4);
                int x = 2 + ((step + spec.MotifVariant) % 3) * 3;
                if (y < 4)
                    break;

                var stair = new List<Vector2Int>();
                int endX = Mathf.Min(spec.Width - 3, x + 4 + (spec.MotifVariant % 2));
                AppendLine(stair, x, y, endX, y);
                AppendLine(stair, endX, y, endX, y - 1);
                ok |= TryAddMotifChain(spec, stair, chains, reserved);
            }

            return ok;
        }

        private static bool BuildKeyDoorMotif(StyleSpec spec, List<List<int>> chains, HashSet<int> reserved)
        {
            int pad = spec.MotifVariant % 2;
            bool ok = TryAddMotifChain(spec, BuildRectSpiralPath(2 + pad, spec.Width / 2 - 1, 6, spec.Height - 7 - pad), chains, reserved);
            var door = new List<Vector2Int>();
            int x = spec.Width - 4 - pad;
            AppendLine(door, x, spec.Height - 4, x, 4 + pad);
            AppendLine(door, x, 4, spec.Width - 2, 4);
            ok |= TryAddMotifChain(spec, door, chains, reserved);
            return ok;
        }

        private static bool BuildFourPocketsMotif(StyleSpec spec, List<List<int>> chains, HashSet<int> reserved)
        {
            bool ok = false;
            ok |= TryAddMotifChain(spec, BuildCornerPocketPath(0, 5, spec.Height - 5, spec.Height - 8), chains, reserved);
            ok |= TryAddMotifChain(spec, BuildCornerPocketPath(spec.Width - 1, spec.Width - 6, spec.Height - 5, spec.Height - 8), chains, reserved);
            ok |= TryAddMotifChain(spec, BuildCornerPocketPath(0, 5, 6, 3), chains, reserved);
            ok |= TryAddMotifChain(spec, BuildCornerPocketPath(spec.Width - 1, spec.Width - 6, 6, 3), chains, reserved);
            return ok;
        }

        private static bool BuildVerticalGateMotif(StyleSpec spec, List<List<int>> chains, HashSet<int> reserved)
        {
            bool ok = false;
            int leftX = spec.Width / 2 - 2;
            int rightX = spec.Width / 2 + 2;
            var left = new List<Vector2Int>();
            AppendLine(left, leftX, spec.Height - 4, leftX, 3);
            ok |= TryAddMotifChain(spec, left, chains, reserved);
            var right = new List<Vector2Int>();
            AppendLine(right, rightX, 3, rightX, spec.Height - 4);
            ok |= TryAddMotifChain(spec, right, chains, reserved);
            var latch = new List<Vector2Int>();
            AppendLine(latch, leftX + 1, spec.Height / 2, rightX - 1, spec.Height / 2);
            ok |= TryAddMotifChain(spec, latch, chains, reserved);
            return ok;
        }

        private static bool BuildZigRiverMotif(StyleSpec spec, List<List<int>> chains, HashSet<int> reserved)
        {
            var path = new List<Vector2Int>();
            int top = spec.Height - 4 - (spec.MotifVariant % 2);
            int mid = spec.Height / 2;
            int bottom = 3 + (spec.MotifVariant % 3 == 2 ? 1 : 0);
            int left = 2 + (spec.MotifVariant % 2);
            int right = spec.Width - 3 - (spec.MotifVariant % 3 == 1 ? 1 : 0);
            AppendLine(path, left, top, right, top);
            AppendLine(path, right, top, right, mid);
            AppendLine(path, right, mid, left + 1, mid);
            AppendLine(path, left + 1, mid, left + 1, bottom);
            AppendLine(path, left + 1, bottom, right, bottom);
            return TryAddMotifChain(spec, path, chains, reserved);
        }

        private static bool BuildDenseKernelMotif(StyleSpec spec, List<List<int>> chains, HashSet<int> reserved)
        {
            int radiusX = 4 + (spec.MotifVariant % 2);
            int radiusY = 5 + (spec.MotifVariant % 3 == 2 ? 1 : 0);
            int left = spec.Width / 2 - radiusX;
            int right = spec.Width / 2 + radiusX;
            int bottom = spec.Height / 2 - radiusY;
            int top = spec.Height / 2 + radiusY;
            bool ok = TryAddMotifChain(spec, BuildRectSpiralPath(left, right, bottom, top), chains, reserved);
            var tail = new List<Vector2Int>();
            AppendLine(tail, left - 2, spec.Height / 2, 2 + (spec.MotifVariant % 2), spec.Height / 2);
            ok |= TryAddMotifChain(spec, tail, chains, reserved);
            return ok;
        }

        private static bool BuildLongCorridorMotif(StyleSpec spec, List<List<int>> chains, HashSet<int> reserved)
        {
            bool ok = false;
            int offset = spec.MotifVariant % 3 - 1;
            var top = new List<Vector2Int>();
            AppendLine(top, 2, spec.Height - 5 + offset, spec.Width - 3, spec.Height - 5 + offset);
            ok |= TryAddMotifChain(spec, top, chains, reserved);
            var middle = new List<Vector2Int>();
            AppendLine(middle, spec.Width - 3, spec.Height / 2 - offset, 2, spec.Height / 2 - offset);
            ok |= TryAddMotifChain(spec, middle, chains, reserved);
            var bottom = new List<Vector2Int>();
            AppendLine(bottom, 2, 4 + offset, spec.Width - 3, 4 + offset);
            ok |= TryAddMotifChain(spec, bottom, chains, reserved);
            return ok;
        }

        private static bool BuildCircuitBoardMotif(StyleSpec spec, List<List<int>> chains, HashSet<int> reserved)
        {
            bool ok = false;
            ok |= TryAddMotifChain(spec, BuildRectFramePath(1, spec.Width - 2, 1, spec.Height - 2), chains, reserved);
            ok |= TryAddMotifChain(spec, BuildRectSpiralPath(3, Mathf.Max(8, spec.Width / 2 - 2), Mathf.Max(3, spec.Height / 2 - 3), spec.Height - 5), chains, reserved);
            ok |= TryAddMotifChain(spec, BuildRectSpiralPath(Mathf.Max(10, spec.Width / 2 + 1), spec.Width - 4, Mathf.Max(5, spec.Height / 2 + 1), spec.Height - 6), chains, reserved);
            ok |= TryAddMotifChain(spec, BuildRectSpiralPath(Mathf.Max(10, spec.Width / 2 + 1), spec.Width - 4, 3, Mathf.Max(8, spec.Height / 2 - 4)), chains, reserved);

            var bus = new List<Vector2Int>();
            int y = Mathf.Clamp(spec.Height / 2, 5, spec.Height - 6);
            AppendLine(bus, 2, y, spec.Width - 3, y);
            AppendLine(bus, spec.Width - 3, y, spec.Width - 3, Mathf.Max(3, y - 5));
            ok |= TryAddMotifChain(spec, bus, chains, reserved);

            var sideLane = new List<Vector2Int>();
            int x = Mathf.Clamp(spec.Width / 2, 6, spec.Width - 7);
            AppendLine(sideLane, x, spec.Height - 4, x, 3);
            AppendLine(sideLane, x, 3, Mathf.Max(2, x - 5), 3);
            ok |= TryAddMotifChain(spec, sideLane, chains, reserved);
            return ok;
        }

        private static bool BuildParallelHighwayMotif(StyleSpec spec, List<List<int>> chains, HashSet<int> reserved)
        {
            bool ok = false;
            int top = spec.Height - 3;
            int lanes = Mathf.Clamp(spec.Height / 5, 5, 7);
            for (int i = 0; i < lanes; i++)
            {
                int y = top - i * 3;
                if (y <= 2)
                    break;

                var lane = new List<Vector2Int>();
                if ((i & 1) == 0)
                {
                    AppendLine(lane, 1, y, spec.Width - 2, y);
                    if (y - 1 > 1)
                        AppendLine(lane, spec.Width - 2, y, spec.Width - 2, y - 1);
                }
                else
                {
                    AppendLine(lane, spec.Width - 2, y, 1, y);
                    if (y - 1 > 1)
                        AppendLine(lane, 1, y, 1, y - 1);
                }

                ok |= TryAddMotifChain(spec, lane, chains, reserved);
            }

            ok |= TryAddMotifChain(spec, BuildRectSpiralPath(3, Mathf.Max(9, spec.Width / 2 - 1), 2, Mathf.Max(8, spec.Height / 3)), chains, reserved);
            ok |= TryAddMotifChain(spec, BuildRectFramePath(Mathf.Max(12, spec.Width / 2 + 2), spec.Width - 4, 2, Mathf.Max(9, spec.Height / 3 + 1)), chains, reserved);
            return ok;
        }

        private static bool BuildRoomCorridorMotif(StyleSpec spec, List<List<int>> chains, HashSet<int> reserved)
        {
            bool ok = false;
            int mid = spec.Width / 2;
            ok |= TryAddMotifChain(spec, BuildRectFramePath(2, mid - 2, spec.Height / 2 + 1, spec.Height - 3), chains, reserved);
            ok |= TryAddMotifChain(spec, BuildRectSpiralPath(mid + 2, spec.Width - 3, spec.Height / 2 + 2, spec.Height - 4), chains, reserved);
            ok |= TryAddMotifChain(spec, BuildRectSpiralPath(2, mid - 2, 2, spec.Height / 2 - 2), chains, reserved);
            ok |= TryAddMotifChain(spec, BuildRectFramePath(mid + 2, spec.Width - 3, 3, spec.Height / 2 - 2), chains, reserved);

            var vertical = new List<Vector2Int>();
            AppendLine(vertical, mid, spec.Height - 3, mid, 2);
            ok |= TryAddMotifChain(spec, vertical, chains, reserved);

            var horizontal = new List<Vector2Int>();
            AppendLine(horizontal, 2, spec.Height / 2, spec.Width - 3, spec.Height / 2);
            ok |= TryAddMotifChain(spec, horizontal, chains, reserved);
            return ok;
        }

        private static bool BuildNestedRoomsMotif(StyleSpec spec, List<List<int>> chains, HashSet<int> reserved)
        {
            bool ok = false;
            ok |= TryAddMotifChain(spec, BuildRectFramePath(1, spec.Width - 2, 1, spec.Height - 2), chains, reserved);
            ok |= TryAddMotifChain(spec, BuildRectFramePath(4, spec.Width - 5, 4, spec.Height - 6), chains, reserved);
            ok |= TryAddMotifChain(spec, BuildRectSpiralPath(7, Mathf.Max(10, spec.Width / 2), 7, Mathf.Max(12, spec.Height / 2 + 2)), chains, reserved);
            ok |= TryAddMotifChain(spec, BuildRectSpiralPath(Mathf.Max(12, spec.Width / 2 + 2), spec.Width - 6, Mathf.Max(7, spec.Height / 2 - 4), spec.Height - 8), chains, reserved);

            var spine = new List<Vector2Int>();
            int x = Mathf.Clamp(spec.Width / 2, 6, spec.Width - 7);
            AppendLine(spine, x, spec.Height - 5, x, 3);
            AppendLine(spine, x, 3, spec.Width - 4, 3);
            ok |= TryAddMotifChain(spec, spine, chains, reserved);
            return ok;
        }

        private static List<Vector2Int> BuildRectSpiralPath(int left, int right, int bottom, int top)
        {
            var path = new List<Vector2Int>();
            while (left <= right && bottom <= top)
            {
                AppendLine(path, left, top, right, top);
                top--;
                if (bottom > top)
                    break;
                AppendLine(path, right, top, right, bottom);
                right--;
                if (left > right)
                    break;
                AppendLine(path, right, bottom, left, bottom);
                bottom++;
                if (bottom > top)
                    break;
                AppendLine(path, left, bottom, left, top);
                left++;
            }

            return path;
        }

        private static List<Vector2Int> BuildRectFramePath(int left, int right, int bottom, int top)
        {
            var path = new List<Vector2Int>();
            AppendLine(path, left, top, right, top);
            AppendLine(path, right, top, right, bottom);
            AppendLine(path, right, bottom, left, bottom);
            AppendLine(path, left, bottom, left, top - 1);
            return path;
        }

        private static List<Vector2Int> BuildCornerPocketPath(int startX, int cornerX, int startY, int endY)
        {
            var path = new List<Vector2Int>();
            AppendLine(path, startX, startY, cornerX, startY);
            AppendLine(path, cornerX, startY, cornerX, endY);
            return path;
        }

        private static void AppendLine(List<Vector2Int> path, int x0, int y0, int x1, int y1)
        {
            if (x0 != x1 && y0 != y1)
                return;

            int dx = Math.Sign(x1 - x0);
            int dy = Math.Sign(y1 - y0);
            int steps = Mathf.Max(Mathf.Abs(x1 - x0), Mathf.Abs(y1 - y0));
            for (int i = 0; i <= steps; i++)
                AppendPoint(path, new Vector2Int(x0 + dx * i, y0 + dy * i));
        }

        private static void AppendPoint(List<Vector2Int> path, Vector2Int point)
        {
            if (path.Count == 0 || path[path.Count - 1] != point)
                path.Add(point);
        }

        private static bool TryAddMotifChain(
            StyleSpec spec,
            List<Vector2Int> path,
            List<List<int>> chains,
            HashSet<int> reserved)
        {
            path = TransformMotifPath(spec, path);
            if ((ShouldChunkDirectDiffMotif(spec) || IsDirectArchitectureSpec(spec)) && path != null && path.Count >= 10)
            {
                if (TryAddDirectDiffMotifChunks(spec, path, chains, reserved))
                    return true;

                var reversedDiffChunks = new List<Vector2Int>(path);
                reversedDiffChunks.Reverse();
                if (TryAddDirectDiffMotifChunks(spec, reversedDiffChunks, chains, reserved))
                    return true;
            }

            if (IsDirectNormalSpec(spec) && path != null && path.Count >= 10)
            {
                if (TryAddMotifChunks(spec, path, chains, reserved))
                    return true;

                var reversedChunks = new List<Vector2Int>(path);
                reversedChunks.Reverse();
                return TryAddMotifChunks(spec, reversedChunks, chains, reserved);
            }

            if (TryBuildMotifIndexPath(spec, path, reserved, out List<int> indices))
            {
                chains.Add(indices);
                for (int i = 0; i < indices.Count; i++)
                    reserved.Add(indices[i]);
                return true;
            }

            var reversed = new List<Vector2Int>(path);
            reversed.Reverse();
            if (!TryBuildMotifIndexPath(spec, reversed, reserved, out indices))
                return false;

            chains.Add(indices);
            for (int i = 0; i < indices.Count; i++)
                reserved.Add(indices[i]);
            return true;
        }

        private static bool TryAddDirectDiffMotifChunks(
            StyleSpec spec,
            List<Vector2Int> path,
            List<List<int>> chains,
            HashSet<int> reserved)
        {
            if (path == null || path.Count < 2)
                return false;

            string id = spec.Id ?? string.Empty;
            if (id.Contains("_refresh_"))
                return false;

            var localReserved = new HashSet<int>(reserved);
            var chunkIndices = new List<List<int>>();
            int cursor = 0;
            int chunkIndex = 0;
            while (cursor < path.Count)
            {
                int remaining = path.Count - cursor;
                if (remaining == 1)
                {
                    if (chunkIndices.Count == 0)
                        return false;

                    Vector2Int tail = path[cursor];
                    if ((uint)tail.x >= (uint)spec.Width || (uint)tail.y >= (uint)spec.Height)
                        return false;

                    int tailIndex = Index(tail, spec.Width);
                    if (localReserved.Contains(tailIndex))
                        return false;

                    chunkIndices[chunkIndices.Count - 1].Add(tailIndex);
                    localReserved.Add(tailIndex);
                    break;
                }

                int chunkLength = DirectDiffMotifChunkLength(spec, path.Count, chunkIndex, remaining);
                var chunkPath = new List<Vector2Int>(chunkLength);
                for (int i = 0; i < chunkLength; i++)
                    chunkPath.Add(path[cursor + i]);

                if (!TryBuildMotifIndexPath(spec, chunkPath, localReserved, out List<int> indices))
                    return false;

                chunkIndices.Add(indices);
                for (int i = 0; i < indices.Count; i++)
                    localReserved.Add(indices[i]);

                cursor += chunkLength;
                chunkIndex++;
            }

            if (chunkIndices.Count == 0)
                return false;

            for (int i = 0; i < chunkIndices.Count; i++)
                chains.Add(chunkIndices[i]);

            foreach (int cell in localReserved)
                reserved.Add(cell);
            return true;
        }

        private static int DirectDiffMotifChunkLength(StyleSpec spec, int pathLength, int chunkIndex, int remaining)
        {
            string id = spec.Id ?? string.Empty;
            int min = 5;
            int max = 9;
            if (id.Contains("_long_spine_"))
            {
                min = 7;
                max = 12;
            }
            else if (id.Contains("_room_gate_"))
            {
                min = 6;
                max = 10;
            }
            else if (id.Contains("_near_square_"))
            {
                min = 5;
                max = 9;
            }
            else if (id.Contains("_dag_unlock_"))
            {
                min = 4;
                max = 8;
            }

            if (id.Contains("_hard_") || id.Contains("_veryhard_"))
            {
                min = Mathf.Max(3, min - 1);
                max = Mathf.Max(min, max - 1);
            }

            int span = Mathf.Max(1, max - min + 1);
            int chunkLength = min + ((spec.MotifVariant + chunkIndex + pathLength) % span);
            chunkLength = Mathf.Clamp(chunkLength, 2, remaining);
            if (remaining - chunkLength == 1)
                chunkLength = remaining;
            return chunkLength;
        }

        private static bool TryAddMotifChunks(
            StyleSpec spec,
            List<Vector2Int> path,
            List<List<int>> chains,
            HashSet<int> reserved)
        {
            if (path == null || path.Count < 2)
                return false;

            var localReserved = new HashSet<int>(reserved);
            var chunkIndices = new List<List<int>>();
            int cursor = 0;
            int chunkIndex = 0;
            while (cursor < path.Count)
            {
                int remaining = path.Count - cursor;
                if (remaining == 1)
                {
                    if (chunkIndices.Count == 0)
                        return false;

                    Vector2Int tail = path[cursor];
                    if ((uint)tail.x >= (uint)spec.Width || (uint)tail.y >= (uint)spec.Height)
                        return false;

                    int tailIndex = Index(tail, spec.Width);
                    if (localReserved.Contains(tailIndex))
                        return false;

                    chunkIndices[chunkIndices.Count - 1].Add(tailIndex);
                    localReserved.Add(tailIndex);
                    break;
                }

                int chunkLength = 4 + ((spec.MotifVariant + chunkIndex + path.Count) % 4);
                chunkLength = Mathf.Clamp(chunkLength, 2, remaining);
                if (remaining - chunkLength == 1)
                    chunkLength = remaining;

                var chunkPath = new List<Vector2Int>(chunkLength);
                for (int i = 0; i < chunkLength; i++)
                    chunkPath.Add(path[cursor + i]);

                if (!TryBuildMotifIndexPath(spec, chunkPath, localReserved, out List<int> indices))
                    return false;

                chunkIndices.Add(indices);
                for (int i = 0; i < indices.Count; i++)
                    localReserved.Add(indices[i]);

                cursor += chunkLength;
                chunkIndex++;
            }

            if (chunkIndices.Count == 0)
                return false;

            for (int i = 0; i < chunkIndices.Count; i++)
            {
                chains.Add(chunkIndices[i]);
                for (int p = 0; p < chunkIndices[i].Count; p++)
                    reserved.Add(chunkIndices[i][p]);
            }

            return true;
        }

        private static List<Vector2Int> TransformMotifPath(StyleSpec spec, List<Vector2Int> path)
        {
            if (path == null || (!spec.MotifMirrorX && !spec.MotifMirrorY))
                return path;

            var transformed = new List<Vector2Int>(path.Count);
            for (int i = 0; i < path.Count; i++)
            {
                Vector2Int point = path[i];
                transformed.Add(new Vector2Int(
                    spec.MotifMirrorX ? spec.Width - 1 - point.x : point.x,
                    spec.MotifMirrorY ? spec.Height - 1 - point.y : point.y));
            }

            return transformed;
        }

        private static bool TryBuildMotifIndexPath(
            StyleSpec spec,
            List<Vector2Int> path,
            HashSet<int> reserved,
            out List<int> indices)
        {
            indices = null;
            if (path == null || path.Count < 2)
                return false;

            var local = new HashSet<int>();
            var cleaned = new List<Vector2Int>(path.Count);
            for (int i = 0; i < path.Count; i++)
            {
                Vector2Int cell = path[i];
                if ((uint)cell.x >= (uint)spec.Width || (uint)cell.y >= (uint)spec.Height)
                    return false;

                if (i > 0)
                {
                    Vector2Int delta = cell - path[i - 1];
                    if (Mathf.Abs(delta.x) + Mathf.Abs(delta.y) != 1)
                        return false;
                }

                int idx = Index(cell, spec.Width);
                if (reserved.Contains(idx) || !local.Add(idx))
                    return false;
                cleaned.Add(cell);
            }

            if (!TryGetOutDir(cleaned[0], cleaned[1], out Dir outDir))
                return false;

            if (PathContains(cleaned, cleaned[0] + DirToOffset(outDir)))
                return false;

            indices = new List<int>(cleaned.Count);
            for (int i = 0; i < cleaned.Count; i++)
                indices.Add(Index(cleaned[i], spec.Width));

            return true;
        }

        private static int PickPeelTargetLength(int area, System.Random rng, PeelStyleProfile profile)
        {
            int min = area < 420 ? 4 : 5;
            int max = area < 420 ? 10 : 14;
            if (profile != null)
            {
                min = Mathf.Max(2, min + profile.MinLengthAdd);
                max = Mathf.Max(min, max + profile.MaxLengthAdd);
            }

            int roll = rng.Next(100);
            int shortRollPercent = profile != null ? profile.ShortRollPercent : 12;
            int longRollPercent = profile != null ? profile.LongRollPercent : 76;
            int longMinAdd = profile != null ? profile.LongMinAdd : 3;
            if (roll < shortRollPercent)
                max = Mathf.Min(max, area < 420 ? 6 : 8);
            else if (roll > longRollPercent)
                min = Mathf.Min(max, min + longMinAdd);

            return rng.Next(min, max + 1);
        }

        private static bool TryBuildPeelChain(
            bool[] remaining,
            int width,
            int height,
            System.Random rng,
            int targetLength,
            PeelStyleProfile profile,
            out List<int> chain)
        {
            chain = null;
            var candidates = new List<PeelHeadCandidate>(64);
            CollectPeelHeadCandidates(remaining, width, height, rng, requireClearRay: true, profile, candidates);
            if (candidates.Count == 0)
                CollectPeelHeadCandidates(remaining, width, height, rng, requireClearRay: false, profile, candidates);

            if (candidates.Count == 0)
                return false;

            candidates.Sort((a, b) => b.Score.CompareTo(a.Score));
            int pickWindow = profile != null ? profile.HeadPickWindow : 18;
            int pickCount = Mathf.Min(candidates.Count, Mathf.Max(1, pickWindow));
            PeelHeadCandidate picked = candidates[rng.Next(pickCount)];

            chain = new List<int>(Mathf.Max(2, targetLength))
            {
                picked.Head,
                picked.Second
            };

            var inPath = new HashSet<int>
            {
                picked.Head,
                picked.Second
            };

            int current = picked.Second;
            int previous = picked.Head;
            int localGuard = targetLength * 6 + 12;
            while (chain.Count < targetLength && localGuard-- > 0)
            {
                if (!TryPickPeelNextCell(remaining, width, height, rng, current, previous, inPath, chain.Count, profile, out int next))
                    break;

                chain.Add(next);
                inPath.Add(next);
                previous = current;
                current = next;
            }

            return chain.Count >= 2;
        }

        private static void CollectPeelHeadCandidates(
            bool[] remaining,
            int width,
            int height,
            System.Random rng,
            bool requireClearRay,
            PeelStyleProfile profile,
            List<PeelHeadCandidate> candidates)
        {
            candidates.Clear();

            for (int idx = 0; idx < remaining.Length; idx++)
            {
                if (!remaining[idx])
                    continue;

                int x = idx % width;
                int y = idx / width;
                for (int d = 0; d < 4; d++)
                {
                    Dir outDir = (Dir)d;
                    Vector2Int outOffset = DirToOffset(outDir);
                    int secondX = x - outOffset.x;
                    int secondY = y - outOffset.y;
                    if (!InBounds(secondX, secondY, width, height))
                        continue;

                    int secondIdx = secondX + secondY * width;
                    if (!remaining[secondIdx])
                        continue;

                    int frontX = x + outOffset.x;
                    int frontY = y + outOffset.y;
                    if (InBounds(frontX, frontY, width, height) && remaining[frontX + frontY * width])
                        continue;

                    if (requireClearRay && PeelRayHitsRemaining(remaining, width, height, frontX, frontY, outOffset))
                        continue;

                    int score = 100;
                    score += IsBoardEdge(x, y, width, height) ? (profile != null ? profile.HeadBoardEdgeBonus : 40) : 0;
                    score += CountPeelClearedRayCells(remaining, width, height, frontX, frontY, outOffset)
                        * (profile != null ? profile.HeadClearRayMultiplier : 3);
                    score += IsPeelBoundaryCell(remaining, width, height, secondX, secondY)
                        ? (profile != null ? profile.HeadBoundaryBonus : 10)
                        : 0;
                    score -= CountPeelRemainingNeighbors(remaining, width, height, x, y)
                        * (profile != null ? profile.HeadNeighborPenalty : 2);
                    score += ComputePeelCenterScore(x, y, width, height) * (profile != null ? profile.CenterHeadBonus : 0);
                    score += rng.Next(17);

                    candidates.Add(new PeelHeadCandidate
                    {
                        Head = idx,
                        Second = secondIdx,
                        OutDir = outDir,
                        Score = score
                    });
                }
            }
        }

        private static bool TryPickPeelNextCell(
            bool[] remaining,
            int width,
            int height,
            System.Random rng,
            int current,
            int previous,
            HashSet<int> inPath,
            int pathCount,
            PeelStyleProfile profile,
            out int next)
        {
            next = -1;
            int currentX = current % width;
            int currentY = current / width;
            int previousX = previous % width;
            int previousY = previous / width;
            var currentDir = new Vector2Int(currentX - previousX, currentY - previousY);

            int bestScore = int.MinValue;
            int tieCount = 0;
            for (int d = 0; d < 4; d++)
            {
                Vector2Int offset = DirToOffset((Dir)d);
                int nx = currentX + offset.x;
                int ny = currentY + offset.y;
                if (!InBounds(nx, ny, width, height))
                    continue;

                int nidx = nx + ny * width;
                if (!remaining[nidx] || inPath.Contains(nidx))
                    continue;

                int score = rng.Next(19);
                bool sameDirection = offset == currentDir;
                if (sameDirection)
                    score += pathCount <= 3
                        ? (profile != null ? profile.SameDirectionEarlyBonus : 12)
                        : (profile != null ? profile.SameDirectionLaterBonus : 2);
                else
                    score += pathCount >= 3
                        ? (profile != null ? profile.TurnLaterBonus : 15)
                        : (profile != null ? profile.TurnEarlyBonus : 2);

                if (IsPeelBoundaryCell(remaining, width, height, nx, ny))
                    score += profile != null ? profile.BoundaryStepBonus : 14;

                int neighborCount = CountPeelRemainingNeighbors(remaining, width, height, nx, ny);
                score += Mathf.Max(0, 4 - neighborCount) * (profile != null ? profile.SparseNeighborWeight : 3);
                score += ComputePeelCenterScore(nx, ny, width, height) * (profile != null ? profile.CenterStepBonus : 0);

                if (score > bestScore)
                {
                    bestScore = score;
                    next = nidx;
                    tieCount = 1;
                }
                else if (score == bestScore)
                {
                    tieCount++;
                    if (rng.Next(tieCount) == 0)
                        next = nidx;
                }
            }

            return next >= 0;
        }

        private static BuildMetrics BuildMetricsFromAuthored(StyleSpec spec, AuthoredLevelData authored)
        {
            var metrics = new BuildMetrics();
            if (authored == null || authored.arrows == null)
                return metrics;

            var occupied = new bool[spec.Width * spec.Height];
            int arrowCount = 0;
            int edgeHeadChains = 0;
            int shortEdgeChains = 0;
            int maxLength = 0;
            int straightSegments = 0;
            int totalSegments = 0;

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                AuthoredArrowData arrow = authored.arrows[i];
                if (arrow.indices == null || arrow.indices.Count < 2)
                    continue;

                var path = new List<Vector2Int>(arrow.indices.Count);
                for (int p = 0; p < arrow.indices.Count; p++)
                {
                    Vector2Int cell = Pos(arrow.indices[p], spec.Width);
                    path.Add(cell);
                    if ((uint)cell.x < (uint)spec.Width && (uint)cell.y < (uint)spec.Height)
                        occupied[Index(cell, spec.Width)] = true;
                }

                arrowCount += arrow.indices.Count;
                maxLength = Mathf.Max(maxLength, arrow.indices.Count);
                if (arrow.indices.Count <= 4)
                    shortEdgeChains++;
                if (TryGetOutDir(path[0], path[1], out Dir outDir) && IsBoundaryOutHead(spec, path[0], outDir))
                    edgeHeadChains++;
                CountStraightSegments(path, ref straightSegments, ref totalSegments);
            }

            metrics.ArrowCount = arrowCount;
            metrics.Chains = authored.arrows.Count;
            metrics.BlockLinks = CountAuthoredBlockLinks(authored);
            metrics.EdgeHeadChains = edgeHeadChains;
            metrics.ShortEdgeChains = shortEdgeChains;
            metrics.MaxChainLength = maxLength;
            metrics.AverageChainLength = authored.arrows.Count > 0 ? (float)arrowCount / authored.arrows.Count : 0f;
            metrics.Coverage = (float)arrowCount / Mathf.Max(1, spec.Width * spec.Height);
            metrics.OuterBandCoverage = CountOccupiedOuterBand(spec, occupied) / Mathf.Max(1f, CountOuterBandCells(spec));
            metrics.Straightness = totalSegments > 0 ? (float)straightSegments / totalSegments : 0f;
            return metrics;
        }

        private static int CountAuthoredBlockLinks(AuthoredLevelData authored)
        {
            if (authored == null || authored.arrows == null)
                return 0;

            var rays = new List<HashSet<int>>(authored.arrows.Count);
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var ray = new HashSet<int>();
                AuthoredArrowData arrow = authored.arrows[i];
                if (arrow.indices != null && arrow.indices.Count >= 2)
                {
                    Vector2Int head = Pos(arrow.indices[0], authored.width);
                    Vector2Int second = Pos(arrow.indices[1], authored.width);
                    if (TryGetOutDir(head, second, out Dir outDir))
                        FillEscapeRay(authored.width, authored.height, head, outDir, ray);
                }

                rays.Add(ray);
            }

            int links = 0;
            for (int i = 0; i < authored.arrows.Count; i++)
            for (int j = 0; j < authored.arrows.Count; j++)
            {
                if (i == j || rays[i].Count == 0)
                    continue;

                AuthoredArrowData arrow = authored.arrows[j];
                if (arrow.indices == null)
                    continue;

                for (int p = 0; p < arrow.indices.Count; p++)
                {
                    if (!rays[i].Contains(arrow.indices[p]))
                        continue;

                    links++;
                    break;
                }
            }

            return links;
        }

        private static bool PeelRayHitsRemaining(bool[] remaining, int width, int height, int x, int y, Vector2Int offset)
        {
            while (InBounds(x, y, width, height))
            {
                if (remaining[x + y * width])
                    return true;

                x += offset.x;
                y += offset.y;
            }

            return false;
        }

        private static int CountPeelClearedRayCells(bool[] remaining, int width, int height, int x, int y, Vector2Int offset)
        {
            int count = 0;
            while (InBounds(x, y, width, height))
            {
                if (!remaining[x + y * width])
                    count++;

                x += offset.x;
                y += offset.y;
            }

            return count;
        }

        private static bool IsPeelBoundaryCell(bool[] remaining, int width, int height, int x, int y)
        {
            for (int d = 0; d < 4; d++)
            {
                Vector2Int offset = DirToOffset((Dir)d);
                int nx = x + offset.x;
                int ny = y + offset.y;
                if (!InBounds(nx, ny, width, height))
                    return true;

                if (!remaining[nx + ny * width])
                    return true;
            }

            return false;
        }

        private static int CountPeelRemainingNeighbors(bool[] remaining, int width, int height, int x, int y)
        {
            int count = 0;
            for (int d = 0; d < 4; d++)
            {
                Vector2Int offset = DirToOffset((Dir)d);
                int nx = x + offset.x;
                int ny = y + offset.y;
                if (InBounds(nx, ny, width, height) && remaining[nx + ny * width])
                    count++;
            }

            return count;
        }

        private static int ComputePeelCenterScore(int x, int y, int width, int height)
        {
            if (width <= 2 || height <= 2)
                return 0;

            int edgeDistance = Mathf.Min(Mathf.Min(x, width - 1 - x), Mathf.Min(y, height - 1 - y));
            int maxDistance = Mathf.Max(1, Mathf.Min(width, height) / 2);
            return Mathf.Clamp(Mathf.RoundToInt(edgeDistance * 4f / maxDistance), 0, 4);
        }

        private static bool InBounds(int x, int y, int width, int height)
        {
            return (uint)x < (uint)width && (uint)y < (uint)height;
        }

        private static bool IsBoardEdge(int x, int y, int width, int height)
        {
            return x == 0 || y == 0 || x == width - 1 || y == height - 1;
        }

        private static bool TryBuildVirtualCropAuthored(
            StyleSpec spec,
            System.Random rng,
            out AuthoredLevelData authored,
            out BuildMetrics metrics,
            out string status)
        {
            authored = null;
            metrics = new BuildMetrics();
            status = string.Empty;

            for (int cropAttempt = 0; cropAttempt < 3; cropAttempt++)
            {
                StyleSpec sourceSpec = CreateVirtualSourceSpec(spec, rng, cropAttempt);
                if (!TryBuildRawAuthored(sourceSpec, rng, out AuthoredLevelData sourceAuthored, out _, out status))
                    continue;

                if (!TryCropAuthoredToPlaced(sourceAuthored, spec, rng, out List<PlacedChain> placed, out bool[] occupied, out status))
                    continue;

                int targetCells = Mathf.CeilToInt(spec.Width * spec.Height * spec.TargetCoverage);
                int outerBandTarget = Mathf.CeilToInt(CountOuterBandCells(spec) * spec.OuterBandTarget);
                DensifyByTailExtension(spec, rng, occupied, placed, outerBandTarget, outerBandOnly: true);
                AddShortEdgePatchChains(spec, rng, occupied, placed, outerBandTarget);
                DensifyByTailExtension(spec, rng, occupied, placed, targetCells, outerBandOnly: false);
                AddShortFillPatchChains(spec, rng, occupied, placed, targetCells);
                DensifyByTailExtension(spec, rng, occupied, placed, outerBandTarget, outerBandOnly: true);
                AddShortEdgePatchChains(spec, rng, occupied, placed, outerBandTarget);

                if (!BuildAuthoredFromPlaced(spec, placed, occupied, CountBlockLinksFromPlaced(placed, spec.Width), out authored, out metrics, out status))
                    continue;

                return true;
            }

            status = $"virtual crop failed: {status}";
            return false;
        }

        private static StyleSpec CreateVirtualSourceSpec(StyleSpec spec, System.Random rng, int cropAttempt)
        {
            int extraWidth = Mathf.Max(2, Mathf.RoundToInt(spec.Width * 0.12f));
            int extraHeight = Mathf.Max(3, Mathf.RoundToInt(spec.Height * 0.12f));
            if (cropAttempt >= 2)
                extraHeight++;
            if (cropAttempt >= 3)
                extraWidth++;

            int sourceWidth = spec.Width + extraWidth;
            int sourceHeight = spec.Height + extraHeight;
            float areaScale = (sourceWidth * sourceHeight) / Mathf.Max(1f, spec.Width * spec.Height);

            return new StyleSpec
            {
                Type = spec.Type,
                Id = $"{spec.Id}_virtual_source",
                DisplayName = spec.DisplayName,
                Width = sourceWidth,
                Height = sourceHeight,
                TargetChains = Mathf.Clamp(Mathf.RoundToInt(spec.TargetChains * Mathf.Lerp(0.86f, 0.98f, Mathf.Clamp01(areaScale - 1f))), spec.TargetChains + 2, spec.TargetChains + 7),
                MinLength = spec.MinLength,
                MaxLength = spec.MaxLength + (cropAttempt >= 2 ? 1 : 0),
                TargetCoverage = Mathf.Clamp(spec.TargetCoverage - 0.01f, 0.88f, 0.92f),
                TurnBias = Mathf.Clamp01(spec.TurnBias + 0.03f),
                BlockWeight = spec.BlockWeight,
                EdgeOpeningBias = Mathf.Clamp(spec.EdgeOpeningBias + 0.02f, 0.20f, 0.50f),
                OuterBandTarget = 0.66f,
                MaxInitialMovableChains = spec.MaxInitialMovableChains + 8,
                MaxShortEdgePatchChains = spec.MaxShortEdgePatchChains + 2,
                MaxShortFillPatchChains = spec.MaxShortFillPatchChains + 4,
                Seed = spec.Seed + 17003 + cropAttempt * 193 + rng.Next(0, 997)
            };
        }

        private static bool TryBuildRawAuthored(
            StyleSpec spec,
            System.Random rng,
            out AuthoredLevelData authored,
            out BuildMetrics metrics,
            out string status)
        {
            authored = new AuthoredLevelData
            {
                width = spec.Width,
                height = spec.Height,
                arrows = new List<AuthoredArrowData>(spec.TargetChains),
                blockIndices = new List<int>()
            };
            metrics = new BuildMetrics();
            status = string.Empty;

            var occupied = new bool[spec.Width * spec.Height];
            var placed = new List<PlacedChain>(spec.TargetChains);
            int blockLinks = 0;

            for (int i = 0; i < spec.TargetChains; i++)
            {
                if (!TryPickCandidate(spec, rng, i, occupied, placed, out ChainCandidate candidate))
                {
                    status = $"candidate failed at chain {i + 1}/{spec.TargetChains}";
                    return false;
                }

                var placedChain = new PlacedChain
                {
                    OutDir = candidate.OutDir
                };
                placedChain.HeadToTail.AddRange(candidate.HeadToTail);
                FillEscapeRay(spec.Width, spec.Height, candidate.HeadToTail[0], candidate.OutDir, placedChain.EscapeRay);
                placed.Add(placedChain);
                blockLinks += candidate.BlocksExisting;

                for (int p = 0; p < candidate.HeadToTail.Count; p++)
                {
                    Vector2Int cell = candidate.HeadToTail[p];
                    occupied[Index(cell, spec.Width)] = true;
                }
            }

            int targetCells = Mathf.CeilToInt(spec.Width * spec.Height * spec.TargetCoverage);
            int outerBandTarget = Mathf.CeilToInt(CountOuterBandCells(spec) * spec.OuterBandTarget);
            DensifyByTailExtension(spec, rng, occupied, placed, outerBandTarget, outerBandOnly: true);
            AddShortEdgePatchChains(spec, rng, occupied, placed, outerBandTarget);
            DensifyByTailExtension(spec, rng, occupied, placed, targetCells, outerBandOnly: false);
            AddShortFillPatchChains(spec, rng, occupied, placed, targetCells);
            DensifyByTailExtension(spec, rng, occupied, placed, outerBandTarget, outerBandOnly: true);
            AddShortEdgePatchChains(spec, rng, occupied, placed, outerBandTarget);

            int arrowCount = 0;
            int edgeHeadChains = 0;
            int shortEdgeChains = 0;
            int maxLength = 0;
            int straightSegments = 0;
            int totalSegments = 0;

            for (int i = 0; i < placed.Count; i++)
            {
                PlacedChain chain = placed[i];
                var arrow = new AuthoredArrowData
                {
                    indices = new List<int>(chain.HeadToTail.Count),
                    colorIndex = i % 6
                };

                for (int p = 0; p < chain.HeadToTail.Count; p++)
                    arrow.indices.Add(Index(chain.HeadToTail[p], spec.Width));

                authored.arrows.Add(arrow);

                arrowCount += chain.HeadToTail.Count;
                if (IsBoundaryOutHead(spec, chain.HeadToTail[0], chain.OutDir))
                    edgeHeadChains++;
                if (chain.HeadToTail.Count <= 4)
                    shortEdgeChains++;
                maxLength = Mathf.Max(maxLength, chain.HeadToTail.Count);
                CountStraightSegments(chain.HeadToTail, ref straightSegments, ref totalSegments);
            }

            metrics.ArrowCount = arrowCount;
            metrics.Chains = authored.arrows.Count;
            metrics.BlockLinks = blockLinks;
            metrics.EdgeHeadChains = edgeHeadChains;
            metrics.ShortEdgeChains = shortEdgeChains;
            metrics.MaxChainLength = maxLength;
            metrics.AverageChainLength = authored.arrows.Count > 0 ? (float)arrowCount / authored.arrows.Count : 0f;
            metrics.Coverage = (float)arrowCount / Mathf.Max(1, spec.Width * spec.Height);
            metrics.OuterBandCoverage = CountOccupiedOuterBand(spec, occupied) / Mathf.Max(1f, CountOuterBandCells(spec));
            metrics.Straightness = totalSegments > 0 ? (float)straightSegments / totalSegments : 0f;

            if (arrowCount + 2 < targetCells)
            {
                status = $"coverage below target {arrowCount}/{targetCells} ({metrics.Coverage:0.000}/{spec.TargetCoverage:0.000})";
                return false;
            }

            int outerBandOccupied = CountOccupiedOuterBand(spec, occupied);
            if (outerBandOccupied + 1 < outerBandTarget)
            {
                status = $"outer band below target {outerBandOccupied}/{outerBandTarget} ({metrics.OuterBandCoverage:0.000}/{spec.OuterBandTarget:0.000})";
                return false;
            }

            return true;
        }

        private static bool TryCropAuthoredToPlaced(
            AuthoredLevelData sourceAuthored,
            StyleSpec finalSpec,
            System.Random rng,
            out List<PlacedChain> placed,
            out bool[] occupied,
            out string status)
        {
            placed = new List<PlacedChain>();
            occupied = new bool[finalSpec.Width * finalSpec.Height];
            status = string.Empty;

            int extraWidth = Mathf.Max(0, sourceAuthored.width - finalSpec.Width);
            int extraHeight = Mathf.Max(0, sourceAuthored.height - finalSpec.Height);
            int cropX = extraWidth <= 0 ? 0 : rng.Next(0, extraWidth + 1);
            int cropY = extraHeight <= 0 ? 0 : rng.Next(0, extraHeight + 1);

            for (int i = 0; i < sourceAuthored.arrows.Count; i++)
            {
                AuthoredArrowData arrow = sourceAuthored.arrows[i];
                if (arrow.indices == null || arrow.indices.Count < 2)
                    continue;

                var run = new List<Vector2Int>();
                for (int p = 0; p < arrow.indices.Count; p++)
                {
                    Vector2Int sourceCell = Pos(arrow.indices[p], sourceAuthored.width);
                    if (TryRemapCroppedCell(sourceCell, cropX, cropY, finalSpec.Width, finalSpec.Height, out Vector2Int finalCell))
                    {
                        run.Add(finalCell);
                        continue;
                    }

                    FlushCroppedRun(finalSpec, run, placed, occupied);
                    run.Clear();
                }

                FlushCroppedRun(finalSpec, run, placed, occupied);
            }

            if (placed.Count < Mathf.Max(8, finalSpec.TargetChains / 2))
            {
                status = $"crop produced too few chains {placed.Count}";
                return false;
            }

            return true;
        }

        private static bool TryRemapCroppedCell(
            Vector2Int sourceCell,
            int cropX,
            int cropY,
            int width,
            int height,
            out Vector2Int finalCell)
        {
            finalCell = new Vector2Int(sourceCell.x - cropX, sourceCell.y - cropY);
            return (uint)finalCell.x < (uint)width && (uint)finalCell.y < (uint)height;
        }

        private static void FlushCroppedRun(
            StyleSpec spec,
            List<Vector2Int> run,
            List<PlacedChain> placed,
            bool[] occupied)
        {
            if (run.Count < 2)
                return;

            var candidate = new List<Vector2Int>(run);
            if (run.Count <= 4)
            {
                var reverseCandidate = new List<Vector2Int>(run);
                reverseCandidate.Reverse();
                if (ScoreCroppedOrientation(spec, reverseCandidate, occupied) > ScoreCroppedOrientation(spec, candidate, occupied))
                    candidate = reverseCandidate;
            }

            if (!TryAddCroppedChain(spec, candidate, placed, occupied))
            {
                candidate.Reverse();
                TryAddCroppedChain(spec, candidate, placed, occupied);
            }
        }

        private static int ScoreCroppedOrientation(StyleSpec spec, List<Vector2Int> path, bool[] occupied)
        {
            if (path.Count < 2 || !TryGetOutDir(path[0], path[1], out Dir outDir))
                return int.MinValue;

            int score = 0;
            if (CountTurns(path) > 0)
                score += 2;
            if (path.Count >= 5)
                score += 1;

            bool boundaryOut = IsBoundaryOutHead(spec, path[0], outDir);
            if (boundaryOut)
                score += path.Count <= 4 ? -6 : 1;

            if (RayClear(spec.Width, spec.Height, path[0], outDir, occupied))
                score += boundaryOut ? 2 : -1;

            if (IsOuterBand(spec, path[0]) && !boundaryOut)
                score += 1;

            return score;
        }

        private static bool TryAddCroppedChain(
            StyleSpec spec,
            List<Vector2Int> path,
            List<PlacedChain> placed,
            bool[] occupied)
        {
            if (path.Count < 2 || !TryGetOutDir(path[0], path[1], out Dir outDir))
                return false;

            var local = new HashSet<int>();
            for (int i = 0; i < path.Count; i++)
            {
                Vector2Int cell = path[i];
                if ((uint)cell.x >= (uint)spec.Width || (uint)cell.y >= (uint)spec.Height)
                    return false;

                int idx = Index(cell, spec.Width);
                if (occupied[idx] || !local.Add(idx))
                    return false;

                if (i > 0)
                {
                    Vector2Int delta = path[i] - path[i - 1];
                    if (Mathf.Abs(delta.x) + Mathf.Abs(delta.y) != 1)
                        return false;
                }
            }

            if (PathContains(path, path[0] + DirToOffset(outDir)))
                return false;

            var chain = new PlacedChain
            {
                OutDir = outDir
            };
            chain.HeadToTail.AddRange(path);
            FillEscapeRay(spec.Width, spec.Height, path[0], outDir, chain.EscapeRay);
            placed.Add(chain);

            for (int i = 0; i < path.Count; i++)
                occupied[Index(path[i], spec.Width)] = true;

            return true;
        }

        private static bool TryGetOutDir(Vector2Int head, Vector2Int second, out Dir outDir)
        {
            Vector2Int offset = head - second;
            if (offset == Vector2Int.up)
            {
                outDir = Dir.Up;
                return true;
            }

            if (offset == Vector2Int.right)
            {
                outDir = Dir.Right;
                return true;
            }

            if (offset == Vector2Int.down)
            {
                outDir = Dir.Down;
                return true;
            }

            if (offset == Vector2Int.left)
            {
                outDir = Dir.Left;
                return true;
            }

            outDir = default;
            return false;
        }

        private static bool BuildAuthoredFromPlaced(
            StyleSpec spec,
            List<PlacedChain> placed,
            bool[] occupied,
            int blockLinks,
            out AuthoredLevelData authored,
            out BuildMetrics metrics,
            out string status)
        {
            authored = new AuthoredLevelData
            {
                width = spec.Width,
                height = spec.Height,
                arrows = new List<AuthoredArrowData>(placed.Count),
                blockIndices = new List<int>()
            };
            metrics = new BuildMetrics();
            status = string.Empty;

            int arrowCount = 0;
            int edgeHeadChains = 0;
            int shortEdgeChains = 0;
            int maxLength = 0;
            int straightSegments = 0;
            int totalSegments = 0;

            for (int i = 0; i < placed.Count; i++)
            {
                PlacedChain chain = placed[i];
                var arrow = new AuthoredArrowData
                {
                    indices = new List<int>(chain.HeadToTail.Count),
                    colorIndex = i % 6
                };

                for (int p = 0; p < chain.HeadToTail.Count; p++)
                    arrow.indices.Add(Index(chain.HeadToTail[p], spec.Width));

                authored.arrows.Add(arrow);

                arrowCount += chain.HeadToTail.Count;
                if (IsBoundaryOutHead(spec, chain.HeadToTail[0], chain.OutDir))
                    edgeHeadChains++;
                if (chain.HeadToTail.Count <= 4)
                    shortEdgeChains++;
                maxLength = Mathf.Max(maxLength, chain.HeadToTail.Count);
                CountStraightSegments(chain.HeadToTail, ref straightSegments, ref totalSegments);
            }

            metrics.ArrowCount = arrowCount;
            metrics.Chains = authored.arrows.Count;
            metrics.BlockLinks = blockLinks;
            metrics.EdgeHeadChains = edgeHeadChains;
            metrics.ShortEdgeChains = shortEdgeChains;
            metrics.MaxChainLength = maxLength;
            metrics.AverageChainLength = authored.arrows.Count > 0 ? (float)arrowCount / authored.arrows.Count : 0f;
            metrics.Coverage = (float)arrowCount / Mathf.Max(1, spec.Width * spec.Height);
            metrics.OuterBandCoverage = CountOccupiedOuterBand(spec, occupied) / Mathf.Max(1f, CountOuterBandCells(spec));
            metrics.Straightness = totalSegments > 0 ? (float)straightSegments / totalSegments : 0f;

            int targetCells = Mathf.CeilToInt(spec.Width * spec.Height * spec.TargetCoverage);
            if (arrowCount + 2 < targetCells)
            {
                status = $"coverage below target {arrowCount}/{targetCells} ({metrics.Coverage:0.000}/{spec.TargetCoverage:0.000})";
                return false;
            }

            int outerBandTarget = Mathf.CeilToInt(CountOuterBandCells(spec) * spec.OuterBandTarget);
            int outerBandOccupied = CountOccupiedOuterBand(spec, occupied);
            if (outerBandOccupied + 1 < outerBandTarget)
            {
                status = $"outer band below target {outerBandOccupied}/{outerBandTarget} ({metrics.OuterBandCoverage:0.000}/{spec.OuterBandTarget:0.000})";
                return false;
            }

            return true;
        }

        private static int CountBlockLinksFromPlaced(List<PlacedChain> placed, int width)
        {
            int links = 0;
            for (int i = 0; i < placed.Count; i++)
            {
                for (int j = 0; j < placed.Count; j++)
                {
                    if (i == j)
                        continue;

                    List<Vector2Int> path = placed[j].HeadToTail;
                    bool blocks = false;
                    for (int p = 0; p < path.Count; p++)
                    {
                        if (placed[i].EscapeRay.Contains(Index(path[p], width)))
                        {
                            blocks = true;
                            break;
                        }
                    }

                    if (blocks)
                        links++;
                }
            }

            return links;
        }

        private static void DensifyByTailExtension(
            StyleSpec spec,
            System.Random rng,
            bool[] occupied,
            List<PlacedChain> placed,
            int targetCells,
            bool outerBandOnly)
        {
            if (placed == null || placed.Count == 0)
                return;

            int occupiedCount = outerBandOnly ? CountOccupiedOuterBand(spec, occupied) : CountOccupied(occupied);
            int guard = Mathf.Max(1, spec.Width * spec.Height * 32);
            int maxFinalLength = MaxFinalChainLength(spec);

            while (occupiedCount < targetCells && guard-- > 0)
            {
                if (!TryPickTailExtension(spec, rng, occupied, placed, maxFinalLength, outerBandOnly, out TailExtensionCandidate best))
                    break;

                PlacedChain chain = placed[best.ChainIndex];
                int idx = Index(best.Cell, spec.Width);
                chain.HeadToTail.Add(best.Cell);
                occupied[idx] = true;
                occupiedCount = outerBandOnly ? CountOccupiedOuterBand(spec, occupied) : occupiedCount + 1;
            }
        }

        private static bool TryPickTailExtension(
            StyleSpec spec,
            System.Random rng,
            bool[] occupied,
            List<PlacedChain> placed,
            int maxFinalLength,
            bool outerBandOnly,
            out TailExtensionCandidate best)
        {
            best = null;
            int samples = Mathf.Max(240, placed.Count * 14);

            for (int s = 0; s < samples; s++)
            {
                int chainIndex = rng.Next(0, placed.Count);
                PlacedChain chain = placed[chainIndex];
                if (chain.HeadToTail.Count >= maxFinalLength)
                    continue;

                Vector2Int tail = chain.HeadToTail[chain.HeadToTail.Count - 1];
                Vector2Int previous = chain.HeadToTail.Count >= 2
                    ? chain.HeadToTail[chain.HeadToTail.Count - 2]
                    : tail;
                Vector2Int previousDir = tail - previous;

                for (int i = 0; i < 4; i++)
                {
                    Dir dir = (Dir)i;
                    Vector2Int cell = tail + DirToOffset(dir);
                    if (!IsEmpty(cell, spec.Width, spec.Height, occupied))
                        continue;

                    if (outerBandOnly && !IsOuterBand(spec, cell))
                        continue;

                    int idx = Index(cell, spec.Width);
                    if (chain.EscapeRay.Contains(idx))
                        continue;

                    Vector2Int extensionDir = cell - tail;
                    if (chain.HeadToTail.Count >= 3 && IsLongStraightTail(chain.HeadToTail, extensionDir, maxRun: 4))
                        continue;

                    float score = ScoreTailExtension(spec, rng, occupied, chain, cell, previousDir, extensionDir, outerBandOnly);
                    if (best == null || score > best.Score)
                    {
                        best = new TailExtensionCandidate
                        {
                            ChainIndex = chainIndex,
                            Cell = cell,
                            Score = score
                        };
                    }
                }
            }

            return best != null;
        }

        private static int AddShortEdgePatchChains(
            StyleSpec spec,
            System.Random rng,
            bool[] occupied,
            List<PlacedChain> placed,
            int outerBandTarget)
        {
            int added = 0;
            int guard = Mathf.Max(1, spec.Width * spec.Height * 8);

            while (added < spec.MaxShortEdgePatchChains &&
                   CountOccupiedOuterBand(spec, occupied) < outerBandTarget &&
                   guard-- > 0)
            {
                if (!TryBuildShortEdgePatch(spec, rng, occupied, placed, out PlacedChain patch))
                    break;

                placed.Add(patch);
                for (int i = 0; i < patch.HeadToTail.Count; i++)
                    occupied[Index(patch.HeadToTail[i], spec.Width)] = true;

                added++;
            }

            return added;
        }

        private static bool TryBuildShortEdgePatch(
            StyleSpec spec,
            System.Random rng,
            bool[] occupied,
            List<PlacedChain> placed,
            out PlacedChain patch)
        {
            patch = null;
            int targetLength = rng.Next(4, 7);

            for (int attempt = 0; attempt < 160; attempt++)
            {
                if (!TryPickPatchHead(spec, rng, occupied, requireOuterBand: true, out Vector2Int head, out Dir outDir, out bool edgeHead))
                    continue;

                Vector2Int firstBody = head - DirToOffset(outDir);
                var path = new List<Vector2Int>(targetLength) { head, firstBody };
                var local = new HashSet<int> { Index(head, spec.Width), Index(firstBody, spec.Width) };
                Dir previousDir = Opposite(outDir);

                for (int step = 2; step < targetLength; step++)
                {
                    if (!TryPickOuterPatchNextCell(spec, rng, path, previousDir, occupied, local, out Vector2Int next, out Dir nextDir))
                        break;

                    path.Add(next);
                    local.Add(Index(next, spec.Width));
                    previousDir = nextDir;
                }

                if (path.Count < 3)
                    continue;

                if (PathContains(path, head + DirToOffset(outDir)))
                    continue;

                var candidate = new PlacedChain
                {
                    OutDir = outDir
                };
                candidate.HeadToTail.AddRange(path);
                FillEscapeRay(spec.Width, spec.Height, head, outDir, candidate.EscapeRay);
                patch = candidate;
                return true;
            }

            return false;
        }

        private static int AddShortFillPatchChains(
            StyleSpec spec,
            System.Random rng,
            bool[] occupied,
            List<PlacedChain> placed,
            int targetCells)
        {
            int added = 0;
            int guard = Mathf.Max(1, spec.Width * spec.Height * 8);

            while (added < spec.MaxShortFillPatchChains &&
                   CountOccupied(occupied) < targetCells &&
                   guard-- > 0)
            {
                if (!TryBuildShortFillPatch(spec, rng, occupied, out PlacedChain patch))
                    break;

                placed.Add(patch);
                for (int i = 0; i < patch.HeadToTail.Count; i++)
                    occupied[Index(patch.HeadToTail[i], spec.Width)] = true;

                added++;
            }

            return added;
        }

        private static bool TryBuildShortFillPatch(
            StyleSpec spec,
            System.Random rng,
            bool[] occupied,
            out PlacedChain patch)
        {
            patch = null;
            int targetLength = rng.Next(3, 6);

            for (int attempt = 0; attempt < 160; attempt++)
            {
                if (!TryPickPatchHead(spec, rng, occupied, requireOuterBand: false, out Vector2Int head, out Dir outDir, out bool edgeHead))
                    continue;

                Vector2Int firstBody = head - DirToOffset(outDir);
                var path = new List<Vector2Int>(targetLength) { head, firstBody };
                var local = new HashSet<int> { Index(head, spec.Width), Index(firstBody, spec.Width) };
                Dir previousDir = Opposite(outDir);

                for (int step = 2; step < targetLength; step++)
                {
                    if (!TryPickFillPatchNextCell(spec, rng, path, previousDir, occupied, local, out Vector2Int next, out Dir nextDir))
                        break;

                    path.Add(next);
                    local.Add(Index(next, spec.Width));
                    previousDir = nextDir;
                }

                if (path.Count < 3)
                    continue;

                if (PathContains(path, head + DirToOffset(outDir)))
                    continue;

                var candidate = new PlacedChain
                {
                    OutDir = outDir
                };
                candidate.HeadToTail.AddRange(path);
                FillEscapeRay(spec.Width, spec.Height, head, outDir, candidate.EscapeRay);
                patch = candidate;
                return true;
            }

            return false;
        }

        private static bool CanUsePatchHead(
            StyleSpec spec,
            Vector2Int head,
            Dir outDir,
            bool[] occupied,
            bool edgeHead)
        {
            if (!IsEmpty(head, spec.Width, spec.Height, occupied))
                return false;

            Vector2Int firstBody = head - DirToOffset(outDir);
            if (!IsEmpty(firstBody, spec.Width, spec.Height, occupied))
                return false;

            return edgeHead || !IsBoundaryOutHead(spec, head, outDir);
        }

        private static bool TryPickPatchHead(
            StyleSpec spec,
            System.Random rng,
            bool[] occupied,
            bool requireOuterBand,
            out Vector2Int head,
            out Dir outDir,
            out bool edgeHead)
        {
            head = default;
            outDir = default;
            edgeHead = false;
            bool found = false;
            float bestScore = float.NegativeInfinity;

            int samples = requireOuterBand ? 220 : 260;
            for (int s = 0; s < samples; s++)
            {
                var cell = new Vector2Int(rng.Next(0, spec.Width), rng.Next(0, spec.Height));
                TryScorePatchHead(spec, rng, occupied, requireOuterBand, cell, ref found, ref bestScore, ref head, ref outDir, ref edgeHead);
            }

            if (found)
                return true;

            for (int y = 0; y < spec.Height; y++)
            {
                for (int x = 0; x < spec.Width; x++)
                {
                    var cell = new Vector2Int(x, y);
                    TryScorePatchHead(spec, rng, occupied, requireOuterBand, cell, ref found, ref bestScore, ref head, ref outDir, ref edgeHead);
                }
            }

            return found;
        }

        private static void TryScorePatchHead(
            StyleSpec spec,
            System.Random rng,
            bool[] occupied,
            bool requireOuterBand,
            Vector2Int cell,
            ref bool found,
            ref float bestScore,
            ref Vector2Int bestHead,
            ref Dir bestOutDir,
            ref bool bestEdgeHead)
        {
            if (!IsEmpty(cell, spec.Width, spec.Height, occupied))
                return;

            if (requireOuterBand && !IsOuterBand(spec, cell))
                return;

            for (int i = 0; i < 4; i++)
            {
                Dir dir = (Dir)i;
                bool edge = IsBoundaryOutHead(spec, cell, dir);
                if (!CanUsePatchHead(spec, cell, dir, occupied, edge))
                    continue;

                float score = 0f;
                score += CountOccupiedNeighbors(spec, cell, occupied) * 1.2f;
                score += IsOuterBand(spec, cell) ? 1.0f : 0f;
                score += edge ? 0.35f : 0f;
                score += (float)rng.NextDouble() * 0.45f;

                if (!found || score > bestScore)
                {
                    found = true;
                    bestScore = score;
                    bestHead = cell;
                    bestOutDir = dir;
                    bestEdgeHead = edge;
                }
            }
        }

        private static bool TryPickFillPatchNextCell(
            StyleSpec spec,
            System.Random rng,
            List<Vector2Int> path,
            Dir previousDir,
            bool[] occupied,
            HashSet<int> local,
            out Vector2Int next,
            out Dir nextDir)
        {
            next = default;
            nextDir = default;
            Vector2Int current = path[path.Count - 1];
            var choices = new List<(Vector2Int Cell, Dir Dir, float Score)>(4);

            for (int i = 0; i < 4; i++)
            {
                Dir dir = (Dir)i;
                if (dir == Opposite(previousDir))
                    continue;

                Vector2Int cell = current + DirToOffset(dir);
                if (!IsEmpty(cell, spec.Width, spec.Height, occupied))
                    continue;

                int idx = Index(cell, spec.Width);
                if (local.Contains(idx))
                    continue;

                float score = 0f;
                score += CountOccupiedNeighbors(spec, cell, occupied) * 0.8f;
                score += IsOuterBand(spec, cell) ? 0.7f : 0.5f;
                score += EdgeDistanceScore(spec, cell) * 0.25f;
                score += dir != previousDir ? spec.TurnBias * 0.45f : 0.12f;
                score += (float)rng.NextDouble() * 0.35f;
                choices.Add((cell, dir, score));
            }

            if (choices.Count == 0)
                return false;

            choices.Sort((a, b) => b.Score.CompareTo(a.Score));
            int pickLimit = Mathf.Min(choices.Count, 2);
            int pick = rng.Next(0, pickLimit);
            next = choices[pick].Cell;
            nextDir = choices[pick].Dir;
            return true;
        }

        private static bool TryPickOuterPatchNextCell(
            StyleSpec spec,
            System.Random rng,
            List<Vector2Int> path,
            Dir previousDir,
            bool[] occupied,
            HashSet<int> local,
            out Vector2Int next,
            out Dir nextDir)
        {
            next = default;
            nextDir = default;
            Vector2Int current = path[path.Count - 1];
            var choices = new List<(Vector2Int Cell, Dir Dir, float Score)>(4);

            for (int i = 0; i < 4; i++)
            {
                Dir dir = (Dir)i;
                if (dir == Opposite(previousDir))
                    continue;

                Vector2Int cell = current + DirToOffset(dir);
                if (!IsEmpty(cell, spec.Width, spec.Height, occupied))
                    continue;

                int idx = Index(cell, spec.Width);
                if (local.Contains(idx))
                    continue;

                float score = 0f;
                score += IsOuterBand(spec, cell) ? 2.4f : 0.3f;
                score += CountOccupiedNeighbors(spec, cell, occupied) * 0.6f;
                score += dir != previousDir ? spec.TurnBias * 0.5f : 0.1f;
                score += (float)rng.NextDouble() * 0.35f;
                choices.Add((cell, dir, score));
            }

            if (choices.Count == 0)
                return false;

            choices.Sort((a, b) => b.Score.CompareTo(a.Score));
            int pickLimit = Mathf.Min(choices.Count, 2);
            int pick = rng.Next(0, pickLimit);
            next = choices[pick].Cell;
            nextDir = choices[pick].Dir;
            return true;
        }

        private static float ScoreTailExtension(
            StyleSpec spec,
            System.Random rng,
            bool[] occupied,
            PlacedChain chain,
            Vector2Int cell,
            Vector2Int previousDir,
            Vector2Int extensionDir,
            bool outerBandOnly)
        {
            float score = 0f;
            score += CountOccupiedNeighbors(spec, cell, occupied) * 0.65f;
            score += EdgeDistanceScore(spec, cell) * (outerBandOnly ? 2.8f : 1.15f);
            score += IsOuterBand(spec, cell) ? 1.2f : 0f;
            score += Radial(spec, cell) > 0.75f ? 0.55f : 0f;
            score += extensionDir != previousDir ? spec.TurnBias * 0.65f : (1f - spec.TurnBias) * 0.25f;
            score += ScoreStylePath(spec, 0.85f, chain.HeadToTail, chain.OutDir) * 0.08f;
            score += (float)rng.NextDouble() * 0.35f;
            return score;
        }

        private static int CountOccupied(bool[] occupied)
        {
            int count = 0;
            for (int i = 0; i < occupied.Length; i++)
            {
                if (occupied[i])
                    count++;
            }

            return count;
        }

        private static int CountOccupiedOuterBand(StyleSpec spec, bool[] occupied)
        {
            int count = 0;
            for (int y = 0; y < spec.Height; y++)
            {
                for (int x = 0; x < spec.Width; x++)
                {
                    var cell = new Vector2Int(x, y);
                    if (IsOuterBand(spec, cell) && occupied[Index(cell, spec.Width)])
                        count++;
                }
            }

            return count;
        }

        private static int CountOuterBandCells(StyleSpec spec)
        {
            int count = 0;
            for (int y = 0; y < spec.Height; y++)
            {
                for (int x = 0; x < spec.Width; x++)
                {
                    if (IsOuterBand(spec, new Vector2Int(x, y)))
                        count++;
                }
            }

            return count;
        }

        private static bool IsOuterBand(StyleSpec spec, Vector2Int cell)
        {
            return cell.x <= 1 ||
                   cell.y <= 1 ||
                   cell.x >= spec.Width - 2 ||
                   cell.y >= spec.Height - 2;
        }

        private static bool IsBoundaryOutHead(StyleSpec spec, Vector2Int head, Dir outDir)
        {
            return (outDir == Dir.Up && head.y == spec.Height - 1) ||
                   (outDir == Dir.Down && head.y == 0) ||
                   (outDir == Dir.Right && head.x == spec.Width - 1) ||
                   (outDir == Dir.Left && head.x == 0);
        }

        private static int CountOccupiedNeighbors(StyleSpec spec, Vector2Int cell, bool[] occupied)
        {
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                Vector2Int n = cell + DirToOffset((Dir)i);
                if ((uint)n.x < (uint)spec.Width && (uint)n.y < (uint)spec.Height && occupied[Index(n, spec.Width)])
                    count++;
            }

            return count;
        }

        private static bool IsLongStraightTail(List<Vector2Int> headToTail, Vector2Int extensionDir, int maxRun)
        {
            int run = 1;
            Vector2Int lastDir = extensionDir;
            for (int i = headToTail.Count - 1; i > 0 && run <= maxRun; i--)
            {
                Vector2Int dir = headToTail[i] - headToTail[i - 1];
                if (dir != lastDir)
                    break;

                run++;
            }

            return run > maxRun;
        }

        private static int MaxFinalChainLength(StyleSpec spec)
        {
            return spec.Type switch
            {
                NoMaskType.MazeLongChain => spec.MaxLength + 8,
                NoMaskType.LockBuckle => spec.MaxLength + 6,
                NoMaskType.CoreBurst => spec.MaxLength + 6,
                NoMaskType.OuterShell => spec.MaxLength + 6,
                NoMaskType.Sweep => spec.MaxLength + 6,
                _ => spec.MaxLength + 5
            };
        }

        private static bool TryPickCandidate(
            StyleSpec spec,
            System.Random rng,
            int chainIndex,
            bool[] occupied,
            List<PlacedChain> placed,
            out ChainCandidate best)
        {
            best = null;
            bool virtualSource = spec.Id != null && spec.Id.EndsWith("_virtual_source", StringComparison.Ordinal);
            int minSamples = virtualSource ? 80 : 520;
            int maxSamples = virtualSource ? 240 : 1400;
            int samples = Mathf.RoundToInt(Mathf.Lerp(minSamples, maxSamples, (float)chainIndex / Mathf.Max(1, spec.TargetChains - 1)));

            for (int i = 0; i < samples; i++)
            {
                int targetLength = PickTargetLength(spec, rng, chainIndex);
                if (!TryBuildCandidatePath(spec, rng, chainIndex, targetLength, occupied, placed, out ChainCandidate candidate))
                    continue;

                if (best == null || candidate.Score > best.Score)
                    best = candidate;
            }

            return best != null;
        }

        private static int PickTargetLength(StyleSpec spec, System.Random rng, int chainIndex)
        {
            float phase = (float)chainIndex / Mathf.Max(1, spec.TargetChains - 1);
            int min = spec.MinLength;
            int max = spec.MaxLength;

            if (spec.Type == NoMaskType.MazeLongChain)
            {
                min += phase > 0.7f ? -1 : 0;
                max += phase < 0.45f ? 2 : 0;
            }
            else if (spec.Type == NoMaskType.DenseWeave)
            {
                max = Mathf.Max(min, max - (phase > 0.65f ? 1 : 0));
            }
            else if (spec.Type == NoMaskType.Sweep)
            {
                max = Mathf.Max(min, max - (phase > 0.75f ? 1 : 0));
            }

            min = Mathf.Clamp(min, 3, max);
            return rng.Next(min, max + 1);
        }

        private static bool TryBuildCandidatePath(
            StyleSpec spec,
            System.Random rng,
            int chainIndex,
            int targetLength,
            bool[] occupied,
            List<PlacedChain> placed,
            out ChainCandidate candidate)
        {
            candidate = null;

            if (!TryPickCandidateHead(spec, rng, chainIndex, occupied, out Vector2Int head, out Dir outDir))
                return false;

            Vector2Int firstBody = head - DirToOffset(outDir);
            var path = new List<Vector2Int>(targetLength) { head, firstBody };
            var local = new HashSet<int> { Index(head, spec.Width), Index(firstBody, spec.Width) };
            Dir previousHeadToTailDir = Opposite(outDir);

            for (int step = 2; step < targetLength; step++)
            {
                if (!TryPickNextPathCell(spec, rng, chainIndex, targetLength, path, previousHeadToTailDir, occupied, local, out Vector2Int next, out Dir nextDir))
                    break;

                path.Add(next);
                local.Add(Index(next, spec.Width));
                previousHeadToTailDir = nextDir;
            }

            if (path.Count < Mathf.Min(targetLength, spec.MinLength))
                return false;

            if (PathContains(path, head + DirToOffset(outDir)))
                return false;

            int turns = CountTurns(path);
            if (path.Count >= 6 && turns == 0)
                return false;

            bool virtualSource = spec.Id != null && spec.Id.EndsWith("_virtual_source", StringComparison.Ordinal);
            int blocks = virtualSource ? 0 : CountBlockedExisting(path, placed, spec.Width);
            float score = ScoreCandidate(spec, chainIndex, path, turns, blocks, outDir);

            candidate = new ChainCandidate
            {
                OutDir = outDir,
                BlocksExisting = blocks,
                Turns = turns,
                Score = score
            };
            candidate.HeadToTail.AddRange(path);
            return true;
        }

        private static bool TryPickCandidateHead(
            StyleSpec spec,
            System.Random rng,
            int chainIndex,
            bool[] occupied,
            out Vector2Int head,
            out Dir outDir)
        {
            head = default;
            outDir = default;
            float edgeBias = EdgeHeadBias(spec, chainIndex);

            for (int attempt = 0; attempt < 96; attempt++)
            {
                bool edgeHead = rng.NextDouble() < edgeBias;
                outDir = PickOutDir(spec, rng, chainIndex);
                head = edgeHead
                    ? RandomBoundaryHead(spec, rng, outDir)
                    : new Vector2Int(rng.Next(0, spec.Width), rng.Next(0, spec.Height));

                bool allowBlockedRay = !edgeHead && rng.NextDouble() < BlockedHeadBias(spec, chainIndex);
                if (CanUseHead(spec, head, outDir, occupied, edgeHead, allowBlockedRay))
                    return true;
            }

            for (int d = 0; d < 4; d++)
            {
                outDir = (Dir)d;
                int count = BoundaryCount(spec, outDir);
                int start = rng.Next(0, Mathf.Max(1, count));
                for (int i = 0; i < count; i++)
                {
                    head = BoundaryHeadAt(spec, outDir, (start + i) % count);
                    if (CanUseHead(spec, head, outDir, occupied, edgeHead: true, allowBlockedRay: false))
                        return true;
                }
            }

            return false;
        }

        private static bool CanUseHead(
            StyleSpec spec,
            Vector2Int head,
            Dir outDir,
            bool[] occupied,
            bool edgeHead,
            bool allowBlockedRay)
        {
            if (!IsEmpty(head, spec.Width, spec.Height, occupied))
                return false;

            Vector2Int firstBody = head - DirToOffset(outDir);
            if (!IsEmpty(firstBody, spec.Width, spec.Height, occupied))
                return false;

            if (edgeHead)
                return true;

            if (allowBlockedRay && !IsBoundaryOutHead(spec, head, outDir))
                return true;

            return RayClear(spec.Width, spec.Height, head, outDir, occupied);
        }

        private static float BlockedHeadBias(StyleSpec spec, int chainIndex)
        {
            float phase = (float)chainIndex / Mathf.Max(1, spec.TargetChains - 1);
            float bias = Mathf.Lerp(0.08f, 0.24f, phase);
            if (spec.Type == NoMaskType.LockBuckle || spec.Type == NoMaskType.CoreBurst)
                bias += 0.08f;
            if (spec.Type == NoMaskType.MazeLongChain || spec.Type == NoMaskType.DenseWeave)
                bias += 0.04f;
            if (spec.Type == NoMaskType.Sweep)
                bias -= 0.04f;
            return Mathf.Clamp(bias, 0.04f, 0.34f);
        }

        private static float EdgeHeadBias(StyleSpec spec, int chainIndex)
        {
            float phase = (float)chainIndex / Mathf.Max(1, spec.TargetChains - 1);
            float bias = Mathf.Lerp(spec.EdgeOpeningBias + 0.12f, spec.EdgeOpeningBias - 0.04f, phase);
            if (spec.Type == NoMaskType.DenseWeave)
                bias -= 0.04f;
            if (spec.Type == NoMaskType.MazeLongChain)
                bias -= 0.05f;
            if (spec.Type == NoMaskType.OuterShell || spec.Type == NoMaskType.Sweep)
                bias += 0.03f;
            return Mathf.Clamp(bias, 0.18f, 0.56f);
        }

        private static Vector2Int RandomBoundaryHead(StyleSpec spec, System.Random rng, Dir outDir)
        {
            int count = BoundaryCount(spec, outDir);
            return BoundaryHeadAt(spec, outDir, rng.Next(0, Mathf.Max(1, count)));
        }

        private static int BoundaryCount(StyleSpec spec, Dir outDir)
        {
            return outDir == Dir.Up || outDir == Dir.Down ? spec.Width : spec.Height;
        }

        private static Vector2Int BoundaryHeadAt(StyleSpec spec, Dir outDir, int offset)
        {
            return outDir switch
            {
                Dir.Up => new Vector2Int(Mathf.Clamp(offset, 0, spec.Width - 1), spec.Height - 1),
                Dir.Down => new Vector2Int(Mathf.Clamp(offset, 0, spec.Width - 1), 0),
                Dir.Right => new Vector2Int(spec.Width - 1, Mathf.Clamp(offset, 0, spec.Height - 1)),
                _ => new Vector2Int(0, Mathf.Clamp(offset, 0, spec.Height - 1))
            };
        }

        private static bool TryPickNextPathCell(
            StyleSpec spec,
            System.Random rng,
            int chainIndex,
            int targetLength,
            List<Vector2Int> path,
            Dir previousHeadToTailDir,
            bool[] occupied,
            HashSet<int> local,
            out Vector2Int next,
            out Dir nextDir)
        {
            next = default;
            nextDir = default;
            Vector2Int current = path[path.Count - 1];
            var choices = new List<(Vector2Int Cell, Dir Dir, float Score)>(4);

            for (int i = 0; i < 4; i++)
            {
                Dir dir = (Dir)i;
                if (dir == Opposite(previousHeadToTailDir))
                    continue;

                Vector2Int cell = current + DirToOffset(dir);
                if (!IsEmpty(cell, spec.Width, spec.Height, occupied))
                    continue;

                int idx = Index(cell, spec.Width);
                if (local.Contains(idx))
                    continue;

                float score = ScorePathStep(spec, chainIndex, targetLength, path.Count, cell, dir, previousHeadToTailDir);
                score += (float)rng.NextDouble() * 0.45f;
                choices.Add((cell, dir, score));
            }

            if (choices.Count == 0)
                return false;

            choices.Sort((a, b) => b.Score.CompareTo(a.Score));
            int pickLimit = Mathf.Min(choices.Count, 2 + (rng.NextDouble() < spec.TurnBias ? 1 : 0));
            int pick = rng.Next(0, pickLimit);
            next = choices[pick].Cell;
            nextDir = choices[pick].Dir;
            return true;
        }

        private static Dir PickOutDir(StyleSpec spec, System.Random rng, int chainIndex)
        {
            float phase = (float)chainIndex / Mathf.Max(1, spec.TargetChains - 1);

            if (spec.Type == NoMaskType.Sweep)
            {
                if (phase < 0.35f)
                    return rng.NextDouble() < 0.65 ? Dir.Up : Dir.Right;
                if (phase > 0.70f)
                    return rng.NextDouble() < 0.65 ? Dir.Down : Dir.Left;
            }

            if (spec.Type == NoMaskType.DualZone)
            {
                if ((chainIndex & 1) == 0)
                    return rng.NextDouble() < 0.72 ? Dir.Left : RandomVerticalDir(rng);
                return rng.NextDouble() < 0.72 ? Dir.Right : RandomVerticalDir(rng);
            }

            if (spec.Type == NoMaskType.StairPush)
            {
                double r = rng.NextDouble();
                if (r < 0.38) return Dir.Up;
                if (r < 0.72) return Dir.Right;
            }

            if (rng.NextDouble() < spec.EdgeOpeningBias)
            {
                double r = rng.NextDouble();
                if (r < 0.25) return Dir.Up;
                if (r < 0.50) return Dir.Right;
                if (r < 0.75) return Dir.Down;
                return Dir.Left;
            }

            return (Dir)rng.Next(0, 4);
        }

        private static float ScoreCandidate(
            StyleSpec spec,
            int chainIndex,
            List<Vector2Int> path,
            int turns,
            int blocks,
            Dir outDir)
        {
            float phase = (float)chainIndex / Mathf.Max(1, spec.TargetChains - 1);
            float radial = AverageRadial(spec, path);
            float desiredRadial = DesiredRadial(spec, phase);
            float zone = 1f - Mathf.Clamp01(Mathf.Abs(radial - desiredRadial) * 2.4f);

            float turnRatio = path.Count > 2 ? (float)turns / (path.Count - 2) : 0f;
            float turnTarget = Mathf.Lerp(0.22f, 0.58f, spec.TurnBias);
            float turnScore = 1f - Mathf.Clamp01(Mathf.Abs(turnRatio - turnTarget) * 2.2f);

            float blockScore = blocks * spec.BlockWeight * Mathf.Lerp(0.6f, 1.4f, phase);
            float lengthScore = Mathf.InverseLerp(spec.MinLength, spec.MaxLength + 1, path.Count);
            float styleScore = ScoreStylePath(spec, phase, path, outDir);
            float straightPenalty = path.Count >= 7 && turns <= 1 ? 2.5f : 0f;

            return zone * 2.2f + turnScore * 1.8f + blockScore + lengthScore * 0.8f + styleScore - straightPenalty;
        }

        private static float ScoreStylePath(StyleSpec spec, float phase, List<Vector2Int> path, Dir outDir)
        {
            Vector2 center = new Vector2((spec.Width - 1) * 0.5f, (spec.Height - 1) * 0.5f);
            Vector2 avg = AveragePosition(path);
            float score = 0f;

            switch (spec.Type)
            {
                case NoMaskType.SectionUnlock:
                {
                    float targetX = phase < 0.34f ? center.x : phase < 0.67f ? spec.Width * 0.25f : spec.Width * 0.75f;
                    score += 1.5f * (1f - Mathf.Clamp01(Mathf.Abs(avg.x - targetX) / Mathf.Max(1f, spec.Width * 0.45f)));
                    break;
                }
                case NoMaskType.LockBuckle:
                    score += 1.2f * CenterBandScore(spec, avg);
                    break;
                case NoMaskType.Sweep:
                {
                    float targetY = Mathf.Lerp(spec.Height * 0.82f, spec.Height * 0.18f, phase);
                    score += 1.6f * (1f - Mathf.Clamp01(Mathf.Abs(avg.y - targetY) / Mathf.Max(1f, spec.Height * 0.35f)));
                    break;
                }
                case NoMaskType.CoreBurst:
                    score += phase < 0.55f ? 1.4f * CenterBandScore(spec, avg) : 1.2f * EdgeBandScore(spec, avg);
                    break;
                case NoMaskType.DualZone:
                    score += Mathf.Abs(avg.x - center.x) > spec.Width * 0.18f ? 1.2f : -0.4f;
                    break;
                case NoMaskType.StairPush:
                {
                    float normalizedX = spec.Width > 1 ? avg.x / (spec.Width - 1) : 0f;
                    float normalizedY = spec.Height > 1 ? avg.y / (spec.Height - 1) : 0f;
                    score += 1.4f * (1f - Mathf.Clamp01(Mathf.Abs(normalizedY - normalizedX) * 1.4f));
                    break;
                }
                case NoMaskType.QuasiSymmetry:
                    score += 1.2f * (1f - Mathf.Clamp01(Mathf.Abs(avg.x - center.x) / Mathf.Max(1f, spec.Width * 0.45f)));
                    break;
                case NoMaskType.MazeLongChain:
                    score += path.Count >= spec.MaxLength - 1 ? 1.2f : 0f;
                    break;
            }

            if (outDir == Dir.Up || outDir == Dir.Down)
                score += spec.Height > spec.Width ? 0.2f : 0f;

            return score;
        }

        private static float ScorePathStep(
            StyleSpec spec,
            int chainIndex,
            int targetLength,
            int step,
            Vector2Int cell,
            Dir dir,
            Dir previousDir)
        {
            float phase = (float)chainIndex / Mathf.Max(1, spec.TargetChains - 1);
            float radial = Radial(spec, cell);
            float score = 0f;

            score += 1f - Mathf.Clamp01(Mathf.Abs(radial - DesiredRadial(spec, phase)) * 1.8f);

            if (dir != previousDir)
                score += spec.TurnBias;
            else
                score += 1f - spec.TurnBias;

            switch (spec.Type)
            {
                case NoMaskType.MazeLongChain:
                    score += dir != previousDir ? 0.6f : 0.1f;
                    break;
                case NoMaskType.Sweep:
                {
                    float targetY = Mathf.Lerp(spec.Height * 0.82f, spec.Height * 0.18f, phase);
                    score += 1f - Mathf.Clamp01(Mathf.Abs(cell.y - targetY) / Mathf.Max(1f, spec.Height * 0.4f));
                    break;
                }
                case NoMaskType.StairPush:
                    score += (dir == Dir.Right || dir == Dir.Up) ? 0.35f : 0f;
                    break;
                case NoMaskType.QuasiSymmetry:
                {
                    float centerX = (spec.Width - 1) * 0.5f;
                    score += 0.45f * (1f - Mathf.Clamp01(Mathf.Abs(cell.x - centerX) / Mathf.Max(1f, centerX)));
                    break;
                }
            }

            if (step >= targetLength - 2)
                score += radial < 0.18f ? -0.5f : 0.15f;

            return score;
        }

        private static float DesiredRadial(StyleSpec spec, float phase)
        {
            return spec.Type switch
            {
                NoMaskType.OuterShell => Mathf.Lerp(0.20f, 0.92f, phase),
                NoMaskType.CoreBurst => phase < 0.55f ? 0.22f : Mathf.Lerp(0.65f, 0.95f, phase),
                NoMaskType.LockBuckle => Mathf.Lerp(0.32f, 0.78f, phase),
                NoMaskType.Sweep => 0.58f,
                NoMaskType.DenseWeave => 0.55f,
                NoMaskType.MazeLongChain => 0.48f,
                NoMaskType.QuasiSymmetry => 0.46f,
                _ => Mathf.Lerp(0.35f, 0.82f, phase)
            };
        }

        private static int CountBlockedExisting(List<Vector2Int> path, List<PlacedChain> placed, int width)
        {
            if (placed.Count == 0)
                return 0;

            var pathCells = new HashSet<int>();
            for (int i = 0; i < path.Count; i++)
                pathCells.Add(Index(path[i], width));

            int blocked = 0;
            for (int i = 0; i < placed.Count; i++)
            {
                foreach (int idx in placed[i].EscapeRay)
                {
                    if (pathCells.Contains(idx))
                    {
                        blocked++;
                        break;
                    }
                }
            }

            return blocked;
        }

        private static int CountInitialMovableChains(BoardState board, AuthoredLevelData authored)
        {
            var ruleset = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            int movable = 0;
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var arrow = authored.arrows[i];
                if (arrow.indices == null || arrow.indices.Count == 0)
                    continue;

                BoardState clone = CloneBoard(board);
                Vector2Int head = Pos(arrow.indices[0], authored.width);
                if (ruleset.TryApplyMove(clone, new Move(head), out MoveDelta delta) && delta.changes.Count > 0)
                    movable++;
            }

            return movable;
        }

        private static BoardState CloneBoard(BoardState src)
        {
            var clone = new BoardState(src.width, src.height);
            Array.Copy(src.tiles, clone.tiles, src.tiles.Length);
            return clone;
        }

        private static void AppendLevelsFromFolder(List<LevelDefinition> levels, string folder)
        {
            if (levels == null || string.IsNullOrWhiteSpace(folder) || !AssetDatabase.IsValidFolder(folder))
                return;

            string[] guids = AssetDatabase.FindAssets("t:LevelDefinition", new[] { folder });
            var paths = new List<string>(guids.Length);
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                if (!string.IsNullOrWhiteSpace(path))
                    paths.Add(path);
            }

            paths.Sort(StringComparer.Ordinal);
            for (int i = 0; i < paths.Count; i++)
            {
                LevelDefinition level = AssetDatabase.LoadAssetAtPath<LevelDefinition>(paths[i]);
                if (level != null)
                    levels.Add(level);
            }
        }

        private static LevelPack SavePack(List<LevelDefinition> levels)
        {
            return SavePackAt(levels, PackPath, "no_mask_type_preview", $"No Mask Type Preview ({levels.Count})");
        }

        private static LevelPack SavePackAt(List<LevelDefinition> levels, string packPath, string packId, string displayName)
        {
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

            AssetDatabase.ImportAsset(packPath);
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
            SerializedProperty activePack = so.FindProperty("activePack");
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

        private static void EnsureFolderExists(string folder)
        {
            if (string.IsNullOrWhiteSpace(folder) || AssetDatabase.IsValidFolder(folder))
                return;

            string[] parts = folder.Replace("\\", "/").Split('/');
            string current = parts[0];
            for (int i = 1; i < parts.Length; i++)
            {
                string next = $"{current}/{parts[i]}";
                if (!AssetDatabase.IsValidFolder(next))
                    AssetDatabase.CreateFolder(current, parts[i]);
                current = next;
            }
        }

        private static bool IsEmpty(Vector2Int cell, int width, int height, bool[] occupied)
        {
            if ((uint)cell.x >= (uint)width || (uint)cell.y >= (uint)height)
                return false;

            return !occupied[Index(cell, width)];
        }

        private static bool RayClear(int width, int height, Vector2Int head, Dir outDir, bool[] occupied)
        {
            Vector2Int step = DirToOffset(outDir);
            Vector2Int p = head + step;
            while ((uint)p.x < (uint)width && (uint)p.y < (uint)height)
            {
                if (occupied[Index(p, width)])
                    return false;
                p += step;
            }

            return true;
        }

        private static void FillEscapeRay(int width, int height, Vector2Int head, Dir outDir, HashSet<int> ray)
        {
            ray.Clear();
            Vector2Int step = DirToOffset(outDir);
            Vector2Int p = head + step;
            while ((uint)p.x < (uint)width && (uint)p.y < (uint)height)
            {
                ray.Add(Index(p, width));
                p += step;
            }
        }

        private static void CountStraightSegments(List<Vector2Int> path, ref int straightSegments, ref int totalSegments)
        {
            if (path == null || path.Count < 3)
                return;

            for (int i = 2; i < path.Count; i++)
            {
                Vector2Int a = path[i - 1] - path[i - 2];
                Vector2Int b = path[i] - path[i - 1];
                if (a == b)
                    straightSegments++;
                totalSegments++;
            }
        }

        private static int CountTurns(List<Vector2Int> path)
        {
            int turns = 0;
            if (path == null || path.Count < 3)
                return turns;

            for (int i = 2; i < path.Count; i++)
            {
                Vector2Int a = path[i - 1] - path[i - 2];
                Vector2Int b = path[i] - path[i - 1];
                if (a != b)
                    turns++;
            }

            return turns;
        }

        private static bool PathContains(List<Vector2Int> path, Vector2Int cell)
        {
            if (path == null)
                return false;

            for (int i = 0; i < path.Count; i++)
            {
                if (path[i] == cell)
                    return true;
            }

            return false;
        }

        private static Vector2 AveragePosition(List<Vector2Int> path)
        {
            Vector2 sum = Vector2.zero;
            for (int i = 0; i < path.Count; i++)
                sum += path[i];
            return path.Count > 0 ? sum / path.Count : Vector2.zero;
        }

        private static float AverageRadial(StyleSpec spec, List<Vector2Int> path)
        {
            if (path == null || path.Count == 0)
                return 0f;

            float sum = 0f;
            for (int i = 0; i < path.Count; i++)
                sum += Radial(spec, path[i]);
            return sum / path.Count;
        }

        private static float Radial(StyleSpec spec, Vector2Int cell)
        {
            float cx = (spec.Width - 1) * 0.5f;
            float cy = (spec.Height - 1) * 0.5f;
            float dx = Mathf.Abs(cell.x - cx) / Mathf.Max(1f, cx);
            float dy = Mathf.Abs(cell.y - cy) / Mathf.Max(1f, cy);
            return Mathf.Clamp01(Mathf.Max(dx, dy));
        }

        private static float CenterBandScore(StyleSpec spec, Vector2 p)
        {
            float cx = (spec.Width - 1) * 0.5f;
            float cy = (spec.Height - 1) * 0.5f;
            float dx = Mathf.Abs(p.x - cx) / Mathf.Max(1f, cx);
            float dy = Mathf.Abs(p.y - cy) / Mathf.Max(1f, cy);
            return 1f - Mathf.Clamp01(Mathf.Max(dx, dy));
        }

        private static float EdgeBandScore(StyleSpec spec, Vector2 p)
        {
            float left = p.x / Mathf.Max(1f, spec.Width - 1);
            float right = 1f - left;
            float down = p.y / Mathf.Max(1f, spec.Height - 1);
            float up = 1f - down;
            return 1f - Mathf.Min(Mathf.Min(left, right), Mathf.Min(down, up)) * 2f;
        }

        private static float EdgeDistanceScore(StyleSpec spec, Vector2Int p)
        {
            int left = p.x;
            int right = (spec.Width - 1) - p.x;
            int down = p.y;
            int up = (spec.Height - 1) - p.y;
            int min = Mathf.Min(Mathf.Min(left, right), Mathf.Min(down, up));
            return 1f - Mathf.Clamp01(min / Mathf.Max(1f, Mathf.Min(spec.Width, spec.Height) * 0.35f));
        }

        private static int Index(Vector2Int p, int width) => p.x + p.y * width;

        private static Vector2Int Pos(int index, int width) => new Vector2Int(index % width, index / width);

        private static Vector2Int DirToOffset(Dir d) => d switch
        {
            Dir.Up => new Vector2Int(0, 1),
            Dir.Right => new Vector2Int(1, 0),
            Dir.Down => new Vector2Int(0, -1),
            _ => new Vector2Int(-1, 0)
        };

        private static Dir Opposite(Dir d) => (Dir)(((int)d + 2) & 3);

        private static Dir RandomVerticalDir(System.Random rng) => rng.NextDouble() < 0.5 ? Dir.Up : Dir.Down;

        private static string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            if (value.IndexOfAny(new[] { ',', '"', '\n', '\r' }) < 0)
                return value;

            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        private static string JoinMessages(List<string> messages, int maxCount)
        {
            if (messages == null || messages.Count == 0)
                return "";

            int count = Mathf.Min(Mathf.Max(1, maxCount), messages.Count);
            var parts = new List<string>(count + 1);
            for (int i = 0; i < count; i++)
                parts.Add(messages[i]);
            if (messages.Count > count)
                parts.Add($"more={messages.Count - count}");
            return string.Join(" | ", parts);
        }

        private static string JoinInts(List<int> values, int maxCount)
        {
            if (values == null || values.Count == 0)
                return "";

            int count = Mathf.Min(Mathf.Max(1, maxCount), values.Count);
            var parts = new List<string>(count + 1);
            for (int i = 0; i < count; i++)
                parts.Add(values[i].ToString());
            if (values.Count > count)
                parts.Add($"more={values.Count - count}");
            return string.Join(" ", parts);
        }
    }
}
#endif
