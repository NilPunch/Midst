using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class RewindDarkening : MonoBehaviour, IRestartable
{
	[SerializeField] private float FadeTime = 1f;
	[SerializeField] [Range(0f,1f)] private float Alpha= 1f;

    private CanvasGroup _darkCanvas;


    //private Coroutine _startCoroutine;
    //private Coroutine _endCoroutine;

    private void Awake()
    {
        _darkCanvas = GetComponent<CanvasGroup>();
    }

    public void StartAnimation()
    {
        //if (_endCoroutine != null)
        //    StopCoroutine(_endCoroutine);

        LeanTween.alphaCanvas(_darkCanvas, Alpha, FadeTime);
    }

    public void EndAnimation()
    {
        LeanTween.alphaCanvas(_darkCanvas, 0f, FadeTime);
    }

    public void RestartFunction()
    {
        _darkCanvas.alpha = 0f;
    }

    //public IEnumerator DarkeningAnimation()
    //{
    //    yield return null;
    //    LeanTween.alphaCanvas(DarkCanvas, 0.6f, 1f);
    //}
}