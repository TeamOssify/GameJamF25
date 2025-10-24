using System.Collections;

using UnityEngine;

using System.Collections.Generic;

using TMPro;

public class CandidateManager : MonoBehaviour {
    [SerializeField] private CandidateEventChannelSO candidateEventChannel;
    [SerializeField] private CandidateDatabase db;
    [SerializeField] private CandidatePhysicalManager candidatePhysicalManager;
    [SerializeField] private ShiftManager shiftManager;
    [SerializeField] private DecisionUIManager decisionUIManager;
    [SerializeField] private TMP_InputField notesText;

    private CandidateInstance _currentCandidate;
    private readonly HashSet<CandidateInstance> _interviewedCandidates = new();

    public void LoadNextCandidate() {
        if (candidatePhysicalManager.IsCandidatePresent) {
            return;
        }

        _currentCandidate = db.CreateRandomCandidateInstance();

        if (_currentCandidate != null) {
            StartCoroutine(BringInCandidate());
            decisionUIManager.AddCandidate(_currentCandidate);
            notesText.text = "";
        }
    }

    private IEnumerator BringInCandidate() {
        candidatePhysicalManager.SpawnCandidateImage(_currentCandidate);
        candidateEventChannel.RaiseCandidateEntered(_currentCandidate);

        yield return candidatePhysicalManager.WalkToChair();

        candidateEventChannel.RaiseCandidateSatDown(_currentCandidate);
    }

    public void KickCurrentCandidate() {
        if (_currentCandidate != null) {
            _currentCandidate.HasBeenInterviewed = true;
            _interviewedCandidates.Add(_currentCandidate);
        }

        StartCoroutine(CoKickCurrentCandidate());
    }

    public IEnumerator CoKickCurrentCandidate() {
        candidateEventChannel.RaiseCandidateStoodUp(_currentCandidate);

        yield return candidatePhysicalManager.WalkToDoor();

        candidateEventChannel.RaiseCandidateExited(_currentCandidate);
    }

    public CandidateInstance GetCurrentCandidate() {
        return _currentCandidate;
    }

    public IReadOnlyCollection<CandidateInstance> GetInterviewedCandidates() {
        return _interviewedCandidates;
    }

    public void SaveNotes() {
        _currentCandidate.PlayerNotes = notesText.text;
    }
}