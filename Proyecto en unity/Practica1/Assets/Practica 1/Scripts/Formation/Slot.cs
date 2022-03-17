using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot
{
    private float orientation;
    private Vector3 slotPosition;

    public float Orientation { get => orientation; set => orientation = value; }
    public Vector3 SlotPosition { get => slotPosition; set => slotPosition = value; }

    public Slot(float orientation_, Vector3 slotPosition_)
    {
        orientation = orientation_;
        slotPosition = slotPosition_;
    }
}
