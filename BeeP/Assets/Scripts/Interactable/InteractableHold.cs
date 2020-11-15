using UnityEngine;
using UnityEngine.Events;

public class InteractableHold : MonoBehaviour
{
    [Tooltip("Функции, которые происходят во время входа другого коллайдера в триггер")]
    [SerializeField] private UnityEvent OnTriggerEnterEvent = new UnityEvent();
    [Tooltip("Функции, которые происходят во время выхода другого коллайдера в триггер")]
    [SerializeField] private UnityEvent OnTriggerExitEvent = new UnityEvent();

    private int _amountOfPushes = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _amountOfPushes++;
        OnTriggerEnterEvent?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        --_amountOfPushes;
        if (_amountOfPushes == 0)
            OnTriggerExitEvent?.Invoke();
    }
}