using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WallAvoidance : Seek
{
    private float avoidDistance = 5;
    private float lookAhead = 2;
    private Collision collision;
    private Vector3 rayVector;
   

    private void Start()
    {
        base.Start();
        CollisionDetector = gameObject.AddComponent<CollisionDetector>();
        collision = gameObject.AddComponent<Collision>();
    }
    public override Steering getSteering(AgentNPC agent)
    {
        rayVector =  agent.Velocity;
        rayVector.Normalize();
        rayVector *= lookAhead;
        collision = CollisionDetector.getCollision(agent.Position, rayVector);
        if (collision == null)
        {
            Steering.Linear = Vector3.zero;
            Steering.Angular = 0;
            return Steering;
        }
        Vector3 newTarget = collision.Position + collision.Normal * avoidDistance;
        return base.getSteering(agent, newTarget);
    }


}
