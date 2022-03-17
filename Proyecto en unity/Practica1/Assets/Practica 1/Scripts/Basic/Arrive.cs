using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : SteeringBehaviour
{
    public override Steering getSteering(AgentNPC agent)
    {
        float TargetSpeed;
        float timeToTarget = 0.1f;
        Vector3 direction = Target.Position - agent.Position;
        float distance = direction.magnitude;
        
        if (distance < Target.InteriorRadius)
        {
            if (agent.Velocity.magnitude == 0)
            {
                Steering.Linear = Vector3.zero;
                Steering.Angular = 0;
                return Steering;
            }
            Steering.Linear = -agent.Velocity;
            Steering.Angular = 0;
            return Steering;
        }

        if (distance > Target.ExteriorRadius)
            TargetSpeed  = agent.MaxVelocity;
        else
            TargetSpeed = agent.MaxVelocity * distance / Target.ExteriorRadius;
        Vector3 TargetVelocity;
        TargetVelocity = direction;
        TargetVelocity = TargetVelocity.normalized;
        TargetVelocity *= TargetSpeed;
        Steering.Linear = TargetVelocity - agent.Velocity;
        Steering.Linear /= timeToTarget;
               
        if (Steering.Linear.magnitude > agent.MaxAcceleration)
        {
            Steering.Linear = Steering.Linear.normalized;
            Steering.Linear *= agent.MaxAcceleration;
        }
        Steering.Angular = 0;
        return Steering;
    }
}
