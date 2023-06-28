using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamagotchiManager : MonoBehaviour
{
    public static List<GestorTamagotchi> gestores = new List<GestorTamagotchi>();



    private void Start ()
    {

    }


    public static GestorTamagotchi GetGestorTamagotchi ( string id ) {

        foreach ( GestorTamagotchi gestor in gestores )
        {
            if ( gestor.id.Equals ( id ) )
            {
                return gestor;
            }
        }
        // throw new System.Exception ( $"No se ha encontrado el gestor {id}" );
        return null;
    }



    public static void AñadirGestor ( GestorTamagotchi gestorTamagotchi )
    {
        gestorTamagotchi.id = AsignarId ();

        gestores.Add ( gestorTamagotchi );
    }

    public static string AsignarId () => ( ( gestores.Count + 1 ).ToString () );

 

}
