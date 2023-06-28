using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform cerdito;
    public Vector3 vdistancia;

    void Start()
    {
        vdistancia = transform.position - cerdito.position;
    }

    // Update is called once per frame
    public void Update()
    {
        Vector3 nuevaPosicion = new Vector3(transform.position.x, 
            transform.position.y, 
            vdistancia.z + cerdito.position.z);

        //transform.position = Vector3.Lerp(transform.position, nuevaPosicion, 20 * Time.deltaTime);
        transform.position = nuevaPosicion;
    }
}
