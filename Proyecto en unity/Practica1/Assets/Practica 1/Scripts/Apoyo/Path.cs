using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    private List<Vector3> positions;

    public List<Vector3> Positions { get => positions; set => positions = value; }

    public Path()
    {
        positions = new List<Vector3>();
    }
    public Vector3 getPosition(int position)
    {
        return Positions[position];
    }

}
