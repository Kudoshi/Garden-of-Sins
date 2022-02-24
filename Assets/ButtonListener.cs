using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Can be a base class to call Unity Events
/// Can be extended to remove 
/// 
/// buttonEvents only stores buttons which are active. Inactive buttons are removed
/// 
/// Button Triggered -> OnButtonTriggered -> Check if gameobject matches buttonsRequired -> If yes, saves the buttonEvents -> check if all required button is pushed
/// </summary>
public class ButtonListener : MonoBehaviour
{
    [SerializeField] protected GameObject[] buttonsRequired; //Required buttons to be turned on before activating something
    

    protected List<ButtonEvent> buttonEvents; // Stores the buttons 
     
    protected void OnEnable()
    {
        Event.OnButtonTriggered += Event_OnButtonTriggered;
    }

    protected void OnDisable()
    {
        Event.OnButtonTriggered -= Event_OnButtonTriggered;
    }

    protected void Awake()
    {
        buttonEvents = new List<ButtonEvent>();
    }
    protected virtual void Event_OnButtonTriggered(ButtonEvent bEvent)
    {
        // Check if game object matches buttons Required
        GameObject triggerer = Array.Find(buttonsRequired, button => button == bEvent.triggerer);

        if (triggerer == null)
            return;

        // If pressed, add to list
        if (bEvent.isPressed)
        {
            //Check if already in list

            ButtonEvent buttonEvent = buttonEvents.Find(x => x.triggerer == bEvent.triggerer);
            if (buttonEvent == null)
            {
                buttonEvents.Add(bEvent);
            }
            else
                return;
        }
        // If unpress, delete from list
        else if (!bEvent.isPressed)
        {
            //Check if already in list
            ButtonEvent buttonEvent = buttonEvents.Find(x => x.triggerer == bEvent.triggerer);
            if (buttonEvent != null)
            {
                buttonEvents.Remove(buttonEvent);
            }
            else
                return;

        }
        CheckCondition();
    }

    protected virtual void CheckCondition()
    {
        if (buttonsRequired.Length == buttonEvents.Count)
        {
            Activate();
        }
    }

    protected virtual void Activate()
    {
        
    }
}
