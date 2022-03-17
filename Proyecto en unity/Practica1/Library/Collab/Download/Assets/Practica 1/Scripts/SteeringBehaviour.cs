using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class SteeringBehaviour : MonoBehaviour
{
    [SerializeField]
    protected Agent target;
    protected Agent Target { get => target; set => target = value; }
    protected float weight;
    protected int priority;
    protected float Weight { get => weight; set => weight = value; }
    protected int Priority { get => priority; set => priority = value; }
    protected Steering Steering { get => steering; set => steering = value; }

    protected Steering steering;

    public void Start ()
    {
        Steering = gameObject.AddComponent<Steering>();
    }

    public abstract Steering getSteering(AgentNPC agent);

}
