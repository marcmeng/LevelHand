using UnityEngine;

namespace PixelBug.ArrowMagic
{
    [CreateAssetMenu(menuName = "PixelBug/Arrow Magic/Palette", fileName = "Palette")]
    public sealed class Palette : ScriptableObject
    {
        [Tooltip("Indexed by colorId. Add as many as you need.")]
        [SerializeField] private Color[] colors =
        {
            new Color(0.96f,0.26f,0.21f), // red
            new Color(0.30f,0.69f,0.31f), // green
            new Color(0.13f,0.59f,0.95f), // blue
            new Color(1.00f,0.92f,0.23f), // yellow
            new Color(0.62f,0.36f,0.71f), // purple
            new Color(1.00f,0.60f,0.00f), // orange
            new Color(0.00f,0.74f,0.83f), // teal
            new Color(0.91f,0.12f,0.39f)  // pink
        };

        public int Count => colors?.Length ?? 0;

        public Color GetColor(int colorId, bool forLiquid = true)
        {
            if (colors == null || colors.Length == 0) return Color.white;
            int i = Mathf.Abs(colorId) % colors.Length;
            var c = colors[i];
            return c;
        }

        public Color this[int index] => GetColor(index);
        
        public Color[] AsArray()
        {
            var arr = new Color[Count];
            for (int i = 0; i < Count; i++) arr[i] = GetColor(i, true);
            return arr;
        }
    }
}