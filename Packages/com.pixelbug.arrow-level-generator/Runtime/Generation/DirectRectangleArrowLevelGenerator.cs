using System;
using System.Collections.Generic;

namespace PixelBug.ArrowLevelGenerator
{
    public sealed class DirectRectangleArrowLevelGenerator : IArrowLevelGenerator
    {
        private const int MaxAttemptsPerLevel = 64;

        public IReadOnlyList<ArrowGenerationResult> Generate(ArrowGenerationRequest request)
        {
            request = request ?? new ArrowGenerationRequest();
            int count = Math.Max(1, request.count);
            var results = new List<ArrowGenerationResult>(count);
            var evaluator = new ArrowLevelQualityEvaluator();
            var solver = new GreedyEscapeSolver();

            for (int i = 0; i < count; i++)
            {
                var failure = new ArrowGenerationResult();
                ArrowGenerationResult accepted = null;
                ArrowGenerationResult fallback = null;
                int seed = request.seed + i * 1000003;

                for (int attempt = 0; attempt < MaxAttemptsPerLevel; attempt++)
                {
                    var rng = new Random(seed + attempt * 104729);
                    ArrowLevelFamily family = PickFamily(request, i, rng);
                    int width = Range(rng, request.minWidth, request.maxWidth);
                    int height = Range(rng, request.minHeight, request.maxHeight);
                    int targetChains = Math.Max(request.minChains, request.maxChains);
                    ArrowQualityPolicy policy = request.quality ?? ArrowQualityPolicy.ForDifficulty(request.difficulty);
                    float effectiveTargetCoverage = Math.Min(0.985f, Math.Max(request.targetCoverage, policy.minCoverage) + 0.015f);

                    ArrowLevelData level = BuildLayeredRectangle(
                        $"{request.levelIdPrefix}_{i + 1:000}",
                        width,
                        height,
                        targetChains,
                        effectiveTargetCoverage,
                        request.difficulty,
                        family,
                        rng);

                    ArrowLevelValidationReport validation = ArrowLevelValidator.Validate(level);
                    if (!validation.isValid)
                    {
                        failure.status = "invalid-geometry: " + string.Join(" | ", validation.errors);
                        continue;
                    }

                    ArrowSolveReport solve = solver.Solve(level);
                    if (request.requireGreedySolvable && !solve.solved)
                    {
                        failure.status = $"greedy-fail removed={solve.removedChains}/{solve.totalChains}";
                        continue;
                    }

                    ArrowLevelMetrics metrics = evaluator.Evaluate(level, policy);
                    var candidate = new ArrowGenerationResult
                    {
                        level = level,
                        metrics = metrics,
                        solveReport = solve,
                        success = true,
                        status = metrics.qualityFlags == "ok" ? "ok" : $"quality={metrics.qualityFlags}"
                    };

                    if (metrics.qualityFlags == "ok")
                    {
                        accepted = candidate;
                        break;
                    }

                    if (fallback == null || metrics.playabilityScore > fallback.metrics.playabilityScore)
                        fallback = candidate;
                }

                ArrowGenerationResult result = accepted ?? fallback ?? failure;
                if (!result.success && string.IsNullOrWhiteSpace(result.status))
                    result.status = "generation-exhausted";
                if (accepted == null && fallback != null)
                    result.warnings.Add("Used best non-ok quality fallback.");

                results.Add(result);
            }

            return results;
        }

        private static ArrowLevelData BuildLayeredRectangle(
            string levelId,
            int width,
            int height,
            int targetChains,
            float targetCoverage,
            ArrowDifficultyBand difficulty,
            ArrowLevelFamily family,
            Random rng)
        {
            width = Math.Max(3, width);
            height = Math.Max(3, height);
            targetChains = Math.Max(1, targetChains);

            var level = new ArrowLevelData(levelId, width, height)
            {
                difficulty = difficulty,
                family = family
            };

            var occupied = new bool[level.Area];
            int targetCells = Clamp((int)Math.Round(level.Area * Clamp01(targetCoverage)), 2, level.Area);
            int maxLayers = Math.Max(1, Math.Min(width, height) / 2);
            int targetAverageLength = Clamp(level.Area / Math.Max(1, targetChains), 2, FamilyMaxLength(family, width, height));
            SeedOuterFrame(level, occupied);

            for (int layer = 1; layer < maxLayers && level.chains.Count < targetChains && CountOccupied(occupied) < targetCells; layer++)
            {
                var heads = BuildLayerHeads(width, height, layer);
                Shuffle(heads, rng);

                for (int i = 0; i < heads.Count && level.chains.Count < targetChains && CountOccupied(occupied) < targetCells; i++)
                {
                    HeadCandidate head = heads[i];
                    int desiredLength = SampleLength(family, targetAverageLength, rng);
                    if (TryBuildChain(level, occupied, head, desiredLength, family, rng, out ArrowChainData chain))
                    {
                        AddChain(level, occupied, chain);
                    }
                }
            }

            FillRemainingUsefulCells(level, occupied, targetChains, targetCells, family, rng);
            level.metadata["generator"] = nameof(DirectRectangleArrowLevelGenerator);
            level.metadata["targetChains"] = targetChains.ToString();
            level.metadata["targetCoverage"] = targetCoverage.ToString("0.000");
            return level;
        }

