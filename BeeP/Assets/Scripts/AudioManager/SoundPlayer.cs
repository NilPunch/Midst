using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
	public void Play(string soundName)
	{
		AudioManager.Instance.Play(soundName);
	}
}
