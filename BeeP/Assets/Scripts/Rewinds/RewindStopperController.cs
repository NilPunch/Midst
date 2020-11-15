using UnityEngine;

public class RewindStopperController : MonoBehaviour
{
    public static RewindStopperController Instance;

    public bool DoNotRecordPause;
    public bool DontRecordStaticRigidbodies;

    public bool DontRecord => DoNotRecordPause || DontRecordStaticRigidbodies;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
}
