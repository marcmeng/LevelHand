using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public static class AuthoredLevelBuilder
    {
        public static bool TryBuildBoard(AuthoredLevelData level, out BoardState board, out string error)
        {
            board = null;
            error = "";

            if (level == null)
            {
                error = "关卡数据为空。";
                return false;
            }

            int width = Mathf.Max(1, level.width);
            int height = Mathf.Max(1, level.height);
            board = new BoardState(width, height);

            for (int i = 0; i < board.tiles.Length; i++)
                board.tiles[i] = TileState.Empty();

            if (level.blockIndices != null)
            {
                var seenBlocks = new HashSet<int>();
                for (int i = 0; i < level.blockIndices.Count; i++)
                {
                    int blockIndex = level.blockIndices[i];
                    if (blockIndex < 0 || blockIndex >= board.tiles.Length)
                    {
                        error = $"Block index out of bounds: {blockIndex}.";
                        return false;
                    }

                    if (!seenBlocks.Add(blockIndex))
                    {
                        error = $"Duplicate block index: {blockIndex}.";
                        return false;
                    }

                    board.tiles[blockIndex] = TileState.Block();
                }
            }

            if (level.arrows == null || level.arrows.Count == 0)
            {
                error = "关卡至少需要 1 条箭头。";
                return false;
            }

            var occupiedByArrow = new int[width * height];
            for (int i = 0; i < occupiedByArrow.Length; i++)
                occupiedByArrow[i] = -1;

            for (int arrowIndex = 0; arrowIndex < level.arrows.Count; arrowIndex++)
            {
                var arrow = level.arrows[arrowIndex];
                if (!TryWriteArrow(board, occupiedByArrow, arrow, arrowIndex, out error))
                {
                    board = null;
                    return false;
                }
            }

            BreakExternalPredecessorLinks(board, occupiedByArrow);

            return true;
        }

        static bool TryWriteArrow(
            BoardState board,
            int[] occupiedByArrow,
            AuthoredArrowData arrow,
            int arrowIndex,
            out string error)
        {
            error = "";

            if (arrow == null || arrow.indices == null || arrow.indices.Count < 2)
            {
                error = $"第 {arrowIndex + 1} 条箭头至少需要 2 个格子。";
                return false;
            }

            var headToTail = new List<Vector2Int>(arrow.indices.Count);
            var localSeen = new HashSet<int>();

            for (int i = 0; i < arrow.indices.Count; i++)
            {
                int authoredIndex = arrow.indices[i];
                if (authoredIndex < 0 || authoredIndex >= board.tiles.Length)
                {
                    error = $"第 {arrowIndex + 1} 条箭头包含越界格子：{authoredIndex}。";
                    return false;
                }

                if (!localSeen.Add(authoredIndex))
                {
                    error = $"第 {arrowIndex + 1} 条箭头内部重复占用格子：{authoredIndex}。";
                    return false;
                }

                if (occupiedByArrow[authoredIndex] >= 0)
                {
                    error = $"第 {arrowIndex + 1} 条箭头与第 {occupiedByArrow[authoredIndex] + 1} 条箭头重复占用格子：{authoredIndex}。";
                    return false;
                }

                if (board.tiles[authoredIndex].type == TileType.Block)
                {
                    error = $"Arrow {arrowIndex + 1} overlaps block cell: {authoredIndex}.";
                    return false;
                }

                headToTail.Add(IndexToPos(authoredIndex, board.width));
            }

            for (int i = 0; i < headToTail.Count - 1; i++)
            {
                Vector2Int delta = headToTail[i + 1] - headToTail[i];
                if (!IsCardinalStep(delta))
                {
                    error = $"第 {arrowIndex + 1} 条箭头第 {i + 1} 个和第 {i + 2} 个格子不相邻。";
                    return false;
                }
            }

            Vector2Int headForward = headToTail[0] - headToTail[1];
            Vector2Int headNext = headToTail[0] + headForward;
            if (board.InBounds(headNext.x, headNext.y) && localSeen.Contains(board.Index(headNext.x, headNext.y)))
            {
                error = $"第 {arrowIndex + 1} 条箭头头部前方被自身占用。";
                return false;
            }

            for (int i = 0; i < arrow.indices.Count; i++)
                occupiedByArrow[arrow.indices[i]] = arrowIndex;

            int count = headToTail.Count;
            for (int tailToHeadIndex = 0; tailToHeadIndex < count; tailToHeadIndex++)
            {
                int headToTailIndex = count - 1 - tailToHeadIndex;
                Vector2Int pos = headToTail[headToTailIndex];

                Dir outDir;
                if (tailToHeadIndex < count - 1)
                {
                    Vector2Int next = headToTail[headToTailIndex - 1];
                    outDir = DirFromDelta(next - pos);
                }
                else
                {
                    outDir = DirFromDelta(headForward);
                }

                Dir inDir;
                if (tailToHeadIndex > 0)
                {
                    Vector2Int prev = headToTail[headToTailIndex + 1];
                    inDir = DirFromDelta(prev - pos);
                }
                else
                {
                    inDir = Opposite(outDir);
                }

                board.Set(pos.x, pos.y, TileState.Arrow(inDir, outDir));
            }

            return true;
        }

        static void BreakExternalPredecessorLinks(BoardState board, int[] occupiedByArrow)
        {
            for (int i = 0; i < board.tiles.Length; i++)
            {
                var tile = board.tiles[i];
                if (tile.type != TileType.Arrow)
                    continue;

                int owner = occupiedByArrow[i];
                if (!HasExternalPredecessor(board, occupiedByArrow, i, tile.arrow.inDir, owner))
                    continue;

                Dir replacement = PickNonExternalInDir(board, occupiedByArrow, i, tile.arrow.inDir, tile.arrow.outDir, owner);
                board.tiles[i] = TileState.Arrow(replacement, tile.arrow.outDir);
            }
        }

        static Dir PickNonExternalInDir(
            BoardState board,
            int[] occupiedByArrow,
            int index,
            Dir currentInDir,
            Dir outDir,
            int owner)
        {
            Dir bestFallback = currentInDir;

            for (int i = 0; i < 4; i++)
            {
                Dir candidate = (Dir)i;
                if (candidate == currentInDir)
                    continue;

                if (candidate == outDir)
                {
                    bestFallback = candidate;
                    continue;
                }

                if (!HasExternalPredecessor(board, occupiedByArrow, index, candidate, owner))
                    return candidate;
            }

            return !HasExternalPredecessor(board, occupiedByArrow, index, bestFallback, owner)
                ? bestFallback
                : currentInDir;
        }

        static bool HasExternalPredecessor(
            BoardState board,
            int[] occupiedByArrow,
            int index,
            Dir inDir,
            int owner)
        {
            Vector2Int pos = IndexToPos(index, board.width);
            Vector2Int prev = pos + DirToOffset(inDir);

            if (!board.InBounds(prev.x, prev.y))
                return false;

            int prevIndex = board.Index(prev.x, prev.y);
            if (occupiedByArrow[prevIndex] == owner)
                return false;

            var prevTile = board.tiles[prevIndex];
            return prevTile.type == TileType.Arrow && prevTile.arrow.outDir == Opposite(inDir);
        }

        public static Vector2Int IndexToPos(int index, int width)
        {
            return new Vector2Int(index % width, index / width);
        }

        public static int PosToIndex(Vector2Int pos, int width)
        {
            return pos.x + pos.y * width;
        }

        static bool IsCardinalStep(Vector2Int delta)
        {
            return Mathf.Abs(delta.x) + Mathf.Abs(delta.y) == 1;
        }

        static Dir DirFromDelta(Vector2Int delta)
        {
            if (delta == Vector2Int.up) return Dir.Up;
            if (delta == Vector2Int.right) return Dir.Right;
            if (delta == Vector2Int.down) return Dir.Down;
            return Dir.Left;
        }

        static Vector2Int DirToOffset(Dir d) => d switch
        {
            Dir.Up => new Vector2Int(0, 1),
            Dir.Right => new Vector2Int(1, 0),
            Dir.Down => new Vector2Int(0, -1),
            _ => new Vector2Int(-1, 0),
        };

        static Dir Opposite(Dir d) => (Dir)(((int)d + 2) & 3);
    }
}
