using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySenseSuspiciousState<T> : IEnemyState where T: EnemySense
{
    protected readonly Enemy _enemy;
    protected readonly T _sense;
    private float _freezeTimer;

    public EnemySenseSuspiciousState(Enemy enemy, T enemySense)
    {
        _enemy = enemy;
        _sense = enemySense;
    }

    public virtual void OnEnter()
    {
        _freezeTimer = _enemy.SuspiciousOnEnterFreezeTime;
    }

    public virtual void OnExit()
    {
        
    }

    public virtual void PhysicsTick()
    {
        if (_freezeTimer > 0f)
        {
            _freezeTimer -= Time.fixedDeltaTime;

            return;
        }
    }

    public virtual void Tick()
    {
        
    }
}
