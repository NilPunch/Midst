using UnityEngine;
using UnityEngine.Events;

public class InteractableEnter : MonoBehaviour
{
    [Tooltip("Функции, которые происходят во время входа другого коллайдера в триггер")]
    [SerializeField] private UnityEvent OnTriggerEnterEvent = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D other)
    {
		//if (other.GetComponent<PlayerController>() != null)
			OnTriggerEnterEvent?.Invoke();
    }
}