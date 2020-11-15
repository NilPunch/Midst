using System.Collections.Generic;
using UnityEngine;

public class AllRigidbodiesIsStopped : MonoBehaviour
{
    private List<Rigidbody2D> _allLevelRigidbodies = new List<Rigidbody2D>();
    [SerializeField] [Range(0f, 0.1f)] private float MinimalRecordingVelocity = 0.01f;

    private void Awake()
    {
        foreach(Rigidbody2D rigidbody in FindObjectsOfType<Rigidbody2D>())
        {
            if (rigidbody.bodyType == RigidbodyType2D.Dynamic)
                _allLevelRigidbodies.Add(rigidbody);
        }
    }

    private void Update()
    {
        foreach(Rigidbody2D rigidbody in _allLevelRigidbodies)
        {
            if (rigidbody.velocity.magnitude >= MinimalRecordingVelocity)
            {
                RewindStopperController.Instance.DontRecordStaticRigidbodies = false;
                return;
            }
        }
        RewindStopperController.Instance.DontRecordStaticRigidbodies = true;
    }
}