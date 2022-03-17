using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WallAvoidance : Seek
{
    private float avoidDistance = 5;
    private float lookAhead = 2;
    private Collision collision;
    private Vector3 rayVector;
    private CollisionDetector collisionDetector;
    private Agent agente;
    private Vector3 rayGizmo;
    public void Start()
    {
        base.Start();
        collisionDetector = gameObject.AddComponent<CollisionDetector>();
        collision = gameObject.AddComponent<Collision>();
    }
    public override Steering getSteering(AgentNPC agent)
    {
        agente = agent;
        rayVector =  agent.Velocity;
        Debug.Log(agent.Velocity);
        rayGizmo = rayVector;
        rayVector.Normalize();
        rayVector *= lookAhead;
        collision = collisionDetector.getCollision(agent.Position, rayVector);
        if (collision == null)
        {
            Steering.Linear = Vector3.zero;
            Steering.Angular = 0;
            return Steering;
        }
        Vector3 newTarget = collision.Position + collision.Normal * avoidDistance;
        return base.getSteering(agent, newTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(agente.Position, rayVector);
    }
}
