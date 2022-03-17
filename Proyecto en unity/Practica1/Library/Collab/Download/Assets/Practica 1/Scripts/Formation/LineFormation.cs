using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFormation : MonoBehaviour
{

    Agent lider;
    int formationPosition;
    Agent targetFicticio;
    Slot slot;

    public Agent Lider { get => lider; set => lider = value; }
    public int FormationPosition { get => formationPosition; set => formationPosition = value; }

    private void Start()
    {
        targetFicticio = gameObject.AddComponent<Agent>();
        if(formationPosition == 1)
        {
            slot = new Slot(-90, new Vector3(-1, 0, -1));
        }
        if (formationPosition == 2)
        {
            slot = new Slot(90, new Vector3(1, 0, -1));
        }
        if (formationPosition == 3)
        {
            slot = new Slot(180, new Vector3(0, 0, -2));
        }
    }
    public Vector3 RelativeOrientation ()
    {
        return new Vector3(Mathf.Cos(lider.Orientation*Mathf.Deg2Rad) * slot.SlotPosition.x + -Mathf.Sin(lider.Orientation*Mathf.Deg2Rad) * slot.SlotPosition.z, 0, Mathf.Sin(Lider.Orientation*Mathf.Deg2Rad) * slot.SlotPosition.x + Mathf.Cos(Lider.Orientation*Mathf.Deg2Rad) * slot.SlotPosition.z);
    }
    public Agent calculateTarget(AgentNPC agent)
    {
        targetFicticio.Position = lider.Position + RelativeOrientation();
        targetFicticio.InteriorRadius = 0;
        targetFicticio.ExteriorRadius = 5;
        targetFicticio.Orientation = Lider.Orientation + slot.Orientation;
        return targetFicticio;
    }
}
