using UnityEngine;

[CreateAssetMenu(fileName = "ShiftData", menuName = "Scriptable Objects/ShiftData")]
public class ShiftData : ScriptableObject {
    private int _shiftNumber;
    public int ShiftNumber {
        get { return _shiftNumber; }
        set {
            _shiftNumber = value;

            // We haven't completed this shift yet
            var shiftMinusOne = value - 1;
            if (shiftMinusOne > PlayerPrefs.GetInt(Constants.PlayerPrefsKeys.HIGH_SCORE)) {
                PlayerPrefs.SetInt(Constants.PlayerPrefsKeys.HIGH_SCORE, shiftMinusOne);
            }
        }
    }

    public int candidatesProcessedCorrectly;
    public int candidatesProcessedIncorrectly;

    public void ResetForNextShift() {
        candidatesProcessedCorrectly = 0;
        candidatesProcessedIncorrectly = 0;
    }

    public void ResetAll() {
        ResetForNextShift();
        ShiftNumber = 0;
    }
}