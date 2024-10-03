using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement_1 : MonoBehaviour
{
    private EnemyController controller;
    [SerializeField] private float runSpd = 65f;                                 // Movement speed
    [SerializeField] private LayerMask whatIsGround;                           // A mask determining what is ground to the character
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    const float groundCheckRadius = .1f; // Radius of the overlap circle to determine if grounded
    private bool isGrounded;            // Whether or not the player is grounded.
    private Rigidbody2D rb;            // Rigidbody of the player
    private bool facingRight = true;  // For determining which way the player is currently facing.
    private Vector3 vel = Vector3.zero;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<EnemyController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (controller.isDead) return;

        bool wasGrounded = isGrounded;
        isGrounded = false;

        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }

        if (Physics2D.OverlapCircleAll(wallCheck.position, groundCheckRadius, whatIsGround).Length > 0) {
            Flip();
        }

        float _spd = runSpd;
        if (controller.isDemonMode) {
            _spd = runSpd * 1.5f;
        }
        Move((facingRight ? 1 : -1) * _spd * Time.deltaTime);
    }

    public void Move(float move)
    {

        // Move the character
        rb.velocity = new Vector2(move * 10f, rb.velocity.y); ;

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
    }


    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
