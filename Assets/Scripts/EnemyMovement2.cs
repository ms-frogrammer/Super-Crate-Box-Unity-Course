using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement2 : MonoBehaviour
{

    Rigidbody2D rb;
    public float spd = 3f;

    public LayerMask whatIsGround;
    public Transform groundCheck;
    public Transform wallCheck;

    public float groundCheckRadius = 0.1f;
    private bool isGrounded;
    bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (Random.Range(0, 2) == 1) Flip();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);
        isGrounded = colliders.Length > 0;

        Collider2D[] colliders2 = Physics2D.OverlapCircleAll(wallCheck.position, groundCheckRadius, whatIsGround);
        if (colliders2.Length > 0) {
            Flip();
        }

        rb.velocity = new Vector2(transform.localScale.x * spd, rb.velocity.y);

        if (transform.position.y <= -4.5f && !GetComponent<EnemyController>().isDead) {
            transform.position = new Vector3(0f, 4.5f, 0f);
            if (Random.Range(0, 2) == 1) Flip();

        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
