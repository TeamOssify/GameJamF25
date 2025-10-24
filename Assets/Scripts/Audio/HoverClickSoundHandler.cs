using UnityEngine;
using UnityEngine.EventSystems;

public sealed class HoverClickSoundHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    [SerializeField] private SfxEventChannelSO sfxEventChannel;

    [Header("Hover Enter")]
    [SerializeField] private AudioClip hoverEnterSound;
    [Range(0, 1)]
    [SerializeField] private float hoverEnterVolume = 0.5f;

    [Header("Hover Exit")]
    [SerializeField] private AudioClip hoverExitSound;
    [Range(0, 1)]
    [SerializeField] private float hoverExitVolume = 0.5f;

    [Header("Click")]
    [SerializeField] private AudioClip clickSound;
    [Range(0, 1)]
    [SerializeField] private float clickVolume = 0.5f;

    public void OnPointerEnter(PointerEventData eventData) {
        if (hoverEnterSound) {
            sfxEventChannel?.PlayVolumedSoundEffect(hoverEnterSound, hoverEnterVolume);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (hoverExitSound) {
            sfxEventChannel?.PlayVolumedSoundEffect(hoverExitSound, hoverExitVolume);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (clickSound && eventData.button == PointerEventData.InputButton.Left) {
            sfxEventChannel?.PlayVolumedSoundEffect(clickSound, clickVolume);
        }
    }
}