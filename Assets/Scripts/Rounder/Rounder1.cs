using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Rounder1 : MonoBehaviour
{
	public float radio;
	public bool restarRadio = true;
	public bool invertirNormales = true;
	public bool redondearConstantemente = false;

	MeshRenderer meshRenderer;
	Vector3[] posicionesIniciales;
	Vector3[] posicionesEditadas;

	public float anguloRotacion2 = 90;


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
		float angulo2;

		// Recorrer cada punto de la antigua maya y calcular el nuevo
		for (int i = 0; i < posicionesIniciales.Length; i++)
		{
			nuevoRadio = radio + posicionesIniciales[i].y;

			angulo = (posicionesIniciales[i].x / perimetroNormalizado) * 2 * Mathf.PI;
			angulo += Mathf.PI/2;

			angulo2 = (posicionesIniciales[i].z / perimetroNormalizado) * 2 * Mathf.PI;
			angulo2 += Mathf.PI;

			pivote.x = nuevoRadio * Mathf.Sin(angulo) * Mathf.Sin(angulo2);
			pivote.z = nuevoRadio * Mathf.Cos(angulo);
			pivote.y = -nuevoRadio * Mathf.Sin(angulo) * Mathf.Cos(angulo2);

			if (restarRadio)
			{
				pivote.y -= radio;
			}
			posicionesEditadas[i] = pivote;
		}
		// Las normales quedan invertidas así que toca Flipear
		if (!normalesInvertidas && invertirNormales)
		{
			InvertirNormales();
		}

		posicionesEditadas = RotateMesh(posicionesEditadas, anguloRotacion2);

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

	Vector3[] RotateMesh(Vector3[] _rotateVerts, float angulo)
	{
		Vector3[] rotatedVerts = _rotateVerts;
		Quaternion qAngle = Quaternion.AngleAxis(angulo, Vector3.up);
		for (int i = 0; i < rotatedVerts.Length; i++)
		{
			rotatedVerts[i] = qAngle * _rotateVerts[i];
		}

		return rotatedVerts;
	}
}
