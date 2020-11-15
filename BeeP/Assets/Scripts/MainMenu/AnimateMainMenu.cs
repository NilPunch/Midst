using System.Collections;
using UnityEngine;

public class AnimateMainMenu : MonoBehaviour
{
	private const float CANVAS_FADE_TIME = 0.5f;

    [SerializeField] private CanvasGroup MainCanvas = null;
    [SerializeField] private CanvasGroup LevelsCanvas = null;
    private Coroutine _coroutineStart;
    private Coroutine _coroutineEnd;

    public void StartThisCanvasAnimation()
    {
        _coroutineStart = StartCoroutine(StartStatScreen());
    }

    public void EndThisAnimation()
    {
        _coroutineEnd = StartCoroutine(StopStatScreen());
    }

    private IEnumerator StopStatScreen()
    {
        if (_coroutineStart != null)
            StopCoroutine(_coroutineStart);
		//LeanTween.moveY(LevelsCanvas.gameObject, -Screen.height / 2f, 1f);

		LevelsCanvas.interactable = false;
		LeanTween.alphaCanvas(LevelsCanvas, 1f, CANVAS_FADE_TIME);
        yield return new WaitForSeconds(CANVAS_FADE_TIME);
		LevelsCanvas.gameObject.SetActive(false);


		MainCanvas.gameObject.SetActive(true);
		LeanTween.alphaCanvas(MainCanvas, 1f, CANVAS_FADE_TIME);
		yield return new WaitForSeconds(CANVAS_FADE_TIME);
		MainCanvas.interactable = true;
        MainCanvas.blocksRaycasts = true;
    }

    private IEnumerator StartStatScreen()
    {
        if (_coroutineEnd != null)
            StopCoroutine(_coroutineEnd);
        FindObjectOfType<LevelUnlocker>().UpdateButtons();

		MainCanvas.interactable = false;
        MainCanvas.blocksRaycasts = false;
        LeanTween.alphaCanvas(MainCanvas, 0f, CANVAS_FADE_TIME);
		yield return new WaitForSeconds(CANVAS_FADE_TIME);

		LevelsCanvas.alpha = 0f;
		LevelsCanvas.interactable = false;
		LevelsCanvas.gameObject.SetActive(true);
		LeanTween.alphaCanvas(LevelsCanvas, 1f, CANVAS_FADE_TIME);
        yield return new WaitForSeconds(CANVAS_FADE_TIME);
		LevelsCanvas.interactable = true;

		// LeanTween.moveY(LevelsCanvas.gameObject, Screen.height / 2f, 1f);
	}
}
