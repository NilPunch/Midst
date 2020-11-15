using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Обеспечение бесшовности звуков по переходу между сценами
    public static AudioManager Instance = null;

    [SerializeField] public ExtSound[] ExternalSoundsCollection = null;

    // Поисковый словарь оптимизация
    private Dictionary<string, ExtSound> _externalSoundsNamedCollection = null;
    // Словарь для хранения состояний пауз по id источников звуков
    private Dictionary<int, bool> _sourcesPauseStates = null;
    // Словарь для хранения источника звуков по id
    private Dictionary<int, AudioSource> _sourcesIds = null;

	public event System.Action<string> OnSoundPlayed;
	public event System.Action<string> OnSoundStopped;

    void Awake()
    {
        // Предотвращаем создание более двух копий
        if (Instance == null)
			Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        // Аудио манагер не будет удалятся при релоаде сцены
        DontDestroyOnLoad(gameObject);

		// Очистка лишних AudioSource
		AudioSource audioSource = null;
		while ((audioSource = GetComponent<AudioSource>()) != null)
			DestroyImmediate(audioSource);

		// Подпись на событие паузы

        _sourcesPauseStates = new Dictionary<int, bool>();
        _sourcesIds = new Dictionary<int, AudioSource>();
        _externalSoundsNamedCollection = new Dictionary<string, ExtSound>();

        int idCounter = 0;
        // Инициализация всех источников звуков
        foreach (ExtSound extSound in ExternalSoundsCollection)
        {
            // Заполняем поисковый словарь
            _externalSoundsNamedCollection.Add(extSound.Name, extSound);

            int сalculatedGeneralWeight = 0;
			foreach (Sound sound in extSound.SoundCollection)
			{
				sound.Source = gameObject.AddComponent<AudioSource>();
				sound.Source.clip = sound.Сlip;
				sound.Source.volume = sound.Volume;
				sound.Source.pitch = sound.Pitch;
				sound.Source.loop = sound.Loop;

				сalculatedGeneralWeight += sound.Weight;

				// Запихиваем в словари обработки паузы только те AudioSource, которые должны реагировать на паузу
				if (extSound.Pausable)
				{
					_sourcesIds.Add(idCounter, sound.Source);
					_sourcesPauseStates.Add(idCounter, false);
					++idCounter;
				}
			}

            extSound.GeneralWeight = сalculatedGeneralWeight;
        }
    }

    void Start()
    {
		Play("Theme");
    }

    public Sound Play(string soundName)
    {
		ExtSound extSound = _externalSoundsNamedCollection[soundName];
		if (extSound != null)
		{
			int randomNumber = Random.Range(0, extSound.GeneralWeight);

			foreach (Sound sound in extSound.SoundCollection)
			{
				if (randomNumber < sound.Weight)
				{
					sound.Source.Play();
					return sound;
				}
				randomNumber = randomNumber - sound.Weight;
			}
		}
		else
		{
            //Debug.LogWarning("Warning[AudioManager]>Play(): Can't find clip '" + soundName + "'");
		}
		return null;
    }

    public void Stop(string soundName)
    {
		ExtSound extSound = _externalSoundsNamedCollection[soundName];
        if (extSound != null)
        {
            foreach (Sound sound in extSound.SoundCollection)
            {
                sound.Source.Stop();
            }
        }
        else
		{
            //Debug.LogWarning("Warning[AudioManager]>Reset(): Can't find clip '" + soundName + "'");
		}
    }

	public Sound GetSound(string soundName)
	{
		ExtSound extSound = _externalSoundsNamedCollection[soundName];
		if (extSound != null)
		{
			int randomNumber = Random.Range(0, extSound.GeneralWeight);

			foreach (Sound sound in extSound.SoundCollection)
			{
				if (randomNumber < sound.Weight)
				{
					return sound;
				}
				randomNumber = randomNumber - sound.Weight;
			}
		}
		else
		{
			//Debug.LogWarning("Warning[AudioManager]>GetSound(): Can't find clip '" + soundName + "'");
		}
		return null;
	}

	public ExtSound GetExtSound(string soundName)
	{
		ExtSound extSound = _externalSoundsNamedCollection[soundName];
		if (extSound != null)
		{
			return extSound;
		}
		else
		{
			//Debug.LogWarning("Warning[AudioManager]>GetExtSound(): Can't find clip '" + soundName + "'");
		}
		return null;
	}

	public void ToggleMuteAll()
    {
        foreach (ExtSound extSound in ExternalSoundsCollection)
        {
            foreach (Sound sound in extSound.SoundCollection)
            {
				sound.Source.mute = !sound.Source.mute;
            }
        }
    }

    public void PauseSounds(bool paused)
    {
        foreach (int sourceId in _sourcesIds.Keys)
        {
            bool soundPrevioslyPaused = _sourcesPauseStates[sourceId];
            AudioSource source = _sourcesIds[sourceId];
            if (paused == true && source.isPlaying)
            {
                source.Pause();
                _sourcesPauseStates[sourceId] = true;
                continue;
            }
            else if (paused == false && soundPrevioslyPaused == true)
            {
                source.UnPause();
                _sourcesPauseStates[sourceId] = false;
                continue;
            }
            _sourcesPauseStates[sourceId] = false;
        }
    }

	public AudioSource PlayingSource(string soundName)
	{
		foreach (Sound sound in _externalSoundsNamedCollection[soundName].SoundCollection)
		{
			AudioSource audioSource = sound.Source;
			if (audioSource.isPlaying)
				return audioSource;
		}
		return null;
	}

	public bool IsPlaying(string soundName)
	{
		foreach (Sound sound in _externalSoundsNamedCollection[soundName].SoundCollection)
		{
			AudioSource audioSource = sound.Source;
			if (audioSource.isPlaying == true)
				return true;
		}
		return false;
	}

	public bool IsStopped(string soundName)
	{
		foreach (Sound sound in _externalSoundsNamedCollection[soundName].SoundCollection)
		{
			AudioSource audioSource = sound.Source;
			if (audioSource.isPlaying == false)
				return false;
		}
		return true;
	}

	public void FakePlay(string soundName)
	{
		OnSoundPlayed?.Invoke(soundName);
	}

	public void FakeStop(string soundName)
	{
		OnSoundStopped?.Invoke(soundName);
	}
}
