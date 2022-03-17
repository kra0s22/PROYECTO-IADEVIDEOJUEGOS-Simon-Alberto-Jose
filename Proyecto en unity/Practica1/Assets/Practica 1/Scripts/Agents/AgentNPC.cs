using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AgentNPC : Agent
{

    private SteeringBehaviour steeringBehaviour; // Steering Behaviour
    private Path path; // Path
    private PathFinding pathFinding; // PathFinding
    private Steering kinetic; // Steering
    private StateMachine stateMachine; // Maquina de estados
    private AgentNPC enemigoMasCercano; // Enemigo más cercano
    private int wayPoint; // WayPoint actual
    private float timer; // Tiempo para ganar
    private bool influenceBlock; // Bloqueado por influencia

    private Text information; //  Texto donde mostrar la información
    private ControladorParte1 controlador; // Controlador para la parte 1

    public Steering Kinetic { get => kinetic; set => kinetic = value; }
    public Path Path { get => path; set => path = value; }
    public PathFinding PathFinding { get => pathFinding; set => pathFinding = value; }
    public SteeringBehaviour SteeringBehaviour { get => steeringBehaviour; set => steeringBehaviour = value; }
    public StateMachine StateMachine { get => stateMachine; set => stateMachine = value; }
    public AgentNPC EnemigoMasCercano { get => enemigoMasCercano; set => enemigoMasCercano = value; }
    public int WayPoint { get => wayPoint; set => wayPoint = value; }
    public bool InfluenceBlock { get => influenceBlock; set => influenceBlock = value; }

    public virtual void Start()
    {
        Position = transform.position;
        MaxVelocity = 3;
        MaxAcceleration = 3;
        MaxVelocityInicial = MaxVelocity;
        SaludMaxima = 100;
        SaludActual = SaludMaxima;
        SteeringBehaviour = gameObject.GetComponent<SteeringBehaviour>();
        Kinetic = gameObject.AddComponent<Steering>();
        influenceBlock = false;
        
        if (this.tag == "PruebaPathFinding")
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
        if (!(this.tag == "Equipo Azul" || this.tag == "Equipo Rojo" || this.tag == "Patrullero Equipo Azul" ||
            this.tag == "Patrullero Equipo Rojo"))
        {
            controlador = GameObject.Find("Controlador").GetComponent<ControladorParte1>();
        }
    }
 
    public void Update()
    {
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
                }
            }
            else
            {
                timer = 10;
            }
        }
        else if (this.transform.CompareTag("Equipo Rojo"))
        {
            if ((Position - StateMachine.Manager.PosicionBaseAzul).magnitude < 1f)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    StateMachine.Manager.VictoriaRojo = true;
                }
            }
            else
            {
                timer = 10;
            }
        }

    }

    // Aplica ecuaciones de Newton-Euler 
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
    }

    // Comprueba en qué terreno está
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

    // Comprueba si hay un enemigo cerca y lo almacena
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

    private void OnMouseDown()
    {
        if(this.tag == "Equipo Azul" || this.tag == "Equipo Rojo" || this.tag == "Patrullero Equipo Azul" ||
            this.tag == "Patrullero Equipo Rojo")
        {
            this.StateMachine.Manager.AgentDebug = this;
        }
        else
        {
            this.controlador.AgentDebug = this;
        }
    }
}
