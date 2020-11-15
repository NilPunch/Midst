using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField]
    public AudioClip Сlip;
    [SerializeField]
    public int Weight = 1;

    [Range(0f, 1f)]
    public float Volume = 1f;
    [Range(0f, 10f)]
    public float Pitch = 1f;
    [SerializeField]
    public bool Loop;
    
    [HideInInspector]
    public AudioSource Source;
}

[System.Serializable]
public class ExtSound
{
    [SerializeField]
    public string Name;
    [SerializeField]
    public Sound[] SoundCollection;
    [SerializeField]
    public bool Pausable;

    [HideInInspector]
    public int GeneralWeight = 0;
}
