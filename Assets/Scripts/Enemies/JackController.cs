using System.Collections;
using UnityEngine;

public class JackController : ShiningCharacterControllerWithStates<IEnemyState>
{
    [SerializeField] private TransformAnchorSO _playerTransformAnchor;

    [Range(0.2f, 5f)]
    [SerializeField] private float _playerChaseGracePeriod = 2f;

    private EnemyVisualDetector _visualDetector;
    private WaitForSeconds _startingSequenceWait;
    private bool _canMove;
    
    public SideViewDoor DestructibleDoor { get; private set; }

    private PlayerController Player
    {
        get
        {
            if (_playerTransformAnchor.IsSet)
                return _playerTransformAnchor.Value.GetComponent<PlayerController>();
            else
                return null;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        _startingSequenceWait = new WaitForSeconds(_playerChaseGracePeriod);
        _visualDetector = GetComponentInChildren<EnemyVisualDetector>();

        _stateMachine = new StateMachine<IEnemyState>();

        JackDefaultState jackDefaultState = new JackDefaultState();
        EnemyChaseState chaseState = new EnemyChaseState(this);
        JackLostPlayerState lostPlayer = new JackLostPlayerState(this);

        _canMove = false;

        _stateMachine.AddTransition(jackDefaultState, chaseState, () => _canMove,
            () => OnBeginChase(chaseState));
        _stateMachine.AddTransition(chaseState, lostPlayer, 
            () => _visualDetector.DetectedPlayer == null);
        _stateMachine.AddTransition(lostPlayer, chaseState,
            () => _canMove && _visualDetector.DetectedPlayer,
            () => OnBeginChase(chaseState));

        _stateMachine.SetState(jackDefaultState);
    }

    private void Start()
    {
        StartCoroutine(StartingSequenceCR());
    }

    protected override void Update()
    {
        base.Update();

        Animator.SetFloat("AbsVelX", Mathf.Abs(Rigidbody.velocity.x));
    }

    private IEnumerator StartingSequenceCR()
    {
        Debug.Log("Jack booting...");

        yield return _startingSequenceWait;

        _canMove = true;
    }

    private void OnBeginChase(EnemyChaseState chaseState)
    {
        if (_playerTransformAnchor.IsSet)
        {
            chaseState.Target = Player;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SideViewDoor door = collision.gameObject.GetComponent<SideViewDoor>();

        if (door)
            DestructibleDoor = door;
    }
}
