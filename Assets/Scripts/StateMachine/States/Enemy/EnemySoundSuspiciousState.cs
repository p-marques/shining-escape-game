using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundSuspiciousState : EnemySenseSuspiciousState<EnemySoundDetector>
{
    public EnemySoundSuspiciousState(
        Enemy enemy,
        EnemySoundDetector soundDetector) : base(enemy, soundDetector)
    {
    }

    public override void PhysicsTick()
    {
        float targetPositionX;

        base.PhysicsTick();

        if (_sense.DetectedPlayer != null)
        {
            targetPositionX = _sense.DetectedPlayer.transform.position.x;
        }
        else
        {
            targetPositionX = _sense.LastKnownPosition.Value.x;
        }

        _enemy.Move(Mathf.Clamp(targetPositionX - _enemy.transform.position.x, -1.0f, 1.0f));
    }
}
