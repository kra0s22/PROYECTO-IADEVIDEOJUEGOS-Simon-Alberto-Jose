using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//No se como poner la lista de targets, todavia no esta probado el codigo a ver si funciona.


public class Separation : SteeringBehaviour
{
    
    private List<AgentNPC> targets;
    private float decayCoefficient = 10f;
    private float threshold;

    public List<AgentNPC> Targets { get => targets; set => targets = value; }
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
        Vector3 direction;
        float distance;
        float strength;
        foreach (Agent target in targets)
        {
            direction = agent.Position - target.Position;
            distance = direction.magnitude;
            distance = Mathf.Abs(distance);
            if (distance < threshold)
            {                
                strength = Mathf.Min(decayCoefficient / (distance * distance), agent.MaxAcceleration);
                direction.Normalize();
                Steering.Linear += strength * direction;
            }
        } 
        return Steering;
    }
}
