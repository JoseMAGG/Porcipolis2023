using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hambre : Sistema
{
   
 

    public Comida comidaSeleccionada;
    [Space]
    public Tiempos tiemposComer;



    public Hambre()
    {
        pivoteNivel = 0;
      
    }

    #region Actualizar

    public void Actualizar()
    {
        tiemposComer.ActualizarTiempos();
        ActualizarNivel();
        VerificarNivel(Estado.lleno.ToString(),Estado.satisfecho.ToString(),Estado.hambriento.ToString(),null);             
    }


    public  void ActualizarNivel() => nivel = pivoteNivel+(float) tiemposComer.tiempoActualSinSec / (float) ConfigTamagotchi.instance.configAlimentacion.configHambre.tiempoMaxSinComerSec;



    #endregion


    float Afloat(Comida comida) => (float)(((int)comida) / 100f);

    #region Acciones

    public void Nacer()
    {
        tiemposComer.ResetearTiempos();

    }

    internal void Inicializar ( string id )
    {
        this.id = id;
    }

    public void Comer()
    {
        int cantidadRecurso = (int) Math.Ceiling((double)((int)comidaSeleccionada / 2));
        bool sePuedeComer = GestorEconomia.singleton.UsarRecurso(1, cantidadRecurso);
        if (!sePuedeComer) throw new Exception("No hay suficiente comida");
        Disminuir(Afloat(comidaSeleccionada));
        pivoteNivel = nivel;
        tiemposComer.ResetearTiempos();
    }


    public void Jugar() {
        Incrementar(ConfigTamagotchi.instance.configAlimentacion.configHambre.hambrePorJugar);
        pivoteNivel = nivel;
        tiemposComer.ResetearTiempos();
    }

    #endregion

    public enum Estado
    {   
        lleno, //No tiene hambre 
        satisfecho, //Estado medio de hambre
        hambriento, //Tiene hambre
    }

    public enum Comida
    {
        comida1 = 5,//Disminye un 5%   el nive de hambre
        comida2 = 10,//Disminye un 10%   el nive de hambre
        comida3 = 20,//Disminye un 20%   el nive de hambre
    }
}