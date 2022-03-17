using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFLWYG : SteeringBehaviour
{
    private PathFollowing pathFollowing;
    private LookWhereYouGoing lookWhereYouGoing;

    public PathFollowing PathFollowing { get => pathFollowing; set => pathFollowing = value; }

    public override void Start()
    {
        base.Start();
        PathFollowing = gameObject.AddComponent<PathFollowing>();
        lookWhereYouGoing = gameObject.AddComponent<LookWhereYouGoing>();
    }

    public override Steering getSteering(AgentNPC agent)
    {
        Steering = pathFollowing.getSteering(agent);
        Steering.Angular = lookWhereYouGoing.getSteering(agent).Angular;
        return Steering;
    }

    private void OnDestroy()
    {
        Destroy(PathFollowing);
        Destroy(lookWhereYouGoing);
    }
}
