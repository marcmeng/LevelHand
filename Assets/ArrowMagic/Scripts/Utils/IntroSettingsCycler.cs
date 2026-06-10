using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    /// <summary>
    /// Utility for demo capture:
    /// cycles through intro presets and replays the intro with keyboard keys.
    /// Attach to the same GameObject as LevelIntroAnimator2D.
    /// </summary>
    public sealed class IntroSettingsCycler : MonoBehaviour
    {
        [Header("Board")]
        [SerializeField] private BoardController controller;
        [SerializeField] private bool clearBoardBeforeReplay = true;
        
        [Header("Refs")]
        [SerializeField] private LevelIntroAnimator2D introAnimator;

        [Header("Preset List")]
        [SerializeField] private List<IntroAnimationSettings> presets = new();

        [SerializeField] private int currentIndex = 0;

        [Header("Keys")]
        [SerializeField] private KeyCode nextKey = KeyCode.RightBracket; // ]
        [SerializeField] private KeyCode prevKey = KeyCode.LeftBracket;  // [
        [SerializeField] private KeyCode replayKey = KeyCode.Backslash;  // \

        [Header("Behavior")]
        [SerializeField] private bool applyOnStart = true;
        [SerializeField] private bool autoReplayOnCycle = true;
        [SerializeField] private bool wrap = true;
        [SerializeField] private bool logSelection = true;
        
        [Header("Auto Cycle")]
        [SerializeField] private bool autoCycle = false;
        [SerializeField] private float autoCycleInterval = 4f;

        float _autoCycleTimer;

        public int CurrentIndex => currentIndex;
        public IReadOnlyList<IntroAnimationSettings> Presets => presets;

        private void Awake()
        {
            if (introAnimator == null)
                introAnimator = GetComponent<LevelIntroAnimator2D>();
            
            if (controller == null)
                controller = GetComponent<BoardController>();
        }

        private void Start()
        {
            _autoCycleTimer = autoCycleInterval;
            if (!applyOnStart) return;
            ApplyCurrent(autoReplayOnCycle);
        }

        private void Update()
        {
            if (presets == null || presets.Count == 0 || introAnimator == null)
                return;

            if (Input.GetKeyDown(nextKey))
            {
                Step(1);
            }
            else if (Input.GetKeyDown(prevKey))
            {
                Step(-1);
            }
            else if (Input.GetKeyDown(replayKey))
            {
                ReplayCurrent();
            }
            if (autoCycle)
            {
                _autoCycleTimer -= Time.deltaTime;

                if (_autoCycleTimer <= 0f)
                {
                    Step(1);
                    _autoCycleTimer = autoCycleInterval;
                }
            }
        }

        public void Step(int delta)
        {
            if (presets == null || presets.Count == 0)
                return;

            int next = currentIndex + delta;

            if (wrap)
            {
                if (next < 0) next = presets.Count - 1;
                if (next >= presets.Count) next = 0;
            }
            else
            {
                next = Mathf.Clamp(next, 0, presets.Count - 1);
            }

            if (next == currentIndex && !wrap)
                return;

            currentIndex = next;
            ApplyCurrent(autoReplayOnCycle);
        }

        public void SetIndex(int index, bool replay = true)
        {
            if (presets == null || presets.Count == 0)
                return;

            currentIndex = Mathf.Clamp(index, 0, presets.Count - 1);
            ApplyCurrent(replay);
        }

        public void ReplayCurrent()
        {
            ApplyCurrent(true);
        }

        private void ApplyCurrent(bool replay)
        {
            if (introAnimator == null) return;
            if (presets == null || presets.Count == 0) return;

            var preset = presets[Mathf.Clamp(currentIndex, 0, presets.Count - 1)];
            if (preset == null) return;
            
            if (clearBoardBeforeReplay && controller != null)
            {
                controller.Restart(controller.CurrentSeed);
            }

            introAnimator.SetSettings(preset);

            if (logSelection)
                Debug.Log($"[IntroSettingsCycler] Preset {currentIndex + 1}/{presets.Count}: {preset.name}");

            if (replay)
                introAnimator.Play();
        }
    }
}