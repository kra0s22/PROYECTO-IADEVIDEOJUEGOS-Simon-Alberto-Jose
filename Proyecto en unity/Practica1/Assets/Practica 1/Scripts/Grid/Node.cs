using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public Vector3 Posreal { get => posreal; set => posreal = value; }
    public float Cost { get => cost; set => cost = value; }
    public Vector3 Posplano { get => posplano; set => posplano = value; }
    public bool Obstacle { get => obstacle; set => obstacle = value; }
    public bool Bosque { get => bosque; set => bosque = value; }
    public bool Tierra { get => tierra; set => tierra = value; }
    public float InfluenciaRojo { get => influenciaRojo; set => influenciaRojo = value; }
    public float InfluenciaAzul { get => influenciaAzul; set => influenciaAzul = value; }

    private Vector3 posplano;
    private Vector3 posreal;
    private float cost;

    private bool obstacle;
    private bool bosque;
    private bool tierra;
    private float influenciaRojo;
    private float influenciaAzul;
}
