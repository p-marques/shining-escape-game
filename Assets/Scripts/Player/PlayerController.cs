using UnityEngine;

public class PlayerController : ShiningCharacterControllerWithStates<IPlayerState>
{
    [Space]
    [Header("Input")]
    [SerializeField] private InputReaderSO _inputReader;

    private bool IsMoving => Mathf.Abs(Rigidbody.velocity.x) > 0.1f;
    public Vector2 PreviousMovementInput { get; private set; }
    
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

        _stateMachine = new StateMachine<IPlayerState>();

        PlayerNormalState playerNormalState = new PlayerNormalState(this);

        _stateMachine.SetState(playerNormalState);
    }

    private void OnEnable()
    {
        _inputReader.MoveEvent += OnMove;
        _inputReader.StartedRunningEvent += OnStartedRunning;
        _inputReader.StoppedRunningEvent += OnStoppedRunning;
        _inputReader.StartedCrouchEvent += OnStartedCrouch;
        _inputReader.StoppedCrouchEvent += OnStoppedCrouch;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMove;
        _inputReader.StartedRunningEvent -= OnStartedRunning;
        _inputReader.StoppedRunningEvent -= OnStoppedRunning;
        _inputReader.StartedCrouchEvent -= OnStartedCrouch;
        _inputReader.StoppedCrouchEvent -= OnStoppedCrouch;
    }

    private void OnMove(Vector2 movement)
    {
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
}
