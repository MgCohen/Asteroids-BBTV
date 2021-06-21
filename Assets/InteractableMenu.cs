using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class InteractableMenu : MonoBehaviour, IDestroyable
{
    [SerializeField] UnityEvent OnHit = new UnityEvent();


    public void Hit()
    {
        OnHit.Invoke();
    }
}
