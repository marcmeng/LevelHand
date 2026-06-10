using UnityEngine;
using System.Collections.Generic;

namespace PixelBug.ArrowMagic
{
    public sealed class BoardView2D : MonoBehaviour
    {
        [SerializeField] BoardController controller;
        [SerializeField] TileView2D tilePrefab;
        [SerializeField] float cellSize = 1.0f;
        [SerializeField] Vector2 origin = Vector2.zero;
        private BoardColorService2D colorService;
        
        Camera cam;
        
        readonly BoardLayout2D _layout = new BoardLayout2D();

        // Dictionary<int, Color> _stableArrowColors;
        TileView2D[] _tiles;

        [Header("Arrow")]
        [SerializeField] float animationSpeed = 32f;
        int overlaySortingOrder = 50;
            
        // How many sub-steps per tile (also effectively "segments per tile").
        [Header("Arrow Tail")]
        //[SerializeField, Range(2, 12)] 
        int segmentsPerTile = 10;
        [SerializeField, Range(0.0f, 0.49f)] float cornerRadius = 0.2f;
        
        bool bumpOnHitArrow = true;
        float bumpDistanceCells = 0.18f;   // in “cellSize” units (0.15–0.25 feels good)
        float bumpDuration = 0.08f;        // seconds (0.06–0.12)
        
        float offscreenViewportMargin = 0.0f;
        
        private bool smoothOverlayPath = true;
        private bool renderBoardAsSnakes = true;
        private int boardSnakeSortingOrder = 20; // below selection overlay (50), above tiles
        
        readonly BoardSelectionAnimator2D _selection = new BoardSelectionAnimator2D();
        
        LevelIntroAnimator2D _intro;
        bool _introRunning;
        bool _snakeRebuildQueued;
        
        BoardDots2D _dots;
        BoardSnakeRenderer2D _snakes = new BoardSnakeRenderer2D();

        ArrowOverlayPool2D _arrowOverlays;
        public ArrowOverlayPool2D ArrowOverlayPool => _arrowOverlays;
        
        readonly BoardPathBuilder2D _path = new BoardPathBuilder2D();
        
        readonly Stack<TileView2D> _tilePool = new();
        readonly List<TileView2D> _activeTileList = new();
        
        // --- Editor draw preview ---
        ArrowSnakeOverlay2D _editorPreviewOverlay;
        readonly List<Vector2Int> _editorPreviewCells = new();
        readonly List<Vector3> _editorPreviewRaw = new();
        readonly List<Vector3> _editorPreviewPts = new();

        TileView2D GetTile()
        {
            if (tilePrefab == null)
                return null;

            TileView2D tv;
            if (_tilePool.Count > 0)
            {
                tv = _tilePool.Pop();
                if (tv != null)
                    tv.gameObject.SetActive(true);
            }
            else
            {
                tv = Instantiate(tilePrefab, transform);
            }

            if (tv != null)
            {
                tv.transform.SetParent(transform, false);
                _activeTileList.Add(tv);
            }

            return tv;
        }

        void ReleaseAllTilesToPool()
        {
            if (_activeTileList.Count == 0)
                return;

            for (int i = 0; i < _activeTileList.Count; i++)
            {
                var tv = _activeTileList[i];
                if (tv == null) continue;

                // Avoid duplicate subscriptions across rebuilds.
                tv.Clicked -= HandleTileClicked;

                tv.SetHidden(false);
                tv.SetDotSuppressed(false);

                tv.gameObject.SetActive(false);
                _tilePool.Push(tv);
            }

            _activeTileList.Clear();
        }
        
        void Awake()
        {
            if (cam == null) cam = Camera.main;
            if (colorService == null)
                colorService = GetComponent<BoardColorService2D>();

            if (colorService != null)
                colorService.Init(controller);
            
            _layout.BindRoot(transform);
            SyncLayoutFromSerialized();
            
            _dots = GetComponent<BoardDots2D>();
            if (_dots != null)
            {
                _dots.Configure(
                    controller: controller,
                    getTiles: () => _tiles,
                    redrawAll: RedrawAll,
                    raiseLevelWon: () => GameEvents.RaiseLevelWon()
                );
            }
            InitPathBuilder();

            // One pool for ALL arrow overlays (intro / board / ghost)
            _arrowOverlays = new ArrowOverlayPool2D(transform);
            _arrowOverlays.ConfigureSprites(GetOverlayStraightSprite(), GetHeadSprite());
            
            InitSnakes();
            InitSelection();
        }
        
