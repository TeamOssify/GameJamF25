using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ConversationWindow : MonoBehaviour {
    [Header("Dialog")]
    [SerializeField] private DialogEventChannelSO dialogEventChannel;

    [SerializeField] private GameObject dialogTreeSeparatorPrefab;
    [SerializeField] private DialogMessage candidateMessagePrefab;
    [SerializeField] private DialogMessage playerMessagePrefab;
    [SerializeField] private PlayerQuestion questionPrefab;
    [SerializeField] private RectTransform windowContents;

    [Header("Sound")]
    [SerializeField] private SfxEventChannelSO sfxEventChannel;

    [SerializeField] private AudioClip newMessageSound;
    [SerializeField] private float newMessageVolume = 0.2f;

    private bool _isNewTree = false;
    private readonly Queue<(DialogOwner owner, string message)> _dialogQueue = new();
    private readonly Queue<string> _questionQueue = new();
    private readonly List<PlayerQuestion> _currentQuestions = new();

    private void OnEnable() {
        StartCoroutine(ListenForDialog());

        dialogEventChannel.OnClearDialog += OnClearDialog;
        dialogEventChannel.OnNewDialogTree += OnNewDialogTree;
        dialogEventChannel.OnNewDialogMessage += OnNewDialogMessage;
        dialogEventChannel.OnNewPlayerQuestions += OnNewPlayerQuestions;
        dialogEventChannel.OnChosenPlayerQuestion += OnChosenPlayerQuestion;
    }

    private void OnDisable() {
        dialogEventChannel.OnClearDialog -= OnClearDialog;
        dialogEventChannel.OnNewDialogTree -= OnNewDialogTree;
        dialogEventChannel.OnNewDialogMessage -= OnNewDialogMessage;
        dialogEventChannel.OnNewPlayerQuestions -= OnNewPlayerQuestions;
        dialogEventChannel.OnChosenPlayerQuestion -= OnChosenPlayerQuestion;
    }

    private void OnNewDialogTree() {
        _isNewTree = true;
    }

    private void OnClearDialog() {
        var childCount = windowContents.childCount;
        for (var i = 0; i < childCount; i++) {
            Destroy(windowContents.GetChild(i).gameObject);
        }
    }

    private void OnNewDialogMessage(DialogOwner owner, string message) {
        _dialogQueue.Enqueue((owner, message));
    }

    private void OnNewPlayerQuestions(string[] questions) {
        foreach (var question in questions) {
            _questionQueue.Enqueue(question);
        }
    }

    private void OnChosenPlayerQuestion(string question) {
        OnNewDialogMessage(DialogOwner.Player, question);
    }

    // Hack to fix weird layout issues
    private IEnumerator ListenForDialog() {
        while (enabled) {
            var created = false;

            if (_isNewTree && (_dialogQueue.Count > 0 || _questionQueue.Count > 0)) {
                _isNewTree = false;
                InstantiateTreeSeparator();
            }

            if (!created && _dialogQueue.TryDequeue(out var dialog)) {
                InstantiateMessage(dialog.owner, dialog.message);
                created = true;
            }

            if (!created && _questionQueue.TryDequeue(out var question)) {
                InstantiateQuestion(question);
                created = true;
            }

            if (created) {
                // We want this to happen as close to instantiation as possible to make it feel good
                sfxEventChannel.OnPlayVolumedSoundEffect(newMessageSound, newMessageVolume);
            }

            yield return WaitForSecondsCache.Get(0.05f);

            if (created) {
                // Must occur after waiting
                LayoutRebuilder.ForceRebuildLayoutImmediate(windowContents);
            }

            // CPU optimization
            if (_dialogQueue.Count == 0 && _questionQueue.Count == 0) {
                yield return WaitForSecondsCache.Get(0.25f);
            }
        }
    }

    private void InstantiateTreeSeparator() {
        Instantiate(dialogTreeSeparatorPrefab, windowContents);
    }

    private void InstantiateMessage(DialogOwner owner, string message) {
        var messagePrefab = owner == DialogOwner.Player
            ? playerMessagePrefab
            : candidateMessagePrefab;

        var newMessage = Instantiate(messagePrefab, windowContents);

        newMessage.SetText(message);
    }

    private void InstantiateQuestion(string question) {
        var newQuestion = Instantiate(questionPrefab, windowContents);

        newQuestion.SetText(question);
        newQuestion.Click += QuestionClicked;

        _currentQuestions.Add(newQuestion);
    }

    private void QuestionClicked(string question) {
        foreach (var currentQuestion in _currentQuestions) {
            Destroy(currentQuestion.gameObject);
        }

        _currentQuestions.Clear();

        dialogEventChannel.RaiseChosenPlayerQuestion(question);
    }
}