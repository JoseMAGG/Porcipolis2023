using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicMove : MonoBehaviour
{
	public MorionNavegador morionNavegador;
	public Transform objetoPivote;

    void Start()
    {
        
    }

	void Update()
	{

		if (Input.GetMouseButtonUp(0) && !MovCamera.moviendo)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				morionNavegador.SetDestino(hit.point);
				objetoPivote.position = hit.point;
			}
		}

	}
}
