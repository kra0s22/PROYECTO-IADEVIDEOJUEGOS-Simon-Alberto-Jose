using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WallAvoidance : Arrive
{

    // USA SOLO 1 RAYO ESTE WALL AVOIDANCE, ESTO TIENE EL PROBLEMA DE QUE SE COME PAREDES LATERALMENTE, POSIBLE MEJORA ES AÑADIR RAYOS LATERALES
    private float avoidDistance = 30;
    private float lookAhead = 10;
    private Collision collision;
    private Vector3 rayVector;
    private CollisionDetector collisionDetector;
    private Agent targetFicticio;

    private Vector3 posicion; //DEBUG

    public Collision Collision { get => collision; set => collision = value; }
    public Agent TargetFicticio { get => targetFicticio; set => targetFicticio = value; }

    public override void Start()
    {
        base.Start();
        collisionDetector = gameObject.AddComponent<CollisionDetector>();
        Collision = gameObject.AddComponent<Collision>();
        TargetFicticio = gameObject.AddComponent<Agent>();
    }

    public override Steering getSteering(AgentNPC agent)
    {
        posicion = agent.Position; //DEBUG
        rayVector =  agent.Velocity;
        rayVector.Normalize();
        rayVector *= lookAhead;
        Collision = collisionDetector.getCollision(agent.Position, rayVector);
        if (Collision == null)
        {
            Steering.Linear = Vector3.zero;
            Steering.Angular = 0;
            return Steering;
        }
        TargetFicticio.Position = Collision.Position + Collision.Normal * avoidDistance;
        base.Target = TargetFicticio;
        return base.getSteering(agent);
    }

}
