using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWhereYouGoing : Align
{
    public override Steering getSteering(AgentNPC agent)
    {
        if (agent.Velocity.magnitude == 0)
        {
            Steering.Linear = Vector3.zero;
            Steering.Angular = 0;
            return Steering;
        }
        float futureOrientation = Mathf.Atan2(agent.Velocity.x, agent.Velocity.z) * Mathf.Rad2Deg;
        return base.getSteering(agent, futureOrientation);
    }
}
