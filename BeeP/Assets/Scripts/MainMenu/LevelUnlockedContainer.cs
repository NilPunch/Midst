using UnityEngine;
/// <summary>
/// SINGLETON
/// </summary>
public class LevelUnlockedContainer : MonoBehaviour
{
    public static LevelUnlockedContainer Instance;

    public int MaxLevelAchieved = 1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
			Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void UpdateMaxLevel(int level)
    {
        if (level > MaxLevelAchieved)
            MaxLevelAchieved = level;
    }
}
