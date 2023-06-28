using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConfigEdad 
{
    [Space]
    public Tiempo tiempoDeVidaMaximo;

    [Space]
    public int  tiempoDeVidaMaximoSegundos;

    public void SetInitialValues () { 
        tiempoDeVidaMaximoSegundos =TamagotchiTiempoExtraTools .TiempoASegundos ( ConfigTamagotchi.instance.configEdad.tiempoDeVidaMaximo );

    }
}
