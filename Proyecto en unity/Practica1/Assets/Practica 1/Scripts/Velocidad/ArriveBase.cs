using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveBase : SteeringBehaviour
{
    public override Steering getSteering(AgentNPC agent)
    {

        float timeToTarget = 0.1f;
        Vector3 direction = Target.Position - agent.Position;
        float distance = direction.magnitude;

        if (distance < Target.InteriorRadius)
        {
            return new Steering();
        }

        direction /= timeToTarget;
        Steering.Linear = direction;

        if (Steering.Linear.magnitude > agent.MaxVelocity)
        {
            Steering.Linear = Steering.Linear.normalized;
            Steering.Linear *= agent.MaxVelocity;
        }
        Steering.Angular = 0;
        return Steering;
    }
}
