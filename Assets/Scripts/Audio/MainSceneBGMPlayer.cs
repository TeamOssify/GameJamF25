using System;

using UnityEngine;

public class MainSceneBGMPlayer : MonoBehaviour {
    [SerializeField] private AudioClip bgm;
    [SerializeField] private BackgroundMusicEventChannelSO bgmEventChannel;

    private void Awake() {
        bgmEventChannel.ChangeBgmFade(bgm);
    }
}
