using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelBug.ArrowMagic
{
    public sealed class BoardController : MonoBehaviour
    {
        public const int InitialMovableArrowSearchAttempts = 500;
        public const int DefaultMinArrowCellCount = 2;

        public enum GenerationMode
        {
            Filtered,
            FastPreview
        }

        public readonly struct GenerationSearchSettings
        {
            public readonly int AttemptCount;
            public readonly bool ValidateWithGreedy;
            public readonly bool ScoreCandidates;

            public GenerationSearchSettings(int attemptCount, bool validateWithGreedy, bool scoreCandidates)
            {
                AttemptCount = attemptCount;
                ValidateWithGreedy = validateWithGreedy;
                ScoreCandidates = scoreCandidates;
            }
        }

        [Header("棋盘")]
        [InspectorName("宽度")]
        public int width = 8;
        [InspectorName("高度")]
        public int height = 8;
        [InspectorName("随机种子")]
        public int seed = 12345;
        
        [HideInInspector] public SignalTravelMode travelMode = SignalTravelMode.ThroughEmpty;

        [Header("生成")]
        [InspectorName("箭头覆盖率")]
        [Range(0.05f, 1.0f)] public float arrowCoverage = 0.95f;
        [Tooltip("0 表示根据棋盘格子数量、覆盖率和路径长度自动使用推荐值。大于 0 时会尽量筛选到这个开局可移动箭头链数量。")]
        [InspectorName("初始可移动箭头数量")]
        [Min(0)] public int initialMovableArrowCount = 0;
        [HideInInspector] public int targetDifficultyScore = 0;
        [HideInInspector] public int fixedGenerationSeed = 0;
        [Tooltip("生成时允许的单条箭头链最短格子数量。")]
        [InspectorName("最短箭头格子数量")]
        [Min(DefaultMinArrowCellCount)] public int minPathLen = DefaultMinArrowCellCount;
        [Tooltip("生成时允许的单条箭头链最大格子数。")]
        [InspectorName("最大链长")]
        [Min(2)] public int maxPathLength = 20;
        [InspectorName("箭头曲折度")]
        [Range(0f, 1f)] public float arrowTwistiness = 0.50f;
        [HideInInspector] public bool validateWithGreedy = true;
        [SerializeField] bool fastPreviewOnAwake;

        [Header("失败条件")]
        [InspectorName("阻塞失败上限")]
        [Min(1)] public int blockedLoseLimit = 3;
        const bool pauseOnLose = true;

        [Header("棋盘遮罩")]
        [InspectorName("生成遮罩")]
        [SerializeField] Texture2D spawnMask;
        const float alphaThreshold = 0.5f;
        const bool useAlphaOnly = true;
        const float luminanceThreshold = 0.5f;
        
        public enum ArrowColorMode
        {
            [InspectorName("使用调色板")]
            UsePalette = 0,
            [InspectorName("使用生成遮罩")]
            UseSpawnMask = 1,
        }

        [Header("箭头颜色（可选）")]
        [InspectorName("箭头颜色模式")]
        [SerializeField] ArrowColorMode arrowColorMode = ArrowColorMode.UsePalette;

        const float arrowColorMaskAlphaThreshold = 0.5f;

        [Tooltip("Quantizes the mask colors before applying (1 = no quantize, 8/16/32 makes palettes cleaner).")]
        [InspectorName("箭头颜色遮罩量化级别")]
        [SerializeField, Range(1, 64)]
        int arrowColorMaskQuantizeSteps = 16;

        public ArrowColorMode ArrowColor => arrowColorMode;
        public Texture2D ArrowColorMask => spawnMask;
        public float ArrowColorMaskAlphaThreshold => arrowColorMaskAlphaThreshold;
        public int ArrowColorMaskQuantizeSteps => arrowColorMaskQuantizeSteps;
        public int ResolvedMinArrowCellCount => Mathf.Max(DefaultMinArrowCellCount, minPathLen);

        [Header("编辑模式（种子 + 覆盖）")]
        [Tooltip("Optional: assign an EditedLevelData asset. If null, an in-memory instance is created when you enter editor mode.")]
        [InspectorName("编辑关卡数据")]
        EditedLevelData editedLevel;

        public bool EditorMode => _editorMode;
        bool _editorMode;

        public EditedLevelData EditedLevel => editedLevel;
        [SerializeField, HideInInspector] LevelDefinition.LevelSource _levelSource = LevelDefinition.LevelSource.Procedural;
        [SerializeField, HideInInspector] AuthoredLevelData _authoredLevel;

        public bool[] CanSpawnHere { get; private set; }   // length = width*height

        public bool CanShowDot(int idx) =>
            CanSpawnHere == null || (idx >= 0 && idx < CanSpawnHere.Length && CanSpawnHere[idx]);

        public bool CanSpawnAt(int x, int y)
        {
            int idx = x + y * width;
            return CanSpawnHere == null || (idx >= 0 && idx < CanSpawnHere.Length && CanSpawnHere[idx]);
        }

        bool[] BuildCanSpawnMask(Texture2D tex, int w, int h)
        {
            var a = new bool[w * h];

            // No mask assigned -> everything allowed
            if (tex == null)
            {
                for (int i = 0; i < a.Length; i++) a[i] = true;
                return a;
            }

            var pix = tex.GetPixels32();
            int tw = tex.width;
            int th = tex.height;

            for (int y = 0; y < h; y++)
            for (int x = 0; x < w; x++)
            {
                int idx = x + y * w;

                int px = Mathf.Clamp(x, 0, tw - 1);
                int py = Mathf.Clamp(y, 0, th - 1);

                var c = pix[px + py * tw];
                float af = c.a / 255f;
                
                a[idx] = af > alphaThreshold;
            }

            return a;
        }

        public BoardGenerationTuning.InitialMovableArrowRecommendation GetInitialMovableArrowRecommendation()
        {
            bool[] mask = CanSpawnHere;
            int expectedLength = Mathf.Max(0, width) * Mathf.Max(0, height);

            if (expectedLength > 0 && (mask == null || mask.Length != expectedLength))
            {
                try
                {
                    mask = BuildCanSpawnMask(spawnMask, width, height);
                }
                catch (System.Exception)
                {
                    mask = null;
                }
            }

            return BoardGenerationTuning.RecommendInitialMovableArrowCount(
                width,
                height,
                arrowCoverage,
                ResolvedMinArrowCellCount,
                Mathf.Max(2, maxPathLength),
                mask);
        }

        public BoardGenerationTuning.DifficultyScoreRecommendation GetDifficultyScoreRecommendation()
        {
            bool[] mask = CanSpawnHere;
            int expectedLength = Mathf.Max(0, width) * Mathf.Max(0, height);

            if (expectedLength > 0 && (mask == null || mask.Length != expectedLength))
            {
                try
                {
                    mask = BuildCanSpawnMask(spawnMask, width, height);
                }
                catch (System.Exception)
                {
                    mask = null;
                }
            }

            return BoardGenerationTuning.RecommendDifficultyScore(
                width,
                height,
                arrowCoverage,
                ResolvedMinArrowCellCount,
                Mathf.Max(2, maxPathLength),
                mask);
        }

        public static GenerationSearchSettings ResolveGenerationSearchSettings(
            GenerationMode mode,
            bool validateWithGreedy,
            int targetInitialMovable,
            int targetDifficulty)
        {
            if (mode == GenerationMode.FastPreview)
                return new GenerationSearchSettings(1, false, false);

            bool scoreCandidates = targetInitialMovable > 0 || targetDifficulty > 0;
            int attemptCount = (validateWithGreedy || scoreCandidates)
                ? InitialMovableArrowSearchAttempts
                : 1;

            return new GenerationSearchSettings(attemptCount, validateWithGreedy, scoreCandidates);
        }

        public static GenerationMode ResolveAwakeGenerationMode(bool fastPreviewOnAwake, string sceneName = null)
        {
            if (string.Equals(sceneName, "Create", System.StringComparison.Ordinal))
                return GenerationMode.FastPreview;

            return fastPreviewOnAwake ? GenerationMode.FastPreview : GenerationMode.Filtered;
        }

        public LevelSpec BuildGenerationSpec(int generatedSeed)
        {
            int expectedLength = Mathf.Max(0, width) * Mathf.Max(0, height);
            if (expectedLength > 0 && (CanSpawnHere == null || CanSpawnHere.Length != expectedLength))
                CanSpawnHere = BuildCanSpawnMask(spawnMask, width, height);

            return new LevelSpec
            {
                width = width,
                height = height,
                seed = generatedSeed,
                arrowFill = arrowCoverage,
                minPathLen = ResolvedMinArrowCellCount,
                maxPathLen = Mathf.Max(2, maxPathLength),
                twistiness = arrowTwistiness,
                canSpawnHere = CanSpawnHere
            };
        }

        public BoardState State { get; private set; }
        public IRuleset Rules { get; private set; }
        public bool Won { get; private set; }
        public bool Lost { get; private set; }

        // HUD-friendly accessors
        public int MoveCount => _undo.Count;
        public int CurrentSeed => seed;
        public int ResolvedInitialMovableArrowCountTarget =>
            BoardGenerationTuning.ResolveInitialMovableArrowCountTarget(
                initialMovableArrowCount,
                GetInitialMovableArrowRecommendation());
        public int ResolvedDifficultyScoreTarget =>
            BoardGenerationTuning.ResolveDifficultyScoreTarget(
                targetDifficultyScore,
                GetDifficultyScoreRecommendation());

        public event System.Action<MoveDelta> OnDeltaApplied;
        public event System.Action OnWin;
        public event System.Action OnLose;
        public event System.Action OnRestart;
        public event System.Action OnRedraw;

        public event System.Action<bool> OnEditorModeChanged;

        readonly Stack<MoveDelta> _undo = new();

        public int BlockedUniqueCount => _blockedUnique.Count;
        public event System.Action OnBlockedChanged;

        readonly HashSet<ulong> _blockedUnique = new();

        void Awake()
        {
            Rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = travelMode
            });
            
            if (FindFirstObjectByType<LevelProgression>() != null)
                return;

            if (ResolveAwakeGenerationMode(fastPreviewOnAwake, SceneManager.GetActiveScene().name) == GenerationMode.FastPreview)
                RestartFastPreview(seed);
            else
                Restart(seed);
        }

        void ResolveBoardDimensionsFromMask()
        {
            if (spawnMask != null)
            {
                width = spawnMask.width;
                height = spawnMask.height;
            }
        }

        public void SetEditorMode(bool enabled)
        {
            if (_editorMode == enabled)
                return;

            _editorMode = enabled;

            if (_editorMode)
            {
                EnsureEditedLevelInitialized(clearOverrides: true);
            }

            OnEditorModeChanged?.Invoke(_editorMode);
        }

        public BoardGenerationTuning.BoardGenerationStats GetCurrentGenerationStats()
        {
            return BoardGenerationTuning.CalculateBoardGenerationStats(State, Rules);
        }

        public void ApplyGeneratedCandidate(BoardState board, int generatedSeed, int difficultyScore)
        {
            if (board == null)
                return;

            _levelSource = LevelDefinition.LevelSource.Procedural;
            _authoredLevel = null;
            Won = false;
            Lost = false;
            _undo.Clear();
            _blockedUnique.Clear();
            OnBlockedChanged?.Invoke();

            fixedGenerationSeed = generatedSeed;
            targetDifficultyScore = Mathf.Max(0, difficultyScore);
            State = CloneBoard(board);
            OnRestart?.Invoke();
        }

        public void PrintCurrentGenerationStats()
        {
            if (State == null)
            {
                Debug.LogWarning("[BoardController] 当前没有可统计的棋盘，请先生成关卡。");
                return;
            }

            var stats = GetCurrentGenerationStats();
            Debug.Log(
                $"[BoardController] 关卡统计：箭头数（链数）={stats.ArrowCount}，" +
                $"箭头格子数={stats.ArrowTileCount}，" +
                $"初始可移动箭头数={stats.InitialMovableArrowChainCount}，" +
                $"最长箭头格数={stats.MaxChainLength}，" +
                $"平均下次解锁步数={stats.AverageStepsToNextUnlock:0.###}，" +
                $"难度分值={BoardGenerationTuning.FormatDifficultyScore(stats.DifficultyScore)}，Seed={seed}，覆盖率={arrowCoverage:0.###}，" +
                $"推荐初始可移动箭头数={ResolvedInitialMovableArrowCountTarget}。");
        }
        
        public void SaveCurrentLevelToLevelIO()
        {
            string levelId =
                (editedLevel != null && !string.IsNullOrWhiteSpace(editedLevel.name))
                    ? editedLevel.name
                    : $"level_{width}x{height}_seed_{seed}";

            var data = CaptureLevelSaveData(levelId);
            LevelIO.Save(data);
        }

        void EnsureEditedLevelInitialized(bool clearOverrides)
        {
            if (editedLevel == null)
            {
                // In-memory instance (won't persist unless you later save it).
                editedLevel = ScriptableObject.CreateInstance<EditedLevelData>();
            }

            // Always re-sync base spec with current procedural settings.
            editedLevel.baseSpec.seed = seed;
            editedLevel.baseSpec.width = width;
            editedLevel.baseSpec.height = height;

            if (clearOverrides)
            {
                editedLevel.clearedTiles.Clear();
                editedLevel.tileOverrides.Clear();
            }
        }

        void ApplyEditedState()
        {
            EnsureEditedLevelInitialized(clearOverrides: false);

            // Editing changes the board; reset gameplay flags/undo/blocked counts.
            Won = false;
            Lost = false;
            _undo.Clear();
            _blockedUnique.Clear();
            OnBlockedChanged?.Invoke();

            State = editedLevel.BuildBoardState(
                arrowFill: arrowCoverage,
                minPathLen: ResolvedMinArrowCellCount,
                maxPathLen: Mathf.Max(2, maxPathLength),
                twistiness: arrowTwistiness,
                canSpawnHere: CanSpawnHere);

            // We don't want to "restart" intros; just redraw.
            OnRedraw?.Invoke();
        }

        /// <summary>
        /// Editor-mode tap behavior:
        /// If you tap an arrow, it disappears (records a clear override).
        /// </summary>
        public void EditorTapCell(Vector2Int cell)
        {
            if (!_editorMode) return;
            if (State == null || !State.InBounds(cell.x, cell.y)) return;

            int startIdx = State.Index(cell.x, cell.y);
            if (State.tiles[startIdx].type != TileType.Arrow)
                return;

            EnsureEditedLevelInitialized(clearOverrides: false);

            // 1) Collect the full connected chain (both forward + backward from tapped tile)
            var chainSet = new HashSet<int>();
            ArrowChainUtility.CollectFullChain(
                State,
                cell,
                // travelMode,  // use current controller mode (your utility keeps it for API compat)
                0,           // maxStepsOverride: 0 => use autoMax (no override)
                chainSet
            );

            // 2) Clear every tile in that chain
            foreach (int idx in chainSet)
            {
                int x = idx % State.width;
                int y = idx / State.width;
                editedLevel.DeleteAt(new Vector2Int(x, y));
            }

            ApplyEditedState();
        }

        /// <summary>
        /// Editor-mode drag behavior:
        /// Dragging dot-to-dot writes a new arrow path.
        /// </summary>
        public void EditorApplyStroke(List<Vector2Int> cells)
        {
            if (!_editorMode) return;
            if (cells == null || cells.Count < 2) return;

            EnsureEditedLevelInitialized(clearOverrides: false);
            editedLevel.ApplyStroke(cells, clearTouched: true, clearFinalCell: false);
            ApplyEditedState();
        }

        public void Restart(int newSeed)
        {
            RestartInternal(newSeed, GenerationMode.Filtered);
        }

        public void RestartFastPreview(int newSeed)
        {
            RestartInternal(newSeed, GenerationMode.FastPreview);
        }

        void RestartInternal(int newSeed, GenerationMode generationMode)
        {
            seed = newSeed;
            Won = false;
            Lost = false;
            _undo.Clear();
            _blockedUnique.Clear();
            OnBlockedChanged?.Invoke();

            if (_levelSource == LevelDefinition.LevelSource.Authored && _authoredLevel != null)
            {
                width = Mathf.Max(1, _authoredLevel.width);
                height = Mathf.Max(1, _authoredLevel.height);
                CanSpawnHere = BuildCanSpawnMask(null, width, height);
            }
            else
            {
                ResolveBoardDimensionsFromMask();
                CanSpawnHere = BuildCanSpawnMask(spawnMask, width, height);
            }

            // If we're editing, we want a clean diff against the new base.
            if (_editorMode)
                EnsureEditedLevelInitialized(clearOverrides: true);

            Rules = new ArrowMagicRuleset(new ArrowMagicRulesetConfig
            {
                travelMode = travelMode
            });

            if (_levelSource == LevelDefinition.LevelSource.Authored)
            {
                if (AuthoredLevelBuilder.TryBuildBoard(_authoredLevel, out var authoredState, out string error))
                {
                    State = authoredState;
                }
                else
                {
                    State = new BoardState(Mathf.Max(1, width), Mathf.Max(1, height));
                    for (int i = 0; i < State.tiles.Length; i++)
                        State.tiles[i] = TileState.Empty();
                    Debug.LogError($"[BoardController] 手工关卡生成失败：{error}");
                }

                OnRestart?.Invoke();
                return;
            }

            var gen = new ClearAllArrowsGenerator();
            LevelSpec BuildSpec(int attemptSeed) => BuildGenerationSpec(attemptSeed);

            if (fixedGenerationSeed != 0)
            {
                State = gen.Generate(BuildSpec(fixedGenerationSeed));
                OnRestart?.Invoke();
                return;
            }

            int targetInitialMovable = 0;
            int targetDifficulty = 0;

            if (generationMode == GenerationMode.Filtered)
            {
                var recommendation = GetInitialMovableArrowRecommendation();
                targetInitialMovable = BoardGenerationTuning.ResolveInitialMovableArrowCountTarget(
                    initialMovableArrowCount,
                    recommendation);
                var difficultyRecommendation = GetDifficultyScoreRecommendation();
                targetDifficulty = BoardGenerationTuning.ResolveDifficultyScoreTarget(
                    targetDifficultyScore,
                    difficultyRecommendation);
            }

            BoardState bestCandidate = null;
            int bestDiff = int.MaxValue;
            int bestMovableCount = 0;
            int bestDifficultyScore = 0;
            var searchSettings = ResolveGenerationSearchSettings(
                generationMode,
                validateWithGreedy,
                targetInitialMovable,
                targetDifficulty);

            for (int attempt = 0; attempt < searchSettings.AttemptCount; attempt++)
            {
                var candidate = gen.Generate(BuildSpec(BoardGenerationTuning.BuildCandidateSeed(seed, attempt)));
                if (searchSettings.ValidateWithGreedy && !GreedyValidator.TryClearAllByGreedy(candidate, Rules, 300, out _))
                    continue;

                if (!searchSettings.ScoreCandidates)
                {
                    State = candidate;
                    OnRestart?.Invoke();
                    return;
                }

                var stats = BoardGenerationTuning.CalculateBoardGenerationStats(candidate, Rules);
                int movableCount = stats.InitialMovableArrowChainCount;
                int diff = targetDifficulty > 0
                    ? Mathf.Abs(stats.DifficultyScore - targetDifficulty)
                    : Mathf.Abs(movableCount - targetInitialMovable);

                if (diff < bestDiff)
                {
                    bestCandidate = candidate;
                    bestDiff = diff;
                    bestMovableCount = movableCount;
                    bestDifficultyScore = stats.DifficultyScore;
                }

                if (diff == 0)
                {
                    State = candidate;
                    OnRestart?.Invoke();
                    return;
                }
            }

            if (bestCandidate != null)
            {
                if (targetDifficulty > 0 && bestDiff > 0)
                {
                    Debug.LogWarning(
                        $"[BoardController] 难度分值未精确匹配：目标={BoardGenerationTuning.FormatDifficultyScore(targetDifficulty)}，实际={BoardGenerationTuning.FormatDifficultyScore(bestDifficultyScore)}，已使用最接近的候选。");
                }
                else if (initialMovableArrowCount > 0 && bestDiff > 0)
                {
                    Debug.LogWarning(
                        $"[BoardController] 初始可移动箭头数量未精确匹配：目标={targetInitialMovable}，实际={bestMovableCount}，已使用最接近的候选。");
                }

                State = bestCandidate;
                OnRestart?.Invoke();
                return;
            }

            State = gen.Generate(BuildSpec(BoardGenerationTuning.BuildCandidateSeed(seed, 0)));

            OnRestart?.Invoke();
        }

        public void RegisterBlockedChainKey(ulong chainKey)
        {
            if (_blockedUnique.Add(chainKey))
            {
                OnBlockedChanged?.Invoke();
                TryLoseFromBlocked();
            }
        }

        public void NewGame(int? newSeed = null)
        {
            if (newSeed.HasValue) Restart(newSeed.Value);
            else Restart(seed + 1);
        }

        public void ClickCell(int x, int y)
        {
            if (_editorMode) return; // ignore gameplay clicks while editing
            if (Won || Lost) return;

            var move = new Move(new Vector2Int(x, y));

            if (!Rules.TryApplyMove(State, move, out var delta))
                return;

            _undo.Push(delta);
            OnDeltaApplied?.Invoke(delta);

            if (!Won && Rules.IsSolved(State))
            {
                Won = true;
                Debug.Log($"WIN! Moves={_undo.Count} Seed={seed}");
                OnWin?.Invoke();
            }
        }

        public void Undo()
        {
            if (_editorMode) return; // optional: you can later add edit-undo separately
            if (Won || Lost) return;
            if (_undo.Count == 0) return;

            var d = _undo.Pop();
            d.Undo(State);

            OnRedraw?.Invoke();
        }

        void TryLoseFromBlocked()
        {
            if (Won || Lost) return;

            if (BlockedUniqueCount >= blockedLoseLimit)
            {
                Lost = true;
                
                Debug.Log($"LOSE! Blocked={BlockedUniqueCount} Limit={blockedLoseLimit} Seed={seed}");

                OnLose?.Invoke();
                GameEvents.RaiseLevelLost();
            }
        }

        // (rest of your file unchanged)
        public void ApplyLevelDefinition(LevelDefinition def)
        {
            if (def == null) return;

            _levelSource = def.source;
            _authoredLevel = def.source == LevelDefinition.LevelSource.Authored
                ? CloneAuthoredLevel(def.authoredLevel)
                : null;

            width  = _levelSource == LevelDefinition.LevelSource.Authored && def.authoredLevel != null
                ? Mathf.Max(1, def.authoredLevel.width)
                : def.board.width;
            height = _levelSource == LevelDefinition.LevelSource.Authored && def.authoredLevel != null
                ? Mathf.Max(1, def.authoredLevel.height)
                : def.board.height;
            seed   = def.board.seed;

            arrowCoverage      = def.generation.arrowCoverage;
            initialMovableArrowCount = Mathf.Max(0, def.generation.initialMovableArrowCount);
            targetDifficultyScore = Mathf.Max(0, def.generation.targetDifficultyScore);
            fixedGenerationSeed = def.generation.fixedGenerationSeed;
            minPathLen         = Mathf.Max(DefaultMinArrowCellCount, def.generation.minPathLen);
            maxPathLength      = Mathf.Max(2, def.generation.maxPathLength);
            arrowTwistiness         = def.generation.twistiness;
            validateWithGreedy = def.generation.validateWithGreedy;
            
            arrowColorMode = def.arrowColorMode;
            arrowColorMaskQuantizeSteps = Mathf.Max(1, def.arrowColorMaskQuantizeSteps);

            blockedLoseLimit = def.lose.blockedLoseLimit;

            spawnMask          = def.masking.spawnMask;

            Restart(seed);
        }

        public void ApplyLevelSaveData(LevelSaveData d, Texture2D resolvedMask)
        {
            if (d == null) return;

            _levelSource = LevelDefinition.LevelSource.Procedural;
            _authoredLevel = null;

            width  = d.width;
            height = d.height;
            seed   = d.seed;

            arrowCoverage      = d.arrowCoverage;
            initialMovableArrowCount = Mathf.Max(0, d.initialMovableArrowCount);
            targetDifficultyScore = Mathf.Max(0, d.targetDifficultyScore);
            fixedGenerationSeed = d.fixedGenerationSeed;
            minPathLen         = Mathf.Max(DefaultMinArrowCellCount, d.minPathLen);
            maxPathLength      = Mathf.Max(2, d.maxPathLength);
            arrowTwistiness         = d.twistiness;
            validateWithGreedy = d.validateWithGreedy;

            blockedLoseLimit = d.blockedLoseLimit;
            
            arrowColorMode = d.arrowColorMode;
            arrowColorMaskQuantizeSteps = Mathf.Max(1, d.arrowColorMaskQuantizeSteps);

            spawnMask          = resolvedMask;

            Restart(seed);
        }

        public LevelSaveData CaptureLevelSaveData(string levelId)
        {
            var d = new LevelSaveData();
            d.levelId = levelId;

            d.width  = width;
            d.height = height;
            d.seed   = seed;

            d.arrowCoverage      = arrowCoverage;
            d.initialMovableArrowCount = Mathf.Max(0, initialMovableArrowCount);
            d.targetDifficultyScore = Mathf.Max(0, targetDifficultyScore);
            d.fixedGenerationSeed = fixedGenerationSeed;
            d.minPathLen         = ResolvedMinArrowCellCount;
            d.maxPathLength      = Mathf.Max(2, maxPathLength);
            d.twistiness         = arrowTwistiness;
            d.validateWithGreedy = validateWithGreedy;

            d.blockedLoseLimit = blockedLoseLimit;
            
            d.arrowColorMode = arrowColorMode;
            d.arrowColorMaskQuantizeSteps = arrowColorMaskQuantizeSteps;

            return d;
        }
        
        public void SaveEditedLevel()
        {
            if (!EditorMode)
            {
                Debug.LogWarning("[BoardController] SaveEditedLevel called, but editor mode is OFF.");
                return;
            }

            if (editedLevel == null)
            {
                Debug.LogWarning("[BoardController] No EditedLevelData assigned/created; nothing to save.");
                return;
            }

#if UNITY_EDITOR
            // If this is an in-memory instance (not an asset yet), offer to create an asset.
            string path = UnityEditor.AssetDatabase.GetAssetPath(editedLevel);
            if (string.IsNullOrEmpty(path))
            {
                path = UnityEditor.EditorUtility.SaveFilePanelInProject(
                    title: "Save Edited Level",
                    defaultName: "EditedLevelData.asset",
                    extension: "asset",
                    message: "Choose where to save this EditedLevelData asset.");

                if (string.IsNullOrEmpty(path))
                {
                    Debug.LogWarning("[BoardController] Save cancelled.");
                    return;
                }

                UnityEditor.AssetDatabase.CreateAsset(editedLevel, path);
            }

            UnityEditor.EditorUtility.SetDirty(editedLevel);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
            Debug.Log($"[BoardController] Saved EditedLevelData asset: {path}");
#else
    Debug.LogWarning("[BoardController] SaveEditedLevel is only supported in the Unity Editor (AssetDatabase not available in builds).");
#endif
        }

        static BoardState CloneBoard(BoardState source)
        {
            var clone = new BoardState(source.width, source.height);
            System.Array.Copy(source.tiles, clone.tiles, source.tiles.Length);
            return clone;
        }

        static AuthoredLevelData CloneAuthoredLevel(AuthoredLevelData source)
        {
            if (source == null)
                return null;

            var clone = new AuthoredLevelData
            {
                width = source.width,
                height = source.height,
                arrows = new List<AuthoredArrowData>(),
                blockIndices = source.blockIndices != null
                    ? new List<int>(source.blockIndices)
                    : new List<int>()
            };

            if (source.arrows == null)
                return clone;

            for (int i = 0; i < source.arrows.Count; i++)
            {
                var arrow = source.arrows[i];
                clone.arrows.Add(new AuthoredArrowData
                {
                    indices = arrow != null && arrow.indices != null
                        ? new List<int>(arrow.indices)
                        : new List<int>(),
                    colorIndex = arrow != null ? arrow.colorIndex : 0
                });
            }

            return clone;
        }

    }
}
