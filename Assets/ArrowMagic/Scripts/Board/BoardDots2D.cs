using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    /// <summary>
    /// Optional dot animation component.
    /// Owns all dot animation variables (inspector).
    /// If not present on the BoardView2D GameObject, dot animations are absent.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class BoardDots2D : MonoBehaviour
    {
        [System.Serializable]
        public struct WinRingSettings
        {
            [InspectorName("启用")]
            public bool enabled;
            [InspectorName("持续时间")]
            public float duration;
            [InspectorName("光环宽度（格）")]
            public float ringWidthCells;
            [InspectorName("基础颜色")]
            public Color baseColor;
            [InspectorName("光环颜色")]
            public Color ringColor;
            [InspectorName("最大缩放")]
            public float maxScale;
        }

        [Header("脉冲")]
        [InspectorName("点颜色 A")]
        [SerializeField] Color dotA = new Color(1f,1f,1f,0.15f);
        [InspectorName("点颜色 B")]
        [SerializeField] Color dotB = new Color(1f,1f,1f,0.35f);
        [InspectorName("点脉冲速度")]
        [SerializeField] float dotPulseSpeed = 1.2f;

        [Header("涟漪")]
        [InspectorName("涟漪速度")]
        [SerializeField] float rippleSpeed = 6f;
        [InspectorName("涟漪宽度")]
        [SerializeField] float rippleWidth = 1.25f;
        [InspectorName("涟漪强度")]
        [SerializeField] float rippleStrength = 0.75f;

        [Header("胜利光环")]
        [InspectorName("胜利光环")]
        [SerializeField] WinRingSettings winRing = new WinRingSettings
        {
            enabled = true,
            duration = 0.65f,
            ringWidthCells = 1.25f,
            baseColor = new Color(1f, 1f, 1f, 0.15f),
            ringColor = new Color(1f, 0.8365737f, 0f, 0.75f),
            maxScale = 2.5f
        };
        

        BoardController _controller;
        System.Func<TileView2D[]> _getTiles;
        System.Action _redrawAll;
        System.Action _raiseLevelWon;

        // Cached indices for ALL tiles that have a TileView.
        readonly List<int> _dotTileIndices = new List<int>(256);
        bool _cacheValid;
        int _cachedTileCount;
        BoardState _cachedState;

        float _maxBoardCellDist;

        float _rippleStartTime = -999f;
        Vector2Int _rippleCenter;

        bool _pendingWinVfx;
        bool _winVfxFired;
        Coroutine _winDotRingCo;

        /// <summary>
        /// Call from BoardView2D when the board is ready (after tiles created / state assigned).
        /// </summary>
        public void Configure(
            BoardController controller,
            System.Func<TileView2D[]> getTiles,
            System.Action redrawAll,
            System.Action raiseLevelWon)
        {
            _controller = controller;
            _getTiles = getTiles;
            _redrawAll = redrawAll;
            _raiseLevelWon = raiseLevelWon;

            RebuildCache();
        }

        public void RebuildCache()
        {
            _dotTileIndices.Clear();
            _cacheValid = false;

            var tiles = _getTiles != null ? _getTiles() : null;
            if (tiles == null || _controller == null) return;

            var s = _controller.State;
            if (s == null) return;

            _cachedState = s;
            _cachedTileCount = tiles.Length;

            _maxBoardCellDist = Mathf.Sqrt(
                (s.width - 1) * (s.width - 1) +
                (s.height - 1) * (s.height - 1));

            for (int i = 0; i < tiles.Length; i++)
            {
                if (tiles[i] == null) continue;
                _dotTileIndices.Add(i);
            }

            _cacheValid = true;
        }

        void EnsureCache()
        {
            var tiles = _getTiles != null ? _getTiles() : null;
            if (tiles == null || _controller == null) return;

            var s = _controller.State;
            if (s == null) return;

            if (!_cacheValid || _cachedState != s || _cachedTileCount != tiles.Length)
                RebuildCache();
        }

        void OnDisable()
        {
            StopMoveIfAny();
        }

        public void StopMoveIfAny()
        {
            if (_winDotRingCo != null)
            {
                StopCoroutine(_winDotRingCo);
                _winDotRingCo = null;
            }
        }

        void Update()
        {
            // Optional component: if not configured, do nothing.
            if (_controller == null || _getTiles == null) return;

            EnsureCache();
            if (_dotTileIndices.Count == 0) return;

            AnimateDots();
        }

        // ---- External triggers (called by BoardView2D / game events) ----

        public void TriggerRipple(Vector2Int centerCell)
        {
            _rippleCenter = centerCell;
            _rippleStartTime = Time.time;
        }

        public void MarkWinPending()
        {
            _pendingWinVfx = true;
            _winVfxFired = false;
        }

        public void TryFirePendingWinVfx(int activeOverlaysCount)
        {
            if (!_pendingWinVfx || _winVfxFired) return;
            if (_controller == null || !_controller.Won) return;
            if (activeOverlaysCount > 0) return;

            _winVfxFired = true;
            _pendingWinVfx = false;

            if (winRing.enabled)
            {
                if (_winDotRingCo != null) StopCoroutine(_winDotRingCo);
                _winDotRingCo = StartCoroutine(WinDotRing());
            }
            
            _raiseLevelWon?.Invoke();
        }

        public void SetAllDotsVisibleForIntro(bool visible)
        {
            var tiles = _getTiles != null ? _getTiles() : null;
            if (tiles == null) return;

            for (int i = 0; i < tiles.Length; i++)
            {
                if (tiles[i] == null) continue;
                tiles[i].SetIntroDotVisible(visible && tiles[i].HasInitialDot);
            }
        }

        public void SetDotsVisibleForCells(List<Vector2Int> cells, bool visible)
        {
            var tiles = _getTiles != null ? _getTiles() : null;
            var s = _controller != null ? _controller.State : null;
            if (tiles == null || s == null || cells == null) return;

            for (int i = 0; i < cells.Count; i++)
            {
                var p = cells[i];
                if (!s.InBounds(p.x, p.y)) continue;

                int idx = s.Index(p.x, p.y);
                if (tiles[idx] != null)
                    tiles[idx].SetIntroDotVisible(visible && tiles[idx].HasInitialDot);
            }
        }

        public void ClearIntroDotOverrides()
        {
            var tiles = _getTiles != null ? _getTiles() : null;
            if (tiles == null) return;

            for (int i = 0; i < tiles.Length; i++)
                tiles[i]?.ClearIntroDotOverride();
        }

        // ---- Implementation ----

        void AnimateDots()
        {
            var tiles = _getTiles != null ? _getTiles() : null;
            if (tiles == null || _controller == null) return;

            var s = _controller.State;
            if (s == null) return;

            float t = Time.time;
            float pulse = 0.5f + 0.5f * Mathf.Sin(t * dotPulseSpeed * Mathf.PI * 2f);

            float rippleT = (t - _rippleStartTime) * rippleSpeed;

            int w = s.width;

            bool rippleActive = _rippleStartTime > 0f;
            if (rippleActive)
            {
                float killAt = _maxBoardCellDist + rippleWidth + 0.5f;
                if (rippleT > killAt) rippleActive = false;
                if (!rippleActive) _rippleStartTime = -999f;
            }

            for (int k = 0; k < _dotTileIndices.Count; k++)
            {
                int i = _dotTileIndices[k];
                var tv = tiles[i];
                if (tv == null) continue;

                if (tv.DotSuppressed) continue;

                int x = i % w;
                int y = i / w;

                float u = pulse;

                if (rippleActive)
                {
                    float dx = x - _rippleCenter.x;
                    float dy = y - _rippleCenter.y;
                    float d = Mathf.Sqrt(dx * dx + dy * dy);
                    float band = Mathf.Abs(d - rippleT);
                    float ripple = Mathf.Clamp01(1f - (band / rippleWidth)) * rippleStrength;
                    u = Mathf.Clamp01(u + ripple);
                }

                tv.SetEmptyDotColor(Color.Lerp(dotA, dotB, u));
            }
        }

        IEnumerator WinDotRing()
        {
            var tiles = _getTiles != null ? _getTiles() : null;
            var s = _controller != null ? _controller.State : null;
            if (s == null || tiles == null) yield break;

            int w = s.width;
            int h = s.height;

            float cx = (w - 1) * 0.5f;
            float cy = (h - 1) * 0.5f;

            float maxR;
            {
                float dx = Mathf.Max(cx, w - 1 - cx);
                float dy = Mathf.Max(cy, h - 1 - cy);
                maxR = Mathf.Sqrt(dx * dx + dy * dy);
            }

            float invWidth = 1f / Mathf.Max(0.0001f, winRing.ringWidthCells);

            for (int i = 0; i < tiles.Length; i++)
            {
                var tv = tiles[i];
                if (tv == null) continue;

                if (tv.DotSuppressed || !tv.HasInitialDot)
                {
                    tv.SetDotVisible(false);
                    tv.ResetDotScale();
                    continue;
                }

                tv.SetDotVisible(true);
                tv.SetDotColor(winRing.baseColor);
                tv.ResetDotScale();
            }

            float t0 = Time.time;
            while (true)
            {
                float u = (Time.time - t0) / Mathf.Max(0.0001f, winRing.duration);
                if (u >= 1f) break;

                float ringR = Mathf.Lerp(0f, maxR, u);

                for (int y = 0; y < h; y++)
                {
                    float dy = y - cy;
                    for (int x = 0; x < w; x++)
                    {
                        int idx = s.Index(x, y);
                        var tv = tiles[idx];
                        if (tv == null || tv.DotSuppressed || !tv.HasInitialDot) continue;

                        float dx = x - cx;
                        float d = Mathf.Sqrt(dx * dx + dy * dy);

                        float band = Mathf.Abs(d - ringR);
                        float a = Mathf.Clamp01(1f - band * invWidth);
                        a = a * a * (3f - 2f * a); // smoothstep

                        tv.SetDotColor(Color.Lerp(winRing.baseColor, winRing.ringColor, a));
                        float scaleA = Mathf.Pow(a, 0.65f);
                        tv.SetDotScale01(scaleA, winRing.maxScale);
                    }
                }

                yield return null;
            }

            for (int i = 0; i < tiles.Length; i++)
                tiles[i]?.ResetDotScale();

            _redrawAll?.Invoke();
            _winDotRingCo = null;
        }
    }
}
