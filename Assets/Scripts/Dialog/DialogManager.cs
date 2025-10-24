using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public sealed class DialogManager : MonoBehaviour {
    [SerializeField] private DialogEventChannelSO dialogEventChannel;

    private readonly Dictionary<string, string> _currentQuestions = new();

    private IEnumerator Start() {
        yield return new WaitForSeconds(1);

        dialogEventChannel.RaiseOnNewDialogMessage(DialogOwner.Candidate, "Its ya boyyy");

        yield return new WaitForSeconds(1);

        dialogEventChannel.RaiseOnNewDialogMessage(DialogOwner.Candidate, "Tell me, what do ya know about the McRib?");

        yield return new WaitForSeconds(1);

        dialogEventChannel.RaiseOnNewDialogMessage(DialogOwner.Player, "Well...");

        yield return new WaitForSeconds(1);

        dialogEventChannel.RaiseOnNewPlayerQuestions(new[] {
            "I know it's a seasonal menu item everywhere in the world, except for Luxembourg who have it as a permanent menu item.",
            "I know they taste good.",
            "Honestly I prefer the Big Mac."
        });
    }
}