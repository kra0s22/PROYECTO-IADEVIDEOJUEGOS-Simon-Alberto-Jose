using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Seek : SteeringBehaviour
{
    private float distanciaParada = 0.1f;
    public override Steering getSteering(AgentNPC agent)
    {
        Steering.Linear = Target.Position - agent.Position;
        if(Steering.Linear.magnitude < distanciaParada)
        {
            Steering.Linear = (-agent.Velocity / Time.deltaTime);
            
            return Steering;
        }
        Steering.Linear = Steering.Linear.normalized;
        Steering.Linear *= agent.MaxAcceleration;
        Steering.Angular = 0;       
        return Steering;
    }

    public Steering getSteering(AgentNPC agent, Vector3 futurePosition)
    {
        Steering.Linear = futurePosition - agent.Position;
        if (Steering.Linear.magnitude < distanciaParada)
        {
            Steering.Linear = (-agent.Velocity / Time.deltaTime);
            return Steering;
        }
        Steering.Linear = Steering.Linear.normalized;
        Steering.Linear *= agent.MaxAcceleration;
        Steering.Angular = 0;
        return Steering;
    }

}
