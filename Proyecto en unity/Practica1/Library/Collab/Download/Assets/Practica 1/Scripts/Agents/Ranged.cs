using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : AgentNPC
{
    
    public override void Start()
    {
        base.Start();
        MaxVelocity = 4;
        MaxAcceleration = 4;
        MaxVelocityInicial = MaxVelocity;
        ResistenciaTierra = 0.5f;
        ResistenciaBosque = 10;
        Rango = 10;
        FuerzaVsRanged = 1;
        FuerzaVsMele = 0.75f;
        FuerzaVsTodoTerreno = 1.25f;
        FuerzaBosqueAtacante = 0.1f;
        FuerzaBosqueDefensor = 1.25f;
        FuerzaTierraAtacante = 2f;
        FuerzaTierraDefensor = 0.75f;
    }
}
