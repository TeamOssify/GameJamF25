using System.Linq;

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Dialog Event Channel")]
public sealed class DialogEventChannelSO : ScriptableObject {
    public UnityAction OnClearDialog;
    public UnityAction OnNewDialogTree;
    public UnityAction<DialogOwner, string> OnNewDialogMessage;
    public UnityAction<string[]> OnNewPlayerQuestions;
    public UnityAction<string> OnChosenPlayerQuestion;

    public void RaiseClearDialog() {
        Debug.Assert(OnClearDialog != null, "OnClearDialog != null");

        OnClearDialog?.Invoke();
    }

    public void RaiseNewDialogTree() {
        Debug.Assert(OnNewDialogTree != null, "OnNewDialogTree != null");

        OnNewDialogTree?.Invoke();
    }

    public void RaiseNewDialogMessage(DialogOwner owner, string message) {
        Debug.Assert(message != null, "Null dialog message was passed to dialog event channel!");
        Debug.Assert(OnNewDialogMessage != null, "A new dialog message was sent, but no listeners were found on the dialog channel!");

        OnNewDialogMessage?.Invoke(owner, message);
    }

    public void RaiseNewPlayerQuestions(string[] questions) {
        Debug.Assert(questions != null, "Null questions array was passed to dialog event channel!");
        Debug.Assert(questions.All(x => x != null), "A null question was passed to dialog event channel!");
        Debug.Assert(OnNewPlayerQuestions != null, "A set of new questions was sent, but no listeners were found on the dialog channel!");

        OnNewPlayerQuestions?.Invoke(questions);
    }

    public void RaiseChosenPlayerQuestion(string question) {
        Debug.Assert(question != null, "Null question was passed to dialog event channel!");
        Debug.Assert(OnChosenPlayerQuestion != null, "A player question was chosen, but no listeners were found on the dialog channel!");

        OnChosenPlayerQuestion?.Invoke(question);
    }
}