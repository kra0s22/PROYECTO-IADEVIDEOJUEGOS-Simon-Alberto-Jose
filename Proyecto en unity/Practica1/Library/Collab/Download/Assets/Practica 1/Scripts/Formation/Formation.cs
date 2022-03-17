using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : SteeringBehaviour
{
    Arrive arrive;
    Align align;
    [SerializeField]
    Agent lider;
    LineFormation lineFormation;
    [SerializeField]
    int formationPosition;

    public LineFormation LineFormation { get => lineFormation; set => lineFormation = value; }

    private void Start()
    {
        base.Start();
        arrive = gameObject.AddComponent<Arrive>();
        align = gameObject.AddComponent<Align>();
        align.Target = lider;
        LineFormation = gameObject.AddComponent<LineFormation>();
        LineFormation.Lider = lider;
        LineFormation.FormationPosition = formationPosition;

    }
    public override Steering getSteering(AgentNPC agent)
    {
        Agent targetFicticio = LineFormation.calculateTarget(agent);
        arrive.Target = targetFicticio;
        align.Target = targetFicticio;
        if ((targetFicticio.Position.magnitude - agent.Position.magnitude) < 5)
            Steering.Angular = align.getSteering(agent).Angular;
        Steering.Linear = arrive.getSteering(agent).Linear;
        return Steering;
    }
}
