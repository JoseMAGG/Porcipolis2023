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
    public Slider slHidratacion;
    public Slider slHigiene;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        slAlimentacion.value = tamagotchi.alimentacion.nivel;
        slAnimo.value =  tamagotchi.animo.nivel;
        slSalud.value =  tamagotchi.salud.nivel;
        slHidratacion.value = tamagotchi.alimentacion.hidratacion.nivel;
        slHigiene.value = tamagotchi.salud.higiene.nivel;
    }

 
}
