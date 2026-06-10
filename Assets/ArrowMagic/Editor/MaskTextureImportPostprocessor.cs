#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public class MaskTextureImportPostprocessor : AssetPostprocessor
    {
        // Change this to whatever folder you want to watch.
        // Must be an "Assets/..." path.
        private const string TargetFolder = "Assets/ArrowMagic/Masks/";

        // If you want to use alpha-only and never read pixels at runtime, you can set this false
        // and bake masks in editor instead.
        private const bool ForceReadable = true;

        void OnPreprocessTexture()
        {
            // Only affect textures inside the target folder.
            if (!assetPath.StartsWith(TargetFolder))
                return;

            var importer = (TextureImporter)assetImporter;

            // Basic mask-friendly settings
            importer.textureType = TextureImporterType.Default; // not Sprite (unless you also use them as sprites)
            importer.alphaSource = TextureImporterAlphaSource.FromInput;
            importer.alphaIsTransparency = true;
            importer.npotScale = TextureImporterNPOTScale.None; // allow non-power-of-two

            importer.mipmapEnabled = false;
            importer.sRGBTexture = false; // masks are data, not color
            importer.filterMode = FilterMode.Point;
            importer.wrapMode = TextureWrapMode.Clamp;

            // Compression: keep it uncompressed so thresholds behave consistently
            importer.textureCompression = TextureImporterCompression.Uncompressed;

            // Critical if you call GetPixels/GetPixels32 at runtime
            importer.isReadable = ForceReadable;

            // Avoid platform overrides doing unexpected things
            // (Optional) Force a known format per-platform by overriding settings:
            SetPlatform(importer, "Standalone", maxSize: 2048, TextureImporterFormat.RGBA32);
            SetPlatform(importer, "iPhone", maxSize: 2048, TextureImporterFormat.RGBA32);
            SetPlatform(importer, "Android", maxSize: 2048, TextureImporterFormat.RGBA32);
            SetPlatform(importer, "WebGL", maxSize: 2048, TextureImporterFormat.RGBA32);
        }

        private static void SetPlatform(TextureImporter importer, string platform, int maxSize,
            TextureImporterFormat format)
        {
            var settings = new TextureImporterPlatformSettings
            {
                name = platform,
                overridden = true,
                maxTextureSize = maxSize,
                format = format,
                compressionQuality = 0
            };
            importer.SetPlatformTextureSettings(settings);
        }

        // Optional: quick utility to reimport everything in the folder
        [MenuItem("Tools/Masks/Reimport Mask Textures")]
        private static void ReimportAllMasks()
        {
            string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { TargetFolder });
            int count = 0;

            try
            {
                AssetDatabase.StartAssetEditing();
                foreach (var guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
                    count++;
                }
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
                Debug.Log($"Reimported {count} mask texture(s) under {TargetFolder}");
            }
        }
    }
}
#endif
