using System;
using System.Collections.Generic;

namespace PixelBug.ArrowLevelGenerator
{
    public sealed class ArrowSolveReport
    {
        public bool solved;
        public int totalChains;
        public int removedChains;
        public int initialOpeners;
        public float averageChoices;
        public int maxChoices;
        public int lowChoiceSteps;
        public readonly List<int> solveOrder = new List<int>();
        public readonly List<int> stuckChains = new List<int>();
    }

    public interface IArrowLevelSolver
    {
        ArrowSolveReport Solve(ArrowLevelData level);
        bool CanExit(ArrowLevelData level, int chainIndex, bool[] activeChains, int[] occupancy);
    }

    public sealed class GreedyEscapeSolver : IArrowLevelSolver
    {
        public ArrowSolveReport Solve(ArrowLevelData level)
        {
            var report = new ArrowSolveReport();
            if (level == null)
                return report;

            report.totalChains = level.chains.Count;
            if (!TryBuildOccupancy(level, out int[] occupancy, out bool[] activeChains))
            {
                report.stuckChains.AddRange(AllChainIndices(level));
                return report;
            }

            int choiceSum = 0;
            var available = new List<int>(level.chains.Count);

            for (int step = 0; step < level.chains.Count; step++)
            {
                available.Clear();
                for (int i = 0; i < level.chains.Count; i++)
                {
                    if (activeChains[i] && CanExit(level, i, activeChains, occupancy))
                        available.Add(i);
                }

                if (step == 0)
                    report.initialOpeners = available.Count;

                if (available.Count == 0)
                    break;

                choiceSum += available.Count;
                report.maxChoices = Math.Max(report.maxChoices, available.Count);
                if (available.Count <= 2)
                    report.lowChoiceSteps++;

                int picked = PickGreedyChain(level, available);
                RemoveChain(level, picked, activeChains, occupancy);
                report.solveOrder.Add(picked);
            }

            report.removedChains = report.solveOrder.Count;
            report.solved = report.removedChains == level.chains.Count;
            report.averageChoices = report.removedChains > 0 ? choiceSum / (float)report.removedChains : 0f;

            if (!report.solved)
            {
                for (int i = 0; i < level.chains.Count; i++)
                    if (activeChains[i])
                        report.stuckChains.Add(i);
            }

            return report;
        }

        public bool CanExit(ArrowLevelData level, int chainIndex, bool[] activeChains, int[] occupancy)
        {
            if (level == null || chainIndex < 0 || chainIndex >= level.chains.Count)
                return false;

            if (activeChains != null && !activeChains[chainIndex])
                return false;

            var chain = level.chains[chainIndex];
            if (chain == null || chain.indices.Count < 2)
                return false;

            if (!TryGetHeadOut(level, chain, out int x, out int y, out ArrowDirection outDir))
                return false;

            ArrowDirectionUtility.ToDelta(outDir, out int dx, out int dy);
            x += dx;
            y += dy;

            var blockSet = BuildBlockSet(level);
            while (level.InBounds(x, y))
            {
                int index = level.Index(x, y);
                if (blockSet.Contains(index))
                    return false;

                int owner = occupancy != null && index >= 0 && index < occupancy.Length ? occupancy[index] : -1;
                if (owner >= 0 && (activeChains == null || activeChains[owner]))
                    return false;

                x += dx;
                y += dy;
            }

            return true;
        }

        public static bool TryGetHeadOut(ArrowLevelData level, ArrowChainData chain, out int x, out int y, out ArrowDirection outDir)
        {
            x = 0;
            y = 0;
            outDir = ArrowDirection.Up;

            if (level == null || chain == null || chain.indices.Count < 2)
                return false;

            int head = chain.indices[0];
            int next = chain.indices[1];
            if (head < 0 || head >= level.Area || next < 0 || next >= level.Area)
                return false;

            level.ToXY(head, out int hx, out int hy);
            level.ToXY(next, out int nx, out int ny);
            int dx = hx - nx;
            int dy = hy - ny;
            if (!ArrowDirectionUtility.TryFromDelta(dx, dy, out outDir))
                return false;

            x = hx;
            y = hy;
            return true;
        }

        private static int PickGreedyChain(ArrowLevelData level, List<int> available)
        {
            int best = available[0];
            int bestScore = int.MinValue;
            for (int i = 0; i < available.Count; i++)
            {
                int chainIndex = available[i];
                var chain = level.chains[chainIndex];
                int score = 0;
                if (chain != null && chain.indices.Count > 0)
                {
                    int head = chain.indices[0];
                    level.ToXY(head, out int x, out int y);
                    if (x == 0 || y == 0 || x == level.width - 1 || y == level.height - 1)
                        score += 1000;
                    score += Math.Min(200, chain.indices.Count * 8);
                }
                score -= chainIndex;

                if (score > bestScore)
                {
                    bestScore = score;
                    best = chainIndex;
                }
            }

            return best;
        }

        private static bool TryBuildOccupancy(ArrowLevelData level, out int[] occupancy, out bool[] activeChains)
        {
            occupancy = new int[level.Area];
            activeChains = new bool[level.chains.Count];
            for (int i = 0; i < occupancy.Length; i++)
                occupancy[i] = -1;

            for (int c = 0; c < level.chains.Count; c++)
            {
                var chain = level.chains[c];
                if (chain == null || chain.indices.Count < 2)
                    return false;

                activeChains[c] = true;
                for (int i = 0; i < chain.indices.Count; i++)
                {
                    int index = chain.indices[i];
                    if (index < 0 || index >= occupancy.Length)
                        return false;
                    if (occupancy[index] >= 0)
                        return false;

                    occupancy[index] = c;
                }
            }

            return true;
        }

        private static void RemoveChain(ArrowLevelData level, int chainIndex, bool[] activeChains, int[] occupancy)
        {
            activeChains[chainIndex] = false;
            var chain = level.chains[chainIndex];
            for (int i = 0; i < chain.indices.Count; i++)
            {
                int index = chain.indices[i];
                if (index >= 0 && index < occupancy.Length && occupancy[index] == chainIndex)
                    occupancy[index] = -1;
            }
        }

        private static HashSet<int> BuildBlockSet(ArrowLevelData level)
        {
            var set = new HashSet<int>();
            if (level.blockIndices == null)
                return set;

            for (int i = 0; i < level.blockIndices.Count; i++)
                set.Add(level.blockIndices[i]);
            return set;
        }

        private static IEnumerable<int> AllChainIndices(ArrowLevelData level)
        {
            for (int i = 0; i < level.chains.Count; i++)
                yield return i;
        }
    }
}
