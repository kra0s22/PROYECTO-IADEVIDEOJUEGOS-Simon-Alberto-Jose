using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentNPC : Agent
{
    public SteeringBehaviour str;
    public Steering kinetic;

    public void Start()
    {
        str = new Seek();
        position = transform.position;
        kinetic = new Steering();
    }
    public void Update()
    {
        kinetic = str.getSteering(this);
        applySteering(kinetic);
    }

    public void applySteering(Steering kinetic)
    {
        position += kinetic.velocity * Time.deltaTime;
        transform.position = position;
    }
}
