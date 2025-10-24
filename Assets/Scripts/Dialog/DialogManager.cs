using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public sealed class DialogManager : MonoBehaviour {
    [SerializeField] private DialogEventChannelSO dialogEventChannel;
    [SerializeField] private CandidateEventChannelSO candidateEventChannel;

    [SerializeField] private CandidateDialogSO generalDialog;

    private readonly Dictionary<string, string> _currentQuestions = new();

    private void OnEnable() {
        candidateEventChannel.OnCandidateEntered += OnCandidateEntered;
        candidateEventChannel.OnCandidateSatDown += OnCandidateSatDown;
    }

    private void OnDisable() {
        candidateEventChannel.OnCandidateEntered -= OnCandidateEntered;
        candidateEventChannel.OnCandidateSatDown -= OnCandidateSatDown;
    }

    private void OnCandidateEntered(CandidateInstance candidate) {
        dialogEventChannel.RaiseClearDialog();
        _currentQuestions.Clear();
    }

    private void OnCandidateSatDown(CandidateInstance candidate) {
        StartCoroutine(ShowIntroDialog(candidate));
    }

    private IEnumerator ShowIntroDialog(CandidateInstance candidate) {
        yield return WaitForSecondsCache.Get(1);

        var dialogSet = ChooseRandomDialogSet(candidate);
        var chosenTree = dialogSet.Intro[Random.Range(0, dialogSet.Intro.Length)].Tree;
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

        dialogEventChannel.RaiseOnNewDialogMessage(DialogOwner.Candidate, "Its ya boyyy");

        yield return WaitForSecondsCache.Get(1);

        dialogEventChannel.RaiseOnNewDialogMessage(DialogOwner.Candidate, "Tell me, what do ya know about the McRib?");

        yield return WaitForSecondsCache.Get(1);

        dialogEventChannel.RaiseOnNewDialogMessage(DialogOwner.Player, "Well...");

        yield return WaitForSecondsCache.Get(1);

        dialogEventChannel.RaiseOnNewPlayerQuestions(new[] {
            "I know it's a seasonal menu item everywhere in the world, except for Luxembourg who have it as a permanent menu item.",
            "I know they taste good.",
            "Honestly I prefer the Big Mac."
        });
    }
}