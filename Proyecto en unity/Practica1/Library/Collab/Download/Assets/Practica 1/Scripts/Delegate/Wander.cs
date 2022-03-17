using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Face
{
    private float wanderOffset = 2f;
    private float wanderRadius = 2f;
    private float wanderRate = 30f;
    private float wanderOrientation;
    public override Steering getSteering(AgentNPC agent)
    {
        float newOrientation = MapToRange(Random.Range(-1, 2) * wanderRate);
     
        wanderOrientation += newOrientation;        
        float targetOrientation = wanderOrientation + agent.Orientation;
        Vector3 agentOrientationAsVector = new Vector3(Mathf.Sin(agent.Orientation * Mathf.Deg2Rad), 0, Mathf.Cos(agent.Orientation * Mathf.Deg2Rad));
        Vector3 targetOrientationAsVector = new Vector3(Mathf.Sin(targetOrientation * Mathf.Deg2Rad), 0, Mathf.Cos(targetOrientation * Mathf.Deg2Rad));
        Vector3 tar = agent.Position + wanderOffset * agentOrientationAsVector;
        tar += wanderRadius * targetOrientationAsVector;
        Steering = base.getSteering(agent, tar);
        
        Steering.Linear = agent.MaxAcceleration * agentOrientationAsVector;
        return Steering;
    }


}
