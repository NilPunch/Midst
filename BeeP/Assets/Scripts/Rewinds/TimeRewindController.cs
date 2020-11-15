using UnityEngine;

public class TimeRewindController : MonoBehaviour, IAmRewindController
{
    private TimeRewindComponent[] _timeRewinds;
    private RewindController _rewindController = null;

    private void Start()
    {
        _timeRewinds = FindObjectsOfType<TimeRewindComponent>();
        _rewindController = FindObjectOfType<RewindController>();
    }

    public void CancelRewind()
    {
        foreach (TimeRewindComponent timeRewind in _timeRewinds)
            timeRewind.CancelRewind();
    }

    public void EndRewind()
    {
        foreach (TimeRewindComponent timeRewind in _timeRewinds)
            timeRewind.EndRewind();
    }

    public void RestartFunction()
    {
        foreach (TimeRewindComponent timeRewind in _timeRewinds)
            timeRewind.RestartFunction();
    }

    public void StartRewind()
    {
        foreach (TimeRewindComponent timeRewind in _timeRewinds)
            timeRewind.StartRewind();
    }

    public void DownSideCancel()
    {
        if (_rewindController.IsTimeRewinding)
        {
            _rewindController.CancelTimeRewind();
            return;
        }
        _rewindController.IsTimeRewinding = false;
        CancelRewind();
        Pauser.Instance.Pause(false);
    }
}