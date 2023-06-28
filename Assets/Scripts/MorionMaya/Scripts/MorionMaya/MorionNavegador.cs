using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Morion/Agente-Navegador")]
public class MorionNavegador : MonoBehaviour {
	[HideInInspector]
	public MorionNodo nodoActual;
	public bool iniciarCorriendo;
	public List<MorionNodo> ruta;
	public float velocidad;
	[HideInInspector]
	public int elemento = 0;
	public float cuantoVa;

	public MorionAgenteNavegacion agente;
	public Transform pivote;

	public bool pausado = false;
	void Start () 
	{
		float d = 1000000;
		List<MorionNodo> nodos = MorionMaya.singleton.nodos;
		for (int i = 0; i < nodos.Count; i++) {
			if ((nodos [i].transform.position - transform.position).sqrMagnitude < d) 
			{
				nodoActual = nodos [i];
				d = (nodos [i].transform.position - transform.position).sqrMagnitude;
			}
		}
		ruta = new List<MorionNodo> ();
		ruta.Add (nodoActual);

		if (iniciarCorriendo) {
			Correr ();
		} 
	}

	void Update () 
	{
		if (elemento >= ruta.Count || pausado)
			return;
		if ((transform.eulerAngles - ruta [elemento].transform.eulerAngles).magnitude > 5f) {
			transform.rotation = Quaternion.LerpUnclamped (nodoActual.transform.rotation, ruta[elemento].transform.rotation, cuantoVa);
			cuantoVa += Time.deltaTime * velocidad;
		} else {
			pausado = true;
			StartCoroutine(Esperar());
		}
	}

	IEnumerator Esperar()
	{
		print("enter center triquin fortin");
		yield return new WaitUntil(() => ((pivote.position - agente.transform.position).sqrMagnitude < 1f));
		cuantoVa = 0;
		nodoActual = ruta[elemento];
		elemento++;
		pausado = false;
	}

	public void Correr()
	{
		elemento = 0;
		MorionNodo desde = nodoActual;
		MorionNodo hasta = MorionMaya.singleton.nodos[Random.Range(0,MorionMaya.singleton.nodos.Count-1)];

		ruta = MorionMaya.singleton.CrearRuta (desde, hasta);
	}


	public void SetDestino(Vector3 nuevaPosicion)
	{
		elemento = 0;
		MorionNodo desde = nodoActual;
		MorionNodo hasta = MorionMaya.singleton.GetNodoCercano(nuevaPosicion);

		ruta = MorionMaya.singleton.CrearRuta(desde, hasta);
	}
}
