using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Listens to events from DialogueSO
/// Then invokes events attached to it
/// </summary>
public class DialogueEventListener : MonoBehaviour
{
    [SerializeField] private DialogueSO dialogueSO;

    public OnDialogueStartUEvent onDialogueStart;
    public OnDialogueEndUEvent onDialogueEndEvent;
    public OnDialogueContinueUEvent onDialogueContinue;
    public OnDialogueInteracted onDialogueInteracted;
    public void OnDialogueStart(Dialogue dialogue)
    {
        onDialogueStart.Invoke(dialogue);
    }
    public void OnDialogueEnd(Dialogue dialogue)
    {
        onDialogueEndEvent.Invoke(dialogue);
    }
    public void OnDialogueContinue(Dialogue dialogue)
    {
        onDialogueContinue.Invoke(dialogue);
    }
    /// <summary>
    /// Used by dialogue manager
    /// Can hook up to other stuff if needed
    /// </summary>
    /// <param name="dialogue"></param>
    public void OnDialogueInteracted(Dialogue dialogue)
    {
        onDialogueInteracted.Invoke(dialogue);
    }

    //Necessary codes

    private void OnEnable()
    {
        dialogueSO.AddListener(this);
    }
    private void OnDisable()
    {
        dialogueSO.RemoveListener(this);
    }
}

/// <summary>
/// Unity event that passes the dialogue data
/// Inherits from UnityEvent class
/// </summary>
[System.Serializable]
public class OnDialogueStartUEvent : UnityEvent <Dialogue> { }
[System.Serializable]
public class OnDialogueEndUEvent : UnityEvent <Dialogue> { }
[System.Serializable]
public class OnDialogueContinueUEvent : UnityEvent <Dialogue> { }
[System.Serializable]
public class OnDialogueInteracted : UnityEvent <Dialogue> { }