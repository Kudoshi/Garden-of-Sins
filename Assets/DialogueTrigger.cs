using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to trigger dialogue events
/// Holds the dialogue conversation
/// 
/// Inherits from player
/// </summary>
public class DialogueTrigger : PlayerInteractable
{
    [SerializeField] private DialogueSO dialogueSO;
    [SerializeField] private Dialogue dialogue;

    /// <summary>
    /// Base class's interact
    /// All interaction point inputs here
    /// </summary>
    public override void Interact()
    {
        TriggerOnDialogueInteracted();
    }


    // ----------------------------
    //        EVENT TRIGGER
    // ----------------------------

    /// <summary>
    /// Triggers the On Dialogue Start event
    /// </summary>
    public void TriggerOnDialogueInteracted()
    {
        dialogueSO.TriggerOnDialogueInteracted(dialogue);
    }

    //

}
