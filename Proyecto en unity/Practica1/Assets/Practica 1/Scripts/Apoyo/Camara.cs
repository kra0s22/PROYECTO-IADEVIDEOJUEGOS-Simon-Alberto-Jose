using UnityEngine;
using System.Collections;
public class Camara : MonoBehaviour
{
    
    float mainSpeed = 50.0f; //regular speed
    float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
    float maxShift = 1000.0f; //Maximum speed when holdin gshift
    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun = 1.0f;
    void Update()
    {
      
        Vector3 p = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            totalRun += Time.deltaTime;
            p = p * totalRun * shiftAdd;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
        }
        else
        {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p = p * mainSpeed;
        }
        p = p * Time.deltaTime;
        Vector3 newPosition = transform.position;
       
        transform.Translate(p);
        newPosition.x = transform.position.x;
        newPosition.z = transform.position.z;
        newPosition.y = transform.position.y;
        transform.position = newPosition;


        if (Input.GetKey(KeyCode.Q))
        {
            Vector3 rotacion = transform.eulerAngles;
            rotacion = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 1, transform.eulerAngles.z);
            transform.eulerAngles = rotacion;
        }

        if (Input.GetKey(KeyCode.E))
        {
            Vector3 rotacion = transform.eulerAngles;
            rotacion = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 1, transform.eulerAngles.z);
            transform.eulerAngles = rotacion;
        }

    }
    private Vector3 GetBaseInput()
    { 

        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, -1, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        return p_Velocity;
    }



}
