using System.Collections;
using System.Linq;

using UnityEngine;

public sealed class DialogManager : MonoBehaviour {
    [SerializeField] private DialogEventChannelSO dialogEventChannel;
    [SerializeField] private CandidateEventChannelSO candidateEventChannel;

    [SerializeField] private CandidateDialogSO generalDialog;

    private DialogStateMachine _dialogStateMachine;
    private bool _questionPending;
    private string _chosenReply;

    private void OnEnable() {
        candidateEventChannel.OnCandidateEntered += OnCandidateEntered;
        candidateEventChannel.OnCandidateSatDown += OnCandidateSatDown;
        candidateEventChannel.OnCandidateStoodUp += OnCandidateStoodUp;
        dialogEventChannel.OnChosenPlayerQuestion += OnChosenPlayerQuestion;
    }

    private void OnDisable() {
        candidateEventChannel.OnCandidateEntered -= OnCandidateEntered;
        candidateEventChannel.OnCandidateSatDown -= OnCandidateSatDown;
        candidateEventChannel.OnCandidateStoodUp -= OnCandidateStoodUp;
        dialogEventChannel.OnChosenPlayerQuestion -= OnChosenPlayerQuestion;
    }

    private void OnCandidateEntered(CandidateInstance candidate) {
        dialogEventChannel.RaiseClearDialog();
        _questionPending = false;
    }

    private void OnCandidateSatDown(CandidateInstance candidate) {
        StartCoroutine(ShowIntroDialog(candidate));
    }

    private void OnCandidateStoodUp(CandidateInstance arg0) {
        _dialogStateMachine.Exit();
    }

    private void OnChosenPlayerQuestion(string question) {
        _chosenReply = question;
        _dialogStateMachine.UsePlayerAnswer(question);
        _questionPending = false;
    }

    private IEnumerator ShowIntroDialog(CandidateInstance candidate) {
        yield return WaitForSecondsCache.Get(1);

        var dialogSet = ChooseRandomDialogSet(candidate);
        var chosenTree = dialogSet.Intro[Random.Range(0, dialogSet.Intro.Length)];

        _dialogStateMachine = new DialogStateMachine(chosenTree);
        yield return ExecuteDialogStateMachine(_dialogStateMachine);
    }

    private IEnumerator ExecuteDialogStateMachine(DialogStateMachine stateMachine) {
        const float WAIT_PER_WORD = 0.05f;
        const float WAIT_PER_PUNCTUATION = 0.2f;

        while (stateMachine.TryAdvance(out var nextEntry)) {
            var waitTime = 1f;

            if (nextEntry.Candidate != null) {
                Debug.Assert(nextEntry.Player == null, "nextEntry.Player == null");

                waitTime += WordCounter.CountWords(nextEntry.Candidate) * WAIT_PER_WORD;
                waitTime += WordCounter.CountPunctuation(nextEntry.Candidate) * WAIT_PER_PUNCTUATION;

                dialogEventChannel.RaiseNewDialogMessage(DialogOwner.Candidate, nextEntry.Candidate);
            }
            else if (nextEntry.Player != null) {
                Debug.Assert(nextEntry.Candidate == null, "nextEntry.Candidate == null");

                waitTime += WordCounter.CountWords(nextEntry.Player) * WAIT_PER_WORD;
                waitTime += WordCounter.CountPunctuation(nextEntry.Player) * WAIT_PER_PUNCTUATION;

                dialogEventChannel.RaiseNewDialogMessage(DialogOwner.Player, nextEntry.Player);
            }

            if (nextEntry.PlayerResponses != null) {
                yield return new WaitForSeconds(Mathf.Max(1.5f, waitTime));

                dialogEventChannel.RaiseNewPlayerQuestions(nextEntry.PlayerResponses.Keys.ToArray());
                _questionPending = true;

                while (_questionPending) {
                    yield return WaitForSecondsCache.Get(0.1f);
                }

                waitTime = 1f + (WordCounter.CountWords(_chosenReply) * WAIT_PER_WORD)
                              + (WordCounter.CountPunctuation(_chosenReply) * WAIT_PER_PUNCTUATION);

                yield return new WaitForSeconds(waitTime);
            }
            else {
                yield return new WaitForSeconds(Mathf.Max(1.5f, waitTime));
            }
        }
    }

    private DialogFile.DialogSet ChooseRandomDialogSet(CandidateInstance candidate, float generalDialogChance = 0.5f) {
        var candidateType = candidate.CurrentVariant.candidateType;

        if (!candidate.CurrentVariant.dialog || Random.Range(0, 1) <= generalDialogChance) {
            return candidateType == CandidateDataStructures.CandidateType.Human
                ? generalDialog.Dialog.Human
                : generalDialog.Dialog.NonHuman;
        }

        return candidateType == CandidateDataStructures.CandidateType.Human
            ? candidate.CurrentVariant.dialog.Dialog.Human
            : candidate.CurrentVariant.dialog.Dialog.NonHuman;
    }

    private IEnumerator TestDialog() {
        dialogEventChannel.RaiseNewDialogTree();

        dialogEventChannel.RaiseNewDialogMessage(DialogOwner.Candidate, "Its ya boyyy");

        yield return WaitForSecondsCache.Get(1);

        dialogEventChannel.RaiseNewDialogMessage(DialogOwner.Candidate, "Tell me, what do ya know about the McRib?");

        yield return WaitForSecondsCache.Get(1);

        dialogEventChannel.RaiseNewDialogMessage(DialogOwner.Player, "Well...");

        yield return WaitForSecondsCache.Get(1);

        dialogEventChannel.RaiseNewPlayerQuestions(new[] {
            "I know it's a seasonal menu item everywhere in the world, except for Luxembourg who have it as a permanent menu item.",
            "I know they taste good.",
            "Honestly I prefer the Big Mac."
        });
    }
}