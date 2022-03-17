using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private Collision collision;
    public void Start()
    {
        collision = gameObject.AddComponent<Collision>();
    }
    public Collision getCollision(Vector3 position, Vector3 moveAmount)
    {
        collision.Position = Vector3.zero;
        collision.Normal = Vector3.zero;
        RaycastHit[] hits = Physics.RaycastAll(position, moveAmount);
        bool hitDetected = false;
        foreach(RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag("Pared"))
            {
                collision.Position = hit.point;
                collision.Normal += hit.normal;
                hitDetected = true;
            }
        }
        if (hitDetected)  return collision;
        return null;
    }
}
