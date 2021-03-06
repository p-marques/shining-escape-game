using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : IEnemyState
{
    private readonly ShiningCharacterController _enemy;

    public PlayerController Target { get; set; }

    public EnemyChaseState(ShiningCharacterController enemy)
    {
        _enemy = enemy;
    }

    public void OnEnter()
    {
        if (!Target)
            Debug.LogError("EnemyChaseState has no target");
        else
            Target.IsBeingChased = true;
    }

    public void OnExit()
    {
        Target.IsBeingChased = false;
        Target = null;
    }

    public void PhysicsTick()
    {
        float distanceToTarget;

        if (!Target) return;

        distanceToTarget = Target.transform.position.x - _enemy.transform.position.x;

        _enemy.Move(Mathf.Clamp(distanceToTarget, -1.0f, 1.0f));
    }

    public void Tick()
    {
        
    }
}
