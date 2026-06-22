#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace PixelBug.ArrowMagic.EditorTools
{
    public static class ShapeIconMaskOnlyBatch11Runner
    {
        const string LevelFolder = "Assets/ArrowMagic/SOData/Levels/ShapeExperiment/Candidates/ProceduralMaskFill";
        const string DemoScenePath = "Assets/ArrowMagic/Scenes/Demo.unity";

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Run Shape Icon Mask Only Batch11 Candidate Pack")]
        public static void RunBatch11CandidatePack()
        {
            RunCandidatePack(BatchConfig.Batch11());
        }

        [MenuItem("Tools/ArrowMagic/Seed Mask Patch/Run Shape Icon Mask Only Batch12 Candidate Pack")]
        public static void RunBatch12CandidatePack()
        {
            RunCandidatePack(BatchConfig.Batch12());
        }

        static void RunCandidatePack(BatchConfig config)
        {
            EnsureFolder(LevelFolder);
            EnsureFolder(Path.GetDirectoryName(config.PackPath)?.Replace("\\", "/"));
            EnsureFolder(Path.GetDirectoryName(config.ReportPath)?.Replace("\\", "/"));

            var report = new List<string>
            {
                $"Shape Icon Mask Only {config.BatchLabel} Candidate Pack",
                $"BaseMaskFolder={config.BaseFolder}",
                $"VariantMaskFolder={config.VariantFolder}",
                $"Catalog={config.CatalogPath}",
                $"Pack={config.PackPath}",
                "Mode=StandaloneMaskOnlyOnionFill",
                "Policy=High coverage, non-straight, Greedy validated"
            };

            if (!TryLoadCatalog(config, out List<CatalogRow> rows, report))
            {
                WriteReport(config.ReportPath, report);
                return;
            }

            var builtLevels = new List<LevelDefinition>();
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);

            for (int i = 0; i < rows.Count; i++)
            {
                CatalogRow row = rows[i];
                string maskPath = ToProjectAssetPath(row.MaskPath);
                Texture2D mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);
                if (mask == null)
                {
                    report.Add($"[{i + 1}] MissingMask={row.MaskPath}");
                    WriteReport(config.ReportPath, report);
                    continue;
                }

                EnsureTextureReadable(maskPath);
                mask = AssetDatabase.LoadAssetAtPath<Texture2D>(maskPath);
                if (!TryReadMask(mask, out bool[] canSpawn, out int area))
                {
                    report.Add($"[{i + 1}] ReadMaskFailed={maskPath}");
                    WriteReport(config.ReportPath, report);
                    continue;
                }

                var profile = FillProfile.For(row.Variant, area);
                LevelDefinition bestLevel = null;
                string bestPath = string.Empty;
                string bestDetails = string.Empty;
                float bestScore = float.MinValue;

                for (int attempt = 0; attempt < profile.Attempts; attempt++)
                {
                    int seed = StableSeed(config.LevelPrefix, row.Name, row.Variant, attempt);
                    if (!TryBuildAuthored(mask.width, mask.height, canSpawn, area, profile, seed, out AuthoredLevelData authored, out BuildStats stats, out string buildDetails))
                    {
                        if (attempt == profile.Attempts - 1)
                            report.Add($"[{i + 1}] {row.Variant} build failed last={buildDetails}");
                        continue;
                    }

                    if (!TryValidate(authored, out BoardState board, out List<Move> moves, out string validateDetails))
                    {
                        if (attempt == profile.Attempts - 1)
                            report.Add($"[{i + 1}] {row.Variant} validate failed last={validateDetails} | {buildDetails}");
                        continue;
                    }

                    float score = ScoreCandidate(stats, profile, moves.Count);
                    if (score <= bestScore)
                        continue;

                    string stem = $"{config.LevelPrefix}_{row.Order:00}_{NormalizeId(row.Variant)}_{NormalizeId(row.Name)}_{stamp}";
                    string assetPath = $"{LevelFolder}/{stem}.asset";
                    bestLevel = SaveLevel(assetPath, stem, authored, mask, seed, stats, moves.Count, board);
                    bestPath = assetPath;
                    bestDetails = $"{buildDetails} | GreedyMoves={moves.Count} | Score={score:0.0}";
                    bestScore = score;
                }

                if (bestLevel == null)
                {
                    report.Add($"[{i + 1}] Failed={row.Name}/{row.Variant}");
                    WriteReport(config.ReportPath, report);
                    continue;
                }

                builtLevels.Add(bestLevel);
                report.Add($"[{i + 1}] Done={row.Name}/{row.Variant} | Mask={maskPath} | Level={bestPath} | {bestDetails}");
                WriteReport(config.ReportPath, report);
            }

            LevelPack pack = SavePack(config, builtLevels);
            AttachPackToDemo(pack, $"ShapeIconMaskOnly{config.BatchLabel}");
            report.Add($"PackSynced={config.PackPath} | Levels={builtLevels.Count}");
            report.Add("DemoAttached=True");
            WriteReport(config.ReportPath, report);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"[ShapeIconMaskOnly{config.BatchLabel}] finished levels={builtLevels.Count}, pack={config.PackPath}");
        }

        static bool TryLoadCatalog(BatchConfig config, out List<CatalogRow> rows, List<string> report)
        {
            rows = new List<CatalogRow>();
            string fullPath = ProjectFullPath(config.CatalogPath);
            if (!File.Exists(fullPath))
            {
                report.Add($"CatalogMissing={config.CatalogPath}");
                return false;
            }

            string[] lines = File.ReadAllLines(fullPath);
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                List<string> cols = ParseCsvLine(lines[i]);
                if (cols.Count < 10)
                    continue;

                int.TryParse(cols[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out int order);
                int.TryParse(cols[4], NumberStyles.Integer, CultureInfo.InvariantCulture, out int width);
                int.TryParse(cols[5], NumberStyles.Integer, CultureInfo.InvariantCulture, out int height);
                int.TryParse(cols[6], NumberStyles.Integer, CultureInfo.InvariantCulture, out int area);
                rows.Add(new CatalogRow
                {
                    Order = order,
                    Theme = cols[1],
                    Name = cols[2],
                    Variant = cols[3],
                    Width = width,
                    Height = height,
                    Area = area,
                    MaskPath = cols[9]
                });
            }

            rows.Sort((a, b) =>
            {
                int cmp = a.Order.CompareTo(b.Order);
                if (cmp != 0) return cmp;
                return VariantRank(a.Variant).CompareTo(VariantRank(b.Variant));
            });

            report.Add($"LoadedRows={rows.Count}");
            return rows.Count > 0;
        }

        static bool TryReadMask(Texture2D mask, out bool[] canSpawn, out int area)
        {
            canSpawn = null;
            area = 0;
            if (mask == null)
                return false;

            Color32[] pixels = mask.GetPixels32();
            if (pixels == null || pixels.Length != mask.width * mask.height)
                return false;

            canSpawn = new bool[pixels.Length];
            for (int i = 0; i < pixels.Length; i++)
            {
                bool allowed = pixels[i].a >= 64;
                canSpawn[i] = allowed;
                if (allowed)
                    area++;
            }

            return area > 0;
        }

        static bool TryBuildAuthored(
            int width,
            int height,
            bool[] canSpawn,
            int area,
            FillProfile profile,
            int seed,
            out AuthoredLevelData authored,
            out BuildStats stats,
            out string details)
        {
            authored = null;
            stats = default;
            details = string.Empty;

            var rng = new System.Random(seed);
            var unused = new bool[canSpawn.Length];
            Array.Copy(canSpawn, unused, canSpawn.Length);

            var chains = new List<List<int>>(profile.TargetChains + 8);
            int remaining = area;
            int stall = 0;

            while (remaining >= 2 && stall < width * height)
            {
                if (!TryPickHead(width, height, unused, rng, out HeadChoice head))
                {
                    if (!TryBuildFallbackPair(width, height, unused, out List<int> pair))
                        break;

                    chains.Add(pair);
                    MarkUsed(pair, unused, ref remaining);
                    stall = 0;
                    continue;
                }

                int chainsLeft = Mathf.Max(1, profile.TargetChains - chains.Count);
                int dynamicLen = Mathf.Clamp(Mathf.RoundToInt((float)remaining / chainsLeft), profile.MinLen, profile.MaxLen);
                int targetLen = Mathf.Clamp(dynamicLen + rng.Next(-2, 3), profile.MinLen, profile.MaxLen);

                if (!TryGrowPath(width, height, unused, head, targetLen, profile, rng, out List<int> path))
                {
                    stall++;
                    unused[head.Index] = false;
                    remaining--;
                    continue;
                }

                chains.Add(path);
                MarkUsed(path, unused, ref remaining);
                stall = 0;
            }

            if (remaining > 0)
                DropUnpairedSingles(unused, ref remaining);

            int used = area - remaining;
            float fill = area > 0 ? (float)used / area : 0f;
            if (chains.Count == 0 || fill < profile.MinFill)
            {
                details = $"low fill {fill:0.000}<{profile.MinFill:0.000}, chains={chains.Count}, remaining={remaining}";
                return false;
            }

            authored = new AuthoredLevelData
            {
                width = width,
                height = height,
                arrows = new List<AuthoredArrowData>(chains.Count),
                blockIndices = new List<int>()
            };

            int straightLong = 0;
            int maxLen = 0;
            int turns = 0;
            int totalLen = 0;
            for (int i = 0; i < chains.Count; i++)
            {
                List<int> chain = chains[i];
                if (chain.Count < 2)
                    continue;

                authored.arrows.Add(new AuthoredArrowData
                {
                    indices = chain,
                    colorIndex = i % 6
                });

                totalLen += chain.Count;
                maxLen = Mathf.Max(maxLen, chain.Count);
                int chainTurns = CountTurns(chain, width);
                turns += chainTurns;
                if (chain.Count >= 8 && chainTurns == 0)
                    straightLong++;
            }

            stats = new BuildStats
            {
                Area = area,
                Used = used,
                FillRatio = fill,
                Chains = authored.arrows.Count,
                MaxLen = maxLen,
                AvgLen = authored.arrows.Count > 0 ? (float)totalLen / authored.arrows.Count : 0f,
                Turns = turns,
                StraightLongChains = straightLong
            };

            details = $"Area={area} Used={used} Fill={fill:0.000} Chains={stats.Chains} AvgLen={stats.AvgLen:0.0} MaxLen={stats.MaxLen} Turns={turns} StraightLong={straightLong}";
            return authored.arrows.Count > 0;
        }

        static bool TryPickHead(int width, int height, bool[] unused, System.Random rng, out HeadChoice choice)
        {
            var candidates = new List<HeadChoice>(128);
            for (int i = 0; i < unused.Length; i++)
            {
                if (!unused[i])
                    continue;

                int x = i % width;
                int y = i / width;
                for (int d = 0; d < 4; d++)
                {
                    Vector2Int outOffset = DirOffset((Dir)d);
                    int ox = x + outOffset.x;
                    int oy = y + outOffset.y;
                    if (InBounds(ox, oy, width, height) && unused[Idx(ox, oy, width)])
                        continue;

                    Vector2Int inward = -outOffset;
                    int ix = x + inward.x;
                    int iy = y + inward.y;
                    if (!InBounds(ix, iy, width, height) || !unused[Idx(ix, iy, width)])
                        continue;

                    if (RayHitsUnused(x, y, outOffset, width, height, unused))
                        continue;

                    int score = 100;
                    if (!InBounds(ox, oy, width, height))
                        score += 40;
                    score += CountUnusedNeighbors(ix, iy, width, height, unused) * 6;
                    score += rng.Next(0, 32);
                    candidates.Add(new HeadChoice { Index = i, OutDir = (Dir)d, Score = score });
                }
            }

            if (candidates.Count == 0)
            {
                choice = default;
                return false;
            }

            candidates.Sort((a, b) => b.Score.CompareTo(a.Score));
            int pickWindow = Mathf.Min(candidates.Count, 12);
            choice = candidates[rng.Next(pickWindow)];
            return true;
        }

        static bool TryGrowPath(
            int width,
            int height,
            bool[] unused,
            HeadChoice head,
            int targetLen,
            FillProfile profile,
            System.Random rng,
            out List<int> path)
        {
            path = new List<int>(targetLen);
            int hx = head.Index % width;
            int hy = head.Index / width;
            Vector2Int inward = -DirOffset(head.OutDir);
            int sx = hx + inward.x;
            int sy = hy + inward.y;
            if (!InBounds(sx, sy, width, height))
                return false;

            int second = Idx(sx, sy, width);
            if (!unused[head.Index] || !unused[second])
                return false;

            var inPath = new HashSet<int> { head.Index, second };
            path.Add(head.Index);
            path.Add(second);

            while (path.Count < targetLen)
            {
                int tail = path[path.Count - 1];
                int prev = path[path.Count - 2];
                Vector2Int lastDir = Pos(tail, width) - Pos(prev, width);

                var options = new List<PathStep>(4);
                int tx = tail % width;
                int ty = tail / width;
                for (int d = 0; d < 4; d++)
                {
                    Vector2Int off = DirOffset((Dir)d);
                    int nx = tx + off.x;
                    int ny = ty + off.y;
                    if (!InBounds(nx, ny, width, height))
                        continue;

                    int ni = Idx(nx, ny, width);
                    if (!unused[ni] || inPath.Contains(ni))
                        continue;

                    int score = 10 + rng.Next(0, 20);
                    bool turning = off != lastDir;
                    if (turning)
                        score += profile.TurnReward;
                    else if (StraightRunLength(path, width, off) >= 3)
                        score -= profile.StraightPenalty;

                    score += (4 - CountUnusedNeighbors(nx, ny, width, height, unused)) * 3;
                    options.Add(new PathStep { Index = ni, Score = score });
                }

                if (options.Count == 0)
                    break;

                options.Sort((a, b) => b.Score.CompareTo(a.Score));
                int pickWindow = Mathf.Min(options.Count, 3);
                int next = options[rng.Next(pickWindow)].Index;
                path.Add(next);
                inPath.Add(next);
            }

            return path.Count >= 2;
        }

        static bool TryBuildFallbackPair(int width, int height, bool[] unused, out List<int> pair)
        {
            pair = null;
            for (int i = 0; i < unused.Length; i++)
            {
                if (!unused[i])
                    continue;

                int x = i % width;
                int y = i / width;
                for (int d = 0; d < 4; d++)
                {
                    Vector2Int off = DirOffset((Dir)d);
                    int nx = x + off.x;
                    int ny = y + off.y;
                    if (!InBounds(nx, ny, width, height))
                        continue;

                    int ni = Idx(nx, ny, width);
                    if (!unused[ni])
                        continue;

                    if (!RayHitsUnused(x, y, -off, width, height, unused))
                    {
                        pair = new List<int> { i, ni };
                        return true;
                    }

                    if (!RayHitsUnused(nx, ny, off, width, height, unused))
                    {
                        pair = new List<int> { ni, i };
                        return true;
                    }
                }
            }

            return false;
        }

        static bool TryValidate(AuthoredLevelData authored, out BoardState board, out List<Move> moves, out string details)
        {
            moves = null;
            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out board, out string buildError))
            {
                details = $"BuildBoard={buildError}";
                return false;
            }

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty,
                maxStepsOverride = 0
            });

            if (!GreedyValidator.TryClearAllByGreedy(CloneBoard(board), rules, 3200, out moves))
            {
                details = "GreedyFail";
                return false;
            }

            details = "GreedyOk";
            return true;
        }

        static LevelDefinition SaveLevel(
            string assetPath,
            string levelId,
            AuthoredLevelData authored,
            Texture2D mask,
            int seed,
            BuildStats stats,
            int greedyMoves,
            BoardState board)
        {
            LevelDefinition level = AssetDatabase.LoadAssetAtPath<LevelDefinition>(assetPath);
            bool isNew = level == null;
            if (level == null)
                level = ScriptableObject.CreateInstance<LevelDefinition>();

            level.levelId = levelId;
            level.source = LevelDefinition.LevelSource.Authored;
            level.board.width = authored.width;
            level.board.height = authored.height;
            level.board.seed = seed;
            level.generation.arrowCoverage = stats.FillRatio;
            level.generation.initialMovableArrowCount = CountInitialOpeners(board);
            level.generation.targetDifficultyScore = Mathf.RoundToInt(stats.Chains * 4f + stats.Turns * 1.5f + greedyMoves * 2f);
            level.generation.fixedGenerationSeed = seed;
            level.generation.minPathLen = 2;
            level.generation.maxPathLength = stats.MaxLen;
            level.generation.twistiness = stats.Turns > 0 ? Mathf.Clamp01((float)stats.Turns / Mathf.Max(1, stats.Used)) : 0f;
            level.generation.validateWithGreedy = true;
            level.authoredLevel = authored;
            level.lose.blockedLoseLimit = 3;
            level.masking.spawnMask = mask;
            level.masking.useAlphaOnly = true;
            level.masking.alphaThreshold = 0.5f;
            level.masking.useMaskToDefineBoardSize = true;
            level.arrowColorMode = BoardController.ArrowColorMode.UsePalette;

            EditorUtility.SetDirty(level);
            if (isNew)
                AssetDatabase.CreateAsset(level, assetPath);
            AssetDatabase.ImportAsset(assetPath);
            return AssetDatabase.LoadAssetAtPath<LevelDefinition>(assetPath);
        }

        static LevelPack SavePack(BatchConfig config, List<LevelDefinition> levels)
        {
            LevelPack pack = AssetDatabase.LoadAssetAtPath<LevelPack>(config.PackPath);
            bool isNew = pack == null;
            if (pack == null)
                pack = ScriptableObject.CreateInstance<LevelPack>();

            pack.packId = config.PackId;
            pack.displayName = $"Shape Icon Mask Only {config.BatchLabel} ({levels.Count})";
            pack.levels = levels.ToArray();
            EditorUtility.SetDirty(pack);

            if (isNew)
                AssetDatabase.CreateAsset(pack, config.PackPath);

            AssetDatabase.ImportAsset(config.PackPath);
            return AssetDatabase.LoadAssetAtPath<LevelPack>(config.PackPath);
        }

        static void AttachPackToDemo(LevelPack pack, string logTag)
        {
            if (pack == null)
                return;

            EditorSceneManager.OpenScene(DemoScenePath, OpenSceneMode.Single);
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
            EditorSceneManager.MarkSceneDirty(progression.gameObject.scene);
            EditorSceneManager.SaveScene(progression.gameObject.scene);
        }

        static int CountInitialOpeners(BoardState board)
        {
            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty,
                maxStepsOverride = 0
            });

            int count = 0;
            foreach (Move move in rules.GetLegalMoves(board))
            {
                BoardState clone = CloneBoard(board);
                if (rules.TryApplyMove(clone, move, out _))
                    count++;
            }

            return count;
        }

        static float ScoreCandidate(BuildStats stats, FillProfile profile, int greedyMoves)
        {
            float fillScore = stats.FillRatio * 1000f;
            float chainScore = -Mathf.Abs(stats.Chains - profile.TargetChains) * 6f;
            float straightPenalty = stats.StraightLongChains * 35f;
            float turnScore = Mathf.Min(160f, stats.Turns * 1.6f);
            float greedyScore = -greedyMoves * 0.4f;
            return fillScore + chainScore + turnScore + greedyScore - straightPenalty;
        }

        static void MarkUsed(List<int> path, bool[] unused, ref int remaining)
        {
            for (int i = 0; i < path.Count; i++)
            {
                int idx = path[i];
                if (idx >= 0 && idx < unused.Length && unused[idx])
                {
                    unused[idx] = false;
                    remaining--;
                }
            }
        }

        static void DropUnpairedSingles(bool[] unused, ref int remaining)
        {
            for (int i = 0; i < unused.Length; i++)
            {
                if (!unused[i])
                    continue;
                unused[i] = false;
                remaining--;
            }
        }

        static int CountTurns(List<int> path, int width)
        {
            int turns = 0;
            for (int i = 2; i < path.Count; i++)
            {
                Vector2Int a = Pos(path[i - 1], width) - Pos(path[i - 2], width);
                Vector2Int b = Pos(path[i], width) - Pos(path[i - 1], width);
                if (a != b)
                    turns++;
            }
            return turns;
        }

        static int StraightRunLength(List<int> path, int width, Vector2Int nextDir)
        {
            int run = 1;
            for (int i = path.Count - 1; i >= 1; i--)
            {
                Vector2Int d = Pos(path[i], width) - Pos(path[i - 1], width);
                if (d != nextDir)
                    break;
                run++;
            }
            return run;
        }

        static int CountUnusedNeighbors(int x, int y, int width, int height, bool[] unused)
        {
            int count = 0;
            for (int d = 0; d < 4; d++)
            {
                Vector2Int off = DirOffset((Dir)d);
                int nx = x + off.x;
                int ny = y + off.y;
                if (InBounds(nx, ny, width, height) && unused[Idx(nx, ny, width)])
                    count++;
            }
            return count;
        }

        static bool RayHitsUnused(int x, int y, Vector2Int dir, int width, int height, bool[] unused)
        {
            x += dir.x;
            y += dir.y;
            while (InBounds(x, y, width, height))
            {
                if (unused[Idx(x, y, width)])
                    return true;
                x += dir.x;
                y += dir.y;
            }
            return false;
        }

        static BoardState CloneBoard(BoardState src)
        {
            var dst = new BoardState(src.width, src.height);
            Array.Copy(src.tiles, dst.tiles, src.tiles.Length);
            return dst;
        }

        static Vector2Int DirOffset(Dir dir) => dir switch
        {
            Dir.Up => Vector2Int.up,
            Dir.Right => Vector2Int.right,
            Dir.Down => Vector2Int.down,
            _ => Vector2Int.left
        };

        static bool InBounds(int x, int y, int width, int height) => (uint)x < (uint)width && (uint)y < (uint)height;
        static int Idx(int x, int y, int width) => x + y * width;
        static Vector2Int Pos(int index, int width) => new Vector2Int(index % width, index / width);

        static int StableSeed(string prefix, string name, string variant, int attempt)
        {
            unchecked
            {
                int hash = 17;
                string s = $"{prefix}|{name}|{variant}|{attempt}";
                for (int i = 0; i < s.Length; i++)
                    hash = hash * 31 + s[i];
                return hash;
            }
        }

        static int VariantRank(string variant)
        {
            string v = (variant ?? string.Empty).ToLowerInvariant();
            if (v.Contains("compact")) return 0;
            if (v.Contains("main")) return 1;
            if (v.Contains("large")) return 2;
            return 9;
        }

        static string NormalizeId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "x";

            var chars = new List<char>(value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                char c = char.ToLowerInvariant(value[i]);
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                    chars.Add(c);
            }

            return chars.Count > 0 ? new string(chars.ToArray()) : "x";
        }

        static string ToProjectAssetPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return string.Empty;

            string normalized = path.Replace("\\", "/");
            if (normalized.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase))
                return normalized;

            int idx = normalized.IndexOf("/Assets/", StringComparison.OrdinalIgnoreCase);
            if (idx >= 0)
                return normalized.Substring(idx + 1);

            return normalized;
        }

        static string ProjectFullPath(string assetPath)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            return Path.GetFullPath(Path.Combine(projectRoot, assetPath));
        }

        static void EnsureFolder(string folder)
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

        static void EnsureTextureReadable(string assetPath)
        {
            var importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (importer == null)
                return;

            bool dirty = false;
            if (!importer.isReadable)
            {
                importer.isReadable = true;
                dirty = true;
            }

            if (importer.textureType != TextureImporterType.Default)
            {
                importer.textureType = TextureImporterType.Default;
                dirty = true;
            }

            if (importer.alphaSource == TextureImporterAlphaSource.None)
            {
                importer.alphaSource = TextureImporterAlphaSource.FromInput;
                dirty = true;
            }

            if (dirty)
                importer.SaveAndReimport();
        }

        static List<string> ParseCsvLine(string line)
        {
            var cols = new List<string>();
            var current = new System.Text.StringBuilder();
            bool quoted = false;
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '"')
                {
                    if (quoted && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        current.Append('"');
                        i++;
                    }
                    else
                    {
                        quoted = !quoted;
                    }
                    continue;
                }

                if (c == ',' && !quoted)
                {
                    cols.Add(current.ToString());
                    current.Length = 0;
                    continue;
                }

                current.Append(c);
            }

            cols.Add(current.ToString());
            return cols;
        }

        static void WriteReport(string reportPath, List<string> report)
        {
            string fullPath = ProjectFullPath(reportPath);
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath) ?? string.Empty);
            File.WriteAllLines(fullPath, report);
            AssetDatabase.ImportAsset(reportPath);
        }

        struct CatalogRow
        {
            public int Order;
            public string Theme;
            public string Name;
            public string Variant;
            public int Width;
            public int Height;
            public int Area;
            public string MaskPath;
        }

        sealed class BatchConfig
        {
            public string BatchLabel;
            public string LevelPrefix;
            public string BaseFolder;
            public string VariantFolder;
            public string CatalogPath;
            public string ReportPath;
            public string PackPath;
            public string PackId;

            public static BatchConfig Batch11()
            {
                return new BatchConfig
                {
                    BatchLabel = "Batch11",
                    LevelPrefix = "maskonlyb11",
                    BaseFolder = "Assets/ArrowMagic/Masks/ShapeIconMaskOnlyBatch11Base",
                    VariantFolder = "Assets/ArrowMagic/Masks/ShapeIconMaskOnlyBatch11Variants",
                    CatalogPath = "Assets/ArrowMagic/SOData/Reports/ShapeExperiment/shape_icon_mask_only_batch11_catalog.csv",
                    ReportPath = "Assets/ArrowMagic/SOData/Reports/ShapeExperiment/shape_icon_mask_only_batch11_candidate_report.txt",
                    PackPath = "Assets/ArrowMagic/SOData/Packs/ShapeExperiment/ShapeIconMaskOnlyBatch11CandidatePack.asset",
                    PackId = "ShapeIconMaskOnlyBatch11CandidatePack"
                };
            }

            public static BatchConfig Batch12()
            {
                return new BatchConfig
                {
                    BatchLabel = "Batch12",
                    LevelPrefix = "maskonlyb12",
                    BaseFolder = "Assets/ArrowMagic/Masks/ShapeIconMaskOnlyBatch12Base",
                    VariantFolder = "Assets/ArrowMagic/Masks/ShapeIconMaskOnlyBatch12Variants",
                    CatalogPath = "Assets/ArrowMagic/SOData/Reports/ShapeExperiment/shape_icon_mask_only_batch12_catalog.csv",
                    ReportPath = "Assets/ArrowMagic/SOData/Reports/ShapeExperiment/shape_icon_mask_only_batch12_candidate_report.txt",
                    PackPath = "Assets/ArrowMagic/SOData/Packs/ShapeExperiment/ShapeIconMaskOnlyBatch12CandidatePack.asset",
                    PackId = "ShapeIconMaskOnlyBatch12CandidatePack"
                };
            }
        }

        struct HeadChoice
        {
            public int Index;
            public Dir OutDir;
            public int Score;
        }

        struct PathStep
        {
            public int Index;
            public int Score;
        }

        struct BuildStats
        {
            public int Area;
            public int Used;
            public float FillRatio;
            public int Chains;
            public int MaxLen;
            public float AvgLen;
            public int Turns;
            public int StraightLongChains;
        }

        sealed class FillProfile
        {
            public int TargetChains;
            public int MinLen;
            public int MaxLen;
            public int Attempts;
            public int TurnReward;
            public int StraightPenalty;
            public float MinFill;

            public static FillProfile For(string variant, int area)
            {
                int rank = VariantRank(variant);
                if (rank == 0)
                {
                    return new FillProfile
                    {
                        TargetChains = Mathf.Clamp(Mathf.RoundToInt(area / 9.5f), 24, 38),
                        MinLen = 4,
                        MaxLen = 10,
                        Attempts = 80,
                        TurnReward = 22,
                        StraightPenalty = 35,
                        MinFill = 0.92f
                    };
                }

                if (rank == 2)
                {
                    return new FillProfile
                    {
                        TargetChains = Mathf.Clamp(Mathf.RoundToInt(area / 11.5f), 52, 72),
                        MinLen = 5,
                        MaxLen = 14,
                        Attempts = 110,
                        TurnReward = 26,
                        StraightPenalty = 42,
                        MinFill = 0.93f
                    };
                }

                return new FillProfile
                {
                    TargetChains = Mathf.Clamp(Mathf.RoundToInt(area / 10.5f), 40, 56),
                    MinLen = 4,
                    MaxLen = 12,
                    Attempts = 100,
                    TurnReward = 28,
                    StraightPenalty = 45,
                    MinFill = 0.93f
                };
            }
        }
    }
}
#endif
