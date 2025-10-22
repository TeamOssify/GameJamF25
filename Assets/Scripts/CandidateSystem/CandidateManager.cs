using UnityEngine;
using System.Collections.Generic;

using UnityEngine.UIElements;

public class CandidateManager : MonoBehaviour {
    [SerializeField]
    private CandidateDatabase db;

    [SerializeField]
    private CandidatePhysicalManager candidatePhysicalManager;

    private CandidateInstance currentCandidate;
    private List<CandidateInstance> interviewedCandidates = new List<CandidateInstance>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextCandidate() {
        currentCandidate = db.CreateRandomCandidateInstance();

        if (currentCandidate != null) {
            BringInCandidate();
        }
    }

    private void BringInCandidate() {
        candidatePhysicalManager.SpawnCandidateImage(currentCandidate);
        candidatePhysicalManager.WalkToChair();

    }
    public void KickCurrentCandidate() {
        if (currentCandidate != null) {
            currentCandidate.HasBeenInterviewed = true;
            interviewedCandidates.Add(currentCandidate);
        }
        candidatePhysicalManager.WalkToDoor();
    }

    public CandidateInstance GetCurrentCandidate() {
        return currentCandidate;
    }

    public List<CandidateInstance> GetInterviewedCandidates() {
        return interviewedCandidates;
    }
}
