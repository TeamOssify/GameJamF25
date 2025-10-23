using System.Collections;

using UnityEngine;
using System.Collections.Generic;

public class CandidateManager : MonoBehaviour {
    [SerializeField] private CandidateDatabase db;
    [SerializeField] private CandidatePhysicalManager candidatePhysicalManager;
    [SerializeField] private ShiftManager shiftManager;
    [SerializeField] private DecisionUIManager decisionUIManager;

    private CandidateInstance _currentCandidate;
    private readonly HashSet<CandidateInstance> _interviewedCandidates = new();

    public void LoadNextCandidate() {
        if (candidatePhysicalManager.IsCandidatePresent) {
            return;
        }

        _currentCandidate = db.CreateRandomCandidateInstance();

        if (_currentCandidate != null) {
            BringInCandidate();
            decisionUIManager.AddCandidate(_currentCandidate);
            if (_interviewedCandidates.Count == 0) {
                shiftManager.StartShift();
            }
        }
    }

    private void BringInCandidate() {
        candidatePhysicalManager.SpawnCandidateImage(_currentCandidate);
        candidatePhysicalManager.WalkToChair();
    }

    public void KickCurrentCandidate() {
        if (_currentCandidate != null) {
            _currentCandidate.HasBeenInterviewed = true;
            _interviewedCandidates.Add(_currentCandidate);

        }

        candidatePhysicalManager.WalkToDoor();

        // need wait until candidate leaves to end shift
        shiftManager.CheckShiftEnd();
    }

    public CandidateInstance GetCurrentCandidate() {
        return _currentCandidate;
    }

    public IReadOnlyCollection<CandidateInstance> GetInterviewedCandidates() {
        return _interviewedCandidates;
    }
}
