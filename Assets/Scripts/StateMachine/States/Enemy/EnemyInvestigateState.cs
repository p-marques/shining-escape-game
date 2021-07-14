using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInvestigateState : IEnemyState
{
    private const float ON_LOCATION_TOLERANCE = 0.1f;

    private readonly Enemy _enemy;
    private readonly VoidEventChannelSO _onExitEvent;
    private int _currentCycle;
    private float _currentCycleTimer;
    private float _currentCycleDirection;

    public Vector3? Target { get; set; }

    public EnemyInvestigateState(Enemy enemy, VoidEventChannelSO eventOnExit)
    {
        _enemy = enemy;
        _onExitEvent = eventOnExit;
    }

    public void OnEnter()
    {
        if (!Target.HasValue)
        {
            Debug.LogError("EnemyInvestigateState has no target");
            _enemy.IsInvestigating = false;
        }

        _currentCycle = 0;
    }

    public void OnExit()
    {
        Target = null;

        if (_onExitEvent)
            _onExitEvent.RaiseEvent();
    }

    public void PhysicsTick()
    {
        float distanceToTarget = Target.Value.x - _enemy.transform.position.x;
        float direction = Mathf.Clamp(distanceToTarget, -1.0f, 1.0f);

        if (_currentCycle == _enemy.InvestigatingCycles)
        {
            _enemy.IsInvestigating = false;

            return;
        }

        if (Mathf.Abs(distanceToTarget) <= ON_LOCATION_TOLERANCE && _currentCycle == 0)
        {
            _currentCycle++;
            _currentCycleTimer = _enemy.InvestigatingCycleTime;
            _currentCycleDirection = -_enemy.transform.right.x;
        }

        if (_currentCycle > 0)
        {
            if (_currentCycleTimer <= 0.0f)
            {
                _currentCycleTimer = _enemy.InvestigatingCycleTime;
                _currentCycleDirection = -_currentCycleDirection;
                _currentCycle++;
            }
            else
            {
                _currentCycleTimer -= Time.fixedDeltaTime;
            }

            direction = _currentCycleDirection;
        }

        _enemy.Move(direction);
    }

    public void Tick()
    {
        
    }
}
