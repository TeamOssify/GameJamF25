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
    [SerializeField] private GameObject questionUi;

    [Header("Sound")]
    [SerializeField] private SfxEventChannelSO sfxEventChannel;

    [SerializeField] private AudioClip newMessageSound;
    [SerializeField] private float newMessageVolume = 0.2f;

    private readonly object _dialogCoroutineLock = new();
    private Coroutine _dialogCoroutine;
    private readonly Queue<(DialogOwner owner, string message)> _dialogQueue = new();
    private readonly Queue<string> _questionQueue = new();
    private readonly List<PlayerQuestion> _currentQuestions = new();

    private void OnEnable() {
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
        InstantiateTreeSeparator();
    }

    private void RunDialogRenderer() {
        lock (_dialogCoroutineLock) {
            if (_dialogCoroutine == null) {
                _dialogCoroutine = StartCoroutine(CoRenderDialog());
            }
        }
    }

    private void OnClearDialog() {
        var childCount = windowContents.childCount;
        for (var i = 0; i < childCount; i++) {
            Destroy(windowContents.GetChild(i).gameObject);
        }
    }

    private void OnNewDialogMessage(DialogOwner owner, string message) {
        _dialogQueue.Enqueue((owner, message));

        RunDialogRenderer();
    }

    private void OnNewPlayerQuestions(string[] questions) {
        foreach (var question in questions) {
            _questionQueue.Enqueue(question);
        }

        RunDialogRenderer();
    }

    private void OnChosenPlayerQuestion(string question) {
        OnNewDialogMessage(DialogOwner.Player, question);
    }

    private IEnumerator CoRenderDialog() {
        while (_dialogQueue.Count > 0 && _questionQueue.Count > 0) {
            var created = false;

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
        }

        lock (_dialogCoroutineLock) {
            _dialogCoroutine = null;
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

    public void QuestionButtonOnClick() {
        questionUi.SetActive(true);
    }
}