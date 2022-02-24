using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonListener_UEvent : ButtonListener
{
    [SerializeField] private OnButtonActivateUEvent onButtonActivate;
    
    protected override void Activate()
    {
        Debug.Log("Activated");
        onButtonActivate.Invoke(buttonEvents);
    }
}

[System.Serializable]
public class OnButtonActivateUEvent : UnityEvent<List<ButtonEvent>> { }