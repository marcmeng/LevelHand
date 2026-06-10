#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace PixelBug.ArrowMagic.EditorTools
{
    [CustomEditor(typeof(BoardAuthoringTools))]
    public sealed class BoardAuthoringToolsEditor : Editor
    {
        SerializedProperty controllerProp;

        void OnEnable()
        {
            controllerProp = serializedObject.FindProperty("controller");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var tools = (BoardAuthoringTools)target;

            EditorGUILayout.PropertyField(controllerProp, new GUIContent("控制器"));

            if (controllerProp.objectReferenceValue == null)
            {
                var found = tools.GetComponent<BoardController>();
                if (found != null)
                    controllerProp.objectReferenceValue = found;
            }

            serializedObject.ApplyModifiedProperties();

            var controller = tools.Controller;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("关卡编辑工具", EditorStyles.boldLabel);

            using (new EditorGUI.DisabledScope(controller == null))
            {
                if (GUILayout.Button("导出关卡定义资产"))
                {
                    LevelExporter.CreateLevelDefinitionFromScene();
                }

                if (GUILayout.Button("保存关卡 JSON"))
                {
                    controller.SaveCurrentLevelToLevelIO();
                    Debug.Log("[ArrowMagic Authoring] 已保存当前关卡 JSON。");
                }
            }

            if (controller == null)
            {
                EditorGUILayout.HelpBox(
                    "这个物体上没有找到或指定 BoardController。",
                    MessageType.Warning);
            }
            else
            {
                EditorGUILayout.HelpBox(
                    $"正在使用 '{controller.gameObject.name}' 上的 BoardController。",
                    MessageType.Info);
            }
        }
    }
}
#endif
