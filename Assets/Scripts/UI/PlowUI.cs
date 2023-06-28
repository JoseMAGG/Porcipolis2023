using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlowUI : MonoBehaviour
{
    public AnimationCurve curva;
    public float velocidad;
    float t;
    public bool adelante=true;

    [ContextMenu("Plotear")]
    public void IniciarPlot()
    {
        adelante = true;
        t = 0;
    }

    [ContextMenu("Plotear Inversa")]
    public void IniciarPlotInverso()
    {
        adelante = false;
        t = 0;
    }

    void Update()
    {
        if (t<1)
        {
            t += velocidad * Time.deltaTime;
            t = Mathf.Clamp01(t);
            if (adelante)
            {
                transform.localScale = Vector3.one * curva.Evaluate(t);
            }
            else
            {
                transform.localScale = Vector3.one * curva.Evaluate(1-t);
            }
            
        }
    }
}
