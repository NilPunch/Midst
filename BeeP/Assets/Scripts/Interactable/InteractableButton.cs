using UnityEngine;
using UnityEngine.Events;

public class InteractableButton : MonoBehaviour
{
    [Tooltip("Функции, которые происходят во время Нажатия кнопки интеракции  при другом коллайдере в триггере")]
    [SerializeField] private UnityEvent OnButtonPressEvent = new UnityEvent();

    private void OnTriggerStay2D(Collider2D other)
    {
        //Игрок стоит в коллайдере
        if (other.GetComponent<SpaceRewindController>() != null)
        {
            //Пусть временная кнопка взаимодействия
            if (Input.GetKeyDown(KeyCode.F))
            {
                OnButtonPressEvent?.Invoke();
            }
        }
    }
}