        private static bool TryBuildChain(
            ArrowLevelData level,
            bool[] occupied,
            HeadCandidate head,
            int desiredLength,
            ArrowLevelFamily family,
            Random rng,
            out ArrowChainData chain)
        {
            chain = null;

            if (!level.InBounds(head.X, head.Y))
                return false;

            int headIndex = level.Index(head.X, head.Y);
            if (occupied[headIndex])
                return false;

            ArrowDirectionUtility.ToDelta(ArrowDirectionUtility.Opposite(head.OutDir), out int inwardDx, out int inwardDy);
            int secondX = head.X + inwardDx;
            int secondY = head.Y + inwardDy;
            if (!level.InBounds(secondX, secondY))
                return false;

            int secondIndex = level.Index(secondX, secondY);
            if (occupied[secondIndex])
                return false;

            var path = new List<Cell>(Math.Max(2, desiredLength))
            {
                new Cell(head.X, head.Y),
                new Cell(secondX, secondY)
            };
            var local = new HashSet<int> { headIndex, secondIndex };
            int headLayer = LayerOf(head.X, head.Y, level.width, level.height);
            Cell previous = path[0];
            Cell current = path[1];

            while (path.Count < desiredLength)
            {
                var options = new List<CellStep>(8);
                AddStepOption(level, occupied, local, options, current.X + 1, current.Y, current, previous, headLayer, family);
                AddStepOption(level, occupied, local, options, current.X - 1, current.Y, current, previous, headLayer, family);
                AddStepOption(level, occupied, local, options, current.X, current.Y + 1, current, previous, headLayer, family);
                AddStepOption(level, occupied, local, options, current.X, current.Y - 1, current, previous, headLayer, family);

                if (options.Count == 0)
                    break;

                options.Sort((a, b) => b.Score.CompareTo(a.Score));
                int pickWindow = Math.Min(options.Count, family == ArrowLevelFamily.Dense ? 2 : 4);
                CellStep picked = options[rng.Next(pickWindow)];
                previous = current;
                current = picked.Cell;
                path.Add(current);
                local.Add(level.Index(current.X, current.Y));

                if (path.Count >= 2 && path.Count >= desiredLength - 2 && rng.NextDouble() < StopChance(family, path.Count, desiredLength))
                    break;
            }

            if (path.Count < 2)
                return false;

            chain = new ArrowChainData();
            for (int i = 0; i < path.Count; i++)
                chain.indices.Add(level.Index(path[i].X, path[i].Y));
            chain.colorIndex = Math.Abs(head.X * 17 + head.Y * 31 + path.Count) % 6;
            return true;
        }

        private static void AddStepOption(
            ArrowLevelData level,
            bool[] occupied,
            HashSet<int> local,
            List<CellStep> options,
            int x,
            int y,
            Cell current,
            Cell previous,
            int headLayer,
            ArrowLevelFamily family)
        {
            if (!level.InBounds(x, y))
                return;

            int index = level.Index(x, y);
            if (occupied[index] || local.Contains(index))
                return;

            int layer = LayerOf(x, y, level.width, level.height);
            if (layer < headLayer)
                return;

            int dx = x - current.X;
            int dy = y - current.Y;
            int pdx = current.X - previous.X;
            int pdy = current.Y - previous.Y;
            bool turn = dx != pdx || dy != pdy;

            int score = 100;
            score += layer > headLayer ? 14 : 2;
            score += SparseNeighborBonus(level, occupied, local, x, y);

            switch (family)
            {
                case ArrowLevelFamily.Maze:
                    score += turn ? 26 : 4;
                    break;
                case ArrowLevelFamily.Sweep:
                    score += turn ? -8 : 28;
                    break;
                case ArrowLevelFamily.Dense:
                    score += turn ? 12 : 8;
                    score += layer <= headLayer + 1 ? 8 : 0;
                    break;
                case ArrowLevelFamily.Lock:
                    score += CenterBias(level, x, y);
                    score += turn ? 16 : 6;
                    break;
                case ArrowLevelFamily.Section:
                    score += Math.Abs(x - level.width / 2) > level.width / 5 ? 10 : 0;
                    score += turn ? 10 : 10;
                    break;
                case ArrowLevelFamily.Shell:
                    score += layer <= headLayer + 2 ? 18 : 0;
                    break;
                default:
                    score += turn ? 10 : 10;
                    break;
            }

            options.Add(new CellStep(new Cell(x, y), score));
        }

