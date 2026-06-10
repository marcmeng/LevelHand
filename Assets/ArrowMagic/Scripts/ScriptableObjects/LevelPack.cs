using UnityEngine;

namespace PixelBug.ArrowMagic
{
    [CreateAssetMenu(menuName = "PixelBug/Arrow Magic/Level Pack", fileName = "LevelPack")]
    public sealed class LevelPack : ScriptableObject
    {
        [Header("Metadata")]
        public string packId = "pack_001";
        public string displayName = "Starter Pack";

        [Header("Levels")]
        public LevelDefinition[] levels;
    }
}