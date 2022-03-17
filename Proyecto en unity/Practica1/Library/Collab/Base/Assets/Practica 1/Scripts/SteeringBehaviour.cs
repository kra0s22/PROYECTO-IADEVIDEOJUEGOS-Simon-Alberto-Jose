using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SteeringBehaviour : MonoBehaviour
{
    [SerializeField]
    protected Agent target;
    protected Agent Target { get => target; set => target = value; }
    protected float weight;
    protected int priority;
    protected float Weight { get => weight; set => weight = value; }
    protected int Priority { get => priority; set => priority = value; }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual Steering getSteering(AgentNPC agent)
    {
        return new Steering();
    }

   

}
