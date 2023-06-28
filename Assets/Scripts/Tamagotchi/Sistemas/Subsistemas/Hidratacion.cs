using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hidratacion : Sistema
{

  
    [Space]
    public Tiempos tiemposHidratacion;



    public Hidratacion()
    {
        pivoteNivel = 1;
        nivel = 1;
    }

    #region Actualizar

    public void Actualizar()
    {
        tiemposHidratacion.ActualizarTiempos();
        ActualizarNivel();
        VerificarNivel(Estado.deshidratado.ToString(),Estado.hidratado.ToString(),null);
      
    }

    public void ActualizarNivel() => nivel = pivoteNivel - (float)tiemposHidratacion.tiempoActualSinSec / (float)ConfigTamagotchi.instance.configAlimentacion.configHidratacion.tiempoMaxSinHidratarseSec;
    

    #endregion

    #region Acciones del sistema

    public void Nacer()
    {
        tiemposHidratacion.ResetearTiempos();
    }

  

    public void Bañar() {
        Incrementar(ConfigTamagotchi.instance.configAlimentacion.configHidratacion.hidratacionPorBaño);
        pivoteNivel = nivel;
        tiemposHidratacion.ResetearTiempos();
    }

    internal void Inicializar ( string id )
    {
        this.id = id;
    }

    public void Jugar() {
        Disminuir(ConfigTamagotchi.instance.configAlimentacion.configHidratacion.deshidratacionPorJugar);
        pivoteNivel = nivel;
        tiemposHidratacion.ResetearTiempos();
    }

    public void Beber() {

        Incrementar(ConfigTamagotchi.instance.configAlimentacion.configHidratacion.hidratacionPorBeber);
        pivoteNivel = nivel;
        tiemposHidratacion.ResetearTiempos();

    }

    #endregion


    public enum Estado
    {
       hidratado,
       deshidratado
    }

}