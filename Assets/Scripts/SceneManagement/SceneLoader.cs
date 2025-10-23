using System.Collections;
using System.Collections.Generic;

using Eflatun.SceneReference;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class SceneLoader : MonoBehaviour {
    [Header("Initialization Scene")]
    [SerializeField] private SceneReference initializationScene;

    [Header("Load on start")]
    [SerializeField] private SceneReference[] mainMenuScenes;

    [Header("Loading Screen")]
    [SerializeField] private GameObject loadingInterface;
    [SerializeField] private Image loadingProgressBar;

    [Header("Black fade in out")]
    // [SerializeField]
    private Image blackFadeInOut;
    [Min(0.1f)]
    // [SerializeField]
    private float fadeDuration = 0.5f;

    [Header("Load Event")]
    [SerializeField] private LoadEventChannelSO loadEventChannel;

    private readonly List<AsyncOperation> _scenesToLoadAsyncOperations = new();
    private readonly List<Scene> _scenesToUnload = new();

    private SceneReference _activeScene;

    private void OnEnable() {
        loadEventChannel.OnLoadingRequested += LoadScenes;
    }

    private void OnDisable() {
        loadEventChannel.OnLoadingRequested -= LoadScenes;
    }

    private void Start() {
        if (SceneManager.GetActiveScene().buildIndex == initializationScene.BuildIndex) {
            LoadMainMenu();
        }
    }

    private void LoadMainMenu() {
        LoadScenes(mainMenuScenes, SceneLoadType.Immediate);
    }

    private void LoadScenes(SceneReference[] locationsToLoad, SceneLoadType sceneLoadType) {
        Debug.Log($"Loading {locationsToLoad.Length} scenes with load type {sceneLoadType}");

        AddScenesToUnload();

        _activeScene = locationsToLoad[0];

        foreach (var sceneReference in locationsToLoad) {
            // This should never really happen except when working in the editor
            if (IsSceneLoaded(sceneReference.LoadedScene)) {
                _scenesToUnload.Remove(sceneReference.LoadedScene);
                continue;
            }

            _scenesToLoadAsyncOperations.Add(SceneManager.LoadSceneAsync(sceneReference.BuildIndex, LoadSceneMode.Additive));
        }

        if (_scenesToLoadAsyncOperations.Count > 0) {
            _scenesToLoadAsyncOperations[0].completed += SetActiveScene;

            if (sceneLoadType != SceneLoadType.Immediate) {
                HandleLoadingTransition(sceneLoadType);
            }
            else {
                _scenesToLoadAsyncOperations.Clear();
            }
        }

        UnloadScenes();
    }

    private void HandleLoadingTransition(SceneLoadType sceneLoadType) {
        if (sceneLoadType == SceneLoadType.LoadingScreen) {
            loadingInterface.SetActive(true);
            StartCoroutine(CoTrackLoadingProgress());
        }
        // else if (sceneLoadType == SceneLoadType.BlackFadeInOut) {
        //     blackFadeInOut.gameObject.SetActive(true);
        //     StartCoroutine(CoBlackFadeInOut());
        // }
    }

    private void SetActiveScene(AsyncOperation asyncOp) {
        SceneManager.SetActiveScene(_activeScene.LoadedScene);
    }

    private void AddScenesToUnload() {
        for (var i = 0; i < SceneManager.sceneCount; ++i) {
            var scene = SceneManager.GetSceneAt(i);

            if (scene.buildIndex != initializationScene.BuildIndex) {
                _scenesToUnload.Add(scene);
            }
        }
    }

    private void UnloadScenes() {
        foreach (var scene in _scenesToUnload) {
            SceneManager.UnloadSceneAsync(scene);
        }

        _scenesToUnload.Clear();
    }

    private bool IsSceneLoaded(Scene scene) {
        for (var i = 0; i < SceneManager.sceneCount; ++i) {
            var sceneAt = SceneManager.GetSceneAt(i);
            if (sceneAt.buildIndex == scene.buildIndex) {
                return true;
            }
        }

        return false;
    }

    // Rip the time I spent on this
    // The next scene usually loads before the black is even fully opaque
    private IEnumerator CoBlackFadeInOut() {
        blackFadeInOut.color = new Color(0, 0, 0, 0);

        var fadeDurationThird = fadeDuration / 3;

        var duration = 0f;
        while (duration < fadeDurationThird) {
            duration += Time.deltaTime;
            if (duration > fadeDurationThird) {
                duration = fadeDurationThird;
            }

            var opacity = Mathf.Lerp(0, 1, duration / fadeDurationThird);
            blackFadeInOut.color = new Color(0, 0, 0, opacity);
            yield return null;
        }

        yield return WaitForSecondsCache.Get(fadeDurationThird);

        duration = 0f;
        while (duration < fadeDurationThird) {
            duration += Time.deltaTime;
            if (duration > fadeDurationThird) {
                duration = fadeDurationThird;
            }

            var opacity = Mathf.Lerp(1, 0, duration / fadeDurationThird);
            blackFadeInOut.color = new Color(0, 0, 0, opacity);
            yield return null;
        }

        blackFadeInOut.gameObject.SetActive(false);
    }

    private IEnumerator CoTrackLoadingProgress() {
        var totalProgress = 0f;

        // When the scene reaches 0.9f, it means that it is loaded
        // The remaining 0.1f are for the integration
        while (totalProgress <= 0.9f) {
            totalProgress = 0;

            foreach (var asyncOperation in _scenesToLoadAsyncOperations) {
                totalProgress += asyncOperation.progress;
            }

            loadingProgressBar.fillAmount = totalProgress / _scenesToLoadAsyncOperations.Count;

            yield return null;
        }

        _scenesToLoadAsyncOperations.Clear();

        loadingInterface.SetActive(false);
    }
}