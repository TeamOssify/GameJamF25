using System.Collections;

using UnityEngine;

public sealed class BackgroundMusicManager : MonoBehaviour {
    [SerializeField] private BackgroundMusicEventChannelSO backgroundMusicEventChannel;

    [SerializeField]
    [Tooltip("The time in seconds it takes for the BGM to fade in or out when switching tracks.")]
    [Min(0.01f)]
    private float bgmFadeTime = 0.75f;

    [SerializeField]
    [Range(0, 1)]
    private float bgmVolume = 1f;

    private AudioSource _audioSource;
    private Coroutine _fadeCoroutine;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        if (!_audioSource) {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        _audioSource.volume = bgmVolume;
        _audioSource.loop = true;
    }

    private void OnEnable() {
        backgroundMusicEventChannel.OnChangeBgmFade += ChangeBgmFade;
        backgroundMusicEventChannel.OnChangeBgmImmediate += ChangeBgmImmediate;
        backgroundMusicEventChannel.OnPauseBgm += PauseBgm;
        backgroundMusicEventChannel.OnUnpauseBgm += UnpauseBgm;
    }

    private void OnDisable() {
        backgroundMusicEventChannel.OnChangeBgmFade -= ChangeBgmFade;
        backgroundMusicEventChannel.OnChangeBgmImmediate -= ChangeBgmImmediate;
        backgroundMusicEventChannel.OnPauseBgm -= PauseBgm;
        backgroundMusicEventChannel.OnUnpauseBgm -= UnpauseBgm;
    }

    public void ChangeBgmFade(AudioClip clip) {
        EnsureNotFading();

        _fadeCoroutine = StartCoroutine(CoChangeBgmFade(clip));
    }

    private IEnumerator CoChangeBgmFade(AudioClip clip) {
        if (_audioSource.isPlaying) {
            yield return _audioSource.FadeOutToStop(bgmFadeTime / 2);
        }

        _audioSource.clip = clip;

        yield return _audioSource.FadeIn(bgmVolume, bgmFadeTime / 2);

        _fadeCoroutine = null;
    }

    public void ChangeBgmImmediate(AudioClip clip) {
        EnsureNotFading();

        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.volume = bgmVolume;
        _audioSource.Play();
    }

    public void PauseBgm() {
        EnsureNotFading();

        _audioSource.Pause();
    }

    public void UnpauseBgm() {
        EnsureNotFading();

        _audioSource.volume = bgmVolume;
        _audioSource.UnPause();
    }

    private void EnsureNotFading() {
        if (_fadeCoroutine != null) {
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null;
        }
    }
}