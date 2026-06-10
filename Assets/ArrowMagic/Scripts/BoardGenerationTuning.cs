using System;
using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public static class BoardGenerationTuning
    {
        public const int CandidateSeedStride = 200;

        public readonly struct ExperienceStats
        {
            public readonly int MinChoices;
            public readonly float AverageChoices;
            public readonly int BottleneckStepCount;
            public readonly int ForcedMoveStepCount;
            public readonly int SampledStepCount;
            public readonly int[] ChoiceWave;

            public float ForcedMoveRatio =>
                SampledStepCount > 0 ? ForcedMoveStepCount / (float)SampledStepCount : 0f;

            public static ExperienceStats Empty =>
                new ExperienceStats(0, 0f, 0, 0, 0, Array.Empty<int>());

            public ExperienceStats(
                int minChoices,
                float averageChoices,
                int bottleneckStepCount,
                int forcedMoveStepCount,
                int sampledStepCount,
                int[] choiceWave)
            {
                MinChoices = minChoices;
                AverageChoices = averageChoices;
                BottleneckStepCount = bottleneckStepCount;
                ForcedMoveStepCount = forcedMoveStepCount;
                SampledStepCount = sampledStepCount;
                ChoiceWave = choiceWave ?? Array.Empty<int>();
            }
        }

        public readonly struct BoardGenerationStats
        {
            public readonly int InitialArrowCount;
            public readonly int ArrowTileCount;
            public readonly int InitialMovableArrowChainCount;
            public readonly int MaxChainLength;
            public readonly int ChainCount;
            public readonly float AverageStepsToNextUnlock;
            public readonly ExperienceStats Experience;
            public readonly int DifficultyScore;
            // A visible arrow is one connected arrow chain. ArrowTileCount is the number of cells it occupies.
            public int ArrowCount => ChainCount;

            public BoardGenerationStats(
                int initialArrowCount,
                int arrowTileCount,
                int initialMovableArrowChainCount,
                int maxChainLength,
                int chainCount,
                float averageStepsToNextUnlock,
                ExperienceStats experience,
                int difficultyScore)
            {
                InitialArrowCount = initialArrowCount;
                ArrowTileCount = arrowTileCount;
                InitialMovableArrowChainCount = initialMovableArrowChainCount;
                MaxChainLength = maxChainLength;
                ChainCount = chainCount;
                AverageStepsToNextUnlock = averageStepsToNextUnlock;
                Experience = experience;
                DifficultyScore = difficultyScore;
            }
        }

        public readonly struct DifficultyScoreRecommendation
        {
            public readonly int Min;
            public readonly int Preferred;
            public readonly int Max;
            public readonly int AllowedCellCount;
            public readonly int TargetArrowTileCount;
            public readonly int EstimatedChainCount;

            public DifficultyScoreRecommendation(
                int min,
                int preferred,
                int max,
                int allowedCellCount,
                int targetArrowTileCount,
                int estimatedChainCount)
            {
                Min = min;
                Preferred = preferred;
                Max = max;
                AllowedCellCount = allowedCellCount;
                TargetArrowTileCount = targetArrowTileCount;
                EstimatedChainCount = estimatedChainCount;
            }
        }

        public readonly struct InitialMovableArrowRecommendation
        {
            public readonly int Min;
            public readonly int Preferred;
            public readonly int Max;
            public readonly int AllowedCellCount;
            public readonly int TargetArrowTileCount;
            public readonly int EstimatedChainCount;

            public InitialMovableArrowRecommendation(
                int min,
                int preferred,
                int max,
                int allowedCellCount,
                int targetArrowTileCount,
                int estimatedChainCount)
            {
                Min = min;
                Preferred = preferred;
                Max = max;
                AllowedCellCount = allowedCellCount;
                TargetArrowTileCount = targetArrowTileCount;
                EstimatedChainCount = estimatedChainCount;
            }
        }

        public readonly struct DifficultyCandidate
        {
            public readonly BoardState Board;
            public readonly BoardGenerationStats Stats;
            public readonly int Seed;
            public readonly int DifficultyDelta;
            public readonly bool GreedySolvable;

            public DifficultyCandidate(
                BoardState board,
                BoardGenerationStats stats,
                int seed,
                int difficultyDelta,
                bool greedySolvable)
            {
                Board = board;
                Stats = stats;
                Seed = seed;
                DifficultyDelta = difficultyDelta;
                GreedySolvable = greedySolvable;
            }
        }

        public static InitialMovableArrowRecommendation RecommendInitialMovableArrowCount(
            int width,
            int height,
            float arrowCoverage,
            int minPathLen,
            int maxPathLen,
            bool[] canSpawnHere = null)
        {
            int allowedCells = CountAllowedCells(width, height, canSpawnHere);
            if (allowedCells <= 0)
                return new InitialMovableArrowRecommendation(0, 0, 0, 0, 0, 0);

            int targetArrowTiles = Mathf.Clamp(
                Mathf.RoundToInt(allowedCells * Mathf.Clamp01(arrowCoverage)),
                1,
                allowedCells);

            int minLen = Mathf.Max(1, minPathLen);
            int maxLen = Mathf.Max(minLen, maxPathLen);
            float averagePathLen = Mathf.Max(1f, (minLen + maxLen) * 0.5f);

            int estimatedChains = Mathf.Clamp(
                Mathf.CeilToInt(targetArrowTiles / averagePathLen),
                1,
                targetArrowTiles);

            int preferred = Mathf.Clamp(
                Mathf.RoundToInt(estimatedChains * 0.65f),
                1,
                estimatedChains);

            int min = Mathf.Clamp(
                Mathf.RoundToInt(preferred * 0.75f),
                1,
                preferred);

            int max = Mathf.Clamp(
                Mathf.RoundToInt(preferred * 1.35f),
                preferred,
                estimatedChains);

            return new InitialMovableArrowRecommendation(
                min,
                preferred,
                max,
                allowedCells,
                targetArrowTiles,
                estimatedChains);
        }

        public static int ResolveInitialMovableArrowCountTarget(
            int configuredCount,
            InitialMovableArrowRecommendation recommendation)
        {
            int upperLimit = Mathf.Max(0, recommendation.TargetArrowTileCount);
            if (upperLimit <= 0)
                return 0;

            if (configuredCount > 0)
                return Mathf.Clamp(configuredCount, 1, upperLimit);

            return Mathf.Clamp(recommendation.Preferred, 0, upperLimit);
        }

        public static DifficultyScoreRecommendation RecommendDifficultyScore(
            int width,
            int height,
            float arrowCoverage,
            int minPathLen,
            int maxPathLen,
            bool[] canSpawnHere = null)
        {
            var initialRecommendation = RecommendInitialMovableArrowCount(
                width,
                height,
                arrowCoverage,
                minPathLen,
                maxPathLen,
                canSpawnHere);

            int arrowCount = Mathf.Max(0, initialRecommendation.TargetArrowTileCount);
            if (arrowCount <= 0)
                return new DifficultyScoreRecommendation(0, 0, 0, initialRecommendation.AllowedCellCount, 0, 0);

            int minLen = Mathf.Max(1, minPathLen);
            int maxLen = Mathf.Max(minLen, maxPathLen);
            int estimatedChains = Mathf.Max(1, initialRecommendation.EstimatedChainCount);

            float easyStepsToNextUnlock = 0f;
            float preferredStepsToNextUnlock = estimatedChains / (float)Mathf.Max(1, initialRecommendation.Preferred);
            float hardStepsToNextUnlock = Mathf.Max(1f, estimatedChains * 0.35f);
            int easyInitial = Mathf.Max(1, estimatedChains);
            int hardInitial = 1;

            int minScore = CalculateDifficultyScore(arrowCount, estimatedChains, easyInitial, easyStepsToNextUnlock);
            int preferredScore = CalculateDifficultyScore(
                arrowCount,
                estimatedChains,
                Mathf.Max(1, initialRecommendation.Preferred),
                preferredStepsToNextUnlock);
            int maxScore = CalculateDifficultyScore(arrowCount, estimatedChains, hardInitial, hardStepsToNextUnlock);

            int boardSizeCap = Mathf.Max(minScore, arrowCount * 20);
            maxScore = Mathf.Min(maxScore, boardSizeCap);
            preferredScore = Mathf.Clamp(preferredScore, minScore, maxScore);

            return new DifficultyScoreRecommendation(
                minScore,
                preferredScore,
                maxScore,
                initialRecommendation.AllowedCellCount,
                arrowCount,
                estimatedChains);
        }

        public static int ResolveDifficultyScoreTarget(
            int configuredScore,
            DifficultyScoreRecommendation recommendation)
        {
            if (configuredScore <= 0 || recommendation.Max <= 0)
                return 0;

            return Mathf.Clamp(configuredScore, recommendation.Min, recommendation.Max);
        }

        public static List<DifficultyCandidate> GenerateDifficultyCandidates(
            LevelSpec baseSpec,
            IRuleset ruleset,
            int targetDifficultyScore,
            int candidateCount,
            int attempts,
            bool requireGreedySolvable,
            Func<int, int, bool> progressCallback = null)
        {
            ruleset ??= new ArrowMagicRuleset(new ArrowMagicRulesetConfig());
            int countLimit = Mathf.Max(1, candidateCount);
            int attemptLimit = Mathf.Max(countLimit, attempts);
            int target = Mathf.Max(0, targetDifficultyScore);

            var generator = new ClearAllArrowsGenerator();
            var candidates = new List<DifficultyCandidate>(attemptLimit);

            for (int attempt = 0; attempt < attemptLimit; attempt++)
            {
                LevelSpec spec = baseSpec;
                spec.seed = BuildCandidateSeed(baseSpec.seed, attempt);

                BoardState board = generator.Generate(spec);
                bool greedySolvable = !requireGreedySolvable ||
                                      GreedyValidator.TryClearAllByGreedy(board, ruleset, 300, out _);
                if (!requireGreedySolvable || greedySolvable)
                {
                    BoardGenerationStats stats = CalculateBoardGenerationStats(board, ruleset);
                    int delta = Mathf.Abs(stats.DifficultyScore - target);
                    candidates.Add(new DifficultyCandidate(board, stats, spec.seed, delta, greedySolvable));
                }

                if (progressCallback != null && !progressCallback(attempt + 1, attemptLimit))
                    break;
            }

            candidates.Sort((a, b) =>
            {
                int deltaCompare = a.DifficultyDelta.CompareTo(b.DifficultyDelta);
                if (deltaCompare != 0) return deltaCompare;
                return a.Seed.CompareTo(b.Seed);
            });

            if (candidates.Count > countLimit)
                candidates.RemoveRange(countLimit, candidates.Count - countLimit);

            return candidates;
        }

        public static int BuildCandidateSeed(int seed, int attempt)
        {
            unchecked
            {
                return seed * CandidateSeedStride + attempt;
            }
        }

        public static BoardGenerationStats CalculateBoardGenerationStats(BoardState state, IRuleset ruleset)
        {
            if (state == null || state.tiles == null || state.tiles.Length == 0)
                return new BoardGenerationStats(0, 0, 0, 0, 0, 0f, ExperienceStats.Empty, 0);

            var visited = new bool[state.tiles.Length];
            var chain = new HashSet<int>();
            int arrowTileCount = 0;
            int movableChainCount = 0;
            int maxChainLength = 0;
            int chainCount = 0;

            for (int i = 0; i < state.tiles.Length; i++)
            {
                if (state.tiles[i].type == TileType.Arrow)
                    arrowTileCount++;

                if (visited[i] || state.tiles[i].type != TileType.Arrow)
                    continue;

                var pos = new Vector2Int(i % state.width, i / state.width);
                ArrowChainUtility.CollectFullChain(state, pos, 0, chain);

                if (chain.Count == 0)
                {
                    visited[i] = true;
                    continue;
                }

                foreach (int idx in chain)
                    visited[idx] = true;

                chainCount++;
                maxChainLength = Mathf.Max(maxChainLength, chain.Count);

                if (ruleset != null && CanClearAnyTileInChain(state, ruleset, chain))
                    movableChainCount++;
            }

            ExperienceStats experience = CalculateExperienceStats(state, ruleset);
            float averageStepsToNextUnlock = CalculateAverageStepsToNextUnlock(state, ruleset);
            int difficultyScore = CalculateDifficultyScore(
                arrowTileCount,
                chainCount,
                movableChainCount,
                averageStepsToNextUnlock);

            return new BoardGenerationStats(
                chainCount,
                arrowTileCount,
                movableChainCount,
                maxChainLength,
                chainCount,
                averageStepsToNextUnlock,
                experience,
                difficultyScore);
        }

        public static int CalculateDifficultyScore(
            int arrowCount,
            int chainCount,
            int initialClearableCount,
            float averageStepsToNextUnlock)
        {
            if (arrowCount <= 0)
                return 0;

            int clearable = Mathf.Max(1, initialClearableCount);
            int chains = Mathf.Max(1, chainCount);
            float openingPressure = chains / (float)clearable;
            float progressPressure = Mathf.Max(0f, averageStepsToNextUnlock);
            return Mathf.Max(0, Mathf.RoundToInt(
                arrowCount +
                openingPressure * 20f +
                progressPressure * 30f));
        }

        public static string FormatDifficultyScore(int difficultyScore)
        {
            return $"{difficultyScore} -> {DescribeDifficultyTier(difficultyScore)}";
        }

        public static string FormatChoiceWave(IReadOnlyList<int> choiceWave)
        {
            if (choiceWave == null || choiceWave.Count == 0)
                return "-";

            var parts = new string[choiceWave.Count];
            for (int i = 0; i < choiceWave.Count; i++)
                parts[i] = choiceWave[i].ToString();

            return string.Join(" > ", parts);
        }

        public static string DescribeDifficultyTier(int difficultyScore)
        {
            if (difficultyScore <= 149)
                return "简单（0-149）";

            if (difficultyScore <= 299)
                return "普通（150-299）";

            if (difficultyScore <= 449)
                return "较难（300-449）";

            if (difficultyScore <= 599)
                return "困难（450-599）";

            return "超难（600+）";
        }

        public static int CountInitialMovableArrowChains(BoardState state, IRuleset ruleset)
        {
            if (state == null || ruleset == null)
                return 0;

            var visited = new HashSet<int>();
            var chain = new HashSet<int>();
            int count = 0;

            for (int i = 0; i < state.tiles.Length; i++)
            {
                if (visited.Contains(i))
                    continue;

                if (state.tiles[i].type != TileType.Arrow)
                    continue;

                var pos = new Vector2Int(i % state.width, i / state.width);
                ArrowChainUtility.CollectFullChain(state, pos, 0, chain);

                if (chain.Count == 0)
                {
                    visited.Add(i);
                    continue;
                }

                foreach (int idx in chain)
                    visited.Add(idx);

                if (CanClearAnyTileInChain(state, ruleset, chain))
                    count++;
            }

            return count;
        }

        static bool CanClearAnyTileInChain(BoardState state, IRuleset ruleset, HashSet<int> chain)
        {
            foreach (int idx in chain)
            {
                var move = new Move(new Vector2Int(idx % state.width, idx / state.width));
                if (!ruleset.TryApplyMove(state, move, out var delta))
                    continue;

                delta.Undo(state);
                return true;
            }

            return false;
        }

        static ExperienceStats CalculateExperienceStats(BoardState state, IRuleset ruleset)
        {
            if (state == null || ruleset == null)
                return ExperienceStats.Empty;

            var clone = CloneBoard(state);
            var clearableChainIds = new HashSet<int>();
            var choiceWave = new List<int>();
            int minChoices = int.MaxValue;
            int totalChoices = 0;
            int bottleneckStepCount = 0;
            int forcedMoveStepCount = 0;
            int sampledStepCount = 0;
            int maxSteps = Mathf.Max(1, clone.width * clone.height * 2);

            for (int i = 0; i < maxSteps && clone.AnyArrows(); i++)
            {
                CollectClearableChainIds(clone, ruleset, clearableChainIds);
                int choices = clearableChainIds.Count;
                if (choices <= 0)
                    break;

                choiceWave.Add(choices);
                minChoices = Mathf.Min(minChoices, choices);
                totalChoices += choices;
                sampledStepCount++;

                if (choices <= 2)
                    bottleneckStepCount++;
                if (choices == 1)
                    forcedMoveStepCount++;

                if (!TryFindBestClearingMove(clone, ruleset, out Move bestMove, out _))
                    break;

                if (!ruleset.TryApplyMove(clone, bestMove, out _))
                    break;
            }

            if (sampledStepCount <= 0)
                return ExperienceStats.Empty;

            return new ExperienceStats(
                minChoices,
                totalChoices / (float)sampledStepCount,
                bottleneckStepCount,
                forcedMoveStepCount,
                sampledStepCount,
                choiceWave.ToArray());
        }

        static float CalculateAverageStepsToNextUnlock(BoardState state, IRuleset ruleset)
        {
            if (state == null || ruleset == null)
                return 0f;

            var clone = CloneBoard(state);
            var clearableChainIds = new HashSet<int>();
            var seenClearableChainIds = new HashSet<int>();
            CollectClearableChainIds(clone, ruleset, seenClearableChainIds);

            int steps = 0;
            int lastUnlockStep = 0;
            int unlockGapTotal = 0;
            int unlockedChainCount = 0;
            int maxSteps = Mathf.Max(1, clone.width * clone.height * 2);

            for (int i = 0; i < maxSteps && clone.AnyArrows(); i++)
            {
                if (!TryFindBestClearingMove(clone, ruleset, out Move bestMove, out _))
                    break;

                if (!ruleset.TryApplyMove(clone, bestMove, out _))
                    break;

                steps++;
                CollectClearableChainIds(clone, ruleset, clearableChainIds);

                int newlyClearable = 0;
                foreach (int chainId in clearableChainIds)
                {
                    if (seenClearableChainIds.Add(chainId))
                        newlyClearable++;
                }

                if (newlyClearable <= 0)
                    continue;

                int gap = steps - lastUnlockStep;
                unlockGapTotal += gap * newlyClearable;
                unlockedChainCount += newlyClearable;
                lastUnlockStep = steps;
            }

            if (steps <= 0 && state.AnyArrows())
                return state.AnyArrows() ? Mathf.Max(1f, state.width * state.height) : 0f;

            return unlockedChainCount > 0 ? unlockGapTotal / (float)unlockedChainCount : 0f;
        }

        static bool TryFindBestClearingMove(BoardState state, IRuleset ruleset, out Move bestMove, out int bestCleared)
        {
            bestMove = default;
            bestCleared = 0;

            foreach (Move move in ruleset.GetLegalMoves(state))
            {
                if (!ruleset.TryApplyMove(state, move, out var delta))
                    continue;

                int cleared = CountCleared(delta);
                delta.Undo(state);

                if (cleared <= bestCleared)
                    continue;

                bestMove = move;
                bestCleared = cleared;
            }

            return bestCleared > 0;
        }

        static int CountCleared(MoveDelta delta)
        {
            int cleared = 0;
            for (int i = 0; i < delta.changes.Count; i++)
            {
                CellChange change = delta.changes[i];
                if (change.before.type == TileType.Arrow && change.after.type == TileType.Empty)
                    cleared++;
            }

            return cleared;
        }

        static void CollectClearableChainIds(BoardState state, IRuleset ruleset, HashSet<int> clearableChainIds)
        {
            clearableChainIds.Clear();
            if (state == null || ruleset == null)
                return;

            var visited = new HashSet<int>();
            var chain = new HashSet<int>();

            for (int i = 0; i < state.tiles.Length; i++)
            {
                if (visited.Contains(i))
                    continue;

                if (state.tiles[i].type != TileType.Arrow)
                    continue;

                var pos = new Vector2Int(i % state.width, i / state.width);
                ArrowChainUtility.CollectFullChain(state, pos, 0, chain);

                if (chain.Count == 0)
                {
                    visited.Add(i);
                    continue;
                }

                foreach (int idx in chain)
                    visited.Add(idx);

                if (CanClearAnyTileInChain(state, ruleset, chain))
                    clearableChainIds.Add(GetChainId(chain));
            }
        }

        static int GetChainId(HashSet<int> chain)
        {
            int id = int.MaxValue;
            foreach (int idx in chain)
                id = Mathf.Min(id, idx);
            return id == int.MaxValue ? -1 : id;
        }

        static BoardState CloneBoard(BoardState source)
        {
            var clone = new BoardState(source.width, source.height);
            System.Array.Copy(source.tiles, clone.tiles, source.tiles.Length);
            return clone;
        }

        static int CountAllowedCells(int width, int height, bool[] canSpawnHere)
        {
            int totalCells = Mathf.Max(0, width) * Mathf.Max(0, height);
            if (totalCells <= 0)
                return 0;

            if (canSpawnHere == null)
                return totalCells;

            int count = 0;
            int limit = Mathf.Min(totalCells, canSpawnHere.Length);
            for (int i = 0; i < limit; i++)
            {
                if (canSpawnHere[i])
                    count++;
            }

            return count;
        }
    }
}
