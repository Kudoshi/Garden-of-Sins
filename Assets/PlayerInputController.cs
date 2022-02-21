using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public MovementSystem movementSystem;


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

    private void FixedUpdate()
    {
        movementSystem.Move(movementValue, false, jump);
    }
}
