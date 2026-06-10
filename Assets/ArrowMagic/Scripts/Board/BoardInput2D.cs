using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
#endif

namespace PixelBug.ArrowMagic
{
    public sealed class BoardInput2D : MonoBehaviour
    {
        [SerializeField] Camera cam;
        [SerializeField] BoardController controller;

        BoardCameraFramer2D _cameraFramer;
        [SerializeField] BoardView2D view;

#if ENABLE_INPUT_SYSTEM
        [Header("Input System")]
        [SerializeField] InputActionReference point;
        [SerializeField] InputActionReference press;
#endif

        [Header("Raycast")]
        [SerializeField] LayerMask tileLayerMask = ~0;

        [Header("Panning")]
        [SerializeField] bool allowPanning = true;
        [SerializeField] float dragThresholdPixels = 10f;
        [SerializeField] float panSensitivity = 1f;
        
        [Header("Zoom")]
        [SerializeField] bool allowZoom = true;
        [SerializeField] float minOrthographicSize = 3f;
        [SerializeField] float maxOrthographicSize = 40f;
        [SerializeField] float minPerspectiveFov = 20f;
        [SerializeField] float maxPerspectiveFov = 70f;
        [SerializeField] float wheelZoomSpeed = 0.5f;
        [SerializeField] float pinchZoomSpeed = 0.01f;

        [Header("Editor Mode")]
        [Tooltip("Max time (seconds) to treat a 1-tile press as a tap (delete).")]
        float editorTapMaxSeconds = 0.35f;

        bool _isPressed;
        bool _isPanning;

        // If true, this press is being used to draw/edit (tile-to-tile).
        bool _isEditingStroke;
        double _pressStartTime;

        Vector2 _pressScreenPos;
        Vector3 _lastWorldPos;
        TileView2D _pressedTile;

        // Stroke capture (editor mode)
        readonly List<Vector2Int> _strokeCells = new();
        Vector2Int _lastCell;
        bool _hasLastCell;
        
        // Pinch zoom state
        bool _isPinching;
        float _lastPinchDistance;
        
        bool InputBlocked =>
            controller != null &&
            !controller.EditorMode &&
            (controller.Won || controller.Lost);

        void OnEnable()
        {
            if (cam == null) cam = Camera.main;

            // Use camera framer ONLY if it exists on the same GameObject
            _cameraFramer = GetComponent<BoardCameraFramer2D>();

#if ENABLE_INPUT_SYSTEM
            point?.action?.Enable();
            press?.action?.Enable();

            if (press != null)
            {
                press.action.started += OnPressStarted;
                press.action.canceled += OnPressCanceled;
            }
#endif

            if (view == null) view = FindFirstObjectByType<BoardView2D>();
        }

        void OnDisable()
        {
#if ENABLE_INPUT_SYSTEM
            if (press != null)
            {
                press.action.started -= OnPressStarted;
                press.action.canceled -= OnPressCanceled;
            }

            point?.action?.Disable();
            press?.action?.Disable();
#endif

            // Cleanup editor preview
            view?.ClearEditorPreviewStroke();

            _isPressed = false;
            _isPanning = false;
            _isEditingStroke = false;
            _isPinching = false;
            _pressedTile = null;

            _strokeCells.Clear();
            _hasLastCell = false;
        }

