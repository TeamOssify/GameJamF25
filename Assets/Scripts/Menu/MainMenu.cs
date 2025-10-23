using System;
using System.Globalization;

using Eflatun.SceneReference;

using TMPro;

using UnityEngine;

public class MainMenu : MonoBehaviour {
    [SerializeField] private LoadEventChannelSO loadEventChannel;
    [SerializeField] private BackgroundMusicEventChannelSO backgroundMusicEventChannel;
    [SerializeField] private SceneReference mainScene;
    [SerializeField] private TextMeshProUGUI daysHighScoreText;

    [SerializeField] private AudioClip backgroundMusic;

    private void Awake() {
        var daysHighScore = PlayerPrefs.GetInt(Constants.PlayerPrefsKeys.HIGH_SCORE);
        daysHighScoreText.text = string.Format(CultureInfo.InvariantCulture, daysHighScoreText.text, daysHighScore);
    }

    private void Start() {
        backgroundMusicEventChannel.ChangeBgmFade(backgroundMusic);
    }

    public void PlayButton_OnClick() {
        Debug.Log("Loading main scene from main menu");
        loadEventChannel.RaiseEvent(mainScene, SceneLoadType.Immediate);
    }

    public void ExitButton_OnClick() {
        Application.Quit();
    }
}