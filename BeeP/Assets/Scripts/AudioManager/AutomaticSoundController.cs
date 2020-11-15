using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticSoundController : MonoBehaviour
{
	[SerializeField] private string SoundName = "Default";
	[SerializeField] private float FadeInStep = 1f;
	[SerializeField] private float FadeOutStep = 1f;
	[SerializeField] [Range(0f,1f)] private float TargetVolume = 1f;
	[SerializeField] private bool Loop = false;

	private AudioManager _audioManager = null;
	private ExtSound _extSound = null;

	private Coroutine _fade = null;
	private Sound _lastPlayingSound = null;

	private bool _isPlaying = false;

	// Start is called before the first frame update
	void Start()
	{
		_audioManager = AudioManager.Instance;
		_extSound = _audioManager.GetExtSound(SoundName);

		AudioManager.Instance.OnSoundPlayed += Play;
		AudioManager.Instance.OnSoundStopped += Stop;
	}

	// Update is called once per frame
	void Update()
	{
		if (_isPlaying == true)
		{
			if (Loop == false) // Proc only once
			{
				_isPlaying = false;
			}
			else if (_lastPlayingSound.Source.isPlaying == false)
			{
				_lastPlayingSound = _audioManager.Play(SoundName);
			}
		}
	}

	private void Play(string soundName)
	{
		if (soundName == SoundName)
		{
			_isPlaying = true;

			if (_fade != null)
				StopCoroutine(_fade);
			_fade = StartCoroutine(FadeIn());
		}
	}

	private void Stop(string soundName)
	{
		if (soundName == SoundName)
		{
			_isPlaying = false;

			if (_fade != null)
				StopCoroutine(_fade);
			_fade = StartCoroutine(FadeOut());
		}
	}

	IEnumerator FadeIn()
	{
		_lastPlayingSound = _audioManager.Play(SoundName);

		float startVolume = _lastPlayingSound.Source.volume;
		while (startVolume < TargetVolume)
		{
			startVolume += FadeInStep;
			foreach (Sound sound in _extSound.SoundCollection)
			{
				sound.Source.volume = startVolume;
			}
			yield return null;
		}
		_fade = null;
	}

	IEnumerator FadeOut()
	{
		float startVolume = _lastPlayingSound.Source.volume;
		while (startVolume >= 0f)
		{
			startVolume -= FadeOutStep;
			foreach (Sound sound in _extSound.SoundCollection)
			{
				sound.Source.volume = startVolume;
			}
			yield return null;
		}
		_audioManager.Stop(SoundName);
		_fade = null;
	}
}
