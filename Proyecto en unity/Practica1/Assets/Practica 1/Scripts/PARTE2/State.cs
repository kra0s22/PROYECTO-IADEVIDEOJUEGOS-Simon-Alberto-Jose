using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    private AgentNPC agent;
    public AgentNPC Agent { get => agent; set => agent = value; }

    public virtual void Start()
    {
        Agent = gameObject.GetComponent<AgentNPC>();
    }

    public virtual void Execute() { }
}
