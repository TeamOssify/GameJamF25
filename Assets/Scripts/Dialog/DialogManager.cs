using System.Collections;
using System.Linq;

using UnityEngine;

public sealed class DialogManager : MonoBehaviour {
    [SerializeField] private DialogEventChannelSO dialogEventChannel;
    [SerializeField] private CandidateEventChannelSO candidateEventChannel;

    [SerializeField] private CandidateDialogSO generalDialog;

    private CandidateInstance _currentCandidate;
    private DialogStateMachine _dialogStateMachine;
    private bool _questionPending;
    private string _chosenReply;

    private void OnEnable() {
        candidateEventChannel.OnCandidateEntered += OnCandidateEntered;
        candidateEventChannel.OnCandidateSatDown += OnCandidateSatDown;
        candidateEventChannel.OnCandidateStoodUp += OnCandidateStoodUp;
        dialogEventChannel.OnChosenPlayerQuestion += OnChosenPlayerQuestion;
        dialogEventChannel.OnAskStandardQuestion += OnAskStandardQuestion;
    }

    private void OnDisable() {
        candidateEventChannel.OnCandidateEntered -= OnCandidateEntered;
        candidateEventChannel.OnCandidateSatDown -= OnCandidateSatDown;
        candidateEventChannel.OnCandidateStoodUp -= OnCandidateStoodUp;
        dialogEventChannel.OnChosenPlayerQuestion -= OnChosenPlayerQuestion;
        dialogEventChannel.OnAskStandardQuestion -= OnAskStandardQuestion;
    }

    private void OnCandidateEntered(CandidateInstance candidate) {
        dialogEventChannel.RaiseClearDialog();
        _questionPending = false;
    }

    private void OnCandidateSatDown(CandidateInstance candidate) {
        if (_dialogStateMachine != null) {
            return;
        }

        _currentCandidate = candidate;
        StartCoroutine(ShowDialogTree(candidate, DialogTreeType.Intro));
    }

    private void OnCandidateStoodUp(CandidateInstance arg0) {
        _currentCandidate = null;
        _dialogStateMachine?.Exit();
    }

    private void OnChosenPlayerQuestion(string question) {
        _dialogStateMachine.UsePlayerAnswer(question);
        _chosenReply = question;
        _questionPending = false;
    }

    private void OnAskStandardQuestion(DialogTreeType treeType) {
        if (_dialogStateMachine != null || _currentCandidate == null) {
            return;
        }

        _chosenReply = null;
        _questionPending = false;
        StartCoroutine(ShowDialogTree(_currentCandidate, treeType));
    }

    private IEnumerator ShowDialogTree(CandidateInstance candidate, DialogTreeType treeType) {
        yield return WaitForSecondsCache.Get(1);

        var dialogSet = ChooseRandomDialogSet(candidate);
        var chosenTree = DialogTreeChooser.GetTree(dialogSet, treeType);

        _dialogStateMachine = new DialogStateMachine(chosenTree);
        yield return ExecuteDialogStateMachine(_dialogStateMachine);

        _dialogStateMachine = null;
    }

    private IEnumerator ExecuteDialogStateMachine(DialogStateMachine stateMachine) {
        const float WAIT_PER_WORD = 0.05f;
        const float WAIT_PER_PUNCTUATION = 0.2f;

        while (stateMachine != null && stateMachine.TryAdvance(out var nextEntry)) {
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
                Debug.Assert(nextEntry.PlayerResponses.Count > 0, "nextEntry.PlayerResponses.Count > 0");

                yield return new WaitForSeconds(Mathf.Max(1.5f, waitTime));

                dialogEventChannel.RaiseNewPlayerQuestions(nextEntry.PlayerResponses.Keys.ToArray());
                _questionPending = true;

                while (_questionPending) {
                    yield return WaitForSecondsCache.Get(0.1f);
                }

                waitTime = 1f;
                if (_chosenReply != null) {
                    waitTime += (WordCounter.CountWords(_chosenReply) * WAIT_PER_WORD) +
                                (WordCounter.CountPunctuation(_chosenReply) * WAIT_PER_PUNCTUATION);
                }

                yield return new WaitForSeconds(waitTime);
            }
            else {
                yield return new WaitForSeconds(Mathf.Max(1.5f, waitTime));
            }
        }
    }

    private DialogFile.DialogSet ChooseRandomDialogSet(CandidateInstance candidate, float generalDialogChance = 0.5f) {
        Debug.Assert(candidate != null, "candidate != null");
        Debug.Assert(candidate.CurrentVariant != null, "candidate.CurrentVariant != null");

        var candidateType = candidate.CurrentVariant.candidateType;

        if (!candidate.CurrentVariant.dialog || Random.Range(0, 1) <= generalDialogChance) {
            Debug.Assert(generalDialog != null, "generalDialog != null");
            Debug.Assert(generalDialog.Dialog != null, "generalDialog.Dialog != null");
            Debug.Assert(generalDialog.Dialog.Human != null, "generalDialog.Dialog.Human != null");
            Debug.Assert(generalDialog.Dialog.NonHuman != null, "generalDialog.Dialog.NonHuman != null");

            return candidateType == CandidateDataStructures.CandidateType.Human
                ? generalDialog.Dialog.Human
                : generalDialog.Dialog.NonHuman;
        }

        return candidateType == CandidateDataStructures.CandidateType.Human
            ? candidate.CurrentVariant.dialog.Dialog.Human
            : candidate.CurrentVariant.dialog.Dialog.NonHuman;
    }
}