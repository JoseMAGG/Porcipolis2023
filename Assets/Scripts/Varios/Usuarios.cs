using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Usuarios : MonoBehaviour
{
    public InputField   txtUsuario;
    public InputField   txtContraseña1;
    public InputField   txtContraseña2;
    public GameObject   imError;
    public Text         txtError;

    public void Login()
    {
        string contra = PlayerPrefs.GetString("URSS" + txtUsuario.text);
        if (txtContraseña1.text == "" || txtUsuario.text == "")
        {
            Error("Todos los campos son obligatorios");
            return;
        }
        else if (contra == txtContraseña1.text)
        {
            MorionTools.nombreUsuario = txtUsuario.text;
            PlayerPrefs.SetString("usuario_cargado", txtUsuario.text);
            Escenas.singleton.CargarEscena("Juego");
        }
        else
        {
            Error("Datos incorrectos.");
        }
    }

    public void Registrar()
    {
        if (txtUsuario.text =="" || txtContraseña2.text=="" || txtContraseña1.text =="")
        {
            Error("Todos los campos son obligatorios");
            return;
        }

        string contra = PlayerPrefs.GetString("URSS" + txtUsuario.text, ".X.");
        if (contra == ".X.")
        {
            if (txtContraseña1.text == txtContraseña2.text)
            {
                MorionTools.nombreUsuario = txtUsuario.text;
                PlayerPrefs.SetString("URSS" + txtUsuario.text, txtContraseña1.text);
                Escenas.singleton.CargarEscena("Login");
            }
            else
            {
                Error("Las contraseñas no coinciden.");
                txtContraseña1.text = "";
                txtContraseña2.text = "";
            }
        }
        else
        {
            Error("El usuario ''" + txtUsuario.text + "'' ya extiste en este dispositivo.");
        }
    }
    public void Error(string msj)
    {
        txtError.text = msj;
        imError.SetActive(true);
        print(msj);
    }
}
