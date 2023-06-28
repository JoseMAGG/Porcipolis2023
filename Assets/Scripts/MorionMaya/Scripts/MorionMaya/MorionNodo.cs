using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[AddComponentMenu("Morion/Nodo")]
public class MorionNodo : MonoBehaviour {
	public Transform    pivote;
	public List<MorionNodo> conecciones;
	public bool activo = true;

	public MorionNodo siguienteEnRuta;
	public LayerMask capaDesactivarNotos;

	public void Inicializar (float radio) 
	{
		pivote.localPosition = Vector3.up * radio;
		RevizarSiActivo();
	}

	public void RevizarSiActivo()
	{
		Vector3 direccion = -pivote.position;
		direccion = direccion.normalized;
		Ray rayo = new Ray(pivote.position - direccion*5, direccion);
		RaycastHit hit;
		activo = !(Physics.Raycast(rayo, out hit, 4, capaDesactivarNotos));
	}

	void OnDrawGizmos () 
	{
		if (MorionMaya.singleton != null && !MorionMaya.singleton.mostrarGizmos)
			return;
		
		Gizmos.color = Color.green;
		if (siguienteEnRuta != null)
			Gizmos.color = Color.red;
		if (!activo) {
			Gizmos.color = Color.black;
		}
		Gizmos.DrawSphere (pivote.position, 0.2f);

		if (conecciones != null && conecciones.Count > 0) {
			if (MorionMaya.singleton != null && MorionMaya.singleton.mostrarLineas) {
				for (int i = 0; i < conecciones.Count; i++) {
					Gizmos.color = Color.green;
					Gizmos.DrawLine (pivote.transform.position, conecciones [i].pivote.transform.position);
				}
			}
			if (MorionMaya.singleton != null && MorionMaya.singleton.mostrarRuta && siguienteEnRuta != null && activo) {
				Gizmos.color = Color.red;
				Gizmos.DrawLine (pivote.transform.position, siguienteEnRuta.pivote.transform.position);

			}
		}
	}

	public void ConectarNodos(List<MorionNodo> lista, float rango)
	{
		conecciones = new List<MorionNodo> ();
		if (!activo)
		{
			return;
		}
		for (int i = 0; i < lista.Count; i++) 
		{
			if (lista [i] != this && lista[i].activo && (pivote.transform.position - lista [i].pivote.transform.position).magnitude <= rango) {
				conecciones.Add (lista [i]);
			}
		}
	}
}
[System.Serializable]
public class DijkstraCuenta
{
	public int pesoFinal;
	public int pesoTemporal;
	public int nodoAnterior;

	public string Texto()
	{
		return "pF: " + pesoFinal + ", pT: " + pesoTemporal + ", nAnt: " + nodoAnterior;
	}

	public DijkstraCuenta()
	{
		Reiniciar();
	}

	public void Reiniciar()
	{
		pesoFinal = -1;
		pesoTemporal = -1;
		nodoAnterior = -1;
	}
}