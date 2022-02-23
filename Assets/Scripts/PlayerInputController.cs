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
    public Player player;

    private Rigidbody2D rb;

    private float movementValue = 0;
    private bool jump = false;

    private Vector2 rightStickVal;

    void OnMovement(InputValue value)
    {
        movementValue = value.Get<float>();
    }
    void OnJump(InputValue value)
    {
        jump = value.isPressed;
    }
    void OnPlaceCube(InputValue value)
    {
        player.PlaceCube();
    }
    void OnRightStick(InputValue value)
    {
        rightStickVal = value.Get<Vector2>();
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
        UpdateBoxCursor();
    }

    private void UpdateBoxCursor()
    {
        movementSystem.Move(movementValue, false, jump);
        if (playerInput.currentControlScheme == "Controller")
        {
            player.CubePlacement(rightStickVal);

        }
        else if (playerInput.currentControlScheme == "KBM")
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            player.CubePlacementMouse(mousePos);
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}



