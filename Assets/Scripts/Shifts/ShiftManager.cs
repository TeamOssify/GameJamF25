using Eflatun.SceneReference;

using TMPro;

using UnityEngine;

public class ShiftManager : MonoBehaviour {
    [SerializeField] private CandidateEventChannelSO candidateEventChannel;
    [SerializeField] private LoadEventChannelSO loadEventChannel;
    [SerializeField] private ShiftData shiftData;
    [SerializeField] private ShiftTimer shiftTimer;
    [SerializeField] private SceneReference reportScene;
    [SerializeField] private CandidateManager candidateManager;
    [SerializeField] private RoomMoodManager roomMoodManager;  // Changing the mood of the room after shifts
    [SerializeField] private TextMeshProUGUI undecidedText;

    private void OnEnable() {
        candidateEventChannel.OnCandidateSatDown += OnCandidateSatDown;
        candidateEventChannel.OnCandidateExited += OnCandidateExited;
    }

    private void OnDisable() {
        candidateEventChannel.OnCandidateSatDown -= OnCandidateSatDown;
        candidateEventChannel.OnCandidateExited -= OnCandidateExited;
    }

    private void OnCandidateSatDown(CandidateInstance arg0) {
        if (!shiftTimer.TimerStarted) {
            StartShift();
        }
    }

    private void OnCandidateExited(CandidateInstance arg0) {
        CheckShiftEnd();
    }

    // starts timer and increments shift number
    public void StartShift() {
        shiftTimer.StartTimer();
        shiftData.ShiftNumber++;
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
            if (candidate.IsHuman() == candidate.PlayerDecision) {
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

    private void DisableWarning() {
        undecidedText.enabled = false;
    }
}