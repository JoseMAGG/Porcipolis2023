using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ConfigAnimo 
{
    public Tiempo tiempoMaxSinJugar;
    public Tiempo tiempoMaxSinConsentir;

    public int tiempoMaxSinJugarSec;
    public int tiempoMaxSinConsentirSec;
    [Space]
    [Range(0, 0.5f)]
    public float animoPorHambre;
    [Space]
    [Range(0, 0.5f)]
    public float animoPorJugar;
    [Space]
    [Range(0, 0.5f)]
    public float animoPorConsentir;
    [Space]
    [Range(0, 0.5f)]
    public float animoPorEnfermar;
    [Space]
    [Range(0, 0.5f)]
    public float animoPorBañar;
    [Space]
    [Range(0, 0.5f)]
    public float animoPorSanar;
    [Space]
    [Range(0, 0.5f)]
    public float animoPorDormir;

    public  void SetInitialValues ()
    {
        tiempoMaxSinJugarSec = TamagotchiTiempoExtraTools.TiempoASegundos( tiempoMaxSinJugar );
        tiempoMaxSinConsentirSec = TamagotchiTiempoExtraTools.TiempoASegundos( tiempoMaxSinConsentir );

    }
}
