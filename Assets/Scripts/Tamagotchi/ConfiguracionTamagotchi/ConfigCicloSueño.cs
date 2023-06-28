using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ConfigCicloSueño 
{
    //[HideInInspector]
    public Tiempo tiempoMaximoDespierto; // El tiempo maximo despierto
    //[HideInInspector]
    public Tiempo tiempoMaximoDormido; // El tiempo maximo dormido
    [HideInInspector]
    public int tiempoDespiertoMaxSegundos;
    [HideInInspector]
    public int tiempoDormidoMaxSegundos;

    [HideInInspector]
    public int totalCicloSec;

    public void SetInitialValues () {
        tiempoDespiertoMaxSegundos = TamagotchiTiempoExtraTools.TiempoASegundos ( tiempoMaximoDespierto );
        tiempoDormidoMaxSegundos = TamagotchiTiempoExtraTools.TiempoASegundos ( tiempoMaximoDormido );
        totalCicloSec = tiempoDespiertoMaxSegundos + tiempoDormidoMaxSegundos;
    }
}
