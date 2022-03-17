using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalBody : MonoBehaviour
{
    protected bool canMove;
    protected float mass;
    [SerializeField]
    protected Vector2 position;
    [SerializeField]
    protected double orientation;
    [SerializeField]
    protected Vector2 velocity;
    protected float rotation;
    [SerializeField]
    protected float maxVelocity;
    [SerializeField]
    protected float maxAcceleration;
    [SerializeField]
    protected float maxRotation;
    [SerializeField]
    protected float maxAngular;

    public bool CanMove { get => canMove; set => canMove = value; }
    public float Mass { get => mass; set => mass = value; }
    public Vector2 Position { get => position; set => position = value; }
    public double Orientation { get => orientation; set => orientation = value; }
    public Vector2 Velocity { get => velocity; set => velocity = value; }
    public float Rotation { get => rotation; set => rotation = value; }
    public float MaxVelocity { get => maxVelocity; set => maxVelocity = value; }
    public float MaxAcceleration { get => maxAcceleration; set => maxAcceleration = value; }
    public float MaxRotation { get => maxRotation; set => maxRotation = value; }
    public float MaxAngular { get => maxAngular; set => maxAngular = value; }

    // Start is called before the first frame update
    void Start()
    {
        Position = new Vector2(2.0f, 1.0f);
        Velocity = new Vector2(2.0f, 1.0f);
        MaxVelocity = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Heading()
    {

    }

    void PositionToAngle()
    {

    }

    


}
