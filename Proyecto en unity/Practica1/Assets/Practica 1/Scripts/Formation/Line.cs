using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : Formation
{

    public override void Start()
    {
        base.Start();
        if(FormationPosition == 1)
        {
            Slot = new Slot(0.001f, new Vector3(-1, 0, 0));  // PARA LINEARFORMATION NO FUNCIONA ORIENTACION 0 (SALE NaN) ASI QUE HAY QUE PONER 0.00001F (ALGO DESPRECIABLE)
        }
        if (FormationPosition == 2)
        {
            Slot = new Slot(0.001f, new Vector3(1, 0, 0));
        }
        if (FormationPosition == 3)
        {
            Slot = new Slot(0.001f, new Vector3(2, 0, 0));
        }
    }
    
}
