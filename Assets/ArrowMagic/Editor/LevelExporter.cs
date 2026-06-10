#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace PixelBug.ArrowMagic.EditorTools
{
    public static class LevelExporter
    {
        // -----------------------------
        // MENU: Export current scene state -> LevelDefinition asset
        // -----------------------------
        [MenuItem("Tools/Export/Create LevelDefinition From Scene")]
        public static void CreateLevelDefinitionFromScene()
        {
            var controller = Object.FindFirstObjectByType<BoardController>();
            if (controller == null)
            {
                Debug.LogError("[ArrowMagic Export] No BoardController found in scene.");
                return;
            }

            // OPTIONAL: pull extras from scene if present
            var loader = Object.FindFirstObjectByType<LevelLoader>();
            var colorService = Object.FindFirstObjectByType<BoardColorService2D>();
            var introAnimator = Object.FindFirstObjectByType<LevelIntroAnimator2D>();

            // Create asset
            var def = ScriptableObject.CreateInstance<LevelDefinition>();

            // Let user pick where to save
            var defaultName = $"Level_{controller.width}x{controller.height}_seed{controller.seed}.asset";
            var path = EditorUtility.SaveFilePanelInProject(
                "Save LevelDefinition",
                defaultName,
                "asset",
                "Choose where to save the LevelDefinition asset.");

            if (string.IsNullOrEmpty(path))
            {
                Debug.LogWarning("[ArrowMagic Export] Export cancelled.");
                return;
            }

            // Write controller settings into LevelDefinition using SerializedObject so we don’t
            // need direct compile-time access to all nested structs/classes.
            var so = new SerializedObject(def);
            SetEnum(so, "source", LevelDefinition.LevelSource.Procedural);

            // board.*
            SetInt(so, "board.width", controller.width);
            SetInt(so, "board.height", controller.height);
            SetInt(so, "board.seed", controller.seed);
            
            SetEnum(so, "board.travelMode", controller.travelMode);

            // generation.*
            SetFloat(so, "generation.arrowCoverage", controller.arrowCoverage);
            SetInt(so, "generation.initialMovableArrowCount", controller.initialMovableArrowCount);
            SetInt(so, "generation.targetDifficultyScore", controller.targetDifficultyScore);
            SetInt(so, "generation.fixedGenerationSeed", controller.fixedGenerationSeed);
            SetInt(so, "generation.minPathLen", controller.minPathLen);
            SetInt(so, "generation.maxPathLength", controller.maxPathLength);
            SetFloat(so, "generation.twistiness", controller.arrowTwistiness);
            SetBool(so, "generation.validateWithGreedy", controller.validateWithGreedy);

            // lose.*
            SetInt(so, "lose.blockedLoseLimit", controller.blockedLoseLimit);
            
            // arrow colors
            SetEnum(so, "arrowColorMode", controller.ArrowColor);
            SetInt(so, "arrowColorMaskQuantizeSteps", controller.ArrowColorMaskQuantizeSteps);

            // masking.*
            // Note: spawnMask is private in BoardController; LevelDefinition is the source of truth for authoring anyway.
            // So we try to pull it from the controller via SerializedObject.
            var controllerSO = new SerializedObject(controller);
            var spawnMaskProp = controllerSO.FindProperty("spawnMask");
            var useMaskSizeProp = controllerSO.FindProperty("useMaskToDefineBoardSize");

            if (spawnMaskProp != null) SetObjectRef(so, "masking.spawnMask", spawnMaskProp.objectReferenceValue);
            if (useMaskSizeProp != null) SetBool(so, "masking.useMaskToDefineBoardSize", useMaskSizeProp.boolValue);

            // presentation (optional)
            // We try to infer palette/intro from the scene if you’re using those systems.
            // If a property doesn’t exist, Set* will safely warn once.
            if (colorService != null)
                SetObjectRef(so, "palette", colorService.CurrentPalette);

            // Intro settings (optional)
            // LevelIntroAnimator2D doesn't expose a public getter, so read the serialized field.
            if (introAnimator != null)
            {
                var introSO = new SerializedObject(introAnimator);
                var settingsProp = introSO.FindProperty("settings"); // private [SerializeField] IntroAnimationSettings settings;
                if (settingsProp != null)
                    SetObjectRef(so, "introSettings", settingsProp.objectReferenceValue);
            }
            
            // hit tint (optional)
            if (colorService != null)
            {
                SetBool(so, "tintOnHit", colorService.TintOnHit);
                SetColor(so, "hitTint", colorService.HitTint);
            }

            so.ApplyModifiedPropertiesWithoutUndo();

            AssetDatabase.CreateAsset(def, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Selection.activeObject = def;
            Debug.Log($"[ArrowMagic Export] Created LevelDefinition: {path}");
        }

        // -----------------------------
        // MENU: Append LevelDefinition to LevelPack
        // -----------------------------
        [MenuItem("Tools/Export/Append Selected LevelDefinition To LevelPack")]
        public static void AppendSelectedLevelDefinitionToPack()
        {
            var levelDef = Selection.activeObject as LevelDefinition;
            if (levelDef == null)
            {
                Debug.LogError("[ArrowMagic Export] Select a LevelDefinition asset first.");
                return;
            }

            // Ask for pack
            var pack = EditorUtility.OpenFilePanel("Select LevelPack asset", "Assets", "asset");
            if (string.IsNullOrEmpty(pack))
            {
                Debug.LogWarning("[ArrowMagic Export] No pack selected.");
                return;
            }

            var packPath = MakeProjectRelative(pack);
            var packObj = AssetDatabase.LoadAssetAtPath<Object>(packPath);
            if (packObj == null)
            {
                Debug.LogError($"[ArrowMagic Export] Could not load asset at: {packPath}");
                return;
            }

            // We don’t assume LevelPack field structure beyond it having a serialized array named "levels"
            var packSO = new SerializedObject(packObj);
            var levelsProp = packSO.FindProperty("levels");
            if (levelsProp == null || !levelsProp.isArray)
            {
                Debug.LogError("[ArrowMagic Export] Selected asset does not appear to be a LevelPack (missing array property 'levels').");
                return;
            }

            int n = levelsProp.arraySize;
            levelsProp.arraySize = n + 1;
            levelsProp.GetArrayElementAtIndex(n).objectReferenceValue = levelDef;

            packSO.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(packObj);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"[ArrowMagic Export] Appended {levelDef.name} to pack: {packPath}");
        }

        // -----------------------------
        // Helpers
        // -----------------------------
        static void SetInt(SerializedObject so, string path, int value)
        {
            var p = so.FindProperty(path);
            if (p == null) { WarnMissing(path); return; }
            p.intValue = value;
        }

        static void SetFloat(SerializedObject so, string path, float value)
        {
            var p = so.FindProperty(path);
            if (p == null) { WarnMissing(path); return; }
            p.floatValue = value;
        }

        static void SetBool(SerializedObject so, string path, bool value)
        {
            var p = so.FindProperty(path);
            if (p == null) { WarnMissing(path); return; }
            p.boolValue = value;
        }

        static void SetEnum<T>(SerializedObject so, string path, T value) where T : System.Enum
        {
            var p = so.FindProperty(path);
            if (p == null) { WarnMissing(path); return; }
            p.enumValueIndex = System.Convert.ToInt32(value);
        }

        static void SetColor(SerializedObject so, string path, Color value)
        {
            var p = so.FindProperty(path);
            if (p == null) { WarnMissing(path); return; }
            p.colorValue = value;
        }

        static void SetObjectRef(SerializedObject so, string path, Object value)
        {
            var p = so.FindProperty(path);
            if (p == null) { WarnMissing(path); return; }
            p.objectReferenceValue = value;
        }

        static void WarnMissing(string path)
        {
            // Don’t spam: a simple warning is enough; if a field is renamed later,
            // the exporter still works for everything else.
            Debug.LogWarning($"[ArrowMagic Export] LevelDefinition missing property: '{path}' (safe to ignore if your schema differs).");
        }

        static string MakeProjectRelative(string fullPath)
        {
            fullPath = fullPath.Replace("\\", "/");
            var dataPath = Application.dataPath.Replace("\\", "/");
            if (!fullPath.StartsWith(dataPath))
                return fullPath; // fallback
            return "Assets" + fullPath.Substring(dataPath.Length);
        }
    }
}
#endif
