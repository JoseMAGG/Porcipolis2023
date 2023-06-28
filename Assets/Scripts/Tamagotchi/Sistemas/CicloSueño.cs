using System.Collections;
using UnityEngine;
using System;
[System.Serializable]
public class CicloSueño
{
     public string id;

    public Tiempo tiempoAldespertar; // hora en la que se desperto
 
    public Tiempo tiempoAldormir; // hora en la que se durmió 
 
    public Tiempo tiempoActualDormido; // tiempo Actual Dormido
  
    public Tiempo tiempoActualDespierto; // tiempo Actual Despierto
 

    [HideInInspector]
    public int tiempoDespiertoSegundos;

    [HideInInspector]
    public int tiempoDormidoSegundos;

    [HideInInspector]
    public Tiempo desfase;
    [HideInInspector]
    public int desfaseSec;


    public CicloSueño () { }
    public void Actualizar ()
    {
        if ( TamagotchiManager.GetGestorTamagotchi ( id ).estadosActuales.VerificarEstado ( CicloSueño.Estado.despierto.ToString () ) )
        {
            ActualizarTiempoDespierto ();

        }
        else if (  TamagotchiManager.GetGestorTamagotchi(id).estadosActuales.VerificarEstado ( CicloSueño.Estado.dormido.ToString () ) )
        {
            ActualizarTiempoDormido ();
        }
    }
    public void ActualizarTiempoDespierto ()
    {
        tiempoActualDespierto = Tiempo.Diferencia ( tiempoAldespertar , ConfigTamagotchi.instance.tiempoActual );
        tiempoDespiertoSegundos = TamagotchiTiempoExtraTools.TiempoASegundos ( tiempoActualDespierto );
        VerificarLimiteCicloTotalDespierto ();
    }

    public void ActualizarTiempoDormido ()
    {
       Tiempo diferencia = Tiempo.Diferencia ( tiempoAldormir , ConfigTamagotchi.instance.tiempoActual );
        if ( !TamagotchiTiempoExtraTools.VerificarTiempoLimite ( diferencia ) )
        {
            tiempoActualDormido = Tiempo.Diferencia ( tiempoAldormir , ConfigTamagotchi.instance.tiempoActual );
            tiempoDormidoSegundos = TamagotchiTiempoExtraTools.TiempoASegundos ( tiempoActualDormido );
            VerificarLimiteCicloTotalDormido ();
        }

    }
    public void VerificarLimiteCicloTotalDespierto ()
    {
        if ( tiempoDespiertoSegundos > ConfigTamagotchi.instance.configCicloSueño.totalCicloSec )
        {
            desfaseSec = tiempoDespiertoSegundos % ConfigTamagotchi.instance.configCicloSueño.totalCicloSec;
            if ( ConfigTamagotchi.instance.configCicloSueño.tiempoDespiertoMaxSegundos > desfaseSec )
            {
                ConfigurarDesfase ();
                tiempoAldespertar = Tiempo.Diferencia ( desfase , ConfigTamagotchi.instance.tiempoActual );

                tiempoAldespertar.horas = 6;
                tiempoAldespertar.minutos = 0;
                tiempoAldespertar.segundos = 0;
            }
            else
            {
                desfaseSec = desfaseSec - ConfigTamagotchi.instance.configCicloSueño.tiempoDespiertoMaxSegundos;
                ConfigurarDesfase ();
                tiempoAldormir = Tiempo.Diferencia ( desfase , ConfigTamagotchi.instance.tiempoActual );
                TamagotchiManager.GetGestorTamagotchi(id).EjecutarAccion ( Accion.accion.dormir );

                tiempoAldormir.horas = 21;
                tiempoAldormir.minutos = 0;
                tiempoAldormir.segundos = 0;

            }
        }
        else
            VerificarTiempoLimiteDespierto ( Tiempo.Diferencia ( tiempoActualDespierto , ConfigTamagotchi.instance.configCicloSueño.tiempoMaximoDespierto ) , Accion.accion.dormir );
    }

    internal void Inicializar ( string id )
    {
        this.id = id;
    }

