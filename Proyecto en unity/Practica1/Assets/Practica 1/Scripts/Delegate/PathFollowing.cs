using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : Arrive
{
    private int pathOffset = 1;
    private int targetParam = 0;
    private float radius = 0.5f;
    private Agent targetFicticio;

    public int TargetParam { get => targetParam; set => targetParam = value; }

    public override void Start()
    {
        base.Start();
        targetFicticio = gameObject.AddComponent<Agent>();
    }

    public void Reset()
    {
        targetParam = 0;
    }
    public override Steering getSteering(AgentNPC agent) {
        if (agent.Path.Positions.Count > 0)
        {
            if (TargetParam >= agent.Path.Positions.Count)
            {
                targetFicticio.Position = agent.Path.getPosition(TargetParam - 1);
                Target = targetFicticio;
                Target.ExteriorRadius = 3;
                Target.InteriorRadius = 1;
                Target.InteriorAngle = 10f;
                Target.ExteriorAngle = 150f;
                    
                Steering.Linear = base.getSteering(agent).Linear;
                return Steering;
            }
            Vector3 direction = agent.Path.Positions[TargetParam] - agent.Position;
            float distance = direction.magnitude;
            if (distance < radius)
            {
                TargetParam += pathOffset;
            }

            if (TargetParam >= agent.Path.Positions.Count)
            {
                targetFicticio.Position = agent.Path.getPosition(TargetParam - 1);
                Target = targetFicticio;
                Target.ExteriorRadius = 3;
                Target.InteriorRadius = 1;
                Steering.Linear = base.getSteering(agent).Linear;
                return Steering;
            }

            if (TargetParam + 1 < agent.Path.Positions.Count && TargetParam > 0)
            {
                if (((agent.Path.getPosition(TargetParam-1).x - agent.Path.getPosition(TargetParam + 1).x) == 0) || 
                    ((agent.Path.getPosition(TargetParam-1).z - agent.Path.getPosition(TargetParam + 1).z) == 0)) 
                {
                    targetFicticio.Position = agent.Path.getPosition(TargetParam);
                    Target = targetFicticio;
                    Target.ExteriorRadius = 0;
                    Target.InteriorRadius = 0;
                    Steering.Linear = base.getSteering(agent).Linear;
                    return Steering;
                }
            }
            if (TargetParam == 0 && agent.Path.Positions.Count > 1)
            {
                targetFicticio.Position = agent.Path.getPosition(TargetParam);
                Target = targetFicticio;
                Target.ExteriorRadius = 0;
                Target.InteriorRadius = 0;
                Steering.Linear = base.getSteering(agent).Linear;
                return Steering;
            }
            targetFicticio.Position = agent.Path.getPosition(TargetParam);
            Target = targetFicticio;
            Target.ExteriorRadius = 3;
            Target.InteriorRadius = 1;
            Steering.Linear = base.getSteering(agent).Linear;
            return Steering;

        }
        targetParam = 0;
        Steering.Linear = -agent.Velocity;
        Steering.Angular = 0;
        return Steering;
    }

    private void OnDestroy()
    {
        Destroy(targetFicticio);
    }
}
