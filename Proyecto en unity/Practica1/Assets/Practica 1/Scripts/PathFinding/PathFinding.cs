using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PathFinding : MonoBehaviour
{
    private Grid grid;
    AgentNPC agent;
    Node nodeactual; //Debug
    Vector3 posicionfinalDebug; //DEBUG

    public Grid Grid { get => grid; set => grid = value; }
    public Vector3 PosicionfinalDebug { get => posicionfinalDebug; set => posicionfinalDebug = value; }

    private void Awake()
    {
        Grid = gameObject.AddComponent<Grid>();
        agent = gameObject.GetComponent<AgentNPC>();
    }
    public float DistanciaManhattan(Vector3 start, Vector3 finish)
    {
        return Mathf.Abs(start.x - finish.x) + Mathf.Abs(start.z - finish.z);
    }

    public float DistanciaChebychev(Node start, Node finish)
    {
        return Mathf.Max((start.Posreal.x - finish.Posreal.x), Mathf.Abs(start.Posreal.z - finish.Posreal.z));
    }

    public float DistanciaEuclidea(Node start, Node finish)
    {
        return Mathf.Sqrt(Mathf.Pow((start.Posreal.x - finish.Posreal.x), 2) - Mathf.Pow((start.Posreal.z - finish.Posreal.z), 2));
    }

    public void InitializeGrid(Vector3 posFinal)
    {
        for (int i = 0; i < Grid.Nodes; i++)
        {
            for (int j = 0; j < Grid.Nodes; j++)
            {
                if (Grid.Map[i, j].Bosque)
                {
                    Grid.Map[i, j].Cost = DistanciaManhattan(Grid.Map[i, j].Posreal, posFinal) * agent.ResistenciaBosque;
                }
                else if (Grid.Map[i, j].Tierra)
                {
                    Grid.Map[i, j].Cost = DistanciaManhattan(Grid.Map[i, j].Posreal, posFinal) * agent.ResistenciaTierra;
                }
                else
                {
                    Grid.Map[i, j].Cost = DistanciaManhattan(Grid.Map[i, j].Posreal, posFinal);
                }
            }
        }
    }

    public Path LRTA(Vector3 posInicial, Vector3 posFinal)
    {
        PosicionfinalDebug = posFinal;
        agent.Path.Positions = new List<Vector3>();
        Vector3 posplano = Grid.RealToPlane(posInicial);
        float cost = Mathf.Infinity;
        Node nodemin = null;
        Node nodeDebug = null;
        List<Node> list;
        int costeMaximo = 1000;
        Vector3 posFinalPlano = Grid.RealToPlane(posFinal);
        if (Grid.Map[(int)posFinalPlano.x, (int)posFinalPlano.z].Obstacle)
        {
            agent.Path.Positions = new List<Vector3>();
            agent.Path.Positions.Add(agent.Position);
            return agent.Path;
        }
        if(agent.transform.CompareTag("Equipo Rojo"))
        {
            if (Grid.Map[(int)posFinalPlano.x, (int)posFinalPlano.z].InfluenciaAzul > Grid.Map[(int)posFinalPlano.x, (int)posFinalPlano.z].InfluenciaRojo)
            {
                agent.Path.Positions = new List<Vector3>();
                agent.Path.Positions.Add(agent.Position);
                return agent.Path;
            }
        }
        if (agent.transform.CompareTag("Equipo Azul"))
        {
            if (Grid.Map[(int)posFinalPlano.x, (int)posFinalPlano.z].InfluenciaRojo > Grid.Map[(int)posFinalPlano.x, (int)posFinalPlano.z].InfluenciaAzul)
            {
                agent.Path.Positions = new List<Vector3>();
                agent.Path.Positions.Add(agent.Position);
                return agent.Path;
            }
        }

        InitializeGrid(Grid.PlaneToReal(Grid.RealToPlane(posFinal)));
        while (posplano != posFinalPlano)
        {
            list = Grid.getNodesAround(posplano);
            foreach (Node n in list)
            {
                float difInfluence = 0;
                if (agent.transform.CompareTag("Equipo Azul") || agent.transform.CompareTag("Patrullero Equipo Azul"))
                {
                    difInfluence = n.InfluenciaAzul - n.InfluenciaRojo;
                }
                else if (agent.transform.CompareTag("Equipo Rojo") || agent.transform.CompareTag("Patrullero Equipo Rojo"))
                {
                    difInfluence = n.InfluenciaRojo - n.InfluenciaAzul;
                }
                if (difInfluence >= 0)
                {
                    if (n.Cost < cost)
                    {
                        cost = n.Cost;
                        if (cost >= costeMaximo)
                        {
                            agent.Path.Positions = new List<Vector3>();
                            agent.Path.Positions.Add(agent.Position);
                            agent.InfluenceBlock = true;
                            return agent.Path;
                        }
                        nodemin = n;                        
                    }
                }
            }

            Grid.Map[(int)posplano.x, (int)posplano.z].Cost = (int)cost + 1;
            if (nodemin == null)
            {
                if (agent.Path.Positions.Count == 0)
                {
                    agent.Path.Positions = new List<Vector3>();
                    agent.Path.Positions.Add(agent.Position);
                }
                return agent.Path;
            }
            
            
            posplano = nodemin.Posplano;
            nodeDebug = nodemin;
            agent.Path.Positions.Add(Grid.PlaneToReal(posplano));
            cost = Mathf.Infinity;
            nodemin = null;
        }
        if (agent.Path.Positions.Count == 0)
        {
            agent.Path.Positions = new List<Vector3>();
            agent.Path.Positions.Add(agent.Position);
        }
        return agent.Path;
    }

    public Path LRTASinInfluencia(Vector3 posInicial, Vector3 posFinal)
    {
        PosicionfinalDebug = posFinal;
        agent.Path.Positions = new List<Vector3>();
        Vector3 posplano = Grid.RealToPlane(posInicial);
        float cost = Mathf.Infinity;
        Node nodemin = null;
        List<Node> list;
        int costeMaximo = 800;
        Vector3 posFinalPlano = Grid.RealToPlane(posFinal);
        InitializeGrid(posFinal);
        while (posplano != posFinalPlano)
        {
            list = Grid.getNodesAround(posplano);
            foreach (Node n in list)
            {
                if (n.Cost < cost)
                {
                    cost = n.Cost;
                    if (cost >= costeMaximo)
                    {
                        agent.Path.Positions = new List<Vector3>();
                        agent.Path.Positions.Add(agent.Position);
                        return agent.Path;
                    }
                    nodemin = n;
                }
            }

            Grid.Map[(int)posplano.x, (int)posplano.z].Cost = (int)cost + 1;
            if (nodemin == null)
            {
                if (agent.Path.Positions.Count == 0)
                {
                    agent.Path.Positions = new List<Vector3>();
                    agent.Path.Positions.Add(agent.Position);
                }
                return agent.Path;
            }
            posplano = nodemin.Posplano;        
            agent.Path.Positions.Add(Grid.PlaneToReal(posplano));
            cost = Mathf.Infinity;
            nodemin = null;
        }
        if (agent.Path.Positions.Count == 0)
        {
            agent.Path.Positions = new List<Vector3>();
            agent.Path.Positions.Add(agent.Position);
        }
        return agent.Path;
    }

    public Path LRTAMin(Vector3 posInicial, Vector3 posFinal)
    {
        agent.Path = LRTA(posInicial, posFinal);
        List<Vector3> pathNuevo = new List<Vector3>();
        int ultima = 0;
        for (int i = 0; i < agent.Path.Positions.Count;)
        {
            pathNuevo.Add(agent.Path.Positions[i]);
            for (int j = i + 1; j < agent.Path.Positions.Count; j++)
            {
                if (agent.Path.Positions[j] == agent.Path.Positions[i])
                {
                    ultima = j;
                }
            }
            ultima = ultima + 1;
            i = ultima;
        }
        agent.Path.Positions = pathNuevo;

        return agent.Path;
    }

    public Path LRTAMinSinInfluencia(Vector3 posInicial, Vector3 posFinal)
    {
        agent.Path = LRTASinInfluencia(posInicial, posFinal);
        List<Vector3> pathNuevo = new List<Vector3>();
        int ultima = 0;
        for (int i = 0; i < agent.Path.Positions.Count;)
        {
            pathNuevo.Add(agent.Path.Positions[i]);
            for (int j = i + 1; j < agent.Path.Positions.Count; j++)
            {
                if (agent.Path.Positions[j] == agent.Path.Positions[i])
                {
                    ultima = j;
                }
            }
            ultima = ultima + 1;
            i = ultima;
        }
        agent.Path.Positions = pathNuevo;
        return agent.Path;
    }

}
