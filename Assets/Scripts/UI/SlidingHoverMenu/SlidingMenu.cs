
using System.Collections;

using UnityEngine;

public class SlidingMenu : MonoBehaviour {
    [SerializeField] private float slideSpeed;
    [SerializeField] private AnimationCurve slideCurve = AnimationCurve.EaseInOut(0,0,1,1);

    [SerializeField] private Vector3 hidePosition;
    [SerializeField] private Vector3 showPosition;

    private RectTransform rectTransform;
    private bool isShown = false;

    private Coroutine slideCoroutine;
    void Awake() {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = hidePosition;
    }

    public void ShowMenu() {
        if (isShown) return;
        isShown = true;

        if (slideCoroutine != null) {
            StopCoroutine(slideCoroutine);
        }

        slideCoroutine = StartCoroutine(Slide(showPosition));
    }

    public void HideMenu() {
        if (!isShown) return;
        isShown = false;

        if (slideCoroutine != null) {
            StopCoroutine(slideCoroutine);
        }

        slideCoroutine = StartCoroutine(Slide(hidePosition));
    }

    private IEnumerator Slide(Vector3 target) {
        var startPosition = rectTransform.anchoredPosition;

        var distance = Vector3.Distance(startPosition, target);
        var duration =  distance / (slideSpeed * 100f);
        var elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            var t = elapsedTime / duration;
            var curvedT = slideCurve.Evaluate(t);

            rectTransform.anchoredPosition = Vector3.Lerp(startPosition, target, curvedT);

            yield return null;
        }

        rectTransform.anchoredPosition = target;
    }
}
