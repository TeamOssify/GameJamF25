using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public sealed class FloatingWindow : MonoBehaviour {
    [SerializeField] private Sprite visibleSprite;
    [SerializeField] private Sprite collapsedSprite;
    [SerializeField] private Image collapseButton;
    [SerializeField] private GameObject scrollContainer;

    private Canvas _canvas;
    private RectTransform _canvasTransform;
    private RectTransform _windowTransform;

    private void Awake() {
        _canvas = GetComponentInParent<Canvas>();
        _canvasTransform = _canvas.transform as RectTransform;
        _windowTransform = transform as RectTransform;
    }

    private void Start() {
        StartCoroutine(DeferMoveWindow(Vector2.zero));
    }

    public void MoveWindow(Vector2 delta) {
        var newPos = _windowTransform.anchoredPosition + (delta / _canvas.scaleFactor);

        var windowMin = new Vector2(_windowTransform.rect.size.x / 2, 0);
        var windowMax = _canvasTransform.rect.size - new Vector2(_windowTransform.rect.size.x / 2, _windowTransform.rect.size.y);

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

    public void ToggleWindowVisible() {
        var shouldShow = !scrollContainer.activeSelf;

        collapseButton.sprite = shouldShow ? visibleSprite : collapsedSprite;
        scrollContainer.SetActive(shouldShow);

        StartCoroutine(DeferMoveWindow(Vector2.zero));
    }

    private IEnumerator DeferMoveWindow(Vector2 delta) {
        yield return null;
        MoveWindow(delta);
    }
}