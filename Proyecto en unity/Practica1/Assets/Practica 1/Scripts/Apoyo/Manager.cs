using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Tactica
{
    Estandar,
    Agrupar,
    OfensivaTotal
}


public class Manager : MonoBehaviour
{
    private AgentNPC[] listaPersonajes;
    private List<AgentNPC> equipoAzul;
    private List<AgentNPC> equipoRojo;
    private int modoActual;
    private Vector3[] wayPointAzulOfensivo;
    private Vector3[] wayPointAzulDefensivo;
    private Vector3[] wayPointRojoOfensivo;
    private Vector3[] wayPointRojoDefensivo;

    private Vector3[] wayPointAzulOfensivaTotal;
    private Vector3[] wayPointRojoOfensivaTotal;

    private AgentNPC[] wayPointAzulOfensivaTotalOcupado;
    private AgentNPC[] wayPointRojoOfensivaTotalOcupado;

    private AgentNPC[] wayPointAzulDefensivoOcupado;
    private AgentNPC[] wayPointRojoDefensivoOcupado;

    private Vector3[] wayPointAzulCura;
    private Vector3[] wayPointRojoCura;

    private Vector3[] wayPointRojoPatrullero;
    private Vector3[] wayPointAzulPatrullero;

    private Vector3[] wayPointMeleAgruparAzul;
    private Vector3[] wayPointMeleAgruparRojo;
    private Vector3[] wayPointTodoTerrenoAgruparAzul;
    private Vector3[] wayPointTodoTerrenoAgruparRojo;
    private Vector3[] wayPointRangedAgruparAzul;
    private Vector3[] wayPointRangedAgruparRojo;

    private int numeroMeles;
    private int numeroRanged;
    private int numeroTodoterreno;
    private int numeroOfensivaTotal;

    private Vector3[] wayPointOfensivaTotal;

    private bool victoriaAzul;
    private bool victoriaRojo;
    private bool deb;
    private bool block;

    private Tactica tactica;

    private Vector3 posicionBaseAzul;
    private Vector3 posicionBaseRojo;

    public Text information;
    private AgentNPC agentDebug;

    public Button azulOfensivo;
    public Button azulDefensivo;
    public Button rojoOfensivo;
    public Button rojoDefensivo;
    public Button botonGuerraTotal;
    public Button estandar;
    public Button agrupar;
    public Button debug;

    private Color azulOriginal;
    private Color azulSelected;
    private Color rojoOriginal;
    private Color rojoSelected;

    private Color guerraOriginal;
    private Color guerraSelected;

    private Color tacticaOriginal;
    private Color tacticaSelected;

    private Color debugOriginal;
    private Color debugSelected;

    private InfluenceMap influenceMap;


