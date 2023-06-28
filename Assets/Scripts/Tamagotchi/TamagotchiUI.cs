using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TamagotchiUI : MonoBehaviour
{


    string comidaSelec;
    GameObject cerdoSelec;


    void Start () => TamagotchiEvent.instance.OnCerdoSelec += AsignarCerdo;

    private void AsignarCerdo ( GameObject cerdoSel ) => cerdoSelec = cerdoSel;

    private void EjecutarAccion ( Accion.accion accion, string nameAccion )
    {
        if ( cerdoSelec is null )
        {
            Debug.Log ( "No se ha seleccionado ningun cerdo" );
            return;
        }


            Debug.Log ( $"Se va a {accion.ToString ()} al cerdo {cerdoSelec.name}" );
        cerdoSelec.GetComponent<GestorTamagotchi> ().EjecutarAccion ( accion );

        if(!nameAccion.Equals("jugar"))
            StartCoroutine(cerdoSelec.GetComponent<GestorTamagotchi> ().animCerdo.Accion (nameAccion));


    }

    public void Bañar () => EjecutarAccion ( Accion.accion.bañar , "bañar" );
    public void Consentir () => EjecutarAccion ( Accion.accion.bañar , "consentir" );
    public void Beber () => EjecutarAccion ( Accion.accion.bañar , "beber" );
    public void Embarrar () => EjecutarAccion ( Accion.accion.bañar , "embarrar" );
    public void Jugar () => EjecutarAccion ( Accion.accion.jugar , "jugar" );

    public void AsignarComidaSelec ( string tipo ) => comidaSelec = tipo;
    public void Comer ()
    {

        if ( cerdoSelec == null  )
        {
            Debug.Log ( "No se ha seleccionado ningun cerdo" );
            return;
        }
        if ( comidaSelec.Equals ( "" ) )
        {
            Debug.Log ( "No se ha seleccionado ninguna comida" );
            return;
        }
        cerdoSelec.GetComponent<GestorTamagotchi> ().AsignarTipoComida ( comidaSelec );
        cerdoSelec.GetComponent<GestorTamagotchi> ().EjecutarAccion ( Accion.accion.comer );
        StartCoroutine ( cerdoSelec.GetComponent<GestorTamagotchi> ().animCerdo.Accion ( "comer" ) );
    }


}
