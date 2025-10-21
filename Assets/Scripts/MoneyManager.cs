using UnityEngine;

public class MoneyManager : MonoBehaviour {

    [SerializeField] private int money;

    public void AddMoney(int amount) {
        money += amount;
    }

    public void SubtractMoney(int amount) {
        money -= amount;
    }

    public void SetMoney(int amount) {
        money = amount;
    }
}