    public void VerificarLimiteCicloTotalDormido ()
    {
        if ( ( ConfigTamagotchi.instance.configCicloSueño.tiempoDespiertoMaxSegundos + tiempoDormidoSegundos ) > ConfigTamagotchi.instance.configCicloSueño.totalCicloSec )
        {
            desfaseSec = ( ConfigTamagotchi.instance.configCicloSueño.tiempoDespiertoMaxSegundos + tiempoDormidoSegundos ) % ConfigTamagotchi.instance.configCicloSueño.totalCicloSec;
            if ( ConfigTamagotchi.instance.configCicloSueño.tiempoDespiertoMaxSegundos > desfaseSec )
            {
                ConfigurarDesfase ();
                tiempoAldespertar = Tiempo.Diferencia ( desfase , ConfigTamagotchi.instance.tiempoActual );
                 TamagotchiManager.GetGestorTamagotchi(id).EjecutarAccion ( Accion.accion.despertar );

                tiempoAldespertar.horas = 6;
                tiempoAldespertar.minutos = 0;
                tiempoAldespertar.segundos = 0;
            }
            else
            {
                desfaseSec = desfaseSec - ConfigTamagotchi.instance.configCicloSueño.tiempoDespiertoMaxSegundos;
                ConfigurarDesfase ();
                tiempoAldormir = Tiempo.Diferencia ( desfase , ConfigTamagotchi.instance.tiempoActual );

                tiempoAldormir.horas = 21;
                tiempoAldormir.minutos = 0;
                tiempoAldormir.segundos = 0;
            }
        }
        else
            VerificarTiempoLimiteDespierto ( Tiempo.Diferencia ( tiempoActualDormido , ConfigTamagotchi.instance.configCicloSueño.tiempoMaximoDormido ) , Accion.accion.despertar );
    }
    private void ConfigurarDesfase ()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds((double)desfaseSec);
        desfase = new Tiempo ( 0 , 0 , timeSpan.Days , timeSpan.Hours , timeSpan.Minutes , timeSpan.Seconds );
        TamagotchiTiempoExtraTools.ConfigurarFecha ( desfase );
    }
    public void VerificarTiempoLimiteDespierto ( Tiempo restante , Accion.accion acc )
    {
        if ( TamagotchiTiempoExtraTools.VerificarTiempoLimite ( restante ) )
        {
            desfase = Tiempo.Diferencia ( ConfigTamagotchi.instance.configCicloSueño.tiempoMaximoDespierto , tiempoActualDespierto );
            TamagotchiTiempoExtraTools .ConfigurarFecha ( desfase );
             TamagotchiManager.GetGestorTamagotchi(id).EjecutarAccion ( acc );
        }
    }
    public void VerificarTiempoLimiteDormido ( Tiempo restante , Accion.accion acc )
    {
        if ( TamagotchiTiempoExtraTools.VerificarTiempoLimite ( restante ) )
        {
            desfase = Tiempo.Diferencia ( ConfigTamagotchi.instance.configCicloSueño.tiempoMaximoDormido , tiempoActualDormido );
            TamagotchiTiempoExtraTools.ConfigurarFecha ( desfase );
             TamagotchiManager.GetGestorTamagotchi(id).EjecutarAccion ( acc );
        }
    }
    public void Nacer ()
    {
        tiempoAldespertar.AsignarTiempoActual ();
        tiempoAldespertar.horas = 6;
        tiempoAldespertar.minutos = 0;
        tiempoAldespertar.segundos = 0;
        Accion.EjecutarAccion ( Accion.accion.despertar ,id);
    }
    public void Dormir ()
    {
         TamagotchiManager.GetGestorTamagotchi(id).estadosActuales.EliminarAgregar ( Estado.despierto.ToString () , Estado.dormido.ToString () );
        tiempoAldormir = Tiempo.Diferencia ( desfase , new Tiempo ( DateTime.Now ) );

        tiempoAldormir.horas = 21;
        tiempoAldormir.minutos = 0;
        tiempoAldormir.segundos = 0;
        // Debug.Log ( $"{GestorTamagotchi.Instance.gameObject.name} se durmió" );
    }

    public void Despertar ()
    {
         TamagotchiManager.GetGestorTamagotchi(id).estadosActuales.EliminarAgregar ( Estado.dormido.ToString () , Estado.despierto.ToString () );
        tiempoAldespertar = Tiempo.Diferencia ( desfase , new Tiempo ( DateTime.Now ) );
        tiempoAldespertar.horas = 6;
        tiempoAldespertar.minutos = 0;
        tiempoAldespertar.segundos = 0;
        // Debug.Log ( $"{GestorTamagotchi.Instance.gameObject.name} se despertó" );
    }

    public enum Estado
    {
        despierto,
        dormido
    }

}
