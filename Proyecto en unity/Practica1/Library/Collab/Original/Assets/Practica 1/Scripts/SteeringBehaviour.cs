using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviour : MonoBehaviour
{
    public Agent target;

    public virtual Steering getSteering(AgentNPC a)
    {
        return new Steering();
    }

}
