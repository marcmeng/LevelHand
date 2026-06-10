using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace PixelBug.ArrowMagic
{
    public sealed class ArrowMagicHudUITK : MonoBehaviour
    {
        [SerializeField] UIDocument uiDoc;
        [SerializeField] BoardController controller;
        [SerializeField] IntroSettingsCycler introCycler;

        [Header("UXML element names")]
        [SerializeField] string undoButtonName = "undoButton";
        [SerializeField] string restartButtonName = "restartButton";
        [SerializeField] string newButtonName = "newButton";
        [SerializeField] string movesLabelName = "movesLabel";
        [SerializeField] string statusLabelName = "statusLabel";
        [SerializeField] string blockedLabelName = "blockedLabel";
        [SerializeField] string heartsLabelName = "heartsLabel";
        
        [SerializeField] string heartsContainerName = "heartsContainer";
        
        [SerializeField] string seedMinusButtonName = "seedMinusButton";
        [SerializeField] string seedPlusButtonName = "seedPlusButton";
        [SerializeField] string seedFieldName = "seedField";

        [Header("Editor Mode UI")]
        [Tooltip("A single toggle button: shows 'Edit' when off and 'Done' when on.")]
        [SerializeField] string editToggleButtonName = "editButton";
        [SerializeField] string saveButtonName = "saveButton";

        [Tooltip("Optional label to show 'EDIT MODE' (leave blank to skip).")]
        [SerializeField] string editModeLabelName = "editModeLabel";
        
        [Header("Hearts")]
        [Tooltip("A single white heart Sprite. We will tint it for full/empty states.")]
        [SerializeField] Sprite heartSprite;
        
        [SerializeField] Color heartFullTint = Color.white;
        [SerializeField] Color heartEmptyTint = new Color(1f, 1f, 1f, 0.25f);

        [Tooltip("Heart icon size in pixels.")]
        [SerializeField] float heartSizePx = 22f;

        [Tooltip("Horizontal spacing between hearts in pixels.")]
        [SerializeField] float heartSpacingPx = 6f;
        
        [SerializeField] string nextButtonName = "nextButton";
        [SerializeField] string skipButtonName = "skipButton";
        [SerializeField] LevelProgression progression;
        
        [SerializeField] string levelNumberLabelName = "levelNumberLabel";
        
        [SerializeField] string introPresetLabelName = "introPresetLabel";
        [SerializeField] string modeLabelName = "modeLabel";

        Button _undoBtn, _restartBtn, _newBtn, _nextBtn, _skipBtn, _saveBtn, _editBtn;
        Label _movesLabel, _statusLabel, _blockedLabel, _heartsLabel, _editModeLabel, _levelNumberLabel, _introPresetLabel, _modeLabel;
        
        Button _seedMinusBtn, _seedPlusBtn;
        IntegerField _seedField;
        int _seedEditValue;
        bool _suppressSeedCallbacks;
        bool _showOutcomeButton;
        bool _showWinOutcome;
        bool _showLoseOutcome;
        
        VisualElement _heartsContainer;
        readonly List<Image> _heartImages = new List<Image>(16);
        int _lastHeartMax = -1;

        System.Action<MoveDelta> _onDelta;

        void Awake()
        {
            // IMPORTANT: don’t use lambdas for event unsubs; keep a handler instance.
            _onDelta = _ => Refresh();
        }
        
        void OnLevelWonEvent()
        {
            _showOutcomeButton = true;
            _showWinOutcome = true;
            _showLoseOutcome = false;
            Refresh();
        }

        void OnLevelLostEvent()
        {
            _showOutcomeButton = true;
            _showWinOutcome = false;
            _showLoseOutcome = true;
            Refresh();
        }

        void ResetOutcomeButtonLatch()
        {
            _showOutcomeButton = false;
            _showWinOutcome = false;
            _showLoseOutcome = false;
        }

        void OnEnable()
        {
            GameEvents.LevelWon += OnLevelWonEvent;
            GameEvents.LevelLost += OnLevelLostEvent;
            
            if (uiDoc == null) uiDoc = GetComponent<UIDocument>();
            Bind();
            
            // Auto-find if not wired in inspector
            if (controller == null) controller = FindFirstObjectByType<BoardController>();
            if (progression == null) progression = FindFirstObjectByType<LevelProgression>();
            if (introCycler == null)
                introCycler = FindFirstObjectByType<IntroSettingsCycler>();
            
            if (controller != null)
            {
                controller.OnRestart += Refresh;
                controller.OnDeltaApplied += _onDelta;
                controller.OnWin += Refresh;
                controller.OnLose += Refresh;
                controller.OnBlockedChanged += Refresh;

                // Editor mode UI refresh
                controller.OnEditorModeChanged += OnEditorModeChanged;
            }

            Refresh();
        }

        void OnDisable()
        {
            GameEvents.LevelWon -= OnLevelWonEvent;
            GameEvents.LevelLost -= OnLevelLostEvent;
            
            if (controller != null)
            {
                controller.OnRestart -= Refresh;
                controller.OnDeltaApplied -= _onDelta;
                controller.OnWin -= Refresh;
                controller.OnLose -= Refresh;
                controller.OnBlockedChanged -= Refresh;

                controller.OnEditorModeChanged -= OnEditorModeChanged;
            }

            // Avoid dangling callbacks in UI Toolkit
            _undoBtn?.UnregisterCallback<ClickEvent>(OnUndoClicked);
            _restartBtn?.UnregisterCallback<ClickEvent>(OnRestartClicked);
            _newBtn?.UnregisterCallback<ClickEvent>(OnNewClicked);
            _nextBtn?.UnregisterCallback<ClickEvent>(OnNextClicked);
            _skipBtn?.UnregisterCallback<ClickEvent>(OnSkipClicked);
            _editBtn?.UnregisterCallback<ClickEvent>(OnEditToggleClicked);
            _saveBtn?.UnregisterCallback<ClickEvent>(OnSaveClicked);
            
            _seedMinusBtn?.UnregisterCallback<ClickEvent>(OnSeedMinusClicked);
            _seedPlusBtn?.UnregisterCallback<ClickEvent>(OnSeedPlusClicked);
            _seedField?.UnregisterValueChangedCallback(OnSeedFieldValueChanged);
            _seedField?.UnregisterCallback<FocusOutEvent>(OnSeedFieldFocusOut);
            _seedField?.UnregisterCallback<KeyDownEvent>(OnSeedFieldKeyDown);
        }

        void Bind()
        {
            if (uiDoc == null) return;
            var root = uiDoc.rootVisualElement;
            if (root == null) return;

            _undoBtn = root.Q<Button>(undoButtonName);
            _restartBtn = root.Q<Button>(restartButtonName);
            _newBtn = root.Q<Button>(newButtonName);
            _nextBtn = root.Q<Button>(nextButtonName);
            _skipBtn = root.Q<Button>(skipButtonName);
            if (_nextBtn != null)
            {
                _nextBtn.RemoveFromClassList("show");
                _nextBtn.pickingMode = PickingMode.Ignore;
            }
            _editBtn = root.Q<Button>(editToggleButtonName);
            _saveBtn = root.Q<Button>(saveButtonName);
            
            _seedMinusBtn = root.Q<Button>(seedMinusButtonName);
            _seedPlusBtn  = root.Q<Button>(seedPlusButtonName);
            _seedField    = root.Q<IntegerField>(seedFieldName);

            _movesLabel = root.Q<Label>(movesLabelName);
            _statusLabel = root.Q<Label>(statusLabelName);
            _blockedLabel = root.Q<Label>(blockedLabelName);
            _heartsLabel = root.Q<Label>(heartsLabelName);
            _heartsContainer = root.Q<VisualElement>(heartsContainerName);
            _levelNumberLabel = root.Q<Label>(levelNumberLabelName);
            _introPresetLabel = root.Q<Label>(introPresetLabelName);
            _modeLabel = root.Q<Label>(modeLabelName);

            _editModeLabel = string.IsNullOrEmpty(editModeLabelName) ? null : root.Q<Label>(editModeLabelName);

            // Clear previous handlers (domain reload quirks)
            _undoBtn?.UnregisterCallback<ClickEvent>(OnUndoClicked);
            _restartBtn?.UnregisterCallback<ClickEvent>(OnRestartClicked);
            _newBtn?.UnregisterCallback<ClickEvent>(OnNewClicked);
            _nextBtn?.UnregisterCallback<ClickEvent>(OnNextClicked);
            _skipBtn?.UnregisterCallback<ClickEvent>(OnSkipClicked);
            _editBtn?.UnregisterCallback<ClickEvent>(OnEditToggleClicked);
            _saveBtn?.UnregisterCallback<ClickEvent>(OnSaveClicked);
            
            _seedMinusBtn?.UnregisterCallback<ClickEvent>(OnSeedMinusClicked);
            _seedPlusBtn?.UnregisterCallback<ClickEvent>(OnSeedPlusClicked);
            _seedField?.UnregisterValueChangedCallback(OnSeedFieldValueChanged);
            _seedField?.UnregisterCallback<FocusOutEvent>(OnSeedFieldFocusOut);
            _seedField?.UnregisterCallback<KeyDownEvent>(OnSeedFieldKeyDown);

            _undoBtn?.RegisterCallback<ClickEvent>(OnUndoClicked);
            _restartBtn?.RegisterCallback<ClickEvent>(OnRestartClicked);
            _newBtn?.RegisterCallback<ClickEvent>(OnNewClicked);
            _nextBtn?.RegisterCallback<ClickEvent>(OnNextClicked);
            _skipBtn?.RegisterCallback<ClickEvent>(OnSkipClicked);
            _editBtn?.RegisterCallback<ClickEvent>(OnEditToggleClicked);
            _saveBtn?.RegisterCallback<ClickEvent>(OnSaveClicked);
            
            _seedMinusBtn?.RegisterCallback<ClickEvent>(OnSeedMinusClicked);
            _seedPlusBtn?.RegisterCallback<ClickEvent>(OnSeedPlusClicked);
            _seedField?.RegisterValueChangedCallback(OnSeedFieldValueChanged);
            _seedField?.RegisterCallback<FocusOutEvent>(OnSeedFieldFocusOut);
            _seedField?.RegisterCallback<KeyDownEvent>(OnSeedFieldKeyDown);
        }
        
        void OnSeedMinusClicked(ClickEvent _)
        {
            if (controller == null) return;

            int newSeed = controller.CurrentSeed - 1;
            SetSeedFieldWithoutNotify(newSeed);
            ApplySeed(newSeed, fastPreview: true);
            GameEvents.RaiseButtonClicked();
        }

        void OnSeedPlusClicked(ClickEvent _)
        {
            if (controller == null) return;

            int newSeed = controller.CurrentSeed + 1;
            SetSeedFieldWithoutNotify(newSeed);
            ApplySeed(newSeed, fastPreview: true);
            GameEvents.RaiseButtonClicked();
        }

        void OnSeedFieldValueChanged(ChangeEvent<int> evt)
        {
            if (_suppressSeedCallbacks) return;
            _seedEditValue = evt.newValue;
        }

        void OnSeedFieldFocusOut(FocusOutEvent _)
        {
            CommitSeedField();
        }

        void OnSeedFieldKeyDown(KeyDownEvent evt)
        {
            if (evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.KeypadEnter)
            {
                CommitSeedField();
                evt.StopPropagation();
            }
        }

        void CommitSeedField()
        {
            if (controller == null) return;
            if (_seedEditValue == controller.CurrentSeed) return;

            ApplySeed(_seedEditValue);
            GameEvents.RaiseSubmit();
        }

        void ApplySeed(int seed, bool fastPreview = false)
        {
            if (controller == null) return;
            if (seed == controller.CurrentSeed) return;

            ResetOutcomeButtonLatch();
            if (fastPreview)
                controller.RestartFastPreview(seed);
            else
                controller.Restart(seed);
            Refresh();
        }

        void SetSeedFieldWithoutNotify(int seed)
        {
            if (_seedField == null) return;

            _suppressSeedCallbacks = true;
            _seedField.value = seed;
            _seedEditValue = seed;
            _suppressSeedCallbacks = false;
        }

        void OnEditorModeChanged(bool _)
        {
            Refresh();
        }

        void OnUndoClicked(ClickEvent _)
        {
            controller?.Undo();
            GameEvents.RaiseUndoClicked();
            Refresh();
        }

        void OnRestartClicked(ClickEvent _)
        {
            if (controller == null) return;
            
            ResetOutcomeButtonLatch();
            controller.Restart(controller.CurrentSeed);
            GameEvents.RaiseStartClicked();
            Refresh();
        }

        void OnNewClicked(ClickEvent _)
        {
            ResetOutcomeButtonLatch();
            controller?.NewGame();
            GameEvents.RaiseNextClicked();
            Refresh();
        }
        
        void OnNextClicked(ClickEvent _)
        {
            if (controller == null) return;
            GameEvents.RaiseNextClicked();

            // Win/progression button should do nothing while editing.
            if (controller.EditorMode) return;
            
            ResetOutcomeButtonLatch();
            
            // If lost, always replay current level.
            if (controller.Lost)
            {
                controller.Restart(controller.CurrentSeed);
                return;
            }

            // If there is a progression controller, let it decide:
            if (progression != null)
            {
                progression.OnWinButtonPressed();
                return;
            }

            // No progression in scene => Replay (restart current level)
            controller.Restart(controller.CurrentSeed);
        }

        void OnSkipClicked(ClickEvent _)
        {
            if (controller == null) return;
            if (controller.EditorMode) return;

            ResetOutcomeButtonLatch();
            GameEvents.RaiseNextClicked();

            if (progression != null && progression.LevelCount > 0)
            {
                progression.SkipCurrentLevel();
            }
            else
            {
                controller.NewGame();
            }

            Refresh();
        }
        
        void OnSaveClicked(ClickEvent _)
        {
            if (controller == null) return;
            if (!controller.EditorMode) return;
            Debug.Log("Save button clicked in editor mode. Saving level...");
            controller.SaveEditedLevel();
            Refresh();
        }

        void OnEditToggleClicked(ClickEvent _)
        {
            if (controller == null) return;

            bool newState = !controller.EditorMode;
            controller.SetEditorMode(newState);

            Refresh();
        }

        void Refresh()
        {
            if (controller == null)
            {
                if (_movesLabel != null) _movesLabel.text = "Moves: -";
                if (_statusLabel != null) _statusLabel.text = "No controller";
                if (_blockedLabel != null) _blockedLabel.text = "Blocked: -";
                if (_heartsLabel != null) _heartsLabel.text = "Hearts: -";
                if (_levelNumberLabel != null) _levelNumberLabel.text = "";
                if (_editBtn != null) _editBtn.text = "Edit";
                if (_editModeLabel != null) _editModeLabel.text = "";
                if (_nextBtn != null)
                {
                    _nextBtn.RemoveFromClassList("show");
                    _nextBtn.pickingMode = PickingMode.Ignore;
                }
                return;
            }
            
            // Keep progression ref fresh if scene reloads, etc.
            if (progression == null) progression = FindFirstObjectByType<LevelProgression>();
            
            SetSeedFieldWithoutNotify(controller.CurrentSeed);
            
            bool editing = controller.EditorMode;
            
            // -------------------------
            // Win / Lose / Next / Replay button
            // -------------------------
            if (_nextBtn != null)
            {
                bool won = !editing && controller.Won && _showOutcomeButton && _showWinOutcome;
                bool lost = !editing && controller.Lost && _showOutcomeButton && _showLoseOutcome;
                bool showButton = won || lost;

                if (lost)
                {
                    _nextBtn.text = "Replay";
                }
                else if (won)
                {
                    if (progression != null)
                        _nextBtn.text = progression.GetWinButtonLabel();
                    else
                        _nextBtn.text = "Replay";
                }

                if (showButton)
                {
                    _nextBtn.AddToClassList("show");
                    _nextBtn.pickingMode = PickingMode.Position;
                }
                else
                {
                    _nextBtn.RemoveFromClassList("show");
                    _nextBtn.pickingMode = PickingMode.Ignore;
                }
            }
            
            // -------------------------
            // Intro preset display
            // -------------------------
            if (_introPresetLabel != null)
            {
                if (introCycler != null && introCycler.Presets.Count > 0)
                {
                    var preset = introCycler.Presets[introCycler.CurrentIndex];
                    string name = preset != null ? preset.name : "None";
                    _introPresetLabel.text = $"{name}";
                }
                else
                {
                    _introPresetLabel.text = "";
                }
            }
            
            // -------------------------
            // Intro timing mode display
            // -------------------------
            if (_modeLabel != null)
            {
                if (introCycler != null && introCycler.Presets.Count > 0)
                {
                    var preset = introCycler.Presets[introCycler.CurrentIndex];

                    if (preset != null)
                        _modeLabel.text = preset.timingMode.ToString() + " mode";
                    else
                        _modeLabel.text = "";
                }
                else
                {
                    _modeLabel.text = "";
                }
            }


            // Labels
            if (_movesLabel != null)
                _movesLabel.text = editing ? "Moves: (editing)" : $"Moves: {controller.MoveCount}";
            
            if (_levelNumberLabel != null)
            {
                if (progression != null)
                    _levelNumberLabel.text = $"Level {progression.CurrentLevelNumber}";
                else
                    _levelNumberLabel.text = "Level 1";
            }

            if (_statusLabel != null)
            {
                if (editing)
                {
                    _statusLabel.text = $"EDIT MODE — Seed: {controller.CurrentSeed}";
                }
                else
                {
                    if (controller.Won) _statusLabel.text = "Solved!";
                    else if (controller.Lost) _statusLabel.text = "Lost!";
                    else _statusLabel.text = $"Seed: {controller.CurrentSeed}";
                }
            }

            if (_blockedLabel != null)
                _blockedLabel.text = $"Blocked: {controller.BlockedUniqueCount}";
            
            if (_heartsLabel != null)
                _heartsLabel.text = $"Hearts: {controller.blockedLoseLimit - controller.BlockedUniqueCount}";
            
            if (_editModeLabel != null)
                _editModeLabel.text = editing ? "EDIT MODE" : "";

            // Buttons
            if (_editBtn != null) _editBtn.text = editing ? "Done" : "Edit";

            // In edit mode: disable gameplay undo (your controller already ignores Undo() in edit mode),
            // but disabling the button is clearer.
            if (_undoBtn != null)
                _undoBtn.SetEnabled(!editing && !controller.Won && !controller.Lost && controller.MoveCount > 0);

            if (_restartBtn != null) _restartBtn.SetEnabled(!editing);
            if (_newBtn != null) _newBtn.SetEnabled(!editing);
            if (_skipBtn != null)
                _skipBtn.SetEnabled(!editing && (!controller.Won || !_showOutcomeButton));
            
            // Hearts: total = blockedLoseLimit, filled = blockedLoseLimit - BlockedUniqueCount
            int maxHearts = Mathf.Max(0, controller.blockedLoseLimit);
            int filledHearts = Mathf.Clamp(maxHearts - controller.BlockedUniqueCount, 0, maxHearts);

            EnsureHeartsBuilt(maxHearts);
            ApplyHeartsTint(filledHearts, maxHearts);
        }
        
        void EnsureHeartsBuilt(int maxHearts)
        {
            if (_heartsContainer == null) return;

            // If no heart sprite, just hide container so we don't spam errors.
            if (heartSprite == null)
            {
                _heartsContainer.style.display = DisplayStyle.None;
                _lastHeartMax = -1;
                return;
            }

            _heartsContainer.style.display = (maxHearts > 0) ? DisplayStyle.Flex : DisplayStyle.None;
            if (maxHearts <= 0) return;

            if (_lastHeartMax == maxHearts && _heartImages.Count == maxHearts)
                return;

            _lastHeartMax = maxHearts;

            _heartsContainer.Clear();
            _heartImages.Clear();

            // Row layout (in case UXML forgot)
            _heartsContainer.style.flexDirection = FlexDirection.Row;
            _heartsContainer.style.alignItems = Align.Center;

            for (int i = 0; i < maxHearts; i++)
            {
                var img = new Image
                {
                    scaleMode = ScaleMode.ScaleToFit,
                    sprite = heartSprite
                };

                img.style.width = heartSizePx;
                img.style.height = heartSizePx;

                // spacing between hearts
                if (i > 0)
                    img.style.marginLeft = heartSpacingPx;

                _heartsContainer.Add(img);
                _heartImages.Add(img);
            }
        }

        void ApplyHeartsTint(int filled, int max)
        {
            if (_heartsContainer == null) return;
            if (_heartImages.Count == 0) return;

            for (int i = 0; i < _heartImages.Count; i++)
            {
                var img = _heartImages[i];
                if (img == null) continue;

                bool isFull = i < filled;
                img.tintColor = isFull ? heartFullTint : heartEmptyTint;
            }
        }
    }
}
