using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMatching : SteeringBehaviour
{
    float timeToTarget = 0.1f;

    public override Steering getSteering(AgentNPC agent)
    {
        Steering.Linear = Target.Velocity - agent.Velocity;
        Steering.Linear /= timeToTarget;
        if(Steering.Linear.magnitude > agent.MaxAcceleration)
        {
            Steering.Linear = Steering.Linear.normalized;
            Steering.Linear *= agent.MaxAcceleration;
        }
        Steering.Angular = 0;
        return Steering;
    }
}
