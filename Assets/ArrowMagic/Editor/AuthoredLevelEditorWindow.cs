#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace PixelBug.ArrowMagic.EditorTools
{
    public sealed class AuthoredLevelEditorWindow : EditorWindow
    {
        [Serializable]
        sealed class EscapeLevelData
        {
            public int GridXSize;
            public int GridYSize;
            public List<EscapeArrowData> Arrows = new();
        }

        [Serializable]
        sealed class EscapeArrowData
        {
            public List<int> Indices = new();
            public int ColorIndex;
        }

        static readonly Color[] ArrowColors =
        {
            new Color32(94, 230, 255, 255),
            new Color32(108, 255, 181, 255),
            new Color32(111, 168, 255, 255),
            new Color32(180, 140, 255, 255),
            new Color32(255, 158, 122, 255),
            new Color32(255, 217, 102, 255),
            new Color32(255, 127, 168, 255),
            new Color32(77, 255, 210, 255),
            new Color32(201, 182, 255, 255),
            new Color32(182, 255, 106, 255),
        };

        AuthoredLevelData level = new();
        string levelId = "authored_level_001";
        BoardController controller;
        BoardColorService2D colorService;
        BoardView2D boardView;
        int selectedArrowIndex = -1;
        readonly List<int> strokeIndices = new();
        bool dragging;
        Vector2 scroll;

        [MenuItem("Tools/手工关卡编辑器")]
        public static void Open()
        {
            var window = GetWindow<AuthoredLevelEditorWindow>("手工关卡编辑器");
            window.minSize = new Vector2(620, 520);
        }

        void OnGUI()
        {
            using (var scope = new EditorGUILayout.ScrollViewScope(scroll))
            {
                scroll = scope.scrollPosition;
                DrawToolbar();
                EditorGUILayout.Space(8);
                DrawSelectedArrowTools();
                EditorGUILayout.Space(8);
                DrawGrid();
                EditorGUILayout.Space(8);
                DrawValidation();
            }
        }

        void DrawToolbar()
        {
            if (controller == null)
                controller = FindFirstObjectByType<BoardController>();
            if (colorService == null)
                colorService = FindFirstObjectByType<BoardColorService2D>();
            if (boardView == null)
                boardView = FindFirstObjectByType<BoardView2D>();

            EditorGUILayout.LabelField("场景", EditorStyles.boldLabel);
            controller = (BoardController)EditorGUILayout.ObjectField(
                "BoardController",
                controller,
                typeof(BoardController),
                true);
            colorService = (BoardColorService2D)EditorGUILayout.ObjectField(
                "BoardColorService2D",
                colorService,
                typeof(BoardColorService2D),
                true);
            boardView = (BoardView2D)EditorGUILayout.ObjectField(
                "BoardView2D",
                boardView,
                typeof(BoardView2D),
                true);

            EditorGUILayout.Space(6);
            EditorGUILayout.LabelField("关卡", EditorStyles.boldLabel);
            levelId = EditorGUILayout.TextField("关卡 ID", levelId);

            using (new EditorGUI.DisabledScope(level.arrows.Count > 0))
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    level.width = Mathf.Max(1, EditorGUILayout.IntField("宽度", level.width));
                    level.height = Mathf.Max(1, EditorGUILayout.IntField("高度", level.height));
                }
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("新建"))
                    NewLevel();

                if (GUILayout.Button("导入 Escape JSON"))
                    ImportEscapeJson();

                if (GUILayout.Button("保存为 LevelDefinition"))
                    SaveAsLevelDefinition();
            }

            EditorGUILayout.LabelField("箭头数量", level.arrows.Count.ToString());
        }

        void DrawSelectedArrowTools()
        {
            using (new EditorGUI.DisabledScope(!TryGetSelectedArrow(out var arrow)))
            {
                EditorGUILayout.LabelField("选中箭头", EditorStyles.boldLabel);
                int selectedLabel = selectedArrowIndex >= 0 ? selectedArrowIndex + 1 : 0;
                EditorGUILayout.LabelField("编号", selectedLabel.ToString());
                EditorGUILayout.LabelField("长度", arrow != null ? arrow.indices.Count.ToString() : "-");

                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("上一色"))
                    {
                        arrow.colorIndex = (arrow.colorIndex + ArrowColors.Length - 1) % ArrowColors.Length;
                        Repaint();
                    }

                    if (GUILayout.Button("下一色"))
                    {
                        arrow.colorIndex = (arrow.colorIndex + 1) % ArrowColors.Length;
                        Repaint();
                    }

                    if (GUILayout.Button("删除"))
                    {
                        DeleteSelectedArrow();
                    }
                }
            }
        }

        void DrawGrid()
        {
            EditorGUILayout.LabelField("棋盘", EditorStyles.boldLabel);

            float availableWidth = Mathf.Max(200f, position.width - 44f);
            float cellSize = Mathf.Clamp(Mathf.Floor(availableWidth / Mathf.Max(1, level.width)), 24f, 56f);
            Rect gridRect = GUILayoutUtility.GetRect(
                level.width * cellSize,
                level.height * cellSize,
                GUILayout.ExpandWidth(false));

            var e = Event.current;
            int hotCell = CellFromMouse(gridRect, cellSize, e.mousePosition);

            DrawGridBackground(gridRect, cellSize);
            DrawArrows(gridRect, cellSize);
            DrawStrokePreview(gridRect, cellSize);

            if (gridRect.Contains(e.mousePosition))
                HandleGridEvent(e, hotCell);
        }

        void DrawGridBackground(Rect gridRect, float cellSize)
        {
            for (int y = 0; y < level.height; y++)
            for (int x = 0; x < level.width; x++)
            {
                int index = x + y * level.width;
                Rect r = CellRect(gridRect, cellSize, x, y);
                EditorGUI.DrawRect(r, new Color(0.15f, 0.16f, 0.18f));
                Handles.color = new Color(1f, 1f, 1f, 0.13f);
                Handles.DrawAAPolyLine(
                    new Vector3(r.xMin, r.yMin),
                    new Vector3(r.xMax, r.yMin),
                    new Vector3(r.xMax, r.yMax),
                    new Vector3(r.xMin, r.yMax),
                    new Vector3(r.xMin, r.yMin));

                if (FindArrowAt(index) >= 0)
                    continue;

                GUI.Label(r, index.ToString(), EditorStyles.centeredGreyMiniLabel);
            }
        }

        void DrawArrows(Rect gridRect, float cellSize)
        {
            Handles.BeginGUI();
            for (int i = 0; i < level.arrows.Count; i++)
            {
                var arrow = level.arrows[i];
                if (arrow.indices == null || arrow.indices.Count == 0)
                    continue;

                Color color = ArrowColors[Mathf.Abs(arrow.colorIndex) % ArrowColors.Length];
                if (i == selectedArrowIndex)
                    color = Color.Lerp(color, Color.white, 0.35f);

                Handles.color = color;
                for (int p = 0; p < arrow.indices.Count - 1; p++)
                {
                    Vector2 a = CellCenter(gridRect, cellSize, arrow.indices[p]);
                    Vector2 b = CellCenter(gridRect, cellSize, arrow.indices[p + 1]);
                    Handles.DrawAAPolyLine(8f, a, b);
                }

                Vector2 head = CellCenter(gridRect, cellSize, arrow.indices[0]);
                Vector2 next = CellCenter(gridRect, cellSize, arrow.indices[1]);
                DrawHead(head, (head - next).normalized, color, cellSize);
            }
            Handles.EndGUI();
        }

        void DrawStrokePreview(Rect gridRect, float cellSize)
        {
            if (strokeIndices.Count == 0)
                return;

            Handles.BeginGUI();
            Handles.color = new Color(1f, 1f, 1f, 0.75f);
            for (int i = 0; i < strokeIndices.Count - 1; i++)
            {
                Vector2 a = CellCenter(gridRect, cellSize, strokeIndices[i]);
                Vector2 b = CellCenter(gridRect, cellSize, strokeIndices[i + 1]);
                Handles.DrawAAPolyLine(5f, a, b);
            }
            Handles.EndGUI();
        }

        void DrawValidation()
        {
            if (AuthoredLevelBuilder.TryBuildBoard(level, out _, out string error))
                EditorGUILayout.HelpBox("当前关卡数据有效，可以保存。", MessageType.Info);
            else
                EditorGUILayout.HelpBox(error, MessageType.Warning);

            using (new EditorGUILayout.HorizontalScope())
            {
                using (new EditorGUI.DisabledScope(controller == null || !IsCurrentLevelValid()))
                {
                    if (GUILayout.Button("应用到场景"))
                        ApplyToScene();
                }

                using (new EditorGUI.DisabledScope(!IsCurrentLevelValid()))
                {
                    if (GUILayout.Button("保存为 LevelDefinition"))
                        SaveAsLevelDefinition();
                }
            }
        }

        void HandleGridEvent(Event e, int cell)
        {
            if (cell < 0)
                return;

            if (e.type == EventType.MouseDown && e.button == 0)
            {
                BeginInteraction(cell);
                e.Use();
            }
            else if (e.type == EventType.MouseDrag && e.button == 0 && dragging)
            {
                AddStrokeCell(cell);
                e.Use();
            }
            else if (e.type == EventType.MouseUp && e.button == 0 && dragging)
            {
                EndInteraction();
                e.Use();
            }
        }

        void BeginInteraction(int cell)
        {
            dragging = true;
            strokeIndices.Clear();

            int arrowIndex = FindArrowAt(cell);
            if (arrowIndex >= 0)
            {
                selectedArrowIndex = arrowIndex;
                var arrow = level.arrows[arrowIndex];
                if (arrow.indices[^1] == cell)
                {
                    strokeIndices.AddRange(arrow.indices);
                }
                Repaint();
                return;
            }

            selectedArrowIndex = -1;
            strokeIndices.Add(cell);
            Repaint();
        }

        void AddStrokeCell(int cell)
        {
            if (strokeIndices.Count == 0)
                return;

            if (strokeIndices[^1] == cell)
                return;

            if (strokeIndices.Count >= 2 && strokeIndices[^2] == cell)
            {
                strokeIndices.RemoveAt(strokeIndices.Count - 1);
                Repaint();
                return;
            }

            if (!AreAdjacent(strokeIndices[^1], cell))
                return;

            int owner = FindArrowAt(cell);
            if (owner >= 0)
                return;

            if (strokeIndices.Contains(cell))
                return;

            strokeIndices.Add(cell);
            Repaint();
        }

        void EndInteraction()
        {
            dragging = false;

            if (strokeIndices.Count >= 2)
            {
                var indices = new List<int>(strokeIndices);

                if (selectedArrowIndex >= 0 && selectedArrowIndex < level.arrows.Count)
                {
                    level.arrows[selectedArrowIndex].indices = indices;
                }
                else
                {
                    level.arrows.Add(new AuthoredArrowData
                    {
                        indices = indices,
                        colorIndex = level.arrows.Count % ArrowColors.Length
                    });
                    selectedArrowIndex = level.arrows.Count - 1;
                }
            }

            strokeIndices.Clear();
            Repaint();
        }

        bool TryGetSelectedArrow(out AuthoredArrowData arrow)
        {
            if (selectedArrowIndex >= 0 && selectedArrowIndex < level.arrows.Count)
            {
                arrow = level.arrows[selectedArrowIndex];
                return true;
            }

            arrow = null;
            return false;
        }

        void DeleteSelectedArrow()
        {
            if (selectedArrowIndex < 0 || selectedArrowIndex >= level.arrows.Count)
                return;

            level.arrows.RemoveAt(selectedArrowIndex);
            selectedArrowIndex = -1;
            Repaint();
        }

        void NewLevel()
        {
            level = new AuthoredLevelData
            {
                width = Mathf.Max(1, level.width),
                height = Mathf.Max(1, level.height),
                arrows = new List<AuthoredArrowData>()
            };
            selectedArrowIndex = -1;
            strokeIndices.Clear();
        }

        void ImportEscapeJson()
        {
            string path = EditorUtility.OpenFilePanel("导入 Escape 关卡 JSON", "", "json");
            if (string.IsNullOrEmpty(path))
                return;

            var imported = JsonUtility.FromJson<EscapeLevelData>(File.ReadAllText(path));
            if (imported == null)
            {
                Debug.LogError("[手工关卡编辑器] 导入失败：JSON 格式不正确。");
                return;
            }

            level = new AuthoredLevelData
            {
                width = Mathf.Max(1, imported.GridXSize),
                height = Mathf.Max(1, imported.GridYSize),
                arrows = new List<AuthoredArrowData>()
            };

            if (imported.Arrows != null)
            {
                foreach (var arrow in imported.Arrows)
                {
                    level.arrows.Add(new AuthoredArrowData
                    {
                        indices = arrow.Indices != null ? new List<int>(arrow.Indices) : new List<int>(),
                        colorIndex = arrow.ColorIndex
                    });
                }
            }

            levelId = Path.GetFileNameWithoutExtension(path);
            selectedArrowIndex = -1;
            strokeIndices.Clear();
            Repaint();
        }

        void SaveAsLevelDefinition()
        {
            if (!AuthoredLevelBuilder.TryBuildBoard(level, out _, out string error))
            {
                EditorUtility.DisplayDialog("保存失败", error, "确定");
                return;
            }

            string path = EditorUtility.SaveFilePanelInProject(
                "保存手工关卡",
                $"{levelId}.asset",
                "asset",
                "选择保存 LevelDefinition 的位置。");

            if (string.IsNullOrEmpty(path))
                return;

            var def = AssetDatabase.LoadAssetAtPath<LevelDefinition>(path);
            if (def == null)
            {
                def = CreateInstance<LevelDefinition>();
                AssetDatabase.CreateAsset(def, path);
            }
            else
            {
                Undo.RecordObject(def, "保存手工关卡");
            }

            FillDefinition(def, levelId, level);
            EditorUtility.SetDirty(def);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Selection.activeObject = def;

            Debug.Log($"[手工关卡编辑器] 已保存 LevelDefinition：{path}");
        }

        void ApplyToScene()
        {
            if (controller == null)
            {
                EditorUtility.DisplayDialog("应用失败", "场景中没有指定 BoardController。", "确定");
                return;
            }

            if (!AuthoredLevelBuilder.TryBuildBoard(level, out _, out string error))
            {
                EditorUtility.DisplayDialog("应用失败", error, "确定");
                return;
            }

            var preview = CreatePreviewDefinition(levelId, level);
            Undo.RecordObject(controller, "应用手工关卡到场景");
            controller.ApplyLevelDefinition(preview);
            ApplyAuthoredColorsToScene();
            EditorUtility.SetDirty(controller);
            EditorSceneManager.MarkSceneDirty(controller.gameObject.scene);
            Selection.activeObject = controller;

            Debug.Log("[手工关卡编辑器] 已应用到场景，可以直接运行体验。");
        }

        static bool IsLevelValid(AuthoredLevelData data)
        {
            return AuthoredLevelBuilder.TryBuildBoard(data, out _, out _);
        }

        void ApplyAuthoredColorsToScene()
        {
            if (colorService == null)
                colorService = FindFirstObjectByType<BoardColorService2D>();

            if (colorService == null)
                return;

            colorService.ClearAllPermanentOverrides();

            for (int i = 0; i < level.arrows.Count; i++)
            {
                var arrow = level.arrows[i];
                if (arrow.indices == null)
                    continue;

                Color color = ArrowColors[Mathf.Abs(arrow.colorIndex) % ArrowColors.Length];
                for (int j = 0; j < arrow.indices.Count; j++)
                    colorService.SetPermanentOverride(arrow.indices[j], color);
            }

            if (boardView == null)
                boardView = FindFirstObjectByType<BoardView2D>();

            boardView?.RedrawAll();
        }

        bool IsCurrentLevelValid()
        {
            return IsLevelValid(level);
        }

        public static LevelDefinition CreatePreviewDefinition(string id, AuthoredLevelData data)
        {
            var def = CreateInstance<LevelDefinition>();
            def.hideFlags = HideFlags.HideAndDontSave;
            FillDefinition(def, id, data);
            return def;
        }

        static void FillDefinition(LevelDefinition def, string id, AuthoredLevelData data)
        {
            def.levelId = id;
            def.source = LevelDefinition.LevelSource.Authored;
            def.board.width = data.width;
            def.board.height = data.height;
            def.generation.arrowCoverage = 1f;
            def.generation.minPathLen = BoardController.DefaultMinArrowCellCount;
            def.generation.maxPathLength = Mathf.Max(2, data.width * data.height);
            def.authoredLevel = CloneLevel(data);
        }

        static AuthoredLevelData CloneLevel(AuthoredLevelData source)
        {
            var clone = new AuthoredLevelData
            {
                width = source.width,
                height = source.height,
                arrows = new List<AuthoredArrowData>()
            };

            foreach (var arrow in source.arrows)
            {
                clone.arrows.Add(new AuthoredArrowData
                {
                    indices = arrow.indices != null ? new List<int>(arrow.indices) : new List<int>(),
                    colorIndex = arrow.colorIndex
                });
            }

            return clone;
        }

        int FindArrowAt(int cell)
        {
            for (int i = 0; i < level.arrows.Count; i++)
            {
                if (level.arrows[i].indices != null && level.arrows[i].indices.Contains(cell))
                    return i;
            }

            return -1;
        }

        bool AreAdjacent(int a, int b)
        {
            Vector2Int pa = AuthoredLevelBuilder.IndexToPos(a, level.width);
            Vector2Int pb = AuthoredLevelBuilder.IndexToPos(b, level.width);
            Vector2Int delta = pb - pa;
            return Mathf.Abs(delta.x) + Mathf.Abs(delta.y) == 1;
        }

        int CellFromMouse(Rect gridRect, float cellSize, Vector2 mouse)
        {
            int x = Mathf.FloorToInt((mouse.x - gridRect.xMin) / cellSize);
            int screenRow = Mathf.FloorToInt((mouse.y - gridRect.yMin) / cellSize);
            int y = ScreenRowToBoardY(screenRow, level.height);
            if (x < 0 || screenRow < 0 || x >= level.width || screenRow >= level.height)
                return -1;
            return x + y * level.width;
        }

        Rect CellRect(Rect gridRect, float cellSize, int x, int y)
        {
            int screenRow = BoardYToScreenRow(y, level.height);
            return new Rect(
                gridRect.xMin + x * cellSize + 1f,
                gridRect.yMin + screenRow * cellSize + 1f,
                cellSize - 2f,
                cellSize - 2f);
        }

        Vector2 CellCenter(Rect gridRect, float cellSize, int index)
        {
            Vector2Int pos = AuthoredLevelBuilder.IndexToPos(index, level.width);
            int screenRow = BoardYToScreenRow(pos.y, level.height);
            return new Vector2(
                gridRect.xMin + (pos.x + 0.5f) * cellSize,
                gridRect.yMin + (screenRow + 0.5f) * cellSize);
        }

        public static int ScreenRowToBoardY(int screenRow, int height)
        {
            return Mathf.Max(0, height - 1) - screenRow;
        }

        public static int BoardYToScreenRow(int boardY, int height)
        {
            return Mathf.Max(0, height - 1) - boardY;
        }

        static void DrawHead(Vector2 center, Vector2 dir, Color color, float cellSize)
        {
            if (dir.sqrMagnitude < 0.0001f)
                dir = Vector2.up;

            Vector2 side = new Vector2(-dir.y, dir.x);
            float length = cellSize * 0.32f;
            float width = cellSize * 0.18f;

            Vector3 p0 = center + dir * length;
            Vector3 p1 = center - dir * length * 0.5f + side * width;
            Vector3 p2 = center - dir * length * 0.5f - side * width;

            Handles.color = color;
            Handles.DrawAAConvexPolygon(p0, p1, p2);
        }
    }
}
#endif
