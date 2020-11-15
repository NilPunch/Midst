using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultJump : MonoBehaviour
{
	[SerializeField] private float JumpVelocity = 0f;

	private Rigidbody2D _rigidbody2D;

	private void OnEnable()
	{
		GetComponent<PlayerController>().OnPlayerJump += Jump;
	}

	private void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	public void Jump()
	{
		_rigidbody2D.velocity = _rigidbody2D.velocity.With(y: 0f);
		_rigidbody2D.AddForce(Vector2.up * JumpVelocity, ForceMode2D.Impulse);
	}
}
