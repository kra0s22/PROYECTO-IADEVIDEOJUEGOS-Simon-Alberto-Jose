using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{

    public override Steering getSteering(AgentNPC agent)
    {
        Steering steering = new Steering();
        steering.Linear = target.Position - agent.Position;
        steering.Linear.Normalize();
        steering.Linear *= agent.MaxVelocity;
        agent.Orientation = agent.OrientationToVector(steering);
        steering.Angular = 0;
        return steering;
    }

}
