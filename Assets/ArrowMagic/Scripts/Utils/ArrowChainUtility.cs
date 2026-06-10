using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    /// <summary>
    /// Utilities for determining *chain membership* and *ordered chain traversal*.
    ///
    /// Rule: Arrows connect ONLY to the immediately adjacent neighbor cell in the relevant direction.
    /// Empty cells always break the chain (no scanning/line-of-sight linking).
    ///
    /// Note: SignalTravelMode is kept for API compatibility, but MUST NOT change chain adjacency.
    /// </summary>
    public static class ArrowChainUtility
    {
        // --------------------------------------------------------------------
        // Existing API (needed by Arrow MagicRuleset + IntroEntryBuilder)
        // --------------------------------------------------------------------

        public static void CollectFullChain(
            BoardState s,
            Vector2Int startPos,
            int maxStepsOverride,
            HashSet<int> chain // output: indices of all arrows in the chain
        )
        {
            chain.Clear();

            // Safety cap. Mode must not affect adjacency, but we keep the old autoMax shape.
            int autoMax = 1 + s.width * s.height * 4; //(mode == SignalTravelMode.ThroughEmpty ? 4 : 1);
            int maxSteps = (maxStepsOverride > 0) ? maxStepsOverride : autoMax;

            // local helper: step exactly one cell; do NOT scan through empties
            static bool StepToAdjacentArrow(BoardState s, ref Vector2Int pos, Dir travelDir)
            {
                pos += DirToOffset(travelDir);
                return s.InBounds(pos.x, pos.y) && s.tiles[s.Index(pos.x, pos.y)].type == TileType.Arrow;
            }

            // --------------- forward walk (outDir) ---------------
            {
                Vector2Int pos = startPos;

                for (int step = 0; step < maxSteps; step++)
                {
                    if (!s.InBounds(pos.x, pos.y)) break;

                    int idx = s.Index(pos.x, pos.y);
                    var cur = s.tiles[idx];
                    if (cur.type != TileType.Arrow) break;

                    chain.Add(idx);

                    Dir outDir = cur.arrow.outDir;

                    Vector2Int nextPos = pos;
                    if (!StepToAdjacentArrow(s, ref nextPos, outDir)) break;

                    var next = s.tiles[s.Index(nextPos.x, nextPos.y)];
                    if (next.type != TileType.Arrow) break;

                    // connection check: next must accept entry from our outDir
                    if (next.arrow.inDir != Opposite(outDir)) break;

                    pos = nextPos;
                }
            }

            // --------------- backward walk (inDir) ---------------
            {
                Vector2Int pos = startPos;

                for (int step = 0; step < maxSteps; step++)
                {
                    if (!s.InBounds(pos.x, pos.y)) break;

                    int idx = s.Index(pos.x, pos.y);
                    var cur = s.tiles[idx];
                    if (cur.type != TileType.Arrow) break;

                    chain.Add(idx);

                    Dir backDir = cur.arrow.inDir; // upstream tile lies in this direction

                    Vector2Int prevPos = pos;
                    if (!StepToAdjacentArrow(s, ref prevPos, backDir)) break;

                    var prev = s.tiles[s.Index(prevPos.x, prevPos.y)];
                    if (prev.type != TileType.Arrow) break;

                    // connection check: prev must point into us
                    if (prev.arrow.outDir != Opposite(backDir)) break;

                    pos = prevPos;
                }
            }
        }

        // --------------------------------------------------------------------
        // New API (replacement for BoardView2D.TryBuildOrderedChain)
        // --------------------------------------------------------------------

        /// <summary>
        /// Orders a chain into a tail->head list of cells.
        /// Returns headPos/headOutDir for extending off-board / collision tracing.
        /// If no tail exists, isLoop will be true and we walk from preferredStart.
        /// </summary>
        public static bool BuildOrderedChain(
            BoardState s,
            HashSet<int> chainSet,
            Vector2Int preferredStart,
            List<Vector2Int> ordered,
            out Vector2Int headPos,
            out Dir headOutDir,
            out bool isLoop)
        {
            ordered.Clear();
            headPos = preferredStart;
            headOutDir = Dir.Right;
            isLoop = false;

            if (s == null || chainSet == null || chainSet.Count == 0)
                return false;

            // Find a tail: a cell in the chain that has no valid predecessor in the chain.
            bool foundTail = false;
            Vector2Int tailPos = preferredStart;

            foreach (int idx in chainSet)
            {
                int x = idx % s.width;
                int y = idx / s.width;

                if (!HasValidPredecessor(s, x, y, chainSet))
                {
                    tailPos = new Vector2Int(x, y);
                    foundTail = true;
                    break;
                }
            }

            // If no tail exists, we have a loop (or malformed chain).
            if (!foundTail)
            {
                isLoop = true;

                Vector2Int start = preferredStart;
                if (!s.InBounds(start.x, start.y) || !chainSet.Contains(s.Index(start.x, start.y)))
                {
                    foreach (int idx in chainSet)
                    {
                        start = new Vector2Int(idx % s.width, idx / s.width);
                        break;
                    }
                }

                var cur = start;
                int cap = chainSet.Count + 2;

                for (int i = 0; i < cap; i++)
                {
                    if (!s.InBounds(cur.x, cur.y)) break;

                    int cidx = s.Index(cur.x, cur.y);
                    if (!chainSet.Contains(cidx)) break;

                    ordered.Add(cur);

                    var t = s.tiles[cidx];
                    if (t.type != TileType.Arrow) break;

                    Vector2Int next = cur + DirToOffset(t.arrow.outDir);

                    if (next == start)
                    {
                        headPos = cur;
                        headOutDir = t.arrow.outDir;
                        return true;
                    }

                    cur = next;
                }

                if (ordered.Count > 0)
                {
                    var last = ordered[ordered.Count - 1];
                    int lastIdx = s.Index(last.x, last.y);
                    headPos = last;
                    headOutDir = s.tiles[lastIdx].arrow.outDir;
                    return true;
                }

                return false;
            }

            // Normal (non-loop) tail->head walk
            {
                var cur = tailPos;

                for (int safety = 0; safety < chainSet.Count + 2; safety++)
                {
                    if (!s.InBounds(cur.x, cur.y)) break;

                    int cidx = s.Index(cur.x, cur.y);
                    if (!chainSet.Contains(cidx)) break;

                    ordered.Add(cur);

                    var t = s.tiles[cidx];
                    if (t.type != TileType.Arrow) break;

                    Vector2Int next = cur + DirToOffset(t.arrow.outDir);
                    if (!s.InBounds(next.x, next.y))
                    {
                        headPos = cur;
                        headOutDir = t.arrow.outDir;
                        return true;
                    }

                    int nidx = s.Index(next.x, next.y);
                    if (!chainSet.Contains(nidx))
                    {
                        headPos = cur;
                        headOutDir = t.arrow.outDir;
                        return true;
                    }

                    cur = next;
                }

                if (ordered.Count > 0)
                {
                    var last = ordered[ordered.Count - 1];
                    int lastIdx = s.Index(last.x, last.y);
                    headPos = last;
                    headOutDir = s.tiles[lastIdx].arrow.outDir;
                    return true;
                }

                return false;
            }
        }

        static bool HasValidPredecessor(BoardState s, int x, int y, HashSet<int> chainSet)
        {
            int idx = s.Index(x, y);
            var t = s.tiles[idx];
            if (t.type != TileType.Arrow) return false;

            // predecessor lies one step "upstream" in inDir direction
            Vector2Int prevPos = new Vector2Int(x, y) + DirToOffset(t.arrow.inDir);

            if (!s.InBounds(prevPos.x, prevPos.y)) return false;

            int pIdx = s.Index(prevPos.x, prevPos.y);
            if (!chainSet.Contains(pIdx)) return false;

            var p = s.tiles[pIdx];
            if (p.type != TileType.Arrow) return false;

            // predecessor must point into us
            return p.arrow.outDir == Opposite(t.arrow.inDir);
        }

        // --------------------------------------------------------------------
        // Shared helpers
        // --------------------------------------------------------------------

        static Vector2Int DirToOffset(Dir d) => d switch
        {
            Dir.Up => new Vector2Int(0, 1),
            Dir.Right => new Vector2Int(1, 0),
            Dir.Down => new Vector2Int(0, -1),
            _ => new Vector2Int(-1, 0),
        };

        static Dir Opposite(Dir d) => (Dir)(((int)d + 2) & 3);
        
        public static bool TryBuildOrderedChain(
            BoardState s,
            HashSet<int> chainSet,
            Vector2Int preferredStart,
            out List<Vector2Int> ordered,
            out Vector2Int headPos,
            out Dir headOutDir,
            out bool isLoop)
        {
            ordered = new List<Vector2Int>(chainSet.Count);
            headPos = default;
            headOutDir = default;
            isLoop = false;

            if (s == null || chainSet == null || chainSet.Count == 0)
                return false;

            // -------- Find tail (non-loop case) --------
            Vector2Int tailPos = default;
            bool foundTail = false;

            foreach (int idx in chainSet)
            {
                int x = idx % s.width;
                int y = idx / s.width;

                if (!HasValidPredecessor(s, x, y, chainSet))
                {
                    tailPos = new Vector2Int(x, y);
                    foundTail = true;
                    break;
                }
            }

            // -------- If no tail, it's a loop --------
            if (!foundTail)
            {
                isLoop = true;

                // Prefer the clicked tile if it's in the loop; otherwise any tile.
                Vector2Int start = preferredStart;
                if (!s.InBounds(start.x, start.y) || !chainSet.Contains(s.Index(start.x, start.y)))
                {
                    foreach (int idx in chainSet)
                    {
                        start = new Vector2Int(idx % s.width, idx / s.width);
                        break;
                    }
                }

                // Walk until we return to start or exceed safety cap
                var visited = new HashSet<int>();
                Vector2Int cur = start;

                for (int safety = 0; safety < chainSet.Count + 2; safety++)
                {
                    int cidx = s.Index(cur.x, cur.y);
                    if (!chainSet.Contains(cidx)) break;
                    if (!visited.Add(cidx)) break;

                    ordered.Add(cur);

                    var t = s.tiles[cidx];
                    if (t.type != TileType.Arrow) break;

                    cur += DirToOffset(t.arrow.outDir);

                    if (cur == start)
                    {
                        headPos = ordered[ordered.Count - 1];
                        headOutDir = t.arrow.outDir;
                        return true;
                    }

                    if (!s.InBounds(cur.x, cur.y)) break;
                }

                if (ordered.Count > 0)
                {
                    var last = ordered[ordered.Count - 1];
                    int lastIdx = s.Index(last.x, last.y);
                    headPos = last;
                    headOutDir = s.tiles[lastIdx].arrow.outDir;
                    return true;
                }

                return false;
            }

            // -------- Tail -> Head walk --------
            {
                Vector2Int cur = tailPos;
                var visited = new HashSet<int>();

                for (int safety = 0; safety < chainSet.Count + 2; safety++)
                {
                    int cidx = s.Index(cur.x, cur.y);
                    if (!chainSet.Contains(cidx)) break;
                    if (!visited.Add(cidx)) { isLoop = true; break; }

                    ordered.Add(cur);

                    var t = s.tiles[cidx];
                    if (t.type != TileType.Arrow) break;

                    Vector2Int next = cur + DirToOffset(t.arrow.outDir);

                    // Head is the last arrow in the set (next is off-board or not in the chain set)
                    if (!s.InBounds(next.x, next.y) || !chainSet.Contains(s.Index(next.x, next.y)))
                    {
                        headPos = cur;
                        headOutDir = t.arrow.outDir;
                        return true;
                    }

                    cur = next;
                }

                if (ordered.Count > 0)
                {
                    var last = ordered[ordered.Count - 1];
                    int lastIdx = s.Index(last.x, last.y);
                    headPos = last;
                    headOutDir = s.tiles[lastIdx].arrow.outDir;
                    return true;
                }

                return false;
            }
        }
    }
}