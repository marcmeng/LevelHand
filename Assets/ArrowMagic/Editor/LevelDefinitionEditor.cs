#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace PixelBug.ArrowMagic.EditorTools
{
    [CustomEditor(typeof(LevelDefinition))]
    public sealed class LevelDefinitionEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space(10);

            using (new EditorGUILayout.VerticalScope("box"))
            {
                EditorGUILayout.LabelField("Authoring Tools", EditorStyles.boldLabel);

                if (GUILayout.Button("Capture From Scene"))
                {
                    CaptureFromScene((LevelDefinition)target);
                }

                EditorGUILayout.HelpBox(
                    "Captures current scene settings into this LevelDefinition asset:\n" +
                    "• BoardController: board/generation/lose/mask\n" +
                    "• BoardColorService2D: hit tint + current palette\n" +
                    "• LevelIntroAnimator2D: intro settings\n\n" +
                    "Tip: Put your scene in a known-good state, then click Capture.",
                    MessageType.Info);
            }
        }

        static void CaptureFromScene(LevelDefinition def)
        {
            if (def == null) return;

            // Find scene objects
            var controller = FindFirst<BoardController>();
            var colorService = FindFirst<BoardColorService2D>();
            var introAnimator = FindFirst<LevelIntroAnimator2D>();

            if (controller == null)
            {
                EditorUtility.DisplayDialog("Capture From Scene",
                    "No BoardController found in the active scene.",
                    "OK");
                return;
            }

            Undo.RecordObject(def, "Capture LevelDefinition From Scene");
            def.source = LevelDefinition.LevelSource.Procedural;

            // -------------------------
            // BoardController -> LevelDefinition.board
            // -------------------------
            def.board.width = GetSO(controller, "width", def.board.width);
            def.board.height = GetSO(controller, "height", def.board.height);
            def.board.seed = GetSO(controller, "seed", def.board.seed);

            // -------------------------
            // BoardController -> LevelDefinition.generation
            // -------------------------
            def.generation.arrowCoverage = GetSO(controller, "arrowCoverage", def.generation.arrowCoverage);
            def.generation.initialMovableArrowCount = GetSO(controller, "initialMovableArrowCount", def.generation.initialMovableArrowCount);
            def.generation.targetDifficultyScore = GetSO(controller, "targetDifficultyScore", def.generation.targetDifficultyScore);
            def.generation.fixedGenerationSeed = GetSO(controller, "fixedGenerationSeed", def.generation.fixedGenerationSeed);
            def.generation.minPathLen = GetSO(controller, "minPathLen", def.generation.minPathLen);
            def.generation.maxPathLength = GetSO(controller, "maxPathLength", def.generation.maxPathLength);
            def.generation.validateWithGreedy = GetSO(controller, "validateWithGreedy", def.generation.validateWithGreedy);

            // -------------------------
            // BoardController -> LevelDefinition.lose
            // -------------------------
            def.lose.blockedLoseLimit = GetSO(controller, "blockedLoseLimit", def.lose.blockedLoseLimit);

            // -------------------------
            // BoardController -> LevelDefinition.masking
            // (these are often private [SerializeField], so we use SerializedObject)
            // -------------------------
            def.masking.spawnMask = GetSO(controller, "spawnMask", def.masking.spawnMask);
            def.masking.alphaThreshold = GetSO(controller, "alphaThreshold", def.masking.alphaThreshold);
            def.masking.useAlphaOnly = GetSO(controller, "useAlphaOnly", def.masking.useAlphaOnly);
            def.masking.luminanceThreshold = GetSO(controller, "luminanceThreshold", def.masking.luminanceThreshold);
            def.masking.useMaskToDefineBoardSize = GetSO(controller, "useMaskToDefineBoardSize", def.masking.useMaskToDefineBoardSize);

            // If your BoardController has a flag for this later, capture it too.
            // For now, keep whatever is on the LevelDefinition asset.
            // def.masking.useMaskToDefineBoardSize = ...

            // -------------------------
            // BoardColorService2D -> hit tint
            // -------------------------
            if (colorService != null)
            {
                def.tintOnHit = GetSO(colorService, "tintOnHit", def.tintOnHit);
                def.hitTint = GetSO(colorService, "hitTint", def.hitTint);
            }

            // -------------------------
            // LevelIntroAnimator2D -> intro settings reference
            // -------------------------
            if (introAnimator != null)
            {
                def.introSettings = GetSO(introAnimator, "settings", def.introSettings);
            }

            // -------------------------
            // PaletteManager -> palette reference (best-effort)
            // -------------------------
            if (colorService != null)
            {
                var foundPalette =
                    GetProperty<Palette>(colorService, "CurrentPalette");

                if (foundPalette != null)
                    def.palette = foundPalette;
            }

            EditorUtility.SetDirty(def);
            AssetDatabase.SaveAssets();

            Debug.Log($"[LevelDefinitionEditor] Captured scene into LevelDefinition: {def.name}");
        }

        // ------------------------------------------------------------
        // Utilities
        // ------------------------------------------------------------

        static T FindFirst<T>() where T : UnityEngine.Object
        {
#if UNITY_2023_1_OR_NEWER
            return UnityEngine.Object.FindFirstObjectByType<T>();
#else
            return UnityEngine.Object.FindObjectOfType<T>();
#endif
        }

        static T GetSO<T>(UnityEngine.Object obj, string fieldName, T fallback)
        {
            if (obj == null) return fallback;

            var so = new SerializedObject(obj);
            var p = so.FindProperty(fieldName);
            if (p == null) return fallback;

            try
            {
                if (typeof(T) == typeof(int) && p.propertyType == SerializedPropertyType.Integer)
                    return (T)(object)p.intValue;

                if (typeof(T) == typeof(float) && p.propertyType == SerializedPropertyType.Float)
                    return (T)(object)p.floatValue;

                if (typeof(T) == typeof(bool) && p.propertyType == SerializedPropertyType.Boolean)
                    return (T)(object)p.boolValue;

                if (typeof(T) == typeof(string) && p.propertyType == SerializedPropertyType.String)
                    return (T)(object)p.stringValue;

                if (typeof(T) == typeof(Color) && p.propertyType == SerializedPropertyType.Color)
                    return (T)(object)p.colorValue;

                if (typeof(UnityEngine.Object).IsAssignableFrom(typeof(T)) &&
                    p.propertyType == SerializedPropertyType.ObjectReference)
                    return (T)(object)p.objectReferenceValue;

                return fallback;
            }
            catch
            {
                return fallback;
            }
        }

        static SignalTravelMode GetSOEnum(UnityEngine.Object obj, string fieldName, SignalTravelMode fallback)
        {
            var so = new SerializedObject(obj);
            var p = so.FindProperty(fieldName);
            if (p == null) return fallback;

            if (p.propertyType == SerializedPropertyType.Enum)
                return (SignalTravelMode)p.enumValueIndex;

            return fallback;
        }

        static T GetProperty<T>(UnityEngine.Object obj, string propName) where T : class
        {
            if (obj == null) return null;

            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var pi = obj.GetType().GetProperty(propName, flags);
            if (pi == null) return null;

            try
            {
                return pi.GetValue(obj) as T;
            }
            catch
            {
                return null;
            }
        }
    }
}
#endif
