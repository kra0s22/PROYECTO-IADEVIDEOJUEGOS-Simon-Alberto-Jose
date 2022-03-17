using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentNPC : Agent
{

    [SerializeField]
    protected SteeringBehaviour[] listSteerings { get; set; }
    protected float blendWeight { get; set; }
    protected float blendPriority { get; set; }
    
    // Update is called once per frame
    void Update()
    {
        Steering kinetic = new Steering();
        foreach (SteeringBehaviour str in listSteerings)
            kinetic = str.getSteering(this);
        applySteering(kinetic);
    }

    void applySteering(Steering kinetic)
    {
        position += kinetic.Linear * Time.deltaTime;
        orientation += kinetic.Angular * Time.deltaTime;
    }

    public double OrientationToVector(Steering steering)
    {
        return Math.Atan2(-steering.Linear.x, steering.Linear.y);
    }
}
