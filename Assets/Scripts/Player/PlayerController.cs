using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerController : ShiningCharacterControllerWithStates<IPlayerState>
{
    [Header("Input")]
    [SerializeField] private InputReaderSO _inputReader;

    [Header("Runtime Anchors")]
    [SerializeField] private TransformAnchorSO _playerTransformAnchor;

    [Header("Events")]
    [SerializeField] private VoidEventChannelSO _toggleVisibilityEvent;

    private SpriteRenderer _spriteRenderer;
    private Light2D _playerLight;
    
    private bool IsMoving => Mathf.Abs(Rigidbody.velocity.x) > 0.1f;
    public Vector2 PreviousMovementInput { get; private set; }
    public bool IsDetectable { get; private set; }
    public bool CanMove { get; private set; }
    public bool IsBeingChased { get; set; }
    
    public PlayerStance CurrentStance 
    {
        get
        {
            PlayerStance stance;

            if (IsCrouched)
            {
                if (IsMoving)
                    stance = PlayerStance.CrouchWalking;
                else
                    stance = PlayerStance.CrouchedIdle;
            }
            else if (IsRunning)
            {
                stance = PlayerStance.StandingRunning;
            }
            else
            {
                if (IsMoving)
                    stance = PlayerStance.StandingWalking;
                else
                    stance = PlayerStance.StandingIdle;
            }

            return stance;
        }
    }

    protected override void Awake()
    {
        base.Awake();
       
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _playerLight = GetComponentInChildren<Light2D>();

        if (!_spriteRenderer || !_playerLight)
            Debug.LogError("PlayerController: one ore more components for visibility toggle where not found!");

        _stateMachine = new StateMachine<IPlayerState>();

        PlayerNormalState playerNormalState = new PlayerNormalState(this);

        _stateMachine.SetState(playerNormalState);

        CanMove = true;
        IsDetectable = true;

        _playerTransformAnchor.Value = transform;
    }

    private void OnEnable()
    {
        _inputReader.MoveEvent += OnMove;
        _inputReader.StartedRunningEvent += OnStartedRunning;
        _inputReader.StoppedRunningEvent += OnStoppedRunning;
        _inputReader.StartedCrouchEvent += OnStartedCrouch;
        _inputReader.StoppedCrouchEvent += OnStoppedCrouch;

        if (_toggleVisibilityEvent)
            _toggleVisibilityEvent.OnEventRaised += ToggleVisibility;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMove;
        _inputReader.StartedRunningEvent -= OnStartedRunning;
        _inputReader.StoppedRunningEvent -= OnStoppedRunning;
        _inputReader.StartedCrouchEvent -= OnStartedCrouch;
        _inputReader.StoppedCrouchEvent -= OnStoppedCrouch;

        if (_toggleVisibilityEvent)
            _toggleVisibilityEvent.OnEventRaised -= ToggleVisibility;
    }

    private void OnMove(Vector2 movement)
    {
        if (CanMove)
            PreviousMovementInput = movement;
    }

    private void OnStartedRunning()
    {
        if (IsCrouched) return;

        IsRunning = true;

        Animator.SetBool("IsRunning", true);
    }

    private void OnStoppedRunning()
    {
        IsRunning = false;

        Animator.SetBool("IsRunning", false);
    }

    private void OnStartedCrouch()
    {
        if (IsRunning) return;

        IsCrouched = true;

        Animator.SetBool("IsCrouched", true);
    }

    private void OnStoppedCrouch()
    {
        IsCrouched = false;

        Animator.SetBool("IsCrouched", false);
    }

    private void ToggleVisibility()
    {
        CanMove = !CanMove;
        IsDetectable = !IsDetectable;
        _spriteRenderer.enabled = !_spriteRenderer.enabled;
        _playerLight.enabled = !_playerLight.enabled;
    }

    private void OnDestroy()
    {
        _playerTransformAnchor.Value = null;
    }
}