        void Start()
        {
            if (controller != null && controller.State != null)
                Rebuild();
        }
        
        void TryFrameCamera(bool animate)
        {
            var framer = GetComponent<BoardCameraFramer2D>();
            if (framer == null)
                return;

            var camToUse = cam != null ? cam : Camera.main;
            framer.SetCamera(camToUse);
            framer.AllowFramingForLevel();
            framer.FrameToBoard(_layout, controller.State, animate);
        }
        
        void InitSelection()
        {
            var settings = new BoardSelectionAnimator2D.Settings
            {
                animationSpeed = animationSpeed,
                overlaySortingOrder = overlaySortingOrder,

                segmentsPerTile = Mathf.Max(2, segmentsPerTile),
                smoothOverlayPath = smoothOverlayPath,
                renderBoardAsSnakes = renderBoardAsSnakes,
                
                bumpOnHitArrow = bumpOnHitArrow,
                bumpDistanceCells = bumpDistanceCells,
                bumpDuration = bumpDuration,

                offscreenViewportMargin = offscreenViewportMargin,
                cellSize = cellSize
            };

            // Selection overlays come from the ONE shared pool.
            BoardSelectionAnimator2D.AcquireSelectionOverlayDelegate acquire =
                (sortingOrder) => _arrowOverlays.Acquire(sortingOrder);

            BoardSelectionAnimator2D.ReleaseSelectionOverlayDelegate release =
                (o) => _arrowOverlays.Release((ArrowSnakeOverlay2D)o);
            
            System.Func<int, Color> getTileColor = (idx) =>
                colorService != null ? colorService.GetTileColor(idx) : Color.white;

            System.Func<int, Color> getStableStartColor = (startIdx) =>
            {
                if (colorService != null && colorService.TryGetStableColor(startIdx, out var c))
                    return c;
                return Color.white;
            };
            
            System.Func<bool> getTintOnHit = () =>
                colorService != null ? colorService.TintOnHit : true;

            System.Func<Color> getHitTint = () =>
                colorService != null ? colorService.HitTint : Color.white;

            _selection.Init(
                host: this,
                controller: controller,
                layout: _layout,
                tilePrefab: tilePrefab,
                cam: cam != null ? cam : Camera.main,
                dots: _dots,
                snakes: _snakes,
                permanentTileColors: colorService != null ? colorService.PermanentTileColors : null,

                getIntroRunning: () => _introRunning,

                getStableStartColor: getStableStartColor,
                getTileColor: getTileColor,
                
                getLiveState: () => controller != null ? controller.State : null,
                redrawCellsFromLiveState: RedrawCellsFromLiveState,
                raiseArrowSelected: (worldPos) => GameEvents.RaiseArrowSelected(worldPos),

                getTintOnHit: getTintOnHit,
                getHitTint: getHitTint,
                
                acquireSelectionOverlay: acquire,
                releaseSelectionOverlay: release,
                settings: settings
            );
        }
        
        void InitSnakes()
        {
            var settings = new BoardSnakeRenderer2D.Settings
            {
                renderBoardAsSnakes = renderBoardAsSnakes,
                boardSnakeSortingOrder = boardSnakeSortingOrder,
                segmentsPerTile = segmentsPerTile
            };
            
            // Board overlays come from the ONE shared pool.
            BoardSnakeRenderer2D.AcquireOverlayDelegate acquire =
                (sortingOrder) => _arrowOverlays.Acquire(sortingOrder);

            BoardSnakeRenderer2D.ReleaseOverlayDelegate release =
                (o) => _arrowOverlays.Release((ArrowSnakeOverlay2D)o);
            
            System.Func<int, Color> getTileColor = (idx) =>
                colorService != null ? colorService.GetTileColor(idx) : Color.white;
            
            _snakes.Init(
                controller: controller,
                getTiles: () => _tiles,
                getTileColor: getTileColor,
                tilePrefab: tilePrefab,
                acquireOverlay: acquire,
                releaseOverlay: release,
                tryBuildOrderedChain: ArrowChainUtility.TryBuildOrderedChain,
                buildBoardWorldPoints: BuildBoardWorldPoints,
                settings: settings);
        }
        
