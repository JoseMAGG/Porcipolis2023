using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomCamera : MonoBehaviour
{
    public Slider slider;
    public Camera camara;

    public float a, b;

    public static ZoomCamera singleton;

    void Awake()
    {
        singleton = this;
    }

    void Start()
    {

        camara.fieldOfView = 80f;
        slider.value = 0.5f;
    }

    void Update()
    {
        //camara.fieldOfView = slider.value * 2f * 80f;
        camara.fieldOfView = Mathf.Lerp(a,b,slider.value);
    }
}
