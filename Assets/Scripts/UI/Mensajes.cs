using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mensajes : MonoBehaviour
{
    public static Mensajes singleton;
    public Text txtMensaje;
    public GameObject mensaje;

    void Awake()
    {
        singleton = this;
    }

    public void Mensaje(string m)
    {
        txtMensaje.text = m;
        mensaje.SetActive(true);
    }
}
