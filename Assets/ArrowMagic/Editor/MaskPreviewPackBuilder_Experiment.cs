#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PixelBug.ArrowMagic.EditorTools
{
    public static class MaskPreviewPackBuilder
    {
        const string OutputFolder = "Assets/ArrowMagic/SOData/Levels/Generated/MaskPreview";
        const string PreviewFolder = "Assets/ArrowMagic/SOData/Levels/Generated/MaskPreview/previews";
        const string PackPath = "Assets/ArrowMagic/SOData/Packs/MaskPreviewPack.asset";
        const string HoleMaskProjectPath = "Assets/ArrowMagic/Masks/hole.png";
        const string HoleMaskExternalPath = "F:/360MoveData/Users/admin/Pictures/hole.png";
        const string HoleV05Top5Folder = OutputFolder + "/HoleV05Top5";
        const string HoleV05Top5PackPath = "Assets/ArrowMagic/SOData/Packs/HoleV05Top5DemoPack.asset";
        const string HoleV06Top5Folder = OutputFolder + "/HoleV06Top5";
        const string HoleV06Top5PackPath = "Assets/ArrowMagic/SOData/Packs/HoleV06Top5DemoPack.asset";
        const string HoleV07Top5Folder = OutputFolder + "/HoleV07Top5";
        const string HoleV07Top5PackPath = "Assets/ArrowMagic/SOData/Packs/HoleV07Top5DemoPack.asset";
        const string HoleV08Top5Folder = OutputFolder + "/HoleV08Top5";
        const string HoleV08Top5PackPath = "Assets/ArrowMagic/SOData/Packs/HoleV08Top5DemoPack.asset";
        const string HoleV09Top5Folder = OutputFolder + "/HoleV09Top5";
        const string HoleV09Top5PackPath = "Assets/ArrowMagic/SOData/Packs/HoleV09Top5DemoPack.asset";
        const string HoleV10Top5Folder = OutputFolder + "/HoleV10Top5";
        const string HoleV10Top5PackPath = "Assets/ArrowMagic/SOData/Packs/HoleV10Top5DemoPack.asset";
        const string HoleV11Top5Folder = OutputFolder + "/HoleV11Top5";
        const string HoleV11Top5PackPath = "Assets/ArrowMagic/SOData/Packs/HoleV11Top5DemoPack.asset";
        const string HoleV12Top5Folder = OutputFolder + "/HoleV12Top5";
        const string HoleV12Top5PackPath = "Assets/ArrowMagic/SOData/Packs/HoleV12Top5DemoPack.asset";
        const string HoleV13Top5Folder = OutputFolder + "/HoleV13Top5";
        const string HoleV13Top5PackPath = "Assets/ArrowMagic/SOData/Packs/HoleV13Top5DemoPack.asset";
        const string DemoScenePath = "Assets/ArrowMagic/Scenes/Demo.unity";
        const string HoleV05ReportPath = PreviewFolder + "/hole_constrained_v05_probe.txt";
        const string HoleV05PreviewPath = PreviewFolder + "/hole_constrained_v05_best.png";
        const string HoleV05SmokeReportPath = PreviewFolder + "/hole_constrained_v05_smoke.txt";
        const string HoleV05SmokePreviewPath = PreviewFolder + "/hole_constrained_v05_smoke.png";
        const string HoleV05Top5ReportPath = PreviewFolder + "/hole_constrained_v05_top5_demo.txt";
        const string HoleV06Top5ReportPath = PreviewFolder + "/hole_lane_cage_v06_top5_demo.txt";
        const string HoleV07Top5ReportPath = PreviewFolder + "/hole_dependency_motif_v07_top5_demo.txt";
        const string HoleV08Top5ReportPath = PreviewFolder + "/hole_seedlike_motif_v08_top5_demo.txt";
        const string HoleV09Top5ReportPath = PreviewFolder + "/hole_motif_router_v09_top5_demo.txt";
        const string HoleV10Top5ReportPath = PreviewFolder + "/hole_motif_diversity_v10_top5_demo.txt";
        const string HoleV11Top5ReportPath = PreviewFolder + "/hole_layered_macro_v11_top5_demo.txt";
        const string HoleV12Top5ReportPath = PreviewFolder + "/hole_template_visual_v12_top5_demo.txt";
        const string HoleV13Top5ReportPath = PreviewFolder + "/hole_template_blocks_v13_top5_demo.txt";
        const int V13MaxShortOuterExitsForTop5 = 2;
        const int V13MaxBoundaryStraightOuterExits = 0;
        const int V13BoundaryStraightOuterLength = 5;

        static readonly string[] MaskPaths =
        {
            "Assets/ArrowMagic/Masks/Mask_19x19-Star.png",
            "Assets/ArrowMagic/Masks/Mask_22x20-Frog.png",
            "Assets/ArrowMagic/Masks/Mask_16x16-Colors.png",
            "Assets/ArrowMagic/Masks/Mask_16x16-Easter.png",
            "Assets/ArrowMagic/Masks/Mask_16x16-NoColor.png",
        };

        static readonly Dictionary<AuthoredLevelData, List<int>> kAuthoredBlockCache = new();

        static void ApplyLevelMetadata(LevelDefinition def, string sourceScreenshotId, string seedTags)
        {
            if (def == null)
                return;

            var variantField = typeof(LevelDefinition).GetField("variant", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (variantField == null)
            {
                if (string.IsNullOrEmpty(seedTags) && string.IsNullOrEmpty(def.levelId))
                    return;

                def.levelId = string.IsNullOrEmpty(def.levelId)
                    ? $"{sourceScreenshotId}_{(seedTags ?? string.Empty).GetHashCode():x8}"
                    : $"{def.levelId}_{sourceScreenshotId}";
                return;
            }

            var variantObject = variantField.GetValue(def);
            if (variantObject == null)
                return;

            var variantType = variantObject.GetType();
            var sourceField = variantType.GetField("sourceScreenshotId", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (sourceField != null && sourceField.FieldType == typeof(string))
                sourceField.SetValue(variantObject, sourceScreenshotId);

            var tagsField = variantType.GetField("seedTags", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (tagsField != null && tagsField.FieldType == typeof(string))
                tagsField.SetValue(variantObject, seedTags);
        }

        static void ApplyGenerationDifficulty(LevelDefinition def, int difficultyScore)
        {
            if (def == null)
                return;

            var generation = def.generation;
            var generationType = generation.GetType();
            var computedField = generationType.GetField("computedDifficultyScoreV0", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (computedField != null && computedField.FieldType == typeof(float))
            {
                computedField.SetValue(generation, (float)difficultyScore);
                return;
            }
            if (computedField != null && computedField.FieldType == typeof(int))
            {
                computedField.SetValue(generation, difficultyScore);
                return;
            }

            var fallbackField = generationType.GetField("targetDifficultyScore", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (fallbackField != null && fallbackField.FieldType == typeof(int))
                fallbackField.SetValue(generation, difficultyScore);
        }

        static LevelDefinition SaveOrUpdateDefinitionAsset(LevelDefinition definition, string assetPath, string logMessage = "Save LevelDefinition")
        {
            if (definition == null || string.IsNullOrWhiteSpace(assetPath))
                return null;

            EnsureFolder(Path.GetDirectoryName(assetPath).Replace("\\", "/"));

            var existing = AssetDatabase.LoadAssetAtPath<LevelDefinition>(assetPath);
            if (existing == null)
            {
                AssetDatabase.CreateAsset(definition, assetPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Selection.activeObject = definition;
                Debug.Log($"[MaskPreviewPackBuilder] {logMessage}: {assetPath}");
                return definition;
            }

            EditorUtility.CopySerialized(definition, existing);
            EditorUtility.SetDirty(existing);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Selection.activeObject = existing;
            Debug.Log($"[MaskPreviewPackBuilder] Updated {assetPath}");
            return existing;
        }

        static void FillAuthoredDefinition(LevelDefinition def, string id, AuthoredLevelData authored)
        {
            if (def == null)
                return;

            def.levelId = id;
            def.source = LevelDefinition.LevelSource.Authored;
            def.board.width = authored != null ? authored.width : 8;
            def.board.height = authored != null ? authored.height : 8;
            def.generation.arrowCoverage = 1f;
            def.generation.minPathLen = BoardController.DefaultMinArrowCellCount;
            def.generation.maxPathLength = Mathf.Max(2, (def.board.width * def.board.height));
            def.authoredLevel = CloneAuthoredLevel(authored);
        }

        static AuthoredLevelData CloneAuthoredLevel(AuthoredLevelData source)
        {
            if (source == null)
                return null;

            var clone = new AuthoredLevelData
            {
                width = source.width,
                height = source.height,
                arrows = new List<AuthoredArrowData>()
            };

            if (source.arrows != null)
            {
                for (int i = 0; i < source.arrows.Count; i++)
                {
                    var arrow = source.arrows[i];
                    clone.arrows.Add(new AuthoredArrowData
                    {
                        indices = arrow != null && arrow.indices != null ? new List<int>(arrow.indices) : new List<int>(),
                        colorIndex = arrow != null ? arrow.colorIndex : 0
                    });
                }
            }

            SetAuthoredBlockIndices(clone, GetAuthoredBlockIndices(source));
            return clone;
        }

        static bool TryConvertBoardToAuthoredLevel(BoardState board, out AuthoredLevelData authored, out string error)
        {
            error = string.Empty;
            authored = null;

            if (board == null)
            {
                error = "[MaskPreviewPackBuilder] Board is null.";
                return false;
            }

            var result = new AuthoredLevelData
            {
                width = board.width,
                height = board.height,
                arrows = new List<AuthoredArrowData>()
            };

            var visited = new HashSet<int>();
            var chainSet = new HashSet<int>();
            var ordered = new List<Vector2Int>();
            int removedChains = 0;
            int totalCells = board.width * board.height;
            var blocked = new List<int>(totalCells / 8);

            for (int i = 0; i < totalCells; i++)
            {
                if (visited.Contains(i))
                    continue;

                if (board.tiles[i].type != TileType.Arrow)
                {
                    int nonArrowX = i % board.width;
                    int nonArrowY = i / board.width;
                    var emptyPos = new Vector2Int(nonArrowX, nonArrowY);
                    if (!board.InBounds(emptyPos.x, emptyPos.y))
                        continue;

                    if (board.tiles[i].type != TileType.Arrow)
                        blocked.Add(i);
                    continue;
                }

                int x = i % board.width;
                int y = i / board.width;
                var start = new Vector2Int(x, y);

                chainSet.Clear();
                ordered.Clear();
                ArrowChainUtility.CollectFullChain(board, start, 0, chainSet);
                if (chainSet.Count == 0)
                {
                    visited.Add(i);
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

                if (!TryBuildStableOrderedChain(board, chainSet, start, out ordered))
                {
                    foreach (int n in chainSet)
                        board.tiles[n] = TileState.Empty();
                    removedChains++;
                    continue;
                }

                var data = new AuthoredArrowData
                {
                    indices = new List<int>(ordered.Count),
                    colorIndex = result.arrows.Count
                };
                for (int j = 0; j < ordered.Count; j++)
                    data.indices.Add(ordered[j].x + ordered[j].y * board.width);

                result.arrows.Add(data);
            }

            SetAuthoredBlockIndices(result, blocked);
            if (!AuthoredLevelBuilder.TryBuildBoard(result, out var authoredBoard, out error) || authoredBoard == null || result.arrows.Count < 1)
            {
                return false;
            }

            if (removedChains > 0)
                error = $"[MaskPreviewPackBuilder] Converted board; removed {removedChains} invalid chains.";

            authored = result;
            return true;
        }

        static List<int> GetAuthoredBlockIndices(AuthoredLevelData authored)
        {
            if (authored == null)
                return null;

            if (kAuthoredBlockCache.TryGetValue(authored, out var result))
                return result != null && result.Count > 0 ? new List<int>(result) : null;

            return null;
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
                return true;

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
                }
            }

            return rebuiltCount == ordered.Count;
        }

        static void SetAuthoredBlockIndices(AuthoredLevelData authored, List<int> blockIndices)
        {
            if (authored == null)
                return;

            if (blockIndices == null || blockIndices.Count == 0)
            {
                kAuthoredBlockCache.Remove(authored);
                return;
            }

            kAuthoredBlockCache[authored] = new List<int>(blockIndices);
        }

        static void MarkAuthoredOccupied(bool[] occupied, AuthoredLevelData data)
        {
            if (occupied == null || data == null || data.arrows == null)
                return;

            var baseBlocks = GetAuthoredBlockIndices(data);
            if (baseBlocks != null)
            {
                for (int i = 0; i < baseBlocks.Count; i++)
                {
                    int idx = baseBlocks[i];
                    if (idx >= 0 && idx < occupied.Length)
                        occupied[idx] = true;
                }
            }

            foreach (var arrow in data.arrows)
            {
                if (arrow?.indices == null)
                    continue;

                foreach (int idx in arrow.indices)
                    if (idx >= 0 && idx < occupied.Length)
                        occupied[idx] = true;
            }
        }

        [MenuItem("Tools/Masks/Build Star Frog 16x16 Preview Pack")]
        public static void BuildStarFrog16PreviewPack()
        {
            EnsureFolder(OutputFolder);
            EnsureFolder(PreviewFolder);

            var levels = new List<LevelDefinition>();
            var previews = new List<string>();
            var report = new List<string>
            {
                "mask preview pack",
                $"builtAtUtc={DateTime.UtcNow:O}",
                "columns=id,mask,seed,width,height,arrowTiles,chains,initialClearable,initialClearableRatio,difficulty,preview"
            };

            foreach (string maskPath in MaskPaths)
            {
                var mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);
                if (mask == null)
                {
                    Debug.LogWarning($"[MaskPreviewPackBuilder] Missing mask: {maskPath}");
                    continue;
                }

                for (int variant = 0; variant < 2; variant++)
                {
                    int baseSeed = 73000 + Array.IndexOf(MaskPaths, maskPath) * 1000 + variant * 137;
                    if (!TryBuildCandidate(mask, baseSeed, out var board, out int generatedSeed, out var stats))
                    {
                        Debug.LogWarning($"[MaskPreviewPackBuilder] No candidate for {maskPath} variant {variant + 1}");
                        continue;
                    }

                    if (!TryConvertBoardToAuthoredLevel(board, out var authored, out string error))
                    {
                        Debug.LogWarning($"[MaskPreviewPackBuilder] Convert failed for {maskPath}: {error}");
                        continue;
                    }

                    string id = $"mask_preview_{Path.GetFileNameWithoutExtension(maskPath).ToLowerInvariant().Replace('-', '_')}_{variant + 1}";
                    var def = ScriptableObject.CreateInstance<LevelDefinition>();
                    FillAuthoredDefinition(def, id, authored);
                    ApplyGenerationDifficulty(def, stats.DifficultyScore);
                    ApplyLevelMetadata(def, Path.GetFileNameWithoutExtension(maskPath), $"maskPreview; generatedSeed={generatedSeed}; initialClearable={stats.InitialMovableArrowChainCount}; chains={stats.ChainCount}" );
                    def.arrowColorMode = BoardController.ArrowColorMode.UsePalette;

                    string assetPath = $"{OutputFolder}/{id}.asset";
                    var saved = SaveOrUpdateDefinitionAsset(def, assetPath, "Save mask preview level");
                    if (saved != null)
                        levels.Add(saved);

                    string previewPath = $"{PreviewFolder}/{id}.png";
                    RenderPreview(board, mask, previewPath);
                    previews.Add(previewPath);

                    float ratio = stats.ChainCount > 0 ? stats.InitialMovableArrowChainCount / (float)stats.ChainCount : 0f;
                    report.Add($"{id},{Path.GetFileName(maskPath)},{generatedSeed},{board.width},{board.height},{stats.ArrowTileCount},{stats.ChainCount},{stats.InitialMovableArrowChainCount},{ratio:0.000},{stats.DifficultyScore},{previewPath}");
                }
            }

            SavePack(levels);
            string reportPath = $"{PreviewFolder}/mask_preview_report.txt";
            File.WriteAllLines(ToFullPath(reportPath), report);
            RenderContactSheet(previews, $"{PreviewFolder}/mask_preview_contact_sheet.png");

            AssetDatabase.Refresh();
            Debug.Log($"[MaskPreviewPackBuilder] Built {levels.Count} levels. Pack={PackPath}, previews={PreviewFolder}");
        }

        [MenuItem("Tools/Masks/Probe Low Opening Mask Tuning")]
        public static void ProbeLowOpeningMaskTuning()
        {
            EnsureFolder(PreviewFolder);
            string reportPath = $"{PreviewFolder}/low_opening_probe.txt";
            var lines = new List<string>
            {
                "low opening probe",
                $"builtAtUtc={DateTime.UtcNow:O}",
                "columns=mask,allowed,maxPath,ratioGate,countGate,attempts,greedy,accepted,bestRatio,bestInitial,bestChains,bestTiles,bestSeed"
            };

            foreach (string maskPath in MaskPaths)
            {
                var mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);
                if (mask == null)
                    continue;

                bool[] canSpawn = BuildCanSpawn(mask);
                int allowed = canSpawn.Count(v => v);
                int[] maxPaths = mask.width <= 16 ? new[] { 8, 10 } : new[] { 10, 12 };
                float ratioGate = maskPath.IndexOf("Star", StringComparison.OrdinalIgnoreCase) >= 0 ? 0.25f : 0.20f;
                int countGate = mask.width <= 16 ? 8 : 10;

                foreach (int maxPath in maxPaths)
                {
                    ProbeOne(mask.width, mask.height, canSpawn, 87000 + Array.IndexOf(MaskPaths, maskPath) * 1000 + maxPath, maxPath, ratioGate, countGate,
                        out int attempts, out int greedy, out int accepted, out float bestRatio, out int bestInitial, out int bestChains, out int bestTiles, out int bestSeed);
                    lines.Add($"{Path.GetFileName(maskPath)},{allowed},{maxPath},{ratioGate:0.000},{countGate},{attempts},{greedy},{accepted},{bestRatio:0.000},{bestInitial},{bestChains},{bestTiles},{bestSeed}");
                }
            }

            File.WriteAllLines(ToFullPath(reportPath), lines);
            AssetDatabase.Refresh();
            Debug.Log($"[MaskPreviewPackBuilder] Low opening probe written: {reportPath}");
        }

        [MenuItem("Tools/Masks/Probe Low Opening Picker Tuning")]
        public static void ProbeLowOpeningPickerTuning()
        {
            EnsureFolder(PreviewFolder);
            string reportPath = $"{PreviewFolder}/low_opening_picker_probe.txt";
            var lines = new List<string>
            {
                "low opening picker probe",
                $"builtAtUtc={DateTime.UtcNow:O}",
                "columns=mask,allowed,profile,targetInitial,targetDifficulty,maxPath,twistiness,attempts,greedy,accepted26,accepted22,bestRatio,bestInitial,bestChains,bestTiles,bestDifficulty,bestSeed,preview"
            };

            var finalPreviews = new List<string>();
            foreach (string maskPath in MaskPaths)
            {
                var mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);
                if (mask == null)
                    continue;

                bool[] canSpawn = BuildCanSpawn(mask);
                int allowed = canSpawn.Count(v => v);
                var profiles = BuildLowOpeningProbeProfiles(mask, canSpawn);
                LowOpeningProbeCandidate bestForMask = default;
                bool hasBestForMask = false;

                foreach (var profile in profiles)
                {
                    ProbeLowOpeningPicker(mask, canSpawn, profile, 180,
                        out int greedy,
                        out int accepted26,
                        out int accepted22,
                        out var best);

                    string preview = "";
                    if (best.Board != null)
                    {
                        string maskName = Path.GetFileNameWithoutExtension(maskPath).ToLowerInvariant().Replace('-', '_');
                        preview = $"{PreviewFolder}/low_opening_picker_{maskName}_{profile.Name}.png";
                        RenderPreview(best.Board, mask, preview);

                        if (!hasBestForMask || IsBetterLowOpeningCandidate(best, bestForMask))
                        {
                            bestForMask = best;
                            hasBestForMask = true;
                        }
                    }

                    lines.Add($"{Path.GetFileName(maskPath)},{allowed},{profile.Name},{profile.TargetInitial},{profile.TargetDifficulty},{profile.MaxPath},{profile.Twistiness:0.000},180,{greedy},{accepted26},{accepted22},{best.OpeningRatio:0.000},{best.Stats.InitialMovableArrowChainCount},{best.Stats.ChainCount},{best.Stats.ArrowTileCount},{best.Stats.DifficultyScore},{best.Seed},{preview}");
                }

                if (hasBestForMask)
                {
                    string maskName = Path.GetFileNameWithoutExtension(maskPath).ToLowerInvariant().Replace('-', '_');
                    string finalPreview = $"{PreviewFolder}/low_opening_picker_best_{maskName}.png";
                    RenderPreview(bestForMask.Board, mask, finalPreview);
                    finalPreviews.Add(finalPreview);
                    lines.Add($"BEST,{Path.GetFileName(maskPath)},{allowed},{bestForMask.ProfileName},{bestForMask.TargetInitial},{bestForMask.TargetDifficulty},{bestForMask.MaxPath},{bestForMask.Twistiness:0.000},-,-,-,-,{bestForMask.OpeningRatio:0.000},{bestForMask.Stats.InitialMovableArrowChainCount},{bestForMask.Stats.ChainCount},{bestForMask.Stats.ArrowTileCount},{bestForMask.Stats.DifficultyScore},{bestForMask.Seed},{finalPreview}");
                }
            }

            File.WriteAllLines(ToFullPath(reportPath), lines);
            RenderContactSheet(finalPreviews, $"{PreviewFolder}/low_opening_picker_best_contact_sheet.png");
            AssetDatabase.Refresh();
            Debug.Log($"[MaskPreviewPackBuilder] Low opening picker probe written: {reportPath}");
        }

        [MenuItem("Tools/Masks/Probe Frog Twisty Structure Picker")]
        public static void ProbeFrogTwistyStructurePicker()
        {
            EnsureFolder(PreviewFolder);
            string maskPath = "Assets/ArrowMagic/Masks/Mask_22x20-Frog.png";
            var mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);
            if (mask == null)
            {
                Debug.LogWarning($"[MaskPreviewPackBuilder] Missing mask: {maskPath}");
                return;
            }

            bool[] canSpawn = BuildCanSpawn(mask);
            int allowed = canSpawn.Count(v => v);
            var profiles = new[]
            {
                new LowOpeningProbeProfile("frog_twisty_m3_p18", 10, 0, 3, 18, 0.78f),
                new LowOpeningProbeProfile("frog_twisty_m4_p20", 10, 0, 4, 20, 0.82f),
                new LowOpeningProbeProfile("frog_twisty_m4_p22", 12, 0, 4, 22, 0.86f),
            };

            string reportPath = $"{PreviewFolder}/frog_twisty_structure_probe.txt";
            var lines = new List<string>
            {
                "frog twisty structure picker probe",
                $"builtAtUtc={DateTime.UtcNow:O}",
                "columns=mask,allowed,profile,targetInitial,minPath,maxPath,twistiness,attempts,greedy,accepted30,accepted28,bestRatio,bestInitial,bestChains,bestTiles,avgChainLen,maxChainLen,bestDifficulty,bestSeed,preview"
            };

            var previews = new List<string>();
            foreach (var profile in profiles)
            {
                ProbeTwistyStructurePicker(mask, canSpawn, profile, 160,
                    out int greedy,
                    out int accepted30,
                    out int accepted28,
                    out var best);

                string preview = "";
                if (best.Board != null)
                {
                    preview = $"{PreviewFolder}/frog_twisty_structure_{profile.Name}.png";
                    RenderPreview(best.Board, mask, preview);
                    previews.Add(preview);
                }

                float avgLen = best.Stats.ChainCount > 0 ? best.Stats.ArrowTileCount / (float)best.Stats.ChainCount : 0f;
                lines.Add($"{Path.GetFileName(maskPath)},{allowed},{profile.Name},{profile.TargetInitial},{profile.MinPath},{profile.MaxPath},{profile.Twistiness:0.000},160,{greedy},{accepted30},{accepted28},{best.OpeningRatio:0.000},{best.Stats.InitialMovableArrowChainCount},{best.Stats.ChainCount},{best.Stats.ArrowTileCount},{avgLen:0.00},{best.Stats.MaxChainLength},{best.Stats.DifficultyScore},{best.Seed},{preview}");
            }

            File.WriteAllLines(ToFullPath(reportPath), lines);
            RenderContactSheet(previews, $"{PreviewFolder}/frog_twisty_structure_contact_sheet.png");
            AssetDatabase.Refresh();
            Debug.Log($"[MaskPreviewPackBuilder] Frog twisty structure probe written: {reportPath}");
        }

        [MenuItem("Tools/Masks/Probe Hole Mask Blocker Structured")]
        public static void ProbeHoleMaskBlockerStructured()
        {
            const string externalMaskPath = "F:/360MoveData/Users/admin/Pictures/hole.png";
            EnsureFolder(PreviewFolder);

            if (!TryLoadTextureFromFile(externalMaskPath, out var mask))
            {
                Debug.LogError($"[MaskPreviewPackBuilder] Missing external mask: {externalMaskPath}");
                return;
            }

            bool[] canSpawn = BuildCanSpawn(mask);
            int allowed = canSpawn.Count(v => v);
            int blocked = canSpawn.Length - allowed;
            string reportPath = $"{PreviewFolder}/hole_blocker_structured_probe.txt";
            var lines = new List<string>
            {
                "hole blocker structured probe",
                $"builtAtUtc={DateTime.UtcNow:O}",
                $"source={externalMaskPath}",
                $"mask={mask.width}x{mask.height},allowed={allowed},blocked={blocked},blockedRatio={blocked / (float)Mathf.Max(1, canSpawn.Length):0.000}",
                "columns=profile,arrowFill,minPath,maxPath,twistiness,attempts,greedy,accepted20,accepted25,bestRatio,bestInitial,bestChains,bestTiles,bestAvgChain,bestMaxChain,bestDifficulty,bestSeed,preview"
            };

            var profiles = new[]
            {
                new HoleProbeProfile("structured_4_20", 0.86f, 4, 20, 0.42f),
                new HoleProbeProfile("structured_4_24", 0.86f, 4, 24, 0.35f),
                new HoleProbeProfile("structured_5_24", 0.84f, 5, 24, 0.38f),
                new HoleProbeProfile("structured_5_28", 0.84f, 5, 28, 0.32f),
            };

            HoleProbeCandidate bestOverall = default;
            bool hasBestOverall = false;
            var bestPreviews = new List<string>();

            foreach (var profile in profiles)
            {
                ProbeHoleProfile(mask, canSpawn, profile, 240,
                    out int greedy,
                    out int accepted20,
                    out int accepted25,
                    out var best);

                string preview = "";
                if (best.Board != null)
                {
                    preview = $"{PreviewFolder}/hole_blocker_structured_{profile.Name}.png";
                    RenderPreview(best.Board, mask, preview);
                    bestPreviews.Add(preview);

                    if (!hasBestOverall || IsBetterHoleCandidate(best, bestOverall))
                    {
                        bestOverall = best;
                        hasBestOverall = true;
                    }
                }

                lines.Add($"{profile.Name},{profile.ArrowFill:0.000},{profile.MinPath},{profile.MaxPath},{profile.Twistiness:0.000},240,{greedy},{accepted20},{accepted25},{best.OpeningRatio:0.000},{best.Stats.InitialMovableArrowChainCount},{best.Stats.ChainCount},{best.Stats.ArrowTileCount},{best.AverageChainLength:0.00},{best.Stats.MaxChainLength},{best.Stats.DifficultyScore},{best.Seed},{preview}");
            }

            if (hasBestOverall)
            {
                string preview = $"{PreviewFolder}/hole_blocker_structured_best.png";
                RenderPreview(bestOverall.Board, mask, preview);
                bestPreviews.Insert(0, preview);
                lines.Add($"BEST,{bestOverall.Profile.Name},{bestOverall.Profile.ArrowFill:0.000},{bestOverall.Profile.MinPath},{bestOverall.Profile.MaxPath},{bestOverall.Profile.Twistiness:0.000},-,-,-,-,{bestOverall.OpeningRatio:0.000},{bestOverall.Stats.InitialMovableArrowChainCount},{bestOverall.Stats.ChainCount},{bestOverall.Stats.ArrowTileCount},{bestOverall.AverageChainLength:0.00},{bestOverall.Stats.MaxChainLength},{bestOverall.Stats.DifficultyScore},{bestOverall.Seed},{preview}");
            }

            File.WriteAllLines(ToFullPath(reportPath), lines);
            RenderContactSheet(bestPreviews, $"{PreviewFolder}/hole_blocker_structured_contact_sheet.png");
            UnityEngine.Object.DestroyImmediate(mask);
            AssetDatabase.Refresh();
            Debug.Log($"[MaskPreviewPackBuilder] Hole blocker structured probe written: {reportPath}");
        }

        [MenuItem("Tools/Masks/Hole V05/Run Constrained Probe")]
        public static void ProbeHoleMaskConstrainedV05()
        {
            RunHoleMaskConstrainedV05(360, HoleV05ReportPath, HoleV05PreviewPath, showDialog: true);
        }

        [MenuItem("Tools/Masks/Hole V05/Run Smoke Probe")]
        public static void ProbeHoleMaskConstrainedV05Smoke()
        {
            RunHoleMaskConstrainedV05(24, HoleV05SmokeReportPath, HoleV05SmokePreviewPath, showDialog: true);
        }

        [MenuItem("Tools/Masks/Hole V05/Build Top5 Demo Pack")]
        public static void BuildHoleMaskConstrainedV05Top5DemoPack()
        {
            BuildHoleMaskConstrainedV05Top5DemoPack(attempts: 2400, topCount: 5, attachToDemo: true, showDialog: true);
        }

        [MenuItem("Tools/Masks/Hole V06/Build Lane Cage Top5")]
        public static void BuildHoleMaskLaneCageV06Top5DemoPack()
        {
            BuildHoleMaskLaneCageV06Top5DemoPack(topCount: 5, attachToDemo: true, showDialog: true);
        }

        [MenuItem("Tools/Masks/Hole V07/Build Dependency Motif Top5")]
        public static void BuildHoleMaskDependencyMotifV07Top5DemoPack()
        {
            BuildHoleMaskDependencyMotifV07Top5DemoPack(topCount: 5, attachToDemo: true, showDialog: true);
        }

        [MenuItem("Tools/Masks/Hole V08/Build Seed-Like Motif Top5")]
        public static void BuildHoleMaskSeedLikeMotifV08Top5DemoPack()
        {
            BuildHoleMaskSeedLikeMotifV08Top5DemoPack(topCount: 5, attachToDemo: true, showDialog: true);
        }

        [MenuItem("Tools/Masks/Hole V09/Build Motif Router Top5")]
        public static void BuildHoleMaskMotifRouterV09Top5DemoPack()
        {
            BuildHoleMaskMotifRouterV09Top5DemoPack(topCount: 5, attachToDemo: true, showDialog: true);
        }

        [MenuItem("Tools/Masks/Hole V10/Build Motif Diversity Top5")]
        public static void BuildHoleMaskMotifDiversityV10Top5DemoPack()
        {
            BuildHoleMaskMotifDiversityV10Top5DemoPack(topCount: 5, attachToDemo: true, showDialog: true);
        }

        [MenuItem("Tools/Masks/Hole V11/Build Layered Macro Top5")]
        public static void BuildHoleMaskLayeredMacroV11Top5DemoPack()
        {
            BuildHoleMaskLayeredMacroV11Top5DemoPack(topCount: 5, attachToDemo: true, showDialog: true);
        }

        [MenuItem("Tools/Masks/Hole V12/Build Template Visual Top5")]
        public static void BuildHoleMaskTemplateVisualV12Top5DemoPack()
        {
            BuildHoleMaskTemplateVisualV12Top5DemoPack(topCount: 5, attachToDemo: true, showDialog: true);
        }

        [MenuItem("Tools/Masks/Hole V13/Build Template Blocks Top5")]
        public static void BuildHoleMaskTemplateBlocksV13Top5DemoPack()
        {
            BuildHoleMaskTemplateBlocksV13Top5DemoPack(topCount: 5, attachToDemo: true, showDialog: true);
        }

        static void BuildHoleMaskTemplateBlocksV13Top5DemoPack(int topCount, bool attachToDemo, bool showDialog)
        {
            EnsureFolder(HoleV13Top5Folder);
            EnsureFolder(PreviewFolder);
            ClearGeneratedAssetsInFolder(HoleV13Top5Folder);
            ClearPreviewFiles("hole_v13_template_blocks_");

            if (!TryLoadHoleMask(out var mask, out string maskSource))
            {
                Debug.LogError($"[MaskPreviewPackBuilder] Missing hole mask. Tried {HoleMaskProjectPath} then {HoleMaskExternalPath}");
                return;
            }

            bool[] canSpawn = BuildCanSpawn(mask);
            s_V13DesignHeavyGreedyFailDebug = null;
            var candidates = BuildTemplateBlocksV13Candidates(mask.width, mask.height, canSpawn, topCount, out var diag);
            var levels = new List<LevelDefinition>();
            var lines = new List<string>
            {
                "hole template blocks v13 top5 demo pack",
                $"builtAtUtc={DateTime.UtcNow:O}",
                $"source={maskSource}",
                "savedLevels=pending",
                "target=fillRatio>=0.80, greedy=true, headToHead=0, directBlock=0, non-mechanical local template blocks first",
                diag,
                "columns=rank,id,seed,variant,fillRatio,preCleanupFill,emptyAllowed,ratio,initial,chains,tiles,avgChain,maxChain,difficulty,modules,turns,visualPenalty,roleSummary,shapeSummary,regionSignature,stripePenalty,directOuter,shortOuter,boundaryStraightOuter,preCleanupShortOuter,wrappedOuter,nakedOuterPenalty,edgeClusterPenalty,cleanupOps,innerRefillOps,ringMotifOps,headToHead,directBlock,dependencyContact,preview"
            };

            for (int i = 0; i < candidates.Count; i++)
            {
                var c = candidates[i];
                int variant = Mathf.Max(0, c.Seed - 930000);
                string id = $"hole_v13_template_blocks_{i + 1:00}_seed_{c.Seed}";
                string preview = $"{PreviewFolder}/{id}.png";
                RenderPreview(c.Board, mask, preview);

                var def = ScriptableObject.CreateInstance<LevelDefinition>();
                FillAuthoredDefinition(def, id, c.Authored);
                ApplyGenerationDifficulty(def, c.Stats.DifficultyScore);
                ApplyLevelMetadata(def, Path.GetFileNameWithoutExtension(HoleMaskProjectPath), $"holeV13TemplateBlocks; seed={c.Seed}; variant={variant}; fillRatio={c.FillRatio:0.000}; preCleanupFill={c.PreCleanupFillRatio:0.000}; " +
                    $"ratio={c.OpeningRatio:0.000}; initial={c.Stats.InitialMovableArrowChainCount}; chains={c.Stats.ChainCount}; " +
                    $"avgChain={c.AverageChainLength:0.00}; modules={c.LaneCount}; turns={c.TurnCount}; " +
                    $"visualPenalty={c.VisualRegularityPenalty:0.000}; roles={c.ChainRoleSummary}; shapes={c.ChainShapeSummary}; " +
                    $"regions={c.RegionSignature}; directOuter={c.DirectOuterExits}; shortOuter={c.ShortOuterExits}; boundaryStraightOuter={c.BoundaryStraightOuterExits}; preCleanupShortOuter={c.PreCleanupShortOuterExits}; wrappedOuter={c.WrappedOuterExits}; cleanupOps={c.ExitCleanupOps}; innerRefillOps={c.InnerRefillOps}; ringMotifOps={c.RingMotifOps}; " +
                    $"nakedOuterPenalty={c.NakedOuterExitPenalty}; edgeClusterPenalty={c.OuterExitClusterPenalty}; " +
                    $"headToHead={c.HeadToHeadConflicts}; directBlock={c.DirectBlockAims}; dependencyContact={c.BadArrowContacts}" );
                def.arrowColorMode = BoardController.ArrowColorMode.UsePalette;

                string assetPath = $"{HoleV13Top5Folder}/{id}.asset";
                var saved = SaveOrUpdateDefinitionAsset(def, assetPath, "Save hole V13 template blocks level");
                if (saved != null)
                    levels.Add(saved);

                lines.Add($"{i + 1},{id},{c.Seed},{variant},{c.FillRatio:0.000},{c.PreCleanupFillRatio:0.000},{c.AllowedEmptyCount},{c.OpeningRatio:0.000},{c.Stats.InitialMovableArrowChainCount},{c.Stats.ChainCount},{c.Stats.ArrowTileCount},{c.AverageChainLength:0.00},{c.Stats.MaxChainLength},{c.Stats.DifficultyScore},{c.LaneCount},{c.TurnCount},{c.VisualRegularityPenalty:0.000},{c.ChainRoleSummary},{c.ChainShapeSummary},{c.RegionSignature},{c.MacroStripePenalty:0.000},{c.DirectOuterExits},{c.ShortOuterExits},{c.BoundaryStraightOuterExits},{c.PreCleanupShortOuterExits},{c.WrappedOuterExits},{c.NakedOuterExitPenalty},{c.OuterExitClusterPenalty},{c.ExitCleanupOps},{c.InnerRefillOps},{c.RingMotifOps},{c.HeadToHeadConflicts},{c.DirectBlockAims},{c.BadArrowContacts},{preview}");
            }

            if (s_V13DesignHeavyGreedyFailDebug.HasValue)
            {
                var c = s_V13DesignHeavyGreedyFailDebug.Value;
                int variant = Mathf.Max(0, c.Seed - 930000);
                string preview = $"{PreviewFolder}/hole_v13_template_blocks_debug_design_greedy_fail_seed_{c.Seed}.png";
                RenderPreview(c.Board, mask, preview);
                lines.Add($"debugDesignGreedyFail,seed={c.Seed},variant={variant},fill={c.FillRatio:0.000},initial={c.Stats.InitialMovableArrowChainCount},chains={c.Stats.ChainCount},tiles={c.Stats.ArrowTileCount},avgChain={c.AverageChainLength:0.00},roles={c.ChainRoleSummary},shapes={c.ChainShapeSummary},stripe={c.MacroStripePenalty:0.000},dependencyContact={c.BadArrowContacts},preview={preview}");
            }

            var pack = SavePackAt(levels, HoleV13Top5PackPath, "hole_v13_template_blocks_top5_demo_pack", "Hole V13 Template Blocks Top5 Demo Pack");
            lines[3] = $"savedLevels={levels.Count}";
            File.WriteAllLines(ToFullPath(HoleV13Top5ReportPath), lines);
            if (attachToDemo && pack != null)
                AttachPackToDemo(pack);

            UnityEngine.Object.DestroyImmediate(mask);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"[MaskPreviewPackBuilder] Built Hole V13 Template Blocks Top5 Demo Pack: {HoleV13Top5PackPath}, levels={levels.Count}, report={HoleV13Top5ReportPath}");
            if (showDialog && !Application.isBatchMode)
                EditorUtility.DisplayDialog("Hole V13 Template Blocks Complete", $"Pack:\n{HoleV13Top5PackPath}\n\nReport:\n{HoleV13Top5ReportPath}", "OK");
        }

        static void BuildHoleMaskTemplateVisualV12Top5DemoPack(int topCount, bool attachToDemo, bool showDialog)
        {
            EnsureFolder(HoleV12Top5Folder);
            EnsureFolder(PreviewFolder);
            ClearGeneratedAssetsInFolder(HoleV12Top5Folder);
            ClearPreviewFiles("hole_v12_template_visual_");

            if (!TryLoadHoleMask(out var mask, out string maskSource))
            {
                Debug.LogError($"[MaskPreviewPackBuilder] Missing hole mask. Tried {HoleMaskProjectPath} then {HoleMaskExternalPath}");
                return;
            }

            bool[] canSpawn = BuildCanSpawn(mask);
            var candidates = BuildTemplateVisualV12Candidates(mask.width, mask.height, canSpawn, topCount, out var diag);
            var levels = new List<LevelDefinition>();
            var lines = new List<string>
            {
                "hole template visual v12 top5 demo pack",
                $"builtAtUtc={DateTime.UtcNow:O}",
                $"source={maskSource}",
                "savedLevels=pending",
                "target=inherit V11 hard gates; add local chain-group visual regularity penalty from seed signatures",
                diag,
                "columns=rank,id,seed,variant,fillRatio,emptyAllowed,ratio,initial,chains,tiles,avgChain,maxChain,difficulty,turns,layerCount,visualPenalty,roleSummary,shapeSummary,regionSignature,stripePenalty,headToHead,directBlock,dependencyContact,preview"
            };

            for (int i = 0; i < candidates.Count; i++)
            {
                var c = candidates[i];
                int variant = Mathf.Max(0, c.Seed - 910000);
                string id = $"hole_v12_template_visual_{i + 1:00}_seed_{c.Seed}";
                string preview = $"{PreviewFolder}/{id}.png";
                RenderPreview(c.Board, mask, preview);

                var def = ScriptableObject.CreateInstance<LevelDefinition>();
                FillAuthoredDefinition(def, id, c.Authored);
                ApplyGenerationDifficulty(def, c.Stats.DifficultyScore);
                ApplyLevelMetadata(def, Path.GetFileNameWithoutExtension(HoleMaskProjectPath), $"holeV12TemplateVisual; seed={c.Seed}; variant={variant}; fillRatio={c.FillRatio:0.000}; " +
                    $"ratio={c.OpeningRatio:0.000}; initial={c.Stats.InitialMovableArrowChainCount}; chains={c.Stats.ChainCount}; " +
                    $"avgChain={c.AverageChainLength:0.00}; turns={c.TurnCount}; layerCount={c.LayerCount}; " +
                    $"visualPenalty={c.VisualRegularityPenalty:0.000}; roles={c.ChainRoleSummary}; shapes={c.ChainShapeSummary}; " +
                    $"regions={c.RegionSignature}; headToHead={c.HeadToHeadConflicts}; directBlock={c.DirectBlockAims}; dependencyContact={c.BadArrowContacts}" );
                def.arrowColorMode = BoardController.ArrowColorMode.UsePalette;

                string assetPath = $"{HoleV12Top5Folder}/{id}.asset";
                var saved = SaveOrUpdateDefinitionAsset(def, assetPath, "Save hole V12 template visual level");
                if (saved != null)
                    levels.Add(saved);

                lines.Add($"{i + 1},{id},{c.Seed},{variant},{c.FillRatio:0.000},{c.AllowedEmptyCount},{c.OpeningRatio:0.000},{c.Stats.InitialMovableArrowChainCount},{c.Stats.ChainCount},{c.Stats.ArrowTileCount},{c.AverageChainLength:0.00},{c.Stats.MaxChainLength},{c.Stats.DifficultyScore},{c.TurnCount},{c.LayerCount},{c.VisualRegularityPenalty:0.000},{c.ChainRoleSummary},{c.ChainShapeSummary},{c.RegionSignature},{c.MacroStripePenalty:0.000},{c.HeadToHeadConflicts},{c.DirectBlockAims},{c.BadArrowContacts},{preview}");
            }

            var pack = SavePackAt(levels, HoleV12Top5PackPath, "hole_v12_template_visual_top5_demo_pack", "Hole V12 Template Visual Top5 Demo Pack");
            lines[3] = $"savedLevels={levels.Count}";
            File.WriteAllLines(ToFullPath(HoleV12Top5ReportPath), lines);
            if (attachToDemo && pack != null)
                AttachPackToDemo(pack);

            UnityEngine.Object.DestroyImmediate(mask);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"[MaskPreviewPackBuilder] Built Hole V12 Template Visual Top5 Demo Pack: {HoleV12Top5PackPath}, levels={levels.Count}, report={HoleV12Top5ReportPath}");
            if (showDialog && !Application.isBatchMode)
                EditorUtility.DisplayDialog("Hole V12 Template Visual Complete", $"Pack:\n{HoleV12Top5PackPath}\n\nReport:\n{HoleV12Top5ReportPath}", "OK");
        }

        static void BuildHoleMaskLayeredMacroV11Top5DemoPack(int topCount, bool attachToDemo, bool showDialog)
        {
            EnsureFolder(HoleV11Top5Folder);
            EnsureFolder(PreviewFolder);
            ClearGeneratedAssetsInFolder(HoleV11Top5Folder);
            ClearPreviewFiles("hole_v11_layered_macro_");

            if (!TryLoadHoleMask(out var mask, out string maskSource))
            {
                Debug.LogError($"[MaskPreviewPackBuilder] Missing hole mask. Tried {HoleMaskProjectPath} then {HoleMaskExternalPath}");
                return;
            }

            bool[] canSpawn = BuildCanSpawn(mask);
            var candidates = BuildLayeredMacroV11Candidates(mask.width, mask.height, canSpawn, topCount, out var diag);
            var levels = new List<LevelDefinition>();
            var lines = new List<string>
            {
                "hole layered macro v11 top5 demo pack",
                $"builtAtUtc={DateTime.UtcNow:O}",
                $"source={maskSource}",
                "savedLevels=pending",
                "target=fillRatio>=0.999, greedy=true, headToHead=0, directBlock=0, layeredRelease=true, lowerStripePenalty=better",
                diag,
                "columns=rank,id,seed,variant,fillRatio,emptyAllowed,ratio,initial,chains,tiles,avgChain,maxChain,difficulty,modules,turns,motifKindCount,straightRatio,dirEntropy,repeatedPenalty,holeEdgeCoverage,layerCount,layer0,layer0Ratio,maxLayerWidth,deadChains,unexpectedEarlyOpen,layerBalance,stripePenalty,headToHead,directBlock,dependencyContact,layerSummary,motifPlan,preview"
            };

            for (int i = 0; i < candidates.Count; i++)
            {
                var c = candidates[i];
                int variant = Mathf.Max(0, c.Seed - 910000);
                string id = $"hole_v11_layered_macro_{i + 1:00}_seed_{c.Seed}";
                string preview = $"{PreviewFolder}/{id}.png";
                RenderPreview(c.Board, mask, preview);

                var def = ScriptableObject.CreateInstance<LevelDefinition>();
                FillAuthoredDefinition(def, id, c.Authored);
                ApplyGenerationDifficulty(def, c.Stats.DifficultyScore);
                ApplyLevelMetadata(def, Path.GetFileNameWithoutExtension(HoleMaskProjectPath), $"holeV11LayeredMacro; seed={c.Seed}; variant={variant}; fillRatio={c.FillRatio:0.000}; " +
                    $"ratio={c.OpeningRatio:0.000}; initial={c.Stats.InitialMovableArrowChainCount}; chains={c.Stats.ChainCount}; " +
                    $"avgChain={c.AverageChainLength:0.00}; modules={c.LaneCount}; turns={c.TurnCount}; " +
                    $"motifKindCount={c.MotifKindCount}; straightRatio={c.StraightChainRatio:0.000}; dirEntropy={c.RegionDirectionEntropy:0.000}; " +
                    $"layerCount={c.LayerCount}; layer0={c.Layer0Chains}; layerBalance={c.LayerBalance:0.000}; stripePenalty={c.MacroStripePenalty:0.000}; " +
                    $"layerSummary={c.LayerSummary}; headToHead={c.HeadToHeadConflicts}; directBlock={c.DirectBlockAims}; dependencyContact={c.BadArrowContacts}" );
                def.arrowColorMode = BoardController.ArrowColorMode.UsePalette;

                string assetPath = $"{HoleV11Top5Folder}/{id}.asset";
                var saved = SaveOrUpdateDefinitionAsset(def, assetPath, "Save hole V11 layered macro level");
                if (saved != null)
                    levels.Add(saved);

                lines.Add($"{i + 1},{id},{c.Seed},{variant},{c.FillRatio:0.000},{c.AllowedEmptyCount},{c.OpeningRatio:0.000},{c.Stats.InitialMovableArrowChainCount},{c.Stats.ChainCount},{c.Stats.ArrowTileCount},{c.AverageChainLength:0.00},{c.Stats.MaxChainLength},{c.Stats.DifficultyScore},{c.LaneCount},{c.TurnCount},{c.MotifKindCount},{c.StraightChainRatio:0.000},{c.RegionDirectionEntropy:0.000},{c.RepeatedMotifPenalty:0.000},{c.HoleEdgeCoverage:0.000},{c.LayerCount},{c.Layer0Chains},{c.Layer0Ratio:0.000},{c.MaxLayerWidth},{c.DeadChains},{c.UnexpectedEarlyOpen},{c.LayerBalance:0.000},{c.MacroStripePenalty:0.000},{c.HeadToHeadConflicts},{c.DirectBlockAims},{c.BadArrowContacts},{c.LayerSummary},{c.MotifPlan},{preview}");
            }

            var pack = SavePackAt(levels, HoleV11Top5PackPath, "hole_v11_layered_macro_top5_demo_pack", "Hole V11 Layered Macro Top5 Demo Pack");
            lines[3] = $"savedLevels={levels.Count}";
            File.WriteAllLines(ToFullPath(HoleV11Top5ReportPath), lines);
            if (attachToDemo && pack != null)
                AttachPackToDemo(pack);

            UnityEngine.Object.DestroyImmediate(mask);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"[MaskPreviewPackBuilder] Built Hole V11 Layered Macro Top5 Demo Pack: {HoleV11Top5PackPath}, levels={levels.Count}, report={HoleV11Top5ReportPath}");
            if (showDialog && !Application.isBatchMode)
                EditorUtility.DisplayDialog("Hole V11 Layered Macro Complete", $"Pack:\n{HoleV11Top5PackPath}\n\nReport:\n{HoleV11Top5ReportPath}", "OK");
        }

        static void BuildHoleMaskMotifDiversityV10Top5DemoPack(int topCount, bool attachToDemo, bool showDialog)
        {
            EnsureFolder(HoleV10Top5Folder);
            EnsureFolder(PreviewFolder);
            ClearGeneratedAssetsInFolder(HoleV10Top5Folder);
            ClearPreviewFiles("hole_v10_motif_diversity_");

            if (!TryLoadHoleMask(out var mask, out string maskSource))
            {
                Debug.LogError($"[MaskPreviewPackBuilder] Missing hole mask. Tried {HoleMaskProjectPath} then {HoleMaskExternalPath}");
                return;
            }

            bool[] canSpawn = BuildCanSpawn(mask);
            var candidates = BuildMotifDiversityV10Candidates(mask.width, mask.height, canSpawn, topCount, out var diag);
            var levels = new List<LevelDefinition>();
            var lines = new List<string>
            {
                "hole motif diversity v10 top5 demo pack",
                $"builtAtUtc={DateTime.UtcNow:O}",
                $"source={maskSource}",
                "savedLevels=pending",
                diag,
                "columns=rank,id,seed,variant,fillRatio,emptyAllowed,ratio,initial,chains,tiles,avgChain,maxChain,difficulty,modules,turns,motifKindCount,straightRatio,dirEntropy,repeatedPenalty,holeEdgeCoverage,headToHead,directBlock,dependencyContact,motifPlan,preview"
            };

            for (int i = 0; i < candidates.Count; i++)
            {
                var c = candidates[i];
                int variant = Mathf.Max(0, c.Seed - 900000);
                string id = $"hole_v10_motif_diversity_{i + 1:00}_seed_{c.Seed}";
                string preview = $"{PreviewFolder}/{id}.png";
                RenderPreview(c.Board, mask, preview);

                var def = ScriptableObject.CreateInstance<LevelDefinition>();
                FillAuthoredDefinition(def, id, c.Authored);
                ApplyGenerationDifficulty(def, c.Stats.DifficultyScore);
                ApplyLevelMetadata(def, Path.GetFileNameWithoutExtension(HoleMaskProjectPath), $"holeV10MotifDiversity; seed={c.Seed}; variant={variant}; fallbackUsed=false; fillRatio={c.FillRatio:0.000}; " +
                    $"ratio={c.OpeningRatio:0.000}; initial={c.Stats.InitialMovableArrowChainCount}; chains={c.Stats.ChainCount}; " +
                    $"avgChain={c.AverageChainLength:0.00}; modules={c.LaneCount}; turns={c.TurnCount}; " +
                    $"motifKindCount={c.MotifKindCount}; straightRatio={c.StraightChainRatio:0.000}; dirEntropy={c.RegionDirectionEntropy:0.000}; " +
                    $"repeatedPenalty={c.RepeatedMotifPenalty:0.000}; holeEdgeCoverage={c.HoleEdgeCoverage:0.000}; motifPlan={c.MotifPlan}; " +
                    $"headToHead={c.HeadToHeadConflicts}; directBlock={c.DirectBlockAims}; dependencyContact={c.BadArrowContacts}" );
                def.arrowColorMode = BoardController.ArrowColorMode.UsePalette;

                string assetPath = $"{HoleV10Top5Folder}/{id}.asset";
                var saved = SaveOrUpdateDefinitionAsset(def, assetPath, "Save hole V10 motif diversity level");
                if (saved != null)
                    levels.Add(saved);

                lines.Add($"{i + 1},{id},{c.Seed},{variant},{c.FillRatio:0.000},{c.AllowedEmptyCount},{c.OpeningRatio:0.000},{c.Stats.InitialMovableArrowChainCount},{c.Stats.ChainCount},{c.Stats.ArrowTileCount},{c.AverageChainLength:0.00},{c.Stats.MaxChainLength},{c.Stats.DifficultyScore},{c.LaneCount},{c.TurnCount},{c.MotifKindCount},{c.StraightChainRatio:0.000},{c.RegionDirectionEntropy:0.000},{c.RepeatedMotifPenalty:0.000},{c.HoleEdgeCoverage:0.000},{c.HeadToHeadConflicts},{c.DirectBlockAims},{c.BadArrowContacts},{c.MotifPlan},{preview}");
            }

            var pack = SavePackAt(levels, HoleV10Top5PackPath, "hole_v10_motif_diversity_top5_demo_pack", "Hole V10 Motif Diversity Top5 Demo Pack");
            lines[3] = $"savedLevels={levels.Count}";
            File.WriteAllLines(ToFullPath(HoleV10Top5ReportPath), lines);
            if (attachToDemo && pack != null)
                AttachPackToDemo(pack);

            UnityEngine.Object.DestroyImmediate(mask);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"[MaskPreviewPackBuilder] Built Hole V10 Motif Diversity Top5 Demo Pack: {HoleV10Top5PackPath}, levels={levels.Count}, report={HoleV10Top5ReportPath}");
            if (showDialog && !Application.isBatchMode)
                EditorUtility.DisplayDialog("Hole V10 Motif Diversity Complete", $"Pack:\n{HoleV10Top5PackPath}\n\nReport:\n{HoleV10Top5ReportPath}", "OK");
        }

        static void BuildHoleMaskMotifRouterV09Top5DemoPack(int topCount, bool attachToDemo, bool showDialog)
        {
            EnsureFolder(HoleV09Top5Folder);
            EnsureFolder(PreviewFolder);
            ClearGeneratedAssetsInFolder(HoleV09Top5Folder);
            ClearPreviewFiles("hole_v09_motif_router_");

            if (!TryLoadHoleMask(out var mask, out string maskSource))
            {
                Debug.LogError($"[MaskPreviewPackBuilder] Missing hole mask. Tried {HoleMaskProjectPath} then {HoleMaskExternalPath}");
                return;
            }

            bool[] canSpawn = BuildCanSpawn(mask);
            var candidates = BuildMotifRouterV09Candidates(mask.width, mask.height, canSpawn, topCount, out var diag);
            var levels = new List<LevelDefinition>();
            var lines = new List<string>
            {
                "hole motif router v09 top5 demo pack",
                $"builtAtUtc={DateTime.UtcNow:O}",
                $"source={maskSource}",
                "savedLevels=pending",
                diag,
                "columns=rank,id,seed,fillRatio,emptyAllowed,ratio,initial,chains,tiles,avgChain,maxChain,difficulty,modules,turns,frameLate,headToHead,directBlock,dependencyContact,preview"
            };

            for (int i = 0; i < candidates.Count; i++)
            {
                var c = candidates[i];
                string id = $"hole_v09_motif_router_{i + 1:00}_seed_{c.Seed}";
                string preview = $"{PreviewFolder}/{id}.png";
                RenderPreview(c.Board, mask, preview);

                var def = ScriptableObject.CreateInstance<LevelDefinition>();
                FillAuthoredDefinition(def, id, c.Authored);
                ApplyGenerationDifficulty(def, c.Stats.DifficultyScore);
                ApplyLevelMetadata(def, Path.GetFileNameWithoutExtension(HoleMaskProjectPath), $"holeV09MotifRouter; seed={c.Seed}; fillRatio={c.FillRatio:0.000}; emptyAllowed={c.AllowedEmptyCount}; " +
                    $"ratio={c.OpeningRatio:0.000}; initial={c.Stats.InitialMovableArrowChainCount}; chains={c.Stats.ChainCount}; " +
                    $"avgChain={c.AverageChainLength:0.00}; modules={c.LaneCount}; turns={c.TurnCount}; frameLate={c.FrameLateScore:0.000}; " +
                    $"headToHead={c.HeadToHeadConflicts}; directBlock={c.DirectBlockAims}; dependencyContact={c.BadArrowContacts}" );
                def.arrowColorMode = BoardController.ArrowColorMode.UsePalette;

                string assetPath = $"{HoleV09Top5Folder}/{id}.asset";
                var saved = SaveOrUpdateDefinitionAsset(def, assetPath, "Save hole V09 motif router level");
                if (saved != null)
                    levels.Add(saved);

                lines.Add($"{i + 1},{id},{c.Seed},{c.FillRatio:0.000},{c.AllowedEmptyCount},{c.OpeningRatio:0.000},{c.Stats.InitialMovableArrowChainCount},{c.Stats.ChainCount},{c.Stats.ArrowTileCount},{c.AverageChainLength:0.00},{c.Stats.MaxChainLength},{c.Stats.DifficultyScore},{c.LaneCount},{c.TurnCount},{c.FrameLateScore:0.000},{c.HeadToHeadConflicts},{c.DirectBlockAims},{c.BadArrowContacts},{preview}");
            }

            var pack = SavePackAt(levels, HoleV09Top5PackPath, "hole_v09_motif_router_top5_demo_pack", "Hole V09 Motif Router Top5 Demo Pack");
            lines[3] = $"savedLevels={levels.Count}";
            File.WriteAllLines(ToFullPath(HoleV09Top5ReportPath), lines);
            if (attachToDemo && pack != null)
                AttachPackToDemo(pack);

            UnityEngine.Object.DestroyImmediate(mask);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"[MaskPreviewPackBuilder] Built Hole V09 Motif Router Top5 Demo Pack: {HoleV09Top5PackPath}, levels={levels.Count}, report={HoleV09Top5ReportPath}");
            if (showDialog && !Application.isBatchMode)
                EditorUtility.DisplayDialog("Hole V09 Motif Router Complete", $"Pack:\n{HoleV09Top5PackPath}\n\nReport:\n{HoleV09Top5ReportPath}", "OK");
        }

        static void BuildHoleMaskSeedLikeMotifV08Top5DemoPack(int topCount, bool attachToDemo, bool showDialog)
        {
            EnsureFolder(HoleV08Top5Folder);
            EnsureFolder(PreviewFolder);
            ClearGeneratedAssetsInFolder(HoleV08Top5Folder);
            ClearPreviewFiles("hole_v08_seedlike_motif_");

            if (!TryLoadHoleMask(out var mask, out string maskSource))
            {
                Debug.LogError($"[MaskPreviewPackBuilder] Missing hole mask. Tried {HoleMaskProjectPath} then {HoleMaskExternalPath}");
                return;
            }

            bool[] canSpawn = BuildCanSpawn(mask);
            var candidates = BuildSeedLikeMotifV08Candidates(mask.width, mask.height, canSpawn, topCount, out var diag);
            var levels = new List<LevelDefinition>();
            var lines = new List<string>
            {
                "hole seed-like motif v08 top5 demo pack",
                $"builtAtUtc={DateTime.UtcNow:O}",
                $"source={maskSource}",
                "savedLevels=pending",
                diag,
                "columns=rank,id,seed,fillRatio,emptyAllowed,ratio,initial,chains,tiles,avgChain,maxChain,difficulty,lanes,turns,frameLate,headToHead,directBlock,dependencyContact,preview"
            };

            for (int i = 0; i < candidates.Count; i++)
            {
                var c = candidates[i];
                string id = $"hole_v08_seedlike_motif_{i + 1:00}_seed_{c.Seed}";
                string preview = $"{PreviewFolder}/{id}.png";
                RenderPreview(c.Board, mask, preview);

                var def = ScriptableObject.CreateInstance<LevelDefinition>();
                FillAuthoredDefinition(def, id, c.Authored);
                ApplyGenerationDifficulty(def, c.Stats.DifficultyScore);
                ApplyLevelMetadata(def, Path.GetFileNameWithoutExtension(HoleMaskProjectPath), $"holeV08SeedLikeMotif; seed={c.Seed}; fillRatio={c.FillRatio:0.000}; emptyAllowed={c.AllowedEmptyCount}; " +
                    $"ratio={c.OpeningRatio:0.000}; initial={c.Stats.InitialMovableArrowChainCount}; chains={c.Stats.ChainCount}; " +
                    $"avgChain={c.AverageChainLength:0.00}; lanes={c.LaneCount}; turns={c.TurnCount}; frameLate={c.FrameLateScore:0.000}; " +
                    $"headToHead={c.HeadToHeadConflicts}; directBlock={c.DirectBlockAims}; dependencyContact={c.BadArrowContacts}" );
                def.arrowColorMode = BoardController.ArrowColorMode.UsePalette;

                string assetPath = $"{HoleV08Top5Folder}/{id}.asset";
                var saved = SaveOrUpdateDefinitionAsset(def, assetPath, "Save hole V08 seed-like motif level");
                if (saved != null)
                    levels.Add(saved);

                lines.Add($"{i + 1},{id},{c.Seed},{c.FillRatio:0.000},{c.AllowedEmptyCount},{c.OpeningRatio:0.000},{c.Stats.InitialMovableArrowChainCount},{c.Stats.ChainCount},{c.Stats.ArrowTileCount},{c.AverageChainLength:0.00},{c.Stats.MaxChainLength},{c.Stats.DifficultyScore},{c.LaneCount},{c.TurnCount},{c.FrameLateScore:0.000},{c.HeadToHeadConflicts},{c.DirectBlockAims},{c.BadArrowContacts},{preview}");
            }

            var pack = SavePackAt(levels, HoleV08Top5PackPath, "hole_v08_seedlike_motif_top5_demo_pack", "Hole V08 Seed-Like Motif Top5 Demo Pack");
            lines[3] = $"savedLevels={levels.Count}";
            File.WriteAllLines(ToFullPath(HoleV08Top5ReportPath), lines);
            if (attachToDemo && pack != null)
                AttachPackToDemo(pack);

            UnityEngine.Object.DestroyImmediate(mask);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"[MaskPreviewPackBuilder] Built Hole V08 Seed-Like Motif Top5 Demo Pack: {HoleV08Top5PackPath}, levels={levels.Count}, report={HoleV08Top5ReportPath}");
            if (showDialog && !Application.isBatchMode)
                EditorUtility.DisplayDialog("Hole V08 Seed-Like Motif Complete", $"Pack:\n{HoleV08Top5PackPath}\n\nReport:\n{HoleV08Top5ReportPath}", "OK");
        }

        static void BuildHoleMaskDependencyMotifV07Top5DemoPack(int topCount, bool attachToDemo, bool showDialog)
        {
            EnsureFolder(HoleV07Top5Folder);
            EnsureFolder(PreviewFolder);
            ClearGeneratedAssetsInFolder(HoleV07Top5Folder);
            ClearPreviewFiles("hole_v07_dependency_motif_");

            if (!TryLoadHoleMask(out var mask, out string maskSource))
            {
                Debug.LogError($"[MaskPreviewPackBuilder] Missing hole mask. Tried {HoleMaskProjectPath} then {HoleMaskExternalPath}");
                return;
            }

            bool[] canSpawn = BuildCanSpawn(mask);
            var candidates = BuildDependencyMotifV07Candidates(mask.width, mask.height, canSpawn, topCount, out var diag);
            var levels = new List<LevelDefinition>();
            var lines = new List<string>
            {
                "hole dependency motif v07 top5 demo pack",
                $"builtAtUtc={DateTime.UtcNow:O}",
                $"source={maskSource}",
                "savedLevels=pending",
                diag,
                "columns=rank,id,seed,fillRatio,emptyAllowed,ratio,initial,chains,tiles,avgChain,maxChain,difficulty,lanes,turns,frameLate,headToHead,directBlock,dependencyContact,preview"
            };

            for (int i = 0; i < candidates.Count; i++)
            {
                var c = candidates[i];
                string id = $"hole_v07_dependency_motif_{i + 1:00}_seed_{c.Seed}";
                string preview = $"{PreviewFolder}/{id}.png";
                RenderPreview(c.Board, mask, preview);

                var def = ScriptableObject.CreateInstance<LevelDefinition>();
                FillAuthoredDefinition(def, id, c.Authored);
                ApplyGenerationDifficulty(def, c.Stats.DifficultyScore);
                ApplyLevelMetadata(def, Path.GetFileNameWithoutExtension(HoleMaskProjectPath), $"holeV07DependencyMotif; seed={c.Seed}; fillRatio={c.FillRatio:0.000}; emptyAllowed={c.AllowedEmptyCount}; " +
                    $"ratio={c.OpeningRatio:0.000}; initial={c.Stats.InitialMovableArrowChainCount}; chains={c.Stats.ChainCount}; " +
                    $"avgChain={c.AverageChainLength:0.00}; lanes={c.LaneCount}; turns={c.TurnCount}; frameLate={c.FrameLateScore:0.000}; " +
                    $"headToHead={c.HeadToHeadConflicts}; directBlock={c.DirectBlockAims}; dependencyContact={c.BadArrowContacts}" );
                def.arrowColorMode = BoardController.ArrowColorMode.UsePalette;

                string assetPath = $"{HoleV07Top5Folder}/{id}.asset";
                var saved = SaveOrUpdateDefinitionAsset(def, assetPath, "Save hole V07 dependency motif level");
                if (saved != null)
                    levels.Add(saved);

                lines.Add($"{i + 1},{id},{c.Seed},{c.FillRatio:0.000},{c.AllowedEmptyCount},{c.OpeningRatio:0.000},{c.Stats.InitialMovableArrowChainCount},{c.Stats.ChainCount},{c.Stats.ArrowTileCount},{c.AverageChainLength:0.00},{c.Stats.MaxChainLength},{c.Stats.DifficultyScore},{c.LaneCount},{c.TurnCount},{c.FrameLateScore:0.000},{c.HeadToHeadConflicts},{c.DirectBlockAims},{c.BadArrowContacts},{preview}");
            }

            var pack = SavePackAt(levels, HoleV07Top5PackPath, "hole_v07_dependency_motif_top5_demo_pack", "Hole V07 Dependency Motif Top5 Demo Pack");
            lines[3] = $"savedLevels={levels.Count}";
            File.WriteAllLines(ToFullPath(HoleV07Top5ReportPath), lines);
            if (attachToDemo && pack != null)
                AttachPackToDemo(pack);

            UnityEngine.Object.DestroyImmediate(mask);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"[MaskPreviewPackBuilder] Built Hole V07 Dependency Motif Top5 Demo Pack: {HoleV07Top5PackPath}, levels={levels.Count}, report={HoleV07Top5ReportPath}");
            if (showDialog && !Application.isBatchMode)
                EditorUtility.DisplayDialog("Hole V07 Dependency Motif Complete", $"Pack:\n{HoleV07Top5PackPath}\n\nReport:\n{HoleV07Top5ReportPath}", "OK");
        }

        static void BuildHoleMaskLaneCageV06Top5DemoPack(int topCount, bool attachToDemo, bool showDialog)
        {
            EnsureFolder(HoleV06Top5Folder);
            EnsureFolder(PreviewFolder);

            if (!TryLoadHoleMask(out var mask, out string maskSource))
            {
                Debug.LogError($"[MaskPreviewPackBuilder] Missing hole mask. Tried {HoleMaskProjectPath} then {HoleMaskExternalPath}");
                return;
            }

            bool[] canSpawn = BuildCanSpawn(mask);
            var candidates = BuildLaneCageV06Candidates(mask.width, mask.height, canSpawn, topCount, out var diag);
            var levels = new List<LevelDefinition>();
            var lines = new List<string>
            {
                "hole lane cage v06 top5 demo pack",
                $"builtAtUtc={DateTime.UtcNow:O}",
                $"source={maskSource}",
                "savedLevels=pending",
                diag,
                "columns=rank,id,seed,fillRatio,emptyAllowed,ratio,initial,chains,tiles,avgChain,maxChain,difficulty,lanes,turns,frameLate,headToHead,directBlock,badArrowContact,preview"
            };

            for (int i = 0; i < candidates.Count; i++)
            {
                var c = candidates[i];
                string id = $"hole_v06_lane_cage_{i + 1:00}_seed_{c.Seed}";
                string preview = $"{PreviewFolder}/{id}.png";
                RenderPreview(c.Board, mask, preview);

                var def = ScriptableObject.CreateInstance<LevelDefinition>();
                FillAuthoredDefinition(def, id, c.Authored);
                ApplyGenerationDifficulty(def, c.Stats.DifficultyScore);
                ApplyLevelMetadata(def, Path.GetFileNameWithoutExtension(HoleMaskProjectPath), $"holeV06LaneCage; seed={c.Seed}; fillRatio={c.FillRatio:0.000}; emptyAllowed={c.AllowedEmptyCount}; " +
                    $"ratio={c.OpeningRatio:0.000}; initial={c.Stats.InitialMovableArrowChainCount}; chains={c.Stats.ChainCount}; " +
                    $"avgChain={c.AverageChainLength:0.00}; lanes={c.LaneCount}; turns={c.TurnCount}; frameLate={c.FrameLateScore:0.000}; " +
                    $"headToHead={c.HeadToHeadConflicts}; directBlock={c.DirectBlockAims}; badArrowContact={c.BadArrowContacts}" );
                def.arrowColorMode = BoardController.ArrowColorMode.UsePalette;

                string assetPath = $"{HoleV06Top5Folder}/{id}.asset";
                var saved = SaveOrUpdateDefinitionAsset(def, assetPath, "Save hole V06 lane cage level");
                if (saved != null)
                    levels.Add(saved);

                lines.Add($"{i + 1},{id},{c.Seed},{c.FillRatio:0.000},{c.AllowedEmptyCount},{c.OpeningRatio:0.000},{c.Stats.InitialMovableArrowChainCount},{c.Stats.ChainCount},{c.Stats.ArrowTileCount},{c.AverageChainLength:0.00},{c.Stats.MaxChainLength},{c.Stats.DifficultyScore},{c.LaneCount},{c.TurnCount},{c.FrameLateScore:0.000},{c.HeadToHeadConflicts},{c.DirectBlockAims},{c.BadArrowContacts},{preview}");
            }

            var pack = SavePackAt(levels, HoleV06Top5PackPath, "hole_v06_lane_cage_top5_demo_pack", "Hole V06 Lane Cage Top5 Demo Pack");
            lines[3] = $"savedLevels={levels.Count}";
            File.WriteAllLines(ToFullPath(HoleV06Top5ReportPath), lines);
            if (attachToDemo && pack != null)
                AttachPackToDemo(pack);

            UnityEngine.Object.DestroyImmediate(mask);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"[MaskPreviewPackBuilder] Built Hole V06 Lane Cage Top5 Demo Pack: {HoleV06Top5PackPath}, levels={levels.Count}, report={HoleV06Top5ReportPath}");
            if (showDialog && !Application.isBatchMode)
                EditorUtility.DisplayDialog("Hole V06 Lane Cage Complete", $"Pack:\n{HoleV06Top5PackPath}\n\nReport:\n{HoleV06Top5ReportPath}", "OK");
        }

        static void RunHoleMaskConstrainedV05(int attempts, string reportPath, string previewPath, bool showDialog)
        {
            EnsureFolder(PreviewFolder);

            if (!TryLoadHoleMask(out var mask, out string maskSource))
            {
                Debug.LogError($"[MaskPreviewPackBuilder] Missing hole mask. Tried {HoleMaskProjectPath} then {HoleMaskExternalPath}");
                return;
            }

            bool[] canSpawn = BuildCanSpawn(mask);
            var profile = new HoleProbeProfile("v05_5_28", 0.84f, 5, 28, 0.32f);
            ProbeHoleConstrained(mask, canSpawn, profile, attempts,
                out int screened,
                out int greedyChecked,
                out int greedy,
                out int accepted20,
                out int accepted22,
                out var best);

            string preview = "";
            if (best.Board != null)
            {
                preview = previewPath;
                RenderPreview(best.Board, mask, preview);
            }

            var lines = new List<string>
            {
                "hole constrained v05 probe",
                $"builtAtUtc={DateTime.UtcNow:O}",
                $"source={maskSource}",
                "columns=profile,attempts,screened,greedyChecked,greedy,accepted20,accepted22,bestRatio,bestInitial,bestChains,bestTiles,bestAvgChain,bestMaxChain,bestDifficulty,bestSeed,flips,outerExits,blockedByArrow,blockedByHole,preview",
                $"{profile.Name},{attempts},{screened},{greedyChecked},{greedy},{accepted20},{accepted22},{best.OpeningRatio:0.000},{best.Stats.InitialMovableArrowChainCount},{best.Stats.ChainCount},{best.Stats.ArrowTileCount},{best.AverageChainLength:0.00},{best.Stats.MaxChainLength},{best.Stats.DifficultyScore},{best.Seed},{best.Flips},{best.OuterExits},{best.BlockedByArrow},{best.BlockedByHole},{preview}"
            };
            File.WriteAllLines(ToFullPath(reportPath), lines);
            UnityEngine.Object.DestroyImmediate(mask);
            AssetDatabase.Refresh();
            Debug.Log($"[MaskPreviewPackBuilder] Hole constrained v05 probe written: {reportPath}");
            if (showDialog && !Application.isBatchMode)
            {
                EditorUtility.DisplayDialog(
                    "Hole V05 Probe Complete",
                    $"Report:\n{reportPath}\n\nPreview:\n{previewPath}",
                    "OK");
            }
        }

        static void BuildHoleMaskConstrainedV05Top5DemoPack(int attempts, int topCount, bool attachToDemo, bool showDialog)
        {
            EnsureFolder(HoleV05Top5Folder);
            EnsureFolder(PreviewFolder);

            if (!TryLoadHoleMask(out var mask, out string maskSource))
            {
                Debug.LogError($"[MaskPreviewPackBuilder] Missing hole mask. Tried {HoleMaskProjectPath} then {HoleMaskExternalPath}");
                return;
            }

            bool[] canSpawn = BuildCanSpawn(mask);
            int allowedCount = CountAllowedCells(canSpawn);
            var profile = new HoleProbeProfile("v05_dense_2_40", 0.95f, 2, 40, 0.38f);
            var candidates = BuildHoleConstrainedCandidates(mask, canSpawn, profile, attempts, topCount,
                true,
                allowedCount,
                out int greedyChecked,
                out int greedy,
                out int shapeRejected,
                out int screened);
            if (candidates.Count < topCount)
            {
                var fallback = BuildHoleEdgeRunFallbackCandidates(mask.width, mask.height, canSpawn, topCount - candidates.Count);
                candidates.AddRange(fallback);
            }

            var levels = new List<LevelDefinition>();
            var lines = new List<string>
            {
                "hole constrained v05 top5 demo pack",
                $"builtAtUtc={DateTime.UtcNow:O}",
                $"source={maskSource}",
                $"attempts={attempts}",
                "columns=rank,id,seed,fillRatio,emptyAllowed,ratio,initial,chains,tiles,avgChain,maxChain,difficulty,denseFilled,singletonRepairs,singleChains,headToHead,directBlock,badArrowContact,shapeRepairs,shapeClears,flips,outerExits,blockedByArrow,blockedByHole,preview"
            };

            for (int i = 0; i < candidates.Count; i++)
            {
                var candidate = candidates[i];
                string id = $"hole_v05_dense_top5_{i + 1:00}_seed_{candidate.Seed}";
                string preview = $"{PreviewFolder}/{id}.png";
                RenderPreview(candidate.Board, mask, preview);

                if (!TryConvertBoardToAuthoredLevel(candidate.Board, out var authored, out string error))
                {
                    Debug.LogWarning($"[MaskPreviewPackBuilder] Convert failed for {id}: {error}");
                    continue;
                }

                var def = ScriptableObject.CreateInstance<LevelDefinition>();
                FillAuthoredDefinition(def, id, authored);
                ApplyGenerationDifficulty(def, candidate.Stats.DifficultyScore);
                ApplyLevelMetadata(def, Path.GetFileNameWithoutExtension(HoleMaskProjectPath), $"holeV05DenseTop5; seed={candidate.Seed}; fillRatio={candidate.FillRatio:0.000}; " +
                    $"emptyAllowed={candidate.AllowedEmptyCount}; ratio={candidate.OpeningRatio:0.000}; " +
                    $"initial={candidate.Stats.InitialMovableArrowChainCount}; chains={candidate.Stats.ChainCount}; " +
                    $"avgChain={candidate.AverageChainLength:0.00}; denseFilled={candidate.DenseFilled}; " +
                    $"singletonRepairs={candidate.SingletonRepairs}; singleChains={candidate.SingleChains}; flips={candidate.Flips}; " +
                    $"headToHead={candidate.HeadToHeadConflicts}; directBlock={candidate.DirectBlockAims}; " +
                    $"badArrowContact={candidate.BadArrowContacts}; shapeRepairs={candidate.ShapeRepairs}; shapeClears={candidate.ShapeClears}; " +
                    $"outerExits={candidate.OuterExits}; blockedByArrow={candidate.BlockedByArrow}; blockedByHole={candidate.BlockedByHole}" );
                def.arrowColorMode = BoardController.ArrowColorMode.UsePalette;

                string assetPath = $"{HoleV05Top5Folder}/{id}.asset";
                var saved = SaveOrUpdateDefinitionAsset(def, assetPath, "Save hole V05 top5 level");
                if (saved != null)
                    levels.Add(saved);

                lines.Add($"{i + 1},{id},{candidate.Seed},{candidate.FillRatio:0.000},{candidate.AllowedEmptyCount},{candidate.OpeningRatio:0.000},{candidate.Stats.InitialMovableArrowChainCount},{candidate.Stats.ChainCount},{candidate.Stats.ArrowTileCount},{candidate.AverageChainLength:0.00},{candidate.Stats.MaxChainLength},{candidate.Stats.DifficultyScore},{candidate.DenseFilled},{candidate.SingletonRepairs},{candidate.SingleChains},{candidate.HeadToHeadConflicts},{candidate.DirectBlockAims},{candidate.BadArrowContacts},{candidate.ShapeRepairs},{candidate.ShapeClears},{candidate.Flips},{candidate.OuterExits},{candidate.BlockedByArrow},{candidate.BlockedByHole},{preview}");
            }

            var pack = SavePackAt(levels, HoleV05Top5PackPath, "hole_v05_top5_demo_pack", "Hole V05 Top5 Demo Pack");
            lines.Insert(4, $"greedyChecked={greedyChecked}");
            lines.Insert(5, $"greedy={greedy}");
            lines.Insert(6, $"shapeRejected={shapeRejected}");
            lines.Insert(7, $"screened={screened}");
            lines.Insert(8, $"savedLevels={levels.Count}");
            File.WriteAllLines(ToFullPath(HoleV05Top5ReportPath), lines);

            if (attachToDemo && pack != null)
                AttachPackToDemo(pack);

            UnityEngine.Object.DestroyImmediate(mask);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"[MaskPreviewPackBuilder] Built Hole V05 Top5 Demo Pack: {HoleV05Top5PackPath}, levels={levels.Count}, report={HoleV05Top5ReportPath}");
            if (showDialog && !Application.isBatchMode)
            {
                EditorUtility.DisplayDialog(
                    "Hole V05 Top5 Demo Pack Complete",
                    $"Pack:\n{HoleV05Top5PackPath}\n\nReport:\n{HoleV05Top5ReportPath}\n\nDemo:\n{DemoScenePath}",
                    "OK");
            }
        }

        [MenuItem("Tools/Masks/Hole V05/Reveal Output")]
        public static void RevealHoleV05Output()
        {
            string reportFullPath = ToFullPath(HoleV05ReportPath);
            string previewFullPath = ToFullPath(HoleV05PreviewPath);
            if (File.Exists(reportFullPath))
                EditorUtility.RevealInFinder(reportFullPath);
            else if (File.Exists(previewFullPath))
                EditorUtility.RevealInFinder(previewFullPath);
            else
                EditorUtility.RevealInFinder(ToFullPath(PreviewFolder));
        }

        [MenuItem("Tools/Masks/Hole V05/Print Last Report")]
        public static void PrintHoleV05Report()
        {
            string reportFullPath = ToFullPath(HoleV05ReportPath);
            if (!File.Exists(reportFullPath))
            {
                Debug.LogWarning($"[MaskPreviewPackBuilder] Missing report: {HoleV05ReportPath}");
                return;
            }

            Debug.Log(File.ReadAllText(reportFullPath));
        }

        readonly struct HoleProbeProfile
        {
            public readonly string Name;
            public readonly float ArrowFill;
            public readonly int MinPath;
            public readonly int MaxPath;
            public readonly float Twistiness;

            public HoleProbeProfile(string name, float arrowFill, int minPath, int maxPath, float twistiness)
            {
                Name = name;
                ArrowFill = arrowFill;
                MinPath = minPath;
                MaxPath = maxPath;
                Twistiness = twistiness;
            }
        }

        struct HoleProbeCandidate
        {
            public BoardState Board;
            public BoardGenerationTuning.BoardGenerationStats Stats;
            public HoleProbeProfile Profile;
            public int Seed;
            public float OpeningRatio;
            public float AverageChainLength;
            public float FillRatio;
            public int AllowedEmptyCount;
            public float Score;
            public int DenseFilled;
            public int SingletonRepairs;
            public int SingleChains;
            public int HeadToHeadConflicts;
            public int DirectBlockAims;
            public int BadArrowContacts;
            public int ShapeRepairs;
            public int ShapeClears;
            public int Flips;
            public int OuterExits;
            public int BlockedByArrow;
            public int BlockedByHole;
        }

        struct LaneCageCandidate
        {
            public BoardState Board;
            public AuthoredLevelData Authored;
            public BoardGenerationTuning.BoardGenerationStats Stats;
            public int Seed;
            public float OpeningRatio;
            public float AverageChainLength;
            public float FillRatio;
            public int AllowedEmptyCount;
            public float Score;
            public int LaneCount;
            public int TurnCount;
            public float FrameLateScore;
            public int HeadToHeadConflicts;
            public int DirectBlockAims;
            public int BadArrowContacts;
            public int MotifKindCount;
            public float StraightChainRatio;
            public float RegionDirectionEntropy;
            public float RepeatedMotifPenalty;
            public float HoleEdgeCoverage;
            public int LayerCount;
            public int Layer0Chains;
            public float Layer0Ratio;
            public int MaxLayerWidth;
            public int DeadChains;
            public int UnexpectedEarlyOpen;
            public float LayerBalance;
            public float MacroStripePenalty;
            public string LayerSummary;
            public string MotifPlan;
            public float VisualRegularityPenalty;
            public string ChainRoleSummary;
            public string ChainShapeSummary;
            public string RegionSignature;
            public int DirectOuterExits;
            public int ShortOuterExits;
            public int WrappedOuterExits;
            public int BoundaryStraightOuterExits;
            public int NakedOuterExitPenalty;
            public int OuterExitClusterPenalty;
            public float PreCleanupFillRatio;
            public int PreCleanupShortOuterExits;
            public int ExitCleanupOps;
            public int InnerRefillOps;
            public int RingMotifOps;
        }

        struct ChainSpec
        {
            public List<int> InnerToOuter;
            public int LaneIndex;
        }

        enum V10MotifKind
        {
            StraightWithBend,
            UShape,
            Hook,
            Turnback,
            ShortBridge,
            HoleEdgeDetour,
            ZigZagLite,
            SwerveLong
        }

        struct V10Lane
        {
            public List<int> Cells;
            public V10MotifKind Kind;
            public string Region;
        }

        sealed class V13SeedMotif
        {
            public List<Vector2Int> Points;
            public int Width;
            public int Height;
        }

        sealed class V13SeedGroupMotif
        {
            public List<List<Vector2Int>> Chains;
            public int Width;
            public int Height;
            public int Turns;
        }

        sealed class V13SeedOverlaySource
        {
            public string Id;
            public int Width;
            public int Height;
            public List<List<Vector2Int>> HeadToTailChains;
        }

        static List<V13SeedMotif> s_V13SeedMotifs;
        static List<V13SeedGroupMotif> s_V13SeedGroupMotifs;
        static List<V13SeedOverlaySource> s_V13SeedOverlaySources;
        static LaneCageCandidate? s_V13DesignHeavyGreedyFailDebug;

        readonly struct HoleCheapStats
        {
            public readonly int ChainCount;
            public readonly int ArrowTileCount;
            public readonly int InitialMovableArrowChainCount;
            public readonly int MaxChainLength;

            public HoleCheapStats(int chainCount, int arrowTileCount, int initialMovableArrowChainCount, int maxChainLength)
            {
                ChainCount = chainCount;
                ArrowTileCount = arrowTileCount;
                InitialMovableArrowChainCount = initialMovableArrowChainCount;
                MaxChainLength = maxChainLength;
            }
        }

        static void ProbeHoleConstrained(
            Texture2D mask,
            bool[] canSpawn,
            HoleProbeProfile profile,
            int attempts,
            out int screened,
            out int greedyChecked,
            out int greedy,
            out int accepted20,
            out int accepted22,
            out HoleProbeCandidate best)
        {
            var generator = new ClearAllArrowsGenerator();
            var ruleset = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int baseSeed = 119000 + profile.MinPath * 1000 + profile.MaxPath * 37 + Mathf.RoundToInt(profile.Twistiness * 100f);
            screened = 0;
            greedyChecked = 0;
            greedy = 0;
            accepted20 = 0;
            accepted22 = 0;
            best = default;
            best.Score = float.MaxValue;

            for (int attempt = 0; attempt < attempts; attempt++)
            {
                int seed = BoardGenerationTuning.BuildCandidateSeed(baseSeed, attempt);
                var board = generator.Generate(new LevelSpec
                {
                    width = mask.width,
                    height = mask.height,
                    seed = seed,
                    arrowFill = profile.ArrowFill,
                    minPathLen = profile.MinPath,
                    maxPathLen = profile.MaxPath,
                    twistiness = profile.Twistiness,
                    canSpawnHere = canSpawn
                });
                ApplyMaskBlocks(board, canSpawn);

                greedyChecked++;
                if (!GreedyValidator.TryClearAllByGreedy(board, ruleset, 500, out _))
                    continue;

                int flips = ReduceOpeningByReversingChains(board, ruleset, maxFlips: 18);
                greedy++;

                var cheapStats = CalculateHoleCheapStats(board, ruleset);
                if (cheapStats.ChainCount <= 0)
                    continue;

                var quality = AnalyzeOpeningQuality(board, ruleset);
                float cheapRatio = cheapStats.InitialMovableArrowChainCount / (float)cheapStats.ChainCount;
                float cheapAverageChainLength = cheapStats.ArrowTileCount / (float)Mathf.Max(1, cheapStats.ChainCount);
                if (cheapAverageChainLength < 6.5f || cheapRatio > 0.34f)
                    continue;

                screened++;
                var stats = BoardGenerationTuning.CalculateBoardGenerationStats(board, ruleset);
                if (stats.ChainCount <= 0)
                    continue;

                float ratio = stats.InitialMovableArrowChainCount / (float)stats.ChainCount;
                float averageChainLength = stats.ArrowTileCount / (float)Mathf.Max(1, stats.ChainCount);
                if (ratio <= 0.20f && averageChainLength >= 7f)
                    accepted20++;
                if (ratio <= 0.22f && averageChainLength >= 7f)
                    accepted22++;

                float score = Mathf.Max(0f, ratio - 0.20f) * 1600f
                              + ratio * 120f
                              + quality.outerExits * 5f
                              + quality.blockedByHole * 0.8f
                              - quality.blockedByArrow * 1.2f
                              - averageChainLength * 4f
                              + attempt * 0.001f;
                if (score >= best.Score)
                    continue;

                best = new HoleProbeCandidate
                {
                    Board = board,
                    Stats = stats,
                    Profile = profile,
                    Seed = seed,
                    OpeningRatio = ratio,
                    AverageChainLength = averageChainLength,
                    Score = score,
                    Flips = flips,
                    OuterExits = quality.outerExits,
                    BlockedByArrow = quality.blockedByArrow,
                    BlockedByHole = quality.blockedByHole
                };
            }
        }

        static List<HoleProbeCandidate> BuildHoleConstrainedCandidates(
            Texture2D mask,
            bool[] canSpawn,
            HoleProbeProfile profile,
            int attempts,
            int topCount,
            bool denseFillPriority,
            int allowedCount,
            out int greedyChecked,
            out int greedy,
            out int shapeRejected,
            out int screened)
        {
            var generator = new ClearAllArrowsGenerator();
            var ruleset = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int baseSeed = 119000 + profile.MinPath * 1000 + profile.MaxPath * 37 + Mathf.RoundToInt(profile.Twistiness * 100f);
            var result = new List<HoleProbeCandidate>();
            greedyChecked = 0;
            greedy = 0;
            shapeRejected = 0;
            screened = 0;

            for (int attempt = 0; attempt < attempts; attempt++)
            {
                int seed = BoardGenerationTuning.BuildCandidateSeed(baseSeed, attempt);
                var board = generator.Generate(new LevelSpec
                {
                    width = mask.width,
                    height = mask.height,
                    seed = seed,
                    arrowFill = profile.ArrowFill,
                    minPathLen = profile.MinPath,
                    maxPathLen = profile.MaxPath,
                    twistiness = profile.Twistiness,
                    canSpawnHere = canSpawn
                });
                ApplyMaskBlocks(board, canSpawn);
                int denseFilled = 0;
                int singletonRepairs = 0;
                int shapeRepairs = 0;
                int shapeClears = 0;
                int maxShapeClears = denseFillPriority ? Mathf.Max(18, allowedCount / 16) : 0;

                if (!TryRepairShapeViolations(board, maxShapeClears, out int initialShapeRepairs, out int initialShapeClears))
                {
                    shapeRejected++;
                    continue;
                }

                shapeRepairs += initialShapeRepairs;
                shapeClears += initialShapeClears;
                CountShapeViolations(board, out int headToHeadConflicts, out int directBlockAims, out int badArrowContacts);
                if (headToHeadConflicts > 0 || directBlockAims > 0 || badArrowContacts > 0)
                {
                    shapeRejected++;
                    continue;
                }

                greedyChecked++;
                int greedyMoveBudget = denseFillPriority ? 1200 : 500;
                if (!GreedyValidator.TryClearAllByGreedy(board, ruleset, greedyMoveBudget, out _))
                    continue;

                int flips = ReduceOpeningByReversingChains(board, ruleset, maxFlips: 18);

                if (!TryRepairShapeViolations(board, Mathf.Max(0, maxShapeClears - shapeClears), out int finalShapeRepairs, out int finalShapeClears))
                {
                    shapeRejected++;
                    continue;
                }

                shapeRepairs += finalShapeRepairs;
                shapeClears += finalShapeClears;
                CountShapeViolations(board, out headToHeadConflicts, out directBlockAims, out badArrowContacts);
                if (headToHeadConflicts > 0 || directBlockAims > 0 || badArrowContacts > 0)
                {
                    shapeRejected++;
                    continue;
                }

                if (!GreedyValidator.TryClearAllByGreedy(board, ruleset, greedyMoveBudget, out _))
                    continue;

                if (!TryRoundTripAuthoredBoard(board, out var authoredBoard))
                    continue;

                CountShapeViolations(authoredBoard, out headToHeadConflicts, out directBlockAims, out badArrowContacts);
                if (headToHeadConflicts > 0 || directBlockAims > 0 || badArrowContacts > 0)
                {
                    shapeRejected++;
                    continue;
                }

                if (!GreedyValidator.TryClearAllByGreedy(authoredBoard, ruleset, greedyMoveBudget, out _))
                    continue;

                board = authoredBoard;
                greedy++;

                var cheapStats = CalculateHoleCheapStats(board, ruleset);
                if (cheapStats.ChainCount <= 0)
                    continue;

                int singleChains = denseFillPriority ? CountSingleCellChains(board) : 0;
                if (denseFillPriority && singleChains > 0)
                    continue;

                var quality = AnalyzeOpeningQuality(board, ruleset);
                float cheapRatio = cheapStats.InitialMovableArrowChainCount / (float)cheapStats.ChainCount;
                float cheapAverageChainLength = cheapStats.ArrowTileCount / (float)Mathf.Max(1, cheapStats.ChainCount);
                if (denseFillPriority)
                {
                    if (cheapAverageChainLength < 3.5f || cheapRatio > 0.52f)
                        continue;
                }
                else if (cheapAverageChainLength < 6.5f || cheapRatio > 0.34f)
                {
                    continue;
                }

                screened++;
                var stats = BoardGenerationTuning.CalculateBoardGenerationStats(board, ruleset);
                if (stats.ChainCount <= 0)
                    continue;

                float ratio = stats.InitialMovableArrowChainCount / (float)stats.ChainCount;
                float averageChainLength = stats.ArrowTileCount / (float)Mathf.Max(1, stats.ChainCount);
                int effectiveAllowedCount = allowedCount > 0 ? allowedCount : CountAllowedCells(canSpawn);
                int allowedEmptyCount = Mathf.Max(0, effectiveAllowedCount - stats.ArrowTileCount);
                float fillRatio = stats.ArrowTileCount / (float)Mathf.Max(1, effectiveAllowedCount);
                float score;
                if (denseFillPriority)
                {
                    score = allowedEmptyCount * 10000f
                            + Mathf.Max(0f, ratio - 0.24f) * 900f
                            + ratio * 80f
                            + quality.outerExits * 2f
                            - quality.blockedByArrow * 0.6f
                            - averageChainLength * 2f
                            + Mathf.Max(0f, 5.5f - averageChainLength) * 500f
                            + attempt * 0.001f;
                }
                else
                {
                    score = Mathf.Max(0f, ratio - 0.20f) * 1600f
                            + ratio * 120f
                            + quality.outerExits * 5f
                            + quality.blockedByHole * 0.8f
                            - quality.blockedByArrow * 1.2f
                            - averageChainLength * 4f
                            + attempt * 0.001f;
                }

                result.Add(new HoleProbeCandidate
                {
                    Board = board,
                    Stats = stats,
                    Profile = profile,
                    Seed = seed,
                    OpeningRatio = ratio,
                    AverageChainLength = averageChainLength,
                    FillRatio = fillRatio,
                    AllowedEmptyCount = allowedEmptyCount,
                    Score = score,
                    DenseFilled = denseFilled,
                    SingletonRepairs = singletonRepairs,
                    SingleChains = singleChains,
                    HeadToHeadConflicts = headToHeadConflicts,
                    DirectBlockAims = directBlockAims,
                    BadArrowContacts = badArrowContacts,
                    ShapeRepairs = shapeRepairs,
                    ShapeClears = shapeClears,
                    Flips = flips,
                    OuterExits = quality.outerExits,
                    BlockedByArrow = quality.blockedByArrow,
                    BlockedByHole = quality.blockedByHole
                });
            }

            result.Sort((a, b) => a.Score.CompareTo(b.Score));
            if (result.Count > topCount)
                result.RemoveRange(topCount, result.Count - topCount);

            return result;
        }

        static List<LaneCageCandidate> BuildLaneCageV06Candidates(int width, int height, bool[] canSpawn, int topCount, out string diag)
        {
            var result = new List<LaneCageCandidate>();
            var ruleset = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int allowedCount = CountAllowedCells(canSpawn);
            int authoredBuilt = 0, boardBuilt = 0, shapeOk = 0, greedyOk = 0, totalHeadToHead = 0, totalDirectBlock = 0, totalBadContact = 0;

            for (int variant = 0; variant < 48 && result.Count < topCount; variant++)
            {
                if (!TryBuildLaneCageAuthored(width, height, canSpawn, variant, out var authored, out int laneCount, out int turnCount, out float frameLate))
                    continue;

                authoredBuilt++;
                if (!AuthoredLevelBuilder.TryBuildBoard(authored, out var board, out _))
                    continue;

                boardBuilt++;
                CountShapeViolations(board, out int headToHead, out int directBlock, out int badContact);
                totalHeadToHead += headToHead;
                totalDirectBlock += directBlock;
                totalBadContact += badContact;
                if (headToHead > 0 || directBlock > 0)
                    continue;

                shapeOk++;
                if (!GreedyValidator.TryClearAllByGreedy(board, ruleset, 1600, out _))
                    continue;

                greedyOk++;
                var stats = BoardGenerationTuning.CalculateBoardGenerationStats(board, ruleset);
                if (stats.ChainCount <= 0)
                    continue;

                float ratio = stats.InitialMovableArrowChainCount / (float)stats.ChainCount;
                float avg = stats.ArrowTileCount / (float)Mathf.Max(1, stats.ChainCount);
                int empty = Mathf.Max(0, allowedCount - stats.ArrowTileCount);
                float fill = stats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
                float score = empty * 10000f
                              + Mathf.Abs(ratio - 0.12f) * 1000f
                              - turnCount * 4f
                              - frameLate * 100f
                              + (variant < 24 ? 0f : 250f)
                              + variant;

                result.Add(new LaneCageCandidate
                {
                    Board = board,
                    Authored = authored,
                    Stats = stats,
                    Seed = 860000 + variant,
                    OpeningRatio = ratio,
                    AverageChainLength = avg,
                    FillRatio = fill,
                    AllowedEmptyCount = empty,
                    Score = score,
                    LaneCount = laneCount,
                    TurnCount = turnCount,
                    FrameLateScore = frameLate,
                    HeadToHeadConflicts = headToHead,
                    DirectBlockAims = directBlock,
                    BadArrowContacts = badContact
                });
            }

            result.Sort((a, b) => a.Score.CompareTo(b.Score));
            if (result.Count > topCount)
                result.RemoveRange(topCount, result.Count - topCount);

            diag = $"diag=authoredBuilt:{authoredBuilt},boardBuilt:{boardBuilt},shapeOk:{shapeOk},greedyOk:{greedyOk},headToHead:{totalHeadToHead},directBlock:{totalDirectBlock},badContact:{totalBadContact}";
            return result;
        }

        static List<LaneCageCandidate> BuildDependencyMotifV07Candidates(int width, int height, bool[] canSpawn, int topCount, out string diag)
        {
            var result = new List<LaneCageCandidate>();
            var ruleset = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int allowedCount = CountAllowedCells(canSpawn);
            int authoredBuilt = 0, boardBuilt = 0, shapeOk = 0, greedyOk = 0, totalHeadToHead = 0, totalDirectBlock = 0, totalDependencyContact = 0;

            for (int variant = 0; variant < 96; variant++)
            {
                if (!TryBuildDependencyMotifV07Authored(width, height, canSpawn, variant, out var authored, out int laneCount, out int turnCount, out float frameLate))
                    continue;

                authoredBuilt++;
                if (!AuthoredLevelBuilder.TryBuildBoard(authored, out var board, out _))
                    continue;

                boardBuilt++;
                CountShapeViolations(board, out int headToHead, out int directBlock, out int dependencyContact);
                totalHeadToHead += headToHead;
                totalDirectBlock += directBlock;
                totalDependencyContact += dependencyContact;
                if (headToHead > 0 || directBlock > 0)
                    continue;

                shapeOk++;
                if (!GreedyValidator.TryClearAllByGreedy(board, ruleset, 1800, out _))
                    continue;

                greedyOk++;
                var stats = BoardGenerationTuning.CalculateBoardGenerationStats(board, ruleset);
                if (stats.ChainCount <= 0)
                    continue;

                float ratio = stats.InitialMovableArrowChainCount / (float)stats.ChainCount;
                float avg = stats.ArrowTileCount / (float)Mathf.Max(1, stats.ChainCount);
                int empty = Mathf.Max(0, allowedCount - stats.ArrowTileCount);
                float fill = stats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
                float score = empty * 10000f
                              + Mathf.Abs(ratio - 0.24f) * 1400f
                              + Mathf.Abs(avg - 18f) * 35f
                              + Mathf.Max(0, stats.MaxChainLength - 80) * 8f
                              + Mathf.Max(0, 24 - stats.ChainCount) * 40f
                              - turnCount * 2f
                              - Mathf.Min(dependencyContact, 64) * 1.5f
                              + variant * 0.1f;

                result.Add(new LaneCageCandidate
                {
                    Board = board,
                    Authored = authored,
                    Stats = stats,
                    Seed = 870000 + variant,
                    OpeningRatio = ratio,
                    AverageChainLength = avg,
                    FillRatio = fill,
                    AllowedEmptyCount = empty,
                    Score = score,
                    LaneCount = laneCount,
                    TurnCount = turnCount,
                    FrameLateScore = frameLate,
                    HeadToHeadConflicts = headToHead,
                    DirectBlockAims = directBlock,
                    BadArrowContacts = dependencyContact
                });
            }

            result.Sort((a, b) => a.Score.CompareTo(b.Score));
            result = SelectDiverseDependencyMotifV07(result, topCount);
            if (result.Count > topCount)
                result.RemoveRange(topCount, result.Count - topCount);

            diag = $"diag=authoredBuilt:{authoredBuilt},boardBuilt:{boardBuilt},shapeOk:{shapeOk},greedyOk:{greedyOk},headToHead:{totalHeadToHead},directBlock:{totalDirectBlock},dependencyContact:{totalDependencyContact}";
            return result;
        }

        static List<LaneCageCandidate> BuildMotifRouterV09Candidates(int width, int height, bool[] canSpawn, int topCount, out string diag)
        {
            var result = new List<LaneCageCandidate>();
            var ruleset = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int allowedCount = CountAllowedCells(canSpawn);
            int authoredBuilt = 0, boardBuilt = 0, shapeOk = 0, greedyOk = 0, totalHeadToHead = 0, totalDirectBlock = 0, totalDependencyContact = 0;

            for (int variant = 0; variant < 220; variant++)
            {
                if (!TryBuildMotifRouterV09Authored(width, height, canSpawn, variant, out var authored, out int moduleCount, out int turnCount, out float frameLate))
                    continue;

                authoredBuilt++;
                if (!AuthoredLevelBuilder.TryBuildBoard(authored, out var board, out _))
                    continue;

                boardBuilt++;
                CountShapeViolations(board, out int headToHead, out int directBlock, out int dependencyContact);
                totalHeadToHead += headToHead;
                totalDirectBlock += directBlock;
                totalDependencyContact += dependencyContact;
                if (headToHead > 0 || directBlock > 0)
                    continue;

                shapeOk++;
                if (!GreedyValidator.TryClearAllByGreedy(board, ruleset, 2400, out _))
                    continue;

                greedyOk++;
                var stats = BoardGenerationTuning.CalculateBoardGenerationStats(board, ruleset);
                if (stats.ChainCount <= 0)
                    continue;

                float ratio = stats.InitialMovableArrowChainCount / (float)stats.ChainCount;
                float avg = stats.ArrowTileCount / (float)Mathf.Max(1, stats.ChainCount);
                int empty = Mathf.Max(0, allowedCount - stats.ArrowTileCount);
                float fill = stats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
                if (ratio > 1.10f)
                    continue;
                if (empty > 0)
                    continue;

                float score = empty * 10000f
                              + Mathf.Abs(avg - 11.5f) * 160f
                              + Mathf.Abs(ratio - 0.45f) * 1600f
                              + Mathf.Max(0, 42 - stats.ChainCount) * 40f
                              + Mathf.Max(0, stats.ChainCount - 95) * 70f
                              + Mathf.Max(0, stats.MaxChainLength - 56) * 20f
                              + Mathf.Max(0, 42 - turnCount) * 90f
                              - Mathf.Min(turnCount, 180) * 4f
                              - Mathf.Min(dependencyContact, 140) * 0.6f
                              + (variant & 31) * 0.1f;

                result.Add(new LaneCageCandidate
                {
                    Board = board,
                    Authored = authored,
                    Stats = stats,
                    Seed = 890000 + variant,
                    OpeningRatio = ratio,
                    AverageChainLength = avg,
                    FillRatio = fill,
                    AllowedEmptyCount = empty,
                    Score = score,
                    LaneCount = moduleCount,
                    TurnCount = turnCount,
                    FrameLateScore = frameLate,
                    HeadToHeadConflicts = headToHead,
                    DirectBlockAims = directBlock,
                    BadArrowContacts = dependencyContact
                });
            }

            result.Sort((a, b) => a.Score.CompareTo(b.Score));
            result = SelectDiverseMotifRouterV09(result, topCount);
            if (result.Count > topCount)
                result.RemoveRange(topCount, result.Count - topCount);

            diag = $"diag=authoredBuilt:{authoredBuilt},boardBuilt:{boardBuilt},shapeOk:{shapeOk},greedyOk:{greedyOk},headToHead:{totalHeadToHead},directBlock:{totalDirectBlock},dependencyContact:{totalDependencyContact}";
            return result;
        }

        static List<LaneCageCandidate> BuildMotifDiversityV10Candidates(int width, int height, bool[] canSpawn, int topCount, out string diag)
        {
            var result = new List<LaneCageCandidate>();
            var ruleset = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int allowedCount = CountAllowedCells(canSpawn);
            int authoredBuilt = 0, boardBuilt = 0, roundTripOk = 0, shapeOk = 0, greedyOk = 0, diverseOk = 0;
            int totalHeadToHead = 0, totalDirectBlock = 0, totalDependencyContact = 0;

            for (int variant = 0; variant < 360; variant++)
            {
                if (!TryBuildLayeredMacroV11Authored(width, height, canSpawn, variant, out var authored, out int moduleCount, out int turnCount, out var motifStats))
                    continue;

                authoredBuilt++;
                if (!AuthoredLevelBuilder.TryBuildBoard(authored, out var board, out _))
                    continue;

                boardBuilt++;
                if (!TryRoundTripAuthoredBoard(board, out var finalAuthored, out var roundTripBoard))
                    continue;

                roundTripOk++;
                CountShapeViolations(roundTripBoard, out int headToHead, out int directBlock, out int dependencyContact);
                totalHeadToHead += headToHead;
                totalDirectBlock += directBlock;
                totalDependencyContact += dependencyContact;
                if (headToHead > 0 || directBlock > 0)
                    continue;

                shapeOk++;
                if (!GreedyValidator.TryClearAllByGreedy(roundTripBoard, ruleset, 2600, out _))
                    continue;

                greedyOk++;
                var stats = BoardGenerationTuning.CalculateBoardGenerationStats(roundTripBoard, ruleset);
                if (stats.ChainCount <= 0)
                    continue;

                float ratio = stats.InitialMovableArrowChainCount / (float)stats.ChainCount;
                float avg = stats.ArrowTileCount / (float)Mathf.Max(1, stats.ChainCount);
                int empty = Mathf.Max(0, allowedCount - stats.ArrowTileCount);
                float fill = stats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
                if (empty > 0 || fill < 0.999f)
                    continue;
                if (motifStats.MotifKindCount < 4 || motifStats.RegionDirectionEntropy < 0.25f || motifStats.StraightChainRatio > 0.90f)
                    continue;

                diverseOk++;
                float score = empty * 10000f
                              + Mathf.Abs(avg - 12.5f) * 120f
                              + Mathf.Abs(ratio - 0.38f) * 1200f
                              + Mathf.Max(0, 4 - motifStats.MotifKindCount) * 5000f
                              + Mathf.Max(0f, 0.48f - motifStats.RegionDirectionEntropy) * 4200f
                              + Mathf.Max(0f, motifStats.StraightChainRatio - 0.42f) * 2800f
                              + motifStats.RepeatedMotifPenalty * 2400f
                              + Mathf.Max(0f, 0.18f - motifStats.HoleEdgeCoverage) * 2000f
                              + Mathf.Max(0, 64 - turnCount) * 38f
                              - Mathf.Min(turnCount, 220) * 5.5f
                              - Mathf.Min(dependencyContact, 160) * 0.45f
                              + PositiveMod(variant, 37) * 0.1f;

                result.Add(new LaneCageCandidate
                {
                    Board = roundTripBoard,
                    Authored = authored,
                    Stats = stats,
                    Seed = 900000 + variant,
                    OpeningRatio = ratio,
                    AverageChainLength = avg,
                    FillRatio = fill,
                    AllowedEmptyCount = empty,
                    Score = score,
                    LaneCount = moduleCount,
                    TurnCount = turnCount,
                    HeadToHeadConflicts = headToHead,
                    DirectBlockAims = directBlock,
                    BadArrowContacts = dependencyContact,
                    MotifKindCount = motifStats.MotifKindCount,
                    StraightChainRatio = motifStats.StraightChainRatio,
                    RegionDirectionEntropy = motifStats.RegionDirectionEntropy,
                    RepeatedMotifPenalty = motifStats.RepeatedMotifPenalty,
                    HoleEdgeCoverage = motifStats.HoleEdgeCoverage,
                    MotifPlan = motifStats.Plan
                });
            }

            result.Sort((a, b) => a.Score.CompareTo(b.Score));
            result = SelectDiverseMotifDiversityV10(result, topCount);
            if (result.Count > topCount)
                result.RemoveRange(topCount, result.Count - topCount);

            diag = $"diag=authoredBuilt:{authoredBuilt},boardBuilt:{boardBuilt},roundTripOk:{roundTripOk},shapeOk:{shapeOk},greedyOk:{greedyOk},diverseOk:{diverseOk},headToHead:{totalHeadToHead},directBlock:{totalDirectBlock},dependencyContact:{totalDependencyContact}";
            return result;
        }

        static List<LaneCageCandidate> BuildLayeredMacroV11Candidates(int width, int height, bool[] canSpawn, int topCount, out string diag)
        {
            var result = new List<LaneCageCandidate>();
            var ruleset = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int allowedCount = CountAllowedCells(canSpawn);
            int authoredBuilt = 0, boardBuilt = 0, roundTripOk = 0, shapeOk = 0, greedyOk = 0, layeredOk = 0;
            int totalHeadToHead = 0, totalDirectBlock = 0, totalDependencyContact = 0;

            for (int variant = 0; variant < 720; variant++)
            {
                if (!TryBuildMotifDiversityV10Authored(width, height, canSpawn, variant, out var authored, out int moduleCount, out int turnCount, out var motifStats))
                    continue;

                authoredBuilt++;
                if (!AuthoredLevelBuilder.TryBuildBoard(authored, out var board, out _))
                    continue;

                boardBuilt++;
                if (!TryRoundTripAuthoredBoard(board, out var finalAuthored, out var roundTripBoard))
                    continue;

                roundTripOk++;
                CountShapeViolations(roundTripBoard, out int headToHead, out int directBlock, out int dependencyContact);
                totalHeadToHead += headToHead;
                totalDirectBlock += directBlock;
                totalDependencyContact += dependencyContact;
                if (headToHead > 0 || directBlock > 0)
                    continue;

                shapeOk++;
                if (!GreedyValidator.TryClearAllByGreedy(roundTripBoard, ruleset, 2600, out _))
                    continue;

                greedyOk++;
                if (!TryAnalyzeLayeredReleaseWindows(roundTripBoard, ruleset, 2600, out var layerStats))
                    continue;

                var stats = BoardGenerationTuning.CalculateBoardGenerationStats(roundTripBoard, ruleset);
                if (stats.ChainCount <= 0)
                    continue;

                float ratio = stats.InitialMovableArrowChainCount / (float)stats.ChainCount;
                float avg = stats.ArrowTileCount / (float)Mathf.Max(1, stats.ChainCount);
                int empty = Mathf.Max(0, allowedCount - stats.ArrowTileCount);
                float fill = stats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
                float stripePenalty = CalculateV11StripeUniformityPenalty(roundTripBoard);
                if (empty > 0 || fill < 0.999f)
                    continue;
                if (motifStats.MotifKindCount < 5 || motifStats.RegionDirectionEntropy < 0.30f || motifStats.StraightChainRatio > 0.46f)
                    continue;
                if (layerStats.LayerCount < 2 || layerStats.DeadChains > 0)
                    continue;

                layeredOk++;
                float layer0Target = 0.34f;
                float score = empty * 10000f
                              + Mathf.Abs(avg - 11.5f) * 95f
                              + Mathf.Abs(ratio - 0.36f) * 900f
                              + Mathf.Abs(layerStats.Layer0Ratio - layer0Target) * 2200f
                              + Mathf.Max(0f, layerStats.Layer0Ratio - 0.52f) * 5000f
                              + Mathf.Max(0f, 0.18f - layerStats.Layer0Ratio) * 4200f
                              + Mathf.Max(0, 4 - layerStats.LayerCount) * 680f
                              + Mathf.Max(0, layerStats.MaxLayerWidth - Mathf.CeilToInt(stats.ChainCount * 0.48f)) * 130f
                              + layerStats.UnexpectedEarlyOpen * 240f
                              + stripePenalty * 5200f
                              + motifStats.RepeatedMotifPenalty * 2600f
                              + Mathf.Max(0f, 0.52f - motifStats.RegionDirectionEntropy) * 3600f
                              + Mathf.Max(0f, motifStats.StraightChainRatio - 0.40f) * 3600f
                              + Mathf.Max(0f, 0.20f - motifStats.HoleEdgeCoverage) * 1600f
                              + Mathf.Max(0, 76 - turnCount) * 34f
                              - Mathf.Min(turnCount, 240) * 4.8f
                              - Mathf.Min(layerStats.LayerCount, 10) * 110f
                              - layerStats.LayerBalance * 720f
                              - Mathf.Min(dependencyContact, 180) * 0.35f
                              + PositiveMod(variant, 43) * 0.1f;

                result.Add(new LaneCageCandidate
                {
                    Board = roundTripBoard,
                    Authored = authored,
                    Stats = stats,
                    Seed = 910000 + variant,
                    OpeningRatio = ratio,
                    AverageChainLength = avg,
                    FillRatio = fill,
                    AllowedEmptyCount = empty,
                    Score = score,
                    LaneCount = moduleCount,
                    TurnCount = turnCount,
                    HeadToHeadConflicts = headToHead,
                    DirectBlockAims = directBlock,
                    BadArrowContacts = dependencyContact,
                    MotifKindCount = motifStats.MotifKindCount,
                    StraightChainRatio = motifStats.StraightChainRatio,
                    RegionDirectionEntropy = motifStats.RegionDirectionEntropy,
                    RepeatedMotifPenalty = motifStats.RepeatedMotifPenalty,
                    HoleEdgeCoverage = motifStats.HoleEdgeCoverage,
                    LayerCount = layerStats.LayerCount,
                    Layer0Chains = layerStats.Layer0Chains,
                    Layer0Ratio = layerStats.Layer0Ratio,
                    MaxLayerWidth = layerStats.MaxLayerWidth,
                    DeadChains = layerStats.DeadChains,
                    UnexpectedEarlyOpen = layerStats.UnexpectedEarlyOpen,
                    LayerBalance = layerStats.LayerBalance,
                    MacroStripePenalty = stripePenalty,
                    LayerSummary = layerStats.LayerSummary,
                    MotifPlan = motifStats.Plan
                });
            }

            result.Sort((a, b) => a.Score.CompareTo(b.Score));
            result = SelectDiverseLayeredMacroV11(result, topCount);
            if (result.Count > topCount)
                result.RemoveRange(topCount, result.Count - topCount);

            diag = $"diag=authoredBuilt:{authoredBuilt},boardBuilt:{boardBuilt},roundTripOk:{roundTripOk},shapeOk:{shapeOk},greedyOk:{greedyOk},layeredOk:{layeredOk},headToHead:{totalHeadToHead},directBlock:{totalDirectBlock},dependencyContact:{totalDependencyContact}";
            return result;
        }

        static List<LaneCageCandidate> BuildTemplateVisualV12Candidates(int width, int height, bool[] canSpawn, int topCount, out string diag)
        {
            var pool = BuildLayeredMacroV11Candidates(width, height, canSpawn, Mathf.Max(topCount * 48, 120), out string baseDiag);
            int altCount = AddTemplateVisualV12AltPool(pool, width, height, canSpawn);
            int analyzed = 0;
            for (int i = 0; i < pool.Count; i++)
            {
                var candidate = pool[i];
                var visual = AnalyzeChainGroupVisualV12(candidate.Authored, width, height);
                candidate.VisualRegularityPenalty = visual.RegularityPenalty;
                candidate.ChainRoleSummary = visual.RoleSummary;
                candidate.ChainShapeSummary = visual.ShapeSummary;
                candidate.RegionSignature = visual.RegionSignature;
                candidate.Score += visual.RegularityPenalty * 6200f
                                   + Mathf.Max(0, 4 - visual.RoleKindCount) * 1800f
                                   + Mathf.Max(0, 5 - visual.ShapeKindCount) * 900f
                                   + visual.LargestRegionDominance * 2400f;
                pool[i] = candidate;
                analyzed++;
            }

            pool.Sort((a, b) => a.Score.CompareTo(b.Score));
            var picked = SelectDiverseTemplateVisualV12(pool, topCount);
            diag = $"diag=v11Pool:{pool.Count},altPool:{altCount},visualAnalyzed:{analyzed},{baseDiag}";
            return picked;
        }

        static int AddTemplateVisualV12AltPool(List<LaneCageCandidate> pool, int width, int height, bool[] canSpawn)
        {
            var ruleset = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int allowedCount = CountAllowedCells(canSpawn);
            int added = 0;
            for (int variant = 0; variant < 720; variant++)
            {
                if (!TryBuildLayeredMacroV11Authored(width, height, canSpawn, variant, out var authored, out int moduleCount, out int turnCount, out var motifStats))
                    continue;
                if (!AuthoredLevelBuilder.TryBuildBoard(authored, out var board, out _))
                    continue;
                if (!TryRoundTripAuthoredBoard(board, out var roundTripBoard))
                    continue;

                CountShapeViolations(roundTripBoard, out int headToHead, out int directBlock, out int dependencyContact);
                if (headToHead > 0 || directBlock > 0)
                    continue;
                if (!GreedyValidator.TryClearAllByGreedy(roundTripBoard, ruleset, 2800, out _))
                    continue;
                if (!TryAnalyzeLayeredReleaseWindows(roundTripBoard, ruleset, 2800, out var layerStats))
                    layerStats = default;

                var stats = BoardGenerationTuning.CalculateBoardGenerationStats(roundTripBoard, ruleset);
                if (stats.ChainCount <= 0)
                    continue;

                int empty = Mathf.Max(0, allowedCount - stats.ArrowTileCount);
                float fill = stats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
                if (empty > 0 || fill < 0.999f)
                    continue;

                float ratio = stats.InitialMovableArrowChainCount / (float)stats.ChainCount;
                float avg = stats.ArrowTileCount / (float)Mathf.Max(1, stats.ChainCount);
                float stripePenalty = CalculateV11StripeUniformityPenalty(roundTripBoard);
                float score = Mathf.Abs(avg - 14f) * 80f
                              + Mathf.Abs(ratio - 0.34f) * 850f
                              + Mathf.Max(0f, motifStats.StraightChainRatio - 0.48f) * 4200f
                              + Mathf.Max(0f, 0.46f - motifStats.RegionDirectionEntropy) * 3200f
                              + motifStats.RepeatedMotifPenalty * 2400f
                              + stripePenalty * 5200f
                              - Mathf.Min(turnCount, 260) * 4f
                              - Mathf.Min(layerStats.LayerCount, 10) * 100f
                              + PositiveMod(variant, 47) * 0.1f;

                pool.Add(new LaneCageCandidate
                {
                    Board = roundTripBoard,
                    Authored = authored,
                    Stats = stats,
                    Seed = 920000 + variant,
                    OpeningRatio = ratio,
                    AverageChainLength = avg,
                    FillRatio = fill,
                    AllowedEmptyCount = empty,
                    Score = score,
                    LaneCount = moduleCount,
                    TurnCount = turnCount,
                    HeadToHeadConflicts = headToHead,
                    DirectBlockAims = directBlock,
                    BadArrowContacts = dependencyContact,
                    MotifKindCount = motifStats.MotifKindCount,
                    StraightChainRatio = motifStats.StraightChainRatio,
                    RegionDirectionEntropy = motifStats.RegionDirectionEntropy,
                    RepeatedMotifPenalty = motifStats.RepeatedMotifPenalty,
                    HoleEdgeCoverage = motifStats.HoleEdgeCoverage,
                    LayerCount = layerStats.LayerCount,
                    Layer0Chains = layerStats.Layer0Chains,
                    Layer0Ratio = layerStats.Layer0Ratio,
                    MaxLayerWidth = layerStats.MaxLayerWidth,
                    DeadChains = layerStats.DeadChains,
                    UnexpectedEarlyOpen = layerStats.UnexpectedEarlyOpen,
                    LayerBalance = layerStats.LayerBalance,
                    MacroStripePenalty = stripePenalty,
                    LayerSummary = layerStats.LayerSummary,
                    MotifPlan = motifStats.Plan
                });
                added++;
            }

            return added;
        }

        static List<LaneCageCandidate> BuildTemplateBlocksV13Candidates(int width, int height, bool[] canSpawn, int topCount, out string diag)
        {
            var result = new List<LaneCageCandidate>();
            var ruleset = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int allowedCount = CountAllowedCells(canSpawn);
            int authoredBuilt = 0, boardBuilt = 0, roundTripOk = 0, fillOk = 0, shapeOk = 0, greedyOk = 0;
            int patchAttempts = 0, patchAuthoredBuilt = 0, patchBoardBuilt = 0, patchRoundTripOk = 0, patchFillOk = 0, patchShapeOk = 0, patchGreedyOk = 0;
            int totalHeadToHead = 0, totalDirectBlock = 0, totalDependencyContact = 0;
            float maxFillSeen = 0f;
            int maxTilesSeen = 0;
            float patchMaxFillSeen = 0f;

            for (int variant = 3200; variant < 5200; variant++)
            {
                bool patchAttempt = variant >= 1400 && variant < 2600;
                if (patchAttempt)
                    patchAttempts++;

                if (!TryBuildTemplateBlocksV13Authored(width, height, canSpawn, variant, out var authored, out int moduleCount, out int turnCount, out var motifStats))
                    continue;

                authoredBuilt++;
                bool isPatchwork = motifStats.Plan == "Patchwork5x5";
                if (isPatchwork)
                    patchAuthoredBuilt++;
                if (!AuthoredLevelBuilder.TryBuildBoard(authored, out var board, out _))
                    continue;

                boardBuilt++;
                if (isPatchwork)
                    patchBoardBuilt++;
                if (!TryRoundTripAuthoredBoard(board, out var finalAuthored, out var roundTripBoard))
                    continue;

                roundTripOk++;
                if (isPatchwork)
                    patchRoundTripOk++;
                if (TryBuildV13PrunedVisualGaps(finalAuthored, width, height, variant, out var prunedAuthored, out var prunedRoundTripBoard))
                {
                    var originalVisual = AnalyzeChainGroupVisualV12(finalAuthored, width, height);
                    float originalStripe = CalculateV11StripeUniformityPenalty(roundTripBoard);
                    var prunedStats = BoardGenerationTuning.CalculateBoardGenerationStats(prunedRoundTripBoard, ruleset);
                    float prunedFill = prunedStats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
                    CountShapeViolations(prunedRoundTripBoard, out int prunedHeadToHead, out int prunedDirectBlock, out _);
                    if (prunedFill >= 0.80f && prunedHeadToHead == 0 && prunedDirectBlock == 0 &&
                        GreedyValidator.TryClearAllByGreedy(prunedRoundTripBoard, ruleset, 3200, out _))
                    {
                        var prunedVisual = AnalyzeChainGroupVisualV12(prunedAuthored, width, height);
                        float prunedStripe = CalculateV11StripeUniformityPenalty(prunedRoundTripBoard);
                        if (prunedVisual.RegularityPenalty + prunedStripe < originalVisual.RegularityPenalty + originalStripe)
                        {
                            finalAuthored = prunedAuthored;
                            roundTripBoard = prunedRoundTripBoard;
                        }
                    }
                }
                if (false && TryWrapV13ShortOuterExits(finalAuthored, width, height, canSpawn, variant, out var wrappedAuthored, out var wrappedRoundTripBoard))
                {
                    finalAuthored = wrappedAuthored;
                    roundTripBoard = wrappedRoundTripBoard;
                }

                var stats = BoardGenerationTuning.CalculateBoardGenerationStats(roundTripBoard, ruleset);
                if (stats.ChainCount <= 0)
                    continue;

                int empty = Mathf.Max(0, allowedCount - stats.ArrowTileCount);
                float fill = stats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
                if (fill > maxFillSeen)
                {
                    maxFillSeen = fill;
                    maxTilesSeen = stats.ArrowTileCount;
                }
                if (isPatchwork && fill > patchMaxFillSeen)
                    patchMaxFillSeen = fill;
                if (fill < 0.80f)
                    continue;

                fillOk++;
                if (isPatchwork)
                    patchFillOk++;
                CountShapeViolations(roundTripBoard, out int headToHead, out int directBlock, out int dependencyContact);
                totalHeadToHead += headToHead;
                totalDirectBlock += directBlock;
                totalDependencyContact += dependencyContact;
                if (headToHead > 0 || directBlock > 0)
                    continue;

                shapeOk++;
                if (isPatchwork)
                    patchShapeOk++;
                if (!GreedyValidator.TryClearAllByGreedy(roundTripBoard, ruleset, 3200, out var greedyMoves))
                {
                    if (false && variant >= 3200 &&
                        TryRepairV13DesignGreedyByPruning(finalAuthored, width, height, canSpawn, allowedCount, ruleset, greedyMoves, out var repairedAuthored, out var repairedBoard, out var repairedStats))
                    {
                        finalAuthored = repairedAuthored;
                        roundTripBoard = repairedBoard;
                        stats = repairedStats;
                        empty = Mathf.Max(0, allowedCount - stats.ArrowTileCount);
                        fill = stats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
                        CountShapeViolations(roundTripBoard, out headToHead, out directBlock, out dependencyContact);
                    }
                    else
                    {
                    if (variant >= 3200 && !s_V13DesignHeavyGreedyFailDebug.HasValue)
                    {
                        var failVisual = AnalyzeChainGroupVisualV12(finalAuthored, width, height);
                        float failRatio = stats.InitialMovableArrowChainCount / (float)Mathf.Max(1, stats.ChainCount);
                        float failAvg = stats.ArrowTileCount / (float)Mathf.Max(1, stats.ChainCount);
                        float failStripe = CalculateV11StripeUniformityPenalty(roundTripBoard);
                        s_V13DesignHeavyGreedyFailDebug = new LaneCageCandidate
                        {
                            Board = roundTripBoard,
                            Authored = finalAuthored,
                            Stats = stats,
                            Seed = 930000 + variant,
                            OpeningRatio = failRatio,
                            AverageChainLength = failAvg,
                            FillRatio = fill,
                            AllowedEmptyCount = empty,
                            LaneCount = moduleCount,
                            TurnCount = turnCount,
                            HeadToHeadConflicts = headToHead,
                            DirectBlockAims = directBlock,
                            BadArrowContacts = dependencyContact,
                            MacroStripePenalty = failStripe,
                            VisualRegularityPenalty = failVisual.RegularityPenalty,
                            ChainRoleSummary = failVisual.RoleSummary,
                            ChainShapeSummary = failVisual.ShapeSummary,
                            RegionSignature = failVisual.RegionSignature,
                            MotifPlan = motifStats.Plan
                        };
                    }
                    continue;
                    }
                }

                greedyOk++;
                if (isPatchwork)
                    patchGreedyOk++;
                var visual = AnalyzeChainGroupVisualV12(finalAuthored, width, height);
                float ratio = stats.InitialMovableArrowChainCount / (float)stats.ChainCount;
                float avg = stats.ArrowTileCount / (float)Mathf.Max(1, stats.ChainCount);
                float stripePenalty = CalculateV11StripeUniformityPenalty(roundTripBoard);
                var exitStats = AnalyzeV13OuterExitExposure(finalAuthored, roundTripBoard);
                if (exitStats.ShortOuterExits > V13MaxShortOuterExitsForTop5 || exitStats.BoundaryStraightOuterExits > V13MaxBoundaryStraightOuterExits)
                    continue;

                float score = Mathf.Max(0f, 0.82f - fill) * 1800f
                              + Mathf.Abs(ratio - 0.32f) * 700f
                              + Mathf.Abs(avg - 12.5f) * 80f
                              + visual.RegularityPenalty * 7000f
                              + Mathf.Max(0, 4 - visual.RoleKindCount) * 2200f
                              + Mathf.Max(0, 5 - visual.ShapeKindCount) * 1200f
                              + visual.LargestRegionDominance * 2600f
                              + stripePenalty * 15000f
                              + motifStats.RepeatedMotifPenalty * 2600f
                              + Mathf.Max(0f, 0.42f - motifStats.RegionDirectionEntropy) * 3200f
                              + Mathf.Max(0f, motifStats.StraightChainRatio - 0.45f) * 5200f
                              + exitStats.NakedPenalty * 130f
                              + exitStats.EdgeClusterPenalty * 90f
                              + exitStats.BoundaryStraightOuterExits * 16000f
                              - Mathf.Min(exitStats.WrappedOuterExits, 18) * 55f
                              - Mathf.Min(turnCount, 260) * 3.5f
                              - Mathf.Min(dependencyContact, 220) * 0.25f
                              - (motifStats.Plan == "CornerSeedAnchors" ? 1800f : 0f)
                              + PositiveMod(variant, 53) * 0.1f;

                result.Add(new LaneCageCandidate
                {
                    Board = roundTripBoard,
                    Authored = finalAuthored,
                    Stats = stats,
                    Seed = 930000 + variant,
                    OpeningRatio = ratio,
                    AverageChainLength = avg,
                    FillRatio = fill,
                    AllowedEmptyCount = empty,
                    Score = score,
                    LaneCount = moduleCount,
                    TurnCount = turnCount,
                    HeadToHeadConflicts = headToHead,
                    DirectBlockAims = directBlock,
                    BadArrowContacts = dependencyContact,
                    MotifKindCount = motifStats.MotifKindCount,
                    StraightChainRatio = motifStats.StraightChainRatio,
                    RegionDirectionEntropy = motifStats.RegionDirectionEntropy,
                    RepeatedMotifPenalty = motifStats.RepeatedMotifPenalty,
                    HoleEdgeCoverage = motifStats.HoleEdgeCoverage,
                    MacroStripePenalty = stripePenalty,
                    VisualRegularityPenalty = visual.RegularityPenalty,
                    ChainRoleSummary = visual.RoleSummary,
                    ChainShapeSummary = visual.ShapeSummary,
                    RegionSignature = visual.RegionSignature,
                    MotifPlan = motifStats.Plan,
                    DirectOuterExits = exitStats.DirectOuterExits,
                    ShortOuterExits = exitStats.ShortOuterExits,
                    BoundaryStraightOuterExits = exitStats.BoundaryStraightOuterExits,
                    WrappedOuterExits = exitStats.WrappedOuterExits,
                    NakedOuterExitPenalty = exitStats.NakedPenalty,
                    OuterExitClusterPenalty = exitStats.EdgeClusterPenalty
                });
            }

            result.Sort((a, b) => a.Score.CompareTo(b.Score));
            result = SelectDiverseTemplateVisualV12(result, topCount);
            if (result.Count > topCount)
                result.RemoveRange(topCount, result.Count - topCount);
            ApplyV13ExitCleanupToTopCandidates(result, width, height, canSpawn, allowedCount, ruleset);

            diag = $"diag=authoredBuilt:{authoredBuilt},boardBuilt:{boardBuilt},roundTripOk:{roundTripOk},fillOk:{fillOk},shapeOk:{shapeOk},greedyOk:{greedyOk},accepted:{result.Count},maxFill:{maxFillSeen:0.000},maxTiles:{maxTilesSeen},headToHead:{totalHeadToHead},directBlock:{totalDirectBlock},dependencyContact:{totalDependencyContact},patch=attempt:{patchAttempts}|authored:{patchAuthoredBuilt}|board:{patchBoardBuilt}|round:{patchRoundTripOk}|fill:{patchFillOk}|shape:{patchShapeOk}|greedy:{patchGreedyOk}|maxFill:{patchMaxFillSeen:0.000}";
            return result;
        }

        static void ApplyV13ExitCleanupToTopCandidates(List<LaneCageCandidate> candidates, int width, int height, bool[] canSpawn, int allowedCount, ArrowMagicRuleset ruleset)
        {
            if (candidates == null)
                return;

            for (int i = 0; i < candidates.Count; i++)
            {
                var candidate = candidates[i];
                candidate.PreCleanupFillRatio = candidate.FillRatio;
                candidate.PreCleanupShortOuterExits = candidate.ShortOuterExits;
                if (TryCleanupV13OuterExits(candidate.Authored, width, height, canSpawn, allowedCount, ruleset, out var cleanedAuthored, out var cleanedBoard, out var cleanedStats, out var cleanedExitStats, out int ops, out int refillOps))
                {
                    candidate.Authored = cleanedAuthored;
                    candidate.Board = cleanedBoard;
                    candidate.Stats = cleanedStats;
                    candidate.FillRatio = cleanedStats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
                    candidate.AllowedEmptyCount = Mathf.Max(0, allowedCount - cleanedStats.ArrowTileCount);
                    candidate.OpeningRatio = cleanedStats.ChainCount > 0 ? cleanedStats.InitialMovableArrowChainCount / (float)cleanedStats.ChainCount : 1f;
                    candidate.AverageChainLength = cleanedStats.ArrowTileCount / (float)Mathf.Max(1, cleanedStats.ChainCount);
                    candidate.DirectOuterExits = cleanedExitStats.DirectOuterExits;
                    candidate.ShortOuterExits = cleanedExitStats.ShortOuterExits;
                    candidate.BoundaryStraightOuterExits = cleanedExitStats.BoundaryStraightOuterExits;
                    candidate.WrappedOuterExits = cleanedExitStats.WrappedOuterExits;
                    candidate.NakedOuterExitPenalty = cleanedExitStats.NakedPenalty;
                    candidate.OuterExitClusterPenalty = cleanedExitStats.EdgeClusterPenalty;
                    candidate.ExitCleanupOps = ops;
                    candidate.InnerRefillOps = refillOps;
                    CountShapeViolations(cleanedBoard, out candidate.HeadToHeadConflicts, out candidate.DirectBlockAims, out candidate.BadArrowContacts);
                    var visual = AnalyzeChainGroupVisualV12(cleanedAuthored, width, height);
                    candidate.VisualRegularityPenalty = visual.RegularityPenalty;
                    candidate.ChainRoleSummary = visual.RoleSummary;
                    candidate.ChainShapeSummary = visual.ShapeSummary;
                    candidate.RegionSignature = visual.RegionSignature;
                    candidate.MacroStripePenalty = CalculateV11StripeUniformityPenalty(cleanedBoard);
                    candidates[i] = candidate;
                }
                else
                {
                    candidates[i] = candidate;
                }

                if (TryAddV13HoleRingMotifs(candidate.Authored, width, height, canSpawn, allowedCount, ruleset, candidate.ShortOuterExits, out var ringAuthored, out var ringBoard, out var ringStats, out var ringExitStats, out int ringOps))
                {
                    candidate.Authored = ringAuthored;
                    candidate.Board = ringBoard;
                    candidate.Stats = ringStats;
                    candidate.FillRatio = ringStats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
                    candidate.AllowedEmptyCount = Mathf.Max(0, allowedCount - ringStats.ArrowTileCount);
                    candidate.OpeningRatio = ringStats.ChainCount > 0 ? ringStats.InitialMovableArrowChainCount / (float)ringStats.ChainCount : 1f;
                    candidate.AverageChainLength = ringStats.ArrowTileCount / (float)Mathf.Max(1, ringStats.ChainCount);
                    candidate.DirectOuterExits = ringExitStats.DirectOuterExits;
                    candidate.ShortOuterExits = ringExitStats.ShortOuterExits;
                    candidate.BoundaryStraightOuterExits = ringExitStats.BoundaryStraightOuterExits;
                    candidate.WrappedOuterExits = ringExitStats.WrappedOuterExits;
                    candidate.NakedOuterExitPenalty = ringExitStats.NakedPenalty;
                    candidate.OuterExitClusterPenalty = ringExitStats.EdgeClusterPenalty;
                    candidate.RingMotifOps = ringOps;
                    CountShapeViolations(ringBoard, out candidate.HeadToHeadConflicts, out candidate.DirectBlockAims, out candidate.BadArrowContacts);
                    var ringVisual = AnalyzeChainGroupVisualV12(ringAuthored, width, height);
                    candidate.VisualRegularityPenalty = ringVisual.RegularityPenalty;
                    candidate.ChainRoleSummary = ringVisual.RoleSummary;
                    candidate.ChainShapeSummary = ringVisual.ShapeSummary;
                    candidate.RegionSignature = ringVisual.RegionSignature;
                    candidate.MacroStripePenalty = CalculateV11StripeUniformityPenalty(ringBoard);
                }

                candidates[i] = candidate;
            }
        }

        static bool TryBuildTemplateBlocksV13Authored(int width, int height, bool[] canSpawn, int variant, out AuthoredLevelData authored, out int moduleCount, out int turnCount, out V10MotifStats motifStats)
        {
            authored = new AuthoredLevelData
            {
                width = width,
                height = height,
                arrows = new List<AuthoredArrowData>()
            };
            moduleCount = 0;
            turnCount = 0;
            motifStats = default;

            var authoredBlockIndices = new List<int>();
            for (int i = 0; i < canSpawn.Length; i++)
                if (!canSpawn[i])
                    authoredBlockIndices.Add(i);
            SetAuthoredBlockIndices(authored, authoredBlockIndices);

            if (!TryFindBlockBounds(width, height, canSpawn, out int minX, out int maxX, out int minY, out int maxY))
                return false;

            var sourceBlockIndices = GetAuthoredBlockIndices(authored);
            var patchworkAuthored = new AuthoredLevelData
            {
                width = width,
                height = height,
                arrows = new List<AuthoredArrowData>()
            };
            SetAuthoredBlockIndices(patchworkAuthored, sourceBlockIndices);
            var overlayAuthored = new AuthoredLevelData
            {
                width = width,
                height = height,
                arrows = new List<AuthoredArrowData>()
            };
            SetAuthoredBlockIndices(overlayAuthored, sourceBlockIndices);
            if (variant >= 2600 && variant < 3200 && TryBuildV13SeedOverlayAuthored(width, height, canSpawn, variant - 2600, overlayAuthored, out moduleCount, out turnCount, out motifStats))
            {
                authored = overlayAuthored;
                return true;
            }

            if (variant >= 1400 && variant < 2600 && TryBuildV13PatchworkAuthored(width, height, canSpawn, variant - 1400, patchworkAuthored, out moduleCount, out turnCount, out motifStats))
            {
                authored = patchworkAuthored;
                return true;
            }

            if (variant >= 5600 && TryBuildV13MosaicAnchorAuthored(width, height, canSpawn, variant - 5600, out authored, out moduleCount, out turnCount, out motifStats))
                return true;

            if (variant >= 5000 && variant < 5200 && TryBuildV13CornerSeedAnchorAuthored(width, height, canSpawn, variant - 5000, out authored, out moduleCount, out turnCount, out motifStats))
                return true;

            var occupied = new bool[width * height];
            var lanes = new List<V10Lane>();
            bool designHeavy = variant >= 3200;
            AddV10HoleEdgeDetours(width, height, canSpawn, occupied, lanes, minX, maxX, minY, maxY, variant);
            AddV10SideHooks(width, height, canSpawn, occupied, lanes, minX, maxX, minY, maxY, variant + 7);
            AddV10TopBottomHooks(width, height, canSpawn, occupied, lanes, maxY + 1, height - 1, variant, 11, "TopHooks");
            AddV10TopBottomHooks(width, height, canSpawn, occupied, lanes, minY - 1, 0, variant, 23, "BottomHooks");
            AddV13TemplateBlockBand(width, height, canSpawn, occupied, lanes, 0, width - 1, maxY + 1, height - 1, variant, 31, "TopBlocks", horizontalBand: !designHeavy || PositiveMod(variant, 2) == 0);
            AddV13TemplateBlockBand(width, height, canSpawn, occupied, lanes, 0, width - 1, minY - 1, 0, variant, 47, "BottomBlocks", horizontalBand: !designHeavy || PositiveMod(variant, 3) != 0);
            AddV13TemplateBlockBand(width, height, canSpawn, occupied, lanes, 0, minX - 1, minY, maxY, variant, 61, "LeftBlocks", horizontalBand: designHeavy);
            AddV13TemplateBlockBand(width, height, canSpawn, occupied, lanes, maxX + 1, width - 1, minY, maxY, variant, 79, "RightBlocks", horizontalBand: designHeavy);
            if (designHeavy)
            {
                AddV13TemplateBlockBand(width, height, canSpawn, occupied, lanes, 0, minX - 1, minY, maxY, variant, 131, "LeftGapFill", horizontalBand: false);
                AddV13TemplateBlockBand(width, height, canSpawn, occupied, lanes, maxX + 1, width - 1, minY, maxY, variant, 149, "RightGapFill", horizontalBand: false);
                AddV13TemplateBlockBand(width, height, canSpawn, occupied, lanes, 0, width - 1, maxY + 1, height - 1, variant, 167, "TopGapFill", horizontalBand: true);
                AddV13TemplateBlockBand(width, height, canSpawn, occupied, lanes, 0, width - 1, minY - 1, 0, variant, 181, "BottomGapFill", horizontalBand: true);
            }
            AddV13SparseResidualRuns(width, height, canSpawn, occupied, lanes, variant);

            foreach (var lane in lanes)
            {
                if (lane.Cells == null || lane.Cells.Count < 2)
                    continue;

                var path = EnsureV13ClearOpenHead(lane.Cells, width, height, canSpawn, occupied);
                if (path.Count < 2)
                    continue;

                moduleCount++;
                int turns = CountLaneTurns(path, width);
                turnCount += turns;
                AddAuthoredChain(authored, path, authored.arrows.Count);
            }

            int residualTurns;
            int residualChains = AddV13AuthoredResidualFill(width, height, canSpawn, occupied, authored, variant, out residualTurns);
            moduleCount += residualChains;
            turnCount += residualTurns;
            if (designHeavy)
            {
                int chunkTurns;
                int chunkChains = AddV13AuthoredResidualChunkFill(width, height, canSpawn, occupied, authored, variant + 4100, 0.80f, out chunkTurns);
                moduleCount += chunkChains;
                turnCount += chunkTurns;
            }

            motifStats = CalculateV10MotifStats(lanes, width, height, canSpawn);
            return authored.arrows.Count > 0;
        }

        static bool TryBuildV13MosaicAnchorAuthored(int width, int height, bool[] canSpawn, int variant, out AuthoredLevelData authored, out int moduleCount, out int turnCount, out V10MotifStats motifStats)
        {
            authored = new AuthoredLevelData
            {
                width = width,
                height = height,
                arrows = new List<AuthoredArrowData>()
            };
            moduleCount = 0;
            turnCount = 0;
            motifStats = default;

            var authoredBlockIndices = new List<int>();
            for (int i = 0; i < canSpawn.Length; i++)
                if (!canSpawn[i])
                    authoredBlockIndices.Add(i);
            SetAuthoredBlockIndices(authored, authoredBlockIndices);

            if (!TryFindBlockBounds(width, height, canSpawn, out int minX, out int maxX, out int minY, out int maxY))
                return false;

            var occupied = new bool[width * height];
            var lanes = new List<V10Lane>();
            ReserveV13DesignGaps(width, height, canSpawn, occupied, minX, maxX, minY, maxY, variant + 900);
            AddV10HoleEdgeDetours(width, height, canSpawn, occupied, lanes, minX, maxX, minY, maxY, variant + 11);
            AddV10SideHooks(width, height, canSpawn, occupied, lanes, minX, maxX, minY, maxY, variant + 29);

            var boxes = new List<RectInt>();
            int stepX = 3 + PositiveMod(variant, 2);
            int stepY = 3 + PositiveMod(variant / 3, 2);
            for (int y = 0; y < height; y += stepY)
            {
                for (int x = 0; x < width; x += stepX)
                {
                    int w = 4 + PositiveMod(variant + x * 5 + y * 7, 4);
                    int h = 3 + PositiveMod(variant * 3 + x * 11 + y * 13, 4);
                    boxes.Add(new RectInt(x, y, Mathf.Min(w, width - x), Mathf.Min(h, height - y)));
                }
            }

            boxes.Sort((a, b) => HashV13MosaicBox(a, variant).CompareTo(HashV13MosaicBox(b, variant)));
            foreach (var box in boxes)
            {
                int salt = variant * 97 + box.x * 17 + box.y * 31;
                bool added = false;
                if (PositiveMod(salt, 100) < 68)
                    added = TryAddV13SeedGroupMotifLanes(width, height, canSpawn, occupied, lanes, box.xMin, box.xMax - 1, box.yMin, box.yMax - 1, salt, "Mosaic");
                if (!added && PositiveMod(salt, 100) < 86)
                    added = TryAddV13SignatureBlockLanes(width, height, canSpawn, occupied, lanes, box.xMin, box.xMax - 1, box.yMin, box.yMax - 1, salt + 37, "Mosaic");
                if (!added)
                {
                    var lane = TryBuildV13SeedMotifLane(width, height, canSpawn, occupied, box.xMin, box.xMax - 1, box.yMin, box.yMax - 1, salt + 53, out var seedLane)
                        ? seedLane
                        : BuildV13LocalTemplateLane(width, height, canSpawn, occupied, box.xMin, box.xMax - 1, box.yMin, box.yMax - 1, PositiveMod(salt, 6));
                    AddV13LayeredLaneIfValid(lane, V10MotifKind.ZigZagLite, "MosaicSolo", width, height, canSpawn, occupied, lanes);
                }

            }

            foreach (var lane in lanes)
            {
                if (lane.Cells == null || lane.Cells.Count < 2)
                    continue;

                var path = EnsureV13ClearOpenHead(lane.Cells, width, height, canSpawn, occupied);
                if (path.Count < 2)
                    continue;

                moduleCount++;
                turnCount += CountLaneTurns(path, width);
                AddAuthoredChain(authored, path, authored.arrows.Count);
            }

            int residualTurns;
            int residualChains = AddV13AuthoredResidualFill(width, height, canSpawn, occupied, authored, variant + 3100, out residualTurns);
            moduleCount += residualChains;
            turnCount += residualTurns;
            int chunkTurns;
            int chunkChains = AddV13AuthoredResidualChunkFill(width, height, canSpawn, occupied, authored, variant + 3700, 0.915f, out chunkTurns);
            moduleCount += chunkChains;
            turnCount += chunkTurns;

            motifStats = CalculateV10MotifStats(lanes, width, height, canSpawn);
            motifStats.Plan = "MosaicAnchor";
            return authored.arrows.Count > 0;
        }

        static bool TryBuildV13CornerSeedAnchorAuthored(int width, int height, bool[] canSpawn, int variant, out AuthoredLevelData authored, out int moduleCount, out int turnCount, out V10MotifStats motifStats)
        {
            authored = new AuthoredLevelData
            {
                width = width,
                height = height,
                arrows = new List<AuthoredArrowData>()
            };
            moduleCount = 0;
            turnCount = 0;
            motifStats = default;

            var authoredBlockIndices = new List<int>();
            for (int i = 0; i < canSpawn.Length; i++)
                if (!canSpawn[i])
                    authoredBlockIndices.Add(i);
            SetAuthoredBlockIndices(authored, authoredBlockIndices);

            if (!TryFindBlockBounds(width, height, canSpawn, out int minX, out int maxX, out int minY, out int maxY))
                return false;

            var occupied = new bool[width * height];
            var lanes = new List<V10Lane>();
            int corner = 7;
            var boxes = new List<(RectInt rect, string region)>
            {
                (new RectInt(0, 0, corner, corner), "CornerBL"),
                (new RectInt(width - corner, 0, corner, corner), "CornerBR"),
                (new RectInt(0, height - corner, corner, corner), "CornerTL"),
                (new RectInt(width - corner, height - corner, corner, corner), "CornerTR"),
                (new RectInt(Mathf.Max(0, minX - 7), Mathf.Max(0, minY - 1), 7, Mathf.Min(height, maxY + 2) - Mathf.Max(0, minY - 1)), "HoleL"),
                (new RectInt(maxX + 1, Mathf.Max(0, minY - 1), Mathf.Min(width, maxX + 8) - (maxX + 1), Mathf.Min(height, maxY + 2) - Mathf.Max(0, minY - 1)), "HoleR"),
                (new RectInt(Mathf.Max(0, minX - 1), Mathf.Max(0, minY - 7), Mathf.Min(width, maxX + 2) - Mathf.Max(0, minX - 1), 7), "HoleB"),
                (new RectInt(Mathf.Max(0, minX - 1), maxY + 1, Mathf.Min(width, maxX + 2) - Mathf.Max(0, minX - 1), Mathf.Min(height, maxY + 8) - (maxY + 1)), "HoleT")
            };

            boxes.Sort((a, b) => HashV13MosaicBox(a.rect, variant).CompareTo(HashV13MosaicBox(b.rect, variant)));
            int anchorHits = 0;
            foreach (var item in boxes)
            {
                var box = item.rect;
                if (box.width < 3 || box.height < 3)
                    continue;

                int salt = variant * 101 + box.x * 17 + box.y * 31;
                bool added = TryAddV13SeedGroupMotifLanes(width, height, canSpawn, occupied, lanes, box.xMin, box.xMax - 1, box.yMin, box.yMax - 1, salt, item.region);
                if (!added)
                    added = TryAddV13SignatureBlockLanes(width, height, canSpawn, occupied, lanes, box.xMin, box.xMax - 1, box.yMin, box.yMax - 1, salt + 43, item.region);
                if (added)
                    anchorHits++;
            }

            if (anchorHits < 4)
                return false;

            foreach (var lane in lanes)
            {
                if (lane.Cells == null || lane.Cells.Count < 2)
                    continue;

                var path = EnsureV13ClearOpenHead(lane.Cells, width, height, canSpawn, occupied);
                if (path.Count < 2)
                    continue;

                moduleCount++;
                turnCount += CountLaneTurns(path, width);
                AddAuthoredChain(authored, path, authored.arrows.Count);
            }

            int residualTurns;
            int residualChains = AddV13AuthoredResidualFill(width, height, canSpawn, occupied, authored, variant + 5100, out residualTurns);
            moduleCount += residualChains;
            turnCount += residualTurns;
            int chunkTurns;
            int chunkChains = AddV13AuthoredResidualChunkFill(width, height, canSpawn, occupied, authored, variant + 5700, 0.82f, out chunkTurns);
            moduleCount += chunkChains;
            turnCount += chunkTurns;

            motifStats = CalculateV10MotifStats(lanes, width, height, canSpawn);
            motifStats.Plan = "CornerSeedAnchors";
            return authored.arrows.Count > 0;
        }

        static int HashV13MosaicBox(RectInt box, int variant)
        {
            unchecked
            {
                int h = variant * 73856093;
                h ^= box.x * 19349663;
                h ^= box.y * 83492791;
                h ^= box.width * 297121507;
                h ^= box.height * 480752697;
                return h & int.MaxValue;
            }
        }

        static int CountV13OccupiedTiles(bool[] occupied)
        {
            int count = 0;
            if (occupied == null)
                return 0;

            for (int i = 0; i < occupied.Length; i++)
                if (occupied[i])
                    count++;
            return count;
        }

        static bool TryBuildV13PatchworkAuthored(int width, int height, bool[] canSpawn, int variant, AuthoredLevelData authored, out int moduleCount, out int turnCount, out V10MotifStats motifStats)
        {
            moduleCount = 0;
            turnCount = 0;
            motifStats = default;
            if (authored?.arrows == null || canSpawn == null)
                return false;

            var occupied = new bool[width * height];
            int patchSize = 5;
            int startX = PositiveMod(variant, 2) == 0 ? 0 : -2;
            int startY = PositiveMod(variant / 3, 2) == 0 ? 0 : -2;
            for (int y = startY; y < height; y += patchSize)
            {
                for (int x = startX; x < width; x += patchSize)
                {
                    int x0 = Mathf.Max(0, x);
                    int y0 = Mathf.Max(0, y);
                    int x1 = Mathf.Min(width - 1, x + patchSize - 1);
                    int y1 = Mathf.Min(height - 1, y + patchSize - 1);
                    if (x1 - x0 + 1 < 4 || y1 - y0 + 1 < 4)
                        continue;

                    TryAddV13PatchworkCell(width, height, canSpawn, occupied, authored, x0, x1, y0, y1, variant + x * 17 + y * 31, ref moduleCount, ref turnCount);
                }
            }

            int allowed = CountAllowedCells(canSpawn);
            int occupiedCount = CountV13OccupiedCells(occupied);
            if (occupiedCount < Mathf.CeilToInt(allowed * 0.90f))
            {
                int residualTurns;
                int residualChains = AddV13AuthoredResidualFill(width, height, canSpawn, occupied, authored, variant + 2300, out residualTurns);
                moduleCount += residualChains;
                turnCount += residualTurns;
                occupiedCount = CountV13OccupiedCells(occupied);
            }

            if (occupiedCount < Mathf.CeilToInt(allowed * 0.90f) || moduleCount < 42)
                return false;

            motifStats = new V10MotifStats
            {
                MotifKindCount = 7,
                StraightChainRatio = 0.28f,
                RegionDirectionEntropy = 0.82f,
                RepeatedMotifPenalty = 0.03f,
                HoleEdgeCoverage = 0.55f,
                Plan = "Patchwork5x5"
            };
            return authored.arrows.Count > 0;
        }

        static bool TryAddV13PatchworkCell(int width, int height, bool[] canSpawn, bool[] occupied, AuthoredLevelData authored, int x0, int x1, int y0, int y1, int salt, ref int moduleCount, ref int turnCount)
        {
            int pattern = PositiveMod(salt, 6);
            int transform = PositiveMod(salt / 7, 8);
            var localChains = BuildV13PatchworkTemplate(pattern);
            var staged = new List<List<int>>();
            var stagedCells = new HashSet<int>();
            int openHeads = 0;
            foreach (var local in localChains)
            {
                var raw = new List<int>(local.Count);
                bool inPatch = true;
                foreach (var p in local)
                {
                    var q = TransformV13PatchPoint(p, transform);
                    int x = x0 + q.x;
                    int y = y0 + q.y;
                    if (x > x1 || y > y1)
                    {
                        inPatch = false;
                        break;
                    }

                    raw.Add(x + y * width);
                }

                if (!inPatch)
                    continue;

                if (!TryStageV13SignatureLane(raw, width, height, canSpawn, occupied, stagedCells, salt + staged.Count * 23, out var path, out bool open, requireLayeredExit: true))
                    continue;

                if (open)
                    openHeads++;
                staged.Add(path);
            }

            if (staged.Count < 2 || openHeads == 0)
                return false;

            foreach (var path in staged)
            {
                AddAuthoredChain(authored, path, authored.arrows.Count);
                foreach (int idx in path)
                    occupied[idx] = true;
                moduleCount++;
                turnCount += CountLaneTurns(path, width);
            }

            return true;
        }

        static List<List<Vector2Int>> BuildV13PatchworkTemplate(int pattern)
        {
            var result = new List<List<Vector2Int>>();
            void Add(params Vector2Int[] points) => result.Add(new List<Vector2Int>(points));

            switch (pattern)
            {
                case 0:
                    Add(new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(2, 0), new Vector2Int(2, 1), new Vector2Int(3, 1), new Vector2Int(4, 1));
                    Add(new Vector2Int(4, 0), new Vector2Int(3, 0));
                    Add(new Vector2Int(0, 1), new Vector2Int(0, 2), new Vector2Int(1, 2), new Vector2Int(2, 2), new Vector2Int(3, 2), new Vector2Int(4, 2));
                    Add(new Vector2Int(0, 3), new Vector2Int(1, 3), new Vector2Int(1, 4), new Vector2Int(2, 4), new Vector2Int(3, 4), new Vector2Int(4, 4), new Vector2Int(4, 3), new Vector2Int(3, 3));
                    break;
                case 1:
                    Add(new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(2, 1), new Vector2Int(2, 0));
                    Add(new Vector2Int(3, 0), new Vector2Int(4, 0), new Vector2Int(4, 1), new Vector2Int(4, 2), new Vector2Int(3, 2));
                    Add(new Vector2Int(0, 2), new Vector2Int(1, 2), new Vector2Int(1, 3), new Vector2Int(2, 3), new Vector2Int(3, 3), new Vector2Int(4, 3));
                    Add(new Vector2Int(0, 4), new Vector2Int(1, 4), new Vector2Int(2, 4), new Vector2Int(3, 4), new Vector2Int(4, 4));
                    break;
                case 2:
                    Add(new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(2, 1), new Vector2Int(3, 1), new Vector2Int(3, 0), new Vector2Int(4, 0));
                    Add(new Vector2Int(0, 1), new Vector2Int(0, 2), new Vector2Int(1, 2), new Vector2Int(2, 2));
                    Add(new Vector2Int(4, 1), new Vector2Int(4, 2), new Vector2Int(3, 2), new Vector2Int(3, 3), new Vector2Int(4, 3));
                    Add(new Vector2Int(0, 3), new Vector2Int(1, 3), new Vector2Int(2, 3), new Vector2Int(2, 4), new Vector2Int(3, 4), new Vector2Int(4, 4));
                    break;
                case 3:
                    Add(new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(0, 2), new Vector2Int(1, 2), new Vector2Int(2, 2));
                    Add(new Vector2Int(1, 0), new Vector2Int(2, 0), new Vector2Int(3, 0), new Vector2Int(3, 1), new Vector2Int(4, 1));
                    Add(new Vector2Int(4, 0), new Vector2Int(4, 2), new Vector2Int(4, 3), new Vector2Int(3, 3), new Vector2Int(2, 3));
                    Add(new Vector2Int(0, 3), new Vector2Int(0, 4), new Vector2Int(1, 4), new Vector2Int(2, 4), new Vector2Int(3, 4), new Vector2Int(4, 4));
                    break;
                case 4:
                    Add(new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(2, 0), new Vector2Int(2, 1), new Vector2Int(2, 2), new Vector2Int(3, 2));
                    Add(new Vector2Int(3, 0), new Vector2Int(4, 0), new Vector2Int(4, 1), new Vector2Int(4, 2));
                    Add(new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(1, 2), new Vector2Int(1, 3), new Vector2Int(0, 3));
                    Add(new Vector2Int(2, 3), new Vector2Int(3, 3), new Vector2Int(4, 3), new Vector2Int(4, 4), new Vector2Int(3, 4), new Vector2Int(2, 4), new Vector2Int(1, 4), new Vector2Int(0, 4));
                    break;
                default:
                    Add(new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(1, 0), new Vector2Int(2, 0));
                    Add(new Vector2Int(3, 0), new Vector2Int(4, 0), new Vector2Int(4, 1), new Vector2Int(3, 1), new Vector2Int(2, 1));
                    Add(new Vector2Int(0, 2), new Vector2Int(1, 2), new Vector2Int(2, 2), new Vector2Int(2, 3), new Vector2Int(3, 3), new Vector2Int(4, 3));
                    Add(new Vector2Int(0, 3), new Vector2Int(0, 4), new Vector2Int(1, 4), new Vector2Int(2, 4), new Vector2Int(3, 4), new Vector2Int(4, 4));
                    break;
            }

            AddV13PatchworkUnusedRuns(result);
            return result;
        }

        static void AddV13PatchworkUnusedRuns(List<List<Vector2Int>> chains)
        {
            var used = new bool[5, 5];
            foreach (var chain in chains)
                foreach (var p in chain)
                    if (p.x >= 0 && p.x < 5 && p.y >= 0 && p.y < 5)
                        used[p.x, p.y] = true;

            for (int y = 0; y < 5; y++)
            {
                int x = 0;
                while (x < 5)
                {
                    while (x < 5 && used[x, y])
                        x++;

                    var run = new List<Vector2Int>();
                    while (x < 5 && !used[x, y])
                    {
                        used[x, y] = true;
                        run.Add(new Vector2Int(x, y));
                        x++;
                    }

                    if (run.Count >= 2)
                        chains.Add(run);
                }
            }
        }

        static Vector2Int TransformV13PatchPoint(Vector2Int p, int transform)
        {
            const int size = 5;
            Vector2Int q;
            switch (transform & 3)
            {
                case 1:
                    q = new Vector2Int(p.y, size - 1 - p.x);
                    break;
                case 2:
                    q = new Vector2Int(size - 1 - p.x, size - 1 - p.y);
                    break;
                case 3:
                    q = new Vector2Int(size - 1 - p.y, p.x);
                    break;
                default:
                    q = p;
                    break;
            }

            return (transform & 4) != 0 ? new Vector2Int(size - 1 - q.x, q.y) : q;
        }

        static int CountV13OccupiedCells(bool[] occupied)
        {
            int count = 0;
            if (occupied == null)
                return 0;
            foreach (bool value in occupied)
                if (value)
                    count++;
            return count;
        }

        static bool TryRepairV13DesignGreedyByPruning(
            AuthoredLevelData source,
            int width,
            int height,
            bool[] canSpawn,
            int allowedCount,
            ArrowMagicRuleset ruleset,
            List<Move> greedyMoves,
            out AuthoredLevelData repairedAuthored,
            out BoardState repairedBoard,
            out BoardGenerationTuning.BoardGenerationStats repairedStats)
        {
            repairedAuthored = null;
            repairedBoard = null;
            repairedStats = default;
            if (source?.arrows == null || source.arrows.Count == 0 || greedyMoves == null || greedyMoves.Count == 0)
                return false;

            var clicked = new HashSet<int>();
            foreach (var move in greedyMoves)
                clicked.Add(move.pos.x + move.pos.y * width);

            var clearedChains = new HashSet<int>();
            for (int i = 0; i < source.arrows.Count; i++)
            {
                var indices = source.arrows[i]?.indices;
                if (indices == null)
                    continue;

                foreach (int idx in indices)
                {
                    if (clicked.Contains(idx))
                    {
                        clearedChains.Add(i);
                        break;
                    }
                }
            }

            var stuck = new List<int>();
            for (int i = 0; i < source.arrows.Count; i++)
                if (!clearedChains.Contains(i))
                    stuck.Add(i);

            if (stuck.Count == 0)
                return false;

            var repairPool = new List<int>(stuck);
            var stuckSet = new HashSet<int>(stuck);
            for (int i = 0; i < source.arrows.Count; i++)
            {
                if (stuckSet.Contains(i))
                    continue;

                var indices = source.arrows[i]?.indices;
                if (indices == null || indices.Count > 4)
                    continue;

                repairPool.Add(i);
            }

            repairPool.Sort((a, b) =>
            {
                int la = source.arrows[a]?.indices?.Count ?? 999;
                int lb = source.arrows[b]?.indices?.Count ?? 999;
                int stuckCmp = (stuckSet.Contains(b) ? 1 : 0).CompareTo(stuckSet.Contains(a) ? 1 : 0);
                if (stuckCmp != 0)
                    return stuckCmp;
                int cmp = la.CompareTo(lb);
                return cmp != 0 ? cmp : a.CompareTo(b);
            });
            if (repairPool.Count > 26)
                repairPool.RemoveRange(26, repairPool.Count - 26);

            int currentTiles = 0;
            foreach (var arrow in source.arrows)
                currentTiles += arrow?.indices?.Count ?? 0;

            int maxRemoveCells = currentTiles - Mathf.CeilToInt(allowedCount * 0.80f);
            if (maxRemoveCells <= 0)
                return false;

            for (int i = 0; i < repairPool.Count; i++)
            {
                int a = repairPool[i];
                int lenA = source.arrows[a]?.indices?.Count ?? 999;
                if (lenA > maxRemoveCells)
                    continue;

                if (TryEvaluateV13PrunedDesignCandidate(source, new HashSet<int> { a }, allowedCount, ruleset, out repairedAuthored, out repairedBoard, out repairedStats))
                    return true;
            }

            for (int i = 0; i < repairPool.Count; i++)
            {
                int a = repairPool[i];
                int lenA = source.arrows[a]?.indices?.Count ?? 999;
                if (lenA >= maxRemoveCells)
                    continue;

                for (int j = i + 1; j < repairPool.Count; j++)
                {
                    int b = repairPool[j];
                    int lenB = source.arrows[b]?.indices?.Count ?? 999;
                    if (lenA + lenB > maxRemoveCells)
                        continue;

                    if (TryEvaluateV13PrunedDesignCandidate(source, new HashSet<int> { a, b }, allowedCount, ruleset, out repairedAuthored, out repairedBoard, out repairedStats))
                        return true;
                }
            }

            for (int i = 0; i < repairPool.Count; i++)
            {
                int a = repairPool[i];
                int lenA = source.arrows[a]?.indices?.Count ?? 999;
                if (lenA >= maxRemoveCells)
                    continue;

                for (int j = i + 1; j < repairPool.Count; j++)
                {
                    int b = repairPool[j];
                    int lenB = source.arrows[b]?.indices?.Count ?? 999;
                    if (lenA + lenB >= maxRemoveCells)
                        continue;

                    for (int k = j + 1; k < repairPool.Count; k++)
                    {
                        int c = repairPool[k];
                        int lenC = source.arrows[c]?.indices?.Count ?? 999;
                        if (lenA + lenB + lenC > maxRemoveCells)
                            continue;

                        if (TryEvaluateV13PrunedDesignCandidate(source, new HashSet<int> { a, b, c }, allowedCount, ruleset, out repairedAuthored, out repairedBoard, out repairedStats))
                            return true;
                    }
                }
            }

            return false;
        }

        static bool TryEvaluateV13PrunedDesignCandidate(
            AuthoredLevelData source,
            HashSet<int> remove,
            int allowedCount,
            ArrowMagicRuleset ruleset,
            out AuthoredLevelData finalAuthored,
            out BoardState roundTripBoard,
            out BoardGenerationTuning.BoardGenerationStats stats)
        {
            finalAuthored = null;
            roundTripBoard = null;
            stats = default;

            var candidate = CloneV13AuthoredWithoutChains(source, remove);
            if (!AuthoredLevelBuilder.TryBuildBoard(candidate, out var board, out _))
                return false;
            if (!TryRoundTripAuthoredBoard(board, out finalAuthored, out roundTripBoard))
                return false;

            stats = BoardGenerationTuning.CalculateBoardGenerationStats(roundTripBoard, ruleset);
            float fill = stats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
            if (fill < 0.80f)
                return false;

            CountShapeViolations(roundTripBoard, out int headToHead, out int directBlock, out _);
            if (headToHead > 0 || directBlock > 0)
                return false;

            return GreedyValidator.TryClearAllByGreedy(roundTripBoard, ruleset, 3200, out _);
        }

        static AuthoredLevelData CloneV13AuthoredWithoutChains(AuthoredLevelData source, HashSet<int> removeChains)
        {
            var clone = new AuthoredLevelData
            {
                width = source.width,
                height = source.height,
                arrows = new List<AuthoredArrowData>()
            };
            SetAuthoredBlockIndices(clone, GetAuthoredBlockIndices(source));

            for (int i = 0; i < source.arrows.Count; i++)
            {
                if (removeChains != null && removeChains.Contains(i))
                    continue;

                var arrow = source.arrows[i];
                if (arrow?.indices == null || arrow.indices.Count < 2)
                    continue;

                clone.arrows.Add(new AuthoredArrowData
                {
                    indices = new List<int>(arrow.indices),
                    colorIndex = arrow.colorIndex,
                });
            }

            return clone;
        }

        static bool TryBuildV13SeedOverlayAuthored(int width, int height, bool[] canSpawn, int variant, AuthoredLevelData authored, out int moduleCount, out int turnCount, out V10MotifStats motifStats)
        {
            moduleCount = 0;
            turnCount = 0;
            motifStats = default;
            var sources = GetV13SeedOverlaySources(width, height);
            if (sources.Count == 0 || authored?.arrows == null)
                return false;

            var source = sources[PositiveMod(variant * 17, sources.Count)];
            int transform = PositiveMod(variant / Mathf.Max(1, sources.Count), 4);
            GetV13OverlayTransformedSize(source.Width, source.Height, transform, out int sourceW, out int sourceH);
            int dxMin = Mathf.Min(0, width - sourceW);
            int dxMax = Mathf.Max(0, width - sourceW);
            int dyMin = Mathf.Min(0, height - sourceH);
            int dyMax = Mathf.Max(0, height - sourceH);
            int dx = dxMin + PositiveMod(variant * 5 + source.Width, dxMax - dxMin + 1);
            int dy = dyMin + PositiveMod(variant * 7 + source.Height, dyMax - dyMin + 1);
            var occupied = new bool[width * height];

            foreach (var headToTail in source.HeadToTailChains)
            {
                if (headToTail == null || headToTail.Count < 2)
                    continue;

                var segment = new List<int>();
                for (int i = headToTail.Count - 1; i >= 0; i--)
                {
                    var p = TransformV13OverlayPoint(headToTail[i], source.Width, source.Height, transform);
                    int x = p.x + dx;
                    int y = p.y + dy;
                    int idx = x + y * width;
                    bool valid = InBounds(width, height, x, y) && idx >= 0 && idx < canSpawn.Length && canSpawn[idx] && !occupied[idx];
                    if (valid)
                    {
                        segment.Add(idx);
                        continue;
                    }

                    AddV13SeedOverlaySegment(authored, segment, width, height, canSpawn, occupied, ref moduleCount, ref turnCount);
                    segment.Clear();
                }

                AddV13SeedOverlaySegment(authored, segment, width, height, canSpawn, occupied, ref moduleCount, ref turnCount);
            }

            int residualTurns;
            int residualChains = AddV13AuthoredResidualFill(width, height, canSpawn, occupied, authored, variant + 1700, out residualTurns);
            moduleCount += residualChains;
            turnCount += residualTurns;

            if (moduleCount < 18)
                return false;

            motifStats = new V10MotifStats
            {
                MotifKindCount = 6,
                StraightChainRatio = 0.35f,
                RegionDirectionEntropy = 0.75f,
                RepeatedMotifPenalty = 0.05f,
                HoleEdgeCoverage = 0.5f,
                Plan = "SeedOverlay:" + source.Id
            };
            return authored.arrows.Count > 0;
        }

        static void AddV13SeedOverlaySegment(AuthoredLevelData authored, List<int> innerToOuter, int width, int height, bool[] canSpawn, bool[] occupied, ref int moduleCount, ref int turnCount)
        {
            if (innerToOuter == null || innerToOuter.Count < 2 || !IsContiguousPath(innerToOuter, width))
                return;

            var path = new List<int>(innerToOuter);
            var reversed = new List<int>(innerToOuter);
            reversed.Reverse();
            bool normalOpen = LaneHeadHasV13LayeredExit(path, width, height, canSpawn);
            bool reversedOpen = LaneHeadHasV13LayeredExit(reversed, width, height, canSpawn);
            bool normalOk = LaneHeadAvoidsV13Block(path, width, height, canSpawn);
            bool reversedOk = LaneHeadAvoidsV13Block(reversed, width, height, canSpawn);
            if (reversedOpen || (!normalOpen && reversedOk && !normalOk))
                path = reversed;
            else if (!normalOpen && !normalOk && reversedOk)
                path = reversed;
            else if (!normalOpen && !normalOk && !reversedOk)
                return;

            AddAuthoredChain(authored, path, authored.arrows.Count);
            foreach (int idx in path)
                occupied[idx] = true;
            moduleCount++;
            turnCount += CountLaneTurns(path, width);
        }

        static List<V13SeedOverlaySource> GetV13SeedOverlaySources(int targetWidth, int targetHeight)
        {
            if (s_V13SeedOverlaySources != null)
                return s_V13SeedOverlaySources;

            var loaded = new List<(V13SeedOverlaySource source, float score)>();
            string[] guids = AssetDatabase.FindAssets("t:LevelDefinition", new[] { "Assets/ArrowMagic/SOData/Levels/Seeds" });
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var def = AssetDatabase.LoadAssetAtPath<LevelDefinition>(path);
                var data = def?.authoredLevel;
                if (data?.arrows == null || data.width <= 0 || data.height <= 0)
                    continue;

                int score = Mathf.Abs(data.width - targetWidth) + Mathf.Abs(data.height - targetHeight);
                if (score > 5)
                    continue;

                var chains = new List<List<Vector2Int>>();
                int straightChains = 0;
                int horizontalChains = 0;
                int verticalChains = 0;
                int totalTurns = 0;
                foreach (var arrow in data.arrows)
                {
                    if (arrow?.indices == null || arrow.indices.Count < 2)
                        continue;

                    var points = new List<Vector2Int>(arrow.indices.Count);
                    foreach (int idx in arrow.indices)
                        points.Add(new Vector2Int(idx % data.width, idx / data.width));
                    if (IsContiguousPointPath(points))
                    {
                        chains.Add(points);
                        int turns = CountPointTurns(points);
                        totalTurns += turns;
                        if (turns == 0)
                            straightChains++;

                        var first = points[0];
                        var last = points[points.Count - 1];
                        if (Mathf.Abs(last.x - first.x) >= Mathf.Abs(last.y - first.y))
                            horizontalChains++;
                        else
                            verticalChains++;
                    }
                }

                if (chains.Count < 12)
                    continue;

                float straightRatio = straightChains / (float)Mathf.Max(1, chains.Count);
                float axisDominance = Mathf.Max(horizontalChains, verticalChains) / (float)Mathf.Max(1, chains.Count);
                float avgTurns = totalTurns / (float)Mathf.Max(1, chains.Count);
                if (straightRatio > 0.58f && axisDominance > 0.62f)
                    continue;

                float sourceScore = score * 20f + straightRatio * 90f + axisDominance * 55f - Mathf.Min(avgTurns, 4f) * 18f;
                loaded.Add((new V13SeedOverlaySource
                {
                    Id = def.levelId,
                    Width = data.width,
                    Height = data.height,
                    HeadToTailChains = chains
                }, sourceScore));
            }

            loaded.Sort((a, b) => a.score.CompareTo(b.score));
            s_V13SeedOverlaySources = new List<V13SeedOverlaySource>();
            int count = Mathf.Min(90, loaded.Count);
            for (int i = 0; i < count; i++)
                s_V13SeedOverlaySources.Add(loaded[i].source);
            return s_V13SeedOverlaySources;
        }

        static void GetV13OverlayTransformedSize(int width, int height, int transform, out int outWidth, out int outHeight)
        {
            if ((transform & 1) == 0)
            {
                outWidth = width;
                outHeight = height;
            }
            else
            {
                outWidth = height;
                outHeight = width;
            }
        }

        static Vector2Int TransformV13OverlayPoint(Vector2Int p, int width, int height, int transform)
        {
            switch (transform & 3)
            {
                case 1:
                    return new Vector2Int(p.y, width - 1 - p.x);
                case 2:
                    return new Vector2Int(width - 1 - p.x, height - 1 - p.y);
                case 3:
                    return new Vector2Int(height - 1 - p.y, p.x);
                default:
                    return p;
            }
        }

        static void AddV13TemplateBlockBand(int width, int height, bool[] canSpawn, bool[] occupied, List<V10Lane> lanes, int xStart, int xEnd, int yStart, int yEnd, int variant, int salt, string region, bool horizontalBand)
        {
            int x0 = Mathf.Min(xStart, xEnd);
            int x1 = Mathf.Max(xStart, xEnd);
            int y0 = Mathf.Min(yStart, yEnd);
            int y1 = Mathf.Max(yStart, yEnd);
            if (x1 < x0 || y1 < y0 || x0 < 0 || y0 < 0 || x1 >= width || y1 >= height)
                return;

            int block = 0;
            int y = y0;
            while (y <= y1)
            {
                int blockH = horizontalBand
                    ? 3 + PositiveMod(variant + salt + block * 5, 4)
                    : 4 + PositiveMod(variant + salt + block * 3, 5);
                int by1 = Mathf.Min(y1, y + blockH - 1);
                if (y1 - by1 == 1)
                    by1 = y1;

                int x = x0;
                while (x <= x1)
                {
                    int blockW = horizontalBand
                        ? 5 + PositiveMod(variant + salt + block * 7 + x, 5)
                        : 3 + PositiveMod(variant + salt + block * 7 + y, 3);
                    int bx1 = Mathf.Min(x1, x + blockW - 1);
                    if (x1 - bx1 == 1)
                        bx1 = x1;

                    bool designHeavy = variant >= 3200;
                    bool leaveBreathingGap = PositiveMod(variant + salt + block * 11 + x * 3 + y * 5, designHeavy ? 19 : 29) == 0;
                    if (!leaveBreathingGap)
                    {
                        int pattern = PositiveMod(variant + salt + block * 13 + x + y, 6);
                        bool useSignatureBlock = PositiveMod(variant + salt * 5 + block * 31 + x * 3 + y * 17, 100) < (designHeavy ? 32 : 34);
                        if (useSignatureBlock && TryAddV13SignatureBlockLanes(width, height, canSpawn, occupied, lanes, x, bx1, y, by1, variant + salt + block * 9, region))
                        {
                            x = bx1 + 1;
                            block++;
                            continue;
                        }

                        bool useSeedGroup = PositiveMod(variant + salt * 3 + block * 23 + x * 5 + y * 7, 100) < (designHeavy ? 68 : 14);
                        if (useSeedGroup && TryAddV13SeedGroupMotifLanes(width, height, canSpawn, occupied, lanes, x, bx1, y, by1, variant + salt + block * 5, region))
                        {
                            x = bx1 + 1;
                            block++;
                            continue;
                        }

                        bool useSeedMotif = PositiveMod(variant + salt + block * 19 + x * 7 + y * 11, 100) < (designHeavy ? 66 : 25);
                        var lane = useSeedMotif && TryBuildV13SeedMotifLane(width, height, canSpawn, occupied, x, bx1, y, by1, variant + salt + block, out var seedLane)
                            ? seedLane
                            : BuildV13LocalTemplateLane(width, height, canSpawn, occupied, x, bx1, y, by1, pattern);
                        var kind = pattern switch
                        {
                            0 => V10MotifKind.SwerveLong,
                            1 => V10MotifKind.Turnback,
                            2 => V10MotifKind.Hook,
                            3 => V10MotifKind.UShape,
                            4 => V10MotifKind.ShortBridge,
                            _ => V10MotifKind.ZigZagLite
                        };
                        AddV13LayeredLaneIfValid(lane, kind, region, width, height, canSpawn, occupied, lanes);
                    }

                    x = bx1 + 1;
                    block++;
                }

                y = by1 + 1;
            }
        }

        static bool TryBuildV13SeedMotifLane(int width, int height, bool[] canSpawn, bool[] occupied, int x0, int x1, int y0, int y1, int salt, out List<int> lane)
        {
            lane = null;
            var motifs = GetV13SeedMotifs();
            if (motifs.Count == 0 || x1 < x0 || y1 < y0)
                return false;

            int blockW = x1 - x0 + 1;
            int blockH = y1 - y0 + 1;
            for (int attempt = 0; attempt < 14; attempt++)
            {
                var motif = motifs[PositiveMod(salt * 31 + attempt * 17, motifs.Count)];
                int transform = PositiveMod(salt + attempt * 3, 8);
                var points = TransformV13SeedMotif(motif, transform, out int motifW, out int motifH);
                if (motifW > blockW || motifH > blockH)
                    continue;

                int ox = x0 + PositiveMod(salt + attempt * 5, blockW - motifW + 1);
                int oy = y0 + PositiveMod(salt * 3 + attempt * 7, blockH - motifH + 1);
                var candidate = new List<int>(points.Count);
                bool ok = true;
                foreach (var p in points)
                {
                    int x = ox + p.x;
                    int y = oy + p.y;
                    int idx = x + y * width;
                    if (!InBounds(width, height, x, y) || idx < 0 || idx >= canSpawn.Length || idx >= occupied.Length || !canSpawn[idx] || occupied[idx])
                    {
                        ok = false;
                        break;
                    }

                    candidate.Add(idx);
                }

                if (ok && candidate.Count >= 3 && IsContiguousPath(candidate, width))
                {
                    lane = candidate;
                    return true;
                }
            }

            return false;
        }

        static bool TryAddV13SignatureBlockLanes(int width, int height, bool[] canSpawn, bool[] occupied, List<V10Lane> lanes, int x0, int x1, int y0, int y1, int salt, string region)
        {
            int blockW = x1 - x0 + 1;
            int blockH = y1 - y0 + 1;
            if (blockW < 4 || blockH < 3)
                return false;

            var raw = new List<List<int>>();
            int pattern = PositiveMod(salt, 4);
            if (pattern == 0)
            {
                raw.Add(BuildV13RawRowThenColumn(width, x0, x1 - 1, y0, x1 - 1, y0 + 1, y1));
                raw.Add(BuildV13RawRow(width, x1, x0 + 1, Mathf.Min(y1, y0 + 2)));
                raw.Add(BuildV13RawColumn(width, x0, y1, y0 + 1));
            }
            else if (pattern == 1)
            {
                raw.Add(BuildV13RawU(width, x0, x1, y0, y1));
                raw.Add(BuildV13RawColumn(width, Mathf.Min(x1, x0 + 1), y1, y0 + 1));
            }
            else if (pattern == 2)
            {
                raw.Add(BuildV13RawRow(width, x0, x1, y0));
                raw.Add(BuildV13RawRowThenColumn(width, x1, x0 + 1, Mathf.Min(y1, y0 + 2), x0 + 1, Mathf.Min(y1, y0 + 2), y1));
                raw.Add(BuildV13RawColumn(width, Mathf.Clamp((x0 + x1) / 2, x0, x1), y1, y0 + 1));
            }
            else
            {
                raw.Add(BuildV13RawColumn(width, x0, y0, y1));
                raw.Add(BuildV13RawRow(width, x0 + 1, x1, y1));
                raw.Add(BuildV13RawRow(width, x1, x0 + 2, Mathf.Clamp((y0 + y1) / 2, y0, y1)));
            }

            var staged = new List<List<int>>();
            var stagedCells = new HashSet<int>();
            int openHeads = 0;
            foreach (var candidateRaw in raw)
            {
                if (!TryStageV13SignatureLane(candidateRaw, width, height, canSpawn, occupied, stagedCells, salt + staged.Count * 19, out var path, out bool open))
                    continue;

                if (open)
                    openHeads++;
                staged.Add(path);
            }

            if (staged.Count < 2 || openHeads == 0)
                return false;

            for (int i = 0; i < staged.Count; i++)
            {
                var kind = i == 0 ? V10MotifKind.SwerveLong : (i == 1 ? V10MotifKind.Hook : V10MotifKind.ShortBridge);
                lanes.Add(new V10Lane { Cells = staged[i], Kind = kind, Region = region + "Signature" });
                foreach (int idx in staged[i])
                    occupied[idx] = true;
            }

            return true;
        }

        static bool TryStageV13SignatureLane(List<int> raw, int width, int height, bool[] canSpawn, bool[] occupied, HashSet<int> stagedCells, int salt, out List<int> path, out bool open, bool requireLayeredExit = false)
        {
            path = null;
            open = false;
            if (raw == null || raw.Count < 2 || !IsContiguousPath(raw, width))
                return false;

            foreach (int idx in raw)
            {
                if (idx < 0 || idx >= canSpawn.Length || idx >= occupied.Length || !canSpawn[idx] || occupied[idx] || stagedCells.Contains(idx))
                    return false;
            }

            var normal = new List<int>(raw);
            var reversed = new List<int>(raw);
            reversed.Reverse();
            bool normalOpen = LaneHeadHasV13LayeredExit(normal, width, height, canSpawn);
            bool reversedOpen = LaneHeadHasV13LayeredExit(reversed, width, height, canSpawn);
            bool normalOk = LaneHeadAvoidsV13Block(normal, width, height, canSpawn);
            bool reversedOk = LaneHeadAvoidsV13Block(reversed, width, height, canSpawn);
            if (requireLayeredExit)
            {
                if (reversedOpen && (!normalOpen || PositiveMod(salt, 2) == 0))
                    path = reversed;
                else if (normalOpen)
                    path = normal;
                else if (reversedOpen)
                    path = reversed;
                else
                    return false;
            }
            else if (reversedOpen || (!normalOpen && reversedOk && PositiveMod(salt, 2) == 0))
                path = reversed;
            else if (normalOpen || normalOk)
                path = normal;
            else if (reversedOk)
                path = reversed;
            else
                return false;

            open = LaneHeadHasV13LayeredExit(path, width, height, canSpawn);
            if (requireLayeredExit && !open)
                return false;
            foreach (int idx in path)
                stagedCells.Add(idx);
            return true;
        }

        static List<int> BuildV13RawRow(int width, int xFrom, int xTo, int y)
        {
            var path = new List<int>();
            int step = xFrom <= xTo ? 1 : -1;
            for (int x = xFrom; step > 0 ? x <= xTo : x >= xTo; x += step)
                path.Add(x + y * width);
            return path;
        }

        static List<int> BuildV13RawColumn(int width, int x, int yFrom, int yTo)
        {
            var path = new List<int>();
            int step = yFrom <= yTo ? 1 : -1;
            for (int y = yFrom; step > 0 ? y <= yTo : y >= yTo; y += step)
                path.Add(x + y * width);
            return path;
        }

        static List<int> BuildV13RawRowThenColumn(int width, int xFrom, int xTo, int y, int columnX, int yFrom, int yTo)
        {
            var path = BuildV13RawRow(width, xFrom, xTo, y);
            int step = yFrom <= yTo ? 1 : -1;
            for (int yy = yFrom; step > 0 ? yy <= yTo : yy >= yTo; yy += step)
            {
                int idx = columnX + yy * width;
                if (path.Count == 0 || path[path.Count - 1] != idx)
                    path.Add(idx);
            }
            return path;
        }

        static List<int> BuildV13RawU(int width, int x0, int x1, int y0, int y1)
        {
            var path = BuildV13RawRow(width, x0, x1, y0);
            for (int y = y0 + 1; y <= y1; y++)
                path.Add(x1 + y * width);
            for (int x = x1 - 1; x >= x0 + 2; x--)
                path.Add(x + y1 * width);
            return path;
        }

        static bool TryAddV13SeedGroupMotifLanes(int width, int height, bool[] canSpawn, bool[] occupied, List<V10Lane> lanes, int x0, int x1, int y0, int y1, int salt, string region)
        {
            var motifs = GetV13SeedGroupMotifs();
            if (motifs.Count == 0 || x1 < x0 || y1 < y0)
                return false;

            int blockW = x1 - x0 + 1;
            int blockH = y1 - y0 + 1;
            for (int attempt = 0; attempt < 28; attempt++)
            {
                var motif = motifs[PositiveMod(salt * 29 + attempt * 31, motifs.Count)];
                int transform = PositiveMod(salt + attempt * 5, 8);
                var transformed = TransformV13SeedGroupMotif(motif, transform, out int motifW, out int motifH);
                if (motifW > blockW || motifH > blockH)
                    continue;

                int ox = x0 + PositiveMod(salt * 3 + attempt * 7, blockW - motifW + 1);
                int oy = y0 + PositiveMod(salt * 5 + attempt * 11, blockH - motifH + 1);
                var staged = new List<List<int>>(transformed.Count);
                var stagedCells = new HashSet<int>();
                int openHeads = 0;
                bool ok = true;
                foreach (var chain in transformed)
                {
                    var candidate = new List<int>(chain.Count);
                    foreach (var p in chain)
                    {
                        int x = ox + p.x;
                        int y = oy + p.y;
                        int idx = x + y * width;
                        if (!InBounds(width, height, x, y) || idx < 0 || idx >= canSpawn.Length || idx >= occupied.Length || !canSpawn[idx] || occupied[idx] || !stagedCells.Add(idx))
                        {
                            ok = false;
                            break;
                        }

                        candidate.Add(idx);
                    }

                    if (!ok || candidate.Count < 3 || !IsContiguousPath(candidate, width))
                    {
                        ok = false;
                        break;
                    }

                    var path = EnsureV13LayeredOpenHead(candidate, width, height, canSpawn);
                    if (LaneHeadHasV13LayeredExit(path, width, height, canSpawn))
                    {
                        openHeads++;
                    }
                    else
                    {
                        var reversed = new List<int>(candidate);
                        reversed.Reverse();
                        bool normalOk = LaneHeadAvoidsV13Block(candidate, width, height, canSpawn);
                        bool reversedOk = LaneHeadAvoidsV13Block(reversed, width, height, canSpawn);
                        if (!normalOk && !reversedOk)
                        {
                            ok = false;
                            break;
                        }

                        path = reversedOk && (!normalOk || PositiveMod(salt + staged.Count * 13 + attempt, 2) == 0)
                            ? reversed
                            : candidate;
                    }

                    staged.Add(path);
                }

                if (!ok || staged.Count < 2 || openHeads == 0)
                    continue;

                foreach (var path in staged)
                {
                    lanes.Add(new V10Lane { Cells = path, Kind = V10MotifKind.HoleEdgeDetour, Region = region + "SeedGroup" });
                    foreach (int idx in path)
                        occupied[idx] = true;
                }

                return true;
            }

            return false;
        }

        static bool TryBuildV13PrunedVisualGaps(AuthoredLevelData source, int width, int height, int variant, out AuthoredLevelData prunedAuthored, out BoardState prunedRoundTripBoard)
        {
            prunedAuthored = null;
            prunedRoundTripBoard = null;
            if (source?.arrows == null || source.arrows.Count < 24)
                return false;

            var scored = new List<(int index, int cells, int score)>();
            for (int i = 0; i < source.arrows.Count; i++)
            {
                var indices = source.arrows[i]?.indices;
                if (indices == null || indices.Count < 3 || indices.Count > 9)
                    continue;

                int turns = CountAuthoredChainTurns(indices, width);
                if (turns > 1)
                    continue;

                var p = IndexToPos(indices[indices.Count / 2], width);
                int edgeDistance = Mathf.Min(Mathf.Min(p.x, width - 1 - p.x), Mathf.Min(p.y, height - 1 - p.y));
                int score = edgeDistance * 17 + indices.Count * 5 + PositiveMod(variant + i * 11, 23);
                scored.Add((i, indices.Count, score));
            }

            if (scored.Count < 3)
                return false;

            scored.Sort((a, b) => b.score.CompareTo(a.score));
            int targetCells = 7 + PositiveMod(variant, 7);
            int removedCells = 0;
            var remove = new HashSet<int>();
            foreach (var item in scored)
            {
                if (removedCells >= targetCells)
                    break;
                remove.Add(item.index);
                removedCells += item.cells;
            }

            if (remove.Count == 0)
                return false;

            var data = new AuthoredLevelData
            {
                width = source.width,
                height = source.height,
                arrows = new List<AuthoredArrowData>()
            };
            SetAuthoredBlockIndices(data, GetAuthoredBlockIndices(source));

            for (int i = 0; i < source.arrows.Count; i++)
            {
                if (remove.Contains(i))
                    continue;

                var arrow = source.arrows[i];
                data.arrows.Add(new AuthoredArrowData
                {
                    indices = arrow.indices != null ? new List<int>(arrow.indices) : new List<int>(),
                    colorIndex = data.arrows.Count
                });
            }

            if (!AuthoredLevelBuilder.TryBuildBoard(data, out var board, out _))
                return false;
            return TryRoundTripAuthoredBoard(board, out prunedAuthored, out prunedRoundTripBoard);
        }

        static int CountAuthoredChainTurns(List<int> indices, int width)
        {
            if (indices == null || indices.Count < 3)
                return 0;

            int turns = 0;
            for (int i = 2; i < indices.Count; i++)
            {
                var a = IndexToPos(indices[i - 2], width);
                var b = IndexToPos(indices[i - 1], width);
                var c = IndexToPos(indices[i], width);
                if (b - a != c - b)
                    turns++;
            }

            return turns;
        }

        static void ReserveV13DesignGaps(int width, int height, bool[] canSpawn, bool[] occupied, int minX, int maxX, int minY, int maxY, int variant)
        {
            int reserved = 0;
            int limit = 5 + PositiveMod(variant, 4);
            for (int crack = 0; crack < 4 && reserved < limit; crack++)
            {
                bool vertical = (crack & 1) == 0;
                if (vertical)
                {
                    bool top = (crack & 2) == 0;
                    int yStart = top ? maxY + 1 : 0;
                    int yEnd = top ? height - 1 : minY - 1;
                    if (yEnd < yStart)
                        continue;

                    int x = Mathf.Clamp(2 + PositiveMod(variant * 5 + crack * 7, width - 4), 1, width - 2);
                    int len = 3 + PositiveMod(variant + crack * 11, 5);
                    int y = yStart + PositiveMod(variant * 3 + crack * 13, Mathf.Max(1, yEnd - yStart + 1));
                    for (int i = 0; i < len && reserved < limit; i++)
                    {
                        int yy = top ? y - i : y + i;
                        if (yy < yStart || yy > yEnd)
                            continue;
                        if (TryReserveV13GapCell(width, height, canSpawn, occupied, x, yy))
                            reserved++;
                        if ((i & 1) == 0 && TryReserveV13GapCell(width, height, canSpawn, occupied, x + 1, yy))
                            reserved++;
                    }
                }
                else
                {
                    bool left = (crack & 2) == 0;
                    int xStart = left ? 0 : maxX + 1;
                    int xEnd = left ? minX - 1 : width - 1;
                    if (xEnd < xStart)
                        continue;

                    int y = Mathf.Clamp(minY + PositiveMod(variant * 7 + crack * 5, Mathf.Max(1, maxY - minY + 1)), 1, height - 2);
                    int len = 3 + PositiveMod(variant + crack * 17, 5);
                    int x = xStart + PositiveMod(variant * 11 + crack * 3, Mathf.Max(1, xEnd - xStart + 1));
                    for (int i = 0; i < len && reserved < limit; i++)
                    {
                        int xx = left ? x - i : x + i;
                        if (xx < xStart || xx > xEnd)
                            continue;
                        if (TryReserveV13GapCell(width, height, canSpawn, occupied, xx, y))
                            reserved++;
                        if ((i & 1) == 0 && TryReserveV13GapCell(width, height, canSpawn, occupied, xx, y + 1))
                            reserved++;
                    }
                }
            }
        }

        static bool TryReserveV13GapCell(int width, int height, bool[] canSpawn, bool[] occupied, int x, int y)
        {
            if (!InBounds(width, height, x, y))
                return false;

            int idx = x + y * width;
            if (idx < 0 || idx >= canSpawn.Length || idx >= occupied.Length || !canSpawn[idx] || occupied[idx])
                return false;

            occupied[idx] = true;
            return true;
        }

        static List<V13SeedGroupMotif> GetV13SeedGroupMotifs()
        {
            if (s_V13SeedGroupMotifs != null)
                return s_V13SeedGroupMotifs;

            s_V13SeedGroupMotifs = new List<V13SeedGroupMotif>();
            string[] guids = AssetDatabase.FindAssets("t:LevelDefinition", new[] { "Assets/ArrowMagic/SOData/Levels/Seeds" });
            int limit = Mathf.Min(guids.Length, 220);
            for (int i = 0; i < limit; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                var def = AssetDatabase.LoadAssetAtPath<LevelDefinition>(path);
                var data = def?.authoredLevel;
                if (data?.arrows == null || data.width <= 0 || data.height <= 0)
                    continue;

                var chains = new List<(List<Vector2Int> points, RectInt bounds, int turns, Vector2 center)>();
                foreach (var arrow in data.arrows)
                {
                    if (arrow?.indices == null || arrow.indices.Count < 4 || arrow.indices.Count > 22)
                        continue;

                    var points = new List<Vector2Int>(arrow.indices.Count);
                    foreach (int idx in arrow.indices)
                        points.Add(new Vector2Int(idx % data.width, idx / data.width));
                    points.Reverse();
                    if (!IsContiguousPointPath(points))
                        continue;

                    int turns = CountPointTurns(points);
                    if (turns < 1)
                        continue;

                    var bounds = BuildV13PointBounds(points);
                    if (bounds.width > 9 || bounds.height > 9)
                        continue;

                    chains.Add((points, bounds, turns, new Vector2(bounds.center.x, bounds.center.y)));
                }

                for (int anchor = 0; anchor < chains.Count && s_V13SeedGroupMotifs.Count < 240; anchor++)
                {
                    var order = new List<int>();
                    for (int j = 0; j < chains.Count; j++)
                    {
                        if (j == anchor)
                            continue;

                        float dx = chains[j].center.x - chains[anchor].center.x;
                        float dy = chains[j].center.y - chains[anchor].center.y;
                        if (dx * dx + dy * dy <= 64f)
                            order.Add(j);
                    }

                    order.Sort((a, b) =>
                    {
                        float adx = chains[a].center.x - chains[anchor].center.x;
                        float ady = chains[a].center.y - chains[anchor].center.y;
                        float bdx = chains[b].center.x - chains[anchor].center.x;
                        float bdy = chains[b].center.y - chains[anchor].center.y;
                        return (adx * adx + ady * ady).CompareTo(bdx * bdx + bdy * bdy);
                    });

                    var group = new List<List<Vector2Int>> { chains[anchor].points };
                    int turns = chains[anchor].turns;
                    foreach (int next in order)
                    {
                        group.Add(chains[next].points);
                        turns += chains[next].turns;
                        if (group.Count >= 2 && TryCreateV13SeedGroupMotif(group, turns, out var motif))
                        {
                            s_V13SeedGroupMotifs.Add(motif);
                            if (s_V13SeedGroupMotifs.Count >= 240)
                                break;
                        }

                        if (group.Count >= 4)
                            break;
                    }
                }
            }

            return s_V13SeedGroupMotifs;
        }

        static bool TryCreateV13SeedGroupMotif(List<List<Vector2Int>> source, int turns, out V13SeedGroupMotif motif)
        {
            motif = null;
            if (source == null || source.Count < 2 || turns < 3)
                return false;

            var clone = new List<List<Vector2Int>>(source.Count);
            var cells = new HashSet<Vector2Int>();
            int totalCells = 0;
            foreach (var chain in source)
            {
                if (chain == null || chain.Count < 3)
                    return false;

                var copy = new List<Vector2Int>(chain.Count);
                foreach (var p in chain)
                {
                    if (!cells.Add(p))
                        return false;

                    copy.Add(p);
                }

                totalCells += copy.Count;
                clone.Add(copy);
            }

            if (totalCells < 12 || totalCells > 46)
                return false;

            NormalizeV13SeedGroupMotif(clone, out int width, out int height);
            if (width > 9 || height > 9 || width * height <= totalCells)
                return false;

            motif = new V13SeedGroupMotif { Chains = clone, Width = width, Height = height, Turns = turns };
            return true;
        }

        static RectInt BuildV13PointBounds(List<Vector2Int> points)
        {
            int minX = int.MaxValue, minY = int.MaxValue, maxX = int.MinValue, maxY = int.MinValue;
            foreach (var p in points)
            {
                minX = Mathf.Min(minX, p.x);
                minY = Mathf.Min(minY, p.y);
                maxX = Mathf.Max(maxX, p.x);
                maxY = Mathf.Max(maxY, p.y);
            }

            return new RectInt(minX, minY, maxX - minX + 1, maxY - minY + 1);
        }

        static List<List<Vector2Int>> TransformV13SeedGroupMotif(V13SeedGroupMotif motif, int transform, out int width, out int height)
        {
            var raw = new List<List<Vector2Int>>(motif.Chains.Count);
            foreach (var chain in motif.Chains)
            {
                var transformed = new List<Vector2Int>(chain.Count);
                foreach (var p in chain)
                {
                    Vector2Int q;
                    switch (transform & 3)
                    {
                        case 1:
                            q = new Vector2Int(p.y, motif.Width - 1 - p.x);
                            break;
                        case 2:
                            q = new Vector2Int(motif.Width - 1 - p.x, motif.Height - 1 - p.y);
                            break;
                        case 3:
                            q = new Vector2Int(motif.Height - 1 - p.y, p.x);
                            break;
                        default:
                            q = p;
                            break;
                    }

                    transformed.Add(q);
                }

                raw.Add(transformed);
            }

            NormalizeV13SeedGroupMotif(raw, out width, out height);
            if ((transform & 4) != 0)
            {
                foreach (var chain in raw)
                    for (int i = 0; i < chain.Count; i++)
                        chain[i] = new Vector2Int(width - 1 - chain[i].x, chain[i].y);
            }

            return raw;
        }

        static void NormalizeV13SeedGroupMotif(List<List<Vector2Int>> chains, out int width, out int height)
        {
            int minX = int.MaxValue, minY = int.MaxValue, maxX = int.MinValue, maxY = int.MinValue;
            foreach (var chain in chains)
            {
                foreach (var p in chain)
                {
                    minX = Mathf.Min(minX, p.x);
                    minY = Mathf.Min(minY, p.y);
                    maxX = Mathf.Max(maxX, p.x);
                    maxY = Mathf.Max(maxY, p.y);
                }
            }

            for (int c = 0; c < chains.Count; c++)
                for (int i = 0; i < chains[c].Count; i++)
                    chains[c][i] = new Vector2Int(chains[c][i].x - minX, chains[c][i].y - minY);

            width = maxX - minX + 1;
            height = maxY - minY + 1;
        }

        static List<V13SeedMotif> GetV13SeedMotifs()
        {
            if (s_V13SeedMotifs != null)
                return s_V13SeedMotifs;

            s_V13SeedMotifs = new List<V13SeedMotif>();
            string[] guids = AssetDatabase.FindAssets("t:LevelDefinition", new[] { "Assets/ArrowMagic/SOData/Levels/Seeds" });
            int limit = Mathf.Min(guids.Length, 260);
            for (int i = 0; i < limit; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                var def = AssetDatabase.LoadAssetAtPath<LevelDefinition>(path);
                var data = def?.authoredLevel;
                if (data?.arrows == null || data.width <= 0)
                    continue;

                foreach (var arrow in data.arrows)
                {
                    if (arrow?.indices == null || arrow.indices.Count < 4 || arrow.indices.Count > 18)
                        continue;

                    var pts = new List<Vector2Int>(arrow.indices.Count);
                    foreach (int idx in arrow.indices)
                        pts.Add(new Vector2Int(idx % data.width, idx / data.width));
                    pts.Reverse();
                    if (!IsContiguousPointPath(pts))
                        continue;

                    int turns = CountPointTurns(pts);
                    if (turns < 1)
                        continue;

                    NormalizeV13SeedMotif(pts, out int w, out int h);
                    if (w > 9 || h > 9)
                        continue;

                    s_V13SeedMotifs.Add(new V13SeedMotif { Points = pts, Width = w, Height = h });
                    if (s_V13SeedMotifs.Count >= 180)
                        return s_V13SeedMotifs;
                }
            }

            return s_V13SeedMotifs;
        }

        static List<Vector2Int> TransformV13SeedMotif(V13SeedMotif motif, int transform, out int width, out int height)
        {
            var raw = new List<Vector2Int>(motif.Points.Count);
            foreach (var p in motif.Points)
            {
                Vector2Int q;
                switch (transform & 3)
                {
                    case 1:
                        q = new Vector2Int(p.y, motif.Width - 1 - p.x);
                        break;
                    case 2:
                        q = new Vector2Int(motif.Width - 1 - p.x, motif.Height - 1 - p.y);
                        break;
                    case 3:
                        q = new Vector2Int(motif.Height - 1 - p.y, p.x);
                        break;
                    default:
                        q = p;
                        break;
                }

                raw.Add(q);
            }

            NormalizeV13SeedMotif(raw, out width, out height);
            if ((transform & 4) != 0)
            {
                for (int i = 0; i < raw.Count; i++)
                    raw[i] = new Vector2Int(width - 1 - raw[i].x, raw[i].y);
            }

            return raw;
        }

        static void NormalizeV13SeedMotif(List<Vector2Int> points, out int width, out int height)
        {
            int minX = int.MaxValue, minY = int.MaxValue, maxX = int.MinValue, maxY = int.MinValue;
            foreach (var p in points)
            {
                minX = Mathf.Min(minX, p.x);
                minY = Mathf.Min(minY, p.y);
                maxX = Mathf.Max(maxX, p.x);
                maxY = Mathf.Max(maxY, p.y);
            }

            for (int i = 0; i < points.Count; i++)
                points[i] = new Vector2Int(points[i].x - minX, points[i].y - minY);
            width = maxX - minX + 1;
            height = maxY - minY + 1;
        }

        static bool IsContiguousPointPath(List<Vector2Int> points)
        {
            if (points == null || points.Count < 2)
                return false;

            for (int i = 1; i < points.Count; i++)
                if (Mathf.Abs(points[i].x - points[i - 1].x) + Mathf.Abs(points[i].y - points[i - 1].y) != 1)
                    return false;
            return true;
        }

        static int CountPointTurns(List<Vector2Int> points)
        {
            int turns = 0;
            for (int i = 2; i < points.Count; i++)
                if (points[i] - points[i - 1] != points[i - 1] - points[i - 2])
                    turns++;
            return turns;
        }

        static List<int> BuildV13LocalTemplateLane(int width, int height, bool[] canSpawn, bool[] occupied, int x0, int x1, int y0, int y1, int pattern)
        {
            if (x1 < x0 || y1 < y0)
                return new List<int>();

            if (pattern == 0)
                return BuildRowSnakeLane(width, height, canSpawn, occupied, x0, x1, y0, y1, forwardFirst: true);
            if (pattern == 1)
                return BuildRowSnakeLane(width, height, canSpawn, occupied, x0, x1, y0, y1, forwardFirst: false);

            var path = new List<int>();
            if (pattern == 2)
            {
                int midY = Mathf.Clamp((y0 + y1) / 2, y0, y1);
                for (int x = x0; x <= x1; x++)
                    if (!TryAppendFreeCellV09(path, width, height, canSpawn, occupied, x, midY))
                        return new List<int>();
                for (int y = midY + 1; y <= y1; y++)
                    if (!TryAppendFreeCellV09(path, width, height, canSpawn, occupied, x1, y))
                        return new List<int>();
                return IsContiguousPath(path, width) ? path : new List<int>();
            }

            if (pattern == 3)
            {
                for (int y = y0; y <= y1; y++)
                    if (!TryAppendFreeCellV09(path, width, height, canSpawn, occupied, x0, y))
                        return new List<int>();
                for (int x = x0 + 1; x <= x1; x++)
                    if (!TryAppendFreeCellV09(path, width, height, canSpawn, occupied, x, y1))
                        return new List<int>();
                for (int y = y1 - 1; y >= y0; y--)
                    if (!TryAppendFreeCellV09(path, width, height, canSpawn, occupied, x1, y))
                        return new List<int>();
                return IsContiguousPath(path, width) ? path : new List<int>();
            }

            if (pattern == 4)
            {
                int midY = Mathf.Clamp((y0 + y1) / 2, y0, y1);
                for (int x = x0; x <= x1; x++)
                    if (!TryAppendFreeCellV09(path, width, height, canSpawn, occupied, x, midY))
                        return new List<int>();
                return IsContiguousPath(path, width) ? path : new List<int>();
            }

            int yy = y0;
            bool forward = true;
            while (yy <= y1)
            {
                int from = forward ? x0 : x1;
                int to = forward ? x1 : x0;
                int step = forward ? 1 : -1;
                for (int x = from; step > 0 ? x <= to : x >= to; x += step)
                    if (!TryAppendFreeCellV09(path, width, height, canSpawn, occupied, x, yy))
                        return new List<int>();
                yy++;
                forward = !forward;
            }

            return IsContiguousPath(path, width) ? path : new List<int>();
        }

        static void AddV13LayeredLaneIfValid(List<int> lane, V10MotifKind kind, string region, int width, int height, bool[] canSpawn, bool[] occupied, List<V10Lane> lanes)
        {
            if (lane == null || lane.Count < 2 || !IsContiguousPath(lane, width))
                return;

            var path = EnsureV13LayeredOpenHead(lane, width, height, canSpawn);
            if (!LaneHeadHasV13LayeredExit(path, width, height, canSpawn))
                return;

            lanes.Add(new V10Lane { Cells = path, Kind = kind, Region = region });
            foreach (int i in path)
                occupied[i] = true;
        }

        static List<int> EnsureV13LayeredOpenHead(List<int> lane, int width, int height, bool[] canSpawn)
        {
            if (LaneHeadHasV13LayeredExit(lane, width, height, canSpawn))
                return lane;

            var reversed = new List<int>(lane);
            reversed.Reverse();
            return LaneHeadHasV13LayeredExit(reversed, width, height, canSpawn) ? reversed : lane;
        }

        static List<int> EnsureV13ClearOpenHead(List<int> lane, int width, int height, bool[] canSpawn, bool[] occupied)
        {
            if (LaneHeadHasV13ClearExit(lane, width, height, canSpawn, occupied))
                return lane;

            var reversed = new List<int>(lane);
            reversed.Reverse();
            if (LaneHeadHasV13ClearExit(reversed, width, height, canSpawn, occupied))
                return reversed;

            return EnsureV13LayeredOpenHead(lane, width, height, canSpawn);
        }

        static bool LaneHeadHasV13ClearExit(List<int> lane, int width, int height, bool[] canSpawn, bool[] occupied)
        {
            if (lane == null || lane.Count < 2 || canSpawn == null || occupied == null)
                return false;

            var head = IndexToPos(lane[^1], width);
            var prev = IndexToPos(lane[^2], width);
            var delta = head - prev;
            int x = head.x + delta.x;
            int y = head.y + delta.y;
            while (InBounds(width, height, x, y))
            {
                int idx = x + y * width;
                if (idx < 0 || idx >= canSpawn.Length || !canSpawn[idx])
                    return false;
                if (idx < occupied.Length && occupied[idx])
                    return false;

                x += delta.x;
                y += delta.y;
            }

            return true;
        }

        static bool LaneHeadHasV13LayeredExit(List<int> lane, int width, int height, bool[] canSpawn)
        {
            if (lane == null || lane.Count < 2 || canSpawn == null)
                return false;

            var head = IndexToPos(lane[^1], width);
            var prev = IndexToPos(lane[^2], width);
            var delta = head - prev;
            int x = head.x + delta.x;
            int y = head.y + delta.y;
            while (InBounds(width, height, x, y))
            {
                int idx = x + y * width;
                if (idx < 0 || idx >= canSpawn.Length || !canSpawn[idx])
                    return false;

                x += delta.x;
                y += delta.y;
            }

            return true;
        }

        static bool LaneHeadAvoidsV13Block(List<int> lane, int width, int height, bool[] canSpawn)
        {
            if (lane == null || lane.Count < 2 || canSpawn == null)
                return false;

            var head = IndexToPos(lane[^1], width);
            var prev = IndexToPos(lane[^2], width);
            var delta = head - prev;
            int x = head.x + delta.x;
            int y = head.y + delta.y;
            if (!InBounds(width, height, x, y))
                return true;

            int idx = x + y * width;
            return idx >= 0 && idx < canSpawn.Length && canSpawn[idx];
        }

        static int AddV13AuthoredResidualFill(int width, int height, bool[] canSpawn, bool[] occupied, AuthoredLevelData authored, int variant, out int turnCount)
        {
            turnCount = 0;
            if (canSpawn == null || occupied == null || authored?.arrows == null)
                return 0;

            int added = 0;
            bool horizontalFirst = (variant & 1) == 0;
            for (int pass = 0; pass < 2; pass++)
            {
                bool horizontal = pass == 0 ? horizontalFirst : !horizontalFirst;
                if (horizontal)
                    added += AddV13ResidualHorizontalChains(width, height, canSpawn, occupied, authored, variant + pass * 37, ref turnCount);
                else
                    added += AddV13ResidualVerticalChains(width, height, canSpawn, occupied, authored, variant + pass * 37, ref turnCount);
            }

            return added;
        }

        static int AddV13AuthoredResidualChunkFill(int width, int height, bool[] canSpawn, bool[] occupied, AuthoredLevelData authored, int variant, float targetFill, out int turnCount)
        {
            turnCount = 0;
            if (canSpawn == null || occupied == null || authored?.arrows == null)
                return 0;

            int allowed = CountAllowedCells(canSpawn);
            int target = Mathf.CeilToInt(allowed * targetFill);
            int current = CountAuthoredArrowTiles(authored);
            int added = 0;
            for (int pass = 0; pass < 4 && current < target; pass++)
            {
                bool horizontal = ((variant + pass) & 1) == 0;
                if (horizontal)
                    added += AddV13ResidualHorizontalChunks(width, height, canSpawn, occupied, authored, variant + pass * 41, target, ref current, ref turnCount);
                else
                    added += AddV13ResidualVerticalChunks(width, height, canSpawn, occupied, authored, variant + pass * 41, target, ref current, ref turnCount);
            }

            return added;
        }

        static int CountAuthoredArrowTiles(AuthoredLevelData authored)
        {
            int count = 0;
            if (authored?.arrows == null)
                return 0;

            foreach (var arrow in authored.arrows)
                count += arrow?.indices?.Count ?? 0;
            return count;
        }

        static int AddV13ResidualHorizontalChunks(int width, int height, bool[] canSpawn, bool[] occupied, AuthoredLevelData authored, int variant, int target, ref int current, ref int turnCount)
        {
            int added = 0;
            for (int y = 0; y < height && current < target; y++)
            {
                int x = 0;
                while (x < width && current < target)
                {
                    while (x < width && !IsV13ResidualFree(width, height, canSpawn, occupied, x, y))
                        x++;

                    var run = new List<int>();
                    while (x < width && IsV13ResidualFree(width, height, canSpawn, occupied, x, y))
                    {
                        run.Add(x + y * width);
                        x++;
                    }

                    added += AddV13ResidualRunChunked(run, width, occupied, authored, variant + y * 17, target, ref current, ref turnCount);
                }
            }

            return added;
        }

        static int AddV13ResidualVerticalChunks(int width, int height, bool[] canSpawn, bool[] occupied, AuthoredLevelData authored, int variant, int target, ref int current, ref int turnCount)
        {
            int added = 0;
            for (int x = 0; x < width && current < target; x++)
            {
                int y = 0;
                while (y < height && current < target)
                {
                    while (y < height && !IsV13ResidualFree(width, height, canSpawn, occupied, x, y))
                        y++;

                    var run = new List<int>();
                    while (y < height && IsV13ResidualFree(width, height, canSpawn, occupied, x, y))
                    {
                        run.Add(x + y * width);
                        y++;
                    }

                    added += AddV13ResidualRunChunked(run, width, occupied, authored, variant + x * 19, target, ref current, ref turnCount);
                }
            }

            return added;
        }

        static int AddV13ResidualRunChunked(List<int> run, int width, bool[] occupied, AuthoredLevelData authored, int variant, int target, ref int current, ref int turnCount)
        {
            if (run == null || run.Count < 2)
                return 0;

            int added = 0;
            int cursor = 0;
            int height = Mathf.Max(1, occupied.Length / Mathf.Max(1, width));
            while (run.Count - cursor >= 2 && current < target)
            {
                int remaining = run.Count - cursor;
                int len = Mathf.Min(remaining, 4 + PositiveMod(variant + cursor * 7, 8));
                if (remaining - len == 1)
                    len--;
                if (len < 2)
                    break;

                int first = run[cursor];
                int last = run[cursor + len - 1];
                int step = run.Count >= 2 ? run[1] - run[0] : 1;
                bool horizontal = Mathf.Abs(step) == 1;
                bool startBoundary = horizontal ? first % width == 0 : first / width == 0;
                bool endBoundary = horizontal ? last % width == width - 1 : last / width == height - 1;
                bool hasStartGap = cursor > 0 || startBoundary;
                bool hasEndGap = cursor + len < run.Count || endBoundary;
                if (!hasStartGap && !hasEndGap)
                    break;

                var path = run.GetRange(cursor, len);
                bool headToStart = hasStartGap && (!hasEndGap || PositiveMod(variant + cursor * 13, 3) == 0);
                if (headToStart)
                    path.Reverse();

                AddAuthoredChain(authored, path, authored.arrows.Count);
                foreach (int idx in path)
                    occupied[idx] = true;
                turnCount += CountLaneTurns(path, width);
                current += path.Count;
                added++;
                cursor += cursor + len < run.Count ? len + 1 : len;
            }

            return added;
        }

        static int AddV13ResidualHorizontalChains(int width, int height, bool[] canSpawn, bool[] occupied, AuthoredLevelData authored, int variant, ref int turnCount)
        {
            int added = 0;
            for (int y = 0; y < height; y++)
            {
                int x = 0;
                while (x < width)
                {
                    while (x < width && !IsV13ResidualFree(width, height, canSpawn, occupied, x, y))
                        x++;

                    var run = new List<int>();
                    while (x < width && IsV13ResidualFree(width, height, canSpawn, occupied, x, y))
                    {
                        run.Add(x + y * width);
                        x++;
                    }

                    added += AddV13ResidualRunChunks(run, width, height, canSpawn, occupied, authored, variant + y * 11, ref turnCount);
                }
            }

            return added;
        }

        static int AddV13ResidualVerticalChains(int width, int height, bool[] canSpawn, bool[] occupied, AuthoredLevelData authored, int variant, ref int turnCount)
        {
            int added = 0;
            for (int x = 0; x < width; x++)
            {
                int y = 0;
                while (y < height)
                {
                    while (y < height && !IsV13ResidualFree(width, height, canSpawn, occupied, x, y))
                        y++;

                    var run = new List<int>();
                    while (y < height && IsV13ResidualFree(width, height, canSpawn, occupied, x, y))
                    {
                        run.Add(x + y * width);
                        y++;
                    }

                    added += AddV13ResidualRunChunks(run, width, height, canSpawn, occupied, authored, variant + x * 13, ref turnCount);
                }
            }

            return added;
        }

        static int AddV13ResidualRunChunks(List<int> run, int width, int height, bool[] canSpawn, bool[] occupied, AuthoredLevelData authored, int variant, ref int turnCount)
        {
            if (run == null || run.Count < 2)
                return 0;

            if (TryAddV13BoundaryResidualRun(run, width, occupied, authored, ref turnCount))
                return 1;

            if (run.Count < 3)
                return 0;

            if (TryAddV13OpenResidualRun(run, width, height, canSpawn, occupied, authored, ref turnCount))
                return 1;

            return 0;

            int added = 0;
            int cursor = 0;
            while (run.Count - cursor >= 3)
            {
                int remaining = run.Count - cursor;
                int targetLen = 14 + PositiveMod(variant + cursor * 5, 14);
                int len = cursor == 0
                    ? Mathf.Min(Mathf.Max(2, remaining - 1), targetLen)
                    : Mathf.Min(remaining, targetLen);
                bool hasRightGap = remaining > len;
                bool hasLeftGap = cursor > 0;
                if (!hasRightGap && !hasLeftGap)
                    break;

                if (!hasRightGap && len == remaining)
                    len = Mathf.Max(2, remaining - 1);

                var path = run.GetRange(cursor, len);
                bool headToLeftGap = hasLeftGap && (!hasRightGap || ((variant + cursor) & 3) == 1);
                if (headToLeftGap)
                    path.Reverse();

                AddAuthoredChain(authored, path, authored.arrows.Count);
                foreach (int idx in path)
                    occupied[idx] = true;
                turnCount += CountLaneTurns(path, width);
                added++;
                cursor += hasRightGap ? len + 1 : len;
            }

            return added;
        }

        static bool TryAddV13BoundaryResidualRun(List<int> run, int width, bool[] occupied, AuthoredLevelData authored, ref int turnCount)
        {
            if (run == null || run.Count < 2 || width <= 0 || occupied == null || occupied.Length == 0)
                return false;

            int height = Mathf.Max(1, occupied.Length / width);
            int first = run[0];
            int last = run[run.Count - 1];
            int step = run[1] - run[0];
            bool reverse = false;
            bool exits = false;

            if (step == 1 || step == -1)
            {
                if (last % width == width - 1)
                {
                    reverse = false;
                    exits = true;
                }
                else if (first % width == 0)
                {
                    reverse = true;
                    exits = true;
                }
            }
            else if (Mathf.Abs(step) == width)
            {
                if (last / width == height - 1)
                {
                    reverse = false;
                    exits = true;
                }
                else if (first / width == 0)
                {
                    reverse = true;
                    exits = true;
                }
            }

            if (!exits)
                return false;

            var path = new List<int>(run);
            if (reverse)
                path.Reverse();

            AddAuthoredChain(authored, path, authored.arrows.Count);
            foreach (int idx in path)
                occupied[idx] = true;
            turnCount += CountLaneTurns(path, width);
            return true;
        }

        static bool TryAddV13OpenResidualRun(List<int> run, int width, int height, bool[] canSpawn, bool[] occupied, AuthoredLevelData authored, ref int turnCount)
        {
            if (run == null || run.Count < 3 || width <= 0 || height <= 0 || canSpawn == null || occupied == null)
                return false;

            int first = run[0];
            int last = run[run.Count - 1];
            int step = run[1] - run[0];
            bool normalOpen = HasV13ClearLineToExit(last, step, width, height, canSpawn, occupied);
            bool reverseOpen = HasV13ClearLineToExit(first, -step, width, height, canSpawn, occupied);
            if (!normalOpen && !reverseOpen)
                return false;

            var path = new List<int>(run);
            if (!normalOpen && reverseOpen)
                path.Reverse();

            AddAuthoredChain(authored, path, authored.arrows.Count);
            foreach (int idx in path)
                occupied[idx] = true;
            turnCount += CountLaneTurns(path, width);
            return true;
        }

        static bool HasV13ClearLineToExit(int headIndex, int step, int width, int height, bool[] canSpawn, bool[] occupied)
        {
            if (step == 0 || canSpawn == null || occupied == null)
                return false;

            int x = headIndex % width;
            int y = headIndex / width;
            int dx = 0;
            int dy = 0;
            if (step == 1) dx = 1;
            else if (step == -1) dx = -1;
            else if (step == width) dy = 1;
            else if (step == -width) dy = -1;
            else return false;

            x += dx;
            y += dy;
            while (InBounds(width, height, x, y))
            {
                int idx = x + y * width;
                if (idx < 0 || idx >= canSpawn.Length || idx >= occupied.Length || !canSpawn[idx] || occupied[idx])
                    return false;

                x += dx;
                y += dy;
            }

            return true;
        }

        static bool IsV13ResidualFree(int width, int height, bool[] canSpawn, bool[] occupied, int x, int y)
        {
            if (!InBounds(width, height, x, y))
                return false;

            int idx = x + y * width;
            return idx >= 0 && idx < canSpawn.Length && idx < occupied.Length && canSpawn[idx] && !occupied[idx];
        }

        static void AddV13SparseResidualRuns(int width, int height, bool[] canSpawn, bool[] occupied, List<V10Lane> lanes, int variant)
        {
            int runId = 0;
            for (int y = 0; y < height; y++)
            {
                int x = 0;
                while (x < width)
                {
                    while (x < width && (!canSpawn[x + y * width] || occupied[x + y * width]))
                        x++;

                    var run = new List<int>();
                    while (x < width && canSpawn[x + y * width] && !occupied[x + y * width])
                    {
                        run.Add(x + y * width);
                        x++;
                    }

                    if (run.Count >= 3)
                    {
                        int cursor = 0;
                        while (run.Count - cursor >= 3)
                        {
                            int len = Mathf.Min(run.Count - cursor, 3 + PositiveMod(variant + runId + cursor, 5));
                            if (run.Count - cursor - len > 0 && run.Count - cursor - len < 3)
                                len = run.Count - cursor;
                            AddV10LaneIfValid(run.GetRange(cursor, len), V10MotifKind.ShortBridge, "ResidualShortBridge", width, height, occupied, lanes);
                            cursor += len;
                        }
                    }

                    runId++;
                }
            }
        }

        static List<LaneCageCandidate> SelectDiverseTemplateVisualV12(List<LaneCageCandidate> sorted, int topCount)
        {
            var picked = new List<LaneCageCandidate>();
            var visualSignatures = new HashSet<string>();
            var boardSignatures = new HashSet<ulong>();
            foreach (var candidate in sorted)
            {
                if (!boardSignatures.Add(CalculateBoardSignatureV11(candidate.Board)))
                    continue;

                string signature = $"{candidate.ChainRoleSummary}|{candidate.ChainShapeSummary}|{candidate.RegionSignature}";
                if (!visualSignatures.Add(signature))
                    continue;

                picked.Add(candidate);
                if (picked.Count >= topCount)
                    return picked;
            }

            foreach (var candidate in sorted)
            {
                if (picked.Any(p => p.Seed == candidate.Seed))
                    continue;
                if (!boardSignatures.Add(CalculateBoardSignatureV11(candidate.Board)))
                    continue;

                picked.Add(candidate);
                if (picked.Count >= topCount)
                    break;
            }

            return picked;
        }

        static V12VisualStats AnalyzeChainGroupVisualV12(AuthoredLevelData authored, int width, int height)
        {
            var chains = new List<V12ChainVisual>();
            if (authored?.arrows == null)
                return V12VisualStats.Empty;

            var owner = new Dictionary<int, int>();
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var indices = authored.arrows[i]?.indices;
                if (indices == null || indices.Count == 0)
                    continue;

                foreach (int index in indices)
                    if (!owner.ContainsKey(index))
                        owner[index] = chains.Count;

                var bounds = BuildV12Bounds(indices, width);
                chains.Add(new V12ChainVisual
                {
                    Index = i,
                    Length = indices.Count,
                    Bounds = bounds,
                    Shape = ClassifyV12Shape(indices, width),
                    Axis = ClassifyV12Axis(indices, width),
                    Region = ClassifyV12Region(bounds, width, height)
                });
            }

            for (int i = 0; i < chains.Count; i++)
            {
                var c = chains[i];
                c.AdjacentCount = CountV12AdjacentChains(authored.arrows[c.Index].indices, owner, i, width, height);
                c.Role = ClassifyV12Role(c);
                chains[i] = c;
            }

            float penalty = 0f;
            var sorted = chains.OrderBy(c => c.Region).ThenBy(c => c.Bounds.y).ThenBy(c => c.Bounds.x).ToList();
            for (int i = 1; i < sorted.Count; i++)
            {
                var prev = sorted[i - 1];
                var cur = sorted[i];
                if (prev.Region != cur.Region)
                    continue;

                if (prev.Shape == cur.Shape) penalty += 0.10f;
                if (prev.Role == cur.Role) penalty += 0.08f;
                if (prev.Axis == cur.Axis) penalty += 0.06f;
                if (prev.Shape == cur.Shape && prev.Role == cur.Role && prev.Axis == cur.Axis)
                    penalty += 0.22f;
            }

            float largestDominance = 0f;
            foreach (var region in chains.GroupBy(c => c.Region))
            {
                int count = region.Count();
                if (count <= 1)
                    continue;

                int dominant = region.GroupBy(c => $"{c.Role}/{c.Shape}/{c.Axis}").Max(g => g.Count());
                float dominance = dominant / (float)count;
                largestDominance = Mathf.Max(largestDominance, dominance);
                if (dominance > 0.55f)
                    penalty += (dominance - 0.55f) * 1.4f;
            }

            int roleKinds = chains.Select(c => c.Role).Distinct().Count();
            int shapeKinds = chains.Select(c => c.Shape).Distinct().Count();
            if (roleKinds >= 4) penalty -= 0.25f;
            if (shapeKinds >= 5) penalty -= 0.20f;

            return new V12VisualStats
            {
                RegularityPenalty = Mathf.Max(0f, penalty),
                RoleKindCount = roleKinds,
                ShapeKindCount = shapeKinds,
                LargestRegionDominance = largestDominance,
                RoleSummary = BuildV12Summary(chains.Select(c => c.Role)),
                ShapeSummary = BuildV12Summary(chains.Select(c => c.Shape)),
                RegionSignature = BuildV12RegionSignature(chains)
            };
        }

        static int CountV12AdjacentChains(List<int> indices, Dictionary<int, int> owner, int self, int width, int height)
        {
            var adjacent = new HashSet<int>();
            foreach (int idx in indices)
            {
                int x = idx % width;
                int y = idx / width;
                TryAddV12Adjacent(owner, adjacent, self, x + 1, y, width, height);
                TryAddV12Adjacent(owner, adjacent, self, x - 1, y, width, height);
                TryAddV12Adjacent(owner, adjacent, self, x, y + 1, width, height);
                TryAddV12Adjacent(owner, adjacent, self, x, y - 1, width, height);
            }

            return adjacent.Count;
        }

        static void TryAddV12Adjacent(Dictionary<int, int> owner, HashSet<int> adjacent, int self, int x, int y, int width, int height)
        {
            if (!InBounds(width, height, x, y))
                return;

            if (owner.TryGetValue(x + y * width, out int other) && other != self)
                adjacent.Add(other);
        }

        static RectInt BuildV12Bounds(List<int> indices, int width)
        {
            int minX = int.MaxValue, minY = int.MaxValue, maxX = -1, maxY = -1;
            foreach (int idx in indices)
            {
                int x = idx % width;
                int y = idx / width;
                minX = Mathf.Min(minX, x);
                minY = Mathf.Min(minY, y);
                maxX = Mathf.Max(maxX, x);
                maxY = Mathf.Max(maxY, y);
            }

            return new RectInt(minX, minY, maxX - minX + 1, maxY - minY + 1);
        }

        static string ClassifyV12Shape(List<int> indices, int width)
        {
            if (indices.Count <= 2)
                return "Stub";

            int turns = 0;
            for (int i = 2; i < indices.Count; i++)
            {
                var a = IndexToPos(indices[i - 1], width) - IndexToPos(indices[i - 2], width);
                var b = IndexToPos(indices[i], width) - IndexToPos(indices[i - 1], width);
                if (a != b)
                    turns++;
            }

            var bounds = BuildV12Bounds(indices, width);
            if (turns == 0) return "Line";
            if (turns == 1) return "L";
            if (turns == 2 && bounds.width > 1 && bounds.height > 1) return "U/S";
            if (turns <= 4) return "Hook";
            return "Snake";
        }

        static string ClassifyV12Axis(List<int> indices, int width)
        {
            var bounds = BuildV12Bounds(indices, width);
            if (bounds.width >= bounds.height * 2) return "H";
            if (bounds.height >= bounds.width * 2) return "V";
            var start = IndexToPos(indices[0], width);
            var end = IndexToPos(indices[^1], width);
            return Mathf.Abs(end.x - start.x) >= Mathf.Abs(end.y - start.y) ? "H" : "V";
        }

        static string ClassifyV12Region(RectInt bounds, int width, int height)
        {
            float cx = bounds.center.x;
            float cy = bounds.center.y;
            bool left = cx < width * 0.38f;
            bool right = cx > width * 0.62f;
            bool bottom = cy < height * 0.34f;
            bool top = cy > height * 0.66f;
            if (top && left) return "TL";
            if (top && right) return "TR";
            if (bottom && left) return "BL";
            if (bottom && right) return "BR";
            if (top) return "T";
            if (bottom) return "B";
            if (left) return "L";
            if (right) return "R";
            return "C";
        }

        static string ClassifyV12Role(V12ChainVisual c)
        {
            if (c.Length >= 18 && c.AdjacentCount >= 3) return "Spine";
            if (c.AdjacentCount >= 5) return "Weave";
            if (c.Length <= 4 && c.AdjacentCount >= 2) return "Cap";
            if (c.Shape == "U/S" || c.Shape == "Hook") return "Motif";
            return "Patch";
        }

        static string BuildV12Summary(IEnumerable<string> values)
        {
            return string.Join("|", values.GroupBy(v => v).OrderByDescending(g => g.Count()).ThenBy(g => g.Key).Select(g => $"{g.Key}:{g.Count()}"));
        }

        static string BuildV12RegionSignature(List<V12ChainVisual> chains)
        {
            return string.Join("|", chains
                .GroupBy(c => c.Region)
                .OrderBy(g => g.Key)
                .Select(g => $"{g.Key}:{string.Join("+", g.GroupBy(c => c.Role).OrderByDescending(r => r.Count()).Select(r => $"{r.Key}{r.Count()}"))}"));
        }

        static List<LaneCageCandidate> SelectDiverseLayeredMacroV11(List<LaneCageCandidate> sorted, int topCount)
        {
            var picked = new List<LaneCageCandidate>();
            var signatures = new HashSet<int>();
            var boardSignatures = new HashSet<ulong>();
            foreach (var candidate in sorted)
            {
                if (!boardSignatures.Add(CalculateBoardSignatureV11(candidate.Board)))
                    continue;

                int variant = Mathf.Max(0, candidate.Seed - 910000);
                int layerBucket = Mathf.Clamp(candidate.LayerCount, 0, 7);
                int stripeBucket = Mathf.Clamp(Mathf.RoundToInt(candidate.MacroStripePenalty * 10f), 0, 7);
                int signature = PositiveMod(variant, 7) | (PositiveMod(variant / 7, 5) << 4) | (layerBucket << 8) | (stripeBucket << 12);
                if (!signatures.Add(signature))
                    continue;

                picked.Add(candidate);
                if (picked.Count >= topCount)
                    return picked;
            }

            foreach (var candidate in sorted)
            {
                if (picked.Any(p => p.Seed == candidate.Seed))
                    continue;
                if (!boardSignatures.Add(CalculateBoardSignatureV11(candidate.Board)))
                    continue;

                picked.Add(candidate);
                if (picked.Count >= topCount)
                    break;
            }

            return picked;
        }

        static bool TryBuildLayeredMacroV11Authored(int width, int height, bool[] canSpawn, int variant, out AuthoredLevelData authored, out int moduleCount, out int turnCount, out V10MotifStats motifStats)
        {
            authored = new AuthoredLevelData
            {
                width = width,
                height = height,
                arrows = new List<AuthoredArrowData>()
            };
            moduleCount = 0;
            turnCount = 0;
            motifStats = default;

            var authoredBlockIndices = new List<int>();
            for (int i = 0; i < canSpawn.Length; i++)
                if (!canSpawn[i])
                    authoredBlockIndices.Add(i);
            SetAuthoredBlockIndices(authored, authoredBlockIndices);

            if (!TryFindBlockBounds(width, height, canSpawn, out int minX, out int maxX, out int minY, out int maxY))
                return false;

            var occupied = new bool[width * height];
            var lanes = new List<V10Lane>();
            AddV10HoleEdgeDetours(width, height, canSpawn, occupied, lanes, minX, maxX, minY, maxY, variant);
            AddV10SideHooks(width, height, canSpawn, occupied, lanes, minX, maxX, minY, maxY, variant + 19);
            AddV11TopBottomMacroBand(width, height, canSpawn, occupied, lanes, 0, width - 1, maxY + 1, height - 1, variant, 0, "TopMacro");
            AddV11TopBottomMacroBand(width, height, canSpawn, occupied, lanes, 0, width - 1, minY - 1, 0, variant, 31, "BottomMacro");
            AddV11SideMacroBand(width, height, canSpawn, occupied, lanes, 0, minX - 1, minY, maxY, variant, 53, "LeftMacro");
            AddV11SideMacroBand(width, height, canSpawn, occupied, lanes, maxX + 1, width - 1, minY, maxY, variant, 71, "RightMacro");
            AddV10LeftoverRowRunLanes(width, height, canSpawn, occupied, lanes);

            foreach (var lane in lanes)
            {
                if (lane.Cells == null || lane.Cells.Count < 2)
                    continue;

                var path = EnsureOpenHead(lane.Cells, width, height);
                if (path.Count < 2 || !LaneHeadExits(path, width, height))
                    continue;

                moduleCount++;
                if (((variant + moduleCount * 13) % 11) < 5 && TryReverseModuleForDependencyV09(path, width, height, canSpawn, out var dependencyPath))
                    path = dependencyPath;

                int turns = CountLaneTurns(path, width);
                turnCount += turns;
                int maxLen = turns == 0 ? 9 + PositiveMod(variant + moduleCount, 4) : 10 + PositiveMod(variant + moduleCount * 3, 4);
                var chains = turns == 0
                    ? SplitStraightLaneSeedLikeChains(path, width, minLen: 4, maxLen: maxLen, moduleCount - 1)
                    : SplitLaneTailDependencyChains(path, width, minLen: 4, maxLen: maxLen, moduleCount - 1);

                foreach (var chain in chains)
                    AddAuthoredChain(authored, chain.InnerToOuter, authored.arrows.Count);
            }

            motifStats = CalculateV10MotifStats(lanes, width, height, canSpawn);
            return authored.arrows.Count > 0;
        }

        static void AddV11TopBottomMacroBand(int width, int height, bool[] canSpawn, bool[] occupied, List<V10Lane> lanes, int xStart, int xEnd, int yStart, int yEnd, int variant, int salt, string region)
        {
            if (xEnd < xStart || (yStart <= yEnd ? yStart > yEnd : yStart < yEnd))
                return;

            int x = xStart;
            int block = 0;
            while (x <= xEnd)
            {
                int cols = 2 + PositiveMod(variant + salt + block * 7, 4);
                int end = Mathf.Min(xEnd, x + cols - 1);
                if (xEnd - end == 1)
                    end = xEnd;

                bool upFirst = ((variant + salt + block * 5) & 1) == 0;
                var kind = block % 4 == 0 ? V10MotifKind.SwerveLong : (block % 4 == 1 ? V10MotifKind.ZigZagLite : (block % 4 == 2 ? V10MotifKind.UShape : V10MotifKind.ShortBridge));
                AddV10LaneIfValid(BuildColumnSnakeLane(width, height, canSpawn, occupied, x, end, yStart, yEnd, upFirst), kind, region, width, height, occupied, lanes);
                x = end + 1;
                block++;
            }
        }

        static void AddV11SideMacroBand(int width, int height, bool[] canSpawn, bool[] occupied, List<V10Lane> lanes, int xStart, int xEnd, int yStart, int yEnd, int variant, int salt, string region)
        {
            if (xEnd < xStart || yEnd < yStart)
                return;

            int y = yStart;
            int block = 0;
            while (y <= yEnd)
            {
                int rows = 2 + PositiveMod(variant + salt + block * 5, 3);
                int end = Mathf.Min(yEnd, y + rows - 1);
                if (yEnd - end == 1)
                    end = yEnd;

                bool forwardFirst = ((variant + salt + block * 3) & 1) == 0;
                var kind = block % 4 == 0 ? V10MotifKind.Hook : (block % 4 == 1 ? V10MotifKind.Turnback : (block % 4 == 2 ? V10MotifKind.ShortBridge : V10MotifKind.StraightWithBend));
                AddV10LaneIfValid(BuildRowSnakeLane(width, height, canSpawn, occupied, xStart, xEnd, y, end, forwardFirst), kind, region, width, height, occupied, lanes);
                y = end + 1;
                block++;
            }
        }

        static ulong CalculateBoardSignatureV11(BoardState board)
        {
            const ulong offset = 1469598103934665603UL;
            const ulong prime = 1099511628211UL;
            ulong hash = offset;
            if (board == null || board.tiles == null)
                return hash;

            for (int i = 0; i < board.tiles.Length; i++)
            {
                var tile = board.tiles[i];
                ulong v = (ulong)tile.type + 17UL * (ulong)tile.arrow.inDir + 97UL * (ulong)tile.arrow.outDir;
                hash ^= v + (ulong)i * 131UL;
                hash *= prime;
            }

            return hash;
        }

        static List<LaneCageCandidate> SelectDiverseMotifDiversityV10(List<LaneCageCandidate> sorted, int topCount)
        {
            var picked = new List<LaneCageCandidate>();
            var signatures = new HashSet<int>();
            foreach (var candidate in sorted)
            {
                int variant = Mathf.Max(0, candidate.Seed - 900000);
                int signature = PositiveMod(variant, 5) | (PositiveMod(variant / 5, 6) << 4);
                if (!signatures.Add(signature))
                    continue;

                picked.Add(candidate);
                if (picked.Count >= topCount)
                    return picked;
            }

            foreach (var candidate in sorted)
            {
                if (picked.Any(p => p.Seed == candidate.Seed))
                    continue;

                picked.Add(candidate);
                if (picked.Count >= topCount)
                    break;
            }

            return picked;
        }

        struct V10MotifStats
        {
            public int MotifKindCount;
            public float StraightChainRatio;
            public float RegionDirectionEntropy;
            public float RepeatedMotifPenalty;
            public float HoleEdgeCoverage;
            public string Plan;
        }

        struct V11LayerStats
        {
            public int InitialChainCount;
            public int LayerCount;
            public int Layer0Chains;
            public float Layer0Ratio;
            public int MaxLayerWidth;
            public int DeadChains;
            public int UnexpectedEarlyOpen;
            public float LayerBalance;
            public int GreedyMoves;
            public string LayerSummary;
        }

        struct V12VisualStats
        {
            public float RegularityPenalty;
            public int RoleKindCount;
            public int ShapeKindCount;
            public float LargestRegionDominance;
            public string RoleSummary;
            public string ShapeSummary;
            public string RegionSignature;

            public static readonly V12VisualStats Empty = new V12VisualStats
            {
                RegularityPenalty = 999f,
                RoleSummary = "none",
                ShapeSummary = "none",
                RegionSignature = "none"
            };
        }

        struct V12ChainVisual
        {
            public int Index;
            public int Length;
            public RectInt Bounds;
            public string Shape;
            public string Axis;
            public string Region;
            public int AdjacentCount;
            public string Role;
        }

        static bool TryBuildMotifDiversityV10Authored(int width, int height, bool[] canSpawn, int variant, out AuthoredLevelData authored, out int moduleCount, out int turnCount, out V10MotifStats motifStats)
        {
            authored = new AuthoredLevelData
            {
                width = width,
                height = height,
                arrows = new List<AuthoredArrowData>()
            };
            moduleCount = 0;
            turnCount = 0;
            motifStats = default;

            var authoredBlockIndices = new List<int>();
            for (int i = 0; i < canSpawn.Length; i++)
                if (!canSpawn[i])
                    authoredBlockIndices.Add(i);
            SetAuthoredBlockIndices(authored, authoredBlockIndices);

            if (!TryFindBlockBounds(width, height, canSpawn, out int minX, out int maxX, out int minY, out int maxY))
                return false;

            var occupied = new bool[width * height];
            var lanes = new List<V10Lane>();
            AddV10HoleEdgeDetours(width, height, canSpawn, occupied, lanes, minX, maxX, minY, maxY, variant);
            AddV10SideHooks(width, height, canSpawn, occupied, lanes, minX, maxX, minY, maxY, variant);
            AddV10TopBottomMixedBand(width, height, canSpawn, occupied, lanes, 0, width - 1, maxY + 1, height - 1, variant, 0, "TopBand");
            AddV10TopBottomMixedBand(width, height, canSpawn, occupied, lanes, 0, width - 1, minY - 1, 0, variant, 17, "BottomBand");
            AddV10SideRunBands(width, height, canSpawn, occupied, lanes, 0, minX - 1, minY, maxY, variant, 31, "LeftBand");
            AddV10SideRunBands(width, height, canSpawn, occupied, lanes, maxX + 1, width - 1, minY, maxY, variant, 47, "RightBand");
            AddV10LeftoverRowRunLanes(width, height, canSpawn, occupied, lanes);

            foreach (var lane in lanes)
            {
                if (lane.Cells == null || lane.Cells.Count < 2)
                    continue;

                var path = EnsureOpenHead(lane.Cells, width, height);
                if (path.Count < 2 || !LaneHeadExits(path, width, height))
                    continue;

                moduleCount++;
                if (((variant + moduleCount * 11) % 10) < 5 && TryReverseModuleForDependencyV09(path, width, height, canSpawn, out var dependencyPath))
                    path = dependencyPath;

                int turns = CountLaneTurns(path, width);
                turnCount += turns;
                var chains = turns == 0
                    ? SplitStraightLaneSeedLikeChains(path, width, minLen: 4, maxLen: 8 + PositiveMod(variant + moduleCount, 3), moduleCount - 1)
                    : SplitLaneTailDependencyChains(path, width, minLen: 4, maxLen: 8 + PositiveMod(variant + moduleCount, 4), moduleCount - 1);

                foreach (var chain in chains)
                    AddAuthoredChain(authored, chain.InnerToOuter, authored.arrows.Count);
            }

            motifStats = CalculateV10MotifStats(lanes, width, height, canSpawn);
            return authored.arrows.Count > 0;
        }

        static void AddV10HoleEdgeDetours(int width, int height, bool[] canSpawn, bool[] occupied, List<V10Lane> lanes, int minX, int maxX, int minY, int maxY, int variant)
        {
            if ((variant & 1) == 0)
            {
                AddV10LaneIfValid(BuildRimHookLaneV09(width, height, canSpawn, occupied, minX - 1, minY, maxY + 1, 0, maxY + 1), V10MotifKind.HoleEdgeDetour, "InnerHoleBand", width, height, occupied, lanes);
                AddV10LaneIfValid(BuildRimHookLaneV09(width, height, canSpawn, occupied, maxX + 1, maxY, minY - 1, width - 1, minY - 1), V10MotifKind.HoleEdgeDetour, "InnerHoleBand", width, height, occupied, lanes);
            }
            else
            {
                AddV10LaneIfValid(BuildRimHookLaneV09(width, height, canSpawn, occupied, maxX + 1, minY, maxY + 1, width - 1, maxY + 1), V10MotifKind.HoleEdgeDetour, "InnerHoleBand", width, height, occupied, lanes);
                AddV10LaneIfValid(BuildRimHookLaneV09(width, height, canSpawn, occupied, minX - 1, maxY, minY - 1, 0, minY - 1), V10MotifKind.HoleEdgeDetour, "InnerHoleBand", width, height, occupied, lanes);
            }
        }

        static void AddV10SideHooks(int width, int height, bool[] canSpawn, bool[] occupied, List<V10Lane> lanes, int minX, int maxX, int minY, int maxY, int variant)
        {
            int span = Mathf.Max(1, maxY - minY + 1);
            int hookCount = Mathf.Clamp(2 + PositiveMod(variant, 3), 2, 4);
            for (int i = 0; i < hookCount; i++)
            {
                int y = minY + Mathf.Clamp((i + 1) * span / (hookCount + 1), 0, span - 1);
                int exitY = ((variant + i) & 1) == 0 ? 0 : height - 1;
                var kind = i % 2 == 0 ? V10MotifKind.Hook : V10MotifKind.Turnback;
                AddV10LaneIfValid(BuildV10SideHookLane(width, height, canSpawn, occupied, leftSide: true, minX - 1, y, exitY), kind, "LeftBand", width, height, occupied, lanes);
                AddV10LaneIfValid(BuildV10SideHookLane(width, height, canSpawn, occupied, leftSide: false, maxX + 1, y, height - 1 - exitY), kind, "RightBand", width, height, occupied, lanes);
            }
        }

        static List<int> BuildV10SideHookLane(int width, int height, bool[] canSpawn, bool[] occupied, bool leftSide, int innerX, int targetY, int exitY)
        {
            var path = new List<int>();
            if (innerX < 0 || innerX >= width || targetY < 0 || targetY >= height || exitY < 0 || exitY >= height)
                return path;

            int outerX = leftSide ? 0 : width - 1;
            int xStep = leftSide ? -1 : 1;
            for (int x = innerX; leftSide ? x >= outerX : x <= outerX; x += xStep)
                if (!TryAppendFreeCellV09(path, width, height, canSpawn, occupied, x, targetY))
                    return new List<int>();

            int yStep = targetY <= exitY ? 1 : -1;
            for (int y = targetY + yStep; yStep > 0 ? y <= exitY : y >= exitY; y += yStep)
                if (!TryAppendFreeCellV09(path, width, height, canSpawn, occupied, outerX, y))
                    return new List<int>();

            return IsContiguousPath(path, width) ? path : new List<int>();
        }

        static void AddV10TopBottomHooks(int width, int height, bool[] canSpawn, bool[] occupied, List<V10Lane> lanes, int yStart, int yEnd, int variant, int salt, string region)
        {
            if (yStart < 0 || yStart >= height || yEnd < 0 || yEnd >= height)
                return;

            int span = Mathf.Abs(yEnd - yStart) + 1;
            if (span < 3)
                return;

            bool top = yStart <= yEnd;
            int hookCount = 3 + PositiveMod(variant + salt, 3);
            for (int i = 0; i < hookCount; i++)
            {
                int x = Mathf.Clamp((i + 1) * width / (hookCount + 1) + PositiveMod(variant + salt + i * 3, 3) - 1, 1, width - 2);
                int targetY = yStart + (top ? 1 : -1) * Mathf.Clamp(1 + PositiveMod(variant + salt + i, Mathf.Max(1, span - 1)), 1, span - 1);
                int hookLen = 2 + PositiveMod(variant + salt + i * 5, 5);
                bool right = ((variant + salt + i) & 1) == 0;
                int horizontalEnd = Mathf.Clamp(x + (right ? hookLen : -hookLen), 0, width - 1);
                var kind = i % 3 == 0 ? V10MotifKind.Hook : (i % 3 == 1 ? V10MotifKind.Turnback : V10MotifKind.UShape);
                AddV10LaneIfValid(BuildV10TopBottomHookLane(width, height, canSpawn, occupied, x, targetY, horizontalEnd, top ? height - 1 : 0), kind, region, width, height, occupied, lanes);
            }
        }

        static List<int> BuildV10TopBottomHookLane(int width, int height, bool[] canSpawn, bool[] occupied, int x, int targetY, int horizontalEnd, int exitY)
        {
            var path = new List<int>();
            if (x < 0 || x >= width || targetY < 0 || targetY >= height || horizontalEnd < 0 || horizontalEnd >= width || exitY < 0 || exitY >= height)
                return path;

            int xStep = horizontalEnd <= x ? 1 : -1;
            for (int hx = horizontalEnd; xStep > 0 ? hx <= x : hx >= x; hx += xStep)
                if (!TryAppendFreeCellV09(path, width, height, canSpawn, occupied, hx, targetY))
                    return new List<int>();

            int yStep = targetY <= exitY ? 1 : -1;
            for (int y = targetY + yStep; yStep > 0 ? y <= exitY : y >= exitY; y += yStep)
                if (!TryAppendFreeCellV09(path, width, height, canSpawn, occupied, x, y))
                    return new List<int>();

            return IsContiguousPath(path, width) ? path : new List<int>();
        }

        static void AddV10TopBottomMixedBand(int width, int height, bool[] canSpawn, bool[] occupied, List<V10Lane> lanes, int xStart, int xEnd, int yStart, int yEnd, int variant, int salt, string region)
        {
            if (xEnd < xStart || (yStart <= yEnd ? yStart > yEnd : yStart < yEnd))
                return;

            int span = Mathf.Abs(yEnd - yStart) + 1;
            int yStep = yStart <= yEnd ? 1 : -1;
            int innerRows = Mathf.Clamp(1 + PositiveMod(variant + salt, 3), 1, Mathf.Max(1, span - 1));
            if (innerRows == 2 && span >= 3)
                innerRows = 3;
            int innerEnd = yStart + yStep * (innerRows - 1);
            var innerKind = PositiveMod(variant + salt, 2) == 0 ? V10MotifKind.SwerveLong : V10MotifKind.ShortBridge;
            AddV10LaneIfValid(BuildV10SegmentedRowSnakeLane(width, height, canSpawn, occupied, xStart, xEnd, yStart, innerEnd, variant + salt), innerKind, region, width, height, occupied, lanes);

            int y = innerEnd + yStep;
            int band = 0;
            while (yStep > 0 ? y <= yEnd : y >= yEnd)
            {
                int rows = 1 + PositiveMod(variant + salt + band * 5, 3);
                int remaining = Mathf.Abs(yEnd - y) + 1;
                if (rows == 2 && remaining >= 3)
                    rows = 3;
                if (rows > remaining)
                    rows = remaining;
                if (rows == 2)
                    rows = 1;
                int end = y + yStep * (rows - 1);
                end = yStep > 0 ? Mathf.Min(end, yEnd) : Mathf.Max(end, yEnd);
                if (Mathf.Abs(yEnd - end) == 1)
                    end = yEnd;

                var kind = ((band + PositiveMod(variant + salt, 5)) % 5) switch
                {
                    0 => V10MotifKind.UShape,
                    1 => V10MotifKind.Hook,
                    2 => V10MotifKind.ZigZagLite,
                    3 => V10MotifKind.Turnback,
                    _ => V10MotifKind.SwerveLong
                };
                AddV10LaneIfValid(BuildV10SegmentedRowSnakeLane(width, height, canSpawn, occupied, xStart, xEnd, y, end, variant + salt + band * 13), kind, region, width, height, occupied, lanes);
                y = end + yStep;
                band++;
            }
        }

        static List<int> BuildV10SegmentedRowSnakeLane(int width, int height, bool[] canSpawn, bool[] occupied, int xStart, int xEnd, int yStart, int yEnd, int salt)
        {
            var path = new List<int>();
            if (xEnd < xStart || (yStart <= yEnd ? yStart > yEnd : yStart < yEnd))
                return path;

            int rowCount = Mathf.Abs(yEnd - yStart) + 1;
            if ((rowCount & 1) == 0)
                return BuildRowSnakeLane(width, height, canSpawn, occupied, xStart, xEnd, yStart, yEnd, true);

            int x = xStart;
            int segment = 0;
            int fromY = yStart;
            int toY = yEnd;
            while (x <= xEnd)
            {
                int widthSegment = 2 + PositiveMod(salt + segment * 5, 3);
                int end = Mathf.Min(xEnd, x + widthSegment - 1);
                if (xEnd - end > 0 && xEnd - end < 2)
                    end = xEnd;

                var part = BuildRowSnakeLane(width, height, canSpawn, occupied, x, end, fromY, toY, true);
                if (part.Count == 0)
                    return new List<int>();

                path.AddRange(part);
                int swap = fromY;
                fromY = toY;
                toY = swap;
                x = end + 1;
                segment++;
            }

            return IsContiguousPath(path, width) ? path : new List<int>();
        }

        static void AddV10SideRunBands(int width, int height, bool[] canSpawn, bool[] occupied, List<V10Lane> lanes, int xStart, int xEnd, int yStart, int yEnd, int variant, int salt, string region)
        {
            if (xEnd < xStart || yEnd < yStart)
                return;

            int y = yStart;
            int band = 0;
            while (y <= yEnd)
            {
                int rows = 1 + PositiveMod(variant + salt + band * 3, 3);
                int end = Mathf.Min(yEnd, y + rows - 1);
                if (yEnd - end == 1)
                    end = yEnd;

                var kind = ((band + PositiveMod(variant + salt, 4)) % 4) switch
                {
                    0 => V10MotifKind.StraightWithBend,
                    1 => V10MotifKind.ShortBridge,
                    2 => V10MotifKind.Turnback,
                    _ => V10MotifKind.Hook
                };
                AddV10LaneIfValid(BuildRowSnakeLane(width, height, canSpawn, occupied, xStart, xEnd, y, end, ((band & 1) == 0) == (((variant + salt) & 1) == 0)), kind, region, width, height, occupied, lanes);
                y = end + 1;
                band++;
            }
        }

        static void AddV10LeftoverRowRunLanes(int width, int height, bool[] canSpawn, bool[] occupied, List<V10Lane> lanes)
        {
            for (int y = 0; y < height; y++)
            {
                int x = 0;
                while (x < width)
                {
                    while (x < width && (!canSpawn[x + y * width] || occupied[x + y * width]))
                        x++;

                    var lane = new List<int>();
                    while (x < width && canSpawn[x + y * width] && !occupied[x + y * width])
                    {
                        lane.Add(x + y * width);
                        x++;
                    }

                    AddV10LaneIfValid(lane, V10MotifKind.StraightWithBend, "Residual", width, height, occupied, lanes);
                }
            }
        }

        static void AddV10LaneIfValid(List<int> lane, V10MotifKind kind, string region, int width, int height, bool[] occupied, List<V10Lane> lanes)
        {
            if (lane == null || lane.Count < 2 || !IsContiguousPath(lane, width))
                return;

            var path = EnsureOpenHead(lane, width, height);
            if (!LaneHeadExits(path, width, height))
                return;

            lanes.Add(new V10Lane { Cells = path, Kind = kind, Region = region });
            foreach (int i in path)
                occupied[i] = true;
        }

        static V10MotifStats CalculateV10MotifStats(List<V10Lane> lanes, int width, int height, bool[] canSpawn)
        {
            var result = new V10MotifStats { Plan = "" };
            if (lanes == null || lanes.Count == 0)
                return result;

            var histogram = new Dictionary<V10MotifKind, int>();
            int straight = 0;
            int repeated = 0;
            int horizontal = 0;
            int vertical = 0;
            int holeTouch = 0;
            int totalCells = 0;
            V10MotifKind? previous = null;

            foreach (var lane in lanes)
            {
                histogram.TryGetValue(lane.Kind, out int count);
                histogram[lane.Kind] = count + 1;

                if (previous.HasValue && previous.Value == lane.Kind)
                    repeated++;
                previous = lane.Kind;

                if (CountLaneTurns(lane.Cells, width) == 0)
                    straight++;

                CountLaneSteps(lane.Cells, width, out int h, out int v);
                horizontal += h;
                vertical += v;

                foreach (int idx in lane.Cells)
                {
                    totalCells++;
                    if (IsAdjacentToBlock(idx, width, height, canSpawn))
                        holeTouch++;
                }
            }

            result.MotifKindCount = histogram.Count;
            result.StraightChainRatio = straight / (float)Mathf.Max(1, lanes.Count);
            result.RegionDirectionEntropy = CalculateBinaryEntropy(horizontal, vertical);
            result.RepeatedMotifPenalty = repeated / (float)Mathf.Max(1, lanes.Count - 1);
            result.HoleEdgeCoverage = holeTouch / (float)Mathf.Max(1, totalCells);
            result.Plan = string.Join("|", histogram.OrderBy(kv => kv.Key).Select(kv => $"{kv.Key}:{kv.Value}"));
            return result;
        }

        static void CountLaneSteps(List<int> lane, int width, out int horizontal, out int vertical)
        {
            horizontal = 0;
            vertical = 0;
            if (lane == null)
                return;

            for (int i = 1; i < lane.Count; i++)
            {
                var a = IndexToPos(lane[i - 1], width);
                var b = IndexToPos(lane[i], width);
                if (a.x != b.x)
                    horizontal++;
                else if (a.y != b.y)
                    vertical++;
            }
        }

        static float CalculateBinaryEntropy(int a, int b)
        {
            int total = a + b;
            if (total <= 0 || a == 0 || b == 0)
                return 0f;

            float pa = a / (float)total;
            float pb = b / (float)total;
            return -(pa * Mathf.Log(pa, 2f) + pb * Mathf.Log(pb, 2f));
        }

        static List<LaneCageCandidate> SelectDiverseMotifRouterV09(List<LaneCageCandidate> sorted, int topCount)
        {
            var picked = new List<LaneCageCandidate>();
            var layouts = new HashSet<int>();
            foreach (var candidate in sorted)
            {
                int variant = Mathf.Max(0, candidate.Seed - 890000);
                int layout = variant & 3;
                if (!layouts.Add(layout))
                    continue;

                picked.Add(candidate);
                if (picked.Count >= topCount || layouts.Count >= 4)
                    break;
            }

            var signatures = new HashSet<int>(picked.Select(p => PositiveMod(Mathf.Max(0, p.Seed - 890000), 36)));
            foreach (var candidate in sorted)
            {
                int variant = Mathf.Max(0, candidate.Seed - 890000);
                int signature = PositiveMod(variant, 36);
                if (!signatures.Add(signature))
                    continue;

                if (picked.Any(p => p.Seed == candidate.Seed))
                    continue;

                picked.Add(candidate);
                if (picked.Count >= topCount)
                    return picked;
            }

            foreach (var candidate in sorted)
            {
                if (picked.Any(p => p.Seed == candidate.Seed))
                    continue;

                picked.Add(candidate);
                if (picked.Count >= topCount)
                    break;
            }

            return picked;
        }

        static bool TryBuildMotifRouterV09Authored(int width, int height, bool[] canSpawn, int variant, out AuthoredLevelData authored, out int moduleCount, out int turnCount, out float frameLate)
        {
            authored = new AuthoredLevelData
            {
                width = width,
                height = height,
                arrows = new List<AuthoredArrowData>()
            };
            moduleCount = 0;
            turnCount = 0;
            frameLate = 0f;

            var authoredBlockIndices = new List<int>();
            for (int i = 0; i < canSpawn.Length; i++)
                if (!canSpawn[i])
                    authoredBlockIndices.Add(i);
            SetAuthoredBlockIndices(authored, authoredBlockIndices);

            if (!TryFindBlockBounds(width, height, canSpawn, out int minX, out int maxX, out int minY, out int maxY))
                return false;

            var occupied = new bool[width * height];
            var modules = new List<List<int>>();
            AddRimHookMotifsV09(width, height, canSpawn, occupied, modules, minX, maxX, minY, maxY, variant);
            AddPatchMotifLanesV09(width, height, canSpawn, occupied, modules, minX, maxX, minY, maxY, variant);
            AddLeftoverRowRunLanes(width, height, canSpawn, occupied, modules);

            foreach (var module in modules)
            {
                if (module.Count < 2)
                    continue;

                var path = EnsureOpenHead(module, width, height);
                if (path.Count < 2)
                    continue;

                moduleCount++;
                if (((variant + moduleCount * 7) % 9) < 7 && TryReverseModuleForDependencyV09(path, width, height, canSpawn, out var dependencyPath))
                    path = dependencyPath;

                int turns = CountLaneTurns(path, width);
                turnCount += turns;
                var chains = turns == 0
                    ? SplitStraightLaneSeedLikeChains(path, width, minLen: 3, maxLen: 6, moduleCount - 1)
                    : SplitLaneTailDependencyChains(path, width, minLen: 3, maxLen: 5 + ((variant / 5) % 2), moduleCount - 1);

                foreach (var chain in chains)
                    AddAuthoredChain(authored, chain.InnerToOuter, authored.arrows.Count);

                if (chains.Count > 0 && chains[0].InnerToOuter.Any(i => IsAdjacentToBlock(i, width, height, canSpawn)))
                    frameLate += 1f;
            }

            frameLate = moduleCount > 0 ? frameLate / moduleCount : 0f;
            return authored.arrows.Count > 0;
        }

        static List<ChainSpec> SplitModuleDependentChainsV09(List<int> path, int width, int minLen, int maxLen, int laneIndex)
        {
            var chains = new List<ChainSpec>();
            if (path == null || path.Count < 2)
                return chains;

            int start = 0;
            while (path.Count - start > maxLen)
            {
                int cut = -1;
                int maxCut = Mathf.Min(path.Count - minLen - 1, start + maxLen - 1);
                int minCut = start + minLen - 1;
                for (int i = maxCut; i >= minCut; i--)
                {
                    if (IsDependencyCutV09(path, i, width))
                    {
                        cut = i;
                        break;
                    }
                }

                if (cut < 0)
                    break;

                chains.Add(new ChainSpec { InnerToOuter = path.GetRange(start, cut - start + 1), LaneIndex = laneIndex });
                start = cut + 1;
            }

            if (path.Count - start >= 2)
                chains.Add(new ChainSpec { InnerToOuter = path.GetRange(start, path.Count - start), LaneIndex = laneIndex });

            if (chains.Count == 0)
                chains.Add(new ChainSpec { InnerToOuter = path, LaneIndex = laneIndex });

            return chains;
        }

        static bool IsDependencyCutV09(List<int> path, int cut, int width)
        {
            if (cut <= 0 || cut + 1 >= path.Count)
                return false;

            var prev = IndexToPos(path[cut - 1], width);
            var current = IndexToPos(path[cut], width);
            var next = IndexToPos(path[cut + 1], width);
            return current - prev == next - current;
        }

        static bool TryReverseModuleForDependencyV09(List<int> path, int width, int height, bool[] canSpawn, out List<int> reversed)
        {
            reversed = null;
            if (path == null || path.Count < 2)
                return false;

            var candidate = new List<int>(path);
            candidate.Reverse();
            if (!ChainHeadRayCanExitThroughAllowed(candidate, width, height, canSpawn))
                return false;

            reversed = candidate;
            return true;
        }

        static void AddRimHookMotifsV09(int width, int height, bool[] canSpawn, bool[] occupied, List<List<int>> lanes, int minX, int maxX, int minY, int maxY, int variant)
        {
            if (minX <= 0 || maxX >= width - 1 || minY <= 0 || maxY >= height - 1)
                return;

            if ((variant & 1) == 0)
            {
                AddOpenHookLaneV09(BuildRimHookLaneV09(width, height, canSpawn, occupied, minX - 1, minY, maxY + 1, 0, maxY + 1), width, height, lanes, occupied);
                AddOpenHookLaneV09(BuildRimHookLaneV09(width, height, canSpawn, occupied, maxX + 1, maxY, minY - 1, width - 1, minY - 1), width, height, lanes, occupied);
            }

            if ((variant & 2) == 0)
            {
                AddOpenHookLaneV09(BuildRimHookLaneV09(width, height, canSpawn, occupied, maxX + 1, minY, maxY + 1, width - 1, maxY + 1), width, height, lanes, occupied);
                AddOpenHookLaneV09(BuildRimHookLaneV09(width, height, canSpawn, occupied, minX - 1, maxY, minY - 1, 0, minY - 1), width, height, lanes, occupied);
            }
        }

        static List<int> BuildRimHookLaneV09(int width, int height, bool[] canSpawn, bool[] occupied, int verticalX, int verticalYStart, int verticalYEnd, int horizontalXEnd, int horizontalY)
        {
            var path = new List<int>();
            int yStep = verticalYStart <= verticalYEnd ? 1 : -1;
            for (int y = verticalYStart; yStep > 0 ? y <= verticalYEnd : y >= verticalYEnd; y += yStep)
            {
                if (!TryAppendFreeCellV09(path, width, height, canSpawn, occupied, verticalX, y))
                    return new List<int>();
            }

            int xStep = verticalX <= horizontalXEnd ? 1 : -1;
            for (int x = verticalX + xStep; xStep > 0 ? x <= horizontalXEnd : x >= horizontalXEnd; x += xStep)
            {
                if (!TryAppendFreeCellV09(path, width, height, canSpawn, occupied, x, horizontalY))
                    return new List<int>();
            }

            return path;
        }

        static bool TryAppendFreeCellV09(List<int> path, int width, int height, bool[] canSpawn, bool[] occupied, int x, int y)
        {
            if (!InBounds(width, height, x, y))
                return false;

            int index = x + y * width;
            if (!canSpawn[index] || occupied[index] || path.Contains(index))
                return false;

            path.Add(index);
            return true;
        }

        static void AddOpenHookLaneV09(List<int> lane, int width, int height, List<List<int>> lanes, bool[] occupied)
        {
            if (lane == null || lane.Count < 3 || !IsContiguousPath(lane, width))
                return;

            var path = EnsureOpenHead(lane, width, height);
            if (!LaneHeadExits(path, width, height))
                return;

            lanes.Add(path);
            foreach (int i in path)
                occupied[i] = true;
        }

        static bool ChainHeadTargetsAllowedCell(List<int> path, int width, int height, bool[] canSpawn)
        {
            if (path == null || path.Count < 2)
                return false;

            var head = IndexToPos(path[^1], width);
            var prev = IndexToPos(path[^2], width);
            var next = head + (head - prev);
            return InBounds(width, height, next.x, next.y) && canSpawn[next.x + next.y * width];
        }

        static bool ChainHeadRayCanExitThroughAllowed(List<int> path, int width, int height, bool[] canSpawn)
        {
            if (path == null || path.Count < 2)
                return false;

            var head = IndexToPos(path[^1], width);
            var prev = IndexToPos(path[^2], width);
            var dir = head - prev;
            var cursor = head + dir;
            if (!InBounds(width, height, cursor.x, cursor.y))
                return false;

            while (InBounds(width, height, cursor.x, cursor.y))
            {
                if (!canSpawn[cursor.x + cursor.y * width])
                    return false;

                cursor += dir;
            }

            return true;
        }

        static void AddPatchMotifLanesV09(int width, int height, bool[] canSpawn, bool[] occupied, List<List<int>> lanes, int minX, int maxX, int minY, int maxY, int variant)
        {
            AddMixedVerticalPatchMotifsV09(width, height, canSpawn, occupied, lanes, 0, width - 1, maxY + 1, height - 1, variant, 0);
            AddMixedVerticalPatchMotifsV09(width, height, canSpawn, occupied, lanes, 0, width - 1, minY - 1, 0, variant, 11);
            AddHorizontalPatchMotifsV09(width, height, canSpawn, occupied, lanes, 0, minX - 1, minY, maxY, variant, 23);
            AddHorizontalPatchMotifsV09(width, height, canSpawn, occupied, lanes, maxX + 1, width - 1, minY, maxY, variant, 37);
        }

        static void AddMixedVerticalPatchMotifsV09(int width, int height, bool[] canSpawn, bool[] occupied, List<List<int>> lanes, int xStart, int xEnd, int yStart, int yEnd, int variant, int salt)
        {
            int span = Mathf.Abs(yEnd - yStart) + 1;
            if (xStart != 0 || xEnd != width - 1 || span < 5)
            {
                AddVerticalPatchMotifsV09(width, height, canSpawn, occupied, lanes, xStart, xEnd, yStart, yEnd, variant, salt);
                return;
            }

            int bandHeight = Mathf.Min(span - 2, 2 + PositiveMod(variant + salt, 2));
            bool forwardFirst = ((variant + salt) & 4) == 0;
            if (yStart <= yEnd)
            {
                int bandEnd = Mathf.Min(yEnd, yStart + bandHeight - 1);
                AddRowBandSnakeLanes(width, height, canSpawn, occupied, lanes, xStart, xEnd, yStart, bandEnd, bandHeight, forwardFirst);
                if (bandEnd + 1 <= yEnd)
                    AddVerticalPatchMotifsV09(width, height, canSpawn, occupied, lanes, xStart, xEnd, bandEnd + 1, yEnd, variant, salt + 5);
            }
            else
            {
                int bandEnd = Mathf.Max(yEnd, yStart - bandHeight + 1);
                AddRowBandSnakeLanes(width, height, canSpawn, occupied, lanes, xStart, xEnd, yStart, bandEnd, bandHeight, forwardFirst);
                if (bandEnd - 1 >= yEnd)
                    AddVerticalPatchMotifsV09(width, height, canSpawn, occupied, lanes, xStart, xEnd, bandEnd - 1, yEnd, variant, salt + 5);
            }
        }

        static void AddVerticalPatchMotifsV09(int width, int height, bool[] canSpawn, bool[] occupied, List<List<int>> lanes, int xStart, int xEnd, int yStart, int yEnd, int variant, int salt)
        {
            if (xEnd < xStart || (yStart <= yEnd ? yStart > yEnd : yStart < yEnd))
                return;

            int x = xStart;
            while (x <= xEnd)
            {
                int segment = 2 + PositiveMod(variant + salt + x * 3, 4);
                int end = Mathf.Min(xEnd, x + segment - 1);
                if (xEnd - end == 1)
                    end = xEnd;

                int columns = end - x + 1;
                bool lastColumnUp = true;
                bool upFirst = ((columns - 1) & 1) == 0 ? lastColumnUp : !lastColumnUp;
                var lane = BuildColumnSnakeLane(width, height, canSpawn, occupied, x, end, yStart, yEnd, upFirst);
                AddOpenModuleLaneIfValidV09(lane, width, height, lanes, occupied);
                x = end + 1;
            }
        }

        static void AddHorizontalPatchMotifsV09(int width, int height, bool[] canSpawn, bool[] occupied, List<List<int>> lanes, int xStart, int xEnd, int yStart, int yEnd, int variant, int salt)
        {
            if (xEnd < xStart || yEnd < yStart)
                return;

            int y = yStart;
            while (y <= yEnd)
            {
                int segment = 2 + PositiveMod(variant + salt + y * 5, 4);
                int end = Mathf.Min(yEnd, y + segment - 1);
                if (yEnd - end == 1)
                    end = yEnd;

                int rows = end - y + 1;
                bool lastRowForward = xEnd == width - 1;
                bool forwardFirst = ((rows - 1) & 1) == 0 ? lastRowForward : !lastRowForward;
                var lane = BuildRowSnakeLane(width, height, canSpawn, occupied, xStart, xEnd, y, end, forwardFirst);
                AddOpenModuleLaneIfValidV09(lane, width, height, lanes, occupied);
                y = end + 1;
            }
        }

        static void AddOpenModuleLaneIfValidV09(List<int> lane, int width, int height, List<List<int>> lanes, bool[] occupied)
        {
            if (lane == null || lane.Count < 2 || !IsContiguousPath(lane, width))
                return;

            var path = EnsureOpenHead(lane, width, height);
            if (!LaneHeadExits(path, width, height))
                return;

            lanes.Add(path);
            foreach (int i in path)
                occupied[i] = true;
        }

        static int PositiveMod(int value, int mod)
        {
            int result = value % mod;
            return result < 0 ? result + mod : result;
        }

        static List<LaneCageCandidate> BuildSeedLikeMotifV08Candidates(int width, int height, bool[] canSpawn, int topCount, out string diag)
        {
            var result = new List<LaneCageCandidate>();
            var ruleset = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int allowedCount = CountAllowedCells(canSpawn);
            int authoredBuilt = 0, boardBuilt = 0, shapeOk = 0, greedyOk = 0, totalHeadToHead = 0, totalDirectBlock = 0, totalDependencyContact = 0;

            for (int variant = 0; variant < 240; variant++)
            {
                if (!TryBuildSeedLikeMotifV08Authored(width, height, canSpawn, variant, out var authored, out int laneCount, out int turnCount, out float frameLate))
                    continue;

                authoredBuilt++;
                if (!AuthoredLevelBuilder.TryBuildBoard(authored, out var board, out _))
                    continue;

                boardBuilt++;
                CountShapeViolations(board, out int headToHead, out int directBlock, out int dependencyContact);
                totalHeadToHead += headToHead;
                totalDirectBlock += directBlock;
                totalDependencyContact += dependencyContact;
                if (headToHead > 0 || directBlock > 0)
                    continue;

                shapeOk++;
                if (!GreedyValidator.TryClearAllByGreedy(board, ruleset, 2200, out _))
                    continue;

                greedyOk++;
                var stats = BoardGenerationTuning.CalculateBoardGenerationStats(board, ruleset);
                if (stats.ChainCount <= 0)
                    continue;

                float ratio = stats.InitialMovableArrowChainCount / (float)stats.ChainCount;
                float avg = stats.ArrowTileCount / (float)Mathf.Max(1, stats.ChainCount);
                int empty = Mathf.Max(0, allowedCount - stats.ArrowTileCount);
                float fill = stats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
                if (ratio > 0.43f)
                    continue;

                float score = empty * 10000f
                              + Mathf.Abs(avg - 9.8f) * 220f
                              + Mathf.Abs(ratio - 0.22f) * 1800f
                              + Mathf.Max(0, 58 - stats.ChainCount) * 45f
                              + Mathf.Max(0, stats.ChainCount - 92) * 75f
                              + Mathf.Max(0, stats.MaxChainLength - 52) * 16f
                              + Mathf.Max(0, 32 - turnCount) * 65f
                              - Mathf.Min(turnCount, 160) * 2.5f
                              - Mathf.Min(dependencyContact, 120) * 0.8f
                              + (variant % 4 == 3 ? 3000f : 0f)
                              + (variant & 15) * 0.1f;

                result.Add(new LaneCageCandidate
                {
                    Board = board,
                    Authored = authored,
                    Stats = stats,
                    Seed = 880000 + variant,
                    OpeningRatio = ratio,
                    AverageChainLength = avg,
                    FillRatio = fill,
                    AllowedEmptyCount = empty,
                    Score = score,
                    LaneCount = laneCount,
                    TurnCount = turnCount,
                    FrameLateScore = frameLate,
                    HeadToHeadConflicts = headToHead,
                    DirectBlockAims = directBlock,
                    BadArrowContacts = dependencyContact
                });
            }

            result.Sort((a, b) => a.Score.CompareTo(b.Score));
            result = SelectDiverseSeedLikeMotifV08(result, topCount);
            if (result.Count > topCount)
                result.RemoveRange(topCount, result.Count - topCount);

            diag = $"diag=authoredBuilt:{authoredBuilt},boardBuilt:{boardBuilt},shapeOk:{shapeOk},greedyOk:{greedyOk},headToHead:{totalHeadToHead},directBlock:{totalDirectBlock},dependencyContact:{totalDependencyContact}";
            return result;
        }

        static List<LaneCageCandidate> SelectDiverseSeedLikeMotifV08(List<LaneCageCandidate> sorted, int topCount)
        {
            var picked = new List<LaneCageCandidate>();
            var signatures = new HashSet<int>();
            foreach (var candidate in sorted)
            {
                int variant = Mathf.Max(0, candidate.Seed - 880000);
                int signature = (variant % 4) | (((variant / 3) % 3) << 3) | (((variant / 9) % 3) << 5);
                if (!signatures.Add(signature))
                    continue;

                picked.Add(candidate);
                if (picked.Count >= topCount)
                    return picked;
            }

            foreach (var candidate in sorted)
            {
                if (picked.Any(p => p.Seed == candidate.Seed))
                    continue;

                picked.Add(candidate);
                if (picked.Count >= topCount)
                    break;
            }

            return picked;
        }

        static bool TryBuildSeedLikeMotifV08Authored(int width, int height, bool[] canSpawn, int variant, out AuthoredLevelData authored, out int laneCount, out int turnCount, out float frameLate)
        {
            authored = new AuthoredLevelData
            {
                width = width,
                height = height,
                arrows = new List<AuthoredArrowData>()
            };
            laneCount = 0;
            turnCount = 0;
            frameLate = 0f;

            var authoredBlockIndices = new List<int>();
            for (int i = 0; i < canSpawn.Length; i++)
                if (!canSpawn[i])
                    authoredBlockIndices.Add(i);
            SetAuthoredBlockIndices(authored, authoredBlockIndices);

            if (!TryFindBlockBounds(width, height, canSpawn, out int minX, out int maxX, out int minY, out int maxY))
                return false;

            var occupied = new bool[width * height];
            var lanes = new List<List<int>>();
            int topBand = 2 + (variant % 3);
            int bottomBand = 2 + ((variant / 3) % 3);
            int leftBand = 2 + ((variant / 9) % 3);
            int rightBand = 2 + ((variant / 27) % 3);

            int layout = variant % 4;
            if (layout == 0)
            {
                AddColumnBandSnakeLanes(width, height, canSpawn, occupied, lanes, 0, width - 1, maxY + 1, height - 1, topBand, (variant & 8) == 0);
                AddColumnBandSnakeLanes(width, height, canSpawn, occupied, lanes, 0, width - 1, minY - 1, 0, bottomBand, (variant & 16) == 0);
                AddRowBandSnakeLanes(width, height, canSpawn, occupied, lanes, 0, minX - 1, minY, maxY, leftBand, (variant & 32) == 0);
                AddRowBandSnakeLanes(width, height, canSpawn, occupied, lanes, maxX + 1, width - 1, minY, maxY, rightBand, (variant & 64) == 0);
            }
            else if (layout == 1)
            {
                AddColumnBandSnakeLanes(width, height, canSpawn, occupied, lanes, 0, width - 1, maxY + 1, height - 1, topBand, (variant & 8) == 0);
                AddRowBandSnakeLanes(width, height, canSpawn, occupied, lanes, 0, width - 1, minY - 1, 0, bottomBand, (variant & 16) == 0);
                AddRowBandSnakeLanes(width, height, canSpawn, occupied, lanes, 0, minX - 1, minY, maxY, leftBand, (variant & 32) == 0);
                AddColumnBandSnakeLanes(width, height, canSpawn, occupied, lanes, maxX + 1, width - 1, minY, maxY, rightBand, (variant & 64) == 0);
            }
            else if (layout == 2)
            {
                AddRowBandSnakeLanes(width, height, canSpawn, occupied, lanes, 0, width - 1, maxY + 1, height - 1, topBand, (variant & 8) == 0);
                AddColumnBandSnakeLanes(width, height, canSpawn, occupied, lanes, 0, width - 1, minY - 1, 0, bottomBand, (variant & 16) == 0);
                AddColumnBandSnakeLanes(width, height, canSpawn, occupied, lanes, 0, minX - 1, minY, maxY, leftBand, (variant & 32) == 0);
                AddRowBandSnakeLanes(width, height, canSpawn, occupied, lanes, maxX + 1, width - 1, minY, maxY, rightBand, (variant & 64) == 0);
            }
            else
            {
                AddRowBandSnakeLanes(width, height, canSpawn, occupied, lanes, 0, width - 1, maxY + 1, height - 1, topBand, (variant & 8) == 0);
                AddRowBandSnakeLanes(width, height, canSpawn, occupied, lanes, 0, width - 1, minY - 1, 0, bottomBand, (variant & 16) == 0);
                AddColumnBandSnakeLanes(width, height, canSpawn, occupied, lanes, 0, minX - 1, minY, maxY, leftBand, (variant & 32) == 0);
                AddColumnBandSnakeLanes(width, height, canSpawn, occupied, lanes, maxX + 1, width - 1, minY, maxY, rightBand, (variant & 64) == 0);
            }

            AddLeftoverRowRunLanes(width, height, canSpawn, occupied, lanes);

            foreach (var lane in lanes)
            {
                if (lane.Count < 2)
                    continue;

                var path = EnsureOpenHead(lane, width, height);
                if (path.Count < 2)
                    continue;

                laneCount++;
                int turns = CountLaneTurns(path, width);
                turnCount += turns;
                var chains = turns == 0
                    ? SplitStraightLaneSeedLikeChains(path, width, minLen: 5 + (variant % 2), maxLen: 11 + ((variant / 3) % 4), laneCount - 1)
                    : SplitLaneTailDependencyChains(path, width, minLen: 5, maxLen: 12, laneCount - 1);

                foreach (var chain in chains)
                    AddAuthoredChain(authored, chain.InnerToOuter, authored.arrows.Count);

                if (chains.Count > 0 && chains[0].InnerToOuter.Any(i => IsAdjacentToBlock(i, width, height, canSpawn)))
                    frameLate += 1f;
            }

            frameLate = laneCount > 0 ? frameLate / laneCount : 0f;
            return authored.arrows.Count > 0;
        }

        static List<LaneCageCandidate> SelectDiverseDependencyMotifV07(List<LaneCageCandidate> sorted, int topCount)
        {
            var picked = new List<LaneCageCandidate>();
            var signatures = new HashSet<int>();

            foreach (var candidate in sorted)
            {
                int variant = Mathf.Max(0, candidate.Seed - 870000);
                int signature = variant & 7;
                if (!signatures.Add(signature))
                    continue;

                picked.Add(candidate);
                if (picked.Count >= topCount)
                    return picked;
            }

            foreach (var candidate in sorted)
            {
                if (picked.Any(p => p.Seed == candidate.Seed))
                    continue;

                picked.Add(candidate);
                if (picked.Count >= topCount)
                    break;
            }

            return picked;
        }

        static bool TryBuildDependencyMotifV07Authored(int width, int height, bool[] canSpawn, int variant, out AuthoredLevelData authored, out int laneCount, out int turnCount, out float frameLate)
        {
            authored = new AuthoredLevelData
            {
                width = width,
                height = height,
                arrows = new List<AuthoredArrowData>()
            };
            laneCount = 0;
            turnCount = 0;
            frameLate = 0f;

            var authoredBlockIndices = new List<int>();
            for (int i = 0; i < canSpawn.Length; i++)
                if (!canSpawn[i])
                    authoredBlockIndices.Add(i);
            SetAuthoredBlockIndices(authored, authoredBlockIndices);

            if (!TryFindBlockBounds(width, height, canSpawn, out int minX, out int maxX, out int minY, out int maxY))
                return false;

            var occupied = new bool[width * height];
            var lanes = new List<List<int>>();
            int topBand = 2 + (variant % 2);
            int bottomBand = 2 + ((variant / 2) % 2);
            int sideBand = 2 + ((variant / 4) % 2);

            AddRowBandSnakeLanes(width, height, canSpawn, occupied, lanes, 0, width - 1, maxY + 1, height - 1, topBand, (variant & 8) == 0);
            AddRowBandSnakeLanes(width, height, canSpawn, occupied, lanes, 0, width - 1, minY - 1, 0, bottomBand, (variant & 16) == 0);
            AddColumnBandSnakeLanes(width, height, canSpawn, occupied, lanes, 0, minX - 1, minY, maxY, sideBand, (variant & 32) == 0);
            AddColumnBandSnakeLanes(width, height, canSpawn, occupied, lanes, maxX + 1, width - 1, minY, maxY, sideBand, (variant & 64) == 0);
            AddLeftoverRowRunLanes(width, height, canSpawn, occupied, lanes);

            foreach (var lane in lanes)
            {
                if (lane.Count < 2)
                    continue;

                var path = EnsureOpenHead(lane, width, height);
                if (path.Count < 2)
                    continue;

                laneCount++;
                turnCount += CountLaneTurns(path, width);
                var chains = SplitLaneTailDependencyChains(path, width, minLen: 5, maxLen: 10, laneCount - 1);
                foreach (var chain in chains)
                    AddAuthoredChain(authored, chain.InnerToOuter, authored.arrows.Count);

                if (chains.Count > 0 && chains[0].InnerToOuter.Any(i => IsAdjacentToBlock(i, width, height, canSpawn)))
                    frameLate += 1f;
            }

            frameLate = laneCount > 0 ? frameLate / laneCount : 0f;
            return authored.arrows.Count > 0;
        }

        static bool TryBuildLaneCageAuthored(int width, int height, bool[] canSpawn, int variant, out AuthoredLevelData authored, out int laneCount, out int turnCount, out float frameLate)
        {
            authored = new AuthoredLevelData
            {
                width = width,
                height = height,
                arrows = new List<AuthoredArrowData>()
            };
            laneCount = 0;
            turnCount = 0;
            frameLate = 0f;

            var authoredBlockIndices = new List<int>();
            for (int i = 0; i < canSpawn.Length; i++)
                if (!canSpawn[i])
                    authoredBlockIndices.Add(i);
            SetAuthoredBlockIndices(authored, authoredBlockIndices);

            if (!TryFindBlockBounds(width, height, canSpawn, out int minX, out int maxX, out int minY, out int maxY))
                return false;

            var occupied = new bool[width * height];
            var lanes = new List<List<int>>();
            AddLaneIfValid(BuildRowSnakeLane(width, height, canSpawn, occupied, 0, width - 1, maxY + 1, height - 1, (variant & 1) == 0), lanes, occupied, width, height);
            AddLaneIfValid(BuildRowSnakeLane(width, height, canSpawn, occupied, 0, width - 1, minY - 1, 0, (variant & 2) == 0), lanes, occupied, width, height);
            AddLaneIfValid(BuildRowSnakeLane(width, height, canSpawn, occupied, minX - 1, 0, minY, maxY, (variant & 4) == 0), lanes, occupied, width, height);
            AddLaneIfValid(BuildRowSnakeLane(width, height, canSpawn, occupied, maxX + 1, width - 1, minY, maxY, (variant & 8) == 0), lanes, occupied, width, height);
            AddLeftoverRowRunLanes(width, height, canSpawn, occupied, lanes);

            bool wholeLaneMode = variant >= 24;
            foreach (var lane in lanes)
            {
                if (lane.Count < 2)
                    continue;

                var path = EnsureOpenHead(lane, width, height);
                if (path.Count < 2)
                    continue;

                laneCount++;
                turnCount += CountLaneTurns(path, width);
                var chains = wholeLaneMode
                    ? new List<ChainSpec> { new ChainSpec { InnerToOuter = path, LaneIndex = laneCount - 1 } }
                    : SplitLaneTailDependencyChains(path, width, minLen: 4, maxLen: 10, laneCount - 1);
                foreach (var chain in chains)
                    AddAuthoredChain(authored, chain.InnerToOuter, authored.arrows.Count);

                if (chains.Count > 0 && chains[0].InnerToOuter.Any(i => IsAdjacentToBlock(i, width, height, canSpawn)))
                    frameLate += 1f;
            }

            frameLate = laneCount > 0 ? frameLate / laneCount : 0f;
            return authored.arrows.Count > 0;
        }

        static bool TryFindBlockBounds(int width, int height, bool[] canSpawn, out int minX, out int maxX, out int minY, out int maxY)
        {
            minX = width;
            maxX = -1;
            minY = height;
            maxY = -1;
            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                int i = x + y * width;
                if (canSpawn[i])
                    continue;

                minX = Mathf.Min(minX, x);
                maxX = Mathf.Max(maxX, x);
                minY = Mathf.Min(minY, y);
                maxY = Mathf.Max(maxY, y);
            }

            return maxX >= minX && maxY >= minY;
        }

        static List<int> BuildRowSnakeLane(int width, int height, bool[] canSpawn, bool[] occupied, int xStart, int xEnd, int yStart, int yEnd, bool forwardFirst)
        {
            var path = new List<int>();
            int yStep = yStart <= yEnd ? 1 : -1;
            int row = 0;
            for (int y = yStart; yStep > 0 ? y <= yEnd : y >= yEnd; y += yStep, row++)
            {
                bool forward = ((row & 1) == 0) == forwardFirst;
                int from = forward ? xStart : xEnd;
                int to = forward ? xEnd : xStart;
                int xStep = from <= to ? 1 : -1;
                for (int x = from; xStep > 0 ? x <= to : x >= to; x += xStep)
                {
                    if (x < 0 || x >= width || y < 0 || y >= height)
                        continue;

                    int idx = x + y * width;
                    if (canSpawn[idx] && !occupied[idx])
                        path.Add(idx);
                }
            }

            return IsContiguousPath(path, width) ? path : new List<int>();
        }

        static void AddRowRunLanes(int width, int height, bool[] canSpawn, bool[] occupied, List<List<int>> lanes, int xStart, int xEnd, int yStart, int yEnd, bool forwardFirst)
        {
            int yStep = yStart <= yEnd ? 1 : -1;
            int row = 0;
            for (int y = yStart; yStep > 0 ? y <= yEnd : y >= yEnd; y += yStep, row++)
            {
                bool forward = ((row & 1) == 0) == forwardFirst;
                int from = forward ? xStart : xEnd;
                int to = forward ? xEnd : xStart;
                int xStep = from <= to ? 1 : -1;
                var lane = new List<int>();
                for (int x = from; xStep > 0 ? x <= to : x >= to; x += xStep)
                {
                    if (x < 0 || x >= width || y < 0 || y >= height)
                        continue;

                    int idx = x + y * width;
                    if (canSpawn[idx] && !occupied[idx])
                        lane.Add(idx);
                    else
                    {
                        AddLaneIfValid(lane, lanes, occupied, width, height);
                        lane = new List<int>();
                    }
                }

                AddLaneIfValid(lane, lanes, occupied, width, height);
            }
        }

        static void AddColumnRunLanes(int width, int height, bool[] canSpawn, bool[] occupied, List<List<int>> lanes, int xStart, int xEnd, int yStart, int yEnd, bool upFirst)
        {
            int xStep = xStart <= xEnd ? 1 : -1;
            int column = 0;
            for (int x = xStart; xStep > 0 ? x <= xEnd : x >= xEnd; x += xStep, column++)
            {
                bool up = ((column & 1) == 0) == upFirst;
                int from = up ? yStart : yEnd;
                int to = up ? yEnd : yStart;
                int yStep = from <= to ? 1 : -1;
                var lane = new List<int>();
                for (int y = from; yStep > 0 ? y <= to : y >= to; y += yStep)
                {
                    if (x < 0 || x >= width || y < 0 || y >= height)
                        continue;

                    int idx = x + y * width;
                    if (canSpawn[idx] && !occupied[idx])
                        lane.Add(idx);
                    else
                    {
                        AddLaneIfValid(lane, lanes, occupied, width, height);
                        lane = new List<int>();
                    }
                }

                AddLaneIfValid(lane, lanes, occupied, width, height);
            }
        }

        static void AddRowBandSnakeLanes(int width, int height, bool[] canSpawn, bool[] occupied, List<List<int>> lanes, int xStart, int xEnd, int yStart, int yEnd, int bandHeight, bool forwardFirst)
        {
            int yStep = yStart <= yEnd ? 1 : -1;
            int y = yStart;
            int band = 0;
            while (yStep > 0 ? y <= yEnd : y >= yEnd)
            {
                int next = y + yStep * (Mathf.Max(1, bandHeight) - 1);
                int bandEnd = yStep > 0 ? Mathf.Min(next, yEnd) : Mathf.Max(next, yEnd);
                var lane = BuildRowSnakeLane(width, height, canSpawn, occupied, xStart, xEnd, y, bandEnd, ((band & 1) == 0) == forwardFirst);
                AddLaneIfValid(lane, lanes, occupied, width, height);
                y = bandEnd + yStep;
                band++;
            }
        }

        static void AddColumnBandSnakeLanes(int width, int height, bool[] canSpawn, bool[] occupied, List<List<int>> lanes, int xStart, int xEnd, int yStart, int yEnd, int bandWidth, bool upFirst)
        {
            int xStep = xStart <= xEnd ? 1 : -1;
            int x = xStart;
            int band = 0;
            while (xStep > 0 ? x <= xEnd : x >= xEnd)
            {
                int next = x + xStep * (Mathf.Max(1, bandWidth) - 1);
                int bandEnd = xStep > 0 ? Mathf.Min(next, xEnd) : Mathf.Max(next, xEnd);
                var lane = BuildColumnSnakeLane(width, height, canSpawn, occupied, x, bandEnd, yStart, yEnd, ((band & 1) == 0) == upFirst);
                AddLaneIfValid(lane, lanes, occupied, width, height);
                x = bandEnd + xStep;
                band++;
            }
        }

        static List<int> BuildColumnSnakeLane(int width, int height, bool[] canSpawn, bool[] occupied, int xStart, int xEnd, int yStart, int yEnd, bool upFirst)
        {
            var path = new List<int>();
            int xStep = xStart <= xEnd ? 1 : -1;
            int column = 0;
            for (int x = xStart; xStep > 0 ? x <= xEnd : x >= xEnd; x += xStep, column++)
            {
                bool up = ((column & 1) == 0) == upFirst;
                int from = up ? yStart : yEnd;
                int to = up ? yEnd : yStart;
                int yStep = from <= to ? 1 : -1;
                for (int y = from; yStep > 0 ? y <= to : y >= to; y += yStep)
                {
                    if (x < 0 || x >= width || y < 0 || y >= height)
                        continue;

                    int idx = x + y * width;
                    if (canSpawn[idx] && !occupied[idx])
                        path.Add(idx);
                }
            }

            return IsContiguousPath(path, width) ? path : new List<int>();
        }

        static void AddLeftoverRowRunLanes(int width, int height, bool[] canSpawn, bool[] occupied, List<List<int>> lanes)
        {
            for (int y = 0; y < height; y++)
            {
                int x = 0;
                while (x < width)
                {
                    while (x < width && (!canSpawn[x + y * width] || occupied[x + y * width]))
                        x++;

                    var lane = new List<int>();
                    while (x < width && canSpawn[x + y * width] && !occupied[x + y * width])
                    {
                        lane.Add(x + y * width);
                        x++;
                    }

                    AddLaneIfValid(lane, lanes, occupied, width, height);
                }
            }
        }

        static void AddLaneIfValid(List<int> lane, List<List<int>> lanes, bool[] occupied, int width, int height)
        {
            if (lane == null || lane.Count < 2 || !IsContiguousPath(lane, width))
                return;

            lanes.Add(lane);
            foreach (int i in lane)
                occupied[i] = true;
        }

        static List<int> EnsureOpenHead(List<int> lane, int width, int height)
        {
            if (LaneHeadExits(lane, width, height))
                return lane;

            var reversed = new List<int>(lane);
            reversed.Reverse();
            return LaneHeadExits(reversed, width, height) ? reversed : lane;
        }

        static bool LaneHeadExits(List<int> lane, int width, int height)
        {
            if (lane.Count < 2)
                return false;

            var head = IndexToPos(lane[^1], width);
            var prev = IndexToPos(lane[^2], width);
            var next = head + (head - prev);
            return !InBounds(width, height, next.x, next.y);
        }

        static List<ChainSpec> SplitLaneIntoChains(List<int> lane, int width, int minLen, int maxLen, int laneIndex)
        {
            var chains = new List<ChainSpec>();
            int start = 0;
            while (lane.Count - start > maxLen)
            {
                int cut = -1;
                int maxCut = Mathf.Min(lane.Count - minLen - 1, start + maxLen - 1);
                for (int i = maxCut; i >= start + minLen - 1; i--)
                {
                    if (IsStraightCut(lane, i, width))
                    {
                        cut = i;
                        break;
                    }
                }

                if (cut < 0)
                    cut = Mathf.Min(maxCut, start + maxLen - 1);

                chains.Add(new ChainSpec { InnerToOuter = lane.GetRange(start, cut - start + 1), LaneIndex = laneIndex });
                start = cut + 1;
            }

            if (lane.Count - start >= 2)
                chains.Add(new ChainSpec { InnerToOuter = lane.GetRange(start, lane.Count - start), LaneIndex = laneIndex });

            return chains;
        }

        static List<ChainSpec> SplitLaneTailDependencyChains(List<int> lane, int width, int minLen, int maxLen, int laneIndex)
        {
            if (lane == null || lane.Count < minLen * 2)
                return new List<ChainSpec> { new ChainSpec { InnerToOuter = lane, LaneIndex = laneIndex } };

            var lastDir = IndexToPos(lane[^1], width) - IndexToPos(lane[^2], width);
            int straightStart = lane.Count - 1;
            while (straightStart > 0)
            {
                var dir = IndexToPos(lane[straightStart], width) - IndexToPos(lane[straightStart - 1], width);
                if (dir != lastDir)
                    break;
                straightStart--;
            }

            int firstCut = straightStart + minLen - 1;
            if (firstCut < minLen - 1 || firstCut >= lane.Count - minLen)
                return new List<ChainSpec> { new ChainSpec { InnerToOuter = lane, LaneIndex = laneIndex } };

            var chains = new List<ChainSpec>
            {
                new ChainSpec { InnerToOuter = lane.GetRange(0, firstCut + 1), LaneIndex = laneIndex }
            };

            int start = firstCut + 1;
            while (lane.Count - start > maxLen)
            {
                chains.Add(new ChainSpec { InnerToOuter = lane.GetRange(start, maxLen), LaneIndex = laneIndex });
                start += maxLen;
            }

            if (lane.Count - start >= minLen)
                chains.Add(new ChainSpec { InnerToOuter = lane.GetRange(start, lane.Count - start), LaneIndex = laneIndex });
            else
                chains[^1].InnerToOuter.AddRange(lane.GetRange(start, lane.Count - start));

            return chains;
        }

        static List<ChainSpec> SplitStraightLaneSeedLikeChains(List<int> lane, int width, int minLen, int maxLen, int laneIndex)
        {
            var chains = new List<ChainSpec>();
            if (lane == null || lane.Count < 2)
                return chains;

            int start = 0;
            int pattern = laneIndex % 5;
            while (lane.Count - start > 0)
            {
                int remaining = lane.Count - start;
                if (remaining < minLen && chains.Count > 0)
                {
                    chains[^1].InnerToOuter.AddRange(lane.GetRange(start, remaining));
                    break;
                }

                int len = pattern switch
                {
                    0 => Mathf.Min(maxLen, Mathf.Max(minLen, 7)),
                    1 => Mathf.Min(maxLen, Mathf.Max(minLen, 9)),
                    2 => Mathf.Min(maxLen, Mathf.Max(minLen, 11)),
                    3 => Mathf.Min(maxLen, Mathf.Max(minLen, 6)),
                    _ => Mathf.Min(maxLen, Mathf.Max(minLen, 8)),
                };

                if (remaining - len > 0 && remaining - len < minLen)
                    len = remaining;
                else
                    len = Mathf.Min(len, remaining);

                if (len >= 2)
                    chains.Add(new ChainSpec { InnerToOuter = lane.GetRange(start, len), LaneIndex = laneIndex });
                start += len;
            }

            return chains;
        }

        static bool IsStraightCut(List<int> path, int cut, int width)
        {
            if (cut - 1 < 0 || cut + 2 >= path.Count)
                return false;

            var a = IndexToPos(path[cut - 1], width);
            var b = IndexToPos(path[cut], width);
            var c = IndexToPos(path[cut + 1], width);
            var d = IndexToPos(path[cut + 2], width);
            return b - a == c - b && c - b == d - c;
        }

        static void AddAuthoredChain(AuthoredLevelData authored, List<int> innerToOuter, int colorIndex)
        {
            if (innerToOuter == null || innerToOuter.Count < 2)
                return;

            var indices = new List<int>(innerToOuter);
            indices.Reverse();
            authored.arrows.Add(new AuthoredArrowData { indices = indices, colorIndex = colorIndex });
        }

        static bool IsContiguousPath(List<int> path, int width)
        {
            for (int i = 1; i < path.Count; i++)
            {
                var a = IndexToPos(path[i - 1], width);
                var b = IndexToPos(path[i], width);
                if (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) != 1)
                    return false;
            }

            return true;
        }

        static int CountLaneTurns(List<int> path, int width)
        {
            int turns = 0;
            for (int i = 2; i < path.Count; i++)
            {
                var a = IndexToPos(path[i - 2], width);
                var b = IndexToPos(path[i - 1], width);
                var c = IndexToPos(path[i], width);
                if (b - a != c - b)
                    turns++;
            }

            return turns;
        }

        static bool IsAdjacentToBlock(int index, int width, int height, bool[] canSpawn)
        {
            var p = IndexToPos(index, width);
            for (int i = 0; i < 4; i++)
            {
                var n = p + DirOffset((Dir)i);
                if (InBounds(width, height, n.x, n.y) && !canSpawn[n.x + n.y * width])
                    return true;
            }

            return false;
        }

        static Vector2Int IndexToPos(int index, int width) => new Vector2Int(index % width, index / width);

        static bool InBounds(int width, int height, int x, int y) => x >= 0 && x < width && y >= 0 && y < height;


        static List<HoleProbeCandidate> BuildHoleEdgeRunFallbackCandidates(int width, int height, bool[] canSpawn, int topCount)
        {
            var result = new List<HoleProbeCandidate>();
            var ruleset = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int allowedCount = CountAllowedCells(canSpawn);

            for (int variant = 0; variant < 8 && result.Count < topCount; variant++)
            {
                bool vertical = variant >= 4;
                var board = vertical
                    ? BuildEdgeRunBoardVertical(width, height, canSpawn, variant)
                    : BuildEdgeRunBoardHorizontal(width, height, canSpawn, variant);

                if (!TryRoundTripAuthoredBoard(board, out var authoredBoard))
                    continue;

                CountShapeViolations(authoredBoard, out int headToHead, out int directBlock, out int badContact);
                if (headToHead > 0 || directBlock > 0 || badContact > 0)
                    continue;

                if (!GreedyValidator.TryClearAllByGreedy(authoredBoard, ruleset, 1200, out _))
                    continue;

                var stats = BoardGenerationTuning.CalculateBoardGenerationStats(authoredBoard, ruleset);
                if (stats.ChainCount <= 0)
                    continue;

                float ratio = stats.InitialMovableArrowChainCount / (float)stats.ChainCount;
                float averageChainLength = stats.ArrowTileCount / (float)Mathf.Max(1, stats.ChainCount);
                int emptyAllowed = Mathf.Max(0, allowedCount - stats.ArrowTileCount);

                result.Add(new HoleProbeCandidate
                {
                    Board = authoredBoard,
                    Stats = stats,
                    Profile = new HoleProbeProfile(vertical ? "fallback_vertical" : "fallback_horizontal", 1f, 2, width * height, 0f),
                    Seed = 990000 + variant,
                    OpeningRatio = ratio,
                    AverageChainLength = averageChainLength,
                    FillRatio = stats.ArrowTileCount / (float)Mathf.Max(1, allowedCount),
                    AllowedEmptyCount = emptyAllowed,
                    Score = emptyAllowed * 10000f + variant,
                    DenseFilled = 0,
                    SingletonRepairs = 0,
                    SingleChains = CountSingleCellChains(authoredBoard),
                    HeadToHeadConflicts = headToHead,
                    DirectBlockAims = directBlock,
                    BadArrowContacts = badContact,
                    ShapeRepairs = 0,
                    ShapeClears = 0,
                    Flips = 0,
                    OuterExits = stats.InitialMovableArrowChainCount,
                    BlockedByArrow = 0,
                    BlockedByHole = 0
                });
            }

            return result;
        }

        static BoardState BuildEdgeRunBoardHorizontal(int width, int height, bool[] canSpawn, int variant)
        {
            var board = CreateMaskBlockedBoard(width, height, canSpawn);
            for (int y = 0; y < height; y++)
            {
                int x = 0;
                while (x < width)
                {
                    while (x < width && !IsAllowed(canSpawn, width, height, x, y))
                        x++;

                    int start = x;
                    while (x < width && IsAllowed(canSpawn, width, height, x, y))
                        x++;

                    int end = x - 1;
                    if (end - start + 1 < 2)
                        continue;

                    Dir outDir = PickHorizontalEdgeDir(start, end, width, y, variant);
                    WriteStraightRun(board, start, y, end, y, outDir);
                }
            }

            return board;
        }

        static BoardState BuildEdgeRunBoardVertical(int width, int height, bool[] canSpawn, int variant)
        {
            var board = CreateMaskBlockedBoard(width, height, canSpawn);
            for (int x = 0; x < width; x++)
            {
                int y = 0;
                while (y < height)
                {
                    while (y < height && !IsAllowed(canSpawn, width, height, x, y))
                        y++;

                    int start = y;
                    while (y < height && IsAllowed(canSpawn, width, height, x, y))
                        y++;

                    int end = y - 1;
                    if (end - start + 1 < 2)
                        continue;

                    Dir outDir = PickVerticalEdgeDir(start, end, height, x, variant);
                    WriteStraightRun(board, x, start, x, end, outDir);
                }
            }

            return board;
        }

        static BoardState CreateMaskBlockedBoard(int width, int height, bool[] canSpawn)
        {
            var board = new BoardState(width, height);
            for (int i = 0; i < board.tiles.Length; i++)
                board.tiles[i] = canSpawn != null && i < canSpawn.Length && canSpawn[i]
                    ? TileState.Empty()
                    : TileState.Block();

            return board;
        }

        static void WriteStraightRun(BoardState board, int x0, int y0, int x1, int y1, Dir outDir)
        {
            Dir inDir = OppositeDir(outDir);
            int dx = Math.Sign(x1 - x0);
            int dy = Math.Sign(y1 - y0);
            for (int y = y0, x = x0;; x += dx, y += dy)
            {
                board.Set(x, y, TileState.Arrow(inDir, outDir));
                if (x == x1 && y == y1)
                    break;
            }
        }

        static Dir PickHorizontalEdgeDir(int start, int end, int width, int row, int variant)
        {
            if (start == 0 && end == width - 1)
                return ((row + variant) & 1) == 0 ? Dir.Right : Dir.Left;
            if (start == 0)
                return Dir.Left;
            if (end == width - 1)
                return Dir.Right;
            return start <= width - 1 - end ? Dir.Left : Dir.Right;
        }

        static Dir PickVerticalEdgeDir(int start, int end, int height, int column, int variant)
        {
            if (start == 0 && end == height - 1)
                return ((column + variant) & 1) == 0 ? Dir.Up : Dir.Down;
            if (start == 0)
                return Dir.Down;
            if (end == height - 1)
                return Dir.Up;
            return start <= height - 1 - end ? Dir.Down : Dir.Up;
        }

        static bool IsAllowed(bool[] canSpawn, int width, int height, int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
                return false;

            int index = x + y * width;
            return canSpawn == null || (index >= 0 && index < canSpawn.Length && canSpawn[index]);
        }

        static void ProbeHoleProfile(
            Texture2D mask,
            bool[] canSpawn,
            HoleProbeProfile profile,
            int attempts,
            out int greedy,
            out int accepted20,
            out int accepted25,
            out HoleProbeCandidate best)
        {
            var generator = new ClearAllArrowsGenerator();
            var ruleset = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int baseSeed = 103000 + profile.MinPath * 1000 + profile.MaxPath * 37 + Mathf.RoundToInt(profile.Twistiness * 100f);
            greedy = 0;
            accepted20 = 0;
            accepted25 = 0;
            best = default;
            best.Score = float.MaxValue;

            for (int attempt = 0; attempt < attempts; attempt++)
            {
                int seed = BoardGenerationTuning.BuildCandidateSeed(baseSeed, attempt);
                var board = generator.Generate(new LevelSpec
                {
                    width = mask.width,
                    height = mask.height,
                    seed = seed,
                    arrowFill = profile.ArrowFill,
                    minPathLen = profile.MinPath,
                    maxPathLen = profile.MaxPath,
                    twistiness = profile.Twistiness,
                    canSpawnHere = canSpawn
                });
                ApplyMaskBlocks(board, canSpawn);

                if (!GreedyValidator.TryClearAllByGreedy(board, ruleset, 400, out _))
                    continue;

                greedy++;
                var stats = BoardGenerationTuning.CalculateBoardGenerationStats(board, ruleset);
                if (stats.ChainCount <= 0)
                    continue;

                float ratio = stats.InitialMovableArrowChainCount / (float)stats.ChainCount;
                float averageChainLength = stats.ArrowTileCount / (float)Mathf.Max(1, stats.ChainCount);
                if (ratio <= 0.20f && averageChainLength >= 5f)
                    accepted20++;
                if (ratio <= 0.25f && averageChainLength >= 5f)
                    accepted25++;

                float score = Mathf.Max(0f, ratio - 0.20f) * 1000f
                              + ratio * 120f
                              + Mathf.Max(0f, 5f - averageChainLength) * 80f
                              + stats.ChainCount * 0.6f
                              - stats.ArrowTileCount * 0.02f
                              + attempt * 0.001f;
                if (score >= best.Score)
                    continue;

                best = new HoleProbeCandidate
                {
                    Board = board,
                    Stats = stats,
                    Profile = profile,
                    Seed = seed,
                    OpeningRatio = ratio,
                    AverageChainLength = averageChainLength,
                    Score = score
                };
            }
        }

        static bool IsBetterHoleCandidate(HoleProbeCandidate candidate, HoleProbeCandidate current)
        {
            bool candidatePasses = candidate.OpeningRatio <= 0.20f && candidate.AverageChainLength >= 5f;
            bool currentPasses = current.OpeningRatio <= 0.20f && current.AverageChainLength >= 5f;
            if (candidatePasses != currentPasses)
                return candidatePasses;

            int ratioCompare = candidate.OpeningRatio.CompareTo(current.OpeningRatio);
            if (ratioCompare != 0)
                return ratioCompare < 0;

            int avgCompare = current.AverageChainLength.CompareTo(candidate.AverageChainLength);
            if (avgCompare != 0)
                return avgCompare < 0;

            return candidate.Stats.ArrowTileCount > current.Stats.ArrowTileCount;
        }

        static int ReduceOpeningByReversingChains(BoardState board, IRuleset ruleset, int maxFlips)
        {
            int flips = 0;
            for (int pass = 0; pass < 2 && flips < maxFlips; pass++)
            {
                var chains = CollectChains(board);
                bool changed = false;
                foreach (var chain in chains)
                {
                    if (flips >= maxFlips)
                        break;

                    var beforeStats = CalculateHoleCheapStats(board, ruleset);
                    var beforeQuality = AnalyzeOpeningQuality(board, ruleset);
                    float beforeScore = OpeningControlScore(beforeStats, beforeQuality);

                    ReverseChain(board, chain);
                    var afterStats = CalculateHoleCheapStats(board, ruleset);
                    var afterQuality = AnalyzeOpeningQuality(board, ruleset);
                    float afterScore = OpeningControlScore(afterStats, afterQuality);

                    if (afterStats.ChainCount > 0 &&
                        afterScore + 0.01f < beforeScore &&
                        GreedyValidator.TryClearAllByGreedy(board, ruleset, 500, out _))
                    {
                        flips++;
                        changed = true;
                    }
                    else
                    {
                        ReverseChain(board, chain);
                    }
                }

                if (!changed)
                    break;
            }

            return flips;
        }

        static bool TryAnalyzeLayeredReleaseWindows(BoardState board, IRuleset ruleset, int maxMoves, out V11LayerStats stats)
        {
            stats = default;
            if (board == null || ruleset == null)
                return false;

            var initialChains = CollectChains(board);
            if (initialChains.Count == 0)
                return false;

            var indexToInitialChain = new Dictionary<int, int>(board.tiles.Length);
            for (int chainId = 0; chainId < initialChains.Count; chainId++)
                foreach (int idx in initialChains[chainId])
                    indexToInitialChain[idx] = chainId;

            var state = CloneBoardForV11Analysis(board);
            var firstOpenStep = new Dictionary<int, int>();
            var clearedChains = new HashSet<int>();
            var layerCounts = new Dictionary<int, int>();
            int moves = 0;

            for (int step = 0; step < maxMoves; step++)
            {
                if (ruleset.IsSolved(state))
                    break;

                RecordOpenOriginalChains(state, ruleset, indexToInitialChain, step, firstOpenStep, layerCounts);
                if (!TryFindBestGreedyMoveForV11(state, ruleset, out var bestMove, out int cleared) || cleared <= 0)
                    break;

                if (!ruleset.TryApplyMove(state, bestMove, out var applied))
                    break;

                AddClearedOriginalChains(applied, indexToInitialChain, step, firstOpenStep, clearedChains, layerCounts);
                moves++;
            }

            bool solved = ruleset.IsSolved(state);
            int maxLayerWidth = 0;
            foreach (var pair in layerCounts)
                maxLayerWidth = Mathf.Max(maxLayerWidth, pair.Value);

            int layer0Chains = layerCounts.TryGetValue(0, out int l0) ? l0 : 0;
            int initialChainCount = initialChains.Count;
            stats.InitialChainCount = initialChainCount;
            stats.LayerCount = layerCounts.Count;
            stats.Layer0Chains = layer0Chains;
            stats.Layer0Ratio = layer0Chains / (float)Mathf.Max(1, initialChainCount);
            stats.MaxLayerWidth = maxLayerWidth;
            stats.DeadChains = Mathf.Max(0, initialChainCount - clearedChains.Count);
            stats.UnexpectedEarlyOpen =
                Mathf.Max(0, layer0Chains - Mathf.CeilToInt(initialChainCount * 0.52f)) +
                Mathf.Max(0, maxLayerWidth - Mathf.CeilToInt(initialChainCount * 0.48f));
            stats.LayerBalance = CalculateLayerBalance(layerCounts, initialChainCount);
            stats.GreedyMoves = moves;
            stats.LayerSummary = BuildLayerSummary(layerCounts);
            return solved;
        }

        static void RecordOpenOriginalChains(
            BoardState state,
            IRuleset ruleset,
            Dictionary<int, int> indexToInitialChain,
            int step,
            Dictionary<int, int> firstOpenStep,
            Dictionary<int, int> layerCounts)
        {
            var chains = CollectChains(state);
            foreach (var chain in chains)
            {
                int chainId = ResolveInitialChainId(chain, indexToInitialChain);
                if (chainId < 0 || firstOpenStep.ContainsKey(chainId))
                    continue;

                if (!CanClearAnyTileInChain(state, ruleset, chain))
                    continue;

                firstOpenStep[chainId] = step;
                IncrementLayerCount(layerCounts, step);
            }
        }

        static void AddClearedOriginalChains(
            MoveDelta delta,
            Dictionary<int, int> indexToInitialChain,
            int step,
            Dictionary<int, int> firstOpenStep,
            HashSet<int> clearedChains,
            Dictionary<int, int> layerCounts)
        {
            var changes = delta.changes;
            for (int i = 0; i < changes.Count; i++)
            {
                var c = changes[i];
                if (c.before.type != TileType.Arrow || c.after.type != TileType.Empty)
                    continue;

                if (!indexToInitialChain.TryGetValue(c.index, out int chainId))
                    continue;

                clearedChains.Add(chainId);
                if (firstOpenStep.ContainsKey(chainId))
                    continue;

                firstOpenStep[chainId] = step;
                IncrementLayerCount(layerCounts, step);
            }
        }

        static int ResolveInitialChainId(List<int> chain, Dictionary<int, int> indexToInitialChain)
        {
            if (chain == null)
                return -1;

            foreach (int idx in chain)
                if (indexToInitialChain.TryGetValue(idx, out int chainId))
                    return chainId;

            return -1;
        }

        static void IncrementLayerCount(Dictionary<int, int> layerCounts, int step)
        {
            layerCounts.TryGetValue(step, out int count);
            layerCounts[step] = count + 1;
        }

        static bool TryFindBestGreedyMoveForV11(BoardState state, IRuleset ruleset, out Move bestMove, out int bestCleared)
        {
            bestMove = default;
            bestCleared = 0;
            foreach (var move in ruleset.GetLegalMoves(state))
            {
                if (!ruleset.TryApplyMove(state, move, out var delta))
                    continue;

                int cleared = CountClearedArrowsForV11(delta);
                delta.Undo(state);
                if (cleared <= bestCleared)
                    continue;

                bestCleared = cleared;
                bestMove = move;
            }

            return bestCleared > 0;
        }

        static int CountClearedArrowsForV11(MoveDelta delta)
        {
            int cleared = 0;
            var changes = delta.changes;
            for (int i = 0; i < changes.Count; i++)
            {
                var c = changes[i];
                if (c.before.type == TileType.Arrow && c.after.type == TileType.Empty)
                    cleared++;
            }

            return cleared;
        }

        static float CalculateLayerBalance(Dictionary<int, int> layerCounts, int totalChains)
        {
            if (layerCounts == null || layerCounts.Count <= 1 || totalChains <= 0)
                return 0f;

            float entropy = 0f;
            foreach (var pair in layerCounts)
            {
                float p = pair.Value / (float)totalChains;
                if (p > 0f)
                    entropy -= p * Mathf.Log(p, 2f);
            }

            return Mathf.Clamp01(entropy / Mathf.Max(0.0001f, Mathf.Log(layerCounts.Count, 2f)));
        }

        static string BuildLayerSummary(Dictionary<int, int> layerCounts)
        {
            if (layerCounts == null || layerCounts.Count == 0)
                return "";

            var parts = new List<string>();
            foreach (var pair in layerCounts.OrderBy(p => p.Key).Take(18))
                parts.Add($"L{pair.Key}:{pair.Value}");

            if (layerCounts.Count > 18)
                parts.Add("...");

            return string.Join("|", parts);
        }

        static BoardState CloneBoardForV11Analysis(BoardState src)
        {
            var dst = new BoardState(src.width, src.height);
            Array.Copy(src.tiles, dst.tiles, src.tiles.Length);
            return dst;
        }

        static float CalculateV11StripeUniformityPenalty(BoardState board)
        {
            if (board == null || board.tiles == null)
                return 1f;

            float rowPenalty = CalculateV11AxisUniformityPenalty(board, rows: true);
            float columnPenalty = CalculateV11AxisUniformityPenalty(board, rows: false);
            return Mathf.Clamp01(rowPenalty * 0.75f + columnPenalty * 0.25f);
        }

        static float CalculateV11AxisUniformityPenalty(BoardState board, bool rows)
        {
            int primaryCount = rows ? board.height : board.width;
            int secondaryCount = rows ? board.width : board.height;
            int sampled = 0;
            float penalty = 0f;

            for (int a = 0; a < primaryCount; a++)
            {
                int arrows = 0;
                int left = 0, right = 0, up = 0, down = 0;
                for (int b = 0; b < secondaryCount; b++)
                {
                    int x = rows ? b : a;
                    int y = rows ? a : b;
                    var tile = board.tiles[x + y * board.width];
                    if (tile.type != TileType.Arrow)
                        continue;

                    arrows++;
                    switch (tile.arrow.outDir)
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

                if (arrows < 5)
                    continue;

                sampled++;
                int dominant = Mathf.Max(Mathf.Max(left, right), Mathf.Max(up, down));
                float dominance = dominant / (float)arrows;
                float axisRatio = rows ? (left + right) / (float)arrows : (up + down) / (float)arrows;
                penalty += Mathf.Max(0f, dominance - 0.62f) * 1.2f;
                penalty += Mathf.Max(0f, axisRatio - 0.78f) * 1.6f;
            }

            return sampled > 0 ? penalty / sampled : 0f;
        }

        struct V13OuterExitStats
        {
            public int DirectOuterExits;
            public int ShortOuterExits;
            public int WrappedOuterExits;
            public int BoundaryStraightOuterExits;
            public int NakedPenalty;
            public int EdgeClusterPenalty;
        }

        static V13OuterExitStats AnalyzeV13OuterExitExposure(AuthoredLevelData authored, BoardState board)
        {
            var stats = new V13OuterExitStats();
            if (authored?.arrows == null || board?.tiles == null || board.width <= 0 || board.height <= 0)
                return stats;

            var directBySide = new int[4];
            var shortBySide = new int[4];
            var boundaryStraightBySide = new int[4];
            foreach (var arrow in authored.arrows)
            {
                var indices = arrow?.indices;
                if (indices == null || indices.Count < 2)
                    continue;

                int headIndex = indices[0];
                if (headIndex < 0 || headIndex >= board.tiles.Length)
                    continue;

                var tile = board.tiles[headIndex];
                if (tile.type != TileType.Arrow)
                    continue;

                var head = IndexToPos(headIndex, board.width);
                var next = head + DirOffset(tile.arrow.outDir);
                if (board.InBounds(next.x, next.y))
                    continue;

                int side = GetV13ExitSide(tile.arrow.outDir);
                stats.DirectOuterExits++;
                directBySide[side]++;

                int len = indices.Count;
                int turns = CountAuthoredChainTurns(indices, board.width);
                bool shortExit = len <= 5;
                bool straight = turns == 0;
                bool wrapped = len >= 6 && turns > 0;
                bool boundaryStraight = straight && len <= V13BoundaryStraightOuterLength;

                if (shortExit)
                {
                    stats.ShortOuterExits++;
                    shortBySide[side]++;
                    stats.NakedPenalty += 7 + Mathf.Max(0, 6 - len) * 3;
                    if (straight)
                    {
                        stats.NakedPenalty += 8;
                        if (boundaryStraight)
                            stats.BoundaryStraightOuterExits++;
                    }
                }
                else if (straight && len <= 8)
                {
                    stats.NakedPenalty += 4;
                }

                if (boundaryStraight)
                    boundaryStraightBySide[side]++;

                if (wrapped)
                    stats.WrappedOuterExits++;
            }

            for (int side = 0; side < 4; side++)
            {
                int directExcess = Mathf.Max(0, directBySide[side] - 12);
                int shortExcess = Mathf.Max(0, shortBySide[side] - 4);
                stats.EdgeClusterPenalty += directExcess * directExcess + shortExcess * shortExcess * 3;
                stats.EdgeClusterPenalty += boundaryStraightBySide[side] * 2;
            }

            return stats;
        }

        static int GetV13ExitSide(Dir outDir)
        {
            return outDir switch
            {
                Dir.Up => 0,
                Dir.Right => 1,
                Dir.Down => 2,
                Dir.Left => 3,
                _ => 0
            };
        }

        struct V13OuterExitEntry
        {
            public int ArrowIndex;
            public int Length;
            public int Penalty;
        }

        static bool TryCleanupV13OuterExits(AuthoredLevelData source, int width, int height, bool[] canSpawn, int allowedCount, ArrowMagicRuleset ruleset, out AuthoredLevelData cleanedAuthored, out BoardState cleanedBoard, out BoardGenerationTuning.BoardGenerationStats cleanedStats, out V13OuterExitStats cleanedExitStats, out int ops, out int refillOps)
        {
            cleanedAuthored = null;
            cleanedBoard = null;
            cleanedStats = default;
            cleanedExitStats = default;
            ops = 0;
            refillOps = 0;
            if (source?.arrows == null || ruleset == null)
                return false;

            var currentAuthored = CloneAuthoredLevel(source);
            if (!AuthoredLevelBuilder.TryBuildBoard(currentAuthored, out var currentBoard, out _))
                return false;
            if (!TryRoundTripAuthoredBoard(currentBoard, out currentAuthored, out currentBoard))
                return false;

            var currentStats = BoardGenerationTuning.CalculateBoardGenerationStats(currentBoard, ruleset);
            var currentExit = AnalyzeV13OuterExitExposure(currentAuthored, currentBoard);
            if (currentExit.BoundaryStraightOuterExits > V13MaxBoundaryStraightOuterExits || currentExit.ShortOuterExits > V13MaxShortOuterExitsForTop5)
                return false;
            int currentScore = ScoreV13ExitCleanup(currentExit);
            float currentFill = currentStats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
            if (currentFill < 0.80f)
                return false;
            float originalFill = currentFill;

            int mergeOps = 0;
            for (int pass = 0; pass < 10; pass++)
            {
                var entries = CollectV13ShortOuterExitEntries(currentAuthored, currentBoard);
                if (entries.Count == 0)
                    break;

                entries.Sort((a, b) => b.Penalty.CompareTo(a.Penalty));
                bool accepted = false;
                int limit = Mathf.Min(entries.Count, 14);
                for (int i = 0; i < limit && !accepted; i++)
                {
                    var entry = entries[i];
                    bool allowMerge = mergeOps < 4 && (pass % 2 == 0 || currentExit.ShortOuterExits >= 32);
                    AuthoredLevelData nextAuthored = null;
                    BoardState nextBoard = null;
                    BoardGenerationTuning.BoardGenerationStats nextStats = default;
                    V13OuterExitStats nextExit = default;
                    int nextScore = currentScore;
                    bool mergedAccepted = allowMerge && TryEvaluateV13MergeOperation(currentAuthored, width, height, canSpawn, allowedCount, ruleset, currentScore, entry, entries, out nextAuthored, out nextBoard, out nextStats, out nextExit, out nextScore);
                    if (mergedAccepted ||
                        TryEvaluateV13CleanupOperation(currentAuthored, width, height, canSpawn, allowedCount, ruleset, currentScore, entry, "flip", out nextAuthored, out nextBoard, out nextStats, out nextExit, out nextScore) ||
                        TryEvaluateV13CleanupOperation(currentAuthored, width, height, canSpawn, allowedCount, ruleset, currentScore, entry, "wrap", out nextAuthored, out nextBoard, out nextStats, out nextExit, out nextScore) ||
                        TryEvaluateV13CleanupOperation(currentAuthored, width, height, canSpawn, allowedCount, ruleset, currentScore, entry, "remove", out nextAuthored, out nextBoard, out nextStats, out nextExit, out nextScore))
                    {
                        if (mergedAccepted)
                            mergeOps++;
                        currentAuthored = nextAuthored;
                        currentBoard = nextBoard;
                        currentStats = nextStats;
                        currentExit = nextExit;
                        currentScore = nextScore;
                        ops++;
                        accepted = true;
                    }
                }

                if (!accepted)
                    break;
            }

            if (ops == 0)
                return false;

            TryRefillV13InteriorAfterCleanup(currentAuthored, width, height, canSpawn, allowedCount, ruleset, originalFill, currentExit.ShortOuterExits, out currentAuthored, out currentBoard, out currentStats, out currentExit, out refillOps);

            cleanedAuthored = currentAuthored;
            cleanedBoard = currentBoard;
            cleanedStats = currentStats;
            cleanedExitStats = currentExit;
            return true;
        }

        static int ScoreV13ExitCleanup(V13OuterExitStats stats)
        {
            return stats.NakedPenalty + stats.EdgeClusterPenalty * 2 + stats.ShortOuterExits * 12 + stats.BoundaryStraightOuterExits * 20 + stats.DirectOuterExits * 2 - stats.WrappedOuterExits * 3;
        }

        static bool TryAddV13HoleRingMotifs(AuthoredLevelData source, int width, int height, bool[] canSpawn, int allowedCount, ArrowMagicRuleset ruleset, int maxShortOuter, out AuthoredLevelData ringAuthored, out BoardState ringBoard, out BoardGenerationTuning.BoardGenerationStats ringStats, out V13OuterExitStats ringExit, out int ringOps)
        {
            ringAuthored = source;
            ringBoard = null;
            ringStats = default;
            ringExit = default;
            ringOps = 0;
            if (source?.arrows == null || canSpawn == null || ruleset == null)
                return false;

            if (!AuthoredLevelBuilder.TryBuildBoard(source, out var board, out _) ||
                !TryRoundTripAuthoredBoard(board, out var currentAuthored, out var currentBoard))
                return false;

            var currentStats = BoardGenerationTuning.CalculateBoardGenerationStats(currentBoard, ruleset);
            var currentExit = AnalyzeV13OuterExitExposure(currentAuthored, currentBoard);
            float currentFill = currentStats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);

            for (int pass = 0; pass < 3; pass++)
            {
                bool accepted = false;
                var occupied = BuildV13OccupiedMap(currentAuthored, width, height);
                var starts = CollectV13HoleRingStarts(width, height, canSpawn, occupied, pass * 41 + currentAuthored.arrows.Count);
                foreach (int start in starts)
                {
                    for (int shape = 0; shape < 6 && !accepted; shape++)
                    {
                        if (!TryBuildV13HoleRingPath(start, width, height, canSpawn, occupied, pass + shape * 13, shape, out var path))
                            continue;

                        if (!TryValidateV13RingCandidate(currentAuthored, path, allowedCount, ruleset, maxShortOuter, currentFill, out var nextAuthored, out var nextBoard, out var nextStats, out var nextExit))
                        {
                            path.Reverse();
                            if (!TryValidateV13RingCandidate(currentAuthored, path, allowedCount, ruleset, maxShortOuter, currentFill, out nextAuthored, out nextBoard, out nextStats, out nextExit))
                                continue;
                        }

                        currentAuthored = nextAuthored;
                        currentBoard = nextBoard;
                        currentStats = nextStats;
                        currentExit = nextExit;
                        currentFill = currentStats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
                        ringOps++;
                        accepted = true;
                    }

                    if (accepted)
                        break;
                }

                if (!accepted)
                    break;
            }

            ringAuthored = currentAuthored;
            ringBoard = currentBoard;
            ringStats = currentStats;
            ringExit = currentExit;
            return ringOps > 0;
        }

        static bool TryValidateV13RingCandidate(AuthoredLevelData source, List<int> path, int allowedCount, ArrowMagicRuleset ruleset, int maxShortOuter, float currentFill, out AuthoredLevelData nextAuthored, out BoardState nextBoard, out BoardGenerationTuning.BoardGenerationStats nextStats, out V13OuterExitStats nextExit)
        {
            nextAuthored = null;
            nextBoard = null;
            nextStats = default;
            nextExit = default;
            if (path == null || path.Count < 4)
                return false;

            var candidate = CloneAuthoredLevel(source);
            candidate.arrows.Add(new AuthoredArrowData { indices = new List<int>(path), colorIndex = candidate.arrows.Count });
            if (!AuthoredLevelBuilder.TryBuildBoard(candidate, out var board, out _) ||
                !TryRoundTripAuthoredBoard(board, out var roundTripAuthored, out var roundTripBoard))
                return false;

            var stats = BoardGenerationTuning.CalculateBoardGenerationStats(roundTripBoard, ruleset);
            float fill = stats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
            if (fill <= currentFill + 0.001f || fill < 0.80f)
                return false;

            CountShapeViolations(roundTripBoard, out int headToHead, out int directBlock, out _);
            if (headToHead > 0 || directBlock > 0)
                return false;

            if (!GreedyValidator.TryClearAllByGreedy(roundTripBoard, ruleset, 3200, out _))
                return false;

            var exit = AnalyzeV13OuterExitExposure(roundTripAuthored, roundTripBoard);
            if (exit.BoundaryStraightOuterExits > V13MaxBoundaryStraightOuterExits)
                return false;
            if (exit.ShortOuterExits > maxShortOuter)
                return false;

            nextAuthored = roundTripAuthored;
            nextBoard = roundTripBoard;
            nextStats = stats;
            nextExit = exit;
            return true;
        }

        static List<int> CollectV13HoleRingStarts(int width, int height, bool[] canSpawn, bool[] occupied, int salt)
        {
            var result = new List<int>();
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    int idx = x + y * width;
                    if (idx < 0 || idx >= canSpawn.Length || idx >= occupied.Length || !canSpawn[idx] || occupied[idx])
                        continue;
                    if (!IsV13NearHoleBlock(width, height, canSpawn, x, y, 3))
                        continue;
                    result.Add(idx);
                }
            }

            result.Sort((a, b) => PositiveMod(a * 37 + salt, 997).CompareTo(PositiveMod(b * 37 + salt, 997)));
            if (result.Count > 80)
                result.RemoveRange(80, result.Count - 80);
            return result;
        }

        static bool TryBuildV13HoleRingPath(int start, int width, int height, bool[] canSpawn, bool[] occupied, int salt, int shape, out List<int> path)
        {
            path = null;
            var origin = IndexToPos(start, width);
            var dirs = new[] { Vector2Int.right, Vector2Int.up, Vector2Int.left, Vector2Int.down };
            int first = PositiveMod(salt + shape * 3 + start, dirs.Length);
            for (int attempt = 0; attempt < dirs.Length; attempt++)
            {
                var a = dirs[(first + attempt) % dirs.Length];
                var b = dirs[(first + attempt + 1 + PositiveMod(shape, 2) * 2) % dirs.Length];
                if (a + b == Vector2Int.zero)
                    continue;

                var points = shape switch
                {
                    0 => new List<Vector2Int> { origin, origin + a, origin + a * 2, origin + a * 2 + b, origin + a + b, origin + b },
                    1 => new List<Vector2Int> { origin, origin + a, origin + a * 2, origin + a * 3, origin + a * 3 + b, origin + a * 3 + b * 2, origin + a * 2 + b * 2 },
                    2 => new List<Vector2Int> { origin, origin + a, origin + a + b, origin + a + b * 2, origin + a * 2 + b * 2, origin + a * 3 + b * 2 },
                    3 => new List<Vector2Int> { origin, origin + a, origin + a + b, origin + b },
                    4 => new List<Vector2Int> { origin, origin + a, origin + a * 2, origin + a * 2 + b, origin + a * 2 + b * 2, origin + a + b * 2 },
                    _ => new List<Vector2Int> { origin, origin + a, origin + a * 2, origin + a * 2 + b, origin + a + b, origin + a + b * 2 }
                };

                bool nearHole = true;
                foreach (var p in points)
                {
                    if (!IsV13NearHoleBlock(width, height, canSpawn, p.x, p.y, 3))
                    {
                        nearHole = false;
                        break;
                    }
                }
                if (!nearHole)
                    continue;

                if (!TryConvertV13RefillPath(points, width, height, canSpawn, occupied, out path))
                    continue;
                if (CountLaneTurns(path, width) < 2)
                    continue;
                return true;
            }

            return false;
        }

        static bool IsV13NearHoleBlock(int width, int height, bool[] canSpawn, int x, int y, int radius)
        {
            if (!InBounds(width, height, x, y) || canSpawn == null)
                return false;

            for (int dy = -radius; dy <= radius; dy++)
            for (int dx = -radius; dx <= radius; dx++)
            {
                int dist = Mathf.Abs(dx) + Mathf.Abs(dy);
                if (dist <= 0 || dist > radius)
                    continue;
                int nx = x + dx;
                int ny = y + dy;
                if (!InBounds(width, height, nx, ny))
                    continue;
                int idx = nx + ny * width;
                if (idx >= 0 && idx < canSpawn.Length && !canSpawn[idx])
                    return true;
            }

            return false;
        }

        static List<V13OuterExitEntry> CollectV13ShortOuterExitEntries(AuthoredLevelData authored, BoardState board)
        {
            var result = new List<V13OuterExitEntry>();
            if (authored?.arrows == null || board?.tiles == null)
                return result;

            for (int i = 0; i < authored.arrows.Count; i++)
            {
                var indices = authored.arrows[i]?.indices;
                if (indices == null || indices.Count < 2 || indices.Count > 6)
                    continue;

                int headIndex = indices[0];
                if (headIndex < 0 || headIndex >= board.tiles.Length)
                    continue;

                var tile = board.tiles[headIndex];
                if (tile.type != TileType.Arrow)
                    continue;

                var head = IndexToPos(headIndex, board.width);
                var next = head + DirOffset(tile.arrow.outDir);
                if (board.InBounds(next.x, next.y))
                    continue;

                int turns = CountAuthoredChainTurns(indices, board.width);
                bool boundaryStraight = turns == 0 && indices.Count <= V13BoundaryStraightOuterLength;
                int penalty = 20 + Mathf.Max(0, 7 - indices.Count) * 4 + (turns == 0 ? 12 : 0) + (boundaryStraight ? 12 : 0);
                result.Add(new V13OuterExitEntry { ArrowIndex = i, Length = indices.Count, Penalty = penalty });
            }

            return result;
        }

        static bool TryEvaluateV13CleanupOperation(AuthoredLevelData source, int width, int height, bool[] canSpawn, int allowedCount, ArrowMagicRuleset ruleset, int currentScore, V13OuterExitEntry entry, string op, out AuthoredLevelData nextAuthored, out BoardState nextBoard, out BoardGenerationTuning.BoardGenerationStats nextStats, out V13OuterExitStats nextExit, out int nextScore)
        {
            nextAuthored = null;
            nextBoard = null;
            nextStats = default;
            nextExit = default;
            nextScore = currentScore;

            var candidate = CloneAuthoredLevel(source);
            if (entry.ArrowIndex < 0 || entry.ArrowIndex >= candidate.arrows.Count)
                return false;

            var indices = candidate.arrows[entry.ArrowIndex]?.indices;
            if (indices == null || indices.Count < 2)
                return false;

            if (op == "flip")
            {
                indices.Reverse();
            }
            else if (op == "remove")
            {
                candidate.arrows.RemoveAt(entry.ArrowIndex);
            }
            else if (op == "wrap")
            {
                if (!TryWrapSingleV13OuterExit(candidate, width, height, canSpawn, entry.ArrowIndex))
                    return false;
            }
            else
            {
                return false;
            }

            if (!AuthoredLevelBuilder.TryBuildBoard(candidate, out var board, out _))
                return false;
            if (!TryRoundTripAuthoredBoard(board, out var roundTripAuthored, out var roundTripBoard))
                return false;

            var stats = BoardGenerationTuning.CalculateBoardGenerationStats(roundTripBoard, ruleset);
            float fill = stats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
            if (fill < 0.80f)
                return false;

            CountShapeViolations(roundTripBoard, out int headToHead, out int directBlock, out _);
            if (headToHead > 0 || directBlock > 0)
                return false;

            if (!GreedyValidator.TryClearAllByGreedy(roundTripBoard, ruleset, 3200, out _))
                return false;

            var exit = AnalyzeV13OuterExitExposure(roundTripAuthored, roundTripBoard);
            if (exit.BoundaryStraightOuterExits > V13MaxBoundaryStraightOuterExits)
                return false;
            int score = ScoreV13ExitCleanup(exit);
            if (score >= currentScore - 10)
                return false;

            nextAuthored = roundTripAuthored;
            nextBoard = roundTripBoard;
            nextStats = stats;
            nextExit = exit;
            nextScore = score;
            return true;
        }

        static bool TryEvaluateV13MergeOperation(AuthoredLevelData source, int width, int height, bool[] canSpawn, int allowedCount, ArrowMagicRuleset ruleset, int currentScore, V13OuterExitEntry entry, List<V13OuterExitEntry> entries, out AuthoredLevelData nextAuthored, out BoardState nextBoard, out BoardGenerationTuning.BoardGenerationStats nextStats, out V13OuterExitStats nextExit, out int nextScore)
        {
            nextAuthored = null;
            nextBoard = null;
            nextStats = default;
            nextExit = default;
            nextScore = currentScore;
            if (entries == null)
                return false;

            for (int i = 0; i < entries.Count; i++)
            {
                var partner = entries[i];
                if (partner.ArrowIndex == entry.ArrowIndex)
                    continue;

                var candidate = CloneAuthoredLevel(source);
                if (!TryMergeV13SameDirectionShortExits(candidate, width, height, canSpawn, entry.ArrowIndex, partner.ArrowIndex))
                    continue;

                if (!TryValidateV13CleanupCandidate(candidate, allowedCount, ruleset, currentScore, out nextAuthored, out nextBoard, out nextStats, out nextExit, out nextScore))
                    continue;

                return true;
            }

            return false;
        }

        static bool TryValidateV13CleanupCandidate(AuthoredLevelData candidate, int allowedCount, ArrowMagicRuleset ruleset, int currentScore, out AuthoredLevelData nextAuthored, out BoardState nextBoard, out BoardGenerationTuning.BoardGenerationStats nextStats, out V13OuterExitStats nextExit, out int nextScore)
        {
            nextAuthored = null;
            nextBoard = null;
            nextStats = default;
            nextExit = default;
            nextScore = currentScore;
            if (!AuthoredLevelBuilder.TryBuildBoard(candidate, out var board, out _))
                return false;
            if (!TryRoundTripAuthoredBoard(board, out var roundTripAuthored, out var roundTripBoard))
                return false;

            var stats = BoardGenerationTuning.CalculateBoardGenerationStats(roundTripBoard, ruleset);
            float fill = stats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
            if (fill < 0.80f)
                return false;

            CountShapeViolations(roundTripBoard, out int headToHead, out int directBlock, out _);
            if (headToHead > 0 || directBlock > 0)
                return false;

            if (!GreedyValidator.TryClearAllByGreedy(roundTripBoard, ruleset, 3200, out _))
                return false;

            var exit = AnalyzeV13OuterExitExposure(roundTripAuthored, roundTripBoard);
            if (exit.BoundaryStraightOuterExits > V13MaxBoundaryStraightOuterExits)
                return false;
            int score = ScoreV13ExitCleanup(exit);
            if (score >= currentScore - 10)
                return false;

            nextAuthored = roundTripAuthored;
            nextBoard = roundTripBoard;
            nextStats = stats;
            nextExit = exit;
            nextScore = score;
            return true;
        }

        static bool TryRefillV13InteriorAfterCleanup(AuthoredLevelData source, int width, int height, bool[] canSpawn, int allowedCount, ArrowMagicRuleset ruleset, float targetFill, int maxShortOuter, out AuthoredLevelData refilledAuthored, out BoardState refilledBoard, out BoardGenerationTuning.BoardGenerationStats refilledStats, out V13OuterExitStats refilledExit, out int refillOps)
        {
            refilledAuthored = source;
            refilledBoard = null;
            refilledStats = default;
            refilledExit = default;
            refillOps = 0;
            if (source?.arrows == null || canSpawn == null || ruleset == null)
                return false;

            if (!AuthoredLevelBuilder.TryBuildBoard(source, out var board, out _) ||
                !TryRoundTripAuthoredBoard(board, out var currentAuthored, out var currentBoard))
                return false;

            var currentStats = BoardGenerationTuning.CalculateBoardGenerationStats(currentBoard, ruleset);
            var currentExit = AnalyzeV13OuterExitExposure(currentAuthored, currentBoard);
            float currentFill = currentStats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
            float fillTarget = Mathf.Min(targetFill, 0.82f);
            if (currentFill >= fillTarget - 0.001f)
            {
                refilledAuthored = currentAuthored;
                refilledBoard = currentBoard;
                refilledStats = currentStats;
                refilledExit = currentExit;
                return false;
            }

            for (int pass = 0; pass < 8 && currentFill < fillTarget - 0.001f; pass++)
            {
                bool accepted = false;
                var occupied = BuildV13OccupiedMap(currentAuthored, width, height);
                var starts = CollectV13InteriorRefillStarts(width, height, canSpawn, occupied, pass * 37 + currentAuthored.arrows.Count);
                foreach (int start in starts)
                {
                    for (int shape = 0; shape < 4 && !accepted; shape++)
                    {
                        if (!TryBuildV13InteriorRefillPath(start, width, height, canSpawn, occupied, pass + shape * 11, shape, out var path))
                            continue;

                        var candidate = CloneAuthoredLevel(currentAuthored);
                        candidate.arrows.Add(new AuthoredArrowData { indices = path, colorIndex = candidate.arrows.Count });
                        if (!TryValidateV13RefillCandidate(candidate, allowedCount, ruleset, maxShortOuter, currentFill, out var nextAuthored, out var nextBoard, out var nextStats, out var nextExit))
                            continue;

                        currentAuthored = nextAuthored;
                        currentBoard = nextBoard;
                        currentStats = nextStats;
                        currentExit = nextExit;
                        currentFill = currentStats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
                        refillOps++;
                        accepted = true;
                    }

                    if (accepted)
                        break;
                }

                if (!accepted)
                    break;
            }

            refilledAuthored = currentAuthored;
            refilledBoard = currentBoard;
            refilledStats = currentStats;
            refilledExit = currentExit;
            return refillOps > 0;
        }

        static bool TryValidateV13RefillCandidate(AuthoredLevelData candidate, int allowedCount, ArrowMagicRuleset ruleset, int maxShortOuter, float currentFill, out AuthoredLevelData nextAuthored, out BoardState nextBoard, out BoardGenerationTuning.BoardGenerationStats nextStats, out V13OuterExitStats nextExit)
        {
            nextAuthored = null;
            nextBoard = null;
            nextStats = default;
            nextExit = default;
            if (!AuthoredLevelBuilder.TryBuildBoard(candidate, out var board, out _))
                return false;
            if (!TryRoundTripAuthoredBoard(board, out var roundTripAuthored, out var roundTripBoard))
                return false;

            var stats = BoardGenerationTuning.CalculateBoardGenerationStats(roundTripBoard, ruleset);
            float fill = stats.ArrowTileCount / (float)Mathf.Max(1, allowedCount);
            if (fill <= currentFill + 0.001f || fill < 0.80f)
                return false;

            CountShapeViolations(roundTripBoard, out int headToHead, out int directBlock, out _);
            if (headToHead > 0 || directBlock > 0)
                return false;

            if (!GreedyValidator.TryClearAllByGreedy(roundTripBoard, ruleset, 3200, out _))
                return false;

            var exit = AnalyzeV13OuterExitExposure(roundTripAuthored, roundTripBoard);
            if (exit.BoundaryStraightOuterExits > V13MaxBoundaryStraightOuterExits)
                return false;
            if (exit.ShortOuterExits > maxShortOuter)
                return false;

            nextAuthored = roundTripAuthored;
            nextBoard = roundTripBoard;
            nextStats = stats;
            nextExit = exit;
            return true;
        }

        static bool[] BuildV13OccupiedMap(AuthoredLevelData data, int width, int height)
        {
            var occupied = new bool[width * height];
            var blockIndices = GetAuthoredBlockIndices(data);
            if (blockIndices != null)
            {
                foreach (int idx in blockIndices)
                    if (idx >= 0 && idx < occupied.Length)
                        occupied[idx] = true;
            }

            if (data?.arrows != null)
            {
                foreach (var arrow in data.arrows)
                {
                    if (arrow?.indices == null)
                        continue;
                    foreach (int idx in arrow.indices)
                        if (idx >= 0 && idx < occupied.Length)
                            occupied[idx] = true;
                }
            }

            return occupied;
        }

        static List<int> CollectV13InteriorRefillStarts(int width, int height, bool[] canSpawn, bool[] occupied, int salt)
        {
            var result = new List<int>();
            for (int y = 2; y < height - 2; y++)
            {
                for (int x = 2; x < width - 2; x++)
                {
                    int idx = x + y * width;
                    if (idx < 0 || idx >= canSpawn.Length || idx >= occupied.Length || !canSpawn[idx] || occupied[idx])
                        continue;
                    result.Add(idx);
                }
            }

            result.Sort((a, b) => PositiveMod(a * 31 + salt, 997).CompareTo(PositiveMod(b * 31 + salt, 997)));
            if (result.Count > 80)
                result.RemoveRange(80, result.Count - 80);
            return result;
        }

        static bool TryBuildV13InteriorRefillPath(int start, int width, int height, bool[] canSpawn, bool[] occupied, int salt, int shape, out List<int> path)
        {
            path = null;
            var origin = IndexToPos(start, width);
            var dirs = new[]
            {
                Vector2Int.right,
                Vector2Int.up,
                Vector2Int.left,
                Vector2Int.down
            };
            int first = PositiveMod(salt + start, dirs.Length);
            for (int attempt = 0; attempt < dirs.Length; attempt++)
            {
                var a = dirs[(first + attempt) % dirs.Length];
                var b = dirs[(first + attempt + 1 + shape) % dirs.Length];
                var c = dirs[(first + attempt + 2) % dirs.Length];
                var candidate = shape switch
                {
                    0 => new List<Vector2Int> { origin, origin + a, origin + a + b },
                    1 => new List<Vector2Int> { origin, origin + a, origin + a * 2 },
                    2 => new List<Vector2Int> { origin, origin + a, origin + a + b, origin + a + b + c },
                    _ => new List<Vector2Int> { origin, origin + a, origin + a + b }
                };

                if (TryConvertV13RefillPath(candidate, width, height, canSpawn, occupied, out path))
                    return true;
            }

            return false;
        }

        static bool TryConvertV13RefillPath(List<Vector2Int> points, int width, int height, bool[] canSpawn, bool[] occupied, out List<int> path)
        {
            path = null;
            if (points == null || points.Count < 2)
                return false;

            var indices = new List<int>(points.Count);
            var seen = new HashSet<int>();
            foreach (var p in points)
            {
                if (!InBounds(width, height, p.x, p.y))
                    return false;
                int idx = p.x + p.y * width;
                if (idx < 0 || idx >= canSpawn.Length || idx >= occupied.Length || !canSpawn[idx] || occupied[idx] || !seen.Add(idx))
                    return false;
                indices.Add(idx);
            }

            path = indices;
            return true;
        }

        static bool TryMergeV13SameDirectionShortExits(AuthoredLevelData data, int width, int height, bool[] canSpawn, int firstArrowIndex, int secondArrowIndex)
        {
            if (data?.arrows == null || canSpawn == null || firstArrowIndex == secondArrowIndex)
                return false;
            if (firstArrowIndex < 0 || firstArrowIndex >= data.arrows.Count || secondArrowIndex < 0 || secondArrowIndex >= data.arrows.Count)
                return false;

            var first = data.arrows[firstArrowIndex]?.indices;
            var second = data.arrows[secondArrowIndex]?.indices;
            if (!TryGetV13MergeableShortOuterChain(first, width, height, out var firstForward) ||
                !TryGetV13MergeableShortOuterChain(second, width, height, out var secondForward) ||
                firstForward != secondForward)
            {
                return false;
            }

            var occupied = BuildV13OccupiedForMerge(data, width, height, firstArrowIndex, secondArrowIndex);
            int firstTail = first[first.Count - 1];
            int secondTail = second[second.Count - 1];
            if (!TryBuildV13MergeBridge(firstTail, secondTail, width, height, canSpawn, occupied, out var bridge))
                return false;

            var merged = new List<int>(first.Count + bridge.Count + second.Count);
            merged.AddRange(first);
            merged.AddRange(bridge);
            for (int i = second.Count - 1; i >= 0; i--)
                merged.Add(second[i]);

            if (!IsContiguousPath(merged, width) || HasDuplicateV13Index(merged))
                return false;

            int removeA = Mathf.Max(firstArrowIndex, secondArrowIndex);
            int removeB = Mathf.Min(firstArrowIndex, secondArrowIndex);
            data.arrows.RemoveAt(removeA);
            data.arrows.RemoveAt(removeB);
            data.arrows.Add(new AuthoredArrowData
            {
                indices = merged,
                colorIndex = data.arrows.Count
            });
            return true;
        }

        static bool TryGetV13MergeableShortOuterChain(List<int> indices, int width, int height, out Vector2Int forward)
        {
            forward = Vector2Int.zero;
            if (indices == null || indices.Count < 2 || indices.Count > 6)
                return false;
            int turns = CountAuthoredChainTurns(indices, width);
            if (turns != 0)
                return false;

            var head = IndexToPos(indices[0], width);
            var second = IndexToPos(indices[1], width);
            forward = head - second;
            if (Mathf.Abs(forward.x) + Mathf.Abs(forward.y) != 1)
                return false;

            var next = head + forward;
            return !InBounds(width, height, next.x, next.y);
        }

        static bool[] BuildV13OccupiedForMerge(AuthoredLevelData data, int width, int height, int firstArrowIndex, int secondArrowIndex)
        {
            var occupied = new bool[width * height];
            var blockIndices = GetAuthoredBlockIndices(data);
            if (blockIndices != null)
            {
                foreach (int idx in blockIndices)
                    if (idx >= 0 && idx < occupied.Length)
                        occupied[idx] = true;
            }

            for (int i = 0; i < data.arrows.Count; i++)
            {
                if (i == firstArrowIndex || i == secondArrowIndex)
                    continue;
                var indices = data.arrows[i]?.indices;
                if (indices == null)
                    continue;
                foreach (int idx in indices)
                    if (idx >= 0 && idx < occupied.Length)
                        occupied[idx] = true;
            }

            return occupied;
        }

        static bool TryBuildV13MergeBridge(int fromIndex, int toIndex, int width, int height, bool[] canSpawn, bool[] occupied, out List<int> bridge)
        {
            bridge = new List<int>();
            var from = IndexToPos(fromIndex, width);
            var to = IndexToPos(toIndex, width);
            int dx = to.x - from.x;
            int dy = to.y - from.y;
            if (Mathf.Abs(dx) + Mathf.Abs(dy) > 3)
                return false;
            if (dx != 0 && dy != 0)
                return false;

            int steps = Mathf.Abs(dx) + Mathf.Abs(dy);
            if (steps == 0)
                return false;

            var step = new Vector2Int(Math.Sign(dx), Math.Sign(dy));
            for (int i = 1; i < steps; i++)
            {
                var p = from + step * i;
                if (!InBounds(width, height, p.x, p.y))
                    return false;
                int idx = p.x + p.y * width;
                if (idx < 0 || idx >= canSpawn.Length || idx >= occupied.Length || !canSpawn[idx] || occupied[idx])
                    return false;
                bridge.Add(idx);
            }

            return true;
        }

        static bool HasDuplicateV13Index(List<int> indices)
        {
            var seen = new HashSet<int>();
            foreach (int idx in indices)
                if (!seen.Add(idx))
                    return true;
            return false;
        }

        static bool TryWrapSingleV13OuterExit(AuthoredLevelData data, int width, int height, bool[] canSpawn, int arrowIndex)
        {
            if (data?.arrows == null || arrowIndex < 0 || arrowIndex >= data.arrows.Count)
                return false;

            var indices = data.arrows[arrowIndex]?.indices;
            if (indices == null || indices.Count < 2 || indices.Count > 6)
                return false;

            var occupied = new bool[width * height];
            var blockIndices = GetAuthoredBlockIndices(data);
            if (blockIndices != null)
            {
                foreach (int idx in blockIndices)
                    if (idx >= 0 && idx < occupied.Length)
                        occupied[idx] = true;
            }

            foreach (var arrow in data.arrows)
            {
                if (arrow?.indices == null)
                    continue;
                foreach (int idx in arrow.indices)
                    if (idx >= 0 && idx < occupied.Length)
                        occupied[idx] = true;
            }

            int headIndex = indices[0];
            int secondIndex = indices[1];
            if (headIndex < 0 || headIndex >= occupied.Length || secondIndex < 0 || secondIndex >= occupied.Length)
                return false;

            var head = IndexToPos(headIndex, width);
            var second = IndexToPos(secondIndex, width);
            var forward = head - second;
            if (Mathf.Abs(forward.x) + Mathf.Abs(forward.y) != 1)
                return false;

            var directNext = head + forward;
            if (InBounds(width, height, directNext.x, directNext.y))
                return false;

            if (!TryPickV13WrappedHeadCell(width, height, canSpawn, occupied, head, forward, arrowIndex * 31 + indices.Count, out int newHeadIndex))
                return false;

            indices.Insert(0, newHeadIndex);
            return true;
        }

        static bool TryWrapV13ShortOuterExits(AuthoredLevelData source, int width, int height, bool[] canSpawn, int variant, out AuthoredLevelData wrappedAuthored, out BoardState wrappedRoundTripBoard)
        {
            wrappedAuthored = null;
            wrappedRoundTripBoard = null;
            if (source?.arrows == null || canSpawn == null || width <= 0 || height <= 0)
                return false;

            var occupied = new bool[width * height];
            var sourceBlockIndices = GetAuthoredBlockIndices(source);
            if (sourceBlockIndices != null)
            {
                foreach (int idx in sourceBlockIndices)
                    if (idx >= 0 && idx < occupied.Length)
                        occupied[idx] = true;
            }

            foreach (var arrow in source.arrows)
            {
                if (arrow?.indices == null)
                    continue;
                foreach (int idx in arrow.indices)
                    if (idx >= 0 && idx < occupied.Length)
                        occupied[idx] = true;
            }

            var data = CloneAuthoredLevel(source);
            int wrapped = 0;
            int maxWraps = 10 + PositiveMod(variant, 5);
            for (int i = 0; i < data.arrows.Count && wrapped < maxWraps; i++)
            {
                var indices = data.arrows[i]?.indices;
                if (indices == null || indices.Count < 2 || indices.Count > 5)
                    continue;

                int headIndex = indices[0];
                int secondIndex = indices[1];
                if (headIndex < 0 || headIndex >= occupied.Length || secondIndex < 0 || secondIndex >= occupied.Length)
                    continue;

                var head = IndexToPos(headIndex, width);
                var second = IndexToPos(secondIndex, width);
                var forward = head - second;
                if (Mathf.Abs(forward.x) + Mathf.Abs(forward.y) != 1)
                    continue;

                var directNext = head + forward;
                if (InBounds(width, height, directNext.x, directNext.y))
                    continue;

                if (!TryPickV13WrappedHeadCell(width, height, canSpawn, occupied, head, forward, variant + i * 17, out int newHeadIndex))
                    continue;

                indices.Insert(0, newHeadIndex);
                occupied[newHeadIndex] = true;
                wrapped++;
            }

            if (wrapped == 0)
                return false;

            if (!AuthoredLevelBuilder.TryBuildBoard(data, out var board, out _))
                return false;

            return TryRoundTripAuthoredBoard(board, out wrappedAuthored, out wrappedRoundTripBoard);
        }

        static bool TryPickV13WrappedHeadCell(int width, int height, bool[] canSpawn, bool[] occupied, Vector2Int head, Vector2Int oldForward, int salt, out int newHeadIndex)
        {
            newHeadIndex = -1;
            var options = oldForward.x != 0
                ? new[] { Vector2Int.up, Vector2Int.down }
                : new[] { Vector2Int.right, Vector2Int.left };

            int first = PositiveMod(salt, options.Length);
            for (int pass = 0; pass < options.Length; pass++)
            {
                var turn = options[(first + pass) % options.Length];
                var newHead = head + turn;
                if (!InBounds(width, height, newHead.x, newHead.y))
                    continue;

                int idx = newHead.x + newHead.y * width;
                if (idx < 0 || idx >= canSpawn.Length || idx >= occupied.Length || !canSpawn[idx] || occupied[idx])
                    continue;

                var projected = newHead + turn;
                if (!InBounds(width, height, projected.x, projected.y))
                    continue;

                int projectedIdx = projected.x + projected.y * width;
                if (projectedIdx < 0 || projectedIdx >= canSpawn.Length || !canSpawn[projectedIdx])
                    continue;

                if (projectedIdx < occupied.Length && occupied[projectedIdx])
                    continue;

                newHeadIndex = idx;
                return true;
            }

            return false;
        }

        static HoleCheapStats CalculateHoleCheapStats(BoardState board, IRuleset ruleset)
        {
            if (board == null || board.tiles == null)
                return new HoleCheapStats(0, 0, 0, 0);

            int arrowTileCount = 0;
            int initialMovable = 0;
            int maxChainLength = 0;
            var chains = CollectChains(board);
            foreach (var chain in chains)
            {
                arrowTileCount += chain.Count;
                maxChainLength = Mathf.Max(maxChainLength, chain.Count);
                if (CanClearAnyTileInChain(board, ruleset, chain))
                    initialMovable++;
            }

            return new HoleCheapStats(chains.Count, arrowTileCount, initialMovable, maxChainLength);
        }

        static bool CanClearAnyTileInChain(BoardState board, IRuleset ruleset, List<int> chain)
        {
            if (board == null || ruleset == null || chain == null)
                return false;

            foreach (int idx in chain)
            {
                var move = new Move(new Vector2Int(idx % board.width, idx / board.width));
                if (!ruleset.TryApplyMove(board, move, out var delta))
                    continue;

                delta.Undo(board);
                return true;
            }

            return false;
        }

        static float OpeningControlScore(HoleCheapStats stats, (int outerExits, int blockedByArrow, int blockedByHole) quality)
        {
            float ratio = stats.ChainCount > 0 ? stats.InitialMovableArrowChainCount / (float)stats.ChainCount : 1f;
            return ratio * 1000f
                   + stats.InitialMovableArrowChainCount * 20f
                   + quality.outerExits * 8f
                   + quality.blockedByHole * 0.5f
                   - quality.blockedByArrow * 1.5f;
        }

        static List<List<int>> CollectChains(BoardState board)
        {
            var result = new List<List<int>>();
            var visited = new HashSet<int>();
            var chain = new HashSet<int>();
            for (int i = 0; i < board.tiles.Length; i++)
            {
                if (visited.Contains(i) || board.tiles[i].type != TileType.Arrow)
                    continue;

                ArrowChainUtility.CollectFullChain(board, new Vector2Int(i % board.width, i / board.width), 0, chain);
                var list = new List<int>(chain.Count);
                foreach (int idx in chain)
                {
                    visited.Add(idx);
                    list.Add(idx);
                }

                if (list.Count > 0)
                    result.Add(list);
            }

            return result;
        }

        static void ReverseChain(BoardState board, List<int> chain)
        {
            foreach (int idx in chain)
            {
                var tile = board.tiles[idx];
                if (tile.type != TileType.Arrow)
                    continue;

                board.tiles[idx] = TileState.Arrow(tile.arrow.outDir, tile.arrow.inDir);
            }
        }

        static (int outerExits, int blockedByArrow, int blockedByHole) AnalyzeOpeningQuality(BoardState board, IRuleset ruleset)
        {
            int outerExits = 0;
            int blockedByArrow = 0;
            int blockedByHole = 0;
            var chains = CollectChains(board);
            foreach (var chain in chains)
            {
                bool chainExits = false;
                bool chainBlockedByArrow = false;
                bool chainBlockedByHole = false;
                foreach (int idx in chain)
                {
                    var reason = TraceFirstStop(board, new Vector2Int(idx % board.width, idx / board.width));
                    if (reason == StopReason.Exit) chainExits = true;
                    else if (reason == StopReason.Arrow) chainBlockedByArrow = true;
                    else if (reason == StopReason.Block) chainBlockedByHole = true;
                }

                if (chainExits) outerExits++;
                if (chainBlockedByArrow) blockedByArrow++;
                if (chainBlockedByHole) blockedByHole++;
            }

            return (outerExits, blockedByArrow, blockedByHole);
        }

        enum StopReason { Other, Exit, Arrow, Block }

        static StopReason TraceFirstStop(BoardState board, Vector2Int start)
        {
            if (!board.InBounds(start.x, start.y))
                return StopReason.Other;

            int startIdx = board.Index(start.x, start.y);
            var startTile = board.tiles[startIdx];
            if (startTile.type != TileType.Arrow)
                return StopReason.Other;

            Vector2Int pos = start;
            Dir entryDir = startTile.arrow.inDir;
            Dir travelDir = default;
            bool hasTravelDir = false;
            var seen = new HashSet<int>();
            int maxSteps = 1 + board.width * board.height * 4;

            for (int step = 0; step < maxSteps; step++)
            {
                if (!board.InBounds(pos.x, pos.y))
                    return StopReason.Exit;

                int idx = board.Index(pos.x, pos.y);
                int key = (idx << 2) | ((int)entryDir & 3);
                if (!seen.Add(key))
                    return StopReason.Other;

                var tile = board.tiles[idx];
                if (tile.type == TileType.Block)
                    return StopReason.Block;

                if (tile.type == TileType.Empty)
                {
                    if (!hasTravelDir)
                        return StopReason.Other;

                    Vector2Int next = pos + DirOffset(travelDir);
                    if (board.InBounds(next.x, next.y) && board.tiles[board.Index(next.x, next.y)].type == TileType.Arrow)
                        return StopReason.Arrow;

                    pos = next;
                    entryDir = OppositeDir(travelDir);
                    continue;
                }

                if (tile.type != TileType.Arrow || tile.arrow.inDir != entryDir)
                    return tile.type == TileType.Arrow ? StopReason.Arrow : StopReason.Other;

                travelDir = tile.arrow.outDir;
                hasTravelDir = true;
                pos += DirOffset(travelDir);
                entryDir = OppositeDir(travelDir);
            }

            return StopReason.Other;
        }

        static Vector2Int DirOffset(Dir dir) => dir switch
        {
            Dir.Up => Vector2Int.up,
            Dir.Right => Vector2Int.right,
            Dir.Down => Vector2Int.down,
            _ => Vector2Int.left
        };

        static Dir OppositeDir(Dir dir) => (Dir)(((int)dir + 2) & 3);

        static int CountAllowedCells(bool[] canSpawn)
        {
            if (canSpawn == null)
                return 0;

            int count = 0;
            for (int i = 0; i < canSpawn.Length; i++)
                if (canSpawn[i])
                    count++;

            return count;
        }

        static int FillAllowedEmptiesOutward(BoardState board, bool[] canSpawn)
        {
            if (board == null || canSpawn == null)
                return 0;

            int filled = 0;
            int limit = Mathf.Min(board.tiles.Length, canSpawn.Length);
            for (int i = 0; i < limit; i++)
            {
                if (!canSpawn[i] || board.tiles[i].type != TileType.Empty)
                    continue;

                var pos = new Vector2Int(i % board.width, i / board.width);
                Dir outDir = OutwardDirTowardNearestEdge(board, pos);
                board.tiles[i] = TileState.Arrow(OppositeDir(outDir), outDir);
                filled++;
            }

            return filled;
        }

        static int RepairSingleCellChains(BoardState board, bool[] canSpawn, int maxPasses)
        {
            if (board == null)
                return 0;

            int repairs = 0;
            for (int pass = 0; pass < maxPasses; pass++)
            {
                var chains = CollectChains(board);
                var singles = new List<int>();
                foreach (var chain in chains)
                    if (chain.Count == 1)
                        singles.Add(chain[0]);

                if (singles.Count == 0)
                    break;

                bool changed = false;
                foreach (int idx in singles)
                {
                    if (idx < 0 || idx >= board.tiles.Length || board.tiles[idx].type != TileType.Arrow)
                        continue;

                    if (!TryAttachSingleCellChain(board, canSpawn, idx))
                        continue;

                    repairs++;
                    changed = true;
                }

                if (!changed)
                    break;
            }

            return repairs;
        }

        static bool TryAttachSingleCellChain(BoardState board, bool[] canSpawn, int idx)
        {
            var pos = new Vector2Int(idx % board.width, idx / board.width);
            Dir preferred = OutwardDirTowardNearestEdge(board, pos);
            var dirs = new[] { preferred, Dir.Right, Dir.Up, Dir.Left, Dir.Down };

            foreach (Dir dir in dirs)
            {
                Vector2Int nextPos = pos + DirOffset(dir);
                if (!board.InBounds(nextPos.x, nextPos.y))
                    continue;

                int nextIdx = board.Index(nextPos.x, nextPos.y);
                if (canSpawn != null && (nextIdx >= canSpawn.Length || !canSpawn[nextIdx]))
                    continue;

                var nextTile = board.tiles[nextIdx];
                if (nextTile.type != TileType.Arrow)
                    continue;

                Dir back = OppositeDir(dir);
                if (nextTile.arrow.outDir == back)
                    nextTile = TileState.Arrow(back, OutwardDirAvoiding(board, nextPos, back));
                else
                    nextTile = TileState.Arrow(back, nextTile.arrow.outDir);

                board.tiles[idx] = TileState.Arrow(back, dir);
                board.tiles[nextIdx] = nextTile;
                return true;
            }

            return false;
        }

        static Dir OutwardDirAvoiding(BoardState board, Vector2Int pos, Dir avoid)
        {
            Dir best = OutwardDirTowardNearestEdge(board, pos);
            if (best != avoid)
                return best;

            var candidates = new[] { Dir.Left, Dir.Right, Dir.Down, Dir.Up };
            foreach (Dir dir in candidates)
            {
                if (dir == avoid)
                    continue;

                Vector2Int next = pos + DirOffset(dir);
                if (!board.InBounds(next.x, next.y))
                    return dir;
            }

            return OppositeDir(avoid);
        }

        static int CountSingleCellChains(BoardState board)
        {
            int singles = 0;
            var chains = CollectChains(board);
            foreach (var chain in chains)
                if (chain.Count == 1)
                    singles++;

            return singles;
        }

        static bool TryRoundTripAuthoredBoard(BoardState board, out BoardState authoredBoard)
        {
            return TryRoundTripAuthoredBoard(board, out _, out authoredBoard);
        }

        static bool TryRoundTripAuthoredBoard(BoardState board, out AuthoredLevelData authored, out BoardState authoredBoard)
        {
            authored = null;
            authoredBoard = null;
            if (!TryConvertBoardToAuthoredLevel(board, out authored, out _))
                return false;

            return AuthoredLevelBuilder.TryBuildBoard(authored, out authoredBoard, out _);
        }

        static bool TryRepairShapeViolations(BoardState board, int maxClears, out int repairs, out int clears)
        {
            repairs = 0;
            clears = 0;
            if (board == null || board.tiles == null)
                return true;

            int maxPasses = Mathf.Max(1, board.tiles.Length * 2);
            for (int pass = 0; pass < maxPasses; pass++)
            {
                bool changed = false;
                for (int idx = 0; idx < board.tiles.Length; idx++)
                {
                    var tile = board.tiles[idx];
                    if (tile.type != TileType.Arrow)
                        continue;

                    var pos = new Vector2Int(idx % board.width, idx / board.width);
                    var next = pos + DirOffset(tile.arrow.outDir);
                    if (!board.InBounds(next.x, next.y))
                        continue;

                    int nextIdx = board.Index(next.x, next.y);
                    var nextTile = board.tiles[nextIdx];
                    bool directBlock = nextTile.type == TileType.Block;
                    bool badArrowContact = nextTile.type == TileType.Arrow &&
                                           nextTile.arrow.inDir != OppositeDir(tile.arrow.outDir);
                    if (!directBlock && !badArrowContact)
                        continue;

                    if (!TryPickSafeOutDir(board, pos, tile.arrow.inDir, tile.arrow.outDir, out Dir safeOutDir))
                    {
                        if (clears >= maxClears ||
                            !TryPickSafeOutDirByClearing(board, pos, tile.arrow.inDir, tile.arrow.outDir, out safeOutDir, out int clearIndex))
                        {
                            return false;
                        }

                        board.tiles[clearIndex] = TileState.Empty();
                        clears++;
                    }

                    board.tiles[idx] = TileState.Arrow(tile.arrow.inDir, safeOutDir);
                    repairs++;
                    changed = true;
                }

                if (!changed)
                    return true;
            }

            return false;
        }

        static bool TryPickSafeOutDir(BoardState board, Vector2Int pos, Dir inDir, Dir currentOutDir, out Dir safeOutDir)
        {
            safeOutDir = currentOutDir;
            int bestScore = int.MaxValue;

            for (int i = 0; i < 4; i++)
            {
                Dir dir = (Dir)i;
                if (!IsSafeImmediateOutDir(board, pos, dir, out int targetScore))
                    continue;

                int score = targetScore;
                if (dir == currentOutDir)
                    score -= 2;
                if (dir == inDir)
                    score += 12;

                if (score >= bestScore)
                    continue;

                bestScore = score;
                safeOutDir = dir;
            }

            return bestScore != int.MaxValue;
        }

        static bool IsSafeImmediateOutDir(BoardState board, Vector2Int pos, Dir outDir, out int score)
        {
            var next = pos + DirOffset(outDir);
            if (!board.InBounds(next.x, next.y))
            {
                score = 30;
                return true;
            }

            var nextTile = board.tiles[board.Index(next.x, next.y)];
            if (nextTile.type == TileType.Block)
            {
                score = int.MaxValue;
                return false;
            }

            if (nextTile.type == TileType.Arrow)
            {
                bool connects = nextTile.arrow.inDir == OppositeDir(outDir);
                score = connects ? 0 : int.MaxValue;
                return connects;
            }

            score = 10;
            return true;
        }

        static bool TryPickSafeOutDirByClearing(BoardState board, Vector2Int pos, Dir inDir, Dir currentOutDir, out Dir safeOutDir, out int clearIndex)
        {
            safeOutDir = currentOutDir;
            clearIndex = -1;
            int bestScore = int.MaxValue;

            for (int i = 0; i < 4; i++)
            {
                Dir dir = (Dir)i;
                var next = pos + DirOffset(dir);
                if (!board.InBounds(next.x, next.y))
                    continue;

                int nextIndex = board.Index(next.x, next.y);
                var nextTile = board.tiles[nextIndex];
                if (nextTile.type != TileType.Arrow)
                    continue;

                int score = 0;
                if (dir == currentOutDir)
                    score -= 2;
                if (dir == inDir)
                    score += 12;

                if (score >= bestScore)
                    continue;

                bestScore = score;
                safeOutDir = dir;
                clearIndex = nextIndex;
            }

            return clearIndex >= 0;
        }

        static void CountShapeViolations(BoardState board, out int headToHeadConflicts, out int directBlockAims, out int badArrowContacts)
        {
            headToHeadConflicts = 0;
            directBlockAims = 0;
            badArrowContacts = 0;
            if (board == null || board.tiles == null)
                return;

            for (int idx = 0; idx < board.tiles.Length; idx++)
            {
                var tile = board.tiles[idx];
                if (tile.type != TileType.Arrow)
                    continue;

                var pos = new Vector2Int(idx % board.width, idx / board.width);
                var next = pos + DirOffset(tile.arrow.outDir);
                if (!board.InBounds(next.x, next.y))
                    continue;

                int nextIdx = board.Index(next.x, next.y);
                var nextTile = board.tiles[nextIdx];
                if (nextTile.type == TileType.Block)
                {
                    directBlockAims++;
                    continue;
                }

                if (nextTile.type == TileType.Arrow &&
                    nextTile.arrow.inDir != OppositeDir(tile.arrow.outDir))
                {
                    badArrowContacts++;
                    if (nextTile.arrow.outDir == OppositeDir(tile.arrow.outDir) && idx < nextIdx)
                        headToHeadConflicts++;
                }
            }
        }

        static Dir OutwardDirTowardNearestEdge(BoardState board, Vector2Int pos)
        {
            int left = pos.x;
            int right = (board.width - 1) - pos.x;
            int down = pos.y;
            int up = (board.height - 1) - pos.y;

            int min = left;
            Dir dir = Dir.Left;
            if (right < min)
            {
                min = right;
                dir = Dir.Right;
            }

            if (down < min)
            {
                min = down;
                dir = Dir.Down;
            }

            if (up < min)
                dir = Dir.Up;

            return dir;
        }

        static void ApplyMaskBlocks(BoardState board, bool[] canSpawn)
        {
            if (board == null || canSpawn == null)
                return;

            int limit = Mathf.Min(board.tiles.Length, canSpawn.Length);
            for (int i = 0; i < limit; i++)
                if (!canSpawn[i])
                    board.tiles[i] = TileState.Block();
        }

        static bool TryLoadHoleMask(out Texture2D texture, out string source)
        {
            string projectFullPath = ToFullPath(HoleMaskProjectPath);
            if (TryLoadTextureFromFile(projectFullPath, out texture))
            {
                source = HoleMaskProjectPath;
                return true;
            }

            if (TryLoadTextureFromFile(HoleMaskExternalPath, out texture))
            {
                source = HoleMaskExternalPath;
                return true;
            }

            source = "";
            return false;
        }

        static bool TryLoadTextureFromFile(string fullPath, out Texture2D texture)
        {
            texture = null;
            if (string.IsNullOrWhiteSpace(fullPath) || !File.Exists(fullPath))
                return false;

            var bytes = File.ReadAllBytes(fullPath);
            texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            if (!texture.LoadImage(bytes))
            {
                UnityEngine.Object.DestroyImmediate(texture);
                texture = null;
                return false;
            }

            texture.name = Path.GetFileNameWithoutExtension(fullPath);
            return true;
        }

        readonly struct LowOpeningProbeProfile
        {
            public readonly string Name;
            public readonly int TargetInitial;
            public readonly int TargetDifficulty;
            public readonly int MinPath;
            public readonly int MaxPath;
            public readonly float Twistiness;

            public LowOpeningProbeProfile(string name, int targetInitial, int targetDifficulty, int maxPath, float twistiness)
                : this(name, targetInitial, targetDifficulty, 2, maxPath, twistiness)
            {
            }

            public LowOpeningProbeProfile(string name, int targetInitial, int targetDifficulty, int minPath, int maxPath, float twistiness)
            {
                Name = name;
                TargetInitial = targetInitial;
                TargetDifficulty = targetDifficulty;
                MinPath = minPath;
                MaxPath = maxPath;
                Twistiness = twistiness;
            }
        }

        struct LowOpeningProbeCandidate
        {
            public BoardState Board;
            public BoardGenerationTuning.BoardGenerationStats Stats;
            public int Seed;
            public float OpeningRatio;
            public float Score;
            public string ProfileName;
            public int TargetInitial;
            public int TargetDifficulty;
            public int MinPath;
            public int MaxPath;
            public float Twistiness;
        }

        static LowOpeningProbeProfile[] BuildLowOpeningProbeProfiles(Texture2D mask, bool[] canSpawn)
        {
            int maxPathA = mask.width <= 16 ? 10 : 12;
            int maxPathB = mask.width <= 16 ? 12 : 16;
            int lowTarget = mask.width <= 16 ? 6 : 8;
            int midTarget = mask.width <= 16 ? 8 : 10;
            if (Path.GetFileName(mask.name).IndexOf("Star", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                lowTarget = 10;
                midTarget = 12;
            }

            var hard = BoardGenerationTuning.RecommendDifficultyScore(
                mask.width,
                mask.height,
                0.86f,
                2,
                maxPathA,
                canSpawn);

            return new[]
            {
                new LowOpeningProbeProfile("initial_low_base", lowTarget, 0, maxPathA, 0.55f),
                new LowOpeningProbeProfile("initial_low_long", lowTarget, 0, maxPathB, 0.45f),
                new LowOpeningProbeProfile("initial_mid_twisty", midTarget, 0, maxPathA, 0.68f),
                new LowOpeningProbeProfile("hard_score", 0, hard.Max, maxPathA, 0.55f),
            };
        }

        static void ProbeLowOpeningPicker(
            Texture2D mask,
            bool[] canSpawn,
            LowOpeningProbeProfile profile,
            int attempts,
            out int greedy,
            out int accepted26,
            out int accepted22,
            out LowOpeningProbeCandidate best)
        {
            var generator = new ClearAllArrowsGenerator();
            var ruleset = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int baseSeed = 93000 + Math.Abs(mask.name.GetHashCode() % 10000) + profile.MaxPath * 37 + Mathf.RoundToInt(profile.Twistiness * 100f);
            greedy = 0;
            accepted26 = 0;
            accepted22 = 0;
            best = default;
            best.Score = float.MaxValue;
            best.OpeningRatio = 0f;

            for (int attempt = 0; attempt < attempts; attempt++)
            {
                int seed = BoardGenerationTuning.BuildCandidateSeed(baseSeed, attempt);
                var board = generator.Generate(new LevelSpec
                {
                    width = mask.width,
                    height = mask.height,
                    seed = seed,
                    arrowFill = 0.86f,
                    minPathLen = profile.MinPath,
                    maxPathLen = profile.MaxPath,
                    twistiness = profile.Twistiness,
                    canSpawnHere = canSpawn
                });

                if (!GreedyValidator.TryClearAllByGreedy(board, ruleset, 300, out _))
                    continue;

                greedy++;
                var stats = BoardGenerationTuning.CalculateBoardGenerationStats(board, ruleset);
                if (stats.ChainCount <= 0)
                    continue;

                float ratio = stats.InitialMovableArrowChainCount / (float)stats.ChainCount;
                if (stats.InitialMovableArrowChainCount >= 2 && ratio <= 0.26f)
                    accepted26++;
                if (stats.InitialMovableArrowChainCount >= 2 && ratio <= 0.22f)
                    accepted22++;

                float score = ScoreLowOpeningCandidate(stats, ratio, profile, attempt);
                if (score >= best.Score)
                    continue;

                best = new LowOpeningProbeCandidate
                {
                    Board = board,
                    Stats = stats,
                    Seed = seed,
                    OpeningRatio = ratio,
                    Score = score,
                    ProfileName = profile.Name,
                    TargetInitial = profile.TargetInitial,
                    TargetDifficulty = profile.TargetDifficulty,
                    MinPath = profile.MinPath,
                    MaxPath = profile.MaxPath,
                    Twistiness = profile.Twistiness
                };
            }
        }

        static void ProbeTwistyStructurePicker(
            Texture2D mask,
            bool[] canSpawn,
            LowOpeningProbeProfile profile,
            int attempts,
            out int greedy,
            out int accepted30,
            out int accepted28,
            out LowOpeningProbeCandidate best)
        {
            var generator = new ClearAllArrowsGenerator();
            var ruleset = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            int baseSeed = 121000 + profile.MinPath * 1009 + profile.MaxPath * 101 + Mathf.RoundToInt(profile.Twistiness * 1000f);
            greedy = 0;
            accepted30 = 0;
            accepted28 = 0;
            best = default;
            best.Score = float.MaxValue;

            for (int attempt = 0; attempt < attempts; attempt++)
            {
                int seed = BoardGenerationTuning.BuildCandidateSeed(baseSeed, attempt);
                var board = generator.Generate(new LevelSpec
                {
                    width = mask.width,
                    height = mask.height,
                    seed = seed,
                    arrowFill = 0.86f,
                    minPathLen = profile.MinPath,
                    maxPathLen = profile.MaxPath,
                    twistiness = profile.Twistiness,
                    canSpawnHere = canSpawn
                });

                if (!GreedyValidator.TryClearAllByGreedy(board, ruleset, 300, out _))
                    continue;

                greedy++;
                var stats = BoardGenerationTuning.CalculateBoardGenerationStats(board, ruleset);
                if (stats.ChainCount <= 0)
                    continue;

                float ratio = stats.InitialMovableArrowChainCount / (float)stats.ChainCount;
                if (stats.InitialMovableArrowChainCount >= 2 && ratio <= 0.30f)
                    accepted30++;
                if (stats.InitialMovableArrowChainCount >= 2 && ratio <= 0.28f)
                    accepted28++;

                float avgChainLen = stats.ArrowTileCount / (float)Mathf.Max(1, stats.ChainCount);
                float score =
                    Mathf.Abs(stats.InitialMovableArrowChainCount - profile.TargetInitial) * 70f
                    + ratio * 90f
                    - avgChainLen * 12f
                    - stats.MaxChainLength * 1.5f
                    + attempt * 0.001f;

                if (score >= best.Score)
                    continue;

                best = new LowOpeningProbeCandidate
                {
                    Board = board,
                    Stats = stats,
                    Seed = seed,
                    OpeningRatio = ratio,
                    Score = score,
                    ProfileName = profile.Name,
                    TargetInitial = profile.TargetInitial,
                    TargetDifficulty = profile.TargetDifficulty,
                    MinPath = profile.MinPath,
                    MaxPath = profile.MaxPath,
                    Twistiness = profile.Twistiness
                };
            }
        }

        static float ScoreLowOpeningCandidate(
            BoardGenerationTuning.BoardGenerationStats stats,
            float openingRatio,
            LowOpeningProbeProfile profile,
            int attempt)
        {
            if (profile.TargetDifficulty > 0)
                return Mathf.Abs(stats.DifficultyScore - profile.TargetDifficulty) * 10f
                       + openingRatio * 120f
                       + stats.InitialMovableArrowChainCount * 2f
                       - stats.ArrowTileCount * 0.01f
                       + attempt * 0.001f;

            int targetInitial = Mathf.Max(1, profile.TargetInitial);
            return Mathf.Abs(stats.InitialMovableArrowChainCount - targetInitial) * 100f
                   + openingRatio * 80f
                   - stats.ChainCount * 0.2f
                   - stats.ArrowTileCount * 0.01f
                   + attempt * 0.001f;
        }

        static bool IsBetterLowOpeningCandidate(LowOpeningProbeCandidate candidate, LowOpeningProbeCandidate current)
        {
            int ratioCompare = candidate.OpeningRatio.CompareTo(current.OpeningRatio);
            if (ratioCompare != 0)
                return ratioCompare < 0;

            int initialCompare = candidate.Stats.InitialMovableArrowChainCount.CompareTo(current.Stats.InitialMovableArrowChainCount);
            if (initialCompare != 0)
                return initialCompare < 0;

            return candidate.Stats.ArrowTileCount > current.Stats.ArrowTileCount;
        }

        static void ProbeOne(
            int width,
            int height,
            bool[] canSpawn,
            int baseSeed,
            int maxPath,
            float ratioGate,
            int countGate,
            out int attempts,
            out int greedy,
            out int accepted,
            out float bestRatio,
            out int bestInitial,
            out int bestChains,
            out int bestTiles,
            out int bestSeed)
        {
            var generator = new ClearAllArrowsGenerator();
            var ruleset = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            attempts = 60;
            greedy = 0;
            accepted = 0;
            bestRatio = 999f;
            bestInitial = 0;
            bestChains = 0;
            bestTiles = 0;
            bestSeed = 0;

            for (int attempt = 0; attempt < attempts; attempt++)
            {
                int seed = BoardGenerationTuning.BuildCandidateSeed(baseSeed, attempt);
                var board = generator.Generate(new LevelSpec
                {
                    width = width,
                    height = height,
                    seed = seed,
                    arrowFill = 0.86f,
                    minPathLen = 2,
                    maxPathLen = maxPath,
                    twistiness = 0.58f,
                    canSpawnHere = canSpawn
                });

                if (!GreedyValidator.TryClearAllByGreedy(board, ruleset, 300, out _))
                    continue;

                greedy++;
                var stats = BoardGenerationTuning.CalculateBoardGenerationStats(board, ruleset);
                if (stats.ChainCount <= 0)
                    continue;

                float ratio = stats.InitialMovableArrowChainCount / (float)stats.ChainCount;
                bool passes = stats.InitialMovableArrowChainCount >= 2 &&
                              stats.InitialMovableArrowChainCount <= countGate &&
                              ratio <= ratioGate;
                if (passes)
                    accepted++;

                if (ratio < bestRatio)
                {
                    bestRatio = ratio;
                    bestInitial = stats.InitialMovableArrowChainCount;
                    bestChains = stats.ChainCount;
                    bestTiles = stats.ArrowTileCount;
                    bestSeed = seed;
                }
            }

            if (bestRatio > 100f)
                bestRatio = 0f;
        }

        static bool TryBuildCandidate(Texture2D mask, int baseSeed, out BoardState best, out int bestSeed, out BoardGenerationTuning.BoardGenerationStats bestStats)
        {
            best = null;
            bestSeed = 0;
            bestStats = default;

            var generator = new ClearAllArrowsGenerator();
            var ruleset = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            bool[] canSpawn = BuildCanSpawn(mask);
            int allowed = canSpawn.Count(v => v);
            int targetInitial = Mathf.Clamp(Mathf.RoundToInt(allowed / 65f), 2, 6);
            float bestScore = float.MaxValue;

            for (int attempt = 0; attempt < BoardController.InitialMovableArrowSearchAttempts; attempt++)
            {
                int seed = BoardGenerationTuning.BuildCandidateSeed(baseSeed, attempt);
                var spec = new LevelSpec
                {
                    width = mask.width,
                    height = mask.height,
                    seed = seed,
                    arrowFill = 0.86f,
                    minPathLen = 2,
                    maxPathLen = Mathf.Clamp(Mathf.RoundToInt(Mathf.Sqrt(Mathf.Max(1, allowed)) * 2.2f), 8, 18),
                    twistiness = 0.55f,
                    canSpawnHere = canSpawn
                };

                var board = generator.Generate(spec);
                if (!GreedyValidator.TryClearAllByGreedy(board, ruleset, 300, out _))
                    continue;

                var stats = BoardGenerationTuning.CalculateBoardGenerationStats(board, ruleset);
                if (stats.ChainCount <= 0)
                    continue;

                float initialDelta = Mathf.Abs(stats.InitialMovableArrowChainCount - targetInitial);
                float openingRatio = stats.InitialMovableArrowChainCount / (float)Mathf.Max(1, stats.ChainCount);
                float score = initialDelta * 100f + openingRatio * 30f - stats.ArrowTileCount * 0.01f + attempt * 0.001f;

                if (score >= bestScore)
                    continue;

                best = board;
                bestSeed = seed;
                bestStats = stats;
                bestScore = score;
            }

            return best != null;
        }

        static bool[] BuildCanSpawn(Texture2D mask)
        {
            var pixels = mask.GetPixels32();
            var result = new bool[mask.width * mask.height];
            for (int i = 0; i < result.Length; i++)
                result[i] = pixels[i].a > 127;
            return result;
        }

        static void SavePack(List<LevelDefinition> levels)
        {
            EnsureFolder(Path.GetDirectoryName(PackPath)?.Replace("\\", "/"));
            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(PackPath);
            if (pack == null)
            {
                pack = ScriptableObject.CreateInstance<LevelPack>();
                AssetDatabase.CreateAsset(pack, PackPath);
            }

            pack.packId = "mask_preview_pack";
            pack.displayName = "Mask Preview Pack";
            pack.levels = levels.ToArray();
            EditorUtility.SetDirty(pack);
            AssetDatabase.SaveAssets();
        }

        static LevelPack SavePackAt(List<LevelDefinition> levels, string packPath, string packId, string displayName)
        {
            EnsureFolder(Path.GetDirectoryName(packPath)?.Replace("\\", "/"));
            var pack = AssetDatabase.LoadAssetAtPath<LevelPack>(packPath);
            if (pack == null)
            {
                pack = ScriptableObject.CreateInstance<LevelPack>();
                AssetDatabase.CreateAsset(pack, packPath);
            }

            pack.packId = packId;
            pack.displayName = displayName;
            pack.levels = levels.ToArray();
            EditorUtility.SetDirty(pack);
            AssetDatabase.SaveAssets();
            return pack;
        }

        static void AttachPackToDemo(LevelPack pack)
        {
            if (pack == null)
                return;

            var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(
                DemoScenePath,
                UnityEditor.SceneManagement.OpenSceneMode.Single);
            var progression = UnityEngine.Object.FindFirstObjectByType<LevelProgression>();
            if (progression == null)
            {
                Debug.LogWarning($"[MaskPreviewPackBuilder] LevelProgression not found in {DemoScenePath}");
                return;
            }

            var so = new SerializedObject(progression);
            var activePack = so.FindProperty("activePack");
            if (activePack == null)
            {
                Debug.LogWarning("[MaskPreviewPackBuilder] LevelProgression.activePack serialized field not found.");
                return;
            }

            activePack.objectReferenceValue = pack;
            so.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(progression);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(scene);
            UnityEditor.SceneManagement.EditorSceneManager.SaveScene(scene);
        }

        static void RenderPreview(BoardState board, Texture2D mask, string assetPath)
        {
            const int cell = 24;
            const int margin = 12;
            int width = board.width * cell + margin * 2;
            int height = board.height * cell + margin * 2;
            var tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
            var bg = new Color32(246, 242, 235, 255);
            var disabled = new Color32(214, 210, 203, 255);
            var grid = new Color32(194, 189, 180, 255);
            var black = new Color32(36, 36, 34, 255);

            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                tex.SetPixel(x, y, bg);

            var pixels = mask.GetPixels32();
            for (int y = 0; y < board.height; y++)
            for (int x = 0; x < board.width; x++)
            {
                int boardIndex = x + y * board.width;
                bool allowed = pixels[boardIndex].a > 127;
                var fill = allowed ? new Color32(236, 230, 219, 255) : disabled;
                FillRect(tex, margin + x * cell, margin + y * cell, cell - 1, cell - 1, fill);
            }

            var chainMap = BuildChainMap(board);
            for (int i = 0; i < board.tiles.Length; i++)
            {
                var tile = board.tiles[i];
                if (tile.type != TileType.Arrow)
                    continue;

                int x = i % board.width;
                int y = i / board.width;
                int chainId = chainMap.TryGetValue(i, out int value) ? value : i;
                var c = Color.HSVToRGB((chainId * 0.137f) % 1f, 0.52f, 0.92f);
                var fill = (Color32)c;
                int px = margin + x * cell + 2;
                int py = margin + y * cell + 2;
                FillRect(tex, px, py, cell - 5, cell - 5, fill);
                DrawOutDir(tex, margin + x * cell, margin + y * cell, cell, tile.arrow.outDir, black);
            }

            for (int y = 0; y <= board.height; y++)
                FillRect(tex, margin, margin + y * cell, board.width * cell, 1, grid);
            for (int x = 0; x <= board.width; x++)
                FillRect(tex, margin + x * cell, margin, 1, board.height * cell, grid);

            File.WriteAllBytes(ToFullPath(assetPath), tex.EncodeToPNG());
            UnityEngine.Object.DestroyImmediate(tex);
        }

        static Dictionary<int, int> BuildChainMap(BoardState board)
        {
            var result = new Dictionary<int, int>();
            var visited = new HashSet<int>();
            var chain = new HashSet<int>();
            int chainId = 0;

            for (int i = 0; i < board.tiles.Length; i++)
            {
                if (visited.Contains(i) || board.tiles[i].type != TileType.Arrow)
                    continue;

                ArrowChainUtility.CollectFullChain(board, new Vector2Int(i % board.width, i / board.width), 0, chain);
                foreach (int index in chain)
                {
                    visited.Add(index);
                    result[index] = chainId;
                }
                chainId++;
            }

            return result;
        }

        static void RenderContactSheet(List<string> assetPaths, string outputAssetPath)
        {
            if (assetPaths.Count == 0)
                return;

            var images = new List<Texture2D>();
            foreach (string path in assetPaths)
            {
                var bytes = File.ReadAllBytes(ToFullPath(path));
                var image = new Texture2D(2, 2, TextureFormat.RGBA32, false);
                image.LoadImage(bytes);
                images.Add(image);
            }

            int thumbW = 240;
            int thumbH = 280;
            int cols = 5;
            int rows = Mathf.CeilToInt(images.Count / (float)cols);
            var sheet = new Texture2D(cols * thumbW, rows * thumbH, TextureFormat.RGBA32, false);
            FillRect(sheet, 0, 0, sheet.width, sheet.height, new Color32(250, 247, 240, 255));

            for (int i = 0; i < images.Count; i++)
            {
                int col = i % cols;
                int row = rows - 1 - i / cols;
                BlitFit(sheet, images[i], col * thumbW + 12, row * thumbH + 12, thumbW - 24, thumbH - 24);
            }

            File.WriteAllBytes(ToFullPath(outputAssetPath), sheet.EncodeToPNG());
            foreach (var image in images)
                UnityEngine.Object.DestroyImmediate(image);
            UnityEngine.Object.DestroyImmediate(sheet);
        }

        static void BlitFit(Texture2D dst, Texture2D src, int x, int y, int w, int h)
        {
            float scale = Mathf.Min(w / (float)src.width, h / (float)src.height);
            int sw = Mathf.Max(1, Mathf.RoundToInt(src.width * scale));
            int sh = Mathf.Max(1, Mathf.RoundToInt(src.height * scale));
            int ox = x + (w - sw) / 2;
            int oy = y + (h - sh) / 2;

            for (int yy = 0; yy < sh; yy++)
            for (int xx = 0; xx < sw; xx++)
            {
                int sx = Mathf.Clamp(Mathf.FloorToInt(xx / scale), 0, src.width - 1);
                int sy = Mathf.Clamp(Mathf.FloorToInt(yy / scale), 0, src.height - 1);
                dst.SetPixel(ox + xx, oy + yy, src.GetPixel(sx, sy));
            }
        }

        static void DrawOutDir(Texture2D tex, int x, int y, int cell, Dir dir, Color32 color)
        {
            int cx = x + cell / 2;
            int cy = y + cell / 2;
            Vector2Int d = dir switch
            {
                Dir.Up => Vector2Int.up,
                Dir.Right => Vector2Int.right,
                Dir.Down => Vector2Int.down,
                _ => Vector2Int.left
            };

            DrawLine(tex, cx - d.x * 5, cy - d.y * 5, cx + d.x * 7, cy + d.y * 7, color);
            FillRect(tex, cx + d.x * 7 - 2, cy + d.y * 7 - 2, 5, 5, color);
        }

        static void DrawLine(Texture2D tex, int x0, int y0, int x1, int y1, Color32 c)
        {
            int dx = Mathf.Abs(x1 - x0);
            int sx = x0 < x1 ? 1 : -1;
            int dy = -Mathf.Abs(y1 - y0);
            int sy = y0 < y1 ? 1 : -1;
            int err = dx + dy;
            while (true)
            {
                FillRect(tex, x0 - 1, y0 - 1, 3, 3, c);
                if (x0 == x1 && y0 == y1)
                    break;
                int e2 = 2 * err;
                if (e2 >= dy) { err += dy; x0 += sx; }
                if (e2 <= dx) { err += dx; y0 += sy; }
            }
        }

        static void FillRect(Texture2D tex, int x, int y, int w, int h, Color32 c)
        {
            int x0 = Mathf.Clamp(x, 0, tex.width);
            int y0 = Mathf.Clamp(y, 0, tex.height);
            int x1 = Mathf.Clamp(x + w, 0, tex.width);
            int y1 = Mathf.Clamp(y + h, 0, tex.height);
            for (int yy = y0; yy < y1; yy++)
            for (int xx = x0; xx < x1; xx++)
                tex.SetPixel(xx, yy, c);
        }

        static void EnsureFolder(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return;

            path = path.Replace("\\", "/");
            if (AssetDatabase.IsValidFolder(path))
                return;

            string parent = Path.GetDirectoryName(path)?.Replace("\\", "/");
            if (!string.IsNullOrEmpty(parent))
                EnsureFolder(parent);

            string name = Path.GetFileName(path);
            AssetDatabase.CreateFolder(string.IsNullOrEmpty(parent) ? "Assets" : parent, name);
        }

        static void ClearGeneratedAssetsInFolder(string folder)
        {
            if (string.IsNullOrWhiteSpace(folder) || !AssetDatabase.IsValidFolder(folder))
                return;

            foreach (string guid in AssetDatabase.FindAssets("", new[] { folder }))
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                if (!string.IsNullOrWhiteSpace(path) && path.StartsWith(folder + "/", StringComparison.Ordinal))
                    AssetDatabase.DeleteAsset(path);
            }
        }

        static void ClearPreviewFiles(string fileNamePrefix)
        {
            string fullFolder = ToFullPath(PreviewFolder);
            if (!Directory.Exists(fullFolder))
                return;

            foreach (string file in Directory.GetFiles(fullFolder, fileNamePrefix + "*.png"))
                File.Delete(file);

            foreach (string file in Directory.GetFiles(fullFolder, fileNamePrefix + "*.png.meta"))
                File.Delete(file);
        }

        static string ToFullPath(string assetPath)
        {
            return Path.GetFullPath(Path.Combine(Application.dataPath, "..", assetPath));
        }
    }
}
#endif





