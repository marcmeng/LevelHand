using UnityEngine;

namespace PixelBug.ArrowMagic
{
    [CreateAssetMenu(menuName = "PixelBug/Arrow Magic/VFX Library", fileName = "VfxLibrary")]
    public sealed class VfxLibrary : ScriptableObject
    {
        [System.Serializable]
        public struct VfxEntry
        {
            public ParticleSystem[] pss;
            public bool HasSystems => pss != null && pss.Length > 0;

            public bool TrySample(out ParticleSystem ps)
            {
                ps = null;
                if (!HasSystems) return false;
                int idx = (pss.Length == 1) ? 0 : Random.Range(0, pss.Length);
                ps = pss[idx];
                return ps != null;
            }
        }

        [Header("Outcome")]
        public VfxEntry win;
        public VfxEntry lose;

        [Header("Arrows")]
        public VfxEntry arrowBlocked;
        public VfxEntry arrowExit;
        public VfxEntry arrowSelected;
    }
}