        void Update()
        {
            // if (!_isPressed) return;
            if (cam == null) return;
            
            if (InputBlocked)
            {
                CancelCurrentInteraction();
                return;
            }
            
            // Desktop / laptop zoom:
            // mouse wheel, and on many trackpads this also receives two-finger scroll.
            HandleScrollZoom();

            // Mobile / touchscreen pinch zoom.
            if (HandleTouchPinchZoom())
                return;

#if !ENABLE_INPUT_SYSTEM
            HandleLegacyPressTransitions();
#endif

            if (!_isPressed) return;
            if (!TryReadPointerScreenPosition(out Vector2 screenPos)) return;

            // --------------------------
            // Editor mode stroke capture + live preview
            // --------------------------
            if (_isEditingStroke)
            {
                if (TryGetTileAtScreenPos(screenPos, out var tile) && tile != null)
                {
                    var cell = new Vector2Int(tile.X, tile.Y);
                    if (AddStrokeCellIfNew(cell))
                        view?.EditorPreviewStroke(_strokeCells);
                }
                return; // while editing stroke, never pan
            }

            // --------------------------
            // Panning
            // --------------------------
            if (!_isPanning && allowPanning)
            {
                float moved = Vector2.Distance(screenPos, _pressScreenPos);
                if (moved >= dragThresholdPixels)
                {
                    _isPanning = true;

                    // Stop & suppress framing for the rest of this level
                    _cameraFramer?.SuppressUntilNextLevel();

                    _pressedTile = null;
                    _lastWorldPos = ScreenToWorldOnBoard(screenPos);
                }
            }

            if (_isPanning)
            {
                Vector3 before = ScreenToWorldOnBoard(screenPos);
                Vector3 delta = before - _lastWorldPos;

                cam.transform.position -= new Vector3(delta.x, delta.y, 0f) * panSensitivity;

                // Re-sample AFTER moving camera to avoid bounce
                _lastWorldPos = ScreenToWorldOnBoard(screenPos);
            }
        }

        void BeginPress(double startTime, Vector2 screenPos)
        {
            if (cam == null || controller == null) return;

            if (InputBlocked) return;

            _isPressed = true;
            _isPanning = false;
            _isEditingStroke = false;

            _pressStartTime = startTime;
            _pressScreenPos = screenPos;

            // Player interaction always wins
            _cameraFramer?.SuppressUntilNextLevel();

            _pressedTile = TryGetTileAtScreenPos(_pressScreenPos);

            // Editor mode: if press starts on a tile AND tile is allowed by mask, begin stroke
            if (controller.EditorMode && _pressedTile != null)
            {
                // Mask limitation: only allow edits where can-spawn is true
                if (!controller.CanSpawnAt(_pressedTile.X, _pressedTile.Y))
                {
                    // Don’t start editing stroke; allow panning instead if user drags.
                    _pressedTile = null;
                    return;
                }

                _isEditingStroke = true;
                _strokeCells.Clear();
                _hasLastCell = false;

                AddStrokeCellIfNew(new Vector2Int(_pressedTile.X, _pressedTile.Y));
                view?.EditorPreviewStroke(_strokeCells);
            }
        }

        void EndPress(double endTime, Vector2 screenPos)
        {
            if (!_isPressed) return;

            if (InputBlocked)
            {
                CancelCurrentInteraction();
                return;
            }
            
            // --------------------------
            // Finish editor stroke
            // --------------------------
            if (_isEditingStroke)
            {
                _isPressed = false;
                _isPanning = false;
                _isEditingStroke = false;

                view?.ClearEditorPreviewStroke();

                if (controller != null && controller.EditorMode && _strokeCells.Count > 0)
                {
                    double heldSeconds = endTime - _pressStartTime;

                    // Tap delete (single tile, quick)
                    if (_strokeCells.Count == 1 && heldSeconds <= editorTapMaxSeconds)
                    {
                        controller.EditorTapCell(_strokeCells[0]);
                    }
                    else
                    {
                        controller.EditorApplyStroke(_strokeCells);
                    }
                }

                _strokeCells.Clear();
                _hasLastCell = false;
                _pressedTile = null;
                return;
            }

            // --------------------------
            // Gameplay click (only if not panning)
            // --------------------------
            if (!_isPanning && _pressedTile != null)
            {
                Vector3 worldPos = ScreenToWorldOnBoard(screenPos);

                if (view != null)
                {
                    view.HandlePointerTileClicked(_pressedTile.X, _pressedTile.Y, worldPos);
                }
                else
                {
                    _pressedTile.Click();
                }
            }

            _isPressed = false;
            _isPanning = false;
            _pressedTile = null;
        }

#if ENABLE_INPUT_SYSTEM
        void OnPressStarted(InputAction.CallbackContext ctx)
        {
            if (point == null) return;
            BeginPress(ctx.startTime, point.action.ReadValue<Vector2>());
        }

