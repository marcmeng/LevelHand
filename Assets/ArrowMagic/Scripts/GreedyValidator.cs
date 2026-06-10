// GreedyValidator.cs
// Fast “good enough” validator for the "clear all arrows" ruleset.
// It does NOT prove minimality—just checks that the level can be cleared
// by repeatedly choosing a move that clears the largest exiting path.
//
//
// Usage:
//   var ok = GreedyValidator.TryClearAllByGreedy(board, rules, maxMoves: 200, out var moves);
//
// Drop in Scripts/Solver/ or Scripts/Gen/.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public static class GreedyValidator
    {
        /// <summary>
        /// Attempts to clear the entire board by repeatedly selecting the move that clears
        /// the most arrows (best-first greedy).
        /// </summary>
        public static bool TryClearAllByGreedy(
            BoardState start,
            IRuleset ruleset,
            int maxMoves,
            out List<Move> moveSequence)
        {
            moveSequence = new List<Move>(64);

            // Work on a copy so generation doesn’t mutate the candidate.
            var s = CloneBoard(start);

            for (int step = 0; step < maxMoves; step++)
            {
                if (ruleset.IsSolved(s))
                    return true;

                // Find best move this step.
                var bestMove = default(Move);
                MoveDelta bestDelta = null;
                int bestScore = 0;

                foreach (var m in ruleset.GetLegalMoves(s))
                {
                    // We need to evaluate "how much would this clear?"
                    // Since TryApplyMove mutates state, we apply then undo.
                    if (!ruleset.TryApplyMove(s, m, out var delta))
                        continue;

                    int score = CountCleared(delta);
                    delta.Undo(s);

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = m;
                        bestDelta = delta;
                    }
                }

                // No move cleared anything -> stuck.
                if (bestScore <= 0 || bestDelta == null)
                    return false;

                // Apply the best move for real (recompute to avoid holding stale delta).
                if (!ruleset.TryApplyMove(s, bestMove, out var applied))
                    return false;

                moveSequence.Add(bestMove);

                // Safety: if the chosen move didn’t reduce arrow count, bail.
                // (Shouldn’t happen if score computed from delta was >0, but keep robust.)
                if (CountCleared(applied) <= 0)
                    return false;
            }

            // Hit move budget without clearing
            return ruleset.IsSolved(s);
        }

        /// <summary>
        /// Returns how many Arrow tiles were changed into Empty by this delta.
        /// Works with the MoveDelta/CellChange structure from the ruleset code I gave you.
        /// If your delta differs, adapt here.
        /// </summary>
        static int CountCleared(MoveDelta d)
        {
            int cleared = 0;
            var changes = d.changes;
            for (int i = 0; i < changes.Count; i++)
            {
                var c = changes[i];
                if (c.before.type == TileType.Arrow && c.after.type == TileType.Empty)
                    cleared++;
            }
            return cleared;
        }

        static BoardState CloneBoard(BoardState src)
        {
            var dst = new BoardState(src.width, src.height);
            Array.Copy(src.tiles, dst.tiles, src.tiles.Length);
            return dst;
        }
    }
}
