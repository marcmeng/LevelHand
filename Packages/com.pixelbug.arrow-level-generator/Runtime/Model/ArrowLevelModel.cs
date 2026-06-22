using System;
using System.Collections.Generic;

namespace PixelBug.ArrowLevelGenerator
{
    public enum ArrowDirection : byte
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    public enum ArrowLevelFamily : byte
    {
        Unknown = 0,
        Shell = 1,
        Section = 2,
        Lock = 3,
        Maze = 4,
        Dense = 5,
        Sweep = 6,
        Shape = 7
    }

    public enum ArrowDifficultyBand : byte
    {
        Refresh = 0,
        Normal = 1,
        Hard = 2,
        VeryHard = 3,
        Extreme = 4
    }

    [Serializable]
    public sealed class ArrowChainData
    {
        public readonly List<int> indices = new List<int>();
        public int colorIndex;

        public ArrowChainData()
        {
        }

        public ArrowChainData(IEnumerable<int> headToTailIndices, int colorIndex = 0)
        {
            if (headToTailIndices != null)
                indices.AddRange(headToTailIndices);
            this.colorIndex = colorIndex;
        }
    }

    [Serializable]
    public sealed class ArrowLevelData
    {
        public string levelId = "arrow_level";
        public int width;
        public int height;
        public ArrowDifficultyBand difficulty;
        public ArrowLevelFamily family;
        public readonly List<ArrowChainData> chains = new List<ArrowChainData>();
        public readonly List<int> blockIndices = new List<int>();
        public readonly Dictionary<string, string> metadata = new Dictionary<string, string>();

        public ArrowLevelData()
        {
        }

        public ArrowLevelData(string levelId, int width, int height)
        {
            this.levelId = string.IsNullOrWhiteSpace(levelId) ? "arrow_level" : levelId;
            this.width = Math.Max(1, width);
            this.height = Math.Max(1, height);
        }

        public int Area => Math.Max(1, width * height);

        public int Index(int x, int y)
        {
            return x + y * width;
        }

        public bool InBounds(int x, int y)
        {
            return (uint)x < (uint)width && (uint)y < (uint)height;
        }

        public void ToXY(int index, out int x, out int y)
        {
            x = index % width;
            y = index / width;
        }
    }

    public static class ArrowDirectionUtility
    {
        public static ArrowDirection Opposite(ArrowDirection direction)
        {
            return (ArrowDirection)(((int)direction + 2) & 3);
        }

        public static void ToDelta(ArrowDirection direction, out int dx, out int dy)
        {
            switch (direction)
            {
                case ArrowDirection.Up:
                    dx = 0;
                    dy = 1;
                    return;
                case ArrowDirection.Right:
                    dx = 1;
                    dy = 0;
                    return;
                case ArrowDirection.Down:
                    dx = 0;
                    dy = -1;
                    return;
                default:
                    dx = -1;
                    dy = 0;
                    return;
            }
        }

        public static bool TryFromDelta(int dx, int dy, out ArrowDirection direction)
        {
            if (dx == 0 && dy == 1)
            {
                direction = ArrowDirection.Up;
                return true;
            }

            if (dx == 1 && dy == 0)
            {
                direction = ArrowDirection.Right;
                return true;
            }

            if (dx == 0 && dy == -1)
            {
                direction = ArrowDirection.Down;
                return true;
            }

            if (dx == -1 && dy == 0)
            {
                direction = ArrowDirection.Left;
                return true;
            }

            direction = ArrowDirection.Up;
            return false;
        }
    }
}
