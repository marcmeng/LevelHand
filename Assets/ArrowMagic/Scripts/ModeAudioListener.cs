using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public sealed class ModeAudioListener : MonoBehaviour
    {
        [SerializeField] private SfxLibrary sfx;

        private void OnEnable()
        {
            GameEvents.LevelWon += OnWin;
            GameEvents.LevelLost += OnLose;
            
            GameEvents.ArrowBlocked += OnArrowBlocked;
            GameEvents.ArrowExit += OnArrowExit;
            GameEvents.ArrowSelected += OnArrowSelected;

            GameEvents.UndoClicked += OnUndo;
            GameEvents.ButtonClicked += OnClick;
            GameEvents.Submit += OnSubmit;
            GameEvents.StartClicked += OnStart;
            GameEvents.NextClicked += OnNext;
        }

        private void OnDisable()
        {
            GameEvents.LevelWon -= OnWin;
            GameEvents.LevelLost -= OnLose;
            
            GameEvents.ArrowBlocked -= OnArrowBlocked;
            GameEvents.ArrowExit -= OnArrowExit;
            GameEvents.ArrowSelected -= OnArrowSelected;

            GameEvents.UndoClicked -= OnUndo;
            GameEvents.ButtonClicked -= OnClick;
            GameEvents.Submit -= OnSubmit;
            GameEvents.StartClicked -= OnStart;
            GameEvents.NextClicked -= OnNext;
        }

        private void Play(SfxLibrary.SfxEntry entry, Vector3 worldPos)
        {
            if (!sfx) return;
            if (!entry.TrySample(out var clip, out var vol, out var pitch)) return;
            SfxUtil.PlayOneShot3D(clip, worldPos, vol, new Vector2(pitch, pitch));
        }

        private void OnWin() => Play(sfx.win, transform.position);
        private void OnLose() => Play(sfx.lose, transform.position);
        
        private void OnArrowBlocked(Vector3 pos) => Play(sfx.arrowBlocked, pos);
        private void OnArrowExit(Vector3 pos) => Play(sfx.arrowExit, pos);
        private void OnArrowSelected(Vector3 pos) => Play(sfx.arrowSelected, pos);

        private void OnUndo() => Play(sfx.undo, transform.position);
        private void OnClick() => Play(sfx.click, transform.position);
        private void OnSubmit() => Play(sfx.submit, transform.position);
        private void OnStart() => Play(sfx.start, transform.position);
        private void OnNext() => Play(sfx.next, transform.position);
    }
}
