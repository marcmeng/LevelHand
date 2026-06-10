using System;
using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    /// <summary>
    /// Centralized color logic for the board:
    /// - owns the available arrow palettes for this board
    /// - tracks the active palette/index
    /// - builds stable per-chain colors (consistent across moves)
    /// - maintains permanent per-tile overrides (e.g. blocked/white arrows)
    /// </summary>
    public sealed class BoardColorService2D : MonoBehaviour
    {
        [Header("Arrow Palette")]
        [SerializeField] Palette currentPalette;
        
        [Header("Blocked / Hit Tint")]
        [SerializeField] bool tintOnHit = true;
        [SerializeField] Color hitTint = Color.white;

        public bool TintOnHit => tintOnHit;
        public Color HitTint => hitTint;
        
        public Palette CurrentPalette => currentPalette;

        public event Action<Palette> PaletteChanged;

        BoardController _controller;

        Dictionary<int, Color> _stableArrowColors;
        readonly Dictionary<ulong, Color> _chainColorByKey = new();
        int _nextPaletteIndex;

        // Per-tile permanent overrides (wins over stable colors)
        readonly Dictionary<int, Color> _permanentTileColors = new();
        public Dictionary<int, Color> PermanentTileColors => _permanentTileColors;

        public void Init(BoardController controller)
        {
            _controller = controller;
            RebuildStableArrowColors();
        }

        // public void SetPaletteByIndex(int index, bool rebuild = true)
        // {
        //     if (palettes == null || palettes.Length == 0)
        //         return;
        //
        //     index = Mathf.Clamp(index, 0, palettes.Length - 1);
        //     SetPaletteInternal(palettes[index], index, rebuild);
        // }

        public void SetPalette(Palette palette, bool rebuild = true)
        {
            if (currentPalette == palette)
                return;

            currentPalette = palette;

            if (rebuild)
                RebuildStableArrowColors();

            PaletteChanged?.Invoke(currentPalette);
        }
        
        // public void NextPalette(bool rebuild = true)
        // {
        //     if (palettes == null || palettes.Length == 0)
        //         return;
        //
        //     int nextIndex = (CurrentPaletteIndex + 1 + palettes.Length) % palettes.Length;
        //     SetPaletteByIndex(nextIndex, rebuild);
        // }
        //
        // public void PreviousPalette(bool rebuild = true)
        // {
        //     if (palettes == null || palettes.Length == 0)
        //         return;
        //
        //     int prevIndex = (CurrentPaletteIndex - 1 + palettes.Length) % palettes.Length;
        //     SetPaletteByIndex(prevIndex, rebuild);
        // }
        
        // void SetPaletteInternal(Palette palette, int index, bool rebuild)
        // {
        //     if (palette == null)
        //         return;
        //
        //     if (palette == CurrentPalette && index == CurrentPaletteIndex)
        //         return;
        //
        //     CurrentPalette = palette;
        //     CurrentPaletteIndex = index;
        //
        //     if (rebuild)
        //         RebuildStableArrowColors();
        //
        //     PaletteChanged?.Invoke(CurrentPalette);
        // }

        public void ClearAllPermanentOverrides()
        {
            _permanentTileColors.Clear();
        }

        public void ClearPermanentOverride(int idx)
        {
            _permanentTileColors.Remove(idx);
        }

        public void SetPermanentOverride(int idx, Color c)
        {
            _permanentTileColors[idx] = c;
        }

        public bool TryGetStableColor(int idx, out Color c)
        {
            c = default;
            return _stableArrowColors != null && _stableArrowColors.TryGetValue(idx, out c);
        }

        public Color GetTileColor(int idx)
        {
            if (_permanentTileColors.TryGetValue(idx, out var c))
                return c;

            if (_stableArrowColors != null && _stableArrowColors.TryGetValue(idx, out c))
                return c;

            return Color.white;
        }
        
        public void ApplyArrowColorMaskFromImage(
            Texture2D mask,
            float alphaThreshold,
            int quantizeSteps)
        {
            if (_controller == null || _controller.State == null) return;
            if (mask == null) return;

            var s = _controller.State;

            var pix = mask.GetPixels32();
            int tw = mask.width;
            int th = mask.height;

            quantizeSteps = Mathf.Clamp(quantizeSteps, 1, 64);

            for (int y = 0; y < s.height; y++)
            for (int x = 0; x < s.width; x++)
            {
                int idx = x + y * s.width;

                if (s.tiles[idx].type != TileType.Arrow)
                    continue;

                int px = Mathf.Clamp(x, 0, tw - 1);
                int py = Mathf.Clamp(y, 0, th - 1);

                var c32 = pix[px + py * tw];
                float af = c32.a / 255f;

                if (af <= alphaThreshold)
                    continue;

                float r = c32.r / 255f;
                float g = c32.g / 255f;
                float b = c32.b / 255f;

                if (quantizeSteps > 1)
                {
                    r = Quantize01(r, quantizeSteps);
                    g = Quantize01(g, quantizeSteps);
                    b = Quantize01(b, quantizeSteps);
                }

                SetPermanentOverride(idx, new Color(r, g, b, 1f));
            }
        }

        static float Quantize01(float v, int steps)
        {
            if (steps <= 1) return Mathf.Clamp01(v);
            
            float s = steps - 1;
            float q = Mathf.Round(Mathf.Clamp01(v) * s) / s;
            return q;
        }

        public void RebuildStableArrowColors()
        {
            if (_controller == null || _controller.State == null)
            {
                _stableArrowColors = null;
                _chainColorByKey.Clear();
                _nextPaletteIndex = 0;
                return;
            }

            var s = _controller.State;

            _stableArrowColors = new Dictionary<int, Color>(s.tiles.Length);
            _chainColorByKey.Clear();
            _nextPaletteIndex = 0;

            var visited = new HashSet<int>();
            var chain = new HashSet<int>();

            for (int i = 0; i < s.tiles.Length; i++)
            {
                if (visited.Contains(i)) continue;
                if (s.tiles[i].type != TileType.Arrow) continue;

                int x = i % s.width;
                int y = i / s.width;

                chain.Clear();
                ArrowChainUtility.CollectFullChain(
                    s, new Vector2Int(x, y), 0, chain);

                if (chain.Count == 0) continue;

                foreach (int idx in chain) visited.Add(idx);

                ulong key = ComputeChainKey(chain);

                if (!_chainColorByKey.TryGetValue(key, out var color))
                {
                    var palette = ResolveArrowPalette();
                    
                    if (palette == null || palette.Length == 0)
                        color = Color.white;
                    else
                    {
                        color = palette[_nextPaletteIndex % palette.Length];
                        _nextPaletteIndex++;
                    }

                    _chainColorByKey[key] = color;
                }

                foreach (int idx in chain)
                    _stableArrowColors[idx] = color;
            }
        }

        Color[] ResolveArrowPalette()
        {
            if (CurrentPalette != null && CurrentPalette.Count > 0)
                return CurrentPalette.AsArray();

            return null;
        }


        static ulong ComputeChainKey(HashSet<int> chain)
        {
            var arr = new int[chain.Count];
            int k = 0;
            foreach (var v in chain) arr[k++] = v;
            Array.Sort(arr);

            const ulong offset = 1469598103934665603UL;
            const ulong prime = 1099511628211UL;

            ulong h = offset;
            for (int i = 0; i < arr.Length; i++)
            {
                unchecked
                {
                    h ^= (ulong)arr[i];
                    h *= prime;
                }
            }
            return h;
        }
        
        public void ApplyHitTintSettings(bool enableTint, Color tint)
        {
            tintOnHit = enableTint;
            hitTint = tint;
        }

    }
}
