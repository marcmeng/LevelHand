using System;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public static class GameEvents
    {
        // Core outcome
        public static event Action LevelWon;
        public static event Action LevelLost;

        // Arrow interactions
        public static event Action<Vector3> ArrowBlocked;         // tried to move, hit obstacle
        public static event Action<Vector3> ArrowExit;            // arrow finished exiting board

        // Input / flow
        public static event Action<Vector3> ArrowSelected;        // tap/click position
        public static event Action UndoClicked;
        public static event Action ButtonClicked;
        public static event Action Submit;
        public static event Action StartClicked;
        public static event Action NextClicked;

        // --- Raisers (use these everywhere, not direct ?.Invoke() scattered) ---
        public static void RaiseLevelWon() => LevelWon?.Invoke();
        public static void RaiseLevelLost() => LevelLost?.Invoke();
        public static void RaiseArrowBlocked(Vector3 worldPos) => ArrowBlocked?.Invoke(worldPos);
        public static void RaiseArrowExit(Vector3 worldPos) => ArrowExit?.Invoke(worldPos);

        public static void RaiseArrowSelected(Vector3 worldPos) => ArrowSelected?.Invoke(worldPos);
        public static void RaiseUndoClicked() => UndoClicked?.Invoke();
        public static void RaiseButtonClicked() => ButtonClicked?.Invoke();
        public static void RaiseSubmit() => Submit?.Invoke();
        public static void RaiseStartClicked() => StartClicked?.Invoke();
        public static void RaiseNextClicked() => NextClicked?.Invoke();
    }
}