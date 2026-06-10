using System.Collections.Generic;
using NUnit.Framework;
using PixelBug.ArrowMagic.EditorTools;
using UnityEngine;

namespace PixelBug.ArrowMagic.Tests
{
    public sealed class AuthoredLevelBuilderTests
    {
        [Test]
        public void BuildBoard_ConvertsStraightHeadToTailChain()
        {
            var level = new AuthoredLevelData
            {
                width = 3,
                height = 1,
                arrows = new List<AuthoredArrowData>
                {
                    new AuthoredArrowData { indices = new List<int> { 2, 1, 0 }, colorIndex = 0 }
                }
            };

            Assert.That(AuthoredLevelBuilder.TryBuildBoard(level, out var board, out var error), Is.True, error);

            AssertArrow(board, 0, 0, Dir.Left, Dir.Right);
            AssertArrow(board, 1, 0, Dir.Left, Dir.Right);
            AssertArrow(board, 2, 0, Dir.Left, Dir.Right);

            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = SignalTravelMode.ThroughEmpty
            });

            Assert.That(rules.TryApplyMove(board, new Move(new Vector2Int(2, 0)), out _), Is.True);
            Assert.That(rules.IsSolved(board), Is.True);
        }

        [Test]
        public void BuildBoard_ConvertsTurnedHeadToTailChain()
        {
            var level = new AuthoredLevelData
            {
                width = 3,
                height = 3,
                arrows = new List<AuthoredArrowData>
                {
                    new AuthoredArrowData { indices = new List<int> { 8, 5, 4 }, colorIndex = 0 }
                }
            };

            Assert.That(AuthoredLevelBuilder.TryBuildBoard(level, out var board, out var error), Is.True, error);

            AssertArrow(board, 1, 1, Dir.Left, Dir.Right);
            AssertArrow(board, 2, 1, Dir.Left, Dir.Up);
            AssertArrow(board, 2, 2, Dir.Down, Dir.Up);
        }

        [Test]
        public void BuildBoard_RejectsNonAdjacentCells()
        {
            var level = new AuthoredLevelData
            {
                width = 3,
                height = 1,
                arrows = new List<AuthoredArrowData>
                {
                    new AuthoredArrowData { indices = new List<int> { 2, 0 }, colorIndex = 0 }
                }
            };

            Assert.That(AuthoredLevelBuilder.TryBuildBoard(level, out _, out var error), Is.False);
            Assert.That(error, Does.Contain("不相邻"));
        }

        [Test]
        public void BuildBoard_RejectsOverlappingArrows()
        {
            var level = new AuthoredLevelData
            {
                width = 3,
                height = 2,
                arrows = new List<AuthoredArrowData>
                {
                    new AuthoredArrowData { indices = new List<int> { 2, 1 }, colorIndex = 0 },
                    new AuthoredArrowData { indices = new List<int> { 4, 1 }, colorIndex = 1 }
                }
            };

            Assert.That(AuthoredLevelBuilder.TryBuildBoard(level, out _, out var error), Is.False);
            Assert.That(error, Does.Contain("重复占用"));
        }

        [Test]
        public void BuildBoard_RejectsArrowShorterThanTwoCells()
        {
            var level = new AuthoredLevelData
            {
                width = 2,
                height = 2,
                arrows = new List<AuthoredArrowData>
                {
                    new AuthoredArrowData { indices = new List<int> { 1 }, colorIndex = 0 }
                }
            };

            Assert.That(AuthoredLevelBuilder.TryBuildBoard(level, out _, out var error), Is.False);
            Assert.That(error, Does.Contain("至少需要 2 个格子"));
        }

        [Test]
        public void BuildBoard_RejectsHeadBlockedByOwnBody()
        {
            var level = new AuthoredLevelData
            {
                width = 3,
                height = 2,
                arrows = new List<AuthoredArrowData>
                {
                    new AuthoredArrowData { indices = new List<int> { 1, 0, 3, 4, 5, 2 }, colorIndex = 0 }
                }
            };

            Assert.That(AuthoredLevelBuilder.TryBuildBoard(level, out _, out var error), Is.False);
            Assert.That(error, Does.Contain("头部前方被自身占用"));
        }

        [Test]
        public void BuildBoard_KeepsAdjacentAuthoredArrowsAsSeparateChains()
        {
            var level = new AuthoredLevelData
            {
                width = 5,
                height = 1,
                arrows = new List<AuthoredArrowData>
                {
                    new AuthoredArrowData { indices = new List<int> { 1, 2 }, colorIndex = 0 },
                    new AuthoredArrowData { indices = new List<int> { 3, 4 }, colorIndex = 1 }
                }
            };

            Assert.That(AuthoredLevelBuilder.TryBuildBoard(level, out var board, out var error), Is.True, error);

            var firstChain = new HashSet<int>();
            ArrowChainUtility.CollectFullChain(board, new Vector2Int(1, 0), 0, firstChain);

            var secondChain = new HashSet<int>();
            ArrowChainUtility.CollectFullChain(board, new Vector2Int(3, 0), 0, secondChain);

            Assert.That(firstChain, Is.EquivalentTo(new[] { 1, 2 }));
            Assert.That(secondChain, Is.EquivalentTo(new[] { 3, 4 }));
        }

        [Test]
        public void CreatePreviewDefinition_UsesAuthoredSourceAndCopiesLevelData()
        {
            var level = new AuthoredLevelData
            {
                width = 4,
                height = 3,
                arrows = new List<AuthoredArrowData>
                {
                    new AuthoredArrowData { indices = new List<int> { 3, 2, 1 }, colorIndex = 2 }
                }
            };

            var definition = AuthoredLevelEditorWindow.CreatePreviewDefinition("preview_level", level);

            Assert.That(definition.levelId, Is.EqualTo("preview_level"));
            Assert.That(definition.source, Is.EqualTo(LevelDefinition.LevelSource.Authored));
            Assert.That(definition.board.width, Is.EqualTo(4));
            Assert.That(definition.board.height, Is.EqualTo(3));
            Assert.That(definition.authoredLevel, Is.Not.SameAs(level));
            Assert.That(definition.authoredLevel.arrows[0].indices, Is.EqualTo(new[] { 3, 2, 1 }));
            Assert.That(definition.authoredLevel.arrows[0].colorIndex, Is.EqualTo(2));
        }

        [Test]
        public void ScreenRowToBoardY_MatchesSceneYAxisConvention()
        {
            Assert.That(AuthoredLevelEditorWindow.ScreenRowToBoardY(0, 8), Is.EqualTo(7));
            Assert.That(AuthoredLevelEditorWindow.ScreenRowToBoardY(7, 8), Is.EqualTo(0));
            Assert.That(AuthoredLevelEditorWindow.BoardYToScreenRow(7, 8), Is.EqualTo(0));
            Assert.That(AuthoredLevelEditorWindow.BoardYToScreenRow(0, 8), Is.EqualTo(7));
        }

        static void AssertArrow(BoardState board, int x, int y, Dir inDir, Dir outDir)
        {
            var tile = board.Get(x, y);
            Assert.That(tile.type, Is.EqualTo(TileType.Arrow), $"({x},{y}) 应该是箭头");
            Assert.That(tile.arrow.inDir, Is.EqualTo(inDir), $"({x},{y}) inDir");
            Assert.That(tile.arrow.outDir, Is.EqualTo(outDir), $"({x},{y}) outDir");
        }
    }
}
