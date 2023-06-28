using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidersTama : MonoBehaviour
{
    public GestorTamagotchi tamagotchi;
    public Slider slAlimentacion;
    public Slider slAnimo;
    public Slider slSalud;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        slAlimentacion.value = tamagotchi.alimentacion.nivel;
        slAnimo.value =  tamagotchi.animo.nivel;
        slSalud.value =  tamagotchi.salud.nivel;
    }

 
}
