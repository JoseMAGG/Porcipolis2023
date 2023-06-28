using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ConfigEnergia 
{
    [Range(0.00001f,0.01f)]
    public float perdidaEnergiaPorRealizarActividades;

    [Range(0.00001f, 0.01f)]
    public float pedidaEnergiaPorEnfermar;

}
