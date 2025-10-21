using Eflatun.SceneReference;

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Load Event Channel")]
public class LoadEventChannelSO : ScriptableObject {
    public UnityAction<SceneReference[], bool> OnLoadingRequested;

    public void RaiseEvent(SceneReference[] locationsToLoad, bool showLoadingScreen) {
        if (OnLoadingRequested is null) {
            Debug.LogWarning("A scene load was requested, but no listeners were found on the scene load action!");
            return;
        }

        OnLoadingRequested.Invoke(locationsToLoad, showLoadingScreen);
    }
}