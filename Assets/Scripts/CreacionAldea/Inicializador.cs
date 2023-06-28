using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicializador : MonoBehaviour
{
    public GameObject[] prefabs;
    [Range(0, 1)]
    public float prpbArbol;

    public static Inicializador singleton;
    public int tipoRecursoACrear;
    public int precioCrear;


    public void CambiarTipoRecurso(int tr)
    {
        tipoRecursoACrear = tr;
    }

    public void CambiarPrecio(int cuanto)
    {
        precioCrear = cuanto;
    }
    private void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        // Crear árboles en la escena aleatoriamente
        string granja = MorionTools.Cargar("granja");
        if (granja == "")
        {
            foreach (Hexagono hexagono in HexagonoControl.singleton.hexagonos)
            {
                if (!hexagono.ocupado && prpbArbol > Random.Range(0f,1f))
                {
                    hexagono.Ocupar(0);
                }
            }
            GuardarDatos();
        }
        else
        {
            CargarDatos(granja);
        }
    }
    
    public void CargarDatos(string datos)
    {
        string[] datosGranja = datos.Split('|');
        for (int i = 0; i < datosGranja.Length; i++)
        {
            //print(i + "/" + HexagonoControl.singleton.hexagonos.Count);
            if (i.EntreDos(0,HexagonoControl.singleton.hexagonos.Count-1))
            {
                HexagonoControl.singleton.hexagonos[i].CargarDesdeString(datosGranja[i]);
            }
        }
    }

    public void GuardarDatos()
    {
        string s = "";
        for (int i = 0; i < HexagonoControl.singleton.hexagonos.Count; i++)
        {
            if (s!="")
            {
                s += "|";
            }
            s = s + HexagonoControl.singleton.hexagonos[i].ConvertirString();
        }
        MorionTools.Guardar("granja", s);
    }
}
