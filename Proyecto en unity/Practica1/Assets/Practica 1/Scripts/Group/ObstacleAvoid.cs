using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoid : BlendedSteering
{
    private WallAvoidance wallAvoidance;
    private Wander wander;

    public override void Start()
    {
        base.Start();
        wallAvoidance = gameObject.AddComponent<WallAvoidance>();
        wander = gameObject.AddComponent<Wander>();
        wallAvoidance.Weight = 5;
        wander.Weight = 1;
        Steerings.Add(wander);
        Steerings.Add(wallAvoidance);
    }
}
