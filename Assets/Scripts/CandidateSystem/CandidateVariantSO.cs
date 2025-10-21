using System.Collections.Generic;

using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "CandidateVariant", menuName = "Scriptable Objects/CandidateVariant")]
public class CandidateVariant : ScriptableObject {
    [Header("Identity")]
    public string variantId;
    public CandidateDataStructures.CandidateType candidateType;

    [Header("Appearance")]
    public Sprite fullBodySprite;
    public Sprite portraitSprite;

    [Header("Stats")]
    public CandidateDataStructures.CandidateStats baseStats;

    [Header("Responses")]
    public List<CandidateDataStructures.QuestionResponse> questionResponses = new List<CandidateDataStructures.QuestionResponse>();

    [Header("Body Language")]
    public List<BodyLanguageBehavior> passiveBodyLanguages = new List<BodyLanguageBehavior>();

    [Header("Physical Tells")]
    [Tooltip("List of physical discrepancies that can be spotted by player")]
    public List<string> physicalDiscrepancies = new List<string>();
}