        void InitPathBuilder()
        {
            _path.Init(_layout, new BoardPathBuilder2D.Settings
            {
                cellSize = cellSize,
                segmentsPerTile = Mathf.Max(2, segmentsPerTile),
                cornerRadius01 = Mathf.Clamp(cornerRadius, 0f, 0.49f),
                smoothCorners = smoothOverlayPath
            });
        }
        
        void SyncLayoutFromSerialized() 
        { 
            _layout.CellSize = cellSize; 
            _layout.Origin = origin;
        }
        
        public Color GetColorForIndex(int idx) => colorService != null ? colorService.GetTileColor(idx) : Color.white;

        void OnEnable()
        {
            if (controller != null)
            {
                controller.OnRestart += Rebuild;
                controller.OnRedraw += RedrawAll;
                controller.OnDeltaApplied += ApplyDelta;
                controller.OnWin += HandleWin;
                controller.OnEditorModeChanged += HandleEditorModeChanged;
            }

            _intro = GetComponent<LevelIntroAnimator2D>();
            if (_intro != null)
                _intro.OnIntroFinished += HandleIntroFinished;
        }

        void OnDisable()
        {
            if (controller != null)
            {
                controller.OnRestart -= Rebuild;
                controller.OnRedraw -= RedrawAll;
                controller.OnDeltaApplied -= ApplyDelta;
                controller.OnWin -= HandleWin;
                controller.OnEditorModeChanged -= HandleEditorModeChanged;
            }

            if (_intro != null)
                _intro.OnIntroFinished -= HandleIntroFinished;
            _snakes.Stop();
            _selection.StopAll();

            StopAllActiveAnimations();
        }
        
        void HandleEditorModeChanged(bool enabled)
        {
            if (enabled)
            {
                // Kill any active selection visuals so the editor feels “clean”.
                _selection.StopAll();
            }
            // Redraw will happen through controller edits; no need to force rebuild here.
        }

        void HandleTileClicked(int x, int y)
        {
            // Editor mode: clicking a tile is an editor action, not selection/gameplay.
            if (controller != null && controller.EditorMode)
            {
                controller.EditorTapCell(new Vector2Int(x, y));
                return;
            }

            _selection.TryHandleTileClicked(x, y);
        }
        
        void HandleIntroFinished()
        {
            _introRunning = false;

            // If something changed while intro was playing, rebuild once now.
            if (_snakeRebuildQueued)
            {
                _snakeRebuildQueued = false;
                _snakes.RebuildBoardSnakeOverlays();
            }
            else
            {
                // still ensure snakes exist after intro
                _snakes.RebuildBoardSnakeOverlays();
            }
        }

        void StopAllActiveAnimations()
        {
            _selection.StopAll();
            
            _snakes.RestoreTilesFromHideRefCounts();
            _snakes.ClearAll();
        }

        void Rebuild()
        {
            if (controller == null || controller.State == null)
                return;
            
            // Stop any running animations (new level layout)
            StopAllActiveAnimations();
            
            colorService?.ClearAllPermanentOverrides();

            // POOL: recycle tiles instead of destroying them
            ReleaseAllTilesToPool();

            var s = controller.State;
            SyncLayoutFromSerialized();
            _layout.ComputeCenteredOrigin(s.width, s.height);
            origin = _layout.Origin; // keep serialized field in sync for inspector/debugging
            
            _tiles = new TileView2D[s.width * s.height];
            _dots?.RebuildCache();

            colorService?.RebuildStableArrowColors();
            
            if (colorService != null && controller != null &&
                controller.ArrowColor == BoardController.ArrowColorMode.UseSpawnMask)
            {
                var mask = controller.ArrowColorMask;
                if (mask != null)
                {
                    colorService.ApplyArrowColorMaskFromImage(
                        mask,
                        controller.ArrowColorMaskAlphaThreshold,
                        controller.ArrowColorMaskQuantizeSteps);
                }
            }
            
            TryFrameCamera(animate: true);

            for (int y = 0; y < s.height; y++)
            for (int x = 0; x < s.width; x++)
            {
                int idx = s.Index(x, y);
                var tv = GetTile();
                if (tv == null) continue;

                tv.Init(controller, x, y);
                tv.SetInitialDotVisible(s.tiles[idx].type == TileType.Arrow);
                tv.SetDotSuppressed(!controller.CanShowDot(idx));

                // make sure subscription is single-instance per tile
                tv.Clicked -= HandleTileClicked;
                tv.Clicked += HandleTileClicked;

                tv.transform.localPosition = new Vector3(
                    origin.x + x * cellSize,
                    origin.y + y * cellSize,
                    0f
                );
                
                var c = colorService != null ? colorService.GetTileColor(idx) : Color.white;

                tv.Render(s.tiles[idx], c);
                tv.SetHidden(false);

                _tiles[idx] = tv;
            }
            
            // Board layout changed -> refresh dot cache (empty indices, max ripple distance, etc.).
            _dots?.RebuildCache();
            
            _introRunning = false;
            _snakeRebuildQueued = false;

            var intro = GetComponent<LevelIntroAnimator2D>();
            if (intro != null)
            {
                _introRunning = true;

                _snakes.ClearAll();

                intro.Play();

                // Queue a snake build for when intro finishes
                _snakeRebuildQueued = true;
            }
            else
            {
                // No intro component -> show board snakes immediately
                _snakes.RebuildBoardSnakeOverlays();
            }
        }
        
