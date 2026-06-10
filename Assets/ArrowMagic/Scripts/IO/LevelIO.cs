using System.IO;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public static class LevelIO
    {
        static string LevelsDir =>
            Path.Combine(Application.persistentDataPath, "levels");

        static string FileForId(string levelId) =>
            Path.Combine(LevelsDir, $"{levelId}.json");

        public static void Save(LevelSaveData data)
        {
            if (data == null) return;

            Directory.CreateDirectory(LevelsDir);

            var json = JsonUtility.ToJson(data, prettyPrint: true);
            File.WriteAllText(FileForId(data.levelId), json);

            Debug.Log($"[LevelIO] Saved level '{data.levelId}' to: {FileForId(data.levelId)}");
        }

        public static bool TryLoad(string levelId, out LevelSaveData data)
        {
            data = null;

            var path = FileForId(levelId);
            if (!File.Exists(path))
            {
                Debug.LogWarning($"[LevelIO] Level not found: {path}");
                return false;
            }

            var json = File.ReadAllText(path);
            data = JsonUtility.FromJson<LevelSaveData>(json);

            if (data == null)
            {
                Debug.LogError($"[LevelIO] Failed to parse JSON for levelId='{levelId}'.");
                return false;
            }

            return true;
        }
    }
}