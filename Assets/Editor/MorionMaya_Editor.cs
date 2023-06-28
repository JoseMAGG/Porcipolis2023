using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MorionMaya))]

public class MorionMaya_Editor : Editor 
{
	public static int pesta;
	MorionMaya maya;

	public SerializedObject soMaya;


	// Campos que aparecerán
	public SerializedProperty nodos;
	public SerializedProperty llenado;
	public SerializedProperty pisos;
	public SerializedProperty radio;
	public SerializedProperty prefabNodo;
	public SerializedProperty rango;
	public SerializedProperty mostrarGizmos;
	public SerializedProperty mostrarLineas;
	public SerializedProperty mostrarRuta;
	public SerializedProperty padreNodos;
	public SerializedProperty nodoInicial;
	public SerializedProperty nodoFinal;
	public SerializedProperty ruta;
	public SerializedProperty debugearVariables;

	private void OnEnable()
	{
		maya = (MorionMaya)target;
		soMaya = new SerializedObject (target);

		EditorGUIUtility.LookLikeInspector();
		// Inicializar los campos
		nodos 			= soMaya.FindProperty("nodos");
		llenado 		= soMaya.FindProperty("llenado");
		pisos 			= soMaya.FindProperty("pisos");
		radio 			= soMaya.FindProperty("radio");
		prefabNodo 		= soMaya.FindProperty("prefabNodo");
		rango	 		= soMaya.FindProperty("rango");
		mostrarGizmos	= soMaya.FindProperty("mostrarGizmos");
		mostrarLineas	= soMaya.FindProperty("mostrarLineas");
		mostrarRuta		= soMaya.FindProperty("mostrarRuta");
		padreNodos		= soMaya.FindProperty("padreNodos");
		nodoInicial		= soMaya.FindProperty("nodoInicial");
		nodoFinal		= soMaya.FindProperty("nodoFinal");
		ruta			= soMaya.FindProperty("ruta");
		debugearVariables= soMaya.FindProperty("debugearVariables");

	}

	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();
		//base.OnInspectorGUI ();
		EditorGUILayout.PropertyField (mostrarGizmos);
		EditorGUILayout.PropertyField (mostrarLineas);
		EditorGUILayout.PropertyField (mostrarRuta);

		string[] nombres = new string[]{"Maya","Configuración", "Ruta" };
		pesta = GUILayout.Toolbar (pesta, nombres);

		switch (pesta) 
		{
		case 0:
			EditorGUILayout.PropertyField (llenado);

			EditorGUILayout.PropertyField (pisos);
			EditorGUILayout.PropertyField (radio);
			if (maya.padreNodos == null) {
				if (GUILayout.Button ("Crear Nodos")) {
					maya.AutoLlenado();
					maya.Inicializar ();
					soMaya.ApplyModifiedProperties ();
					Selection.activeObject = null;
				}
			} else {
				if (GUILayout.Button ("Eliminar Nodos")) {
					maya.DeshacerNodos ();
				}
				// los nodos están creados
				EditorGUILayout.PropertyField (nodos,true);
				GUILayout.Label ("Rango de conexión de las aristas");
				EditorGUILayout.PropertyField (rango);
				if (GUILayout.Button ("Conectar")) {
					maya.ConectarNodos ();
					SceneView.RepaintAll ();
				}
			}

			if (GUILayout.Button("Recalcular Activos"))
			{
				maya.RecalcularActivos();
				SceneView.RepaintAll();
			}
			break;
		case 1:
			EditorGUILayout.PropertyField (prefabNodo);
			EditorGUILayout.PropertyField (padreNodos);
			break;
		case 2:
			EditorGUILayout.PropertyField (nodoInicial);
			EditorGUILayout.PropertyField (nodoFinal);

			if (GUILayout.Button ("Calcular Ruta")) {
				maya.CalcularRuta ();
				SceneView.RepaintAll ();
			}

			EditorGUILayout.PropertyField (ruta,true);
			break;
		default:
			break;
		}
		EditorGUILayout.PropertyField (debugearVariables);

		if(maya.debugearVariables)
			base.OnInspectorGUI ();

		soMaya.ApplyModifiedProperties ();
	}
}
