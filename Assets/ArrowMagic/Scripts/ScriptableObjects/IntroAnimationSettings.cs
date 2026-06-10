using UnityEngine;

namespace PixelBug.ArrowMagic
{
    [CreateAssetMenu(menuName = "PixelBug/Arrow Magic/Intro Animation Settings", fileName = "IntroAnimationSettings")]
    public sealed class IntroAnimationSettings : ScriptableObject
    {
        [Header("Timing")]
        public IntroTimingMode timingMode = IntroTimingMode.Duration;

        [Min(0.02f)]
        public float perArrowDuration = 0.35f;

        [Tooltip("Used when Timing Mode = Speed (world units per second along the sampled path).")]
        [Min(0.0001f)]
        public float revealSpeed = 12f;

        [Min(0f)]
        public float stagger = 0.01f;

        public AnimationCurve ease = AnimationCurve.EaseInOut(0, 0, 1, 1);

        [Header("Start Order")]
        public IntroStartOrder startOrder = IntroStartOrder.BottomToTop;

        [Tooltip("If 0, a new seed is picked each Play(). If non-zero, Random order is deterministic.")]
        public int randomSeed = 0;

        [Header("Overlay")]
        public int overlaySortingOrderBase = 200;

        [Tooltip("Fallback bucket count when we can't map palette index.")]
        [Min(1)]
        public int hueBucketsFallback = 12;
    }
}