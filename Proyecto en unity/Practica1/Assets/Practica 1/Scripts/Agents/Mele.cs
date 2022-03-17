using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mele : AgentNPC
{
    public override void Start()
    {
        base.Start();
        MaxVelocity = 3;
        MaxAcceleration = 3;
        MaxVelocityInicial = MaxVelocity;
        ResistenciaTierra = 10;
        ResistenciaBosque = 0.5f;
        Rango = 2;
        FuerzaVsRanged = 1.25f;
        FuerzaVsMele = 1;
        FuerzaVsTodoTerreno = 0.75f;
        FuerzaBosqueAtacante = 0.25f;
        FuerzaBosqueDefensor = 0.5f;
        FuerzaTierraAtacante = 1;
        FuerzaTierraDefensor = 1;
    }
}