        void ApplyDelta(MoveDelta d)
        {
            if (controller == null || controller.State == null || _tiles == null)
                return;
            
            var s = controller.State;

            for (int i = 0; i < _tiles.Length; i++)
            {
                if (colorService != null && s.tiles[i].type != TileType.Arrow)
                    colorService.ClearPermanentOverride(i);

                _tiles[i].Render(s.tiles[i], colorService != null ? colorService.GetTileColor(i) : Color.white);
            }
            if (_introRunning)
                _snakeRebuildQueued = true;
            else
                RebuildBoardSnakeOverlays();

        }
        
        public void RedrawAll()
        {
            if (controller == null || controller.State == null || _tiles == null)
                return;
            
            var s = controller.State;
            if (s == null) return;

            if (colorService != null)
            {
                // In edit mode we don’t want “stale” tint/blocked overrides sticking around.
                if (controller != null && controller.EditorMode)
                    colorService.ClearAllPermanentOverrides();

                // Board changed (especially in edit mode) -> rebuild stable chain colors.
                colorService.RebuildStableArrowColors();
            }

            for (int i = 0; i < _tiles.Length; i++)
            {
                if (controller != null && controller.EditorMode)
                    _tiles[i].SetInitialDotVisible(s.tiles[i].type == TileType.Arrow);

                _tiles[i].Render(s.tiles[i], colorService != null ? colorService.GetTileColor(i) : Color.white);
            }

            if (_introRunning)
                _snakeRebuildQueued = true;
            else
                RebuildBoardSnakeOverlays();
        }

        void RebuildBoardSnakeOverlays()
        {
            _snakes.RebuildBoardSnakeOverlays();
        }

        void HandleWin()
        {
            if (_dots != null)
                _dots.MarkWinPending();
            else
                GameEvents.RaiseLevelWon();
        }
        
        public Sprite GetHeadSprite()    => tilePrefab != null ? tilePrefab.head   : null;

        // Simple brute-force restore (safe): clear ref counts and show all tiles
        public void UnhideAllForIntro()
        {
            if (_tiles == null) return;
            
            for (int i = 0; i < _tiles.Length; i++)
                if (_tiles[i] != null)
                    _tiles[i].SetHidden(false);
        }
        
        public Sprite GetOverlayStraightSprite()
        {
            if (tilePrefab == null) return null;
            return tilePrefab.tail;
        }
        
        public int IntroSamplesPerTile => Mathf.Max(2, segmentsPerTile);
        
        public void BuildIntroWorldPoints(List<Vector2Int> orderedCells, List<Vector3> outPts, BoardState s)
            => _path.BuildIntroWorldPoints(orderedCells, outPts, s);
        
        void BuildBoardWorldPoints(List<Vector2Int> orderedCells, List<Vector3> outPts, BoardState s)
            => _path.BuildBoardWorldPoints(orderedCells, outPts, s);

        void RedrawCellsFromLiveState(List<Vector2Int> cells)
        {
            var live = controller.State;
            if (live == null || _tiles == null) return;

            for (int i = 0; i < cells.Count; i++)
            {
                var p = cells[i];
                if (!live.InBounds(p.x, p.y)) continue;

                int idx = live.Index(p.x, p.y);
                _tiles[idx].SetHidden(false); // ensure visible now
                _tiles[idx].Render(live.tiles[idx], colorService != null ? colorService.GetTileColor(idx) : Color.white);
            }
        }
        
        // Show dots on every tile (even arrows/blocks) for the intro "grid" look.
        public void SetAllDotsVisibleForIntro(bool visible)
        {
            _dots?.SetAllDotsVisibleForIntro(visible);
        }