    public Vector3[] WayPointAzulOfensivo { get => wayPointAzulOfensivo; set => wayPointAzulOfensivo = value; }
    public Vector3[] WayPointRojoOfensivo { get => wayPointRojoOfensivo; set => wayPointRojoOfensivo = value; }
    public Vector3[] WayPointAzulCura { get => wayPointAzulCura; set => wayPointAzulCura = value; }
    public Vector3[] WayPointRojoCura { get => wayPointRojoCura; set => wayPointRojoCura = value; }
    public Vector3[] WayPointAzulDefensivo { get => wayPointAzulDefensivo; set => wayPointAzulDefensivo = value; }
    public Vector3[] WayPointRojoDefensivo { get => wayPointRojoDefensivo; set => wayPointRojoDefensivo = value; }
    public AgentNPC[] WayPointAzulDefensivoOcupado { get => wayPointAzulDefensivoOcupado; set => wayPointAzulDefensivoOcupado = value; }
    public AgentNPC[] WayPointRojoDefensivoOcupado { get => wayPointRojoDefensivoOcupado; set => wayPointRojoDefensivoOcupado = value; }
    public bool VictoriaAzul { get => victoriaAzul; set => victoriaAzul = value; }
    public bool VictoriaRojo { get => victoriaRojo; set => victoriaRojo = value; }
    public Vector3 PosicionBaseAzul { get => posicionBaseAzul; set => posicionBaseAzul = value; }
    public Vector3 PosicionBaseRojo { get => posicionBaseRojo; set => posicionBaseRojo = value; }
    public Vector3[] WayPointRojoPatrullero { get => wayPointRojoPatrullero; set => wayPointRojoPatrullero = value; }
    public Vector3[] WayPointAzulPatrullero { get => wayPointAzulPatrullero; set => wayPointAzulPatrullero = value; }
    public List<AgentNPC> EquipoAzul { get => equipoAzul; set => equipoAzul = value; }
    public List<AgentNPC> EquipoRojo { get => equipoRojo; set => equipoRojo = value; }
    public AgentNPC AgentDebug { get => agentDebug; set => agentDebug = value; }
    public Tactica Tactica { get => tactica; set => tactica = value; }
    public Vector3[] WayPointMeleAgruparAzul { get => wayPointMeleAgruparAzul; set => wayPointMeleAgruparAzul = value; }
    public Vector3[] WayPointMeleAgruparRojo { get => wayPointMeleAgruparRojo; set => wayPointMeleAgruparRojo = value; }
    public Vector3[] WayPointTodoTerrenoAgruparAzul { get => wayPointTodoTerrenoAgruparAzul; set => wayPointTodoTerrenoAgruparAzul = value; }
    public Vector3[] WayPointTodoTerrenoAgruparRojo { get => wayPointTodoTerrenoAgruparRojo; set => wayPointTodoTerrenoAgruparRojo = value; }
    public Vector3[] WayPointRangedAgruparAzul { get => wayPointRangedAgruparAzul; set => wayPointRangedAgruparAzul = value; }
    public Vector3[] WayPointRangedAgruparRojo { get => wayPointRangedAgruparRojo; set => wayPointRangedAgruparRojo = value; }
    public int NumeroMeles { get => numeroMeles; set => numeroMeles = value; }
    public int NumeroRanged { get => numeroRanged; set => numeroRanged = value; }
    public int NumeroTodoterreno { get => numeroTodoterreno; set => numeroTodoterreno = value; }
    public Vector3[] WayPointAzulOfensivaTotal { get => wayPointAzulOfensivaTotal; set => wayPointAzulOfensivaTotal = value; }
    public Vector3[] WayPointRojoOfensivaTotal { get => wayPointRojoOfensivaTotal; set => wayPointRojoOfensivaTotal = value; }
    public AgentNPC[] WayPointAzulOfensivaTotalOcupado { get => wayPointAzulOfensivaTotalOcupado; set => wayPointAzulOfensivaTotalOcupado = value; }
    public AgentNPC[] WayPointRojoOfensivaTotalOcupado { get => wayPointRojoOfensivaTotalOcupado; set => wayPointRojoOfensivaTotalOcupado = value; }
    public int NumeroOfensivaTotal { get => numeroOfensivaTotal; set => numeroOfensivaTotal = value; }
    public InfluenceMap InfluenceMap { get => influenceMap; set => influenceMap = value; }

