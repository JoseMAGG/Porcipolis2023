using System.Collections;
using UnityEngine;
using System;
[System.Serializable]
public class Edad : Sistema
{

  
    public Tiempo fechaNacimiento;
   
    public Tiempo tiempoDeVidaActual;


    [HideInInspector]
    public int tiempoDeVidaSegundos;


    public Edad () { }
    public void AsignarFechaNacimiento ()
    {
        fechaNacimiento.AsignarTiempoActual ();
        tiempoDeVidaActual = Tiempo.Diferencia ( fechaNacimiento , ConfigTamagotchi.instance.tiempoActual );
    }
    public void ActualizarTiempoDeVida ()
    {
        tiempoDeVidaActual   = Tiempo.Diferencia ( fechaNacimiento , ConfigTamagotchi.instance.tiempoActual );
        tiempoDeVidaSegundos = TamagotchiTiempoExtraTools.TiempoASegundos ( tiempoDeVidaActual );
        ActualizarNivelVida ();
        VerificarNivel ( Estado.joven.ToString () , Estado.adulto.ToString () , Estado.anciano.ToString () , null );
        VerificarTiempoLimiteDeVida ();
    }
    public void ActualizarNivelVida () => nivel = ( float ) tiempoDeVidaSegundos / ( float ) ConfigTamagotchi.instance.configEdad.tiempoDeVidaMaximoSegundos;
 
    void VerificarTiempoLimiteDeVida ()
    {
        if (TamagotchiTiempoExtraTools .VerificarTiempoLimite ( Tiempo.Diferencia ( tiempoDeVidaActual , ConfigTamagotchi.instance.configEdad.tiempoDeVidaMaximo ) ) )
        {
            Accion.EjecutarAccion ( Accion.accion.morir,id );
            Debug.Log ( "Peppa envejeció y murió" );
        }
    }
    public void Nacer ()
    {
        AsignarFechaNacimiento ();
        VerificarNivel ( Estado.joven.ToString () , Estado.adulto.ToString () , Estado.anciano.ToString () , null );
    }
    
    
    public enum Estado
    {
        joven,
        adulto,
        anciano,
    }

    internal void Inicializar ( string id )
    {
        this.id = id;
    }
}
