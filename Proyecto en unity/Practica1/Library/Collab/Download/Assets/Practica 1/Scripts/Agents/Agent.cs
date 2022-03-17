using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : PhysicalBody
{
    [SerializeField]
    private float interiorRadius;
    [SerializeField]
    private float exteriorRadius;
    [SerializeField]
    private float interiorAngle;
    [SerializeField]
    private float exteriorAngle;

    public float InteriorRadius { get => interiorRadius; set => interiorRadius = value; }
    public float ExteriorRadius { get => exteriorRadius; set => exteriorRadius = value; }
    public float InteriorAngle { get => interiorAngle; set => interiorAngle = value; }
    public float ExteriorAngle { get => exteriorAngle; set => exteriorAngle = value; }

    /*public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interiorRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, exteriorRadius);
    }*/
}