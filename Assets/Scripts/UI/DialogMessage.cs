using TMPro;

using UnityEngine;

public sealed class DialogMessage : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI messageText;

    public void SetText(string text) {
        messageText.text = text;
    }
}