using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Alimentacion 
{
    public string id;
    public float nivel;
    public Hambre hambre=new Hambre();
    public Hidratacion hidratacion= new  Hidratacion();

    float importanciaHambre=0.6f;
    float imporHidratacion=0.4f;


    public Alimentacion() {

 
    }

    public void Actualizar()
    {
        hambre.Actualizar();
        hidratacion.Actualizar();
        ActualizarNivel ();
    }

    public void ActualizarNivel ()
    {
       // Debug.Log ( $"Nivel hambre: {(1-hambre.nivel)*0.6}| Nivel hidratacion: {hidratacion.nivel*0.4f}" );
        nivel = (1-hambre.nivel )* importanciaHambre + hidratacion.nivel * imporHidratacion;
    }

    #region Acciones metodos

    public void Nacer() {
        hambre.Nacer();
        hidratacion.Nacer();
    }

    public void Bañar() {
        hidratacion.Bañar();
    }

    public void Jugar() {
        hambre.Jugar();
        hidratacion.Jugar();
    }

    public void Comer() {
        hambre.Comer();
    }

    public void Beber() {
        hidratacion.Beber();
    }

    internal void Inicializar ( string id )
    {
        this.id = id;
        hambre.Inicializar ( id );
        hidratacion.Inicializar ( id );
    }

    #endregion
}