using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Face
{
    private float wanderOffset = 5f;
    private float wanderRadius = 4f;
    private float wanderRate = 30f;
    private float wanderOrientation;
    private Agent tar;

    public override void Start()
    {
        base.Start();
        tar = gameObject.AddComponent<Agent>();
    }

    public override Steering getSteering(AgentNPC agent)
    {
        wanderOrientation += Random.Range(-1, 2) * wanderRate;
        wanderOrientation = MapToRange(wanderOrientation);
        tar.Orientation = wanderOrientation + agent.Orientation;
        tar.Position = agent.Position + wanderOffset * agent.OrientationAsVector();
        tar.Position += wanderRadius * tar.OrientationAsVector();
        Steering = base.getSteering(agent, tar.Position);
        Steering.Linear = agent.MaxAcceleration * agent.OrientationAsVector();
        return Steering;
    }
}
