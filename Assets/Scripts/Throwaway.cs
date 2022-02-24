using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Throwaway : MonoBehaviour
{
    public void ReceiveInput(List<ButtonEvent> events)
    {
        Debug.Log(events[0].isPressed);
    }
}
