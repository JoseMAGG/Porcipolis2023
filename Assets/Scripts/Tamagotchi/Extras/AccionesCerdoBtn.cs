using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionesCerdoBtn : MonoBehaviour
{
    public GestorTamagotchi gestorTamagotchi;

    public void Jugar()
    {
        gestorTamagotchi.EjecutarAccion(Accion.accion.jugar);
    }

    public void Comer(int comida)
    {
        switch (comida)
        {
            case 1:
                gestorTamagotchi.alimentacion.hambre.comidaSeleccionada = Hambre.Comida.comida1;
                break;
            case 2:
                gestorTamagotchi.alimentacion.hambre.comidaSeleccionada = Hambre.Comida.comida2;
                break;
            case 3:
                gestorTamagotchi.alimentacion.hambre.comidaSeleccionada = Hambre.Comida.comida3;
                break;
            default:
                break;
        }
        gestorTamagotchi.EjecutarAccion(Accion.accion.comer);
    }

    public void Beber()
    {
        gestorTamagotchi.EjecutarAccion(Accion.accion.beber);
    }

    public void Consentir()
    {
        gestorTamagotchi.EjecutarAccion(Accion.accion.consentir);
    }

    public void Bañar()
    {
        gestorTamagotchi.EjecutarAccion(Accion.accion.bañar);
    }

    public void Embarrar()
    {
        gestorTamagotchi.EjecutarAccion(Accion.accion.enmugrar);
    }
}
