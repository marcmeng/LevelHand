using System;
using System.Collections.Generic;

namespace PixelBug.ArrowLevelGenerator
{
    public sealed class ArrowLevelValidationReport
    {
        public bool isValid = true;
        public readonly List<string> errors = new List<string>();
        public readonly List<string> warnings = new List<string>();

        public void Error(string message)
        {
            isValid = false;
            errors.Add(message);
        }
    }

    public static class ArrowLevelValidator
    {
        public static ArrowLevelValidationReport Validate(ArrowLevelData level)
        {
            var report = new ArrowLevelValidationReport();
            if (level == null)
            {
                report.Error("level is null");
                return report;
            }

            if (level.width <= 0 || level.height <= 0)
                report.Error($"invalid board size {level.width}x{level.height}");

            int area = Math.Max(0, level.width * level.height);
            var occupied = new int[Math.Max(0, area)];
            for (int i = 0; i < occupied.Length; i++)
                occupied[i] = -1;

            var blocks = new HashSet<int>();
            if (level.blockIndices != null)
            {
                for (int i = 0; i < level.blockIndices.Count; i++)
                {
                    int index = level.blockIndices[i];
                    if (!InBoundsIndex(index, area))
                    {
                        report.Error($"block index out of bounds: {index}");
                        continue;
                    }

                    if (!blocks.Add(index))
                        report.Error($"duplicate block index: {index}");
                }
            }

            if (level.chains == null || level.chains.Count == 0)
            {
                report.Error("level has no chains");
                return report;
            }

            for (int c = 0; c < level.chains.Count; c++)
            {
                ArrowChainData chain = level.chains[c];
                ValidateChain(level, chain, c, area, blocks, occupied, report);
            }

            return report;
        }

        private static void ValidateChain(
            ArrowLevelData level,
            ArrowChainData chain,
            int chainIndex,
            int area,
            HashSet<int> blocks,
            int[] occupied,
            ArrowLevelValidationReport report)
        {
            if (chain == null)
            {
                report.Error($"chain {chainIndex} is null");
                return;
            }

            if (chain.indices == null || chain.indices.Count < 2)
            {
                report.Error($"chain {chainIndex} has fewer than 2 cells");
                return;
            }

            var local = new HashSet<int>();
            for (int i = 0; i < chain.indices.Count; i++)
            {
                int index = chain.indices[i];
                if (!InBoundsIndex(index, area))
                {
                    report.Error($"chain {chainIndex} index out of bounds: {index}");
                    continue;
                }

                if (!local.Add(index))
                    report.Error($"chain {chainIndex} repeats cell {index}");

                if (blocks.Contains(index))
                    report.Error($"chain {chainIndex} overlaps block cell {index}");

                if (occupied[index] >= 0)
                    report.Error($"chain {chainIndex} overlaps chain {occupied[index]} at cell {index}");
                else
                    occupied[index] = chainIndex;
            }

            for (int i = 1; i < chain.indices.Count; i++)
            {
                if (!InBoundsIndex(chain.indices[i - 1], area) || !InBoundsIndex(chain.indices[i], area))
                    continue;

                level.ToXY(chain.indices[i - 1], out int ax, out int ay);
                level.ToXY(chain.indices[i], out int bx, out int by);
                int distance = Math.Abs(ax - bx) + Math.Abs(ay - by);
                if (distance != 1)
                    report.Error($"chain {chainIndex} cells {i - 1}->{i} are not adjacent");
            }

            if (!GreedyEscapeSolver.TryGetHeadOut(level, chain, out int hx, out int hy, out ArrowDirection outDir))
            {
                report.Error($"chain {chainIndex} has invalid head direction");
                return;
            }

            if (HeadRayHitsSelf(level, chain, hx, hy, outDir))
                report.Error($"chain {chainIndex} head escape ray hits itself");
        }

        private static bool HeadRayHitsSelf(ArrowLevelData level, ArrowChainData chain, int hx, int hy, ArrowDirection outDir)
        {
            var self = new HashSet<int>(chain.indices);
            ArrowDirectionUtility.ToDelta(outDir, out int dx, out int dy);
            int x = hx + dx;
            int y = hy + dy;

            while (level.InBounds(x, y))
            {
                if (self.Contains(level.Index(x, y)))
                    return true;

                x += dx;
                y += dy;
            }

            return false;
        }

        private static bool InBoundsIndex(int index, int area)
        {
            return index >= 0 && index < area;
        }
    }
}
