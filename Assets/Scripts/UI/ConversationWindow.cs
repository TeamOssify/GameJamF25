using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ConversationWindow : MonoBehaviour {
    [SerializeField] private DialogEventChannelSO dialogEventChannel;
    [SerializeField] private DialogMessage candidateMessagePrefab;
    [SerializeField] private DialogMessage playerMessagePrefab;
    [SerializeField] private PlayerQuestion questionPrefab;
    [SerializeField] private RectTransform windowContents;

    private readonly Queue<(DialogOwner owner, string message)> _dialogQueue = new();
    private readonly Queue<string> _questionQueue = new();
    private readonly List<PlayerQuestion> _currentQuestions = new();

    private void OnEnable() {
        StartCoroutine(ListenForDialog());

        dialogEventChannel.OnNewDialogMessage += OnNewDialogMessage;
        dialogEventChannel.OnNewPlayerQuestions += OnNewPlayerQuestions;
        dialogEventChannel.OnChosenPlayerQuestion += OnChosenPlayerQuestion;
    }

    private void OnDisable() {
        dialogEventChannel.OnNewDialogMessage -= OnNewDialogMessage;
        dialogEventChannel.OnNewPlayerQuestions -= OnNewPlayerQuestions;
        dialogEventChannel.OnChosenPlayerQuestion -= OnChosenPlayerQuestion;
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

            if (!created && _dialogQueue.TryDequeue(out var dialog)) {
                InstantiateMessage(dialog.owner, dialog.message);
                created = true;
            }

            if (!created && _questionQueue.TryDequeue(out var question)) {
                InstantiateQuestion(question);
                created = true;
            }

            yield return WaitForSecondsCache.Get(0.05f);

            if (created) {
                LayoutRebuilder.ForceRebuildLayoutImmediate(windowContents);
            }
        }
    }

    private void InstantiateMessage(DialogOwner owner, string message) {
        var messagePrefab = owner == DialogOwner.Player
            ? playerMessagePrefab
            : candidateMessagePrefab;

        var newMessage = Instantiate(messagePrefab, windowContents);

        newMessage.SetText(message);
    }

    private void InstantiateQuestion(string question)
    {
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

        dialogEventChannel.RaiseOnChosenPlayerQuestion(question);
    }
}