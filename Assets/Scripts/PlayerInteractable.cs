using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class of player interactable
/// </summary>
public class PlayerInteractable: MonoBehaviour
{
    public virtual void Interact()
    {
        Debug.Log("Player Interacted");
    }
}
