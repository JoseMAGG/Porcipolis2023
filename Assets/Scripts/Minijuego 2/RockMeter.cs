using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMeter : MonoBehaviour
{

    public float rm;
    public GameObject needle;

    void Start()
    {
        needle = transform.Find("needle").gameObject;
    }

    void Update()
    {
        rm = PlayerPrefs.GetInt("RockMeter");

        needle.transform.localPosition = new Vector3((rm - 25)/25, 0, 0);

    }
}
