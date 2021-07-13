using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalState : IPlayerState
{
    private readonly PlayerController _playerController;

    public PlayerNormalState(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public void PhysicsTick()
    {
        _playerController.Move(_playerController.PreviousMovementInput.x);
    }

    public void Tick()
    {
        _playerController.Animator.SetFloat("AbsVelX", Mathf.Abs(_playerController.Rigidbody.velocity.x));
    }
}
