using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAction : MonoBehaviour, IPointerClickHandler
{
    PistasController pistasController;
    public void OnPointerClick(PointerEventData eventData)
    {
        pistasController = PistasController.Instance;
        pistasController.ResetStatusButton();
    }
}