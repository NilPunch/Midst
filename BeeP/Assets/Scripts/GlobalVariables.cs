using UnityEngine;
/// <summary>
/// SINGLETON
/// </summary>
public class GlobalVariables : MonoBehaviour
{
    public int FPS = 60;
    public int RecordedSeconds  = 15;

    public static GlobalVariables Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        Application.targetFrameRate = FPS;

        DontDestroyOnLoad(gameObject);
    }
}