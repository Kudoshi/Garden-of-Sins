using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles movement of the character
/// Has unity events for OnLandEvent and OnCrouchEvent
/// 
/// Components required to be created:
/// - Input parameters: Movement value, crouch and jump
/// - Transform headCheck and groundCheck
/// 
/// You can adjust the movement in the inspector
/// 
/// Input: Move() - Receives movement value, crouch and jump 
/// </summary>
/// 

// TODO:
// Animation (Not sure how to integrate it nicely)
// Sound 
// Crouch is not implemented yet
// To implement crouch (2 ways):
// 1. Remove collider/s
// 2. Enable disable new collider sets
public class MovementSystem : MonoBehaviour
{
    [SerializeField] private Vector2 velocityViewOnly;
    [Header("Ground/Ceiling Check")]
    [SerializeField] private Transform headCheck; //Place headCheck on head
    [SerializeField] private Transform groundCheck; //Place groundCheck on feet
    [SerializeField] private LayerMask whatIsGround;

    [Header("Movement")]

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float smoothTime = 0.05f;
    [SerializeField] private float maxSpeed = 0; //Set 0 to disable max speed constraint
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private int jumpAmt = 2;
    [SerializeField] private float gravityForce = 0.75f;
    [SerializeField] private float gravityMaxSpeed = 0; //Set 0 to disable max speed constraint

    [Header("Others")]
    [SerializeField] private bool isFacingRight = true; //If player sprite is facing right -> true

    [Header("Events")]
    public UnityEvent OnLandEvent;
    public BoolEvent OnCrouchEvent;

  

    //Movement Var

    private Rigidbody2D rb;
    private Vector3 refVelo = Vector3.zero;
    private int jumpAmtLeft; //current data of amt of jump
    private bool isJumping = false;
    private bool isGrounded = false;

    //Constant information

    const float groundedRadius = .2f; // Circle size to check ground overlap
    const float headRadius = .2f; // Circle size to check head overlap

    // Event class for the crouch event
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpAmtLeft = jumpAmt;
    }

    private void Update()
    {
        velocityViewOnly = rb.velocity;
    }

    private void FixedUpdate()
    {
        CheckGround(); //Constant checking of ground
        ApplyGravity(); //Constant check when to apply gravity modifier when player is falling down
        SpeedBound(); //Constraints the max speed
    }

    /// <summary>
    /// Gets the movement parameters from an input controller
    /// Movement - velocity smooth damp
    /// Jump - addforce impuse
    /// 
    /// Jump:
    /// In the parameters in inspector. Allows the number of jumps
    /// 
    /// Prevents extra accident jumps by checking the isJumping variable.
    /// When jump is pressed, isJumping is set. It is resetted when the jump button is let go.
    /// Player can ony ljump again if they release their jump button.
    /// 
    /// Variables Affected:
    /// - rb.velocity
    /// - jumpAmtLeft
    /// - isJumping
    /// 
    /// </summary>
    /// <param name="moveVal">Horizontal float value</param>
    /// <param name="crouch">Bool if player is holding crouch</param>
    /// <param name="jump">Bool if player is holding jump</param>
    public void Move(float moveVal, bool crouch, bool jump)
    {
        //-- JUMP --

        //Jump is pressed/hold
        if (jump == true)
        {
            //Jump only if got jump and isn't jumping already
            if (jumpAmtLeft > 0 && !isJumping)
            {
                //Force jump
                rb.velocity = new Vector2(rb.velocity.x, 0); //Reset gravity
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                
                //Condition setting
                jumpAmtLeft -= 1;
                isJumping = true; 
            }
        }
        //When jump button is released
        else if (jump == false)
        {
            //Resets isJumping to allow player to jump again
            if (isJumping)
            {
                isJumping = false;
            }
        }

        // -- MOVEMENT --

        //Smoothdamp towards target velocity
        Vector3 targetVelocity = new Vector2(moveVal * moveSpeed, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref refVelo, smoothTime);

        // -- FLIP -- 

        if (moveVal > 0 && !isFacingRight) // Triggers if player moves right but transform looks left
        {
            Flip();
        }
        else if (moveVal <0 && isFacingRight) // Triggers if player moves left but transform looks right
        {
            Flip();
        }
    }

    /// <summary>
    /// Flips the transform of the object by reversing the transform local scale
    /// 
    /// Variables Affected:
    /// - isFacingRight
    /// - transform.localScale
    /// </summary>
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        isFacingRight = !isFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    /// <summary>
    /// Constraints the speed of gravity fall and movement speed
    /// It manually sets to the max speed if the velocity is exceeded
    /// 
    /// Variables Affected:
    /// - rb.velocity
    /// </summary>
    private void SpeedBound()
    {
        if (gravityMaxSpeed != 0 && rb.velocity.y < (gravityMaxSpeed))
        {
            rb.velocity = new Vector2(rb.velocity.x, gravityMaxSpeed);
        }
        if (maxSpeed != 0 && Mathf.Abs(rb.velocity.x) > maxSpeed )
        {
            float setSpeed = rb.velocity.x > 0 ? maxSpeed : -maxSpeed;
            rb.velocity = new Vector2(setSpeed, rb.velocity.y);
        }
    }

    /// <summary>
    /// Constantly checks ground to terni whether it is grounded by casting circles and checking the colliders.
    /// 1. It first sets grounded to false
    /// 2. If ground is detected it is set to true. Else it will continue being false
    /// 3. If previously the character isnt grounded then it becomes grounded now. It will invoke OnLandEvent
    /// 
    /// Variables Affected:
    /// - isGrounded
    /// - OnLandEvent
    /// - jumpAmtLeft
    /// </summary>
    private void CheckGround()
    {
        // Sets grounded to false
        bool wasGrounded = isGrounded;
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        // There are ground obj collided
        for (int i = 0; i < colliders.Length; i++) 
        {
            if (colliders[i].gameObject == gameObject) //Skip if the gameobject collided is original GameObj
                continue;

            isGrounded = true; //Sets grounded
            
            if (!wasGrounded)
            {
                jumpAmtLeft = jumpAmt; //Resets Jump amount
                OnLandEvent.Invoke();
            }
                
        }
    }

    /// <summary>
    /// Apply gravity modifier when the player is in falling in the air
    /// 
    /// Variables Affected:
    /// - rb.velocity
    /// </summary>
    private void ApplyGravity()
    {
        //Apply gravity
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics.gravity.y *gravityForce;
        }
    }
}
