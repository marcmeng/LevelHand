using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public sealed class ArrowSnakeOverlay2D : BoardSelectionAnimator2D.ISelectionOverlay
    {
        readonly GameObject _root;
        readonly List<SpriteRenderer> _segments = new();
        readonly SpriteRenderer _head;

        Sprite _straight;
        Sprite _headSprite;

        int _activeSegments;

        Vector3 _headExtraOffset;

        Color _baseColor = Color.white;
        Color _color = Color.white;

        bool _visible = true;
        Color? _overrideColor = null;

        int _sortingOrderBase;

        // Tail mode (anchored vs follow-head)
        BoardSnakeRenderer2D.TailMode _tailMode = BoardSnakeRenderer2D.TailMode.AnchoredTail;

        // Anchored-tail tuning
        float _headGapP = 0.35f;

        // Tangent stability
        float _tangentEpsP = 0.15f;

        public Vector3 HeadWorldPosition => _head != null ? _head.transform.position : Vector3.zero;

        public ArrowSnakeOverlay2D(Transform parent, int sortingOrderBase)
        {
            _sortingOrderBase = sortingOrderBase;
            _root = new GameObject("ArrowSnakeOverlay");
            _root.transform.SetParent(parent, false);

            _head = CreateRenderer("Head", sortingOrderBase + 1);
        }

        public void SetSortingOrderBase(int sortingOrderBase)
        {
            _sortingOrderBase = sortingOrderBase;
            if (_head != null) _head.sortingOrder = _sortingOrderBase + 1;

            for (int i = 0; i < _segments.Count; i++)
                if (_segments[i] != null)
                    _segments[i].sortingOrder = _sortingOrderBase;
        }

        public void ResetForPoolReuse()
        {
            _overrideColor = null;
            _baseColor = _color = Color.white;
            _headExtraOffset = Vector3.zero;

            // Default back to anchored (safer for intro/static). Callers can override per-use.
            _tailMode = BoardSnakeRenderer2D.TailMode.AnchoredTail;

            if (_head != null) _head.enabled = false;
            _activeSegments = 0;

            for (int i = 0; i < _segments.Count; i++)
                if (_segments[i] != null)
                    _segments[i].enabled = false;

            SetVisible(false);
        }

        SpriteRenderer CreateRenderer(string name, int sortingOrder)
        {
            var go = new GameObject(name);
            go.transform.SetParent(_root.transform, false);
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sortingOrder = sortingOrder;
            sr.enabled = false;
            return sr;
        }

        public void ConfigureSprites(Sprite straight, Sprite head)
        {
            _straight = straight;
            _headSprite = head;
        }

        public void SetTailMode(BoardSnakeRenderer2D.TailMode mode)
        {
            _tailMode = mode;
        }

        public void SetVisible(bool v)
        {
            _visible = v;
            if (_root != null) _root.SetActive(v);
        }

        public void SetOverrideColor(Color c)
        {
            _overrideColor = c;
            ApplyCurrentColor();
        }

        public void SetColor(Color c)
        {
            _baseColor = c;
            _color = c;
            _overrideColor = null;
            ApplyCurrentColor();
        }

        void ApplyCurrentColor()
        {
            var c = _overrideColor ?? _baseColor;

            if (_head != null) _head.color = c;
            for (int i = 0; i < _segments.Count; i++)
                _segments[i].color = c;
        }

        public void SetHeadExtraOffset(Vector3 worldOffset) => _headExtraOffset = worldOffset;
        public void ClearHeadExtraOffset() => _headExtraOffset = Vector3.zero;

        public void EnsureSegmentCount(int count)
        {
            _activeSegments = Mathf.Max(0, count);

            while (_segments.Count < count)
            {
                var sr = CreateRenderer($"Seg{_segments.Count}", _sortingOrderBase);
                sr.color = _color;
                _segments.Add(sr);
            }

            for (int i = 0; i < _segments.Count; i++)
            {
                _segments[i].sortingOrder = _sortingOrderBase;
                _segments[i].enabled = (i < _activeSegments);
            }
        }

        public void Destroy()
        {
            if (_root != null)
                GameObject.Destroy(_root);
        }

        public void UpdateSnake(List<Vector3> pts, float headProgress)
        {
            if (!_visible) return;
            if (pts == null || pts.Count == 0) return;

            int n = pts.Count;
            if (n == 1)
            {
                _head.enabled = true;
                _head.sprite = _headSprite;
                _head.transform.position = pts[0] + _headExtraOffset;
                _head.transform.rotation = Quaternion.identity;

                for (int i = 0; i < _activeSegments; i++)
                    _segments[i].enabled = false;
                return;
            }

            float maxP = n - 1f;
            float headP = Mathf.Clamp(headProgress, 0f, maxP);

            // Head placement (stable tangent)
            SamplePosAndForward(pts, headP, _tangentEpsP, out var headPos, out var headFwd);
            _head.enabled = true;
            _head.sprite = _headSprite;
            _head.transform.position = headPos + _headExtraOffset;
            _head.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(headFwd.y, headFwd.x) * Mathf.Rad2Deg);

            if (_activeSegments <= 0)
                return;

            if (_tailMode == BoardSnakeRenderer2D.TailMode.FollowHead)
            {
                // ---- CLASSIC BEHAVIOR ----
                // Tail follows headProgress; each segment is placed at headP - (i+1).
                // This is what you want for the exit/travel animation.
                for (int seg = 0; seg < _activeSegments; seg++)
                {
                    float p = headP - (seg + 1);

                    var sr = _segments[seg];
                    if (p < 0f)
                    {
                        sr.enabled = false;
                        continue;
                    }

                    sr.enabled = true;
                    sr.sprite = _straight;

                    SamplePosAndForward(pts, p, _tangentEpsP, out var pos, out var fwd);
                    sr.transform.position = pos;
                    sr.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(fwd.y, fwd.x) * Mathf.Rad2Deg);
                }

                return;
            }

            // ---- ANCHORED-TAIL BEHAVIOR ----
            // Tail stays pinned at p=0 (pts[0]) and segments redistribute toward the head.
            float tailP = 0f;
            float segHeadP = Mathf.Max(tailP, headP - _headGapP);
            float span = segHeadP - tailP;

            if (_activeSegments == 1)
            {
                var sr = _segments[0];
                sr.enabled = true;
                sr.sprite = _straight;

                SamplePosAndForward(pts, tailP, _tangentEpsP, out var pos, out var fwd);
                sr.transform.position = pos;
                sr.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(fwd.y, fwd.x) * Mathf.Rad2Deg);
                return;
            }

            int N = _activeSegments;
            for (int seg = 0; seg < N; seg++)
            {
                var sr = _segments[seg];
                sr.enabled = true;
                sr.sprite = _straight;

                float p;
                if (seg == N - 1)
                {
                    // tail piece pinned forever
                    p = tailP;
                }
                else
                {
                    // seg 0 near head, seg N-2 near tail
                    float u = (float)(N - 1 - seg) / (N - 1);
                    p = tailP + span * u;
                }

                SamplePosAndForward(pts, p, _tangentEpsP, out var pos, out var fwd);
                sr.transform.position = pos;
                sr.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(fwd.y, fwd.x) * Mathf.Rad2Deg);
            }
        }

        // ---- Sampling helpers ----

        static void SamplePos(List<Vector3> pts, float p, out Vector3 pos)
        {
            int n = pts != null ? pts.Count : 0;
            if (n <= 0)
            {
                pos = Vector3.zero;
                return;
            }

            if (n == 1)
            {
                pos = pts[0];
                return;
            }

            float maxP = n - 1f;
            float clamped = Mathf.Clamp(p, 0f, maxP);

            int i = Mathf.FloorToInt(clamped);
            float f = clamped - i;

            int j = Mathf.Min(i + 1, n - 1);
            pos = Vector3.Lerp(pts[i], pts[j], f);
        }

        static void SamplePosAndForward(List<Vector3> pts, float p, float epsP, out Vector3 pos, out Vector3 forward)
        {
            SamplePos(pts, p, out pos);

            int n = pts != null ? pts.Count : 0;
            if (n < 2)
            {
                forward = Vector3.right;
                return;
            }

            float maxP = n - 1f;
            float p0 = Mathf.Clamp(p - epsP, 0f, maxP);
            float p1 = Mathf.Clamp(p + epsP, 0f, maxP);

            SamplePos(pts, p0, out var a);
            SamplePos(pts, p1, out var b);

            Vector3 d = b - a;

            if (d.sqrMagnitude < 1e-8f)
            {
                float p2 = Mathf.Clamp(p - epsP * 2f, 0f, maxP);
                float p3 = Mathf.Clamp(p + epsP * 2f, 0f, maxP);
                SamplePos(pts, p2, out var c);
                SamplePos(pts, p3, out var e);
                d = e - c;
            }

            forward = d.sqrMagnitude < 1e-8f ? Vector3.right : d.normalized;
        }
    }
}
