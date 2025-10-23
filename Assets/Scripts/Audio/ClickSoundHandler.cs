using UnityEngine;
using UnityEngine.EventSystems;

public sealed class ClickSoundHandler : MonoBehaviour, IPointerClickHandler {
    [SerializeField] private SfxEventChannelSO sfxEventChannel;
    [SerializeField] private AudioClip clickSound;

    [SerializeField]
    [Range(0, 1)]
    private float volume = 0.5f;

    public void OnPointerClick(PointerEventData eventData) {
        if (clickSound && eventData.button == PointerEventData.InputButton.Left) {
            sfxEventChannel.PlayVolumedSoundEffect(clickSound, volume);
        }
    }
}