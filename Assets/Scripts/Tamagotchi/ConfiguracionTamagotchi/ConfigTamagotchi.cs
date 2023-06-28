using System.Collections;
using UnityEngine;

public class ConfigTamagotchi : MonoBehaviour
{
    #region Config Sistemas
    public ConfigCicloSueño configCicloSueño;
    public ConfigEnergia configEnergia;
    public ConfigEdad configEdad;
    public ConfigAnimo configAnimo;
    public ConfigSalud configSalud;
    public ConfigAlimentacion configAlimentacion;
    //public Alimentacion alimentacion;

    #endregion
    [Space]
    public  Tiempo tiempoActual;

    public static ConfigTamagotchi instance;

    public void Awake ()
    {
        
        instance = this;
        tiempoActual.AsignarTiempoActual ();

        configCicloSueño.SetInitialValues ();
        configEdad.SetInitialValues ();
        configAnimo.SetInitialValues ();
        configAlimentacion.SetInitialValues ();
    }

    private void Update ()
    {
        tiempoActual.AsignarTiempoActual();

    }


}
