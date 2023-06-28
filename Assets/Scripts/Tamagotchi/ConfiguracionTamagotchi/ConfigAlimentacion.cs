using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ConfigAlimentacion 
{
    public ConfigHambre configHambre;
    public ConfigHidratacion configHidratacion;

    public  void SetInitialValues ()
    {
        configHambre.SetInitialValues ();
        configHidratacion.SetInitialValues ();
    }
}
