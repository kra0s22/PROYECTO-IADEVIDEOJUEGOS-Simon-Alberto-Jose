using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Flee : SteeringBehaviour
{

    public override Steering getSteering(AgentNPC agent)
    {
        Steering.Linear = agent.Position - Target.Position;
        Steering.Linear = Steering.Linear.normalized;
        Steering.Linear *= agent.MaxAcceleration;
        Steering.Angular = 0;
        return Steering;

    }

    //necesario para evade
    public Steering getSteering(AgentNPC agent, Vector3 futurePosition)
    {
        
        Steering.Linear = agent.Position - futurePosition;
        Steering.Linear = Steering.Linear.normalized;
        Steering.Linear *= agent.MaxAcceleration;
        Steering.Angular = 0;
        return Steering;

    }
}
