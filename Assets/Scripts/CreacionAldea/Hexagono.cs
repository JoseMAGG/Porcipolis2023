using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagono : MonoBehaviour
{
    public List<Hexagono> vecinos = new List<Hexagono>();
    public bool tieneValla        = false;
    public bool ocupado           = false;
    public int ocupadoPor         = -1;

    void Start()
    {
        BuscarVecinos();
        HexagonoControl.singleton.hexagonos.Add(this);
    }

    void BuscarVecinos()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, HexagonoControl.singleton.radio);
        foreach (Collider collider in cols)
        {
            if (collider.CompareTag("Hexagono") && collider.gameObject != gameObject)
            {
                vecinos.Add(collider.gameObject.GetComponent<Hexagono>());
            }
        }
    }

    private void OnMouseUp()
    {
        if (MorionTools.MouseEnUI()) return;
        if (ControlAldea.singleton.modo == Modos.crearVallas && !MovCamera.moviendo)
        {
            
            HexagonoControl.singleton.LimpiarVallas();
            tieneValla = true;
            HexagonoControl.singleton.CrearVallas();
        }
        else if (ControlAldea.singleton.modo == Modos.crear && !ocupado && !MovCamera.moviendo)
        {
            Ocupar(ControlAldea.singleton.cualCrear);
            GestorEconomia.singleton.UsarRecurso(Inicializador.singleton.tipoRecursoACrear, Inicializador.singleton.precioCrear);
            
            ControlAldea.singleton.CambiarModo(Modos.exploracion);

        }else if (ControlAldea.singleton.modo == Modos.quitarVallas && !MovCamera.moviendo)
        {
            tieneValla = false;
            HexagonoControl.singleton.LimpiarVallas();
            HexagonoControl.singleton.CrearVallas();
        }

    }

    public void Ocupar(int c)
    {
        ocupado = true;
        ocupadoPor = c;
        CrearElemento(c);
        
        Inicializador.singleton.GuardarDatos();
    }

    public void CrearElemento (int c)
    {
        if (c<0)
        {
            return;
        }
        GameObject arbolito = Instantiate(Inicializador.singleton.prefabs[c], transform.position, Quaternion.identity) as GameObject;
        //GestorEconomia.singleton.UsarRecurso(Inicializador.singleton.tipoRecursoACrear, Inicializador.singleton.precioCrear);
        arbolito.transform.up = transform.position.normalized;
        arbolito.GetComponent<ObjetoCreado>().padre = this;
        //Borrar esto que no va ahí!!!!
        ocupado = true;
    }

    public string ConvertirString()
    {
        string s = ocupado.AString() + "¬" + tieneValla.AString() + "¬" + ocupadoPor;
        return s;
    }

    public void CargarDesdeString(string s)
    {
        string[] svec = s.Split('¬');
        ocupado.ABooleano(svec[0]);
        tieneValla = "1" == (svec[1]);
        ocupadoPor = int.Parse(svec[2]);
        if (ocupadoPor!=-1) CrearElemento(ocupadoPor);
        if (tieneValla)
        {
            StartCoroutine(EnvallaConDelay());
        }
    }

    public IEnumerator EnvallaConDelay()
    {
        yield return new WaitForSeconds(0.2f);
        Envallar();
    }

    public void Desocupar()
    {
        ocupado = false;
        ocupadoPor = -1;
        Inicializador.singleton.GuardarDatos();
    }

    public void Envallar()
    {
        GameObject go = HexagonoControl.singleton.prValla;
        for (int i = 0; i < vecinos.Count; i++)
        {
            if (!vecinos[i].tieneValla)
            {
                GameObject nGO = Instantiate(
                    go,
                    transform.position + (vecinos[i].transform.position - transform.position) / 2,
                    Quaternion.identity) as GameObject;
                nGO.transform.LookAt( vecinos[i].transform.position - transform.position, transform.position.normalized);
                nGO.transform.Rotate(270, 0, 0);
                HexagonoControl.singleton.vallas.Add(nGO);
            }
            

        }
    }

    private void OnDrawGizmosSelected()
    {
        if (HexagonoControl.singleton != null)
        {
            Gizmos.DrawWireSphere(transform.position, HexagonoControl.singleton.radio);
        }
        
    }
}
