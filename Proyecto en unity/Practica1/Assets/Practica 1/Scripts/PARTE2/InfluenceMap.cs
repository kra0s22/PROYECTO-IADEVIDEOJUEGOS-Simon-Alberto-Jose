using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceMap : MonoBehaviour
{
    public const int TIEMPO_ACTUALIZACION = 2;
    private Grid grid;
    private AgentNPC[] listaPersonajes;
    private List<AgentNPC> equipoAzul;
    private List<AgentNPC> equipoRojo;
    private float timer;
    private bool actualizar;

    public Grid Grid { get => grid; set => grid = value; }
    public float Timer { get => timer; set => timer = value; }
    public bool Actualizar { get => actualizar; set => actualizar = value; }

    private void Awake()
    {
        Grid = gameObject.AddComponent<Grid>();
        listaPersonajes = AgentNPC.FindObjectsOfType<AgentNPC>();
        equipoAzul = new List<AgentNPC>();
        equipoRojo = new List<AgentNPC>();
        foreach (AgentNPC a in listaPersonajes)
        {
            if (a.transform.CompareTag("Equipo Azul") || a.transform.CompareTag("Patrullero Equipo Azul"))
            {
                equipoAzul.Add(a);
            }
            else if (a.transform.CompareTag("Equipo Rojo") || a.transform.CompareTag("Patrullero Equipo Rojo"))
            {
                equipoRojo.Add(a);
            }
        }
        Timer = 0;
    }


    // Hay dos listas de equipos. Cada 3 segundos el mapa se resetea y calcula de nuevo la influencia. La influencia se calcula en los nodos actual y adyacentes de cada personaje, 
    // y se guarda por separado la influencia total de cada equipo. En PathFinding luego se tiene en cuenta qué equipo tiene más influencia para decidir si puede o no pasar por ahi. 
    private void Update()
    {
        if (Timer <= 0)
        {
            for (int i = 0; i < Grid.Nodes; i++)
            {
                for (int j = 0; j < Grid.Nodes; j++)
                {
                    Grid.Map[i, j].InfluenciaAzul = 0;
                    Grid.Map[i, j].InfluenciaRojo = 0;
                }
            }
            foreach (AgentNPC agent in equipoAzul)
            {
                Vector3 posPlano = Grid.RealToPlane(agent.Position);
                //Sacar posicion del agent en grid
                Grid.Map[(int)posPlano.x, (int)posPlano.z].InfluenciaAzul += 50; // 50 es un valor arbitrario
                List<Node> nodosAdyacentes = Grid.getNodesAround(posPlano);
                foreach (Node n in nodosAdyacentes)
                {
                    n.InfluenciaAzul += 50 / 2; // Los nodos adyacentes tienen menos influencia que el actual
                }
            }

            foreach (AgentNPC agent in equipoRojo)
            {
                Vector3 posPlano = Grid.RealToPlane(agent.Position);
                //Sacar posicion del agent en grid
                Grid.Map[(int)posPlano.x, (int)posPlano.z].InfluenciaRojo += 1; 
                List<Node> nodosAdyacentes = Grid.getNodesAround(posPlano);
                foreach (Node n in nodosAdyacentes)
                {
                    n.InfluenciaRojo += 50 / 2;
                }
            }
            Timer = TIEMPO_ACTUALIZACION;
            Actualizar = true;
        }        
        else
        {
            Actualizar = false;
        }
        Timer -= Time.deltaTime;
    }


    private void OnDrawGizmos()
    {
        if(Input.GetKey(KeyCode.M))
        {
            for (int i = 0; i < Grid.Nodes; i++)
            {
                for (int j = 0; j < Grid.Nodes; j++)
                {
                    if(Grid.Map[i,j].InfluenciaAzul > Grid.Map[i, j].InfluenciaRojo)
                    {
                        Gizmos.color = Color.blue;
                        Vector3 newPos = new Vector3(Grid.Map[i, j].Posreal.x + 200, Grid.Map[i, j].Posreal.y, Grid.Map[i, j].Posreal.z);
                        Gizmos.DrawSphere(newPos, 1);
                    }
                    else if(Grid.Map[i, j].InfluenciaRojo > Grid.Map[i, j].InfluenciaAzul)
                    {
                        Gizmos.color = Color.red;
                        Vector3 newPos = new Vector3(Grid.Map[i, j].Posreal.x + 200, Grid.Map[i, j].Posreal.y, Grid.Map[i, j].Posreal.z);
                        Gizmos.DrawSphere(newPos, 1);
                    }

                    if (Grid.Map[i, j].Obstacle)
                    {
                        Gizmos.color = Color.cyan;
                        Vector3 newPos = new Vector3(Grid.Map[i, j].Posreal.x + 200, Grid.Map[i, j].Posreal.y, Grid.Map[i, j].Posreal.z);
                        Gizmos.DrawSphere(newPos, 1);
                    }
                    else if (Grid.Map[i, j].Bosque)
                    {
                        Gizmos.color = Color.green;
                        Vector3 newPos = new Vector3(Grid.Map[i, j].Posreal.x + 200, Grid.Map[i, j].Posreal.y, Grid.Map[i, j].Posreal.z);
                        Gizmos.DrawSphere(newPos, 1);
                    }
                    else if (Grid.Map[i, j].Tierra)
                    {
                        Gizmos.color = new Color(153f / 255f, 101f / 255f, 21f / 255f, 1); // Color BROWN
                        Vector3 newPos = new Vector3(Grid.Map[i, j].Posreal.x + 200, Grid.Map[i, j].Posreal.y, Grid.Map[i, j].Posreal.z);
                        Gizmos.DrawSphere(newPos, 1);
                    }
                }
            }
        }
    }
}
