using System;

using TMPro;

using UnityEngine;

public class ShiftTimer : MonoBehaviour {
    public bool timerEnded;
    [SerializeField] private float shiftDuration = 180.0f;
    [SerializeField] private TextMeshProUGUI timerText;

    private float _currentTime;
    private bool _timerActive = false;

    public void StartTimer() 
    {
        _currentTime = shiftDuration;

        timerText.text = TimeSpan.FromSeconds(shiftDuration).ToString(@"mm\:ss");
        _timerActive = true;
        timerEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        while (_timerActive) {
            _currentTime -= Time.deltaTime;

            timerText.text = TimeSpan.FromSeconds(shiftDuration).ToString(@"mm\:ss");

            if (_currentTime <= 0) {
                _timerActive = false;
                timerEnded = true;
            }
        }
    }
}
