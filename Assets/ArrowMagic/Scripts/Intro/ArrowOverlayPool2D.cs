using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    /// <summary>
    /// One pool for ALL arrow overlays:
    /// - Intro reveal overlays
    /// - Board snake overlays
    /// - Selection / Ghost overlays
    ///
    /// Backed by ArrowSnakeOverlay2D as the one concrete implementation.
    /// </summary>
    public sealed class ArrowOverlayPool2D
    {
        readonly Transform _parent;
        readonly Stack<ArrowSnakeOverlay2D> _pool = new();

        Sprite _straight;
        Sprite _head;

        public ArrowOverlayPool2D(Transform parent)
        {
            _parent = parent;
        }

        public void ConfigureSprites(Sprite straight, Sprite head)
        {
            _straight = straight;
            _head = head;
        }

        public ArrowSnakeOverlay2D Acquire(int sortingOrderBase)
        {
            ArrowSnakeOverlay2D o = null;
            while (_pool.Count > 0 && o == null)
                o = _pool.Pop();

            if (o == null)
                o = new ArrowSnakeOverlay2D(_parent, sortingOrderBase);
            else
                o.SetSortingOrderBase(sortingOrderBase);

            // Always re-apply sprites in case settings changed.
            o.ConfigureSprites(_straight, _head);
            o.SetVisible(true);

            return o;
        }

        public void Release(ArrowSnakeOverlay2D o)
        {
            if (o == null) return;
            o.ResetForPoolReuse();
            _pool.Push(o);
        }

        public void DestroyAllPooled()
        {
            while (_pool.Count > 0)
            {
                var o = _pool.Pop();
                if (o != null) o.Destroy();
            }
        }
    }
}
