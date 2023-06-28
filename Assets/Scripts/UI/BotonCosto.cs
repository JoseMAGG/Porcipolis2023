using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BotonCosto : MonoBehaviour
{
    public int precio;
    public int tipoRecurso;
    public UnityEvent evento;
    public void ActivarEvento()
    {
        if (GestorEconomia.singleton.VerificarRecurso(tipoRecurso, precio))
        {
            GestorEconomia.singleton.UsarRecurso(tipoRecurso, precio);
            evento.Invoke();
        }
        else
        {
            Mensajes.singleton.Mensaje("No tiene recursos suficientes para realizar esta operación");
        }
    }
}
