using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public LayerMask killLayer;
    public bool isDead = false;
    public int HP = 5;

    public SpriteRenderer spr;
    private float damagedTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -12f) {
            Destroy(gameObject);
        }

        if (!isDead)
        {
            Collider2D bullet = Physics2D.OverlapCircle(transform.position, 0.5f, killLayer);
            if (bullet)
            {
                HP -= bullet.gameObject.GetComponent<Bullet>().damage;
                damagedTime = 0.075f;
                Destroy(bullet.gameObject);
            }
            if (HP <= 0)
            {
                Died();
            }
        }
        else transform.Rotate(new Vector3(0, 0, 2f * transform.localScale.x));

        if (damagedTime > 0) {
            damagedTime -= Time.deltaTime;
            spr.color = Color.red;
        }
        else spr.color = Color.white;
    }

    void Died()
    {
        isDead = true;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity += new Vector2(0f, 15);
    }
}
