using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{

    public bool tap, swipeIzq, swipeDer, swipeArr, swipeAbaj;
    public bool isDraging = false;
    public Vector2 startTouch, swipeD;

    public static SwipeManager singleton;

    public void Awake()
    {
        singleton = this;
    }

    public void Update()
    {
        tap = swipeIzq = swipeDer = swipeArr = swipeAbaj = false;

        // PC Inputs 
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }

        // Mobile Inputs
        if (Input.touches.Length > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDraging = true;
                startTouch = Input.touches[0].position;
            }
            else if(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                { }
                isDraging = false;

                Reset();
            }
        }

        // Distancia
        swipeD = Vector2.zero;
        if(isDraging)
        {
            if (Input.touches.Length < 0) swipeD = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0)) swipeD = (Vector2)Input.mousePosition - startTouch;
        }

        // Suficiente swipe
        if(swipeD.magnitude > 125)
        {
            // Direccion
            float x = swipeD.x;
            float y = swipeD.y;
            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0) swipeIzq = true; 
                else swipeDer = true;
            }
            else
            {
                if (y < 0) swipeAbaj = true;
                else swipeArr = true;
            }

            Reset();
        }


    }


    public void Reset()
    {
        startTouch = swipeD = Vector2.zero;
        isDraging = false;
    }

}
