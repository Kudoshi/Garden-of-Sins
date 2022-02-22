using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dialogue events manager
/// 
/// Manages the listeners of the dialogue events
/// Controls the flow of the dialogue events
/// Notifies the listeners of the events
/// 
/// Dialogue Interact:
/// DialogueTrigger -> DialogueSO -> DialogueEventListener -> DialogueManager (Decides what event to trigger)
/// 
/// Dialogue Event Trigger:
/// DialogueManager -> DialogueSO -> DialogueEventListener -> Other scripts
/// 
/// Holds a list of the listeners. This object informs the listeners when an event happens
/// 
/// Requires:
/// Dialogue Manager to function
/// 
/// Input: 
/// - DialogueTrigger
/// - DialogueManager
/// 
/// Output: 
/// - DialogueEventListener
/// - *DialogueManager
/// 
/// Events:
/// - OnDialogueStart
/// - OnDialogueContinue
/// - OnDialogueEnd
/// - OnDialogueInteracted 
/// </summary>

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "Dialogue SO")]
public class DialogueSO : ScriptableObject
{
    private List<DialogueEventListener> listeners = new List<DialogueEventListener>();
    

    // ----------------------------
    //        TRIGGER EVENT
    // ----------------------------

    // CALLED BY DIALOGUE MANAGER
    // DialogueManager -> DialogueSO -> DialogueEventListener -> Other scripts

    public void TriggerOnDialogueStart(Dialogue dialogue)
    {
        for (int i = listeners.Count-1; i >= 0; i--)
        {
            listeners[i].OnDialogueStart(dialogue);
        }
    }

    public void TriggerOnDialogueEnd(Dialogue dialogue)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnDialogueEnd(dialogue);
        }
    }

    public void TriggerOnDialogueContinue(Dialogue dialogue)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnDialogueContinue(dialogue);
        }
    }

    // CALLED FROM DIALOGUE TRIGGER
    // DialogueTrigger -> DialogueSO -> DialogueEventListener -> DialogueManager (Decides what event to trigger)

    /// <summary>
    /// Passes the trigger to the game manager to decide what event to call
    /// </summary>
    /// <param name="dialogue"></param>
    public void TriggerOnDialogueInteracted(Dialogue dialogue)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnDialogueInteracted(dialogue);
        }
    }


    // ----------------------------
    //      REQUIRED FUNCTIONS
    // ----------------------------

    // Required functions

    public void AddListener(DialogueEventListener subscriber)
    {
        listeners.Add(subscriber);
    }

    public void RemoveListener(DialogueEventListener subscriber)
    {
        listeners.Remove(subscriber);
    }
}
