using UnityEngine;

public sealed class SfxManager : MonoBehaviour {
    [SerializeField] private SfxEventChannelSO sfxEventChannel;

    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();

        if (!_audioSource) {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnEnable() {
        sfxEventChannel.OnPlaySoundEffect += PlaySoundEffect;
    }

    private void OnDisable() {
        sfxEventChannel.OnPlaySoundEffect -= PlaySoundEffect;
    }

    public void PlaySoundEffect(AudioClip clip) {
        _audioSource.PlayOneShot(clip);
    }

    public void PlaySoundEffect(AudioClip clip, float volume) {
        _audioSource.PlayOneShot(clip, volume);
    }

    public void StopAllSoundEffects() {
        _audioSource.Stop();
    }
}