using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Higiene : Sistema
{


    [Space]
    public Tiempos tiemposBañar;
    public Higiene() { }

    #region Actualizar y verificar

    public void Actualizar() {
         
            if (TamagotchiManager.GetGestorTamagotchi(id).estadosActuales.VerificarEstado(Estado.sucio.ToString())) {
                tiemposBañar.ActualizarTiempoActualSin();
                VerificarTiempoSinBañar();
            }
          
            VerificarNivel(Estado.sucio.ToString(),Estado.aseado.ToString(),Estado.impecable.ToString(),tiemposBañar);
        
    }

    public void VerificarTiempoSinBañar() {
        if (TamagotchiTiempoExtraTools.VerificarTiempoLimite(Tiempo.Diferencia(tiemposBañar.tiempoActualSin,ConfigTamagotchi.instance.configSalud.configHigiene.tiempoMaxSinBañar)))
        {
            TamagotchiManager.GetGestorTamagotchi(id).animo.Disminuir ( ConfigTamagotchi.instance.configAnimo.animoPorBañar );

            //copiaNivel = nivel;
            tiemposBañar.ResetearTiempos();
        }

    }
    #endregion

    #region Acciones
    public void Nacer() {
        tiemposBañar.ResetearTiempos();
       
    }



    public void Bañar() {
        tiemposBañar.ResetearTiempos();
        Incrementar(ConfigTamagotchi.instance.configSalud.configHigiene. higienePorBañar);
     
    }

    public void Enmugrar()
    {
        Disminuir( ConfigTamagotchi.instance.configSalud.configHigiene.perdidaHigienePorEnmugrar);
 
    }

    #endregion

    public enum Estado
    {

        impecable,//limpieza alta
        aseado,// limpieza media
        sucio,//limpieza baja

    }

 

    internal void Inicializar ( string id )
    {
        nivel = 1;
        this.id = id;
    }
}