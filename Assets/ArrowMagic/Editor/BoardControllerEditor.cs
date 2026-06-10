#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace PixelBug.ArrowMagic.EditorTools
{
    [CustomEditor(typeof(BoardController))]
    public sealed class BoardControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (target is not BoardController controller)
                return;

            EditorGUILayout.Space(8);

            var difficultyRange = controller.GetDifficultyScoreRecommendation();
            int resolvedDifficulty = controller.ResolvedDifficultyScoreTarget;
            string difficultyMode = controller.targetDifficultyScore <= 0 ? "关闭筛选" : "手动目标";

            EditorGUILayout.LabelField("难度筛选", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                $"可选难度范围：{BoardGenerationTuning.FormatDifficultyScore(difficultyRange.Min)} ~ {BoardGenerationTuning.FormatDifficultyScore(difficultyRange.Max)}\n" +
                $"当前生成目标：{(resolvedDifficulty > 0 ? BoardGenerationTuning.FormatDifficultyScore(resolvedDifficulty) : "关闭")}（{difficultyMode}）\n" +
                $"固定候选种子：{(controller.fixedGenerationSeed != 0 ? controller.fixedGenerationSeed.ToString() : "无")}\n" +
                $"估算依据：可生成格子 {difficultyRange.AllowedCellCount}，覆盖后箭头格约 {difficultyRange.TargetArrowTileCount}，估算箭头链约 {difficultyRange.EstimatedChainCount}。",
                MessageType.Info);

            using (new EditorGUI.DisabledScope(difficultyRange.Max <= 0))
            {
                int currentDifficulty = controller.targetDifficultyScore > 0
                    ? Mathf.Clamp(controller.targetDifficultyScore, difficultyRange.Min, difficultyRange.Max)
                    : Mathf.Clamp(difficultyRange.Preferred, difficultyRange.Min, difficultyRange.Max);

                EditorGUI.BeginChangeCheck();
                int nextDifficulty = EditorGUILayout.IntSlider(
                    "目标难度分值",
                    currentDifficulty,
                    difficultyRange.Min,
                    difficultyRange.Max);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(controller, "设置目标难度分值");
                    controller.targetDifficultyScore = nextDifficulty;
                    EditorUtility.SetDirty(controller);
                }

                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("打开难度候选面板"))
                    {
                        DifficultyCandidateBrowserWindow.Open(controller);
                    }

                    if (GUILayout.Button("使用推荐难度"))
                    {
                        Undo.RecordObject(controller, "使用推荐目标难度");
                        controller.targetDifficultyScore = difficultyRange.Preferred;
                        EditorUtility.SetDirty(controller);
                    }

                    if (GUILayout.Button("关闭难度筛选"))
                    {
                        Undo.RecordObject(controller, "关闭难度筛选");
                        controller.targetDifficultyScore = 0;
                        EditorUtility.SetDirty(controller);
                    }

                    using (new EditorGUI.DisabledScope(controller.fixedGenerationSeed == 0))
                    {
                        if (GUILayout.Button("解除固定候选"))
                        {
                            Undo.RecordObject(controller, "解除固定候选");
                            controller.fixedGenerationSeed = 0;
                            EditorUtility.SetDirty(controller);
                        }
                    }
                }
            }

            EditorGUILayout.Space(8);
            if (GUILayout.Button("Open Generated Level Preview"))
            {
                GeneratedLevelPreviewWindow.Open(controller);
            }

            EditorGUILayout.Space(8);

            var recommendation = controller.GetInitialMovableArrowRecommendation();
            int resolvedTarget = controller.ResolvedInitialMovableArrowCountTarget;
            string mode = controller.initialMovableArrowCount <= 0 ? "自动推荐" : "手动目标";

            EditorGUILayout.HelpBox(
                $"初始可移动箭头数量推荐范围：{recommendation.Min} ~ {recommendation.Max}\n" +
                $"推荐值：{recommendation.Preferred}，当前生成目标：{resolvedTarget}（{mode}）\n" +
                $"估算依据：可生成格子 {recommendation.AllowedCellCount}，覆盖后箭头格约 {recommendation.TargetArrowTileCount}，估算箭头链约 {recommendation.EstimatedChainCount}。\n" +
                "填写 0 表示自动使用推荐值；填写大于 0 的数值表示生成时尽量筛选到该目标。",
                MessageType.Info);

            using (new EditorGUI.DisabledScope(recommendation.Preferred <= 0))
            {
                if (GUILayout.Button("使用推荐值"))
                {
                    Undo.RecordObject(controller, "使用推荐的初始可移动箭头数量");
                    controller.initialMovableArrowCount = recommendation.Preferred;
                    EditorUtility.SetDirty(controller);
                }
            }

            if (GUILayout.Button("打印关卡统计"))
            {
                controller.PrintCurrentGenerationStats();
            }
        }
    }
}
#endif
