using UnityEngine;
using UnityEngine.UI;

public class LevelTimeLimiter : MonoBehaviour, IRestartable
{
    [SerializeField] private Text TimeText = null;
    [SerializeField] private float SecondsOnLevel = 15f;
    private float _timeElapsed = 0f;
    private Restarter _restarter = null;

    //alternative
    [SerializeField] private bool AlternativeView = false;
    [SerializeField] private Image TimerArrowImage = null;
    [SerializeField] private Image TimerBGImage = null;

    private void Awake()
    {
        _restarter = FindObjectOfType<Restarter>();

        if (AlternativeView)
        {
            TimeText.gameObject.SetActive(false);
            TimerArrowImage.gameObject.SetActive(true);
            TimerBGImage.gameObject.SetActive(true);
        }

        _timeElapsed = SecondsOnLevel;
    }

    private void Start()
    {
        UpdateVisuals();
    }

    public void RestartFunction()
    {
        if (AlternativeView)
        {
            TimerArrowImage.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
        _timeElapsed = SecondsOnLevel;
    }

    private void Update()
    {
        if (!RewindStopperController.Instance.DontRecord)
        {
            if (_timeElapsed <= 0f)
            {
                //restart
                _restarter.Restart();
                return;
            }
            _timeElapsed -= Time.deltaTime;
            UpdateVisuals();
        }
    }

    private void UpdateVisuals()
    {
        if (AlternativeView)
        {
            TimerArrowImage.transform.localEulerAngles = new Vector3(0f, 0f, (SecondsOnLevel - _timeElapsed) / SecondsOnLevel * 360);
        }
        else
            TimeText.text = ToFormattedTime(_timeElapsed);
    }

    public string ToFormattedTime(float time)
    {
        string result;
        int seconds = Mathf.RoundToInt(time * 100);
        result = (seconds / 100) + ":" + seconds % 100;
        return result;
    }
}