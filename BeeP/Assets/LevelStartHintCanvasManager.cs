using System.Collections;
using UnityEngine;

public class LevelStartHintCanvasManager : MonoBehaviour
{

    private bool _ended = true;
    private bool _buttonEnd = false;
    [SerializeField] private CanvasGroup PanelGroup = null;

    private Coroutine _startCoroutine = null;
    private Coroutine _endCoroutine = null;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForInputEnd());
    }

    private IEnumerator WaitForInputEnd()
    {
        yield return new WaitForSeconds(1f);
        _ended = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_buttonEnd)
            return;
        if (!_ended)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetButtonDown("Jump") ||  Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
            {
                EndAnimation();
            }
        }
    }

    public void EndAnimation()
    {
        if (_ended || _buttonEnd)
            return;
        if (!_ended || !_buttonEnd)
        {
            _ended = _buttonEnd = true;
            if (_endCoroutine != null)
                StopCoroutine(_endCoroutine);
            _startCoroutine  = StartCoroutine(CanvasAnimation());
        }
    }


    private IEnumerator CanvasAnimation()
    {
        PanelGroup.interactable = false;
        PanelGroup.blocksRaycasts = false;

        LeanTween.alphaCanvas(PanelGroup, 0f, 1f);
        yield return new WaitForSeconds(1f);
        //PanelGroup.gameObject.SetActive(false);
    }

}
