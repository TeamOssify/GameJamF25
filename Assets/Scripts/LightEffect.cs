using UnityEngine;

public class LightEffect : MonoBehaviour {
    public Animator lightAnimator;

    public void PlayFlash() {
        if (lightAnimator.isActiveAndEnabled) {
            lightAnimator.Play(0); // plays the first (and only) animation
        }
    }
}