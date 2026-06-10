using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    /// <summary>
    /// Owns selection input + ghost animation lifecycle.
    /// BoardView2D becomes an orchestrator: it wires this up, but doesn't own the big coroutine anymore.
    /// </summary>
    public sealed class BoardSelectionAnimator2D
    {
        public interface ISelectionOverlay : BoardSnakeRenderer2D.IArrowSnakeOverlay
        {
            Vector3 HeadWorldPosition { get; }
            void SetHeadExtraOffset(Vector3 worldOffset);
            void ClearHeadExtraOffset();
        }

        /// <summary>
        /// Overlay acquisition/release is injected so Intro / Ghost / Board can share ONE pool.
        /// </summary>
        public delegate ISelectionOverlay AcquireSelectionOverlayDelegate(int sortingOrder);
        public delegate void ReleaseSelectionOverlayDelegate(ISelectionOverlay overlay);

        public struct Settings
        {
            public float animationSpeed;
            public int overlaySortingOrder;

            public int segmentsPerTile;
            public bool smoothOverlayPath;
            public bool renderBoardAsSnakes;

            public bool bumpOnHitArrow;
            public float bumpDistanceCells;
            public float bumpDuration;

            public float offscreenViewportMargin;
            public float cellSize;
        }

        MonoBehaviour _host;
        BoardController _controller;
        BoardLayout2D _layout;
        TileView2D _tilePrefab;
        Camera _cam;

        BoardDots2D _dots;
        BoardSnakeRenderer2D _snakes;

        Settings _settings;

        // External helpers/ownership
        System.Func<bool> _getIntroRunning;
        System.Func<int, Color> _getStableStartColor;
        System.Func<int, Color> _getTileColor;
        System.Func<BoardState> _getLiveState;
        System.Action<List<Vector2Int>> _redrawCellsFromLiveState;
        System.Action<Vector3> _raiseArrowSelected;
        System.Func<bool> _getTintOnHit;
        System.Func<Color> _getHitTint;

        // Mutations owned by BoardView2D but used here
        Dictionary<int, Color> _permanentTileColors;

        AcquireSelectionOverlayDelegate _acquireSelectionOverlay;
        ReleaseSelectionOverlayDelegate _releaseSelectionOverlay;

        readonly List<Coroutine> _activeAnims = new();
        readonly List<ISelectionOverlay> _activeOverlays = new();


        // GC reduction: reuse temp collections in click handler.
        readonly HashSet<int> _tmpChainSet = new();
        int _collisionRetraceLocks;

        public int ActiveOverlayCount => _activeOverlays.Count;
        public bool IsLocked => _collisionRetraceLocks > 0;

        public void Init(
            MonoBehaviour host,
            BoardController controller,
            BoardLayout2D layout,
            TileView2D tilePrefab,
            Camera cam,
            BoardDots2D dots,
            BoardSnakeRenderer2D snakes,
            Dictionary<int, Color> permanentTileColors,
            System.Func<bool> getIntroRunning,
            System.Func<int, Color> getStableStartColor,
            System.Func<int, Color> getTileColor,
            System.Func<BoardState> getLiveState,
            System.Action<List<Vector2Int>> redrawCellsFromLiveState,
            System.Action<Vector3> raiseArrowSelected,
            System.Func<bool> getTintOnHit,
            System.Func<Color> getHitTint,
            AcquireSelectionOverlayDelegate acquireSelectionOverlay,
            ReleaseSelectionOverlayDelegate releaseSelectionOverlay,
            Settings settings)
        {
            _host = host;
            _controller = controller;
            _layout = layout;
            _tilePrefab = tilePrefab;
            _cam = cam;
            _dots = dots;
            _snakes = snakes;
            _permanentTileColors = permanentTileColors;
            _getIntroRunning = getIntroRunning;
            _getStableStartColor = getStableStartColor;
            _getTileColor = getTileColor;
            _getLiveState = getLiveState;
            _redrawCellsFromLiveState = redrawCellsFromLiveState;
            _raiseArrowSelected = raiseArrowSelected;
            _getTintOnHit = getTintOnHit;
            _getHitTint = getHitTint;
            _acquireSelectionOverlay = acquireSelectionOverlay;
            _releaseSelectionOverlay = releaseSelectionOverlay;
            _settings = settings;
        }

        ISelectionOverlay GetOverlay(int sortingOrder) =>
            _acquireSelectionOverlay != null ? _acquireSelectionOverlay(sortingOrder) : null;

        void ReleaseOverlay(ISelectionOverlay o)
        {
            if (o == null) return;
            _releaseSelectionOverlay?.Invoke(o);
        }

        public void StopAll()
        {
            for (int i = 0; i < _activeAnims.Count; i++)
                if (_activeAnims[i] != null)
                    _host.StopCoroutine(_activeAnims[i]);
            _activeAnims.Clear();

            for (int i = 0; i < _activeOverlays.Count; i++)
                if (_activeOverlays[i] != null)
                    ReleaseOverlay(_activeOverlays[i]);
            _activeOverlays.Clear();

            _collisionRetraceLocks = 0;

            // If win is pending, we can potentially fire now that overlays are gone.
            _dots?.TryFirePendingWinVfx(_activeOverlays.Count);
        }

        public void TryHandleTileClicked(int x, int y)
        {
            // Back-compat: old callers still quantize.
            TryHandleTileClicked(x, y, _layout.CellToWorld(x, y));
        }
        
        public void TryHandleTileClicked(int x, int y, Vector3 clickWorldPos)
        {
            if (_getIntroRunning()) return;
            if (IsLocked) return;
            if (_controller == null || _controller.Won) return;

            var live = _getLiveState();
            if (live == null || !live.InBounds(x, y)) return;

            int startIdx = live.Index(x, y);
            if (live.tiles[startIdx].type != TileType.Arrow) return;

            // SNAPSHOT for visuals & path prediction
            var snap = CloneState(live);

            // Build chain/path from SNAPSHOT (so visuals reflect what player clicked)
            _tmpChainSet.Clear();
            ArrowChainUtility.CollectFullChain(snap, new Vector2Int(x, y), 0, _tmpChainSet);
            if (_tmpChainSet.Count == 0) return;

            if (!ArrowChainUtility.TryBuildOrderedChain(
                    snap, _tmpChainSet, new Vector2Int(x, y),
                    out var chainOrdered, out var headPos, out var headOutDir, out bool isLoop))
                return;

            Color color = _getTileColor(startIdx);

            // Apply the real gameplay move immediately (your existing behavior).
            _controller.ClickCell(x, y);

            _raiseArrowSelected?.Invoke(clickWorldPos);

            // Start ghost animation
            var routine = _host.StartCoroutine(
                AnimateGhost(startIdx, chainOrdered, headPos, headOutDir, color, snap, isLoop)
            );
            _activeAnims.Add(routine);

            // In case win was pending, try now (usually no-op until overlays are gone).
            _dots?.TryFirePendingWinVfx(_activeOverlays.Count);
        }

        enum PathStop : byte
        {
            ExitedBoard,
            HitArrow,
            HitBlock,
            NoTravelDir,
            StepLimit
        }

        IEnumerator AnimateGhost(int startIdx, List<Vector2Int> chainTailToHead, Vector2Int headPos, Dir headOutDir, Color color, BoardState snap, bool isLoop)
        {
            bool acquiredCollisionLock = false;

            // Identify this arrow chain consistently even if clicked from a different tile.
            ulong blockedChainKey = 0;
            {
                var set = new HashSet<int>(chainTailToHead.Count);
                for (int i = 0; i < chainTailToHead.Count; i++)
                    set.Add(snap.Index(chainTailToHead[i].x, chainTailToHead[i].y));
                blockedChainKey = ComputeChainKey(set);
            }

            if (_settings.renderBoardAsSnakes)
                _snakes.HideBoardSnakeForChain(snap, chainTailToHead);
            else
                _snakes.SetChainHiddenRefCounted(chainTailToHead, true);

            _redrawCellsFromLiveState(chainTailToHead);

            var overlay = GetOverlay(_settings.overlaySortingOrder);
            _activeOverlays.Add(overlay);

            // NOTE: sprites are configured by the shared pool, but we keep this safe/idempotent.
            var overlayStraight = _tilePrefab.tail;
            overlay.ConfigureSprites(overlayStraight, _tilePrefab.head);
            overlay.SetColor(color);
            overlay.SetTailMode(BoardSnakeRenderer2D.TailMode.FollowHead);

            int bodyLenTiles = Mathf.Max(0, chainTailToHead.Count - 1);
            int spt = Mathf.Max(2, _settings.segmentsPerTile);

            // More segments: one segment per sample step
            int bodyLenPoints = bodyLenTiles * spt;

            // Single-tile arrows still need a visible tail
            const int minTailPointsForSingle = 6;
            if (chainTailToHead.Count == 1)
                bodyLenPoints = Mathf.Max(bodyLenPoints, minTailPointsForSingle);

            overlay.SetTailMode(BoardSnakeRenderer2D.TailMode.AnchoredTail);
            overlay.EnsureSegmentCount(bodyLenPoints);

            // Build a cell path first (tail->head, plus extension if needed)
            int loopLaps = 2;
            int reps = isLoop ? loopLaps : 1;

            var cellPath = new List<Vector2Int>(chainTailToHead.Count * reps + 128);
            for (int r = 0; r < reps; r++)
                cellPath.AddRange(chainTailToHead);

            PathStop stop;

            bool hasPendingImpact = false;
            bool pendingFired = false;
            Vector3 pendingImpactPos = Vector3.zero;

            if (isLoop)
            {
                stop = PathStop.StepLimit;
            }
            else
            {
                var extCells = new List<Vector2Int>(128);
                stop = ExtendForwardInBoundsCells(snap, headPos, headOutDir, _controller.travelMode, extCells, out var impactPos);

                if (extCells.Count > 0)
                    cellPath.AddRange(extCells);

                hasPendingImpact = (stop == PathStop.HitArrow || stop == PathStop.HitBlock || stop == PathStop.NoTravelDir);
                pendingFired = false;
                pendingImpactPos = impactPos;
            }
            bool movingSnake =
                stop == PathStop.ExitedBoard ||
                stop == PathStop.HitArrow ||        
                stop == PathStop.StepLimit;
            
            overlay.SetTailMode(movingSnake
                ? BoardSnakeRenderer2D.TailMode.FollowHead
                : BoardSnakeRenderer2D.TailMode.AnchoredTail);

            // Build world points: smooth if enabled, else just centers (still subdivided by point sampling)
            List<Vector3> pts;
            if (_settings.smoothOverlayPath)
            {
                pts = BuildSmoothWorldPathFromCells(cellPath);
            }
            else
            {
                pts = new List<Vector3>(cellPath.Count);
                for (int i = 0; i < cellPath.Count; i++)
                    pts.Add(_layout.CellToWorld(cellPath[i].x, cellPath[i].y));
            }

            // If blocked, ensure the head can reach the impact point
            if (hasPendingImpact)
            {
                float spacing = PointSpacingWorld;
                if (pts.Count == 0) pts.Add(_layout.CellToWorld(headPos.x, headPos.y));
                AddSubdivided(pts, pts[pts.Count - 1], pendingImpactPos, spacing);
            }

            // If exited, extend offboard until tail clears camera
            if (stop == PathStop.ExitedBoard && pts.Count > 0)
            {
                Vector2Int lastInBounds = cellPath.Count > 0 ? cellPath[cellPath.Count - 1] : headPos;
                ExtendOffBoardUntilOutOfView(lastInBounds, headOutDir, pts, bodyLenPoints);
            }

            // For 1-tile arrows, add a short segment behind the start so the tail is visible immediately
            if (chainTailToHead.Count == 1 && pts.Count > 0)
            {
                float spacing = PointSpacingWorld;

                Vector2Int o = DirToOffset(headOutDir);
                Vector3 fwd = new Vector3(o.x, o.y, 0f);

                Vector3 center = pts[0];
                float tailLenWorld = _settings.cellSize * 0.60f;
                Vector3 tailStart = center - fwd * tailLenWorld;

                var pref = new List<Vector3>(16) { tailStart };
                AddSubdivided(pref, tailStart, center, spacing);

                for (int i = 1; i < pts.Count; i++)
                    pref.Add(pts[i]);

                pts = pref;
            }

            // Animate in POINT units
            float headProgress;
            if (chainTailToHead.Count == 1)
            {
                Vector3 center = _layout.CellToWorld(headPos.x, headPos.y);
                int centerIndex = 0;
                for (int i = 0; i < pts.Count; i++)
                {
                    if ((pts[i] - center).sqrMagnitude < 1e-6f) { centerIndex = i; break; }
                }
                headProgress = centerIndex;
            }
            else
            {
                headProgress = (chainTailToHead.Count - 1) * spt;
            }

            float minAdvance = headProgress + spt;
            float blockedStopAt = pts.Count - 1f;

            while (true)
            {
                headProgress += Time.deltaTime * Mathf.Max(1f, _settings.animationSpeed) * spt;
                overlay.UpdateSnake(pts, headProgress);

                if (hasPendingImpact && !pendingFired)
                {
                    float threshold = _settings.cellSize * 0.2f;

                    if ((overlay.HeadWorldPosition - pendingImpactPos).sqrMagnitude <= threshold * threshold)
                    {
                        pendingFired = true;
                        GameEvents.RaiseArrowBlocked(pendingImpactPos);
                    }
                }

                if (stop == PathStop.ExitedBoard)
                {
                    float tailProgress = headProgress - bodyLenPoints;

                    if (headProgress >= minAdvance && tailProgress >= 0f && IsProgressOffscreen(pts, tailProgress))
                    {
                        Vector3 exitPos = (pts.Count > 0)
                            ? pts[Mathf.Min(chainTailToHead.Count - 1, pts.Count - 1)]
                            : _layout.CellToWorld(headPos.x, headPos.y);

                        GameEvents.RaiseArrowExit(exitPos);
                        break;
                    }
                }
                else
                {
                    if (headProgress >= blockedStopAt)
                        break;
                }

                if (headProgress > pts.Count + chainTailToHead.Count + 30f)
                    break;

                yield return null;
            }

            // Only do bump/flash/permanent color when we truly hit another arrow
            if (stop == PathStop.HitArrow)
            {
                if (!acquiredCollisionLock)
                {
                    acquiredCollisionLock = true;
                    _collisionRetraceLocks++;
                }

                _controller?.RegisterBlockedChainKey(blockedChainKey);

                Vector3 fwd = Vector3.right;
                if (pts.Count >= 2)
                {
                    var d = pts[pts.Count - 1] - pts[pts.Count - 2];
                    if (d.sqrMagnitude > 1e-6f) fwd = d.normalized;
                }

                if (_settings.bumpOnHitArrow && _settings.bumpDuration > 0f && _settings.bumpDistanceCells > 0f)
                {
                    float t = 0f;
                    float dist = _settings.bumpDistanceCells * _settings.cellSize;

                    while (t < _settings.bumpDuration)
                    {
                        t += Time.deltaTime;
                        float u = Mathf.Clamp01(t / _settings.bumpDuration);
                        float s = Mathf.Sin(u * Mathf.PI);
                        overlay.SetHeadExtraOffset(fwd * (s * dist));
                        yield return null;
                    }

                    overlay.ClearHeadExtraOffset();
                }

                bool tintOnHit = _getTintOnHit == null || _getTintOnHit();
                Color hitTint = _getHitTint != null ? _getHitTint() : Color.white;

                if (tintOnHit)
                    overlay.SetColor(hitTint);

                if (tintOnHit && _permanentTileColors != null)
                {
                    var live = _getLiveState();
                    if (live != null)
                    {
                        for (int i = 0; i < chainTailToHead.Count; i++)
                        {
                            var p = chainTailToHead[i];
                            if (!live.InBounds(p.x, p.y)) continue;

                            int idx = live.Index(p.x, p.y);
                            _permanentTileColors[idx] = hitTint;
                        }

                        // Ensure tile colors refresh immediately even when not rendering board-snakes.
                        _redrawCellsFromLiveState?.Invoke(chainTailToHead);
                    }
                }
            }

            // Collision retreat
            if (stop == PathStop.HitArrow)
            {
                float retreatTarget = (chainTailToHead.Count - 1) * spt;
                float retreatSpeedPointsPerSec = Mathf.Max(1f, _settings.animationSpeed) * spt;

                while (headProgress > retreatTarget)
                {
                    headProgress -= Time.deltaTime * retreatSpeedPointsPerSec;
                    if (headProgress < retreatTarget) headProgress = retreatTarget;

                    overlay.UpdateSnake(pts, headProgress);
                    yield return null;
                }
            }

            _activeOverlays.Remove(overlay);
            ReleaseOverlay(overlay);
            _dots?.TryFirePendingWinVfx(_activeOverlays.Count);

            if (_settings.renderBoardAsSnakes)
            {
                var live = _getLiveState();
                _snakes.ShowBoardSnakeForChainIfExists(snap, live, chainTailToHead);
            }
            else
            {
                _snakes.SetChainHiddenRefCounted(chainTailToHead, false);
            }

            // Clean dead coroutine references
            _activeAnims.RemoveAll(c => c == null);

            if (acquiredCollisionLock)
                _collisionRetraceLocks = Mathf.Max(0, _collisionRetraceLocks - 1);
        }

        static BoardState CloneState(BoardState s)
        {
            var c = new BoardState(s.width, s.height);
            c.tiles = (TileState[])s.tiles.Clone();
            return c;
        }

        // -------- Chain key hashing (stable ID for “same arrow chain”) --------
        static ulong ComputeChainKey(HashSet<int> chain)
        {
            var arr = new int[chain.Count];
            int k = 0;
            foreach (var v in chain) arr[k++] = v;
            System.Array.Sort(arr);

            const ulong offset = 1469598103934665603UL;
            const ulong prime = 1099511628211UL;

            ulong h = offset;
            for (int i = 0; i < arr.Length; i++)
            {
                unchecked
                {
                    h ^= (ulong)arr[i];
                    h *= prime;
                }
            }
            return h;
        }

        // -------- Collision/exit extension --------
        PathStop ExtendForwardInBoundsCells(
            BoardState s,
            Vector2Int headPos,
            Dir headOutDir,
            SignalTravelMode mode,
            List<Vector2Int> outCells,
            out Vector3 impactWorldPos)
        {
            impactWorldPos = Vector3.zero;

            Vector2Int pos = headPos + DirToOffset(headOutDir);
            Dir travelDir = headOutDir;
            bool hasTravelDir = true;

            Dir entryDir = Opposite(travelDir);

            int autoMax = 1 + s.width * s.height * (mode == SignalTravelMode.ThroughEmpty ? 4 : 1);

            for (int step = 0; step < autoMax; step++)
            {
                if (!s.InBounds(pos.x, pos.y))
                {
                    if (outCells.Count > 0)
                    {
                        var last = outCells[outCells.Count - 1];
                        impactWorldPos = _layout.CellToWorld(last.x, last.y);
                    }
                    else
                    {
                        impactWorldPos = _layout.CellToWorld(headPos.x, headPos.y);
                    }

                    return PathStop.ExitedBoard;
                }

                int idx = s.Index(pos.x, pos.y);
                var t = s.tiles[idx];

                Vector3 PrevCenter()
                {
                    if (outCells.Count > 0)
                    {
                        var last = outCells[outCells.Count - 1];
                        return _layout.CellToWorld(last.x, last.y);
                    }
                    return _layout.CellToWorld(headPos.x, headPos.y);
                }

                Vector3 HitCenter() => _layout.CellToWorld(pos.x, pos.y);

                if (t.type == TileType.Block)
                {
                    impactWorldPos = Vector3.Lerp(PrevCenter(), HitCenter(), 0.5f);
                    return PathStop.HitBlock;
                }

                if (t.type == TileType.Arrow && t.arrow.inDir != entryDir)
                {
                    impactWorldPos = Vector3.Lerp(PrevCenter(), HitCenter(), 0.5f);
                    return PathStop.HitArrow;
                }

                outCells.Add(pos);

                if (t.type == TileType.Empty)
                {
                    if (mode == SignalTravelMode.ArrowToArrowOnly || !hasTravelDir)
                    {
                        impactWorldPos = _layout.CellToWorld(pos.x, pos.y);
                        return PathStop.NoTravelDir;
                    }

                    Vector2Int nextPos = pos + DirToOffset(travelDir);

                    if (s.InBounds(nextPos.x, nextPos.y) && s.tiles[s.Index(nextPos.x, nextPos.y)].type == TileType.Arrow)
                    {
                        Vector3 a = _layout.CellToWorld(pos.x, pos.y);
                        Vector3 b = _layout.CellToWorld(nextPos.x, nextPos.y);
                        impactWorldPos = Vector3.Lerp(a, b, 0.5f);
                        return PathStop.HitArrow;
                    }

                    pos = nextPos;
                    entryDir = Opposite(travelDir);
                    continue;
                }

                travelDir = t.arrow.outDir;
                hasTravelDir = true;

                pos += DirToOffset(travelDir);
                entryDir = Opposite(travelDir);
            }

            return PathStop.StepLimit;
        }

        // -------- Offboard extension / viewport tests --------
        bool IsHeadOffscreen(Vector3 worldPos)
        {
            if (_cam == null) _cam = Camera.main;
            if (_cam == null) return true;

            var v = _cam.WorldToViewportPoint(worldPos);

            if (v.z < 0f) return true;

            float m = _settings.offscreenViewportMargin;
            return (v.x < -m || v.x > 1f + m || v.y < -m || v.y > 1f + m);
        }

        bool IsProgressOffscreen(List<Vector3> pts, float progress)
        {
            if (pts == null || pts.Count == 0) return true;
            SampleSmooth(pts, progress, out var p, out _);
            return IsHeadOffscreen(p);
        }

        void ExtendOffBoardUntilOutOfView(Vector2Int lastInBoundsPos, Dir outDir, List<Vector3> pts, int bodyLenPoints)
        {
            if (_cam == null) _cam = Camera.main;
            if (_cam == null) return;

            Vector3 p = pts.Count > 0 ? pts[pts.Count - 1] : _layout.CellToWorld(lastInBoundsPos.x, lastInBoundsPos.y);

            Vector3 step = new Vector3(DirToOffset(outDir).x, DirToOffset(outDir).y, 0f) * PointSpacingWorld;

            for (int i = 0; i < 2048; i++)
            {
                bool headOff = IsHeadOffscreen(p);

                Vector3 tail = p - step * Mathf.Max(0, bodyLenPoints);
                bool tailOff = IsHeadOffscreen(tail);

                if (headOff && tailOff)
                    break;

                p += step;
                pts.Add(p);
            }
        }

        // -------- Smooth path build (copied from your BoardView2D logic, but self-contained) --------
        float PointSpacingWorld => _settings.cellSize / Mathf.Max(2, _settings.segmentsPerTile);

        void AddSubdivided(List<Vector3> pts, Vector3 a, Vector3 b, float spacingWorld)
        {
            float dist = Vector3.Distance(a, b);
            if (dist < 1e-5f)
                return;

            int steps = Mathf.Max(1, Mathf.CeilToInt(dist / spacingWorld));
            for (int s = 1; s <= steps; s++)
            {
                float t = (float)s / steps;
                pts.Add(Vector3.Lerp(a, b, t));
            }
        }

        static void SampleSmooth(List<Vector3> pts, float p, out Vector3 pos, out Vector3 forward)
        {
            int n = pts.Count;
            if (n == 0)
            {
                pos = Vector3.zero;
                forward = Vector3.right;
                return;
            }

            if (n == 1)
            {
                pos = pts[0];
                forward = Vector3.right;
                return;
            }

            float clamped = Mathf.Clamp(p, 0f, n - 1f);
            int i = Mathf.FloorToInt(clamped);
            float f = clamped - i;

            int j = Mathf.Min(i + 1, n - 1);
            pos = Vector3.Lerp(pts[i], pts[j], f);

            Vector3 d = (pts[j] - pts[i]);
            if (d.sqrMagnitude < 1e-6f && i > 0) d = pts[i] - pts[i - 1];

            forward = d.sqrMagnitude < 1e-6f ? Vector3.right : d.normalized;
        }

        List<Vector3> BuildSmoothWorldPathFromCells(List<Vector2Int> cells)
        {
            // NOTE: this is the simplest version (cell centers + even spacing).
            // If you want the corner-radius bezier version here too, we can move it next.
            var outPts = new List<Vector3>(cells.Count * _settings.segmentsPerTile + 64);
            if (cells == null || cells.Count == 0) return outPts;

            float spacing = PointSpacingWorld;

            Vector3 last = _layout.CellToWorld(cells[0].x, cells[0].y);
            outPts.Add(last);

            for (int i = 1; i < cells.Count; i++)
            {
                Vector3 next = _layout.CellToWorld(cells[i].x, cells[i].y);
                AddSubdivided(outPts, last, next, spacing);
                last = next;
            }

            return outPts;
        }

        // -------- Dir helpers --------
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
