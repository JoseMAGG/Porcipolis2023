using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPrecios : MonoBehaviour
{
    public Precio[] precios;

    private void Start()
    {
        ActualizarEstados();
        for (int i = 0; i < precios.Length; i++)
        {
            precios[i].txtPrecio.text = precios[i].precio.ToString("00");
        }
    }

    public void Seleccionar(int cual)
    {
        Inicializador.singleton.CambiarPrecio(precios[cual].precio);
        Inicializador.singleton.CambiarTipoRecurso(precios[cual].tipoRecurso);
    }

    public void ActualizarEstados()
    {
        for (int i = 0; i < precios.Length; i++)
        {
            precios[i].Informar(GestorEconomia.singleton.VerificarRecurso(precios[i].tipoRecurso, precios[i].precio));
        }
    }
}

[System.Serializable]
public class Precio
{
    public int precio;
    public int tipoRecurso;
    public Button boton;
    public Text txtPrecio;

    public void Informar(bool que)
    {
        boton.interactable = que;
    }

}