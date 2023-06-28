using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[AddComponentMenu("Morion/Maya de Navegacion")]
public class MorionMaya : MonoBehaviour {
	public bool siguienteComando;
	public DijkstraCuenta[] matriz;


	// Elementos para crear en la maya
	public int[] 		nodosPorPiso;
	public int 			pisos;
	public float 		radio = 9;
	public GameObject 	prefabNodo;

	// Elementos para editar la maya 
	[Range(0.5f,9f)]
	public float rango = 0.5f;

	//Ruta
	public MorionNodo nodoInicial;
	public MorionNodo nodoFinal;
	public List<MorionNodo> ruta;

	//Configuración de la maya

	public List<MorionNodo> nodos;
	[SerializeField]
	public GameObject padreNodos;

	public int llenado;

	public static MorionMaya singleton;
	public bool mostrarGizmos = true;
	public bool mostrarLineas = true;
	public bool mostrarRuta = true;

	public bool debugearVariables= false;

	void Awake(){
		singleton = this;
	}

	void OnDrawGizmosSelected()
	{
		if (singleton == null) {
			Awake ();
		}
		prefabNodo = Resources.Load<GameObject> ("Nodo");
	}

	public void AutoLlenado()
	{
		nodosPorPiso = new int[pisos];
		int k = Mathf.FloorToInt (nodosPorPiso.Length / 2);
		if (nodosPorPiso.Length%2 == 1) {
			k++;
		}
		for (int i = 0; i < k; i++) {
			int cuanto = 1 + Mathf.RoundToInt ( llenado *(Mathf.Sqrt((i + 0f)/(k-1f))));
			nodosPorPiso [i] = cuanto;
			nodosPorPiso [nodosPorPiso.Length - 1 - i] = cuanto;
		}
	}

	public void Inicializar () 
	{
		if (nodosPorPiso.Length == 0)
			return;
		if (nodos == null)
			nodos = new List<MorionNodo> ();
		
		padreNodos = new GameObject();
		padreNodos.name = "Maya de Navegación";
		for (int i = 0; i < nodosPorPiso.Length; i++) {
			for (int j = 0; j < nodosPorPiso[i]; j++) 
			{
				GameObject go = Instantiate (prefabNodo, Vector3.zero, Quaternion.identity, padreNodos.transform) as GameObject;
				go.name = "nodo_" + i + "-" + j;
				go.transform.eulerAngles = Vector3.forward * (i * (180f / (nodosPorPiso.Length-1))) + (Vector3.up * (j*(360f /((float) nodosPorPiso[i]))));
				MorionNodo mon = go.GetComponent<MorionNodo> ();
				mon.Inicializar (radio);
				nodos.Add(mon);
			}
		}
	}

	public void RecalcularActivos()
	{
		for (int i = 0; i < nodos.Count; i++)
		{
			nodos[i].RevizarSiActivo();
		}
		ConectarNodos();
	}

	public MorionNodo GetNodoCercano(Vector3 posicion)
	{
		MorionNodo nodoDefinitivo = nodos[0];
		float d = 10000000;
		for (int i = 0; i < nodos.Count; i++)
		{
			if ((nodos[i].pivote.transform.position - posicion).sqrMagnitude < d)
			{
				nodoDefinitivo = nodos[i];
				d = (nodos[i].pivote.transform.position - posicion).sqrMagnitude;
				//print("se indica que es " + i + " para " + posicion + "con una distancia " + d);
			}
		}
		return nodoDefinitivo;
	}

	public void DeshacerNodos()
	{
		DestroyImmediate (padreNodos);
		nodos.Clear ();
	}

	public void ConectarNodos(){
		for (int i = 0; i < nodos.Count; i++) {
			nodos [i].ConectarNodos (nodos, rango);
		}
	}
	

	public void  CalcularRuta()
	{
		for (int i = 0; i < nodos.Count; i++) {
			nodos [i].siguienteEnRuta = null;
		}
		ruta = new List<MorionNodo> ();
		/*
		if (nodoInicial == null || nodoFinal == null) 
		{
			return;
		}*/
		
		MorionNodo definitivo = nodoInicial;

		matriz = new DijkstraCuenta[nodos.Count];
		for (int i = 0; i < matriz.Length; i++)
		{
			matriz[i] = new DijkstraCuenta();
		}
		int salida=0;

		int indice = NumeroNodo(definitivo);
		matriz[indice].nodoAnterior = NumeroNodo(nodoInicial);
		matriz[indice].pesoFinal = 0;
		matriz[indice].pesoTemporal = 0;

		while (definitivo != nodoFinal && salida < nodos.Count)
		{
			// Etiqueto las conecciones del nodo definitivo
			for (int i = 0; i < definitivo.conecciones.Count; i++)
			{
				int indiceTemporal = NumeroNodo(definitivo.conecciones[i]);
				int pesoSiguientes = matriz[indice].pesoFinal + 1;
				if (matriz[indiceTemporal].pesoFinal == -1 && (matriz[indiceTemporal].pesoTemporal==-1 ||  matriz[indiceTemporal].pesoTemporal > pesoSiguientes))
				{
					matriz[indiceTemporal].pesoTemporal = pesoSiguientes;
					matriz[indiceTemporal].nodoAnterior = NumeroNodo(definitivo);
				}
			}
			// Buscar el nuevo definitivo, con el menor peso Temporal y sin peso Final
			int pesoMenor = 100000;
			int indiceMenor=0;
			for (int i = 0; i < matriz.Length; i++)
			{
				if (matriz[i].pesoFinal == -1 && matriz[i].pesoTemporal!=-1 && matriz[i].pesoTemporal < pesoMenor)
				{
					pesoMenor = matriz[i].pesoTemporal;
					indiceMenor = i;
				}
			}
			matriz[indiceMenor].pesoFinal = matriz[indiceMenor].pesoTemporal;
			//matriz[indiceMenor].nodoAnterior = NumeroNodo(definitivo);
			definitivo = nodos[indiceMenor];
			indice = NumeroNodo(definitivo);

			//print("Seleccionado " + definitivo.gameObject.name + ", con un peso de " + pesoMenor);

			//yield return new WaitUntil(() => siguienteComando);
			//siguienteComando = false;
			// Control para evitar ir al infierno
			salida++;
		}

		List<MorionNodo> ruta2 = new List<MorionNodo>();
		ruta2.Add(nodoFinal);
		salida = 0;
		while (definitivo != nodoInicial && salida<nodos.Count)
		{
			indice = NumeroNodo(definitivo);
			//print(indice + "---" + matriz[indice].Texto());
			ruta2.Add(nodos[matriz[indice].nodoAnterior]);
			definitivo = nodos[matriz[indice].nodoAnterior];
			salida++;
		}

		for (int i = 0; i < ruta2.Count; i++)
		{
			ruta.Add(ruta2[ruta2.Count - 1 - i]);
			if (i>0)
			{
				ruta[i - 1].siguienteEnRuta = ruta[i];
			}
		}
		ruta[0].siguienteEnRuta = ruta[1];
	}

	public int NumeroNodo(MorionNodo no)
	{
		return (nodos.IndexOf(no));
	}

	public List<MorionNodo> CrearRuta(MorionNodo desde, MorionNodo hasta)
	{
		nodoInicial = desde;
		nodoFinal = hasta;
		CalcularRuta ();
		return ruta;
	}
}
