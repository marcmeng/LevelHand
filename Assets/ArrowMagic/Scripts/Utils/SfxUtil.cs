using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public static class SfxUtil
    {
        public static void PlayOneShot3D(AudioClip clip, Vector3 worldPos, float volume, Vector2 pitchMinMax)
        {
            if (!clip) return;
            
            var go = new GameObject($"SFX_{clip.name}");
            var src = go.AddComponent<AudioSource>();
            src.playOnAwake = false;
            src.rolloffMode = AudioRolloffMode.Linear;
            src.minDistance = 1f;
            src.maxDistance = 12f;
            src.pitch = Random.Range(pitchMinMax.x, pitchMinMax.y);
            src.PlayOneShot(clip, volume);
            Object.Destroy(go, clip.length + 1f);
        }
    }
}