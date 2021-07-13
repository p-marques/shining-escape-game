using UnityEngine;

public class PlayerNoiseGenerator : MonoBehaviour
{
    [Header("Sound Traveling Radius")]
    [Range(1f, 1000f)]
    [SerializeField] private float _standingIdle = 100f;
    [Range(1f, 1000f)]
    [SerializeField] private float _standingWalking = 300f;
    [Range(1f, 1000f)]
    [SerializeField] private float _standingRunning = 500f;
    [Range(1f, 1000f)]
    [SerializeField] private float _crouchedIdle = 40f;
    [Range(1f, 1000f)]
    [SerializeField] private float _crouchedWalking = 80f;

    [Header("Events")]
    [SerializeField] private PlayerStanceEventChannelSO _onPlayerStanceChange;

    private PlayerController _player;
    private CircleCollider2D _circleCollider2D;
    private float _currentNoiseRadius;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerController>();
        _circleCollider2D = GetComponent<CircleCollider2D>();

        if (!_player)
            Debug.LogError("PlayerNoiseGenerator can't find PlayerController");
        else if (!_circleCollider2D)
            Debug.LogError("PlayerNoiseGenerator can't find CircleCollider2D");
    }

    private void Update()
    {
        if (!_player || !_circleCollider2D) return;

        UpdateNoiseRadius();
    }

    private void UpdateNoiseRadius()
    {
        float newRadius = GetNoiseRadiusByPlayerStance();

        if (newRadius != _currentNoiseRadius)
        {
            _currentNoiseRadius = newRadius;
            _circleCollider2D.radius = _currentNoiseRadius;

            if (_onPlayerStanceChange)
                _onPlayerStanceChange.RaiseEvent(_player.CurrentStance);
        }
    }

    private float GetNoiseRadiusByPlayerStance()
    {
        switch (_player.CurrentStance)
        {
            case PlayerStance.StandingWalking:
                return _standingWalking;
            case PlayerStance.StandingRunning:
                return _standingRunning;
            case PlayerStance.CrouchedIdle:
                return _crouchedIdle;
            case PlayerStance.CrouchWalking:
                return _crouchedWalking;
            default:
                return _standingIdle;
        }
    }
}
