using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/Background Music Event Channel")]
public sealed class BackgroundMusicEventChannelSO : ScriptableObject {
    public UnityAction<AudioClip> OnChangeBgmFade;
    public UnityAction<AudioClip> OnChangeBgmImmediate;

    public UnityAction OnPauseBgm;
    public UnityAction OnUnpauseBgm;

    public void ChangeBgmFade(AudioClip clip) {
        Debug.Assert(OnChangeBgmFade != null, "OnChangeBgmFade != null");

        OnChangeBgmFade?.Invoke(clip);
    }

    public void ChangeBgmImmediate(AudioClip clip) {
        Debug.Assert(OnChangeBgmImmediate != null, "OnChangeBgmImmediate != null");

        OnChangeBgmImmediate?.Invoke(clip);
    }

    public void PauseBgm() {
        Debug.Assert(OnPauseBgm != null, "OnPauseBgm != null");

        OnPauseBgm?.Invoke();
    }

    public void UnpauseBgm() {
        Debug.Assert(OnUnpauseBgm != null, "OnUnpauseBgm != null");

        OnUnpauseBgm?.Invoke();
    }
}