using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class HexagonoControl : MonoBehaviour
{
    public static HexagonoControl singleton;

    public float radio              = 3;
    public List<GameObject> vallas  = new List<GameObject>();
    public List<Hexagono> hexagonos = new List<Hexagono>();

    public GameObject prValla;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        Invoke("CargarAhora", 0.05f);
    }

    void CargarAhora()
    {
        CargarAldea();
    }

    public void LimpiarVallas()
    {
        foreach (GameObject valla in vallas)
        {
            Destroy(valla);
        }
        vallas.Clear();
    }

    public void CrearVallas()
    {
        foreach (Hexagono hexagono in hexagonos)
        {
            if (hexagono.tieneValla)
            {
                hexagono.Envallar();
            }
        }
    }

    public void GuardarAldea()
    {
        /*
        string aldea = "";
        for (int i = 0; i < hexagonos.Count; i++)
        {
            if (hexagonos[i].ocupado)
            {
                aldea += hexagonos[i].ocupadoPor.ToString();
            }
            else
            {
                aldea += "-1";
            }
            aldea += "|";
        }
        MorionTools.Guardar("Aldea", aldea);
        */
    }

    public void CargarAldea()
    {
        /*
        string aldea = MorionTools.Cargar("Aldea");
        string[] info = aldea.Split('|');
        if (info.Length >= hexagonos.Count)
        {
            for (int i = 0; i < hexagonos.Count; i++)
            {
                if (info[i] != "-1")
                {
                    hexagonos[i].Ocupar(int.Parse(info[i]));
                }
            }
        }
        */
    }
}
