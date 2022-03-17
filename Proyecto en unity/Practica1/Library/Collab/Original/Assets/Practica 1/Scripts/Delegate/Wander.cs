using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Face
{
    private float wanderOffset = 5f;
    private float wanderRadius = 4f;
    private float wanderRate = 30f;
    private float wanderOrientation;

    public Vector3 OrientationAsVector(float orientation)
    {
        return new Vector3(Mathf.Sin(orientation * Mathf.Deg2Rad), 0, Mathf.Cos(orientation * Mathf.Deg2Rad));
    }
    public override Steering getSteering(AgentNPC agent)
    {
        wanderOrientation += Random.Range(-1, 2) * wanderRate;
        wanderOrientation = MapToRange(wanderOrientation);
        float targetOrientation = wanderOrientation + agent.Orientation;
        Vector3 tar = agent.Position + wanderOffset * OrientationAsVector(agent.Orientation);
        tar += wanderRadius * OrientationAsVector(targetOrientation);
        Steering = base.getSteering(agent, tar);
        Steering.Linear = agent.MaxAcceleration * OrientationAsVector(agent.Orientation);
        return Steering;
    }
}
