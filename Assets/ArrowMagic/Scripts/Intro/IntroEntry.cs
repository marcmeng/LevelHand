using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public struct IntroEntry
    {
        public ArrowSnakeOverlay2D overlay;
        public List<Vector3> pts;
        public int bodyLenPoints;

        public List<Vector2Int> cells;

        public Vector2Int tailCell;  // ordering key
        public int arrowLenTiles;    // ordering key
        public float colorHue;       // 0..1
        public int colorIndex;       // ordering key (preferred)

        public float revealDuration; // per entry (duration or computed from speed)
    }
}