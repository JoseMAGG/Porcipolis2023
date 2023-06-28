using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteM : MonoBehaviour
{
    public Rigidbody2D rb;
    public float velocidad;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -velocidad);
    }

    void Update()
    {
        
    }
}
