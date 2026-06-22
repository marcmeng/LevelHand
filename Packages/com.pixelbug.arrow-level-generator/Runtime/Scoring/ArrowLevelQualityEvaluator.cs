using System;
using System.Collections.Generic;

namespace PixelBug.ArrowLevelGenerator
{
    public sealed class ArrowLevelMetrics
    {
        public int width;
        public int height;
        public int chains;
        public int occupiedCells;
        public float coverage;
        public float outerBandCoverage;
        public int edgeShortChains;
        public int maxStraightRun;
        public int maxChainLength;
        public float averageChainLength;
        public int initialOpeners;
        public float playabilityScore;
        public string qualityFlags = "ok";
    }

    public sealed class ArrowLevelQualityEvaluator
    {
        private readonly IArrowLevelSolver solver;

        public ArrowLevelQualityEvaluator()
            : this(new GreedyEscapeSolver())
        {
        }

        public ArrowLevelQualityEvaluator(IArrowLevelSolver solver)
        {
            this.solver = solver ?? new GreedyEscapeSolver();
        }

        public ArrowLevelMetrics Evaluate(ArrowLevelData level, ArrowQualityPolicy policy = null)
        {
            policy = policy ?? new ArrowQualityPolicy();
            var metrics = new ArrowLevelMetrics();
            if (level == null)
            {
                metrics.qualityFlags = "null-level";
                return metrics;
            }

            ArrowLevelValidationReport validation = ArrowLevelValidator.Validate(level);
            if (!validation.isValid)
            {
                metrics.width = level.width;
                metrics.height = level.height;
                metrics.chains = level.chains != null ? level.chains.Count : 0;
                metrics.qualityFlags = "invalid-geometry";
                return metrics;
            }

            metrics.width = level.width;
            metrics.height = level.height;
            metrics.chains = level.chains.Count;

            var occupied = BuildOccupiedSet(level);
            metrics.occupiedCells = occupied.Count;
            metrics.coverage = occupied.Count / (float)Math.Max(1, level.Area);
            metrics.outerBandCoverage = ComputeOuterBandCoverage(level, occupied);
            ComputeChainShapeMetrics(level, occupied, metrics);

            ArrowSolveReport solve = solver.Solve(level);
            metrics.initialOpeners = solve.initialOpeners;
            metrics.playabilityScore = Score(level, metrics, solve);
            metrics.qualityFlags = BuildQualityFlags(metrics, solve, policy);
            return metrics;
        }

        private static HashSet<int> BuildOccupiedSet(ArrowLevelData level)
        {
            var occupied = new HashSet<int>();
            for (int c = 0; c < level.chains.Count; c++)
            {
                var chain = level.chains[c];
                if (chain == null)
                    continue;
                for (int i = 0; i < chain.indices.Count; i++)
                    occupied.Add(chain.indices[i]);
            }

            if (level.blockIndices != null)
            {
                for (int i = 0; i < level.blockIndices.Count; i++)
                    occupied.Add(level.blockIndices[i]);
            }

            return occupied;
        }

        private static float ComputeOuterBandCoverage(ArrowLevelData level, HashSet<int> occupied)
        {
            int outerCells = 0;
            int outerOccupied = 0;
            for (int y = 0; y < level.height; y++)
            for (int x = 0; x < level.width; x++)
            {
                bool outer = x == 0 || y == 0 || x == level.width - 1 || y == level.height - 1;
                if (!outer)
                    continue;

                outerCells++;
                if (occupied.Contains(level.Index(x, y)))
                    outerOccupied++;
            }

            return outerCells > 0 ? outerOccupied / (float)outerCells : 1f;
        }

        private static void ComputeChainShapeMetrics(ArrowLevelData level, HashSet<int> occupied, ArrowLevelMetrics metrics)
        {
            int lengthSum = 0;
            int maxStraight = 0;

            for (int c = 0; c < level.chains.Count; c++)
            {
                var chain = level.chains[c];
                if (chain == null)
                    continue;

                int length = chain.indices.Count;
                lengthSum += length;
                metrics.maxChainLength = Math.Max(metrics.maxChainLength, length);

                if (length <= 2 && TouchesOuterBand(level, chain))
                    metrics.edgeShortChains++;

                maxStraight = Math.Max(maxStraight, LongestStraightRun(level, chain));
            }

            metrics.maxStraightRun = maxStraight;
            metrics.averageChainLength = level.chains.Count > 0 ? lengthSum / (float)level.chains.Count : 0f;
        }

        private static bool TouchesOuterBand(ArrowLevelData level, ArrowChainData chain)
        {
            for (int i = 0; i < chain.indices.Count; i++)
            {
                level.ToXY(chain.indices[i], out int x, out int y);
                if (x == 0 || y == 0 || x == level.width - 1 || y == level.height - 1)
                    return true;
            }

            return false;
        }

        private static int LongestStraightRun(ArrowLevelData level, ArrowChainData chain)
        {
            if (chain == null || chain.indices.Count < 2)
                return 0;

            int best = 1;
            int current = 1;
            int prevDx = 0;
            int prevDy = 0;

            for (int i = 1; i < chain.indices.Count; i++)
            {
                level.ToXY(chain.indices[i - 1], out int ax, out int ay);
                level.ToXY(chain.indices[i], out int bx, out int by);
                int dx = bx - ax;
                int dy = by - ay;
                if (i > 1 && dx == prevDx && dy == prevDy)
                    current++;
                else
                    current = 2;

                best = Math.Max(best, current);
                prevDx = dx;
                prevDy = dy;
            }

            return best;
        }

        private static float Score(ArrowLevelData level, ArrowLevelMetrics metrics, ArrowSolveReport solve)
        {
            float score = 0f;
            score += metrics.coverage * 90f;
            score += metrics.outerBandCoverage * 45f;
            score += Math.Min(45f, solve.averageChoices * 7f);
            score += Math.Min(35f, solve.removedChains * 0.12f);
            score -= Math.Max(0, metrics.edgeShortChains - 8) * 2.5f;
            score -= Math.Max(0, metrics.maxStraightRun - 16) * 2f;
            score -= solve.solved ? 0f : 100f;
            return score;
        }

        private static string BuildQualityFlags(ArrowLevelMetrics metrics, ArrowSolveReport solve, ArrowQualityPolicy policy)
        {
            var flags = new List<string>();
            if (!solve.solved)
                flags.Add("greedy-fail");
            if (metrics.coverage < policy.minCoverage)
                flags.Add("low-coverage");
            if (metrics.outerBandCoverage < policy.minOuterBandCoverage)
                flags.Add("weak-outer");
            if (metrics.initialOpeners > policy.maxInitialOpeners)
                flags.Add("too-open");
            if (metrics.edgeShortChains > policy.maxEdgeShortChains)
                flags.Add("edge-short-heavy");
            if (metrics.maxStraightRun > policy.maxLongStraightRun)
                flags.Add("long-straight-run");

            return flags.Count == 0 ? "ok" : string.Join("|", flags);
        }
    }
}
