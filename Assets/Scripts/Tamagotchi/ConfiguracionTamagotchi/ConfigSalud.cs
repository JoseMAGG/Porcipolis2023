using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ConfigSalud 
{
    
    public ConfigHigiene configHigiene;

    public Tiempo tiempoMaxSinCurar;

    [Range(0,1f)]
    public float saludPorCurar;
    [Range(0, 1f)]
    public float perdidaSaludPorEnfermar;
}
