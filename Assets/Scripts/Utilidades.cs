using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilidades : MonoBehaviour
{
    public float escalaTiempo=1;
    // Start is called before the first frame update
    [ContextMenu("Escalar el Tiempo")]
    public void EscalarTiempo(){
        Time.timeScale = escalaTiempo;
    }
}
