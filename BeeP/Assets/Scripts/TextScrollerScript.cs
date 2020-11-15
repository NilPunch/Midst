using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class TextScrollerScript : MonoBehaviour, IRestartable
{
	[SerializeField] private string[] Hints = null;
	[SerializeField] [Range(0f, 0.5f)] float OneLetterTime = 0.05f;
	[SerializeField] [Range(0f, 5f)] float OneStringTime = 3f;
	[SerializeField] private bool Cycle = true;


	private int _index = 0;
	private Coroutine _animation = null;
	private TMPro.TextMeshProUGUI _textBox = null;

	public void RestartFunction()
	{
		StopCoroutine(_animation);
		_index = 0;
		_animation = StartCoroutine(CycleHints());
	}

	// Start is called before the first frame update
	void Start()
	{
		_textBox = GetComponent<TMPro.TextMeshProUGUI>();
		foreach (string s in Hints)
		{
			if (s.Length > 30)
			{
				Debug.LogWarning("Hint string '" + s + "' is too long");
			}
		}
		_textBox.text = "";
		_animation = StartCoroutine(CycleHints());
	}

	private IEnumerator CycleHints()
	{
		while (true)
		{
			_textBox.text = "";
			for (int i = 0; i < Hints[_index].Length; ++i)
			{
				_textBox.text += Hints[_index][i];
				yield return new WaitForSeconds(OneLetterTime);
			}
			yield return new WaitForSeconds(OneStringTime);
			_index++;
			if (_index >= Hints.Length)
			{
				if (Cycle)
					_index = 0;
				else
					break;
			}
		}
	}
}
