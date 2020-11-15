using UnityEngine;

public class PlayerPhantomScript : MonoBehaviour
{
    private SpriteRenderer _renderer;
    public bool ImInSomething;
    [SerializeField] private Color StartColor = Color.white;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ImInSomething = true;
        _renderer.color = Color.red;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        ImInSomething = true;
        _renderer.color = Color.red;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ImInSomething = false;
        _renderer.color = StartColor;
    }
}