#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace PixelBug.ArrowMagic.EditorTools
{
    public sealed class DifficultyCandidateBrowserWindow : EditorWindow
    {
        BoardController controller;
        readonly List<BoardGenerationTuning.DifficultyCandidate> candidates = new();
        Vector2 listScroll;
        Vector2 detailScroll;
        int selectedIndex;
        int targetDifficultyScore;
        int candidateCount = 20;
        int attempts = 500;
        float candidateListWidth = 560f;
        bool resizingCandidateList;
        CandidateSortColumn activeSortColumn = CandidateSortColumn.None;
        bool difficultySortAscending = true;
        bool deltaSortAscending = true;
        bool seedSortAscending = true;
        bool arrowCountSortAscending = true;
        bool arrowTileCountSortAscending = true;
        bool initialMovableSortAscending = true;
        bool maxChainSortAscending = true;
        bool averageStepsSortAscending = true;
        bool minChoicesSortAscending = true;
        bool bottleneckSortAscending = true;
        string generationStatusMessage;
        MessageType generationStatusType = MessageType.Info;
        string hoveredHeaderTooltip;

        const float CandidateListMinWidth = 260f;
        const float CandidateDetailsMinWidth = 320f;
        const float SplitterWidth = 6f;
        const float CandidateRowHeight = 22f;
        const string CandidateListWidthPrefsKey = "PixelBug.ArrowMagic.DifficultyCandidateBrowser.CandidateListWidth";

        enum CandidateSortColumn
        {
            None,
            Difficulty,
            Delta,
            Seed,
            ArrowCount,
            ArrowTileCount,
            InitialMovable,
            MaxChain,
            AverageSteps,
            MinChoices,
            BottleneckSteps,
        }

        [MenuItem("Tools/Difficulty Candidate Browser")]
        public static void Open()
        {
            Open(null);
        }

        public static void Open(BoardController initialController)
        {
            var window = GetWindow<DifficultyCandidateBrowserWindow>("Difficulty Candidates");
            if (initialController == null)
                return;

            window.controller = initialController;
            window.SyncTargetFromController();
            window.Repaint();
        }

        void OnEnable()
        {
            controller = FindFirstObjectByType<BoardController>();
            candidateListWidth = EditorPrefs.GetFloat(CandidateListWidthPrefsKey, candidateListWidth);
            wantsMouseMove = true;
            SyncTargetFromController();
        }

        void OnDisable()
        {
            EditorPrefs.SetFloat(CandidateListWidthPrefsKey, candidateListWidth);
        }

        void OnSelectionChange()
        {
            if (Selection.activeGameObject != null &&
                Selection.activeGameObject.TryGetComponent(out BoardController selectedController))
            {
                controller = selectedController;
                SyncTargetFromController();
                Repaint();
            }
        }

        void OnGUI()
        {
            hoveredHeaderTooltip = null;
            DrawToolbar();

            if (controller == null)
            {
                EditorGUILayout.HelpBox("场景里没有找到 BoardController。", MessageType.Warning);
                return;
            }

            HandleCandidateKeyboardNavigation();
            ClampCandidateListWidth();

            using (new EditorGUILayout.HorizontalScope())
            {
                DrawCandidateList();
                DrawColumnSplitter();
                DrawCandidateDetails();
            }

            DrawHoveredHeaderTooltip();

            if (Event.current.type == EventType.MouseMove)
                Repaint();
        }

        void DrawToolbar()
        {
            EditorGUILayout.LabelField("候选关卡浏览器", EditorStyles.boldLabel);
            controller = (BoardController)EditorGUILayout.ObjectField(
                "BoardController",
                controller,
                typeof(BoardController),
                true);

            if (controller == null)
                return;

            DrawGenerationParameterFields();

            var range = controller.GetDifficultyScoreRecommendation();
            int min = Mathf.Max(0, range.Min);
            int max = Mathf.Max(min, range.Max);
            if (targetDifficultyScore <= 0)
                targetDifficultyScore = Mathf.Clamp(range.Preferred, min, max);

            using (new EditorGUI.DisabledScope(max <= 0))
            {
                targetDifficultyScore = EditorGUILayout.IntSlider(
                    "目标难度",
                    Mathf.Clamp(targetDifficultyScore, min, max),
                    min,
                    max);
            }

            EditorGUILayout.LabelField(
                "当前目标",
                BoardGenerationTuning.FormatDifficultyScore(targetDifficultyScore));
            EditorGUILayout.LabelField(
                "可选范围",
                $"{BoardGenerationTuning.FormatDifficultyScore(min)} ~ {BoardGenerationTuning.FormatDifficultyScore(max)}");

            using (new EditorGUILayout.HorizontalScope())
            {
                candidateCount = EditorGUILayout.IntField("候选数量", Mathf.Clamp(candidateCount, 1, 100));
                attempts = EditorGUILayout.IntField("搜索次数", Mathf.Clamp(attempts, candidateCount, 5000));
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("生成候选"))
                    GenerateCandidates();

                using (new EditorGUI.DisabledScope(candidates.Count == 0))
                {
                    if (GUILayout.Button("清空"))
                    {
                        candidates.Clear();
                        selectedIndex = 0;
                    }
                }
            }

            if (!string.IsNullOrEmpty(generationStatusMessage))
                EditorGUILayout.HelpBox(generationStatusMessage, generationStatusType);
        }

        void DrawGenerationParameterFields()
        {
            EditorGUILayout.Space(4);
            EditorGUILayout.LabelField("生成参数", EditorStyles.boldLabel);

            int nextWidth;
            int nextHeight;
            int nextSeed;
            float nextArrowCoverage;
            float nextArrowTwistiness;
            int nextMinPathLen;
            int nextMaxPathLength;
            int nextInitialMovableArrowCount;
            SignalTravelMode nextTravelMode;
            bool nextValidateWithGreedy;

            EditorGUI.BeginChangeCheck();
            using (new EditorGUILayout.HorizontalScope())
            {
                nextWidth = EditorGUILayout.IntField("宽度", Mathf.Max(1, controller.width));
                nextHeight = EditorGUILayout.IntField("高度", Mathf.Max(1, controller.height));
                nextSeed = EditorGUILayout.IntField("随机种子", controller.seed);
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                nextArrowCoverage = EditorGUILayout.Slider("箭头覆盖率", controller.arrowCoverage, 0.05f, 1f);
                nextArrowTwistiness = EditorGUILayout.Slider("箭头弯折度", controller.arrowTwistiness, 0f, 1f);
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                nextMinPathLen = EditorGUILayout.IntField("最短箭头格子数", Mathf.Max(BoardController.DefaultMinArrowCellCount, controller.minPathLen));
                nextMaxPathLength = EditorGUILayout.IntField("最大链长", Mathf.Max(2, controller.maxPathLength));
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                nextInitialMovableArrowCount = EditorGUILayout.IntField("初始可移动箭头数", Mathf.Max(0, controller.initialMovableArrowCount));
                nextTravelMode = (SignalTravelMode)EditorGUILayout.EnumPopup("移动模式", controller.travelMode);
                nextValidateWithGreedy = EditorGUILayout.Toggle("贪心校验", controller.validateWithGreedy);
            }

            if (!EditorGUI.EndChangeCheck())
                return;

            Undo.RecordObject(controller, "修改难度候选生成参数");
            controller.width = Mathf.Max(1, nextWidth);
            controller.height = Mathf.Max(1, nextHeight);
            controller.seed = nextSeed;
            controller.arrowCoverage = Mathf.Clamp(nextArrowCoverage, 0.05f, 1f);
            controller.arrowTwistiness = Mathf.Clamp01(nextArrowTwistiness);
            controller.minPathLen = Mathf.Max(BoardController.DefaultMinArrowCellCount, nextMinPathLen);
            controller.maxPathLength = Mathf.Max(2, nextMaxPathLength);
            controller.initialMovableArrowCount = Mathf.Max(0, nextInitialMovableArrowCount);
            controller.travelMode = nextTravelMode;
            controller.validateWithGreedy = nextValidateWithGreedy;
            EditorUtility.SetDirty(controller);
            generationStatusMessage = "生成参数已修改，请重新生成候选。";
            generationStatusType = MessageType.Info;
            Repaint();
        }

        void DrawCandidateList()
        {
            using (new EditorGUILayout.VerticalScope(GUILayout.Width(candidateListWidth)))
            {
                EditorGUILayout.LabelField($"候选列表 ({candidates.Count})", EditorStyles.boldLabel);
                DrawCandidateTableHeader();
                listScroll = EditorGUILayout.BeginScrollView(listScroll);

                for (int i = 0; i < candidates.Count; i++)
                {
                    DrawCandidateTableRow(i, candidates[i]);
                }

                EditorGUILayout.EndScrollView();
            }
        }

        void DrawCandidateTableRow(int index, BoardGenerationTuning.DifficultyCandidate candidate)
        {
            Rect rowRect = GUILayoutUtility.GetRect(0f, CandidateRowHeight, GUILayout.ExpandWidth(true));
            bool selected = index == selectedIndex;

            if (selected)
                EditorGUI.DrawRect(rowRect, new Color(0.24f, 0.37f, 0.56f, 0.30f));

            if (Event.current.type == EventType.MouseDown && rowRect.Contains(Event.current.mousePosition))
            {
                selectedIndex = index;
                GUI.FocusControl(null);
                Repaint();
                Event.current.Use();
            }

            float x = rowRect.x + 4f;
            float y = rowRect.y + 2f;
            float h = rowRect.height - 4f;

            DrawTableCell(new Rect(x, y, 36f, h), $"#{index + 1:00}");
            x += 40f;
            DrawTableCell(new Rect(x, y, 86f, h), candidate.Stats.DifficultyScore.ToString());
            x += 92f;
            DrawTableCell(new Rect(x, y, 56f, h), candidate.DifficultyDelta.ToString());
            x += 62f;
            DrawTableCell(new Rect(x, y, 64f, h), candidate.Seed.ToString());
            x += 70f;
            DrawTableCell(new Rect(x, y, 50f, h), candidate.Stats.ArrowCount.ToString());
            x += 56f;
            DrawTableCell(new Rect(x, y, 50f, h), candidate.Stats.ArrowTileCount.ToString());
            x += 56f;
            DrawTableCell(new Rect(x, y, 50f, h), candidate.Stats.InitialMovableArrowChainCount.ToString());
            x += 56f;
            DrawTableCell(new Rect(x, y, 50f, h), candidate.Stats.MaxChainLength.ToString());
            x += 56f;
            DrawTableCell(new Rect(x, y, 56f, h), candidate.Stats.AverageStepsToNextUnlock.ToString("0.###"));
            x += 62f;
            DrawTableCell(new Rect(x, y, 46f, h), candidate.Stats.Experience.MinChoices.ToString());
            x += 52f;
            DrawTableCell(new Rect(x, y, 46f, h), candidate.Stats.Experience.BottleneckStepCount.ToString());
        }

        static void DrawTableCell(Rect rect, string text)
        {
            GUI.Label(rect, text, EditorStyles.label);
        }

        void DrawCandidateTableHeader()
        {
            using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                DrawSortHeaderButton("序号", 40f, null, "候选在当前列表中的显示顺序，不参与排序。");
                DrawSortHeaderButton("难度", 86f, CandidateSortColumn.Difficulty, "候选关卡的实际难度分值，分值越高通常越难。");
                DrawSortHeaderButton("差值", 56f, CandidateSortColumn.Delta, "实际难度分值与目标难度分值的绝对差，越小越接近目标。");
                DrawSortHeaderButton("Seed", 64f, CandidateSortColumn.Seed, "生成这个候选关卡使用的随机种子。");
                DrawSortHeaderButton("箭头数", 50f, CandidateSortColumn.ArrowCount, "候选关卡中生成的初始箭头总数。");
                DrawSortHeaderButton("箭头格", 50f, CandidateSortColumn.ArrowTileCount, "棋盘上实际被箭头占用的格子数量。");
                DrawSortHeaderButton("初始", 50f, CandidateSortColumn.InitialMovable, "开局时可直接移动的箭头链数量。");
                DrawSortHeaderButton("最长", 50f, CandidateSortColumn.MaxChain, "候选关卡中单条箭头链的最大长度。");
                DrawSortHeaderButton("解锁步", 56f, CandidateSortColumn.AverageSteps, "当前可动箭头链清掉后，到下一条新可动箭头链出现的平均清除步数。");
                DrawSortHeaderButton("最低", 46f, CandidateSortColumn.MinChoices, "模拟清关过程中，单步可选择的最少可动箭头链数量。");
                DrawSortHeaderButton("瓶颈", 46f, CandidateSortColumn.BottleneckSteps, "模拟清关过程中，可选链数量小于等于 2 的步数。");
            }
        }

        void DrawSortHeaderButton(string label, float width, CandidateSortColumn? column, string tooltip)
        {
            string text = label;
            if (column.HasValue && activeSortColumn == column.Value)
                text += GetSortAscending(column.Value) ? " ▲" : " ▼";

            var content = new GUIContent(text, tooltip);
            Rect buttonRect;
            using (new EditorGUI.DisabledScope(!column.HasValue))
            {
                if (GUILayout.Button(content, EditorStyles.toolbarButton, GUILayout.Width(width)))
                    OnClickSortColumn(column.Value);

                buttonRect = GUILayoutUtility.GetLastRect();
            }

            if (Event.current.type == EventType.Repaint && buttonRect.Contains(Event.current.mousePosition))
                hoveredHeaderTooltip = tooltip;
        }

        void DrawHoveredHeaderTooltip()
        {
            if (string.IsNullOrEmpty(hoveredHeaderTooltip) || Event.current.type != EventType.Repaint)
                return;

            var content = new GUIContent(hoveredHeaderTooltip);
            var style = new GUIStyle(EditorStyles.helpBox)
            {
                wordWrap = true,
                alignment = TextAnchor.MiddleLeft,
                padding = new RectOffset(8, 8, 6, 6),
            };

            float width = Mathf.Min(300f, Mathf.Max(180f, position.width - 24f));
            float height = style.CalcHeight(content, width);
            Vector2 mouse = Event.current.mousePosition;
            Rect rect = new Rect(mouse.x + 14f, mouse.y + 18f, width, height);

            if (rect.xMax > position.width - 8f)
                rect.x = Mathf.Max(8f, position.width - rect.width - 8f);
            if (rect.yMax > position.height - 8f)
                rect.y = Mathf.Max(8f, mouse.y - rect.height - 12f);

            GUI.Box(rect, content, style);
        }

        void DrawColumnSplitter()
        {
            Rect rect = GUILayoutUtility.GetRect(SplitterWidth, 1f, GUILayout.ExpandHeight(true));
            EditorGUIUtility.AddCursorRect(rect, MouseCursor.ResizeHorizontal);
            EditorGUI.DrawRect(
                new Rect(rect.x + rect.width * 0.5f - 1f, rect.y, 2f, rect.height),
                new Color(0.18f, 0.18f, 0.18f, 1f));

            Event e = Event.current;
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (rect.Contains(e.mousePosition))
                    {
                        resizingCandidateList = true;
                        e.Use();
                    }
                    break;

                case EventType.MouseDrag:
                    if (resizingCandidateList)
                    {
                        candidateListWidth += e.delta.x;
                        ClampCandidateListWidth();
                        Repaint();
                        e.Use();
                    }
                    break;

                case EventType.MouseUp:
                case EventType.MouseLeaveWindow:
                    if (resizingCandidateList)
                    {
                        resizingCandidateList = false;
                        EditorPrefs.SetFloat(CandidateListWidthPrefsKey, candidateListWidth);
                        e.Use();
                    }
                    break;
            }
        }

        void ClampCandidateListWidth()
        {
            float maxWidth = Mathf.Max(
                CandidateListMinWidth,
                position.width - CandidateDetailsMinWidth - SplitterWidth - 32f);
            candidateListWidth = Mathf.Clamp(candidateListWidth, CandidateListMinWidth, maxWidth);
        }

        void HandleCandidateKeyboardNavigation()
        {
            if (candidates.Count == 0 || EditorGUIUtility.editingTextField)
                return;

            Event e = Event.current;
            if (e.type != EventType.KeyDown)
                return;

            int nextIndex = selectedIndex;
            switch (e.keyCode)
            {
                case KeyCode.UpArrow:
                    nextIndex = Mathf.Max(0, selectedIndex - 1);
                    break;

                case KeyCode.DownArrow:
                    nextIndex = Mathf.Min(candidates.Count - 1, selectedIndex + 1);
                    break;

                default:
                    return;
            }

            if (nextIndex != selectedIndex)
            {
                selectedIndex = nextIndex;
                EnsureSelectedCandidateVisible();
                Repaint();
            }

            e.Use();
        }

        void EnsureSelectedCandidateVisible()
        {
            if (selectedIndex < 0)
                return;

            float visibleHeight = Mathf.Max(CandidateRowHeight, position.height * 0.65f);
            float rowTop = selectedIndex * CandidateRowHeight;
            if (rowTop < listScroll.y)
                listScroll.y = rowTop;
            else if (rowTop + CandidateRowHeight > listScroll.y + visibleHeight)
                listScroll.y = Mathf.Max(0f, rowTop - visibleHeight + CandidateRowHeight);
        }

        void OnClickSortColumn(CandidateSortColumn column)
        {
            if (column == CandidateSortColumn.None || candidates.Count <= 1)
                return;

            BoardState selectedBoard = null;
            int selectedSeed = 0;
            if (TryGetSelectedCandidate(out var selectedCandidate))
            {
                selectedBoard = selectedCandidate.Board;
                selectedSeed = selectedCandidate.Seed;
            }

            bool ascending = activeSortColumn == column
                ? !GetSortAscending(column)
                : GetSortAscending(column);
            SetSortAscending(column, ascending);
            activeSortColumn = column;

            SortCandidates(column, ascending);
            RestoreSelectedCandidate(selectedBoard, selectedSeed);
            EnsureSelectedCandidateVisible();
            Repaint();
        }

        void SortCandidates(CandidateSortColumn column, bool ascending)
        {
            candidates.Sort((a, b) =>
            {
                int result = CompareCandidates(a, b, column);
                if (!ascending)
                    result = -result;

                return result != 0 ? result : a.Seed.CompareTo(b.Seed);
            });
        }

        void RestoreSelectedCandidate(BoardState selectedBoard, int selectedSeed)
        {
            if (selectedBoard != null)
            {
                for (int i = 0; i < candidates.Count; i++)
                {
                    if (candidates[i].Board == selectedBoard && candidates[i].Seed == selectedSeed)
                    {
                        selectedIndex = i;
                        return;
                    }
                }
            }

            selectedIndex = candidates.Count > 0
                ? Mathf.Clamp(selectedIndex, 0, candidates.Count - 1)
                : -1;
        }

        bool GetSortAscending(CandidateSortColumn column)
        {
            return column switch
            {
                CandidateSortColumn.Difficulty => difficultySortAscending,
                CandidateSortColumn.Delta => deltaSortAscending,
                CandidateSortColumn.Seed => seedSortAscending,
                CandidateSortColumn.ArrowCount => arrowCountSortAscending,
                CandidateSortColumn.ArrowTileCount => arrowTileCountSortAscending,
                CandidateSortColumn.InitialMovable => initialMovableSortAscending,
                CandidateSortColumn.MaxChain => maxChainSortAscending,
                CandidateSortColumn.AverageSteps => averageStepsSortAscending,
                CandidateSortColumn.MinChoices => minChoicesSortAscending,
                CandidateSortColumn.BottleneckSteps => bottleneckSortAscending,
                _ => true,
            };
        }

        void SetSortAscending(CandidateSortColumn column, bool ascending)
        {
            switch (column)
            {
                case CandidateSortColumn.Difficulty:
                    difficultySortAscending = ascending;
                    break;
                case CandidateSortColumn.Delta:
                    deltaSortAscending = ascending;
                    break;
                case CandidateSortColumn.Seed:
                    seedSortAscending = ascending;
                    break;
                case CandidateSortColumn.ArrowCount:
                    arrowCountSortAscending = ascending;
                    break;
                case CandidateSortColumn.ArrowTileCount:
                    arrowTileCountSortAscending = ascending;
                    break;
                case CandidateSortColumn.InitialMovable:
                    initialMovableSortAscending = ascending;
                    break;
                case CandidateSortColumn.MaxChain:
                    maxChainSortAscending = ascending;
                    break;
                case CandidateSortColumn.AverageSteps:
                    averageStepsSortAscending = ascending;
                    break;
                case CandidateSortColumn.MinChoices:
                    minChoicesSortAscending = ascending;
                    break;
                case CandidateSortColumn.BottleneckSteps:
                    bottleneckSortAscending = ascending;
                    break;
            }
        }

        static int CompareCandidates(
            BoardGenerationTuning.DifficultyCandidate a,
            BoardGenerationTuning.DifficultyCandidate b,
            CandidateSortColumn column)
        {
            return column switch
            {
                CandidateSortColumn.Difficulty => a.Stats.DifficultyScore.CompareTo(b.Stats.DifficultyScore),
                CandidateSortColumn.Delta => a.DifficultyDelta.CompareTo(b.DifficultyDelta),
                CandidateSortColumn.Seed => a.Seed.CompareTo(b.Seed),
                CandidateSortColumn.ArrowCount => a.Stats.ArrowCount.CompareTo(b.Stats.ArrowCount),
                CandidateSortColumn.ArrowTileCount => a.Stats.ArrowTileCount.CompareTo(b.Stats.ArrowTileCount),
                CandidateSortColumn.InitialMovable => a.Stats.InitialMovableArrowChainCount.CompareTo(b.Stats.InitialMovableArrowChainCount),
                CandidateSortColumn.MaxChain => a.Stats.MaxChainLength.CompareTo(b.Stats.MaxChainLength),
                CandidateSortColumn.AverageSteps => a.Stats.AverageStepsToNextUnlock.CompareTo(b.Stats.AverageStepsToNextUnlock),
                CandidateSortColumn.MinChoices => a.Stats.Experience.MinChoices.CompareTo(b.Stats.Experience.MinChoices),
                CandidateSortColumn.BottleneckSteps => a.Stats.Experience.BottleneckStepCount.CompareTo(b.Stats.Experience.BottleneckStepCount),
                _ => 0,
            };
        }

        void DrawCandidateDetails()
        {
            using (new EditorGUILayout.VerticalScope())
            {
                EditorGUILayout.LabelField("预览", EditorStyles.boldLabel);

                if (!TryGetSelectedCandidate(out var candidate))
                {
                    EditorGUILayout.HelpBox("点击“生成候选”后，在左侧选择一个候选关卡。", MessageType.Info);
                    return;
                }

                detailScroll = EditorGUILayout.BeginScrollView(detailScroll);

                Rect previewRect = GUILayoutUtility.GetRect(360, 360, GUILayout.ExpandWidth(false));
                DrawBoardPreview(candidate.Board, controller, previewRect);

                EditorGUILayout.Space(8);
                EditorGUILayout.LabelField("Seed", candidate.Seed.ToString());
                EditorGUILayout.LabelField("难度", BoardGenerationTuning.FormatDifficultyScore(candidate.Stats.DifficultyScore));
                EditorGUILayout.LabelField("差值", candidate.DifficultyDelta.ToString());
                EditorGUILayout.LabelField("箭头数（链数）", candidate.Stats.ArrowCount.ToString());
                EditorGUILayout.LabelField("箭头格子数", candidate.Stats.ArrowTileCount.ToString());
                EditorGUILayout.LabelField("初始可动箭头", candidate.Stats.InitialMovableArrowChainCount.ToString());
                EditorGUILayout.LabelField("最长箭头格数", candidate.Stats.MaxChainLength.ToString());
                EditorGUILayout.LabelField("平均下次解锁步数", candidate.Stats.AverageStepsToNextUnlock.ToString("0.###"));

                EditorGUILayout.Space(8);
                EditorGUILayout.LabelField("体验统计", EditorStyles.boldLabel);
                EditorGUILayout.LabelField("最低可选链", candidate.Stats.Experience.MinChoices.ToString());
                EditorGUILayout.LabelField("平均可选链", candidate.Stats.Experience.AverageChoices.ToString("0.###"));
                EditorGUILayout.LabelField(
                    "瓶颈步数",
                    $"{candidate.Stats.Experience.BottleneckStepCount}/{candidate.Stats.Experience.SampledStepCount}");
                EditorGUILayout.LabelField(
                    "强制步比例",
                    $"{candidate.Stats.Experience.ForcedMoveStepCount}/{candidate.Stats.Experience.SampledStepCount} ({candidate.Stats.Experience.ForcedMoveRatio:P0})");
                EditorGUILayout.LabelField(
                    "解锁曲线",
                    BoardGenerationTuning.FormatChoiceWave(candidate.Stats.Experience.ChoiceWave));

                EditorGUILayout.Space(8);
                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("应用到场景"))
                        ApplySelectedToScene(candidate);

                    if (GUILayout.Button("保存为 LevelDefinition"))
                        SaveSelectedAsLevelDefinition(candidate);
                }

                EditorGUILayout.EndScrollView();
            }
        }

        void GenerateCandidates()
        {
            if (controller == null)
                return;

            LevelSpec baseSpec = controller.BuildGenerationSpec(controller.seed);
            var rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = controller.travelMode
            });

            bool cancelled = false;
            candidates.Clear();
            try
            {
                candidates.AddRange(BoardGenerationTuning.GenerateDifficultyCandidates(
                    baseSpec,
                    rules,
                    targetDifficultyScore,
                    candidateCount,
                    attempts,
                    controller.validateWithGreedy,
                    progressCallback: (current, total) =>
                    {
                        cancelled = EditorUtility.DisplayCancelableProgressBar(
                            "生成难度候选",
                            $"正在生成候选 ({current}/{total})",
                            total > 0 ? current / (float)total : 1f);
                        return !cancelled;
                    }));
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }

            if (activeSortColumn != CandidateSortColumn.None)
                SortCandidates(activeSortColumn, GetSortAscending(activeSortColumn));

            selectedIndex = candidates.Count > 0 ? 0 : -1;
            listScroll = Vector2.zero;
            generationStatusMessage = cancelled
                ? $"已取消生成，当前保留 {candidates.Count} 个候选。"
                : $"生成完成，共 {candidates.Count} 个候选。";
            generationStatusType = cancelled ? MessageType.Warning : MessageType.Info;
        }

        void ApplySelectedToScene(BoardGenerationTuning.DifficultyCandidate candidate)
        {
            if (controller == null || candidate.Board == null)
                return;

            Undo.RecordObject(controller, "应用难度候选关卡");
            controller.ApplyGeneratedCandidate(
                candidate.Board,
                candidate.Seed,
                candidate.Stats.DifficultyScore);
            EditorUtility.SetDirty(controller);
            EditorSceneManager.MarkSceneDirty(controller.gameObject.scene);
        }

        void SaveSelectedAsLevelDefinition(BoardGenerationTuning.DifficultyCandidate candidate)
        {
            if (controller == null || candidate.Board == null)
                return;

            string defaultName =
                $"Level_{controller.width}x{controller.height}_seed{candidate.Seed}_score{candidate.Stats.DifficultyScore}.asset";
            string path = EditorUtility.SaveFilePanelInProject(
                "保存候选 LevelDefinition",
                defaultName,
                "asset",
                "选择保存候选关卡的位置。");

            if (string.IsNullOrEmpty(path))
                return;

            var definition = CreateInstance<LevelDefinition>();
            FillDefinition(definition, candidate);

            AssetDatabase.CreateAsset(definition, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Selection.activeObject = definition;
        }

        void FillDefinition(LevelDefinition definition, BoardGenerationTuning.DifficultyCandidate candidate)
        {
            definition.levelId = $"level_{controller.width}x{controller.height}_seed_{candidate.Seed}";
            definition.source = LevelDefinition.LevelSource.Procedural;
            definition.board.width = controller.width;
            definition.board.height = controller.height;
            definition.board.seed = controller.seed;

            definition.generation.arrowCoverage = controller.arrowCoverage;
            definition.generation.initialMovableArrowCount = controller.initialMovableArrowCount;
            definition.generation.targetDifficultyScore = candidate.Stats.DifficultyScore;
            definition.generation.fixedGenerationSeed = candidate.Seed;
            definition.generation.minPathLen = controller.ResolvedMinArrowCellCount;
            definition.generation.maxPathLength = Mathf.Max(2, controller.maxPathLength);
            definition.generation.twistiness = controller.arrowTwistiness;
            definition.generation.validateWithGreedy = controller.validateWithGreedy;

            definition.lose.blockedLoseLimit = controller.blockedLoseLimit;
            definition.arrowColorMode = controller.ArrowColor;
            definition.arrowColorMaskQuantizeSteps = controller.ArrowColorMaskQuantizeSteps;

            var controllerSO = new SerializedObject(controller);
            var spawnMask = controllerSO.FindProperty("spawnMask");
            if (spawnMask != null)
                definition.masking.spawnMask = spawnMask.objectReferenceValue as Texture2D;
        }

        bool TryGetSelectedCandidate(out BoardGenerationTuning.DifficultyCandidate candidate)
        {
            if (selectedIndex >= 0 && selectedIndex < candidates.Count)
            {
                candidate = candidates[selectedIndex];
                return true;
            }

            candidate = default;
            return false;
        }

        void SyncTargetFromController()
        {
            if (controller == null)
                return;

            int resolved = controller.ResolvedDifficultyScoreTarget;
            targetDifficultyScore = resolved > 0
                ? resolved
                : controller.GetDifficultyScoreRecommendation().Preferred;
        }

        internal static void DrawBoardPreview(BoardState board, BoardController controller, Rect rect)
        {
            if (board == null)
                return;

            Dictionary<int, Color> previewColors = BuildPreviewColors(board, controller);

            GUI.Box(rect, GUIContent.none);
            float cellSize = Mathf.Min(rect.width / board.width, rect.height / board.height);
            float boardWidth = cellSize * board.width;
            float boardHeight = cellSize * board.height;
            float offsetX = rect.x + (rect.width - boardWidth) * 0.5f;
            float offsetY = rect.y + (rect.height - boardHeight) * 0.5f;

            for (int y = 0; y < board.height; y++)
            for (int x = 0; x < board.width; x++)
            {
                Rect cell = CellRect(board, x, y, offsetX, offsetY, cellSize);

                TileState tile = board.Get(x, y);
                Color color = tile.type switch
                {
                    TileType.Arrow => new Color(0.08f, 0.08f, 0.1f),
                    TileType.Block => new Color(0.35f, 0.35f, 0.35f),
                    _ => new Color(0.12f, 0.12f, 0.14f),
                };

                EditorGUI.DrawRect(cell, color);
            }

            DrawChainPreviewOverlays(board, previewColors, offsetX, offsetY, cellSize);
        }

        static Rect CellRect(BoardState board, int x, int y, float offsetX, float offsetY, float cellSize)
        {
            int drawY = board.height - 1 - y;
            return new Rect(
                offsetX + x * cellSize,
                offsetY + drawY * cellSize,
                cellSize - 1,
                cellSize - 1);
        }

        static Vector3 CellCenter(BoardState board, int x, int y, float offsetX, float offsetY, float cellSize)
        {
            Rect rect = CellRect(board, x, y, offsetX, offsetY, cellSize);
            return new Vector3(rect.center.x, rect.center.y, 0f);
        }

        static void DrawChainPreviewOverlays(
            BoardState board,
            Dictionary<int, Color> previewColors,
            float offsetX,
            float offsetY,
            float cellSize)
        {
            var visited = new HashSet<int>();
            var chain = new HashSet<int>();

            Handles.BeginGUI();
            for (int i = 0; i < board.tiles.Length; i++)
            {
                if (visited.Contains(i)) continue;
                if (board.tiles[i].type != TileType.Arrow) continue;

                int x = i % board.width;
                int y = i / board.width;

                chain.Clear();
                ArrowChainUtility.CollectFullChain(board, new Vector2Int(x, y), 0, chain);
                if (chain.Count == 0) continue;

                foreach (int idx in chain)
                    visited.Add(idx);

                if (!ArrowChainUtility.TryBuildOrderedChain(
                        board,
                        chain,
                        new Vector2Int(x, y),
                        out var ordered,
                        out var headPos,
                        out var headOutDir,
                        out _))
                    continue;

                Color color = previewColors.TryGetValue(i, out var c) ? c : Color.white;
                DrawChainPreviewPath(board, ordered, headPos, headOutDir, color, offsetX, offsetY, cellSize);
            }
            Handles.EndGUI();
        }

        static void DrawChainPreviewPath(
            BoardState board,
            List<Vector2Int> ordered,
            Vector2Int headPos,
            Dir headOutDir,
            Color color,
            float offsetX,
            float offsetY,
            float cellSize)
        {
            if (ordered == null || ordered.Count == 0)
                return;

            float lineWidth = Mathf.Clamp(cellSize * 0.38f, 5f, 16f);
            float tailLength = cellSize * 0.42f;
            var points = new List<Vector3>(ordered.Count + 1);

            if (ordered.Count == 1)
            {
                Vector3 center = CellCenter(board, ordered[0].x, ordered[0].y, offsetX, offsetY, cellSize);
                Vector3 dir = GuiDirVector(headOutDir);
                points.Add(center - dir * tailLength);
                points.Add(center);
            }
            else
            {
                for (int i = 0; i < ordered.Count; i++)
                {
                    Vector2Int p = ordered[i];
                    points.Add(CellCenter(board, p.x, p.y, offsetX, offsetY, cellSize));
                }
            }

            Handles.color = new Color(0f, 0f, 0f, 0.28f);
            Handles.DrawAAPolyLine(lineWidth + 2f, points.ToArray());
            Handles.color = color;
            Handles.DrawAAPolyLine(lineWidth, points.ToArray());

            Vector3 headCenter = CellCenter(board, headPos.x, headPos.y, offsetX, offsetY, cellSize);
            DrawPreviewArrowHead(headCenter, headOutDir, color, cellSize);
        }

        static Vector3 GuiDirVector(Dir dir)
        {
            return dir switch
            {
                Dir.Up => new Vector3(0f, -1f, 0f),
                Dir.Right => new Vector3(1f, 0f, 0f),
                Dir.Down => new Vector3(0f, 1f, 0f),
                _ => new Vector3(-1f, 0f, 0f),
            };
        }

        static void DrawPreviewArrowHead(Vector3 center, Dir dir, Color color, float cellSize)
        {
            Vector3 forward = GuiDirVector(dir);
            Vector3 side = new Vector3(-forward.y, forward.x, 0f);
            float length = Mathf.Clamp(cellSize * 0.36f, 5f, 15f);
            float halfWidth = length * 0.55f;

            Vector3 tip = center + forward * length * 0.55f;
            Vector3 baseCenter = center - forward * length * 0.35f;

            Handles.color = new Color(0f, 0f, 0f, 0.32f);
            Handles.DrawAAConvexPolygon(
                tip + forward,
                baseCenter + side * (halfWidth + 1f),
                baseCenter - side * (halfWidth + 1f));

            Handles.color = color;
            Handles.DrawAAConvexPolygon(
                tip,
                baseCenter + side * halfWidth,
                baseCenter - side * halfWidth);
        }

        static Dictionary<int, Color> BuildPreviewColors(BoardState board, BoardController controller)
        {
            var colors = new Dictionary<int, Color>(board.tiles.Length);
            Color[] palette = ResolvePreviewPalette(controller);
            int paletteIndex = 0;

            var visited = new HashSet<int>();
            var chain = new HashSet<int>();

            for (int i = 0; i < board.tiles.Length; i++)
            {
                if (visited.Contains(i)) continue;
                if (board.tiles[i].type != TileType.Arrow) continue;

                int x = i % board.width;
                int y = i / board.width;

                chain.Clear();
                ArrowChainUtility.CollectFullChain(board, new Vector2Int(x, y), 0, chain);
                if (chain.Count == 0) continue;

                foreach (int idx in chain)
                    visited.Add(idx);

                Color color = palette != null && palette.Length > 0
                    ? palette[paletteIndex % palette.Length]
                    : Color.white;
                paletteIndex++;

                foreach (int idx in chain)
                    colors[idx] = color;
            }

            ApplyPreviewMaskOverrides(board, controller, colors);
            return colors;
        }

        static Color[] ResolvePreviewPalette(BoardController controller)
        {
            if (controller == null)
                return null;

            var colorService = controller.GetComponent<BoardColorService2D>();
            Palette palette = colorService != null ? colorService.CurrentPalette : null;
            return palette != null && palette.Count > 0 ? palette.AsArray() : null;
        }

        static void ApplyPreviewMaskOverrides(BoardState board, BoardController controller, Dictionary<int, Color> colors)
        {
            if (controller == null || controller.ArrowColor != BoardController.ArrowColorMode.UseSpawnMask)
                return;

            Texture2D mask = controller.ArrowColorMask;
            if (mask == null)
                return;

            Color32[] pixels = mask.GetPixels32();
            int textureWidth = mask.width;
            int textureHeight = mask.height;
            int quantizeSteps = Mathf.Clamp(controller.ArrowColorMaskQuantizeSteps, 1, 64);

            for (int y = 0; y < board.height; y++)
            for (int x = 0; x < board.width; x++)
            {
                int idx = board.Index(x, y);
                if (board.tiles[idx].type != TileType.Arrow)
                    continue;

                int px = Mathf.Clamp(x, 0, textureWidth - 1);
                int py = Mathf.Clamp(y, 0, textureHeight - 1);
                Color32 color = pixels[px + py * textureWidth];
                float alpha = color.a / 255f;
                if (alpha <= controller.ArrowColorMaskAlphaThreshold)
                    continue;

                float r = Quantize01(color.r / 255f, quantizeSteps);
                float g = Quantize01(color.g / 255f, quantizeSteps);
                float b = Quantize01(color.b / 255f, quantizeSteps);
                colors[idx] = new Color(r, g, b, 1f);
            }
        }

        static float Quantize01(float value, int steps)
        {
            if (steps <= 1)
                return Mathf.Clamp01(value);

            float scale = steps - 1;
            return Mathf.Round(Mathf.Clamp01(value) * scale) / scale;
        }

    }
}
#endif
