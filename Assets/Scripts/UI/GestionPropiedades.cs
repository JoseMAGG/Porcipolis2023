using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPropiedades : MonoBehaviour
{
    public GameObject fondo;
    public Objeto[] objetos;

    public static GestionPropiedades singleton;

    void Awake()
    {
        singleton = this;
    }
    
    public void Activar(int cual)
    {

    }
}

[System.Serializable]
public class Objeto
{
    public PlowUI plow;
    public string titulo;
    public string descripcion;
    public Sprite icono;
}