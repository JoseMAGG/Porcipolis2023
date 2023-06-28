using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SText : MonoBehaviour
{
    public string nombre;

    void Start()
    {
        PlayerPrefs.SetInt(nombre, 0);
    }

    void Update()
    {
        GetComponent<Text>().text = PlayerPrefs.GetInt(nombre) + "";
    }
}
