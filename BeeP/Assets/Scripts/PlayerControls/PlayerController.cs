using UnityEngine;

[RequireComponent(typeof(DefaultJump), typeof(DefaultJump))]
public class PlayerController : MonoBehaviour
{
	[SerializeField] private float MovingSpeed = 0f;
	[SerializeField] private float MaxFallingSpeed = 0f;
	[SerializeField] [Range(0f, 20f)] private float DefaultMultiplyer = 1f;
	[SerializeField] [Range(0f, 20f)] private float FallMultiplyer = 1f;
	[SerializeField] [Range(0f, 20f)] private float LowJumpMultiplyer = 1f;
    [SerializeField] [Range(0f, 5f)] private float SmoothingTime = 0.5f;
    //[SerializeField] [Range(0f, 0.1f)] private float MinimalRecordingVelocity = 0.01f;

	[SerializeField] private Transform GroundCheck = null;
	[SerializeField] private Vector2 GroundCheckBox = Vector2.one;
	[SerializeField] private float CoyoteTime = 0f;
	[SerializeField] private float NoCheckingGroundTime = 0f;
	[SerializeField] private float JumpPreregisterTime = 0f;
	[SerializeField] private LayerMask GroundLayer = new LayerMask();

	//public event System.Action OnPlayerStoped;
	//public event System.Action OnPlayerStartMoving;
	public event System.Action OnPlayerJump;

	public bool IsGrounded { get; private set; } = false;
	public bool InMidair { get; private set; } = false;

	private Rigidbody2D _rigidbody2D = null;

	private bool _jumpHelded = false;

	private float _timerNoGroundRegister = 0f;
	private float _timeAfterGrounded = 0f;
	private float _timeJumpButtonRegistered = 0f;

    private Vector3 _velocity = Vector3.zero;
    //private float _movingTime = 0f;
    //private bool _playerStopedMoving = false;
    float _moveInput;

	private void Awake()
    {
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		/* Midair adjustment */
		if (_rigidbody2D.velocity.y < 0f && _timeAfterGrounded <= 0f)
		{
			_rigidbody2D.gravityScale = FallMultiplyer;
			_rigidbody2D.velocity = _rigidbody2D.velocity.With(y: Mathf.Clamp(_rigidbody2D.velocity.y, -MaxFallingSpeed, MaxFallingSpeed));
		}
		else if (_rigidbody2D.velocity.y > 0f && !_jumpHelded && InMidair == true)
		{
			_rigidbody2D.gravityScale = LowJumpMultiplyer;
		}
		else
		{
			_rigidbody2D.gravityScale = DefaultMultiplyer;
		}

        //pause recording
        //if (_rigidbody2D.velocity.magnitude >= MinimalRecordingVelocity)
        //    RewindStopperController.Instance.DoNotRecordStaticPlayer = false;
        //else
        //    RewindStopperController.Instance.DoNotRecordStaticPlayer = true;
        Vector2 targetVelocity = new Vector2(_moveInput * MovingSpeed, _rigidbody2D.velocity.y);
        //_rigidbody2D.velocity = new Vector2(moveInput * MovingSpeed, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = Vector3.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref _velocity, SmoothingTime);
    }

	private void Update()
    {
		IsGrounded = Physics2D.OverlapBox(GroundCheck.position, GroundCheckBox, 0f, GroundLayer);

		if (IsGrounded)
		{
			_timeAfterGrounded = CoyoteTime;
		}
		else
		{
			_timeAfterGrounded -= Time.deltaTime;
		}

		_timerNoGroundRegister -= Time.deltaTime;
		_timeJumpButtonRegistered -= Time.deltaTime;

		/* Just for absolutely non-abuse system */
		if (IsGrounded == true && InMidair == true && _timerNoGroundRegister <= 0f)
		{
			InMidair = false;
		}

		if (Input.GetButtonDown("Jump"))
		{
			_timeJumpButtonRegistered = JumpPreregisterTime;
		}

		if (_timeJumpButtonRegistered > 0f && Input.GetButton("Jump") && (IsGrounded == true || _timeAfterGrounded > 0f) && InMidair == false)
		{
			OnPlayerJump?.Invoke();
			InMidair = true;
			_timerNoGroundRegister = NoCheckingGroundTime;
			_jumpHelded = true;
		}
		if (Input.GetButtonUp("Jump") && _jumpHelded)
		{
			_jumpHelded = false;
		}
		
		/* Player movement */
		_moveInput = Input.GetAxisRaw("Horizontal");
        //Вынес в Fixed, потому что так прпавильно

	}
}