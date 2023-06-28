using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ConfigHidratacion 
{
    public Tiempo tiempoMaxSinHidratarse;

    //[HideInInspector]
    public int tiempoMaxSinHidratarseSec;

    
    [Range(0,0.25f)]
    public float hidratacionPorBaño;
    [Range(0, 0.3f)]
    public float deshidratacionPorJugar;
    [Range(0, 0.25f)]
    public float hidratacionPorBeber;

    public  void SetInitialValues ()
    {
        tiempoMaxSinHidratarseSec = TamagotchiTiempoExtraTools.TiempoASegundos ( tiempoMaxSinHidratarse );
    }
}
