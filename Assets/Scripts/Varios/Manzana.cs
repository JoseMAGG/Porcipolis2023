using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class Manzana : MonoBehaviour
{
    public float probabilidad = 0.2f;
    public Arbol arbol;
    
    private static float tiempoMinAparecer = 60;
    private static float tiempoMaxAparecer = 120;

    public bool arbolEstaGrande;

    void Start()
    {
        arbolEstaGrande = arbol.crecimiento >= CrecimientoArbol.Grande;
        if (arbolEstaGrande)
        {
            gameObject.SetActive(Random.Range(0f, 1f) < probabilidad);
        }
        else gameObject.SetActive(false);
    }
    private void OnMouseUp()
    {
        GestorEconomia.singleton.SumarRecurso(1, 1);
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (!arbolEstaGrande)
            arbolEstaGrande = arbol.crecimiento >= CrecimientoArbol.Grande;
    }
}
