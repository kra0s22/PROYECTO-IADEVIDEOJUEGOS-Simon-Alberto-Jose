using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evade : Flee
{
    private float maxDistance;
    public override Steering getSteering(AgentNPC agent)
    {
        maxDistance = 20f;
        float maxPrediction = 3;
        Vector3 direction = Target.transform.position - agent.transform.position;
        float distance = direction.magnitude;
        float speed = agent.Velocity.magnitude;
        float prediction;

        if (distance > maxDistance)
        {
            Steering.Linear = Vector3.zero;
            Steering.Angular = 0;
            return Steering;
        }
        if (speed <= distance/ maxPrediction)
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
