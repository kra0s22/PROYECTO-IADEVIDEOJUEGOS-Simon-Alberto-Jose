using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cohesion : Seek
{
    private List<AgentNPC> targets;
    private float decayCoefficient;
    private float threshold;
    public float Threshold { get => threshold; set => threshold = value; }
    public List<AgentNPC> Targets { get => targets; set => targets = value; }

    public override void Start()
    {
        base.Start();
        Threshold = 20;
        Targets = new List<AgentNPC>();
    }

    public override Steering getSteering(AgentNPC agent)
    {
        Steering.Linear = new Vector3(0, 0, 0);
        Steering.Angular = 0;

        int count = 0;
        Vector3 direction;
        Vector3 centerOfMass = new Vector3(0, 0, 0);
        float distance;
        foreach (Agent target in Targets)
        {
            direction = agent.Position - target.Position;
            distance = direction.magnitude;
            distance = Mathf.Abs(distance);
            if (distance > threshold)
            {
                continue;
            }
            centerOfMass += target.Position;
            count++;

        }
        if (count == 0)
        {
            Steering.Linear = Vector3.zero;
            Steering.Angular = 0;
            return Steering;
        }
        centerOfMass /= count;
        return base.getSteering(agent, centerOfMass);
    }
}
