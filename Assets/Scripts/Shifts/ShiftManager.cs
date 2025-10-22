using System.Runtime.CompilerServices;

using Eflatun.SceneReference;

using UnityEngine;
using UnityEngine.SceneManagement;

public class ShiftManager : MonoBehaviour {
    [SerializeField]
    private MoneyManager moneyManager;
    [SerializeField]
    private ShiftData shiftData;

    private Timer shiftTimer = new Timer();
    private int candidateNumber = 0;

    [SerializeField] private LoadEventChannelSO LoadEventChannel;
    [SerializeField] private SceneReference reportScene;

    public void StartShift() //starts timer and increments shift number
    {
        shiftData.shiftNumber++;
        shiftTimer.StartTimer();
    }

    public void CheckShiftEnd() // checks if the shift should end and ends it
    { 
        if (shiftTimer.timerEnded) 
        { 
            EndShift();
        }
    }


    public void EndShift() // switches the scene to the report screen
    {
        LoadEventChannel.RaiseEvent(reportScene, false);
    }
}
