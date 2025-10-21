using UnityEngine;

public sealed class FloatingWindow : MonoBehaviour {
    private Canvas _canvas;
    private RectTransform _canvasTransform;
    private RectTransform _windowTransform;

    private void Awake() {
        _canvas = GetComponentInParent<Canvas>();
        _canvasTransform = _canvas.transform as RectTransform;
        _windowTransform = transform as RectTransform;
    }

    public void MoveWindow(Vector2 delta) {
        var newPos = _windowTransform.anchoredPosition + (delta / _canvas.scaleFactor);

        var windowMin = _windowTransform.rect.size / 2;
        var windowMax = _canvasTransform.rect.size - (_windowTransform.rect.size / 2);

        if (newPos.x > windowMax.x) {
            newPos.x = windowMax.x;
        }
        else if (newPos.x < windowMin.x) {
            newPos.x = windowMin.x;
        }

        if (newPos.y < -windowMax.y) {
            newPos.y = -windowMax.y;
        }
        else if (newPos.y > -windowMin.y) {
            newPos.y = -windowMin.y;
        }

        _windowTransform.anchoredPosition = newPos;
    }
}