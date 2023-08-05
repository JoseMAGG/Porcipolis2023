using System.Collections;
using UnityEngine;

public class AccionesCerdoBtn : MonoBehaviour
{
    public GestorTamagotchi gestorTamagotchi;
    public GameObject panel;
    public float tiempoPanel;

    public void Start()
    {
        panel.SetActive(false);
    }

    public void Jugar()
    {
        StartCoroutine(ActivarPanel());
        gestorTamagotchi.EjecutarAccion(Accion.accion.jugar);
    }

    public void Comer(int comida)
    {
        switch (comida)
        {
            case 1:
                gestorTamagotchi.alimentacion.hambre.comidaSeleccionada = Hambre.Comida.comida1;
                break;
            case 2:
                gestorTamagotchi.alimentacion.hambre.comidaSeleccionada = Hambre.Comida.comida2;
                break;
            case 3:
                gestorTamagotchi.alimentacion.hambre.comidaSeleccionada = Hambre.Comida.comida3;
                break;
            default:
                break;
        }
        try
        {
            gestorTamagotchi.EjecutarAccion(Accion.accion.comer);
            StartCoroutine(ActivarPanel());
            GestionCerdoUI.singleton.ActivarObjetos(comida - 1);
        }
        catch (System.Exception e)
        {
            Mensajes.singleton.Mensaje(e.Message);
        }
    }

    public void Beber()
    {
        gestorTamagotchi.EjecutarAccion(Accion.accion.beber);
        StartCoroutine(ActivarPanel());
    }

    public void Consentir()
    {
        gestorTamagotchi.EjecutarAccion(Accion.accion.consentir);
        StartCoroutine(ActivarPanel());
    }

    public void Bañar()
    {
        gestorTamagotchi.EjecutarAccion(Accion.accion.bañar);
        StartCoroutine(ActivarPanel());
    }

    public void Embarrar()
    {
        gestorTamagotchi.EjecutarAccion(Accion.accion.enmugrar);
        StartCoroutine(ActivarPanel());
    }
    public void Sanar()
    {
        gestorTamagotchi.EjecutarAccion(Accion.accion.sanar);
        StartCoroutine(ActivarPanel());
    }

    private IEnumerator ActivarPanel()
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(tiempoPanel);
        panel.SetActive(false);
    }
}
