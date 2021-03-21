﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T: IState
{
    private Dictionary<Type, List<StateTransition>> _transitions;

    public T CurrentState { get; private set; }

    public StateMachine()
    {
        _transitions = new Dictionary<Type, List<StateTransition>>();
    }

    public void PhysicsTick() => CurrentState?.PhysicsTick();

    public void Tick()
    {
        StateTransition transition = GetTransition();

        if (transition != null)
            SetState(transition.Destination);

        CurrentState?.Tick();
    }

    public void SetState(IState state)
    {
        if (state == (CurrentState as IState))
            return;

        Debug.Log($"FSM: {CurrentState?.GetType().Name} --> {state.GetType().Name}");

        CurrentState?.OnExit();
        
        CurrentState = (T)state;

        CurrentState.OnEnter();
    }

    public void AddTransiction(IState origin, IState destination, Func<bool> condition)
    {
        Type originType = origin.GetType();
        bool isListed = _transitions
            .TryGetValue(originType, out List<StateTransition> stateTransitions);

        if (!isListed)
        {
            stateTransitions = new List<StateTransition>();
            _transitions[originType] = stateTransitions;
        }

        stateTransitions.Add(new StateTransition(destination, condition));
    }

    private StateTransition GetTransition()
    {
        Type stateType = CurrentState.GetType();

        if (_transitions.ContainsKey(stateType))
        {
            List<StateTransition> availableTransitions = _transitions[stateType];

            for (int i = 0; i < availableTransitions.Count; i++)
            {
                StateTransition transition = availableTransitions[i];

                if (transition.Condition())
                    return transition;
            }
        }

        return null;
    }
}
