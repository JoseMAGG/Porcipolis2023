using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoCreado : MonoBehaviour
{
    public Hexagono padre;
    public int indicePropiedades;
    public int activaBoton = -1;

    public void OnMouseUp()
    {
        if (MorionTools.MouseEnUI()) return;
        if (GestionPropiedades.singleton != null)
            GestionPropiedades.singleton.Activar(indicePropiedades);
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(()=> (BotonesCerdito.singleton != null));
        BotonesCerdito.singleton.Activar(activaBoton);

    }
}
