using UnityEngine;
using System.Collections.Generic;

using UnityEngine.UIElements;

public class CandidateManager : MonoBehaviour {
    [SerializeField]
    public CandidateDatabase db;

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

        }
    }

    void BringInCandidate(CandidateInstance candidate) {

    }
}
