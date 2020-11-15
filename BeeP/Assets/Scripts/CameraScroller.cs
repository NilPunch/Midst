using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScroller : MonoBehaviour, IRestartable
{
	[SerializeField] [Range(0f, 1f)] private float Ease = 1f;
    [SerializeField] private Vector2 LowerLeftBound = Vector2.zero;
    [SerializeField] private Vector2 UpperRightBound = Vector2.zero;

    [SerializeField] private SpaceRewindController Player = null;

    private Vector3 _startPos;

    private void Awake()
    {
        _startPos = transform.position;
    }

    public void RestartFunction()
    {
        transform.position = _startPos; 
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = Player.GetPlayerPosition();
        if (playerPos.x > LowerLeftBound.x && playerPos.x < UpperRightBound.x)
            transform.position += (transform.position.With(x: playerPos.x) - transform.position) * Ease;
        if (playerPos.y > LowerLeftBound.y && playerPos.y < UpperRightBound.y)
            transform.position += (transform.position.With(y: playerPos.y) - transform.position) * Ease;
    }
}