    void Awake()
    {
        InfluenceMap = gameObject.AddComponent<InfluenceMap>();
        listaPersonajes = AgentNPC.FindObjectsOfType<AgentNPC>();
        EquipoAzul = new List<AgentNPC>();
        EquipoRojo = new List<AgentNPC>();
        modoActual = 0;
        numeroMeles = 0;
        numeroTodoterreno = 0;
        numeroRanged = 0;
        NumeroOfensivaTotal = 0;
        Tactica = Tactica.Estandar;
        deb = false;
        block = false;

        foreach (AgentNPC a in listaPersonajes)
        {
            if (a.transform.CompareTag("Equipo Azul") || a.transform.CompareTag("Patrullero Equipo Azul"))
            {
                EquipoAzul.Add(a);
            }
            else if (a.transform.CompareTag("Equipo Rojo") || a.transform.CompareTag("Patrullero Equipo Rojo"))
            {
                EquipoRojo.Add(a);
            }
        }

        WayPointAzulOfensivo = new Vector3[2];
        WayPointAzulOfensivo[0] = new Vector3(35, 0, 50);
        WayPointAzulOfensivo[1] = new Vector3(90, 0, 50);

        WayPointRojoOfensivo = new Vector3[2];
        WayPointRojoOfensivo[0] = new Vector3(60, 0, 50);
        WayPointRojoOfensivo[1] = new Vector3(5, 0, 50);

        WayPointAzulCura = new Vector3[2];
        WayPointAzulCura[0] = new Vector3(5, 0, 90);
        WayPointAzulCura[1] = new Vector3(5, 0, 5);

        WayPointRojoCura = new Vector3[2];
        WayPointRojoCura[0] = new Vector3(90, 0, 90);
        WayPointRojoCura[1] = new Vector3(90, 0, 5);

        WayPointAzulDefensivo = new Vector3[5];
        WayPointAzulDefensivo[0] = new Vector3(30, 0, 85);
        WayPointAzulDefensivo[1] = new Vector3(30, 0, 65);
        WayPointAzulDefensivo[2] = new Vector3(30, 0, 50);
        WayPointAzulDefensivo[3] = new Vector3(30, 0, 30);
        WayPointAzulDefensivo[4] = new Vector3(30, 0, 10);

        WayPointRojoDefensivo = new Vector3[5];
        WayPointRojoDefensivo[0] = new Vector3(65, 0, 85);
        WayPointRojoDefensivo[1] = new Vector3(65, 0, 65);
        WayPointRojoDefensivo[2] = new Vector3(65, 0, 50);
        WayPointRojoDefensivo[3] = new Vector3(65, 0, 30);
        WayPointRojoDefensivo[4] = new Vector3(65, 0, 10);

        wayPointAzulDefensivoOcupado = new AgentNPC[WayPointAzulDefensivo.Length];
        wayPointRojoDefensivoOcupado = new AgentNPC[WayPointRojoDefensivo.Length];

        PosicionBaseAzul = WayPointRojoOfensivo[WayPointRojoOfensivo.Length - 1];
        PosicionBaseRojo = WayPointAzulOfensivo[WayPointAzulOfensivo.Length - 1];

        WayPointAzulPatrullero = new Vector3[4];
        WayPointAzulPatrullero[0] = new Vector3(10, 0, 55);
        WayPointAzulPatrullero[1] = new Vector3(10, 0, 40);
        WayPointAzulPatrullero[2] = new Vector3(0, 0, 40);
        WayPointAzulPatrullero[3] = new Vector3(0, 0, 55);

        WayPointRojoPatrullero = new Vector3[4];
        WayPointRojoPatrullero[0] = new Vector3(85, 0, 55);
        WayPointRojoPatrullero[1] = new Vector3(85, 0, 40);
        WayPointRojoPatrullero[2] = new Vector3(95, 0, 40);
        WayPointRojoPatrullero[3] = new Vector3(95, 0, 55);

        WayPointMeleAgruparAzul = new Vector3[2];
        WayPointMeleAgruparAzul[0] = new Vector3(35, 0, 15);
        WayPointMeleAgruparAzul[1] = posicionBaseRojo;

        WayPointMeleAgruparRojo = new Vector3[2];
        WayPointMeleAgruparRojo[0] = new Vector3(60, 0, 80);
        WayPointMeleAgruparRojo[1] = posicionBaseAzul;

        WayPointTodoTerrenoAgruparAzul = new Vector3[2];
        WayPointTodoTerrenoAgruparAzul[0] = new Vector3(35, 0, 50);
        WayPointTodoTerrenoAgruparAzul[1] = posicionBaseRojo;

        WayPointTodoTerrenoAgruparRojo = new Vector3[2];
        wayPointTodoTerrenoAgruparRojo[0] = new Vector3(60, 0, 50);
        WayPointTodoTerrenoAgruparRojo[1] = posicionBaseAzul;

        WayPointRangedAgruparAzul = new Vector3[2];
        WayPointRangedAgruparAzul[0] = new Vector3(35, 0, 80);
        WayPointRangedAgruparAzul[1] = PosicionBaseRojo;

        WayPointRangedAgruparRojo = new Vector3[2];
        WayPointRangedAgruparRojo[0] = new Vector3(60, 0, 15);
        WayPointRangedAgruparRojo[1] = posicionBaseAzul;

        WayPointAzulOfensivaTotal = new Vector3[6];
        WayPointAzulOfensivaTotal[0] = new Vector3(40, 0, 85);
        WayPointAzulOfensivaTotal[1] = new Vector3(30, 0, 65);
        WayPointAzulOfensivaTotal[2] = new Vector3(30, 0, 50);
        WayPointAzulOfensivaTotal[3] = new Vector3(30, 0, 30);
        WayPointAzulOfensivaTotal[4] = new Vector3(40, 0, 10);
        WayPointAzulOfensivaTotal[5] = posicionBaseRojo;

        WayPointRojoOfensivaTotal = new Vector3[6];
        WayPointRojoOfensivaTotal[0] = new Vector3(55, 0, 85);
        WayPointRojoOfensivaTotal[1] = new Vector3(65, 0, 65);
        WayPointRojoOfensivaTotal[2] = new Vector3(65, 0, 50);
        WayPointRojoOfensivaTotal[3] = new Vector3(65, 0, 30);
        WayPointRojoOfensivaTotal[4] = new Vector3(55, 0, 10);
        WayPointRojoOfensivaTotal[5] = posicionBaseAzul;

        wayPointAzulOfensivaTotalOcupado = new AgentNPC[WayPointAzulOfensivaTotal.Length];
        wayPointRojoOfensivaTotalOcupado = new AgentNPC[WayPointRojoOfensivaTotal.Length];

        victoriaAzul = false;
        victoriaRojo = false;

        azulOriginal = azulOfensivo.GetComponent<Image>().color;
        azulSelected = new Color(105f / 255f, 118f / 255f, 1, 1);
        rojoOriginal = rojoOfensivo.GetComponent<Image>().color;
        rojoSelected = new Color(1, 94f / 255f, 116f / 255f, 1);
        guerraOriginal = botonGuerraTotal.GetComponent<Image>().color;
        guerraSelected = new Color(173f / 255f, 128f / 255, 38f / 255f, 1);
        tacticaOriginal = estandar.GetComponent<Image>().color;
        tacticaSelected = new Color(180f / 255f, 180f / 255f, 180f / 255f, 1);
        debugOriginal = debug.GetComponent<Image>().color;
        debugSelected = new Color(157f / 255, 157f / 255, 157f / 255, 1);
        Estandar();
    }

