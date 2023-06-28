using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovCamera : MonoBehaviour
{
    //public Text txtDebug;
    public Vector2 touchDeltaPosition;

    public float minVel;
    public float maxVel;
    public static bool moviendo;
    public float ajusteAndroid;
    float tt;
    Vector3 rotacion;
    float tiempoInicial;

    private void Start()
    {
#if UNITY_ANDROID
        minVel *= ajusteAndroid;
        maxVel *= ajusteAndroid;
#endif
    }
    void Update()
    {
        //txtDebug.text = "Min: " + minVel + " / Max: " + maxVel;
        if (Input.GetMouseButtonDown(0))
        {
            tiempoInicial = Time.time + 0.1f;
        }

        if (MorionTools.MouseEnUI()) return;

        if (Input.GetMouseButton(0) && Time.time > (tiempoInicial))
        {
            float pointerY = Input.GetAxis("Mouse X");
            float pointerX = Input.GetAxis("Mouse Y");
            gameObject.transform.Rotate(-pointerX * Mathf.Lerp(minVel,maxVel, ZoomCamera.singleton.slider.value), 
                                         pointerY * Mathf.Lerp(minVel, maxVel, ZoomCamera.singleton.slider.value), 0);
            MirarSiMueve();
        }
         
        if(Input.touchCount == 1 && Time.time > (tiempoInicial))
        {
            Touch touchZero = Input.GetTouch(0);
            if(touchZero.phase == TouchPhase.Moved)
            {
                touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                gameObject.transform.Rotate(touchDeltaPosition.y * Mathf.Lerp(minVel, maxVel, ZoomCamera.singleton.slider.value), 
                                           -touchDeltaPosition.x * Mathf.Lerp(minVel, maxVel, ZoomCamera.singleton.slider.value), 0);
                MirarSiMueve();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Invoke("Desactivar", 0.5f);
        }
    }

    void MirarSiMueve()
    {
        if (!moviendo)
        {
            if ((rotacion - transform.eulerAngles).sqrMagnitude > 0)
            {
                moviendo = true;
                tt = 0;
            }
        }
        else
        {
            tt += Time.deltaTime;
            moviendo = tt < 1;
        }
    }
    void Desactivar()
    {
        rotacion = transform.eulerAngles;
        moviendo = false;
    }
}