        private static void FillRemainingUsefulCells(
            ArrowLevelData level,
            bool[] occupied,
            int targetChains,
            int targetCells,
            ArrowLevelFamily family,
            Random rng)
        {
            int guard = level.Area * 2;
            while (guard-- > 0 && level.chains.Count < targetChains && CountOccupied(occupied) < targetCells)
            {
                var heads = BuildAllPotentialHeads(level.width, level.height);
                Shuffle(heads, rng);

                bool added = false;
                for (int i = 0; i < heads.Count; i++)
                {
                    int desired = SampleLength(family, 3, rng);
                    if (TryBuildChain(level, occupied, heads[i], desired, family, rng, out ArrowChainData chain))
                    {
                        AddChain(level, occupied, chain);
                        added = true;
                        break;
                    }
                }

                if (!added)
                    break;
            }
        }

        private static void SeedOuterFrame(ArrowLevelData level, bool[] occupied)
        {
            if (level.width < 4 || level.height < 4)
                return;

            int left = 0;
            int right = level.width - 1;
            int bottom = 0;
            int top = level.height - 1;

            AddLineSegments(level, occupied, left, top, 1, 0, right - left + 1, 7);
            AddLineSegments(level, occupied, right, bottom, -1, 0, right - left + 1, 7);

            if (top - bottom > 2)
            {
                AddLineSegments(level, occupied, left, top - 1, 0, -1, top - bottom - 1, 7);
                AddLineSegments(level, occupied, right, bottom + 1, 0, 1, top - bottom - 1, 7);
            }
        }

        private static void AddLineSegments(
            ArrowLevelData level,
            bool[] occupied,
            int startX,
            int startY,
            int dx,
            int dy,
            int totalCells,
            int maxSegmentLength)
        {
            int offset = 0;
            while (offset < totalCells)
            {
                int remaining = totalCells - offset;
                int length = Math.Min(Math.Max(2, maxSegmentLength), remaining);
                if (remaining > maxSegmentLength && remaining - maxSegmentLength == 1)
                    length = maxSegmentLength - 1;
                if (length < 2)
                    break;

                var encoded = new List<int>(length);
                for (int i = 0; i < length; i++)
                {
                    int x = startX + (offset + i) * dx;
                    int y = startY + (offset + i) * dy;
                    encoded.Add(x + y * 100000);
                }

                AddFrameChain(level, occupied, encoded);
                offset += length;
            }
        }

        private static void AddFrameChain(ArrowLevelData level, bool[] occupied, List<int> encodedPath)
        {
            if (encodedPath == null || encodedPath.Count < 2)
                return;

            var chain = new ArrowChainData();
            for (int i = 0; i < encodedPath.Count; i++)
            {
                int x = encodedPath[i] % 100000;
                int y = encodedPath[i] / 100000;
                if (!level.InBounds(x, y))
                    return;

                int index = level.Index(x, y);
                if (occupied[index])
                    return;

                chain.indices.Add(index);
            }

            chain.colorIndex = Math.Abs(chain.indices[0] + chain.indices.Count) % 6;
            AddChain(level, occupied, chain);
        }

        private static List<HeadCandidate> BuildLayerHeads(int width, int height, int layer)
        {
            var heads = new List<HeadCandidate>();
            int left = layer;
            int right = width - 1 - layer;
            int bottom = layer;
            int top = height - 1 - layer;

            if (left > right || bottom > top)
                return heads;

            for (int x = left; x <= right; x++)
            {
                heads.Add(new HeadCandidate(x, top, ArrowDirection.Up));
                if (bottom != top)
                    heads.Add(new HeadCandidate(x, bottom, ArrowDirection.Down));
            }

            for (int y = bottom + 1; y <= top - 1; y++)
            {
                heads.Add(new HeadCandidate(left, y, ArrowDirection.Left));
                if (left != right)
                    heads.Add(new HeadCandidate(right, y, ArrowDirection.Right));
            }

            return heads;
        }

        private static List<HeadCandidate> BuildAllPotentialHeads(int width, int height)
        {
            var heads = new List<HeadCandidate>(width * height);
            int maxLayer = Math.Max(1, Math.Min(width, height) / 2);
            for (int layer = 0; layer <= maxLayer; layer++)
                heads.AddRange(BuildLayerHeads(width, height, layer));
            return heads;
        }

        private static void AddChain(ArrowLevelData level, bool[] occupied, ArrowChainData chain)
        {
            level.chains.Add(chain);
            for (int i = 0; i < chain.indices.Count; i++)
                occupied[chain.indices[i]] = true;
        }

