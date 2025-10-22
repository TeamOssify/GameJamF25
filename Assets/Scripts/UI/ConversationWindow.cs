using System;

using UnityEngine;

public class ConversationWindow : MonoBehaviour {
    [SerializeField] private DialogEventChannelSO dialogEventChannel;
    [SerializeField] private DialogMessage candidateMessagePrefab;
    [SerializeField] private DialogMessage playerMessagePrefab;
    [SerializeField] private GameObject questionPrefab;
    [SerializeField] private Transform windowContents;

    private void OnEnable() {
        dialogEventChannel.OnNewDialogMessage += OnNewDialogMessage;
        dialogEventChannel.OnNewPlayerQuestions += OnNewPlayerQuestions;
    }

    private void OnDisable() {
        dialogEventChannel.OnNewDialogMessage -= OnNewDialogMessage;
        dialogEventChannel.OnNewPlayerQuestions -= OnNewPlayerQuestions;
    }

    private void OnNewDialogMessage(DialogOwner owner, string message) {
        var messagePrefab = owner == DialogOwner.Player
            ? playerMessagePrefab
            : candidateMessagePrefab;

        var newMessage = Instantiate(messagePrefab, windowContents);

        newMessage.SetText(message);
    }

    private void OnNewPlayerQuestions(string[] questions) {
        throw new NotImplementedException();
    }
}