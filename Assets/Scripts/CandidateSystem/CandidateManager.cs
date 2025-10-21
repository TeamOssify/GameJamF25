using UnityEngine;
using System.Collections.Generic;

using UnityEngine.UIElements;

public class CandidateManager : MonoBehaviour {
    [SerializeField]
    private CandidateDatabase db;

    [SerializeField]
    public GameObject candidateWorld;

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
            BringInCandidate(currentCandidate);
        }
    }

    void BringInCandidate(CandidateInstance candidate) {

    }

    public CandidateInstance GetCurrentCandidate() {
        return currentCandidate;
    }
}
