using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlAldea : MonoBehaviour
{
    public static ControlAldea singleton;
    public Modos modo;
    public GameObject particulasExplocion;
    public int cualCrear = 0;
    public Image imModo;
    public Sprite[] imagenesEstados;
    

    public static bool MouseEnUI()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return (results.Count > 0);
    }

    public void Crear(int cual)
    {
        cualCrear = cual;
        CambiarModo(Modos.crear);
    }

    public void ModoTalar()
    {
       CambiarModo(Modos.talar);
    }

    public void ModoCrearVallas()
    {
        CambiarModo(Modos.crearVallas);
    }

    public void ModoQuitarVallas()
    {
        CambiarModo(Modos.quitarVallas);
    }
    
    public void CambiarModo(Modos m)
    {
        imModo.sprite = imagenesEstados[(int)m];
        modo = m;
    }

    private void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}

public enum Modos
{
    exploracion  = 0,
    crearVallas  = 1,
    quitarVallas = 2,
    talar        = 3,
    crear        = 4
}