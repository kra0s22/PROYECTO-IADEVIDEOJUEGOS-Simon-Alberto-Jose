using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAway : State
{
    private PFLWYG pathFollowing;
    private int wayPoint;

    public int WayPoint { get => wayPoint; set => wayPoint = value; }

    public override void Start()
    {
        base.Start();
        wayPoint = 0;
    }

    public void Enter()
    {
        float distanciaMinima = Mathf.Infinity;
        int wp = 0;
        if (Agent.transform.CompareTag("Equipo Azul"))
        {
            foreach (Vector3 pos in Agent.StateMachine.Manager.WayPointAzulCura)
            {
                if (((Agent.Position - pos).magnitude) < distanciaMinima)
                {
                    WayPoint = wp;
                    distanciaMinima = (Agent.Position - pos).magnitude;
                }
                wp++;
            }
        }
        if (Agent.transform.CompareTag("Equipo Rojo"))
        {
            foreach (Vector3 pos in Agent.StateMachine.Manager.WayPointRojoCura)
            {
                if (((Agent.Position - pos).magnitude) < distanciaMinima)
                {
                    WayPoint = wp;
                    distanciaMinima = (Agent.Position - pos).magnitude;
                }
                wp++;
            }
        }
    }
    public override void Execute()
    {
        if (Agent.Path == null)
        {
            Agent.Path = gameObject.AddComponent<Path>();
            Agent.PathFinding = gameObject.AddComponent<PathFinding>();
            Agent.PathFinding.Grid = Agent.StateMachine.InfluenceMap.Grid;
            if (Agent.transform.CompareTag("Equipo Azul"))
            {
                Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position, Agent.StateMachine.Manager.WayPointAzulCura[WayPoint]);
            }
            else if (Agent.transform.CompareTag("Equipo Rojo"))
            {
                Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position, Agent.StateMachine.Manager.WayPointRojoCura[WayPoint]);
            }
            pathFollowing = gameObject.AddComponent<PFLWYG>();
            Agent.SteeringBehaviour = pathFollowing;
        }
        if (Agent.Path.Positions.Count > 0)
        {
            if (Agent.transform.CompareTag("Equipo Azul") || Agent.transform.CompareTag("Patrullero Equipo Azul"))
            {
                if ((Agent.StateMachine.Manager.WayPointAzulCura[WayPoint] - Agent.Position).magnitude < 1f)
                {
                    Agent.SaludActual += 5;
                }
            }
            else if (Agent.transform.CompareTag("Equipo Rojo") || Agent.transform.CompareTag("Patrullero Equipo Rojo"))
            {
                if ((Agent.StateMachine.Manager.WayPointRojoCura[WayPoint] - Agent.Position).magnitude < 1f)
                {
                    Agent.SaludActual += 5;
                }
            }
        }

    }
}
