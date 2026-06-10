using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    /// <summary>
    /// Owns the *board* snake overlays (not the ghost/selection overlays).
    /// Also owns tile hide ref-counting for the non-snake render mode.
    /// 
    /// IMPORTANT: Uses an overlay interface so BoardView2D can keep its existing
    /// nested ArrowSnakeOverlay class without moving it yet.
    /// </summary>
    public sealed class BoardSnakeRenderer2D
    {
        public enum TailMode : byte
        {
            AnchoredTail, // intro/static board snakes
            FollowHead    // moving/exiting ghost
        }
        
        public interface IArrowSnakeOverlay
        {
            void ConfigureSprites(Sprite straight, Sprite head);
            void SetColor(Color c);
            void EnsureSegmentCount(int bodyLenPoints);
            void SetTailMode(TailMode mode);
            void UpdateSnake(List<Vector3> pts, float headProgress);
            void SetVisible(bool v);
            void SetOverrideColor(Color c);
            void Destroy();
        }

        /// <summary>
        /// Overlay acquisition/release is injected so Intro / Ghost / Board can share ONE pool.
        /// </summary>
        public delegate IArrowSnakeOverlay AcquireOverlayDelegate(int sortingOrder);
        public delegate void ReleaseOverlayDelegate(IArrowSnakeOverlay overlay);

        public delegate bool TryBuildOrderedChainDelegate(
            BoardState s,
            HashSet<int> chainSet,
            Vector2Int clicked,
            out List<Vector2Int> ordered,
            out Vector2Int headPos,
            out Dir headOutDir,
            out bool isLoop);

        public delegate void BuildBoardWorldPointsDelegate(List<Vector2Int> orderedCells, List<Vector3> outPts, BoardState s);

        [System.Serializable]
        public struct Settings
        {
            public bool renderBoardAsSnakes;
            public int boardSnakeSortingOrder;
            public int segmentsPerTile;
        }

        BoardController _controller;
        System.Func<TileView2D[]> _getTiles;
        System.Func<int, Color> _getTileColor;
        TileView2D _tilePrefab;

        AcquireOverlayDelegate _acquireOverlay;
        ReleaseOverlayDelegate _releaseOverlay;
        TryBuildOrderedChainDelegate _tryBuildOrderedChain;
        BuildBoardWorldPointsDelegate _buildBoardWorldPoints;

        Settings _settings;

        readonly List<IArrowSnakeOverlay> _boardSnakes = new();
        readonly Dictionary<int, IArrowSnakeOverlay> _boardSnakeByTile = new();

        // per-tile hide ref-count so multiple anims can overlap safely (when NOT rendering snakes)
        readonly Dictionary<int, int> _hideRefCount = new();
        
        readonly HashSet<int> _visited = new();
        readonly HashSet<int> _chainSet = new();
        readonly List<Vector3> _pts = new(512);

        public void Init(
            BoardController controller,
            System.Func<TileView2D[]> getTiles,
            System.Func<int, Color> getTileColor,
            TileView2D tilePrefab,
            AcquireOverlayDelegate acquireOverlay,
            ReleaseOverlayDelegate releaseOverlay,
            TryBuildOrderedChainDelegate tryBuildOrderedChain,
            BuildBoardWorldPointsDelegate buildBoardWorldPoints,
            Settings settings)
        {
            _controller = controller;
            _getTiles = getTiles;
            _getTileColor = getTileColor;
            _tilePrefab = tilePrefab;
            _acquireOverlay = acquireOverlay;
            _releaseOverlay = releaseOverlay;
            _tryBuildOrderedChain = tryBuildOrderedChain;
            _buildBoardWorldPoints = buildBoardWorldPoints;
            _settings = settings;
        }


        void ReleaseOverlay(IArrowSnakeOverlay o)
        {
            if (o == null) return;
            _releaseOverlay?.Invoke(o);
        }

        IArrowSnakeOverlay AcquireOverlay(int sortingOrder)
        {
            return _acquireOverlay != null ? _acquireOverlay(sortingOrder) : null;
        }

        public void ClearAll()
        {
            // Recycle active overlays into the pool.
            for (int i = 0; i < _boardSnakes.Count; i++)
            {
                var o = _boardSnakes[i];
                if (o == null) continue;
                ReleaseOverlay(o);
            }
            _boardSnakes.Clear();
            _boardSnakeByTile.Clear();
            _hideRefCount.Clear();
        }

        public void Stop()
        {
            // Restore hidden tiles caused by ref-counting (only relevant when not rendering snakes).
            RestoreTilesFromHideRefCounts();

            // Destroy overlays + clear maps/refcounts.
            ClearAll();
        }

        public void RestoreTilesFromHideRefCounts()
        {
            var tiles = _getTiles != null ? _getTiles() : null;
            if (tiles == null) return;

            foreach (var kv in _hideRefCount)
            {
                int idx = kv.Key;
                if (idx >= 0 && idx < tiles.Length && tiles[idx] != null)
                    tiles[idx].SetHidden(false);
            }
        }
        
        public void HideBoardSnakeForChain(BoardState snap, List<Vector2Int> chainTailToHead)
        {
            if (!_settings.renderBoardAsSnakes) return;
            if (snap == null || chainTailToHead == null) return;

            for (int i = 0; i < chainTailToHead.Count; i++)
            {
                int idx = snap.Index(chainTailToHead[i].x, chainTailToHead[i].y);
                if (_boardSnakeByTile.TryGetValue(idx, out var boardSnake))
                {
                    boardSnake.SetVisible(false);
                    break;
                }
            }
        }

        public void ShowBoardSnakeForChainIfExists(BoardState snap, BoardState live, List<Vector2Int> chainTailToHead)
        {
            if (!_settings.renderBoardAsSnakes) return;
            if (snap == null || live == null || chainTailToHead == null) return;

            bool chainStillExists = false;
            for (int i = 0; i < chainTailToHead.Count; i++)
            {
                var p = chainTailToHead[i];
                if (!live.InBounds(p.x, p.y)) continue;

                int idxLive = live.Index(p.x, p.y);
                if (live.tiles[idxLive].type == TileType.Arrow)
                {
                    chainStillExists = true;
                    break;
                }
            }

            if (chainStillExists)
            {
                for (int i = 0; i < chainTailToHead.Count; i++)
                {
                    int idxSnap = snap.Index(chainTailToHead[i].x, chainTailToHead[i].y);
                    if (_boardSnakeByTile.TryGetValue(idxSnap, out var boardSnake))
                    {
                        int idxLive = live.Index(chainTailToHead[i].x, chainTailToHead[i].y);
                        Color c = (_getTileColor != null) ? _getTileColor(idxLive) : Color.white;
                        boardSnake.SetColor(c);
                            
                        boardSnake.SetVisible(true);
                        break;
                    }
                }
            }
            else
            {
                // Chain cleared -> need rebuild so dots show immediately and mappings update.
                RebuildBoardSnakeOverlays();
            }
        }

        /// <summary>
        /// Used when NOT rendering snakes: hide/show tiles using ref counting so multiple anims overlap safely.
        /// </summary>
        public void SetChainHiddenRefCounted(List<Vector2Int> chainTailToHead, bool hidden)
        {
            if (_settings.renderBoardAsSnakes) return;

            var tiles = _getTiles != null ? _getTiles() : null;
            var s = _controller != null ? _controller.State : null;
            if (s == null || tiles == null || chainTailToHead == null) return;

            for (int i = 0; i < chainTailToHead.Count; i++)
            {
                var p = chainTailToHead[i];
                if (!s.InBounds(p.x, p.y)) continue;

                int idx = s.Index(p.x, p.y);

                int count = 0;
                _hideRefCount.TryGetValue(idx, out count);

                if (hidden) count++;
                else count = Mathf.Max(0, count - 1);

                if (count == 0) _hideRefCount.Remove(idx);
                else _hideRefCount[idx] = count;

                var tv = tiles[idx];
                if (tv != null) tv.SetHidden(count > 0);
            }
        }

        public void RebuildBoardSnakeOverlays()
        {
            var tiles = _getTiles != null ? _getTiles() : null;

            for (int i = 0; i < _boardSnakes.Count; i++)
            {
                var o = _boardSnakes[i];
                if (o == null) continue;
                ReleaseOverlay(o);
            }
            _boardSnakes.Clear();
            _boardSnakeByTile.Clear();

            if (!_settings.renderBoardAsSnakes)
            {
                // Ensure arrows are visible in normal tile mode
                if (tiles != null)
                {
                    for (int i = 0; i < tiles.Length; i++)
                        if (tiles[i] != null)
                            tiles[i].SetHidden(false);
                }

                _hideRefCount.Clear();
                return;
            }

            var s = _controller != null ? _controller.State : null;
            if (s == null || tiles == null) return;

            // Hide all arrow tiles (keep blocks visible)
            for (int i = 0; i < s.tiles.Length; i++)
            {
                if (tiles[i] == null) continue;

                if (s.tiles[i].type == TileType.Arrow)
                    tiles[i].SetHidden(true);
                else
                    tiles[i].SetHidden(false);
            }

            // Build one smooth overlay per chain
            _visited.Clear();
            _chainSet.Clear();

            for (int i = 0; i < s.tiles.Length; i++)
            {
                if (_visited.Contains(i)) continue;
                if (s.tiles[i].type != TileType.Arrow) continue;

                int x = i % s.width;
                int y = i / s.width;

                _chainSet.Clear();
                ArrowChainUtility.CollectFullChain(s, new Vector2Int(x, y), 0, _chainSet);
                if (_chainSet.Count == 0) continue;

                foreach (int idx in _chainSet) _visited.Add(idx);

                if (_tryBuildOrderedChain == null) continue;

                if (!_tryBuildOrderedChain(
                        s, _chainSet, new Vector2Int(x, y),
                        out var ordered, out var headPos, out var headOutDir, out bool isLoop))
                    continue;
                
                Color color = (_getTileColor != null) ? _getTileColor(i) : Color.white;

                _pts.Clear();
                _buildBoardWorldPoints?.Invoke(ordered, _pts, s);
                if (_pts.Count < 2) continue;

                // Create overlay (sorting order stacks)
                int sortingOrder = _settings.boardSnakeSortingOrder + _boardSnakes.Count * 2;

                var overlay = AcquireOverlay(sortingOrder);

                if (overlay == null) continue;

                var overlayStraight = _tilePrefab.tail;

                overlay.ConfigureSprites(overlayStraight, _tilePrefab.head);
                overlay.SetColor(color);
                overlay.SetTailMode(TailMode.AnchoredTail);

                int spt = Mathf.Max(2, _settings.segmentsPerTile);

                int minTailPointsForSingle = 6;

                int bodyLenPoints = Mathf.Max(0, (ordered.Count - 1) * spt);
                if (ordered.Count == 1)
                    bodyLenPoints = Mathf.Max(bodyLenPoints, minTailPointsForSingle);

                bodyLenPoints = Mathf.Min(bodyLenPoints, Mathf.Max(0, _pts.Count - 1));

                overlay.EnsureSegmentCount(bodyLenPoints);

                float endProgress = Mathf.Max(0f, _pts.Count - 1f);
                overlay.UpdateSnake(_pts, endProgress);

                _boardSnakes.Add(overlay);

                for (int k = 0; k < ordered.Count; k++)
                {
                    var p = ordered[k];
                    int idx = s.Index(p.x, p.y);
                    _boardSnakeByTile[idx] = overlay;
                }
            }
        }
    }
}
