using System;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    /// <summary>
    /// JSON-friendly save data. Stores primitive settings + Resources keys for assets.
    /// (Later you can swap Resources keys for Addressables keys without changing structure much.)
    /// </summary>
    [Serializable]
    public sealed class LevelSaveData
    {
        public string levelId = "level_001";

        // -------------------------
        // Board + Rules
        // -------------------------
        public int width = 8;
        public int height = 8;
        public int seed = 12345;

        // -------------------------
        // Generation
        // -------------------------
        public float arrowCoverage = 0.95f;
        public int initialMovableArrowCount = 0;
        public int targetDifficultyScore = 0;
        public int fixedGenerationSeed = 0;
        public int minPathLen = BoardController.DefaultMinArrowCellCount;
        public int maxPathLength = 20;
        public float twistiness = 0.50f;
        public bool validateWithGreedy = true;

        // -------------------------
        // Lose Condition
        // -------------------------
        public int blockedLoseLimit = 3;

        // -------------------------
        // Masking (Resources key)
        // -------------------------
        public string spawnMaskKey = null; // e.g. "Arrow Magic/Masks/mask_01"

        // -------------------------
        // Presentation (Resources keys)
        // -------------------------
        public string introSettingsKey = null; // e.g. "Arrow Magic/Intro/Intro_Default"
        public string paletteKey = null;       // e.g. "Arrow Magic/Palettes/Palette_A"

        // -------------------------
        // Arrow Colors
        // -------------------------
        public BoardController.ArrowColorMode arrowColorMode = BoardController.ArrowColorMode.UsePalette;
        public int arrowColorMaskQuantizeSteps = 16;

        // -------------------------
        // Hit Tint
        // -------------------------
        public bool tintOnHit = true;
        public Color hitTint = Color.white;
    }
}
