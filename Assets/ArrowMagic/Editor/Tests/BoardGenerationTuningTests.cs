using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace PixelBug.ArrowMagic.Tests
{
    public sealed class BoardGenerationTuningTests
    {
        [MenuItem("Tools/Tests/Run BoardGenerationTuning Smoke")]
        public static void RunBoardGenerationTuningSmoke()
        {
            try
            {
                var tests = new BoardGenerationTuningTests();
                tests.BoardController_UsesFiveHundredSearchAttempts();
                tests.BoardController_FastPreviewUsesSingleUnfilteredAttempt();
                tests.BoardController_FilteredModeKeepsSearchWhenDifficultyIsTargeted();
                tests.BoardController_AwakeFastPreviewFlagSelectsFastPreviewMode();
                tests.BoardController_AwakeDefaultSelectsFilteredMode();
                tests.BoardController_AwakeCreateSceneSelectsFastPreviewMode();
                tests.ClearAllArrowsGenerator_RespectsMaxChainLength();
                tests.ClearAllArrowsGenerator_RespectsMinChainLength();
                tests.CalculateBoardGenerationStats_ReturnsArrowCountMaxChainAndDifficultyScore();
                tests.CalculateAverageStepsToNextUnlock_CountsStepsBetweenNewlyClearableChains();
                tests.CalculateExperienceStats_RecordsChoiceWaveAndBottlenecks();
                tests.FormatChoiceWave_ReturnsReadableUnlockCurve();
                tests.CalculateDifficultyScore_UsesNextUnlockStepsForProgressPressure();
                tests.GenerateDifficultyCandidates_ReturnsClosestCandidatesFirst();
                tests.GenerateDifficultyCandidates_ReportsAttemptProgress();
                tests.GenerateDifficultyCandidates_StopsWhenProgressReturnsFalse();
                tests.RecommendDifficultyScore_UsesBoardSizeAndCapsSmallBoardsBelowHard();
                tests.FormatDifficultyScore_ReturnsScoreTierAndRange();
                tests.RecommendInitialMovableArrowCount_UsesAllowedCellsCoverageAndPathLength();
                tests.CountInitialMovableArrowChains_CountsAConnectedClearableChainOnce();
                Debug.Log("[BoardGenerationTuningTests] 断言通过。");
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                throw;
            }
        }

        [Test]
        public void BoardController_UsesFiveHundredSearchAttempts()
        {
            Assert.That(BoardController.InitialMovableArrowSearchAttempts, Is.EqualTo(500));
        }

        [Test]
        public void ArrowMagicMenuItems_ArePlacedDirectlyUnderTools()
        {
            var menuItems = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(GetLoadableTypes)
                .Where(type => type.Namespace != null && type.Namespace.StartsWith("PixelBug.ArrowMagic", StringComparison.Ordinal))
                .SelectMany(type => type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                .SelectMany(method => method.GetCustomAttributes<MenuItem>())
                .Select(attribute => attribute.menuItem)
                .Where(path => path.StartsWith("Tools/", StringComparison.Ordinal))
                .ToArray();

            Assert.That(menuItems, Is.Not.Empty);
            Assert.That(menuItems, Has.None.StartsWith("Tools/PixelBug/"));
            Assert.That(menuItems, Has.None.StartsWith("Tools/Arrow Magic/"));
        }

        [Test]
        public void BoardController_FastPreviewUsesSingleUnfilteredAttempt()
        {
            var settings = BoardController.ResolveGenerationSearchSettings(
                BoardController.GenerationMode.FastPreview,
                validateWithGreedy: true,
                targetInitialMovable: 7,
                targetDifficulty: 624);

            Assert.That(settings.AttemptCount, Is.EqualTo(1));
            Assert.That(settings.ValidateWithGreedy, Is.False);
            Assert.That(settings.ScoreCandidates, Is.False);
        }

        [Test]
        public void BoardController_FilteredModeKeepsSearchWhenDifficultyIsTargeted()
        {
            var settings = BoardController.ResolveGenerationSearchSettings(
                BoardController.GenerationMode.Filtered,
                validateWithGreedy: true,
                targetInitialMovable: 0,
                targetDifficulty: 624);

            Assert.That(settings.AttemptCount, Is.EqualTo(BoardController.InitialMovableArrowSearchAttempts));
            Assert.That(settings.ValidateWithGreedy, Is.True);
            Assert.That(settings.ScoreCandidates, Is.True);
        }

        [Test]
        public void BoardController_AwakeFastPreviewFlagSelectsFastPreviewMode()
        {
            Assert.That(
                BoardController.ResolveAwakeGenerationMode(fastPreviewOnAwake: true),
                Is.EqualTo(BoardController.GenerationMode.FastPreview));
        }

        [Test]
        public void BoardController_AwakeDefaultSelectsFilteredMode()
        {
            Assert.That(
                BoardController.ResolveAwakeGenerationMode(fastPreviewOnAwake: false, sceneName: "Demo"),
                Is.EqualTo(BoardController.GenerationMode.Filtered));
        }

        [Test]
        public void BoardController_AwakeCreateSceneSelectsFastPreviewMode()
        {
            Assert.That(
                BoardController.ResolveAwakeGenerationMode(fastPreviewOnAwake: false, sceneName: "Create"),
                Is.EqualTo(BoardController.GenerationMode.FastPreview));
        }

        [Test]
        public void ClearAllArrowsGenerator_RespectsMaxChainLength()
        {
            var gen = new ClearAllArrowsGenerator();
            int maxAllowedChainLength = 2;

            for (int seed = 1; seed <= 32; seed++)
            {
                var state = gen.Generate(new LevelSpec
                {
                    width = 6,
                    height = 6,
                    seed = seed,
                    arrowFill = 1f,
                    minPathLen = 2,
                    maxPathLen = maxAllowedChainLength,
                    twistiness = 0.5f,
                    canSpawnHere = null
                });

                Assert.That(GetMaxConnectedArrowChainLength(state), Is.LessThanOrEqualTo(maxAllowedChainLength),
                    $"seed={seed} 生成出的链长超过上限 {maxAllowedChainLength}");
            }
        }

        [Test]
        public void ClearAllArrowsGenerator_RespectsMinChainLength()
        {
            var gen = new ClearAllArrowsGenerator();
            int minAllowedChainLength = 2;

            for (int seed = 1; seed <= 32; seed++)
            {
                var state = gen.Generate(new LevelSpec
                {
                    width = 6,
                    height = 6,
                    seed = seed,
                    arrowFill = 1f,
                    minPathLen = minAllowedChainLength,
                    maxPathLen = 20,
                    twistiness = 0.5f,
                    canSpawnHere = null
                });

                Assert.That(GetMinConnectedArrowChainLength(state), Is.GreaterThanOrEqualTo(minAllowedChainLength),
                    $"seed={seed} 生成出了小于最短格子数量 {minAllowedChainLength} 的箭头链");
            }
        }

        [Test]
        public void CalculateBoardGenerationStats_ReturnsArrowCountMaxChainAndDifficultyScore()
        {
            var state = new BoardState(3, 2);
            state.Set(0, 0, TileState.Arrow(Dir.Left, Dir.Right));
            state.Set(1, 0, TileState.Arrow(Dir.Left, Dir.Right));
            state.Set(2, 0, TileState.Empty());
            state.Set(0, 1, TileState.Arrow(Dir.Down, Dir.Up));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var stats = BoardGenerationTuning.CalculateBoardGenerationStats(state, rules);

            Assert.That(stats.InitialArrowCount, Is.EqualTo(2));
            Assert.That(stats.ArrowCount, Is.EqualTo(2));
            Assert.That(stats.ArrowTileCount, Is.EqualTo(3));
            Assert.That(stats.InitialMovableArrowChainCount, Is.EqualTo(2));
            Assert.That(stats.MaxChainLength, Is.EqualTo(2));
            Assert.That(stats.ChainCount, Is.EqualTo(2));
            Assert.That(stats.AverageStepsToNextUnlock, Is.EqualTo(0f).Within(0.001f));
            Assert.That(stats.DifficultyScore, Is.EqualTo(23));
        }

        [Test]
        public void CalculateAverageStepsToNextUnlock_CountsStepsBetweenNewlyClearableChains()
        {
            var state = new BoardState(3, 1);
            state.Set(0, 0, TileState.Arrow(Dir.Left, Dir.Right));
            state.Set(1, 0, TileState.Empty());
            state.Set(2, 0, TileState.Arrow(Dir.Left, Dir.Right));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var stats = BoardGenerationTuning.CalculateBoardGenerationStats(state, rules);

            Assert.That(stats.InitialMovableArrowChainCount, Is.EqualTo(1));
            Assert.That(stats.AverageStepsToNextUnlock, Is.EqualTo(1f).Within(0.001f));
        }

        [Test]
        public void CalculateExperienceStats_RecordsChoiceWaveAndBottlenecks()
        {
            var state = new BoardState(3, 1);
            state.Set(0, 0, TileState.Arrow(Dir.Left, Dir.Right));
            state.Set(1, 0, TileState.Empty());
            state.Set(2, 0, TileState.Arrow(Dir.Left, Dir.Right));

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var stats = BoardGenerationTuning.CalculateBoardGenerationStats(state, rules);

            Assert.That(stats.Experience.MinChoices, Is.EqualTo(1));
            Assert.That(stats.Experience.AverageChoices, Is.EqualTo(1f).Within(0.001f));
            Assert.That(stats.Experience.BottleneckStepCount, Is.EqualTo(2));
            Assert.That(stats.Experience.ForcedMoveStepCount, Is.EqualTo(2));
            Assert.That(stats.Experience.ForcedMoveRatio, Is.EqualTo(1f).Within(0.001f));
            Assert.That(stats.Experience.ChoiceWave, Is.EqualTo(new[] { 1, 1 }));
        }

        [Test]
        public void FormatChoiceWave_ReturnsReadableUnlockCurve()
        {
            Assert.That(BoardGenerationTuning.FormatChoiceWave(new[] { 2, 1, 3 }), Is.EqualTo("2 > 1 > 3"));
            Assert.That(BoardGenerationTuning.FormatChoiceWave(Array.Empty<int>()), Is.EqualTo("-"));
        }

        [Test]
        public void CalculateDifficultyScore_UsesNextUnlockStepsForProgressPressure()
        {
            int scoreWithoutUnlockGap = BoardGenerationTuning.CalculateDifficultyScore(
                arrowCount: 113,
                chainCount: 20,
                initialClearableCount: 4,
                averageStepsToNextUnlock: 0f);
            int scoreWithUnlockGap = BoardGenerationTuning.CalculateDifficultyScore(
                arrowCount: 113,
                chainCount: 20,
                initialClearableCount: 4,
                averageStepsToNextUnlock: 2f);

            Assert.That(scoreWithoutUnlockGap, Is.EqualTo(213));
            Assert.That(scoreWithUnlockGap, Is.EqualTo(273));
            Assert.That(BoardGenerationTuning.DescribeDifficultyTier(scoreWithUnlockGap), Is.EqualTo("普通（150-299）"));
        }

        [Test]
        public void GenerateDifficultyCandidates_ReturnsClosestCandidatesFirst()
        {
            var spec = new LevelSpec
            {
                width = 4,
                height = 4,
                seed = 7,
                arrowFill = 0.8f,
                minPathLen = 2,
                maxPathLen = 6,
                twistiness = 0.5f,
                canSpawnHere = null
            };

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            var candidates = BoardGenerationTuning.GenerateDifficultyCandidates(
                spec,
                rules,
                targetDifficultyScore: 180,
                candidateCount: 5,
                attempts: 12,
                requireGreedySolvable: false);

            Assert.That(candidates.Count, Is.GreaterThan(0));
            Assert.That(candidates.Count, Is.LessThanOrEqualTo(5));

            for (int i = 1; i < candidates.Count; i++)
                Assert.That(candidates[i].DifficultyDelta, Is.GreaterThanOrEqualTo(candidates[i - 1].DifficultyDelta));
        }

        [Test]
        public void GenerateDifficultyCandidates_ReportsAttemptProgress()
        {
            var progress = new List<(int Current, int Total)>();

            BoardGenerationTuning.GenerateDifficultyCandidates(
                CreateSmallCandidateSpec(),
                CreateThroughEmptyRules(),
                targetDifficultyScore: 180,
                candidateCount: 5,
                attempts: 12,
                requireGreedySolvable: false,
                progressCallback: (current, total) =>
                {
                    progress.Add((current, total));
                    return true;
                });

            Assert.That(progress.Count, Is.EqualTo(12));
            Assert.That(progress[0], Is.EqualTo((1, 12)));
            Assert.That(progress[^1], Is.EqualTo((12, 12)));
        }

        [Test]
        public void GenerateDifficultyCandidates_StopsWhenProgressReturnsFalse()
        {
            var progress = new List<(int Current, int Total)>();

            var candidates = BoardGenerationTuning.GenerateDifficultyCandidates(
                CreateSmallCandidateSpec(),
                CreateThroughEmptyRules(),
                targetDifficultyScore: 180,
                candidateCount: 5,
                attempts: 12,
                requireGreedySolvable: false,
                progressCallback: (current, total) =>
                {
                    progress.Add((current, total));
                    return current < 3;
                });

            Assert.That(progress.Count, Is.EqualTo(3));
            Assert.That(progress[^1], Is.EqualTo((3, 12)));
            Assert.That(candidates.Count, Is.EqualTo(3));
        }

        static LevelSpec CreateSmallCandidateSpec()
        {
            return new LevelSpec
            {
                width = 4,
                height = 4,
                seed = 7,
                arrowFill = 0.8f,
                minPathLen = 2,
                maxPathLen = 6,
                twistiness = 0.5f,
                canSpawnHere = null
            };
        }

        static ArrowMagicRuleset CreateThroughEmptyRules()
        {
            return new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });
        }

        static Type[] GetLoadableTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(type => type != null).ToArray();
            }
        }

        [Test]
        public void RecommendDifficultyScore_UsesBoardSizeAndCapsSmallBoardsBelowHard()
        {
            var range = BoardGenerationTuning.RecommendDifficultyScore(
                width: 4,
                height: 4,
                arrowCoverage: 1f,
                minPathLen: 2,
                maxPathLen: 20);

            Assert.That(range.TargetArrowTileCount, Is.EqualTo(16));
            Assert.That(range.Max, Is.LessThan(450));
            Assert.That(range.Min, Is.LessThanOrEqualTo(range.Preferred));
            Assert.That(range.Preferred, Is.LessThanOrEqualTo(range.Max));
        }

        [Test]
        public void FormatDifficultyScore_ReturnsScoreTierAndRange()
        {
            Assert.That(BoardGenerationTuning.FormatDifficultyScore(0), Is.EqualTo("0 -> 简单（0-149）"));
            Assert.That(BoardGenerationTuning.FormatDifficultyScore(299), Is.EqualTo("299 -> 普通（150-299）"));
            Assert.That(BoardGenerationTuning.FormatDifficultyScore(540), Is.EqualTo("540 -> 困难（450-599）"));
            Assert.That(BoardGenerationTuning.FormatDifficultyScore(600), Is.EqualTo("600 -> 超难（600+）"));
        }

        [Test]
        public void RecommendInitialMovableArrowCount_UsesAllowedCellsCoverageAndPathLength()
        {
            var mask = new bool[100];
            for (int i = 0; i < 40; i++)
                mask[i] = true;

            var recommendation = BoardGenerationTuning.RecommendInitialMovableArrowCount(
                width: 10,
                height: 10,
                arrowCoverage: 0.5f,
                minPathLen: 2,
                maxPathLen: 10,
                canSpawnHere: mask);

            Assert.That(recommendation.AllowedCellCount, Is.EqualTo(40));
            Assert.That(recommendation.TargetArrowTileCount, Is.EqualTo(20));
            Assert.That(recommendation.EstimatedChainCount, Is.EqualTo(4));
            Assert.That(recommendation.Min, Is.EqualTo(2));
            Assert.That(recommendation.Preferred, Is.EqualTo(3));
            Assert.That(recommendation.Max, Is.EqualTo(4));
        }

        [Test]
        public void CountInitialMovableArrowChains_CountsAConnectedClearableChainOnce()
        {
            var state = new BoardState(3, 1);
            state.Set(0, 0, TileState.Arrow(Dir.Left, Dir.Right));
            state.Set(1, 0, TileState.Arrow(Dir.Left, Dir.Right));
            state.Set(2, 0, TileState.Empty());

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            int count = BoardGenerationTuning.CountInitialMovableArrowChains(state, rules);

            Assert.That(count, Is.EqualTo(1));
            Assert.That(state.Get(0, 0).type, Is.EqualTo(TileType.Arrow));
            Assert.That(state.Get(1, 0).type, Is.EqualTo(TileType.Arrow));
        }

        static int GetMaxConnectedArrowChainLength(BoardState state)
        {
            if (state == null)
                return 0;

            var visited = new bool[state.tiles.Length];
            var chain = new System.Collections.Generic.HashSet<int>();
            int max = 0;

            for (int i = 0; i < state.tiles.Length; i++)
            {
                if (visited[i] || state.tiles[i].type != TileType.Arrow)
                    continue;

                chain.Clear();
                var pos = new Vector2Int(i % state.width, i / state.width);
                ArrowChainUtility.CollectFullChain(state, pos, 0, chain);

                foreach (int idx in chain)
                    visited[idx] = true;

                max = Mathf.Max(max, chain.Count);
            }

            return max;
        }

        static int GetMinConnectedArrowChainLength(BoardState state)
        {
            if (state == null)
                return 0;

            var visited = new bool[state.tiles.Length];
            var chain = new System.Collections.Generic.HashSet<int>();
            int min = int.MaxValue;

            for (int i = 0; i < state.tiles.Length; i++)
            {
                if (visited[i] || state.tiles[i].type != TileType.Arrow)
                    continue;

                chain.Clear();
                var pos = new Vector2Int(i % state.width, i / state.width);
                ArrowChainUtility.CollectFullChain(state, pos, 0, chain);

                foreach (int idx in chain)
                    visited[idx] = true;

                min = Mathf.Min(min, chain.Count);
            }

            return min == int.MaxValue ? 0 : min;
        }
    }
}
