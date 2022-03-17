using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendedSteering : SteeringBehaviour
{
    private List<SteeringBehaviour> steerings;

    public List<SteeringBehaviour> Steerings { get => steerings; set => steerings = value; }

    public override void Start()
    {
        Steerings = new List<SteeringBehaviour>();
        base.Start();
    }

    public void addList(SteeringBehaviour node)
    {
        steerings.Add(node);
    }
    public override Steering getSteering(AgentNPC agent)
    {
        Steering.Linear = Vector3.zero;
        Steering.Angular = 0;
        foreach (SteeringBehaviour s in Steerings)
        {
            Steering.Linear += s.getSteering(agent).Linear * s.Weight;
            Steering.Angular += s.getSteering(agent).Angular * s.Weight;
        }

        if (Steering.Linear.magnitude > agent.MaxAcceleration)
        {
            Steering.Linear.Normalize();
            Steering.Linear = Steering.Linear.normalized * agent.MaxAcceleration;
        }

        if (Steering.Angular > agent.MaxAngular)
        {
            Steering.Angular = agent.MaxAngular;
        }
        return Steering;
    }
}
