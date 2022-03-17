using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : Align
{
    public override Steering getSteering(AgentNPC agent)
    {
        
        Vector3 direction = Target.Position - agent.Position;
        if (direction.magnitude == 0)
            return new Steering();
        float futureOrientation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        return getSteering(agent, futureOrientation);
    }

    public Steering getSteering(AgentNPC agent, Vector3 tar)
    {
        Vector3 direction = tar - agent.Position;
        float futureOrientation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        return getSteering(agent, futureOrientation);
    }

}
