using TMPro;

using UnityEngine;

public class DialogMessage : MonoBehaviour {
    [SerializeField] protected TextMeshProUGUI messageText;

    public void SetText(string text) {
        messageText.text = text;
    }
}