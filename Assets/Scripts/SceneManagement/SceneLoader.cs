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
        LoadScenes(mainMenuScenes, false);
    }

    private void LoadScenes(SceneReference[] locationsToLoad, bool showLoadingScreen) {
        AddScenesToUnload();

        _activeScene = locationsToLoad[0];

        foreach (var sceneReference in locationsToLoad) {
            if (!IsSceneLoaded(sceneReference.LoadedScene)) {
                _scenesToLoadAsyncOperations.Add(SceneManager.LoadSceneAsync(sceneReference.BuildIndex, LoadSceneMode.Additive));
            }
        }

        _scenesToLoadAsyncOperations[0].completed += SetActiveScene;
        if (showLoadingScreen) {
            loadingInterface.SetActive(true);
            StartCoroutine(TrackLoadingProgress());
        }
        else {
            _scenesToLoadAsyncOperations.Clear();
        }

        UnloadScenes();
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

    private IEnumerator TrackLoadingProgress() {
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