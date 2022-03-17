using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Form : SteeringBehaviour
{
    private Arrive arrive;
    private Align align;
    public Agent lider;
    private Formation formation;
    public int formationType;   
    public int formationPosition;

    public Formation Formation { get => formation; set => formation = value; }

    public override void Start()
    {
        base.Start();
        if(formationType == 1) // CIRCLE
        {
            Formation = gameObject.AddComponent<Circle>();
        }
        else // LINE
        {
            Formation = gameObject.AddComponent<Line>();
        }
        arrive = gameObject.AddComponent<Arrive>();
        align = gameObject.AddComponent<Align>();
        align.Target = lider;
        Formation.Lider = lider;
        Formation.FormationPosition = formationPosition;
    }
    public override Steering getSteering(AgentNPC agent)
    {
        Agent targetFicticio = Formation.calculateTarget(agent); 
        arrive.Target = targetFicticio;
        align.Target = targetFicticio;
        if ((targetFicticio.Position.magnitude - agent.Position.magnitude) < 5)
            Steering.Angular = align.getSteering(agent).Angular;
        Steering.Linear = arrive.getSteering(agent).Linear;
        return Steering;
    }
}
