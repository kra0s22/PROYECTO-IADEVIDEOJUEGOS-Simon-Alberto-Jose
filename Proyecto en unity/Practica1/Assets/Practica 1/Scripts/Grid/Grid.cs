using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Grid : MonoBehaviour
{
    private int ancho;
    private int alto;
    private int nodes;
    Ray ray;
    private GameObject suelo;

    public int Tam { get; set; }
    public Node[,] Map { get; set; }
    public int Nodes { get => nodes; set => nodes = value; }


    private void Awake()
    {
        suelo = GameObject.Find("Suelo");
        ancho = (int)suelo.transform.localScale.x;
        alto = (int)suelo.transform.localScale.z;
        
        Nodes = 20;
        Tam = ancho / nodes;
        Map = new Node[Nodes, Nodes];
        ray = new Ray();

        for (int i = 0; i * Tam < ancho; i++)
        {
            for (int j = 0; j * Tam < alto; j++)
            {
                Node node = new Node();
                node.Posreal = new Vector3(i * Tam, 0, j * Tam);
                node.Posplano = new Vector3(i, 0, j);
                ray.origin = new Vector3(node.Posreal.x, node.Posreal.y + 5, node.Posreal.z);
                ray.direction = new Vector3(0, -1, 0);
                RaycastHit[] hit = Physics.RaycastAll(ray, 10);

                foreach (RaycastHit h in hit)
                {
                    if (h.transform.CompareTag("Pared"))
                    {
                        node.Obstacle = true;
                    }
                    else if (h.transform.CompareTag("Bosque"))
                    {
                        node.Bosque = true;
                    }
                    else if (h.transform.CompareTag("Tierra"))
                    {
                        node.Tierra = true;
                    }
                }
                Map[i, j] = node;
            }
        }
    }
    
    public Vector3 RealToPlane(Vector3 position)
    {
        return new Vector3(Mathf.Round(position.x / Tam), 0, Mathf.Round(position.z / Tam));
    }

    public Vector3 PlaneToReal(Vector3 position)
    {
        return new Vector3(position.x * Tam, 0, position.z * Tam);
    }
    public List<Node> getNodesAround(Vector3 vector)
    {
        List<Node> list = new List<Node>();

        // X   
        if (vector.x > 0 && vector.x < 19)
        {
            if (!(Map[(int)vector.x - 1, (int)vector.z]).Obstacle)
            {
                list.Add(Map[(int)vector.x - 1, (int)vector.z]);
            }

            if (!(Map[(int)vector.x + 1, (int)vector.z]).Obstacle)
                list.Add(Map[(int)vector.x + 1, (int)vector.z]);
        }

        if (vector.x == 0)
        {
            if (!(Map[(int)vector.x + 1, (int)vector.z]).Obstacle)
                list.Add(Map[(int)vector.x + 1, (int)vector.z]);
        }

        if (vector.x == 19)
        {
            if (!(Map[(int)vector.x - 1, (int)vector.z]).Obstacle)
                list.Add(Map[(int)vector.x - 1, (int)vector.z]);
        }

        // Z
        if (vector.z > 0 && vector.z < 19)
        {
            if (!(Map[(int)vector.x, (int)vector.z - 1]).Obstacle)
                list.Add(Map[(int)vector.x, (int)vector.z - 1]);
            if (!(Map[(int)vector.x, (int)vector.z + 1]).Obstacle)
                list.Add(Map[(int)vector.x, (int)vector.z + 1]);
        }

        if (vector.z == 0)
        {
            if (!(Map[(int)vector.x, (int)vector.z + 1]).Obstacle)
                list.Add(Map[(int)vector.x, (int)vector.z + 1]);
        }

        if (vector.z == 19)
        {
            if (!(Map[(int)vector.x, (int)vector.z - 1]).Obstacle)
                list.Add(Map[(int)vector.x, (int)vector.z - 1]);
        }

        return list;
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for (int i = 0; i < Nodes; i++)
        {
            Gizmos.DrawLine(Map[i, 0].Posreal, Map[i, Nodes - 1].Posreal);
            Gizmos.DrawLine(Map[0, i].Posreal, Map[Nodes - 1, i].Posreal);
        }
        for (int i = 0; i < Nodes; i++)
        {
            for (int j = 0; j < Nodes; j++)
            {
                if (Map[i, j].Obstacle)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawSphere(Map[i, j].Posreal, 1);
                }
                else if (Map[i, j].Bosque)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(Map[i, j].Posreal, 1);
                }
                else if (Map[i, j].Tierra)
                {
                    Gizmos.color = new Color(153f / 255f, 101f / 255f, 21f / 255f, 1); //BROWN
                    Gizmos.DrawSphere(Map[i, j].Posreal, 1);
                }
            }
        }
    }*/
}

