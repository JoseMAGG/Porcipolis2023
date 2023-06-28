using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GestorEconomia : MonoBehaviour
{
    public ListaRecursos recursos;
    public UnityEvent cambioRecursos;
    public static GestorEconomia singleton;
    public Text[] txtRecursos;

    private void Awake()
    {
        singleton = this;
    }
    private void Start()
    {
        CargarDatos();
    }

    public void ActualizarUI()
    {
        if (txtRecursos == null || txtRecursos.Length <1)
        {
            return;
        }
        for (int i = 0; i < recursos.recursos.Length; i++)
        {
            if (txtRecursos[i] !=null)
            {
                txtRecursos[i].text = recursos.recursos[i].cantidad.ToString("00");
            }
        }
    }

    public void SumarRecurso(int tipoObjeto, int cantidad)
    {
        recursos.recursos[tipoObjeto].cantidad += cantidad;
        cambioRecursos.Invoke();
        GuardarDatos();
        ActualizarUI();
    }

    public bool VerificarRecurso(int tipoRec, int cantidad)
    {
        return recursos.recursos[tipoRec].cantidad >= cantidad;
    }

    public bool UsarRecurso(int tipoObjeto, int cantidad)
    {
        if (recursos.recursos[tipoObjeto].cantidad  < cantidad)
        {
            print("No hay suficiente " + recursos.recursos[tipoObjeto].nombre);

            return false;
        }
        else
        {
            recursos.recursos[tipoObjeto].cantidad -= cantidad;
        }
        cambioRecursos.Invoke();
        GuardarDatos();
        ActualizarUI();
        return true;
    }


    public void GuardarDatos()
    {
        string datos = JsonUtility.ToJson(recursos);
        MorionTools.Guardar("recursos", datos);
        //print(datos);
        
    }
    
    public void CargarDatos()
    {
        string cargados = MorionTools.Cargar("recursos");
        if (cargados.Length < 4)
        {
            GuardarDatos();
        }
        else
        {
            recursos = JsonUtility.FromJson<ListaRecursos>(cargados);
        }
        ActualizarUI();

    }

}

[System.Serializable]
public class Recurso
{
    public string nombre;
    public int cantidad;

    public Recurso(string nombre, int cantidad)
    {
        this.nombre = nombre;
        this.cantidad = cantidad;
    }

    public Recurso()
    {
        nombre = "cosa";
        cantidad = 0;
    }

}

[System.Serializable]
public class ListaRecursos
{
    public Recurso[] recursos;
}

