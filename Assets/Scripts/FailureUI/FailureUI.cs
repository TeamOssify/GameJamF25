using System;

using Eflatun.SceneReference;

using TMPro;

using UnityEngine;

public class FailureUI : MonoBehaviour
{
    [SerializeField] private LoadEventChannelSO loadEventChannel;
    [SerializeField] private SceneReference mainMenu;
    [SerializeField] private TextMeshProUGUI failureDescription;
    [SerializeField] private ShiftData shiftData;
    [SerializeField] private TextMeshProUGUI daysNumber;


    private void Awake() {
        daysNumber.text = shiftData.shiftNumber.ToString();
        shiftData.ResetAll();
    }

    public void ReturnToMenu() {
        loadEventChannel.RaiseEvent(mainMenu, SceneLoadType.LoadingScreen);
    }
}
