using System;
using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    /// <summary>
    /// The "recipe" for generating the base (algorithmic) level.
    /// This stays small + deterministic.
    /// </summary>
    [Serializable]
    public sealed class ProceduralLevelSpec
    {
        public int seed;
        public int width;
        public int height;
        public float twistiness;

        // Optional bookkeeping (nice for future-proofing).
        public string generatorId = "ClearAllArrowsGenerator";
        public int generatorVersion = 1;

        // Optional: parameters blob if you want to extend procedural tuning later.
        public string generatorParamsJson = "";
    }

    public enum TileKindOverride : byte
    {
        Unchanged = 0,
        Empty = 1,
        Arrow = 2,
        Block = 3,
    }

    /// <summary>
    /// A sparse "diff" entry for a single tile.
    /// Only entries that differ from the procedural base need to exist.
    /// </summary>
    [Serializable]
    public sealed class TileOverride
    {
        public Vector2Int pos;
        public TileKindOverride kind = TileKindOverride.Unchanged;

        // Only used if kind == Arrow.
        public Dir inDir;
        public Dir outDir;
    }

    /// <summary>
    /// Edited level = Procedural base spec + sparse overrides.
    /// </summary>
    [CreateAssetMenu(menuName = "PixelBug/Arrow Magic/Edited Level Data", fileName = "EditedLevelData")]
    public sealed class EditedLevelData : ScriptableObject
    {
        [Header("Base Procedural Spec")]
        public ProceduralLevelSpec baseSpec = new();

        [Header("Overrides (Diff)")]
        [Tooltip("Tiles that should become empty (delete arrow / clear tile).")]
        public List<Vector2Int> clearedTiles = new();

        [Tooltip("Explicit tile set operations (arrow/block/empty).")]
        public List<TileOverride> tileOverrides = new();

        [Header("Optional Validation")]
        [Tooltip("Optional hash of the generated base board at the time editing began.")]
        public int baseBoardHashAtEditTime = 0;

        // --------------------------------------------------------------------
        // BUILD: procedural base -> apply overrides
        // --------------------------------------------------------------------

        public BoardState BuildBoardState(LevelSpec proceduralTuning, bool[] canSpawnHere = null)
        {
            // Ensure spec matches base dimensions/seed.
            proceduralTuning.width = baseSpec.width;
            proceduralTuning.height = baseSpec.height;
            proceduralTuning.seed = baseSpec.seed;
            proceduralTuning.twistiness = baseSpec.twistiness;
            proceduralTuning.canSpawnHere = canSpawnHere;

            // 1) Generate base
            var gen = new ClearAllArrowsGenerator();
            BoardState s = gen.Generate(proceduralTuning);

            // 2) Apply overrides
            ApplyOverridesToState(s);

            return s;
        }

        public BoardState BuildBoardState(
            float arrowFill,
            int minPathLen,
            int maxPathLen,
            float twistiness,
            bool[] canSpawnHere = null)
        {
            var spec = new LevelSpec
            {
                width = baseSpec.width,
                height = baseSpec.height,
                seed = baseSpec.seed,
                arrowFill = arrowFill,
                minPathLen = minPathLen,
                maxPathLen = maxPathLen,
                twistiness = twistiness,
                canSpawnHere = canSpawnHere
            };

            var gen = new ClearAllArrowsGenerator();
            BoardState s = gen.Generate(spec);

            ApplyOverridesToState(s);
            return s;
        }

        void ApplyOverridesToState(BoardState s)
        {
            if (s == null) return;

            // A) Clears
            for (int i = 0; i < clearedTiles.Count; i++)
            {
                Vector2Int p = clearedTiles[i];
                if (!s.InBounds(p.x, p.y)) continue;

                s.Set(p.x, p.y, TileState.Empty());
            }

            // B) Explicit overrides
            for (int i = 0; i < tileOverrides.Count; i++)
            {
                TileOverride o = tileOverrides[i];
                if (o == null) continue;
                if (o.kind == TileKindOverride.Unchanged) continue;
                if (!s.InBounds(o.pos.x, o.pos.y)) continue;

                switch (o.kind)
                {
                    case TileKindOverride.Empty:
                        s.Set(o.pos.x, o.pos.y, TileState.Empty());
                        break;

                    case TileKindOverride.Block:
                        s.Set(o.pos.x, o.pos.y, TileState.Block());
                        break;

                    case TileKindOverride.Arrow:
                        s.Set(o.pos.x, o.pos.y, TileState.Arrow(o.inDir, o.outDir));
                        break;
                }
            }
        }

        // --------------------------------------------------------------------
        // EDIT OPS
        // --------------------------------------------------------------------

        /// <summary>
        /// In editor mode: tapping an arrow should delete it.
        /// We record that as a clear, and remove any explicit override at that tile.
        /// </summary>
        public void DeleteAt(Vector2Int pos)
        {
            AddClear(pos);
            RemoveOverrideAt(pos);
        }

        /// <summary>
        /// Optional: tap again to undo delete (pure convenience).
        /// </summary>
        public void ToggleDeleteAt(Vector2Int pos)
        {
            int idx = clearedTiles.FindIndex(p => p == pos);
            if (idx >= 0)
            {
                clearedTiles.RemoveAt(idx);
                return;
            }

            AddClear(pos);
            RemoveOverrideAt(pos);
        }

        /// <summary>
        /// Core dot-to-dot edit operation.
        ///
        /// Given a list of cells visited by the drag (tile-to-tile),
        /// this produces overrides that set arrows from each cell to the next.
        ///
        /// Typical behavior:
        /// - Clear the touched tiles first so the stroke "replaces" existing arrows.
        /// - Write arrow overrides along the stroke.
        /// - Clear the final tile so the path has an endpoint.
        /// </summary>
        public void ApplyStroke(
            List<Vector2Int> cells,
            bool clearTouched = true,
            bool clearFinalCell = true)
        {
            if (cells == null || cells.Count < 2)
                return;

            // 1) Clear touched cells (recommended UX: stroke replaces)
            if (clearTouched)
            {
                for (int i = 0; i < cells.Count; i++)
                {
                    AddClear(cells[i]);
                    RemoveOverrideAt(cells[i]);
                }
            }

            int n = cells.Count;

            // Precompute segment directions between consecutive cells.
            // segDir[i] is direction from cells[i] -> cells[i+1]
            // Length is n-1
            var segDir = new Dir[n - 1];
            var segValid = new bool[n - 1];

            for (int i = 0; i < n - 1; i++)
            {
                Vector2Int a = cells[i];
                Vector2Int b = cells[i + 1];
                Vector2Int delta = b - a;

                if (TryGetDirFromDelta(delta, out Dir d))
                {
                    segDir[i] = d;
                    segValid[i] = true;
                }
                else
                {
                    segValid[i] = false;
                }
            }

            // 2) Write arrows per tile using BOTH previous+next segments (so corners work)
            for (int i = 0; i < n; i++)
            {
                Vector2Int p = cells[i];

                bool hasPrev = i > 0 && segValid[i - 1];
                bool hasNext = i < n - 1 && segValid[i];

                // If we clear final cell, we skip writing the last tile entirely.
                if (i == n - 1 && clearFinalCell)
                    break;

                // If this tile has no next segment:
                // - If it's the last tile and we are NOT clearing it, we "continue" using the last segment dir.
                // - Otherwise, we can't define an outDir, so skip.
                Dir outDir;
                if (hasNext)
                {
                    outDir = segDir[i];
                }
                else if (i == n - 1 && !clearFinalCell && hasPrev)
                {
                    outDir = segDir[i - 1]; // continuation on the endpoint tile
                }
                else
                {
                    continue;
                }

                Dir inDir;
                if (hasPrev)
                {
                    // Coming into this tile from previous tile
                    inDir = Opposite(segDir[i - 1]);
                }
                else
                {
                    // Start tile: default to straight-in opposite of out
                    inDir = Opposite(outDir);
                }

                UpsertArrowOverride(p, inDir, outDir);
                RemoveClear(p);
            }

            // 3) If we DO clear final cell, keep old behavior
            if (clearFinalCell)
            {
                Vector2Int last = cells[n - 1];
                AddClear(last);
                RemoveOverrideAt(last);
            }
        }

        // --------------------------------------------------------------------
        // INTERNAL: overrides list helpers
        // --------------------------------------------------------------------

        void UpsertArrowOverride(Vector2Int pos, Dir inDir, Dir outDir)
        {
            int idx = tileOverrides.FindIndex(o => o.pos == pos);
            if (idx < 0)
            {
                tileOverrides.Add(new TileOverride
                {
                    pos = pos,
                    kind = TileKindOverride.Arrow,
                    inDir = inDir,
                    outDir = outDir
                });
            }
            else
            {
                TileOverride o = tileOverrides[idx];
                o.kind = TileKindOverride.Arrow;
                o.inDir = inDir;
                o.outDir = outDir;
            }
        }

        void RemoveOverrideAt(Vector2Int pos)
        {
            tileOverrides.RemoveAll(o => o.pos == pos);
        }

        void AddClear(Vector2Int pos)
        {
            if (!clearedTiles.Contains(pos))
                clearedTiles.Add(pos);
        }

        void RemoveClear(Vector2Int pos)
        {
            int idx = clearedTiles.FindIndex(p => p == pos);
            if (idx >= 0) clearedTiles.RemoveAt(idx);
        }

        // --------------------------------------------------------------------
        // DIR helpers (match your Dir enum: Up, Right, Down, Left)
        // --------------------------------------------------------------------

        static bool TryGetDirFromDelta(Vector2Int delta, out Dir d)
        {
            // 4-way only (dot-to-dot / tile-to-tile).
            if (delta == Vector2Int.up)    { d = Dir.Up; return true; }
            if (delta == Vector2Int.right) { d = Dir.Right; return true; }
            if (delta == Vector2Int.down)  { d = Dir.Down; return true; }
            if (delta == Vector2Int.left)  { d = Dir.Left; return true; }

            d = default;
            return false;
        }

        static Dir Opposite(Dir d) => (Dir)(((int)d + 2) & 3);
    }
}
