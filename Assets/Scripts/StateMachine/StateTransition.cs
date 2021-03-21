using System;

public class StateTransition
{
    public Func<bool> Condition { get; }
    public IState Destination { get; }

    public StateTransition(IState destination, Func<bool> condition)
    {
        Condition = condition;

        Destination = destination;
    }
}
