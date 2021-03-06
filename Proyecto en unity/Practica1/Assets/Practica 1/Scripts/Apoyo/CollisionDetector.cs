using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private Collision collision;
    private Vector3 posicion; //DEBUG
    private bool hitDetected; //DEBUG


    private void Awake()
    {
        collision = gameObject.AddComponent<Collision>();
    }
    public Collision getCollision(Vector3 position, Vector3 moveAmount)
    {
        posicion = position;

        collision.Position = Vector3.zero;
        collision.Normal = Vector3.zero;

        RaycastHit[] hits = Physics.RaycastAll(position, moveAmount, 20);
        hitDetected = false;
        foreach(RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag("Pared"))
            {
                collision.Position = new Vector3(hit.point.x, 0, hit.point.z);
                collision.Normal = new Vector3(hit.normal.x, 0, hit.normal.z);                
                hitDetected = true;
            }
        }

        if (hitDetected)  return collision;
        return null;
    }

}
