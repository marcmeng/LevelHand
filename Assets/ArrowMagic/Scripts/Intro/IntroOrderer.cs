using System;
using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public static class IntroOrderer
    {
        public static void Apply(List<IntroEntry> entries, BoardState s, IntroStartOrder order, int randomSeed)
        {
            float cx = (s.width - 1) * 0.5f;
            float cy = (s.height - 1) * 0.5f;

            float Dist2(Vector2Int p)
            {
                float dx = p.x - cx;
                float dy = p.y - cy;
                return dx * dx + dy * dy;
            }

            int StableTie(in IntroEntry a, in IntroEntry b)
            {
                int ty = a.tailCell.y.CompareTo(b.tailCell.y);
                if (ty != 0) return ty;
                return a.tailCell.x.CompareTo(b.tailCell.x);
            }

            switch (order)
            {
                case IntroStartOrder.BottomToTop:
                    entries.Sort((a, b) =>
                    {
                        int cy2 = a.tailCell.y.CompareTo(b.tailCell.y);
                        if (cy2 != 0) return cy2;
                        return a.tailCell.x.CompareTo(b.tailCell.x);
                    });
                    break;

                case IntroStartOrder.TopToBottom:
                    entries.Sort((a, b) =>
                    {
                        int cy2 = b.tailCell.y.CompareTo(a.tailCell.y);
                        if (cy2 != 0) return cy2;
                        return a.tailCell.x.CompareTo(b.tailCell.x);
                    });
                    break;

                case IntroStartOrder.LeftToRight:
                    entries.Sort((a, b) =>
                    {
                        int cx2 = a.tailCell.x.CompareTo(b.tailCell.x);
                        if (cx2 != 0) return cx2;
                        return a.tailCell.y.CompareTo(b.tailCell.y);
                    });
                    break;

                case IntroStartOrder.RightToLeft:
                    entries.Sort((a, b) =>
                    {
                        int cx2 = b.tailCell.x.CompareTo(a.tailCell.x);
                        if (cx2 != 0) return cx2;
                        return a.tailCell.y.CompareTo(b.tailCell.y);
                    });
                    break;

                case IntroStartOrder.DiagonalBLToUR:
                    entries.Sort((a, b) =>
                    {
                        int da = a.tailCell.x + a.tailCell.y;
                        int db = b.tailCell.x + b.tailCell.y;

                        int cd = da.CompareTo(db);
                        if (cd != 0) return cd;

                        int cx2 = a.tailCell.x.CompareTo(b.tailCell.x);
                        if (cx2 != 0) return cx2;

                        return StableTie(a, b);
                    });
                    break;

                case IntroStartOrder.DiagonalULToBR:
                    entries.Sort((a, b) =>
                    {
                        int da = a.tailCell.x + (s.height - 1 - a.tailCell.y);
                        int db = b.tailCell.x + (s.height - 1 - b.tailCell.y);

                        int cd = da.CompareTo(db);
                        if (cd != 0) return cd;

                        int cx2 = a.tailCell.x.CompareTo(b.tailCell.x);
                        if (cx2 != 0) return cx2;

                        return StableTie(a, b);
                    });
                    break;

                case IntroStartOrder.DiagonalBRToUL:
                    entries.Sort((a, b) =>
                    {
                        int da = (s.width - 1 - a.tailCell.x) + a.tailCell.y;
                        int db = (s.width - 1 - b.tailCell.x) + b.tailCell.y;

                        int cd = da.CompareTo(db);
                        if (cd != 0) return cd;

                        int cx2 = b.tailCell.x.CompareTo(a.tailCell.x);
                        if (cx2 != 0) return cx2;

                        return StableTie(a, b);
                    });
                    break;

                case IntroStartOrder.DiagonalURToBL:
                    entries.Sort((a, b) =>
                    {
                        int da = (s.width - 1 - a.tailCell.x) + (s.height - 1 - a.tailCell.y);
                        int db = (s.width - 1 - b.tailCell.x) + (s.height - 1 - b.tailCell.y);

                        int cd = da.CompareTo(db);
                        if (cd != 0) return cd;

                        int cx2 = b.tailCell.x.CompareTo(a.tailCell.x);
                        if (cx2 != 0) return cx2;

                        return StableTie(a, b);
                    });
                    break;

                case IntroStartOrder.CenterToEdge:
                case IntroStartOrder.EdgeToCenter:
                    entries.Sort((a, b) =>
                    {
                        int cd = Dist2(a.tailCell).CompareTo(Dist2(b.tailCell));
                        if (cd != 0) return cd;
                        return StableTie(a, b);
                    });
                    if (order == IntroStartOrder.EdgeToCenter)
                        entries.Reverse();
                    break;

                case IntroStartOrder.ByArrowLength_ShortToLong:
                    entries.Sort((a, b) =>
                    {
                        int cl = a.arrowLenTiles.CompareTo(b.arrowLenTiles);
                        if (cl != 0) return cl;
                        return StableTie(a, b);
                    });
                    break;

                case IntroStartOrder.ByArrowLength_LongToShort:
                    entries.Sort((a, b) =>
                    {
                        int cl = b.arrowLenTiles.CompareTo(a.arrowLenTiles);
                        if (cl != 0) return cl;
                        return StableTie(a, b);
                    });
                    break;

                case IntroStartOrder.ByColorHue:
                    entries.Sort((a, b) =>
                    {
                        int ck = a.colorHue.CompareTo(b.colorHue);
                        if (ck != 0) return ck;
                        return StableTie(a, b);
                    });
                    break;

                case IntroStartOrder.ByColorIndex:
                    entries.Sort((a, b) =>
                    {
                        int ci = a.colorIndex.CompareTo(b.colorIndex);
                        if (ci != 0) return ci;
                        return StableTie(a, b);
                    });
                    break;

                case IntroStartOrder.CheckerboardWave:
                    entries.Sort((a, b) =>
                    {
                        int pa = (a.tailCell.x + a.tailCell.y) & 1;
                        int pb = (b.tailCell.x + b.tailCell.y) & 1;

                        int cp = pa.CompareTo(pb);
                        if (cp != 0) return cp;

                        int cd = Dist2(a.tailCell).CompareTo(Dist2(b.tailCell));
                        if (cd != 0) return cd;

                        return StableTie(a, b);
                    });
                    break;

                case IntroStartOrder.Random:
                {
                    int seed = (randomSeed != 0) ? randomSeed : unchecked((int)DateTime.UtcNow.Ticks);
                    var rng = new System.Random(seed);

                    for (int i = entries.Count - 1; i > 0; i--)
                    {
                        int j = rng.Next(i + 1);
                        (entries[i], entries[j]) = (entries[j], entries[i]);
                    }
                    break;
                }
            }
        }
    }
}
