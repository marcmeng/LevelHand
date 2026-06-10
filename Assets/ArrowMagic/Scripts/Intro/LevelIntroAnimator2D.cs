using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public sealed class LevelIntroAnimator2D : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private BoardView2D boardView;
        [SerializeField] private BoardController controller;

        [Header("Settings")]
        [SerializeField] private IntroAnimationSettings settings;

        public IntroAnimationSettings CurrentSettings => settings;
        
        public event System.Action OnIntroFinished;

        private readonly List<Coroutine> _revealRoutines = new();
        private Coroutine _routine;

        private IntroEntryBuilder _builder;

        public void Play()
        {
            if (boardView == null) boardView = GetComponent<BoardView2D>();
            if (controller == null || controller.State == null) return;
            if (settings == null)
            {
                Debug.LogWarning($"{nameof(LevelIntroAnimator2D)} missing IntroAnimationSettings.");
                return;
            }

            Stop();
            _routine = StartCoroutine(Run(controller.State));
        }

        public void Stop()
        {
            if (_routine != null) StopCoroutine(_routine);
            _routine = null;

            for (int i = 0; i < _revealRoutines.Count; i++)
                if (_revealRoutines[i] != null)
                    StopCoroutine(_revealRoutines[i]);
            _revealRoutines.Clear();

            if (boardView != null)
            {
                boardView.UnhideAllForIntro();
                boardView.RedrawAll();
            }

            _builder?.CleanupOverlays();
        }

        private IEnumerator Run(BoardState s)
        {
            boardView.UnhideAllForIntro();
            boardView.SetAllDotsVisibleForIntro(true);
            boardView.RedrawAll();

            _builder ??= new IntroEntryBuilder(boardView, controller, settings, boardView.ArrowOverlayPool);

            List<IntroEntry> entries = null;

            try
            {
                // 1) Build entries
                entries = _builder.Build(s);

                // 2) Order entries
                IntroOrderer.Apply(entries, s, settings.startOrder, settings.randomSeed);

                // 3) Reveal sequencing
                _revealRoutines.Clear();
                bool doStagger = (settings.stagger > 0f);

                for (int i = 0; i < entries.Count; i++)
                {
                    var e = entries[i];

                    var co = StartCoroutine(
                        ArrowIntroRevealer.RevealOne(
                            e.overlay,
                            e.pts,
                            e.bodyLenPoints,
                            e.revealDuration,
                            settings.ease
                        )
                    );

                    _revealRoutines.Add(co);

                    if (doStagger)
                        yield return new WaitForSeconds(settings.stagger);
                }

                // wait for the longest reveal
                float maxDur = 0f;
                for (int i = 0; i < entries.Count; i++)
                    if (entries[i].revealDuration > maxDur)
                        maxDur = entries[i].revealDuration;

                if (maxDur > 0f)
                {
                    float staggerTotal = doStagger ? settings.stagger * (entries.Count - 1) : 0f;
                    yield return new WaitForSeconds(maxDur + staggerTotal);
                }
            }
            finally
            {
                _routine = null;
                _revealRoutines.Clear();

                boardView.ClearIntroDotOverrides();
                boardView.UnhideAllForIntro();
                boardView.RedrawAll();
                
                _builder?.CleanupOverlays();
                OnIntroFinished?.Invoke();
            }
        }
        
        public void SetSettings(IntroAnimationSettings s)
        {
            if (_routine != null) Stop(); // stop any running intro using old settings
            settings = s;
            _builder = null;
        }
    }
}
