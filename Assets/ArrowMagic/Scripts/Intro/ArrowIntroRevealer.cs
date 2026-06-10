using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public static class ArrowIntroRevealer
    {
        public static IEnumerator RevealOne(
            ArrowSnakeOverlay2D overlay,
            List<Vector3> pts,
            int bodyLenPoints,
            float duration,
            AnimationCurve ease)
        {
            if (overlay == null) yield break;
            if (pts == null || pts.Count == 0) yield break;

            float t = 0f;
            float endProgress = (pts.Count - 1);

            // Allocate required segments once.
            overlay.EnsureSegmentCount(Mathf.Max(0, bodyLenPoints));

            while (t < duration)
            {
                t += Time.deltaTime;
                float u = Mathf.Clamp01(t / Mathf.Max(0.0001f, duration));
                float eased = (ease != null) ? ease.Evaluate(u) : u;

                float headProgress = eased * endProgress;
                overlay.UpdateSnake(pts, headProgress);

                yield return null;
            }

            overlay.UpdateSnake(pts, endProgress);
        }
    }
}
