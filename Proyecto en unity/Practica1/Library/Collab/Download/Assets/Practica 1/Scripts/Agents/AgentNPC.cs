using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AgentNPC : Agent
{

    private SteeringBehaviour steeringBehaviour;

    private Path path;
    private PathFinding pathFinding;
    private Steering kinetic;
    private StateMachine stateMachine;
    private AgentNPC enemigoMasCercano;
    private int wayPoint;
    private float timer;
    private bool influenceBlock;

    private Text information;

    public Steering Kinetic { get => kinetic; set => kinetic = value; }
    public Path Path { get => path; set => path = value; }
    public PathFinding PathFinding { get => pathFinding; set => pathFinding = value; }
    public SteeringBehaviour SteeringBehaviour { get => steeringBehaviour; set => steeringBehaviour = value; }
    public StateMachine StateMachine { get => stateMachine; set => stateMachine = value; }
    public AgentNPC EnemigoMasCercano { get => enemigoMasCercano; set => enemigoMasCercano = value; }
    public int WayPoint { get => wayPoint; set => wayPoint = value; }
    public bool InfluenceBlock { get => influenceBlock; set => influenceBlock = value; }

    public void Start()
    {
        Position = transform.position;
        MaxVelocityInicial = MaxVelocity;
        SteeringBehaviour = gameObject.GetComponent<SteeringBehaviour>();
        Kinetic = gameObject.AddComponent<Steering>();
        influenceBlock = false;
        if (SteeringBehaviour is PFLWYG || SteeringBehaviour is PathFollowing)
        {
            Path = gameObject.AddComponent<Path>();
            PathFinding = gameObject.AddComponent<PathFinding>();
        }
        if (this.tag == "Equipo Azul" || this.tag == "Equipo Rojo" || this.tag == "Patrullero Equipo Azul" || 
            this.tag == "Patrullero Equipo Rojo")
        {
            StateMachine = gameObject.AddComponent<StateMachine>();
            information = StateMachine.Manager.information;
        }
            
        Respawned = true;
        timer = 10;       
    }

    
    public void Update()
    {
        base.Update();
        // Solo si corresponde a la parte 1 se puede ejecutar esto
        if (!(this.tag == "Equipo Azul" || this.tag == "Equipo Rojo" || this.tag == "Patrullero Equipo Azul" ||
            this.tag == "Patrullero Equipo Rojo") && (SteeringBehaviour is PFLWYG || SteeringBehaviour is PathFollowing) && Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                PathFollowing pathfollowing = (PathFollowing)steeringBehaviour;
                pathfollowing.Reset();
                SteeringBehaviour = pathfollowing;
                Path = PathFinding.LRTAMin(Position, new Vector3(hit.point.x, 0, hit.point.z));
            }           
        }


        


        Material material;
        material = this.gameObject.GetComponent<Renderer>().material;
        if (Ofensivo)
        {
            if (this.transform.CompareTag("Equipo Azul"))
            {
                material.color = Color.cyan;
            }
            else if (this.transform.CompareTag("Equipo Rojo"))
            {
                material.color = Color.magenta;
            }
        }
        else
        {
            if (this.transform.CompareTag("Equipo Azul"))
            {
                material.color = Color.blue;
            }
            else if (this.transform.CompareTag("Equipo Rojo"))
            {
                material.color = Color.red;
            }
        }

        comprobarTerreno();

        if (SteeringBehaviour != null)
        {
            Kinetic = SteeringBehaviour.getSteering(this);
            applySteering(Kinetic);
        }

        if (this.transform.CompareTag("Equipo Azul"))
        {
            if ((Position - StateMachine.Manager.PosicionBaseRojo).magnitude < 1f)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    StateMachine.Manager.VictoriaAzul = true;
                    //CARGAR ESCENA VICTORIA
                }
            }
            else
            {
                timer = 10;
            }

            //Debug.Log(this.name + " TIMER = " + timer);

        }
        else if (this.transform.CompareTag("Equipo Rojo"))
        {
            if ((Position - StateMachine.Manager.PosicionBaseAzul).magnitude < 1f)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    StateMachine.Manager.VictoriaAzul = true;
                }
            }
            else
            {
                timer = 10;
            }
        }
    }

    public void applySteering(Steering kinetic)
    {
        Velocity += kinetic.Linear * Time.deltaTime;
        if (Velocity.magnitude > MaxVelocity)
        {
            Velocity = Velocity.normalized * MaxVelocity;
        }

        Position += Velocity * Time.deltaTime;
        transform.position = Position;

        Rotation += ((float) kinetic.Angular) * Time.deltaTime;
        if (Rotation > MaxRotation)
        {
            Rotation = MaxRotation;
        }
        Orientation += Rotation * Time.deltaTime;
        transform.eulerAngles = new Vector3(0.0f, Orientation, 0.0f);
        Debug.Log(transform.eulerAngles + " angular = " + kinetic.Angular + " Rotation = " + Rotation);
    }


    public void comprobarTerreno()
    {
        Ray ray = new Ray();
        ray.origin = new Vector3(Position.x, Position.y + 5, Position.z);
        ray.direction = new Vector3(0, -1, 0);
        RaycastHit[] hit = Physics.RaycastAll(ray, 10);
        bool terreno = false; // para comprobar solo si hay terreno encima del suelo (no funciona hit[0])

        foreach (RaycastHit h in hit)
        {
            if (terreno)
            {
                continue;
            }
            if (h.transform.CompareTag("Bosque"))
            {
                if (ResistenciaBosque > ResistenciaTierra)
                {
                    MaxVelocity = MaxVelocityInicial / 2;
                }
                else if (ResistenciaTierra > ResistenciaBosque)
                {
                    MaxVelocity = MaxVelocityInicial * 1.5f;
                }
                else
                {
                    MaxVelocity = MaxVelocityInicial;
                }

                terreno = true;

            }
            else if (h.transform.CompareTag("Tierra"))
            {
                if (ResistenciaBosque < ResistenciaTierra)
                {
                    MaxVelocity = MaxVelocityInicial / 2;
                }
                else if (ResistenciaTierra < ResistenciaBosque)
                {
                    MaxVelocity = MaxVelocityInicial * 1.5f;
                }
                else
                {
                    MaxVelocity = MaxVelocityInicial;
                }
                terreno = true;

            }
            else
            {
                MaxVelocity = MaxVelocityInicial;
            }
        }
    }

    public bool enemigoCerca()
    {
        bool enemigoEncontrado = false;
        RaycastHit[] hit = Physics.SphereCastAll(Position, 10, Vector3.up);
        float minDistance = Mathf.Infinity;
        foreach (RaycastHit h in hit)
        {
            if (this.transform.CompareTag("Equipo Azul") || this.transform.CompareTag("Patrullero Equipo Azul"))
            {
                if (h.transform.CompareTag("Equipo Rojo") || h.transform.CompareTag("Patrullero Equipo Rojo"))
                {
                    enemigoEncontrado = true;
                    GameObject enemigo = h.transform.gameObject;
                    AgentNPC en = enemigo.GetComponent<AgentNPC>();
                    float distance = (enemigo.transform.position - Position).magnitude;
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        EnemigoMasCercano = en;
                    }
                }
            }
            else if (this.transform.CompareTag("Equipo Rojo") || this.transform.CompareTag("Patrullero Equipo Rojo"))
            {
                if (h.transform.CompareTag("Equipo Azul") || h.transform.CompareTag("Patrullero Equipo Azul"))
                {
                    enemigoEncontrado = true;
                    GameObject enemigo = h.transform.gameObject;
                    AgentNPC en = enemigo.GetComponent<AgentNPC>();
                    float distance = (enemigo.transform.position - Position).magnitude;
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        EnemigoMasCercano = en;
                    }
                }
            }
        }
        return enemigoEncontrado;
    }

    private void OnMouseOver()
    {
        if (this.transform.CompareTag("Equipo Azul") || this.transform.CompareTag("Patrullero Equipo Azul"))
        {
            information.color = Color.blue;
            information.text = this.name + "\n" + "Estado actual = " + StateMachine.CurrentState.GetType().Name + "\n" + "Salud actual = " + SaludActual;
        }
        else if (this.transform.CompareTag("Equipo Rojo") || this.transform.CompareTag("Patrullero Equipo Rojo"))
        {
            information.color = Color.red;
            information.text = this.name + "\n" + "Estado actual = " + StateMachine.CurrentState.GetType().Name + "\n" + "Salud actual = " + SaludActual;
        }            
    }

    private void OnMouseExit()
    {
        information.text = "";
    }

    private void OnDrawGizmos()
    {
        Vector3 aux = new Vector3(-1, 0, 0);
        if(Deb)
        {
            if (Path != null)
            {
                if (Input.GetKey(KeyCode.P))
                {
                    foreach (Vector3 pos in Path.Positions)
                    {
                        if (aux.x == -1)
                        {
                            aux = pos;
                            continue;
                        }


                        if (this.gameObject.CompareTag("Equipo Azul"))
                        {
                            Gizmos.color = Color.blue;
                            Gizmos.DrawLine(aux, pos);
                            aux = pos;
                            
                        }
                        else
                        {
                            Gizmos.color = Color.red;
                            Gizmos.DrawLine(aux, pos);
                            aux = pos;
                        }
                        Gizmos.DrawSphere(pos, 1);
                    }
                }
            }
            if (Input.GetKey(KeyCode.G))
            {
                if (PathFinding.Grid != null)
                {
                    Gizmos.color = Color.blue;
                    for (int i = 0; i < PathFinding.Grid.Nodes; i++)
                    {
                        Gizmos.DrawLine(PathFinding.Grid.Map[i, 0].Posreal, PathFinding.Grid.Map[i, PathFinding.Grid.Nodes - 1].Posreal);
                        Gizmos.DrawLine(PathFinding.Grid.Map[0, i].Posreal, PathFinding.Grid.Map[PathFinding.Grid.Nodes - 1, i].Posreal);
                    }
                    for (int i = 0; i < PathFinding.Grid.Nodes; i++)
                    {
                        for (int j = 0; j < PathFinding.Grid.Nodes; j++)
                        {
                            
                            if (PathFinding.Grid.Map[i, j].Obstacle)
                            {
                                Gizmos.color = Color.cyan;
                                Gizmos.DrawSphere(PathFinding.Grid.Map[i, j].Posreal, 1);
                            }
                            else if (PathFinding.Grid.Map[i, j].Bosque)
                            {
                                Gizmos.color = Color.green;
                                Gizmos.DrawSphere(PathFinding.Grid.Map[i, j].Posreal, 1);
                            }
                            else if (PathFinding.Grid.Map[i, j].Tierra)
                            {
                                Gizmos.color = new Color(153f / 255f, 101f / 255f, 21f / 255f, 1); //BROWN
                                Gizmos.DrawSphere(PathFinding.Grid.Map[i, j].Posreal, 1);
                            }
                        }
                    }
                }
            }
            if (Input.GetKey(KeyCode.R))
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(Position, 10);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(Position, Rango);
            }           
        }
    }
}
