using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For levers / one click buttons
/// 
/// - Player triggers it
/// </summary>
public class ButtonTrigger : PlayerInteractable
{
    public bool initialIsPressed; // Initially the button is on what state
    private bool isPressed;
    private void Awake()
    {
        isPressed = initialIsPressed;
    }
    public override void Interact()
    {
        isPressed = !isPressed;
        Event.TriggerButtonTriggered(gameObject, isPressed);
    }
}


