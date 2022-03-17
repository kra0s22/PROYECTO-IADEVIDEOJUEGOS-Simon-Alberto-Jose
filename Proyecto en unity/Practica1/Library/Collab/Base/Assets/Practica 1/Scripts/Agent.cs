using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : PhysicalBody
{
    [SerializeField]
    protected float interiorRadius;
    [SerializeField]
    protected float exteriorRadius;
    [SerializeField]
    protected float InteriorAngle;
    [SerializeField]
    protected float ExteriorAngle;

    public float InteriorRadius { get => interiorRadius; set => interiorRadius = value; }
    public float ExteriorRadius { get => exteriorRadius; set => exteriorRadius = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DrawGizmoInteriorRadius()
    {

    }



}
