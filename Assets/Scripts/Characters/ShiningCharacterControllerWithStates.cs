using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShiningCharacterControllerWithStates<T> : ShiningCharacterController where T : IState
{
    protected StateMachine<T> _stateMachine;

    private void FixedUpdate() => _stateMachine.PhysicsTick();

    private void Update() => _stateMachine.Tick();
}
