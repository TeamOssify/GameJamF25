using UnityEngine;

public class MoneyManager : MonoBehaviour {

    [SerializeField] private MoneyData money;
    public void AddMoney(int amount) {
        money.currentBalance += amount;
    }

    public void SubtractMoney(int amount) {
        money.currentBalance -= amount;
    }

    public int GetBalance() {
        return money.currentBalance;
    }

    public int GetCurrentRentCost() {
        return money.rentAmount;
    }

    public int GetCurrentSalary() {
        return money.baseSalaryPerCorrect;
    }

    public void SetMoney(int amount) {
        money.currentBalance = amount;
    }
}
