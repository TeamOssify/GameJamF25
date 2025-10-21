using System.Runtime.CompilerServices;

using Eflatun.SceneReference;

using UnityEngine;
using UnityEngine.SceneManagement;

public class ShiftManager : MonoBehaviour
{
    private MoneyManager money = new MoneyManager();
    private Timer shiftTimer = new Timer();
    private int shiftNumber = 1;
    private int rent;
    private int candidateNumber = 0;

    [SerializeField] private LoadEventChannelSO LoadEventChannel;
    [SerializeField] private SceneReference reportScene;

    public void StartShift() //opens the scene, sets some initial values
    {
        money.SetMoney(10);
        shiftNumber = 1;
        rent = 25 + shiftNumber * 5;
    }

    public void CallFirstCandidate() //brings in the first candidate, starts the timer
    {
        shiftTimer.StartTimer();

        candidateNumber++;
        //call first candidate
    }

    public void CallNextCandidate() // brings in the next candidate
    { 
        if (shiftTimer.timerEnded) 
        { 
            EndShift();
        }

        //dismiss current candidate
        candidateNumber++;
        //call new candidate

    }

    public void EndShift() // switches the scene to the report screen
    {
        shiftNumber++;
        LoadEventChannel.RaiseEvent(reportScene, false);
    }
}
