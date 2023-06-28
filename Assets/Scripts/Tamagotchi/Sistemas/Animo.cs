using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Animo : Sistema
{


    public Tiempos tiemposJugar;
    public Tiempos tiemposConsentir;


    

    public Animo() {
      
    }

    public void Actualizar() {


        if ( TamagotchiManager.GetGestorTamagotchi ( id ).estadosActuales.VerificarEstado ( Hambre.Estado.hambriento.ToString () ) )
        {
            DisminuirAnimoPorHambre ();
        }


        ActualizarTiempoSinJugar ();
         ActualizarTiempoSinConsentir();                 
        

        VerificarNivel(Estado.triste.ToString(),Estado.tranquilo.ToString(),Estado.feliz.ToString(),null);

    }

    public void DisminuirAnimoPorHambre () {
        Disminuir ( ConfigTamagotchi.instance.configAnimo.animoPorHambre );
            }

    public void ActualizarTiempoSinJugar() {
        tiemposJugar.ActualizarTiempoActualSin();
        VerificarTiempoSinJugar();

    }

    public void ActualizarTiempoSinConsentir()
    {
        tiemposConsentir.ActualizarTiempoActualSin();
        VerificarTiempoSinConsentir();
    }

    public void VerificarTiempoSinConsentir() {
        if (TamagotchiTiempoExtraTools.VerificarTiempoLimite(Tiempo.Diferencia(tiemposConsentir.tiempoActualSin,
                                                                 ConfigTamagotchi.instance.configAnimo.tiempoMaxSinConsentir)))
        {
            Disminuir(ConfigTamagotchi.instance.configAnimo.animoPorConsentir);
            pivoteNivel = nivel;
            tiemposConsentir.ResetearTiempos();
        }
    }



    void VerificarTiempoSinJugar()
    {

        if (TamagotchiTiempoExtraTools.VerificarTiempoLimite(Tiempo.Diferencia(tiemposJugar.tiempoActualSin,
                                                                    ConfigTamagotchi.instance.configAnimo.tiempoMaxSinJugar)))
        {
            Disminuir( ConfigTamagotchi.instance.configAnimo.animoPorJugar );
            pivoteNivel = nivel;
            tiemposJugar.ResetearTiempos();
        }      
    }

   

    #region Accion metodos


    public void Jugar() {
        
        Incrementar( ConfigTamagotchi.instance.configAnimo.animoPorJugar );
        tiemposJugar.ResetearTiempos();

    }

   
    public void Nacer() {

        VerificarNivel(Estado.triste.ToString(), Estado.tranquilo.ToString(), Estado.feliz.ToString(),null);

    }

    internal void Inicializar ( string id )
    {
        this.id = id;
        nivel = 1;
    }

    public void Dormir()
    {

        Incrementar( ConfigTamagotchi.instance.configAnimo.animoPorDormir );
    

    }

    public void Bañar()
    {
        Incrementar( ConfigTamagotchi.instance.configAnimo.animoPorBañar );
    

    }

    public void Enfermar()
    {
   
        Disminuir( ConfigTamagotchi.instance.configAnimo.animoPorEnfermar );
      

    }

    public void Sanar()
    {
        Incrementar( ConfigTamagotchi.instance.configAnimo.animoPorSanar );
  

    }


    public void Despertar() {
        tiemposConsentir.ResetearTiempos();
        tiemposJugar.ResetearTiempos();
    }
    public void Consentir()
    {
        Incrementar( ConfigTamagotchi.instance.configAnimo.animoPorConsentir );


        tiemposConsentir.ResetearTiempos();
    }

    #endregion

    public enum Estado {
        
        feliz,
        tranquilo,
        triste,

    }
}
