using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    public float maxSpeed = 1f;
    public override Steering getSteering(AgentNPC character)
    {
        Steering steering = new Steering();
        steering.velocity = target.position - character.position;

        steering.velocity.Normalize();
        steering.velocity *= maxSpeed;

        return steering;
    }
}
