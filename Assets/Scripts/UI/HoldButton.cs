using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class HoldButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    [SerializeField] UnityEvent OnHold = new UnityEvent();
    bool hold = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        hold = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        hold = false;
    }

    private void Update()
    {
        if (hold) OnHold.Invoke();
    }
}
