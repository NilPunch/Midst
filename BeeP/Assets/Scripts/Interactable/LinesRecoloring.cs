using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class LinesRecoloring : MonoBehaviour, IRestartable
{
    private Tilemap _tilemap = null;

    // Start is called before the first frame update
    void Start()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    public void Power()
    {
        _tilemap.color = Color.white;
    }

    public void Unpower()
    {
        _tilemap.color = Color.black;
    }

    public void RestartFunction()
    {
        _tilemap.color = Color.black;
    }
}
