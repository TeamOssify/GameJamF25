using UnityEngine;

[CreateAssetMenu(fileName = "Candidate", menuName = "Scriptable Objects/Candidate")]
public class CandidateSO : ScriptableObject {
    [Header("Character Identity")]
    public string characterName;

    [Header("Variants")]
    [Tooltip("Human version of this character")]
    public CandidateVariantSO[] humanVariants;

    [Tooltip("Non-Human version of this character")]
    public CandidateVariantSO[] nonHumanVariants;

    public CandidateVariantSO GetRandomVariant() {
        var pool = Random.value > 0.5f ? humanVariants : nonHumanVariants;
        var pick = pool[Random.Range(0, pool.Length)];
        Debug.Log("Picked");
        Debug.Log(pick);
        return pick;
    }
}
