﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargarCerdoMaterial : MonoBehaviour
{
    public Material material;
    [Range(0, 1)]
    public float sensible;
    [Range(0, 10)]
    public float escala;
    [Range(0, 1)]
    public float desplasamiento;

    void Start()
    {
        if (material == null)
        {
            return;
        }
        sensible       = float.Parse(MorionTools.Cargar("sensible"));
        escala         = float.Parse(MorionTools.Cargar("escala"));
        desplasamiento = float.Parse(MorionTools.Cargar("desplasamiento"));
        if ((sensible+escala+desplasamiento)==0)
        {
            CrearCerdo();
        }


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


    void Update()
    {
        
    }
}
