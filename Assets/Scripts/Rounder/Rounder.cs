using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Rounder : MonoBehaviour
{
	public float radio;
	public bool redondearZ;
	public bool restarRadio = true;
	public bool redondearConstantemente = false;

	MeshRenderer meshRenderer;
	Vector3[] posicionesIniciales;
	Vector3[] posicionesEditadas;

	Mesh maya;

	float perimetroNormalizado;

	bool normalesInvertidas;
	// Start is called before the first frame update
	void Start()
	{
		maya = GetComponent<MeshFilter>().mesh;
		meshRenderer = GetComponent<MeshRenderer>();
		posicionesIniciales = maya.vertices;
		posicionesEditadas = new Vector3[posicionesIniciales.Length];
        Redondear();
    }

	public void Redondear(float nuevoRadio)
	{
		radio = nuevoRadio;
		Redondear();
	}

	public void Redondear()
	{
		if (radio == 0)
		{
			return;
		}
		float nuevoRadio;
		// Calculo el perimetro
		perimetroNormalizado = 2 * Mathf.PI * radio;
		// en este vector voy a guardar los nuevos datos calculados y en la variable el ángulo en radianes
		Vector3 pivote = new Vector3();
		float angulo;

		// Recorrer cada punto de la antigua maya y calcular el nuevo
		for (int i = 0; i < posicionesIniciales.Length; i++)
		{
			nuevoRadio = radio + posicionesIniciales[i].y;
			angulo = (posicionesIniciales[i].x / perimetroNormalizado) * 2 * Mathf.PI;
			angulo -= Mathf.PI/2;

			pivote.x = -nuevoRadio * Mathf.Cos(angulo);
			pivote.y = -nuevoRadio * Mathf.Sin(angulo);
			if (redondearZ)
			{
				angulo = (posicionesIniciales[i].z / perimetroNormalizado) * 2 * Mathf.PI;
				angulo -= Mathf.PI / 2;
				pivote.z = nuevoRadio * Mathf.Cos(angulo);
				pivote.y =(-nuevoRadio * Mathf.Sin(angulo) + pivote.y)/2;
			}
			else
			{
				pivote.z = posicionesIniciales[i].z;
			}

			if (restarRadio)
			{
				pivote.y -= radio;
			}
			posicionesEditadas[i] = pivote;
		}
		// Las normales quedan invertidas así que toca Flipear
		if (!normalesInvertidas)
		{
			InvertirNormales();
		}

		// Graficar solucion
		maya.vertices = posicionesEditadas;
		maya.RecalculateBounds();
	}

	void InvertirNormales()
	{
		maya.triangles = maya.triangles.Reverse().ToArray();
		normalesInvertidas = !normalesInvertidas;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if (redondearConstantemente)
		{
			Redondear();
		}
    }
}
