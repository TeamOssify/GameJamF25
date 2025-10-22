using System.Runtime.CompilerServices;

using Eflatun.SceneReference;

using UnityEngine;
using UnityEngine.SceneManagement;

public class ShiftManager : MonoBehaviour {
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private ShiftData shiftData;
    [SerializeField] private ShiftTimer shiftTimer;


    [SerializeField] private LoadEventChannelSO loadEventChannel;
    [SerializeField] private SceneReference reportScene;

    public void StartShift() //starts timer and increments shift number
    {
        //start timer
        shiftData.shiftNumber++;

    }

    public void CheckShiftEnd() // checks if the shift should end and ends it
    { 
        // if (_shiftTimer.timerEnded)
        // {
        //     EndShift();
        // }
    }


    public void EndShift() // switches the scene to the report screen
    {
        loadEventChannel.RaiseEvent(reportScene, false);
    }
}
