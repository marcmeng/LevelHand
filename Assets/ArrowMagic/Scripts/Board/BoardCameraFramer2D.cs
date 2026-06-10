using System.Collections;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    /// <summary>
    /// Optional camera framing component for BoardView2D.
    ///
    /// Attach this component when you want BoardView2D to auto-frame the camera.
    /// If this component is absent, camera framing is simply not performed.
    ///
    /// Perspective-only framing (orthographic cameras are ignored).
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class BoardCameraFramer2D : MonoBehaviour
    {
        [Header("Framing")]
        [Tooltip("Padding around the board, measured in cells.")]
        [SerializeField] float cameraPaddingInCells = 2.0f;

        [Tooltip("If true, camera moves smoothly to its target position when you pass animate=true.")]
        [SerializeField] bool animateCamera = true;

        [Tooltip("Seconds to animate the camera move.")]
        [SerializeField] float cameraMoveDuration = 0.6f;

        [Tooltip("Ease curve for camera movement.")]
        [SerializeField] AnimationCurve cameraMoveEase = AnimationCurve.EaseInOut(0, 0, 1, 1);

        [Header("Implementation")]
        [Tooltip("If true, uses CameraFitPerspective.FrameBoard instead of the internal distance-to-fit calculation.")]
        [SerializeField] bool useCameraFitPerspective;

        Camera _camOverride;
        Coroutine _moveCo;

        // Once the player starts interacting (panning/clicking), we suppress any further
        // framing calls until the next level explicitly re-enables it.
        bool _suppressFramingUntilNextLevel;

        /// <summary>Optionally override the camera used for framing.</summary>
        public void SetCamera(Camera cam) => _camOverride = cam;

        /// <summary>
        /// Prevents this component from moving the camera again until <see cref="AllowFramingForLevel"/> is called.
        /// Intended to be called as soon as the player starts interacting (panning/clicking).
        /// </summary>
        public void SuppressUntilNextLevel()
        {
            _suppressFramingUntilNextLevel = true;
            StopMoveIfAny();
        }

        /// <summary>
        /// Re-enables framing for the current level.
        /// Call this right before you frame on level start / between levels.
        /// </summary>
        public void AllowFramingForLevel()
        {
            _suppressFramingUntilNextLevel = false;
        }

        /// <summary>Stop any in-flight camera move started by this component.</summary>
        public void StopMoveIfAny()
        {
            if (_moveCo != null)
            {
                StopCoroutine(_moveCo);
                _moveCo = null;
            }
        }

        /// <summary>
        /// Frames the camera to fit the board bounds.
        /// Call this from BoardView2D after layout is bound and board state is known.
        /// </summary>
        public void FrameToBoard(BoardLayout2D layout, BoardState s, bool animate)
        {
            if (_suppressFramingUntilNextLevel)
                return;
            if (layout == null) return;

            var cam = _camOverride != null ? _camOverride : Camera.main;
            if (cam == null) return;
            if (cam.orthographic) return;

            layout.GetBoardWorldBounds(s, out var worldCenter, out var worldW, out var worldH);
            float paddingWorld = Mathf.Max(0f, cameraPaddingInCells) * layout.CellSize;

            StopMoveIfAny();

            if (useCameraFitPerspective)
            {
                CameraFitPerspective.FrameBoard(
                    cam,
                    worldCenter,
                    worldW,
                    worldH,
                    padding: paddingWorld
                );
                return;
            }

            // Assumes camera is axis-aligned, looking down -Z.
            float targetDistance = ComputePerspectiveDistanceToFitRect(cam, worldW, worldH, paddingWorld);
            Vector3 targetPos = new Vector3(worldCenter.x, worldCenter.y, worldCenter.z - targetDistance);

            bool shouldAnimate =
                animate &&
                animateCamera &&
                cameraMoveDuration > 0.001f &&
                isActiveAndEnabled;

            if (!shouldAnimate)
            {
                cam.transform.position = targetPos;
                return;
            }

            _moveCo = StartCoroutine(AnimateCameraPosition(cam, targetPos, cameraMoveDuration, cameraMoveEase));
        }

        static IEnumerator AnimateCameraPosition(Camera cam, Vector3 targetPos, float duration, AnimationCurve ease)
        {
            Vector3 startPos = cam.transform.position;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / duration;
                float u = Mathf.Clamp01(t);
                float e = (ease != null) ? ease.Evaluate(u) : u;

                cam.transform.position = Vector3.LerpUnclamped(startPos, targetPos, e);
                yield return null;
            }

            cam.transform.position = targetPos;
        }

        static float ComputePerspectiveDistanceToFitRect(Camera cam, float worldW, float worldH, float paddingWorld)
        {
            float w = Mathf.Max(0.0001f, worldW + 2f * paddingWorld);
            float h = Mathf.Max(0.0001f, worldH + 2f * paddingWorld);

            float vFov = cam.fieldOfView * Mathf.Deg2Rad;
            float hFov = 2f * Mathf.Atan(Mathf.Tan(vFov * 0.5f) * cam.aspect);

            float distH = (h * 0.5f) / Mathf.Tan(vFov * 0.5f);
            float distW = (w * 0.5f) / Mathf.Tan(hFov * 0.5f);

            return Mathf.Max(distH, distW);
        }
    }
}
