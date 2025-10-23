using System.Collections;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public sealed class ButtonManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler {
    [SerializeField]
    private Vector3 clickDelta = new(0, -0.0045f, 0);

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
        _clickPosition = transform.position + clickDelta;

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
            if (Mathf.Abs((transform.position - _lerpTarget).sqrMagnitude) < clickDelta.sqrMagnitude / 80) {
                _isLerping = false;
                yield break;
            }

            transform.position = Vector3.Lerp(transform.position, _lerpTarget, clickSpeed);
            yield return null;
        }
    }
}