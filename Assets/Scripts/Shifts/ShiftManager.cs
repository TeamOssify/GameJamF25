using Eflatun.SceneReference;

using UnityEngine;

public class ShiftManager : MonoBehaviour {
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private ShiftData shiftData;
    [SerializeField] private ShiftTimer shiftTimer;
    [SerializeField] private LoadEventChannelSO loadEventChannel;
    [SerializeField] private SceneReference reportScene;

    //starts timer and increments shift number
    public void StartShift() {
        shiftTimer.StartTimer();
        shiftData.shiftNumber++;
    }

    // checks if the shift should end and ends it
    public void CheckShiftEnd() {
        if (shiftTimer.TimerEnded)
        {
            EndShift();
        }
    }

    // switches the scene to the report screen
    public void EndShift() {
        loadEventChannel.RaiseEvent(reportScene, false);
    }
}