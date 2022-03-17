using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{
    Agent lider;
    private int formationPosition;
    Agent targetFicticio;
    Slot slot;

    public Agent Lider { get => lider; set => lider = value; }
    public Slot Slot { get => slot; set => slot = value; }
    public int FormationPosition { get => formationPosition; set => formationPosition = value; }

    public virtual void Start()
    {
        targetFicticio = gameObject.AddComponent<Agent>();
    }
    public Vector3 RelativeOrientation()
    {
        return new Vector3(Mathf.Cos(lider.Orientation * Mathf.Deg2Rad) * Slot.SlotPosition.x + Mathf.Sin(lider.Orientation * Mathf.Deg2Rad) * Slot.SlotPosition.z, 0, -Mathf.Sin(Lider.Orientation * Mathf.Deg2Rad) * Slot.SlotPosition.x + Mathf.Cos(Lider.Orientation * Mathf.Deg2Rad) * Slot.SlotPosition.z);
    }
    public Agent calculateTarget(AgentNPC agent)
    {
        targetFicticio.Position = lider.Position + RelativeOrientation();
        targetFicticio.InteriorRadius = 0;
        targetFicticio.ExteriorRadius = 5;
        targetFicticio.InteriorAngle = 0;
        targetFicticio.ExteriorAngle = 50;
        targetFicticio.Orientation = Lider.Orientation + Slot.Orientation;
        return targetFicticio;
    }
}
