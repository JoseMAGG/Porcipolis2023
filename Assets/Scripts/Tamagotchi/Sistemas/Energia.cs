using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Energia : Sistema
{

    public Energia ()
    {
        
    }

    public void Actualizar ()
    {
        if ( TamagotchiManager.GetGestorTamagotchi ( id ).estadosActuales.VerificarEstado ( CicloSueño.Estado.despierto.ToString () ) )
        {
            if ( TamagotchiManager.GetGestorTamagotchi ( id ).estadosActuales.VerificarEstado ( Estado.activo.ToString () ) )
            {
                DisminuirEnergia ();
            }
            else
            {
                AumentarEnergia ();
            }
            pivoteNivel = nivel;
        }
        else if ( TamagotchiManager.GetGestorTamagotchi ( id ).estadosActuales.VerificarEstado ( CicloSueño.Estado.dormido.ToString () ) )
        {
            Durmiendo ();
        }
        VerificarNivel ( Estado.cansado.ToString () , Estado.tranquilo.ToString () , Estado.energico.ToString () , null );
    }

    public void DisminuirEnergia ()
    {


        nivel = nivel - ConfigTamagotchi.instance.configEnergia.perdidaEnergiaPorRealizarActividades;
        if ( TamagotchiManager.GetGestorTamagotchi ( id ).estadosActuales.VerificarEstado ( Salud.Estado.enfermo.ToString () ) )
        {
            nivel = nivel - ConfigTamagotchi.instance.configEnergia.pedidaEnergiaPorEnfermar;
        }

        if ( nivel <= 0 )
            nivel = 0;

    }

    public void AumentarEnergia ()
    {
        nivel += ConfigTamagotchi.instance.configEnergia.perdidaEnergiaPorRealizarActividades;
        if ( nivel >= 1 )
            nivel = 1;
    }


    

    public void AumentarEnergia ( int actual , int max )
    {
        nivel = pivoteNivel + ( float ) actual / ( float ) max;
        if ( nivel >= 1 )
            nivel = 1;
    }

    public void Despertar ()
    {
        Actualizar ();
    }

    public void Nacer ()
    {

        TamagotchiManager.GetGestorTamagotchi ( id ).estadosActuales.AgregarEstado ( Estado.activo.ToString () );

        Actualizar ();

    }
    public void Jugar ()
    {
        TamagotchiManager.GetGestorTamagotchi ( id ).estadosActuales.EliminarAgregar ( Estado.inactivo.ToString () , Estado.activo.ToString () );

    }

    internal void Inicializar ( string id )
    {
        this.id = id;
        nivel = 1;
    }

    public void Dormir ()
    {
        TamagotchiManager.GetGestorTamagotchi ( id ).estadosActuales.EliminarAgregar ( Estado.activo.ToString () , Estado.inactivo.ToString () );

    }
    public void Durmiendo ()
    {
        AumentarEnergia ( TamagotchiManager.GetGestorTamagotchi ( id ).cicloSueño.tiempoDormidoSegundos ,
               ConfigTamagotchi.instance.configCicloSueño.tiempoDormidoMaxSegundos );
    }
    public void Descansar ()
    {
        TamagotchiManager.GetGestorTamagotchi ( id ).estadosActuales.EliminarAgregar ( Estado.activo.ToString () , Estado.inactivo.ToString () );
    }
    void BorrarEstadosActuales ()
    {
        TamagotchiManager.GetGestorTamagotchi ( id ).estadosActuales.EliminarEstado ( Estado.energico.ToString () );
        TamagotchiManager.GetGestorTamagotchi ( id ).estadosActuales.EliminarEstado ( Estado.tranquilo.ToString () );
        TamagotchiManager.GetGestorTamagotchi ( id ).estadosActuales.EliminarEstado ( Estado.cansado.ToString () );
    }

    public enum Estado
    {
        energico,
        tranquilo,
        cansado,

        inactivo, // Estado en el cual NOO está realizando actividades que demandan esfuerzo
        activo, // Estado en el cual está realizando actividades que demandan esfuerzo

    }
}
