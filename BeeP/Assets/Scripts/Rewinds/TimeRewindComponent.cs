using UnityEngine;

public class TimeRewindComponent : MonoBehaviour, IAmRewindController
{
    private Vector3 _myStartPosition; //for caching
    private TimeRewindController _rewindController;

    private Vector3[] _myPositions; // for looping
    private int _positionIndex;
    private int _rewindIndex;

    private bool _isInRewind;
    private bool _recordedPastCircle;
    private GameObject _phantomObject;
    private float _elapsedInRewindEnd;

    [SerializeField] private float TimeInLastRewPosition = 3f;
    [SerializeField] private GameObject PhantomObject = null;

    private void Start()
    {
        //if (PhantomObject == null)
        //    Debug.LogWarning("This rewind controller has not set phantom object - " + gameObject.name);

        _myStartPosition = transform.position;

        //temp
        _myPositions = new Vector3[GlobalVariables.Instance.FPS * GlobalVariables.Instance.RecordedSeconds];
        _phantomObject = Instantiate(PhantomObject);
        _phantomObject.SetActive(false);

        _rewindController = FindObjectOfType<TimeRewindController>();
    }

    //temp
    private void OnDestroy()
    {
        Destroy(_phantomObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isInRewind)
            RewindUpdateLoop();
        else
            RegularUpdateLoop();
    }

    private void RegularUpdateLoop()
    {
        if (Pauser.Instance.DoNotRecord)
            return;
        _myPositions[_positionIndex] = transform.position;
        _positionIndex++;
        if (_positionIndex >= GlobalVariables.Instance.FPS * GlobalVariables.Instance.RecordedSeconds)
        {
            _recordedPastCircle = true;
            _positionIndex = 0;
        }
    }

    private void RewindUpdateLoop()
    {
        if (!_recordedPastCircle)
        {
            if (_rewindIndex == 0)
            {
                _elapsedInRewindEnd += Time.deltaTime;
                if (_elapsedInRewindEnd > TimeInLastRewPosition)
                    _rewindController.DownSideCancel();
                return;
            }
        }
        else
        {
            if (_rewindIndex == _positionIndex + 1)
            {
                _elapsedInRewindEnd += Time.deltaTime;
                if (_elapsedInRewindEnd > TimeInLastRewPosition)
                    _rewindController.DownSideCancel();
                return;
            }
        }
        _phantomObject.transform.position = _myPositions[_rewindIndex];
        _rewindIndex--;
        if (_rewindIndex < 0)
            _rewindIndex = GlobalVariables.Instance.FPS * GlobalVariables.Instance.RecordedSeconds - 1;
    }

    public void StartRewind()
    {
        _isInRewind = true;
        _phantomObject.SetActive(true);
        _phantomObject.transform.position = transform.position;
        _rewindIndex = _positionIndex - 1;
        if (_rewindIndex < 0)
            _rewindIndex = GlobalVariables.Instance.FPS * GlobalVariables.Instance.RecordedSeconds - 1;
        _elapsedInRewindEnd = 0f;
    }

    public void EndRewind()
    {
        gameObject.transform.position = _myPositions[_rewindIndex];
        CancelRewind();
    }

    public void CancelRewind()
    {
        _phantomObject.SetActive(false);
        _isInRewind = false;
    }

    public void RestartFunction()
    {
        transform.position = _myStartPosition;
        _isInRewind = false;
        _recordedPastCircle = false;
        _positionIndex = 0;
        _phantomObject.SetActive(false);
    }
}