using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoHexagonal : MonoBehaviour
{
    public Animator miAnimator;
    public Vector2 frecuenciaCambio;

    public Hexagono hexagonoActual;
    public Hexagono hexagonoObjetivo;
    public float tiempoDemoraAndando = 3;
    public AnimationCurve curvaAltura;

    public int fase = 0;
    float tiempoRotando = 0;
    float tiempoAndando = 0;

    Quaternion qa;
    Quaternion qo;

    Vector3 pa;
    Vector3 po;
    bool dormido = false;
    bool muerto = false;

    void Start()
    {
        TamagotchiEvent.instance.OnCerdoMuerto += DisableComponent;

         dormido = GestorTamagotchi.gestor.estadosActuales.VerificarEstado("dormido");
         muerto = GestorTamagotchi.gestor.estadosActuales.VerificarEstado("muerto");

        Collider[] colisiones = Physics.OverlapSphere(transform.position, 4);
        for (int i = 0; i < colisiones.Length; i++)
        {
            if (hexagonoActual == null || (hexagonoActual.transform.position - transform.position).sqrMagnitude > (transform.position - colisiones[i].transform.position).sqrMagnitude)
            {
                Hexagono h = colisiones[i].GetComponent<Hexagono>();
                if (h!=null)
                {
                    hexagonoActual = h;
                }
            }
        }
        if (!dormido && !muerto)
        {
            CambiarFase(0);
        }
    }

    public void DisableComponent()
    {
        TamagotchiEvent.instance.OnCerdoMuerto -= DisableComponent;
        this.GetComponent<MovimientoHexagonal>().enabled = false;
    }


    IEnumerator EsperarQueSePueda(int f)
    {
        dormido = GestorTamagotchi.gestor.estadosActuales.VerificarEstado("dormido");
        muerto = GestorTamagotchi.gestor.estadosActuales.VerificarEstado("muerto");
        yield return new WaitUntil(() => this.enabled);
        if(!dormido && !muerto) { 
            CambiarFase(f);
        }
    }

    public void CambiarFase(int f)
    {
        if (!this.enabled)
        {
            StartCoroutine(EsperarQueSePueda(f));
            return;
        }
        switch (f)
        {
            case 0:
                fase = 0;
                miAnimator.SetFloat("velocidad", 0);
                Invoke("CambiarLugar", Random.Range(frecuenciaCambio.x, frecuenciaCambio.y));
                break;
            case 1:
                qa = transform.rotation;
                qo = Quaternion.LookRotation(
                    (hexagonoObjetivo.transform.position - hexagonoActual.transform.position).normalized, 
                     hexagonoObjetivo.transform.position.normalized);
                tiempoRotando = 0;
                miAnimator.SetFloat("velocidad", 1);
                fase = 1;
                break;
            case 2:
                pa = transform.position;
                po = hexagonoObjetivo.transform.position;
                tiempoAndando = 0;
                fase = 2;
                break;
            default:
                break;
        }
    }

    public IEnumerator BuscarADondeIr()
    {
        yield return new WaitForSeconds(0.5f);
        List<Hexagono> hexagonosPosibles = new List<Hexagono>();
        foreach (Hexagono hex in hexagonoActual.vecinos)
        {
            if (!hex.ocupado && hex.tieneValla == hexagonoActual.tieneValla)
            {
                hexagonosPosibles.Add(hex);
            }
        }
        if (hexagonosPosibles.Count==0)
        {
            CambiarFase(0);
        }
        else
        {
            hexagonoObjetivo = hexagonosPosibles[Random.Range(0, hexagonosPosibles.Count)];
            CambiarFase(1);
        }
    }

    void CambiarLugar()
    {
        StartCoroutine(BuscarADondeIr());
    }
    
    void Update()
    {
        if (GestorTamagotchi.gestor.estadosActuales.VerificarEstado("muerto"))
        {
            Debug.Log("esta muerto");
            return;
        }
        if (GestorTamagotchi.gestor.estadosActuales.VerificarEstado("dormido"))
        {
            Debug.Log("esta dormido");
            return;
        }
        switch (fase)
        {
            case 1:
                transform.rotation = Quaternion.Lerp(qa, qo, tiempoRotando);
                tiempoRotando += Time.deltaTime / tiempoDemoraAndando;
                if (tiempoRotando>1)
                {
                    CambiarFase(2);
                }
                break;
            case 2:
                transform.position = Vector3.Lerp(pa,po,tiempoAndando);
                transform.Translate(0, curvaAltura.Evaluate(tiempoAndando), 0);

                RaycastHit hit;
                if (Physics.Raycast(transform.position + transform.forward, transform.forward, out hit, 1))
                {
                    if (hit.collider.CompareTag("Cerdo") && hit.transform != transform)
                    {
                        print("Delante");
                        CambiarFase(0);
                    }
                }

                tiempoAndando += Time.deltaTime / tiempoDemoraAndando;
                if (tiempoAndando > 1)
                {
                    CambiarFase(0);
                    hexagonoActual = hexagonoObjetivo;
                }
                break;
            default:
                break;
        }
    }
}
