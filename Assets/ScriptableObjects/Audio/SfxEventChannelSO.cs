using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/SFX Event Channel")]
public sealed class SfxEventChannelSO : ScriptableObject {
    public UnityAction<AudioClip> OnPlaySoundEffect;
    public UnityAction<AudioClip, float> OnPlayVolumedSoundEffect;
    public UnityAction OnStopAllSoundEffects;

    public void PlaySoundEffect(AudioClip clip) {
        Debug.Assert(OnPlaySoundEffect != null, "OnPlaySoundEffect != null");

        OnPlaySoundEffect.Invoke(clip);
    }

    public void PlayVolumedSoundEffect(AudioClip clip, float volume) {
        Debug.Assert(OnPlayVolumedSoundEffect != null, "OnPlayVolumedSoundEffect != null");

        OnPlayVolumedSoundEffect.Invoke(clip, volume);
    }

    public void StopAllSoundEffects() {
        Debug.Assert(OnStopAllSoundEffects != null, "OnStopAllSoundEffects != null");

        OnStopAllSoundEffects.Invoke();
    }
}