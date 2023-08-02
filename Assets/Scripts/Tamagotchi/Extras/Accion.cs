using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Accion
{

    

   

    public  static void EjecutarAccion(accion accion,string id)
    {
        GestorTamagotchi gestorTama = TamagotchiManager.GetGestorTamagotchi ( id );


        switch (accion)
        {

            #region Accion.nacer
            case accion.nacer:
                gestorTama.estadosActuales.AgregarEstado(GestorTamagotchi.estados.vivo.ToString());
                gestorTama.edad.Nacer();
                gestorTama.salud.Nacer();
                gestorTama.alimentacion.Nacer();
                gestorTama.animo.Nacer();
                gestorTama.cicloSueño.Nacer();
                gestorTama.energia.Nacer();

                gestorTama.animCerdo.Activo ();
                
                break;

            #endregion

            #region Accion.dormir
            case accion.dormir:
                gestorTama.cicloSueño.Dormir();
                gestorTama.animo.Dormir();
                gestorTama.energia.Dormir();

                gestorTama.animCerdo.Dormido();
                gestorTama.animCerdo.Inactivo ();

                break;
            #endregion

            #region Accion.despertar
            case accion.despertar:
                gestorTama.cicloSueño.Despertar();
                gestorTama.energia.Despertar();
                gestorTama.animo.Despertar();

                gestorTama.animCerdo.Despierto ();
                gestorTama.animCerdo.Activo ();

                break;
            #endregion

            #region Accion.descansar
            case accion.descansar:
                gestorTama.energia.Descansar();

                gestorTama.animCerdo.Inactivo ();

                break;
            #endregion

            #region Accion.bañar
            case accion.bañar:
                gestorTama.salud.Bañar();
                gestorTama.alimentacion.Bañar();
                gestorTama.animo.Bañar ();

                break;
            #endregion

            #region Accion.enmugrar
            case accion.enmugrar:
                gestorTama.salud.Enmugrar();
                gestorTama.animo.Enmugrar();
                break;
            #endregion

            #region Accion.jugar
            case accion.jugar:
                gestorTama.alimentacion.Jugar();
                gestorTama.animo.Jugar();
                gestorTama.energia.Jugar();

                gestorTama.animCerdo.Activo ();


                break;
            #endregion

            #region Accion.enfermar
            case accion.enfermar:
                gestorTama.salud.Enfermar();
                gestorTama.animo.Enfermar();
             
                break;
            #endregion

            #region Accion.sanar
            case accion.sanar:
                gestorTama.salud.Sanar();
                gestorTama.animo.Sanar();
               
                break; ;
            #endregion

            #region Accion.comer
            case accion.comer:
                gestorTama.alimentacion.Comer();
                break; ;
            #endregion

            #region Accion.beber
            case accion.beber:
                gestorTama.alimentacion.Beber();
                break; ;

            #endregion

            #region Accion.consentir
            case accion.consentir:
                gestorTama.animo.Consentir();

                break;
            #endregion

            #region Accion.morir
            case accion.morir:
                gestorTama.estadosActuales.lista.Clear();
                gestorTama.estadosActuales.EliminarAgregar(GestorTamagotchi.estados.vivo.ToString(), GestorTamagotchi.estados.muerto.ToString());

                gestorTama.animCerdo.Muerto ();
                break;
            #endregion

           

        }

    }

    public static bool ValidarAccion(Accion.accion accion,string id)
    {
        if (accion == Accion.accion.nacer ||
        (TamagotchiManager.GetGestorTamagotchi(id).estadosActuales.VerificarEstado(GestorTamagotchi.estados.vivo.ToString()) &&
        (TamagotchiManager.GetGestorTamagotchi(id).estadosActuales.VerificarEstado(CicloSueño.Estado.despierto.ToString())
        || accion == Accion.accion.despertar)))
            return true;
        else return false;
    }
    public  enum accion
    {
        [HideInInspector]
        nacer,
        dormir,
        despertar,
        bañar,
        enmugrar,
        jugar,
        enfermar,
        sanar,
        comer,
        beber,
        consentir,
        morir,
        descansar


    }

}
