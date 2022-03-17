using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Formation
{

    public override void Start()
    {
        base.Start();
        if (FormationPosition == 1)
        {
            Slot = new Slot(-90, new Vector3(-1, 0, -1)); 
        }
        if (FormationPosition == 2)
        {
            Slot = new Slot(90, new Vector3(1, 0, -1));
        }
        if (FormationPosition == 3)
        {
            Slot = new Slot(180, new Vector3(0, 0, -2));
        }
    }
}
