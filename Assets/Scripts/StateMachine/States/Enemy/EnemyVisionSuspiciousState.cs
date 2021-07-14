using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisionSuspiciousState : EnemySenseSuspiciousState<EnemyVisualDetector>
{
    public EnemyVisionSuspiciousState(
        Enemy enemy, 
        EnemyVisualDetector visualDetector) : base(enemy, visualDetector)
    {

    }

    public override void PhysicsTick()
    {
        base.PhysicsTick();
    }
}
