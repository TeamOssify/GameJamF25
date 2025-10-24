using System.Linq;

using Eflatun.SceneReference;

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Load Event Channel")]
public class LoadEventChannelSO : ScriptableObject {
    public UnityAction<SceneReference[], SceneLoadType> OnLoadingRequested;

    public void RaiseEvent(SceneReference locationToLoad, SceneLoadType sceneLoadType)
        => RaiseEvent(new[] { locationToLoad }, sceneLoadType);

    public void RaiseEvent(SceneReference[] locationsToLoad, SceneLoadType sceneLoadType) {
        Debug.Assert(locationsToLoad != null, "A scene load was requestioned with a null array of locations to load!");
        Debug.Assert(locationsToLoad.All(x => x != null), "A scene load was requested,but the array of locations to load contains 1 or more null scenes!");
        Debug.Assert(OnLoadingRequested != null, "A scene load was requested, but no listeners were found on the scene load action!");

        OnLoadingRequested?.Invoke(locationsToLoad, sceneLoadType);
    }
}