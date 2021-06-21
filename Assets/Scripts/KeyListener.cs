using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class KeyListener : MonoBehaviour
{
    public KeyCode key;
    [SerializeField] UnityEvent OnKeyPress = new UnityEvent();

    private void Update()
    {
        if (Input.GetKeyDown(key)) OnKeyPress.Invoke();
    }
}
