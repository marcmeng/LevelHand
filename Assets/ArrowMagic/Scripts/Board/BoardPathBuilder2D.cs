using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    /// <summary>
    /// Pure path/geometry builder: converts ordered grid cells into evenly spaced world points
    /// (optionally rounded corners). Also includes even-resample util.
    ///
    /// Designed to be shared by:
    /// - Level intro builder/animator (intro world points)
    /// - Board snake renderer (board world points)
    /// - Selection animator ghost overlay (smooth/extended paths)
    /// </summary>
    public sealed class BoardPathBuilder2D
    {
        public struct Settings
        {
            public float cellSize;
            public int segmentsPerTile;          // samples per tile (>=2)
            public float cornerRadius01;         // 0..0.49 (multiplied by cellSize)
            public bool smoothCorners;           // if false: just subdivide between centers
        }

        BoardLayout2D _layout;
        Settings _settings;

        public void Init(BoardLayout2D layout, Settings settings)
        {
            _layout = layout;
            _settings = settings;
        }

        public void SetSettings(Settings settings) => _settings = settings;

        public float PointSpacingWorld => _settings.cellSize / Mathf.Max(2, _settings.segmentsPerTile);

        // ---------- Public API used by your systems ----------

        /// <summary>
        /// Builds intro points for an ordered arrow chain (tail->head).
        /// Matches your previous behavior:
        /// - For 1-tile arrows: add a short segment behind the center so a tail exists.
        /// - For multi-tile: seed first segment using first tile outDir (better alignment),
        ///   then build smooth/straight path, then resample evenly.
        /// </summary>
        public void BuildIntroWorldPoints(List<Vector2Int> orderedCells, List<Vector3> outPts, BoardState s)
        {
            outPts.Clear();
            if (orderedCells == null || orderedCells.Count == 0) return;

            // Single-tile arrow: tail segment behind center -> center
            if (orderedCells.Count == 1)
            {
                var cell = orderedCells[0];
                var t = s.Get(cell.x, cell.y);

                Vector3 center = _layout.CellToWorld(cell.x, cell.y);

                Vector2Int o = DirToOffset(t.arrow.outDir);
                Vector3 fwd = new Vector3(o.x, o.y, 0f);

                float tailLen = _settings.cellSize * 0.40f;

                var raw1 = new List<Vector3>(4)
                {
                    center - fwd * tailLen,
                    center
                };

                outPts.AddRange(ResamplePolylineEven(raw1, PointSpacingWorld));
                return;
            }

            // Multi-tile: build RAW path, then resample evenly
            var raw = new List<Vector3>(orderedCells.Count * _settings.segmentsPerTile + 64);

            // Seed from first tile outDir so first segment matches the board better
            var first = orderedCells[0];
            int firstIdx = s.Index(first.x, first.y);
            var firstTile = s.tiles[firstIdx];

            Vector3 p0 = _layout.CellToWorld(first.x, first.y);

            if (firstTile.type == TileType.Arrow)
            {
                Vector2Int o = DirToOffset(firstTile.arrow.outDir);
                float r = Mathf.Min(_settings.cellSize * _settings.cornerRadius01, _settings.cellSize * 0.49f);

                // "exit point" inside the cell (slightly toward outDir)
                Vector3 exit0 = p0 + new Vector3(o.x, o.y, 0f) * r;

                raw.Add(p0);
                raw.Add(exit0);

                if (_settings.smoothCorners)
                {
                    var rest = BuildSmoothWorldPathFromCells(orderedCells);

                    // Find nearest point in rest to splice after exit0
                    int start = 1; // skip rest[0] (p0)
                    float best = float.PositiveInfinity;

                    for (int i = 1; i < rest.Count; i++)
                    {
                        float d = (rest[i] - exit0).sqrMagnitude;
                        if (d < best)
                        {
                            best = d;
                            start = i;
                        }
                    }

                    // If the next point would move backwards relative to outDir, advance start
                    Vector3 outDirW = (exit0 - p0);
                    if (outDirW.sqrMagnitude > 1e-6f)
                    {
                        outDirW.Normalize();
                        while (start < rest.Count && Vector3.Dot(rest[start] - exit0, outDirW) < -1e-5f)
                            start++;
                    }

                    for (int i = start; i < rest.Count; i++)
                        raw.Add(rest[i]);
                }
                else
                {
                    // fallback: cell centers
                    for (int i = 0; i < orderedCells.Count; i++)
                        raw.Add(_layout.CellToWorld(orderedCells[i].x, orderedCells[i].y));
                }
            }
            else
            {
                // fallback if not arrow
                if (_settings.smoothCorners)
                    raw.AddRange(BuildSmoothWorldPathFromCells(orderedCells));
                else
                    for (int i = 0; i < orderedCells.Count; i++)
                        raw.Add(_layout.CellToWorld(orderedCells[i].x, orderedCells[i].y));
            }

            // Remove accidental duplicate consecutive points (prevents “clumping” at tail)
            for (int i = raw.Count - 1; i > 0; i--)
            {
                if ((raw[i] - raw[i - 1]).sqrMagnitude < 1e-10f)
                    raw.RemoveAt(i);
            }

            // Enforce EVEN spacing everywhere
            outPts.AddRange(ResamplePolylineEven(raw, PointSpacingWorld));
        }

        /// <summary>
        /// Board snakes use the same point rules as intro for consistency.
        /// (This matches your previous "Multi-tile chains: use intro builder" behavior.)
        /// </summary>
        public void BuildBoardWorldPoints(List<Vector2Int> orderedCells, List<Vector3> outPts, BoardState s)
        {
            outPts.Clear();
            if (orderedCells == null || orderedCells.Count == 0) return;

            if (orderedCells.Count == 1)
            {
                var cell = orderedCells[0];
                var t = s.Get(cell.x, cell.y);

                Vector3 center = _layout.CellToWorld(cell.x, cell.y);

                Vector2Int o = DirToOffset(t.arrow.outDir);
                Vector3 fwd = new Vector3(o.x, o.y, 0f);

                float tailLen = _settings.cellSize * 0.40f;

                var raw = new List<Vector3>(8)
                {
                    center - fwd * tailLen,
                    center
                };

                outPts.AddRange(ResamplePolylineEven(raw, PointSpacingWorld));
                return;
            }

            BuildIntroWorldPoints(orderedCells, outPts, s);
        }

        /// <summary>
        /// Builds a smooth-ish path from cell centers with optional rounded corners (quadratic bezier).
        /// This is your previous BuildSmoothWorldPathFromCells implementation.
        /// </summary>
        public List<Vector3> BuildSmoothWorldPathFromCells(List<Vector2Int> cells)
        {
            var outPts = new List<Vector3>(cells != null ? cells.Count * _settings.segmentsPerTile + 64 : 64);
            if (cells == null || cells.Count == 0) return outPts;

            float spacing = PointSpacingWorld;
            float r = Mathf.Min(_settings.cellSize * _settings.cornerRadius01, _settings.cellSize * 0.49f);

            Vector3 WorldCenter(int i) => _layout.CellToWorld(cells[i].x, cells[i].y);

            Vector3 last = WorldCenter(0);
            outPts.Add(last);

            for (int i = 1; i < cells.Count - 1; i++)
            {
                Vector3 prev = WorldCenter(i - 1);
                Vector3 cur  = WorldCenter(i);
                Vector3 next = WorldCenter(i + 1);

                Vector3 inDir  = (cur - prev);
                Vector3 outDir = (next - cur);

                if (inDir.sqrMagnitude < 1e-6f || outDir.sqrMagnitude < 1e-6f)
                {
                    AddSubdivided(outPts, last, cur, spacing);
                    last = cur;
                    continue;
                }

                inDir.Normalize();
                outDir.Normalize();

                bool isCorner = Mathf.Abs(Vector3.Dot(inDir, outDir)) < 0.2f;

                if (!isCorner || r <= 1e-4f)
                {
                    AddSubdivided(outPts, last, cur, spacing);
                    last = cur;
                    continue;
                }

                Vector3 entry = cur - inDir * r;
                Vector3 exit  = cur + outDir * r;

                AddSubdivided(outPts, last, entry, spacing);
                AddQuadraticBezier(outPts, entry, cur, exit, spacing);

                last = exit;
            }

            Vector3 end = WorldCenter(cells.Count - 1);
            AddSubdivided(outPts, last, end, spacing);

            return outPts;
        }

        // ---------- Static utility (moved out of BoardView2D) ----------

        public static List<Vector3> ResamplePolylineEven(List<Vector3> pts, float spacing)
        {
            if (pts == null || pts.Count == 0) return new List<Vector3>();
            if (pts.Count == 1 || spacing <= 1e-5f) return new List<Vector3>(pts);

            var outPts = new List<Vector3>(pts.Count);
            outPts.Add(pts[0]);

            float distCarry = 0f;
            Vector3 prev = pts[0];

            for (int i = 1; i < pts.Count; i++)
            {
                Vector3 cur = pts[i];
                float segLen = Vector3.Distance(prev, cur);
                if (segLen < 1e-6f) { prev = cur; continue; }

                Vector3 dir = (cur - prev) / segLen;

                float d = segLen;
                while (distCarry + d >= spacing)
                {
                    float need = spacing - distCarry;
                    prev += dir * need;
                    outPts.Add(prev);
                    d -= need;
                    distCarry = 0f;
                }

                distCarry += d;
                prev = cur;
            }

            if ((outPts[outPts.Count - 1] - pts[pts.Count - 1]).sqrMagnitude > 1e-8f)
                outPts.Add(pts[pts.Count - 1]);

            return outPts;
        }

        // ---------- Internals ----------

        static Vector2Int DirToOffset(Dir d) => d switch
        {
            Dir.Up => new Vector2Int(0, 1),
            Dir.Right => new Vector2Int(1, 0),
            Dir.Down => new Vector2Int(0, -1),
            _ => new Vector2Int(-1, 0),
        };

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

        void AddQuadraticBezier(List<Vector3> pts, Vector3 p0, Vector3 p1, Vector3 p2, float spacingWorld)
        {
            float approxLen = Vector3.Distance(p0, p1) + Vector3.Distance(p1, p2);
            int steps = Mathf.Max(2, Mathf.CeilToInt(approxLen / spacingWorld));

            for (int s = 1; s <= steps; s++)
            {
                float t = (float)s / steps;
                float u = 1f - t;
                Vector3 p = (u * u) * p0 + (2f * u * t) * p1 + (t * t) * p2;
                pts.Add(p);
            }
        }
    }
}
