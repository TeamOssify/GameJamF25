using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Scriptable Objects/Question")]
public class Question : ScriptableObject {
    public string questionid;

    [TextArea(2, 5)]
    public string questionText;
}
