using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalBody : MonoBehaviour
{
    private Vector3 position;
    private float orientation;
    private Vector3 velocity;
    private float rotation;
    private float maxVelocity;
    private float maxVelocityInicial;
    private float maxAcceleration;
    [SerializeField]
    private float maxRotation;
    [SerializeField]
    private float maxAngular;
    private float saludMaxima;
    private float saludActual;
    [SerializeField]
    private float rango;
    private bool muerto;
    private float contadorMuerte;
    private Vector3 deadPosition;
    private bool respawned;
    private bool ofensivo;
    private float fuerzaVsRanged;
    private float fuerzaVsMele;
    private float fuerzaVsTodoTerreno;
    private float fuerzaBosqueAtacante;
    private float fuerzaBosqueDefensor;
    private float fuerzaTierraAtacante;
    private float fuerzaTierraDefensor;

    private float resistenciaBosque;
    private float resistenciaTierra;
    public float Orientation { get => orientation; set => orientation = value; }
    public Vector3 Velocity { get => velocity; set => velocity = value; }
    public float Rotation { get => rotation; set => rotation = value; }
    public float MaxVelocity { get => maxVelocity; set => maxVelocity = value; }
    public float MaxAcceleration { get => maxAcceleration; set => maxAcceleration = value; }
    public float MaxRotation { get => maxRotation; set => maxRotation = value; }
    public float MaxAngular { get => maxAngular; set => maxAngular = value; }
    public Vector3 Position { get => position; set => position = value; }
    public float SaludMaxima { get => saludMaxima; set => saludMaxima = value; }
    public float SaludActual { get => saludActual; set => saludActual = value; }
    public float Rango { get => rango; set => rango = value; }
    public float ResistenciaBosque { get => resistenciaBosque; set => resistenciaBosque = value; }
    public float ResistenciaTierra { get => resistenciaTierra; set => resistenciaTierra = value; }
    public bool Muerto { get => muerto; set => muerto = value; }
    public float ContadorMuerte { get => contadorMuerte; set => contadorMuerte = value; }
    public Vector3 DeadPosition { get => deadPosition; set => deadPosition = value; }
    public bool Respawned { get => respawned; set => respawned = value; }
    public bool Ofensivo { get => ofensivo; set => ofensivo = value; }
    public float FuerzaVsRanged { get => fuerzaVsRanged; set => fuerzaVsRanged = value; }
    public float FuerzaVsMele { get => fuerzaVsMele; set => fuerzaVsMele = value; }
    public float FuerzaVsTodoTerreno { get => fuerzaVsTodoTerreno; set => fuerzaVsTodoTerreno = value; }
    public float FuerzaBosqueAtacante { get => fuerzaBosqueAtacante; set => fuerzaBosqueAtacante = value; }
    public float FuerzaBosqueDefensor { get => fuerzaBosqueDefensor; set => fuerzaBosqueDefensor = value; }
    public float FuerzaTierraAtacante { get => fuerzaTierraAtacante; set => fuerzaTierraAtacante = value; }
    public float FuerzaTierraDefensor { get => fuerzaTierraDefensor; set => fuerzaTierraDefensor = value; }
    public float MaxVelocityInicial { get => maxVelocityInicial; set => maxVelocityInicial = value; }
}
