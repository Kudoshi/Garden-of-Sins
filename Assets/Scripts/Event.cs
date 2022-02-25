using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Handles event for
/// 
/// - On Button Trigger
/// </summary>

[System.Serializable]
public class Event
{
    // Events

    public static event Action<ButtonEvent> OnButtonTriggered;

    /// <summary>
    /// When a button or slate is pressed
    /// </summary>
    /// <param name="triggerer">The caller</param>
    /// <param name="isPressed">Whether button is in active state or disabled now</param>
    public static void TriggerButtonTriggered(GameObject triggerer, bool isPressed)
    {
        OnButtonTriggered?.Invoke(new ButtonEvent(triggerer, isPressed));
    }

    public static event Action<Transform> OnPlayerDie;
    public static void TriggerPlayerDie(Transform player)
    {
        OnPlayerDie?.Invoke(player);
    }
}

[System.Serializable]
public class ButtonEvent
{
    public GameObject triggerer;
    public bool isPressed;

    public ButtonEvent(GameObject triggerer, bool isPressed)
    {
        this.triggerer = triggerer;
        this.isPressed = isPressed;
    }


}