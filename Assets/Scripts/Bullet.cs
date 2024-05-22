using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float spd = 200;
    [SerializeField] private LayerMask destroyOnHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * spd * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((destroyOnHit & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer) { 
            Destroy(gameObject);
        }
    }
}
