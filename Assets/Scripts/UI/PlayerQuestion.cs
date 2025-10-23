using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public sealed class PlayerQuestion : DialogMessage {
    [SerializeField] private Button button;

    public UnityAction<string> Click;

    private void Awake() {
        button.onClick.AddListener(OnClick);
    }

    private void OnClick() {
        Click?.Invoke(messageText.text);
    }
}