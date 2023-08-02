using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GestionCerdoUI : MonoBehaviour
{
    public static GestionCerdoUI singleton;
    public GestorTamagotchi tamagotchiActual;
    public GameObject cnvInfoCerdo;
    public SelectorCerdos selectorCerdo;
    public ObjetosActivarDesactivar[] objetosAD;

    public RectTransform recursos;
    public RectTransform padreOriginalDeRecursos;
    public RectTransform padreDeRecursos;
    private Vector3 infoRecursosPosition;
    

    private void Awake()
    {
        singleton = this;
    }

    public void CambiarEscena(string escena)
    {
        Inicializador.singleton.GuardarDatos();
        tamagotchiActual.GuardarAhora();
        SceneManager.LoadScene(escena);
    }

    public void Activar(GestorTamagotchi a, SelectorCerdos s)
    {
        tamagotchiActual = a;
        selectorCerdo = s;
        cnvInfoCerdo.SetActive(true);

        infoRecursosPosition = recursos.transform.position;
        recursos.SetParent(padreDeRecursos);
        recursos.transform.localPosition = Vector3.zero;
    }

    public void Desactivar()
    {
        selectorCerdo.VolverAlPiso();
        cnvInfoCerdo.SetActive(false);

        recursos.SetParent(padreOriginalDeRecursos);
        recursos.transform.position = infoRecursosPosition;
    }

    public void ActivarObjetos(int cuales)
    {
        objetosAD[cuales].Activar();
        DesactivarRetrazado(objetosAD[cuales], 8);
    }

    public void DesactivarRetrazado(ObjetosActivarDesactivar o, float t)
    {
        StartCoroutine(AccionarRetrazadamente(o, t));
    }

    IEnumerator AccionarRetrazadamente(ObjetosActivarDesactivar o, float t)
    {
        yield return new WaitForSeconds(t);
        o.Desactivar();
    }
}
[System.Serializable]
public class ObjetosActivarDesactivar
{
    public GameObject[] objetosActivar;

    public void Activar()
    {
        for (int i = 0; i < objetosActivar.Length; i++)
        {
            objetosActivar[i].SetActive(true);
        }
    }


    public void Desactivar()
    {
        for (int i = 0; i < objetosActivar.Length; i++)
        {
            objetosActivar[i].SetActive(false);
        }
    }
}