        void OnPressCanceled(InputAction.CallbackContext ctx)
        {
            Vector2 screenPos = point != null ? point.action.ReadValue<Vector2>() : _pressScreenPos;
            EndPress(ctx.time, screenPos);
        }
#endif
        
        void CancelCurrentInteraction()
        {
            _isPressed = false;
            _isPanning = false;
            _isEditingStroke = false;
            _pressedTile = null;

            _strokeCells.Clear();
            _hasLastCell = false;

            view?.ClearEditorPreviewStroke();
        }
        
        void HandleScrollZoom()
        {
            if (!allowZoom) return;

            float scrollY = 0f;

#if ENABLE_INPUT_SYSTEM
            if (Mouse.current != null)
                scrollY = Mouse.current.scroll.ReadValue().y;
#else
            scrollY = Input.mouseScrollDelta.y;
#endif

            if (Mathf.Abs(scrollY) < 0.01f)
                return;

            _cameraFramer?.SuppressUntilNextLevel();

            // Positive scroll should zoom in.
            float zoomDelta = -scrollY * wheelZoomSpeed;
            ApplyZoomDelta(zoomDelta);
        }
        
        bool HandleTouchPinchZoom()
        {
            if (!allowZoom) return false;

#if ENABLE_INPUT_SYSTEM
            var ts = Touchscreen.current;
            if (ts == null)
            {
                _isPinching = false;
                _lastPinchDistance = 0f;
                return false;
            }

            int activeCount = GetActiveTouchCount();
            if (activeCount < 2)
            {
                _isPinching = false;
                _lastPinchDistance = 0f;
                return false;
            }

            if (!TryGetFirstTwoActiveTouches(out var touch0, out var touch1))
                return false;

            Vector2 p0 = touch0.position.ReadValue();
            Vector2 p1 = touch1.position.ReadValue();
            float currentDistance = Vector2.Distance(p0, p1);

            if (!_isPinching)
            {
                _isPinching = true;
                _lastPinchDistance = currentDistance;

                // Pinch takes over from any click/pan/edit interaction.
                CancelCurrentInteraction();
                _cameraFramer?.SuppressUntilNextLevel();
                return true;
            }

            float distanceDelta = currentDistance - _lastPinchDistance;
            _lastPinchDistance = currentDistance;

            if (Mathf.Abs(distanceDelta) > 0.01f)
            {
                _cameraFramer?.SuppressUntilNextLevel();

                // Fingers farther apart => zoom in.
                float zoomDelta = -distanceDelta * pinchZoomSpeed;
                ApplyZoomDelta(zoomDelta);
            }

            return true;
#else
            if (Input.touchCount < 2)
            {
                _isPinching = false;
                _lastPinchDistance = 0f;
                return false;
            }

            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);
            float currentDistance = Vector2.Distance(touch0.position, touch1.position);

            if (!_isPinching)
            {
                _isPinching = true;
                _lastPinchDistance = currentDistance;
                CancelCurrentInteraction();
                _cameraFramer?.SuppressUntilNextLevel();
                return true;
            }

            float distanceDelta = currentDistance - _lastPinchDistance;
            _lastPinchDistance = currentDistance;

            if (Mathf.Abs(distanceDelta) > 0.01f)
            {
                _cameraFramer?.SuppressUntilNextLevel();
                ApplyZoomDelta(-distanceDelta * pinchZoomSpeed);
            }

            return true;
#endif
        }

