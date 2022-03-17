using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField]
    private Vector3 position;
    [SerializeField]
    private Vector3 normal;

    public Vector3 Position { get => position; set => position = value; }
    public Vector3 Normal { get => normal; set => normal = value; }
}
