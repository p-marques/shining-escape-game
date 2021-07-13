using UnityEngine;

public class EnemyRoamState : IEnemyState
{
    private const float AT_POINT_TOLERANCE = 0.1f;

    private readonly Enemy _enemy;
    private readonly EnemyRoamPointsManager _pointsManager;
    private int _currentPointIndex;
    private float _currentWaitTimer;

    private float CurrentPoint => _pointsManager.Points[_currentPointIndex];

    public EnemyRoamState(Enemy enemy, EnemyRoamPointsManager roamPointsManager)
    {
        _enemy = enemy;
        _pointsManager = roamPointsManager;
        _currentPointIndex = 1;
        _currentWaitTimer = 0f;
    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {
        
    }

    public void PhysicsTick()
    {
        float distanceToTarget;

        if (_currentWaitTimer > 0f)
        {
            _currentWaitTimer -= Time.fixedDeltaTime;

            return;
        }

        distanceToTarget = CurrentPoint - _enemy.transform.position.x;

        if (Mathf.Abs(distanceToTarget) > AT_POINT_TOLERANCE)
        {
            _enemy.Move(Mathf.Clamp(distanceToTarget, -1.0f, 1.0f));
        }
        else
        {
            _currentWaitTimer = _pointsManager.PointChangeWaitTime;

            Debug.Log(_currentWaitTimer);

            if (_currentPointIndex + 1 < _pointsManager.Points.Length)
            {
                _currentPointIndex++;
            }
            else
            {
                _currentPointIndex = 0;
            }
        }
    }

    public void Tick()
    {
        // Set animator info
    }
}
