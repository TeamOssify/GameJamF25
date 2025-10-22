using System.Collections;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public sealed class ButtonManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler {
    [Range(0, 0.01f)]
    [SerializeField]
    private float clickDownAmount = 0.005f;

    [Range(0, 1)]
    [SerializeField]
    private float clickSpeed = 0.25f;

    private Vector3 _homePosition;
    private Vector3 _clickPosition;
    private Vector3 _lerpTarget;
    private bool _isLerping;

    [SerializeField] private UnityEvent onClick;

    private void Awake() {
        _homePosition = transform.position;
        _clickPosition = transform.position - new Vector3(0, clickDownAmount, 0);

        EnsureCameraRaycaster();
    }

    private static void EnsureCameraRaycaster() {
        var physicsRaycaster = FindFirstObjectByType<PhysicsRaycaster>();
        if (!physicsRaycaster) {
            Camera.main!.gameObject.AddComponent<PhysicsRaycaster>();
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button != PointerEventData.InputButton.Left) {
            return;
        }

        onClick?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (eventData.button != PointerEventData.InputButton.Left) {
            return;
        }

        _lerpTarget = _clickPosition;
        if (!_isLerping) {
            _isLerping = true;
            StartCoroutine(LerpPosition());
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (eventData.button != PointerEventData.InputButton.Left) {
            return;
        }

        _lerpTarget = _homePosition;
        if (!_isLerping) {
            _isLerping = true;
            StartCoroutine(LerpPosition());
        }
    }

    private IEnumerator LerpPosition() {
        while (true) {
            if (Mathf.Abs((transform.position - _lerpTarget).y) < clickDownAmount / 50) {
                _isLerping = false;
                yield break;
            }

            transform.position = Vector3.Lerp(transform.position, _lerpTarget, clickSpeed);
            yield return null;
        }
    }
}