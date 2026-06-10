using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public sealed class LevelLoader : MonoBehaviour
    {
        [Header("Scene Refs")]
        [SerializeField] private BoardController controller;
        [SerializeField] private BoardColorService2D colorService;
        [SerializeField] private LevelIntroAnimator2D introAnimator;

        [Header("Optional Defaults (if save data omits keys)")]
        [SerializeField] private IntroAnimationSettings defaultIntroSettings;
        [SerializeField] private Palette defaultPalette;
        
        [ContextMenu("EXPORT/Save Current To Disk (JSON)")]
        void ExportJsonQuick()
        {
            SaveCurrentToDisk("exported_level");
        }
        
        void Reset()
        {
            controller = FindFirstObjectByType<BoardController>();
            colorService = FindFirstObjectByType<BoardColorService2D>();
            introAnimator = FindFirstObjectByType<LevelIntroAnimator2D>();
        }

        // ----------------------------
        // Authoring path: LevelDefinition
        // ----------------------------
        public void Apply(LevelDefinition def)
        {
            if (def == null) return;

            // Palette
            var palette = def.palette != null ? def.palette : defaultPalette;
            if (palette != null && colorService != null)
                colorService.SetPalette(palette);

            // Intro settings
            var intro = def.introSettings != null ? def.introSettings : defaultIntroSettings;
            if (introAnimator != null && intro != null)
                introAnimator.SetSettings(intro);
            
            // Hit tint
            if (colorService != null)
                colorService.ApplyHitTintSettings(def.tintOnHit, def.hitTint);
            
            // BoardController
            if (controller != null)
                controller.ApplyLevelDefinition(def);
        }

        // ----------------------------
        // Runtime save/load path: LevelSaveData
        // ----------------------------
        public void Apply(LevelSaveData data)
        {
            if (data == null) return;

            // Resolve assets from Resources keys (optional)
            Texture2D mask = !string.IsNullOrEmpty(data.spawnMaskKey)
                ? Resources.Load<Texture2D>(data.spawnMaskKey)
                : null;

            IntroAnimationSettings intro = !string.IsNullOrEmpty(data.introSettingsKey)
                ? Resources.Load<IntroAnimationSettings>(data.introSettingsKey)
                : defaultIntroSettings;

            Palette palette = !string.IsNullOrEmpty(data.paletteKey)
                ? Resources.Load<Palette>(data.paletteKey)
                : defaultPalette;

            if (!string.IsNullOrEmpty(data.spawnMaskKey) && mask == null)
                Debug.LogWarning($"[LevelLoader] spawnMaskKey not found in Resources: {data.spawnMaskKey}");

            if (!string.IsNullOrEmpty(data.introSettingsKey) && intro == null)
                Debug.LogWarning($"[LevelLoader] introSettingsKey not found in Resources: {data.introSettingsKey}");

            if (!string.IsNullOrEmpty(data.paletteKey) && palette == null)
                Debug.LogWarning($"[LevelLoader] paletteKey not found in Resources: {data.paletteKey}");

            // Palette
            if (palette != null && colorService != null)
                colorService.SetPalette(palette);

            // Intro settings
            if (introAnimator != null && intro != null)
                introAnimator.SetSettings(intro);
            
            // Hit tint
            if (colorService != null)
                colorService.ApplyHitTintSettings(data.tintOnHit, data.hitTint);
            
            // Apply to controller
            if (controller != null)
                controller.ApplyLevelSaveData(data, mask);
        }

        public bool LoadFromDisk(string levelId)
        {
            if (!LevelIO.TryLoad(levelId, out var data))
                return false;

            Apply(data);
            return true;
        }

        // Convenience: capture current scene settings and save as JSON
        public void SaveCurrentToDisk(string levelId,
            string paletteKey = null,
            string introSettingsKey = null,
            string maskKey = null)
        {
            if (controller == null)
            {
                Debug.LogError("[LevelLoader] Missing BoardController.");
                return;
            }

            var data = controller.CaptureLevelSaveData(levelId);

            // Fill optional asset keys for later reload
            data.paletteKey = paletteKey;
            data.introSettingsKey = introSettingsKey;
            data.spawnMaskKey = maskKey;

            if (colorService != null)
            {
                data.tintOnHit = colorService.TintOnHit;
                data.hitTint = colorService.HitTint;
            }

            LevelIO.Save(data);
        }
    }
}
