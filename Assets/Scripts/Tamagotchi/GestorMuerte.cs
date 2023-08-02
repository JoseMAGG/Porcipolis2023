using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GestorMuerte : MonoBehaviour
{

    public GameObject spriteMuerto;

    //eliminar cuando se apruebe el cambio
    public bool matarCerdito = false;
    public bool revivirCerdito = false;
    public static GestorMuerte instance;
    public bool estaVivo;

    public GameObject[] objetosDesactivar;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        estaVivo = !GestorTamagotchi.gestor.estadosActuales.VerificarEstado("muerto");
        if (!estaVivo)
        {
            for (int i = 0; i < objetosDesactivar.Length; i++)
            {
                objetosDesactivar[i].SetActive(false);
            }
        }
        if (matarCerdito)
        {
            CerditoMuerto();
            matarCerdito = false;
        }
    }
    public void CerditoMuerto()
    {
        spriteMuerto.SetActive(true);
        GestorTamagotchi.gestor.SistemasNivelACero();
        GestorTamagotchi.gestor.estadosActuales.AgregarEstado("muerto");
        TamagotchiEvent.instance.CerdoMuerto();
        GestorTamagotchi.gestor.animCerdo.Muerto();
        print("se te murio el cerdito");


    }

}
