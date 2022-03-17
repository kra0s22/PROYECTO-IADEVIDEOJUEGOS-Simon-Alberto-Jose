using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : PhysicalBody
{
    [SerializeField]
    private float interiorRadius; // Radio interior 
    [SerializeField]
    private float exteriorRadius; // Radio exterior
    [SerializeField]
    private float interiorAngle; // Radio angular interior
    [SerializeField]
    private float exteriorAngle; // Radio angular exterior
    private bool deb;

    public float InteriorRadius { get => interiorRadius; set => interiorRadius = value; }
    public float ExteriorRadius { get => exteriorRadius; set => exteriorRadius = value; }
    public float InteriorAngle { get => interiorAngle; set => interiorAngle = value; }
    public float ExteriorAngle { get => exteriorAngle; set => exteriorAngle = value; }
    public bool Deb { get => deb; set => deb = value; }

    // Calcula un vector orientación
    public Vector3 OrientationAsVector()
    {
        return new Vector3(Mathf.Sin(Orientation * Mathf.Deg2Rad), 0, Mathf.Cos(Orientation * Mathf.Deg2Rad));
    }
}