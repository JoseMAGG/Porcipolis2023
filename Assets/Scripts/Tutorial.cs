using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject[] tut;
    public GameObject botones;
    public int pagActual;

    public string scene;
    public string value;

    public static Tutorial singleton;

    void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        scene = SceneManager.GetActiveScene().name;
        
        // Para verificar si el tutorial ya se había mostrado antes
        string value = MorionTools.Cargar(scene + "Tut");
        if(value != "1")
        {
            MorionTools.Guardar(scene + "Tut", "1");
            LlamarTutorial();
        }
    }

    // cuando abra el tutorial GUARDAR en playerfrefs, 
    // verificar si ya está guardado un 1 o algo

    void Update()
    {
        
    }

    public void LlamarTutorial()
    {
        pagActual = 0;
        for (int i = 0; i < tut.Length; i++)
        {
            tut[i].SetActive(i == 0);
        }
        botones.SetActive(true);
    }


    public void PasarPagina()
    {
        pagActual++;
        pagActual = pagActual > tut.Length - 1 ? tut.Length - 1 : pagActual;
        for (int i = 0; i < tut.Length; i++)
        {
            tut[i].SetActive(i == pagActual);
        }
    }


    public void DevolverPagina()
    {
        pagActual--;
        pagActual = pagActual < 0 ? 0 : pagActual;
        for (int i = 0; i < tut.Length; i++)
        {
            tut[i].SetActive(i == pagActual);
        }
        
    }

    public void CerrarTutorial()
    {
        for (int i = 0; i < tut.Length; i++)
        {
            tut[i].SetActive(false);
        }
        botones.SetActive(false);
    }

}
