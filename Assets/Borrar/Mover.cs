using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
	public float velocidad;
	public float velRotacion;

    void Update()
    {

		transform.Translate(
			(Vector3.forward * Input.GetAxis("Vertical")) 
			* Time.deltaTime * velocidad);

		transform.Rotate(Vector3.up * Input.GetAxis("Horizontal")*velRotacion * Time.deltaTime);
		
    }
}
