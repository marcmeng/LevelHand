using System;
using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    [Serializable]
    public sealed class AuthoredLevelData
    {
        [Min(1)] public int width = 10;
        [Min(1)] public int height = 10;
        public List<AuthoredArrowData> arrows = new();
        public List<int> blockIndices = new();
    }

    [Serializable]
    public sealed class AuthoredArrowData
    {
        [Tooltip("Escape 编辑器语义：从箭头头部到尾部的格子索引。")]
        public List<int> indices = new();
        public int colorIndex;
    }
}
