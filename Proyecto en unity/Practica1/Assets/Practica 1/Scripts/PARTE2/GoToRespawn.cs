using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToRespawn : State
{
    private PFLWYG pathFollowing;
    public override void Start()
    {
        base.Start();
    }
    // PATHFINDING HASTA BASE -> PATHFOLLOWING
    public override void Execute()
    {
        if (Agent.Path == null)
        {
            Agent.Path = gameObject.AddComponent<Path>();
            Agent.PathFinding = gameObject.AddComponent<PathFinding>();            
            Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position, Agent.DeadPosition); //POSICION DONDE MURIO
            pathFollowing = gameObject.AddComponent<PFLWYG>();
            Agent.SteeringBehaviour = pathFollowing;
        } 


        float distance = (Agent.DeadPosition - Agent.Position).magnitude;
        if (distance < 3f)
        {
            Agent.Respawned = true;
        }

    }
}
