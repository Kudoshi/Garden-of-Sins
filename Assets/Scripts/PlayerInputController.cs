using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public MovementSystem movementSystem;
    public InteractionManager interactionManager;
    public PlayerInput playerInput;
    public DialogueSO dialogueSO;

    private Rigidbody2D rb;

    private float movementValue = 0;
    private bool jump = false;
    void OnMovement(InputValue value)
    {
        movementValue = value.Get<float>();
    }
    void OnJump(InputValue value)
    {
        jump = value.isPressed;
    }

    void OnInteract(InputValue value)
    {
        interactionManager.Interact();
        startTime = Time.time;
    }
    private float startTime;
    void OnInteractDialogue(InputValue value)
    {
        dialogueSO.TriggerOnDialogueInteracted(cachedDialogue);
    }




    // Events
    private Dialogue cachedDialogue;

    public void OnDialogueStart(Dialogue dialogue)
    {
        playerInput.SwitchCurrentActionMap("Dialogue");
        cachedDialogue = dialogue;
    }
    public void OnDialogueEnd()
    {
        playerInput.SwitchCurrentActionMap("Platforming");
        cachedDialogue = null;
    }
    private void FixedUpdate()
    {
        movementSystem.Move(movementValue, false, jump);
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}



