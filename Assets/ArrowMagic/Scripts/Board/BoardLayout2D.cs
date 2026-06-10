using UnityEngine;

namespace PixelBug.ArrowMagic
{
    /// <summary>
    /// Pure layout math for BoardView2D: centered origin, cell-to-world conversion,
    /// and board world bounds (accounts for BoardView2D transform scale/rotation).
    /// </summary>
    [System.Serializable]
    public sealed class BoardLayout2D
    {
        [SerializeField] float _cellSize = 1.1f;
        [SerializeField] Vector2 _origin = Vector2.zero;

        /// <summary>Root transform used for local->world conversions (usually BoardView2D.transform).</summary>
        Transform _root;

        public float CellSize
        {
            get => _cellSize;
            set => _cellSize = Mathf.Max(0.0001f, value);
        }

        public Vector2 Origin
        {
            get => _origin;
            set => _origin = value;
        }

        public void BindRoot(Transform root) => _root = root;

        /// <summary>
        /// Centers the grid of cell centers around local (0,0). Works for odd/even sizes.
        /// Equivalent to BoardView2D.ComputeCenteredOrigin(s).
        /// </summary>
        public void ComputeCenteredOrigin(int width, int height)
        {
            _origin = new Vector2(
                -((width  - 1) * _cellSize) * 0.5f,
                -((height - 1) * _cellSize) * 0.5f
            );
        }

        /// <summary>
        /// Cell center (x,y) in world space. Z is always 0 in local space before transform.
        /// Equivalent to BoardView2D.CellToWorld(x,y).
        /// </summary>
        public Vector3 CellToWorld(int x, int y)
        {
            Vector3 local = new Vector3(
                _origin.x + x * _cellSize,
                _origin.y + y * _cellSize,
                0f
            );

            return (_root != null) ? _root.TransformPoint(local) : local;
        }

        public Vector3 CellToWorld(Vector2Int cell) => CellToWorld(cell.x, cell.y);

        /// <summary>
        /// Gets the board center + width/height in world units.
        /// Mirrors BoardView2D.GetBoardWorldBounds(s, out center, out w, out h).
        /// </summary>
        public void GetBoardWorldBounds(BoardState s, out Vector3 worldCenter, out float worldW, out float worldH)
        {
            float localW = s.width  * _cellSize;
            float localH = s.height * _cellSize;

            // Center of the grid in local space (center of all cell centers).
            Vector3 localCenter = new Vector3(
                _origin.x + (s.width  - 1) * _cellSize * 0.5f,
                _origin.y + (s.height - 1) * _cellSize * 0.5f,
                0f
            );

            if (_root != null)
            {
                worldCenter = _root.TransformPoint(localCenter);

                // Convert local extents into world extents (handles scaling/rotation).
                Vector3 wx = _root.TransformVector(new Vector3(localW, 0f, 0f));
                Vector3 wy = _root.TransformVector(new Vector3(0f, localH, 0f));

                worldW = wx.magnitude;
                worldH = wy.magnitude;
            }
            else
            {
                worldCenter = localCenter;
                worldW = localW;
                worldH = localH;
            }
        }
    }
}
