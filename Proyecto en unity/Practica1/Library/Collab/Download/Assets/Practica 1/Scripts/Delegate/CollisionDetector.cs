using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private Collision collision;
    private float distancia = 20;
    public void Start()
    {
        collision = gameObject.AddComponent<Collision>();
    }
    public Collision getCollision(Vector3 position, Vector3 moveAmount)
    {
        RaycastHit[] hits = Physics.RaycastAll(position, moveAmount, distancia);
        bool hitDetected = false;
        foreach(RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag("Pared"))
            {
                Debug.Log("Hit" + hit.point + hit.normal);
                collision.Position = new Vector3 (hit.point.x, 0, hit.point.z);
                collision.Normal += hit.normal;
                hitDetected = true;
            }
        }
        if (hitDetected)  return collision;
        return null;
    }
}
