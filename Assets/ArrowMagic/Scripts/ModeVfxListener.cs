using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public sealed class ModeVfxListener : MonoBehaviour
    {
        [SerializeField] private VfxLibrary vfx;
        private Camera cam;

        [Header("Spawn Space")]
        [Tooltip("Outcome VFX (win/lose) are usually screen-space. If true, they spawn parented to the camera using the prefab-authored local offset.")]
        [SerializeField] private bool vfxCameraRelative = true;

        [Tooltip("Prevents accidental spam. Set 0 to disable cooldown.")]
        [SerializeField] private float debugCooldownSeconds = 0.15f;
        float _nextDebugAllowedTime;
        
        [Header("Debug VFX Test")]
        [SerializeField] private bool enableDebugVfxTest = false;

        // Use function keys so they don't collide with gameplay input.
        [SerializeField] private KeyCode testWinKey = KeyCode.UpArrow;
        [SerializeField] private KeyCode testLoseKey = KeyCode.DownArrow;

        private void Awake()
        {
            if (!cam) cam = Camera.main;
        }

        private void OnEnable()
        {
            GameEvents.LevelWon += OnWin;
            GameEvents.LevelLost += OnLose;
            
            GameEvents.ArrowBlocked += OnArrowBlocked;
            GameEvents.ArrowExit += OnArrowExit;
            GameEvents.ArrowSelected += OnArrowSelected;
        }

        private void OnDisable()
        {
            GameEvents.LevelWon -= OnWin;
            GameEvents.LevelLost -= OnLose;
            
            GameEvents.ArrowBlocked -= OnArrowBlocked;
            GameEvents.ArrowExit -= OnArrowExit;
            GameEvents.ArrowSelected -= OnArrowSelected;
        }

        void Update()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (!enableDebugVfxTest) return;

            if (debugCooldownSeconds > 0f && Time.unscaledTime < _nextDebugAllowedTime)
                return;

            if (Input.GetKeyDown(testWinKey))
            {
                OnWin();
                _nextDebugAllowedTime = Time.unscaledTime + debugCooldownSeconds;
            }
            else if (Input.GetKeyDown(testLoseKey))
            {
                OnLose();
                _nextDebugAllowedTime = Time.unscaledTime + debugCooldownSeconds;
            }
#endif
        }

        void PlayWorld(VfxLibrary.VfxEntry entry, Vector3 worldPos)
        {
            if (!vfx) return;
            if (!entry.TrySample(out var ps)) return;
            VfxUtil.Play(ps, worldPos, null);
        }

        void PlayCameraRelative(VfxLibrary.VfxEntry entry)
        {
            if (!vfx) return;
            if (!entry.TrySample(out var ps)) return;
            VfxUtil.PlayCameraRelativePrefabOffset(ps, cam);
        }

        private void OnWin()
        {
            if (vfxCameraRelative) PlayCameraRelative(vfx.win);
            else PlayWorld(vfx.win, transform.position);
        }

        private void OnLose()
        {
            if (vfxCameraRelative) PlayCameraRelative(vfx.lose);
            else PlayWorld(vfx.lose, transform.position);
        }
        
        private void OnArrowBlocked(Vector3 pos) => PlayWorld(vfx.arrowBlocked, pos);
        private void OnArrowExit(Vector3 pos) => PlayWorld(vfx.arrowExit, pos);
        private void OnArrowSelected(Vector3 pos) => PlayWorld(vfx.arrowSelected, pos);
    }
}
