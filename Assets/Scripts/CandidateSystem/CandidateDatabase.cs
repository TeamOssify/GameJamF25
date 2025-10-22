using UnityEngine;

[CreateAssetMenu(fileName = "CandidateDatabase", menuName = "Scriptable Objects/CandidateDatabase")]
public class CandidateDatabase : ScriptableObject {
    [Header("All Candidates")]
    [SerializeField]
    private Candidate[] allCandidates;

    [Header("Body Languages")]
    [SerializeField]
    private BodyLanguageBehavior[] bodyLanguages;

    public Candidate GetRandomCandidate() {
        if (allCandidates.Length > 0) {
            return allCandidates[Random.Range(0, allCandidates.Length)];
        }

        Debug.LogError("No candidates in database");
        return null;
    }

    public CandidateInstance CreateRandomCandidateInstance() {
        var candidate = GetRandomCandidate();
        if (!candidate) {
            return null;
        }

        var variant = candidate.GetRandomVariant();
        return new CandidateInstance(candidate, variant);
    }
}
