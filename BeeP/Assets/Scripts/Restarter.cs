using UnityEngine;
using UnityEngine.Events;

public class Restarter : MonoBehaviour
{
	[Tooltip("Использовать для кастомных уникальных рестартов")]
	[SerializeField] private UnityEvent OnLevelRestart = new UnityEvent();

    private RewindController _rewindController;

    private void Awake()
    {
        _rewindController = FindObjectOfType<RewindController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!_rewindController.IsSpaceRewinding && !_rewindController.IsTimeRewinding)
                Restart();
        }
    }

    public void Restart()
    {
        foreach (IRestartable restartable in FindOfTypeMine.Find<IRestartable>())
        {
            restartable.RestartFunction();
        }
        foreach (Rigidbody2D rigidbody in FindObjectsOfType<Rigidbody2D>())
        {
            if (rigidbody.bodyType == RigidbodyType2D.Dynamic)
            {
                rigidbody.velocity = Vector2.zero;
            }
        }
		OnLevelRestart?.Invoke();
		AudioManager.Instance.Play("Restart");
	}
}