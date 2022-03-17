using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoid : BlendedSteering
{
    private WallAvoidance wallAvoidance;
    private Wander wander;

    private void Start()
    {
        base.Start();
        wallAvoidance = gameObject.AddComponent<WallAvoidance>();
        wander = gameObject.AddComponent<Wander>();
        Steerings.Add(wander);
        Steerings.Add(wallAvoidance);        
        wallAvoidance.Weight = 6;
        wander.Weight = 1;
    }

    public override Steering getSteering(AgentNPC agent)
    {
        return base.getSteering(agent);
    }
}
