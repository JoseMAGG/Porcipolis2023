using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TamagotchiEvent : MonoBehaviour
{
    #region Singlenton

    public static TamagotchiEvent instance;

    private void Awake ()
    {
        instance = this;
    }

    #endregion

    public event Action <GameObject>OnCerdoSelec;

    public event Action<string> OnCerdoCritico;

    public event Action OnCerdoMuerto;


    public void AsignarCerdo ( GameObject cerdoSel ) => OnCerdoSelec?.Invoke ( cerdoSel );

    public void ChangeClip(string clipname) => OnCerdoCritico?.Invoke(clipname);

    public void CerdoMuerto() => OnCerdoMuerto?.Invoke();

}
