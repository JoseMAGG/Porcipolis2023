using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjetoCreado))]
public class Arbol : MonoBehaviour
{
    ObjetoCreado objeto;
    public GameObject[] arboles;

    private float minimoPequeño = 0.3f;
    private float minimoMediano = 0.6f;
    private float minimoGrande = 0.9f;
    private float maximoGrande = 1.2f;

    private static int tiempoMinCrecimiento = 20;
    private static int tiempoMaxCrecimiento = 80;
    

    internal CrecimientoArbol crecimiento = 0;

    void Start()
    {
        objeto = GetComponent<ObjetoCreado>();
        int k = Random.Range(0, arboles.Length);
        for (int i = 0; i < arboles.Length; i++)
        {
            arboles[i].SetActive(i == k);
        }
        StartCoroutine(Crecer());
    }

    private IEnumerator Crecer()
    {
        do
        {
            Vector3 sizeVector;
            switch (crecimiento)
            {
                case CrecimientoArbol.Pequeño:
                    sizeVector = RandomiceVector(minimoPequeño, minimoMediano);
                    break;
                case CrecimientoArbol.Mediano:
                    sizeVector = RandomiceVector(minimoMediano, minimoGrande);
                    break;
                case CrecimientoArbol.Grande:
                    sizeVector = RandomiceVector(minimoGrande, maximoGrande);
                    crecimiento++;
                    break;
                case CrecimientoArbol.Crecido:
                    sizeVector = RandomiceVector(minimoGrande, maximoGrande);
                    break;

                default:
                    sizeVector = Vector3.zero;
                    break;
            }
            objeto.padre.crecimientoArbol = (int)crecimiento;
            transform.localScale = sizeVector;

            Inicializador.singleton.GuardarDatos();

            int secs = Random.Range(tiempoMinCrecimiento, tiempoMaxCrecimiento);
            yield return new WaitForSecondsRealtime(secs);

            if (crecimiento < CrecimientoArbol.Crecido) crecimiento++;
        } while (!crecimiento.Equals(CrecimientoArbol.Crecido));
    }

    private Vector3 RandomiceVector(float min, float max)
    {
        Vector3 sizeVector;
        float sizeX = Random.Range(min, max);
        float sizeY = Random.Range(min, max);
        float sizeZ = Random.Range(min, max);
        sizeVector = new Vector3(sizeX, sizeY, sizeZ);
        return sizeVector;
    }
}
public enum CrecimientoArbol
{
    Pequeño = 0, Mediano = 1, Grande = 2, Crecido = 3
}
