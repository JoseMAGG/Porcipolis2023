using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonesCerdito : MonoBehaviour
{
    public static BotonesCerdito singleton;
    public Button[] botones;

    private void Awake()
    {
        singleton = this;
    }



    public void Activar(int i)
    {
        if (i>=0 && i<botones.Length)
        {
            botones[i].interactable = true;
        }
    }
}
