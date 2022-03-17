using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Todoterreno : AgentNPC
{
    public override void Start()
    {
        base.Start();
        MaxVelocity = 3;
        MaxAcceleration = 3;
        MaxVelocityInicial = MaxVelocity;
        ResistenciaTierra = 1;
        ResistenciaBosque = 1;
        Rango = 2;
        FuerzaVsRanged = 0.75f;
        FuerzaVsMele = 1.25f;
        FuerzaVsTodoTerreno = 1;
        FuerzaBosqueAtacante = 2;
        FuerzaBosqueDefensor = 2;
        FuerzaTierraAtacante = 1;
        FuerzaTierraDefensor = 0.5f;
    }
}
