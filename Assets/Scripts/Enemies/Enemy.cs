using UnityEngine;

public class Enemy : ShiningCharacterControllerWithStates<IEnemyState>
{
    [Header("State Specific Settings")]
    [Range(0.0f, 2f)]
    [SerializeField] private float _suspiciousOnEnterFreezeTime = 0.5f;
    [Range(2, 10)]
    [SerializeField] private int _investigatingCycles = 4;
    [Range(0.1f, 2f)]
    [SerializeField] private float _investigatingCycleTime = 0.5f;

    [Header("Events")]
    [SerializeField] private LastKnownPositionEventChannelSO _onEnemyInvestigatingLastKnownPosition;
    [SerializeField] private VoidEventChannelSO _onStoppedInvestigating;

    private EnemyRoamPointsManager _roamPointsManager;
    private EnemySoundDetector _soundDetector;
    private EnemyVisualDetector _visualDetector;

    public float SuspiciousOnEnterFreezeTime => _suspiciousOnEnterFreezeTime;
    public float InvestigatingCycles => _investigatingCycles;
    public float InvestigatingCycleTime => _investigatingCycleTime;
    public bool IsInvestigating { get; set; }

    protected override void Awake()
    {
        base.Awake();

        _roamPointsManager = GetComponentInChildren<EnemyRoamPointsManager>();
        _soundDetector = GetComponent<EnemySoundDetector>();
        _visualDetector = GetComponentInChildren<EnemyVisualDetector>();

        if (!_roamPointsManager || !_soundDetector || !_visualDetector)
            Debug.LogError("Enemy is missing required component(s).");

        _stateMachine = new StateMachine<IEnemyState>();

        EnemyRoamState roamState = new EnemyRoamState(this, _roamPointsManager);
        EnemySoundSuspiciousState soundSuspiciousState = new EnemySoundSuspiciousState(this, _soundDetector);
        EnemyVisionSuspiciousState visualSuspiciousState = new EnemyVisionSuspiciousState(this, _visualDetector);
        EnemyInvestigateState investigateState = new EnemyInvestigateState(this, _onStoppedInvestigating);
        EnemyChaseState chaseState = new EnemyChaseState(this);

        // EnemyRoamState to X
        _stateMachine.AddTransition(roamState, soundSuspiciousState, () => _soundDetector.DetectedPlayer != null);
        _stateMachine.AddTransition(roamState, visualSuspiciousState, () => _visualDetector.IsSuspecting);

        // EnemySoundSuspiciousState to X
        _stateMachine.AddTransition(soundSuspiciousState, investigateState, 
            () => _soundDetector.DetectedPlayer == null, 
            () => OnEnterInvestigation(investigateState,
            _soundDetector.LastKnownPosition, LastKnownPositionMode.Sound));
        _stateMachine.AddTransition(soundSuspiciousState, visualSuspiciousState, () => _visualDetector.IsSuspecting);

        // EnemyVisionSuspiciousState to X
        _stateMachine.AddTransition(visualSuspiciousState, investigateState, 
            () => _visualDetector.DetectedPlayer == null,
            () => OnEnterInvestigation(investigateState, 
            _visualDetector.LastKnownPosition, LastKnownPositionMode.Visual));
        _stateMachine.AddTransition(visualSuspiciousState, chaseState, 
            () => _visualDetector.PlayerFullyDetected,
            () => chaseState.Target = _visualDetector.DetectedPlayer);

        // EnemyInvestigateState to X
        _stateMachine.AddTransition(investigateState, roamState, () => !IsInvestigating);
        _stateMachine.AddTransition(investigateState, soundSuspiciousState, () => _soundDetector.DetectedPlayer != null);
        _stateMachine.AddTransition(investigateState, visualSuspiciousState, () => _visualDetector.IsSuspecting);

        // EnemyChaseState to X
        _stateMachine.AddTransition(chaseState, investigateState, 
            () => !_visualDetector.PlayerFullyDetected && _visualDetector.LastKnownPosition.HasValue,
            () => OnEnterInvestigation(investigateState,
            _visualDetector.LastKnownPosition, LastKnownPositionMode.Visual));
        _stateMachine.AddTransition(chaseState, roamState,
            () => !_visualDetector.LastKnownPosition.HasValue && !_visualDetector.DetectedPlayer);

        _stateMachine.SetState(roamState);
    }

    protected override void Update()
    {
        base.Update();

        Animator.SetFloat("AbsVelX", Mathf.Abs(Rigidbody.velocity.x));
    }

    private void OnEnterInvestigation(
        EnemyInvestigateState state, 
        Vector3? lastKnownPosition, 
        LastKnownPositionMode mode)
    {
        state.Target = lastKnownPosition;

        IsInvestigating = true;

        if (_onEnemyInvestigatingLastKnownPosition && lastKnownPosition.HasValue)
            _onEnemyInvestigatingLastKnownPosition.RaiseEvent(new LastKnownPositionInfo(state.Target.Value, mode));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SideViewDoor door = collision.gameObject.GetComponent<SideViewDoor>();

        if (door && !door.IsOpen)
            door.Interact();
    }
}
