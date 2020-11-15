using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pauser : MonoBehaviour
{
    private bool _pause;
    private List<Rigidbody2D> _rigidbodies = new List<Rigidbody2D>();
    private List<Vector2> _velocities = new List<Vector2>();
    private List<Vector2> _positions = new List<Vector2>();

    public static Pauser Instance;
    [HideInInspector] public bool DoNotRecord;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        foreach(Rigidbody2D rigidbody in FindObjectsOfType<Rigidbody2D>())
        {
            if (rigidbody.bodyType == RigidbodyType2D.Dynamic)
                _rigidbodies.Add(rigidbody);
        }
    }

    public void Pause(bool pause)
    {
        if (_pause == pause)
            return;
        _pause = pause;
        if (_pause)
        {
            foreach (Rigidbody2D rigidbody in _rigidbodies)
            {
                _velocities.Add(rigidbody.velocity);
                _positions.Add(rigidbody.transform.position);
                //rigidbody.simulated = false;
            }
            RewindStopperController.Instance.DoNotRecordPause = true;
            DoNotRecord = true;
        }
        else
        {
            if (_velocities.Count != 0)
            {
                for (int i = 0; i < _rigidbodies.Count; i++)
                {
                    if (_rigidbodies[i].bodyType == RigidbodyType2D.Dynamic)
                    {
                        _rigidbodies[i].velocity = _velocities[i];
                        _rigidbodies[i].position = _positions[i];
                    }
                }
                _velocities.Clear();
                _positions.Clear();
            }
            RewindStopperController.Instance.DoNotRecordPause = false;
            DoNotRecord = false;
        }
    }

    private void FixedUpdate()
    {
        if (_pause)
        {
            for (int i = 0; i < _rigidbodies.Count; i++)
            {
                Rigidbody2D rigidbody = (Rigidbody2D)_rigidbodies[i];
                if (rigidbody.isKinematic)
                    continue;
                rigidbody.velocity = Vector2.zero;
                rigidbody.position = _positions[i];
            }
        }
    }
}