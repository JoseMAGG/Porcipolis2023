using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificarEstasdos : MonoBehaviour
{
    public Color colorDia;
    public Color colorNoche;
    public Camera camaraPrincipal;
    public Animator animCerdo;
    public GameObject imgDormido;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GestorTamagotchi.gestor.estadosActuales.VerificarEstado("dormido"))
        {
            camaraPrincipal.backgroundColor = colorNoche;
            animCerdo.SetBool("despierto",false);
            imgDormido.SetActive(true);
        }
        else 
        { 
            camaraPrincipal.backgroundColor = colorDia; 
            animCerdo.SetBool("despierto",true);
            imgDormido.SetActive(false);
        }

    }
}
