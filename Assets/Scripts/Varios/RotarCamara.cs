using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarCamara : MonoBehaviour
{
    public float velocidadAngular;
    public Transform hijo;
    public static bool moviendo;
    public float proporcion;

    float tt;
    Vector3 rotacion;
    float tiempoInicial;

    void Start()
    {
        proporcion = Mathf.Sqrt(Screen.height * Screen.height + Screen.width * Screen.width)/600f;
        velocidadAngular *= proporcion;
        rotacion = transform.eulerAngles;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tiempoInicial = Time.time + 0.1f;
        }
        if (((Input.GetMouseButton(0) || Input.touchCount > 0) && Input.touchCount<2) && Time.time > (tiempoInicial) && !MorionTools.MouseEnUI())
        {
            if (Mathf.Abs(Input.GetAxis("Mouse X")) < 0.9f)
            {
                transform.Rotate((Vector3.up * Input.GetAxis("Mouse X")) * velocidadAngular * Time.deltaTime);
                hijo.Rotate((Vector3.left * Input.GetAxis("Mouse Y")) * velocidadAngular * Time.deltaTime);
            }
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

        if (Input.GetMouseButtonUp(0))
        {
            Invoke("Desactivar", 0.5f);
        }
    }

    void Desactivar()
    {
        rotacion = transform.eulerAngles;
        moviendo = false;
    }
}
