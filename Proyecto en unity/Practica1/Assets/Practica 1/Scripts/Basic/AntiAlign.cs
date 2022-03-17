using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAlign : SteeringBehaviour
{
    public override Steering getSteering(AgentNPC agent)
    {
        float rotationSize;
        float timeToTarget = 0.1f;
        float rotation = (Target.Orientation+180) - agent.Orientation;
        rotation = MapToRange(rotation);
        rotationSize = Mathf.Abs(rotation);
        if (rotationSize < Target.InteriorAngle)
        {
            if (agent.Rotation == 0)
            {
                Steering.Linear = Vector3.zero;
                Steering.Angular = 0;
                return Steering;
            }
            Steering.Linear = Vector3.zero;
            Steering.Angular = -agent.Rotation;
            return Steering;
        }
        float TargetRotation = 0;
        if (rotationSize > Target.ExteriorAngle)
        {
            TargetRotation = agent.MaxRotation;
        }
        else
        {
            TargetRotation = agent.MaxRotation * rotationSize / Target.ExteriorAngle;
        }
        if (rotationSize > 0)
        {
            TargetRotation *= rotation / rotationSize;
        }

        Steering.Angular = TargetRotation - agent.Rotation;
        Steering.Angular /= timeToTarget;
        float angularAceleration = Mathf.Abs(Steering.Angular);
        if (angularAceleration > agent.MaxAngular)
        {
            Steering.Angular /= angularAceleration;
            Steering.Angular *= agent.MaxAngular;
        }
        return Steering;
    }

}
