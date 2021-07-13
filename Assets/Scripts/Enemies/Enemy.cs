using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ShiningCharacterControllerWithStates<IEnemyState>
{
    private EnemyRoamPointsManager _roamPointsManager;

    protected override void Awake()
    {
        base.Awake();

        _roamPointsManager = GetComponentInChildren<EnemyRoamPointsManager>();

        if (!_roamPointsManager)
            Debug.LogError("Enemy has no EnemyRoamPointsManager.");

        _stateMachine = new StateMachine<IEnemyState>();

        EnemyRoamState roamState = new EnemyRoamState(this, _roamPointsManager);

        _stateMachine.SetState(roamState);
    }

    private void FixedUpdate() => _stateMachine.PhysicsTick();

    private void Update() => _stateMachine.Tick();
}
