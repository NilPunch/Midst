using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPulser : MonoBehaviour
{
	[SerializeField] [Range(0f, 5f)] private float PulseTime = 0.05f;
	[SerializeField] [Range(0f, 5f)] private float PulseDuration = 0.5f;
	[SerializeField] [Range(0f, 5f)] private float PulseCooldown = 1f;
	[SerializeField] [Range(0f, 5f)] private float Randomness = 0f;
	[SerializeField] private Color PulseColor = Color.cyan;

	private SpriteRenderer _spriteRenderer;
	private Color _startColor;
	private Coroutine _pulseCoroutine = null;

	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_startColor = _spriteRenderer.color;
	}

	// Update is called once per frame
	private void Update()
    {
        if (_pulseCoroutine == null)
		{
			_pulseCoroutine = StartCoroutine(Pulse());
		}
    }

	IEnumerator Pulse()
	{
		LeanTween.color(gameObject, PulseColor, PulseTime * (1 + Random.Range(0f, Randomness)));
		yield return new WaitForSeconds(PulseTime * (1 + Random.Range(0f, Randomness)));
		LeanTween.color(gameObject, _startColor, PulseDuration);
		yield return new WaitForSeconds(PulseDuration + PulseCooldown * (1 + Random.Range(0f, Randomness)));
		_pulseCoroutine = null;
	}
}
