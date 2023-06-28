using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent rightClick;
    [HideInInspector]
    public UnityEvent leftClick;
    [HideInInspector]
    public UnityEvent middleClick;
    

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            leftClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Middle)
            middleClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right)
            rightClick.Invoke();
    }
}