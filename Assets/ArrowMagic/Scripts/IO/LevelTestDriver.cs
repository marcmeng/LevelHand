using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public sealed class LevelTestDriver : MonoBehaviour
    {
        [SerializeField] LevelLoader loader;
        [Header("Test Assets")]
        [SerializeField] LevelDefinition level;
        [SerializeField] LevelPack pack;
        [SerializeField] int packIndex = 0;

        void Reset()
        {
            loader = GetComponent<LevelLoader>();
        }

        [ContextMenu("TEST/Load LevelDefinition")]
        public void TestLoadLevel()
        {
            if (loader == null) loader = GetComponent<LevelLoader>();
            if (loader == null || level == null) return;
            loader.Apply(level);
        }

        [ContextMenu("TEST/Load LevelPack[index]")]
        public void TestLoadPackIndex()
        {
            if (loader == null) loader = GetComponent<LevelLoader>();
            if (loader == null || pack == null || pack.levels == null || pack.levels.Length == 0) return;

            packIndex = Mathf.Clamp(packIndex, 0, pack.levels.Length - 1);
            loader.Apply(pack.levels[packIndex]);
        }
    }
}