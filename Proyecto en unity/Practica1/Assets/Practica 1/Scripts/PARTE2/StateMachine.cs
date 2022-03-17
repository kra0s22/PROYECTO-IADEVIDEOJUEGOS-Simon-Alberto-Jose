using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    private State currentState;
    private AgentNPC agent;
    private GoToWayPoint goToWayPoint;
    private RunAway runAway;
    private Attack attack;
    private GoToRespawn goToRespawn;

    private Manager manager;
    private InfluenceMap influenceMap;


    public AgentNPC Agent { get => agent; set => agent = value; }
    public GoToWayPoint GoToWayPoint { get => goToWayPoint; set => goToWayPoint = value; }
    public RunAway RunAway { get => runAway; set => runAway = value; }
    public Attack Attack { get => attack; set => attack = value; }
    public State CurrentState { get => currentState; set => currentState = value; }
    public GoToRespawn GoToRespawn { get => goToRespawn; set => goToRespawn = value; }
    public InfluenceMap InfluenceMap { get => influenceMap; set => influenceMap = value; }
    public Manager Manager { get => manager; set => manager = value; }

    void Awake()
    {
        Agent = gameObject.GetComponent<AgentNPC>();
        GoToWayPoint = gameObject.AddComponent<GoToWayPoint>();
        RunAway = gameObject.AddComponent<RunAway>();
        Attack = gameObject.AddComponent<Attack>();
        GoToRespawn = gameObject.AddComponent<GoToRespawn>();
        CurrentState = GoToWayPoint;
        Manager = GameObject.FindObjectOfType<Manager>();
        InfluenceMap = Manager.GetComponent<InfluenceMap>();
    }

    public void ExitState(State newState)
    {        
        Destroy(Agent.Path);
        Agent.Path = null;
        Destroy(Agent.PathFinding);
        Agent.PathFinding = null;
        Destroy(Agent.SteeringBehaviour);
        Agent.SteeringBehaviour = null;
        
    }
    public void ChangeState(State newState)
    {
        ExitState(newState);
        CurrentState = newState;
    }

    // INCLUIR PATRULLERO, INCLUIR MODO OFENSIVO/DEFENSIVO
    private void LateUpdate()
    {
        if (currentState == GoToRespawn)
        {
            if (Agent.Respawned)
            {
                GoToWayPoint.Enter();
                ChangeState(GoToWayPoint);
            }
            else
            {
                if (Agent.SaludActual > 0.2 * Agent.SaludMaxima)
                {
                    if (Agent.enemigoCerca())
                    {
                        ChangeState(Attack);
                    }
                }
                else
                {
                    RunAway.Enter();
                    ChangeState(RunAway);
                }
            }
        }

        else if (CurrentState == GoToWayPoint)
        {
            if (Agent.SaludActual > 0.2 * Agent.SaludMaxima)
            {
                if (Agent.enemigoCerca())
                {
                    ChangeState(Attack);
                }
            }
            else
            {
                RunAway.Enter();
                ChangeState(RunAway);
            }
        }
        else if (CurrentState == RunAway)
        {
            if (Agent.SaludActual > 0.8 * Agent.SaludMaxima)
            {
                if (Agent.enemigoCerca())
                {
                    ChangeState(Attack);
                }
                else
                {
                    if (!Agent.Respawned)
                    {
                        ChangeState(GoToRespawn);
                    }
                    else
                    {
                        ChangeState(GoToWayPoint);
                    }

                }
            }
        }
        else if (CurrentState == Attack)
        {
            if (Agent.SaludActual > 0.2 * Agent.SaludMaxima)
            {
                if (!Agent.enemigoCerca())
                {
                    if (!Agent.Respawned)
                    {
                        ChangeState(GoToRespawn);
                    }
                    else
                    {
                        GoToWayPoint.Enter();
                        ChangeState(GoToWayPoint);
                    }
                }
            }
            else
            {
                RunAway.Enter();
                ChangeState(RunAway);
            }
        }
        CurrentState.Execute();
    }
}
