using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjetoCreado))]
public class Arbol : MonoBehaviour
{
    ObjetoCreado objeto;
    public GameObject[] arboles;
    public int cuantoRecursos = 5;

    void Start()
    {
        objeto = GetComponent<ObjetoCreado>();
        int k = Random.Range(0, arboles.Length);
        for (int i = 0; i < arboles.Length; i++)
        {
            arboles[i].SetActive(i == k);
        }
    }

    private void OnMouseUp()
    {
        if (ControlAldea.singleton.modo == Modos.talar && !MovCamera.moviendo && !ControlAldea.MouseEnUI()) 
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
}
