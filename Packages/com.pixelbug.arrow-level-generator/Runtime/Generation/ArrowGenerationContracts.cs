using System;
using System.Collections.Generic;

namespace PixelBug.ArrowLevelGenerator
{
    [Serializable]
    public sealed class ArrowGenerationRequest
    {
        public string levelIdPrefix = "generated_arrow";
        public int seed = 12345;
        public int count = 1;
        public int minWidth = 15;
        public int maxWidth = 22;
        public int minHeight = 22;
        public int maxHeight = 32;
        public int minChains = 30;
        public int maxChains = 80;
        public float targetCoverage = 0.96f;
        public ArrowDifficultyBand difficulty = ArrowDifficultyBand.Normal;
        public readonly List<ArrowLevelFamily> families = new List<ArrowLevelFamily>();
        public ArrowQualityPolicy quality = new ArrowQualityPolicy();
        public bool requireGreedySolvable = true;
    }

    [Serializable]
    public sealed class ArrowQualityPolicy
    {
        public float minCoverage = 0.90f;
        public float minOuterBandCoverage = 0.92f;
        public int maxInitialOpeners = 32;
        public int maxEdgeShortChains = 10;
        public int maxLongStraightRun = 14;
        public int maxFamilyUse = 10;

        public static ArrowQualityPolicy ForDifficulty(ArrowDifficultyBand difficulty)
        {
            var policy = new ArrowQualityPolicy();
            switch (difficulty)
            {
                case ArrowDifficultyBand.Refresh:
                    policy.minCoverage = 0.92f;
                    policy.minOuterBandCoverage = 0.94f;
                    policy.maxInitialOpeners = 20;
                    policy.maxLongStraightRun = 12;
                    return policy;
                case ArrowDifficultyBand.Hard:
                    policy.minCoverage = 0.93f;
                    policy.minOuterBandCoverage = 0.94f;
                    policy.maxInitialOpeners = 26;
                    return policy;
                case ArrowDifficultyBand.VeryHard:
                    policy.minCoverage = 0.94f;
                    policy.minOuterBandCoverage = 0.95f;
                    policy.maxInitialOpeners = 28;
                    return policy;
                case ArrowDifficultyBand.Extreme:
                    policy.minCoverage = 0.95f;
                    policy.minOuterBandCoverage = 0.96f;
                    policy.maxInitialOpeners = 34;
                    policy.maxLongStraightRun = 18;
                    return policy;
                default:
                    return policy;
            }
        }
    }

    public sealed class ArrowGenerationResult
    {
        public ArrowLevelData level;
        public ArrowLevelMetrics metrics;
        public ArrowSolveReport solveReport;
        public bool success;
        public string status = "";
        public readonly List<string> warnings = new List<string>();
    }

    public interface IArrowLevelGenerator
    {
        IReadOnlyList<ArrowGenerationResult> Generate(ArrowGenerationRequest request);
    }
}
