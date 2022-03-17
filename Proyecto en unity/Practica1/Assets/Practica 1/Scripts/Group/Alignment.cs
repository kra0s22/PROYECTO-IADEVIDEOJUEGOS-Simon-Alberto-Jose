using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alignment : SteeringBehaviour
{
    private List<AgentNPC>  targets;
    private float decayCoefficient;
    private float threshold;
    public List<AgentNPC>  Targets { get => targets; set => targets = value; }
    public float Threshold { get => threshold; set => threshold = value; }

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
        Vector3 Heading = new Vector3(0, 0, 0);
        Vector3 direction;
        float distance;
       
        foreach (Agent tar in Targets)
        {
            direction = tar.Position - agent.Position;
            distance = direction.magnitude;
            distance = Mathf.Abs(distance);
 
            if (distance > threshold)
                continue;

            Heading += tar.OrientationAsVector();
            count++;
        }
        if (count > 1)
        {
            Heading -= agent.OrientationAsVector();
            count--;
            Heading /= count;
            
            Steering.Linear = Heading;
            return Steering;
        } else
        {
            Steering.Linear = Heading;
            return Steering;
        }
    }
}
