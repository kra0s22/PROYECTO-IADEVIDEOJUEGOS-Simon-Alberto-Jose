using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ControladorParte1 : MonoBehaviour
{
    public Text informacion;
    private AgentNPC agentDebug;
    private Agent[] listaPersonajes;
    private Grid grid;
    public Button debug;
    public Button botonBasicos;
    public Button botonDelegados;
    public Button botonFormaciones;
    public Button botonFlocking;
    public Button botonLeaderFollowing;
    public Button botonWallAvoidance;
    public Button botonPathFinding;
    public GameObject basicos;
    public GameObject delegados;
    public GameObject formaciones;
    public GameObject flocking;
    public GameObject leaderFollowing;
    public GameObject obstacleAvoid;
    public GameObject pathFinding;
    private bool deb;
    private Color original;
    private Color selected;


    public Grid Grid { get => grid; set => grid = value; }
    public AgentNPC AgentDebug { get => agentDebug; set => agentDebug = value; }

    private void Start()
    {
        Grid = gameObject.AddComponent<Grid>();
        original = Color.white;
        selected = new Color(157f / 255, 157f / 255, 157f / 255, 1);
        deb = false;
    }

    private void Update()
    {
        listaPersonajes = global::Agent.FindObjectsOfType<Agent>();
        if (AgentDebug != null)
        {
            informacion.text = AgentDebug.name + "\n\n"
                + "Velocidad = " + AgentDebug.Velocity.magnitude + "\n"
                + "Max Velocidad = " + AgentDebug.MaxVelocity + "\n"
                + "Rotación = " + AgentDebug.Rotation + "\n"
                + "Max Rotación = " + AgentDebug.MaxRotation + "\n";
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
    public void ModeDebug()
    {
        if (!deb)
        {
            debug.GetComponent<Image>().color = selected;
        }
        else
        {
            debug.GetComponent<Image>().color = original;
        }
        deb = !deb;
    }

    public void Basicos()
    {
        botonBasicos.GetComponent<Image>().color = selected;
        botonDelegados.GetComponent<Image>().color = original;
        botonFormaciones.GetComponent<Image>().color = original;
        botonFlocking.GetComponent<Image>().color = original;
        botonLeaderFollowing.GetComponent<Image>().color = original;

        basicos.SetActive(true);
        delegados.SetActive(false);
        formaciones.SetActive(false);
        flocking.SetActive(false);
        leaderFollowing.SetActive(false);
    }

    public void Delegados()
    {
        botonBasicos.GetComponent<Image>().color = original;
        botonDelegados.GetComponent<Image>().color = selected;
        botonFormaciones.GetComponent<Image>().color = original;
        botonFlocking.GetComponent<Image>().color = original;
        botonLeaderFollowing.GetComponent<Image>().color = original;

        basicos.SetActive(false);
        delegados.SetActive(true);
        formaciones.SetActive(false);
        flocking.SetActive(false);
        leaderFollowing.SetActive(false);
    }

    public void Formaciones()
    {
        botonBasicos.GetComponent<Image>().color = original;
        botonDelegados.GetComponent<Image>().color = original;
        botonFormaciones.GetComponent<Image>().color = selected;
        botonFlocking.GetComponent<Image>().color = original;
        botonLeaderFollowing.GetComponent<Image>().color = original;

        basicos.SetActive(false);
        delegados.SetActive(false);
        formaciones.SetActive(true);
        flocking.SetActive(false);
        leaderFollowing.SetActive(false);
    }

    public void Flocking()
    {
        botonBasicos.GetComponent<Image>().color = original;
        botonDelegados.GetComponent<Image>().color = original;
        botonFormaciones.GetComponent<Image>().color = original;
        botonFlocking.GetComponent<Image>().color = selected;
        botonLeaderFollowing.GetComponent<Image>().color = original;

        basicos.SetActive(false);
        delegados.SetActive(false);
        formaciones.SetActive(false);
        flocking.SetActive(true);
        leaderFollowing.SetActive(false);
    }
    public void LeaderFollowing()
    {
        botonBasicos.GetComponent<Image>().color = original;
        botonDelegados.GetComponent<Image>().color = original;
        botonFormaciones.GetComponent<Image>().color = original;
        botonLeaderFollowing.GetComponent<Image>().color = selected;
        botonFlocking.GetComponent<Image>().color = original;

        basicos.SetActive(false);
        delegados.SetActive(false);
        formaciones.SetActive(false);
        flocking.SetActive(false);
        leaderFollowing.SetActive(true);
    }

    public void WallAvoidance()
    {
        botonPathFinding.GetComponent<Image>().color = original;
        botonWallAvoidance.GetComponent<Image>().color = selected;

        pathFinding.SetActive(false);
        obstacleAvoid.SetActive(true);
    }

    public void PathFinding()
    {
        botonPathFinding.GetComponent<Image>().color = selected;
        botonWallAvoidance.GetComponent<Image>().color = original;

        pathFinding.SetActive(true);
        obstacleAvoid.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        if (deb)
        {
            foreach (Agent a in listaPersonajes)
            {
                if (a is AgentNPC)
                {
                    AgentNPC anpc = (AgentNPC)a;

                    if (anpc.SteeringBehaviour is PathFollowing)
                    {

                        if (Input.GetKey(KeyCode.G))
                        {
                            if (Grid != null)
                            {
                                Gizmos.color = Color.blue;
                                for (int i = 0; i < Grid.Nodes; i++)
                                {
                                    Gizmos.DrawLine(Grid.Map[i, 0].Posreal, Grid.Map[i, Grid.Nodes - 1].Posreal);
                                    Gizmos.DrawLine(Grid.Map[0, i].Posreal, Grid.Map[Grid.Nodes - 1, i].Posreal);
                                }
                                for (int i = 0; i < Grid.Nodes; i++)
                                {
                                    for (int j = 0; j < Grid.Nodes; j++)
                                    {

                                        if (Grid.Map[i, j].Obstacle)
                                        {
                                            Gizmos.color = Color.cyan;
                                            Gizmos.DrawSphere(Grid.Map[i, j].Posreal, 1);
                                        }
                                    }
                                }
                            }
                        }
                        if (anpc.PathFinding != null)
                        {
                            Gizmos.color = Color.black;
                            if (anpc.PathFinding.PosicionfinalDebug != Vector3.zero)
                            {
                                Gizmos.DrawLine(anpc.Position, anpc.PathFinding.PosicionfinalDebug);
                            }
                        }
                        if (anpc.Path != null)
                        {
                            if (Input.GetKey(KeyCode.P))
                            {
                                Vector3 aux = new Vector3(-1, 0, 0);
                                if (anpc.Path != null)
                                {
                                    if (Input.GetKey(KeyCode.P))
                                    {
                                        foreach (Vector3 pos in anpc.Path.Positions)
                                        {
                                            if (aux.x == -1)
                                            {
                                                aux = pos;
                                                continue;
                                            }

                                            Gizmos.DrawLine(aux, pos);
                                            aux = pos;
                                            Gizmos.DrawSphere(pos, 1);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (anpc.SteeringBehaviour is ObstacleAvoid)
                    {
                        WallAvoidance w = anpc.GetComponent<WallAvoidance>();
                        if (w.Collision != null)
                        {
                            Gizmos.color = Color.black;
                            Gizmos.DrawLine(anpc.Position, w.Collision.Position);
                            Gizmos.color = Color.red;
                            Gizmos.DrawLine(w.Collision.Position, w.TargetFicticio.Position);
                        }
                    }
                }
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(a.Position, a.ExteriorRadius);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(a.Position, a.InteriorRadius);
            }
        }
    }
}
