using UnityEngine;

[CreateAssetMenu(fileName = "Candidate", menuName = "Scriptable Objects/Candidate")]
public class Candidate : ScriptableObject {
    [Header("Character Identity")]
    public string characterName;

    [Header("Variants")]
    [Tooltip("Human version of this character")]
    public CandidateVariant humanVariant;

    [Tooltip("Non-Human version of this character")]
    public CandidateVariant nonHumanVariant;

    public CandidateVariant GetRandomVariant() {
        return Random.value > 0.5f ? humanVariant : nonHumanVariant;
    }

    public CandidateVariant GetVariant(CandidateDataStructures.CandidateType type) {
        return type == CandidateDataStructures.CandidateType.Human ?humanVariant : nonHumanVariant;
    }
}
