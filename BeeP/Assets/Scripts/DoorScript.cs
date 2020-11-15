using System.Collections;
using UnityEngine;

public class DoorScript : MonoBehaviour, IRestartable
{
	private const float EPS_DISTANCE = 0.1f;

    [Tooltip("Как сдвинется дверь при выполнении функции")]
    [SerializeField] private Vector3 MoveOffset = Vector3.up;
	[SerializeField] private float Speed = 0.5f;

	private Vector3 _startPosition;
	private Coroutine _moveToCoroutine = null;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    public void OpenTheDoor()
    {
		if (_moveToCoroutine != null)
		{
			StopCoroutine(_moveToCoroutine);
		}
		_moveToCoroutine = StartCoroutine(MoveTo(_startPosition + MoveOffset, Speed));
	}

    public void CloseTheDoor()
    {
		if (_moveToCoroutine != null)
		{
			StopCoroutine(_moveToCoroutine);
		}
		_moveToCoroutine = StartCoroutine(MoveTo(_startPosition, Speed));
    }

	IEnumerator MoveTo(Vector3 targetPosition, float speed)
	{
		Vector3 direction = (targetPosition - transform.position).normalized;
		while (Vector2.SqrMagnitude(transform.position - targetPosition) > EPS_DISTANCE)
		{
			transform.position += direction * speed;
			yield return null;
		}
		transform.position = targetPosition;
	}

    public void RestartFunction()
    {
		if (_moveToCoroutine != null)
			StopCoroutine(_moveToCoroutine);
        transform.position = _startPosition;
    }
}