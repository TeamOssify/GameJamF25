using TMPro;

using UnityEngine;

public class ShiftReportUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI endMessage;
    [SerializeField] private TextMeshProUGUI dayText;

    [SerializeField] private TextMeshProUGUI salaryText;
    [SerializeField] private TextMeshProUGUI salaryValueText;
    [SerializeField] private TextMeshProUGUI savingsValueText;
    [SerializeField] private TextMeshProUGUI rentCostText;

    [SerializeField] private TextMeshProUGUI correctProcessedText;
    [SerializeField] private TextMeshProUGUI incorrectProcessedText;
    [SerializeField] private TextMeshProUGUI totalProcessedText;

    [SerializeField] private ShiftData shiftData;
    [SerializeField] private MoneyData moneyData;

    void Awake() {
        correctProcessedText.text = "Correct: " + shiftData.candidatesProcessedCorrectly;
        incorrectProcessedText.text = "Incorrect: " + shiftData.candidatesProcessedIncorrectly;
        totalProcessedText.text = "Total: " + (shiftData.candidatesProcessedCorrectly + shiftData.candidatesProcessedIncorrectly);

        savingsValueText.text = moneyData.currentBalance.ToString();
        rentCostText.text = moneyData.rentAmount.ToString();
        salaryText.text = "Salary (" + shiftData.candidatesProcessedCorrectly + ")";
        salaryValueText.text = (shiftData.candidatesProcessedCorrectly * moneyData.baseSalaryPerCorrect).ToString();
    }

    public void SleepButtonPressed() {

    }
}
