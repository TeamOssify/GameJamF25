using System.Globalization;

using TMPro;

using UnityEngine;

public class MainMenu : MonoBehaviour {
    [SerializeField] private GameObject daysHighScoreObject;

    private void Awake() {
        var daysHighScore = PlayerPrefs.GetInt(Constants.PlayerPrefsKeys.HIGH_SCORE);
        var daysHighScoreText = daysHighScoreObject.GetComponent<TextMeshProUGUI>();
        daysHighScoreText.text = string.Format(CultureInfo.InvariantCulture, daysHighScoreText.text, daysHighScore);
    }

    public void PlayButton_OnClick() {
        Debug.Log("PlayButton_OnClick");
    }

    public void ExitButton_OnClick() {
        Application.Quit();
    }
}