using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoCreado : MonoBehaviour
{
    public Hexagono padre;
    public int indicePropiedades;
    public int activaBoton = -1;
    public int cuantoRecursos = 5;

    public void OnMouseUp()
    {
        if (MorionTools.MouseEnUI()) return;
        if (GestionPropiedades.singleton != null)
            GestionPropiedades.singleton.Activar(indicePropiedades);


        if (ControlAldea.singleton.modo == Modos.talar && !MovCamera.moviendo && !ControlAldea.MouseEnUI())
        {
            if (padre.ocupadoPor == 0 && padre.crecimientoArbol != (int) CrecimientoArbol.Crecido) return;
            padre.Desocupar();
            Instantiate(ControlAldea.singleton.particulasExplocion, transform.position, Quaternion.identity);
            GestorEconomia.singleton.SumarRecurso(0, cuantoRecursos);
            Destroy(gameObject);
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(()=> (BotonesCerdito.singleton != null));
        BotonesCerdito.singleton.Activar(activaBoton);

    }
}
