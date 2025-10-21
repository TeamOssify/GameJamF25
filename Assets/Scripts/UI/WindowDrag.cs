using UnityEngine;
using UnityEngine.EventSystems;

public class WindowDrag : MonoBehaviour, IDragHandler {
    private FloatingWindow _floatingWindow;

    private void Awake() {
        _floatingWindow = transform.parent.GetComponent<FloatingWindow>();
    }

    public void OnDrag(PointerEventData eventData) {
        _floatingWindow.MoveWindow(eventData.delta);
    }
}