    // Update is called once per frame
    void Update()
    {
        DeadManagement();
        TextInformation();
        CheckInput();
        CheckFinish();
    }

    public void DeadManagement()
    {
        foreach (AgentNPC agent in listaPersonajes)
        {
            if (agent.Muerto)
            {
                agent.gameObject.SetActive(false);
                agent.DeadPosition = agent.Position;
                agent.Respawned = false;
                agent.ContadorMuerte -= Time.deltaTime;
                if (agent.ContadorMuerte <= 0)
                {
                    if (agent.transform.CompareTag("Equipo Azul") || agent.transform.CompareTag("Patrullero Equipo Azul"))
                    {
                        float distance = Mathf.Infinity;
                        Vector3 newPos = Vector3.zero;
                        foreach (Vector3 pos in WayPointAzulCura)
                        {
                            if (((agent.Position - pos).magnitude) < distance)
                            {
                                distance = (agent.Position - pos).magnitude;
                                newPos = pos;
                            }
                        }

                        agent.Position = newPos;
                    }
                    else if (agent.transform.CompareTag("Equipo Rojo") || agent.transform.CompareTag("Patrullero Equipo Rojo"))
                    {
                        float distance = Mathf.Infinity;
                        Vector3 newPos = Vector3.zero;
                        foreach (Vector3 pos in WayPointRojoCura)
                        {
                            if (((agent.Position - pos).magnitude) < distance)
                            {
                                distance = (agent.Position - pos).magnitude;
                                newPos = pos;
                            }
                        }
                        agent.Position = newPos;
                    }

                    transform.position = agent.Position;
                    agent.gameObject.SetActive(true);
                    agent.Muerto = false;
                    agent.SaludActual = agent.SaludMaxima;
                    agent.StateMachine.ChangeState(agent.StateMachine.GoToRespawn);
                }
            }
        }
    }
    private void TextInformation()
    {
        if (AgentDebug != null)
        {
            if (AgentDebug.transform.CompareTag("Equipo Azul") || AgentDebug.transform.CompareTag("Patrullero Equipo Azul"))
            {
                information.color = Color.blue;
            }
            else if (AgentDebug.transform.CompareTag("Equipo Rojo") || AgentDebug.transform.CompareTag("Patrullero Equipo Rojo"))
            {
                information.color = Color.red;
            }
            information.text = AgentDebug.name + "\n"
                    + "Estado actual = " + AgentDebug.StateMachine.CurrentState.GetType().Name + "\n"
                    + "Salud actual = " + AgentDebug.SaludActual + "\n" + "WayPoint = " + AgentDebug.WayPoint + "\n"
                    + "Velocidad actual = " + System.Math.Round(AgentDebug.Velocity.magnitude, 1);
            if (AgentDebug.StateMachine.CurrentState is Attack)
            {
                information.text += "\n"
                    + "Enemigo más cercano = " + AgentDebug.EnemigoMasCercano + "\n"
                    + "Salud Enemigo = " + AgentDebug.EnemigoMasCercano.SaludActual;
            }
            else
            {
                information.text = AgentDebug.name + "\n"
                    + "Estado actual = " + AgentDebug.StateMachine.CurrentState.GetType().Name + "\n"
                    + "Salud actual = " + AgentDebug.SaludActual + "\n" + "WayPoint = " + AgentDebug.WayPoint + "\n"
                    + "Velocidad actual = " + System.Math.Round(AgentDebug.Velocity.magnitude, 1);
            }
        }

    }
    public void CheckInput()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

    }
    public void CheckFinish()
    {
        if (VictoriaAzul)
        {
            SceneManager.LoadScene(4);
        }
        if (VictoriaRojo)
        {
            SceneManager.LoadScene(5);
        }
    }

    public void Reset()
    {
        numeroMeles = 0;
        numeroTodoterreno = 0;
        numeroRanged = 0;
        numeroOfensivaTotal = 0;

        foreach (AgentNPC a in EquipoAzul)
        {
            a.WayPoint = 0;
            Destroy(a.Path);
            a.Path = null;
            Destroy(a.PathFinding);
            a.PathFinding = null;
            Destroy(a.SteeringBehaviour);
            a.SteeringBehaviour = null;
            if (a.StateMachine.CurrentState is GoToWayPoint)
            {
                a.StateMachine.GoToWayPoint.Enter();
            }
            if (a.StateMachine.CurrentState is RunAway)
            {
                a.StateMachine.RunAway.Enter();
            }

        }

        for (int i = 0; i < WayPointAzulDefensivoOcupado.Length; i++)
        {
            WayPointAzulDefensivoOcupado[i] = null;

        }

        for (int i = 0; i < WayPointRojoDefensivoOcupado.Length; i++)
        {
            WayPointRojoDefensivoOcupado[i] = null;
        }


        for (int i = 0; i < WayPointAzulOfensivaTotalOcupado.Length; i++)
        {
            WayPointAzulOfensivaTotalOcupado[i] = null;
        }

        for (int i = 0; i < WayPointRojoOfensivaTotalOcupado.Length; i++)
        {
            WayPointRojoOfensivaTotalOcupado[i] = null;
        }





        foreach (AgentNPC a in EquipoRojo)
        {
            a.WayPoint = 0;
            Destroy(a.Path);
            a.Path = null;
            Destroy(a.PathFinding);
            a.PathFinding = null;
            Destroy(a.SteeringBehaviour);
            a.SteeringBehaviour = null;
            if (a.StateMachine.CurrentState is GoToWayPoint)
            {
                a.StateMachine.GoToWayPoint.Enter();
            }
            if (a.StateMachine.CurrentState is RunAway)
            {
                a.StateMachine.RunAway.Enter();
            }
        }
    }

    public void AzulOfensivo()
    {
        if (modoActual != 1 && !block)
        {
            azulOfensivo.GetComponent<Image>().color = azulSelected;
            azulDefensivo.GetComponent<Image>().color = azulOriginal;
            rojoDefensivo.GetComponent<Image>().color = rojoSelected;
            rojoOfensivo.GetComponent<Image>().color = rojoOriginal;
            botonGuerraTotal.GetComponent<Image>().color = guerraOriginal;

            modoActual = 1;
            block = true;
            foreach (AgentNPC a in EquipoAzul)
            {
                a.Ofensivo = true;
            }
            foreach (AgentNPC a in EquipoRojo)
            {
                a.Ofensivo = false;
            }
            Reset();
        }
    }

    public void AzulDefensivo()
    {
        

        if (modoActual != 2 && !block)
        {
            azulDefensivo.GetComponent<Image>().color = azulSelected;
            azulOfensivo.GetComponent<Image>().color = azulOriginal;
            rojoOfensivo.GetComponent<Image>().color = rojoSelected;
            rojoDefensivo.GetComponent<Image>().color = rojoOriginal;
            botonGuerraTotal.GetComponent<Image>().color = guerraOriginal;

            modoActual = 2;
            block = true;
            foreach (AgentNPC a in EquipoRojo)
            {
                a.Ofensivo = true;
            }
            foreach (AgentNPC a in EquipoAzul)
            {
                a.Ofensivo = false;
            }
            Reset();
        }
    }

    public void RojoOfensivo()
    {
        

        if (modoActual != 2 && !block)
        {

            azulDefensivo.GetComponent<Image>().color = azulSelected;
            azulOfensivo.GetComponent<Image>().color = azulOriginal;
            rojoOfensivo.GetComponent<Image>().color = rojoSelected;
            rojoDefensivo.GetComponent<Image>().color = rojoOriginal;
            botonGuerraTotal.GetComponent<Image>().color = guerraOriginal;
            modoActual = 2;
            block = true;
            foreach (AgentNPC a in EquipoRojo)
            {
                a.Ofensivo = true;
            }
            foreach (AgentNPC a in EquipoAzul)
            {
                a.Ofensivo = false;
            }
            Reset();
        }
    }

    public void RojoDefensivo()
    {
        

        if (modoActual != 1 && !block)
        {
            azulOfensivo.GetComponent<Image>().color = azulSelected;
            azulDefensivo.GetComponent<Image>().color = azulOriginal;
            rojoDefensivo.GetComponent<Image>().color = rojoSelected;
            rojoOfensivo.GetComponent<Image>().color = rojoOriginal;
            botonGuerraTotal.GetComponent<Image>().color = guerraOriginal;

            modoActual = 1;
            block = true;
            foreach (AgentNPC a in EquipoAzul)
            {
                a.Ofensivo = true;
            }
            foreach (AgentNPC a in EquipoRojo)
            {
                a.Ofensivo = false;
            }
            Reset();
        }
    }

    public void BotonGuerraTotal()
    {
        if (modoActual != 3 && !block)
        {
            azulOfensivo.GetComponent<Image>().color = azulSelected;
            azulDefensivo.GetComponent<Image>().color = azulOriginal;
            rojoDefensivo.GetComponent<Image>().color = rojoOriginal;
            rojoOfensivo.GetComponent<Image>().color = rojoSelected;
            botonGuerraTotal.GetComponent<Image>().color = guerraSelected;

            block = true;
            foreach (AgentNPC a in EquipoAzul)
            {
                a.Ofensivo = true;
            }
            foreach (AgentNPC a in EquipoRojo)
            {
                a.Ofensivo = true;
            }
            modoActual = 3;
            OfensivaTotal();
        }    
    }

    public void ModeDebug()
    {
        if(!deb)
        {
            debug.GetComponent<Image>().color = debugSelected;
        }
        else
        {
            debug.GetComponent<Image>().color = debugOriginal;
        }
        deb = !deb;
    }

    public void Estandar()
    {
        estandar.GetComponent<Image>().color = tacticaSelected;
        agrupar.GetComponent<Image>().color = tacticaOriginal;

        if (Tactica != Tactica.Estandar)
        {
            Tactica = Tactica.Estandar;
            Reset();
        }
    }

    public void Agrupar()
    {
        estandar.GetComponent<Image>().color = tacticaOriginal;
        agrupar.GetComponent<Image>().color = tacticaSelected;

        if (Tactica != Tactica.Agrupar)
        {
            Tactica = Tactica.Agrupar;
            Reset();
        }
    }

    public void OfensivaTotal()
    {
        estandar.GetComponent<Image>().color = tacticaOriginal;
        agrupar.GetComponent<Image>().color = tacticaOriginal;

        if (Tactica != Tactica.OfensivaTotal)
        {
            Tactica = Tactica.OfensivaTotal;

            Reset();
        }
    }

    private void OnDrawGizmos()
    {
        if (deb)
        {
            if (Input.GetKey(KeyCode.G))
            {
                Debug.Log("HOLA");
                if (InfluenceMap.Grid != null)
                {
                    Gizmos.color = Color.blue;
                    for (int i = 0; i < InfluenceMap.Grid.Nodes; i++)
                    {
                        Gizmos.DrawLine(InfluenceMap.Grid.Map[i, 0].Posreal, InfluenceMap.Grid.Map[i, InfluenceMap.Grid.Nodes - 1].Posreal);
                        Gizmos.DrawLine(InfluenceMap.Grid.Map[0, i].Posreal, InfluenceMap.Grid.Map[InfluenceMap.Grid.Nodes - 1, i].Posreal);
                    }
                    for (int i = 0; i < InfluenceMap.Grid.Nodes; i++)
                    {
                        for (int j = 0; j < InfluenceMap.Grid.Nodes; j++)
                        {

                            if (InfluenceMap.Grid.Map[i, j].Obstacle)
                            {
                                Gizmos.color = Color.cyan;
                                Gizmos.DrawSphere(InfluenceMap.Grid.Map[i, j].Posreal, 1);
                            }
                            else if (InfluenceMap.Grid.Map[i, j].Bosque)
                            {
                                Gizmos.color = Color.green;
                                Gizmos.DrawSphere(InfluenceMap.Grid.Map[i, j].Posreal, 1);
                            }
                            else if (InfluenceMap.Grid.Map[i, j].Tierra)
                            {
                                Gizmos.color = new Color(153f / 255f, 101f / 255f, 21f / 255f, 1); //BROWN
                                Gizmos.DrawSphere(InfluenceMap.Grid.Map[i, j].Posreal, 1);
                            }
                        }
                    }
                }
            }
            foreach (AgentNPC a in listaPersonajes)
            {
                if (Input.GetKey(KeyCode.P))
                {

                    Vector3 aux = new Vector3(-1, 0, 0);
                    if (a.Path != null)
                    {
                        if (Input.GetKey(KeyCode.P))
                        {
                            foreach (Vector3 pos in a.Path.Positions)
                            {
                                if (aux.x == -1)
                                {
                                    aux = pos;
                                    continue;
                                }

                                if ((a.gameObject.CompareTag("Equipo Azul")) || (a.tag == "Patrullero Equipo Azul"))
                                {
                                    Gizmos.color = Color.blue;
                                    Gizmos.DrawLine(aux, pos);
                                    aux = pos;

                                }
                                else if ((a.tag == "Equipo Rojo") || (a.tag == "Patrullero Equipo Rojo"))
                                {
                                    Gizmos.color = Color.red;
                                    Gizmos.DrawLine(aux, pos);
                                    aux = pos;
                                }
                                Gizmos.DrawSphere(pos, 1);
                            }
                        }
                    }
                }
                if (Input.GetKey(KeyCode.R))
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireSphere(a.Position, 10);
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(a.Position, a.Rango);
                }

                if (a.PathFinding != null)
                {
                    if (a.transform.CompareTag("Equipo Azul") || a.transform.CompareTag("Patrullero Equipo Azul"))
                    {
                        Gizmos.color = Color.blue;
                    }
                    else if (a.transform.CompareTag("Equipo Rojo") || a.transform.CompareTag("Patrullero Equipo Rojo"))
                    {
                        Gizmos.color = Color.red;
                    }
                    if (a.PathFinding.PosicionfinalDebug != Vector3.zero)
                    {
                        Gizmos.DrawLine(a.Position, a.PathFinding.PosicionfinalDebug);
                    }
                }
            }
        }
    }
}
