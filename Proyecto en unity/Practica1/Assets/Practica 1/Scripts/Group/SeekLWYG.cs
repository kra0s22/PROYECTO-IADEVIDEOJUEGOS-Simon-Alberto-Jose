using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekLWYG : SteeringBehaviour
{
    //private Seek seek;
    private Arrive arrive;
    private LookWhereYouGoing lookWhereYouGoing;

    public override void Start()
    {
        base.Start();
        //seek = gameObject.AddComponent<Seek>();
        arrive = gameObject.AddComponent<Arrive>();
        lookWhereYouGoing = gameObject.AddComponent<LookWhereYouGoing>();
    }

    public override Steering getSteering(AgentNPC agent)
    {
        //seek.Target = Target;
        arrive.Target = Target;
        arrive.Target.ExteriorRadius = 3;
        arrive.Target.InteriorRadius = 1;
        //Steering = seek.getSteering(agent);
        Steering = arrive.getSteering(agent);
        
        Steering.Angular = lookWhereYouGoing.getSteering(agent).Angular;
        return Steering;
    }

    private void OnDestroy()
    {
        Destroy(arrive);
        Destroy(lookWhereYouGoing);
    }
}
