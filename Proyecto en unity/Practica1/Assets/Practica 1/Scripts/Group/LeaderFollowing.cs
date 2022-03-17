using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderFollowing : BlendedSteering
{
    private Arrive arrive;
    private Evade evade;
    private Separation separation;
    [SerializeField]
    private Agent targetEvade;

    private List<AgentNPC> targets;
    private LookWhereYouGoing lookWhereYouGoing;

    public Agent TargetEvade { get => targetEvade; set => targetEvade = value; }

    public override void Start()
    {
        base.Start();
        arrive = gameObject.AddComponent<Arrive>();
        evade = gameObject.AddComponent<Evade>();
        separation = gameObject.AddComponent<Separation>();
        lookWhereYouGoing = gameObject.AddComponent<LookWhereYouGoing>();

        arrive.Weight = 5f;
        separation.Weight = 3f;
        evade.Weight = 2f;

        evade.Target = TargetEvade;
        arrive.Target = Target;

        Steerings.Add(arrive);
        Steerings.Add(separation);
        Steerings.Add(evade);
    }

    public override Steering getSteering(AgentNPC agent)
    {
        if (separation.Targets.Count == 0)
        {
            targets = new List<AgentNPC>();
            targets.AddRange(FindObjectsOfType<AgentNPC>());
            targets.Remove(agent);
            targets.Remove((AgentNPC)Target);
            targets.Remove((AgentNPC)TargetEvade);
            separation.Targets = targets;
        }
        Steering = base.getSteering(agent);
        Steering.Angular = lookWhereYouGoing.getSteering(agent).Angular;
        return Steering;
    }
}