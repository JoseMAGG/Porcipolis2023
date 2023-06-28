using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sistema 
{
      [Range(0,1)]
      public string id;
    public float nivel;
    [HideInInspector]
    public float pivoteNivel;

 
    public  void VerificarNivel(string bajo,string medio, string alto, Tiempos tiempos)
    {
        if (NivelDentroDelLimite())
        {

            if (nivel >= 1f / 3f)
            {
                if (nivel >= 2f / 3f)
                {
                   

                    TamagotchiManager.GetGestorTamagotchi(id).estadosActuales.EliminarAgregar(medio, alto);
                }

                else
                {
                    if (TamagotchiManager.GetGestorTamagotchi(id).estadosActuales.lista.Contains(alto))
                    {
                        TamagotchiManager.GetGestorTamagotchi(id).estadosActuales.EliminarAgregar(alto, medio);
                    }
                    else if (TamagotchiManager.GetGestorTamagotchi(id).estadosActuales.lista.Contains(bajo))
                    {
                        TamagotchiManager.GetGestorTamagotchi(id).estadosActuales.EliminarAgregar(bajo, medio);
                    }
                    else
                    {
                        TamagotchiManager.GetGestorTamagotchi(id).estadosActuales.AgregarEstado(medio);
                    }
                }
            }
            else
            {
               

                if (TamagotchiManager.GetGestorTamagotchi(id).estadosActuales.VerificarEstado(bajo))
                    return;

                TamagotchiManager.GetGestorTamagotchi(id).estadosActuales.EliminarAgregar(medio,bajo);
                if (!(tiempos is null))
                    tiempos.ResetearTiempos();

            }
         

        }
        else {
            Debug.Log( $"{TamagotchiManager.GetGestorTamagotchi(id).gameObject.name} murio a causa del sistema de  " + this.GetType().Name);
            Accion.EjecutarAccion(Accion.accion.morir,id);
            
        }


    }

    public void VerificarNivel(string bajo, string alto,Tiempos tiempos) {
        if (NivelDentroDelLimite())
        {
            if (nivel >= 0.5)
                TamagotchiManager.GetGestorTamagotchi(id).estadosActuales.EliminarAgregar(bajo, alto);
            else
            {
                if (TamagotchiManager.GetGestorTamagotchi(id).estadosActuales.VerificarEstado(bajo))
                    return;
                TamagotchiManager.GetGestorTamagotchi(id).estadosActuales.EliminarAgregar(alto, bajo);
                if (!(tiempos is null))
                    tiempos.ResetearTiempos();
            }

        }
        else {
            Debug.Log("Pepa murio a causa del sistema de  "+this.GetType().Name);
            Accion.EjecutarAccion(Accion.accion.morir,id);


        }

    }

    bool NivelDentroDelLimite() => (nivel >= 0 && nivel <= 1) ? true : false;


    public void Incrementar(float aux)
    {
        float sub = nivel + aux;
        if (sub >= 1)
            nivel = 1;
        else
        {
            nivel = sub;
        }


    }
    public void Disminuir(float aux)
    {
        float sub = nivel - aux;

        if (sub >= 0)
            nivel = sub;
        else
        {
            nivel = 0;
        }


    }



    #region clase Tiempos
    [System.Serializable]
    public class Tiempos
    {   [HideInInspector]
        public Tiempo ultimaVez;
        
        public Tiempo tiempoActualSin;

        [HideInInspector]
        public int tiempoActualSinSec; // tiempo Sin en segundos

        public Tiempos()
        { }

        public void ActualizarTiempos()
        {
            ActualizarTiempoActualSin();
            TiempoActualSinASec();
        }

        public void ResetearTiempos()
        {
            tiempoActualSin = new Tiempo();
            ultimaVez.AsignarTiempoActual();
        }

        public void ActualizarTiempoActualSin()
        {
            tiempoActualSin = Tiempo.Diferencia(ultimaVez,
                       ConfigTamagotchi.instance.tiempoActual);

        }

        public void TiempoActualSinASec()
        {
            tiempoActualSinSec = TamagotchiTiempoExtraTools.TiempoASegundos(tiempoActualSin);
        }
 
    }

    #endregion
}
