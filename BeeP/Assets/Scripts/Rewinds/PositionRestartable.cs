using UnityEngine;

public class PositionRestartable : MonoBehaviour, IRestartable
{
    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    public void RestartFunction()
    {
        transform.position = _startPosition;
    }
}
