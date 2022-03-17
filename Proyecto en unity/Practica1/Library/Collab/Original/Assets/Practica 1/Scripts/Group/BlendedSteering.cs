using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendedSteering : SteeringBehaviour
{

    private List<SteeringBehaviour> steerings;
    private Steering aux;

    public List<SteeringBehaviour> Steerings { get => steerings; set => steerings = value; }

    public void Start()
    {
        Steerings = new List<SteeringBehaviour>();
        aux = gameObject.AddComponent<Steering>();
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
            aux = s.getSteering(agent);
            Steering.Linear += aux.Linear * s.Weight;
            Steering.Angular += aux.Angular * s.Weight;
            //Debug.Log("Aux.Linear = " + aux.Linear + " s.Weight = " + s.Weight);
            //Debug.Log(s.GetType().Name + " " + Steering.Linear + " " + Steering.Angular);
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
