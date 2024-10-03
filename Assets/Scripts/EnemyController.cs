using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool isDead = false;
    public int HP = 5;

    public SpriteRenderer spr;
    private float damagedTime = 0;

    [HideInInspector]
    public bool isDemonMode = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -12f) {
            if (isDead) Destroy(gameObject);
            else {
                isDemonMode = true;
                transform.position = new Vector3(0, 5.5f, 0);
            }
           
        }

        if (!isDead)
        {
            if (HP <= 0)
            {
                Died();
            }
        }
        else transform.Rotate(new Vector3(0, 0, 2f * transform.localScale.x));

        if (damagedTime > 0) {
            damagedTime -= Time.deltaTime;

            spr.color = isDemonMode ? Color.white : Color.red;
        }
        else spr.color = isDemonMode ? Color.red : Color.white;



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;
        if (collision.gameObject.tag == "Bullet")
        {
            HP -= collision.gameObject.GetComponent<Bullet>().damage;
            damagedTime = 0.075f;
            Destroy(collision.gameObject);
        }
    }
    void Died()
    {
        isDead = true;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity += new Vector2(0f, 15);

    }
}
