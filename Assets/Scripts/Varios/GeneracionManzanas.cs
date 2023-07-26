using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneracionManzanas : MonoBehaviour
{
    private const float probabilidadGeneracion = 0.3f;
    public List<Manzana> manzanas;

    private bool esperandoGenerar = false;
    private static int tiempoMinGenerar = 20;
    private static int tiempoMaxGenerar = 50;
    private float tiempoPrueba;
        
    void Update()
    {
        tiempoPrueba += Time.deltaTime;
        bool arbolCrecido = manzanas[0].arbol.crecimiento == CrecimientoArbol.Crecido;
        if (!esperandoGenerar && arbolCrecido)
            StartCoroutine(Generar()); 
    }

    private IEnumerator Generar()
    {
        esperandoGenerar = true;
        yield return new WaitForSeconds(Random.Range(tiempoMinGenerar, tiempoMaxGenerar));

        List<Manzana> inactivas = ManzanasActivas();
        if (inactivas.Count > 0)
        {
            Manzana elegida = inactivas[Random.Range(0, inactivas.Count)];
            bool activar = Random.Range(0f, 1f) < probabilidadGeneracion;
            elegida.gameObject.SetActive(activar);
        }
        tiempoPrueba = 0f;
        esperandoGenerar = false;
    }

    private List<Manzana> ManzanasActivas()
    {
        List<Manzana> inactivas = new List<Manzana>();
        foreach (Manzana manzana in manzanas)
        {
            if (!manzana.gameObject.activeInHierarchy) inactivas.Add(manzana);
        }
        return inactivas;
    }
}
