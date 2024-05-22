using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    public int damage = 1;
    public float spd = 200;
    public float slowDownSpd = 20f;
    public float lifetime = 1f;
    [SerializeField] private LayerMask destroyOnHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * spd * Time.deltaTime;
        if(spd > 0) spd -= Time.deltaTime * slowDownSpd;

        if (lifetime > 0)
        {
            lifetime -= Time.deltaTime;
        }
        else Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((destroyOnHit & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            Destroy(gameObject);
        }
        else {
            spd = -spd;
        }
    }
}
