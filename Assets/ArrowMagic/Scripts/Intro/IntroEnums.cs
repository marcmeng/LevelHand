using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public enum IntroStartOrder
    {
        BottomToTop, // ↑
        TopToBottom, // ↓
        LeftToRight, // →
        RightToLeft, // ←

        CenterToEdge,
        EdgeToCenter,

        DiagonalBLToUR, // ↗
        DiagonalULToBR, // ↘
        DiagonalBRToUL, // ↖
        DiagonalURToBL, // ↙

        ByArrowLength_ShortToLong,
        ByArrowLength_LongToShort,

        ByColorHue,
        ByColorIndex,

        CheckerboardWave,

        Random,
    }

    public enum IntroTimingMode
    {
        Duration,
        Speed
    }
}