using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialGotas : MonoBehaviour
{
    public MeshRenderer maya;
    public Material material;
    public float velocidad;

    Vector2 vec = Vector2.zero;

    void Start()
    {
        material = maya.material;
    }

    // Update is called once per frame
    void Update()
    {
        vec.y = vec.y + Time.deltaTime * velocidad;
        material.SetTextureOffset("_MainTex", vec);
    }
}
