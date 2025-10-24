using UnityEngine;

[CreateAssetMenu(fileName = "Candidate", menuName = "Scriptable Objects/Candidate")]
public class CandidateSO : ScriptableObject {
    [Header("Character Identity")]
    public string characterName;

    [Header("Variants")]
    [Tooltip("Human version of this character")]
    public CandidateVariantSO humanVariant;

    [Tooltip("Non-Human version of this character")]
    public CandidateVariantSO nonHumanVariant;

    public CandidateVariantSO GetRandomVariant() {
        return Random.value > 0.5f ? humanVariant : nonHumanVariant;
    }

    public CandidateVariantSO GetVariant(CandidateDataStructures.CandidateType type) {
        return type == CandidateDataStructures.CandidateType.Human ?humanVariant : nonHumanVariant;
    }
}
