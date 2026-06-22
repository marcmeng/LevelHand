using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelBug.ArrowMagic.EditorTools
{
    public static class ArchitecturalLineworkPreviewBuilder
    {
        private const string OutputFolder = "Assets/ArrowMagic/SOData/Levels/ArchitecturalLinework/Preview";
        private const string PackPath = "Assets/ArrowMagic/SOData/Packs/ArchitecturalLinework/ArchitecturalLineworkPreviewPack.asset";
        private const string ReportPath = "Assets/ArrowMagic/SOData/Reports/ArchitecturalLinework/architectural_linework_preview_report.csv";
        private const string DemoScenePath = "Assets/ArrowMagic/Scenes/Demo.unity";

        private enum TemplateKind
        {
            CircuitMaze,
            ParallelCorridor,
            NestedRooms
        }

        private sealed class Spec
        {
            public string Id;
            public TemplateKind Kind;
            public int Width;
            public int Height;
            public int Variant;
            public int TargetChains;
            public float TargetCoverage;
        }

        private sealed class Metrics
        {
            public int Chains;
            public int Arrows;
            public float Coverage;
            public int InitialOpeners;
            public int GreedyMoves;
            public int MaxChain;
            public string Status;
        }

        [MenuItem("Tools/ArrowMagic/Architectural Linework/Build Preview Pack")]
        public static void BuildPreviewPack()
        {
            EnsureFolder(OutputFolder);
            EnsureFolder(Path.GetDirectoryName(PackPath)?.Replace("\\", "/"));
            EnsureFolder(Path.GetDirectoryName(ReportPath)?.Replace("\\", "/"));

            var levels = new List<LevelDefinition>();
            var report = new List<string>
            {
                "Index,Kind,LevelId,AssetPath,Width,Height,Chains,Arrows,Coverage,InitialOpeners,GreedyMoves,MaxChain,Status"
            };

            Spec[] specs = CreateSpecs();
            for (int i = 0; i < specs.Length; i++)
            {
                Spec spec = specs[i];
                string levelId = $"arch_linework_{i + 1:00}_{spec.Id}";
                string assetPath = $"{OutputFolder}/{levelId}.asset";

                if (!TryBuild(spec, levelId, assetPath, out LevelDefinition level, out Metrics metrics))
                {
                    report.Add($"{i + 1},{spec.Kind},{levelId},{assetPath},{spec.Width},{spec.Height},0,0,0,0,0,0,{EscapeCsv(metrics.Status)}");
                    File.WriteAllLines(ReportPath, report);
                    Debug.LogWarning($"[ArchitecturalLinework] Failed {levelId}: {metrics.Status}");
                    continue;
                }

                levels.Add(level);
                report.Add(
                    $"{i + 1},{spec.Kind},{levelId},{assetPath},{spec.Width},{spec.Height}," +
                    $"{metrics.Chains},{metrics.Arrows},{metrics.Coverage:0.000},{metrics.InitialOpeners},{metrics.GreedyMoves},{metrics.MaxChain},{EscapeCsv(metrics.Status)}");
                File.WriteAllLines(ReportPath, report);
                Debug.Log($"[ArchitecturalLinework] Built {levelId}: chains={metrics.Chains}, coverage={metrics.Coverage:0.000}, openers={metrics.InitialOpeners}");
            }

            File.WriteAllLines(ReportPath, report);
            AssetDatabase.ImportAsset(ReportPath);

            LevelPack pack = SavePack(levels);
            AttachPackToDemo(pack);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"[ArchitecturalLinework] Preview complete. levels={levels.Count}, pack={PackPath}, report={ReportPath}");
        }

        private static Spec[] CreateSpecs()
        {
            var list = new List<Spec>();
            for (int i = 0; i < 5; i++)
            {
                list.Add(new Spec { Id = $"circuit_v{i + 1}", Kind = TemplateKind.CircuitMaze, Width = 26 + i % 3, Height = 36 + i % 4, Variant = i, TargetChains = 34 + i * 2, TargetCoverage = 0.52f });
                list.Add(new Spec { Id = $"corridor_v{i + 1}", Kind = TemplateKind.ParallelCorridor, Width = 27 + i % 2, Height = 36 + i % 4, Variant = i, TargetChains = 38 + i * 2, TargetCoverage = 0.58f });
                list.Add(new Spec { Id = $"rooms_v{i + 1}", Kind = TemplateKind.NestedRooms, Width = 25 + i % 3, Height = 36 + i % 4, Variant = i, TargetChains = 32 + i * 2, TargetCoverage = 0.50f });
            }

            return list.ToArray();
        }

        private static bool TryBuild(Spec spec, string levelId, string assetPath, out LevelDefinition level, out Metrics metrics)
        {
            level = null;
            metrics = new Metrics();

            AuthoredLevelData authored = BuildAuthored(spec);
            if (authored.arrows.Count == 0)
            {
                metrics.Status = "No chains";
                return false;
            }

            if (!AuthoredLevelBuilder.TryBuildBoard(authored, out BoardState board, out string buildError))
            {
                metrics.Status = $"BoardBuildFailed={buildError}";
                return false;
            }

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig { travelMode = SignalTravelMode.ThroughEmpty });
            if (!GreedyValidator.TryClearAllByGreedy(CloneBoard(board), rules, 1200, out List<Move> moves))
            {
                metrics.Status = "GreedyFailed";
                return false;
            }

            metrics.Chains = authored.arrows.Count;
            metrics.Arrows = CountArrowCells(authored);
            metrics.Coverage = metrics.Arrows / (float)(authored.width * authored.height);
            metrics.InitialOpeners = CountInitialOpeners(board, rules, authored);
            metrics.GreedyMoves = moves?.Count ?? 0;
            metrics.MaxChain = MaxChainLength(authored);
            metrics.Status = "ok";

            level = SaveLevel(assetPath, levelId, authored, spec, metrics);
            return true;
        }

        private static AuthoredLevelData BuildAuthored(Spec spec)
        {
            var authored = new AuthoredLevelData
            {
                width = spec.Width,
                height = spec.Height,
                arrows = new List<AuthoredArrowData>(),
                blockIndices = new List<int>()
            };

            var occupied = new bool[spec.Width * spec.Height];
            switch (spec.Kind)
            {
                case TemplateKind.CircuitMaze:
                    BuildCircuitMaze(spec, authored, occupied);
                    break;
                case TemplateKind.ParallelCorridor:
                    BuildParallelCorridor(spec, authored, occupied);
                    break;
                case TemplateKind.NestedRooms:
                    BuildNestedRooms(spec, authored, occupied);
                    break;
            }

            AddArchitecturalFill(spec, authored, occupied);
            return authored;
        }

        private static void BuildCircuitMaze(Spec spec, AuthoredLevelData authored, bool[] occupied)
        {
            int w = spec.Width;
            int h = spec.Height;
            int shift = spec.Variant % 3;

            AddPolyline(authored, occupied, P(0, h - 3), P(w - 4, h - 3), P(w - 4, h - 8), P(w - 9, h - 8), P(w - 9, h - 5), P(w - 14, h - 5));
            AddPolyline(authored, occupied, P(w - 1, h - 6), P(w - 7, h - 6), P(w - 7, h - 12), P(w - 3, h - 12), P(w - 3, h - 17));
            AddPolyline(authored, occupied, P(0, h - 12), P(7 + shift, h - 12), P(7 + shift, h - 18), P(13 + shift, h - 18), P(13 + shift, h - 22));
            AddPolyline(authored, occupied, P(w - 1, 4), P(w - 6, 4), P(w - 6, 10), P(w - 12, 10), P(w - 12, 6), P(w - 17, 6));
            AddPolyline(authored, occupied, P(0, 3), P(9, 3), P(9, 8), P(4, 8), P(4, 13), P(10, 13));

            AddOpenFrame(authored, occupied, 3, 10, h - 11, h - 6, Side.Left);
            AddOpenFrame(authored, occupied, 12, 20, h - 16, h - 10, Side.Top);
            AddOpenFrame(authored, occupied, 3, 11, 11, 17, Side.Left);
            AddOpenFrame(authored, occupied, 13, 21, 13, 20, Side.Right);
            AddOpenFrame(authored, occupied, 7, 16, 4, 10, Side.Bottom);
        }

        private static void BuildParallelCorridor(Spec spec, AuthoredLevelData authored, bool[] occupied)
        {
            int w = spec.Width;
            int h = spec.Height;
            int top = h - 3;
            int rowGap = 3 + (spec.Variant % 2);

            for (int i = 0; i < 7; i++)
            {
                int y = top - i * rowGap;
                if (y < 5)
                    break;

                if ((i & 1) == 0)
                    AddPolyline(authored, occupied, P(0, y), P(w - 3, y), P(w - 3, y - 1), P(5 + i % 3, y - 1));
                else
                    AddPolyline(authored, occupied, P(w - 1, y), P(2, y), P(2, y - 1), P(w - 6 - i % 3, y - 1));
            }

            AddOpenFrame(authored, occupied, 3, 9, 4, 12, Side.Left);
            AddOpenFrame(authored, occupied, 11, 18, 5, 14, Side.Bottom);
            AddOpenFrame(authored, occupied, w - 9, w - 3, 15, 23, Side.Right);
            AddPolyline(authored, occupied, P(0, 2), P(w - 4, 2), P(w - 4, 4));
            AddPolyline(authored, occupied, P(w - 1, h - 2), P(w - 11, h - 2), P(w - 11, h - 5));
        }

        private static void BuildNestedRooms(Spec spec, AuthoredLevelData authored, bool[] occupied)
        {
            int w = spec.Width;
            int h = spec.Height;
            int inset = 1 + spec.Variant % 2;

            AddOpenFrame(authored, occupied, inset, w - 2, 2, h - 3, Side.Left);
            AddOpenFrame(authored, occupied, inset + 3, w - 5, 6, h - 7, Side.Top);
            AddOpenFrame(authored, occupied, inset + 6, w - 8, 10, h - 11, Side.Right);
            AddOpenFrame(authored, occupied, 3, 10, 4, 11, Side.Bottom);
            AddOpenFrame(authored, occupied, w - 10, w - 3, 4, 12, Side.Right);
            AddPolyline(authored, occupied, P(0, h - 6), P(6, h - 6), P(6, h - 10), P(3, h - 10));
            AddPolyline(authored, occupied, P(w - 1, h - 9), P(w - 6, h - 9), P(w - 6, h - 14), P(w - 12, h - 14));
            AddPolyline(authored, occupied, P(0, 14), P(5, 14), P(5, 19), P(11, 19), P(11, 16));
        }

        private static void AddArchitecturalFill(Spec spec, AuthoredLevelData authored, bool[] occupied)
        {
            int w = spec.Width;
            int h = spec.Height;

            AddEdgeCorridorField(spec, authored, occupied);
            AddRoomletField(spec, authored, occupied);
            AddColumnHooks(spec, authored, occupied);

            int minCells = Mathf.RoundToInt(w * h * spec.TargetCoverage);
            int maxChains = spec.TargetChains + 8;
            for (int attempt = 0; attempt < 900; attempt++)
            {
                if (authored.arrows.Count >= spec.TargetChains && CountArrowCells(authored) >= minCells)
                    break;

                if (authored.arrows.Count >= maxChains)
                    break;

                int mode = PositiveMod(attempt + spec.Variant, 4);
                bool added;
                switch (mode)
                {
                    case 0:
                        added = TryAddEdgeHook(spec, authored, occupied, attempt, true);
                        break;
                    case 1:
                        added = TryAddEdgeHook(spec, authored, occupied, attempt, false);
                        break;
                    case 2:
                        added = TryAddVerticalHook(spec, authored, occupied, attempt, true);
                        break;
                    default:
                        added = TryAddVerticalHook(spec, authored, occupied, attempt, false);
                        break;
                }

                if (!added && (attempt % 5) == 0)
                    TryAddCompactRoomlet(spec, authored, occupied, attempt);
            }
        }

        private static void AddEdgeCorridorField(Spec spec, AuthoredLevelData authored, bool[] occupied)
        {
            int w = spec.Width;
            int h = spec.Height;
            int step = spec.Kind == TemplateKind.ParallelCorridor ? 3 : 4;

            for (int row = 0, y = h - 2; y >= 2; row++, y -= step)
            {
                bool fromLeft = ((row + spec.Variant) & 1) == 0;
                int span = Mathf.Clamp(w - 5 - PositiveMod(row + spec.Variant, 5), 9, w - 2);
                int jogY = Mathf.Clamp(y - 1 - PositiveMod(row, 2), 1, h - 2);
                int returnLen = Mathf.Clamp(4 + PositiveMod(row * 3 + spec.Variant, 8), 3, w - 6);

                if (fromLeft)
                    AddPolyline(authored, occupied, P(0, y), P(span, y), P(span, jogY), P(Mathf.Max(2, span - returnLen), jogY));
                else
                    AddPolyline(authored, occupied, P(w - 1, y), P(w - 1 - span, y), P(w - 1 - span, jogY), P(Mathf.Min(w - 3, w - 1 - span + returnLen), jogY));
            }
        }

        private static void AddRoomletField(Spec spec, AuthoredLevelData authored, bool[] occupied)
        {
            int w = spec.Width;
            int h = spec.Height;
            int cellW = spec.Kind == TemplateKind.NestedRooms ? 7 : 8;
            int cellH = spec.Kind == TemplateKind.NestedRooms ? 7 : 8;
            int n = 0;

            for (int y = 4 + spec.Variant % 2; y < h - 6; y += cellH)
            {
                for (int x = 3 + (n + spec.Variant) % 3; x < w - 7; x += cellW)
                {
                    int right = Mathf.Min(w - 3, x + 4 + PositiveMod(n + spec.Variant, 3));
                    int top = Mathf.Min(h - 3, y + 4 + PositiveMod(n * 2 + spec.Variant, 3));
                    Side side = (Side)PositiveMod(n + spec.Variant, 4);
                    AddOpenFrame(authored, occupied, x, right, y, top, side);
                    n++;
                }
            }
        }

        private static void AddColumnHooks(Spec spec, AuthoredLevelData authored, bool[] occupied)
        {
            int w = spec.Width;
            int h = spec.Height;

            for (int i = 0, x = 2 + spec.Variant % 4; x < w - 2; i++, x += 5)
            {
                bool fromTop = ((i + spec.Variant) & 1) == 0;
                int depth = Mathf.Clamp(9 + PositiveMod(i * 4 + spec.Variant, h / 2), 7, h - 5);
                int jogX = Mathf.Clamp(x + (((i & 1) == 0) ? 2 : -2), 1, w - 2);
                int jogY = fromTop ? Mathf.Max(2, h - 1 - depth) : Mathf.Min(h - 3, depth);

                if (fromTop)
                    AddPolyline(authored, occupied, P(x, h - 1), P(x, jogY), P(jogX, jogY), P(jogX, Mathf.Min(h - 3, jogY + 3)));
                else
                    AddPolyline(authored, occupied, P(x, 0), P(x, jogY), P(jogX, jogY), P(jogX, Mathf.Max(2, jogY - 3)));
            }
        }

        private static bool TryAddEdgeHook(Spec spec, AuthoredLevelData authored, bool[] occupied, int attempt, bool fromLeft)
        {
            int w = spec.Width;
            int h = spec.Height;
            int y = 1 + PositiveMod(attempt * 5 + spec.Variant * 7, h - 2);
            int span = Mathf.Clamp(6 + PositiveMod(attempt * 7 + spec.Variant, w - 8), 5, w - 3);
            int jog = ((attempt + spec.Variant) & 1) == 0 ? 1 : -1;
            int jogY = Mathf.Clamp(y + jog, 1, h - 2);
            int bite = Mathf.Clamp(3 + PositiveMod(attempt * 3 + spec.Variant, 7), 3, w - 5);

            if (fromLeft)
            {
                int endX = Mathf.Min(w - 2, span);
                return AddPolyline(authored, occupied, P(0, y), P(endX, y), P(endX, jogY), P(Mathf.Max(1, endX - bite), jogY));
            }

            int end = Mathf.Max(1, w - 1 - span);
            return AddPolyline(authored, occupied, P(w - 1, y), P(end, y), P(end, jogY), P(Mathf.Min(w - 2, end + bite), jogY));
        }

        private static bool TryAddVerticalHook(Spec spec, AuthoredLevelData authored, bool[] occupied, int attempt, bool fromTop)
        {
            int w = spec.Width;
            int h = spec.Height;
            int x = 1 + PositiveMod(attempt * 7 + spec.Variant * 5, w - 2);
            int span = Mathf.Clamp(7 + PositiveMod(attempt * 5 + spec.Variant, h - 9), 6, h - 3);
            int jog = ((attempt + spec.Variant) & 1) == 0 ? 1 : -1;
            int jogX = Mathf.Clamp(x + jog, 1, w - 2);
            int bite = Mathf.Clamp(3 + PositiveMod(attempt * 2 + spec.Variant, 6), 3, h - 6);

            if (fromTop)
            {
                int endY = Mathf.Max(1, h - 1 - span);
                return AddPolyline(authored, occupied, P(x, h - 1), P(x, endY), P(jogX, endY), P(jogX, Mathf.Min(h - 2, endY + bite)));
            }

            int end = Mathf.Min(h - 2, span);
            return AddPolyline(authored, occupied, P(x, 0), P(x, end), P(jogX, end), P(jogX, Mathf.Max(1, end - bite)));
        }

        private static bool TryAddCompactRoomlet(Spec spec, AuthoredLevelData authored, bool[] occupied, int attempt)
        {
            int w = spec.Width;
            int h = spec.Height;
            int left = 2 + PositiveMod(attempt * 3 + spec.Variant, Math.Max(1, w - 9));
            int bottom = 2 + PositiveMod(attempt * 5 + spec.Variant, Math.Max(1, h - 9));
            int right = Mathf.Min(w - 3, left + 4 + PositiveMod(attempt, 3));
            int top = Mathf.Min(h - 3, bottom + 4 + PositiveMod(attempt * 2, 3));
            return AddOpenFrame(authored, occupied, left, right, bottom, top, (Side)PositiveMod(attempt, 4));
        }

        private enum Side
        {
            Left,
            Right,
            Top,
            Bottom
        }

        private static bool AddOpenFrame(AuthoredLevelData authored, bool[] occupied, int left, int right, int bottom, int top, Side side)
        {
            if (right - left < 3 || top - bottom < 3)
                return false;

            switch (side)
            {
                case Side.Left:
                    return AddPolyline(authored, occupied, P(left, top), P(right, top), P(right, bottom), P(left, bottom), P(left, bottom + 2));
                case Side.Right:
                    return AddPolyline(authored, occupied, P(right, bottom), P(left, bottom), P(left, top), P(right, top), P(right, top - 2));
                case Side.Top:
                    return AddPolyline(authored, occupied, P(left, top), P(left, bottom), P(right, bottom), P(right, top), P(right - 2, top));
                case Side.Bottom:
                    return AddPolyline(authored, occupied, P(right, bottom), P(right, top), P(left, top), P(left, bottom), P(left + 2, bottom));
            }

            return false;
        }

        private static bool TryAddStraightRun(AuthoredLevelData authored, bool[] occupied, int x0, int y0, int x1, int y1)
        {
            if (x0 == x1)
                return AddPolyline(authored, occupied, P(x0, y0), P(x1, y1));
            else if (y0 == y1)
                return AddPolyline(authored, occupied, P(x0, y0), P(x1, y1));

            return false;
        }

        private static bool AddPolyline(AuthoredLevelData authored, bool[] occupied, params Vector2Int[] points)
        {
            if (points == null || points.Length < 2)
                return false;

            var path = new List<Vector2Int>();
            for (int i = 0; i < points.Length - 1; i++)
            {
                Vector2Int a = points[i];
                Vector2Int b = points[i + 1];
                int dx = Math.Sign(b.x - a.x);
                int dy = Math.Sign(b.y - a.y);
                if (dx != 0 && dy != 0)
                    return false;

                int steps = Math.Max(Math.Abs(b.x - a.x), Math.Abs(b.y - a.y));
                for (int s = 0; s <= steps; s++)
                {
                    Vector2Int p = new Vector2Int(a.x + dx * s, a.y + dy * s);
                    if (path.Count == 0 || path[path.Count - 1] != p)
                        path.Add(p);
                }
            }

            if (TryAddSegmentedPath(authored, occupied, path))
                return true;

            return TryAddPath(authored, occupied, path);
        }

        private static bool TryAddSegmentedPath(AuthoredLevelData authored, bool[] occupied, List<Vector2Int> path)
        {
            const int minSegmentLength = 6;
            const int maxSegmentLength = 10;

            if (path == null || path.Count < maxSegmentLength + 4)
                return false;

            int w = authored.width;
            int h = authored.height;
            var chunks = new List<List<int>>();
            var local = new HashSet<int>();
            int start = 0;
            while (start < path.Count)
            {
                int remaining = path.Count - start;
                int len;
                if (remaining <= maxSegmentLength)
                {
                    len = remaining;
                }
                else
                {
                    int span = maxSegmentLength - minSegmentLength + 1;
                    len = minSegmentLength + ((authored.arrows.Count + start) % span);
                    if (remaining - len == 1)
                        len++;
                }

                if (len < 2)
                    return false;

                var indices = new List<int>(len);
                for (int i = 0; i < len; i++)
                {
                    Vector2Int p = path[start + i];
                    if (p.x < 0 || p.x >= w || p.y < 0 || p.y >= h)
                        return false;

                    int idx = Index(w, p.x, p.y);
                    if (occupied[idx] || !local.Add(idx))
                        return false;

                    indices.Add(idx);
                }

                chunks.Add(indices);
                start += len;
            }

            if (chunks.Count < 2)
                return false;

            foreach (List<int> chunk in chunks)
            {
                foreach (int idx in chunk)
                    occupied[idx] = true;

                authored.arrows.Add(new AuthoredArrowData
                {
                    indices = chunk,
                    colorIndex = authored.arrows.Count % 6
                });
            }

            return true;
        }

        private static bool TryAddPath(AuthoredLevelData authored, bool[] occupied, List<Vector2Int> path)
        {
            if (path == null || path.Count < 2)
                return false;

            int w = authored.width;
            int h = authored.height;
            var indices = new List<int>(path.Count);
            var local = new HashSet<int>();
            for (int i = 0; i < path.Count; i++)
            {
                Vector2Int p = path[i];
                if (p.x < 0 || p.x >= w || p.y < 0 || p.y >= h)
                    return false;

                int idx = Index(w, p.x, p.y);
                if (occupied[idx] || !local.Add(idx))
                    return false;

                indices.Add(idx);
            }

            for (int i = 0; i < indices.Count; i++)
                occupied[indices[i]] = true;

            authored.arrows.Add(new AuthoredArrowData
            {
                indices = indices,
                colorIndex = authored.arrows.Count % 6
            });
            return true;
        }

        private static LevelDefinition SaveLevel(string assetPath, string levelId, AuthoredLevelData authored, Spec spec, Metrics metrics)
        {
            LevelDefinition level = AssetDatabase.LoadAssetAtPath<LevelDefinition>(assetPath);
            bool isNew = level == null;
            if (level == null)
                level = ScriptableObject.CreateInstance<LevelDefinition>();

            level.levelId = levelId;
            level.source = LevelDefinition.LevelSource.Authored;
            level.board.width = authored.width;
            level.board.height = authored.height;
            level.board.seed = 510000 + spec.Variant;
            level.generation.arrowCoverage = metrics.Coverage;
            level.generation.initialMovableArrowCount = metrics.InitialOpeners;
            level.generation.targetDifficultyScore = metrics.Chains * 2 + metrics.GreedyMoves;
            level.generation.fixedGenerationSeed = level.board.seed;
            level.generation.minPathLen = 2;
            level.generation.maxPathLength = Math.Max(2, MaxChainLength(authored));
            level.generation.twistiness = 0.35f;
            level.generation.validateWithGreedy = true;
            level.authoredLevel = Clone(authored);
            level.masking.spawnMask = null;
            level.masking.useMaskToDefineBoardSize = false;

            EditorUtility.SetDirty(level);
            if (isNew)
                AssetDatabase.CreateAsset(level, assetPath);
            AssetDatabase.ImportAsset(assetPath);
            return level;
        }

        private static LevelPack SavePack(List<LevelDefinition> levels)
        {
            LevelPack pack = AssetDatabase.LoadAssetAtPath<LevelPack>(PackPath);
            bool isNew = pack == null;
            if (pack == null)
                pack = ScriptableObject.CreateInstance<LevelPack>();

            pack.packId = "architectural_linework_preview";
            pack.displayName = $"Architectural Linework Preview ({levels.Count})";
            pack.levels = levels.ToArray();
            EditorUtility.SetDirty(pack);
            if (isNew)
                AssetDatabase.CreateAsset(pack, PackPath);
            AssetDatabase.ImportAsset(PackPath);
            return pack;
        }

        private static void AttachPackToDemo(LevelPack pack)
        {
            if (pack == null)
                return;

            Scene scene = EditorSceneManager.OpenScene(DemoScenePath, OpenSceneMode.Single);
            LevelProgression progression = null;
            foreach (GameObject root in scene.GetRootGameObjects())
            {
                progression = root.GetComponentInChildren<LevelProgression>(true);
                if (progression != null)
                    break;
            }

            if (progression == null)
            {
                Debug.LogWarning("[ArchitecturalLinework] LevelProgression not found in Demo.");
                return;
            }

            var so = new SerializedObject(progression);
            SerializedProperty activePack = so.FindProperty("activePack");
            if (activePack == null)
            {
                Debug.LogWarning("[ArchitecturalLinework] activePack property not found.");
                return;
            }

            activePack.objectReferenceValue = pack;
            so.ApplyModifiedProperties();
            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
        }

        private static AuthoredLevelData Clone(AuthoredLevelData source)
        {
            var clone = new AuthoredLevelData
            {
                width = source.width,
                height = source.height,
                arrows = new List<AuthoredArrowData>(source.arrows.Count),
                blockIndices = source.blockIndices != null ? new List<int>(source.blockIndices) : new List<int>()
            };

            foreach (AuthoredArrowData arrow in source.arrows)
            {
                clone.arrows.Add(new AuthoredArrowData
                {
                    indices = arrow.indices != null ? new List<int>(arrow.indices) : new List<int>(),
                    colorIndex = arrow.colorIndex
                });
            }

            return clone;
        }

        private static BoardState CloneBoard(BoardState src)
        {
            var dst = new BoardState(src.width, src.height);
            for (int i = 0; i < src.tiles.Length; i++)
                dst.tiles[i] = src.tiles[i];
            return dst;
        }

        private static int CountArrowCells(AuthoredLevelData authored)
        {
            int count = 0;
            foreach (AuthoredArrowData arrow in authored.arrows)
                count += arrow.indices?.Count ?? 0;
            return count;
        }

        private static int CountInitialOpeners(BoardState board, IRuleset rules, AuthoredLevelData authored)
        {
            int count = 0;
            for (int i = 0; i < authored.arrows.Count; i++)
            {
                BoardState test = CloneBoard(board);
                if (TryClearChain(test, rules, authored.arrows[i]))
                    count++;
            }
            return count;
        }

        private static bool TryClearChain(BoardState board, IRuleset rules, AuthoredArrowData arrow)
        {
            if (arrow.indices == null || arrow.indices.Count == 0)
                return false;

            int head = arrow.indices[0];
            var move = new Move(Pos(board.width, head));
            if (!rules.TryApplyMove(board, move, out MoveDelta delta))
                return false;

            delta.Undo(board);
            return true;
        }

        private static int MaxChainLength(AuthoredLevelData authored)
        {
            int max = 0;
            foreach (AuthoredArrowData arrow in authored.arrows)
                max = Math.Max(max, arrow.indices?.Count ?? 0);
            return max;
        }

        private static Vector2Int P(int x, int y) => new Vector2Int(x, y);
        private static int Index(int width, int x, int y) => y * width + x;
        private static Vector2Int Pos(int width, int index) => new Vector2Int(index % width, index / width);
        private static int PositiveMod(int value, int modulo)
        {
            if (modulo <= 0)
                return 0;

            int result = value % modulo;
            return result < 0 ? result + modulo : result;
        }

        private static void EnsureFolder(string path)
        {
            if (string.IsNullOrEmpty(path) || AssetDatabase.IsValidFolder(path))
                return;

            string parent = Path.GetDirectoryName(path)?.Replace("\\", "/");
            if (!string.IsNullOrEmpty(parent))
                EnsureFolder(parent);

            string name = Path.GetFileName(path);
            AssetDatabase.CreateFolder(parent, name);
        }

        private static string EscapeCsv(string value)
        {
            value ??= string.Empty;
            if (value.IndexOfAny(new[] { ',', '"', '\n', '\r' }) < 0)
                return value;
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
    }
}
