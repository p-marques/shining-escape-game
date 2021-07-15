using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShiningCharacterControllerWithStates<T> : ShiningCharacterController where T : IState
{
    protected StateMachine<T> _stateMachine;

    protected virtual void FixedUpdate() => _stateMachine.PhysicsTick();

    protected virtual void Update() => _stateMachine.Tick();
}
