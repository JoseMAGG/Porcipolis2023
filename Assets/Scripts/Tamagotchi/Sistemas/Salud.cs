using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Salud :Sistema
{
  
    public Higiene higiene = new Higiene();
    public  Tiempos sanar;

    public Salud () : base(){ 
    
    }
    


    public void Actualizar()
    {

        if ( TamagotchiManager.GetGestorTamagotchi ( id ).estadosActuales.VerificarEstado ( Hambre.Estado.hambriento.ToString () ) )
        {
            DisminuirSaludPorHambre ();
        }





        if (TamagotchiManager.GetGestorTamagotchi ( id ).estadosActuales.VerificarEstado(Estado.enfermo.ToString()))
        {
            sanar.ActualizarTiempoActualSin();
            VerificarTiempoEnfermo();
        }
        higiene.Actualizar();
        VerificarNivel(Estado.enfermo.ToString(),Estado.sano.ToString(),sanar);

    }


    public void DisminuirSaludPorHambre () {
        nivel = (1-TamagotchiManager.GetGestorTamagotchi ( id ).alimentacion.hambre.nivel) / 0.33334f;
    }


    public void VerificarTiempoEnfermo()
    {
        if (TamagotchiTiempoExtraTools.VerificarTiempoLimite(Tiempo.Diferencia(sanar.tiempoActualSin, ConfigTamagotchi.instance.configSalud.tiempoMaxSinCurar)))
        {

            TamagotchiManager.GetGestorTamagotchi ( id ).animo.Disminuir(ConfigTamagotchi.instance.configAnimo.animoPorEnfermar);
            //copiaNivel = nivel;
            sanar.ResetearTiempos();
        }

    }



    #region Acciones metodos

    public void Nacer() {
        higiene.Nacer();
           
    }

    public void Bañar() {
        higiene.Bañar();
    }
    

    public void Sanar() {
        Incrementar(ConfigTamagotchi.instance.configSalud.saludPorCurar);
     

    }
    public void  Enfermar() {
        sanar.ResetearTiempos();
        Disminuir(ConfigTamagotchi.instance.configSalud.perdidaSaludPorEnfermar);
    

    }



    public void Enmugrar() {
        higiene.Enmugrar();
    }

    #endregion

    public enum Estado {
     
        sano,
        enfermo
    }

    internal void Inicializar ( string id )
    {
        this.id = id;
        nivel = 1;
        higiene.Inicializar (id);
    }
}
