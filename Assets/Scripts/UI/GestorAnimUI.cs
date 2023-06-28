using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorAnimUI : MonoBehaviour
{
    public AnimUI[] animsUI;
    public float frecuencia;
    public bool funcionando;

    bool activo;
    public void Activar()
    {
        if (funcionando)
        {
            return;
        }
        StartCoroutine(Cabios(2));

        for (int i = 0; i < animsUI.Length; i++)
        {
            animsUI[i].gameObject.SetActive(true);
        }
        activo = true;
    }

    public void Desactivar()
    {
        if (funcionando)
        {
            return;
        }
        StartCoroutine(Cabios(1));
        activo = false;
    }


    public void ActivarDesactivar()
    {
        if (funcionando)
        {
            return;
        }
        if (activo)
        {
            Desactivar();
        }
        else
        {
            Activar();
        }
    }

    IEnumerator Cabios(int c)
    {
        funcionando = true;
        for (int i = 0; i < animsUI.Length; i++)
        {
            animsUI[(c == 2)?i:animsUI.Length-i-1].CambiarFase(c);
            yield return new WaitForSeconds(frecuencia);
        }
        funcionando = false;
    }
}
