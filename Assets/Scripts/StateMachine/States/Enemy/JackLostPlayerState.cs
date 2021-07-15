using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackLostPlayerState : IEnemyState
{
    private readonly JackController _jack;
    private bool _isAttacking;

    public JackLostPlayerState(JackController jack)
    {
        _jack = jack;
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        _jack.Animator.SetBool("IsAttacking", false);
    }

    public void PhysicsTick()
    {
        float direction = _jack.transform.right.x;

        _jack.Move(direction);
    }

    public void Tick()
    {
        bool shouldAttack = _jack.DestructibleDoor && !_jack.DestructibleDoor.IsDestroyed;

        if (shouldAttack && !_isAttacking)
        {
            _jack.DestructibleDoor.Attack();
        }

        _isAttacking = shouldAttack;

        _jack.Animator.SetBool("IsAttacking", _isAttacking);
    }
}
