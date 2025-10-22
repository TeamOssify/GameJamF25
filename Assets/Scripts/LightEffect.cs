using UnityEngine;

public class LightEffect : MonoBehaviour {
    public Animator lightAnimator;

    public void PlayFlash() {
        lightAnimator.Play(0); // plays the first (and only) animation
    }
}