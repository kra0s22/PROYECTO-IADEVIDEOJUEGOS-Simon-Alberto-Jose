using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToWayPoint : State
{
    private PFLWYG pathFollowing;
    private float timer;
    private bool esperarAgrupar;
    private bool esperarOfensivaTotal;
    Vector3[] wayPointActual;


    public override void Start()
    {
        base.Start();
    }

    public void Enter()
    {
        float distanciaMinima = Mathf.Infinity;
        int wayPoint = 0;
        if (Agent.transform.CompareTag("Patrullero Equipo Azul"))
        {
            foreach (Vector3 pos in Agent.StateMachine.Manager.WayPointAzulPatrullero)
            {
                if (((Agent.Position - pos).magnitude) < distanciaMinima)
                {
                    Agent.WayPoint = wayPoint;
                    distanciaMinima = (Agent.Position - pos).magnitude;
                }
                wayPoint++;
            }
        }
        else if (Agent.transform.CompareTag("Patrullero Equipo Rojo"))
        {
            foreach (Vector3 pos in Agent.StateMachine.Manager.WayPointRojoPatrullero)
            {
                if (((Agent.Position - pos).magnitude) < distanciaMinima)
                {
                    Agent.WayPoint = wayPoint;
                    distanciaMinima = (Agent.Position - pos).magnitude;
                }
                wayPoint++;
            }
        }
        if (Agent.transform.CompareTag("Equipo Azul"))
        {

            if (!Agent.Ofensivo)
            {
                foreach (Vector3 pos in Agent.StateMachine.Manager.WayPointAzulDefensivo)
                {
                    if (((Agent.Position - pos).magnitude) < distanciaMinima)
                    {
                        Agent.WayPoint = wayPoint;
                        distanciaMinima = (Agent.Position - pos).magnitude;
                    }
                    wayPoint++;
                }
            }

        }
        if (Agent.transform.CompareTag("Equipo Rojo"))
        {
            if (!Agent.Ofensivo)
            {
                foreach (Vector3 pos in Agent.StateMachine.Manager.WayPointRojoDefensivo)
                {
                    if (((Agent.Position - pos).magnitude) < distanciaMinima)
                    {
                        Agent.WayPoint = wayPoint;
                        distanciaMinima = (Agent.Position - pos).magnitude;
                    }
                    wayPoint++;
                }
            }
        }
    }

    public override void Execute()
    {
        switch (Agent.StateMachine.Manager.Tactica)
        {
            case Tactica.Estandar:
                TacticaEstandar();
                break;
            case Tactica.Agrupar:
                TacticaAgrupar();
                break;
            case Tactica.OfensivaTotal:
                TacticaOfensivaTotal();
                break;
        }
    }

    public void TacticaEstandar()
    {
        if (!Agent.InfluenceBlock)
        {
            if (Agent.Path == null)
            {
                Agent.Path = gameObject.AddComponent<Path>();
                Agent.PathFinding = gameObject.AddComponent<PathFinding>();
                Agent.PathFinding.Grid = Agent.StateMachine.InfluenceMap.Grid;

                if (Agent.transform.CompareTag("Patrullero Equipo Azul"))
                {
                    Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointAzulPatrullero[Agent.WayPoint]);
                    Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointAzulPatrullero.Length;
                }
                else if (Agent.transform.CompareTag("Patrullero Equipo Rojo"))
                {
                    Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointRojoPatrullero[Agent.WayPoint]);
                    Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointRojoPatrullero.Length;
                }
                if (Agent.Ofensivo && (Agent.transform.CompareTag("Equipo Rojo") || Agent.transform.CompareTag("Equipo Azul")))
                {
                    if (Agent.transform.CompareTag("Equipo Rojo"))
                    {
                        Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointRojoOfensivo[Agent.WayPoint]); // POSICION WAYPOINT X
                    }
                    else if (Agent.transform.CompareTag("Equipo Azul"))
                    {
                        Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointAzulOfensivo[Agent.WayPoint]);
                    }
                }
                else if (Agent.Ofensivo == false && (Agent.transform.CompareTag("Equipo Rojo") || Agent.transform.CompareTag("Equipo Azul")))
                {
                    if (Agent.transform.CompareTag("Equipo Rojo"))
                    {

                        if (Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] == null)
                        {
                            Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointRojoDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                        else
                        {
                            
                            while (Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] != null &&
                                Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint].name != Agent.name)
                            {
                                Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointRojoDefensivo.Length;
                            }
                            Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position,
                                Agent.StateMachine.Manager.WayPointRojoDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                    }
                    else if (Agent.transform.CompareTag("Equipo Azul"))
                    {
                        if (Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] == null)
                        {
                            Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointAzulDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                        else
                        {
                            while (Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] != null &&
                                Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint].name != Agent.name)
                            {
                                Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointAzulDefensivo.Length;
                            }
                            Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position,
                                Agent.StateMachine.Manager.WayPointAzulDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                    }
                }

                pathFollowing = gameObject.AddComponent<PFLWYG>();
                Agent.SteeringBehaviour = pathFollowing;
            }
            else if (Agent.transform.CompareTag("Equipo Azul") && (Agent.Position - Agent.Path.Positions[Agent.Path.Positions.Count - 1]).magnitude < 1f)
            {
                if (Agent.Ofensivo)
                {
                    if (Agent.WayPoint < Agent.StateMachine.Manager.WayPointAzulOfensivo.Length - 1)
                    {
                        Agent.WayPoint++;
                        Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointAzulOfensivo[Agent.WayPoint]);
                        pathFollowing.PathFollowing.Reset();
                    }
                    else if ((Agent.Path.Positions.Count == 1 && (Agent.Position - Agent.StateMachine.Manager.WayPointAzulOfensivo[Agent.WayPoint]).magnitude > 1f))
                    {
                        Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointAzulOfensivo[Agent.WayPoint]);
                        pathFollowing.PathFollowing.Reset();
                    }
                }
                else
                {
                    if (Agent.WayPoint < Agent.StateMachine.Manager.WayPointAzulDefensivo.Length - 1)
                    {                        
                        Agent.WayPoint++;
                        if (Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] == null)
                        {
                            Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointAzulDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                        else
                        {
                            while (Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] != null &&
                                Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint].name != Agent.name)
                            {
                                Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointAzulDefensivo.Length;
                            }
                            Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position,
                                Agent.StateMachine.Manager.WayPointAzulDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                        pathFollowing.PathFollowing.Reset();
                    }
                }
            }
            else if (Agent.transform.CompareTag("Equipo Rojo") && (Agent.Position - Agent.Path.Positions[Agent.Path.Positions.Count - 1]).magnitude < 1f)
            {
                if (Agent.Ofensivo)
                {
                    if (Agent.WayPoint < Agent.StateMachine.Manager.WayPointRojoOfensivo.Length - 1)
                    {
                        Agent.WayPoint++;
                        Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointRojoOfensivo[Agent.WayPoint]);
                        pathFollowing.PathFollowing.Reset();
                    }
                    else if (Agent.Path.Positions.Count == 1 && (Agent.Position - Agent.StateMachine.Manager.WayPointRojoOfensivo[Agent.WayPoint]).magnitude > 1f)
                    {
                        Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointRojoOfensivo[Agent.WayPoint]);
                        pathFollowing.PathFollowing.Reset();
                    }
                }
                else
                {
                    if (Agent.WayPoint < Agent.StateMachine.Manager.WayPointRojoDefensivo.Length - 1)
                    {
                        Agent.WayPoint++;
                        if (Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] == null)
                        {
                            Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointRojoDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                        else
                        {

                            while (Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] != null &&
                                Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint].name != Agent.name)
                            {
                                Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointRojoDefensivo.Length;
                            }
                            Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position,
                                Agent.StateMachine.Manager.WayPointRojoDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                        pathFollowing.PathFollowing.Reset();
                    }
                }
            }
            else if (Agent.transform.CompareTag("Patrullero Equipo Azul") && (Agent.Position - Agent.Path.Positions[Agent.Path.Positions.Count - 1]).magnitude < 1f)
            {
                Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointAzulPatrullero[Agent.WayPoint]);
                Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointAzulPatrullero.Length;
                pathFollowing.PathFollowing.Reset();
            }
            else if (Agent.transform.CompareTag("Patrullero Equipo Rojo") && (Agent.Position - Agent.Path.Positions[Agent.Path.Positions.Count - 1]).magnitude < 1f)
            {
                Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointRojoPatrullero[Agent.WayPoint]);
                Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointRojoPatrullero.Length;
                pathFollowing.PathFollowing.Reset();
            }
        }
        else
        {
            if (Agent.StateMachine.InfluenceMap.Actualizar)
            {
                Agent.InfluenceBlock = false;
            }
        }
    }

    public void TacticaAgrupar()
    {

        if (!Agent.InfluenceBlock)
        {
            if (Agent.Path == null)
            {
                Agent.Path = gameObject.AddComponent<Path>();
                Agent.PathFinding = gameObject.AddComponent<PathFinding>();
                Agent.PathFinding.Grid = Agent.StateMachine.InfluenceMap.Grid;

                if (Agent.transform.CompareTag("Patrullero Equipo Azul"))
                {
                    Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointAzulPatrullero[Agent.WayPoint]);
                    Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointAzulPatrullero.Length;
                }
                else if (Agent.transform.CompareTag("Patrullero Equipo Rojo"))
                {
                    Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointRojoPatrullero[Agent.WayPoint]);
                    Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointRojoPatrullero.Length;
                }

                if (Agent.Ofensivo && (Agent.transform.CompareTag("Equipo Rojo") || Agent.transform.CompareTag("Equipo Azul")))
                {
                    if (Agent.transform.CompareTag("Equipo Rojo"))
                    {

                        if (Agent is Mele)
                        {
                            wayPointActual = new Vector3[Agent.StateMachine.Manager.WayPointMeleAgruparRojo.Length];
                            wayPointActual = Agent.StateMachine.Manager.WayPointMeleAgruparRojo;
                        }
                        else if (Agent is Todoterreno)
                        {
                            wayPointActual = new Vector3[Agent.StateMachine.Manager.WayPointTodoTerrenoAgruparRojo.Length];
                            wayPointActual = Agent.StateMachine.Manager.WayPointTodoTerrenoAgruparRojo;
                        }
                        else
                        {
                            wayPointActual = new Vector3[Agent.StateMachine.Manager.WayPointRangedAgruparRojo.Length];
                            wayPointActual = Agent.StateMachine.Manager.WayPointRangedAgruparRojo;
                        }
                        Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, wayPointActual[Agent.WayPoint]); // POSICION WAYPOINT X
                    }
                    else if (Agent.transform.CompareTag("Equipo Azul"))
                    {
                        if (Agent is Mele)
                        {
                            wayPointActual = new Vector3[Agent.StateMachine.Manager.WayPointMeleAgruparAzul.Length];
                            wayPointActual = Agent.StateMachine.Manager.WayPointMeleAgruparAzul;
                        }
                        else if (Agent is Todoterreno)
                        {
                            wayPointActual = new Vector3[Agent.StateMachine.Manager.WayPointTodoTerrenoAgruparAzul.Length];
                            wayPointActual = Agent.StateMachine.Manager.WayPointTodoTerrenoAgruparAzul;
                        }
                        else
                        {
                            wayPointActual = new Vector3[Agent.StateMachine.Manager.WayPointRangedAgruparAzul.Length];
                            wayPointActual = Agent.StateMachine.Manager.WayPointRangedAgruparAzul;
                        }
                        Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, wayPointActual[Agent.WayPoint]); // POSICION WAYPOINT X
                    }
                }
                else if (Agent.Ofensivo == false && (Agent.transform.CompareTag("Equipo Rojo") || Agent.transform.CompareTag("Equipo Azul")))
                {
                    if (Agent.transform.CompareTag("Equipo Rojo"))
                    {

                        if (Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] == null)
                        {
                            Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointRojoDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                        else
                        {
                            while (Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] != null &&
                                Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint].name != Agent.name)
                            {
                                Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointRojoDefensivo.Length;
                            }
                            Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position,
                                Agent.StateMachine.Manager.WayPointRojoDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                    }
                    else if (Agent.transform.CompareTag("Equipo Azul"))
                    {
                        if (Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] == null)
                        {
                            Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointAzulDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                        else
                        {
                            while (Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] != null &&
                                Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint].name != Agent.name)
                            {
                                Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointAzulDefensivo.Length;
                            }
                            Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position,
                                Agent.StateMachine.Manager.WayPointAzulDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                    }
                }

                pathFollowing = gameObject.AddComponent<PFLWYG>();
                Agent.SteeringBehaviour = pathFollowing;
            }


            else if ((Agent.transform.CompareTag("Equipo Azul") || Agent.transform.CompareTag("Equipo Rojo")) && (Agent.Position - Agent.Path.Positions[Agent.Path.Positions.Count - 1]).magnitude < 1f)
            {
                if (Agent.Ofensivo)
                {
                    if (Agent.WayPoint == 0)
                    {
                        if ((Agent.Position - wayPointActual[Agent.WayPoint]).magnitude < 1f)
                        {
                            if (Agent is Mele)
                            {
                                if (!esperarAgrupar)
                                {
                                    Agent.StateMachine.Manager.NumeroMeles++;
                                }

                                if (Agent.StateMachine.Manager.NumeroMeles == 1)
                                {
                                    esperarAgrupar = true;
                                }
                                if (!esperarAgrupar && Agent.StateMachine.Manager.NumeroMeles == 2)
                                {
                                    Agent.WayPoint++;
                                    Agent.StateMachine.Manager.NumeroMeles++;
                                }
                                if (esperarAgrupar && Agent.StateMachine.Manager.NumeroMeles == 3)
                                {
                                    Agent.WayPoint++;
                                }
                            }
                            else if (Agent is Todoterreno)
                            {
                                Agent.WayPoint++;
                            }
                            else
                            {
                                if (!esperarAgrupar)
                                {
                                    Agent.StateMachine.Manager.NumeroRanged++;
                                }

                                if (Agent.StateMachine.Manager.NumeroRanged == 1)
                                {
                                    esperarAgrupar = true;
                                }
                                if (!esperarAgrupar && Agent.StateMachine.Manager.NumeroRanged == 2)
                                {
                                    Agent.WayPoint++;
                                    Agent.StateMachine.Manager.NumeroRanged++;
                                }
                                if (esperarAgrupar && Agent.StateMachine.Manager.NumeroRanged == 3)
                                {
                                    Agent.WayPoint++;
                                }
                            }
                        }

                    }
                    Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, wayPointActual[Agent.WayPoint]);
                    pathFollowing.PathFollowing.Reset();
                }
                else
                {
                    if (Agent.transform.CompareTag("Equipo Azul") && Agent.WayPoint < Agent.StateMachine.Manager.WayPointAzulDefensivo.Length - 1)
                    {                      
                        Agent.WayPoint++;
                        if (Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] == null)
                        {
                            Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointAzulDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                        else
                        {
                            while (Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] != null &&
                                Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint].name != Agent.name)
                            {
                                Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointAzulDefensivo.Length;
                            }
                            Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position,
                                Agent.StateMachine.Manager.WayPointAzulDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                        pathFollowing.PathFollowing.Reset();
                    }
                    else if (Agent.transform.CompareTag("Equipo Rojo") && Agent.WayPoint < Agent.StateMachine.Manager.WayPointRojoDefensivo.Length - 1)
                    {
                        Agent.WayPoint++;
                        if (Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] == null)
                        {
                            Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointRojoDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                        else
                        {

                            while (Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] != null &&
                                Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint].name != Agent.name)
                            {
                                Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointRojoDefensivo.Length;
                            }
                            Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position,
                                Agent.StateMachine.Manager.WayPointRojoDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                        pathFollowing.PathFollowing.Reset();
                    }
                }
            }

            else if (Agent.transform.CompareTag("Patrullero Equipo Azul") && (Agent.Position - Agent.Path.Positions[Agent.Path.Positions.Count - 1]).magnitude < 1f)
            {
                Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointAzulPatrullero[Agent.WayPoint]);
                Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointAzulPatrullero.Length;
                pathFollowing.PathFollowing.Reset();
            }
            else if (Agent.transform.CompareTag("Patrullero Equipo Rojo") && (Agent.Position - Agent.Path.Positions[Agent.Path.Positions.Count - 1]).magnitude < 1f)
            {
                Agent.Path = Agent.PathFinding.LRTAMin(Agent.Position, Agent.StateMachine.Manager.WayPointRojoPatrullero[Agent.WayPoint]);
                Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointRojoPatrullero.Length;
                pathFollowing.PathFollowing.Reset();
            }
        }
        else
        {
            if (Agent.StateMachine.InfluenceMap.Actualizar)
            {
                Agent.InfluenceBlock = false;
            }
        }
    }

    public void TacticaOfensivaTotal()
    {
        if (!Agent.InfluenceBlock)
        {
            if (Agent.Path == null)
            {
                Agent.Path = gameObject.AddComponent<Path>();
                Agent.PathFinding = gameObject.AddComponent<PathFinding>();
                Agent.PathFinding.Grid = Agent.StateMachine.InfluenceMap.Grid;

                if (Agent.transform.CompareTag("Patrullero Equipo Azul"))
                {
                    Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position, Agent.StateMachine.Manager.WayPointAzulPatrullero[Agent.WayPoint]);
                    Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointAzulPatrullero.Length;
                }
                else if (Agent.transform.CompareTag("Patrullero Equipo Rojo"))
                {
                    Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position, Agent.StateMachine.Manager.WayPointRojoPatrullero[Agent.WayPoint]);
                    Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointRojoPatrullero.Length;
                }
                if (Agent.Ofensivo && (Agent.transform.CompareTag("Equipo Rojo") || Agent.transform.CompareTag("Equipo Azul")))
                {
                    if (Agent.transform.CompareTag("Equipo Rojo"))
                    {
                        wayPointActual = Agent.StateMachine.Manager.WayPointRojoOfensivaTotal;
                        if (Agent.StateMachine.Manager.WayPointRojoOfensivaTotalOcupado[Agent.WayPoint] == null)
                        {
                            Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position, wayPointActual[Agent.WayPoint]); // POSICION WAYPOINT X
                            Agent.StateMachine.Manager.WayPointRojoOfensivaTotalOcupado[Agent.WayPoint] = Agent;
                        }
                        else
                        {
                            if (Agent.WayPoint < Agent.StateMachine.Manager.WayPointRojoOfensivaTotalOcupado.Length - 1)
                            {
                                while (Agent.StateMachine.Manager.WayPointRojoOfensivaTotalOcupado[Agent.WayPoint] != null &&
                                Agent.StateMachine.Manager.WayPointRojoOfensivaTotalOcupado[Agent.WayPoint].name != Agent.name)
                                {
                                    Agent.WayPoint = (Agent.WayPoint + 1) % (Agent.StateMachine.Manager.WayPointRojoOfensivaTotal.Length - 1);
                                }
                            } else
                            {
                                Agent.SteeringBehaviour = gameObject.AddComponent<PFLWYG>();
                            }
                            Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position, wayPointActual[Agent.WayPoint]); // POSICION WAYPOINT X
                            Agent.StateMachine.Manager.WayPointRojoOfensivaTotalOcupado[Agent.WayPoint] = Agent;
                        }
                    }
                    else if (Agent.transform.CompareTag("Equipo Azul"))
                    {
                        wayPointActual = Agent.StateMachine.Manager.WayPointAzulOfensivaTotal;
                        if (Agent.StateMachine.Manager.WayPointAzulOfensivaTotalOcupado[Agent.WayPoint] == null)
                        {
                            Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position, wayPointActual[Agent.WayPoint]); // POSICION WAYPOINT X
                            Agent.StateMachine.Manager.WayPointAzulOfensivaTotalOcupado[Agent.WayPoint] = Agent;
                        }
                        else
                        {
                            if (Agent.WayPoint < Agent.StateMachine.Manager.WayPointAzulOfensivaTotalOcupado.Length - 1)
                            {
                                while (Agent.StateMachine.Manager.WayPointAzulOfensivaTotalOcupado[Agent.WayPoint] != null &&
                                Agent.StateMachine.Manager.WayPointAzulOfensivaTotalOcupado[Agent.WayPoint].name != Agent.name)
                                {
                                    Agent.WayPoint = (Agent.WayPoint + 1) % (Agent.StateMachine.Manager.WayPointAzulOfensivaTotal.Length - 1);
                                }
                            }
                            else
                            {
                                Agent.SteeringBehaviour = gameObject.AddComponent<PFLWYG>();
                            }

                            Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position, wayPointActual[Agent.WayPoint]); // POSICION WAYPOINT X
                            Agent.StateMachine.Manager.WayPointAzulOfensivaTotalOcupado[Agent.WayPoint] = Agent;
                        }
                    }
                }
                else if (Agent.Ofensivo == false && (Agent.transform.CompareTag("Equipo Rojo") || Agent.transform.CompareTag("Equipo Azul")))
                {
                    if (Agent.transform.CompareTag("Equipo Rojo"))
                    {

                        if (Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] == null)
                        {
                            Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position, Agent.StateMachine.Manager.WayPointRojoDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                        else
                        {

                            while (Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] != null &&
                                Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint].name != Agent.name)
                            {
                                Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointRojoDefensivo.Length;
                            }
                            Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position,
                                Agent.StateMachine.Manager.WayPointRojoDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                    }
                    else if (Agent.transform.CompareTag("Equipo Azul"))
                    {
                        if (Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] == null)
                        {
                            Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position, Agent.StateMachine.Manager.WayPointAzulDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                        else
                        {
                            while (Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] != null &&
                                Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint].name != Agent.name)
                            {
                                Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointAzulDefensivo.Length;
                            }
                            Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position,
                                Agent.StateMachine.Manager.WayPointAzulDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                    }
                }

                pathFollowing = gameObject.AddComponent<PFLWYG>();
                Agent.SteeringBehaviour = pathFollowing;
            }
            else if (Agent.transform.CompareTag("Equipo Azul") && (Agent.Position - Agent.Path.Positions[Agent.Path.Positions.Count - 1]).magnitude < 1f)
            {
                if (Agent.Ofensivo)
                {

                    if (Agent.WayPoint < wayPointActual.Length - 1)
                    {

                        if (!esperarOfensivaTotal)
                        {
                            Agent.StateMachine.Manager.NumeroOfensivaTotal++;
                        }
                        if (Agent.StateMachine.Manager.NumeroOfensivaTotal < (Agent.StateMachine.Manager.EquipoAzul.Count - 1))
                        {
                            esperarOfensivaTotal = true;
                        }
                        if (!esperarOfensivaTotal && (Agent.StateMachine.Manager.NumeroOfensivaTotal >= (Agent.StateMachine.Manager.EquipoAzul.Count - 1)))
                        {
                            Agent.WayPoint = wayPointActual.Length - 1;

                            Agent.StateMachine.Manager.NumeroOfensivaTotal++;
                        }
                        if (esperarOfensivaTotal && Agent.StateMachine.Manager.NumeroOfensivaTotal == (Agent.StateMachine.Manager.EquipoAzul.Count - 1) + 1)
                        {
                            Agent.WayPoint = wayPointActual.Length - 1;
                        }
                        Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position, wayPointActual[Agent.WayPoint]);
                        pathFollowing.PathFollowing.Reset();
                    }
                    
                    else if ((Agent.Path.Positions.Count == 1 && (Agent.Position - wayPointActual[Agent.WayPoint]).magnitude > 1f))
                    {
                        Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position, wayPointActual[Agent.WayPoint]);
                        pathFollowing.PathFollowing.Reset();
                    }  
                }
                else
                {
                    if (Agent.WayPoint < Agent.StateMachine.Manager.WayPointAzulDefensivo.Length - 1)
                    {
                        Agent.WayPoint++;
                        if (Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] == null)
                        {
                            Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position, Agent.StateMachine.Manager.WayPointAzulDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                        else
                        {
                            while (Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] != null &&
                                Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint].name != Agent.name)
                            {
                                Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointAzulDefensivo.Length;
                            }
                            Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position,
                                Agent.StateMachine.Manager.WayPointAzulDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointAzulDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                        pathFollowing.PathFollowing.Reset();
                    }
                }
            }
            else if (Agent.transform.CompareTag("Equipo Rojo") && (Agent.Position - Agent.Path.Positions[Agent.Path.Positions.Count - 1]).magnitude < 1f)
            {
                if (Agent.Ofensivo)
                {
                    if (Agent.WayPoint < wayPointActual.Length - 1)
                    {

                        if (!esperarOfensivaTotal)
                        {
                            Agent.StateMachine.Manager.NumeroOfensivaTotal++;
                        }

                        if (Agent.StateMachine.Manager.NumeroOfensivaTotal < (Agent.StateMachine.Manager.EquipoRojo.Count - 1))
                        {
                            esperarOfensivaTotal = true;
                        }
                        if (!esperarOfensivaTotal && (Agent.StateMachine.Manager.NumeroOfensivaTotal >= (Agent.StateMachine.Manager.EquipoRojo.Count - 1)))
                        {
                            Agent.WayPoint = wayPointActual.Length - 1;

                            Agent.StateMachine.Manager.NumeroOfensivaTotal++;
                        }
                        if (esperarOfensivaTotal && Agent.StateMachine.Manager.NumeroOfensivaTotal == (Agent.StateMachine.Manager.EquipoRojo.Count - 1) + 1)
                        {
                            Agent.WayPoint = wayPointActual.Length - 1;
                        }
                        Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position, wayPointActual[Agent.WayPoint]);
                        pathFollowing.PathFollowing.Reset();
                    }

                    else if ((Agent.Path.Positions.Count == 1 && (Agent.Position - wayPointActual[Agent.WayPoint]).magnitude > 1f))
                    {
                        Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position, wayPointActual[Agent.WayPoint]);
                        pathFollowing.PathFollowing.Reset();
                    }
                }
                else
                {
                    if (Agent.WayPoint < Agent.StateMachine.Manager.WayPointRojoDefensivo.Length - 1)
                    {
                        Agent.WayPoint++;
                        if (Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] == null)
                        {
                            Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position, Agent.StateMachine.Manager.WayPointRojoDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                        else
                        {

                            while (Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] != null &&
                                Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint].name != Agent.name)
                            {
                                Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointRojoDefensivo.Length;
                            }
                            Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position,
                                Agent.StateMachine.Manager.WayPointRojoDefensivo[Agent.WayPoint]);
                            Agent.StateMachine.Manager.WayPointRojoDefensivoOcupado[Agent.WayPoint] = Agent;
                        }
                        pathFollowing.PathFollowing.Reset();
                    }
                }
            }

            else if (Agent.transform.CompareTag("Patrullero Equipo Azul") && (Agent.Position - Agent.Path.Positions[Agent.Path.Positions.Count - 1]).magnitude < 1f)
            {
                Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position, Agent.StateMachine.Manager.WayPointAzulPatrullero[Agent.WayPoint]);
                Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointAzulPatrullero.Length;
                pathFollowing.PathFollowing.Reset();
            }
            else if (Agent.transform.CompareTag("Patrullero Equipo Rojo") && (Agent.Position - Agent.Path.Positions[Agent.Path.Positions.Count - 1]).magnitude < 1f)
            {
                Agent.Path = Agent.PathFinding.LRTAMinSinInfluencia(Agent.Position, Agent.StateMachine.Manager.WayPointRojoPatrullero[Agent.WayPoint]);
                Agent.WayPoint = (Agent.WayPoint + 1) % Agent.StateMachine.Manager.WayPointRojoPatrullero.Length;
                pathFollowing.PathFollowing.Reset();
            }
        }
        else
        {
            if(Agent.Path != null) {
                Destroy(Agent.Path);
                Destroy(Agent.PathFinding);
                Destroy(Agent.SteeringBehaviour);
            }
            if (Agent.StateMachine.InfluenceMap.Actualizar)
            {
                Agent.InfluenceBlock = false;
            }
        }
    }

}
