using UnityEngine;

namespace PixelBug.ArrowMagic
{
    [CreateAssetMenu(menuName = "PixelBug/Arrow Magic/SFX Library", fileName = "SfxLibrary")]
    public sealed class SfxLibrary : ScriptableObject
    {
        [System.Serializable]
        public struct SfxEntry
        {
            [Tooltip("One will be chosen at random when played.")]
            public AudioClip[] clips;

            [Range(0f, 1f)] public float volume;
            [Tooltip("Randomized pitch each play.")]
            public Vector2 pitchRange;

            public bool HasClips => clips != null && clips.Length > 0;

            public bool TrySample(out AudioClip clip, out float vol, out float pitch)
            {
                clip = null; vol = 1f; pitch = 1f;
                if (!HasClips) return false;

                int idx = (clips.Length == 1) ? 0 : Random.Range(0, clips.Length);
                clip = clips[idx];
                vol = Mathf.Clamp01(volume);

                float minP = Mathf.Min(pitchRange.x, pitchRange.y);
                float maxP = Mathf.Max(pitchRange.x, pitchRange.y);
                pitch = (minP == 0f && maxP == 0f) ? 1f : Random.Range(minP, maxP);

                return clip != null;
            }
        }

        [Header("Outcome")]
        public SfxEntry win;
        public SfxEntry lose;

        [Header("Arrows")]
        public SfxEntry arrowBlocked;
        public SfxEntry arrowExit;
        public SfxEntry arrowSelected;

        [Header("UI")]
        public SfxEntry undo;
        public SfxEntry click;
        public SfxEntry submit;
        public SfxEntry start;
        public SfxEntry next;
    }
}