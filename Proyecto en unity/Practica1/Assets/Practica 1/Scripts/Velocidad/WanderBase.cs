using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBase : SteeringBehaviour
{
    public override Steering getSteering(AgentNPC agent)
    {
        Vector3 agentOrientationAsVector = new Vector3(Mathf.Sin(agent.Orientation * Mathf.Deg2Rad), 
            0, Mathf.Cos(agent.Orientation * Mathf.Deg2Rad));
        Steering.Linear = agent.MaxVelocity * agentOrientationAsVector;
        Steering.Angular = Random.Range(-1, 2) * agent.MaxRotation;
        return Steering;
    }
}
