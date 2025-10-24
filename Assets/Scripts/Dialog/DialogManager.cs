using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public sealed class DialogManager : MonoBehaviour {
    [SerializeField] private DialogEventChannelSO dialogEventChannel;
    [SerializeField] private CandidateEventChannelSO candidateEventChannel;

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

        yield return TestDialog();
        // TODO:
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