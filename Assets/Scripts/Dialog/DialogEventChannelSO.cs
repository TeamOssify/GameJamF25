using System.Linq;

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Dialog Event Channel")]
public sealed class DialogEventChannelSO : ScriptableObject {
    public UnityAction<DialogOwner, string> OnNewDialogMessage;

    public UnityAction<string[]> OnNewPlayerQuestions;

    public UnityAction<string> OnChosenPlayerQuestion;

    public void RaiseOnNewDialogMessage(DialogOwner owner, string message) {
        Debug.Assert(message != null, "Null dialog message was passed to dialog event channel!");
        Debug.Assert(OnNewDialogMessage != null, "A new dialog message was sent, but no listeners were found on the dialog channel!");

        OnNewDialogMessage.Invoke(owner, message);
    }

    public void RaiseOnNewPlayerQuestions(string[] questions) {
        Debug.Assert(questions != null, "Null questions array was passed to dialog event channel!");
        Debug.Assert(questions.All(x => x != null), "A null question was passed to dialog event channel!");
        Debug.Assert(OnNewPlayerQuestions != null, "A set of new questions was sent, but no listeners were found on the dialog channel!");

        OnNewPlayerQuestions.Invoke(questions);
    }

    public void RaiseOnChosenPlayerQuestion(string question) {
        Debug.Assert(question != null, "Null question was passed to dialog event channel!");
        Debug.Assert(OnChosenPlayerQuestion != null, "A player question was chosen, but no listeners were found on the dialog channel!");

        OnChosenPlayerQuestion.Invoke(question);
    }
}