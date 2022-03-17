using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class SteeringBehaviour : MonoBehaviour
{
    [SerializeField]
    private Agent target;
    private Path path;
    private float weight;
    private Steering steering;

    public Agent Target { get => target; set => target = value; }
    public Steering Steering { get => steering; set => steering = value; }
    public float Weight { get => weight; set => weight = value; }

    public virtual void Start ()
    {
        steering = gameObject.AddComponent<Steering>();
    }

    public abstract Steering getSteering(AgentNPC agent);

    public float MapToRange(float rotation)
    {
        if (rotation > 180)
        {
            return MapToRange(rotation - 360);
        }
        if (rotation < -180)
        {
            return MapToRange(rotation + 360);
        }
        return rotation;
    }

    private void OnDestroy()
    {
        Destroy(steering);
        Destroy(Target);
    }
}
 