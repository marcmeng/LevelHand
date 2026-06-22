using System;
using System.Collections.Generic;

namespace PixelBug.ArrowLevelGenerator
{
    [Serializable]
    public sealed class ArrowCampaignBucketTarget
    {
        public ArrowDifficultyBand difficulty;
        public int count;
        public ArrowQualityPolicy qualityPolicy;
    }

    [Serializable]
    public sealed class ArrowCampaignPlanRequest
    {
        public readonly List<ArrowCampaignBucketTarget> buckets = new List<ArrowCampaignBucketTarget>();
        public int maxFamilyUse = 10;
        public bool requireOkQuality = true;
    }

    public sealed class ArrowCampaignPlan
    {
        public readonly List<ArrowGenerationResult> selected = new List<ArrowGenerationResult>();
        public readonly List<string> warnings = new List<string>();
    }

    public sealed class ArrowCampaignPlanner
    {
        private readonly ArrowLevelQualityEvaluator evaluator;

        public ArrowCampaignPlanner()
            : this(new ArrowLevelQualityEvaluator())
        {
        }

        public ArrowCampaignPlanner(ArrowLevelQualityEvaluator evaluator)
        {
            this.evaluator = evaluator ?? new ArrowLevelQualityEvaluator();
        }

        public ArrowCampaignPlan Select(IReadOnlyList<ArrowGenerationResult> candidates, ArrowCampaignPlanRequest request)
        {
            var plan = new ArrowCampaignPlan();
            if (candidates == null || request == null)
                return plan;

            var familyUse = new Dictionary<ArrowLevelFamily, int>();
            var usedIds = new HashSet<string>();

            for (int b = 0; b < request.buckets.Count; b++)
            {
                ArrowCampaignBucketTarget bucket = request.buckets[b];
                int before = plan.selected.Count;

                var matching = new List<ArrowGenerationResult>();
                for (int i = 0; i < candidates.Count; i++)
                {
                    ArrowGenerationResult result = candidates[i];
                    if (result == null || result.level == null)
                        continue;
                    if (result.level.difficulty != bucket.difficulty)
                        continue;
                    if (usedIds.Contains(result.level.levelId))
                        continue;

                    EnsureMetrics(result, bucket.qualityPolicy);
                    if (request.requireOkQuality && result.metrics.qualityFlags != "ok")
                        continue;

                    matching.Add(result);
                }

                matching.Sort(CompareByScoreDescending);

                for (int i = 0; i < matching.Count && plan.selected.Count - before < bucket.count; i++)
                {
                    ArrowGenerationResult result = matching[i];
                    ArrowLevelFamily family = result.level.family;
                    int currentUse = familyUse.TryGetValue(family, out int value) ? value : 0;
                    if (currentUse >= request.maxFamilyUse)
                        continue;

                    familyUse[family] = currentUse + 1;
                    usedIds.Add(result.level.levelId);
                    plan.selected.Add(result);
                }

                int picked = plan.selected.Count - before;
                if (picked < bucket.count)
                    plan.warnings.Add($"Bucket {bucket.difficulty} selected {picked}/{bucket.count}.");
            }

            return plan;
        }

        private void EnsureMetrics(ArrowGenerationResult result, ArrowQualityPolicy policy)
        {
            if (result.metrics == null)
                result.metrics = evaluator.Evaluate(result.level, policy);
        }

        private static int CompareByScoreDescending(ArrowGenerationResult a, ArrowGenerationResult b)
        {
            float sa = a?.metrics != null ? a.metrics.playabilityScore : float.MinValue;
            float sb = b?.metrics != null ? b.metrics.playabilityScore : float.MinValue;
            int cmp = sb.CompareTo(sa);
            if (cmp != 0)
                return cmp;

            string ai = a?.level?.levelId ?? "";
            string bi = b?.level?.levelId ?? "";
            return string.CompareOrdinal(ai, bi);
        }
    }
}
