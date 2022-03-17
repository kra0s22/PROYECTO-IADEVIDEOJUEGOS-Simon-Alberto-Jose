using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : Seek
{
    public override Steering getSteering(AgentNPC agent)
    {
        float maxPrediction = 5;
        Vector3 direction = Target.transform.position - agent.transform.position;
        float distance = direction.magnitude;
        float speed = agent.Velocity.magnitude;
        float prediction;
        if(speed <= distance/ maxPrediction)
        {
             prediction = maxPrediction;
        }
        else
        {
            prediction = distance / speed;
        }
        Vector3 futurePosition = Target.Position + Target.Velocity * prediction;
        return base.getSteering(agent, futurePosition);
    }
}
