using UnityEngine;

public class MoneyManager : MonoBehaviour {

    [SerializeField] private MoneyData money;
    public void AddMoney(int amount) {
        money.currentBalance += amount;
    }

    public void SubtractMoney(int amount) {
        money.currentBalance -= amount;
    }

    public void SetMoney(int amount) {
        money.currentBalance = amount;
    }
}
