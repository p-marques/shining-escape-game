using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IPlayerState
{
    private PlayerController _playerController;

    public PlayerJumpState(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        _playerController.Animator.SetFloat("AbsVelY", 0f);
    }

    public void PhysicsTick()
    {
        _playerController.Move(_playerController.PreviousMovementInput.x, true);
    }

    public void Tick()
    {
        float yVelocity = _playerController.Rigidbody.velocity.y;

        _playerController.Animator.SetFloat("AbsVelY", Mathf.Abs(yVelocity));
        _playerController.Animator.SetFloat("VelY", yVelocity);
    }
}
