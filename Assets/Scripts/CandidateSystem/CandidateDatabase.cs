using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "CandidateDatabase", menuName = "Scriptable Objects/CandidateDatabase")]
public class CandidateDatabase : ScriptableObject {
    [Header("All Candidates")]
    public List<Candidate> allCandidates = new List<Candidate>();

    [Header("Body Languages")]
    public List<BodyLanguageBehavior> bodyLanguages = new List<BodyLanguageBehavior>();

    public Candidate GetRandomCandidate() {
        if (allCandidates.Count > 0) {
            return allCandidates[Random.Range(0, allCandidates.Count)];
        }
        Debug.LogError("No candidates in database");
        return null;
    }

    public CandidateInstance CreateRandomCandidateInstance() {
        Candidate candidate = GetRandomCandidate();
        if (candidate == null) return null;
        CandidateVariant variant = candidate.GetRandomVariant();
        return new CandidateInstance(candidate, variant);
    }
}
