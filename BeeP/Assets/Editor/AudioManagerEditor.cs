using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor
{
	private static string _soundName = "";

	public override void OnInspectorGUI()
	{
		GUILayout.Label("Quick sound test. Type the name of sound:");
		_soundName = GUILayout.TextField(_soundName);

		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Play"))
		{
			Play();
		}
		if (GUILayout.Button("Stop"))
		{
			Stop();
		}
		GUILayout.EndHorizontal();

		base.OnInspectorGUI();
	}

	public void Play()
	{
		AudioManager audioManager = (AudioManager)target;

		ExtSound extSound = Array.Find(audioManager.ExternalSoundsCollection, findExtSound => findExtSound.Name.ToLower() == _soundName.ToLower());
		if (extSound != null)
		{
			int randomNumber = UnityEngine.Random.Range(0, extSound.GeneralWeight);

			foreach (Sound sound in extSound.SoundCollection)
			{
				if (randomNumber < sound.Weight)
				{
					AudioSource audioSource = ((AudioManager)target).gameObject.GetComponent<AudioSource>();
					if (audioSource == null)
						audioSource = ((AudioManager)target).gameObject.AddComponent<AudioSource>();
					audioSource.clip = sound.Сlip;
					audioSource.volume = sound.Volume;
					audioSource.pitch = sound.Pitch;
					audioSource.loop = sound.Loop;
					audioSource.Play();
					return;
				}
				randomNumber = randomNumber - sound.Weight;
			}
		}
		else
			Debug.LogWarning("Editor: Can't find clip '" + _soundName + "'");
	}

	public void Stop()
	{
		DestroyImmediate(((AudioManager)target).gameObject.GetComponent<AudioSource>());
	}
}