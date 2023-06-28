using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanciaGPS : MonoBehaviour
{
    public Vector2 coordenada1;
    public Vector2 coordenada2;
    public float distancia;
    void Start()
    {
        
    }

    [ContextMenu("Calcular Distancia")]
    public void CalcularDistancia()
    {
        distancia = 6371f * Mathf.Acos(Mathf.Cos(Mathf.Deg2Rad * coordenada1.x) * Mathf.Cos(Mathf.Deg2Rad * coordenada2.x) * Mathf.Cos(Mathf.Deg2Rad * (coordenada2.y - coordenada1.y)) + Mathf.Sin(Mathf.Deg2Rad * coordenada1.x) * Mathf.Sin(Mathf.Deg2Rad * coordenada2.x));
    }
}
