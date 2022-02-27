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
    public GameObject lever1;
    public GameObject lever2;
    public bool destroyAfterInteract;
    private bool isPressed;
    private void Awake()
    {
        isPressed = initialIsPressed;
    }
    public override void Interact()
    {
        isPressed = !isPressed;
        Event.TriggerButtonTriggered(gameObject, isPressed);
        
        lever1.SetActive(!isPressed);
        lever2.SetActive(isPressed);

        if (destroyAfterInteract)
            Destroy(this);
    }
}


