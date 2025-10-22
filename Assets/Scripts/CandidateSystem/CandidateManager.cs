using UnityEngine;
using System.Collections.Generic;

public class CandidateManager : MonoBehaviour {
    [SerializeField] private CandidateDatabase db;
    [SerializeField] private CandidatePhysicalManager candidatePhysicalManager;

    private CandidateInstance _currentCandidate;
    private readonly HashSet<CandidateInstance> _interviewedCandidates = new();

    public void LoadNextCandidate() {
        if (candidatePhysicalManager.IsCandidatePresent) {
            return;
        }

        _currentCandidate = db.CreateRandomCandidateInstance();

        if (_currentCandidate != null) {
            BringInCandidate();
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
    }

    public CandidateInstance GetCurrentCandidate() {
        return _currentCandidate;
    }

    public IReadOnlyCollection<CandidateInstance> GetInterviewedCandidates() {
        return _interviewedCandidates;
    }
}
