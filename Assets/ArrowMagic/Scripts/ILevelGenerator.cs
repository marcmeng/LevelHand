using System;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    [Serializable]
    public struct LevelSpec
    {
        public int width;
        public int height;
        public int seed;

        [Range(0, 1f)] public float arrowFill;
        public int minPathLen;
        public int maxPathLen;
        public float twistiness;
        
        public bool[] canSpawnHere;

        public static LevelSpec Default(int w, int h, int seed) => new LevelSpec
        {
            width = w,
            height = h,
            seed = seed,
            arrowFill = 0.35f,
            minPathLen = Mathf.Max(4, (w + h) / 2),
            maxPathLen = Mathf.Max(8, (w * h) / 2),
            twistiness = 0.50f,
        };
    }

    public interface ILevelGenerator
    {
        BoardState Generate(LevelSpec spec);
    }
}
