using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public static class VfxUtil
    {
        /// <summary>
        /// Instantiates a ParticleSystem prefab at a world position, plays it, and auto-destroys when finished.
        /// Prefab should be set up as a root ParticleSystem (children OK). Looping systems will NOT self-destroy.
        /// </summary>
        public static void Play(ParticleSystem prefab, Vector3 worldPos, Transform parent = null)
        {
            if (!prefab) return;

            // Instantiate at requested position/rotation
            var inst = Object.Instantiate(prefab, worldPos, Quaternion.identity, parent);

            // Ensure it cleans itself up without coroutines
            var main = inst.main;
            main.stopAction = ParticleSystemStopAction.Destroy;

            // Defensive: if any child systems exist, make them also destroy on stop
            var children = inst.GetComponentsInChildren<ParticleSystem>(true);
            for (int i = 0; i < children.Length; i++)
            {
                var cm = children[i].main;
                cm.stopAction = ParticleSystemStopAction.Destroy;
            }

            // Kick it off
            inst.Play(true);

            // If someone accidentally marks it as looping, avoid immortal objects
            // (Destroy after a generous maximum duration)
            if (main.loop)
            {
                float fallbackSeconds = Mathf.Max(3f, main.duration + main.startLifetime.constantMax + 1f);
                Object.Destroy(inst.gameObject, fallbackSeconds);
            }
        }

        /// <summary>
        /// Convenience overload to play a randomized entry (like SfxEntry).
        /// </summary>
        public static void Play(VfxLibrary.VfxEntry entry, Vector3 worldPos, Transform parent = null)
        {
            if (entry.TrySample(out var ps))
                Play(ps, worldPos, parent);
        }
        
        /// Spawn at the prefab's authored world transform (pos/rot/scale) and play.
        public static ParticleSystem SpawnAtPrefabTransform(ParticleSystem prefab, Transform parent = null)
        {
            if (!prefab) return null;

            var pos = prefab.transform.position;    // world-space stored in the prefab
            var rot = prefab.transform.rotation;
            var inst = Object.Instantiate(prefab, pos, rot, parent);

            var main = inst.main;
            main.stopAction = ParticleSystemStopAction.Destroy;

            foreach (var p in inst.GetComponentsInChildren<ParticleSystem>(true))
            {
                var m = p.main; m.stopAction = ParticleSystemStopAction.Destroy;
            }

            inst.Play(true);

            if (main.loop)
            {
                float fallback = Mathf.Max(3f, main.duration + main.startLifetime.constantMax + 1f);
                Object.Destroy(inst.gameObject, fallback);
            }

            return inst;
        }

        public static ParticleSystem SpawnAtPrefabTransform(VfxLibrary.VfxEntry entry, Transform parent = null)
            => entry.TrySample(out var ps) ? SpawnAtPrefabTransform(ps, parent) : null;
        
        public static void PlayCameraRelative(ParticleSystem prefab, Vector3 worldPos, Camera cam)
        {
            if (!prefab) return;
            if (!cam) cam = Camera.main;
            if (!cam) { Play(prefab, worldPos, null); return; }

            // Convert the desired world position into camera-local space
            Vector3 localPos = cam.transform.InverseTransformPoint(worldPos);

            // Instantiate parented to the camera so it moves with it
            var inst = Object.Instantiate(prefab, cam.transform);
            inst.transform.localPosition = localPos;
            inst.transform.localRotation = Quaternion.identity;

            // Same cleanup behavior as Play()
            var main = inst.main;
            main.stopAction = ParticleSystemStopAction.Destroy;

            var children = inst.GetComponentsInChildren<ParticleSystem>(true);
            for (int i = 0; i < children.Length; i++)
            {
                var cm = children[i].main;
                cm.stopAction = ParticleSystemStopAction.Destroy;
            }

            inst.Play(true);

            if (main.loop)
            {
                float fallbackSeconds = Mathf.Max(3f, main.duration + main.startLifetime.constantMax + 1f);
                Object.Destroy(inst.gameObject, fallbackSeconds);
            }
        }
        
        public static void PlayCameraRelativePrefabOffset(ParticleSystem prefab, Camera cam, Vector3 extraLocalOffset = default)
        {
            if (!prefab) return;

            if (!cam) cam = Camera.main;
            if (!cam)
            {
                // Fallback: spawn in world at prefab's current position (best-effort)
                Play(prefab, prefab.transform.position, null);
                return;
            }

            // Instantiate parented to camera
            var inst = Object.Instantiate(prefab, cam.transform);

            // Use prefab-authored LOCAL transform as the offset/pose under camera
            inst.transform.localPosition = prefab.transform.localPosition + extraLocalOffset;
            inst.transform.localRotation = prefab.transform.localRotation;
            inst.transform.localScale    = prefab.transform.localScale;

            // Same stop/destroy behavior as your existing Play()
            var main = inst.main;
            main.stopAction = ParticleSystemStopAction.Destroy;

            var children = inst.GetComponentsInChildren<ParticleSystem>(true);
            for (int i = 0; i < children.Length; i++)
            {
                var cm = children[i].main;
                cm.stopAction = ParticleSystemStopAction.Destroy;
            }

            inst.Play(true);

            if (main.loop)
            {
                float fallbackSeconds = Mathf.Max(3f, main.duration + main.startLifetime.constantMax + 1f);
                Object.Destroy(inst.gameObject, fallbackSeconds);
            }
        }
    }
}