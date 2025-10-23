using System.Globalization;

using Eflatun.SceneReference;

using TMPro;

using UnityEngine;

public class MainMenu : MonoBehaviour {
    [SerializeField] private LoadEventChannelSO loadEventChannel;
    [SerializeField] private SceneReference mainScene;
    [SerializeField] private TextMeshProUGUI daysHighScoreText;

    private void Awake() {
        var daysHighScore = PlayerPrefs.GetInt(Constants.PlayerPrefsKeys.HIGH_SCORE);
        daysHighScoreText.text = string.Format(CultureInfo.InvariantCulture, daysHighScoreText.text, daysHighScore);
    }

    public void PlayButton_OnClick() {
        Debug.Log("Loading main scene from main menu");
        loadEventChannel.RaiseEvent(mainScene, true);
    }

    public void ExitButton_OnClick() {
        Application.Quit();
    }
}