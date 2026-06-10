using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public sealed class IntroEntryBuilder
    {
        private readonly BoardView2D _boardView;
        private readonly BoardController _controller;
        private readonly IntroAnimationSettings _settings;

        private readonly ArrowOverlayPool2D _pool;

        private readonly List<ArrowSnakeOverlay2D> _ownedOverlays = new();
        private readonly HashSet<int> _visited = new();
        private readonly HashSet<int> _chainSet = new();
        
        private static Type _cachedPaletteType;
        private static Func<Palette, Color[]> _cachedGetColors;

        public IntroEntryBuilder(BoardView2D boardView, BoardController controller, IntroAnimationSettings settings, ArrowOverlayPool2D pool)
        {
            _boardView = boardView;
            _controller = controller;
            _settings = settings;
            _pool = pool;
        }

        public IReadOnlyList<ArrowSnakeOverlay2D> OwnedOverlays => _ownedOverlays;

        public void CleanupOverlays()
        {
            for (int i = 0; i < _ownedOverlays.Count; i++)
                _pool?.Release(_ownedOverlays[i]);
            _ownedOverlays.Clear();
        }

        public List<IntroEntry> Build(BoardState s)
        {
            var entries = new List<IntroEntry>(64);
            _visited.Clear();

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

                if (!TryBuildOrderedChain_ForIntro(
                        s, _chainSet, new Vector2Int(x, y),
                        out var ordered, out _, out _, out bool _))
                    continue;

                // stable color from board (so intro matches board)
                int anyIdx = AnyIndex(_chainSet);
                Color c = (anyIdx >= 0) ? _boardView.GetColorForIndex(anyIdx) : Color.white;

                // Acquire from the ONE unified pool (shared with board + ghost).
                // Fallback: create an overlay directly (should not happen in normal wiring).
                int sortingOrder = _settings.overlaySortingOrderBase + _ownedOverlays.Count * 4;
                var overlay = (_pool != null)
                    ? _pool.Acquire(sortingOrder)
                    : new ArrowSnakeOverlay2D(_boardView.transform, sortingOrder);

                if (_pool == null)
                {
                    overlay.ConfigureSprites(
                        _boardView.GetOverlayStraightSprite(),
                        _boardView.GetHeadSprite());
                }
                overlay.SetColor(c);
                
                _ownedOverlays.Add(overlay);

                // build intro sample points
                var pts = new List<Vector3>(ordered.Count * _boardView.IntroSamplesPerTile + 64);
                _boardView.BuildIntroWorldPoints(ordered, pts, s);

                int spt = _boardView.IntroSamplesPerTile;

                int bodyLenPoints = Mathf.Max((_boardView.IntroSamplesPerTile / 2) + 1, (ordered.Count - 1) * spt);

                Color.RGBToHSV(c, out float h, out _, out _);

                int colorIdx = ResolvePaletteIndex(c, h);
                float dur = ComputeRevealDuration(pts);

                entries.Add(new IntroEntry
                {
                    overlay = overlay,
                    pts = pts,
                    bodyLenPoints = bodyLenPoints,

                    cells = ordered,
                    tailCell = ordered[0],
                    arrowLenTiles = ordered.Count,

                    colorHue = h,
                    colorIndex = colorIdx,

                    revealDuration = dur,
                });
            }

            return entries;
        }

        private float ComputeRevealDuration(List<Vector3> pts)
        {
            const float minDuration = 0.02f;

            if (_settings.timingMode == IntroTimingMode.Duration)
                return Mathf.Max(minDuration, _settings.perArrowDuration);

            float speed = Mathf.Max(0.0001f, _settings.revealSpeed);

            float length = 0f;
            for (int i = 1; i < pts.Count; i++)
                length += Vector3.Distance(pts[i - 1], pts[i]);

            if (length <= 0.0001f)
                return Mathf.Max(minDuration, _settings.perArrowDuration);

            return Mathf.Max(minDuration, length / speed);
        }

        private int ResolvePaletteIndex(Color c, float hue)
        {
            var colorService = _boardView != null ? _boardView.GetComponent<BoardColorService2D>() : null;
            var pal = colorService != null ? colorService.CurrentPalette : null;

            if (pal != null)
            {
                var arr = GetPaletteColorsCached(pal);
                if (arr != null && arr.Length > 0)
                    return FindClosestColorIndex(arr, c);
            }

            // fallback: hue buckets
            int buckets = Mathf.Max(1, _settings.hueBucketsFallback);
            int bucket = Mathf.Clamp(Mathf.FloorToInt(hue * buckets), 0, buckets - 1);
            return bucket;
        }
        
        public static void ClearPaletteReflectionCache()
        {
            _cachedPaletteType = null;
            _cachedGetColors = null;
        }

        private static int AnyIndex(HashSet<int> set)
        {
            foreach (var idx in set) return idx;
            return -1;
        }

        private static int FindClosestColorIndex(Color[] colors, Color target)
        {
            float best = float.PositiveInfinity;
            int bestIdx = 0;

            for (int i = 0; i < colors.Length; i++)
            {
                var c = colors[i];
                float dr = c.r - target.r;
                float dg = c.g - target.g;
                float db = c.b - target.b;
                float d2 = dr * dr + dg * dg + db * db;

                if (d2 < best)
                {
                    best = d2;
                    bestIdx = i;
                }
            }

            return bestIdx;
        }
        
        private static Color[] GetPaletteColorsCached(Palette palette)
        {
            if (palette == null) return null;

            var t = palette.GetType();

            // If first time or palette type changed, rebuild the getter once.
            if (_cachedGetColors == null || _cachedPaletteType != t)
            {
                _cachedPaletteType = t;
                _cachedGetColors = BuildPaletteColorGetter(t);
            }

            return _cachedGetColors?.Invoke(palette);
        }

        private static Func<Palette, Color[]> BuildPaletteColorGetter(Type t)
        {
            const BindingFlags BF = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            string[] names =
            {
                "arrowColors", "ArrowColors",
                "colors", "Colors",
                "paletteColors", "PaletteColors",
            };

            // Fields (preferred)
            for (int i = 0; i < names.Length; i++)
            {
                var f = t.GetField(names[i], BF);
                if (f != null && f.FieldType == typeof(Color[]))
                    return p => (Color[])f.GetValue(p);
            }

            // Properties
            for (int i = 0; i < names.Length; i++)
            {
                var pInfo = t.GetProperty(names[i], BF);
                if (pInfo != null && pInfo.PropertyType == typeof(Color[]))
                    return p => (Color[])pInfo.GetValue(p);
            }

            // Last resort: first Color[] field we can find
            var allFields = t.GetFields(BF);
            for (int i = 0; i < allFields.Length; i++)
            {
                var f = allFields[i];
                if (f.FieldType == typeof(Color[]))
                    return p => (Color[])f.GetValue(p);
            }

            // Nothing found: safe null getter
            return _ => null;
        }

        // ----- Chain ordering helper (moved from old file, unchanged) -----
        private static bool TryBuildOrderedChain_ForIntro(
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

                tailPos = start;
            }

            Vector2Int cur = tailPos;
            while (true)
            {
                ordered.Add(cur);

                int curIdx = s.Index(cur.x, cur.y);
                if (!chainSet.Contains(curIdx))
                    break;

                var t = s.Get(cur.x, cur.y);
                if (t.type != TileType.Arrow)
                    break;

                Vector2Int next = cur + DirToOffset(t.arrow.outDir);

                if (!s.InBounds(next.x, next.y))
                {
                    headPos = cur;
                    headOutDir = t.arrow.outDir;
                    break;
                }

                int nextIdx = s.Index(next.x, next.y);
                if (!chainSet.Contains(nextIdx))
                {
                    headPos = cur;
                    headOutDir = t.arrow.outDir;
                    break;
                }

                if (isLoop && next == tailPos)
                {
                    headPos = cur;
                    headOutDir = t.arrow.outDir;
                    break;
                }

                cur = next;
            }

            return ordered.Count > 0;

            static bool HasValidPredecessor(BoardState s, int x, int y, HashSet<int> chainSet)
            {
                for (int d = 0; d < 4; d++)
                {
                    Dir dir = (Dir)d;
                    Vector2Int off = DirToOffset(dir);
                    int px = x - off.x;
                    int py = y - off.y;

                    if (!s.InBounds(px, py)) continue;

                    int pIdx = s.Index(px, py);
                    if (!chainSet.Contains(pIdx)) continue;

                    var pt = s.Get(px, py);
                    if (pt.type != TileType.Arrow) continue;

                    if (pt.arrow.outDir == dir) return true;
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
        }
    }
}
