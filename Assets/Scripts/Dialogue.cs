using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Entire dialogue scene
/// Dialogue contains an entire set of dialogue message between multiple talking NPC
/// - One for each dialogue scene
/// - Can fit many NPC talking dialogues
/// 
/// Hierarchy:
/// Dialogue -> DialogueMessage[] -> sentences[]
/// </summary>
[System.Serializable]
public class Dialogue
{
    public string description; //Sets a description used for editor to know what the dialogue is about
    public Transform cinematicTarget;
    public DialogueMessage[] dialogueMessage;

}

/// <summary>
/// - Dialogue message contains an array of strings and an npc name
/// - One for each npc talking turn
/// - Can fit many sentences
/// </summary>
[System.Serializable]
public class DialogueMessage
{
    public string npcName;

    [TextArea(4, 10)]
    public string[] sentences;
}
