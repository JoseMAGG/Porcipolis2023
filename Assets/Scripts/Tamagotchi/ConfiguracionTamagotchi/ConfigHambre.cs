using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ConfigHambre 
{

    public Tiempo tiempoMaxSinComer;

    public int tiempoMaxSinComerSec;

    [Range(0,0.25f)]
    public float hambrePorJugar;

    public void SetInitialValues () {

        tiempoMaxSinComerSec = TamagotchiTiempoExtraTools.TiempoASegundos ( tiempoMaxSinComer );
    }
}
