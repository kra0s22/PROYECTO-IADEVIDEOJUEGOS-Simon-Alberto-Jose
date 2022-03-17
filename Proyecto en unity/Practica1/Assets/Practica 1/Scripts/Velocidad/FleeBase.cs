using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeBase : SteeringBehaviour
{
    public override Steering getSteering(AgentNPC agent)
    {
        Steering.Linear = agent.Position - Target.Position;
        Steering.Linear = Steering.Linear.normalized;
        Steering.Linear *= agent.MaxVelocity;
        Steering.Angular = 0;
        return Steering;

    }
}
