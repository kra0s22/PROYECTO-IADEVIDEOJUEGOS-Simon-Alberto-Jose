using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public GameObject panelPrincipal;
    public GameObject panelComenzar;
    public GameObject panelInstrucciones;
    public GameObject panelInformacion;

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void Comenzar()
    {
        panelComenzar.SetActive(true);
        panelPrincipal.SetActive(false);
    }

    public void Parte1()
    {
        SceneManager.LoadScene(1);
    }

    public void PathFinding()
    {
        SceneManager.LoadScene(2);
    }

    public void Parte2()
    {
        SceneManager.LoadScene(3);
    }

    public void volverComenzar()
    {
        panelComenzar.SetActive(false);
        panelPrincipal.SetActive(true);
    }

    public void Instrucciones()
    {
        panelPrincipal.SetActive(false);
        panelInstrucciones.SetActive(true);
    }

    public void volverInstrucciones()
    {
        panelInstrucciones.SetActive(false);
        panelPrincipal.SetActive(true);
    }

    public void Informacion()
    {
        panelPrincipal.SetActive(false);
        panelInformacion.SetActive(true);
    }

    public void volverInformacion()
    {
        panelInformacion.SetActive(false);
        panelPrincipal.SetActive(true);
    }
}
