#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace PixelBug.ArrowMagic.EditorTools
{
    public sealed class GeneratedLevelPreviewWindow : EditorWindow
    {
        BoardController controller;
        BoardState previewBoard;
        BoardGenerationTuning.BoardGenerationStats previewStats;
        int previewSeed;
        Vector2 scroll;
        string status;
        MessageType statusType = MessageType.Info;

        [MenuItem("Tools/Generated Level Preview")]
        public static void Open()
        {
            Open(null);
        }

        [MenuItem("Tools/生成关卡预览")]
        public static void OpenChinese()
        {
            Open(null);
        }

        public static void Open(BoardController initialController)
        {
            var window = GetWindow<GeneratedLevelPreviewWindow>("Level Preview");
            window.minSize = new Vector2(520f, 620f);
            if (initialController != null)
                window.controller = initialController;
            else
                window.ResolveController();
            window.Repaint();
        }

        void OnEnable()
        {
            ResolveController();
        }

        void OnSelectionChange()
        {
            if (Selection.activeGameObject != null &&
                Selection.activeGameObject.TryGetComponent(out BoardController selectedController))
            {
                controller = selectedController;
                Repaint();
            }
        }

        void OnGUI()
        {
            using (var scope = new EditorGUILayout.ScrollViewScope(scroll))
            {
                scroll = scope.scrollPosition;
                DrawSceneRefs();

                if (controller == null)
                {
                    EditorGUILayout.HelpBox("No BoardController found in the active scene.", MessageType.Warning);
                    return;
                }

                DrawGenerationSettings();
                DrawActions();
                DrawPreview();
                DrawStatus();
            }
        }

        void DrawSceneRefs()
        {
            EditorGUILayout.LabelField("Scene", EditorStyles.boldLabel);
            controller = (BoardController)EditorGUILayout.ObjectField(
                "BoardController",
                controller,
                typeof(BoardController),
                true);
        }

        void DrawGenerationSettings()
        {
            EditorGUILayout.Space(6f);
            EditorGUILayout.LabelField("Generation", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();

            int width = EditorGUILayout.IntField("Width", Mathf.Max(1, controller.width));
            int height = EditorGUILayout.IntField("Height", Mathf.Max(1, controller.height));
            int seed = EditorGUILayout.IntField("Seed", controller.seed);
            float coverage = EditorGUILayout.Slider("Arrow Coverage", controller.arrowCoverage, 0.05f, 1f);
            float twistiness = EditorGUILayout.Slider("Twistiness", controller.arrowTwistiness, 0f, 1f);
            int minPath = EditorGUILayout.IntField(
                "Min Arrow Cells",
                Mathf.Max(BoardController.DefaultMinArrowCellCount, controller.minPathLen));
            int maxPath = EditorGUILayout.IntField("Max Chain Length", Mathf.Max(2, controller.maxPathLength));
            int initialMovable = EditorGUILayout.IntField(
                "Initial Movable Target",
                Mathf.Max(0, controller.initialMovableArrowCount));
            int targetDifficulty = EditorGUILayout.IntField(
                "Difficulty Target",
                Mathf.Max(0, controller.targetDifficultyScore));
            bool validate = EditorGUILayout.Toggle("Greedy Validate", controller.validateWithGreedy);
            SignalTravelMode travelMode = (SignalTravelMode)EditorGUILayout.EnumPopup("Travel Mode", controller.travelMode);

            if (!EditorGUI.EndChangeCheck())
                return;

            Undo.RecordObject(controller, "Edit generated level settings");
            controller.width = Mathf.Max(1, width);
            controller.height = Mathf.Max(1, height);
            controller.seed = seed;
            controller.arrowCoverage = Mathf.Clamp(coverage, 0.05f, 1f);
            controller.arrowTwistiness = Mathf.Clamp01(twistiness);
            controller.minPathLen = Mathf.Max(BoardController.DefaultMinArrowCellCount, minPath);
            controller.maxPathLength = Mathf.Max(2, maxPath);
            controller.initialMovableArrowCount = Mathf.Max(0, initialMovable);
            controller.targetDifficultyScore = Mathf.Max(0, targetDifficulty);
            controller.validateWithGreedy = validate;
            controller.travelMode = travelMode;
            EditorUtility.SetDirty(controller);
            EditorSceneManager.MarkSceneDirty(controller.gameObject.scene);
            status = "Settings changed. Generate again to preview the new board.";
            statusType = MessageType.Info;
        }

        void DrawActions()
        {
            EditorGUILayout.Space(8f);
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Generate And Preview", GUILayout.Height(28f)))
                    GenerateAndApply(controller.seed);

                if (GUILayout.Button("Next Seed", GUILayout.Height(28f)))
                    GenerateAndApply(controller.seed + 1);
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                using (new EditorGUI.DisabledScope(previewBoard == null))
                {
                    if (GUILayout.Button("Apply Preview To Scene"))
                        ApplyPreviewToScene();

                    if (GUILayout.Button("Save As LevelDefinition"))
                        SavePreviewAsLevelDefinition();
                }
            }
        }

        void DrawPreview()
        {
            EditorGUILayout.Space(8f);
            EditorGUILayout.LabelField("Preview", EditorStyles.boldLabel);

            if (previewBoard == null)
            {
                EditorGUILayout.HelpBox("Click Generate And Preview to create a board.", MessageType.Info);
                return;
            }

            float side = Mathf.Min(position.width - 34f, 520f);
            Rect previewRect = GUILayoutUtility.GetRect(side, side, GUILayout.ExpandWidth(false));
            DifficultyCandidateBrowserWindow.DrawBoardPreview(previewBoard, controller, previewRect);

            EditorGUILayout.Space(6f);
            EditorGUILayout.LabelField("Seed", previewSeed.ToString());
            EditorGUILayout.LabelField("Difficulty", BoardGenerationTuning.FormatDifficultyScore(previewStats.DifficultyScore));
            EditorGUILayout.LabelField("Arrow Chains", previewStats.ArrowCount.ToString());
            EditorGUILayout.LabelField("Arrow Cells", previewStats.ArrowTileCount.ToString());
            EditorGUILayout.LabelField("Initial Movable", previewStats.InitialMovableArrowChainCount.ToString());
            EditorGUILayout.LabelField("Max Chain", previewStats.MaxChainLength.ToString());
        }

        void DrawStatus()
        {
            if (!string.IsNullOrEmpty(status))
                EditorGUILayout.HelpBox(status, statusType);
        }

        void GenerateAndApply(int seed)
        {
            if (controller == null)
                return;

            Undo.RecordObject(controller, "Generate level preview");
            controller.seed = seed;

            var generator = new ClearAllArrowsGenerator();
            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = controller.travelMode
            });

            int targetInitialMovable = controller.ResolvedInitialMovableArrowCountTarget;
            int targetDifficulty = controller.ResolvedDifficultyScoreTarget;
            var settings = BoardController.ResolveGenerationSearchSettings(
                BoardController.GenerationMode.Filtered,
                controller.validateWithGreedy,
                targetInitialMovable,
                targetDifficulty);

            BoardState best = null;
            int bestSeed = seed;
            int bestDiff = int.MaxValue;
            BoardGenerationTuning.BoardGenerationStats bestStats = default;
            bool foundExact = false;

            for (int attempt = 0; attempt < settings.AttemptCount; attempt++)
            {
                int candidateSeed = BoardGenerationTuning.BuildCandidateSeed(seed, attempt);
                BoardState candidate = generator.Generate(controller.BuildGenerationSpec(candidateSeed));

                if (settings.ValidateWithGreedy &&
                    !GreedyValidator.TryClearAllByGreedy(candidate, rules, 300, out _))
                    continue;

                var stats = BoardGenerationTuning.CalculateBoardGenerationStats(candidate, rules);
                int diff = targetDifficulty > 0
                    ? Mathf.Abs(stats.DifficultyScore - targetDifficulty)
                    : targetInitialMovable > 0
                        ? Mathf.Abs(stats.InitialMovableArrowChainCount - targetInitialMovable)
                        : 0;

                if (best == null || diff < bestDiff)
                {
                    best = candidate;
                    bestSeed = candidateSeed;
                    bestDiff = diff;
                    bestStats = stats;
                }

                if (diff == 0)
                {
                    foundExact = true;
                    break;
                }
            }

            if (best == null)
            {
                bestSeed = seed;
                best = generator.Generate(controller.BuildGenerationSpec(bestSeed));
                bestStats = BoardGenerationTuning.CalculateBoardGenerationStats(best, rules);
            }

            previewBoard = best;
            previewSeed = bestSeed;
            previewStats = bestStats;
            ApplyPreviewToScene();

            status = foundExact || bestDiff == 0
                ? "Generated and applied to the scene."
                : "Generated the closest matching board and applied it to the scene.";
            statusType = MessageType.Info;
        }

        void ApplyPreviewToScene()
        {
            if (controller == null || previewBoard == null)
                return;

            Undo.RecordObject(controller, "Apply generated level preview");
            controller.ApplyGeneratedCandidate(previewBoard, previewSeed, previewStats.DifficultyScore);
            EditorUtility.SetDirty(controller);
            EditorSceneManager.MarkSceneDirty(controller.gameObject.scene);
            Selection.activeObject = controller;
        }

        void SavePreviewAsLevelDefinition()
        {
            if (controller == null || previewBoard == null)
                return;

            string path = EditorUtility.SaveFilePanelInProject(
                "Save generated LevelDefinition",
                $"Level_{controller.width}x{controller.height}_seed{previewSeed}_score{previewStats.DifficultyScore}.asset",
                "asset",
                "Choose where to save the generated LevelDefinition.");

            if (string.IsNullOrEmpty(path))
                return;

            var definition = CreateInstance<LevelDefinition>();
            definition.levelId = $"level_{controller.width}x{controller.height}_seed_{previewSeed}";
            definition.source = LevelDefinition.LevelSource.Procedural;
            definition.board.width = controller.width;
            definition.board.height = controller.height;
            definition.board.seed = controller.seed;
            definition.generation.arrowCoverage = controller.arrowCoverage;
            definition.generation.initialMovableArrowCount = controller.initialMovableArrowCount;
            definition.generation.targetDifficultyScore = previewStats.DifficultyScore;
            definition.generation.fixedGenerationSeed = previewSeed;
            definition.generation.minPathLen = controller.ResolvedMinArrowCellCount;
            definition.generation.maxPathLength = Mathf.Max(2, controller.maxPathLength);
            definition.generation.twistiness = controller.arrowTwistiness;
            definition.generation.validateWithGreedy = controller.validateWithGreedy;
            definition.lose.blockedLoseLimit = controller.blockedLoseLimit;
            definition.arrowColorMode = controller.ArrowColor;
            definition.arrowColorMaskQuantizeSteps = controller.ArrowColorMaskQuantizeSteps;

            var controllerSO = new SerializedObject(controller);
            SerializedProperty spawnMask = controllerSO.FindProperty("spawnMask");
            if (spawnMask != null)
                definition.masking.spawnMask = spawnMask.objectReferenceValue as Texture2D;

            AssetDatabase.CreateAsset(definition, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Selection.activeObject = definition;
            status = $"Saved LevelDefinition: {path}";
            statusType = MessageType.Info;
        }

        void ResolveController()
        {
            if (controller == null)
                controller = FindFirstObjectByType<BoardController>();
        }
    }
}
#endif