        private static ArrowLevelFamily PickFamily(ArrowGenerationRequest request, int index, Random rng)
        {
            if (request.families != null && request.families.Count > 0)
                return request.families[index % request.families.Count];

            ArrowLevelFamily[] defaults =
            {
                ArrowLevelFamily.Shell,
                ArrowLevelFamily.Section,
                ArrowLevelFamily.Lock,
                ArrowLevelFamily.Maze,
                ArrowLevelFamily.Dense,
                ArrowLevelFamily.Sweep
            };
            return defaults[rng.Next(defaults.Length)];
        }

        private static int SampleLength(ArrowLevelFamily family, int targetAverageLength, Random rng)
        {
            int min = 2;
            int max = Math.Max(2, targetAverageLength + 3);
            switch (family)
            {
                case ArrowLevelFamily.Maze:
                    max += 5;
                    break;
                case ArrowLevelFamily.Sweep:
                    max += 7;
                    break;
                case ArrowLevelFamily.Dense:
                    max = Math.Max(5, targetAverageLength + 6);
                    break;
                case ArrowLevelFamily.Lock:
                    max += 3;
                    break;
            }

            return Range(rng, min, Math.Max(min, max));
        }

        private static int FamilyMaxLength(ArrowLevelFamily family, int width, int height)
        {
            int baseMax = Math.Max(4, Math.Min(width, height));
            switch (family)
            {
                case ArrowLevelFamily.Maze:
                case ArrowLevelFamily.Sweep:
                    return baseMax + Math.Max(width, height) / 2;
                case ArrowLevelFamily.Dense:
                    return Math.Max(4, baseMax / 2);
                default:
                    return baseMax;
            }
        }

        private static double StopChance(ArrowLevelFamily family, int length, int desired)
        {
            if (family == ArrowLevelFamily.Maze || family == ArrowLevelFamily.Sweep)
                return length >= desired ? 0.55 : 0.04;
            if (family == ArrowLevelFamily.Dense)
                return length >= desired ? 0.34 : 0.03;
            return length >= desired ? 0.62 : 0.08;
        }

        private static int SparseNeighborBonus(ArrowLevelData level, bool[] occupied, HashSet<int> local, int x, int y)
        {
            int filled = 0;
            CountNeighbor(level, occupied, local, x + 1, y, ref filled);
            CountNeighbor(level, occupied, local, x - 1, y, ref filled);
            CountNeighbor(level, occupied, local, x, y + 1, ref filled);
            CountNeighbor(level, occupied, local, x, y - 1, ref filled);
            return (4 - filled) * 5;
        }

        private static void CountNeighbor(ArrowLevelData level, bool[] occupied, HashSet<int> local, int x, int y, ref int filled)
        {
            if (!level.InBounds(x, y))
            {
                filled++;
                return;
            }

            int index = level.Index(x, y);
            if (occupied[index] || local.Contains(index))
                filled++;
        }

        private static int CenterBias(ArrowLevelData level, int x, int y)
        {
            float cx = (level.width - 1) * 0.5f;
            float cy = (level.height - 1) * 0.5f;
            float dx = Math.Abs(x - cx) / Math.Max(1f, cx);
            float dy = Math.Abs(y - cy) / Math.Max(1f, cy);
            return (int)Math.Round((1f - Math.Min(1f, (dx + dy) * 0.5f)) * 18f);
        }

        private static int CountOccupied(bool[] occupied)
        {
            int count = 0;
            for (int i = 0; i < occupied.Length; i++)
                if (occupied[i])
                    count++;
            return count;
        }

        private static int LayerOf(int x, int y, int width, int height)
        {
            return Math.Min(Math.Min(x, y), Math.Min(width - 1 - x, height - 1 - y));
        }

        private static int Range(Random rng, int minInclusive, int maxInclusive)
        {
            if (maxInclusive < minInclusive)
            {
                int tmp = minInclusive;
                minInclusive = maxInclusive;
                maxInclusive = tmp;
            }

            return rng.Next(minInclusive, maxInclusive + 1);
        }

        private static int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }

        private static float Clamp01(float value)
        {
            if (value < 0f)
                return 0f;
            if (value > 1f)
                return 1f;
            return value;
        }

        private static void Shuffle<T>(List<T> values, Random rng)
        {
            for (int i = values.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                T temp = values[i];
                values[i] = values[j];
                values[j] = temp;
            }
        }

        private readonly struct Cell
        {
            public readonly int X;
            public readonly int Y;

            public Cell(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        private readonly struct CellStep
        {
            public readonly Cell Cell;
            public readonly int Score;

            public CellStep(Cell cell, int score)
            {
                Cell = cell;
                Score = score;
            }
        }

        private readonly struct HeadCandidate
        {
            public readonly int X;
            public readonly int Y;
            public readonly ArrowDirection OutDir;

            public HeadCandidate(int x, int y, ArrowDirection outDir)
            {
                X = x;
                Y = y;
                OutDir = outDir;
            }
        }
    }
}
