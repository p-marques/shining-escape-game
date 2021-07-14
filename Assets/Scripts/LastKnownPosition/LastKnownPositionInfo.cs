using UnityEngine;

public struct LastKnownPositionInfo
{
    public Vector3 Position { get; }
    public LastKnownPositionMode Mode { get; }

    public LastKnownPositionInfo(Vector3 position, LastKnownPositionMode mode)
    {
        Position = position;
        Mode = mode;
    }
}
