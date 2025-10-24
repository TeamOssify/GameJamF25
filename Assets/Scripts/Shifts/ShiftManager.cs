using System.Collections.Generic;

using Eflatun.SceneReference;

using UnityEngine;

public class ShiftManager : MonoBehaviour {
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private ShiftData shiftData;
    [SerializeField] private ShiftTimer shiftTimer;
    [SerializeField] private LoadEventChannelSO loadEventChannel;
    [SerializeField] private SceneReference reportScene;
    [SerializeField] private CandidateManager candidateManager;
    [SerializeField] private RoomMoodManager roomMoodManager;  // Changing the mood of the room after shifts

    //starts timer and increments shift number
    public void StartShift() {
        shiftTimer.StartTimer();
        shiftData.shiftNumber++;
    }

    // checks if the shift should end and ends it
    public void CheckShiftEnd() {
        if (shiftTimer.TimerEnded) {
            EndShift();
        }
    }

    // switches the scene to the report screen
    public void EndShift() {
        var interviewedCandidates = candidateManager.GetInterviewedCandidates();
        var correct = 0;
        var incorrect = 0;

        foreach (var candidate in interviewedCandidates) {
            if (candidate.PlayerDecision == null) {
                Debug.Log("Cannot end shift because there is undecided candidates");
                return;
            }
            if(candidate.IsHuman() && candidate.PlayerDecision == true) {
                correct++;
            }
            else {
                incorrect++;
            }
        }
        shiftData.candidatesProcessedCorrectly = correct;
        shiftData.candidatesProcessedIncorrectly = incorrect;

        // Room mood manager being called
        if (roomMoodManager != null) {
            roomMoodManager.UpdateEnvironment(incorrect, correct); // passing in the players answers
        }

        loadEventChannel.RaiseEvent(reportScene, SceneLoadType.Immediate);
    }
}