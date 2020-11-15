using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingButtonScript : MonoBehaviour
{
    [Tooltip("Номер ТЕКУЩЕЙ сцены кнопки, на которой она находится")]
    [SerializeField] private int CurrentScene = 1;

    /// <summary>
    /// Загрузка новой сцены
    /// </summary>
    /// <param name="scene"> Номер новой сцены </param>
    public void LoadScene(int scene)
    {
        //Debug.Log(SceneManager.sceneCountInBuildSettings);

        if (CurrentScene != SceneManager.GetActiveScene().buildIndex)
        {
            //Debug.LogWarning("На сцене " + SceneManager.GetActiveScene().name + " у одного из объектов LoadingButtonScript неверно выставлен параметр CurrentScene. Он был изменен автоматически");
            CurrentScene = SceneManager.GetActiveScene().buildIndex;
        }

        //if (scene != SceneManager.sceneCountInBuildSettings - 1)
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (scene != SceneManager.GetActiveScene().buildIndex + 1 && scene != SceneManager.sceneCountInBuildSettings - 1)
            {
                //Debug.LogWarning("Сцена " + SceneManager.GetActiveScene().name + " LoadingButtonScript параметр Scene " + scene + ". Он был изменен автоматически");
                scene = SceneManager.GetActiveScene().buildIndex + 1;
            }
        }

        if (scene > 0 && scene < SceneManager.sceneCountInBuildSettings - 1)
            LevelUnlockedContainer.Instance.UpdateMaxLevel(scene);

        LoadingSceneScript.Instance.LoadScene(scene, CurrentScene);
    }

    public void LoadMainMenu()
    {
        LoadingSceneScript.Instance.LoadScene( SceneManager.sceneCountInBuildSettings - 1, SceneManager.GetActiveScene().buildIndex);
    }
}
