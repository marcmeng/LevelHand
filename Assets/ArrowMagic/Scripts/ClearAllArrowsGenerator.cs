

using System;
using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    [Serializable]
    public struct ClearAllSpec
    {
        public int width;
        public int height;
        public int seed;

        [Range(0.05f, 1.0f)] public float arrowCoverage;

        public int minPathLen;
        public int maxPathLen;
        public float outwardBias;

        public bool avoidOverlap;

        public static ClearAllSpec Default(int w, int h, int seed) => new ClearAllSpec
        {
            width = w,
            height = h,
            seed = seed,
            arrowCoverage = 0.95f,
            minPathLen = Mathf.Max(4, (w + h) / 2),
            maxPathLen = Mathf.Max(8, (w * h) / 2),
            outwardBias = 0.50f,
            avoidOverlap = true
        };
    }

    public sealed class ClearAllArrowsGenerator : ILevelGenerator
    {
        public BoardState Generate(LevelSpec spec)
        {
            var cs = ClearAllSpec.Default(spec.width, spec.height, spec.seed);
            cs.arrowCoverage = Mathf.Clamp01(spec.arrowFill);
            cs.minPathLen = spec.minPathLen;
            cs.maxPathLen = spec.maxPathLen;
            cs.outwardBias = Mathf.Clamp01(1.0f - spec.twistiness);

            return Generate(cs, spec.canSpawnHere);
        }

        public BoardState Generate(ClearAllSpec spec, bool[] canSpawnHere)
        {
            var rng = new System.Random(spec.seed);
            int minChainLength = Mathf.Max(2, spec.minPathLen);
            int maxChainLength = Mathf.Max(minChainLength, spec.maxPathLen);

            var s = new BoardState(spec.width, spec.height);
            for (int i = 0; i < s.tiles.Length; i++)
                s.tiles[i] = TileState.Empty();

            PlaceBlocks(s, spec, rng);
            
            int totalAllowed = 0;
            if (canSpawnHere == null)
            {
                totalAllowed = s.width * s.height;
            }
            else
            {
                for (int i = 0; i < canSpawnHere.Length; i++)
                    if (canSpawnHere[i]) totalAllowed++;
            }

            int minimumTargetArrows = totalAllowed >= minChainLength ? minChainLength : Mathf.Max(1, totalAllowed);
            int targetArrows = Mathf.Clamp(
                Mathf.RoundToInt(totalAllowed * spec.arrowCoverage),
                minimumTargetArrows,
                Mathf.Max(1, totalAllowed)
            );

            var used = new HashSet<int>();
            int placedArrows = 0;
            int guard = targetArrows * 8;

            bool Allowed(int idx) => canSpawnHere == null || (idx >= 0 && idx < canSpawnHere.Length && canSpawnHere[idx]);
            
            while (placedArrows < targetArrows && guard-- > 0)
            {
                int remainingTargetArrows = targetArrows - placedArrows;
                if (remainingTargetArrows < minChainLength)
                    break;

                // NEW: only choose allowed start cells
                var start = FindRandomCell(s, rng, (t, idx) => t.type == TileType.Empty && Allowed(idx));
                if (!s.InBounds(start.x, start.y)) break;

                var path = BuildExitPath(s, start, rng, minChainLength, maxChainLength, spec.outwardBias, used, spec.avoidOverlap, Allowed);
                if (path.Count == 0) continue;

                int pathTileCount = Mathf.Min(path.Count, remainingTargetArrows);
                if (pathTileCount < minChainLength)
                    continue;

                for (int i = 0; i < pathTileCount; i++)
                {
                    var n = path[i];
                    int idx = s.Index(n.pos.x, n.pos.y);

                    // NEW: never place arrow tiles on disallowed cells
                    if (!Allowed(idx)) break;

                    if (s.tiles[idx].type == TileType.Block) break;
                    if (s.tiles[idx].type != TileType.Empty && spec.avoidOverlap) continue;

                    if (s.tiles[idx].type == TileType.Empty)
                    {
                        s.tiles[idx] = TileState.Arrow(n.entryDir, n.outDir);
                        used.Add(idx);
                        placedArrows++;
                        if (placedArrows >= targetArrows) break;
                    }
                }
            }

            if (spec.arrowCoverage >= 0.999f && minChainLength <= 1)
            {
                FillRemainingWithSingleArrows(s, canSpawnHere);
            }

            EnforceMaxChainLength(s, maxChainLength);
            RemoveShortArrowChains(s, minChainLength);

            if (!s.AnyArrows())
            {
                PlaceFallbackArrowChain(s, canSpawnHere, minChainLength);
            }

            // Debug: U-turns (in==out) are still suspicious
            for (int i = 0; i < s.tiles.Length; i++)
            {
                var t = s.tiles[i];
                if (t.type == TileType.Arrow && t.arrow.inDir == t.arrow.outDir)
                {
                    int x = i % s.width;
                    int y = i / s.width;
                    Debug.LogWarning($"U-turn arrow at ({x},{y}) seed={spec.seed}");
                }
            }

            return s;
        }
        
        static void FillRemainingWithSingleArrows(BoardState s, bool[] canSpawnHere)
        {
            bool Allowed(int idx) => canSpawnHere == null || (idx >= 0 && idx < canSpawnHere.Length && canSpawnHere[idx]);

            for (int y = 0; y < s.height; y++)
            {
                for (int x = 0; x < s.width; x++)
                {
                    int idx = s.Index(x, y);
                    if (!Allowed(idx)) continue;
                    if (s.tiles[idx].type != TileType.Empty) continue;

                    var pos = new Vector2Int(x, y);
                    Dir outDir = OutwardDirTowardNearestEdge(s, pos);
                    Dir entryDir = Opposite(outDir);

                    s.tiles[idx] = TileState.Arrow(entryDir, outDir);
                }
            }
        }

        // ----------------------------
        // Path node + build
        // ----------------------------

        struct PathNode
        {
            public Vector2Int pos;
            public Dir entryDir;
            public Dir outDir;

            public PathNode(Vector2Int pos, Dir entryDir, Dir outDir)
            {
                this.pos = pos;
                this.entryDir = entryDir;
                this.outDir = outDir;
            }
        }

        static List<PathNode> BuildExitPath(
            BoardState s,
            Vector2Int start,
            System.Random rng,
            int minLen,
            int maxLen,
            float outwardBias,
            HashSet<int> used,
            bool avoidOverlap,
            Func<int,bool> allowed)
        {
            var path = new List<PathNode>(maxLen);

            Dir entry = (Dir)rng.Next(0, 4);

            int targetLen = rng.Next(
                Mathf.Clamp(minLen, 2, s.width * s.height),
                Mathf.Clamp(maxLen, minLen + 1, s.width * s.height)
            );

            var localVisited = new HashSet<int>();
            Vector2Int pos = start;

            for (int step = 0; step < targetLen; step++)
            {
                if (!s.InBounds(pos.x, pos.y)) break;

                int idx = s.Index(pos.x, pos.y);

                if (!allowed(idx)) break;
                
                if (s.tiles[idx].type == TileType.Block) break;
                if (!localVisited.Add(idx)) break;
                if (avoidOverlap && used.Contains(idx)) break;

                Dir outDir = ChooseOutDirBiasedToExit(s, pos, rng, outwardBias);
                outDir = EnsureNoUTurn(rng, entry, outDir);

                path.Add(new PathNode(pos, entry, outDir));

                Vector2Int next = pos + DirToOffset(outDir);
                entry = Opposite(outDir);
                pos = next;

                if (!s.InBounds(pos.x, pos.y))
                    break;
            }

            if (path.Count < minLen) return new List<PathNode>(0);

            ForceExitOnLastStepIfNeeded(s, path, rng);
            if (!EnsureStraightExitTail(s, path, Mathf.Max(1, maxLen)))
                return new List<PathNode>(0);

            return path;
        }

        static void ForceExitOnLastStepIfNeeded(BoardState s, List<PathNode> path, System.Random rng)
        {
            if (path.Count == 0) return;
            var last = path[^1];
            var next = last.pos + DirToOffset(last.outDir);
            if (!s.InBounds(next.x, next.y)) return;

            Dir forced = OutwardDirTowardNearestEdge(s, last.pos);
            forced = EnsureNoUTurn(rng, last.entryDir, forced);

            path[^1] = new PathNode(last.pos, last.entryDir, forced);
        }

        // ----------------------------
        // NEW: Straight-exit tail enforcement
        // ----------------------------

        static bool EnsureStraightExitTail(BoardState s, List<PathNode> path, int maxLen)
        {
            if (path == null || path.Count == 0) return false;

            var occupied = new HashSet<int>(path.Count);
            for (int i = 0; i < path.Count; i++)
                occupied.Add(s.Index(path[i].pos.x, path[i].pos.y));

            int maxBacktrack = Mathf.Min(12, path.Count - 1);
            for (int back = 0; back <= maxBacktrack; back++)
            {
                int endIndex = path.Count - 1 - back;
                if (endIndex < 0) break;

                while (path.Count - 1 > endIndex)
                {
                    var removed = path[^1];
                    occupied.Remove(s.Index(removed.pos.x, removed.pos.y));
                    path.RemoveAt(path.Count - 1);
                }

                var last = path[^1];

                // Straight direction is fixed by entryDir
                Dir straightDir = Opposite(last.entryDir);

                // Make last node straight
                path[^1] = new PathNode(last.pos, last.entryDir, straightDir);

                Vector2Int pos = last.pos;
                int cap = s.width * s.height;

                for (int i = 0; i < cap; i++)
                {
                    Vector2Int nextPos = pos + DirToOffset(straightDir);

                    if (!s.InBounds(nextPos.x, nextPos.y))
                    {
                        return true;
                    }

                    if (path.Count >= maxLen) break;

                    int nIdx = s.Index(nextPos.x, nextPos.y);

                    if (s.tiles[nIdx].type == TileType.Block) break;
                    if (occupied.Contains(nIdx)) break;

                    Dir nextEntry = Opposite(straightDir);
                    path.Add(new PathNode(nextPos, nextEntry, straightDir));
                    occupied.Add(nIdx);

                    pos = nextPos;
                }
            }

            return false;
        }

        static void EnforceMaxChainLength(BoardState s, int maxChainLen)
        {
            if (s == null || maxChainLen <= 0) return;

            var chainSet = new HashSet<int>();
            int guard = Mathf.Max(1, s.width * s.height * 4);

            while (guard-- > 0 && TryFindLongChain(s, maxChainLen, chainSet, out var start))
            {
                if (!ArrowChainUtility.TryBuildOrderedChain(
                        s, chainSet, start,
                        out var ordered, out _, out _, out _)
                    || ordered.Count <= maxChainLen)
                {
                    break;
                }

                BreakChainAfter(s, ordered, maxChainLen - 1);
            }
        }

        static void RemoveShortArrowChains(BoardState s, int minChainLen)
        {
            if (s == null || minChainLen <= 1) return;

            var visited = new HashSet<int>();
            var chainSet = new HashSet<int>();
            int guard = Mathf.Max(1, s.width * s.height * 4);
            bool removed;

            do
            {
                removed = false;
                visited.Clear();

                for (int i = 0; i < s.tiles.Length; i++)
                {
                    if (visited.Contains(i) || s.tiles[i].type != TileType.Arrow)
                        continue;

                    var pos = new Vector2Int(i % s.width, i / s.width);
                    ArrowChainUtility.CollectFullChain(s, pos, 0, chainSet);

                    foreach (int idx in chainSet)
                        visited.Add(idx);

                    if (chainSet.Count >= minChainLen)
                        continue;

                    foreach (int idx in chainSet)
                        s.tiles[idx] = TileState.Empty();

                    removed = true;
                }
            } while (removed && guard-- > 0);
        }

        static void PlaceFallbackArrowChain(BoardState s, bool[] canSpawnHere, int minChainLen)
        {
            bool Allowed(int idx) => canSpawnHere == null || (idx >= 0 && idx < canSpawnHere.Length && canSpawnHere[idx]);
            int chainLen = Mathf.Max(1, minChainLen);

            for (int dirIndex = 0; dirIndex < 4; dirIndex++)
            {
                Dir outDir = (Dir)dirIndex;
                Vector2Int offset = DirToOffset(outDir);

                for (int y = 0; y < s.height; y++)
                for (int x = 0; x < s.width; x++)
                {
                    var start = new Vector2Int(x, y);
                    var afterLast = start + new Vector2Int(offset.x * chainLen, offset.y * chainLen);
                    if (s.InBounds(afterLast.x, afterLast.y))
                        continue;

                    bool canPlace = true;
                    for (int step = 0; step < chainLen; step++)
                    {
                        var pos = start + new Vector2Int(offset.x * step, offset.y * step);
                        if (!s.InBounds(pos.x, pos.y))
                        {
                            canPlace = false;
                            break;
                        }

                        int idx = s.Index(pos.x, pos.y);
                        if (!Allowed(idx) || s.tiles[idx].type == TileType.Block)
                        {
                            canPlace = false;
                            break;
                        }
                    }

                    if (!canPlace)
                        continue;

                    Dir entryDir = Opposite(outDir);
                    for (int step = 0; step < chainLen; step++)
                    {
                        var pos = start + new Vector2Int(offset.x * step, offset.y * step);
                        s.Set(pos.x, pos.y, TileState.Arrow(entryDir, outDir));
                    }

                    return;
                }
            }

            for (int i = 0; i < s.tiles.Length; i++)
            {
                if (!Allowed(i) || s.tiles[i].type == TileType.Block)
                    continue;

                int x = i % s.width;
                int y = i / s.width;
                var pos = new Vector2Int(x, y);
                Dir outDir = OutwardDirTowardNearestEdge(s, pos);
                s.tiles[i] = TileState.Arrow(Opposite(outDir), outDir);
                return;
            }
        }

        static bool TryFindLongChain(
            BoardState s,
            int maxChainLen,
            HashSet<int> chainSet,
            out Vector2Int start)
        {
            start = default;
            var visited = new HashSet<int>();

            for (int i = 0; i < s.tiles.Length; i++)
            {
                if (visited.Contains(i) || s.tiles[i].type != TileType.Arrow)
                    continue;

                var pos = new Vector2Int(i % s.width, i / s.width);
                ArrowChainUtility.CollectFullChain(s, pos, 0, chainSet);

                foreach (int idx in chainSet)
                    visited.Add(idx);

                if (chainSet.Count > maxChainLen)
                {
                    start = pos;
                    return true;
                }
            }

            return false;
        }

        static void BreakChainAfter(BoardState s, List<Vector2Int> ordered, int breakIndex)
        {
            if (ordered == null || ordered.Count < 2)
                return;

            breakIndex = Mathf.Clamp(breakIndex, 0, ordered.Count - 2);

            Vector2Int from = ordered[breakIndex];
            Vector2Int to = ordered[breakIndex + 1];
            int fromIdx = s.Index(from.x, from.y);
            var fromTile = s.tiles[fromIdx];
            if (fromTile.type != TileType.Arrow)
                return;

            Dir directionToNext = DirFromOffset(to - from);
            if (TryPickBreakingOutDir(s, from, fromTile.arrow.inDir, directionToNext, out Dir newOutDir))
            {
                s.tiles[fromIdx] = TileState.Arrow(fromTile.arrow.inDir, newOutDir);
                return;
            }

            int toIdx = s.Index(to.x, to.y);
            var toTile = s.tiles[toIdx];
            if (toTile.type != TileType.Arrow)
                return;

            Dir blockedEntry = Opposite(directionToNext);
            for (int i = 0; i < 4; i++)
            {
                Dir newInDir = (Dir)i;
                if (newInDir == blockedEntry || newInDir == toTile.arrow.outDir)
                    continue;

                s.tiles[toIdx] = TileState.Arrow(newInDir, toTile.arrow.outDir);
                return;
            }
        }

        static bool TryPickBreakingOutDir(
            BoardState s,
            Vector2Int pos,
            Dir entryDir,
            Dir directionToNext,
            out Dir outDir)
        {
            for (int i = 0; i < 4; i++)
            {
                Dir candidate = (Dir)i;
                if (candidate == entryDir || candidate == directionToNext)
                    continue;

                Vector2Int next = pos + DirToOffset(candidate);
                if (!s.InBounds(next.x, next.y))
                {
                    outDir = candidate;
                    return true;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                Dir candidate = (Dir)i;
                if (candidate == entryDir || candidate == directionToNext)
                    continue;

                Vector2Int next = pos + DirToOffset(candidate);
                if (!s.InBounds(next.x, next.y))
                {
                    outDir = candidate;
                    return true;
                }

                var nextTile = s.tiles[s.Index(next.x, next.y)];
                if (nextTile.type != TileType.Arrow || nextTile.arrow.inDir != Opposite(candidate))
                {
                    outDir = candidate;
                    return true;
                }
            }

            outDir = default;
            return false;
        }

        // ----------------------------
        // Outward bias helpers
        // ----------------------------

        static Dir ChooseOutDirBiasedToExit(BoardState s, Vector2Int p, System.Random rng, float outwardBias)
        {
            if (rng.NextDouble() < Mathf.Clamp01(outwardBias))
                return OutwardDirTowardNearestEdge(s, p);

            return (Dir)rng.Next(0, 4);
        }

        static Dir OutwardDirTowardNearestEdge(BoardState s, Vector2Int p)
        {
            int left = p.x;
            int right = (s.width - 1) - p.x;
            int down = p.y;
            int up = (s.height - 1) - p.y;

            int min = left; Dir d = Dir.Left;
            if (right < min) { min = right; d = Dir.Right; }
            if (down < min) { min = down; d = Dir.Down; }
            if (up < min) { min = up; d = Dir.Up; }

            return d;
        }

        // ----------------------------
        // Blocks
        // ----------------------------

        static void PlaceBlocks(BoardState s, ClearAllSpec spec, System.Random rng)
        {
            int total = s.width * s.height;
            int blocks = Mathf.Clamp(Mathf.RoundToInt(total * 0), 0, total / 3);

            int attempts = blocks * 10;
            int placed = 0;

            while (placed < blocks && attempts-- > 0)
            {
                int x = rng.Next(0, s.width);
                int y = rng.Next(0, s.height);

                if (x == 0 || y == 0 || x == s.width - 1 || y == s.height - 1)
                    continue;

                int idx = s.Index(x, y);
                if (s.tiles[idx].type != TileType.Empty) continue;

                s.tiles[idx] = TileState.Block();
                placed++;
            }
        }

        // ----------------------------
        // Helpers
        // ----------------------------

        static Vector2Int FindRandomCell(BoardState s, System.Random rng, Func<TileState,int,bool> predicate)
        {
            for (int i = 0; i < 64; i++)
            {
                int x = rng.Next(0, s.width);
                int y = rng.Next(0, s.height);
                int idx = s.Index(x, y);
                if (predicate(s.Get(x, y), idx)) return new Vector2Int(x, y);
            }

            for (int y = 0; y < s.height; y++)
            for (int x = 0; x < s.width; x++)
            {
                int idx = s.Index(x, y);
                if (predicate(s.Get(x, y), idx)) return new Vector2Int(x, y);
            }

            return new Vector2Int(-1, -1);
        }


        static Vector2Int DirToOffset(Dir d) => d switch
        {
            Dir.Up => new Vector2Int(0, 1),
            Dir.Right => new Vector2Int(1, 0),
            Dir.Down => new Vector2Int(0, -1),
            _ => new Vector2Int(-1, 0),
        };

        static Dir DirFromOffset(Vector2Int offset)
        {
            if (offset.x > 0) return Dir.Right;
            if (offset.x < 0) return Dir.Left;
            if (offset.y > 0) return Dir.Up;
            return Dir.Down;
        }

        static Dir Opposite(Dir d) => (Dir)(((int)d + 2) & 3);

        static Dir RandomDirExcept(System.Random rng, Dir banned)
        {
            int r = rng.Next(0, 3);
            int b = (int)banned;
            int v = r >= b ? r + 1 : r;
            return (Dir)v;
        }

        static Dir EnsureNoUTurn(System.Random rng, Dir entryDir, Dir outDir)
        {
            return (outDir == entryDir) ? RandomDirExcept(rng, entryDir) : outDir;
        }
    }
}
