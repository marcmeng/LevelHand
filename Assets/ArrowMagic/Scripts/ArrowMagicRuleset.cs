using System;
using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public enum TileType : byte { Empty, Arrow, Block }
    public enum Dir : byte { Up, Right, Down, Left }

    public enum SignalTravelMode : byte
    {
        ArrowToArrowOnly = 0, // chain-only
        ThroughEmpty = 1      // Arrow Magic style
    }

    [Serializable]
    public struct Arrow
    {
        public Dir inDir;
        public Dir outDir;

        public Arrow(Dir inDir, Dir outDir)
        {
            this.inDir = inDir;
            this.outDir = outDir;
        }

        public Arrow RotCW() => new Arrow(RotateCW(inDir), RotateCW(outDir));
        public static Dir RotateCW(Dir d) => (Dir)(((int)d + 1) & 3);
    }

    [Serializable]
    public struct TileState
    {
        public TileType type;
        public Arrow arrow;

        public static TileState Empty() => new TileState { type = TileType.Empty };
        public static TileState Block() => new TileState { type = TileType.Block };
        public static TileState Arrow(Dir inDir, Dir outDir) =>
            new TileState { type = TileType.Arrow, arrow = new Arrow(inDir, outDir) };
    }

    [Serializable]
    public sealed class BoardState
    {
        public readonly int width;
        public readonly int height;
        public TileState[] tiles;

        public BoardState(int width, int height)
        {
            this.width = width;
            this.height = height;
            tiles = new TileState[width * height];
        }

        public bool InBounds(int x, int y) => (uint)x < (uint)width && (uint)y < (uint)height;
        public int Index(int x, int y) => x + y * width;

        public TileState Get(int x, int y) => tiles[Index(x, y)];
        public void Set(int x, int y, TileState t) => tiles[Index(x, y)] = t;

        public bool AnyArrows()
        {
            for (int i = 0; i < tiles.Length; i++)
                if (tiles[i].type == TileType.Arrow) return true;
            return false;
        }
    }

    public readonly struct Move
    {
        public readonly Vector2Int pos;
        public Move(Vector2Int pos) { this.pos = pos; }
    }

    public readonly struct CellChange
    {
        public readonly int index;
        public readonly TileState before;
        public readonly TileState after;

        public CellChange(int index, TileState before, TileState after)
        {
            this.index = index;
            this.before = before;
            this.after = after;
        }
    }

    public sealed class MoveDelta
    {
        public readonly List<CellChange> changes = new(32);

        public void Undo(BoardState s)
        {
            for (int i = changes.Count - 1; i >= 0; i--)
                s.tiles[changes[i].index] = changes[i].before;
        }
    }

    public interface IRuleset
    {
        bool TryApplyMove(BoardState s, in Move m, out MoveDelta delta);
        bool IsSolved(BoardState s);
        IEnumerable<Move> GetLegalMoves(BoardState s);
    }

    [Serializable]
    public sealed class ArrowMagicRulesetConfig
    {
        public SignalTravelMode travelMode = SignalTravelMode.ThroughEmpty;

        [Tooltip("0 = auto. Use only if you want a tighter or looser safety cap.")]
        public int maxStepsOverride = 0;
    }

    public sealed class ArrowMagicRuleset : IRuleset
    {
        readonly ArrowMagicRulesetConfig _cfg;

        public ArrowMagicRuleset(ArrowMagicRulesetConfig cfg)
        {
            _cfg = cfg ?? new ArrowMagicRulesetConfig();
        }

        public bool TryApplyMove(BoardState s, in Move m, out MoveDelta delta)
        {
            delta = null;

            if (!s.InBounds(m.pos.x, m.pos.y)) return false;

            int startIdx = s.Index(m.pos.x, m.pos.y);
            var startTile = s.tiles[startIdx];
            if (startTile.type != TileType.Arrow) return false;

            var d = new MoveDelta();

            // Collect entire chain (both directions), but keep your existing "exit required?" rule.
            var visitedForward = new List<int>(32);
            bool exited = Propagate(
                s, m.pos, startTile.arrow.inDir,
                _cfg.travelMode, _cfg.maxStepsOverride,
                visitedForward
            );

            var chain = new HashSet<int>(64);
            // IMPORTANT: chain membership is always adjacent-only. Do NOT scan through empties.
            ArrowChainUtility.CollectFullChain(s, m.pos, _cfg.maxStepsOverride, chain);

            if (exited)
            {
                foreach (int idx in chain)
                {
                    var before = s.tiles[idx];
                    if (before.type != TileType.Arrow)
                        continue;

                    var after = TileState.Empty();
                    s.tiles[idx] = after;
                    d.changes.Add(new CellChange(idx, before, after));
                }
            }

            if (d.changes.Count == 0) return false;

            delta = d;
            return true;
        }

        public bool IsSolved(BoardState s) => !s.AnyArrows();

        public IEnumerable<Move> GetLegalMoves(BoardState s)
        {
            for (int y = 0; y < s.height; y++)
            for (int x = 0; x < s.width; x++)
            {
                int i = s.Index(x, y);
                if (s.tiles[i].type == TileType.Arrow)
                    yield return new Move(new Vector2Int(x, y));
            }
        }

        static bool Propagate(
            BoardState s,
            Vector2Int start,
            Dir entryDir,
            SignalTravelMode mode,
            int maxStepsOverride,
            List<int> visited)
        {
            visited.Clear();

            var pos = start;

            Dir travelDir = default;
            bool hasTravelDir = false;

            var seen = new HashSet<int>();

            int autoMax = 1 + s.width * s.height * (mode == SignalTravelMode.ThroughEmpty ? 4 : 1);
            int maxSteps = (maxStepsOverride > 0) ? maxStepsOverride : autoMax;

            for (int step = 0; step < maxSteps; step++)
            {
                if (!s.InBounds(pos.x, pos.y))
                    return true; // exited board

                int idx = s.Index(pos.x, pos.y);
                int key = (idx << 2) | ((int)entryDir & 3);
                if (!seen.Add(key)) break;

                var t = s.tiles[idx];

                if (t.type == TileType.Block)
                    break;

                if (t.type == TileType.Empty)
                {
                    if (mode == SignalTravelMode.ArrowToArrowOnly || !hasTravelDir)
                        break;
                    
                    Vector2Int nextPos = pos + DirToOffset(travelDir);
                    if (s.InBounds(nextPos.x, nextPos.y) && s.tiles[s.Index(nextPos.x, nextPos.y)].type == TileType.Arrow)
                        break;

                    pos += DirToOffset(travelDir);
                    entryDir = Opposite(travelDir);
                    continue;
                }

                // Must be Arrow and must enter correctly
                if (t.type != TileType.Arrow || t.arrow.inDir != entryDir)
                    break;

                visited.Add(idx);

                travelDir = t.arrow.outDir;
                hasTravelDir = true;

                pos += DirToOffset(travelDir);
                entryDir = Opposite(travelDir);
            }

            return false;
        }

        static Vector2Int DirToOffset(Dir d) => d switch
        {
            Dir.Up => new Vector2Int(0, 1),
            Dir.Right => new Vector2Int(1, 0),
            Dir.Down => new Vector2Int(0, -1),
            _ => new Vector2Int(-1, 0),
        };

        static Dir Opposite(Dir d) => (Dir)(((int)d + 2) & 3);

        public bool TryGetSignalPath(BoardState s, Vector2Int startPos, out List<Vector2Int> path, out bool exited)
        {
            path = new List<Vector2Int>(64);
            exited = false;

            if (!s.InBounds(startPos.x, startPos.y)) return false;

            int startIdx = s.Index(startPos.x, startPos.y);
            var startTile = s.tiles[startIdx];
            if (startTile.type != TileType.Arrow) return false;

            // IMPORTANT: match your gameplay behavior (rotate first or not)
            Arrow a = startTile.arrow;

            // Simulate the same propagation, but record positions (including empties if ThroughEmpty)
            return TracePath(s, startPos, a.inDir, _cfg.travelMode, _cfg.maxStepsOverride, path, out exited);
        }

        static bool TracePath(
            BoardState s,
            Vector2Int start,
            Dir entryDir,
            SignalTravelMode mode,
            int maxStepsOverride,
            List<Vector2Int> path,
            out bool exited)
        {
            path.Clear();
            exited = false;

            Vector2Int pos = start;
            Dir travelDir = default;
            bool hasTravelDir = false;

            var seen = new HashSet<int>();

            int autoMax = 1 + s.width * s.height * (mode == SignalTravelMode.ThroughEmpty ? 4 : 1);
            int maxSteps = (maxStepsOverride > 0) ? maxStepsOverride : autoMax;

            for (int step = 0; step < maxSteps; step++)
            {
                // record the current in-bounds cell we are visiting
                if (!s.InBounds(pos.x, pos.y))
                {
                    exited = true;
                    return true;
                }

                path.Add(pos);

                int idx = s.Index(pos.x, pos.y);
                int key = (idx << 2) | ((int)entryDir & 3);
                if (!seen.Add(key)) return true; // loop; treat as "traced" but not exited

                var t = s.tiles[idx];

                if (t.type == TileType.Block) return true;

                if (t.type == TileType.Empty)
                {
                    if (mode == SignalTravelMode.ArrowToArrowOnly || !hasTravelDir) return true;
                    
                    Vector2Int nextPos = pos + DirToOffset(travelDir);
                    if (s.InBounds(nextPos.x, nextPos.y) && s.tiles[s.Index(nextPos.x, nextPos.y)].type == TileType.Arrow)
                        return true;

                    pos += DirToOffset(travelDir);
                    entryDir = Opposite(travelDir);
                    continue;
                }

                if (t.type != TileType.Arrow || t.arrow.inDir != entryDir) return true;

                travelDir = t.arrow.outDir;
                hasTravelDir = true;

                pos += DirToOffset(travelDir);
                entryDir = Opposite(travelDir);
            }

            return true;
        }
    }
}
