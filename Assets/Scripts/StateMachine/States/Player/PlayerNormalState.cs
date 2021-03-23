using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalState : IPlayerState
{
    private PlayerController _playerController;

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

        if (_playerController.JumpInput)
            _playerController.Jump();
    }

    public void Tick()
    {
        float xVelocity = _playerController.Rigidbody.velocity.x;

        if (xVelocity < -0.5f)
        {
            if (_playerController.transform.right.x > 0)
                _playerController.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (xVelocity > 0.5f)
        {
            if (_playerController.transform.right.x < 0)
                _playerController.transform.rotation = Quaternion.identity;
        }

        _playerController.Animator.SetFloat("AbsVelX", Mathf.Abs(xVelocity));
    }
}
