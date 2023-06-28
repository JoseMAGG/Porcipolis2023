using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ConfigHigiene 
{
    public Tiempo tiempoMaxSinBañar;

    [Range(0, 0.5f)]
    public float perdidaHigienePorEnmugrar;
    [Range(0, 0.5f)]
    public float higienePorBañar;
}
