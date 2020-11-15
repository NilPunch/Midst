using UnityEngine;

public class ButtonDropDownScript : MonoBehaviour, IRestartable
{
    private Vector3 _startPos;
    [SerializeField] private Vector3 DropDistance = Vector3.down;

    private void Awake()
    {
        _startPos = transform.localPosition;
    }

    public void DropDown()
    {
        if (transform.localPosition == _startPos)
        {
            transform.localPosition += DropDistance;
            AudioManager.Instance.Play("Interact");
        }
    }

    public void DropUp()
    {
        if (transform.localPosition == _startPos + DropDistance)
        {
            transform.localPosition = _startPos;
           //AudioManager.Instance.Play("Interact");
        }
    }

    void IRestartable.RestartFunction()
    {
        transform.localPosition = _startPos;
    }
}