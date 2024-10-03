using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementSkull : MonoBehaviour
{
    public float spd = 4f;
    public float moveControl = 2.5f;

    float velX;
    float velY;

    Rigidbody2D rb;
    Transform plr;
    private EnemyController controller;

    public LayerMask whatIsWall;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        plr = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        controller = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isDead)
        {
            rb.gravityScale = 1.5f;
            return;
        }

        var _dir = Mathf.Sign(plr.position.x - transform.position.x);
        velX += Mathf.Clamp(_dir * spd - velX, -moveControl, moveControl) * Time.deltaTime;
        velY += Mathf.Clamp(Mathf.Sign(plr.position.y - transform.position.y) * spd - velY, -moveControl, moveControl) * Time.deltaTime;

        rb.velocity = new Vector2(velX, velY);

        Vector3 theScale = transform.localScale;
        theScale.x = _dir;
        transform.localScale = theScale;

        // Collision on the right
        if (Physics2D.OverlapCircleAll(transform.position + Vector3.right * 0.25f, 0.1f, whatIsWall).Length > 0)
        {
            velX = -2;
        }

        // Collision on the left
        if (Physics2D.OverlapCircleAll(transform.position + Vector3.left * 0.25f, 0.1f, whatIsWall).Length > 0)
        {
            velX = 2;
        }

        // Collision on top
        if (Physics2D.OverlapCircleAll(transform.position + Vector3.up * 0.25f, 0.1f, whatIsWall).Length > 0)
        {
            velY = -2;
        }

        // Collision on the bottom
        if (Physics2D.OverlapCircleAll(transform.position + Vector3.down * 0.25f, 0.1f, whatIsWall).Length > 0)
        {
            velY = 2;
        }
    }
}
