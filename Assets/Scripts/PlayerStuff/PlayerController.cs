using UnityEngine;

public class PlayerController : MonoBehaviour, ICharacter
{
    [Header("Movement Settings")]
    [SerializeField] private float _walkSpeed = 200f;
    [Range(1.1f, 2f)]
    [SerializeField] private float _runSpeedMultiplier = 1.5f;
    [Range(0.1f, 0.9f)]
    [SerializeField] private float _crouchSpeedMultiplier = 0.5f;
    [SerializeField] private float _jumpSpeed = 100f;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _drag = 0.9f;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _onAirMovementDamper = 0.6f;

    [Header("Movement Actions")]
    [SerializeField] private Move2DActionsSO move2DActions;

    [Space]
    [Header("Input")]
    [SerializeField] private InputReader _inputReader;

    [Header("Ground Check")]
    [SerializeField] private float _groundCheckRadius = 1.0f;
    [SerializeField] private float _groundCheckSidesDistance = 18.0f;
    [SerializeField] private LayerMask _groundLayers;

    private StateMachine<IPlayerState> _stateMachine;

    private Vector2[] GroundColliderPoints
    {
        get
        {
            Vector2[] points = new Vector2[]
            {
                transform.position,
                new Vector2(transform.position.x - _groundCheckSidesDistance, transform.position.y),
                new Vector2(transform.position.x + _groundCheckSidesDistance, transform.position.y),
            };

            return points;
        }
    }

    private bool IsGrounded
    {
        get
        {
            Vector2[] points = GroundColliderPoints;

            for (int i = 0; i < points.Length; i++)
            {
                Collider2D c = Physics2D
                    .OverlapCircle(points[i], _groundCheckRadius, _groundLayers);

                if (c != null) return true;
            }

            return false;
        }
    }

    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public Vector2 PreviousMovementInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsCrouched { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        if (!Rigidbody)
            Debug.LogError("No rigidbody component on player!");

        if (!Animator)
            Debug.LogError("No animator component on player!");

        _stateMachine = new StateMachine<IPlayerState>();

        PlayerNormalState playerNormalState = new PlayerNormalState(this);
        PlayerJumpState playerJumpState = new PlayerJumpState(this);

        _stateMachine.SetState(playerNormalState);

        _stateMachine.AddTransition(playerNormalState, playerJumpState, () => !IsGrounded);
        _stateMachine.AddTransition(playerJumpState, playerNormalState, () => IsGrounded, () => JumpInput = false);
    }

    private void OnEnable()
    {
        _inputReader.moveEvent += OnMove;
        _inputReader.jumpEvent += OnJumpPressed;
        _inputReader.startedRunningEvent += OnStartedRunning;
        _inputReader.stoppedRunningEvent += OnStoppedRunning;
        _inputReader.startedCrouchEvent += OnStartedCrouch;
        _inputReader.stoppedCrouchEvent += OnStoppedCrouch;
    }

    private void OnDisable()
    {
        _inputReader.moveEvent -= OnMove;
        _inputReader.jumpEvent -= OnJumpPressed;
        _inputReader.startedRunningEvent -= OnStartedRunning;
        _inputReader.stoppedRunningEvent -= OnStoppedRunning;
        _inputReader.startedCrouchEvent -= OnStartedCrouch;
        _inputReader.stoppedCrouchEvent -= OnStoppedCrouch;
    }

    private void OnMove(Vector2 movement)
    {
        PreviousMovementInput = movement;
    }

    private void OnJumpPressed() => JumpInput = true;

    private void OnStoppedRunning()
    {
        IsRunning = false;

        Animator.SetBool("IsRunning", false);
    }

    private void OnStartedRunning()
    {
        IsRunning = true;

        Animator.SetBool("IsRunning", true);
    }

    private void OnStartedCrouch()
    {
        IsCrouched = true;

        Animator.SetBool("IsCrouched", true);

    }

    private void OnStoppedCrouch()
    {
        IsCrouched = false;

        Animator.SetBool("IsCrouched", false);
    }

    private void FixedUpdate() => _stateMachine.PhysicsTick();

    private void Update() => _stateMachine.Tick();

    private void OnDrawGizmos()
    {
        Vector2[] points = GroundColliderPoints;

        Gizmos.color = Color.magenta;

        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.DrawSphere(points[i], _groundCheckRadius);
        }
    }

    public void Move(float horizontalAxis, bool useDampener = false)
    {
        float moveSpeed = _walkSpeed;

        if (IsRunning)
            moveSpeed *= _runSpeedMultiplier;
        else if (IsCrouched)
            moveSpeed *= _crouchSpeedMultiplier;

        move2DActions.Move(this, horizontalAxis, moveSpeed, 
            _drag, useDampener ? _onAirMovementDamper : 1f);
    }

    public void Jump()
    {
        move2DActions.Jump(this, _jumpSpeed);
    }

}
