using UnityEngine;

[CreateAssetMenu(fileName = "ShiftData", menuName = "Scriptable Objects/ShiftData")]
public class ShiftData : ScriptableObject {
    public int shiftNumber;

    public int candidatesProcessedCorrectly;
    public int candidatesProcessedIncorrectly;

    public void ResetForNextShift() {
        candidatesProcessedCorrectly = 0;
        candidatesProcessedIncorrectly = 0;
    }

    public void ResetAll() {
        ResetForNextShift();
        shiftNumber = 0;
    }
}