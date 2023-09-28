using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class CargarCerdoMaterial : MonoBehaviour
{
    public Material material;
    [Range(0, 1)]
    public float sensible;
    [Range(0, 10)]
    public float escala;
    [Range(0, 1)]
    public float desplasamiento;
    public bool visitante;

    void Start()
    {
        if (material == null || visitante)
        {
            return;
        }
        string carga = MorionTools.Cargar("sensible");
        if (!carga.Equals(string.Empty))
        {
            sensible = float.Parse(carga);
            escala = float.Parse(MorionTools.Cargar("escala"));
            desplasamiento = float.Parse(MorionTools.Cargar("desplasamiento"));
        }
        else
        {
            CrearCerdo();
        }

        ActualizarMaterial();

    }

    public void ActualizarMaterial()
    {
        material.SetFloat("_Escala", escala);
        material.SetFloat("_Sensible", sensible);
        material.SetFloat("_Desplazamiento", desplasamiento);
    }

    public void CrearCerdo()
    {
        sensible        = Random.Range(0, 0.5f);
        escala          = Random.Range(0, 10f);
        desplasamiento  = Random.Range(0, 1);

        MorionTools.Guardar("sensible", sensible.ToString());
        MorionTools.Guardar("escala", escala.ToString());
        MorionTools.Guardar("desplasamiento", desplasamiento.ToString());
    }

    public void Update()
    {
        ActualizarMaterial();
    }

    public void SetValues(float sensible, float escala, float desplasamiento)
    {
        this.sensible = sensible;
        this.escala = escala;
        this.desplasamiento = desplasamiento;
    }
}
