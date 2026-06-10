using UnityEngine;
using System;

namespace PixelBug.ArrowMagic
{
    public sealed class TileView2D : MonoBehaviour
    {
        [Header("Renderers")]
        [SerializeField] SpriteRenderer emptyDot;

        [Header("Sprites")]
        public Sprite head;
        public Sprite tail;

        BoardController _controller;
        int _x, _y;
        public Vector2Int Cell => new Vector2Int(_x, _y);
        
        public int X => _x;
        public int Y => _y;

        public event Action<int, int> Clicked;

        // Used while animating the overlay snake so the "real" arrow disappears.
        bool _hidden;

        // Dot existence is anchored to the generated board, not the live tile state.
        bool _initialDotVisible;

        // Intro dot override (used by intro animator).
        bool _introDotOverrideActive;
        bool _introDotVisible;

        // Suppress dot (used by win/selection effects that temporarily disable dots).
        bool _suppressDot;

        // Cache base scale for dot scaling effects.
        Vector3 _dotBaseScale;
        bool _dotBaseScaleCached;

        // Avoid redundant SetActive calls.
        bool _dotActiveCached;
        bool _dotActiveCachedValid;

        public bool DotSuppressed => _suppressDot;
        public bool HasInitialDot => _initialDotVisible;
        public bool Hidden => _hidden;

        public void Init(BoardController controller, int x, int y)
        {
            _controller = controller;
            _x = x;
            _y = y;
        }

        public void Click()
        {
            if (Clicked != null) Clicked.Invoke(_x, _y);
            else _controller?.ClickCell(_x, _y);
        }

        /// <summary>
        /// Hide/show this tile's visuals regardless of what Render() wants.
        /// Used while animating the overlay snake so the "real" arrow disappears.
        /// </summary>
        public void SetHidden(bool hidden)
        {
            _hidden = hidden;
            // Let Render() decide final state; no immediate flip here.
        }

        public void SetEmptyDotColor(Color c)
        {
            if (emptyDot != null) emptyDot.color = c;
        }

        public void SetInitialDotVisible(bool visible)
        {
            _initialDotVisible = visible;
        }

        // Back-compat helpers (BoardView2D may still call these).
        public void SetDotVisible(bool visible) => SetDotActive(visible);
        public void SetDotColor(Color c) => SetEmptyDotColor(c);

        public void SetDotSuppressed(bool suppressed)
        {
            _suppressDot = suppressed;

            // If suppressed, force off immediately.
            if (_suppressDot)
                SetDotActive(false);
        }

        public void SetIntroDotVisible(bool visible)
        {
            _introDotOverrideActive = true;
            _introDotVisible = visible;

            // Apply immediately (still respects suppression).
            SetDotActive(_introDotVisible && !_suppressDot);
        }

        public void ClearIntroDotOverride()
        {
            _introDotOverrideActive = false;
        }

        public void SetDotScale01(float t01, float maxScale = 1.6f)
        {
            if (emptyDot == null) return;

            CacheDotBaseScale();

            t01 = Mathf.Clamp01(t01);
            float s = Mathf.Lerp(1f, Mathf.Max(1f, maxScale), t01);
            emptyDot.transform.localScale = _dotBaseScale * s;
        }

        public void ResetDotScale()
        {
            if (emptyDot == null) return;
            CacheDotBaseScale();
            emptyDot.transform.localScale = _dotBaseScale;
        }

        void CacheDotBaseScale()
        {
            if (_dotBaseScaleCached) return;
            _dotBaseScale = emptyDot.transform.localScale;
            _dotBaseScaleCached = true;
        }

        void SetDotActive(bool active)
        {
            if (emptyDot == null) return;

            if (_dotActiveCachedValid && _dotActiveCached == active)
                return;

            emptyDot.gameObject.SetActive(active);
            _dotActiveCached = active;
            _dotActiveCachedValid = true;
        }

        public void Render(TileState t, Color arrowColor)
        {
            // Current implementation only deals with the empty-dot background.
            // If you later reintroduce pipe/arrow visuals here, keep dot logic isolated.

            if (_suppressDot)
            {
                SetDotActive(false);
                return;
            }

            bool dotVisible;

            if (_introDotOverrideActive)
            {
                dotVisible = _introDotVisible;
            }
            else
            {
                dotVisible = _initialDotVisible;
            }

            SetDotActive(dotVisible);
        }
    }
}
