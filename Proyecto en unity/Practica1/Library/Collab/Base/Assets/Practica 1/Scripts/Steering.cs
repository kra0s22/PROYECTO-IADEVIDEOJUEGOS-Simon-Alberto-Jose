using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    protected float angular;
    protected Vector2 linear;

    public Vector2 Linear { get => linear; set => linear = value; }
    public float Angular { get => angular; set => angular = value; }
}
