using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    Manager manager;
    InfluenceMap influenceMap;
    List<GameObject> list;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<Manager>();
        influenceMap = manager.GetComponent<InfluenceMap>();
        list = new List<GameObject>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (influenceMap.Actualizar)
        {
            if (list.Count > 0)
            {
                foreach (GameObject o in list)
                {
                    Destroy(o);
                }
            }

            for (int i = 0; i < influenceMap.Grid.Nodes; i++)
            {
                for (int j = 0; j < influenceMap.Grid.Nodes; j++)
                {
                    if (influenceMap.Grid.Map[i, j].InfluenciaAzul > influenceMap.Grid.Map[i, j].InfluenciaRojo)
                    {
                        Vector3 newPos = new Vector3(influenceMap.Grid.Map[i, j].Posreal.x + 200, influenceMap.Grid.Map[i, j].Posreal.y, influenceMap.Grid.Map[i, j].Posreal.z);
                        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        Material mat = sphere.GetComponent<Renderer>().material;
                        float influenciaFinal = influenceMap.Grid.Map[i, j].InfluenciaAzul - influenceMap.Grid.Map[i, j].InfluenciaRojo;
                        mat.color = new Color(0, 0, 1 - (influenciaFinal / 150), 1 - (influenciaFinal / 150));
                        sphere.transform.position = newPos;
                        Vector3 newScale = new Vector3(3, 1, 3);
                        sphere.transform.localScale = newScale;
                        list.Add(sphere);
                    }
                    else if (influenceMap.Grid.Map[i, j].InfluenciaRojo > influenceMap.Grid.Map[i, j].InfluenciaAzul)
                    {
                        Vector3 newPos = new Vector3(influenceMap.Grid.Map[i, j].Posreal.x + 200, influenceMap.Grid.Map[i, j].Posreal.y, influenceMap.Grid.Map[i, j].Posreal.z);
                        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        Material mat = sphere.GetComponent<Renderer>().material;
                        float influenciaFinal = influenceMap.Grid.Map[i, j].InfluenciaRojo - influenceMap.Grid.Map[i, j].InfluenciaAzul;
                        mat.color = new Color(1 - (influenciaFinal / 150), 0, 0, 1 - (influenciaFinal / 150));
                        sphere.transform.position = newPos;
                        Vector3 newScale = new Vector3(3, 1, 3);
                        sphere.transform.localScale = newScale;
                        list.Add(sphere);
                    }
                }
            }
        }
    }
}

