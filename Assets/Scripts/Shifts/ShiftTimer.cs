using System;

using TMPro;

using UnityEngine;
using UnityEngine.Events;

public class ShiftTimer : MonoBehaviour {
    [SerializeField] private float shiftDuration = 180.0f;
    [SerializeField] private TextMeshPro timerText;

    private float _currentTime;
    private bool _timerActive;

    public bool TimerEnded => !_timerActive;
    public UnityEvent OnTimerEnd;

    private void Awake() {
        SetTimerText(shiftDuration);
    }

    public void StartTimer() {
        _currentTime = shiftDuration;
        _timerActive = true;
    }

    private void FixedUpdate() {
        if (!_timerActive) {
            return;
        }

        _currentTime -= Time.fixedDeltaTime;

        if (_currentTime <= 0) {
            _timerActive = false;
            _currentTime = 0;
        }

        SetTimerText(_currentTime);

        if (!_timerActive) {
            OnTimerEnd?.Invoke();
        }
    }

    private void SetTimerText(float time) {
        timerText.text = TimeSpan.FromSeconds(time).ToString(@"m\:ss");
    }
}