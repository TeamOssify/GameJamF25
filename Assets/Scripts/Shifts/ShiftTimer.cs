using UnityEngine;

public class Timer : MonoBehaviour {

    
    public bool timerEnded;
    [SerializeField] private float shiftDuration = 180.0f;
    private float currentTime;
    private bool timerActive = false;

    public void StartTimer() 
    {
        currentTime = shiftDuration;
        timerActive = true;
        timerEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        while (timerActive) {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0) {
                timerActive = false;
                timerEnded = true;
            }
        }
    }
}
