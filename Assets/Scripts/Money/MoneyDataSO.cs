using UnityEngine;

[CreateAssetMenu(fileName = "MoneyData", menuName = "Scriptable Objects/MoneyData")]
public class MoneyDataSO : ScriptableObject {
    public int currentBalance;

    public int rentAmount;

    public int baseSalaryPerCorrect;
}
