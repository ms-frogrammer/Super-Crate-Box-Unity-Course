using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController controller;
    [SerializeField] private float jumpPower = 22;                                  // Immediate force added the moment player presses jump
    [SerializeField] private float jumpForce = 125;                                 // Force that is added while the player is holding the jump
    [SerializeField] private float jumpDuration = 0.2f;                            // How long the player can hold the jump button
    [SerializeField] private float runSpd = 70f;                                 // Movement speed
    [SerializeField] private LayerMask whatIsGround;                           // A mask determining what is ground to the character
    [SerializeField] private Transform groundCheck;                           // A position marking where to check if the player is grounded.
    private float cayoteeTime = 0.2f;                                        // Time after player dropped off the platfrom but still can jump
    const float groundCheckRadius = .1f; // Radius of the overlap circle to determine if grounded
    private bool isGrounded;            // Whether or not the player is grounded.
    private Rigidbody2D rb;            // Rigidbody of the player
    private bool facingRight = true;  // For determining which way the player is currently facing.
    private Vector3 vel = Vector3.zero;

    private float inpHor = 0f; // Horizontal input from the player

    // Jumping
    private float jumpTime = 0; // How much time passed since the button was pressed
    private bool isJumping = false; // Is currently jumping (holding the button)

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void Update()
    {
        if (controller.isDead) return;

        inpHor = Input.GetAxisRaw("Horizontal");
        if (Input.GetButton("Jump") && !isJumping && (isGrounded || cayoteeTime > 0f))
        {
            isJumping = true;
            jumpTime = 0f;
            cayoteeTime = 0f;
            isGrounded = false;
            rb.velocity = new Vector2(0f, jumpPower);
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTime > jumpDuration)
            {
                isJumping = false;
            }
            else jumpTime += Time.deltaTime;
        }
        else if (Input.GetButtonUp("Jump")) isJumping = false;
    }
    private void FixedUpdate()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }

        Move(inpHor * runSpd * Time.deltaTime, isJumping);
    }


    public void Move(float move, bool jump)
    {

        // Move the character
        rb.velocity = new Vector2(move * 10f, rb.velocity.y); ;

        if (isGrounded)
        {
            cayoteeTime = 0.1f;
        }
        else if(cayoteeTime > 0) cayoteeTime -= Time.deltaTime;

        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !facingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }

        // If the player should jump...
        if (jump)
        {
            // Add a vertical force to the player.
            isGrounded = false;
            rb.velocity += new Vector2(0f, jumpForce * Time.deltaTime);
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}