        // As an arrow reveals in intro, hide dots just on that chain.
        public void SetDotsVisibleForCells(List<Vector2Int> cells, bool visible)
        {
            _dots?.SetDotsVisibleForCells(cells, visible);
        }

        public void ClearIntroDotOverrides()
        {
            _dots?.ClearIntroDotOverrides();
        }
        
        public void HandlePointerTileClicked(int x, int y, Vector3 clickWorldPos)
        {
            _selection.TryHandleTileClicked(x, y, clickWorldPos);
        }
        
        public void EditorPreviewStroke(IReadOnlyList<Vector2Int> cells)
        {
            if (cells == null || cells.Count < 2)
            {
                ClearEditorPreviewStroke();
                return;
            }

            EnsureEditorPreviewOverlay();

            _editorPreviewCells.Clear();
            for (int i = 0; i < cells.Count; i++)
                _editorPreviewCells.Add(cells[i]);

            // Build a smooth-ish world polyline from the cell stroke.
            _editorPreviewRaw.Clear();
            _editorPreviewRaw.AddRange(_path.BuildSmoothWorldPathFromCells(_editorPreviewCells));

            if (_editorPreviewRaw.Count == 0)
            {
                ClearEditorPreviewStroke();
                return;
            }

            // Denser spacing for preview so segments show while dragging.
            float previewSpacing = Mathf.Max(0.001f, _path.PointSpacingWorld * 0.35f);

            _editorPreviewPts.Clear();
            _editorPreviewPts.AddRange(BoardPathBuilder2D.ResamplePolylineEven(_editorPreviewRaw, previewSpacing));

            // Fallback: if resampling collapses, use raw.
            if (_editorPreviewPts.Count < 2)
            {
                _editorPreviewPts.Clear();
                _editorPreviewPts.AddRange(_editorPreviewRaw);
            }

            // Still too few? Force 2 points so the segment renderer activates.
            if (_editorPreviewPts.Count == 1)
            {
                Vector3 p0 = _editorPreviewPts[0];
                _editorPreviewPts.Add(p0 + Vector3.right * 0.01f);
            }

            if (_editorPreviewPts.Count < 2)
            {
                ClearEditorPreviewStroke();
                return;
            }

            _editorPreviewOverlay.SetVisible(true);
            _editorPreviewOverlay.SetTailMode(BoardSnakeRenderer2D.TailMode.AnchoredTail);

            // Show full snake immediately
            _editorPreviewOverlay.UpdateSnake(_editorPreviewPts, _editorPreviewPts.Count - 1f);
        }

        public void ClearEditorPreviewStroke()
        {
            if (_editorPreviewOverlay == null)
                return;

            _arrowOverlays.Release(_editorPreviewOverlay);
            _editorPreviewOverlay = null;
        }

        void EnsureEditorPreviewOverlay()
        {
            if (_editorPreviewOverlay != null)
                return;

            // Above board snakes/selection (tweak if you need).
            int sortingOrder = 60;

            _editorPreviewOverlay = _arrowOverlays.Acquire(sortingOrder);
            _editorPreviewOverlay.SetVisible(true);

            // Slightly translucent preview color
            _editorPreviewOverlay.SetOverrideColor(new Color(1f, 1f, 1f, 0.7f));
        }
        
//         void Update()
//         {
// #if UNITY_EDITOR || DEVELOPMENT_BUILD
//             HandleTimeScaleDebug();
// #endif
//         }
//
// #if UNITY_EDITOR || DEVELOPMENT_BUILD
//         void HandleTimeScaleDebug()
//         {
//             if (Input.GetKeyDown(KeyCode.Alpha1))
//             {
//                 Time.timeScale = 1f;
//                 Debug.Log("TimeScale = 1.0");
//             }
//             else if (Input.GetKeyDown(KeyCode.Alpha2))
//             {
//                 Time.timeScale = 0.1f;
//                 Debug.Log("TimeScale = 0.1");
//             }
//             else if (Input.GetKeyDown(KeyCode.Alpha3))
//             {
//                 Time.timeScale = 0.01f;
//                 Debug.Log("TimeScale = 0.01");
//             }
//             else if (Input.GetKeyDown(KeyCode.Alpha0))
//             {
//                 Time.timeScale = 0f;
//                 Debug.Log("TimeScale = 0.0 (PAUSED)");
//             }
//         }
// #endif
     }
}
