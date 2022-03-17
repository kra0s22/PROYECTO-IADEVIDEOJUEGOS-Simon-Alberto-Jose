using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPlayer : Agent
{
    private void Start()
    {
        Position = transform.position;
        MaxVelocity = 5;
    }
    public void Update()
    {
        int x = 0, z = 0;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            z = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            z = -1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            x = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            x = 1;
        }
        Velocity = new Vector3(x, 0, z) * MaxVelocity;
        Position += Velocity * Time.deltaTime;
        transform.position = Position;
        if (x != 0 || z != 0)
            Orientation = Mathf.Atan2(Velocity.x, Velocity.z) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, Orientation, 0);
    }
}