        void ApplyZoomDelta(float delta)
        {
            if (cam == null) return;

            if (cam.orthographic)
            {
                cam.orthographicSize = Mathf.Clamp(
                    cam.orthographicSize + delta,
                    minOrthographicSize,
                    maxOrthographicSize
                );
            }
            else
            {
                cam.fieldOfView = Mathf.Clamp(
                    cam.fieldOfView + delta,
                    minPerspectiveFov,
                    maxPerspectiveFov
                );
            }
        }

        int GetActiveTouchCount()
        {
#if ENABLE_INPUT_SYSTEM
            var ts = Touchscreen.current;
            if (ts == null) return 0;

            int count = 0;
            foreach (var touch in ts.touches)
            {
                if (touch.press.isPressed)
                    count++;
            }
            return count;
#else
            return Input.touchCount;
#endif
        }
        
#if ENABLE_INPUT_SYSTEM
        bool TryGetFirstTwoActiveTouches(out TouchControl a, out TouchControl b)
        {
            a = null;
            b = null;

            var ts = Touchscreen.current;
            if (ts == null) return false;

            foreach (var touch in ts.touches)
            {
                if (!touch.press.isPressed)
                    continue;

                if (a == null) a = touch;
                else
                {
                    b = touch;
                    return true;
                }
            }

            return false;
        }
#endif

        bool TryReadPointerScreenPosition(out Vector2 screenPos)
        {
#if ENABLE_INPUT_SYSTEM
            if (point == null)
            {
                screenPos = default;
                return false;
            }

            screenPos = point.action.ReadValue<Vector2>();
            return true;
#else
            if (Input.touchCount > 0)
            {
                screenPos = Input.GetTouch(0).position;
                return true;
            }

            screenPos = Input.mousePosition;
            return true;
#endif
        }

#if !ENABLE_INPUT_SYSTEM
        void HandleLegacyPressTransitions()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                    BeginPress(Time.realtimeSinceStartupAsDouble, touch.position);
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    EndPress(Time.realtimeSinceStartupAsDouble, touch.position);
                return;
            }

            if (Input.GetMouseButtonDown(0))
                BeginPress(Time.realtimeSinceStartupAsDouble, Input.mousePosition);
            else if (Input.GetMouseButtonUp(0))
                EndPress(Time.realtimeSinceStartupAsDouble, Input.mousePosition);
        }
#endif

        TileView2D TryGetTileAtScreenPos(Vector2 screenPos)
        {
            Vector3 worldPos = ScreenToWorldOnBoard(screenPos);
            var hit = Physics2D.Raycast((Vector2)worldPos, Vector2.zero, 0f, tileLayerMask);
            if (!hit.collider) return null;

            var tile = hit.collider.GetComponent<TileView2D>();
            if (!tile) tile = hit.collider.GetComponentInParent<TileView2D>();
            return tile;
        }

        bool TryGetTileAtScreenPos(Vector2 screenPos, out TileView2D tile)
        {
            tile = TryGetTileAtScreenPos(screenPos);
            return tile != null;
        }

        Vector3 ScreenToWorldOnBoard(Vector2 screenPos)
        {
            float z = -cam.transform.position.z;
            return cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, z));
        }

        // Returns true if stroke list changed
        bool AddStrokeCellIfNew(Vector2Int cell)
        {
            // Mask limitation: do not allow stroke to include masked-out cells
            if (controller != null && controller.EditorMode)
            {
                if (!controller.CanSpawnAt(cell.x, cell.y))
                    return false;
            }

            if (_hasLastCell && cell == _lastCell)
                return false;

            // Enforce 4-way adjacency while capturing so the stroke stays “dot-to-dot”.
            if (_strokeCells.Count > 0)
            {
                var prev = _strokeCells[_strokeCells.Count - 1];
                var d = cell - prev;

                bool adjacent =
                    (d == Vector2Int.up) || (d == Vector2Int.down) ||
                    (d == Vector2Int.left) || (d == Vector2Int.right);

                if (!adjacent)
                    return false;
            }

            _strokeCells.Add(cell);
            _lastCell = cell;
            _hasLastCell = true;
            return true;
        }
    }
}
