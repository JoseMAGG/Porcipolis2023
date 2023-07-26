using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjetoCreado))]
public class Arbol : MonoBehaviour
{
    ObjetoCreado objeto;
    public GameObject[] arboles;
    public int cuantoRecursos = 5;

    private float minimoPequeño = 0.3f;
    private float minimoMediano = 0.6f;
    private float minimoGrande = 0.9f;
    private float maximoGrande = 1.2f;

    private static int tiempoMinCrecimiento = 5;
    private static int tiempoMaxCrecimiento = 20;
    

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

    private void OnMouseUp()
    {
        if (ControlAldea.singleton.modo == Modos.talar && !MovCamera.moviendo && !ControlAldea.MouseEnUI()
            && crecimiento.Equals(CrecimientoArbol.Crecido))
        {
            objeto.padre.Desocupar();
            Instantiate(ControlAldea.singleton.particulasExplocion, transform.position, Quaternion.identity);
            GestorEconomia.singleton.SumarRecurso(0, cuantoRecursos);
            Destroy(gameObject);
        }
    }

    void Update()
    {

    }

    private IEnumerator Crecer()
    {
        //yield return new WaitForSeconds(1);
        while (!crecimiento.Equals(CrecimientoArbol.Crecido))
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
                default:
                    sizeVector = Vector3.zero;
                    break;
            }
            objeto.padre.crecimientoArbol = (int) crecimiento;
            transform.localScale = sizeVector;
            
            int secs = Random.Range(tiempoMinCrecimiento, tiempoMaxCrecimiento);
            yield return new WaitForSecondsRealtime(secs);

            if(crecimiento < CrecimientoArbol.Crecido) crecimiento++;
        }
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
