using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorionAgenteNavegacion : MonoBehaviour
{
	public Transform target;
	public float velocidad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if ((target.position - transform.position).sqrMagnitude > 0.1f)
		{
			transform.position = Vector3.Lerp(transform.position, target.position, velocidad*Time.deltaTime);
			//transform.forward = -transform.position + target.position;
			transform.up = transform.position;
			transform.LookAt(target, transform.up);
		}
    }
}
