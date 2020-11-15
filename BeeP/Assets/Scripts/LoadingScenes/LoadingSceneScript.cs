using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneScript : MonoBehaviour
{
    private float _totalProgress;
    private int _newScene;
    private AsyncOperation _firstLoading;
    [SerializeField] private GameObject LoadingScreen = null;
    [SerializeField] private Image ProgressBar = null;
    [SerializeField] private AudioListener AudioListener = null;
    public static LoadingSceneScript Instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

		_firstLoading = SceneManager.LoadSceneAsync(SceneManager.sceneCountInBuildSettings-1, LoadSceneMode.Additive);
        _newScene = SceneManager.sceneCountInBuildSettings - 1;
        StartCoroutine(AfterFirstSceneLoaded());
    }

    private IEnumerator AfterFirstSceneLoaded()
    {
        while (!_firstLoading.isDone)
            yield return null;
        AudioListener.enabled = false;
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(_newScene));
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void LoadScene(Scene newScene, Scene currentScene)
    {
        LoadingScreen.SetActive(true);

        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)currentScene));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)newScene, LoadSceneMode.Additive));
        _newScene = (int)newScene;

        StartCoroutine(GetSceneLoadProgress());
    }

    public void LoadScene(int newScene, int currentScene)
    {

        LoadingScreen.SetActive(true);

        scenesLoading.Add(SceneManager.UnloadSceneAsync(currentScene));
        scenesLoading.Add(SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive));
        _newScene = (int)newScene;

        StartCoroutine(GetSceneLoadProgress());
    }

    public IEnumerator GetSceneLoadProgress()
    {
        AudioListener.enabled = true;

        for (int i = 0; i < scenesLoading.Count; ++i)
        {
            while(!scenesLoading[i].isDone)
            {
                _totalProgress = 0;

                foreach(AsyncOperation operation in scenesLoading)
                {
                    _totalProgress += operation.progress;
                }

                _totalProgress = (_totalProgress / scenesLoading.Count);

                ProgressBar.fillAmount = _totalProgress;

                yield return null;
            }
        }

        LoadingScreen.SetActive(false);

        AudioListener.enabled = false;

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(_newScene));
    }
}
