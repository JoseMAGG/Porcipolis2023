using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escenas : MonoBehaviour
{
    public static Escenas singleton;

    private void Awake()
    {
        singleton = this;
    }

    public void CargarEscena(string esc)
    {
        SceneManager.LoadScene(esc);
    }
    
    public void Salir()
    {
        Application.Quit();
    }
}
