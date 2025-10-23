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
        correctProcessedText.text = shiftData.candidatesProcessedCorrectly.ToString();
        incorrectProcessedText.text = shiftData.candidatesProcessedIncorrectly.ToString();

        savingsValueText.text = moneyData.currentBalance.ToString();
    }

    public void SleepButtonPressed() {

    }
}
