using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public sealed class LevelProgression : MonoBehaviour
    {
        [SerializeField] LevelLoader loader;
        [SerializeField] LevelPack activePack;

        [Header("Flow")]
        [Tooltip("If true, after the last level, the win button becomes 'Replay' and restarts the pack.")]
        [SerializeField] bool loopPack = false;
        
        int currentIndex = -1;
        
        public int CurrentLevelNumber => currentIndex + 1;

        void Start()
        { 
            if (activePack != null)
                StartPack(activePack);
        }
        
        void Reset()
        {
            loader = FindFirstObjectByType<LevelLoader>();
        }

        public void StartPack(LevelPack pack)
        {
            if (pack == null || pack.levels == null || pack.levels.Length == 0)
            {
                Debug.LogWarning("[LevelProgression] Pack empty.");
                return;
            }

            activePack = pack;
            currentIndex = -1;

            LoadNextLevel();
        }

        public void LoadNextLevel()
        {
            if (activePack == null) return;

            currentIndex++;

            if (currentIndex >= activePack.levels.Length)
            {
                if (loopPack)
                {
                    // Restart pack
                    currentIndex = 0;
                    loader.Apply(activePack.levels[currentIndex]);
                    return;
                }

                Debug.Log("[LevelProgression] Pack complete!");
                return;
            }

            loader.Apply(activePack.levels[currentIndex]);
        }

        public bool TryJumpToLevel(int levelNumber)
        {
            if (activePack == null || activePack.levels == null || activePack.levels.Length == 0)
                return false;

            if (loader == null)
                loader = FindFirstObjectByType<LevelLoader>();

            if (loader == null)
            {
                Debug.LogWarning("[LevelProgression] Cannot jump: missing LevelLoader.");
                return false;
            }

            int targetIndex = Mathf.Clamp(levelNumber - 1, 0, activePack.levels.Length - 1);
            currentIndex = targetIndex;
            loader.Apply(activePack.levels[currentIndex]);
            return true;
        }

        public bool HasNextLevel()
        {
            if (activePack == null) return false;
            return currentIndex + 1 < activePack.levels.Length;
        }

        public bool LoopPack => loopPack;

        /// <summary>
        /// UI helper: what should the win button say, given current progression state?
        /// </summary>
        public string GetWinButtonLabel()
        {
            if (HasNextLevel()) return "Next Level";
            return loopPack ? "Replay" : "Level Complete";
        }

        /// <summary>
        /// UI helper: what should the win button do when clicked?
        /// </summary>
        public void OnWinButtonPressed()
        {
            if (HasNextLevel())
            {
                LoadNextLevel();
                return;
            }

            if (loopPack)
            {
                // Replay pack from beginning (LoadNextLevel() will wrap)
                currentIndex = activePack != null ? activePack.levels.Length - 1 : -1;
                LoadNextLevel();
                return;
            }

            // Non-looping pack: do nothing (or you could raise an event).
            Debug.Log("[LevelProgression] Pack complete (non-loop).");
        }

        public void SkipCurrentLevel()
        {
            if (activePack == null || activePack.levels == null || activePack.levels.Length == 0)
                return;

            if (HasNextLevel())
            {
                LoadNextLevel();
                return;
            }

            if (loopPack)
            {
                currentIndex = activePack.levels.Length - 1;
                LoadNextLevel();
                return;
            }

            Debug.Log("[LevelProgression] Cannot skip: already at the last level.");
        }

        public int CurrentLevelIndex => currentIndex;
        public int LevelCount => activePack != null ? activePack.levels.Length : 0;
    }
}
