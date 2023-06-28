using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public   class Estado 
{
    public string id;
    public List<string> lista;


    public Estado(string id) {
        this.id = id;
        lista= new List<string> ();
    }

    public  static void  MostrarEstados(string[] arreglo)
    {
        foreach (var estado in arreglo)
        {
            Debug.Log(estado);
        }

    }

 

    public bool VerificarEstado(string estado)
    {
        if (lista.Contains(estado)) return true;
        return false;
    }
    public  void EliminarAgregar(string viejo, string nuevo)
    {
        VerificarTransicionAnimo ( viejo , nuevo );

        EliminarEstado ( viejo);
        AgregarEstado(nuevo);
    }

    public void VerificarTransicionAnimo (string viejo, string nuevo ) {
        if ( ( nuevo.Equals ( "feliz" )|| nuevo.Equals ( "tranquilo" ) ) && viejo.Equals ( "triste" ) )
        {
            TamagotchiManager.GetGestorTamagotchi ( id ).animCerdo.Alegre ();
        }

        else if ( viejo.Equals ( "tranquilo" ) && ( viejo.Equals ( "feliz" ) || nuevo.Equals ( "triste" ) ) )
        {
            TamagotchiManager.GetGestorTamagotchi ( id ).animCerdo.Triste ();

        }
    }
    

    public void EliminarEstado(string estado)
    {
        if (lista.Contains(estado))
        {
            lista.Remove(estado);
        }
    }

    public void AgregarEstado(string estado)
    {
        if (!lista.Contains(estado))
        {
            lista.Add(estado);
        }
    }

}
