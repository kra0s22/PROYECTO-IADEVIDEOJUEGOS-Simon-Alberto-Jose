using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : BlendedSteering
{
    private Cohesion cohesion;
    private Separation separation;
    private Alignment alignment;
    private Seek seek;
    private List<AgentNPC> targets;
    private LookWhereYouGoing lookWhereYouGoing;

    public override void Start()
    {
        base.Start();
        cohesion = gameObject.AddComponent<Cohesion>();
        separation = gameObject.AddComponent<Separation>();
        alignment = gameObject.AddComponent<Alignment>();
        seek = gameObject.AddComponent<Seek>();
        lookWhereYouGoing = gameObject.AddComponent<LookWhereYouGoing>();

        cohesion.Weight = 3f;
        separation.Weight = 5f;
        alignment.Weight = 2f;
        seek.Weight = 1f;
        seek.Target = Target;

        Steerings.Add(cohesion);
        Steerings.Add(separation);
        Steerings.Add(alignment);
        Steerings.Add(seek);
    }

    public override Steering getSteering(AgentNPC agent)
    {
        if (cohesion.Targets.Count == 0)
        {
            targets = new List<AgentNPC>();
            targets.AddRange(FindObjectsOfType<AgentNPC>());
            targets.Remove(agent);
            targets.Remove((AgentNPC)Target);
            cohesion.Targets = targets;
            separation.Targets = targets;
            alignment.Targets = targets;
        }
        Steering = base.getSteering(agent);
        Steering.Angular = lookWhereYouGoing.getSteering(agent).Angular;
        return Steering;
    }
}
