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
        timerText.text = TimeSpan.FromSeconds(shiftDuration).ToString(@"m\:ss");
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

        timerText.text = TimeSpan.FromSeconds(_currentTime).ToString(@"m\:ss");

        if (!_timerActive) {
            OnTimerEnd?.Invoke();
        }
    }
}