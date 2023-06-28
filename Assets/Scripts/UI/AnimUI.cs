using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class AnimUI : MonoBehaviour
{
    public AnimationCurve curvaAnimacion;
    public float tiempo;
    public Vector2 tam1;
    public Vector2 tam2;

    RectTransform rect;
    public float t;
    int fase;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        rect.sizeDelta = tam1;
        gameObject.SetActive(false);
    }
    

    public void CambiarFase(int f)
    {
        switch (f)
        {
            case 0:
                break;
            case 1:
                t = 1;
                break;
            case 2:
                t = 0;
                break;
            default:
                break;
        }
        fase = f;
    }
    
    void Update()
    {
        switch (fase)
        {
            case 0:
                break;
            case 1:
                Vector2 ntam = Vector2.LerpUnclamped(tam1, tam2, curvaAnimacion.Evaluate(t));
                rect.sizeDelta = ntam;
                t -= Time.deltaTime / tiempo;
                if (t<0)
                {
                    CambiarFase(0);
                    rect.sizeDelta = tam1;
                    gameObject.SetActive(false);
                }
                break;
            case 2:
                Vector2 ntam2 = Vector2.LerpUnclamped(tam1, tam2, curvaAnimacion.Evaluate(t));
                rect.sizeDelta = ntam2;
                t += Time.deltaTime / tiempo;
                if (t>1)
                {
                    CambiarFase(0);
                    rect.sizeDelta = tam2;
                }
                break;
            default:
                break;
        }
    }
}
