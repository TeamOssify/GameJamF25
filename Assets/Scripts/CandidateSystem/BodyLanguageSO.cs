using UnityEngine;

[CreateAssetMenu(fileName = "BodyLanguage", menuName = "Scriptable Objects/BodyLanguage")]
public class BodyLanguageBehavior : ScriptableObject {
    public string behaviorName;
    public CandidateDataStructures.BodyLanguageType type;


    public float frequency; // for passive

    public float duration; //how long the frame lasts

    public Sprite frame;
}
