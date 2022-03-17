using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State
{
    public override void Execute()
    {
        float distancia = (Agent.EnemigoMasCercano.transform.position - Agent.Position).magnitude;
        if (distancia > Agent.Rango)
        {
            if (!(Agent.SteeringBehaviour is SeekLWYG)) 
            {
                Agent.SteeringBehaviour = gameObject.AddComponent<SeekLWYG>();
                Agent.SteeringBehaviour.Target = gameObject.AddComponent<Agent>();
                Agent.SteeringBehaviour.Target.Position = Agent.EnemigoMasCercano.Position;
            }

        }
        else if (Agent.Velocity.magnitude > 0)
        {
            Destroy(Agent.SteeringBehaviour);
            Agent.Velocity = Vector3.zero;
        }
        else
        {

            //Variables
            float daño;
            float fuerza;
            float fuerzaTerreno = 0;
            bool bosque = false;
            bool tierra = false;
            
            //Calculo de terreno
            Ray ray = new Ray();
            ray.origin = new Vector3(Agent.Position.x, Agent.Position.y + 5, Agent.Position.z);
            ray.direction = new Vector3(0, -1, 0);
            RaycastHit[] hit = Physics.RaycastAll(ray, 10);

            foreach (RaycastHit h in hit)
            {
                if (h.transform.CompareTag("Bosque"))
                {
                    bosque = true;
                }
                else if (h.transform.CompareTag("Tierra"))
                {
                    tierra = true;
                }
            }

            //Comprobar ofensivo/Defensivo
            if (Agent.Ofensivo)
            {
                if (bosque)
                {
                    fuerzaTerreno = Agent.FuerzaBosqueAtacante;
                }
                else if (tierra)
                {
                    fuerzaTerreno = Agent.FuerzaTierraAtacante;
                }

            }
            else
            {
                if (bosque)
                {
                    fuerzaTerreno = Agent.FuerzaBosqueDefensor;
                }
                else if (tierra)
                {
                    fuerzaTerreno = Agent.FuerzaTierraDefensor;
                }
            }
            fuerza = fuerzaTerreno;


            //Calculo de la fuerza
            switch (Agent.EnemigoMasCercano.GetType().Name)
            {
                case "Ranged":
                    if (Agent.Ofensivo)
                    {
                        fuerza *= Agent.FuerzaVsRanged;
                    }                    
                    break;
                case "Mele":
                    if (Agent.Ofensivo)
                    {
                        fuerza *= Agent.FuerzaVsMele;
                    }
                    break;
                case "TodoTerreno":
                    if (Agent.Ofensivo)
                    {
                        fuerza *= Agent.FuerzaVsTodoTerreno;
                    }
                    break;
                default:
                    fuerza = 1;
                    break;
            }


            //Calculo del daño final
            if (Random.Range(0, 100) == 100) //CRITICO
            {
                daño = 50; 
            }
            else
            {
                daño = fuerza * Random.Range(0, 50) / 100 + 0.5f;
                if (daño <= 1 / 10)
                {
                    daño = Random.Range(1 / 10, 1 / 5);
                }
            }

            Agent.EnemigoMasCercano.SaludActual -= daño;

            if (Agent.EnemigoMasCercano.SaludActual <= 0)
            {
                Agent.EnemigoMasCercano.Muerto = true;
                Agent.EnemigoMasCercano.MaxVelocity = Agent.EnemigoMasCercano.MaxVelocityInicial; // para resetear la velocidad inicial al respawnear
                Agent.EnemigoMasCercano.ContadorMuerte = 5; // SEGUNDOS PARA REAPARECER
            }
        }
    }
}
