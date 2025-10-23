using System;
using System.Collections.Generic;

using UnityEngine;

public class ConversationWindow : MonoBehaviour {
    [SerializeField] private DialogEventChannelSO dialogEventChannel;
    [SerializeField] private DialogMessage candidateMessagePrefab;
    [SerializeField] private DialogMessage playerMessagePrefab;
    [SerializeField] private PlayerQuestion questionPrefab;
    [SerializeField] private Transform windowContents;

    private readonly List<PlayerQuestion> _currentQuestions = new();

    private void OnEnable() {
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
        var messagePrefab = owner == DialogOwner.Player
            ? playerMessagePrefab
            : candidateMessagePrefab;

        var newMessage = Instantiate(messagePrefab, windowContents);
        newMessage.SetText(message);
    }

    private void OnNewPlayerQuestions(string[] questions) {
        foreach (var question in questions) {
            var newQuestion = Instantiate(questionPrefab, windowContents);

            newQuestion.SetText(question);
            newQuestion.Click += QuestionClicked;

            _currentQuestions.Add(newQuestion);
        }
    }

    private void OnChosenPlayerQuestion(string question) {
        OnNewDialogMessage(DialogOwner.Player, question);
    }

    private void QuestionClicked(string question) {
        foreach (var currentQuestion in _currentQuestions) {
            Destroy(currentQuestion.gameObject);
        }

        _currentQuestions.Clear();

        dialogEventChannel.RaiseOnChosenPlayerQuestion(question);
    }
}