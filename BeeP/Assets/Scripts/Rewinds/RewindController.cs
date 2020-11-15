using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RewindController : MonoBehaviour, IRestartable
{
    private SpaceRewindController _spaceRewinder = null;
    private IAmRewindController _timeRewinder = null;
    [SerializeField] private Text SpaceRewText = null;
    [SerializeField] private Text TimeRewText = null;

    [SerializeField] private int SpaceRewindsAmount = 1;
    [SerializeField] private int TimeRewindsAmount = 1;
    private int _spaceRewindsLeft;
    private int _timeRewindsLeft;
    public bool IsTimeRewinding;
    public bool IsSpaceRewinding;

    private RewindsButtonsAndTextControls _textControls;
    private RewindDarkening _rewindDarkening;

    private void Start()
    {
        _spaceRewinder = FindObjectOfType<SpaceRewindController>();
        _timeRewinder = FindObjectOfType<TimeRewindController>();
        _timeRewindsLeft = TimeRewindsAmount;
        _spaceRewindsLeft = SpaceRewindsAmount;
        UpdateTexts();

        _textControls = FindObjectOfType<RewindsButtonsAndTextControls>();
        _rewindDarkening = FindObjectOfType<RewindDarkening>();
        //update activeness
        _textControls?.UpdateActiveness(_spaceRewindsLeft > 0 ? true : false, _timeRewindsLeft > 0 ? true : false);
    }

    void Update()
    {
        //No rewinds
        if (!IsSpaceRewinding && !IsTimeRewinding)
        {
            if (Input.GetKeyDown(KeyCode.Q) && _spaceRewindsLeft > 0)
            {
                _spaceRewinder.StartRewind();
                IsSpaceRewinding = true;
                Pauser.Instance.Pause(true);
                //set new texts
                _textControls?.SpaceRewindStarted();

                _rewindDarkening?.StartAnimation();

                AudioManager.Instance.FakePlay("Rewind");
			}
			else if (Input.GetKeyDown(KeyCode.E) && _timeRewindsLeft > 0)
            {
                _timeRewinder.StartRewind();
                IsTimeRewinding = true;
                Pauser.Instance.Pause(true);
                //set new texts
                _textControls?.TimeRewindStarted();

                _rewindDarkening?.StartAnimation();

                AudioManager.Instance.FakePlay("Rewind");
			}
		}
        //Space rewind
        else if (IsSpaceRewinding)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CancelSpaceRewind();
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                if (_spaceRewinder.EndRewind())
                {
                    --_spaceRewindsLeft;
                    //AudioManager.Instance.Play("Rewind Success");
                }
                else
                    AudioManager.Instance.Play("Rewind Cancel");
                UpdateTexts();
                IsSpaceRewinding = false;
                RewindEndedTextsAndPause();

                AudioManager.Instance.FakeStop("Rewind");
            }
		}
        //Time rewind
        else if (IsTimeRewinding)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                CancelTimeRewind();
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                --_timeRewindsLeft;
                //AudioManager.Instance.Play("Rewind Success");
                UpdateTexts();
                _timeRewinder.EndRewind();
                IsTimeRewinding = false;
                RewindEndedTextsAndPause();

                AudioManager.Instance.FakeStop("Rewind");
            }
		}
    }

    public void CancelTimeRewind()
    {
        IsTimeRewinding = false;
        _timeRewinder.CancelRewind();
        RewindEndedTextsAndPause();

        AudioManager.Instance.FakeStop("Rewind");
        AudioManager.Instance.Play("Rewind Cancel");
    }

    public void CancelSpaceRewind()
    {
        IsSpaceRewinding = false;
        _spaceRewinder.CancelRewind();
        RewindEndedTextsAndPause();

        AudioManager.Instance.FakeStop("Rewind");
        AudioManager.Instance.Play("Rewind Cancel");
    }

    private void RewindEndedTextsAndPause()
    {
        Pauser.Instance.Pause(false);
        //update activeness
        _textControls?.UpdateActiveness(_spaceRewindsLeft > 0 ? true : false, _timeRewindsLeft > 0 ? true : false);
        _rewindDarkening?.EndAnimation();
    }

    public void RestartFunction()
    {
        IsTimeRewinding = IsSpaceRewinding = false;
        _spaceRewinder.RestartFunction();
        _timeRewinder.RestartFunction();
        _spaceRewindsLeft = SpaceRewindsAmount;
        _timeRewindsLeft = TimeRewindsAmount;
        UpdateTexts();
        //update activeness
        _textControls?.UpdateActiveness(_spaceRewindsLeft > 0 ? true : false, _timeRewindsLeft > 0 ? true : false);
    }

    private void UpdateTexts()
    {
        if (TimeRewText != null)
            TimeRewText.text = "Level rewinds left - " + _timeRewindsLeft;
        if (SpaceRewText != null)
            SpaceRewText.text = "Self rewinds left - " + _spaceRewindsLeft;
    }
}