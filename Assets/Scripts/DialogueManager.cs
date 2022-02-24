using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

/// <summary>
/// Dialogue Manager
/// 
/// Handles logic and display of dialogue
/// 
/// INTERACTED - 
/// 
/// Input: 
/// -OnDialogueInteracted | DialogueTrigger -> DialogueSO -> GameManager
/// 
/// Output:
/// - UI Display
/// - Trigger dialogue events in Dialogue SO
/// 
/// Flow:
/// DialogueEventListener -> OnInteractDialogue <-> TriggerStartDialogue  +-> [ DialogueSO ]
///                                            |
///                                            -> DisplayDialogue -> TriggerContinueDialogue -> [ DialogueSO ]
///                                            |
///                                            -> TriggerEndDialogue -> [ DialogueSO ]
/// </summary>
public class DialogueManager : MonoBehaviour
{
    
    [SerializeField] private DialogueSO dialogueSO;
    [SerializeField] private GameObject displayUI;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI dialogueTxt;
    [SerializeField] private float textAnimationTime = -1; //Time per letter. Leave as -1 if want to just skip a frame per letter
    [SerializeField] private Animator animator;

    private Dialogue dialogue;
    private Vector2Int dialogueIndex; //[DialogueMessage(person speaking), sentences]
    private bool isInDialogueMode = false;
    private bool animatingTyping = false;

    private void Awake()
    {
        displayUI.SetActive(false);
    }


    // ----------------------------
    //         INPUT RECEIVE
    // ----------------------------

    /// <summary>
    /// Check whether to start new dialogue or continue displaying the dialogue
    /// 
    /// Called from DialogueTrigger -> DialogueSO -> This script
    /// </summary>
    /// <param name="dialogue"></param>
    public void OnInteractDialogue(Dialogue dialogue)
    {
        //Starts new dialogue scene. Restarts the parameters
        if(isInDialogueMode == false || this.dialogue != dialogue)
        {
            isInDialogueMode = true;
            TriggerStartDialogue(dialogue);
        }

        //Checks if should update display and sets the new dialogue. Else trigger end dialogue
        bool shouldDisplay = GetNewDialogue();
        if (shouldDisplay)
        {
            DisplayDialogue();
        }
        else if (!shouldDisplay)
        {
            TriggerEndDialogue(dialogue);
        }
    }

    // ----------------------------
    //         EVENT TRIGGER
    // ----------------------------

    /// <summary>
    /// Setup the dialogue system when the dialogue is first initiated
    /// Resets the variable
    /// 
    /// </summary>
    /// <param name="dialogue">The dialogue class</param>
    private void TriggerStartDialogue(Dialogue dialogue)
    {
        //Initialize dialogue setup
        this.dialogue = dialogue;

        // Set current dialogueIndex
        dialogueIndex = new Vector2Int(-1,-1);
        displayUI.SetActive(true);
        animator.SetBool("IsOpen", true);

        // Resets Display

        dialogueSO.TriggerOnDialogueStart(dialogue);
    }
    private void Update()
    {
        //Debug.Log(animator.GetBool("IsOpen"));
    }

    private void TriggerEndDialogue(Dialogue dialogue)
    {
        isInDialogueMode = false;

        animator.SetBool("IsOpen", false);

        StartCoroutine(CheckAnimationCompleted("DialogueClose", CloseDialogueBox));
        dialogueSO.TriggerOnDialogueEnd(dialogue);
    }

    private void TriggerContinueDialogue(Dialogue dialogue)
    {
        dialogueSO.TriggerOnDialogueContinue(dialogue);
    }

    private void CloseDialogueBox()
    {
        displayUI.SetActive(false);
    }

    /// <summary>
    /// Checks if the current animation has ended. If yes, call the onComplete function
    /// 
    /// After set trigger. It will have to wait awhile before changing to next animation
    /// This is because animation state is updated after one frame. So need to wait 1 frame before checking
    /// </summary>
    /// <param name="currentAnim">The animation you want to check if its completed</param>
    /// <param name="onComplete">The function you want to invoke after animation is completed</param>
    /// <returns></returns>
    IEnumerator CheckAnimationCompleted(string currentAnim, Action onComplete)
    {
        yield return null;
        //Skips next frame if animation is still the same
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(currentAnim))
        {
            AnimatorClipInfo[] animatorinfo = animator.GetCurrentAnimatorClipInfo(0);
            yield return null;  
        }

        //Calls the onComplete function after the currentAnim has changed
        if (onComplete != null)
            onComplete();
    }

    /// <summary>
    /// Checks and sets the new dialogue index.
    /// Has a read and write mode
    /// 
    /// Returns true if there are dialogue to display
    /// Returns false if there are no dialogue to display
    /// 
    /// 1. Check if the dialogue has just been initiated
    /// 2. Sets the next sentence of the speaker
    /// 3. Sets the next speaker if sentences has run out
    /// </summary>
    ///  <param name="write">Bool whether if want to write variables. Put true to write the var</param>
    private bool GetNewDialogue(bool write = false)
    {
        //Check if this is a new dialogue initiated
        if (dialogueIndex.x == -1)
        {
            if (write)
            {
                dialogueIndex = new Vector2Int(0, 0);
                animatingTyping = false;
            }
            return true;
        }

        //Checks if is still in animating
        if (animatingTyping)
        {
            return true;
        }    

        // SENTENCES
        Vector2Int tempIndex = dialogueIndex;
        tempIndex.y++;
        //Check if person's sentences is finished. If yes. Change to next speaker
        if (tempIndex.y > GetSentenceArr(dialogueIndex.x).Length-1)
        {
            //Change next speaker, reset sentence
            tempIndex.x++;
            tempIndex.y = 0;


            // SPEAKER
            //Check if there is speaker
            if (tempIndex.x > dialogue.dialogueMessage.Length - 1)
            {
                return false;
            }
        }

        if (write)
        {
            dialogueIndex = tempIndex;
        }
        return true;
    }

    /// <summary>
    /// Displays the dialogue
    /// 
    /// Calls the animation function to animate the typing animation
    /// </summary>
    private void DisplayDialogue()
    { 
        StopAllCoroutines();

        // If not animating - Get new dialogue and animate
        if (!animatingTyping) 
        {
            GetNewDialogue(true);
            nameTxt.text = dialogue.dialogueMessage[dialogueIndex.x].npcName; //Gets npc name
            StartCoroutine(TypeSentence(dialogue.dialogueMessage[dialogueIndex.x].sentences[dialogueIndex.y]));
        }
        // if animating - Skip animation
        else if (animatingTyping)
        {
            dialogueTxt.text = dialogue.dialogueMessage[dialogueIndex.x].sentences[dialogueIndex.y];
            animatingTyping = false;
        }

        TriggerContinueDialogue(dialogue);
    }

    // ----------------------------
    //          ANIMATION
    // ----------------------------

    IEnumerator TypeSentence(string sentence)
    {
        animatingTyping = true;
        dialogueTxt.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueTxt.text += letter;
            
            if (textAnimationTime < 0)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(textAnimationTime);
            }
            
        }
        animatingTyping = false;
    }





    // ----------------------------
    //          TOOLS
    // ----------------------------

    /// <summary>
    /// Returns the string array that contains the sentences for the person(DialogueMessage) specified
    /// </summary>
    /// <param name="person">The index of the DialogueMessage array. The person conversing</param>
    private string[] GetSentenceArr(int person)
    {
        return dialogue.dialogueMessage[person].sentences;
    }
}
