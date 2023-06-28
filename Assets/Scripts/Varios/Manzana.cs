using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manzana : MonoBehaviour
{
    public float probabilidad = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0f,1f) > probabilidad)
        {
            Destroy(gameObject);
        }
    }
    private void OnMouseUp()
    {
        GestorEconomia.singleton.SumarRecurso(1, 1);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
