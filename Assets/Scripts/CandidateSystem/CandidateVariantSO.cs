using System.Collections.Generic;

using JetBrains.Annotations;

using UnityEngine;

[CreateAssetMenu(fileName = "CandidateVariant", menuName = "Scriptable Objects/CandidateVariant")]
public class CandidateVariantSO : ScriptableObject {
    [Header("Identity")]
    public CandidateDataStructures.CandidateType candidateType;

    [Header("Appearance")]
    public Sprite fullBodySprite;
    public Sprite backSprite;
    public Sprite portraitSprite;

    [Header("Stats")]
    public CandidateDataStructures.CandidateStats baseStats;

    [Header("Dialog")]
    [CanBeNull]
    public CandidateDialogSO dialog;

    [Header("Body Language")]
    public List<BodyLanguageBehavior> passiveBodyLanguages = new();

    [Header("Physical Tells")]
    [Tooltip("List of physical discrepancies that can be spotted by player")]
    public List<string> physicalDiscrepancies = new();
}
