using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBase : SteeringBehaviour
{
    //empleamos Linear como el Velocity por comodidad.
    public override Steering getSteering(AgentNPC agent)
    {
        Steering.Linear = Target.Position - agent.Position;
        Steering.Linear = Steering.Linear.normalized;
        Steering.Linear *= agent.MaxVelocity;
        Steering.Angular = 0;
        return Steering;
    }

    public Steering getSteering(AgentNPC agent, Vector3 futurePosition)
    {
        Steering.Linear = futurePosition - agent.Position;
        Steering.Linear = Steering.Linear.normalized;
        //este es el cambio entre los dos
        Steering.Linear *= agent.MaxVelocity;
        Steering.Angular = 0;
        return Steering;

    }

}
