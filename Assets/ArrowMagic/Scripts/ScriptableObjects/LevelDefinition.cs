using System;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    [CreateAssetMenu(menuName = "PixelBug/Arrow Magic/Level Definition", fileName = "LevelDefinition")]
    public sealed class LevelDefinition : ScriptableObject
    {
        public enum LevelSource
        {
            Procedural = 0,
            Authored = 1,
        }

        [Header("Identity")]
        public string levelId = "level_001";
        public LevelSource source = LevelSource.Procedural;

        [Header("Board + Rules")]
        public BoardSettings board = new BoardSettings();

        [Header("Generation")]
        public GenerationSettings generation = new GenerationSettings();

        [Header("Authored Layout")]
        public AuthoredLevelData authoredLevel = new();

        [Header("Lose Condition")]
        public LoseSettings lose = new LoseSettings();

        [Header("Masking")]
        public MaskSettings masking = new MaskSettings();
        
        [Header("Presentation")]
        public IntroAnimationSettings introSettings;
        public Palette palette;
        
        [Header("Arrow Colors")]
        public BoardController.ArrowColorMode arrowColorMode = BoardController.ArrowColorMode.UsePalette;
        [Range(1, 64)]
        public int arrowColorMaskQuantizeSteps = 16;

        [Header("Arrow Hit")]
        public bool tintOnHit = true;
        public Color hitTint = Color.white;

        // ---------------------------
        // Serializable groups
        // ---------------------------

        [Serializable]
        public sealed class BoardSettings
        {
            public int width = 8;
            public int height = 8;
            public int seed = 12345;
        }

        [Serializable]
        public sealed class GenerationSettings
        {
            [Range(0.05f, 1.0f)] public float arrowCoverage = 0.95f;
            [InspectorName("初始可移动箭头数量")]
            [Min(0)] public int initialMovableArrowCount = 0;
            [InspectorName("目标难度分值")]
            [Min(0)] public int targetDifficultyScore = 0;
            [HideInInspector] public int fixedGenerationSeed = 0;
            [HideInInspector] public int minPathLen = BoardController.DefaultMinArrowCellCount;
            [Tooltip("生成时允许的单条箭头链最大格子数。")]
            [InspectorName("最大链长")]
            [Min(2)] public int maxPathLength = 20;
            [Range(0f, 1f)] public float twistiness = 0.50f;
            [HideInInspector] public bool validateWithGreedy = true;
        }

        [Serializable]
        public sealed class LoseSettings
        {
            [Min(1)] public int blockedLoseLimit = 3;
        }

        [Serializable]
        public sealed class MaskSettings
        {
            // Authoring-time: direct reference
            public Texture2D spawnMask;

            [HideInInspector] [Range(0f, 1f)] public float alphaThreshold = 0.5f;
            [HideInInspector] public bool useAlphaOnly = true;
            [HideInInspector] [Range(0f, 1f)] public float luminanceThreshold = 0.5f;

            [HideInInspector] public bool useMaskToDefineBoardSize = true;
        }
    }
}
