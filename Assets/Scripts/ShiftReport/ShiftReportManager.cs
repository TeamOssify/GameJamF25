using TMPro;

using UnityEngine;

public class ShiftReportManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI endMessage;
    [SerializeField] private TextMeshProUGUI dayText;

    [SerializeField] private TextMeshProUGUI salaryText;
    [SerializeField] private TextMeshProUGUI salaryValueText;
    [SerializeField] private TextMeshProUGUI savingsValueText;
    [SerializeField] private TextMeshProUGUI rentCostText;
    [SerializeField] private TextMeshProUGUI totalBalanceText;

    [SerializeField] private TextMeshProUGUI correctProcessedText;
    [SerializeField] private TextMeshProUGUI incorrectProcessedText;
    [SerializeField] private TextMeshProUGUI totalProcessedText;

    [SerializeField] private ShiftData shiftData;

    [SerializeField] private MoneyManager moneyManager;


    void Awake() {
        UpdateShiftReportUI();
        UpdateBalance();

    }

    public void SleepButtonPressed() {

    }

    private void UpdateBalance() {
        moneyManager.AddMoney(shiftData.candidatesProcessedCorrectly * moneyManager.GetCurrentSalary());
        moneyManager.SubtractMoney(moneyManager.GetCurrentRentCost());

        totalBalanceText.text = "$" + moneyManager.GetBalance();
    }

    private void UpdateShiftReportUI() {
        dayText.text = "Day " + shiftData.shiftNumber;

        correctProcessedText.text = "Correct: " + shiftData.candidatesProcessedCorrectly;
        incorrectProcessedText.text = "Incorrect: " + shiftData.candidatesProcessedIncorrectly;
        totalProcessedText.text = "Total: " + (shiftData.candidatesProcessedCorrectly + shiftData.candidatesProcessedIncorrectly);

        savingsValueText.text = moneyManager.GetBalance().ToString();
        rentCostText.text = moneyManager.GetCurrentRentCost().ToString();
        salaryText.text = "Salary (" + shiftData.candidatesProcessedCorrectly + ")";
        salaryValueText.text = (shiftData.candidatesProcessedCorrectly * moneyManager.GetCurrentSalary()).ToString();
    }
}
